using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200002F RID: 47
	internal class StoreDeploymentMetadataPropertyEnumeration : IEnumerator
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00006CAC File Offset: 0x00004EAC
		[SecuritySafeCritical]
		public StoreDeploymentMetadataPropertyEnumeration(IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY pI)
		{
			this._enum = pI;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00006CBB File Offset: 0x00004EBB
		private StoreOperationMetadataProperty GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00006CD1 File Offset: 0x00004ED1
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00006CDE File Offset: 0x00004EDE
		public StoreOperationMetadataProperty Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00006C59 File Offset: 0x00004E59
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00006CE8 File Offset: 0x00004EE8
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			StoreOperationMetadataProperty[] array = new StoreOperationMetadataProperty[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = (num == 1U);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006D28 File Offset: 0x00004F28
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x0400012A RID: 298
		private IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY _enum;

		// Token: 0x0400012B RID: 299
		private bool _fValid;

		// Token: 0x0400012C RID: 300
		private StoreOperationMetadataProperty _current;
	}
}
