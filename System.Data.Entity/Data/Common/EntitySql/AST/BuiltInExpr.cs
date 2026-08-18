using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000365 RID: 869
	internal sealed class BuiltInExpr : Node
	{
		// Token: 0x060031FC RID: 12796 RVA: 0x000C4A91 File Offset: 0x000C2C91
		private BuiltInExpr(BuiltInKind kind, string name)
		{
			this.Kind = kind;
			this.Name = name.ToUpperInvariant();
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x000C4AAC File Offset: 0x000C2CAC
		internal BuiltInExpr(BuiltInKind kind, string name, Node arg1) : this(kind, name)
		{
			this.ArgCount = 1;
			this.Arg1 = arg1;
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x000C4AC4 File Offset: 0x000C2CC4
		internal BuiltInExpr(BuiltInKind kind, string name, Node arg1, Node arg2) : this(kind, name)
		{
			this.ArgCount = 2;
			this.Arg1 = arg1;
			this.Arg2 = arg2;
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x000C4AE4 File Offset: 0x000C2CE4
		internal BuiltInExpr(BuiltInKind kind, string name, Node arg1, Node arg2, Node arg3) : this(kind, name)
		{
			this.ArgCount = 3;
			this.Arg1 = arg1;
			this.Arg2 = arg2;
			this.Arg3 = arg3;
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x000C4B0C File Offset: 0x000C2D0C
		internal BuiltInExpr(BuiltInKind kind, string name, Node arg1, Node arg2, Node arg3, Node arg4) : this(kind, name)
		{
			this.ArgCount = 4;
			this.Arg1 = arg1;
			this.Arg2 = arg2;
			this.Arg3 = arg3;
			this.Arg4 = arg4;
		}

		// Token: 0x040015E9 RID: 5609
		internal readonly BuiltInKind Kind;

		// Token: 0x040015EA RID: 5610
		internal readonly string Name;

		// Token: 0x040015EB RID: 5611
		internal readonly int ArgCount;

		// Token: 0x040015EC RID: 5612
		internal readonly Node Arg1;

		// Token: 0x040015ED RID: 5613
		internal readonly Node Arg2;

		// Token: 0x040015EE RID: 5614
		internal readonly Node Arg3;

		// Token: 0x040015EF RID: 5615
		internal readonly Node Arg4;
	}
}
