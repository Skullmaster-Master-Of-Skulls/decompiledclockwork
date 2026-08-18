using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000C22 RID: 3106
	public class PivotGridAggregateZone : PivotGridZone
	{
		// Token: 0x0600761A RID: 30234 RVA: 0x001B6CC1 File Offset: 0x001B4EC1
		public PivotGridAggregateZone(RadPivotGrid owner) : base(owner)
		{
		}

		// Token: 0x0600761B RID: 30235 RVA: 0x001B6CE8 File Offset: 0x001B4EE8
		public void Initialize()
		{
			if (base.OwnerPivotGrid.ShowDataHeaderZone)
			{
				IOrderedEnumerable<PivotGridField> orderedEnumerable = from f in base.OwnerPivotGrid.Fields
				where f is PivotGridAggregateField && !f.IsHidden
				orderby f.ZoneIndex
				select f;
				int num = orderedEnumerable.Count<PivotGridField>();
				WebControl webControl = base.CreateFieldsPopup(base.OwnerPivotGrid.FieldsPopupSettings.AggregateFieldsMinCount, num, base.Localization.AggregateGroupedFieldsTitle);
				foreach (PivotGridField pivotGridField in orderedEnumerable)
				{
					webControl.Controls.Add(pivotGridField.RenderingControl);
				}
				if (num == 0)
				{
					this.Controls.Add(new Literal
					{
						ID = "DropFieldHereLiteral",
						Text = base.OwnerPivotGrid.Fields.Owner.DataHeaderZoneText
					});
				}
			}
		}
	}
}
