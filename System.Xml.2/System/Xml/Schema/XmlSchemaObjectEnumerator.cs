using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020002A4 RID: 676
	public class XmlSchemaObjectEnumerator : IEnumerator
	{
		// Token: 0x06002763 RID: 10083 RVA: 0x000CF87E File Offset: 0x000CDA7E
		internal XmlSchemaObjectEnumerator(IEnumerator enumerator)
		{
			this.enumerator = enumerator;
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x000CF88D File Offset: 0x000CDA8D
		public void Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x000CF89A File Offset: 0x000CDA9A
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002766 RID: 10086 RVA: 0x000CF8A7 File Offset: 0x000CDAA7
		public XmlSchemaObject Current
		{
			get
			{
				return (XmlSchemaObject)this.enumerator.Current;
			}
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x000CF8B9 File Offset: 0x000CDAB9
		void IEnumerator.Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x000CF8C6 File Offset: 0x000CDAC6
		bool IEnumerator.MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06002769 RID: 10089 RVA: 0x000CF8D3 File Offset: 0x000CDAD3
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		// Token: 0x04001125 RID: 4389
		private IEnumerator enumerator;
	}
}
