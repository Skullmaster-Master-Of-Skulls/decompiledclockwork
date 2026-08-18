using System;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl.LookupCourses
{
	// Token: 0x020000A0 RID: 160
	public class SessionDAO : ISessionDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00028254 File Offset: 0x00026454
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x0002825C File Offset: 0x0002645C
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x06000464 RID: 1124 RVA: 0x00028265 File Offset: 0x00026465
		public SessionDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00028296 File Offset: 0x00026496
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x0002829E File Offset: 0x0002649E
		public OperationContext OpContext { get; set; }

		// Token: 0x06000467 RID: 1127 RVA: 0x000282A8 File Offset: 0x000264A8
		public void SetSessionChooserDefaultValue(DateTime DtpNow)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@misccode", DbType.Int32, 1135),
				databaseLayer.GetParameter("@dt", DbType.String, DtpNow.ToString("yyyy-MM-dd"))
			};
			string query = "IF EXISTS(SELECT misccode FROM misc WHERE misccode=@misccode)\r\n    UPDATE misc SET miscstring=@dt WHERE misccode=@misccode\r\nELSE\r\n    INSERT INTO misc (misccode,miscstring) VALUES (@misccode,@dt)";
			databaseLayer.ExecuteNonQuery(query, parameters);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0002831C File Offset: 0x0002651C
		public DateTime? GetSessionChooserDefaultValue()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@misccode", DbType.Int32, 1135)
			};
			object obj = databaseLayer.ExecuteScalar("SELECT miscstring FROM misc WHERE misccode=@misccode", parameters);
			bool flag = obj == null || Convert.IsDBNull(obj);
			DateTime? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DateTime value;
				bool flag2 = DateTime.TryParse(obj.ToString(), out value);
				if (flag2)
				{
					result = new DateTime?(value);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}
	}
}
