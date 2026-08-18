using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200085C RID: 2140
	internal class StringMessage : ContentOnlyMessage
	{
		// Token: 0x0600502F RID: 20527 RVA: 0x001261BD File Offset: 0x001243BD
		public StringMessage(string data)
		{
			this.data = data;
		}

		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x06005030 RID: 20528 RVA: 0x001261CC File Offset: 0x001243CC
		public override bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this.data);
			}
		}

		// Token: 0x06005031 RID: 20529 RVA: 0x001261D9 File Offset: 0x001243D9
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			if (this.data != null && this.data.Length > 0)
			{
				writer.WriteElementString("BODY", this.data);
			}
		}

		// Token: 0x040031A5 RID: 12709
		private string data;
	}
}
