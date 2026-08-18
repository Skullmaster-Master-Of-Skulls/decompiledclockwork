using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008EE RID: 2286
	internal class MsmqOutputMessage<TChannel> : NativeMsmqMessage
	{
		// Token: 0x06005722 RID: 22306 RVA: 0x0013FACD File Offset: 0x0013DCCD
		public MsmqOutputMessage(MsmqChannelFactoryBase<TChannel> factory, int bodySize, EndpointAddress remoteAddress) : this(factory, bodySize, remoteAddress, 0)
		{
		}

		// Token: 0x06005723 RID: 22307 RVA: 0x0013FADC File Offset: 0x0013DCDC
		protected MsmqOutputMessage(MsmqChannelFactoryBase<TChannel> factory, int bodySize, EndpointAddress remoteAddress, int additionalPropertyCount) : base(15 + additionalPropertyCount)
		{
			this.body = new NativeMsmqMessage.BufferProperty(this, 9, bodySize);
			this.messageId = new NativeMsmqMessage.BufferProperty(this, 2, 20);
			this.EnsureBodyTypeProperty(4113);
			this.EnsureJournalProperty(2, factory.UseSourceJournal);
			this.delivery = new NativeMsmqMessage.ByteProperty(this, 5);
			if (factory.Durable)
			{
				this.delivery.Value = 1;
			}
			else
			{
				this.delivery.Value = 0;
			}
			if (factory.TimeToLive != TimeSpan.MaxValue)
			{
				int value = MsmqDuration.FromTimeSpan(factory.TimeToLive);
				this.EnsureTimeToReachQueueProperty(value);
				this.timeToBeReceived = new NativeMsmqMessage.IntProperty(this, 14, value);
			}
			switch (factory.DeadLetterQueue)
			{
			case DeadLetterQueue.None:
				this.EnsureJournalProperty(1, false);
				break;
			case DeadLetterQueue.System:
				this.EnsureJournalProperty(1, true);
				break;
			case DeadLetterQueue.Custom:
				this.EnsureJournalProperty(1, true);
				this.EnsureDeadLetterQueueProperty(factory.DeadLetterQueuePathName);
				break;
			}
			if (MsmqAuthenticationMode.WindowsDomain == factory.MsmqTransportSecurity.MsmqAuthenticationMode)
			{
				this.EnsureSenderIdTypeProperty(1);
				this.authLevel = new NativeMsmqMessage.IntProperty(this, 24, 1);
				this.hashAlgorithm = new NativeMsmqMessage.IntProperty(this, 26, MsmqSecureHashAlgorithmHelper.ToInt32(factory.MsmqTransportSecurity.MsmqSecureHashAlgorithm));
				if (ProtectionLevel.EncryptAndSign == factory.MsmqTransportSecurity.MsmqProtectionLevel)
				{
					this.privLevel = new NativeMsmqMessage.IntProperty(this, 23, 3);
					this.encryptionAlgorithm = new NativeMsmqMessage.IntProperty(this, 27, MsmqEncryptionAlgorithmHelper.ToInt32(factory.MsmqTransportSecurity.MsmqEncryptionAlgorithm));
				}
			}
			else if (MsmqAuthenticationMode.Certificate == factory.MsmqTransportSecurity.MsmqAuthenticationMode)
			{
				this.authLevel = new NativeMsmqMessage.IntProperty(this, 24, 1);
				this.hashAlgorithm = new NativeMsmqMessage.IntProperty(this, 26, MsmqSecureHashAlgorithmHelper.ToInt32(factory.MsmqTransportSecurity.MsmqSecureHashAlgorithm));
				if (ProtectionLevel.EncryptAndSign == factory.MsmqTransportSecurity.MsmqProtectionLevel)
				{
					this.privLevel = new NativeMsmqMessage.IntProperty(this, 23, 3);
					this.encryptionAlgorithm = new NativeMsmqMessage.IntProperty(this, 27, MsmqEncryptionAlgorithmHelper.ToInt32(factory.MsmqTransportSecurity.MsmqEncryptionAlgorithm));
				}
				this.EnsureSenderIdTypeProperty(0);
				this.senderCert = new NativeMsmqMessage.BufferProperty(this, 28);
			}
			else
			{
				this.authLevel = new NativeMsmqMessage.IntProperty(this, 24, 0);
				this.EnsureSenderIdTypeProperty(0);
			}
			this.trace = new NativeMsmqMessage.ByteProperty(this, 41, factory.UseMsmqTracing ? 1 : 0);
		}

		// Token: 0x1700153A RID: 5434
		// (get) Token: 0x06005724 RID: 22308 RVA: 0x0013FD13 File Offset: 0x0013DF13
		public NativeMsmqMessage.BufferProperty Body
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x1700153B RID: 5435
		// (get) Token: 0x06005725 RID: 22309 RVA: 0x0013FD1B File Offset: 0x0013DF1B
		public NativeMsmqMessage.BufferProperty MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x06005726 RID: 22310 RVA: 0x0013FD24 File Offset: 0x0013DF24
		internal void ApplyCertificateIfNeeded(SecurityTokenProviderContainer certificateTokenProvider, MsmqAuthenticationMode authenticationMode, TimeSpan timeout)
		{
			if (MsmqAuthenticationMode.Certificate == authenticationMode)
			{
				if (certificateTokenProvider == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificateTokenProvider");
				}
				X509Certificate2 certificate = certificateTokenProvider.GetCertificate(timeout);
				if (certificate == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new InvalidOperationException(SR.GetString("MsmqCertificateNotFound")));
				}
				this.senderCert.SetBufferReference(certificate.GetRawCertData());
			}
		}

		// Token: 0x06005727 RID: 22311 RVA: 0x0013FD7E File Offset: 0x0013DF7E
		protected void EnsureBodyTypeProperty(int value)
		{
			if (this.bodyType == null)
			{
				this.bodyType = new NativeMsmqMessage.IntProperty(this, 42);
			}
			this.bodyType.Value = value;
		}

		// Token: 0x06005728 RID: 22312 RVA: 0x0013FDA2 File Offset: 0x0013DFA2
		protected void EnsureDeadLetterQueueProperty(string value)
		{
			if (value.Length > 0)
			{
				if (this.deadLetterQueue == null)
				{
					this.deadLetterQueue = new NativeMsmqMessage.StringProperty(this, 67, value);
					return;
				}
				this.deadLetterQueue.SetValue(value);
			}
		}

		// Token: 0x06005729 RID: 22313 RVA: 0x0013FDD1 File Offset: 0x0013DFD1
		protected void EnsureSenderIdTypeProperty(int value)
		{
			if (this.senderIdType == null)
			{
				this.senderIdType = new NativeMsmqMessage.IntProperty(this, 22);
			}
			this.senderIdType.Value = value;
		}

		// Token: 0x0600572A RID: 22314 RVA: 0x0013FDF5 File Offset: 0x0013DFF5
		protected void EnsureTimeToReachQueueProperty(int value)
		{
			if (this.timeToReachQueue == null)
			{
				this.timeToReachQueue = new NativeMsmqMessage.IntProperty(this, 13);
			}
			this.timeToReachQueue.Value = value;
		}

		// Token: 0x0600572B RID: 22315 RVA: 0x0013FE1C File Offset: 0x0013E01C
		protected void EnsureJournalProperty(byte flag, bool isFlagSet)
		{
			if (this.journal == null)
			{
				this.journal = new NativeMsmqMessage.ByteProperty(this, 7);
			}
			if (isFlagSet)
			{
				NativeMsmqMessage.ByteProperty byteProperty = this.journal;
				byteProperty.Value |= flag;
				return;
			}
			NativeMsmqMessage.ByteProperty byteProperty2 = this.journal;
			byteProperty2.Value &= ~flag;
		}

		// Token: 0x0400359B RID: 13723
		private NativeMsmqMessage.BufferProperty body;

		// Token: 0x0400359C RID: 13724
		private NativeMsmqMessage.IntProperty bodyType;

		// Token: 0x0400359D RID: 13725
		private NativeMsmqMessage.ByteProperty delivery;

		// Token: 0x0400359E RID: 13726
		private NativeMsmqMessage.IntProperty timeToReachQueue;

		// Token: 0x0400359F RID: 13727
		private NativeMsmqMessage.IntProperty timeToBeReceived;

		// Token: 0x040035A0 RID: 13728
		private NativeMsmqMessage.ByteProperty journal;

		// Token: 0x040035A1 RID: 13729
		private NativeMsmqMessage.StringProperty deadLetterQueue;

		// Token: 0x040035A2 RID: 13730
		private NativeMsmqMessage.IntProperty senderIdType;

		// Token: 0x040035A3 RID: 13731
		private NativeMsmqMessage.IntProperty authLevel;

		// Token: 0x040035A4 RID: 13732
		private NativeMsmqMessage.BufferProperty senderCert;

		// Token: 0x040035A5 RID: 13733
		private NativeMsmqMessage.IntProperty privLevel;

		// Token: 0x040035A6 RID: 13734
		private NativeMsmqMessage.ByteProperty trace;

		// Token: 0x040035A7 RID: 13735
		private NativeMsmqMessage.BufferProperty messageId;

		// Token: 0x040035A8 RID: 13736
		private NativeMsmqMessage.IntProperty encryptionAlgorithm;

		// Token: 0x040035A9 RID: 13737
		private NativeMsmqMessage.IntProperty hashAlgorithm;
	}
}
