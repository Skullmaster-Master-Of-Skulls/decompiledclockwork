using System;

namespace TechnoPro.Common.Public.Entities.MergeDuplicates.Courses
{
	// Token: 0x02000298 RID: 664
	public class DuplicateCourseMergeResult
	{
		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x00019C6D File Offset: 0x00017E6D
		// (set) Token: 0x06001422 RID: 5154 RVA: 0x00019C75 File Offset: 0x00017E75
		public eDuplicateCourseMergeStatus Status { get; set; }

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x00019C7E File Offset: 0x00017E7E
		// (set) Token: 0x06001424 RID: 5156 RVA: 0x00019C86 File Offset: 0x00017E86
		public string ErrorMessage { get; set; }

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x00019C8F File Offset: 0x00017E8F
		// (set) Token: 0x06001426 RID: 5158 RVA: 0x00019C97 File Offset: 0x00017E97
		public DuplicateCourseMergeAction Action { get; set; }
	}
}
