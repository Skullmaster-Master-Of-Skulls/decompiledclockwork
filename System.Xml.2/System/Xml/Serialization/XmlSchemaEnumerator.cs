using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020001A6 RID: 422
	public class XmlSchemaEnumerator : IEnumerator<XmlSchema>, IDisposable, IEnumerator
	{
		// Token: 0x06001C1F RID: 7199 RVA: 0x00083B27 File Offset: 0x00081D27
		public XmlSchemaEnumerator(XmlSchemas list)
		{
			this.list = list;
			this.idx = -1;
			this.end = list.Count - 1;
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x00083B4B File Offset: 0x00081D4B
		public void Dispose()
		{
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x00083B4D File Offset: 0x00081D4D
		public bool MoveNext()
		{
			if (this.idx >= this.end)
			{
				return false;
			}
			this.idx++;
			return true;
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x00083B6E File Offset: 0x00081D6E
		public XmlSchema Current
		{
			get
			{
				return this.list[this.idx];
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x00083B81 File Offset: 0x00081D81
		object IEnumerator.Current
		{
			get
			{
				return this.list[this.idx];
			}
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x00083B94 File Offset: 0x00081D94
		void IEnumerator.Reset()
		{
			this.idx = -1;
		}

		// Token: 0x04000C3A RID: 3130
		private XmlSchemas list;

		// Token: 0x04000C3B RID: 3131
		private int idx;

		// Token: 0x04000C3C RID: 3132
		private int end;
	}
}
