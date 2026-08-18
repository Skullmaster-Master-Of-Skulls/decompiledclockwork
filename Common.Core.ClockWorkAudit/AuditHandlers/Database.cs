using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.DAO.ClockWorkAudit;
using TechnoPro.Common.DAO.Impl.ClockWorkAudit;
using TechnoPro.Common.ICore.ClockWorkAudit;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;

namespace TechnoPro.Common.Core.ClockWorkAudit.AuditHandlers
{
	// Token: 0x02000006 RID: 6
	public class Database : IClockWorkAuditHandler, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002050 File Offset: 0x00000250
		public Database()
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002F39 File Offset: 0x00001139
		public Database(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002F4B File Offset: 0x0000114B
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002F53 File Offset: 0x00001153
		public OperationContext OpContext { get; set; }

		// Token: 0x0600001E RID: 30 RVA: 0x00002F5C File Offset: 0x0000115C
		public AuditResult ExecuteAudit()
		{
			IClockWorkAuditDAO clockWorkAuditDAO = new ClockWorkAuditDAO(this.OpContext);
			bool flag = clockWorkAuditDAO.AreFileDbAndRegularDbTheSameDatabase();
			string[] dbPatchSqlUserPrivileges = clockWorkAuditDAO.GetDbPatchSqlUserPrivileges();
			string[] clockWorkDbUserPriviliges = clockWorkAuditDAO.GetClockWorkDbUserPriviliges();
			string[] filesDbUserPriviliges = clockWorkAuditDAO.GetFilesDbUserPriviliges();
			return new AuditResult(eClockWorkAuditType.Database)
			{
				Checks = new List<AuditCheck>
				{
					new AuditCheck("Check file database is separate", flag ? eAuditStatus.Failed : eAuditStatus.CompletedSuccessful, Array.Empty<string>()),
					this.CheckRoles("Check database patch user roles", dbPatchSqlUserPrivileges, new string[]
					{
						"db_owner"
					}),
					this.CheckRoles("Check files user roles", filesDbUserPriviliges, new string[]
					{
						"db_datareader",
						"db_datawriter"
					}),
					this.CheckRoles("Check regular database user roles", clockWorkDbUserPriviliges, new string[]
					{
						"db_datareader",
						"db_datawriter"
					})
				}
			};
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000304C File Offset: 0x0000124C
		private AuditCheck CheckRoles(string title, IEnumerable<string> existingRoles, params string[] requiredRoles)
		{
			return new AuditCheck(title, requiredRoles.Any((string g) => existingRoles.FirstOrDefault((string h) => h.Equals(g, StringComparison.OrdinalIgnoreCase)) == null) ? eAuditStatus.Failed : eAuditStatus.CompletedSuccessful, new string[]
			{
				"Existing roles={0}:Required roles={1}",
				string.Join(", ", existingRoles.ToArray<string>()),
				string.Join(", ", requiredRoles.ToArray<string>())
			});
		}
	}
}
