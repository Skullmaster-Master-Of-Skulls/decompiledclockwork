using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x020000D4 RID: 212
	public sealed class OracleBlob : Stream, ICloneable, INullable
	{
		// Token: 0x060007C1 RID: 1985 RVA: 0x0004C9DC File Offset: 0x0004B9DC
		static OracleBlob()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0004C9F6 File Offset: 0x0004B9F6
		public OracleBlob(OracleConnection con) : this(con, IntPtr.Zero, false, true)
		{
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0004CA06 File Offset: 0x0004BA06
		internal OracleBlob() : this(OracleConnection.GetInternalConnection(), IntPtr.Zero, false, true)
		{
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0004CA1A File Offset: 0x0004BA1A
		public OracleBlob(OracleConnection con, bool bCaching) : this(con, IntPtr.Zero, bCaching, true)
		{
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0004CA2A File Offset: 0x0004BA2A
		private OracleBlob(char dummy)
		{
			this.m_bNotNull = true;
			base..ctor();
			this.m_bNotNull = false;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0004CA40 File Offset: 0x0004BA40
		internal unsafe OracleBlob(OracleConnection con, IntPtr opsLobCtx, bool bCaching, bool bTempLob)
		{
			this.m_bNotNull = true;
			base..ctor();
			if (con == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("con", null);
			}
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
			this.m_popoLobValCtx = null;
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
			this.m_popoLobValCtx->pLobProperties->lobType = 2;
			this.m_popoLobValCtx->pLobProperties->isTemporaryLob = 0;
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
					" (LOB) OracleBlob object created: " + this.m_opsLobCtx.ToString() + "\n"
				});
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0004CC7C File Offset: 0x0004BC7C
		internal unsafe OracleBlob(OracleConnection con, IntPtr opsLobLoc, bool bCaching, bool bTempLob, int allocOciLobLoc)
		{
			this.m_bNotNull = true;
			base..ctor();
			if (con == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("con", null);
			}
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
			this.m_popoLobValCtx->pLobProperties->lobType = 2;
			this.m_popoLobValCtx->pLobProperties->isTemporaryLob = 0;
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
					" (LOB) OracleBlob object created: " + this.m_opsLobCtx.ToString() + "\n"
				});
			}
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0004CEAC File Offset: 0x0004BEAC
		internal OracleBlob(IntPtr opsLobLoc, bool bCaching, bool bTempLob, int allocOciLobLoc) : this(OracleConnection.GetInternalConnection(), opsLobLoc, bCaching, bTempLob, allocOciLobLoc)
		{
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0004CEC0 File Offset: 0x0004BEC0
		~OracleBlob()
		{
			this.Dispose(false);
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x0004CEF0 File Offset: 0x0004BEF0
		internal IntPtr LobCtx
		{
			get
			{
				return this.m_opsLobCtx;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x0004CEF8 File Offset: 0x0004BEF8
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x0004CF04 File Offset: 0x0004BF04
		public override bool CanRead
		{
			get
			{
				return !this.m_bNotNull || (!this.m_doneDispose && !(this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero) && this.m_connection.m_conSignature == this.m_conSignature);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x0004CF58 File Offset: 0x0004BF58
		public override bool CanSeek
		{
			get
			{
				return !this.m_bNotNull || (!this.m_doneDispose && !(this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero) && this.m_connection.m_conSignature == this.m_conSignature);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x0004CFAC File Offset: 0x0004BFAC
		public override bool CanWrite
		{
			get
			{
				return this.m_bNotNull && !this.m_doneDispose && !(this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero) && this.m_connection.m_conSignature == this.m_conSignature;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x0004CFFD File Offset: 0x0004BFFD
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

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x0004D038 File Offset: 0x0004C038
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

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0004D094 File Offset: 0x0004C094
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

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0004D0E7 File Offset: 0x0004C0E7
		public bool IsInChunkWriteMode
		{
			get
			{
				return this.m_bNotNull && this.m_isInChunkWriteMode;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x0004D0FC File Offset: 0x0004C0FC
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

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x0004D26C File Offset: 0x0004C26C
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
				this.m_length = this.m_popoLobValCtx->lobDataLength;
				return this.m_length;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x0004D394 File Offset: 0x0004C394
		public byte[] Value
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
					return null;
				}
				long position = this.m_position;
				this.m_position = 0L;
				long length = this.Length;
				int num;
				if (length >= 2147483647L)
				{
					num = int.MaxValue;
				}
				else
				{
					num = (int)length;
				}
				byte[] array = new byte[num];
				this.Read(array, 0, num);
				this.m_position = position;
				return array;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0004D480 File Offset: 0x0004C480
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x0004D514 File Offset: 0x0004C514
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

		// Token: 0x060007D8 RID: 2008 RVA: 0x0004D5B1 File Offset: 0x0004C5B1
		internal void KeepOciLobLoc()
		{
			this.m_allocOciLobLoc = 0;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0004D5BC File Offset: 0x0004C5BC
		internal int GetLobLocator(out IntPtr opsLobCtx)
		{
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			opsLobCtx = IntPtr.Zero;
			return OpsLob.GetLobLocator(this.LobCtx, ref opsLobCtx);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0004D5F4 File Offset: 0x0004C5F4
		public void Append(OracleBlob obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBlob::Append(1)\n"
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
						" (EXIT)  OracleBlob::Append(1)\n"
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
					" (EXIT)  OracleBlob::Append(1)\n"
				});
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0004D7AC File Offset: 0x0004C7AC
		public void Append(byte[] buffer, int offset, int count)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBlob::Append(2)\n"
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
						" (EXIT)  OracleBlob::Append(2)\n"
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
					" (EXIT)  OracleBlob::Append(2)\n"
				});
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0004D8D4 File Offset: 0x0004C8D4
		public object Clone()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBlob::Clone()\n"
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
						" (EXIT)  OracleBlob::Clone()\n"
					});
				}
				return OracleBlob.Null;
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
			OracleBlob oracleBlob = new OracleBlob(this.m_connection, zero, this.m_caching, this.m_isTemporaryLob);
			if (oracleBlob.m_isTemporaryLob)
			{
				oracleBlob.CreateTempLob();
			}
			try
			{
				num = OpsLob.LocatorAssign(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx, oracleBlob.LobCtx);
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
			oracleBlob.m_position = this.m_position;
			oracleBlob.m_bNotNull = this.m_bNotNull;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleBlob::Clone()\n"
				});
			}
			return oracleBlob;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0004DA88 File Offset: 0x0004CA88
		public override void Close()
		{
			this.Dispose();
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0004DA90 File Offset: 0x0004CA90
		public int Compare(long src_offset, OracleBlob obj, long dst_offset, long amount)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBlob::Compare()\n"
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
							" (EXIT)  OracleBlob::Compare()\n"
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
					this.m_cmd.Parameters.Add("provided_blob", OracleDbType.Blob, obj, ParameterDirection.Input);
					this.m_cmd.Parameters.Add("current_blob", OracleDbType.Blob, this, ParameterDirection.Input);
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
							" (EXIT)  OracleBlob::Compare()\n"
						});
					}
				}
				return num;
			}
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0004DDBC File Offset: 0x0004CDBC
		public long CopyTo(OracleBlob obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBlob::CopyTo(1)\n"
				});
			}
			if (obj == null)
			{
				throw new ArgumentNullException();
			}
			if (!this.m_bNotNull || obj.IsNull)
			{
				throw new OracleNullValueException();
			}
			long result = this.CopyTo(0L, obj, 0L, this.Length);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleBlob::CopyTo(1)\n"
				});
			}
			return result;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0004DE38 File Offset: 0x0004CE38
		public long CopyTo(OracleBlob obj, long dst_offset)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBlob::CopyTo(2)\n"
				});
			}
			if (obj == null)
			{
				throw new ArgumentNullException();
			}
			if (!this.m_bNotNull || obj.IsNull)
			{
				throw new OracleNullValueException();
			}
			long result = this.CopyTo(0L, obj, dst_offset, this.Length);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleBlob::CopyTo(2)\n"
				});
			}
			return result;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0004DEB0 File Offset: 0x0004CEB0
		public unsafe long CopyTo(long src_offset, OracleBlob obj, long dst_offset, long amount)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBlob::CopyTo(3)\n"
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
						" (EXIT)  OracleBlob::CopyTo(3)\n"
					});
				}
				return 0L;
			}
			if (obj.m_isTemporaryLob && !obj.m_doneTempLobCreate)
			{
				obj.CreateTempLob();
			}
			int num = 0;
			this.m_popoLobValCtx->inAmount = amount;
			this.m_popoLobValCtx->src_offset = src_offset + 1L;
			this.m_popoLobValCtx->dst_offset = dst_offset + 1L;
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
					" (EXIT)  OracleBlob::CopyTo(3)\n"
				});
			}
			return this.m_popoLobValCtx->inAmount;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0004E0CC File Offset: 0x0004D0CC
		public new void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0004E0DC File Offset: 0x0004D0DC
		public void EndChunkWrite()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBlob::EndChunkWrite()\n"
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
						" (EXIT)  OracleBlob::EndChunkWrite()\n"
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
					" (EXIT)  OracleBlob::EndChunkWrite()\n"
				});
			}
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0004E238 File Offset: 0x0004D238
		public long Erase()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBlob::Erase(1)\n"
				});
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			long result = this.Erase(0L, this.Length);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleBlob::Erase(1)\n"
				});
			}
			return result;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0004E2A0 File Offset: 0x0004D2A0
		public unsafe long Erase(long offset, long amount)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBlob::Erase(2)\n"
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
			if (offset < 0L || amount < 0L)
			{
				throw new ArgumentOutOfRangeException(null, null);
			}
			if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBlob::Erase(2)\n"
					});
				}
				return 0L;
			}
			int num = 0;
			this.m_popoLobValCtx->dst_offset = offset + 1L;
			this.m_popoLobValCtx->inAmount = amount;
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
					" (EXIT)  OracleBlob::Erase(2)\n"
				});
			}
			return this.m_popoLobValCtx->outAmount;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0004E43C File Offset: 0x0004D43C
		public override void Flush()
		{
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0004E440 File Offset: 0x0004D440
		public unsafe bool IsEqual(OracleBlob obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBlob::IsEqual()\n"
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
						" (EXIT)  OracleBlob::IsEqual()\n"
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
						" (EXIT)  OracleBlob::IsEqual()\n"
					});
				}
				return true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleBlob::IsEqual()\n"
				});
			}
			return false;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0004E668 File Offset: 0x0004D668
		public void BeginChunkWrite()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBlob::BeginChunkWrite()\n"
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
						" (EXIT)  OracleBlob::BeginChunkWrite()\n"
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
					" (EXIT)  OracleBlob::BeginChunkWrite()\n"
				});
			}
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0004E7E0 File Offset: 0x0004D7E0
		public unsafe override int Read(byte[] buffer, int offset, int count)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBlob::Read()\n"
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
						" (EXIT)  OracleBlob::Read()\n"
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
				throw new ArgumentOutOfRangeException(null, null);
			}
			if (count == 0 || (this.m_isTemporaryLob && !this.m_doneTempLobCreate))
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBlob::Read()\n"
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
				this.m_popoLobValCtx->src_offset = this.m_position + 1L;
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
			this.m_popoLobValCtx->pLobProperties->isUnicode = 0;
			GCHandle gchandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			IntPtr opoLobRefCtx = gchandle.AddrOfPinnedObject();
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
					" (EXIT)  OracleBlob::Read()\n"
				});
			}
			return (int)this.m_popoLobValCtx->outAmount;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0004EA70 File Offset: 0x0004DA70
		public long Search(byte[] val, long offset, long nth)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBlob::Search()\n"
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
						" (EXIT)  OracleBlob::Search()\n"
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
			if (offset < 0L || nth <= 0L || val.Length > 16383 || nth >= (long)((ulong)-1) || offset >= (long)((ulong)-1))
			{
				throw new ArgumentOutOfRangeException(null, null);
			}
			if (this.m_isTemporaryLob && !this.m_doneTempLobCreate)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBlob::Search()\n"
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
				this.m_cmd.Parameters.Add("current_blob", OracleDbType.Blob, this, ParameterDirection.Input);
				this.m_cmd.Parameters.Add("pattern", OracleDbType.Raw, val, ParameterDirection.Input);
				this.m_cmd.Parameters.Add("current_offset", OracleDbType.Int64, offset, ParameterDirection.Input);
				this.m_cmd.Parameters.Add("occurence", OracleDbType.Int64, nth, ParameterDirection.Input);
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
						" (EXIT)  OracleBlob::Search()\n"
					});
				}
			}
			return num;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0004ECE4 File Offset: 0x0004DCE4
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBlob::Seek()\n"
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
						" (EXIT)  OracleBlob::Seek()\n"
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
						" (EXIT)  OracleBlob::Seek()\n"
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
					" (EXIT)  OracleBlob::Seek()\n"
				});
			}
			return this.m_position;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0004EE2C File Offset: 0x0004DE2C
		public unsafe override void SetLength(long newLength)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBlob::SetLength()\n"
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
						" (EXIT)  OracleBlob::SetLength()\n"
					});
				}
				return;
			}
			int num = 0;
			this.m_popoLobValCtx->inAmount = newLength;
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
			if (this.m_position > newLength)
			{
				this.Seek(0L, SeekOrigin.End);
			}
			if (newLength == 0L)
			{
				this.m_isEmpty = true;
				this.m_length = 0L;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleBlob::SetLength()\n"
				});
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0004EFD0 File Offset: 0x0004DFD0
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.Write(buffer, offset, count, false);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0004EFDC File Offset: 0x0004DFDC
		public unsafe void Write(byte[] buffer, int offset, int count, bool bIsFromEF)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBlob::Write()\n"
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
						" (EXIT)  OracleBlob::Write()\n"
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
				this.m_popoLobValCtx->dst_offset = this.m_position + 1L;
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
				this.m_popoLobValCtx->inAmount = (long)(buffer.Length - offset);
			}
			this.m_popoLobValCtx->isFromEF = (bIsFromEF ? 1 : 0);
			this.m_popoLobValCtx->pLobProperties->isUnicode = 0;
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
			this.m_position += this.m_popoLobValCtx->inAmount;
			if (this.m_popoLobValCtx->inAmount != 0L)
			{
				this.m_isEmpty = false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleBlob::Write()\n"
				});
			}
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0004F274 File Offset: 0x0004E274
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

		// Token: 0x060007F0 RID: 2032 RVA: 0x0004F384 File Offset: 0x0004E384
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
			return this.m_optimumChunkSize = (int)this.m_popoLobValCtx->outAmount;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0004F498 File Offset: 0x0004E498
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
						" (ENTRY) OracleBlob::Dispose()\n"
					});
					OraTrace.Trace(1U, new string[]
					{
						" (LOB) Disposing OracleBlob object: " + this.m_opsLobCtx.ToString() + "\n"
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
						" (EXIT)  OracleBlob::Dispose()\n"
					});
				}
			}
		}

		// Token: 0x04000669 RID: 1641
		public const long MaxSize = 4294967295L;

		// Token: 0x0400066A RID: 1642
		internal int m_allocOciLobLoc;

		// Token: 0x0400066B RID: 1643
		private IntPtr m_opsLobCtx;

		// Token: 0x0400066C RID: 1644
		private IntPtr m_opsErrCtx;

		// Token: 0x0400066D RID: 1645
		private IntPtr m_opsConCtx;

		// Token: 0x0400066E RID: 1646
		private unsafe OpoLobValCtx* m_popoLobValCtx;

		// Token: 0x0400066F RID: 1647
		internal OracleConnection m_connection;

		// Token: 0x04000670 RID: 1648
		private bool m_doneDispose;

		// Token: 0x04000671 RID: 1649
		internal bool m_isEmpty;

		// Token: 0x04000672 RID: 1650
		internal bool m_isTemporaryLob;

		// Token: 0x04000673 RID: 1651
		internal bool m_doneTempLobCreate;

		// Token: 0x04000674 RID: 1652
		private long m_length;

		// Token: 0x04000675 RID: 1653
		private long m_position;

		// Token: 0x04000676 RID: 1654
		private bool m_caching;

		// Token: 0x04000677 RID: 1655
		private bool m_isInChunkWriteMode;

		// Token: 0x04000678 RID: 1656
		internal int m_conSignature;

		// Token: 0x04000679 RID: 1657
		private int m_optimumChunkSize;

		// Token: 0x0400067A RID: 1658
		private OracleCommand m_cmd;

		// Token: 0x0400067B RID: 1659
		private bool m_bNotNull;

		// Token: 0x0400067C RID: 1660
		public new static readonly OracleBlob Null = new OracleBlob('x');
	}
}
