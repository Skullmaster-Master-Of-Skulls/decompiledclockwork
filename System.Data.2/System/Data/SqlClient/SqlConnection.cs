using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020001B5 RID: 437
	[DefaultEvent("InfoMessage")]
	public sealed class SqlConnection : DbConnection, ICloneable
	{
		// Token: 0x06001A32 RID: 6706 RVA: 0x000BA54C File Offset: 0x000B994C
		static SqlConnection()
		{
			SqlColumnEncryptionEnclaveProviderConfigurationSection configSection = null;
			try
			{
				configSection = (SqlColumnEncryptionEnclaveProviderConfigurationSection)ConfigurationManager.GetSection("SqlColumnEncryptionEnclaveProviders");
			}
			catch (ConfigurationErrorsException innerException)
			{
				throw SQL.CannotGetSqlColumnEncryptionEnclaveProviderConfig(innerException);
			}
			SqlConnection.sqlColumnEncryptionEnclaveProviderConfigurationManager = new SqlColumnEncryptionEnclaveProviderConfigurationManager(configSection);
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x000BA638 File Offset: 0x000B9A38
		[DefaultValue(null)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("TCE_SqlConnection_TrustedColumnMasterKeyPaths")]
		public static IDictionary<string, IList<string>> ColumnEncryptionTrustedMasterKeyPaths
		{
			get
			{
				return SqlConnection._ColumnEncryptionTrustedMasterKeyPaths;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001A34 RID: 6708 RVA: 0x000BA64C File Offset: 0x000B9A4C
		// (set) Token: 0x06001A35 RID: 6709 RVA: 0x000BA660 File Offset: 0x000B9A60
		[DefaultValue(null)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("TCE_SqlConnection_ColumnEncryptionQueryMetadataCacheEnabled")]
		public static bool ColumnEncryptionQueryMetadataCacheEnabled
		{
			get
			{
				return SqlConnection._ColumnEncryptionQueryMetadataCacheEnabled;
			}
			set
			{
				SqlConnection._ColumnEncryptionQueryMetadataCacheEnabled = value;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x000BA674 File Offset: 0x000B9A74
		// (set) Token: 0x06001A37 RID: 6711 RVA: 0x000BA688 File Offset: 0x000B9A88
		[ResDescription("TCE_SqlConnection_ColumnEncryptionKeyCacheTtl")]
		[DefaultValue(null)]
		[ResCategory("DataCategory_Data")]
		public static TimeSpan ColumnEncryptionKeyCacheTtl
		{
			get
			{
				return SqlConnection._ColumnEncryptionKeyCacheTtl;
			}
			set
			{
				SqlConnection._ColumnEncryptionKeyCacheTtl = value;
			}
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x000BA69C File Offset: 0x000B9A9C
		public static void RegisterColumnEncryptionKeyStoreProviders(IDictionary<string, SqlColumnEncryptionKeyStoreProvider> customProviders)
		{
			if (customProviders == null)
			{
				throw SQL.NullCustomKeyStoreProviderDictionary();
			}
			foreach (string text in customProviders.Keys)
			{
				if (string.IsNullOrWhiteSpace(text))
				{
					throw SQL.EmptyProviderName();
				}
				if (text.StartsWith("MSSQL_", StringComparison.InvariantCultureIgnoreCase))
				{
					throw SQL.InvalidCustomKeyStoreProviderName(text, "MSSQL_");
				}
				if (customProviders[text] == null)
				{
					throw SQL.NullProviderValue(text);
				}
			}
			object customColumnEncryptionKeyProvidersLock = SqlConnection._CustomColumnEncryptionKeyProvidersLock;
			lock (customColumnEncryptionKeyProvidersLock)
			{
				if (SqlConnection._CustomColumnEncryptionKeyStoreProviders != null)
				{
					throw SQL.CanOnlyCallOnce();
				}
				Dictionary<string, SqlColumnEncryptionKeyStoreProvider> dictionary = new Dictionary<string, SqlColumnEncryptionKeyStoreProvider>(customProviders, StringComparer.OrdinalIgnoreCase);
				SqlConnection._CustomColumnEncryptionKeyStoreProviders = new ReadOnlyDictionary<string, SqlColumnEncryptionKeyStoreProvider>(dictionary);
			}
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x000BA78C File Offset: 0x000B9B8C
		internal static bool TryGetColumnEncryptionKeyStoreProvider(string providerName, out SqlColumnEncryptionKeyStoreProvider columnKeyStoreProvider)
		{
			columnKeyStoreProvider = null;
			if (SqlConnection._SystemColumnEncryptionKeyStoreProviders.TryGetValue(providerName, out columnKeyStoreProvider))
			{
				return true;
			}
			object customColumnEncryptionKeyProvidersLock = SqlConnection._CustomColumnEncryptionKeyProvidersLock;
			bool result;
			lock (customColumnEncryptionKeyProvidersLock)
			{
				if (SqlConnection._CustomColumnEncryptionKeyStoreProviders == null)
				{
					result = false;
				}
				else
				{
					result = SqlConnection._CustomColumnEncryptionKeyStoreProviders.TryGetValue(providerName, out columnKeyStoreProvider);
				}
			}
			return result;
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x000BA800 File Offset: 0x000B9C00
		internal static List<string> GetColumnEncryptionSystemKeyStoreProviders()
		{
			HashSet<string> source = new HashSet<string>(SqlConnection._SystemColumnEncryptionKeyStoreProviders.Keys);
			return source.ToList<string>();
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000BA824 File Offset: 0x000B9C24
		internal static List<string> GetColumnEncryptionCustomKeyStoreProviders()
		{
			if (SqlConnection._CustomColumnEncryptionKeyStoreProviders != null)
			{
				HashSet<string> source = new HashSet<string>(SqlConnection._CustomColumnEncryptionKeyStoreProviders.Keys);
				return source.ToList<string>();
			}
			return new List<string>();
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x000BA854 File Offset: 0x000B9C54
		public SqlConnection(string connectionString) : this(connectionString, null)
		{
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x000BA86C File Offset: 0x000B9C6C
		public SqlConnection(string connectionString, SqlCredential credential) : this()
		{
			this.ConnectionString = connectionString;
			if (credential != null)
			{
				SqlConnectionString opt = (SqlConnectionString)this.ConnectionOptions;
				if (this.UsesClearUserIdOrPassword(opt))
				{
					throw ADP.InvalidMixedArgumentOfSecureAndClearCredential();
				}
				if (this.UsesIntegratedSecurity(opt))
				{
					throw ADP.InvalidMixedArgumentOfSecureCredentialAndIntegratedSecurity();
				}
				if (this.UsesContextConnection(opt))
				{
					throw ADP.InvalidMixedArgumentOfSecureCredentialAndContextConnection();
				}
				if (this.UsesActiveDirectoryIntegrated(opt))
				{
					throw SQL.SettingCredentialWithIntegratedArgument();
				}
				this.Credential = credential;
			}
			this.CacheConnectionStringProperties();
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x000BA8E0 File Offset: 0x000B9CE0
		private SqlConnection(SqlConnection connection)
		{
			this._reconnectLock = new object();
			this._originalConnectionId = Guid.Empty;
			this.ObjectID = Interlocked.Increment(ref SqlConnection._objectTypeCount);
			base..ctor();
			GC.SuppressFinalize(this);
			this.CopyFrom(connection);
			this._connectionString = connection._connectionString;
			if (connection._credential != null)
			{
				SecureString secureString = connection._credential.Password.Copy();
				secureString.MakeReadOnly();
				this._credential = new SqlCredential(connection._credential.UserId, secureString);
			}
			this._accessToken = connection._accessToken;
			this.CacheConnectionStringProperties();
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x000BA97C File Offset: 0x000B9D7C
		private void CacheConnectionStringProperties()
		{
			SqlConnectionString sqlConnectionString = this.ConnectionOptions as SqlConnectionString;
			if (sqlConnectionString != null)
			{
				this._connectRetryCount = sqlConnectionString.ConnectRetryCount;
				if (this._connectRetryCount == 1 && ADP.IsAzureSqlServerEndpoint(sqlConnectionString.DataSource))
				{
					this._connectRetryCount = 2;
				}
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x000BA9C4 File Offset: 0x000B9DC4
		// (set) Token: 0x06001A41 RID: 6721 RVA: 0x000BA9D8 File Offset: 0x000B9DD8
		[DefaultValue(false)]
		[ResDescription("SqlConnection_StatisticsEnabled")]
		[ResCategory("DataCategory_Data")]
		public bool StatisticsEnabled
		{
			get
			{
				return this._collectstats;
			}
			set
			{
				if (this.IsContextConnection)
				{
					if (value)
					{
						throw SQL.NotAvailableOnContextConnection();
					}
				}
				else
				{
					if (value)
					{
						if (ConnectionState.Open == this.State)
						{
							if (this._statistics == null)
							{
								this._statistics = new SqlStatistics();
								ADP.TimerCurrent(out this._statistics._openTimestamp);
							}
							this.Parser.Statistics = this._statistics;
						}
					}
					else if (this._statistics != null && ConnectionState.Open == this.State)
					{
						TdsParser parser = this.Parser;
						parser.Statistics = null;
						ADP.TimerCurrent(out this._statistics._closeTimestamp);
					}
					this._collectstats = value;
				}
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x000BAA74 File Offset: 0x000B9E74
		// (set) Token: 0x06001A43 RID: 6723 RVA: 0x000BAA88 File Offset: 0x000B9E88
		internal bool AsyncCommandInProgress
		{
			get
			{
				return this._AsyncCommandInProgress;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this._AsyncCommandInProgress = value;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x000BAA9C File Offset: 0x000B9E9C
		internal bool IsContextConnection
		{
			get
			{
				SqlConnectionString opt = (SqlConnectionString)this.ConnectionOptions;
				return this.UsesContextConnection(opt);
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001A45 RID: 6725 RVA: 0x000BAABC File Offset: 0x000B9EBC
		internal bool IsColumnEncryptionSettingEnabled
		{
			get
			{
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				return sqlConnectionString != null && sqlConnectionString.ColumnEncryptionSetting == SqlConnectionColumnEncryptionSetting.Enabled;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x000BAAE4 File Offset: 0x000B9EE4
		internal string EnclaveAttestationUrl
		{
			get
			{
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				return sqlConnectionString.EnclaveAttestationUrl;
			}
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x000BAB04 File Offset: 0x000B9F04
		private bool UsesContextConnection(SqlConnectionString opt)
		{
			return opt != null && opt.ContextConnection;
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x000BAB1C File Offset: 0x000B9F1C
		private bool UsesActiveDirectoryIntegrated(SqlConnectionString opt)
		{
			return opt != null && opt.Authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x000BAB38 File Offset: 0x000B9F38
		private bool UsesAuthentication(SqlConnectionString opt)
		{
			return opt != null && opt.Authentication > SqlAuthenticationMethod.NotSpecified;
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x000BAB54 File Offset: 0x000B9F54
		private bool UsesIntegratedSecurity(SqlConnectionString opt)
		{
			return opt != null && opt.IntegratedSecurity;
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x000BAB6C File Offset: 0x000B9F6C
		private bool UsesClearUserIdOrPassword(SqlConnectionString opt)
		{
			bool result = false;
			if (opt != null)
			{
				result = (!ADP.IsEmpty(opt.UserID) || !ADP.IsEmpty(opt.Password));
			}
			return result;
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x000BABA0 File Offset: 0x000B9FA0
		internal SqlConnectionString.TransactionBindingEnum TransactionBinding
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).TransactionBinding;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001A4D RID: 6733 RVA: 0x000BABC0 File Offset: 0x000B9FC0
		internal SqlConnectionString.TypeSystem TypeSystem
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).TypeSystemVersion;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001A4E RID: 6734 RVA: 0x000BABE0 File Offset: 0x000B9FE0
		internal Version TypeSystemAssemblyVersion
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).TypeSystemAssemblyVersion;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x000BAC00 File Offset: 0x000BA000
		internal PoolBlockingPeriod PoolBlockingPeriod
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).PoolBlockingPeriod;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001A50 RID: 6736 RVA: 0x000BAC20 File Offset: 0x000BA020
		internal int ConnectRetryInterval
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).ConnectRetryInterval;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001A51 RID: 6737 RVA: 0x000BAC40 File Offset: 0x000BA040
		protected override DbProviderFactory DbProviderFactory
		{
			get
			{
				return SqlClientFactory.Instance;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x000BAC54 File Offset: 0x000BA054
		// (set) Token: 0x06001A53 RID: 6739 RVA: 0x000BAC90 File Offset: 0x000BA090
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("SqlConnection_AccessToken")]
		[Browsable(false)]
		public string AccessToken
		{
			get
			{
				string result = this._accessToken;
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.UserConnectionOptions;
				if (this.InnerConnection.ShouldHidePassword && sqlConnectionString != null && !sqlConnectionString.PersistSecurityInfo)
				{
					result = null;
				}
				return result;
			}
			set
			{
				if (!this.InnerConnection.AllowSetConnectionString)
				{
					throw ADP.OpenConnectionPropertySet("AccessToken", this.InnerConnection.State);
				}
				if (value != null)
				{
					this.CheckAndThrowOnInvalidCombinationOfConnectionOptionAndAccessToken((SqlConnectionString)this.ConnectionOptions);
				}
				this._accessToken = value;
				this.ConnectionString_Set(new SqlConnectionPoolKey(this._connectionString, this._credential, this._accessToken));
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x000BACF8 File Offset: 0x000BA0F8
		// (set) Token: 0x06001A55 RID: 6741 RVA: 0x000BAD0C File Offset: 0x000BA10C
		[ResDescription("SqlConnection_ConnectionString")]
		[DefaultValue("")]
		[RecommendedAsConfigurable(true)]
		[SettingsBindable(true)]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Data")]
		[Editor("Microsoft.VSDesigner.Data.SQL.Design.SqlConnectionStringEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public override string ConnectionString
		{
			get
			{
				return this.ConnectionString_Get();
			}
			set
			{
				if (this._credential != null || this._accessToken != null)
				{
					SqlConnectionString sqlConnectionString = new SqlConnectionString(value);
					if (this._credential != null)
					{
						if (this.UsesActiveDirectoryIntegrated(sqlConnectionString))
						{
							throw SQL.SettingIntegratedWithCredential();
						}
						this.CheckAndThrowOnInvalidCombinationOfConnectionStringAndSqlCredential(sqlConnectionString);
					}
					else if (this._accessToken != null)
					{
						this.CheckAndThrowOnInvalidCombinationOfConnectionOptionAndAccessToken(sqlConnectionString);
					}
				}
				this.ConnectionString_Set(new SqlConnectionPoolKey(value, this._credential, this._accessToken));
				this._connectionString = value;
				this.CacheConnectionStringProperties();
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x000BAD84 File Offset: 0x000BA184
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("SqlConnection_ConnectionTimeout")]
		public override int ConnectionTimeout
		{
			get
			{
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				if (sqlConnectionString == null)
				{
					return 15;
				}
				return sqlConnectionString.ConnectTimeout;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x000BADAC File Offset: 0x000BA1AC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("SqlConnection_Database")]
		public override string Database
		{
			get
			{
				SqlInternalConnection sqlInternalConnection = this.InnerConnection as SqlInternalConnection;
				string result;
				if (sqlInternalConnection != null)
				{
					result = sqlInternalConnection.CurrentDatabase;
				}
				else
				{
					SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
					result = ((sqlConnectionString != null) ? sqlConnectionString.InitialCatalog : "");
				}
				return result;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x000BADF0 File Offset: 0x000BA1F0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("SqlConnection_DataSource")]
		[Browsable(true)]
		public override string DataSource
		{
			get
			{
				SqlInternalConnection sqlInternalConnection = this.InnerConnection as SqlInternalConnection;
				string result;
				if (sqlInternalConnection != null)
				{
					result = sqlInternalConnection.CurrentDataSource;
				}
				else
				{
					SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
					result = ((sqlConnectionString != null) ? sqlConnectionString.DataSource : "");
				}
				return result;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001A59 RID: 6745 RVA: 0x000BAE34 File Offset: 0x000BA234
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("SqlConnection_PacketSize")]
		public int PacketSize
		{
			get
			{
				if (this.IsContextConnection)
				{
					throw SQL.NotAvailableOnContextConnection();
				}
				SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
				int result;
				if (sqlInternalConnectionTds != null)
				{
					result = sqlInternalConnectionTds.PacketSize;
				}
				else
				{
					SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
					result = ((sqlConnectionString != null) ? sqlConnectionString.PacketSize : 8000);
				}
				return result;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001A5A RID: 6746 RVA: 0x000BAE88 File Offset: 0x000BA288
		[ResDescription("SqlConnection_ClientConnectionId")]
		[ResCategory("DataCategory_Data")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Guid ClientConnectionId
		{
			get
			{
				SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
				if (sqlInternalConnectionTds != null)
				{
					return sqlInternalConnectionTds.ClientConnectionId;
				}
				Task currentReconnectionTask = this._currentReconnectionTask;
				if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
				{
					return this._originalConnectionId;
				}
				return Guid.Empty;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001A5B RID: 6747 RVA: 0x000BAECC File Offset: 0x000BA2CC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[ResDescription("SqlConnection_ServerVersion")]
		public override string ServerVersion
		{
			get
			{
				return this.GetOpenConnection().ServerVersion;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x000BAEE4 File Offset: 0x000BA2E4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("DbConnection_State")]
		[Browsable(false)]
		public override ConnectionState State
		{
			get
			{
				Task currentReconnectionTask = this._currentReconnectionTask;
				if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
				{
					return ConnectionState.Open;
				}
				return this.InnerConnection.State;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x000BAF10 File Offset: 0x000BA310
		internal SqlStatistics Statistics
		{
			get
			{
				return this._statistics;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x000BAF24 File Offset: 0x000BA324
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("SqlConnection_WorkstationId")]
		public string WorkstationId
		{
			get
			{
				if (this.IsContextConnection)
				{
					throw SQL.NotAvailableOnContextConnection();
				}
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				string text = (sqlConnectionString != null) ? sqlConnectionString.WorkstationId : null;
				if (text == null)
				{
					text = Environment.MachineName;
				}
				return text;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001A5F RID: 6751 RVA: 0x000BAF64 File Offset: 0x000BA364
		// (set) Token: 0x06001A60 RID: 6752 RVA: 0x000BAFA0 File Offset: 0x000BA3A0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("SqlConnection_Credential")]
		[Browsable(false)]
		public SqlCredential Credential
		{
			get
			{
				SqlCredential result = this._credential;
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.UserConnectionOptions;
				if (this.InnerConnection.ShouldHidePassword && sqlConnectionString != null && !sqlConnectionString.PersistSecurityInfo)
				{
					result = null;
				}
				return result;
			}
			set
			{
				if (!this.InnerConnection.AllowSetConnectionString)
				{
					throw ADP.OpenConnectionPropertySet("Credential", this.InnerConnection.State);
				}
				if (value != null)
				{
					if (this.UsesActiveDirectoryIntegrated((SqlConnectionString)this.ConnectionOptions))
					{
						throw SQL.SettingCredentialWithIntegratedInvalid();
					}
					this.CheckAndThrowOnInvalidCombinationOfConnectionStringAndSqlCredential((SqlConnectionString)this.ConnectionOptions);
					if (this._accessToken != null)
					{
						throw ADP.InvalidMixedUsageOfCredentialAndAccessToken();
					}
				}
				this._credential = value;
				this.ConnectionString_Set(new SqlConnectionPoolKey(this._connectionString, this._credential, this._accessToken));
			}
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x000BB030 File Offset: 0x000BA430
		private void CheckAndThrowOnInvalidCombinationOfConnectionStringAndSqlCredential(SqlConnectionString connectionOptions)
		{
			if (this.UsesClearUserIdOrPassword(connectionOptions))
			{
				throw ADP.InvalidMixedUsageOfSecureAndClearCredential();
			}
			if (this.UsesIntegratedSecurity(connectionOptions))
			{
				throw ADP.InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity();
			}
			if (this.UsesContextConnection(connectionOptions))
			{
				throw ADP.InvalidMixedArgumentOfSecureCredentialAndContextConnection();
			}
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x000BB06C File Offset: 0x000BA46C
		private void CheckAndThrowOnInvalidCombinationOfConnectionOptionAndAccessToken(SqlConnectionString connectionOptions)
		{
			if (this.UsesClearUserIdOrPassword(connectionOptions))
			{
				throw ADP.InvalidMixedUsageOfAccessTokenAndUserIDPassword();
			}
			if (this.UsesIntegratedSecurity(connectionOptions))
			{
				throw ADP.InvalidMixedUsageOfAccessTokenAndIntegratedSecurity();
			}
			if (this.UsesContextConnection(connectionOptions))
			{
				throw ADP.InvalidMixedUsageOfAccessTokenAndContextConnection();
			}
			if (this.UsesAuthentication(connectionOptions))
			{
				throw ADP.InvalidMixedUsageOfAccessTokenAndAuthentication();
			}
			if (this._credential != null)
			{
				throw ADP.InvalidMixedUsageOfAccessTokenAndCredential();
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06001A63 RID: 6755 RVA: 0x000BB0C4 File Offset: 0x000BA4C4
		// (remove) Token: 0x06001A64 RID: 6756 RVA: 0x000BB0E4 File Offset: 0x000BA4E4
		[ResDescription("DbConnection_InfoMessage")]
		[ResCategory("DataCategory_InfoMessage")]
		public event SqlInfoMessageEventHandler InfoMessage
		{
			add
			{
				base.Events.AddHandler(SqlConnection.EventInfoMessage, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlConnection.EventInfoMessage, value);
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x000BB104 File Offset: 0x000BA504
		// (set) Token: 0x06001A66 RID: 6758 RVA: 0x000BB118 File Offset: 0x000BA518
		public bool FireInfoMessageEventOnUserErrors
		{
			get
			{
				return this._fireInfoMessageEventOnUserErrors;
			}
			set
			{
				this._fireInfoMessageEventOnUserErrors = value;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001A67 RID: 6759 RVA: 0x000BB12C File Offset: 0x000BA52C
		internal int ReconnectCount
		{
			get
			{
				return this._reconnectCount;
			}
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x000BB140 File Offset: 0x000BA540
		public new SqlTransaction BeginTransaction()
		{
			return this.BeginTransaction(IsolationLevel.Unspecified, null);
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x000BB158 File Offset: 0x000BA558
		public new SqlTransaction BeginTransaction(IsolationLevel iso)
		{
			return this.BeginTransaction(iso, null);
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x000BB170 File Offset: 0x000BA570
		public SqlTransaction BeginTransaction(string transactionName)
		{
			return this.BeginTransaction(IsolationLevel.Unspecified, transactionName);
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x000BB188 File Offset: 0x000BA588
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<prov.SqlConnection.BeginDbTransaction|API> %d#, isolationLevel=%d{ds.IsolationLevel}", this.ObjectID, (int)isolationLevel);
			DbTransaction result;
			try
			{
				DbTransaction dbTransaction = this.BeginTransaction(isolationLevel);
				GC.KeepAlive(this);
				result = dbTransaction;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x000BB1E0 File Offset: 0x000BA5E0
		public SqlTransaction BeginTransaction(IsolationLevel iso, string transactionName)
		{
			this.WaitForPendingReconnection();
			SqlStatistics statistics = null;
			string a = ADP.IsEmpty(transactionName) ? "None" : transactionName;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlConnection.BeginTransaction|API> %d#, iso=%d{ds.IsolationLevel}, transactionName='%ls'\n", this.ObjectID, (int)iso, a);
			SqlTransaction result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				bool shouldReconnect = true;
				SqlTransaction sqlTransaction;
				do
				{
					sqlTransaction = this.GetOpenConnection().BeginSqlTransaction(iso, transactionName, shouldReconnect);
					shouldReconnect = false;
				}
				while (sqlTransaction.InternalTransaction.ConnectionHasBeenRestored);
				GC.KeepAlive(this);
				result = sqlTransaction;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x000BB280 File Offset: 0x000BA680
		public override void ChangeDatabase(string database)
		{
			SqlStatistics statistics = null;
			this.RepairInnerConnection();
			Bid.CorrelationTrace("<sc.SqlConnection.ChangeDatabase|API|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this);
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.InnerConnection.ChangeDatabase(database);
			}
			catch (OutOfMemoryException e)
			{
				this.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				this.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this.Abort(e3);
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x000BB368 File Offset: 0x000BA768
		public static void ClearAllPools()
		{
			new SqlClientPermission(PermissionState.Unrestricted).Demand();
			SqlConnectionFactory.SingletonInstance.ClearAllPools();
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x000BB38C File Offset: 0x000BA78C
		public static void ClearPool(SqlConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			DbConnectionOptions userConnectionOptions = connection.UserConnectionOptions;
			if (userConnectionOptions != null)
			{
				userConnectionOptions.DemandPermission();
				if (connection.IsContextConnection)
				{
					throw SQL.NotAvailableOnContextConnection();
				}
				SqlConnectionFactory.SingletonInstance.ClearPool(connection);
			}
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x000BB3D0 File Offset: 0x000BA7D0
		object ICloneable.Clone()
		{
			SqlConnection sqlConnection = new SqlConnection(this);
			Bid.Trace("<sc.SqlConnection.Clone|API> %d#, clone=%d#\n", this.ObjectID, sqlConnection.ObjectID);
			return sqlConnection;
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x000BB3FC File Offset: 0x000BA7FC
		private void CloseInnerConnection()
		{
			this.InnerConnection.CloseConnection(this, this.ConnectionFactory);
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x000BB41C File Offset: 0x000BA81C
		public override void Close()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlConnection.Close|API> %d#", this.ObjectID);
			Bid.CorrelationTrace("<sc.SqlConnection.Close|API|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			try
			{
				SqlStatistics statistics = null;
				TdsParser target = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					target = SqlInternalConnection.GetBestEffortCleanupTarget(this);
					statistics = SqlStatistics.StartTimer(this.Statistics);
					Task currentReconnectionTask = this._currentReconnectionTask;
					if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
					{
						CancellationTokenSource reconnectionCancellationSource = this._reconnectionCancellationSource;
						if (reconnectionCancellationSource != null)
						{
							reconnectionCancellationSource.Cancel();
						}
						AsyncHelper.WaitForCompletion(currentReconnectionTask, 0, null, false);
						if (this.State != ConnectionState.Open)
						{
							this.OnStateChange(DbConnectionInternal.StateChangeClosed);
						}
					}
					this.CancelOpenAndWait();
					this.CloseInnerConnection();
					GC.SuppressFinalize(this);
					if (this.Statistics != null)
					{
						ADP.TimerCurrent(out this._statistics._closeTimestamp);
					}
				}
				catch (OutOfMemoryException e)
				{
					this.Abort(e);
					throw;
				}
				catch (StackOverflowException e2)
				{
					this.Abort(e2);
					throw;
				}
				catch (ThreadAbortException e3)
				{
					this.Abort(e3);
					SqlInternalConnection.BestEffortCleanup(target);
					throw;
				}
				finally
				{
					SqlStatistics.StopTimer(statistics);
					if (this._lastIdentity != null)
					{
						this._lastIdentity.Dispose();
					}
				}
			}
			finally
			{
				SqlDebugContext sdc = this._sdc;
				this._sdc = null;
				Bid.ScopeLeave(ref intPtr);
				if (sdc != null)
				{
					sdc.Dispose();
				}
			}
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x000BB5C0 File Offset: 0x000BA9C0
		public new SqlCommand CreateCommand()
		{
			return new SqlCommand(null, this);
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x000BB5D4 File Offset: 0x000BA9D4
		private void DisposeMe(bool disposing)
		{
			this._credential = null;
			this._accessToken = null;
			if (!disposing)
			{
				SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
				if (sqlInternalConnectionTds != null && !sqlInternalConnectionTds.ConnectionOptions.Pooling)
				{
					TdsParser parser = sqlInternalConnectionTds.Parser;
					if (parser != null && parser._physicalStateObj != null)
					{
						parser._physicalStateObj.DecrementPendingCallbacks(false);
					}
				}
			}
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x000BB630 File Offset: 0x000BAA30
		public void EnlistDistributedTransaction(ITransaction transaction)
		{
			if (this.IsContextConnection)
			{
				throw SQL.NotAvailableOnContextConnection();
			}
			this.EnlistDistributedTransactionHelper(transaction);
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x000BB654 File Offset: 0x000BAA54
		public override void Open()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlConnection.Open|API> %d#", this.ObjectID);
			Bid.CorrelationTrace("<sc.SqlConnection.Open|API|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			try
			{
				if (this.StatisticsEnabled)
				{
					if (this._statistics == null)
					{
						this._statistics = new SqlStatistics();
					}
					else
					{
						this._statistics.ContinueOnNewConnection();
					}
				}
				SqlStatistics statistics = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					statistics = SqlStatistics.StartTimer(this.Statistics);
					if (!this.TryOpen(null))
					{
						throw ADP.InternalError(ADP.InternalErrorCode.SynchronousConnectReturnedPending);
					}
				}
				finally
				{
					SqlStatistics.StopTimer(statistics);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x000BB718 File Offset: 0x000BAB18
		internal void RegisterWaitingForReconnect(Task waitingTask)
		{
			if (((SqlConnectionString)this.ConnectionOptions).MARS)
			{
				return;
			}
			Interlocked.CompareExchange<Task>(ref this._asyncWaitingForReconnection, waitingTask, null);
			if (this._asyncWaitingForReconnection != waitingTask)
			{
				throw SQL.MARSUnspportedOnConnection();
			}
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x000BB758 File Offset: 0x000BAB58
		private Task ReconnectAsync(int timeout)
		{
			SqlConnection.<ReconnectAsync>d__127 <ReconnectAsync>d__;
			<ReconnectAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ReconnectAsync>d__.<>4__this = this;
			<ReconnectAsync>d__.timeout = timeout;
			<ReconnectAsync>d__.<>1__state = -1;
			<ReconnectAsync>d__.<>t__builder.Start<SqlConnection.<ReconnectAsync>d__127>(ref <ReconnectAsync>d__);
			return <ReconnectAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x000BB7A4 File Offset: 0x000BABA4
		internal Task ValidateAndReconnect(Action beforeDisconnect, int timeout)
		{
			Task task = this._currentReconnectionTask;
			while (task != null && task.IsCompleted)
			{
				Interlocked.CompareExchange<Task>(ref this._currentReconnectionTask, null, task);
				task = this._currentReconnectionTask;
			}
			if (task == null)
			{
				if (this._connectRetryCount > 0)
				{
					SqlInternalConnectionTds openTdsConnection = this.GetOpenTdsConnection();
					if (openTdsConnection._sessionRecoveryAcknowledged)
					{
						TdsParserStateObject physicalStateObj = openTdsConnection.Parser._physicalStateObj;
						if (!physicalStateObj.ValidateSNIConnection())
						{
							if (openTdsConnection.Parser._sessionPool != null && openTdsConnection.Parser._sessionPool.ActiveSessionsCount > 0)
							{
								if (beforeDisconnect != null)
								{
									beforeDisconnect();
								}
								this.OnError(SQL.CR_UnrecoverableClient(this.ClientConnectionId), true, null);
							}
							SessionData currentSessionData = openTdsConnection.CurrentSessionData;
							if (currentSessionData._unrecoverableStatesCount == 0)
							{
								bool flag = false;
								object reconnectLock = this._reconnectLock;
								lock (reconnectLock)
								{
									openTdsConnection.CheckEnlistedTransactionBinding();
									task = this._currentReconnectionTask;
									if (task == null)
									{
										if (currentSessionData._unrecoverableStatesCount == 0)
										{
											this._originalConnectionId = this.ClientConnectionId;
											Bid.Trace("<sc.SqlConnection.ReconnectIfNeeded|INFO> Connection ClientConnectionID %ls is invalid, reconnecting\n", this._originalConnectionId.ToString());
											this._recoverySessionData = currentSessionData;
											if (beforeDisconnect != null)
											{
												beforeDisconnect();
											}
											try
											{
												this._supressStateChangeForReconnection = true;
												openTdsConnection.DoomThisConnection();
											}
											catch (SqlException)
											{
											}
											task = Task.Run(() => this.ReconnectAsync(timeout));
											this._currentReconnectionTask = task;
										}
									}
									else
									{
										flag = true;
									}
								}
								if (flag && beforeDisconnect != null)
								{
									beforeDisconnect();
								}
							}
							else
							{
								if (beforeDisconnect != null)
								{
									beforeDisconnect();
								}
								this.OnError(SQL.CR_UnrecoverableServer(this.ClientConnectionId), true, null);
							}
						}
					}
				}
			}
			else if (beforeDisconnect != null)
			{
				beforeDisconnect();
			}
			return task;
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x000BB98C File Offset: 0x000BAD8C
		private void WaitForPendingReconnection()
		{
			Task currentReconnectionTask = this._currentReconnectionTask;
			if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
			{
				AsyncHelper.WaitForCompletion(currentReconnectionTask, 0, null, false);
			}
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x000BB9B4 File Offset: 0x000BADB4
		private void CancelOpenAndWait()
		{
			Tuple<TaskCompletionSource<DbConnectionInternal>, Task> currentCompletion = this._currentCompletion;
			if (currentCompletion != null)
			{
				currentCompletion.Item1.TrySetCanceled();
				((IAsyncResult)currentCompletion.Item2).AsyncWaitHandle.WaitOne();
			}
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x000BB9E8 File Offset: 0x000BADE8
		public override Task OpenAsync(CancellationToken cancellationToken)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlConnection.OpenAsync|API> %d#", this.ObjectID);
			Bid.CorrelationTrace("<sc.SqlConnection.OpenAsync|API|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			Task task;
			try
			{
				if (this.StatisticsEnabled)
				{
					if (this._statistics == null)
					{
						this._statistics = new SqlStatistics();
					}
					else
					{
						this._statistics.ContinueOnNewConnection();
					}
				}
				SqlStatistics statistics = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					statistics = SqlStatistics.StartTimer(this.Statistics);
					Transaction currentTransaction = ADP.GetCurrentTransaction();
					TaskCompletionSource<DbConnectionInternal> completion = new TaskCompletionSource<DbConnectionInternal>(currentTransaction);
					TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();
					if (cancellationToken.IsCancellationRequested)
					{
						taskCompletionSource.SetCanceled();
						task = taskCompletionSource.Task;
					}
					else if (this.IsContextConnection)
					{
						taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(SQL.NotAvailableOnContextConnection()));
						task = taskCompletionSource.Task;
					}
					else
					{
						bool flag;
						try
						{
							flag = this.TryOpen(completion);
						}
						catch (Exception exception)
						{
							taskCompletionSource.SetException(exception);
							return taskCompletionSource.Task;
						}
						if (flag)
						{
							taskCompletionSource.SetResult(null);
							task = taskCompletionSource.Task;
						}
						else
						{
							CancellationTokenRegistration registration = default(CancellationTokenRegistration);
							if (cancellationToken.CanBeCanceled)
							{
								registration = cancellationToken.Register(delegate()
								{
									completion.TrySetCanceled();
								});
							}
							SqlConnection.OpenAsyncRetry @object = new SqlConnection.OpenAsyncRetry(this, completion, taskCompletionSource, registration);
							this._currentCompletion = new Tuple<TaskCompletionSource<DbConnectionInternal>, Task>(completion, taskCompletionSource.Task);
							completion.Task.ContinueWith(new Action<Task<DbConnectionInternal>>(@object.Retry), TaskScheduler.Default);
							task = taskCompletionSource.Task;
						}
					}
				}
				finally
				{
					SqlStatistics.StopTimer(statistics);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return task;
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x000BBBCC File Offset: 0x000BAFCC
		private bool TryOpen(TaskCompletionSource<DbConnectionInternal> retry)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
			this._applyTransientFaultHandling = (retry == null && sqlConnectionString != null && sqlConnectionString.ConnectRetryCount > 0);
			if (sqlConnectionString != null && (sqlConnectionString.Authentication == SqlAuthenticationMethod.SqlPassword || sqlConnectionString.Authentication == SqlAuthenticationMethod.ActiveDirectoryPassword) && (!sqlConnectionString.HasUserIdKeyword || !sqlConnectionString.HasPasswordKeyword) && this._credential == null)
			{
				throw SQL.CredentialsNotProvided(sqlConnectionString.Authentication);
			}
			if (this._impersonateIdentity != null)
			{
				using (WindowsIdentity currentWindowsIdentity = DbConnectionPoolIdentity.GetCurrentWindowsIdentity())
				{
					if (this._impersonateIdentity.User == currentWindowsIdentity.User)
					{
						return this.TryOpenInner(retry);
					}
					using (this._impersonateIdentity.Impersonate())
					{
						return this.TryOpenInner(retry);
					}
				}
			}
			if (this.UsesIntegratedSecurity(sqlConnectionString) || this.UsesActiveDirectoryIntegrated(sqlConnectionString))
			{
				this._lastIdentity = DbConnectionPoolIdentity.GetCurrentWindowsIdentity();
			}
			else
			{
				this._lastIdentity = null;
			}
			return this.TryOpenInner(retry);
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x000BBCF8 File Offset: 0x000BB0F8
		private bool TryOpenInner(TaskCompletionSource<DbConnectionInternal> retry)
		{
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (base.ForceNewConnection)
				{
					if (!this.InnerConnection.TryReplaceConnection(this, this.ConnectionFactory, retry, this.UserConnectionOptions))
					{
						return false;
					}
				}
				else if (!this.InnerConnection.TryOpenConnection(this, this.ConnectionFactory, retry, this.UserConnectionOptions))
				{
					return false;
				}
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this);
				SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
				if (sqlInternalConnectionTds == null)
				{
					SqlInternalConnectionSmi sqlInternalConnectionSmi = this.InnerConnection as SqlInternalConnectionSmi;
					sqlInternalConnectionSmi.AutomaticEnlistment();
				}
				else
				{
					if (!sqlInternalConnectionTds.ConnectionOptions.Pooling)
					{
						GC.ReRegisterForFinalize(this);
					}
					if (this.StatisticsEnabled)
					{
						ADP.TimerCurrent(out this._statistics._openTimestamp);
						sqlInternalConnectionTds.Parser.Statistics = this._statistics;
					}
					else
					{
						sqlInternalConnectionTds.Parser.Statistics = null;
						this._statistics = null;
					}
					this.CompleteOpen();
				}
			}
			catch (OutOfMemoryException e)
			{
				this.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				this.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this.Abort(e3);
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
			return true;
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x000BBE5C File Offset: 0x000BB25C
		internal bool HasLocalTransaction
		{
			get
			{
				return this.GetOpenConnection().HasLocalTransaction;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x000BBE74 File Offset: 0x000BB274
		internal bool HasLocalTransactionFromAPI
		{
			get
			{
				Task currentReconnectionTask = this._currentReconnectionTask;
				return (currentReconnectionTask == null || currentReconnectionTask.IsCompleted) && this.GetOpenConnection().HasLocalTransactionFromAPI;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x000BBEA0 File Offset: 0x000BB2A0
		internal bool IsShiloh
		{
			get
			{
				return this._currentReconnectionTask != null || this.GetOpenConnection().IsShiloh;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001A82 RID: 6786 RVA: 0x000BBEC4 File Offset: 0x000BB2C4
		internal bool IsYukonOrNewer
		{
			get
			{
				return this._currentReconnectionTask != null || this.GetOpenConnection().IsYukonOrNewer;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x000BBEE8 File Offset: 0x000BB2E8
		internal bool IsKatmaiOrNewer
		{
			get
			{
				return this._currentReconnectionTask != null || this.GetOpenConnection().IsKatmaiOrNewer;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x000BBF0C File Offset: 0x000BB30C
		internal TdsParser Parser
		{
			get
			{
				SqlInternalConnectionTds sqlInternalConnectionTds = this.GetOpenConnection() as SqlInternalConnectionTds;
				if (sqlInternalConnectionTds == null)
				{
					throw SQL.NotAvailableOnContextConnection();
				}
				return sqlInternalConnectionTds.Parser;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x000BBF34 File Offset: 0x000BB334
		internal bool Asynchronous
		{
			get
			{
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				return sqlConnectionString != null && sqlConnectionString.Asynchronous;
			}
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x000BBF58 File Offset: 0x000BB358
		internal void ValidateConnectionForExecute(string method, SqlCommand command)
		{
			Task asyncWaitingForReconnection = this._asyncWaitingForReconnection;
			if (asyncWaitingForReconnection != null)
			{
				if (!asyncWaitingForReconnection.IsCompleted)
				{
					throw SQL.MARSUnspportedOnConnection();
				}
				Interlocked.CompareExchange<Task>(ref this._asyncWaitingForReconnection, null, asyncWaitingForReconnection);
			}
			if (this._currentReconnectionTask != null)
			{
				Task currentReconnectionTask = this._currentReconnectionTask;
				if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
				{
					return;
				}
			}
			SqlInternalConnection openConnection = this.GetOpenConnection(method);
			openConnection.ValidateConnectionForExecute(command);
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x000BBFB8 File Offset: 0x000BB3B8
		internal static string FixupDatabaseTransactionName(string name)
		{
			if (!ADP.IsEmpty(name))
			{
				return SqlServerEscapeHelper.EscapeIdentifier(name);
			}
			return name;
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x000BBFD8 File Offset: 0x000BB3D8
		internal void OnError(SqlException exception, bool breakConnection, Action<Action> wrapCloseInAction)
		{
			if (breakConnection && ConnectionState.Open == this.State)
			{
				if (wrapCloseInAction != null)
				{
					int capturedCloseCount = this._closeCount;
					Action obj = delegate()
					{
						if (capturedCloseCount == this._closeCount)
						{
							Bid.Trace("<sc.SqlConnection.OnError|INFO> %d#, Connection broken.\n", this.ObjectID);
							this.Close();
						}
					};
					wrapCloseInAction(obj);
				}
				else
				{
					Bid.Trace("<sc.SqlConnection.OnError|INFO> %d#, Connection broken.\n", this.ObjectID);
					this.Close();
				}
			}
			if (exception.Class >= 11)
			{
				throw exception;
			}
			this.OnInfoMessage(new SqlInfoMessageEventArgs(exception));
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x000BC054 File Offset: 0x000BB454
		private void CompleteOpen()
		{
			if (!this.GetOpenConnection().IsYukonOrNewer && Debugger.IsAttached)
			{
				bool flag = false;
				try
				{
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
					flag = true;
				}
				catch (SecurityException e)
				{
					ADP.TraceExceptionWithoutRethrow(e);
				}
				if (flag)
				{
					this.CheckSQLDebugOnConnect();
				}
			}
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x000BC0B4 File Offset: 0x000BB4B4
		internal SqlInternalConnection GetOpenConnection()
		{
			SqlInternalConnection sqlInternalConnection = this.InnerConnection as SqlInternalConnection;
			if (sqlInternalConnection == null)
			{
				throw ADP.ClosedConnectionError();
			}
			return sqlInternalConnection;
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x000BC0D8 File Offset: 0x000BB4D8
		internal SqlInternalConnection GetOpenConnection(string method)
		{
			DbConnectionInternal innerConnection = this.InnerConnection;
			SqlInternalConnection sqlInternalConnection = innerConnection as SqlInternalConnection;
			if (sqlInternalConnection == null)
			{
				throw ADP.OpenConnectionRequired(method, innerConnection.State);
			}
			return sqlInternalConnection;
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x000BC104 File Offset: 0x000BB504
		internal SqlInternalConnectionTds GetOpenTdsConnection()
		{
			SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
			if (sqlInternalConnectionTds == null)
			{
				throw ADP.ClosedConnectionError();
			}
			return sqlInternalConnectionTds;
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x000BC128 File Offset: 0x000BB528
		internal SqlInternalConnectionTds GetOpenTdsConnection(string method)
		{
			SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
			if (sqlInternalConnectionTds == null)
			{
				throw ADP.OpenConnectionRequired(method, this.InnerConnection.State);
			}
			return sqlInternalConnectionTds;
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x000BC158 File Offset: 0x000BB558
		internal void OnInfoMessage(SqlInfoMessageEventArgs imevent)
		{
			bool flag;
			this.OnInfoMessage(imevent, out flag);
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x000BC170 File Offset: 0x000BB570
		internal void OnInfoMessage(SqlInfoMessageEventArgs imevent, out bool notified)
		{
			if (Bid.TraceOn)
			{
				Bid.Trace("<sc.SqlConnection.OnInfoMessage|API|INFO> %d#, Message='%ls'\n", this.ObjectID, (imevent != null) ? imevent.Message : "");
			}
			SqlInfoMessageEventHandler sqlInfoMessageEventHandler = (SqlInfoMessageEventHandler)base.Events[SqlConnection.EventInfoMessage];
			if (sqlInfoMessageEventHandler != null)
			{
				notified = true;
				try
				{
					sqlInfoMessageEventHandler(this, imevent);
					return;
				}
				catch (Exception e)
				{
					if (!ADP.IsCatchableOrSecurityExceptionType(e))
					{
						throw;
					}
					ADP.TraceExceptionWithoutRethrow(e);
					return;
				}
			}
			notified = false;
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x000BC1FC File Offset: 0x000BB5FC
		private void CheckSQLDebugOnConnect()
		{
			uint currentProcessId = (uint)SafeNativeMethods.GetCurrentProcessId();
			string text;
			if (ADP.IsPlatformNT5)
			{
				text = "Global\\SqlClientSSDebug";
			}
			else
			{
				text = "SqlClientSSDebug";
			}
			text += currentProcessId.ToString(CultureInfo.InvariantCulture);
			IntPtr intPtr = NativeMethods.OpenFileMappingA(4, false, text);
			if (ADP.PtrZero != intPtr)
			{
				IntPtr intPtr2 = NativeMethods.MapViewOfFile(intPtr, 4, 0, 0, IntPtr.Zero);
				if (ADP.PtrZero != intPtr2)
				{
					SqlDebugContext sqlDebugContext = new SqlDebugContext();
					sqlDebugContext.hMemMap = intPtr;
					sqlDebugContext.pMemMap = intPtr2;
					sqlDebugContext.pid = currentProcessId;
					this.CheckSQLDebug(sqlDebugContext);
					this._sdc = sqlDebugContext;
				}
			}
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x000BC294 File Offset: 0x000BB694
		internal void CheckSQLDebug()
		{
			if (this._sdc != null)
			{
				this.CheckSQLDebug(this._sdc);
			}
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x000BC2B8 File Offset: 0x000BB6B8
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private void CheckSQLDebug(SqlDebugContext sdc)
		{
			uint currentThreadId = (uint)AppDomain.GetCurrentThreadId();
			SqlConnection.RefreshMemoryMappedData(sdc);
			if (!sdc.active && sdc.fOption)
			{
				sdc.active = true;
				sdc.tid = currentThreadId;
				try
				{
					this.IssueSQLDebug(1U, sdc.machineName, sdc.pid, sdc.dbgpid, sdc.sdiDllName, sdc.data);
					sdc.tid = 0U;
				}
				catch
				{
					sdc.active = false;
					throw;
				}
			}
			if (sdc.active)
			{
				if (!sdc.fOption)
				{
					sdc.Dispose();
					this.IssueSQLDebug(0U, null, 0U, 0U, null, null);
					return;
				}
				if (sdc.tid != currentThreadId)
				{
					sdc.tid = currentThreadId;
					try
					{
						this.IssueSQLDebug(2U, null, sdc.pid, sdc.tid, null, null);
					}
					catch
					{
						sdc.tid = 0U;
						throw;
					}
				}
			}
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x000BC3B4 File Offset: 0x000BB7B4
		private void IssueSQLDebug(uint option, string machineName, uint pid, uint id, string sdiDllName, byte[] data)
		{
			if (this.GetOpenConnection().IsYukonOrNewer)
			{
				return;
			}
			SqlCommand sqlCommand = new SqlCommand("sp_sdidebug", this);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlParameter sqlParameter = new SqlParameter(null, SqlDbType.VarChar, TdsEnums.SQLDEBUG_MODE_NAMES[(int)option].Length);
			sqlParameter.Value = TdsEnums.SQLDEBUG_MODE_NAMES[(int)option];
			sqlCommand.Parameters.Add(sqlParameter);
			if (option == 1U)
			{
				sqlParameter = new SqlParameter(null, SqlDbType.VarChar, sdiDllName.Length);
				sqlParameter.Value = sdiDllName;
				sqlCommand.Parameters.Add(sqlParameter);
				sqlParameter = new SqlParameter(null, SqlDbType.VarChar, machineName.Length);
				sqlParameter.Value = machineName;
				sqlCommand.Parameters.Add(sqlParameter);
			}
			if (option != 0U)
			{
				sqlParameter = new SqlParameter(null, SqlDbType.Int);
				sqlParameter.Value = pid;
				sqlCommand.Parameters.Add(sqlParameter);
				sqlParameter = new SqlParameter(null, SqlDbType.Int);
				sqlParameter.Value = id;
				sqlCommand.Parameters.Add(sqlParameter);
			}
			if (option == 1U)
			{
				sqlParameter = new SqlParameter(null, SqlDbType.VarBinary, (data != null) ? data.Length : 0);
				sqlParameter.Value = data;
				sqlCommand.Parameters.Add(sqlParameter);
			}
			sqlCommand.ExecuteNonQuery();
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x000BC4D8 File Offset: 0x000BB8D8
		public static void ChangePassword(string connectionString, string newPassword)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlConnection.ChangePassword|API>");
			Bid.CorrelationTrace("<sc.SqlConnection.ChangePassword|API|Correlation> ActivityID %ls\n");
			try
			{
				if (ADP.IsEmpty(connectionString))
				{
					throw SQL.ChangePasswordArgumentMissing("connectionString");
				}
				if (ADP.IsEmpty(newPassword))
				{
					throw SQL.ChangePasswordArgumentMissing("newPassword");
				}
				if (128 < newPassword.Length)
				{
					throw ADP.InvalidArgumentLength("newPassword", 128);
				}
				SqlConnectionPoolKey key = new SqlConnectionPoolKey(connectionString, null, null);
				SqlConnectionString sqlConnectionString = SqlConnectionFactory.FindSqlConnectionOptions(key);
				if (sqlConnectionString.IntegratedSecurity || sqlConnectionString.Authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated)
				{
					throw SQL.ChangePasswordConflictsWithSSPI();
				}
				if (!ADP.IsEmpty(sqlConnectionString.AttachDBFilename))
				{
					throw SQL.ChangePasswordUseOfUnallowedKey("attachdbfilename");
				}
				if (sqlConnectionString.ContextConnection)
				{
					throw SQL.ChangePasswordUseOfUnallowedKey("context connection");
				}
				PermissionSet permissionSet = sqlConnectionString.CreatePermissionSet();
				permissionSet.Demand();
				SqlConnection.ChangePassword(connectionString, sqlConnectionString, null, newPassword, null);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x000BC5D0 File Offset: 0x000BB9D0
		public static void ChangePassword(string connectionString, SqlCredential credential, SecureString newSecurePassword)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlConnection.ChangePassword|API>");
			Bid.CorrelationTrace("<sc.SqlConnection.ChangePassword|API|Correlation> ActivityID %ls\n");
			try
			{
				if (ADP.IsEmpty(connectionString))
				{
					throw SQL.ChangePasswordArgumentMissing("connectionString");
				}
				if (credential == null)
				{
					throw SQL.ChangePasswordArgumentMissing("credential");
				}
				if (newSecurePassword == null || newSecurePassword.Length == 0)
				{
					throw SQL.ChangePasswordArgumentMissing("newSecurePassword");
				}
				if (!newSecurePassword.IsReadOnly())
				{
					throw ADP.MustBeReadOnly("newSecurePassword");
				}
				if (128 < newSecurePassword.Length)
				{
					throw ADP.InvalidArgumentLength("newSecurePassword", 128);
				}
				SqlConnectionPoolKey key = new SqlConnectionPoolKey(connectionString, credential, null);
				SqlConnectionString sqlConnectionString = SqlConnectionFactory.FindSqlConnectionOptions(key);
				if (!ADP.IsEmpty(sqlConnectionString.UserID) || !ADP.IsEmpty(sqlConnectionString.Password))
				{
					throw ADP.InvalidMixedArgumentOfSecureAndClearCredential();
				}
				if (sqlConnectionString.IntegratedSecurity || sqlConnectionString.Authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated)
				{
					throw SQL.ChangePasswordConflictsWithSSPI();
				}
				if (!ADP.IsEmpty(sqlConnectionString.AttachDBFilename))
				{
					throw SQL.ChangePasswordUseOfUnallowedKey("attachdbfilename");
				}
				if (sqlConnectionString.ContextConnection)
				{
					throw SQL.ChangePasswordUseOfUnallowedKey("context connection");
				}
				PermissionSet permissionSet = sqlConnectionString.CreatePermissionSet();
				permissionSet.Demand();
				SqlConnection.ChangePassword(connectionString, sqlConnectionString, credential, null, newSecurePassword);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x000BC70C File Offset: 0x000BBB0C
		private static void ChangePassword(string connectionString, SqlConnectionString connectionOptions, SqlCredential credential, string newPassword, SecureString newSecurePassword)
		{
			using (SqlInternalConnectionTds sqlInternalConnectionTds = new SqlInternalConnectionTds(null, connectionOptions, credential, null, newPassword, newSecurePassword, false, null, null, null, null, false, null))
			{
				if (!sqlInternalConnectionTds.IsYukonOrNewer)
				{
					throw SQL.ChangePasswordRequiresYukon();
				}
			}
			SqlConnectionPoolKey key = new SqlConnectionPoolKey(connectionString, credential, null);
			SqlConnectionFactory.SingletonInstance.ClearPool(key);
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x000BC778 File Offset: 0x000BBB78
		internal void RegisterForConnectionCloseNotification<T>(ref Task<T> outterTask, object value, int tag)
		{
			outterTask = outterTask.ContinueWith<Task<T>>(delegate(Task<T> task)
			{
				this.RemoveWeakReference(value);
				return task;
			}, TaskScheduler.Default).Unwrap<T>();
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x000BC7B8 File Offset: 0x000BBBB8
		private static void RefreshMemoryMappedData(SqlDebugContext sdc)
		{
			MEMMAP memmap = (MEMMAP)Marshal.PtrToStructure(sdc.pMemMap, typeof(MEMMAP));
			sdc.dbgpid = memmap.dbgpid;
			sdc.fOption = (memmap.fOption == 1U);
			Encoding encoding = Encoding.GetEncoding(1252);
			sdc.machineName = encoding.GetString(memmap.rgbMachineName, 0, memmap.rgbMachineName.Length);
			sdc.sdiDllName = encoding.GetString(memmap.rgbDllName, 0, memmap.rgbDllName.Length);
			sdc.data = memmap.rgbData;
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x000BC84C File Offset: 0x000BBC4C
		public void ResetStatistics()
		{
			if (this.IsContextConnection)
			{
				throw SQL.NotAvailableOnContextConnection();
			}
			if (this.Statistics != null)
			{
				this.Statistics.Reset();
				if (ConnectionState.Open == this.State)
				{
					ADP.TimerCurrent(out this._statistics._openTimestamp);
				}
			}
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x000BC894 File Offset: 0x000BBC94
		public IDictionary RetrieveStatistics()
		{
			if (this.IsContextConnection)
			{
				throw SQL.NotAvailableOnContextConnection();
			}
			if (this.Statistics != null)
			{
				this.UpdateStatistics();
				return this.Statistics.GetHashtable();
			}
			return new SqlStatistics().GetHashtable();
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x000BC8D4 File Offset: 0x000BBCD4
		private void UpdateStatistics()
		{
			if (ConnectionState.Open == this.State)
			{
				ADP.TimerCurrent(out this._statistics._closeTimestamp);
			}
			this.Statistics.UpdateStatistics();
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x000BC908 File Offset: 0x000BBD08
		private Assembly ResolveTypeAssembly(AssemblyName asmRef, bool throwOnError)
		{
			if (string.Compare(asmRef.Name, "Microsoft.SqlServer.Types", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (Bid.TraceOn && asmRef.Version != this.TypeSystemAssemblyVersion)
				{
					Bid.Trace("<sc.SqlConnection.ResolveTypeAssembly> SQL CLR type version change: Server sent %ls, client will instantiate %ls", asmRef.Version.ToString(), this.TypeSystemAssemblyVersion.ToString());
				}
				asmRef.Version = this.TypeSystemAssemblyVersion;
			}
			Assembly result;
			try
			{
				result = Assembly.Load(asmRef);
			}
			catch (Exception e)
			{
				if (throwOnError || !ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x000BC9A8 File Offset: 0x000BBDA8
		internal void CheckGetExtendedUDTInfo(SqlMetaDataPriv metaData, bool fThrow)
		{
			if (metaData.udtType == null)
			{
				metaData.udtType = Type.GetType(metaData.udtAssemblyQualifiedName, (AssemblyName asmRef) => this.ResolveTypeAssembly(asmRef, fThrow), null, fThrow);
				if (fThrow && metaData.udtType == null)
				{
					throw SQL.UDTUnexpectedResult(metaData.udtAssemblyQualifiedName);
				}
			}
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x000BCA20 File Offset: 0x000BBE20
		internal object GetUdtValue(object value, SqlMetaDataPriv metaData, bool returnDBNull)
		{
			if (returnDBNull && ADP.IsNull(value))
			{
				return DBNull.Value;
			}
			if (ADP.IsNull(value))
			{
				Type udtType = metaData.udtType;
				return udtType.InvokeMember("Null", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, null, null, new object[0], CultureInfo.InvariantCulture);
			}
			MemoryStream s = new MemoryStream((byte[])value);
			return SerializationHelperSql9.Deserialize(s, metaData.udtType);
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x000BCA8C File Offset: 0x000BBE8C
		internal byte[] GetBytes(object o)
		{
			Format format = Format.Native;
			int num = 0;
			return this.GetBytes(o, out format, out num);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x000BCAA8 File Offset: 0x000BBEA8
		internal byte[] GetBytes(object o, out Format format, out int maxSize)
		{
			SqlUdtInfo infoFromType = AssemblyCache.GetInfoFromType(o.GetType());
			maxSize = infoFromType.MaxByteSize;
			format = infoFromType.SerializationFormat;
			if (maxSize < -1 || maxSize >= 65535)
			{
				Type type = o.GetType();
				throw new InvalidOperationException(((type != null) ? type.ToString() : null) + ": invalid Size");
			}
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream((maxSize < 0) ? 0 : maxSize))
			{
				SerializationHelperSql9.Serialize(memoryStream, o);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x000BCB48 File Offset: 0x000BBF48
		public SqlConnection()
		{
			this._reconnectLock = new object();
			this._originalConnectionId = Guid.Empty;
			this.ObjectID = Interlocked.Increment(ref SqlConnection._objectTypeCount);
			base..ctor();
			GC.SuppressFinalize(this);
			this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x000BCB94 File Offset: 0x000BBF94
		private void CopyFrom(SqlConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			this._userConnectionOptions = connection.UserConnectionOptions;
			this._poolGroup = connection.PoolGroup;
			if (DbConnectionClosedNeverOpened.SingletonInstance == connection._innerConnection)
			{
				this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
				return;
			}
			this._innerConnection = DbConnectionClosedPreviouslyOpened.SingletonInstance;
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x000BCBE8 File Offset: 0x000BBFE8
		internal int CloseCount
		{
			get
			{
				return this._closeCount;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x000BCBFC File Offset: 0x000BBFFC
		internal DbConnectionFactory ConnectionFactory
		{
			get
			{
				return SqlConnection._connectionFactory;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x000BCC10 File Offset: 0x000BC010
		internal DbConnectionOptions ConnectionOptions
		{
			get
			{
				DbConnectionPoolGroup poolGroup = this.PoolGroup;
				if (poolGroup == null)
				{
					return null;
				}
				return poolGroup.ConnectionOptions;
			}
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x000BCC30 File Offset: 0x000BC030
		private string ConnectionString_Get()
		{
			Bid.Trace("<prov.DbConnectionHelper.ConnectionString_Get|API> %d#\n", this.ObjectID);
			bool shouldHidePassword = this.InnerConnection.ShouldHidePassword;
			DbConnectionOptions userConnectionOptions = this.UserConnectionOptions;
			if (userConnectionOptions == null)
			{
				return "";
			}
			return userConnectionOptions.UsersConnectionString(shouldHidePassword);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x000BCC70 File Offset: 0x000BC070
		private void ConnectionString_Set(string value)
		{
			DbConnectionPoolKey key = new DbConnectionPoolKey(value);
			this.ConnectionString_Set(key);
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x000BCC8C File Offset: 0x000BC08C
		private void ConnectionString_Set(DbConnectionPoolKey key)
		{
			DbConnectionOptions dbConnectionOptions = null;
			DbConnectionPoolGroup connectionPoolGroup = this.ConnectionFactory.GetConnectionPoolGroup(key, null, ref dbConnectionOptions);
			DbConnectionInternal innerConnection = this.InnerConnection;
			bool flag = innerConnection.AllowSetConnectionString;
			if (flag)
			{
				flag = this.SetInnerConnectionFrom(DbConnectionClosedBusy.SingletonInstance, innerConnection);
				if (flag)
				{
					this._userConnectionOptions = dbConnectionOptions;
					this._poolGroup = connectionPoolGroup;
					this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
				}
			}
			if (!flag)
			{
				throw ADP.OpenConnectionPropertySet("ConnectionString", innerConnection.State);
			}
			if (Bid.TraceOn)
			{
				string a = (dbConnectionOptions != null) ? dbConnectionOptions.UsersConnectionStringForTrace() : "";
				Bid.Trace("<prov.DbConnectionHelper.ConnectionString_Set|API> %d#, '%ls'\n", this.ObjectID, a);
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x000BCD24 File Offset: 0x000BC124
		internal DbConnectionInternal InnerConnection
		{
			get
			{
				return this._innerConnection;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x000BCD38 File Offset: 0x000BC138
		// (set) Token: 0x06001AAB RID: 6827 RVA: 0x000BCD4C File Offset: 0x000BC14C
		internal DbConnectionPoolGroup PoolGroup
		{
			get
			{
				return this._poolGroup;
			}
			set
			{
				this._poolGroup = value;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001AAC RID: 6828 RVA: 0x000BCD60 File Offset: 0x000BC160
		internal DbConnectionOptions UserConnectionOptions
		{
			get
			{
				return this._userConnectionOptions;
			}
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x000BCD74 File Offset: 0x000BC174
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void Abort(Exception e)
		{
			DbConnectionInternal innerConnection = this._innerConnection;
			if (ConnectionState.Open == innerConnection.State)
			{
				Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, DbConnectionClosedPreviouslyOpened.SingletonInstance, innerConnection);
				innerConnection.DoomThisConnection();
			}
			if (e is OutOfMemoryException)
			{
				Bid.Trace("<prov.DbConnectionHelper.Abort|RES|INFO|CPOOL> %d#, Aborting operation due to asynchronous exception: %ls\n", this.ObjectID, "OutOfMemory");
				return;
			}
			Bid.Trace("<prov.DbConnectionHelper.Abort|RES|INFO|CPOOL> %d#, Aborting operation due to asynchronous exception: %ls\n", this.ObjectID, e.ToString());
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x000BCDE0 File Offset: 0x000BC1E0
		internal void AddWeakReference(object value, int tag)
		{
			this.InnerConnection.AddWeakReference(value, tag);
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x000BCDFC File Offset: 0x000BC1FC
		protected override DbCommand CreateDbCommand()
		{
			DbCommand dbCommand = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<prov.DbConnectionHelper.CreateDbCommand|API> %d#\n", this.ObjectID);
			try
			{
				DbProviderFactory providerFactory = this.ConnectionFactory.ProviderFactory;
				dbCommand = providerFactory.CreateCommand();
				dbCommand.Connection = this;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return dbCommand;
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x000BCE60 File Offset: 0x000BC260
		private static CodeAccessPermission CreateExecutePermission()
		{
			DBDataPermission dbdataPermission = (DBDataPermission)SqlConnectionFactory.SingletonInstance.ProviderFactory.CreatePermission(PermissionState.None);
			dbdataPermission.Add(string.Empty, string.Empty, KeyRestrictionBehavior.AllowOnly);
			return dbdataPermission;
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x000BCE98 File Offset: 0x000BC298
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._userConnectionOptions = null;
				this._poolGroup = null;
				this.Close();
			}
			this.DisposeMe(disposing);
			base.Dispose(disposing);
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x000BCECC File Offset: 0x000BC2CC
		private void RepairInnerConnection()
		{
			this.WaitForPendingReconnection();
			if (this._connectRetryCount == 0)
			{
				return;
			}
			SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
			if (sqlInternalConnectionTds != null)
			{
				sqlInternalConnectionTds.ValidateConnectionForExecute(null);
				sqlInternalConnectionTds.GetSessionAndReconnectIfNeeded(this, 0);
			}
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x000BCF08 File Offset: 0x000BC308
		private void EnlistDistributedTransactionHelper(ITransaction transaction)
		{
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(SqlConnection.ExecutePermission);
			permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
			permissionSet.Demand();
			Bid.Trace("<prov.DbConnectionHelper.EnlistDistributedTransactionHelper|RES|TRAN> %d#, Connection enlisting in a transaction.\n", this.ObjectID);
			Transaction transaction2 = null;
			if (transaction != null)
			{
				transaction2 = TransactionInterop.GetTransactionFromDtcTransaction((IDtcTransaction)transaction);
			}
			this.RepairInnerConnection();
			this.InnerConnection.EnlistTransaction(transaction2);
			GC.KeepAlive(this);
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x000BCF74 File Offset: 0x000BC374
		public override void EnlistTransaction(Transaction transaction)
		{
			SqlConnection.ExecutePermission.Demand();
			Bid.Trace("<prov.DbConnectionHelper.EnlistTransaction|RES|TRAN> %d#, Connection enlisting in a transaction.\n", this.ObjectID);
			DbConnectionInternal innerConnection = this.InnerConnection;
			Transaction enlistedTransaction = innerConnection.EnlistedTransaction;
			if (enlistedTransaction != null)
			{
				if (enlistedTransaction.Equals(transaction))
				{
					return;
				}
				if (enlistedTransaction.TransactionInformation.Status == System.Transactions.TransactionStatus.Active)
				{
					throw ADP.TransactionPresent();
				}
			}
			this.RepairInnerConnection();
			this.InnerConnection.EnlistTransaction(transaction);
			GC.KeepAlive(this);
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x000BCFE8 File Offset: 0x000BC3E8
		private DbMetaDataFactory GetMetaDataFactory(DbConnectionInternal internalConnection)
		{
			return this.ConnectionFactory.GetMetaDataFactory(this._poolGroup, internalConnection);
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x000BD008 File Offset: 0x000BC408
		internal DbMetaDataFactory GetMetaDataFactoryInternal(DbConnectionInternal internalConnection)
		{
			return this.GetMetaDataFactory(internalConnection);
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x000BD01C File Offset: 0x000BC41C
		public override DataTable GetSchema()
		{
			return this.GetSchema(DbMetaDataCollectionNames.MetaDataCollections, null);
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x000BD038 File Offset: 0x000BC438
		public override DataTable GetSchema(string collectionName)
		{
			return this.GetSchema(collectionName, null);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x000BD050 File Offset: 0x000BC450
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			SqlConnection.ExecutePermission.Demand();
			return this.InnerConnection.GetSchema(this.ConnectionFactory, this.PoolGroup, this, collectionName, restrictionValues);
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x000BD084 File Offset: 0x000BC484
		internal void NotifyWeakReference(int message)
		{
			this.InnerConnection.NotifyWeakReference(message);
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x000BD0A0 File Offset: 0x000BC4A0
		internal void PermissionDemand()
		{
			DbConnectionPoolGroup poolGroup = this.PoolGroup;
			DbConnectionOptions dbConnectionOptions = (poolGroup != null) ? poolGroup.ConnectionOptions : null;
			if (dbConnectionOptions == null || dbConnectionOptions.IsEmpty)
			{
				throw ADP.NoConnectionString();
			}
			DbConnectionOptions userConnectionOptions = this.UserConnectionOptions;
			userConnectionOptions.DemandPermission();
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x000BD0E0 File Offset: 0x000BC4E0
		internal void RemoveWeakReference(object value)
		{
			this.InnerConnection.RemoveWeakReference(value);
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x000BD0FC File Offset: 0x000BC4FC
		internal void SetInnerConnectionEvent(DbConnectionInternal to)
		{
			ConnectionState connectionState = this._innerConnection.State & ConnectionState.Open;
			ConnectionState connectionState2 = to.State & ConnectionState.Open;
			if (connectionState != connectionState2 && connectionState2 == ConnectionState.Closed)
			{
				this._closeCount++;
			}
			this._innerConnection = to;
			if (connectionState == ConnectionState.Closed && ConnectionState.Open == connectionState2)
			{
				this.OnStateChange(DbConnectionInternal.StateChangeOpen);
				return;
			}
			if (ConnectionState.Open == connectionState && connectionState2 == ConnectionState.Closed)
			{
				this.OnStateChange(DbConnectionInternal.StateChangeClosed);
				return;
			}
			if (connectionState != connectionState2)
			{
				this.OnStateChange(new StateChangeEventArgs(connectionState, connectionState2));
			}
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x000BD174 File Offset: 0x000BC574
		internal bool SetInnerConnectionFrom(DbConnectionInternal to, DbConnectionInternal from)
		{
			return from == Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, to, from);
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x000BD194 File Offset: 0x000BC594
		internal void SetInnerConnectionTo(DbConnectionInternal to)
		{
			this._innerConnection = to;
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x000BD1A8 File Offset: 0x000BC5A8
		[Conditional("DEBUG")]
		internal static void VerifyExecutePermission()
		{
			try
			{
				SqlConnection.ExecutePermission.Demand();
			}
			catch (SecurityException)
			{
				throw;
			}
		}

		// Token: 0x04000F43 RID: 3907
		private static readonly object EventInfoMessage = new object();

		// Token: 0x04000F44 RID: 3908
		internal static readonly SqlColumnEncryptionEnclaveProviderConfigurationManager sqlColumnEncryptionEnclaveProviderConfigurationManager;

		// Token: 0x04000F45 RID: 3909
		private static readonly Dictionary<string, SqlColumnEncryptionKeyStoreProvider> _SystemColumnEncryptionKeyStoreProviders = new Dictionary<string, SqlColumnEncryptionKeyStoreProvider>(1, StringComparer.OrdinalIgnoreCase)
		{
			{
				"MSSQL_CERTIFICATE_STORE",
				new SqlColumnEncryptionCertificateStoreProvider()
			},
			{
				"MSSQL_CNG_STORE",
				new SqlColumnEncryptionCngProvider()
			},
			{
				"MSSQL_CSP_PROVIDER",
				new SqlColumnEncryptionCspProvider()
			}
		};

		// Token: 0x04000F46 RID: 3910
		private static ReadOnlyDictionary<string, SqlColumnEncryptionKeyStoreProvider> _CustomColumnEncryptionKeyStoreProviders;

		// Token: 0x04000F47 RID: 3911
		private static readonly object _CustomColumnEncryptionKeyProvidersLock = new object();

		// Token: 0x04000F48 RID: 3912
		private static readonly ConcurrentDictionary<string, IList<string>> _ColumnEncryptionTrustedMasterKeyPaths = new ConcurrentDictionary<string, IList<string>>(4 * Environment.ProcessorCount, 1, StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000F49 RID: 3913
		private static bool _ColumnEncryptionQueryMetadataCacheEnabled = true;

		// Token: 0x04000F4A RID: 3914
		private static TimeSpan _ColumnEncryptionKeyCacheTtl = TimeSpan.FromHours(2.0);

		// Token: 0x04000F4B RID: 3915
		private SqlDebugContext _sdc;

		// Token: 0x04000F4C RID: 3916
		private bool _AsyncCommandInProgress;

		// Token: 0x04000F4D RID: 3917
		internal SqlStatistics _statistics;

		// Token: 0x04000F4E RID: 3918
		private bool _collectstats;

		// Token: 0x04000F4F RID: 3919
		private bool _fireInfoMessageEventOnUserErrors;

		// Token: 0x04000F50 RID: 3920
		private Tuple<TaskCompletionSource<DbConnectionInternal>, Task> _currentCompletion;

		// Token: 0x04000F51 RID: 3921
		private SqlCredential _credential;

		// Token: 0x04000F52 RID: 3922
		private string _connectionString;

		// Token: 0x04000F53 RID: 3923
		private int _connectRetryCount;

		// Token: 0x04000F54 RID: 3924
		private string _accessToken;

		// Token: 0x04000F55 RID: 3925
		private object _reconnectLock;

		// Token: 0x04000F56 RID: 3926
		internal Task _currentReconnectionTask;

		// Token: 0x04000F57 RID: 3927
		private Task _asyncWaitingForReconnection;

		// Token: 0x04000F58 RID: 3928
		private Guid _originalConnectionId;

		// Token: 0x04000F59 RID: 3929
		private CancellationTokenSource _reconnectionCancellationSource;

		// Token: 0x04000F5A RID: 3930
		internal SessionData _recoverySessionData;

		// Token: 0x04000F5B RID: 3931
		internal WindowsIdentity _lastIdentity;

		// Token: 0x04000F5C RID: 3932
		internal WindowsIdentity _impersonateIdentity;

		// Token: 0x04000F5D RID: 3933
		private int _reconnectCount;

		// Token: 0x04000F5E RID: 3934
		internal bool _applyTransientFaultHandling;

		// Token: 0x04000F5F RID: 3935
		private static readonly DbConnectionFactory _connectionFactory = SqlConnectionFactory.SingletonInstance;

		// Token: 0x04000F60 RID: 3936
		internal static readonly CodeAccessPermission ExecutePermission = SqlConnection.CreateExecutePermission();

		// Token: 0x04000F61 RID: 3937
		private DbConnectionOptions _userConnectionOptions;

		// Token: 0x04000F62 RID: 3938
		private DbConnectionPoolGroup _poolGroup;

		// Token: 0x04000F63 RID: 3939
		private DbConnectionInternal _innerConnection;

		// Token: 0x04000F64 RID: 3940
		private int _closeCount;

		// Token: 0x04000F65 RID: 3941
		private static int _objectTypeCount;

		// Token: 0x04000F66 RID: 3942
		internal readonly int ObjectID;

		// Token: 0x020003A3 RID: 931
		private class OpenAsyncRetry
		{
			// Token: 0x060034D3 RID: 13523 RVA: 0x00142810 File Offset: 0x00141C10
			public OpenAsyncRetry(SqlConnection parent, TaskCompletionSource<DbConnectionInternal> retry, TaskCompletionSource<object> result, CancellationTokenRegistration registration)
			{
				this._parent = parent;
				this._retry = retry;
				this._result = result;
				this._registration = registration;
			}

			// Token: 0x060034D4 RID: 13524 RVA: 0x00142840 File Offset: 0x00141C40
			internal void Retry(Task<DbConnectionInternal> retryTask)
			{
				Bid.Trace("<sc.SqlConnection.OpenAsyncRetry|Info> %d#\n", this._parent.ObjectID);
				this._registration.Dispose();
				try
				{
					SqlStatistics statistics = null;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						statistics = SqlStatistics.StartTimer(this._parent.Statistics);
						if (retryTask.IsFaulted)
						{
							Exception innerException = retryTask.Exception.InnerException;
							this._parent.CloseInnerConnection();
							this._parent._currentCompletion = null;
							this._result.SetException(retryTask.Exception.InnerException);
						}
						else if (retryTask.IsCanceled)
						{
							this._parent.CloseInnerConnection();
							this._parent._currentCompletion = null;
							this._result.SetCanceled();
						}
						else
						{
							DbConnectionInternal innerConnection = this._parent.InnerConnection;
							bool flag2;
							lock (innerConnection)
							{
								flag2 = this._parent.TryOpen(this._retry);
							}
							if (flag2)
							{
								this._parent._currentCompletion = null;
								this._result.SetResult(null);
							}
							else
							{
								this._parent.CloseInnerConnection();
								this._parent._currentCompletion = null;
								this._result.SetException(ADP.ExceptionWithStackTrace(ADP.InternalError(ADP.InternalErrorCode.CompletedConnectReturnedPending)));
							}
						}
					}
					finally
					{
						SqlStatistics.StopTimer(statistics);
					}
				}
				catch (Exception exception)
				{
					this._parent.CloseInnerConnection();
					this._parent._currentCompletion = null;
					this._result.SetException(exception);
				}
			}

			// Token: 0x04002011 RID: 8209
			private SqlConnection _parent;

			// Token: 0x04002012 RID: 8210
			private TaskCompletionSource<DbConnectionInternal> _retry;

			// Token: 0x04002013 RID: 8211
			private TaskCompletionSource<object> _result;

			// Token: 0x04002014 RID: 8212
			private CancellationTokenRegistration _registration;
		}
	}
}
