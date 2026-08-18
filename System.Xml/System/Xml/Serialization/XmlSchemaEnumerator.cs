using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000322 RID: 802
	public class XmlSchemaEnumerator : IEnumerator<XmlSchema>, IDisposable, IEnumerator
	{
		// Token: 0x06002653 RID: 9811 RVA: 0x000BAE77 File Offset: 0x000B9E77
		public XmlSchemaEnumerator(XmlSchemas list)
		{
			this.list = list;
			this.idx = -1;
			this.end = list.Count - 1;
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x000BAE9B File Offset: 0x000B9E9B
		public void Dispose()
		{
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x000BAE9D File Offset: 0x000B9E9D
		public bool MoveNext()
		{
			if (this.idx >= this.end)
			{
				return false;
			}
			this.idx++;
			return true;
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06002656 RID: 9814 RVA: 0x000BAEBE File Offset: 0x000B9EBE
		public XmlSchema Current
		{
			get
			{
				return this.list[this.idx];
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002657 RID: 9815 RVA: 0x000BAED1 File Offset: 0x000B9ED1
		object IEnumerator.Current
		{
			get
			{
				return this.list[this.idx];
			}
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x000BAEE4 File Offset: 0x000B9EE4
		void IEnumerator.Reset()
		{
			this.idx = -1;
		}

		// Token: 0x040015CE RID: 5582
		private XmlSchemas list;

		// Token: 0x040015CF RID: 5583
		private int idx;

		// Token: 0x040015D0 RID: 5584
		private int end;
	}
}
