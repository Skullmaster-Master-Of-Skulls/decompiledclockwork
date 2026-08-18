using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.DAO.Impl.Appointments
{
	// Token: 0x0200012D RID: 301
	public class IconInfoDAO : IIconInfoDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0005C8AC File Offset: 0x0005AAAC
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x0005C8B4 File Offset: 0x0005AAB4
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x060008F1 RID: 2289 RVA: 0x0005C8BD File Offset: 0x0005AABD
		public IconInfoDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x0005C8EE File Offset: 0x0005AAEE
		// (set) Token: 0x060008F3 RID: 2291 RVA: 0x0005C8F6 File Offset: 0x0005AAF6
		public OperationContext OpContext { get; set; }

		// Token: 0x060008F4 RID: 2292 RVA: 0x0005C900 File Offset: 0x0005AB00
		internal static IconInfo GetIconInfoFromRecord(IDataRecord record)
		{
			bool flag = record == null || record["appointmenticoninfoid"] == DBNull.Value;
			IconInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new IconInfo
				{
					IconInfoId = (int)record["appointmenticoninfoid"],
					IconNum = ((PeopleDAO.ReaderContainsColumn((IDataReader)record, "iconindex") && record["iconindex"] != DBNull.Value) ? ((int)record["iconindex"]) : ((PeopleDAO.ReaderContainsColumn((IDataReader)record, "iconnum") && record["iconnum"] != DBNull.Value) ? ((int)record["iconnum"]) : -1)),
					IconText = record["icontext"].ToString(),
					IconLetterIdentifier = record["iconletteridentifier"].ToString()
				};
			}
			return result;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0005C9F4 File Offset: 0x0005ABF4
		public IconInfo LoadIconInfo(int IconInfoId)
		{
			IconInfo result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT appointmenticoninfoid,iconindex,icontext,iconletteridentifier FROM appointmenticoninfo WHERE appointmenticoninfoid=@iconinfoid", new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@iconinfoid", DbType.Int32, IconInfoId)
			}))
			{
				bool flag = dataReader == null || dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = IconInfoDAO.GetIconInfoFromRecord(dataReader);
				}
			}
			return result;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0005CA70 File Offset: 0x0005AC70
		public void DeleteIconInfo(int IconInfoId)
		{
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM appointmenticoninfo WHERE appointmenticoninfoid=@iconinfoid", new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@iconinfoid", DbType.Int32, IconInfoId)
			});
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0005CAB0 File Offset: 0x0005ACB0
		public int InsertOrUpdateIconInfo(IconInfo IconInfo)
		{
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("@iconinfoidnew", DbType.Int32, 0),
				this.DatabaseManager.GetParameter("@iconinfoid", DbType.Int32, IconInfo.IconInfoId),
				this.DatabaseManager.GetParameter("@iconnum", DbType.Int32, IconInfo.IconNum),
				this.DatabaseManager.GetParameter("@icontext", DbType.String, IconInfo.IconText ?? ""),
				this.DatabaseManager.GetParameter("@iconletteridentifier", DbType.String, string.IsNullOrEmpty(IconInfo.IconLetterIdentifier) ? " " : IconInfo.IconLetterIdentifier.Substring(0, 1))
			};
			this.DatabaseManager.ExecuteNonQuery("IF EXISTS(SELECT appointmenticoninfoid FROM appointmenticoninfo WHERE iconindex=@iconnum)\r\nBEGIN\r\n    UPDATE appointmenticoninfo SET icontext=@icontext,iconletteridentifier=@iconletteridentifier WHERE iconindex=@iconnum\r\n    SET @iconinfoidnew=(SELECT TOP 1 appointmenticoninfoid FROM appointmenticoninfo WHERE iconindex=@iconnum)\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO appointmenticoninfo (iconindex,icontext,iconletteridentifier) VALUES (@iconnum,@icontext,@iconletteridentifier)\r\n    SET @iconinfoidnew=SCOPE_IDENTITY()\r\nEND", array);
			int num = (array[0].Value is DBNull) ? 0 : ((int)array[0].Value);
			IconInfo.IconInfoId = num;
			return num;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0005CBB4 File Offset: 0x0005ADB4
		public IList<IconInfo> LoadAllIconInfos()
		{
			IList<IconInfo> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT appointmenticoninfoid,iconindex,icontext,iconletteridentifier FROM appointmenticoninfo ORDER BY iconindex"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<IconInfo> list = new List<IconInfo>();
					while (dataReader.Read())
					{
						IconInfo iconInfoFromRecord = IconInfoDAO.GetIconInfoFromRecord(dataReader);
						bool flag2 = iconInfoFromRecord != null;
						if (flag2)
						{
							list.Add(iconInfoFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}
	}
}
