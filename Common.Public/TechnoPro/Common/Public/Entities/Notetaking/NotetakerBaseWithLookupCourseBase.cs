using System;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Public.Entities.Notetaking
{
	// Token: 0x0200027F RID: 639
	public class NotetakerBaseWithLookupCourseBase : BusinessBase<int>
	{
		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x0001950C File Offset: 0x0001770C
		public virtual int ServiceProviderId
		{
			get
			{
				bool flag = this.Notetaker == null;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = this.Notetaker.ServiceProviderId;
				}
				return result;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x0001953A File Offset: 0x0001773A
		// (set) Token: 0x06001342 RID: 4930 RVA: 0x00019542 File Offset: 0x00017742
		public NotetakerBase Notetaker { get; set; }

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x0001954B File Offset: 0x0001774B
		// (set) Token: 0x06001344 RID: 4932 RVA: 0x00019553 File Offset: 0x00017753
		public LookupCourseBase Course { get; set; }
	}
}
