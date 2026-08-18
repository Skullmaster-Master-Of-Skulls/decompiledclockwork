using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Permissions;

namespace System.Net
{
	// Token: 0x020001B2 RID: 434
	internal class FtpDataStream : Stream, ICloseEx
	{
		// Token: 0x0600111F RID: 4383 RVA: 0x0005D50C File Offset: 0x0005B70C
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

		// Token: 0x06001120 RID: 4384 RVA: 0x0005D548 File Offset: 0x0005B748
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

		// Token: 0x06001121 RID: 4385 RVA: 0x0005D584 File Offset: 0x0005B784
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
				bool flag2 = true;
				WebException ex2 = ex as WebException;
				if (ex2 != null)
				{
					FtpWebResponse ftpWebResponse = ex2.Response as FtpWebResponse;
					if (ftpWebResponse != null && !this.m_IsFullyRead && ftpWebResponse.StatusCode == FtpStatusCode.ConnectionClosed)
					{
						flag2 = false;
					}
				}
				if (flag2 && (closeState & CloseExState.Silent) == CloseExState.Normal)
				{
					throw;
				}
			}
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0005D66C File Offset: 0x0005B86C
		private void CheckError()
		{
			if (this.m_Request.Aborted)
			{
				throw new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x0005D68D File Offset: 0x0005B88D
		public override bool CanRead
		{
			get
			{
				return this.m_Readable;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x0005D695 File Offset: 0x0005B895
		public override bool CanSeek
		{
			get
			{
				return this.m_NetworkStream.CanSeek;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x0005D6A2 File Offset: 0x0005B8A2
		public override bool CanWrite
		{
			get
			{
				return this.m_Writeable;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x0005D6AA File Offset: 0x0005B8AA
		public override long Length
		{
			get
			{
				return this.m_NetworkStream.Length;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x0005D6B7 File Offset: 0x0005B8B7
		// (set) Token: 0x06001128 RID: 4392 RVA: 0x0005D6C4 File Offset: 0x0005B8C4
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

		// Token: 0x06001129 RID: 4393 RVA: 0x0005D6D4 File Offset: 0x0005B8D4
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

		// Token: 0x0600112A RID: 4394 RVA: 0x0005D714 File Offset: 0x0005B914
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

		// Token: 0x0600112B RID: 4395 RVA: 0x0005D764 File Offset: 0x0005B964
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

		// Token: 0x0600112C RID: 4396 RVA: 0x0005D7A0 File Offset: 0x0005B9A0
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

		// Token: 0x0600112D RID: 4397 RVA: 0x0005D818 File Offset: 0x0005BA18
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

		// Token: 0x0600112E RID: 4398 RVA: 0x0005D870 File Offset: 0x0005BA70
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

		// Token: 0x0600112F RID: 4399 RVA: 0x0005D8BC File Offset: 0x0005BABC
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

		// Token: 0x06001130 RID: 4400 RVA: 0x0005D900 File Offset: 0x0005BB00
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

		// Token: 0x06001131 RID: 4401 RVA: 0x0005D934 File Offset: 0x0005BB34
		public override void Flush()
		{
			this.m_NetworkStream.Flush();
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0005D941 File Offset: 0x0005BB41
		public override void SetLength(long value)
		{
			this.m_NetworkStream.SetLength(value);
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x0005D94F File Offset: 0x0005BB4F
		public override bool CanTimeout
		{
			get
			{
				return this.m_NetworkStream.CanTimeout;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x0005D95C File Offset: 0x0005BB5C
		// (set) Token: 0x06001135 RID: 4405 RVA: 0x0005D969 File Offset: 0x0005BB69
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

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x0005D977 File Offset: 0x0005BB77
		// (set) Token: 0x06001137 RID: 4407 RVA: 0x0005D984 File Offset: 0x0005BB84
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

		// Token: 0x06001138 RID: 4408 RVA: 0x0005D992 File Offset: 0x0005BB92
		internal void SetSocketTimeoutOption(SocketShutdown mode, int timeout, bool silent)
		{
			this.m_NetworkStream.SetSocketTimeoutOption(mode, timeout, silent);
		}

		// Token: 0x04001410 RID: 5136
		private FtpWebRequest m_Request;

		// Token: 0x04001411 RID: 5137
		private NetworkStream m_NetworkStream;

		// Token: 0x04001412 RID: 5138
		private bool m_Writeable;

		// Token: 0x04001413 RID: 5139
		private bool m_Readable;

		// Token: 0x04001414 RID: 5140
		private bool m_IsFullyRead;

		// Token: 0x04001415 RID: 5141
		private bool m_Closing;
	}
}
