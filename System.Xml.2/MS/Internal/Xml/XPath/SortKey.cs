using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200003A RID: 58
	internal sealed class SortKey
	{
		// Token: 0x060001BF RID: 447 RVA: 0x00007112 File Offset: 0x00005312
		public SortKey(int numKeys, int originalPosition, XPathNavigator node)
		{
			this.numKeys = numKeys;
			this.keys = new object[numKeys];
			this.originalPosition = originalPosition;
			this.node = node;
		}

		// Token: 0x17000066 RID: 102
		public object this[int index]
		{
			get
			{
				return this.keys[index];
			}
			set
			{
				this.keys[index] = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00007150 File Offset: 0x00005350
		public int NumKeys
		{
			get
			{
				return this.numKeys;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00007158 File Offset: 0x00005358
		public int OriginalPosition
		{
			get
			{
				return this.originalPosition;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00007160 File Offset: 0x00005360
		public XPathNavigator Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x040000C6 RID: 198
		private int numKeys;

		// Token: 0x040000C7 RID: 199
		private object[] keys;

		// Token: 0x040000C8 RID: 200
		private int originalPosition;

		// Token: 0x040000C9 RID: 201
		private XPathNavigator node;
	}
}
