using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Data.Metadata.Edm;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Xml;
using Oracle.DataAccess.Types;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000055 RID: 85
	[Designer("Oracle.VsDevTools.OracleVSGCommandDesigner, Oracle.VsDevTools, Version=4.112.3.0, Culture=neutral, PublicKeyToken=89b483f429c47342, processorArchitecture=X86", typeof(IDesigner))]
	[ToolboxBitmap(typeof(resfinder), "Oracle.DataAccess.src.Client.Icons.OracleCommandToolBox_hc.bmp")]
	[SecurityPermission(SecurityAction.Assert, ControlThread = true)]
	public sealed class OracleCommand : DbCommand, ICloneable
	{
		// Token: 0x060003EE RID: 1006 RVA: 0x0002AB49 File Offset: 0x00029B49
		static OracleCommand()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0002AB57 File Offset: 0x00029B57
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0002AB5F File Offset: 0x00029B5F
		protected override DbConnection DbConnection
		{
			get
			{
				return this.m_connection;
			}
			set
			{
				this.Connection = (OracleConnection)value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0002AB6D File Offset: 0x00029B6D
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0002AB78 File Offset: 0x00029B78
		[Description("")]
		[Category("Behavior")]
		[DefaultValue(null)]
		public new OracleConnection Connection
		{
			get
			{
				return this.m_connection;
			}
			set
			{
				if (this.m_connection != value || (value != null && this.m_conSignature != value.m_conSignature))
				{
					if (this.m_metaData != null)
					{
						this.m_metaData = null;
					}
					if (this.m_opsSqlCtx != IntPtr.Zero)
					{
						try
						{
							if (!this.m_addToStmtCache)
							{
								OpsSql.FreeCtx(ref this.m_opsSqlCtx, this.m_opsErrCtx, 0);
							}
							else
							{
								OpsSql.FreeCtx(ref this.m_opsSqlCtx, this.m_opsErrCtx, 1);
							}
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
						}
						this.m_opsSqlCtx = IntPtr.Zero;
					}
					if (this.m_opsErrCtx != IntPtr.Zero)
					{
						try
						{
							OpsErr.FreeCtx(ref this.m_opsErrCtx);
						}
						catch (Exception ex2)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex2);
							}
						}
						this.m_opsErrCtx = IntPtr.Zero;
					}
					this.m_connection = value;
					if (this.m_connection != null)
					{
						this.m_conSignature = this.m_connection.m_conSignature;
						return;
					}
					this.m_conSignature = 0;
				}
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0002AC94 File Offset: 0x00029C94
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0002ACAC File Offset: 0x00029CAC
		[DefaultValue("")]
		[Description("")]
		[Category("Data")]
		public override string CommandText
		{
			get
			{
				if (this.m_commandText != null)
				{
					return this.m_commandText;
				}
				return string.Empty;
			}
			set
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleCommand::CommandText(): set\n"
					});
				}
				if (this.m_commandText != value)
				{
					this.m_commandText = value;
					this.m_parsed = false;
					this.m_addParam = true;
					this.m_selectStmt = false;
					this.m_cmdTxtModified = true;
					this.m_metaData = null;
					this.m_utf8CmdText = null;
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleCommand::CommandText(): set\n"
					});
				}
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0002AD32 File Offset: 0x00029D32
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0002AD3A File Offset: 0x00029D3A
		[Description("")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool AddToStatementCache
		{
			get
			{
				return this.m_addToStatementCache;
			}
			set
			{
				if (this.m_addToStatementCache != value)
				{
					this.m_addToStatementCache = value;
				}
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0002AD4C File Offset: 0x00029D4C
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x0002AD54 File Offset: 0x00029D54
		[DefaultValue(false)]
		[Description("")]
		[Category("Behavior")]
		public bool AddRowid
		{
			get
			{
				return this.m_addRowid;
			}
			set
			{
				if (this.m_addRowid != value)
				{
					this.m_addRowid = value;
					if (this.m_addRowid)
					{
						this.m_localParse = true;
					}
					this.m_modified = true;
				}
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0002AD7C File Offset: 0x00029D7C
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x0002ADAB File Offset: 0x00029DAB
		protected override DbTransaction DbTransaction
		{
			get
			{
				if (OracleConnection.IsAvailable)
				{
					return null;
				}
				if (this.m_connection == null)
				{
					return null;
				}
				if (this.m_connection.m_oraTransaction == null)
				{
					return null;
				}
				return this.m_connection.m_oraTransaction;
			}
			set
			{
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0002ADAD File Offset: 0x00029DAD
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x0002ADDC File Offset: 0x00029DDC
		[Browsable(false)]
		public new OracleTransaction Transaction
		{
			get
			{
				if (OracleConnection.IsAvailable)
				{
					return null;
				}
				if (this.m_connection == null)
				{
					return null;
				}
				if (this.m_connection.m_oraTransaction == null)
				{
					return null;
				}
				return this.m_connection.m_oraTransaction;
			}
			set
			{
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0002ADDE File Offset: 0x00029DDE
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				if (this.m_parameters == null)
				{
					this.m_parameters = new OracleParameterCollection();
				}
				return this.m_parameters;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0002ADF9 File Offset: 0x00029DF9
		[Description("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Data")]
		public new OracleParameterCollection Parameters
		{
			get
			{
				if (this.m_parameters == null)
				{
					this.m_parameters = new OracleParameterCollection();
				}
				return this.m_parameters;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0002AE14 File Offset: 0x00029E14
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x0002AE1C File Offset: 0x00029E1C
		[Description("")]
		[Category("Data")]
		[DefaultValue(CommandType.Text)]
		public override CommandType CommandType
		{
			get
			{
				return this.m_commandType;
			}
			set
			{
				if (this.m_commandType != value)
				{
					if (value != CommandType.Text && value != CommandType.StoredProcedure && value != CommandType.TableDirect)
					{
						throw new ArgumentException();
					}
					this.m_commandType = value;
					this.m_parsed = false;
					this.m_addParam = true;
					this.m_modified = true;
					this.m_selectStmt = false;
					this.m_cmdTxtModified = true;
				}
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0002AE72 File Offset: 0x00029E72
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x0002AE7A File Offset: 0x00029E7A
		[Category("Data")]
		[Description("")]
		[DefaultValue(OracleXmlCommandType.None)]
		public OracleXmlCommandType XmlCommandType
		{
			get
			{
				return this.m_xmlCommandType;
			}
			set
			{
				if (this.m_xmlCommandType != value)
				{
					this.m_xmlCommandType = value;
					this.m_parsed = false;
					this.m_addParam = true;
					this.m_modified = true;
				}
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0002AEA1 File Offset: 0x00029EA1
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x0002AEBC File Offset: 0x00029EBC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public OracleXmlQueryProperties XmlQueryProperties
		{
			get
			{
				if (this.m_xmlQueryProperties == null)
				{
					this.m_xmlQueryProperties = new OracleXmlQueryProperties();
				}
				return this.m_xmlQueryProperties;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.m_xmlQueryProperties = (OracleXmlQueryProperties)value.Clone();
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0002AED8 File Offset: 0x00029ED8
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0002AEF3 File Offset: 0x00029EF3
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public OracleXmlSaveProperties XmlSaveProperties
		{
			get
			{
				if (this.m_xmlSaveProperties == null)
				{
					this.m_xmlSaveProperties = new OracleXmlSaveProperties();
				}
				return this.m_xmlSaveProperties;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.m_xmlSaveProperties = (OracleXmlSaveProperties)value.Clone();
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0002AF0F File Offset: 0x00029F0F
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x0002AF17 File Offset: 0x00029F17
		[Category("Behavior")]
		[DefaultValue(UpdateRowSource.Both)]
		[Description("")]
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				return this.m_updatedRowSource;
			}
			set
			{
				if (this.m_updatedRowSource != value)
				{
					if (value != UpdateRowSource.Both && value != UpdateRowSource.FirstReturnedRecord && value != UpdateRowSource.None && value != UpdateRowSource.OutputParameters)
					{
						throw new ArgumentException();
					}
					this.m_updatedRowSource = value;
					this.m_modified = true;
				}
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0002AF45 File Offset: 0x00029F45
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x0002AF50 File Offset: 0x00029F50
		[Browsable(false)]
		[DefaultValue(0)]
		public override int CommandTimeout
		{
			get
			{
				return this.m_commandTimeout;
			}
			set
			{
				if (value < 0 || value > 2147483647)
				{
					throw new ArgumentException();
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleCommand::CommandTimeout(): set\n"
					});
				}
				this.m_commandTimeout = value;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT) OracleCommand::CommandTimeout(): set\n"
					});
				}
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0002AFB0 File Offset: 0x00029FB0
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x0002AFB8 File Offset: 0x00029FB8
		[Description("")]
		[DefaultValue(131072L)]
		public long FetchSize
		{
			get
			{
				return this.m_fetchSize;
			}
			set
			{
				if (this.m_fetchSize != value)
				{
					if (value <= 0L)
					{
						throw new ArgumentException();
					}
					this.m_fetchSize = value;
					this.m_modified = true;
				}
				this.m_bFetchSizePropertySet = true;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0002AFE4 File Offset: 0x00029FE4
		[DefaultValue(0)]
		[Browsable(false)]
		public unsafe long RowSize
		{
			get
			{
				if (this.m_metaData != null)
				{
					if (this.m_addRowid && this.m_metaData.m_pOpoMetValCtxWRowid != null)
					{
						return (long)((ulong)this.m_metaData.m_pOpoMetValCtxWRowid->pColMetaVal[this.m_metaData.m_pOpoMetValCtxWRowid->NoOfCols - 1].Offset);
					}
					if (!this.m_addRowid && this.m_metaData.m_pOpoMetValCtx != null)
					{
						return (long)((ulong)this.m_metaData.m_pOpoMetValCtx->pColMetaVal[this.m_metaData.m_pOpoMetValCtx->NoOfCols - 1].Offset);
					}
				}
				return 0L;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0002B090 File Offset: 0x0002A090
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x0002B098 File Offset: 0x0002A098
		[Description("")]
		[DefaultValue(0)]
		public int InitialLONGFetchSize
		{
			get
			{
				return this.m_userLongFS;
			}
			set
			{
				if (this.m_initialLongFS != value)
				{
					if (value < -1)
					{
						throw new ArgumentException();
					}
					this.m_initialLongFS = value;
					if (this.m_initialLongFS > 32764)
					{
						this.m_initialLongFS = 32764;
					}
					this.m_userLongFS = this.m_initialLongFS;
					if (this.m_initialLongFS > 0)
					{
						this.m_initialLongFS = this.AlignedFS(this.m_initialLongFS);
					}
					if (this.m_metaData != null)
					{
						this.m_metaData = null;
					}
					this.m_modified = true;
				}
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0002B114 File Offset: 0x0002A114
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x0002B11C File Offset: 0x0002A11C
		[DefaultValue(0)]
		[Description("")]
		public int InitialLOBFetchSize
		{
			get
			{
				return this.m_userLobFS;
			}
			set
			{
				if (this.m_initialLobFS != value)
				{
					if (value < -1)
					{
						throw new ArgumentException();
					}
					this.m_initialLobFS = value;
					this.m_userLobFS = this.m_initialLobFS;
					if (this.m_initialLobFS > 0)
					{
						this.m_initialLobFS = this.AlignedFS(this.m_initialLobFS);
					}
					if (this.m_metaData != null)
					{
						this.m_metaData = null;
					}
					this.m_modified = true;
				}
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0002B180 File Offset: 0x0002A180
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x0002B188 File Offset: 0x0002A188
		[Description("")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool BindByName
		{
			get
			{
				return this.m_bindByName;
			}
			set
			{
				if (this.m_bindByName != value)
				{
					this.m_bindByName = value;
					this.m_modified = true;
				}
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0002B1A1 File Offset: 0x0002A1A1
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x0002B1A9 File Offset: 0x0002A1A9
		[Browsable(false)]
		[DefaultValue(0)]
		public int ArrayBindCount
		{
			get
			{
				return this.m_arrayBindCount;
			}
			set
			{
				if (this.m_arrayBindCount != value)
				{
					if (value < 0)
					{
						throw new ArgumentException();
					}
					this.m_arrayBindCount = value;
					this.m_modified = true;
				}
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0002B1CC File Offset: 0x0002A1CC
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x0002B1D4 File Offset: 0x0002A1D4
		[DefaultValue(null)]
		[Browsable(false)]
		public OracleNotificationRequest Notification
		{
			get
			{
				return this.m_NTFNReq;
			}
			set
			{
				this.m_NTFNReq = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0002B1DD File Offset: 0x0002A1DD
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x0002B1E5 File Offset: 0x0002A1E5
		[Browsable(false)]
		[DefaultValue(true)]
		public bool NotificationAutoEnlist
		{
			get
			{
				return this.m_NTFNAutoEnlist;
			}
			set
			{
				this.m_NTFNAutoEnlist = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0002B1EE File Offset: 0x0002A1EE
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x0002B1F6 File Offset: 0x0002A1F6
		[DesignOnly(true)]
		[Browsable(false)]
		[DefaultValue(true)]
		public override bool DesignTimeVisible
		{
			get
			{
				return this.m_designTimeVisible;
			}
			set
			{
				this.m_designTimeVisible = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0002B1FF File Offset: 0x0002A1FF
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x0002B207 File Offset: 0x0002A207
		internal PrimitiveType[] ExpectedColumnTypes
		{
			get
			{
				return this.m_expectedColumnTypes;
			}
			set
			{
				this.m_expectedColumnTypes = value;
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0002B210 File Offset: 0x0002A210
		public OracleCommand()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommand::OracleCommand(1)\n"
				});
			}
			this.Initialize();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleCommand::OracleCommand(1)\n"
				});
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0002B264 File Offset: 0x0002A264
		public OracleCommand(string cmdText)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommand::OracleCommand(2)\n"
				});
			}
			this.Initialize();
			this.m_commandText = cmdText;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleCommand::OracleCommand(2)\n"
				});
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0002B2C0 File Offset: 0x0002A2C0
		public OracleCommand(string cmdText, OracleConnection conn)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommand::OracleCommand(3)\n"
				});
			}
			this.Initialize();
			this.m_commandText = cmdText;
			if (conn != null)
			{
				this.m_connection = conn;
				this.m_conSignature = this.m_connection.m_conSignature;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleCommand::OracleCommand(3)\n"
				});
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0002B338 File Offset: 0x0002A338
		public object Clone()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommand::Clone()\n"
				});
			}
			OracleCommand oracleCommand = new OracleCommand();
			oracleCommand.m_connection = this.m_connection;
			oracleCommand.m_conSignature = this.m_conSignature;
			oracleCommand.m_updatedRowSource = this.m_updatedRowSource;
			oracleCommand.m_commandText = this.m_commandText;
			oracleCommand.m_pooledCmdText = this.m_pooledCmdText;
			oracleCommand.m_commandType = this.m_commandType;
			oracleCommand.m_addRowid = this.m_addRowid;
			oracleCommand.m_localParse = this.m_localParse;
			oracleCommand.m_rowsAffected = this.m_rowsAffected;
			oracleCommand.m_fetchSize = this.m_fetchSize;
			oracleCommand.m_initialLongFS = this.m_initialLongFS;
			oracleCommand.m_initialLobFS = this.m_initialLobFS;
			oracleCommand.m_userLongFS = this.m_userLongFS;
			oracleCommand.m_userLobFS = this.m_userLobFS;
			oracleCommand.m_bindByName = this.m_bindByName;
			oracleCommand.m_arrayBindCount = this.m_arrayBindCount;
			oracleCommand.m_parsed = this.m_parsed;
			oracleCommand.m_addParam = this.m_addParam;
			oracleCommand.m_safeMapping = this.m_safeMapping;
			oracleCommand.m_modified = this.m_modified;
			oracleCommand.m_selectStmt = this.m_selectStmt;
			oracleCommand.m_cmdTxtModified = this.m_cmdTxtModified;
			oracleCommand.CommandTimeout = this.m_commandTimeout;
			oracleCommand.m_bFetchSizePropertySet = this.m_bFetchSizePropertySet;
			if (this.m_expectedColumnTypes != null)
			{
				oracleCommand.m_expectedColumnTypes = this.m_expectedColumnTypes;
			}
			oracleCommand.m_isFromEF = this.m_isFromEF;
			if (this.m_parameters != null)
			{
				oracleCommand.m_parameters = new OracleParameterCollection();
				using (IEnumerator enumerator = this.m_parameters.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						OracleParameter oracleParameter = (OracleParameter)obj;
						oracleCommand.m_parameters.Add(oracleParameter.Clone());
					}
					goto IL_1C7;
				}
			}
			oracleCommand.m_parameters = null;
			IL_1C7:
			oracleCommand.m_cachedReader = this.m_cachedReader;
			if (this.m_xmlQueryProperties != null)
			{
				oracleCommand.m_xmlQueryProperties = (OracleXmlQueryProperties)this.m_xmlQueryProperties.Clone();
			}
			if (this.m_xmlSaveProperties != null)
			{
				oracleCommand.m_xmlSaveProperties = (OracleXmlSaveProperties)this.m_xmlSaveProperties.Clone();
			}
			oracleCommand.m_xmlCommandType = this.m_xmlCommandType;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleCommand::Clone()\n"
				});
			}
			return oracleCommand;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0002B594 File Offset: 0x0002A594
		public override void Prepare()
		{
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0002B598 File Offset: 0x0002A598
		internal unsafe MetaData InternalPrepare(bool openCon)
		{
			OpoMetValCtx* ptr = null;
			int num = 0;
			if (this.m_connection == null)
			{
				throw new InvalidOperationException();
			}
			this.CheckConStatus();
			if (this.m_cmdTxtModified)
			{
				if (this.m_commandText == null || this.m_commandText.Length == 0)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
					{
						"CommandText"
					}));
				}
				if (this.m_commandType == CommandType.Text)
				{
					this.m_selectStmt = OracleCommand.isSelectStatement(this.m_commandText);
					this.m_pooledCmdText = this.m_commandText;
				}
				else if (this.m_commandType == CommandType.TableDirect)
				{
					this.m_selectStmt = true;
					this.m_pooledCmdText = "Select * from " + this.m_commandText;
				}
				else
				{
					this.m_selectStmt = false;
					this.m_pooledCmdText = this.m_commandText;
				}
				this.m_cmdTxtModified = false;
			}
			if (!this.m_selectStmt)
			{
				return null;
			}
			int metaPool = this.m_connection.m_opoConCtx.metaPool;
			if (this.m_metaData != null && metaPool == 0)
			{
				this.m_metaData = null;
			}
			if (this.m_metaData != null && ((!this.m_addRowid && this.m_metaData.m_pOpoMetValCtx != null) || (this.m_addRowid && this.m_metaData.m_pOpoMetValCtxWRowid != null)))
			{
				return this.m_metaData;
			}
			if (metaPool == 1 && ((this.m_initialLongFS == 0 && this.m_initialLobFS == 0) || (this.m_isFromEF && this.m_initialLongFS <= 0 && this.m_initialLobFS <= 0)))
			{
				MetaData metaData = this.m_connection.m_opoConCtx.m_conPooler.Get(this.m_pooledCmdText) as MetaData;
				if (metaData != null && ((!this.m_addRowid && metaData.m_pOpoMetValCtx != null) || (this.m_addRowid && metaData.m_pOpoMetValCtxWRowid != null)))
				{
					this.m_metaData = metaData;
					return this.m_metaData;
				}
			}
			this.SetSqlValCtx(false);
			this.m_pOpoSqlValCtx->LocalParse = 1;
			try
			{
				this.m_pOpoSqlValCtx->pOpoPrmCtx = null;
				this.m_opsDacCtx = IntPtr.Zero;
				num = OpsSql.Prepare(this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_opsSqlCtx, ref this.m_opsDacCtx, ref this.m_pOpoSqlValCtx, this.m_pooledCmdText, ref ptr);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (!this.m_localParse)
				{
					this.m_pOpoSqlValCtx->LocalParse = 0;
				}
				if (num != 0)
				{
					string procedure;
					if (this.m_commandType == CommandType.StoredProcedure)
					{
						procedure = this.m_commandText;
					}
					else
					{
						procedure = string.Empty;
					}
					OracleException.HandleError(num, this.m_connection, procedure, this.m_opsErrCtx, this.m_pOpoSqlValCtx, this);
				}
			}
			if (ptr != null)
			{
				MetaData metaData = null;
				if (metaPool == 1 && ((this.m_initialLongFS == 0 && this.m_initialLobFS == 0) || (this.m_isFromEF && this.m_initialLongFS <= 0 && this.m_initialLobFS <= 0)))
				{
					metaData = (this.m_connection.m_opoConCtx.m_conPooler.Get(this.m_pooledCmdText) as MetaData);
				}
				if (metaData == null || (!this.m_addRowid && metaData.m_pOpoMetValCtx == null) || (this.m_addRowid && metaData.m_pOpoMetValCtxWRowid == null))
				{
					ptr->bPooled = 1;
					if (metaData == null)
					{
						if (this.m_metaData == null)
						{
							this.m_metaData = new MetaData();
						}
						this.m_metaData.m_addParam = this.m_addParam;
						this.m_metaData.m_parsed = this.m_parsed;
					}
					else
					{
						this.m_metaData = metaData;
					}
					if (!this.m_addRowid && this.m_metaData.m_pOpoMetValCtx == null)
					{
						this.m_metaData.m_pOpoMetValCtx = ptr;
					}
					else if (this.m_addRowid && this.m_metaData.m_pOpoMetValCtxWRowid == null)
					{
						this.m_metaData.m_pOpoMetValCtxWRowid = ptr;
					}
					if (metaPool == 1 && ((this.m_initialLongFS == 0 && this.m_initialLobFS == 0) || (this.m_isFromEF && this.m_initialLongFS <= 0 && this.m_initialLobFS <= 0)))
					{
						this.m_connection.m_opoConCtx.m_conPooler.Put(this.m_pooledCmdText, this.m_metaData);
					}
				}
				else if (this.m_metaData == null)
				{
					this.m_metaData = metaData;
					if (ptr->bPooled == 0)
					{
						OpsMet.FreeValCtx(ptr);
					}
				}
				return this.m_metaData;
			}
			return null;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0002B9C4 File Offset: 0x0002A9C4
		internal unsafe void GetPrimaryKey(MetaData metadata, bool openCon)
		{
			int num = 0;
			OpoMetValCtx* ptr = null;
			if (!this.m_addRowid)
			{
				ptr = metadata.m_pOpoMetValCtx;
			}
			else
			{
				ptr = metadata.m_pOpoMetValCtxWRowid;
			}
			if (this.m_cmdTxtModified)
			{
				if (this.m_commandText == null || this.m_commandText.Length == 0)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
					{
						"CommandText"
					}));
				}
				if (this.m_commandType == CommandType.Text)
				{
					this.m_selectStmt = OracleCommand.isSelectStatement(this.m_commandText);
					this.m_pooledCmdText = this.m_commandText;
				}
				else if (this.m_commandType == CommandType.TableDirect)
				{
					this.m_selectStmt = true;
					this.m_pooledCmdText = "Select * from " + this.m_commandText;
				}
				else
				{
					this.m_selectStmt = false;
					this.m_pooledCmdText = this.m_commandText;
				}
				this.m_cmdTxtModified = false;
			}
			if (!this.m_selectStmt)
			{
				return;
			}
			this.CheckConStatus();
			if (this.m_opsErrCtx == IntPtr.Zero)
			{
				try
				{
					OpsErr.AllocCtx(ref this.m_opsErrCtx, this.m_opsConCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
			}
			if (this.m_pOpoSqlValCtx == null)
			{
				try
				{
					OpsSql.AllocSqlValCtx(ref this.m_pOpoSqlValCtx);
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					throw;
				}
			}
			try
			{
				num = OpsMet.GetPrimaryKey(this.m_opsConCtx, this.m_opsErrCtx, ptr, 1, this.m_pOpoSqlValCtx->AddRowid, this.m_pOpoSqlValCtx->AddToStmtCache);
			}
			catch (Exception ex3)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex3);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			MetaData metaData = null;
			int metaPool = this.m_connection.m_opoConCtx.metaPool;
			if (metaPool == 1 && ((this.m_initialLongFS == 0 && this.m_initialLobFS == 0) || (this.m_isFromEF && this.m_initialLongFS <= 0 && this.m_initialLobFS <= 0)))
			{
				metaData = (this.m_connection.m_opoConCtx.m_conPooler.Get(this.m_pooledCmdText) as MetaData);
			}
			if (metaData == null || (!this.m_addRowid && (metaData.m_pOpoMetValCtx == null || metaData.m_pOpoMetValCtx->bPkFetched == 0)) || (this.m_addRowid && (metaData.m_pOpoMetValCtxWRowid == null || metaData.m_pOpoMetValCtxWRowid->bPkFetched == 0)))
			{
				ptr->bPooled = 1;
				if (metaData != null)
				{
					this.m_metaData = metaData;
				}
				if (this.m_metaData == null || (!this.m_addRowid && this.m_metaData.m_pOpoMetValCtx == null) || (this.m_addRowid && this.m_metaData.m_pOpoMetValCtxWRowid == null))
				{
					this.m_metaData = metadata;
				}
				if (metaPool == 1 && ((this.m_initialLongFS == 0 && this.m_initialLobFS == 0) || (this.m_isFromEF && this.m_initialLongFS <= 0 && this.m_initialLobFS <= 0)))
				{
					this.m_connection.m_opoConCtx.m_conPooler.Put(this.m_pooledCmdText, this.m_metaData);
					return;
				}
			}
			else if (this.m_metaData == null)
			{
				this.m_metaData = metaData;
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0002BCF4 File Offset: 0x0002ACF4
		public new OracleDataReader ExecuteReader()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommand::ExecuteReader()\n"
				});
			}
			OracleDataReader result = this.ExecuteReader(true, false, CommandBehavior.Default);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleCommand::ExecuteReader()\n"
				});
			}
			return result;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0002BD48 File Offset: 0x0002AD48
		internal unsafe OracleDataReader ExecuteReader(bool requery, bool fillRequest, CommandBehavior behavior)
		{
			IntPtr[] array = null;
			string[] array2 = null;
			IntPtr[] array3 = null;
			IntPtr opsSubscrCtx = IntPtr.Zero;
			int num = 0;
			OracleDependency oracleDependency = null;
			int num2 = 0;
			int bchgNTFNExcludeRowidInfo = 0;
			long num3 = 0L;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			OracleParameter oracleParameter = null;
			IntPtr zero = IntPtr.Zero;
			OpoDacValCtx* pOpoDacValCtx = null;
			bool flag = false;
			bool flag2 = false;
			CmdTimeoutCtx cmdTimeoutCtx = null;
			Timer timer = null;
			if (this.m_connection == null)
			{
				throw new InvalidOperationException();
			}
			if (this.m_cmdTxtModified && (this.m_commandText == null || this.m_commandText.Length == 0))
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"OracleCommand.CommandText"
				}));
			}
			if (EFOracleProviderServices.m_GetDbProviderManifestTokenWasCalled && !this.m_isFromEF)
			{
				EFOracleProviderServices.m_GetDbProviderManifestTokenWasCalled = false;
			}
			if (this.m_xmlCommandType != OracleXmlCommandType.None)
			{
				throw new InvalidOperationException();
			}
			if (!requery && this.m_cachedReader != null)
			{
				return this.m_cachedReader;
			}
			if (fillRequest && requery)
			{
				if (this.m_cachedReader != null)
				{
					if (!this.m_cachedReader.IsClosed)
					{
						this.m_cachedReader.Close();
					}
					this.m_cachedReader = null;
				}
				if (this.m_connection.m_state == ConnectionState.Closed)
				{
					this.m_connection.Open();
					behavior |= CommandBehavior.CloseConnection;
				}
			}
			this.CheckConStatus();
			int metaPool = this.m_connection.m_opoConCtx.metaPool;
			if (this.m_cmdTxtModified || this.m_commandType == CommandType.StoredProcedure)
			{
				if (this.m_commandType == CommandType.Text)
				{
					this.m_selectStmt = OracleCommand.isSelectStatement(this.m_commandText);
					this.m_pooledCmdText = this.m_commandText;
				}
				else if (this.m_commandType == CommandType.TableDirect)
				{
					this.m_selectStmt = true;
					this.m_pooledCmdText = "Select * from " + this.m_commandText;
				}
				else if (this.m_commandType == CommandType.StoredProcedure)
				{
					this.BuildCommandText();
					this.m_selectStmt = false;
					this.m_utf8CmdText = null;
					this.m_addParam = true;
				}
				if (this.m_metaData == null && this.m_selectStmt && metaPool == 1 && ((this.m_initialLongFS == 0 && this.m_initialLobFS == 0) || (this.m_isFromEF && this.m_initialLongFS <= 0 && this.m_initialLobFS <= 0)))
				{
					MetaData metaData = this.m_connection.m_opoConCtx.m_conPooler.Get(this.m_pooledCmdText) as MetaData;
					if (metaData != null)
					{
						this.m_metaData = metaData;
						flag2 = true;
					}
				}
				if (this.m_metaData != null)
				{
					this.m_addParam = this.m_metaData.m_addParam;
					this.m_parsed = this.m_metaData.m_parsed;
				}
				if (!this.m_parsed && this.m_commandType == CommandType.Text)
				{
					this.ParseCommandText();
				}
				this.m_cmdTxtModified = false;
			}
			if (this.m_NTFNReq != null && this.m_NTFNAutoEnlist && !this.m_connection.m_contextConnection && OracleNotificationRequest.s_idTable[this.m_NTFNReq.Id] != null)
			{
				opsSubscrCtx = OracleNotificationRequest.PopulateChgNTFNSubscrCtx(this, this.m_addRowid, out oracleDependency);
				if (oracleDependency != null && oracleDependency.m_bIsRegistered)
				{
					num = 1;
				}
				if (oracleDependency != null)
				{
					if (oracleDependency.m_OracleRowidInfo == OracleRowidInfo.Exclude)
					{
						bchgNTFNExcludeRowidInfo = 1;
					}
					if (oracleDependency.QueryBasedNotification && this.m_connection.IsDBVer11gR1OrHigher)
					{
						num2 = 1;
					}
				}
			}
			if (this.m_bindByName && this.m_commandType != CommandType.StoredProcedure)
			{
				flag = true;
			}
			if (this.m_metaData != null && metaPool == 0)
			{
				this.m_metaData = null;
			}
			OpoMetValCtx* ptr = null;
			if (this.m_metaData != null)
			{
				if (!this.m_addRowid)
				{
					ptr = this.m_metaData.m_pOpoMetValCtx;
				}
				else
				{
					ptr = this.m_metaData.m_pOpoMetValCtxWRowid;
				}
			}
			this.SetSqlValCtx(false);
			if ((behavior & CommandBehavior.SchemaOnly) == CommandBehavior.SchemaOnly && this.m_selectStmt)
			{
				this.m_pOpoSqlValCtx->mode = 16U;
			}
			if (this.m_executeScalar)
			{
				this.m_pOpoSqlValCtx->FetchSize = 1L;
			}
			this.m_opsDacCtx = IntPtr.Zero;
			if (this.m_addParam && this.m_parameters != null)
			{
				num5 = this.m_parameters.Count;
				if (num5 > 0 && (this.m_addToStmtCache || this.m_pOpoPrmCtx == null || this.m_pOpoPrmCtx->NumValCtxElems < num5))
				{
					IntPtr zero2 = IntPtr.Zero;
					try
					{
						bool flag3 = ptr != null && ptr->pNewCommandText != IntPtr.Zero;
						num4 = OpsSql.Prepare2(this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_opsSqlCtx, ref this.m_opsDacCtx, ref this.m_pOpoSqlValCtx, flag3 ? null : this.m_pooledCmdText, ref zero2, ref ptr, num5);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						num4 = ErrRes.INT_ERR;
						throw;
					}
					finally
					{
						this.m_executeScalar = false;
						if (zero2 != IntPtr.Zero)
						{
							try
							{
								Marshal.FreeCoTaskMem(zero2);
							}
							catch (Exception ex2)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex2);
								}
							}
						}
						if (num4 != 0)
						{
							if (!this.m_addToStmtCache && this.m_pOpoSqlValCtx->pOpoPrmCtx == null)
							{
								this.m_pOpoPrmCtx = null;
							}
							if (num4 != ErrRes.INT_ERR)
							{
								string procedure;
								if (this.m_commandType == CommandType.StoredProcedure)
								{
									procedure = this.m_commandText;
								}
								else
								{
									procedure = string.Empty;
								}
								OracleException.HandleError(num4, this.m_connection, procedure, this.m_opsErrCtx, this.m_pOpoSqlValCtx, this);
							}
						}
					}
					if (!this.m_addToStmtCache && this.m_pOpoPrmCtx == null)
					{
						this.m_pOpoPrmCtx = this.m_pOpoSqlValCtx->pOpoPrmCtx;
					}
				}
				if (flag)
				{
					array2 = new string[num5];
				}
				array3 = new IntPtr[num5];
				for (int i = 0; i < num5; i++)
				{
					oracleParameter = this.m_parameters[i];
					oracleParameter.m_pOpoPrmValCtx = this.m_pOpoSqlValCtx->pOpoPrmCtx->pOpoPrmValCtx + i;
					try
					{
						oracleParameter.PreBind(this.m_connection, this.m_opsErrCtx, this.m_arrayBindCount, this.m_isFromEF, this.m_selectStmt);
					}
					catch (Exception)
					{
						for (int j = 0; j < i; j++)
						{
							oracleParameter = this.m_parameters[j];
							oracleParameter.PreBindFree(this.m_connection, this.m_arrayBindCount);
						}
						this.FreeNonCachedOpoPrmCtx();
						throw;
					}
					if (flag)
					{
						array2[i] = oracleParameter.m_paramName;
					}
					array3[i] = (IntPtr)((void*)oracleParameter.m_pOpoPrmValCtx);
				}
			}
			try
			{
				if (this.m_commandTimeout > 0)
				{
					cmdTimeoutCtx = new CmdTimeoutCtx(this.m_opsConCtx, this.m_commandTimeout);
					TimerCallback callback = new TimerCallback(cmdTimeoutCtx.TimeoutNew);
					long num7 = (long)this.m_commandTimeout * 1000L;
					if (num7 > (long)((ulong)-147767296))
					{
						num7 = (long)((ulong)-147767296);
					}
					timer = new Timer(callback, cmdTimeoutCtx, num7, -1L);
					if (cmdTimeoutCtx.m_bDoneOCIBreak)
					{
						string procedure2;
						if (this.m_commandType == CommandType.StoredProcedure)
						{
							procedure2 = this.m_commandText;
						}
						else
						{
							procedure2 = string.Empty;
						}
						num4 = 1013;
						OracleException.HandleError(num4, this.m_connection, procedure2, this.m_opsErrCtx, this.m_pOpoSqlValCtx, this);
					}
				}
				num4 = 0;
				if (this.m_connection.m_opoConCtx.m_bSelfTuning && this.m_connection.m_opoConCtx.pOpoConValCtx->StmtCacheSize > OraTrace.MaxStatementCacheSize)
				{
					this.m_connection.m_opoConCtx.pOpoConValCtx->StmtCacheSize = OraTrace.MaxStatementCacheSize;
					num4 = OpsCon.SetStatementCacheSize(this.m_opsConCtx, ref this.m_opsErrCtx, this.m_connection.m_opoConCtx.pOpoConValCtx);
					if (this.m_connection.m_opoConCtx.m_conPooler != null)
					{
						this.m_connection.m_opoConCtx.m_conPooler.ModifyConPoolerSize(this.m_connection.m_opoConCtx.pOpoConValCtx->StmtCacheSize);
					}
				}
				if (num4 == 0)
				{
					if (ptr != null && ptr->pNewCommandText != IntPtr.Zero)
					{
						num4 = OpsSql.ExecuteReader(this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_opsSqlCtx, ref this.m_opsDacCtx, out zero, opsSubscrCtx, ref num, bchgNTFNExcludeRowidInfo, num2, ref num3, ref this.m_pOpoSqlValCtx, null, ref pOpoDacValCtx, array3, array2, ref ptr, num5);
					}
					else
					{
						num4 = OpsSql.ExecuteReader(this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_opsSqlCtx, ref this.m_opsDacCtx, out zero, opsSubscrCtx, ref num, bchgNTFNExcludeRowidInfo, num2, ref num3, ref this.m_pOpoSqlValCtx, this.m_pooledCmdText, ref pOpoDacValCtx, array3, array2, ref ptr, num5);
					}
				}
			}
			catch (Exception ex3)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex3);
				}
				num4 = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (this.m_commandTimeout > 0 && cmdTimeoutCtx != null)
				{
					cmdTimeoutCtx.m_bDoneExecution = true;
					if (!cmdTimeoutCtx.m_hWaitForOciBreakEvent.WaitOne(5000, false) && OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (WARN)  OracleCommand::ExecuteReader() WaitOne() timed out \n"
						});
					}
					timer.Dispose();
					cmdTimeoutCtx.Dispose();
				}
				this.m_executeScalar = false;
				if (oracleDependency != null && num == 1 && !this.m_connection.m_contextConnection)
				{
					oracleDependency.SetRegisterInfo(this.m_connection.m_opoConCtx.opoConRefCtx.userID, this.m_connection.DataSource, this.m_NTFNReq.IsNotifiedOnce, this.m_NTFNReq.IsPersistent, this.m_NTFNReq.Timeout);
				}
				if (this.m_connection.m_contextConnection && ptr != null && ptr->bHasUdtType == 1)
				{
					num4 = ErrRes.CLR_UDT_NOTSUPPORTED_CTX_CONN;
				}
				if (num4 != 0)
				{
					for (int i = 0; i < num5; i++)
					{
						oracleParameter = this.m_parameters[i];
						oracleParameter.PreBindFree(this.m_connection, this.m_arrayBindCount);
					}
					this.FreeNonCachedOpoPrmCtx();
					if (num4 != ErrRes.INT_ERR)
					{
						string procedure3;
						if (this.m_commandType == CommandType.StoredProcedure)
						{
							procedure3 = this.m_commandText;
						}
						else
						{
							procedure3 = string.Empty;
						}
						if (this.m_isFromEF && this.m_connection.m_majorVersion < 12 && this.m_commandText.Contains(" APPLY "))
						{
							Exception innerException = new Exception(OpoErrResManager.GetErrorMesg(ErrRes.ODP_NOT_SUPPORTED, new string[]
							{
								"Oracle " + this.m_connection.ServerVersion.ToString(),
								"APPLY"
							}));
							OracleException.HandleError(num4, this.m_connection, procedure3, this.m_opsErrCtx, this.m_pOpoSqlValCtx, this, true, innerException);
						}
						else
						{
							OracleException.HandleError(num4, this.m_connection, procedure3, this.m_opsErrCtx, this.m_pOpoSqlValCtx, this, true);
						}
					}
				}
			}
			if (oracleDependency != null && !this.m_connection.m_contextConnection)
			{
				oracleDependency.m_bIsEnabled = true;
				if (!oracleDependency.m_regList.Contains(this.m_commandText))
				{
					oracleDependency.m_regList.Add(this.m_commandText);
				}
				if (num2 == 1 && !oracleDependency.m_queryIDList.Contains(num3))
				{
					oracleDependency.m_queryIDList.Add(num3);
				}
			}
			if (ptr != null && this.m_selectStmt)
			{
				MetaData metaData = null;
				if (metaPool == 1 && !flag2 && ((this.m_initialLongFS == 0 && this.m_initialLobFS == 0) || (this.m_isFromEF && this.m_initialLongFS <= 0 && this.m_initialLobFS <= 0)))
				{
					metaData = (this.m_connection.m_opoConCtx.m_conPooler.Get(this.m_pooledCmdText) as MetaData);
				}
				if (metaData == null || (!this.m_addRowid && metaData.m_pOpoMetValCtx == null) || (this.m_addRowid && metaData.m_pOpoMetValCtxWRowid == null))
				{
					ptr->bPooled = 1;
					if (metaData == null)
					{
						if (this.m_metaData == null)
						{
							this.m_metaData = new MetaData();
						}
						this.m_metaData.m_addParam = this.m_addParam;
						this.m_metaData.m_parsed = this.m_parsed;
					}
					else
					{
						this.m_metaData = metaData;
					}
					if (!this.m_addRowid && this.m_metaData.m_pOpoMetValCtx == null)
					{
						this.m_metaData.m_pOpoMetValCtx = ptr;
					}
					else if (this.m_addRowid && this.m_metaData.m_pOpoMetValCtxWRowid == null)
					{
						this.m_metaData.m_pOpoMetValCtxWRowid = ptr;
					}
					if (metaPool == 1 && !flag2 && ((this.m_initialLongFS == 0 && this.m_initialLobFS == 0) || (this.m_isFromEF && this.m_initialLongFS <= 0 && this.m_initialLobFS <= 0)))
					{
						this.m_connection.m_opoConCtx.m_conPooler.Put(this.m_pooledCmdText, this.m_metaData);
					}
				}
				else if (this.m_metaData == null)
				{
					this.m_metaData = metaData;
					if (ptr->bPooled == 0)
					{
						OpsMet.FreeValCtx(ptr);
					}
				}
			}
			else if (!this.m_selectStmt && ptr != null && this.m_pOpoSqlValCtx->CommandType == 1)
			{
				if (this.m_metaData == null)
				{
					this.m_metaData = new MetaData();
				}
				this.m_metaData.m_addParam = this.m_addParam;
				this.m_metaData.m_parsed = this.m_parsed;
				if (!this.m_addRowid && this.m_metaData.m_pOpoMetValCtx == null)
				{
					this.m_metaData.m_pOpoMetValCtx = ptr;
				}
				else if (this.m_addRowid && this.m_metaData.m_pOpoMetValCtxWRowid == null)
				{
					this.m_metaData.m_pOpoMetValCtxWRowid = ptr;
				}
			}
			if (this.m_pOpoSqlValCtx->CommandType == 1)
			{
				this.m_rowsAffected = -1;
			}
			else if (this.m_pOpoSqlValCtx->CommandType == 4 || this.m_pOpoSqlValCtx->CommandType == 2 || this.m_pOpoSqlValCtx->CommandType == 3)
			{
				this.m_rowsAffected = this.m_pOpoSqlValCtx->RowsAffected;
			}
			else
			{
				this.m_rowsAffected = -1;
			}
			for (int i = 0; i < num5; i++)
			{
				oracleParameter = this.m_parameters[i];
				if (oracleParameter.m_bOracleDbTypeExSet)
				{
					oracleParameter.m_enumType = PrmEnumType.DBTYPE;
				}
				if (oracleParameter.m_oraDbType == OracleDbType.RefCursor)
				{
					oracleParameter.m_commandText = this.m_commandText;
					if (this.m_bindByName)
					{
						oracleParameter.m_paramPosOrName = oracleParameter.ParameterName;
					}
					else
					{
						oracleParameter.m_paramPosOrName = i.ToString();
					}
				}
				oracleParameter.m_pOpoPrmValCtx = (OpoPrmValCtx*)((void*)array3[i]);
				try
				{
					if (oracleParameter.m_direction == ParameterDirection.Input)
					{
						OracleDbType oraDbType = oracleParameter.m_oraDbType;
						if (oraDbType == OracleDbType.Varchar2)
						{
							oracleParameter.FreeDataBuffer();
						}
						else if (oraDbType == OracleDbType.Date)
						{
							oracleParameter.m_saveValue = null;
						}
						else if (oraDbType != OracleDbType.Decimal)
						{
							oracleParameter.PostBind(this.m_connection, this.m_pOpoSqlValCtx, this.m_arrayBindCount);
						}
					}
					else
					{
						oracleParameter.PostBind(this.m_connection, this.m_pOpoSqlValCtx, this.m_arrayBindCount);
					}
				}
				catch (Exception)
				{
					for (int j = i + 1; j < num5; j++)
					{
						oracleParameter = this.m_parameters[j];
						oracleParameter.PreBindFree(this.m_connection, this.m_arrayBindCount);
					}
					this.FreeNonCachedOpoPrmCtx();
					throw;
				}
				if (oracleParameter.OracleDbType == OracleDbType.RefCursor)
				{
					num6++;
				}
				if (oracleParameter.m_bOracleDbTypeExSet)
				{
					oracleParameter.m_enumType = PrmEnumType.ORADBTYPE;
				}
			}
			if (this.m_pOpoSqlValCtx->CommandType == 1)
			{
				num6 = 1;
				array = new IntPtr[num6];
				array[0] = this.m_opsSqlCtx;
				this.m_opsSqlCtx = IntPtr.Zero;
			}
			else if (num6 != 0)
			{
				if (this.m_pOpoSqlValCtx->pOpoPrmCtx != null && this.m_pOpoSqlValCtx->pOpoPrmCtx->bInStmtCache == 1)
				{
					this.m_pOpoSqlValCtx->pOpoPrmCtx = null;
				}
				array = new IntPtr[num6];
				int i = 0;
				int j = 0;
				while (i < num5)
				{
					oracleParameter = this.m_parameters[i];
					if (oracleParameter.OracleDbType == OracleDbType.RefCursor)
					{
						if (oracleParameter.Value == DBNull.Value)
						{
							array[j++] = IntPtr.Zero;
						}
						else
						{
							if (oracleParameter.m_bOracleDbTypeExSet)
							{
								array[j++] = ((OracleDataReader)oracleParameter.Value).SqlCtx;
								((OracleDataReader)oracleParameter.Value).SqlCtx = IntPtr.Zero;
								((OracleDataReader)oracleParameter.Value).Dispose();
							}
							else
							{
								array[j++] = ((OracleRefCursor)oracleParameter.Value).SqlCtx;
								((OracleRefCursor)oracleParameter.Value).SqlCtx = IntPtr.Zero;
								((OracleRefCursor)oracleParameter.Value).Dispose();
							}
							oracleParameter.Value = DBNull.Value;
						}
					}
					i++;
				}
			}
			if (!this.m_addToStmtCache)
			{
				this.m_pOpoSqlValCtx->pOpoPrmCtx = null;
			}
			OracleDataReader oracleDataReader = new OracleDataReader(this.m_connection, array, this.m_opsDacCtx, zero, this.m_pOpoSqlValCtx, pOpoDacValCtx, this.m_metaData, num6, behavior, this.m_safeMapping, this.m_pooledCmdText, 1, this.m_bFetchSizePropertySet);
			if (this.m_commandType == CommandType.StoredProcedure)
			{
				oracleDataReader.m_storedProcName = this.m_commandText;
			}
			this.m_safeMapping = null;
			this.m_pOpoSqlValCtx = null;
			if (this.m_isFromEF)
			{
				oracleDataReader.m_isFromEF = true;
				if (this.m_expectedColumnTypes != null)
				{
					oracleDataReader.m_expectedColumnTypes = this.m_expectedColumnTypes;
				}
			}
			if (!requery)
			{
				this.m_cachedReader = oracleDataReader;
			}
			return oracleDataReader;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0002CEAC File Offset: 0x0002BEAC
		public new OracleDataReader ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(true, false, behavior);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0002CEB8 File Offset: 0x0002BEB8
		public override object ExecuteScalar()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommand::ExecuteScalar()\n"
				});
			}
			object result = null;
			this.m_executeScalar = true;
			OracleDataReader oracleDataReader = this.ExecuteReader();
			this.m_executeScalar = false;
			if (!oracleDataReader.Read())
			{
				oracleDataReader.Dispose();
				return result;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleCommand::ExecuteScalar()\n"
				});
			}
			result = oracleDataReader.GetValue(0);
			oracleDataReader.Dispose();
			return result;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0002CF3C File Offset: 0x0002BF3C
		public unsafe override int ExecuteNonQuery()
		{
			string[] array = null;
			IntPtr[] array2 = null;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr opsSubscrCtx = IntPtr.Zero;
			int num = 0;
			OracleDependency oracleDependency = null;
			int num2 = 0;
			int bchgNTFNExcludeRowidInfo = 0;
			long num3 = 0L;
			int num4 = 0;
			int num5 = 0;
			bool flag = false;
			int bFromPool = 0;
			CmdTimeoutCtx cmdTimeoutCtx = null;
			Timer timer = null;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommand::ExecuteNonQuery()\n"
				});
			}
			if (this.m_connection == null)
			{
				throw new InvalidOperationException();
			}
			if (this.m_cmdTxtModified && (this.m_commandText == null || this.m_commandText.Length == 0))
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"OracleCommand.CommandText"
				}));
			}
			if (this.m_xmlCommandType == OracleXmlCommandType.None)
			{
				this.CheckConStatus();
				if (this.m_cmdTxtModified || this.m_commandType == CommandType.StoredProcedure)
				{
					if (this.m_commandType == CommandType.Text)
					{
						this.m_selectStmt = OracleCommand.isSelectStatement(this.m_commandText);
						this.m_pooledCmdText = this.m_commandText;
					}
					else if (this.m_commandType == CommandType.TableDirect)
					{
						this.m_selectStmt = true;
						this.m_pooledCmdText = "Select * from " + this.m_commandText;
					}
					else if (this.m_commandType == CommandType.StoredProcedure)
					{
						this.BuildCommandText();
						this.m_selectStmt = false;
						this.m_utf8CmdText = null;
						this.m_addParam = true;
					}
					UTF8CommandText utf8CommandText = UTF8CommandText.m_pooler.Get(this.m_connection.m_internalConStr, this.m_pooledCmdText) as UTF8CommandText;
					if (utf8CommandText != null && utf8CommandText.m_utf8CmdText != IntPtr.Zero)
					{
						this.m_utf8CmdText = utf8CommandText;
						this.m_addParam = this.m_utf8CmdText.m_addParam;
						this.m_parsed = this.m_utf8CmdText.m_parsed;
						bFromPool = 1;
					}
					if (!this.m_parsed && this.m_commandType == CommandType.Text)
					{
						this.ParseCommandText();
					}
					this.m_cmdTxtModified = false;
				}
				if (this.m_bindByName && this.m_commandType != CommandType.StoredProcedure)
				{
					flag = true;
				}
				if (this.m_NTFNReq != null && this.m_NTFNAutoEnlist && !this.m_connection.m_contextConnection && OracleNotificationRequest.s_idTable[this.m_NTFNReq.Id] != null)
				{
					opsSubscrCtx = OracleNotificationRequest.PopulateChgNTFNSubscrCtx(this, this.m_addRowid, out oracleDependency);
					if (oracleDependency != null && oracleDependency.m_bIsRegistered)
					{
						num = 1;
					}
					if (oracleDependency != null)
					{
						if (oracleDependency.m_OracleRowidInfo == OracleRowidInfo.Exclude)
						{
							bchgNTFNExcludeRowidInfo = 1;
						}
						if (oracleDependency.QueryBasedNotification && this.m_connection.IsDBVer11gR1OrHigher)
						{
							num2 = 1;
						}
					}
				}
				this.SetSqlValCtx(false);
				if (this.m_connection.m_opoConCtx.m_bSelfTuning && !OracleTuningAgent.bHighMemoryAlertFlag && 1 == this.m_pOpoSqlValCtx->AddToStmtCache)
				{
					this.m_connection.AcceptStatementData(this.m_pooledCmdText);
				}
				OpoMetValCtx* ptr = null;
				try
				{
					if (this.m_utf8CmdText != null)
					{
						intPtr = this.m_utf8CmdText.m_utf8CmdText;
						if (intPtr != IntPtr.Zero)
						{
							bFromPool = 1;
						}
					}
					if (this.m_parameters != null && this.m_addParam)
					{
						num5 = this.m_parameters.Count;
						if (num5 > 0)
						{
							if (!this.m_addToStmtCache && this.m_pOpoPrmCtx != null)
							{
								if (this.m_pOpoPrmCtx->NumValCtxElems >= num5)
								{
									goto IL_409;
								}
							}
							try
							{
								num4 = OpsSql.Prepare2(this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_opsSqlCtx, ref this.m_opsDacCtx, ref this.m_pOpoSqlValCtx, (intPtr == IntPtr.Zero) ? this.m_pooledCmdText : null, ref intPtr, ref ptr, num5);
							}
							catch (Exception ex)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex);
								}
								num4 = ErrRes.INT_ERR;
								throw;
							}
							finally
							{
								if (num4 != 0)
								{
									if (!this.m_addToStmtCache && this.m_pOpoSqlValCtx->pOpoPrmCtx == null)
									{
										this.m_pOpoPrmCtx = null;
									}
									if (num4 != ErrRes.INT_ERR)
									{
										string procedure;
										if (this.m_commandType == CommandType.StoredProcedure)
										{
											procedure = this.m_commandText;
										}
										else
										{
											procedure = string.Empty;
										}
										OracleException.HandleError(num4, this.m_connection, procedure, this.m_opsErrCtx, this.m_pOpoSqlValCtx, this);
									}
								}
							}
							if (!this.m_addToStmtCache && this.m_pOpoPrmCtx == null)
							{
								this.m_pOpoPrmCtx = this.m_pOpoSqlValCtx->pOpoPrmCtx;
							}
						}
						IL_409:
						if (flag)
						{
							array = new string[num5];
						}
						array2 = new IntPtr[num5];
						for (int i = 0; i < num5; i++)
						{
							OracleParameter oracleParameter = this.m_parameters[i];
							oracleParameter.m_pOpoPrmValCtx = this.m_pOpoSqlValCtx->pOpoPrmCtx->pOpoPrmValCtx + i;
							try
							{
								oracleParameter.PreBind(this.m_connection, this.m_opsErrCtx, this.m_arrayBindCount, this.m_isFromEF, this.m_selectStmt);
							}
							catch (Exception)
							{
								for (int j = 0; j < i; j++)
								{
									oracleParameter = this.m_parameters[j];
									oracleParameter.PreBindFree(this.m_connection, this.m_arrayBindCount);
								}
								throw;
							}
							if (flag)
							{
								array[i] = oracleParameter.m_paramName;
							}
							array2[i] = (IntPtr)((void*)oracleParameter.m_pOpoPrmValCtx);
						}
					}
					try
					{
						if (this.m_commandTimeout > 0)
						{
							cmdTimeoutCtx = new CmdTimeoutCtx(this.m_opsConCtx, this.m_commandTimeout);
							TimerCallback callback = new TimerCallback(cmdTimeoutCtx.TimeoutNew);
							long num6 = (long)this.m_commandTimeout * 1000L;
							if (num6 > (long)((ulong)-147767296))
							{
								num6 = (long)((ulong)-147767296);
							}
							timer = new Timer(callback, cmdTimeoutCtx, num6, -1L);
							if (cmdTimeoutCtx.m_bDoneOCIBreak)
							{
								string procedure2;
								if (this.m_commandType == CommandType.StoredProcedure)
								{
									procedure2 = this.m_commandText;
								}
								else
								{
									procedure2 = string.Empty;
								}
								num4 = 1013;
								OracleException.HandleError(num4, this.m_connection, procedure2, this.m_opsErrCtx, this.m_pOpoSqlValCtx, this);
							}
						}
						num4 = 0;
						if (this.m_connection.m_opoConCtx.m_bSelfTuning && this.m_connection.m_opoConCtx.pOpoConValCtx->StmtCacheSize > OraTrace.MaxStatementCacheSize)
						{
							this.m_connection.m_opoConCtx.pOpoConValCtx->StmtCacheSize = OraTrace.MaxStatementCacheSize;
							num4 = OpsCon.SetStatementCacheSize(this.m_opsConCtx, ref this.m_opsErrCtx, this.m_connection.m_opoConCtx.pOpoConValCtx);
							if (this.m_connection.m_opoConCtx.m_conPooler != null)
							{
								this.m_connection.m_opoConCtx.m_conPooler.ModifyConPoolerSize(this.m_connection.m_opoConCtx.pOpoConValCtx->StmtCacheSize);
							}
						}
						if (num4 == 0)
						{
							this.m_opsDacCtx = IntPtr.Zero;
							num4 = OpsSql.ExecuteNonQuery(this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_opsSqlCtx, ref this.m_opsDacCtx, opsSubscrCtx, ref num, bchgNTFNExcludeRowidInfo, num2, ref num3, ref this.m_pOpoSqlValCtx, (intPtr == IntPtr.Zero || this.m_selectStmt) ? this.m_pooledCmdText : null, ref intPtr, array2, array, ref ptr, num5, bFromPool);
						}
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
						num4 = ErrRes.INT_ERR;
						throw;
					}
				}
				finally
				{
					if (this.m_commandTimeout > 0 && cmdTimeoutCtx != null)
					{
						cmdTimeoutCtx.m_bDoneExecution = true;
						if (!cmdTimeoutCtx.m_hWaitForOciBreakEvent.WaitOne(5000, false) && OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.Trace(1U, new string[]
							{
								" (WARN)  OracleCommand::ExecuteNonQuery() WaitOne() timed out \n"
							});
						}
						timer.Dispose();
						cmdTimeoutCtx.Dispose();
					}
					if (oracleDependency != null && num == 1 && !this.m_connection.m_contextConnection)
					{
						oracleDependency.SetRegisterInfo(this.m_connection.m_opoConCtx.opoConRefCtx.userID, this.m_connection.DataSource, this.m_NTFNReq.IsNotifiedOnce, this.m_NTFNReq.IsPersistent, this.m_NTFNReq.Timeout);
					}
					if (this.m_connection.m_contextConnection && ptr != null && ptr->bHasUdtType == 1)
					{
						num4 = ErrRes.CLR_UDT_NOTSUPPORTED_CTX_CONN;
					}
					if (intPtr != IntPtr.Zero)
					{
						if (!(UTF8CommandText.m_pooler.Get(this.m_connection.m_internalConStr, this.m_pooledCmdText) is UTF8CommandText))
						{
							if (this.m_utf8CmdText == null)
							{
								this.m_utf8CmdText = new UTF8CommandText(intPtr);
							}
							this.m_utf8CmdText.m_parsed = this.m_parsed;
							this.m_utf8CmdText.m_addParam = this.m_addParam;
							UTF8CommandText.m_pooler.Put(this.m_connection.m_internalConStr, this.m_pooledCmdText, this.m_utf8CmdText);
						}
						else if (this.m_utf8CmdText == null)
						{
							this.m_utf8CmdText = new UTF8CommandText(intPtr);
						}
					}
					if (num4 != 0)
					{
						for (int i = 0; i < num5; i++)
						{
							OracleParameter oracleParameter = this.m_parameters[i];
							oracleParameter.PreBindFree(this.m_connection, this.m_arrayBindCount);
						}
						this.FreeNonCachedOpoPrmCtx();
						if (num4 != ErrRes.INT_ERR)
						{
							string procedure3;
							if (this.m_commandType == CommandType.StoredProcedure)
							{
								procedure3 = this.m_commandText;
							}
							else
							{
								procedure3 = string.Empty;
							}
							OracleException.HandleError(num4, this.m_connection, procedure3, this.m_opsErrCtx, this.m_pOpoSqlValCtx, this, true);
						}
					}
				}
				if (oracleDependency != null && !this.m_connection.m_contextConnection)
				{
					oracleDependency.m_bIsEnabled = true;
					if (!oracleDependency.m_regList.Contains(this.m_commandText))
					{
						oracleDependency.m_regList.Add(this.m_commandText);
					}
					if (num2 == 1 && !oracleDependency.m_queryIDList.Contains(num3))
					{
						oracleDependency.m_queryIDList.Add(num3);
					}
				}
				if (this.m_pOpoSqlValCtx->CommandType == 4 || this.m_pOpoSqlValCtx->CommandType == 2 || this.m_pOpoSqlValCtx->CommandType == 3)
				{
					this.m_rowsAffected = this.m_pOpoSqlValCtx->RowsAffected;
				}
				else
				{
					this.m_rowsAffected = -1;
				}
				for (int i = 0; i < num5; i++)
				{
					OracleParameter oracleParameter = this.m_parameters[i];
					if (oracleParameter.m_bOracleDbTypeExSet)
					{
						oracleParameter.m_enumType = PrmEnumType.DBTYPE;
					}
					if (oracleParameter.m_oraDbType == OracleDbType.RefCursor)
					{
						oracleParameter.m_commandText = this.m_commandText;
						if (this.m_bindByName)
						{
							oracleParameter.m_paramPosOrName = oracleParameter.ParameterName;
						}
						else
						{
							oracleParameter.m_paramPosOrName = i.ToString();
						}
					}
					oracleParameter.m_pOpoPrmValCtx = (OpoPrmValCtx*)((void*)array2[i]);
					try
					{
						if (oracleParameter.m_direction == ParameterDirection.Input)
						{
							OracleDbType oraDbType = oracleParameter.m_oraDbType;
							if (oraDbType == OracleDbType.Varchar2)
							{
								oracleParameter.FreeDataBuffer();
							}
							else if (oraDbType == OracleDbType.Date)
							{
								oracleParameter.m_saveValue = null;
							}
							else if (oraDbType != OracleDbType.Decimal)
							{
								oracleParameter.PostBind(this.m_connection, this.m_pOpoSqlValCtx, this.m_arrayBindCount);
							}
						}
						else
						{
							oracleParameter.PostBind(this.m_connection, this.m_pOpoSqlValCtx, this.m_arrayBindCount);
						}
					}
					catch (Exception)
					{
						for (int j = i + 1; j < num5; j++)
						{
							oracleParameter = this.m_parameters[j];
							oracleParameter.PreBindFree(this.m_connection, this.m_arrayBindCount);
						}
						this.FreeNonCachedOpoPrmCtx();
						throw;
					}
					if (oracleParameter.m_bOracleDbTypeExSet)
					{
						oracleParameter.m_enumType = PrmEnumType.ORADBTYPE;
					}
				}
				this.FreeNonCachedOpoPrmCtx();
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleCommand::ExecuteNonQuery()\n"
					});
				}
				return this.m_rowsAffected;
			}
			if (OracleXmlCommandType.Query == this.m_xmlCommandType)
			{
				this.ExecuteXmlQuery(false);
				return -1;
			}
			return this.ExecuteXmlSave();
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0002DACC File Offset: 0x0002CACC
		public XmlReader ExecuteXmlReader()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommand::ExecuteXmlReader()\n"
				});
			}
			if (this.m_connection == null)
			{
				throw new InvalidOperationException();
			}
			if (this.m_cmdTxtModified && (this.m_commandText == null || this.m_commandText.Length == 0))
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"OracleCommand.CommandText"
				}));
			}
			if (this.m_xmlCommandType == OracleXmlCommandType.None)
			{
				throw new InvalidOperationException();
			}
			XmlReader result;
			if (OracleXmlCommandType.Query == this.m_xmlCommandType)
			{
				OracleClob oracleClob = this.ExecuteXmlQuery(true);
				long length = oracleClob.Length;
				int num = 65536;
				if (length < 65536L)
				{
					num = (int)length;
				}
				num /= 2;
				StreamReader input = new StreamReader(oracleClob, Encoding.Unicode, false, num);
				result = new XmlTextReader(input);
			}
			else
			{
				this.ExecuteXmlSave();
				result = new XmlTextReader(new StringReader(string.Empty));
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleCommand::ExecuteXmlReader()\n"
				});
			}
			return result;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0002DBD8 File Offset: 0x0002CBD8
		public Stream ExecuteStream()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommand::ExecuteStream()\n"
				});
			}
			if (this.m_connection == null)
			{
				throw new InvalidOperationException();
			}
			if (this.m_cmdTxtModified && (this.m_commandText == null || this.m_commandText.Length == 0))
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"OracleCommand.CommandText"
				}));
			}
			if (this.m_xmlCommandType == OracleXmlCommandType.None)
			{
				throw new InvalidOperationException();
			}
			Stream result;
			if (OracleXmlCommandType.Query == this.m_xmlCommandType)
			{
				OracleClob oracleClob = this.ExecuteXmlQuery(true);
				result = oracleClob;
			}
			else
			{
				this.ExecuteXmlSave();
				result = new OracleClob(this.m_connection);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleCommand::ExecuteStream()\n"
				});
			}
			return result;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0002DCA8 File Offset: 0x0002CCA8
		public void ExecuteToStream(Stream outputStream)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommand::ExecuteToStream()\n"
				});
			}
			if (this.m_connection == null)
			{
				throw new InvalidOperationException();
			}
			if (this.m_cmdTxtModified && (this.m_commandText == null || this.m_commandText.Length == 0))
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"OracleCommand.CommandText"
				}));
			}
			if (this.m_xmlCommandType == OracleXmlCommandType.None)
			{
				throw new InvalidOperationException();
			}
			if (outputStream == null)
			{
				throw new ArgumentNullException();
			}
			if (!outputStream.CanWrite)
			{
				throw new ArgumentException();
			}
			if (OracleXmlCommandType.Query == this.m_xmlCommandType)
			{
				OracleClob oracleClob = this.ExecuteXmlQuery(true);
				long num = oracleClob.Length;
				string fullName = outputStream.GetType().FullName;
				if (fullName.Equals("Oracle.DataAccess.Types.OracleClob") && num % 2L == 0L)
				{
					OracleClob oracleClob2 = (OracleClob)outputStream;
					oracleClob.CopyTo(oracleClob2, oracleClob2.Position / 2L);
				}
				else
				{
					int num2;
					if (num < 65536L)
					{
						num2 = (int)num;
					}
					else
					{
						int num3 = 2 * oracleClob.OptimumChunkSize;
						num2 = num3 * (65536 / num3);
						if (num2 == 0)
						{
							num2 = num3;
						}
					}
					byte[] buffer = new byte[num2];
					while (num > 0L)
					{
						int num4;
						if (num < (long)num2)
						{
							num4 = (int)num;
							num = 0L;
						}
						else
						{
							num4 = num2;
							num -= (long)num2;
						}
						int num5 = oracleClob.Read(buffer, 0, num4);
						if (num5 != num4)
						{
							throw new IOException();
						}
						outputStream.Write(buffer, 0, num5);
					}
				}
				oracleClob.Close();
			}
			else
			{
				this.ExecuteXmlSave();
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleCommand::ExecuteToStream()\n"
				});
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0002DE54 File Offset: 0x0002CE54
		public new OracleParameter CreateParameter()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommand::CreateParameter()\n"
				});
			}
			OracleParameter result = new OracleParameter();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleCommand::CreateParameter()\n"
				});
			}
			return result;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0002DEA4 File Offset: 0x0002CEA4
		public override void Cancel()
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommand::Cancel()\n"
				});
			}
			if (this.m_connection == null)
			{
				throw new InvalidOperationException();
			}
			this.CheckConStatus();
			try
			{
				num = OpsSql.BreakExecution(this.m_opsConCtx, ref this.m_opsErrCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleCommand::Cancel()\n"
				});
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0002DF5C File Offset: 0x0002CF5C
		protected override void Dispose(bool disposing)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommand::Dispose()\n"
				});
			}
			if (!this.m_disposed)
			{
				try
				{
					this.FreeAllCtx();
					if (disposing)
					{
						try
						{
							if (this.m_cachedReader != null && !this.m_cachedReader.IsClosed)
							{
								try
								{
									this.m_cachedReader.Close();
								}
								catch
								{
								}
							}
							this.m_cachedReader = null;
							this.m_safeMapping = null;
						}
						catch
						{
						}
					}
					this.m_metaData = null;
					this.m_utf8CmdText = null;
					this.m_modified = true;
					this.m_disposed = true;
				}
				catch
				{
				}
				finally
				{
					try
					{
						base.Dispose(disposing);
					}
					catch
					{
					}
				}
				try
				{
					OpsCon.RelRef(ref this.m_opsConCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleCommand::Dispose()\n"
				});
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0002E088 File Offset: 0x0002D088
		private OracleParameter GetReturnValueParam()
		{
			if (this.m_commandType != CommandType.StoredProcedure)
			{
				return null;
			}
			for (int i = 0; i < this.m_parameters.Count; i++)
			{
				if (this.m_parameters[i].Direction == ParameterDirection.ReturnValue)
				{
					return this.m_parameters[i];
				}
			}
			return null;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0002E0D8 File Offset: 0x0002D0D8
		private void ParseCommandText()
		{
			int i = 0;
			int length = this.m_commandText.Length;
			while (i < length)
			{
				char c = this.m_commandText[i];
				if (c == '\'')
				{
					i++;
					while (i < length && this.m_commandText[i] != '\'')
					{
						i++;
					}
					if (i >= length)
					{
						break;
					}
					c = this.m_commandText[i];
				}
				if (c == '"')
				{
					i++;
					while (i < length && this.m_commandText[i] != '"')
					{
						i++;
					}
					if (i >= length)
					{
						break;
					}
					c = this.m_commandText[i];
				}
				int num = length - 1;
				if (i < num && c == '/' && this.m_commandText[i + 1] == '*')
				{
					for (i += 2; i < length; i++)
					{
						if (i >= num || this.m_commandText[i] == '*' || this.m_commandText[i + 1] == '/')
						{
							i += 2;
							break;
						}
					}
					if (i >= length)
					{
						break;
					}
					c = this.m_commandText[i];
				}
				if (c == ':')
				{
					i++;
					while (i < length && this.m_commandText[i] == ' ')
					{
						i++;
					}
					if (i >= length)
					{
						break;
					}
					c = this.m_commandText[i];
					if (i + 3 < length && this.m_commandText[i + 3] == '.' && (((c == 'N' || c == 'n') && (c == 'E' || c == 'e') && (c == 'W' || c == 'w')) || ((c == 'O' || c == 'o') && (c == 'L' || c == 'l') && (c == 'D' || c == 'd'))))
					{
						continue;
					}
					if (c != '=')
					{
						this.m_addParam = true;
						this.m_parsed = true;
						return;
					}
				}
				i++;
			}
			this.m_addParam = false;
			this.m_parsed = true;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0002E2A0 File Offset: 0x0002D2A0
		private unsafe string BuildCommandText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.m_pOpoSqlValCtx == null)
			{
				try
				{
					OpsSql.AllocSqlValCtx(ref this.m_pOpoSqlValCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
			}
			this.m_pOpoSqlValCtx->RetIdxForSP = -1;
			if (this.m_cmdTxtModified)
			{
				StoredProcedureInfo storedProcInfo = RegAndConfigRdr.GetStoredProcInfo(this.m_commandText);
				if (storedProcInfo != null && storedProcInfo.refCursors.Count > 0)
				{
					for (int i = 0; i < storedProcInfo.refCursors.Count; i++)
					{
						this.AddRefCursorParamToParamColl((RefCursorInfo)storedProcInfo.refCursors[i]);
					}
				}
			}
			if (this.m_parameters == null || this.m_parameters.Count == 0)
			{
				stringBuilder.Append("Begin " + this.m_commandText + "(); End;");
				this.m_pooledCmdText = stringBuilder.ToString();
			}
			else
			{
				int count = this.Parameters.Count;
				OracleParameter returnValueParam;
				if (!this.m_bindByName)
				{
					if ((returnValueParam = this.GetReturnValueParam()) == null)
					{
						stringBuilder.Append("Begin " + this.m_commandText + "(:v0");
						for (int j = 1; j < count; j++)
						{
							stringBuilder.Append(", :v" + j);
						}
						stringBuilder.Append("); End;");
						this.m_pooledCmdText = stringBuilder.ToString();
					}
					else
					{
						int j;
						if (this.m_parameters[0] == returnValueParam)
						{
							if (count > 1)
							{
								stringBuilder.Append("Begin :ret := " + this.m_commandText + "(:v1");
								j = 2;
							}
							else
							{
								stringBuilder.Append("Begin :ret := " + this.m_commandText + "(");
								j = 1;
							}
						}
						else
						{
							stringBuilder.Append("Begin :ret := " + this.m_commandText + "(:v0");
							j = 1;
						}
						while (j < count)
						{
							if (this.m_parameters[j] != returnValueParam)
							{
								stringBuilder.Append(", :v" + j);
							}
							j++;
						}
						stringBuilder.Append("); End;");
						this.m_pooledCmdText = stringBuilder.ToString();
					}
				}
				else if ((returnValueParam = this.GetReturnValueParam()) == null)
				{
					stringBuilder.Append(string.Concat(new string[]
					{
						"Begin ",
						this.m_commandText,
						"(",
						this.m_parameters[0].ParameterName,
						"=>:v0"
					}));
					for (int j = 1; j < count; j++)
					{
						stringBuilder.Append(string.Concat(new object[]
						{
							", ",
							this.m_parameters[j].ParameterName,
							"=>:v",
							j
						}));
					}
					stringBuilder.Append("); End;");
					this.m_pooledCmdText = stringBuilder.ToString();
				}
				else
				{
					int j;
					if (this.m_parameters[0] == returnValueParam)
					{
						if (count > 1)
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"Begin :ret := ",
								this.m_commandText,
								"(",
								this.m_parameters[1].ParameterName,
								"=>:v1"
							}));
							j = 2;
						}
						else
						{
							stringBuilder.Append("Begin :ret := " + this.m_commandText + "(");
							j = 1;
						}
					}
					else
					{
						this.m_pOpoSqlValCtx->RetIdxForSP = this.m_parameters.IndexOf(returnValueParam);
						stringBuilder.Append(string.Concat(new string[]
						{
							"Begin :ret := ",
							this.m_commandText,
							"(",
							this.m_parameters[0].ParameterName,
							"=>:v0"
						}));
						j = 1;
					}
					while (j < count)
					{
						if (this.m_parameters[j] != returnValueParam)
						{
							stringBuilder.Append(string.Concat(new object[]
							{
								", ",
								this.m_parameters[j].ParameterName,
								"=>:v",
								j
							}));
						}
						j++;
					}
					stringBuilder.Append("); End;");
					this.m_pooledCmdText = stringBuilder.ToString();
				}
			}
			return this.m_pooledCmdText;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0002E718 File Offset: 0x0002D718
		private void Initialize()
		{
			this.m_updatedRowSource = UpdateRowSource.Both;
			this.m_commandType = CommandType.Text;
			this.m_rowsAffected = -1;
			this.m_fetchSize = (long)OraTrace.m_FetchSize;
			this.m_addParam = true;
			this.m_cmdTxtModified = true;
			this.m_xmlCommandType = OracleXmlCommandType.None;
			this.m_addToStatementCache = true;
			this.m_NTFNAutoEnlist = true;
			this.m_designTimeVisible = true;
			this.m_expectedColumnTypes = null;
			this.m_isFromEF = false;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0002E780 File Offset: 0x0002D780
		private unsafe void FreeAllCtx()
		{
			bool flag = true;
			this.m_metaData = null;
			this.m_utf8CmdText = null;
			this.m_parameters = null;
			try
			{
				if (OracleConnection.IsAvailable && this.m_connection != null && this.m_connection.m_extProcEnv != null)
				{
					Monitor.Enter(this.m_connection.m_extProcEnv);
					flag = this.m_connection.m_extProcEnv.m_status;
				}
				if (this.m_opsSqlCtx != IntPtr.Zero)
				{
					try
					{
						if (flag)
						{
							if (!this.m_addToStmtCache)
							{
								OpsSql.FreeCtx(ref this.m_opsSqlCtx, this.m_opsErrCtx, 0);
							}
							else
							{
								OpsSql.FreeCtx(ref this.m_opsSqlCtx, this.m_opsErrCtx, 1);
							}
						}
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
					}
					this.m_opsSqlCtx = IntPtr.Zero;
				}
				if (this.m_pOpoSqlValCtx != null)
				{
					if (this.m_pOpoPrmCtx != null && this.m_pOpoPrmCtx == this.m_pOpoSqlValCtx->pOpoPrmCtx)
					{
						this.m_pOpoPrmCtx = null;
					}
					else
					{
						this.m_pOpoSqlValCtx->pOpoPrmCtx = null;
					}
					try
					{
						if (flag)
						{
							OpsSql.FreeValCtx(this.m_pOpoSqlValCtx, 1);
						}
						else
						{
							OpsSql.FreeValCtx(this.m_pOpoSqlValCtx, 0);
						}
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
					}
					this.m_pOpoSqlValCtx = null;
				}
				if (this.m_pOpoPrmCtx != null)
				{
					try
					{
						OpsPrm.FreeOpoPrmCtx(this.m_pOpoPrmCtx);
					}
					catch (Exception ex3)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex3);
						}
					}
					this.m_pOpoPrmCtx = null;
				}
				if (this.m_opsErrCtx != IntPtr.Zero)
				{
					try
					{
						if (flag)
						{
							OpsErr.FreeCtx(ref this.m_opsErrCtx);
						}
					}
					catch (Exception ex4)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex4);
						}
					}
					this.m_opsErrCtx = IntPtr.Zero;
				}
			}
			finally
			{
				if (OracleConnection.IsAvailable && this.m_connection != null && this.m_connection.m_extProcEnv != null)
				{
					Monitor.Exit(this.m_connection.m_extProcEnv);
				}
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0002E9D8 File Offset: 0x0002D9D8
		private unsafe void FreeNonCachedOpoPrmCtx()
		{
			if (this.m_addToStmtCache && this.m_pOpoSqlValCtx->pOpoPrmCtx != null && this.m_pOpoSqlValCtx->pOpoPrmCtx->bInStmtCache == 0)
			{
				try
				{
					OpsPrm.FreeOpoPrmCtx(this.m_pOpoSqlValCtx->pOpoPrmCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
				}
				this.m_pOpoSqlValCtx->pOpoPrmCtx = null;
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0002EA50 File Offset: 0x0002DA50
		private unsafe OracleClob ExecuteXmlQuery(bool wantResult)
		{
			bool flag = false;
			string[] array = null;
			IntPtr[] array2 = null;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr opsSubscrCtx = IntPtr.Zero;
			int num = 0;
			OracleDependency oracleDependency = null;
			int num2 = 0;
			int bchgNTFNExcludeRowidInfo = 0;
			long num3 = 0L;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			OracleParameter oracleParameter = null;
			OracleParameter oracleParameter2 = null;
			OracleClob oracleClob = null;
			OracleParameter oracleParameter3 = null;
			OracleClob oracleClob2 = null;
			bool flag2 = false;
			int bFromPool = 0;
			CmdTimeoutCtx cmdTimeoutCtx = null;
			Timer timer = null;
			this.CheckConStatus();
			int majorVersion = this.m_connection.m_majorVersion;
			int minorVersion = this.m_connection.m_minorVersion;
			if ((majorVersion == 8 && minorVersion == 1) || (majorVersion == 9 && minorVersion == 0))
			{
				flag = true;
			}
			if (this.m_xmlQueryProperties == null)
			{
				this.m_xmlQueryProperties = new OracleXmlQueryProperties();
			}
			if (this.m_xmlQueryProperties.Xslt != null && this.m_xmlQueryProperties.Xslt.Length != 0)
			{
				flag2 = true;
			}
			string text = ":OracleResult$";
			string parameterName = ":OracleXslDoc$";
			string parameterName2 = ":OracleSqlQuery$";
			if (this.m_cmdTxtModified && !this.m_parsed && this.m_commandType == CommandType.Text)
			{
				this.ParseCommandText();
			}
			if (this.m_NTFNReq != null && this.m_NTFNAutoEnlist && !this.m_connection.m_contextConnection && OracleNotificationRequest.s_idTable[this.m_NTFNReq.Id] != null)
			{
				opsSubscrCtx = OracleNotificationRequest.PopulateChgNTFNSubscrCtx(this, this.m_addRowid, out oracleDependency);
				if (oracleDependency != null && oracleDependency.m_bIsRegistered)
				{
					num = 1;
				}
				if (oracleDependency != null)
				{
					if (oracleDependency.m_OracleRowidInfo == OracleRowidInfo.Exclude)
					{
						bchgNTFNExcludeRowidInfo = 1;
					}
					if (oracleDependency.QueryBasedNotification && this.m_connection.IsDBVer11gR1OrHigher)
					{
						num2 = 1;
					}
				}
			}
			if (this.m_parameters != null && this.m_addParam)
			{
				num6 = this.m_parameters.Count;
				if (num6 > 0 && !this.m_bindByName)
				{
					throw new InvalidOperationException();
				}
			}
			num7 = num6;
			if (wantResult)
			{
				num7++;
			}
			if (flag2)
			{
				num7++;
			}
			if (flag)
			{
				num7++;
			}
			this.BuildXmlQueryCommandText(wantResult, text);
			this.m_utf8CmdText = null;
			UTF8CommandText utf8CommandText = UTF8CommandText.m_pooler.Get(this.m_connection.m_internalConStr, this.m_pooledCmdText) as UTF8CommandText;
			if (utf8CommandText != null && utf8CommandText.m_utf8CmdText != IntPtr.Zero)
			{
				this.m_utf8CmdText = utf8CommandText;
				this.m_addParam = this.m_utf8CmdText.m_addParam;
				this.m_parsed = this.m_utf8CmdText.m_parsed;
				bFromPool = 1;
			}
			OpoMetValCtx* ptr = null;
			this.m_selectStmt = false;
			this.SetSqlValCtx(true);
			try
			{
				if (num7 > 0)
				{
					if (!this.m_addToStmtCache && this.m_pOpoPrmCtx != null)
					{
						if (this.m_pOpoPrmCtx->NumValCtxElems >= num7)
						{
							goto IL_34D;
						}
					}
					try
					{
						if (this.m_utf8CmdText != null)
						{
							intPtr = this.m_utf8CmdText.m_utf8CmdText;
						}
						num5 = OpsSql.Prepare2(this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_opsSqlCtx, ref this.m_opsDacCtx, ref this.m_pOpoSqlValCtx, (intPtr == IntPtr.Zero) ? this.m_pooledCmdText : null, ref intPtr, ref ptr, num7);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						num5 = ErrRes.INT_ERR;
						throw;
					}
					finally
					{
						if (num5 != 0)
						{
							if (!this.m_addToStmtCache && this.m_pOpoSqlValCtx->pOpoPrmCtx == null)
							{
								this.m_pOpoPrmCtx = null;
							}
							if (num5 != ErrRes.INT_ERR)
							{
								OracleException.HandleError(num5, this.m_connection, string.Empty, this.m_opsErrCtx, this.m_pOpoSqlValCtx, this);
							}
						}
					}
					if (!this.m_addToStmtCache && this.m_pOpoPrmCtx == null)
					{
						this.m_pOpoPrmCtx = this.m_pOpoSqlValCtx->pOpoPrmCtx;
					}
				}
				IL_34D:
				array = new string[num7];
				array2 = new IntPtr[num7];
				for (int i = 0; i < num6; i++)
				{
					OracleParameter oracleParameter4 = this.m_parameters[i];
					oracleParameter4.m_pOpoPrmValCtx = this.m_pOpoSqlValCtx->pOpoPrmCtx->pOpoPrmValCtx + i;
					try
					{
						oracleParameter4.PreBind(this.m_connection, this.m_opsErrCtx, 0);
					}
					catch (Exception)
					{
						for (int j = 0; j < i; j++)
						{
							oracleParameter4 = this.m_parameters[j];
							oracleParameter4.PreBindFree(this.m_connection, 0);
						}
						this.FreeNonCachedOpoPrmCtx();
						throw;
					}
					array[i] = oracleParameter4.m_paramName;
					array2[i] = (IntPtr)((void*)oracleParameter4.m_pOpoPrmValCtx);
				}
				num4 = num6;
				if (wantResult)
				{
					oracleParameter = new OracleParameter(text, OracleDbType.Clob);
					oracleParameter.Direction = ParameterDirection.Output;
					oracleParameter.m_pOpoPrmValCtx = this.m_pOpoSqlValCtx->pOpoPrmCtx->pOpoPrmValCtx + num4;
					try
					{
						oracleParameter.PreBind(this.m_connection, this.m_opsErrCtx, 0);
					}
					catch (Exception)
					{
						for (int j = 0; j < num6; j++)
						{
							OracleParameter oracleParameter4 = this.m_parameters[j];
							oracleParameter4.PreBindFree(this.m_connection, 0);
						}
						this.FreeNonCachedOpoPrmCtx();
						throw;
					}
					array[num4] = oracleParameter.m_paramName;
					array2[num4] = (IntPtr)((void*)oracleParameter.m_pOpoPrmValCtx);
					num4++;
				}
				if (flag2)
				{
					if (this.m_xmlQueryProperties.Xslt.Length > 32512 || flag)
					{
						oracleParameter2 = new OracleParameter(parameterName, OracleDbType.Clob);
						oracleParameter2.Direction = ParameterDirection.Input;
						oracleClob = new OracleClob(this.m_connection);
						oracleClob.Append(this.m_xmlQueryProperties.Xslt.ToCharArray(), 0, this.m_xmlQueryProperties.Xslt.Length);
						oracleParameter2.Value = oracleClob;
					}
					else
					{
						oracleParameter2 = new OracleParameter(parameterName, OracleDbType.Varchar2);
						oracleParameter2.Direction = ParameterDirection.Input;
						oracleParameter2.Value = this.m_xmlQueryProperties.Xslt;
					}
					oracleParameter2.m_pOpoPrmValCtx = this.m_pOpoSqlValCtx->pOpoPrmCtx->pOpoPrmValCtx + num4;
					try
					{
						oracleParameter2.PreBind(this.m_connection, this.m_opsErrCtx, 0);
					}
					catch (Exception)
					{
						for (int j = 0; j < num6; j++)
						{
							OracleParameter oracleParameter4 = this.m_parameters[j];
							oracleParameter4.PreBindFree(this.m_connection, 0);
						}
						if (wantResult)
						{
							oracleParameter.PreBindFree(this.m_connection, 0);
						}
						if (oracleClob != null)
						{
							oracleClob.Close();
						}
						this.FreeNonCachedOpoPrmCtx();
						throw;
					}
					array[num4] = oracleParameter2.m_paramName;
					array2[num4] = (IntPtr)((void*)oracleParameter2.m_pOpoPrmValCtx);
					num4++;
				}
				if (flag)
				{
					if (this.m_commandText.Length > 32512)
					{
						oracleParameter3 = new OracleParameter(parameterName2, OracleDbType.Clob);
						oracleParameter3.Direction = ParameterDirection.Input;
						oracleClob2 = new OracleClob(this.m_connection);
						oracleClob2.Append(this.m_commandText.ToCharArray(), 0, this.m_commandText.Length);
						oracleParameter3.Value = oracleClob2;
					}
					else
					{
						oracleParameter3 = new OracleParameter(parameterName2, OracleDbType.Varchar2);
						oracleParameter3.Direction = ParameterDirection.Input;
						oracleParameter3.Value = this.m_commandText;
					}
					oracleParameter3.m_pOpoPrmValCtx = this.m_pOpoSqlValCtx->pOpoPrmCtx->pOpoPrmValCtx + num4;
					try
					{
						oracleParameter3.PreBind(this.m_connection, this.m_opsErrCtx, 0);
					}
					catch (Exception)
					{
						for (int j = 0; j < num6; j++)
						{
							OracleParameter oracleParameter4 = this.m_parameters[j];
							oracleParameter4.PreBindFree(this.m_connection, 0);
						}
						if (wantResult)
						{
							oracleParameter.PreBindFree(this.m_connection, 0);
						}
						if (flag2)
						{
							oracleParameter2.PreBindFree(this.m_connection, 0);
						}
						if (oracleClob != null)
						{
							oracleClob.Close();
						}
						if (oracleClob2 != null)
						{
							oracleClob2.Close();
						}
						this.FreeNonCachedOpoPrmCtx();
						throw;
					}
					array[num4] = oracleParameter3.m_paramName;
					array2[num4] = (IntPtr)((void*)oracleParameter3.m_pOpoPrmValCtx);
					num4++;
				}
				try
				{
					if (this.m_utf8CmdText != null)
					{
						intPtr = this.m_utf8CmdText.m_utf8CmdText;
					}
					if (this.m_commandTimeout > 0)
					{
						cmdTimeoutCtx = new CmdTimeoutCtx(this.m_opsConCtx, this.m_commandTimeout);
						TimerCallback callback = new TimerCallback(cmdTimeoutCtx.TimeoutNew);
						long num8 = (long)this.m_commandTimeout * 1000L;
						if (num8 > (long)((ulong)-147767296))
						{
							num8 = (long)((ulong)-147767296);
						}
						timer = new Timer(callback, cmdTimeoutCtx, num8, -1L);
						if (cmdTimeoutCtx.m_bDoneOCIBreak)
						{
							string procedure;
							if (this.m_commandType == CommandType.StoredProcedure)
							{
								procedure = this.m_commandText;
							}
							else
							{
								procedure = string.Empty;
							}
							num5 = 1013;
							OracleException.HandleError(num5, this.m_connection, procedure, this.m_opsErrCtx, this.m_pOpoSqlValCtx, this);
						}
					}
					num5 = 0;
					if (this.m_connection.m_opoConCtx.m_bSelfTuning && this.m_connection.m_opoConCtx.pOpoConValCtx->StmtCacheSize > OraTrace.MaxStatementCacheSize)
					{
						this.m_connection.m_opoConCtx.pOpoConValCtx->StmtCacheSize = OraTrace.MaxStatementCacheSize;
						num5 = OpsCon.SetStatementCacheSize(this.m_opsConCtx, ref this.m_opsErrCtx, this.m_connection.m_opoConCtx.pOpoConValCtx);
						if (this.m_connection.m_opoConCtx.m_conPooler != null)
						{
							this.m_connection.m_opoConCtx.m_conPooler.ModifyConPoolerSize(this.m_connection.m_opoConCtx.pOpoConValCtx->StmtCacheSize);
						}
					}
					if (num5 == 0)
					{
						this.m_opsDacCtx = IntPtr.Zero;
						num5 = OpsSql.ExecuteNonQuery(this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_opsSqlCtx, ref this.m_opsDacCtx, opsSubscrCtx, ref num, bchgNTFNExcludeRowidInfo, num2, ref num3, ref this.m_pOpoSqlValCtx, (intPtr == IntPtr.Zero || this.m_selectStmt) ? this.m_pooledCmdText : null, ref intPtr, array2, array, ref ptr, num7, bFromPool);
					}
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					num5 = ErrRes.INT_ERR;
					throw;
				}
				finally
				{
					if (this.m_commandTimeout > 0 && cmdTimeoutCtx != null)
					{
						cmdTimeoutCtx.m_bDoneExecution = true;
						if (!cmdTimeoutCtx.m_hWaitForOciBreakEvent.WaitOne(5000, false) && OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.Trace(1U, new string[]
							{
								" (WARN)  OracleCommand::ExecuteXmlQuery() WaitOne() timed out \n"
							});
						}
						timer.Dispose();
						cmdTimeoutCtx.Dispose();
					}
					if (oracleDependency != null && num == 1 && !this.m_connection.m_contextConnection)
					{
						oracleDependency.SetRegisterInfo(this.m_connection.m_opoConCtx.opoConRefCtx.userID, this.m_connection.DataSource, this.m_NTFNReq.IsNotifiedOnce, this.m_NTFNReq.IsPersistent, this.m_NTFNReq.Timeout);
					}
					if (num5 != 0)
					{
						for (int i = 0; i < num6; i++)
						{
							OracleParameter oracleParameter4 = this.m_parameters[i];
							oracleParameter4.PreBindFree(this.m_connection, 0);
						}
						if (wantResult)
						{
							oracleParameter.PreBindFree(this.m_connection, 0);
						}
						if (flag2)
						{
							oracleParameter2.PreBindFree(this.m_connection, 0);
							if (oracleClob != null)
							{
								oracleClob.Close();
							}
						}
						if (flag)
						{
							oracleParameter3.PreBindFree(this.m_connection, 0);
							if (oracleClob2 != null)
							{
								oracleClob2.Close();
							}
						}
						this.FreeNonCachedOpoPrmCtx();
						if (num5 != ErrRes.INT_ERR)
						{
							OracleException.HandleError(num5, this.m_connection, string.Empty, this.m_opsErrCtx, this.m_pOpoSqlValCtx, this, true);
						}
					}
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					utf8CommandText = (UTF8CommandText.m_pooler.Get(this.m_connection.m_internalConStr, this.m_pooledCmdText) as UTF8CommandText);
					if (utf8CommandText == null)
					{
						if (utf8CommandText == null && this.m_utf8CmdText == null)
						{
							this.m_utf8CmdText = new UTF8CommandText(intPtr);
						}
						this.m_utf8CmdText.m_parsed = this.m_parsed;
						this.m_utf8CmdText.m_addParam = this.m_addParam;
						UTF8CommandText.m_pooler.Put(this.m_connection.m_internalConStr, this.m_pooledCmdText, this.m_utf8CmdText);
					}
					else if (this.m_utf8CmdText == null)
					{
						this.m_utf8CmdText = new UTF8CommandText(intPtr);
					}
				}
			}
			this.m_rowsAffected = -1;
			if (oracleDependency != null && !this.m_connection.m_contextConnection)
			{
				oracleDependency.m_bIsEnabled = true;
				if (!oracleDependency.m_regList.Contains(this.m_commandText))
				{
					oracleDependency.m_regList.Add(this.m_commandText);
				}
				if (num2 == 1 && !oracleDependency.m_queryIDList.Contains(num3))
				{
					oracleDependency.m_queryIDList.Add(num3);
				}
			}
			for (int i = 0; i < num6; i++)
			{
				OracleParameter oracleParameter4 = this.m_parameters[i];
				if (oracleParameter4.m_bOracleDbTypeExSet)
				{
					oracleParameter4.m_enumType = PrmEnumType.DBTYPE;
				}
				if (oracleParameter4.m_oraDbType == OracleDbType.RefCursor)
				{
					oracleParameter4.m_commandText = this.m_commandText;
					if (this.m_bindByName)
					{
						oracleParameter4.m_paramPosOrName = oracleParameter4.ParameterName;
					}
					else
					{
						oracleParameter4.m_paramPosOrName = i.ToString();
					}
				}
				oracleParameter4.m_pOpoPrmValCtx = (OpoPrmValCtx*)((void*)array2[i]);
				try
				{
					oracleParameter4.PostBind(this.m_connection, this.m_pOpoSqlValCtx, 0);
				}
				catch (Exception)
				{
					for (int j = i + 1; j < num6; j++)
					{
						oracleParameter4 = this.m_parameters[j];
						oracleParameter4.PreBindFree(this.m_connection, 0);
					}
					if (wantResult)
					{
						oracleParameter.PreBindFree(this.m_connection, 0);
					}
					if (flag2)
					{
						oracleParameter2.PreBindFree(this.m_connection, 0);
						if (oracleClob != null)
						{
							oracleClob.Close();
						}
					}
					if (flag)
					{
						oracleParameter3.PreBindFree(this.m_connection, 0);
						if (oracleClob2 != null)
						{
							oracleClob2.Close();
						}
					}
					this.FreeNonCachedOpoPrmCtx();
					throw;
				}
				if (oracleParameter4.m_bOracleDbTypeExSet)
				{
					oracleParameter4.m_enumType = PrmEnumType.ORADBTYPE;
				}
				if (oracleParameter4.m_oraDbType == OracleDbType.RefCursor)
				{
					oracleParameter4.m_commandText = this.m_commandText;
					if (this.m_bindByName)
					{
						oracleParameter4.m_paramPosOrName = oracleParameter4.ParameterName;
					}
					else
					{
						oracleParameter4.m_paramPosOrName = i.ToString();
					}
				}
			}
			num4 = num6;
			if (wantResult)
			{
				oracleParameter.m_pOpoPrmValCtx = (OpoPrmValCtx*)((void*)array2[num4]);
				try
				{
					oracleParameter.PostBind(this.m_connection, this.m_pOpoSqlValCtx, 0);
				}
				catch (Exception)
				{
					if (flag2)
					{
						oracleParameter2.PreBindFree(this.m_connection, 0);
						if (oracleClob != null)
						{
							oracleClob.Close();
						}
					}
					if (flag)
					{
						oracleParameter3.PreBindFree(this.m_connection, 0);
						if (oracleClob2 != null)
						{
							oracleClob2.Close();
						}
					}
					this.FreeNonCachedOpoPrmCtx();
					throw;
				}
				num4++;
			}
			if (flag2)
			{
				oracleParameter2.m_pOpoPrmValCtx = (OpoPrmValCtx*)((void*)array2[num4]);
				try
				{
					oracleParameter2.PostBind(this.m_connection, this.m_pOpoSqlValCtx, 0);
				}
				catch (Exception)
				{
					if (oracleClob != null)
					{
						oracleClob.Close();
					}
					if (flag)
					{
						oracleParameter3.PreBindFree(this.m_connection, 0);
						if (oracleClob2 != null)
						{
							oracleClob2.Close();
						}
					}
					this.FreeNonCachedOpoPrmCtx();
					throw;
				}
				num4++;
			}
			if (oracleClob != null)
			{
				oracleClob.Close();
			}
			if (flag)
			{
				oracleParameter3.m_pOpoPrmValCtx = (OpoPrmValCtx*)((void*)array2[num4]);
				try
				{
					oracleParameter3.PostBind(this.m_connection, this.m_pOpoSqlValCtx, 0);
				}
				catch (Exception)
				{
					if (oracleClob2 != null)
					{
						oracleClob2.Close();
					}
					this.FreeNonCachedOpoPrmCtx();
					throw;
				}
				num4++;
			}
			if (oracleClob2 != null)
			{
				oracleClob2.Close();
			}
			this.FreeNonCachedOpoPrmCtx();
			if (!wantResult)
			{
				return null;
			}
			string fullName = oracleParameter.Value.GetType().FullName;
			if (fullName.Equals("Oracle.DataAccess.Types.OracleClob") && !((OracleClob)oracleParameter.Value).IsNull)
			{
				OracleClob oracleClob3 = (OracleClob)oracleParameter.Value;
				oracleClob3.m_doneTempLobCreate = true;
				oracleClob3.m_isTemporaryLob = true;
				return oracleClob3;
			}
			string text2;
			if (flag)
			{
				text2 = "<?xml version = '1.0'?>\n";
			}
			else
			{
				text2 = "<?xml version = \"1.0\"?>\n";
			}
			if (this.m_xmlQueryProperties.RootTag != null && this.m_xmlQueryProperties.RootTag.Length != 0)
			{
				text2 = text2 + "<" + this.m_xmlQueryProperties.RootTag + "/>\n";
			}
			OracleClob oracleClob4 = new OracleClob(this.m_connection);
			oracleClob4.Append(text2.ToCharArray(), 0, text2.Length);
			return oracleClob4;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0002FB28 File Offset: 0x0002EB28
		private unsafe int ExecuteXmlSave()
		{
			string[] array = null;
			IntPtr[] array2 = null;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr opsSubscrCtx = IntPtr.Zero;
			int num = 0;
			OracleDependency oracleDependency = null;
			int num2 = 0;
			int bchgNTFNExcludeRowidInfo = 0;
			long num3 = 0L;
			int num4 = 0;
			int num5 = 0;
			OracleParameter oracleParameter = null;
			OracleParameter oracleParameter2 = null;
			OracleClob oracleClob = null;
			OracleParameter oracleParameter3 = null;
			OracleParameter oracleParameter4 = null;
			OracleClob oracleClob2 = null;
			bool flag = false;
			int bFromPool = 0;
			CmdTimeoutCtx cmdTimeoutCtx = null;
			Timer timer = null;
			this.CheckConStatus();
			if (this.m_xmlSaveProperties == null)
			{
				this.m_xmlSaveProperties = new OracleXmlSaveProperties();
			}
			if (this.m_xmlSaveProperties.Xslt != null && this.m_xmlSaveProperties.Xslt.Length != 0)
			{
				flag = true;
			}
			if (this.m_NTFNReq != null && this.m_NTFNAutoEnlist && !this.m_connection.m_contextConnection && OracleNotificationRequest.s_idTable[this.m_NTFNReq.Id] != null)
			{
				opsSubscrCtx = OracleNotificationRequest.PopulateChgNTFNSubscrCtx(this, this.m_addRowid, out oracleDependency);
				if (oracleDependency != null && oracleDependency.m_bIsRegistered)
				{
					num = 1;
				}
				if (oracleDependency != null)
				{
					if (oracleDependency.m_OracleRowidInfo == OracleRowidInfo.Exclude)
					{
						bchgNTFNExcludeRowidInfo = 1;
					}
					if (oracleDependency.QueryBasedNotification && this.m_connection.IsDBVer11gR1OrHigher)
					{
						num2 = 1;
					}
				}
			}
			string parameterName = ":OracleXmlDoc$";
			string parameterName2 = ":OracleResult$";
			string parameterName3 = ":OracleTableName$";
			string parameterName4 = ":OracleXslDoc$";
			num5 = 3;
			if (flag)
			{
				num5++;
			}
			this.BuildXmlSaveCommandText();
			this.m_utf8CmdText = null;
			UTF8CommandText utf8CommandText = UTF8CommandText.m_pooler.Get(this.m_connection.m_internalConStr, this.m_pooledCmdText) as UTF8CommandText;
			if (utf8CommandText != null && utf8CommandText.m_utf8CmdText != IntPtr.Zero)
			{
				this.m_utf8CmdText = utf8CommandText;
				this.m_addParam = this.m_utf8CmdText.m_addParam;
				this.m_parsed = this.m_utf8CmdText.m_parsed;
				bFromPool = 1;
			}
			OpoMetValCtx* ptr = null;
			this.m_selectStmt = false;
			this.SetSqlValCtx(true);
			try
			{
				if (!this.m_addToStmtCache && this.m_pOpoPrmCtx != null)
				{
					if (this.m_pOpoPrmCtx->NumValCtxElems >= num5)
					{
						goto IL_2B0;
					}
				}
				try
				{
					if (this.m_utf8CmdText != null)
					{
						intPtr = this.m_utf8CmdText.m_utf8CmdText;
					}
					num4 = OpsSql.Prepare2(this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_opsSqlCtx, ref this.m_opsDacCtx, ref this.m_pOpoSqlValCtx, (intPtr == IntPtr.Zero) ? this.m_pooledCmdText : null, ref intPtr, ref ptr, num5);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					num4 = ErrRes.INT_ERR;
					throw;
				}
				finally
				{
					if (num4 != 0)
					{
						if (!this.m_addToStmtCache && this.m_pOpoSqlValCtx->pOpoPrmCtx == null)
						{
							this.m_pOpoPrmCtx = null;
						}
						if (num4 != ErrRes.INT_ERR)
						{
							OracleException.HandleError(num4, this.m_connection, string.Empty, this.m_opsErrCtx, this.m_pOpoSqlValCtx, this);
						}
					}
				}
				if (!this.m_addToStmtCache && this.m_pOpoPrmCtx == null)
				{
					this.m_pOpoPrmCtx = this.m_pOpoSqlValCtx->pOpoPrmCtx;
				}
				IL_2B0:
				array = new string[num5];
				array2 = new IntPtr[num5];
				if (this.m_commandText.Length > 32512)
				{
					oracleParameter2 = new OracleParameter(parameterName, OracleDbType.Clob);
					oracleParameter2.Direction = ParameterDirection.Input;
					oracleClob = new OracleClob(this.m_connection);
					oracleClob.Append(this.m_commandText.ToCharArray(), 0, this.m_commandText.Length);
					oracleParameter2.Value = oracleClob;
				}
				else
				{
					oracleParameter2 = new OracleParameter(parameterName, OracleDbType.Varchar2);
					oracleParameter2.Direction = ParameterDirection.Input;
					oracleParameter2.Value = this.m_commandText;
				}
				oracleParameter = new OracleParameter();
				oracleParameter.ParameterName = parameterName2;
				oracleParameter.DbType = DbType.Int32;
				oracleParameter.Direction = ParameterDirection.Output;
				oracleParameter3 = new OracleParameter(parameterName3, OracleDbType.Varchar2);
				oracleParameter3.Direction = ParameterDirection.Input;
				if (this.m_xmlSaveProperties.Table == null)
				{
					oracleParameter3.Value = string.Empty;
				}
				else
				{
					oracleParameter3.Value = this.m_xmlSaveProperties.Table;
				}
				if (flag)
				{
					if (this.m_connection.m_majorVersion == 8 && this.m_connection.m_minorVersion == 1 && this.m_xmlSaveProperties.Xslt.Length <= 32512)
					{
						oracleParameter4 = new OracleParameter(parameterName4, OracleDbType.Varchar2);
						oracleParameter4.Direction = ParameterDirection.Input;
						oracleParameter4.Value = this.m_xmlSaveProperties.Xslt;
					}
					else
					{
						oracleParameter4 = new OracleParameter(parameterName4, OracleDbType.Clob);
						oracleParameter4.Direction = ParameterDirection.Input;
						oracleClob2 = new OracleClob(this.m_connection);
						oracleClob2.Append(this.m_xmlSaveProperties.Xslt.ToCharArray(), 0, this.m_xmlSaveProperties.Xslt.Length);
						oracleParameter4.Value = oracleClob2;
					}
				}
				oracleParameter2.m_pOpoPrmValCtx = this.m_pOpoSqlValCtx->pOpoPrmCtx->pOpoPrmValCtx;
				try
				{
					oracleParameter2.PreBind(this.m_connection, this.m_opsErrCtx, 0);
				}
				catch (Exception)
				{
					if (oracleClob != null)
					{
						oracleClob.Close();
					}
					if (oracleClob2 != null)
					{
						oracleClob2.Close();
					}
					this.FreeNonCachedOpoPrmCtx();
					throw;
				}
				oracleParameter.m_pOpoPrmValCtx = this.m_pOpoSqlValCtx->pOpoPrmCtx->pOpoPrmValCtx + 1;
				try
				{
					oracleParameter.PreBind(this.m_connection, this.m_opsErrCtx, 0);
				}
				catch (Exception)
				{
					oracleParameter2.PreBindFree(this.m_connection, 0);
					if (oracleClob != null)
					{
						oracleClob.Close();
					}
					if (oracleClob2 != null)
					{
						oracleClob2.Close();
					}
					this.FreeNonCachedOpoPrmCtx();
					throw;
				}
				oracleParameter3.m_pOpoPrmValCtx = this.m_pOpoSqlValCtx->pOpoPrmCtx->pOpoPrmValCtx + 2;
				try
				{
					oracleParameter3.PreBind(this.m_connection, this.m_opsErrCtx, 0);
				}
				catch (Exception)
				{
					oracleParameter.PreBindFree(this.m_connection, 0);
					oracleParameter2.PreBindFree(this.m_connection, 0);
					if (oracleClob != null)
					{
						oracleClob.Close();
					}
					if (oracleClob2 != null)
					{
						oracleClob2.Close();
					}
					this.FreeNonCachedOpoPrmCtx();
					throw;
				}
				if (flag)
				{
					oracleParameter4.m_pOpoPrmValCtx = this.m_pOpoSqlValCtx->pOpoPrmCtx->pOpoPrmValCtx + 3;
					try
					{
						oracleParameter4.PreBind(this.m_connection, this.m_opsErrCtx, 0);
					}
					catch (Exception)
					{
						oracleParameter3.PreBindFree(this.m_connection, 0);
						oracleParameter.PreBindFree(this.m_connection, 0);
						oracleParameter2.PreBindFree(this.m_connection, 0);
						if (oracleClob != null)
						{
							oracleClob.Close();
						}
						if (oracleClob2 != null)
						{
							oracleClob2.Close();
						}
						this.FreeNonCachedOpoPrmCtx();
						throw;
					}
				}
				array[0] = oracleParameter2.m_paramName;
				array[1] = oracleParameter.m_paramName;
				array[2] = oracleParameter3.m_paramName;
				if (flag)
				{
					array[3] = oracleParameter4.m_paramName;
				}
				array2[0] = (IntPtr)((void*)oracleParameter2.m_pOpoPrmValCtx);
				array2[1] = (IntPtr)((void*)oracleParameter.m_pOpoPrmValCtx);
				array2[2] = (IntPtr)((void*)oracleParameter3.m_pOpoPrmValCtx);
				if (flag)
				{
					array2[3] = (IntPtr)((void*)oracleParameter4.m_pOpoPrmValCtx);
				}
				try
				{
					if (this.m_utf8CmdText != null)
					{
						intPtr = this.m_utf8CmdText.m_utf8CmdText;
					}
					if (this.m_commandTimeout > 0)
					{
						cmdTimeoutCtx = new CmdTimeoutCtx(this.m_opsConCtx, this.m_commandTimeout);
						TimerCallback callback = new TimerCallback(cmdTimeoutCtx.TimeoutNew);
						long num6 = (long)this.m_commandTimeout * 1000L;
						if (num6 > (long)((ulong)-147767296))
						{
							num6 = (long)((ulong)-147767296);
						}
						timer = new Timer(callback, cmdTimeoutCtx, num6, -1L);
						if (cmdTimeoutCtx.m_bDoneOCIBreak)
						{
							string procedure;
							if (this.m_commandType == CommandType.StoredProcedure)
							{
								procedure = this.m_commandText;
							}
							else
							{
								procedure = string.Empty;
							}
							num4 = 1013;
							OracleException.HandleError(num4, this.m_connection, procedure, this.m_opsErrCtx, this.m_pOpoSqlValCtx, this);
						}
					}
					num4 = 0;
					if (this.m_connection.m_opoConCtx.m_bSelfTuning && this.m_connection.m_opoConCtx.pOpoConValCtx->StmtCacheSize > OraTrace.MaxStatementCacheSize)
					{
						this.m_connection.m_opoConCtx.pOpoConValCtx->StmtCacheSize = OraTrace.MaxStatementCacheSize;
						num4 = OpsCon.SetStatementCacheSize(this.m_opsConCtx, ref this.m_opsErrCtx, this.m_connection.m_opoConCtx.pOpoConValCtx);
						if (this.m_connection.m_opoConCtx.m_conPooler != null)
						{
							this.m_connection.m_opoConCtx.m_conPooler.ModifyConPoolerSize(this.m_connection.m_opoConCtx.pOpoConValCtx->StmtCacheSize);
						}
					}
					if (num4 == 0)
					{
						this.m_opsDacCtx = IntPtr.Zero;
						num4 = OpsSql.ExecuteNonQuery(this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_opsSqlCtx, ref this.m_opsDacCtx, opsSubscrCtx, ref num, bchgNTFNExcludeRowidInfo, num2, ref num3, ref this.m_pOpoSqlValCtx, (intPtr == IntPtr.Zero || this.m_selectStmt) ? this.m_pooledCmdText : null, ref intPtr, array2, array, ref ptr, num5, bFromPool);
					}
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					num4 = ErrRes.INT_ERR;
					throw;
				}
				finally
				{
					if (this.m_commandTimeout > 0 && cmdTimeoutCtx != null)
					{
						cmdTimeoutCtx.m_bDoneExecution = true;
						if (!cmdTimeoutCtx.m_hWaitForOciBreakEvent.WaitOne(5000, false) && OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.Trace(1U, new string[]
							{
								" (WARN)  OracleCommand::ExecuteXmlSave() WaitOne() timed out \n"
							});
						}
						timer.Dispose();
						cmdTimeoutCtx.Dispose();
					}
					if (oracleDependency != null && num == 1 && !this.m_connection.m_contextConnection)
					{
						oracleDependency.SetRegisterInfo(this.m_connection.m_opoConCtx.opoConRefCtx.userID, this.m_connection.DataSource, this.m_NTFNReq.IsNotifiedOnce, this.m_NTFNReq.IsPersistent, this.m_NTFNReq.Timeout);
					}
					if (num4 != 0)
					{
						oracleParameter2.PreBindFree(this.m_connection, 0);
						if (oracleClob != null)
						{
							oracleClob.Close();
						}
						oracleParameter.PreBindFree(this.m_connection, 0);
						oracleParameter3.PreBindFree(this.m_connection, 0);
						if (flag)
						{
							oracleParameter4.PreBindFree(this.m_connection, 0);
							if (oracleClob2 != null)
							{
								oracleClob2.Close();
							}
						}
						this.FreeNonCachedOpoPrmCtx();
						if (num4 != ErrRes.INT_ERR)
						{
							OracleException.HandleError(num4, this.m_connection, string.Empty, this.m_opsErrCtx, this.m_pOpoSqlValCtx, this, true);
						}
					}
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					utf8CommandText = (UTF8CommandText.m_pooler.Get(this.m_connection.m_internalConStr, this.m_pooledCmdText) as UTF8CommandText);
					if (utf8CommandText == null)
					{
						if (utf8CommandText == null && this.m_utf8CmdText == null)
						{
							this.m_utf8CmdText = new UTF8CommandText(intPtr);
						}
						this.m_utf8CmdText.m_parsed = this.m_parsed;
						this.m_utf8CmdText.m_addParam = this.m_addParam;
						UTF8CommandText.m_pooler.Put(this.m_connection.m_internalConStr, this.m_pooledCmdText, this.m_utf8CmdText);
					}
					else if (this.m_utf8CmdText == null)
					{
						this.m_utf8CmdText = new UTF8CommandText(intPtr);
					}
				}
			}
			if (oracleDependency != null && !this.m_connection.m_contextConnection)
			{
				oracleDependency.m_bIsEnabled = true;
				if (!oracleDependency.m_regList.Contains(this.m_commandText))
				{
					oracleDependency.m_regList.Add(this.m_commandText);
				}
				if (num2 == 1 && !oracleDependency.m_queryIDList.Contains(num3))
				{
					oracleDependency.m_queryIDList.Add(num3);
				}
			}
			oracleParameter2.m_pOpoPrmValCtx = (OpoPrmValCtx*)((void*)array2[0]);
			oracleParameter.m_pOpoPrmValCtx = (OpoPrmValCtx*)((void*)array2[1]);
			oracleParameter3.m_pOpoPrmValCtx = (OpoPrmValCtx*)((void*)array2[2]);
			try
			{
				if (oracleParameter2.m_bOracleDbTypeExSet)
				{
					oracleParameter2.m_enumType = PrmEnumType.DBTYPE;
				}
				oracleParameter2.PostBind(this.m_connection, this.m_pOpoSqlValCtx, 0);
			}
			catch (Exception)
			{
				if (oracleClob != null)
				{
					oracleClob.Close();
				}
				oracleParameter.PreBindFree(this.m_connection, 0);
				oracleParameter3.PreBindFree(this.m_connection, 0);
				if (flag)
				{
					oracleParameter4.PreBindFree(this.m_connection, 0);
					if (oracleClob2 != null)
					{
						oracleClob2.Close();
					}
				}
				this.FreeNonCachedOpoPrmCtx();
				throw;
			}
			try
			{
				oracleParameter.PostBind(this.m_connection, this.m_pOpoSqlValCtx, 0);
			}
			catch (Exception)
			{
				if (oracleClob != null)
				{
					oracleClob.Close();
				}
				oracleParameter3.PreBindFree(this.m_connection, 0);
				if (flag)
				{
					oracleParameter4.PreBindFree(this.m_connection, 0);
					if (oracleClob2 != null)
					{
						oracleClob2.Close();
					}
				}
				this.FreeNonCachedOpoPrmCtx();
				throw;
			}
			try
			{
				oracleParameter3.PostBind(this.m_connection, this.m_pOpoSqlValCtx, 0);
			}
			catch (Exception)
			{
				if (oracleClob != null)
				{
					oracleClob.Close();
				}
				if (flag)
				{
					oracleParameter4.PreBindFree(this.m_connection, 0);
					if (oracleClob2 != null)
					{
						oracleClob2.Close();
					}
				}
				this.FreeNonCachedOpoPrmCtx();
				throw;
			}
			if (flag)
			{
				oracleParameter4.m_pOpoPrmValCtx = (OpoPrmValCtx*)((void*)array2[3]);
				try
				{
					oracleParameter4.PostBind(this.m_connection, this.m_pOpoSqlValCtx, 0);
				}
				catch (Exception)
				{
					if (oracleClob != null)
					{
						oracleClob.Close();
					}
					if (oracleClob2 != null)
					{
						oracleClob2.Close();
					}
					this.FreeNonCachedOpoPrmCtx();
					throw;
				}
			}
			if (oracleClob != null)
			{
				oracleClob.Close();
			}
			if (oracleClob2 != null)
			{
				oracleClob2.Close();
			}
			this.FreeNonCachedOpoPrmCtx();
			this.m_rowsAffected = (int)oracleParameter.Value;
			return this.m_rowsAffected;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00030928 File Offset: 0x0002F928
		private void BuildXmlQueryCommandText(bool wantResult, string resultParamName)
		{
			bool flag = false;
			int num = 0;
			bool flag2 = false;
			string text = string.Empty;
			string text2 = string.Empty;
			int majorVersion = this.m_connection.m_majorVersion;
			int minorVersion = this.m_connection.m_minorVersion;
			if ((majorVersion == 8 && minorVersion == 1) || (majorVersion == 9 && minorVersion == 0))
			{
				flag = true;
			}
			this.m_pooledCmdText = this.m_commandText;
			StringBuilder stringBuilder = new StringBuilder(4096);
			if (this.m_xmlQueryProperties == null)
			{
				this.m_xmlQueryProperties = new OracleXmlQueryProperties();
			}
			if (this.m_xmlQueryProperties.Xslt != null && this.m_xmlQueryProperties.Xslt.Length != 0)
			{
				flag2 = true;
			}
			if (this.m_xmlQueryProperties.RootTag != null && this.m_xmlQueryProperties.RootTag.Length != 0)
			{
				text = this.m_xmlQueryProperties.RootTag;
			}
			if (this.m_xmlQueryProperties.RowTag != null && this.m_xmlQueryProperties.RowTag.Length != 0)
			{
				text2 = this.m_xmlQueryProperties.RowTag;
			}
			if (flag)
			{
				stringBuilder.Append("declare ");
				stringBuilder.Append("ctx DBMS_XMLQUERY.ctxType; ");
				if (!wantResult)
				{
					stringBuilder.Append("OracleResult CLOB; ");
				}
				stringBuilder.Append("begin ");
				stringBuilder.Append("ctx := DBMS_XMLQUERY.newContext(:OracleSqlQuery$); ");
				stringBuilder.Append("DBMS_XMLQUERY.setRaiseException(ctx, true); ");
				stringBuilder.Append("DBMS_XMLQUERY.setRowIdAttrName(ctx, ''); ");
				stringBuilder.Append("DBMS_XMLQUERY.setDateFormat(ctx, 'yyyy-MM-dd''T''HH:mm:ss.SSS'); ");
				stringBuilder.Append("DBMS_XMLQUERY.useTypeForCollElemTag(ctx); ");
				if (!text.Equals("ROWSET"))
				{
					stringBuilder.Append("DBMS_XMLQUERY.setRowsetTag(ctx, '");
					stringBuilder.Append(text);
					stringBuilder.Append("'); ");
				}
				if (!text2.Equals("ROW"))
				{
					stringBuilder.Append("DBMS_XMLQUERY.setRowTag(ctx, '");
					stringBuilder.Append(text2);
					stringBuilder.Append("'); ");
				}
				if (this.m_xmlQueryProperties.MaxRows > -1)
				{
					stringBuilder.Append("DBMS_XMLQUERY.setMaxRows(ctx, '");
					stringBuilder.Append(this.m_xmlQueryProperties.MaxRows.ToString());
					stringBuilder.Append("'); ");
				}
				if (this.m_parameters != null && this.m_addParam)
				{
					num = this.m_parameters.Count;
				}
				for (int i = 0; i < num; i++)
				{
					string text3 = this.m_parameters[i].ParameterName.Trim();
					stringBuilder.Append("DBMS_XMLQUERY.setBindValue(ctx, '");
					stringBuilder.Append(text3.Substring(1));
					stringBuilder.Append("', ");
					stringBuilder.Append(text3);
					stringBuilder.Append("); ");
				}
				if (flag2)
				{
					stringBuilder.Append("DBMS_XMLQUERY.setXSLT(ctx, :OracleXslDoc$, ''); ");
				}
				if (wantResult)
				{
					stringBuilder.Append(resultParamName);
				}
				else
				{
					stringBuilder.Append("OracleResult");
				}
				stringBuilder.Append(" := DBMS_XMLQUERY.getXML(ctx); ");
				stringBuilder.Append("DBMS_XMLQUERY.closeContext(ctx); ");
				stringBuilder.Append("end;");
			}
			else
			{
				stringBuilder.Append("declare ");
				stringBuilder.Append("ctx DBMS_XMLGEN.ctxHandle; ");
				stringBuilder.Append("refcur SYS_REFCURSOR; ");
				if (!wantResult)
				{
					stringBuilder.Append("OracleResult CLOB; ");
				}
				if (flag2)
				{
					stringBuilder.Append("xmlClob CLOB; ");
					stringBuilder.Append("tmpClob CLOB; ");
					stringBuilder.Append("p DBMS_XMLPARSER.Parser; ");
					stringBuilder.Append("xmldoc DBMS_XMLDOM.DOMDocument; ");
					stringBuilder.Append("xsldoc DBMS_XMLDOM.DOMDocument; ");
					stringBuilder.Append("ss DBMS_XSLPROCESSOR.Stylesheet; ");
					stringBuilder.Append("proc DBMS_XSLPROCESSOR.Processor; ");
				}
				stringBuilder.Append("begin ");
				this.m_pooledCmdText = this.m_pooledCmdText.Trim();
				stringBuilder.Append("OPEN refcur FOR ");
				stringBuilder.Append(this.m_pooledCmdText);
				if (this.m_pooledCmdText.EndsWith(";"))
				{
					stringBuilder.Append(" ");
				}
				else
				{
					stringBuilder.Append("; ");
				}
				stringBuilder.Append("ctx := DBMS_XMLGEN.newContext(refcur); ");
				if (!text.Equals("ROWSET"))
				{
					stringBuilder.Append("DBMS_XMLGEN.setRowSetTag(ctx, '");
					stringBuilder.Append(text);
					stringBuilder.Append("'); ");
				}
				if (!text2.Equals("ROW"))
				{
					stringBuilder.Append("DBMS_XMLGEN.setRowTag(ctx, '");
					stringBuilder.Append(text2);
					stringBuilder.Append("'); ");
				}
				if (this.m_xmlQueryProperties.MaxRows > -1)
				{
					stringBuilder.Append("DBMS_XMLGEN.setMaxRows(ctx, '");
					stringBuilder.Append(this.m_xmlQueryProperties.MaxRows.ToString());
					stringBuilder.Append("'); ");
				}
				if (flag2)
				{
					stringBuilder.Append("xmlClob");
				}
				else if (wantResult)
				{
					stringBuilder.Append(resultParamName);
				}
				else
				{
					stringBuilder.Append("OracleResult");
				}
				stringBuilder.Append(" := DBMS_XMLGEN.getXML(ctx); ");
				stringBuilder.Append("DBMS_XMLGEN.closeContext(ctx); ");
				stringBuilder.Append("CLOSE refcur; ");
				if (flag2)
				{
					this.Build9iXslCommandTextForXmlGen(stringBuilder, wantResult, this.m_xmlQueryProperties.XsltParams);
				}
				stringBuilder.Append("end;");
			}
			this.m_pooledCmdText = stringBuilder.ToString();
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00030E58 File Offset: 0x0002FE58
		private void BuildXmlSaveCommandText()
		{
			bool flag = false;
			bool flag2 = false;
			string[] array = null;
			string[] array2 = null;
			string text = string.Empty;
			int majorVersion = this.m_connection.m_majorVersion;
			int minorVersion = this.m_connection.m_minorVersion;
			if ((majorVersion == 8 && minorVersion == 1) || (majorVersion == 9 && minorVersion == 0))
			{
				flag = true;
			}
			StringBuilder stringBuilder = new StringBuilder(4096);
			if (this.m_xmlSaveProperties == null)
			{
				this.m_xmlSaveProperties = new OracleXmlSaveProperties();
			}
			if (this.m_xmlSaveProperties.Xslt != null && this.m_xmlSaveProperties.Xslt.Length != 0)
			{
				flag2 = true;
			}
			if (this.m_xmlSaveProperties.RowTag != null && this.m_xmlSaveProperties.RowTag.Length != 0)
			{
				text = this.m_xmlSaveProperties.RowTag;
			}
			stringBuilder.Append("declare ");
			stringBuilder.Append("ctx DBMS_XMLSAVE.ctxType; ");
			if (flag && flag2)
			{
				stringBuilder.Append("xmlClob CLOB; ");
				stringBuilder.Append("tmpClob CLOB; ");
				stringBuilder.Append("p XMLPARSER.Parser; ");
				stringBuilder.Append("xmldoc XMLDOM.DOMDocument; ");
				stringBuilder.Append("xsldoc XMLDOM.DOMDocument; ");
				stringBuilder.Append("ss XSLPROCESSOR.Stylesheet; ");
				stringBuilder.Append("proc XSLPROCESSOR.Processor; ");
			}
			stringBuilder.Append("begin ");
			if (flag && flag2)
			{
				this.Build8iXslCommandTextForXmlSave(stringBuilder, this.m_xmlSaveProperties.XsltParams);
			}
			stringBuilder.Append("ctx := DBMS_XMLSAVE.newContext(:OracleTableName$); ");
			if (!text.Equals("ROW"))
			{
				stringBuilder.Append("DBMS_XMLSAVE.setRowTag(ctx, '");
				stringBuilder.Append(text);
				stringBuilder.Append("'); ");
			}
			stringBuilder.Append("DBMS_XMLSAVE.setIgnoreCase(ctx, DBMS_XMLSAVE.MATCH_CASE); ");
			if (!flag)
			{
				stringBuilder.Append("DBMS_XMLSAVE.setSQLToXMLNameEscaping(ctx, true); ");
			}
			stringBuilder.Append("DBMS_XMLSAVE.setDateFormat(ctx, 'yyyy-MM-dd''T''HH:mm:ss.SSS'); ");
			if (this.m_xmlSaveProperties.KeyColumnsList != null)
			{
				int i = 0;
				while (i < this.m_xmlSaveProperties.KeyColumnsList.Length && this.m_xmlSaveProperties.KeyColumnsList[i] != null)
				{
					stringBuilder.Append("DBMS_XMLSAVE.setKeyColumn(ctx, '");
					stringBuilder.Append(this.m_xmlSaveProperties.KeyColumnsList[i]);
					stringBuilder.Append("'); ");
					i++;
				}
			}
			if (this.m_xmlSaveProperties.UpdateColumnsList != null)
			{
				int i = 0;
				while (i < this.m_xmlSaveProperties.UpdateColumnsList.Length && this.m_xmlSaveProperties.UpdateColumnsList[i] != null)
				{
					stringBuilder.Append("DBMS_XMLSAVE.setUpdateColumn(ctx, '");
					stringBuilder.Append(this.m_xmlSaveProperties.UpdateColumnsList[i]);
					stringBuilder.Append("'); ");
					i++;
				}
			}
			if (!flag && flag2)
			{
				stringBuilder.Append("DBMS_XMLSAVE.setXSLT(ctx, :OracleXslDoc$, ''); ");
				int num = this.ParseXsltParams(this.m_xmlSaveProperties.XsltParams, out array, out array2);
				for (int i = 0; i < num; i++)
				{
					stringBuilder.Append("DBMS_XMLSAVE.setXSLTParam(ctx, '");
					stringBuilder.Append(array[i]);
					stringBuilder.Append("', '");
					stringBuilder.Append(array2[i]);
					stringBuilder.Append("'); ");
				}
			}
			stringBuilder.Append(":OracleResult$");
			if (flag && flag2)
			{
				if (OracleXmlCommandType.Insert == this.m_xmlCommandType)
				{
					stringBuilder.Append(" := DBMS_XMLSAVE.insertXML(ctx, xmlClob); ");
				}
				else if (OracleXmlCommandType.Update == this.m_xmlCommandType)
				{
					stringBuilder.Append(" := DBMS_XMLSAVE.updateXML(ctx, xmlClob); ");
				}
				else if (OracleXmlCommandType.Delete == this.m_xmlCommandType)
				{
					stringBuilder.Append(" := DBMS_XMLSAVE.deleteXML(ctx, xmlClob); ");
				}
			}
			else if (OracleXmlCommandType.Insert == this.m_xmlCommandType)
			{
				stringBuilder.Append(" := DBMS_XMLSAVE.insertXML(ctx, :OracleXmlDoc$); ");
			}
			else if (OracleXmlCommandType.Update == this.m_xmlCommandType)
			{
				stringBuilder.Append(" := DBMS_XMLSAVE.updateXML(ctx, :OracleXmlDoc$); ");
			}
			else if (OracleXmlCommandType.Delete == this.m_xmlCommandType)
			{
				stringBuilder.Append(" := DBMS_XMLSAVE.deleteXML(ctx, :OracleXmlDoc$); ");
			}
			stringBuilder.Append("DBMS_XMLSAVE.closeContext(ctx); ");
			if (flag && flag2)
			{
				stringBuilder.Append("dbms_lob.freetemporary(xmlClob); ");
			}
			stringBuilder.Append("end;");
			this.m_pooledCmdText = stringBuilder.ToString();
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00031238 File Offset: 0x00030238
		private void Build8iXslCommandTextForXmlSave(StringBuilder strBldr, string xsltParams)
		{
			string[] array = null;
			string[] array2 = null;
			string value = ":OracleXmlDoc$";
			string value2 = "xmlClob";
			strBldr.Append("dbms_lob.createtemporary(tmpClob, TRUE); ");
			strBldr.Append("p := XMLPARSER.newParser; ");
			strBldr.Append("XMLPARSER.setValidationMode(p, FALSE); ");
			strBldr.Append("XMLPARSER.setPreserveWhiteSpace(p, TRUE); ");
			if (this.m_commandText.Length > 32512)
			{
				strBldr.Append("XMLPARSER.parseClob(p, ");
			}
			else
			{
				strBldr.Append("XMLPARSER.parseBuffer(p, ");
			}
			strBldr.Append(value);
			strBldr.Append("); ");
			strBldr.Append("xmldoc := XMLPARSER.getDocument(p); ");
			if (this.m_xmlSaveProperties == null)
			{
				this.m_xmlSaveProperties = new OracleXmlSaveProperties();
			}
			if (this.m_xmlSaveProperties.Xslt.Length > 32512)
			{
				strBldr.Append("XMLPARSER.parseClob(p, :OracleXslDoc$); ");
			}
			else
			{
				strBldr.Append("XMLPARSER.parseBuffer(p, :OracleXslDoc$); ");
			}
			strBldr.Append("xsldoc := XMLPARSER.getDocument(p); ");
			strBldr.Append("ss := XSLPROCESSOR.newStylesheet(xsldoc, ''); ");
			int num = this.ParseXsltParams(xsltParams, out array, out array2);
			for (int i = 0; i < num; i++)
			{
				strBldr.Append("XSLPROCESSOR.setParam(ss, '");
				strBldr.Append(array[i]);
				strBldr.Append("', '");
				strBldr.Append(array2[i]);
				strBldr.Append("'); ");
			}
			strBldr.Append("proc := XSLPROCESSOR.newProcessor; ");
			strBldr.Append("XSLPROCESSOR.processXSL(proc, ss, xmldoc, tmpClob); ");
			strBldr.Append(value2);
			strBldr.Append(" := tmpClob; ");
			strBldr.Append("XMLDOM.freeDocument(xmldoc); ");
			strBldr.Append("XMLDOM.freeDocument(xsldoc); ");
			strBldr.Append("XSLPROCESSOR.freeProcessor(proc); ");
			strBldr.Append("XSLPROCESSOR.freeStylesheet(ss); ");
			strBldr.Append("XMLPARSER.freeParser(p); ");
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000313F0 File Offset: 0x000303F0
		private void Build9iXslCommandTextForXmlGen(StringBuilder strBldr, bool wantResult, string xsltParams)
		{
			string[] array = null;
			string[] array2 = null;
			string value;
			string value2;
			if (OracleXmlCommandType.Query == this.m_xmlCommandType)
			{
				value = "xmlClob";
				if (wantResult)
				{
					value2 = ":OracleResult$";
				}
				else
				{
					value2 = "OracleResult";
				}
			}
			else
			{
				value = ":OracleXmlDoc$";
				value2 = "xmlClob";
			}
			strBldr.Append("dbms_lob.createtemporary(tmpClob, TRUE); ");
			strBldr.Append("p := DBMS_XMLPARSER.newParser; ");
			strBldr.Append("DBMS_XMLPARSER.setValidationMode(p, FALSE); ");
			strBldr.Append("DBMS_XMLPARSER.setPreserveWhiteSpace(p, TRUE); ");
			if (OracleXmlCommandType.Query == this.m_xmlCommandType || this.m_commandText.Length > 32512)
			{
				strBldr.Append("DBMS_XMLPARSER.parseClob(p, ");
			}
			else
			{
				strBldr.Append("DBMS_XMLPARSER.parseBuffer(p, ");
			}
			strBldr.Append(value);
			strBldr.Append("); ");
			strBldr.Append("xmldoc := DBMS_XMLPARSER.getDocument(p); ");
			if (this.m_xmlQueryProperties == null)
			{
				this.m_xmlQueryProperties = new OracleXmlQueryProperties();
			}
			if (this.m_xmlSaveProperties == null)
			{
				this.m_xmlSaveProperties = new OracleXmlSaveProperties();
			}
			if ((OracleXmlCommandType.Query == this.m_xmlCommandType && this.m_xmlQueryProperties.Xslt.Length > 32512) || (OracleXmlCommandType.Query != this.m_xmlCommandType && this.m_xmlSaveProperties.Xslt.Length > 32512))
			{
				strBldr.Append("DBMS_XMLPARSER.parseClob(p, :OracleXslDoc$); ");
			}
			else
			{
				strBldr.Append("DBMS_XMLPARSER.parseBuffer(p, :OracleXslDoc$); ");
			}
			strBldr.Append("xsldoc := DBMS_XMLPARSER.getDocument(p); ");
			strBldr.Append("ss := DBMS_XSLPROCESSOR.newStylesheet(xsldoc, ''); ");
			int num = this.ParseXsltParams(xsltParams, out array, out array2);
			for (int i = 0; i < num; i++)
			{
				strBldr.Append("DBMS_XSLPROCESSOR.setParam(ss, '");
				strBldr.Append(array[i]);
				strBldr.Append("', '");
				strBldr.Append(array2[i]);
				strBldr.Append("'); ");
			}
			strBldr.Append("proc := DBMS_XSLPROCESSOR.newProcessor; ");
			strBldr.Append("DBMS_XSLPROCESSOR.processXSL(proc, ss, xmldoc, tmpClob); ");
			strBldr.Append(value2);
			strBldr.Append(" := tmpClob; ");
			strBldr.Append("DBMS_XMLDOM.freeDocument(xmldoc); ");
			strBldr.Append("DBMS_XMLDOM.freeDocument(xsldoc); ");
			strBldr.Append("DBMS_XSLPROCESSOR.freeProcessor(proc); ");
			strBldr.Append("DBMS_XSLPROCESSOR.freeStylesheet(ss); ");
			strBldr.Append("DBMS_XMLPARSER.freeParser(p); ");
			strBldr.Append("dbms_lob.freetemporary(tmpClob); ");
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00031624 File Offset: 0x00030624
		private int ParseXsltParams(string xsltParams, out string[] xsltParamNames, out string[] xsltParamValues)
		{
			int num = 1;
			int num2 = 0;
			xsltParamNames = null;
			xsltParamValues = null;
			if (xsltParams == null || xsltParams.Length == 0)
			{
				return num2;
			}
			int num3 = 0;
			int num4;
			while (-1 != (num4 = xsltParams.IndexOf(";", num3)))
			{
				num++;
				num3 = num4 + 1;
			}
			xsltParamNames = new string[num];
			xsltParamValues = new string[num];
			num3 = 0;
			for (int i = 0; i < num; i++)
			{
				num4 = xsltParams.IndexOf(";", num3);
				int num5;
				if (-1 == num4)
				{
					num5 = xsltParams.Length;
				}
				else
				{
					num5 = num4;
				}
				string text = xsltParams.Substring(num3, num5 - num3);
				int num6;
				if (text != null && text.Length != 0 && -1 != (num6 = text.IndexOf("=")))
				{
					string text2 = text.Substring(0, num6).Trim();
					if (text2 != null && text2.Length != 0)
					{
						string text3 = text.Substring(num6 + 1).Trim();
						xsltParamNames[num2] = text2;
						xsltParamValues[num2] = text3;
						num2++;
					}
				}
				num3 = num5 + 1;
			}
			return num2;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00031738 File Offset: 0x00030738
		private int AlignedFS(int FetchSize)
		{
			int num;
			if ((num = FetchSize % 4) != 0)
			{
				FetchSize = FetchSize - num + 4;
			}
			return FetchSize;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00031758 File Offset: 0x00030758
		private string[] GetPlsqlOutput()
		{
			int num = 1024;
			string[] array = null;
			if (this.m_connection == null)
			{
				throw new InvalidOperationException();
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_opsConCtx != this.m_connection.m_opoConCtx.opsConCtx)
			{
				if (this.m_opsConCtx != IntPtr.Zero)
				{
					try
					{
						OpsCon.RelRef(ref this.m_opsConCtx);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
					}
				}
				this.m_opsConCtx = this.m_connection.m_opoConCtx.opsConCtx;
				if (this.m_opsConCtx != IntPtr.Zero)
				{
					try
					{
						int num2 = OpsCon.AddRef(this.m_opsConCtx);
						if (num2 <= 1)
						{
							throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
						}
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
						throw;
					}
				}
			}
			ArrayList arrayList = new ArrayList(32);
			while (num == 1024)
			{
				array = new string[num];
				try
				{
					OpsDac.GetPlsqlOutput(this.m_opsConCtx, this.m_opsErrCtx, array, ref num);
				}
				catch (Exception ex3)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex3);
					}
					throw;
				}
				arrayList.Add(array);
			}
			int num3 = num + (arrayList.Count - 1) * 1024;
			string[] array2 = new string[num3];
			int i = 0;
			int num4 = 0;
			while (i < arrayList.Count)
			{
				string[] array3 = (string[])arrayList[i];
				int num5;
				if (i == arrayList.Count - 1)
				{
					num5 = num;
				}
				else
				{
					num5 = 1024;
				}
				int j = 0;
				while (j < num5)
				{
					array2[num4] = array3[j];
					j++;
					num4++;
				}
				i++;
			}
			return array2;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00031958 File Offset: 0x00030958
		private static bool isSelectStatement(string text)
		{
			char c = ' ';
			int length = text.Length;
			int i = 0;
			if (length >= 6)
			{
				while (i < length)
				{
					c = text[i];
					if (c != ' ' && (c > '\r' || c < '\t'))
					{
						break;
					}
					i++;
				}
				if (length - i >= 6 && (c == 's' || c == 'S'))
				{
					c = text[++i];
					if (c == 'e' || c == 'E')
					{
						c = text[++i];
						if (c == 'l' || c == 'L')
						{
							c = text[++i];
							if (c == 'e' || c == 'E')
							{
								c = text[++i];
								if (c == 'c' || c == 'C')
								{
									c = text[i + 1];
									if (c == 't' || c == 'T')
									{
										return true;
									}
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00031A1C File Offset: 0x00030A1C
		private unsafe void CheckConStatus()
		{
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_conSignature != this.m_connection.m_conSignature)
			{
				if (this.m_metaData != null)
				{
					this.m_metaData = null;
				}
				if (this.m_pOpoSqlValCtx == null || !(this.m_pOpoSqlValCtx->pSnapShot != IntPtr.Zero))
				{
					if (this.m_pOpoPrmCtx == null || !(this.m_pOpoPrmCtx->m_pAttrRefTdo != IntPtr.Zero))
					{
						goto IL_BD;
					}
				}
				try
				{
					OpsSql.FreeRefTDOandOCISnapShot(this.m_pOpoPrmCtx, this.m_pOpoSqlValCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
				}
				IL_BD:
				if (this.m_opsSqlCtx != IntPtr.Zero)
				{
					try
					{
						if (!this.m_addToStmtCache)
						{
							OpsSql.FreeCtx(ref this.m_opsSqlCtx, this.m_opsErrCtx, 0);
						}
						else
						{
							OpsSql.FreeCtx(ref this.m_opsSqlCtx, this.m_opsErrCtx, 1);
						}
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
					}
					this.m_opsSqlCtx = IntPtr.Zero;
				}
				if (this.m_opsErrCtx != IntPtr.Zero)
				{
					try
					{
						OpsErr.FreeCtx(ref this.m_opsErrCtx);
					}
					catch (Exception ex3)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex3);
						}
					}
					this.m_opsErrCtx = IntPtr.Zero;
				}
				this.m_conSignature = this.m_connection.m_conSignature;
			}
			if (this.m_opsConCtx != this.m_connection.m_opoConCtx.opsConCtx)
			{
				if (this.m_opsConCtx != IntPtr.Zero)
				{
					try
					{
						OpsCon.RelRef(ref this.m_opsConCtx);
					}
					catch (Exception ex4)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex4);
						}
					}
				}
				this.m_opsConCtx = this.m_connection.m_opoConCtx.opsConCtx;
				try
				{
					int num = OpsCon.AddRef(this.m_opsConCtx);
					if (num <= 1)
					{
						throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
					}
				}
				catch (Exception ex5)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex5);
					}
					throw;
				}
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00031C74 File Offset: 0x00030C74
		private unsafe void SetSqlValCtx(bool bXmlQuerySave)
		{
			if (this.m_pOpoSqlValCtx == null)
			{
				try
				{
					OpsSql.AllocSqlValCtx(ref this.m_pOpoSqlValCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
			}
			if (this.m_selectStmt || (this.m_connection.m_oraTransaction != null && !this.m_connection.m_oraTransaction.Completed))
			{
				this.m_pOpoSqlValCtx->mode = 0U;
			}
			else if (this.m_connection.m_opoConCtx.pOpoConValCtx != null && this.m_connection.m_opoConCtx.pOpoConValCtx->InMtsTxn == 1)
			{
				this.m_pOpoSqlValCtx->mode = 0U;
			}
			else if (this.m_connection.m_contextConnection)
			{
				this.m_pOpoSqlValCtx->mode = 0U;
			}
			else
			{
				this.m_pOpoSqlValCtx->mode = 32U;
			}
			if (this.m_arrayBindCount > 1)
			{
				this.m_pOpoSqlValCtx->mode |= 128U;
			}
			if (this.m_connection.m_opoConCtx.pOpoConValCtx->OSAuthent.Equals(OSAuthent.ProxyUser) || (this.m_connection.m_opoConCtx.opoConRefCtx.proxyUserId != null && this.m_connection.m_opoConCtx.opoConRefCtx.proxyUserId.Length > 0))
			{
				this.m_addToStmtCache = false;
				this.m_pOpoSqlValCtx->AddToStmtCache = 0;
				this.m_pOpoSqlValCtx->pOpoPrmCtx = this.m_pOpoPrmCtx;
			}
			else if (this.m_connection.m_opoConCtx.pOpoConValCtx->StmtCacheSize > 0 && !this.m_addToStatementCache)
			{
				this.m_addToStmtCache = false;
				this.m_pOpoSqlValCtx->AddToStmtCache = 0;
				this.m_pOpoSqlValCtx->pOpoPrmCtx = this.m_pOpoPrmCtx;
			}
			else
			{
				this.m_addToStmtCache = true;
				this.m_pOpoSqlValCtx->AddToStmtCache = 1;
				this.m_pOpoSqlValCtx->pOpoPrmCtx = null;
			}
			this.m_pOpoSqlValCtx->RowsAffected = this.m_rowsAffected;
			this.m_pOpoSqlValCtx->StmtPrepared = 0;
			if (this.m_isFromEF)
			{
				this.m_pOpoSqlValCtx->bIsFromEF = 1;
			}
			else
			{
				this.m_pOpoSqlValCtx->bIsFromEF = 0;
			}
			if (bXmlQuerySave)
			{
				this.m_pOpoSqlValCtx->BindByName = 1;
				this.m_pOpoSqlValCtx->LocalParse = 0;
				this.m_pOpoSqlValCtx->AddRowid = 0;
				this.m_pOpoSqlValCtx->ArraySize = 1;
				this.m_pOpoSqlValCtx->FetchSize = 0L;
				this.m_pOpoSqlValCtx->InitialLongFS = 65536;
				return;
			}
			if (this.m_bindByName && this.m_commandType != CommandType.StoredProcedure)
			{
				this.m_pOpoSqlValCtx->BindByName = 1;
			}
			else
			{
				this.m_pOpoSqlValCtx->BindByName = 0;
			}
			if (this.m_localParse)
			{
				this.m_pOpoSqlValCtx->LocalParse = 1;
			}
			else
			{
				this.m_pOpoSqlValCtx->LocalParse = 0;
			}
			if (!this.m_addRowid)
			{
				this.m_pOpoSqlValCtx->AddRowid = 0;
			}
			else
			{
				this.m_pOpoSqlValCtx->AddRowid = 1;
			}
			if (this.m_arrayBindCount == 0)
			{
				this.m_pOpoSqlValCtx->ArraySize = 1;
			}
			else
			{
				this.m_pOpoSqlValCtx->ArraySize = this.m_arrayBindCount;
			}
			this.m_pOpoSqlValCtx->FetchSize = this.m_fetchSize;
			this.m_pOpoSqlValCtx->InitialLongFS = this.m_initialLongFS;
			if (this.m_connection.m_majorVersion == 8)
			{
				this.m_pOpoSqlValCtx->InitialLobFS = 0;
				return;
			}
			this.m_pOpoSqlValCtx->InitialLobFS = this.m_initialLobFS;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00031FCC File Offset: 0x00030FCC
		private void AddRefCursorParamToParamColl(RefCursorInfo cursorInfo)
		{
			OracleParameter oracleParameter = new OracleParameter();
			oracleParameter.ParameterName = cursorInfo.name;
			oracleParameter.OracleDbType = OracleDbType.RefCursor;
			oracleParameter.Direction = cursorInfo.mode;
			if (this.m_parameters == null)
			{
				this.m_parameters = new OracleParameterCollection();
			}
			if (cursorInfo.position >= 0 && this.m_parameters.Count > cursorInfo.position)
			{
				this.m_parameters.Insert(cursorInfo.position, oracleParameter);
				return;
			}
			this.m_parameters.Add(oracleParameter);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00032050 File Offset: 0x00031050
		protected override DbParameter CreateDbParameter()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleCommand::CreateDbParameter()\n"
				});
			}
			OracleParameter result = new OracleParameter();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleCommand::CreateDbParameter()\n"
				});
			}
			return result;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000320A0 File Offset: 0x000310A0
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			OracleDataReader oracleDataReader = this.ExecuteReader(true, false, behavior);
			oracleDataReader.m_returnPSTypes = this.m_returnPSTypes;
			return oracleDataReader;
		}

		// Token: 0x0400028D RID: 653
		internal const int m_rowsToFetch = 1024;

		// Token: 0x0400028E RID: 654
		private IntPtr m_opsConCtx;

		// Token: 0x0400028F RID: 655
		private IntPtr m_opsSqlCtx;

		// Token: 0x04000290 RID: 656
		private IntPtr m_opsDacCtx;

		// Token: 0x04000291 RID: 657
		internal unsafe OpoSqlValCtx* m_pOpoSqlValCtx;

		// Token: 0x04000292 RID: 658
		private IntPtr m_opsErrCtx;

		// Token: 0x04000293 RID: 659
		private MetaData m_metaData;

		// Token: 0x04000294 RID: 660
		private UTF8CommandText m_utf8CmdText;

		// Token: 0x04000295 RID: 661
		private OracleConnection m_connection;

		// Token: 0x04000296 RID: 662
		private OracleParameterCollection m_parameters;

		// Token: 0x04000297 RID: 663
		private UpdateRowSource m_updatedRowSource;

		// Token: 0x04000298 RID: 664
		private string m_commandText;

		// Token: 0x04000299 RID: 665
		private string m_pooledCmdText;

		// Token: 0x0400029A RID: 666
		private CommandType m_commandType;

		// Token: 0x0400029B RID: 667
		internal bool m_disposed;

		// Token: 0x0400029C RID: 668
		private bool m_addRowid;

		// Token: 0x0400029D RID: 669
		private int m_rowsAffected;

		// Token: 0x0400029E RID: 670
		private long m_fetchSize;

		// Token: 0x0400029F RID: 671
		private bool m_bFetchSizePropertySet;

		// Token: 0x040002A0 RID: 672
		internal int m_initialLongFS;

		// Token: 0x040002A1 RID: 673
		internal int m_initialLobFS;

		// Token: 0x040002A2 RID: 674
		internal int m_userLongFS;

		// Token: 0x040002A3 RID: 675
		internal int m_userLobFS;

		// Token: 0x040002A4 RID: 676
		private bool m_executeScalar;

		// Token: 0x040002A5 RID: 677
		private bool m_bindByName;

		// Token: 0x040002A6 RID: 678
		private int m_arrayBindCount;

		// Token: 0x040002A7 RID: 679
		private bool m_parsed;

		// Token: 0x040002A8 RID: 680
		private bool m_addParam;

		// Token: 0x040002A9 RID: 681
		private OracleDataReader m_cachedReader;

		// Token: 0x040002AA RID: 682
		internal Hashtable m_safeMapping;

		// Token: 0x040002AB RID: 683
		internal bool m_modified;

		// Token: 0x040002AC RID: 684
		private int m_conSignature;

		// Token: 0x040002AD RID: 685
		private bool m_selectStmt;

		// Token: 0x040002AE RID: 686
		private bool m_cmdTxtModified;

		// Token: 0x040002AF RID: 687
		private OracleXmlCommandType m_xmlCommandType;

		// Token: 0x040002B0 RID: 688
		private OracleXmlQueryProperties m_xmlQueryProperties;

		// Token: 0x040002B1 RID: 689
		private OracleXmlSaveProperties m_xmlSaveProperties;

		// Token: 0x040002B2 RID: 690
		internal bool m_addToStatementCache;

		// Token: 0x040002B3 RID: 691
		private int m_commandTimeout;

		// Token: 0x040002B4 RID: 692
		internal OracleNotificationRequest m_NTFNReq;

		// Token: 0x040002B5 RID: 693
		internal bool m_NTFNAutoEnlist;

		// Token: 0x040002B6 RID: 694
		internal bool m_designTimeVisible;

		// Token: 0x040002B7 RID: 695
		internal bool m_localParse;

		// Token: 0x040002B8 RID: 696
		private bool m_addToStmtCache;

		// Token: 0x040002B9 RID: 697
		private unsafe OpoPrmCtx* m_pOpoPrmCtx;

		// Token: 0x040002BA RID: 698
		internal bool m_returnPSTypes;

		// Token: 0x040002BB RID: 699
		private PrimitiveType[] m_expectedColumnTypes;

		// Token: 0x040002BC RID: 700
		internal bool m_isFromEF;
	}
}
