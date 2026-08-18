using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000C4 RID: 196
	internal class SingleStreamNestOp : NestBaseOp
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0003BE3F File Offset: 0x0003A03F
		internal Var Discriminator
		{
			get
			{
				return this.m_discriminator;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0003BE47 File Offset: 0x0003A047
		internal List<SortKey> PostfixSortKeys
		{
			get
			{
				return this.m_postfixSortKeys;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x0003BE4F File Offset: 0x0003A04F
		internal VarVec Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0003BE57 File Offset: 0x0003A057
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0003BE61 File Offset: 0x0003A061
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0003BE6B File Offset: 0x0003A06B
		internal SingleStreamNestOp(VarVec keys, List<SortKey> prefixSortKeys, List<SortKey> postfixSortKeys, VarVec outputVars, List<CollectionInfo> collectionInfoList, Var discriminatorVar) : base(OpType.SingleStreamNest, prefixSortKeys, outputVars, collectionInfoList)
		{
			this.m_keys = keys;
			this.m_postfixSortKeys = postfixSortKeys;
			this.m_discriminator = discriminatorVar;
		}

		// Token: 0x0400095C RID: 2396
		private VarVec m_keys;

		// Token: 0x0400095D RID: 2397
		private Var m_discriminator;

		// Token: 0x0400095E RID: 2398
		private List<SortKey> m_postfixSortKeys;
	}
}
