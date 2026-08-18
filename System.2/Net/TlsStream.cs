using System;
using System.Collections;
using System.Globalization;
using System.Net.Configuration;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200021D RID: 541
	internal class TlsStream : NetworkStream, IDisposable
	{
		// Token: 0x060013EE RID: 5102 RVA: 0x00069ACC File Offset: 0x00067CCC
		public TlsStream(string destinationHost, NetworkStream networkStream, bool checkCertificateRevocationList, SslProtocols sslProtocols, X509CertificateCollection clientCertificates, ServicePoint servicePoint, object initiatingRequest, ExecutionContext executionContext) : base(networkStream, true)
		{
			this.m_CheckCertificateRevocationList = checkCertificateRevocationList;
			this.m_SslProtocols = sslProtocols;
			this._ExecutionContext = executionContext;
			if (this._ExecutionContext == null)
			{
				this._ExecutionContext = ExecutionContext.Capture();
			}
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, this, ".ctor", string.Format("host={0}, #certs={1}, checkCertificateRevocationList={2}, sslProtocols={3}", new object[]
				{
					destinationHost,
					(clientCertificates == null) ? "null" : clientCertificates.Count.ToString(NumberFormatInfo.InvariantInfo),
					checkCertificateRevocationList,
					sslProtocols
				}));
			}
			this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
			this.m_Worker = new SslState(networkStream, initiatingRequest is HttpWebRequest, SettingsSectionInternal.Section.EncryptionPolicy);
			this.m_DestinationHost = destinationHost;
			this.m_ClientCertificates = clientCertificates;
			RemoteCertValidationCallback certValidationDelegate = servicePoint.SetupHandshakeDoneProcedure(this, initiatingRequest);
			this.m_Worker.SetCertValidationDelegate(certValidationDelegate);
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x00069BC4 File Offset: 0x00067DC4
		internal WebExceptionStatus ExceptionStatus
		{
			get
			{
				return this.m_ExceptionStatus;
			}
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00069BCC File Offset: 0x00067DCC
		protected override void Dispose(bool disposing)
		{
			if (Interlocked.Exchange(ref this.m_ShutDown, 1) == 1)
			{
				return;
			}
			try
			{
				if (disposing)
				{
					this.m_CachedChannelBinding = this.GetChannelBinding(ChannelBindingKind.Endpoint);
					this.m_Worker.Close();
				}
				else
				{
					this.m_Worker = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x00069C2C File Offset: 0x00067E2C
		public override bool DataAvailable
		{
			get
			{
				return this.m_Worker.DataAvailable || base.DataAvailable;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x00069C43 File Offset: 0x00067E43
		internal bool IsTls13
		{
			get
			{
				return this.m_Worker != null && this.m_Worker.IsAuthenticated && (this.m_Worker.SslProtocol & SslProtocols.Tls13) > SslProtocols.None;
			}
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00069C70 File Offset: 0x00067E70
		internal bool PollForApplicationData(int microSeconds)
		{
			if (this.m_Worker.DataAvailable)
			{
				return true;
			}
			bool dataAvailable;
			try
			{
				this.m_Worker.SecureStream.ProcessReadForPoll(TlsStream.s_EmptyBuffer, 0, 0, microSeconds);
				dataAvailable = this.m_Worker.DataAvailable;
			}
			catch
			{
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.ReceiveFailure;
				}
				throw;
			}
			return dataAvailable;
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00069CFC File Offset: 0x00067EFC
		public override int Read(byte[] buffer, int offset, int size)
		{
			if (!this.m_Worker.IsAuthenticated)
			{
				this.ProcessAuthentication(null);
			}
			int result;
			try
			{
				result = this.m_Worker.SecureStream.Read(buffer, offset, size);
			}
			catch
			{
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.ReceiveFailure;
				}
				throw;
			}
			return result;
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x00069D80 File Offset: 0x00067F80
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback asyncCallback, object asyncState)
		{
			if (!this.m_Worker.IsAuthenticated)
			{
				BufferAsyncResult result = new BufferAsyncResult(this, buffer, offset, size, false, asyncState, asyncCallback);
				if (this.ProcessAuthentication(result))
				{
					return result;
				}
			}
			IAsyncResult result2;
			try
			{
				result2 = this.m_Worker.SecureStream.BeginRead(buffer, offset, size, asyncCallback, asyncState);
			}
			catch
			{
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.ReceiveFailure;
				}
				throw;
			}
			return result2;
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x00069E18 File Offset: 0x00068018
		internal override IAsyncResult UnsafeBeginRead(byte[] buffer, int offset, int size, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginRead(buffer, offset, size, asyncCallback, asyncState);
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00069E28 File Offset: 0x00068028
		public override int EndRead(IAsyncResult asyncResult)
		{
			int result;
			try
			{
				BufferAsyncResult bufferAsyncResult = asyncResult as BufferAsyncResult;
				if (bufferAsyncResult == null || bufferAsyncResult.AsyncObject != this)
				{
					result = this.m_Worker.SecureStream.EndRead(asyncResult);
				}
				else
				{
					bufferAsyncResult.InternalWaitForCompletion();
					Exception ex = bufferAsyncResult.Result as Exception;
					if (ex != null)
					{
						throw ex;
					}
					result = (int)bufferAsyncResult.Result;
				}
			}
			catch
			{
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.ReceiveFailure;
				}
				throw;
			}
			return result;
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00069ECC File Offset: 0x000680CC
		public override void Write(byte[] buffer, int offset, int size)
		{
			if (!this.m_Worker.IsAuthenticated)
			{
				this.ProcessAuthentication(null);
			}
			try
			{
				this.m_Worker.SecureStream.Write(buffer, offset, size);
			}
			catch
			{
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.SendFailure;
				}
				Socket socket = base.Socket;
				if (socket != null)
				{
					socket.InternalShutdown(SocketShutdown.Both);
				}
				throw;
			}
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00069F60 File Offset: 0x00068160
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback asyncCallback, object asyncState)
		{
			if (!this.m_Worker.IsAuthenticated)
			{
				BufferAsyncResult result = new BufferAsyncResult(this, buffer, offset, size, true, asyncState, asyncCallback);
				if (this.ProcessAuthentication(result))
				{
					return result;
				}
			}
			IAsyncResult result2;
			try
			{
				result2 = this.m_Worker.SecureStream.BeginWrite(buffer, offset, size, asyncCallback, asyncState);
			}
			catch
			{
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.SendFailure;
				}
				throw;
			}
			return result2;
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x00069FF8 File Offset: 0x000681F8
		internal override IAsyncResult UnsafeBeginWrite(byte[] buffer, int offset, int size, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginWrite(buffer, offset, size, asyncCallback, asyncState);
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0006A008 File Offset: 0x00068208
		public override void EndWrite(IAsyncResult asyncResult)
		{
			try
			{
				BufferAsyncResult bufferAsyncResult = asyncResult as BufferAsyncResult;
				if (bufferAsyncResult == null || bufferAsyncResult.AsyncObject != this)
				{
					this.m_Worker.SecureStream.EndWrite(asyncResult);
				}
				else
				{
					bufferAsyncResult.InternalWaitForCompletion();
					Exception ex = bufferAsyncResult.Result as Exception;
					if (ex != null)
					{
						throw ex;
					}
				}
			}
			catch
			{
				Socket socket = base.Socket;
				if (socket != null)
				{
					socket.InternalShutdown(SocketShutdown.Both);
				}
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.SendFailure;
				}
				throw;
			}
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0006A0B0 File Offset: 0x000682B0
		internal override void MultipleWrite(BufferOffsetSize[] buffers)
		{
			if (!this.m_Worker.IsAuthenticated)
			{
				this.ProcessAuthentication(null);
			}
			try
			{
				this.m_Worker.SecureStream.Write(buffers);
			}
			catch
			{
				Socket socket = base.Socket;
				if (socket != null)
				{
					socket.InternalShutdown(SocketShutdown.Both);
				}
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.SendFailure;
				}
				throw;
			}
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0006A140 File Offset: 0x00068340
		internal override IAsyncResult BeginMultipleWrite(BufferOffsetSize[] buffers, AsyncCallback callback, object state)
		{
			if (!this.m_Worker.IsAuthenticated)
			{
				BufferAsyncResult result = new BufferAsyncResult(this, buffers, state, callback);
				if (this.ProcessAuthentication(result))
				{
					return result;
				}
			}
			IAsyncResult result2;
			try
			{
				result2 = this.m_Worker.SecureStream.BeginWrite(buffers, callback, state);
			}
			catch
			{
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.SendFailure;
				}
				throw;
			}
			return result2;
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0006A1D0 File Offset: 0x000683D0
		internal override IAsyncResult UnsafeBeginMultipleWrite(BufferOffsetSize[] buffers, AsyncCallback callback, object state)
		{
			return this.BeginMultipleWrite(buffers, callback, state);
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0006A1DB File Offset: 0x000683DB
		internal override void EndMultipleWrite(IAsyncResult asyncResult)
		{
			this.EndWrite(asyncResult);
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x0006A1E4 File Offset: 0x000683E4
		public X509Certificate ClientCertificate
		{
			get
			{
				return this.m_Worker.InternalLocalCertificate;
			}
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0006A1F1 File Offset: 0x000683F1
		internal ChannelBinding GetChannelBinding(ChannelBindingKind kind)
		{
			if (kind == ChannelBindingKind.Endpoint && this.m_CachedChannelBinding != null)
			{
				return this.m_CachedChannelBinding;
			}
			return this.m_Worker.GetChannelBinding(kind);
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0006A214 File Offset: 0x00068414
		internal bool ProcessAuthentication(LazyAsyncResult result)
		{
			bool flag = false;
			bool flag2 = result == null;
			ArrayList pendingIO = this.m_PendingIO;
			lock (pendingIO)
			{
				if (this.m_Worker.IsAuthenticated)
				{
					return false;
				}
				if (this.m_PendingIO.Count == 0)
				{
					flag = true;
				}
				if (flag2)
				{
					result = new LazyAsyncResult(this, null, null);
				}
				this.m_PendingIO.Add(result);
			}
			try
			{
				if (flag)
				{
					bool flag4 = true;
					LazyAsyncResult lazyAsyncResult = null;
					try
					{
						try
						{
							this.m_Worker.ValidateCreateContext(false, this.m_DestinationHost, this.m_SslProtocols, null, this.m_ClientCertificates, true, this.m_CheckCertificateRevocationList, ServicePointManager.CheckCertificateName);
							if (!flag2)
							{
								lazyAsyncResult = new LazyAsyncResult(this.m_Worker, null, new AsyncCallback(this.WakeupPendingIO));
							}
							if (this._ExecutionContext != null)
							{
								ExecutionContext.Run(this._ExecutionContext.CreateCopy(), new ContextCallback(this.CallProcessAuthentication), lazyAsyncResult);
							}
							else
							{
								this.m_Worker.ProcessAuthentication(lazyAsyncResult);
							}
						}
						catch
						{
							flag4 = false;
							throw;
						}
						goto IL_165;
					}
					finally
					{
						if (flag2 || !flag4)
						{
							ArrayList pendingIO2 = this.m_PendingIO;
							lock (pendingIO2)
							{
								if (this.m_PendingIO.Count > 1)
								{
									ThreadPool.QueueUserWorkItem(new WaitCallback(this.StartWakeupPendingIO), null);
								}
								else
								{
									this.m_PendingIO.Clear();
								}
							}
						}
					}
				}
				if (flag2)
				{
					Exception ex = result.InternalWaitForCompletion() as Exception;
					if (ex != null)
					{
						throw ex;
					}
				}
				IL_165:;
			}
			catch
			{
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.ReceiveFailure;
				}
				throw;
			}
			return true;
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0006A440 File Offset: 0x00068640
		private void CallProcessAuthentication(object state)
		{
			this.m_Worker.ProcessAuthentication((LazyAsyncResult)state);
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0006A453 File Offset: 0x00068653
		private void StartWakeupPendingIO(object nullState)
		{
			this.WakeupPendingIO(null);
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0006A45C File Offset: 0x0006865C
		private void WakeupPendingIO(IAsyncResult ar)
		{
			Exception result = null;
			try
			{
				if (ar != null)
				{
					this.m_Worker.EndProcessAuthentication(ar);
				}
			}
			catch (Exception ex)
			{
				result = ex;
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.ReceiveFailure;
				}
			}
			ArrayList pendingIO = this.m_PendingIO;
			lock (pendingIO)
			{
				while (this.m_PendingIO.Count != 0)
				{
					LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)this.m_PendingIO[this.m_PendingIO.Count - 1];
					this.m_PendingIO.RemoveAt(this.m_PendingIO.Count - 1);
					if (lazyAsyncResult is BufferAsyncResult)
					{
						if (this.m_PendingIO.Count == 0)
						{
							this.ResumeIOWorker(lazyAsyncResult);
						}
						else
						{
							ThreadPool.QueueUserWorkItem(new WaitCallback(this.ResumeIOWorker), lazyAsyncResult);
						}
					}
					else
					{
						try
						{
							lazyAsyncResult.InvokeCallback(result);
						}
						catch
						{
						}
					}
				}
			}
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0006A588 File Offset: 0x00068788
		private void ResumeIOWorker(object result)
		{
			BufferAsyncResult bufferAsyncResult = (BufferAsyncResult)result;
			try
			{
				this.ResumeIO(bufferAsyncResult);
			}
			catch (Exception ex)
			{
				if (ex is OutOfMemoryException || ex is StackOverflowException || ex is ThreadAbortException)
				{
					throw;
				}
				if (bufferAsyncResult.InternalPeekCompleted)
				{
					throw;
				}
				bufferAsyncResult.InvokeCallback(ex);
			}
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0006A5E4 File Offset: 0x000687E4
		private void ResumeIO(BufferAsyncResult bufferResult)
		{
			IAsyncResult asyncResult;
			if (bufferResult.IsWrite)
			{
				if (bufferResult.Buffers != null)
				{
					asyncResult = this.m_Worker.SecureStream.BeginWrite(bufferResult.Buffers, TlsStream._CompleteIOCallback, bufferResult);
				}
				else
				{
					asyncResult = this.m_Worker.SecureStream.BeginWrite(bufferResult.Buffer, bufferResult.Offset, bufferResult.Count, TlsStream._CompleteIOCallback, bufferResult);
				}
			}
			else
			{
				asyncResult = this.m_Worker.SecureStream.BeginRead(bufferResult.Buffer, bufferResult.Offset, bufferResult.Count, TlsStream._CompleteIOCallback, bufferResult);
			}
			if (asyncResult.CompletedSynchronously)
			{
				TlsStream.CompleteIO(asyncResult);
			}
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0006A684 File Offset: 0x00068884
		private static void CompleteIOCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			try
			{
				TlsStream.CompleteIO(result);
			}
			catch (Exception ex)
			{
				if (ex is OutOfMemoryException || ex is StackOverflowException || ex is ThreadAbortException)
				{
					throw;
				}
				if (((LazyAsyncResult)result.AsyncState).InternalPeekCompleted)
				{
					throw;
				}
				((LazyAsyncResult)result.AsyncState).InvokeCallback(ex);
			}
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0006A6F8 File Offset: 0x000688F8
		private static void CompleteIO(IAsyncResult result)
		{
			BufferAsyncResult bufferAsyncResult = (BufferAsyncResult)result.AsyncState;
			object result2 = null;
			if (bufferAsyncResult.IsWrite)
			{
				((TlsStream)bufferAsyncResult.AsyncObject).m_Worker.SecureStream.EndWrite(result);
			}
			else
			{
				result2 = ((TlsStream)bufferAsyncResult.AsyncObject).m_Worker.SecureStream.EndRead(result);
			}
			bufferAsyncResult.InvokeCallback(result2);
		}

		// Token: 0x040015F9 RID: 5625
		private SslState m_Worker;

		// Token: 0x040015FA RID: 5626
		private WebExceptionStatus m_ExceptionStatus;

		// Token: 0x040015FB RID: 5627
		private string m_DestinationHost;

		// Token: 0x040015FC RID: 5628
		private X509CertificateCollection m_ClientCertificates;

		// Token: 0x040015FD RID: 5629
		private static AsyncCallback _CompleteIOCallback = new AsyncCallback(TlsStream.CompleteIOCallback);

		// Token: 0x040015FE RID: 5630
		private ExecutionContext _ExecutionContext;

		// Token: 0x040015FF RID: 5631
		private ChannelBinding m_CachedChannelBinding;

		// Token: 0x04001600 RID: 5632
		private bool m_CheckCertificateRevocationList;

		// Token: 0x04001601 RID: 5633
		private SslProtocols m_SslProtocols;

		// Token: 0x04001602 RID: 5634
		private int m_ShutDown;

		// Token: 0x04001603 RID: 5635
		private static readonly byte[] s_EmptyBuffer = new byte[0];

		// Token: 0x04001604 RID: 5636
		private ArrayList m_PendingIO = new ArrayList();
	}
}
