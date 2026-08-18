using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000035 RID: 53
	internal class StoreCategoryEnumeration : IEnumerator
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00006E5C File Offset: 0x0000505C
		[SecuritySafeCritical]
		public StoreCategoryEnumeration(IEnumSTORE_CATEGORY pI)
		{
			this._enum = pI;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006C59 File Offset: 0x00004E59
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006E6B File Offset: 0x0000506B
		private STORE_CATEGORY GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00006E81 File Offset: 0x00005081
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00006E8E File Offset: 0x0000508E
		public STORE_CATEGORY Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006E98 File Offset: 0x00005098
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			STORE_CATEGORY[] array = new STORE_CATEGORY[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = (num == 1U);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00006ED8 File Offset: 0x000050D8
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04000133 RID: 307
		private IEnumSTORE_CATEGORY _enum;

		// Token: 0x04000134 RID: 308
		private bool _fValid;

		// Token: 0x04000135 RID: 309
		private STORE_CATEGORY _current;
	}
}
