using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001814 RID: 6164
	[ToolboxItem(false)]
	internal class RadComboBoxHeaderFooterControl : WebControl, INamingContainer
	{
		// Token: 0x1700489F RID: 18591
		// (get) Token: 0x0600F010 RID: 61456 RVA: 0x0036A24B File Offset: 0x0036844B
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x170048A0 RID: 18592
		// (get) Token: 0x0600F011 RID: 61457 RVA: 0x0036A24F File Offset: 0x0036844F
		// (set) Token: 0x0600F012 RID: 61458 RVA: 0x0036A257 File Offset: 0x00368457
		private RadComboBox Owner
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

		// Token: 0x0600F013 RID: 61459 RVA: 0x0036A260 File Offset: 0x00368460
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.Controls.Count > 0)
			{
				base.Render(writer);
			}
		}

		// Token: 0x170048A1 RID: 18593
		// (get) Token: 0x0600F014 RID: 61460 RVA: 0x0036A277 File Offset: 0x00368477
		// (set) Token: 0x0600F015 RID: 61461 RVA: 0x0036A27F File Offset: 0x0036847F
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

		// Token: 0x04004533 RID: 17715
		private RadComboBox _owner;

		// Token: 0x04004534 RID: 17716
		private bool _templateInstantiated;
	}
}
