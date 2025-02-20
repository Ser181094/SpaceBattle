namespace SpaceBattle
{
    public class Angle
    {
        public int n;
        public int d;

        public Angle(int n, int d)
        {
            this.d = d;
            this.n = n;
        }

        public static Angle Plus(Angle a1, Angle a2)
        {
            if (a1.n != a2.n)
            {
                throw new ArgumentException();
            }
            return new Angle((a1.d + a2.d) % a1.n, a1.n);
        }
    }
}
