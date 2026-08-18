using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x02000122 RID: 290
	internal class XmlNodeListEnumerator : IEnumerator
	{
		// Token: 0x06001466 RID: 5222 RVA: 0x00054136 File Offset: 0x00052336
		public XmlNodeListEnumerator(XPathNodeList list)
		{
			this.list = list;
			this.index = -1;
			this.valid = false;
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00054153 File Offset: 0x00052353
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0005415C File Offset: 0x0005235C
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

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x000541B8 File Offset: 0x000523B8
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

		// Token: 0x0400058D RID: 1421
		private XPathNodeList list;

		// Token: 0x0400058E RID: 1422
		private int index;

		// Token: 0x0400058F RID: 1423
		private bool valid;
	}
}
