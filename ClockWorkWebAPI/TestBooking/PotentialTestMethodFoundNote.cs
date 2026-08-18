using System;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x02000036 RID: 54
	[Serializable]
	public class PotentialTestMethodFoundNote
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002BC RID: 700 RVA: 0x000117AC File Offset: 0x0000F9AC
		// (set) Token: 0x060002BD RID: 701 RVA: 0x000117B4 File Offset: 0x0000F9B4
		public virtual string Note { get; set; }

		// Token: 0x060002BE RID: 702 RVA: 0x000117BD File Offset: 0x0000F9BD
		public PotentialTestMethodFoundNote()
		{
			this.Note = "";
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000117D3 File Offset: 0x0000F9D3
		public PotentialTestMethodFoundNote(string note)
		{
			this.Note = note;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000117E8 File Offset: 0x0000F9E8
		public override string ToString()
		{
			return this.Note;
		}
	}
}
