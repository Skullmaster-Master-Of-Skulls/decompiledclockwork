using System;
using System.Globalization;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200084C RID: 2124
	internal static class PipeError
	{
		// Token: 0x06004F82 RID: 20354 RVA: 0x00122CC4 File Offset: 0x00120EC4
		public static string GetErrorString(int error)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			if (UnsafeNativeMethods.FormatMessage(12800, IntPtr.Zero, error, CultureInfo.CurrentCulture.LCID, stringBuilder, stringBuilder.Capacity, IntPtr.Zero) != 0)
			{
				stringBuilder = stringBuilder.Replace("\n", "");
				stringBuilder = stringBuilder.Replace("\r", "");
				return SR.GetString("PipeKnownWin32Error", new object[]
				{
					stringBuilder.ToString(),
					error.ToString(CultureInfo.InvariantCulture),
					Convert.ToString(error, 16)
				});
			}
			return SR.GetString("PipeUnknownWin32Error", new object[]
			{
				error.ToString(CultureInfo.InvariantCulture),
				Convert.ToString(error, 16)
			});
		}
	}
}
