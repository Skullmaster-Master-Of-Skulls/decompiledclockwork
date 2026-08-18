using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Layouting;

// Token: 0x02000393 RID: 915
internal class sprᴁ
{
	// Token: 0x060033BB RID: 13243 RVA: 0x002F7F00 File Offset: 0x002F6F00
	internal spr\u19E0 ᜄ()
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
		return this.ᜆ;
	}

	// Token: 0x060033BC RID: 13244 RVA: 0x002F7F44 File Offset: 0x002F6F44
	internal void ᜀ(spr\u19E0 A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x060033BD RID: 13245 RVA: 0x002F7F88 File Offset: 0x002F6F88
	internal void ᜁ(bool A_0)
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
		this.ᜇ = A_0;
	}

	// Token: 0x060033BE RID: 13246 RVA: 0x002F7FCC File Offset: 0x002F6FCC
	internal void ᜀ(spr\u1AB8 A_0)
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
		this.ᜈ = A_0;
	}

	// Token: 0x060033C0 RID: 13248 RVA: 0x002F8024 File Offset: 0x002F7024
	public spr\u200A ᜁ(string A_0, Graphics A_1, Font A_2, StringFormat A_3, SizeF A_4)
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
		this.ᜀ(A_0, A_1, A_2, A_3, A_4);
		spr\u200A result = this.ᜃ();
		this.ᜂ();
		return result;
	}

	// Token: 0x060033C1 RID: 13249 RVA: 0x002F807C File Offset: 0x002F707C
	public Size ᜀ(string A_0, Font A_1)
	{
		if (A_0.Length == 0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3E;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return Size.Empty;
		}
		IL_3E:
		CharacterRange[] measurableCharacterRanges = new CharacterRange[]
		{
			new CharacterRange(0, A_0.Length)
		};
		StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic);
		stringFormat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
		stringFormat.FormatFlags |= StringFormatFlags.NoClip;
		stringFormat.Trimming = StringTrimming.Word;
		stringFormat.SetMeasurableCharacterRanges(measurableCharacterRanges);
		Region[] array = this.ᜅ.MeasureCharacterRanges(A_0, A_1, new Rectangle(0, 0, int.MaxValue, int.MaxValue), stringFormat);
		RectangleF bounds = array[0].GetBounds(this.ᜅ);
		bounds.Width += 2f * bounds.X;
		bounds.Height += 2f * bounds.Y;
		return Size.Ceiling(bounds.Size);
	}

	// Token: 0x060033C2 RID: 13250 RVA: 0x002F81A4 File Offset: 0x002F71A4
	internal SizeF ᜀ(Font A_0, string A_1)
	{
		if (A_1.Length == 0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3E;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return SizeF.Empty;
		}
		IL_3E:
		CharacterRange[] measurableCharacterRanges = new CharacterRange[]
		{
			new CharacterRange(0, A_1.Length)
		};
		StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic);
		stringFormat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
		stringFormat.FormatFlags |= StringFormatFlags.NoClip;
		stringFormat.Trimming = StringTrimming.Word;
		stringFormat.SetMeasurableCharacterRanges(measurableCharacterRanges);
		Region[] array = this.ᜅ.MeasureCharacterRanges(A_1, A_0, new Rectangle(0, 0, int.MaxValue, int.MaxValue), stringFormat);
		RectangleF bounds = array[0].GetBounds(this.ᜅ);
		bounds.Width += 2f * bounds.X;
		bounds.Height += 2f * bounds.Y;
		return bounds.Size;
	}

	// Token: 0x060033C3 RID: 13251 RVA: 0x002F82C4 File Offset: 0x002F72C4
	private void ᜀ(string A_0, Graphics A_1, Font A_2, StringFormat A_3, SizeF A_4)
	{
		int a_ = 3;
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
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_8B;
				case 2:
					if (A_2 == null)
					{
						num = 1;
						continue;
					}
					goto IL_A1;
				case 3:
					goto IL_5A;
				}
				if (A_0 == null)
				{
					num = 3;
				}
				else
				{
					num = 2;
				}
			}
			IL_8B:
			if (true)
			{
			}
			throw new ArgumentNullException(ClipboardData.b("ཨѪͬ᭮", a_));
			IL_A1:
			this.ᜀ = A_0;
			this.ᜅ = A_1;
			this.ᜁ = A_2;
			this.ᜂ = A_3;
			this.ᜃ = A_4;
			this.ᜄ = new sprᣨ(A_0);
			return;
		}
		}
		IL_5A:
		throw new ArgumentNullException(ClipboardData.b("ᵨ๪ᕬ᭮", a_));
	}

	// Token: 0x060033C4 RID: 13252 RVA: 0x002F83A4 File Offset: 0x002F73A4
	private spr\u200A ᜃ()
	{
		switch (0)
		{
		default:
		{
			spr\u200A spr_u200A;
			spr\u200A spr_u200A2;
			List<sprṴ> list;
			for (;;)
			{
				spr_u200A = new spr\u200A();
				spr_u200A2 = new spr\u200A();
				list = new List<sprṴ>();
				string text = this.ᜄ.ᜁ();
				float a_ = this.ᜀ(true);
				int num = 12;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (spr_u200A2.ᜁ.Length <= 0)
						{
							num = 2;
							continue;
						}
						goto IL_240;
					case 1:
						goto IL_112;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_112;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							goto IL_116;
						}
						break;
					case 3:
						goto IL_1A4;
					case 4:
						if (!spr_u200A2.ᜁ())
						{
							num = 15;
							continue;
						}
						goto IL_17F;
					case 5:
					{
						int a_2;
						this.ᜄ.ᜀ(a_2);
						num = 8;
						continue;
					}
					case 6:
					{
						bool flag;
						if (!flag)
						{
							num = 5;
							continue;
						}
						num = 16;
						continue;
					}
					case 7:
						return spr_u200A2;
					case 8:
						goto IL_23E;
					case 9:
						goto IL_1C2;
					case 10:
						if (text == null)
						{
							num = 9;
							continue;
						}
						spr_u200A2 = this.ᜀ(text, a_);
						num = 14;
						continue;
					case 11:
						num = 0;
						continue;
					case 12:
						goto IL_1A4;
					case 13:
						if (spr_u200A2.ᜁ != null)
						{
							num = 11;
							continue;
						}
						goto IL_116;
					case 14:
						if (this.ᜄ.ᜆ() == text.Length)
						{
							num = 7;
							continue;
						}
						num = 4;
						continue;
					case 15:
					{
						int a_2 = 0;
						bool flag = this.ᜀ(spr_u200A, spr_u200A2, list, out a_2);
						num = 6;
						continue;
					}
					case 16:
						if (this.ᜄ.ᜆ() != this.ᜄ.ᜅ())
						{
							num = 1;
							continue;
						}
						goto IL_240;
					}
					break;
					IL_116:
					this.ᜄ.ᜇ();
					text = this.ᜄ.ᜁ();
					a_ = this.ᜀ(false);
					num = 3;
					continue;
					IL_17F:
					num = 13;
					continue;
					IL_112:
					goto IL_17F;
					IL_1A4:
					num = 10;
				}
			}
			return spr_u200A2;
			IL_1C2:
			IL_23E:
			IL_240:
			this.ᜀ(spr_u200A, list);
			return spr_u200A;
		}
		}
	}

	// Token: 0x060033C5 RID: 13253 RVA: 0x002F85FC File Offset: 0x002F75FC
	private bool ᜀ(spr\u200A A_0, spr\u200A A_1, List<sprṴ> A_2, out int A_3)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num = 23;
			for (;;)
			{
				bool result;
				bool flag;
				float num3;
				float num2;
				bool flag2;
				int num4;
				float height;
				bool flag3;
				bool flag4;
				switch (num)
				{
				case 0:
					goto IL_4A6;
				case 1:
					return result;
				case 2:
					if (flag)
					{
						num = 37;
						continue;
					}
					num2 = num3;
					num = 22;
					continue;
				case 3:
					goto IL_4FE;
				case 4:
					num = 18;
					continue;
				case 5:
					if (A_1 == null)
					{
						num = 36;
						continue;
					}
					num = 29;
					continue;
				case 6:
					goto IL_4FE;
				case 7:
					flag2 = false;
					goto IL_461;
				case 8:
					goto IL_4FE;
				case 9:
					flag2 = (this.ᜂ.FormatFlags != StringFormatFlags.LineLimit);
					goto IL_461;
				case 10:
					goto IL_1BB;
				case 11:
					if (this.ᜂ != null)
					{
						num = 40;
						continue;
					}
					if (true)
					{
					}
					num = 39;
					continue;
				case 12:
					num4++;
					num = 0;
					continue;
				case 13:
					if (num3 > height)
					{
						num = 32;
						continue;
					}
					goto IL_3AD;
				case 14:
					goto IL_4A6;
				case 15:
					num = 9;
					continue;
				case 16:
					num = 11;
					continue;
				case 17:
				{
					int num5;
					if (num4 >= num5)
					{
						num = 8;
						continue;
					}
					num3 = num2 + A_1.ᜃ;
					num = 13;
					continue;
				}
				case 18:
					if (height > 0f)
					{
						num = 31;
						continue;
					}
					goto IL_399;
				case 19:
				{
					num4 = 0;
					int num5 = A_1.ᜀ.Length;
					num = 14;
					continue;
				}
				case 20:
					goto IL_3AD;
				case 21:
					if (flag3)
					{
						num = 16;
						continue;
					}
					goto IL_399;
				case 22:
					goto IL_1BB;
				case 23:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_518;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 24:
					if (A_1.ᜀ != null)
					{
						num = 19;
						continue;
					}
					goto IL_4FE;
				case 25:
					goto IL_394;
				case 26:
					num = 38;
					continue;
				case 27:
					goto IL_102;
				case 28:
					if (num3 >= height)
					{
						num = 4;
						continue;
					}
					goto IL_399;
				case 29:
					if (A_2 == null)
					{
						num = 25;
						continue;
					}
					result = true;
					num = 33;
					continue;
				case 30:
					if (num2 != A_0.ᜂ.Height)
					{
						goto IL_518;
					}
					return result;
				case 31:
					num = 21;
					continue;
				case 32:
					num = 35;
					continue;
				case 33:
					if (this.ᜂ != null)
					{
						num = 15;
						continue;
					}
					num = 7;
					continue;
				case 34:
					flag4 = (this.ᜂ.FormatFlags != StringFormatFlags.NoClip);
					goto IL_2DD;
				case 35:
					if (height > 0f)
					{
						num = 26;
						continue;
					}
					goto IL_3AD;
				case 36:
					goto IL_371;
				case 37:
				{
					float num6 = num3 - height;
					float num7 = A_1.ᜃ - num6;
					num2 += num7;
					num = 10;
					continue;
				}
				case 38:
					if (flag3)
					{
						num = 20;
						continue;
					}
					result = false;
					num = 6;
					continue;
				case 39:
					flag4 = true;
					goto IL_2DD;
				case 40:
					num = 34;
					continue;
				case 41:
				{
					SizeF sizeF = A_0.ᜂ;
					sizeF.Height = num2;
					A_0.ᜂ = sizeF;
					num = 1;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 27;
					continue;
				}
				num = 5;
				continue;
				IL_1BB:
				result = false;
				num = 3;
				continue;
				IL_2DD:
				flag = flag4;
				num = 2;
				continue;
				IL_399:
				num2 = num3;
				num = 12;
				continue;
				IL_3AD:
				sprṴ sprṴ = A_1.ᜀ[num4];
				A_3 += sprṴ.ᜀ.Length;
				sprṴ = this.ᜀ(sprṴ, A_2.Count == 0);
				A_2.Add(sprṴ);
				SizeF sizeF2 = A_0.ᜂ;
				sizeF2.Width = Math.Max(sizeF2.Width, sprṴ.ᜁ);
				A_0.ᜂ = sizeF2;
				num = 28;
				continue;
				IL_461:
				flag3 = flag2;
				num2 = A_0.ᜂ.Height;
				height = this.ᜃ.Height;
				A_3 = 0;
				num = 24;
				continue;
				IL_4A6:
				num = 17;
				continue;
				IL_4FE:
				num = 30;
				continue;
				IL_518:
				num = 41;
			}
			IL_102:
			throw new ArgumentNullException(ClipboardData.b("Ͱᙲٴɶᕸེ", a_));
			IL_371:
			throw new ArgumentNullException(ClipboardData.b("ᵰᩲ᭴ቶ⭸Ṻ๼੾", a_));
			IL_394:
			throw new ArgumentNullException(ClipboardData.b("ᵰᩲ᭴ቶ੸", a_));
		}
		}
	}

	// Token: 0x060033C6 RID: 13254 RVA: 0x002F8B30 File Offset: 0x002F7B30
	private void ᜀ(spr\u200A A_0, List<sprṴ> A_1)
	{
		int a_ = 17;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_55;
			case 1:
				if (!this.ᜄ.ᜃ())
				{
					num = 6;
					continue;
				}
				goto IL_122;
			case 2:
				IL_10C:
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				A_0.ᜀ = A_1.ToArray();
				A_0.ᜃ = this.ᜁ();
				num = 1;
				continue;
			case 3:
				goto IL_122;
			case 4:
				goto IL_11D;
			case 5:
				if (true)
				{
				}
				break;
			case 6:
			{
				int length = A_1[0].ᜀ.Length;
				A_0.ᜁ = this.ᜀ.Substring(length, this.ᜀ.Length - length).TrimStart(sprᣨ.ᜅ);
				num = 3;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 2;
			continue;
			IL_122:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_10C;
			default:
				goto IL_138;
			}
		}
		IL_55:
		throw new ArgumentNullException(ClipboardData.b("նᱸࡺࡼ፾", a_));
		IL_11D:
		throw new ArgumentNullException(ClipboardData.b("᭶ၸᕺ᡼౾", a_));
		IL_138:
		if (false)
		{
		}
		A_1.Clear();
	}

	// Token: 0x060033C7 RID: 13255 RVA: 0x002F8C84 File Offset: 0x002F7C84
	private void ᜂ()
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
		this.ᜁ = null;
		this.ᜂ = null;
		this.ᜄ.ᜀ();
		this.ᜄ = null;
		this.ᜀ = null;
	}

	// Token: 0x060033C8 RID: 13256 RVA: 0x002F8CE8 File Offset: 0x002F7CE8
	private float ᜁ()
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
		return (float)this.ᜁ.Height;
	}

	// Token: 0x060033C9 RID: 13257 RVA: 0x002F8D34 File Offset: 0x002F7D34
	internal bool ᜂ(string A_0)
	{
		bool result;
		for (;;)
		{
			char[] array = A_0.ToCharArray();
			result = false;
			int num = 0;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (true)
					{
					}
					goto IL_85;
				case 1:
					if (array[num] > 'ÿ')
					{
						num2 = 5;
						continue;
					}
					num++;
					num2 = 0;
					continue;
				case 2:
					goto IL_A3;
				case 3:
					goto IL_85;
				case 4:
					goto IL_A3;
				case 5:
					result = true;
					num2 = 4;
					continue;
				case 6:
				{
					IL_90:
					if (num >= array.Length)
					{
						num2 = 2;
						continue;
					}
					char c = array[num];
					num2 = 1;
					continue;
				}
				}
				break;
				IL_A3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_90;
				default:
					goto IL_B9;
				}
				IL_85:
				num2 = 6;
			}
		}
		IL_B9:
		if (false)
		{
		}
		return result;
	}

	// Token: 0x060033CA RID: 13258 RVA: 0x002F8E04 File Offset: 0x002F7E04
	internal bool ᜁ(string A_0)
	{
		int a_ = 4;
		for (;;)
		{
			Regex regex = new Regex(ClipboardData.b("ㅩに᭭䑯᝱䑳䙵啷♹ॻ䝽놃\udb85ꎇ", a_));
			int num = 0;
			for (;;)
			{
				Regex regex3;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_91;
					default:
					{
						if (false)
						{
						}
						if (regex.IsMatch(A_0))
						{
							num = 2;
							continue;
						}
						Regex regex2 = new Regex(ClipboardData.b("ㅩに᭭䍯䉱䁳䙵啷♹ॻ䵽끿뮁\udb85ꎇ", a_));
						regex3 = new Regex(ClipboardData.b("ㅩに᭭䍯䉱ᕳ䙵啷♹ॻ䵽끿\udb85ꎇ", a_));
						num = 3;
						continue;
					}
					}
					break;
				case 1:
					goto IL_91;
				case 2:
					goto IL_7C;
				case 3:
				{
					Regex regex2;
					if (!regex2.IsMatch(A_0))
					{
						num = 4;
						continue;
					}
					return true;
				}
				case 4:
					num = 1;
					continue;
				case 5:
					return true;
				}
				break;
				IL_91:
				if (!regex3.IsMatch(A_0))
				{
					return false;
				}
				num = 5;
			}
		}
		IL_7C:
		if (true)
		{
		}
		return true;
	}

	// Token: 0x060033CB RID: 13259 RVA: 0x002F8F08 File Offset: 0x002F7F08
	private spr\u200A ᜀ(string A_0, float A_1)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 63;
			spr\u200A spr_u200A;
			List<sprṴ> list;
			for (;;)
			{
				StringBuilder stringBuilder;
				StringBuilder stringBuilder2;
				sprᣨ sprᣨ;
				sprᡌ sprᡌ;
				string text;
				int num2;
				string text3;
				bool flag2;
				float num3;
				float width;
				sprᡌ sprᡌ2;
				float num5;
				int length;
				float num9;
				TextLineType textLineType;
				switch (num)
				{
				case 0:
				{
					bool flag;
					if (flag)
					{
						num = 85;
						continue;
					}
					goto IL_372;
				}
				case 1:
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
					stringBuilder.ToString();
					stringBuilder2 = stringBuilder;
					sprᣨ.ᜁ(stringBuilder.Length);
					num = 49;
					continue;
				case 2:
					if (sprᡌ.ᜂ().ᜁ() != TextWrappingStyle.TopAndBottom)
					{
						num = 13;
						continue;
					}
					goto IL_672;
				case 3:
				{
					string text2;
					text = text2;
					sprᣨ sprᣨ2 = sprᣨ;
					sprᣨ2.ᜁ(sprᣨ2.ᜅ() + num2);
					num = 54;
					continue;
				}
				case 4:
				{
					sprᣨ sprᣨ3 = sprᣨ;
					sprᣨ3.ᜁ(sprᣨ3.ᜅ() + 1);
					text3 = ClipboardData.b("偯", a_) + sprᣨ.ᜄ();
					num = 16;
					continue;
				}
				case 5:
					goto IL_6D5;
				case 6:
					sprᣨ.ᜊ();
					text3 = sprᣨ.ᜄ();
					num = 57;
					continue;
				case 7:
					if (sprᡌ.ᜂ().ᜁ() != TextWrappingStyle.Behind)
					{
						num = 70;
						continue;
					}
					goto IL_1CF;
				case 8:
					goto IL_1CF;
				case 9:
				{
					RectangleF rectangleF = sprᡌ.ᜀ();
					num = 11;
					continue;
				}
				case 10:
					num = 34;
					continue;
				case 11:
				{
					RectangleF rectangleF;
					if (!rectangleF.IsEmpty)
					{
						num = 19;
						continue;
					}
					goto IL_672;
				}
				case 12:
					if (this.ᜀ() != StringTrimming.Word)
					{
						num = 77;
						continue;
					}
					goto IL_561;
				case 13:
					goto IL_3C7;
				case 14:
					goto IL_5AB;
				case 15:
					if (flag2)
					{
						num = 6;
						continue;
					}
					sprᣨ.ᜂ();
					text3 = sprᣨ.ᜉ().ToString();
					num = 32;
					continue;
				case 16:
					goto IL_B48;
				case 17:
					if (num3 <= width)
					{
						num = 43;
						continue;
					}
					goto IL_23C;
				case 18:
				{
					RectangleF rectangleF2 = sprᡌ2.ᜀ();
					num = 82;
					continue;
				}
				case 19:
					num = 56;
					continue;
				case 20:
					if (sprᡌ.ᜂ().ᜁ() != TextWrappingStyle.TopAndBottom)
					{
						num = 45;
						continue;
					}
					goto IL_1CF;
				case 21:
					num = 7;
					continue;
				case 22:
					if (this.ᜀ() == StringTrimming.Character)
					{
						num = 66;
						continue;
					}
					goto IL_B6C;
				case 23:
					if (text3.Length != sprᣨ.ᜆ())
					{
						num = 10;
						continue;
					}
					goto IL_B48;
				case 24:
					goto IL_9BD;
				case 25:
					goto IL_B48;
				case 26:
					stringBuilder2.Append(A_0.Substring(0, sprᣨ.ᜅ()));
					num = 31;
					continue;
				case 27:
					num = 60;
					continue;
				case 28:
					num = 71;
					continue;
				case 29:
				{
					this.ᜄ().\u171D().ᜀ(true);
					float val = (float)((double)sprᡌ.ᜀ().Left + Math.Round((double)sprᡌ.ᜀ().Width, 6));
					this.ᜄ().\u171D().ᜀ(Math.Max(sprᡌ.ᜀ().Right, val));
					num = 8;
					continue;
				}
				case 30:
				{
					int num4;
					if (num4 == 1)
					{
						num = 76;
						continue;
					}
					flag2 = false;
					stringBuilder.Length = 0;
					text3 = sprᣨ.ᜉ().ToString();
					num = 44;
					continue;
				}
				case 31:
					goto IL_372;
				case 32:
					goto IL_B48;
				case 33:
					stringBuilder.Append(text3);
					num = 14;
					continue;
				case 34:
					if (text3 == ClipboardData.b("偯", a_))
					{
						num = 4;
						continue;
					}
					goto IL_B48;
				case 35:
					if (!flag2)
					{
						num = 88;
						continue;
					}
					flag2 = false;
					stringBuilder.Length = 0;
					stringBuilder.Append(stringBuilder2.ToString());
					text3 = sprᣨ.ᜉ().ToString();
					num = 25;
					continue;
				case 36:
					if (stringBuilder2.Length <= 0)
					{
						num = 27;
						continue;
					}
					goto IL_2B6;
				case 37:
					if (width > 0f)
					{
						num = 18;
						continue;
					}
					goto IL_23C;
				case 38:
					if (width > 0f)
					{
						num = 52;
						continue;
					}
					goto IL_A95;
				case 39:
					num = 47;
					continue;
				case 40:
					if (num2 != 0)
					{
						num = 64;
						continue;
					}
					goto IL_39C;
				case 41:
					goto IL_BD3;
				case 42:
					if (num5 <= width)
					{
						num = 9;
						continue;
					}
					goto IL_3C7;
				case 43:
					goto IL_A95;
				case 44:
					goto IL_B48;
				case 45:
					goto IL_C59;
				case 46:
					if (true)
					{
					}
					if (this.ᜀ() != StringTrimming.None)
					{
						num = 86;
						continue;
					}
					goto IL_372;
				case 47:
					if (sprᡌ.ᜂ().ᜁ() != TextWrappingStyle.InFrontOfText)
					{
						num = 21;
						continue;
					}
					goto IL_1CF;
				case 48:
					if (sprᡌ.ᜂ().ᜀ() == TextWrappingType.Both)
					{
						num = 28;
						continue;
					}
					goto IL_1CF;
				case 49:
					goto IL_372;
				case 50:
				{
					if (text3 == null)
					{
						num = 74;
						continue;
					}
					bool flag = this.ᜁ(text3);
					int num6 = 0;
					num = 81;
					continue;
				}
				case 51:
				{
					int num4 = A_0.Split(null).Length;
					num = 12;
					continue;
				}
				case 52:
					num = 17;
					continue;
				case 53:
					if (sprᡌ.ᜂ().ᜁ() != TextWrappingStyle.Behind)
					{
						num = 73;
						continue;
					}
					goto IL_672;
				case 54:
					goto IL_39C;
				case 55:
					goto IL_6D5;
				case 56:
					if (sprᡌ.ᜂ().ᜁ() != TextWrappingStyle.Inline)
					{
						num = 58;
						continue;
					}
					goto IL_672;
				case 57:
					goto IL_B48;
				case 58:
					num = 78;
					continue;
				case 59:
					goto IL_5AB;
				case 60:
					if (this.ᜄ().\u171D().ᜃ())
					{
						num = 65;
						continue;
					}
					goto IL_BC0;
				case 61:
					if (text3.StartsWith(ClipboardData.b("偯", a_)))
					{
						num = 26;
						continue;
					}
					goto IL_372;
				case 62:
					goto IL_5AB;
				case 64:
				{
					string text2 = A_0.Substring(0, length + num2);
					float width2 = this.ᜄ().ᜁ(text2, this.ᜁ, null).Width;
					num = 87;
					continue;
				}
				case 65:
					goto IL_2B6;
				case 66:
					num = 35;
					continue;
				case 67:
				{
					int num7;
					if (num7 < text3.Length)
					{
						stringBuilder.Append(text3.Substring(num7, 1));
						float num8 = this.ᜀ(stringBuilder.ToString());
						num = 89;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C59;
					default:
						if (false)
						{
						}
						num = 59;
						continue;
					}
					break;
				}
				case 68:
					goto IL_BC0;
				case 69:
					num = 53;
					continue;
				case 70:
					num = 20;
					continue;
				case 71:
					if (num9 < this.ᜄ().ᜆ().ᜇ().Right)
					{
						num = 29;
						continue;
					}
					goto IL_1CF;
				case 72:
					this.ᜀ(spr_u200A, list, A_0, num3, TextLineType.NewLineBreak | textLineType);
					num = 24;
					continue;
				case 73:
					num = 2;
					continue;
				case 74:
					goto IL_372;
				case 75:
					if (sprᡌ.ᜂ().ᜁ() != TextWrappingStyle.Inline)
					{
						num = 39;
						continue;
					}
					goto IL_1CF;
				case 76:
					goto IL_561;
				case 77:
					num = 30;
					continue;
				case 78:
					if (sprᡌ.ᜂ().ᜁ() != TextWrappingStyle.InFrontOfText)
					{
						num = 69;
						continue;
					}
					goto IL_672;
				case 79:
				{
					int num6;
					if (num6 > 0)
					{
						num = 1;
						continue;
					}
					goto IL_372;
				}
				case 80:
				{
					int num7;
					int num6 = num7;
					num = 62;
					continue;
				}
				case 81:
				{
					bool flag;
					if (!flag)
					{
						num = 33;
						continue;
					}
					int num7 = 0;
					num = 55;
					continue;
				}
				case 82:
				{
					RectangleF rectangleF2;
					if (rectangleF2.IsEmpty)
					{
						num = 72;
						continue;
					}
					goto IL_23C;
				}
				case 83:
					goto IL_1A6;
				case 84:
					if (stringBuilder.Length == text3.Length)
					{
						num = 51;
						continue;
					}
					num = 22;
					continue;
				case 85:
					num = 79;
					continue;
				case 86:
					num = 84;
					continue;
				case 87:
				{
					float width2;
					if (width2 <= width)
					{
						num = 3;
						continue;
					}
					goto IL_39C;
				}
				case 88:
					goto IL_B6C;
				case 89:
				{
					float num8;
					if (num8 > width)
					{
						num = 80;
						continue;
					}
					int num7;
					num7++;
					num = 5;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 83;
					continue;
				}
				A_0 = A_0.Replace(ClipboardData.b("祯", a_), ClipboardData.b("偯剱味噵", a_));
				spr_u200A = new spr\u200A();
				spr_u200A.ᜃ = this.ᜁ();
				list = new List<sprṴ>();
				width = this.ᜃ.Width;
				num3 = this.ᜀ(A_0) + A_1;
				textLineType = TextLineType.FirstParagraphLine;
				flag2 = true;
				PointF location = this.ᜄ().ᜆ().ᜇ().Location;
				SizeF size = this.ᜀ(A_0.ToString(), this.ᜁ);
				RectangleF a_2 = new RectangleF(location, size);
				sprᡌ2 = this.ᜄ().\u171D().ᜀ(a_2, this.ᜈ);
				num = 38;
				continue;
				IL_1CF:
				num = 46;
				continue;
				IL_23C:
				stringBuilder2 = new StringBuilder();
				stringBuilder = new StringBuilder();
				num3 = A_1;
				sprᣨ = new sprᣨ(A_0);
				text3 = sprᣨ.ᜄ();
				num = 23;
				continue;
				IL_2B6:
				text = stringBuilder2.ToString();
				length = text.Length;
				num2 = this.ᜄ().ᜂ(A_0, length);
				num = 40;
				continue;
				IL_372:
				num = 36;
				continue;
				IL_39C:
				this.ᜀ(spr_u200A, list, text, num3, TextLineType.NewLineBreak | TextLineType.LastParagraphLine);
				spr_u200A.ᜁ = sprᣨ.ᜈ();
				num = 68;
				continue;
				IL_3C7:
				num = 75;
				continue;
				IL_561:
				spr_u200A.ᜁ = A_0.Substring(sprᣨ.ᜅ());
				num = 61;
				continue;
				IL_5AB:
				PointF location2 = this.ᜄ().ᜆ().ᜇ().Location;
				SizeF sizeF = this.ᜀ(stringBuilder.ToString(), this.ᜁ);
				RectangleF a_3 = new RectangleF(location2, new SizeF(sizeF.Width, sizeF.Height));
				sprᡌ = this.ᜄ().\u171D().ᜀ(a_3, this.ᜈ);
				num9 = a_3.Right + sprᡌ.ᜀ().Width;
				num5 = this.ᜀ(stringBuilder.ToString());
				num = 42;
				continue;
				IL_672:
				stringBuilder2.Append(text3);
				num3 = num5;
				num = 15;
				continue;
				IL_6D5:
				num = 67;
				continue;
				IL_A95:
				num = 37;
				continue;
				IL_B48:
				num = 50;
				continue;
				IL_B6C:
				stringBuilder2.ToString();
				num = 0;
				continue;
				IL_BC0:
				sprᣨ.ᜀ();
				num = 41;
				continue;
				IL_C59:
				num = 48;
			}
			IL_1A6:
			throw new ArgumentNullException(ClipboardData.b("ᱯ᭱ᩳ፵", a_));
			IL_9BD:
			IL_BD3:
			spr_u200A.ᜀ = list.ToArray();
			list.Clear();
			return spr_u200A;
		}
		}
	}

	// Token: 0x060033CC RID: 13260 RVA: 0x002F9BE4 File Offset: 0x002F8BE4
	private void ᜀ(spr\u200A A_0, List<sprṴ> A_1, string A_2, float A_3, TextLineType A_4)
	{
		int a_ = 4;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_79;
		}
		if (false)
		{
		}
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				num = 1;
				continue;
			case 1:
				if (A_2 == null)
				{
					num = 2;
					continue;
				}
				goto IL_D8;
			case 2:
				goto IL_77;
			case 3:
				goto IL_62;
			case 4:
				goto IL_D6;
			}
			if (A_0 == null)
			{
				num = 3;
			}
			else
			{
				num = 0;
			}
		}
		IL_62:
		throw new ArgumentNullException(ClipboardData.b("٩իmᕯⁱᅳյ൷ᙹࡻ", a_));
		IL_77:
		throw new ArgumentNullException(ClipboardData.b("٩իmᕯ", a_));
		IL_D6:
		goto IL_79;
		IL_D8:
		A_1.Add(new sprṴ
		{
			ᜀ = A_2,
			ᜁ = A_3,
			ᜂ = A_4
		});
		SizeF sizeF = A_0.ᜂ;
		sizeF.Height += this.ᜁ();
		sizeF.Width = Math.Max(sizeF.Width, A_3);
		A_0.ᜂ = sizeF;
		return;
		IL_79:
		throw new ArgumentNullException(ClipboardData.b("٩իmᕯű", a_));
	}

	// Token: 0x060033CD RID: 13261 RVA: 0x002F9D2C File Offset: 0x002F8D2C
	private sprṴ ᜀ(sprṴ A_0, bool A_1)
	{
		switch (0)
		{
		default:
		{
			string text;
			float num;
			for (;;)
			{
				text = A_0.ᜀ;
				num = A_0.ᜁ;
				bool flag = (A_0.ᜂ & TextLineType.FirstParagraphLine) == TextLineType.None;
				int num2 = 22;
				for (;;)
				{
					char[] trimChars;
					string text2;
					bool flag2;
					string text3;
					bool flag3;
					bool flag4;
					bool flag5;
					switch (num2)
					{
					case 0:
						goto IL_1AC;
					case 1:
						text2 = text.TrimStart(trimChars);
						goto IL_2FD;
					case 2:
						goto IL_295;
					case 3:
						if (sprᣨ.ᜀ(text))
						{
							num2 = 11;
							continue;
						}
						goto IL_1D2;
					case 4:
						if ((A_0.ᜂ & TextLineType.FirstParagraphLine) > TextLineType.None)
						{
							num2 = 21;
							continue;
						}
						goto IL_399;
					case 5:
						IL_191:
						num2 = 8;
						continue;
					case 6:
						flag2 = true;
						goto IL_170;
					case 7:
						if (this.ᜂ != null)
						{
							num2 = 31;
							continue;
						}
						num2 = 6;
						continue;
					case 8:
						if ((A_0.ᜂ & TextLineType.FirstParagraphLine) > TextLineType.None)
						{
							num2 = 10;
							continue;
						}
						goto IL_1D2;
					case 9:
						num2 = 13;
						continue;
					case 10:
						num2 = 3;
						continue;
					case 11:
						text = new string(' ', 1);
						num2 = 15;
						continue;
					case 12:
						num2 = 29;
						continue;
					case 13:
						text3 = text.TrimStart(trimChars);
						goto IL_32B;
					case 14:
						if (!flag3)
						{
							num2 = 20;
							continue;
						}
						num2 = 1;
						continue;
					case 15:
						goto IL_295;
					case 16:
						goto IL_257;
					case 17:
						text3 = text.TrimEnd(trimChars);
						goto IL_32B;
					case 18:
						if (text.Length != A_0.ᜀ.Length)
						{
							num2 = 25;
							continue;
						}
						goto IL_399;
					case 19:
						flag2 = (this.ᜂ.FormatFlags != StringFormatFlags.MeasureTrailingSpaces);
						goto IL_170;
					case 20:
						num2 = 28;
						continue;
					case 21:
						num += this.ᜀ(A_1);
						num2 = 0;
						continue;
					case 22:
						if (this.ᜂ != null)
						{
							num2 = 12;
							continue;
						}
						num2 = 30;
						continue;
					case 23:
						if (flag4)
						{
							num2 = 5;
							continue;
						}
						goto IL_295;
					case 24:
						num2 = 14;
						continue;
					case 25:
						num = this.ᜀ(text);
						num2 = 4;
						continue;
					case 26:
						if (!flag3)
						{
							num2 = 9;
							continue;
						}
						num2 = 17;
						continue;
					case 27:
						if (flag)
						{
							num2 = 24;
							continue;
						}
						goto IL_257;
					case 28:
						text2 = text.TrimEnd(trimChars);
						goto IL_2FD;
					case 29:
						flag5 = (this.ᜂ.FormatFlags != StringFormatFlags.DirectionRightToLeft);
						goto IL_36E;
					case 30:
						flag5 = true;
						goto IL_36E;
					case 31:
						num2 = 19;
						continue;
					}
					break;
					IL_170:
					flag4 = flag2;
					num2 = 23;
					continue;
					IL_2FD:
					text = text2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_191;
					default:
						if (false)
						{
						}
						num2 = 16;
						continue;
					}
					IL_1D2:
					num2 = 26;
					continue;
					IL_257:
					num2 = 7;
					continue;
					IL_295:
					num2 = 18;
					continue;
					IL_32B:
					text = text3;
					num2 = 2;
					continue;
					IL_36E:
					flag3 = flag5;
					trimChars = sprᣨ.ᜅ;
					num2 = 27;
				}
			}
			IL_1AC:
			if (true)
			{
			}
			IL_399:
			A_0.ᜀ = text;
			A_0.ᜁ = num;
			return A_0;
		}
		}
	}

	// Token: 0x060033CE RID: 13262 RVA: 0x002FA0E4 File Offset: 0x002F90E4
	private float ᜀ(string A_0)
	{
		float num;
		for (;;)
		{
			IL_00:
			for (;;)
			{
				IL_4A:
				num = this.ᜅ.MeasureString(A_0, this.ᜁ, new SizeF(float.MaxValue, float.MaxValue), this.ᜂ).Width;
				int num2 = 2;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						switch (num2)
						{
						case 0:
							num2 = 6;
							continue;
						case 1:
							num2 = 3;
							continue;
						case 2:
							if ((double)num == 0.0)
							{
								num2 = 1;
								continue;
							}
							goto IL_101;
						case 3:
							if (!this.ᜇ)
							{
								num2 = 0;
								continue;
							}
							goto IL_101;
						case 4:
							num = (float)this.ᜀ(A_0, this.ᜁ).Width;
							num2 = 5;
							continue;
						case 5:
							goto IL_BE;
						case 6:
							if (A_0.Length > 1)
							{
								num2 = 4;
								continue;
							}
							goto IL_101;
						}
						goto IL_4A;
					}
				}
			}
		}
		IL_BE:
		IL_101:
		if (true)
		{
		}
		return num;
	}

	// Token: 0x060033CF RID: 13263 RVA: 0x002FA1FC File Offset: 0x002F91FC
	private float ᜀ(bool A_0)
	{
		float num2;
		float num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_95:
			num = num2;
			goto IL_BF;
		case 1:
			goto IL_20;
		default:
			goto IL_20;
		}
		int num3;
		for (;;)
		{
			IL_30:
			switch (num3)
			{
			case 0:
				num3 = 1;
				continue;
			case 1:
				goto IL_95;
			case 2:
				return num2;
			case 3:
				if (this.ᜃ.Width <= 0f)
				{
					num3 = 0;
					continue;
				}
				num3 = 6;
				continue;
			case 4:
				if (this.ᜂ != null)
				{
					num3 = 5;
					continue;
				}
				return num2;
			case 5:
				num3 = 3;
				continue;
			case 6:
				goto IL_7A;
			}
			goto IL_52;
		}
		IL_7A:
		num = Math.Min(this.ᜃ.Width, num2);
		goto IL_BF;
		IL_20:
		if (false)
		{
		}
		if (true)
		{
		}
		IL_52:
		num2 = 0f;
		num3 = 4;
		goto IL_30;
		IL_BF:
		num2 = num;
		num3 = 2;
		goto IL_30;
	}

	// Token: 0x060033D0 RID: 13264 RVA: 0x002FA2D8 File Offset: 0x002F92D8
	private StringTrimming ᜀ()
	{
		for (;;)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_44;
				case 1:
					goto IL_7D;
				case 2:
					num = 1;
					continue;
				}
				if (this.ᜂ == null)
				{
					num = 2;
				}
				else
				{
					num = 0;
				}
			}
			IL_44:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_5A;
			}
		}
		IL_5A:
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜂ.Trimming;
		IL_7D:
		return StringTrimming.Word;
	}

	// Token: 0x04002819 RID: 10265
	private string ᜀ;

	// Token: 0x0400281A RID: 10266
	private Font ᜁ;

	// Token: 0x0400281B RID: 10267
	private StringFormat ᜂ;

	// Token: 0x0400281C RID: 10268
	private SizeF ᜃ;

	// Token: 0x0400281D RID: 10269
	private sprᣨ ᜄ;

	// Token: 0x0400281E RID: 10270
	private Graphics ᜅ;

	// Token: 0x0400281F RID: 10271
	private spr\u19E0 ᜆ;

	// Token: 0x04002820 RID: 10272
	private bool ᜇ;

	// Token: 0x04002821 RID: 10273
	private spr\u1AB8 ᜈ;
}
