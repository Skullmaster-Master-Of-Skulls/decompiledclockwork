using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using Spire.License;
using Spire.License.V1_0;

// Token: 0x02000008 RID: 8
internal class c
{
	// Token: 0x0600002E RID: 46 RVA: 0x00003944 File Offset: 0x00001B44
	internal static Spire.License.LicenseInfo b(string A_0)
	{
		switch (0)
		{
		default:
		{
			byte[] array;
			BaseLicenseInfo baseLicenseInfo;
			for (;;)
			{
				array = c.a(A_0);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_7E;
						try
						{
							for (;;)
							{
								IL_7E:
								MemoryStream memoryStream;
								StreamReader streamReader = new StreamReader(memoryStream, c.a);
								try
								{
									XmlSerializer xmlSerializer;
									baseLicenseInfo = (BaseLicenseInfo)xmlSerializer.Deserialize(streamReader);
								}
								finally
								{
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_DA;
										case 1:
											((IDisposable)streamReader).Dispose();
											num = 0;
											continue;
										}
										if (streamReader == null)
										{
											break;
										}
										num = 1;
									}
									IL_DA:;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									goto IL_F3;
								}
							}
							IL_F3:
							if (false)
							{
							}
							goto IL_57;
						}
						finally
						{
							num = 0;
							for (;;)
							{
								MemoryStream memoryStream;
								switch (num)
								{
								case 1:
									((IDisposable)memoryStream).Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_139;
								}
								if (memoryStream == null)
								{
									break;
								}
								num = 1;
							}
							IL_139:;
						}
						goto IL_13C;
						IL_57:
						num = 4;
						continue;
					case 1:
					{
						if (true)
						{
						}
						if (array == null)
						{
							num = 3;
							continue;
						}
						baseLicenseInfo = null;
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(BaseLicenseInfo));
						MemoryStream memoryStream = new MemoryStream(array);
						num = 0;
						continue;
					}
					case 2:
						goto IL_6F;
					case 3:
						goto IL_52;
					case 4:
						if (baseLicenseInfo == null)
						{
							num = 2;
							continue;
						}
						goto IL_16A;
					}
					break;
				}
			}
			IL_52:
			goto IL_13C;
			IL_6F:
			return null;
			IL_13C:
			return null;
			IL_16A:
			baseLicenseInfo.Key = A_0;
			return c.a(array, c.a, baseLicenseInfo);
		}
		}
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00003AEC File Offset: 0x00001CEC
	internal static Spire.License.LicenseInfo a(Stream A_0)
	{
		switch (0)
		{
		default:
		{
			BaseLicenseInfo baseLicenseInfo;
			for (;;)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(BaseLicenseInfo));
				baseLicenseInfo = null;
				StreamReader streamReader = new StreamReader(A_0, c.a);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						num = 1;
						continue;
					case 1:
						if (baseLicenseInfo.Key.Length == 0)
						{
							num = 6;
							continue;
						}
						goto IL_142;
					case 2:
						try
						{
							baseLicenseInfo = (BaseLicenseInfo)xmlSerializer.Deserialize(streamReader);
							goto IL_8B;
						}
						finally
						{
							num = 2;
							for (;;)
							{
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
										((IDisposable)streamReader).Dispose();
										num = 1;
										continue;
									}
									break;
								case 1:
									goto IL_118;
								}
								if (streamReader == null)
								{
									break;
								}
								num = 0;
							}
							IL_118:;
						}
						goto IL_11B;
						IL_8B:
						num = 4;
						continue;
					case 3:
						num = 5;
						continue;
					case 4:
						if (baseLicenseInfo != null)
						{
							num = 3;
							continue;
						}
						goto IL_11B;
					case 5:
						if (baseLicenseInfo.Key != null)
						{
							num = 0;
							continue;
						}
						goto IL_11B;
					case 6:
						goto IL_86;
					}
					break;
				}
			}
			IL_86:
			IL_11B:
			return null;
			IL_142:
			byte[] a_ = c.a(baseLicenseInfo.Key);
			return c.a(a_, c.a, baseLicenseInfo);
		}
		}
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00003C64 File Offset: 0x00001E64
	private static Spire.License.LicenseInfo a(byte[] A_0, Encoding A_1, BaseLicenseInfo A_2)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			LicenseInfoAdapter licenseInfoAdapter;
			for (;;)
			{
				string name = string.Format(Spire.License.V1_0.Product.b("풣쾥\udaa7쾩芫\ud9af톱톳\ud8b5쮷\udfb9銻뮿맃蓇ꏉ꿋ꯍ뻏ꇑ뇓鿕뛗볙돛", a_), A_2.Version.Replace('.', '_'));
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				Type type = executingAssembly.GetType(name, false);
				int num = 0;
				for (;;)
				{
					MemoryStream memoryStream;
					XmlSerializer xmlSerializer;
					switch (num)
					{
					case 0:
						if (type == null)
						{
							num = 4;
							continue;
						}
						goto IL_1A8;
					case 1:
						goto IL_1A8;
					case 2:
						if (licenseInfoAdapter != null)
						{
							num = 5;
							continue;
						}
						goto IL_1CB;
					case 3:
						goto IL_A7;
						try
						{
							for (;;)
							{
								IL_A7:
								StreamReader streamReader = new StreamReader(memoryStream, A_1);
								try
								{
									licenseInfoAdapter = (LicenseInfoAdapter)xmlSerializer.Deserialize(streamReader);
								}
								catch (Exception)
								{
								}
								finally
								{
									num = 0;
									for (;;)
									{
										switch (num)
										{
										case 1:
											((IDisposable)streamReader).Dispose();
											num = 2;
											continue;
										case 2:
											goto IL_106;
										}
										if (streamReader == null)
										{
											break;
										}
										num = 1;
									}
									IL_106:;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									goto IL_11F;
								}
							}
							IL_11F:
							if (false)
							{
							}
							goto IL_87;
						}
						finally
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									((IDisposable)memoryStream).Dispose();
									num = 1;
									continue;
								case 1:
									goto IL_167;
								}
								if (memoryStream == null)
								{
									break;
								}
								num = 0;
							}
							IL_167:;
						}
						goto IL_16A;
						IL_87:
						num = 2;
						continue;
					case 4:
						goto IL_16A;
					case 5:
						goto IL_A2;
					}
					break;
					IL_16A:
					type = typeof(Spire.License.LicenseInfo);
					num = 1;
					continue;
					IL_1A8:
					licenseInfoAdapter = null;
					xmlSerializer = new XmlSerializer(type);
					memoryStream = new MemoryStream(A_0);
					num = 3;
				}
			}
			IL_A2:
			if (true)
			{
			}
			licenseInfoAdapter.Key = A_2.Key;
			return licenseInfoAdapter.ConvertToCurrentVersion();
			IL_1CB:
			return null;
		}
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00003E68 File Offset: 0x00002068
	private static byte[] a(string A_0)
	{
		switch (0)
		{
		default:
		{
			byte[] array = Convert.FromBase64String(A_0);
			byte[] array2 = new byte[15];
			Array.Copy(array, array2, array2.Length);
			int num = (int)(array2[0] % 13);
			int num2 = (int)(array2[num + 1] & byte.MaxValue) << 8 | (int)(array2[num + 2] & byte.MaxValue);
			byte[] array3 = new byte[num2];
			Array.Copy(array, 15, array3, 0, array3.Length);
			num = array2.Length + array3.Length;
			byte[] array4 = new byte[array.Length - num];
			Array.Copy(array, num, array4, 0, array4.Length);
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
			byte[] result;
			try
			{
				for (;;)
				{
					rsacryptoServiceProvider.ImportParameters(new RSAParameters
					{
						Modulus = global::b.a,
						Exponent = global::b.b
					});
					int num3 = 3;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_170;
						case 1:
							goto IL_17F;
						case 2:
							goto IL_190;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_170;
							default:
								if (false)
								{
								}
								if (!rsacryptoServiceProvider.VerifyData(array4, new SHA1CryptoServiceProvider(), array3))
								{
									num3 = 0;
									continue;
								}
								num3 = 2;
								continue;
							}
							break;
						}
						break;
						IL_170:
						result = null;
						num3 = 1;
					}
				}
				IL_17F:
				return result;
				IL_190:
				goto IL_8B;
			}
			finally
			{
				int num3 = 0;
				for (;;)
				{
					switch (num3)
					{
					case 1:
						goto IL_1D2;
					case 2:
						((IDisposable)rsacryptoServiceProvider).Dispose();
						num3 = 1;
						continue;
					}
					if (rsacryptoServiceProvider == null)
					{
						break;
					}
					num3 = 2;
				}
				IL_1D2:;
			}
			byte[] result2;
			return result2;
			IL_8B:
			int num4 = (int)array4[0];
			byte[] array5 = new byte[num4];
			Array.Copy(array4, 1, array5, 0, array5.Length);
			int num5 = array4.Length - 1 - array5.Length;
			byte[] array6 = new byte[num5];
			Array.Copy(array4, 1 + array5.Length, array6, 0, array6.Length);
			result2 = null;
			DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
			try
			{
				descryptoServiceProvider.Key = global::b.c;
				descryptoServiceProvider.IV = array5;
				MemoryStream memoryStream = new MemoryStream(array6);
				try
				{
					CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Read);
					try
					{
						MemoryStream memoryStream2 = new MemoryStream();
						try
						{
							for (;;)
							{
								byte[] array7 = new byte[1024];
								int num3 = 2;
								for (;;)
								{
									switch (num3)
									{
									case 0:
										goto IL_262;
									case 1:
									{
										int count;
										if ((count = cryptoStream.Read(array7, 0, array7.Length)) <= 0)
										{
											num3 = 3;
											continue;
										}
										memoryStream2.Write(array7, 0, count);
										num3 = 0;
										continue;
									}
									case 2:
										goto IL_262;
									case 3:
										result2 = memoryStream2.ToArray();
										num3 = 4;
										continue;
									case 4:
										goto IL_29F;
									}
									break;
									IL_262:
									num3 = 1;
								}
							}
							IL_29F:;
						}
						finally
						{
							int num3 = 0;
							for (;;)
							{
								switch (num3)
								{
								case 1:
									((IDisposable)memoryStream2).Dispose();
									num3 = 2;
									continue;
								case 2:
									goto IL_2DE;
								}
								if (memoryStream2 == null)
								{
									break;
								}
								num3 = 1;
							}
							IL_2DE:;
						}
					}
					finally
					{
						int num3 = 0;
						for (;;)
						{
							switch (num3)
							{
							case 1:
								goto IL_320;
							case 2:
								((IDisposable)cryptoStream).Dispose();
								num3 = 1;
								continue;
							}
							if (cryptoStream == null)
							{
								break;
							}
							num3 = 2;
						}
						IL_320:;
					}
				}
				finally
				{
					int num3 = 1;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_362;
						case 2:
							((IDisposable)memoryStream).Dispose();
							num3 = 0;
							continue;
						}
						if (memoryStream == null)
						{
							break;
						}
						num3 = 2;
					}
					IL_362:;
				}
				return result2;
			}
			finally
			{
				int num3 = 2;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						if (true)
						{
						}
						((IDisposable)descryptoServiceProvider).Dispose();
						num3 = 1;
						continue;
					case 1:
						goto IL_3AF;
					}
					if (descryptoServiceProvider == null)
					{
						break;
					}
					num3 = 0;
				}
				IL_3AF:;
			}
			return result;
		}
		}
	}

	// Token: 0x06000033 RID: 51 RVA: 0x000042BC File Offset: 0x000024BC
	// Note: this type is marked as 'beforefieldinit'.
	static c()
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
		c.a = new UTF8Encoding(false);
	}

	// Token: 0x04000012 RID: 18
	private static Encoding a;
}
