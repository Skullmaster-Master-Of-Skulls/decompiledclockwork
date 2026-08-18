using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004C7 RID: 1223
	[Serializable]
	public class Attendee : BusinessBase<int>
	{
		// Token: 0x06002500 RID: 9472 RVA: 0x00027ED2 File Offset: 0x000260D2
		public Attendee()
		{
			this.Person = null;
			this.IsNoShow = false;
			this.MiscCode = -1;
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x00027EF4 File Offset: 0x000260F4
		public Attendee(PersonBase Person, bool IsNoShow, int MiscCode)
		{
			this.Person = Person;
			this.IsNoShow = IsNoShow;
			this.MiscCode = MiscCode;
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06002502 RID: 9474 RVA: 0x00027F18 File Offset: 0x00026118
		// (set) Token: 0x06002503 RID: 9475 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int AttendeeId
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

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06002504 RID: 9476 RVA: 0x00027F30 File Offset: 0x00026130
		// (set) Token: 0x06002505 RID: 9477 RVA: 0x00027F38 File Offset: 0x00026138
		public PersonBase Person { get; set; }

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06002506 RID: 9478 RVA: 0x00027F41 File Offset: 0x00026141
		// (set) Token: 0x06002507 RID: 9479 RVA: 0x00027F49 File Offset: 0x00026149
		public bool IsNoShow { get; set; }

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06002508 RID: 9480 RVA: 0x00027F52 File Offset: 0x00026152
		// (set) Token: 0x06002509 RID: 9481 RVA: 0x00027F5A File Offset: 0x0002615A
		public int MiscCode { get; set; }
	}
}
