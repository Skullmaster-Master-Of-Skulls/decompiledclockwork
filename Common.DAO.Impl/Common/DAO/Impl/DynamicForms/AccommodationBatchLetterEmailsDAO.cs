using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.AccommodationBatchLetterEmails;

namespace TechnoPro.Common.DAO.Impl.DynamicForms
{
	// Token: 0x020000D8 RID: 216
	public class AccommodationBatchLetterEmailsDAO : IAccommodationBatchLetterEmailsDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060005D5 RID: 1493 RVA: 0x00036C9A File Offset: 0x00034E9A
		public AccommodationBatchLetterEmailsDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00036CAC File Offset: 0x00034EAC
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x00036CB4 File Offset: 0x00034EB4
		public OperationContext OpContext { get; set; }

		// Token: 0x060005D8 RID: 1496 RVA: 0x00036CC0 File Offset: 0x00034EC0
		private PotentialLetterToSendOut GetPotentialLetterToSendOutFromRecord(IDataReader record)
		{
			bool flag = record == null;
			PotentialLetterToSendOut result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new PotentialLetterToSendOut
				{
					PersonId = ((record["personid"] is DBNull) ? 0 : ((int)record["personid"])),
					LuCourseId = ((record["lucourseid"] is DBNull) ? 0 : ((int)record["lucourseid"])),
					AccommodationsExpiryDate = ((record["expiry"] is DBNull) ? null : new DateTime?((DateTime)record["expiry"])),
					DateLetterLastSent = ((record["datelastsent"] is DBNull) ? null : new DateTime?((DateTime)record["datelastsent"])),
					MaxDateAccommodationsWereModified = ((record["maxdatemodified"] is DBNull) ? null : new DateTime?((DateTime)record["maxdatemodified"]))
				};
			}
			return result;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00036DEC File Offset: 0x00034FEC
		public void MarkLetterSent(int PersonId, int LuCourseId, DateTime DateSent)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PersonId),
				databaseLayer.GetParameter("@lucid", DbType.Int32, LuCourseId),
				databaseLayer.GetParameter("@datesent", DbType.DateTime, DateSent)
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO AccommodationLettersBatchSent (personid,lucourseid,datesent) VALUES (@pid,@lucid,@datesent)", parameters);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00036E68 File Offset: 0x00035068
		public IList<PotentialLetterToSendOut> GetPotentialLettersToSendOut(DateTime Today, int AccommodationExpiryDateCid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@now", DbType.DateTime, Today),
				databaseLayer.GetParameter("@expirydatecid", DbType.Int32, AccommodationExpiryDateCid)
			};
			IList<PotentialLetterToSendOut> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @defaultexpirydate datetime \r\nSET @defaultexpirydate=dateadd(year,1,@now)\r\n\r\nSELECT DISTINCT x.personid,x.lucourseid,MAX(x.maxdatemodified) AS maxdatemodified \r\nINTO #t2\r\nFROM\r\n(\r\n\tSELECT DISTINCT personid,lucourseid,MAX(dateentered) AS maxdatemodified FROM archive_maininfoaccommodationps GROUP BY personid,lucourseid\r\n\tUNION\r\n\tSELECT DISTINCT personid,lucourseid,MAX(dateentered) AS maxdatemodified FROM archive_otherinfoaccommodationps GROUP BY personid,lucourseid\r\n\tUNION\r\n\tSELECT DISTINCT personid,lucourseid,MAX(dateentered) AS maxdatemodified FROM archive_datetimeinfoaccommodationps GROUP BY personid,lucourseid\r\n) x GROUP BY x.personid,x.lucourseid\r\n\r\nSELECT DISTINCT c.personid,c.lucourseid,COALESCE(d.controlvalue,@defaultexpirydate) AS expiry,t2.maxdatemodified,MAX(ab.datesent) AS datelastsent\r\nFROM\tcourses c LEFT JOIN people p ON p.personid=c.personid\r\n\t\tLEFT JOIN DateTimeInfoAccommodationPS d ON d.personid=c.personid AND NOT @expirydatecid IS NULL AND d.controlid=@expirydatecid\r\n\t\tLEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n\t\tLEFT JOIN #t2 t2 ON t2.personid=c.personid AND t2.lucourseid=dbo.AccommodationsCourseOrTemplate(c.personid,c.lucourseid)\r\n\t\tLEFT JOIN AccommodationLettersBatchSent ab ON ab.personid=c.personid AND ab.lucourseid=c.lucourseid\r\nWHERE\tNOT p.personid IS NULL AND p.isactive=1\r\n\t\tAND (c.registrationstatus is null OR NOT c.registrationstatus=2)\r\n\t\tAND NOT luc.lucourseid IS NULL \r\n\t\tAND luc.enddate>=@now\r\n\t\tAND COALESCE(d.controlvalue,@defaultexpirydate) >= @now\r\nGROUP BY c.personid,c.lucourseid,COALESCE(d.controlvalue,@defaultexpirydate),t2.maxdatemodified\r\nORDER BY c.personid,c.lucourseid,datelastsent DESC\r\n\r\nDROP TABLE #t2", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<PotentialLetterToSendOut> list = new List<PotentialLetterToSendOut>();
					while (dataReader.Read())
					{
						PotentialLetterToSendOut item = this.GetPotentialLetterToSendOutFromRecord(dataReader);
						bool flag2 = item != null && list.FirstOrDefault((PotentialLetterToSendOut g) => g.PersonId == item.PersonId && g.LuCourseId == item.LuCourseId) == null;
						if (flag2)
						{
							list.Add(item);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00036F64 File Offset: 0x00035164
		public IDictionary<int, DateTime?> GetBatchLetterSentDates(int PersonId, IList<int> LuCourseIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[2];
			array[0] = databaseLayer.GetParameter("@pid", DbType.Int32, PersonId);
			array[1] = databaseLayer.GetParameter("@lucids", DbType.String, string.Join(",", LuCourseIds.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray()));
			DbParameter[] parameters = array;
			IDictionary<int, DateTime?> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT DISTINCT personid,lucourseid,max(datesent) AS datesent \r\nFROM AccommodationLettersBatchSent\r\nWHERE personid=@pid AND lucourseid IN (SELECT orderid AS lucourseid FROM splitorderids(@lucids,','))\r\nGROUP BY personid,lucourseid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					Dictionary<int, DateTime?> dictionary = new Dictionary<int, DateTime?>();
					foreach (int key in LuCourseIds)
					{
						bool flag2 = !dictionary.ContainsKey(key);
						if (flag2)
						{
							dictionary.Add(key, null);
						}
					}
					while (dataReader.Read())
					{
						int key2 = (dataReader["lucourseid"] is DBNull) ? 0 : ((int)dataReader["lucourseid"]);
						bool flag3 = dataReader["datesent"] != DBNull.Value && dictionary.ContainsKey(key2);
						if (flag3)
						{
							dictionary[key2] = new DateTime?((DateTime)dataReader["datesent"]);
						}
					}
					result = dictionary;
				}
			}
			return result;
		}
	}
}
