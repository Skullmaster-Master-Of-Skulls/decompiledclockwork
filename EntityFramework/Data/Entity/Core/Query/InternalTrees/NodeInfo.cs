using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005F1 RID: 1521
	internal class NodeInfo
	{
		// Token: 0x06003C3D RID: 15421 RVA: 0x00118D75 File Offset: 0x00116F75
		internal NodeInfo(Command cmd)
		{
			this.m_externalReferences = cmd.CreateVarVec();
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x00118D89 File Offset: 0x00116F89
		internal virtual void Clear()
		{
			this.m_externalReferences.Clear();
			this.m_hashValue = 0;
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06003C3F RID: 15423 RVA: 0x00118D9D File Offset: 0x00116F9D
		internal VarVec ExternalReferences
		{
			get
			{
				return this.m_externalReferences;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06003C40 RID: 15424 RVA: 0x00118DA5 File Offset: 0x00116FA5
		internal int HashValue
		{
			get
			{
				return this.m_hashValue;
			}
		}

		// Token: 0x06003C41 RID: 15425 RVA: 0x00118DB0 File Offset: 0x00116FB0
		internal static int GetHashValue(VarVec vec)
		{
			int num = 0;
			foreach (Var var in vec)
			{
				num ^= var.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003C42 RID: 15426 RVA: 0x00118E00 File Offset: 0x00117000
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

		// Token: 0x04001692 RID: 5778
		private readonly VarVec m_externalReferences;

		// Token: 0x04001693 RID: 5779
		protected int m_hashValue;
	}
}
