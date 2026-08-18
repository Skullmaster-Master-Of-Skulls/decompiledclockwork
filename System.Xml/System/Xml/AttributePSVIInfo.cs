using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000AE RID: 174
	internal class AttributePSVIInfo
	{
		// Token: 0x06000986 RID: 2438 RVA: 0x0002C5A4 File Offset: 0x0002B5A4
		internal AttributePSVIInfo()
		{
			this.attributeSchemaInfo = new XmlSchemaInfo();
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0002C5B7 File Offset: 0x0002B5B7
		internal void Reset()
		{
			this.typedAttributeValue = null;
			this.localName = string.Empty;
			this.namespaceUri = string.Empty;
			this.attributeSchemaInfo.Clear();
		}

		// Token: 0x04000850 RID: 2128
		internal string localName;

		// Token: 0x04000851 RID: 2129
		internal string namespaceUri;

		// Token: 0x04000852 RID: 2130
		internal object typedAttributeValue;

		// Token: 0x04000853 RID: 2131
		internal XmlSchemaInfo attributeSchemaInfo;
	}
}
