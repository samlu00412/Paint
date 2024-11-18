using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    using System;
    using System.Numerics;

    internal class FFTProcessor
    {
        // 一維 FFT
        public static Complex[] FFT(Complex[] input)
        {
            int n = input.Length;

            // 基本情況
            if (n == 1)
                return new Complex[] { input[0] };

            // 必須為 2 的冪次方
            if ((n & (n - 1)) != 0)
                throw new ArgumentException("FFT 的輸入長度必須是 2 的冪次方");

            // 分成偶數和奇數
            Complex[] even = new Complex[n / 2];
            Complex[] odd = new Complex[n / 2];
            for (int i = 0; i < n / 2; i++)
            {
                even[i] = input[2 * i];
                odd[i] = input[2 * i + 1];
            }

            // 遞迴計算
            Complex[] fftEven = FFT(even);
            Complex[] fftOdd = FFT(odd);

            // 合併
            Complex[] result = new Complex[n];
            for (int k = 0; k < n / 2; k++)
            {
                Complex exp = Complex.FromPolarCoordinates(1, -2 * Math.PI * k / n) * fftOdd[k];
                result[k] = fftEven[k] + exp;
                result[k + n / 2] = fftEven[k] - exp;
            }

            return result;
        }

    }

}
