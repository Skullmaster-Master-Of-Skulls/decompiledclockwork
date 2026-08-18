using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x0200010B RID: 267
	internal sealed class NavigateOp : ScalarOp
	{
		// Token: 0x06000D93 RID: 3475 RVA: 0x0003D06D File Offset: 0x0003B26D
		internal NavigateOp(TypeUsage type, RelProperty relProperty) : base(OpType.Navigate, type)
		{
			this.m_property = relProperty;
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0003D07F File Offset: 0x0003B27F
		private NavigateOp() : base(OpType.Navigate)
		{
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0003D089 File Offset: 0x0003B289
		internal RelProperty RelProperty
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x0003D091 File Offset: 0x0003B291
		internal RelationshipType Relationship
		{
			get
			{
				return this.m_property.Relationship;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0003D09E File Offset: 0x0003B29E
		internal RelationshipEndMember FromEnd
		{
			get
			{
				return this.m_property.FromEnd;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x0003D0AB File Offset: 0x0003B2AB
		internal RelationshipEndMember ToEnd
		{
			get
			{
				return this.m_property.ToEnd;
			}
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0003D0B8 File Offset: 0x0003B2B8
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0003D0C2 File Offset: 0x0003B2C2
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009CA RID: 2506
		private readonly RelProperty m_property;

		// Token: 0x040009CB RID: 2507
		internal static readonly NavigateOp Pattern = new NavigateOp();
	}
}
