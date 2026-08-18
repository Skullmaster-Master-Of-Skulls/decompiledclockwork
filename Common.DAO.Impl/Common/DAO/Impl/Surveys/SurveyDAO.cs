using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.Surveys;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Surveys;

namespace TechnoPro.Common.DAO.Impl.Surveys
{
	// Token: 0x0200003E RID: 62
	public class SurveyDAO : ISurveyDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000DF1F File Offset: 0x0000C11F
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000DF27 File Offset: 0x0000C127
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x06000193 RID: 403 RVA: 0x0000DF30 File Offset: 0x0000C130
		public SurveyDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000DF61 File Offset: 0x0000C161
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000DF69 File Offset: 0x0000C169
		public OperationContext OpContext { get; set; }

		// Token: 0x06000196 RID: 406 RVA: 0x0000DF74 File Offset: 0x0000C174
		public int CreateNewSurvey(Survey Survey)
		{
			Survey.WhoCreated = new BasicPerson
			{
				PersonId = this.OpContext.WhoAmI
			};
			Survey.DateCreated = DateTime.Now;
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@whocreated", DbType.Int32, this.OpContext.WhoAmI),
				this.DatabaseManager.GetParameter("@title", DbType.String, Survey.Title ?? ""),
				this.DatabaseManager.GetParameter("@description", DbType.String, Survey.Description ?? ""),
				this.DatabaseManager.GetParameter("@formnum", DbType.Int32, (Survey.Form == null) ? 0 : Survey.Form.ScreenNum),
				this.DatabaseManager.GetParameter("@usewizard", DbType.Boolean, Survey.UseWizard),
				this.DatabaseManager.GetParameter("@submitmessage", DbType.String, Survey.SubmitMessage ?? ""),
				this.DatabaseManager.GetParameter("@submitbuttontext", DbType.String, Survey.SubmitButtonText ?? ""),
				this.DatabaseManager.GetParameter("@shortcode", DbType.String, Survey.ShortCode ?? ""),
				(Survey.StartDate != null) ? this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, Survey.StartDate.Value) : this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, DBNull.Value),
				(Survey.EndDate != null) ? this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, Survey.EndDate.Value) : this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, DBNull.Value),
				this.DatabaseManager.GetParameter("@requireslogin", DbType.Boolean, Survey.RequiresLogin),
				this.DatabaseManager.GetParameter("@canonlybefilledinonce", DbType.Boolean, Survey.CanOnlyBeFilledInOnce),
				this.DatabaseManager.GetParameter("@captcha", DbType.Int32, Survey.Captcha),
				this.DatabaseManager.GetParameter("@studentemailconfirmationtemplateid", DbType.Int32, (Survey.StudentEmailConfirmationTemplateId > 0) ? Survey.StudentEmailConfirmationTemplateId : DBNull.Value),
				this.DatabaseManager.GetParameter("@staffemailconfirmationtemplateid", DbType.Int32, (Survey.StaffEmailConfirmationTemplateId > 0) ? Survey.StaffEmailConfirmationTemplateId : DBNull.Value),
				(Survey.RestrictedToGroup != null && Survey.RestrictedToGroup.GroupId > 0) ? this.DatabaseManager.GetParameter("@restricttogroupid", DbType.Int32, Survey.RestrictedToGroup.GroupId) : this.DatabaseManager.GetParameter("@restricttogroupid", DbType.Int32, DBNull.Value),
				this.DatabaseManager.GetParameter("@isdeleted", DbType.Boolean, Survey.IsDeleted),
				this.DatabaseManager.GetParameter("@isdisabled", DbType.Boolean, Survey.IsDisabled)
			};
			object obj = this.DatabaseManager.ExecuteScalar("INSERT INTO survey (title,description,formnum,usewizard,submitmessage,submitbuttontext\r\n        ,shortcode,startdate,enddate,requireslogin,canonlybefilledinonce,captcha,studentemailconfirmationtemplateid,staffemailconfirmationtemplateid\r\n        ,restricttogroupid,isdeleted,isdisabled,whocreated)\r\nVALUES (@title,@description,@formnum,@usewizard,@submitmessage,@submitbuttontext\r\n        ,@shortcode,@startdate,@enddate,@requireslogin,@canonlybefilledinonce,@captcha,@studentemailconfirmationtemplateid,@staffemailconfirmationtemplateid\r\n        ,@restricttogroupid,@isdeleted,@isdisabled,@whocreated);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS surveyid", parameters);
			bool flag = obj != null && obj != DBNull.Value && obj is int;
			if (flag)
			{
				Survey.SurveyId = (int)obj;
			}
			return Survey.SurveyId;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000E31C File Offset: 0x0000C51C
		public void UpdateSurvey(Survey Survey)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@surveyid", DbType.Int32, Survey.SurveyId),
				this.DatabaseManager.GetParameter("@title", DbType.String, Survey.Title ?? ""),
				this.DatabaseManager.GetParameter("@description", DbType.String, Survey.Description ?? ""),
				this.DatabaseManager.GetParameter("@formnum", DbType.Int32, (Survey.Form == null) ? 0 : Survey.Form.ScreenNum),
				this.DatabaseManager.GetParameter("@usewizard", DbType.Boolean, Survey.UseWizard),
				this.DatabaseManager.GetParameter("@submitmessage", DbType.String, Survey.SubmitMessage ?? ""),
				this.DatabaseManager.GetParameter("@submitbuttontext", DbType.String, Survey.SubmitButtonText ?? ""),
				this.DatabaseManager.GetParameter("@shortcode", DbType.String, Survey.ShortCode ?? ""),
				(Survey.StartDate != null) ? this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, Survey.StartDate.Value) : this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, DBNull.Value),
				(Survey.EndDate != null) ? this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, Survey.EndDate.Value) : this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, DBNull.Value),
				this.DatabaseManager.GetParameter("@requireslogin", DbType.Boolean, Survey.RequiresLogin),
				this.DatabaseManager.GetParameter("@canonlybefilledinonce", DbType.Boolean, Survey.CanOnlyBeFilledInOnce),
				this.DatabaseManager.GetParameter("@captcha", DbType.Int32, Survey.Captcha),
				this.DatabaseManager.GetParameter("@studentemailconfirmationtemplateid", DbType.Int32, (Survey.StudentEmailConfirmationTemplateId > 0) ? Survey.StudentEmailConfirmationTemplateId : DBNull.Value),
				this.DatabaseManager.GetParameter("@staffemailconfirmationtemplateid", DbType.Int32, (Survey.StaffEmailConfirmationTemplateId > 0) ? Survey.StaffEmailConfirmationTemplateId : DBNull.Value),
				(Survey.RestrictedToGroup != null && Survey.RestrictedToGroup.GroupId > 0) ? this.DatabaseManager.GetParameter("@restricttogroupid", DbType.Int32, Survey.RestrictedToGroup.GroupId) : this.DatabaseManager.GetParameter("@restricttogroupid", DbType.Int32, DBNull.Value),
				this.DatabaseManager.GetParameter("@isdeleted", DbType.Boolean, Survey.IsDeleted),
				this.DatabaseManager.GetParameter("@isdisabled", DbType.Boolean, Survey.IsDisabled),
				this.DatabaseManager.GetParameter("@wholastmodified", DbType.Int32, this.OpContext.WhoAmI)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE survey SET title=@title,description=@description,formnum=@formnum,usewizard=@usewizard\r\n        ,submitmessage=@submitmessage,submitbuttontext=@submitbuttontext,shortcode=@shortcode\r\n        ,startdate=@startdate,enddate=@enddate,requireslogin=@requireslogin,canonlybefilledinonce=@canonlybefilledinonce\r\n        ,captcha=@captcha,studentemailconfirmationtemplateid=@studentemailconfirmationtemplateid,staffemailconfirmationtemplateid=@staffemailconfirmationtemplateid\r\n        ,restricttogroupid=@restricttogroupid,isdeleted=@isdeleted,isdisabled=@isdisabled\r\n        ,wholastmodified=@wholastmodified,datelastmodified=getdate()\r\nWHERE   surveyid=@surveyid", parameters);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000E684 File Offset: 0x0000C884
		private Survey GetSurveyFromRecord(IDataReader reader, IBatchDecryptor batchDecryptor)
		{
			object obj = reader["surveyid"];
			object obj2 = reader["surveytitle"];
			object obj3 = reader["surveydescription"];
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
			return new Survey
			{
				SurveyId = ((obj == DBNull.Value) ? 0 : ((int)obj)),
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

		// Token: 0x06000199 RID: 409 RVA: 0x0000EA48 File Offset: 0x0000CC48
		public List<Survey> GetAllSurveys()
		{
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT s.surveyid,s.title AS surveytitle,s.[description] AS surveydescription,\r\n\t\ts.formnum AS screennum,s.usewizard,s.submitmessage,s.submitbuttontext,s.shortcode,s.startdate,s.enddate,s.requireslogin,s.canonlybefilledinonce,\r\n        s.captcha,s.studentemailconfirmationtemplateid,s.staffemailconfirmationtemplateid,s.restricttogroupid,s.isdeleted,s.isdisabled,s.whocreated AS whocreatedpersonid,s.wholastmodified AS wholastmodifiedpersonid,s.datecreated,s.datelastmodified,\r\n        p.firstname AS whocreatedfirstname,p.lastname AS whocreatedlastname,p.student_no AS whocreatedstudent_no,\r\n        p2.firstname AS wholastmodifiedfirstname,p2.lastname AS wholastmodifiedlastname,p2.student_no AS wholastmodifiedstudent_no,\r\n        g.[description] AS restricttodescription,g.isprimary AS restricttoisprimary,\r\n        sc.typecode,sc.[description],sc.shorttext,sc.bottomless,sc.columnwidth,\r\n        sc.longdescription,sc.showasbutton,sc.iconindex,sc.largeiconindex,sc.isactive,sc.screenuniqueid\r\nFROM    survey s LEFT JOIN people p ON p.personid=s.whocreated\r\n        LEFT JOIN people p2 ON p2.personid=s.wholastmodified\r\n        LEFT JOIN screens sc ON sc.screennum=s.formnum\r\n        LEFT JOIN groups g ON g.GroupID=s.RestrictToGroupId\r\nWHERE   s.isdeleted=0\r\nORDER BY s.title"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<Survey> list = new List<Survey>();
					IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						Survey surveyFromRecord = this.GetSurveyFromRecord(dataReader, batchDecryptor);
						bool flag2 = surveyFromRecord != null;
						if (flag2)
						{
							list.Add(surveyFromRecord);
						}
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000EAE0 File Offset: 0x0000CCE0
		public List<Survey> GetActiveSurveys()
		{
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT s.surveyid,s.title AS surveytitle,s.[description] AS surveydescription,\r\n\t\ts.formnum AS screennum,s.usewizard,s.submitmessage,s.submitbuttontext,s.shortcode,s.startdate,s.enddate,s.requireslogin,s.canonlybefilledinonce,\r\n        s.captcha,s.studentemailconfirmationtemplateid,s.staffemailconfirmationtemplateid,s.restricttogroupid,s.isdeleted,s.isdisabled,s.whocreated AS whocreatedpersonid,s.wholastmodified AS wholastmodifiedpersonid,s.datecreated,s.datelastmodified,\r\n        p.firstname AS whocreatedfirstname,p.lastname AS whocreatedlastname,p.student_no AS whocreatedstudent_no,\r\n        p2.firstname AS wholastmodifiedfirstname,p2.lastname AS wholastmodifiedlastname,p2.student_no AS wholastmodifiedstudent_no,\r\n        g.[description] AS restricttodescription,g.isprimary AS restricttoisprimary,\r\n        sc.typecode,sc.description,sc.shorttext,sc.bottomless,sc.columnwidth,\r\n        sc.longdescription,sc.showasbutton,sc.iconindex,sc.largeiconindex,sc.isactive,sc.screenuniqueid\r\nFROM    survey s LEFT JOIN people p ON p.personid=s.whocreated\r\n        LEFT JOIN people p2 ON p2.personid=s.wholastmodified\r\n        LEFT JOIN screens sc ON sc.screennum=s.formnum\r\n        LEFT JOIN groups g ON g.GroupID=s.RestrictToGroupId\r\nWHERE   s.isdeleted=0 AND s.isdisabled=0 \r\n        AND \r\n        (\r\n            (s.startdate is NULL AND s.enddate IS NULL )\r\n\t\t\tOR\r\n            (NOT s.startdate IS NULL AND NOT s.enddate IS NULL AND getdate() BETWEEN s.startdate AND s.enddate)\r\n            OR\r\n            (NOT s.startdate IS NULL AND s.enddate IS NULL AND getdate() >= s.startdate)\r\n            OR\r\n            (s.startdate IS NULL AND NOT s.enddate IS NULL AND getdate() <= s.enddate)\r\n        )\r\nORDER BY s.title"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<Survey> list = new List<Survey>();
					IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						Survey surveyFromRecord = this.GetSurveyFromRecord(dataReader, batchDecryptor);
						bool flag2 = surveyFromRecord != null;
						if (flag2)
						{
							list.Add(surveyFromRecord);
						}
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000EB78 File Offset: 0x0000CD78
		public Survey GetSurvey(int SurveyId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@surveyid", DbType.Int32, SurveyId)
			};
			Survey result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT s.surveyid,s.title AS surveytitle,s.[description] AS surveydescription,\r\n\t\ts.formnum AS screennum,s.usewizard,s.submitmessage,s.submitbuttontext,s.shortcode,s.startdate,s.enddate,s.requireslogin,s.canonlybefilledinonce,\r\n        s.captcha,s.studentemailconfirmationtemplateid,s.staffemailconfirmationtemplateid,s.restricttogroupid,s.isdeleted,s.isdisabled,s.whocreated AS whocreatedpersonid,s.wholastmodified AS wholastmodifiedpersonid,s.datecreated,s.datelastmodified,\r\n        p.firstname AS whocreatedfirstname,p.lastname AS whocreatedlastname,p.student_no AS whocreatedstudent_no,\r\n        p2.firstname AS wholastmodifiedfirstname,p2.lastname AS wholastmodifiedlastname,p2.student_no AS wholastmodifiedstudent_no,\r\n        g.[description] AS restricttodescription,g.isprimary AS restricttoisprimary,\r\n        sc.typecode,sc.description,sc.shorttext,sc.bottomless,sc.columnwidth,\r\n        sc.longdescription,sc.showasbutton,sc.iconindex,sc.largeiconindex,sc.isactive,sc.screenuniqueid\r\nFROM    survey s LEFT JOIN people p ON p.personid=s.whocreated\r\n        LEFT JOIN people p2 ON p2.personid=s.wholastmodified\r\n        LEFT JOIN screens sc ON sc.screennum=s.formnum\r\n        LEFT JOIN groups g ON g.GroupID=s.RestrictToGroupId\r\nWHERE   s.surveyid=@surveyid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<Survey> list = new List<Survey>();
					IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						Survey surveyFromRecord = this.GetSurveyFromRecord(dataReader, batchDecryptor);
						bool flag2 = surveyFromRecord == null;
						if (!flag2)
						{
							list.Add(surveyFromRecord);
							break;
						}
					}
					result = ((list.Count > 0) ? list[0] : null);
				}
			}
			return result;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000EC48 File Offset: 0x0000CE48
		public void DeleteSurvey(int SurveyId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@id", DbType.Int32, SurveyId)
			};
			databaseLayer.ExecuteNonQuery("UPDATE Survey SET isdeleted=1 WHERE surveyid=@id", parameters);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000EC99 File Offset: 0x0000CE99
		public void DisableSurvey(int SurveyId)
		{
			this.EnableOrDisableSurvey(SurveyId, true);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000ECA5 File Offset: 0x0000CEA5
		public void EnableSurvey(int SurveyId)
		{
			this.EnableOrDisableSurvey(SurveyId, false);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000ECB4 File Offset: 0x0000CEB4
		private void EnableOrDisableSurvey(int SurveyId, bool newDisabled)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@id", DbType.Int32, SurveyId),
				databaseLayer.GetParameter("@newdisabled", DbType.Boolean, newDisabled)
			};
			databaseLayer.ExecuteNonQuery("UPDATE survey SET isdisabled=@newdisabled WHERE surveyid=@id", parameters);
		}
	}
}
