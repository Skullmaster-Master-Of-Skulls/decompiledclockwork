using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005F2 RID: 1522
	internal class ExtendedNodeInfo : NodeInfo
	{
		// Token: 0x06003C43 RID: 15427 RVA: 0x00118EA4 File Offset: 0x001170A4
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

		// Token: 0x06003C44 RID: 15428 RVA: 0x00118F04 File Offset: 0x00117104
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

		// Token: 0x06003C45 RID: 15429 RVA: 0x00118F5C File Offset: 0x0011715C
		internal override void ComputeHashValue(Command cmd, Node n)
		{
			base.ComputeHashValue(cmd, n);
			this.m_hashValue = (this.m_hashValue << 4 ^ NodeInfo.GetHashValue(this.Definitions));
			this.m_hashValue = (this.m_hashValue << 4 ^ NodeInfo.GetHashValue(this.Keys.KeyVars));
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06003C46 RID: 15430 RVA: 0x00118FAA File Offset: 0x001171AA
		internal VarVec LocalDefinitions
		{
			get
			{
				return this.m_localDefinitions;
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06003C47 RID: 15431 RVA: 0x00118FB2 File Offset: 0x001171B2
		internal VarVec Definitions
		{
			get
			{
				return this.m_definitions;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06003C48 RID: 15432 RVA: 0x00118FBA File Offset: 0x001171BA
		internal KeyVec Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06003C49 RID: 15433 RVA: 0x00118FC2 File Offset: 0x001171C2
		internal VarVec NonNullableDefinitions
		{
			get
			{
				return this.m_nonNullableDefinitions;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06003C4A RID: 15434 RVA: 0x00118FCA File Offset: 0x001171CA
		internal VarVec NonNullableVisibleDefinitions
		{
			get
			{
				return this.m_nonNullableVisibleDefinitions;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06003C4B RID: 15435 RVA: 0x00118FD2 File Offset: 0x001171D2
		// (set) Token: 0x06003C4C RID: 15436 RVA: 0x00118FDA File Offset: 0x001171DA
		internal RowCount MinRows
		{
			get
			{
				return this.m_minRows;
			}
			set
			{
				this.m_minRows = value;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06003C4D RID: 15437 RVA: 0x00118FE3 File Offset: 0x001171E3
		// (set) Token: 0x06003C4E RID: 15438 RVA: 0x00118FEB File Offset: 0x001171EB
		internal RowCount MaxRows
		{
			get
			{
				return this.m_maxRows;
			}
			set
			{
				this.m_maxRows = value;
			}
		}

		// Token: 0x06003C4F RID: 15439 RVA: 0x00118FF4 File Offset: 0x001171F4
		internal void SetRowCount(RowCount minRows, RowCount maxRows)
		{
			this.m_minRows = minRows;
			this.m_maxRows = maxRows;
		}

		// Token: 0x06003C50 RID: 15440 RVA: 0x00119004 File Offset: 0x00117204
		internal void InitRowCountFrom(ExtendedNodeInfo source)
		{
			this.m_minRows = source.m_minRows;
			this.m_maxRows = source.m_maxRows;
		}

		// Token: 0x06003C51 RID: 15441 RVA: 0x0011901E File Offset: 0x0011721E
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[Conditional("DEBUG")]
		private void ValidateRowCount()
		{
		}

		// Token: 0x04001694 RID: 5780
		private readonly VarVec m_localDefinitions;

		// Token: 0x04001695 RID: 5781
		private readonly VarVec m_definitions;

		// Token: 0x04001696 RID: 5782
		private readonly KeyVec m_keys;

		// Token: 0x04001697 RID: 5783
		private readonly VarVec m_nonNullableDefinitions;

		// Token: 0x04001698 RID: 5784
		private readonly VarVec m_nonNullableVisibleDefinitions;

		// Token: 0x04001699 RID: 5785
		private RowCount m_minRows;

		// Token: 0x0400169A RID: 5786
		private RowCount m_maxRows;
	}
}
