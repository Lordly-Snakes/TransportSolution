using application;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.Storage.Provider;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace TransportSolution
{
    /// <summary>
    /// Обеспечивает зависящее от конкретного приложения поведение, дополняющее класс Application по умолчанию.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Инициализирует одноэлементный объект приложения. Это первая выполняемая строка разрабатываемого
        /// кода, поэтому она является логическим эквивалентом main() или WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Вызывается при обычном запуске приложения пользователем. Будут использоваться другие точки входа,
        /// например, если приложение запускается для открытия конкретного файла.
        /// </summary>
        /// <param name="e">Сведения о запросе и обработке запуска.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Не повторяйте инициализацию приложения, если в окне уже имеется содержимое,
            // только обеспечьте активность окна
            if (rootFrame == null)
            {
                // Создание фрейма, который станет контекстом навигации, и переход к первой странице
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Загрузить состояние из ранее приостановленного приложения
                }

                // Размещение фрейма в текущем окне
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // Если стек навигации не восстанавливается для перехода к первой странице,
                    // настройка новой страницы путем передачи необходимой информации в качестве параметра
                    // навигации
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Обеспечение активности текущего окна
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Вызывается в случае сбоя навигации на определенную страницу
        /// </summary>
        /// <param name="sender">Фрейм, для которого произошел сбой навигации</param>
        /// <param name="e">Сведения о сбое навигации</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Вызывается при приостановке выполнения приложения.  Состояние приложения сохраняется
        /// без учета информации о том, будет ли оно завершено или возобновлено с неизменным
        /// содержимым памяти.
        /// </summary>
        /// <param name="sender">Источник запроса приостановки.</param>
        /// <param name="e">Сведения о запросе приостановки.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Сохранить состояние приложения и остановить все фоновые операции
            deferral.Complete();
        }
    }
}
namespace core
{
    class Data : ICloneable
    {
        double[,] moneyPath;
        double[] from;
        double[] to;
        double sum;

        public Data(double[,] moneyPath, double[] from, double[] to)
        {
            this.moneyPath = moneyPath;
            this.from = from;
            this.to = to;
        }

        public Data()
        {           
        }

        public double[,] MoneyPath { get => moneyPath; set => moneyPath = value; }
        public double[] To { get => to; set => to = value; }
        public double[] From { get => from; set => from = value; }
        public double Sum { get => sum; set => sum = value; }

        /// <summary>
        /// Нахождение максимального и минимального элемента в двумерном массиве
        /// </summary>
        /// <returns>Возращает массив с двумя элементами, первый элемент максимальное значение, второй - минимальный</returns>
        public double[] MaxMincoef()
        {
            List<double> result = new List<double>();
            for(int i = 0; i < MoneyPath.GetLength(0); i++)
            {
                for(var j = 0; j < MoneyPath.GetLength(1); j++)
                {
                    result.Add(MoneyPath[i, j]);
                }
            }
            return new double[] { result.Max(), result.Min() };
        }

        /// <summary>
        /// Создание таблицы в Grid
        /// </summary>
        /// <param name="table">Элемент Grid</param>
        public void createTable(Grid table)
        {
            int RowCount = MoneyPath.GetLength(0) + 1;
            int Columnount = MoneyPath.GetLength(1) + 1;

            for (int i = 0; i < RowCount; i++)
            {
                table.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < Columnount; i++)
            {
                table.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        /// <summary>
        /// Создание TextBlock
        /// </summary>
        /// <param name="text">Входной текст</param>
        /// <param name="size">Размер текста</param>
        /// <returns>Возращает TextBlock</returns>
        public static TextBlock createTextBlock(string text, int size = 20)
        {
            return new TextBlock()
            {
                Text = text,
                FontSize = size,
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextWrapping = TextWrapping.Wrap
            };
        }

        /// <summary>
        /// Добавление элемента
        /// </summary>
        /// <param name="grid">Элемент Grid</param>
        /// <param name="row">Номер строки</param>
        /// <param name="column">Номер столбца</param>
        /// <param name="Obj">Элемент добавляемый в Grid</param>
        void addInGrid(Grid grid, int row, int column, FrameworkElement Obj)
        {
            Grid.SetRow(Obj, row);
            Grid.SetColumn(Obj, column);
            grid.Children.Add(Obj);
            Debug.WriteLine($"----{row} {column}----{column + row * (MoneyPath.GetLength(1)) + row} index:{grid.Children.Count - 1}");
        }

        /// <summary>
        /// Изменение значений в Grid
        /// </summary>
        /// <param name="grid">Элемент в котором происходят измененения</param>
        /// <param name="row">Номер строки</param>
        /// <param name="column">Номер столбца</param>
        /// <param name="text">Текст</param>
        void setInGrid(Grid grid, int row, int column, string text)
        {
            TextBlock textBlock = (TextBlock)grid.Children[column + row * (MoneyPath.GetLength(1)) + row];
            if (textBlock != null)
            {
                textBlock.Text = text;
            }

        }

        /// <summary>
        /// Устанавливает цвет у ячейки
        /// </summary>
        /// <param name="grid">Элемент в котором происходят измененения</param>
        /// <param name="color">Цвет</param>
        /// <param name="row">Номер строки</param>
        /// <param name="column">Номер столбца</param>
        public void ColorSetGrid(Grid grid, Color color, int row, int column)
        {
            TextBlock text = (TextBlock)(grid.Children[column + row * (MoneyPath.GetLength(1)) + row]);
            if (text != null)
            {
                text.Foreground = new SolidColorBrush(color);
            }


        }

        /// <summary>
        /// Отображение в Grid
        /// </summary>
        /// <param name="table">Элемент Grid</param>
        public void displayInGrid(Grid table)
        {
            if (table.ColumnDefinitions.Count != MoneyPath.GetLength(1) + 1 || table.RowDefinitions.Count != MoneyPath.GetLength(0) + 1)
            {
                table.ColumnDefinitions.Clear();
                table.RowDefinitions.Clear();
                createTable(table);
                table.Children.Clear();
                for (int i = 0; i < MoneyPath.GetLength(0) + 1; i++)
                {
                    for (int j = 0; j < MoneyPath.GetLength(1) + 1; j++)
                    {
                        if (j < MoneyPath.GetLength(1) && i < MoneyPath.GetLength(0))
                        {
                            TextBlock textBlock = createTextBlock(MoneyPath[i, j].ToString());
                            if (MoneyPath[i,j] == -1)
                            {
                                textBlock.Text = "X";
                            }
                            
                            addInGrid(table, i, j, textBlock);
                        }
                        else if (i < MoneyPath.GetLength(0) && j == MoneyPath.GetLength(1))
                        {
                            TextBlock textBlock = createTextBlock(to[i].ToString());
                            addInGrid(table, i, MoneyPath.GetLength(1), textBlock);
                        }
                        else if (j < MoneyPath.GetLength(1) && i == MoneyPath.GetLength(0))
                        {
                            TextBlock textBlock = createTextBlock(from[j].ToString());
                            addInGrid(table, MoneyPath.GetLength(0), j, textBlock);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < MoneyPath.GetLength(0) + 1; i++)
                {
                    for (int j = 0; j < MoneyPath.GetLength(1) + 1; j++)
                    {
                        if (j < MoneyPath.GetLength(1) && i < MoneyPath.GetLength(0))
                        {
                            TextBlock textBlock = createTextBlock(MoneyPath[i, j].ToString());
                            setInGrid(table, i, j, MoneyPath[i, j].ToString());
                            if (MoneyPath[i, j] == -1)
                            {
                                setInGrid(table, i, j, "X");
                                
                            }

                        }
                        else if (i < MoneyPath.GetLength(0) && j == MoneyPath.GetLength(1))
                        {
                            TextBlock textBlock = createTextBlock(to[i].ToString());
                            setInGrid(table, i, MoneyPath.GetLength(1), to[i].ToString());
                        }
                        else if (j < MoneyPath.GetLength(1) && i == MoneyPath.GetLength(0))
                        {
                            TextBlock textBlock = createTextBlock(from[j].ToString());
                            setInGrid(table, MoneyPath.GetLength(0), j, from[j].ToString());
                        }
                    }
                }
            }
        }

        // 2 3 4 5
        // 1 3 4 6
        // 3 4 6 7;
        // 212 324 453 123;
        // 345 543 324
        /// <summary>
        /// Чтение из файла
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns></returns>
        public static Data ReadFile(string path)
        {
            //try
            {
                //Algorithms.PrintInList(listBox, "___START READ FILE___");
                string data = File.ReadAllText(path);
                Log.I().write("sad");
                //Algorithms.PrintInList(listBox, "___END READ FILE___");
                string[] dataSplitter = data.Split(';');
                string[] ch = dataSplitter[0].Split('\n');
                string[] f = dataSplitter[1].Split(' ');
                string[] t = dataSplitter[2].Split(' ');
                double[,] dataCh = new double[ch.Length, ch[0].Split(' ').Length];
                double[] from = new double[f.Length];
                double[] to = new double[t.Length];
                for (int i = 0; i < f.Length; i++)
                {
                    from[i] = Convert.ToDouble(f[i]);
                }
                for (int i = 0; i < t.Length; i++)
                {
                    to[i] = Convert.ToDouble(t[i]);
                }

                if(from.Sum() != to.Sum())
                {
                    if (from.Sum()>to.Sum())
                    {
                        to = new double[t.Length+1];
                        for (int i = 0; i < t.Length+1; i++)
                        {
                            if (i<t.Length)
                            {
                                to[i] = Convert.ToDouble(t[i]);
                            }
                            else
                            {
                                to[i] = from.Sum()-to.Sum();
                            }
                        }
                        dataCh = new double[ch.Length+1, ch[0].Split(' ').Length];
                        for (int i = 0; i < ch.Length+1; i++)
                        {
                            string[] chs = ch[i].Split(' ');
                            for (int j = 0; j < chs.Length; j++)
                            {
                                if (i< ch.Length)
                                {
                                    dataCh[i, j] = Convert.ToDouble(chs[j]);
                                }
                                else
                                {
                                    dataCh[i, j] = 0;
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        from = new double[f.Length + 1];
                        for (int i = 0; i < f.Length + 1; i++)
                        {
                            if (i < f.Length)
                            {
                                from[i] = Convert.ToDouble(f[i]);
                            }
                            else
                            {
                                from[i] = to.Sum() - from.Sum();
                            }
                        }
                        dataCh = new double[ch.Length, ch[0].Split(' ').Length+1];
                        for (int i = 0; i < ch.Length; i++)
                        {
                            string[] chs = ch[i].Split(' ');
                            for (int j = 0; j < chs.Length+1; j++)
                            {
                                if (j<chs.Length)
                                {
                                    dataCh[i, j] = Convert.ToDouble(chs[j]);
                                }
                                else
                                {
                                    dataCh[i, j] = 0;
                                }
                                
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < ch.Length; i++)
                    {
                        string[] chs = ch[i].Split(' ');
                        for (int j = 0; j < chs.Length; j++)
                        {
                            dataCh[i, j] = Convert.ToDouble(chs[j]);
                        }
                    }
                }
                //Algorithms.PrintInList(listBox, "___START PROCESSED DATA___");
               
                

                //for(int i=0;i<dataSplitter[].Length;i++)
                return new Data(dataCh, from, to);
            }
            //catch (Exception e)
            //{
            //    Algorithms.DisplayDialog("error", e.Message);
            //    return null;
            //}

        }

        /// <summary>
        /// Асинзронное чтение из файла 
        /// </summary>
        /// <param name="file">Файл из которого нужно выполнить чтение</param>
        /// <returns>Данные структурированные в классе Data</returns>
        public async static Task<Data> ReadFile(StorageFile file)
        {
            try
            {
                //Algorithms.PrintInList(listBox, "___START READ FILE___");
                string data = await FileIO.ReadTextAsync(file);



                //Algorithms.PrintInList(listBox, "___END READ FILE___");
                string[] dataSplitter = data.Split(';');
                string[] ch = dataSplitter[0].Split('\n');
                string[] f = dataSplitter[1].Split(' ');
                string[] t = dataSplitter[2].Split(' ');
                double[,] dataCh = new double[ch.Length, ch[0].Split(' ').Length];
                double[] from = new double[f.Length];
                double[] to = new double[t.Length];
                for (int i = 0; i < f.Length; i++)
                {
                    from[i] = Convert.ToDouble(f[i]);
                }
                for (int i = 0; i < t.Length; i++)
                {
                    to[i] = Convert.ToDouble(t[i]);
                }

                if (from.Sum() != to.Sum())
                {
                    if (from.Sum() > to.Sum())
                    {
                        to = new double[t.Length + 1];
                        for (int i = 0; i < t.Length + 1; i++)
                        {
                            if (i < t.Length)
                            {
                                to[i] = Convert.ToDouble(t[i]);
                            }
                            else
                            {
                                to[i] = from.Sum() - to.Sum();
                            }
                        }
                        dataCh = new double[ch.Length + 1, ch[0].Split(' ').Length];
                        for (int i = 0; i < ch.Length + 1; i++)
                        {
                            string[] chs = ch[0].Split(' ');
                            if (i < ch.Length)
                            {
                                chs = ch[i].Split(' ');
                            }
                            
                            for (int j = 0; j < chs.Length; j++)
                            {
                                if (i < ch.Length)
                                {
                                    dataCh[i, j] = Convert.ToDouble(chs[j]);
                                }
                                else
                                {
                                    dataCh[i, j] = 0;
                                }

                            }
                        }
                    }
                    else
                    {
                        from = new double[f.Length + 1];
                        for (int i = 0; i < f.Length + 1; i++)
                        {
                            if (i < f.Length)
                            {
                                from[i] = Convert.ToDouble(f[i]);
                            }
                            else
                            {
                                from[i] = to.Sum() - from.Sum();
                            }
                        }
                        dataCh = new double[ch.Length, ch[0].Split(' ').Length + 1];
                        for (int i = 0; i < ch.Length; i++)
                        {
                            string[] chs = ch[i].Split(' ');
                            for (int j = 0; j < chs.Length + 1; j++)
                            {
                                if (j < chs.Length)
                                {
                                    dataCh[i, j] = Convert.ToDouble(chs[j]);
                                }
                                else
                                {
                                    dataCh[i, j] = 0;
                                }

                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < ch.Length; i++)
                    {
                        string[] chs = ch[i].Split(' ');
                        for (int j = 0; j < chs.Length; j++)
                        {
                            dataCh[i, j] = Convert.ToDouble(chs[j]);
                        }
                    }
                }
                return new Data(dataCh, from, to);
            }
            catch (Exception e)
            {
               await Algorithms.DisplayDialog("Error", e.Message);
              return null;
            }

        }

        /// <summary>
        /// Запись в файл
        /// </summary>
        /// <param name="path">путь к файлу</param>
        public void WriteFile(string path)
        {
            try
            {

                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                // double[,] dataCh = this.moneyPath;
                for (int i = 0; i < moneyPath.GetLength(0); i++)
                {
                    for (int j = 0; j < moneyPath.GetLength(1); j++)
                    {
                        if (j < moneyPath.GetLength(1) - 1)
                        {
                            sw.Write(moneyPath[i, j].ToString() + " ");
                        }
                        else
                        {
                            sw.Write(moneyPath[i, j].ToString());
                        }
                    }
                    if (i == moneyPath.GetLength(0) - 1)
                    {
                        sw.Write(";");
                    }
                    sw.WriteLine();
                }
                sw.WriteLine(this.sum.ToString());
                sw.Close();
                fs.Close();


            }
            catch (Exception e)
            {
                

            }

        }

        public async Task<FileUpdateStatus> WriteFileAync(StorageFile file)
        {
            try
            {

               // FileStream fs = new FileStream(file);
                //StreamWriter sw = new StreamWriter(fs);
                string sw = "";
                // double[,] dataCh = this.moneyPath;
                for (int i = 0; i < moneyPath.GetLength(0); i++)
                {
                    for (int j = 0; j < moneyPath.GetLength(1); j++)
                    {
                        if (j < moneyPath.GetLength(1) - 1)
                        {
                            sw += moneyPath[i, j].ToString() + " ";
                        }
                        else
                        {
                            sw += moneyPath[i, j].ToString();
                        }
                    }
                    if (i == moneyPath.GetLength(0) - 1)
                    {
                        sw += ";";
                    }
                    sw += "\n";
                    //sw.WriteLine();
                }
                sw += "\n" + this.sum.ToString();
                //sw.WriteLine(this.sum.ToString());
                //sw.Close();
                //fs.Close();
                
                await FileIO.WriteTextAsync(file, sw);
                return await CachedFileManager.CompleteUpdatesAsync(file);
                 
            }
            catch (Exception e)
            {

                return FileUpdateStatus.Failed;
            }

        }

        public object Clone()
        {
            return new Data() { moneyPath = (double[,])this.moneyPath.Clone(), from = (double[])this.from.Clone(), to = (double[])this.to.Clone(), sum = this.sum };

        }

        public async Task<Data> CloneAsync()
        {
            Data data = null;
            await Task.Run(() => { data = new Data() { moneyPath = (double[,])this.moneyPath.Clone(), from = (double[])this.from.Clone(), to = (double[])this.to.Clone(), sum = this.sum }; });
            return data;
        }
    }

    /// <summary>
    /// Вспомагательный класс для фиксаций точек в массиве
    /// </summary>
    class PointI
    {
        int x;
        int y;

        public PointI(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int Y { get => y; set => y = value; }
        public int X { get => x; set => x = value; }

        /// <summary>
        /// Сравнеие двух точек
        /// </summary>
        /// <param name="pointTwo">Вторая точка</param>
        /// <returns>true если они одинаковы false если отличается хотя бы одна из координат</returns>
        public bool Equals(PointI pointTwo)
        {
            //  Debug.WriteLine($"X-X {this.X} {pointTwo.X} Y-Y {this.Y} {pointTwo.Y}");
            return X == pointTwo.X && Y == pointTwo.Y;
        }

        /// <summary>
        /// Сравнеие двух точек
        /// </summary>
        /// <param name="pointOne">Первая точка</param>
        /// <param name="pointTwo">Вторая точка</param>
        /// <returns>true если они одинаковы false если отличается хотя бы одна из координат</returns>
        public static bool Equals(PointI pointOne, PointI pointTwo)
        {
            return pointOne.X == pointTwo.X && pointOne.Y == pointTwo.Y;
        }

        /// <summary>
        /// Проверка наличия точки в List
        /// </summary>
        /// <param name="pointDs">Лист в котором проверяется</param>
        /// <returns></returns>
        public bool ContainInList(IEnumerable<PointI> pointDs)
        {
            foreach (PointI v in pointDs)
                if (v.Equals(this))
                    return true;
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    static class Algorithms
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listBox"></param>
        /// <param name="value"></param>
        public static void PrintInList(ListBox listBox, string value)
        {
            listBox.Items.Add(Data.createTextBlock(value, 10));
            listBox.SelectedIndex = listBox.Items.Count - 1;
            listBox.ScrollIntoView(listBox.SelectedItem);
        }

        /// <summary>
        /// Функция нахождения минимального коэффицента
        /// </summary>
        /// <param name="d"></param>
        /// <param name="pointIs"></param>
        /// <returns></returns>
        private static int[] MinArray(Data d,List<PointI> pointIs)
        {
            double min = d.MaxMincoef()[0];
            int mini = -1;
            int minj = -1;
            double[,] data = d.MoneyPath;
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    if (min > data[i, j] && data[i, j] != -1 && data[i, j] !=0 && !new PointI(j,i).ContainInList(pointIs))
                    {
                        min = data[i, j];
                        
                        mini = i;
                        minj = j;
                        Debug.WriteLine( $"new min {mini} {minj}");
                    }
                }
            }
            Debug.WriteLine(min + $" {mini} {minj}");
            return new int[] { mini, minj };
        }

        /// <summary>
        /// Поиск индексов для метода Фогиля
        /// </summary>
        /// <param name="Val"></param>
        /// <param name="linei"></param>
        /// <param name="linej"></param>
        /// <param name="pointIs"></param>
        /// <returns></returns>
        public static int[] FogArray(Data Val, List<int> linei, List<int> linej, List<PointI> pointIs)
        {
            double[,] data = Val.MoneyPath;
            int maxi;
            int maxj;
            Debug.WriteLine($"counti {linei.Count} getl(1){data.GetLength(1)}");
            Debug.WriteLine($"countj {linej.Count} getl(0){data.GetLength(0)}");
            double min;
            // Создание массивов для хранения максимумов
            double[] maxJ = new double[data.GetLength(0)];
            double[] maxI = new double[data.GetLength(1)];
            
            // Создание дополнительных переменных
            double maxiJ = 0;
            int minimumJindex = -1;
            double maxiI = 0;
            int minimumIindex = -1;
            List<PointI> countschetstr = new List<PointI>();
            List<PointI> countschetstb = new List<PointI>();
            // Считает разность двух по строкам
            for (int i = 0; i < data.GetLength(0); i++)
            {
                Debug.WriteLine($"==================================== строка {i} ============================================");
                if (!contain(linej, i))
                {
                    Debug.WriteLine("-=-=-=-=-=-=-=-=-=-=-");
                    List<PointI> vsJ = new List<PointI>();
                    min = data[i, 0];
                    maxj = i;
                    maxi = 0;

                    List<double> v = new List<double>();
                    for (int h = 0; h < 3; h++)
                    {
                        for (int j = 0; j < data.GetLength(1); j++)
                        {
                            if (!contain(linei, j))
                            {
                                if (h == 0)
                                {
                                    v.Add(data[i, j]);
                                }
                                else
                                {
                                    if (min >= data[i, j] && data[i, j] != -1 && !(new PointI(j, i).ContainInList(vsJ)))
                                    {
                                        min = data[i, j];
                                        maxi = i;
                                        maxj = j;
                                    }
                                }
                            }
                        }
                        if (h != 0)
                        {
                            vsJ.Add(new PointI(maxj, maxi));
                            countschetstr.Add(new PointI(maxj, maxi));
                            Debug.WriteLine($"try {h}:coordinate {maxi}:{maxj} = {data[maxi, maxj]}");
                        }
                        min = v.Max();
                    }
                    maxJ[i] = Math.Abs(data[vsJ[0].Y, vsJ[0].X] - data[vsJ[1].Y, vsJ[1].X]);
                    Debug.WriteLine($"raz {maxJ[i]}");
                    if (i == 0)
                    {
                        maxiJ = Math.Abs(data[vsJ[0].Y, vsJ[0].X] - data[vsJ[1].Y, vsJ[1].X]);
                        minimumJindex = 0;
                    }
                    else
                    {
                        if (Math.Abs(data[vsJ[0].Y, vsJ[0].X] - data[vsJ[1].Y, vsJ[1].X]) > maxiJ)
                        {
                            maxiJ = Math.Abs(data[vsJ[0].Y, vsJ[0].X] - data[vsJ[1].Y, vsJ[1].X]);
                            minimumJindex = i;
                        }
                    }
                }
            }

            maxi = -1;
            maxj = -1;
            for (int i = 0; i < data.GetLength(1); i++)
            {
                Debug.WriteLine($"=======================================столбец {i}========================================");
                if (!contain(linei, i))
                {
                    Debug.WriteLine("-=-=-=-=-=-=-=-=-=-=-");
                    List<PointI> vsI = new List<PointI>();
                    min = data[0, i];
                    maxj = 0;
                    maxi = i;

                    List<double> v = new List<double>();
                    for (int h = 0; h < 3; h++)
                    {
                        for (int j = 0; j < data.GetLength(0); j++)
                        {
                            if (!contain(linej, j))
                            {
                                if (h == 0)
                                {
                                    v.Add(data[j, i]);
                                }
                                else
                                {
                                    if (min >= data[j, i] && data[j, i] != -1 && !(new PointI(j, i).ContainInList(vsI)))
                                    {
                                        min = data[j, i];
                                        maxi = i;
                                        maxj = j;
                                    }
                                }
                            }
                        }
                        if (h != 0)
                        {
                            countschetstb.Add(new PointI(maxj, maxi));
                            Debug.WriteLine($"try {h}:coordinate {maxj}:{maxi} = {data[maxj, maxi]}");
                            vsI.Add(new PointI(maxj, maxi));
                        }
                        min = v.Max();
                    }
                    maxI[i] = Math.Abs(data[vsI[0].X, vsI[0].Y] - data[vsI[1].X, vsI[1].Y]);
                    Debug.WriteLine($"raz  {data[vsI[0].X, vsI[0].Y]}-{data[vsI[1].X, vsI[1].Y]}={maxI[i]}");
                    if (i == 0)
                    {
                        maxiI = Math.Abs(data[vsI[0].X, vsI[0].Y] - data[vsI[1].X, vsI[1].Y]);
                        minimumIindex = 0;
                    }
                    else
                    {
                        if (Math.Abs(data[vsI[0].X, vsI[0].Y] - data[vsI[1].X, vsI[1].Y]) > maxiI)
                        {
                            maxiI = Math.Abs(data[vsI[0].X, vsI[0].Y] - data[vsI[1].X, vsI[1].Y]);
                            minimumIindex = i;
                        }
                    }
                }
            }


            Debug.WriteLine($"minimumminimuma {maxiJ}<{maxiI} строка{minimumJindex} столбец{minimumIindex}  col-vo{countschetstb.Count} col-vo{countschetstr.Count}");
            if (maxiI == maxiJ && countschetstb.Count == 2 && countschetstr.Count ==2)
            {
                if (true)
                {
                    Debug.WriteLine($"!!!!!!!!!!!!!!!!!!!!");
                    {
                        maxj = countschetstb[0].Y;
                        maxi = countschetstb[0].X;
                    }
                }
                
            }
            else if (maxiJ > maxiI)
            {
                if (minimumJindex > -1)
                {
                    double endMin = Val.MaxMincoef()[0];
                    maxi = minimumJindex;
                    maxj = 0;
                    for (int j = 0; j < data.GetLength(1); j++)
                    {
                        if (endMin >= data[minimumJindex, j] && data[minimumJindex, j] != -1 && !new PointI(j, minimumJindex).ContainInList(pointIs))
                        {
                            endMin = data[minimumJindex, j];
                            maxi = minimumJindex;
                            maxj = j;
                        }
                        Debug.WriteLine($"t {endMin}");
                    }
                }
            }
            else
            {
                if (minimumIindex > -1)
                {
                    double endMin = Val.MaxMincoef()[0];
                    maxi = 0;
                    maxj = minimumIindex;
                    for (int j = 0; j < data.GetLength(0); j++)
                    {
                        if (endMin >= data[j, minimumIindex] && data[j, minimumIindex] != -1 && !new PointI(minimumIindex,j).ContainInList(pointIs))
                        {
                            endMin = data[j, minimumIindex];
                            maxi = j;
                            maxj = minimumIindex;
                        }
                        Debug.WriteLine($"e {endMin}");
                    }
                }

            }
            Debug.WriteLine($"___________________ {maxi} {maxj} ________________________");
            return new int[] { maxi, maxj };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vs"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        static bool contain(List<int> vs, int x)
        {
            for (int i = 0; i < vs.Count; i++)
            {
                if (vs[i] == x)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataGrid"></param>
        /// <param name="listBox"></param>
        /// <returns></returns>
        public static async Task<Data> FogVal(Data data, Grid dataGrid, ListBox listBox)
        {

            int count = data.From.ToList().Count() + data.To.ToList().Count() - 1;
            List<PointI> pointDs = new List<PointI>();
            List<int> indexesi = new List<int>();
            List<int> indexesj = new List<int>();
            double sum = 0;
            PrintInList(listBox, "___START ALGORITHM___");
            for (int i = 0; i < count; i++)
            {

                int[] index = FogArray(data, indexesi, indexesj,pointDs);
                if (index[0] > -1 && index[0] > -1)
                {
                    PointI currentPoint = new PointI(index[1], index[0]);
                    await Task.Delay(1000);
                    PrintInList(listBox, $"__ITERATION__{i}");
                    if (currentPoint.X != -1 && currentPoint.Y != -1)
                    {

                        if (!currentPoint.ContainInList(pointDs))
                        {
                            PrintInList(listBox, $"{currentPoint.X} {currentPoint.Y}");
                            pointDs.Add(currentPoint);
                            //MessageBox.Show($"{data.From[currentPoint.X]} {data.To[currentPoint.Y]} {index[0]} {index[1]} {data.MoneyPath[currentPoint.Y, currentPoint.X]}");
                            if (data.From[currentPoint.X] < data.To[currentPoint.Y])
                            {
                                sum += data.From[currentPoint.X] * data.MoneyPath[currentPoint.Y, currentPoint.X];
                                Debug.WriteLine($"vert {data.From[currentPoint.X]}*{ data.MoneyPath[currentPoint.Y, currentPoint.X]}={data.From[currentPoint.X] * data.MoneyPath[currentPoint.Y, currentPoint.X]} sum={sum}");
                                PrintInList(listBox, $"min value [{data.From[currentPoint.X]},{ data.To[currentPoint.Y]}] is {data.From[currentPoint.X]}");
                                PrintInList(listBox, $"vert {data.From[currentPoint.X]}*{ data.MoneyPath[currentPoint.Y, currentPoint.X]}={data.From[currentPoint.X] * data.MoneyPath[currentPoint.Y, currentPoint.X]} sum={sum}");
                                Perestanovka(data, currentPoint, pointDs, 0, dataGrid);
                                indexesi.Add(index[1]);
                                Debug.WriteLine($"-----------------------------i{index[1]}-----------------------");
                            }
                            else if (data.From[currentPoint.X] == data.To[currentPoint.Y])
                            {
                                sum += data.From[currentPoint.X] * data.MoneyPath[currentPoint.Y, currentPoint.X];
                                Debug.WriteLine($"vert {data.From[currentPoint.X]}*{data.MoneyPath[currentPoint.Y, currentPoint.X]}={data.From[currentPoint.X] * data.MoneyPath[currentPoint.Y, currentPoint.X]} sum={sum}");
                                PrintInList(listBox, $"value [{data.From[currentPoint.X]},{ data.To[currentPoint.Y]}] is equal");
                                PrintInList(listBox, $"horver {data.From[currentPoint.X]}*{ data.MoneyPath[currentPoint.Y, currentPoint.X]}={data.From[currentPoint.X] * data.MoneyPath[currentPoint.Y, currentPoint.X]} sum={sum}");
                                Perestanovka(data, currentPoint, pointDs, 2, dataGrid);
                                Debug.WriteLine($"-----------------------------j{index[0]}-----------------------");
                                Debug.WriteLine($"-----------------------------i{index[1]}-----------------------");
                                indexesj.Add(index[0]);
                                indexesi.Add(index[1]);
                            }
                            else
                            {
                                sum += data.To[currentPoint.Y] * data.MoneyPath[currentPoint.Y, currentPoint.X];
                                Debug.WriteLine($"hor {data.To[currentPoint.Y]}*{data.MoneyPath[currentPoint.Y, currentPoint.X]}={data.To[currentPoint.Y] * data.MoneyPath[currentPoint.Y, currentPoint.X]} sum={sum}");
                                PrintInList(listBox, $"min value [{data.From[currentPoint.X]},{ data.To[currentPoint.Y]}] is {data.To[currentPoint.Y]}");
                                PrintInList(listBox, $"hor {data.To[currentPoint.Y]}*{data.MoneyPath[currentPoint.Y, currentPoint.X]}={data.To[currentPoint.Y] * data.MoneyPath[currentPoint.Y, currentPoint.X]} sum={sum}");
                                Perestanovka(data, currentPoint, pointDs, 1, dataGrid);
                                indexesj.Add(index[0]);
                                Debug.WriteLine($"-----------------------------j{index[0]}-----------------------");
                            }
                            data.displayInGrid(dataGrid);
                            // return data;
                        }
                        else
                        {
                            //return null;
                            PrintInList(listBox, $"__THIS POINT JUST PROCESSED__");
                        }
                    }
                    else
                    {
                        //return null;
                        PrintInList(listBox, $"__POINT NOT FOUND__");
                    }
                    PrintInList(listBox, $"__END ITERATION__{i}");
                }
            }
            for (int i = 0; i < data.MoneyPath.GetLength(0) + 1; i++)
            {
                for (int j = 0; j < data.MoneyPath.GetLength(1) + 1; j++)
                {
                    data.ColorSetGrid(dataGrid, Windows.UI.Colors.White, i, j);
                    // dataGrid.Rows[i].Cells[j].Style.BackColor = Color.White;
                }
            }
            PrintInList(listBox, $"___END ALGORITHM___SUM={sum}");

            data.Sum = sum;
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataGrid"></param>
        /// <param name="listBox"></param>
        /// <returns></returns>
        public static async Task<Data> MinVal(Data data, Grid dataGrid, ListBox listBox)
        {

            int count = data.From.ToList().Count() + data.To.ToList().Count()-1;
            List<PointI> pointDs = new List<PointI>();
            double sum = 0;
            PrintInList(listBox, "___START ALGORITHM___");
            for (int i = 0; i < count; i++)
            {
                int[] index = MinArray(data,pointDs);
                PointI currentPoint = new PointI(index[1], index[0]);
                await Task.Delay(1000);
                PrintInList(listBox, $"__ITERATION__{i}");
                if (currentPoint.X != -1 && currentPoint.Y != -1)
                {

                    if (!currentPoint.ContainInList(pointDs))
                    {

                        pointDs.Add(currentPoint);
                        //MessageBox.Show($"{data.From[currentPoint.X]} {data.To[currentPoint.Y]} {index[0]} {index[1]} {data.MoneyPath[currentPoint.Y, currentPoint.X]}");
                        if (data.From[currentPoint.X] < data.To[currentPoint.Y])
                        {
                            sum += data.From[currentPoint.X] * data.MoneyPath[currentPoint.Y, currentPoint.X];
                            Debug.WriteLine($"vert {data.From[currentPoint.X]}*{ data.MoneyPath[currentPoint.Y, currentPoint.X]}={data.From[currentPoint.X] * data.MoneyPath[currentPoint.Y, currentPoint.X]} sum={sum}");
                            PrintInList(listBox, $"min value [{data.From[currentPoint.X]},{ data.To[currentPoint.Y]}] is {data.From[currentPoint.X]}");
                            PrintInList(listBox, $"vert {data.From[currentPoint.X]}*{ data.MoneyPath[currentPoint.Y, currentPoint.X]}={data.From[currentPoint.X] * data.MoneyPath[currentPoint.Y, currentPoint.X]} sum={sum}");
                            Perestanovka(data, currentPoint, pointDs, 0, dataGrid);
                        }
                        else if (data.From[currentPoint.X] == data.To[currentPoint.Y])
                        {
                            sum += data.From[currentPoint.X] * data.MoneyPath[currentPoint.Y, currentPoint.X];
                            Debug.WriteLine($"vert {data.From[currentPoint.X]}*{data.MoneyPath[currentPoint.Y, currentPoint.X]}={data.From[currentPoint.X] * data.MoneyPath[currentPoint.Y, currentPoint.X]} sum={sum}");
                            PrintInList(listBox, $"value [{data.From[currentPoint.X]},{ data.To[currentPoint.Y]}] is equal");
                            PrintInList(listBox, $"horver {data.From[currentPoint.X]}*{ data.MoneyPath[currentPoint.Y, currentPoint.X]}={data.From[currentPoint.X] * data.MoneyPath[currentPoint.Y, currentPoint.X]} sum={sum}");
                            Perestanovka(data, currentPoint, pointDs, 2, dataGrid);
                        }
                        else
                        {
                            sum += data.To[currentPoint.Y] * data.MoneyPath[currentPoint.Y, currentPoint.X];
                            Debug.WriteLine($"hor {data.To[currentPoint.Y]}*{data.MoneyPath[currentPoint.Y, currentPoint.X]}={data.To[currentPoint.Y] * data.MoneyPath[currentPoint.Y, currentPoint.X]} sum={sum}");
                            PrintInList(listBox, $"min value [{data.From[currentPoint.X]},{ data.To[currentPoint.Y]}] is {data.To[currentPoint.Y]}");
                            PrintInList(listBox, $"hor {data.To[currentPoint.Y]}*{data.MoneyPath[currentPoint.Y, currentPoint.X]}={data.To[currentPoint.Y] * data.MoneyPath[currentPoint.Y, currentPoint.X]} sum={sum}");
                            Perestanovka(data, currentPoint, pointDs, 1, dataGrid);
                        }
                        data.displayInGrid(dataGrid);
                        // return data;
                    }
                    else
                    {
                        PrintInList(listBox, $"__THIS POINT JUST PROCESSED__");
                    }
                }
                else
                {
                    PrintInList(listBox, $"__POINT NOT FOUND__");
                    break;
                   
                }
                PrintInList(listBox, $"__END ITERATION__{i}");

            }
            for (int i = 0; i < data.MoneyPath.GetLength(0) + 1; i++)
            {
                for (int j = 0; j < data.MoneyPath.GetLength(1) + 1; j++)
                {
                    data.ColorSetGrid(dataGrid, Windows.UI.Colors.White, i, j);
                    // dataGrid.Rows[i].Cells[j].Style.BackColor = Color.White;
                }
            }
            PrintInList(listBox, $"___END ALGORITHM___SUM={sum}");

            data.Sum = sum;
            return data;
        }

        /// <summary>
        /// Функция нахождения стоимости перевозок методом северо-западного угла
        /// </summary>
        /// <param name="data">Обьект данных</param>
        /// <param name="dataGrid">Сетка куда ввыведенны данные</param>
        /// <param name="listBox"></param>
        /// <returns></returns>
        public static async Task<Data> SZVal(Data data, Grid dataGrid, ListBox listBox)
        {

            int count = data.From.ToList().Count() + data.To.ToList().Count() - 1;
            List<PointI> pointDs = new List<PointI>();
            double sum = 0;
            int[] index = new int[] { 0, 0 };
            PrintInList(listBox, "___START ALGORITHM___");
            for (int i = 0; i < count; i++)
            {

                //int[] index = MinArray(data.MoneyPath);
                PointI currentPoint = new PointI(index[1], index[0]);
                await Task.Delay(1000);
                PrintInList(listBox, $"__ITERATION__{i}");
                if (currentPoint.X != -1 && currentPoint.Y != -1 )
                {
                    if (currentPoint.X< data.MoneyPath.GetLength(1) && currentPoint.Y < data.MoneyPath.GetLength(0))
                    {
                        if (!currentPoint.ContainInList(pointDs))
                        {

                            pointDs.Add(currentPoint);

                            //MessageBox.Show($"{data.From[currentPoint.X]} {data.To[currentPoint.Y]} {index[0]} {index[1]} {data.MoneyPath[currentPoint.Y, currentPoint.X]}");
                            if (data.From[currentPoint.X] < data.To[currentPoint.Y])
                            {
                                sum += data.From[currentPoint.X] * data.MoneyPath[currentPoint.Y, currentPoint.X];
                                Debug.WriteLine($"vert {data.From[currentPoint.X]}*{ data.MoneyPath[currentPoint.Y, currentPoint.X]}={data.From[currentPoint.X] * data.MoneyPath[currentPoint.Y, currentPoint.X]} sum={sum}");
                                PrintInList(listBox, $"min value [{data.From[currentPoint.X]},{ data.To[currentPoint.Y]}] is {data.From[currentPoint.X]}");
                                PrintInList(listBox, $"vert {data.From[currentPoint.X]}*{ data.MoneyPath[currentPoint.Y, currentPoint.X]}={data.From[currentPoint.X] * data.MoneyPath[currentPoint.Y, currentPoint.X]} sum={sum}");
                                Perestanovka(data, currentPoint, pointDs, 0, dataGrid);
                                index[1]++;
                            }
                            else if (data.From[currentPoint.X] == data.To[currentPoint.Y])
                            {
                                sum += data.From[currentPoint.X] * data.MoneyPath[currentPoint.Y, currentPoint.X];
                                Debug.WriteLine($"vert {data.From[currentPoint.X]}*{data.MoneyPath[currentPoint.Y, currentPoint.X]}={data.From[currentPoint.X] * data.MoneyPath[currentPoint.Y, currentPoint.X]} sum={sum}");
                                PrintInList(listBox, $"value [{data.From[currentPoint.X]},{ data.To[currentPoint.Y]}] is equal");
                                PrintInList(listBox, $"horver {data.From[currentPoint.X]}*{ data.MoneyPath[currentPoint.Y, currentPoint.X]}={data.From[currentPoint.X] * data.MoneyPath[currentPoint.Y, currentPoint.X]} sum={sum}");
                                Perestanovka(data, currentPoint, pointDs, 2, dataGrid);
                                index[0]++;
                                index[1]++;
                            }
                            else
                            {
                                sum += data.To[currentPoint.Y] * data.MoneyPath[currentPoint.Y, currentPoint.X];
                                Debug.WriteLine($"hor {data.To[currentPoint.Y]}*{data.MoneyPath[currentPoint.Y, currentPoint.X]}={data.To[currentPoint.Y] * data.MoneyPath[currentPoint.Y, currentPoint.X]} sum={sum}");
                                PrintInList(listBox, $"min value [{data.From[currentPoint.X]},{ data.To[currentPoint.Y]}] is {data.To[currentPoint.Y]}");
                                PrintInList(listBox, $"hor {data.To[currentPoint.Y]}*{data.MoneyPath[currentPoint.Y, currentPoint.X]}={data.To[currentPoint.Y] * data.MoneyPath[currentPoint.Y, currentPoint.X]} sum={sum}");
                                Perestanovka(data, currentPoint, pointDs, 1, dataGrid);
                                index[0]++;
                            }
                            data.displayInGrid(dataGrid);
                            //return data;
                        }
                        else
                        {
                            //return null;
                            PrintInList(listBox, $"__THIS POINT JUST PROCESSED__");
                        }
                    }
                    else
                    {
                        PrintInList(listBox, $"__ITERATIONS BREAK__");
                        break;
                    }
                    
                }
                else
                {
                    //return null;
                    PrintInList(listBox, $"__POINT NOT FOUND__");
                }
                PrintInList(listBox, $"__END ITERATION__{i}");
                // PrintInList(listBox, $"");
            }


            await Task.Delay(1000);
            for (int i = 0; i < data.MoneyPath.GetLength(0) + 1; i++)
            {
                for (int j = 0; j < data.MoneyPath.GetLength(1) + 1; j++)
                {
                    data.ColorSetGrid(dataGrid, Windows.UI.Colors.White, i, j);
                    // dataGrid.Rows[i].Cells[j].Style.BackColor = Color.White;
                }
            }
            PrintInList(listBox, $"___END ALGORITHM___SUM={sum}");
            // PrintInList(listBox, $"");
            data.Sum = sum;
            // Message.InfoMessage($"End {sum}");
            //if (path == "")


            //StorageFolder folder = KnownFolders.DocumentsLibrary;
            //string path = folder.Path + "\\res.txt";

            //data.WriteFile(path);
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="currentPoint"></param>
        /// <param name="pointDs"></param>
        /// <param name="sign"></param>
        /// <param name="dataGrid"></param>
        static void Perestanovka(Data data, PointI currentPoint, List<PointI> pointDs, int sign, Grid dataGrid)
        {
            Debug.WriteLine($"start perestanovka {currentPoint.Y}:{currentPoint.X}");

            double value = 0;

            for (int i1 = 0; i1 < data.MoneyPath.GetLength(0) + 1; i1++)
            {
                for (int j = 0; j < data.MoneyPath.GetLength(1) + 1; j++)
                {
                    data.ColorSetGrid(dataGrid, Windows.UI.Colors.White, i1, j);
                    // dataGrid.Rows[i].Cells[j].Style.BackColor = Color.White;
                }
            }
            // gridView.Rows[currentPoint.Y].Cells[currentPoint.X].Style.BackColor = Color.Aqua;
            data.ColorSetGrid(dataGrid, Windows.UI.Colors.Red, currentPoint.Y, currentPoint.X);
            Debug.WriteLine($"_____perep____ {sign}");
            if (sign < 2)
            {
                Debug.WriteLine($"_____perepFor____ {sign}");
                for (int h = 0; h < data.MoneyPath.GetLength(sign); h++)
                {

                    // if(!istop.Contains(h) || !jstop.Contains(index[1]))
                    if (sign == 0)
                    {
                        if (!new PointI(currentPoint.X, h).ContainInList(pointDs))
                        {
                            Debug.WriteLine($"_____perepForVert____ {h}");
                            data.MoneyPath[h, currentPoint.X] = -1;
                            // gridView.Rows[h].Cells[currentPoint.X].Style.BackColor = Color.Yellow;
                            data.ColorSetGrid(dataGrid, Windows.UI.Colors.Aqua, h, currentPoint.X);
                            Debug.WriteLine($"pp {h} - {currentPoint.X}");
                            value = data.From[currentPoint.X];
                            Debug.WriteLine($"h {h} x {currentPoint.X}");
                        }

                    }
                    else if (sign == 1)
                    {
                        if (!new PointI(h, currentPoint.Y).ContainInList(pointDs))
                        {
                            Debug.WriteLine($"_____perepForHor____ {h}");
                            data.MoneyPath[currentPoint.Y, h] = -1;
                            //   gridView.Rows[currentPoint.Y].Cells[h].Style.BackColor = Color.Yellow;
                            data.ColorSetGrid(dataGrid, Windows.UI.Colors.Aqua, currentPoint.Y, h);
                            value = data.To[currentPoint.Y];
                            Debug.WriteLine($"y {currentPoint.Y} h {h} ");
                        }

                    }
                    else
                    {
                        Debug.WriteLine($"uppzas");
                    }
                }
            }
            else
            {
                for (int h = 0; h < data.MoneyPath.GetLength(0); h++)
                {
                    // if (!istop.Contains(h) || !jstop.Contains(index[1]))
                    if (!new PointI(currentPoint.X, h).ContainInList(pointDs))
                    {
                        data.MoneyPath[h, currentPoint.X] = -1;
                        // gridView.Rows[h].Cells[currentPoint.X].Style.BackColor = Color.Yellow;
                        data.ColorSetGrid(dataGrid, Windows.UI.Colors.Aqua, h, currentPoint.X);
                        Debug.WriteLine($"h {h} x {currentPoint.X}");
                    }
                }
                for (int h = 0; h < data.MoneyPath.GetLength(1); h++)
                {
                    // if (!istop.Contains(index[0]) || !jstop.Contains(h))
                    if (!new PointI(h, currentPoint.Y).ContainInList(pointDs))
                    {
                        data.MoneyPath[currentPoint.Y, h] = -1;
                        //  gridView.Rows[currentPoint.Y].Cells[h].Style.BackColor = Color.Yellow;
                        data.ColorSetGrid(dataGrid, Windows.UI.Colors.Aqua, currentPoint.Y, h);
                        Debug.WriteLine($"y {currentPoint.Y} h {h} ");
                    }
                }
                value = data.From[currentPoint.X];
            }
            data.MoneyPath[currentPoint.Y, currentPoint.X] = value;
            data.To[currentPoint.Y] -= value;
            data.From[currentPoint.X] -= value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async static Task<ContentDialogResult> DisplayDialog(string title, string content,bool isYesNo=false)
        {
            ContentDialog subscribeDialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "OK",
                
                DefaultButton = ContentDialogButton.Primary,

            };
            if (isYesNo)
            {
                subscribeDialog.CloseButtonText = String.Empty;
                subscribeDialog.PrimaryButtonText = "Да";
                subscribeDialog.SecondaryButtonText = "Нет";
            }
            ContentDialogResult result = await subscribeDialog.ShowAsync();
            return result;
        }
    }
}
