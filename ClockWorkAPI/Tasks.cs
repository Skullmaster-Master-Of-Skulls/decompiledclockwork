using System;
using System.Collections;
using System.Data;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x02000051 RID: 81
	public class Tasks
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00014E80 File Offset: 0x00013E80
		public DataTable TaskCategoriesTable
		{
			get
			{
				if (this.taskCategoriesTable == null)
				{
					this.LoadCategories();
				}
				return this.taskCategoriesTable;
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00014EAF File Offset: 0x00013EAF
		public Tasks(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, PersonBaseDTO whoAmI)
		{
			this.myTasksTable = null;
			this.da = da;
			this.tripleDES = tripleDES;
			this.whoAmI = whoAmI;
			this.LoadCategories();
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00014EE0 File Offset: 0x00013EE0
		public DataTable MyTasksTable
		{
			get
			{
				if (this.myTasksTable == null)
				{
					this.RefreshTasks();
				}
				return this.myTasksTable;
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00014F0F File Offset: 0x00013F0F
		public void RefreshTasks()
		{
			this.myTasksTable = Tasks.LoadTasks(this.whoAmI.PersonId, Tasks.TaskType.CompletedOrNotCompleted, this.da);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00014F30 File Offset: 0x00013F30
		public void DeleteTaskCategory(int taskgroupid)
		{
			this.da.SelectCommand.CommandText = "UDPATE tasks SET taskgroupid=1 WHERE taskgroupid=@taskgroupid";
			this.da.SelectCommand.Parameters.Add("@taskgroupid", taskgroupid);
			this.da.Fill(new DataTable());
			this.da.SelectCommand.CommandText = "DELETE FROM taskgroups WHERE taskgroupid=@taskgroupid";
			this.da.SelectCommand.Parameters.Add("@taskgroupid", taskgroupid);
			this.da.Fill(new DataTable());
			this.LoadCategories();
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00014FD8 File Offset: 0x00013FD8
		public void EditTaskCategory(int taskgroupid, string name, int personid, int ordernum)
		{
			if (name != null && name.Trim().Length > 0)
			{
				this.da.SelectCommand.CommandText = "UPDATE taskgroups SET personid=@personid,taskgroupdescription=@taskgroupdescription,ordernum=@ordernum WHERE taskgroupid=@taskgroupid";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@personid", personid);
				this.da.SelectCommand.Parameters.Add("@taskgroupdescription", name);
				this.da.SelectCommand.Parameters.Add("@ordernum", ordernum);
				this.da.SelectCommand.Parameters.Add("@taskgroupid", taskgroupid);
				this.da.Fill(new DataTable());
				this.LoadCategories();
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000150CC File Offset: 0x000140CC
		public void AddTaskCategory(string name, int personid, int ordernum)
		{
			if (name != null && name.Trim().Length > 0)
			{
				this.da.SelectCommand.CommandText = "INSERT INTO taskgroups (personid,taskgroupdescription,ordernum) VALUES (@personid,@taskgroupdescription,@ordernum)";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@personid", personid);
				this.da.SelectCommand.Parameters.Add("@taskgroupdescription", name);
				this.da.SelectCommand.Parameters.Add("@ordernum", ordernum);
				this.da.Fill(new DataTable());
				this.LoadCategories();
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0001519C File Offset: 0x0001419C
		public void ReloadTasks()
		{
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000151A0 File Offset: 0x000141A0
		private void LoadCategories()
		{
			this.da.SelectCommand.CommandText = "SELECT taskgroupid,personid,taskgroupdescription,ordernum FROM taskgroups WHERE personid=-1 OR personid=@personid ORDER BY ordernum,taskgroupdescription";
			this.da.SelectCommand.Parameters.Clear();
			this.da.SelectCommand.Parameters.Add("@personid", this.whoAmI.PersonId);
			this.taskCategoriesTable = new DataTable();
			this.da.Fill(this.taskCategoriesTable);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00015224 File Offset: 0x00014224
		public ArrayList ModifyTasks(ArrayList taskRows)
		{
			for (int i = 0; i < taskRows.Count; i++)
			{
				DataRow taskRow = (DataRow)taskRows[i];
				this.ModifyTask(taskRow);
			}
			return taskRows;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00015264 File Offset: 0x00014264
		public int NewTask(DataRow TaskRow)
		{
			DataRow dataRow = this.myTasksTable.LoadDataRow(TaskRow.ItemArray, false);
			this.da.SelectCommand.CommandText = "INSERT INTO tasks (personid,description,isencrypted,duedate,completed,iconid,reminder,taskgroupid,progress,priority,startdate) VALUES (@personid,@description,@isencrypted,@duedate,@completed,@iconid,@reminder,@taskgroupid,@progress,@priority,@startdate)";
			this.SetTaskParameters(ref dataRow, ref this.da);
			this.da.SelectCommand.Parameters.Add("@personid", this.whoAmI.PersonId);
			DataTable dataTable = new DataTable();
			int num = this.da.FillReturnIdentity(dataTable, "taskid", "tasks");
			int result;
			if (dataTable.Rows.Count < 1)
			{
				result = -1;
			}
			else
			{
				dataRow[0] = (int)dataTable.Rows[0][0];
				dataRow.AcceptChanges();
				result = 1;
			}
			return result;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00015340 File Offset: 0x00014340
		private void SetTaskParameters(ref DataRow taskRow, ref UnivDataAdapter da)
		{
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@description", taskRow[2]);
			da.SelectCommand.Parameters.Add("@isencrypted", taskRow[3]);
			da.SelectCommand.Parameters.Add("@duedate", taskRow[4]);
			da.SelectCommand.Parameters.Add("@completed", taskRow[5]);
			da.SelectCommand.Parameters.Add("@iconid", taskRow[6]);
			da.SelectCommand.Parameters.Add("@ordernum", taskRow[7]);
			da.SelectCommand.Parameters.Add("@reminder", taskRow[8]);
			da.SelectCommand.Parameters.Add("@progress", taskRow[11]);
			da.SelectCommand.Parameters.Add("@priority", taskRow[12]);
			da.SelectCommand.Parameters.Add("@startdate", taskRow[13]);
			da.SelectCommand.Parameters.Add("@taskgroupid", taskRow[9]);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000154BC File Offset: 0x000144BC
		public ArrayList ModifyTask(DataRow taskRow)
		{
			ArrayList arrayList = new ArrayList(1);
			arrayList.Add(taskRow);
			ArrayList result;
			if (taskRow.RowState == DataRowState.Unchanged)
			{
				result = arrayList;
			}
			else
			{
				this.da.SelectCommand.CommandText = "UPDATE tasks SET description=@description,isencrypted=@isencrypted,duedate=@duedate,completed=@completed,iconid=@iconid,ordernum=@ordernum,reminder=@reminder,taskgroupid=@taskgroupid,progress=@progress,priority=@priority,startdate=@startdate WHERE taskid=@taskid";
				this.da.SelectCommand.Parameters.Clear();
				this.SetTaskParameters(ref taskRow, ref this.da);
				this.da.SelectCommand.Parameters.Add("@taskid", taskRow[0]);
				string text;
				this.da.Fill(new DataTable(), out text);
				if (text != null && text.Length > 0)
				{
					result = null;
				}
				else
				{
					taskRow.AcceptChanges();
					result = arrayList;
				}
			}
			return result;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00015588 File Offset: 0x00014588
		public int DeleteTask(DataRow taskRow)
		{
			int result;
			if (taskRow.RowState != DataRowState.Deleted)
			{
				result = 1;
			}
			else
			{
				taskRow.RejectChanges();
				int num = (int)taskRow[0];
				this.da.SelectCommand.CommandText = "DELETE FROM tasks WHERE taskid=@taskid";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@taskid", num);
				string text;
				this.da.Fill(new DataTable(), out text);
				if (text != null && text.Length > 0)
				{
					result = -1;
				}
				else
				{
					taskRow.Table.Rows.Remove(taskRow);
					result = 1;
				}
			}
			return result;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00015650 File Offset: 0x00014650
		public int DeleteTasks(ArrayList taskRows)
		{
			foreach (object obj in taskRows)
			{
				DataRow taskRow = (DataRow)obj;
				this.DeleteTask(taskRow);
			}
			return 1;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000156BC File Offset: 0x000146BC
		public static DataTable LoadTasks(int personid, Tasks.TaskType typesOfTasksToLoad, UnivDataAdapter da)
		{
			string str = "SELECT t.taskid,t.personid,t.description,t.isencrypted,t.duedate,t.completed,t.iconid,t.ordernum,t.reminder,t.taskgroupid,tg.taskgroupdescription,t.progress,t.priority,t.startdate FROM tasks t LEFT JOIN taskgroups tg ON tg.taskgroupid=t.taskgroupid WHERE (t.personid=-1 OR t.personid=@personid)";
			if (typesOfTasksToLoad == Tasks.TaskType.Completed)
			{
				str += " AND t.completed=@true";
			}
			else if (typesOfTasksToLoad == Tasks.TaskType.NotCompleted)
			{
				str += " AND t.completed=@false";
			}
			da.SelectCommand.CommandText = str + " ORDER BY t.duedate,t.ordernum,t.description";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@personid", personid);
			da.SelectCommand.Parameters.Add("@true", true);
			da.SelectCommand.Parameters.Add("@false", false);
			DataTable dataTable = new DataTable("tasks");
			string text;
			da.Fill(dataTable, out text);
			if (text != null && text.Length > 0)
			{
				Console.WriteLine(text);
			}
			return dataTable;
		}

		// Token: 0x040001AB RID: 427
		private PersonBaseDTO whoAmI;

		// Token: 0x040001AC RID: 428
		private UnivDataAdapter da;

		// Token: 0x040001AD RID: 429
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x040001AE RID: 430
		private DataTable taskCategoriesTable;

		// Token: 0x040001AF RID: 431
		private DataTable myTasksTable;

		// Token: 0x02000052 RID: 82
		public enum TaskType
		{
			// Token: 0x040001B1 RID: 433
			NotCompleted = 1,
			// Token: 0x040001B2 RID: 434
			Completed,
			// Token: 0x040001B3 RID: 435
			CompletedOrNotCompleted
		}
	}
}
