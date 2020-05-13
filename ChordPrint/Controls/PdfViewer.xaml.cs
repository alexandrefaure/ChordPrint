using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChordPrint.Controls
{
    /// <summary>
    /// Interaction logic for PdfViewer.xaml
    /// </summary>
    public partial class PdfViewer : UserControl
    {

        #region Bindable Properties

        public string PdfPath
        {
            get { return (string)GetValue(PdfPathProperty); }
            set { SetValue(PdfPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PdfPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PdfPathProperty =
            DependencyProperty.Register("PdfPath", typeof(string), typeof(PdfViewer), new PropertyMetadata(null, propertyChangedCallback: OnPdfPathChanged));

        private static void OnPdfPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pdfDrawer = (PdfViewer)d;

            if (!string.IsNullOrEmpty(pdfDrawer.PdfPath))
            {
                //making sure it's an absolute path
                var path = System.IO.Path.GetFullPath(pdfDrawer.PdfPath);

                //Windows.Storage.StorageFile.GetFileFromPathAsync(path).AsTask()
                //  //load pdf document on background thread
                //  .ContinueWith(t => Windows.Data.Pdf.PdfDocument.LoadFromFileAsync(t.Result).AsTask()).Unwrap()
                //  //display on UI Thread
                //  .ContinueWith(t2 => PdfToImages(pdfDrawer, t2.Result), TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                // Clear the current PDF UI if path is not specified
                var items = pdfDrawer.PagesContainer.Items;
                items.Clear();
            }
        }

        #endregion


        public PdfViewer()
        {
            InitializeComponent();
        }

        //private async static Task PdfToImages(PdfViewer pdfViewer, Windows.Data.Pdf.PdfDocument pdfDoc)
        //{
        //    var items = pdfViewer.PagesContainer.Items;
        //    items.Clear();

        //    if (pdfDoc == null) return;

        //    for (uint i = 0; i < pdfDoc.PageCount; i++)
        //    {
        //        using (var page = pdfDoc.GetPage(i))
        //        {
        //            var bitmap = await PageToBitmapAsync(page);
        //            var image = new Image
        //            {
        //                Source = bitmap,
        //                HorizontalAlignment = HorizontalAlignment.Center,
        //                Margin = new Thickness(0, 4, 0, 4),
        //                MaxWidth = 800
        //            };
        //            items.Add(image);
        //        }
        //    }
        //}

        //private static async Task<BitmapImage> PageToBitmapAsync(Windows.Data.Pdf.PdfPage page)
        //{
        //    BitmapImage image = new BitmapImage();

        //    using (var stream = new InMemoryRandomAccessStream())
        //    {
        //        await page.RenderToStreamAsync(stream);

        //        image.BeginInit();
        //        image.CacheOption = BitmapCacheOption.OnLoad;
        //        image.StreamSource = stream.AsStream();
        //        image.EndInit();
        //    }

        //    return image;
        //}

    }
}
