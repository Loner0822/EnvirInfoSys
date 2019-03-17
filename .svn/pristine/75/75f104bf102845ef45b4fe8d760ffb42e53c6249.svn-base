using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishSys
{
    public class DPoint
    {
        public double x
        {
            get;
            set;
        }

        public double y
        {
            get;
            set;
        }

        public DPoint(double p1, double p2)
        {
            x = p1;
            y = p2;
        }
    }

    public class DLine
    {
        public DPoint pa;

        public DPoint pb;

        public DLine(DPoint a, DPoint b)
        {
            pa = a;
            pb = b;
        }

        public DPoint GetCross(double Y)
        {
            double p = pa.x - (pa.y - Y) * (pa.x - pb.x) / (pa.y - pb.y);
            return new DPoint(p, Y);
        }
    }

    public class LineData
    {
        public string Type
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public string Color
        {
            get;
            set;
        }

        public double Opacity
        {
            get;
            set;
        }

        public void Get_NewLine()
        {
            Type = "实线";
            Width = 1;
            Color = "#000000";
            Opacity = 1.0;
        }

        public void Load_Line(string markerguid)
        {
            string sql = "select * from ENVIRLINE_H0001Z000E00 where ISDELETE = 0 and UPGUID = '" + markerguid + "'";
            DataTable dataTable = FileReader.often_ahp.ExecuteDataTable(sql);
            if (dataTable.Rows.Count > 0)
            {
                Type = dataTable.Rows[0]["LINETYPE"].ToString();
                Width = int.Parse(dataTable.Rows[0]["LINEWIDTH"].ToString());
                Color = dataTable.Rows[0]["LINECOLOR"].ToString();
                Opacity = double.Parse(dataTable.Rows[0]["LINEOPACITY"].ToString());
            }
            else
            {
                Type = null;
                Width = 0;
                Color = null;
                Opacity = 0.0;
            }
        }

        public void Save_Line(string markerguid)
        {
            string sql = "select PGUID from ENVIRLINE_H0001Z000E00 where ISDELETE = 0 and UPGUID = '" + markerguid + "'";
            DataTable dataTable = FileReader.often_ahp.ExecuteDataTable(sql);
            if (dataTable.Rows.Count > 0)
            {
                sql = "update ENVIRLINE_H0001Z000E00 set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', LINETYPE = '" + Type + "', LINEWIDTH = " + Width + ", LINECOLOR = '" + Color + "', LINEOPACITY = '" + Opacity.ToString() + "' where ISDELETE = 0 and UPGUID = '" + markerguid + "'";
                FileReader.often_ahp.ExecuteSql(sql);
            }
            else
            {
                sql = "insert into ENVIRLINE_H0001Z000E00 (PGUID, S_UDTIME, UPGUID, LINETYPE, LINEWIDTH, LINECOLOR, LINEOPACITY) values ('" + Guid.NewGuid().ToString("B") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + markerguid + "', '" + Type + "', " + Width + ", '" + Color + "', '" + Opacity.ToString() + "')";
                FileReader.often_ahp.ExecuteSql(sql);
            }
        }
    }

    public class Polygon
    {
        public int len
        {
            get;
            set;
        }

        public List<DPoint> ploygon
        {
            get;
            set;
        }

        public Polygon()
        {
            len = 0;
            ploygon = new List<DPoint>();
        }

        public Polygon(List<double[]> list)
        {
            len = list.Count;
            ploygon = new List<DPoint>();
            for (int i = 0; i < len; i++)
            {
                ploygon.Add(new DPoint(list[i][0], list[i][1]));
            }
        }

        public bool PointInPolygon(DPoint p)
        {
            if (len < 3)
            {
                return true;
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

        public DPoint GetAPoint()
        {
            List<DPoint> list = new List<DPoint>();
            double num = 0.0;
            for (int i = 0; i < len; i++)
            {
                num += ploygon[i].y;
            }
            num /= (double)len;
            for (int i = 0; i < len; i++)
            {
                DLine DLine = new DLine(ploygon[i], ploygon[(i + 1) % len]);
                if ((ploygon[i].y > num) ^ (ploygon[(i + 1) % len].y > num))
                {
                    list.Add(DLine.GetCross(num));
                }
            }
            if (list.Count < 2)
            {
                return new DPoint(0.0, 0.0);
            }
            list.Sort((DPoint x, DPoint y) => y.x.CompareTo(x.x));
            return new DPoint((list[0].x + list[1].x) / 2.0, (list[0].y + list[1].y) / 2.0);
        }
    }
}
