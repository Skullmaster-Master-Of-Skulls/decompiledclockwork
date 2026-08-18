using System;
using System.Security.Cryptography;

namespace Telerik.Web.UI.Common
{
	// Token: 0x02000091 RID: 145
	internal sealed class HmacEnabledCryptoService : IHmacEnabledService
	{
		// Token: 0x06000580 RID: 1408 RVA: 0x0000DA80 File Offset: 0x0000BC80
		public HmacEnabledCryptoService(ICryptoService cryptoService, IHmacService hmacService)
		{
			this.CryptoService = cryptoService;
			this.HmacService = hmacService;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000DAB4 File Offset: 0x0000BCB4
		public string Encrypt(string input)
		{
			return HmacEnabledCryptoService.exceptionThrower.ThrowIfFails<string>(() => this.EncryptAndHash(input));
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000DAEC File Offset: 0x0000BCEC
		private string EncryptAndHash(string input)
		{
			string text = this.CryptoService.Encrypt(input);
			string str = this.HmacService.HMAC256(text);
			return text + str;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000DB38 File Offset: 0x0000BD38
		public string Decrypt(string input)
		{
			return HmacEnabledCryptoService.exceptionThrower.ThrowIfFails<string>(() => this.DecryptHashed(input));
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000DB70 File Offset: 0x0000BD70
		private string DecryptHashed(string input)
		{
			if (!this.IsValidHMac(input))
			{
				throw new CryptographicException("The hash is not valid!");
			}
			string encryptedText = this.GetEncryptedText(input);
			return this.CryptoService.Decrypt(encryptedText);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000DBA5 File Offset: 0x0000BDA5
		private bool IsValidHMac(string input)
		{
			return this.HmacService.HMAC256(this.GetEncryptedText(input)) == this.GetHash(input);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000DBC5 File Offset: 0x0000BDC5
		private string GetHash(string input)
		{
			return input.Substring(input.Length - this.HmacService.GetHmacLength());
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000DBDF File Offset: 0x0000BDDF
		private string GetEncryptedText(string input)
		{
			return input.Substring(0, input.Length - this.HmacService.GetHmacLength());
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0000DBFC File Offset: 0x0000BDFC
		public static IHmacEnabledService GetService(string cryptoServiceOwner = "")
		{
			if (HmacEnabledCryptoService.service == null)
			{
				lock (HmacEnabledCryptoService.serviceLock)
				{
					if (HmacEnabledCryptoService.service == null)
					{
						HmacEnabledCryptoService.service = new HmacEnabledCryptoService(Telerik.Web.UI.CryptoService.GetService(cryptoServiceOwner), Telerik.Web.UI.Common.HmacService.GetService());
					}
				}
			}
			return HmacEnabledCryptoService.service;
		}

		// Token: 0x040000BC RID: 188
		private static readonly object serviceLock = new object();

		// Token: 0x040000BD RID: 189
		private static IHmacEnabledService service;

		// Token: 0x040000BE RID: 190
		private readonly ICryptoService CryptoService;

		// Token: 0x040000BF RID: 191
		private readonly IHmacService HmacService;

		// Token: 0x040000C0 RID: 192
		private static readonly ICryptoExceptionThrower exceptionThrower = new CryptoExceptionThrower();
	}
}
