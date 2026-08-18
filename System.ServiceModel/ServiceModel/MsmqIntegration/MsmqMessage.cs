using System;
using System.Messaging;

namespace System.ServiceModel.MsmqIntegration
{
	// Token: 0x020003BB RID: 955
	[MessageContract(IsWrapped = false)]
	public sealed class MsmqMessage<T>
	{
		// Token: 0x060023B8 RID: 9144 RVA: 0x00082510 File Offset: 0x00080710
		public MsmqMessage(T body)
		{
			if (body == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("body");
			}
			this.property = new MsmqIntegrationMessageProperty();
			this.property.Body = body;
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x0008254C File Offset: 0x0008074C
		internal MsmqMessage()
		{
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x00082554 File Offset: 0x00080754
		// (set) Token: 0x060023BB RID: 9147 RVA: 0x00082566 File Offset: 0x00080766
		public T Body
		{
			get
			{
				return (T)((object)this.property.Body);
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.property.Body = value;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x060023BC RID: 9148 RVA: 0x00082591 File Offset: 0x00080791
		// (set) Token: 0x060023BD RID: 9149 RVA: 0x0008259E File Offset: 0x0008079E
		public AcknowledgeTypes? AcknowledgeType
		{
			get
			{
				return this.property.AcknowledgeType;
			}
			set
			{
				this.property.AcknowledgeType = value;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x060023BE RID: 9150 RVA: 0x000825AC File Offset: 0x000807AC
		public Acknowledgment? Acknowledgment
		{
			get
			{
				return this.property.Acknowledgment;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x060023BF RID: 9151 RVA: 0x000825B9 File Offset: 0x000807B9
		// (set) Token: 0x060023C0 RID: 9152 RVA: 0x000825C6 File Offset: 0x000807C6
		public Uri AdministrationQueue
		{
			get
			{
				return this.property.AdministrationQueue;
			}
			set
			{
				this.property.AdministrationQueue = value;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x060023C1 RID: 9153 RVA: 0x000825D4 File Offset: 0x000807D4
		// (set) Token: 0x060023C2 RID: 9154 RVA: 0x000825E1 File Offset: 0x000807E1
		public int? AppSpecific
		{
			get
			{
				return this.property.AppSpecific;
			}
			set
			{
				this.property.AppSpecific = value;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x060023C3 RID: 9155 RVA: 0x000825EF File Offset: 0x000807EF
		public DateTime? ArrivedTime
		{
			get
			{
				return this.property.ArrivedTime;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x060023C4 RID: 9156 RVA: 0x000825FC File Offset: 0x000807FC
		public bool? Authenticated
		{
			get
			{
				return this.property.Authenticated;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x060023C5 RID: 9157 RVA: 0x00082609 File Offset: 0x00080809
		// (set) Token: 0x060023C6 RID: 9158 RVA: 0x00082616 File Offset: 0x00080816
		public int? BodyType
		{
			get
			{
				return this.property.BodyType;
			}
			set
			{
				this.property.BodyType = value;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x060023C7 RID: 9159 RVA: 0x00082624 File Offset: 0x00080824
		// (set) Token: 0x060023C8 RID: 9160 RVA: 0x00082631 File Offset: 0x00080831
		public string CorrelationId
		{
			get
			{
				return this.property.CorrelationId;
			}
			set
			{
				this.property.CorrelationId = value;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x060023C9 RID: 9161 RVA: 0x0008263F File Offset: 0x0008083F
		public Uri DestinationQueue
		{
			get
			{
				return this.property.DestinationQueue;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x060023CA RID: 9162 RVA: 0x0008264C File Offset: 0x0008084C
		// (set) Token: 0x060023CB RID: 9163 RVA: 0x00082659 File Offset: 0x00080859
		public byte[] Extension
		{
			get
			{
				return this.property.Extension;
			}
			set
			{
				this.property.Extension = value;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x060023CC RID: 9164 RVA: 0x00082667 File Offset: 0x00080867
		public string Id
		{
			get
			{
				return this.property.Id;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x060023CD RID: 9165 RVA: 0x00082674 File Offset: 0x00080874
		// (set) Token: 0x060023CE RID: 9166 RVA: 0x00082681 File Offset: 0x00080881
		public string Label
		{
			get
			{
				return this.property.Label;
			}
			set
			{
				this.property.Label = value;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x060023CF RID: 9167 RVA: 0x0008268F File Offset: 0x0008088F
		public MessageType? MessageType
		{
			get
			{
				return this.property.MessageType;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x060023D0 RID: 9168 RVA: 0x0008269C File Offset: 0x0008089C
		// (set) Token: 0x060023D1 RID: 9169 RVA: 0x000826A9 File Offset: 0x000808A9
		public MessagePriority? Priority
		{
			get
			{
				return this.property.Priority;
			}
			set
			{
				this.property.Priority = value;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x060023D2 RID: 9170 RVA: 0x000826B7 File Offset: 0x000808B7
		// (set) Token: 0x060023D3 RID: 9171 RVA: 0x000826C4 File Offset: 0x000808C4
		public Uri ResponseQueue
		{
			get
			{
				return this.property.ResponseQueue;
			}
			set
			{
				this.property.ResponseQueue = value;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x060023D4 RID: 9172 RVA: 0x000826D2 File Offset: 0x000808D2
		public byte[] SenderId
		{
			get
			{
				return this.property.SenderId;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x060023D5 RID: 9173 RVA: 0x000826DF File Offset: 0x000808DF
		public DateTime? SentTime
		{
			get
			{
				return this.property.SentTime;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x060023D6 RID: 9174 RVA: 0x000826EC File Offset: 0x000808EC
		// (set) Token: 0x060023D7 RID: 9175 RVA: 0x000826F9 File Offset: 0x000808F9
		public TimeSpan? TimeToReachQueue
		{
			get
			{
				return this.property.TimeToReachQueue;
			}
			set
			{
				this.property.TimeToReachQueue = value;
			}
		}

		// Token: 0x04002025 RID: 8229
		[MessageProperty(Name = "MsmqIntegrationMessageProperty")]
		private MsmqIntegrationMessageProperty property;
	}
}
