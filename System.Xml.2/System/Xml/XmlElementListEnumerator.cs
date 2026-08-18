using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x02000109 RID: 265
	internal class XmlElementListEnumerator : IEnumerator
	{
		// Token: 0x060012C3 RID: 4803 RVA: 0x0004DFDE File Offset: 0x0004C1DE
		public XmlElementListEnumerator(XmlElementList list)
		{
			this.list = list;
			this.curElem = null;
			this.changeCount = list.ChangeCount;
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x0004E000 File Offset: 0x0004C200
		public bool MoveNext()
		{
			if (this.list.ChangeCount != this.changeCount)
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Enum_ElementList"));
			}
			this.curElem = this.list.GetNextNode(this.curElem);
			return this.curElem != null;
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0004E050 File Offset: 0x0004C250
		public void Reset()
		{
			this.curElem = null;
			this.changeCount = this.list.ChangeCount;
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x0004E06A File Offset: 0x0004C26A
		public object Current
		{
			get
			{
				return this.curElem;
			}
		}

		// Token: 0x04000535 RID: 1333
		private XmlElementList list;

		// Token: 0x04000536 RID: 1334
		private XmlNode curElem;

		// Token: 0x04000537 RID: 1335
		private int changeCount;
	}
}
