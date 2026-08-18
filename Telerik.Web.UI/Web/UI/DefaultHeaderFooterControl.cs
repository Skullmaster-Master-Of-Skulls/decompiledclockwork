using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000F81 RID: 3969
	internal class DefaultHeaderFooterControl : WebControl, INamingContainer
	{
		// Token: 0x17003017 RID: 12311
		// (get) Token: 0x0600980C RID: 38924 RVA: 0x00220B1E File Offset: 0x0021ED1E
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17003018 RID: 12312
		// (get) Token: 0x0600980D RID: 38925 RVA: 0x00220B22 File Offset: 0x0021ED22
		// (set) Token: 0x0600980E RID: 38926 RVA: 0x00220B2A File Offset: 0x0021ED2A
		private RadListBox Owner
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

		// Token: 0x0600980F RID: 38927 RVA: 0x00220B33 File Offset: 0x0021ED33
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.Controls.Count > 0)
			{
				base.Render(writer);
			}
		}

		// Token: 0x17003019 RID: 12313
		// (get) Token: 0x06009810 RID: 38928 RVA: 0x00220B4A File Offset: 0x0021ED4A
		// (set) Token: 0x06009811 RID: 38929 RVA: 0x00220B52 File Offset: 0x0021ED52
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

		// Token: 0x04002B74 RID: 11124
		private RadListBox _owner;

		// Token: 0x04002B75 RID: 11125
		private bool _templateInstantiated;
	}
}
