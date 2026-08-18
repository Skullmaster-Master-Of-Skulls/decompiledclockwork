using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200015E RID: 350
	internal sealed class SortKey
	{
		// Token: 0x060012FF RID: 4863 RVA: 0x000526C2 File Offset: 0x000516C2
		public SortKey(int numKeys, int originalPosition, XPathNavigator node)
		{
			this.numKeys = numKeys;
			this.keys = new object[numKeys];
			this.originalPosition = originalPosition;
			this.node = node;
		}

		// Token: 0x1700049D RID: 1181
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

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x00052700 File Offset: 0x00051700
		public int NumKeys
		{
			get
			{
				return this.numKeys;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x00052708 File Offset: 0x00051708
		public int OriginalPosition
		{
			get
			{
				return this.originalPosition;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x00052710 File Offset: 0x00051710
		public XPathNavigator Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x04000BD7 RID: 3031
		private int numKeys;

		// Token: 0x04000BD8 RID: 3032
		private object[] keys;

		// Token: 0x04000BD9 RID: 3033
		private int originalPosition;

		// Token: 0x04000BDA RID: 3034
		private XPathNavigator node;
	}
}
