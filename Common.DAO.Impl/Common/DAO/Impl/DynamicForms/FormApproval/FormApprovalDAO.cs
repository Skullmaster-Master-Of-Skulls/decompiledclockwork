using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.DynamicForms.FormApproval;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;

namespace TechnoPro.Common.DAO.Impl.DynamicForms.FormApproval
{
	// Token: 0x020000ED RID: 237
	public class FormApprovalDAO : IFormApprovalDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public FormApprovalDAO()
		{
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00047424 File Offset: 0x00045624
		public FormApprovalDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00047436 File Offset: 0x00045636
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x0004743E File Offset: 0x0004563E
		public OperationContext OpContext { get; set; }

		// Token: 0x060006CA RID: 1738 RVA: 0x00047448 File Offset: 0x00045648
		private FormApprovalForAppointment GetFormApprovalForAppointmentFromRecords(IDataReader reader, IBatchDecryptor batchDecryptor)
		{
			bool flag = reader == null || !reader.Read();
			FormApprovalForAppointment result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (reader["CurrentStateId"] is DBNull) ? 0 : ((int)reader["CurrentStateId"]);
				FormApprovalForAppointment formApprovalForAppointment = new FormApprovalForAppointment
				{
					FormApprovalId = ((reader["FormApprovalId"] is DBNull) ? Guid.Empty : ((Guid)reader["FormApprovalId"])),
					ScreenNum = ((reader["screennum"] is DBNull) ? 0 : ((int)reader["screennum"])),
					StudentPersonId = ((reader["personid"] is DBNull) ? 0 : ((int)reader["personid"])),
					AppointmentId = ((reader["appointmentid"] is DBNull) ? 0 : ((int)reader["appointmentid"])),
					CurrentState = (eFormApprovalState)(Enum.IsDefined(typeof(eFormApprovalState), num) ? num : 0),
					DateCreated = ((reader["datecreated"] is DBNull) ? DateTime.MinValue : ((DateTime)reader["datecreated"])),
					Comments = new List<FormApprovalComment>()
				};
				FormApprovalComment formApprovalCommentFromRecord = this.GetFormApprovalCommentFromRecord(reader, batchDecryptor);
				bool flag2 = formApprovalCommentFromRecord != null;
				if (flag2)
				{
					formApprovalForAppointment.Comments.Add(formApprovalCommentFromRecord);
				}
				while (reader.Read())
				{
					FormApprovalComment formApprovalCommentFromRecord2 = this.GetFormApprovalCommentFromRecord(reader, batchDecryptor);
					bool flag3 = formApprovalCommentFromRecord2 != null;
					if (flag3)
					{
						formApprovalForAppointment.Comments.Add(formApprovalCommentFromRecord2);
					}
				}
				result = formApprovalForAppointment;
			}
			return result;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00047614 File Offset: 0x00045814
		private FormApprovalComment GetFormApprovalCommentFromRecord(IDataRecord record, IBatchDecryptor batchDecryptor)
		{
			bool flag = record["FormApprovalCommentId"] is DBNull;
			FormApprovalComment result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new FormApprovalComment
				{
					FormApprovalCommentId = (Guid)record["FormApprovalCommentId"],
					DateEntered = ((record["commentdatecreated"] is DBNull) ? DateTime.MinValue : ((DateTime)record["commentdatecreated"])),
					WhoEntered = PeopleDAO.GetBasicPersonFromRecord("comment", record, batchDecryptor),
					Comment = new FormApprovalCommentText
					{
						CommentText = ((record["CommentText"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["CommentText"]))
					}
				};
			}
			return result;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000476EC File Offset: 0x000458EC
		public FormApprovalForAppointment LoadFormApproval(int screenNum, int studentPersonId, int appId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@screennum", DbType.Int32, screenNum),
				databaseLayer.GetParameter("@pid", DbType.Int32, studentPersonId),
				databaseLayer.GetParameter("@appid", DbType.Int32, appId)
			};
			IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
			FormApprovalForAppointment formApprovalForAppointmentFromRecords;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT fa.FormApprovalId,fa.screennum,fa.personid,fa.appointmentid,\r\nfa.datecreated,fa.whouploaded AS createdpersonid,p3.firstname AS createdfirstname,p3.middlename AS createdmiddlename,p3.lastname AS createdlastname,p3.student_no AS createdstudent_no,\r\nfa.CurrentStateId,fa.ApprovedFormApprovalSignatureId,fa.SubmittedFormApprovalSignatureId,\r\np.firstname,p.middlename,p.lastname,p.student_no,p.isactive,\r\nfac.FormApprovalCommentId,fac.datecreated AS commentdatecreated,fac.commenttext,\r\nfac.whocreated AS commentpersonid,p2.firstname AS commentfirstname,p2.middlename AS commentmiddlename,\r\np2.lastname AS commentlastname,p2.student_no AS commentstudent_no\r\nFROM    FormApproval fa LEFT JOIN people p ON p.personid=fa.personid\r\nLEFT JOIN FormApprovalComment fac ON fac.FormApprovalId=fa.FormApprovalId\r\nLEFT JOIN people p2 ON p2.personid=fac.whocreated\r\nLEFT JOIN people p3 ON p3.personid=fa.whouploaded\r\nWHERE fa.screennum=@screennum AND fa.personid=@pid AND fa.appointmentid=@appid\r\nORDER BY fac.datecreated DESC", parameters))
			{
				formApprovalForAppointmentFromRecords = this.GetFormApprovalForAppointmentFromRecords(dataReader, batchDecryptor);
			}
			return formApprovalForAppointmentFromRecords;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x000477A0 File Offset: 0x000459A0
		public int GetScreenNumForFormApproval(Guid formApprovalId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@formApprovalId", DbType.Guid, formApprovalId)
			};
			object obj = databaseLayer.ExecuteScalar("SELECT screennum FROM FormApproval WHERE FormApprovalId=@formApprovalId", parameters);
			bool flag = obj == null || obj is DBNull;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = (int)obj;
			}
			return result;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00047818 File Offset: 0x00045A18
		public IList<FormApprovalPendingItem> LoadPendingFormApprovalItemsForUser(int pid, int[] screenNums)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[2];
			array[0] = databaseLayer.GetParameter("@pid", DbType.Int32, pid);
			array[1] = databaseLayer.GetParameter("@screennums", DbType.String, string.Join(",", (from g in screenNums
			select g.ToString()).ToArray<string>()));
			DbParameter[] parameters = array;
			IList<FormApprovalPendingItem> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @approvedStatusCode int = 4\r\nSELECT orderid AS screennum INTO #tscreennums FROM splitorderids(@screennums,',')\r\n\r\nSELECT\tfa.FormApprovalId,fa.screennum,s.[description] AS screentitle,\r\n\t\tfa.personid,p.firstname,p.middlename,p.lastname,p.student_no,\r\n\t\tfa.appointmentid,app.startdate,app.ishidden,app.islocked,app.personid AS whobookedpersonid,\r\n\t\tfa.datecreated,\r\n\t\tfa.CurrentStateId,\r\n\t\tMAX(fac.DateCreated) AS LastModifiedDate\r\nFROM\tFormApproval fa LEFT JOIN people p ON p.personid=fa.personid\r\n\t\tLEFT JOIN appointments app ON app.appointmentid=fa.appointmentid\r\n\t\tLEFT JOIN screens s ON s.screennum=fa.screennum\r\n\t\tLEFT JOIN FormApprovalComment fac ON fac.FormApprovalId=fa.FormApprovalId\r\nWHERE\tfa.screennum IN (SELECT screennum FROM #tscreennums) AND NOT fa.CurrentStateId=@approvedStatusCode\r\nGROUP BY fa.FormApprovalId,fa.screennum,s.[description],\r\n\t\tfa.personid,p.firstname,p.middlename,p.lastname,p.student_no,\r\n\t\tfa.appointmentid,app.startdate,app.ishidden,app.islocked,app.personid,\r\n\t\tfa.datecreated,\r\n\t\tfa.CurrentStateId\r\nORDER BY LastModifiedDate,s.[description]\r\n\r\nDROP TABLE #tscreennums", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<FormApprovalPendingItem> list = new List<FormApprovalPendingItem>();
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						FormApprovalPendingItem pendingItemFromRecord = this.GetPendingItemFromRecord(dataReader, batchDecryptor);
						bool flag2 = pendingItemFromRecord != null;
						if (flag2)
						{
							list.Add(pendingItemFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00047924 File Offset: 0x00045B24
		public eFormApprovalState LoadFormApprovalStatus(int studentPersonId, int appId, int screenNum)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, studentPersonId),
				databaseLayer.GetParameter("@appid", DbType.Int32, appId),
				databaseLayer.GetParameter("@screennum", DbType.Int32, screenNum)
			};
			object obj = databaseLayer.ExecuteScalar("SELECT fa.CurrentStateId FROM FormApproval fa WHERE fa.personid=@pid AND fa.appointmentid=@appid AND fa.screennum=@screennum", parameters);
			int num = (obj == null || obj is DBNull) ? 0 : ((int)obj);
			return (eFormApprovalState)(Enum.IsDefined(typeof(eFormApprovalState), num) ? num : 0);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x000479D8 File Offset: 0x00045BD8
		private FormApprovalPendingItem GetPendingItemFromRecord(IDataRecord record, IBatchDecryptor batchDecryptor)
		{
			Guid guid = (record["FormApprovalId"] is DBNull) ? Guid.Empty : ((Guid)record["FormApprovalId"]);
			bool flag = guid == Guid.Empty;
			FormApprovalPendingItem result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int intFromRecord = record.GetIntFromRecord("CurrentStateId", 0);
				result = new FormApprovalPendingItem
				{
					FormApprovalId = guid,
					AppointmentId = record.GetIntFromRecord("appointmentid", 0),
					AppointmentDate = record.GetDateTimeFromRecord("startdate"),
					Student = PeopleDAO.GetBasicPersonFromRecord("", record, batchDecryptor),
					ScreenNum = record.GetIntFromRecord("screennum", 0),
					ScreenTitle = record["screentitle"].ToString().Trim(),
					DateCreated = record.GetDateTimeFromRecord("datecreated", DateTime.MinValue),
					CurrentState = (eFormApprovalState)(Enum.IsDefined(typeof(eFormApprovalState), intFromRecord) ? intFromRecord : 0),
					LastModifiedDate = record.GetDateTimeFromRecord("LastModifiedDate"),
					AppointmentIsPrivate = record.GetBoolFromRecord("ishidden", false),
					AppointmentIsLocked = record.GetBoolFromRecord("islocked", false),
					AppointmentBookedByPersonId = record.GetIntFromRecord("whobookedpersonid", 0)
				};
			}
			return result;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00047B40 File Offset: 0x00045D40
		public void AddFormApprovalComment(Guid formApprovalId, string commentText)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@formApprovalId", DbType.Guid, formApprovalId),
				databaseLayer.GetParameter("@whoamipid", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@commentText", DbType.Binary, databaseLayer.Encryption.Encrypt(commentText ?? ""))
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO FormApprovalComment (FormApprovalId,WhoCreated,CommentText) VALUES (@formApprovalId,@whoamipid,@commentText)", parameters);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00047BD8 File Offset: 0x00045DD8
		public void UpdateFormApprovalCurrentStatus(Guid formApprovalId, eFormApprovalState newStatus)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@formApprovalId", DbType.Guid, formApprovalId),
				databaseLayer.GetParameter("@whoamipid", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@newStatus", DbType.Int32, (int)newStatus)
			};
			databaseLayer.ExecuteNonQuery("UPDATE FormApproval SET CurrentStateId=@newStatus WHERE FormApprovalId=@formApprovalId", parameters);
		}
	}
}
