using System;
using System.Diagnostics;

namespace System.Collections.Concurrent
{
	// Token: 0x020003D1 RID: 977
	internal sealed class SystemThreadingCollections_BlockingCollectionDebugView<T>
	{
		// Token: 0x06002594 RID: 9620 RVA: 0x000AEBE0 File Offset: 0x000ACDE0
		public SystemThreadingCollections_BlockingCollectionDebugView(BlockingCollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.m_blockingCollection = collection;
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002595 RID: 9621 RVA: 0x000AEBFD File Offset: 0x000ACDFD
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this.m_blockingCollection.ToArray();
			}
		}

		// Token: 0x0400205F RID: 8287
		private BlockingCollection<T> m_blockingCollection;
	}
}
