using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000628 RID: 1576
	internal class SingleStreamNestOp : NestBaseOp
	{
		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06003D6D RID: 15725 RVA: 0x0011B2E2 File Offset: 0x001194E2
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06003D6E RID: 15726 RVA: 0x0011B2E5 File Offset: 0x001194E5
		internal Var Discriminator
		{
			get
			{
				return this.m_discriminator;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06003D6F RID: 15727 RVA: 0x0011B2ED File Offset: 0x001194ED
		internal List<SortKey> PostfixSortKeys
		{
			get
			{
				return this.m_postfixSortKeys;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06003D70 RID: 15728 RVA: 0x0011B2F5 File Offset: 0x001194F5
		internal VarVec Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x06003D71 RID: 15729 RVA: 0x0011B2FD File Offset: 0x001194FD
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003D72 RID: 15730 RVA: 0x0011B307 File Offset: 0x00119507
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x0011B311 File Offset: 0x00119511
		internal SingleStreamNestOp(VarVec keys, List<SortKey> prefixSortKeys, List<SortKey> postfixSortKeys, VarVec outputVars, List<CollectionInfo> collectionInfoList, Var discriminatorVar) : base(OpType.SingleStreamNest, prefixSortKeys, outputVars, collectionInfoList)
		{
			this.m_keys = keys;
			this.m_postfixSortKeys = postfixSortKeys;
			this.m_discriminator = discriminatorVar;
		}

		// Token: 0x04001733 RID: 5939
		private readonly VarVec m_keys;

		// Token: 0x04001734 RID: 5940
		private readonly Var m_discriminator;

		// Token: 0x04001735 RID: 5941
		private readonly List<SortKey> m_postfixSortKeys;
	}
}
