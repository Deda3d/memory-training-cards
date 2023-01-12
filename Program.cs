int LengthField = 0; // довжина та ширина поля, переед тип, як ми впишемо - вона = 0
while (LengthField != 4 && LengthField != 8 && LengthField != 6) // цикл не завершиться до тих пір, поки ми не введемо правильне значення: 4,6 або 8
{
    Console.Clear(); // кожного разу стираємо консоль, щоб текст не дублювався 
    Console.Write("Введiть значення довжини та ширини поля (4,6 або 8): ");
    LengthField = Convert.ToInt32(Console.ReadLine()); // зчитуємо значення
}
Console.Clear(); // ще раз чистимо консоль, щоб пропав текст "Введіть значення..."

Console.CursorVisible = false; // вимикаємо курсор, щоб його не було видно
int[,] Field = new int[LengthField + 2, LengthField + 2];  // робимо ігрове поле. воно більше, ніж те, що ми вписали на 2 тому, що ще будуть стіни
bool[,] CardInfo = new bool[LengthField + 2, LengthField + 2]; // массив булів для того, щоб стежити за станом карти ( відкрита чи закрита )
int NCards = LengthField * LengthField / 2;  // кількість різних карт у полі
Random rnd = new Random();
int c = 0; // просто лічильник
bool game = false; // запам'ятовуємо, що гра ще не почалась

int time; // змінна, у якій зберігається інформація про те, скільки в нас часу є на розв'язок задачі
int starttime; // змінна, у якій зберігається інформація про те, скільки в нас часу є для того, щоб запам'ятати картки
if (LengthField == 4) // при різних значеннях складності поля буде даватися різний час
{
    time = 30;
    starttime = 15;
}
else if (LengthField == 6)
{
    time = 60;
    starttime = 30;
}
else if (LengthField == 8)
{
    time = 90;
    starttime = 45;
}
else
{
    time = 0;
    starttime = 0;
}

int timebeforeclosecard = 1000;  // час, через який відкриті карти будуть закриватися, якщо ми не вгадали
int personx = 1;  // координати ігрока. спочатку ігрок стоїть у верхньому лівому куту
int persony = 1;

for (int i = 0; i < LengthField + 2; i++) Field[i, 0] = -1;  // заповнюємо по краям стінками. стінка - це (-1)
for (int i = 0; i < LengthField + 2; i++) Field[0, i] = -1;
for (int i = 0; i < LengthField + 2; i++) Field[i, LengthField + 1] = -1;
for (int i = 0; i < LengthField + 2; i++) Field[LengthField + 1, i] = -1;

int cardsput = 0; // кількість карт, які сгенерувалися при створенні лабіринту
while (cardsput != LengthField * LengthField)  // цикл буде виконуватися до тих пір, поки все поле не буде в картках
{
    for (int i = 1; i < LengthField + 1; i++)  // проходимо по полю
    {
        for (int j = 1; j < LengthField + 1; j++)
        {
            int card = rnd.Next(1, NCards+1); // беремо випадкову картку
            c = 0;
            foreach (int x in Field) if (x == card) c++; // рахуємо, скільки таких карт вже є у полі
            if (c < 2 && Field[i, j] == 0) // якщо поле пусте, та таких карт було не більше однієї
            {
                Field[i, j] = card; // записуємо цю картку в поле
                cardsput++; // кількість розміщених карт стає більшою
            }
            c = 0;
        }
    }
}
void WriteCard(int ii, int jj) // метод для малювання комірки в полі, у який подаються її координати
{
    if (CardInfo[ii, jj] == true || game == false) // якщо карта відкрита АБО гра ще не почалась ( для того, щоб ми бачили всі відкриті, поки запам'ятовуємо )
    {
        if (Field[ii, jj] % 10 == 1) Console.ForegroundColor = ConsoleColor.Red; // змінюємо колір в залежності від одиничної частини числа
        if (Field[ii, jj] % 10 == 2) Console.ForegroundColor = ConsoleColor.Green;
        if (Field[ii, jj] % 10 == 3) Console.ForegroundColor = ConsoleColor.Blue;
        if (Field[ii, jj] % 10 == 4) Console.ForegroundColor = ConsoleColor.Yellow;
        if (Field[ii, jj] % 10 == 5) Console.ForegroundColor = ConsoleColor.White;
        if (Field[ii, jj] % 10 == 6) Console.ForegroundColor = ConsoleColor.Cyan;
        if (Field[ii, jj] % 10 == 7) Console.ForegroundColor = ConsoleColor.DarkGray;
        if (Field[ii, jj] % 10 == 8) Console.ForegroundColor = ConsoleColor.Magenta;
        if (Field[ii, jj] % 10 == 9) Console.ForegroundColor = ConsoleColor.DarkBlue;
        if (Field[ii, jj] % 10 == 0) Console.ForegroundColor = ConsoleColor.DarkYellow;
        if (Field[ii, jj] / 10 == 0) Console.Write(Convert.ToChar(3)); // виводимо символ в залежності від десяткової частини числа
        if (Field[ii, jj] / 10 == 1) Console.Write(Convert.ToChar(4));
        if (Field[ii, jj] / 10 == 2) Console.Write(Convert.ToChar(5));
        if (Field[ii, jj] / 10 == 3) Console.Write(Convert.ToChar(6));
    }
    else // якщо комірка повинна бути закрита
    {
        Console.ForegroundColor = ConsoleColor.Gray;  // малюємо замість картки сірий прямокутник
        Console.Write('█');
    }
}

Console.WriteLine("Натисніть будь-яку кнопку, щоб почати гру");
Console.ReadKey(); // чекаємо, доки ігрок не буде готовий, щоб почати гру. після того, як рідкей зловить якесь натискання - код буде йти далі
Console.Clear();

void PrintField()  // метод для того, щоб намалювати поле
{
    for (int i = 0; i < LengthField + 2; i++)
    {
        for (int j = 0; j < LengthField + 2; j++)
        {
            if (Field[i, j] == -1) // якщо стіна (-1) - виводимо стіну
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("█");
            }
            else WriteCard(i, j); // інакше - малюємо карту, або якщо гра почалась та ми не відкрили ще цю карту - малюється сірий прямокутник
        }
        Console.WriteLine();
    }
}
PrintField();  // малюємо поле, щоб гравець під час відліку міг дивитися на нього

Console.WriteLine();
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("   секунд залишилося, щоб запам'ятати картки"); // пишемо під полем інформацію про стан часу. замість пропуску далі буде таймер
for (int i = starttime; i>=0; i--)  // виконуємо цикл стільки разів, скільки в нас дається часу для того, щоб запам'ятати поле
{
    Console.SetCursorPosition(0, LengthField + 3); // ставимо курсор поруч з текстом, який ми виводили у 119 рядку
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write($"{i} ");  // пишемо, скільки секунд залишилось
    Thread.Sleep(1000); // виконання коду зупиняється на 1 секунду
}


void TimeLeft() // метод для того, щоб коли гра почалась, ми змогли писати скільки в нас залишилося часу
{
    while (game!=false)  // цикл, який буде виконуватися нескінченно до тих пір, поки гра активна ( гравець ще не виграв та час ще не вийшов )
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(3, LengthField + 3);
        Console.WriteLine("секунд залишилося до кінця гри");
        for (int t = time; t >= 0; t--)  // виконуємо цикл стільки разів, скільки в нас дається часу для того, щоб пройти поле
        {
            Console.CursorVisible = false;  // вимикаємо курсор
            Console.SetCursorPosition(0, LengthField + 3);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{t} "); // виводить скільки залишилося секунд
            Console.CursorVisible = true;
            Console.SetCursorPosition(personx, persony); // вертаємо курсор на позицію гравця
            Thread.Sleep(1000);
            if (t == 0) // якщо час вийшов
            {
                game = false; // зупиняємо гру
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ви програли!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}

void CloseOpened(int xop, int yop, int xop2, int yop2) // метод, який приймає координати двох комірок та закриває їх, якщо гравець не вгадав
{

    CardInfo[yop, xop] = false; // закриваємо ці комірки
    CardInfo[yop2, xop2] = false;
    Thread.Sleep(timebeforeclosecard); // затримка, щоб гравець не зміг дуже швидко всі комірки відкривати
    Console.CursorVisible = false;
    Console.SetCursorPosition(xop, yop);  // ставимо курсор в першу комірку
    WriteCard(yop, xop);  // оновлюємо ії, тільки після цієї команди вона візуально зміниться
    Console.SetCursorPosition(xop2, yop2);// ставимо курсор в другу комірку
    WriteCard(yop2, xop2);
    Console.CursorVisible = false;
    Console.SetCursorPosition(personx,persony); // вертаємо курсор на позицію гравця
}

game = true; // починаємо гру, відлік закінчився
Console.Clear();
PrintField(); // малюємо нове поле. теперь воно буде закритим, бо гра почалася 
Task.Run(() => TimeLeft()); // запускаєио асинхронне виконання метода з таймером, щоб він не тормозив код

while (game == true) // цикл виконується, поки гра не завершена 
{
    Thread.Sleep(10); // маленька затримка для того, щоб таймери не збігалися, бо через це гра може запрацювати некоректно
    var x = Console.ReadKey(true).Key; // зчитуємо кнопку, яку ми натиснули
    switch (x)
    {
        case ConsoleKey.RightArrow: if(Field[persony,personx+1]!=-1) personx++; Console.SetCursorPosition(personx, persony); break;  // якщо це стрілка вправо, так справа немає стінки
        case ConsoleKey.LeftArrow: if (Field[persony, personx - 1] != -1) personx--; Console.SetCursorPosition(personx, persony); break;
        case ConsoleKey.UpArrow: if (Field[persony-1, personx] != -1) persony--; Console.SetCursorPosition(personx, persony); break;
        case ConsoleKey.DownArrow: if (Field[persony + 1, personx] != -1) persony++; Console.SetCursorPosition(personx, persony); break;
        case ConsoleKey.Spacebar:  // пробіл - відкриття комірки
            if (CardInfo[persony, personx] == false)  // якщо ми не намагаємося відкрити відкриту комірку
            {
                int xopen = personx; // запам'ятовуємо координати цієї відкритої комірки, щоб далі її порівняти з іншою
                int yopen = persony;
                CardInfo[persony, personx] = true; // відкриваємо комірку
                WriteCard(persony, personx); // оновлюємо її, щоб побачити це візуально
                Thread.Sleep(100);  // маленька затримка для коректної роботи програми
                bool second = false;  // статус другої комірки ( натиснута чи не натиснута )
                while (second == false)  // виконуємо цикл до тих пір, поки друга карта не відкрита ( це потрібно для того, щоб можна було спокійно переміщатися по полю в цей час )
                {
                    var xx = Console.ReadKey(true).Key; // зчитуємо друге натискання
                    switch (xx)
                    {
                        case ConsoleKey.RightArrow: if (Field[persony, personx + 1] != -1) personx++; Console.SetCursorPosition(personx, persony); break; // даємо змогу переміщатися
                        case ConsoleKey.LeftArrow: if (Field[persony, personx - 1] != -1) personx--; Console.SetCursorPosition(personx, persony); break;
                        case ConsoleKey.UpArrow: if (Field[persony - 1, personx] != -1) persony--; Console.SetCursorPosition(personx, persony); break;
                        case ConsoleKey.DownArrow: if (Field[persony + 1, personx] != -1) persony++; Console.SetCursorPosition(personx, persony); break;
                        case ConsoleKey.Spacebar:  // друге натискання пробілу
                            if (CardInfo[persony, personx] == false) // перевіряємо, чи не відкрита вже друга карта
                            {
                                CardInfo[persony, personx] = true; // відкриваємо її
                                WriteCard(persony, personx); // оновлюємо візуально
                                Thread.Sleep(100);
                                if (Field[persony, personx] != Field[yopen, xopen]) CloseOpened(xopen, yopen, personx, persony); // якщо дві карти не совпали - виконуємо метод із закриття ціх комірок.
                                second = true; // запам'ятовуємо, що ми відкрили другу карту
                            }
                            break;
                    }
                }
            }
            break;
    }
    c = 0;
    for(int i = 1;i< LengthField + 1; i++)  // рахуємо кількість відкритих карт
    {
        for (int j = 1; j < LengthField + 1; j++) if (CardInfo[i, j] == true) c++;
    }
    if (c == LengthField * LengthField) // якщо всі карти відкриті - ми виграли
    {
        game = false; // зупиняємо гру
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Ви виграли!");
        Console.ForegroundColor = ConsoleColor.White;
    }
}