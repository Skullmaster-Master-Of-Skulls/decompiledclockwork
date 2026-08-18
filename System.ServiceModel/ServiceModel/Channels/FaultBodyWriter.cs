using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009B9 RID: 2489
	internal class FaultBodyWriter : BodyWriter
	{
		// Token: 0x060061D7 RID: 25047 RVA: 0x0016C65D File Offset: 0x0016A85D
		public FaultBodyWriter(MessageFault fault, EnvelopeVersion version) : base(true)
		{
			this.fault = fault;
			this.version = version;
		}

		// Token: 0x1700178F RID: 6031
		// (get) Token: 0x060061D8 RID: 25048 RVA: 0x0016C674 File Offset: 0x0016A874
		internal override bool IsFault
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060061D9 RID: 25049 RVA: 0x0016C677 File Offset: 0x0016A877
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			this.fault.WriteTo(writer, this.version);
		}

		// Token: 0x040038DB RID: 14555
		private MessageFault fault;

		// Token: 0x040038DC RID: 14556
		private EnvelopeVersion version;
	}
}
