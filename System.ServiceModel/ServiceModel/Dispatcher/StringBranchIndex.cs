using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004E6 RID: 1254
	internal class StringBranchIndex : HashBranchIndex
	{
		// Token: 0x06002FA5 RID: 12197 RVA: 0x000B71F4 File Offset: 0x000B53F4
		internal override void Match(int valIndex, ref Value val, QueryBranchResultSet results)
		{
			QueryBranch queryBranch;
			if (ValueDataType.Sequence == val.Type)
			{
				NodeSequence sequence = val.Sequence;
				for (int i = 0; i < sequence.Count; i++)
				{
					queryBranch = this[sequence.Items[i].StringValue()];
					if (queryBranch != null)
					{
						results.Add(queryBranch, valIndex);
					}
				}
				return;
			}
			queryBranch = this[val.String];
			if (queryBranch != null)
			{
				results.Add(queryBranch, valIndex);
			}
		}
	}
}
