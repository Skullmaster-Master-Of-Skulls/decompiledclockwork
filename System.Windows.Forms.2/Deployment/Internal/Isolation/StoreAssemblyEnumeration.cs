using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000031 RID: 49
	internal class StoreAssemblyEnumeration : IEnumerator
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00006D3C File Offset: 0x00004F3C
		[SecuritySafeCritical]
		public StoreAssemblyEnumeration(IEnumSTORE_ASSEMBLY pI)
		{
			this._enum = pI;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006D4B File Offset: 0x00004F4B
		private STORE_ASSEMBLY GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00006D61 File Offset: 0x00004F61
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00006D6E File Offset: 0x00004F6E
		public STORE_ASSEMBLY Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006C59 File Offset: 0x00004E59
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00006D78 File Offset: 0x00004F78
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			STORE_ASSEMBLY[] array = new STORE_ASSEMBLY[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = (num == 1U);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006DB8 File Offset: 0x00004FB8
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x0400012D RID: 301
		private IEnumSTORE_ASSEMBLY _enum;

		// Token: 0x0400012E RID: 302
		private bool _fValid;

		// Token: 0x0400012F RID: 303
		private STORE_ASSEMBLY _current;
	}
}
