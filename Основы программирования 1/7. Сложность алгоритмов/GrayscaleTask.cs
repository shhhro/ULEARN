using System.Windows.Forms;

namespace Recognizer
{
	public static class GrayscaleTask
	{
		public static double RCoefficient = 0.299;
        public static double GCoefficient = 0.587;
        public static double BCoefficient = 0.114;
		public static int RGBValue = 255;
        public static double[,] ToGrayscale(Pixel[,] original)
		{
			var widthOfImage = original.GetLength(0);
			var heightOfImage = original.GetLength(1);
			return MakeAllPixelsGray(original, widthOfImage, heightOfImage) ;
		}
		public static double[,] MakeAllPixelsGray(Pixel[,] originalImage, int lengthX, int lengthY)
		{
            var grayscaleImage = new double[lengthX, lengthY];
            Pixel pixel;
            for (int x = 0; x < lengthX; x++)
                for (int y = 0; y < lengthY; y++)
				{
                    pixel = originalImage[x, y];
                    grayscaleImage[x, y] = (RCoefficient * pixel.R + GCoefficient * pixel.G + BCoefficient * pixel.B) / RGBValue;
				}
			return grayscaleImage;
		}
	} 
}