using System;
using System.Data;
using System.Data.Common;
using ClockWorkWebAPI;
using Databases;
using EncryptionClassLibrary;

namespace ClockWorkController
{
	// Token: 0x0200000D RID: 13
	public class ServiceProvider
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00004820 File Offset: 0x00002A20
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00004838 File Offset: 0x00002A38
		public int ServiceProviderId
		{
			get
			{
				return this.serviceProviderId;
			}
			set
			{
				this.serviceProviderId = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00004844 File Offset: 0x00002A44
		// (set) Token: 0x06000053 RID: 83 RVA: 0x0000485C File Offset: 0x00002A5C
		public string FirstName
		{
			get
			{
				return this.firstname;
			}
			set
			{
				this.firstname = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00004868 File Offset: 0x00002A68
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00004880 File Offset: 0x00002A80
		public string LastName
		{
			get
			{
				return this.lastname;
			}
			set
			{
				this.lastname = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000056 RID: 86 RVA: 0x0000488C File Offset: 0x00002A8C
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000048A4 File Offset: 0x00002AA4
		public string Student_no
		{
			get
			{
				return this.student_no;
			}
			set
			{
				this.student_no = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000048B0 File Offset: 0x00002AB0
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000048C8 File Offset: 0x00002AC8
		public string Email
		{
			get
			{
				return this.email;
			}
			set
			{
				this.email = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000048D4 File Offset: 0x00002AD4
		// (set) Token: 0x0600005B RID: 91 RVA: 0x000048EC File Offset: 0x00002AEC
		public string Address
		{
			get
			{
				return this.address;
			}
			set
			{
				this.address = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000048F8 File Offset: 0x00002AF8
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00004910 File Offset: 0x00002B10
		public string PermanentAddress
		{
			get
			{
				return this.permanentAddress;
			}
			set
			{
				this.permanentAddress = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000491C File Offset: 0x00002B1C
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00004934 File Offset: 0x00002B34
		public string Phone1
		{
			get
			{
				return this.phone1;
			}
			set
			{
				this.phone1 = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00004940 File Offset: 0x00002B40
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00004958 File Offset: 0x00002B58
		public string Phone2
		{
			get
			{
				return this.phone2;
			}
			set
			{
				this.phone2 = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00004964 File Offset: 0x00002B64
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000497C File Offset: 0x00002B7C
		public string AltId
		{
			get
			{
				return this.altId;
			}
			set
			{
				this.altId = value;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004988 File Offset: 0x00002B88
		public ServiceProvider()
		{
			this.serviceProviderId = 0;
			this.firstname = "";
			this.lastname = "";
			this.student_no = "";
			this.email = "";
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004A08 File Offset: 0x00002C08
		public ServiceProvider(int serviceProviderId, string firstname, string lastname, string student_no, string email)
		{
			this.serviceProviderId = serviceProviderId;
			this.firstname = firstname;
			this.lastname = lastname;
			this.student_no = student_no;
			this.email = email;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004A7C File Offset: 0x00002C7C
		public static ServiceProvider LoadServiceProvider(int spid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@id", DbType.Int32, spid)
			};
			DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_ServiceProviderById, parameters);
			dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"firstname",
				"lastname",
				"student_no",
				"email"
			});
			bool flag = dataTable.Rows.Count > 0;
			ServiceProvider result;
			if (flag)
			{
				DataRow dataRow = dataTable.Rows[0];
				ServiceProvider serviceProvider = new ServiceProvider(spid, dataRow["firstname"].ToString().Trim(), dataRow["lastname"].ToString().Trim(), dataRow["student_no"].ToString().Trim(), dataRow["email"].ToString().Trim());
				result = serviceProvider;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004B80 File Offset: 0x00002D80
		public static ServiceProvider LoadServiceProvider(string student_no)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@snume", DbType.Binary, encryption.Encrypt(student_no))
			};
			DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_ServiceProviderByStudent_no2, parameters);
			dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"firstname",
				"lastname",
				"student_no",
				"email"
			});
			bool flag = dataTable.Rows.Count > 0;
			ServiceProvider result;
			if (flag)
			{
				DataRow dataRow = dataTable.Rows[0];
				ServiceProvider serviceProvider = new ServiceProvider((int)dataRow["serviceproviderid"], dataRow["firstname"].ToString().Trim(), dataRow["lastname"].ToString().Trim(), dataRow["student_no"].ToString().Trim(), dataRow["email"].ToString().Trim());
				result = serviceProvider;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004C98 File Offset: 0x00002E98
		public static ServiceProvider LoadNotetakerByUsername(string username)
		{
			bool flag = string.IsNullOrEmpty(username) || username.Trim().Length < 1;
			ServiceProvider result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
				IEncryption encryption = clockWork.Encryption;
				DbParameter[] parameters = new DbParameter[]
				{
					clockWork.GetParameter("@u1", DbType.Binary, encryption.Encrypt(username.ToUpper())),
					clockWork.GetParameter("@u2", DbType.Binary, encryption.Encrypt(username.ToLower()))
				};
				string query = "SELECT sp.serviceproviderid,sp.firstname,sp.lastname,sp.student_no,sp.email FROM serviceproviders sp WHERE (sp.altid=@u1 OR sp.altid=@u2) AND isactive=1";
				DataTable dataTable = clockWork.ExecuteQuery(query, parameters);
				dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
				{
					"firstname",
					"lastname",
					"student_no",
					"email"
				});
				bool flag2 = dataTable.Rows.Count > 0;
				if (flag2)
				{
					DataRow dataRow = dataTable.Rows[0];
					ServiceProvider serviceProvider = new ServiceProvider((int)dataRow["serviceproviderid"], dataRow["firstname"].ToString().Trim(), dataRow["lastname"].ToString().Trim(), dataRow["student_no"].ToString().Trim(), dataRow["email"].ToString().Trim());
					result = serviceProvider;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x04000013 RID: 19
		private int serviceProviderId;

		// Token: 0x04000014 RID: 20
		private string firstname;

		// Token: 0x04000015 RID: 21
		private string lastname;

		// Token: 0x04000016 RID: 22
		private string student_no;

		// Token: 0x04000017 RID: 23
		private string email;

		// Token: 0x04000018 RID: 24
		private string address = "";

		// Token: 0x04000019 RID: 25
		private string permanentAddress = "";

		// Token: 0x0400001A RID: 26
		private string phone1 = "";

		// Token: 0x0400001B RID: 27
		private string phone2 = "";

		// Token: 0x0400001C RID: 28
		private string altId = "";
	}
}
