using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;
using System.Security;
using System.Security.Permissions;

namespace System.Data.SqlClient
{
	// Token: 0x020001BE RID: 446
	internal sealed class SqlConnectionString : DbConnectionOptions
	{
		// Token: 0x06001AE9 RID: 6889 RVA: 0x000BDE58 File Offset: 0x000BD258
		internal SqlConnectionString(string connectionString) : base(connectionString, SqlConnectionString.GetParseSynonyms(), false)
		{
			bool inProc = InOutOfProcHelper.InProc;
			this._integratedSecurity = base.ConvertValueToIntegratedSecurity();
			base.ConvertValueToBoolean("asynchronous processing", false);
			this._poolBlockingPeriod = this.ConvertValueToPoolBlockingPeriod();
			this._connectionReset = base.ConvertValueToBoolean("connection reset", true);
			this._contextConnection = base.ConvertValueToBoolean("context connection", false);
			this._encrypt = this.ConvertValueToEncrypt();
			this._enlist = base.ConvertValueToBoolean("enlist", ADP.IsWindowsNT);
			this._mars = base.ConvertValueToBoolean("multipleactiveresultsets", false);
			this._persistSecurityInfo = base.ConvertValueToBoolean("persist security info", false);
			this._pooling = base.ConvertValueToBoolean("pooling", true);
			this._replication = base.ConvertValueToBoolean("replication", false);
			this._userInstance = base.ConvertValueToBoolean("user instance", false);
			this._multiSubnetFailover = base.ConvertValueToBoolean("multisubnetfailover", false);
			this._transparentNetworkIPResolution = base.ConvertValueToBoolean("transparentnetworkipresolution", SqlConnectionString.DEFAULT.TransparentNetworkIPResolution);
			this._connectTimeout = base.ConvertValueToInt32("connect timeout", 15);
			this._loadBalanceTimeout = base.ConvertValueToInt32("load balance timeout", 0);
			this._maxPoolSize = base.ConvertValueToInt32("max pool size", 100);
			this._minPoolSize = base.ConvertValueToInt32("min pool size", 0);
			this._packetSize = base.ConvertValueToInt32("packet size", 8000);
			this._connectRetryCount = base.ConvertValueToInt32("connectretrycount", 1);
			this._connectRetryInterval = base.ConvertValueToInt32("connectretryinterval", 10);
			this._applicationIntent = this.ConvertValueToApplicationIntent();
			this._applicationName = base.ConvertValueToString("application name", ".Net SqlClient Data Provider");
			this._attachDBFileName = base.ConvertValueToString("attachdbfilename", "");
			this._currentLanguage = base.ConvertValueToString("current language", "");
			this._dataSource = base.ConvertValueToString("data source", "");
			this._localDBInstance = LocalDBAPI.GetLocalDbInstanceNameFromServerName(this._dataSource);
			this._failoverPartner = base.ConvertValueToString("failover partner", "");
			this._initialCatalog = base.ConvertValueToString("initial catalog", "");
			this._networkLibrary = base.ConvertValueToString("network library", null);
			this._password = base.ConvertValueToString("password", "");
			this._trustServerCertificate = base.ConvertValueToBoolean("trustservercertificate", false);
			this._authType = this.ConvertValueToAuthenticationType();
			this._columnEncryptionSetting = this.ConvertValueToColumnEncryptionSetting();
			this._enclaveAttestationUrl = base.ConvertValueToString("enclave attestation url", "");
			string text = base.ConvertValueToString("type system version", null);
			string text2 = base.ConvertValueToString("transaction binding", null);
			this._userID = base.ConvertValueToString("user id", "");
			this._workstationId = base.ConvertValueToString("workstation id", null);
			if (this._contextConnection)
			{
				if (!inProc)
				{
					throw SQL.ContextUnavailableOutOfProc();
				}
				foreach (object obj in base.Parsetable)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if ((string)dictionaryEntry.Key != "context connection" && (string)dictionaryEntry.Key != "type system version")
					{
						throw SQL.ContextAllowsLimitedKeywords();
					}
				}
			}
			if (!this._encrypt)
			{
				object obj2 = ADP.LocalMachineRegistryValue("Software\\Microsoft\\MSSQLServer\\Client\\SuperSocketNetLib", "Encrypt");
				if (obj2 is int && 1 == (int)obj2)
				{
					this._encrypt = true;
				}
			}
			if (this._loadBalanceTimeout < 0)
			{
				throw ADP.InvalidConnectionOptionValue("load balance timeout");
			}
			if (this._connectTimeout < 0)
			{
				throw ADP.InvalidConnectionOptionValue("connect timeout");
			}
			if (this._maxPoolSize < 1)
			{
				throw ADP.InvalidConnectionOptionValue("max pool size");
			}
			if (this._minPoolSize < 0)
			{
				throw ADP.InvalidConnectionOptionValue("min pool size");
			}
			if (this._maxPoolSize < this._minPoolSize)
			{
				throw ADP.InvalidMinMaxPoolSizeValues();
			}
			if (this._packetSize < 512 || 32768 < this._packetSize)
			{
				throw SQL.InvalidPacketSizeValue();
			}
			if (this._networkLibrary != null)
			{
				string key = this._networkLibrary.Trim().ToLower(CultureInfo.InvariantCulture);
				Hashtable hashtable = SqlConnectionString.NetlibMapping();
				if (!hashtable.ContainsKey(key))
				{
					throw ADP.InvalidConnectionOptionValue("network library");
				}
				this._networkLibrary = (string)hashtable[key];
			}
			else
			{
				this._networkLibrary = "";
			}
			this.ValidateValueLength(this._applicationName, 128, "application name");
			this.ValidateValueLength(this._currentLanguage, 128, "current language");
			this.ValidateValueLength(this._dataSource, 128, "data source");
			this.ValidateValueLength(this._failoverPartner, 128, "failover partner");
			this.ValidateValueLength(this._initialCatalog, 128, "initial catalog");
			this.ValidateValueLength(this._password, 128, "password");
			this.ValidateValueLength(this._userID, 128, "user id");
			if (this._workstationId != null)
			{
				this.ValidateValueLength(this._workstationId, 128, "workstation id");
			}
			if (!string.Equals("", this._failoverPartner, StringComparison.OrdinalIgnoreCase))
			{
				if (this._multiSubnetFailover)
				{
					throw SQL.MultiSubnetFailoverWithFailoverPartner(false, null);
				}
				if (string.Equals("", this._initialCatalog, StringComparison.OrdinalIgnoreCase))
				{
					throw ADP.MissingConnectionOptionValue("failover partner", "initial catalog");
				}
			}
			string text3 = null;
			this._expandedAttachDBFilename = DbConnectionOptions.ExpandDataDirectory("attachdbfilename", this._attachDBFileName, ref text3);
			if (this._expandedAttachDBFilename != null)
			{
				if (0 <= this._expandedAttachDBFilename.IndexOf('|'))
				{
					throw ADP.InvalidConnectionOptionValue("attachdbfilename");
				}
				this.ValidateValueLength(this._expandedAttachDBFilename, 260, "attachdbfilename");
				if (this._localDBInstance == null)
				{
					string dataSource = this._dataSource;
					string networkLibrary = this._networkLibrary;
					TdsParserStaticMethods.AliasRegistryLookup(ref dataSource, ref networkLibrary);
					SqlConnectionString.VerifyLocalHostAndFixup(ref dataSource, true, false);
				}
			}
			else
			{
				if (0 <= this._attachDBFileName.IndexOf('|'))
				{
					throw ADP.InvalidConnectionOptionValue("attachdbfilename");
				}
				this.ValidateValueLength(this._attachDBFileName, 260, "attachdbfilename");
			}
			this._typeSystemAssemblyVersion = SqlConnectionString.constTypeSystemAsmVersion10;
			if (this._userInstance && !ADP.IsEmpty(this._failoverPartner))
			{
				throw SQL.UserInstanceFailoverNotCompatible();
			}
			if (ADP.IsEmpty(text))
			{
				text = "Latest";
			}
			if (text.Equals("Latest", StringComparison.OrdinalIgnoreCase))
			{
				this._typeSystemVersion = SqlConnectionString.TypeSystem.Latest;
			}
			else if (text.Equals("SQL Server 2000", StringComparison.OrdinalIgnoreCase))
			{
				if (this._contextConnection)
				{
					throw SQL.ContextAllowsOnlyTypeSystem2005();
				}
				this._typeSystemVersion = SqlConnectionString.TypeSystem.SQLServer2000;
			}
			else if (text.Equals("SQL Server 2005", StringComparison.OrdinalIgnoreCase))
			{
				this._typeSystemVersion = SqlConnectionString.TypeSystem.SQLServer2005;
			}
			else if (text.Equals("SQL Server 2008", StringComparison.OrdinalIgnoreCase))
			{
				this._typeSystemVersion = SqlConnectionString.TypeSystem.Latest;
			}
			else
			{
				if (!text.Equals("SQL Server 2012", StringComparison.OrdinalIgnoreCase))
				{
					throw ADP.InvalidConnectionOptionValue("type system version");
				}
				this._typeSystemVersion = SqlConnectionString.TypeSystem.SQLServer2012;
				this._typeSystemAssemblyVersion = SqlConnectionString.constTypeSystemAsmVersion11;
			}
			if (ADP.IsEmpty(text2))
			{
				text2 = "Implicit Unbind";
			}
			if (text2.Equals("Implicit Unbind", StringComparison.OrdinalIgnoreCase))
			{
				this._transactionBinding = SqlConnectionString.TransactionBindingEnum.ImplicitUnbind;
			}
			else
			{
				if (!text2.Equals("Explicit Unbind", StringComparison.OrdinalIgnoreCase))
				{
					throw ADP.InvalidConnectionOptionValue("transaction binding");
				}
				this._transactionBinding = SqlConnectionString.TransactionBindingEnum.ExplicitUnbind;
			}
			if (this._connectRetryCount < 0 || this._connectRetryCount > 255)
			{
				throw ADP.InvalidConnectRetryCountValue();
			}
			if (this._connectRetryInterval < 1 || this._connectRetryInterval > 60)
			{
				throw ADP.InvalidConnectRetryIntervalValue();
			}
			if (this.Authentication != SqlAuthenticationMethod.NotSpecified && this._integratedSecurity)
			{
				throw SQL.AuthenticationAndIntegratedSecurity();
			}
			if (this.Authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated && (this.HasUserIdKeyword || this.HasPasswordKeyword))
			{
				throw SQL.IntegratedWithUserIDAndPassword();
			}
			if (this.Authentication == SqlAuthenticationMethod.ActiveDirectoryInteractive && !this.HasUserIdKeyword)
			{
				throw SQL.InteractiveWithoutUserID();
			}
			if (this.Authentication == SqlAuthenticationMethod.ActiveDirectoryInteractive && this.HasPasswordKeyword)
			{
				throw SQL.InteractiveWithPassword();
			}
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x000BE658 File Offset: 0x000BDA58
		internal SqlConnectionString(SqlConnectionString connectionOptions, string dataSource, bool userInstance, bool? setEnlistValue) : base(connectionOptions)
		{
			this._integratedSecurity = connectionOptions._integratedSecurity;
			this._connectionReset = connectionOptions._connectionReset;
			this._contextConnection = connectionOptions._contextConnection;
			this._encrypt = connectionOptions._encrypt;
			if (setEnlistValue != null)
			{
				this._enlist = setEnlistValue.Value;
			}
			else
			{
				this._enlist = connectionOptions._enlist;
			}
			this._mars = connectionOptions._mars;
			this._persistSecurityInfo = connectionOptions._persistSecurityInfo;
			this._pooling = connectionOptions._pooling;
			this._replication = connectionOptions._replication;
			this._userInstance = userInstance;
			this._connectTimeout = connectionOptions._connectTimeout;
			this._loadBalanceTimeout = connectionOptions._loadBalanceTimeout;
			this._poolBlockingPeriod = connectionOptions._poolBlockingPeriod;
			this._maxPoolSize = connectionOptions._maxPoolSize;
			this._minPoolSize = connectionOptions._minPoolSize;
			this._multiSubnetFailover = connectionOptions._multiSubnetFailover;
			this._transparentNetworkIPResolution = connectionOptions._transparentNetworkIPResolution;
			this._packetSize = connectionOptions._packetSize;
			this._applicationName = connectionOptions._applicationName;
			this._attachDBFileName = connectionOptions._attachDBFileName;
			this._currentLanguage = connectionOptions._currentLanguage;
			this._dataSource = dataSource;
			this._localDBInstance = LocalDBAPI.GetLocalDbInstanceNameFromServerName(this._dataSource);
			this._failoverPartner = connectionOptions._failoverPartner;
			this._initialCatalog = connectionOptions._initialCatalog;
			this._password = connectionOptions._password;
			this._userID = connectionOptions._userID;
			this._networkLibrary = connectionOptions._networkLibrary;
			this._workstationId = connectionOptions._workstationId;
			this._expandedAttachDBFilename = connectionOptions._expandedAttachDBFilename;
			this._typeSystemVersion = connectionOptions._typeSystemVersion;
			this._typeSystemAssemblyVersion = connectionOptions._typeSystemAssemblyVersion;
			this._transactionBinding = connectionOptions._transactionBinding;
			this._applicationIntent = connectionOptions._applicationIntent;
			this._connectRetryCount = connectionOptions._connectRetryCount;
			this._connectRetryInterval = connectionOptions._connectRetryInterval;
			this._authType = connectionOptions._authType;
			this._columnEncryptionSetting = connectionOptions._columnEncryptionSetting;
			this._enclaveAttestationUrl = connectionOptions._enclaveAttestationUrl;
			this.ValidateValueLength(this._dataSource, 128, "data source");
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x000BE86C File Offset: 0x000BDC6C
		internal bool IntegratedSecurity
		{
			get
			{
				return this._integratedSecurity;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x000BE880 File Offset: 0x000BDC80
		internal bool Asynchronous
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x000BE890 File Offset: 0x000BDC90
		internal PoolBlockingPeriod PoolBlockingPeriod
		{
			get
			{
				return this._poolBlockingPeriod;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x000BE8A4 File Offset: 0x000BDCA4
		internal bool ConnectionReset
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x000BE8B4 File Offset: 0x000BDCB4
		internal bool ContextConnection
		{
			get
			{
				return this._contextConnection;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x000BE8C8 File Offset: 0x000BDCC8
		internal bool Encrypt
		{
			get
			{
				return this._encrypt;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x000BE8DC File Offset: 0x000BDCDC
		internal bool TrustServerCertificate
		{
			get
			{
				return this._trustServerCertificate;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x000BE8F0 File Offset: 0x000BDCF0
		internal bool Enlist
		{
			get
			{
				return this._enlist;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x000BE904 File Offset: 0x000BDD04
		internal bool MARS
		{
			get
			{
				return this._mars;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x000BE918 File Offset: 0x000BDD18
		internal bool MultiSubnetFailover
		{
			get
			{
				return this._multiSubnetFailover;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x000BE92C File Offset: 0x000BDD2C
		internal bool TransparentNetworkIPResolution
		{
			get
			{
				return this._transparentNetworkIPResolution;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x000BE940 File Offset: 0x000BDD40
		internal SqlAuthenticationMethod Authentication
		{
			get
			{
				return this._authType;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x000BE954 File Offset: 0x000BDD54
		internal SqlConnectionColumnEncryptionSetting ColumnEncryptionSetting
		{
			get
			{
				return this._columnEncryptionSetting;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x000BE968 File Offset: 0x000BDD68
		internal string EnclaveAttestationUrl
		{
			get
			{
				return this._enclaveAttestationUrl;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x000BE97C File Offset: 0x000BDD7C
		internal bool PersistSecurityInfo
		{
			get
			{
				return this._persistSecurityInfo;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x000BE990 File Offset: 0x000BDD90
		internal bool Pooling
		{
			get
			{
				return this._pooling;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x000BE9A4 File Offset: 0x000BDDA4
		internal bool Replication
		{
			get
			{
				return this._replication;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001AFC RID: 6908 RVA: 0x000BE9B8 File Offset: 0x000BDDB8
		internal bool UserInstance
		{
			get
			{
				return this._userInstance;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x000BE9CC File Offset: 0x000BDDCC
		internal int ConnectTimeout
		{
			get
			{
				return this._connectTimeout;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001AFE RID: 6910 RVA: 0x000BE9E0 File Offset: 0x000BDDE0
		internal int LoadBalanceTimeout
		{
			get
			{
				return this._loadBalanceTimeout;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x000BE9F4 File Offset: 0x000BDDF4
		internal int MaxPoolSize
		{
			get
			{
				return this._maxPoolSize;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x000BEA08 File Offset: 0x000BDE08
		internal int MinPoolSize
		{
			get
			{
				return this._minPoolSize;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x000BEA1C File Offset: 0x000BDE1C
		internal int PacketSize
		{
			get
			{
				return this._packetSize;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x000BEA30 File Offset: 0x000BDE30
		internal int ConnectRetryCount
		{
			get
			{
				return this._connectRetryCount;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x000BEA44 File Offset: 0x000BDE44
		internal int ConnectRetryInterval
		{
			get
			{
				return this._connectRetryInterval;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x000BEA58 File Offset: 0x000BDE58
		internal ApplicationIntent ApplicationIntent
		{
			get
			{
				return this._applicationIntent;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001B05 RID: 6917 RVA: 0x000BEA6C File Offset: 0x000BDE6C
		internal string ApplicationName
		{
			get
			{
				return this._applicationName;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001B06 RID: 6918 RVA: 0x000BEA80 File Offset: 0x000BDE80
		internal string AttachDBFilename
		{
			get
			{
				return this._attachDBFileName;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001B07 RID: 6919 RVA: 0x000BEA94 File Offset: 0x000BDE94
		internal string CurrentLanguage
		{
			get
			{
				return this._currentLanguage;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x000BEAA8 File Offset: 0x000BDEA8
		internal string DataSource
		{
			get
			{
				return this._dataSource;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x000BEABC File Offset: 0x000BDEBC
		internal string LocalDBInstance
		{
			get
			{
				return this._localDBInstance;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x000BEAD0 File Offset: 0x000BDED0
		internal string FailoverPartner
		{
			get
			{
				return this._failoverPartner;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x000BEAE4 File Offset: 0x000BDEE4
		internal string InitialCatalog
		{
			get
			{
				return this._initialCatalog;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x000BEAF8 File Offset: 0x000BDEF8
		internal string NetworkLibrary
		{
			get
			{
				return this._networkLibrary;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x000BEB0C File Offset: 0x000BDF0C
		internal string Password
		{
			get
			{
				return this._password;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x000BEB20 File Offset: 0x000BDF20
		internal string UserID
		{
			get
			{
				return this._userID;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x000BEB34 File Offset: 0x000BDF34
		internal string WorkstationId
		{
			get
			{
				return this._workstationId;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x000BEB48 File Offset: 0x000BDF48
		internal SqlConnectionString.TypeSystem TypeSystemVersion
		{
			get
			{
				return this._typeSystemVersion;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x000BEB5C File Offset: 0x000BDF5C
		internal Version TypeSystemAssemblyVersion
		{
			get
			{
				return this._typeSystemAssemblyVersion;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x000BEB70 File Offset: 0x000BDF70
		internal SqlConnectionString.TransactionBindingEnum TransactionBinding
		{
			get
			{
				return this._transactionBinding;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x000BEB84 File Offset: 0x000BDF84
		internal bool EnforceLocalHost
		{
			get
			{
				return this._expandedAttachDBFilename != null && this._localDBInstance == null;
			}
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x000BEBA4 File Offset: 0x000BDFA4
		protected internal override PermissionSet CreatePermissionSet()
		{
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(new SqlClientPermission(this));
			return permissionSet;
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x000BEBC8 File Offset: 0x000BDFC8
		protected internal override string Expand()
		{
			if (this._expandedAttachDBFilename != null)
			{
				return base.ExpandKeyword("attachdbfilename", this._expandedAttachDBFilename);
			}
			return base.Expand();
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x000BEBF8 File Offset: 0x000BDFF8
		private static bool CompareHostName(ref string host, string name, bool fixup)
		{
			bool result = false;
			if (host.Equals(name, StringComparison.OrdinalIgnoreCase))
			{
				if (fixup)
				{
					host = ".";
				}
				result = true;
			}
			else if (host.StartsWith(name + "\\", StringComparison.OrdinalIgnoreCase))
			{
				if (fixup)
				{
					host = "." + host.Substring(name.Length);
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x000BEC54 File Offset: 0x000BE054
		internal static Hashtable GetParseSynonyms()
		{
			Hashtable hashtable = SqlConnectionString._sqlClientSynonyms;
			if (hashtable == null)
			{
				hashtable = new Hashtable(59);
				hashtable.Add("applicationintent", "applicationintent");
				hashtable.Add("application name", "application name");
				hashtable.Add("asynchronous processing", "asynchronous processing");
				hashtable.Add("attachdbfilename", "attachdbfilename");
				hashtable.Add("poolblockingperiod", "poolblockingperiod");
				hashtable.Add("connect timeout", "connect timeout");
				hashtable.Add("connection reset", "connection reset");
				hashtable.Add("context connection", "context connection");
				hashtable.Add("current language", "current language");
				hashtable.Add("data source", "data source");
				hashtable.Add("encrypt", "encrypt");
				hashtable.Add("enlist", "enlist");
				hashtable.Add("failover partner", "failover partner");
				hashtable.Add("initial catalog", "initial catalog");
				hashtable.Add("integrated security", "integrated security");
				hashtable.Add("load balance timeout", "load balance timeout");
				hashtable.Add("multipleactiveresultsets", "multipleactiveresultsets");
				hashtable.Add("max pool size", "max pool size");
				hashtable.Add("min pool size", "min pool size");
				hashtable.Add("multisubnetfailover", "multisubnetfailover");
				hashtable.Add("transparentnetworkipresolution", "transparentnetworkipresolution");
				hashtable.Add("network library", "network library");
				hashtable.Add("packet size", "packet size");
				hashtable.Add("password", "password");
				hashtable.Add("persist security info", "persist security info");
				hashtable.Add("pooling", "pooling");
				hashtable.Add("replication", "replication");
				hashtable.Add("trustservercertificate", "trustservercertificate");
				hashtable.Add("transaction binding", "transaction binding");
				hashtable.Add("type system version", "type system version");
				hashtable.Add("column encryption setting", "column encryption setting");
				hashtable.Add("enclave attestation url", "enclave attestation url");
				hashtable.Add("user id", "user id");
				hashtable.Add("user instance", "user instance");
				hashtable.Add("workstation id", "workstation id");
				hashtable.Add("connectretrycount", "connectretrycount");
				hashtable.Add("connectretryinterval", "connectretryinterval");
				hashtable.Add("authentication", "authentication");
				hashtable.Add("app", "application name");
				hashtable.Add("async", "asynchronous processing");
				hashtable.Add("extended properties", "attachdbfilename");
				hashtable.Add("initial file name", "attachdbfilename");
				hashtable.Add("connection timeout", "connect timeout");
				hashtable.Add("timeout", "connect timeout");
				hashtable.Add("language", "current language");
				hashtable.Add("addr", "data source");
				hashtable.Add("address", "data source");
				hashtable.Add("network address", "data source");
				hashtable.Add("server", "data source");
				hashtable.Add("database", "initial catalog");
				hashtable.Add("trusted_connection", "integrated security");
				hashtable.Add("connection lifetime", "load balance timeout");
				hashtable.Add("net", "network library");
				hashtable.Add("network", "network library");
				hashtable.Add("pwd", "password");
				hashtable.Add("persistsecurityinfo", "persist security info");
				hashtable.Add("uid", "user id");
				hashtable.Add("user", "user id");
				hashtable.Add("wsid", "workstation id");
				SqlConnectionString._sqlClientSynonyms = hashtable;
			}
			return hashtable;
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x000BF02C File Offset: 0x000BE42C
		internal string ObtainWorkstationId()
		{
			string text = this.WorkstationId;
			if (text == null)
			{
				text = ADP.MachineName();
				this.ValidateValueLength(text, 128, "workstation id");
			}
			return text;
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x000BF05C File Offset: 0x000BE45C
		internal static Hashtable NetlibMapping()
		{
			Hashtable hashtable = SqlConnectionString._netlibMapping;
			if (hashtable == null)
			{
				hashtable = new Hashtable(8);
				hashtable.Add("dbmssocn", "tcp");
				hashtable.Add("dbnmpntw", "np");
				hashtable.Add("dbmsrpcn", "rpc");
				hashtable.Add("dbmsvinn", "bv");
				hashtable.Add("dbmsadsn", "adsp");
				hashtable.Add("dbmsspxn", "spx");
				hashtable.Add("dbmsgnet", "via");
				hashtable.Add("dbmslpcn", "lpc");
				SqlConnectionString._netlibMapping = hashtable;
			}
			return hashtable;
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x000BF104 File Offset: 0x000BE504
		internal static bool ValidProtocal(string protocal)
		{
			return protocal == "tcp" || protocal == "np" || protocal == "via" || protocal == "lpc";
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x000BF148 File Offset: 0x000BE548
		private void ValidateValueLength(string value, int limit, string key)
		{
			if (limit < value.Length)
			{
				throw ADP.InvalidConnectionOptionValueLength(key, limit);
			}
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x000BF168 File Offset: 0x000BE568
		internal static void VerifyLocalHostAndFixup(ref string host, bool enforceLocalHost, bool fixup)
		{
			if (ADP.IsEmpty(host))
			{
				if (fixup)
				{
					host = ".";
					return;
				}
			}
			else if (!SqlConnectionString.CompareHostName(ref host, ".", fixup) && !SqlConnectionString.CompareHostName(ref host, "(local)", fixup))
			{
				string computerNameDnsFullyQualified = ADP.GetComputerNameDnsFullyQualified();
				if (!SqlConnectionString.CompareHostName(ref host, computerNameDnsFullyQualified, fixup))
				{
					int num = computerNameDnsFullyQualified.IndexOf('.');
					if ((num <= 0 || !SqlConnectionString.CompareHostName(ref host, computerNameDnsFullyQualified.Substring(0, num), fixup)) && enforceLocalHost)
					{
						throw ADP.InvalidConnectionOptionValue("attachdbfilename");
					}
				}
			}
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x000BF1E4 File Offset: 0x000BE5E4
		internal ApplicationIntent ConvertValueToApplicationIntent()
		{
			object obj = base.Parsetable["applicationintent"];
			if (obj == null)
			{
				return ApplicationIntent.ReadWrite;
			}
			ApplicationIntent result;
			try
			{
				result = DbConnectionStringBuilderUtil.ConvertToApplicationIntent("applicationintent", obj);
			}
			catch (FormatException inner)
			{
				throw ADP.InvalidConnectionOptionValue("applicationintent", inner);
			}
			catch (OverflowException inner2)
			{
				throw ADP.InvalidConnectionOptionValue("applicationintent", inner2);
			}
			return result;
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x000BF26C File Offset: 0x000BE66C
		internal PoolBlockingPeriod ConvertValueToPoolBlockingPeriod()
		{
			object obj = base.Parsetable["poolblockingperiod"];
			if (obj == null)
			{
				return PoolBlockingPeriod.Auto;
			}
			PoolBlockingPeriod result;
			try
			{
				result = DbConnectionStringBuilderUtil.ConvertToPoolBlockingPeriod("poolblockingperiod", obj);
			}
			catch (FormatException inner)
			{
				throw ADP.InvalidConnectionOptionValue("poolblockingperiod", inner);
			}
			catch (OverflowException inner2)
			{
				throw ADP.InvalidConnectionOptionValue("poolblockingperiod", inner2);
			}
			return result;
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x000BF2F4 File Offset: 0x000BE6F4
		internal SqlAuthenticationMethod ConvertValueToAuthenticationType()
		{
			object obj = base.Parsetable["authentication"];
			string text = obj as string;
			if (text == null)
			{
				return SqlConnectionString.DEFAULT.Authentication;
			}
			SqlAuthenticationMethod result;
			try
			{
				result = DbConnectionStringBuilderUtil.ConvertToAuthenticationType("authentication", text);
			}
			catch (FormatException inner)
			{
				throw ADP.InvalidConnectionOptionValue("authentication", inner);
			}
			catch (OverflowException inner2)
			{
				throw ADP.InvalidConnectionOptionValue("authentication", inner2);
			}
			return result;
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x000BF388 File Offset: 0x000BE788
		internal SqlConnectionColumnEncryptionSetting ConvertValueToColumnEncryptionSetting()
		{
			object obj = base.Parsetable["column encryption setting"];
			string text = obj as string;
			if (text == null)
			{
				return SqlConnectionString.DEFAULT.ColumnEncryptionSetting;
			}
			SqlConnectionColumnEncryptionSetting result;
			try
			{
				result = DbConnectionStringBuilderUtil.ConvertToColumnEncryptionSetting("column encryption setting", text);
			}
			catch (FormatException inner)
			{
				throw ADP.InvalidConnectionOptionValue("column encryption setting", inner);
			}
			catch (OverflowException inner2)
			{
				throw ADP.InvalidConnectionOptionValue("column encryption setting", inner2);
			}
			return result;
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x000BF41C File Offset: 0x000BE81C
		internal bool ConvertValueToEncrypt()
		{
			object obj = base.Parsetable["authentication"];
			bool defaultValue = obj != null;
			return base.ConvertValueToBoolean("encrypt", defaultValue);
		}

		// Token: 0x04000F9B RID: 3995
		internal const int SynonymCount = 21;

		// Token: 0x04000F9C RID: 3996
		private static Hashtable _sqlClientSynonyms;

		// Token: 0x04000F9D RID: 3997
		private static Hashtable _netlibMapping;

		// Token: 0x04000F9E RID: 3998
		private readonly bool _integratedSecurity;

		// Token: 0x04000F9F RID: 3999
		private readonly PoolBlockingPeriod _poolBlockingPeriod;

		// Token: 0x04000FA0 RID: 4000
		private readonly bool _connectionReset;

		// Token: 0x04000FA1 RID: 4001
		private readonly bool _contextConnection;

		// Token: 0x04000FA2 RID: 4002
		private readonly bool _encrypt;

		// Token: 0x04000FA3 RID: 4003
		private readonly bool _trustServerCertificate;

		// Token: 0x04000FA4 RID: 4004
		private readonly bool _enlist;

		// Token: 0x04000FA5 RID: 4005
		private readonly bool _mars;

		// Token: 0x04000FA6 RID: 4006
		private readonly bool _persistSecurityInfo;

		// Token: 0x04000FA7 RID: 4007
		private readonly bool _pooling;

		// Token: 0x04000FA8 RID: 4008
		private readonly bool _replication;

		// Token: 0x04000FA9 RID: 4009
		private readonly bool _userInstance;

		// Token: 0x04000FAA RID: 4010
		private readonly bool _multiSubnetFailover;

		// Token: 0x04000FAB RID: 4011
		private readonly bool _transparentNetworkIPResolution;

		// Token: 0x04000FAC RID: 4012
		private readonly SqlAuthenticationMethod _authType;

		// Token: 0x04000FAD RID: 4013
		private readonly SqlConnectionColumnEncryptionSetting _columnEncryptionSetting;

		// Token: 0x04000FAE RID: 4014
		private readonly string _enclaveAttestationUrl;

		// Token: 0x04000FAF RID: 4015
		private readonly int _connectTimeout;

		// Token: 0x04000FB0 RID: 4016
		private readonly int _loadBalanceTimeout;

		// Token: 0x04000FB1 RID: 4017
		private readonly int _maxPoolSize;

		// Token: 0x04000FB2 RID: 4018
		private readonly int _minPoolSize;

		// Token: 0x04000FB3 RID: 4019
		private readonly int _packetSize;

		// Token: 0x04000FB4 RID: 4020
		private readonly int _connectRetryCount;

		// Token: 0x04000FB5 RID: 4021
		private readonly int _connectRetryInterval;

		// Token: 0x04000FB6 RID: 4022
		private readonly ApplicationIntent _applicationIntent;

		// Token: 0x04000FB7 RID: 4023
		private readonly string _applicationName;

		// Token: 0x04000FB8 RID: 4024
		private readonly string _attachDBFileName;

		// Token: 0x04000FB9 RID: 4025
		private readonly string _currentLanguage;

		// Token: 0x04000FBA RID: 4026
		private readonly string _dataSource;

		// Token: 0x04000FBB RID: 4027
		private readonly string _localDBInstance;

		// Token: 0x04000FBC RID: 4028
		private readonly string _failoverPartner;

		// Token: 0x04000FBD RID: 4029
		private readonly string _initialCatalog;

		// Token: 0x04000FBE RID: 4030
		private readonly string _password;

		// Token: 0x04000FBF RID: 4031
		private readonly string _userID;

		// Token: 0x04000FC0 RID: 4032
		private readonly string _networkLibrary;

		// Token: 0x04000FC1 RID: 4033
		private readonly string _workstationId;

		// Token: 0x04000FC2 RID: 4034
		private readonly SqlConnectionString.TypeSystem _typeSystemVersion;

		// Token: 0x04000FC3 RID: 4035
		private readonly Version _typeSystemAssemblyVersion;

		// Token: 0x04000FC4 RID: 4036
		private static readonly Version constTypeSystemAsmVersion10 = new Version("10.0.0.0");

		// Token: 0x04000FC5 RID: 4037
		private static readonly Version constTypeSystemAsmVersion11 = new Version("11.0.0.0");

		// Token: 0x04000FC6 RID: 4038
		private readonly SqlConnectionString.TransactionBindingEnum _transactionBinding;

		// Token: 0x04000FC7 RID: 4039
		private readonly string _expandedAttachDBFilename;

		// Token: 0x020003AA RID: 938
		internal static class DEFAULT
		{
			// Token: 0x04002027 RID: 8231
			internal const ApplicationIntent ApplicationIntent = ApplicationIntent.ReadWrite;

			// Token: 0x04002028 RID: 8232
			internal const string Application_Name = ".Net SqlClient Data Provider";

			// Token: 0x04002029 RID: 8233
			internal const bool Asynchronous = false;

			// Token: 0x0400202A RID: 8234
			internal const string AttachDBFilename = "";

			// Token: 0x0400202B RID: 8235
			internal const PoolBlockingPeriod PoolBlockingPeriod = PoolBlockingPeriod.Auto;

			// Token: 0x0400202C RID: 8236
			internal const int Connect_Timeout = 15;

			// Token: 0x0400202D RID: 8237
			internal const bool Connection_Reset = true;

			// Token: 0x0400202E RID: 8238
			internal const bool Context_Connection = false;

			// Token: 0x0400202F RID: 8239
			internal const string Current_Language = "";

			// Token: 0x04002030 RID: 8240
			internal const string Data_Source = "";

			// Token: 0x04002031 RID: 8241
			internal const bool Encrypt = false;

			// Token: 0x04002032 RID: 8242
			internal const bool Enlist = true;

			// Token: 0x04002033 RID: 8243
			internal const string FailoverPartner = "";

			// Token: 0x04002034 RID: 8244
			internal const string Initial_Catalog = "";

			// Token: 0x04002035 RID: 8245
			internal const bool Integrated_Security = false;

			// Token: 0x04002036 RID: 8246
			internal const int Load_Balance_Timeout = 0;

			// Token: 0x04002037 RID: 8247
			internal const bool MARS = false;

			// Token: 0x04002038 RID: 8248
			internal const int Max_Pool_Size = 100;

			// Token: 0x04002039 RID: 8249
			internal const int Min_Pool_Size = 0;

			// Token: 0x0400203A RID: 8250
			internal const bool MultiSubnetFailover = false;

			// Token: 0x0400203B RID: 8251
			internal static readonly bool TransparentNetworkIPResolution = DbConnectionStringDefaults.TransparentNetworkIPResolution;

			// Token: 0x0400203C RID: 8252
			internal const string Network_Library = "";

			// Token: 0x0400203D RID: 8253
			internal const int Packet_Size = 8000;

			// Token: 0x0400203E RID: 8254
			internal const string Password = "";

			// Token: 0x0400203F RID: 8255
			internal const bool Persist_Security_Info = false;

			// Token: 0x04002040 RID: 8256
			internal const bool Pooling = true;

			// Token: 0x04002041 RID: 8257
			internal const bool TrustServerCertificate = false;

			// Token: 0x04002042 RID: 8258
			internal const string Type_System_Version = "";

			// Token: 0x04002043 RID: 8259
			internal const string User_ID = "";

			// Token: 0x04002044 RID: 8260
			internal const bool User_Instance = false;

			// Token: 0x04002045 RID: 8261
			internal const bool Replication = false;

			// Token: 0x04002046 RID: 8262
			internal const int Connect_Retry_Count = 1;

			// Token: 0x04002047 RID: 8263
			internal const int Connect_Retry_Interval = 10;

			// Token: 0x04002048 RID: 8264
			internal static readonly SqlAuthenticationMethod Authentication = SqlAuthenticationMethod.NotSpecified;

			// Token: 0x04002049 RID: 8265
			internal static readonly SqlConnectionColumnEncryptionSetting ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Disabled;

			// Token: 0x0400204A RID: 8266
			internal const string EnclaveAttestationUrl = "";
		}

		// Token: 0x020003AB RID: 939
		internal enum TypeSystem
		{
			// Token: 0x0400204C RID: 8268
			Latest = 2008,
			// Token: 0x0400204D RID: 8269
			SQLServer2000 = 2000,
			// Token: 0x0400204E RID: 8270
			SQLServer2005 = 2005,
			// Token: 0x0400204F RID: 8271
			SQLServer2008 = 2008,
			// Token: 0x04002050 RID: 8272
			SQLServer2012 = 2012
		}

		// Token: 0x020003AC RID: 940
		internal enum TransactionBindingEnum
		{
			// Token: 0x04002052 RID: 8274
			ImplicitUnbind,
			// Token: 0x04002053 RID: 8275
			ExplicitUnbind
		}
	}
}
