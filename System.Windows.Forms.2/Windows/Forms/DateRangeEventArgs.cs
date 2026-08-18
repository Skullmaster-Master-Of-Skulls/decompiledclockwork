using System;

namespace System.Windows.Forms
{
	// Token: 0x02000229 RID: 553
	public class DateRangeEventArgs : EventArgs
	{
		// Token: 0x060023D5 RID: 9173 RVA: 0x000AAC3F File Offset: 0x000A8E3F
		public DateRangeEventArgs(DateTime start, DateTime end)
		{
			this.start = start;
			this.end = end;
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x060023D6 RID: 9174 RVA: 0x000AAC55 File Offset: 0x000A8E55
		public DateTime Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x060023D7 RID: 9175 RVA: 0x000AAC5D File Offset: 0x000A8E5D
		public DateTime End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x04000EBB RID: 3771
		private readonly DateTime start;

		// Token: 0x04000EBC RID: 3772
		private readonly DateTime end;
	}
}
