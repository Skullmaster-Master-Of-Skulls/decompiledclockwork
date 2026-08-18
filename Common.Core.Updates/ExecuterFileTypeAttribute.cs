using System;

namespace TechnoPro.Common.Core.Updates
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	internal class ExecuterFileTypeAttribute : Attribute
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000028E8 File Offset: 0x00000AE8
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000028F0 File Offset: 0x00000AF0
		public string FileType { get; set; }

		// Token: 0x0600002D RID: 45 RVA: 0x000028F9 File Offset: 0x00000AF9
		public ExecuterFileTypeAttribute(string fileType)
		{
			this.FileType = fileType;
		}
	}
}
