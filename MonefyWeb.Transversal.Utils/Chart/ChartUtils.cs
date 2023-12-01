using MonefyWeb.Transversal.Utils.Chart;

namespace MonefyWeb.Transversal.Utils
{
    public class ChartUtils : IChartUtils
    {
        public string GenerateRandomColor()
        {
            byte[] colorBytes = new byte[3];
            Random random = new Random();
            random.NextBytes(colorBytes);

            int brightnessThreshold = 384;
            while (colorBytes[0] + colorBytes[1] + colorBytes[2] < brightnessThreshold)
            {
                random.NextBytes(colorBytes);
            }

            string randomColor = $"#{colorBytes[0]:X2}{colorBytes[1]:X2}{colorBytes[2]:X2}";

            return randomColor;
        }
    }
}
