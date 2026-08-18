using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020003C1 RID: 961
	internal sealed class System_DictionaryValueCollectionDebugView<TKey, TValue>
	{
		// Token: 0x06002424 RID: 9252 RVA: 0x000A96BC File Offset: 0x000A78BC
		public System_DictionaryValueCollectionDebugView(ICollection<TValue> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			this.collection = collection;
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06002425 RID: 9253 RVA: 0x000A96D4 File Offset: 0x000A78D4
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TValue[] Items
		{
			get
			{
				TValue[] array = new TValue[this.collection.Count];
				this.collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04002006 RID: 8198
		private ICollection<TValue> collection;
	}
}
