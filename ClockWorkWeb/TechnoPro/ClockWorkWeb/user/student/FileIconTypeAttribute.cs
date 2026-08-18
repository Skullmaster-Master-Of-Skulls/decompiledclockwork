using System;

namespace TechnoPro.ClockWorkWeb.user.student
{
	// Token: 0x0200007E RID: 126
	public class FileIconTypeAttribute : Attribute
	{
		// Token: 0x06000472 RID: 1138 RVA: 0x0002077F File Offset: 0x0001E97F
		public FileIconTypeAttribute()
		{
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00020789 File Offset: 0x0001E989
		public FileIconTypeAttribute(string cssClass, params string[] fileExtensions)
		{
			this.CssClass = cssClass;
			this.FileExtensions = fileExtensions;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x000207A3 File Offset: 0x0001E9A3
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x000207AB File Offset: 0x0001E9AB
		public string CssClass { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x000207B4 File Offset: 0x0001E9B4
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x000207BC File Offset: 0x0001E9BC
		public string[] FileExtensions { get; set; }
	}
}
