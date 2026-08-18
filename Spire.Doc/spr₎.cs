using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields;

// Token: 0x02000391 RID: 913
internal class spr\u208E : spr\u17BA
{
	// Token: 0x06003398 RID: 13208 RVA: 0x002F5BC8 File Offset: 0x002F4BC8
	public spr\u208E(spr\u1C7D A_0, string A_1)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
	}

	// Token: 0x06003399 RID: 13209 RVA: 0x002F5BEC File Offset: 0x002F4BEC
	public spr\u208E(spr\u1C7D A_0, sprᜫ A_1, bool A_2)
	{
		string text = string.Empty;
		text = (A_0 as TextRange).TextToSplit;
		this.ᜀ = A_0;
		string text2 = A_2 ? text.Substring(0, A_1.ᜂ()) : text.Substring(A_1.ᜁ());
		this.ᜁ = text2;
	}

	// Token: 0x0600339A RID: 13210 RVA: 0x002F5C44 File Offset: 0x002F4C44
	public string ᜃ()
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
		return this.ᜁ;
	}

	// Token: 0x0600339B RID: 13211 RVA: 0x002F5C88 File Offset: 0x002F4C88
	public void ᜀ(string A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x0600339C RID: 13212 RVA: 0x002F5CCC File Offset: 0x002F4CCC
	public spr\u1C7D ᜂ()
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
		return this.ᜀ;
	}

	// Token: 0x0600339D RID: 13213 RVA: 0x002F5D10 File Offset: 0x002F4D10
	public string ᜀ()
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
		return this.ᜃ();
	}

	// Token: 0x0600339E RID: 13214 RVA: 0x002F5D54 File Offset: 0x002F4D54
	public spr\u1D30 ᜁ()
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
		return this.ᜀ.ᜀ();
	}

	// Token: 0x0600339F RID: 13215 RVA: 0x002F5D9C File Offset: 0x002F4D9C
	public void ᜀ(spr\u19E0 A_0, sprᦰ A_1)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_DE;
			case 1:
				num = 2;
				continue;
			case 2:
				if (this.ᜃ() != string.Empty)
				{
					num = 0;
					continue;
				}
				goto IL_100;
			case 3:
				num = 5;
				continue;
			case 5:
				if (this.ᜃ() != null)
				{
					goto IL_F3;
				}
				goto IL_100;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_F3:
				num = 1;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				if (!(this.ᜀ is Field))
				{
					goto IL_100;
				}
				num = 3;
				break;
			}
		}
		IL_DE:
		string text = (this.ᜀ as Field).Text;
		(this.ᜀ as Field).Text = this.ᜃ();
		this.ᜀ.ᜀ(A_0, A_1);
		(this.ᜀ as Field).Text = text;
		return;
		IL_100:
		this.ᜀ.ᜀ(A_0, A_1, this.ᜃ());
	}

	// Token: 0x060033A0 RID: 13216 RVA: 0x002F5EBC File Offset: 0x002F4EBC
	public spr\u17BA[] ᜀ(spr\u19E0 A_0, SizeF A_1, float A_2, float A_3)
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
		return spr\u208E.ᜀ(A_0, (double)A_1.Width, this.ᜀ, this.ᜃ(), A_2, A_3);
	}

	// Token: 0x060033A1 RID: 13217 RVA: 0x002F5F14 File Offset: 0x002F4F14
	public SizeF ᜀ(spr\u19E0 A_0)
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
		return this.ᜀ.ᜀ(A_0, this.ᜀ());
	}

	// Token: 0x060033A2 RID: 13218 RVA: 0x002F5F64 File Offset: 0x002F4F64
	public static spr\u17BA[] ᜀ(spr\u19E0 A_0, double A_1, spr\u1C7D A_2, string A_3, float A_4, float A_5)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			string text;
			spr\u17BA[] array;
			for (;;)
			{
				StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic);
				stringFormat.Alignment = StringAlignment.Near;
				stringFormat.LineAlignment = StringAlignment.Near;
				stringFormat.Trimming = StringTrimming.Word;
				stringFormat.FormatFlags = (StringFormatFlags.FitBlackBox | StringFormatFlags.NoClip);
				int num = 35;
				for (;;)
				{
					spr\u200A spr_u200A;
					int num2;
					bool allCaps;
					Font font;
					bool allCaps2;
					string text2;
					string text3;
					bool flag;
					switch (num)
					{
					case 0:
						goto IL_365;
					case 1:
						num = 11;
						continue;
					case 2:
						if (!(A_2 is Field))
						{
							num = 14;
							continue;
						}
						num = 15;
						continue;
					case 3:
						if (!(A_2 is Field))
						{
							num = 51;
							continue;
						}
						num = 20;
						continue;
					case 4:
						if (spr_u200A.ᜀ[num2].ᜀ == ClipboardData.b("啴", a_))
						{
							num = 22;
							continue;
						}
						text = text + ClipboardData.b("罴", a_) + spr_u200A.ᜀ[num2].ᜀ;
						num = 31;
						continue;
					case 5:
						goto IL_70F;
					case 6:
						num = 19;
						continue;
					case 7:
						if (spr_u200A.ᜀ.Length > 1)
						{
							num = 12;
							continue;
						}
						goto IL_254;
					case 8:
						goto IL_518;
					case 9:
						if (spr_u200A.ᜀ.Length > 0)
						{
							num = 40;
							continue;
						}
						num = 43;
						continue;
					case 10:
						if (true)
						{
						}
						num = 7;
						continue;
					case 11:
						if (A_2.ᜆ() != string.Empty)
						{
							num = 26;
							continue;
						}
						goto IL_799;
					case 12:
						num2 = 1;
						num = 42;
						continue;
					case 13:
						goto IL_365;
					case 14:
						num = 53;
						continue;
					case 15:
						allCaps = (A_2 as Field).CharacterFormat.AllCaps;
						goto IL_567;
					case 16:
						if (num2 >= spr_u200A.ᜀ())
						{
							num = 37;
							continue;
						}
						num = 4;
						continue;
					case 17:
						num = 29;
						continue;
					case 18:
						goto IL_183;
					case 19:
						if (A_2.ᜆ() != null)
						{
							num = 1;
							continue;
						}
						goto IL_799;
					case 20:
						font = A_0.ᜂ((A_2 as Field).CharacterFormat);
						goto IL_666;
					case 21:
						goto IL_6DB;
					case 22:
						text += ClipboardData.b("罴", a_);
						num = 8;
						continue;
					case 23:
						goto IL_724;
					case 24:
						allCaps2 = (A_2 as Field).CharacterFormat.AllCaps;
						goto IL_39C;
					case 25:
						goto IL_746;
					case 26:
					{
						(A_2 as TextRange).TextToSplit = spr_u200A.ᜁ;
						SizeF sizeF = A_0.ᜀ(A_2 as TextRange, (A_2 as TextRange).TextToSplit);
						num = 49;
						continue;
					}
					case 27:
						text2 = A_3;
						goto IL_770;
					case 28:
						font = A_0.ᜂ((A_2 as TextRange).CharacterFormat);
						goto IL_666;
					case 29:
						if (text == ClipboardData.b("硴", a_))
						{
							num = 41;
							continue;
						}
						goto IL_490;
					case 30:
						if (text != null)
						{
							num = 50;
							continue;
						}
						goto IL_509;
					case 31:
						goto IL_518;
					case 32:
						if (spr_u200A.ᜁ == null)
						{
							num = 10;
							continue;
						}
						goto IL_254;
					case 33:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_70F;
						default:
							if (false)
							{
							}
							num = 52;
							continue;
						}
						break;
					case 34:
						if (!(text == ClipboardData.b("罴", a_)))
						{
							num = 17;
							continue;
						}
						goto IL_4D4;
					case 35:
						if (A_3 == null)
						{
							num = 45;
							continue;
						}
						num = 27;
						continue;
					case 36:
						text3 = text3.ToUpper();
						num = 56;
						continue;
					case 37:
						num = 13;
						continue;
					case 38:
						goto IL_490;
					case 39:
						text2 = A_2.ᜆ();
						goto IL_770;
					case 40:
						array = new spr\u17BA[2];
						array[0] = new spr\u208E(A_2, spr_u200A.ᜀ[0].ᜀ);
						text = string.Empty;
						num = 32;
						continue;
					case 41:
						goto IL_4D4;
					case 42:
						goto IL_6DB;
					case 43:
						if (A_2 != null)
						{
							num = 6;
							continue;
						}
						goto IL_799;
					case 44:
						if (!text.StartsWith(ClipboardData.b("罴", a_)))
						{
							num = 33;
							continue;
						}
						goto IL_724;
					case 45:
						num = 39;
						continue;
					case 46:
						goto IL_1FC;
					case 47:
						if (flag)
						{
							num = 36;
							continue;
						}
						goto IL_5BF;
					case 48:
						num = 5;
						continue;
					case 49:
					{
						SizeF sizeF;
						if ((double)sizeF.Width > A_1)
						{
							num = 46;
							continue;
						}
						goto IL_799;
					}
					case 50:
						num = 44;
						continue;
					case 51:
						num = 28;
						continue;
					case 52:
						if (text.StartsWith(ClipboardData.b("硴", a_)))
						{
							num = 23;
							continue;
						}
						goto IL_509;
					case 53:
						allCaps = (A_2 as TextRange).CharacterFormat.AllCaps;
						goto IL_567;
					case 54:
						text3 = text3.ToUpper();
						num = 18;
						continue;
					case 55:
						if (!(A_2 is Field))
						{
							num = 48;
							continue;
						}
						num = 24;
						continue;
					case 56:
						goto IL_5BF;
					}
					break;
					IL_183:
					num = 3;
					continue;
					IL_39C:
					if (allCaps2)
					{
						num = 54;
						continue;
					}
					goto IL_183;
					IL_70F:
					allCaps2 = (A_2 as TextRange).CharacterFormat.AllCaps;
					goto IL_39C;
					IL_254:
					text = spr_u200A.ᜁ;
					num = 0;
					continue;
					IL_365:
					num = 34;
					continue;
					IL_490:
					num = 30;
					continue;
					IL_4D4:
					text = ClipboardData.b("啴", a_);
					num = 38;
					continue;
					IL_518:
					num2++;
					num = 21;
					continue;
					IL_567:
					flag = allCaps;
					num = 47;
					continue;
					IL_5BF:
					num = 55;
					continue;
					IL_666:
					Font a_2 = font;
					sprᴁ sprᴁ = new sprᴁ();
					sprᴁ.ᜀ(A_0);
					sprᴁ.ᜁ(A_0.ᜂ(A_2 as TextRange));
					sprᴁ.ᜀ(A_2);
					spr_u200A = sprᴁ.ᜁ(text3, A_0.ᜅ(), a_2, stringFormat, new SizeF((float)A_1, float.MaxValue));
					num = 9;
					continue;
					IL_6DB:
					num = 16;
					continue;
					IL_724:
					text = text.Remove(0, 1).TrimStart(new char[0]);
					num = 25;
					continue;
					IL_770:
					text3 = text2;
					num = 2;
				}
			}
			IL_1FC:
			return spr\u208E.ᜀ(A_0, A_1, A_2, null, A_4, A_5);
			IL_509:
			array[1] = new spr\u208E(A_2, text);
			return array;
			IL_746:
			goto IL_509;
			IL_799:
			return null;
		}
		}
	}

	// Token: 0x060033A3 RID: 13219 RVA: 0x002F670C File Offset: 0x002F570C
	public static spr\u17BA[] ᜀ(spr\u19E0 A_0, double A_1, spr\u1C7D A_2, sprᜫ A_3, float A_4, float A_5)
	{
		switch (0)
		{
		default:
		{
			int num2;
			spr\u17BA[] array;
			spr\u17BA[] array2;
			for (;;)
			{
				string text = string.Empty;
				text = (A_2 as TextRange).TextToSplit;
				int num = 16;
				for (;;)
				{
					int num3;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_20C;
						default:
							if (false)
							{
							}
							if (num2 < A_3.ᜀ())
							{
								num = 25;
								continue;
							}
							goto IL_258;
						}
						break;
					case 1:
						array[0] = new spr\u208E(A_2, A_3.ᜁ(num2), true);
						array[1] = new spr\u208E(A_2, A_3.ᜂ(num2), false);
						num = 15;
						continue;
					case 2:
						if (array2[0] == null)
						{
							num = 7;
							continue;
						}
						goto IL_438;
					case 3:
						num3 = num2 - 1;
						goto IL_3E5;
					case 4:
						A_3 = new sprᜫ(0, text.Length - 1);
						num = 13;
						continue;
					case 5:
						if (num2 >= 0)
						{
							num = 17;
							continue;
						}
						num = 6;
						continue;
					case 6:
						num3 = 0;
						goto IL_3E5;
					case 7:
						array2[0] = new spr\u208E(A_2, A_3.ᜁ(num2), true);
						if (true)
						{
						}
						num = 27;
						continue;
					case 8:
						num = 26;
						continue;
					case 9:
						goto IL_31D;
					case 10:
						if (num2 > 0)
						{
							num = 12;
							continue;
						}
						goto IL_119;
					case 11:
						if (num2 != text.Length)
						{
							num = 19;
							continue;
						}
						num = 3;
						continue;
					case 12:
					{
						int num4 = A_0.ᜂ(text, num2);
						num2 += num4;
						num = 30;
						continue;
					}
					case 13:
						goto IL_35E;
					case 14:
						num = 29;
						continue;
					case 15:
						goto IL_195;
					case 16:
						if (A_3 == null)
						{
							num = 4;
							continue;
						}
						goto IL_35E;
					case 17:
						num = 11;
						continue;
					case 18:
					{
						TextRange textRange = new TextRange((A_2 as TextRange).Document);
						textRange.ᜀ((A_2 as TextRange).Owner);
						textRange.CharacterFormat.Font = (A_2 as TextRange).CharacterFormat.Font;
						textRange.CharacterFormat.FontSize = (A_2 as TextRange).CharacterFormat.FontSize;
						array2[0] = new spr\u208E(textRange, string.Empty);
						num = 9;
						continue;
					}
					case 19:
						num = 22;
						continue;
					case 20:
						goto IL_20C;
					case 21:
						if (num2 > -1)
						{
							num = 23;
							continue;
						}
						goto IL_258;
					case 22:
						num3 = num2;
						goto IL_3E5;
					case 23:
						num = 0;
						continue;
					case 24:
						if (num2 == 0)
						{
							num = 14;
							continue;
						}
						goto IL_31D;
					case 25:
						array = new spr\u17BA[2];
						num = 28;
						continue;
					case 26:
						if ((A_2 as TextRange).Document != null)
						{
							num = 18;
							continue;
						}
						goto IL_31D;
					case 27:
						goto IL_3BA;
					case 28:
						if (text.Length == (A_2 as TextRange).Text.Length)
						{
							num = 1;
							continue;
						}
						array[0] = new spr\u208E(A_2, text.Substring(0, num2));
						array[1] = new spr\u208E(A_2, text.Substring(num2));
						num = 20;
						continue;
					case 29:
						if (A_2 is TextRange)
						{
							num = 8;
							continue;
						}
						goto IL_31D;
					case 30:
						goto IL_119;
					}
					break;
					IL_119:
					num = 21;
					continue;
					IL_258:
					array2 = new spr\u17BA[2];
					num = 5;
					continue;
					IL_31D:
					num = 2;
					continue;
					IL_35E:
					num2 = A_2.ᜀ(A_0, A_1, A_3.ᜀ(text), text, A_4, A_5);
					num = 10;
					continue;
					IL_3E5:
					num2 = num3;
					num = 24;
				}
			}
			IL_195:
			IL_20C:
			(A_2 as TextRange).TextToSplit = (A_2 as TextRange).TextToSplit.Remove(0, num2);
			return array;
			IL_3BA:
			IL_438:
			array2[1] = new spr\u208E(A_2, A_3.ᜂ(num2), false);
			return array2;
		}
		}
	}

	// Token: 0x0400280E RID: 10254
	private spr\u1C7D ᜀ;

	// Token: 0x0400280F RID: 10255
	private string ᜁ;
}
