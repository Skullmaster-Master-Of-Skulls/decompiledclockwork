using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000B4 RID: 180
	internal class NodeInfo
	{
		// Token: 0x06000B54 RID: 2900 RVA: 0x000396AD File Offset: 0x000378AD
		internal NodeInfo(Command cmd)
		{
			this.m_externalReferences = cmd.CreateVarVec();
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x000396C1 File Offset: 0x000378C1
		internal virtual void Clear()
		{
			this.m_externalReferences.Clear();
			this.m_hashValue = 0;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x000396D5 File Offset: 0x000378D5
		internal VarVec ExternalReferences
		{
			get
			{
				return this.m_externalReferences;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x000396DD File Offset: 0x000378DD
		internal int HashValue
		{
			get
			{
				return this.m_hashValue;
			}
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x000396E8 File Offset: 0x000378E8
		internal static int GetHashValue(VarVec vec)
		{
			int num = 0;
			foreach (Var var in vec)
			{
				num ^= var.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00039738 File Offset: 0x00037938
		internal virtual void ComputeHashValue(Command cmd, Node n)
		{
			this.m_hashValue = 0;
			foreach (Node n2 in n.Children)
			{
				NodeInfo nodeInfo = cmd.GetNodeInfo(n2);
				this.m_hashValue ^= nodeInfo.HashValue;
			}
			this.m_hashValue = (this.m_hashValue << 4 ^ (int)n.Op.OpType);
			this.m_hashValue = (this.m_hashValue << 4 ^ NodeInfo.GetHashValue(this.m_externalReferences));
		}

		// Token: 0x040008ED RID: 2285
		private VarVec m_externalReferences;

		// Token: 0x040008EE RID: 2286
		protected int m_hashValue;
	}
}
