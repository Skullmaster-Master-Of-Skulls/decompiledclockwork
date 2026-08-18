using System;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009BA RID: 2490
	internal class XmlObjectSerializerBodyWriter : BodyWriter
	{
		// Token: 0x060061DA RID: 25050 RVA: 0x0016C68B File Offset: 0x0016A88B
		public XmlObjectSerializerBodyWriter(object body, XmlObjectSerializer serializer) : base(true)
		{
			this.body = body;
			this.serializer = serializer;
		}

		// Token: 0x17001790 RID: 6032
		// (get) Token: 0x060061DB RID: 25051 RVA: 0x0016C6A2 File Offset: 0x0016A8A2
		private object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060061DC RID: 25052 RVA: 0x0016C6A8 File Offset: 0x0016A8A8
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				this.serializer.WriteObject(writer, this.body);
			}
		}

		// Token: 0x040038DD RID: 14557
		private object body;

		// Token: 0x040038DE RID: 14558
		private XmlObjectSerializer serializer;
	}
}
