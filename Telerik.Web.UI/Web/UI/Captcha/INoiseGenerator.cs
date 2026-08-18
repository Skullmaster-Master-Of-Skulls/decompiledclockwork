using System;

namespace Telerik.Web.UI.Captcha
{
	// Token: 0x020000F7 RID: 247
	internal interface INoiseGenerator
	{
		// Token: 0x06000A7E RID: 2686
		byte[] GetNoiseData(int numberOfSamples, int bitsPerSample, byte loudness);
	}
}
