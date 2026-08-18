using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000643 RID: 1603
	internal sealed class NavigateOp : ScalarOp
	{
		// Token: 0x06003EDA RID: 16090 RVA: 0x001202AB File Offset: 0x0011E4AB
		internal NavigateOp(TypeUsage type, RelProperty relProperty) : base(OpType.Navigate, type)
		{
			this.m_property = relProperty;
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x001202BD File Offset: 0x0011E4BD
		private NavigateOp() : base(OpType.Navigate)
		{
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06003EDC RID: 16092 RVA: 0x001202C7 File Offset: 0x0011E4C7
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06003EDD RID: 16093 RVA: 0x001202CA File Offset: 0x0011E4CA
		internal RelProperty RelProperty
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06003EDE RID: 16094 RVA: 0x001202D2 File Offset: 0x0011E4D2
		internal RelationshipType Relationship
		{
			get
			{
				return this.m_property.Relationship;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06003EDF RID: 16095 RVA: 0x001202DF File Offset: 0x0011E4DF
		internal RelationshipEndMember FromEnd
		{
			get
			{
				return this.m_property.FromEnd;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06003EE0 RID: 16096 RVA: 0x001202EC File Offset: 0x0011E4EC
		internal RelationshipEndMember ToEnd
		{
			get
			{
				return this.m_property.ToEnd;
			}
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x001202F9 File Offset: 0x0011E4F9
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003EE2 RID: 16098 RVA: 0x00120303 File Offset: 0x0011E503
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001781 RID: 6017
		private readonly RelProperty m_property;

		// Token: 0x04001782 RID: 6018
		internal static readonly NavigateOp Pattern = new NavigateOp();
	}
}
