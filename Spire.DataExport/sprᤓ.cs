using System;
using System.Drawing;
using System.Text;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.PDF;

// Token: 0x0200005E RID: 94
internal abstract class sprᤓ
{
	// Token: 0x06000310 RID: 784 RVA: 0x0001D3E0 File Offset: 0x0001C3E0
	public static double ᜁ(int A_0)
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
		return (double)((float)A_0 / 72f);
	}

	// Token: 0x06000311 RID: 785 RVA: 0x0001D424 File Offset: 0x0001C424
	public static double ᜀ(int A_0)
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
		return (double)((float)A_0 / 72f * 25.4f);
	}

	// Token: 0x06000312 RID: 786 RVA: 0x0001D470 File Offset: 0x0001C470
	public static int ᜁ(double A_0)
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
		return (int)(A_0 * 72.0);
	}

	// Token: 0x06000313 RID: 787 RVA: 0x0001D4B8 File Offset: 0x0001C4B8
	public static int ᜀ(double A_0)
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
		return (int)(A_0 / 25.399999618530273 * 72.0);
	}

	// Token: 0x06000314 RID: 788 RVA: 0x0001D50C File Offset: 0x0001C50C
	public static double ᜀ(double A_0, int A_1)
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
		return Math.Round(A_0 * Math.Pow(10.0, (double)A_1)) / Math.Pow(10.0, (double)A_1);
	}

	// Token: 0x06000315 RID: 789 RVA: 0x0001D570 File Offset: 0x0001C570
	public static double ᜀ(PageUnits A_0, int A_1)
	{
		for (;;)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					switch (A_0)
					{
					case PageUnits.Inch:
						goto IL_70;
					case PageUnits.Millimeter:
						goto IL_64;
					}
					goto IL_34;
				case 1:
					goto IL_88;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				break;
				IL_34:
				num = 2;
			}
		}
		IL_64:
		return Math.Round(sprᤓ.ᜀ(A_1));
		IL_70:
		return sprᤓ.ᜀ(sprᤓ.ᜁ(A_1), 2);
		IL_88:
		return (double)A_1;
	}

	// Token: 0x06000316 RID: 790 RVA: 0x0001D60C File Offset: 0x0001C60C
	public static int ᜀ(PageUnits A_0, double A_1)
	{
		for (;;)
		{
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7A;
				case 1:
					switch (A_0)
					{
					case PageUnits.Inch:
						goto IL_6B;
					case PageUnits.Millimeter:
						goto IL_64;
					}
					goto IL_34;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				break;
				IL_34:
				num = 2;
			}
		}
		IL_64:
		return sprᤓ.ᜀ(A_1);
		IL_6B:
		return sprᤓ.ᜁ(A_1);
		IL_7A:
		return (int)A_1;
	}

	// Token: 0x06000317 RID: 791 RVA: 0x0001D698 File Offset: 0x0001C698
	public static double ᜁ(PageFormat A_0)
	{
		for (;;)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					goto IL_B3;
				case 2:
					for (;;)
					{
						switch (A_0)
						{
						case PageFormat.Letter:
							goto IL_82;
						case PageFormat.Legal:
							goto IL_D3;
						case PageFormat.A3:
							goto IL_C9;
						case PageFormat.A4:
							goto IL_DD;
						case PageFormat.A5:
							goto IL_78;
						case PageFormat.B5_JIS:
							goto IL_B5;
						case PageFormat.US_Std_Fanfold:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								goto IL_68;
							}
							break;
						case PageFormat.Fanfold:
							goto IL_8C;
						case PageFormat.User:
							goto IL_BF;
						}
						break;
					}
					num = 0;
					continue;
				}
				break;
			}
		}
		IL_68:
		if (false)
		{
		}
		return 14.88;
		IL_78:
		return 5.83;
		IL_82:
		return 8.5;
		IL_8C:
		if (true)
		{
		}
		return 8.5;
		IL_B3:
		return 8.27;
		IL_B5:
		return 7.17;
		IL_BF:
		return 8.27;
		IL_C9:
		return 11.69;
		IL_D3:
		return 8.5;
		IL_DD:
		return 8.27;
	}

	// Token: 0x06000318 RID: 792 RVA: 0x0001D798 File Offset: 0x0001C798
	public static double ᜀ(PageFormat A_0)
	{
		for (;;)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					goto IL_B3;
				case 2:
					for (;;)
					{
						switch (A_0)
						{
						case PageFormat.Letter:
							goto IL_8A;
						case PageFormat.Legal:
							goto IL_D3;
						case PageFormat.A3:
							goto IL_C9;
						case PageFormat.A4:
							goto IL_DD;
						case PageFormat.A5:
							goto IL_80;
						case PageFormat.B5_JIS:
							goto IL_B5;
						case PageFormat.US_Std_Fanfold:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								goto IL_70;
							}
							break;
						case PageFormat.Fanfold:
							goto IL_9E;
						case PageFormat.User:
							goto IL_BF;
						}
						break;
					}
					if (true)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
		}
		IL_70:
		if (false)
		{
		}
		return 11.0;
		IL_80:
		return 8.27;
		IL_8A:
		return 11.0;
		IL_9E:
		return 12.0;
		IL_B3:
		return 11.69;
		IL_B5:
		return 10.12;
		IL_BF:
		return 11.69;
		IL_C9:
		return 16.54;
		IL_D3:
		return 14.0;
		IL_DD:
		return 11.69;
	}

	// Token: 0x06000319 RID: 793 RVA: 0x0001D898 File Offset: 0x0001C898
	public static int ᜀ(string A_0, PdfFont A_1)
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
		Font font = (Font)A_1.CustomFont.Clone();
		Graphics graphics = Graphics.FromHwnd((IntPtr)0);
		return (int)graphics.MeasureString(A_0, font).Width;
	}

	// Token: 0x0600031A RID: 794 RVA: 0x0001D904 File Offset: 0x0001C904
	public static int ᜀ(double A_0, PdfFont A_1)
	{
		int num2;
		for (;;)
		{
			int num;
			int num3;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_65:
				num = 3;
				break;
			default:
				if (false)
				{
				}
				num2 = 0;
				num3 = 0;
				num = 1;
				break;
			}
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (num3 >= A_1.ReturnFontLength())
					{
						goto IL_65;
					}
					num2 += A_1.GetWidth(num3);
					num3++;
					num = 2;
					continue;
				case 1:
					goto IL_54;
				case 2:
					goto IL_54;
				case 3:
					goto IL_6D;
				}
				break;
				IL_54:
				num = 0;
			}
		}
		IL_6D:
		return (int)(A_0 * (double)num2 / (double)A_1.ReturnFontLength());
	}

	// Token: 0x0600031B RID: 795 RVA: 0x0001D9A8 File Offset: 0x0001C9A8
	public static int ᜀ(string A_0, int A_1, int A_2, PDFExport A_3)
	{
		switch (0)
		{
		default:
		{
			int num = 18;
			double num5;
			for (;;)
			{
				int num2;
				byte[] bytes;
				int num3;
				int num4;
				int num6;
				int codepage;
				switch (num)
				{
				case 0:
					if (A_3.DataEncoding == PDFEncodingType.OEM)
					{
						num = 29;
						continue;
					}
					goto IL_2AF;
				case 1:
					goto IL_284;
				case 2:
					num = 4;
					continue;
				case 3:
					num2 = 0;
					num = 43;
					continue;
				case 4:
					if (A_2 <= 12)
					{
						num = 11;
						continue;
					}
					goto IL_15F;
				case 5:
					goto IL_4F3;
				case 6:
					if (true)
					{
					}
					if (bytes[num3] != 0)
					{
						num = 15;
						continue;
					}
					goto IL_51A;
				case 7:
					goto IL_2AF;
				case 8:
					goto IL_3B6;
				case 9:
					if (A_2 == 13)
					{
						num = 20;
						continue;
					}
					goto IL_531;
				case 10:
					goto IL_435;
				case 11:
					num4 = 0;
					num = 35;
					continue;
				case 12:
					goto IL_265;
				case 13:
					if (A_2 >= 4)
					{
						num = 27;
						continue;
					}
					goto IL_3B6;
				case 14:
					goto IL_36D;
				case 15:
					num5 += (double)((int)((float)(A_1 * sprᤓ.\u171A[(int)bytes[num3]]) / 1000f));
					num = 31;
					continue;
				case 16:
					if (num2 >= bytes.Length)
					{
						num = 39;
						continue;
					}
					num = 48;
					continue;
				case 17:
					num6 = 0;
					num = 5;
					continue;
				case 19:
					if (bytes[num4] != 0)
					{
						num = 28;
						continue;
					}
					goto IL_36D;
				case 20:
					num3 = 0;
					num = 12;
					continue;
				case 21:
					num = 42;
					continue;
				case 22:
					num5 += Math.Floor((double)((float)A_1 * ((float)sprᤓ.\u171C[(int)bytes[num2]] / 1000f)));
					num = 25;
					continue;
				case 23:
					return 0;
				case 24:
					return 0;
				case 25:
					goto IL_3DA;
				case 26:
					goto IL_124;
				case 27:
					num = 38;
					continue;
				case 28:
					num5 += (double)((int)((float)(A_1 * sprᤓ.\u1719[(int)bytes[num4]]) / 1000f));
					num = 14;
					continue;
				case 29:
					codepage = A_3.Culture.TextInfo.OEMCodePage;
					num = 7;
					continue;
				case 30:
					goto IL_4F3;
				case 31:
					goto IL_51A;
				case 32:
					if (num4 >= bytes.Length)
					{
						num = 44;
						continue;
					}
					num = 19;
					continue;
				case 33:
					if (num6 >= bytes.Length)
					{
						num = 8;
						continue;
					}
					num = 37;
					continue;
				case 34:
					if (num3 >= bytes.Length)
					{
						num = 1;
						continue;
					}
					num = 6;
					continue;
				case 35:
					goto IL_40E;
				case 36:
					if (A_3.Culture == null)
					{
						num = 24;
						continue;
					}
					codepage = A_3.Culture.TextInfo.ANSICodePage;
					num = 0;
					continue;
				case 37:
					if (bytes[num6] != 0)
					{
						num = 40;
						continue;
					}
					goto IL_124;
				case 38:
					if (A_2 <= 7)
					{
						num = 17;
						continue;
					}
					goto IL_3B6;
				case 39:
					goto IL_289;
				case 40:
					num5 += (double)((int)((float)(A_1 * sprᤓ.\u171B[(int)bytes[num6]]) / 1000f));
					num = 26;
					continue;
				case 41:
					if (A_2 >= 8)
					{
						num = 2;
						continue;
					}
					goto IL_15F;
				case 42:
					if (A_2 <= 3)
					{
						num = 3;
						continue;
					}
					goto IL_289;
				case 43:
					goto IL_435;
				case 44:
					goto IL_15F;
				case 45:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (A_2 < 0)
						{
							goto IL_289;
						}
						break;
					}
					num = 21;
					continue;
				case 46:
					goto IL_40E;
				case 47:
					goto IL_265;
				case 48:
					if (bytes[num2] != 0)
					{
						num = 22;
						continue;
					}
					goto IL_3DA;
				}
				if (A_3 == null)
				{
					num = 23;
					continue;
				}
				num = 36;
				continue;
				IL_124:
				num6++;
				num = 30;
				continue;
				IL_15F:
				num = 9;
				continue;
				IL_265:
				num = 34;
				continue;
				IL_289:
				num = 13;
				continue;
				IL_2AF:
				bytes = Encoding.GetEncoding(codepage).GetBytes(A_0);
				num5 = 0.0;
				num = 45;
				continue;
				IL_36D:
				num4++;
				num = 46;
				continue;
				IL_3B6:
				num = 41;
				continue;
				IL_3DA:
				num2++;
				num = 10;
				continue;
				IL_40E:
				num = 32;
				continue;
				IL_435:
				num = 16;
				continue;
				IL_4F3:
				num = 33;
				continue;
				IL_51A:
				num3++;
				num = 47;
			}
			return 0;
			IL_284:
			IL_531:
			return (int)num5;
		}
		}
	}

	// Token: 0x0600031C RID: 796 RVA: 0x0001DEE8 File Offset: 0x0001CEE8
	public static int ᜀ(double A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				num = 0;
				int num2 = 26;
				for (;;)
				{
					int num4;
					switch (num2)
					{
					case 0:
						goto IL_1D1;
					case 1:
						goto IL_2EC;
					case 2:
						if (A_2 <= 7)
						{
							num2 = 12;
							continue;
						}
						goto IL_D7;
					case 3:
						goto IL_2EC;
					case 4:
						if (A_2 <= 3)
						{
							num2 = 21;
							continue;
						}
						goto IL_1D1;
					case 5:
						goto IL_117;
					case 6:
						goto IL_D7;
					case 7:
						goto IL_244;
					case 8:
						if (A_2 < 8)
						{
							goto IL_1F5;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_226;
						default:
							if (false)
							{
							}
							num2 = 10;
							continue;
						}
						break;
					case 9:
					{
						int num3;
						if (num3 > sprᤓ.\u171B.GetUpperBound(0))
						{
							num2 = 6;
							continue;
						}
						num += sprᤓ.\u171B[num3];
						num3++;
						num2 = 22;
						continue;
					}
					case 10:
						num2 = 13;
						continue;
					case 11:
						if (A_2 >= 4)
						{
							num2 = 28;
							continue;
						}
						goto IL_D7;
					case 12:
					{
						int num3 = 0;
						num2 = 27;
						continue;
					}
					case 13:
						if (A_2 <= 12)
						{
							num2 = 23;
							continue;
						}
						goto IL_1F5;
					case 14:
						num4 = 0;
						num2 = 17;
						continue;
					case 15:
						num2 = 4;
						continue;
					case 16:
						goto IL_226;
					case 17:
						goto IL_21A;
					case 18:
					{
						int num5;
						if (num5 > sprᤓ.\u171C.GetUpperBound(0))
						{
							if (true)
							{
							}
							num2 = 0;
							continue;
						}
						num += sprᤓ.\u171C[num5];
						num5++;
						num2 = 19;
						continue;
					}
					case 19:
						goto IL_117;
					case 20:
						goto IL_21A;
					case 21:
					{
						int num5 = 0;
						num2 = 5;
						continue;
					}
					case 22:
						goto IL_1A3;
					case 23:
					{
						int num6 = 0;
						num2 = 3;
						continue;
					}
					case 24:
					{
						int num6;
						if (num6 > sprᤓ.\u1719.GetUpperBound(0))
						{
							num2 = 25;
							continue;
						}
						num += sprᤓ.\u1719[num6];
						num6++;
						num2 = 1;
						continue;
					}
					case 25:
						goto IL_1F5;
					case 26:
						if (A_2 >= 0)
						{
							num2 = 15;
							continue;
						}
						goto IL_1D1;
					case 27:
						goto IL_1A3;
					case 28:
						num2 = 2;
						continue;
					case 29:
						if (A_2 == 13)
						{
							num2 = 14;
							continue;
						}
						goto IL_33F;
					}
					break;
					IL_D7:
					num2 = 8;
					continue;
					IL_117:
					num2 = 18;
					continue;
					IL_226:
					if (num4 > sprᤓ.\u171A.GetUpperBound(0))
					{
						num2 = 7;
						continue;
					}
					num += sprᤓ.\u171A[num4];
					num4++;
					num2 = 20;
					continue;
					IL_1A3:
					num2 = 9;
					continue;
					IL_1D1:
					num2 = 11;
					continue;
					IL_1F5:
					num2 = 29;
					continue;
					IL_21A:
					num2 = 16;
					continue;
					IL_2EC:
					num2 = 24;
				}
			}
			IL_244:
			IL_33F:
			return (int)(A_0 * (double)A_1 * (double)num / 256.0 / 1000.0);
		}
		}
	}

	// Token: 0x0600031E RID: 798 RVA: 0x0001E264 File Offset: 0x0001D264
	// Note: this type is marked as 'beforefieldinit'.
	static sprᤓ()
	{
		int a_ = 9;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		sprᤓ.\u1717 = new string[]
		{
			HyperlinksCollectionEditor.b("洤䈦䔨崪䠬嬮堰倲吴", a_),
			HyperlinksCollectionEditor.b("洤䈦䔨崪䠬嬮堰倲吴ᨶ笸吺儼嬾", a_),
			HyperlinksCollectionEditor.b("洤䈦䔨崪䠬嬮堰倲吴ᨶ瘸夺儼嘾぀㙂⁄", a_),
			HyperlinksCollectionEditor.b("洤䈦䔨崪䠬嬮堰倲吴ᨶ笸吺儼嬾เ⅂⥄⹆㡈㹊⡌", a_),
			HyperlinksCollectionEditor.b("昤䠦尨太䐬䨮䌰", a_),
			HyperlinksCollectionEditor.b("昤䠦尨太䐬䨮䌰Ḳ眴堶唸强", a_),
			HyperlinksCollectionEditor.b("昤䠦尨太䐬䨮䌰Ḳ稴唶唸刺䰼䨾⑀", a_),
			HyperlinksCollectionEditor.b("昤䠦尨太䐬䨮䌰Ḳ眴堶唸强爼崾ⵀ⩂㑄㉆ⱈ", a_),
			HyperlinksCollectionEditor.b("焤並䐨个帬Ȯ挰尲場嘶圸", a_),
			HyperlinksCollectionEditor.b("焤並䐨个帬Ȯ猰尲头匶", a_),
			HyperlinksCollectionEditor.b("焤並䐨个帬Ȯ砰䜲吴嬶倸堺", a_),
			HyperlinksCollectionEditor.b("焤並䐨个帬Ȯ猰尲头匶瀸伺尼匾⡀⁂", a_),
			HyperlinksCollectionEditor.b("瘤带䐨䤪䈬䌮", a_),
			HyperlinksCollectionEditor.b("缤䘦夨䴪椬䘮弰吲圴嘶䴸䠺", a_)
		};
		sprᤓ.\u1718 = new string[]
		{
			HyperlinksCollectionEditor.b("瘤匦䠨䔪䤬丮䌰圲瀴夶娸吺夼嘾⽀⑂", a_),
			HyperlinksCollectionEditor.b("爤並䜨横䌬尮堰瘲嬴吶嘸强吼儾♀", a_),
			HyperlinksCollectionEditor.b("栤䘦䨨礪䈬䈮倰崲瀴夶娸吺夼嘾⽀⑂", a_),
			HyperlinksCollectionEditor.b("甤挦漨漪䈬䰮琰崲嘴堶崸刺匼堾", a_)
		};
		sprᤓ.\u1719 = new int[]
		{
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			250,
			333,
			408,
			500,
			500,
			833,
			778,
			333,
			333,
			333,
			500,
			564,
			250,
			333,
			250,
			278,
			500,
			500,
			500,
			500,
			500,
			500,
			500,
			500,
			500,
			500,
			278,
			278,
			564,
			564,
			564,
			444,
			921,
			722,
			667,
			667,
			722,
			611,
			556,
			722,
			722,
			333,
			389,
			722,
			611,
			889,
			722,
			722,
			556,
			722,
			667,
			556,
			611,
			722,
			722,
			944,
			722,
			722,
			611,
			333,
			278,
			333,
			469,
			500,
			333,
			444,
			500,
			444,
			500,
			444,
			333,
			500,
			500,
			278,
			278,
			500,
			278,
			778,
			500,
			500,
			500,
			500,
			333,
			389,
			278,
			500,
			500,
			722,
			500,
			500,
			444,
			480,
			200,
			480,
			541,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			300,
			333,
			500,
			500,
			167,
			500,
			500,
			500,
			500,
			180,
			444,
			500,
			333,
			333,
			556,
			556,
			663,
			500,
			500,
			500,
			250,
			663,
			453,
			350,
			333,
			444,
			444,
			500,
			100,
			100,
			663,
			444,
			663,
			333,
			333,
			333,
			333,
			333,
			333,
			333,
			333,
			663,
			333,
			333,
			663,
			333,
			333,
			333,
			100,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			889,
			663,
			276,
			663,
			663,
			663,
			663,
			611,
			722,
			889,
			310,
			663,
			663,
			663,
			663,
			663,
			667,
			663,
			663,
			663,
			278,
			663,
			663,
			278,
			500,
			722,
			500,
			663,
			663,
			663,
			663
		};
		sprᤓ.\u171A = new int[]
		{
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			278,
			974,
			961,
			974,
			980,
			719,
			789,
			790,
			791,
			690,
			960,
			939,
			549,
			855,
			911,
			933,
			911,
			945,
			974,
			755,
			846,
			762,
			761,
			571,
			677,
			763,
			760,
			759,
			754,
			494,
			552,
			537,
			577,
			692,
			786,
			788,
			788,
			790,
			793,
			794,
			816,
			823,
			789,
			841,
			823,
			833,
			816,
			831,
			923,
			744,
			723,
			749,
			790,
			792,
			695,
			776,
			768,
			792,
			759,
			707,
			708,
			682,
			701,
			826,
			815,
			789,
			789,
			707,
			687,
			696,
			689,
			786,
			787,
			713,
			791,
			785,
			791,
			873,
			761,
			762,
			762,
			759,
			759,
			892,
			892,
			788,
			784,
			438,
			138,
			277,
			415,
			392,
			392,
			668,
			668,
			741,
			390,
			390,
			317,
			317,
			276,
			276,
			509,
			509,
			410,
			410,
			234,
			234,
			334,
			334,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			741,
			550,
			732,
			544,
			544,
			910,
			667,
			760,
			760,
			776,
			595,
			694,
			626,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			788,
			894,
			838,
			101,
			458,
			748,
			924,
			748,
			918,
			927,
			928,
			928,
			834,
			873,
			828,
			924,
			924,
			917,
			930,
			931,
			463,
			883,
			836,
			836,
			867,
			867,
			696,
			696,
			874,
			741,
			874,
			760,
			946,
			771,
			865,
			771,
			888,
			967,
			888,
			831,
			873,
			927,
			970,
			918,
			741
		};
		sprᤓ.\u171B = new int[]
		{
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600,
			600
		};
		sprᤓ.\u171C = new int[]
		{
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			278,
			278,
			355,
			556,
			556,
			889,
			667,
			222,
			333,
			333,
			389,
			584,
			278,
			333,
			278,
			278,
			556,
			556,
			556,
			556,
			556,
			556,
			556,
			556,
			556,
			556,
			278,
			278,
			584,
			584,
			584,
			556,
			101,
			667,
			667,
			722,
			722,
			667,
			611,
			778,
			722,
			278,
			500,
			667,
			556,
			833,
			722,
			778,
			667,
			778,
			722,
			667,
			611,
			722,
			667,
			944,
			667,
			667,
			611,
			278,
			278,
			278,
			469,
			556,
			222,
			556,
			556,
			500,
			556,
			556,
			278,
			556,
			556,
			222,
			222,
			500,
			222,
			833,
			556,
			556,
			556,
			556,
			333,
			500,
			278,
			556,
			500,
			722,
			500,
			500,
			500,
			334,
			260,
			334,
			584,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			400,
			333,
			556,
			556,
			167,
			556,
			556,
			556,
			556,
			191,
			333,
			556,
			333,
			333,
			500,
			500,
			663,
			556,
			556,
			556,
			278,
			663,
			537,
			350,
			222,
			333,
			333,
			556,
			100,
			100,
			663,
			611,
			663,
			333,
			333,
			333,
			333,
			333,
			333,
			333,
			333,
			663,
			333,
			333,
			663,
			333,
			333,
			333,
			100,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			100,
			663,
			370,
			663,
			663,
			663,
			663,
			556,
			778,
			100,
			365,
			663,
			663,
			663,
			663,
			663,
			889,
			663,
			663,
			663,
			278,
			663,
			663,
			222,
			611,
			944,
			611,
			663,
			663,
			663,
			663
		};
	}

	// Token: 0x040000D6 RID: 214
	public const string ᜀ = "%PDF-1.3\r";

	// Token: 0x040000D7 RID: 215
	public const string ᜁ = "{0} 0 obj\r<< /Producer ({1})\r/Author ({2})\r/CreationDate ({3})\r/Creator ({4})\r/Keywords ({5})\r/Subject ({6})\r/Title ({7})\r/ModDate ({8})\r>>\rendobj\r";

	// Token: 0x040000D8 RID: 216
	public const string ᜂ = "{0} 0 obj\r<< /Type /Catalog \r/Pages {1} 0 R \r/Outlines {2} 0 R \r>>\rendobj\r";

	// Token: 0x040000D9 RID: 217
	public const string ᜃ = "{0} 0 obj\r<< /Type /Outlines\r/Count 0\r>>\rendobj\r";

	// Token: 0x040000DA RID: 218
	public const string ᜄ = "{0} 0 obj\r<< /Type /Pages\r/Count {1}\r/Kids [{2}]\r>>\rendobj\r";

	// Token: 0x040000DB RID: 219
	public const string ᜅ = "{0} 0 obj\r<< /Type /Font\r/Subtype /Type1\r/Name /{1}\r/BaseFont /{2}\r/Encoding /{3}\r/FirstChar 0\r/LastChar 255\r>>\rendobj\r";

	// Token: 0x040000DC RID: 220
	public const string ᜆ = "{0} 0 obj\r<< /Type /Font\r/Subtype /TrueType\r/Name /{1}\r/BaseFont /{2}\r/Encoding /{3}\r/FirstChar 0\r/LastChar 255\r/Widths [{4}]\r/FontDescriptor <</Type /FontDescriptor /Flags 32 /FontBBox [0 0 0 0]>>\r>>\rendobj\r";

	// Token: 0x040000DD RID: 221
	public const string ᜇ = "{0} 0 obj\r<< /Font <<{1} >> /ProcSet [ /PDF /Text ] >>\rendobj\r";

	// Token: 0x040000DE RID: 222
	public const string ᜈ = "{0} 0 obj\r<< /Type /Page\r/Parent {1} 0 R\r/Resources {2} 0 R\r/MediaBox [{3} {4} {5} {6}]\r/TrimBox [{7} {8} {9} {10}]\r/Contents {11}\r>>\rendobj\r";

	// Token: 0x040000DF RID: 223
	public const string ᜉ = "{0} 0 obj\r<< /Length {1} 0 R >>\rstream\r";

	// Token: 0x040000E0 RID: 224
	public const string ᜊ = "endstream\rendobj\r";

	// Token: 0x040000E1 RID: 225
	public const string ᜋ = "BT\r/{0} {1} Tf\r{2} {3} Td ({4}) Tj\rET\r";

	// Token: 0x040000E2 RID: 226
	public const string ᜌ = "{0} {1} {2} rg\r";

	// Token: 0x040000E3 RID: 227
	public const string \u170D = "{0:f2} {1:f2} {2:f2} RG\r";

	// Token: 0x040000E4 RID: 228
	public const string ᜎ = "{0:f1} {1:f1} m \r{2:f1} {3:f1} l \r{4:f1} w \rS\r";

	// Token: 0x040000E5 RID: 229
	public const string ᜏ = "{0:f1} {1:f1} {2:f1} {3:f1} re {4}\r";

	// Token: 0x040000E6 RID: 230
	public const string ᜐ = "{0} 0 obj\r{1} \rendobj\r";

	// Token: 0x040000E7 RID: 231
	public const string ᜑ = "xref\r0 {0}\r0000000000 65535 f \r";

	// Token: 0x040000E8 RID: 232
	public const string \u1712 = "{0,10:d10} 00000 n \r";

	// Token: 0x040000E9 RID: 233
	public const string \u1713 = "trailer\r<< /Size {0}\r/Root {1} 0 R\r>>\rstartxref\r{2}\r";

	// Token: 0x040000EA RID: 234
	public const string \u1714 = "%EOF";

	// Token: 0x040000EB RID: 235
	public const float \u1715 = 72f;

	// Token: 0x040000EC RID: 236
	public const float \u1716 = 25.4f;

	// Token: 0x040000ED RID: 237
	public static readonly string[] \u1717;

	// Token: 0x040000EE RID: 238
	public static readonly string[] \u1718;

	// Token: 0x040000EF RID: 239
	public static readonly int[] \u1719;

	// Token: 0x040000F0 RID: 240
	public static readonly int[] \u171A;

	// Token: 0x040000F1 RID: 241
	public static readonly int[] \u171B;

	// Token: 0x040000F2 RID: 242
	public static readonly int[] \u171C;
}
