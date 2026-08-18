using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000033 RID: 51
	internal class StoreAssemblyFileEnumeration : IEnumerator
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00006DCC File Offset: 0x00004FCC
		[SecuritySafeCritical]
		public StoreAssemblyFileEnumeration(IEnumSTORE_ASSEMBLY_FILE pI)
		{
			this._enum = pI;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006C59 File Offset: 0x00004E59
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006DDB File Offset: 0x00004FDB
		private STORE_ASSEMBLY_FILE GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00006DF1 File Offset: 0x00004FF1
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00006DFE File Offset: 0x00004FFE
		public STORE_ASSEMBLY_FILE Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00006E08 File Offset: 0x00005008
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			STORE_ASSEMBLY_FILE[] array = new STORE_ASSEMBLY_FILE[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = (num == 1U);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006E48 File Offset: 0x00005048
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04000130 RID: 304
		private IEnumSTORE_ASSEMBLY_FILE _enum;

		// Token: 0x04000131 RID: 305
		private bool _fValid;

		// Token: 0x04000132 RID: 306
		private STORE_ASSEMBLY_FILE _current;
	}
}
