using System;

namespace TechnoPro.Common.Public.Entities.AppointmentSync
{
	// Token: 0x020004CD RID: 1229
	public class ClockWorkSyncPersonBase : BusinessBase<int>
	{
		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06002521 RID: 9505 RVA: 0x00028018 File Offset: 0x00026218
		// (set) Token: 0x06002522 RID: 9506 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int PersonId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x00028030 File Offset: 0x00026230
		// (set) Token: 0x06002524 RID: 9508 RVA: 0x00028038 File Offset: 0x00026238
		public string FirstName { get; set; }

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06002525 RID: 9509 RVA: 0x00028041 File Offset: 0x00026241
		// (set) Token: 0x06002526 RID: 9510 RVA: 0x00028049 File Offset: 0x00026249
		public string LastName { get; set; }

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06002527 RID: 9511 RVA: 0x00028052 File Offset: 0x00026252
		// (set) Token: 0x06002528 RID: 9512 RVA: 0x0002805A File Offset: 0x0002625A
		public string Student_no { get; set; }
	}
}
