using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200045D RID: 1117
	internal class DropDownTreeHeaderFooterControl : WebControl, INamingContainer
	{
		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06002860 RID: 10336 RVA: 0x000830F1 File Offset: 0x000812F1
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06002861 RID: 10337 RVA: 0x000830F5 File Offset: 0x000812F5
		// (set) Token: 0x06002862 RID: 10338 RVA: 0x000830FD File Offset: 0x000812FD
		private RadDropDownTree Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x00083106 File Offset: 0x00081306
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.Controls.Count > 0)
			{
				base.Render(writer);
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06002864 RID: 10340 RVA: 0x0008311D File Offset: 0x0008131D
		// (set) Token: 0x06002865 RID: 10341 RVA: 0x00083125 File Offset: 0x00081325
		internal bool TemplateInstantiated
		{
			get
			{
				return this._templateInstantiated;
			}
			set
			{
				this._templateInstantiated = value;
			}
		}

		// Token: 0x04000A33 RID: 2611
		private RadDropDownTree _owner;

		// Token: 0x04000A34 RID: 2612
		private bool _templateInstantiated;
	}
}
