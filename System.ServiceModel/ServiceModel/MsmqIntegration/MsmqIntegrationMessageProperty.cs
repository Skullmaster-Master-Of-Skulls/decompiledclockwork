using System;
using System.ComponentModel;
using System.Messaging;
using System.Runtime;
using System.ServiceModel.Channels;

namespace System.ServiceModel.MsmqIntegration
{
	// Token: 0x020003B5 RID: 949
	public sealed class MsmqIntegrationMessageProperty
	{
		// Token: 0x06002378 RID: 9080 RVA: 0x00081F78 File Offset: 0x00080178
		public static MsmqIntegrationMessageProperty Get(System.ServiceModel.Channels.Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (message.Properties == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message.Properties");
			}
			return message.Properties["MsmqIntegrationMessageProperty"] as MsmqIntegrationMessageProperty;
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06002379 RID: 9081 RVA: 0x00081FC5 File Offset: 0x000801C5
		// (set) Token: 0x0600237A RID: 9082 RVA: 0x00081FCD File Offset: 0x000801CD
		public object Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = value;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x00081FD6 File Offset: 0x000801D6
		// (set) Token: 0x0600237C RID: 9084 RVA: 0x00081FDE File Offset: 0x000801DE
		public AcknowledgeTypes? AcknowledgeType
		{
			get
			{
				return this.acknowledgeType;
			}
			set
			{
				this.acknowledgeType = value;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x00081FE7 File Offset: 0x000801E7
		// (set) Token: 0x0600237E RID: 9086 RVA: 0x00081FEF File Offset: 0x000801EF
		public Acknowledgment? Acknowledgment
		{
			get
			{
				return this.acknowledgment;
			}
			internal set
			{
				this.acknowledgment = value;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x00081FF8 File Offset: 0x000801F8
		// (set) Token: 0x06002380 RID: 9088 RVA: 0x00082000 File Offset: 0x00080200
		public Uri AdministrationQueue
		{
			get
			{
				return this.administrationQueue;
			}
			set
			{
				this.administrationQueue = value;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06002381 RID: 9089 RVA: 0x00082009 File Offset: 0x00080209
		// (set) Token: 0x06002382 RID: 9090 RVA: 0x00082011 File Offset: 0x00080211
		public int? AppSpecific
		{
			get
			{
				return this.appSpecific;
			}
			set
			{
				this.appSpecific = value;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06002383 RID: 9091 RVA: 0x0008201A File Offset: 0x0008021A
		// (set) Token: 0x06002384 RID: 9092 RVA: 0x00082022 File Offset: 0x00080222
		public DateTime? ArrivedTime
		{
			get
			{
				return this.arrivedTime;
			}
			internal set
			{
				this.arrivedTime = value;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06002385 RID: 9093 RVA: 0x0008202B File Offset: 0x0008022B
		// (set) Token: 0x06002386 RID: 9094 RVA: 0x00082033 File Offset: 0x00080233
		public bool? Authenticated
		{
			get
			{
				return this.authenticated;
			}
			internal set
			{
				this.authenticated = value;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x0008203C File Offset: 0x0008023C
		// (set) Token: 0x06002388 RID: 9096 RVA: 0x00082044 File Offset: 0x00080244
		public int? BodyType
		{
			get
			{
				return this.bodyType;
			}
			set
			{
				this.bodyType = value;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06002389 RID: 9097 RVA: 0x0008204D File Offset: 0x0008024D
		// (set) Token: 0x0600238A RID: 9098 RVA: 0x00082055 File Offset: 0x00080255
		public string CorrelationId
		{
			get
			{
				return this.correlationId;
			}
			set
			{
				this.correlationId = value;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x0008205E File Offset: 0x0008025E
		// (set) Token: 0x0600238C RID: 9100 RVA: 0x00082066 File Offset: 0x00080266
		public Uri DestinationQueue
		{
			get
			{
				return this.destinationQueue;
			}
			internal set
			{
				this.destinationQueue = value;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x0600238D RID: 9101 RVA: 0x0008206F File Offset: 0x0008026F
		// (set) Token: 0x0600238E RID: 9102 RVA: 0x00082077 File Offset: 0x00080277
		public byte[] Extension
		{
			get
			{
				return this.extension;
			}
			set
			{
				this.extension = value;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x0600238F RID: 9103 RVA: 0x00082080 File Offset: 0x00080280
		// (set) Token: 0x06002390 RID: 9104 RVA: 0x00082088 File Offset: 0x00080288
		public string Id
		{
			get
			{
				return this.id;
			}
			internal set
			{
				this.id = value;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x00082091 File Offset: 0x00080291
		// (set) Token: 0x06002392 RID: 9106 RVA: 0x00082099 File Offset: 0x00080299
		public string Label
		{
			get
			{
				return this.label;
			}
			set
			{
				this.label = value;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06002393 RID: 9107 RVA: 0x000820A2 File Offset: 0x000802A2
		// (set) Token: 0x06002394 RID: 9108 RVA: 0x000820AA File Offset: 0x000802AA
		public MessageType? MessageType
		{
			get
			{
				return this.messageType;
			}
			internal set
			{
				this.messageType = value;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06002395 RID: 9109 RVA: 0x000820B3 File Offset: 0x000802B3
		// (set) Token: 0x06002396 RID: 9110 RVA: 0x000820BB File Offset: 0x000802BB
		public MessagePriority? Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				MsmqIntegrationMessageProperty.ValidateMessagePriority(value);
				this.priority = value;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06002397 RID: 9111 RVA: 0x000820CA File Offset: 0x000802CA
		// (set) Token: 0x06002398 RID: 9112 RVA: 0x000820D2 File Offset: 0x000802D2
		public Uri ResponseQueue
		{
			get
			{
				return this.responseQueue;
			}
			set
			{
				this.responseQueue = value;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002399 RID: 9113 RVA: 0x000820DB File Offset: 0x000802DB
		// (set) Token: 0x0600239A RID: 9114 RVA: 0x000820E3 File Offset: 0x000802E3
		public byte[] SenderId
		{
			get
			{
				return this.senderId;
			}
			internal set
			{
				this.senderId = value;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x0600239B RID: 9115 RVA: 0x000820EC File Offset: 0x000802EC
		// (set) Token: 0x0600239C RID: 9116 RVA: 0x000820F4 File Offset: 0x000802F4
		public DateTime? SentTime
		{
			get
			{
				return this.sentTime;
			}
			internal set
			{
				this.sentTime = value;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x0600239D RID: 9117 RVA: 0x000820FD File Offset: 0x000802FD
		// (set) Token: 0x0600239E RID: 9118 RVA: 0x00082105 File Offset: 0x00080305
		public TimeSpan? TimeToReachQueue
		{
			get
			{
				return this.timeToReachQueue;
			}
			set
			{
				MsmqIntegrationMessageProperty.ValidateTimeToReachQueue(value);
				this.timeToReachQueue = value;
			}
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x00082114 File Offset: 0x00080314
		internal void InternalSetTimeToReachQueue(TimeSpan timeout)
		{
			this.timeToReachQueue = new TimeSpan?(timeout);
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x00082124 File Offset: 0x00080324
		private static void ValidateMessagePriority(MessagePriority? priority)
		{
			if (priority != null && (priority.Value < MessagePriority.Lowest || priority.Value > MessagePriority.Highest))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("priority", (int)priority.Value, typeof(MessagePriority)));
			}
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x00082174 File Offset: 0x00080374
		private static void ValidateTimeToReachQueue(TimeSpan? timeout)
		{
			if (timeout != null && timeout.Value < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			if (timeout != null && TimeoutHelper.IsTooLarge(timeout.Value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", timeout, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
			}
		}

		// Token: 0x04002008 RID: 8200
		public const string Name = "MsmqIntegrationMessageProperty";

		// Token: 0x04002009 RID: 8201
		private object body;

		// Token: 0x0400200A RID: 8202
		private AcknowledgeTypes? acknowledgeType;

		// Token: 0x0400200B RID: 8203
		private Acknowledgment? acknowledgment;

		// Token: 0x0400200C RID: 8204
		private Uri administrationQueue;

		// Token: 0x0400200D RID: 8205
		private int? appSpecific;

		// Token: 0x0400200E RID: 8206
		private DateTime? arrivedTime;

		// Token: 0x0400200F RID: 8207
		private bool? authenticated;

		// Token: 0x04002010 RID: 8208
		private int? bodyType;

		// Token: 0x04002011 RID: 8209
		private string correlationId;

		// Token: 0x04002012 RID: 8210
		private Uri destinationQueue;

		// Token: 0x04002013 RID: 8211
		private byte[] extension;

		// Token: 0x04002014 RID: 8212
		private string id;

		// Token: 0x04002015 RID: 8213
		private string label;

		// Token: 0x04002016 RID: 8214
		private MessageType? messageType;

		// Token: 0x04002017 RID: 8215
		private MessagePriority? priority;

		// Token: 0x04002018 RID: 8216
		private Uri responseQueue;

		// Token: 0x04002019 RID: 8217
		private byte[] senderId;

		// Token: 0x0400201A RID: 8218
		private DateTime? sentTime;

		// Token: 0x0400201B RID: 8219
		private TimeSpan? timeToReachQueue;
	}
}
