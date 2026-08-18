using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x0200010A RID: 266
	internal class XmlEmptyElementListEnumerator : IEnumerator
	{
		// Token: 0x060012C7 RID: 4807 RVA: 0x0004E072 File Offset: 0x0004C272
		public XmlEmptyElementListEnumerator(XmlElementList list)
		{
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0004E07A File Offset: 0x0004C27A
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0004E07D File Offset: 0x0004C27D
		public void Reset()
		{
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x0004E07F File Offset: 0x0004C27F
		public object Current
		{
			get
			{
				return null;
			}
		}
	}
}
