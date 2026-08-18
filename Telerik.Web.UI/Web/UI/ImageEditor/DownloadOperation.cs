using System;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000E8B RID: 3723
	internal static class DownloadOperation
	{
		// Token: 0x06008D22 RID: 36130 RVA: 0x00200A84 File Offset: 0x001FEC84
		private static bool IsDefault(string downloadKey)
		{
			return string.IsNullOrEmpty(downloadKey);
		}

		// Token: 0x06008D23 RID: 36131 RVA: 0x00200A8C File Offset: 0x001FEC8C
		internal static bool IsFromImageProvider(string downloadKey)
		{
			return DownloadOperation.IsDefault(downloadKey) || downloadKey == "1";
		}

		// Token: 0x06008D24 RID: 36132 RVA: 0x00200AA3 File Offset: 0x001FECA3
		internal static bool IsFromCanvas(string downloadKey)
		{
			return downloadKey == "2";
		}

		// Token: 0x06008D25 RID: 36133 RVA: 0x00200AB0 File Offset: 0x001FECB0
		internal static bool IsCustom(string downloadKey)
		{
			return !DownloadOperation.IsFromImageProvider(downloadKey) && !DownloadOperation.IsFromCanvas(downloadKey);
		}

		// Token: 0x0400279E RID: 10142
		internal const string FROM_PROVIDER = "1";

		// Token: 0x0400279F RID: 10143
		internal const string FROM_CANVAS = "2";
	}
}
