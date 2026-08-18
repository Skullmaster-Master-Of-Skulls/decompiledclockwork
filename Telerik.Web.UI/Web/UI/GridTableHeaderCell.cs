using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200117F RID: 4479
	public class GridTableHeaderCell : TableHeaderCell
	{
		// Token: 0x17003AFD RID: 15101
		// (get) Token: 0x0600B691 RID: 46737 RVA: 0x00283A60 File Offset: 0x00281C60
		// (set) Token: 0x0600B692 RID: 46738 RVA: 0x00283A68 File Offset: 0x00281C68
		public string HeaderID { get; set; }

		// Token: 0x17003AFE RID: 15102
		// (get) Token: 0x0600B693 RID: 46739 RVA: 0x00283A71 File Offset: 0x00281C71
		// (set) Token: 0x0600B694 RID: 46740 RVA: 0x00283A79 File Offset: 0x00281C79
		internal GridTableHeaderCell _parentHeaderCell { get; set; }

		// Token: 0x17003AFF RID: 15103
		// (get) Token: 0x0600B695 RID: 46741 RVA: 0x00283A82 File Offset: 0x00281C82
		public GridTableHeaderCell ParentHeaderCell
		{
			get
			{
				return this._parentHeaderCell;
			}
		}

		// Token: 0x0600B696 RID: 46742 RVA: 0x00283A8A File Offset: 0x00281C8A
		private void AddScopeAttribute(HtmlTextWriter writer)
		{
			writer.AddAttribute("scope", "col");
		}

		// Token: 0x0600B697 RID: 46743 RVA: 0x00283A9C File Offset: 0x00281C9C
		private void AddIDAttribute(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.HeaderID))
			{
				writer.AddAttribute("id", this.HeaderID);
			}
		}

		// Token: 0x0600B698 RID: 46744 RVA: 0x00283ABC File Offset: 0x00281CBC
		private void AddStyleAttributes(HtmlTextWriter writer)
		{
			if (base.ControlStyle is TableItemStyle && (base.ControlStyle as TableItemStyle).HorizontalAlign != HorizontalAlign.NotSet)
			{
				base.Style["text-align"] = (base.ControlStyle as TableItemStyle).HorizontalAlign.ToString().ToLower();
				(base.ControlStyle as TableItemStyle).HorizontalAlign = HorizontalAlign.NotSet;
			}
		}

		// Token: 0x0600B699 RID: 46745 RVA: 0x00283B28 File Offset: 0x00281D28
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.AddScopeAttribute(writer);
			this.AddStyleAttributes(writer);
			this.AddIDAttribute(writer);
			base.AddAttributesToRender(writer);
		}
	}
}
