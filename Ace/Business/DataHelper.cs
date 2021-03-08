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
                vs = new string[81];
            else
                vs = new string[72];
            List<string> ls = DataSplit(value, type);//数据拆分后的数组
            //210218220023888888328______431
            //233237244246048367_________471
            //208_________012331_________435
            //21352140214521502155016088888888319532003210322032304240                                            
            int nowIndex = 0;//当前拆分数组的位置
            int nowValue = 0;//当前有效数组的值
            int indexLength = type == 1 ? 2 : 3;
            for (int i = 0; i < ls.Count; i++)
            {
                var start = ls[i].Substring(0, 1);
                var myIndex = type == 1 ? ls[i].Substring(1, indexLength) : Convert.ToInt16(ls[i].Substring(1, indexLength)) / 10 * 2 + "";
                //前一个点的类型和序号
                var pt = i == 0 ? start : ls[i - 1].Substring(0, 1);
                var pi = i == 0 ? myIndex : (type == 1 ? ls[i].Substring(1, indexLength) : Convert.ToInt16(ls[i].Substring(1, indexLength)) / 10 * 2 + "");
                switch (start)
                {
                    case "2":

                        if (i == 0)
                        {
                            vs[Convert.ToInt16(myIndex)] = "0";
                        }
                        else
                        {
                            int cc = Convert.ToInt32(myIndex) - nowIndex;
                            for (int j = 1; j <= cc; j++)
                            {
                                vs[nowIndex + j] = (nowValue+1) + "";
                            }
                        }
                        nowIndex = Convert.ToInt16(myIndex);
                        nowValue = Convert.ToInt16(vs[nowIndex]);
                        break;
                    case "0"://在前面一个基础上加一
                        
                        if (pt.Equals("_"))
                        {
                            vs[Convert.ToInt32(myIndex)] = (nowValue + 1) + "";
                        }
                        else
                        {
                            float aa = Convert.ToInt32(myIndex) - nowIndex;
                            float v = 1 / aa;
                            for (int j = 1; j <= aa; j++)
                            {
                                //vs[nowIndex + j] = (nowValue + j) + "";
                                vs[nowIndex + j] = j == aa ? (nowValue + 1) + "" : (nowValue + v * j) + "";
                            }
                        }
                        nowIndex = Convert.ToInt16(myIndex);
                        nowValue = Convert.ToInt16(vs[nowIndex]);
                        break;
                    case "8"://后面一个不是8的位置减去前面一个的位置
                        
                        for (var j = 1; ; j++)
                        {
                            var lss = ls[i + j].Substring(0, 1);
                            if(!lss.Equals("8"))
                            {
                                var not8index = ls[i + j].Substring(1, indexLength);
                                var cc = Convert.ToInt16(not8index) - Convert.ToInt16(nowIndex);//差值
                                for (int o = 0; o < cc; o++)
                                {
                                    vs[Convert.ToInt16(nowIndex) + o] = nowValue + "";
                                }
                                break;
                            }
                        }
                        break;
                    case "3"://跟前面一个一样,个数需要减一下
                       
                         var dd = Convert.ToInt16(myIndex) - Convert.ToInt16(nowIndex);//差值
                            
                         for (var j = 1; j <= dd; j++)
                         {
                            if (pt.Equals("3"))
                            {
                                vs[nowIndex + j] = (nowValue - 1) + "";
                            }
                            else
                            {
                                vs[nowIndex + j] = (nowValue) + "";
                            }
                        }
                        nowIndex = Convert.ToInt16(myIndex);
                        nowValue = Convert.ToInt16(vs[nowIndex]);
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
                                myIndex = (Convert.ToInt16(ls[i - j].Substring(1, indexLength)) + j)+"";
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
