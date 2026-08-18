using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x0200011D RID: 285
	public sealed class OracleRefCursor : MarshalByRefObject, IDisposable, INullable
	{
		// Token: 0x06000B66 RID: 2918 RVA: 0x00073BF3 File Offset: 0x00072BF3
		static OracleRefCursor()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x00073C0B File Offset: 0x00072C0B
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x00073C18 File Offset: 0x00072C18
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

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x00073CAE File Offset: 0x00072CAE
		// (set) Token: 0x06000B69 RID: 2921 RVA: 0x00073C71 File Offset: 0x00072C71
		public long FetchSize
		{
			get
			{
				if (this.m_doneDispose)
				{
					throw new InvalidOperationException();
				}
				return this.m_fetchSize;
			}
			set
			{
				if (this.m_doneDispose)
				{
					throw new InvalidOperationException();
				}
				if (value <= 0L)
				{
					throw new ArgumentException();
				}
				if (this.m_cachedReader != null)
				{
					this.m_cachedReader.FetchSize = value;
				}
				this.m_fetchSize = value;
				this.m_bFetchSizePropertySet = true;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x00073CC4 File Offset: 0x00072CC4
		public long RowSize
		{
			get
			{
				if (this.m_rowSize == 0L)
				{
					this.m_RowSizeGetInvoked = true;
					this.GetDataReader(true);
					this.m_rowSize = this.m_cachedReader.RowSize;
					this.m_RowSizeGetInvoked = false;
				}
				return this.m_rowSize;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x00073CFD File Offset: 0x00072CFD
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x00073D05 File Offset: 0x00072D05
		internal IntPtr SqlCtx
		{
			get
			{
				return this.m_opsSqlCtx;
			}
			set
			{
				this.m_opsSqlCtx = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x00073D0E File Offset: 0x00072D0E
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x00073D16 File Offset: 0x00072D16
		internal int FreeSqlCtx
		{
			get
			{
				return this.m_freeOpsSqlCtx;
			}
			set
			{
				this.m_freeOpsSqlCtx = value;
			}
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00073D1F File Offset: 0x00072D1F
		private OracleRefCursor()
		{
			this.m_bNotNull = false;
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00073D35 File Offset: 0x00072D35
		internal unsafe OracleRefCursor(OracleConnection con, IntPtr opsSqlCtx, OpoSqlValCtx* pOpoSqlValCtx, string cmdText, string posOrName) : this(con, opsSqlCtx, pOpoSqlValCtx, cmdText, posOrName, 1)
		{
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00073D48 File Offset: 0x00072D48
		internal unsafe OracleRefCursor(OracleConnection con, IntPtr opsSqlCtx, OpoSqlValCtx* pOpoSqlValCtx, string cmdText, string posOrName, int freeOpsSqlCtx)
		{
			this.m_connection = con;
			this.m_conSignature = con.m_conSignature;
			this.m_state = OraRefCursorState.Open;
			this.m_opsSqlCtx = opsSqlCtx;
			this.m_pOpoSqlValCtx = pOpoSqlValCtx;
			this.m_freeOpsSqlCtx = freeOpsSqlCtx;
			this.m_fetchSize = pOpoSqlValCtx->FetchSize;
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
			bool flag = false;
			int num2 = -1;
			if (int.TryParse(posOrName, out num2))
			{
				flag = true;
			}
			StoredProcedureInfo storedProcInfo = RegAndConfigRdr.GetStoredProcInfo(cmdText);
			if (storedProcInfo != null)
			{
				foreach (object obj in storedProcInfo.refCursors)
				{
					RefCursorInfo refCursorInfo = (RefCursorInfo)obj;
					if (flag)
					{
						if (refCursorInfo.position == num2)
						{
							this.m_refCursorInfo = refCursorInfo;
						}
					}
					else if (refCursorInfo.name == posOrName)
					{
						this.m_refCursorInfo = refCursorInfo;
					}
				}
			}
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00073ECC File Offset: 0x00072ECC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00073EDB File Offset: 0x00072EDB
		public OracleDataReader GetDataReader()
		{
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			return this.GetDataReader(false);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00073EF4 File Offset: 0x00072EF4
		[MethodImpl(MethodImplOptions.Synchronized)]
		internal OracleDataReader GetDataReader(bool fillRequest)
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
			if (!(this.m_opsSqlCtx == IntPtr.Zero))
			{
				IntPtr[] opsSqlCtx = new IntPtr[]
				{
					this.m_opsSqlCtx
				};
				OracleDataReader oracleDataReader = new OracleDataReader(this.m_connection, opsSqlCtx, IntPtr.Zero, IntPtr.Zero, this.m_pOpoSqlValCtx, null, null, 1, CommandBehavior.Default, null, null, this.m_freeOpsSqlCtx, this.m_bFetchSizePropertySet);
				oracleDataReader.RefCursor = this;
				oracleDataReader.m_fetchSize = this.m_fetchSize;
				if (fillRequest)
				{
					this.m_cachedReader = oracleDataReader;
				}
				else
				{
					this.m_cachedReader = null;
				}
				this.m_opsSqlCtx = IntPtr.Zero;
				this.m_pOpoSqlValCtx = null;
				if (this.m_rowSize == 0L)
				{
					this.m_rowSize = oracleDataReader.RowSize;
				}
				return oracleDataReader;
			}
			if ((!fillRequest || this.m_cachedReader == null) && !this.m_RowSizeGetInvoked)
			{
				throw new InvalidOperationException();
			}
			if (this.m_state != OraRefCursorState.Open)
			{
				if (this.m_cachedReader != null && !this.m_cachedReader.IsClosed)
				{
					this.m_cachedReader.Close();
				}
				this.m_cachedReader = null;
				throw new InvalidOperationException();
			}
			return this.m_cachedReader;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00074078 File Offset: 0x00073078
		private void Dispose(bool disposing)
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
						" (ENTRY) OracleRefCursor::Dispose()\n"
					});
				}
				try
				{
					if (OracleConnection.IsAvailable && this.m_connection != null && this.m_connection.m_extProcEnv != null)
					{
						Monitor.Enter(this.m_connection.m_extProcEnv);
						flag = this.m_connection.m_extProcEnv.m_status;
					}
					if (this.m_freeOpsSqlCtx == 1 && this.m_opsSqlCtx != IntPtr.Zero)
					{
						try
						{
							if (flag)
							{
								OpsSql.FreeCtx(ref this.m_opsSqlCtx, this.m_connection.m_opoConCtx.opsErrCtx, 0);
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
						try
						{
							OpsSql.FreeValCtx(this.m_pOpoSqlValCtx, flag ? 1 : 0);
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
					}
					catch
					{
					}
					this.m_cachedReader = null;
					this.m_connection = null;
					this.m_state = OraRefCursorState.Closed;
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
						" (EXIT)  OracleRefCursor::Dispose()\n"
					});
				}
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00074288 File Offset: 0x00073288
		~OracleRefCursor()
		{
			this.Dispose(false);
		}

		// Token: 0x04000944 RID: 2372
		private int m_freeOpsSqlCtx;

		// Token: 0x04000945 RID: 2373
		private IntPtr m_opsSqlCtx;

		// Token: 0x04000946 RID: 2374
		private unsafe OpoSqlValCtx* m_pOpoSqlValCtx;

		// Token: 0x04000947 RID: 2375
		internal OracleConnection m_connection;

		// Token: 0x04000948 RID: 2376
		internal OraRefCursorState m_state;

		// Token: 0x04000949 RID: 2377
		private bool m_doneDispose;

		// Token: 0x0400094A RID: 2378
		internal int m_conSignature;

		// Token: 0x0400094B RID: 2379
		private OracleDataReader m_cachedReader;

		// Token: 0x0400094C RID: 2380
		private IntPtr m_opsConCtx;

		// Token: 0x0400094D RID: 2381
		private bool m_bNotNull = true;

		// Token: 0x0400094E RID: 2382
		private long m_rowSize;

		// Token: 0x0400094F RID: 2383
		private long m_fetchSize;

		// Token: 0x04000950 RID: 2384
		private bool m_RowSizeGetInvoked;

		// Token: 0x04000951 RID: 2385
		private bool m_bFetchSizePropertySet;

		// Token: 0x04000952 RID: 2386
		internal RefCursorInfo m_refCursorInfo;

		// Token: 0x04000953 RID: 2387
		public static readonly OracleRefCursor Null = new OracleRefCursor();
	}
}
