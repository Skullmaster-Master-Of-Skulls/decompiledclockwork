using System;
using NLog.Config;

namespace NLog.Layouts
{
	// Token: 0x02000115 RID: 277
	[NLogConfigurationItem]
	[ThreadAgnostic]
	public class JsonAttribute
	{
		// Token: 0x060007AB RID: 1963 RVA: 0x00010D04 File Offset: 0x0000EF04
		public JsonAttribute() : this(null, null, true)
		{
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00010D0F File Offset: 0x0000EF0F
		public JsonAttribute(string name, Layout layout) : this(name, layout, true)
		{
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00010D1A File Offset: 0x0000EF1A
		public JsonAttribute(string name, Layout layout, bool encode)
		{
			this.Name = name;
			this.Layout = layout;
			this.Encode = encode;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00010D37 File Offset: 0x0000EF37
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x00010D3F File Offset: 0x0000EF3F
		[RequiredParameter]
		public string Name { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00010D48 File Offset: 0x0000EF48
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x00010D50 File Offset: 0x0000EF50
		[RequiredParameter]
		public Layout Layout { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00010D59 File Offset: 0x0000EF59
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x00010D61 File Offset: 0x0000EF61
		public bool Encode { get; set; }
	}
}
