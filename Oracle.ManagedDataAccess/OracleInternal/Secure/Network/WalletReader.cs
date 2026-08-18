using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using \u0005;

namespace OracleInternal.Secure.Network
{
	// Token: 0x02000359 RID: 857
	internal class WalletReader
	{
		// Token: 0x06001E38 RID: 7736 RVA: 0x00126078 File Offset: 0x00124278
		internal static byte[] ReadWallet(string WD, ref string WP)
		{
			return WalletReader.\u0001(WD, ref WP);
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x00126084 File Offset: 0x00124284
		internal static byte[] \u0001(string \u0002, ref string \u0003)
		{
			if (!string.IsNullOrEmpty(\u0003))
			{
				return WalletReader.\u0001(\u0002 + (\u0002.EndsWith(global::\u0005.\u0001.\u0001(679)) ? global::\u0005.\u0001.\u0001(684) : global::\u0005.\u0001.\u0001(679)) + global::\u0005.\u0001.\u0001(685));
			}
			byte[] array = WalletReader.\u0001(\u0002 + (\u0002.EndsWith(global::\u0005.\u0001.\u0001(679)) ? global::\u0005.\u0001.\u0001(684) : global::\u0005.\u0001.\u0001(679)) + global::\u0005.\u0001.\u0001(702));
			if (array.Length < WalletReader.\u000F + WalletReader.\u0010)
			{
				throw new Exception(global::\u0005.\u0001.\u0001(719));
			}
			for (int i = 0; i < 4; i++)
			{
				if (array[i] != WalletReader.\u0001[i])
				{
					throw new Exception(global::\u0005.\u0001.\u0001(764));
				}
			}
			int num = ((int)array[WalletReader.\u0002] << 24) + ((int)array[WalletReader.\u0002 + 1] << 16) + ((int)array[WalletReader.\u0002 + 2] << 8) + (int)array[WalletReader.\u0002 + 3];
			int num2 = ((int)array[WalletReader.\u0003] << 24) + ((int)array[WalletReader.\u0003 + 1] << 16) + ((int)array[WalletReader.\u0003 + 2] << 8) + (int)array[WalletReader.\u0003 + 3];
			if (num == 5)
			{
				return null;
			}
			if (num == 6)
			{
				byte b = array[WalletReader.\u0004];
				byte[] bytes;
				int num3;
				if (b == WalletReader.\u0007)
				{
					byte[] array2 = new byte[WalletReader.\u000E];
					byte[] array3 = new byte[num2 - 1];
					byte[] array4 = new byte[(num2 - 1) / 2];
					Array.Copy(array, WalletReader.\u0004 + 1, array3, 0, array3.Length);
					string @string = Encoding.ASCII.GetString(array3);
					for (int j = 0; j < array4.Length; j++)
					{
						array4[j] = Convert.ToByte(@string.Substring(j * 2, 2), 16);
					}
					Array.Copy(array4, 0, array2, 0, WalletReader.\u000E);
					DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
					ICryptoTransform cryptoTransform = descryptoServiceProvider.CreateDecryptor(array2, WalletReader.\u0011);
					bytes = cryptoTransform.TransformFinalBlock(array4, 8, WalletReader.\u0010);
					num3 = WalletReader.\u0008;
				}
				else
				{
					if (b != WalletReader.\u0006)
					{
						throw new Exception(global::\u0005.\u0001.\u0001(809));
					}
					byte[] array5 = new byte[WalletReader.\u0012];
					Array.Copy(array, WalletReader.\u0005, array5, 0, WalletReader.\u0012);
					ICryptoTransform cryptoTransform2 = new AesCryptoServiceProvider
					{
						Padding = PaddingMode.None,
						Mode = CipherMode.CBC
					}.CreateDecryptor(array5, WalletReader.\u0017);
					bytes = cryptoTransform2.TransformFinalBlock(array, WalletReader.\u0005 + WalletReader.\u0012, WalletReader.\u0014);
					num3 = WalletReader.\u0015;
				}
				\u0003 = Encoding.Default.GetString(bytes);
				byte[] array6 = new byte[array.Length - num3];
				Array.Copy(array, num3, array6, 0, array.Length - num3);
				return array6;
			}
			throw new Exception(global::\u0005.\u0001.\u0001(834));
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x00126358 File Offset: 0x00124558
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static byte[] \u0001(string \u0002)
		{
			FileStream fileStream = new FileStream(\u0002, FileMode.Open, FileAccess.Read);
			int num = (int)fileStream.Length;
			byte[] array = new byte[num];
			num = fileStream.Read(array, 0, num);
			fileStream.Close();
			return array;
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x00126398 File Offset: 0x00124598
		// Note: this type is marked as 'beforefieldinit'.
		static WalletReader()
		{
			byte[] u = new byte[8];
			WalletReader.\u0011 = u;
			WalletReader.\u0012 = 16;
			WalletReader.\u0013 = WalletReader.\u0005 + WalletReader.\u0012;
			WalletReader.\u0014 = 16;
			WalletReader.\u0015 = WalletReader.\u0013 + WalletReader.\u0014;
			WalletReader.\u0016 = 16;
			WalletReader.\u0017 = new byte[]
			{
				192,
				52,
				216,
				49,
				28,
				2,
				206,
				248,
				81,
				240,
				20,
				75,
				129,
				237,
				75,
				242
			};
		}

		// Token: 0x0400205E RID: 8286
		internal static byte[] \u0001 = new byte[]
		{
			161,
			248,
			78,
			54
		};

		// Token: 0x0400205F RID: 8287
		internal static int \u0002 = 4;

		// Token: 0x04002060 RID: 8288
		internal static int \u0003 = 8;

		// Token: 0x04002061 RID: 8289
		internal static int \u0004 = 12;

		// Token: 0x04002062 RID: 8290
		internal static int \u0005 = 13;

		// Token: 0x04002063 RID: 8291
		internal static byte \u0006 = 6;

		// Token: 0x04002064 RID: 8292
		internal static byte \u0007 = Convert.ToByte('5');

		// Token: 0x04002065 RID: 8293
		internal static int \u0008 = 77;

		// Token: 0x04002066 RID: 8294
		internal static int \u000E = 8;

		// Token: 0x04002067 RID: 8295
		internal static int \u000F = 21;

		// Token: 0x04002068 RID: 8296
		internal static int \u0010 = 24;

		// Token: 0x04002069 RID: 8297
		internal static byte[] \u0011;

		// Token: 0x0400206A RID: 8298
		internal static int \u0012;

		// Token: 0x0400206B RID: 8299
		internal static int \u0013;

		// Token: 0x0400206C RID: 8300
		internal static int \u0014;

		// Token: 0x0400206D RID: 8301
		internal static int \u0015;

		// Token: 0x0400206E RID: 8302
		internal static int \u0016;

		// Token: 0x0400206F RID: 8303
		internal static byte[] \u0017;
	}
}
