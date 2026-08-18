using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000619 RID: 1561
	public class NavigationNodeContentTemplateContainer : WebControl, INamingContainer
	{
		// Token: 0x0600388B RID: 14475 RVA: 0x000BA317 File Offset: 0x000B8517
		public NavigationNodeContentTemplateContainer(NavigationNode owner)
		{
			this._owner = owner;
		}

		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x0600388C RID: 14476 RVA: 0x000BA326 File Offset: 0x000B8526
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000BA32C File Offset: 0x000B852C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this.CssClass;
			this.CssClass = RadNavigation.Styles.Combine(new string[]
			{
				"radPopup rnvPopup",
				"rnvContentTemplate"
			});
			if (this._owner.Owner.Attributes["dir"] == "rtl")
			{
				this.CssClass = RadNavigation.Styles.Combine(new string[]
				{
					this.CssClass,
					"rnvPopup_rtl"
				});
			}
			base.AddAttributesToRender(writer);
			this.CssClass = cssClass;
		}

		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x0600388E RID: 14478 RVA: 0x000BA3BB File Offset: 0x000B85BB
		public Dictionary<string, object> TemplateData
		{
			get
			{
				return this._owner.TemplateData;
			}
		}

		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x0600388F RID: 14479 RVA: 0x000BA3C8 File Offset: 0x000B85C8
		public NavigationNode Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06003890 RID: 14480 RVA: 0x000BA3D0 File Offset: 0x000B85D0
		public string Text
		{
			get
			{
				return this._owner.Text;
			}
		}

		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06003891 RID: 14481 RVA: 0x000BA3DD File Offset: 0x000B85DD
		public string NavigateUrl
		{
			get
			{
				return this._owner.NavigateUrl;
			}
		}

		// Token: 0x04000F08 RID: 3848
		private NavigationNode _owner;
	}
}
