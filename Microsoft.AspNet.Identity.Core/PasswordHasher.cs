using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200003B RID: 59
	public class PasswordHasher : IPasswordHasher
	{
		// Token: 0x060000EE RID: 238 RVA: 0x000066C4 File Offset: 0x000048C4
		public virtual string HashPassword(string password)
		{
			return Crypto.HashPassword(password);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000066CC File Offset: 0x000048CC
		public virtual PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
		{
			if (Crypto.VerifyHashedPassword(hashedPassword, providedPassword))
			{
				return PasswordVerificationResult.Success;
			}
			return PasswordVerificationResult.Failed;
		}
	}
}
