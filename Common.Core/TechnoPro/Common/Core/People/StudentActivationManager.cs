using System;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.People;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.People
{
	// Token: 0x020000A7 RID: 167
	public class StudentActivationManager : IStudentActivationManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060005E8 RID: 1512 RVA: 0x00022CCC File Offset: 0x00020ECC
		public StudentActivationManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new StudentActivationDAO(opContext);
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00022CEA File Offset: 0x00020EEA
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x00022CF2 File Offset: 0x00020EF2
		public OperationContext OpContext { get; set; }

		// Token: 0x060005EB RID: 1515 RVA: 0x00022CFB File Offset: 0x00020EFB
		public void MergeActivations(int PersonIdNew, int PersonIdOld)
		{
			this.dao.MergeActivations(PersonIdNew, PersonIdOld);
		}

		// Token: 0x0400012C RID: 300
		private IStudentActivationDAO dao;
	}
}
