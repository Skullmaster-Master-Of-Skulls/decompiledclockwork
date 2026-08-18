using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Menu
{
	// Token: 0x020005D0 RID: 1488
	public class MenuItemContentTemplateContainer : WebControl, IDataItemContainer, INamingContainer
	{
		// Token: 0x0600359B RID: 13723 RVA: 0x000B1FA4 File Offset: 0x000B01A4
		public MenuItemContentTemplateContainer(RadMenuItem owner)
		{
			this._owner = owner;
		}

		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x0600359C RID: 13724 RVA: 0x000B1FB3 File Offset: 0x000B01B3
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x000B1FB8 File Offset: 0x000B01B8
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this.CssClass;
			this.CssClass = string.Format("{2} {1} {0}", this.CssClass, "rmContentTemplate", "rmPopup");
			base.AddAttributesToRender(writer);
			this.CssClass = cssClass;
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x000B1FFA File Offset: 0x000B01FA
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.Controls.Count > 0)
			{
				base.Render(writer);
			}
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000B2011 File Offset: 0x000B0211
		protected virtual object GetDataItem()
		{
			return this._owner;
		}

		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x060035A0 RID: 13728 RVA: 0x000B2019 File Offset: 0x000B0219
		public object DataItem
		{
			get
			{
				return this.GetDataItem();
			}
		}

		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x060035A1 RID: 13729 RVA: 0x000B2021 File Offset: 0x000B0221
		public int DataItemIndex
		{
			get
			{
				return this._owner.Index;
			}
		}

		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x060035A2 RID: 13730 RVA: 0x000B202E File Offset: 0x000B022E
		public int DisplayIndex
		{
			get
			{
				return this._owner.Index;
			}
		}

		// Token: 0x04000E81 RID: 3713
		private RadMenuItem _owner;
	}
}
