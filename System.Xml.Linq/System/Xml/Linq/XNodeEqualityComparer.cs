using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Linq
{
	// Token: 0x02000019 RID: 25
	[__DynamicallyInvokable]
	public sealed class XNodeEqualityComparer : IEqualityComparer, IEqualityComparer<XNode>
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00004E43 File Offset: 0x00003043
		[__DynamicallyInvokable]
		public bool Equals(XNode x, XNode y)
		{
			return XNode.DeepEquals(x, y);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004E4C File Offset: 0x0000304C
		[__DynamicallyInvokable]
		public int GetHashCode(XNode obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetDeepHashCode();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004E5C File Offset: 0x0000305C
		[__DynamicallyInvokable]
		bool IEqualityComparer.Equals(object x, object y)
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
			return this.Equals(xnode, xnode2);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004EDC File Offset: 0x000030DC
		[__DynamicallyInvokable]
		int IEqualityComparer.GetHashCode(object obj)
		{
			XNode xnode = obj as XNode;
			if (xnode == null && obj != null)
			{
				throw new ArgumentException(Res.GetString("Argument_MustBeDerivedFrom", new object[]
				{
					typeof(XNode)
				}), "obj");
			}
			return this.GetHashCode(xnode);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004F25 File Offset: 0x00003125
		[__DynamicallyInvokable]
		public XNodeEqualityComparer()
		{
		}
	}
}
