using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.DynamicForms.Adapters;
using TechnoPro.Common.DAO.StudentFiles;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.Common.DAO.Impl.StudentFiles
{
	// Token: 0x02000040 RID: 64
	public class StudentFilesQueueDAO : IStudentFilesQueueDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000F45D File Offset: 0x0000D65D
		// (set) Token: 0x060001AE RID: 430 RVA: 0x0000F465 File Offset: 0x0000D665
		public OperationContext OpContext { get; set; }

		// Token: 0x060001AF RID: 431 RVA: 0x0000F46E File Offset: 0x0000D66E
		public StudentFilesQueueDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000F480 File Offset: 0x0000D680
		[DebuggerStepThrough]
		public Task<IList<StudentFilesLookupStatus>> GetStudentFileLookupStatusesAsync(int cid)
		{
			StudentFilesQueueDAO.<GetStudentFileLookupStatusesAsync>d__5 <GetStudentFileLookupStatusesAsync>d__ = new StudentFilesQueueDAO.<GetStudentFileLookupStatusesAsync>d__5();
			<GetStudentFileLookupStatusesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentFilesLookupStatus>>.Create();
			<GetStudentFileLookupStatusesAsync>d__.<>4__this = this;
			<GetStudentFileLookupStatusesAsync>d__.cid = cid;
			<GetStudentFileLookupStatusesAsync>d__.<>1__state = -1;
			<GetStudentFileLookupStatusesAsync>d__.<>t__builder.Start<StudentFilesQueueDAO.<GetStudentFileLookupStatusesAsync>d__5>(ref <GetStudentFileLookupStatusesAsync>d__);
			return <GetStudentFileLookupStatusesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000F4CC File Offset: 0x0000D6CC
		public IList<StudentFilesLookupStatus> GetStudentFileLookupStatuses(int cid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@cid", DbType.Int32, cid)
			};
			object obj = databaseLayer.ExecuteScalar("SELECT TOP 1 ll.lookuptext FROM dynamiccontrols dc LEFT JOIN lookuplists ll ON ll.lookupgroupid=dc.setting1 WHERE dc.controlid=@cid ORDER BY ll.ordernum", parameters);
			bool flag = obj == null || !(obj is string);
			IList<StudentFilesLookupStatus> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = (from h in Convert.ToString(obj).Split(new char[]
				{
					'`'
				}).Skip(1).Select(delegate(string g)
				{
					string text = g.Trim();
					StudentFilesLookupStatus result2;
					if (text.Length <= 0)
					{
						result2 = null;
					}
					else
					{
						StudentFilesLookupStatus studentFilesLookupStatus = new StudentFilesLookupStatus();
						studentFilesLookupStatus.Title = text;
						result2 = studentFilesLookupStatus;
						studentFilesLookupStatus.StatusType = this.GetStatusTypeFromStatusString(text);
					}
					return result2;
				})
				where h != null
				select h).ToList<StudentFilesLookupStatus>();
			}
			return result;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000F594 File Offset: 0x0000D794
		private eStudentFileStatusType GetStatusTypeFromStatusString(string s)
		{
			bool flag = string.IsNullOrWhiteSpace(s);
			eStudentFileStatusType result;
			if (flag)
			{
				result = eStudentFileStatusType.Open;
			}
			else
			{
				var <>f__AnonymousType = (from g in (eStudentFileStatusType[])Enum.GetValues(typeof(eStudentFileStatusType))
				select new
				{
					EnumItem = g,
					AttrItem = g.GetAttribute<StudentFileStatusTypeAttribute>()
				}).FirstOrDefault(m => m.AttrItem != null && m.AttrItem.PostFix != null && s.EndsWith(m.AttrItem.PostFix, StringComparison.OrdinalIgnoreCase));
				result = ((<>f__AnonymousType != null) ? <>f__AnonymousType.EnumItem : eStudentFileStatusType.Open);
			}
			return result;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000F61C File Offset: 0x0000D81C
		[DebuggerStepThrough]
		public Task<IList<StudentFilesQueueStudentItem>> LoadStudentFilesQueueStudentItemsAsync(int cid, DateTime startDate, bool loadClosedStudents)
		{
			StudentFilesQueueDAO.<LoadStudentFilesQueueStudentItemsAsync>d__8 <LoadStudentFilesQueueStudentItemsAsync>d__ = new StudentFilesQueueDAO.<LoadStudentFilesQueueStudentItemsAsync>d__8();
			<LoadStudentFilesQueueStudentItemsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentFilesQueueStudentItem>>.Create();
			<LoadStudentFilesQueueStudentItemsAsync>d__.<>4__this = this;
			<LoadStudentFilesQueueStudentItemsAsync>d__.cid = cid;
			<LoadStudentFilesQueueStudentItemsAsync>d__.startDate = startDate;
			<LoadStudentFilesQueueStudentItemsAsync>d__.loadClosedStudents = loadClosedStudents;
			<LoadStudentFilesQueueStudentItemsAsync>d__.<>1__state = -1;
			<LoadStudentFilesQueueStudentItemsAsync>d__.<>t__builder.Start<StudentFilesQueueDAO.<LoadStudentFilesQueueStudentItemsAsync>d__8>(ref <LoadStudentFilesQueueStudentItemsAsync>d__);
			return <LoadStudentFilesQueueStudentItemsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000F678 File Offset: 0x0000D878
		public IList<StudentFilesQueueStudentItem> LoadStudentFilesQueueStudentItems(int cid, DateTime startDate, bool loadClosedStudents)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@cid", DbType.Int32, cid),
				databaseLayer.GetParameter("@startDate", DbType.DateTime, startDate.Date),
				databaseLayer.GetParameter("@loadClosed", DbType.Boolean, loadClosedStudents)
			};
			IList<StudentFilesQueueStudentItem> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT  o.personid,o.dataid,CAST(o.controlvalue AS varchar(max)) AS controlvalue,\r\n        p.firstname,p.middlename,p.lastname,p.student_no,c.email,c.assignedcounsellorfirst,c.assignedcounsellorlast,c.assignedcounsellorpid\r\nFROM    OtherInfoPsFileUpload ofu LEFT JOIN OtherInfoPS o ON o.PersonID=ofu.FK_personid \r\n        LEFT JOIN people p ON p.personid=o.personid\r\n        LEFT JOIN common c ON c.personid=o.personid\r\nWHERE   (@loadClosed=1 OR ofu.IsAtLeastOneFileStatusOpen=1) AND ofu.LastUpdated>=@startDate AND o.ControlID=@cid\r\nORDER BY o.personid,o.dataid DESC", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<StudentFilesQueueStudentItem> list = new List<StudentFilesQueueStudentItem>();
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						int pid = (dataReader["personid"] is DBNull) ? 0 : Convert.ToInt32(dataReader["personid"]);
						bool flag2 = pid < 1 || list.Any((StudentFilesQueueStudentItem g) => g.PersonId == pid);
						if (!flag2)
						{
							IList<StudentFilesQueueFileItem> list2 = this.ExtractFileItemsFromReader(dataReader);
							bool flag3 = list2.Count < 1;
							if (!flag3)
							{
								list.Add(new StudentFilesQueueStudentItem
								{
									FileItems = list2,
									PersonId = pid,
									DataId = ((dataReader["dataid"] is DBNull) ? 0 : Convert.ToInt32(dataReader["dataid"])),
									FirstName = ((dataReader["firstname"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])dataReader["firstname"])),
									MiddleName = ((dataReader["middlename"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])dataReader["middlename"])),
									LastName = ((dataReader["lastname"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])dataReader["lastname"])),
									StudentNumber = ((dataReader["student_no"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])dataReader["student_no"])),
									Email = ((dataReader["email"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])dataReader["email"])),
									AssignedCounsellorFirstName = ((dataReader["assignedcounsellorfirst"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])dataReader["assignedcounsellorfirst"])),
									AssignedCounsellorLastName = ((dataReader["assignedcounsellorlast"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])dataReader["assignedcounsellorlast"])),
									AssignedCounsellorPersonId = ((dataReader["assignedcounsellorpid"] is DBNull) ? 0 : Convert.ToInt32(dataReader["assignedcounsellorpid"]))
								});
							}
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000F9EC File Offset: 0x0000DBEC
		private IList<StudentFilesQueueFileItem> ExtractFileItemsFromReader(IDataReader reader)
		{
			string text = (reader["controlvalue"] is DBNull) ? string.Empty : Convert.ToString(reader["controlvalue"]);
			bool flag = string.IsNullOrWhiteSpace(text);
			IList<StudentFilesQueueFileItem> result;
			if (flag)
			{
				result = new List<StudentFilesQueueFileItem>();
			}
			else
			{
				List<string[]> list = text.DecodeDocumentsList();
				List<StudentFilesQueueFileItem> list2 = new List<StudentFilesQueueFileItem>();
				foreach (string[] array in list)
				{
					bool flag2 = array == null;
					if (!flag2)
					{
						int num = array.Length;
						bool flag3 = num < 1;
						if (!flag3)
						{
							string text2 = this.SafeGetArrayItemByIndex<string>(num, array, 0);
							StudentFilesStatus status = new StudentFilesStatus
							{
								Title = text2,
								StatusType = this.GetStatusTypeFromStatusString(text2)
							};
							string text3 = this.SafeGetArrayItemByIndex<string>(num, array, num - 1);
							int num2 = text3.LastIndexOf(':');
							string text4 = (num2 > 0) ? text3.Substring(num2 + 1) : "";
							int num3;
							int fileId = (!string.IsNullOrWhiteSpace(text4) && int.TryParse(text4, out num3)) ? num3 : 0;
							bool flag4 = num2 > 0;
							if (flag4)
							{
								text3 = text3.Substring(0, num2).Trim();
							}
							list2.Add(new StudentFilesQueueFileItem
							{
								Status = status,
								FileName = text3,
								FileId = fileId,
								DateAddedStr = this.SafeGetArrayItemByIndex<string>(num, array, num - 2),
								StudentComment = this.SafeGetArrayItemByIndex<string>(num, array, 1),
								StaffComment = this.SafeGetArrayItemByIndex<string>(num, array, 2),
								OriginalColumn = array,
								WasModified = false
							});
						}
					}
				}
				result = list2;
			}
			return result;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000FBD0 File Offset: 0x0000DDD0
		private T SafeGetArrayItemByIndex<T>(int itemsLength, T[] items, int ind)
		{
			return (ind >= 0 && itemsLength > ind) ? items[ind] : default(T);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000FBFC File Offset: 0x0000DDFC
		public static string[] ConvertFileItemToRow(StudentFilesQueueFileItem fileItem, int numItems)
		{
			bool flag = !fileItem.WasModified;
			string[] result;
			if (flag)
			{
				result = fileItem.OriginalColumn;
			}
			else
			{
				string[] array = new string[numItems];
				bool flag2 = fileItem.OriginalColumn != null && fileItem.OriginalColumn.Length > array.Length;
				if (flag2)
				{
					for (int i = 3; i < fileItem.OriginalColumn.Length - 2; i++)
					{
						array[i] = fileItem.OriginalColumn[i];
					}
				}
				string[] array2 = array;
				int num = 0;
				StudentFilesStatus status = fileItem.Status;
				array2[num] = (((status != null) ? status.Title : null) ?? "");
				array[1] = (fileItem.StudentComment ?? "");
				array[2] = (fileItem.StaffComment ?? "");
				array[array.Length - 1] = ((fileItem.FileId > 0) ? ((fileItem.FileName ?? "unknown") + ":" + fileItem.FileId.ToString()) : "");
				array[array.Length - 2] = (fileItem.DateAddedStr ?? "");
				result = array;
			}
			return result;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000FD10 File Offset: 0x0000DF10
		[DebuggerStepThrough]
		public Task<IList<StudentFilesQueueFileItem>> UpdateStudentFilesQueueStudentItemAsync(int cid, int pid, IList<StudentFilesQueueFileItem> allUpdatedFileItemsForStudent)
		{
			StudentFilesQueueDAO.<UpdateStudentFilesQueueStudentItemAsync>d__13 <UpdateStudentFilesQueueStudentItemAsync>d__ = new StudentFilesQueueDAO.<UpdateStudentFilesQueueStudentItemAsync>d__13();
			<UpdateStudentFilesQueueStudentItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentFilesQueueFileItem>>.Create();
			<UpdateStudentFilesQueueStudentItemAsync>d__.<>4__this = this;
			<UpdateStudentFilesQueueStudentItemAsync>d__.cid = cid;
			<UpdateStudentFilesQueueStudentItemAsync>d__.pid = pid;
			<UpdateStudentFilesQueueStudentItemAsync>d__.allUpdatedFileItemsForStudent = allUpdatedFileItemsForStudent;
			<UpdateStudentFilesQueueStudentItemAsync>d__.<>1__state = -1;
			<UpdateStudentFilesQueueStudentItemAsync>d__.<>t__builder.Start<StudentFilesQueueDAO.<UpdateStudentFilesQueueStudentItemAsync>d__13>(ref <UpdateStudentFilesQueueStudentItemAsync>d__);
			return <UpdateStudentFilesQueueStudentItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000FD6C File Offset: 0x0000DF6C
		public IList<StudentFilesQueueFileItem> UpdateStudentFilesQueueStudentItem(int cid, int pid, IList<StudentFilesQueueFileItem> allUpdatedFileItemsForStudent)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			object obj = databaseLayer.ExecuteScalar("SELECT COUNT(ll.lookuptext) AS ct FROM dynamiccontrols dc LEFT JOIN lookuplists ll ON ll.lookupgroupid=dc.setting1 WHERE dc.controlid=@cid", new DbParameter[]
			{
				databaseLayer.GetParameter("@cid", DbType.Int32, cid)
			});
			int numItems = (obj == null || !(obj is int)) ? 5 : (Convert.ToInt32(obj) + 2);
			string text;
			if (allUpdatedFileItemsForStudent == null)
			{
				text = null;
			}
			else
			{
				text = allUpdatedFileItemsForStudent.EncodeDocumentsList((StudentFilesQueueFileItem g, int h) => StudentFilesQueueDAO.ConvertFileItemToRow(g, h), numItems);
			}
			string text2 = text;
			string text3 = text2.Replace('\0'.ToString(), " | ").Replace('\t'.ToString(), "\r\n");
			bool flag = string.IsNullOrWhiteSpace(text2);
			if (flag)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@cid", DbType.Int32, cid),
					databaseLayer.GetParameter("@pid", DbType.Int32, pid)
				};
				databaseLayer.ExecuteNonQuery("DELETE FROM otherinfops WHERE controlid=@cid AND personid=@pid", parameters);
			}
			else
			{
				DbParameter[] parameters2 = new DbParameter[]
				{
					databaseLayer.GetParameter("@cid", DbType.Int32, cid),
					databaseLayer.GetParameter("@pid", DbType.Int32, pid),
					databaseLayer.GetParameter("@val", DbType.Binary, Encoding.UTF8.GetBytes(text2))
				};
				databaseLayer.ExecuteNonQuery("IF NOT EXISTS(SELECT dataid FROM otherinfops WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\nINSERT INTO otherinfops (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val)\r\nEND\r\nELSE\r\nBEGIN\r\nUPDATE otherinfops SET controlvalue=@val WHERE personid=@pid AND controlid=@cid\r\nEND", parameters2);
			}
			return this.LoadStudentFilesQueueFileItemsByStudent(cid, pid);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000FEEC File Offset: 0x0000E0EC
		[DebuggerStepThrough]
		public Task<IList<StudentFilesQueueFileItem>> LoadStudentFilesQueueFileItemsByStudentAsync(int cid, int pid)
		{
			StudentFilesQueueDAO.<LoadStudentFilesQueueFileItemsByStudentAsync>d__15 <LoadStudentFilesQueueFileItemsByStudentAsync>d__ = new StudentFilesQueueDAO.<LoadStudentFilesQueueFileItemsByStudentAsync>d__15();
			<LoadStudentFilesQueueFileItemsByStudentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentFilesQueueFileItem>>.Create();
			<LoadStudentFilesQueueFileItemsByStudentAsync>d__.<>4__this = this;
			<LoadStudentFilesQueueFileItemsByStudentAsync>d__.cid = cid;
			<LoadStudentFilesQueueFileItemsByStudentAsync>d__.pid = pid;
			<LoadStudentFilesQueueFileItemsByStudentAsync>d__.<>1__state = -1;
			<LoadStudentFilesQueueFileItemsByStudentAsync>d__.<>t__builder.Start<StudentFilesQueueDAO.<LoadStudentFilesQueueFileItemsByStudentAsync>d__15>(ref <LoadStudentFilesQueueFileItemsByStudentAsync>d__);
			return <LoadStudentFilesQueueFileItemsByStudentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000FF40 File Offset: 0x0000E140
		public IList<StudentFilesQueueFileItem> LoadStudentFilesQueueFileItemsByStudent(int cid, int pid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@cid", DbType.Int32, cid),
				databaseLayer.GetParameter("@pid", DbType.Int32, pid)
			};
			IList<StudentFilesQueueFileItem> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT  TOP 1 o.personid,o.dataid,CAST(o.controlvalue AS varchar(max)) AS controlvalue,\r\n        p.firstname,p.middlename,p.lastname,p.student_no,c.email,c.assignedcounsellorfirst,c.assignedcounsellorlast,c.assignedcounsellorpid\r\nFROM    OtherInfoPS o LEFT JOIN people p ON p.personid=o.personid\r\n        LEFT JOIN common c ON c.personid=o.personid\r\nWHERE   o.ControlID=@cid AND o.personid=@pid\r\nORDER BY o.personid,o.dataid DESC", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.ExtractFileItemsFromReader(dataReader);
				}
			}
			return result;
		}
	}
}
