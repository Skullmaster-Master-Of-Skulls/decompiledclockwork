using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.PivotGrid.Core;

namespace Telerik.Web.UI
{
	// Token: 0x02000DAC RID: 3500
	public class PivotGridRowField : PivotGridGroupField, IPivotGridPropertyGroupField
	{
		// Token: 0x17002957 RID: 10583
		// (get) Token: 0x060082C8 RID: 33480 RVA: 0x001DCF98 File Offset: 0x001DB198
		// (set) Token: 0x060082C9 RID: 33481 RVA: 0x001DCFA0 File Offset: 0x001DB1A0
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Layout")]
		[NotifyParentProperty(true)]
		[Description("RadPivotGrid Row Cell Template")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TemplateContainer(typeof(PivotGridCell), BindingDirection.OneWay)]
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

		// Token: 0x17002958 RID: 10584
		// (get) Token: 0x060082CA RID: 33482 RVA: 0x001DCFA9 File Offset: 0x001DB1A9
		// (set) Token: 0x060082CB RID: 33483 RVA: 0x001DCFB1 File Offset: 0x001DB1B1
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TemplateContainer(typeof(PivotGridCell), BindingDirection.OneWay)]
		[NotifyParentProperty(true)]
		[Description("RadPivotGrid Row Total Header Cell Template property")]
		[Category("Layout")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x060082CC RID: 33484 RVA: 0x001DCFBA File Offset: 0x001DB1BA
		public override IEnumerable<object> GetUniqueKeys(int level)
		{
			if (base.Owner != null && base.Owner.PivotModel.DataProvider != null)
			{
				return base.Owner.PivotModel.DataProvider.Results.GetUniqueKeys(PivotAxis.Rows, level);
			}
			return null;
		}

		// Token: 0x0400240F RID: 9231
		private ITemplate cellTemplate;

		// Token: 0x04002410 RID: 9232
		private ITemplate totalHeaderCellTemplate;
	}
}
