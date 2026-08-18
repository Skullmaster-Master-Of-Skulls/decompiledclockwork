using System;

namespace System.Web.UI
{
	// Token: 0x02000318 RID: 792
	public class TemplatePropertyEntry : BuilderPropertyEntry
	{
		// Token: 0x06002500 RID: 9472 RVA: 0x0005752A File Offset: 0x0005572A
		internal TemplatePropertyEntry()
		{
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x0007A557 File Offset: 0x00078757
		internal TemplatePropertyEntry(bool bindableTemplate)
		{
			this._bindableTemplate = bindableTemplate;
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06002502 RID: 9474 RVA: 0x0007A566 File Offset: 0x00078766
		internal bool IsMultiple
		{
			get
			{
				return Util.IsMultiInstanceTemplateProperty(base.PropertyInfo);
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06002503 RID: 9475 RVA: 0x0007A573 File Offset: 0x00078773
		public bool BindableTemplate
		{
			get
			{
				return this._bindableTemplate;
			}
		}

		// Token: 0x04001D60 RID: 7520
		private bool _bindableTemplate;
	}
}
