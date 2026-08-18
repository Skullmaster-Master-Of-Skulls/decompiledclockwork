using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000DF4 RID: 3572
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class PivotGridFieldsPopupSettings : StateManager
	{
		// Token: 0x060084B3 RID: 33971 RVA: 0x001E4690 File Offset: 0x001E2890
		public PivotGridFieldsPopupSettings(RadPivotGrid owner)
		{
			this.owner = owner;
		}

		// Token: 0x170029F7 RID: 10743
		// (get) Token: 0x060084B4 RID: 33972 RVA: 0x001E46A0 File Offset: 0x001E28A0
		// (set) Token: 0x060084B5 RID: 33973 RVA: 0x001E46C9 File Offset: 0x001E28C9
		[DefaultValue(0)]
		[Category("Client")]
		[Description("The minimum amount of row fields that should the RadPivotGrid Row zone contain in order for the popup to appear.")]
		[NotifyParentProperty(true)]
		public int RowFieldsMinCount
		{
			get
			{
				object obj = base.ViewState["RowFieldsMinCount"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new PivotGridException("The RowFieldsMinCount value should be 0 or greater");
				}
				base.ViewState["RowFieldsMinCount"] = value;
			}
		}

		// Token: 0x170029F8 RID: 10744
		// (get) Token: 0x060084B6 RID: 33974 RVA: 0x001E46F0 File Offset: 0x001E28F0
		// (set) Token: 0x060084B7 RID: 33975 RVA: 0x001E4719 File Offset: 0x001E2919
		[Category("Client")]
		[DefaultValue(0)]
		[Description("The minimum amount of column fields that should the RadPivotGrid Column zone contain in order for the popup to appear.")]
		[NotifyParentProperty(true)]
		public int ColumnFieldsMinCount
		{
			get
			{
				object obj = base.ViewState["ColumnFieldsMinCount"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new PivotGridException("The ColumnFieldsMinCount value should be 0 or greater");
				}
				base.ViewState["ColumnFieldsMinCount"] = value;
			}
		}

		// Token: 0x170029F9 RID: 10745
		// (get) Token: 0x060084B8 RID: 33976 RVA: 0x001E4740 File Offset: 0x001E2940
		// (set) Token: 0x060084B9 RID: 33977 RVA: 0x001E4769 File Offset: 0x001E2969
		[Category("Client")]
		[DefaultValue(0)]
		[NotifyParentProperty(true)]
		[Description("The minimum amount of aggregate fields that should the RadPivotGrid Aggregate zone containin order for the popup to appear.")]
		public int AggregateFieldsMinCount
		{
			get
			{
				object obj = base.ViewState["AggregateFieldsMinCount"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new PivotGridException("The AggregateFieldsMinCount value should be 0 or greater");
				}
				base.ViewState["AggregateFieldsMinCount"] = value;
			}
		}

		// Token: 0x170029FA RID: 10746
		// (get) Token: 0x060084BA RID: 33978 RVA: 0x001E4790 File Offset: 0x001E2990
		// (set) Token: 0x060084BB RID: 33979 RVA: 0x001E47B9 File Offset: 0x001E29B9
		[DefaultValue(0)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[Description("The minimum amount of filter fields that should the RadPivotGrid Filter zone contain in order for the popup to appear.")]
		public int FilterFieldsMinCount
		{
			get
			{
				object obj = base.ViewState["FilterFieldsMinCount"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new PivotGridException("The FilterFieldsMinCount value should be 0 or greater");
				}
				base.ViewState["FilterFieldsMinCount"] = value;
			}
		}

		// Token: 0x040024F7 RID: 9463
		private readonly RadPivotGrid owner;
	}
}
