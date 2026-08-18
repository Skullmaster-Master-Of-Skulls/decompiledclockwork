using System;

namespace TechnoPro.Common.Public.Entities.MergeDuplicates.Courses
{
	// Token: 0x02000297 RID: 663
	public class DuplicateCourseMergeAction
	{
		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x00019C29 File Offset: 0x00017E29
		// (set) Token: 0x06001419 RID: 5145 RVA: 0x00019C31 File Offset: 0x00017E31
		public eDuplicateCourseMergeActionType ActionType { get; set; }

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x00019C3A File Offset: 0x00017E3A
		// (set) Token: 0x0600141B RID: 5147 RVA: 0x00019C42 File Offset: 0x00017E42
		public int OldLucid { get; set; }

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x00019C4B File Offset: 0x00017E4B
		// (set) Token: 0x0600141D RID: 5149 RVA: 0x00019C53 File Offset: 0x00017E53
		public int NewLucid { get; set; }

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x00019C5C File Offset: 0x00017E5C
		// (set) Token: 0x0600141F RID: 5151 RVA: 0x00019C64 File Offset: 0x00017E64
		public TableAndColumn TableAndColumnToApplyTo { get; set; }
	}
}
