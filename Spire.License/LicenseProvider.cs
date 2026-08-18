using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using Spire.License.V1_0;

namespace Spire.License
{
	// Token: 0x0200000C RID: 12
	public class LicenseProvider : LicenseProvider
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00004398 File Offset: 0x00002598
		public static void SetLicenseFileFullPath(string licenseFileFullPath)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				lock (LicenseProvider.h)
				{
					LicenseProvider.a = licenseFileFullPath;
				}
				break;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00004400 File Offset: 0x00002600
		public static void SetLicenseFileName(string licenseFileName)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				lock (LicenseProvider.h)
				{
					LicenseProvider.d = licenseFileName;
				}
				break;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00004468 File Offset: 0x00002668
		public static string GetLicenseFileName()
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return LicenseProvider.d;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000044A8 File Offset: 0x000026A8
		public static void SetLicenseFile(FileInfo licenseFile)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				object obj;
				Monitor.Enter(obj = LicenseProvider.h);
				try
				{
					LicenseProvider.b = licenseFile;
				}
				finally
				{
					if (true)
					{
					}
					Monitor.Exit(obj);
				}
				break;
			}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00004510 File Offset: 0x00002710
		public static void SetLicenseFileStream(Stream licenseFileStream)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				lock (LicenseProvider.h)
				{
					LicenseProvider.c = licenseFileStream;
				}
				break;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00004578 File Offset: 0x00002778
		public static void SetLicenseKey(string key)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				lock (LicenseProvider.h)
				{
					LicenseProvider.f = key;
				}
				break;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000045E0 File Offset: 0x000027E0
		public static void ClearLicense()
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
				lock (LicenseProvider.h)
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_8F;
						case 1:
							LicenseProvider.e.Clear();
							num = 3;
							continue;
						case 3:
							goto IL_81;
						}
						if (true)
						{
						}
						if (LicenseProvider.e != null)
						{
							num = 1;
							continue;
						}
						IL_81:
						LicenseProvider.e = null;
						num = 0;
					}
					IL_8F:;
				}
				break;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00004698 File Offset: 0x00002898
		public static void LoadLicense()
		{
			try
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
					lock (LicenseProvider.h)
					{
						int num = 3;
						for (;;)
						{
							LicenseInfo licenseInfo;
							switch (num)
							{
							case 0:
								goto IL_F8;
							case 1:
								goto IL_ED;
							case 2:
								if (!LicenseProvider.e.ContainsKey(licenseInfo.Key))
								{
									num = 6;
									continue;
								}
								goto IL_ED;
							case 4:
								num = 2;
								continue;
							case 5:
								LicenseProvider.e = new Dictionary<string, LicenseInfo>();
								num = 8;
								continue;
							case 6:
								LicenseProvider.e.Add(licenseInfo.Key, licenseInfo);
								num = 1;
								continue;
							case 7:
								if (licenseInfo != null)
								{
									num = 4;
									continue;
								}
								goto IL_ED;
							case 8:
								goto IL_9E;
							}
							if (LicenseProvider.e == null)
							{
								num = 5;
								continue;
							}
							IL_9E:
							licenseInfo = LicenseProvider.a(null);
							licenseInfo.c();
							num = 7;
							continue;
							IL_ED:
							num = 0;
						}
						IL_F8:;
					}
					break;
				}
			}
			catch (Exception)
			{
			}
			if (true)
			{
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000047F4 File Offset: 0x000029F4
		private static void b(Type A_0)
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				lock (LicenseProvider.h)
				{
					int num = 1;
					for (;;)
					{
						LicenseInfo licenseInfo;
						switch (num)
						{
						case 0:
							goto IL_FF;
						case 2:
							LicenseProvider.e = new Dictionary<string, LicenseInfo>();
							num = 8;
							continue;
						case 3:
							if (!LicenseProvider.e.ContainsKey(licenseInfo.Key))
							{
								num = 4;
								continue;
							}
							goto IL_FF;
						case 4:
							LicenseProvider.e.Add(licenseInfo.Key, licenseInfo);
							num = 0;
							continue;
						case 5:
							if (licenseInfo != null)
							{
								num = 7;
								continue;
							}
							goto IL_FF;
						case 6:
							goto IL_10A;
						case 7:
							num = 3;
							continue;
						case 8:
							goto IL_B0;
						}
						if (LicenseProvider.e == null)
						{
							num = 2;
							continue;
						}
						IL_B0:
						licenseInfo = LicenseProvider.a(A_0);
						licenseInfo.c();
						num = 5;
						continue;
						IL_FF:
						num = 6;
					}
					IL_10A:;
				}
				break;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00004924 File Offset: 0x00002B24
		public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
		{
			LicenseInfo result;
			for (;;)
			{
				bool flag = false;
				result = null;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (LicenseProvider.e != null)
						{
							if (true)
							{
							}
							num = 3;
							continue;
						}
						goto IL_8E;
					case 1:
						if (LicenseProvider.e.Count == 0)
						{
							num = 2;
							continue;
						}
						goto IL_47;
					case 2:
						goto IL_8E;
					case 3:
						IL_45:
						goto IL_70;
					}
					break;
					IL_70:
					num = 1;
					continue;
					try
					{
						IL_47:
						result = this.a(type, instance);
						goto IL_9B;
					}
					catch (Exception)
					{
						if (!flag)
						{
							try
							{
								LicenseProvider.b(type);
								flag = true;
								result = this.a(type, instance);
							}
							catch (Exception)
							{
							}
						}
						goto IL_9B;
					}
					goto IL_70;
					IL_9B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_45;
					default:
						goto IL_B1;
					}
					try
					{
						IL_8E:
						LicenseProvider.b(type);
						flag = true;
						goto IL_47;
					}
					catch (Exception)
					{
						goto IL_47;
					}
					goto IL_9B;
				}
			}
			IL_B1:
			if (false)
			{
			}
			return result;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00004A14 File Offset: 0x00002C14
		private LicenseInfo a(Type A_0, object A_1)
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					IEnumerable<LicenseInfo> enumerable;
					IEnumerator<LicenseInfo> enumerator;
					switch (num)
					{
					case 0:
					{
						try
						{
							enumerable = LicenseProvider.e.Values;
							goto IL_167;
						}
						finally
						{
							object obj;
							Monitor.Exit(obj);
						}
						LicenseInfo result;
						return result;
					}
					case 1:
						num = 4;
						continue;
					case 2:
					{
						enumerable = null;
						object obj;
						Monitor.Enter(obj = LicenseProvider.h);
						num = 0;
						continue;
					}
					case 3:
						goto IL_61;
						try
						{
							LicenseInfo result;
							for (;;)
							{
								IL_61:
								num = 4;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										if (!enumerator.MoveNext())
										{
											num = 6;
											continue;
										}
										LicenseInfo licenseInfo = enumerator.Current;
										licenseInfo.b();
										num = 2;
										continue;
									}
									case 1:
									{
										LicenseInfo licenseInfo;
										result = LicenseProvider.g.a(licenseInfo, A_0, A_1);
										num = 3;
										continue;
									}
									case 2:
									{
										LicenseInfo licenseInfo;
										if (this.a(licenseInfo, A_0, A_1) != null)
										{
											num = 1;
											continue;
										}
										break;
									}
									case 3:
										goto IL_111;
									case 4:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_61;
										default:
											if (false)
											{
											}
											break;
										}
										break;
									case 5:
										goto IL_122;
									case 6:
										num = 5;
										continue;
									}
									IL_DA:
									num = 0;
									continue;
									goto IL_DA;
								}
							}
							IL_111:
							return result;
							IL_122:
							goto IL_1A6;
						}
						finally
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_164;
								case 1:
									enumerator.Dispose();
									num = 0;
									continue;
								}
								if (enumerator == null)
								{
									break;
								}
								num = 1;
							}
							IL_164:;
						}
						goto IL_167;
					case 4:
						if (LicenseProvider.e.Count > 0)
						{
							num = 2;
							continue;
						}
						goto IL_1A6;
					}
					if (LicenseProvider.e != null)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					break;
					IL_167:
					enumerator = enumerable.GetEnumerator();
					num = 3;
				}
				IL_1A6:
				throw new FileNotFoundException(Product.b("용얫춭햯\udcb1잳펵隷\udfb9킻ힽꎿ볃ꯅ꓇", a_));
			}
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004C38 File Offset: 0x00002E38
		private string a(Assembly A_0)
		{
			AssemblyProductAttribute assemblyProductAttribute;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				assemblyProductAttribute = (AssemblyProductAttribute)Attribute.GetCustomAttribute(A_0, typeof(AssemblyProductAttribute));
				if (assemblyProductAttribute == null)
				{
					AssemblyName name = A_0.GetName();
					return name.Name;
				}
				break;
			}
			return assemblyProductAttribute.Product;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00004CA4 File Offset: 0x00002EA4
		private LicenseInfo a(LicenseInfo A_0, Type A_1, object A_2)
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				int num = 31;
				for (;;)
				{
					string text;
					int num2;
					PackageAttribute packageAttribute;
					AssemblyName name;
					Product product;
					PackageAttribute[] package;
					Product[] products;
					Assembly assembly;
					switch (num)
					{
					case 0:
						goto IL_307;
					case 1:
					{
						string value;
						if (text.Equals(value))
						{
							num = 24;
							continue;
						}
						num2++;
						num = 59;
						continue;
					}
					case 2:
						if (A_0.Type != LicenseType.Demo)
						{
							num = 38;
							continue;
						}
						goto IL_704;
					case 3:
						goto IL_691;
					case 4:
						return A_0;
					case 5:
					{
						if (packageAttribute != null)
						{
							num = 43;
							continue;
						}
						int num4;
						int num3 = name.Version.Major - num4;
						int num6;
						int num5 = name.Version.Minor - num6;
						num = 23;
						continue;
					}
					case 6:
					{
						int num7;
						PackageAttribute[] array;
						if (num7 >= array.Length)
						{
							num = 58;
							continue;
						}
						PackageAttribute packageAttribute2 = array[num7];
						num = 10;
						continue;
					}
					case 7:
						if (A_0.Version2.a(1, 3) >= 0)
						{
							num = 46;
							continue;
						}
						goto IL_237;
					case 8:
						goto IL_6D5;
					case 9:
						if (A_0.Type == LicenseType.Temporary)
						{
							num = 54;
							continue;
						}
						goto IL_737;
					case 10:
					{
						string value;
						PackageAttribute packageAttribute2;
						if (packageAttribute2.Name.Equals(value))
						{
							num = 45;
							continue;
						}
						int num7;
						num7++;
						num = 28;
						continue;
					}
					case 11:
						if (A_0.Type == LicenseType.Temporary)
						{
							num = 33;
							continue;
						}
						return A_0;
					case 12:
						if (product == null)
						{
							num = 51;
							continue;
						}
						goto IL_368;
					case 13:
					{
						int num5;
						A_0.IsUpdateRightExpired = (num5 >= 0);
						goto IL_794;
					}
					case 14:
						if (package != null)
						{
							num = 25;
							continue;
						}
						goto IL_237;
					case 15:
						goto IL_387;
					case 16:
					{
						PackageAttribute[] array = package;
						int num7 = 0;
						if (true)
						{
						}
						num = 40;
						continue;
					}
					case 17:
						goto IL_460;
					case 18:
					{
						int num3;
						if (num3 > 1)
						{
							num = 61;
							continue;
						}
						num = 50;
						continue;
					}
					case 19:
						goto IL_368;
					case 20:
						goto IL_368;
					case 21:
						if (package.Length > 0)
						{
							num = 16;
							continue;
						}
						goto IL_237;
					case 22:
						num = 57;
						continue;
					case 23:
						goto IL_460;
					case 24:
					{
						Product product2;
						product = product2;
						num = 20;
						continue;
					}
					case 25:
						num = 21;
						continue;
					case 26:
						return A_0;
					case 27:
					{
						DateTime? releaseDate;
						A_0.IsUpdateRightExpired = (A_0.ExpiredDate < releaseDate);
						num = 32;
						continue;
					}
					case 28:
						goto IL_1B6;
					case 29:
						num = 7;
						continue;
					case 30:
					{
						if (num2 >= products.Length)
						{
							num = 19;
							continue;
						}
						Product product2 = products[num2];
						string value = product2.Name.Replace(Product.b("蒩좭횯\udbb1ힳ펵隷", a_), Product.b("蒩", a_));
						num = 49;
						continue;
					}
					case 32:
						goto IL_18E;
					case 33:
						A_0.Type = LicenseType.Runtime;
						num = 0;
						continue;
					case 34:
					{
						Match match;
						if (match.Success)
						{
							num = 52;
							continue;
						}
						goto IL_6D5;
					}
					case 35:
						return A_0;
					case 36:
						if (product == null)
						{
							num = 15;
							continue;
						}
						num = 2;
						continue;
					case 37:
						goto IL_438;
					case 38:
						num = 9;
						continue;
					case 39:
					{
						if (A_0.IsUpdateRightExpired)
						{
							num = 35;
							continue;
						}
						DateTime? releaseDate = ReleaseDateAttribute.GetReleaseDate(assembly);
						num = 42;
						continue;
					}
					case 40:
						goto IL_1B6;
					case 41:
						goto IL_539;
					case 42:
					{
						DateTime? releaseDate;
						if (releaseDate != null)
						{
							num = 47;
							continue;
						}
						goto IL_18E;
					}
					case 43:
					{
						int num4;
						int num3 = packageAttribute.MajorVersion - num4;
						int num6;
						int num5 = packageAttribute.MinorVersion - num6;
						num = 17;
						continue;
					}
					case 44:
					{
						if (A_0.IsUpdateRightExpired)
						{
							num = 4;
							continue;
						}
						string version = product.Version;
						Match match = Regex.Match(version, Product.b("芩쪭鮯鮱颵銹\udabd", a_));
						num = 34;
						continue;
					}
					case 45:
					{
						PackageAttribute packageAttribute2;
						packageAttribute = packageAttribute2;
						Product product2;
						product = product2;
						num = 41;
						continue;
					}
					case 46:
						goto IL_2A3;
					case 47:
						num = 27;
						continue;
					case 48:
						if (A_0.IsUpdateRightExpired)
						{
							num = 26;
							continue;
						}
						num = 11;
						continue;
					case 49:
						if (A_0.OriginalVersion != null)
						{
							num = 22;
							continue;
						}
						goto IL_438;
					case 50:
					{
						int num3;
						if (num3 == 1)
						{
							num = 13;
							continue;
						}
						goto IL_6D5;
					}
					case 51:
						goto IL_237;
					case 52:
					{
						Match match;
						int num4 = Convert.ToInt32(match.Groups[1].Value);
						int num6 = Convert.ToInt32(match.Groups[2].Value);
						int num3 = 0;
						int num5 = 0;
						num = 5;
						continue;
					}
					case 53:
						goto IL_136;
					case 54:
						goto IL_704;
					case 55:
						goto IL_737;
					case 56:
						if (A_0.OriginalVersion == null)
						{
							num = 29;
							continue;
						}
						goto IL_237;
					case 57:
						if (A_0.OriginalVersion.a(1, 3) < 0)
						{
							num = 37;
							continue;
						}
						goto IL_2A3;
					case 58:
						goto IL_539;
					case 59:
						goto IL_691;
					case 60:
						goto IL_6D5;
					case 61:
						A_0.IsUpdateRightExpired = true;
						num = 60;
						continue;
					}
					if (A_0 == null)
					{
						num = 53;
						continue;
					}
					assembly = Assembly.GetAssembly(A_1);
					name = assembly.GetName();
					text = this.a(assembly);
					text = text.Replace(Product.b("蒩좭횯\udbb1ힳ펵隷", a_), Product.b("蒩", a_));
					package = PackageAttribute.GetPackage(assembly);
					packageAttribute = null;
					product = null;
					products = A_0.Products;
					num2 = 0;
					num = 3;
					continue;
					IL_18E:
					num = 44;
					continue;
					IL_1B6:
					num = 6;
					continue;
					IL_237:
					num = 1;
					continue;
					IL_2A3:
					num = 14;
					continue;
					IL_368:
					num = 36;
					continue;
					IL_438:
					num = 56;
					continue;
					IL_460:
					num = 18;
					continue;
					IL_539:
					num = 12;
					continue;
					IL_691:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_794:
						num = 8;
						continue;
					default:
						if (false)
						{
						}
						num = 30;
						continue;
					}
					IL_6D5:
					num = 48;
					continue;
					IL_704:
					DateTime t = DateTime.Now.ToUniversalTime();
					A_0.IsUpdateRightExpired = (A_0.ExpiredDate < t);
					num = 55;
					continue;
					IL_737:
					num = 39;
				}
				IL_136:
				return null;
				IL_307:
				return A_0;
				IL_387:
				return null;
			}
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00005458 File Offset: 0x00003658
		private static LicenseInfo a(Stream A_0)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return global::c.a(A_0);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000549C File Offset: 0x0000369C
		private static LicenseInfo a(string A_0)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return global::c.b(A_0);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000054E0 File Offset: 0x000036E0
		private LicenseInfo a(LicenseContext A_0, Type A_1, object A_2, bool A_3)
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

		// Token: 0x06000046 RID: 70 RVA: 0x0000551C File Offset: 0x0000371C
		private static LicenseInfo a(Type A_0)
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				int num = 10;
				for (;;)
				{
					List<Assembly> list;
					int num2;
					int num3;
					List<Assembly>.Enumerator enumerator;
					switch (num)
					{
					case 0:
						goto IL_745;
					case 1:
						if (LicenseProvider.c != null)
						{
							num = 7;
							continue;
						}
						num = 39;
						continue;
					case 2:
					{
						FileInfo fileInfo;
						FileStream fileStream = fileInfo.OpenRead();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_432;
						default:
							if (false)
							{
							}
							num = 30;
							continue;
						}
						break;
					}
					case 3:
						goto IL_426;
					case 4:
						goto IL_365;
					case 5:
						goto IL_33D;
					case 6:
					{
						FileStream fileStream2 = LicenseProvider.b.OpenRead();
						num = 13;
						continue;
					}
					case 7:
						goto IL_723;
					case 8:
						if (LicenseProvider.a != null)
						{
							num = 26;
							continue;
						}
						goto IL_527;
					case 9:
						list.Add(Assembly.GetAssembly(A_0));
						num = 4;
						continue;
					case 11:
						goto IL_725;
					case 12:
						goto IL_877;
					case 13:
						goto IL_46A;
					case 14:
						num = 16;
						continue;
					case 15:
						goto IL_33D;
					case 16:
					{
						string text;
						if (text.Length >= LicenseProvider.d.Length)
						{
							num = 36;
							continue;
						}
						goto IL_4BA;
					}
					case 17:
						try
						{
							FileStream fileStream3;
							return LicenseProvider.a(fileStream3);
						}
						finally
						{
							num = 0;
							for (;;)
							{
								FileStream fileStream3;
								switch (num)
								{
								case 1:
									((IDisposable)fileStream3).Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_423;
								}
								if (fileStream3 == null)
								{
									break;
								}
								num = 1;
							}
							IL_423:;
						}
						goto IL_426;
					case 18:
						goto IL_725;
					case 19:
						try
						{
							Assembly[] assemblies;
							string[] array = assemblies[num2].GetManifestResourceNames();
							goto IL_10F;
						}
						catch (Exception)
						{
							goto IL_10F;
						}
						goto Block_25;
						IL_10F:
						num = 35;
						continue;
					case 20:
					{
						string[] array2;
						if (num3 >= array2.Length)
						{
							num = 40;
							continue;
						}
						string text = array2[num3];
						num = 21;
						continue;
					}
					case 21:
					{
						string text;
						if (text != null)
						{
							num = 14;
							continue;
						}
						goto IL_4BA;
					}
					case 22:
					{
						FileInfo fileInfo2;
						if (fileInfo2.Exists)
						{
							num = 24;
							continue;
						}
						int num4;
						num4++;
						num = 31;
						continue;
					}
					case 23:
						num = 37;
						continue;
					case 24:
					{
						FileInfo fileInfo2;
						FileStream fileStream3 = fileInfo2.OpenRead();
						num = 17;
						continue;
					}
					case 25:
						goto IL_6BD;
					case 26:
					{
						FileInfo fileInfo = new FileInfo(LicenseProvider.a);
						num = 43;
						continue;
					}
					case 27:
						list = new List<Assembly>();
						num = 42;
						continue;
					case 28:
					{
						string[] array3 = new string[]
						{
							AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
							Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, Product.b("첤즦", a_))
						};
						int num4 = 0;
						num = 25;
						continue;
					}
					case 29:
					{
						try
						{
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 1:
								{
									Assembly assembly;
									FileInfo fileInfo3 = new FileInfo(assembly.Location);
									num = 10;
									continue;
								}
								case 2:
									goto IL_2AF;
								case 3:
								{
									FileInfo fileInfo4;
									FileStream fileStream4 = fileInfo4.OpenRead();
									num = 7;
									continue;
								}
								case 4:
								{
									Assembly assembly;
									if (assembly != null)
									{
										num = 1;
										continue;
									}
									break;
								}
								case 5:
									num = 2;
									continue;
								case 6:
								{
									if (!enumerator.MoveNext())
									{
										num = 5;
										continue;
									}
									Assembly assembly = enumerator.Current;
									num = 4;
									continue;
								}
								case 7:
									try
									{
										FileStream fileStream4;
										return LicenseProvider.a(fileStream4);
									}
									finally
									{
										num = 2;
										for (;;)
										{
											FileStream fileStream4;
											switch (num)
											{
											case 0:
												goto IL_1BE;
											case 1:
												((IDisposable)fileStream4).Dispose();
												num = 0;
												continue;
											}
											if (fileStream4 == null)
											{
												break;
											}
											num = 1;
										}
										IL_1BE:;
									}
									break;
								case 8:
								{
									FileInfo fileInfo3;
									string fileName = Path.Combine(fileInfo3.Directory.FullName, LicenseProvider.d);
									FileInfo fileInfo4 = new FileInfo(fileName);
									num = 9;
									continue;
								}
								case 9:
								{
									FileInfo fileInfo4;
									if (fileInfo4.Exists)
									{
										num = 3;
										continue;
									}
									break;
								}
								case 10:
								{
									FileInfo fileInfo3;
									if (fileInfo3.Exists)
									{
										num = 8;
										continue;
									}
									break;
								}
								}
								IL_1C1:
								num = 6;
								continue;
								goto IL_1C1;
							}
							IL_2AF:
							goto IL_7FA;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_2C2;
						IL_7FA:
						Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
						string value = Product.b("趢", a_) + LicenseProvider.d;
						num2 = assemblies.Length - 1;
						num = 18;
						continue;
					}
					case 30:
						try
						{
							FileStream fileStream;
							return LicenseProvider.a(fileStream);
						}
						finally
						{
							num = 1;
							for (;;)
							{
								FileStream fileStream;
								switch (num)
								{
								case 0:
									goto IL_4B7;
								case 2:
									((IDisposable)fileStream).Dispose();
									num = 0;
									continue;
								}
								if (fileStream == null)
								{
									break;
								}
								num = 2;
							}
							IL_4B7:;
						}
						goto IL_4BA;
					case 31:
						goto IL_6BD;
					case 32:
						goto IL_432;
					case 33:
					{
						string text;
						if (!text.Equals(LicenseProvider.d))
						{
							num = 38;
							continue;
						}
						goto IL_877;
					}
					case 34:
					{
						string[] array;
						if (array.Length > 0)
						{
							num = 41;
							continue;
						}
						goto IL_6E2;
					}
					case 35:
					{
						string[] array;
						if (array != null)
						{
							num = 49;
							continue;
						}
						goto IL_6E2;
					}
					case 36:
						num = 33;
						continue;
					case 37:
						if (LicenseProvider.b.Exists)
						{
							num = 6;
							continue;
						}
						goto IL_2C2;
					case 38:
						num = 47;
						continue;
					case 39:
						if (LicenseProvider.b != null)
						{
							num = 23;
							continue;
						}
						goto IL_2C2;
					case 40:
						goto IL_6E2;
					case 41:
					{
						string[] array;
						string[] array2 = array;
						num3 = 0;
						num = 15;
						continue;
					}
					case 42:
						if (A_0 != null)
						{
							num = 9;
							continue;
						}
						goto IL_365;
					case 43:
					{
						FileInfo fileInfo;
						if (fileInfo.Exists)
						{
							num = 2;
							continue;
						}
						goto IL_527;
					}
					case 44:
					{
						int num4;
						string[] array3;
						if (num4 >= array3.Length)
						{
							num = 27;
							continue;
						}
						string path = array3[num4];
						string fileName2 = Path.Combine(path, LicenseProvider.d);
						FileInfo fileInfo2 = new FileInfo(fileName2);
						num = 22;
						continue;
					}
					case 45:
					{
						if (num2 < 0)
						{
							num = 0;
							continue;
						}
						string[] array = null;
						num = 19;
						continue;
					}
					case 46:
						goto IL_44E;
					case 47:
					{
						string text;
						string value;
						if (text.EndsWith(value))
						{
							num = 12;
							continue;
						}
						goto IL_4BA;
					}
					case 48:
						if (LicenseProvider.d != null)
						{
							num = 28;
							continue;
						}
						goto IL_3D6;
					case 49:
						num = 34;
						continue;
					}
					if (LicenseProvider.f != null)
					{
						num = 3;
						continue;
					}
					goto IL_701;
					IL_2C2:
					num = 8;
					continue;
					IL_33D:
					num = 20;
					continue;
					IL_365:
					list.Add(Assembly.GetCallingAssembly());
					list.Add(Assembly.GetEntryAssembly());
					list.Add(Assembly.GetExecutingAssembly());
					enumerator = list.GetEnumerator();
					num = 29;
					continue;
					IL_426:
					num = 32;
					continue;
					IL_432:
					if (LicenseProvider.f.Length > 0)
					{
						num = 46;
						continue;
					}
					goto IL_701;
					IL_4BA:
					num3++;
					num = 5;
					continue;
					Block_25:
					LicenseInfo result;
					try
					{
						IL_877:
						for (;;)
						{
							if (true)
							{
							}
							string text;
							Assembly[] assemblies;
							Stream manifestResourceStream = assemblies[num2].GetManifestResourceStream(text);
							num = 5;
							for (;;)
							{
								switch (num)
								{
								case 0:
								{
									LicenseInfo licenseInfo;
									if (licenseInfo != null)
									{
										num = 3;
										continue;
									}
									goto IL_8F6;
								}
								case 1:
								{
									LicenseInfo licenseInfo = LicenseProvider.a(manifestResourceStream);
									num = 0;
									continue;
								}
								case 2:
									goto IL_902;
								case 3:
								{
									LicenseInfo licenseInfo;
									result = licenseInfo;
									num = 4;
									continue;
								}
								case 4:
									goto IL_8F4;
								case 5:
									if (manifestResourceStream != null)
									{
										num = 1;
										continue;
									}
									goto IL_8F6;
								}
								break;
								IL_8F6:
								num = 2;
							}
						}
						IL_8F4:
						return result;
						IL_902:
						goto IL_4BA;
					}
					catch (Exception)
					{
						goto IL_4BA;
					}
					return result;
					IL_527:
					num = 48;
					continue;
					IL_6BD:
					num = 44;
					continue;
					IL_6E2:
					num2--;
					num = 11;
					continue;
					IL_701:
					num = 1;
					continue;
					IL_725:
					num = 45;
				}
				IL_3D6:
				return null;
				IL_44E:
				goto IL_64D;
				IL_46A:
				try
				{
					FileStream fileStream2;
					return LicenseProvider.a(fileStream2);
				}
				finally
				{
					num = 2;
					for (;;)
					{
						FileStream fileStream2;
						switch (num)
						{
						case 0:
							((IDisposable)fileStream2).Dispose();
							num = 1;
							continue;
						case 1:
							goto IL_64A;
						}
						if (fileStream2 == null)
						{
							break;
						}
						num = 0;
					}
					IL_64A:;
				}
				IL_64D:
				return LicenseProvider.a(LicenseProvider.f);
				IL_723:
				return LicenseProvider.a(LicenseProvider.c);
				IL_745:
				goto IL_3D6;
			}
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00005F04 File Offset: 0x00004104
		// Note: this type is marked as 'beforefieldinit'.
		static LicenseProvider()
		{
			int a_ = 17;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			LicenseProvider.a = null;
			LicenseProvider.b = null;
			LicenseProvider.c = null;
			LicenseProvider.d = Product.b("\uddb0\udab2횴튶ힸ좺\ud8bc醾꓀꿂계꓆돊ꃌꏎ", a_);
			LicenseProvider.e = null;
			LicenseProvider.f = null;
			LicenseProvider.g = new d();
			LicenseProvider.h = new object();
		}

		// Token: 0x04000018 RID: 24
		private int \u25D9\u009C\u00A6\u0081;

		// Token: 0x04000019 RID: 25
		private bool \u2460\u0086\u007F\u00A8;

		// Token: 0x0400001A RID: 26
		private byte[] \u2609\u009B\u00B0\u0085;

		// Token: 0x0400001B RID: 27
		private static string a;

		// Token: 0x0400001C RID: 28
		private float[] \u2460\u008D\u00AB\u0095;

		// Token: 0x0400001D RID: 29
		private long \u2609\u00AC\u00AE\u0083;

		// Token: 0x0400001E RID: 30
		private long[] \u25D9\u00A6\u00A1\u009A;

		// Token: 0x0400001F RID: 31
		private static FileInfo b;

		// Token: 0x04000020 RID: 32
		private static Stream c;

		// Token: 0x04000021 RID: 33
		private static string d;

		// Token: 0x04000022 RID: 34
		private static Dictionary<string, LicenseInfo> e;

		// Token: 0x04000023 RID: 35
		private static string f;

		// Token: 0x04000024 RID: 36
		private static d g;

		// Token: 0x04000025 RID: 37
		private static object h;
	}
}
