using System;
using System.Collections.Generic;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x02000009 RID: 9
	internal class DictionaryEnumerator<TKey, TValue> : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002B42 File Offset: 0x00000D42
		internal DictionaryEnumerator(IEnumerator<KeyValuePair<TKey, TValue>> inner)
		{
			Requires.NotNull<IEnumerator<KeyValuePair<TKey, TValue>>>(inner, "inner");
			this._inner = inner;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002B5C File Offset: 0x00000D5C
		public DictionaryEntry Entry
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this._inner.Current;
				object key = keyValuePair.Key;
				keyValuePair = this._inner.Current;
				return new DictionaryEntry(key, keyValuePair.Value);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public object Key
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this._inner.Current;
				return keyValuePair.Key;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public object Value
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this._inner.Current;
				return keyValuePair.Value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002BED File Offset: 0x00000DED
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002BFA File Offset: 0x00000DFA
		public bool MoveNext()
		{
			return this._inner.MoveNext();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C07 File Offset: 0x00000E07
		public void Reset()
		{
			this._inner.Reset();
		}

		// Token: 0x04000005 RID: 5
		private readonly IEnumerator<KeyValuePair<TKey, TValue>> _inner;
	}
}
