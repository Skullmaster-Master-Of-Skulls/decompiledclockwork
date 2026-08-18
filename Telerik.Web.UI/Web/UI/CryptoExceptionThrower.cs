using System;
using System.Security.Cryptography;

namespace Telerik.Web.UI
{
	// Token: 0x0200008F RID: 143
	internal class CryptoExceptionThrower : ICryptoExceptionThrower
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x0000DA36 File Offset: 0x0000BC36
		public T ThrowGenericCryptoException<T>()
		{
			throw new CryptographicException("The cryptographic operation has failed!");
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0000DA44 File Offset: 0x0000BC44
		public T ThrowIfFails<T>(Func<T> function)
		{
			T result;
			try
			{
				result = function();
			}
			catch (Exception)
			{
				result = this.ThrowGenericCryptoException<T>();
			}
			return result;
		}

		// Token: 0x040000BB RID: 187
		private const string GENERIC_CRYPTO_ERROR = "The cryptographic operation has failed!";
	}
}
