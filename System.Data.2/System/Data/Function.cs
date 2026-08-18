using System;

namespace System.Data
{
	// Token: 0x020000F6 RID: 246
	internal sealed class Function
	{
		// Token: 0x06000FF1 RID: 4081 RVA: 0x0007FD68 File Offset: 0x0007F168
		internal Function()
		{
			this.name = null;
			this.id = FunctionId.none;
			this.result = null;
			this.IsValidateArguments = false;
			this.argumentCount = 0;
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0007FDAC File Offset: 0x0007F1AC
		internal Function(string name, FunctionId id, Type result, bool IsValidateArguments, bool IsVariantArgumentList, int argumentCount, Type a1, Type a2, Type a3)
		{
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

		// Token: 0x0400050F RID: 1295
		internal readonly string name;

		// Token: 0x04000510 RID: 1296
		internal readonly FunctionId id;

		// Token: 0x04000511 RID: 1297
		internal readonly Type result;

		// Token: 0x04000512 RID: 1298
		internal readonly bool IsValidateArguments;

		// Token: 0x04000513 RID: 1299
		internal readonly bool IsVariantArgumentList;

		// Token: 0x04000514 RID: 1300
		internal readonly int argumentCount;

		// Token: 0x04000515 RID: 1301
		internal readonly Type[] parameters = new Type[3];

		// Token: 0x04000516 RID: 1302
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
