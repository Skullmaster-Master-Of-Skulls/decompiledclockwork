using System;

namespace TechnoPro.Common.Public.Entities.Templates
{
	// Token: 0x02000174 RID: 372
	public class TemplateOrGroup
	{
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0001281F File Offset: 0x00010A1F
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x00012827 File Offset: 0x00010A27
		public Template Template { get; set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00012830 File Offset: 0x00010A30
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x00012838 File Offset: 0x00010A38
		public TemplateGroup Group { get; set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00012844 File Offset: 0x00010A44
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x0001285C File Offset: 0x00010A5C
		public virtual Template Item
		{
			get
			{
				return this.Template;
			}
			set
			{
				this.Template = value;
			}
		}
	}
}
