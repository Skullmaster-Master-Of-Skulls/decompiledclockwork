using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Spire.License;
using Spire.License.V1_0;

// Token: 0x02000006 RID: 6
internal class d
{
	// Token: 0x06000025 RID: 37 RVA: 0x00002E9C File Offset: 0x0000109C
	private string a(Assembly A_0)
	{
		AssemblyProductAttribute assemblyProductAttribute = (AssemblyProductAttribute)Attribute.GetCustomAttribute(A_0, typeof(AssemblyProductAttribute));
		if (assemblyProductAttribute == null)
		{
			for (;;)
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
					goto IL_39;
				}
			}
			IL_39:
			if (false)
			{
			}
			AssemblyName name = A_0.GetName();
			return name.Name;
		}
		return assemblyProductAttribute.Product;
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002F08 File Offset: 0x00001108
	internal Spire.License.LicenseInfo a(Spire.License.LicenseInfo A_0, Type A_1, object A_2)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 12;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					PackageAttribute packageAttribute2;
					PackageAttribute packageAttribute = packageAttribute2;
					Spire.License.Product product2;
					Spire.License.Product product = product2;
					num = 60;
					continue;
				}
				case 1:
					goto IL_34D;
				case 2:
					return A_0;
				case 3:
					if (A_0.OriginalVersion.a(1, 3) < 0)
					{
						num = 30;
						continue;
					}
					goto IL_786;
				case 4:
					num = 48;
					continue;
				case 5:
				{
					DateTime? releaseDate;
					A_0.IsUpdateRightExpired = (A_0.ExpiredDate < releaseDate);
					num = 38;
					continue;
				}
				case 6:
					num = 46;
					continue;
				case 7:
					A_0.Type = LicenseType.Runtime;
					num = 1;
					continue;
				case 8:
					goto IL_5A8;
				case 9:
					if (A_0.OriginalVersion == null)
					{
						num = 4;
						continue;
					}
					goto IL_273;
				case 10:
				{
					if (true)
					{
					}
					Match match;
					int num2 = Convert.ToInt32(match.Groups[1].Value);
					int num3 = Convert.ToInt32(match.Groups[2].Value);
					int num4 = 0;
					int num5 = 0;
					num = 27;
					continue;
				}
				case 11:
					goto IL_24E;
				case 13:
					goto IL_4B0;
				case 14:
				{
					Spire.License.Product product2;
					Spire.License.Product product = product2;
					num = 28;
					continue;
				}
				case 15:
					goto IL_68F;
				case 16:
					goto IL_273;
				case 17:
				{
					DateTime? releaseDate;
					if (releaseDate != null)
					{
						num = 25;
						continue;
					}
					goto IL_45F;
				}
				case 18:
				{
					int num6;
					PackageAttribute[] array;
					if (num6 >= array.Length)
					{
						num = 35;
						continue;
					}
					PackageAttribute packageAttribute2 = array[num6];
					goto IL_65F;
				}
				case 19:
					goto IL_77F;
				case 20:
					return A_0;
				case 21:
					goto IL_6E8;
				case 22:
				{
					Spire.License.Product product;
					if (product == null)
					{
						num = 16;
						continue;
					}
					goto IL_760;
				}
				case 23:
					goto IL_786;
				case 24:
				{
					if (A_0.IsUpdateRightExpired)
					{
						num = 29;
						continue;
					}
					Assembly assembly;
					DateTime? releaseDate = ReleaseDateAttribute.GetReleaseDate(assembly);
					num = 17;
					continue;
				}
				case 25:
					num = 5;
					continue;
				case 26:
				{
					int num4;
					if (num4 == 1)
					{
						num = 51;
						continue;
					}
					goto IL_24E;
				}
				case 27:
				{
					PackageAttribute packageAttribute;
					if (packageAttribute != null)
					{
						num = 43;
						continue;
					}
					int num2;
					AssemblyName name;
					int num4 = name.Version.Major - num2;
					int num3;
					int num5 = name.Version.Minor - num3;
					num = 61;
					continue;
				}
				case 28:
					goto IL_760;
				case 29:
					return A_0;
				case 30:
					goto IL_437;
				case 31:
				{
					PackageAttribute packageAttribute2;
					string value;
					if (packageAttribute2.Name.Equals(value))
					{
						num = 0;
						continue;
					}
					int num6;
					num6++;
					num = 13;
					continue;
				}
				case 32:
				{
					Match match;
					if (match.Success)
					{
						num = 10;
						continue;
					}
					goto IL_24E;
				}
				case 33:
					goto IL_377;
				case 34:
				{
					PackageAttribute[] package;
					PackageAttribute[] array = package;
					int num6 = 0;
					num = 59;
					continue;
				}
				case 35:
					goto IL_584;
				case 36:
					goto IL_760;
				case 37:
				{
					if (A_0.IsUpdateRightExpired)
					{
						num = 2;
						continue;
					}
					Spire.License.Product product;
					string version = product.Version;
					Match match = Regex.Match(version, Spire.License.V1_0.Product.b("膨즬蒮颰鮴醸\ud9bc钾", a_));
					num = 32;
					continue;
				}
				case 38:
					goto IL_45F;
				case 39:
				{
					int num4;
					if (num4 > 1)
					{
						num = 54;
						continue;
					}
					num = 26;
					continue;
				}
				case 40:
				{
					Spire.License.Product product;
					if (product == null)
					{
						num = 19;
						continue;
					}
					num = 42;
					continue;
				}
				case 41:
					if (A_0.Type == LicenseType.Temporary)
					{
						num = 7;
						continue;
					}
					return A_0;
				case 42:
					if (A_0.Type != LicenseType.Demo)
					{
						num = 52;
						continue;
					}
					goto IL_377;
				case 43:
				{
					PackageAttribute packageAttribute;
					int num2;
					int num4 = packageAttribute.MajorVersion - num2;
					int num3;
					int num5 = packageAttribute.MinorVersion - num3;
					num = 8;
					continue;
				}
				case 44:
				{
					if (d.a.Next(2147483647) % 11 != (int)(A_0.Key[0] % '\v'))
					{
						num = 49;
						continue;
					}
					Assembly assembly = Assembly.GetAssembly(A_1);
					AssemblyName name = assembly.GetName();
					string text = this.a(assembly);
					text = text.Replace(Spire.License.V1_0.Product.b("螨쮬즮\ud8b0킲킴馶", a_), Spire.License.V1_0.Product.b("螨", a_));
					PackageAttribute[] package = PackageAttribute.GetPackage(assembly);
					PackageAttribute packageAttribute = null;
					Spire.License.Product product = null;
					Spire.License.Product[] products = A_0.Products;
					int num7 = 0;
					num = 15;
					continue;
				}
				case 45:
				{
					string value;
					string text;
					if (!text.Equals(value))
					{
						int num7;
						num7++;
						num = 63;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_65F;
					default:
						if (false)
						{
						}
						num = 14;
						continue;
					}
					break;
				}
				case 46:
				{
					PackageAttribute[] package;
					if (package.Length > 0)
					{
						num = 34;
						continue;
					}
					goto IL_273;
				}
				case 47:
					goto IL_24E;
				case 48:
					if (A_0.Version2.a(1, 3) >= 0)
					{
						num = 23;
						continue;
					}
					goto IL_273;
				case 49:
					return A_0;
				case 50:
					goto IL_13E;
				case 51:
				{
					int num5;
					A_0.IsUpdateRightExpired = (num5 >= 0);
					num = 47;
					continue;
				}
				case 52:
					num = 58;
					continue;
				case 53:
				{
					Spire.License.Product[] products;
					int num7;
					if (num7 >= products.Length)
					{
						num = 36;
						continue;
					}
					Spire.License.Product product2 = products[num7];
					string value = product2.Name.Replace(Spire.License.V1_0.Product.b("螨쮬즮\ud8b0킲킴馶", a_), Spire.License.V1_0.Product.b("螨", a_));
					num = 55;
					continue;
				}
				case 54:
					A_0.IsUpdateRightExpired = true;
					num = 11;
					continue;
				case 55:
					if (A_0.OriginalVersion != null)
					{
						num = 57;
						continue;
					}
					goto IL_437;
				case 56:
				{
					PackageAttribute[] package;
					if (package != null)
					{
						num = 6;
						continue;
					}
					goto IL_273;
				}
				case 57:
					num = 3;
					continue;
				case 58:
					if (A_0.Type == LicenseType.Temporary)
					{
						num = 33;
						continue;
					}
					goto IL_6E8;
				case 59:
					goto IL_4B0;
				case 60:
					goto IL_584;
				case 61:
					goto IL_5A8;
				case 62:
					if (A_0.IsUpdateRightExpired)
					{
						num = 20;
						continue;
					}
					num = 41;
					continue;
				case 63:
					goto IL_68F;
				}
				if (A_0 == null)
				{
					num = 50;
					continue;
				}
				num = 44;
				continue;
				IL_24E:
				num = 62;
				continue;
				IL_273:
				num = 45;
				continue;
				IL_377:
				DateTime t = DateTime.Now.ToUniversalTime();
				A_0.IsUpdateRightExpired = (A_0.ExpiredDate < t);
				num = 21;
				continue;
				IL_437:
				num = 9;
				continue;
				IL_45F:
				num = 37;
				continue;
				IL_4B0:
				num = 18;
				continue;
				IL_584:
				num = 22;
				continue;
				IL_5A8:
				num = 39;
				continue;
				IL_65F:
				num = 31;
				continue;
				IL_68F:
				num = 53;
				continue;
				IL_6E8:
				num = 24;
				continue;
				IL_760:
				num = 40;
				continue;
				IL_786:
				num = 56;
			}
			IL_13E:
			return null;
			IL_34D:
			return A_0;
			IL_77F:
			return null;
		}
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00003718 File Offset: 0x00001918
	// Note: this type is marked as 'beforefieldinit'.
	static d()
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
		d.a = new Random();
	}

	// Token: 0x0400000F RID: 15
	private static Random a;
}
