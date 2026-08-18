using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Web.Http
{
	// Token: 0x02000037 RID: 55
	internal class EmptyReadOnlyDictionary<TKey, TValue>
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00007053 File Offset: 0x00005253
		public static IDictionary<TKey, TValue> Value
		{
			get
			{
				return EmptyReadOnlyDictionary<TKey, TValue>._value;
			}
		}

		// Token: 0x0400007F RID: 127
		private static readonly ReadOnlyDictionary<TKey, TValue> _value = new ReadOnlyDictionary<TKey, TValue>(new Dictionary<TKey, TValue>());
	}
}
