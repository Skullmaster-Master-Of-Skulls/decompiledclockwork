using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000077 RID: 119
	public sealed class OracleXmlStream : Stream, ICloneable
	{
		// Token: 0x0600054A RID: 1354 RVA: 0x0003B2C0 File Offset: 0x0003A2C0
		static OracleXmlStream()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0003B2D0 File Offset: 0x0003A2D0
		public OracleXmlStream(OracleXmlType xmlType)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlStream::OracleXmlType(xmlType)\n"
				});
			}
			OracleConnection connection = xmlType.m_connection;
			if (connection == null)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException();
			}
			this.m_bFreeOciXmlType = xmlType.m_bFreeOciXmlType;
			this.m_opsConCtx = connection.m_opoConCtx.opsConCtx;
			if (this.m_opsConCtx == IntPtr.Zero)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			int num = 0;
			try
			{
				this.m_opsXmlStreamCtx = IntPtr.Zero;
				this.m_opsErrCtx = IntPtr.Zero;
				num = OpsXmlStream.AllocCtx(this.m_opsConCtx, xmlType.OpsXmlTypeCtx, ref this.m_opsErrCtx, ref this.m_opsXmlStreamCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				GC.SuppressFinalize(this);
				throw;
			}
			finally
			{
				if (num != 0 && num != ErrRes.INT_ERR)
				{
					GC.SuppressFinalize(this);
					throw new OracleTypeException(num, new object[]
					{
						"xmlType"
					});
				}
			}
			this.m_opsXmlTypeCtx = xmlType.OpsXmlTypeCtx;
			this.m_popoXmlStreamReadParamList = null;
			try
			{
				num = OpsXmlStream.AllocReadParamList(ref this.m_popoXmlStreamReadParamList);
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				GC.SuppressFinalize(this);
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num != 0)
				{
					try
					{
						OpsXmlStream.FreeCtx(ref this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_opsXmlTypeCtx, ref this.m_opsXmlStreamCtx, this.m_bFreeOciXmlType, 1);
					}
					catch (Exception ex3)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex3);
						}
					}
					if (num != ErrRes.INT_ERR)
					{
						GC.SuppressFinalize(this);
						throw new OracleTypeException(num, new object[]
						{
							"xmlType"
						});
					}
				}
			}
			this.m_connection = connection;
			this.m_conSignature = connection.m_conSignature;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlStream::OracleXmlStream(xmlType)\n"
				});
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0003B500 File Offset: 0x0003A500
		internal OracleXmlStream(OracleConnection con, IntPtr opsXmlTypeCtx)
		{
			this.m_bFreeOciXmlType = 1;
			if (con == null)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException();
			}
			this.m_opsConCtx = con.m_opoConCtx.opsConCtx;
			if (this.m_opsConCtx == IntPtr.Zero)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			int num = 0;
			try
			{
				this.m_opsXmlStreamCtx = IntPtr.Zero;
				this.m_opsErrCtx = IntPtr.Zero;
				num = OpsXmlStream.AllocCtx(this.m_opsConCtx, opsXmlTypeCtx, ref this.m_opsErrCtx, ref this.m_opsXmlStreamCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				GC.SuppressFinalize(this);
				throw;
			}
			finally
			{
				if (num != 0 && num != ErrRes.INT_ERR)
				{
					GC.SuppressFinalize(this);
					throw new OracleTypeException(num, new object[]
					{
						"con"
					});
				}
			}
			this.m_opsXmlTypeCtx = opsXmlTypeCtx;
			this.m_popoXmlStreamReadParamList = null;
			try
			{
				OpsXmlStream.AllocReadParamList(ref this.m_popoXmlStreamReadParamList);
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				GC.SuppressFinalize(this);
				throw;
			}
			this.m_connection = con;
			this.m_conSignature = con.m_conSignature;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0003B650 File Offset: 0x0003A650
		public override bool CanRead
		{
			get
			{
				return !this.m_doneDispose && !(this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero) && this.m_conSignature == this.m_connection.m_conSignature;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0003B690 File Offset: 0x0003A690
		public override bool CanSeek
		{
			get
			{
				return !this.m_doneDispose && !(this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero) && this.m_conSignature == this.m_connection.m_conSignature;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0003B6D0 File Offset: 0x0003A6D0
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0003B6D3 File Offset: 0x0003A6D3
		public OracleConnection Connection
		{
			get
			{
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				return this.m_connection;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0003B6F4 File Offset: 0x0003A6F4
		public override long Length
		{
			get
			{
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
				if (this.m_conSignature != this.m_connection.m_conSignature)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
				}
				int num = 0;
				int num2 = 0;
				try
				{
					num = OpsXmlStream.GetLength(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsXmlTypeCtx, ref num2);
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
				this.m_length = (long)(num2 * 2);
				return this.m_length;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0003B7EC File Offset: 0x0003A7EC
		public string Value
		{
			get
			{
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
				if (this.m_conSignature != this.m_connection.m_conSignature)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
				}
				IntPtr zero = IntPtr.Zero;
				string result = null;
				int num = 0;
				int num2 = 0;
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
					num2 = OpsXmlStream.GetValueBuffer(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsXmlTypeCtx, ref zero, ref num);
					if (num2 == 0)
					{
						if (num > 0 && zero != IntPtr.Zero)
						{
							result = Marshal.PtrToStringUni(zero, num);
							num2 = OpsXmlStream.FreeValueBuffer(ref zero);
						}
						else
						{
							result = string.Empty;
						}
					}
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
					if (num2 != 0)
					{
						OracleException.HandleError(num2, this.m_connection, IntPtr.Zero, this);
					}
				}
				return result;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0003B968 File Offset: 0x0003A968
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x0003B9F0 File Offset: 0x0003A9F0
		public unsafe override long Position
		{
			get
			{
				if (this.m_doneDispose)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
				if (this.m_conSignature != this.m_connection.m_conSignature)
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
				if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
				if (this.m_conSignature != this.m_connection.m_conSignature)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
				}
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("Position");
				}
				if (this.m_position != value)
				{
					this.m_popoXmlStreamReadParamList->bOverflow = 0;
				}
				this.m_position = value;
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0003BAA0 File Offset: 0x0003AAA0
		public object Clone()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlStream::Clone\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_conSignature != this.m_connection.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			OracleXmlStream result = new OracleXmlStream(this.m_connection, this.m_opsXmlTypeCtx);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlStream::Clone\n"
				});
			}
			return result;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0003BB70 File Offset: 0x0003AB70
		public override void Close()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlStream::Close()\n"
				});
			}
			this.Dispose();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleXmlStream::Close()\n"
				});
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0003BBC0 File Offset: 0x0003ABC0
		public new void Dispose()
		{
			bool flag = true;
			if (!this.m_doneDispose)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleXmlStream::Dispose()\n"
					});
				}
				try
				{
					try
					{
						OpsXmlStream.FreeReadParamList(ref this.m_popoXmlStreamReadParamList);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
					}
					try
					{
						if (OracleConnection.IsAvailable && this.m_connection != null && this.m_connection.m_extProcEnv != null)
						{
							Monitor.Enter(this.m_connection.m_extProcEnv);
							flag = this.m_connection.m_extProcEnv.m_status;
						}
						try
						{
							OpsXmlStream.FreeCtx(ref this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_opsXmlTypeCtx, ref this.m_opsXmlStreamCtx, flag ? this.m_bFreeOciXmlType : 0, flag ? 1 : 0);
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
				}
				catch
				{
				}
				this.m_popoXmlStreamReadParamList = null;
				this.m_position = 0L;
				this.m_length = 0L;
				this.m_connection = null;
				this.m_conSignature = 0;
				this.m_doneDispose = true;
				try
				{
					GC.SuppressFinalize(this);
				}
				catch
				{
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleXmlStream::Dispose()\n"
					});
				}
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0003BD64 File Offset: 0x0003AD64
		public override void Flush()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlStream::Flush()\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			throw new NotSupportedException(null, null);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0003BDB0 File Offset: 0x0003ADB0
		public unsafe override int Read(byte[] buffer, int offset, int count)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlStream::Read(byte[], ...)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_conSignature != this.m_connection.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			if (count == 0)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleXmlStream::Read(byte[], ...)\n"
					});
				}
				return 0;
			}
			if (offset < 0 || count < 0 || offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			int num = 0;
			this.m_popoXmlStreamReadParamList->newPosition = this.m_position;
			this.m_popoXmlStreamReadParamList->dst_offset = (long)offset;
			if (count + offset <= buffer.Length)
			{
				this.m_popoXmlStreamReadParamList->inAmount = (long)(count / 2);
				this.m_popoXmlStreamReadParamList->numBytes = (long)count;
			}
			else
			{
				this.m_popoXmlStreamReadParamList->inAmount = (long)((buffer.Length - offset) / 2);
				this.m_popoXmlStreamReadParamList->numBytes = (long)(buffer.Length - count);
			}
			GCHandle gchandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			IntPtr pBuffer = gchandle.AddrOfPinnedObject();
			try
			{
				num = OpsXmlStream.ReadBytes(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsXmlStreamCtx, this.m_opsXmlTypeCtx, pBuffer, ref this.m_popoXmlStreamReadParamList);
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
			this.m_position += this.m_popoXmlStreamReadParamList->outAmount;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleXmlStream::Read(byte[], ...)\n"
				});
			}
			return (int)this.m_popoXmlStreamReadParamList->outAmount;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0003BFC8 File Offset: 0x0003AFC8
		public unsafe int Read(char[] buffer, int offset, int count)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlStream::Read(char[], ...)\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_conSignature != this.m_connection.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			if (count == 0)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleXmlStream::Read(char[], ...)\n"
					});
				}
				return 0;
			}
			if (offset < 0 || count < 0 || offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (this.m_position % 2L != 0L)
			{
				throw new ArgumentOutOfRangeException(null, OpoErrResManager.GetErrorMesg(ErrRes.EVEN_VALUE_PARAM_REQUIRED, new string[0]));
			}
			int num = 0;
			this.m_popoXmlStreamReadParamList->newPosition = this.m_position;
			this.m_popoXmlStreamReadParamList->dst_offset = (long)offset;
			if (count + offset <= buffer.Length)
			{
				this.m_popoXmlStreamReadParamList->inAmount = (long)count;
			}
			else
			{
				this.m_popoXmlStreamReadParamList->inAmount = (long)(buffer.Length - offset);
			}
			GCHandle gchandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			IntPtr pBuffer = gchandle.AddrOfPinnedObject();
			try
			{
				num = OpsXmlStream.ReadChars(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsXmlStreamCtx, this.m_opsXmlTypeCtx, pBuffer, ref this.m_popoXmlStreamReadParamList);
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
			this.m_position += this.m_popoXmlStreamReadParamList->outAmount * 2L;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleXmlStream::Read(char[], ...)\n"
				});
			}
			return (int)this.m_popoXmlStreamReadParamList->outAmount;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0003C1E4 File Offset: 0x0003B1E4
		public unsafe override long Seek(long offset, SeekOrigin origin)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlStream::Seek()\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_conSignature != this.m_connection.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			long position = this.m_position;
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
			if (this.m_position < 0L)
			{
				this.m_position = 0L;
				throw new ArgumentOutOfRangeException("offset");
			}
			if (this.m_position != position)
			{
				this.m_popoXmlStreamReadParamList->bOverflow = 0;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleXmlStream::Seek()\n"
				});
			}
			return this.m_position;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0003C30C File Offset: 0x0003B30C
		public override void SetLength(long newLength)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlStream::SetLength()\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			throw new NotSupportedException(null, null);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0003C358 File Offset: 0x0003B358
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleXmlStream::Write()\n"
				});
			}
			if (this.m_doneDispose)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			throw new NotSupportedException(null, null);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0003C3A4 File Offset: 0x0003B3A4
		~OracleXmlStream()
		{
			this.Dispose();
		}

		// Token: 0x0400038B RID: 907
		private int m_bFreeOciXmlType;

		// Token: 0x0400038C RID: 908
		private IntPtr m_opsXmlStreamCtx;

		// Token: 0x0400038D RID: 909
		private IntPtr m_opsErrCtx;

		// Token: 0x0400038E RID: 910
		private IntPtr m_opsConCtx;

		// Token: 0x0400038F RID: 911
		private IntPtr m_opsXmlTypeCtx;

		// Token: 0x04000390 RID: 912
		private bool m_doneDispose;

		// Token: 0x04000391 RID: 913
		private int m_conSignature;

		// Token: 0x04000392 RID: 914
		private long m_length;

		// Token: 0x04000393 RID: 915
		private long m_position;

		// Token: 0x04000394 RID: 916
		private OracleConnection m_connection;

		// Token: 0x04000395 RID: 917
		private unsafe OpoXmlStreamReadParamList* m_popoXmlStreamReadParamList;
	}
}
