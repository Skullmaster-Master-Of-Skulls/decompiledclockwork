using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001D9 RID: 473
	internal struct EdmFunctionPayload
	{
		// Token: 0x04000E25 RID: 3621
		public string Name;

		// Token: 0x04000E26 RID: 3622
		public string NamespaceName;

		// Token: 0x04000E27 RID: 3623
		public string Schema;

		// Token: 0x04000E28 RID: 3624
		public string StoreFunctionName;

		// Token: 0x04000E29 RID: 3625
		public string CommandText;

		// Token: 0x04000E2A RID: 3626
		public EntitySet[] EntitySets;

		// Token: 0x04000E2B RID: 3627
		public bool? IsAggregate;

		// Token: 0x04000E2C RID: 3628
		public bool? IsBuiltIn;

		// Token: 0x04000E2D RID: 3629
		public bool? IsNiladic;

		// Token: 0x04000E2E RID: 3630
		public bool? IsComposable;

		// Token: 0x04000E2F RID: 3631
		public bool? IsFromProviderManifest;

		// Token: 0x04000E30 RID: 3632
		public bool? IsCachedStoreFunction;

		// Token: 0x04000E31 RID: 3633
		public bool? IsFunctionImport;

		// Token: 0x04000E32 RID: 3634
		public FunctionParameter[] ReturnParameters;

		// Token: 0x04000E33 RID: 3635
		public ParameterTypeSemantics? ParameterTypeSemantics;

		// Token: 0x04000E34 RID: 3636
		public FunctionParameter[] Parameters;

		// Token: 0x04000E35 RID: 3637
		public DataSpace DataSpace;
	}
}
