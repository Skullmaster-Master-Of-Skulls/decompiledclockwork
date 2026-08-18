using System;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.ServiceProvider;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.DAO.Impl.ServiceProvider
{
	// Token: 0x02000058 RID: 88
	public class ServiceProviderApplicationDAO : IServiceProviderApplicationDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000230 RID: 560 RVA: 0x000131D7 File Offset: 0x000113D7
		public ServiceProviderApplicationDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00013207 File Offset: 0x00011407
		// (set) Token: 0x06000232 RID: 562 RVA: 0x0001320F File Offset: 0x0001140F
		public OperationContext OpContext { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00013218 File Offset: 0x00011418
		private IServiceProviderDAO serviceProviderDao
		{
			get
			{
				bool flag = this._serviceProviderDao == null;
				if (flag)
				{
					this._serviceProviderDao = new ServiceProviderDAO(this.OpContext);
				}
				return this._serviceProviderDao;
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00013250 File Offset: 0x00011450
		private SPApplication GetApplicationFromRecord(IDataReader record)
		{
			bool flag = record == null || record["spapplicationid"] == DBNull.Value;
			SPApplication result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new SPApplication
				{
					SPApplicationId = (int)record["spapplicationid"],
					ApplicationAvailabilityType = this.GetApplicationAvailabilityTypeFromRecord(record),
					DateEntered = (DateTime)record["dateentered"],
					IsActive = (record["isactive"] != DBNull.Value && Convert.ToBoolean(record["isactive"])),
					Note1 = ((record["note1"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])record["note1"])),
					Note2 = ((record["note2"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])record["note2"]))
				};
			}
			return result;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00013374 File Offset: 0x00011574
		private SPApplicationAvailabilityType GetApplicationAvailabilityTypeFromRecord(IDataReader record)
		{
			bool flag = record == null || record["SPApplicationAvailabilityTypeId"] == DBNull.Value;
			SPApplicationAvailabilityType result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new SPApplicationAvailabilityType
				{
					SPApplicationAvailabilityTypeId = (int)record["SPApplicationAvailabilityTypeId"],
					Title = record["ApplicationAvailabilityTitle"].ToString(),
					Description = record["ApplicationAvailabilityDescription"].ToString(),
					IsActive = (record["ApplicationAvailabilityIsActive"] != DBNull.Value && Convert.ToBoolean(record["ApplicationAvailabilityIsActive"])),
					IsVisible = (record["ApplicationAvailabilityIsVisible"] != DBNull.Value && Convert.ToBoolean(record["ApplicationAvailabilityIsVisible"]))
				};
			}
			return result;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0001344C File Offset: 0x0001164C
		public SPApplication LoadApplicationById(int SPApplicationId)
		{
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    spa.SPApplicationId,spa.SPProviderId,p.firstname,p.middlename,p.lastname,p.student_no,\r\n\t\t  sp.UserName,sp.ExternalId,sp.Address1,sp.Address1IsPrimary,sp.AlternateEmail,sp.Email,\r\n\t\t  sp.IsActive,sp.Note1,sp.Note2,sp.Phone1,sp.Phone2,sp.PhoneNote,sp.Specializations,\r\n\t\t  spa.SPProviderTypeId,spt.ProviderTypeTitle,spt.ProviderTypeDescription,spt.ProviderTypeIsActive,\r\n\t\t  spt.SPProviderTypeBehaviourCode,\r\n\t\t  spa.SPApplicationAvailabilityTypeId,sat.ApplicationAvailabilityTitle,sat.ApplicationAvailabilityDescription,\r\n\t\t  sat.ApplicationAvailabilityIsActive,sat.ApplicationAvailabilityIsVisible,\r\n\t\t  spa.note1,spa.note2,spa.dateentered,spa.whoentered,spa.isactive,spa.rateofpay,\r\n\t\t  spa.sprateofpaytypeid,rop.rateofpaytitle,rop.rateofpaydescription,rop.RateOfPayIsOneTimePayment,\r\n\t\t  rop.RateOfPayIsHourlyRate,rop.rateofpayisactive\r\nFROM        spapplication spa LEFT JOIN SPProvider sp ON sp.SPProviderId=spa.SPProviderId \r\n\t\t\tLEFT JOIN people_hide p ON p.personid=sp.PersonId \r\n\t\t\tLEFT JOIN SPProviderType spt ON spt.SPProviderTypeId=spa.SPProviderTypeId \r\n\t\t\tLEFT JOIN SPApplicationAvailabilityType sat ON sat.SPApplicationAvailabilityTypeId=spa.SPApplicationAvailabilityTypeId \r\n\t\t\tLEFT JOIN sprateofpaytype rop ON rop.sprateofpaytypeid=spa.SPRateOfPayTypeId \r\nWHERE\t\tspa.SPApplicationId=@id", new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.Int32, SPApplicationId)
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return this.GetApplicationFromRecord(dataReader);
				}
			}
			return null;
		}

		// Token: 0x040000DB RID: 219
		public DatabaseLayer DatabaseManager;

		// Token: 0x040000DD RID: 221
		private IServiceProviderDAO _serviceProviderDao;
	}
}
