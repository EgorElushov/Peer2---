using System;
using System.IO;

namespace peer_2

{
    /// <summary>
    ///     Класс матрицы, содержащий основные операции над ними.
    /// </summary>
    internal class Matrix
    {
        private double[][] _matrix;

        /// <summary>
        ///     Конструктор матрицы по зубчатому массиву элементов.
        /// </summary>
        /// <param name="matrix">Массив элементов матрицы.</param>
        public Matrix(double[][] matrix)
        {
            _matrix = matrix;
        }

        /// <summary>
        ///     Индексатор, используемый для доступа к элементам матрицы.
        /// </summary>
        /// <param name="i">Строки матрицы.</param>
        /// <param name="j">Столбцы матрицы.</param>
        /// <returns></returns>
        public double this[int i, int j]
        {
            get => _matrix[i][j];
            set => _matrix[i][j] = value;
        }

        /// <summary>
        ///     Получение количества строк матрицы.
        /// </summary>
        /// <returns>Количество строк матрицы.</returns>
        public int GetLength()
        {
            return _matrix.Length;
        }

        /// <summary>
        ///     Получение количества столбцов матрицы.
        /// </summary>
        /// <returns>Количество столбцов матрицы.</returns>
        public int GetWidth()
        {
            return _matrix[0].Length;
        }

        /// <summary>
        ///     Метод класса, вычисляющий след матрицы.
        /// </summary>
        /// <returns>След заданной матрицы.</returns>
        public double Trace()
        {
            double trace = 0;
            for (var i = 0; i < _matrix.Length; i++)
                trace += _matrix[i][i];
            return trace;
        }

        /// <summary>
        ///     Метод транспонирования матрицы, меняет исходный массив.
        /// </summary>
        public void Transpose()
        {
            var auxiliaryArray = new double[_matrix[0].Length][];
            for (var i = 0; i < _matrix[0].Length; i++)
                auxiliaryArray[i] = new double[_matrix.Length];
            for (var i = 0; i < _matrix.Length; i++)
                for (var j = 0; j < _matrix[0].Length; j++)
                    auxiliaryArray[j][i] = _matrix[i][j];
            _matrix = auxiliaryArray;
        }

        /// <summary>
        ///     Метод сложения двух матриц.
        /// </summary>
        /// <param name="firstAddend">Первое слагаемое.</param>
        /// <param name="secondAddend">Второе слагаемое.</param>
        /// <returns>Сумма 2ух матриц.</returns>
        public static Matrix operator +(Matrix firstAddend, Matrix secondAddend)
        {
            var summation = new double[firstAddend._matrix.Length][];
            for (var i = 0; i < firstAddend._matrix.Length; i++)
                summation[i] = new double[firstAddend._matrix[0].Length];
            for (var i = 0; i < firstAddend._matrix.Length; i++)
                for (var j = 0; j < firstAddend._matrix[0].Length; j++)
                    summation[i][j] = firstAddend._matrix[i][j] + secondAddend._matrix[i][j];
            return new Matrix(summation);
        }

        /// <summary>
        ///     Метод вычисления разности 2 матриц.
        /// </summary>
        /// <param name="minuend">Уменьшаемое.</param>
        /// <param name="subtrahend">Вычитаемое.</param>
        /// <returns>Разность 2ух матриц.</returns>
        public static Matrix operator -(Matrix minuend, Matrix subtrahend)
        {
            var subtraction = new double[minuend._matrix.Length][];
            for (var i = 0; i < minuend._matrix.Length; i++)
                subtraction[i] = new double[minuend._matrix[0].Length];
            for (var i = 0; i < minuend._matrix.Length; i++)
                for (var j = 0; j < minuend._matrix[0].Length; j++)
                    subtraction[i][j] = minuend._matrix[i][j] - subtrahend._matrix[i][j];
            return new Matrix(subtraction);
        }

        /// <summary>
        ///     Метод перемножения 2ух матриц.
        /// </summary>
        /// <param name="firstMultiplier">Первый множитель.</param>
        /// <param name="secondMultiplier">Второй множитель.</param>
        /// <returns>Произведение 2ух матриц.</returns>
        public static Matrix operator *(Matrix firstMultiplier, Matrix secondMultiplier)
        {
            var answer = new double[firstMultiplier._matrix.Length][];
            for (var i = 0; i < firstMultiplier._matrix.Length; i++)
                answer[i] = new double[secondMultiplier._matrix[0].Length];
            for (var i = 0; i < firstMultiplier._matrix.Length; i++)
                for (var j = 0; j < secondMultiplier._matrix[0].Length; j++)
                    for (var k = 0; k < firstMultiplier._matrix[0].Length; k++)
                        answer[i][j] += firstMultiplier._matrix[i][k] * secondMultiplier._matrix[k][j];
            return new Matrix(answer);
        }

        /// <summary>
        ///     Метод умножения матрицы на число.
        /// </summary>
        /// <param name="firstMultiplier">Первый множитель - матрица.</param>
        /// <param name="secondMultiplier">Второй множитель - число.</param>
        /// <returns>Произведение матрицы на число.</returns>
        public static Matrix operator *(Matrix firstMultiplier, double secondMultiplier)
        {
            var product = new double[firstMultiplier._matrix.Length][];
            for (var i = 0; i < firstMultiplier._matrix.Length; i++)
                product[i] = new double[firstMultiplier._matrix[0].Length];
            for (var i = 0; i < firstMultiplier._matrix.Length; i++)
                for (var j = 0; j < firstMultiplier._matrix[0].Length; j++)
                    product[i][j] = firstMultiplier._matrix[i][j] * secondMultiplier;
            return new Matrix(product);
        }

        /// <summary>
        ///     Метод приведения класса Matrix к строке.
        /// </summary>
        /// <returns>Строковое представление матрицы.</returns>
        public override string ToString()
        {
            var answer = "";
            foreach (var lines in _matrix)
            {
                foreach (var elements in lines)
                    answer += elements.ToString("F3") + " ";

                answer += "\n";
            }

            return answer;
        }
    }

    /// <summary>
    ///     Основной класс программы.
    /// </summary>
    internal class Program
    {
        /// <summary>
        ///     Метод считывания СЛАУ.
        /// </summary>
        /// <param name="firstMatrix">Значения коэффициентов уравнения.</param>
        /// <param name="secondMatrix">Значения коэффициентов после знака равенства.</param>
        public static void EquationInput(out Matrix firstMatrix, out Matrix secondMatrix)
        {
            double[][] auxiliaryArray;
            Console.WriteLine("Введите количество уравнений: ");
            int equationCount;
            while (!int.TryParse(Console.ReadLine(), out equationCount)
                   || equationCount < 1)
                Console.WriteLine("Неверный ввод, попробуй еще раз");
            Console.WriteLine("Введите коэффициенты уравнений: (до знака равенства)");
            SelectMatrixInput(equationCount, equationCount, out auxiliaryArray);
            firstMatrix = new Matrix(auxiliaryArray);
            Console.WriteLine("Ваша матрица: \n" + firstMatrix);
            Console.WriteLine("Введите коэффициенты после знака равенства:");
            SelectMatrixInput(equationCount, 1, out auxiliaryArray);
            secondMatrix = new Matrix(auxiliaryArray);
            Console.WriteLine("Ваша матрица: \n" + secondMatrix);
        }

        /// <summary>
        ///     Метод решения СЛАУ методом Крамера.
        /// </summary>
        /// <param name="firstMatrix">Значения коэффициентов уравнения.</param>
        /// <param name="secondMatrix">Значения коэффициентов после знака равенства.</param>
        /// <returns>Массив корней уравнения.</returns>
        public static double[] EquationSolve(Matrix firstMatrix, Matrix secondMatrix)
        {
            var roots = new double[firstMatrix.GetLength()];
            var matrixDeterminant = Determinant(firstMatrix);
            if (matrixDeterminant == 0)
                return null;
            for (var i = 0; i < firstMatrix.GetLength(); i++)
            {
                var saveIndex = new double[firstMatrix.GetLength()];
                for (var j = 0; j < firstMatrix.GetLength(); j++)
                {
                    saveIndex[j] = firstMatrix[j, i];
                    firstMatrix[j, i] = secondMatrix[j, 0];
                }

                roots[i] = Determinant(firstMatrix) / matrixDeterminant;
                for (var j = 0; j < firstMatrix.GetLength(); j++) firstMatrix[j, i] = saveIndex[j];
            }

            return roots;
        }

        /// <summary>
        ///     Метод вычисления определителя квадратной матрицы.
        /// </summary>
        /// <param name="matrix">Матрица.</param>
        /// <returns>Определитель.</returns>
        public static double Determinant(Matrix matrix)
        {
            double determinant = 0;
            var sign = 1;
            if (matrix.GetLength() == 1)
                return matrix[0, 0];

            var copy = new double[matrix.GetLength() - 1][];
            for (var i = 0; i < matrix.GetLength() - 1; i++)
                copy[i] = new double[matrix.GetLength() - 1];

            for (var i = 0; i < matrix.GetWidth(); i++)
            {
                for (var j = 0; j < matrix.GetLength() - 1; j++)
                {
                    for (var k = 0; k < i; k++)
                        copy[j][k] = matrix[j + 1, k];
                    for (var k = i; k < matrix.GetLength() - 1; k++)
                        copy[j][k] = matrix[j + 1, k + 1];
                }

                determinant += sign * Determinant(new Matrix(copy)) * matrix[0, i];
                sign = -sign;
            }

            return determinant;
        }

        /// <summary>
        ///     Метод ввода матрицы из консоли.
        /// </summary>
        /// <param name="length">Количество строк матрицы.</param>
        /// <param name="width">Количество столбцов матрицы.</param>
        /// <param name="auxiliaryArray">Вспомогательный массив для хранения элементов матрицы.</param>
        public static void ConsoleMatrixInput(int length, int width, out double[][] auxiliaryArray)
        {
            auxiliaryArray = new double[length][];
            for (var i = 0; i < length; i++)
                auxiliaryArray[i] = new double[width];
            for (var i = 0; i < length; i++)
            {
                var inputString = new string[width];
                var correctInputFlag = true;
                while (correctInputFlag)
                {
                    while ((inputString = Console.ReadLine().Split()).Length != width)
                        Console.WriteLine($@"Длина твоей строки должна быть {width}.");
                    var k = 0;
                    foreach (var el in inputString)
                    {
                        double checkCorrection;
                        if (!double.TryParse(el, out checkCorrection))
                        {
                            Console.WriteLine($@"Элементы должны быть double, а ты ввёл '{el}'.");
                            break;
                        }

                        k++;
                    }

                    if (k == width)
                        correctInputFlag = false;
                }

                for (var j = 0; j < width; j++)
                    auxiliaryArray[i][j] = double.Parse(inputString[j]);
            }
        }

        /// <summary>
        ///     Метод случайной генерации матрицы.
        /// </summary>
        /// <param name="length">Количество строк матрицы.</param>
        /// <param name="width">Количество столбцов матрицы.</param>
        /// <param name="auxiliaryArray">Вспомогательный массив для хранения элементов матрицы.</param>
        public static void RandomMatrixInput(int length, int width, out double[][] auxiliaryArray)
        {
            var random = new Random();
            auxiliaryArray = new double[length][];
            for (var i = 0; i < length; i++)
                auxiliaryArray[i] = new double[width];
            for (var i = 0; i < length; i++)
                for (var j = 0; j < width; j++)
                    auxiliaryArray[i][j] = random.Next(-20, 21) + random.NextDouble();
        }

        /// <summary>
        ///     Метод считывания названия файла.
        /// </summary>
        /// <returns>Строка - название файла</returns>
        public static string FileNameInput()
        {
            var fileName = Console.ReadLine();
            while (!File.Exists(fileName))
            {
                Console.WriteLine("Указанный файл не существует, попробуй еще раз");
                fileName = Console.ReadLine();
            }

            return fileName;
        }

        /// <summary>
        ///     Считывание матрицы из файла.
        /// </summary>
        /// <param name="length">Количество строк матрицы.</param>
        /// <param name="width">Количество столбцов матрицы.</param>
        /// <param name="auxiliaryArray">Вспомогательный массив для хранения элементов матрицы.</param>
        /// <param name="fileName">Название файла для считывания.</param>
        public static void FileMatrixInput(int length, int width, out double[][] auxiliaryArray, string fileName)
        {
            auxiliaryArray = new double[length][];
            for (var i = 0; i < length; i++)
                auxiliaryArray[i] = new double[width];

            var inputStrings = File.ReadAllLines(fileName);
            while (inputStrings.Length != length)
            {
                Console.WriteLine("В файле не хватает строк, попробуй ввести другой");
                fileName = FileNameInput();
                inputStrings = File.ReadAllLines(fileName);
            }

            var correctionFlag = false;
            do
            {
                for (var i = 0; i < length; i++)
                {
                    Console.WriteLine(inputStrings[i]);
                    var correctInputFlag = true;
                    while (correctInputFlag)
                    {
                        if (inputStrings[i].Split().Length != width)
                        {
                            Console.WriteLine($@"Длина твоей строки в файле должна быть {width}.");
                            correctionFlag = true;
                            break;
                        }

                        var k = 0;
                        foreach (var el in inputStrings[i].Split())
                        {
                            double checkCorrection;
                            if (!double.TryParse(el, out checkCorrection))
                            {
                                Console.WriteLine($@"Элементы должны быть double, а в файле '{el}'.");
                                correctionFlag = true;
                                break;
                            }

                            k++;
                        }

                        if (k == width)
                            correctInputFlag = false;
                    }

                    for (var j = 0; j < width; j++)
                        auxiliaryArray[i][j] = double.Parse(inputStrings[i].Split()[j]);
                }

                if (correctionFlag)
                {
                    Console.WriteLine("При вводе из файла произошла ошибка, попробуй ввести другой.");
                    fileName = FileNameInput();
                    inputStrings = File.ReadAllLines(fileName);
                }
            } while (correctionFlag);
        }

        /// <summary>
        ///     Метод выбора способа ввода матрицы.
        /// </summary>
        /// <param name="length">Количество строк матрицы.</param>
        /// <param name="width">Количество столбцов матрицы.</param>
        /// <param name="auxiliaryArray">Вспомогательный массив для хранения элементов матрицы.</param>
        public static void SelectMatrixInput(int length, int width, out double[][] auxiliaryArray)
        {
            Console.WriteLine("Выбери способ ввода матрицы: (также цифрой)");
            Console.WriteLine("1. Ввод из консоли");
            Console.WriteLine("2. Случайное задание");
            Console.WriteLine("3. Ввод из файла");
            int selectedAction;
            while (!int.TryParse(Console.ReadLine(), out selectedAction)
                   || selectedAction < 1 || selectedAction > 3)
                Console.WriteLine("Неверный ввод, попробуй еще раз");
            RandomMatrixInput(length, width, out auxiliaryArray);
            switch (selectedAction)
            {
                case 1:
                    ConsoleMatrixInput(length, width, out auxiliaryArray);
                    break;
                case 2:
                    RandomMatrixInput(length, width, out auxiliaryArray);
                    break;
                case 3:
                    string fileName;
                    Console.WriteLine("Введи название файла: ");
                    fileName = FileNameInput();
                    FileMatrixInput(length, width, out auxiliaryArray, fileName);
                    break;
            }
        }

        /// <summary>
        ///     Метод считывания размеров матрицы.
        /// </summary>
        /// <param name="length">Количество строк матрицы.</param>
        /// <param name="width">Количество столбцов матрицы.</param>
        public static void SizeInput(out int length, out int width)
        {
            Console.WriteLine("Введи количество строк:");
            while (!int.TryParse(Console.ReadLine(), out length) || length <= 0)
                Console.WriteLine("Неправильный ввод, пробуй еще раз");
            Console.WriteLine("Введи количество столбцов:");
            while (!int.TryParse(Console.ReadLine(), out width) || width <= 0)
                Console.WriteLine("Неправильный ввод, пробуй еще раз");
        }

        /// <summary>
        ///     Метод вывода выбора действия.
        /// </summary>
        public static void Menu()
        {
            Console.WriteLine("Выбери действие: (Для выбора введи номер действия)");
            Console.WriteLine("1. След матрицы");
            Console.WriteLine("2. Транспонирование матрицы");
            Console.WriteLine("3. Сумма двух матриц");
            Console.WriteLine("4. Разность двух матриц");
            Console.WriteLine("5. Произведение двух матриц");
            Console.WriteLine("6. Произведение матрицы на число");
            Console.WriteLine("7. Определитель матрицы");
            Console.WriteLine("8. Решение системы линейных уравнений");
        }

        /// <summary>
        ///     Метод вывода приветственного сообщения.
        /// </summary>
        public static void Greetings()
        {
            Console.WriteLine("Приветствую!");
            Console.WriteLine("Это программа матричный калькулятор");
            Console.WriteLine("Надеюсь ты знаешь ограничения, накладываемые операциями");
            Console.WriteLine("Кстати о них:");
        }

        /// <summary>
        /// Метод обработки пользовательского запроса следа матрицы.
        /// </summary>
        /// <param name="length1">Количество строк матрицы.</param>
        /// <param name="width1">Количество столбцов матрицы.</param>
        /// <param name="auxiliaryArray">Вспомогательный массив коэффициентов.</param>
        /// <param name="firstMatrix">Матрица.</param>
        private static void TreatmentMatrixTrace(out int length1, out int width1, out double[][] auxiliaryArray,
            out Matrix firstMatrix)
        {
            SizeInput(out length1, out width1);
            while (length1 != width1)
            {
                Console.WriteLine("След можно посчитать только для квадрутных матриц, " +
                                  "2 числа должны быть равны");
                SizeInput(out length1, out width1);
            }

            SelectMatrixInput(length1, width1, out auxiliaryArray);
            firstMatrix = new Matrix(auxiliaryArray);
            Console.WriteLine("Ваша матрица: \n" + firstMatrix);
            Console.WriteLine("След матрицы: " + firstMatrix.Trace());
        }

        /// <summary>
        /// Метод обработки пользовательского запроса Транспонирования матрицы.
        /// </summary>
        /// <param name="length1">Количество строк матрицы.</param>
        /// <param name="width1">Количество столбцов матрицы.</param>
        /// <param name="auxiliaryArray">Вспомогательный массив коэффициентов.</param>
        /// <param name="firstMatrix">Матрица.</param>
        private static void TreatmentMatrixTranspose(out int length1, out int width1, out double[][] auxiliaryArray,
            out Matrix firstMatrix)
        {
            SizeInput(out length1, out width1);
            SelectMatrixInput(length1, width1, out auxiliaryArray);
            firstMatrix = new Matrix(auxiliaryArray);
            Console.WriteLine("Ваша матрица: \n" + firstMatrix);
            firstMatrix.Transpose();
            Console.WriteLine("Транспонированная матрица: \n" + firstMatrix);
        }

        /// <summary>
        /// Метод обработки пользовательского запроса суммы матриц.
        /// </summary>
        /// <param name="length1">Количество строк первой матрицы.</param>
        /// <param name="width1">Количество столбцов первой матрицы.</param>
        /// <param name="auxiliaryArray">Вспомогательный массив коэффициентов.</param>
        /// <param name="firstMatrix">Первая матрица.</param>
        /// <param name="length2">Количество строк второй матрицы.</param>
        /// <param name="width2">Количество столбцов второй матрицы.</param>
        /// <param name="secondMatrix">Вторая матрица.</param>
        private static void TreatmentMatrixAddition(out int length1, out int width1, out double[][] auxiliaryArray,
            out Matrix firstMatrix, out int length2, out int width2, out Matrix secondMatrix)
        {
            Console.WriteLine("Введи 2 матрицы одинакового размера");
            SizeInput(out length1, out width1);
            SelectMatrixInput(length1, width1, out auxiliaryArray);
            firstMatrix = new Matrix(auxiliaryArray);
            Console.WriteLine("Ваша матрица: \n" + firstMatrix);
            SizeInput(out length2, out width2);
            while (length1 != length2 || width1 != width2)
            {
                Console.WriteLine("Размеры не совпадают, введи еще раз");
                SizeInput(out length2, out width2);
            }

            SelectMatrixInput(length2, width2, out auxiliaryArray);
            secondMatrix = new Matrix(auxiliaryArray);
            Console.WriteLine("Ваша матрица: \n" + secondMatrix);
            Console.WriteLine("Результат сложения: \n" + (firstMatrix + secondMatrix));
        }

        /// <summary>
        /// Метод обработки пользовательского запроса разности матриц.
        /// </summary>
        /// <param name="length1">Количество строк первой матрицы.</param>
        /// <param name="width1">Количество столбцов первой матрицы.</param>
        /// <param name="auxiliaryArray">Вспомогательный массив коэффициентов.</param>
        /// <param name="firstMatrix">Первая матрица.</param>
        /// <param name="length2">Количество строк второй матрицы.</param>
        /// <param name="width2">Количество столбцов второй матрицы.</param>
        /// <param name="secondMatrix">Вторая матрица.</param>
        private static void TreatmentMatrixSubtraction(out int length1, out int width1, out double[][] auxiliaryArray,
            out Matrix firstMatrix, out int length2, out int width2, out Matrix secondMatrix)
        {
            Console.WriteLine("Введи 2 матрицы одинакового размера");
            SizeInput(out length1, out width1);
            SelectMatrixInput(length1, width1, out auxiliaryArray);
            firstMatrix = new Matrix(auxiliaryArray);
            Console.WriteLine("Ваша матрица: \n" + firstMatrix);
            SizeInput(out length2, out width2);
            while (length1 != length2 || width1 != width2)
            {
                Console.WriteLine("Размеры не совпадают, введи еще раз");
                SizeInput(out length2, out width2);
            }

            SelectMatrixInput(length2, width2, out auxiliaryArray);
            secondMatrix = new Matrix(auxiliaryArray);
            Console.WriteLine("Ваша матрица: \n" + secondMatrix);
            Console.WriteLine("Результат вычитания: \n" + (firstMatrix - secondMatrix));
        }

        /// <summary>
        /// Метод обработки пользовательского запроса умножения матрицы на матрицу.
        /// </summary>
        /// <param name="length1">Количество строк первой матрицы.</param>
        /// <param name="width1">Количество столбцов первой матрицы.</param>
        /// <param name="auxiliaryArray">Вспомогательный массив коэффициентов.</param>
        /// <param name="firstMatrix">Первая матрица.</param>
        /// <param name="length2">Количество строк второй матрицы.</param>
        /// <param name="width2">Количество столбцов второй матрицы.</param>
        /// <param name="secondMatrix">Вторая матрица.</param>
        private static void TreatmentMatrixToMatrix(out int length1, out int width1, out double[][] auxiliaryArray,
            out Matrix firstMatrix, out int length2, out int width2, out Matrix secondMatrix)
        {
            Console.WriteLine(
                "Введи 2 матрицы, количество столбцов первой должно быть равно количеству строк во второй:");
            SizeInput(out length1, out width1);
            SelectMatrixInput(length1, width1, out auxiliaryArray);
            firstMatrix = new Matrix(auxiliaryArray);
            Console.WriteLine("Ваша матрица: \n" + firstMatrix);
            SizeInput(out length2, out width2);
            while (width1 != length2)
            {
                Console.WriteLine("Количество строк и столбцов не совпадает, давай по новой");
                SizeInput(out length2, out width2);
            }

            SelectMatrixInput(length2, width2, out auxiliaryArray);
            secondMatrix = new Matrix(auxiliaryArray);
            Console.WriteLine("Ваша матрица: \n" + secondMatrix);
            Console.WriteLine("Результат перемножения: \n" + firstMatrix * secondMatrix);
        }

        /// <summary>
        /// Метод обработки пользовательского запроса умножения матрицы на число.
        /// </summary>
        /// <param name="length1">Количество строк матрицы.</param>
        /// <param name="width1">Количество столбцов матрицы.</param>
        /// <param name="auxiliaryArray">Вспомогательный массив коэффициентов.</param>
        /// <param name="firstMatrix">Матрица.</param>
        /// <param name="multiply">Второй множитель.</param>
        private static void TreatmentMatrixToDouble(out int length1, out int width1, out double[][] auxiliaryArray,
            out Matrix firstMatrix, out double multiply)
        {
            SizeInput(out length1, out width1);
            SelectMatrixInput(length1, width1, out auxiliaryArray);
            firstMatrix = new Matrix(auxiliaryArray);
            Console.WriteLine("Ваша матрица: \n" + firstMatrix);
            Console.WriteLine("Введи 2 множитель:");
            while (!double.TryParse(Console.ReadLine(), out multiply))
                Console.WriteLine("Неккоректный ввод, попробуй еще раз");
            Console.WriteLine("Результат перемножения: \n" + firstMatrix * multiply);
        }

        /// <summary>
        /// Метод обработки пользовательского запроса определителя матрицы.
        /// </summary>
        /// <param name="length1">Количество строк матрицы.</param>
        /// <param name="width1">Количество столбцов матрицы.</param>
        /// <param name="firstMatrix">Матрица.</param>
        /// <param name="auxiliaryArray">Вспомогательный массив коэффициентов.</param>
        private static void TreatmentMatrixDetermenant(out int length1, out int width1, out Matrix firstMatrix, out double[][] auxiliaryArray)
        {
            SizeInput(out length1, out width1);
            while (length1 != width1)
            {
                Console.WriteLine("Определитель можно посчитать только для квадратных матриц, " +
                                  "2 числа должны быть равны");
                SizeInput(out length1, out width1);
            }

            SelectMatrixInput(length1, width1, out auxiliaryArray);
            firstMatrix = new Matrix(auxiliaryArray);
            Console.WriteLine("Ваша матрица: \n" + firstMatrix);
            Console.WriteLine("Определитель матрицы: " + Determinant(firstMatrix));
        }

        private static void Main()
        {
            string repeat;
            Greetings();
            do
            {
                int selectedAction;
                Menu();
                while (!int.TryParse(Console.ReadLine(), out selectedAction)
                       || selectedAction < 1 || selectedAction > 8)
                    Console.WriteLine("Неверный ввод, попробуй еще раз");
                int length1, width1, length2, width2;
                Matrix firstMatrix, secondMatrix;
                double[][] auxiliaryArray;
                switch (selectedAction)
                {
                    case 1:
                        TreatmentMatrixTrace(out length1, out width1, out auxiliaryArray, out firstMatrix);
                        break;
                    case 2:
                        TreatmentMatrixTranspose(out length1, out width1, out auxiliaryArray, out firstMatrix);
                        break;
                    case 3:
                        TreatmentMatrixAddition(out length1, out width1, out auxiliaryArray, out firstMatrix, out length2, out width2, out secondMatrix);
                        break;
                    case 4:
                        TreatmentMatrixSubtraction(out length1, out width1, out auxiliaryArray, out firstMatrix, out length2, out width2, out secondMatrix);
                        break;
                    case 5:
                        TreatmentMatrixToMatrix(out length1, out width1, out auxiliaryArray, out firstMatrix, out length2, out width2, out secondMatrix);
                        break;
                    case 6:
                        TreatmentMatrixToDouble(out length1, out width1, out auxiliaryArray, out firstMatrix, out var multiply);
                        break;
                    case 7:
                        TreatmentMatrixDetermenant(out length1, out width1, out firstMatrix, out auxiliaryArray);
                        break;
                    case 8:
                        EquationInput(out firstMatrix, out secondMatrix);
                        var roots = EquationSolve(firstMatrix, secondMatrix);
                        if (roots == null)
                            Console.WriteLine("Корней нет!");
                        else
                            for (var i = 0; i < roots.Length; i++)
                                Console.WriteLine($@"{i + 1} корень - " + roots[i].ToString("F3"));
                        break;
                }

                Console.WriteLine("\nВведи end если хочешь закончить или нажми Enter, чтобы продолжить.");
                repeat = Console.ReadLine();
                Console.Clear();
            } while (repeat.ToLower() != "end");

            Console.WriteLine("Спасибо за использование!\n");
        }
    }
}