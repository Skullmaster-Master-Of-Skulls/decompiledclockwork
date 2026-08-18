using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.EnterpriseServices;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Transactions;
using System.Xml;
using Oracle.SqlAndPlsqlParser;
using Oracle.SqlAndPlsqlParser.RuleProcessors;
using OracleInternal.Common;
using OracleInternal.ConnectionPool;
using OracleInternal.MTS;
using OracleInternal.Network;
using OracleInternal.ServiceObjects;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000057 RID: 87
	[DefaultEvent("InfoMessage")]
	[ToolboxBitmap(typeof(resfinder), "Oracle.ManagedDataAccess.src.Client.Icons.OracleConnectionToolBox_hc.bmp")]
	[SecurityPermission(SecurityAction.Assert, ControlThread = true)]
	public sealed class OracleConnection : DbConnection, ICloneable, IOracleMetadata
	{
		// Token: 0x060003C0 RID: 960 RVA: 0x0001DE0C File Offset: 0x0001C00C
		public OracleConnection()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			this.m_connectionState = ConnectionState.Closed;
			this.m_appEdition = ConfigBaseClass.m_appEdition;
			this.m_drcpConnectionClass = ConfigBaseClass.m_connectionClass;
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
			}
			this.m_id = this.GetHashCode().ToString();
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001DEC8 File Offset: 0x0001C0C8
		public OracleConnection(string connectionString)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			this.m_appEdition = ConfigBaseClass.m_appEdition;
			this.m_drcpConnectionClass = ConfigBaseClass.m_connectionClass;
			this.m_id = this.GetHashCode().ToString();
			try
			{
				this.m_connectionState = ConnectionState.Closed;
				this.ConnectionString = connectionString;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001DFBC File Offset: 0x0001C1BC
		protected override void Finalize()
		{
			try
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
				}
				try
				{
					this.Dispose(false);
				}
				catch (Exception ex)
				{
					if (ProviderConfig.m_bTraceLevelPublic)
					{
						Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Error, new string[]
						{
							ex.Message
						});
					}
				}
				finally
				{
					if (ProviderConfig.m_bTraceLevelPublic)
					{
						Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001E05C File Offset: 0x0001C25C
		internal static string Dump()
		{
			return OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.Dump();
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001E064 File Offset: 0x0001C264
		internal static string Dump(string txnid)
		{
			return OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.Dump(txnid);
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0001E06C File Offset: 0x0001C26C
		internal string SessionId
		{
			get
			{
				return this.m_oracleConnectionImpl.m_endUserSessionId + ":" + this.m_oracleConnectionImpl.m_endUserSerialNum;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0001E098 File Offset: 0x0001C298
		internal string InstanceBranch
		{
			get
			{
				return string.Format("((sessid={0}:{1})(inst={2})(br={3})(rmid={4})(branchid={5})", new object[]
				{
					this.m_oracleConnectionImpl.m_endUserSessionId,
					this.m_oracleConnectionImpl.m_endUserSerialNum,
					this.m_oracleConnectionImpl.m_instanceName,
					this.m_oracleConnectionImpl.m_mtsTxnCtx.m_mtsTxnBranch.BranchNumber,
					this.m_oracleConnectionImpl.m_txnCtx.m_mtsTxnRM.GetHashCode(),
					this.m_oracleConnectionImpl.m_mtsTxnCtx.m_mtsTxnBranch.GetHashCode()
				});
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0001E144 File Offset: 0x0001C344
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x0001E1AC File Offset: 0x0001C3AC
		[DefaultValue("")]
		[System.ComponentModel.Description("")]
		[Editor("Oracle.VsDevTools.OracleVSGConnStringEditor, Oracle.VsDevTools, Version=4.122.1.0, Culture=neutral, PublicKeyToken=89b483f429c47342, processorArchitecture=X86", "System.Drawing.Design.UITypeEditor")]
		[Category("Data")]
		public override string ConnectionString
		{
			get
			{
				if (!this.m_pwdValidated || (this.m_cs != null && this.m_cs.m_persistSecurityInfo))
				{
					if (this.m_originalConnectionString == null)
					{
						return string.Empty;
					}
					return this.m_originalConnectionString;
				}
				else
				{
					if (this.m_cs == null || this.m_cs.m_passwordlessConString == null)
					{
						return string.Empty;
					}
					return this.m_cs.m_passwordlessConString;
				}
			}
			set
			{
				if (this.m_connectionState == ConnectionState.Open)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_NOT_UPDATABLE, new string[0]));
				}
				string originalConnectionString = this.m_originalConnectionString;
				ConnectionString cs = this.m_cs;
				if (value != null)
				{
					this.m_originalConnectionString = value;
				}
				else
				{
					this.m_originalConnectionString = string.Empty;
				}
				try
				{
					this.m_cs = OracleInternal.ConnectionPool.ConnectionString.GetCS(this.m_originalConnectionString);
					this.InitializeOrclPermission(this.m_originalConnectionString);
				}
				catch
				{
					this.m_originalConnectionString = originalConnectionString;
					this.m_cs = cs;
					throw;
				}
				this.m_securePassword = null;
				this.m_secureProxyPassword = null;
				this.m_connectionTimeout = this.m_cs.m_connectionTimeout;
				this.m_dataSource = this.m_cs.m_dataSource;
				this.m_statementCacheSizeSnapshot = this.m_cs.m_stmtCacheSize;
				this.m_pwdValidated = false;
				this.pmCS = null;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0001E28C File Offset: 0x0001C48C
		[DefaultValue(ConnectionState.Closed)]
		[System.ComponentModel.Description("")]
		[Browsable(false)]
		public override ConnectionState State
		{
			get
			{
				if (this.m_connectionState == ConnectionState.Open && (!this.m_oracleConnectionImpl.IsConnectionAlive() || this.m_oracleConnectionImpl.m_deletionRequestor == DeletionRequestor.HA))
				{
					this.Close();
				}
				return this.m_connectionState;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (set) Token: 0x060003CA RID: 970 RVA: 0x0001E2C0 File Offset: 0x0001C4C0
		[DefaultValue("")]
		[Category("Data")]
		[System.ComponentModel.Description("")]
		public string ModuleName
		{
			set
			{
				if (this.m_connectionState == ConnectionState.Closed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
				}
				if (value != this.m_oracleConnectionImpl.m_endToEndMetrics[1])
				{
					this.m_oracleConnectionImpl.m_endToEndMetrics[1] = value;
					this.m_oracleConnectionImpl.m_endToEndMetricsModified[1] = true;
					this.m_oracleConnectionImpl.m_endToEndMetricsModified[2] = true;
				}
			}
		}

		// Token: 0x170000E7 RID: 231
		// (set) Token: 0x060003CB RID: 971 RVA: 0x0001E32C File Offset: 0x0001C52C
		[DefaultValue("")]
		[Category("Data")]
		[System.ComponentModel.Description("")]
		public string ActionName
		{
			set
			{
				if (this.m_connectionState == ConnectionState.Closed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
				}
				if (value != this.m_oracleConnectionImpl.m_endToEndMetrics[2])
				{
					this.m_oracleConnectionImpl.m_endToEndMetrics[2] = value;
					this.m_oracleConnectionImpl.m_endToEndMetricsModified[2] = true;
				}
			}
		}

		// Token: 0x170000E8 RID: 232
		// (set) Token: 0x060003CC RID: 972 RVA: 0x0001E388 File Offset: 0x0001C588
		[System.ComponentModel.Description("")]
		[Category("Data")]
		[DefaultValue("")]
		public string ClientId
		{
			set
			{
				if (this.m_connectionState == ConnectionState.Closed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
				}
				if (value != this.m_oracleConnectionImpl.m_endToEndMetrics[0])
				{
					this.m_oracleConnectionImpl.m_endToEndMetrics[0] = value;
					this.m_oracleConnectionImpl.m_endToEndMetricsModified[0] = true;
				}
			}
		}

		// Token: 0x170000E9 RID: 233
		// (set) Token: 0x060003CD RID: 973 RVA: 0x0001E3E4 File Offset: 0x0001C5E4
		[Category("Data")]
		[System.ComponentModel.Description("")]
		[DefaultValue("")]
		public string ClientInfo
		{
			set
			{
				if (this.m_connectionState == ConnectionState.Closed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
				}
				if (value != this.m_oracleConnectionImpl.m_endToEndMetrics[3])
				{
					this.m_oracleConnectionImpl.m_endToEndMetrics[3] = value;
					this.m_oracleConnectionImpl.m_endToEndMetricsModified[3] = true;
				}
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0001E440 File Offset: 0x0001C640
		[Browsable(false)]
		[DefaultValue(15)]
		[System.ComponentModel.Description("")]
		public override int ConnectionTimeout
		{
			get
			{
				return this.m_connectionTimeout;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0001E448 File Offset: 0x0001C648
		public override string Database
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0001E450 File Offset: 0x0001C650
		[DefaultValue("")]
		[Category("Data")]
		[System.ComponentModel.Description("")]
		public string DatabaseName
		{
			get
			{
				if (this.m_oracleConnectionImpl != null && this.m_connectionState == ConnectionState.Open)
				{
					return this.m_oracleConnectionImpl.m_databaseName;
				}
				return string.Empty;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0001E474 File Offset: 0x0001C674
		[System.ComponentModel.Description("")]
		[Category("Data")]
		[DefaultValue("")]
		public string DatabaseDomainName
		{
			get
			{
				string text = string.Empty;
				if (this.m_oracleConnectionImpl != null && this.m_connectionState == ConnectionState.Open)
				{
					text = this.m_oracleConnectionImpl.m_databaseDomainName;
				}
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
				return null;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0001E4B0 File Offset: 0x0001C6B0
		[DefaultValue("")]
		[Category("Data")]
		[System.ComponentModel.Description("")]
		public string HostName
		{
			get
			{
				if (this.m_oracleConnectionImpl != null && this.m_connectionState == ConnectionState.Open)
				{
					return this.m_oracleConnectionImpl.m_hostName;
				}
				return string.Empty;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0001E4D4 File Offset: 0x0001C6D4
		[Category("Data")]
		[System.ComponentModel.Description("")]
		[DefaultValue("")]
		public string InstanceName
		{
			get
			{
				if (this.m_oracleConnectionImpl != null && this.m_connectionState == ConnectionState.Open)
				{
					return this.m_oracleConnectionImpl.m_instanceName;
				}
				return string.Empty;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0001E4F8 File Offset: 0x0001C6F8
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x0001E520 File Offset: 0x0001C720
		[System.ComponentModel.Description("")]
		[DefaultValue("")]
		[Category("Data")]
		public string ServiceName
		{
			get
			{
				if (this.m_oracleConnectionImpl != null && this.State == ConnectionState.Open)
				{
					return this.m_oracleConnectionImpl.ServiceName;
				}
				return this.m_serviceName;
			}
			set
			{
				if (this.m_connectionState == ConnectionState.Open)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(-7502, new string[]
					{
						"ServiceName"
					}));
				}
				if (!string.IsNullOrEmpty(value))
				{
					this.m_serviceName = HelperClass.RemoveSingleAndDoubleQuotes(value.ToLowerInvariant()).Trim();
				}
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0001E574 File Offset: 0x0001C774
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x0001E604 File Offset: 0x0001C804
		[System.ComponentModel.Description("")]
		[DefaultValue(null)]
		[Category("Data")]
		public string DatabaseEditionName
		{
			get
			{
				if (this.m_oracleConnectionImpl != null && this.State == ConnectionState.Open)
				{
					string text = this.m_oracleConnectionImpl.EditionName;
					if (string.IsNullOrEmpty(text))
					{
						text = this.m_oracleConnectionImpl.GetDefaultEditionName();
					}
					return text;
				}
				string result = null;
				if (!string.IsNullOrEmpty(this.m_userProvidedConEditionName))
				{
					if (this.m_userProvidedConEditionName[0] == '"')
					{
						int num = this.m_userProvidedConEditionName.IndexOf('"', 1);
						result = this.m_userProvidedConEditionName.Substring(1, num - 1);
					}
					else
					{
						result = this.m_userProvidedConEditionName.ToUpper();
					}
				}
				return result;
			}
			set
			{
				if (this.m_connectionState == ConnectionState.Open)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(-7502, new string[]
					{
						"DatabaseEditionName"
					}));
				}
				if (!string.IsNullOrEmpty(value))
				{
					this.m_userProvidedConEditionName = value;
				}
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0001E64C File Offset: 0x0001C84C
		[System.ComponentModel.Description("")]
		[DefaultValue("")]
		public override string DataSource
		{
			get
			{
				if (!this.m_disposed)
				{
					return this.m_dataSource;
				}
				return string.Empty;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0001E664 File Offset: 0x0001C864
		public static bool IsAvailable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0001E668 File Offset: 0x0001C868
		[DefaultValue("")]
		[Browsable(false)]
		[System.ComponentModel.Description("")]
		public override string ServerVersion
		{
			get
			{
				if (this.m_connectionState == ConnectionState.Open)
				{
					return this.m_serverVersion;
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0001E680 File Offset: 0x0001C880
		[DefaultValue(0)]
		[System.ComponentModel.Description("")]
		[Browsable(false)]
		public int StatementCacheSize
		{
			get
			{
				if (this.m_oracleConnectionImpl == null)
				{
					return this.m_statementCacheSizeSnapshot;
				}
				if (this.m_oracleConnectionImpl.m_statementCache != null)
				{
					this.m_statementCacheSizeSnapshot = this.m_oracleConnectionImpl.m_statementCache.m_maxCacheSize;
				}
				else
				{
					this.m_statementCacheSizeSnapshot = 0;
				}
				return this.m_statementCacheSizeSnapshot;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0001E6D0 File Offset: 0x0001C8D0
		internal OracleLogicalTransaction OracleLogicalTransaction
		{
			get
			{
				if (!this.m_isDb12cR1OrHigher)
				{
					return new OracleLogicalTransaction(this, null);
				}
				byte[] array = null;
				if (this.m_oracleConnectionImpl != null && this.m_oracleConnectionImpl.m_cs != null)
				{
					try
					{
						array = this.m_oracleConnectionImpl.GetLogicalTransactionId;
					}
					catch (Exception ex)
					{
						OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
						throw;
					}
				}
				if (array == null)
				{
					return new OracleLogicalTransaction(this, null);
				}
				if (this.m_logicalTransaction == null)
				{
					return new OracleLogicalTransaction(this, array);
				}
				if (this.m_logicalTransaction.m_ltxId.Length != array.Length)
				{
					return new OracleLogicalTransaction(this, array);
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (this.m_logicalTransaction.m_ltxId[i] != array[i])
					{
						return new OracleLogicalTransaction(this, array);
					}
				}
				return this.m_logicalTransaction;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060003DD RID: 989 RVA: 0x0001E798 File Offset: 0x0001C998
		// (remove) Token: 0x060003DE RID: 990 RVA: 0x0001E7B0 File Offset: 0x0001C9B0
		public static event OracleHAEventHandler HAEvent
		{
			add
			{
				OracleConnection.m_haEventHandler = (OracleHAEventHandler)Delegate.Combine(OracleConnection.m_haEventHandler, value);
			}
			remove
			{
				OracleConnection.m_haEventHandler = (OracleHAEventHandler)Delegate.Remove(OracleConnection.m_haEventHandler, value);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0001E7C8 File Offset: 0x0001C9C8
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0001E7D0 File Offset: 0x0001C9D0
		[DefaultValue(null)]
		[System.ComponentModel.Description("")]
		[Category("Behavior")]
		public string DRCPConnectionClass
		{
			get
			{
				return this.m_drcpConnectionClass;
			}
			set
			{
				if (this.m_connectionState == ConnectionState.Open)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(-7502, new string[]
					{
						"DRCPConnectionClass"
					}));
				}
				if (this.m_drcpConnectionClass != value)
				{
					this.m_drcpConnectionClass = value;
				}
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0001E81C File Offset: 0x0001CA1C
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0001E824 File Offset: 0x0001CA24
		[Category("Behavior")]
		[DefaultValue(OracleConnection.OracleDRCPPurity.Pooled)]
		[System.ComponentModel.Description("")]
		public OracleConnection.OracleDRCPPurity DRCPPurity
		{
			get
			{
				return this.m_drcpPurity;
			}
			set
			{
				if (this.m_connectionState == ConnectionState.Open)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(-7502, new string[]
					{
						"DRCPPurity"
					}));
				}
				if (value != OracleConnection.OracleDRCPPurity.New && value != OracleConnection.OracleDRCPPurity.Pooled)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.m_drcpPurity = value;
				this.m_isPuritySet = true;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060003E3 RID: 995 RVA: 0x0001E878 File Offset: 0x0001CA78
		// (remove) Token: 0x060003E4 RID: 996 RVA: 0x0001E8B8 File Offset: 0x0001CAB8
		public event OracleConnectionOpenEventHandler ConnectionOpen
		{
			add
			{
				if (this.m_connectionState == ConnectionState.Open)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(-7502, new string[]
					{
						"ConnectionOpen"
					}));
				}
				this.m_conOpenEventHandler = value;
			}
			remove
			{
				this.m_conOpenEventHandler = (OracleConnectionOpenEventHandler)Delegate.Remove(this.m_conOpenEventHandler, value);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0001E8D4 File Offset: 0x0001CAD4
		protected override DbProviderFactory DbProviderFactory
		{
			get
			{
				return OracleClientFactory.Instance;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0001E8DC File Offset: 0x0001CADC
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x0001E904 File Offset: 0x0001CB04
		[System.ComponentModel.Description("")]
		[Category("Data")]
		[DefaultValue("")]
		public string PDBName
		{
			get
			{
				if (this.m_oracleConnectionImpl != null && this.State == ConnectionState.Open)
				{
					return this.m_oracleConnectionImpl.PdbName;
				}
				return this.m_pdbName;
			}
			set
			{
				if (this.m_connectionState == ConnectionState.Open)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(-7502, new string[]
					{
						"PDBName"
					}));
				}
				if (!string.IsNullOrEmpty(value))
				{
					this.m_pdbName = HelperClass.RemoveSingleAndDoubleQuotes(value.ToLowerInvariant()).Trim();
				}
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0001E958 File Offset: 0x0001CB58
		[DefaultValue(false)]
		[System.ComponentModel.Description("")]
		[Category("Behavior")]
		public bool SwitchedConnection
		{
			get
			{
				bool result = false;
				if (this.m_oracleConnectionImpl != null && this.State == ConnectionState.Open)
				{
					result = this.m_oracleConnectionImpl.bSessionSwitched;
				}
				return result;
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001E988 File Offset: 0x0001CB88
		public override void Open()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					string.Concat(new object[]
					{
						"(conid=",
						this.m_id,
						") (state=",
						this.m_connectionState,
						") (sessid=",
						this.m_sessionId,
						") (implid=",
						this.m_implId,
						") (pooling=",
						(this.m_cs != null && this.m_cs.m_pooling) ? "T" : "F",
						") ",
						Trace.GetCPInfo(this.m_oracleConnectionImpl, null, null, null, false, false)
					})
				});
			}
			try
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (ConnectionState.Open == this.m_connectionState)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_ALREADY_OPEN, new string[0]));
				}
				if (this.m_cs == null || !this.m_cs.m_bInitilialized)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_INVALID_VALUE, new string[]
					{
						"OracleConnection.ConnectionString"
					}));
				}
				if (this.m_cs.m_enlist == Enlist.True && Transaction.Current != null && Transaction.Current.IsolationLevel != System.Transactions.IsolationLevel.Serializable && Transaction.Current.IsolationLevel != System.Transactions.IsolationLevel.ReadCommitted)
				{
					throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_INVALID_ISO_LEVEL, new string[0]), "isolationLevel");
				}
				if (ConfigBaseClass.m_DemandOraclePermission && this.m_orclPermission != null)
				{
					this.m_orclPermission.Demand();
				}
				if (string.IsNullOrEmpty(this.m_pdbName) && !string.IsNullOrEmpty(this.m_serviceName))
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(-7501, new string[]
					{
						"PDBName",
						"ServiceName"
					}));
				}
				CriteriaCtx criteriaCtx = new CriteriaCtx();
				criteriaCtx.m_connectionClass = this.m_drcpConnectionClass;
				if (this.m_drcpPurity == OracleConnection.OracleDRCPPurity.New)
				{
					criteriaCtx.m_bDrcpPurityNew = 1;
				}
				if (!string.IsNullOrEmpty(this.m_pdbName) || !string.IsNullOrEmpty(this.m_serviceName) || !string.IsNullOrEmpty(this.m_userProvidedConEditionName) || !string.IsNullOrEmpty(this.m_appEdition))
				{
					if (!string.IsNullOrEmpty(this.m_pdbName))
					{
						criteriaCtx.m_pdbName = this.m_pdbName.Trim().ToLowerInvariant();
						criteriaCtx.m_serviceSwitchRequested = true;
					}
					else
					{
						criteriaCtx.m_pdbName = null;
					}
					criteriaCtx.m_serviceName = ((!string.IsNullOrEmpty(this.m_serviceName)) ? this.m_serviceName.Trim().ToLowerInvariant() : null);
					if (!string.IsNullOrEmpty(this.m_userProvidedConEditionName) || !string.IsNullOrEmpty(this.m_appEdition))
					{
						if (!string.IsNullOrEmpty(this.m_appEdition) && string.IsNullOrEmpty(this.m_userProvidedConEditionName))
						{
							criteriaCtx.m_edition = this.m_appEdition.Trim();
						}
						else
						{
							criteriaCtx.m_edition = this.m_userProvidedConEditionName.Trim();
						}
					}
				}
				this.m_criteriaCtx = criteriaCtx;
				this.m_oracleConnectionImpl = OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.Get(this.m_cs, this.m_cs.m_pm, this.pmCS, this.m_securePassword, this.m_secureProxyPassword, criteriaCtx);
				this.CheckForWarnings(this);
				if (this.m_oracleConnectionImpl != null)
				{
					this.m_statementCacheSizeSnapshot = ((this.m_oracleConnectionImpl.m_statementCache != null) ? this.m_oracleConnectionImpl.m_statementCache.m_maxCacheSize : 0);
				}
				if (this.m_newPassword != null && this.m_newPassword != string.Empty)
				{
					this.m_cs.SecureWithNewPassword(this.m_newPassword);
				}
				else if (!this.m_cs.m_bSecured)
				{
					this.m_cs.Secure();
				}
				if (!this.m_cs.m_bPooled && this.m_originalConnectionString != null && this.m_originalConnectionString.Length > 0 && !this.m_cs.m_bPooled)
				{
					OracleInternal.ConnectionPool.ConnectionString.m_conStringPool.Put(this.m_cs);
				}
				if (this.m_cs.m_pm == null)
				{
					this.m_cs.m_pm = this.m_oracleConnectionImpl.m_pm;
				}
				this.pmCS = this.m_cs.m_pm.m_cs;
				this.m_securePassword = this.m_cs.m_pm.m_cs.m_securedPassword;
				this.m_secureProxyPassword = this.m_cs.m_pm.m_cs.m_securedProxyPassword;
				if (this.m_oracleConnectionImpl.m_pm.m_serverVersion != null)
				{
					this.m_serverVersion = this.m_oracleConnectionImpl.m_pm.m_serverVersion;
				}
				else
				{
					this.m_serverVersion = this.m_oracleConnectionImpl.GetServerVersion();
				}
				ConnectionState connectionState = this.m_connectionState;
				this.m_connectionState = ConnectionState.Open;
				this.m_pwdValidated = true;
				if (!this.m_cs.m_persistSecurityInfo)
				{
					this.m_originalConnectionString = null;
				}
				this.m_isDb10gR2OrHigher = this.m_oracleConnectionImpl.m_isDb10gR2OrHigher;
				this.m_isDb11gR1OrHigher = this.m_oracleConnectionImpl.m_isDb11gR1OrHigher;
				this.m_isDb12cR1OrHigher = this.m_oracleConnectionImpl.m_isDb12cR1OrHigher;
				if (this.m_cs.m_enlist == Enlist.True && Transaction.Current != null && (this.m_oracleConnectionImpl.m_mtsTxnCtx == null || (this.m_oracleConnectionImpl.m_mtsTxnCtx != null && this.m_oracleConnectionImpl.m_mtsTxnCtx.m_txnType == MTSTxnType.None)))
				{
					MTSRMManager.CCPEnlistTransaction(this.m_oracleConnectionImpl, Transaction.Current, criteriaCtx);
				}
				if (this.m_cs.m_enlist == Enlist.True && Transaction.Current != null)
				{
					this.m_oracleConnectionImpl.m_lastEnlistedTransaction = Transaction.Current;
				}
				if (this.m_oracleConnectionImpl != null)
				{
					this.m_oracleConnectionImpl.AlterSessionOnConnect(this);
				}
				if (this.m_stateChangeEventHandler != null)
				{
					this.RaiseStateChange(connectionState, this.m_connectionState);
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					if (this.m_oracleConnectionImpl.m_pxyUserSessionId == -1)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
						{
							string.Concat(new object[]
							{
								"(GET) (ENDSID=",
								this.m_oracleConnectionImpl.m_endUserSessionId,
								":",
								this.m_oracleConnectionImpl.m_endUserSerialNum,
								")"
							})
						});
					}
					else
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
						{
							string.Concat(new object[]
							{
								"(GET) (ENDSID=",
								this.m_oracleConnectionImpl.m_endUserSessionId,
								":",
								this.m_oracleConnectionImpl.m_endUserSerialNum,
								")(PXYSID=",
								this.m_oracleConnectionImpl.m_pxyUserSessionId,
								":",
								this.m_oracleConnectionImpl.m_pxyUserSerialNum,
								")"
							})
						});
					}
				}
				if (this.m_oracleConnectionImpl != null)
				{
					this.m_implId = this.m_oracleConnectionImpl.GetHashCode();
					this.m_sessionId = this.m_oracleConnectionImpl.m_endUserSessionId;
				}
				if (this.m_cs != null && this.m_cs.m_newPassword != null)
				{
					this.m_cs.SecureWithNewPassword(this.m_cs.m_newPassword);
					this.m_cs.m_newPassword = null;
				}
				OracleConnectionOpenEventArgs eventArgs = new OracleConnectionOpenEventArgs(this);
				this.OnConnectionOpen(eventArgs);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (this.m_oracleConnectionImpl != null)
				{
					this.m_oracleConnectionImpl.m_bCheckIfAlterSessionReqd = true;
				}
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
					{
						string.Concat(new object[]
						{
							"(conid=",
							this.m_id,
							") (state=",
							this.m_connectionState,
							") (sessid=",
							this.m_sessionId,
							") (implid=",
							this.m_implId,
							") (pooling=",
							(this.m_cs != null && this.m_cs.m_pooling) ? "T" : "F",
							") ",
							Trace.GetCPInfo(this.m_oracleConnectionImpl, null, null, null, true, true)
						})
					});
				}
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001F218 File Offset: 0x0001D418
		public void OpenWithNewPassword(string newPassword)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					string.Concat(new object[]
					{
						"(conid=",
						this.m_id,
						") (state=",
						this.m_connectionState,
						") (sessid=",
						this.m_sessionId,
						") (implid=",
						this.m_implId,
						") (pooling=",
						(this.m_cs != null && this.m_cs.m_pooling) ? "T" : "F",
						") (txnid=",
						(this.m_cs != null && this.m_cs.m_enlist == Enlist.True && Transaction.Current != null) ? Transaction.Current.TransactionInformation.LocalIdentifier : "n/a",
						")"
					})
				});
			}
			try
			{
				ConnectionString connectionString = this.m_cs;
				this.m_cs = connectionString.Clone();
				this.m_cs.m_bPooled = false;
				this.m_cs.m_newPassword = newPassword;
				try
				{
					this.Open();
				}
				catch (Exception)
				{
					this.m_cs = connectionString;
					throw;
				}
				connectionString = null;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
					{
						string.Concat(new object[]
						{
							"(conid=",
							this.m_id,
							") (state=",
							this.m_connectionState,
							") (sessid=",
							this.m_sessionId,
							") (implid=",
							this.m_implId,
							") (pooling=",
							(this.m_cs != null && this.m_cs.m_pooling) ? "T" : "F",
							") ",
							Trace.GetCPInfo(this.m_oracleConnectionImpl, null, null, null, true, true)
						})
					});
				}
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001F480 File Offset: 0x0001D680
		public new OracleTransaction BeginTransaction()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraTransaction = this.GetTransaction();
				bool flag = this.m_oracleConnectionImpl.m_mtsTxnCtx != null && this.m_oracleConnectionImpl.m_mtsTxnCtx.m_txnType != MTSTxnType.None;
				if (this.m_oraTransaction != null || flag)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_ALREADY_TXNED, new string[0]));
				}
				this.m_oraTransaction = new OracleTransaction(this, System.Data.IsolationLevel.ReadCommitted);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return this.m_oraTransaction;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001F55C File Offset: 0x0001D75C
		public new OracleTransaction BeginTransaction(System.Data.IsolationLevel isolationLevel)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraTransaction = this.GetTransaction();
				bool flag = this.m_oracleConnectionImpl.m_mtsTxnCtx != null && this.m_oracleConnectionImpl.m_mtsTxnCtx.m_txnType != MTSTxnType.None;
				if (this.m_oraTransaction != null || flag)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_ALREADY_TXNED, new string[0]));
				}
				if (isolationLevel != System.Data.IsolationLevel.ReadCommitted && isolationLevel != System.Data.IsolationLevel.Serializable)
				{
					throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_INVALID_ISO_LEVEL, new string[0]), "isolationLevel");
				}
				this.m_oraTransaction = new OracleTransaction(this, isolationLevel);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return this.m_oraTransaction;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001F65C File Offset: 0x0001D85C
		public override void ChangeDatabase(string pdbName)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				throw new NotSupportedException();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
					goto IL_46;
				}
				goto IL_46;
				IL_46:;
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001F6CC File Offset: 0x0001D8CC
		public override void Close()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					string.Concat(new object[]
					{
						"(conid=",
						this.m_id,
						") (state=",
						this.m_connectionState,
						") (sessid=",
						this.m_sessionId,
						") (implid=",
						this.m_implId,
						") (pooling=",
						(this.m_cs != null && this.m_cs.m_pooling) ? "T" : "F",
						") ",
						Trace.GetCPInfo(this.m_oracleConnectionImpl, null, null, null, false, false)
					})
				});
			}
			try
			{
				this.m_sessionId = 0;
				if (ConnectionState.Open == this.m_connectionState)
				{
					bool flag = true;
					if (!this.m_oracleConnectionImpl.IsConnectionAlive())
					{
						flag = false;
					}
					else if (this.m_oracleConnectionImpl.m_cs.m_pooling && !this.m_oracleConnectionImpl.m_oracleCommunication.TransportAlive)
					{
						flag = false;
					}
					if (!flag)
					{
						lock (this.m_oracleConnectionImpl)
						{
							this.m_oracleConnectionImpl.m_deletionRequestor = DeletionRequestor.HA;
							goto IL_221;
						}
					}
					bool flag3 = (this.m_oracleConnectionImpl.m_marshallingEngine.m_endOfCallStatus & 2L) != 0L;
					bool flag4 = this.m_oracleConnectionImpl.m_mtsTxnCtx != null && this.m_oracleConnectionImpl.m_mtsTxnCtx.m_txnType != MTSTxnType.None;
					if (flag3 && this.m_oraTransaction == null && !flag4)
					{
						this.m_oraTransaction = new OracleTransaction(this, System.Data.IsolationLevel.ReadCommitted);
					}
					if (this.m_oraTransaction != null)
					{
						if (this.m_oraTransaction.Completed)
						{
							this.m_oraTransaction = null;
						}
						else
						{
							try
							{
								this.m_oraTransaction.Rollback();
							}
							catch
							{
								flag = false;
								lock (this.m_oracleConnectionImpl)
								{
									this.m_oracleConnectionImpl.m_deletionRequestor = DeletionRequestor.HA;
								}
							}
							finally
							{
								this.m_oraTransaction = null;
							}
						}
					}
					IL_221:
					if (this.m_oracleConnectionImpl != null)
					{
						this.m_oracleConnectionImpl.FireConnectionCloseEvent();
					}
					if (flag && (this.m_oracleConnectionImpl.m_cs.m_pooling || this.m_oracleConnectionImpl.m_pm.m_cs.m_drcpEnabled == DrcpType.True))
					{
						this.m_oracleConnectionImpl.FlushPendingPiggybackMessages();
					}
					ConnectionState connectionState = this.m_connectionState;
					this.m_connectionState = ConnectionState.Closed;
					if (this.m_stateChangeEventHandler != null)
					{
						this.RaiseStateChange(connectionState, this.m_connectionState);
					}
					if (this.m_oracleConnectionImpl != null)
					{
						this.m_oracleConnectionImpl.m_lastEnlistedTransaction = null;
						if ((this.m_cs.m_pooling || this.m_cs.m_drcpEnabled == DrcpType.True) && flag)
						{
							this.m_oracleConnectionImpl.ResetEndToEndMetrics();
						}
						this.m_statementCacheSizeSnapshot = ((this.m_oracleConnectionImpl.m_statementCache != null) ? this.m_oracleConnectionImpl.m_statementCache.m_maxCacheSize : 0);
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							if (this.m_oracleConnectionImpl.m_pxyUserSessionId == -1)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
								{
									string.Concat(new object[]
									{
										"(PUT) (ENDSID=",
										this.m_oracleConnectionImpl.m_endUserSessionId,
										":",
										this.m_oracleConnectionImpl.m_endUserSerialNum,
										")"
									})
								});
							}
							else
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
								{
									string.Concat(new object[]
									{
										"(PUT) (ENDSID=",
										this.m_oracleConnectionImpl.m_endUserSessionId,
										":",
										this.m_oracleConnectionImpl.m_endUserSerialNum,
										")(PXYSID=",
										this.m_oracleConnectionImpl.m_pxyUserSessionId,
										":",
										this.m_oracleConnectionImpl.m_pxyUserSerialNum,
										")"
									})
								});
							}
						}
						CriteriaCtx criteriaCtx = null;
						OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.PutFromApp(this.m_oracleConnectionImpl, criteriaCtx);
						this.m_oracleConnectionImpl.bSessionSwitched = false;
						this.m_oracleConnectionImpl = null;
					}
					if (this.m_metaDataCollectionDS != null)
					{
						this.m_metaDataCollectionDS.Clear();
						this.m_metaDataCollectionDS.Dispose();
						this.m_metaDataCollectionDS = null;
					}
				}
			}
			catch (OracleException ex)
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Error, new string[]
					{
						ex.ToString()
					});
				}
			}
			catch (NetworkException ex2)
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Error, new string[]
					{
						ex2.ToString()
					});
				}
			}
			catch (Exception ex3)
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Error, new string[]
					{
						ex3.ToString()
					});
				}
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
					{
						string.Concat(new object[]
						{
							"(conid=",
							this.m_id,
							") (state=",
							this.m_connectionState,
							") (sessid=",
							this.m_sessionId,
							") (implid=",
							this.m_implId,
							") (pooling=",
							(this.m_cs != null && this.m_cs.m_pooling) ? "T" : "F",
							") ",
							Trace.GetCPInfo(this.m_oracleConnectionImpl, null, null, null, false, false)
						})
					});
				}
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001FD74 File Offset: 0x0001DF74
		public new OracleCommand CreateCommand()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleCommand result = null;
			try
			{
				result = new OracleCommand("", this);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001FDF4 File Offset: 0x0001DFF4
		public object Clone()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleConnection oracleConnection = null;
			try
			{
				oracleConnection = (OracleConnection)base.MemberwiseClone();
				oracleConnection.m_oracleConnectionImpl = null;
				oracleConnection.m_connectionState = ConnectionState.Closed;
				oracleConnection.m_newPassword = null;
				oracleConnection.m_pwdValidated = false;
				oracleConnection.m_stateChangeEventHandler = null;
				oracleConnection.m_infoMessageEventHandler = null;
				oracleConnection.m_disposed = false;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return oracleConnection;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001FEA4 File Offset: 0x0001E0A4
		public OracleGlobalization GetSessionInfo()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleGlobalization result;
			try
			{
				if (this.m_connectionState == ConnectionState.Closed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
				}
				if (this.m_oracleConnectionImpl.m_oracleGlobalizationImpl == null)
				{
					this.m_oracleConnectionImpl.m_oracleGlobalizationImpl = new OracleGlobalizationImpl();
					this.m_oracleConnectionImpl.m_oracleGlobalizationImpl.AlterSession(this.m_oracleConnectionImpl.m_oracleGlobalizationImpl, this);
				}
				OracleGlobalizationImpl oracleGlobImpl = (OracleGlobalizationImpl)this.m_oracleConnectionImpl.m_oracleGlobalizationImpl.Clone();
				result = new OracleGlobalization(oracleGlobImpl);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001FF88 File Offset: 0x0001E188
		public void GetSessionInfo(OracleGlobalization oraGlob)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_connectionState == ConnectionState.Closed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
				}
				if (this.m_oracleConnectionImpl.m_oracleGlobalizationImpl == null)
				{
					this.m_oracleConnectionImpl.m_oracleGlobalizationImpl = new OracleGlobalizationImpl();
					this.m_oracleConnectionImpl.m_oracleGlobalizationImpl.AlterSession(this.m_oracleConnectionImpl.m_oracleGlobalizationImpl, this);
				}
				oraGlob.m_oracleGlobalizationImpl.RefreshFrom(this.m_oracleConnectionImpl.m_oracleGlobalizationImpl);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00020064 File Offset: 0x0001E264
		public void SetSessionInfo(OracleGlobalization oraGlob)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_connectionState == ConnectionState.Closed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
				}
				if (oraGlob != null)
				{
					if (this.m_oracleConnectionImpl.m_oracleGlobalizationImpl == null)
					{
						this.m_oracleConnectionImpl.m_oracleGlobalizationImpl = new OracleGlobalizationImpl();
						this.m_oracleConnectionImpl.m_oracleGlobalizationImpl.AlterSession(this.m_oracleConnectionImpl.m_oracleGlobalizationImpl, this);
					}
					this.m_oracleConnectionImpl.m_oracleGlobalizationImpl.AlterSession(oraGlob.m_oracleGlobalizationImpl, this);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00020148 File Offset: 0x0001E348
		public void PurgeStatementCache()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_oracleConnectionImpl != null && this.m_oracleConnectionImpl.m_statementCache != null)
				{
					this.m_oracleConnectionImpl.PurgeStatementCache(0);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x000201D8 File Offset: 0x0001E3D8
		public override DataTable GetSchema()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			DataTable result = null;
			try
			{
				result = this.GetSchema(DbMetaDataCollectionNames.MetaDataCollections, null);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00020258 File Offset: 0x0001E458
		public override DataTable GetSchema(string collectionName)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			DataTable result = null;
			try
			{
				result = this.GetSchema(collectionName, null);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x000202D4 File Offset: 0x0001E4D4
		public override DataTable GetSchema(string collectionName, string[] restrictionsArray)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			DataTable dataTable = null;
			try
			{
				if (collectionName == null || collectionName.Length == 0)
				{
					throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_GS_COLL_NOT_DEFINED, new string[]
					{
						collectionName
					}));
				}
				if (this.m_connectionState == ConnectionState.Closed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
				}
				if (this.m_metaDataCollectionDS == null)
				{
					this.LoadMetaDataXmlDS();
				}
				if (this.m_metaDataCollectionDS == null)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_GS_NO_METADATA_STREAM, new string[]
					{
						collectionName
					}));
				}
				dataTable = new DataTable();
				string text = this.NormalizeDBVersion(this.m_serverVersion);
				string text2 = collectionName.ToUpperInvariant();
				int num = 0;
				if (restrictionsArray != null)
				{
					num = restrictionsArray.Length;
				}
				string a;
				if ((a = text2) != null)
				{
					if (!(a == "METADATACOLLECTIONS"))
					{
						if (!(a == "DATATYPES"))
						{
							if (!(a == "RESTRICTIONS"))
							{
								if (!(a == "RESERVEDWORDS"))
								{
									if (a == "DATASOURCEINFORMATION")
									{
										if (num > 0)
										{
											throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_GS_MORE_RESTRICTIONS, new string[]
											{
												collectionName,
												"0"
											}));
										}
										dataTable = this.m_metaDataCollectionDS.Tables[collectionName].Copy();
										dataTable.Rows[0][DbMetaDataColumnNames.DataSourceProductVersion] = this.m_serverVersion;
										dataTable.Rows[0][DbMetaDataColumnNames.DataSourceProductVersionNormalized] = text;
										dataTable.TableName = DbMetaDataCollectionNames.DataSourceInformation;
										dataTable.AcceptChanges();
										goto IL_70F;
									}
								}
								else
								{
									if (num > 0)
									{
										throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_GS_MORE_RESTRICTIONS, new string[]
										{
											collectionName,
											"0"
										}));
									}
									this.PopulateSupportedDataRows(dataTable, collectionName, text);
									dataTable.TableName = DbMetaDataCollectionNames.ReservedWords;
									dataTable.AcceptChanges();
									goto IL_70F;
								}
							}
							else
							{
								if (num > 0)
								{
									throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_GS_MORE_RESTRICTIONS, new string[]
									{
										collectionName,
										"0"
									}));
								}
								this.PopulateSupportedDataRows(dataTable, collectionName, text);
								dataTable.TableName = DbMetaDataCollectionNames.Restrictions;
								dataTable.AcceptChanges();
								goto IL_70F;
							}
						}
						else
						{
							if (num > 0)
							{
								throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_GS_MORE_RESTRICTIONS, new string[]
								{
									collectionName,
									"0"
								}));
							}
							this.PopulateSupportedDataRows(dataTable, collectionName, text);
							dataTable.TableName = DbMetaDataCollectionNames.DataTypes;
							dataTable.AcceptChanges();
							goto IL_70F;
						}
					}
					else
					{
						if (num > 0)
						{
							throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_GS_MORE_RESTRICTIONS, new string[]
							{
								collectionName,
								"0"
							}));
						}
						this.PopulateSupportedDataRows(dataTable, collectionName, text);
						dataTable.TableName = DbMetaDataCollectionNames.MetaDataCollections;
						dataTable.AcceptChanges();
						goto IL_70F;
					}
				}
				string text3 = null;
				int num2 = 0;
				string text4 = null;
				bool flag = false;
				bool flag2 = false;
				DataRowCollection rows = this.m_metaDataCollectionDS.Tables[DbMetaDataCollectionNames.MetaDataCollections].Rows;
				for (int i = 0; i < rows.Count; i++)
				{
					if (((string)rows[i][DbMetaDataColumnNames.CollectionName]).ToUpperInvariant() == text2 && ((string)rows[i]["PopulationMechanism"]).ToUpperInvariant() == "ORACLECOMMAND")
					{
						flag2 = true;
						if (text4 == null)
						{
							text4 = (string)rows[i][DbMetaDataColumnNames.CollectionName];
						}
						if (this.SupportedInCurrentVersion(rows[i], text))
						{
							num2 = (int)rows[i][DbMetaDataColumnNames.NumberOfRestrictions];
							text3 = (string)rows[i]["PopulationString"];
							flag = false;
							break;
						}
						flag = true;
					}
					else if (((string)rows[i][DbMetaDataColumnNames.CollectionName]).ToUpperInvariant() == text2 && ((string)rows[i]["PopulationMechanism"]).ToUpperInvariant() == "DATATABLE")
					{
						dataTable = this.m_metaDataCollectionDS.Tables[collectionName].Copy();
						dataTable.TableName = collectionName.ToString();
						dataTable.AcceptChanges();
						return dataTable;
					}
				}
				if (!flag2)
				{
					throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_GS_COLL_NOT_DEFINED, new string[]
					{
						collectionName
					}));
				}
				if (flag)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_GS_COLL_NOT_SUPPORTED, new string[]
					{
						collectionName
					}));
				}
				if (num > num2)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_GS_MORE_RESTRICTIONS, new string[]
					{
						collectionName,
						num2.ToString()
					}));
				}
				DataRowCollection rows2 = this.m_metaDataCollectionDS.Tables[DbMetaDataCollectionNames.Restrictions].Rows;
				int num3 = 0;
				ArrayList arrayList = new ArrayList();
				for (int j = 0; j < rows2.Count; j++)
				{
					if (((string)rows2[j][DbMetaDataColumnNames.CollectionName]).ToUpperInvariant() == text2)
					{
						OracleParameter oracleParameter = new OracleParameter();
						OracleParameter oracleParameter2 = new OracleParameter();
						if (restrictionsArray != null)
						{
							if (num3 >= restrictionsArray.Length)
							{
								oracleParameter.Value = null;
								oracleParameter2.Value = null;
							}
							else
							{
								oracleParameter.Value = restrictionsArray[num3];
								oracleParameter2.Value = restrictionsArray[num3];
							}
						}
						else
						{
							oracleParameter.Value = null;
							oracleParameter2.Value = null;
						}
						oracleParameter.ParameterName = (string)rows2[j]["ParameterName"];
						oracleParameter2.ParameterName = (string)rows2[j]["ParameterName"];
						arrayList.Add(oracleParameter);
						arrayList.Add(oracleParameter2);
						num3++;
						if (num3 >= num2)
						{
							break;
						}
					}
				}
				if (text3 == null)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_GS_NO_POPULATION_STRING, new string[]
					{
						collectionName
					}));
				}
				OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(text3, this);
				oracleDataAdapter.SelectCommand.InitialLONGFetchSize = -1;
				foreach (object obj in arrayList)
				{
					OracleParameter param = (OracleParameter)obj;
					oracleDataAdapter.SelectCommand.Parameters.Add(param);
				}
				try
				{
					oracleDataAdapter.Fill(dataTable);
				}
				catch (Exception innerException)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_GS_QUERY_FAILED, new string[]
					{
						collectionName
					}), innerException);
				}
				if (text4 != null)
				{
					dataTable.TableName = text4;
				}
				dataTable.AcceptChanges();
				foreach (object obj2 in arrayList)
				{
					OracleParameter oracleParameter3 = (OracleParameter)obj2;
					oracleParameter3.Dispose();
				}
				arrayList.Clear();
				arrayList = null;
				oracleDataAdapter.Dispose();
				oracleDataAdapter = null;
				IL_70F:;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return dataTable;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00020AA0 File Offset: 0x0001ECA0
		public void EnlistDistributedTransaction(ITransaction itrans)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					string.Concat(new object[]
					{
						"(conid=",
						this.m_id,
						") (state=",
						this.m_connectionState,
						") (sessid=",
						this.m_sessionId,
						") (implid=",
						this.m_implId,
						") (pooling=",
						(this.m_cs != null && this.m_cs.m_pooling) ? "T" : "F",
						") ",
						Trace.GetCPInfo(this.m_oracleConnectionImpl, null, null, null, false, false)
					})
				});
			}
			try
			{
				if (itrans == null)
				{
					throw new ArgumentException();
				}
				Transaction transaction = null;
				try
				{
					transaction = TransactionInterop.GetTransactionFromDtcTransaction(itrans as IDtcTransaction);
				}
				catch (Exception ex)
				{
					throw new ArgumentException(ex.Message, ex);
				}
				this.EnlistTransaction(transaction);
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex2, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
					{
						string.Concat(new object[]
						{
							"(conid=",
							this.m_id,
							") (state=",
							this.m_connectionState,
							") (sessid=",
							this.m_sessionId,
							") (implid=",
							this.m_implId,
							") (pooling=",
							(this.m_cs != null && this.m_cs.m_pooling) ? "T" : "F",
							") ",
							Trace.GetCPInfo(this.m_oracleConnectionImpl, null, null, null, true, false)
						})
					});
				}
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00020CD0 File Offset: 0x0001EED0
		public override void EnlistTransaction(Transaction transaction)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					string.Concat(new object[]
					{
						"(conid=",
						this.m_id,
						") (state=",
						this.m_connectionState,
						") (sessid=",
						this.m_sessionId,
						") (implid=",
						this.m_implId,
						") (pooling=",
						(this.m_cs != null && this.m_cs.m_pooling) ? "T" : "F",
						") ",
						Trace.GetCPInfo(this.m_oracleConnectionImpl, null, null, null, false, false)
					})
				});
			}
			try
			{
				if (this.m_connectionState == ConnectionState.Closed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
				}
				if (this.m_oraTransaction != null && !this.m_oraTransaction.Completed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_ALREADY_TXNED, new string[0]));
				}
				if (this.m_oracleConnectionImpl.m_mtsTxnCtx != null && this.m_oracleConnectionImpl.m_mtsTxnCtx.m_txnType != MTSTxnType.None)
				{
					if (string.Compare(this.m_oracleConnectionImpl.m_mtsTxnCtx.m_txnLocalID, transaction.TransactionInformation.LocalIdentifier, true) != 0)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_ALREADY_TXNED, new string[0]));
					}
					this.m_oracleConnectionImpl.m_lastEnlistedTransaction = transaction;
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
						{
							"Connection with Conn ID = " + this.m_oracleConnectionImpl.m_endUserSessionId + " is already enlisted"
						});
					}
				}
				else if (!(transaction == null))
				{
					this.m_oracleConnectionImpl.m_pm.ProcessCriteriaCtx_EnlistedConnection(ref this.m_criteriaCtx);
					MTSRMManager.CCPEnlistTransaction(this.m_oracleConnectionImpl, transaction, this.m_criteriaCtx);
					this.m_oracleConnectionImpl.m_lastEnlistedTransaction = transaction;
					this.m_oracleConnectionImpl.m_bDynamicallyEnlisted = true;
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
					{
						string.Concat(new object[]
						{
							"(conid=",
							this.m_id,
							") (state=",
							this.m_connectionState,
							") (sessid=",
							this.m_sessionId,
							") (implid=",
							this.m_implId,
							") (pooling=",
							(this.m_cs != null && this.m_cs.m_pooling) ? "T" : "F",
							") ",
							Trace.GetCPInfo(this.m_oracleConnectionImpl, null, null, null, true, false)
						})
					});
				}
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00021004 File Offset: 0x0001F204
		public static void ClearAllPools()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			if (ConfigBaseClass.m_DemandOraclePermission)
			{
				new OraclePermission(PermissionState.Unrestricted).Demand();
			}
			try
			{
				OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.ClearAllPools();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0002108C File Offset: 0x0001F28C
		public static void ClearPool(OracleConnection conn)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			if (ConfigBaseClass.m_DemandOraclePermission && conn.m_orclPermission != null)
			{
				conn.m_orclPermission.Demand();
			}
			try
			{
				if (conn != null && conn.m_cs != null && !string.IsNullOrEmpty(conn.m_cs.m_pmId))
				{
					OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.ClearPool(conn.m_cs);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060003FC RID: 1020 RVA: 0x0002113C File Offset: 0x0001F33C
		// (remove) Token: 0x060003FD RID: 1021 RVA: 0x00021158 File Offset: 0x0001F358
		public override event StateChangeEventHandler StateChange
		{
			add
			{
				this.m_stateChangeEventHandler = (StateChangeEventHandler)Delegate.Combine(this.m_stateChangeEventHandler, value);
			}
			remove
			{
				this.m_stateChangeEventHandler = (StateChangeEventHandler)Delegate.Remove(this.m_stateChangeEventHandler, value);
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00021174 File Offset: 0x0001F374
		protected override DbTransaction BeginDbTransaction(System.Data.IsolationLevel isolationLevel)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			DbTransaction result = null;
			try
			{
				if (System.Data.IsolationLevel.Unspecified == isolationLevel)
				{
					isolationLevel = System.Data.IsolationLevel.ReadCommitted;
				}
				result = this.BeginTransaction(isolationLevel);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000211F8 File Offset: 0x0001F3F8
		protected override void Dispose(bool disposing)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			this.m_disposed = true;
			this.m_dataSource = string.Empty;
			this.m_serverVersion = string.Empty;
			this.m_serviceName = string.Empty;
			this.m_pdbName = string.Empty;
			this.m_appEdition = string.Empty;
			this.m_userProvidedConEditionName = string.Empty;
			try
			{
				bool flag = this.m_connectionState == ConnectionState.Closed && this.m_oracleConnectionImpl == null;
				try
				{
					if (!disposing && !flag && OraclePool.m_bPerfNumberOfReclaimedConnections)
					{
						OraclePool.PerformanceCounterIncrement(OraclePerfParams.CounterIndex.NumberOfReclaimedConnections, this.m_oracleConnectionImpl, this.m_oracleConnectionImpl.m_cp);
					}
				}
				catch (Exception ex)
				{
					if (ProviderConfig.m_bTraceLevelPublic)
					{
						Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Error, new string[]
						{
							ex.ToString()
						});
					}
				}
				if (!flag)
				{
					try
					{
						this.Close();
					}
					catch (Exception ex2)
					{
						if (ProviderConfig.m_bTraceLevelPublic)
						{
							Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Error, new string[]
							{
								ex2.ToString()
							});
						}
					}
				}
				try
				{
					base.Dispose(disposing);
				}
				catch (Exception ex3)
				{
					if (ProviderConfig.m_bTraceLevelPublic)
					{
						Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Error, new string[]
						{
							ex3.ToString()
						});
					}
				}
				try
				{
					GC.SuppressFinalize(this);
				}
				catch (Exception ex4)
				{
					if (ProviderConfig.m_bTraceLevelPublic)
					{
						Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Error, new string[]
						{
							ex4.ToString()
						});
					}
				}
			}
			catch (Exception ex5)
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Error, new string[]
					{
						ex5.ToString()
					});
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00021440 File Offset: 0x0001F640
		protected override DbCommand CreateDbCommand()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			DbCommand result = null;
			try
			{
				result = new OracleCommand("", this);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000214C0 File Offset: 0x0001F6C0
		protected override void OnStateChange(StateChangeEventArgs eventArgs)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_stateChangeEventHandler != null)
				{
					this.m_stateChangeEventHandler(this, eventArgs);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00021544 File Offset: 0x0001F744
		internal void OnConnectionOpen(OracleConnectionOpenEventArgs eventArgs)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_conOpenEventHandler != null)
				{
					this.m_conOpenEventHandler(eventArgs);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000215C8 File Offset: 0x0001F7C8
		internal static void OnHAEvent(object state)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				OracleHAEventArgs eventArgs = (OracleHAEventArgs)state;
				if (OracleConnection.m_haEventHandler != null)
				{
					OracleConnection.m_haEventHandler(eventArgs);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00021650 File Offset: 0x0001F850
		internal int m_majorVersion
		{
			get
			{
				return this.m_oracleConnectionImpl.DatabaseMajorVersion;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00021660 File Offset: 0x0001F860
		internal int m_minorVersion
		{
			get
			{
				return this.m_oracleConnectionImpl.DatabaseMinorVersion;
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00021670 File Offset: 0x0001F870
		private void PopulateSupportedDataRows(DataTable dt, string collectionName, string normalizedDBVersion)
		{
			int count = this.m_metaDataCollectionDS.Tables[collectionName].Columns.Count;
			for (int i = 0; i < count; i++)
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.ColumnName = this.m_metaDataCollectionDS.Tables[collectionName].Columns[i].ColumnName;
				dataColumn.DataType = this.m_metaDataCollectionDS.Tables[collectionName].Columns[i].DataType;
				dt.Columns.Add(dataColumn);
			}
			DataRowCollection rows = this.m_metaDataCollectionDS.Tables[collectionName].Rows;
			foreach (object obj in rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (this.SupportedInCurrentVersion(dataRow, normalizedDBVersion))
				{
					DataRow dataRow2 = dt.NewRow();
					for (int j = 0; j < count; j++)
					{
						dataRow2[j] = dataRow[j];
					}
					dt.Rows.Add(dataRow2);
				}
			}
			dt.Columns.Remove("MaximumVersion");
			dt.Columns.Remove("MinimumVersion");
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000217C8 File Offset: 0x0001F9C8
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		private void LoadMetaDataXmlDS()
		{
			Stream stream = null;
			try
			{
				string metaDataXml = ConfigBaseClass.m_MetaDataXml;
				if (!string.IsNullOrWhiteSpace(metaDataXml))
				{
					try
					{
						Configuration configuration = ConfigurationManager.OpenMachineConfiguration();
						stream = new FileStream(configuration.FilePath.Replace("machine.config", metaDataXml), FileMode.Open);
					}
					catch (FileNotFoundException)
					{
						throw new ConfigurationErrorsException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_GS_NO_CUSTOM_FILE, new string[]
						{
							metaDataXml
						}));
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			if (stream == null)
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				stream = executingAssembly.GetManifestResourceStream("Oracle.ManagedDataAccess.src.Client.Resources.OracleMetadata.xml");
			}
			if (stream != null)
			{
				XmlTextReader xmlTextReader = new XmlTextReader(stream);
				this.m_metaDataCollectionDS = new DataSet("DocumentElement");
				this.m_metaDataCollectionDS.ReadXml(xmlTextReader);
				xmlTextReader.Close();
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00021890 File Offset: 0x0001FA90
		private string NormalizeDBVersion(string str)
		{
			string text = null;
			int num = 0;
			int num2 = 0;
			int length = str.Length;
			while (num <= length && num2 > -1)
			{
				num2 = str.IndexOf(".", num);
				if (num2 == -1)
				{
					if (length - num == 1)
					{
						text += "0";
					}
					text += str.Substring(num, length - num);
					break;
				}
				if (num2 - num == 1)
				{
					text += "0";
				}
				text += str.Substring(num, num2 - num + 1);
				num = num2 + 1;
			}
			return text;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00021914 File Offset: 0x0001FB14
		private bool SupportedInCurrentVersion(DataRow row, string normalizedDBVersion)
		{
			string xmlnormalizedDBVersion = row["MaximumVersion"].ToString();
			string xmlnormalizedDBVersion2 = row["MinimumVersion"].ToString();
			return this.ComparenormalizedDBVersions(normalizedDBVersion, xmlnormalizedDBVersion2) >= 0 && this.ComparenormalizedDBVersions(normalizedDBVersion, xmlnormalizedDBVersion) <= 0;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0002195C File Offset: 0x0001FB5C
		private int ComparenormalizedDBVersions(string normalizedDBVersion, string xmlnormalizedDBVersion)
		{
			int result = 0;
			int i = 0;
			int length = normalizedDBVersion.Length;
			NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
			numberFormatInfo.NumberDecimalSeparator = ".";
			if (xmlnormalizedDBVersion.Length > 0)
			{
				while (i <= length)
				{
					if (int.Parse(normalizedDBVersion.Substring(i, 2), numberFormatInfo) > int.Parse(xmlnormalizedDBVersion.Substring(i, 2), numberFormatInfo))
					{
						return 1;
					}
					if (int.Parse(normalizedDBVersion.Substring(i, 2), numberFormatInfo) < int.Parse(xmlnormalizedDBVersion.Substring(i, 2), numberFormatInfo))
					{
						return -1;
					}
					i += 3;
				}
			}
			return result;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000219E0 File Offset: 0x0001FBE0
		internal OracleTransaction GetTransaction()
		{
			if (this.m_oraTransaction != null && this.m_oraTransaction.Completed)
			{
				this.m_oraTransaction = null;
			}
			return this.m_oraTransaction;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00021A04 File Offset: 0x0001FC04
		internal void InitializeOrclPermission(string connString)
		{
			if (ConfigBaseClass.m_DemandOraclePermission)
			{
				if (this.m_orclPermission == null)
				{
					this.m_orclPermission = new OraclePermission(PermissionState.None);
				}
				this.m_orclPermission.Clear();
				this.m_orclPermission.Add(connString, "", KeyRestrictionBehavior.AllowOnly);
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00021A40 File Offset: 0x0001FC40
		internal void RaiseStateChange(ConnectionState originalState, ConnectionState currentState)
		{
			StateChangeEventArgs stateChange = new StateChangeEventArgs(originalState, currentState);
			this.OnStateChange(stateChange);
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600040E RID: 1038 RVA: 0x00021A5C File Offset: 0x0001FC5C
		// (remove) Token: 0x0600040F RID: 1039 RVA: 0x00021A78 File Offset: 0x0001FC78
		public event OracleInfoMessageEventHandler InfoMessage
		{
			add
			{
				this.m_infoMessageEventHandler = (OracleInfoMessageEventHandler)Delegate.Combine(this.m_infoMessageEventHandler, value);
			}
			remove
			{
				this.m_infoMessageEventHandler = (OracleInfoMessageEventHandler)Delegate.Remove(this.m_infoMessageEventHandler, value);
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00021A94 File Offset: 0x0001FC94
		internal void OnInfoMessage(object obj, int errCode, string warningMsg)
		{
			if (this.m_infoMessageEventHandler != null)
			{
				OracleError value = new OracleError(errCode, this.DataSource, string.Empty, warningMsg);
				OracleErrorCollection oracleErrorCollection = new OracleErrorCollection();
				oracleErrorCollection.Add(value);
				try
				{
					this.m_infoMessageEventHandler(obj, new OracleInfoMessageEventArgs(oracleErrorCollection));
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00021AF4 File Offset: 0x0001FCF4
		internal void CheckForWarnings(object source)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				string warningMsg;
				int errCode;
				if (this.m_infoMessageEventHandler != null && this.m_oracleConnectionImpl.GetLastWarning(out warningMsg, out errCode))
				{
					this.OnInfoMessage(source, errCode, warningMsg);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00021B84 File Offset: 0x0001FD84
		public IEnumerable<OracleLpTableColumns> GetColumnInformation(IEnumerable<OracleLpTable> tables)
		{
			return OracleMetadata.GetColumnInformation(this, tables);
		}

		// Token: 0x04000582 RID: 1410
		private const string METADATA_COLLECTION = "METADATACOLLECTIONS";

		// Token: 0x04000583 RID: 1411
		private const string DATA_TYPES = "DATATYPES";

		// Token: 0x04000584 RID: 1412
		private const string RESTRICTIONS = "RESTRICTIONS";

		// Token: 0x04000585 RID: 1413
		private const string RESERVED_WORDS = "RESERVEDWORDS";

		// Token: 0x04000586 RID: 1414
		private const string DATA_SOURCE_INFORMATION = "DATASOURCEINFORMATION";

		// Token: 0x04000587 RID: 1415
		private const string ORCL_COMMAND = "ORACLECOMMAND";

		// Token: 0x04000588 RID: 1416
		private const string DATA_TABLE = "DATATABLE";

		// Token: 0x04000589 RID: 1417
		internal static string s_getLTXIDstatus = "declare n1 number; n2 number; committed boolean; userCallCompleted boolean; begin committed := FALSE; userCallCompleted := FALSE;sys.dbms_app_cont.get_ltxid_outcome(:1, committed, userCallCompleted); if committed then n1:=1; else n1:=0; end if; if userCallCompleted then n2:=1; else n2:=0; end if; :2 := n1; :3 := n2; end; ";

		// Token: 0x0400058A RID: 1418
		internal OraclePermission m_orclPermission;

		// Token: 0x0400058B RID: 1419
		internal ConnectionString m_cs;

		// Token: 0x0400058C RID: 1420
		internal string m_originalConnectionString;

		// Token: 0x0400058D RID: 1421
		internal OracleConnectionImpl m_oracleConnectionImpl;

		// Token: 0x0400058E RID: 1422
		internal ConnectionState m_connectionState;

		// Token: 0x0400058F RID: 1423
		internal OracleTransaction m_oraTransaction;

		// Token: 0x04000590 RID: 1424
		private string m_serverVersion;

		// Token: 0x04000591 RID: 1425
		internal OracleLogicalTransaction m_logicalTransaction;

		// Token: 0x04000592 RID: 1426
		internal string m_newPassword;

		// Token: 0x04000593 RID: 1427
		internal ConnectionString pmCS;

		// Token: 0x04000594 RID: 1428
		private CriteriaCtx m_criteriaCtx;

		// Token: 0x04000595 RID: 1429
		internal static OracleLpParser OracleLpParser = new OracleLpParser(OracleMbEarleySqlPlsqlAnsiRuleProcessorTableDictionary.Instance);

		// Token: 0x04000596 RID: 1430
		[ThreadStatic]
		internal static bool bIgnoreLogicalTransaction = false;

		// Token: 0x04000597 RID: 1431
		internal bool bConnectionforTxnStatus;

		// Token: 0x04000598 RID: 1432
		private DataSet m_metaDataCollectionDS;

		// Token: 0x04000599 RID: 1433
		private bool m_pwdValidated;

		// Token: 0x0400059A RID: 1434
		private bool m_disposed;

		// Token: 0x0400059B RID: 1435
		private SecureString m_securePassword;

		// Token: 0x0400059C RID: 1436
		private SecureString m_secureProxyPassword;

		// Token: 0x0400059D RID: 1437
		internal bool m_isDb10gR2OrHigher;

		// Token: 0x0400059E RID: 1438
		internal bool m_isDb11gR1OrHigher;

		// Token: 0x0400059F RID: 1439
		internal bool m_isDb12cR1OrHigher;

		// Token: 0x040005A0 RID: 1440
		internal StateChangeEventHandler m_stateChangeEventHandler;

		// Token: 0x040005A1 RID: 1441
		internal static OracleHAEventHandler m_haEventHandler;

		// Token: 0x040005A2 RID: 1442
		private OracleInfoMessageEventHandler m_infoMessageEventHandler;

		// Token: 0x040005A3 RID: 1443
		internal OracleConnectionOpenEventHandler m_conOpenEventHandler;

		// Token: 0x040005A4 RID: 1444
		private int m_statementCacheSizeSnapshot;

		// Token: 0x040005A5 RID: 1445
		private int m_connectionTimeout = 15;

		// Token: 0x040005A6 RID: 1446
		private string m_dataSource = string.Empty;

		// Token: 0x040005A7 RID: 1447
		private int m_implId;

		// Token: 0x040005A8 RID: 1448
		private int m_sessionId;

		// Token: 0x040005A9 RID: 1449
		private string m_id;

		// Token: 0x040005AA RID: 1450
		internal string m_pdbName = string.Empty;

		// Token: 0x040005AB RID: 1451
		internal string m_serviceName = string.Empty;

		// Token: 0x040005AC RID: 1452
		private string m_appEdition = string.Empty;

		// Token: 0x040005AD RID: 1453
		internal string m_userProvidedConEditionName = string.Empty;

		// Token: 0x040005AE RID: 1454
		internal string m_drcpConnectionClass;

		// Token: 0x040005AF RID: 1455
		internal OracleConnection.OracleDRCPPurity m_drcpPurity = OracleConnection.OracleDRCPPurity.Pooled;

		// Token: 0x040005B0 RID: 1456
		internal bool m_isPuritySet;

		// Token: 0x040005B1 RID: 1457
		internal static bool m_enableDRCP = false;

		// Token: 0x040005B2 RID: 1458
		internal string m_drcpPLSQLCallback;

		// Token: 0x02000058 RID: 88
		public enum OracleDRCPPurity
		{
			// Token: 0x040005B4 RID: 1460
			New = 1,
			// Token: 0x040005B5 RID: 1461
			Pooled
		}
	}
}
