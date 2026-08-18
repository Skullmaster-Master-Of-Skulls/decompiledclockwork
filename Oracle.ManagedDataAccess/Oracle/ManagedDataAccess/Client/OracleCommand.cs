using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Xml;
using Oracle.ManagedDataAccess.Types;
using Oracle.SqlAndPlsqlParser.LocalParsing;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000052 RID: 82
	[ToolboxBitmap(typeof(resfinder), "Oracle.ManagedDataAccess.src.Client.Icons.OracleCommandToolBox_hc.bmp")]
	[Designer("Oracle.VsDevTools.OracleVSGCommandDesigner, Oracle.VsDevTools, Version=4.122.1.0, Culture=neutral, PublicKeyToken=89b483f429c47342, processorArchitecture=X86", typeof(IDesigner))]
	[SecurityPermission(SecurityAction.Assert, ControlThread = true)]
	public sealed class OracleCommand : DbCommand, ICloneable
	{
		// Token: 0x06000325 RID: 805 RVA: 0x000160CC File Offset: 0x000142CC
		public OracleCommand()
		{
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00016128 File Offset: 0x00014328
		public OracleCommand(string cmdText)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			this.m_commandText = cmdText;
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000161BC File Offset: 0x000143BC
		public OracleCommand(string cmdText, OracleConnection conn)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			this.m_commandText = cmdText;
			this.m_connection = conn;
			this.m_commandImpl = this.GetInitializedCommandImpl();
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00016264 File Offset: 0x00014464
		// (set) Token: 0x06000329 RID: 809 RVA: 0x0001626C File Offset: 0x0001446C
		internal Type[] ExpectedColumnTypes
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

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00016278 File Offset: 0x00014478
		// (set) Token: 0x0600032B RID: 811 RVA: 0x00016280 File Offset: 0x00014480
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("")]
		public bool AddRowid
		{
			get
			{
				return this.m_addRowId;
			}
			set
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (this.m_addRowId != value)
				{
					this.m_addRowId = value;
					this.m_modified = true;
					this.m_cmdTxtModified = true;
					if (this.m_commandImpl != null)
					{
						this.m_commandImpl.m_addRowid = this.m_addRowId;
					}
				}
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600032C RID: 812 RVA: 0x000162E0 File Offset: 0x000144E0
		// (set) Token: 0x0600032D RID: 813 RVA: 0x000162E8 File Offset: 0x000144E8
		[Category("Behavior")]
		[Description("")]
		[DefaultValue(true)]
		public bool AddToStatementCache
		{
			get
			{
				return this.m_addToStatementCache;
			}
			set
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				this.m_addToStatementCache = value;
				if (this.m_commandImpl != null)
				{
					this.m_commandImpl.m_addToStatementCache = this.m_addToStatementCache;
				}
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00016324 File Offset: 0x00014524
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0001632C File Offset: 0x0001452C
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
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (this.m_arrayBindCount != value)
				{
					if (value < 0)
					{
						throw new ArgumentException();
					}
					this.m_arrayBindCount = value;
					this.m_modified = true;
					if (this.m_commandImpl != null)
					{
						this.m_commandImpl.m_arrayBindCount = this.m_arrayBindCount;
					}
				}
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0001638C File Offset: 0x0001458C
		[Browsable(false)]
		[DefaultValue(null)]
		public long[] ArrayBindRowsAffected
		{
			get
			{
				long[] result = null;
				if (this.m_commandImpl != null)
				{
					result = this.m_commandImpl.m_rowsAffectedPerBind;
				}
				return result;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000331 RID: 817 RVA: 0x000163B0 File Offset: 0x000145B0
		// (set) Token: 0x06000332 RID: 818 RVA: 0x000163B8 File Offset: 0x000145B8
		[Description("")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool BindByName
		{
			get
			{
				return this.m_bBindByName;
			}
			set
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (this.m_bBindByName != value)
				{
					this.m_bBindByName = value;
					this.m_modified = true;
					if (this.m_commandImpl != null)
					{
						this.m_commandImpl.m_bBindByName = this.m_bBindByName;
					}
				}
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00016410 File Offset: 0x00014610
		// (set) Token: 0x06000334 RID: 820 RVA: 0x00016418 File Offset: 0x00014618
		[Description("")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool UseEdmMapping
		{
			get
			{
				return this.m_isFromEF;
			}
			set
			{
				this.m_isFromEF = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00016424 File Offset: 0x00014624
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0001643C File Offset: 0x0001463C
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
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (this.m_commandText != value)
				{
					this.m_commandText = value;
					this.m_cmdTxtModified = true;
				}
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00016474 File Offset: 0x00014674
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0001647C File Offset: 0x0001467C
		[DefaultValue(0)]
		[Browsable(false)]
		public override int CommandTimeout
		{
			get
			{
				return this.m_commandTimeout;
			}
			set
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (value < 0 || value > 2147483647)
				{
					throw new ArgumentException();
				}
				this.m_commandTimeout = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000339 RID: 825 RVA: 0x000164B0 File Offset: 0x000146B0
		// (set) Token: 0x0600033A RID: 826 RVA: 0x000164B8 File Offset: 0x000146B8
		[DefaultValue(CommandType.Text)]
		[Description("")]
		[Category("Data")]
		public override CommandType CommandType
		{
			get
			{
				return this.m_commandType;
			}
			set
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (this.m_commandType != value)
				{
					if (value != CommandType.Text && value != CommandType.StoredProcedure && value != CommandType.TableDirect)
					{
						throw new ArgumentException();
					}
					this.m_commandType = value;
					this.m_cmdTxtModified = true;
				}
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0001650C File Offset: 0x0001470C
		[Category("Data")]
		[DefaultValue(null)]
		[Description("")]
		public OracleRefCursor[] ImplicitRefCursors
		{
			get
			{
				if (this.m_commandImpl.m_implicitRSList != null)
				{
					this.m_implicitRefCursors = new OracleRefCursor[this.m_commandImpl.m_implicitRSList.Count];
					for (int i = 0; i < this.m_commandImpl.m_implicitRSList.Count; i++)
					{
						OracleRefCursorImpl refCursorImpl = new OracleRefCursorImpl(this.m_commandImpl.m_implicitRSList[i]);
						OracleRefCursor oracleRefCursor = new OracleRefCursor(this.m_connection, refCursorImpl, this.m_commandImpl.m_sessionTimeZone, this.m_commandText, i.ToString(), (long)this.m_initialLongFS, (long)this.m_clientInitialLOBFS, 0L, null, true);
						this.m_implicitRefCursors[i] = oracleRefCursor;
					}
					this.m_commandImpl.m_implicitRSList = null;
				}
				return this.m_implicitRefCursors;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600033C RID: 828 RVA: 0x000165C8 File Offset: 0x000147C8
		// (set) Token: 0x0600033D RID: 829 RVA: 0x000165D0 File Offset: 0x000147D0
		[Description("")]
		[Category("Data")]
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
					this.m_modified = true;
				}
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600033E RID: 830 RVA: 0x000165EC File Offset: 0x000147EC
		// (set) Token: 0x0600033F RID: 831 RVA: 0x00016608 File Offset: 0x00014808
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

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00016624 File Offset: 0x00014824
		// (set) Token: 0x06000341 RID: 833 RVA: 0x00016640 File Offset: 0x00014840
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

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0001665C File Offset: 0x0001485C
		// (set) Token: 0x06000343 RID: 835 RVA: 0x00016664 File Offset: 0x00014864
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
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (this.m_connection != value)
				{
					this.m_implicitRefCursors = null;
					if (this.m_commandImpl != null && this.m_commandImpl.m_implicitRSList != null && this.m_connection != null && this.m_connection.m_oracleConnectionImpl != null)
					{
						for (int i = 0; i < this.m_commandImpl.m_implicitRSList.Count; i++)
						{
							this.m_connection.m_oracleConnectionImpl.AddCursorIdToBeClosed((long)this.m_commandImpl.m_implicitRSList[i].CursorId);
						}
						this.m_commandImpl.m_implicitRSList = null;
					}
					if (this.m_connection != null && this.m_connection.m_oracleConnectionImpl != null && this.m_commandImpl != null && this.m_commandImpl.m_bPooled)
					{
						this.m_connection.m_oracleConnectionImpl.m_preferredCommandImplTaken = false;
					}
					this.m_commandImpl = null;
				}
				this.m_connection = value;
				if (this.m_commandImpl == null)
				{
					this.m_commandImpl = this.GetInitializedCommandImpl();
				}
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00016774 File Offset: 0x00014974
		// (set) Token: 0x06000345 RID: 837 RVA: 0x0001677C File Offset: 0x0001497C
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

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0001678C File Offset: 0x0001498C
		// (set) Token: 0x06000347 RID: 839 RVA: 0x000167B0 File Offset: 0x000149B0
		protected override DbTransaction DbTransaction
		{
			get
			{
				if (this.m_connection == null || this.m_connection.m_oraTransaction == null)
				{
					return null;
				}
				return this.m_connection.m_oraTransaction;
			}
			set
			{
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000348 RID: 840 RVA: 0x000167B4 File Offset: 0x000149B4
		// (set) Token: 0x06000349 RID: 841 RVA: 0x000167E4 File Offset: 0x000149E4
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

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600034A RID: 842 RVA: 0x000167E8 File Offset: 0x000149E8
		// (set) Token: 0x0600034B RID: 843 RVA: 0x000167F0 File Offset: 0x000149F0
		[DefaultValue(true)]
		[DesignOnly(true)]
		[Browsable(false)]
		public override bool DesignTimeVisible
		{
			get
			{
				return this.m_designTimeVisible;
			}
			set
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				this.m_designTimeVisible = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00016814 File Offset: 0x00014A14
		// (set) Token: 0x0600034D RID: 845 RVA: 0x0001681C File Offset: 0x00014A1C
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
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (this.m_fetchSize != value)
				{
					if (value <= 0L)
					{
						throw new ArgumentException();
					}
					this.m_fetchSize = value;
					if (this.m_commandImpl != null)
					{
						this.m_commandImpl.m_fetchSize = this.m_fetchSize;
					}
				}
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00016878 File Offset: 0x00014A78
		[DefaultValue(0)]
		[Browsable(false)]
		public long RowSize
		{
			get
			{
				long result = 0L;
				if (this.m_commandImpl != null && this.m_commandImpl.m_sqlMetaData != null)
				{
					result = (long)(this.m_commandImpl.m_sqlMetaData.m_maxRowSize + this.m_commandImpl.m_sqlMetaData.m_numOfLOBColumns * Math.Max(86, 86 + this.m_clientInitialLOBFS) + this.m_commandImpl.m_sqlMetaData.m_numOfLONGColumns * Math.Max(2, this.m_initialLongFS) + this.m_commandImpl.m_sqlMetaData.m_numOfBFileColumns * 86);
				}
				return result;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00016904 File Offset: 0x00014B04
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0001690C File Offset: 0x00014B0C
		[DefaultValue(0)]
		[Description("")]
		public int InitialLOBFetchSize
		{
			get
			{
				return this.m_clientInitialLOBFS;
			}
			set
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (this.m_clientInitialLOBFS != value)
				{
					if (value < -1)
					{
						throw new ArgumentException();
					}
					this.m_clientInitialLOBFS = value;
					this.m_modified = true;
				}
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00016948 File Offset: 0x00014B48
		// (set) Token: 0x06000352 RID: 850 RVA: 0x00016950 File Offset: 0x00014B50
		[Description("")]
		[DefaultValue(0)]
		public int InitialLONGFetchSize
		{
			get
			{
				return this.m_initialLongFS;
			}
			set
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (this.m_initialLongFS != value)
				{
					if (value < -1)
					{
						throw new ArgumentException();
					}
					this.m_initialLongFS = value;
					this.m_modified = true;
				}
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0001698C File Offset: 0x00014B8C
		// (set) Token: 0x06000354 RID: 852 RVA: 0x00016994 File Offset: 0x00014B94
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				return this.m_updatedRowSource;
			}
			set
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
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

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000355 RID: 853 RVA: 0x000169E8 File Offset: 0x00014BE8
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

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00016A04 File Offset: 0x00014C04
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("")]
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

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00016A20 File Offset: 0x00014C20
		// (set) Token: 0x06000358 RID: 856 RVA: 0x00016A28 File Offset: 0x00014C28
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
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				this.m_NTFNReq = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00016A4C File Offset: 0x00014C4C
		// (set) Token: 0x0600035A RID: 858 RVA: 0x00016A54 File Offset: 0x00014C54
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
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				this.m_NTFNAutoEnlist = value;
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00016A78 File Offset: 0x00014C78
		public override void Cancel()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.ValidateStatePriorToExecution();
				if (this.m_bExecuteInProgress)
				{
					this.m_commandImpl.Cancel(this.m_connection.m_oracleConnectionImpl, this.m_commandImpl.m_executionId);
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

		// Token: 0x0600035C RID: 860 RVA: 0x00016B18 File Offset: 0x00014D18
		public object Clone()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleCommand oracleCommand = null;
			try
			{
				oracleCommand = new OracleCommand();
				oracleCommand.m_connection = this.m_connection;
				oracleCommand.m_updatedRowSource = this.m_updatedRowSource;
				oracleCommand.m_commandText = this.m_commandText;
				oracleCommand.m_pooledCmdText = this.m_pooledCmdText;
				oracleCommand.m_addRowId = this.m_addRowId;
				oracleCommand.m_addToStatementCache = this.m_addToStatementCache;
				oracleCommand.m_arrayBindCount = this.m_arrayBindCount;
				oracleCommand.m_bBindByName = this.m_bBindByName;
				oracleCommand.m_fetchSize = this.m_fetchSize;
				oracleCommand.m_commandImpl = new OracleCommandImpl();
				if (this.m_commandImpl != null)
				{
					oracleCommand.m_commandImpl.Copy(this.m_commandImpl);
				}
				oracleCommand.m_commandType = this.m_commandType;
				oracleCommand.m_rowsAffected = this.m_rowsAffected;
				oracleCommand.m_initialLongFS = this.m_initialLongFS;
				oracleCommand.m_clientInitialLOBFS = this.m_clientInitialLOBFS;
				oracleCommand.m_modified = this.m_modified;
				oracleCommand.m_cmdTxtModified = this.m_cmdTxtModified;
				oracleCommand.CommandTimeout = this.m_commandTimeout;
				oracleCommand.m_designTimeVisible = this.m_designTimeVisible;
				oracleCommand.m_isFromEF = this.m_isFromEF;
				oracleCommand.m_expectedColumnTypes = this.m_expectedColumnTypes;
				if (this.m_parameters != null)
				{
					oracleCommand.m_parameters = new OracleParameterCollection();
					foreach (object obj in this.m_parameters)
					{
						OracleParameter oracleParameter = (OracleParameter)obj;
						oracleCommand.m_parameters.Add(oracleParameter.Clone());
					}
				}
				oracleCommand.m_cachedReader = this.m_cachedReader;
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
			return oracleCommand;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00016D30 File Offset: 0x00014F30
		protected override DbParameter CreateDbParameter()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			DbParameter result;
			try
			{
				OracleParameter oracleParameter = new OracleParameter();
				result = oracleParameter;
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

		// Token: 0x0600035E RID: 862 RVA: 0x00016DA8 File Offset: 0x00014FA8
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			DbDataReader result;
			try
			{
				OracleDataReader oracleDataReader = this.ExecuteReader(true, false, behavior);
				oracleDataReader.m_returnPSTypes = this.m_returnPSTypes;
				result = oracleDataReader;
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

		// Token: 0x0600035F RID: 863 RVA: 0x00016E30 File Offset: 0x00015030
		public new OracleParameter CreateParameter()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleParameter result;
			try
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				result = new OracleParameter();
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

		// Token: 0x06000360 RID: 864 RVA: 0x00016EBC File Offset: 0x000150BC
		protected override void Dispose(bool disposing)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!this.m_disposed)
				{
					try
					{
						if (this.m_commandImpl != null && this.m_commandImpl.m_implicitRSList != null && this.m_connection != null && this.m_connection.m_oracleConnectionImpl != null)
						{
							this.m_commandImpl.CloseImplicitRefCursors(this.m_connection.m_oracleConnectionImpl);
						}
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
							}
							catch
							{
							}
						}
						if (this.m_commandImpl != null && this.m_commandImpl.m_bPooled && this.m_connection != null && this.m_connection.m_oracleConnectionImpl != null)
						{
							this.m_connection.m_oracleConnectionImpl.m_preferredCommandImplTaken = false;
						}
					}
					catch
					{
					}
					finally
					{
						this.m_modified = true;
						this.m_disposed = true;
						this.m_commandImpl = null;
						try
						{
							base.Dispose(disposing);
						}
						catch
						{
						}
						GC.SuppressFinalize(this);
					}
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

		// Token: 0x06000361 RID: 865 RVA: 0x00017034 File Offset: 0x00015234
		private void BuildCommandText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_cmdTxtModified)
				{
					ConfigBaseClass.StoredProcedureInfo storedProcInfo = ConfigBaseClass.GetInstance(true).GetStoredProcInfo(this.m_commandText.Trim());
					if (storedProcInfo != null && storedProcInfo.m_refCursors.Count > 0)
					{
						for (int i = 0; i < storedProcInfo.m_refCursors.Count; i++)
						{
							this.AddRefCursorParamToParamColl(storedProcInfo.m_refCursors[i]);
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
					if (!this.m_commandImpl.m_bBindByName)
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

		// Token: 0x06000362 RID: 866 RVA: 0x000174CC File Offset: 0x000156CC
		private OracleParameter GetReturnValueParam()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_commandType != CommandType.StoredProcedure)
				{
					return null;
				}
				int count = this.m_parameters.Count;
				for (int i = 0; i < count; i++)
				{
					if (this.m_parameters[i].Direction == ParameterDirection.ReturnValue)
					{
						return this.m_parameters[i];
					}
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
			return null;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00017588 File Offset: 0x00015788
		private void DoPreExecuteProcessing(OracleDependencyImpl orclDependencyImpl, bool bXmlQuerySave = false)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_connection == null)
				{
					throw new InvalidOperationException();
				}
				if (ConnectionState.Open != this.m_connection.m_connectionState)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
				}
				if (this.m_cmdTxtModified && (this.m_commandText == null || this.m_commandText.Length == 0))
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_INVALID_VALUE, new string[]
					{
						"OracleCommand.CommandText"
					}));
				}
				bool flag = orclDependencyImpl != null && !orclDependencyImpl.m_bIsRegistered;
				if (!bXmlQuerySave && (this.m_cmdTxtModified || this.m_commandType == CommandType.StoredProcedure))
				{
					if (this.m_commandType == CommandType.Text)
					{
						this.m_pooledCmdText = this.m_commandText;
					}
					else if (this.m_commandType == CommandType.TableDirect)
					{
						this.m_pooledCmdText = "Select * from " + this.m_commandText;
					}
					else if (this.m_commandType == CommandType.StoredProcedure)
					{
						this.BuildCommandText();
					}
					this.m_commandImpl.m_addRowidDoneImplicitly = (this.m_commandImpl.m_foundExplicitRowidInSql = false);
					if (this.m_commandImpl.m_addRowid || flag)
					{
						this.m_commandImpl.m_addRowidDoneImplicitly = SQLParser.DoSqlLocalProcessing(ref this.m_pooledCmdText, this.m_commandImpl.m_addRowid, out this.m_commandImpl.m_foundExplicitRowidInSql, this.m_connection.m_oracleConnectionImpl, this.m_connection);
					}
					this.m_cmdTxtModified = false;
				}
				if (flag)
				{
					bool flag2 = this.m_commandImpl.m_addRowid || this.m_commandImpl.m_foundExplicitRowidInSql || orclDependencyImpl.m_bIncludeRowId;
					OracleNotificationManager.RegisterForChangeNotification(this.m_connection.m_oracleConnectionImpl, orclDependencyImpl, flag2 && !orclDependencyImpl.m_bExcludeRowId);
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

		// Token: 0x06000364 RID: 868 RVA: 0x000177AC File Offset: 0x000159AC
		public override int ExecuteNonQuery()
		{
			int result = 0;
			OracleException ex = null;
			OracleLogicalTransaction oracleLogicalTransaction = null;
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.ValidateStatePriorToExecution();
				this.m_commandImpl.m_cancelExecutionEvent.Reset();
				this.m_commandImpl.m_continueCancel.Reset();
				this.m_commandImpl.m_bServerExecutionComplete = false;
				this.m_bExecuteInProgress = true;
				OracleParameterCollection oracleParameterCollection = null;
				bool flag = true;
				Timer timer = null;
				long[] scnFromExecution;
				try
				{
					if (this.m_xmlCommandType != OracleXmlCommandType.None)
					{
						if (OracleXmlCommandType.Query == this.m_xmlCommandType)
						{
							this.ExecuteXmlQuery(false);
							return -1;
						}
						return this.ExecuteXmlSave();
					}
					else
					{
						if (this.m_commandTimeout > 0)
						{
							timer = this.SetupCommandTimeoutCallback();
						}
						OracleDependencyImpl oracleDependencyImpl = null;
						if (this.m_NTFNAutoEnlist && this.m_NTFNReq != null)
						{
							this.PopulateSubscriptionInfo(out oracleDependencyImpl);
						}
						this.DoPreExecuteProcessing(oracleDependencyImpl, false);
						result = this.m_commandImpl.ExecuteNonQuery(this.m_pooledCmdText, this.m_parameters, this.m_commandType, this.m_connection.m_oracleConnectionImpl, this.m_initialLongFS, (long)this.m_clientInitialLOBFS, oracleDependencyImpl, out scnFromExecution, out oracleParameterCollection, ref flag, out ex, this.m_connection, ref oracleLogicalTransaction, this.m_isFromEF);
						this.m_connection.CheckForWarnings(this);
						if (oracleDependencyImpl != null && !oracleDependencyImpl.m_regList.Contains(this.m_commandText))
						{
							oracleDependencyImpl.m_regList.Add(this.m_commandText);
						}
					}
				}
				finally
				{
					this.m_bExecuteInProgress = false;
					if (timer != null)
					{
						timer.Change(-1L, -1L);
						timer.Dispose();
					}
				}
				this.m_cmdTxtModified = false;
				if (flag && this.m_parameters != null && this.m_parameters.Count > 0)
				{
					this.m_commandImpl.ExtractAccessorValuesIntoParam(oracleParameterCollection, this.m_connection, oracleParameterCollection.Count, this.m_commandText, (long)this.m_initialLongFS, (long)this.m_clientInitialLOBFS, 0L, scnFromExecution, false);
				}
				if (ex != null)
				{
					if (oracleParameterCollection != null)
					{
						foreach (object obj in oracleParameterCollection)
						{
							OracleParameter oracleParameter = (OracleParameter)obj;
							oracleParameter.PreBindFree();
						}
					}
					throw ex;
				}
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex2, oracleLogicalTransaction);
				if (!(ex2 is OracleException))
				{
					throw;
				}
				if (((OracleException)ex2).OracleLogicalTransaction == null || !(((OracleException)ex2).OracleLogicalTransaction.UserCallCompleted == true) || !(((OracleException)ex2).OracleLogicalTransaction.Committed == true))
				{
					throw;
				}
			}
			finally
			{
				if (this.m_commandImpl != null)
				{
					this.m_commandImpl.m_bindAccessors = null;
				}
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00017AD8 File Offset: 0x00015CD8
		internal SQLMetaData DoDescribeSelectQuery(out int hiddenColumnCount)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			SQLMetaData result;
			try
			{
				hiddenColumnCount = 0;
				this.ValidateStatePriorToExecution();
				this.DoPreExecuteProcessing(null, false);
				SQLMetaData sqlmetaData = null;
				this.m_commandImpl.RetrieveMetadata(this.m_pooledCmdText, this.m_commandType, this.m_parameters, this.m_connection.m_oracleConnectionImpl, this.m_connection, out sqlmetaData, out hiddenColumnCount);
				result = sqlmetaData;
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

		// Token: 0x06000366 RID: 870 RVA: 0x00017B8C File Offset: 0x00015D8C
		public new OracleDataReader ExecuteReader()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDataReader result;
			try
			{
				result = this.ExecuteReader(true, false, CommandBehavior.Default);
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

		// Token: 0x06000367 RID: 871 RVA: 0x00017C04 File Offset: 0x00015E04
		public new OracleDataReader ExecuteReader(CommandBehavior behavior)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDataReader result;
			try
			{
				result = this.ExecuteReader(true, false, behavior);
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

		// Token: 0x06000368 RID: 872 RVA: 0x00017C7C File Offset: 0x00015E7C
		internal OracleDataReader ExecuteReader(bool requery, bool fillRequest, CommandBehavior behavior)
		{
			OracleDataReader oracleDataReader = null;
			OracleException ex = null;
			OracleLogicalTransaction oracleLogicalTransaction = null;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_isFromEF && this.m_connection.m_majorVersion < 12 && this.m_commandText.Contains(" APPLY "))
				{
					throw new Exception(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_NOT_SUPPORTED, new string[]
					{
						"Oracle " + this.m_connection.ServerVersion.ToString(),
						"APPLY"
					}));
				}
				OracleDataReaderImpl oracleDataReaderImpl = null;
				int recordsAffected = 0;
				OracleParameterCollection oracleParameterCollection = null;
				bool flag = true;
				long[] array = null;
				long internalInitialLOBFS = 0L;
				bool flag2 = true;
				bool? flag3 = new bool?(false);
				string commandText = this.m_commandText;
				Timer timer = null;
				IEnumerable<OracleLpStatement> adrianParsedStmt = null;
				bool flag4 = false;
				if (this.m_xmlCommandType != OracleXmlCommandType.None)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_NOT_SUPPORTED, new string[]
					{
						"ExecuteReader",
						"XmlCommandType"
					}));
				}
				try
				{
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
						if (this.m_connection != null && this.m_connection.m_connectionState == ConnectionState.Closed)
						{
							this.m_connection.Open();
							behavior |= CommandBehavior.CloseConnection;
						}
					}
					this.ValidateStatePriorToExecution();
					this.m_commandImpl.m_cancelExecutionEvent.Reset();
					this.m_commandImpl.m_continueCancel.Reset();
					this.m_commandImpl.m_bServerExecutionComplete = false;
					this.m_bExecuteInProgress = true;
					if (this.m_commandTimeout > 0)
					{
						timer = this.SetupCommandTimeoutCallback();
					}
					OracleDependencyImpl oracleDependencyImpl = null;
					if (this.m_NTFNAutoEnlist && this.m_NTFNReq != null)
					{
						this.PopulateSubscriptionInfo(out oracleDependencyImpl);
					}
					this.DoPreExecuteProcessing(oracleDependencyImpl, false);
					if ((behavior & CommandBehavior.SchemaOnly) == CommandBehavior.SchemaOnly)
					{
						bool? flag5 = null;
						SQLInfo sqlinfo = null;
						SQLMetaData sqlmetaData = null;
						this.m_commandImpl.m_sqlStatementType = SqlStatementType.OTHERS;
						if (this.m_commandType == CommandType.StoredProcedure)
						{
							this.m_commandImpl.m_sqlStatementType = SqlStatementType.PLSQL;
						}
						else if (this.m_commandType == CommandType.TableDirect)
						{
							this.m_commandImpl.m_sqlStatementType = SqlStatementType.SELECT;
						}
						else
						{
							OracleCommandImpl.TrimCommentsFromSQL(ref commandText);
							if (!ConfigBaseClass.m_bUseLegacyLocalParser && !flag4)
							{
								if (this.m_connection.m_oracleConnectionImpl.m_statementCache != null && this.m_connection.m_oracleConnectionImpl.m_statementCache.PeekForSQLMetaInfo(this.m_pooledCmdText, out sqlinfo, out sqlmetaData) && sqlmetaData.bGotDescribeInfoFromDB)
								{
									flag5 = new bool?(true);
								}
								else
								{
									flag5 = new bool?(false);
								}
								if (flag5 == true && sqlmetaData.parsedStmt != null)
								{
									adrianParsedStmt = sqlmetaData.parsedStmt;
								}
								this.m_commandImpl.m_sqlStatementType = OracleCommandImpl.GetSqlStatementTypeAdrianParser(this.m_connection, commandText, ref adrianParsedStmt, ref flag3, ref flag4);
							}
							if (ConfigBaseClass.m_bUseLegacyLocalParser || flag4)
							{
								this.m_commandImpl.m_sqlStatementType = OracleCommandImpl.GetSqlStatementType(commandText, ref flag3);
							}
						}
						if (this.m_commandImpl.m_sqlStatementType == SqlStatementType.SELECT)
						{
							oracleDataReaderImpl = this.m_commandImpl.GetReaderImplWithSchemaOnly(this.m_connection.m_oracleConnectionImpl, this.m_commandType, this.m_pooledCmdText, flag5, sqlmetaData);
							flag2 = false;
						}
					}
					if (flag2)
					{
						recordsAffected = this.m_commandImpl.ExecuteReader(this.m_pooledCmdText, this.m_parameters, this.m_commandType, this.m_connection.m_oracleConnectionImpl, ref oracleDataReaderImpl, this.m_initialLongFS, (long)this.m_clientInitialLOBFS, oracleDependencyImpl, null, out array, out oracleParameterCollection, ref flag, ref internalInitialLOBFS, out ex, this.m_connection, ref oracleLogicalTransaction, adrianParsedStmt, (behavior & CommandBehavior.SchemaOnly) == CommandBehavior.SchemaOnly, this.m_isFromEF);
					}
					this.m_connection.CheckForWarnings(this);
					if (oracleDependencyImpl != null && !oracleDependencyImpl.m_regList.Contains(this.m_commandText))
					{
						oracleDependencyImpl.m_regList.Add(this.m_commandText);
					}
				}
				finally
				{
					this.m_bExecuteInProgress = false;
					if (timer != null)
					{
						timer.Change(-1L, -1L);
						timer.Dispose();
					}
				}
				this.m_cmdTxtModified = false;
				if (flag2)
				{
					if (flag && this.m_parameters != null && this.m_parameters.Count > 0)
					{
						this.m_commandImpl.ExtractAccessorValuesIntoParam(oracleParameterCollection, this.m_connection, oracleParameterCollection.Count, this.m_commandText, (long)this.m_initialLongFS, (long)this.m_clientInitialLOBFS, internalInitialLOBFS, array, true);
					}
					if (ex != null)
					{
						if (oracleParameterCollection != null)
						{
							foreach (object obj in oracleParameterCollection)
							{
								OracleParameter oracleParameter = (OracleParameter)obj;
								oracleParameter.PreBindFree();
							}
						}
						throw ex;
					}
				}
				if (oracleDataReaderImpl != null)
				{
					oracleDataReader = new OracleDataReader(oracleDataReaderImpl, this.m_connection, this.m_commandImpl.m_fetchSize, (long)this.m_clientInitialLOBFS, internalInitialLOBFS, this.m_initialLongFS, recordsAffected, this.m_pooledCmdText, this.m_commandImpl.m_sqlStatementType, behavior);
				}
				else
				{
					List<OracleRefCursor> list = null;
					int num = 0;
					int num2 = 0;
					if (flag && this.m_parameters != null && this.m_parameters.Count > 0)
					{
						list = new List<OracleRefCursor>();
						foreach (object obj2 in this.m_parameters)
						{
							OracleParameter oracleParameter2 = (OracleParameter)obj2;
							if (oracleParameter2.Direction != ParameterDirection.Input && oracleParameter2.OracleDbType == OracleDbType.RefCursor)
							{
								OracleRefCursor item = null;
								if (oracleParameter2.Value != null)
								{
									if (this.m_arrayBindCount > 0)
									{
										OracleRefCursor[] array2 = oracleParameter2.Value as OracleRefCursor[];
										for (int i = 0; i < array2.Length; i++)
										{
											if (OracleRefCursor.Null != array2[i] && array2[i] != null)
											{
												item = array2[i];
											}
											list.Add(item);
											num++;
											num2++;
										}
									}
									else
									{
										if (oracleParameter2.Value != DBNull.Value && oracleParameter2.Value != OracleRefCursor.Null)
										{
											item = (oracleParameter2.Value as OracleRefCursor);
										}
										list.Add(item);
										oracleParameter2.Value = DBNull.Value;
										num++;
										num2++;
									}
									oracleParameter2.Value = DBNull.Value;
								}
							}
						}
					}
					if (this.m_commandImpl.m_implicitRSList != null && this.m_commandImpl.m_implicitRSList.Count > 0)
					{
						if (list == null)
						{
							list = new List<OracleRefCursor>();
						}
						for (int j = 0; j < this.m_commandImpl.m_implicitRSList.Count; j++)
						{
							OracleRefCursorImpl refCursorImpl = new OracleRefCursorImpl(this.m_commandImpl.m_implicitRSList[j]);
							OracleRefCursor item2 = new OracleRefCursor(this.m_connection, refCursorImpl, this.m_commandImpl.m_sessionTimeZone, "", num.ToString(), (long)this.m_initialLongFS, (long)this.m_clientInitialLOBFS, 0L, array, true);
							list.Add(item2);
							num++;
						}
						this.m_commandImpl.m_implicitRSList = null;
					}
					if (list != null && list.Count > 0)
					{
						oracleDataReaderImpl = this.m_connection.m_oracleConnectionImpl.GetInitializedDataReaderImpl(list, (long)this.m_initialLongFS, array);
						oracleDataReader = new OracleDataReader(oracleDataReaderImpl, this.m_connection, this.m_commandImpl.m_fetchSize, (long)this.m_clientInitialLOBFS, internalInitialLOBFS, this.m_initialLongFS, recordsAffected, string.Empty, SqlStatementType.PLSQL, behavior);
						if (this.m_commandType == CommandType.StoredProcedure)
						{
							oracleDataReader.m_storedProcName = this.m_commandText;
						}
						oracleDataReader.m_numExplicitBoundRefCursors = num2;
						if (this.m_isFromEF)
						{
							oracleDataReader.m_isFromEF = this.m_isFromEF;
							if (this.m_expectedColumnTypes != null)
							{
								oracleDataReader.m_expectedColumnTypes = this.m_expectedColumnTypes;
							}
							else
							{
								oracleDataReader.GetEdmMappingConfigValues();
								oracleDataReader.PopulateExpectedTypes();
							}
						}
						return oracleDataReader;
					}
					if (oracleDataReader == null)
					{
						oracleDataReader = new OracleDataReader(null, this.m_connection, this.m_commandImpl.m_fetchSize, (long)this.m_clientInitialLOBFS, internalInitialLOBFS, this.m_initialLongFS, recordsAffected, this.m_pooledCmdText, this.m_commandImpl.m_sqlStatementType, behavior);
					}
				}
				if (this.m_isFromEF)
				{
					oracleDataReader.m_isFromEF = this.m_isFromEF;
					if (this.m_expectedColumnTypes != null)
					{
						oracleDataReader.m_expectedColumnTypes = this.m_expectedColumnTypes;
					}
					else
					{
						oracleDataReader.GetEdmMappingConfigValues();
						oracleDataReader.PopulateExpectedTypes();
					}
				}
				if (!requery)
				{
					this.m_cachedReader = oracleDataReader;
				}
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex2, oracleLogicalTransaction);
				if (!(ex2 is OracleException))
				{
					throw;
				}
				if (((OracleException)ex2).OracleLogicalTransaction == null || !(((OracleException)ex2).OracleLogicalTransaction.UserCallCompleted == true) || !(((OracleException)ex2).OracleLogicalTransaction.Committed == true))
				{
					throw;
				}
			}
			finally
			{
				if (this.m_commandImpl != null)
				{
					this.m_commandImpl.m_bindAccessors = null;
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return oracleDataReader;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000185C8 File Offset: 0x000167C8
		public override object ExecuteScalar()
		{
			object result = null;
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.ValidateStatePriorToExecution();
				long fetchSize = this.m_commandImpl.m_fetchSize;
				this.m_commandImpl.m_fetchSize = 1L;
				this.m_pooledCmdText = this.m_commandText;
				OracleDataReader oracleDataReader = this.ExecuteReader();
				this.m_commandImpl.m_fetchSize = fetchSize;
				if (oracleDataReader.Read())
				{
					if (!oracleDataReader.IsDBNull(0))
					{
						result = oracleDataReader.GetValue(0);
					}
					else
					{
						result = DBNull.Value;
					}
				}
				oracleDataReader.Close();
				oracleDataReader.Dispose();
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

		// Token: 0x0600036A RID: 874 RVA: 0x000186A8 File Offset: 0x000168A8
		public Stream ExecuteStream()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			if (this.m_connection == null)
			{
				throw new InvalidOperationException();
			}
			if (this.m_cmdTxtModified && (this.m_commandText == null || this.m_commandText.Length == 0))
			{
				throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_INVALID_VALUE, new string[]
				{
					"OracleCommand.CommandText"
				}));
			}
			if (this.m_xmlCommandType == OracleXmlCommandType.None)
			{
				throw new InvalidOperationException();
			}
			Stream result;
			try
			{
				Stream stream;
				if (OracleXmlCommandType.Query == this.m_xmlCommandType)
				{
					OracleClob oracleClob = this.ExecuteXmlQuery(true);
					stream = oracleClob;
				}
				else
				{
					this.ExecuteXmlSave();
					stream = new OracleClob(this.m_connection);
				}
				result = stream;
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

		// Token: 0x0600036B RID: 875 RVA: 0x000187A0 File Offset: 0x000169A0
		public void ExecuteToStream(Stream outputStream)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			if (this.m_connection == null)
			{
				throw new InvalidOperationException();
			}
			if (this.m_cmdTxtModified && (this.m_commandText == null || this.m_commandText.Length == 0))
			{
				throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_INVALID_VALUE, new string[]
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
			try
			{
				if (OracleXmlCommandType.Query == this.m_xmlCommandType)
				{
					OracleClob oracleClob = this.ExecuteXmlQuery(true);
					long num = oracleClob.Length;
					string fullName = outputStream.GetType().FullName;
					if (fullName.Equals("(Oracle.ManagedDataAccess.Types.OracleClob") && num % 2L == 0L)
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

		// Token: 0x0600036C RID: 876 RVA: 0x00018970 File Offset: 0x00016B70
		public XmlReader ExecuteXmlReader()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			if (this.m_connection == null)
			{
				throw new InvalidOperationException();
			}
			if (this.m_cmdTxtModified && (this.m_commandText == null || this.m_commandText.Length == 0))
			{
				throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_INVALID_VALUE, new string[]
				{
					"OracleCommand.CommandText"
				}));
			}
			if (this.m_xmlCommandType == OracleXmlCommandType.None)
			{
				throw new InvalidOperationException();
			}
			XmlReader result;
			try
			{
				XmlReader xmlReader;
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
					xmlReader = new XmlTextReader(input);
				}
				else
				{
					this.ExecuteXmlSave();
					xmlReader = new XmlTextReader(new StringReader(string.Empty));
				}
				result = xmlReader;
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

		// Token: 0x0600036D RID: 877 RVA: 0x00018AA0 File Offset: 0x00016CA0
		private OracleClob ExecuteXmlQuery(bool wantResult)
		{
			OracleException ex = null;
			OracleLogicalTransaction oracleLogicalTransaction = null;
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleClob result;
			try
			{
				this.ValidateStatePriorToExecution();
				this.m_commandImpl.m_cancelExecutionEvent.Reset();
				this.m_commandImpl.m_continueCancel.Reset();
				this.m_commandImpl.m_bServerExecutionComplete = false;
				this.m_bExecuteInProgress = true;
				OracleParameterCollection oracleParameterCollection = null;
				bool flag = true;
				bool isOracle8i = false;
				bool transform = false;
				int num = 0;
				Timer timer = null;
				long[] scnFromExecution;
				try
				{
					this.m_bExecuteInProgress = true;
					if (this.m_commandTimeout > 0)
					{
						timer = this.SetupCommandTimeoutCallback();
					}
					OracleDependencyImpl oracleDependencyImpl = null;
					if (this.m_NTFNAutoEnlist && this.m_NTFNReq != null)
					{
						this.PopulateSubscriptionInfo(out oracleDependencyImpl);
					}
					this.DoPreExecuteProcessing(oracleDependencyImpl, true);
					this.m_pooledCmdText = this.m_commandText;
					int majorVersion = this.m_connection.m_majorVersion;
					int minorVersion = this.m_connection.m_minorVersion;
					if ((majorVersion == 8 && minorVersion == 1) || (majorVersion == 9 && minorVersion == 0))
					{
						isOracle8i = true;
					}
					this.m_commandImpl.ExecuteXmlQuery(this.m_pooledCmdText, this.m_parameters, this.m_commandType, this.m_xmlCommandType, this.m_connection.m_oracleConnectionImpl, this.m_initialLongFS, (long)this.m_clientInitialLOBFS, oracleDependencyImpl, this.m_connection, out scnFromExecution, out oracleParameterCollection, ref flag, ref this.m_xmlQueryProperties, out ex, out transform, out num, ref oracleLogicalTransaction, false, isOracle8i, wantResult);
					this.m_connection.CheckForWarnings(this);
					if (oracleDependencyImpl != null && !oracleDependencyImpl.m_regList.Contains(this.m_commandText))
					{
						oracleDependencyImpl.m_regList.Add(this.m_commandText);
					}
				}
				finally
				{
					this.m_bExecuteInProgress = false;
					if (timer != null)
					{
						timer.Change(-1L, -1L);
						timer.Dispose();
					}
				}
				this.m_cmdTxtModified = false;
				if (flag && this.m_parameters != null && this.m_parameters.Count > 0)
				{
					this.m_commandImpl.ExtractAccessorValuesIntoParam(oracleParameterCollection, this.m_connection, num, this.m_commandText, (long)this.m_initialLongFS, (long)this.m_clientInitialLOBFS, 0L, scnFromExecution, false);
				}
				if (ex != null)
				{
					if (oracleParameterCollection != null)
					{
						foreach (object obj in oracleParameterCollection)
						{
							OracleParameter oracleParameter = (OracleParameter)obj;
							oracleParameter.PreBindFree();
						}
					}
					throw ex;
				}
				result = this.m_commandImpl.ExtractXMLValuesIntoParam(ref oracleParameterCollection, this.m_connection, num, wantResult, transform, isOracle8i, this.m_xmlQueryProperties);
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex2, oracleLogicalTransaction);
				if (!(ex2 is OracleException))
				{
					throw;
				}
				if (((OracleException)ex2).OracleLogicalTransaction == null || !(((OracleException)ex2).OracleLogicalTransaction.UserCallCompleted == true) || !(((OracleException)ex2).OracleLogicalTransaction.Committed == true))
				{
					throw;
				}
				return null;
			}
			finally
			{
				if (this.m_commandImpl != null)
				{
					this.m_commandImpl.m_bindAccessors = null;
				}
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00018E14 File Offset: 0x00017014
		private int ExecuteXmlSave()
		{
			int result = 0;
			OracleException ex = null;
			OracleLogicalTransaction oracleLogicalTransaction = null;
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.ValidateStatePriorToExecution();
				this.m_commandImpl.m_cancelExecutionEvent.Reset();
				this.m_commandImpl.m_continueCancel.Reset();
				this.m_commandImpl.m_bServerExecutionComplete = false;
				this.m_bExecuteInProgress = true;
				OracleParameterCollection oracleParameterCollection = null;
				bool flag = true;
				Timer timer = null;
				long[] scnFromExecution;
				bool transform;
				try
				{
					if (this.m_commandTimeout > 0)
					{
						timer = this.SetupCommandTimeoutCallback();
					}
					OracleDependencyImpl oracleDependencyImpl = null;
					if (this.m_NTFNAutoEnlist && this.m_NTFNReq != null)
					{
						this.PopulateSubscriptionInfo(out oracleDependencyImpl);
					}
					this.DoPreExecuteProcessing(oracleDependencyImpl, true);
					this.m_pooledCmdText = this.m_commandText;
					result = this.m_commandImpl.ExecuteXmlSave(this.m_pooledCmdText, this.m_parameters, this.m_commandType, this.m_xmlCommandType, this.m_connection.m_oracleConnectionImpl, this.m_initialLongFS, (long)this.m_clientInitialLOBFS, oracleDependencyImpl, this.m_connection, out scnFromExecution, out oracleParameterCollection, ref flag, ref this.m_xmlSaveProperties, out ex, out transform, ref oracleLogicalTransaction, this.m_isFromEF);
					this.m_connection.CheckForWarnings(this);
					if (oracleDependencyImpl != null && !oracleDependencyImpl.m_regList.Contains(this.m_commandText))
					{
						oracleDependencyImpl.m_regList.Add(this.m_commandText);
					}
				}
				finally
				{
					this.m_bExecuteInProgress = false;
					if (timer != null)
					{
						timer.Change(-1L, -1L);
						timer.Dispose();
					}
				}
				this.m_cmdTxtModified = false;
				if (flag && this.m_parameters != null && this.m_parameters.Count > 0)
				{
					this.m_commandImpl.ExtractAccessorValuesIntoParam(oracleParameterCollection, this.m_connection, oracleParameterCollection.Count, this.m_commandText, (long)this.m_initialLongFS, (long)this.m_clientInitialLOBFS, 0L, scnFromExecution, false);
				}
				if (ex != null)
				{
					if (oracleParameterCollection != null)
					{
						foreach (object obj in oracleParameterCollection)
						{
							OracleParameter oracleParameter = (OracleParameter)obj;
							oracleParameter.PreBindFree();
						}
					}
					throw ex;
				}
				result = this.m_commandImpl.ExtractXMLSaveValuesIntoParam(ref oracleParameterCollection, transform);
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex2, oracleLogicalTransaction);
				if (!(ex2 is OracleException))
				{
					throw;
				}
				if (((OracleException)ex2).OracleLogicalTransaction == null || !(((OracleException)ex2).OracleLogicalTransaction.UserCallCompleted == true) || !(((OracleException)ex2).OracleLogicalTransaction.Committed == true))
				{
					throw;
				}
			}
			finally
			{
				if (this.m_commandImpl != null)
				{
					this.m_commandImpl.m_bindAccessors = null;
				}
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00019134 File Offset: 0x00017334
		public override void Prepare()
		{
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00019138 File Offset: 0x00017338
		private Timer SetupCommandTimeoutCallback()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			Timer result;
			try
			{
				TimerCallback callback = new TimerCallback(this.CommandTimeoutCallback);
				long num = (long)this.m_commandTimeout * 1000L;
				if (num > (long)((ulong)-147767296))
				{
					num = (long)((ulong)-147767296);
				}
				result = new Timer(callback, this.m_commandImpl.m_executionId, num, -1L);
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

		// Token: 0x06000371 RID: 881 RVA: 0x000191EC File Offset: 0x000173EC
		private void CommandTimeoutCallback(object state)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				long num = (long)state;
				if (this.m_commandImpl.m_executionId == num)
				{
					this.ValidateStatePriorToExecution();
					if (this.m_bExecuteInProgress)
					{
						this.m_commandImpl.Cancel(this.m_connection.m_oracleConnectionImpl, num);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00019294 File Offset: 0x00017494
		private void PopulateSubscriptionInfo(out OracleDependencyImpl orclDependencyImpl)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				orclDependencyImpl = null;
				OracleDependency oracleDependencyFromNTFNId;
				if ((oracleDependencyFromNTFNId = OracleDependency.GetOracleDependencyFromNTFNId(this.m_NTFNReq.m_id)) != null)
				{
					orclDependencyImpl = oracleDependencyFromNTFNId.m_orclDependencyImpl;
					if (!orclDependencyImpl.m_bIsRegistered)
					{
						if (oracleDependencyFromNTFNId.m_OracleRowidInfo == OracleRowidInfo.Exclude)
						{
							orclDependencyImpl.m_bExcludeRowId = true;
						}
						else if (oracleDependencyFromNTFNId.m_OracleRowidInfo == OracleRowidInfo.Include)
						{
							orclDependencyImpl.m_bIncludeRowId = true;
						}
						orclDependencyImpl.m_bQueryBasedNTFN = false;
						if (oracleDependencyFromNTFNId.m_bQueryBasedNTFN && this.m_connection.m_isDb11gR1OrHigher)
						{
							orclDependencyImpl.m_bQueryBasedNTFN = true;
						}
						oracleDependencyFromNTFNId.SetRegisterInfo(this.m_connection.m_oracleConnectionImpl.m_cs.m_userId, this.m_connection.DataSource, this.m_NTFNReq);
					}
					else if (oracleDependencyFromNTFNId.m_dataSource != this.m_connection.DataSource || oracleDependencyFromNTFNId.m_userName != this.m_connection.m_oracleConnectionImpl.m_cs.m_userId)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_DIFFERENT_CONNECTIONS, new string[0]));
					}
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

		// Token: 0x06000373 RID: 883 RVA: 0x00019410 File Offset: 0x00017610
		private void AddRefCursorParamToParamColl(RefCursorInfo cursorInfo)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
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
				}
				else
				{
					this.m_parameters.Add(oracleParameter);
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

		// Token: 0x06000374 RID: 884 RVA: 0x000194F4 File Offset: 0x000176F4
		private string[] GetPlsqlOutput()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			string[] array = null;
			try
			{
				if (this.m_connection == null)
				{
					throw new InvalidOperationException();
				}
				if (this.m_connection.m_connectionState != ConnectionState.Open)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
				}
				int num;
				if (this.m_connection.m_isDb10gR2OrHigher)
				{
					num = 32767;
				}
				else
				{
					num = 255;
				}
				OracleCommand oracleCommand = new OracleCommand("BEGIN DBMS_OUTPUT.GET_LINES(:1, :2); END;", this.m_connection);
				oracleCommand.CommandType = CommandType.Text;
				OracleParameter oracleParameter = new OracleParameter();
				oracleParameter.DbType = DbType.String;
				oracleParameter.Direction = ParameterDirection.Output;
				oracleParameter.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
				oracleCommand.Parameters.Add(oracleParameter);
				OracleParameter oracleParameter2 = new OracleParameter();
				oracleParameter2.DbType = DbType.Int32;
				oracleParameter2.Direction = ParameterDirection.InputOutput;
				oracleCommand.Parameters.Add(oracleParameter2);
				oracleParameter2.Value = 1024;
				ArrayList arrayList = new ArrayList(32);
				while ((int)oracleParameter2.Value == 1024)
				{
					try
					{
						oracleParameter.Value = null;
						oracleParameter.Size = (int)oracleParameter2.Value;
						oracleParameter.ArrayBindSize = new int[(int)oracleParameter2.Value];
						for (int i = 0; i < (int)oracleParameter2.Value; i++)
						{
							oracleParameter.ArrayBindSize[i] = num;
						}
						oracleCommand.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
						throw;
					}
					arrayList.Add(oracleParameter.Value);
				}
				int num2 = (int)oracleParameter2.Value + (arrayList.Count - 1) * 1024;
				array = new string[num2];
				int j = 0;
				int num3 = 0;
				while (j < arrayList.Count)
				{
					string[] array2 = (string[])arrayList[j];
					int num4;
					if (j == arrayList.Count - 1)
					{
						num4 = (int)oracleParameter2.Value;
					}
					else
					{
						num4 = 1024;
					}
					int k = 0;
					while (k < num4)
					{
						array[num3] = array2[k];
						k++;
						num3++;
					}
					j++;
				}
				oracleCommand.Parameters.Clear();
				oracleCommand.Dispose();
				oracleCommand = null;
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex2, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return array;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000197A4 File Offset: 0x000179A4
		private void ValidateStatePriorToExecution()
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (this.m_connection == null)
			{
				throw new InvalidOperationException();
			}
			if (this.m_connection.m_connectionState != ConnectionState.Open)
			{
				throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
			}
			if (this.m_commandImpl == null)
			{
				this.m_commandImpl = this.GetInitializedCommandImpl();
				if (this.m_commandImpl == null)
				{
					throw new InvalidOperationException();
				}
			}
			this.m_commandImpl.m_arrayBindCount = this.m_arrayBindCount;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00019830 File Offset: 0x00017A30
		private OracleCommandImpl GetInitializedCommandImpl()
		{
			OracleCommandImpl oracleCommandImpl = null;
			if (this.m_connection != null && this.m_connection.m_oracleConnectionImpl != null)
			{
				oracleCommandImpl = this.m_connection.m_oracleConnectionImpl.getCommandImpl();
				oracleCommandImpl.m_addRowid = this.m_addRowId;
				oracleCommandImpl.m_addToStatementCache = this.m_addToStatementCache;
				oracleCommandImpl.m_arrayBindCount = this.m_arrayBindCount;
				oracleCommandImpl.m_bBindByName = this.m_bBindByName;
				oracleCommandImpl.m_fetchSize = this.m_fetchSize;
			}
			return oracleCommandImpl;
		}

		// Token: 0x04000538 RID: 1336
		internal const int m_rowsToFetch = 1024;

		// Token: 0x04000539 RID: 1337
		internal OracleCommandImpl m_commandImpl;

		// Token: 0x0400053A RID: 1338
		private OracleConnection m_connection;

		// Token: 0x0400053B RID: 1339
		private string m_commandText;

		// Token: 0x0400053C RID: 1340
		private string m_pooledCmdText;

		// Token: 0x0400053D RID: 1341
		private CommandType m_commandType = CommandType.Text;

		// Token: 0x0400053E RID: 1342
		private OracleDataReader m_cachedReader;

		// Token: 0x0400053F RID: 1343
		private bool m_cmdTxtModified = true;

		// Token: 0x04000540 RID: 1344
		private int m_rowsAffected = -1;

		// Token: 0x04000541 RID: 1345
		private bool m_designTimeVisible = true;

		// Token: 0x04000542 RID: 1346
		private int m_commandTimeout;

		// Token: 0x04000543 RID: 1347
		private OracleParameterCollection m_parameters;

		// Token: 0x04000544 RID: 1348
		internal bool m_modified;

		// Token: 0x04000545 RID: 1349
		internal bool m_disposed;

		// Token: 0x04000546 RID: 1350
		internal int m_initialLongFS;

		// Token: 0x04000547 RID: 1351
		internal int m_clientInitialLOBFS;

		// Token: 0x04000548 RID: 1352
		private UpdateRowSource m_updatedRowSource = UpdateRowSource.Both;

		// Token: 0x04000549 RID: 1353
		private bool m_bExecuteInProgress;

		// Token: 0x0400054A RID: 1354
		internal OracleNotificationRequest m_NTFNReq;

		// Token: 0x0400054B RID: 1355
		internal bool m_NTFNAutoEnlist = true;

		// Token: 0x0400054C RID: 1356
		internal OracleRefCursor[] m_implicitRefCursors;

		// Token: 0x0400054D RID: 1357
		internal bool m_returnPSTypes;

		// Token: 0x0400054E RID: 1358
		private Type[] m_expectedColumnTypes;

		// Token: 0x0400054F RID: 1359
		internal bool m_isFromEF;

		// Token: 0x04000550 RID: 1360
		private bool m_addRowId;

		// Token: 0x04000551 RID: 1361
		private bool m_addToStatementCache = true;

		// Token: 0x04000552 RID: 1362
		private int m_arrayBindCount;

		// Token: 0x04000553 RID: 1363
		private bool m_bBindByName = ConfigBaseClass.m_BindByName;

		// Token: 0x04000554 RID: 1364
		private long m_fetchSize = (long)ConfigBaseClass.m_FetchSize;

		// Token: 0x04000555 RID: 1365
		private OracleXmlCommandType m_xmlCommandType;

		// Token: 0x04000556 RID: 1366
		private OracleXmlQueryProperties m_xmlQueryProperties;

		// Token: 0x04000557 RID: 1367
		private OracleXmlSaveProperties m_xmlSaveProperties;
	}
}
