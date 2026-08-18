using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Tasks;

namespace TechnoPro.Common.DAO.Impl.Tasks
{
	// Token: 0x0200003C RID: 60
	public class TaskGroupDAO : ITaskGroupDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000188 RID: 392 RVA: 0x0000D9E0 File Offset: 0x0000BBE0
		public TaskGroupDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000DA10 File Offset: 0x0000BC10
		// (set) Token: 0x0600018A RID: 394 RVA: 0x0000DA18 File Offset: 0x0000BC18
		public OperationContext OpContext { get; set; }

		// Token: 0x0600018B RID: 395 RVA: 0x0000DA24 File Offset: 0x0000BC24
		public int CreateNewTaskGroup(TaskGroup Group)
		{
			bool flag = Group.ParentTaskGroupId > 0;
			DbParameter parameter;
			if (flag)
			{
				parameter = this.DatabaseManager.GetParameter("@parenttaskgroupid", DbType.Int32, Group.ParentTaskGroupId);
			}
			else
			{
				parameter = this.DatabaseManager.GetParameter("@parenttaskgroupid", DbType.Int32, DBNull.Value);
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, (Group.Owner == null) ? -1 : Group.Owner.PersonId),
				this.DatabaseManager.GetParameter("@ordernum", DbType.Int32, Group.OrderNum),
				this.DatabaseManager.GetParameter("@isactive", DbType.Boolean, Group.IsActive),
				this.DatabaseManager.GetParameter("@description", DbType.String, Group.Description),
				parameter,
				this.DatabaseManager.GetParameter("@isprivate", DbType.Boolean, Group.IsPrivate)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("INSERT INTO taskgroups (ownerpersonid,taskgroupdescription,ordernum,isactive,isprivate,parenttaskgroupid) VALUES (@pid,@description,@ordernum,@isactive,@isprivate,@parenttaskgroupid); SELECT CAST(SCOPE_IDENTITY() AS int) AS taskgroupid", parameters))
			{
				bool flag2 = dataReader != null && dataReader.Read();
				if (flag2)
				{
					bool flag3 = dataReader["taskgroupid"] != DBNull.Value;
					if (flag3)
					{
						return (int)dataReader["taskgroupid"];
					}
				}
			}
			return 0;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
		public void DeleteTaskGroup(int TaskGroupId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@taskgroupid", DbType.Int32, TaskGroupId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM taskgroups WHERE taskgroupid=@taskgroupid", parameters);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000DBEC File Offset: 0x0000BDEC
		public void UpdateTaskGroup(TaskGroup Group)
		{
			bool flag = Group.ParentTaskGroupId == Group.TaskGroupId;
			if (flag)
			{
				Group.ParentTaskGroupId = 0;
			}
			bool flag2 = Group.ParentTaskGroupId > 0;
			DbParameter parameter;
			if (flag2)
			{
				parameter = this.DatabaseManager.GetParameter("@parenttaskgroupid", DbType.Int32, Group.ParentTaskGroupId);
			}
			else
			{
				parameter = this.DatabaseManager.GetParameter("@parenttaskgroupid", DbType.Int32, DBNull.Value);
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, (Group.Owner == null) ? -1 : Group.Owner.PersonId),
				this.DatabaseManager.GetParameter("@ordernum", DbType.Int32, Group.OrderNum),
				this.DatabaseManager.GetParameter("@isactive", DbType.Boolean, Group.IsActive),
				this.DatabaseManager.GetParameter("@description", DbType.String, Group.Description),
				parameter,
				this.DatabaseManager.GetParameter("@isprivate", DbType.Boolean, Group.IsPrivate),
				this.DatabaseManager.GetParameter("@taskgroupid", DbType.Int32, Group.TaskGroupId)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE taskgroups SET parenttaskgroupid=@parenttaskgroupid,ownerpersonid=@pid,taskgroupdescription=@description,ordernum=@ordernum,isactive=@isactive,isprivate=@isprivate WHERE taskgroupid=@taskgroupid", parameters);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000DD40 File Offset: 0x0000BF40
		public List<TaskGroup> LoadGroups(bool IncludePrivate, bool IncludeShared)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@includeprivate", DbType.Boolean, IncludePrivate),
				this.DatabaseManager.GetParameter("@includeshared", DbType.Boolean, IncludeShared),
				this.DatabaseManager.GetParameter("@whoami", DbType.Int32, this.OpContext.WhoAmI)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ttg.taskgroupid,tg.personid,p.firstname,p.lastname,p.student_no,\r\n\t\ttg.taskgroupdescription,tg.ordernum,tg.isactive,tg.isprivate AS isprivategroup,tg.parenttaskgroupid,\r\n        tg.ownerpersonid AS groupowner_personid,pgroup.firstName AS groupowner_firstname,pgroup.lastName AS groupowner_lastname,pgroup.student_no AS groupowner_student_no\r\nFROM\tTaskGroups tg LEFT JOIN people p ON p.PersonID=tg.personid\r\n        LEFT JOIN people pgroup ON pgroup.personid=tg.ownerpersonid\r\nWHERE\ttg.isactive=1\r\n        AND \r\n        (\r\n            (@includeprivate=1 AND tg.isprivate=1 AND tg.ownerpersonid=@whoami)\r\n            OR\r\n            (@includeshared=1 AND tg.isprivate=0)\r\n        )\r\nORDER BY tg.ordernum,tg.taskgroupdescription", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<TaskGroup> list = new List<TaskGroup>();
					while (dataReader.Read())
					{
						TaskGroup taskGroupFromRecord = this.GetTaskGroupFromRecord(dataReader);
						list.Add(taskGroupFromRecord);
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000DE1C File Offset: 0x0000C01C
		public TaskGroup GetTaskGroupFromRecord(IDataReader record)
		{
			bool flag = record["taskgroupid"] == DBNull.Value || record["taskgroupdescription"] == DBNull.Value;
			TaskGroup result;
			if (flag)
			{
				result = null;
			}
			else
			{
				TaskGroup taskGroup = new TaskGroup
				{
					TaskGroupId = (int)record["taskgroupid"],
					Owner = PeopleDAO.GetPersonFromReader("groupowner_", record, this.OpContext, null),
					IsPrivate = Convert.ToBoolean(record["isprivategroup"]),
					ParentTaskGroupId = ((record["parenttaskgroupid"] == DBNull.Value) ? 0 : ((int)record["parenttaskgroupid"])),
					Description = record["taskgroupdescription"].ToString(),
					IsActive = Convert.ToBoolean(record["isactive"]),
					OrderNum = (int)record["ordernum"]
				};
				result = taskGroup;
			}
			return result;
		}

		// Token: 0x040000AD RID: 173
		private DatabaseLayer DatabaseManager;
	}
}
