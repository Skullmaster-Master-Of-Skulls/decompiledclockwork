using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200002D RID: 45
	internal class StoreDeploymentMetadataEnumeration : IEnumerator
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00006C2C File Offset: 0x00004E2C
		[SecuritySafeCritical]
		public StoreDeploymentMetadataEnumeration(IEnumSTORE_DEPLOYMENT_METADATA pI)
		{
			this._enum = pI;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00006C3B File Offset: 0x00004E3B
		private IDefinitionAppId GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00006C51 File Offset: 0x00004E51
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00006C51 File Offset: 0x00004E51
		public IDefinitionAppId Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00006C59 File Offset: 0x00004E59
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00006C5C File Offset: 0x00004E5C
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			IDefinitionAppId[] array = new IDefinitionAppId[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = (num == 1U);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006C98 File Offset: 0x00004E98
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04000127 RID: 295
		private IEnumSTORE_DEPLOYMENT_METADATA _enum;

		// Token: 0x04000128 RID: 296
		private bool _fValid;

		// Token: 0x04000129 RID: 297
		private IDefinitionAppId _current;
	}
}
