using System;
using System.Web.UI;

namespace TechnoPro.Common.UI.Web.Entity.Adapters
{
	// Token: 0x02000055 RID: 85
	public class MyStyleAttribute
	{
		// Token: 0x06000273 RID: 627 RVA: 0x00002221 File Offset: 0x00000421
		public MyStyleAttribute()
		{
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000056CC File Offset: 0x000038CC
		public MyStyleAttribute(HtmlTextWriterStyle styleTag, string val)
		{
			this.StyleTag = styleTag;
			this.Value = val;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000275 RID: 629 RVA: 0x000056E6 File Offset: 0x000038E6
		// (set) Token: 0x06000276 RID: 630 RVA: 0x000056EE File Offset: 0x000038EE
		public HtmlTextWriterStyle StyleTag { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000277 RID: 631 RVA: 0x000056F7 File Offset: 0x000038F7
		// (set) Token: 0x06000278 RID: 632 RVA: 0x000056FF File Offset: 0x000038FF
		public string Value { get; set; }
	}
}
