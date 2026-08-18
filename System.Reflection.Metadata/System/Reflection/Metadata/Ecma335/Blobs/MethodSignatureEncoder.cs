using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x02000125 RID: 293
	internal struct MethodSignatureEncoder
	{
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x0001CD01 File Offset: 0x0001AF01
		public BlobBuilder Builder { get; }

		// Token: 0x060009BE RID: 2494 RVA: 0x0001CD09 File Offset: 0x0001AF09
		public MethodSignatureEncoder(BlobBuilder builder, bool isVarArg)
		{
			this.Builder = builder;
			this._isVarArg = isVarArg;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0001CD19 File Offset: 0x0001AF19
		public void Parameters(int parameterCount, out ReturnTypeEncoder returnType, out ParametersEncoder parameters)
		{
			this.Builder.WriteCompressedInteger(parameterCount);
			returnType = new ReturnTypeEncoder(this.Builder);
			parameters = new ParametersEncoder(this.Builder, this._isVarArg);
		}

		// Token: 0x04000898 RID: 2200
		private readonly bool _isVarArg;
	}
}
