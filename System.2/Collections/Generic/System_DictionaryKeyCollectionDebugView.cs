using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020003C0 RID: 960
	internal sealed class System_DictionaryKeyCollectionDebugView<TKey, TValue>
	{
		// Token: 0x06002422 RID: 9250 RVA: 0x000A9678 File Offset: 0x000A7878
		public System_DictionaryKeyCollectionDebugView(ICollection<TKey> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			this.collection = collection;
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06002423 RID: 9251 RVA: 0x000A9690 File Offset: 0x000A7890
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TKey[] Items
		{
			get
			{
				TKey[] array = new TKey[this.collection.Count];
				this.collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04002005 RID: 8197
		private ICollection<TKey> collection;
	}
}
