
namespace VoiceChat.Base
{
    public class VoiceChatInnerSettings
    {
        public static int Frequency = 16000;

        public static int SampleSize = 640;

        public static VoiceChatCompression Compression = VoiceChatCompression.Speex;

        public static VoiceChatPreset Preset = VoiceChatPreset.Speex_16K;

        public static bool localDebug = false;
    }
}
