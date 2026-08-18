using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008E2 RID: 2274
	internal class MsmqInputMessage : NativeMsmqMessage
	{
		// Token: 0x06005697 RID: 22167 RVA: 0x0013E0BF File Offset: 0x0013C2BF
		public MsmqInputMessage() : this(0, 4194304)
		{
		}

		// Token: 0x06005698 RID: 22168 RVA: 0x0013E0CD File Offset: 0x0013C2CD
		public MsmqInputMessage(int maxBufferSize) : this(0, maxBufferSize)
		{
		}

		// Token: 0x06005699 RID: 22169 RVA: 0x0013E0D7 File Offset: 0x0013C2D7
		protected MsmqInputMessage(int additionalPropertyCount, int maxBufferSize) : this(additionalPropertyCount, new MsmqInputMessage.SizeQuota(maxBufferSize))
		{
		}

		// Token: 0x0600569A RID: 22170 RVA: 0x0013E0E8 File Offset: 0x0013C2E8
		protected MsmqInputMessage(int additionalPropertyCount, MsmqInputMessage.SizeQuota bufferSizeQuota) : base(12 + additionalPropertyCount)
		{
			this.maxBufferSize = bufferSizeQuota.MaxSize;
			this.body = new NativeMsmqMessage.BufferProperty(this, 9, bufferSizeQuota.AllocIfAvailable(4096));
			this.bodyLength = new NativeMsmqMessage.IntProperty(this, 10);
			this.messageId = new NativeMsmqMessage.BufferProperty(this, 2, 20);
			this.lookupId = new NativeMsmqMessage.LongProperty(this, 60);
			this.cls = new NativeMsmqMessage.ShortProperty(this, 1);
			this.senderId = new NativeMsmqMessage.BufferProperty(this, 20, 256);
			this.senderIdLength = new NativeMsmqMessage.IntProperty(this, 21);
			this.senderCertificate = new NativeMsmqMessage.BufferProperty(this, 28, bufferSizeQuota.AllocIfAvailable(4096));
			this.senderCertificateLength = new NativeMsmqMessage.IntProperty(this, 29);
			if (Msmq.IsAdvancedPoisonHandlingSupported)
			{
				this.lastMovedTime = new NativeMsmqMessage.IntProperty(this, 75);
				this.abortCount = new NativeMsmqMessage.IntProperty(this, 69);
				this.moveCount = new NativeMsmqMessage.IntProperty(this, 70);
			}
		}

		// Token: 0x0600569B RID: 22171 RVA: 0x0013E1D5 File Offset: 0x0013C3D5
		public override void GrowBuffers()
		{
			this.OnGrowBuffers(new MsmqInputMessage.SizeQuota(this.maxBufferSize));
		}

		// Token: 0x0600569C RID: 22172 RVA: 0x0013E1E8 File Offset: 0x0013C3E8
		protected virtual void OnGrowBuffers(MsmqInputMessage.SizeQuota bufferSizeQuota)
		{
			bufferSizeQuota.Alloc(this.senderIdLength.Value);
			this.senderId.EnsureBufferLength(this.senderIdLength.Value);
			bufferSizeQuota.Alloc(this.senderCertificateLength.Value);
			this.senderCertificate.EnsureBufferLength(this.senderCertificateLength.Value);
			bufferSizeQuota.Alloc(this.bodyLength.Value);
			this.body.EnsureBufferLength(this.bodyLength.Value);
		}

		// Token: 0x1700151F RID: 5407
		// (get) Token: 0x0600569D RID: 22173 RVA: 0x0013E26A File Offset: 0x0013C46A
		public NativeMsmqMessage.BufferProperty SenderId
		{
			get
			{
				return this.senderId;
			}
		}

		// Token: 0x17001520 RID: 5408
		// (get) Token: 0x0600569E RID: 22174 RVA: 0x0013E272 File Offset: 0x0013C472
		public NativeMsmqMessage.IntProperty SenderIdLength
		{
			get
			{
				return this.senderIdLength;
			}
		}

		// Token: 0x17001521 RID: 5409
		// (get) Token: 0x0600569F RID: 22175 RVA: 0x0013E27A File Offset: 0x0013C47A
		public NativeMsmqMessage.LongProperty LookupId
		{
			get
			{
				return this.lookupId;
			}
		}

		// Token: 0x17001522 RID: 5410
		// (get) Token: 0x060056A0 RID: 22176 RVA: 0x0013E282 File Offset: 0x0013C482
		public NativeMsmqMessage.IntProperty AbortCount
		{
			get
			{
				return this.abortCount;
			}
		}

		// Token: 0x17001523 RID: 5411
		// (get) Token: 0x060056A1 RID: 22177 RVA: 0x0013E28A File Offset: 0x0013C48A
		public NativeMsmqMessage.IntProperty MoveCount
		{
			get
			{
				return this.moveCount;
			}
		}

		// Token: 0x17001524 RID: 5412
		// (get) Token: 0x060056A2 RID: 22178 RVA: 0x0013E292 File Offset: 0x0013C492
		public NativeMsmqMessage.BufferProperty SenderCertificate
		{
			get
			{
				return this.senderCertificate;
			}
		}

		// Token: 0x17001525 RID: 5413
		// (get) Token: 0x060056A3 RID: 22179 RVA: 0x0013E29A File Offset: 0x0013C49A
		public NativeMsmqMessage.IntProperty SenderCertificateLength
		{
			get
			{
				return this.senderCertificateLength;
			}
		}

		// Token: 0x17001526 RID: 5414
		// (get) Token: 0x060056A4 RID: 22180 RVA: 0x0013E2A2 File Offset: 0x0013C4A2
		public NativeMsmqMessage.IntProperty LastMovedTime
		{
			get
			{
				return this.lastMovedTime;
			}
		}

		// Token: 0x17001527 RID: 5415
		// (get) Token: 0x060056A5 RID: 22181 RVA: 0x0013E2AA File Offset: 0x0013C4AA
		public NativeMsmqMessage.BufferProperty Body
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x17001528 RID: 5416
		// (get) Token: 0x060056A6 RID: 22182 RVA: 0x0013E2B2 File Offset: 0x0013C4B2
		public NativeMsmqMessage.IntProperty BodyLength
		{
			get
			{
				return this.bodyLength;
			}
		}

		// Token: 0x17001529 RID: 5417
		// (get) Token: 0x060056A7 RID: 22183 RVA: 0x0013E2BA File Offset: 0x0013C4BA
		public NativeMsmqMessage.BufferProperty MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x1700152A RID: 5418
		// (get) Token: 0x060056A8 RID: 22184 RVA: 0x0013E2C2 File Offset: 0x0013C4C2
		public NativeMsmqMessage.ShortProperty Class
		{
			get
			{
				return this.cls;
			}
		}

		// Token: 0x04003572 RID: 13682
		private NativeMsmqMessage.BufferProperty senderId;

		// Token: 0x04003573 RID: 13683
		private NativeMsmqMessage.IntProperty senderIdLength;

		// Token: 0x04003574 RID: 13684
		private NativeMsmqMessage.LongProperty lookupId;

		// Token: 0x04003575 RID: 13685
		private NativeMsmqMessage.IntProperty abortCount;

		// Token: 0x04003576 RID: 13686
		private NativeMsmqMessage.IntProperty moveCount;

		// Token: 0x04003577 RID: 13687
		private NativeMsmqMessage.BufferProperty senderCertificate;

		// Token: 0x04003578 RID: 13688
		private NativeMsmqMessage.IntProperty senderCertificateLength;

		// Token: 0x04003579 RID: 13689
		private NativeMsmqMessage.IntProperty lastMovedTime;

		// Token: 0x0400357A RID: 13690
		private NativeMsmqMessage.BufferProperty body;

		// Token: 0x0400357B RID: 13691
		private NativeMsmqMessage.IntProperty bodyLength;

		// Token: 0x0400357C RID: 13692
		private NativeMsmqMessage.BufferProperty messageId;

		// Token: 0x0400357D RID: 13693
		private NativeMsmqMessage.ShortProperty cls;

		// Token: 0x0400357E RID: 13694
		private int maxBufferSize;

		// Token: 0x0400357F RID: 13695
		private const int maxSize = 4194304;

		// Token: 0x04003580 RID: 13696
		private const int initialBodySize = 4096;

		// Token: 0x04003581 RID: 13697
		private const int initialSenderIdSize = 256;

		// Token: 0x04003582 RID: 13698
		private const int initialCertificateSize = 4096;

		// Token: 0x02000D8C RID: 3468
		protected class SizeQuota
		{
			// Token: 0x06007E94 RID: 32404 RVA: 0x001D7BFB File Offset: 0x001D5DFB
			public SizeQuota(int maxSize)
			{
				this.maxSize = maxSize;
				this.remainingSize = maxSize;
			}

			// Token: 0x17001C38 RID: 7224
			// (get) Token: 0x06007E95 RID: 32405 RVA: 0x001D7C11 File Offset: 0x001D5E11
			public int MaxSize
			{
				get
				{
					return this.maxSize;
				}
			}

			// Token: 0x06007E96 RID: 32406 RVA: 0x001D7C19 File Offset: 0x001D5E19
			public void Alloc(int requiredSize)
			{
				if (requiredSize > this.remainingSize)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(MaxMessageSizeStream.CreateMaxReceivedMessageSizeExceededException((long)this.maxSize));
				}
				this.remainingSize -= requiredSize;
			}

			// Token: 0x06007E97 RID: 32407 RVA: 0x001D7C4C File Offset: 0x001D5E4C
			public int AllocIfAvailable(int desiredSize)
			{
				int num = Math.Min(desiredSize, this.remainingSize);
				this.remainingSize -= num;
				return num;
			}

			// Token: 0x040048A3 RID: 18595
			private int remainingSize;

			// Token: 0x040048A4 RID: 18596
			private int maxSize;
		}
	}
}
