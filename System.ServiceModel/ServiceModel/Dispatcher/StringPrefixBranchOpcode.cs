using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004DC RID: 1244
	internal class StringPrefixBranchOpcode : QueryConditionalBranchOpcode
	{
		// Token: 0x06002F2A RID: 12074 RVA: 0x000B6340 File Offset: 0x000B4540
		internal StringPrefixBranchOpcode() : base(OpcodeID.StringPrefixBranch, new TrieBranchIndex())
		{
		}
	}
}
