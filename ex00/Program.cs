using System;

const string ErrorMsg = "Ошибка ввода. Проверьте входные данные и повторите запрос.";
const string ReCount = "При уменьшении размера регулярного платежа сумма переплат составит: ";
const string ReDate = "При досpочном погашении сумма переплат составит: ";


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
    }
    isParsed = Double.TryParse(args[1], out rate);
    if (isParsed == false) {
        Console.WriteLine(ErrorMsg);
    }
    isParsed = Int32.TryParse(args[2], out term);
    if (isParsed == false) {
        Console.WriteLine(ErrorMsg);
    }
    isParsed = Int32.TryParse(args[3], out selectedMonth);
    if (isParsed == false) {
        Console.WriteLine(ErrorMsg);
    }
    isParsed = double.TryParse(args[4], out extraPayment);
    if (isParsed == false) {
        Console.WriteLine(ErrorMsg);
    }
    if (extraPayment > sum){
        Console.WriteLine("Внеплановый взнос не может быть больше суммы долга");
    }

    double i = rate/12/100; //Процентная ставка по займу в месяц
    double payment = (sum * i * Math.Pow((1 + i), term)) / (Math.Pow((1 + i), term) - 1);
    double fullPercents = sum * rate * term / (100 * 12);
    //double remainder;
    Console.WriteLine("i = " + i);
    int startMonth = DateTime.Now.Month;
    double reCountRes = 0;
    double reDateRes = 0;
    
    
    //Console.WriteLine("Month Payment = " + monthPayment + " Количество месяцев кредита = " + findMonths(payment, sum) + " Размер переплат = " + percents * term);
    reCountRes = showTableWithReCount();
    reDateRes =  showTableWithReDate();
    
    Console.WriteLine(ReCount + $"{reCountRes}");
    Console.WriteLine(ReDate + $"{reDateRes}");
    
    
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
}
else {
    Console.WriteLine(ErrorMsg);
}
//double findReminder(double sum, )