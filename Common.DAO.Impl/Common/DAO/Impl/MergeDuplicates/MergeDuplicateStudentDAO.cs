using System;
using TechnoPro.Common.DAO.MergeDuplicates;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.MergeDuplicates.Students;

namespace TechnoPro.Common.DAO.Impl.MergeDuplicates
{
	// Token: 0x0200008D RID: 141
	public class MergeDuplicateStudentDAO : IMergeDuplicateStudentDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003A7 RID: 935 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public MergeDuplicateStudentDAO()
		{
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00020A6C File Offset: 0x0001EC6C
		public MergeDuplicateStudentDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00020A7E File Offset: 0x0001EC7E
		// (set) Token: 0x060003AA RID: 938 RVA: 0x00020A86 File Offset: 0x0001EC86
		public OperationContext OpContext { get; set; }

		// Token: 0x060003AB RID: 939 RVA: 0x00013135 File Offset: 0x00011335
		public void MergeDuplicateStudents(DuplicateStudentSet DuplicateStudentSet)
		{
		}
	}
}
