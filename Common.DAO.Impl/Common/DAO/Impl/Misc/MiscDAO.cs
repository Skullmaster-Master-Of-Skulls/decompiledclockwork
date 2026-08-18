using System;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Misc;

namespace TechnoPro.Common.DAO.Impl.Misc
{
	// Token: 0x02000088 RID: 136
	public class MiscDAO : IMiscDAO
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0001F7C4 File Offset: 0x0001D9C4
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0001F7CC File Offset: 0x0001D9CC
		public DatabaseLayer DatabaseManager { get; set; }

		// Token: 0x06000389 RID: 905 RVA: 0x0001F7D5 File Offset: 0x0001D9D5
		public MiscDAO()
		{
			this.DatabaseManager = DatabaseLayerFactory.ClockWork;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001F7EB File Offset: 0x0001D9EB
		public MiscDAO(eDatabaseConnectionStringName dbRole)
		{
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(dbRole);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001F804 File Offset: 0x0001DA04
		public void Save(int key, string value)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@miskey", DbType.Int32, key),
				this.DatabaseManager.GetParameter("@miscvalue", DbType.String, value)
			};
			this.DatabaseManager.ExecuteNonQuery("if not exists(select 1 from misc where misccode=@misckey)\r\n            begin\r\n                insert into misc (misccode, miscstring) values(@misckey, @miscvalue)\r\n            end\r\n            else\r\n            begin\r\n                update misc set miscstring=@miscvalue where misccode=@misckey\r\n            end", parameters);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001F85C File Offset: 0x0001DA5C
		public string GetValue(int key)
		{
			DbParameter parameter = this.DatabaseManager.GetParameter("@misckey", DbType.Int32, key);
			object obj = this.DatabaseManager.ExecuteScalar("select miscstring from misc where misccode = @misckey", new DbParameter[]
			{
				parameter
			});
			return obj as string;
		}
	}
}
