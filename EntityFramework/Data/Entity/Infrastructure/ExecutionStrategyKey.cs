using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000288 RID: 648
	public class ExecutionStrategyKey
	{
		// Token: 0x060016BE RID: 5822 RVA: 0x0006F5D9 File Offset: 0x0006D7D9
		public ExecutionStrategyKey(string providerInvariantName, string serverName)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			this.ProviderInvariantName = providerInvariantName;
			this.ServerName = serverName;
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x0006F5FB File Offset: 0x0006D7FB
		// (set) Token: 0x060016C0 RID: 5824 RVA: 0x0006F603 File Offset: 0x0006D803
		public string ProviderInvariantName { get; private set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x0006F60C File Offset: 0x0006D80C
		// (set) Token: 0x060016C2 RID: 5826 RVA: 0x0006F614 File Offset: 0x0006D814
		public string ServerName { get; private set; }

		// Token: 0x060016C3 RID: 5827 RVA: 0x0006F620 File Offset: 0x0006D820
		public override bool Equals(object obj)
		{
			ExecutionStrategyKey executionStrategyKey = obj as ExecutionStrategyKey;
			return !object.ReferenceEquals(executionStrategyKey, null) && this.ProviderInvariantName.Equals(executionStrategyKey.ProviderInvariantName, StringComparison.Ordinal) && ((this.ServerName == null && executionStrategyKey.ServerName == null) || (this.ServerName != null && this.ServerName.Equals(executionStrategyKey.ServerName, StringComparison.Ordinal)));
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0006F683 File Offset: 0x0006D883
		public override int GetHashCode()
		{
			if (this.ServerName != null)
			{
				return this.ProviderInvariantName.GetHashCode() ^ this.ServerName.GetHashCode();
			}
			return this.ProviderInvariantName.GetHashCode();
		}
	}
}
