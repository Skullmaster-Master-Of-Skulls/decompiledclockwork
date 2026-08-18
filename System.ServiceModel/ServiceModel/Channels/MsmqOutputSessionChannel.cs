using System;
using System.Collections.Generic;
using System.Runtime;
using System.ServiceModel.Security.Tokens;
using System.Transactions;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008F0 RID: 2288
	internal sealed class MsmqOutputSessionChannel : TransportOutputChannel, IOutputSessionChannel, IOutputChannel, IChannel, ICommunicationObject, ISessionChannel<IOutputSession>
	{
		// Token: 0x0600573B RID: 22331 RVA: 0x00140258 File Offset: 0x0013E458
		public MsmqOutputSessionChannel(MsmqChannelFactory<IOutputSessionChannel> factory, EndpointAddress to, Uri via, bool manualAddressing) : base(factory, to, via, manualAddressing, factory.MessageVersion)
		{
			this.factory = factory;
			this.encoder = this.factory.MessageEncoderFactory.CreateSessionEncoder();
			this.buffers = new List<ArraySegment<byte>>();
			this.buffers.Add(this.EncodeSessionPreamble());
			if (factory.IsMsmqX509SecurityConfigured)
			{
				this.certificateTokenProvider = factory.CreateX509TokenProvider(to, via);
			}
			this.session = new MsmqOutputSessionChannel.OutputSession();
		}

		// Token: 0x0600573C RID: 22332 RVA: 0x001402D0 File Offset: 0x0013E4D0
		private int CalcSessionGramSize()
		{
			long num = 0L;
			for (int i = 0; i < this.buffers.Count; i++)
			{
				num += (long)this.buffers[i].Count;
			}
			if (num > 2147483647L)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqSessionGramSizeMustBeInIntegerRange")));
			}
			return (int)num;
		}

		// Token: 0x0600573D RID: 22333 RVA: 0x00140334 File Offset: 0x0013E534
		private void CopySessionGramToBuffer(byte[] sessionGramBuffer)
		{
			int num = 0;
			for (int i = 0; i < this.buffers.Count; i++)
			{
				ArraySegment<byte> arraySegment = this.buffers[i];
				Buffer.BlockCopy(arraySegment.Array, arraySegment.Offset, sessionGramBuffer, num, arraySegment.Count);
				num += arraySegment.Count;
			}
		}

		// Token: 0x0600573E RID: 22334 RVA: 0x0014038C File Offset: 0x0013E58C
		private void ReturnSessionGramBuffers()
		{
			for (int i = 0; i < this.buffers.Count - 1; i++)
			{
				this.Factory.BufferManager.ReturnBuffer(this.buffers[i].Array);
			}
		}

		// Token: 0x1700153C RID: 5436
		// (get) Token: 0x0600573F RID: 22335 RVA: 0x001403D5 File Offset: 0x0013E5D5
		public IOutputSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x06005740 RID: 22336 RVA: 0x001403E0 File Offset: 0x0013E5E0
		private void OnCloseCore(bool isAborting, TimeSpan timeout)
		{
			if (!isAborting && this.buffers.Count > 1)
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					this.VerifyTransaction();
					this.buffers.Add(this.EncodeEndMarker());
				}
				int num = this.CalcSessionGramSize();
				using (MsmqOutputMessage<IOutputSessionChannel> msmqOutputMessage = new MsmqOutputMessage<IOutputSessionChannel>(this.Factory, num, this.RemoteAddress))
				{
					msmqOutputMessage.ApplyCertificateIfNeeded(this.certificateTokenProvider, this.factory.MsmqTransportSecurity.MsmqAuthenticationMode, timeout);
					msmqOutputMessage.Body.EnsureBufferLength(num);
					msmqOutputMessage.Body.BufferLength = num;
					this.CopySessionGramToBuffer(msmqOutputMessage.Body.Buffer);
					bool flag2 = false;
					try
					{
						Msmq.EnterXPSendLock(out flag2, this.factory.MsmqTransportSecurity.MsmqProtectionLevel);
						this.msmqQueue.Send(msmqOutputMessage, MsmqTransactionMode.CurrentOrSingle);
						MsmqDiagnostics.SessiongramSent(this.Session.Id, msmqOutputMessage.MessageId, this.buffers.Count);
					}
					catch (MsmqException ex)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Normalized);
					}
					finally
					{
						if (flag2)
						{
							Msmq.LeaveXPSendLock();
						}
						this.ReturnSessionGramBuffers();
					}
				}
			}
			if (this.msmqQueue != null)
			{
				this.msmqQueue.Dispose();
			}
			this.msmqQueue = null;
			if (this.certificateTokenProvider != null)
			{
				if (isAborting)
				{
					this.certificateTokenProvider.Abort();
					return;
				}
				this.certificateTokenProvider.Close(timeout);
			}
		}

		// Token: 0x06005741 RID: 22337 RVA: 0x00140584 File Offset: 0x0013E784
		protected override void OnAbort()
		{
			this.OnCloseCore(true, TimeSpan.Zero);
		}

		// Token: 0x06005742 RID: 22338 RVA: 0x00140592 File Offset: 0x0013E792
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnCloseCore(false, timeout);
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06005743 RID: 22339 RVA: 0x001405A3 File Offset: 0x0013E7A3
		protected override void OnEndClose(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06005744 RID: 22340 RVA: 0x001405AB File Offset: 0x0013E7AB
		protected override void OnClose(TimeSpan timeout)
		{
			this.OnCloseCore(false, timeout);
		}

		// Token: 0x06005745 RID: 22341 RVA: 0x001405B8 File Offset: 0x0013E7B8
		private void OnOpenCore(TimeSpan timeout)
		{
			if (null == Transaction.Current)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new InvalidOperationException(SR.GetString("MsmqTransactionCurrentRequired")));
			}
			this.associatedTx = Transaction.Current;
			this.associatedTx.EnlistVolatile(new MsmqOutputSessionChannel.TransactionEnlistment(this, this.associatedTx), EnlistmentOptions.None);
			this.msmqQueue = new MsmqQueue(this.Factory.AddressTranslator.UriToFormatName(this.RemoteAddress.Uri), 2);
			if (this.certificateTokenProvider != null)
			{
				this.certificateTokenProvider.Open(timeout);
			}
		}

		// Token: 0x06005746 RID: 22342 RVA: 0x0014064B File Offset: 0x0013E84B
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnOpenCore(timeout);
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06005747 RID: 22343 RVA: 0x0014065B File Offset: 0x0013E85B
		protected override void OnEndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06005748 RID: 22344 RVA: 0x00140663 File Offset: 0x0013E863
		protected override void OnOpen(TimeSpan timeout)
		{
			this.OnOpenCore(timeout);
		}

		// Token: 0x06005749 RID: 22345 RVA: 0x0014066C File Offset: 0x0013E86C
		protected override IAsyncResult OnBeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnSend(message, timeout);
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x0600574A RID: 22346 RVA: 0x0014067E File Offset: 0x0013E87E
		protected override void OnEndSend(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x0600574B RID: 22347 RVA: 0x00140688 File Offset: 0x0013E888
		protected override void OnSend(Message message, TimeSpan timeout)
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				base.ThrowIfDisposed();
				this.VerifyTransaction();
				this.buffers.Add(this.EncodeMessage(message));
			}
		}

		// Token: 0x0600574C RID: 22348 RVA: 0x001406E0 File Offset: 0x0013E8E0
		private void VerifyTransaction()
		{
			if (this.associatedTx != Transaction.Current)
			{
				base.Fault();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new InvalidOperationException(SR.GetString("MsmqSameTransactionExpected")));
			}
			if (Transaction.Current.TransactionInformation.Status != TransactionStatus.Active)
			{
				base.Fault();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new InvalidOperationException(SR.GetString("MsmqTransactionNotActive")));
			}
		}

		// Token: 0x0600574D RID: 22349 RVA: 0x00140750 File Offset: 0x0013E950
		private ArraySegment<byte> EncodeSessionPreamble()
		{
			EncodedVia via = new EncodedVia(this.Via.AbsoluteUri);
			EncodedContentType contentType = EncodedContentType.Create(this.encoder.ContentType);
			int num = ClientSimplexEncoder.ModeBytes.Length + SessionEncoder.CalcStartSize(via, contentType) + SessionEncoder.PreambleEndBytes.Length;
			byte[] array = this.Factory.BufferManager.TakeBuffer(num);
			Buffer.BlockCopy(ClientSimplexEncoder.ModeBytes, 0, array, 0, ClientSimplexEncoder.ModeBytes.Length);
			SessionEncoder.EncodeStart(array, ClientSimplexEncoder.ModeBytes.Length, via, contentType);
			Buffer.BlockCopy(SessionEncoder.PreambleEndBytes, 0, array, num - SessionEncoder.PreambleEndBytes.Length, SessionEncoder.PreambleEndBytes.Length);
			return new ArraySegment<byte>(array, 0, num);
		}

		// Token: 0x0600574E RID: 22350 RVA: 0x001407F0 File Offset: 0x0013E9F0
		private ArraySegment<byte> EncodeEndMarker()
		{
			return new ArraySegment<byte>(SessionEncoder.EndBytes, 0, SessionEncoder.EndBytes.Length);
		}

		// Token: 0x0600574F RID: 22351 RVA: 0x00140804 File Offset: 0x0013EA04
		private ArraySegment<byte> EncodeMessage(Message message)
		{
			ArraySegment<byte> messageFrame = this.encoder.WriteMessage(message, int.MaxValue, this.Factory.BufferManager, 6);
			return SessionEncoder.EncodeMessageFrame(messageFrame);
		}

		// Token: 0x1700153D RID: 5437
		// (get) Token: 0x06005750 RID: 22352 RVA: 0x00140835 File Offset: 0x0013EA35
		private MsmqChannelFactory<IOutputSessionChannel> Factory
		{
			get
			{
				return this.factory;
			}
		}

		// Token: 0x040035B0 RID: 13744
		private MsmqQueue msmqQueue;

		// Token: 0x040035B1 RID: 13745
		private List<ArraySegment<byte>> buffers;

		// Token: 0x040035B2 RID: 13746
		private Transaction associatedTx;

		// Token: 0x040035B3 RID: 13747
		private IOutputSession session;

		// Token: 0x040035B4 RID: 13748
		private MsmqChannelFactory<IOutputSessionChannel> factory;

		// Token: 0x040035B5 RID: 13749
		private MessageEncoder encoder;

		// Token: 0x040035B6 RID: 13750
		private SecurityTokenProviderContainer certificateTokenProvider;

		// Token: 0x02000D92 RID: 3474
		private class OutputSession : IOutputSession, ISession
		{
			// Token: 0x17001C3A RID: 7226
			// (get) Token: 0x06007EAC RID: 32428 RVA: 0x001D7EBE File Offset: 0x001D60BE
			public string Id
			{
				get
				{
					return this.id;
				}
			}

			// Token: 0x040048AC RID: 18604
			private string id = "uuid:/session-gram/" + Guid.NewGuid().ToString();
		}

		// Token: 0x02000D93 RID: 3475
		private class TransactionEnlistment : IEnlistmentNotification
		{
			// Token: 0x06007EAE RID: 32430 RVA: 0x001D7EFE File Offset: 0x001D60FE
			public TransactionEnlistment(MsmqOutputSessionChannel channel, Transaction transaction)
			{
				this.channel = channel;
				this.transaction = transaction;
			}

			// Token: 0x06007EAF RID: 32431 RVA: 0x001D7F14 File Offset: 0x001D6114
			public void Prepare(PreparingEnlistment preparingEnlistment)
			{
				if (this.channel.State != CommunicationState.Closed)
				{
					this.channel.Fault();
					Exception e = DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqSessionChannelsMustBeClosed")));
					preparingEnlistment.ForceRollback(e);
					return;
				}
				preparingEnlistment.Prepared();
			}

			// Token: 0x06007EB0 RID: 32432 RVA: 0x001D7F62 File Offset: 0x001D6162
			public void Commit(Enlistment enlistment)
			{
				enlistment.Done();
			}

			// Token: 0x06007EB1 RID: 32433 RVA: 0x001D7F6A File Offset: 0x001D616A
			public void Rollback(Enlistment enlistment)
			{
				this.channel.Fault();
				enlistment.Done();
			}

			// Token: 0x06007EB2 RID: 32434 RVA: 0x001D7F7D File Offset: 0x001D617D
			public void InDoubt(Enlistment enlistment)
			{
				enlistment.Done();
			}

			// Token: 0x040048AD RID: 18605
			private MsmqOutputSessionChannel channel;

			// Token: 0x040048AE RID: 18606
			private Transaction transaction;
		}
	}
}
