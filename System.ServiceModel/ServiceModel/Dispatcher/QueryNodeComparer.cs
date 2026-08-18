using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004C8 RID: 1224
	internal class QueryNodeComparer : IComparer<QueryNode>
	{
		// Token: 0x06002E5C RID: 11868 RVA: 0x000B45F0 File Offset: 0x000B27F0
		public int Compare(QueryNode item1, QueryNode item2)
		{
			switch (item1.Node.ComparePosition(item1.Position, item2.Position))
			{
			case XmlNodeOrder.Before:
				return -1;
			case XmlNodeOrder.After:
				return 1;
			case XmlNodeOrder.Same:
				return 0;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new XPathException(SR.GetString("QueryNotSortable")));
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x000B4657 File Offset: 0x000B2857
		public bool Equals(QueryNode item1, QueryNode item2)
		{
			return this.Compare(item1, item2) == 0;
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x000B4664 File Offset: 0x000B2864
		public int GetHashCode(QueryNode item)
		{
			return item.GetHashCode();
		}
	}
}
