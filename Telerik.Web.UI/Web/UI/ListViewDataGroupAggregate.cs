using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000585 RID: 1413
	public class ListViewDataGroupAggregate : StateManager
	{
		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x060032F8 RID: 13048 RVA: 0x000A8A04 File Offset: 0x000A6C04
		// (set) Token: 0x060032F9 RID: 13049 RVA: 0x000A8A31 File Offset: 0x000A6C31
		[Description("DataField")]
		[Category("Data")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string DataField
		{
			get
			{
				object obj = base.ViewState["DataField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataField"] = value;
			}
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x060032FA RID: 13050 RVA: 0x000A8A44 File Offset: 0x000A6C44
		// (set) Token: 0x060032FB RID: 13051 RVA: 0x000A8A4C File Offset: 0x000A6C4C
		[DefaultValue(typeof(ListViewAggregateFunction), "None")]
		[NotifyParentProperty(true)]
		public ListViewAggregateFunction Aggregate
		{
			get
			{
				return this._aggregate;
			}
			set
			{
				this._aggregate = value;
			}
		}

		// Token: 0x04000DF7 RID: 3575
		private ListViewAggregateFunction _aggregate;
	}
}
