using System;

namespace System.Xml
{
	// Token: 0x0200038D RID: 909
	internal sealed class XmlDataImplementation : XmlImplementation
	{
		// Token: 0x0600308D RID: 12429 RVA: 0x002DAE58 File Offset: 0x002DA258
		public override XmlDocument CreateDocument()
		{
			return new XmlDataDocument(this);
		}
	}
}
