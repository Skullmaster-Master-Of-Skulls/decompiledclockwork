using System;

namespace System.Data
{
	// Token: 0x020001B4 RID: 436
	internal sealed class Function
	{
		// Token: 0x06001907 RID: 6407 RVA: 0x00257CD8 File Offset: 0x002570D8
		internal Function()
		{
			Type[] array = new Type[3];
			this.parameters = array;
			base..ctor();
			this.name = null;
			this.id = FunctionId.none;
			this.result = null;
			this.IsValidateArguments = false;
			this.argumentCount = 0;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00257D28 File Offset: 0x00257128
		internal Function(string name, FunctionId id, Type result, bool IsValidateArguments, bool IsVariantArgumentList, int argumentCount, Type a1, Type a2, Type a3)
		{
			Type[] array = new Type[3];
			this.parameters = array;
			base..ctor();
			this.name = name;
			this.id = id;
			this.result = result;
			this.IsValidateArguments = IsValidateArguments;
			this.IsVariantArgumentList = IsVariantArgumentList;
			this.argumentCount = argumentCount;
			if (a1 != null)
			{
				this.parameters[0] = a1;
			}
			if (a2 != null)
			{
				this.parameters[1] = a2;
			}
			if (a3 != null)
			{
				this.parameters[2] = a3;
			}
		}

		// Token: 0x04000DE0 RID: 3552
		internal readonly string name;

		// Token: 0x04000DE1 RID: 3553
		internal readonly FunctionId id;

		// Token: 0x04000DE2 RID: 3554
		internal readonly Type result;

		// Token: 0x04000DE3 RID: 3555
		internal readonly bool IsValidateArguments;

		// Token: 0x04000DE4 RID: 3556
		internal readonly bool IsVariantArgumentList;

		// Token: 0x04000DE5 RID: 3557
		internal readonly int argumentCount;

		// Token: 0x04000DE6 RID: 3558
		internal readonly Type[] parameters;

		// Token: 0x04000DE7 RID: 3559
		internal static string[] FunctionName = new string[]
		{
			"Unknown",
			"Ascii",
			"Char",
			"CharIndex",
			"Difference",
			"Len",
			"Lower",
			"LTrim",
			"Patindex",
			"Replicate",
			"Reverse",
			"Right",
			"RTrim",
			"Soundex",
			"Space",
			"Str",
			"Stuff",
			"Substring",
			"Upper",
			"IsNull",
			"Iif",
			"Convert",
			"cInt",
			"cBool",
			"cDate",
			"cDbl",
			"cStr",
			"Abs",
			"Acos",
			"In",
			"Trim",
			"Sum",
			"Avg",
			"Min",
			"Max",
			"Count",
			"StDev",
			"Var",
			"DateTimeOffset"
		};
	}
}
