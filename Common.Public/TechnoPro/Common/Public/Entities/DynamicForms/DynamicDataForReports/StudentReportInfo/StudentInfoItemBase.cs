using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataForReports.StudentReportInfo
{
	// Token: 0x020003A9 RID: 937
	public class StudentInfoItemBase : BusinessBase<int>
	{
		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x00020B44 File Offset: 0x0001ED44
		// (set) Token: 0x06001C81 RID: 7297 RVA: 0x0000E258 File Offset: 0x0000C458
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
	}
}
