using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x020000F3 RID: 243
	internal class XmlNodeListEnumerator : IEnumerator
	{
		// Token: 0x06000ED1 RID: 3793 RVA: 0x00040D4E File Offset: 0x0003FD4E
		public XmlNodeListEnumerator(XPathNodeList list)
		{
			this.list = list;
			this.index = -1;
			this.valid = false;
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00040D6B File Offset: 0x0003FD6B
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00040D74 File Offset: 0x0003FD74
		public bool MoveNext()
		{
			this.index++;
			int num = this.list.ReadUntil(this.index + 1);
			if (num - 1 < this.index)
			{
				return false;
			}
			this.valid = (this.list[this.index] != null);
			return this.valid;
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x00040DD3 File Offset: 0x0003FDD3
		public object Current
		{
			get
			{
				if (this.valid)
				{
					return this.list[this.index];
				}
				return null;
			}
		}

		// Token: 0x040009AD RID: 2477
		private XPathNodeList list;

		// Token: 0x040009AE RID: 2478
		private int index;

		// Token: 0x040009AF RID: 2479
		private bool valid;
	}
}
