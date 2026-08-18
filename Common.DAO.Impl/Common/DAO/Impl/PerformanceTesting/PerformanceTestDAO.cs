using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.DAO.PerformanceTesting;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;
using TechnoPro.Common.Public.Entities.Cases;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.DAO.Impl.PerformanceTesting
{
	// Token: 0x0200006F RID: 111
	public class PerformanceTestDAO : IPerformanceTestDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00016185 File Offset: 0x00014385
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0001618D File Offset: 0x0001438D
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x0600029F RID: 671 RVA: 0x00016196 File Offset: 0x00014396
		public PerformanceTestDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x000161C7 File Offset: 0x000143C7
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x000161CF File Offset: 0x000143CF
		public OperationContext OpContext { get; set; }

		// Token: 0x060002A2 RID: 674 RVA: 0x000161D8 File Offset: 0x000143D8
		internal static IList<Appointment> GetAppointmentsFromReader(IDataReader reader, OperationContext opContext)
		{
			bool flag = reader != null;
			IList<Appointment> result;
			if (flag)
			{
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
				IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
				List<Appointment> list = new List<Appointment>();
				Appointment appointment = null;
				while (reader.Read())
				{
					int num = (int)reader["appointmentid"];
					bool flag2 = appointment == null || appointment.AppointmentId != num;
					if (flag2)
					{
						appointment = BaseAppointmentDAO.GetMainBaseExtendedAppointment<Appointment>(reader, opContext, batchDecryptor);
						list.Add(appointment);
					}
					BaseAppointmentDAO.AddExtendedInfoToBaseExtendedAppointment(reader, appointment, opContext, batchDecryptor);
					PerformanceTestDAO.AddCalendarInfoToBaseExtendedAppointment(reader, ref appointment, opContext, batchDecryptor);
				}
				result = list;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00016290 File Offset: 0x00014490
		private static void AddCalendarInfoToBaseExtendedAppointment(IDataReader reader, ref Appointment app, OperationContext opContext, IBatchDecryptor batchDecryptor = null)
		{
			bool flag = app == null || reader == null;
			if (!flag)
			{
				bool flag2 = PerformanceTestDAO.ReaderContainsColumn(reader, "iconnum");
				if (flag2)
				{
					int iconNum = (reader["iconnum"] is DBNull) ? 0 : ((int)reader["iconnum"]);
					bool flag3 = iconNum > 0 && app.Icons.FirstOrDefault((AppointmentIcon f) => f.IconNum == iconNum) == null;
					if (flag3)
					{
						int num = (reader["screennum"] is DBNull) ? 0 : ((int)reader["screennum"]);
						AppointmentIcon appointmentIcon = new AppointmentIcon();
						object screen;
						if (num <= 0)
						{
							screen = null;
						}
						else
						{
							(screen = new DynamicFormBase()).ScreenNum = num;
						}
						appointmentIcon.Screen = screen;
						appointmentIcon.Icon = new IconInfo
						{
							IconNum = iconNum,
							IconText = reader["icontext"].ToString(),
							IconLetterIdentifier = reader["iconletteridentifier"].ToString()
						};
						AppointmentIcon item = appointmentIcon;
						app.Icons.Add(item);
					}
				}
				bool flag4 = app.CaseInfo == null && PerformanceTestDAO.ReaderContainsColumn(reader, "caseid");
				if (flag4)
				{
					int num2 = (reader["caseid"] == DBNull.Value) ? 0 : ((int)reader["caseid"]);
					bool flag5 = num2 > 0;
					if (flag5)
					{
						app.CaseInfo = new CaseBase
						{
							InfoPcId = num2
						};
					}
				}
				bool flag6 = app.TestExamInfo == null && PerformanceTestDAO.ReaderContainsColumn(reader, "lucourseid");
				if (flag6)
				{
					int num3 = (reader["lucourseid"] == DBNull.Value) ? 0 : ((int)reader["lucourseid"]);
					bool flag7 = num3 > 0;
					if (flag7)
					{
						IEncryption encryption = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null).Encryption;
						app.TestExamInfo = new BasicAppointmentTestExamInfo
						{
							Course = new LookupCourseBase
							{
								Subject = new LookupSubject
								{
									SubjectDescription = reader["subject"].ToString()
								},
								Course = reader["course"].ToString(),
								LuCourseId = num3
							},
							TestNote = ((reader["testnote"] == DBNull.Value) ? "" : encryption.Decrypt((byte[])reader["testnote"])),
							StudentNote = ((reader["studentnote"] == DBNull.Value) ? "" : encryption.Decrypt((byte[])reader["studentnote"])),
							ExamId = (PerformanceTestDAO.ReaderContainsColumn(reader, "examid") ? ((reader["examid"] is DBNull) ? 0 : ((int)reader["examid"])) : 0)
						};
					}
				}
				bool flag8 = app.WorkshopInfo == null && PerformanceTestDAO.ReaderContainsColumn(reader, "workshopid");
				if (flag8)
				{
					int num4 = (reader["workshopid"] == DBNull.Value) ? 0 : ((int)reader["workshopid"]);
					bool flag9 = num4 > 0;
					if (flag9)
					{
						app.WorkshopInfo = new AppointmentWorkshopInfo
						{
							WorkshopId = num4,
							WorkshopTitle = reader["workshoptitle"].ToString()
						};
					}
				}
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00016634 File Offset: 0x00014834
		internal static bool ReaderContainsColumn(IDataReader reader, string colName)
		{
			for (int i = 0; i < reader.FieldCount; i++)
			{
				bool flag = reader.GetName(i).Equals(colName, StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00016674 File Offset: 0x00014874
		public List<Appointment> LoadAppointments(List<int> PersonIds, List<int> AppTypeIds, bool HideCancelled, bool LoadPerStudentDataIcons, bool LoadPerAnonymousDataIcons, DateTime StartDateTime, DateTime EndDateTime)
		{
			DbParameter[] array = new DbParameter[7];
			array[0] = this.DatabaseManager.GetParameter("@sd", DbType.DateTime, (StartDateTime == DateTime.MinValue) ? DBNull.Value : StartDateTime.Date);
			array[1] = this.DatabaseManager.GetParameter("@ed", DbType.DateTime, (EndDateTime == DateTime.MinValue) ? DBNull.Value : EndDateTime.AddDays(1.0).Date);
			array[2] = this.DatabaseManager.GetParameter("@hidecancelled", DbType.Boolean, HideCancelled);
			int num = 3;
			DatabaseLayer databaseManager = this.DatabaseManager;
			string pName = "@apptypeids";
			DbType pType = DbType.String;
			object value;
			if (AppTypeIds != null)
			{
				value = string.Join(",", AppTypeIds.ConvertAll<string>((int at) => at.ToString()).ToArray());
			}
			else
			{
				value = "";
			}
			array[num] = databaseManager.GetParameter(pName, pType, value);
			int num2 = 4;
			DatabaseLayer databaseManager2 = this.DatabaseManager;
			string pName2 = "@pids";
			DbType pType2 = DbType.String;
			object value2;
			if (PersonIds != null)
			{
				value2 = string.Join(",", PersonIds.ConvertAll<string>((int p) => p.ToString()).ToArray());
			}
			else
			{
				value2 = "";
			}
			array[num2] = databaseManager2.GetParameter(pName2, pType2, value2);
			array[5] = this.DatabaseManager.GetParameter("@checkpsicons", DbType.Boolean, LoadPerStudentDataIcons);
			array[6] = this.DatabaseManager.GetParameter("@checkanicons", DbType.Boolean, LoadPerAnonymousDataIcons);
			DbParameter[] parameters = array;
			List<Appointment> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SET ARITHABORT ON \r\nEXECUTE LoadAppointments @pids,@apptypeids,@sd,@ed,@checkpsicons,@checkanicons,@hidecancelled", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = PerformanceTestDAO.GetAppointmentsFromReader(dataReader, this.OpContext).ToList<Appointment>();
				}
			}
			return result;
		}
	}
}
