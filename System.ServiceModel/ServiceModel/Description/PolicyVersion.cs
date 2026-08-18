using System;

namespace System.ServiceModel.Description
{
	// Token: 0x0200041B RID: 1051
	public sealed class PolicyVersion
	{
		// Token: 0x06002839 RID: 10297 RVA: 0x0009727A File Offset: 0x0009547A
		private PolicyVersion(string policyNamespace)
		{
			this.policyNamespace = policyNamespace;
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x0600283A RID: 10298 RVA: 0x00097289 File Offset: 0x00095489
		public static PolicyVersion Policy12
		{
			get
			{
				return PolicyVersion.policyVersion12;
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x0600283B RID: 10299 RVA: 0x00097290 File Offset: 0x00095490
		public static PolicyVersion Policy15
		{
			get
			{
				return PolicyVersion.policyVersion15;
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x0600283C RID: 10300 RVA: 0x00097297 File Offset: 0x00095497
		public static PolicyVersion Default
		{
			get
			{
				return PolicyVersion.policyVersion12;
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x0600283D RID: 10301 RVA: 0x0009729E File Offset: 0x0009549E
		public string Namespace
		{
			get
			{
				return this.policyNamespace;
			}
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x000972A6 File Offset: 0x000954A6
		public override string ToString()
		{
			return this.policyNamespace;
		}

		// Token: 0x0400221E RID: 8734
		private string policyNamespace;

		// Token: 0x0400221F RID: 8735
		private static PolicyVersion policyVersion12 = new PolicyVersion("http://schemas.xmlsoap.org/ws/2004/09/policy");

		// Token: 0x04002220 RID: 8736
		private static PolicyVersion policyVersion15 = new PolicyVersion("http://www.w3.org/ns/ws-policy");
	}
}
