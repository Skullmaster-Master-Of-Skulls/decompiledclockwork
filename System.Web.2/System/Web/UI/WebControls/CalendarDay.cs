using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200038E RID: 910
	public class CalendarDay
	{
		// Token: 0x06002A9E RID: 10910 RVA: 0x0008AE4D File Offset: 0x0008904D
		public CalendarDay(DateTime date, bool isWeekend, bool isToday, bool isSelected, bool isOtherMonth, string dayNumberText)
		{
			this.date = date;
			this.isWeekend = isWeekend;
			this.isToday = isToday;
			this.isOtherMonth = isOtherMonth;
			this.isSelected = isSelected;
			this.dayNumberText = dayNumberText;
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06002A9F RID: 10911 RVA: 0x0008AE82 File Offset: 0x00089082
		public DateTime Date
		{
			get
			{
				return this.date;
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06002AA0 RID: 10912 RVA: 0x0008AE8A File Offset: 0x0008908A
		public string DayNumberText
		{
			get
			{
				return this.dayNumberText;
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06002AA1 RID: 10913 RVA: 0x0008AE92 File Offset: 0x00089092
		public bool IsOtherMonth
		{
			get
			{
				return this.isOtherMonth;
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06002AA2 RID: 10914 RVA: 0x0008AE9A File Offset: 0x0008909A
		// (set) Token: 0x06002AA3 RID: 10915 RVA: 0x0008AEA2 File Offset: 0x000890A2
		public bool IsSelectable
		{
			get
			{
				return this.isSelectable;
			}
			set
			{
				this.isSelectable = value;
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06002AA4 RID: 10916 RVA: 0x0008AEAB File Offset: 0x000890AB
		public bool IsSelected
		{
			get
			{
				return this.isSelected;
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06002AA5 RID: 10917 RVA: 0x0008AEB3 File Offset: 0x000890B3
		public bool IsToday
		{
			get
			{
				return this.isToday;
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06002AA6 RID: 10918 RVA: 0x0008AEBB File Offset: 0x000890BB
		public bool IsWeekend
		{
			get
			{
				return this.isWeekend;
			}
		}

		// Token: 0x04001EC0 RID: 7872
		private DateTime date;

		// Token: 0x04001EC1 RID: 7873
		private bool isSelectable;

		// Token: 0x04001EC2 RID: 7874
		private bool isToday;

		// Token: 0x04001EC3 RID: 7875
		private bool isWeekend;

		// Token: 0x04001EC4 RID: 7876
		private bool isOtherMonth;

		// Token: 0x04001EC5 RID: 7877
		private bool isSelected;

		// Token: 0x04001EC6 RID: 7878
		private string dayNumberText;
	}
}
