using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;

namespace TechnoPro.Common.DAO.Impl
{
	// Token: 0x02000019 RID: 25
	public class MiscSafeDAO : IMiscSafeDAO
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00005408 File Offset: 0x00003608
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00005410 File Offset: 0x00003610
		public DatabaseLayer DatabaseManager { get; set; }

		// Token: 0x060000A1 RID: 161 RVA: 0x00005419 File Offset: 0x00003619
		public MiscSafeDAO()
		{
			this.DatabaseManager = DatabaseLayerFactory.ClockWork;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005430 File Offset: 0x00003630
		public void Save(string key, string value)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@safekey", DbType.String, key.ToUpper()),
				this.DatabaseManager.GetParameter("@safevalue", DbType.String, value ?? string.Empty)
			};
			this.DatabaseManager.ExecuteNonQuery("if not exists(select 1 from miscsafe where safekey=@safekey)\r\n            begin\r\n                insert into miscsafe (safekey, safevalue) values(@safekey, @safevalue)\r\n            end\r\n            else\r\n            begin\r\n                update miscsafe set safevalue=@safevalue where safekey=@safekey\r\n            end", parameters);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00005494 File Offset: 0x00003694
		public string GetValue(string key)
		{
			DbParameter parameter = this.DatabaseManager.GetParameter("@safekey", DbType.String, key.ToUpper());
			object obj = this.DatabaseManager.ExecuteScalar("select safevalue from miscsafe where safekey = @safekey", new DbParameter[]
			{
				parameter
			});
			return obj as string;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000054E0 File Offset: 0x000036E0
		public IList<string> GetKeys(string value)
		{
			List<string> list = new List<string>();
			DbParameter parameter = this.DatabaseManager.GetParameter("@safevalue", DbType.String, value);
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("select safekey from miscsafe where safevalue = @safevalue", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						string text = (string)dataReader[0];
						bool flag2 = !string.IsNullOrEmpty(text);
						if (flag2)
						{
							list.Add(text);
						}
					}
				}
			}
			return list;
		}
	}
}
