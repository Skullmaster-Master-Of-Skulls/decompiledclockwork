using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x020000F9 RID: 249
	public sealed class OracleClob : Stream, ICloneable, INullable
	{
		// Token: 0x06000906 RID: 2310 RVA: 0x00058CE6 File Offset: 0x00057CE6
		static OracleClob()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00058CFE File Offset: 0x00057CFE
		public OracleClob(OracleConnection con) : this(con, false, false)
		{
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00058D0C File Offset: 0x00057D0C
		public unsafe OracleClob(OracleConnection con, bool bCaching, bool bNClob)
		{
			this.m_allocOciLobLoc = 1;
			this.m_bNotNull = true;
			base..ctor();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::OracleClob(2)\n"
				});
			}
			if (con == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("con", null);
			}
			this.m_connection = con;
			this.m_conSignature = this.m_connection.m_conSignature;
			this.m_allocOciLobLoc = 1;
			this.m_opsConCtx = this.m_connection.m_opoConCtx.opsConCtx;
			if (this.m_opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			try
			{
				int num = OpsCon.AddRef(this.m_opsConCtx);
				if (num <= 1)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				GC.SuppressFinalize(this);
				throw;
			}
			int num2 = 0;
			try
			{
				num2 = OpsLob.AllocAllLobCtx(this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_popoLobValCtx, ref this.m_opsLobCtx, 0, IntPtr.Zero, this.m_allocOciLobLoc);
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				num2 = ErrRes.INT_ERR;
				GC.SuppressFinalize(this);
				throw;
			}
			finally
			{
				if (num2 != 0)
				{
					try
					{
						OpsCon.RelRef(ref this.m_opsConCtx);
					}
					catch (Exception ex3)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex3);
						}
					}
					if (num2 != ErrRes.INT_ERR)
					{
						GC.SuppressFinalize(this);
						OracleException.HandleError(num2, this.m_connection, IntPtr.Zero, this);
					}
				}
			}
			if (bNClob)
			{
				this.m_popoLobValCtx->pLobProperties->lobType = 4;
				this.m_isNClob = true;
			}
			else
			{
				this.m_popoLobValCtx->pLobProperties->lobType = 3;
			}
			if (bCaching)
			{
				this.m_popoLobValCtx->pLobProperties->isCached = 1;
				this.m_caching = true;
			}
			else
			{
				this.m_popoLobValCtx->pLobProperties->isCached = 0;
			}
			this.m_isTemporaryLob = true;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClob::OracleClob(2) created: " + this.m_opsLobCtx.ToString() + "\n"
				});
			}
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00058F60 File Offset: 0x00057F60
		internal OracleClob(bool bCaching, bool bNClob) : this(OracleConnection.GetInternalConnection(), bCaching, bNClob)
		{
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00058F6F File Offset: 0x00057F6F
		private OracleClob()
		{
			this.m_allocOciLobLoc = 1;
			this.m_bNotNull = true;
			base..ctor();
			this.m_bNotNull = false;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00058F8C File Offset: 0x00057F8C
		internal unsafe OracleClob(OracleConnection con, IntPtr opsLobCtx, bool bCaching, bool bNClob, bool bTempLob)
		{
			this.m_allocOciLobLoc = 1;
			this.m_bNotNull = true;
			base..ctor();
			this.m_connection = con;
			this.m_conSignature = this.m_connection.m_conSignature;
			this.m_allocOciLobLoc = 1;
			this.m_opsLobCtx = opsLobCtx;
			this.m_opsConCtx = this.m_connection.m_opoConCtx.opsConCtx;
			if (this.m_opsConCtx == IntPtr.Zero)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			try
			{
				int num = OpsCon.AddRef(this.m_opsConCtx);
				if (num <= 1)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				GC.SuppressFinalize(this);
				throw;
			}
			int num2 = 0;
			try
			{
				num2 = OpsLob.AllocAllLobCtx(this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_popoLobValCtx, ref this.m_opsLobCtx, 0, IntPtr.Zero, this.m_allocOciLobLoc);
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				num2 = ErrRes.INT_ERR;
				GC.SuppressFinalize(this);
				throw;
			}
			finally
			{
				if (num2 != 0)
				{
					try
					{
						OpsCon.RelRef(ref this.m_opsConCtx);
					}
					catch (Exception ex3)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex3);
						}
					}
					if (num2 != ErrRes.INT_ERR)
					{
						GC.SuppressFinalize(this);
						OracleException.HandleError(num2, this.m_connection, IntPtr.Zero, this);
					}
				}
			}
			this.m_popoLobValCtx->pLobProperties->lobType = 0;
			try
			{
				num2 = OpsLob.LobCheckNClob(this.m_opsLobCtx, this.m_popoLobValCtx);
			}
			catch (Exception ex4)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex4);
				}
				num2 = ErrRes.INT_ERR;
				GC.SuppressFinalize(this);
				throw;
			}
			finally
			{
				if (num2 != 0)
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
					if (num2 != ErrRes.INT_ERR)
					{
						GC.SuppressFinalize(this);
						OracleException.HandleError(num2, this.m_connection, this.m_opsErrCtx, this);
					}
				}
			}
			if (this.m_popoLobValCtx->pLobProperties->lobType == 4)
			{
				this.m_isNClob = true;
			}
			if (bCaching)
			{
				this.m_popoLobValCtx->pLobProperties->isCached = 1;
				this.m_caching = true;
			}
			else
			{
				this.m_popoLobValCtx->pLobProperties->isCached = 0;
			}
			if (bTempLob)
			{
				this.m_isTemporaryLob = true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClob::OracleClob(3) created: " + this.m_opsLobCtx.ToString() + "\n"
				});
			}
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00059258 File Offset: 0x00058258
		internal unsafe OracleClob(OracleConnection con, IntPtr opsLobLoc, bool bCaching, bool bNClob, bool bTempLob, int allocOciLobLoc)
		{
			this.m_allocOciLobLoc = 1;
			this.m_bNotNull = true;
			base..ctor();
			this.m_connection = con;
			this.m_conSignature = this.m_connection.m_conSignature;
			this.m_allocOciLobLoc = allocOciLobLoc;
			this.m_opsConCtx = this.m_connection.m_opoConCtx.opsConCtx;
			if (this.m_opsConCtx == IntPtr.Zero)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			try
			{
				int num = OpsCon.AddRef(this.m_opsConCtx);
				if (num <= 1)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				GC.SuppressFinalize(this);
				throw;
			}
			int num2 = 0;
			this.m_popoLobValCtx = null;
			try
			{
				num2 = OpsLob.AllocAllLobCtx(this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_popoLobValCtx, ref this.m_opsLobCtx, 0, opsLobLoc, this.m_allocOciLobLoc);
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				num2 = ErrRes.INT_ERR;
				GC.SuppressFinalize(this);
				throw;
			}
			finally
			{
				if (num2 != 0)
				{
					try
					{
						OpsCon.RelRef(ref this.m_opsConCtx);
					}
					catch (Exception ex3)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex3);
						}
					}
					if (num2 != ErrRes.INT_ERR)
					{
						GC.SuppressFinalize(this);
						OracleException.HandleError(num2, this.m_connection, IntPtr.Zero, this);
					}
				}
			}
			this.m_popoLobValCtx->pLobProperties->lobType = 0;
			try
			{
				num2 = OpsLob.LobCheckNClob(this.m_opsLobCtx, this.m_popoLobValCtx);
			}
			catch (Exception ex4)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex4);
				}
				num2 = ErrRes.INT_ERR;
				GC.SuppressFinalize(this);
				throw;
			}
			finally
			{
				if (num2 != 0)
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
					if (num2 != ErrRes.INT_ERR)
					{
						GC.SuppressFinalize(this);
						OracleException.HandleError(num2, this.m_connection, this.m_opsErrCtx, this);
					}
				}
			}
			if (this.m_popoLobValCtx->pLobProperties->lobType == 4)
			{
				this.m_isNClob = true;
			}
			if (bCaching)
			{
				this.m_popoLobValCtx->pLobProperties->isCached = 1;
				this.m_caching = true;
			}
			else
			{
				this.m_popoLobValCtx->pLobProperties->isCached = 0;
			}
			if (bTempLob)
			{
				this.m_isTemporaryLob = true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClob::OracleClob(3) created: " + this.m_opsLobCtx.ToString() + "\n"
				});
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00059520 File Offset: 0x00058520
		internal OracleClob(IntPtr opsLobLoc, bool bCaching, bool bNClob, bool bTempLob, int allocOciLobLoc) : this(OracleConnection.GetInternalConnection(), opsLobLoc, bCaching, bNClob, bTempLob, allocOciLobLoc)
		{
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00059534 File Offset: 0x00058534
		~OracleClob()
		{
			this.Dispose(false);
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00059564 File Offset: 0x00058564
		internal IntPtr LobCtx
		{
			get
			{
				return this.m_opsLobCtx;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0005956C File Offset: 0x0005856C
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00059578 File Offset: 0x00058578
		public override bool CanRead
		{
			get
			{
				return !this.m_bNotNull || (!this.m_doneDispose && !(this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero) && this.m_connection.m_conSignature == this.m_conSignature);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x000595CC File Offset: 0x000585CC
		public override bool CanSeek
		{
			get
			{
				return !this.m_bNotNull || (!this.m_doneDispose && !(this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero) && this.m_connection.m_conSignature == this.m_conSignature);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00059620 File Offset: 0x00058620
		public override bool CanWrite
		{
			get
			{
				return this.m_bNotNull && !this.m_doneDispose && !(this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero) && this.m_connection.m_conSignature == this.m_conSignature;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x00059671 File Offset: 0x00058671
		public int OptimumChunkSize
		{
			get
			{
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (!this.m_bNotNull)
				{
					return 0;
				}
				if (this.m_optimumChunkSize != 0)
				{
					return this.m_optimumChunkSize;
				}
				return this.GetOptimumChunkSize();
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x000596AC File Offset: 0x000586AC
		public OracleConnection Connection
		{
			get
			{
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (!this.m_bNotNull)
				{
					return null;
				}
				if (this.m_connection.m_internalUse)
				{
					throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_NOTSUPPORTED_INTERNAL_CONN, new string[0]));
				}
				return this.m_connection;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x00059708 File Offset: 0x00058708
		public bool IsEmpty
		{
			get
			{
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.Length != 0L)
				{
					return this.m_isEmpty = false;
				}
				return this.m_isEmpty = true;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0005975B File Offset: 0x0005875B
		public bool IsNClob
		{
			get
			{
				return this.m_isNClob;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00059763 File Offset: 0x00058763
		public bool IsInChunkWriteMode
		{
			get
			{
				return this.m_bNotNull && this.m_isInChunkWriteMode;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x00059778 File Offset: 0x00058778
		public unsafe bool IsTemporary
		{
			get
			{
				if (this.m_doneDispose)
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
				if (this.m_isTemporaryLob)
				{
					return true;
				}
				if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
				if (this.m_connection.m_conSignature != this.m_conSignature)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
				}
				int num = 0;
				try
				{
					num = OpsLob.IsTemporary(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx, this.m_popoLobValCtx);
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
				return this.m_popoLobValCtx->pLobProperties->isTemporaryLob == 1;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x000598E8 File Offset: 0x000588E8
		public unsafe override long Length
		{
			get
			{
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (!this.m_bNotNull)
				{
					return 0L;
				}
				if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
				if (this.m_connection.m_conSignature != this.m_conSignature)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
				}
				if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
				{
					return this.m_length = 0L;
				}
				int num = 0;
				try
				{
					num = OpsLob.GetLength(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx, this.m_popoLobValCtx);
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
				this.m_length = this.m_popoLobValCtx->lobDataLength * 2L;
				return this.m_length;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00059A14 File Offset: 0x00058A14
		public string Value
		{
			get
			{
				if (this.m_doneDispose)
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
				if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
				{
					return string.Empty;
				}
				long position = this.m_position;
				this.m_position = 0L;
				long num = this.Length / 2L;
				int num2;
				if (num >= 2147483647L)
				{
					num2 = int.MaxValue;
				}
				else
				{
					num2 = (int)num;
				}
				char[] array = new char[num2];
				this.Read(array, 0, num2);
				string result = new string(array);
				this.m_position = position;
				return result;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x00059B10 File Offset: 0x00058B10
		// (set) Token: 0x0600091D RID: 2333 RVA: 0x00059BA4 File Offset: 0x00058BA4
		public override long Position
		{
			get
			{
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (!this.m_bNotNull)
				{
					return 0L;
				}
				if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
				if (this.m_connection.m_conSignature != this.m_conSignature)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
				}
				return this.m_position;
			}
			set
			{
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (this.m_bNotNull)
				{
					if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
					{
						throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
					}
					if (this.m_connection.m_conSignature != this.m_conSignature)
					{
						throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
					}
					if (value < 0L)
					{
						throw new ArgumentOutOfRangeException(null, null);
					}
					this.m_position = value;
				}
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00059C41 File Offset: 0x00058C41
		internal void KeepOciLobLoc()
		{
			this.m_allocOciLobLoc = 0;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00059C4C File Offset: 0x00058C4C
		internal int GetLobLocator(out IntPtr opsLobCtx)
		{
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			opsLobCtx = IntPtr.Zero;
			return OpsLob.GetLobLocator(this.LobCtx, ref opsLobCtx);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00059C84 File Offset: 0x00058C84
		public void Append(OracleClob obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::Append(1)\n"
				});
			}
			if (this.m_doneDispose)
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
			if (obj.m_connection != this.m_connection && (!obj.m_connection.m_contextConnection || !this.m_connection.m_contextConnection))
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_DIFFERENT_CONNECTIONS, new string[0]));
			}
			if (obj.m_isTemporaryLob && !obj.m_doneTempLobCreate)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::Append(1)\n"
					});
				}
				return;
			}
			if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
			{
				this.CreateTempLob();
			}
			int num = 0;
			try
			{
				num = OpsLob.Append(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx, obj.m_opsLobCtx);
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
					" (EXIT)  OracleClob::Append(1)\n"
				});
			}
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00059E3C File Offset: 0x00058E3C
		public void Append(byte[] buffer, int offset, int count)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::Append(2)\n"
				});
			}
			if (this.m_doneDispose)
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
			if (buffer.Length == 0 || count == 0)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::Append(2)\n"
					});
				}
				return;
			}
			if (offset % 2 != 0 || count % 2 != 0)
			{
				throw new ArgumentOutOfRangeException("", OpoErrResManager.GetErrorMesg(ErrRes.EVEN_VALUE_PARAM_REQUIRED, new string[0]));
			}
			if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
			{
				this.CreateTempLob();
			}
			long position = this.m_position;
			this.Seek(0L, SeekOrigin.End);
			this.Write(buffer, offset, count);
			this.m_position = position;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClob::Append(2)\n"
				});
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00059F88 File Offset: 0x00058F88
		public void Append(char[] buffer, int offset, int count)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::Append(3)\n"
				});
			}
			if (this.m_doneDispose)
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
			if (buffer.Length == 0 || count == 0)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::Append(3)\n"
					});
				}
				return;
			}
			if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
			{
				this.CreateTempLob();
			}
			long position = this.m_position;
			this.Seek(0L, SeekOrigin.End);
			this.Write(buffer, offset, count);
			this.m_position = position;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClob::Append(3)\n"
				});
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0005A0B0 File Offset: 0x000590B0
		public object Clone()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::Clone()\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::Clone()\n"
					});
				}
				return OracleClob.Null;
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
			{
				this.CreateTempLob();
			}
			int num = 0;
			IntPtr zero = IntPtr.Zero;
			OracleClob oracleClob;
			if (this.m_isTemporaryLob)
			{
				oracleClob = new OracleClob(this.m_connection, this.m_caching, this.IsNClob);
			}
			else
			{
				oracleClob = new OracleClob(this.m_connection, zero, this.m_caching, this.m_isNClob, false);
			}
			if (oracleClob.m_isTemporaryLob)
			{
				oracleClob.CreateTempLob();
			}
			try
			{
				num = OpsLob.LocatorAssign(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx, oracleClob.LobCtx);
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
			oracleClob.m_position = this.m_position;
			oracleClob.m_bNotNull = this.m_bNotNull;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClob::Clone()\n"
				});
			}
			return oracleClob;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0005A288 File Offset: 0x00059288
		public override void Close()
		{
			this.Dispose();
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0005A290 File Offset: 0x00059290
		public int Compare(long src_offset, OracleClob obj, long dst_offset, long amount)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::Compare()\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (obj == null)
			{
				throw new ArgumentNullException();
			}
			if (!this.m_bNotNull || obj.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBlob::IsEqual()\n"
					});
				}
				if (!this.m_bNotNull && obj.IsNull)
				{
					return 0;
				}
				throw new OracleNullValueException();
			}
			else
			{
				if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
				if (this.m_connection.m_conSignature != this.m_conSignature)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
				}
				if (obj.m_connection != this.m_connection && (!obj.m_connection.m_contextConnection || !this.m_connection.m_contextConnection))
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_DIFFERENT_CONNECTIONS, new string[0]));
				}
				if (src_offset < 0L || dst_offset < 0L || amount < 0L)
				{
					throw new ArgumentOutOfRangeException(null, null);
				}
				if (obj.m_isTemporaryLob && !obj.m_doneTempLobCreate && this.m_isTemporaryLob && !this.m_doneTempLobCreate)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (EXIT)  OracleClob::Compare()\n"
						});
					}
					return 0;
				}
				if (obj.m_isTemporaryLob && !obj.m_doneTempLobCreate)
				{
					obj.CreateTempLob();
				}
				if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
				{
					this.CreateTempLob();
				}
				int num = -1;
				src_offset += 1L;
				dst_offset += 1L;
				if (this.m_cmd == null)
				{
					this.m_cmd = new OracleCommand();
				}
				this.m_cmd.Connection = this.m_connection;
				this.m_cmd.CommandText = "BEGIN :1 := DBMS_LOB.COMPARE(:LOB_1, :LOB_2, :AMOUNT, :OFFSET_1, :OFFSET_2); END;";
				this.m_cmd.CommandType = CommandType.Text;
				try
				{
					OracleParameter oracleParameter = new OracleParameter("return_value", OracleDbType.Int32, num, ParameterDirection.ReturnValue);
					oracleParameter.DbType = DbType.Int32;
					this.m_cmd.Parameters.Add(oracleParameter);
					OracleDbType dbType;
					if (obj.IsNClob)
					{
						dbType = OracleDbType.NClob;
					}
					else
					{
						dbType = OracleDbType.Clob;
					}
					this.m_cmd.Parameters.Add("provided_clob", dbType, obj, ParameterDirection.Input);
					OracleDbType dbType2;
					if (this.IsNClob)
					{
						dbType2 = OracleDbType.NClob;
					}
					else
					{
						dbType2 = OracleDbType.Clob;
					}
					this.m_cmd.Parameters.Add("current_clob", dbType2, this, ParameterDirection.Input);
					this.m_cmd.Parameters.Add("compare_amount", OracleDbType.Int64, amount, ParameterDirection.Input);
					this.m_cmd.Parameters.Add("src_offset", OracleDbType.Int64, src_offset, ParameterDirection.Input);
					this.m_cmd.Parameters.Add("dst_offset", OracleDbType.Int64, dst_offset, ParameterDirection.Input);
					this.m_cmd.ExecuteNonQuery();
					num = (int)this.m_cmd.Parameters[0].Value;
				}
				finally
				{
					this.m_cmd.Parameters.Clear();
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (EXIT)  OracleClob::Compare()\n"
						});
					}
				}
				return num;
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0005A5EC File Offset: 0x000595EC
		public long CopyTo(OracleClob obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::CopyTo(1)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (obj == null)
			{
				throw new ArgumentNullException();
			}
			if (!this.m_bNotNull || obj.IsNull)
			{
				throw new OracleNullValueException();
			}
			if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::CopyTo(1)\n"
					});
				}
				return 0L;
			}
			long result = this.CopyTo(0L, obj, 0L, this.Length / 2L);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClob::CopyTo(1)\n"
				});
			}
			return result;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0005A6B4 File Offset: 0x000596B4
		public long CopyTo(OracleClob obj, long dst_offset)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::CopyTo(2)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (obj == null)
			{
				throw new ArgumentNullException();
			}
			if (!this.m_bNotNull || obj.IsNull)
			{
				throw new OracleNullValueException();
			}
			if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::CopyTo(2)\n"
					});
				}
				return 0L;
			}
			long result = this.CopyTo(0L, obj, dst_offset, this.Length / 2L);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClob::CopyTo(2)\n"
				});
			}
			return result;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0005A778 File Offset: 0x00059778
		public unsafe long CopyTo(long src_offset, OracleClob obj, long dst_offset, long amount)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::CopyTo(3)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (obj == null)
			{
				throw new ArgumentNullException();
			}
			if (!this.m_bNotNull || obj.IsNull)
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
			if (obj.m_connection != this.m_connection && (!obj.m_connection.m_contextConnection || !this.m_connection.m_contextConnection))
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_DIFFERENT_CONNECTIONS, new string[0]));
			}
			if (src_offset < 0L || dst_offset < 0L || amount < 0L)
			{
				throw new ArgumentOutOfRangeException(null, null);
			}
			if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::CopyTo(3)\n"
					});
				}
				return 0L;
			}
			if (obj.m_isTemporaryLob && !obj.m_doneTempLobCreate)
			{
				obj.CreateTempLob();
			}
			int num = 0;
			this.m_popoLobValCtx->inAmount = (long)((int)amount);
			this.m_popoLobValCtx->src_offset = (long)((int)src_offset + 1);
			this.m_popoLobValCtx->dst_offset = (long)((int)dst_offset + 1);
			try
			{
				num = OpsLob.Copy(this.m_opsConCtx, this.m_opsErrCtx, obj.m_opsLobCtx, this.m_opsLobCtx, this.m_popoLobValCtx);
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
					" (EXIT)  OracleClob::CopyTo(3)\n"
				});
			}
			return this.m_popoLobValCtx->inAmount;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0005A998 File Offset: 0x00059998
		public new void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0005A9A8 File Offset: 0x000599A8
		public void EndChunkWrite()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::EndChunkWrite()\n"
				});
			}
			if (this.m_doneDispose)
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
			if (!this.m_isInChunkWriteMode)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::EndChunkWrite()\n"
					});
				}
				return;
			}
			int num = 0;
			try
			{
				num = OpsLob.Close(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx);
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
			this.m_isInChunkWriteMode = false;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClob::EndChunkWrite()\n"
				});
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0005AB04 File Offset: 0x00059B04
		public long Erase()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::Erase(1)\n"
				});
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			long result = this.Erase(0L, this.Length / 2L);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClob::Erase(1)\n"
				});
			}
			return result;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0005AB6C File Offset: 0x00059B6C
		public unsafe long Erase(long offset, long amount)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::Erase(2)\n"
				});
			}
			if (this.m_doneDispose)
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
			if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::Erase(2)\n"
					});
				}
				return 0L;
			}
			if (offset < 0L || amount < 0L)
			{
				throw new ArgumentOutOfRangeException(null, null);
			}
			int num = 0;
			this.m_popoLobValCtx->dst_offset = (long)((int)offset + 1);
			this.m_popoLobValCtx->inAmount = (long)((int)amount);
			try
			{
				num = OpsLob.Erase(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx, this.m_popoLobValCtx);
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
					" (EXIT)  OracleClob::Erase(2)\n"
				});
			}
			return this.m_popoLobValCtx->outAmount;
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0005AD0C File Offset: 0x00059D0C
		public override void Flush()
		{
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0005AD10 File Offset: 0x00059D10
		public unsafe bool IsEqual(OracleClob obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::IsEqual()\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (obj == null)
			{
				throw new ArgumentNullException();
			}
			if (!this.m_bNotNull || obj.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::IsEqual()\n"
					});
				}
				return !this.m_bNotNull && obj.IsNull;
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			if (obj.m_connection != this.m_connection && (!obj.m_connection.m_contextConnection || !this.m_connection.m_contextConnection))
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_DIFFERENT_CONNECTIONS, new string[0]));
			}
			if ((obj.m_isTemporaryLob && !obj.m_doneTempLobCreate) || (this.m_isTemporaryLob && !this.m_doneTempLobCreate))
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::IsEqual()\n"
					});
				}
				return false;
			}
			int num = 0;
			try
			{
				num = OpsLob.IsEqual(this.m_opsConCtx, this.m_opsLobCtx, obj.LobCtx, this.m_popoLobValCtx);
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
			if (this.m_popoLobValCtx->pLobProperties->isEqual == 1)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::IsEqual()\n"
					});
				}
				return true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClob::IsEqual()\n"
				});
			}
			return false;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0005AF38 File Offset: 0x00059F38
		public void BeginChunkWrite()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::BeginChunkWrite()\n"
				});
			}
			if (this.m_doneDispose)
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
			if (this.m_isInChunkWriteMode)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::BeginChunkWrite()\n"
					});
				}
				return;
			}
			if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
			{
				this.CreateTempLob();
			}
			int num = 0;
			try
			{
				num = OpsLob.Open(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx, this.m_popoLobValCtx);
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
			this.m_isInChunkWriteMode = true;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClob::BeginChunkWrite()\n"
				});
			}
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0005B0B0 File Offset: 0x0005A0B0
		public unsafe override int Read(byte[] buffer, int offset, int count)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::Read(1)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::Read(1)\n"
					});
				}
				return 0;
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			if (offset < 0 || count < 0 || offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (count == 0 || (this.m_isTemporaryLob && !this.m_doneTempLobCreate))
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::Read(1)\n"
					});
				}
				return 0;
			}
			int num = 0;
			if (this.m_position <= 0L)
			{
				this.m_popoLobValCtx->src_offset = 1L;
			}
			else
			{
				this.m_popoLobValCtx->src_offset = this.m_position / 2L + 1L;
			}
			if (this.m_popoLobValCtx->src_offset > (long)((ulong)-1))
			{
				throw new OracleTypeException(OpoErrResManager.GetErrorMesg(ErrRes.TYP_OFFSET_NOT_SUPPORTED, new string[]
				{
					4294967294U.ToString()
				}));
			}
			this.m_popoLobValCtx->dst_offset = (long)(offset / 2);
			if (count + offset <= buffer.Length)
			{
				this.m_popoLobValCtx->inAmount = (long)(count / 2);
				this.m_popoLobValCtx->count = (long)count;
			}
			else
			{
				this.m_popoLobValCtx->inAmount = (long)(buffer.Length / 2) - this.m_popoLobValCtx->dst_offset;
				this.m_popoLobValCtx->count = (long)(count - offset);
			}
			this.m_popoLobValCtx->pLobProperties->isUnicode = 1;
			GCHandle gchandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			IntPtr opoLobRefCtx = gchandle.AddrOfPinnedObject();
			this.m_popoLobValCtx->offset = (long)offset;
			this.m_popoLobValCtx->position = this.m_position;
			try
			{
				num = OpsLob.Read(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx, this.m_popoLobValCtx, opoLobRefCtx);
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
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			this.m_position += this.m_popoLobValCtx->outAmount;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClob::Read(1)\n"
				});
			}
			return (int)this.m_popoLobValCtx->outAmount;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0005B38C File Offset: 0x0005A38C
		public unsafe int Read(char[] buffer, int offset, int count)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::Read(2)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::Read(2)\n"
					});
				}
				return 0;
			}
			if (this.m_position % 2L != 0L)
			{
				throw new ArgumentOutOfRangeException(null, OpoErrResManager.GetErrorMesg(ErrRes.EVEN_VALUE_PARAM_REQUIRED, new string[0]));
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			if (offset < 0 || count < 0 || offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException(null, null);
			}
			if (count == 0 || (this.m_isTemporaryLob && !this.m_doneTempLobCreate))
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::Read(2)\n"
					});
				}
				return 0;
			}
			int num = 0;
			if (this.m_position <= 0L)
			{
				this.m_popoLobValCtx->src_offset = 1L;
			}
			else
			{
				this.m_popoLobValCtx->src_offset = this.m_position / 2L + 1L;
			}
			if (this.m_popoLobValCtx->src_offset > (long)((ulong)-1))
			{
				throw new OracleTypeException(OpoErrResManager.GetErrorMesg(ErrRes.TYP_OFFSET_NOT_SUPPORTED, new string[]
				{
					4294967294U.ToString()
				}));
			}
			this.m_popoLobValCtx->dst_offset = (long)offset;
			if (count + offset <= buffer.Length)
			{
				this.m_popoLobValCtx->inAmount = (long)count;
			}
			else
			{
				this.m_popoLobValCtx->inAmount = (long)(buffer.Length - offset);
			}
			this.m_popoLobValCtx->pLobProperties->isUnicode = 1;
			GCHandle gchandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			IntPtr opoLobRefCtx = gchandle.AddrOfPinnedObject();
			this.m_popoLobValCtx->offset = -1L;
			this.m_popoLobValCtx->count = -1L;
			this.m_popoLobValCtx->position = -1L;
			try
			{
				num = OpsLob.Read(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx, this.m_popoLobValCtx, opoLobRefCtx);
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
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			this.m_position += this.m_popoLobValCtx->outAmount * 2L;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClob::Read(2)\n"
				});
			}
			return (int)this.m_popoLobValCtx->outAmount;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0005B66C File Offset: 0x0005A66C
		public long Search(byte[] val, long offset, long nth)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::Search(1)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::Search(1)\n"
					});
				}
				return 0L;
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			if (offset < 0L || nth <= 0L || nth >= (long)((ulong)-1) || offset >= (long)((ulong)-1))
			{
				throw new ArgumentOutOfRangeException(null, null);
			}
			if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::Search(1)\n"
					});
				}
				return 0L;
			}
			OracleString oracleString = new OracleString(val, true);
			int num;
			if (oracleString.Length > 0)
			{
				num = oracleString.Length;
			}
			else
			{
				num = 1;
			}
			char[] array = new char[num];
			oracleString.IsCaseIgnored = false;
			if (oracleString.Length > 0)
			{
				oracleString.Value.CopyTo(0, array, 0, oracleString.Length);
			}
			if (array.Length * 2 > 16383)
			{
				throw new ArgumentOutOfRangeException(null, null);
			}
			long num2 = 0L;
			offset += 1L;
			if (this.m_cmd == null)
			{
				this.m_cmd = new OracleCommand();
			}
			this.m_cmd.Connection = this.m_connection;
			this.m_cmd.CommandText = "BEGIN :1 := DBMS_LOB.INSTR(:LOB_LOC, :PATTERN, :OFFSET, :NTH); END;";
			this.m_cmd.CommandType = CommandType.Text;
			try
			{
				OracleParameter oracleParameter = new OracleParameter("return_value", OracleDbType.Int64, num2, ParameterDirection.ReturnValue);
				oracleParameter.DbType = DbType.Int64;
				this.m_cmd.Parameters.Add(oracleParameter);
				OracleDbType dbType;
				if (this.IsNClob)
				{
					dbType = OracleDbType.NClob;
				}
				else
				{
					dbType = OracleDbType.Clob;
				}
				this.m_cmd.Parameters.Add("this_clob_or_nclob", dbType, this, ParameterDirection.Input);
				this.m_cmd.Parameters.Add("pattern", OracleDbType.Varchar2, array, ParameterDirection.Input);
				this.m_cmd.Parameters.Add("this_offset", OracleDbType.Int64, offset, ParameterDirection.Input);
				this.m_cmd.Parameters.Add("occurrence", OracleDbType.Int64, nth, ParameterDirection.Input);
				this.m_cmd.ExecuteNonQuery();
				num2 = (long)this.m_cmd.Parameters[0].Value;
			}
			finally
			{
				this.m_cmd.Parameters.Clear();
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::Search(1)\n"
					});
				}
			}
			return num2;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0005B954 File Offset: 0x0005A954
		public long Search(char[] val, long offset, long nth)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::Search(2)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::Search(2)\n"
					});
				}
				return 0L;
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			if (offset < 0L || nth <= 0L || val.Length * 2 > 16383 || nth >= (long)((ulong)-1) || offset >= (long)((ulong)-1))
			{
				throw new ArgumentOutOfRangeException(null, null);
			}
			if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::Search(2)\n"
					});
				}
				return 0L;
			}
			long num = 0L;
			offset += 1L;
			if (this.m_cmd == null)
			{
				this.m_cmd = new OracleCommand();
			}
			this.m_cmd.Connection = this.m_connection;
			this.m_cmd.CommandText = "BEGIN :1 := DBMS_LOB.INSTR(:LOB_LOC, :PATTERN, :OFFSET, :NTH); END;";
			this.m_cmd.CommandType = CommandType.Text;
			try
			{
				OracleParameter oracleParameter = new OracleParameter("return_value", OracleDbType.Int64, num, ParameterDirection.ReturnValue);
				oracleParameter.DbType = DbType.Int64;
				this.m_cmd.Parameters.Add(oracleParameter);
				OracleDbType dbType;
				if (this.IsNClob)
				{
					dbType = OracleDbType.NClob;
				}
				else
				{
					dbType = OracleDbType.Clob;
				}
				this.m_cmd.Parameters.Add("this_clob_or_nclob", dbType, this, ParameterDirection.Input);
				OracleDbType dbType2;
				if (this.IsNClob)
				{
					dbType2 = OracleDbType.NVarchar2;
				}
				else
				{
					dbType2 = OracleDbType.Varchar2;
				}
				this.m_cmd.Parameters.Add("pattern", dbType2, val, ParameterDirection.Input);
				this.m_cmd.Parameters.Add("this_offset", OracleDbType.Int64, offset, ParameterDirection.Input);
				this.m_cmd.Parameters.Add("occurrence", OracleDbType.Int64, nth, ParameterDirection.Input);
				this.m_cmd.ExecuteNonQuery();
				num = (long)this.m_cmd.Parameters[0].Value;
			}
			finally
			{
				this.m_cmd.Parameters.Clear();
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::Search(2)\n"
					});
				}
			}
			return num;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0005BBF0 File Offset: 0x0005ABF0
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::Seek()\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::Seek()\n"
					});
				}
				return 0L;
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::Seek()\n"
					});
				}
				return 0L;
			}
			if (origin == SeekOrigin.Begin)
			{
				this.m_position = offset;
			}
			if (origin == SeekOrigin.Current)
			{
				this.m_position += offset;
			}
			if (origin == SeekOrigin.End)
			{
				this.m_position = this.Length + offset;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClob::Seek()\n"
				});
			}
			return this.m_position;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0005BD38 File Offset: 0x0005AD38
		public unsafe override void SetLength(long newLength)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::SetLength()\n"
				});
			}
			if (this.m_doneDispose)
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
			if (newLength < 0L)
			{
				throw new ArgumentOutOfRangeException(null, null);
			}
			if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::SetLength()\n"
					});
				}
				return;
			}
			int num = 0;
			this.m_popoLobValCtx->inAmount = (long)((int)newLength);
			this.m_popoLobValCtx->pLobProperties->isUnicode = 1;
			try
			{
				num = OpsLob.Trim(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx, this.m_popoLobValCtx);
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
			if (this.m_position > newLength * 2L)
			{
				this.Seek(0L, SeekOrigin.End);
			}
			if (newLength == 0L)
			{
				this.m_isEmpty = true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClob::SetLength()\n"
				});
			}
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0005BEE8 File Offset: 0x0005AEE8
		public unsafe override void Write(byte[] buffer, int offset, int count)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::Write(1)\n"
				});
			}
			if (this.m_doneDispose)
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
			if (offset < 0 || count < 0 || offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException(null, null);
			}
			if (offset % 2 != 0 || count % 2 != 0 || this.m_position % 2L != 0L)
			{
				throw new ArgumentOutOfRangeException(null, OpoErrResManager.GetErrorMesg(ErrRes.EVEN_VALUE_PARAM_REQUIRED, new string[0]));
			}
			if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
			{
				this.CreateTempLob();
			}
			if (count == 0)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::Write(1)\n"
					});
				}
				return;
			}
			int num = 0;
			this.m_popoLobValCtx->src_offset = (long)(offset / 2);
			if (this.m_position <= 0L)
			{
				this.m_popoLobValCtx->dst_offset = 1L;
			}
			else
			{
				this.m_popoLobValCtx->dst_offset = this.m_position / 2L + 1L;
			}
			if (this.m_popoLobValCtx->dst_offset > (long)((ulong)-1))
			{
				throw new OracleTypeException(OpoErrResManager.GetErrorMesg(ErrRes.TYP_OFFSET_NOT_SUPPORTED, new string[]
				{
					4294967294U.ToString()
				}));
			}
			if (count + offset <= buffer.Length)
			{
				this.m_popoLobValCtx->inAmount = (long)(count / 2);
			}
			else
			{
				this.m_popoLobValCtx->inAmount = (long)(buffer.Length / 2) - this.m_popoLobValCtx->src_offset;
			}
			this.m_popoLobValCtx->pLobProperties->isUnicode = 1;
			GCHandle gchandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			IntPtr opoLobRefCtx = gchandle.AddrOfPinnedObject();
			try
			{
				num = OpsLob.Write(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx, this.m_popoLobValCtx, opoLobRefCtx);
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
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			this.m_position += this.m_popoLobValCtx->inAmount * 2L;
			if (this.m_popoLobValCtx->inAmount != 0L)
			{
				this.m_isEmpty = false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClob::Write(1)\n"
				});
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0005C1B0 File Offset: 0x0005B1B0
		public void Write(char[] buffer, int offset, int count)
		{
			this.Write(buffer, offset, count, false);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0005C1BC File Offset: 0x0005B1BC
		public unsafe void Write(char[] buffer, int offset, int count, bool bIsFromEF)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClob::Write(2)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			if (this.m_position % 2L != 0L)
			{
				throw new ArgumentOutOfRangeException(null, OpoErrResManager.GetErrorMesg(ErrRes.EVEN_VALUE_PARAM_REQUIRED, new string[0]));
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			if (offset < 0 || count < 0 || offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException(null, null);
			}
			if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
			{
				this.CreateTempLob();
			}
			if (count == 0)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::Write(2)\n"
					});
				}
				return;
			}
			int num = 0;
			this.m_popoLobValCtx->src_offset = (long)offset;
			if (this.m_position <= 0L)
			{
				this.m_popoLobValCtx->dst_offset = 1L;
			}
			else
			{
				this.m_popoLobValCtx->dst_offset = this.m_position / 2L + 1L;
			}
			if (this.m_popoLobValCtx->dst_offset > (long)((ulong)-1))
			{
				throw new OracleTypeException(OpoErrResManager.GetErrorMesg(ErrRes.TYP_OFFSET_NOT_SUPPORTED, new string[]
				{
					4294967294U.ToString()
				}));
			}
			if (count + offset <= buffer.Length)
			{
				this.m_popoLobValCtx->inAmount = (long)count;
			}
			else
			{
				this.m_popoLobValCtx->inAmount = (long)buffer.Length - this.m_popoLobValCtx->src_offset;
			}
			this.m_popoLobValCtx->isFromEF = (bIsFromEF ? 1 : 0);
			this.m_popoLobValCtx->pLobProperties->isUnicode = 1;
			GCHandle gchandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			IntPtr opoLobRefCtx = gchandle.AddrOfPinnedObject();
			try
			{
				num = OpsLob.Write(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx, this.m_popoLobValCtx, opoLobRefCtx);
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
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			this.m_position += this.m_popoLobValCtx->inAmount * 2L;
			if (this.m_popoLobValCtx->inAmount != 0L)
			{
				this.m_isEmpty = false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClob::Write(2)\n"
				});
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0005C488 File Offset: 0x0005B488
		internal unsafe void CreateTempLob()
		{
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
			{
				int num = 0;
				try
				{
					num = OpsLob.CreateTemporary(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx, this.m_popoLobValCtx);
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
				this.m_doneTempLobCreate = true;
				this.m_popoLobValCtx->pLobProperties->isTemporaryLob = 1;
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0005C598 File Offset: 0x0005B598
		internal unsafe int GetOptimumChunkSize()
		{
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
			{
				this.CreateTempLob();
			}
			int num = 0;
			try
			{
				num = OpsLob.GetOptimumChunkSize(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx, this.m_popoLobValCtx);
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
			this.m_optimumChunkSize = (int)this.m_popoLobValCtx->outAmount * 2;
			return this.m_optimumChunkSize;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0005C6B0 File Offset: 0x0005B6B0
		protected override void Dispose(bool disposing)
		{
			bool flag = true;
			if (!this.m_bNotNull)
			{
				return;
			}
			if (!this.m_doneDispose)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleClob::Dispose()\n"
					});
					OraTrace.Trace(1U, new string[]
					{
						" (LOB) Disposing Clob object: " + this.m_opsLobCtx.ToString() + "\n"
					});
				}
				if (this.m_cmd != null)
				{
					try
					{
						this.m_cmd.Dispose();
					}
					catch
					{
					}
					this.m_cmd = null;
				}
				if (this.m_isInChunkWriteMode)
				{
					try
					{
						this.EndChunkWrite();
					}
					catch
					{
					}
				}
				try
				{
					if (OracleConnection.IsAvailable && this.m_connection != null && this.m_connection.m_extProcEnv != null)
					{
						Monitor.Enter(this.m_connection.m_extProcEnv);
						flag = this.m_connection.m_extProcEnv.m_status;
					}
					if (this.m_allocOciLobLoc == 1)
					{
						try
						{
							if (flag)
							{
								OpsLob.FreeTemporary(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx);
							}
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
						}
						this.m_doneTempLobCreate = false;
					}
					try
					{
						OpsLob.FreeAllLobCtx(this.m_opsErrCtx, this.m_popoLobValCtx, this.m_opsLobCtx, 0, flag ? this.m_allocOciLobLoc : 0, flag ? 1 : 0);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
					}
				}
				finally
				{
					if (OracleConnection.IsAvailable && this.m_connection != null && this.m_connection.m_extProcEnv != null)
					{
						Monitor.Exit(this.m_connection.m_extProcEnv);
					}
				}
				if (disposing)
				{
					this.m_popoLobValCtx = null;
					this.m_opsLobCtx = IntPtr.Zero;
					this.m_opsErrCtx = IntPtr.Zero;
					this.m_connection = null;
				}
				try
				{
					OpsCon.RelRef(ref this.m_opsConCtx);
				}
				catch (Exception ex3)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex3);
					}
				}
				this.m_doneDispose = true;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleClob::Dispose()\n"
					});
				}
			}
		}

		// Token: 0x04000839 RID: 2105
		public const long MaxSize = 4294967295L;

		// Token: 0x0400083A RID: 2106
		internal int m_allocOciLobLoc;

		// Token: 0x0400083B RID: 2107
		private IntPtr m_opsLobCtx;

		// Token: 0x0400083C RID: 2108
		private IntPtr m_opsErrCtx;

		// Token: 0x0400083D RID: 2109
		private IntPtr m_opsConCtx;

		// Token: 0x0400083E RID: 2110
		private unsafe OpoLobValCtx* m_popoLobValCtx;

		// Token: 0x0400083F RID: 2111
		internal OracleConnection m_connection;

		// Token: 0x04000840 RID: 2112
		private bool m_doneDispose;

		// Token: 0x04000841 RID: 2113
		internal bool m_isEmpty;

		// Token: 0x04000842 RID: 2114
		private bool m_isNClob;

		// Token: 0x04000843 RID: 2115
		internal bool m_isTemporaryLob;

		// Token: 0x04000844 RID: 2116
		internal bool m_doneTempLobCreate;

		// Token: 0x04000845 RID: 2117
		private long m_length;

		// Token: 0x04000846 RID: 2118
		private long m_position;

		// Token: 0x04000847 RID: 2119
		private bool m_caching;

		// Token: 0x04000848 RID: 2120
		private bool m_isInChunkWriteMode;

		// Token: 0x04000849 RID: 2121
		internal int m_conSignature;

		// Token: 0x0400084A RID: 2122
		private int m_optimumChunkSize;

		// Token: 0x0400084B RID: 2123
		private OracleCommand m_cmd;

		// Token: 0x0400084C RID: 2124
		private bool m_bNotNull;

		// Token: 0x0400084D RID: 2125
		public new static readonly OracleClob Null = new OracleClob();
	}
}
