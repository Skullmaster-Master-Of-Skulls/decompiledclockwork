using System;

namespace TechnoPro.Common.Public.Entities.AppointmentBookingStudent
{
	// Token: 0x02000567 RID: 1383
	public class CutoffTime
	{
		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x06002C8F RID: 11407 RVA: 0x00031A0C File Offset: 0x0002FC0C
		// (set) Token: 0x06002C90 RID: 11408 RVA: 0x00031A14 File Offset: 0x0002FC14
		public bool Enabled { get; set; }

		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x06002C91 RID: 11409 RVA: 0x00031A1D File Offset: 0x0002FC1D
		// (set) Token: 0x06002C92 RID: 11410 RVA: 0x00031A25 File Offset: 0x0002FC25
		public int Amount { get; set; }

		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x06002C93 RID: 11411 RVA: 0x00031A2E File Offset: 0x0002FC2E
		// (set) Token: 0x06002C94 RID: 11412 RVA: 0x00031A36 File Offset: 0x0002FC36
		public eTimeInterval Interval { get; set; }

		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x06002C95 RID: 11413 RVA: 0x00031A3F File Offset: 0x0002FC3F
		public static CutoffTime None
		{
			get
			{
				return new CutoffTime();
			}
		}
	}
}
