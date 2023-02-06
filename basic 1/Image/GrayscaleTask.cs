namespace Recognizer
{
	public static class GrayscaleTask
	{
		public static double[,] ToGrayscale(Pixel[,] original)
		{
            var imageHeight = original.GetLength(0);
			var imageLength = original.GetLength(1);
            var grayscale = new double[imageHeight, imageLength];

            const double redColourCoef = 0.299;
            const double greenColourCoef = 0.587;
            const double blueColourCoef = 0.114;
			            
			for (int i = 0; i < imageHeight; i++)
				for (int j = 0; j < imageLength; j++)
					grayscale[i, j] = (redColourCoef * original[i, j].R + greenColourCoef * original[i, j].G
						+ blueColourCoef * original[i, j].B) / 255;

            return grayscale;
		}
	}
}