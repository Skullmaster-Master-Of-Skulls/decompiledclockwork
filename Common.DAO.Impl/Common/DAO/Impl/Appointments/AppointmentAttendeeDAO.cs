using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.DataStructure.Adapters;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.Appointments
{
	// Token: 0x02000124 RID: 292
	public class AppointmentAttendeeDAO : IAppointmentAttendeeDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x00054C43 File Offset: 0x00052E43
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x00054C4B File Offset: 0x00052E4B
		public OperationContext OpContext { get; set; }

		// Token: 0x06000845 RID: 2117 RVA: 0x00054C54 File Offset: 0x00052E54
		public AppointmentAttendeeDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00054C84 File Offset: 0x00052E84
		internal static T GetAttendeeFromRecord<T>(IDataReader record, OperationContext opContext, string prefix = "", IBatchDecryptor batchDecryptor = null) where T : Attendee
		{
			string name = prefix + "attendeeid";
			bool flag = record == null || record[name] is DBNull;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				string name2 = prefix + "noshow";
				T t = (T)((object)Activator.CreateInstance(typeof(T)));
				t.AttendeeId = (int)record[name];
				t.IsNoShow = (!(record[name2] is DBNull) && (bool)record[name2]);
				t.MiscCode = (int)record[prefix + "misccode"];
				t.Person = PeopleDAO.GetPersonFromReader(prefix, record, opContext, batchDecryptor);
				bool flag2 = t.Person == null || t.Person.PersonId < 1;
				if (flag2)
				{
					result = default(T);
				}
				else
				{
					result = t;
				}
			}
			return result;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00054DA0 File Offset: 0x00052FA0
		internal static Attendee GetAttendeeFromRecord(IDataReader record, OperationContext opContext, string prefix = "", IBatchDecryptor batchDecryptor = null)
		{
			return AppointmentAttendeeDAO.GetAttendeeFromRecord<Attendee>(record, opContext, prefix, batchDecryptor);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00054DBC File Offset: 0x00052FBC
		public IDictionary<int, IList<Attendee>> LoadAttendeesByAppointmentIds(IList<int> appointmentIds)
		{
			IList<Chunk> list = appointmentIds.Distinct<int>().ToList<int>().BreakdownItemsIntoChunks(2000);
			Dictionary<int, IList<Attendee>> dictionary = new Dictionary<int, IList<Attendee>>();
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			List<int> items = appointmentIds.ToList<int>();
			IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
			foreach (Chunk chunk in list)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@appids", DbType.String, string.Join<int>(",", chunk.GetChunkRange(items)))
				};
				using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT orderid AS appointmentid INTO #tappids FROM splitorderids(@appids,',')\r\n\r\nSELECT  att.attendeeid,att.appointmentid,att.personid,att.noshow,att.misccode,p.firstname,p.lastname,p.middlename,p.student_no,pg.groupid\r\nFROM    attendees att LEFT JOIN people p ON p.personid=att.personid\r\n        LEFT JOIN peoplegroups pg ON pg.personid=att.personid\r\nWHERE   att.appointmentid IN (SELECT appointmentid FROM #tappids)\r\nORDER BY att.appointmentid,att.attendeeid,att.PersonID,pg.GroupID\r\n\r\nDROP TABLE #tappids", parameters))
				{
					bool flag = dataReader == null;
					if (flag)
					{
						return dictionary;
					}
					List<AppointmentAttendeeDAO.AttendeeWithAppId> list2 = new List<AppointmentAttendeeDAO.AttendeeWithAppId>();
					while (dataReader.Read())
					{
						list2.Add(new AppointmentAttendeeDAO.AttendeeWithAppId
						{
							AttendeeId = dataReader.GetIntFromRecord("attendeeid", 0),
							AppointmentId = dataReader.GetIntFromRecord("appointmentid", 0),
							GroupId = dataReader.GetIntFromRecord("groupid", 0),
							PersonId = dataReader.GetIntFromRecord("personid", 0),
							FirstName = dataReader.GetEncryptedStringFromRecord(batchDecryptor, "firstname"),
							MiddleName = dataReader.GetEncryptedStringFromRecord(batchDecryptor, "middlename"),
							LastName = dataReader.GetEncryptedStringFromRecord(batchDecryptor, "lastname"),
							StudentNumber = dataReader.GetEncryptedStringFromRecord(batchDecryptor, "student_no"),
							MiscCode = dataReader.GetIntFromRecord("misccode", 0),
							NoShow = dataReader.GetBoolFromRecord("noshow", false)
						});
					}
					int num = 0;
					int num2 = 0;
					List<Attendee> list3 = new List<Attendee>();
					Attendee attendee = null;
					foreach (AppointmentAttendeeDAO.AttendeeWithAppId attendeeWithAppId in list2)
					{
						int appointmentId = attendeeWithAppId.AppointmentId;
						bool flag2 = num != appointmentId;
						if (flag2)
						{
							num = appointmentId;
							num2 = 0;
							attendee = null;
							list3 = new List<Attendee>();
							dictionary.Add(num, list3);
						}
						int personId = attendeeWithAppId.PersonId;
						bool flag3 = personId != num2;
						if (flag3)
						{
							num2 = personId;
							attendee = new Attendee
							{
								AttendeeId = attendeeWithAppId.AttendeeId,
								IsNoShow = attendeeWithAppId.NoShow,
								MiscCode = attendeeWithAppId.MiscCode,
								Person = new PersonBase
								{
									PersonId = personId,
									FirstName = attendeeWithAppId.FirstName,
									MiddleName = attendeeWithAppId.MiddleName,
									LastName = attendeeWithAppId.LastName,
									Student_no = attendeeWithAppId.StudentNumber,
									Groups = new List<Group>(),
									CoreGroup = eCoreGroup.Unknown
								}
							};
							list3.Add(attendee);
						}
						int gid = attendeeWithAppId.GroupId;
						bool flag4 = gid < 1;
						if (!flag4)
						{
							bool flag5 = attendee.Person.Groups.All((Group g) => g.GroupId != gid);
							if (flag5)
							{
								attendee.Person.Groups.Add(new Group
								{
									GroupId = gid
								});
								bool flag6 = gid == 1;
								if (flag6)
								{
									attendee.Person.CoreGroup = eCoreGroup.Students;
								}
								else
								{
									bool flag7 = attendee.Person.CoreGroup == eCoreGroup.Unknown && gid > 0 && Enum.IsDefined(typeof(eCoreGroup), gid);
									if (flag7)
									{
										attendee.Person.CoreGroup = (eCoreGroup)gid;
									}
								}
							}
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0005520C File Offset: 0x0005340C
		public IList<Attendee> LoadAttendeesByAppointmentId(int appointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, appointmentId)
			};
			IList<Attendee> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT att.attendeeid,att.appointmentid,att.personid,att.noshow,att.misccode,p.firstname,p.lastname,p.middlename,p.student_no,MIN(pg.groupid) AS groupid\r\nFROM attendees att LEFT JOIN people p ON p.personid=att.personid\r\n    LEFT JOIN peoplegroups pg ON pg.personid=att.personid\r\nWHERE att.appointmentid=@appid\r\nGROUP BY att.attendeeid,att.appointmentid,att.personid,att.noshow,att.misccode,p.firstname,p.lastname,p.middlename,p.student_no", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<Attendee> list = new List<Attendee>();
					while (dataReader.Read())
					{
						Attendee attendeeFromRecord = AppointmentAttendeeDAO.GetAttendeeFromRecord(dataReader, this.OpContext, "", null);
						bool flag2 = attendeeFromRecord != null;
						if (flag2)
						{
							list.Add(attendeeFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000552BC File Offset: 0x000534BC
		public Attendee LoadAttendeeById(int appointmentId, int personId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, appointmentId),
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, personId)
			};
			Attendee result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT att.attendeeid,att.appointmentid,att.personid,att.noshow,att.misccode,p.firstname,p.lastname,p.middlename,p.student_no,MIN(pg.groupid) AS groupid\r\nFROM attendees att LEFT JOIN people p ON p.personid=att.personid\r\n    LEFT JOIN peoplegroups pg ON pg.personid=att.personid\r\nWHERE att.appointmentid=@appid AND att.personid=@pid\r\nGROUP BY att.attendeeid,att.appointmentid,att.personid,att.noshow,att.misccode,p.firstname,p.lastname,p.middlename,p.student_no", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = AppointmentAttendeeDAO.GetAttendeeFromRecord(dataReader, this.OpContext, "", null);
				}
			}
			return result;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00055364 File Offset: 0x00053564
		public Attendee LoadAttendeeById(int attendeeId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@attendeeid", DbType.Int32, attendeeId)
			};
			Attendee result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT att.attendeeid,att.appointmentid,att.personid,att.noshow,att.misccode,p.firstname,p.lastname,p.middlename,p.student_no,MIN(pg.groupid) AS groupid\r\nFROM attendees att LEFT JOIN people p ON p.personid=att.personid\r\n    LEFT JOIN peoplegroups pg ON pg.personid=att.personid\r\nWHERE att.appointmentid=@appid AND att.personid=@pid\r\nGROUP BY att.attendeeid,att.appointmentid,att.personid,att.noshow,att.misccode,p.firstname,p.lastname,p.middlename,p.student_no", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = AppointmentAttendeeDAO.GetAttendeeFromRecord(dataReader, this.OpContext, "", null);
				}
			}
			return result;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x000553F0 File Offset: 0x000535F0
		public void InsertOrUpdateAppointmentAttendees(int appointmentId, IList<Attendee> attendees, DbTransaction transaction = null)
		{
			foreach (Attendee attendee in attendees)
			{
				this.InsertOrUpdateAppointmentAttendee(appointmentId, attendee, transaction);
			}
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00055440 File Offset: 0x00053640
		public int InsertOrUpdateAppointmentAttendee(int appointmentId, Attendee attendee, DbTransaction transaction = null)
		{
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("@attendeeid", DbType.Int32, 0),
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, appointmentId),
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, attendee.Person.PersonId),
				this.DatabaseManager.GetParameter("@noshow", DbType.Boolean, attendee.IsNoShow),
				this.DatabaseManager.GetParameter("@misccode", DbType.Int32, attendee.MiscCode)
			};
			this.DatabaseManager.ExecuteNonQuery("IF NOT EXISTS(SELECT attendeeid FROM attendees WHERE AppointmentID=@appid AND PersonID=@pid)\r\n\tINSERT INTO attendees(AppointmentID,PersonID,noShow,miscCode) VALUES (@appid,@pid,@noshow,@misccode)\r\nELSE\r\n\tUPDATE attendees SET noShow=@noshow,miscCode=@misccode WHERE AppointmentID=@appid AND PersonID=@pid\r\nSET @attendeeid=(SELECT TOP 1 attendeeid FROM attendees WHERE AppointmentID=@appid AND PersonID=@pid);", array);
			return attendee.AttendeeId = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0005552C File Offset: 0x0005372C
		public void DeleteAttendee(int appointmentId, int personId, DbTransaction transaction = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, appointmentId),
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, personId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM attendees WHERE AppointmentID=@appid AND PersonID=@pid", parameters);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0005558C File Offset: 0x0005378C
		public int DeleteAttendee(int attendeeId, DbTransaction transaction = null)
		{
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("@appid", DbType.Int32, 0),
				this.DatabaseManager.GetParameter("@attendeeid", DbType.Int32, attendeeId)
			};
			this.DatabaseManager.ExecuteNonQuery("SET @appid=(SELECT appointmentid FROM attendees WHERE attendeeid=@attendeeid); DELETE FROM attendees WHERE AttendeeID=@attendeeid", array);
			return (array[0].Value is DBNull) ? 0 : ((int)array[0].Value);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00055608 File Offset: 0x00053808
		public void RemoveAttendeesNotInList(int appointmentId, IList<int> personIds, DbTransaction transaction = null)
		{
			DbParameter[] array = new DbParameter[2];
			array[0] = this.DatabaseManager.GetParameter("@appid", DbType.Int32, appointmentId);
			int num = 1;
			DatabaseLayer databaseManager = this.DatabaseManager;
			string pName = "@pids";
			DbType pType = DbType.String;
			object value;
			if (personIds != null)
			{
				value = string.Join(",", personIds.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray());
			}
			else
			{
				value = "";
			}
			array[num] = databaseManager.GetParameter(pName, pType, value);
			DbParameter[] parameters = array;
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM attendees \r\n\tWHERE AppointmentID=@appid \r\n\t\tAND NOT PersonID IN (SELECT orderid AS PersonID FROM SplitOrderIDs(@pids,',')) \r\n\t\tAND NOT PersonID IN (SELECT PersonID FROM PeopleGroups WHERE GroupID=3)", parameters);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000556A4 File Offset: 0x000538A4
		public void UpdateNoShowValue(int appointmentId, int personId, bool noShowValue, DbTransaction transaction = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, appointmentId),
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, personId),
				this.DatabaseManager.GetParameter("@noshow", DbType.Boolean, noShowValue)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE attendees SET noShow=@noshow WHERE AppointmentID=@appid AND PersonID=@pid", parameters);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0005571C File Offset: 0x0005391C
		public int UpdateNoShowValue(int attendeeId, bool noShowValue, DbTransaction transaction = null)
		{
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("@appid", DbType.Int32, 0),
				this.DatabaseManager.GetParameter("@attendeeid", DbType.Int32, attendeeId),
				this.DatabaseManager.GetParameter("@noshow", DbType.Boolean, noShowValue)
			};
			this.DatabaseManager.ExecuteNonQuery("SET @appid=(SELECT appointmentid FROM attendees WHERE attendeeid=@attendeeid); UPDATE attendees SET noShow=@noshow WHERE AttendeeID=@attendeeid", array);
			return (array[0].Value is DBNull) ? 0 : ((int)array[0].Value);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000557B4 File Offset: 0x000539B4
		public void UpdateMiscCodeValue(int appointmentId, int personId, int misccodeValue, DbTransaction transaction = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, appointmentId),
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, personId),
				this.DatabaseManager.GetParameter("@misccode", DbType.Int32, misccodeValue)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE attendees SET miscCode=@misccode WHERE AppointmentID=@appid AND PersonID=@pid", parameters);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0005582C File Offset: 0x00053A2C
		public int UpdateMiscCodeValue(int attendeeId, int misccodeValue, DbTransaction transaction = null)
		{
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("@appid", DbType.Int32, 0),
				this.DatabaseManager.GetParameter("@attendeeid", DbType.Int32, attendeeId),
				this.DatabaseManager.GetParameter("@misccode", DbType.Int32, misccodeValue)
			};
			this.DatabaseManager.ExecuteNonQuery("SET @appid=(SELECT appointmentid FROM attendees WHERE attendeeid=@attendeeid); UPDATE attendees SET miscCode=@misccode WHERE AttendeeID=@attendeeid", array);
			return (array[0].Value is DBNull) ? 0 : ((int)array[0].Value);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x000558C4 File Offset: 0x00053AC4
		public void SwapAttendee(int AppointmentId, int OldPersonId, int NewPersonId, DbTransaction transaction = null)
		{
			bool flag = NewPersonId < 1;
			if (flag)
			{
				throw new Exception("SwapAttendee:NewPersonId can't be null");
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pidold", DbType.Int32, OldPersonId),
				this.DatabaseManager.GetParameter("@pidnew", DbType.Int32, NewPersonId),
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE attendees SET personid=@pidnew WHERE appointmentid=@appid AND personid=@pidold", parameters);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00055950 File Offset: 0x00053B50
		public IList<AttendeeWithAppointmentId> LoadAttendeesWhoHaveNoShowedInThePast(DateTime minimumDateToCheckFrom, int SkipAppointmentsWithThisIconId = -1, int[] AppTypeIds = null)
		{
			DbParameter[] array = new DbParameter[3];
			array[0] = this.DatabaseManager.GetParameter("@mindate", DbType.DateTime, minimumDateToCheckFrom);
			int num = 1;
			DatabaseLayer databaseManager = this.DatabaseManager;
			string pName = "@apptypeids";
			DbType pType = DbType.String;
			object value;
			if (AppTypeIds != null)
			{
				value = string.Join(",", AppTypeIds.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray());
			}
			else
			{
				value = "";
			}
			array[num] = databaseManager.GetParameter(pName, pType, value);
			array[2] = this.DatabaseManager.GetParameter("@iconid", DbType.Int32, SkipAppointmentsWithThisIconId);
			DbParameter[] parameters = array;
			IList<AttendeeWithAppointmentId> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT att.attendeeid,att.appointmentid,att.personid,att.noshow,att.misccode,p.firstname,p.lastname,p.middlename,p.student_no,MIN(pg.groupid) AS groupid\r\nFROM apps a LEFT JOIN attendees att ON att.appointmentid=a.appointmentid\r\n        LEFT JOIN people p ON p.personid=att.personid\r\n    LEFT JOIN peoplegroups pg ON pg.personid=att.personid\r\nWHERE a.startdate>=@mindate AND att.noshow=1 AND a.cancelled=0 AND NOT a.appointmentid IN (SELECT appointmentid FROM appointmenticons WHERE iconnum=@iconid)\r\n    AND (@apptypeids='' OR a.apptypeid IN (SELECT orderid AS apptypeid FROM splitorderids(@apptypeids,',')))\r\nGROUP BY att.attendeeid,att.appointmentid,att.personid,att.noshow,att.misccode,p.firstname,p.lastname,p.middlename,p.student_no\r\nORDER BY personid,appointmentid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<AttendeeWithAppointmentId> list = new List<AttendeeWithAppointmentId>();
					while (dataReader.Read())
					{
						AttendeeWithAppointmentId attendeeFromRecord = AppointmentAttendeeDAO.GetAttendeeFromRecord<AttendeeWithAppointmentId>(dataReader, this.OpContext, "", null);
						bool flag2 = attendeeFromRecord != null;
						if (flag2)
						{
							attendeeFromRecord.AppointmentId = ((dataReader["appointmentid"] is DBNull) ? 0 : ((int)dataReader["appointmentid"]));
							list.Add(attendeeFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00055AA4 File Offset: 0x00053CA4
		public IList<int> GetDoubleBookedAttendees(IList<int> PersonIdsToCheck, DateTime StartDateTime, DateTime EndDateTime, int AppointmentIdToSkip)
		{
			DbParameter[] array = new DbParameter[4];
			array[0] = this.DatabaseManager.GetParameter("@pids", DbType.String, string.Join(",", PersonIdsToCheck.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray()));
			array[1] = this.DatabaseManager.GetParameter("@sd", DbType.DateTime, StartDateTime);
			array[2] = this.DatabaseManager.GetParameter("@ed", DbType.DateTime, EndDateTime);
			array[3] = this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentIdToSkip);
			DbParameter[] parameters = array;
			IList<int> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT orderid AS personid INTO #temp1 FROM splitorderids(@pids,',');\r\n\r\nSELECT DISTINCT(t1.personid) AS personid\r\nFROM #temp1 t1 LEFT JOIN attendees att ON att.personid=t1.personid\r\n    LEFT JOIN appointments a ON a.appointmentid=att.appointmentid\r\nWHERE   NOT ( ( a.enddate<@sd ) OR (a.startdate>@ed ) )\r\n        AND NOT a.appointmentid IS NULL\r\n        AND a.cancelled=0\r\n        AND NOT att.appointmentid=@appid\r\n\r\nDROP TABLE #temp1", parameters))
			{
				List<int> list = new List<int>();
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						int num = (dataReader[0] is DBNull) ? 0 : ((int)dataReader[0]);
						bool flag2 = num > 0;
						if (flag2)
						{
							list.Add(num);
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00055BE0 File Offset: 0x00053DE0
		public int LoadAppointmentIdByAttendee(int AttendeeId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@attendeeid", DbType.Int32, AttendeeId)
			};
			int result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT appointmentid FROM attendees WHERE attendeeid=@attendeeid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = (int)dataReader[0];
				}
			}
			return result;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00055C78 File Offset: 0x00053E78
		public bool CheckIfDoubleBooked(int PersonId, DateTime StartDateTime, DateTime EndDateTime, params int[] AppTypeIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PersonId),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, StartDateTime),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, EndDateTime),
				databaseLayer.GetParameter("@apptypeids", DbType.String, (AppTypeIds == null || AppTypeIds.Length < 1) ? "" : AppTypeIds.ToList<int>().CommaSeparatedValuesWithoutSpace<int>())
			};
			int num = (int)databaseLayer.ExecuteScalar("SELECT orderid AS apptypeid INTO #t1 FROM splitorderids(COALESCE(@apptypeids,''),',')\r\n\r\nSELECT  COUNT(DISTINCT att.appointmentid)\r\nFROM    attendees att LEFT JOIN appointments app ON app.AppointmentID=att.AppointmentID\r\nWHERE\tatt.PersonID=@pid \r\n\t\tAND app.cancelled=0\r\n\t\tAND app.startDate<@enddate AND app.endDate>@startdate\r\n        AND (@apptypeids IS NULL OR @apptypeids='' OR app.AppTypeID IN (SELECT apptypeid FROM #t1))\r\n\r\nDROP TABLE #t1", parameters);
			return num > 0;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00055D30 File Offset: 0x00053F30
		public IList<int> TryToRemoveAttendees(int appointmentId, params int[] attendeeIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appid", DbType.Int32, appointmentId),
				databaseLayer.GetParameter("@personids", DbType.String, attendeeIds.CommaSeparatedValuesWithoutSpace<int>())
			};
			List<int> list = new List<int>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select ExternalId from appointments where AppointmentID = @appid and ExternalId is not null and ExternalId in (select orderid as ExternalId from SplitOrderIDs(@personids, ','))", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						bool flag2 = !(dataReader["ExternalId"] is DBNull);
						if (flag2)
						{
							list.Add((int)dataReader["ExternalId"]);
						}
					}
				}
			}
			return (list.Count > 0) ? list : null;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00055E1C File Offset: 0x0005401C
		public IList<int> TryToRemoveAttendees(IList<int> attendeeIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@attids", DbType.String, attendeeIds.CommaSeparatedValuesWithoutSpace<int>())
			};
			List<int> list = new List<int>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select ExternalId from appointments app\r\ninner join Attendees att on att.AppointmentID = app.AppointmentID\r\nwhere app.ExternalId is not null and att.AttendeeId in (select orderid as AttendeeId from SplitOrderIDs(@attids, ',')) and app.ExternalId = att.PersonId", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						bool flag2 = !(dataReader["ExternalId"] is DBNull);
						if (flag2)
						{
							list.Add((int)dataReader["ExternalId"]);
						}
					}
				}
			}
			return (list.Count > 0) ? list : null;
		}

		// Token: 0x040004E5 RID: 1253
		private DatabaseLayer DatabaseManager;

		// Token: 0x02000296 RID: 662
		internal class AttendeeWithAppId
		{
			// Token: 0x17000149 RID: 329
			// (get) Token: 0x06000F04 RID: 3844 RVA: 0x0008DC2B File Offset: 0x0008BE2B
			// (set) Token: 0x06000F05 RID: 3845 RVA: 0x0008DC33 File Offset: 0x0008BE33
			public int AttendeeId { get; set; }

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x06000F06 RID: 3846 RVA: 0x0008DC3C File Offset: 0x0008BE3C
			// (set) Token: 0x06000F07 RID: 3847 RVA: 0x0008DC44 File Offset: 0x0008BE44
			public int AppointmentId { get; set; }

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x06000F08 RID: 3848 RVA: 0x0008DC4D File Offset: 0x0008BE4D
			// (set) Token: 0x06000F09 RID: 3849 RVA: 0x0008DC55 File Offset: 0x0008BE55
			public int PersonId { get; set; }

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x06000F0A RID: 3850 RVA: 0x0008DC5E File Offset: 0x0008BE5E
			// (set) Token: 0x06000F0B RID: 3851 RVA: 0x0008DC66 File Offset: 0x0008BE66
			public bool NoShow { get; set; }

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x06000F0C RID: 3852 RVA: 0x0008DC6F File Offset: 0x0008BE6F
			// (set) Token: 0x06000F0D RID: 3853 RVA: 0x0008DC77 File Offset: 0x0008BE77
			public int MiscCode { get; set; }

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x06000F0E RID: 3854 RVA: 0x0008DC80 File Offset: 0x0008BE80
			// (set) Token: 0x06000F0F RID: 3855 RVA: 0x0008DC88 File Offset: 0x0008BE88
			public string FirstName { get; set; }

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x06000F10 RID: 3856 RVA: 0x0008DC91 File Offset: 0x0008BE91
			// (set) Token: 0x06000F11 RID: 3857 RVA: 0x0008DC99 File Offset: 0x0008BE99
			public string MiddleName { get; set; }

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0008DCA2 File Offset: 0x0008BEA2
			// (set) Token: 0x06000F13 RID: 3859 RVA: 0x0008DCAA File Offset: 0x0008BEAA
			public string LastName { get; set; }

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x06000F14 RID: 3860 RVA: 0x0008DCB3 File Offset: 0x0008BEB3
			// (set) Token: 0x06000F15 RID: 3861 RVA: 0x0008DCBB File Offset: 0x0008BEBB
			public string StudentNumber { get; set; }

			// Token: 0x17000152 RID: 338
			// (get) Token: 0x06000F16 RID: 3862 RVA: 0x0008DCC4 File Offset: 0x0008BEC4
			// (set) Token: 0x06000F17 RID: 3863 RVA: 0x0008DCCC File Offset: 0x0008BECC
			public int GroupId { get; set; }
		}
	}
}
