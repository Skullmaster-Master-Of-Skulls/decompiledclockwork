using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200026A RID: 618
	public class XmlSchemaObjectEnumerator : IEnumerator
	{
		// Token: 0x06001CC1 RID: 7361 RVA: 0x000836A6 File Offset: 0x000826A6
		internal XmlSchemaObjectEnumerator(IEnumerator enumerator)
		{
			this.enumerator = enumerator;
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x000836B5 File Offset: 0x000826B5
		public void Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x000836C2 File Offset: 0x000826C2
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x000836CF File Offset: 0x000826CF
		public XmlSchemaObject Current
		{
			get
			{
				return (XmlSchemaObject)this.enumerator.Current;
			}
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x000836E1 File Offset: 0x000826E1
		void IEnumerator.Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x000836EE File Offset: 0x000826EE
		bool IEnumerator.MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x000836FB File Offset: 0x000826FB
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		// Token: 0x040011A3 RID: 4515
		private IEnumerator enumerator;
	}
}
