using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	// Token: 0x02000118 RID: 280
	public abstract class XmlNodeList : IEnumerable, IDisposable
	{
		// Token: 0x0600139A RID: 5018
		public abstract XmlNode Item(int index);

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600139B RID: 5019
		public abstract int Count { get; }

		// Token: 0x0600139C RID: 5020
		public abstract IEnumerator GetEnumerator();

		// Token: 0x17000416 RID: 1046
		[IndexerName("ItemOf")]
		public virtual XmlNode this[int i]
		{
			get
			{
				return this.Item(i);
			}
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x00051834 File Offset: 0x0004FA34
		void IDisposable.Dispose()
		{
			this.PrivateDisposeNodeList();
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0005183C File Offset: 0x0004FA3C
		protected virtual void PrivateDisposeNodeList()
		{
		}
	}
}
