using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004C6 RID: 1222
	internal struct QueryNode
	{
		// Token: 0x06002E53 RID: 11859 RVA: 0x000B455D File Offset: 0x000B275D
		internal QueryNode(SeekableXPathNavigator node)
		{
			this.node = node;
			this.nodePosition = node.CurrentPosition;
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06002E54 RID: 11860 RVA: 0x000B4572 File Offset: 0x000B2772
		internal string LocalName
		{
			get
			{
				return this.node.GetLocalName(this.nodePosition);
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06002E55 RID: 11861 RVA: 0x000B4585 File Offset: 0x000B2785
		internal string Name
		{
			get
			{
				return this.node.GetName(this.nodePosition);
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06002E56 RID: 11862 RVA: 0x000B4598 File Offset: 0x000B2798
		internal string Namespace
		{
			get
			{
				return this.node.GetNamespace(this.nodePosition);
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06002E57 RID: 11863 RVA: 0x000B45AB File Offset: 0x000B27AB
		internal SeekableXPathNavigator Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x000B45B3 File Offset: 0x000B27B3
		internal long Position
		{
			get
			{
				return this.nodePosition;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06002E59 RID: 11865 RVA: 0x000B45BB File Offset: 0x000B27BB
		internal string Value
		{
			get
			{
				return this.node.GetValue(this.nodePosition);
			}
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x000B45CE File Offset: 0x000B27CE
		internal SeekableXPathNavigator MoveTo()
		{
			this.node.CurrentPosition = this.nodePosition;
			return this.node;
		}

		// Token: 0x04002539 RID: 9529
		private SeekableXPathNavigator node;

		// Token: 0x0400253A RID: 9530
		private long nodePosition;
	}
}
