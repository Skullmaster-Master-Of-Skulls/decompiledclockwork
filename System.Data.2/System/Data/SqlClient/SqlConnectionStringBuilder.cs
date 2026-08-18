using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data.Common;
using System.Globalization;
using System.Reflection;

namespace System.Data.SqlClient
{
	// Token: 0x020001BF RID: 447
	[DefaultProperty("DataSource")]
	[TypeConverter(typeof(SqlConnectionStringBuilder.SqlConnectionStringBuilderConverter))]
	public sealed class SqlConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x06001B23 RID: 6947 RVA: 0x000BF47C File Offset: 0x000BE87C
		static SqlConnectionStringBuilder()
		{
			string[] array = new string[38];
			array[31] = "ApplicationIntent";
			array[25] = "Application Name";
			array[13] = "Asynchronous Processing";
			array[2] = "AttachDbFilename";
			array[12] = "PoolBlockingPeriod";
			array[14] = "Connection Reset";
			array[29] = "Context Connection";
			array[17] = "Connect Timeout";
			array[26] = "Current Language";
			array[0] = "Data Source";
			array[18] = "Encrypt";
			array[8] = "Enlist";
			array[1] = "Failover Partner";
			array[3] = "Initial Catalog";
			array[4] = "Integrated Security";
			array[20] = "Load Balance Timeout";
			array[11] = "Max Pool Size";
			array[10] = "Min Pool Size";
			array[15] = "MultipleActiveResultSets";
			array[32] = "MultiSubnetFailover";
			array[33] = "TransparentNetworkIPResolution";
			array[21] = "Network Library";
			array[22] = "Packet Size";
			array[7] = "Password";
			array[5] = "Persist Security Info";
			array[9] = "Pooling";
			array[16] = "Replication";
			array[30] = "Transaction Binding";
			array[19] = "TrustServerCertificate";
			array[23] = "Type System Version";
			array[6] = "User ID";
			array[28] = "User Instance";
			array[27] = "Workstation ID";
			array[34] = "ConnectRetryCount";
			array[35] = "ConnectRetryInterval";
			array[24] = "Authentication";
			array[36] = "Column Encryption Setting";
			array[37] = "Enclave Attestation Url";
			SqlConnectionStringBuilder._validKeywords = array;
			SqlConnectionStringBuilder._keywords = new Dictionary<string, SqlConnectionStringBuilder.Keywords>(59, StringComparer.OrdinalIgnoreCase)
			{
				{
					"ApplicationIntent",
					SqlConnectionStringBuilder.Keywords.ApplicationIntent
				},
				{
					"Application Name",
					SqlConnectionStringBuilder.Keywords.ApplicationName
				},
				{
					"Asynchronous Processing",
					SqlConnectionStringBuilder.Keywords.AsynchronousProcessing
				},
				{
					"AttachDbFilename",
					SqlConnectionStringBuilder.Keywords.AttachDBFilename
				},
				{
					"PoolBlockingPeriod",
					SqlConnectionStringBuilder.Keywords.PoolBlockingPeriod
				},
				{
					"Connect Timeout",
					SqlConnectionStringBuilder.Keywords.ConnectTimeout
				},
				{
					"Connection Reset",
					SqlConnectionStringBuilder.Keywords.ConnectionReset
				},
				{
					"Context Connection",
					SqlConnectionStringBuilder.Keywords.ContextConnection
				},
				{
					"Current Language",
					SqlConnectionStringBuilder.Keywords.CurrentLanguage
				},
				{
					"Data Source",
					SqlConnectionStringBuilder.Keywords.DataSource
				},
				{
					"Encrypt",
					SqlConnectionStringBuilder.Keywords.Encrypt
				},
				{
					"Enlist",
					SqlConnectionStringBuilder.Keywords.Enlist
				},
				{
					"Failover Partner",
					SqlConnectionStringBuilder.Keywords.FailoverPartner
				},
				{
					"Initial Catalog",
					SqlConnectionStringBuilder.Keywords.InitialCatalog
				},
				{
					"Integrated Security",
					SqlConnectionStringBuilder.Keywords.IntegratedSecurity
				},
				{
					"Load Balance Timeout",
					SqlConnectionStringBuilder.Keywords.LoadBalanceTimeout
				},
				{
					"MultipleActiveResultSets",
					SqlConnectionStringBuilder.Keywords.MultipleActiveResultSets
				},
				{
					"Max Pool Size",
					SqlConnectionStringBuilder.Keywords.MaxPoolSize
				},
				{
					"Min Pool Size",
					SqlConnectionStringBuilder.Keywords.MinPoolSize
				},
				{
					"MultiSubnetFailover",
					SqlConnectionStringBuilder.Keywords.MultiSubnetFailover
				},
				{
					"TransparentNetworkIPResolution",
					SqlConnectionStringBuilder.Keywords.TransparentNetworkIPResolution
				},
				{
					"Network Library",
					SqlConnectionStringBuilder.Keywords.NetworkLibrary
				},
				{
					"Packet Size",
					SqlConnectionStringBuilder.Keywords.PacketSize
				},
				{
					"Password",
					SqlConnectionStringBuilder.Keywords.Password
				},
				{
					"Persist Security Info",
					SqlConnectionStringBuilder.Keywords.PersistSecurityInfo
				},
				{
					"Pooling",
					SqlConnectionStringBuilder.Keywords.Pooling
				},
				{
					"Replication",
					SqlConnectionStringBuilder.Keywords.Replication
				},
				{
					"Transaction Binding",
					SqlConnectionStringBuilder.Keywords.TransactionBinding
				},
				{
					"TrustServerCertificate",
					SqlConnectionStringBuilder.Keywords.TrustServerCertificate
				},
				{
					"Type System Version",
					SqlConnectionStringBuilder.Keywords.TypeSystemVersion
				},
				{
					"User ID",
					SqlConnectionStringBuilder.Keywords.UserID
				},
				{
					"User Instance",
					SqlConnectionStringBuilder.Keywords.UserInstance
				},
				{
					"Workstation ID",
					SqlConnectionStringBuilder.Keywords.WorkstationID
				},
				{
					"ConnectRetryCount",
					SqlConnectionStringBuilder.Keywords.ConnectRetryCount
				},
				{
					"ConnectRetryInterval",
					SqlConnectionStringBuilder.Keywords.ConnectRetryInterval
				},
				{
					"Authentication",
					SqlConnectionStringBuilder.Keywords.Authentication
				},
				{
					"Column Encryption Setting",
					SqlConnectionStringBuilder.Keywords.ColumnEncryptionSetting
				},
				{
					"Enclave Attestation Url",
					SqlConnectionStringBuilder.Keywords.EnclaveAttestationUrl
				},
				{
					"app",
					SqlConnectionStringBuilder.Keywords.ApplicationName
				},
				{
					"async",
					SqlConnectionStringBuilder.Keywords.AsynchronousProcessing
				},
				{
					"extended properties",
					SqlConnectionStringBuilder.Keywords.AttachDBFilename
				},
				{
					"initial file name",
					SqlConnectionStringBuilder.Keywords.AttachDBFilename
				},
				{
					"connection timeout",
					SqlConnectionStringBuilder.Keywords.ConnectTimeout
				},
				{
					"timeout",
					SqlConnectionStringBuilder.Keywords.ConnectTimeout
				},
				{
					"language",
					SqlConnectionStringBuilder.Keywords.CurrentLanguage
				},
				{
					"addr",
					SqlConnectionStringBuilder.Keywords.DataSource
				},
				{
					"address",
					SqlConnectionStringBuilder.Keywords.DataSource
				},
				{
					"network address",
					SqlConnectionStringBuilder.Keywords.DataSource
				},
				{
					"server",
					SqlConnectionStringBuilder.Keywords.DataSource
				},
				{
					"database",
					SqlConnectionStringBuilder.Keywords.InitialCatalog
				},
				{
					"trusted_connection",
					SqlConnectionStringBuilder.Keywords.IntegratedSecurity
				},
				{
					"connection lifetime",
					SqlConnectionStringBuilder.Keywords.LoadBalanceTimeout
				},
				{
					"net",
					SqlConnectionStringBuilder.Keywords.NetworkLibrary
				},
				{
					"network",
					SqlConnectionStringBuilder.Keywords.NetworkLibrary
				},
				{
					"pwd",
					SqlConnectionStringBuilder.Keywords.Password
				},
				{
					"persistsecurityinfo",
					SqlConnectionStringBuilder.Keywords.PersistSecurityInfo
				},
				{
					"uid",
					SqlConnectionStringBuilder.Keywords.UserID
				},
				{
					"user",
					SqlConnectionStringBuilder.Keywords.UserID
				},
				{
					"wsid",
					SqlConnectionStringBuilder.Keywords.WorkstationID
				}
			};
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x000BF8E4 File Offset: 0x000BECE4
		public SqlConnectionStringBuilder() : this(null)
		{
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x000BF8F8 File Offset: 0x000BECF8
		public SqlConnectionStringBuilder(string connectionString)
		{
			if (!ADP.IsEmpty(connectionString))
			{
				base.ConnectionString = connectionString;
			}
		}

		// Token: 0x17000410 RID: 1040
		public override object this[string keyword]
		{
			get
			{
				SqlConnectionStringBuilder.Keywords index = this.GetIndex(keyword);
				return this.GetAt(index);
			}
			set
			{
				if (value == null)
				{
					this.Remove(keyword);
					return;
				}
				switch (this.GetIndex(keyword))
				{
				case SqlConnectionStringBuilder.Keywords.DataSource:
					this.DataSource = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.FailoverPartner:
					this.FailoverPartner = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.AttachDBFilename:
					this.AttachDBFilename = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.InitialCatalog:
					this.InitialCatalog = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.IntegratedSecurity:
					this.IntegratedSecurity = SqlConnectionStringBuilder.ConvertToIntegratedSecurity(value);
					return;
				case SqlConnectionStringBuilder.Keywords.PersistSecurityInfo:
					this.PersistSecurityInfo = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.UserID:
					this.UserID = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.Password:
					this.Password = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.Enlist:
					this.Enlist = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.Pooling:
					this.Pooling = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.MinPoolSize:
					this.MinPoolSize = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.MaxPoolSize:
					this.MaxPoolSize = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.PoolBlockingPeriod:
					this.PoolBlockingPeriod = SqlConnectionStringBuilder.ConvertToPoolBlockingPeriod(keyword, value);
					return;
				case SqlConnectionStringBuilder.Keywords.AsynchronousProcessing:
					this.AsynchronousProcessing = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.ConnectionReset:
					this.ConnectionReset = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.MultipleActiveResultSets:
					this.MultipleActiveResultSets = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.Replication:
					this.Replication = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.ConnectTimeout:
					this.ConnectTimeout = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.Encrypt:
					this.Encrypt = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.TrustServerCertificate:
					this.TrustServerCertificate = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.LoadBalanceTimeout:
					this.LoadBalanceTimeout = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.NetworkLibrary:
					this.NetworkLibrary = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.PacketSize:
					this.PacketSize = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.TypeSystemVersion:
					this.TypeSystemVersion = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.Authentication:
					this.Authentication = SqlConnectionStringBuilder.ConvertToAuthenticationType(keyword, value);
					return;
				case SqlConnectionStringBuilder.Keywords.ApplicationName:
					this.ApplicationName = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.CurrentLanguage:
					this.CurrentLanguage = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.WorkstationID:
					this.WorkstationID = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.UserInstance:
					this.UserInstance = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.ContextConnection:
					this.ContextConnection = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.TransactionBinding:
					this.TransactionBinding = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				case SqlConnectionStringBuilder.Keywords.ApplicationIntent:
					this.ApplicationIntent = SqlConnectionStringBuilder.ConvertToApplicationIntent(keyword, value);
					return;
				case SqlConnectionStringBuilder.Keywords.MultiSubnetFailover:
					this.MultiSubnetFailover = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.TransparentNetworkIPResolution:
					this.TransparentNetworkIPResolution = SqlConnectionStringBuilder.ConvertToBoolean(value);
					return;
				case SqlConnectionStringBuilder.Keywords.ConnectRetryCount:
					this.ConnectRetryCount = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.ConnectRetryInterval:
					this.ConnectRetryInterval = SqlConnectionStringBuilder.ConvertToInt32(value);
					return;
				case SqlConnectionStringBuilder.Keywords.ColumnEncryptionSetting:
					this.ColumnEncryptionSetting = SqlConnectionStringBuilder.ConvertToColumnEncryptionSetting(keyword, value);
					return;
				case SqlConnectionStringBuilder.Keywords.EnclaveAttestationUrl:
					this.EnclaveAttestationUrl = SqlConnectionStringBuilder.ConvertToString(value);
					return;
				default:
					throw ADP.KeywordNotSupported(keyword);
				}
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001B28 RID: 6952 RVA: 0x000BFCE8 File Offset: 0x000BF0E8
		// (set) Token: 0x06001B29 RID: 6953 RVA: 0x000BFCFC File Offset: 0x000BF0FC
		[ResDescription("DbConnectionString_ApplicationIntent")]
		[DisplayName("ApplicationIntent")]
		[ResCategory("DataCategory_Initialization")]
		[RefreshProperties(RefreshProperties.All)]
		public ApplicationIntent ApplicationIntent
		{
			get
			{
				return this._applicationIntent;
			}
			set
			{
				if (!DbConnectionStringBuilderUtil.IsValidApplicationIntentValue(value))
				{
					throw ADP.InvalidEnumerationValue(typeof(ApplicationIntent), (int)value);
				}
				this.SetApplicationIntentValue(value);
				this._applicationIntent = value;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001B2A RID: 6954 RVA: 0x000BFD30 File Offset: 0x000BF130
		// (set) Token: 0x06001B2B RID: 6955 RVA: 0x000BFD44 File Offset: 0x000BF144
		[ResCategory("DataCategory_Context")]
		[DisplayName("Application Name")]
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DbConnectionString_ApplicationName")]
		public string ApplicationName
		{
			get
			{
				return this._applicationName;
			}
			set
			{
				this.SetValue("Application Name", value);
				this._applicationName = value;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x000BFD64 File Offset: 0x000BF164
		// (set) Token: 0x06001B2D RID: 6957 RVA: 0x000BFD78 File Offset: 0x000BF178
		[DisplayName("Asynchronous Processing")]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Initialization")]
		[ResDescription("DbConnectionString_AsynchronousProcessing")]
		public bool AsynchronousProcessing
		{
			get
			{
				return this._asynchronousProcessing;
			}
			set
			{
				this.SetValue("Asynchronous Processing", value);
				this._asynchronousProcessing = value;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001B2E RID: 6958 RVA: 0x000BFD98 File Offset: 0x000BF198
		// (set) Token: 0x06001B2F RID: 6959 RVA: 0x000BFDAC File Offset: 0x000BF1AC
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DbConnectionString_AttachDBFilename")]
		[DisplayName("AttachDbFilename")]
		[ResCategory("DataCategory_Source")]
		[Editor("System.Windows.Forms.Design.FileNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string AttachDBFilename
		{
			get
			{
				return this._attachDBFilename;
			}
			set
			{
				this.SetValue("AttachDbFilename", value);
				this._attachDBFilename = value;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001B30 RID: 6960 RVA: 0x000BFDCC File Offset: 0x000BF1CC
		// (set) Token: 0x06001B31 RID: 6961 RVA: 0x000BFDE0 File Offset: 0x000BF1E0
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("PoolBlockingPeriod")]
		[ResCategory("DataCategory_Pooling")]
		[ResDescription("DbConnectionString_PoolBlockingPeriod")]
		public PoolBlockingPeriod PoolBlockingPeriod
		{
			get
			{
				return this._poolBlockingPeriod;
			}
			set
			{
				if (!DbConnectionStringBuilderUtil.IsValidPoolBlockingPeriodValue(value))
				{
					throw ADP.InvalidEnumerationValue(typeof(PoolBlockingPeriod), (int)value);
				}
				this.SetPoolBlockingPeriodValue(value);
				this._poolBlockingPeriod = value;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x000BFE14 File Offset: 0x000BF214
		// (set) Token: 0x06001B33 RID: 6963 RVA: 0x000BFE28 File Offset: 0x000BF228
		[Obsolete("ConnectionReset has been deprecated.  SqlConnection will ignore the 'connection reset' keyword and always reset the connection")]
		[RefreshProperties(RefreshProperties.All)]
		[Browsable(false)]
		[DisplayName("Connection Reset")]
		[ResCategory("DataCategory_Pooling")]
		[ResDescription("DbConnectionString_ConnectionReset")]
		public bool ConnectionReset
		{
			get
			{
				return this._connectionReset;
			}
			set
			{
				this.SetValue("Connection Reset", value);
				this._connectionReset = value;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x000BFE48 File Offset: 0x000BF248
		// (set) Token: 0x06001B35 RID: 6965 RVA: 0x000BFE5C File Offset: 0x000BF25C
		[ResCategory("DataCategory_Source")]
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Context Connection")]
		[ResDescription("DbConnectionString_ContextConnection")]
		public bool ContextConnection
		{
			get
			{
				return this._contextConnection;
			}
			set
			{
				this.SetValue("Context Connection", value);
				this._contextConnection = value;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x000BFE7C File Offset: 0x000BF27C
		// (set) Token: 0x06001B37 RID: 6967 RVA: 0x000BFE90 File Offset: 0x000BF290
		[DisplayName("Connect Timeout")]
		[ResDescription("DbConnectionString_ConnectTimeout")]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Initialization")]
		public int ConnectTimeout
		{
			get
			{
				return this._connectTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw ADP.InvalidConnectionOptionValue("Connect Timeout");
				}
				this.SetValue("Connect Timeout", value);
				this._connectTimeout = value;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x000BFEC0 File Offset: 0x000BF2C0
		// (set) Token: 0x06001B39 RID: 6969 RVA: 0x000BFED4 File Offset: 0x000BF2D4
		[ResDescription("DbConnectionString_CurrentLanguage")]
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Current Language")]
		[ResCategory("DataCategory_Initialization")]
		public string CurrentLanguage
		{
			get
			{
				return this._currentLanguage;
			}
			set
			{
				this.SetValue("Current Language", value);
				this._currentLanguage = value;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x000BFEF4 File Offset: 0x000BF2F4
		// (set) Token: 0x06001B3B RID: 6971 RVA: 0x000BFF08 File Offset: 0x000BF308
		[ResDescription("DbConnectionString_DataSource")]
		[ResCategory("DataCategory_Source")]
		[TypeConverter(typeof(SqlConnectionStringBuilder.SqlDataSourceConverter))]
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Data Source")]
		public string DataSource
		{
			get
			{
				return this._dataSource;
			}
			set
			{
				this.SetValue("Data Source", value);
				this._dataSource = value;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x000BFF28 File Offset: 0x000BF328
		// (set) Token: 0x06001B3D RID: 6973 RVA: 0x000BFF3C File Offset: 0x000BF33C
		[ResCategory("DataCategory_Security")]
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Encrypt")]
		[ResDescription("DbConnectionString_Encrypt")]
		public bool Encrypt
		{
			get
			{
				return this._encrypt;
			}
			set
			{
				this.SetValue("Encrypt", value);
				this._encrypt = value;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x000BFF5C File Offset: 0x000BF35C
		// (set) Token: 0x06001B3F RID: 6975 RVA: 0x000BFF70 File Offset: 0x000BF370
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Security")]
		[ResDescription("TCE_DbConnectionString_ColumnEncryptionSetting")]
		[DisplayName("Column Encryption Setting")]
		public SqlConnectionColumnEncryptionSetting ColumnEncryptionSetting
		{
			get
			{
				return this._columnEncryptionSetting;
			}
			set
			{
				if (!DbConnectionStringBuilderUtil.IsValidColumnEncryptionSetting(value))
				{
					throw ADP.InvalidEnumerationValue(typeof(SqlConnectionColumnEncryptionSetting), (int)value);
				}
				this.SetColumnEncryptionSettingValue(value);
				this._columnEncryptionSetting = value;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x000BFFA4 File Offset: 0x000BF3A4
		// (set) Token: 0x06001B41 RID: 6977 RVA: 0x000BFFB8 File Offset: 0x000BF3B8
		[ResDescription("TCE_DbConnectionString_EnclaveAttestationUrl")]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Security")]
		[DisplayName("Enclave Attestation Url")]
		public string EnclaveAttestationUrl
		{
			get
			{
				return this._enclaveAttestationUrl;
			}
			set
			{
				this.SetValue("Enclave Attestation Url", value);
				this._enclaveAttestationUrl = value;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001B42 RID: 6978 RVA: 0x000BFFD8 File Offset: 0x000BF3D8
		// (set) Token: 0x06001B43 RID: 6979 RVA: 0x000BFFEC File Offset: 0x000BF3EC
		[DisplayName("TrustServerCertificate")]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Security")]
		[ResDescription("DbConnectionString_TrustServerCertificate")]
		public bool TrustServerCertificate
		{
			get
			{
				return this._trustServerCertificate;
			}
			set
			{
				this.SetValue("TrustServerCertificate", value);
				this._trustServerCertificate = value;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001B44 RID: 6980 RVA: 0x000C000C File Offset: 0x000BF40C
		// (set) Token: 0x06001B45 RID: 6981 RVA: 0x000C0020 File Offset: 0x000BF420
		[ResDescription("DbConnectionString_Enlist")]
		[ResCategory("DataCategory_Pooling")]
		[DisplayName("Enlist")]
		[RefreshProperties(RefreshProperties.All)]
		public bool Enlist
		{
			get
			{
				return this._enlist;
			}
			set
			{
				this.SetValue("Enlist", value);
				this._enlist = value;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001B46 RID: 6982 RVA: 0x000C0040 File Offset: 0x000BF440
		// (set) Token: 0x06001B47 RID: 6983 RVA: 0x000C0054 File Offset: 0x000BF454
		[ResDescription("DbConnectionString_FailoverPartner")]
		[RefreshProperties(RefreshProperties.All)]
		[TypeConverter(typeof(SqlConnectionStringBuilder.SqlDataSourceConverter))]
		[ResCategory("DataCategory_Source")]
		[DisplayName("Failover Partner")]
		public string FailoverPartner
		{
			get
			{
				return this._failoverPartner;
			}
			set
			{
				this.SetValue("Failover Partner", value);
				this._failoverPartner = value;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x000C0074 File Offset: 0x000BF474
		// (set) Token: 0x06001B49 RID: 6985 RVA: 0x000C0088 File Offset: 0x000BF488
		[TypeConverter(typeof(SqlConnectionStringBuilder.SqlInitialCatalogConverter))]
		[ResDescription("DbConnectionString_InitialCatalog")]
		[DisplayName("Initial Catalog")]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Source")]
		public string InitialCatalog
		{
			get
			{
				return this._initialCatalog;
			}
			set
			{
				this.SetValue("Initial Catalog", value);
				this._initialCatalog = value;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001B4A RID: 6986 RVA: 0x000C00A8 File Offset: 0x000BF4A8
		// (set) Token: 0x06001B4B RID: 6987 RVA: 0x000C00BC File Offset: 0x000BF4BC
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DbConnectionString_IntegratedSecurity")]
		[DisplayName("Integrated Security")]
		[ResCategory("DataCategory_Security")]
		public bool IntegratedSecurity
		{
			get
			{
				return this._integratedSecurity;
			}
			set
			{
				this.SetValue("Integrated Security", value);
				this._integratedSecurity = value;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x000C00DC File Offset: 0x000BF4DC
		// (set) Token: 0x06001B4D RID: 6989 RVA: 0x000C00F0 File Offset: 0x000BF4F0
		[ResDescription("DbConnectionString_Authentication")]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Security")]
		[DisplayName("Authentication")]
		public SqlAuthenticationMethod Authentication
		{
			get
			{
				return this._authentication;
			}
			set
			{
				if (!DbConnectionStringBuilderUtil.IsValidAuthenticationTypeValue(value))
				{
					throw ADP.InvalidEnumerationValue(typeof(SqlAuthenticationMethod), (int)value);
				}
				this.SetAuthenticationValue(value);
				this._authentication = value;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x000C0124 File Offset: 0x000BF524
		// (set) Token: 0x06001B4F RID: 6991 RVA: 0x000C0138 File Offset: 0x000BF538
		[DisplayName("Load Balance Timeout")]
		[ResCategory("DataCategory_Pooling")]
		[ResDescription("DbConnectionString_LoadBalanceTimeout")]
		[RefreshProperties(RefreshProperties.All)]
		public int LoadBalanceTimeout
		{
			get
			{
				return this._loadBalanceTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw ADP.InvalidConnectionOptionValue("Load Balance Timeout");
				}
				this.SetValue("Load Balance Timeout", value);
				this._loadBalanceTimeout = value;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x000C0168 File Offset: 0x000BF568
		// (set) Token: 0x06001B51 RID: 6993 RVA: 0x000C017C File Offset: 0x000BF57C
		[DisplayName("Max Pool Size")]
		[ResCategory("DataCategory_Pooling")]
		[ResDescription("DbConnectionString_MaxPoolSize")]
		[RefreshProperties(RefreshProperties.All)]
		public int MaxPoolSize
		{
			get
			{
				return this._maxPoolSize;
			}
			set
			{
				if (value < 1)
				{
					throw ADP.InvalidConnectionOptionValue("Max Pool Size");
				}
				this.SetValue("Max Pool Size", value);
				this._maxPoolSize = value;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x000C01AC File Offset: 0x000BF5AC
		// (set) Token: 0x06001B53 RID: 6995 RVA: 0x000C01C0 File Offset: 0x000BF5C0
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("ConnectRetryCount")]
		[ResCategory("DataCategory_ConnectionResilency")]
		[ResDescription("DbConnectionString_ConnectRetryCount")]
		public int ConnectRetryCount
		{
			get
			{
				return this._connectRetryCount;
			}
			set
			{
				if (value < 0 || value > 255)
				{
					throw ADP.InvalidConnectionOptionValue("ConnectRetryCount");
				}
				this.SetValue("ConnectRetryCount", value);
				this._connectRetryCount = value;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x000C01F8 File Offset: 0x000BF5F8
		// (set) Token: 0x06001B55 RID: 6997 RVA: 0x000C020C File Offset: 0x000BF60C
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("ConnectRetryInterval")]
		[ResDescription("DbConnectionString_ConnectRetryInterval")]
		[ResCategory("DataCategory_ConnectionResilency")]
		public int ConnectRetryInterval
		{
			get
			{
				return this._connectRetryInterval;
			}
			set
			{
				if (value < 1 || value > 60)
				{
					throw ADP.InvalidConnectionOptionValue("ConnectRetryInterval");
				}
				this.SetValue("ConnectRetryInterval", value);
				this._connectRetryInterval = value;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001B56 RID: 6998 RVA: 0x000C0240 File Offset: 0x000BF640
		// (set) Token: 0x06001B57 RID: 6999 RVA: 0x000C0254 File Offset: 0x000BF654
		[ResCategory("DataCategory_Pooling")]
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Min Pool Size")]
		[ResDescription("DbConnectionString_MinPoolSize")]
		public int MinPoolSize
		{
			get
			{
				return this._minPoolSize;
			}
			set
			{
				if (value < 0)
				{
					throw ADP.InvalidConnectionOptionValue("Min Pool Size");
				}
				this.SetValue("Min Pool Size", value);
				this._minPoolSize = value;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x000C0284 File Offset: 0x000BF684
		// (set) Token: 0x06001B59 RID: 7001 RVA: 0x000C0298 File Offset: 0x000BF698
		[ResCategory("DataCategory_Advanced")]
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("MultipleActiveResultSets")]
		[ResDescription("DbConnectionString_MultipleActiveResultSets")]
		public bool MultipleActiveResultSets
		{
			get
			{
				return this._multipleActiveResultSets;
			}
			set
			{
				this.SetValue("MultipleActiveResultSets", value);
				this._multipleActiveResultSets = value;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x000C02B8 File Offset: 0x000BF6B8
		// (set) Token: 0x06001B5B RID: 7003 RVA: 0x000C02CC File Offset: 0x000BF6CC
		[ResDescription("DbConnectionString_MultiSubnetFailover")]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Source")]
		[DisplayName("MultiSubnetFailover")]
		public bool MultiSubnetFailover
		{
			get
			{
				return this._multiSubnetFailover;
			}
			set
			{
				this.SetValue("MultiSubnetFailover", value);
				this._multiSubnetFailover = value;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x000C02EC File Offset: 0x000BF6EC
		// (set) Token: 0x06001B5D RID: 7005 RVA: 0x000C0300 File Offset: 0x000BF700
		[DisplayName("TransparentNetworkIPResolution")]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Source")]
		[ResDescription("DbConnectionString_TransparentNetworkIPResolution")]
		public bool TransparentNetworkIPResolution
		{
			get
			{
				return this._transparentNetworkIPResolution;
			}
			set
			{
				this.SetValue("TransparentNetworkIPResolution", value);
				this._transparentNetworkIPResolution = value;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001B5E RID: 7006 RVA: 0x000C0320 File Offset: 0x000BF720
		// (set) Token: 0x06001B5F RID: 7007 RVA: 0x000C0334 File Offset: 0x000BF734
		[DisplayName("Network Library")]
		[ResDescription("DbConnectionString_NetworkLibrary")]
		[TypeConverter(typeof(SqlConnectionStringBuilder.NetworkLibraryConverter))]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Advanced")]
		public string NetworkLibrary
		{
			get
			{
				return this._networkLibrary;
			}
			set
			{
				if (value != null)
				{
					string text = value.Trim().ToLower(CultureInfo.InvariantCulture);
					uint num = <PrivateImplementationDetails><System_Data_netmodule>.ComputeStringHash(text);
					if (num <= 2020728718U)
					{
						if (num <= 994024157U)
						{
							if (num != 631123172U)
							{
								if (num == 994024157U)
								{
									if (text == "dbnmpntw")
									{
										value = "dbnmpntw";
										goto IL_170;
									}
								}
							}
							else if (text == "dbmssocn")
							{
								value = "dbmssocn";
								goto IL_170;
							}
						}
						else if (num != 1378252155U)
						{
							if (num == 2020728718U)
							{
								if (text == "dbmslpcn")
								{
									value = "dbmslpcn";
									goto IL_170;
								}
							}
						}
						else if (text == "dbmsadsn")
						{
							value = "dbmsadsn";
							goto IL_170;
						}
					}
					else if (num <= 2988123455U)
					{
						if (num != 2313954678U)
						{
							if (num == 2988123455U)
							{
								if (text == "dbmsgnet")
								{
									value = "dbmsgnet";
									goto IL_170;
								}
							}
						}
						else if (text == "dbmsvinn")
						{
							value = "dbmsvinn";
							goto IL_170;
						}
					}
					else if (num != 3296403786U)
					{
						if (num == 3357720472U)
						{
							if (text == "dbmsrpcn")
							{
								value = "dbmsrpcn";
								goto IL_170;
							}
						}
					}
					else if (text == "dbmsspxn")
					{
						value = "dbmsspxn";
						goto IL_170;
					}
					throw ADP.InvalidConnectionOptionValue("Network Library");
				}
				IL_170:
				this.SetValue("Network Library", value);
				this._networkLibrary = value;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x000C04C4 File Offset: 0x000BF8C4
		// (set) Token: 0x06001B61 RID: 7009 RVA: 0x000C04D8 File Offset: 0x000BF8D8
		[ResCategory("DataCategory_Advanced")]
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Packet Size")]
		[ResDescription("DbConnectionString_PacketSize")]
		public int PacketSize
		{
			get
			{
				return this._packetSize;
			}
			set
			{
				if (value < 512 || 32768 < value)
				{
					throw SQL.InvalidPacketSizeValue();
				}
				this.SetValue("Packet Size", value);
				this._packetSize = value;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001B62 RID: 7010 RVA: 0x000C0510 File Offset: 0x000BF910
		// (set) Token: 0x06001B63 RID: 7011 RVA: 0x000C0524 File Offset: 0x000BF924
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Password")]
		[PasswordPropertyText(true)]
		[ResCategory("DataCategory_Security")]
		[ResDescription("DbConnectionString_Password")]
		public string Password
		{
			get
			{
				return this._password;
			}
			set
			{
				this.SetValue("Password", value);
				this._password = value;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x000C0544 File Offset: 0x000BF944
		// (set) Token: 0x06001B65 RID: 7013 RVA: 0x000C0558 File Offset: 0x000BF958
		[ResCategory("DataCategory_Security")]
		[DisplayName("Persist Security Info")]
		[ResDescription("DbConnectionString_PersistSecurityInfo")]
		[RefreshProperties(RefreshProperties.All)]
		public bool PersistSecurityInfo
		{
			get
			{
				return this._persistSecurityInfo;
			}
			set
			{
				this.SetValue("Persist Security Info", value);
				this._persistSecurityInfo = value;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x000C0578 File Offset: 0x000BF978
		// (set) Token: 0x06001B67 RID: 7015 RVA: 0x000C058C File Offset: 0x000BF98C
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Pooling")]
		[ResCategory("DataCategory_Pooling")]
		[ResDescription("DbConnectionString_Pooling")]
		public bool Pooling
		{
			get
			{
				return this._pooling;
			}
			set
			{
				this.SetValue("Pooling", value);
				this._pooling = value;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x000C05AC File Offset: 0x000BF9AC
		// (set) Token: 0x06001B69 RID: 7017 RVA: 0x000C05C0 File Offset: 0x000BF9C0
		[ResCategory("DataCategory_Replication")]
		[DisplayName("Replication")]
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DbConnectionString_Replication")]
		public bool Replication
		{
			get
			{
				return this._replication;
			}
			set
			{
				this.SetValue("Replication", value);
				this._replication = value;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x000C05E0 File Offset: 0x000BF9E0
		// (set) Token: 0x06001B6B RID: 7019 RVA: 0x000C05F4 File Offset: 0x000BF9F4
		[DisplayName("Transaction Binding")]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Advanced")]
		[ResDescription("DbConnectionString_TransactionBinding")]
		public string TransactionBinding
		{
			get
			{
				return this._transactionBinding;
			}
			set
			{
				this.SetValue("Transaction Binding", value);
				this._transactionBinding = value;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x000C0614 File Offset: 0x000BFA14
		// (set) Token: 0x06001B6D RID: 7021 RVA: 0x000C0628 File Offset: 0x000BFA28
		[ResCategory("DataCategory_Advanced")]
		[ResDescription("DbConnectionString_TypeSystemVersion")]
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Type System Version")]
		public string TypeSystemVersion
		{
			get
			{
				return this._typeSystemVersion;
			}
			set
			{
				this.SetValue("Type System Version", value);
				this._typeSystemVersion = value;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x000C0648 File Offset: 0x000BFA48
		// (set) Token: 0x06001B6F RID: 7023 RVA: 0x000C065C File Offset: 0x000BFA5C
		[ResDescription("DbConnectionString_UserID")]
		[ResCategory("DataCategory_Security")]
		[DisplayName("User ID")]
		[RefreshProperties(RefreshProperties.All)]
		public string UserID
		{
			get
			{
				return this._userID;
			}
			set
			{
				this.SetValue("User ID", value);
				this._userID = value;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x000C067C File Offset: 0x000BFA7C
		// (set) Token: 0x06001B71 RID: 7025 RVA: 0x000C0690 File Offset: 0x000BFA90
		[DisplayName("User Instance")]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Source")]
		[ResDescription("DbConnectionString_UserInstance")]
		public bool UserInstance
		{
			get
			{
				return this._userInstance;
			}
			set
			{
				this.SetValue("User Instance", value);
				this._userInstance = value;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x000C06B0 File Offset: 0x000BFAB0
		// (set) Token: 0x06001B73 RID: 7027 RVA: 0x000C06C4 File Offset: 0x000BFAC4
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Workstation ID")]
		[ResCategory("DataCategory_Context")]
		[ResDescription("DbConnectionString_WorkstationID")]
		public string WorkstationID
		{
			get
			{
				return this._workstationID;
			}
			set
			{
				this.SetValue("Workstation ID", value);
				this._workstationID = value;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001B74 RID: 7028 RVA: 0x000C06E4 File Offset: 0x000BFAE4
		public override bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x000C06F4 File Offset: 0x000BFAF4
		public override ICollection Keys
		{
			get
			{
				return new ReadOnlyCollection<string>(SqlConnectionStringBuilder._validKeywords);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x000C070C File Offset: 0x000BFB0C
		public override ICollection Values
		{
			get
			{
				object[] array = new object[SqlConnectionStringBuilder._validKeywords.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.GetAt((SqlConnectionStringBuilder.Keywords)i);
				}
				return new ReadOnlyCollection<object>(array);
			}
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x000C0744 File Offset: 0x000BFB44
		public override void Clear()
		{
			base.Clear();
			for (int i = 0; i < SqlConnectionStringBuilder._validKeywords.Length; i++)
			{
				this.Reset((SqlConnectionStringBuilder.Keywords)i);
			}
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x000C0770 File Offset: 0x000BFB70
		public override bool ContainsKey(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			return SqlConnectionStringBuilder._keywords.ContainsKey(keyword);
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x000C0794 File Offset: 0x000BFB94
		private static bool ConvertToBoolean(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToBoolean(value);
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x000C07A8 File Offset: 0x000BFBA8
		private static int ConvertToInt32(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToInt32(value);
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x000C07BC File Offset: 0x000BFBBC
		private static bool ConvertToIntegratedSecurity(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToIntegratedSecurity(value);
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x000C07D0 File Offset: 0x000BFBD0
		private static string ConvertToString(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToString(value);
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x000C07E4 File Offset: 0x000BFBE4
		private static ApplicationIntent ConvertToApplicationIntent(string keyword, object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToApplicationIntent(keyword, value);
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x000C07F8 File Offset: 0x000BFBF8
		private static SqlAuthenticationMethod ConvertToAuthenticationType(string keyword, object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToAuthenticationType(keyword, value);
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x000C080C File Offset: 0x000BFC0C
		private static PoolBlockingPeriod ConvertToPoolBlockingPeriod(string keyword, object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToPoolBlockingPeriod(keyword, value);
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x000C0820 File Offset: 0x000BFC20
		private static SqlConnectionColumnEncryptionSetting ConvertToColumnEncryptionSetting(string keyword, object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToColumnEncryptionSetting(keyword, value);
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x000C0834 File Offset: 0x000BFC34
		internal override string ConvertValueToString(object value)
		{
			if (value is SqlAuthenticationMethod)
			{
				return DbConnectionStringBuilderUtil.AuthenticationTypeToString((SqlAuthenticationMethod)value);
			}
			return base.ConvertValueToString(value);
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x000C085C File Offset: 0x000BFC5C
		private object GetAt(SqlConnectionStringBuilder.Keywords index)
		{
			switch (index)
			{
			case SqlConnectionStringBuilder.Keywords.DataSource:
				return this.DataSource;
			case SqlConnectionStringBuilder.Keywords.FailoverPartner:
				return this.FailoverPartner;
			case SqlConnectionStringBuilder.Keywords.AttachDBFilename:
				return this.AttachDBFilename;
			case SqlConnectionStringBuilder.Keywords.InitialCatalog:
				return this.InitialCatalog;
			case SqlConnectionStringBuilder.Keywords.IntegratedSecurity:
				return this.IntegratedSecurity;
			case SqlConnectionStringBuilder.Keywords.PersistSecurityInfo:
				return this.PersistSecurityInfo;
			case SqlConnectionStringBuilder.Keywords.UserID:
				return this.UserID;
			case SqlConnectionStringBuilder.Keywords.Password:
				return this.Password;
			case SqlConnectionStringBuilder.Keywords.Enlist:
				return this.Enlist;
			case SqlConnectionStringBuilder.Keywords.Pooling:
				return this.Pooling;
			case SqlConnectionStringBuilder.Keywords.MinPoolSize:
				return this.MinPoolSize;
			case SqlConnectionStringBuilder.Keywords.MaxPoolSize:
				return this.MaxPoolSize;
			case SqlConnectionStringBuilder.Keywords.PoolBlockingPeriod:
				return this.PoolBlockingPeriod;
			case SqlConnectionStringBuilder.Keywords.AsynchronousProcessing:
				return this.AsynchronousProcessing;
			case SqlConnectionStringBuilder.Keywords.ConnectionReset:
				return this.ConnectionReset;
			case SqlConnectionStringBuilder.Keywords.MultipleActiveResultSets:
				return this.MultipleActiveResultSets;
			case SqlConnectionStringBuilder.Keywords.Replication:
				return this.Replication;
			case SqlConnectionStringBuilder.Keywords.ConnectTimeout:
				return this.ConnectTimeout;
			case SqlConnectionStringBuilder.Keywords.Encrypt:
				return this.Encrypt;
			case SqlConnectionStringBuilder.Keywords.TrustServerCertificate:
				return this.TrustServerCertificate;
			case SqlConnectionStringBuilder.Keywords.LoadBalanceTimeout:
				return this.LoadBalanceTimeout;
			case SqlConnectionStringBuilder.Keywords.NetworkLibrary:
				return this.NetworkLibrary;
			case SqlConnectionStringBuilder.Keywords.PacketSize:
				return this.PacketSize;
			case SqlConnectionStringBuilder.Keywords.TypeSystemVersion:
				return this.TypeSystemVersion;
			case SqlConnectionStringBuilder.Keywords.Authentication:
				return this.Authentication;
			case SqlConnectionStringBuilder.Keywords.ApplicationName:
				return this.ApplicationName;
			case SqlConnectionStringBuilder.Keywords.CurrentLanguage:
				return this.CurrentLanguage;
			case SqlConnectionStringBuilder.Keywords.WorkstationID:
				return this.WorkstationID;
			case SqlConnectionStringBuilder.Keywords.UserInstance:
				return this.UserInstance;
			case SqlConnectionStringBuilder.Keywords.ContextConnection:
				return this.ContextConnection;
			case SqlConnectionStringBuilder.Keywords.TransactionBinding:
				return this.TransactionBinding;
			case SqlConnectionStringBuilder.Keywords.ApplicationIntent:
				return this.ApplicationIntent;
			case SqlConnectionStringBuilder.Keywords.MultiSubnetFailover:
				return this.MultiSubnetFailover;
			case SqlConnectionStringBuilder.Keywords.TransparentNetworkIPResolution:
				return this.TransparentNetworkIPResolution;
			case SqlConnectionStringBuilder.Keywords.ConnectRetryCount:
				return this.ConnectRetryCount;
			case SqlConnectionStringBuilder.Keywords.ConnectRetryInterval:
				return this.ConnectRetryInterval;
			case SqlConnectionStringBuilder.Keywords.ColumnEncryptionSetting:
				return this.ColumnEncryptionSetting;
			case SqlConnectionStringBuilder.Keywords.EnclaveAttestationUrl:
				return this.EnclaveAttestationUrl;
			default:
				throw ADP.KeywordNotSupported(SqlConnectionStringBuilder._validKeywords[(int)index]);
			}
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x000C0AA0 File Offset: 0x000BFEA0
		private SqlConnectionStringBuilder.Keywords GetIndex(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			SqlConnectionStringBuilder.Keywords result;
			if (SqlConnectionStringBuilder._keywords.TryGetValue(keyword, out result))
			{
				return result;
			}
			throw ADP.KeywordNotSupported(keyword);
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x000C0AD0 File Offset: 0x000BFED0
		protected override void GetProperties(Hashtable propertyDescriptors)
		{
			foreach (object obj in TypeDescriptor.GetProperties(this, true))
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				bool refreshOnChange = false;
				string displayName = propertyDescriptor.DisplayName;
				bool isReadOnly;
				if ("Integrated Security" == displayName)
				{
					refreshOnChange = true;
					isReadOnly = propertyDescriptor.IsReadOnly;
				}
				else
				{
					if (!("Password" == displayName) && !("User ID" == displayName))
					{
						continue;
					}
					isReadOnly = this.IntegratedSecurity;
				}
				Attribute[] attributesFromCollection = base.GetAttributesFromCollection(propertyDescriptor.Attributes);
				propertyDescriptors[displayName] = new DbConnectionStringBuilderDescriptor(propertyDescriptor.Name, propertyDescriptor.ComponentType, propertyDescriptor.PropertyType, isReadOnly, attributesFromCollection)
				{
					RefreshOnChange = refreshOnChange
				};
			}
			base.GetProperties(propertyDescriptors);
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x000C0BC4 File Offset: 0x000BFFC4
		public override bool Remove(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			SqlConnectionStringBuilder.Keywords keywords;
			if (SqlConnectionStringBuilder._keywords.TryGetValue(keyword, out keywords) && base.Remove(SqlConnectionStringBuilder._validKeywords[(int)keywords]))
			{
				this.Reset(keywords);
				return true;
			}
			return false;
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x000C0C04 File Offset: 0x000C0004
		private void Reset(SqlConnectionStringBuilder.Keywords index)
		{
			switch (index)
			{
			case SqlConnectionStringBuilder.Keywords.DataSource:
				this._dataSource = "";
				return;
			case SqlConnectionStringBuilder.Keywords.FailoverPartner:
				this._failoverPartner = "";
				return;
			case SqlConnectionStringBuilder.Keywords.AttachDBFilename:
				this._attachDBFilename = "";
				return;
			case SqlConnectionStringBuilder.Keywords.InitialCatalog:
				this._initialCatalog = "";
				return;
			case SqlConnectionStringBuilder.Keywords.IntegratedSecurity:
				this._integratedSecurity = false;
				return;
			case SqlConnectionStringBuilder.Keywords.PersistSecurityInfo:
				this._persistSecurityInfo = false;
				return;
			case SqlConnectionStringBuilder.Keywords.UserID:
				this._userID = "";
				return;
			case SqlConnectionStringBuilder.Keywords.Password:
				this._password = "";
				return;
			case SqlConnectionStringBuilder.Keywords.Enlist:
				this._enlist = true;
				return;
			case SqlConnectionStringBuilder.Keywords.Pooling:
				this._pooling = true;
				return;
			case SqlConnectionStringBuilder.Keywords.MinPoolSize:
				this._minPoolSize = 0;
				return;
			case SqlConnectionStringBuilder.Keywords.MaxPoolSize:
				this._maxPoolSize = 100;
				return;
			case SqlConnectionStringBuilder.Keywords.PoolBlockingPeriod:
				this._poolBlockingPeriod = PoolBlockingPeriod.Auto;
				return;
			case SqlConnectionStringBuilder.Keywords.AsynchronousProcessing:
				this._asynchronousProcessing = false;
				return;
			case SqlConnectionStringBuilder.Keywords.ConnectionReset:
				this._connectionReset = true;
				return;
			case SqlConnectionStringBuilder.Keywords.MultipleActiveResultSets:
				this._multipleActiveResultSets = false;
				return;
			case SqlConnectionStringBuilder.Keywords.Replication:
				this._replication = false;
				return;
			case SqlConnectionStringBuilder.Keywords.ConnectTimeout:
				this._connectTimeout = 15;
				return;
			case SqlConnectionStringBuilder.Keywords.Encrypt:
				this._encrypt = false;
				return;
			case SqlConnectionStringBuilder.Keywords.TrustServerCertificate:
				this._trustServerCertificate = false;
				return;
			case SqlConnectionStringBuilder.Keywords.LoadBalanceTimeout:
				this._loadBalanceTimeout = 0;
				return;
			case SqlConnectionStringBuilder.Keywords.NetworkLibrary:
				this._networkLibrary = "";
				return;
			case SqlConnectionStringBuilder.Keywords.PacketSize:
				this._packetSize = 8000;
				return;
			case SqlConnectionStringBuilder.Keywords.TypeSystemVersion:
				this._typeSystemVersion = "Latest";
				return;
			case SqlConnectionStringBuilder.Keywords.Authentication:
				this._authentication = DbConnectionStringDefaults.Authentication;
				return;
			case SqlConnectionStringBuilder.Keywords.ApplicationName:
				this._applicationName = ".Net SqlClient Data Provider";
				return;
			case SqlConnectionStringBuilder.Keywords.CurrentLanguage:
				this._currentLanguage = "";
				return;
			case SqlConnectionStringBuilder.Keywords.WorkstationID:
				this._workstationID = "";
				return;
			case SqlConnectionStringBuilder.Keywords.UserInstance:
				this._userInstance = false;
				return;
			case SqlConnectionStringBuilder.Keywords.ContextConnection:
				this._contextConnection = false;
				return;
			case SqlConnectionStringBuilder.Keywords.TransactionBinding:
				this._transactionBinding = "Implicit Unbind";
				return;
			case SqlConnectionStringBuilder.Keywords.ApplicationIntent:
				this._applicationIntent = ApplicationIntent.ReadWrite;
				return;
			case SqlConnectionStringBuilder.Keywords.MultiSubnetFailover:
				this._multiSubnetFailover = false;
				return;
			case SqlConnectionStringBuilder.Keywords.TransparentNetworkIPResolution:
				this._transparentNetworkIPResolution = DbConnectionStringDefaults.TransparentNetworkIPResolution;
				return;
			case SqlConnectionStringBuilder.Keywords.ConnectRetryCount:
				this._connectRetryCount = 1;
				return;
			case SqlConnectionStringBuilder.Keywords.ConnectRetryInterval:
				this._connectRetryInterval = 10;
				return;
			case SqlConnectionStringBuilder.Keywords.ColumnEncryptionSetting:
				this._columnEncryptionSetting = DbConnectionStringDefaults.ColumnEncryptionSetting;
				return;
			case SqlConnectionStringBuilder.Keywords.EnclaveAttestationUrl:
				this._enclaveAttestationUrl = "";
				return;
			default:
				throw ADP.KeywordNotSupported(SqlConnectionStringBuilder._validKeywords[(int)index]);
			}
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x000C0E38 File Offset: 0x000C0238
		private void SetValue(string keyword, bool value)
		{
			base[keyword] = value.ToString(null);
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x000C0E54 File Offset: 0x000C0254
		private void SetValue(string keyword, int value)
		{
			base[keyword] = value.ToString(null);
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x000C0E70 File Offset: 0x000C0270
		private void SetValue(string keyword, string value)
		{
			ADP.CheckArgumentNull(value, keyword);
			base[keyword] = value;
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x000C0E8C File Offset: 0x000C028C
		private void SetApplicationIntentValue(ApplicationIntent value)
		{
			base["ApplicationIntent"] = DbConnectionStringBuilderUtil.ApplicationIntentToString(value);
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x000C0EAC File Offset: 0x000C02AC
		private void SetPoolBlockingPeriodValue(PoolBlockingPeriod value)
		{
			base["PoolBlockingPeriod"] = DbConnectionStringBuilderUtil.PoolBlockingPeriodToString(value);
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x000C0ECC File Offset: 0x000C02CC
		private void SetAuthenticationValue(SqlAuthenticationMethod value)
		{
			base["Authentication"] = DbConnectionStringBuilderUtil.AuthenticationTypeToString(value);
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x000C0EEC File Offset: 0x000C02EC
		private void SetColumnEncryptionSettingValue(SqlConnectionColumnEncryptionSetting value)
		{
			base["Column Encryption Setting"] = DbConnectionStringBuilderUtil.ColumnEncryptionSettingToString(value);
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x000C0F0C File Offset: 0x000C030C
		public override bool ShouldSerialize(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			SqlConnectionStringBuilder.Keywords keywords;
			return SqlConnectionStringBuilder._keywords.TryGetValue(keyword, out keywords) && base.ShouldSerialize(SqlConnectionStringBuilder._validKeywords[(int)keywords]);
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x000C0F44 File Offset: 0x000C0344
		public override bool TryGetValue(string keyword, out object value)
		{
			SqlConnectionStringBuilder.Keywords index;
			if (SqlConnectionStringBuilder._keywords.TryGetValue(keyword, out index))
			{
				value = this.GetAt(index);
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x04000FC8 RID: 4040
		internal const int KeywordsCount = 38;

		// Token: 0x04000FC9 RID: 4041
		private static readonly string[] _validKeywords;

		// Token: 0x04000FCA RID: 4042
		private static readonly Dictionary<string, SqlConnectionStringBuilder.Keywords> _keywords;

		// Token: 0x04000FCB RID: 4043
		private ApplicationIntent _applicationIntent;

		// Token: 0x04000FCC RID: 4044
		private string _applicationName = ".Net SqlClient Data Provider";

		// Token: 0x04000FCD RID: 4045
		private string _attachDBFilename = "";

		// Token: 0x04000FCE RID: 4046
		private string _currentLanguage = "";

		// Token: 0x04000FCF RID: 4047
		private string _dataSource = "";

		// Token: 0x04000FD0 RID: 4048
		private string _failoverPartner = "";

		// Token: 0x04000FD1 RID: 4049
		private string _initialCatalog = "";

		// Token: 0x04000FD2 RID: 4050
		private string _networkLibrary = "";

		// Token: 0x04000FD3 RID: 4051
		private string _password = "";

		// Token: 0x04000FD4 RID: 4052
		private string _transactionBinding = "Implicit Unbind";

		// Token: 0x04000FD5 RID: 4053
		private string _typeSystemVersion = "Latest";

		// Token: 0x04000FD6 RID: 4054
		private string _userID = "";

		// Token: 0x04000FD7 RID: 4055
		private string _workstationID = "";

		// Token: 0x04000FD8 RID: 4056
		private int _connectTimeout = 15;

		// Token: 0x04000FD9 RID: 4057
		private int _loadBalanceTimeout;

		// Token: 0x04000FDA RID: 4058
		private int _maxPoolSize = 100;

		// Token: 0x04000FDB RID: 4059
		private int _minPoolSize;

		// Token: 0x04000FDC RID: 4060
		private int _packetSize = 8000;

		// Token: 0x04000FDD RID: 4061
		private int _connectRetryCount = 1;

		// Token: 0x04000FDE RID: 4062
		private int _connectRetryInterval = 10;

		// Token: 0x04000FDF RID: 4063
		private bool _asynchronousProcessing;

		// Token: 0x04000FE0 RID: 4064
		private bool _connectionReset = true;

		// Token: 0x04000FE1 RID: 4065
		private bool _contextConnection;

		// Token: 0x04000FE2 RID: 4066
		private bool _encrypt;

		// Token: 0x04000FE3 RID: 4067
		private bool _trustServerCertificate;

		// Token: 0x04000FE4 RID: 4068
		private bool _enlist = true;

		// Token: 0x04000FE5 RID: 4069
		private bool _integratedSecurity;

		// Token: 0x04000FE6 RID: 4070
		private bool _multipleActiveResultSets;

		// Token: 0x04000FE7 RID: 4071
		private bool _multiSubnetFailover;

		// Token: 0x04000FE8 RID: 4072
		private bool _transparentNetworkIPResolution = DbConnectionStringDefaults.TransparentNetworkIPResolution;

		// Token: 0x04000FE9 RID: 4073
		private bool _persistSecurityInfo;

		// Token: 0x04000FEA RID: 4074
		private bool _pooling = true;

		// Token: 0x04000FEB RID: 4075
		private bool _replication;

		// Token: 0x04000FEC RID: 4076
		private bool _userInstance;

		// Token: 0x04000FED RID: 4077
		private SqlAuthenticationMethod _authentication = DbConnectionStringDefaults.Authentication;

		// Token: 0x04000FEE RID: 4078
		private SqlConnectionColumnEncryptionSetting _columnEncryptionSetting = DbConnectionStringDefaults.ColumnEncryptionSetting;

		// Token: 0x04000FEF RID: 4079
		private string _enclaveAttestationUrl = "";

		// Token: 0x04000FF0 RID: 4080
		private PoolBlockingPeriod _poolBlockingPeriod;

		// Token: 0x020003AD RID: 941
		private enum Keywords
		{
			// Token: 0x04002055 RID: 8277
			DataSource,
			// Token: 0x04002056 RID: 8278
			FailoverPartner,
			// Token: 0x04002057 RID: 8279
			AttachDBFilename,
			// Token: 0x04002058 RID: 8280
			InitialCatalog,
			// Token: 0x04002059 RID: 8281
			IntegratedSecurity,
			// Token: 0x0400205A RID: 8282
			PersistSecurityInfo,
			// Token: 0x0400205B RID: 8283
			UserID,
			// Token: 0x0400205C RID: 8284
			Password,
			// Token: 0x0400205D RID: 8285
			Enlist,
			// Token: 0x0400205E RID: 8286
			Pooling,
			// Token: 0x0400205F RID: 8287
			MinPoolSize,
			// Token: 0x04002060 RID: 8288
			MaxPoolSize,
			// Token: 0x04002061 RID: 8289
			PoolBlockingPeriod,
			// Token: 0x04002062 RID: 8290
			AsynchronousProcessing,
			// Token: 0x04002063 RID: 8291
			ConnectionReset,
			// Token: 0x04002064 RID: 8292
			MultipleActiveResultSets,
			// Token: 0x04002065 RID: 8293
			Replication,
			// Token: 0x04002066 RID: 8294
			ConnectTimeout,
			// Token: 0x04002067 RID: 8295
			Encrypt,
			// Token: 0x04002068 RID: 8296
			TrustServerCertificate,
			// Token: 0x04002069 RID: 8297
			LoadBalanceTimeout,
			// Token: 0x0400206A RID: 8298
			NetworkLibrary,
			// Token: 0x0400206B RID: 8299
			PacketSize,
			// Token: 0x0400206C RID: 8300
			TypeSystemVersion,
			// Token: 0x0400206D RID: 8301
			Authentication,
			// Token: 0x0400206E RID: 8302
			ApplicationName,
			// Token: 0x0400206F RID: 8303
			CurrentLanguage,
			// Token: 0x04002070 RID: 8304
			WorkstationID,
			// Token: 0x04002071 RID: 8305
			UserInstance,
			// Token: 0x04002072 RID: 8306
			ContextConnection,
			// Token: 0x04002073 RID: 8307
			TransactionBinding,
			// Token: 0x04002074 RID: 8308
			ApplicationIntent,
			// Token: 0x04002075 RID: 8309
			MultiSubnetFailover,
			// Token: 0x04002076 RID: 8310
			TransparentNetworkIPResolution,
			// Token: 0x04002077 RID: 8311
			ConnectRetryCount,
			// Token: 0x04002078 RID: 8312
			ConnectRetryInterval,
			// Token: 0x04002079 RID: 8313
			ColumnEncryptionSetting,
			// Token: 0x0400207A RID: 8314
			EnclaveAttestationUrl,
			// Token: 0x0400207B RID: 8315
			KeywordsCount
		}

		// Token: 0x020003AE RID: 942
		private sealed class NetworkLibraryConverter : TypeConverter
		{
			// Token: 0x060034E3 RID: 13539 RVA: 0x00142EE0 File Offset: 0x001422E0
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return typeof(string) == sourceType || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x060034E4 RID: 13540 RVA: 0x00142F0C File Offset: 0x0014230C
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				string text = value as string;
				if (text == null)
				{
					return base.ConvertFrom(context, culture, value);
				}
				text = text.Trim();
				if (StringComparer.OrdinalIgnoreCase.Equals(text, "Named Pipes (DBNMPNTW)"))
				{
					return "dbnmpntw";
				}
				if (StringComparer.OrdinalIgnoreCase.Equals(text, "Shared Memory (DBMSLPCN)"))
				{
					return "dbmslpcn";
				}
				if (StringComparer.OrdinalIgnoreCase.Equals(text, "TCP/IP (DBMSSOCN)"))
				{
					return "dbmssocn";
				}
				if (StringComparer.OrdinalIgnoreCase.Equals(text, "VIA (DBMSGNET)"))
				{
					return "dbmsgnet";
				}
				return text;
			}

			// Token: 0x060034E5 RID: 13541 RVA: 0x00142F98 File Offset: 0x00142398
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(string) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x060034E6 RID: 13542 RVA: 0x00142FC4 File Offset: 0x001423C4
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				string text = value as string;
				if (text == null || !(destinationType == typeof(string)))
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				string a = text.Trim().ToLower(CultureInfo.InvariantCulture);
				if (a == "dbnmpntw")
				{
					return "Named Pipes (DBNMPNTW)";
				}
				if (a == "dbmslpcn")
				{
					return "Shared Memory (DBMSLPCN)";
				}
				if (a == "dbmssocn")
				{
					return "TCP/IP (DBMSSOCN)";
				}
				if (!(a == "dbmsgnet"))
				{
					return text;
				}
				return "VIA (DBMSGNET)";
			}

			// Token: 0x060034E7 RID: 13543 RVA: 0x00143060 File Offset: 0x00142460
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			// Token: 0x060034E8 RID: 13544 RVA: 0x00143070 File Offset: 0x00142470
			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return false;
			}

			// Token: 0x060034E9 RID: 13545 RVA: 0x00143080 File Offset: 0x00142480
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				if (context != null)
				{
					SqlConnectionStringBuilder sqlConnectionStringBuilder = context.Instance as SqlConnectionStringBuilder;
				}
				TypeConverter.StandardValuesCollection standardValuesCollection = this._standardValues;
				if (standardValuesCollection == null)
				{
					string[] values = new string[]
					{
						"Named Pipes (DBNMPNTW)",
						"Shared Memory (DBMSLPCN)",
						"TCP/IP (DBMSSOCN)",
						"VIA (DBMSGNET)"
					};
					standardValuesCollection = new TypeConverter.StandardValuesCollection(values);
					this._standardValues = standardValuesCollection;
				}
				return standardValuesCollection;
			}

			// Token: 0x0400207C RID: 8316
			private const string NamedPipes = "Named Pipes (DBNMPNTW)";

			// Token: 0x0400207D RID: 8317
			private const string SharedMemory = "Shared Memory (DBMSLPCN)";

			// Token: 0x0400207E RID: 8318
			private const string TCPIP = "TCP/IP (DBMSSOCN)";

			// Token: 0x0400207F RID: 8319
			private const string VIA = "VIA (DBMSGNET)";

			// Token: 0x04002080 RID: 8320
			private TypeConverter.StandardValuesCollection _standardValues;
		}

		// Token: 0x020003AF RID: 943
		private sealed class SqlDataSourceConverter : StringConverter
		{
			// Token: 0x060034EB RID: 13547 RVA: 0x001430F4 File Offset: 0x001424F4
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			// Token: 0x060034EC RID: 13548 RVA: 0x00143104 File Offset: 0x00142504
			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return false;
			}

			// Token: 0x060034ED RID: 13549 RVA: 0x00143114 File Offset: 0x00142514
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				TypeConverter.StandardValuesCollection standardValuesCollection = this._standardValues;
				if (this._standardValues == null)
				{
					DataTable dataSources = SqlClientFactory.Instance.CreateDataSourceEnumerator().GetDataSources();
					DataColumn column = dataSources.Columns["ServerName"];
					DataColumn column2 = dataSources.Columns["InstanceName"];
					DataRowCollection rows = dataSources.Rows;
					string[] array = new string[rows.Count];
					for (int i = 0; i < array.Length; i++)
					{
						string text = rows[i][column] as string;
						string text2 = rows[i][column2] as string;
						if (text2 == null || text2.Length == 0 || "MSSQLSERVER" == text2)
						{
							array[i] = text;
						}
						else
						{
							array[i] = text + "\\" + text2;
						}
					}
					Array.Sort<string>(array);
					standardValuesCollection = new TypeConverter.StandardValuesCollection(array);
					this._standardValues = standardValuesCollection;
				}
				return standardValuesCollection;
			}

			// Token: 0x04002081 RID: 8321
			private TypeConverter.StandardValuesCollection _standardValues;
		}

		// Token: 0x020003B0 RID: 944
		private sealed class SqlInitialCatalogConverter : StringConverter
		{
			// Token: 0x060034EF RID: 13551 RVA: 0x00143214 File Offset: 0x00142614
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return this.GetStandardValuesSupportedInternal(context);
			}

			// Token: 0x060034F0 RID: 13552 RVA: 0x00143228 File Offset: 0x00142628
			private bool GetStandardValuesSupportedInternal(ITypeDescriptorContext context)
			{
				bool result = false;
				if (context != null)
				{
					SqlConnectionStringBuilder sqlConnectionStringBuilder = context.Instance as SqlConnectionStringBuilder;
					if (sqlConnectionStringBuilder != null && 0 < sqlConnectionStringBuilder.DataSource.Length && (sqlConnectionStringBuilder.IntegratedSecurity || 0 < sqlConnectionStringBuilder.UserID.Length))
					{
						result = true;
					}
				}
				return result;
			}

			// Token: 0x060034F1 RID: 13553 RVA: 0x00143270 File Offset: 0x00142670
			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return false;
			}

			// Token: 0x060034F2 RID: 13554 RVA: 0x00143280 File Offset: 0x00142680
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				if (this.GetStandardValuesSupportedInternal(context))
				{
					List<string> list = new List<string>();
					try
					{
						SqlConnectionStringBuilder sqlConnectionStringBuilder = (SqlConnectionStringBuilder)context.Instance;
						using (SqlConnection sqlConnection = new SqlConnection())
						{
							sqlConnection.ConnectionString = sqlConnectionStringBuilder.ConnectionString;
							sqlConnection.Open();
							DataTable schema = sqlConnection.GetSchema("DATABASES");
							foreach (object obj in schema.Rows)
							{
								DataRow dataRow = (DataRow)obj;
								string item = (string)dataRow["database_name"];
								list.Add(item);
							}
						}
					}
					catch (SqlException e)
					{
						ADP.TraceExceptionWithoutRethrow(e);
					}
					return new TypeConverter.StandardValuesCollection(list);
				}
				return null;
			}
		}

		// Token: 0x020003B1 RID: 945
		internal sealed class SqlConnectionStringBuilderConverter : ExpandableObjectConverter
		{
			// Token: 0x060034F4 RID: 13556 RVA: 0x001433A8 File Offset: 0x001427A8
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x060034F5 RID: 13557 RVA: 0x001433D4 File Offset: 0x001427D4
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == null)
				{
					throw ADP.ArgumentNull("destinationType");
				}
				if (typeof(InstanceDescriptor) == destinationType)
				{
					SqlConnectionStringBuilder sqlConnectionStringBuilder = value as SqlConnectionStringBuilder;
					if (sqlConnectionStringBuilder != null)
					{
						return this.ConvertToInstanceDescriptor(sqlConnectionStringBuilder);
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			// Token: 0x060034F6 RID: 13558 RVA: 0x00143428 File Offset: 0x00142828
			private InstanceDescriptor ConvertToInstanceDescriptor(SqlConnectionStringBuilder options)
			{
				Type[] types = new Type[]
				{
					typeof(string)
				};
				object[] arguments = new object[]
				{
					options.ConnectionString
				};
				ConstructorInfo constructor = typeof(SqlConnectionStringBuilder).GetConstructor(types);
				return new InstanceDescriptor(constructor, arguments);
			}
		}
	}
}
