using System.Runtime.CompilerServices;
using Unigine;

namespace UnigineApp.data.Timer
{
    internal static class Lerper
    {
        public enum LERP_TYPE { LINEAR, EASE_IN, EASE_OUT, EASE_IN_OUT };
        public static dvec3 Lerp(dvec3 Start, dvec3 End, float t, LERP_TYPE Type = LERP_TYPE.LINEAR) 
        { 
            switch (Type)
            {
                case LERP_TYPE.EASE_IN:     return MLerp(Start, End, EIn(t));
                case LERP_TYPE.EASE_OUT:    return MLerp(Start, End, EOut(t));
                case LERP_TYPE.EASE_IN_OUT: return MLerp(Start, End, EInOut(t));
                default:                    return MLerp(Start, End, t);
            }
        }

        public static dvec3 PingPong(dvec3 Start, dvec3 End, float t, float Repeat)
        {
            return new dvec3(
                PingPong(Start.x, End.x, t, Repeat),
                PingPong(Start.y, End.y, t, Repeat),
                PingPong(Start.z, End.z, t, Repeat)
                );
        }

        private static dvec3 MLerp(dvec3 Start, dvec3 End, float t)
        {
            return new dvec3(
                Lerp(Start.x, End.x, t),
                Lerp(Start.y, End.y, t),
                Lerp(Start.z, End.z, t)
                );
        }

        private static float Lerp(float Start, float End, float t) { return Start + t * (End - Start); }
        private static double Lerp(double Start, double End, float t) { return Start + t * (End - Start); }
        private static float EIn(float t) { return Square(t); }
        private static float EOut(float t) { return Flip(Square(Flip(t))); }
        private static float EInOut(float t) { return Lerp(EIn(t), EOut(t), t) ; }
        private static double PingPong(double min, double max, float t, float repeat) {

            double hRepeat = repeat * 0.5;
            double phase = t % repeat;
            double slope = (max - min) / hRepeat;

            if(phase < hRepeat) { return min + (slope * phase); }
            return max - (slope * (phase - hRepeat));
        }

        private static float Flip(float t) { return 1 - t; }
        private static float Square(float t) { return t * t; }
    }
}
