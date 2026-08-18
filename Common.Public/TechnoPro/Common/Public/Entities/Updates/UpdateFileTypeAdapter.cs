using System;
using TechnoPro.Common.Public.Adapters;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x02000141 RID: 321
	public static class UpdateFileTypeAdapter
	{
		// Token: 0x060007A5 RID: 1957 RVA: 0x000108D0 File Offset: 0x0000EAD0
		public static string GetTitle(this eUpdateFileTypes fileType)
		{
			return fileType.GetAttribute<UpdateFileTypeAttribute>().Title;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x000108F4 File Offset: 0x0000EAF4
		public static string GetExtension(this eUpdateFileTypes fileType)
		{
			return fileType.GetAttribute<UpdateFileTypeAttribute>().Extension;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00010918 File Offset: 0x0000EB18
		public static string GetDescription(this eUpdateFileTypes fileType)
		{
			return fileType.GetAttribute<UpdateFileTypeAttribute>().Description;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001093C File Offset: 0x0000EB3C
		public static bool GetAddSizeVersion(this eUpdateFileTypes fileType)
		{
			return fileType.GetAttribute<UpdateFileTypeAttribute>().AddSizeVersion;
		}
	}
}
