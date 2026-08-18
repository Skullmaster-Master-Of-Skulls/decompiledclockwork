using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005FF RID: 1535
	internal class KeyVec
	{
		// Token: 0x06003C97 RID: 15511 RVA: 0x00119330 File Offset: 0x00117530
		internal KeyVec(Command itree)
		{
			this.m_keys = itree.CreateVarVec();
			this.m_noKeys = true;
		}

		// Token: 0x06003C98 RID: 15512 RVA: 0x0011934B File Offset: 0x0011754B
		internal void InitFrom(KeyVec keyset)
		{
			this.m_keys.InitFrom(keyset.m_keys);
			this.m_noKeys = keyset.m_noKeys;
		}

		// Token: 0x06003C99 RID: 15513 RVA: 0x0011936A File Offset: 0x0011756A
		internal void InitFrom(IEnumerable<Var> varSet)
		{
			this.InitFrom(varSet, false);
		}

		// Token: 0x06003C9A RID: 15514 RVA: 0x00119374 File Offset: 0x00117574
		internal void InitFrom(IEnumerable<Var> varSet, bool ignoreParameters)
		{
			this.m_keys.InitFrom(varSet, ignoreParameters);
			this.m_noKeys = false;
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x0011938C File Offset: 0x0011758C
		internal void InitFrom(KeyVec left, KeyVec right)
		{
			if (left.m_noKeys || right.m_noKeys)
			{
				this.m_noKeys = true;
				return;
			}
			this.m_noKeys = false;
			this.m_keys.InitFrom(left.m_keys);
			this.m_keys.Or(right.m_keys);
		}

		// Token: 0x06003C9C RID: 15516 RVA: 0x001193DC File Offset: 0x001175DC
		internal void InitFrom(List<KeyVec> keyVecList)
		{
			this.m_noKeys = false;
			this.m_keys.Clear();
			foreach (KeyVec keyVec in keyVecList)
			{
				if (keyVec.m_noKeys)
				{
					this.m_noKeys = true;
					break;
				}
				this.m_keys.Or(keyVec.m_keys);
			}
		}

		// Token: 0x06003C9D RID: 15517 RVA: 0x00119458 File Offset: 0x00117658
		internal void Clear()
		{
			this.m_noKeys = true;
			this.m_keys.Clear();
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06003C9E RID: 15518 RVA: 0x0011946C File Offset: 0x0011766C
		internal VarVec KeyVars
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06003C9F RID: 15519 RVA: 0x00119474 File Offset: 0x00117674
		// (set) Token: 0x06003CA0 RID: 15520 RVA: 0x0011947C File Offset: 0x0011767C
		internal bool NoKeys
		{
			get
			{
				return this.m_noKeys;
			}
			set
			{
				this.m_noKeys = value;
			}
		}

		// Token: 0x040016AF RID: 5807
		private readonly VarVec m_keys;

		// Token: 0x040016B0 RID: 5808
		private bool m_noKeys;
	}
}
