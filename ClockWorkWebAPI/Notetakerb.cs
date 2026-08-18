using System;
using System.Data;
using System.Data.Common;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;

namespace ClockWorkWebAPI
{
	// Token: 0x0200001F RID: 31
	[Serializable]
	public class Notetakerb
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000D268 File Offset: 0x0000B468
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0000D280 File Offset: 0x0000B480
		public string FirstName
		{
			get
			{
				return this.firstName;
			}
			set
			{
				this.firstName = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000D28C File Offset: 0x0000B48C
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0000D2A4 File Offset: 0x0000B4A4
		public string MiddleName
		{
			get
			{
				return this.middleName;
			}
			set
			{
				this.middleName = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000D2B0 File Offset: 0x0000B4B0
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000D2C8 File Offset: 0x0000B4C8
		public string LastName
		{
			get
			{
				return this.lastName;
			}
			set
			{
				this.lastName = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000D2D4 File Offset: 0x0000B4D4
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x0000D2EC File Offset: 0x0000B4EC
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

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000D2F8 File Offset: 0x0000B4F8
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000D280 File Offset: 0x0000B480
		public string Email
		{
			get
			{
				return this.email;
			}
			set
			{
				this.firstName = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000D310 File Offset: 0x0000B510
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000D328 File Offset: 0x0000B528
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

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000D334 File Offset: 0x0000B534
		// (set) Token: 0x060001CC RID: 460 RVA: 0x0000D34C File Offset: 0x0000B54C
		public string PhoneHome
		{
			get
			{
				return this.phoneHome;
			}
			set
			{
				this.phoneHome = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000D358 File Offset: 0x0000B558
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000D370 File Offset: 0x0000B570
		public string PhoneCell
		{
			get
			{
				return this.phoneCell;
			}
			set
			{
				this.phoneCell = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000D37C File Offset: 0x0000B57C
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000D394 File Offset: 0x0000B594
		public string Address2
		{
			get
			{
				return this.address2;
			}
			set
			{
				this.address2 = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		public string Email2
		{
			get
			{
				return this.email2;
			}
			set
			{
				this.email2 = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000D3C4 File Offset: 0x0000B5C4
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000D3DC File Offset: 0x0000B5DC
		public bool AddressActive
		{
			get
			{
				return this.addressActive;
			}
			set
			{
				this.addressActive = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000D3E8 File Offset: 0x0000B5E8
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x0000D400 File Offset: 0x0000B600
		public bool Address2Active
		{
			get
			{
				return this.address2Active;
			}
			set
			{
				this.address2Active = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000D40C File Offset: 0x0000B60C
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0000D424 File Offset: 0x0000B624
		public int NotetakerId
		{
			get
			{
				return this.notetakerId;
			}
			set
			{
				this.notetakerId = value;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000D430 File Offset: 0x0000B630
		public Notetakerb(int notetakerId)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			DbParameter[] array = new DbParameter[]
			{
				clockWork.Parameter
			};
			array[0].ParameterName = "@nid";
			array[0].DbType = DbType.Int32;
			array[0].Value = notetakerId;
			DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_ServiceProviderInfo, array);
			dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"address2",
				"email2",
				"firstname",
				"lastname",
				"student_no",
				"email",
				"middlename",
				"address",
				"phone1",
				"phone2"
			});
			bool flag = dataTable.Rows.Count > 0;
			if (flag)
			{
				this.Init(dataTable.Rows[0]);
			}
			else
			{
				notetakerId = 0;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000D528 File Offset: 0x0000B728
		public Notetakerb(db conn, int notetakerId)
		{
			conn.Da.SelectCommand.CommandText = "SELECT sp.serviceproviderid,sp.firstname,sp.lastname,sp.middlename,sp.student_no,sp.email,sp.address,sp.phone1,sp.phone2 FROM serviceproviders sp WHERE sp.serviceproviderid=@nid";
			conn.Da.SelectCommand.Parameters.Clear();
			conn.Da.SelectCommand.Parameters.AddWithValue("@nid", notetakerId);
			DataTable dataTable = new DataTable();
			conn.Da.Fill(dataTable);
			dataTable = conn.TripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"firstname",
				"lastname",
				"student_no",
				"email",
				"middlename",
				"address",
				"phone1",
				"phone2"
			});
			bool flag = dataTable.Rows.Count > 0;
			if (flag)
			{
				this.Init(dataTable.Rows[0]);
			}
			else
			{
				notetakerId = 0;
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000D620 File Offset: 0x0000B820
		private void Init(DataRow dr)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			this.notetakerId = (int)dr["serviceproviderid"];
			this.firstName = dr["firstname"].ToString();
			this.middleName = dr["middlename"].ToString();
			this.lastName = dr["lastname"].ToString();
			this.email = dr["email"].ToString();
			this.address = dr["address"].ToString();
			this.phoneHome = dr["phone1"].ToString();
			this.phoneCell = dr["phone2"].ToString();
			this.student_no = dr["student_no"].ToString();
			this.address2 = dr["address2"].ToString();
			this.email2 = dr["email2"].ToString();
			this.addressActive = (dr["addressactive"] != DBNull.Value && Convert.ToBoolean(dr["addressactive"]));
			this.address2Active = (dr["address2active"] != DBNull.Value && Convert.ToBoolean(dr["address2active"]));
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000D784 File Offset: 0x0000B984
		public static void UpdateNotetakerAccount(int notetakerId, string fn, string mn, string ln, string sn, string email, string email2, string address, string address2, string phoneHome, string phoneCell, bool addressActive, bool address2Active)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@firstname", DbType.Binary, encryption.Encrypt(fn)),
				clockWork.GetParameter("@lastname", DbType.Binary, encryption.Encrypt(ln)),
				clockWork.GetParameter("@middlename", DbType.Binary, encryption.Encrypt(mn)),
				clockWork.GetParameter("@student_no", DbType.Binary, encryption.Encrypt(sn)),
				clockWork.GetParameter("@email", DbType.Binary, encryption.Encrypt(email)),
				clockWork.GetParameter("@address", DbType.Binary, encryption.Encrypt(address)),
				clockWork.GetParameter("@phone1", DbType.Binary, encryption.Encrypt(phoneHome)),
				clockWork.GetParameter("@phone2", DbType.Binary, encryption.Encrypt(phoneCell)),
				clockWork.GetParameter("@phonenote", DbType.Binary, encryption.Encrypt("")),
				clockWork.GetParameter("@address2", DbType.Binary, encryption.Encrypt(address2)),
				clockWork.GetParameter("@nid", DbType.Int32, notetakerId),
				clockWork.GetParameter("@addressactive", DbType.Boolean, addressActive),
				clockWork.GetParameter("@address2active", DbType.Boolean, address2Active),
				clockWork.GetParameter("@email2", DbType.Binary, encryption.Encrypt(email2))
			};
			clockWork.ExecuteNonQuery(QueryStorage.QS_UPDATE_ServiceProvider, parameters);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000D8F8 File Offset: 0x0000BAF8
		public static int CreateNotetakerAccount(string altId, string fn, string mn, string ln, string sn, string email, string email2, string address, string address2, string phoneHome, string phoneCell, bool addressActive, bool address2Active)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@firstname", DbType.Binary, encryption.Encrypt(fn ?? "")),
				clockWork.GetParameter("@lastname", DbType.Binary, encryption.Encrypt(ln ?? "")),
				clockWork.GetParameter("@middlename", DbType.Binary, encryption.Encrypt(mn ?? "")),
				clockWork.GetParameter("@student_no", DbType.Binary, encryption.Encrypt(sn ?? "")),
				clockWork.GetParameter("@email", DbType.Binary, encryption.Encrypt(email ?? "")),
				clockWork.GetParameter("@address", DbType.Binary, encryption.Encrypt(address ?? "")),
				clockWork.GetParameter("@phone1", DbType.Binary, encryption.Encrypt(phoneHome ?? "")),
				clockWork.GetParameter("@phone2", DbType.Binary, encryption.Encrypt(phoneCell ?? "")),
				clockWork.GetParameter("@phonenote", DbType.Binary, encryption.Encrypt("")),
				clockWork.GetParameter("@address2", DbType.Binary, encryption.Encrypt(address2 ?? "")),
				clockWork.GetParameter("@addressactive", DbType.Boolean, addressActive),
				clockWork.GetParameter("@address2active", DbType.Boolean, address2Active),
				clockWork.GetParameter("@email2", DbType.Binary, encryption.Encrypt(email2 ?? "")),
				clockWork.GetParameter("@altid", DbType.Binary, encryption.Encrypt(altId ?? ""))
			};
			bool flag = !string.IsNullOrEmpty(altId);
			string query;
			if (flag)
			{
				query = "INSERT INTO serviceproviders \r\n    (altid,firstname,middlename,lastname,student_no,email,phone1,phone2,phonenote,address,address2,addressactive,address2active,email2) \r\nSELECT @altid,@firstname,@middlename,@lastname,@student_no,@email,@phone1,@phone2,@phonenote,@address,@address2,@addressactive,@address2active,@email2\r\n    WHERE NOT EXISTS(SELECT serviceproviderid FROM serviceproviders WHERE altid=@altid AND isactive=1); SELECT serviceproviderid FROM serviceproviders WHERE altid=@altid AND isactive=1";
			}
			else
			{
				query = "INSERT INTO serviceproviders \r\n    (firstname,middlename,lastname,student_no,email,phone1,phone2,phonenote,address,address2,addressactive,address2active,email2) \r\nSELECT @firstname,@middlename,@lastname,@student_no,@email,@phone1,@phone2,@phonenote,@address,@address2,@addressactive,@address2active,@email2\r\n    WHERE NOT EXISTS(SELECT serviceproviderid FROM serviceproviders WHERE student_no=@student_no AND isactive=1); \r\nSELECT serviceproviderid FROM serviceproviders WHERE student_no=@student_no AND isactive=1";
			}
			object obj = clockWork.ExecuteScalar(query, parameters);
			bool flag2 = obj != null;
			int result;
			if (flag2)
			{
				result = (int)obj;
			}
			else
			{
				result = 0;
			}
			CWLogger.Logger.Trace("ClockWorkWebAPI:Notetakerb:CreateNotetakerAccount:CreatedNotetakerAccount:newnid={0}:altid={1}:name={2}", result.ToString(), string.IsNullOrEmpty(altId) ? "NULLorEMPTY" : altId, (fn ?? "NULL") + " " + (ln ?? "NULL"));
			return result;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000DB54 File Offset: 0x0000BD54
		public static int CreateServiceProviderApplication(db conn, int serviceProviderId, int serviceProviderType)
		{
			return Notetakerb.CreateServiceProviderApplication(serviceProviderId, serviceProviderType);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000DB70 File Offset: 0x0000BD70
		public static int CreateServiceProviderApplication(int serviceProviderId, int serviceProviderType)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@spid", DbType.Int32, serviceProviderId),
				clockWork.GetParameter("@sptype", DbType.Int32, serviceProviderType)
			};
			DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_INSERT_NewServiceProviderApplication, parameters);
			bool flag = dataTable.Rows.Count > 0;
			int result;
			if (flag)
			{
				result = (int)dataTable.Rows[0][0];
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000DBFC File Offset: 0x0000BDFC
		public static int AddServiceProviderApplicationCourse(db conn, int serviceProviderId, int serviceProviderType, int serviceProviderApplicationId, int lucid)
		{
			return Notetakerb.AddServiceProviderApplicationCourse(serviceProviderId, serviceProviderType, serviceProviderApplicationId, lucid);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000DC18 File Offset: 0x0000BE18
		public static int AddServiceProviderApplicationCourse(int serviceProviderId, int serviceProviderType, int serviceProviderApplicationId, int lucid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] array = new DbParameter[]
			{
				clockWork.GetParameter("@spa", DbType.Int32, serviceProviderApplicationId),
				clockWork.GetParameter("@sptype", DbType.Int32, serviceProviderType),
				clockWork.GetParameter("@lucid", DbType.Int32, lucid)
			};
			DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_INSERT_NewServiceProviderApplicationCourse, array);
			bool flag = dataTable.Rows.Count > 0;
			int result;
			if (flag)
			{
				int num = (int)dataTable.Rows[0][0];
				array = new DbParameter[]
				{
					clockWork.Parameter
				};
				array[0].ParameterName = "@id";
				array[0].DbType = DbType.Int32;
				array[0].Value = num;
				clockWork.ExecuteNonQuery(QueryStorage.QS_Update_ApplicationCourseDateCancelled, array);
				result = num;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x0400008C RID: 140
		private string firstName;

		// Token: 0x0400008D RID: 141
		private string middleName;

		// Token: 0x0400008E RID: 142
		private string lastName;

		// Token: 0x0400008F RID: 143
		private string student_no;

		// Token: 0x04000090 RID: 144
		private int notetakerId;

		// Token: 0x04000091 RID: 145
		private string email;

		// Token: 0x04000092 RID: 146
		private string address;

		// Token: 0x04000093 RID: 147
		private string phoneHome;

		// Token: 0x04000094 RID: 148
		private string phoneCell;

		// Token: 0x04000095 RID: 149
		private string email2;

		// Token: 0x04000096 RID: 150
		private string address2;

		// Token: 0x04000097 RID: 151
		private bool addressActive;

		// Token: 0x04000098 RID: 152
		private bool address2Active;
	}
}
