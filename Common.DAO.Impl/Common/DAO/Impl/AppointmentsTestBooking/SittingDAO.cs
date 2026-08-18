using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.AppointmentsTestBooking
{
	// Token: 0x02000151 RID: 337
	public class SittingDAO : ISittingDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060009D4 RID: 2516 RVA: 0x00066877 File Offset: 0x00064A77
		public SittingDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x000668A7 File Offset: 0x00064AA7
		// (set) Token: 0x060009D6 RID: 2518 RVA: 0x000668AF File Offset: 0x00064AAF
		public OperationContext OpContext { get; set; }

		// Token: 0x060009D7 RID: 2519 RVA: 0x000668B8 File Offset: 0x00064AB8
		private static bool ReaderContainsColumn(IDataReader reader, string colName)
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

		// Token: 0x060009D8 RID: 2520 RVA: 0x000668F8 File Offset: 0x00064AF8
		internal static Sitting GetSittingFromRecord(IDataReader reader, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			Sitting sitting = new Sitting();
			bool flag = SittingDAO.ReaderContainsColumn(reader, "sittingid");
			if (flag)
			{
				object obj = reader["sittingid"];
				bool flag2 = obj != DBNull.Value;
				if (flag2)
				{
					sitting.SittingId = (int)obj;
					bool flag3 = SittingDAO.ReaderContainsColumn(reader, "sittingtitle");
					if (flag3)
					{
						object obj2 = reader["sittingtitle"];
						object obj3 = reader["sittingexamdate"];
						object obj4 = reader["sittingdatecreated"];
						object obj5 = reader["sittingwhocreated"];
						object obj6 = reader["sittinginvigilatorpid"];
						object obj7 = reader["sittinginvigilatorconfirmed"];
						object obj8 = reader["sittingrateofpay"];
						object obj9 = reader["sittingroompid"];
						object obj10 = reader["sittingroomname"];
						object obj11 = reader["sittinglocation"];
						object obj12 = reader["sittingprivatenotes"];
						object obj13 = reader["sittinginvigilatornotes"];
						object obj14 = reader["sittingscheduledstarttime"];
						object obj15 = reader["sittingscheduledendtime"];
						object obj16 = reader["sittingactualtimein"];
						object obj17 = reader["sittingactualtimeout"];
						object obj18 = reader["sittingcancelled"];
						object obj19 = reader["sittingpaydate"];
						object obj20 = reader["sittingisprivate"];
						bool flag4 = obj2 != DBNull.Value;
						if (flag4)
						{
							sitting.Title = (string)obj2;
						}
						bool flag5 = obj3 != DBNull.Value;
						if (flag5)
						{
							sitting.ExamDate = (DateTime)obj3;
						}
						bool flag6 = obj4 != DBNull.Value;
						if (flag6)
						{
							sitting.DateCreated = (DateTime)obj4;
						}
						bool flag7 = obj5 != DBNull.Value;
						if (flag7)
						{
							int num = (int)obj5;
							bool flag8 = num > 0;
							if (flag8)
							{
								sitting.WhoCreated = new PersonBase
								{
									PersonId = num
								};
							}
						}
						bool flag9 = obj6 != DBNull.Value;
						if (flag9)
						{
							bool flag10 = SittingDAO.ReaderContainsColumn(reader, "sittinginvigilatorfirstname");
							string firstName;
							if (flag10)
							{
								object obj21 = reader["sittinginvigilatorfirstname"];
								object obj22 = reader["sittinginvigilatorlastname"];
								firstName = ((obj21 == DBNull.Value) ? "" : databaseLayer.Encryption.Decrypt((byte[])obj21));
								string text = (obj22 == DBNull.Value) ? "" : databaseLayer.Encryption.Decrypt((byte[])obj22);
							}
							else
							{
								firstName = "";
							}
							string lastName = "";
							sitting.Invigilator = new PersonBase
							{
								PersonId = (int)obj6,
								FirstName = firstName,
								LastName = lastName
							};
						}
						bool flag11 = obj7 != DBNull.Value;
						if (flag11)
						{
							sitting.InvigilatorConfirmed = (int)obj7;
						}
						bool flag12 = obj8 != DBNull.Value;
						if (flag12)
						{
							sitting.RateOfPay = Convert.ToDouble((int)obj8) / 100.0;
						}
						bool flag13 = obj9 != DBNull.Value;
						if (flag13)
						{
							sitting.Room = new AppointmentRoom
							{
								RoomId = (int)obj9,
								RoomTitle = ((obj10 != DBNull.Value) ? databaseLayer.Encryption.Decrypt((byte[])obj10) : "")
							};
						}
						bool flag14 = obj11 != DBNull.Value;
						if (flag14)
						{
							sitting.Location = (string)obj11;
						}
						bool flag15 = obj12 != DBNull.Value;
						if (flag15)
						{
							sitting.PrivateNotes = (string)obj12;
						}
						bool flag16 = obj13 != DBNull.Value;
						if (flag16)
						{
							sitting.InvigilatorNotes = (string)obj13;
						}
						bool flag17 = obj14 != DBNull.Value;
						if (flag17)
						{
							sitting.ScheduledStartDateTime = new DateTime?((DateTime)obj14);
						}
						bool flag18 = obj15 != DBNull.Value;
						if (flag18)
						{
							sitting.ScheduledEndDateTime = new DateTime?((DateTime)obj15);
						}
						bool flag19 = obj16 != DBNull.Value;
						if (flag19)
						{
							sitting.ActualTimeIn = new DateTime?((DateTime)obj16);
						}
						bool flag20 = obj17 != DBNull.Value;
						if (flag20)
						{
							sitting.ActualTimeOut = new DateTime?((DateTime)obj17);
						}
						bool flag21 = obj18 != DBNull.Value;
						if (flag21)
						{
							sitting.Cancelled = (bool)obj18;
						}
						bool flag22 = obj19 != DBNull.Value;
						if (flag22)
						{
							sitting.PayDate = new DateTime?((DateTime)obj19);
						}
						bool flag23 = obj20 != DBNull.Value;
						if (flag23)
						{
							sitting.IsPrivate = (bool)obj20;
						}
						bool flag24 = SittingDAO.ReaderContainsColumn(reader, "minstartdate");
						if (flag24)
						{
							bool flag25 = reader["minstartdate"] != DBNull.Value;
							if (flag25)
							{
								sitting.VirtualMinStartDateTimeFromBookings = new DateTime?((DateTime)reader["minstartdate"]);
							}
							bool flag26 = reader["maxenddate"] != DBNull.Value;
							if (flag26)
							{
								sitting.VirtualMaxEndDateTimeFromBookings = new DateTime?((DateTime)reader["maxenddate"]);
							}
						}
					}
				}
			}
			return sitting;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00066E5C File Offset: 0x0006505C
		private DbParameter[] GetSittingParameters(Sitting OldSitting, Sitting NewSitting)
		{
			bool flag = NewSitting.ActualTimeIn != null && NewSitting.ActualTimeOut != null;
			DbParameter parameter;
			DbParameter parameter2;
			if (flag)
			{
				parameter = this.DatabaseManager.GetParameter("@actualtimein", DbType.DateTime, NewSitting.ActualTimeIn);
				parameter2 = this.DatabaseManager.GetParameter("@actualtimeout", DbType.DateTime, NewSitting.ActualTimeOut);
			}
			else
			{
				parameter = this.DatabaseManager.GetParameter("@actualtimein", DbType.DateTime, DBNull.Value);
				parameter2 = this.DatabaseManager.GetParameter("@actualtimeout", DbType.DateTime, DBNull.Value);
			}
			bool flag2 = NewSitting.ScheduledStartDateTime != null && NewSitting.ScheduledEndDateTime != null;
			DbParameter parameter3;
			DbParameter parameter4;
			if (flag2)
			{
				parameter3 = this.DatabaseManager.GetParameter("@scheduledstarttime", DbType.DateTime, NewSitting.ScheduledStartDateTime.Value);
				parameter4 = this.DatabaseManager.GetParameter("@scheduledendtime", DbType.DateTime, NewSitting.ScheduledEndDateTime.Value);
			}
			else
			{
				parameter3 = this.DatabaseManager.GetParameter("@scheduledstarttime", DbType.DateTime, DBNull.Value);
				parameter4 = this.DatabaseManager.GetParameter("@scheduledendtime", DbType.DateTime, DBNull.Value);
			}
			bool flag3 = NewSitting.PayDate != null;
			DbParameter parameter5;
			if (flag3)
			{
				parameter5 = this.DatabaseManager.GetParameter("@paydate", DbType.DateTime, NewSitting.PayDate.Value);
			}
			else
			{
				parameter5 = this.DatabaseManager.GetParameter("@paydate", DbType.DateTime, DBNull.Value);
			}
			bool flag4 = NewSitting.Invigilator != null && NewSitting.Invigilator.PersonId > 0;
			DbParameter parameter6;
			if (flag4)
			{
				parameter6 = this.DatabaseManager.GetParameter("@invigilatorpid", DbType.Int32, NewSitting.Invigilator.PersonId);
			}
			else
			{
				parameter6 = this.DatabaseManager.GetParameter("@invigilatorpid", DbType.Int32, DBNull.Value);
			}
			bool flag5 = NewSitting.Room != null && NewSitting.Room.RoomId > 0;
			DbParameter parameter7;
			if (flag5)
			{
				parameter7 = this.DatabaseManager.GetParameter("@roompid", DbType.Int32, NewSitting.Room.RoomId);
			}
			else
			{
				parameter7 = this.DatabaseManager.GetParameter("@roompid", DbType.Int32, DBNull.Value);
			}
			return new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@title", DbType.String, NewSitting.Title),
				this.DatabaseManager.GetParameter("@rateofpay", DbType.Int32, (int)(NewSitting.RateOfPay * 100.0)),
				this.DatabaseManager.GetParameter("@location", DbType.String, NewSitting.Location),
				this.DatabaseManager.GetParameter("@cancelled", DbType.Boolean, NewSitting.Cancelled),
				this.DatabaseManager.GetParameter("@examdate", DbType.DateTime, NewSitting.ExamDate),
				this.DatabaseManager.GetParameter("@whocreated", DbType.Int32, (NewSitting.WhoCreated == null) ? -1 : NewSitting.WhoCreated.PersonId),
				this.DatabaseManager.GetParameter("@invigilatorconfirmed", DbType.Boolean, NewSitting.InvigilatorConfirmed),
				this.DatabaseManager.GetParameter("@privatenotes", DbType.String, (NewSitting.PrivateNotes == null) ? "" : NewSitting.PrivateNotes),
				this.DatabaseManager.GetParameter("@invigilatornotes", DbType.String, (NewSitting.InvigilatorNotes == null) ? "" : NewSitting.InvigilatorNotes),
				this.DatabaseManager.GetParameter("@isprivate", DbType.Boolean, NewSitting.IsPrivate),
				parameter,
				parameter2,
				parameter3,
				parameter4,
				parameter5,
				parameter6,
				parameter7
			};
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00067268 File Offset: 0x00065468
		private void CreateSitting(Sitting Sitting)
		{
			Sitting.WhoCreated = new PersonBase
			{
				PersonId = this.OpContext.WhoAmI
			};
			Sitting.DateCreated = DateTime.Now;
			DbParameter[] sittingParameters = this.GetSittingParameters(null, Sitting);
			DataTable dataTable = this.DatabaseManager.ExecuteQuery("INSERT INTO examsitting (title,examdate,datecreated,whocreated,invigilatorpid\r\n    ,invigilatorconfirmed,rateofpay,roompid,location,privatenotes,invigilatornotes\r\n    ,scheduledstarttime,scheduledendtime,actualtimein,actualtimeout,cancelled\r\n    ,paydate,isprivate)\r\nVALUES (@title,@examdate,getdate(),@whocreated,@invigilatorpid\r\n    ,@invigilatorconfirmed,@rateofpay,@roompid,@location,@privatenotes,@invigilatornotes\r\n    ,@scheduledstarttime,@scheduledendtime,@actualtimein,@actualtimeout,@cancelled\r\n    ,@paydate,@isprivate);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS sittingid", sittingParameters);
			bool flag = dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value;
			if (flag)
			{
				Sitting.SittingId = (int)dataTable.Rows[0][0];
			}
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0006730C File Offset: 0x0006550C
		private void UpdateSitting(Sitting OldSitting, Sitting NewSitting)
		{
			bool flag = OldSitting == null;
			if (flag)
			{
				OldSitting = this.LoadSittingById(NewSitting.Id);
			}
			DbParameter[] sittingParameters = this.GetSittingParameters(OldSitting, NewSitting);
			DbParameter[] array = new DbParameter[sittingParameters.Length + 1];
			array[0] = this.DatabaseManager.GetParameter("@sittingid", DbType.Int32, NewSitting.SittingId);
			for (int i = 1; i < array.Length; i++)
			{
				array[i] = sittingParameters[i - 1];
			}
			this.DatabaseManager.ExecuteNonQuery("UPDATE examsitting SET title=@title,examdate=@examdate,invigilatorpid=@invigilatorpid\r\n    ,invigilatorconfirmed=@invigilatorconfirmed,rateofpay=@rateofpay,roompid=@roompid\r\n    ,location=@location,privatenotes=@privatenotes,invigilatornotes=@invigilatornotes\r\n    ,scheduledstarttime=@scheduledstarttime,scheduledendtime=@scheduledendtime\r\n    ,actualtimein=@actualtimein,actualtimeout=@actualtimeout,cancelled=@cancelled,paydate=@paydate\r\n    ,isprivate=@isprivate\r\nWHERE sittingid=@sittingid", array);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00003998 File Offset: 0x00001B98
		public void DeleteSitting(int SittingId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00003998 File Offset: 0x00001B98
		public void SaveSitting(Sitting OldSitting, Sitting NewSitting)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00003998 File Offset: 0x00001B98
		public List<Test> LoadSittingTests(int SittingId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00067394 File Offset: 0x00065594
		public IList<Sitting> LoadSittings(DateTime StartDate, DateTime EndDate)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate.Date),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate.Date.AddDays(1.0).AddMinutes(-1.0))
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT es.sittingid,es.title AS sittingtitle,es.examdate AS sittingexamdate,\r\n es.datecreated AS sittingdatecreated,es.whocreated AS sittingwhocreated,\r\n es.invigilatorpid AS sittinginvigilatorpid,\r\n es.invigilatorconfirmed AS sittinginvigilatorconfirmed,es.rateofpay AS sittingrateofpay,\r\n es.roompid AS sittingroompid,es.location AS sittinglocation,\r\n es.privatenotes AS sittingprivatenotes,es.invigilatornotes AS sittinginvigilatornotes,\r\n es.scheduledstarttime AS sittingscheduledstarttime,es.scheduledendtime AS sittingscheduledendtime,\r\n es.actualtimein AS sittingactualtimein,es.actualtimeout AS sittingactualtimeout,\r\n es.cancelled AS sittingcancelled,es.paydate AS sittingpaydate,es.isprivate AS sittingisprivate,\r\n sittingroom.firstname AS sittingroomname,\r\n MIN(a.startdate) AS minstartdate,MAX(a.enddate) AS maxenddate\r\nFROM    examsitting es LEFT JOIN people ip ON ip.personid=es.invigilatorpid\r\n        LEFT JOIN people sittingroom ON sittingroom.personid=es.roompid\r\n\t\tLEFT JOIN appointments a ON a.sittingid=es.sittingid\r\nWHERE   es.examdate>=@startdate AND es.examdate<@enddate\r\nGROUP BY es.sittingid,es.title,es.examdate,\r\nes.datecreated,es.whocreated,es.invigilatorpid,es.invigilatorconfirmed,es.rateofpay,\r\nes.roompid,es.location,\r\nes.privatenotes,es.invigilatornotes,\r\nes.scheduledstarttime,es.scheduledendtime,\r\nes.actualtimein,es.actualtimeout,\r\nes.cancelled,es.paydate,es.isprivate,\r\nsittingroom.firstname\r\nORDER BY es.examdate", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<Sitting> list = new List<Sitting>();
					while (dataReader.Read())
					{
						Sitting sittingFromRecord = SittingDAO.GetSittingFromRecord(dataReader, this.OpContext);
						bool flag2 = sittingFromRecord != null;
						if (flag2)
						{
							list.Add(sittingFromRecord);
						}
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0006748C File Offset: 0x0006568C
		public Sitting LoadSittingById(int SittingId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@sittingid", DbType.Int32, SittingId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT es.sittingid,es.title AS sittingtitle,es.examdate AS sittingexamdate,\r\n es.datecreated AS sittingdatecreated,es.whocreated AS sittingwhocreated,\r\n es.invigilatorpid AS sittinginvigilatorpid,\r\n es.invigilatorconfirmed AS sittinginvigilatorconfirmed,es.rateofpay AS sittingrateofpay,\r\n es.roompid AS sittingroompid,es.location AS sittinglocation,\r\n es.privatenotes AS sittingprivatenotes,es.invigilatornotes AS sittinginvigilatornotes,\r\n es.scheduledstarttime AS sittingscheduledstarttime,es.scheduledendtime AS sittingscheduledendtime,\r\n es.actualtimein AS sittingactualtimein,es.actualtimeout AS sittingactualtimeout,\r\n es.cancelled AS sittingcancelled,es.paydate AS sittingpaydate,es.isprivate AS sittingisprivate,\r\n sittingroom.firstname AS sittingroomname,\r\n MIN(a.startdate) AS minstartdate,MAX(a.enddate) AS maxenddate\r\nFROM    examsitting es LEFT JOIN people ip ON ip.personid=es.invigilatorpid\r\n        LEFT JOIN people sittingroom ON sittingroom.personid=es.roompid\r\n\t\tLEFT JOIN appointments a ON a.sittingid=es.sittingid\r\nWHERE   es.sittingid=@sittingid\r\nGROUP BY es.sittingid,es.title,es.examdate,\r\nes.datecreated,es.whocreated,es.invigilatorpid,es.invigilatorconfirmed,es.rateofpay,\r\nes.roompid,es.location,\r\nes.privatenotes,es.invigilatornotes,\r\nes.scheduledstarttime,es.scheduledendtime,\r\nes.actualtimein,es.actualtimeout,\r\nes.cancelled,es.paydate,es.isprivate,\r\nsittingroom.firstname\r\nORDER BY es.examdate", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<Sitting> list = new List<Sitting>();
					while (dataReader.Read())
					{
						Sitting sittingFromRecord = SittingDAO.GetSittingFromRecord(dataReader, this.OpContext);
						bool flag2 = sittingFromRecord != null;
						if (flag2)
						{
							return sittingFromRecord;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00067534 File Offset: 0x00065734
		public void ClearSittingOnAppointment(int AppointmentId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appid", DbType.Int32, AppointmentId),
				databaseLayer.GetParameter("@sittingid", DbType.Int32, DBNull.Value)
			};
			databaseLayer.ExecuteNonQuery("UPDATE appointments SET sittingid=@sittingid WHERE appointmentid=@appid", parameters);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0006759C File Offset: 0x0006579C
		public void SetSittingOnAppointment(int AppointmentId, int SittingId)
		{
			bool flag = SittingId < 1;
			if (flag)
			{
				this.ClearSittingOnAppointment(AppointmentId);
			}
			else
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@appid", DbType.Int32, AppointmentId),
					databaseLayer.GetParameter("@sittingid", DbType.Int32, SittingId)
				};
				databaseLayer.ExecuteNonQuery("UPDATE appointments SET sittingid=@sittingid WHERE appointmentid=@appid", parameters);
			}
		}

		// Token: 0x040005D3 RID: 1491
		private DatabaseLayer DatabaseManager;
	}
}
