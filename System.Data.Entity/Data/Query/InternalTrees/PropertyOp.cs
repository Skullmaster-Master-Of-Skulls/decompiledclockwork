using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000F2 RID: 242
	internal sealed class PropertyOp : ScalarOp
	{
		// Token: 0x06000CFC RID: 3324 RVA: 0x0003CA4E File Offset: 0x0003AC4E
		internal PropertyOp(TypeUsage type, EdmMember property) : base(OpType.Property, type)
		{
			this.m_property = property;
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0003CA60 File Offset: 0x0003AC60
		private PropertyOp() : base(OpType.Property)
		{
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x0003CA6A File Offset: 0x0003AC6A
		internal EdmMember PropertyInfo
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0003CA72 File Offset: 0x0003AC72
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0003CA7C File Offset: 0x0003AC7C
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009A3 RID: 2467
		private EdmMember m_property;

		// Token: 0x040009A4 RID: 2468
		internal static readonly PropertyOp Pattern = new PropertyOp();
	}
}
