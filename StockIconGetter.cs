using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FileExercise
{
    class StockIconGetter
    {
        private static Icon folderIcon;

        private static Icon FolderLarge => folderIcon ?? (folderIcon = GetStockIcon(SHSIID_FOLDER, SHGSI_SMALLICON));

        private static Icon GetStockIcon(uint type, uint size)
        {
            var info = new SHSTOCKICONINFO();
            info.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(info);

            SHGetStockIconInfo(type, SHGSI_ICON | size, ref info);

            var icon = (Icon)System.Drawing.Icon.FromHandle(info.hIcon).Clone(); // Get a copy that doesn't use the original handle
            DestroyIcon(info.hIcon); // Clean up native icon to prevent resource leak

            return icon;
        }

        public static System.Windows.Controls.Image GetImage()
        {
            var bitmap = FolderLarge.ToBitmap();
            BitmapImage bitmapImage;
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }
            var myImage = new System.Windows.Controls.Image();
            myImage.Source = bitmapImage;
            myImage.Width = bitmap.Width;
            myImage.Height = bitmap.Height;

            return myImage;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SHSTOCKICONINFO
        {
            public uint cbSize;
            public IntPtr hIcon;
            public int iSysIconIndex;
            public int iIcon;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szPath;
        }

        [DllImport("shell32.dll")]
        public static extern int SHGetStockIconInfo(uint siid, uint uFlags, ref SHSTOCKICONINFO psii);

        [DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr handle);

        private const uint SHSIID_FOLDER = 0x3;
        private const uint SHGSI_ICON = 0x100;
        private const uint SHGSI_LARGEICON = 0x0;
        private const uint SHGSI_SMALLICON = 0x1;
    }
}
