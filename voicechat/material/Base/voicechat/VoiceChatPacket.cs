using System.Collections;

namespace VoiceChat.Base
{
    public struct VoiceChatPacket
    {
        public VoiceChatCompression Compression;
        public int Length;
        public byte[] Data;
        public int NetworkId;
        public ulong PacketId;
    }

}