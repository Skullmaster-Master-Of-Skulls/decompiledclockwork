using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000C21 RID: 3105
	public class PivotGridZone : PivotGridCell
	{
		// Token: 0x06007617 RID: 30231 RVA: 0x001B6C08 File Offset: 0x001B4E08
		public PivotGridZone(RadPivotGrid ownerPivotGrid) : base(ownerPivotGrid)
		{
		}

		// Token: 0x17002668 RID: 9832
		// (get) Token: 0x06007618 RID: 30232 RVA: 0x001B6C11 File Offset: 0x001B4E11
		internal PivotGridStrings Localization
		{
			get
			{
				return base.OwnerPivotGrid.Localization;
			}
		}

		// Token: 0x06007619 RID: 30233 RVA: 0x001B6C20 File Offset: 0x001B4E20
		protected WebControl CreateFieldsPopup(int property, int fieldsCount, string groupedFieldsTitle)
		{
			if (property != 0 && property <= fieldsCount)
			{
				SpanPanel spanPanel = new SpanPanel();
				this.Controls.Add(spanPanel);
				spanPanel.ID = "SpanPanelFieldsPopup";
				spanPanel.CssClass = "rpgFieldsPopup";
				SpanPanel spanPanel2 = new SpanPanel();
				spanPanel.Controls.Add(spanPanel2);
				spanPanel2.CssClass = "rpgFieldsPopupWrapper";
				Label label = new Label();
				spanPanel.Controls.Add(label);
				if (base.OwnerPivotGrid.ResolvedRenderMode == RenderMode.Lightweight)
				{
					label.CssClass = "rpgIcon rpgGroupedFieldsTitleIcon";
				}
				else
				{
					label.CssClass = "rpgGroupedFieldsTitle";
				}
				label.Text = groupedFieldsTitle;
				return spanPanel2;
			}
			return this;
		}
	}
}
