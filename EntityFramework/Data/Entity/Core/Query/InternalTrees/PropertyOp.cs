using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000615 RID: 1557
	internal sealed class PropertyOp : ScalarOp
	{
		// Token: 0x06003D17 RID: 15639 RVA: 0x0011AD0A File Offset: 0x00118F0A
		internal PropertyOp(TypeUsage type, EdmMember property) : base(OpType.Property, type)
		{
			this.m_property = property;
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x0011AD1C File Offset: 0x00118F1C
		private PropertyOp() : base(OpType.Property)
		{
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06003D19 RID: 15641 RVA: 0x0011AD26 File Offset: 0x00118F26
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06003D1A RID: 15642 RVA: 0x0011AD29 File Offset: 0x00118F29
		internal EdmMember PropertyInfo
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x0011AD31 File Offset: 0x00118F31
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003D1C RID: 15644 RVA: 0x0011AD3B File Offset: 0x00118F3B
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x0011AD48 File Offset: 0x00118F48
		internal override bool IsEquivalent(Op other)
		{
			PropertyOp propertyOp = other as PropertyOp;
			return propertyOp != null && propertyOp.PropertyInfo.EdmEquals(this.PropertyInfo) && base.IsEquivalent(other);
		}

		// Token: 0x04001719 RID: 5913
		private readonly EdmMember m_property;

		// Token: 0x0400171A RID: 5914
		internal static readonly PropertyOp Pattern = new PropertyOp();
	}
}
