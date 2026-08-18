using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x020000DD RID: 221
	internal class XmlEmptyElementListEnumerator : IEnumerator
	{
		// Token: 0x06000D93 RID: 3475 RVA: 0x0003C25D File Offset: 0x0003B25D
		public XmlEmptyElementListEnumerator(XmlElementList list)
		{
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0003C265 File Offset: 0x0003B265
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0003C268 File Offset: 0x0003B268
		public void Reset()
		{
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0003C26A File Offset: 0x0003B26A
		public object Current
		{
			get
			{
				return null;
			}
		}
	}
}
