using System;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace calendario_console
{
    internal class Program
    {
        static CultureInfo ci = new CultureInfo("es");

        static string strFormat = "MMMM", strFormat_2 = "dddd";
        static string[] festivos;
        static int[] days_number;

        static void Main(string[] args)
        {
            LoadParameters();
            Calendar();
        }

        static void LoadParameters()
        {
            //Tomar el dia de la semana
            days_number = new int[] { 7, 1, 2, 3, 4, 5, 6 };

            //agregamos todos los dias festivos
            festivos = new string[]
            {
                "24/01/2023",
                "01/01/2023",
                "30/12/2023",
                "30/11/2023",
                "14/09/2023",
                "15/04/2023"
            };
        }

        static void Calendar()
        {
            var hoy = DateTime.Now;
            var inicio_año = new DateTime(hoy.Year, 1, 1);

            for (var i = 1; i <= 12; i++)
            {

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{inicio_año.ToString(strFormat, ci).ToUpper()} - - - - - - - - - - - - -  - - - - - - - - - - - - - - - - - - - - {inicio_año.AddMonths(1).ToString(strFormat, ci).ToUpper()}");
                Console.ForegroundColor = ConsoleColor.Gray;


                var start_date = new DateTime(inicio_año.Year, inicio_año.Month, 1);
                var end_date = start_date.AddMonths(2).AddSeconds(-1);

                print(start_date, end_date);

                inicio_año = inicio_año.AddMonths(2);
            }

            Console.ReadLine();
        }

        static void print(DateTime month_1, DateTime month_2)
        {
            var max_date = month_1 > month_2 ? month_1 : month_2;
            var min_date = month_1 < month_2 ? month_1 : month_2;
            month_2 = new DateTime(month_1.Year, month_1.AddMonths(1).Month, 1);
            while (min_date <= max_date)
            {
                if (min_date.Month == month_1.Month)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    var ttt = min_date.ToString("dd/MM/yyyy");
                    var results = Array.FindAll(festivos, s => s.Equals(ttt)).FirstOrDefault();
                    if (results != null)
                        Console.ForegroundColor = ConsoleColor.Blue;

                    var dn = days_number[(int)min_date.DayOfWeek];
                    if (dn == 7)
                        Console.ForegroundColor = ConsoleColor.Red;

                    var str = min_date.ToString(strFormat_2, ci);
                    var dia_semana = char.ToUpper(str[0]) + str.Substring(1);

                    Console.Write($"Dia: {month_1.Day} - {dia_semana} mes: {month_1.Month} - dia semana: {dn}");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"---------------------");
                if (max_date.Month == month_2.Month)
                {
                    var ttt = month_2.ToString("dd/MM/yyyy");
                    var results = Array.FindAll(festivos, s => s.Equals(ttt)).FirstOrDefault();
                    if (results != null)
                        Console.ForegroundColor = ConsoleColor.Blue;

                    var dn = days_number[(int)month_2.DayOfWeek];
                    if (dn == 7)
                        Console.ForegroundColor = ConsoleColor.Red;

                    var str = month_2.ToString(strFormat_2, ci);
                    var dia_semana = char.ToUpper(str[0]) + str.Substring(1);

                    Console.Write($"Dia: {month_2.Day} - {dia_semana} mes: {month_2.Month} - dia semana: {dn}");
                }
                Console.Write("\n");
                min_date = min_date.AddDays(1);
                month_1 = month_1.AddDays(1);
                month_2 = month_2.AddDays(1);
                if (min_date.Month == max_date.Month)
                    break;
            }
        }
    }
}
