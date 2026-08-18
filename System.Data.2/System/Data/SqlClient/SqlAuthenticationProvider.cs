using System;
using System.Threading.Tasks;

namespace System.Data.SqlClient
{
	// Token: 0x020001D8 RID: 472
	public abstract class SqlAuthenticationProvider
	{
		// Token: 0x06001DF1 RID: 7665 RVA: 0x000D3020 File Offset: 0x000D2420
		public static SqlAuthenticationProvider GetProvider(SqlAuthenticationMethod authenticationMethod)
		{
			return SqlAuthenticationProviderManager.Instance.GetProvider(authenticationMethod);
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x000D3038 File Offset: 0x000D2438
		public static bool SetProvider(SqlAuthenticationMethod authenticationMethod, SqlAuthenticationProvider provider)
		{
			return SqlAuthenticationProviderManager.Instance.SetProvider(authenticationMethod, provider);
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x000D3054 File Offset: 0x000D2454
		public virtual void BeforeLoad(SqlAuthenticationMethod authenticationMethod)
		{
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x000D3064 File Offset: 0x000D2464
		public virtual void BeforeUnload(SqlAuthenticationMethod authenticationMethod)
		{
		}

		// Token: 0x06001DF5 RID: 7669
		public abstract bool IsSupported(SqlAuthenticationMethod authenticationMethod);

		// Token: 0x06001DF6 RID: 7670
		public abstract Task<SqlAuthenticationToken> AcquireTokenAsync(SqlAuthenticationParameters parameters);
	}
}
