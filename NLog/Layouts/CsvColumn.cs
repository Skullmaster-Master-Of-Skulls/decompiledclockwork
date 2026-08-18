using System;
using NLog.Config;

namespace NLog.Layouts
{
	// Token: 0x0200010E RID: 270
	[NLogConfigurationItem]
	[ThreadAgnostic]
	public class CsvColumn
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x00010773 File Offset: 0x0000E973
		public CsvColumn() : this(null, null)
		{
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001077D File Offset: 0x0000E97D
		public CsvColumn(string name, Layout layout)
		{
			this.Name = name;
			this.Layout = layout;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00010793 File Offset: 0x0000E993
		// (set) Token: 0x0600077E RID: 1918 RVA: 0x0001079B File Offset: 0x0000E99B
		public string Name { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x000107A4 File Offset: 0x0000E9A4
		// (set) Token: 0x06000780 RID: 1920 RVA: 0x000107AC File Offset: 0x0000E9AC
		[RequiredParameter]
		public Layout Layout { get; set; }
	}
}
