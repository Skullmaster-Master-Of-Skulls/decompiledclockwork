using System;

namespace System.Windows.Forms
{
	// Token: 0x02000227 RID: 551
	public class DateBoldEventArgs : EventArgs
	{
		// Token: 0x060023CC RID: 9164 RVA: 0x000AAC08 File Offset: 0x000A8E08
		internal DateBoldEventArgs(DateTime start, int size)
		{
			this.startDate = start;
			this.size = size;
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x060023CD RID: 9165 RVA: 0x000AAC1E File Offset: 0x000A8E1E
		public DateTime StartDate
		{
			get
			{
				return this.startDate;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060023CE RID: 9166 RVA: 0x000AAC26 File Offset: 0x000A8E26
		public int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x060023CF RID: 9167 RVA: 0x000AAC2E File Offset: 0x000A8E2E
		// (set) Token: 0x060023D0 RID: 9168 RVA: 0x000AAC36 File Offset: 0x000A8E36
		public int[] DaysToBold
		{
			get
			{
				return this.daysToBold;
			}
			set
			{
				this.daysToBold = value;
			}
		}

		// Token: 0x04000EB8 RID: 3768
		private readonly DateTime startDate;

		// Token: 0x04000EB9 RID: 3769
		private readonly int size;

		// Token: 0x04000EBA RID: 3770
		private int[] daysToBold;
	}
}
