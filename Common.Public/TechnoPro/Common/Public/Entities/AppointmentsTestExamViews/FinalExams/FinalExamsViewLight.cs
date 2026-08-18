using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.FinalExams
{
	// Token: 0x020004FA RID: 1274
	public class FinalExamsViewLight : FinalExamsViewBase
	{
		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x060026B9 RID: 9913 RVA: 0x0002921C File Offset: 0x0002741C
		// (set) Token: 0x060026BA RID: 9914 RVA: 0x00029224 File Offset: 0x00027424
		public IList<FinalExamsViewLightBooking> Bookings { get; set; }
	}
}
