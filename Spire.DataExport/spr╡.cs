using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;
using Spire.DataExport.CollectionEditors;

// Token: 0x02000074 RID: 116
internal abstract class spr\u2561
{
	// Token: 0x0600039D RID: 925 RVA: 0x00021F10 File Offset: 0x00020F10
	public static bool ᜃ(string A_0)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = -1;
			bool result;
			try
			{
				int num2 = 6;
				for (;;)
				{
					string text;
					switch (num2)
					{
					case 0:
						if (num >= 0)
						{
							num2 = 8;
							continue;
						}
						goto IL_136;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9E;
						default:
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
						break;
					case 2:
						goto IL_142;
					case 3:
						result = false;
						num2 = 7;
						continue;
					case 4:
						goto IL_A7;
					case 5:
						if (text.Length > 0)
						{
							num2 = 1;
							continue;
						}
						goto IL_136;
					case 7:
						goto IL_B8;
					case 8:
					{
						string a_2 = text.Substring(0, num);
						string a_3 = text.Substring(num + 3);
						result = spr\u2561.ᜁ(a_2, a_3);
						goto IL_9E;
					}
					}
					if (A_0 == null)
					{
						num2 = 3;
						continue;
					}
					text = spr\u2561.ᜀ(A_0);
					num = text.IndexOf(HyperlinksCollectionEditor.b("尨弪䤬", a_));
					num2 = 5;
					continue;
					IL_9E:
					num2 = 4;
					continue;
					IL_136:
					num2 = 2;
				}
				IL_A7:
				IL_B8:
				return result;
				IL_142:
				return false;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}
		}
	}

	// Token: 0x0600039E RID: 926 RVA: 0x00022088 File Offset: 0x00021088
	public static void ᜃ(string A_0, string A_1)
	{
		int a_ = 14;
		try
		{
			int num = 3;
			for (;;)
			{
				RegistryKey registryKey;
				switch (num)
				{
				case 0:
					num = 9;
					continue;
				case 1:
					num = 7;
					continue;
				case 2:
					goto IL_133;
				case 4:
					if (registryKey != null)
					{
						num = 0;
						continue;
					}
					goto IL_128;
				case 5:
					goto IL_55;
				case 6:
					goto IL_128;
				case 7:
					if (A_0.Length <= 0)
					{
						goto IL_128;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_55;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				case 8:
					if (true)
					{
					}
					registryKey.SetValue(HyperlinksCollectionEditor.b("昩䔫䴭唯就䜳匵", a_), A_1);
					registryKey.SetValue(HyperlinksCollectionEditor.b("砩䤫䤭縯匱夳匵", a_), A_0);
					num = 6;
					continue;
				case 9:
					if (A_1.Length > 0)
					{
						num = 1;
						continue;
					}
					goto IL_128;
				}
				if (spr\u2561.ᜁ(A_0, A_1))
				{
					num = 5;
					continue;
				}
				goto IL_128;
				IL_55:
				registryKey = Registry.CurrentUser.CreateSubKey(HyperlinksCollectionEditor.b("礩䌫䠭䐯䔱唳䐵崷昹夻ጽ⤿⅁⅃⑅⑇㽉⥋ቍ͏≑㵓⑕㵗瑙ᡛ㽝ᑟ͡ⅣṥᡧթṫᩭⱯ䁱婳䙵䥷♹", a_));
				num = 4;
				continue;
				IL_128:
				num = 2;
			}
			IL_133:;
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0600039F RID: 927 RVA: 0x000221F4 File Offset: 0x000211F4
	public static string ᜂ(string A_0)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			string result;
			try
			{
				for (;;)
				{
					string text = spr\u2561.ᜀ(A_0);
					int num = text.IndexOf(HyperlinksCollectionEditor.b("栜欞䔠", a_));
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_CD;
							default:
								goto IL_B5;
							}
							break;
						case 1:
							if (text.Length > 0)
							{
								num2 = 5;
								continue;
							}
							goto IL_DB;
						case 2:
						{
							string text2 = text.Substring(0, num);
							result = text2;
							num2 = 0;
							continue;
						}
						case 3:
							if (num >= 0)
							{
								goto IL_CD;
							}
							goto IL_DB;
						case 4:
							goto IL_E7;
						case 5:
							num2 = 3;
							continue;
						}
						break;
						IL_CD:
						num2 = 2;
						continue;
						IL_DB:
						num2 = 4;
					}
				}
				IL_B5:
				if (false)
				{
				}
				goto IL_F5;
				IL_E7:
				goto IL_21;
			}
			catch (Exception)
			{
				result = string.Empty;
			}
			goto IL_F5;
			IL_21:
			return string.Empty;
			IL_F5:
			if (true)
			{
			}
			return result;
		}
		}
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x00022310 File Offset: 0x00021310
	private static byte[] ᜂ(string A_0, string A_1)
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
		return null;
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x0002234C File Offset: 0x0002134C
	public static string ᜀ(RegistryKey A_0, string A_1)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			string result;
			try
			{
				for (;;)
				{
					RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(HyperlinksCollectionEditor.b("紭弯吱䀳䄵夷䠹夻戽┿潁ⵃ╅ⵇ⡉⁋㭍㕏๑ݓ♕ㅗ⡙㥛灝⑟͡ၣݥⵧቩᱫŭɯٱ⡳䑵噷䩹䵻≽", a_));
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (registryKey != null)
							{
								num = 3;
								continue;
							}
							num = 2;
							continue;
						case 1:
							goto IL_100;
						case 2:
							goto IL_10E;
						case 3:
						{
							string str = registryKey.GetValue(HyperlinksCollectionEditor.b("戭夯儱儳堵䬷弹", a_)).ToString();
							string str2 = registryKey.GetValue(HyperlinksCollectionEditor.b("簭唯唱稳圵唷弹", a_)).ToString();
							result = spr\u2561.ᜁ(str2 + HyperlinksCollectionEditor.b("嬭䐯嘱", a_) + str);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_100;
							default:
								if (false)
								{
								}
								num = 1;
								continue;
							}
							break;
						}
						}
						break;
					}
				}
				IL_100:
				return result;
				IL_10E:
				goto IL_37;
			}
			catch (Exception)
			{
				goto IL_37;
			}
			return result;
			IL_37:
			return string.Empty;
		}
		}
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x00022484 File Offset: 0x00021484
	internal static string ᜀ(string A_0, string A_1, string A_2, string A_3, int A_4, string A_5, int A_6)
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
		byte[] bytes = Encoding.ASCII.GetBytes(A_5);
		byte[] bytes2 = Encoding.ASCII.GetBytes(A_2);
		byte[] array = Convert.FromBase64String(A_0);
		PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(A_1, bytes2, A_3, A_4);
		byte[] bytes3 = passwordDeriveBytes.GetBytes(A_6 / 8);
		ICryptoTransform transform = new RijndaelManaged
		{
			Mode = CipherMode.CBC
		}.CreateDecryptor(bytes3, bytes);
		MemoryStream memoryStream = new MemoryStream(array);
		CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read);
		byte[] array2 = new byte[array.Length];
		int count = cryptoStream.Read(array2, 0, array2.Length);
		memoryStream.Close();
		cryptoStream.Close();
		return Encoding.UTF8.GetString(array2, 0, count);
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x00022560 File Offset: 0x00021560
	internal static string ᜁ(string A_0)
	{
		string text;
		for (;;)
		{
			text = "";
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_34;
				case 1:
					if (true)
					{
					}
					if (num >= A_0.Length)
					{
						num2 = 3;
						continue;
					}
					text += A_0[num] + '\n' - '\u0002';
					num++;
					num2 = 2;
					continue;
				case 2:
					goto IL_34;
				case 3:
					return text;
				}
				break;
				IL_34:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num2 = 1;
					break;
				}
			}
		}
		return text;
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x0002260C File Offset: 0x0002160C
	internal static string ᜀ(string A_0)
	{
		string text;
		for (;;)
		{
			text = "";
			int num = 0;
			if (true)
			{
			}
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return text;
				case 1:
					goto IL_3C;
				case 2:
					if (num >= A_0.Length)
					{
						num2 = 0;
						continue;
					}
					text += A_0[num] - '\n' + '\u0002';
					num++;
					num2 = 1;
					continue;
				case 3:
					goto IL_3C;
				}
				break;
				IL_3C:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num2 = 2;
					break;
				}
			}
		}
		return text;
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x000226B8 File Offset: 0x000216B8
	internal static bool ᜁ(string A_0, string A_1)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			string a_2 = HyperlinksCollectionEditor.b("氛氝䬟戡圣別䴧丩䜫䬭", a_);
			string a_3 = HyperlinksCollectionEditor.b("伛渝䤟倡䄣ࠥ氧䬩堫伭甯䨱䐳夵䨷丹഻愽爿", a_);
			string a_4 = HyperlinksCollectionEditor.b("伛嘝感ጡ", a_);
			int a_5 = 2;
			string a_6 = HyperlinksCollectionEditor.b("尛⼝戟အ䜣ᔥ洧ḩ丫ᬭ瘯б尳ĵ強夹", a_);
			int a_7 = 128;
			bool result;
			try
			{
				int num = 8;
				for (;;)
				{
					string a;
					int num2;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							if (a == A_0)
							{
								num = 10;
								continue;
							}
							num2++;
							num = 4;
							continue;
						}
						break;
					case 1:
						goto IL_F3;
					case 2:
						if (num2 >= 100)
						{
							num = 5;
							continue;
						}
						num = 0;
						continue;
					case 3:
						goto IL_18E;
					case 4:
						goto IL_F8;
					case 5:
						result = false;
						num = 3;
						continue;
					case 6:
						goto IL_E2;
					case 7:
						result = false;
						num = 6;
						continue;
					case 9:
						goto IL_F8;
					case 10:
						result = true;
						num = 1;
						continue;
					}
					if (A_0.Trim().Length <= 0)
					{
						num = 7;
						continue;
					}
					a = spr\u2561.ᜀ(A_1, a_2, a_3, a_4, a_5, a_6, a_7);
					num2 = 0;
					num = 9;
					continue;
					IL_F8:
					num = 2;
				}
				IL_E2:
				IL_F3:
				IL_18E:;
			}
			catch (Exception)
			{
				return false;
			}
			if (true)
			{
			}
			return result;
		}
		}
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00022884 File Offset: 0x00021884
	internal static string ᜀ(string A_0, string A_1)
	{
		int a_ = 0;
		string a_2 = HyperlinksCollectionEditor.b("氛氝䬟戡圣別䴧丩䜫䬭", a_);
		string a_3 = HyperlinksCollectionEditor.b("伛渝䤟倡䄣ࠥ氧䬩堫伭甯䨱䐳夵䨷丹", a_);
		string a_4 = HyperlinksCollectionEditor.b("伛嘝感ጡ", a_);
		int a_5 = 2;
		string a_6 = HyperlinksCollectionEditor.b("尛⼝戟အ䜣ᔥ洧ḩ丫ᬭ瘯б尳ĵ強夹", a_);
		int a_7 = 128;
		if (A_0.Trim().Length > 0)
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
				return spr\u2561.ᜀ(A_0 + HyperlinksCollectionEditor.b("椛樝䐟", a_) + A_1, a_2, a_3, a_4, a_5, a_6, a_7);
			}
		}
		if (true)
		{
		}
		return string.Empty;
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x00022960 File Offset: 0x00021960
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u2561()
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
		spr\u2561.ᜀ = true;
		spr\u2561.ᜁ = string.Empty;
		spr\u2561.ᜂ = string.Empty;
	}

	// Token: 0x04000275 RID: 629
	internal static bool ᜀ;

	// Token: 0x04000276 RID: 630
	internal static string ᜁ;

	// Token: 0x04000277 RID: 631
	internal static string ᜂ;
}
