using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020003BC RID: 956
	internal sealed class System_CollectionDebugView<T>
	{
		// Token: 0x0600241A RID: 9242 RVA: 0x000A958E File Offset: 0x000A778E
		public System_CollectionDebugView(ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.collection = collection;
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x0600241B RID: 9243 RVA: 0x000A95AC File Offset: 0x000A77AC
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				T[] array = new T[this.collection.Count];
				this.collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04002001 RID: 8193
		private ICollection<T> collection;
	}
}
