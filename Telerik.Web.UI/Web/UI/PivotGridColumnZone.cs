using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000C26 RID: 3110
	public class PivotGridColumnZone : PivotGridZone
	{
		// Token: 0x06007626 RID: 30246 RVA: 0x001B6FA5 File Offset: 0x001B51A5
		public PivotGridColumnZone(RadPivotGrid owner) : base(owner)
		{
		}

		// Token: 0x06007627 RID: 30247 RVA: 0x001B6FCC File Offset: 0x001B51CC
		public void Initialize()
		{
			if (base.OwnerPivotGrid.ShowColumnHeaderZone)
			{
				IOrderedEnumerable<PivotGridField> orderedEnumerable = from f in base.OwnerPivotGrid.Fields
				where f is PivotGridColumnField && !f.IsHidden
				orderby f.ZoneIndex
				select f;
				int num = orderedEnumerable.Count<PivotGridField>();
				WebControl webControl = base.CreateFieldsPopup(base.OwnerPivotGrid.FieldsPopupSettings.ColumnFieldsMinCount, num, base.Localization.ColumnGroupedFieldsTitle);
				foreach (PivotGridField pivotGridField in orderedEnumerable)
				{
					webControl.Controls.Add(pivotGridField.RenderingControl);
				}
				if (num == 0)
				{
					this.Controls.Add(new Literal
					{
						ID = "DropFieldHereLiteral",
						Text = base.OwnerPivotGrid.Fields.Owner.ColumnHeaderZoneText
					});
				}
			}
		}
	}
}
