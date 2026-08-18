using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000039 RID: 57
	public interface IPasswordHasher
	{
		// Token: 0x060000EC RID: 236
		string HashPassword(string password);

		// Token: 0x060000ED RID: 237
		PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword);
	}
}
