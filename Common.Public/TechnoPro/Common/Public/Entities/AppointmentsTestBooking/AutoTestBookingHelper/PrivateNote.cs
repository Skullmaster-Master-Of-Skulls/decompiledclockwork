using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000545 RID: 1349
	[Serializable]
	public class PrivateNote
	{
		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x06002B38 RID: 11064 RVA: 0x0002E914 File Offset: 0x0002CB14
		// (set) Token: 0x06002B39 RID: 11065 RVA: 0x0002E92C File Offset: 0x0002CB2C
		public string Note
		{
			get
			{
				return this.note;
			}
			set
			{
				this.note = value;
			}
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x0002E936 File Offset: 0x0002CB36
		public PrivateNote()
		{
			this.note = "";
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x0002E94B File Offset: 0x0002CB4B
		public PrivateNote(string note)
		{
			this.note = note;
		}

		// Token: 0x04001EA9 RID: 7849
		private string note;
	}
}
