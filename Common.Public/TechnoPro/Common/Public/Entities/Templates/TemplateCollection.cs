using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Templates
{
	// Token: 0x02000172 RID: 370
	public class TemplateCollection
	{
		// Token: 0x060008F2 RID: 2290 RVA: 0x000126D0 File Offset: 0x000108D0
		public TemplateCollection()
		{
			this.Templates = new List<Template>();
			this.Groups = new List<TemplateGroup>();
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x000126F2 File Offset: 0x000108F2
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x000126FA File Offset: 0x000108FA
		public IList<Template> Templates { get; set; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00012703 File Offset: 0x00010903
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x0001270B File Offset: 0x0001090B
		public IList<TemplateGroup> Groups { get; set; }
	}
}
