using System;
using System.Data;

namespace System.Xml
{
	// Token: 0x02000384 RID: 900
	internal interface IXmlDataVirtualNode
	{
		// Token: 0x06002F98 RID: 12184
		bool IsOnNode(XmlNode nodeToCheck);

		// Token: 0x06002F99 RID: 12185
		bool IsOnColumn(DataColumn col);

		// Token: 0x06002F9A RID: 12186
		bool IsInUse();

		// Token: 0x06002F9B RID: 12187
		void OnFoliated(XmlNode foliatedNode);
	}
}
