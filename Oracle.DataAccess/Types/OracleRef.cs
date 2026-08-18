using System;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000043 RID: 67
	public sealed class OracleRef : MarshalByRefObject, IDisposable, ICloneable, INullable
	{
		// Token: 0x06000306 RID: 774 RVA: 0x00024CBD File Offset: 0x00023CBD
		static OracleRef()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00024CD8 File Offset: 0x00023CD8
		public OracleRef(OracleConnection con, string udtTypeName, string objTabName)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleRef::OracleRef(1)\n"
				});
			}
			this.InitRef(OracleUdtDescriptor.GetOracleUdtDescriptor(con, udtTypeName), objTabName);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleRef::OracleRef(1)\n"
				});
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00024D3C File Offset: 0x00023D3C
		internal unsafe void InitRef(OracleUdtDescriptor oraUdtDesc, string objTabName)
		{
			if (oraUdtDesc == null || objTabName == null)
			{
				throw new ArgumentNullException(null, null);
			}
			if (oraUdtDesc.m_connection.m_contextConnection)
			{
				throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_UDT_NOTSUPPORTED_CTX_CONN, new string[0]));
			}
			if (objTabName == "")
			{
				throw new ArgumentException();
			}
			this.Initialize();
			if (oraUdtDesc.GetUdtTypeCode() != OciTypeCode.OBJECT)
			{
				throw new ArgumentException();
			}
			this.m_pOpoObjValCtx->TypeCode = 108;
			oraUdtDesc.GetMetaDataTable();
			this.m_oracleUdtDesc = oraUdtDesc;
			int num = objTabName.LastIndexOf('.');
			if (num != -1)
			{
				this.m_opoObjRefCtx.schemaName = objTabName.Substring(0, num);
				this.m_opoObjRefCtx.objTableName = objTabName.Substring(num + 1);
			}
			else
			{
				this.m_opoObjRefCtx.objTableName = objTabName;
			}
			this.m_connection = this.m_oracleUdtDesc.m_connection;
			this.m_conSignature = this.m_connection.m_conSignature;
			this.m_opsConCtx = this.m_connection.m_opoConCtx.opsConCtx;
			if (this.m_opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			int num2 = 0;
			try
			{
				num2 = OpsCon.AddRef(this.m_opsConCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				this.m_opsConCtx = IntPtr.Zero;
				throw;
			}
			if (num2 <= 1)
			{
				this.m_opsConCtx = IntPtr.Zero;
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			try
			{
				OpsErr.AllocCtx(ref this.m_opsErrCtx, this.m_opsConCtx);
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				throw;
			}
			this.OpoUdtCtx = new OpoUdtCtx(this.m_opsConCtx, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			int num3 = 0;
			try
			{
				num3 = OpsObj.New(this.m_opsConCtx, this.m_opsErrCtx, this.m_oracleUdtDesc.m_opsDscCtx, ref this.m_pOpoObjValCtx, this.m_opoObjRefCtx, ref this.m_opoUdtCtx.m_pUDT, ref this.m_opoUdtCtx.m_pOCIRef, ref this.m_opoUdtCtx.m_pObjInd);
			}
			catch (Exception ex3)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex3);
				}
				num3 = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num3 != 0 && num3 != ErrRes.INT_ERR)
				{
					OracleException.HandleError(num3, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			this.m_opoObjRefCtx.objTableName = this.m_oracleUdtDesc.SchemaName + "." + objTabName;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00024FD8 File Offset: 0x00023FD8
		public OracleRef(OracleConnection con, string hexStr)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleRef::OracleRef(2)\n"
				});
			}
			if (con == null || hexStr == null)
			{
				throw new ArgumentNullException(null, null);
			}
			if (con.m_contextConnection)
			{
				throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_UDT_NOTSUPPORTED_CTX_CONN, new string[0]));
			}
			if (hexStr == "")
			{
				throw new ArgumentException();
			}
			this.Initialize();
			this.m_connection = con;
			this.m_conSignature = this.m_connection.m_conSignature;
			this.m_opsConCtx = this.m_connection.m_opoConCtx.opsConCtx;
			if (this.m_opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			this.m_opoObjRefCtx.hexStr = hexStr;
			int num = 0;
			try
			{
				num = OpsCon.AddRef(this.m_opsConCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				this.m_opsConCtx = IntPtr.Zero;
				throw;
			}
			if (num <= 1)
			{
				this.m_opsConCtx = IntPtr.Zero;
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			this.OpoUdtCtx = new OpoUdtCtx(this.m_opsConCtx, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			try
			{
				OpsErr.AllocCtx(ref this.m_opsErrCtx, this.m_opsConCtx);
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				throw;
			}
			int num2 = 0;
			try
			{
				num2 = OpsObj.New(this.m_opsConCtx, this.m_opsErrCtx, IntPtr.Zero, ref this.m_pOpoObjValCtx, this.m_opoObjRefCtx, ref this.m_opoUdtCtx.m_pUDT, ref this.m_opoUdtCtx.m_pOCIRef, ref this.m_opoUdtCtx.m_pObjInd);
			}
			catch (Exception ex3)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex3);
				}
				num2 = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num2 != 0 && num2 != ErrRes.INT_ERR)
				{
					OracleException.HandleError(num2, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			OpoDscRefCtx opoDscRefCtx = new OpoDscRefCtx();
			try
			{
				num2 = OpsRef.GetTypeName(this.m_opsConCtx, this.m_opsErrCtx, ref this.m_pOpoObjValCtx, ref opoDscRefCtx, ref this.m_opoUdtCtx.m_pUDT, ref this.m_opoUdtCtx.m_pOCIRef, ref this.m_opoUdtCtx.m_pObjInd, ref this.m_opoUdtCtx.m_IsPinned, ref this.m_opoUdtCtx.m_pinLatest);
			}
			catch (Exception ex4)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex4);
				}
				num2 = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num2 != 0 && num2 != ErrRes.INT_ERR)
				{
					OracleException.HandleError(num2, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			this.m_oracleUdtDesc = OracleUdtDescriptor.GetOracleUdtDescriptor(this.m_connection, opoDscRefCtx.SchemaName, opoDscRefCtx.TypeName);
			this.m_oracleUdtDesc.GetMetaDataTable();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleRef::OracleRef(2)\n"
				});
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000252F8 File Offset: 0x000242F8
		private OracleRef()
		{
			this.m_bNotNull = false;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00025310 File Offset: 0x00024310
		internal unsafe OracleRef(OracleUdtDescriptor oraUdtDsc, OpoUdtCtx opoUdtCtx)
		{
			if (oraUdtDsc == null || opoUdtCtx == null)
			{
				throw new ArgumentNullException(null, null);
			}
			if (oraUdtDsc.m_connection.m_contextConnection)
			{
				throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_UDT_NOTSUPPORTED_CTX_CONN, new string[0]));
			}
			this.Initialize();
			if (oraUdtDsc.GetUdtTypeCode() != OciTypeCode.OBJECT)
			{
				throw new ArgumentException();
			}
			this.m_pOpoObjValCtx->TypeCode = 108;
			this.m_oracleUdtDesc = oraUdtDsc;
			this.m_connection = oraUdtDsc.m_connection;
			this.m_opsConCtx = this.m_connection.m_opoConCtx.opsConCtx;
			this.m_conSignature = this.m_connection.m_conSignature;
			if (this.m_opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			this.OpoUdtCtx = opoUdtCtx;
			int num = 0;
			try
			{
				num = OpsCon.AddRef(this.m_opsConCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				this.m_opsConCtx = IntPtr.Zero;
				throw;
			}
			if (num <= 1)
			{
				this.m_opsConCtx = IntPtr.Zero;
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			try
			{
				OpsErr.AllocCtx(ref this.m_opsErrCtx, this.m_opsConCtx);
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

		// Token: 0x0600030C RID: 780 RVA: 0x0002547C File Offset: 0x0002447C
		internal OracleRef(OracleConnection con, OpoUdtCtx opoUdtCtx)
		{
			if (con == null || opoUdtCtx == null)
			{
				throw new ArgumentNullException(null, null);
			}
			if (con.m_contextConnection)
			{
				throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_UDT_NOTSUPPORTED_CTX_CONN, new string[0]));
			}
			this.Initialize();
			this.m_connection = con;
			this.m_conSignature = this.m_connection.m_conSignature;
			this.m_opsConCtx = this.m_connection.m_opoConCtx.opsConCtx;
			if (this.m_opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			this.OpoUdtCtx = opoUdtCtx;
			int num = 0;
			try
			{
				num = OpsCon.AddRef(this.m_opsConCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				this.m_opsConCtx = IntPtr.Zero;
				throw;
			}
			if (num <= 1)
			{
				this.m_opsConCtx = IntPtr.Zero;
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			try
			{
				OpsErr.AllocCtx(ref this.m_opsErrCtx, this.m_opsConCtx);
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

		// Token: 0x0600030D RID: 781 RVA: 0x000255B8 File Offset: 0x000245B8
		~OracleRef()
		{
			this.Dispose(false);
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600030E RID: 782 RVA: 0x000255E8 File Offset: 0x000245E8
		public OracleConnection Connection
		{
			get
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				return this.m_connection;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0002560C File Offset: 0x0002460C
		internal OracleUdtDescriptor UdtDescriptor
		{
			get
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (this.m_oracleUdtDesc == null)
				{
					int num = 0;
					if (this.m_opsConCtx == IntPtr.Zero)
					{
						throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
					}
					if (this.m_connection.m_conSignature != this.m_conSignature)
					{
						throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
					}
					OpoDscRefCtx opoDscRefCtx = new OpoDscRefCtx();
					try
					{
						num = OpsRef.GetTypeName(this.m_opsConCtx, this.m_opsErrCtx, ref this.m_pOpoObjValCtx, ref opoDscRefCtx, ref this.m_opoUdtCtx.m_pUDT, ref this.m_opoUdtCtx.m_pOCIRef, ref this.m_opoUdtCtx.m_pObjInd, ref this.m_opoUdtCtx.m_IsPinned, ref this.m_opoUdtCtx.m_pinLatest);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						num = ErrRes.INT_ERR;
						throw;
					}
					finally
					{
						if (num != 0 && num != ErrRes.INT_ERR)
						{
							OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
						}
					}
					this.m_oracleUdtDesc = OracleUdtDescriptor.GetOracleUdtDescriptor(this.m_connection, opoDscRefCtx.SchemaName, opoDscRefCtx.TypeName);
					this.m_oracleUdtDesc.GetMetaDataTable();
				}
				return this.m_oracleUdtDesc;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0002576C File Offset: 0x0002476C
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00025778 File Offset: 0x00024778
		public string Value
		{
			get
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.m_opoUdtCtx.m_pOCIRef == IntPtr.Zero)
				{
					throw new InvalidOperationException();
				}
				if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
				if (this.m_connection.m_conSignature != this.m_conSignature)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
				}
				if (this.m_opoObjRefCtx.hexStr == null)
				{
					int num = 0;
					try
					{
						num = OpsRef.ToHex(this.m_opsConCtx, this.m_opsErrCtx, this.m_opoUdtCtx.m_pOCIRef, ref this.m_opoObjRefCtx);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						num = ErrRes.INT_ERR;
						throw;
					}
					finally
					{
						if (num != 0 && num != ErrRes.INT_ERR)
						{
							OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
						}
					}
				}
				return this.m_opoObjRefCtx.hexStr;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000312 RID: 786 RVA: 0x000258B8 File Offset: 0x000248B8
		public bool IsLocked
		{
			get
			{
				int num = 0;
				int num2 = 0;
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (!this.m_bNotNull)
				{
					return false;
				}
				if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
				if (this.m_connection.m_conSignature != this.m_conSignature)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
				}
				try
				{
					num2 = OpsRef.IsLocked(this.m_opsConCtx, this.m_opsErrCtx, ref this.m_pOpoObjValCtx, ref this.m_opoUdtCtx.m_pUDT, ref this.m_opoUdtCtx.m_pOCIRef, ref this.m_opoUdtCtx.m_pObjInd, ref num, ref this.m_opoUdtCtx.m_IsPinned, ref this.m_opoUdtCtx.m_pinLatest);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					num2 = ErrRes.INT_ERR;
					throw;
				}
				finally
				{
					if (num2 != 0 && num2 != ErrRes.INT_ERR)
					{
						OracleException.HandleError(num2, this.m_connection, this.m_opsErrCtx, this);
					}
				}
				return num == 1;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000313 RID: 787 RVA: 0x000259F4 File Offset: 0x000249F4
		public string ObjectTableName
		{
			get
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
				if (this.m_connection.m_conSignature != this.m_conSignature)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
				}
				if (this.m_oracleUdtDesc == null)
				{
					this.m_oracleUdtDesc = this.UdtDescriptor;
				}
				if (this.m_opoObjRefCtx.objTableName == null)
				{
					int num = 0;
					try
					{
						num = OpsRef.GetTableName(this.m_opsConCtx, this.m_opsErrCtx, ref this.m_pOpoObjValCtx, ref this.m_opoObjRefCtx, ref this.m_opoUdtCtx.m_pUDT, ref this.m_opoUdtCtx.m_pOCIRef, ref this.m_opoUdtCtx.m_pObjInd, ref this.m_opoUdtCtx.m_IsPinned, ref this.m_opoUdtCtx.m_pinLatest);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						num = ErrRes.INT_ERR;
						throw;
					}
					finally
					{
						if (num != 0 && num != ErrRes.INT_ERR)
						{
							OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
						}
					}
					this.m_opoObjRefCtx.objTableName = this.m_oracleUdtDesc.SchemaName + "." + this.m_opoObjRefCtx.objTableName;
				}
				return this.m_opoObjRefCtx.objTableName;
			}
		}

		// Token: 0x17000073 RID: 115
		// (set) Token: 0x06000314 RID: 788 RVA: 0x00025B8C File Offset: 0x00024B8C
		internal OpoUdtCtx OpoUdtCtx
		{
			set
			{
				if (this.m_opoUdtCtx == null && value == null)
				{
					return;
				}
				if (this.m_opoUdtCtx == value)
				{
					return;
				}
				if (this.m_opoUdtCtx != null)
				{
					this.m_opoUdtCtx.RelRefCount();
				}
				if (value != null)
				{
					this.m_opoUdtCtx = value;
					this.m_opoUdtCtx.AddRefCount();
				}
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00025BCC File Offset: 0x00024BCC
		public bool IsEqual(OracleRef oraRef)
		{
			int num = 0;
			int num2 = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleRef::IsEqual()\n"
				});
			}
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (oraRef == null)
			{
				throw new ArgumentNullException();
			}
			if (!this.m_bNotNull || oraRef.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleRef::IsEqual()\n"
					});
				}
				return !this.m_bNotNull && oraRef.IsNull;
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			try
			{
				num2 = OpsRef.IsEqual(this.m_opsConCtx, this.m_opoUdtCtx.m_pOCIRef, oraRef.m_opoUdtCtx.m_pOCIRef, ref num);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num2 = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num2 != 0 && num2 != ErrRes.INT_ERR)
				{
					OracleException.HandleError(num2, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleRef::IsEqual()\n"
				});
			}
			return num == 1;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00025D5C File Offset: 0x00024D5C
		public object Clone()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleRef::Clone()\n"
				});
			}
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleRef::Clone()\n"
					});
				}
				return OracleRef.Null;
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			if (this.m_oracleUdtDesc == null)
			{
				this.m_oracleUdtDesc = this.UdtDescriptor;
			}
			OracleRef oracleRef = new OracleRef(this.m_oracleUdtDesc, this.m_opoUdtCtx);
			oracleRef.m_bNotNull = this.m_bNotNull;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleRef::Clone()\n"
				});
			}
			return oracleRef;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00025E78 File Offset: 0x00024E78
		public unsafe void Delete(bool bFlush)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleRef::Delete()\n"
				});
			}
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			try
			{
				bool flag = false;
				if (this.m_opoUdtCtx.m_IsPinned == 0)
				{
					flag = true;
				}
				num = OpsRef.MarkDelete(this.m_opsConCtx, this.m_opsErrCtx, ref this.m_opoUdtCtx.m_pUDT, ref this.m_opoUdtCtx.m_pObjInd, ref this.m_opoUdtCtx.m_IsPinned, ref this.m_opoUdtCtx.m_pOCIRef);
				if (num == 0 && flag && this.m_opoUdtCtx.m_IsPinned == 1)
				{
					this.m_objectPinCount++;
				}
				if (num == 0 && bFlush)
				{
					this.m_pOpoObjValCtx->deleteOnFlush = 1;
					bool flag2 = false;
					if (this.m_opoUdtCtx.m_IsPinned == 1)
					{
						flag2 = true;
					}
					num = OpsRef.Flush(this.m_opsConCtx, this.m_opsErrCtx, ref this.m_pOpoObjValCtx, ref this.m_opoUdtCtx.m_pUDT, ref this.m_opoUdtCtx.m_pOCIRef, ref this.m_opoUdtCtx.m_pObjInd, ref this.m_opoUdtCtx.m_IsPinned, ref this.m_opoUdtCtx.m_pinLatest);
					if (num == 0 && flag2)
					{
						this.m_objectPinCount--;
					}
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num != 0 && num != ErrRes.INT_ERR)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleRef::Delete()\n"
				});
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000260B4 File Offset: 0x000250B4
		public void Flush()
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleRef::Flush()\n"
				});
			}
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				return;
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			bool flag = false;
			if (this.m_opoUdtCtx.m_IsPinned == 1)
			{
				flag = true;
			}
			try
			{
				num = OpsRef.Flush(this.m_opsConCtx, this.m_opsErrCtx, ref this.m_pOpoObjValCtx, ref this.m_opoUdtCtx.m_pUDT, ref this.m_opoUdtCtx.m_pOCIRef, ref this.m_opoUdtCtx.m_pObjInd, ref this.m_opoUdtCtx.m_IsPinned, ref this.m_opoUdtCtx.m_pinLatest);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num != 0 && num != ErrRes.INT_ERR)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
				else if (num == 0 && flag)
				{
					this.m_objectPinCount--;
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleRef::Flush()\n"
				});
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0002624C File Offset: 0x0002524C
		public bool Lock(bool wait)
		{
			int num = 0;
			int num2 = 1;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleRef::Lock(0)\n"
				});
			}
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			bool flag = false;
			if (this.m_opoUdtCtx.m_IsPinned == 0)
			{
				flag = true;
			}
			try
			{
				num = OpsRef.Lock(this.m_opsConCtx, this.m_opsErrCtx, wait, ref this.m_opoUdtCtx.m_pUDT, ref this.m_opoUdtCtx.m_pObjInd, ref this.m_opoUdtCtx.m_pOCIRef, ref this.m_opoUdtCtx.m_IsPinned, ref num2);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (flag && this.m_opoUdtCtx.m_IsPinned == 1)
				{
					this.m_objectPinCount++;
				}
				if (num != 0 && num != ErrRes.INT_ERR)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleRef::Lock(0)\n"
				});
			}
			return num2 == 1;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000263EC File Offset: 0x000253EC
		public void Dispose()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleRef::Dispose()\n"
				});
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleRef::Dispose()\n"
				});
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00026440 File Offset: 0x00025440
		private unsafe object CreateCustomObject()
		{
			int num = 0;
			if (this.m_oracleUdtDesc.m_customTypeFactory == null)
			{
				object factory = OracleUdt.GetFactory(this.m_oracleUdtDesc);
				this.m_oracleUdtDesc.DescribeCustomType(factory);
			}
			if ((IntPtr)((void*)this.m_pOpoUdtValCtx) == IntPtr.Zero)
			{
				try
				{
					num = OpsUdt.AllocValCtx(out this.m_pOpoUdtValCtx, 1);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, null);
				}
			}
			this.m_pOpoUdtValCtx->pUDT = this.m_opoUdtCtx.m_pUDT;
			this.m_pOpoUdtValCtx->pNullStruct = this.m_opoUdtCtx.m_pObjInd;
			this.m_pOpoUdtValCtx->pOpsErrCtx = this.m_connection.m_opoConCtx.opsErrCtx;
			this.m_pOpoUdtValCtx->pTDO = this.m_oracleUdtDesc.m_opsDscCtx;
			this.m_pOpoUdtValCtx->pOpoDscValCtx = this.m_oracleUdtDesc.m_pOpoDscValCtx;
			try
			{
				num = OpsUdt.GetObj(this.m_connection.m_opoConCtx.opsConCtx, this.m_pOpoUdtValCtx);
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				throw;
			}
			if (num != 0)
			{
				OracleException.HandleError(num, this.m_connection, this.m_pOpoUdtValCtx->pOpsErrCtx, null);
			}
			object obj = ((IOracleCustomTypeFactory)this.m_oracleUdtDesc.m_customTypeFactory).CreateObject();
			if (obj != null)
			{
				((IOracleCustomType)obj).ToCustomObject(this.m_connection, (IntPtr)((void*)this.m_pOpoUdtValCtx));
			}
			return obj;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000265D4 File Offset: 0x000255D4
		private void UnPinObj()
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleRef::UnPinObj()\n"
				});
			}
			try
			{
				if (this.m_opoUdtCtx.m_IsPinned == 1)
				{
					num = OpsRef.UnPinObj(this.m_opsConCtx, this.m_opsErrCtx, ref this.m_opoUdtCtx.m_pUDT, ref this.m_opoUdtCtx.m_IsPinned);
					if (num == 0)
					{
						this.m_objectPinCount--;
					}
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num != 0 && num != ErrRes.INT_ERR)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleRef::UnPinObj()\n"
				});
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000266BC File Offset: 0x000256BC
		private void PinObj(OracleUdtFetchOption fetchOption, int nDepthLevel)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleRef::GetCustomObject()\n"
				});
			}
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			if (this.m_oracleUdtDesc == null)
			{
				this.m_oracleUdtDesc = this.UdtDescriptor;
			}
			try
			{
				int num2 = (int)fetchOption;
				num = OpsRef.PinObjCOR(this.m_opsConCtx, this.m_opsErrCtx, this.m_opoUdtCtx.m_pOCIRef, ref this.m_opoUdtCtx.m_pUDT, ref this.m_opoUdtCtx.m_pObjInd, ref this.m_complexObjCtx, nDepthLevel, ref num2);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num != 0 && num != ErrRes.INT_ERR)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
				else if (num == 0)
				{
					this.m_objectPinCount++;
				}
			}
			if (fetchOption == OracleUdtFetchOption.Server)
			{
				this.m_opoUdtCtx.m_pinLatest = 1;
			}
			this.m_opoUdtCtx.m_IsPinned = 1;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00026840 File Offset: 0x00025840
		public object GetCustomObject(OracleUdtFetchOption fetchOption)
		{
			return this.GetCustomObject(fetchOption, 0);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0002684C File Offset: 0x0002584C
		public object GetCustomObject(OracleUdtFetchOption fetchOption, int depthLevel)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleRef::GetCustomObject()\n"
				});
			}
			if (fetchOption == OracleUdtFetchOption.Server)
			{
				try
				{
					OpsRef.UnMarkObjectByRef(this.m_opsConCtx, this.m_opsErrCtx, this.m_opoUdtCtx.m_pOCIRef);
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
			this.PinObj(fetchOption, depthLevel);
			object result = this.CreateCustomObject();
			this.UnPinObj();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleRef::GetCustomObject(1)\n"
				});
			}
			return result;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000268EC File Offset: 0x000258EC
		public object GetCustomObjectForUpdate(bool bWait)
		{
			return this.GetCustomObjectForUpdate(bWait, 0);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000268F8 File Offset: 0x000258F8
		public object GetCustomObjectForUpdate(bool bWait, int depthLevel)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleRef::GetCustomObjectForUpdate()\n"
				});
			}
			this.m_opoUdtCtx.m_pinLatest = 1;
			if (1 == this.m_opoUdtCtx.m_IsPinned && this.HasChanges)
			{
				try
				{
					OpsRef.UnMarkObjectByRef(this.m_opsConCtx, this.m_opsErrCtx, this.m_opoUdtCtx.m_pOCIRef);
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
			if (!this.PinAndLock(bWait, depthLevel))
			{
				throw new OracleException(54, this.m_connection.DataSource, string.Empty, OracleTypeException.GetTypeMsg(54, new object[0]));
			}
			object result = this.CreateCustomObject();
			this.UnPinObj();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleRef::GetCustomObjectForUpdate(1)\n"
				});
			}
			return result;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000269E0 File Offset: 0x000259E0
		public void Update(object customObject, bool bFlush)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleRef::Update()\n"
				});
			}
			if (customObject == null || (customObject as INullable).IsNull)
			{
				throw new InvalidOperationException();
			}
			if (this.m_opoUdtCtx.m_IsPinned == 0)
			{
				this.PinObj(OracleUdtFetchOption.Cache, 0);
			}
			this.UpdateFromCustomObject((IOracleCustomType)customObject);
			if (bFlush)
			{
				this.Flush();
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleRef::Update()\n"
				});
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00026A68 File Offset: 0x00025A68
		private unsafe void UpdateFromCustomObject(IOracleCustomType customObj)
		{
			int num = 0;
			OracleUdtDescriptor oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor2(this.m_connection, (OpoDscRefCtx)OracleUdt.GetUdtName(customObj.GetType().FullName, this.m_connection.DataSource));
			if (oracleUdtDescriptor == null)
			{
				throw new InvalidOperationException();
			}
			if (oracleUdtDescriptor.UdtTypeName.CompareTo(this.m_oracleUdtDesc.UdtTypeName) != 0)
			{
				throw new ArgumentException();
			}
			if (oracleUdtDescriptor.m_customTypeFactory == null)
			{
				object factory = OracleUdt.GetFactory(oracleUdtDescriptor);
				oracleUdtDescriptor.DescribeCustomType(factory);
			}
			if ((IntPtr)((void*)this.m_pOpoUdtValCtx) == IntPtr.Zero)
			{
				try
				{
					num = OpsUdt.AllocValCtx(out this.m_pOpoUdtValCtx, 1);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, null);
				}
			}
			if ((IntPtr)((void*)this.m_pOpoUdtValCtx->pOpoUdtValCtx) == IntPtr.Zero)
			{
				try
				{
					num = OpsUdt.AllocValCtx(out this.m_pOpoUdtValCtx->pOpoUdtValCtx, oracleUdtDescriptor.AttributeCount);
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					throw;
				}
				if (num == 0)
				{
					this.m_pOpoUdtValCtx->NumOpoUdtValCtx = oracleUdtDescriptor.AttributeCount;
				}
			}
			else if (this.m_pOpoUdtValCtx->NumOpoUdtValCtx < oracleUdtDescriptor.AttributeCount)
			{
				try
				{
					num = OpsUdt.ReAllocValCtx(ref this.m_pOpoUdtValCtx->pOpoUdtValCtx, this.m_pOpoUdtValCtx->NumOpoUdtValCtx, oracleUdtDescriptor.AttributeCount);
				}
				catch (Exception ex3)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex3);
					}
					throw;
				}
				if (num == 0)
				{
					this.m_pOpoUdtValCtx->NumOpoUdtValCtx = oracleUdtDescriptor.AttributeCount;
				}
			}
			if (num != 0)
			{
				OracleException.HandleError(num, this.m_connection, this.m_pOpoUdtValCtx->pOpsErrCtx, null);
			}
			this.m_pOpoUdtValCtx->pOpsErrCtx = this.m_connection.m_opoConCtx.opsErrCtx;
			this.m_pOpoUdtValCtx->pTDO = oracleUdtDescriptor.m_opsDscCtx;
			this.m_pOpoUdtValCtx->pOpoDscValCtx = oracleUdtDescriptor.m_pOpoDscValCtx;
			for (int i = 0; i < oracleUdtDescriptor.AttributeCount; i++)
			{
				this.m_pOpoUdtValCtx->pOpoUdtValCtx[i].bIsNull = 1;
			}
			customObj.FromCustomObject(this.m_connection, (IntPtr)((void*)this.m_pOpoUdtValCtx));
			try
			{
				num = OpsUdt.SetData(this.m_connection.m_opoConCtx.opsConCtx, this.m_pOpoUdtValCtx);
			}
			catch (Exception ex4)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex4);
				}
				throw;
			}
			if (num != 0)
			{
				OracleException.HandleError(num, this.m_connection, this.m_pOpoUdtValCtx->pOpsErrCtx, null);
			}
			try
			{
				num = OpsUdt.Copy(this.m_connection.m_opoConCtx.opsConCtx, this.m_pOpoUdtValCtx, this.m_opoUdtCtx.m_pUDT, this.m_opoUdtCtx.m_pObjInd);
			}
			catch (Exception ex5)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex5);
				}
				throw;
			}
			if (num != 0)
			{
				OracleException.HandleError(num, this.m_connection, this.m_pOpoUdtValCtx->pOpsErrCtx, null);
			}
			GC.KeepAlive(oracleUdtDescriptor);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00026D78 File Offset: 0x00025D78
		private bool PinAndLock(bool wait, int depthlevel)
		{
			int num = 0;
			int num2 = 1;
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			if (this.m_oracleUdtDesc == null)
			{
				this.m_oracleUdtDesc = this.UdtDescriptor;
			}
			try
			{
				num = OpsRef.PinAndLock(this.m_opsConCtx, this.m_opsErrCtx, wait, ref this.m_opoUdtCtx.m_pUDT, ref this.m_opoUdtCtx.m_pOCIRef, ref this.m_opoUdtCtx.m_pObjInd, ref num2, ref this.m_complexObjCtx, depthlevel, ref this.m_opoUdtCtx.m_pinLatest);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num != 0 && num != ErrRes.INT_ERR)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
				else if (num == 0)
				{
					this.m_objectPinCount++;
				}
			}
			this.m_opoUdtCtx.m_IsPinned = 1;
			return num2 == 1;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00026EE4 File Offset: 0x00025EE4
		internal void Initialize()
		{
			try
			{
				OpsObj.AllocObjValCtx(ref this.m_pOpoObjValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			this.m_opoObjRefCtx = new OpoObjRefCtx();
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00026F2C File Offset: 0x00025F2C
		private void Dispose(bool disposing)
		{
			if (!this.m_bNotNull)
			{
				return;
			}
			if (!this.m_disposed)
			{
				if (this.m_pOpoObjValCtx != null)
				{
					try
					{
						OpsObj.FreeValCtx(this.m_opsConCtx, this.m_opsErrCtx, this.m_complexObjCtx, this.m_pOpoObjValCtx);
						this.m_pOpoObjValCtx = null;
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
					}
				}
				if (this.m_opoUdtCtx != null && this.m_opoUdtCtx.m_pUDT != IntPtr.Zero)
				{
					try
					{
						int num = 0;
						while (num < this.m_objectPinCount && OpsRef.UnPinObj(this.m_opsConCtx, this.m_opsErrCtx, ref this.m_opoUdtCtx.m_pUDT, ref this.m_opoUdtCtx.m_IsPinned) == 0)
						{
							num++;
						}
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
					}
					finally
					{
						this.m_opoUdtCtx.m_pUDT = IntPtr.Zero;
					}
				}
				if (this.m_pOpoUdtValCtx != null)
				{
					try
					{
						OpsUdt.FreeValCtx(this.m_pOpoUdtValCtx, true);
						this.m_pOpoUdtValCtx = null;
					}
					catch (Exception ex3)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex3);
						}
					}
				}
				if (this.m_opsErrCtx != IntPtr.Zero)
				{
					try
					{
						OpsErr.FreeCtx(ref this.m_opsErrCtx);
					}
					catch (Exception ex4)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex4);
						}
					}
				}
				if (this.m_opsConCtx != IntPtr.Zero)
				{
					try
					{
						OpsCon.RelRef(ref this.m_opsConCtx);
					}
					catch (Exception ex5)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex5);
						}
					}
				}
				if (disposing)
				{
					if (this.m_opoUdtCtx.m_refCount >= 1)
					{
						this.m_opoUdtCtx.RelRefCount();
					}
					this.m_connection = null;
					this.m_oracleUdtDesc = null;
					this.m_opoObjRefCtx = null;
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0002712C File Offset: 0x0002612C
		public bool HasChanges
		{
			get
			{
				int num = 0;
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (!this.m_bNotNull)
				{
					return false;
				}
				if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
				if (this.m_connection.m_conSignature != this.m_conSignature)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
				}
				bool result = false;
				bool flag = false;
				if (this.m_opoUdtCtx.m_IsPinned == 0)
				{
					flag = true;
				}
				try
				{
					num = OpsRef.IsDirty(this.m_opsConCtx, this.m_opsErrCtx, ref this.m_opoUdtCtx.m_pUDT, ref this.m_opoUdtCtx.m_pObjInd, ref this.m_opoUdtCtx.m_pOCIRef, ref this.m_opoUdtCtx.m_IsPinned, ref result);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					num = ErrRes.INT_ERR;
					throw;
				}
				finally
				{
					if (flag && this.m_opoUdtCtx.m_IsPinned == 1)
					{
						this.m_objectPinCount++;
					}
					if (num != 0 && num != ErrRes.INT_ERR)
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
				return result;
			}
		}

		// Token: 0x0400022F RID: 559
		internal IntPtr m_opsConCtx;

		// Token: 0x04000230 RID: 560
		internal IntPtr m_opsErrCtx;

		// Token: 0x04000231 RID: 561
		internal OpoUdtCtx m_opoUdtCtx;

		// Token: 0x04000232 RID: 562
		internal OracleConnection m_connection;

		// Token: 0x04000233 RID: 563
		private OracleUdtDescriptor m_oracleUdtDesc;

		// Token: 0x04000234 RID: 564
		internal OpoObjRefCtx m_opoObjRefCtx;

		// Token: 0x04000235 RID: 565
		internal unsafe OpoObjValCtx* m_pOpoObjValCtx;

		// Token: 0x04000236 RID: 566
		internal unsafe OpoUdtValCtx* m_pOpoUdtValCtx;

		// Token: 0x04000237 RID: 567
		internal bool m_disposed;

		// Token: 0x04000238 RID: 568
		internal int m_conSignature;

		// Token: 0x04000239 RID: 569
		private bool m_bNotNull = true;

		// Token: 0x0400023A RID: 570
		private IntPtr m_complexObjCtx;

		// Token: 0x0400023B RID: 571
		private int m_objectPinCount;

		// Token: 0x0400023C RID: 572
		internal bool m_bNotRefByApp;

		// Token: 0x0400023D RID: 573
		public static readonly OracleRef Null = new OracleRef();
	}
}
