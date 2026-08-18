using System;
using System.Collections.Generic;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004E5 RID: 1253
	internal abstract class HashBranchIndex : QueryBranchIndex
	{
		// Token: 0x06002F9E RID: 12190 RVA: 0x000B712F File Offset: 0x000B532F
		internal HashBranchIndex()
		{
			this.literals = new Dictionary<object, QueryBranch>();
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06002F9F RID: 12191 RVA: 0x000B7142 File Offset: 0x000B5342
		internal override int Count
		{
			get
			{
				return this.literals.Count;
			}
		}

		// Token: 0x17000B4E RID: 2894
		internal override QueryBranch this[object literal]
		{
			get
			{
				QueryBranch result;
				if (this.literals.TryGetValue(literal, out result))
				{
					return result;
				}
				return null;
			}
			set
			{
				this.literals[literal] = value;
			}
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x000B7180 File Offset: 0x000B5380
		internal override void CollectXPathFilters(ICollection<MessageFilter> filters)
		{
			foreach (QueryBranch queryBranch in this.literals.Values)
			{
				queryBranch.Branch.CollectXPathFilters(filters);
			}
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x000B71E0 File Offset: 0x000B53E0
		internal override void Remove(object key)
		{
			this.literals.Remove(key);
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000B71EF File Offset: 0x000B53EF
		internal override void Trim()
		{
		}

		// Token: 0x040025E6 RID: 9702
		private Dictionary<object, QueryBranch> literals;
	}
}
