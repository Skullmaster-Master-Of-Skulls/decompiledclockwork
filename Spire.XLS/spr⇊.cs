using System;
using System.IO;
using System.Security.Cryptography;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002DB RID: 731
internal sealed class spr\u21CA
{
	// Token: 0x06002CD4 RID: 11476 RVA: 0x001938BC File Offset: 0x001928BC
	internal static byte[] ᜀ(byte[] A_0, byte[] A_1, byte[] A_2, int A_3)
	{
		switch (0)
		{
		default:
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				break;
			}
			byte[] array;
			for (;;)
			{
				int num = 0;
				spr\u1C4C spr_u1C4C = new spr\u1C4C(spr\u1C4C.KeySize.Bits128, A_1);
				array = new byte[A_0.Length];
				byte[] array2 = new byte[A_3];
				Buffer.BlockCopy(A_0, 0, array2, 0, array2.Length);
				byte[] a_ = spr\u21CA.ᜀ(array2, A_2);
				byte[] array3 = new byte[A_3];
				spr_u1C4C.ᜁ(a_, array3);
				Buffer.BlockCopy(array3, 0, array, 0, array3.Length);
				num += A_3;
				int num2 = 3;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
					{
						if (num >= A_0.Length)
						{
							num2 = 2;
							continue;
						}
						byte[] array4 = new byte[A_3];
						Buffer.BlockCopy(A_0, num, array4, 0, array2.Length);
						a_ = spr\u21CA.ᜀ(array4, array3);
						spr_u1C4C.ᜁ(a_, array3);
						Buffer.BlockCopy(array3, 0, array, num, array3.Length);
						num += A_3;
						num2 = 1;
						continue;
					}
					case 1:
						goto IL_B6;
					case 2:
						return array;
					case 3:
						goto IL_B6;
					}
					break;
					IL_B6:
					num2 = 0;
				}
			}
			return array;
		}
		}
	}

	// Token: 0x06002CD5 RID: 11477 RVA: 0x001939EC File Offset: 0x001929EC
	internal static byte[] ᜀ(byte[] A_0, byte[] A_1, byte[] A_2, int A_3, int A_4)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			ICryptoTransform transform = new RijndaelManaged
			{
				KeySize = A_3 * 8,
				BlockSize = 128,
				Mode = CipherMode.CBC,
				Padding = PaddingMode.Zeros,
				Key = A_1,
				IV = A_2
			}.CreateDecryptor();
			MemoryStream memoryStream = new MemoryStream();
			byte[] result;
			try
			{
				CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
				try
				{
					cryptoStream.Write(A_0, 0, A_0.Length);
				}
				finally
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							((IDisposable)cryptoStream).Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_AE;
						}
						if (cryptoStream == null)
						{
							break;
						}
						num = 1;
					}
					IL_AE:;
				}
				result = memoryStream.ToArray();
			}
			finally
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						((IDisposable)memoryStream).Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_F6;
					}
					if (memoryStream == null)
					{
						break;
					}
					num = 1;
				}
				IL_F6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_F6;
				default:
					if (false)
					{
					}
					break;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06002CD6 RID: 11478 RVA: 0x00193B38 File Offset: 0x00192B38
	internal static HMAC ᜁ(string A_0)
	{
		int a_ = 7;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_63;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 1:
			{
				string a;
				if (!(a == RecordTableEnumerator.b("渼眾@牂", a_)))
				{
					num = 14;
					continue;
				}
				goto IL_189;
			}
			case 2:
				num = 10;
				continue;
			case 3:
				num = 11;
				continue;
			case 4:
			{
				string a;
				if ((a = A_0.ToUpper()) != null)
				{
					num = 2;
					continue;
				}
				goto IL_1DB;
			}
			case 5:
			{
				string a;
				if (!(a == RecordTableEnumerator.b("渼眾@療瑄畆", a_)))
				{
					num = 13;
					continue;
				}
				goto IL_117;
			}
			case 6:
				goto IL_63;
			case 7:
				num = 1;
				continue;
			case 8:
				goto IL_1A2;
			case 10:
			{
				string a;
				if (!(a == RecordTableEnumerator.b("瀼笾瑀", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_111;
			}
			case 11:
			{
				string a;
				if (!(a == RecordTableEnumerator.b("渼眾@灂組獆", a_)))
				{
					num = 7;
					continue;
				}
				goto IL_68;
			}
			case 12:
			{
				string a;
				if (!(a == RecordTableEnumerator.b("渼眾@煂灄煆", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_E6;
			}
			case 13:
				if (true)
				{
				}
				num = 8;
				continue;
			case 14:
				num = 12;
				continue;
			}
			if (A_0 == null)
			{
				num = 6;
			}
			else
			{
				num = 4;
			}
		}
		IL_63:
		return new HMACSHA1();
		IL_68:
		return new HMACSHA384();
		IL_E6:
		return new HMACSHA256();
		IL_111:
		return new HMACMD5();
		IL_117:
		return new HMACSHA512();
		IL_189:
		return new HMACSHA1();
		IL_1A2:
		IL_1DB:
		throw new NotImplementedException(string.Format(RecordTableEnumerator.b("格儾㉀㙂㕄㝆♈㥊㥌⩎㕐獒㵔㙖⩘㍚絜㹞ൠѢ੤ᕦhὪլɮ兰ࡲ䕴੶", a_), A_0));
	}

	// Token: 0x06002CD7 RID: 11479 RVA: 0x00193D3C File Offset: 0x00192D3C
	internal static HashAlgorithm ᜀ(string A_0)
	{
		int a_ = 6;
		int num = 10;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 4;
				continue;
			case 1:
				goto IL_19A;
			case 2:
			{
				string a;
				if ((a = A_0.ToUpper()) != null)
				{
					num = 0;
					continue;
				}
				goto IL_1DB;
			}
			case 3:
				num = 8;
				continue;
			case 4:
			{
				string a;
				if (!(a == RecordTableEnumerator.b("焻稽甿", a_)))
				{
					num = 7;
					continue;
				}
				goto IL_111;
			}
			case 5:
			{
				string a;
				if (!(a == RecordTableEnumerator.b("漻瘽Ŀ睁畃瑅", a_)))
				{
					num = 12;
					continue;
				}
				goto IL_117;
			}
			case 6:
			{
				string a;
				if (!(a == RecordTableEnumerator.b("漻瘽Ŀ獁", a_)))
				{
					num = 9;
					continue;
				}
				goto IL_68;
			}
			case 7:
				num = 6;
				continue;
			case 8:
			{
				string a;
				if (!(a == RecordTableEnumerator.b("漻瘽Ŀ煁籃牅", a_)))
				{
					num = 14;
					continue;
				}
				goto IL_E6;
			}
			case 9:
				num = 13;
				continue;
			case 11:
				goto IL_63;
			case 12:
				num = 1;
				continue;
			case 13:
			{
				string a;
				if (!(a == RecordTableEnumerator.b("漻瘽Ŀ灁煃灅", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_189;
			}
			case 14:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_63;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 11;
			}
			else
			{
				num = 2;
			}
		}
		IL_63:
		return new SHA1CryptoServiceProvider();
		IL_68:
		return new SHA1CryptoServiceProvider();
		IL_E6:
		return new SHA384CryptoServiceProvider();
		IL_111:
		return new MD5CryptoServiceProvider();
		IL_117:
		return new SHA512CryptoServiceProvider();
		IL_189:
		return new SHA256CryptoServiceProvider();
		IL_19A:
		if (true)
		{
		}
		IL_1DB:
		throw new NotImplementedException(string.Format(RecordTableEnumerator.b("椻倽㌿㝁㑃㙅❇㡉㡋⭍㑏牑㱓㝕⭗㉙籛㽝౟աୣᑥŧṩѫͭ偯ॱ䑳୵", a_), A_0));
	}

	// Token: 0x06002CD8 RID: 11480 RVA: 0x00193F40 File Offset: 0x00192F40
	internal static byte[] ᜀ(byte[] A_0, byte[] A_1, byte[] A_2, int A_3, int A_4, string A_5 = null)
	{
		switch (0)
		{
		default:
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				break;
			}
			HashAlgorithm hashAlgorithm;
			byte[] buffer;
			byte[] array2;
			for (;;)
			{
				hashAlgorithm = spr\u21CA.ᜀ(A_5);
				buffer = sprṯ.ᜀ(A_1, A_0);
				byte[] array = hashAlgorithm.ComputeHash(buffer);
				array2 = array;
				uint num = 0U;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_96;
					case 1:
						goto IL_75;
					case 2:
						goto IL_75;
					case 3:
					{
						if ((ulong)num >= (ulong)((long)A_3))
						{
							if (true)
							{
							}
							num2 = 0;
							continue;
						}
						byte[] bytes = BitConverter.GetBytes(num);
						buffer = sprṯ.ᜀ(bytes, array2);
						array2 = hashAlgorithm.ComputeHash(buffer);
						num += 1U;
						num2 = 1;
						continue;
					}
					}
					break;
					IL_75:
					num2 = 3;
				}
			}
			IL_96:
			buffer = sprṯ.ᜀ(array2, A_2);
			byte[] a_ = hashAlgorithm.ComputeHash(buffer);
			return spr\u21CA.ᜀ(a_, A_4, 54);
		}
		}
	}

	// Token: 0x06002CD9 RID: 11481 RVA: 0x00194034 File Offset: 0x00193034
	internal static byte[] ᜀ(byte[] A_0, uint A_1, string A_2 = null)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		HashAlgorithm hashAlgorithm = spr\u21CA.ᜀ(A_2);
		byte[] bytes = BitConverter.GetBytes(A_1);
		byte[] buffer = sprṯ.ᜀ(A_0, bytes);
		byte[] src = hashAlgorithm.ComputeHash(buffer);
		byte[] array = new byte[16];
		Buffer.BlockCopy(src, 0, array, 0, array.Length);
		return array;
	}

	// Token: 0x06002CDA RID: 11482 RVA: 0x001940A8 File Offset: 0x001930A8
	internal static byte[] ᜀ(byte[] A_0, byte[] A_1, string A_2 = null)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		HashAlgorithm hashAlgorithm = spr\u21CA.ᜀ(A_2);
		byte[] buffer = sprṯ.ᜀ(A_0, A_1);
		byte[] src = hashAlgorithm.ComputeHash(buffer);
		byte[] array = new byte[16];
		Buffer.BlockCopy(src, 0, array, 0, array.Length);
		return array;
	}

	// Token: 0x06002CDB RID: 11483 RVA: 0x00194110 File Offset: 0x00193110
	internal static byte[] ᜀ(byte[] A_0, string A_1 = null)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		HashAlgorithm hashAlgorithm = spr\u21CA.ᜀ(A_1);
		return hashAlgorithm.ComputeHash(A_0);
	}

	// Token: 0x06002CDC RID: 11484 RVA: 0x0019415C File Offset: 0x0019315C
	internal static byte[] ᜀ(byte[] A_0, byte[] A_1)
	{
		byte[] array;
		for (;;)
		{
			IL_18:
			array = new byte[A_0.Length];
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_35;
					case 1:
					{
						if (num >= array.Length)
						{
							num2 = 3;
							continue;
						}
						int num3 = num % A_1.Length;
						array[num] = (A_0[num] ^ A_1[num3]);
						num++;
						num2 = 0;
						continue;
					}
					case 2:
						goto IL_35;
					case 3:
						goto IL_55;
					}
					goto IL_18;
					IL_35:
					num2 = 1;
				}
				IL_55:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_8C;
				}
			}
		}
		IL_8C:
		if (false)
		{
		}
		return array;
	}

	// Token: 0x06002CDD RID: 11485 RVA: 0x001941FC File Offset: 0x001931FC
	internal static byte[] ᜀ(byte[] A_0, int A_1, byte A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				byte[] array = new byte[A_1];
				int num = A_1 - A_0.Length;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F7;
						default:
							if (false)
							{
							}
							goto IL_9B;
						}
						break;
					case 1:
						return A_0;
					case 2:
						if (num == 0)
						{
							num2 = 1;
							continue;
						}
						num2 = 8;
						continue;
					case 3:
					{
						int num3;
						if (num3 >= A_1)
						{
							num2 = 6;
							continue;
						}
						byte[] array2;
						Buffer.BlockCopy(array2, 0, array, num3, array2.Length);
						num3 += array2.Length;
						num2 = 4;
						continue;
					}
					case 4:
						goto IL_9B;
					case 5:
						Buffer.BlockCopy(A_0, 0, array, 0, array.Length);
						num2 = 7;
						continue;
					case 6:
						return array;
					case 7:
						return array;
					case 8:
					{
						if (num < 0)
						{
							num2 = 5;
							continue;
						}
						byte[] array2 = new byte[]
						{
							A_2
						};
						Buffer.BlockCopy(A_0, 0, array, 0, A_0.Length);
						int num3 = A_0.Length;
						goto IL_F7;
					}
					}
					break;
					IL_9B:
					num2 = 3;
					continue;
					IL_F7:
					if (true)
					{
					}
					num2 = 0;
				}
			}
			return A_0;
		}
	}
}
