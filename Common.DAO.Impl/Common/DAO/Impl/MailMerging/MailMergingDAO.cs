using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.DAO.MailMerging;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.MailMergeEntities;

namespace TechnoPro.Common.DAO.Impl.MailMerging
{
	// Token: 0x02000095 RID: 149
	public class MailMergingDAO : IMailMergingDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00021C05 File Offset: 0x0001FE05
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x00021C0D File Offset: 0x0001FE0D
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00021C18 File Offset: 0x0001FE18
		private DynamicDataDAO dynamicDataDao
		{
			get
			{
				DynamicDataDAO result;
				if ((result = this._dynamicDataDao) == null)
				{
					result = (this._dynamicDataDao = new DynamicDataDAO(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00021C43 File Offset: 0x0001FE43
		public MailMergingDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00021C74 File Offset: 0x0001FE74
		// (set) Token: 0x060003DD RID: 989 RVA: 0x00021C7C File Offset: 0x0001FE7C
		public OperationContext OpContext { get; set; }

		// Token: 0x060003DE RID: 990 RVA: 0x00021C88 File Offset: 0x0001FE88
		public List<DynamicData> LoadAllPerStudentData(int PersonId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    ps.personid,ps.controlid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename\r\nFROM        perstudentdata2 ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       ps.personid=@pid\r\n            AND ps.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum IN (SELECT screennum FROM screens))", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return this.dynamicDataDao.GetDataListFromRecords(dataReader);
				}
			}
			return null;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00021D0C File Offset: 0x0001FF0C
		public List<DynamicData> LoadAllPerDateData(int PersonId, int PerDateId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, PerDateId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    ps.personid,ps.controlid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename\r\nFROM        pmdata2 ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       ps.personid=@pid AND ps.appointmentid=@appid\r\n            AND ps.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum IN (SELECT screennum FROM screens))", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return this.dynamicDataDao.GetDataListFromRecords(dataReader);
				}
			}
			return null;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00021DAC File Offset: 0x0001FFAC
		public List<DynamicData> LoadAllPerAppointmentData(int PersonId, int AppointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    ps.personid,ps.controlid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename\r\nFROM        perappdata2 ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       ps.personid=@pid AND ps.appointmentid=@appid\r\n            AND ps.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum IN (SELECT screennum FROM screens))", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return this.dynamicDataDao.GetDataListFromRecords(dataReader);
				}
			}
			return null;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00021E4C File Offset: 0x0002004C
		public List<DynamicData> LoadAllAccommodationTemplateData(int PersonId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    ps.personid,ps.controlid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename\r\nFROM        accommodationdata ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       ps.personid=@pid\r\n            AND ps.courseid=0\r\n            AND ps.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=4)", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					bool flag2 = dataReader.Read();
					if (flag2)
					{
						return this.dynamicDataDao.GetDataListFromRecords(dataReader);
					}
				}
			}
			return null;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00021EE0 File Offset: 0x000200E0
		public MailMergeContext LoadSampleContextFromDatabase(int OptionalPersonId, int OptionalAppointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, OptionalPersonId),
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, OptionalAppointmentId)
			};
			MailMergeContext mailMergeContext = new MailMergeContext();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT TOP 1 p.personid,c.luCourseID,att.AppointmentID,pl.instructorid,c.CoursesID,app.examid,ac.AppointmentCourseID \r\n\t\t,COALESCE(c.luCourseID,0)\r\n\t\t\t+ COALESCE(att.AppointmentID,0)\r\n\t\t\t+ COALESCE(pl.instructorid,0)\r\n\t\t\t+ COALESCE(app.examid,0) \r\n\t\t\t+ COALESCE(ac.AppointmentCourseID,0)\r\n\t\t\tAS bob\r\nFROM\tpeoplegroups pg LEFT JOIN people p ON p.personid=pg.personid \r\n\t\tLEFT JOIN courses c ON c.personID=pg.PersonID AND (c.registrationstatus is null OR NOT c.registrationstatus=2)\r\n\t\tLEFT JOIN vInstructorPrimaryList pl ON pl.lucourseid=c.luCourseID \r\n\t\tLEFT JOIN attendees att ON att.PersonID=pg.PersonID AND att.noShow=0 AND NOT att.AppointmentID IN (SELECT appointmentid FROM appointments WHERE cancelled=1)\r\n\t\tLEFT JOIN appointments app ON app.AppointmentID=att.AppointmentID \r\n\t\tLEFT JOIN appointmentcourses ac ON ac.AppointmentID=att.AppointmentID \r\nWHERE\t((@pid IS NULL OR @pid<1) OR @pid=pg.personid)\r\n\t\tAND ((@appid IS NULL OR @appid<1) OR @appid=att.AppointmentID)\r\n\t\tAND pg.groupid=1 AND p.isActive=1\r\nORDER BY bob DESC", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					if (dataReader.Read())
					{
						mailMergeContext.PersonId = ((dataReader["personid"] is DBNull) ? 0 : ((int)dataReader["personid"]));
						mailMergeContext.LuCourseId = ((dataReader["lucourseid"] is DBNull) ? 0 : ((int)dataReader["lucourseid"]));
						mailMergeContext.AppointmentId = ((dataReader["appointmentid"] is DBNull) ? 0 : ((int)dataReader["appointmentid"]));
						mailMergeContext.InstructorId = ((dataReader["instructorid"] is DBNull) ? 0 : ((int)dataReader["instructorid"]));
						mailMergeContext.CourseId = new int?((dataReader["coursesid"] is DBNull) ? 0 : ((int)dataReader["coursesid"]));
						mailMergeContext.ExamId = ((dataReader["examid"] is DBNull) ? 0 : ((int)dataReader["examid"]));
					}
				}
			}
			return mailMergeContext;
		}

		// Token: 0x040001BF RID: 447
		private DynamicDataDAO _dynamicDataDao;
	}
}
