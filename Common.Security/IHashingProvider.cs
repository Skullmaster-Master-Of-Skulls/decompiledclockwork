using System;

namespace TechnoPro.Common.Security.Hashing
{
	// Token: 0x02000007 RID: 7
	public interface IHashingProvider
	{
		// Token: 0x06000012 RID: 18
		string CreateHash(string password, PasswordHashContext context = null);

		// Token: 0x06000013 RID: 19
		bool ValidatePassword(string password, string correctHash, PasswordHashContext context = null);
	}
}
