using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Internal;

namespace Telerik.Web.UI
{
	// Token: 0x02000DAB RID: 3499
	public class PivotGridReportFilterField : PivotGridField
	{
		// Token: 0x060082BA RID: 33466 RVA: 0x001DCE00 File Offset: 0x001DB000
		protected override void OnDescriptionInfoChanged()
		{
		}

		// Token: 0x17002951 RID: 10577
		// (get) Token: 0x060082BB RID: 33467 RVA: 0x001DCE02 File Offset: 0x001DB002
		// (set) Token: 0x060082BC RID: 33468 RVA: 0x001DCE0A File Offset: 0x001DB00A
		public FilterDescription FilterDescription
		{
			get
			{
				return this.filterDescription;
			}
			set
			{
				this.filterDescription = value;
				this.OnDescriptionInfoChanged();
			}
		}

		// Token: 0x17002952 RID: 10578
		// (get) Token: 0x060082BD RID: 33469 RVA: 0x001DCE19 File Offset: 0x001DB019
		// (set) Token: 0x060082BE RID: 33470 RVA: 0x001DCE21 File Offset: 0x001DB021
		public int FilterDescriptionIndex { get; set; }

		// Token: 0x17002953 RID: 10579
		// (get) Token: 0x060082BF RID: 33471 RVA: 0x001DCE2A File Offset: 0x001DB02A
		// (set) Token: 0x060082C0 RID: 33472 RVA: 0x001DCE55 File Offset: 0x001DB055
		[Category("Data")]
		[DefaultValue(PivotGridReportFilterActionType.Includes)]
		[NotifyParentProperty(true)]
		public PivotGridReportFilterActionType FilterType
		{
			get
			{
				if (base.ViewState["FilterType"] == null)
				{
					return PivotGridReportFilterActionType.Includes;
				}
				return (PivotGridReportFilterActionType)base.ViewState["FilterType"];
			}
			set
			{
				base.ViewState["FilterType"] = value;
			}
		}

		// Token: 0x17002954 RID: 10580
		// (get) Token: 0x060082C1 RID: 33473 RVA: 0x001DCE6D File Offset: 0x001DB06D
		// (set) Token: 0x060082C2 RID: 33474 RVA: 0x001DCE9D File Offset: 0x001DB09D
		[DefaultValue("")]
		[Category("Data")]
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] FilterValues
		{
			get
			{
				if (base.ViewState["FilterValues"] == null)
				{
					return new string[0];
				}
				return (string[])base.ViewState["FilterValues"];
			}
			set
			{
				base.ViewState["FilterValues"] = value;
			}
		}

		// Token: 0x17002955 RID: 10581
		// (get) Token: 0x060082C3 RID: 33475 RVA: 0x001DCEB0 File Offset: 0x001DB0B0
		// (set) Token: 0x060082C4 RID: 33476 RVA: 0x001DCEE2 File Offset: 0x001DB0E2
		[TypeConverter(typeof(GridDataTypeConverter))]
		[DefaultValue(typeof(string))]
		[Category("Data")]
		public Type FilterValueType
		{
			get
			{
				object obj = base.ViewState["FilterValueType"];
				if (obj == null)
				{
					obj = typeof(string);
				}
				return (Type)obj;
			}
			set
			{
				if (!GridDataTypeConverter.SupportedTypes.Contains(value) && !value.IsEnum)
				{
					throw new NotSupportedException("Specified value type is not supported " + value.ToString());
				}
				base.ViewState["FilterValueType"] = value;
			}
		}

		// Token: 0x060082C5 RID: 33477 RVA: 0x001DCF20 File Offset: 0x001DB120
		public IEnumerable<object> GetUniqueFilterItems()
		{
			if (base.Owner != null && base.Owner.PivotModel.DataProvider != null)
			{
				IReportFilterDescription reportFilterDescription = this.FilterDescription as IReportFilterDescription;
				if (reportFilterDescription != null)
				{
					DistinctValuesProvider disctinctValuesProvider = reportFilterDescription.GetDisctinctValuesProvider();
					EventCompletionSource<EventArgs> eventCompletionSource = new EventCompletionSource<EventArgs>(disctinctValuesProvider, "Updated");
					disctinctValuesProvider.Refresh();
					eventCompletionSource.AwaitEvent();
					eventCompletionSource.Dispose();
					return disctinctValuesProvider.DisctinctValues;
				}
			}
			return null;
		}

		// Token: 0x17002956 RID: 10582
		// (get) Token: 0x060082C6 RID: 33478 RVA: 0x001DCF83 File Offset: 0x001DB183
		internal bool IsFiltered
		{
			get
			{
				return this.FilterValues.Length > 0;
			}
		}

		// Token: 0x0400240D RID: 9229
		private FilterDescription filterDescription;
	}
}
