using System;
using System.Runtime;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Security
{
	// Token: 0x020002BF RID: 703
	internal sealed class DuplexSecurityProtocolFactory : SecurityProtocolFactory
	{
		// Token: 0x06001633 RID: 5683 RVA: 0x00054666 File Offset: 0x00052866
		public DuplexSecurityProtocolFactory()
		{
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x00054675 File Offset: 0x00052875
		public DuplexSecurityProtocolFactory(SecurityProtocolFactory forwardProtocolFactory, SecurityProtocolFactory reverseProtocolFactory) : this()
		{
			this.forwardProtocolFactory = forwardProtocolFactory;
			this.reverseProtocolFactory = reverseProtocolFactory;
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x0005468B File Offset: 0x0005288B
		// (set) Token: 0x06001636 RID: 5686 RVA: 0x00054693 File Offset: 0x00052893
		public SecurityProtocolFactory ForwardProtocolFactory
		{
			get
			{
				return this.forwardProtocolFactory;
			}
			set
			{
				base.ThrowIfImmutable();
				this.forwardProtocolFactory = value;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x000546A2 File Offset: 0x000528A2
		private SecurityProtocolFactory ProtocolFactoryForIncomingMessages
		{
			get
			{
				if (!base.ActAsInitiator)
				{
					return this.ForwardProtocolFactory;
				}
				return this.ReverseProtocolFactory;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x000546B9 File Offset: 0x000528B9
		private SecurityProtocolFactory ProtocolFactoryForOutgoingMessages
		{
			get
			{
				if (!base.ActAsInitiator)
				{
					return this.ReverseProtocolFactory;
				}
				return this.ForwardProtocolFactory;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x000546D0 File Offset: 0x000528D0
		// (set) Token: 0x0600163A RID: 5690 RVA: 0x000546D8 File Offset: 0x000528D8
		public bool RequireSecurityOnBothDuplexDirections
		{
			get
			{
				return this.requireSecurityOnBothDuplexDirections;
			}
			set
			{
				base.ThrowIfImmutable();
				this.requireSecurityOnBothDuplexDirections = value;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x000546E7 File Offset: 0x000528E7
		// (set) Token: 0x0600163C RID: 5692 RVA: 0x000546EF File Offset: 0x000528EF
		public SecurityProtocolFactory ReverseProtocolFactory
		{
			get
			{
				return this.reverseProtocolFactory;
			}
			set
			{
				base.ThrowIfImmutable();
				this.reverseProtocolFactory = value;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x000546FE File Offset: 0x000528FE
		public override bool SupportsDuplex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x00054701 File Offset: 0x00052901
		public override bool SupportsReplayDetection
		{
			get
			{
				return this.ForwardProtocolFactory != null && this.ForwardProtocolFactory.SupportsReplayDetection && this.ReverseProtocolFactory != null && this.ReverseProtocolFactory.SupportsReplayDetection;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x0005472D File Offset: 0x0005292D
		public override bool SupportsRequestReply
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00054730 File Offset: 0x00052930
		public override EndpointIdentity GetIdentityOfSelf()
		{
			SecurityProtocolFactory protocolFactoryForIncomingMessages = this.ProtocolFactoryForIncomingMessages;
			if (protocolFactoryForIncomingMessages != null)
			{
				return protocolFactoryForIncomingMessages.GetIdentityOfSelf();
			}
			return base.GetIdentityOfSelf();
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00054754 File Offset: 0x00052954
		public override void OnAbort()
		{
			if (this.forwardProtocolFactory != null)
			{
				this.forwardProtocolFactory.Close(true, TimeSpan.Zero);
			}
			if (this.reverseProtocolFactory != null)
			{
				this.reverseProtocolFactory.Close(true, TimeSpan.Zero);
			}
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x00054788 File Offset: 0x00052988
		public override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.forwardProtocolFactory != null)
			{
				this.forwardProtocolFactory.Close(false, timeoutHelper.RemainingTime());
			}
			if (this.reverseProtocolFactory != null)
			{
				this.reverseProtocolFactory.Close(false, timeoutHelper.RemainingTime());
			}
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x000547D4 File Offset: 0x000529D4
		protected override SecurityProtocol OnCreateSecurityProtocol(EndpointAddress target, Uri via, object listenerSecurityState, TimeSpan timeout)
		{
			SecurityProtocolFactory protocolFactoryForOutgoingMessages = this.ProtocolFactoryForOutgoingMessages;
			SecurityProtocolFactory protocolFactoryForIncomingMessages = this.ProtocolFactoryForIncomingMessages;
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			SecurityProtocol outgoingProtocol = (protocolFactoryForOutgoingMessages == null) ? null : protocolFactoryForOutgoingMessages.CreateSecurityProtocol(target, via, listenerSecurityState, false, timeoutHelper.RemainingTime());
			SecurityProtocol incomingProtocol = (protocolFactoryForIncomingMessages == null) ? null : protocolFactoryForIncomingMessages.CreateSecurityProtocol(null, null, listenerSecurityState, false, timeoutHelper.RemainingTime());
			return new DuplexSecurityProtocolFactory.DuplexSecurityProtocol(outgoingProtocol, incomingProtocol);
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x00054834 File Offset: 0x00052A34
		public override void OnOpen(TimeSpan timeout)
		{
			if (this.ForwardProtocolFactory != null && this.ForwardProtocolFactory == this.ReverseProtocolFactory)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("ReverseProtocolFactory", SR.GetString("SameProtocolFactoryCannotBeSetForBothDuplexDirections"));
			}
			if (this.forwardProtocolFactory != null)
			{
				this.forwardProtocolFactory.ListenUri = base.ListenUri;
			}
			if (this.reverseProtocolFactory != null)
			{
				this.reverseProtocolFactory.ListenUri = base.ListenUri;
			}
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.Open(this.ForwardProtocolFactory, base.ActAsInitiator, "ForwardProtocolFactory", timeoutHelper.RemainingTime());
			this.Open(this.ReverseProtocolFactory, !base.ActAsInitiator, "ReverseProtocolFactory", timeoutHelper.RemainingTime());
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x000548EA File Offset: 0x00052AEA
		private void Open(SecurityProtocolFactory factory, bool actAsInitiator, string propertyName, TimeSpan timeout)
		{
			if (factory != null)
			{
				factory.Open(actAsInitiator, timeout);
				return;
			}
			if (this.RequireSecurityOnBothDuplexDirections)
			{
				base.OnPropertySettingsError(propertyName, true);
			}
		}

		// Token: 0x04001BB6 RID: 7094
		private SecurityProtocolFactory forwardProtocolFactory;

		// Token: 0x04001BB7 RID: 7095
		private SecurityProtocolFactory reverseProtocolFactory;

		// Token: 0x04001BB8 RID: 7096
		private bool requireSecurityOnBothDuplexDirections = true;

		// Token: 0x02000B4A RID: 2890
		private sealed class DuplexSecurityProtocol : SecurityProtocol
		{
			// Token: 0x060070E6 RID: 28902 RVA: 0x001A434A File Offset: 0x001A254A
			public DuplexSecurityProtocol(SecurityProtocol outgoingProtocol, SecurityProtocol incomingProtocol) : base(incomingProtocol.SecurityProtocolFactory, null, null)
			{
				this.outgoingProtocol = outgoingProtocol;
				this.incomingProtocol = incomingProtocol;
			}

			// Token: 0x060070E7 RID: 28903 RVA: 0x001A4368 File Offset: 0x001A2568
			public override void OnOpen(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				this.outgoingProtocol.Open(timeoutHelper.RemainingTime());
				this.incomingProtocol.Open(timeoutHelper.RemainingTime());
			}

			// Token: 0x060070E8 RID: 28904 RVA: 0x001A43A4 File Offset: 0x001A25A4
			public override void OnClose(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				this.outgoingProtocol.Close(false, timeoutHelper.RemainingTime());
				this.incomingProtocol.Close(false, timeoutHelper.RemainingTime());
			}

			// Token: 0x060070E9 RID: 28905 RVA: 0x001A43DF File Offset: 0x001A25DF
			public override void OnAbort()
			{
				this.outgoingProtocol.Close(true, TimeSpan.Zero);
				this.incomingProtocol.Close(true, TimeSpan.Zero);
			}

			// Token: 0x060070EA RID: 28906 RVA: 0x001A4403 File Offset: 0x001A2603
			public override IAsyncResult BeginSecureOutgoingMessage(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				if (this.outgoingProtocol != null)
				{
					return this.outgoingProtocol.BeginSecureOutgoingMessage(message, timeout, callback, state);
				}
				return new CompletedAsyncResult<Message>(message, callback, state);
			}

			// Token: 0x060070EB RID: 28907 RVA: 0x001A4427 File Offset: 0x001A2627
			public override IAsyncResult BeginSecureOutgoingMessage(Message message, TimeSpan timeout, SecurityProtocolCorrelationState correlationState, AsyncCallback callback, object state)
			{
				if (this.outgoingProtocol != null)
				{
					return this.outgoingProtocol.BeginSecureOutgoingMessage(message, timeout, correlationState, callback, state);
				}
				return new CompletedAsyncResult<Message, SecurityProtocolCorrelationState>(message, null, callback, state);
			}

			// Token: 0x060070EC RID: 28908 RVA: 0x001A444F File Offset: 0x001A264F
			public override void EndSecureOutgoingMessage(IAsyncResult result, out Message message)
			{
				if (this.outgoingProtocol != null)
				{
					this.outgoingProtocol.EndSecureOutgoingMessage(result, out message);
					return;
				}
				message = CompletedAsyncResult<Message>.End(result);
			}

			// Token: 0x060070ED RID: 28909 RVA: 0x001A446F File Offset: 0x001A266F
			public override void EndSecureOutgoingMessage(IAsyncResult result, out Message message, out SecurityProtocolCorrelationState newCorrelationState)
			{
				if (this.outgoingProtocol != null)
				{
					this.outgoingProtocol.EndSecureOutgoingMessage(result, out message, out newCorrelationState);
					return;
				}
				message = CompletedAsyncResult<Message, SecurityProtocolCorrelationState>.End(result, out newCorrelationState);
			}

			// Token: 0x060070EE RID: 28910 RVA: 0x001A4491 File Offset: 0x001A2691
			public override void SecureOutgoingMessage(ref Message message, TimeSpan timeout)
			{
				if (this.outgoingProtocol != null)
				{
					this.outgoingProtocol.SecureOutgoingMessage(ref message, timeout);
				}
			}

			// Token: 0x060070EF RID: 28911 RVA: 0x001A44A8 File Offset: 0x001A26A8
			public override SecurityProtocolCorrelationState SecureOutgoingMessage(ref Message message, TimeSpan timeout, SecurityProtocolCorrelationState correlationState)
			{
				if (this.outgoingProtocol != null)
				{
					return this.outgoingProtocol.SecureOutgoingMessage(ref message, timeout, correlationState);
				}
				return null;
			}

			// Token: 0x060070F0 RID: 28912 RVA: 0x001A44C2 File Offset: 0x001A26C2
			public override void VerifyIncomingMessage(ref Message message, TimeSpan timeout)
			{
				if (this.incomingProtocol != null)
				{
					this.incomingProtocol.VerifyIncomingMessage(ref message, timeout);
				}
			}

			// Token: 0x060070F1 RID: 28913 RVA: 0x001A44D9 File Offset: 0x001A26D9
			public override SecurityProtocolCorrelationState VerifyIncomingMessage(ref Message message, TimeSpan timeout, params SecurityProtocolCorrelationState[] correlationStates)
			{
				if (this.incomingProtocol != null)
				{
					return this.incomingProtocol.VerifyIncomingMessage(ref message, timeout, correlationStates);
				}
				return null;
			}

			// Token: 0x04004036 RID: 16438
			private readonly SecurityProtocol outgoingProtocol;

			// Token: 0x04004037 RID: 16439
			private readonly SecurityProtocol incomingProtocol;
		}
	}
}
