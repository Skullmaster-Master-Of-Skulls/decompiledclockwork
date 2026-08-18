using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Permissions;

namespace System.Net
{
	// Token: 0x020004DE RID: 1246
	internal class FtpDataStream : Stream, ICloseEx
	{
		// Token: 0x060026C6 RID: 9926 RVA: 0x0009FA50 File Offset: 0x0009EA50
		internal FtpDataStream(NetworkStream networkStream, FtpWebRequest request, TriState writeOnly)
		{
			this.m_Readable = true;
			this.m_Writeable = true;
			if (writeOnly == TriState.True)
			{
				this.m_Readable = false;
			}
			else if (writeOnly == TriState.False)
			{
				this.m_Writeable = false;
			}
			this.m_NetworkStream = networkStream;
			this.m_Request = request;
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x0009FA8C File Offset: 0x0009EA8C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					((ICloseEx)this).CloseEx(CloseExState.Normal);
				}
				else
				{
					((ICloseEx)this).CloseEx(CloseExState.Abort | CloseExState.Silent);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x0009FAC8 File Offset: 0x0009EAC8
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			lock (this)
			{
				if (this.m_Closing)
				{
					return;
				}
				this.m_Closing = true;
				this.m_Writeable = false;
				this.m_Readable = false;
			}
			try
			{
				try
				{
					if ((closeState & CloseExState.Abort) == CloseExState.Normal)
					{
						this.m_NetworkStream.Close(-1);
					}
					else
					{
						this.m_NetworkStream.Close(0);
					}
				}
				finally
				{
					this.m_Request.DataStreamClosed(closeState);
				}
			}
			catch (Exception ex)
			{
				bool flag = true;
				WebException ex2 = ex as WebException;
				if (ex2 != null)
				{
					FtpWebResponse ftpWebResponse = ex2.Response as FtpWebResponse;
					if (ftpWebResponse != null && !this.m_IsFullyRead && ftpWebResponse.StatusCode == FtpStatusCode.ConnectionClosed)
					{
						flag = false;
					}
				}
				if (flag && (closeState & CloseExState.Silent) == CloseExState.Normal)
				{
					throw;
				}
			}
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x0009FBA4 File Offset: 0x0009EBA4
		private void CheckError()
		{
			if (this.m_Request.Aborted)
			{
				throw new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060026CA RID: 9930 RVA: 0x0009FBC5 File Offset: 0x0009EBC5
		public override bool CanRead
		{
			get
			{
				return this.m_Readable;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060026CB RID: 9931 RVA: 0x0009FBCD File Offset: 0x0009EBCD
		public override bool CanSeek
		{
			get
			{
				return this.m_NetworkStream.CanSeek;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060026CC RID: 9932 RVA: 0x0009FBDA File Offset: 0x0009EBDA
		public override bool CanWrite
		{
			get
			{
				return this.m_Writeable;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060026CD RID: 9933 RVA: 0x0009FBE2 File Offset: 0x0009EBE2
		public override long Length
		{
			get
			{
				return this.m_NetworkStream.Length;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x0009FBEF File Offset: 0x0009EBEF
		// (set) Token: 0x060026CF RID: 9935 RVA: 0x0009FBFC File Offset: 0x0009EBFC
		public override long Position
		{
			get
			{
				return this.m_NetworkStream.Position;
			}
			set
			{
				this.m_NetworkStream.Position = value;
			}
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x0009FC0C File Offset: 0x0009EC0C
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckError();
			long result;
			try
			{
				result = this.m_NetworkStream.Seek(offset, origin);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return result;
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x0009FC4C File Offset: 0x0009EC4C
		public override int Read(byte[] buffer, int offset, int size)
		{
			this.CheckError();
			int num;
			try
			{
				num = this.m_NetworkStream.Read(buffer, offset, size);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			if (num == 0)
			{
				this.m_IsFullyRead = true;
				this.Close();
			}
			return num;
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x0009FC9C File Offset: 0x0009EC9C
		public override void Write(byte[] buffer, int offset, int size)
		{
			this.CheckError();
			try
			{
				this.m_NetworkStream.Write(buffer, offset, size);
			}
			catch
			{
				this.CheckError();
				throw;
			}
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x0009FCD8 File Offset: 0x0009ECD8
		private void AsyncReadCallback(IAsyncResult ar)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)ar.AsyncState;
			try
			{
				try
				{
					int num = this.m_NetworkStream.EndRead(ar);
					if (num == 0)
					{
						this.m_IsFullyRead = true;
						this.Close();
					}
					lazyAsyncResult.InvokeCallback(num);
				}
				catch (Exception result)
				{
					if (!lazyAsyncResult.IsCompleted)
					{
						lazyAsyncResult.InvokeCallback(result);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x0009FD50 File Offset: 0x0009ED50
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckError();
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this, state, callback);
			try
			{
				this.m_NetworkStream.BeginRead(buffer, offset, size, new AsyncCallback(this.AsyncReadCallback), lazyAsyncResult);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return lazyAsyncResult;
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x0009FDA8 File Offset: 0x0009EDA8
		public override int EndRead(IAsyncResult ar)
		{
			int result;
			try
			{
				object obj = ((LazyAsyncResult)ar).InternalWaitForCompletion();
				if (obj is Exception)
				{
					throw (Exception)obj;
				}
				result = (int)obj;
			}
			finally
			{
				this.CheckError();
			}
			return result;
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x0009FDF4 File Offset: 0x0009EDF4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckError();
			IAsyncResult result;
			try
			{
				result = this.m_NetworkStream.BeginWrite(buffer, offset, size, callback, state);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return result;
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x0009FE38 File Offset: 0x0009EE38
		public override void EndWrite(IAsyncResult asyncResult)
		{
			try
			{
				this.m_NetworkStream.EndWrite(asyncResult);
			}
			finally
			{
				this.CheckError();
			}
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x0009FE6C File Offset: 0x0009EE6C
		public override void Flush()
		{
			this.m_NetworkStream.Flush();
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x0009FE79 File Offset: 0x0009EE79
		public override void SetLength(long value)
		{
			this.m_NetworkStream.SetLength(value);
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060026DA RID: 9946 RVA: 0x0009FE87 File Offset: 0x0009EE87
		public override bool CanTimeout
		{
			get
			{
				return this.m_NetworkStream.CanTimeout;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060026DB RID: 9947 RVA: 0x0009FE94 File Offset: 0x0009EE94
		// (set) Token: 0x060026DC RID: 9948 RVA: 0x0009FEA1 File Offset: 0x0009EEA1
		public override int ReadTimeout
		{
			get
			{
				return this.m_NetworkStream.ReadTimeout;
			}
			set
			{
				this.m_NetworkStream.ReadTimeout = value;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060026DD RID: 9949 RVA: 0x0009FEAF File Offset: 0x0009EEAF
		// (set) Token: 0x060026DE RID: 9950 RVA: 0x0009FEBC File Offset: 0x0009EEBC
		public override int WriteTimeout
		{
			get
			{
				return this.m_NetworkStream.WriteTimeout;
			}
			set
			{
				this.m_NetworkStream.WriteTimeout = value;
			}
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x0009FECA File Offset: 0x0009EECA
		internal void SetSocketTimeoutOption(SocketShutdown mode, int timeout, bool silent)
		{
			this.m_NetworkStream.SetSocketTimeoutOption(mode, timeout, silent);
		}

		// Token: 0x0400265F RID: 9823
		private FtpWebRequest m_Request;

		// Token: 0x04002660 RID: 9824
		private NetworkStream m_NetworkStream;

		// Token: 0x04002661 RID: 9825
		private bool m_Writeable;

		// Token: 0x04002662 RID: 9826
		private bool m_Readable;

		// Token: 0x04002663 RID: 9827
		private bool m_IsFullyRead;

		// Token: 0x04002664 RID: 9828
		private bool m_Closing;
	}
}
