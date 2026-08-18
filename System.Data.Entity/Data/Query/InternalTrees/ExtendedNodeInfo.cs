using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000B6 RID: 182
	internal class ExtendedNodeInfo : NodeInfo
	{
		// Token: 0x06000B5A RID: 2906 RVA: 0x000397DC File Offset: 0x000379DC
		internal ExtendedNodeInfo(Command cmd) : base(cmd)
		{
			this.m_localDefinitions = cmd.CreateVarVec();
			this.m_definitions = cmd.CreateVarVec();
			this.m_nonNullableDefinitions = cmd.CreateVarVec();
			this.m_nonNullableVisibleDefinitions = cmd.CreateVarVec();
			this.m_keys = new KeyVec(cmd);
			this.m_minRows = RowCount.Zero;
			this.m_maxRows = RowCount.Unbounded;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0003983C File Offset: 0x00037A3C
		internal override void Clear()
		{
			base.Clear();
			this.m_definitions.Clear();
			this.m_localDefinitions.Clear();
			this.m_nonNullableDefinitions.Clear();
			this.m_nonNullableVisibleDefinitions.Clear();
			this.m_keys.Clear();
			this.m_minRows = RowCount.Zero;
			this.m_maxRows = RowCount.Unbounded;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00039894 File Offset: 0x00037A94
		internal override void ComputeHashValue(Command cmd, Node n)
		{
			base.ComputeHashValue(cmd, n);
			this.m_hashValue = (this.m_hashValue << 4 ^ NodeInfo.GetHashValue(this.Definitions));
			this.m_hashValue = (this.m_hashValue << 4 ^ NodeInfo.GetHashValue(this.Keys.KeyVars));
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x000398E2 File Offset: 0x00037AE2
		internal VarVec LocalDefinitions
		{
			get
			{
				return this.m_localDefinitions;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x000398EA File Offset: 0x00037AEA
		internal VarVec Definitions
		{
			get
			{
				return this.m_definitions;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x000398F2 File Offset: 0x00037AF2
		internal KeyVec Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x000398FA File Offset: 0x00037AFA
		internal VarVec NonNullableDefinitions
		{
			get
			{
				return this.m_nonNullableDefinitions;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x00039902 File Offset: 0x00037B02
		internal VarVec NonNullableVisibleDefinitions
		{
			get
			{
				return this.m_nonNullableVisibleDefinitions;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0003990A File Offset: 0x00037B0A
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x00039912 File Offset: 0x00037B12
		internal RowCount MinRows
		{
			get
			{
				return this.m_minRows;
			}
			set
			{
				this.m_minRows = value;
				this.ValidateRowCount();
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00039921 File Offset: 0x00037B21
		// (set) Token: 0x06000B65 RID: 2917 RVA: 0x00039929 File Offset: 0x00037B29
		internal RowCount MaxRows
		{
			get
			{
				return this.m_maxRows;
			}
			set
			{
				this.m_maxRows = value;
				this.ValidateRowCount();
			}
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00039938 File Offset: 0x00037B38
		internal void SetRowCount(RowCount minRows, RowCount maxRows)
		{
			this.m_minRows = minRows;
			this.m_maxRows = maxRows;
			this.ValidateRowCount();
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0003994E File Offset: 0x00037B4E
		internal void InitRowCountFrom(ExtendedNodeInfo source)
		{
			this.m_minRows = source.m_minRows;
			this.m_maxRows = source.m_maxRows;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x000089D0 File Offset: 0x00006BD0
		private void ValidateRowCount()
		{
		}

		// Token: 0x040008F3 RID: 2291
		private VarVec m_localDefinitions;

		// Token: 0x040008F4 RID: 2292
		private VarVec m_definitions;

		// Token: 0x040008F5 RID: 2293
		private KeyVec m_keys;

		// Token: 0x040008F6 RID: 2294
		private VarVec m_nonNullableDefinitions;

		// Token: 0x040008F7 RID: 2295
		private VarVec m_nonNullableVisibleDefinitions;

		// Token: 0x040008F8 RID: 2296
		private RowCount m_minRows;

		// Token: 0x040008F9 RID: 2297
		private RowCount m_maxRows;
	}
}
