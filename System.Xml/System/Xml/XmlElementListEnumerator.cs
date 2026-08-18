using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x020000DC RID: 220
	internal class XmlElementListEnumerator : IEnumerator
	{
		// Token: 0x06000D8F RID: 3471 RVA: 0x0003C1C5 File Offset: 0x0003B1C5
		public XmlElementListEnumerator(XmlElementList list)
		{
			this.list = list;
			this.curElem = null;
			this.changeCount = list.ChangeCount;
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0003C1E8 File Offset: 0x0003B1E8
		public bool MoveNext()
		{
			if (this.list.ChangeCount != this.changeCount)
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Enum_ElementList"));
			}
			this.curElem = this.list.GetNextNode(this.curElem);
			return this.curElem != null;
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0003C23B File Offset: 0x0003B23B
		public void Reset()
		{
			this.curElem = null;
			this.changeCount = this.list.ChangeCount;
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x0003C255 File Offset: 0x0003B255
		public object Current
		{
			get
			{
				return this.curElem;
			}
		}

		// Token: 0x04000955 RID: 2389
		private XmlElementList list;

		// Token: 0x04000956 RID: 2390
		private XmlNode curElem;

		// Token: 0x04000957 RID: 2391
		private int changeCount;
	}
}
