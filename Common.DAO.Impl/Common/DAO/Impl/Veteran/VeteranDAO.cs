using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Veteran;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Veteran;

namespace TechnoPro.Common.DAO.Impl.Veteran
{
	// Token: 0x02000025 RID: 37
	public class VeteranDAO : IVeteranDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000067E0 File Offset: 0x000049E0
		// (set) Token: 0x060000DC RID: 220 RVA: 0x000067E8 File Offset: 0x000049E8
		public OperationContext OpContext { get; set; }

		// Token: 0x060000DD RID: 221 RVA: 0x000067F1 File Offset: 0x000049F1
		public VeteranDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00006804 File Offset: 0x00004A04
		private static ChangeInBenefitRequest GetChangeInBenefitRequestFromRecord(IDataReader record, IBatchDecryptor batchDecryptor)
		{
			int num = (record["personid"] is DBNull) ? 0 : ((int)record["personid"]);
			int num2 = (record["appointmentid"] is DBNull) ? 0 : ((int)record["appointmentid"]);
			bool flag = num < 1 || num2 < 1;
			ChangeInBenefitRequest result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new ChangeInBenefitRequest
				{
					PersonId = num,
					AppointmentId = num2,
					DateEntered = ((record["dateentered"] is DBNull) ? DateTime.MinValue : ((DateTime)record["dateentered"])),
					Status = VeteranDAO.GetStatus((record["status"] is DBNull) ? "" : record["status"].ToString())
				};
			}
			return result;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000068FC File Offset: 0x00004AFC
		private static eVeteranRequestStatus GetStatus(string statusDynamicDataValue)
		{
			string text = (statusDynamicDataValue ?? "").Trim();
			bool flag = text.Length < 1;
			eVeteranRequestStatus result;
			if (flag)
			{
				result = eVeteranRequestStatus.Unspecified;
			}
			else
			{
				result = (text.StartsWith("approved", StringComparison.OrdinalIgnoreCase) ? eVeteranRequestStatus.Approved : eVeteranRequestStatus.Denied);
			}
			return result;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006944 File Offset: 0x00004B44
		public IList<ChangeInBenefitRequest> LoadBenefitRequests(int PersonId, DateTime StartDate, DateTime EndDate, int ChangeInBenefitRequestScreenNum, int DropListStatusCid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PersonId),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, StartDate.Date),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, EndDate.Date),
				databaseLayer.GetParameter("@screennum", DbType.Int32, ChangeInBenefitRequestScreenNum),
				databaseLayer.GetParameter("@cid", DbType.Int32, DropListStatusCid)
			};
			List<ChangeInBenefitRequest> list = new List<ChangeInBenefitRequest>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @sd datetime = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\nDECLARE @ed datetime = DATEADD(D, 1, DATEDIFF(D, 0, @enddate))\r\n\r\nSELECT\tpm.personid,pm.appointmentid,pm.dateentered,pm.[description],\r\n\t\tm.controlid,m.controlvalue,ll.LookupText AS [status]\r\nFROM\tinfopm pm LEFT JOIN maininfopm m ON m.personid=pm.personid AND m.appointmentid=pm.appointmentid AND m.controlid=@cid\r\n\t\tLEFT JOIN LookupLists ll ON ll.lookuplistid=m.controlvalue\r\nWHERE\tpm.personid=@pid\r\n\t\tAND pm.dateentered >= @sd AND pm.dateentered < @ed \r\n\t\tAND pm.screennum=@screennum\r\n\t\tAND (pm.[description] IS NULL OR NOT CAST(pm.[description] AS varchar(max)) = '.DELETED.')\r\nORDER BY pm.dateentered DESC,pm.personid,pm.appointmentid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return null;
				}
				IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
				ChangeInBenefitRequest changeInBenefitRequest = null;
				while (dataReader.Read())
				{
					ChangeInBenefitRequest changeInBenefitRequestFromRecord = VeteranDAO.GetChangeInBenefitRequestFromRecord(dataReader, batchDecryptor);
					bool flag2 = changeInBenefitRequestFromRecord == null;
					if (!flag2)
					{
						bool flag3 = changeInBenefitRequest == null || changeInBenefitRequest.PersonId != changeInBenefitRequestFromRecord.PersonId || changeInBenefitRequest.AppointmentId != changeInBenefitRequestFromRecord.AppointmentId;
						if (flag3)
						{
							changeInBenefitRequest = changeInBenefitRequestFromRecord;
							list.Add(changeInBenefitRequest);
						}
						else
						{
							bool flag4 = changeInBenefitRequest.Status == eVeteranRequestStatus.Denied;
							if (!flag4)
							{
								bool flag5 = changeInBenefitRequestFromRecord.Status > eVeteranRequestStatus.Unspecified;
								if (flag5)
								{
									changeInBenefitRequest.Status = changeInBenefitRequestFromRecord.Status;
								}
							}
						}
					}
				}
			}
			return list;
		}
	}
}
