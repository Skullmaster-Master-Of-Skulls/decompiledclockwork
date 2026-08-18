using System;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x0200039B RID: 923
	public class ClaimTypeRequirement
	{
		// Token: 0x06002268 RID: 8808 RVA: 0x0007D90F File Offset: 0x0007BB0F
		public ClaimTypeRequirement(string claimType) : this(claimType, false)
		{
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x0007D91C File Offset: 0x0007BB1C
		public ClaimTypeRequirement(string claimType, bool isOptional)
		{
			if (claimType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claimType");
			}
			if (claimType.Length <= 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("claimType", SR.GetString("ClaimTypeCannotBeEmpty"));
			}
			this.claimType = claimType;
			this.isOptional = isOptional;
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x0007D973 File Offset: 0x0007BB73
		public string ClaimType
		{
			get
			{
				return this.claimType;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x0600226B RID: 8811 RVA: 0x0007D97B File Offset: 0x0007BB7B
		public bool IsOptional
		{
			get
			{
				return this.isOptional;
			}
		}

		// Token: 0x04001FA0 RID: 8096
		internal const bool DefaultIsOptional = false;

		// Token: 0x04001FA1 RID: 8097
		private string claimType;

		// Token: 0x04001FA2 RID: 8098
		private bool isOptional;
	}
}
