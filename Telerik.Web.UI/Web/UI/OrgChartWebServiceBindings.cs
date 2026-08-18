using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000C18 RID: 3096
	public class OrgChartWebServiceBindings
	{
		// Token: 0x1700265F RID: 9823
		// (get) Token: 0x060075EE RID: 30190 RVA: 0x001B678F File Offset: 0x001B498F
		// (set) Token: 0x060075EF RID: 30191 RVA: 0x001B67AA File Offset: 0x001B49AA
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public OrgChartSimpleWebServiceBinding Simple
		{
			get
			{
				if (this._simple == null)
				{
					this._simple = new OrgChartSimpleWebServiceBinding();
				}
				return this._simple;
			}
			set
			{
				this._simple = value;
			}
		}

		// Token: 0x17002660 RID: 9824
		// (get) Token: 0x060075F0 RID: 30192 RVA: 0x001B67B3 File Offset: 0x001B49B3
		// (set) Token: 0x060075F1 RID: 30193 RVA: 0x001B67CE File Offset: 0x001B49CE
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public OrgChartGroupEnabledWebServiceBinding GroupEnabled
		{
			get
			{
				if (this._groupEnabled == null)
				{
					this._groupEnabled = new OrgChartGroupEnabledWebServiceBinding();
				}
				return this._groupEnabled;
			}
			set
			{
				this._groupEnabled = value;
			}
		}

		// Token: 0x0400205A RID: 8282
		private OrgChartSimpleWebServiceBinding _simple;

		// Token: 0x0400205B RID: 8283
		private OrgChartGroupEnabledWebServiceBinding _groupEnabled;
	}
}
