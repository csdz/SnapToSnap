//example.cpp

#include <openssl/pem.h>
#include <openssl/ssl.h>
#include <openssl/rsa.h>
#include <openssl/evp.h>
#include <openssl/bio.h>
#include <openssl/err.h>
#include <stdio.h>
 
int padding = RSA_PKCS1_PADDING;

RSA* createRSAWithFilename(const char*, int);

int public_encrypt(unsigned char * data,int data_len, const char* pub_fp, unsigned char *encrypted)
{
    RSA * rsa = createRSAWithFilename(pub_fp,1);
    int result = RSA_public_encrypt(data_len,data,encrypted,rsa,padding);
    return result;
}
int private_decrypt(unsigned char * enc_data,int data_len, const  char* pri_fp, unsigned char *decrypted)
{
    RSA * rsa = createRSAWithFilename(pri_fp,0);
    int  result = RSA_private_decrypt(data_len,enc_data,decrypted,rsa,padding);
    return result;
}
 
 
int private_encrypt(unsigned char * data,int data_len, const char* pri_fp, unsigned char *encrypted)
{
    RSA * rsa = createRSAWithFilename(pri_fp,0);
    int result = RSA_private_encrypt(data_len,data,encrypted,rsa,padding);
    return result;
}
int public_decrypt(unsigned char * enc_data,int data_len, const char *pub_fp, unsigned char *decrypted)
{
    RSA * rsa = createRSAWithFilename(pub_fp,1);
    int  result = RSA_public_decrypt(data_len,enc_data,decrypted,rsa,padding);
    return result;
}
 
void printLastError(char *msg)
{
    char * err = (char*)malloc(130);
    ERR_load_crypto_strings();
    ERR_error_string(ERR_get_error(), err);
    printf("%s ERROR: %s\n",msg, err);
    free(err);
}

RSA * createRSAWithFilename(const char * filename,int nPublic)
{
    FILE * fp = fopen(filename,"rb");
 
    if(fp == NULL)
    {
        printf("Unable to open file %s \n",filename);
        return NULL;    
    }
    RSA *rsa= RSA_new() ;
 
    if(nPublic)
    {
        rsa = PEM_read_RSA_PUBKEY(fp, &rsa,NULL, NULL);
    }
    else
    {
        rsa = PEM_read_RSAPrivateKey(fp, &rsa,NULL, NULL);
    } 
    return rsa;
}
 
int main(){
 
    unsigned char plainText[2048/8] = "Hello this is tab_space";
    unsigned char  encrypted[4098]={};
    unsigned char decrypted[4098]={};
    
    const char* pub_fp = "/home/wsn/crypt/res/public.pem";
    const char* pri_fp = "/home/wsn/crypt/res/private.pem";
    int encrypted_length= public_encrypt(plainText,strlen((const char*)plainText),pub_fp,encrypted);
    if(encrypted_length == -1)
    {
        printLastError((char*)"Public Encrypt failed ");
        exit(0);
    }
    printf("Encrypted length =%d\n",encrypted_length);
     
    int decrypted_length = private_decrypt(encrypted,encrypted_length, pri_fp, decrypted);
    if(decrypted_length == -1)
    {
        printLastError((char*)"Private Decrypt failed ");
        exit(0);
    }
    printf("Decrypted Text =%s\n",decrypted);
    printf("Decrypted Length =%d\n",decrypted_length);
     
     
    encrypted_length= private_encrypt(plainText,strlen((const char*) plainText),pri_fp,encrypted);
    if(encrypted_length == -1)
    {
        printLastError((char*)"Private Encrypt failed");
        exit(0);
    }
    printf("Encrypted length =%d\n",encrypted_length);
     
    decrypted_length = public_decrypt(encrypted,encrypted_length,pub_fp, decrypted);
    if(decrypted_length == -1)
    {
        printLastError((char*)"Public Decrypt failed");
        exit(0);
    }
    printf("Decrypted Text =%s\n",decrypted);
    printf("Decrypted Length =%d\n",decrypted_length);
 
}