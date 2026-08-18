using System;
using System.Data;

namespace System.Xml
{
	// Token: 0x02000085 RID: 133
	internal interface IXmlDataVirtualNode
	{
		// Token: 0x06000673 RID: 1651
		bool IsOnNode(XmlNode nodeToCheck);

		// Token: 0x06000674 RID: 1652
		bool IsOnColumn(DataColumn col);

		// Token: 0x06000675 RID: 1653
		bool IsInUse();

		// Token: 0x06000676 RID: 1654
		void OnFoliated(XmlNode foliatedNode);
	}
}
