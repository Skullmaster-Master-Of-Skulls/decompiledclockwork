using System;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009CA RID: 2506
	internal class XmlObjectSerializerFault : MessageFault
	{
		// Token: 0x0600627D RID: 25213 RVA: 0x0016EC2B File Offset: 0x0016CE2B
		public XmlObjectSerializerFault(FaultCode code, FaultReason reason, object detail, XmlObjectSerializer serializer, string actor, string node)
		{
			this.code = code;
			this.reason = reason;
			this.detail = detail;
			this.serializer = serializer;
			this.actor = actor;
			this.node = node;
		}

		// Token: 0x170017BF RID: 6079
		// (get) Token: 0x0600627E RID: 25214 RVA: 0x0016EC60 File Offset: 0x0016CE60
		public override string Actor
		{
			get
			{
				return this.actor;
			}
		}

		// Token: 0x170017C0 RID: 6080
		// (get) Token: 0x0600627F RID: 25215 RVA: 0x0016EC68 File Offset: 0x0016CE68
		public override FaultCode Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x170017C1 RID: 6081
		// (get) Token: 0x06006280 RID: 25216 RVA: 0x0016EC70 File Offset: 0x0016CE70
		public override bool HasDetail
		{
			get
			{
				return this.serializer != null;
			}
		}

		// Token: 0x170017C2 RID: 6082
		// (get) Token: 0x06006281 RID: 25217 RVA: 0x0016EC7B File Offset: 0x0016CE7B
		public override string Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x170017C3 RID: 6083
		// (get) Token: 0x06006282 RID: 25218 RVA: 0x0016EC83 File Offset: 0x0016CE83
		public override FaultReason Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x170017C4 RID: 6084
		// (get) Token: 0x06006283 RID: 25219 RVA: 0x0016EC8B File Offset: 0x0016CE8B
		private object ThisLock
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x06006284 RID: 25220 RVA: 0x0016EC94 File Offset: 0x0016CE94
		protected override void OnWriteDetailContents(XmlDictionaryWriter writer)
		{
			if (this.serializer != null)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.serializer.WriteObject(writer, this.detail);
				}
			}
		}

		// Token: 0x0400391A RID: 14618
		private FaultCode code;

		// Token: 0x0400391B RID: 14619
		private FaultReason reason;

		// Token: 0x0400391C RID: 14620
		private string actor;

		// Token: 0x0400391D RID: 14621
		private string node;

		// Token: 0x0400391E RID: 14622
		private object detail;

		// Token: 0x0400391F RID: 14623
		private XmlObjectSerializer serializer;
	}
}
