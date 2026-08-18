using System;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl.People
{
	// Token: 0x02000075 RID: 117
	public class StudentActivationDAO : IStudentActivationDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x00017D0D File Offset: 0x00015F0D
		public StudentActivationDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00017D3D File Offset: 0x00015F3D
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x00017D45 File Offset: 0x00015F45
		public OperationContext OpContext { get; set; }

		// Token: 0x060002D5 RID: 725 RVA: 0x00017D50 File Offset: 0x00015F50
		public void MergeActivations(int PersonIdNew, int PersonIdOld)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pidnew", DbType.Int32, PersonIdNew),
				this.DatabaseManager.GetParameter("@pidold", DbType.Int32, PersonIdOld)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE peopledatesadded SET personid=@pidnew WHERE personid=@pidold; UPDATE peoplepreviousyears SET personid=@pidnew WHERE personid=@pidold", parameters);
		}

		// Token: 0x0400012B RID: 299
		private DatabaseLayer DatabaseManager;
	}
}
