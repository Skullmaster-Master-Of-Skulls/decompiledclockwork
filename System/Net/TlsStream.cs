using System;
using System.Collections;
using System.Globalization;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200055A RID: 1370
	internal class TlsStream : NetworkStream, IDisposable
	{
		// Token: 0x0600299E RID: 10654 RVA: 0x000AE684 File Offset: 0x000AD684
		public TlsStream(string destinationHost, NetworkStream networkStream, X509CertificateCollection clientCertificates, ServicePoint servicePoint, object initiatingRequest, ExecutionContext executionContext) : base(networkStream, true)
		{
			this._ExecutionContext = executionContext;
			if (this._ExecutionContext == null)
			{
				this._ExecutionContext = ExecutionContext.Capture();
			}
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, this, ".ctor", "host=" + destinationHost + ", #certs=" + ((clientCertificates == null) ? "null" : clientCertificates.Count.ToString(NumberFormatInfo.InvariantInfo)));
			}
			this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
			this.m_Worker = new SslState(networkStream, initiatingRequest is HttpWebRequest);
			this.m_DestinationHost = destinationHost;
			this.m_ClientCertificates = clientCertificates;
			RemoteCertValidationCallback certValidationDelegate = servicePoint.SetupHandshakeDoneProcedure(this, initiatingRequest);
			this.m_Worker.SetCertValidationDelegate(certValidationDelegate);
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x0600299F RID: 10655 RVA: 0x000AE746 File Offset: 0x000AD746
		internal WebExceptionStatus ExceptionStatus
		{
			get
			{
				return this.m_ExceptionStatus;
			}
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x000AE750 File Offset: 0x000AD750
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

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x060029A1 RID: 10657 RVA: 0x000AE7AC File Offset: 0x000AD7AC
		public override bool DataAvailable
		{
			get
			{
				return this.m_Worker.DataAvailable || base.DataAvailable;
			}
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x000AE7C4 File Offset: 0x000AD7C4
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

		// Token: 0x060029A3 RID: 10659 RVA: 0x000AE848 File Offset: 0x000AD848
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

		// Token: 0x060029A4 RID: 10660 RVA: 0x000AE8E0 File Offset: 0x000AD8E0
		internal override IAsyncResult UnsafeBeginRead(byte[] buffer, int offset, int size, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginRead(buffer, offset, size, asyncCallback, asyncState);
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x000AE8F0 File Offset: 0x000AD8F0
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

		// Token: 0x060029A6 RID: 10662 RVA: 0x000AE994 File Offset: 0x000AD994
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

		// Token: 0x060029A7 RID: 10663 RVA: 0x000AEA24 File Offset: 0x000ADA24
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

		// Token: 0x060029A8 RID: 10664 RVA: 0x000AEABC File Offset: 0x000ADABC
		internal override IAsyncResult UnsafeBeginWrite(byte[] buffer, int offset, int size, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginWrite(buffer, offset, size, asyncCallback, asyncState);
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x000AEACC File Offset: 0x000ADACC
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

		// Token: 0x060029AA RID: 10666 RVA: 0x000AEB74 File Offset: 0x000ADB74
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

		// Token: 0x060029AB RID: 10667 RVA: 0x000AEC04 File Offset: 0x000ADC04
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

		// Token: 0x060029AC RID: 10668 RVA: 0x000AEC94 File Offset: 0x000ADC94
		internal override IAsyncResult UnsafeBeginMultipleWrite(BufferOffsetSize[] buffers, AsyncCallback callback, object state)
		{
			return this.BeginMultipleWrite(buffers, callback, state);
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x000AEC9F File Offset: 0x000ADC9F
		internal override void EndMultipleWrite(IAsyncResult asyncResult)
		{
			this.EndWrite(asyncResult);
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x060029AE RID: 10670 RVA: 0x000AECA8 File Offset: 0x000ADCA8
		public X509Certificate ClientCertificate
		{
			get
			{
				return this.m_Worker.InternalLocalCertificate;
			}
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x000AECB5 File Offset: 0x000ADCB5
		internal ChannelBinding GetChannelBinding(ChannelBindingKind kind)
		{
			if (kind == ChannelBindingKind.Endpoint && this.m_CachedChannelBinding != null)
			{
				return this.m_CachedChannelBinding;
			}
			return this.m_Worker.GetChannelBinding(kind);
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x000AECD8 File Offset: 0x000ADCD8
		internal bool ProcessAuthentication(LazyAsyncResult result)
		{
			bool flag = false;
			bool flag2 = result == null;
			lock (this.m_PendingIO)
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
					bool flag3 = true;
					LazyAsyncResult lazyAsyncResult = null;
					try
					{
						try
						{
							this.m_Worker.ValidateCreateContext(false, this.m_DestinationHost, (SslProtocols)ServicePointManager.SecurityProtocol, null, this.m_ClientCertificates, true, ServicePointManager.CheckCertificateRevocationList, ServicePointManager.CheckCertificateName);
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
							flag3 = false;
							throw;
						}
						goto IL_14C;
					}
					finally
					{
						if (flag2 || !flag3)
						{
							lock (this.m_PendingIO)
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
				IL_14C:;
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

		// Token: 0x060029B1 RID: 10673 RVA: 0x000AEEB0 File Offset: 0x000ADEB0
		private void CallProcessAuthentication(object state)
		{
			this.m_Worker.ProcessAuthentication((LazyAsyncResult)state);
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x000AEEC3 File Offset: 0x000ADEC3
		private void StartWakeupPendingIO(object nullState)
		{
			this.WakeupPendingIO(null);
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x000AEECC File Offset: 0x000ADECC
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
			lock (this.m_PendingIO)
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

		// Token: 0x060029B4 RID: 10676 RVA: 0x000AEFEC File Offset: 0x000ADFEC
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

		// Token: 0x060029B5 RID: 10677 RVA: 0x000AF048 File Offset: 0x000AE048
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

		// Token: 0x060029B6 RID: 10678 RVA: 0x000AF0E8 File Offset: 0x000AE0E8
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

		// Token: 0x060029B7 RID: 10679 RVA: 0x000AF158 File Offset: 0x000AE158
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

		// Token: 0x04002881 RID: 10369
		private SslState m_Worker;

		// Token: 0x04002882 RID: 10370
		private WebExceptionStatus m_ExceptionStatus;

		// Token: 0x04002883 RID: 10371
		private string m_DestinationHost;

		// Token: 0x04002884 RID: 10372
		private X509CertificateCollection m_ClientCertificates;

		// Token: 0x04002885 RID: 10373
		private static AsyncCallback _CompleteIOCallback = new AsyncCallback(TlsStream.CompleteIOCallback);

		// Token: 0x04002886 RID: 10374
		private ExecutionContext _ExecutionContext;

		// Token: 0x04002887 RID: 10375
		private ChannelBinding m_CachedChannelBinding;

		// Token: 0x04002888 RID: 10376
		private int m_ShutDown;

		// Token: 0x04002889 RID: 10377
		private ArrayList m_PendingIO = new ArrayList();
	}
}
