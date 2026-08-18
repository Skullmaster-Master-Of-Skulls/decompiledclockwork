using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.Tutoring;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.Common.DAO.Impl.Tutoring
{
	// Token: 0x02000033 RID: 51
	public class TutorDAO : ITutorDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00009899 File Offset: 0x00007A99
		public TutorDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000098AB File Offset: 0x00007AAB
		// (set) Token: 0x0600013E RID: 318 RVA: 0x000098B3 File Offset: 0x00007AB3
		public OperationContext OpContext { get; set; }

		// Token: 0x0600013F RID: 319 RVA: 0x000098BC File Offset: 0x00007ABC
		public static MyTutor GetMyTutorFromRecord(IDataReader record, OperationContext opContext)
		{
			bool flag = record == null;
			MyTutor result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Tutor tutorFromRecord = TutorDAO.GetTutorFromRecord(record, opContext);
				bool flag2 = tutorFromRecord == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = new MyTutor
					{
						Tutor = tutorFromRecord,
						StudentPersonId = ((record["StudentPersonId"] is DBNull) ? 0 : ((int)record["StudentPersonId"])),
						LastDateMetWith = (DateTime)record["MaxStartDate"]
					};
				}
			}
			return result;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00009948 File Offset: 0x00007B48
		public static Tutor GetTutorFromRecord(IDataReader record, OperationContext opContext)
		{
			Tutor personBaseFromReader = PeopleDAO.GetPersonBaseFromReader<Tutor>("", record, opContext, null);
			personBaseFromReader.Specializations = TutorDAO.GetStringValueFromRecord(record, "Specialization", opContext);
			personBaseFromReader.PublicNoteFromTutor = TutorDAO.GetStringValueFromRecord(record, "PublicNote", opContext);
			personBaseFromReader.Email = TutorDAO.GetStringValueFromRecord(record, "Email", opContext);
			return personBaseFromReader;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000099A4 File Offset: 0x00007BA4
		private TutorWithActiveStatus GetTutorWIthActiveStatusFromRecord(IDataReader record)
		{
			TutorWithActiveStatus personBaseFromReader = PeopleDAO.GetPersonBaseFromReader<TutorWithActiveStatus>("", record, this.OpContext, null);
			personBaseFromReader.Specializations = TutorDAO.GetStringValueFromRecord(record, "Specialization", this.OpContext);
			personBaseFromReader.PublicNoteFromTutor = TutorDAO.GetStringValueFromRecord(record, "PublicNote", this.OpContext);
			personBaseFromReader.Email = TutorDAO.GetStringValueFromRecord(record, "Email", this.OpContext);
			bool flag = !(record["IsActivated"] is DBNull) && Convert.ToBoolean(record["IsActivated"]);
			personBaseFromReader.Status = (flag ? eTutorStatus.TutorActive : eTutorStatus.TutorNotActive);
			return personBaseFromReader;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00009A48 File Offset: 0x00007C48
		private static string GetStringValueFromRecord(IDataReader record, string colName, OperationContext opContext)
		{
			string text = (record[colName] is DBNull) ? "" : ((string)record[colName]);
			bool flag = !string.IsNullOrEmpty(text);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				string name = colName + "Bytes";
				bool flag2 = record[name] is DBNull;
				if (flag2)
				{
					result = "";
				}
				else
				{
					byte[] encryptedText = (byte[])record[name];
					result = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null).Encryption.Decrypt(encryptedText);
				}
			}
			return result;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00009AE4 File Offset: 0x00007CE4
		public IList<TutorInfo> LoadTutorInfos(int[] tutorPersonIds, int tutorIsAuthorizedCid, int tutorConfidentialityAgreementSignedCid)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] array = new DbParameter[3];
			array[0] = databaseLayer.GetParameter("@pids", DbType.String, string.Join(",", (from g in tutorPersonIds
			select g.ToString()).ToArray<string>()));
			array[1] = databaseLayer.GetParameter("@authcid", DbType.Int32, tutorIsAuthorizedCid);
			array[2] = databaseLayer.GetParameter("@confCid", DbType.Int32, tutorConfidentialityAgreementSignedCid);
			DbParameter[] parameters = array;
			IList<TutorInfo> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\tDISTINCT p.personid,pg.groupid,m.controlvalue AS isauthorized,d.controlvalue AS confsigndate\r\nFROM\tpeople p LEFT JOIN peoplegroups pg ON pg.personid=p.personid AND pg.groupid=5 --tutor group\r\n\t\tLEFT JOIN maininfops m ON m.personid=p.personid AND m.controlid=@authcid\r\n\t\tLEFT JOIN datetimeinfops d ON d.personid=p.personid AND d.controlid=@confCid\r\nWHERE\tp.isactive=1 AND NOT pg.groupid IS NULL\r\n        AND p.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))\r\nORDER BY p.personid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<TutorInfo> list = new List<TutorInfo>();
					while (dataReader.Read())
					{
						list.Add(new TutorInfo
						{
							TutorId = (int)dataReader["personid"],
							IsAuthorized = ((dataReader["isauthorized"] is DBNull) ? null : new bool?((int)dataReader["isauthorized"] != 0)),
							ConfidentialitySignedDate = ((dataReader["confsigndate"] is DBNull) ? null : new DateTime?((DateTime)dataReader["confsigndate"]))
						});
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00009C7C File Offset: 0x00007E7C
		public IList<Tutor> SearchForTutors(string courseSearchString, string SearchString, int TutorIsActiveCid)
		{
			bool flag = string.IsNullOrEmpty(SearchString) && string.IsNullOrEmpty(courseSearchString);
			IList<Tutor> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@coursesearchstring", DbType.String, courseSearchString ?? ""),
					databaseLayer.GetParameter("@searchstring", DbType.String, SearchString),
					databaseLayer.GetParameter("@activecid", DbType.Int32, TutorIsActiveCid)
				};
				using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_Tutoring_TutorSearch", parameters))
				{
					bool flag2 = dataReader != null;
					if (flag2)
					{
						List<Tutor> list = new List<Tutor>();
						while (dataReader.Read())
						{
							Tutor tutorFromRecord = TutorDAO.GetTutorFromRecord(dataReader, this.OpContext);
							bool flag3 = tutorFromRecord != null;
							if (flag3)
							{
								list.Add(tutorFromRecord);
							}
						}
						return list;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00009D88 File Offset: 0x00007F88
		public Tutor LoadTutorByPersonId(int PersonId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PersonId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_Tutoring_TutorByPersonId", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return TutorDAO.GetTutorFromRecord(dataReader, this.OpContext);
				}
			}
			return null;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00009E20 File Offset: 0x00008020
		public IList<TutorWithActiveStatus> LoadAllTutors(int ActiveCid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@activecid", DbType.Int32, ActiveCid)
			};
			IList<TutorWithActiveStatus> result;
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_Tutoring_AllTutors", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<TutorWithActiveStatus> list = new List<TutorWithActiveStatus>();
					while (dataReader.Read())
					{
						TutorWithActiveStatus tutorWIthActiveStatusFromRecord = this.GetTutorWIthActiveStatusFromRecord(dataReader);
						bool flag2 = tutorWIthActiveStatusFromRecord != null;
						if (flag2)
						{
							list.Add(tutorWIthActiveStatusFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}
	}
}
