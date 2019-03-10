using System.Collections.Generic;

namespace EnvirInfoSys
{
	public class Polygon
	{
		public int len
		{
			get;
			set;
		}

		public List<dPoint> ploygon
		{
			get;
			set;
		}

		public Polygon()
		{
			len = 0;
			ploygon = new List<dPoint>();
		}

		public Polygon(List<double[]> list)
		{
			len = list.Count;
			ploygon = new List<dPoint>();
			for (int i = 0; i < len; i++)
			{
				ploygon.Add(new dPoint(list[i][0], list[i][1]));
			}
		}

		public bool PointInPolygon(dPoint p)
		{
			if (len < 3)
			{
				return false;
			}
			int index = len - 1;
			bool flag = false;
			for (int i = 0; i < len; i++)
			{
				if (((ploygon[i].y < p.y && ploygon[index].y >= p.y) || (ploygon[index].y < p.y && ploygon[i].y >= p.y)) && (ploygon[i].x <= p.x || ploygon[index].x <= p.x))
				{
					flag ^= (ploygon[i].x + (p.y - ploygon[i].y) / (ploygon[index].y - ploygon[i].y) * (ploygon[index].x - ploygon[i].x) < p.x);
				}
				index = i;
			}
			return flag;
		}

		public dPoint GetAPoint()
		{
			List<dPoint> list = new List<dPoint>();
			double num = 0.0;
			for (int i = 0; i < len; i++)
			{
				num += ploygon[i].y;
			}
			num /= (double)len;
			for (int i = 0; i < len; i++)
			{
				dLine dLine = new dLine(ploygon[i], ploygon[(i + 1) % len]);
				if ((ploygon[i].y > num) ^ (ploygon[(i + 1) % len].y > num))
				{
					list.Add(dLine.GetCross(num));
				}
			}
			if (list.Count < 2)
			{
				return new dPoint(0.0, 0.0);
			}
			list.Sort((dPoint x, dPoint y) => y.x.CompareTo(x.x));
			return new dPoint((list[0].x + list[1].x) / 2.0, (list[0].y + list[1].y) / 2.0);
		}
	}
}
