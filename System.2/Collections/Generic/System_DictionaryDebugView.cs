using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020003BF RID: 959
	internal sealed class System_DictionaryDebugView<K, V>
	{
		// Token: 0x06002420 RID: 9248 RVA: 0x000A962C File Offset: 0x000A782C
		public System_DictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dict = dictionary;
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06002421 RID: 9249 RVA: 0x000A964C File Offset: 0x000A784C
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<K, V>[] Items
		{
			get
			{
				KeyValuePair<K, V>[] array = new KeyValuePair<K, V>[this.dict.Count];
				this.dict.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04002004 RID: 8196
		private IDictionary<K, V> dict;
	}
}
