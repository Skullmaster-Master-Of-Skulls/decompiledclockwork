using System;
using System.Collections.Generic;

namespace ClockWorkAPI.ServiceProviders
{
	// Token: 0x02000025 RID: 37
	public class SPDateTimeOccurrence : iSPDateTimeOccurrence
	{
		// Token: 0x06000205 RID: 517 RVA: 0x0000BB28 File Offset: 0x0000AB28
		public SPDateTimeOccurrence(string caption, DateTime sdate, DateTime edate)
		{
			this.caption = caption;
			this.sdate = sdate;
			this.edate = edate;
			this.matchings = new List<SPMatching>();
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000BB54 File Offset: 0x0000AB54
		public List<SPMatching> Matchings
		{
			get
			{
				return this.matchings;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000BB6C File Offset: 0x0000AB6C
		public string Caption
		{
			get
			{
				return this.caption;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000BB84 File Offset: 0x0000AB84
		public char DateTimeOccurrenceType
		{
			get
			{
				return 'd';
			}
		}

		// Token: 0x040000F3 RID: 243
		private string caption;

		// Token: 0x040000F4 RID: 244
		private DateTime sdate;

		// Token: 0x040000F5 RID: 245
		private DateTime edate;

		// Token: 0x040000F6 RID: 246
		private List<SPMatching> matchings;
	}
}
