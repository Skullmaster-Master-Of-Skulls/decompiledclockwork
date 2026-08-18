using System;
using System.Text.RegularExpressions;
using Spire.License;
using Spire.License.V1_0;
using Spire.License.V1_1;
using Spire.License.V1_2;

// Token: 0x02000003 RID: 3
internal class a
{
	// Token: 0x06000005 RID: 5 RVA: 0x0000223C File Offset: 0x0000043C
	internal static Spire.License.LicenseInfo a(Spire.License.V1_0.LicenseInfo A_0)
	{
		switch (0)
		{
		default:
		{
			Spire.License.LicenseInfo licenseInfo;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return licenseInfo;
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					licenseInfo = new Spire.License.LicenseInfo
					{
						OriginalVersion = f.a(A_0.Version),
						Key = A_0.Key,
						Type = A_0.Type,
						Username = A_0.Username,
						Email = A_0.Email,
						Organization = A_0.Organization,
						LicensedDate = A_0.LicensedDate,
						ExpiredDate = A_0.LicensedDate.AddYears(1),
						Issuer = A_0.Issuer,
						IsUpdateRightExpired = A_0.IsUpdateRightExpired
					};
					licenseInfo.Products = new Spire.License.Product[A_0.Products.Length];
					int num = 0;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_109;
						case 1:
							if (num >= licenseInfo.Products.Length)
							{
								num2 = 2;
								continue;
							}
							licenseInfo.Products[num] = new Spire.License.Product
							{
								Name = A_0.Products[num].Name,
								Version = A_0.Products[num].Version,
								Subscription = new Spire.License.LicenseSubscription
								{
									NumberOfPermittedDeveloper = A_0.Products[num].Subscription.NumberOfPermitedDeveloper,
									NumberOfPermittedSite = A_0.Products[num].Subscription.NumberOfPermitedSite
								}
							};
							num++;
							num2 = 3;
							continue;
						case 2:
							return licenseInfo;
						case 3:
							goto IL_109;
						}
						break;
						IL_109:
						num2 = 1;
					}
					break;
				}
				}
			}
			return licenseInfo;
		}
		}
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002410 File Offset: 0x00000610
	internal static void a(Spire.License.LicenseInfo A_0, Spire.License.V1_0.LicenseInfo A_1)
	{
		for (;;)
		{
			A_1.Key = A_0.Key;
			A_1.Type = A_0.Type;
			A_1.Username = A_0.Username;
			A_1.Email = A_0.Email;
			A_1.Organization = A_0.Organization;
			A_1.LicensedDate = A_0.LicensedDate;
			A_1.Issuer = A_0.Issuer;
			A_1.IsUpdateRightExpired = A_0.IsUpdateRightExpired;
			A_1.Products = new Spire.License.V1_0.Product[A_0.Products.Length];
			int num = 0;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_172;
					default:
						goto IL_DC;
					}
					break;
				case 1:
					if (num >= A_0.Products.Length)
					{
						num2 = 0;
						continue;
					}
					A_1.Products[num] = new Spire.License.V1_0.Product
					{
						Name = A_0.Products[num].Name,
						Version = A_0.Products[num].Version,
						Subscription = new Spire.License.V1_0.LicenseSubscription
						{
							NumberOfPermitedDeveloper = A_0.Products[num].Subscription.NumberOfPermittedDeveloper,
							NumberOfPermitedSite = A_0.Products[num].Subscription.NumberOfPermittedSite
						}
					};
					num++;
					num2 = 2;
					continue;
				case 2:
					goto IL_172;
				case 3:
					if (true)
					{
					}
					goto IL_A2;
				}
				break;
				IL_A2:
				num2 = 1;
				continue;
				IL_172:
				goto IL_A2;
			}
		}
		IL_DC:
		if (false)
		{
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002594 File Offset: 0x00000794
	internal static Spire.License.LicenseInfo a(Spire.License.V1_1.LicenseInfo A_0)
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
		return new Spire.License.LicenseInfo
		{
			OriginalVersion = f.a(A_0.Version),
			Key = A_0.Key,
			Type = A_0.Type,
			Username = A_0.Username,
			Email = A_0.Email,
			Organization = A_0.Organization,
			LicensedDate = A_0.LicensedDate,
			ExpiredDate = A_0.LicensedDate.AddYears(1),
			Issuer = A_0.Issuer,
			IsUpdateRightExpired = A_0.IsUpdateRightExpired,
			Products = A_0.Products
		};
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000266C File Offset: 0x0000086C
	internal static void a(Spire.License.LicenseInfo A_0, Spire.License.V1_1.LicenseInfo A_1)
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
		A_1.Key = A_0.Key;
		A_1.Type = A_0.Type;
		A_1.Username = A_0.Username;
		A_1.Email = A_0.Email;
		A_1.Organization = A_0.Organization;
		A_1.LicensedDate = A_0.LicensedDate;
		A_1.Issuer = A_0.Issuer;
		A_1.IsUpdateRightExpired = A_0.IsUpdateRightExpired;
		A_1.Products = A_0.Products;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002714 File Offset: 0x00000914
	internal static Spire.License.LicenseInfo a(Spire.License.V1_2.LicenseInfo A_0)
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
		return new Spire.License.LicenseInfo
		{
			OriginalVersion = f.a(A_0.Version),
			Key = A_0.Key,
			Type = A_0.Type,
			Username = A_0.Username,
			Email = A_0.Email,
			Organization = A_0.Organization,
			LicensedDate = A_0.LicensedDate,
			ExpiredDate = A_0.ExpiredDate,
			Issuer = A_0.Issuer,
			IsUpdateRightExpired = A_0.IsUpdateRightExpired,
			Products = A_0.Products
		};
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000027E4 File Offset: 0x000009E4
	internal static void a(Spire.License.LicenseInfo A_0, Spire.License.V1_2.LicenseInfo A_1)
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
		A_1.Key = A_0.Key;
		A_1.Type = A_0.Type;
		A_1.Username = A_0.Username;
		A_1.Email = A_0.Email;
		A_1.Organization = A_0.Organization;
		A_1.LicensedDate = A_0.LicensedDate;
		A_1.ExpiredDate = A_0.ExpiredDate;
		A_1.Issuer = A_0.Issuer;
		A_1.IsUpdateRightExpired = A_0.IsUpdateRightExpired;
		A_1.Products = A_0.Products;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000028AC File Offset: 0x00000AAC
	// Note: this type is marked as 'beforefieldinit'.
	static a()
	{
		int a_ = 19;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		global::a.a = new Regex(Spire.License.V1_0.Product.b("鶴\uddb8邺钼黂鯆귈諐﷒裔ￖ藘뿚죠\udce2쇤", a_));
	}

	// Token: 0x04000004 RID: 4
	public static Regex a;
}
