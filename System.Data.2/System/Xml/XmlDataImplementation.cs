using System;

namespace System.Xml
{
	// Token: 0x0200008C RID: 140
	internal sealed class XmlDataImplementation : XmlImplementation
	{
		// Token: 0x0600072D RID: 1837 RVA: 0x0004F3E0 File Offset: 0x0004E7E0
		public override XmlDocument CreateDocument()
		{
			return new XmlDataDocument(this);
		}
	}
}
