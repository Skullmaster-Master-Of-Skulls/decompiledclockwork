using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Legacy.ActionPlan;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;

namespace TechnoPro.Common.DAO.Impl.Legacy
{
	// Token: 0x020000A5 RID: 165
	public class LegacyActionPlanDAO : ILegacyActionPlanDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600047F RID: 1151 RVA: 0x0002903C File Offset: 0x0002723C
		public LegacyActionPlanDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0002904E File Offset: 0x0002724E
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x00029056 File Offset: 0x00027256
		public OperationContext OpContext { get; set; }

		// Token: 0x06000482 RID: 1154 RVA: 0x00029060 File Offset: 0x00027260
		public int CreateActionPlanNote(ActionPlanNote note)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("noteid", DbType.Int32, 0),
				databaseLayer.GetParameter("@personid", DbType.Int32, note.PersonId),
				databaseLayer.GetParameter("@whoadded", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@wholastmodified", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@notegroup", DbType.String, (note.NoteGroup ?? "").Trim()),
				databaseLayer.GetParameter("@notedescription", DbType.String, (note.NoteDescription ?? "").Trim()),
				databaseLayer.GetParameter("@staffnotes", DbType.Int32, databaseLayer.Encryption.Encrypt(note.StaffNotes ?? ""))
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO studentfiletasks_note (personid,whoadded,wholastmodified,notegroup,notedescription,staffnotes) VALUES (@personid,@whoadded,@wholastmodified,@notegroup,@notedescription,@staffnotes)\r\nSET @noteid=(SELECT TOP 1 CAST(@@identity AS int) AS noteid FROM studentfiletasks_note)", array);
			return ((int?)array[0].Value).GetValueOrDefault();
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00029194 File Offset: 0x00027394
		public void UpdateActionPlanNote(ActionPlanNote note)
		{
			bool flag = note.NoteId < 1;
			if (flag)
			{
				throw new InvalidParameterException();
			}
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("noteid", DbType.Int32, note.NoteId),
				databaseLayer.GetParameter("@personid", DbType.Int32, note.PersonId),
				databaseLayer.GetParameter("@whoadded", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@wholastmodified", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@notegroup", DbType.String, (note.NoteGroup ?? "").Trim()),
				databaseLayer.GetParameter("@notedescription", DbType.String, (note.NoteDescription ?? "").Trim()),
				databaseLayer.GetParameter("@staffnotes", DbType.Int32, databaseLayer.Encryption.Encrypt(note.StaffNotes ?? ""))
			};
			databaseLayer.ExecuteNonQuery("UPDATE studentfiletasks_note SET datelastmodified=getdate(),wholastmodified=@whoami,notegroup=@notegroup,notedescription=@notedescription,staffnotes=@staffnotes WHERE noteid=@noteid", parameters);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x000292CC File Offset: 0x000274CC
		public void DeleteActionPlanNote(int noteId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@noteid", DbType.Int32, noteId)
			};
			databaseLayer.ExecuteNonQuery("DELETE FROM studentfiletasks_note WHERE noteid=@noteid", parameters);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00029320 File Offset: 0x00027520
		private ActionPlanNote GetNoteFromRecord(IDataRecord record, IBatchDecryptor batchDecryptor)
		{
			bool flag = record == null;
			ActionPlanNote result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new ActionPlanNote
				{
					NoteId = ((record["noteid"] is DBNull) ? 0 : ((int)record["noteid"])),
					PersonId = ((record["personid"] is DBNull) ? 0 : ((int)record["personid"])),
					DateLastModified = ((record["datelastmodified"] is DBNull) ? null : new DateTime?((DateTime)record["datelastmodified"])),
					WhoAddedPersonId = ((record["whoadded"] is DBNull) ? 0 : ((int)record["whoadded"])),
					WhoLastModifiedPersonId = ((record["wholastmodified"] is DBNull) ? 0 : ((int)record["wholastmodified"])),
					NoteGroup = record["notegroup"].ToString().Trim(),
					NoteDescription = record["notedescription"].ToString().Trim(),
					StaffNotes = ((record["staffnotes"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["staffnotes"])),
					DateAdded = ((record["dateadded"] is DBNull) ? DateTime.MinValue : ((DateTime)record["dateadded"])),
					WhoLastModified = ((record["wholastmodified"] is DBNull) ? null : new PersonBase
					{
						LastName = ((record["lastname"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["lastname"])),
						FirstName = ((record["firstname"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["firstname"]))
					})
				};
			}
			return result;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00029564 File Offset: 0x00027764
		public IList<ActionPlanNote> LoadNotes(int personId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, personId)
			};
			IList<ActionPlanNote> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT n.noteid,n.personid,n.datelastmodified,n.whoadded,n.wholastmodified,'' AS who,n.notegroup,n.notedescription,n.staffnotes\r\n,p.firstname,p.lastname,n.dateadded\r\nFROM studentfiletasks_note n LEFT JOIN people p ON p.personid=n.wholastmodified\r\nWHERE n.personid=@pid\r\nORDER BY n.datelastmodified", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<ActionPlanNote> list = new List<ActionPlanNote>();
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						ActionPlanNote noteFromRecord = this.GetNoteFromRecord(dataReader, batchDecryptor);
						bool flag2 = noteFromRecord != null;
						if (flag2)
						{
							list.Add(noteFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0002962C File Offset: 0x0002782C
		public void UpdateActionPlanTask(ActionPlanTask task)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[7];
			array[0] = databaseLayer.GetParameter("@taskid", DbType.Int32, task.TaskId);
			array[1] = databaseLayer.GetParameter("@whoresponsiblecode", DbType.Int32, task.WhoResponsibleCode);
			array[2] = databaseLayer.GetParameter("@whoami", DbType.Int32, this.OpContext.WhoAmI);
			array[3] = databaseLayer.GetParameter("@description", DbType.String, task.Description ?? "");
			int num = 4;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@completedid";
			DbType pType = DbType.Int32;
			int? completedId = task.CompletedId;
			array[num] = databaseLayer2.GetParameter(pName, pType, (completedId != null) ? completedId.GetValueOrDefault() : DBNull.Value);
			array[5] = databaseLayer.GetParameter("@staffnotes", DbType.Binary, databaseLayer.Encryption.Encrypt(task.StaffNotes ?? ""));
			array[6] = databaseLayer.GetParameter("@studentnotes", DbType.Binary, databaseLayer.Encryption.Encrypt(task.StudentNotes ?? ""));
			DbParameter[] parameters = array;
			databaseLayer.ExecuteNonQuery("UPDATE studentfiletasks_task SET whoresponsiblecode=@whoresponsiblecode,datelastmodified=getdate(),wholastmodified=@whoami,description=@description,completedid=@completedid,staffnotes=@staffnotes,studentnotes=@studentnotes WHERE taskid=@taskid", parameters);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00029764 File Offset: 0x00027964
		public int CreateActionPlanTask(ActionPlanTask task)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[9];
			array[0] = databaseLayer.GetOutputParameter("@taskid", DbType.Int32, 0);
			array[1] = databaseLayer.GetParameter("@personid", DbType.Int32, task.PersonId);
			array[2] = databaseLayer.GetParameter("@whoresponsiblecode", DbType.Int32, task.WhoResponsibleCode);
			array[3] = databaseLayer.GetParameter("@whoadded", DbType.Int32, this.OpContext.WhoAmI);
			array[4] = databaseLayer.GetParameter("@wholastmodified", DbType.Int32, this.OpContext.WhoAmI);
			array[5] = databaseLayer.GetParameter("@description", DbType.String, task.Description ?? "");
			int num = 6;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@completedid";
			DbType pType = DbType.Int32;
			int? completedId = task.CompletedId;
			array[num] = databaseLayer2.GetParameter(pName, pType, (completedId != null) ? completedId.GetValueOrDefault() : DBNull.Value);
			array[7] = databaseLayer.GetParameter("@staffnotes", DbType.Binary, databaseLayer.Encryption.Encrypt(task.StaffNotes ?? ""));
			array[8] = databaseLayer.GetParameter("@studentnotes", DbType.Binary, databaseLayer.Encryption.Encrypt(task.StudentNotes ?? ""));
			DbParameter[] array2 = array;
			databaseLayer.ExecuteNonQuery("INSERT INTO studentfiletasks_task (personid,whoresponsiblecode,whoadded,wholastmodified,description,completedid,staffnotes,studentnotes) VALUES (@personid,@whoresponsiblecode,@whoadded,@wholastmodified,@description,@completedid,@staffnotes,@studentnotes)\r\nSET @taskid=(SELECT TOP 1 CAST(@@identity AS int) AS taskid FROM studentfiletasks_task)", array2);
			return ((int?)array2[0].Value).GetValueOrDefault();
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000298E4 File Offset: 0x00027AE4
		public void DeleteActionPlanTask(int taskId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@taskid", DbType.Int32, taskId)
			};
			databaseLayer.ExecuteNonQuery("DELETE FROM studentfiletasks_task WHERE taskid=@taskid", parameters);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00029938 File Offset: 0x00027B38
		public IList<ActionPlanTask> LoadTasks(int pid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, pid)
			};
			IList<ActionPlanTask> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT t.taskid,t.whoresponsiblecode,t.datelastmodified,t.whoadded,t.wholastmodified,'' AS who,'' AS Assigned_To,t.[group],t.description,t.completedid,c.title AS completed,c.meanscomplete,t.staffnotes,t.studentnotes,t.ordernum\r\n,p.firstname,p.lastname,t.dateadded,t.personid\r\nFROM studentfiletasks_task t LEFT JOIN studentfiletasks_completed c ON c.completedid=t.completedid\r\nLEFT JOIN people p ON p.personid=t.wholastmodified\r\nWHERE t.personid=@pid\r\nORDER BY t.datelastmodified", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					List<ActionPlanTask> list = new List<ActionPlanTask>();
					while (dataReader.Read())
					{
						list.Add(this.GetTaskFromRecord(dataReader, batchDecryptor));
					}
					result = (from h in list
					where h != null
					select h).ToList<ActionPlanTask>();
				}
			}
			return result;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00029A18 File Offset: 0x00027C18
		private ActionPlanTask GetTaskFromRecord(IDataRecord record, IBatchDecryptor batchDecryptor)
		{
			bool flag = record == null;
			ActionPlanTask result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new ActionPlanTask
				{
					LastName = ((record["lastname"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["lastname"])),
					FirstName = ((record["firstname"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["firstname"])),
					StaffNotes = ((record["staffnotes"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["staffnotes"])),
					StudentNotes = ((record["studentnotes"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["studentnotes"])),
					Group = record["group"].ToString().Trim(),
					Description = record["description"].ToString().Trim(),
					PersonId = ((record["personid"] is DBNull) ? 0 : ((int)record["personid"])),
					CompletedId = ((record["completedid"] is DBNull) ? null : new int?((int)record["completedid"])),
					DateAdded = ((record["dateadded"] is DBNull) ? DateTime.MinValue : ((DateTime)record["dateadded"])),
					DateLastModified = ((record["datelastmodified"] is DBNull) ? null : new DateTime?((DateTime)record["datelastmodified"])),
					MeansComplete = (record["meanscomplete"] != DBNull.Value && Convert.ToBoolean(record["meanscomplete"])),
					OrderNum = ((record["ordernum"] is DBNull) ? 0 : ((int)record["ordernum"])),
					TaskId = ((record["taskid"] is DBNull) ? 0 : ((int)record["taskid"])),
					WhoAdded = ((record["whoadded"] is DBNull) ? 0 : ((int)record["whoadded"])),
					WhoLastModified = ((record["wholastmodified"] is DBNull) ? 0 : ((int)record["wholastmodified"])),
					WhoResponsibleCode = ((record["whoresponsiblecode"] is DBNull) ? 0 : ((int)record["whoresponsiblecode"])),
					Completed = record["completed"].ToString()
				};
			}
			return result;
		}
	}
}
