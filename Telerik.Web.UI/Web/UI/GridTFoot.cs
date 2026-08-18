using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200114B RID: 4427
	public class GridTFoot : GridMultiRowItem
	{
		// Token: 0x0600B44C RID: 46156 RVA: 0x00277364 File Offset: 0x00275564
		public GridTFoot(GridTableView ownerTableView) : base(ownerTableView)
		{
			this.SetItemType(GridItemType.TFoot);
		}

		// Token: 0x0600B44D RID: 46157 RVA: 0x00277374 File Offset: 0x00275574
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Tfoot);
		}

		// Token: 0x0600B44E RID: 46158 RVA: 0x0027737E File Offset: 0x0027557E
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
		}
	}
}
