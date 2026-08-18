using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200023F RID: 575
	public sealed class XmlSchemaCollectionEnumerator : IEnumerator
	{
		// Token: 0x06001B71 RID: 7025 RVA: 0x00081A0E File Offset: 0x00080A0E
		internal XmlSchemaCollectionEnumerator(Hashtable collection)
		{
			this.enumerator = collection.GetEnumerator();
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x00081A22 File Offset: 0x00080A22
		void IEnumerator.Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x00081A2F File Offset: 0x00080A2F
		bool IEnumerator.MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x00081A3C File Offset: 0x00080A3C
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x00081A49 File Offset: 0x00080A49
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x00081A54 File Offset: 0x00080A54
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

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x00081A80 File Offset: 0x00080A80
		internal XmlSchemaCollectionNode CurrentNode
		{
			get
			{
				return (XmlSchemaCollectionNode)this.enumerator.Value;
			}
		}

		// Token: 0x0400110D RID: 4365
		private IDictionaryEnumerator enumerator;
	}
}
