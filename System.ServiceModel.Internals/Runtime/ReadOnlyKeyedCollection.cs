using System;
using System.Collections.ObjectModel;

namespace System.Runtime
{
	// Token: 0x02000026 RID: 38
	internal class ReadOnlyKeyedCollection<TKey, TValue> : ReadOnlyCollection<TValue>
	{
		// Token: 0x0600013B RID: 315 RVA: 0x00005C97 File Offset: 0x00003E97
		public ReadOnlyKeyedCollection(KeyedCollection<TKey, TValue> innerCollection) : base(innerCollection)
		{
			this.innerCollection = innerCollection;
		}

		// Token: 0x1700002F RID: 47
		public TValue this[TKey key]
		{
			get
			{
				return this.innerCollection[key];
			}
		}

		// Token: 0x04000096 RID: 150
		private KeyedCollection<TKey, TValue> innerCollection;
	}
}
