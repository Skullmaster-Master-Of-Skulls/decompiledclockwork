using System;

namespace TechnoPro.Common.Public.Entities.Intake
{
	// Token: 0x02000322 RID: 802
	public class CreateRealStudentAccountFromIntakeStatusAttribute : Attribute
	{
		// Token: 0x060018FF RID: 6399 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public CreateRealStudentAccountFromIntakeStatusAttribute()
		{
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x0001DAEA File Offset: 0x0001BCEA
		public CreateRealStudentAccountFromIntakeStatusAttribute(string title)
		{
			this.Title = title;
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x0001DAFC File Offset: 0x0001BCFC
		// (set) Token: 0x06001902 RID: 6402 RVA: 0x0001DB04 File Offset: 0x0001BD04
		public string Title { get; set; }
	}
}
