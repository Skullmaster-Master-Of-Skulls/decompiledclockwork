using System;
using System.Messaging;
using System.ServiceModel.Channels;

namespace System.ServiceModel.MsmqIntegration
{
	// Token: 0x020003B2 RID: 946
	internal class MsmqIntegrationInputMessage : MsmqInputMessage
	{
		// Token: 0x06002360 RID: 9056 RVA: 0x000817C8 File Offset: 0x0007F9C8
		public MsmqIntegrationInputMessage() : this(4194304)
		{
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x000817D5 File Offset: 0x0007F9D5
		public MsmqIntegrationInputMessage(int maxBufferSize) : this(new MsmqInputMessage.SizeQuota(maxBufferSize))
		{
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x000817E4 File Offset: 0x0007F9E4
		protected MsmqIntegrationInputMessage(MsmqInputMessage.SizeQuota bufferSizeQuota) : base(22, bufferSizeQuota)
		{
			this.acknowledge = new NativeMsmqMessage.ByteProperty(this, 6);
			this.adminQueue = new NativeMsmqMessage.StringProperty(this, 17, 256);
			this.adminQueueLength = new NativeMsmqMessage.IntProperty(this, 18, 256);
			this.appSpecific = new NativeMsmqMessage.IntProperty(this, 8);
			this.arrivedTime = new NativeMsmqMessage.IntProperty(this, 32);
			this.senderIdType = new NativeMsmqMessage.IntProperty(this, 22);
			this.authenticated = new NativeMsmqMessage.ByteProperty(this, 25);
			this.bodyType = new NativeMsmqMessage.IntProperty(this, 42);
			this.correlationId = new NativeMsmqMessage.BufferProperty(this, 3, 20);
			this.destinationQueue = new NativeMsmqMessage.StringProperty(this, 58, 256);
			this.destinationQueueLength = new NativeMsmqMessage.IntProperty(this, 59, 256);
			this.extension = new NativeMsmqMessage.BufferProperty(this, 35, bufferSizeQuota.AllocIfAvailable(0));
			this.extensionLength = new NativeMsmqMessage.IntProperty(this, 36, 0);
			this.label = new NativeMsmqMessage.StringProperty(this, 11, 128);
			this.labelLength = new NativeMsmqMessage.IntProperty(this, 12, 128);
			this.priority = new NativeMsmqMessage.ByteProperty(this, 4);
			this.responseFormatName = new NativeMsmqMessage.StringProperty(this, 54, 256);
			this.responseFormatNameLength = new NativeMsmqMessage.IntProperty(this, 55, 256);
			this.sentTime = new NativeMsmqMessage.IntProperty(this, 31);
			this.timeToReachQueue = new NativeMsmqMessage.IntProperty(this, 13);
			this.privacyLevel = new NativeMsmqMessage.IntProperty(this, 23);
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x00081950 File Offset: 0x0007FB50
		protected override void OnGrowBuffers(MsmqInputMessage.SizeQuota bufferSizeQuota)
		{
			base.OnGrowBuffers(bufferSizeQuota);
			this.adminQueue.EnsureValueLength(this.adminQueueLength.Value);
			this.responseFormatName.EnsureValueLength(this.responseFormatNameLength.Value);
			this.destinationQueue.EnsureValueLength(this.destinationQueueLength.Value);
			this.label.EnsureValueLength(this.labelLength.Value);
			bufferSizeQuota.Alloc(this.extensionLength.Value);
			this.extension.EnsureBufferLength(this.extensionLength.Value);
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000819E4 File Offset: 0x0007FBE4
		public void SetMessageProperties(MsmqIntegrationMessageProperty property)
		{
			property.AcknowledgeType = new AcknowledgeTypes?((AcknowledgeTypes)this.acknowledge.Value);
			property.Acknowledgment = new Acknowledgment?((Acknowledgment)base.Class.Value);
			property.AdministrationQueue = MsmqIntegrationInputMessage.GetQueueName(this.adminQueue.GetValue(this.adminQueueLength.Value));
			property.AppSpecific = new int?(this.appSpecific.Value);
			property.ArrivedTime = new DateTime?(MsmqDateTime.ToDateTime(this.arrivedTime.Value).ToLocalTime());
			property.Authenticated = new bool?(this.authenticated.Value > 0);
			property.BodyType = new int?(this.bodyType.Value);
			property.CorrelationId = MsmqMessageId.ToString(this.correlationId.Buffer);
			property.DestinationQueue = MsmqIntegrationInputMessage.GetQueueName(this.destinationQueue.GetValue(this.destinationQueueLength.Value));
			property.Extension = this.extension.GetBufferCopy(this.extensionLength.Value);
			property.Id = MsmqMessageId.ToString(base.MessageId.Buffer);
			property.Label = this.label.GetValue(this.labelLength.Value);
			if (base.Class.Value == 0)
			{
				property.MessageType = new MessageType?(MessageType.Normal);
			}
			else if (base.Class.Value == 1)
			{
				property.MessageType = new MessageType?(MessageType.Report);
			}
			else
			{
				property.MessageType = new MessageType?(MessageType.Acknowledgment);
			}
			property.Priority = new MessagePriority?((MessagePriority)this.priority.Value);
			property.ResponseQueue = MsmqIntegrationInputMessage.GetQueueName(this.responseFormatName.GetValue(this.responseFormatNameLength.Value));
			property.SenderId = base.SenderId.GetBufferCopy(base.SenderIdLength.Value);
			property.SentTime = new DateTime?(MsmqDateTime.ToDateTime(this.sentTime.Value).ToLocalTime());
			property.InternalSetTimeToReachQueue(MsmqDuration.ToTimeSpan(this.timeToReachQueue.Value));
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x00081BFA File Offset: 0x0007FDFA
		private static Uri GetQueueName(string formatName)
		{
			if (string.IsNullOrEmpty(formatName))
			{
				return null;
			}
			return new Uri("msmq.formatname:" + formatName);
		}

		// Token: 0x04001FEA RID: 8170
		private NativeMsmqMessage.ByteProperty acknowledge;

		// Token: 0x04001FEB RID: 8171
		private NativeMsmqMessage.StringProperty adminQueue;

		// Token: 0x04001FEC RID: 8172
		private NativeMsmqMessage.IntProperty adminQueueLength;

		// Token: 0x04001FED RID: 8173
		private NativeMsmqMessage.IntProperty appSpecific;

		// Token: 0x04001FEE RID: 8174
		private NativeMsmqMessage.IntProperty arrivedTime;

		// Token: 0x04001FEF RID: 8175
		private NativeMsmqMessage.IntProperty senderIdType;

		// Token: 0x04001FF0 RID: 8176
		private NativeMsmqMessage.ByteProperty authenticated;

		// Token: 0x04001FF1 RID: 8177
		private NativeMsmqMessage.IntProperty bodyType;

		// Token: 0x04001FF2 RID: 8178
		private NativeMsmqMessage.BufferProperty correlationId;

		// Token: 0x04001FF3 RID: 8179
		private NativeMsmqMessage.StringProperty destinationQueue;

		// Token: 0x04001FF4 RID: 8180
		private NativeMsmqMessage.IntProperty destinationQueueLength;

		// Token: 0x04001FF5 RID: 8181
		private NativeMsmqMessage.BufferProperty extension;

		// Token: 0x04001FF6 RID: 8182
		private NativeMsmqMessage.IntProperty extensionLength;

		// Token: 0x04001FF7 RID: 8183
		private NativeMsmqMessage.StringProperty label;

		// Token: 0x04001FF8 RID: 8184
		private NativeMsmqMessage.IntProperty labelLength;

		// Token: 0x04001FF9 RID: 8185
		private NativeMsmqMessage.ByteProperty priority;

		// Token: 0x04001FFA RID: 8186
		private NativeMsmqMessage.StringProperty responseFormatName;

		// Token: 0x04001FFB RID: 8187
		private NativeMsmqMessage.IntProperty responseFormatNameLength;

		// Token: 0x04001FFC RID: 8188
		private NativeMsmqMessage.IntProperty sentTime;

		// Token: 0x04001FFD RID: 8189
		private NativeMsmqMessage.IntProperty timeToReachQueue;

		// Token: 0x04001FFE RID: 8190
		private NativeMsmqMessage.IntProperty privacyLevel;

		// Token: 0x04001FFF RID: 8191
		private const int initialQueueNameLength = 256;

		// Token: 0x04002000 RID: 8192
		private const int initialExtensionLength = 0;

		// Token: 0x04002001 RID: 8193
		private const int initialLabelLength = 128;

		// Token: 0x04002002 RID: 8194
		private const int maxSize = 4194304;
	}
}
