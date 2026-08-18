using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000277 RID: 631
	public sealed class XmlSchemaCollectionEnumerator : IEnumerator
	{
		// Token: 0x060025F9 RID: 9721 RVA: 0x000CD8E2 File Offset: 0x000CBAE2
		internal XmlSchemaCollectionEnumerator(Hashtable collection)
		{
			this.enumerator = collection.GetEnumerator();
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x000CD8F6 File Offset: 0x000CBAF6
		void IEnumerator.Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x000CD903 File Offset: 0x000CBB03
		bool IEnumerator.MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x000CD910 File Offset: 0x000CBB10
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x060025FD RID: 9725 RVA: 0x000CD91D File Offset: 0x000CBB1D
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x060025FE RID: 9726 RVA: 0x000CD928 File Offset: 0x000CBB28
		public XmlSchema Current
		{
			get
			{
				XmlSchemaCollectionNode xmlSchemaCollectionNode = (XmlSchemaCollectionNode)this.enumerator.Value;
				if (xmlSchemaCollectionNode != null)
				{
					return xmlSchemaCollectionNode.Schema;
				}
				return null;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x060025FF RID: 9727 RVA: 0x000CD954 File Offset: 0x000CBB54
		internal XmlSchemaCollectionNode CurrentNode
		{
			get
			{
				return (XmlSchemaCollectionNode)this.enumerator.Value;
			}
		}

		// Token: 0x04001094 RID: 4244
		private IDictionaryEnumerator enumerator;
	}
}
