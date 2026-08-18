using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000039 RID: 57
	internal class StoreCategoryInstanceEnumeration : IEnumerator
	{
		// Token: 0x06000111 RID: 273 RVA: 0x00006F7C File Offset: 0x0000517C
		[SecuritySafeCritical]
		public StoreCategoryInstanceEnumeration(IEnumSTORE_CATEGORY_INSTANCE pI)
		{
			this._enum = pI;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006C59 File Offset: 0x00004E59
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006F8B File Offset: 0x0000518B
		private STORE_CATEGORY_INSTANCE GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00006FA1 File Offset: 0x000051A1
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00006FAE File Offset: 0x000051AE
		public STORE_CATEGORY_INSTANCE Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006FB8 File Offset: 0x000051B8
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			STORE_CATEGORY_INSTANCE[] array = new STORE_CATEGORY_INSTANCE[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = (num == 1U);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006FF8 File Offset: 0x000051F8
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04000139 RID: 313
		private IEnumSTORE_CATEGORY_INSTANCE _enum;

		// Token: 0x0400013A RID: 314
		private bool _fValid;

		// Token: 0x0400013B RID: 315
		private STORE_CATEGORY_INSTANCE _current;
	}
}
