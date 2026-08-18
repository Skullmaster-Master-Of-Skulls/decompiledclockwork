using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.AlternativeFormat;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.DAO.Impl.AlternativeFormat
{
	// Token: 0x0200016D RID: 365
	public class MediaVolunteerDAO : IMediaVolunteerDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000B0B RID: 2827 RVA: 0x000752D9 File Offset: 0x000734D9
		public MediaVolunteerDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x000752EB File Offset: 0x000734EB
		// (set) Token: 0x06000B0D RID: 2829 RVA: 0x000752F3 File Offset: 0x000734F3
		public OperationContext OpContext { get; set; }

		// Token: 0x06000B0E RID: 2830 RVA: 0x000752FC File Offset: 0x000734FC
		public MediaJobVolunteerInfo GetMediaVolunteerById(int jobVolunteerId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@jobvolunteerid", DbType.Int32, jobVolunteerId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_AlternateFormat_LoadJobVolunteerById", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetMediaJobVolunteerInfoFromReader(dataReader, null);
				}
			}
			return null;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00075390 File Offset: 0x00073590
		public MediaJobVolunteerInfo GetMediaVolunteerByVolunteerAndJob(int volunteerId, int mediaJobId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@volunteerid", DbType.Int32, volunteerId),
				databaseLayer.GetParameter("@mediajobid", DbType.Int32, mediaJobId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_AlternateFormat_LoadJobVolunteerByVolunteerAndJobId", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetMediaJobVolunteerInfoFromReader(dataReader, null);
				}
			}
			return null;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0007543C File Offset: 0x0007363C
		public IList<MediaJobVolunteerInfo> GetMediaVolunteersAssignedToMediaJob(int mediaJobId)
		{
			List<MediaJobVolunteerInfo> list = new List<MediaJobVolunteerInfo>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@mediajobid", DbType.Int32, mediaJobId);
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_AlternateFormat_LoadJobVolunteerAssignedToJobId", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						MediaJobVolunteerInfo mediaJobVolunteerInfoFromReader = this.GetMediaJobVolunteerInfoFromReader(dataReader, batchDecryptor);
						bool flag2 = mediaJobVolunteerInfoFromReader != null;
						if (flag2)
						{
							list.Add(mediaJobVolunteerInfoFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00075504 File Offset: 0x00073704
		public IList<MediaJobVolunteerInfo> GetMediaJobVolunteerInfoByVolunteer(int volunteerId)
		{
			List<MediaJobVolunteerInfo> list = new List<MediaJobVolunteerInfo>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@volunteerid", DbType.Int32, volunteerId);
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_AlternateFormat_LoadJobVolunteerByVolunteerId", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						MediaJobVolunteerInfo mediaJobVolunteerInfoFromReader = this.GetMediaJobVolunteerInfoFromReader(dataReader, batchDecryptor);
						bool flag2 = mediaJobVolunteerInfoFromReader != null;
						if (flag2)
						{
							list.Add(mediaJobVolunteerInfoFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x000755CC File Offset: 0x000737CC
		public int CreateMediaJobVolunteer(MediaJobVolunteerInfo mediaJobVolunteer)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@jobvolunteerid", DbType.Int32, 0),
				databaseLayer.GetParameter("@volunteerid", DbType.Int32, mediaJobVolunteer.Volunteer.Staff.PersonId),
				databaseLayer.GetParameter("@mediajobid", DbType.Int32, mediaJobVolunteer.MediaJobId),
				databaseLayer.GetParameter("@whoassignedvolunteer", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@jobvolunteernotes", DbType.String, mediaJobVolunteer.JobVolunteerNotes ?? string.Empty)
			};
			return (databaseLayer.ExecuteStoredProcedure("sp_AlternateFormat_AssignedJobVolunteer", array) > 0) ? (mediaJobVolunteer.Id = Convert.ToInt32(array[0].Value)) : -1;
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x000756B8 File Offset: 0x000738B8
		public void ChangeMediaJobVolunteerNotes(int volunteerId, int mediaJobId, string newNotes)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@volunteerid", DbType.Int32, volunteerId),
				databaseLayer.GetParameter("@mediajobid", DbType.Int32, mediaJobId),
				databaseLayer.GetParameter("@jobvolunteernotes", DbType.String, newNotes ?? string.Empty)
			};
			databaseLayer.ExecuteNonQuery("update AlternativeFormat_MediaJob_x_Volunteer\r\n                set JobVolunteerNotes=@jobvolunteernotes\r\n                where VolunteerId=@volunteerid and MediaJobId=@mediajobid", parameters);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0007573C File Offset: 0x0007393C
		public void ChangeMediaJobVolunteerActiveStatus(int volunteerId, int mediaJobId, bool isActive)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@volunteerid", DbType.Int32, volunteerId),
				databaseLayer.GetParameter("@mediajobid", DbType.Int32, mediaJobId),
				databaseLayer.GetParameter("@isactive", DbType.Boolean, isActive)
			};
			databaseLayer.ExecuteNonQuery("update AlternativeFormat_MediaJob_x_Volunteer\r\n                set IsActive=@isactive\r\n                where VolunteerId=@volunteerid and MediaJobId=@mediajobid", parameters);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000757B8 File Offset: 0x000739B8
		public void ChangeMediaJobVolunteerActiveStatus(int jobVolunteerId, bool isActive)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@jobvolunteerid", DbType.Int32, jobVolunteerId),
				databaseLayer.GetParameter("@isactive", DbType.Boolean, isActive)
			};
			databaseLayer.ExecuteNonQuery("update AlternativeFormat_MediaJob_x_Volunteer\r\n                set IsActive=@isactive\r\n                where JobVolunteerId=@jobvolunteerid", parameters);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00075820 File Offset: 0x00073A20
		public void ChangeMediaJobVolunteerActiveStatus(IList<int> jobVolunteerIdList, bool isActive)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@jobvolunteeridlist", DbType.String, jobVolunteerIdList.CommaSeparatedValuesWithoutSpace<int>()),
				databaseLayer.GetParameter("@isactive", DbType.Boolean, isActive)
			};
			databaseLayer.ExecuteNonQuery("update AlternativeFormat_MediaJob_x_Volunteer\r\n                set IsActive=@isactive\r\n                where JobVolunteerId IN (SELECT OrderId as JobVolunteerId from SplitOrderIds(@jobvolunteeridlist, ','))", parameters);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00075888 File Offset: 0x00073A88
		public IList<MediaJobVolunteerWorkingHoursInfo> GetMediaJobVolunteerWorkingHoursByVolunteerAndJob(int volunteerId, int mediaJobId)
		{
			List<MediaJobVolunteerWorkingHoursInfo> list = new List<MediaJobVolunteerWorkingHoursInfo>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@volunteerid", DbType.Int32, volunteerId),
				databaseLayer.GetParameter("@mediajobid", DbType.Int32, mediaJobId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_AlternateFormat_LoadVolunteerWorkingHoursByVolunteerAndJobId", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						MediaJobVolunteerWorkingHoursInfo volunteerWorkingHoursFromReader = this.GetVolunteerWorkingHoursFromReader(dataReader, batchDecryptor);
						bool flag2 = volunteerWorkingHoursFromReader != null;
						if (flag2)
						{
							list.Add(volunteerWorkingHoursFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00075964 File Offset: 0x00073B64
		public IList<MediaJobVolunteerWorkingHoursInfo> GetAllMediaJobVolunteerWorkingHoursByVolunteerId(int volunteerId)
		{
			List<MediaJobVolunteerWorkingHoursInfo> list = new List<MediaJobVolunteerWorkingHoursInfo>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@volunteerid", DbType.Int32, volunteerId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_AlternateFormat_LoadAllVolunteerWorkingHoursByVolunteerId", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						MediaJobVolunteerWorkingHoursInfo volunteerWorkingHoursFromReader = this.GetVolunteerWorkingHoursFromReader(dataReader, batchDecryptor);
						bool flag2 = volunteerWorkingHoursFromReader != null;
						if (flag2)
						{
							list.Add(volunteerWorkingHoursFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00075A2C File Offset: 0x00073C2C
		public int AddMediaJobVolunteerWorkingHours(MediaJobVolunteerWorkingHoursInfo volunteerWorkingHours)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@jobvolunteerworkinghoursid", DbType.Int32, 0),
				databaseLayer.GetParameter("@volunteerid", DbType.Int32, volunteerWorkingHours.Volunteer.Staff.PersonId),
				databaseLayer.GetParameter("@mediajobid", DbType.Int32, volunteerWorkingHours.MediaJobId),
				databaseLayer.GetParameter("@startworkingtime", DbType.DateTime, volunteerWorkingHours.StartWorkingTime),
				databaseLayer.GetParameter("@endworkingtime", DbType.DateTime, volunteerWorkingHours.EndWorkingTime),
				databaseLayer.GetParameter("@whoaddworkinghours", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@volunteerworkinghoursnotes", DbType.String, volunteerWorkingHours.VolunteerWorkingHoursNotes ?? string.Empty)
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO [AlternativeFormat_VolunteerWorkingHours]\r\n                       ([VolunteerId]\r\n                       ,[MediaJobId]\r\n                       ,[StartWorkingTime]\r\n                       ,[EndWorkingTime]\r\n                       ,[WhoAddWorkingHours]\r\n                       ,[VolunteerWorkingHoursNotes])\r\n                 VALUES\r\n                       (@volunteerid\r\n                       ,@mediajobid\r\n                       ,@startworkingtime\r\n                       ,@endworkingtime\r\n                       ,@whoaddworkinghours\r\n                       ,@volunteerworkinghoursnotes)\r\n\r\n            SET @jobvolunteerworkinghoursid = scope_identity()", array);
			return volunteerWorkingHours.Id = Convert.ToInt32(array[0].Value);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00075B48 File Offset: 0x00073D48
		public void UpdateMediaJobVolunteerWorkingHours(MediaJobVolunteerWorkingHoursInfo volunteerWorkingHours)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startworkingtime", DbType.DateTime, volunteerWorkingHours.StartWorkingTime),
				databaseLayer.GetParameter("@endworkingtime", DbType.DateTime, volunteerWorkingHours.EndWorkingTime),
				databaseLayer.GetParameter("@whoaddworkinghours", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@volunteerworkinghoursnotes", DbType.String, volunteerWorkingHours.VolunteerWorkingHoursNotes ?? string.Empty),
				databaseLayer.GetParameter("@jobvolunteerworkinghoursid", DbType.Int32, volunteerWorkingHours.Id)
			};
			databaseLayer.ExecuteNonQuery("UPDATE [AlternativeFormat_VolunteerWorkingHours]\r\n               SET [StartWorkingTime] = @startworkingtime\r\n                  ,[EndWorkingTime] = @endworkingtime\r\n                  ,[WhoAddWorkingHours] = @whoaddworkinghours\r\n                  ,[VolunteerWorkingHoursNotes] = @volunteerworkinghoursnotes\r\n             WHERE JobVolunteerWorkingHoursId=@jobvolunteerworkinghoursid", parameters);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00075C14 File Offset: 0x00073E14
		public void DeleteMediaJobVolunteerWorkingHours(int jobVolunteerWorkingHoursId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@jobvolunteerworkinghoursid", DbType.Int32, jobVolunteerWorkingHoursId)
			};
			databaseLayer.ExecuteNonQuery("delete from AlternativeFormat_VolunteerWorkingHours where JobVolunteerWorkingHoursId=@jobvolunteerworkinghoursid", parameters);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00075C68 File Offset: 0x00073E68
		private MediaJobVolunteerInfo GetMediaJobVolunteerInfoFromReader(IDataReader record, IBatchDecryptor decryptor = null)
		{
			return new MediaJobVolunteerInfo
			{
				JobVolunteerId = (int)record["JobVolunteerId"],
				Volunteer = StaffCommonInfoDAO.GetStaffWithCommonInfoFromRecord<AlternateFormatVolunteer>(record, this.OpContext, decryptor, "vol"),
				MediaJobId = (int)record["MediaJobId"],
				WhenWasAssigned = (DateTime)record["WhenVolunteerWasAssigned"],
				JobVolunteerNotes = (string)record["JobVolunteerNotes"],
				WhoAssigned = PeopleDAO.GetPersonFromReader("from", record, this.OpContext, decryptor),
				MediaJobDueDate = (DateTime)record["JobDueDate"],
				MediaJobStartTime = (DateTime)record["JobStartTime"],
				MediaContentTitle = (string)record["ShortTitle"],
				MediaContentFormatName = ((record["MediaContentFormat"] is DBNull) ? MediaContentFormat.UNSPECIFIED : ((MediaContentFormat)Enum.Parse(typeof(MediaContentFormat), Convert.ToString(record["MediaContentFormat"]))))
			};
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00075D94 File Offset: 0x00073F94
		private MediaJobVolunteerWorkingHoursInfo GetVolunteerWorkingHoursFromReader(IDataReader record, IBatchDecryptor decryptor = null)
		{
			return new MediaJobVolunteerWorkingHoursInfo
			{
				JobVolunteerWorkingHoursId = Convert.ToInt32(record["JobVolunteerWorkingHoursId"]),
				Volunteer = StaffCommonInfoDAO.GetStaffWithCommonInfoFromRecord<AlternateFormatVolunteer>(record, this.OpContext, decryptor, "vol"),
				MediaJobId = Convert.ToInt32(record["MediaJobId"]),
				StartWorkingTime = (DateTime)record["StartWorkingTime"],
				EndWorkingTime = (DateTime)record["EndWorkingTime"],
				WhoAddWorkingHours = PeopleDAO.GetPersonFromReader("from", record, this.OpContext, decryptor),
				VolunteerWorkingHoursNotes = Convert.ToString(record["VolunteerWorkingHoursNotes"])
			};
		}
	}
}
