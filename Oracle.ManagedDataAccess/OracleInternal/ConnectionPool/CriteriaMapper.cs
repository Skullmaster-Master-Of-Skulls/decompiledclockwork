using System;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x020000DB RID: 219
	internal class CriteriaMapper
	{
		// Token: 0x060008B2 RID: 2226 RVA: 0x0005DD00 File Offset: 0x0005BF00
		internal CriteriaMapper()
		{
			this.m_dictConnectionId = new SyncDictionary<string, uint>();
			this.m_dictEditionId = new SyncDictionary<string, uint>();
			this.m_syncCriteriaCtx = new object();
			this.m_syncConnCls = new object();
			this.m_syncEdition = new object();
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0005DD58 File Offset: 0x0005BF58
		internal void GetId(OracleConnectionImpl pr)
		{
			if (pr != null)
			{
				lock (this.m_syncCriteriaCtx)
				{
					if (pr != null)
					{
						if (!string.IsNullOrEmpty(pr.m_connectionClass))
						{
							pr.m_criteriaIds[(int)((UIntPtr)0)] = this.m_dictConnectionId[pr.m_connectionClass];
						}
						else
						{
							pr.m_criteriaIds[(int)((UIntPtr)0)] = 0U;
						}
						if (!string.IsNullOrEmpty(pr.EditionName))
						{
							pr.m_criteriaIds[(int)((UIntPtr)1)] = this.m_dictEditionId[pr.EditionName];
						}
						else
						{
							pr.m_criteriaIds[(int)((UIntPtr)1)] = 0U;
						}
					}
				}
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0005DE04 File Offset: 0x0005C004
		internal void AssignId(CriteriaCtx criteriaCtx)
		{
			if (criteriaCtx != null)
			{
				lock (this.m_syncCriteriaCtx)
				{
					if (criteriaCtx != null)
					{
						if (!string.IsNullOrEmpty(criteriaCtx.m_connectionClass))
						{
							if (this.m_dictConnectionId.ContainsKey(criteriaCtx.m_connectionClass))
							{
								criteriaCtx.m_criteriaIds[(int)((UIntPtr)0)] = this.m_dictConnectionId[criteriaCtx.m_connectionClass];
							}
							else
							{
								this.AddId(criteriaCtx.m_connectionClass, 1);
								criteriaCtx.m_criteriaIds[(int)((UIntPtr)0)] = this.m_dictConnectionId[criteriaCtx.m_connectionClass];
							}
						}
						else
						{
							criteriaCtx.m_criteriaIds[(int)((UIntPtr)0)] = 0U;
						}
						if (!string.IsNullOrEmpty(criteriaCtx.m_edition))
						{
							if (this.m_dictEditionId.ContainsKey(criteriaCtx.m_edition))
							{
								criteriaCtx.m_criteriaIds[(int)((UIntPtr)1)] = this.m_dictEditionId[criteriaCtx.m_edition];
							}
							else
							{
								this.AddId(criteriaCtx.m_edition, 2);
								criteriaCtx.m_criteriaIds[(int)((UIntPtr)1)] = this.m_dictEditionId[criteriaCtx.m_edition];
							}
						}
						else
						{
							criteriaCtx.m_criteriaIds[(int)((UIntPtr)1)] = 0U;
						}
					}
				}
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0005DF28 File Offset: 0x0005C128
		internal void AddId(string id_value, int id_type)
		{
			if (id_value != null)
			{
				switch (id_type)
				{
				case 1:
					lock (this.m_syncConnCls)
					{
						if (!this.m_dictConnectionId.ContainsKey(id_value))
						{
							this.m_dictConnectionId[id_value] = this.m_ConnectionIdCounter++;
						}
						return;
					}
					break;
				case 2:
					break;
				default:
					return;
				}
				lock (this.m_syncEdition)
				{
					if (!this.m_dictEditionId.ContainsKey(id_value))
					{
						this.m_dictEditionId[id_value] = this.m_EditionIdCounter++;
					}
				}
			}
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0005E000 File Offset: 0x0005C200
		internal void RemoveId(string id_value, int id_type)
		{
			if (id_value != null)
			{
				switch (id_type)
				{
				case 1:
					this.m_dictConnectionId.Remove(id_value);
					return;
				case 2:
					this.m_dictEditionId.Remove(id_value);
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x04000B9E RID: 2974
		private SyncDictionary<string, uint> m_dictConnectionId;

		// Token: 0x04000B9F RID: 2975
		private SyncDictionary<string, uint> m_dictEditionId;

		// Token: 0x04000BA0 RID: 2976
		private uint m_ConnectionIdCounter = 1U;

		// Token: 0x04000BA1 RID: 2977
		private uint m_EditionIdCounter = 1U;

		// Token: 0x04000BA2 RID: 2978
		internal object m_syncCriteriaCtx;

		// Token: 0x04000BA3 RID: 2979
		internal object m_syncConnCls;

		// Token: 0x04000BA4 RID: 2980
		internal object m_syncEdition;

		// Token: 0x020000DC RID: 220
		internal enum ID_TYPE
		{
			// Token: 0x04000BA6 RID: 2982
			ConnectionId = 1,
			// Token: 0x04000BA7 RID: 2983
			EditionId,
			// Token: 0x04000BA8 RID: 2984
			TagId
		}
	}
}
