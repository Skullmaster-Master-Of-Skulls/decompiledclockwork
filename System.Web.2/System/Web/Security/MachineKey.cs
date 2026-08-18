using System;
using System.Linq;
using System.Web.Configuration;
using System.Web.Security.Cryptography;
using System.Web.Util;

namespace System.Web.Security
{
	// Token: 0x020005E7 RID: 1511
	public static class MachineKey
	{
		// Token: 0x06004C28 RID: 19496 RVA: 0x00104214 File Offset: 0x00102414
		[Obsolete("This method is obsolete and is only provided for compatibility with existing code. It is recommended that new code use the Protect and Unprotect methods instead.")]
		public static string Encode(byte[] data, MachineKeyProtection protectionOption)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (protectionOption == MachineKeyProtection.All || protectionOption == MachineKeyProtection.Validation)
			{
				byte[] array = MachineKeySection.HashData(data, null, 0, data.Length);
				byte[] array2 = new byte[array.Length + data.Length];
				Buffer.BlockCopy(data, 0, array2, 0, data.Length);
				Buffer.BlockCopy(array, 0, array2, data.Length, array.Length);
				data = array2;
			}
			if (protectionOption == MachineKeyProtection.All || protectionOption == MachineKeyProtection.Encryption)
			{
				data = MachineKeySection.EncryptOrDecryptData(true, data, null, 0, data.Length, false, false, IVType.Random, !AppSettings.UseLegacyMachineKeyEncryption);
			}
			return CryptoUtil.BinaryToHex(data);
		}

		// Token: 0x06004C29 RID: 19497 RVA: 0x00104294 File Offset: 0x00102494
		[Obsolete("This method is obsolete and is only provided for compatibility with existing code. It is recommended that new code use the Protect and Unprotect methods instead.")]
		public static byte[] Decode(string encodedData, MachineKeyProtection protectionOption)
		{
			if (encodedData == null)
			{
				throw new ArgumentNullException("encodedData");
			}
			if (encodedData.Length % 2 != 0)
			{
				throw new ArgumentException(null, "encodedData");
			}
			byte[] array = null;
			try
			{
				array = CryptoUtil.HexToBinary(encodedData);
			}
			catch
			{
				throw new ArgumentException(null, "encodedData");
			}
			if (array == null || array.Length < 1)
			{
				throw new ArgumentException(null, "encodedData");
			}
			if (protectionOption == MachineKeyProtection.All || protectionOption == MachineKeyProtection.Encryption)
			{
				array = MachineKeySection.EncryptOrDecryptData(false, array, null, 0, array.Length, false, false, IVType.Random, !AppSettings.UseLegacyMachineKeyEncryption);
				if (array == null)
				{
					return null;
				}
			}
			if (protectionOption == MachineKeyProtection.All || protectionOption == MachineKeyProtection.Validation)
			{
				if (array.Length < MachineKeySection.HashSize)
				{
					return null;
				}
				byte[] array2 = array;
				array = new byte[array2.Length - MachineKeySection.HashSize];
				Buffer.BlockCopy(array2, 0, array, 0, array.Length);
				byte[] array3 = MachineKeySection.HashData(array, null, 0, array.Length);
				if (array3 == null || array3.Length != MachineKeySection.HashSize)
				{
					return null;
				}
				for (int i = 0; i < array3.Length; i++)
				{
					if (array3[i] != array2[array.Length + i])
					{
						return null;
					}
				}
			}
			return array;
		}

		// Token: 0x06004C2A RID: 19498 RVA: 0x00104390 File Offset: 0x00102590
		public static byte[] Protect(byte[] userData, params string[] purposes)
		{
			if (userData == null)
			{
				throw new ArgumentNullException("userData");
			}
			if (purposes != null && purposes.Any(new Func<string, bool>(string.IsNullOrWhiteSpace)))
			{
				throw new ArgumentException(SR.GetString("MachineKey_InvalidPurpose"), "purposes");
			}
			return MachineKey.Protect(AspNetCryptoServiceProvider.Instance, userData, purposes);
		}

		// Token: 0x06004C2B RID: 19499 RVA: 0x001043E4 File Offset: 0x001025E4
		internal static byte[] Protect(ICryptoServiceProvider cryptoServiceProvider, byte[] userData, string[] purposes)
		{
			Purpose purpose = Purpose.User_MachineKey_Protect.AppendSpecificPurposes(purposes);
			ICryptoService cryptoService = cryptoServiceProvider.GetCryptoService(purpose, CryptoServiceOptions.None);
			return cryptoService.Protect(userData);
		}

		// Token: 0x06004C2C RID: 19500 RVA: 0x00104410 File Offset: 0x00102610
		public static byte[] Unprotect(byte[] protectedData, params string[] purposes)
		{
			if (protectedData == null)
			{
				throw new ArgumentNullException("protectedData");
			}
			if (purposes != null && purposes.Any(new Func<string, bool>(string.IsNullOrWhiteSpace)))
			{
				throw new ArgumentException(SR.GetString("MachineKey_InvalidPurpose"), "purposes");
			}
			return MachineKey.Unprotect(AspNetCryptoServiceProvider.Instance, protectedData, purposes);
		}

		// Token: 0x06004C2D RID: 19501 RVA: 0x00104464 File Offset: 0x00102664
		internal static byte[] Unprotect(ICryptoServiceProvider cryptoServiceProvider, byte[] protectedData, string[] purposes)
		{
			Purpose purpose = Purpose.User_MachineKey_Protect.AppendSpecificPurposes(purposes);
			ICryptoService cryptoService = cryptoServiceProvider.GetCryptoService(purpose, CryptoServiceOptions.None);
			return cryptoService.Unprotect(protectedData);
		}
	}
}
