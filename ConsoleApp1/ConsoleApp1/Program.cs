using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class List1
    {
        public string PLAY_ID { get; set; }
        public string SONG_ID { get; set; }
        public string CLIENT_ID { get; set; }
        public string PLAY_TS { get; set; }
    }

    class List2
    {
        public string DISTINCT_PLAY_COUNT { get; set; }
        public string CLIENT_ID { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<List1> empList1 = new List<List1>();
            List<List2> empList2 = new List<List2>();

            using (var reader = new StreamReader("exhibitA-input.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split('\t');
                    var datetime = values[3].Split(' ');
                    if (datetime[0] == "10/08/2016")
                    {
                        empList1.Add(new List1() { PLAY_ID = values[0], SONG_ID = values[1], CLIENT_ID = values[2], PLAY_TS = values[3] });
                    }
                }
            }

            var groupedDistinct1 = empList1.GroupBy(x => x.CLIENT_ID)
                  .Select(x => new { CLIENT_ID = x.Key, DISTINCT_PLAY_COUNT = x.Select(y => y.SONG_ID).ToList() })
                  .ToList();

            //var groupedDistinct1 = empList1.GroupBy(x => x.CLIENT_ID)
            //      .Select(x => new { CLIENT_ID = x.Key, DISTINCT_PLAY_COUNT = x.Select(y => y.SONG_ID).ToList() })
            //      .ToList();


            //for (var j = 0; j < empList1.Count; j++)
            //    Console.WriteLine(j + "\t" + empList1[j].CLIENT_ID + "\t" + empList1[j].SONG_ID);
            //for (var j = 0; j < 15; j++)//groupedDistinct1.Count; j++)
            //{
            //    Console.WriteLine(j + "\t" + groupedDistinct1[j].CLIENT_ID + "\t\t\t" + groupedDistinct1[j].DISTINCT_PLAY_COUNT.Distinct().Count());
            //    for (var k = 0; k < groupedDistinct1[j].DISTINCT_PLAY_COUNT.Count; k++)
            //        Console.WriteLine("\t" + k + "\t" + groupedDistinct1[j].DISTINCT_PLAY_COUNT[k]);
            //}

            for (var i = 0; i < groupedDistinct1.Count; i++)
            {
                empList2.Add(new List2() { CLIENT_ID = groupedDistinct1[i].CLIENT_ID, DISTINCT_PLAY_COUNT = groupedDistinct1[i].DISTINCT_PLAY_COUNT.Distinct().Count().ToString() });
                //Console.WriteLine(groupedDistinct1[i].CLIENT_ID +"\t"+ groupedDistinct1[i].DISTINCT_PLAY_COUNT.Distinct().Count().ToString());

            }


            //for (var j = 0; j < empList2.Count; j++)
            //    Console.WriteLine(j + "\t" + empList2[j].CLIENT_ID + "\t" + empList2[j].DISTINCT_PLAY_COUNT);

            var groupedDistinct2 = empList2.GroupBy(x => x.DISTINCT_PLAY_COUNT)
            .Select(x => new { CLIENT_ID_COUNT = x.Key, DISTINCT_PLAY_COUNT = x.Select(y => y.DISTINCT_PLAY_COUNT).ToList() })
            .ToList();

            //Console.WriteLine("Liste2");
            //for (var l = 0; l < groupedDistinct2.Count; l++)
            //    Console.WriteLine(groupedDistinct2[l].CLIENT_ID_COUNT + "\t" + groupedDistinct2[l].DISTINCT_PLAY_COUNT.Count);

            using (StreamWriter writetext = new StreamWriter("output.csv"))
            {
                writetext.WriteLine("DISTINCT_PLAY_COUNT\tCLIENT_COUNT");
                for (var i = 0; i < groupedDistinct2.Count; i++)
                {
                    writetext.WriteLine(groupedDistinct2[i].CLIENT_ID_COUNT + "\t" + groupedDistinct2[i].DISTINCT_PLAY_COUNT.Count());
                }
            }


            }


        }
}
