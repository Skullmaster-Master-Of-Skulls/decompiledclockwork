using System;

namespace TechnoPro.Common.Public.Entities.Files
{
	// Token: 0x02000339 RID: 825
	public class FileFormatAttribute : Attribute
	{
		// Token: 0x060019C7 RID: 6599 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public FileFormatAttribute()
		{
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0001E1E1 File Offset: 0x0001C3E1
		public FileFormatAttribute(string extension)
		{
			this.Extension = extension;
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x0001E1F3 File Offset: 0x0001C3F3
		// (set) Token: 0x060019CA RID: 6602 RVA: 0x0001E1FB File Offset: 0x0001C3FB
		public string Extension { get; set; }
	}
}
