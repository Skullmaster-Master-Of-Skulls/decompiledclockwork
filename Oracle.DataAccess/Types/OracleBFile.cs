using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000020 RID: 32
	public sealed class OracleBFile : Stream, ICloneable, INullable
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00011AF8 File Offset: 0x00010AF8
		static OracleBFile()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00011B10 File Offset: 0x00010B10
		public OracleBFile(OracleConnection con) : this(con, string.Empty, string.Empty)
		{
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00011B24 File Offset: 0x00010B24
		public unsafe OracleBFile(OracleConnection con, string directoryName, string fileName)
		{
			this.m_bNotNull = true;
			base..ctor();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBFile::OracleBFile(2)\n"
				});
			}
			if (con == null)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentNullException("con", null);
			}
			this.m_connection = con;
			this.m_conSignature = con.m_conSignature;
			this.m_directoryName = directoryName;
			this.m_fileName = fileName;
			this.m_allocOciLobLoc = 1;
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
				num2 = OpsLob.AllocAllLobCtx(this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_popoLobValCtx, ref this.m_opsLobCtx, 1, IntPtr.Zero, this.m_allocOciLobLoc);
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
			this.m_popoLobValCtx->pLobProperties->lobType = 1;
			this.m_popoLobValCtx->pLobProperties->isTemporaryLob = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleBFile::OracleBFile(2)\n"
				});
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00011D24 File Offset: 0x00010D24
		private OracleBFile()
		{
			this.m_bNotNull = true;
			base..ctor();
			this.m_bNotNull = false;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00011D3C File Offset: 0x00010D3C
		internal unsafe OracleBFile(OracleConnection con, IntPtr opsLobCtx)
		{
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
			this.m_popoLobValCtx = null;
			try
			{
				num2 = OpsLob.AllocAllLobCtx(this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_popoLobValCtx, ref this.m_opsLobCtx, 1, IntPtr.Zero, this.m_allocOciLobLoc);
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
			this.m_popoLobValCtx->pLobProperties->lobType = 1;
			this.m_popoLobValCtx->pLobProperties->isTemporaryLob = 0;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00011EF0 File Offset: 0x00010EF0
		internal unsafe OracleBFile(OracleConnection con, IntPtr opsLobLoc, int allocOciLobLoc)
		{
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
				num2 = OpsLob.AllocAllLobCtx(this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_popoLobValCtx, ref this.m_opsLobCtx, 1, opsLobLoc, this.m_allocOciLobLoc);
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
			this.m_popoLobValCtx->pLobProperties->lobType = 1;
			this.m_popoLobValCtx->pLobProperties->isTemporaryLob = 0;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00012098 File Offset: 0x00011098
		internal OracleBFile(IntPtr opsLobLoc, int allocOciLobLoc) : this(OracleConnection.GetInternalConnection(), opsLobLoc, allocOciLobLoc)
		{
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000120A8 File Offset: 0x000110A8
		~OracleBFile()
		{
			this.Dispose(false);
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000120D8 File Offset: 0x000110D8
		internal IntPtr LobCtx
		{
			get
			{
				return this.m_opsLobCtx;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000120E0 File Offset: 0x000110E0
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000120EC File Offset: 0x000110EC
		public override bool CanRead
		{
			get
			{
				return !this.m_bNotNull || (!this.m_doneDispose && !(this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero) && this.m_connection.m_conSignature == this.m_conSignature);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00012140 File Offset: 0x00011140
		public override bool CanSeek
		{
			get
			{
				return !this.m_bNotNull || (!this.m_doneDispose && !(this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero) && this.m_connection.m_conSignature == this.m_conSignature);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00012191 File Offset: 0x00011191
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00012194 File Offset: 0x00011194
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

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000121F0 File Offset: 0x000111F0
		// (set) Token: 0x06000143 RID: 323 RVA: 0x000122D0 File Offset: 0x000112D0
		public string DirectoryName
		{
			get
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleBFile::DirectoryName: get\n"
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
				if (this.m_directoryName == null)
				{
					this.GetDFNames();
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBFile::DirectoryName: get\n"
					});
				}
				return this.m_directoryName;
			}
			set
			{
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.IsOpen)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.LOB_BFILE_ALREADY_OPEN, new string[0]));
				}
				this.m_directoryName = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0001232C File Offset: 0x0001132C
		public unsafe bool FileExists
		{
			get
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleBFile::FileExists: get\n"
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
							" (EXIT)  OracleBFile::FileExists: get\n"
						});
					}
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
				int num = 0;
				try
				{
					num = OpsLob.FileExists(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx, this.m_popoLobValCtx);
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
				if (this.m_popoLobValCtx->pLobProperties->exists == 1)
				{
					this.m_fileExists = true;
				}
				else
				{
					this.m_fileExists = false;
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBFile::FileExists: get\n"
					});
				}
				return this.m_fileExists;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000124A4 File Offset: 0x000114A4
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00012584 File Offset: 0x00011584
		public string FileName
		{
			get
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleBFile::FileName: get\n"
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
				if (this.m_fileName == null)
				{
					this.GetDFNames();
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBFile::FileName: get\n"
					});
				}
				return this.m_fileName;
			}
			set
			{
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.IsOpen)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.LOB_BFILE_ALREADY_OPEN, new string[0]));
				}
				this.m_fileName = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000125E0 File Offset: 0x000115E0
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

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00012634 File Offset: 0x00011634
		public bool IsOpen
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
				return this.m_isOpen;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000126C8 File Offset: 0x000116C8
		public unsafe override long Length
		{
			get
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleBFile::Length: get\n"
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
							" (EXIT)  OracleBFile::Length: get\n"
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
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBFile::Length: get\n"
					});
				}
				return this.m_length;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0001282C File Offset: 0x0001182C
		public byte[] Value
		{
			get
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleBFile::Value: get\n"
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
				bool flag = false;
				if (!this.m_isOpen)
				{
					this.OpenFile();
					flag = true;
				}
				this.Read(array, 0, num);
				if (flag)
				{
					this.CloseFile();
				}
				this.m_position = position;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBFile::Value: get\n"
					});
				}
				return array;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00012964 File Offset: 0x00011964
		// (set) Token: 0x0600014C RID: 332 RVA: 0x000129F8 File Offset: 0x000119F8
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

		// Token: 0x0600014D RID: 333 RVA: 0x00012A95 File Offset: 0x00011A95
		internal void KeepOciLobLoc()
		{
			this.m_allocOciLobLoc = 0;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00012AA0 File Offset: 0x00011AA0
		internal int GetLobLocator(out IntPtr opsLobCtx)
		{
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			opsLobCtx = IntPtr.Zero;
			return OpsLob.GetLobLocator(this.LobCtx, ref opsLobCtx);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00012AD8 File Offset: 0x00011AD8
		public object Clone()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBFile::Clone()\n"
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
						" (EXIT)  OracleBFile::Clone()\n"
					});
				}
				return OracleBFile.Null;
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			OracleBFile oracleBFile = new OracleBFile(this.m_connection);
			int num = 0;
			try
			{
				num = OpsLob.LocatorAssign(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx, oracleBFile.LobCtx);
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
			oracleBFile.Position = this.m_position;
			oracleBFile.m_directoryName = this.m_directoryName;
			oracleBFile.m_fileName = this.m_fileName;
			oracleBFile.m_bNotNull = this.m_bNotNull;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleBFile::Clone()\n"
				});
			}
			return oracleBFile;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00012C68 File Offset: 0x00011C68
		public override void Close()
		{
			this.Dispose();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00012C70 File Offset: 0x00011C70
		public void CloseFile()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBFile::CloseFile()\n"
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
			if (this.m_isOpen)
			{
				int num = 0;
				try
				{
					num = OpsLob.CloseFile(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx);
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
				this.m_isOpen = false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleBFile::CloseFile()\n"
				});
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00012DAC File Offset: 0x00011DAC
		public int Compare(long src_offset, OracleBFile obj, long dst_offset, long amount)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBFile::Compare()\n"
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
						" (EXIT)  OracleBFile::IsEqual()\n"
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
				if (obj.m_connection != this.m_connection)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_DIFFERENT_CONNECTIONS, new string[0]));
				}
				if (src_offset < 0L || dst_offset < 0L || amount < 0L)
				{
					throw new ArgumentOutOfRangeException(null, null);
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
					this.m_cmd.Parameters.Add("provided_bfile", OracleDbType.BFile, obj, ParameterDirection.Input);
					this.m_cmd.Parameters.Add("current_bfile", OracleDbType.BFile, this, ParameterDirection.Input);
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
							" (EXIT)  OracleBFile::Compare()\n"
						});
					}
				}
				return num;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00013050 File Offset: 0x00012050
		public long CopyTo(OracleBlob obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBFile::CopyTo(1)\n"
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
					" (EXIT)  OracleBFile::CopyTo(1)\n"
				});
			}
			return result;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000130CC File Offset: 0x000120CC
		public long CopyTo(OracleBlob obj, long dst_offset)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBFile::CopyTo(2)\n"
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
					" (EXIT)  OracleBFile::CopyTo(2)\n"
				});
			}
			return result;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00013144 File Offset: 0x00012144
		public long CopyTo(OracleClob obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBFile::CopyTo(3)\n"
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
					" (EXIT)  OracleBFile::CopyTo(3)\n"
				});
			}
			return result;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000131C0 File Offset: 0x000121C0
		public long CopyTo(OracleClob obj, long dst_offset)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBFile::CopyTo(4)\n"
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
					" (EXIT)  OracleBFile::CopyTo(4)\n"
				});
			}
			return result;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00013238 File Offset: 0x00012238
		public unsafe long CopyTo(long src_offset, OracleBlob obj, long dst_offset, long amount)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBFile::CopyTo(5)\n"
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
			if (src_offset < 0L || dst_offset < 0L || amount < 0L)
			{
				throw new ArgumentOutOfRangeException(null, null);
			}
			if (obj.m_connection != this.m_connection && (!obj.m_connection.m_contextConnection || !this.m_connection.m_contextConnection))
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_DIFFERENT_CONNECTIONS, new string[0]));
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
				num = OpsLob.LoadFromFile(this.m_opsConCtx, this.m_opsErrCtx, obj.LobCtx, this.m_opsLobCtx, this.m_popoLobValCtx);
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
					" (EXIT)  OracleBFile::CopyTo(5)\n"
				});
			}
			return this.m_popoLobValCtx->inAmount;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00013424 File Offset: 0x00012424
		public unsafe long CopyTo(long src_offset, OracleClob obj, long amount, long dst_offset)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBFile::CopyTo(6)\n"
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
			if (src_offset < 0L || dst_offset < 0L || amount < 0L)
			{
				throw new ArgumentOutOfRangeException(null, null);
			}
			if (obj.m_connection != this.m_connection && (!obj.m_connection.m_contextConnection || !this.m_connection.m_contextConnection))
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_DIFFERENT_CONNECTIONS, new string[0]));
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
				num = OpsLob.LoadFromFile(this.m_opsConCtx, this.m_opsErrCtx, obj.LobCtx, this.m_opsLobCtx, this.m_popoLobValCtx);
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
					" (EXIT)  OracleBFile::CopyTo(6)\n"
				});
			}
			return this.m_popoLobValCtx->inAmount;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00013610 File Offset: 0x00012610
		public new void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0001361F File Offset: 0x0001261F
		public override void Flush()
		{
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00013624 File Offset: 0x00012624
		public unsafe bool IsEqual(OracleBFile obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBFile::IsEqual()\n"
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
						" (EXIT)  OracleBFile::IsEqual()\n"
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
			int num = 0;
			this.SetDFNames();
			obj.SetDFNames();
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
						" (EXIT)  OracleBFile::IsEqual()\n"
					});
				}
				return true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleBFile::IsEqual()\n"
				});
			}
			return false;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00013814 File Offset: 0x00012814
		public void OpenFile()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBFile::OpenFile()\n"
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
			if (this.IsOpen)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBFile::OpenFile()\n"
					});
				}
				return;
			}
			int num = 0;
			this.SetDFNames();
			num = 0;
			try
			{
				num = OpsLob.OpenFile(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx, this.m_popoLobValCtx);
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
			this.m_isOpen = true;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleBFile::OpenFile()\n"
				});
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0001397C File Offset: 0x0001297C
		public unsafe override int Read(byte[] buffer, int offset, int count)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBFile::Read()\n"
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
						" (EXIT)  OracleBFile::Read()\n"
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
			if (count == 0)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBFile::Read()\n"
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
					" (EXIT)  OracleBFile::Read()\n"
				});
			}
			return (int)this.m_popoLobValCtx->outAmount;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00013BC4 File Offset: 0x00012BC4
		public long Search(byte[] val, long offset, long nth)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBFile::Search()\n"
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
						" (EXIT)  OracleBFile::Search()\n"
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
				this.m_cmd.Parameters.Add("current_bfile", OracleDbType.BFile, this, ParameterDirection.Input);
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
						" (EXIT)  OracleBFile::Search()\n"
					});
				}
			}
			return num;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00013E08 File Offset: 0x00012E08
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBFile::Seek()\n"
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
						" (EXIT)  OracleBFile::Seek()\n"
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
					" (EXIT)  OracleBFile::Seek()\n"
				});
			}
			return this.m_position;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00013F1F File Offset: 0x00012F1F
		public override void SetLength(long newLength)
		{
			throw new NotSupportedException(null, null);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00013F28 File Offset: 0x00012F28
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException(null, null);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00013F34 File Offset: 0x00012F34
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
						" (ENTRY) OracleBFile::Dispose()\n"
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
				try
				{
					if (OracleConnection.IsAvailable && this.m_connection != null && this.m_connection.m_extProcEnv != null)
					{
						Monitor.Enter(this.m_connection.m_extProcEnv);
						flag = this.m_connection.m_extProcEnv.m_status;
					}
					if (this.m_allocOciLobLoc == 1 && this.m_isOpen)
					{
						try
						{
							if (flag)
							{
								OpsLob.CloseFile(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx);
							}
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
						}
						this.m_isOpen = false;
					}
					try
					{
						OpsLob.FreeAllLobCtx(this.m_opsErrCtx, this.m_popoLobValCtx, this.m_opsLobCtx, 1, flag ? this.m_allocOciLobLoc : 0, flag ? 1 : 0);
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
						" (EXIT)  OracleBFile::Dispose()\n"
					});
				}
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00014134 File Offset: 0x00013134
		internal unsafe void GetDFNames()
		{
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			char[] value = new char[31];
			IntPtr directoryName = IntPtr.Zero;
			int length = 0;
			char[] value2 = new char[256];
			IntPtr fileName = IntPtr.Zero;
			int length2 = 0;
			GCHandle gchandle = GCHandle.Alloc(value, GCHandleType.Pinned);
			GCHandle gchandle2 = GCHandle.Alloc(value2, GCHandleType.Pinned);
			directoryName = gchandle.AddrOfPinnedObject();
			fileName = gchandle2.AddrOfPinnedObject();
			int num = 0;
			try
			{
				num = OpsLob.FileGetName(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx, directoryName, &length, fileName, &length2);
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
				if (gchandle2.IsAllocated)
				{
					gchandle2.Free();
				}
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			this.m_directoryName = new string(value, 0, length);
			this.m_fileName = new string(value2, 0, length2);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00014294 File Offset: 0x00013294
		internal void SetDFNames()
		{
			int num = 0;
			if (this.m_directoryName == null || this.m_fileName == null)
			{
				this.GetDFNames();
			}
			if (this.m_directoryName != null && this.m_directoryName.Length != 0 && this.m_fileName != null && this.m_fileName.Length != 0)
			{
				try
				{
					num = OpsLob.FileSetName(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsLobCtx, this.m_directoryName, 0, this.m_fileName, 0);
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
				this.m_fileExists = true;
			}
		}

		// Token: 0x040000C0 RID: 192
		public const long MaxSize = 4294967295L;

		// Token: 0x040000C1 RID: 193
		internal int m_allocOciLobLoc;

		// Token: 0x040000C2 RID: 194
		private IntPtr m_opsLobCtx;

		// Token: 0x040000C3 RID: 195
		private IntPtr m_opsErrCtx;

		// Token: 0x040000C4 RID: 196
		private IntPtr m_opsConCtx;

		// Token: 0x040000C5 RID: 197
		private unsafe OpoLobValCtx* m_popoLobValCtx;

		// Token: 0x040000C6 RID: 198
		private OracleCommand m_cmd;

		// Token: 0x040000C7 RID: 199
		internal OracleConnection m_connection;

		// Token: 0x040000C8 RID: 200
		private string m_directoryName;

		// Token: 0x040000C9 RID: 201
		private bool m_doneDispose;

		// Token: 0x040000CA RID: 202
		private bool m_fileExists;

		// Token: 0x040000CB RID: 203
		private string m_fileName;

		// Token: 0x040000CC RID: 204
		private long m_length;

		// Token: 0x040000CD RID: 205
		private long m_position;

		// Token: 0x040000CE RID: 206
		internal bool m_isEmpty;

		// Token: 0x040000CF RID: 207
		private bool m_isOpen;

		// Token: 0x040000D0 RID: 208
		internal int m_conSignature;

		// Token: 0x040000D1 RID: 209
		private bool m_bNotNull;

		// Token: 0x040000D2 RID: 210
		public new static readonly OracleBFile Null = new OracleBFile();
	}
}
