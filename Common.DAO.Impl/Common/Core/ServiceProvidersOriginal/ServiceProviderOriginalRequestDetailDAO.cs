using System;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x02000015 RID: 21
	public class ServiceProviderOriginalRequestDetailDAO : IServiceProviderOriginalRequestDetailDAO, IBaseOperationContext<ServiceProvidersOperationContext>
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003F3A File Offset: 0x0000213A
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003F42 File Offset: 0x00002142
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x06000079 RID: 121 RVA: 0x00003F4B File Offset: 0x0000214B
		public ServiceProviderOriginalRequestDetailDAO(ServiceProvidersOperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			ServiceProvidersOperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003F7C File Offset: 0x0000217C
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003F84 File Offset: 0x00002184
		public ServiceProvidersOperationContext OpContext { get; set; }

		// Token: 0x0600007C RID: 124 RVA: 0x00003F90 File Offset: 0x00002190
		public ServiceProviderRequestDetail LoadServiceRequestDetailByRequestId(int serviceProviderRequestId)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@sprid", DbType.Int32, serviceProviderRequestId)
			};
			ServiceProviderRequestDetail result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\tspr.ServiceProviderId,sprd.ServiceProviderRequestDetailId,\r\n\t\tsprd.CounsellorPid,p.student_no,p.firstName,p.middleName,p.lastName,\r\n\t\tsprd.dateentered2,sprd.fsBSWD,sprd.fsBSWDStatus,sprd.fsFirstNations,sprd.fsFirstNationsCaseWorkerPhone,sprd.fsFirstNationsLetterOfApprovalFile,\r\n\t\tsprd.fsFirstNationsLetterOfApprovalFilename,sprd.fsFirstNationsStatus,sprd.fsInterpreterFund,sprd.fsInterpreterFundCode,\r\n\t\tsprd.fsInterpreterFundStatus,sprd.fsOsapStatus,sprd.fsOther,sprd.fsOtherDetail,sprd.fsOtherDetail,sprd.fsOtherFile,\r\n\t\tsprd.fsOtherFilename,sprd.fsOtherStatus,sprd.fsSsd,sprd.fsSsdStatus,sprd.fsWSIB,sprd.fsWSIBCaseWorkerPhone,sprd.fsWSIBLetterOfApprovalFile,\r\n\t\tsprd.fsWSIBLetterOfApprovalFilename,sprd.fsWSIBStatus,sprd.[plan],sprd.rationale,sprd.specialrequest\r\nFROM\tServiceProviderRequests spr LEFT JOIN ServiceProviderRequestDetail sprd ON sprd.ServiceProviderRequestDetailId=spr.ServiceProviderRequestDetailId\r\n\t\tLEFT JOIN people p ON p.PersonID=sprd.CounsellorPid\r\nWHERE\tspr.ServiceProviderRequestID=@sprid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetRequestDetailFromRecord(dataReader, databaseLayer.Encryption.GetBatchDecryptor());
				}
			}
			return result;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004028 File Offset: 0x00002228
		private ServiceProviderRequestDetail GetRequestDetailFromRecord(IDataReader record, IBatchDecryptor batchDecryptor)
		{
			int num = (record["ServiceProviderRequestID"] is DBNull) ? 0 : ((int)record["ServiceProviderRequestID"]);
			bool flag = num < 1;
			ServiceProviderRequestDetail result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ServiceProviderRequestDetail serviceProviderRequestDetail = new ServiceProviderRequestDetail();
				serviceProviderRequestDetail.ServiceProviderRequestDetailId = num;
				serviceProviderRequestDetail.Plan = ((record["plan"] is DBNull) ? null : batchDecryptor.Decrypt((byte[])record["plan"]));
				serviceProviderRequestDetail.CounsellorWhoEntered = PeopleDAO.GetBasicPersonFromRecord("", record, batchDecryptor);
				serviceProviderRequestDetail.Rationale = ((record["rationale"] is DBNull) ? null : batchDecryptor.Decrypt((byte[])record["rationale"]));
				serviceProviderRequestDetail.SpecialRequest = ((record["specialrequest"] is DBNull) ? null : batchDecryptor.Decrypt((byte[])record["specialrequest"]));
				ServiceProviderRequestDetail serviceProviderRequestDetail2 = serviceProviderRequestDetail;
				ServiceProviderRequestLegacyInfo serviceProviderRequestLegacyInfo = new ServiceProviderRequestLegacyInfo();
				serviceProviderRequestLegacyInfo.dateentered2 = ((record["dateentered2"] is DBNull) ? DateTime.MinValue : ((DateTime)record["dateentered2"]));
				serviceProviderRequestLegacyInfo.fsFirstNationsCaseWorkerPhone = ((record["fsfirstnationscaseworkerphone"] is DBNull) ? null : batchDecryptor.Decrypt((byte[])record["fsfirstnationscaseworkerphone"]));
				serviceProviderRequestLegacyInfo.fsWSIBCaseWorkerPhone = ((record["fswsibcaseworkerphone"] is DBNull) ? null : batchDecryptor.Decrypt((byte[])record["fswsibcaseworkerphone"]));
				serviceProviderRequestLegacyInfo.fsOtherDetail = ((record["fsotherdetail"] is DBNull) ? null : batchDecryptor.Decrypt((byte[])record["fsotherdetail"]));
				serviceProviderRequestLegacyInfo.fsBSWD = (!(record["fsbswd"] is DBNull) && Convert.ToBoolean(record["fsbswd"]));
				serviceProviderRequestLegacyInfo.fsWSIB = (!(record["fswsib"] is DBNull) && Convert.ToBoolean(record["fswsib"]));
				serviceProviderRequestLegacyInfo.fsFirstNations = (!(record["fsfirstnations"] is DBNull) && Convert.ToBoolean(record["fsfirstnations"]));
				serviceProviderRequestLegacyInfo.fsInterpreterFund = (!(record["fsinterpreterfund"] is DBNull) && Convert.ToBoolean(record["fsinterpreterfund"]));
				serviceProviderRequestLegacyInfo.fsOther = (!(record["fsother"] is DBNull) && Convert.ToBoolean(record["fsother"]));
				serviceProviderRequestLegacyInfo.fsSsd = (!(record["fsssd"] is DBNull) && Convert.ToBoolean(record["fsssd"]));
				serviceProviderRequestLegacyInfo.fsBSWDStatus = this.ParseEnum<eServiceProviderRequestDetailLegacyItemStatus>(record["fsBSWDStatus"], eServiceProviderRequestDetailLegacyItemStatus.Unknown);
				serviceProviderRequestLegacyInfo.fsWSIBStatus = this.ParseEnum<eServiceProviderRequestDetailLegacyItemStatus>(record["fsWSIBStatus"], eServiceProviderRequestDetailLegacyItemStatus.Unknown);
				serviceProviderRequestLegacyInfo.fsFirstNationsStatus = this.ParseEnum<eServiceProviderRequestDetailLegacyItemStatus>(record["fsFirstNationsStatus"], eServiceProviderRequestDetailLegacyItemStatus.Unknown);
				serviceProviderRequestLegacyInfo.fsInterpreterFundStatus = this.ParseEnum<eServiceProviderRequestDetailLegacyItemStatus>(record["fsInterpreterFundStatus"], eServiceProviderRequestDetailLegacyItemStatus.Unknown);
				serviceProviderRequestLegacyInfo.fsOtherStatus = this.ParseEnum<eServiceProviderRequestDetailLegacyItemStatus>(record["fsOtherStatus"], eServiceProviderRequestDetailLegacyItemStatus.Unknown);
				serviceProviderRequestLegacyInfo.fsSsdStatus = this.ParseEnum<eServiceProviderRequestDetailLegacyItemStatus>(record["fsSsdStatus"], eServiceProviderRequestDetailLegacyItemStatus.Unknown);
				ServiceProviderRequestLegacyInfo serviceProviderRequestLegacyInfo2 = serviceProviderRequestLegacyInfo;
				BinaryFile fsFirstNationsLetterOfApprovalFile;
				if (!(record["fsFirstNationsLetterOfApprovalFile"] is DBNull))
				{
					(fsFirstNationsLetterOfApprovalFile = new BinaryFile()).ByteArray = (byte[])record["fsFirstNationsLetterOfApprovalFile"];
				}
				else
				{
					fsFirstNationsLetterOfApprovalFile = null;
				}
				serviceProviderRequestLegacyInfo2.fsFirstNationsLetterOfApprovalFile = fsFirstNationsLetterOfApprovalFile;
				serviceProviderRequestDetail2.LegacyInfo = serviceProviderRequestLegacyInfo;
				result = serviceProviderRequestDetail;
			}
			return result;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000043E4 File Offset: 0x000025E4
		private T ParseEnum<T>(object enumValue, T defaultValue) where T : struct
		{
			bool flag = enumValue is DBNull || !(enumValue is int);
			T result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				int num = (int)enumValue;
				bool flag2 = !Enum.IsDefined(typeof(T), num);
				if (flag2)
				{
					result = defaultValue;
				}
				else
				{
					result = (T)((object)enumValue);
				}
			}
			return result;
		}
	}
}
