using System;
using Microsoft.Owin.Security.DataProtection;

namespace Microsoft.AspNet.Identity.Owin
{
	// Token: 0x02000008 RID: 8
	public class DataProtectorTokenProvider<TUser> : DataProtectorTokenProvider<TUser, string> where TUser : class, IUser<string>
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002D73 File Offset: 0x00000F73
		public DataProtectorTokenProvider(IDataProtector protector) : base(protector)
		{
		}
	}
}
