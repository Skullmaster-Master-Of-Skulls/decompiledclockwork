using System;
using System.Collections.Generic;

namespace ClockWorkAPI.ServiceProviders
{
	// Token: 0x02000097 RID: 151
	public class SPDayOfWeekOccurrence : iSPDateTimeOccurrence
	{
		// Token: 0x060007AB RID: 1963 RVA: 0x0002CAA0 File Offset: 0x0002BAA0
		public SPDayOfWeekOccurrence(AvailabilityDayOfWeek availability)
		{
			this.availability = availability;
			this.matchings = new List<SPMatching>();
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x0002CAC0 File Offset: 0x0002BAC0
		public List<SPMatching> Matchings
		{
			get
			{
				return this.matchings;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0002CAD8 File Offset: 0x0002BAD8
		public string Caption
		{
			get
			{
				return this.availability.Caption;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0002CAF8 File Offset: 0x0002BAF8
		public char DateTimeOccurrenceType
		{
			get
			{
				return 'w';
			}
		}

		// Token: 0x040003E6 RID: 998
		private AvailabilityDayOfWeek availability;

		// Token: 0x040003E7 RID: 999
		private List<SPMatching> matchings;
	}
}
