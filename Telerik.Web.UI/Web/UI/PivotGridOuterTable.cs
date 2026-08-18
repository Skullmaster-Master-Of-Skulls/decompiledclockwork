using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000E25 RID: 3621
	internal class PivotGridOuterTable : PivotGridTable
	{
		// Token: 0x0600894E RID: 35150 RVA: 0x001F560A File Offset: 0x001F380A
		public PivotGridOuterTable(RadPivotGrid owner) : base(owner)
		{
			base.Owner = owner;
			this.ID = "OT";
		}

		// Token: 0x0600894F RID: 35151 RVA: 0x001F5644 File Offset: 0x001F3844
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			List<PivotGridField> list = (from f in base.Owner.Fields
			where f is PivotGridRowField && !f.IsHidden
			orderby f.ZoneIndex
			select f).ToList<PivotGridField>();
			foreach (PivotGridField pivotGridField in list)
			{
				if (!pivotGridField.CellStyle.Width.IsEmpty)
				{
					base.Owner.RowHeaderTableLayout = PivotGridTableLayout.Fixed;
				}
			}
			if (!base.Owner.RowHeaderCellStyle.Width.IsEmpty || base.Owner.RowHeaderTableLayout == PivotGridTableLayout.Fixed)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Style, "table-layout: fixed;");
			}
			if (!string.IsNullOrEmpty(base.Owner.AccessibilitySettings.OuterTableSummary))
			{
				writer.AddAttribute("summary", base.Owner.AccessibilitySettings.OuterTableSummary);
			}
		}

		// Token: 0x06008950 RID: 35152 RVA: 0x001F5770 File Offset: 0x001F3970
		protected override void RenderContents(HtmlTextWriter writer)
		{
			AccessibilityHelper.RenderCaption(writer, base.Owner, base.Owner.AccessibilitySettings.OuterTableCaption, "rpgOuterTableCaption", true);
			base.RenderContents(writer);
		}

		// Token: 0x06008951 RID: 35153 RVA: 0x001F579C File Offset: 0x001F399C
		protected override void Render(HtmlTextWriter writer)
		{
			if (!base.Owner.ClientSettings.Scrolling.AllowVerticalScroll)
			{
				bool flag = false;
				PivotGridTableRow pivotGridTableRow = null;
				for (int i = 0; i < this.Rows.Count; i++)
				{
					PivotGridRowHeaderItem pivotGridRowHeaderItem = this.Rows[i] as PivotGridRowHeaderItem;
					if (pivotGridRowHeaderItem != null)
					{
						pivotGridTableRow = pivotGridRowHeaderItem;
						if (!flag)
						{
							pivotGridRowHeaderItem.Attributes["id"] = HttpUtility.HtmlEncode(string.Format("{0}__{1}", this.ClientID, "fhRow"));
						}
						flag = true;
					}
				}
				if (pivotGridTableRow != null)
				{
					pivotGridTableRow.Attributes["id"] = HttpUtility.HtmlEncode(string.Format("{0}__{1}", this.ClientID, "lhRow"));
				}
			}
			base.Render(writer);
		}
	}
}
