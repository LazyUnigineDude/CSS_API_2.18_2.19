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

        private static float Flip(float t) { return 1 - t; }
        private static float Square(float t) { return t * t; }
    }
}
