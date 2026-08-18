using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000C29 RID: 3113
	public class PivotGridFilterZone : PivotGridZone
	{
		// Token: 0x0600763D RID: 30269 RVA: 0x001B7322 File Offset: 0x001B5522
		public PivotGridFilterZone(RadPivotGrid owner) : base(owner)
		{
		}

		// Token: 0x0600763E RID: 30270 RVA: 0x001B7340 File Offset: 0x001B5540
		public void Initialize()
		{
			IEnumerable<PivotGridField> enumerable = from f in base.OwnerPivotGrid.Fields
			where f is PivotGridReportFilterField && !f.IsHidden
			select f;
			int num = enumerable.Count<PivotGridField>();
			WebControl webControl = base.CreateFieldsPopup(base.OwnerPivotGrid.FieldsPopupSettings.FilterFieldsMinCount, num, base.Localization.FilterGroupedFieldsTitle);
			foreach (PivotGridField pivotGridField in enumerable)
			{
				webControl.Controls.Add(pivotGridField.RenderingControl);
			}
			if (num == 0)
			{
				this.Controls.Add(new Literal
				{
					ID = "DropFieldHereLiteral",
					Text = base.OwnerPivotGrid.Fields.Owner.FilterHeaderZoneText
				});
			}
		}
	}
}
