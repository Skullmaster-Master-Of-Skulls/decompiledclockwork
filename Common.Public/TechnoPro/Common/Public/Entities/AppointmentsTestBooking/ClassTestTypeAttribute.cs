using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x02000510 RID: 1296
	public class ClassTestTypeAttribute : Attribute
	{
		// Token: 0x0600278E RID: 10126 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public ClassTestTypeAttribute()
		{
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x0002992E File Offset: 0x00027B2E
		public ClassTestTypeAttribute(string title)
		{
			this.Title = title;
		}

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x06002790 RID: 10128 RVA: 0x00029940 File Offset: 0x00027B40
		// (set) Token: 0x06002791 RID: 10129 RVA: 0x00029948 File Offset: 0x00027B48
		public string Title { get; set; }
	}
}
