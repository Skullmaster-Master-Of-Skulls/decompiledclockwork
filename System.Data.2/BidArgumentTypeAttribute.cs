using System;
using System.Diagnostics;

// Token: 0x02000032 RID: 50
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = true)]
[Conditional("CODE_ANALYSIS")]
internal sealed class BidArgumentTypeAttribute : Attribute
{
	// Token: 0x0600012C RID: 300 RVA: 0x000385FC File Offset: 0x000379FC
	internal BidArgumentTypeAttribute(Type bidArgumentType)
	{
		this.ArgumentType = bidArgumentType;
		this.Index = -1;
	}

	// Token: 0x0600012D RID: 301 RVA: 0x00038620 File Offset: 0x00037A20
	internal BidArgumentTypeAttribute(Type bidArgumentType, int index)
	{
		this.ArgumentType = bidArgumentType;
		this.Index = index;
	}

	// Token: 0x040000C2 RID: 194
	public readonly Type ArgumentType;

	// Token: 0x040000C3 RID: 195
	public readonly int Index;
}
