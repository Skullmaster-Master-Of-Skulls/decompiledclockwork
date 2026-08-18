using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004E8 RID: 1256
	internal class NumberBranchIndex : HashBranchIndex
	{
		// Token: 0x06002FA9 RID: 12201 RVA: 0x000B7294 File Offset: 0x000B5494
		internal override void Match(int valIndex, ref Value val, QueryBranchResultSet results)
		{
			QueryBranch queryBranch;
			if (ValueDataType.Sequence == val.Type)
			{
				NodeSequence sequence = val.Sequence;
				for (int i = 0; i < sequence.Count; i++)
				{
					queryBranch = this[sequence.Items[i].NumberValue()];
					if (queryBranch != null)
					{
						results.Add(queryBranch, valIndex);
					}
				}
				return;
			}
			queryBranch = this[val.ToDouble()];
			if (queryBranch != null)
			{
				results.Add(queryBranch, valIndex);
			}
		}
	}
}
