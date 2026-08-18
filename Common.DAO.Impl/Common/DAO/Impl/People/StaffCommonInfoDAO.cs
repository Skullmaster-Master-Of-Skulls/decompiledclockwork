using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.People
{
	// Token: 0x02000074 RID: 116
	public class StaffCommonInfoDAO : IStaffCommonInfoDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x00017795 File Offset: 0x00015995
		public StaffCommonInfoDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x000177C5 File Offset: 0x000159C5
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x000177CD File Offset: 0x000159CD
		public OperationContext OpContext { get; set; }

		// Token: 0x060002CA RID: 714 RVA: 0x000177D8 File Offset: 0x000159D8
		private static StaffCommonInfo GetStaffCommonInfoFromRecord(IDataReader record, OperationContext opContext, IBatchDecryptor batchDecryptor = null, string colPrefix = null)
		{
			string colName = (colPrefix ?? "") + "email";
			string colName2 = (colPrefix ?? "") + "phone";
			string colName3 = (colPrefix ?? "") + "title";
			string text = (colPrefix ?? "") + "signaturedataid";
			string text2 = (colPrefix ?? "") + "personid";
			return new StaffCommonInfo
			{
				Email = (record.ContainsColumn(colName) ? StaffCommonInfoDAO.GetStringFromRecord(record, "email", "emailIsEncrypted", opContext, batchDecryptor, colPrefix) : ""),
				Phone = (record.ContainsColumn(colName2) ? StaffCommonInfoDAO.GetStringFromRecord(record, "phone", "phoneIsEncrypted", opContext, batchDecryptor, colPrefix) : ""),
				Title = (record.ContainsColumn(colName3) ? StaffCommonInfoDAO.GetStringFromRecord(record, "title", "titleIsEncrypted", opContext, batchDecryptor, colPrefix) : ""),
				SignatureDataId = (record.ContainsColumn(text) ? ((record[text] is DBNull) ? 0 : ((int)record[text])) : 0),
				PersonId = (record.ContainsColumn(text2) ? ((record[text2] is DBNull) ? 0 : ((int)record[text2])) : 0)
			};
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00017948 File Offset: 0x00015B48
		private static string GetStringFromRecord(IDataRecord record, string colName, string nameIsEncrypted, OperationContext opContext, IBatchDecryptor batchDecryptor = null, string colPrefix = null)
		{
			string name = (colPrefix ?? "") + colName;
			string name2 = (colPrefix ?? "") + nameIsEncrypted;
			bool flag = record[name] is DBNull;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				byte[] array = (byte[])record[name];
				bool flag2 = array.Length < 1;
				if (flag2)
				{
					result = "";
				}
				else
				{
					bool flag3 = record[name2] != DBNull.Value && (bool)record[name2];
					bool flag4 = !flag3;
					if (flag4)
					{
						result = Encoding.ASCII.GetString(array);
					}
					else
					{
						result = ((batchDecryptor == null) ? DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null).Encryption.Decrypt(array) : batchDecryptor.Decrypt(array));
					}
				}
			}
			return result;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00017A28 File Offset: 0x00015C28
		public StaffWithCommonInfo LoadStaffWithCommonInfoById(int PersonId)
		{
			return this.LoadStaffWithCommonInfoById<StaffWithCommonInfo>(PersonId);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00017A44 File Offset: 0x00015C44
		public T LoadStaffWithCommonInfoById<T>(int PersonId) where T : StaffWithCommonInfo
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PersonId),
				databaseLayer.GetParameter("@staffgrouponly", DbType.Boolean, false)
			};
			T result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("EXEC CommonStaff @pid,@staffgrouponly", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = default(T);
				}
				else
				{
					result = StaffCommonInfoDAO.GetStaffWithCommonInfoFromRecord<T>(dataReader, this.OpContext, null, null);
				}
			}
			return result;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00017AFC File Offset: 0x00015CFC
		public static T GetStaffWithCommonInfoFromRecord<T>(IDataReader record, OperationContext opContext, IBatchDecryptor batchDecryptor = null, string colPrefix = null) where T : StaffWithCommonInfo
		{
			T t = Activator.CreateInstance<T>();
			t.Staff = PeopleDAO.GetPersonFromReader(colPrefix ?? "", record, opContext, batchDecryptor);
			t.StaffCommonInfo = StaffCommonInfoDAO.GetStaffCommonInfoFromRecord(record, opContext, batchDecryptor, colPrefix);
			return t;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00017B48 File Offset: 0x00015D48
		public IList<T> LoadStaffWithCommonInfoByGroupTitle<T>(params string[] GroupTitles) where T : StaffWithCommonInfo
		{
			bool flag = GroupTitles.Length > 1;
			string value;
			if (flag)
			{
				string[] array = new string[GroupTitles.Length - 1];
				for (int i = 1; i <= array.Length; i++)
				{
					array[i - 1] = GroupTitles[i];
				}
				value = string.Join(",", array);
			}
			else
			{
				value = "";
			}
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@grouptitle", DbType.String, GroupTitles[0]),
				databaseLayer.GetParameter("@altgrouptitle", DbType.String, value)
			};
			IList<T> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("EXEC CommonStaffByGroup @grouptitle,@altgrouptitle", parameters))
			{
				bool flag2 = dataReader == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					List<T> list = new List<T>();
					while (dataReader.Read())
					{
						T staffWithCommonInfoFromRecord = StaffCommonInfoDAO.GetStaffWithCommonInfoFromRecord<T>(dataReader, this.OpContext, batchDecryptor, null);
						bool flag3 = staffWithCommonInfoFromRecord != null;
						if (flag3)
						{
							list.Add(staffWithCommonInfoFromRecord);
						}
					}
					list.Sort((T g1, T g2) => this.GetSortString(g1).CompareTo(this.GetSortString(g2)));
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00017C98 File Offset: 0x00015E98
		private string GetSortString(StaffWithCommonInfo swci)
		{
			bool flag = swci == null || swci.Staff == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = (swci.Staff.LastName ?? "") + ", " + swci.Staff.FirstName;
			}
			return result;
		}

		// Token: 0x04000129 RID: 297
		private DatabaseLayer DatabaseManager;
	}
}
