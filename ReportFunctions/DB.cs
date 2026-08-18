using System;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ReportFunctions
{
	// Token: 0x02000018 RID: 24
	public class DB
	{
		// Token: 0x06000221 RID: 545 RVA: 0x00036C4C File Offset: 0x00035C4C
		public override string ToString()
		{
			return this.dbDescription;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00036C64 File Offset: 0x00035C64
		public string DbDescription
		{
			get
			{
				return this.dbDescription;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00036C7C File Offset: 0x00035C7C
		public string DbConnectionString
		{
			get
			{
				return this.dbConnectionString;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00036C94 File Offset: 0x00035C94
		public string DbPassword
		{
			get
			{
				return this.dbPassword;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00036CAC File Offset: 0x00035CAC
		public UnivDataAdapter Da
		{
			get
			{
				return this.da;
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00036CC4 File Offset: 0x00035CC4
		public DB(string _dbDescription, string _dbConnectionString, string _dbPassword, bool use192bit)
		{
			this.dbDescription = _dbDescription;
			this.dbConnectionString = _dbConnectionString;
			this.dbPassword = _dbPassword;
			this.da = null;
			this.tripleDES = null;
			this.use192bit = use192bit;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00036CFC File Offset: 0x00035CFC
		public DB(string dbDescription, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			this.dbDescription = dbDescription;
			this.da = da;
			this.tripleDES = tripleDES;
			this.use192bit = (tripleDES.encryptionType == EncryptionType.TripleDES_192bit);
			this.dbPassword = "";
			this.dbConnectionString = da.Connection.ConnectionString;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00036D54 File Offset: 0x00035D54
		public void CreateConnection(out string errMsg)
		{
			try
			{
				this.mainConnection = UnivOleDbFactory.CreateConnection(this.dbConnectionString);
				this.da = this.mainConnection.CreateDataAdapter();
				errMsg = null;
			}
			catch (Exception ex)
			{
				this.da = null;
				this.mainConnection = null;
				errMsg = ex.ToString();
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00036DB8 File Offset: 0x00035DB8
		public TripleDESEncryptionClass TripleDES
		{
			get
			{
				if (this.tripleDES == null)
				{
					if (this.dbPassword != null && this.dbPassword.Length > 0)
					{
						byte[][] bytes = TripleDESEncryptionClass.GetBytes(this.use192bit, this.dbPassword);
						byte[] key = bytes[0];
						byte[] iv = bytes[1];
						this.tripleDES = new TripleDESEncryptionClass(key, iv);
					}
				}
				return this.tripleDES;
			}
		}

		// Token: 0x040000FB RID: 251
		private string dbDescription;

		// Token: 0x040000FC RID: 252
		private string dbConnectionString;

		// Token: 0x040000FD RID: 253
		private string dbPassword;

		// Token: 0x040000FE RID: 254
		private UnivDataAdapter da;

		// Token: 0x040000FF RID: 255
		private UnivConnection mainConnection;

		// Token: 0x04000100 RID: 256
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x04000101 RID: 257
		private bool use192bit;
	}
}
