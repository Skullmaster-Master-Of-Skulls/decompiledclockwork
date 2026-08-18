using System;
using System.Globalization;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Security
{
	// Token: 0x020002B0 RID: 688
	internal abstract class SecurityHeader : MessageHeader
	{
		// Token: 0x0600155A RID: 5466 RVA: 0x000513F8 File Offset: 0x0004F5F8
		public SecurityHeader(Message message, string actor, bool mustUnderstand, bool relay, SecurityStandardsManager standardsManager, SecurityAlgorithmSuite algorithmSuite, MessageDirection transferDirection)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (actor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("actor");
			}
			if (standardsManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("standardsManager");
			}
			if (algorithmSuite == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("algorithmSuite");
			}
			this.message = message;
			this.actor = actor;
			this.mustUnderstand = mustUnderstand;
			this.relay = relay;
			this.standardsManager = standardsManager;
			this.algorithmSuite = algorithmSuite;
			this.transferDirection = transferDirection;
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x0005149C File Offset: 0x0004F69C
		public override string Actor
		{
			get
			{
				return this.actor;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x000514A4 File Offset: 0x0004F6A4
		public SecurityAlgorithmSuite AlgorithmSuite
		{
			get
			{
				return this.algorithmSuite;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x000514AC File Offset: 0x0004F6AC
		// (set) Token: 0x0600155E RID: 5470 RVA: 0x000514B4 File Offset: 0x0004F6B4
		public bool EncryptedKeyContainsReferenceList
		{
			get
			{
				return this.encryptedKeyContainsReferenceList;
			}
			set
			{
				this.ThrowIfProcessingStarted();
				this.encryptedKeyContainsReferenceList = value;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x000514C3 File Offset: 0x0004F6C3
		// (set) Token: 0x06001560 RID: 5472 RVA: 0x000514CB File Offset: 0x0004F6CB
		public bool RequireMessageProtection
		{
			get
			{
				return this.requireMessageProtection;
			}
			set
			{
				this.ThrowIfProcessingStarted();
				this.requireMessageProtection = value;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x000514DA File Offset: 0x0004F6DA
		// (set) Token: 0x06001562 RID: 5474 RVA: 0x000514E2 File Offset: 0x0004F6E2
		public bool MaintainSignatureConfirmationState
		{
			get
			{
				return this.maintainSignatureConfirmationState;
			}
			set
			{
				this.ThrowIfProcessingStarted();
				this.maintainSignatureConfirmationState = value;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x000514F1 File Offset: 0x0004F6F1
		// (set) Token: 0x06001564 RID: 5476 RVA: 0x000514F9 File Offset: 0x0004F6F9
		protected Message Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x00051502 File Offset: 0x0004F702
		public override bool MustUnderstand
		{
			get
			{
				return this.mustUnderstand;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x0005150A File Offset: 0x0004F70A
		public override bool Relay
		{
			get
			{
				return this.relay;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x00051512 File Offset: 0x0004F712
		// (set) Token: 0x06001568 RID: 5480 RVA: 0x0005151A File Offset: 0x0004F71A
		public SecurityHeaderLayout Layout
		{
			get
			{
				return this.layout;
			}
			set
			{
				this.ThrowIfProcessingStarted();
				this.layout = value;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x00051529 File Offset: 0x0004F729
		public SecurityStandardsManager StandardsManager
		{
			get
			{
				return this.standardsManager;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x00051531 File Offset: 0x0004F731
		public MessageDirection MessageDirection
		{
			get
			{
				return this.transferDirection;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x00051539 File Offset: 0x0004F739
		protected MessageVersion Version
		{
			get
			{
				return this.message.Version;
			}
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00051546 File Offset: 0x0004F746
		protected void SetProcessingStarted()
		{
			this.processingStarted = true;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0005154F File Offset: 0x0004F74F
		protected void ThrowIfProcessingStarted()
		{
			if (this.processingStarted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("OperationCannotBeDoneAfterProcessingIsStarted")));
			}
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00051573 File Offset: 0x0004F773
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}(Actor = '{1}')", new object[]
			{
				base.GetType().Name,
				this.Actor
			});
		}

		// Token: 0x04001B50 RID: 6992
		private readonly string actor;

		// Token: 0x04001B51 RID: 6993
		private readonly SecurityAlgorithmSuite algorithmSuite;

		// Token: 0x04001B52 RID: 6994
		private bool encryptedKeyContainsReferenceList = true;

		// Token: 0x04001B53 RID: 6995
		private Message message;

		// Token: 0x04001B54 RID: 6996
		private readonly bool mustUnderstand;

		// Token: 0x04001B55 RID: 6997
		private readonly bool relay;

		// Token: 0x04001B56 RID: 6998
		private bool requireMessageProtection = true;

		// Token: 0x04001B57 RID: 6999
		private bool processingStarted;

		// Token: 0x04001B58 RID: 7000
		private bool maintainSignatureConfirmationState;

		// Token: 0x04001B59 RID: 7001
		private readonly SecurityStandardsManager standardsManager;

		// Token: 0x04001B5A RID: 7002
		private MessageDirection transferDirection;

		// Token: 0x04001B5B RID: 7003
		private SecurityHeaderLayout layout;
	}
}
