using System;
using System.ComponentModel;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009DD RID: 2525
	internal static class CompressionFormatHelper
	{
		// Token: 0x060063C3 RID: 25539 RVA: 0x00174ACC File Offset: 0x00172CCC
		public static void Validate(CompressionFormat value)
		{
			if (!CompressionFormatHelper.IsDefined(value))
			{
				throw FxTrace.Exception.AsError(new InvalidEnumArgumentException("value", (int)value, typeof(CompressionFormat)));
			}
		}

		// Token: 0x060063C4 RID: 25540 RVA: 0x00174AF6 File Offset: 0x00172CF6
		internal static bool IsDefined(CompressionFormat value)
		{
			return value == CompressionFormat.None || value == CompressionFormat.Deflate || value == CompressionFormat.GZip;
		}
	}
}
