using System;

namespace TechnoPro.Common.Public.Entities.Intake
{
	// Token: 0x02000324 RID: 804
	public class PreIntakeStatusAttribute : Attribute
	{
		// Token: 0x06001903 RID: 6403 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public PreIntakeStatusAttribute()
		{
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0001DB0D File Offset: 0x0001BD0D
		public PreIntakeStatusAttribute(string title)
		{
			this.Title = title;
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x0001DB1F File Offset: 0x0001BD1F
		// (set) Token: 0x06001906 RID: 6406 RVA: 0x0001DB27 File Offset: 0x0001BD27
		public string Title { get; set; }
	}
}
