using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	// Token: 0x020000D1 RID: 209
	public abstract class XmlNodeList : IEnumerable
	{
		// Token: 0x06000C61 RID: 3169
		public abstract XmlNode Item(int index);

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000C62 RID: 3170
		public abstract int Count { get; }

		// Token: 0x06000C63 RID: 3171
		public abstract IEnumerator GetEnumerator();

		// Token: 0x170002CD RID: 717
		[IndexerName("ItemOf")]
		public virtual XmlNode this[int i]
		{
			get
			{
				return this.Item(i);
			}
		}
	}
}
