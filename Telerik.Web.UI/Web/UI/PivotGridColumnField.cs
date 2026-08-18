using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.PivotGrid.Core;

namespace Telerik.Web.UI
{
	// Token: 0x02000DA2 RID: 3490
	public class PivotGridColumnField : PivotGridGroupField, IPivotGridPropertyGroupField
	{
		// Token: 0x17002939 RID: 10553
		// (get) Token: 0x06008251 RID: 33361 RVA: 0x001DB6AB File Offset: 0x001D98AB
		// (set) Token: 0x06008252 RID: 33362 RVA: 0x001DB6B3 File Offset: 0x001D98B3
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(PivotGridCell), BindingDirection.OneWay)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[Description("RadPivotGrid Column Cell Template")]
		[Category("Layout")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual ITemplate CellTemplate
		{
			get
			{
				return this.cellTemplate;
			}
			set
			{
				this.cellTemplate = value;
			}
		}

		// Token: 0x1700293A RID: 10554
		// (get) Token: 0x06008253 RID: 33363 RVA: 0x001DB6BC File Offset: 0x001D98BC
		// (set) Token: 0x06008254 RID: 33364 RVA: 0x001DB6C4 File Offset: 0x001D98C4
		[NotifyParentProperty(true)]
		[Description("RadPivotGrid Column Total Header Cell Template property")]
		[Category("Layout")]
		[TemplateContainer(typeof(PivotGridCell), BindingDirection.OneWay)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual ITemplate TotalHeaderCellTemplate
		{
			get
			{
				return this.totalHeaderCellTemplate;
			}
			set
			{
				this.totalHeaderCellTemplate = value;
			}
		}

		// Token: 0x06008255 RID: 33365 RVA: 0x001DB6CD File Offset: 0x001D98CD
		public override IEnumerable<object> GetUniqueKeys(int level)
		{
			if (base.Owner != null && base.Owner.PivotModel.DataProvider != null)
			{
				return base.Owner.PivotModel.DataProvider.Results.GetUniqueKeys(PivotAxis.Columns, level);
			}
			return null;
		}

		// Token: 0x040023E4 RID: 9188
		private ITemplate cellTemplate;

		// Token: 0x040023E5 RID: 9189
		private ITemplate totalHeaderCellTemplate;
	}
}
