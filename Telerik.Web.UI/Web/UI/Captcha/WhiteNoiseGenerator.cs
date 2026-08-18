using System;

namespace Telerik.Web.UI.Captcha
{
	// Token: 0x020000F9 RID: 249
	public class WhiteNoiseGenerator : INoiseGenerator
	{
		// Token: 0x06000A91 RID: 2705 RVA: 0x00025964 File Offset: 0x00023B64
		public byte[] GetNoiseData(int numberOfSamples, int bitsPerSample, byte loudness = 255)
		{
			byte[] array = new byte[numberOfSamples * bitsPerSample / 8];
			Random random = new Random();
			for (int i = 0; i < numberOfSamples * bitsPerSample / 8; i++)
			{
				array[i] = (byte)random.Next((int)loudness);
			}
			return array;
		}
	}
}
