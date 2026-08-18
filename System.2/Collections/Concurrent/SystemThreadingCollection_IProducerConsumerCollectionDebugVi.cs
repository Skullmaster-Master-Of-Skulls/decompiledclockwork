using System;
using System.Diagnostics;

namespace System.Collections.Concurrent
{
	// Token: 0x020003D3 RID: 979
	internal sealed class SystemThreadingCollection_IProducerConsumerCollectionDebugView<T>
	{
		// Token: 0x060025B7 RID: 9655 RVA: 0x000AF508 File Offset: 0x000AD708
		public SystemThreadingCollection_IProducerConsumerCollectionDebugView(IProducerConsumerCollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.m_collection = collection;
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x000AF525 File Offset: 0x000AD725
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this.m_collection.ToArray();
			}
		}

		// Token: 0x04002065 RID: 8293
		private IProducerConsumerCollection<T> m_collection;
	}
}
