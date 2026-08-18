using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000EB RID: 235
	internal class AttributePSVIInfo
	{
		// Token: 0x06000FB1 RID: 4017 RVA: 0x000416FC File Offset: 0x0003F8FC
		internal AttributePSVIInfo()
		{
			this.attributeSchemaInfo = new XmlSchemaInfo();
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0004170F File Offset: 0x0003F90F
		internal void Reset()
		{
			this.typedAttributeValue = null;
			this.localName = string.Empty;
			this.namespaceUri = string.Empty;
			this.attributeSchemaInfo.Clear();
		}

		// Token: 0x0400046F RID: 1135
		internal string localName;

		// Token: 0x04000470 RID: 1136
		internal string namespaceUri;

		// Token: 0x04000471 RID: 1137
		internal object typedAttributeValue;

		// Token: 0x04000472 RID: 1138
		internal XmlSchemaInfo attributeSchemaInfo;
	}
}
