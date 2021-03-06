using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace.Business
{
    public class DataHelper
    {
        public static string[] ConvertList(string value, int type)
        {
            string[] vs = null;//结果集
            if (type == 1)
                vs = new string[70];
            else
                vs = new string[36];
            List<string> ls = DataSplit(value, type);//数据拆分后的数组

            int nowIndex = 0;//当前拆分数组的位置
            int nowValue = 0;//当前有效数组的值
            for (int i = 0; i < ls.Count; i++)
            {
                var start = ls[i].Substring(0, 1);
                var myIndex = ls[i].Substring(1, 2);
                switch (start)
                {
                    case "2":

                        if (i == 0)
                        {
                            vs[nowIndex] = "0";
                        }
                        else
                        {
                            int cc = Convert.ToInt32(myIndex) - nowIndex;
                            for (int j = 0; j <= cc; j++)
                            {
                                vs[nowIndex + j + 1] = (nowValue + 1) + "";
                            }
                        }
                        nowIndex = Convert.ToInt16(myIndex);
                        nowValue = Convert.ToInt16(vs[nowIndex]);
                        break;
                    case "0"://在前面一个基础上加一
                        vs[Convert.ToInt16(myIndex)] = (nowValue + 1) + "";
                        nowIndex = Convert.ToInt16(myIndex);
                        nowValue = Convert.ToInt16(vs[nowIndex]);
                        break;
                    case "8"://跟前面一个不是8开头的一样
                        vs[Convert.ToInt16(nowIndex) + 1] = nowValue + "";
                        nowIndex += 1;
                        break;
                    case "3"://跟前面一个一样,个数需要减一下
                        vs[Convert.ToInt16(myIndex)] = nowValue + "";
                        nowIndex = Convert.ToInt16(myIndex);
                        break;
                    case "4":
                        vs[Convert.ToInt16(myIndex)] = "0";
                        break;
                    case "_":
                        var laststart = "2";
                        for (var j=1; ; j++)
                        {
                            var lss = ls[i-j].Substring(0, 1);
                            if (!lss.Equals("_"))
                            {
                                laststart = lss;
                                myIndex = (Convert.ToInt16(ls[i - j].Substring(1, 2)) + j)+"";
                                break ;
                            }
                        }
                        if (laststart.Equals("2"))//上升
                        {
                            vs[Convert.ToInt16(myIndex)] = (nowValue + 1) + "";
                        }
                        else
                        {
                            vs[Convert.ToInt16(myIndex)] = (nowValue - 1) + "";
                        }
                        nowValue = Convert.ToInt16(vs[Convert.ToInt16(myIndex)]);
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
            for (int i = 0; i < l; i++)
            {
                ls.Add(value.Substring(i * c, c));
            }

            return ls;
        }
    }
}
