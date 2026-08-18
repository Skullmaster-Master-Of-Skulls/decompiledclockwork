using System;
using System.Web.Configuration;
using System.Web.Security.Cryptography;

namespace System.Web.Security
{
	// Token: 0x020005D9 RID: 1497
	internal class CookieProtectionHelper
	{
		// Token: 0x06004BA9 RID: 19369 RVA: 0x00101928 File Offset: 0x000FFB28
		internal static string Encode(CookieProtection cookieProtection, byte[] buf, Purpose purpose)
		{
			if (AspNetCryptoServiceProvider.Instance.IsDefaultProvider)
			{
				ICryptoService cryptoService = AspNetCryptoServiceProvider.Instance.GetCryptoService(purpose, CryptoServiceOptions.None);
				return HttpServerUtility.UrlTokenEncode(cryptoService.Protect(buf));
			}
			int num = buf.Length;
			if (cookieProtection == CookieProtection.All || cookieProtection == CookieProtection.Validation)
			{
				byte[] array = MachineKeySection.HashData(buf, null, 0, num);
				if (array == null)
				{
					return null;
				}
				if (buf.Length >= num + array.Length)
				{
					Buffer.BlockCopy(array, 0, buf, num, array.Length);
				}
				else
				{
					byte[] src = buf;
					buf = new byte[num + array.Length];
					Buffer.BlockCopy(src, 0, buf, 0, num);
					Buffer.BlockCopy(array, 0, buf, num, array.Length);
				}
				num += array.Length;
			}
			if (cookieProtection == CookieProtection.All || cookieProtection == CookieProtection.Encryption)
			{
				buf = MachineKeySection.EncryptOrDecryptData(true, buf, null, 0, num);
				num = buf.Length;
			}
			if (num < buf.Length)
			{
				byte[] src2 = buf;
				buf = new byte[num];
				Buffer.BlockCopy(src2, 0, buf, 0, num);
			}
			return HttpServerUtility.UrlTokenEncode(buf);
		}

		// Token: 0x06004BAA RID: 19370 RVA: 0x001019F4 File Offset: 0x000FFBF4
		internal static byte[] Decode(CookieProtection cookieProtection, string data, Purpose purpose)
		{
			byte[] array = HttpServerUtility.UrlTokenDecode(data);
			if (AspNetCryptoServiceProvider.Instance.IsDefaultProvider)
			{
				ICryptoService cryptoService = AspNetCryptoServiceProvider.Instance.GetCryptoService(purpose, CryptoServiceOptions.None);
				return cryptoService.Unprotect(array);
			}
			if (array == null || cookieProtection == CookieProtection.None)
			{
				return array;
			}
			if (cookieProtection == CookieProtection.All || cookieProtection == CookieProtection.Encryption)
			{
				array = MachineKeySection.EncryptOrDecryptData(false, array, null, 0, array.Length);
				if (array == null)
				{
					return null;
				}
			}
			if (cookieProtection == CookieProtection.All || cookieProtection == CookieProtection.Validation)
			{
				return MachineKeySection.GetUnHashedData(array);
			}
			return array;
		}
	}
}
