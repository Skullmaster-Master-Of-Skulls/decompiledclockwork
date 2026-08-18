using System;
using System.Collections.Generic;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007B2 RID: 1970
	internal class ContextAddressHeader : AddressHeader
	{
		// Token: 0x06004A83 RID: 19075 RVA: 0x00111EE9 File Offset: 0x001100E9
		public ContextAddressHeader(IDictionary<string, string> context)
		{
			this.context = new ContextDictionary(context);
		}

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x06004A84 RID: 19076 RVA: 0x00111EFD File Offset: 0x001100FD
		public override string Name
		{
			get
			{
				return "Context";
			}
		}

		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x06004A85 RID: 19077 RVA: 0x00111F04 File Offset: 0x00110104
		public override string Namespace
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/context";
			}
		}

		// Token: 0x06004A86 RID: 19078 RVA: 0x00111F0B File Offset: 0x0011010B
		protected override void OnWriteAddressHeaderContents(XmlDictionaryWriter writer)
		{
			ContextMessageHeader.WriteHeaderContents(writer, this.context);
		}

		// Token: 0x04002F1B RID: 12059
		private ContextDictionary context;
	}
}
