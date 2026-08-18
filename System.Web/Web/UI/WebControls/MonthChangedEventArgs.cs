using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005EC RID: 1516
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class MonthChangedEventArgs
	{
		// Token: 0x06004B10 RID: 19216 RVA: 0x001327AB File Offset: 0x001317AB
		public MonthChangedEventArgs(DateTime newDate, DateTime previousDate)
		{
			this.newDate = newDate;
			this.previousDate = previousDate;
		}

		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x06004B11 RID: 19217 RVA: 0x001327C1 File Offset: 0x001317C1
		public DateTime NewDate
		{
			get
			{
				return this.newDate;
			}
		}

		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x06004B12 RID: 19218 RVA: 0x001327C9 File Offset: 0x001317C9
		public DateTime PreviousDate
		{
			get
			{
				return this.previousDate;
			}
		}

		// Token: 0x04002B98 RID: 11160
		private DateTime newDate;

		// Token: 0x04002B99 RID: 11161
		private DateTime previousDate;
	}
}
