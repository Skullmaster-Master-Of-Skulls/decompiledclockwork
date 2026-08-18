using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200047F RID: 1151
	public class MonthChangedEventArgs
	{
		// Token: 0x0600391B RID: 14619 RVA: 0x000B9E20 File Offset: 0x000B8020
		public MonthChangedEventArgs(DateTime newDate, DateTime previousDate)
		{
			this.newDate = newDate;
			this.previousDate = previousDate;
		}

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x0600391C RID: 14620 RVA: 0x000B9E36 File Offset: 0x000B8036
		public DateTime NewDate
		{
			get
			{
				return this.newDate;
			}
		}

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x0600391D RID: 14621 RVA: 0x000B9E3E File Offset: 0x000B803E
		public DateTime PreviousDate
		{
			get
			{
				return this.previousDate;
			}
		}

		// Token: 0x040022A7 RID: 8871
		private DateTime newDate;

		// Token: 0x040022A8 RID: 8872
		private DateTime previousDate;
	}
}
