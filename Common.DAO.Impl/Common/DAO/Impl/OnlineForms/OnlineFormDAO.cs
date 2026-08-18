using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.OnlineForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.OnlineForms;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.OnlineForms
{
	// Token: 0x0200007F RID: 127
	public class OnlineFormDAO : IOnlineFormDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0001AEA4 File Offset: 0x000190A4
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0001AEAC File Offset: 0x000190AC
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x06000324 RID: 804 RVA: 0x0001AEB5 File Offset: 0x000190B5
		public OnlineFormDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0001AEE6 File Offset: 0x000190E6
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0001AEEE File Offset: 0x000190EE
		public OperationContext OpContext { get; set; }

		// Token: 0x06000327 RID: 807 RVA: 0x0001AEF8 File Offset: 0x000190F8
		public int CreateNewOnlineForm(OnlineForm OnlineForm)
		{
			OnlineForm.WhoCreated = new BasicPerson
			{
				PersonId = this.OpContext.WhoAmI
			};
			OnlineForm.DateCreated = DateTime.Now;
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@whocreated", DbType.Int32, this.OpContext.WhoAmI),
				this.DatabaseManager.GetParameter("@title", DbType.String, OnlineForm.Title ?? ""),
				this.DatabaseManager.GetParameter("@description", DbType.String, OnlineForm.Description ?? ""),
				this.DatabaseManager.GetParameter("@formnum", DbType.Int32, (OnlineForm.Form == null) ? 0 : OnlineForm.Form.ScreenNum),
				this.DatabaseManager.GetParameter("@usewizard", DbType.Boolean, OnlineForm.UseWizard),
				this.DatabaseManager.GetParameter("@submitmessage", DbType.String, OnlineForm.SubmitMessage ?? ""),
				this.DatabaseManager.GetParameter("@submitbuttontext", DbType.String, OnlineForm.SubmitButtonText ?? ""),
				this.DatabaseManager.GetParameter("@shortcode", DbType.String, OnlineForm.ShortCode ?? ""),
				(OnlineForm.StartDate != null) ? this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, OnlineForm.StartDate.Value) : this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, DBNull.Value),
				(OnlineForm.EndDate != null) ? this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, OnlineForm.EndDate.Value) : this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, DBNull.Value),
				this.DatabaseManager.GetParameter("@requireslogin", DbType.Boolean, OnlineForm.RequiresLogin),
				this.DatabaseManager.GetParameter("@canonlybefilledinonce", DbType.Boolean, OnlineForm.CanOnlyBeFilledInOnce),
				this.DatabaseManager.GetParameter("@captcha", DbType.Int32, OnlineForm.Captcha),
				this.DatabaseManager.GetParameter("@studentemailconfirmationtemplateid", DbType.Int32, (OnlineForm.StudentEmailConfirmationTemplateId > 0) ? OnlineForm.StudentEmailConfirmationTemplateId : DBNull.Value),
				this.DatabaseManager.GetParameter("@staffemailconfirmationtemplateid", DbType.Int32, (OnlineForm.StaffEmailConfirmationTemplateId > 0) ? OnlineForm.StaffEmailConfirmationTemplateId : DBNull.Value),
				(OnlineForm.RestrictedToGroup != null && OnlineForm.RestrictedToGroup.GroupId > 0) ? this.DatabaseManager.GetParameter("@restricttogroupid", DbType.Int32, OnlineForm.RestrictedToGroup.GroupId) : this.DatabaseManager.GetParameter("@restricttogroupid", DbType.Int32, DBNull.Value),
				this.DatabaseManager.GetParameter("@isdeleted", DbType.Boolean, OnlineForm.IsDeleted),
				this.DatabaseManager.GetParameter("@isdisabled", DbType.Boolean, OnlineForm.IsDisabled)
			};
			object obj = this.DatabaseManager.ExecuteScalar("INSERT INTO OnlineForm (title,description,formnum,usewizard,submitmessage,submitbuttontext\r\n        ,shortcode,startdate,enddate,requireslogin,canonlybefilledinonce,captcha,studentemailconfirmationtemplateid,staffemailconfirmationtemplateid\r\n        ,restricttogroupid,isdeleted,isdisabled,whocreated)\r\nVALUES (@title,@description,@formnum,@usewizard,@submitmessage,@submitbuttontext\r\n        ,@shortcode,@startdate,@enddate,@requireslogin,@canonlybefilledinonce,@captcha,@studentemailconfirmationtemplateid,@staffemailconfirmationtemplateid\r\n        ,@restricttogroupid,@isdeleted,@isdisabled,@whocreated);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS onlineformid", parameters);
			bool flag = obj != null && obj != DBNull.Value && obj is int;
			if (flag)
			{
				OnlineForm.OnlineFormId = (int)obj;
			}
			return OnlineForm.OnlineFormId;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0001B2A0 File Offset: 0x000194A0
		public void UpdateOnlineForm(OnlineForm OnlineForm)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@onlineformid", DbType.Int32, OnlineForm.OnlineFormId),
				this.DatabaseManager.GetParameter("@title", DbType.String, OnlineForm.Title ?? ""),
				this.DatabaseManager.GetParameter("@description", DbType.String, OnlineForm.Description ?? ""),
				this.DatabaseManager.GetParameter("@formnum", DbType.Int32, (OnlineForm.Form == null) ? 0 : OnlineForm.Form.ScreenNum),
				this.DatabaseManager.GetParameter("@usewizard", DbType.Boolean, OnlineForm.UseWizard),
				this.DatabaseManager.GetParameter("@submitmessage", DbType.String, OnlineForm.SubmitMessage ?? ""),
				this.DatabaseManager.GetParameter("@submitbuttontext", DbType.String, OnlineForm.SubmitButtonText ?? ""),
				this.DatabaseManager.GetParameter("@shortcode", DbType.String, OnlineForm.ShortCode ?? ""),
				(OnlineForm.StartDate != null) ? this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, OnlineForm.StartDate.Value) : this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, DBNull.Value),
				(OnlineForm.EndDate != null) ? this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, OnlineForm.EndDate.Value) : this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, DBNull.Value),
				this.DatabaseManager.GetParameter("@requireslogin", DbType.Boolean, OnlineForm.RequiresLogin),
				this.DatabaseManager.GetParameter("@canonlybefilledinonce", DbType.Boolean, OnlineForm.CanOnlyBeFilledInOnce),
				this.DatabaseManager.GetParameter("@captcha", DbType.Int32, OnlineForm.Captcha),
				this.DatabaseManager.GetParameter("@studentemailconfirmationtemplateid", DbType.Int32, (OnlineForm.StudentEmailConfirmationTemplateId > 0) ? OnlineForm.StudentEmailConfirmationTemplateId : DBNull.Value),
				this.DatabaseManager.GetParameter("@staffemailconfirmationtemplateid", DbType.Int32, (OnlineForm.StaffEmailConfirmationTemplateId > 0) ? OnlineForm.StaffEmailConfirmationTemplateId : DBNull.Value),
				(OnlineForm.RestrictedToGroup != null && OnlineForm.RestrictedToGroup.GroupId > 0) ? this.DatabaseManager.GetParameter("@restricttogroupid", DbType.Int32, OnlineForm.RestrictedToGroup.GroupId) : this.DatabaseManager.GetParameter("@restricttogroupid", DbType.Int32, DBNull.Value),
				this.DatabaseManager.GetParameter("@isdeleted", DbType.Boolean, OnlineForm.IsDeleted),
				this.DatabaseManager.GetParameter("@isdisabled", DbType.Boolean, OnlineForm.IsDisabled),
				this.DatabaseManager.GetParameter("@wholastmodified", DbType.Int32, this.OpContext.WhoAmI)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE OnlineForm SET title=@title,description=@description,formnum=@formnum,usewizard=@usewizard\r\n        ,submitmessage=@submitmessage,submitbuttontext=@submitbuttontext,shortcode=@shortcode\r\n        ,startdate=@startdate,enddate=@enddate,requireslogin=@requireslogin,canonlybefilledinonce=@canonlybefilledinonce\r\n        ,captcha=@captcha,studentemailconfirmationtemplateid=@studentemailconfirmationtemplateid,staffemailconfirmationtemplateid=@staffemailconfirmationtemplateid\r\n        ,restricttogroupid=@restricttogroupid,isdeleted=@isdeleted,isdisabled=@isdisabled\r\n        ,wholastmodified=@wholastmodified,datelastmodified=getdate()\r\nWHERE   onlineformid=@onlineformid", parameters);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0001B608 File Offset: 0x00019808
		private OnlineForm GetOnlineFormFromRecord(IDataReader reader, IBatchDecryptor batchDecryptor)
		{
			object obj = reader["onlineformid"];
			object obj2 = reader["onlineformtitle"];
			object obj3 = reader["onlineformdescription"];
			object obj4 = reader["usewizard"];
			object obj5 = reader["submitmessage"];
			object obj6 = reader["submitbuttontext"];
			object obj7 = reader["shortcode"];
			object obj8 = reader["startdate"];
			object obj9 = reader["enddate"];
			object obj10 = reader["requireslogin"];
			object obj11 = reader["captcha"];
			object obj12 = reader["restricttogroupid"];
			object obj13 = reader["isdeleted"];
			object obj14 = reader["isdisabled"];
			object obj15 = reader["datecreated"];
			object obj16 = reader["datelastmodified"];
			DynamicForm dynamicFormFromRecord = DynamicFormsDAO.GetDynamicFormFromRecord(reader);
			bool flag = obj8 == DBNull.Value;
			DateTime? startDate;
			if (flag)
			{
				startDate = null;
			}
			else
			{
				startDate = new DateTime?((DateTime)obj8);
			}
			bool flag2 = obj9 == DBNull.Value;
			DateTime? endDate;
			if (flag2)
			{
				endDate = null;
			}
			else
			{
				endDate = new DateTime?((DateTime)obj9);
			}
			Group group;
			if (obj12 != DBNull.Value)
			{
				(group = new Group()).GroupId = (int)obj12;
			}
			else
			{
				group = null;
			}
			Group restrictedToGroup = group;
			return new OnlineForm
			{
				OnlineFormId = ((obj == DBNull.Value) ? 0 : ((int)obj)),
				Title = (string)obj2,
				Description = (string)obj3,
				Form = dynamicFormFromRecord,
				UseWizard = (obj4 != DBNull.Value && Convert.ToBoolean(obj4)),
				SubmitMessage = ((obj5 == DBNull.Value) ? "" : ((string)obj6)),
				SubmitButtonText = ((obj6 == DBNull.Value) ? "" : ((string)obj6)),
				ShortCode = ((obj7 == DBNull.Value) ? "" : ((string)obj7)),
				StartDate = startDate,
				EndDate = endDate,
				RequiresLogin = (obj10 != DBNull.Value && Convert.ToBoolean(obj10)),
				CanOnlyBeFilledInOnce = (!(reader["canonlybefilledinonce"] is DBNull) && Convert.ToBoolean(reader["canonlybefilledinonce"])),
				Captcha = ((obj11 == DBNull.Value) ? 0 : ((int)obj11)),
				StudentEmailConfirmationTemplateId = ((reader["studentemailconfirmationtemplateid"] is DBNull) ? 0 : ((int)reader["studentemailconfirmationtemplateid"])),
				StaffEmailConfirmationTemplateId = ((reader["staffemailconfirmationtemplateid"] is DBNull) ? 0 : ((int)reader["staffemailconfirmationtemplateid"])),
				RestrictedToGroup = restrictedToGroup,
				IsDeleted = (obj13 != DBNull.Value && Convert.ToBoolean(obj14)),
				IsDisabled = (obj14 != DBNull.Value && Convert.ToBoolean(obj14)),
				DateCreated = ((obj15 is DBNull) ? DateTime.MinValue : ((DateTime)obj15)),
				DateLastModified = ((obj16 is DBNull) ? null : new DateTime?((DateTime)obj16)),
				WhoCreated = PeopleDAO.GetBasicPersonFromRecord("whocreated", reader, batchDecryptor),
				WhoLastModified = ((!(reader["wholastmodifiedpersonid"] is DBNull) && (int)reader["wholastmodifiedpersonid"] > 0) ? PeopleDAO.GetBasicPersonFromRecord("wholastmodified", reader, batchDecryptor) : null)
			};
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0001B9CC File Offset: 0x00019BCC
		public List<OnlineForm> GetAllOnlineForms()
		{
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT s.onlineformid,s.title AS onlineformtitle,s.[description] AS onlineformdescription,\r\n\t\ts.formnum AS screennum,s.usewizard,s.submitmessage,s.submitbuttontext,s.shortcode,s.startdate,s.enddate,s.requireslogin,s.canonlybefilledinonce,\r\n        s.captcha,s.studentemailconfirmationtemplateid,s.staffemailconfirmationtemplateid,s.restricttogroupid,s.isdeleted,s.isdisabled,s.whocreated AS whocreatedpersonid,s.wholastmodified AS wholastmodifiedpersonid,s.datecreated,s.datelastmodified,\r\n        p.firstname AS whocreatedfirstname,p.lastname AS whocreatedlastname,p.student_no AS whocreatedstudent_no,\r\n        p2.firstname AS wholastmodifiedfirstname,p2.lastname AS wholastmodifiedlastname,p2.student_no AS wholastmodifiedstudent_no,\r\n        g.[description] AS restricttodescription,g.isprimary AS restricttoisprimary,\r\n        sc.typecode,sc.[description],sc.shorttext,sc.bottomless,sc.columnwidth,\r\n        sc.longdescription,sc.showasbutton,sc.iconindex,sc.largeiconindex,sc.isactive,sc.screenuniqueid\r\nFROM    OnlineForm s LEFT JOIN people p ON p.personid=s.whocreated\r\n        LEFT JOIN people p2 ON p2.personid=s.wholastmodified\r\n        LEFT JOIN screens sc ON sc.screennum=s.formnum\r\n        LEFT JOIN groups g ON g.GroupID=s.RestrictToGroupId\r\nWHERE   s.isdeleted=0\r\nORDER BY s.title"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<OnlineForm> list = new List<OnlineForm>();
					IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						OnlineForm onlineFormFromRecord = this.GetOnlineFormFromRecord(dataReader, batchDecryptor);
						bool flag2 = onlineFormFromRecord != null;
						if (flag2)
						{
							list.Add(onlineFormFromRecord);
						}
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0001BA64 File Offset: 0x00019C64
		public List<OnlineForm> GetActiveOnlineForms()
		{
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT s.onlineformid,s.title AS onlineformtitle,s.[description] AS onlineformdescription,\r\n\t\ts.formnum AS screennum,s.usewizard,s.submitmessage,s.submitbuttontext,s.shortcode,s.startdate,s.enddate,s.requireslogin,s.canonlybefilledinonce,\r\n        s.captcha,s.studentemailconfirmationtemplateid,s.staffemailconfirmationtemplateid,s.restricttogroupid,s.isdeleted,s.isdisabled,s.whocreated AS whocreatedpersonid,s.wholastmodified AS wholastmodifiedpersonid,s.datecreated,s.datelastmodified,\r\n        p.firstname AS whocreatedfirstname,p.lastname AS whocreatedlastname,p.student_no AS whocreatedstudent_no,\r\n        p2.firstname AS wholastmodifiedfirstname,p2.lastname AS wholastmodifiedlastname,p2.student_no AS wholastmodifiedstudent_no,\r\n        g.[description] AS restricttodescription,g.isprimary AS restricttoisprimary,\r\n        sc.typecode,sc.description,sc.shorttext,sc.bottomless,sc.columnwidth,\r\n        sc.longdescription,sc.showasbutton,sc.iconindex,sc.largeiconindex,sc.isactive,sc.screenuniqueid\r\nFROM    OnlineForm s LEFT JOIN people p ON p.personid=s.whocreated\r\n        LEFT JOIN people p2 ON p2.personid=s.wholastmodified\r\n        LEFT JOIN screens sc ON sc.screennum=s.formnum\r\n        LEFT JOIN groups g ON g.GroupID=s.RestrictToGroupId\r\nWHERE   s.isdeleted=0 AND s.isdisabled=0 AND s.formnum > 0\r\n        AND \r\n        (\r\n            (s.startdate is NULL AND s.enddate IS NULL )\r\n\t\t\tOR\r\n            (NOT s.startdate IS NULL AND NOT s.enddate IS NULL AND getdate() BETWEEN s.startdate AND s.enddate)\r\n            OR\r\n            (NOT s.startdate IS NULL AND s.enddate IS NULL AND getdate() >= s.startdate)\r\n            OR\r\n            (s.startdate IS NULL AND NOT s.enddate IS NULL AND getdate() <= s.enddate)\r\n        )\r\nORDER BY s.title"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<OnlineForm> list = new List<OnlineForm>();
					IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						OnlineForm onlineFormFromRecord = this.GetOnlineFormFromRecord(dataReader, batchDecryptor);
						bool flag2 = onlineFormFromRecord != null;
						if (flag2)
						{
							list.Add(onlineFormFromRecord);
						}
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0001BAFC File Offset: 0x00019CFC
		[DebuggerStepThrough]
		public Task<List<OnlineForm>> GetActiveOnlineFormsAsync()
		{
			OnlineFormDAO.<GetActiveOnlineFormsAsync>d__14 <GetActiveOnlineFormsAsync>d__ = new OnlineFormDAO.<GetActiveOnlineFormsAsync>d__14();
			<GetActiveOnlineFormsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<OnlineForm>>.Create();
			<GetActiveOnlineFormsAsync>d__.<>4__this = this;
			<GetActiveOnlineFormsAsync>d__.<>1__state = -1;
			<GetActiveOnlineFormsAsync>d__.<>t__builder.Start<OnlineFormDAO.<GetActiveOnlineFormsAsync>d__14>(ref <GetActiveOnlineFormsAsync>d__);
			return <GetActiveOnlineFormsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0001BB40 File Offset: 0x00019D40
		public OnlineForm GetOnlineForm(int OnlineFormId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@onlineformid", DbType.Int32, OnlineFormId)
			};
			OnlineForm result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT s.onlineformid,s.title AS onlineformtitle,s.[description] AS onlineformdescription,\r\n\t\ts.formnum AS screennum,s.usewizard,s.submitmessage,s.submitbuttontext,s.shortcode,s.startdate,s.enddate,s.requireslogin,s.canonlybefilledinonce,\r\n        s.captcha,s.studentemailconfirmationtemplateid,s.staffemailconfirmationtemplateid,s.restricttogroupid,s.isdeleted,s.isdisabled,s.whocreated AS whocreatedpersonid,s.wholastmodified AS wholastmodifiedpersonid,s.datecreated,s.datelastmodified,\r\n        p.firstname AS whocreatedfirstname,p.lastname AS whocreatedlastname,p.student_no AS whocreatedstudent_no,\r\n        p2.firstname AS wholastmodifiedfirstname,p2.lastname AS wholastmodifiedlastname,p2.student_no AS wholastmodifiedstudent_no,\r\n        g.[description] AS restricttodescription,g.isprimary AS restricttoisprimary,\r\n        sc.typecode,sc.description,sc.shorttext,sc.bottomless,sc.columnwidth,\r\n        sc.longdescription,sc.showasbutton,sc.iconindex,sc.largeiconindex,sc.isactive,sc.screenuniqueid\r\nFROM    OnlineForm s LEFT JOIN people p ON p.personid=s.whocreated\r\n        LEFT JOIN people p2 ON p2.personid=s.wholastmodified\r\n        LEFT JOIN screens sc ON sc.screennum=s.formnum\r\n        LEFT JOIN groups g ON g.GroupID=s.RestrictToGroupId\r\nWHERE   s.onlineformid=@onlineformid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<OnlineForm> list = new List<OnlineForm>();
					IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						OnlineForm onlineFormFromRecord = this.GetOnlineFormFromRecord(dataReader, batchDecryptor);
						bool flag2 = onlineFormFromRecord == null;
						if (!flag2)
						{
							list.Add(onlineFormFromRecord);
							break;
						}
					}
					result = ((list.Count > 0) ? list[0] : null);
				}
			}
			return result;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0001BC10 File Offset: 0x00019E10
		public void DeleteOnlineForm(int OnlineFormId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@id", DbType.Int32, OnlineFormId)
			};
			databaseLayer.ExecuteNonQuery("UPDATE OnlineForm SET isdeleted=1 WHERE onlineformid=@id", parameters);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0001BC61 File Offset: 0x00019E61
		public void DisableOnlineForm(int OnlineFormId)
		{
			this.EnableOrDisableOnlineForm(OnlineFormId, true);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0001BC6D File Offset: 0x00019E6D
		public void EnableOnlineForm(int OnlineFormId)
		{
			this.EnableOrDisableOnlineForm(OnlineFormId, false);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001BC7C File Offset: 0x00019E7C
		private void EnableOrDisableOnlineForm(int OnlineFormId, bool newDisabled)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@id", DbType.Int32, OnlineFormId),
				databaseLayer.GetParameter("@newdisabled", DbType.Boolean, newDisabled)
			};
			databaseLayer.ExecuteNonQuery("UPDATE OnlineForm SET isdisabled=@newdisabled WHERE onlineformid=@id", parameters);
		}
	}
}
