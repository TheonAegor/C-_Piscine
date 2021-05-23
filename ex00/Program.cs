using System;

namespace ex00
{
    class Programs {
        const string ErrorMsg = "Ошибка ввода. Проверьте входные данные и повторите запрос.";
        const string ReCount = "Переплата при уменьшении платежа: ";
        const string ReDate = "Переплата при уменьшении срока: ";

        static double reDate(int term, double sum, double rate, double extraPayment, int selectedMonth) 
        {
            double res = 0;
            double monthPercents = 0;
            double reminder = sum;
            double i = rate/12/100;
            double monthPayment = (sum * i * Math.Pow((1 + i), term)) / (Math.Pow((1 + i), term) - 1);
            DateTime thisPayDay = new DateTime ( DateTime.Today.Year,
                                            DateTime.Today.Month,
                                            1).AddMonths(1);
            DateTime prevPayDay = thisPayDay.AddMonths(-1);
            int dateDiff = 0;

            for (int j = 1; j <= term; j++)
            {
                dateDiff = (thisPayDay - prevPayDay).Days;
                monthPercents = (reminder * rate * dateDiff) / (100 * (DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365));
                if (monthPercents < 0)
                    monthPercents = 0;
                res += monthPercents;
                reminder -= monthPayment - monthPercents; 
                thisPayDay = thisPayDay.AddMonths(1);
                prevPayDay = prevPayDay.AddMonths(1);
                if (j == selectedMonth) {
                    reminder -= extraPayment;
                }
            }
            return (res);
        }
        static double reCount(int term, double sum, double rate, double extraPayment, int selectedMonth) 
        {
            double res = 0;
            double monthPercents = 0;
            double reminder = sum;
            double i = rate/12/100;
            double monthPayment = (sum * i * Math.Pow((1 + i), term)) / (Math.Pow((1 + i), term) - 1);
            DateTime thisPayDay = new DateTime ( DateTime.Today.Year,
                                            DateTime.Today.Month,
                                            1).AddMonths(1);
            DateTime prevPayDay = thisPayDay.AddMonths(-1);
            int dateDiff = 0;

            for (int j = 1; j <= term; j++)
            {
                dateDiff = (thisPayDay - prevPayDay).Days;
                monthPercents = (reminder * rate * dateDiff) / (100 * (DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365));
                res += monthPercents;
                reminder -= monthPayment - monthPercents; 
                thisPayDay = thisPayDay.AddMonths(1);
                prevPayDay = prevPayDay.AddMonths(1);
                if (j == selectedMonth) {
                    reminder -= extraPayment;
                    monthPayment = (reminder * i * Math.Pow((1 + i), term - j)) / (Math.Pow((1 + i), term - j) - 1);
                }
            }
            return (res);
        }
        static int Main(string[] args) {
            double sum = 0; //Сумма кредита, руб
            double rate = 0; //Годовая процентная ставка, %
            int term = 0;// Количество месяцев кредита
            int selectedMonth = 0; //Номер месяца кредита, в котором вносится досрочный платеж 
            double extraPayment = 0; //Сумма досрочного платежа, руб
            int lengthArr = args.GetLength(0);

            if (lengthArr == 5)
            {
                var isParsed = Double.TryParse(args[0], out sum);
                if (isParsed == false) {
                    Console.WriteLine(ErrorMsg);
                    return(0);
                }
                isParsed = Double.TryParse(args[1], out rate);
                if (isParsed == false) {
                    Console.WriteLine(ErrorMsg);
                    return(0);
                }
                isParsed = Int32.TryParse(args[2], out term);
                if (isParsed == false) {
                    Console.WriteLine(ErrorMsg);
                    return(0);
                }
                isParsed = Int32.TryParse(args[3], out selectedMonth);
                if (isParsed == false) {
                    Console.WriteLine(ErrorMsg);
                    return(0);
                }
                isParsed = double.TryParse(args[4], out extraPayment);
                if (isParsed == false) {
                    Console.WriteLine(ErrorMsg);
                    return(0);
                }
                if (extraPayment > sum){
                    Console.WriteLine("Внеплановый взнос не может быть больше суммы долга");
                    return(0);
                }
                double reCountRes = reCount(term, sum, rate, extraPayment, selectedMonth);
                double reDateRes = reDate(term, sum, rate, extraPayment, selectedMonth);

                Console.WriteLine(ReCount + $"{reCountRes}");
                Console.WriteLine(ReDate + $"{reDateRes}");
                if (reCountRes > reDateRes){
                    Console.WriteLine($"Уменьшение срока выгоднее уменьшения платежа на {reCountRes - reDateRes:f2}!");
                }
                else if (reCountRes < reDateRes) {
                    Console.WriteLine($"Уменьшение платежа выгоднее уменьшения срока на {reDateRes - reCountRes:f2}!");
                }
            }
            else {
                Console.WriteLine(ErrorMsg);
            }
                
                return (1);
        }
                //Console.WriteLine("Month Payment = " + monthPayment + " Количество месяцев кредита = " + findMonths(payment, sum) + " Размер переплат = " + percents * term);
                /*
                reCountRes = showTableWithReCount();
                reDateRes =  showTableWithReDate();



                double showTableWithReCount()
                {
                    double reminder = sum;
                    var currPayDate = DateTime.Now;
                    var prevPayDate = DateTime.Now;
                    double monthPercents = 0;
                    int dateDiff = 0;
                    double res = 0;
                    double payment = (sum * i * Math.Pow((1 + i), term)) / (Math.Pow((1 + i), term) - 1);

                    Console.WriteLine("Дата\t\tПлатеж\t\tОД\t\tПроценты\tОстаток\n");
                    Console.WriteLine($"    \t\t{payment:f2}\t{payment - monthPercents:f2}\t{fullPercents}\t{reminder}\n");
                    for (int j = 1; j <= term && reminder > 0; j++) { 
                        reminder -= payment - monthPercents;
                        prevPayDate = currPayDate;
                        currPayDate = DateTime.Now.AddMonths(j); 
                        dateDiff = currPayDate.Subtract(prevPayDate).Days;
                        monthPercents = (reminder * rate * dateDiff) / (100 * (DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365));
                        if (j == selectedMonth) {
                            reminder -= extraPayment;
                            payment = (reminder * i * Math.Pow((1 + i), term - j)) / (Math.Pow((1 + i), term - j) - 1);
                        }
                        res += monthPercents;
                        reminder = reminder < 0 ? 0 : reminder;
                        monthPercents = monthPercents < 0 ? 0 : monthPercents;
                        Console.WriteLine(currPayDate.ToString("d") + "\t" + "{0:f2}" + "\t" + "{3:f2}" + "\t" + "{1:f2}" + "\t" + "{2:f2}" + "\t" + dateDiff + "\n", payment, monthPercents, reminder, payment - monthPercents);
                    }
                    Console.WriteLine($"{res}");
                    return (res);
                }

                double showTableWithReDate()
                {
                    double reminder = sum;
                    var currPayDate = DateTime.Now;
                    var prevPayDate = DateTime.Now;
                    double monthPercents = 0;
                    int dateDiff = 0;
                    double res = 0;

                    Console.WriteLine("Дата\t\tПлатеж\t\tОД\t\tПроценты\tОстаток\n");
                    Console.WriteLine($"    \t\t{payment:f2}\t{payment - monthPercents:f2}\t{fullPercents}\t{reminder}\n");
                    for (int j = 1; j <= term && reminder > 0; j++) { 
                        reminder -= payment - monthPercents;
                        prevPayDate = currPayDate;
                        currPayDate = DateTime.Now.AddMonths(j); 
                        dateDiff = currPayDate.Subtract(prevPayDate).Days;
                        monthPercents = (reminder * rate * dateDiff) / (100 * (DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365));
                        if (j == selectedMonth) {
                            reminder -= extraPayment;
                        }
                        res += monthPercents;
                        reminder = reminder < 0 ? 0 : reminder;
                        monthPercents = monthPercents < 0 ? 0 : monthPercents;
                        Console.WriteLine(currPayDate.ToString("d") + "\t" + "{0:f2}" + "\t" + "{3:f2}" + "\t" + "{1:f2}" + "\t" + "{2:f2}" + "\t" + dateDiff + "\n", payment, monthPercents, reminder, payment - monthPercents);
                    }
                    Console.WriteLine($"{res}");
                    return (res);
                }
                    */
                    /*
            }
            return (1);
            */

        
    }
}



//double findReminder(double sum, )