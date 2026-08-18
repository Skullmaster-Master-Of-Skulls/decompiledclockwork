using System;
using System.Collections.Generic;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000B3 RID: 179
	internal class KeyVec
	{
		// Token: 0x06000B4A RID: 2890 RVA: 0x00039557 File Offset: 0x00037757
		internal KeyVec(Command itree)
		{
			this.m_keys = itree.CreateVarVec();
			this.m_noKeys = true;
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00039572 File Offset: 0x00037772
		internal void InitFrom(KeyVec keyset)
		{
			this.m_keys.InitFrom(keyset.m_keys);
			this.m_noKeys = keyset.m_noKeys;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00039591 File Offset: 0x00037791
		internal void InitFrom(IEnumerable<Var> varSet)
		{
			this.InitFrom(varSet, false);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0003959B File Offset: 0x0003779B
		internal void InitFrom(IEnumerable<Var> varSet, bool ignoreParameters)
		{
			this.m_keys.InitFrom(varSet, ignoreParameters);
			this.m_noKeys = false;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x000395B4 File Offset: 0x000377B4
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

		// Token: 0x06000B4F RID: 2895 RVA: 0x00039604 File Offset: 0x00037804
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

		// Token: 0x06000B50 RID: 2896 RVA: 0x00039680 File Offset: 0x00037880
		internal void Clear()
		{
			this.m_noKeys = true;
			this.m_keys.Clear();
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x00039694 File Offset: 0x00037894
		internal VarVec KeyVars
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0003969C File Offset: 0x0003789C
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x000396A4 File Offset: 0x000378A4
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

		// Token: 0x040008EB RID: 2283
		private VarVec m_keys;

		// Token: 0x040008EC RID: 2284
		private bool m_noKeys;
	}
}
