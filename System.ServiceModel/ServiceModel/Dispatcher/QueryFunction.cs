using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200049C RID: 1180
	internal abstract class QueryFunction
	{
		// Token: 0x06002D36 RID: 11574 RVA: 0x000B04FE File Offset: 0x000AE6FE
		internal QueryFunction(string name, ValueDataType returnType) : this(name, returnType, QueryFunction.emptyParams, QueryFunctionFlag.None)
		{
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x000B050E File Offset: 0x000AE70E
		internal QueryFunction(string name, ValueDataType returnType, QueryFunctionFlag flags) : this(name, returnType, QueryFunction.emptyParams, flags)
		{
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x000B051E File Offset: 0x000AE71E
		internal QueryFunction(string name, ValueDataType returnType, ValueDataType[] paramTypes) : this(name, returnType, paramTypes, QueryFunctionFlag.None)
		{
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x000B052A File Offset: 0x000AE72A
		internal QueryFunction(string name, ValueDataType returnType, ValueDataType[] paramTypes, QueryFunctionFlag flags)
		{
			this.name = name;
			this.returnType = returnType;
			this.paramTypes = paramTypes;
			this.flags = flags;
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06002D3A RID: 11578 RVA: 0x000B054F File Offset: 0x000AE74F
		internal ValueDataType[] ParamTypes
		{
			get
			{
				return this.paramTypes;
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06002D3B RID: 11579 RVA: 0x000B0557 File Offset: 0x000AE757
		internal ValueDataType ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x000B055F File Offset: 0x000AE75F
		internal bool Bind(string name, XPathExprList args)
		{
			return string.CompareOrdinal(this.name, name) == 0 && this.paramTypes.Length == args.Count && this.paramTypes.Length == args.Count;
		}

		// Token: 0x06002D3D RID: 11581
		internal abstract bool Equals(QueryFunction function);

		// Token: 0x06002D3E RID: 11582
		internal abstract void Eval(ProcessingContext context);

		// Token: 0x06002D3F RID: 11583 RVA: 0x000B0591 File Offset: 0x000AE791
		internal bool TestFlag(QueryFunctionFlag flag)
		{
			return (this.flags & flag) > QueryFunctionFlag.None;
		}

		// Token: 0x0400249E RID: 9374
		private static ValueDataType[] emptyParams = new ValueDataType[0];

		// Token: 0x0400249F RID: 9375
		private QueryFunctionFlag flags;

		// Token: 0x040024A0 RID: 9376
		protected string name;

		// Token: 0x040024A1 RID: 9377
		private ValueDataType[] paramTypes;

		// Token: 0x040024A2 RID: 9378
		private ValueDataType returnType;
	}
}
