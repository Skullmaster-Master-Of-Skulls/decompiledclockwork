using System;

namespace TechnoPro.Common.Public.Entities.Vets
{
	// Token: 0x02000101 RID: 257
	public class VeteranRequestStatusAttribute : Attribute
	{
		// Token: 0x060005DB RID: 1499 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public VeteranRequestStatusAttribute()
		{
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0000EDD2 File Offset: 0x0000CFD2
		public VeteranRequestStatusAttribute(string displayTitle)
		{
			this.DisplayTitle = displayTitle;
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0000EDE4 File Offset: 0x0000CFE4
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x0000EDEC File Offset: 0x0000CFEC
		public string DisplayTitle { get; set; }
	}
}
