using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008ED RID: 2285
	public sealed class MsmqMessageProperty
	{
		// Token: 0x06005717 RID: 22295 RVA: 0x0013F8FC File Offset: 0x0013DAFC
		internal MsmqMessageProperty(MsmqInputMessage msmqMessage)
		{
			if (msmqMessage == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("msmqMessage");
			}
			this.lookupId = msmqMessage.LookupId.Value;
			if (msmqMessage.AbortCount != null)
			{
				this.abortCount = msmqMessage.AbortCount.Value;
			}
			if (msmqMessage.MoveCount != null)
			{
				this.moveCount = msmqMessage.MoveCount.Value;
			}
			this.acknowledge = (int)((ushort)msmqMessage.Class.Value);
			this.messageId = MsmqMessageId.ToString(msmqMessage.MessageId.Buffer);
		}

		// Token: 0x17001534 RID: 5428
		// (get) Token: 0x06005718 RID: 22296 RVA: 0x0013F98D File Offset: 0x0013DB8D
		public DeliveryFailure? DeliveryFailure
		{
			get
			{
				return MsmqMessageProperty.TryGetDeliveryFailure(this.messageId, this.acknowledge);
			}
		}

		// Token: 0x17001535 RID: 5429
		// (get) Token: 0x06005719 RID: 22297 RVA: 0x0013F9A0 File Offset: 0x0013DBA0
		public DeliveryStatus? DeliveryStatus
		{
			get
			{
				DeliveryFailure? deliveryFailure = this.DeliveryFailure;
				if (deliveryFailure == null)
				{
					return null;
				}
				if (System.ServiceModel.Channels.DeliveryFailure.ReachQueueTimeout == deliveryFailure.Value || deliveryFailure.Value == System.ServiceModel.Channels.DeliveryFailure.Unknown)
				{
					return new DeliveryStatus?(System.ServiceModel.Channels.DeliveryStatus.InDoubt);
				}
				return new DeliveryStatus?(System.ServiceModel.Channels.DeliveryStatus.NotDelivered);
			}
		}

		// Token: 0x17001536 RID: 5430
		// (get) Token: 0x0600571A RID: 22298 RVA: 0x0013F9EB File Offset: 0x0013DBEB
		// (set) Token: 0x0600571B RID: 22299 RVA: 0x0013F9F3 File Offset: 0x0013DBF3
		public int AbortCount
		{
			get
			{
				return this.abortCount;
			}
			internal set
			{
				this.abortCount = value;
			}
		}

		// Token: 0x17001537 RID: 5431
		// (get) Token: 0x0600571C RID: 22300 RVA: 0x0013F9FC File Offset: 0x0013DBFC
		internal long LookupId
		{
			get
			{
				return this.lookupId;
			}
		}

		// Token: 0x17001538 RID: 5432
		// (get) Token: 0x0600571D RID: 22301 RVA: 0x0013FA04 File Offset: 0x0013DC04
		internal string MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x17001539 RID: 5433
		// (get) Token: 0x0600571E RID: 22302 RVA: 0x0013FA0C File Offset: 0x0013DC0C
		// (set) Token: 0x0600571F RID: 22303 RVA: 0x0013FA14 File Offset: 0x0013DC14
		public int MoveCount
		{
			get
			{
				return this.moveCount;
			}
			internal set
			{
				this.moveCount = value;
			}
		}

		// Token: 0x06005720 RID: 22304 RVA: 0x0013FA20 File Offset: 0x0013DC20
		public static MsmqMessageProperty Get(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (message.Properties == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message.Properties");
			}
			return message.Properties["MsmqMessageProperty"] as MsmqMessageProperty;
		}

		// Token: 0x06005721 RID: 22305 RVA: 0x0013FA70 File Offset: 0x0013DC70
		private static DeliveryFailure? TryGetDeliveryFailure(string messageId, int acknowledgment)
		{
			if ((32768 & acknowledgment) == 0)
			{
				return null;
			}
			int num = 16384 & acknowledgment;
			int num2 = -49153 & acknowledgment;
			if ((num == 0 && num2 >= 0 && num2 <= 10) || (num != 0 && num2 >= 0 && num2 <= 2))
			{
				return new DeliveryFailure?((DeliveryFailure)acknowledgment);
			}
			MsmqDiagnostics.UnexpectedAcknowledgment(messageId, acknowledgment);
			return new DeliveryFailure?(System.ServiceModel.Channels.DeliveryFailure.Unknown);
		}

		// Token: 0x04003595 RID: 13717
		public const string Name = "MsmqMessageProperty";

		// Token: 0x04003596 RID: 13718
		private int abortCount;

		// Token: 0x04003597 RID: 13719
		private int moveCount;

		// Token: 0x04003598 RID: 13720
		private long lookupId;

		// Token: 0x04003599 RID: 13721
		private string messageId;

		// Token: 0x0400359A RID: 13722
		private int acknowledge;
	}
}
