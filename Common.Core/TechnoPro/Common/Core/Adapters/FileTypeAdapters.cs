using System;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.Adapters
{
	// Token: 0x02000171 RID: 369
	public static class FileTypeAdapters
	{
		// Token: 0x0600103F RID: 4159 RVA: 0x00077C30 File Offset: 0x00075E30
		public static string GetFilenamePattern(this FileType fileType, int addressSize)
		{
			return (fileType.AddrSizeVersion && addressSize > 0) ? string.Format("{0}.x{1}.*.{2}", fileType.Title, addressSize, fileType.Extension) : string.Format("{0}.*.{1}", fileType.Title, fileType.Extension);
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00077C84 File Offset: 0x00075E84
		public static string GetSecondaryFilenamePattern(this FileType fileType, int addressSize)
		{
			return (fileType.AddrSizeVersion && addressSize > 0) ? string.Format("{0}.x{1}.*.{2}", fileType.SecondaryTitle, addressSize, fileType.Extension) : string.Format("{0}.*.{1}", fileType.SecondaryTitle, fileType.Extension);
		}
	}
}
