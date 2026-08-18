using System;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x02000037 RID: 55
	[Serializable]
	public class PrivateNote
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00011800 File Offset: 0x0000FA00
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x00011818 File Offset: 0x0000FA18
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

		// Token: 0x060002C3 RID: 707 RVA: 0x00011822 File Offset: 0x0000FA22
		public PrivateNote()
		{
			this.note = "";
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00011837 File Offset: 0x0000FA37
		public PrivateNote(string note)
		{
			this.note = note;
		}

		// Token: 0x0400017E RID: 382
		private string note;
	}
}
