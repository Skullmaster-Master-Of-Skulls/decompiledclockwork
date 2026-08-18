using System;
using TechnoPro.Common.DAO.ClockWorkDatabase;
using TechnoPro.Common.DAO.Impl.ClockWorkDatabase;
using TechnoPro.Common.ICore.ClockWorkDatabase;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.ClockWorkDatabase
{
	// Token: 0x0200011F RID: 287
	public class ClockWorkDatabaseManager : IClockWorkDatabaseManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000C1F RID: 3103 RVA: 0x0005545E File Offset: 0x0005365E
		public ClockWorkDatabaseManager(OperationContext OpContext)
		{
			this.OpContext = OpContext;
			this.dao = new ClockWorkDatabaseDAO(OpContext);
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0005547C File Offset: 0x0005367C
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x00055484 File Offset: 0x00053684
		public OperationContext OpContext { get; set; }

		// Token: 0x06000C22 RID: 3106 RVA: 0x00055490 File Offset: 0x00053690
		public bool DoesTableExist(string TableName)
		{
			return this.dao.DoesTableExist(TableName);
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x000554B0 File Offset: 0x000536B0
		public string[] LoadAllTableNames()
		{
			return this.dao.LoadAllTableNames();
		}

		// Token: 0x0400024A RID: 586
		private IClockWorkDatabaseDAO dao;
	}
}
