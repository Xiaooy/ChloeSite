using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace.Business
{
    public class DataHelper
    {
        public static string[] ConvertList(string value,int type)
        {
            string[] vs = null;//结果集
            if(type==1)
                vs = new string[70];
            else 
                vs = new string[36];
            List<string> ls = DataSplit(value, type);//数据拆分后的数组

            for (int i=0;i<ls.Count;i++)
            {
                var start = ls[i].Substring(0, 1);
                switch (start)
                {
                    case "2":
                        if (i == 0)
                        {
                            vs[Convert.ToInt32(ls[i].Substring(1, 2))] = "0";
                        }
                        else
                        {
                            int cc = Convert.ToInt32(ls[i].Substring(1, 2)) - Convert.ToInt32(ls[i - 1].Substring(1, 2));
                            for (int j = 0; j <= cc; j++)
                            {
                                vs[Convert.ToInt32(ls[i - 1].Substring(1, 2)) + j + 1] = (Convert.ToInt32((vs[Convert.ToInt32(ls[i - 1].Substring(1, 2))])) + 1) + "";
                            }
                            
                        }
                        
                        break;
                    case "0"://在前面一个基础上加一
                        vs[Convert.ToInt32(ls[i].Substring(1, 2))] = (Convert.ToInt32((vs[Convert.ToInt32(ls[i - 1].Substring(1, 2))])) + 1) + "";
                        break;
                    case "8"://跟前面一个不是8开头的一样
                        for (int t = 1; ; t++)
                        {
                            int ps = Convert.ToInt32(ls[i - t].Substring(0, 1));
                            if (ps != 8)
                            {
                                int ln = Convert.ToInt32(ls[i - t].Substring(1, 2));
                                vs[ln + t] = vs[ln];
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        break;
                    case "3"://跟前面一个一样,个数需要减一下
                        int pp = Convert.ToInt32(ls[i].Substring(1, 2)) - Convert.ToInt32(ls[i - 1].Substring(1, 2));
                        for (int j = 0; j < pp; j++)
                        {
                            vs[Convert.ToInt32(ls[i - 1].Substring(1, 2)) + j + 1] = (vs[Convert.ToInt32(ls[i - 1].Substring(1, 2))]);
                        }
                        break;
                    case "4": 
                        vs[Convert.ToInt32(ls[i].Substring(1, 2))] = "0";
                        break;
                    case "_":
                        for (int t = 1; ; t++)
                        {
                            if (ls[i - t].Substring(0, 1).Equals("_")) continue;
                            int ps = Convert.ToInt32(ls[i - t].Substring(0, 1));
                            if (ps==2)//上升
                            {
                                int ln = Convert.ToInt32(ls[i - t].Substring(1, 2));
                                vs[ln + t] = (Convert.ToInt32(vs[ln]) + t) + "";
                                break;
                            }
                            else//下降
                            {
                                int ln = Convert.ToInt32(ls[i - t].Substring(1, 2));
                                vs[ln + t] = (Convert.ToInt32(vs[ln]) - t) + "";
                                break;
                            }
                        }
                        
                        break;
                    default:
                        break;
                }
            }


            return vs;

        }



        private static List<string> DataSplit(string value, int type)
        {
            int c = (type == 1 ? 3 : 4);
            int l = value.Length / c;
            List<string> ls = new List<string>();
            for(int i = 0; i < l; i++)
            {
                ls.Add(value.Substring(i*c,c));
            }

            return ls;
        }
    }
}
