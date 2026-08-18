using System;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004BE RID: 1214
	public class AppCancelReasonGroup : BusinessBase<string>
	{
		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x060024BD RID: 9405 RVA: 0x00027C20 File Offset: 0x00025E20
		// (set) Token: 0x060024BE RID: 9406 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public virtual string CancelReasonGroupName
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
	}
}
