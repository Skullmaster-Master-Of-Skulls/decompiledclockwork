using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Tasks;

namespace TechnoPro.Common.DAO.Impl.Tasks
{
	// Token: 0x0200003B RID: 59
	public class TaskDAO : ITaskDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000C031 File Offset: 0x0000A231
		// (set) Token: 0x06000179 RID: 377 RVA: 0x0000C039 File Offset: 0x0000A239
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x0600017A RID: 378 RVA: 0x0000C042 File Offset: 0x0000A242
		public TaskDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000C073 File Offset: 0x0000A273
		// (set) Token: 0x0600017C RID: 380 RVA: 0x0000C07B File Offset: 0x0000A27B
		public OperationContext OpContext { get; set; }

		// Token: 0x0600017D RID: 381 RVA: 0x0000C084 File Offset: 0x0000A284
		private List<Task> GetTasksFromReader(IDataReader reader)
		{
			TaskGroupDAO taskGroupDAO = new TaskGroupDAO(this.OpContext);
			List<Task> list = new List<Task>();
			int num = 0;
			Task task = null;
			while (reader.Read())
			{
				int num2 = (int)reader["taskid"];
				bool flag = task == null || num != num2;
				if (flag)
				{
					num = num2;
					bool flag2 = reader["personid"] != DBNull.Value;
					PersonBase owner;
					if (flag2)
					{
						owner = PeopleDAO.GetPersonFromReader("", reader, this.OpContext, null);
					}
					else
					{
						owner = null;
					}
					object obj = reader["priority"];
					int num3 = (obj != DBNull.Value) ? ((int)obj) : 0;
					task = new Task
					{
						TaskId = num2,
						Clients = new List<TaskClient>(),
						DateEntered = (DateTime)reader["dateentered"],
						Description = ((reader["description"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])reader["description"])),
						DueDate = ((reader["duedate"] != DBNull.Value) ? new DateTime?((DateTime)reader["duedate"]) : null),
						IconId = ((reader["iconid"] == DBNull.Value) ? 0 : ((int)reader["iconid"])),
						IsCompleted = (reader["completed"] != DBNull.Value && Convert.ToBoolean(reader["completed"])),
						Notes = new List<TaskNote>(),
						OrderNum = (int)reader["ordernum"],
						OverrideColourArgb = ((reader["overridecolourargb"] != DBNull.Value) ? new int?((int)reader["overridecolourargb"]) : null),
						Owner = owner,
						Priority = (eTaskPriority)(Enum.IsDefined(typeof(eTaskPriority), num3) ? num3 : 0),
						Progress = (int)reader["progress"],
						Reminder = ((reader["reminder"] != DBNull.Value) ? new DateTime?((DateTime)reader["reminder"]) : null),
						Title = reader["title"].ToString(),
						TaskGroup = taskGroupDAO.GetTaskGroupFromRecord(reader),
						PrimaryTaskId = ((reader["primarytaskid"] == DBNull.Value) ? null : new int?((int)reader["primarytaskid"])),
						WhoEntered = PeopleDAO.GetPersonFromReader("whoentered", reader, this.OpContext, null),
						DateLastModified = ((reader["datelastmodified"] == DBNull.Value) ? null : new DateTime?((DateTime)reader["datelastmodified"])),
						WhoLastModified = PeopleDAO.GetPersonFromReader("whomodified", reader, this.OpContext, null),
						IsPrivate = Convert.ToBoolean(reader["isprivate"])
					};
					list.Add(task);
				}
				TaskNote taskNoteFromRecord = this.GetTaskNoteFromRecord(reader);
				bool flag3 = taskNoteFromRecord != null;
				if (flag3)
				{
					task.Notes.Add(taskNoteFromRecord);
				}
				bool flag4 = reader["client_personid"] != DBNull.Value;
				if (flag4)
				{
					TaskClient item = new TaskClient
					{
						Client = PeopleDAO.GetPersonFromReader("client_", reader, this.OpContext, null),
						Notes = ((reader["client_notes"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])reader["client_notes"])),
						TaskClientId = (int)reader["taskclientid"]
					};
					task.Clients.Add(item);
				}
			}
			return list;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000C4F4 File Offset: 0x0000A6F4
		private TaskNote GetTaskNoteFromRecord(IDataReader record)
		{
			bool flag = record["tasknoteid"] != DBNull.Value;
			TaskNote result;
			if (flag)
			{
				result = new TaskNote
				{
					TaskNoteId = (int)record["tasknoteid"],
					DateEntered = (DateTime)record["notes_dateentered"],
					DateLastModified = (DateTime)record["notes_datelastmodified"],
					WhoEntered = PeopleDAO.GetPersonFromReader("notes_whoentered_", record, this.OpContext, null),
					WhoLastModified = PeopleDAO.GetPersonFromReader("notes_whomodified_", record, this.OpContext, null),
					Notes = ((record["notes_notes"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])record["notes_notes"]))
				};
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000C5E4 File Offset: 0x0000A7E4
		public void DeleteTask(int TaskId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@taskid", DbType.Int32, TaskId)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE tasks SET isactive=0 WHERE taskid=@taskid", parameters);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000C628 File Offset: 0x0000A828
		public void ChangeTaskCompletedStatus(int TaskId, bool NewCompletedStatus)
		{
			bool flag = !NewCompletedStatus;
			if (flag)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@taskid", DbType.Int32, TaskId),
					this.DatabaseManager.GetParameter("@iscompleted", DbType.Int32, NewCompletedStatus ? 1 : 0)
				};
				this.DatabaseManager.ExecuteNonQuery("UPDATE tasks SET completed=@iscompleted WHERE taskid=@taskid", parameters);
			}
			else
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@taskid", DbType.Int32, TaskId),
					this.DatabaseManager.GetParameter("@iscompleted", DbType.Int32, NewCompletedStatus ? 1 : 0),
					this.DatabaseManager.GetParameter("@progress", DbType.Int32, 100)
				};
				this.DatabaseManager.ExecuteNonQuery("UPDATE tasks SET completed=@iscompleted,progress=@progress WHERE taskid=@taskid", parameters);
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000C70C File Offset: 0x0000A90C
		public Task LoadTaskById(int TaskId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@taskid", DbType.Int32, TaskId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\tt.taskid,t.dateentered,t.personid,powner.firstName,powner.lastName,powner.student_no,\r\n\t\tt.[description],t.isEncrypted,t.dueDate,t.completed,t.iconID,t.orderNum,\r\n\t\tt.reminder,t.taskGroupID,t.progress,t.priority,t.startDate,t.Title,t.OverrideColourArgb,\r\n\t\ttc.taskclientid,tc.personid AS client_personid,tc.notes AS client_notes,\r\n\t\tp.firstName AS client_firstname,p.lastName AS client_lastname,p.student_no AS client_student_no,\r\n        tn.notes AS notes_notes,\r\n\t\ttn.tasknoteid,tn.whoentered AS notes_whoentered_personid,tn.dateentered AS notes_dateentered,pnoteswe.firstName AS notes_whoentered_firstname,pnoteswe.lastName AS notes_whoentered_lastname,pnoteswe.student_no AS notes_whoentered_student_no,\r\n\t\ttn.wholastmodified AS notes_whomodified_personid,tn.datelastmodified AS notes_datelastmodified,pnoteslm.firstname AS notes_whomodified_firstname,pnoteslm.lastName AS notes_whomodified_lastname,pnoteslm.student_no AS notes_whomodified_student_no,\r\n\t\ttg.ownerpersonid AS groupowner_personid,pgroup.firstName AS groupowner_firstname,pgroup.lastName AS groupowner_lastname,pgroup.student_no AS groupowner_student_no,\r\n        t.primarytaskid,t.whoentered AS whoenteredpersonid,pwe.firstname AS whoenteredfirstname,pwe.lastname AS whoenteredlastname,pwe.student_no AS whoenteredstudent_no,\r\n        t.wholastmodified AS whomodifiedpersonid,pwl.firstname AS whomodifiedfirstname,pwl.lastname AS whomodifiedlastname,pwl.student_no AS whomodifiedstudent_no,\r\n        t.datelastmodified,tg.taskgroupdescription,tg.isactive,t.isprivate,tg.isprivate AS isprivategroup,tg.parenttaskgroupid\r\nFROM\tTasks t LEFT JOIN TaskGroups tg ON tg.TaskGroupID=t.taskGroupID \r\n\t\tLEFT JOIN TaskNotes tn ON tn.TaskId=t.TaskID \r\n\t\tLEFT JOIN TaskClients tc ON tc.TaskId=t.TaskID\r\n\t\tLEFT JOIN people p ON p.PersonID=tc.personid \r\n\t\tLEFT JOIN people powner ON powner.personid=t.personID \r\n\t\tLEFT JOIN people pnoteswe ON pnoteswe.personid=tn.whoentered \r\n\t\tLEFT JOIN people pnoteslm ON pnoteslm.PersonID=tn.wholastmodified \r\n\t\tLEFT JOIN people pgroup ON pgroup.PersonID=tg.ownerpersonid\r\n        LEFT JOIN people pwe ON pwe.personid=t.whoentered\r\n        LEFT JOIN people pwl ON pwl.personid=t.wholastmodified\r\nWHERE   t.taskid=@taskid AND t.isactive=1\r\nORDER BY t.TaskID,t.dateentered", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<Task> tasksFromReader = this.GetTasksFromReader(dataReader);
					bool flag2 = tasksFromReader != null && tasksFromReader.Count > 0;
					if (flag2)
					{
						return tasksFromReader[0];
					}
				}
			}
			return null;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
		public void UpdateTask(Task Task)
		{
			bool flag = Task.Owner == null;
			DbParameter parameter;
			if (flag)
			{
				parameter = this.DatabaseManager.GetParameter("@pid", DbType.Int32, DBNull.Value);
			}
			else
			{
				parameter = this.DatabaseManager.GetParameter("@pid", DbType.Int32, Task.Owner.PersonId);
			}
			bool flag2 = Task.Reminder != null;
			DbParameter parameter2;
			if (flag2)
			{
				parameter2 = this.DatabaseManager.GetParameter("@reminder", DbType.DateTime, Task.Reminder.Value);
			}
			else
			{
				parameter2 = this.DatabaseManager.GetParameter("@reminder", DbType.DateTime, DBNull.Value);
			}
			bool flag3 = Task.OverrideColourArgb != null;
			DbParameter parameter3;
			if (flag3)
			{
				parameter3 = this.DatabaseManager.GetParameter("@overridecolourargb", DbType.Int32, Task.OverrideColourArgb.Value);
			}
			else
			{
				parameter3 = this.DatabaseManager.GetParameter("@overridecolourargb", DbType.Int32, DBNull.Value);
			}
			bool flag4 = Task.PrimaryTaskId != null && Task.PrimaryTaskId.Value.Equals(Task.TaskId);
			if (flag4)
			{
				Task.PrimaryTaskId = null;
			}
			bool flag5 = Task.PrimaryTaskId != null;
			DbParameter parameter4;
			if (flag5)
			{
				parameter4 = this.DatabaseManager.GetParameter("@primarytaskid", DbType.Int32, Task.PrimaryTaskId);
			}
			else
			{
				parameter4 = this.DatabaseManager.GetParameter("@primarytaskid", DbType.Int32, DBNull.Value);
			}
			bool flag6 = Task.DueDate != null;
			DbParameter parameter5;
			if (flag6)
			{
				parameter5 = this.DatabaseManager.GetParameter("@duedate", DbType.DateTime, Task.DueDate.Value);
			}
			else
			{
				parameter5 = this.DatabaseManager.GetParameter("@duedate", DbType.DateTime, DBNull.Value);
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@taskid", DbType.Int32, Task.TaskId),
				parameter,
				this.DatabaseManager.GetParameter("@description", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(Task.Description ?? "")),
				parameter5,
				this.DatabaseManager.GetParameter("@iconid", DbType.Int32, Task.IconId),
				this.DatabaseManager.GetParameter("@completed", DbType.Int32, Task.IsCompleted ? 1 : 0),
				this.DatabaseManager.GetParameter("@ordernum", DbType.Int32, Task.OrderNum),
				parameter3,
				this.DatabaseManager.GetParameter("@priority", DbType.Int32, (int)Task.Priority),
				this.DatabaseManager.GetParameter("@progress", DbType.Int32, Task.Progress),
				parameter2,
				this.DatabaseManager.GetParameter("@taskgroupid", DbType.Int32, (Task.TaskGroup == null) ? 0 : Task.TaskGroup.TaskGroupId),
				this.DatabaseManager.GetParameter("@title", DbType.String, Task.Title),
				this.DatabaseManager.GetParameter("@wholastmodified", DbType.Int32, this.OpContext.WhoAmI),
				parameter4
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE tasks SET description=@description,duedate=@duedate,iconid=@iconid,completed=@completed,\r\nordernum=@ordernum,priority=@priority,progress=@progress,taskgroupid=@taskgroupid,title=@title,\r\nwholastmodified=@wholastmodified,datelastmodified=getdate(),primarytaskid=@primarytaskid\r\nWHERE taskid=@taskid", parameters);
			List<TaskClient> list;
			if (Task.Clients != null)
			{
				list = Task.Clients.FindAll((TaskClient f) => f.Client != null && f.Client.PersonId > 0);
			}
			else
			{
				list = new List<TaskClient>();
			}
			List<TaskClient> list2 = list;
			string text;
			if (list2.Count >= 1)
			{
				text = string.Join(",", list2.ConvertAll<string>((TaskClient f) => f.Client.PersonId.ToString()).ToArray());
			}
			else
			{
				text = "0";
			}
			string value = text;
			parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@taskid", DbType.Int32, Task.TaskId),
				this.DatabaseManager.GetParameter("@ids", DbType.String, value)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM taskclients WHERE taskid=@taskid AND NOT taskclientid IN (SELECT orderid AS taskclientid FROM splitorderids(@ids,','))", parameters);
			bool flag7 = Task.Clients != null;
			if (flag7)
			{
				foreach (TaskClient taskClient in Task.Clients)
				{
					bool flag8 = taskClient.TaskClientId > 0;
					if (flag8)
					{
						parameters = new DbParameter[]
						{
							this.DatabaseManager.GetParameter("@taskclientid", DbType.Int32, taskClient.TaskClientId),
							this.DatabaseManager.GetParameter("@notes", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(taskClient.Notes ?? ""))
						};
						this.DatabaseManager.ExecuteNonQuery("UPDATE taskclients SET notes=@notes WHERE taskclientid=@taskclientid", parameters);
					}
					else
					{
						parameters = new DbParameter[]
						{
							this.DatabaseManager.GetParameter("@taskid", DbType.Int32, Task.TaskId),
							this.DatabaseManager.GetParameter("@notes", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(taskClient.Notes ?? "")),
							this.DatabaseManager.GetParameter("@personid", DbType.Int32, taskClient.Client.PersonId)
						};
						IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("INSERT INTO taskclients (taskid,personid,notes) VALUES (@taskid,@personid,@notes)", parameters);
						bool flag9 = dataReader != null && dataReader.Read();
						if (flag9)
						{
							taskClient.TaskClientId = (int)dataReader[0];
						}
					}
				}
			}
			else
			{
				parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@taskid", DbType.Int32, Task.TaskId)
				};
				this.DatabaseManager.ExecuteNonQuery("DELETE FROM taskclients WHERE taskid=@taskid", parameters);
			}
			List<TaskNote> list3;
			if (Task.Notes != null)
			{
				list3 = Task.Notes.FindAll((TaskNote f) => f.TaskNoteId > 0);
			}
			else
			{
				list3 = new List<TaskNote>();
			}
			List<TaskNote> list4 = list3;
			string text2;
			if (list4.Count >= 1)
			{
				text2 = string.Join(",", list4.ConvertAll<string>((TaskNote f) => f.TaskNoteId.ToString()).ToArray());
			}
			else
			{
				text2 = "0";
			}
			string value2 = text2;
			parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@taskid", DbType.Int32, Task.TaskId),
				this.DatabaseManager.GetParameter("@ids", DbType.String, value2)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM tasknotes WHERE taskid=@taskid AND NOT tasknoteid IN (SELECT orderid AS tasknoteid FROM splitorderids(@ids,','))", parameters);
			bool flag10 = Task.Notes != null;
			if (flag10)
			{
				foreach (TaskNote taskNote in Task.Notes)
				{
					bool flag11 = taskNote.TaskNoteId > 0;
					if (flag11)
					{
						parameters = new DbParameter[]
						{
							this.DatabaseManager.GetParameter("@tasknoteid", DbType.Int32, taskNote.TaskNoteId),
							this.DatabaseManager.GetParameter("@notes", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(taskNote.Notes ?? "")),
							this.DatabaseManager.GetParameter("@wholastmodified", DbType.Int32, this.OpContext.WhoAmI)
						};
						this.DatabaseManager.ExecuteNonQuery("UPDATE tasknotes SET wholastmodified=@wholastmodified,datelastmodified=getdate(),notes=@notes WHERE tasknoteid=@tasknoteid", parameters);
					}
					else
					{
						parameters = new DbParameter[]
						{
							this.DatabaseManager.GetParameter("@taskid", DbType.Int32, Task.TaskId),
							this.DatabaseManager.GetParameter("@notes", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(taskNote.Notes ?? "")),
							this.DatabaseManager.GetParameter("@whoentered", DbType.Int32, this.OpContext.WhoAmI)
						};
						IDataReader dataReader2 = this.DatabaseManager.ExecuteQueryReader("INSERT INTO tasknotes (taskid,whoentered,wholastmodified,notes) \r\nVALUES (@taskid,@whoentered,@whoentered,@notes); SELECT CAST(SCOPE_IDENTITY() AS int) AS tasknoteid", parameters);
						bool flag12 = dataReader2 != null && dataReader2.Read();
						if (flag12)
						{
							taskNote.TaskNoteId = (int)dataReader2[0];
						}
					}
				}
			}
			else
			{
				parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@taskid", DbType.Int32, Task.TaskId)
				};
				this.DatabaseManager.ExecuteNonQuery("DELETE FROM tasknotes WHERE taskid=@taskid AND NOT tasknoteid IN (SELECT orderid AS tasknoteid FROM splitorderids(@ids,','))", parameters);
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000D0F0 File Offset: 0x0000B2F0
		public int CreateTask(Task Task)
		{
			bool flag = Task.Owner == null;
			DbParameter parameter;
			if (flag)
			{
				parameter = this.DatabaseManager.GetParameter("@pid", DbType.Int32, DBNull.Value);
			}
			else
			{
				parameter = this.DatabaseManager.GetParameter("@pid", DbType.Int32, Task.Owner.PersonId);
			}
			bool flag2 = Task.Reminder != null;
			DbParameter parameter2;
			if (flag2)
			{
				parameter2 = this.DatabaseManager.GetParameter("@reminder", DbType.DateTime, Task.Reminder.Value);
			}
			else
			{
				parameter2 = this.DatabaseManager.GetParameter("@reminder", DbType.DateTime, DBNull.Value);
			}
			bool flag3 = Task.OverrideColourArgb != null;
			DbParameter parameter3;
			if (flag3)
			{
				parameter3 = this.DatabaseManager.GetParameter("@overridecolourargb", DbType.Int32, Task.OverrideColourArgb.Value);
			}
			else
			{
				parameter3 = this.DatabaseManager.GetParameter("@overridecolourargb", DbType.Int32, DBNull.Value);
			}
			bool flag4 = Task.PrimaryTaskId != null;
			DbParameter parameter4;
			if (flag4)
			{
				parameter4 = this.DatabaseManager.GetParameter("@primarytaskid", DbType.Int32, Task.PrimaryTaskId);
			}
			else
			{
				parameter4 = this.DatabaseManager.GetParameter("@primarytaskid", DbType.Int32, DBNull.Value);
			}
			bool flag5 = Task.DueDate != null;
			DbParameter parameter5;
			if (flag5)
			{
				parameter5 = this.DatabaseManager.GetParameter("@duedate", DbType.DateTime, Task.DueDate.Value);
			}
			else
			{
				parameter5 = this.DatabaseManager.GetParameter("@duedate", DbType.DateTime, DBNull.Value);
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@whoentered", DbType.Int32, this.OpContext.WhoAmI),
				parameter,
				this.DatabaseManager.GetParameter("@description", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(Task.Description ?? "")),
				parameter5,
				this.DatabaseManager.GetParameter("@iconid", DbType.Int32, Task.IconId),
				this.DatabaseManager.GetParameter("@completed", DbType.Int32, Task.IsCompleted ? 1 : 0),
				this.DatabaseManager.GetParameter("@ordernum", DbType.Int32, Task.OrderNum),
				parameter3,
				this.DatabaseManager.GetParameter("@priority", DbType.Int32, (int)Task.Priority),
				this.DatabaseManager.GetParameter("@progress", DbType.Int32, Task.Progress),
				parameter2,
				this.DatabaseManager.GetParameter("@taskgroupid", DbType.Int32, (Task.TaskGroup == null) ? 0 : Task.TaskGroup.TaskGroupId),
				this.DatabaseManager.GetParameter("@title", DbType.String, Task.Title),
				parameter4
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("INSERT INTO tasks (description,duedate,iconid,completed,ordernum,priority,progress,taskgroupid,title,whoentered,dateentered,primarytaskid) \r\nVALUES (@description,@duedate,@iconid,@completed,@ordernum,@priority,@progress,@taskgroupid,@title,@whoentered,getdate(),@primarytaskid);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS taskid", parameters))
			{
				bool flag6 = dataReader != null && dataReader.Read();
				if (flag6)
				{
					Task.TaskId = (int)dataReader[0];
				}
			}
			bool flag7 = Task.TaskId > 0;
			if (flag7)
			{
				bool flag8 = Task.Clients != null;
				if (flag8)
				{
					foreach (TaskClient taskClient in Task.Clients)
					{
						parameters = new DbParameter[]
						{
							this.DatabaseManager.GetParameter("@taskid", DbType.Int32, Task.TaskId),
							this.DatabaseManager.GetParameter("@notes", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(taskClient.Notes ?? "")),
							this.DatabaseManager.GetParameter("@personid", DbType.Int32, taskClient.Client.PersonId)
						};
						IDataReader dataReader2 = this.DatabaseManager.ExecuteQueryReader("INSERT INTO taskclients (taskid,personid,notes) VALUES (@taskid,@personid,@notes)", parameters);
						bool flag9 = dataReader2 != null && dataReader2.Read();
						if (flag9)
						{
							taskClient.TaskClientId = (int)dataReader2[0];
						}
					}
				}
				bool flag10 = Task.Notes != null;
				if (flag10)
				{
					foreach (TaskNote taskNote in Task.Notes)
					{
						parameters = new DbParameter[]
						{
							this.DatabaseManager.GetParameter("@taskid", DbType.Int32, Task.TaskId),
							this.DatabaseManager.GetParameter("@notes", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(taskNote.Notes ?? "")),
							this.DatabaseManager.GetParameter("@whoentered", DbType.Int32, this.OpContext.WhoAmI)
						};
						IDataReader dataReader3 = this.DatabaseManager.ExecuteQueryReader("INSERT INTO tasknotes (taskid,whoentered,wholastmodified,notes) \r\nVALUES (@taskid,@whoentered,@whoentered,@notes); SELECT CAST(SCOPE_IDENTITY() AS int) AS tasknoteid", parameters);
						bool flag11 = dataReader3 != null && dataReader3.Read();
						if (flag11)
						{
							taskNote.TaskNoteId = (int)dataReader3[0];
						}
					}
				}
			}
			return Task.TaskId;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000D6BC File Offset: 0x0000B8BC
		public List<Task> LoadTasks(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, eTaskPart PartsToLoad)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@whoami", DbType.Int32, this.OpContext.WhoAmI),
				this.DatabaseManager.GetParameter("@includeshared", DbType.Boolean, IncludeSharedTasks),
				this.DatabaseManager.GetParameter("@includeprivate", DbType.Boolean, IncludePrivateTasks),
				this.DatabaseManager.GetParameter("@includeassigned", DbType.Boolean, IncludeAssignedTasks),
				this.DatabaseManager.GetParameter("@loadnotes", DbType.Boolean, (PartsToLoad & eTaskPart.Notes) == eTaskPart.Notes),
				this.DatabaseManager.GetParameter("@loadclients", DbType.Boolean, (PartsToLoad & eTaskPart.Clients) == eTaskPart.Clients)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\tt.taskid,t.dateentered,t.personid,powner.firstName,powner.lastName,powner.student_no,\r\n\t\tt.[description],t.isEncrypted,t.dueDate,t.completed,t.iconID,t.orderNum,\r\n\t\tt.reminder,t.taskGroupID,t.progress,t.priority,t.startDate,t.Title,t.OverrideColourArgb,\r\n\t\ttc.taskclientid,tc.personid AS client_personid,tc.notes AS client_notes,\r\n\t\tp.firstName AS client_firstname,p.lastName AS client_lastname,p.student_no AS client_student_no,\r\n        tn.notes AS notes_notes,\r\n\t\ttn.tasknoteid,tn.whoentered AS notes_whoentered_personid,tn.dateentered AS notes_dateentered,pnoteswe.firstName AS notes_whoentered_firstname,pnoteswe.lastName AS notes_whoentered_lastname,pnoteswe.student_no AS notes_whoentered_student_no,\r\n\t\ttn.wholastmodified AS notes_whomodified_personid,tn.datelastmodified AS notes_datelastmodified,pnoteslm.firstname AS notes_whomodified_firstname,pnoteslm.lastName AS notes_whomodified_lastname,pnoteslm.student_no AS notes_whomodified_student_no,\r\n\t\ttg.ownerpersonid AS groupowner_personid,pgroup.firstName AS groupowner_firstname,pgroup.lastName AS groupowner_lastname,pgroup.student_no AS groupowner_student_no,\r\n        t.primarytaskid,t.whoentered AS whoenteredpersonid,pwe.firstname AS whoenteredfirstname,pwe.lastname AS whoenteredlastname,pwe.student_no AS whoenteredstudent_no,\r\n        t.wholastmodified AS whomodifiedpersonid,pwl.firstname AS whomodifiedfirstname,pwl.lastname AS whomodifiedlastname,pwl.student_no AS whomodifiedstudent_no,\r\n        t.datelastmodified,tg.taskgroupdescription,tg.isactive,t.isprivate,tg.isprivate AS isprivategroup,tg.parenttaskgroupid\r\nFROM\tTasks t LEFT JOIN TaskGroups tg ON tg.TaskGroupID=t.taskGroupID \r\n\t\tLEFT JOIN TaskNotes tn ON @loadnotes=1 AND tn.TaskId=t.TaskID \r\n\t\tLEFT JOIN TaskClients tc ON @loadclients=1 AND tc.TaskId=t.TaskID\r\n\t\tLEFT JOIN people p ON p.PersonID=tc.personid \r\n\t\tLEFT JOIN people powner ON powner.personid=t.personID \r\n\t\tLEFT JOIN people pnoteswe ON pnoteswe.personid=tn.whoentered \r\n\t\tLEFT JOIN people pnoteslm ON pnoteslm.PersonID=tn.wholastmodified \r\n\t\tLEFT JOIN people pgroup ON pgroup.PersonID=tg.ownerpersonid\r\n        LEFT JOIN people pwe ON pwe.personid=t.whoentered\r\n        LEFT JOIN people pwl ON pwl.personid=t.wholastmodified\r\nWHERE\t(\r\n          (@includeprivate=1 AND t.isprivate=1 AND t.personid=@whoami)\r\n          OR (@includeshared=1 AND t.isprivate=0)\r\n          OR (@includeassigned=1 AND t.taskid IN (SELECT taskid FROM taskclients WHERE personid=@whoami))\r\n        )\r\n        AND t.removefromlist=0\r\n        AND t.isactive=1\r\nORDER BY t.TaskID,t.dateentered", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return this.GetTasksFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000D7D0 File Offset: 0x0000B9D0
		public List<Task> LoadCompletedTasks(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, DateTime StartDate, DateTime EndDate)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@whoami", DbType.Int32, this.OpContext.WhoAmI),
				this.DatabaseManager.GetParameter("@includeshared", DbType.Boolean, IncludeSharedTasks),
				this.DatabaseManager.GetParameter("@includeprivate", DbType.Boolean, IncludePrivateTasks),
				this.DatabaseManager.GetParameter("@includeassigned", DbType.Boolean, IncludeAssignedTasks),
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\tt.taskid,t.dateentered,t.personid,powner.firstName,powner.lastName,powner.student_no,\r\n\t\tt.[description],t.isEncrypted,t.dueDate,t.completed,t.iconID,t.orderNum,\r\n\t\tt.reminder,t.taskGroupID,t.progress,t.priority,t.startDate,t.Title,t.OverrideColourArgb,\r\n\t\ttc.taskclientid,tc.personid AS client_personid,tc.notes AS client_notes,\r\n\t\tp.firstName AS client_firstname,p.lastName AS client_lastname,p.student_no AS client_student_no,\r\n        tn.notes AS notes_notes,\r\n\t\ttn.tasknoteid,tn.whoentered AS notes_whoentered_personid,tn.dateentered AS notes_dateentered,pnoteswe.firstName AS notes_whoentered_firstname,pnoteswe.lastName AS notes_whoentered_lastname,pnoteswe.student_no AS notes_whoentered_student_no,\r\n\t\ttn.wholastmodified AS notes_whomodified_personid,tn.datelastmodified AS notes_datelastmodified,pnoteslm.firstname AS notes_whomodified_firstname,pnoteslm.lastName AS notes_whomodified_lastname,pnoteslm.student_no AS notes_whomodified_student_no,\r\n\t\ttg.ownerpersonid AS groupowner_personid,pgroup.firstName AS groupowner_firstname,pgroup.lastName AS groupowner_lastname,pgroup.student_no AS groupowner_student_no,\r\n        t.primarytaskid,t.whoentered AS whoenteredpersonid,pwe.firstname AS whoenteredfirstname,pwe.lastname AS whoenteredlastname,pwe.student_no AS whoenteredstudent_no,\r\n        t.wholastmodified AS whomodifiedpersonid,pwe.firstname AS whomodifiedfirstname,pwe.lastname AS whomodifiedlastname,pwe.student_no AS whomodifiedstudent_no,\r\n        t.datelastmodified,tg.taskgroupdescription,tg.isactive,t.isprivate,tg.isprivate AS isprivategroup,tg.parenttaskgroupid\r\nFROM\tTasks t LEFT JOIN TaskGroups tg ON tg.TaskGroupID=t.taskGroupID \r\n\t\tLEFT JOIN TaskNotes tn ON tn.TaskId=t.TaskID \r\n\t\tLEFT JOIN TaskClients tc ON tc.TaskId=t.TaskID\r\n\t\tLEFT JOIN people p ON p.PersonID=tc.personid \r\n\t\tLEFT JOIN people powner ON powner.personid=t.personID \r\n\t\tLEFT JOIN people pnoteswe ON pnoteswe.personid=tn.whoentered \r\n\t\tLEFT JOIN people pnoteslm ON pnoteslm.PersonID=tn.wholastmodified \r\n\t\tLEFT JOIN people pgroup ON pgroup.PersonID=tg.ownerpersonid \r\n        LEFT JOIN people pwe ON pwe.personid=t.whoentered\r\n        LEFT JOIN people pwl ON pwl.personid=t.wholastmodified\r\nWHERE\t(\r\n          (@includeprivate=1 AND t.isprivate=1 AND t.personid=@whoami)\r\n          OR (@includeshared=1 AND t.isprivate=0)\r\n          OR (@includeassigned=1 AND t.taskid IN (SELECT taskid FROM taskclients WHERE personid=@whoami))\r\n        )\r\n        AND t.completed=1\r\n        AND ((t.dateentered>=@startdate AND t.dateentered<=@enddate)\r\n            OR (NOT t.datelastmodified IS NULL AND t.datelastmodified>=@startdate AND t.datelastmodified<=@enddate)\r\n            OR (NOT t.duedate IS NULL AND t.duedate>=@startdate AND t.duedate<=@enddate)\r\n            )\r\n        AND t.isactive=1\r\nORDER BY t.TaskID,t.dateentered", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return this.GetTasksFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000D8D8 File Offset: 0x0000BAD8
		public void ChangeRemoveFromListStatus(int TaskId, bool NewRemoveFromListStatus)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@taskid", DbType.Int32, TaskId),
				this.DatabaseManager.GetParameter("@removefromlist", DbType.Boolean, NewRemoveFromListStatus)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE tasks SET removefromlist=@removefromlist WHERE taskid=@taskid", parameters);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000D934 File Offset: 0x0000BB34
		public List<TaskNote> LoadTaskNotesByTaskId(int TaskId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@taskid", DbType.Int32, TaskId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT tn.notes AS notes_notes,\r\n\t\ttn.tasknoteid,tn.whoentered AS notes_whoentered_personid,tn.dateentered AS notes_dateentered,pnoteswe.firstName AS notes_whoentered_firstname,pnoteswe.lastName AS notes_whoentered_lastname,pnoteswe.student_no AS notes_whoentered_student_no,\r\n\t\ttn.wholastmodified AS notes_whomodified_personid,tn.datelastmodified AS notes_datelastmodified,pnoteslm.firstname AS notes_whomodified_firstname,pnoteslm.lastName AS notes_whomodified_lastname,pnoteslm.student_no AS notes_whomodified_student_no\r\nFROM    tasknotes tn \r\n        LEFT JOIN people pnoteswe ON pnoteswe.personid=tn.whoentered \r\n\t\tLEFT JOIN people pnoteslm ON pnoteslm.PersonID=tn.wholastmodified \r\nWHERE   tn.taskid=@taskid\r\nORDER BY tn.dateentered DESC", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<TaskNote> list = new List<TaskNote>();
					while (dataReader.Read())
					{
						TaskNote taskNoteFromRecord = this.GetTaskNoteFromRecord(dataReader);
						bool flag2 = taskNoteFromRecord != null;
						if (flag2)
						{
							list.Add(taskNoteFromRecord);
						}
					}
					return list;
				}
			}
			return null;
		}
	}
}
