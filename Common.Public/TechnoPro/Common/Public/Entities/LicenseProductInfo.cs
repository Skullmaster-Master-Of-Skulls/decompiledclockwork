using System;

namespace TechnoPro.Common.Public.Entities
{
	// Token: 0x020000F2 RID: 242
	public class LicenseProductInfo : BusinessBase<string>
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0000EAD8 File Offset: 0x0000CCD8
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public virtual string ProductName
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

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0000EAF0 File Offset: 0x0000CCF0
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x0000EAF8 File Offset: 0x0000CCF8
		public virtual string ProductParameters { get; set; }
	}
}
