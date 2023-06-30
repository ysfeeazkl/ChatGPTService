using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tesseract;
using Image = System.Drawing.Image;

namespace ImageTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Image asd;
            var img = new Bitmap("test.png");

            var ocr = new TesseractEngine("./eng.traineddata", "eng");

            var sonuc = ocr.Process(img);

            //using (var bitmap = GetSubtitleBitmap(index))
            //using (var image = GetPix(bitmap))
            //using (var page = engine.Process(image))
            //{
            //    result = page.GetText();
            //    result = result?.Trim();
            //}


        }

        public static void ShowText(Bitmap bitmap)
        {
            
            //using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            //{
            //    using (var pix = new BitmapToPixConverter().Convert(bitmap))
            //    {
            //        using (var page = engine.Process(pix, PageSegMode.SingleLine))
            //        {
            //            textBox.Text = page.GetText().Trim();
            //        }
            //    }
            //}
        }
    }
}
