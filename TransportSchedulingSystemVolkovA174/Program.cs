using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportSchedulingSystemVolkovA174
{
    class Program
    {
        static void ReadData(out List<Route> Routes, out List<Stop> Stops, out List<StopInRoute> stopsInRoutes, out int count)
        {
            Routes = new List<Route>();
            Stops = new List<Stop>();
            stopsInRoutes = new List<StopInRoute>();

            using (var sr = new StreamReader("../../schedule.txt"))
            {
                string line;
                string[] parts;

                count = int.Parse(sr.ReadLine());

                line = sr.ReadLine();
                parts = line.Split(';');

                int stopsCount = parts.Length;
                string[] psrtsStations = new string[parts.Length * 2];
                string[] intermediateParts = new string[2];

                for (int i = 0; i < stopsCount; i++)
                {
                    intermediateParts = parts[i].Split('-');
                    psrtsStations[i * 2] = intermediateParts[0];
                    psrtsStations[i * 2 + 1] = intermediateParts[1];
                }

                for (int i = 0; i < stopsCount; i++)
                {
                    var allStops = new Stop
                    {
                        Id = int.Parse(psrtsStations[i * 2 + 1]),
                        Name = (psrtsStations[i * 2])
                    };
                    Stops.Add(allStops);
                }

                for (int k = 0; k < count; k++)
                {
                    int route = int.Parse(sr.ReadLine());

                    line = sr.ReadLine();
                    parts = line.Split(';');

                    int countIBS = parts.Length;
                    int[] ibs = new int[countIBS];
                    for (int i = 0; i < countIBS; i++)
                    {
                        ibs[i] = int.Parse(parts[i]);
                    };

                    line = sr.ReadLine();
                    parts = line.Split(';');

                    int countStops = parts.Length;
                    List<int> allStopsArray = new List<int>();
                    for (int i = 0; i < countStops; i++)
                    {
                        allStopsArray.Add(int.Parse(parts[i]));
                    };

                    line = sr.ReadLine();
                    parts = line.Split(';');

                    var Route = new Route
                    {
                        Id = route,
                        TimeStart = int.Parse(parts[0]),
                        TimeEnd = int.Parse(parts[1]),
                        Interval = int.Parse(parts[2]),
                        arrayStops = allStopsArray
                    };
                    Routes.Add(Route);

                    var stopInRoute = new StopInRoute
                    {
                        Routes = Routes[k],
                        Stops = FindStops(Stops, Routes[k].arrayStops),
                        IBS = ibs
                    };
                    stopsInRoutes.Add(stopInRoute);
                }
            }
        }

        static int FindNeededId(out string enter, List<Stop> Stops)
        {
            int neededId = -10;
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Enter station: ");

                Console.ForegroundColor = ConsoleColor.Cyan;
                enter = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                enter = enter.ToUpper();
                foreach (var item in Stops)
                {
                    if (item.Name == enter)
                        neededId = item.Id;
                }

                if (neededId == -10)
                    Console.WriteLine("No such station!");
            } while (neededId == -10 && !(string.IsNullOrWhiteSpace(enter)));

            return neededId;
        }

        static int ShowCurrentTime() // отображение настоящего времени и возврещение ctim (Current Time In Minutes)
        {
            Console.WriteLine("Current time: " + DateTime.Now.Hour + DateTime.Now.ToString(":mm"));
            Console.WriteLine("Schedule:");
            int cth = DateTime.Now.Hour;
            int ctm = DateTime.Now.Minute;
            int ctim = cth * 60 + ctm;

            return ctim;
        }

        static string GetNameById(int id, string stationName, List<Stop> Stops)
        {
            foreach (var item in Stops)
            {
                if (item.Id == id)
                    stationName = item.Name;
            }
            return stationName;
        }

        static List<Stop> FindStops(List<Stop> stops, List<int> arrayStops) // поиск конкретных станций для маршрута
        {
            List<Stop> ExactStops = new List<Stop>();

            foreach (var stop in arrayStops)
            {
                for (int i = 0; i < stops.Count; i++)
                {
                    if (stop == stops[i].Id)
                        ExactStops.Add(stops[i]);
                }
            }
            return ExactStops;
        }

        static int MinutesFormLeft(int[] array, List<int> asa, int id, int ctimLeft) // суммируем минуты слева
        {
            int IntervalSumLeft = 0;
            for (int i = asa.IndexOf(id); i >= 0; i--)
            {
                IntervalSumLeft += array[i];
            }

            ctimLeft -= IntervalSumLeft;

            return ctimLeft;
        }

        static int MinutesFromRight(int[] array, List<int> asa, int id, int ctimRight) // суммируем минуты справа
        {
            int IntervalSumRight = 0;
            for (int i = asa.IndexOf(id); i < array.Length - 1; i++)
            {
                IntervalSumRight += array[(i + 1)];
            }

            ctimRight -= IntervalSumRight;

            return ctimRight;
        }

        static void MinutesToStop(int timeStart, int timeEnd, int interval, int lastCtim, int intervalSum, int ctim, out int min) // основной алгоритм, который возращает минимальное расстояние до станции
        {
            min = -10;

            while (timeStart < lastCtim && timeEnd >= lastCtim)
            {
                min = intervalSum - (ctim - (timeStart + interval));
                timeStart += interval;
            }
        }

        static int SortOutputs(Output station1, Output station2)
        {
            return station1.minToStop - station2.minToStop;
        }

        static void Main(string[] args)
        {

            try
            {
                string enter;
                do
                {
                    ReadData(out List<Route> Routes, out List<Stop> Stops, out List<StopInRoute> stopsInRoutes, out int count);
                    int neededId = FindNeededId(out enter, Stops);
                    int ctim = ShowCurrentTime();
                    string stationName = "";

                    List<Output> Outputs = new List<Output>();

                    for (int i = 0; i < count; i++)
                    {
                        if (ctim > 0 && ctim < Routes[i].TimeStart)
                            ctim = 1440 + ctim;

                        int ctimLeft = ctim;
                        int ctimRight = ctim;
                        int minl = 0;
                        int minr = 0;

                        int lastCtimLeft = MinutesFormLeft(stopsInRoutes[i].IBS, Routes[i].arrayStops, neededId, ctimLeft); // время, в которое должен был выехать автобус, чтобы сейчас быть в введёной станции
                        int intervalSumLeft = ctim - lastCtimLeft; // сумма всех интервалов слева (в минутах)
                        MinutesToStop(Routes[i].TimeStart, Routes[i].TimeEnd, Routes[i].Interval, lastCtimLeft, intervalSumLeft, ctim, out minl); // !желаемое число минут, оставшихся поезду до станцции слева!

                        int lastctimRight = MinutesFromRight(stopsInRoutes[i].IBS, Routes[i].arrayStops, neededId, ctimRight); // время, в которое должен был выехать автобус, чтобы сейчас быть в введёной станции
                        int intervalSumRight = ctim - lastctimRight; // сумма всех интервалов справа (в минутах)
                        MinutesToStop(Routes[i].TimeStart, Routes[i].TimeEnd, Routes[i].Interval, lastctimRight, intervalSumRight, ctim, out minr); // !желаемое число минут, оставшихся поезду до станцции справа!

                        if (minl == -10)
                            minl = Routes[i].TimeStart + intervalSumLeft - ctim;
                        if (minr == -10)
                            minr = Routes[i].TimeStart + intervalSumRight - ctim;

                        if ((!(Routes[i].arrayStops.IndexOf(neededId) == Routes[i].arrayStops.Count - 1)) && (Routes[i].arrayStops.Contains(neededId)))
                        {
                            var OutputLeft = new Output
                            {
                                Id = Routes[i].Id,
                                destName = GetNameById(Routes[i].arrayStops[Routes[i].arrayStops.Count - 1], stationName, Stops),
                                minToStop = minl
                            };
                            Outputs.Add(OutputLeft);
                        }
                        if ((!(Routes[i].arrayStops.IndexOf(neededId) == 0)) && (Routes[i].arrayStops.Contains(neededId)))
                        {
                            var OutputRight = new Output
                            {
                                Id = Routes[i].Id,
                                destName = GetNameById(Routes[i].arrayStops[0], stationName, Stops),
                                minToStop = minr
                            };
                            Outputs.Add(OutputRight);
                        }

                    }
                    for (int p = 0; p < Outputs.Count; p++)
                    {
                        if (Outputs[p].minToStop < 0)
                        {
                            Outputs.RemoveAt(p);
                            p--;
                        }
                    }
                    Outputs.Sort(SortOutputs);

                    if (Outputs.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There are currently no trains!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    for (int i = 0; i < Outputs.Count; i++)
                    {
                        Console.WriteLine(Outputs[i].Id + ", destanation " + Outputs[i].destName + ", " + Outputs[i].minToStop + " min");
                    }
                    Console.WriteLine();

                } while (!string.IsNullOrWhiteSpace(enter));
            }
            catch
            {
                Console.WriteLine("Error reading data!");
                Console.ReadKey();
            }

        }
    }
}
