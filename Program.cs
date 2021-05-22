using System;

double sum = Convert.ToDouble(args[0]); //Сумма кредита, руб
double rate = Convert.ToDouble(args[1]); //Годовая процентная ставка, %
int term = Convert.ToInt32(args[2]);// Количество месяцев кредита
int selectedMonth = Convert.ToInt32(args[3]); //Номер месяца кредита, в котором вносится досрочный платеж 
double extraPayment = Convert.ToDouble(args[4]); //Сумма досрочного платежа, руб
double i = rate/12/100; //Процентная ставка по займу в месяц
double payment = (sum * i * Math.Pow((1 + i), term)) / (Math.Pow((1 + i), term) - 1);
double fullPercents = sum * rate * term / 100/12;
//double remainder;
Console.WriteLine("i = " + i);
int startMonth = DateTime.Now.Month;


//Console.WriteLine("Month Payment = " + monthPayment + " Количество месяцев кредита = " + findMonths(payment, sum) + " Размер переплат = " + percents * term);
showTable();

int findMonths(double payment, double sum) {
    return (Convert.ToInt32(Math.Ceiling(Math.Log(((payment) / (payment - i * sum)),1 + i))));
}

void showTable()
{
    double reminder = sum;
    var currPayDate = DateTime.Now;
    var prevPayDate = DateTime.Now;
    double monthPercents = 0;
    int dateDiff = 0;

    Console.WriteLine("Дата\t\tПлатеж\t\tОД\tПроценты\tОстаток\n");
    Console.WriteLine($"    \t\t{payment:f2}\t{fullPercents}\t{reminder}\n");
    for (int j = 0; j < term; j++) { 
        reminder -= payment;
        prevPayDate = currPayDate;
        currPayDate = DateTime.Now.AddMonths(j + 1); 
        dateDiff = currPayDate.Subtract(prevPayDate).Days;
        monthPercents = (reminder * rate * 30) / (100 * 365); 
        Console.WriteLine(currPayDate.ToString("d") + "\t" + "{0:f2}" + "\t" + "{1:f2}" + "\t" + "{2:f2}" + "\t" + dateDiff + "\n", payment, monthPercents, reminder);
    }
}

//double findReminder(double sum, )