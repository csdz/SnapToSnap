using System.Collections;

namespace VoiceChat.Base
{
    public enum VoiceChatCompression : byte
    {
        /*
        Raw, 
        RawZlib, 
        */
        Alaw,
        AlawZlib,
        Speex
    } 
}
