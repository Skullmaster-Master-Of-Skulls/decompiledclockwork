using System;
using System.Runtime;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008EF RID: 2287
	internal sealed class MsmqOutputChannel : TransportOutputChannel
	{
		// Token: 0x0600572C RID: 22316 RVA: 0x0013FE6C File Offset: 0x0013E06C
		public MsmqOutputChannel(MsmqChannelFactory<IOutputChannel> factory, EndpointAddress to, Uri via, bool manualAddressing) : base(factory, to, via, manualAddressing, factory.MessageVersion)
		{
			byte[] modeBytes = ClientSingletonSizedEncoder.ModeBytes;
			EncodedVia via2 = new EncodedVia(this.Via.AbsoluteUri);
			EncodedContentType contentType = EncodedContentType.Create(factory.MessageEncoderFactory.Encoder.ContentType);
			this.preamble = DiagnosticUtility.Utility.AllocateByteArray(modeBytes.Length + ClientSingletonSizedEncoder.CalcStartSize(via2, contentType));
			Buffer.BlockCopy(modeBytes, 0, this.preamble, 0, modeBytes.Length);
			ClientSingletonSizedEncoder.EncodeStart(this.preamble, modeBytes.Length, via2, contentType);
			this.outputMessages = new SynchronizedDisposablePool<MsmqOutputMessage<IOutputChannel>>(factory.MaxPoolSize);
			if (factory.IsMsmqX509SecurityConfigured)
			{
				this.certificateTokenProvider = factory.CreateX509TokenProvider(to, via);
			}
			this.factory = factory;
		}

		// Token: 0x0600572D RID: 22317 RVA: 0x0013FF21 File Offset: 0x0013E121
		private void CloseQueue()
		{
			this.outputMessages.Dispose();
			if (this.msmqQueue != null)
			{
				this.msmqQueue.Dispose();
			}
			this.msmqQueue = null;
		}

		// Token: 0x0600572E RID: 22318 RVA: 0x0013FF48 File Offset: 0x0013E148
		private void OnCloseCore(bool isAborting, TimeSpan timeout)
		{
			this.CloseQueue();
			this.outputMessages.Dispose();
			if (this.factory.IsMsmqX509SecurityConfigured)
			{
				if (isAborting)
				{
					this.certificateTokenProvider.Abort();
					return;
				}
				this.certificateTokenProvider.Close(timeout);
			}
		}

		// Token: 0x0600572F RID: 22319 RVA: 0x0013FF83 File Offset: 0x0013E183
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnCloseCore(false, timeout);
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06005730 RID: 22320 RVA: 0x0013FF94 File Offset: 0x0013E194
		protected override void OnEndClose(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06005731 RID: 22321 RVA: 0x0013FF9C File Offset: 0x0013E19C
		protected override void OnClose(TimeSpan timeout)
		{
			this.OnCloseCore(false, timeout);
		}

		// Token: 0x06005732 RID: 22322 RVA: 0x0013FFA6 File Offset: 0x0013E1A6
		protected override void OnAbort()
		{
			this.OnCloseCore(true, TimeSpan.Zero);
		}

		// Token: 0x06005733 RID: 22323 RVA: 0x0013FFB4 File Offset: 0x0013E1B4
		private void OnOpenCore(TimeSpan timeout)
		{
			this.OpenQueue();
			if (this.factory.IsMsmqX509SecurityConfigured)
			{
				this.certificateTokenProvider.Open(timeout);
			}
		}

		// Token: 0x06005734 RID: 22324 RVA: 0x0013FFD8 File Offset: 0x0013E1D8
		private void OpenQueue()
		{
			try
			{
				this.msmqQueue = new MsmqQueue(this.factory.AddressTranslator.UriToFormatName(this.RemoteAddress.Uri), 2);
			}
			catch (MsmqException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Normalized);
			}
			if (this.factory.ExactlyOnce)
			{
				this.transactionMode = MsmqTransactionMode.CurrentOrSingle;
				return;
			}
			this.transactionMode = MsmqTransactionMode.None;
		}

		// Token: 0x06005735 RID: 22325 RVA: 0x0014004C File Offset: 0x0013E24C
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnOpenCore(timeout);
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06005736 RID: 22326 RVA: 0x0014005C File Offset: 0x0013E25C
		protected override void OnEndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06005737 RID: 22327 RVA: 0x00140064 File Offset: 0x0013E264
		protected override void OnOpen(TimeSpan timeout)
		{
			this.OnOpenCore(timeout);
		}

		// Token: 0x06005738 RID: 22328 RVA: 0x0014006D File Offset: 0x0013E26D
		protected override IAsyncResult OnBeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnSend(message, timeout);
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06005739 RID: 22329 RVA: 0x0014007F File Offset: 0x0013E27F
		protected override void OnEndSend(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x0600573A RID: 22330 RVA: 0x00140088 File Offset: 0x0013E288
		protected override void OnSend(Message message, TimeSpan timeout)
		{
			ArraySegment<byte> arraySegment = this.factory.MessageEncoderFactory.Encoder.WriteMessage(message, int.MaxValue, this.factory.BufferManager, this.preamble.Length);
			Buffer.BlockCopy(this.preamble, 0, arraySegment.Array, arraySegment.Offset - this.preamble.Length, this.preamble.Length);
			byte[] array = arraySegment.Array;
			int srcOffset = arraySegment.Offset - this.preamble.Length;
			int num = arraySegment.Count + this.preamble.Length;
			MsmqOutputMessage<IOutputChannel> msmqOutputMessage = this.outputMessages.Take();
			if (msmqOutputMessage == null)
			{
				msmqOutputMessage = new MsmqOutputMessage<IOutputChannel>(this.factory, num, this.RemoteAddress);
				MsmqDiagnostics.PoolFull(this.factory.MaxPoolSize);
			}
			try
			{
				msmqOutputMessage.ApplyCertificateIfNeeded(this.certificateTokenProvider, this.factory.MsmqTransportSecurity.MsmqAuthenticationMode, timeout);
				msmqOutputMessage.Body.EnsureBufferLength(num);
				msmqOutputMessage.Body.BufferLength = num;
				Buffer.BlockCopy(array, srcOffset, msmqOutputMessage.Body.Buffer, 0, num);
				this.factory.BufferManager.ReturnBuffer(array);
				bool flag = false;
				try
				{
					Msmq.EnterXPSendLock(out flag, this.factory.MsmqTransportSecurity.MsmqProtectionLevel);
					this.msmqQueue.Send(msmqOutputMessage, this.transactionMode);
					MsmqDiagnostics.DatagramSent(msmqOutputMessage.MessageId, message);
				}
				catch (MsmqException ex)
				{
					if (ex.FaultSender)
					{
						base.Fault();
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Normalized);
				}
				finally
				{
					if (flag)
					{
						Msmq.LeaveXPSendLock();
					}
				}
			}
			finally
			{
				if (!this.outputMessages.Return(msmqOutputMessage))
				{
					msmqOutputMessage.Dispose();
				}
			}
		}

		// Token: 0x040035AA RID: 13738
		private MsmqQueue msmqQueue;

		// Token: 0x040035AB RID: 13739
		private MsmqTransactionMode transactionMode;

		// Token: 0x040035AC RID: 13740
		private readonly byte[] preamble;

		// Token: 0x040035AD RID: 13741
		private SynchronizedDisposablePool<MsmqOutputMessage<IOutputChannel>> outputMessages;

		// Token: 0x040035AE RID: 13742
		private MsmqChannelFactory<IOutputChannel> factory;

		// Token: 0x040035AF RID: 13743
		private SecurityTokenProviderContainer certificateTokenProvider;
	}
}
