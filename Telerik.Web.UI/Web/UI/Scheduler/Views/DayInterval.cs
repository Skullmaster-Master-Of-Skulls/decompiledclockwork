using System;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02001A50 RID: 6736
	internal struct DayInterval
	{
		// Token: 0x17004F56 RID: 20310
		// (get) Token: 0x06010572 RID: 66930 RVA: 0x003A5B4A File Offset: 0x003A3D4A
		public DateTime DayStart
		{
			get
			{
				return this._dayStart;
			}
		}

		// Token: 0x17004F57 RID: 20311
		// (get) Token: 0x06010573 RID: 66931 RVA: 0x003A5B52 File Offset: 0x003A3D52
		public DateTime DayEnd
		{
			get
			{
				return this._dayEnd;
			}
		}

		// Token: 0x06010574 RID: 66932 RVA: 0x003A5B5A File Offset: 0x003A3D5A
		public DayInterval(DateTime dayStart, DateTime dayEnd)
		{
			this._dayStart = dayStart;
			this._dayEnd = dayEnd;
		}

		// Token: 0x04004983 RID: 18819
		private readonly DateTime _dayStart;

		// Token: 0x04004984 RID: 18820
		private readonly DateTime _dayEnd;
	}
}
