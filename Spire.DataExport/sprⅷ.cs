using System;
using System.Globalization;
using Spire.DataExport.CollectionEditors;

// Token: 0x02000075 RID: 117
internal class spr\u2177
{
	// Token: 0x060003A9 RID: 937 RVA: 0x000229B8 File Offset: 0x000219B8
	public static NumberFormatInfo ᜀ()
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
		return spr\u2177.ᜀ;
	}

	// Token: 0x060003AA RID: 938 RVA: 0x000229F8 File Offset: 0x000219F8
	static spr\u2177()
	{
		int a_ = 14;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u2177.ᜀ = new NumberFormatInfo();
		spr\u2177.ᜀ.NumberDecimalSeparator = HyperlinksCollectionEditor.b("Щ", a_);
	}

	// Token: 0x060003AB RID: 939 RVA: 0x00022A60 File Offset: 0x00021A60
	public static bool ᜀ(double A_0)
	{
		if (A_0 < 65535.0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return false;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return Math.Ceiling(A_0) == A_0;
		}
		return false;
	}

	// Token: 0x060003AC RID: 940 RVA: 0x00022AB4 File Offset: 0x00021AB4
	public static float ᜂ(string A_0)
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
		double num = 0.0;
		double.TryParse(A_0, NumberStyles.Float, spr\u2177.ᜀ, out num);
		return (float)num;
	}

	// Token: 0x060003AD RID: 941 RVA: 0x00022B10 File Offset: 0x00021B10
	public static int ᜁ(string A_0)
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
		double num = 0.0;
		double.TryParse(A_0, NumberStyles.Integer, spr\u2177.ᜀ, out num);
		return (int)num;
	}

	// Token: 0x060003AE RID: 942 RVA: 0x00022B68 File Offset: 0x00021B68
	public static double ᜀ(string A_0)
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
		double result = 0.0;
		double.TryParse(A_0, NumberStyles.Float, spr\u2177.ᜀ, out result);
		return result;
	}

	// Token: 0x04000278 RID: 632
	private static NumberFormatInfo ᜀ;
}
