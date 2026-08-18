using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x02000013 RID: 19
	public class ServiceProviderOriginalProviderDAO : IServiceProviderOriginalProviderDAO, IBaseOperationContext<ServiceProvidersOperationContext>
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003198 File Offset: 0x00001398
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000031A0 File Offset: 0x000013A0
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x06000064 RID: 100 RVA: 0x000031A9 File Offset: 0x000013A9
		public ServiceProviderOriginalProviderDAO(ServiceProvidersOperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			ServiceProvidersOperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000031DA File Offset: 0x000013DA
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000031E2 File Offset: 0x000013E2
		public ServiceProvidersOperationContext OpContext { get; set; }

		// Token: 0x06000067 RID: 103 RVA: 0x000031EC File Offset: 0x000013EC
		public T GetServiceProviderBaseFromRecord<T>(IDataReader record, IBatchDecryptor batchDecryptor = null) where T : ServiceProviderBase
		{
			bool flag = record == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				int num = (record["serviceproviderid"] is DBNull) ? 0 : ((int)record["serviceproviderid"]);
				bool flag2 = num < 1;
				if (flag2)
				{
					result = default(T);
				}
				else
				{
					IBatchDecryptor batchDecryptor2 = batchDecryptor;
					if (batchDecryptor == null)
					{
						eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
						ServiceProvidersOperationContext opContext = this.OpContext;
						batchDecryptor2 = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption.GetBatchDecryptor();
					}
					IBatchDecryptor decryptor = batchDecryptor2;
					T t = Activator.CreateInstance<T>();
					t.ServiceProviderId = num;
					t.FirstName = record.DecryptString(decryptor, "firstname");
					t.MiddleName = record.DecryptString(decryptor, "middlename");
					t.LastName = record.DecryptString(decryptor, "lastname");
					t.StudentNumber = record.DecryptString(decryptor, "student_no");
					t.Username = record.DecryptString(decryptor, "altid");
					t.Email = (record.ContainsColumn("email") ? record.DecryptString(decryptor, "email") : string.Empty);
					t.RegistrationIsComplete = (record["registrationcomplete"] != DBNull.Value && (bool)record["registrationcomplete"]);
					result = t;
				}
			}
			return result;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000336C File Offset: 0x0000156C
		public ServiceProvider GetServiceProviderFromRecord(IDataReader record, IBatchDecryptor batchDecryptor = null)
		{
			bool flag = record == null;
			ServiceProvider result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (record["serviceproviderid"] is DBNull) ? 0 : ((int)record["serviceproviderid"]);
				bool flag2 = num < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					IEncryption encryption = this.DatabaseManager.Encryption;
					IBatchDecryptor decryptor = batchDecryptor ?? encryption.GetBatchDecryptor();
					ServiceProvider serviceProviderBaseFromRecord = this.GetServiceProviderBaseFromRecord<ServiceProvider>(record, null);
					bool flag3 = serviceProviderBaseFromRecord == null;
					if (flag3)
					{
						result = null;
					}
					else
					{
						serviceProviderBaseFromRecord.AdditionalServices = record.DecryptString(decryptor, "additionalservices");
						serviceProviderBaseFromRecord.Specialization = record.DecryptString(decryptor, "specialization");
						serviceProviderBaseFromRecord.Notes1 = record.DecryptString(decryptor, "notes1");
						serviceProviderBaseFromRecord.Notes2 = record.DecryptString(decryptor, "notes2");
						serviceProviderBaseFromRecord.Email = record.DecryptString(decryptor, "email");
						serviceProviderBaseFromRecord.Phone1 = record.DecryptString(decryptor, "phone1");
						serviceProviderBaseFromRecord.Phone2 = record.DecryptString(decryptor, "phone2");
						serviceProviderBaseFromRecord.PhoneNote = record.DecryptString(decryptor, "phonenote");
						serviceProviderBaseFromRecord.Address = record.DecryptString(decryptor, "address");
						serviceProviderBaseFromRecord.Address2 = record.DecryptString(decryptor, "address2");
						serviceProviderBaseFromRecord.Address2Active = (!(record["address2active"] is DBNull) && Convert.ToBoolean(record["address2active"]));
						serviceProviderBaseFromRecord.AddressActive = (!(record["addressactive"] is DBNull) && Convert.ToBoolean(record["addressactive"]));
						serviceProviderBaseFromRecord.IsActive = (!(record["isactive"] is DBNull) && Convert.ToBoolean(record["isactive"]));
						serviceProviderBaseFromRecord.IsActiveNote = ((record["isactivenote"] is DBNull) ? "" : encryption.Decrypt((byte[])record["isactivenote"]));
						serviceProviderBaseFromRecord.DateEntered = ((record["dateentered"] is DBNull) ? DateTime.Now : ((DateTime)record["dateentered"]));
						serviceProviderBaseFromRecord.WhoEnteredPersonId = ((record["whoentered"] is DBNull) ? 0 : ((int)record["whoentered"]));
						serviceProviderBaseFromRecord.Email2 = record.DecryptString(decryptor, "email2");
						result = serviceProviderBaseFromRecord;
					}
				}
			}
			return result;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000035E8 File Offset: 0x000017E8
		public ServiceProvider LoadProviderById(int ServiceProviderId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@spid", DbType.Int32, ServiceProviderId)
			};
			IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
			ServiceProvider result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    sp.[ServiceProviderId],sp.[firstname],sp.[middlename],sp.[lastname],sp.[student_no],sp.[altid],sp.[additionalservices],sp.[specialization],\r\n            sp.[notes1],sp.[notes2],sp.[email],sp.[phone1],sp.[phone2],sp.[phonenote],sp.[address],sp.[dateentered],sp.[whoentered],sp.[isactive],\r\n            sp.[isactivenote],sp.[address2],sp.[email2],sp.[addressactive],sp.[address2active],sp.[RegistrationComplete]\r\nFROM        ServiceProviders sp\r\nWHERE       sp.serviceproviderid=@spid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetServiceProviderFromRecord(dataReader, batchDecryptor);
				}
			}
			return result;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003680 File Offset: 0x00001880
		public ServiceProvider LoadProviderByStudentNumber(string StudentNumber)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@snum", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(StudentNumber))
			};
			IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
			ServiceProvider result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    sp.[ServiceProviderId],sp.[firstname],sp.[middlename],sp.[lastname],sp.[student_no],sp.[altid],sp.[additionalservices],sp.[specialization],\r\n            sp.[notes1],sp.[notes2],sp.[email],sp.[phone1],sp.[phone2],sp.[phonenote],sp.[address],sp.[dateentered],sp.[whoentered],sp.[isactive],\r\n            sp.[isactivenote],sp.[address2],sp.[email2],sp.[addressactive],sp.[address2active],sp.[RegistrationComplete]\r\nFROM        ServiceProviders sp\r\nWHERE       sp.student_no=@snum", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetServiceProviderFromRecord(dataReader, batchDecryptor);
				}
			}
			return result;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003720 File Offset: 0x00001920
		public ServiceProvider LoadProviderByUsername(string Username)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@username", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(Username))
			};
			IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
			ServiceProvider result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    sp.[ServiceProviderId],sp.[firstname],sp.[middlename],sp.[lastname],sp.[student_no],sp.[altid],sp.[additionalservices],sp.[specialization],\r\n            sp.[notes1],sp.[notes2],sp.[email],sp.[phone1],sp.[phone2],sp.[phonenote],sp.[address],sp.[dateentered],sp.[whoentered],sp.[isactive],\r\n            sp.[isactivenote],sp.[address2],sp.[email2],sp.[addressactive],sp.[address2active],sp.[RegistrationComplete]\r\nFROM        ServiceProviders sp\r\nWHERE       sp.altid=@username", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetServiceProviderFromRecord(dataReader, batchDecryptor);
				}
			}
			return result;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000037C0 File Offset: 0x000019C0
		public ServiceProviderBase LoadProviderBaseById(int ServiceProviderId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@spid", DbType.Int32, ServiceProviderId)
			};
			IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
			ServiceProviderBase result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    sp.[ServiceProviderId],sp.[firstname],sp.[middlename],sp.[lastname],sp.[student_no],sp.[altid],sp.[additionalservices],sp.[specialization],\r\n            sp.[notes1],sp.[notes2],sp.[email],sp.[phone1],sp.[phone2],sp.[phonenote],sp.[address],sp.[dateentered],sp.[whoentered],sp.[isactive],\r\n            sp.[isactivenote],sp.[address2],sp.[email2],sp.[addressactive],sp.[address2active],sp.[RegistrationComplete]\r\nFROM        ServiceProviders sp\r\nWHERE       sp.serviceproviderid=@spid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetServiceProviderBaseFromRecord<ServiceProviderBase>(dataReader, batchDecryptor);
				}
			}
			return result;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003858 File Offset: 0x00001A58
		public ServiceProviderBase LoadProviderBaseByStudentNumber(string StudentNumber)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@snum", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(StudentNumber))
			};
			IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
			ServiceProviderBase result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    sp.[ServiceProviderId],sp.[firstname],sp.[middlename],sp.[lastname],sp.[student_no],sp.[altid],sp.[additionalservices],sp.[specialization],\r\n            sp.[notes1],sp.[notes2],sp.[email],sp.[phone1],sp.[phone2],sp.[phonenote],sp.[address],sp.[dateentered],sp.[whoentered],sp.[isactive],\r\n            sp.[isactivenote],sp.[address2],sp.[email2],sp.[addressactive],sp.[address2active],sp.[RegistrationComplete]\r\nFROM        ServiceProviders sp\r\nWHERE       sp.student_no=@snum", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetServiceProviderBaseFromRecord<ServiceProviderBase>(dataReader, batchDecryptor);
				}
			}
			return result;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000038F8 File Offset: 0x00001AF8
		public ServiceProviderBase LoadProviderBaseByUsername(string Username)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@username", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(Username))
			};
			IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
			ServiceProviderBase result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    sp.[ServiceProviderId],sp.[firstname],sp.[middlename],sp.[lastname],sp.[student_no],sp.[altid],sp.[additionalservices],sp.[specialization],\r\n            sp.[notes1],sp.[notes2],sp.[email],sp.[phone1],sp.[phone2],sp.[phonenote],sp.[address],sp.[dateentered],sp.[whoentered],sp.[isactive],\r\n            sp.[isactivenote],sp.[address2],sp.[email2],sp.[addressactive],sp.[address2active],sp.[RegistrationComplete]\r\nFROM        ServiceProviders sp\r\nWHERE       sp.altid=@username", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetServiceProviderBaseFromRecord<ServiceProviderBase>(dataReader, batchDecryptor);
				}
			}
			return result;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003998 File Offset: 0x00001B98
		public IList<ServiceProvider> LoadProvidersByProviderTypeAndDate(int ServiceProviderTypeId, DateTime StartDate, DateTime EndDate)
		{
			throw new NotImplementedException();
		}
	}
}
