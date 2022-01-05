using application;
using core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace TransportSolution
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Log.I().write("start application");
            
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
        Data data;
        Data dataOrig;
        private async void ButOpen_Click(object sender, RoutedEventArgs e)
        {
            // Запускаем видимость прогресс бара
            progress.Visibility = Visibility.Visible;

            // Выполняем действия
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".txt");
            
            // Среди действий обязательно должен быть код с await
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                try
                {
                    this.pathText.Text = file.Path;
                    data = await Data.ReadFile(file);
                    dataOrig = (Data)data.Clone();
                    if (data != null)
                    {
                        data.displayInGrid(gridData);
                        //await Algorithms.DisplayDialog("Message", data.To[0].ToString());
                    }
                }
                catch(Exception error)
                {
                        await Algorithms.DisplayDialog("Error", error.Message+"\nПопробуйте выбрать другой файл");
                }
                // Application now has read/write access to the picked file

            }
           
            // в самом конце скрываем кольцо загрузки
            progress.Visibility = Visibility.Collapsed;
        }

        void ButtonsToggle()
        {
            ButOpen.IsEnabled = !ButOpen.IsEnabled;
            ButExecute.IsEnabled = !ButExecute.IsEnabled;
        }

        private  void ButExecute_Click(object sender, RoutedEventArgs e)
        {
            if (data != null)
            {
                if (data.Sum != 0)
                {
                    data = (Data)dataOrig.Clone();
                    data.displayInGrid(gridData);
                }
                exec();
            }
        }

        async void exec()
        {
            ButtonsToggle();
            if (selector.SelectedIndex == 0)
            {
                await Algorithms.SZVal(data, gridData, list);
            }
            else if (selector.SelectedIndex == 1)
            {
                await Algorithms.MinVal(data, gridData, list);
            }else if (selector.SelectedIndex == 2)
            {

                await Algorithms.FogVal(data, gridData, list);
                //Algorithms.FogArray(data.MoneyPath);
            }
            ContentDialogResult result = await Algorithms.DisplayDialog("Result", "Ответ:"+data.Sum.ToString()+"\nВы хотите сохранить опорный план и ответ?",true);
            if (result == ContentDialogResult.Primary)
            {
                progress.Visibility = Visibility.Visible;
                

                var savePicker = new FileSavePicker();
                // место для сохранения по умолчанию
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                // устанавливаем типы файлов для сохранения
                savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
                // устанавливаем имя нового файла по умолчанию
                savePicker.SuggestedFileName = "New Document";
                savePicker.CommitButtonText = "Сохранить";

                var new_file = await savePicker.PickSaveFileAsync();
                if (new_file != null)
                {
                    await data.WriteFileAync(new_file);
                    //await FileIO.WriteTextAsync(new_file, myTextBox.Text);
                }
                progress.Visibility = Visibility.Collapsed;
            }
            ButtonsToggle();
            
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings));
        }
    }



    
}
