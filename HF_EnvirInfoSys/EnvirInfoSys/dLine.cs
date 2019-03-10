namespace EnvirInfoSys
{
	public class dLine
	{
		public dPoint pa;

		public dPoint pb;

		public dLine(dPoint a, dPoint b)
		{
			pa = a;
			pb = b;
		}

		public dPoint GetCross(double Y)
		{
			double p = pa.x - (pa.y - Y) * (pa.x - pb.x) / (pa.y - pb.y);
			return new dPoint(p, Y);
		}
	}
}
