using System;
using System.Collections;

namespace System.Web
{
	// Token: 0x020000BE RID: 190
	internal class HttpStaticObjectsEnumerator : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x06000D38 RID: 3384 RVA: 0x00025024 File Offset: 0x00023224
		internal HttpStaticObjectsEnumerator(IDictionaryEnumerator e)
		{
			this._enum = e;
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00025033 File Offset: 0x00023233
		public void Reset()
		{
			this._enum.Reset();
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00025040 File Offset: 0x00023240
		public bool MoveNext()
		{
			return this._enum.MoveNext();
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x0002504D File Offset: 0x0002324D
		public object Key
		{
			get
			{
				return this._enum.Key;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x0002505C File Offset: 0x0002325C
		public object Value
		{
			get
			{
				HttpStaticObjectsEntry httpStaticObjectsEntry = (HttpStaticObjectsEntry)this._enum.Value;
				if (httpStaticObjectsEntry == null)
				{
					return null;
				}
				return httpStaticObjectsEntry.Instance;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x00025085 File Offset: 0x00023285
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x00025092 File Offset: 0x00023292
		public DictionaryEntry Entry
		{
			get
			{
				return new DictionaryEntry(this._enum.Key, this.Value);
			}
		}

		// Token: 0x040004E8 RID: 1256
		private IDictionaryEnumerator _enum;
	}
}
