using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.DAO.Impl.Appointments
{
	// Token: 0x02000129 RID: 297
	public class AppointmentRoomDAO : IAppointmentRoomDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x000572C4 File Offset: 0x000554C4
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x000572CC File Offset: 0x000554CC
		public OperationContext OpContext { get; set; }

		// Token: 0x06000885 RID: 2181 RVA: 0x000572D5 File Offset: 0x000554D5
		public AppointmentRoomDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x000572E8 File Offset: 0x000554E8
		public static T GetAppointmentRoomBaseFromRecord<T>(IDataReader record, OperationContext opContext, IBatchDecryptor batchDecryptor = null) where T : AppointmentRoom
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			bool flag = record == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				int num = (record["personid"] is DBNull) ? 0 : ((int)record["personid"]);
				bool flag2 = num < 1;
				if (flag2)
				{
					result = default(T);
				}
				else
				{
					T t = Activator.CreateInstance<T>();
					t.RoomId = num;
					t.RoomDescription = AppointmentRoomDAO.DecryptString(record, "lastname", encryption, batchDecryptor);
					t.RoomTitle = AppointmentRoomDAO.DecryptString(record, "firstname", encryption, batchDecryptor);
					t.RoomUniqueId = AppointmentRoomDAO.DecryptString(record, "student_no", encryption, batchDecryptor);
					t.RoomInfo = AppointmentRoomDAO.DecryptString(record, "middlename", encryption, batchDecryptor);
					result = t;
				}
			}
			return result;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x000573E8 File Offset: 0x000555E8
		private static string DecryptString(IDataReader record, string colName, IEncryption encryption, IBatchDecryptor batchDecryptor)
		{
			bool flag = record[colName] is DBNull;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				byte[] array = (byte[])record[colName];
				bool flag2 = batchDecryptor == null;
				if (flag2)
				{
					result = encryption.Decrypt(array);
				}
				else
				{
					result = batchDecryptor.Decrypt(array);
				}
			}
			return result;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0005743C File Offset: 0x0005563C
		private AppointmentRoom GetAppointmentRoomFromRecord(IDataReader record)
		{
			return AppointmentRoomDAO.GetAppointmentRoomBaseFromRecord<AppointmentRoom>(record, this.OpContext, null);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0005745C File Offset: 0x0005565C
		public IList<AppointmentRoom> LoadAllRooms()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IList<AppointmentRoom> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    pg.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\nFROM        peoplegroups pg LEFT JOIN people p ON p.personid=pg.personid\r\nWHERE       pg.groupid=3 AND p.isactive=1"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<AppointmentRoom> list = new List<AppointmentRoom>();
					while (dataReader.Read())
					{
						AppointmentRoom appointmentRoomFromRecord = this.GetAppointmentRoomFromRecord(dataReader);
						bool flag2 = appointmentRoomFromRecord != null;
						if (flag2)
						{
							list.Add(appointmentRoomFromRecord);
						}
					}
					list.Sort((AppointmentRoom g1, AppointmentRoom g2) => (g1.RoomTitle ?? "").CompareTo(g2.RoomTitle ?? ""));
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00057518 File Offset: 0x00055718
		public IList<AppointmentRoomWithAvailability> LoadRoomsWithAvailability(IList<int> RoomIds, DateTime StartDateTime, DateTime EndDateTime)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[3];
			int num = 0;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@pids";
			DbType pType = DbType.String;
			object value;
			if (RoomIds != null)
			{
				value = string.Join(",", RoomIds.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray());
			}
			else
			{
				value = "";
			}
			array[num] = databaseLayer2.GetParameter(pName, pType, value);
			array[1] = databaseLayer.GetParameter("@startdate", DbType.DateTime, StartDateTime);
			array[2] = databaseLayer.GetParameter("@enddate", DbType.DateTime, EndDateTime);
			DbParameter[] parameters = array;
			IList<AppointmentRoomWithAvailability> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT orderid AS personid INTO #t1 FROM splitorderids(@pids,',');\r\n\r\nSELECT    pg.personid,p.firstname,p.middlename,p.lastname,p.student_no,\r\n          CASE WHEN EXISTS(SELECT appointmentid FROM apps WHERE cancelled=0 AND personid=pg.personid AND NOT ( ( enddate<=@startdate ) OR (startdate >= @enddate ) ))\r\n            THEN CAST(0 AS bit)\r\n          ELSE CAST(1 as bit) END AS isavailable\r\nFROM      peoplegroups pg LEFT JOIN people p ON p.personid=pg.personid\r\nWHERE     pg.groupid=3 AND p.isactive=1\r\n          AND pg.personid IN (SELECT personid FROM #t1);\r\n\r\nDROP TABLE #t1", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<AppointmentRoomWithAvailability> list = new List<AppointmentRoomWithAvailability>();
					while (dataReader.Read())
					{
						AppointmentRoomWithAvailability appointmentRoomBaseFromRecord = AppointmentRoomDAO.GetAppointmentRoomBaseFromRecord<AppointmentRoomWithAvailability>(dataReader, this.OpContext, null);
						bool flag2 = appointmentRoomBaseFromRecord != null;
						if (flag2)
						{
							object obj = dataReader["isavailable"];
							bool flag3 = obj != DBNull.Value;
							if (flag3)
							{
								bool flag4 = obj is bool;
								if (flag4)
								{
									appointmentRoomBaseFromRecord.IsAvailable = (bool)obj;
								}
								else
								{
									appointmentRoomBaseFromRecord.IsAvailable = Convert.ToBoolean(dataReader["isavailable"]);
								}
							}
							else
							{
								appointmentRoomBaseFromRecord.IsAvailable = false;
							}
							list.Add(appointmentRoomBaseFromRecord);
						}
					}
					list.Sort((AppointmentRoomWithAvailability g1, AppointmentRoomWithAvailability g2) => (g1.RoomTitle ?? "").CompareTo(g2.RoomTitle ?? ""));
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x000576D4 File Offset: 0x000558D4
		public AppointmentRoom LoadRoomById(int RoomId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, RoomId)
			};
			AppointmentRoom result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    p.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\nFROM        people p\r\nWHERE       p.personid=@pid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetAppointmentRoomFromRecord(dataReader);
				}
			}
			return result;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00057768 File Offset: 0x00055968
		public IList<AppointmentRoom> LoadRoomsInGrousp(params int[] GroupIds)
		{
			List<AppointmentRoom> list = new List<AppointmentRoom>();
			List<int> list2;
			if (GroupIds == null)
			{
				list2 = null;
			}
			else
			{
				list2 = (from g in GroupIds
				where g > 0
				select g).Distinct<int>().ToList<int>();
			}
			List<int> list3 = list2;
			bool flag = list3 == null || list3.Count < 1;
			IList<AppointmentRoom> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] array = new DbParameter[1];
				array[0] = databaseLayer.GetParameter("@gids", DbType.String, string.Join(",", (from g in list3
				select g.ToString()).ToArray<string>()));
				DbParameter[] parameters = array;
				using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT DISTINCT pg.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\nFROM    peoplegroups pg LEFT JOIN people p ON p.personid=pg.personid\r\nWHERE   pg.groupid IN (SELECT orderid AS groupid FROM splitorderids(@gids,',')) \r\n        AND pg.groupid IN (SELECT groupid FROM peoplegroups WHERE groupid=3) AND p.isactive=1", parameters))
				{
					bool flag2 = dataReader == null;
					if (flag2)
					{
						return null;
					}
					while (dataReader.Read())
					{
						AppointmentRoom room = this.GetAppointmentRoomFromRecord(dataReader);
						bool flag3 = room != null && room.RoomId > 0 && list.FirstOrDefault((AppointmentRoom g) => g.RoomId == room.RoomId) == null;
						if (flag3)
						{
							list.Add(room);
						}
					}
				}
				result = list;
			}
			return result;
		}
	}
}
