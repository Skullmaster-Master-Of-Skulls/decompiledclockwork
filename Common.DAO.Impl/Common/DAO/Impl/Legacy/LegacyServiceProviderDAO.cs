using System;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.Legacy.ServiceProviders;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;

namespace TechnoPro.Common.DAO.Impl.Legacy
{
	// Token: 0x020000A8 RID: 168
	public class LegacyServiceProviderDAO : ILegacyServiceProviderDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004A4 RID: 1188 RVA: 0x0002A4BE File Offset: 0x000286BE
		public LegacyServiceProviderDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0002A4D0 File Offset: 0x000286D0
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x0002A4D8 File Offset: 0x000286D8
		public OperationContext OpContext { get; set; }

		// Token: 0x060004A7 RID: 1191 RVA: 0x0002A4E4 File Offset: 0x000286E4
		private LegacyRequestDetailNotesAndSpecialInstructions GetRequestDetailNotesAndSpecialInstructionsFromRecord(IDataRecord record, IBatchDecryptor batchDecryptor)
		{
			bool flag = record == null;
			LegacyRequestDetailNotesAndSpecialInstructions result;
			if (flag)
			{
				result = null;
			}
			else
			{
				byte[] array = (record["notes"] is DBNull) ? null : ((byte[])record["notes"]);
				byte[] array2 = (record["specialinstructions"] is DBNull) ? null : ((byte[])record["specialinstructions"]);
				result = new LegacyRequestDetailNotesAndSpecialInstructions
				{
					Id = ((record["serviceproviderrequestid"] is DBNull) ? 0 : ((int)record["serviceproviderrequestid"])),
					Notes = ((array == null || array.Length < 1) ? null : batchDecryptor.Decrypt(array)),
					SpecialInstructions = ((array2 == null || array2.Length < 1) ? null : batchDecryptor.Decrypt(array2))
				};
			}
			return result;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0002A5C0 File Offset: 0x000287C0
		public LegacyRequestDetailNotesAndSpecialInstructions LoadRequestDetailNotesAndSpecialInstructions(int RequestId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@id", DbType.Int32, RequestId)
			};
			LegacyRequestDetailNotesAndSpecialInstructions result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT serviceproviderrequestid,notes,specialinstructions FROM serviceproviderrequests WHERE serviceproviderrequestid=@id", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetRequestDetailNotesAndSpecialInstructionsFromRecord(dataReader, databaseLayer.Encryption.GetBatchDecryptor());
				}
			}
			return result;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0002A660 File Offset: 0x00028860
		public void UpdateRequest(LegacyServiceProviderRequestDetail RequestDetail)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[26];
			array[0] = databaseLayer.GetParameter("@counsellorpid", DbType.Int32, RequestDetail.CounsellorPid);
			array[1] = databaseLayer.GetParameter("id", DbType.Int32, RequestDetail.ServiceProviderRequestDetailId);
			array[2] = databaseLayer.GetParameter("@rationale", DbType.Binary, string.IsNullOrEmpty(RequestDetail.Rationale) ? DBNull.Value : databaseLayer.Encryption.Encrypt(RequestDetail.Rationale));
			array[3] = databaseLayer.GetParameter("@specialrequest", DbType.Binary, string.IsNullOrEmpty(RequestDetail.SpecialRequest) ? DBNull.Value : databaseLayer.Encryption.Encrypt(RequestDetail.SpecialRequest));
			array[4] = databaseLayer.GetParameter("@plan", DbType.Binary, string.IsNullOrEmpty(RequestDetail.Plan) ? DBNull.Value : databaseLayer.Encryption.Encrypt(RequestDetail.Plan));
			int num = 5;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@fsbswd";
			DbType pType = DbType.Boolean;
			bool? flag = RequestDetail.FsBswd;
			array[num] = databaseLayer2.GetParameter(pName, pType, (flag != null) ? flag.GetValueOrDefault() : DBNull.Value);
			int num2 = 6;
			DatabaseLayer databaseLayer3 = databaseLayer;
			string pName2 = "@fsOsapStatus";
			DbType pType2 = DbType.Int32;
			int? fsOsapStatus = RequestDetail.FsOsapStatus;
			array[num2] = databaseLayer3.GetParameter(pName2, pType2, (fsOsapStatus != null) ? fsOsapStatus.GetValueOrDefault() : DBNull.Value);
			int num3 = 7;
			DatabaseLayer databaseLayer4 = databaseLayer;
			string pName3 = "@fsWSIB";
			DbType pType3 = DbType.Boolean;
			flag = RequestDetail.FsWsib;
			array[num3] = databaseLayer4.GetParameter(pName3, pType3, (flag != null) ? flag.GetValueOrDefault() : DBNull.Value);
			array[8] = databaseLayer.GetParameter("@fsWSIBLetterOfApprovalFilename", DbType.String, (RequestDetail.FsWsibLetterOfApprovalFile == null) ? DBNull.Value : (RequestDetail.FsWsibLetterOfApprovalFile.FileName ?? DBNull.Value));
			int num4 = 9;
			DatabaseLayer databaseLayer5 = databaseLayer;
			string pName4 = "@fsWSIBLetterOfApprovalFile";
			DbType pType4 = DbType.Binary;
			BinaryFile fsWsibLetterOfApprovalFile = RequestDetail.FsWsibLetterOfApprovalFile;
			array[num4] = databaseLayer5.GetParameter(pName4, pType4, (((fsWsibLetterOfApprovalFile != null) ? fsWsibLetterOfApprovalFile.ByteArray : null) == null || RequestDetail.FsWsibLetterOfApprovalFile.ByteArray.Length < 1) ? DBNull.Value : RequestDetail.FsWsibLetterOfApprovalFile.ByteArray);
			array[10] = databaseLayer.GetParameter("@fsWSIBCaseWorkerPhone", DbType.Binary, string.IsNullOrEmpty(RequestDetail.FsWsibCaseWorkerPhone) ? DBNull.Value : databaseLayer.Encryption.Encrypt(RequestDetail.FsWsibCaseWorkerPhone));
			array[11] = databaseLayer.GetParameter("@fsFirstNations", DbType.Boolean, (RequestDetail.FsFirstNations != null) ? RequestDetail.FsFirstNations.Value : DBNull.Value);
			array[12] = databaseLayer.GetParameter("@fsFirstNationsLetterOfApprovalFilename", DbType.String, (RequestDetail.FsFirstNationsLetterOfApprovalFile == null) ? DBNull.Value : (RequestDetail.FsFirstNationsLetterOfApprovalFile.FileName ?? DBNull.Value));
			int num5 = 13;
			DatabaseLayer databaseLayer6 = databaseLayer;
			string pName5 = "@fsFirstNationsLetterOfApprovalFile";
			DbType pType5 = DbType.Binary;
			BinaryFile fsFirstNationsLetterOfApprovalFile = RequestDetail.FsFirstNationsLetterOfApprovalFile;
			array[num5] = databaseLayer6.GetParameter(pName5, pType5, (((fsFirstNationsLetterOfApprovalFile != null) ? fsFirstNationsLetterOfApprovalFile.ByteArray : null) == null || RequestDetail.FsFirstNationsLetterOfApprovalFile.ByteArray.Length < 1) ? DBNull.Value : RequestDetail.FsFirstNationsLetterOfApprovalFile.ByteArray);
			array[14] = databaseLayer.GetParameter("@fsFirstNationsCaseWorkerPhone", DbType.Binary, string.IsNullOrEmpty(RequestDetail.FsFirstNationsCaseWorkerPhone) ? DBNull.Value : databaseLayer.Encryption.Encrypt(RequestDetail.FsFirstNationsCaseWorkerPhone));
			array[15] = databaseLayer.GetParameter("@fsInterpreterFund", DbType.Boolean, (RequestDetail.FsInterpreterFund != null) ? RequestDetail.FsInterpreterFund.Value : DBNull.Value);
			array[16] = databaseLayer.GetParameter("@fsInterpreterFundCode", DbType.Int32, (RequestDetail.FsInterpreterFundCode != null) ? RequestDetail.FsInterpreterFundCode.Value : DBNull.Value);
			array[17] = databaseLayer.GetParameter("@fsOther", DbType.Boolean, (RequestDetail.FsOther != null) ? RequestDetail.FsOther.Value : DBNull.Value);
			array[18] = databaseLayer.GetParameter("@fsOtherDetail", DbType.Binary, string.IsNullOrEmpty(RequestDetail.FsOtherDetail) ? DBNull.Value : databaseLayer.Encryption.Encrypt(RequestDetail.FsOtherDetail));
			array[19] = databaseLayer.GetParameter("@fsBswdStatus", DbType.Boolean, (RequestDetail.FsBswdStatus != null) ? RequestDetail.FsBswdStatus.Value : DBNull.Value);
			array[20] = databaseLayer.GetParameter("@fsWSIBStatus", DbType.Boolean, (RequestDetail.FsWsibStatus != null) ? RequestDetail.FsWsibStatus.Value : DBNull.Value);
			array[21] = databaseLayer.GetParameter("@fsFirstNationsStatus", DbType.Boolean, (RequestDetail.FsFirstNationsStatus != null) ? RequestDetail.FsFirstNationsStatus.Value : DBNull.Value);
			array[22] = databaseLayer.GetParameter("@fsInterpreterFundStatus", DbType.Boolean, (RequestDetail.FsInterpreterFundStatus != null) ? RequestDetail.FsInterpreterFundStatus.Value : DBNull.Value);
			array[23] = databaseLayer.GetParameter("@fsOtherStatus", DbType.Boolean, (RequestDetail.FsOtherStatus != null) ? RequestDetail.FsOtherStatus.Value : DBNull.Value);
			array[24] = databaseLayer.GetParameter("@fsssd", DbType.Boolean, RequestDetail.FsSsd);
			array[25] = databaseLayer.GetParameter("@fsssdstatus", DbType.Int32, (RequestDetail.FsSsdStatus != null) ? RequestDetail.FsSsdStatus.Value : DBNull.Value);
			DbParameter[] parameters = array;
			databaseLayer.ExecuteNonQuery("UPDATE serviceproviderrequestdetail SET \r\n        counsellorpid=@counsellorpid,rationale=@rationale,specialrequest=@specialrequest,\r\n        [plan]=@plan,fsbswd=@fsbswd,fsOsapStatus=@fsOsapStatus,fsWSIB=@fsWSIB,\r\n        fsWSIBLetterOfApprovalFilename=@fsWSIBLetterOfApprovalFilename,fsWSIBLetterOfApprovalFile=@fsWSIBLetterOfApprovalFile,fsWSIBCaseWorkerPhone=@fsWSIBCaseWorkerPhone,fsFirstNations=@fsFirstNations,fsFirstNationsLetterOfApprovalFilename=@fsFirstNationsLetterOfApprovalFilename,fsFirstNationsLetterOfApprovalFile=@fsFirstNationsLetterOfApprovalFile,fsFirstNationsCaseWorkerPhone=@fsFirstNationsCaseWorkerPhone,fsInterpreterFund=@fsInterpreterFund,fsInterpreterFundCode=@fsInterpreterFundCode,fsOther=@fsOther,fsOtherDetail=@fsOtherDetail,\r\n        fsBSWDStatus=@fsBSWDStatus,\r\n        fsWSIBStatus=@fsWSIBStatus,\r\n        fsFirstNationsStatus =@fsFirstNationsStatus,\r\n        fsInterpreterFundStatus=@fsInterpreterFundStatus,\r\n        fsOtherStatus=@fsOtherStatus,\r\n        fsssd=@fsssd,\r\n        fsssdstatus=@fsssdstatus\r\nWHERE   serviceproviderrequestdetailid=@id", parameters);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0002ABF8 File Offset: 0x00028DF8
		public void UpdateRequestDetailNotesAndSpecialInstructions(LegacyRequestDetailNotesAndSpecialInstructions notesAndSpecialInstructions)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@id", DbType.Int32, notesAndSpecialInstructions.RequestId),
				databaseLayer.GetParameter("@notes", DbType.Binary, databaseLayer.Encryption.Encrypt(notesAndSpecialInstructions.Notes ?? "")),
				databaseLayer.GetParameter("@si", DbType.Binary, databaseLayer.Encryption.Encrypt(notesAndSpecialInstructions.SpecialInstructions ?? ""))
			};
			databaseLayer.ExecuteNonQuery("UPDATE serviceproviderrequests SET notes=@notes,specialinstructions=@si WHERE serviceproviderrequestid=@id", parameters);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0002ACA0 File Offset: 0x00028EA0
		public void UpdateRequestNotes(int RequestId, string notes)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@id", DbType.Int32, RequestId),
				databaseLayer.GetParameter("@notes", DbType.Binary, databaseLayer.Encryption.Encrypt(notes ?? ""))
			};
			databaseLayer.ExecuteNonQuery("UPDATE serviceproviderrequests SET notes=@notes WHERE serviceproviderrequestid=@id", parameters);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0002AD18 File Offset: 0x00028F18
		public void UpdateProvider(ServiceProvider provider)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@id", DbType.Int32, provider.ServiceProviderId),
				databaseLayer.GetParameter("@firstname", DbType.Binary, encryption.Encrypt(provider.FirstName ?? "")),
				databaseLayer.GetParameter("@middlename", DbType.Binary, encryption.Encrypt(provider.MiddleName ?? "")),
				databaseLayer.GetParameter("@lastname", DbType.Binary, encryption.Encrypt(provider.LastName ?? "")),
				databaseLayer.GetParameter("@nickname", DbType.Binary, encryption.Encrypt(provider.NickName ?? "")),
				databaseLayer.GetParameter("@student_no", DbType.Binary, encryption.Encrypt(provider.StudentNumber ?? "")),
				databaseLayer.GetParameter("@altid", DbType.Binary, encryption.Encrypt(provider.Username ?? "")),
				databaseLayer.GetParameter("@additionalservices", DbType.Binary, encryption.Encrypt(provider.AdditionalServices ?? "")),
				databaseLayer.GetParameter("@specialization", DbType.Binary, encryption.Encrypt(provider.Specialization ?? "")),
				databaseLayer.GetParameter("@notes1", DbType.Binary, encryption.Encrypt(provider.Notes1 ?? "")),
				databaseLayer.GetParameter("@notes2", DbType.Binary, encryption.Encrypt(provider.Notes2 ?? "")),
				databaseLayer.GetParameter("@email", DbType.Binary, encryption.Encrypt(provider.Email ?? "")),
				databaseLayer.GetParameter("@phone1", DbType.Binary, encryption.Encrypt(provider.Phone1 ?? "")),
				databaseLayer.GetParameter("@phone2", DbType.Binary, encryption.Encrypt(provider.Phone2 ?? "")),
				databaseLayer.GetParameter("@phonenote", DbType.Binary, encryption.Encrypt(provider.PhoneNote ?? "")),
				databaseLayer.GetParameter("@address", DbType.Binary, encryption.Encrypt(provider.Address ?? "")),
				databaseLayer.GetParameter("@address2", DbType.Binary, encryption.Encrypt(provider.Address2 ?? "")),
				databaseLayer.GetParameter("@addressactive", DbType.Boolean, provider.AddressActive),
				databaseLayer.GetParameter("@address2active", DbType.Boolean, provider.Address2Active),
				databaseLayer.GetParameter("@email2", DbType.Binary, encryption.Encrypt(provider.Email2 ?? "")),
				databaseLayer.GetParameter("@registrationcomplete", DbType.Boolean, provider.RegistrationIsComplete)
			};
			databaseLayer.ExecuteNonQuery("UPDATE serviceproviders SET nickname=@nickname,registrationcomplete=@registrationcomplete,firstname=@firstname,middlename=@middlename,lastname=@lastname,student_no=@student_no,altid=@altid,additionalservices=@additionalservices,specialization=@specialization,notes1=@notes1,notes2=@notes2,email=@email,phone1=@phone1,phone2=@phone2,phonenote=@phonenote,address=@address,address2=@address2,email2=@email2,addressactive=@addressactive,address2active=@address2active WHERE serviceproviderid=@id", parameters);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0002B034 File Offset: 0x00029234
		public int CreateProvider(ServiceProvider provider)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@spid", DbType.Int32, 0),
				databaseLayer.GetParameter("@firstname", DbType.Binary, encryption.Encrypt(provider.FirstName ?? "")),
				databaseLayer.GetParameter("@middlename", DbType.Binary, encryption.Encrypt(provider.MiddleName ?? "")),
				databaseLayer.GetParameter("@lastname", DbType.Binary, encryption.Encrypt(provider.LastName ?? "")),
				databaseLayer.GetParameter("@nickname", DbType.Binary, encryption.Encrypt(provider.NickName ?? "")),
				databaseLayer.GetParameter("@student_no", DbType.Binary, encryption.Encrypt(provider.StudentNumber ?? "")),
				databaseLayer.GetParameter("@altid", DbType.Binary, encryption.Encrypt(provider.Username ?? "")),
				databaseLayer.GetParameter("@additionalservices", DbType.Binary, encryption.Encrypt(provider.AdditionalServices ?? "")),
				databaseLayer.GetParameter("@specialization", DbType.Binary, encryption.Encrypt(provider.Specialization ?? "")),
				databaseLayer.GetParameter("@notes1", DbType.Binary, encryption.Encrypt(provider.Notes1 ?? "")),
				databaseLayer.GetParameter("@notes2", DbType.Binary, encryption.Encrypt(provider.Notes2 ?? "")),
				databaseLayer.GetParameter("@email", DbType.Binary, encryption.Encrypt(provider.Email ?? "")),
				databaseLayer.GetParameter("@phone1", DbType.Binary, encryption.Encrypt(provider.Phone1 ?? "")),
				databaseLayer.GetParameter("@phone2", DbType.Binary, encryption.Encrypt(provider.Phone2 ?? "")),
				databaseLayer.GetParameter("@phonenote", DbType.Binary, encryption.Encrypt(provider.PhoneNote ?? "")),
				databaseLayer.GetParameter("@address", DbType.Binary, encryption.Encrypt(provider.Address ?? "")),
				databaseLayer.GetParameter("@address2", DbType.Binary, encryption.Encrypt(provider.Address2 ?? "")),
				databaseLayer.GetParameter("@addressactive", DbType.Boolean, provider.AddressActive),
				databaseLayer.GetParameter("@address2active", DbType.Boolean, provider.Address2Active),
				databaseLayer.GetParameter("@email2", DbType.Binary, encryption.Encrypt(provider.Email2 ?? "")),
				databaseLayer.GetParameter("@registrationcomplete", DbType.Boolean, provider.RegistrationIsComplete)
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO serviceproviders (firstname,middlename,lastname,student_no,altid,additionalservices,specialization,notes1,notes2,email,phone1,phone2,phonenote,address,address2,email2,addressactive,address2active,nickname,registrationcomplete) VALUES (@firstname,@middlename,@lastname,@student_no,@altid,@additionalservices,@specialization,@notes1,@notes2,@email,@phone1,@phone2,@phonenote,@address,@address2,@email2,@addressactive,@address2active,@nickname,@registrationcomplete)\r\nSET @spid=(SELECT TOP 1 CAST(@@identity AS int) AS serviceproviderid FROM serviceproviders)", array);
			return ((int?)array[0].Value).GetValueOrDefault();
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0002B364 File Offset: 0x00029564
		public ServiceProvider LoadProvider(int serviceProviderId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@id", DbType.Int32, serviceProviderId)
			};
			ServiceProvider result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT sp.serviceproviderid,sp.firstname,sp.middlename,sp.lastname,sp.student_no,sp.email\r\n        ,sp.phone1,sp.phone2,sp.specialization,sp.notes1,sp.notes2,sp.address\r\n        ,sp.address2,sp.email2,sp.addressactive,sp.address2active\r\n        ,spac.lucourseid,luc.startdate,luc.enddate,lucd.altlookupstring AS subject\r\n        ,luc.subjectid,luc.course,luc.timeofday,luc.section,tt.* \r\n        ,sp.altid,sp.nickname,sp.registrationcomplete\r\nFROM serviceproviders sp LEFT JOIN serviceproviderapplications spa ON spa.serviceproviderid=sp.serviceproviderid \r\n        LEFT JOIN serviceproviderapplicationcourses spac ON spac.serviceproviderapplicationid=spa.serviceproviderapplicationid \r\n        LEFT JOIN lucourses luc ON luc.lucourseid=spac.lucourseid \r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n        LEFT JOIN timetable tt ON tt.lucourseid=spac.lucourseid \r\nWHERE sp.serviceproviderid=@id", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetProviderFromRecord(dataReader, batchDecryptor);
				}
			}
			return result;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0002B408 File Offset: 0x00029608
		public int LoadProviderIdByStudentNumber(string snum)
		{
			string text = (snum ?? "").Trim();
			bool flag = text.Length < 1;
			if (flag)
			{
				throw new InvalidParameterException();
			}
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@sne", DbType.Binary, databaseLayer.Encryption.Encrypt(snum ?? ""))
			};
			object obj = databaseLayer.ExecuteScalar("SELECT TOP 1 serviceproviderid FROM serviceproviders WHERE student_no=@sne", parameters);
			return (obj as int?).GetValueOrDefault();
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0002B4A8 File Offset: 0x000296A8
		private ServiceProvider GetProviderFromRecord(IDataRecord record, IBatchDecryptor bd)
		{
			bool flag = record == null;
			ServiceProvider result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new ServiceProvider
				{
					StudentNumber = ((record["student_no"] is DBNull) ? "" : (bd.Decrypt((byte[])record["student_no"]) ?? "").Trim()),
					FirstName = ((record["firstname"] is DBNull) ? "" : (bd.Decrypt((byte[])record["firstname"]) ?? "").Trim()),
					MiddleName = ((record["middlename"] is DBNull) ? "" : (bd.Decrypt((byte[])record["middlename"]) ?? "").Trim()),
					LastName = ((record["lastname"] is DBNull) ? "" : (bd.Decrypt((byte[])record["lastname"]) ?? "").Trim()),
					NickName = ((record["nickname"] is DBNull) ? "" : (bd.Decrypt((byte[])record["nickname"]) ?? "").Trim()),
					Email = ((record["email"] is DBNull) ? "" : (bd.Decrypt((byte[])record["email"]) ?? "").Trim()),
					Phone1 = ((record["phone1"] is DBNull) ? "" : (bd.Decrypt((byte[])record["phone1"]) ?? "").Trim()),
					Specialization = ((record["specialization"] is DBNull) ? "" : (bd.Decrypt((byte[])record["specialization"]) ?? "").Trim()),
					Phone2 = ((record["phone2"] is DBNull) ? "" : (bd.Decrypt((byte[])record["phone2"]) ?? "").Trim()),
					Address = ((record["address"] is DBNull) ? "" : (bd.Decrypt((byte[])record["address"]) ?? "").Trim()),
					Address2 = ((record["address2"] is DBNull) ? "" : (bd.Decrypt((byte[])record["address2"]) ?? "").Trim()),
					Email2 = ((record["email2"] is DBNull) ? "" : (bd.Decrypt((byte[])record["email2"]) ?? "").Trim()),
					AddressActive = (record["addressactive"] != DBNull.Value && Convert.ToBoolean(record["addressactive"])),
					Address2Active = (record["address2active"] != DBNull.Value && Convert.ToBoolean(record["address2active"])),
					Notes1 = ((record["notes1"] is DBNull) ? "" : (bd.Decrypt((byte[])record["notes1"]) ?? "").Trim()),
					Notes2 = ((record["notes2"] is DBNull) ? "" : (bd.Decrypt((byte[])record["notes2"]) ?? "").Trim()),
					Username = ((record["altid"] is DBNull) ? "" : (bd.Decrypt((byte[])record["altid"]) ?? "").Trim()),
					RegistrationIsComplete = (record["registrationcomplete"] != DBNull.Value && Convert.ToBoolean(record["registrationcomplete"]))
				};
			}
			return result;
		}
	}
}
