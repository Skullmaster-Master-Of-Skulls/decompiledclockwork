using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000544 RID: 1348
	[Serializable]
	public class PotentialTestMethodFoundNote
	{
		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x06002B33 RID: 11059 RVA: 0x0002E8C0 File Offset: 0x0002CAC0
		// (set) Token: 0x06002B34 RID: 11060 RVA: 0x0002E8C8 File Offset: 0x0002CAC8
		public virtual string Note { get; set; }

		// Token: 0x06002B35 RID: 11061 RVA: 0x0002E8D1 File Offset: 0x0002CAD1
		public PotentialTestMethodFoundNote()
		{
			this.Note = "";
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x0002E8E7 File Offset: 0x0002CAE7
		public PotentialTestMethodFoundNote(string note)
		{
			this.Note = note;
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x0002E8FC File Offset: 0x0002CAFC
		public override string ToString()
		{
			return this.Note;
		}
	}
}
