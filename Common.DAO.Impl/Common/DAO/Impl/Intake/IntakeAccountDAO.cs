using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.DAO.Intake;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Intake;

namespace TechnoPro.Common.DAO.Impl.Intake
{
	// Token: 0x020000C6 RID: 198
	public class IntakeAccountDAO : IIntakeAccountDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000551 RID: 1361 RVA: 0x0003320F File Offset: 0x0003140F
		public IntakeAccountDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00033221 File Offset: 0x00031421
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x00033229 File Offset: 0x00031429
		public OperationContext OpContext { get; set; }

		// Token: 0x06000554 RID: 1364 RVA: 0x00033234 File Offset: 0x00031434
		public int CreateNewIntakeAccount(IntakeUserAccount UserAccount)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@pid", DbType.Int32, 0),
				databaseLayer.GetParameter("@fne", DbType.Binary, encryption.Encrypt(UserAccount.FirstName ?? "")),
				databaseLayer.GetParameter("@mne", DbType.Binary, encryption.Encrypt(UserAccount.MiddleName ?? "")),
				databaseLayer.GetParameter("@lne", DbType.Binary, encryption.Encrypt(UserAccount.LastName ?? "")),
				databaseLayer.GetParameter("@sne", DbType.Binary, encryption.Encrypt(UserAccount.StudentNumber ?? "")),
				databaseLayer.GetParameter("@email", DbType.Binary, encryption.Encrypt(UserAccount.Email ?? "")),
				databaseLayer.GetParameter("@ip", DbType.Binary, encryption.Encrypt(UserAccount.IpAddress ?? ""))
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO people_intake (firstname,lastname,middlename,student_no,email,isactive,ip,dateadded,note) VALUES (@fne,@lne,@mne,@sne,@email,1,@ip,getdate(),NULL)\r\nSET @pid=(SELECT TOP 1 CAST(@@identity AS int) AS pid FROM people_intake)", array);
			bool flag = array[0].Value == null || array[0].Value is DBNull;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = (int)array[0].Value;
			}
			return result;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00033398 File Offset: 0x00031598
		private T GetIntakeEntryFromRecord<T>(IDataRecord record, IBatchDecryptor batchDecryptor, out int intakePersonId) where T : IntakeEntry
		{
			intakePersonId = ((record["personid"] is DBNull) ? 0 : ((int)record["personid"]));
			bool flag = intakePersonId == 0;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				T t = Activator.CreateInstance<T>();
				t.StudentNumber = ((record["student_no"] is DBNull) ? null : batchDecryptor.Decrypt((byte[])record["student_no"]).Trim().ToUpper());
				t.FirstName = ((record["firstname"] is DBNull) ? null : batchDecryptor.Decrypt((byte[])record["firstname"]));
				t.MiddleName = ((record["middlename"] is DBNull) ? null : batchDecryptor.Decrypt((byte[])record["middlename"]));
				t.LastName = ((record["lastname"] is DBNull) ? null : batchDecryptor.Decrypt((byte[])record["lastname"]));
				t.Email = ((record["email"] is DBNull) ? null : batchDecryptor.Decrypt((byte[])record["email"]));
				t.Ip = record["ip"].ToString().Trim();
				t.DateAdded = (DateTime)record["dateadded"];
				t.Note = ((record["note"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["note"]));
				t.Status = this.GetIntakeStatusFromRecord(record);
				t.ExistingClockWorkStudentPersonId = ((IntakeAccountDAO.ReaderContainsColumn(record, "existingpid") && !(record["existingpid"] is DBNull)) ? ((int)record["existingpid"]) : 0);
				result = t;
			}
			return result;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x000335D8 File Offset: 0x000317D8
		private static bool ReaderContainsColumn(IDataRecord record, string colName)
		{
			for (int i = 0; i < record.FieldCount; i++)
			{
				bool flag = record.GetName(i).Equals(colName, StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00033618 File Offset: 0x00031818
		public void UpdateActiveIntakeStatus(int[] intakePersonIds, Guid newIntakeStatusId)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] array = new DbParameter[2];
			array[0] = databaseLayer.GetParameter("@pids", DbType.String, string.Join(",", (from g in intakePersonIds ?? new int[0]
			select g.ToString()).ToArray<string>()));
			array[1] = databaseLayer.GetParameter("@intakeStatusId", DbType.Guid, (newIntakeStatusId == Guid.Empty) ? DBNull.Value : newIntakeStatusId);
			DbParameter[] parameters = array;
			databaseLayer.ExecuteNonQuery("UPDATE people_intake SET IntakeStatusId=@intakeStatusId WHERE personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))", parameters);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x000336C4 File Offset: 0x000318C4
		public void UpdateActiveIntakeNote(int[] intakePersonIds, string newNote)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			string text = (newNote ?? "").Trim();
			DbParameter[] array = new DbParameter[2];
			array[0] = databaseLayer.GetParameter("@pids", DbType.String, string.Join(",", (from g in intakePersonIds ?? new int[0]
			select g.ToString()).ToArray<string>()));
			array[1] = databaseLayer.GetParameter("@note", DbType.Binary, (text.Length > 0) ? databaseLayer.Encryption.Encrypt(text) : DBNull.Value);
			DbParameter[] parameters = array;
			databaseLayer.ExecuteNonQuery("UPDATE people_intake SET note=@note WHERE personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))", parameters);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00033784 File Offset: 0x00031984
		public void MarkIntakesInactiveByStudentNumber(string student_no)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@snum", DbType.Binary, databaseLayer.Encryption.Encrypt((student_no ?? "").Trim().ToUpper()))
			};
			databaseLayer.ExecuteNonQuery("UPDATE people_intake SET isactive=0 WHERE student_no=@snum", parameters);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000337E8 File Offset: 0x000319E8
		public void MarkIntakesInactiveByPersonIds(int[] intakePersonIds)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] array = new DbParameter[1];
			array[0] = databaseLayer.GetParameter("@pids", DbType.String, string.Join(",", (from g in intakePersonIds
			select g.ToString()).ToArray<string>()));
			DbParameter[] parameters = array;
			databaseLayer.ExecuteNonQuery("UPDATE people_intake SET isactive=0 WHERE personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))", parameters);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00033860 File Offset: 0x00031A60
		public IList<IntakeStatus> LoadLookupStatuses()
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			IList<IntakeStatus> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT IntakeStatusId,Title,[Description],BackgroundColor,OrderNum,IsInactive FROM people_intake_status WHERE isinactive=0 ORDER BY OrderNum,Title"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<IntakeStatus> list = new List<IntakeStatus>();
					while (dataReader.Read())
					{
						IntakeStatus intakeStatusFromRecord = this.GetIntakeStatusFromRecord(dataReader);
						bool flag2 = intakeStatusFromRecord == null;
						if (!flag2)
						{
							list.Add(intakeStatusFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000338F4 File Offset: 0x00031AF4
		public IList<DynamicData> LoadIntakeFormData(string snum, int intakeFormScreenNum)
		{
			string text = (snum ?? "").Trim().ToUpper();
			bool flag = text.Length < 1;
			if (flag)
			{
				throw new Exception("IntakeAccountDAO:LoadIntakeFormData:Can't load intake form data for empty student number");
			}
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@snume", DbType.Binary, databaseLayer.Encryption.Encrypt(text)),
				databaseLayer.GetParameter("@screennum", DbType.Int32, intakeFormScreenNum)
			};
			IList<DynamicData> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT personid INTO #tpids FROM people_intake WHERE student_no=@snume\r\nSELECT controlid INTO #tcids FROM dynamicscreencontrols WHERE screennum=@screennum\r\nSELECT #tcids.controlid,#tpids.personid INTO #tpc FROM #tcids LEFT JOIN #tpids ON 1=1\r\n\r\nSELECT\tx.controlid,MAX(x.personid) AS personid \r\nINTO #t1\r\nFROM \r\n(\r\nSELECT\t#tpc.controlid,#tpc.personid\r\nFROM\t#tpc LEFT JOIN maininfoIntake m ON m.controlid=#tpc.controlid AND m.personid=#tpc.personid\r\nWHERE\tNOT m.dataid IS NULL\r\nUNION ALL\r\nSELECT\t#tpc.controlid,#tpc.personid\r\nFROM\t#tpc LEFT JOIN otherinfoIntake m ON m.controlid=#tpc.controlid AND m.personid=#tpc.personid\r\nWHERE\tNOT m.dataid IS NULL\r\nUNION ALL\r\nSELECT\t#tpc.controlid,#tpc.personid\r\nFROM\t#tpc LEFT JOIN datetimeinfoIntake m ON m.controlid=#tpc.controlid AND m.personid=#tpc.personid\r\nWHERE\tNOT m.dataid IS NULL\r\nUNION ALL\r\nSELECT\t#tpc.controlid,#tpc.personid\r\nFROM\t#tpc LEFT JOIN imageinfoIntake m ON m.controlid=#tpc.controlid AND m.personid=#tpc.personid\r\nWHERE\tNOT m.dataid IS NULL\r\n) x GROUP BY x.controlid\r\n\r\nSELECT\t#t1.personid,#t1.controlid,m.*\r\nFROM\t#t1 LEFT JOIN perintakedata2 m ON m.personid=#t1.personid AND m.controlid=#t1.controlid \r\n\t\t\r\n\r\nDROP TABLE #tpids\r\nDROP TABLE #tcids\r\nDROP TABLE #tpc\r\nDROP TABLE #t1", parameters))
			{
				bool flag2 = dataReader == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					DynamicDataDAO dynamicDataDAO = new DynamicDataDAO(this.OpContext);
					result = dynamicDataDAO.GetDataListFromRecords(dataReader);
				}
			}
			return result;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x000339D4 File Offset: 0x00031BD4
		private IntakeStatus GetIntakeStatusFromRecord(IDataRecord record)
		{
			Guid guid = (record["IntakeStatusId"] is DBNull) ? Guid.Empty : ((Guid)record["IntakeStatusId"]);
			bool flag = guid == Guid.Empty;
			IntakeStatus result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new IntakeStatus
				{
					IntakeStatusId = guid,
					Title = ((record["Title"] is DBNull) ? null : ((string)record["Title"])),
					Description = ((record["Description"] is DBNull) ? null : ((string)record["Description"])),
					BackgroundColor = ((record["BackgroundColor"] is DBNull) ? 0 : ((int)record["BackgroundColor"])),
					IsInactive = (!(record["IsInActive"] is DBNull) && Convert.ToBoolean(record["IsInActive"])),
					OrderNum = ((record["OrderNum"] is DBNull) ? 0 : ((int)record["OrderNum"]))
				};
			}
			return result;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00033B14 File Offset: 0x00031D14
		public IntakePerson LoadIntakePersonByStudentNumber(string snum)
		{
			string text = (snum ?? "").Trim().ToUpper();
			bool flag = text.Length < 1;
			if (flag)
			{
				throw new Exception("IntakeAccountDAO:LoadIntakePersonByStudentNumber:Can't load intake person for empty student number");
			}
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@snume", DbType.Binary, databaseLayer.Encryption.Encrypt(text))
			};
			IntakePerson result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT personid,firstname,middlename,lastname,student_no,email,dateadded FROM people_intake WHERE student_no=@snume AND isactive=1", parameters))
			{
				bool flag2 = dataReader == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					if (!dataReader.Read())
					{
						result = null;
					}
					else
					{
						result = new IntakePerson
						{
							StudentNumber = snum,
							FirstName = ((dataReader["firstname"] is DBNull) ? null : batchDecryptor.Decrypt((byte[])dataReader["firstname"])),
							MiddleName = ((dataReader["middlename"] is DBNull) ? null : batchDecryptor.Decrypt((byte[])dataReader["middlename"])),
							LastName = ((dataReader["lastname"] is DBNull) ? null : batchDecryptor.Decrypt((byte[])dataReader["lastname"])),
							Email = ((dataReader["email"] is DBNull) ? null : batchDecryptor.Decrypt((byte[])dataReader["email"]))
						};
					}
				}
			}
			return result;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00033CE8 File Offset: 0x00031EE8
		public IList<IntakeEntry> LoadPendingIntakeEntries()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IList<IntakeEntry> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\tp.personid,p.firstname,p.middlename,p.lastname,p.student_no,p.email,p.isactive,p.[ip],p.dateadded,\r\n\t\tp.IntakeStatusId,pis.title,pis.[description],pis.backgroundcolor,pis.IsInactive,pis.OrderNum,p.note,\r\n        pp.personid AS existingpid\r\nFROM\tpeople_intake p LEFT JOIN people_intake_status pis ON pis.IntakeStatusId=p.IntakeStatusId\r\n        LEFT JOIN people pp ON pp.student_no=p.student_no\r\nWHERE\tp.isactive=1\r\nORDER BY dateadded DESC"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<IntakeAccountDAO.IntakeEntryPart> list = new List<IntakeAccountDAO.IntakeEntryPart>();
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						int personId;
						IntakeEntry intakeEntryFromRecord = this.GetIntakeEntryFromRecord<IntakeEntry>(dataReader, batchDecryptor, out personId);
						bool flag2 = intakeEntryFromRecord == null;
						if (!flag2)
						{
							list.Add(new IntakeAccountDAO.IntakeEntryPart
							{
								PersonId = personId,
								IntakeEntry = intakeEntryFromRecord
							});
						}
					}
					list.Sort(delegate(IntakeAccountDAO.IntakeEntryPart g1, IntakeAccountDAO.IntakeEntryPart g2)
					{
						int num = g1.IntakeEntry.StudentNumber.CompareTo(g2.IntakeEntry.StudentNumber);
						bool flag6 = num != 0;
						int result2;
						if (flag6)
						{
							result2 = num;
						}
						else
						{
							result2 = g2.IntakeEntry.DateAdded.CompareTo(g1.IntakeEntry.DateAdded);
						}
						return result2;
					});
					int i = 0;
					List<IntakeEntry> list2 = new List<IntakeEntry>();
					while (i < list.Count)
					{
						IntakeAccountDAO.IntakeEntryPart intakeEntryPart = list[i];
						string studentNumber = intakeEntryPart.IntakeEntry.StudentNumber;
						int j = i + 1;
						List<int> list3 = new List<int>
						{
							intakeEntryPart.PersonId
						};
						while (j < list.Count)
						{
							IntakeAccountDAO.IntakeEntryPart intakeEntryPart2 = list[j];
							string studentNumber2 = intakeEntryPart2.IntakeEntry.StudentNumber;
							bool flag3 = studentNumber2 != studentNumber;
							if (flag3)
							{
								break;
							}
							list3.Add(intakeEntryPart2.PersonId);
							bool flag4 = intakeEntryPart.IntakeEntry.Status == null;
							if (flag4)
							{
								intakeEntryPart.IntakeEntry.Status = intakeEntryPart2.IntakeEntry.Status;
							}
							bool flag5 = string.IsNullOrEmpty(intakeEntryPart.IntakeEntry.Note);
							if (flag5)
							{
								intakeEntryPart.IntakeEntry.Note = intakeEntryPart2.IntakeEntry.Note;
							}
							j++;
						}
						intakeEntryPart.IntakeEntry.PersonIds = list3.Distinct<int>().ToArray<int>();
						list2.Add(intakeEntryPart.IntakeEntry);
						i = j;
					}
					result = list2;
				}
			}
			return result;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00033F18 File Offset: 0x00032118
		public IList<IntakeEntryQueueItem> LoadPendingIntakeEntryQueueItems(int departmentControlId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@cid", DbType.Int32, departmentControlId)
			};
			IList<IntakeEntryQueueItem> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\tp.personid,p.firstname,p.middlename,p.lastname,p.student_no,p.email,p.isactive,p.[ip],p.dateadded,\r\n\t\tp.IntakeStatusId,pis.title,pis.[description],pis.backgroundcolor,pis.IsInactive,pis.OrderNum,p.note,\r\n        pp.personid AS existingpid,\r\n        mii.controlvalue AS seldeptid,ll.lookuptext AS seldepttitle\r\nFROM\tpeople_intake p LEFT JOIN people_intake_status pis ON pis.IntakeStatusId=p.IntakeStatusId\r\n        LEFT JOIN people pp ON pp.student_no=p.student_no\r\n        LEFT JOIN maininfointake mii ON mii.personid=p.personid AND mii.controlid=@cid AND @cid>0\r\n        LEFT JOIN lookuplists ll ON ll.lookuplistid=mii.controlvalue\r\nWHERE\tp.isactive=1\r\nORDER BY dateadded DESC", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<IntakeAccountDAO.IntakeEntryQueueItemPart> list = new List<IntakeAccountDAO.IntakeEntryQueueItemPart>();
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						int personId;
						IntakeEntryQueueItem intakeEntryFromRecord = this.GetIntakeEntryFromRecord<IntakeEntryQueueItem>(dataReader, batchDecryptor, out personId);
						bool flag2 = intakeEntryFromRecord == null;
						if (!flag2)
						{
							intakeEntryFromRecord.SelectedDepartmentValue = ((dataReader["seldeptid"] is DBNull) ? 0 : ((int)dataReader["seldeptid"]));
							intakeEntryFromRecord.SelectedDepartmentTitle = ((dataReader["seldepttitle"] is DBNull) ? "" : ((string)dataReader["seldepttitle"]));
							list.Add(new IntakeAccountDAO.IntakeEntryQueueItemPart
							{
								PersonId = personId,
								IntakeEntry = intakeEntryFromRecord
							});
						}
					}
					list.Sort(delegate(IntakeAccountDAO.IntakeEntryQueueItemPart g1, IntakeAccountDAO.IntakeEntryQueueItemPart g2)
					{
						int num = g1.IntakeEntry.StudentNumber.CompareTo(g2.IntakeEntry.StudentNumber);
						bool flag6 = num != 0;
						int result2;
						if (flag6)
						{
							result2 = num;
						}
						else
						{
							result2 = g2.IntakeEntry.DateAdded.CompareTo(g1.IntakeEntry.DateAdded);
						}
						return result2;
					});
					int i = 0;
					List<IntakeEntryQueueItem> list2 = new List<IntakeEntryQueueItem>();
					while (i < list.Count)
					{
						IntakeAccountDAO.IntakeEntryQueueItemPart intakeEntryQueueItemPart = list[i];
						string studentNumber = intakeEntryQueueItemPart.IntakeEntry.StudentNumber;
						int j = i + 1;
						List<int> list3 = new List<int>
						{
							intakeEntryQueueItemPart.PersonId
						};
						while (j < list.Count)
						{
							IntakeAccountDAO.IntakeEntryQueueItemPart intakeEntryQueueItemPart2 = list[j];
							string studentNumber2 = intakeEntryQueueItemPart2.IntakeEntry.StudentNumber;
							bool flag3 = studentNumber2 != studentNumber;
							if (flag3)
							{
								break;
							}
							list3.Add(intakeEntryQueueItemPart2.PersonId);
							bool flag4 = intakeEntryQueueItemPart.IntakeEntry.Status == null;
							if (flag4)
							{
								intakeEntryQueueItemPart.IntakeEntry.Status = intakeEntryQueueItemPart2.IntakeEntry.Status;
							}
							bool flag5 = string.IsNullOrEmpty(intakeEntryQueueItemPart.IntakeEntry.Note);
							if (flag5)
							{
								intakeEntryQueueItemPart.IntakeEntry.Note = intakeEntryQueueItemPart2.IntakeEntry.Note;
							}
							j++;
						}
						intakeEntryQueueItemPart.IntakeEntry.PersonIds = list3.Distinct<int>().ToArray<int>();
						list2.Add(intakeEntryQueueItemPart.IntakeEntry);
						i = j;
					}
					result = list2;
				}
			}
			return result;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000341CC File Offset: 0x000323CC
		public int[] LoadIntakePersonIdsByStudentNumber(string snum)
		{
			string text = (snum ?? "").Trim().ToUpper();
			bool flag = text.Length < 1;
			if (flag)
			{
				throw new Exception("IntakeAccountDAO:LoadIntakePersonByStudentNumber:Can't load intake person ids for empty student number");
			}
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@snume", DbType.Binary, databaseLayer.Encryption.Encrypt(text))
			};
			int[] result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT personid FROM people_intake WHERE isactive=1 AND student_no=@snume", parameters))
			{
				bool flag2 = dataReader == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					List<int> list = new List<int>();
					while (dataReader.Read())
					{
						int num = (dataReader["personid"] is DBNull) ? 0 : ((int)dataReader["personid"]);
						bool flag3 = num > 0;
						if (flag3)
						{
							list.Add(num);
						}
					}
					result = list.Distinct<int>().ToArray<int>();
				}
			}
			return result;
		}

		// Token: 0x020001FE RID: 510
		internal class IntakeEntryPart
		{
			// Token: 0x17000140 RID: 320
			// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x00081DD6 File Offset: 0x0007FFD6
			// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x00081DDE File Offset: 0x0007FFDE
			public int PersonId { get; set; }

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x00081DE7 File Offset: 0x0007FFE7
			// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x00081DEF File Offset: 0x0007FFEF
			public IntakeEntry IntakeEntry { get; set; }
		}

		// Token: 0x020001FF RID: 511
		internal class IntakeEntryQueueItemPart
		{
			// Token: 0x17000142 RID: 322
			// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x00081DF8 File Offset: 0x0007FFF8
			// (set) Token: 0x06000CFA RID: 3322 RVA: 0x00081E00 File Offset: 0x00080000
			public int PersonId { get; set; }

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x06000CFB RID: 3323 RVA: 0x00081E09 File Offset: 0x00080009
			// (set) Token: 0x06000CFC RID: 3324 RVA: 0x00081E11 File Offset: 0x00080011
			public IntakeEntryQueueItem IntakeEntry { get; set; }
		}
	}
}
