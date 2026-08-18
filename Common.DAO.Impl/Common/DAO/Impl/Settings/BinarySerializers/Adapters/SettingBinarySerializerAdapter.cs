using System;
using EncryptionClassLibrary;

namespace TechnoPro.Common.DAO.Impl.Settings.BinarySerializers.Adapters
{
	// Token: 0x02000057 RID: 87
	public static class SettingBinarySerializerAdapter
	{
		// Token: 0x0600022A RID: 554 RVA: 0x00013118 File Offset: 0x00011318
		private static byte[] GetEmptyArray(IEncryption encryption)
		{
			return string.Empty.StringToBytes(encryption);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00013138 File Offset: 0x00011338
		public static string BytesToString(this byte[] binaryData, IEncryption encryption)
		{
			return (encryption != null) ? encryption.Decrypt(binaryData) : null;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00013148 File Offset: 0x00011348
		public static byte[] StringToBytes(this string text, IEncryption encryption)
		{
			return (encryption != null) ? encryption.Encrypt(text) : null;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00013168 File Offset: 0x00011368
		public static bool IsEmptyArray(this byte[] binaryArray, IEncryption encryption)
		{
			return SettingBinarySerializerAdapter.ArrayEquals(binaryArray, SettingBinarySerializerAdapter.GetEmptyArray(encryption));
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00013188 File Offset: 0x00011388
		private static bool ArrayEquals(byte[] array1, byte[] array2)
		{
			bool flag = array1.Length != array2.Length;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < array1.Length; i++)
				{
					bool flag2 = array1[i] != array2[i];
					if (flag2)
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}
	}
}
