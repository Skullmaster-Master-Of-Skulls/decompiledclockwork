using System;
using System.Collections;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006F4 RID: 1780
	internal class DictionaryEnumeratorByKeys : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x06003F7D RID: 16253 RVA: 0x000D8935 File Offset: 0x000D7935
		public DictionaryEnumeratorByKeys(IDictionary properties)
		{
			this._properties = properties;
			this._keyEnum = properties.Keys.GetEnumerator();
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x000D8955 File Offset: 0x000D7955
		public bool MoveNext()
		{
			return this._keyEnum.MoveNext();
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x000D8962 File Offset: 0x000D7962
		public void Reset()
		{
			this._keyEnum.Reset();
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06003F80 RID: 16256 RVA: 0x000D896F File Offset: 0x000D796F
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06003F81 RID: 16257 RVA: 0x000D897C File Offset: 0x000D797C
		public DictionaryEntry Entry
		{
			get
			{
				return new DictionaryEntry(this.Key, this.Value);
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06003F82 RID: 16258 RVA: 0x000D898F File Offset: 0x000D798F
		public object Key
		{
			get
			{
				return this._keyEnum.Current;
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06003F83 RID: 16259 RVA: 0x000D899C File Offset: 0x000D799C
		public object Value
		{
			get
			{
				return this._properties[this.Key];
			}
		}

		// Token: 0x04002020 RID: 8224
		private IDictionary _properties;

		// Token: 0x04002021 RID: 8225
		private IEnumerator _keyEnum;
	}
}
