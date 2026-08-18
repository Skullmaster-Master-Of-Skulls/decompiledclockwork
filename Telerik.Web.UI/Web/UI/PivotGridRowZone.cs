using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000C2B RID: 3115
	public class PivotGridRowZone : PivotGridZone
	{
		// Token: 0x06007644 RID: 30276 RVA: 0x001B75AA File Offset: 0x001B57AA
		public PivotGridRowZone(RadPivotGrid owner) : base(owner)
		{
		}

		// Token: 0x06007645 RID: 30277 RVA: 0x001B75B4 File Offset: 0x001B57B4
		public void Initialize(PivotGridField field)
		{
			PivotGridGroupField pivotGridGroupField = field as PivotGridGroupField;
			if (pivotGridGroupField != null)
			{
				this.ColumnSpan = pivotGridGroupField.ColumnSpan;
			}
			this.Controls.Add(field.RenderingControl);
		}

		// Token: 0x06007646 RID: 30278 RVA: 0x001B75E8 File Offset: 0x001B57E8
		public void Initialize(List<PivotGridField> fields)
		{
			WebControl webControl = base.CreateFieldsPopup(base.OwnerPivotGrid.FieldsPopupSettings.RowFieldsMinCount, fields.Count, base.Localization.RowGroupedFieldsTitle);
			if (webControl == this)
			{
				webControl = new SpanPanel();
				this.Controls.Add(webControl);
			}
			foreach (PivotGridField pivotGridField in fields)
			{
				webControl.Controls.Add(pivotGridField.RenderingControl);
			}
		}
	}
}
