using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Linq
{
	// Token: 0x02000018 RID: 24
	[__DynamicallyInvokable]
	public sealed class XNodeDocumentOrderComparer : IComparer, IComparer<XNode>
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00004DB0 File Offset: 0x00002FB0
		[__DynamicallyInvokable]
		public int Compare(XNode x, XNode y)
		{
			return XNode.CompareDocumentOrder(x, y);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004DBC File Offset: 0x00002FBC
		[__DynamicallyInvokable]
		int IComparer.Compare(object x, object y)
		{
			XNode xnode = x as XNode;
			if (xnode == null && x != null)
			{
				throw new ArgumentException(Res.GetString("Argument_MustBeDerivedFrom", new object[]
				{
					typeof(XNode)
				}), "x");
			}
			XNode xnode2 = y as XNode;
			if (xnode2 == null && y != null)
			{
				throw new ArgumentException(Res.GetString("Argument_MustBeDerivedFrom", new object[]
				{
					typeof(XNode)
				}), "y");
			}
			return this.Compare(xnode, xnode2);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004E3B File Offset: 0x0000303B
		[__DynamicallyInvokable]
		public XNodeDocumentOrderComparer()
		{
		}
	}
}
