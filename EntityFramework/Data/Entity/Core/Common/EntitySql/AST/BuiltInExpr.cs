using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000215 RID: 533
	internal sealed class BuiltInExpr : Node
	{
		// Token: 0x0600135B RID: 4955 RVA: 0x000503C8 File Offset: 0x0004E5C8
		private BuiltInExpr(BuiltInKind kind, string name)
		{
			this.Kind = kind;
			this.Name = name.ToUpperInvariant();
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x000503E3 File Offset: 0x0004E5E3
		internal BuiltInExpr(BuiltInKind kind, string name, Node arg1) : this(kind, name)
		{
			this.ArgCount = 1;
			this.Arg1 = arg1;
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x000503FB File Offset: 0x0004E5FB
		internal BuiltInExpr(BuiltInKind kind, string name, Node arg1, Node arg2) : this(kind, name)
		{
			this.ArgCount = 2;
			this.Arg1 = arg1;
			this.Arg2 = arg2;
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0005041B File Offset: 0x0004E61B
		internal BuiltInExpr(BuiltInKind kind, string name, Node arg1, Node arg2, Node arg3) : this(kind, name)
		{
			this.ArgCount = 3;
			this.Arg1 = arg1;
			this.Arg2 = arg2;
			this.Arg3 = arg3;
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00050443 File Offset: 0x0004E643
		internal BuiltInExpr(BuiltInKind kind, string name, Node arg1, Node arg2, Node arg3, Node arg4) : this(kind, name)
		{
			this.ArgCount = 4;
			this.Arg1 = arg1;
			this.Arg2 = arg2;
			this.Arg3 = arg3;
			this.Arg4 = arg4;
		}

		// Token: 0x040005A7 RID: 1447
		internal readonly BuiltInKind Kind;

		// Token: 0x040005A8 RID: 1448
		internal readonly string Name;

		// Token: 0x040005A9 RID: 1449
		internal readonly int ArgCount;

		// Token: 0x040005AA RID: 1450
		internal readonly Node Arg1;

		// Token: 0x040005AB RID: 1451
		internal readonly Node Arg2;

		// Token: 0x040005AC RID: 1452
		internal readonly Node Arg3;

		// Token: 0x040005AD RID: 1453
		internal readonly Node Arg4;
	}
}
