using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008DB RID: 2267
	internal abstract class MsmqChannelFactoryBase<TChannel> : TransportChannelFactory<TChannel>
	{
		// Token: 0x06005649 RID: 22089 RVA: 0x0013C219 File Offset: 0x0013A419
		protected MsmqChannelFactoryBase(MsmqBindingElementBase bindingElement, BindingContext context) : this(bindingElement, context, TransportDefaults.GetDefaultMessageEncoderFactory())
		{
		}

		// Token: 0x0600564A RID: 22090 RVA: 0x0013C228 File Offset: 0x0013A428
		protected MsmqChannelFactoryBase(MsmqBindingElementBase bindingElement, BindingContext context, MessageEncoderFactory encoderFactory) : base(bindingElement, context)
		{
			this.addressTranslator = bindingElement.AddressTranslator;
			this.customDeadLetterQueue = bindingElement.CustomDeadLetterQueue;
			this.durable = bindingElement.Durable;
			this.deadLetterQueue = bindingElement.DeadLetterQueue;
			this.exactlyOnce = bindingElement.ExactlyOnce;
			this.msmqTransportSecurity = new MsmqTransportSecurity(bindingElement.MsmqTransportSecurity);
			this.timeToLive = bindingElement.TimeToLive;
			this.useMsmqTracing = bindingElement.UseMsmqTracing;
			this.useSourceJournal = bindingElement.UseSourceJournal;
			if (this.MsmqTransportSecurity.MsmqAuthenticationMode == MsmqAuthenticationMode.Certificate)
			{
				this.InitializeSecurityTokenManager(context);
			}
			if (null != this.customDeadLetterQueue)
			{
				this.deadLetterQueuePathName = MsmqUri.DeadLetterQueueAddressTranslator.UriToFormatName(this.customDeadLetterQueue);
			}
		}

		// Token: 0x1700150D RID: 5389
		// (get) Token: 0x0600564B RID: 22091 RVA: 0x0013C2EE File Offset: 0x0013A4EE
		internal MsmqUri.IAddressTranslator AddressTranslator
		{
			get
			{
				return this.addressTranslator;
			}
		}

		// Token: 0x1700150E RID: 5390
		// (get) Token: 0x0600564C RID: 22092 RVA: 0x0013C2F6 File Offset: 0x0013A4F6
		public Uri CustomDeadLetterQueue
		{
			get
			{
				return this.customDeadLetterQueue;
			}
		}

		// Token: 0x1700150F RID: 5391
		// (get) Token: 0x0600564D RID: 22093 RVA: 0x0013C2FE File Offset: 0x0013A4FE
		public DeadLetterQueue DeadLetterQueue
		{
			get
			{
				return this.deadLetterQueue;
			}
		}

		// Token: 0x17001510 RID: 5392
		// (get) Token: 0x0600564E RID: 22094 RVA: 0x0013C306 File Offset: 0x0013A506
		internal string DeadLetterQueuePathName
		{
			get
			{
				return this.deadLetterQueuePathName;
			}
		}

		// Token: 0x17001511 RID: 5393
		// (get) Token: 0x0600564F RID: 22095 RVA: 0x0013C30E File Offset: 0x0013A50E
		public bool Durable
		{
			get
			{
				return this.durable;
			}
		}

		// Token: 0x17001512 RID: 5394
		// (get) Token: 0x06005650 RID: 22096 RVA: 0x0013C316 File Offset: 0x0013A516
		public bool ExactlyOnce
		{
			get
			{
				return this.exactlyOnce;
			}
		}

		// Token: 0x17001513 RID: 5395
		// (get) Token: 0x06005651 RID: 22097 RVA: 0x0013C31E File Offset: 0x0013A51E
		public MsmqTransportSecurity MsmqTransportSecurity
		{
			get
			{
				return this.msmqTransportSecurity;
			}
		}

		// Token: 0x17001514 RID: 5396
		// (get) Token: 0x06005652 RID: 22098 RVA: 0x0013C326 File Offset: 0x0013A526
		public override string Scheme
		{
			get
			{
				return this.addressTranslator.Scheme;
			}
		}

		// Token: 0x17001515 RID: 5397
		// (get) Token: 0x06005653 RID: 22099 RVA: 0x0013C333 File Offset: 0x0013A533
		public TimeSpan TimeToLive
		{
			get
			{
				return this.timeToLive;
			}
		}

		// Token: 0x17001516 RID: 5398
		// (get) Token: 0x06005654 RID: 22100 RVA: 0x0013C33B File Offset: 0x0013A53B
		public SecurityTokenManager SecurityTokenManager
		{
			get
			{
				return this.securityTokenManager;
			}
		}

		// Token: 0x17001517 RID: 5399
		// (get) Token: 0x06005655 RID: 22101 RVA: 0x0013C343 File Offset: 0x0013A543
		public bool UseSourceJournal
		{
			get
			{
				return this.useSourceJournal;
			}
		}

		// Token: 0x17001518 RID: 5400
		// (get) Token: 0x06005656 RID: 22102 RVA: 0x0013C34B File Offset: 0x0013A54B
		public bool UseMsmqTracing
		{
			get
			{
				return this.useMsmqTracing;
			}
		}

		// Token: 0x17001519 RID: 5401
		// (get) Token: 0x06005657 RID: 22103 RVA: 0x0013C353 File Offset: 0x0013A553
		internal bool IsMsmqX509SecurityConfigured
		{
			get
			{
				return MsmqAuthenticationMode.Certificate == this.MsmqTransportSecurity.MsmqAuthenticationMode;
			}
		}

		// Token: 0x06005658 RID: 22104 RVA: 0x0013C364 File Offset: 0x0013A564
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitializeSecurityTokenManager(BindingContext context)
		{
			SecurityCredentialsManager securityCredentialsManager = context.BindingParameters.Find<SecurityCredentialsManager>();
			if (securityCredentialsManager != null)
			{
				this.securityTokenManager = securityCredentialsManager.CreateSecurityTokenManager();
			}
		}

		// Token: 0x06005659 RID: 22105 RVA: 0x0013C38C File Offset: 0x0013A58C
		internal SecurityTokenProvider CreateTokenProvider(EndpointAddress to, Uri via)
		{
			InitiatorServiceModelSecurityTokenRequirement initiatorServiceModelSecurityTokenRequirement = new InitiatorServiceModelSecurityTokenRequirement();
			initiatorServiceModelSecurityTokenRequirement.TokenType = SecurityTokenTypes.X509Certificate;
			initiatorServiceModelSecurityTokenRequirement.TargetAddress = to;
			initiatorServiceModelSecurityTokenRequirement.Via = via;
			initiatorServiceModelSecurityTokenRequirement.KeyUsage = SecurityKeyUsage.Signature;
			initiatorServiceModelSecurityTokenRequirement.TransportScheme = this.Scheme;
			return this.SecurityTokenManager.CreateSecurityTokenProvider(initiatorServiceModelSecurityTokenRequirement);
		}

		// Token: 0x0600565A RID: 22106 RVA: 0x0013C3D7 File Offset: 0x0013A5D7
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal SecurityTokenProviderContainer CreateX509TokenProvider(EndpointAddress to, Uri via)
		{
			if (MsmqAuthenticationMode.Certificate == this.MsmqTransportSecurity.MsmqAuthenticationMode && this.SecurityTokenManager != null)
			{
				return new SecurityTokenProviderContainer(this.CreateTokenProvider(to, via));
			}
			return null;
		}

		// Token: 0x0600565B RID: 22107 RVA: 0x0013C3FE File Offset: 0x0013A5FE
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x0600565C RID: 22108 RVA: 0x0013C407 File Offset: 0x0013A607
		protected override void OnEndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x0600565D RID: 22109 RVA: 0x0013C40F File Offset: 0x0013A60F
		protected override void OnOpen(TimeSpan timeout)
		{
		}

		// Token: 0x0400355A RID: 13658
		private MsmqUri.IAddressTranslator addressTranslator;

		// Token: 0x0400355B RID: 13659
		private Uri customDeadLetterQueue;

		// Token: 0x0400355C RID: 13660
		private bool durable;

		// Token: 0x0400355D RID: 13661
		private DeadLetterQueue deadLetterQueue;

		// Token: 0x0400355E RID: 13662
		private string deadLetterQueuePathName;

		// Token: 0x0400355F RID: 13663
		private bool exactlyOnce = true;

		// Token: 0x04003560 RID: 13664
		private TimeSpan timeToLive;

		// Token: 0x04003561 RID: 13665
		private MsmqTransportSecurity msmqTransportSecurity;

		// Token: 0x04003562 RID: 13666
		private bool useMsmqTracing;

		// Token: 0x04003563 RID: 13667
		private bool useSourceJournal;

		// Token: 0x04003564 RID: 13668
		private SecurityTokenManager securityTokenManager;
	}
}
