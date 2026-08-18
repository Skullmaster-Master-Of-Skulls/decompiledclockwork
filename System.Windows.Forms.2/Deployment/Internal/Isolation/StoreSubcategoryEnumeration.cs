using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000037 RID: 55
	internal class StoreSubcategoryEnumeration : IEnumerator
	{
		// Token: 0x06000106 RID: 262 RVA: 0x00006EEC File Offset: 0x000050EC
		[SecuritySafeCritical]
		public StoreSubcategoryEnumeration(IEnumSTORE_CATEGORY_SUBCATEGORY pI)
		{
			this._enum = pI;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00006C59 File Offset: 0x00004E59
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006EFB File Offset: 0x000050FB
		private STORE_CATEGORY_SUBCATEGORY GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006F11 File Offset: 0x00005111
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00006F1E File Offset: 0x0000511E
		public STORE_CATEGORY_SUBCATEGORY Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006F28 File Offset: 0x00005128
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			STORE_CATEGORY_SUBCATEGORY[] array = new STORE_CATEGORY_SUBCATEGORY[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = (num == 1U);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006F68 File Offset: 0x00005168
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04000136 RID: 310
		private IEnumSTORE_CATEGORY_SUBCATEGORY _enum;

		// Token: 0x04000137 RID: 311
		private bool _fValid;

		// Token: 0x04000138 RID: 312
		private STORE_CATEGORY_SUBCATEGORY _current;
	}
}
