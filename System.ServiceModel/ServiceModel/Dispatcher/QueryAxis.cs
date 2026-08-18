using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004C1 RID: 1217
	internal struct QueryAxis
	{
		// Token: 0x06002E18 RID: 11800 RVA: 0x000B3D23 File Offset: 0x000B1F23
		internal QueryAxis(QueryAxisType type, AxisDirection direction, QueryNodeType principalNode, QueryNodeType validNodeTypes)
		{
			this.direction = direction;
			this.principalNode = principalNode;
			this.type = type;
			this.validNodeTypes = validNodeTypes;
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06002E19 RID: 11801 RVA: 0x000B3D42 File Offset: 0x000B1F42
		internal QueryNodeType PrincipalNodeType
		{
			get
			{
				return this.principalNode;
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06002E1A RID: 11802 RVA: 0x000B3D4A File Offset: 0x000B1F4A
		internal QueryAxisType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06002E1B RID: 11803 RVA: 0x000B3D52 File Offset: 0x000B1F52
		internal QueryNodeType ValidNodeTypes
		{
			get
			{
				return this.validNodeTypes;
			}
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x000B3D5C File Offset: 0x000B1F5C
		internal bool IsSupported()
		{
			QueryAxisType queryAxisType = this.type;
			return queryAxisType - QueryAxisType.Attribute <= 3 || queryAxisType == QueryAxisType.Self;
		}

		// Token: 0x04002528 RID: 9512
		private AxisDirection direction;

		// Token: 0x04002529 RID: 9513
		private QueryNodeType principalNode;

		// Token: 0x0400252A RID: 9514
		private QueryAxisType type;

		// Token: 0x0400252B RID: 9515
		private QueryNodeType validNodeTypes;
	}
}
