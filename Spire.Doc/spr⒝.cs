using System;
using System.Drawing;
using System.Text.RegularExpressions;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;
using Spire.Layouting;

// Token: 0x02000148 RID: 328
internal class spr\u249D : spr\u2573
{
	// Token: 0x060008D7 RID: 2263 RVA: 0x000706A8 File Offset: 0x0006F6A8
	public spr\u249D(spr\u2297 A_0, sprᴉ A_1)
	{
		int a_ = 19;
		this.ᜁ = new Regex(ClipboardData.b("╸ࡺ", a_));
		base..ctor(A_0, A_1);
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x000706E0 File Offset: 0x0006F6E0
	public new spr\u2297 ᜅ()
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
		return this.ᜂ as spr\u2297;
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x00070728 File Offset: 0x0006F728
	internal new spr\u25E5 ᜄ()
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
		return this.ᜂ;
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x0007076C File Offset: 0x0006F76C
	internal new void ᜀ(spr\u25E5 A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x000707B0 File Offset: 0x0006F7B0
	public override sprᦰ ᜀ(RectangleF A_0)
	{
		int a_ = 13;
		switch (0)
		{
		default:
			for (;;)
			{
				base.ᜃ(A_0);
				float width = A_0.Width;
				spr\u2297 spr_u = this.ᜅ();
				SizeF sizeF = spr_u.ᜀ(base.\u171E());
				int num = 112;
				for (;;)
				{
					float num2;
					spr\u17BA spr_u17BA;
					bool flag;
					float a_2;
					float num3;
					sprḈ sprḈ;
					bool flag2;
					bool flag3;
					sprᦰ sprᦰ2;
					sprℐ sprℐ;
					bool flag4;
					float num4;
					bool flag5;
					sprᡌ sprᡌ2;
					RectangleF rectangleF3;
					spr\u17BA spr_u17BA2;
					RectangleF a_4;
					bool flag6;
					bool flag7;
					RectangleF rectangleF4;
					switch (num)
					{
					case 0:
						if (A_0.Height >= 1f)
						{
							goto IL_ABE;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_DDB;
						default:
							if (false)
							{
							}
							num = 110;
							continue;
						}
						break;
					case 1:
					{
						sprᦰ sprᦰ = this.ᜀ(this.ᜅ(), sizeF, A_0, this.ᜅ.ᜇ().Width);
						num = 109;
						continue;
					}
					case 2:
						num = 216;
						continue;
					case 3:
						goto IL_1802;
					case 4:
						num = 188;
						continue;
					case 5:
						goto IL_DDB;
					case 6:
						goto IL_1B19;
					case 7:
						if (num2 != 0f)
						{
							num = 65;
							continue;
						}
						goto IL_12D2;
					case 8:
						this.ᜀ(ref A_0, ref sizeF);
						num = 185;
						continue;
					case 9:
						num = 73;
						continue;
					case 10:
						goto IL_12D2;
					case 11:
						spr_u17BA = (this.ᜅ() as spr\u17BA);
						num = 47;
						continue;
					case 12:
						num = 28;
						continue;
					case 13:
						goto IL_1A5A;
					case 14:
						num = 211;
						continue;
					case 15:
						if (!this.ᜂ())
						{
							num = 122;
							continue;
						}
						goto IL_10B9;
					case 16:
						if (num2 >= this.ᜅ.ᜆ().Height)
						{
							num = 220;
							continue;
						}
						goto IL_10B9;
					case 17:
						goto IL_A18;
					case 18:
						goto IL_1A5A;
					case 19:
						if (sizeF.Height > this.ᜅ.ᜆ().Height)
						{
							num = 68;
							continue;
						}
						goto IL_12D2;
					case 20:
						num = 48;
						continue;
					case 21:
						num = 90;
						continue;
					case 22:
						num = 100;
						continue;
					case 23:
					{
						object obj;
						if ((obj as TextRange).NextSibling is FieldMark)
						{
							num = 4;
							continue;
						}
						goto IL_F7B;
					}
					case 24:
						goto IL_141E;
					case 25:
						if (!((this.ᜅ() as TextRange).PreviousSibling as TextRange).Text.EndsWith(ClipboardData.b("卲", a_)))
						{
							num = 9;
							continue;
						}
						goto IL_1D3C;
					case 26:
						goto IL_A18;
					case 27:
						if ((((spr_u as spr\u208E).ᜂ() as TextRange).NextSibling as Break).BreakType == BreakType.LineBreak)
						{
							num = 128;
							continue;
						}
						goto IL_1D76;
					case 28:
					{
						Paragraph paragraph;
						if ((paragraph.Owner.Owner.Owner as Table).IsFrame)
						{
							num = 55;
							continue;
						}
						goto IL_ABE;
					}
					case 29:
					{
						object obj;
						if (((obj as TextRange).PreviousSibling as FieldMark).Type == FieldMarkType.FieldSeparator)
						{
							num = 155;
							continue;
						}
						goto IL_F7B;
					}
					case 30:
						num = 127;
						continue;
					case 31:
						num = 212;
						continue;
					case 32:
					{
						sprᡌ sprᡌ;
						if (sprᡌ.ᜂ().ᜁ() != TextWrappingStyle.InFrontOfText)
						{
							num = 168;
							continue;
						}
						goto IL_1D3C;
					}
					case 33:
						num = 190;
						continue;
					case 34:
						if (num2 >= this.ᜅ.ᜆ().Height)
						{
							num = 6;
							continue;
						}
						goto IL_C91;
					case 35:
						num = 93;
						continue;
					case 36:
					{
						Section section;
						section.IsColumnsBreak = true;
						num = 138;
						continue;
					}
					case 37:
					{
						Paragraph paragraph;
						if (paragraph != null)
						{
							num = 123;
							continue;
						}
						goto IL_ABE;
					}
					case 38:
						num = 183;
						continue;
					case 39:
						num = 156;
						continue;
					case 40:
						goto IL_8FF;
					case 41:
					{
						TextRange textRange;
						Paragraph paragraph = textRange.OwnerParagraph;
						num = 13;
						continue;
					}
					case 42:
						num = 152;
						continue;
					case 43:
						if (flag)
						{
							num = 222;
							continue;
						}
						goto IL_13E6;
					case 44:
						num = 148;
						continue;
					case 45:
						num = 181;
						continue;
					case 46:
					{
						sprᡌ sprᡌ;
						a_2 = sprᡌ.ᜀ().X - A_0.X;
						num = 26;
						continue;
					}
					case 47:
						if (spr_u17BA != null)
						{
							num = 95;
							continue;
						}
						goto IL_1AD7;
					case 48:
						if (num3 <= sprḈ.ᜃ.ᜂ())
						{
							num = 226;
							continue;
						}
						goto IL_1B19;
					case 49:
					{
						object obj;
						if (((obj as TextRange).NextSibling as Field).Type != FieldType.FieldPage)
						{
							num = 30;
							continue;
						}
						goto IL_116C;
					}
					case 50:
					{
						sprᦰ sprᦰ;
						return sprᦰ;
					}
					case 51:
						if (flag2)
						{
							num = 62;
							continue;
						}
						goto IL_C91;
					case 52:
						this.ᜅ.ᜈ();
						num = 162;
						continue;
					case 53:
					{
						RectangleF rectangleF;
						if (!rectangleF.IsEmpty)
						{
							num = 21;
							continue;
						}
						goto IL_1D3C;
					}
					case 54:
						if (spr_u is spr\u208E)
						{
							num = 232;
							continue;
						}
						goto IL_1D76;
					case 55:
						flag3 = true;
						num = 200;
						continue;
					case 56:
						goto IL_141E;
					case 57:
						if (!(this.ᜅ() is TextRange))
						{
							num = 147;
							continue;
						}
						goto IL_1802;
					case 58:
						if (spr_u is TextRange)
						{
							num = 44;
							continue;
						}
						goto IL_16B2;
					case 59:
						goto IL_F7B;
					case 60:
						num = 105;
						continue;
					case 61:
						if ((this.ᜅ() as TextRange).Text.Length > 0)
						{
							num = 171;
							continue;
						}
						goto IL_1D3C;
					case 62:
						num = 88;
						continue;
					case 63:
						return sprᦰ2;
					case 64:
						if (!char.IsPunctuation(((this.ᜅ() as TextRange).PreviousSibling as TextRange).Text, ((this.ᜅ() as TextRange).PreviousSibling as TextRange).Text.Length - 1))
						{
							num = 141;
							continue;
						}
						goto IL_1D3C;
					case 65:
						num = 85;
						continue;
					case 66:
					{
						object obj;
						if ((obj as Field).DocumentObjectType == DocumentObjectType.Field)
						{
							num = 193;
							continue;
						}
						goto IL_17D9;
					}
					case 67:
						flag4 = sprℐ.\u171E();
						goto IL_1180;
					case 68:
						num = 7;
						continue;
					case 69:
						num = 192;
						continue;
					case 70:
						if (sprḈ == null)
						{
							num = 39;
							continue;
						}
						goto IL_12D2;
					case 71:
						num = 72;
						continue;
					case 72:
					{
						sprᡌ sprᡌ;
						if (sprᡌ.ᜂ().ᜁ() != TextWrappingStyle.Behind)
						{
							num = 131;
							continue;
						}
						goto IL_1D3C;
					}
					case 73:
						if (((this.ᜅ() as TextRange).PreviousSibling as TextRange).Text.Length > 0)
						{
							num = 151;
							continue;
						}
						goto IL_1D3C;
					case 74:
						this.ᜃ = this.ᜀ(A_0, sizeF);
						num = 159;
						continue;
					case 75:
						num = 70;
						continue;
					case 76:
					{
						object obj;
						if (((obj as TextRange).NextSibling as Field).DocumentObjectType == DocumentObjectType.Field)
						{
							num = 87;
							continue;
						}
						goto IL_C68;
					}
					case 77:
						num = 86;
						continue;
					case 78:
						num = 186;
						continue;
					case 79:
					{
						Paragraph paragraph = (spr_u as TextRange).OwnerParagraph;
						num = 199;
						continue;
					}
					case 80:
						num = 176;
						continue;
					case 81:
						num = 117;
						continue;
					case 82:
						goto IL_10B9;
					case 83:
						goto IL_531;
					case 84:
						flag4 = false;
						goto IL_1180;
					case 85:
						if (num2 >= this.ᜅ.ᜆ().Height)
						{
							num = 10;
							continue;
						}
						goto IL_C91;
					case 86:
						if (((spr_u as spr\u208E).ᜂ() as TextRange).NextSibling is Break)
						{
							num = 214;
							continue;
						}
						goto IL_1D76;
					case 87:
						num = 49;
						continue;
					case 88:
						if (sprḈ != null)
						{
							num = 20;
							continue;
						}
						goto IL_1B19;
					case 89:
						num = 25;
						continue;
					case 90:
					{
						sprᡌ sprᡌ;
						if (sprᡌ.ᜂ().ᜁ() != TextWrappingStyle.Inline)
						{
							num = 71;
							continue;
						}
						goto IL_1D3C;
					}
					case 91:
						if (flag2)
						{
							num = 120;
							continue;
						}
						goto IL_C91;
					case 92:
						if ((spr_u as spr\u208E).ᜂ() is TextRange)
						{
							num = 77;
							continue;
						}
						goto IL_1D76;
					case 93:
						num4 = 0f;
						goto IL_1396;
					case 94:
						num = 149;
						continue;
					case 95:
						num = 234;
						continue;
					case 96:
						if (num2 != 0f)
						{
							num = 98;
							continue;
						}
						goto IL_1B19;
					case 97:
						num = 15;
						continue;
					case 98:
						num = 34;
						continue;
					case 99:
						goto IL_122A;
					case 100:
						if ((this.ᜅ() as TextRange).PreviousSibling != null)
						{
							num = 78;
							continue;
						}
						goto IL_1D3C;
					case 101:
					{
						TextRange textRange;
						if (textRange.OwnerParagraph != null)
						{
							num = 41;
							continue;
						}
						num = 215;
						continue;
					}
					case 102:
						flag5 = this.ᜅ().ᜀ().ᜀ();
						goto IL_14D2;
					case 103:
						num = 19;
						continue;
					case 104:
						if (!(this.ᜃ.ᜂ() is TextRange))
						{
							num = 129;
							continue;
						}
						goto IL_8FF;
					case 105:
						if ((spr_u as TextRange).OwnerParagraph != null)
						{
							num = 79;
							continue;
						}
						num = 136;
						continue;
					case 106:
						if (((spr_u as TextRange).NextSibling as Break).BreakType != BreakType.LineBreak)
						{
							num = 198;
							continue;
						}
						goto IL_519;
					case 107:
					{
						object obj;
						if (obj is TextRange)
						{
							num = 113;
							continue;
						}
						goto IL_F7B;
					}
					case 108:
						if (this.ᜅ() is spr\u208E)
						{
							num = 3;
							continue;
						}
						num = 236;
						continue;
					case 109:
					{
						sprᦰ sprᦰ;
						if (sprᦰ != null)
						{
							num = 50;
							continue;
						}
						this.ᜀ(sizeF, spr_u);
						sprᡌ2 = base.\u171E().\u171D().ᜀ(this.ᜃ.ᜁ(), this.ᜃ.ᜂ());
						num = 104;
						continue;
					}
					case 110:
					{
						Paragraph paragraph = null;
						num = 223;
						continue;
					}
					case 111:
					{
						RectangleF rectangleF2;
						if (rectangleF2.Height > 0f)
						{
							num = 82;
							continue;
						}
						goto IL_13E6;
					}
					case 112:
						if (spr_u is Break)
						{
							num = 45;
							continue;
						}
						goto IL_184B;
					case 113:
						num = 23;
						continue;
					case 114:
						goto IL_16B2;
					case 115:
						if (this.ᜅ() is TextRange)
						{
							num = 170;
							continue;
						}
						goto IL_1D3C;
					case 116:
						if (num2 != 0f)
						{
							num = 209;
							continue;
						}
						goto IL_CD8;
					case 117:
					{
						object obj;
						if ((obj as Field).Type != FieldType.FieldNumPages)
						{
							num = 118;
							continue;
						}
						goto IL_116C;
					}
					case 118:
						goto IL_17D9;
					case 119:
						if (spr_u is TextRange)
						{
							num = 33;
							continue;
						}
						goto IL_122A;
					case 120:
						num = 208;
						continue;
					case 121:
						num = 58;
						continue;
					case 122:
						num = 116;
						continue;
					case 123:
						num = 133;
						continue;
					case 124:
					{
						object obj;
						if ((obj as TextRange).PreviousSibling is FieldMark)
						{
							num = 158;
							continue;
						}
						goto IL_F7B;
					}
					case 125:
						goto IL_141E;
					case 126:
						goto IL_1044;
					case 127:
					{
						object obj;
						if (((obj as TextRange).NextSibling as Field).Type != FieldType.FieldNumPages)
						{
							num = 187;
							continue;
						}
						goto IL_116C;
					}
					case 128:
						goto IL_519;
					case 129:
						num = 230;
						continue;
					case 130:
						if (rectangleF3.Width <= 0f)
						{
							num = 14;
							continue;
						}
						goto IL_F7B;
					case 131:
						num = 32;
						continue;
					case 132:
						if (spr_u is spr\u208E)
						{
							num = 69;
							continue;
						}
						goto IL_1A5A;
					case 133:
					{
						Paragraph paragraph;
						if (paragraph.IsInCell)
						{
							num = 12;
							continue;
						}
						goto IL_ABE;
					}
					case 134:
						goto IL_141E;
					case 135:
						if ((this.ᜆ as spr\u1DA4).ᜂ())
						{
							num = 142;
							continue;
						}
						goto IL_1044;
					case 136:
						if ((spr_u as TextRange).OwnerEmptyParagraph != null)
						{
							num = 233;
							continue;
						}
						goto IL_1A5A;
					case 137:
					{
						object obj;
						if ((obj as Field).Type != FieldType.FieldPage)
						{
							num = 81;
							continue;
						}
						goto IL_116C;
					}
					case 138:
						goto IL_184B;
					case 139:
						num = 157;
						continue;
					case 140:
					{
						TextRange textRange = (spr_u as spr\u208E).ᜂ() as TextRange;
						num = 101;
						continue;
					}
					case 141:
					{
						RectangleF a_3 = new RectangleF(A_0.Location, sizeF);
						sprᡌ sprᡌ = base.\u171E().\u171D().ᜀ(a_3, this.ᜅ());
						num = 115;
						continue;
					}
					case 142:
						goto IL_600;
					case 143:
						if (spr_u17BA2 != null)
						{
							num = 167;
							continue;
						}
						goto IL_13E6;
					case 144:
						goto IL_1976;
					case 145:
						num = 218;
						continue;
					case 146:
						flag3 = true;
						num = 114;
						continue;
					case 147:
						num = 108;
						continue;
					case 148:
						if ((spr_u as TextRange).Text.Length == 1)
						{
							num = 2;
							continue;
						}
						goto IL_16B2;
					case 149:
						if (num2 < this.ᜅ.ᜆ().Height)
						{
							num = 245;
							continue;
						}
						goto IL_1AD7;
					case 150:
						num = 174;
						continue;
					case 151:
						num = 64;
						continue;
					case 152:
						if (this.ᜅ.ᜂ() != 0.0)
						{
							num = 52;
							continue;
						}
						goto IL_931;
					case 153:
					{
						sprᡌ sprᡌ;
						if (A_0.X < sprᡌ.ᜀ().X)
						{
							num = 46;
							continue;
						}
						a_2 = this.ᜅ.ᜇ().Right - sprᡌ.ᜀ().Right;
						a_4.X = sprᡌ.ᜀ().Right;
						num = 17;
						continue;
					}
					case 154:
						if (sprᡌ2.ᜂ().ᜁ() != TextWrappingStyle.InFrontOfText)
						{
							num = 11;
							continue;
						}
						goto IL_1BE1;
					case 155:
						goto IL_116C;
					case 156:
						if (sizeF.Width < this.ᜅ.ᜆ().Width)
						{
							num = 103;
							continue;
						}
						goto IL_12D2;
					case 157:
					{
						object obj;
						if ((obj as TextRange).NextSibling is Field)
						{
							num = 169;
							continue;
						}
						goto IL_C68;
					}
					case 158:
						num = 29;
						continue;
					case 159:
						if (this.ᜃ != null)
						{
							num = 144;
							continue;
						}
						goto IL_F4A;
					case 160:
						flag6 = (spr_u is Symbol);
						goto IL_13A9;
					case 161:
					{
						object obj = spr_u;
						num = 163;
						continue;
					}
					case 162:
						goto IL_931;
					case 163:
					{
						object obj;
						if (obj is TextRange)
						{
							num = 139;
							continue;
						}
						goto IL_C68;
					}
					case 164:
						if (sprḈ != null)
						{
							num = 8;
							continue;
						}
						goto IL_1044;
					case 165:
						if (this.ᜅ() is TextRange)
						{
							num = 74;
							continue;
						}
						goto IL_F4A;
					case 166:
						if (num2 != 0f)
						{
							num = 94;
							continue;
						}
						goto IL_1AD7;
					case 167:
						num = 205;
						continue;
					case 168:
						a_2 = 0f;
						a_4 = A_0;
						num = 153;
						continue;
					case 169:
						num = 76;
						continue;
					case 170:
					{
						sprᡌ sprᡌ;
						RectangleF rectangleF = sprᡌ.ᜀ();
						num = 53;
						continue;
					}
					case 171:
						num = 197;
						continue;
					case 172:
						num = 124;
						continue;
					case 173:
					{
						Section section;
						if (section != null)
						{
							num = 36;
							continue;
						}
						goto IL_184B;
					}
					case 174:
						if (base.\u171A().ᜇ())
						{
							num = 5;
							continue;
						}
						this.ᜀ = LayoutState.Breaked;
						num = 134;
						continue;
					case 175:
						num = 84;
						continue;
					case 176:
						if (sprᡌ2.ᜂ().ᜁ() != TextWrappingStyle.Inline)
						{
							num = 239;
							continue;
						}
						goto IL_1BE1;
					case 177:
						goto IL_1257;
					case 178:
						if (this.ᜅ() is TextRange)
						{
							num = 31;
							continue;
						}
						goto IL_1D3C;
					case 179:
						if (spr_u is TextRange)
						{
							num = 145;
							continue;
						}
						goto IL_1BB9;
					case 180:
					{
						TextRange textRange;
						Paragraph paragraph = textRange.OwnerEmptyParagraph;
						num = 18;
						continue;
					}
					case 181:
						if ((spr_u as Break).BreakType == BreakType.ColumnBreak)
						{
							num = 195;
							continue;
						}
						goto IL_184B;
					case 182:
						num = 206;
						continue;
					case 183:
						if (!this.ᜂ())
						{
							num = 217;
							continue;
						}
						goto IL_D39;
					case 184:
						if (!flag3)
						{
							num = 121;
							continue;
						}
						goto IL_16B2;
					case 185:
						if (!(this.ᜆ as spr\u1DA4).ᜅ())
						{
							num = 219;
							continue;
						}
						goto IL_600;
					case 186:
						if ((this.ᜅ() as TextRange).PreviousSibling is TextRange)
						{
							num = 89;
							continue;
						}
						goto IL_1D3C;
					case 187:
						goto IL_C68;
					case 188:
					{
						object obj;
						if (((obj as TextRange).NextSibling as FieldMark).Type == FieldMarkType.FieldEnd)
						{
							num = 172;
							continue;
						}
						goto IL_F7B;
					}
					case 189:
						if (flag2)
						{
							num = 75;
							continue;
						}
						goto IL_C91;
					case 190:
						if (sizeF.Height <= this.ᜅ.ᜆ().Height)
						{
							num = 35;
							continue;
						}
						num = 231;
						continue;
					case 191:
						if (sprᡌ2.ᜂ().ᜁ() != TextWrappingStyle.Behind)
						{
							num = 244;
							continue;
						}
						goto IL_1BE1;
					case 192:
						if ((spr_u as spr\u208E).ᜂ() is TextRange)
						{
							num = 140;
							continue;
						}
						goto IL_1A5A;
					case 193:
						num = 137;
						continue;
					case 194:
						if (base.\u171A().ᜀ())
						{
							num = 42;
							continue;
						}
						goto IL_931;
					case 195:
					{
						sizeF = new SizeF(0f, A_0.Height);
						Section section = this.ᜁ(spr_u as Break);
						num = 173;
						continue;
					}
					case 196:
						num = 210;
						continue;
					case 197:
						if (!char.IsPunctuation((this.ᜅ() as TextRange).Text, 0))
						{
							num = 22;
							continue;
						}
						goto IL_1D3C;
					case 198:
						goto IL_1BB9;
					case 199:
						goto IL_1A5A;
					case 200:
						goto IL_ABE;
					case 201:
						if (this.ᜃ != null)
						{
							num = 235;
							continue;
						}
						goto IL_1D76;
					case 202:
						num = 61;
						continue;
					case 203:
						num = 96;
						continue;
					case 204:
					{
						object obj;
						if (obj is Field)
						{
							num = 242;
							continue;
						}
						goto IL_17D9;
					}
					case 205:
						if (sizeF.Height > this.ᜅ.ᜆ().Height)
						{
							num = 97;
							continue;
						}
						goto IL_10B9;
					case 206:
						if (sizeF.Height > this.ᜅ.ᜆ().Height)
						{
							num = 177;
							continue;
						}
						goto IL_C91;
					case 207:
						goto IL_141E;
					case 208:
						if (sprḈ != null)
						{
							num = 196;
							continue;
						}
						goto IL_1257;
					case 209:
						num = 16;
						continue;
					case 210:
						if (num3 <= sprḈ.ᜃ.ᜂ())
						{
							num = 182;
							continue;
						}
						goto IL_1257;
					case 211:
						if (!flag3)
						{
							num = 161;
							continue;
						}
						goto IL_F7B;
					case 212:
						if (!(this.ᜅ() as TextRange).Text.StartsWith(ClipboardData.b("卲", a_)))
						{
							num = 202;
							continue;
						}
						goto IL_1D3C;
					case 213:
						goto IL_141E;
					case 214:
						num = 27;
						continue;
					case 215:
					{
						TextRange textRange;
						if (textRange.OwnerEmptyParagraph != null)
						{
							num = 180;
							continue;
						}
						goto IL_1A5A;
					}
					case 216:
						if (!base.\u171E().ᜄ((spr_u as TextRange).Text[0]))
						{
							num = 146;
							continue;
						}
						goto IL_16B2;
					case 217:
						num = 166;
						continue;
					case 218:
						if ((spr_u as TextRange).NextSibling is Break)
						{
							num = 225;
							continue;
						}
						goto IL_1BB9;
					case 219:
						if (true)
						{
						}
						num = 135;
						continue;
					case 220:
						goto IL_CD8;
					case 221:
						if (flag3)
						{
							num = 1;
							continue;
						}
						num = 178;
						continue;
					case 222:
					{
						RectangleF rectangleF2 = this.ᜅ.ᜆ();
						num = 111;
						continue;
					}
					case 223:
						if (spr_u is TextRange)
						{
							num = 60;
							continue;
						}
						num = 132;
						continue;
					case 224:
						num = 0;
						continue;
					case 225:
						num = 106;
						continue;
					case 226:
						num = 228;
						continue;
					case 227:
						goto IL_1A5A;
					case 228:
						if (sizeF.Height > this.ᜅ.ᜆ().Height)
						{
							num = 203;
							continue;
						}
						goto IL_1B19;
					case 229:
						if (flag7)
						{
							num = 150;
							continue;
						}
						goto IL_4C4;
					case 230:
						if (this.ᜃ.ᜂ() is spr\u208E)
						{
							num = 40;
							continue;
						}
						goto IL_1BE1;
					case 231:
						num4 = (spr_u as TextRange).CharacterFormat.FontSize;
						goto IL_1396;
					case 232:
						num = 92;
						continue;
					case 233:
					{
						Paragraph paragraph = (spr_u as TextRange).OwnerEmptyParagraph;
						num = 227;
						continue;
					}
					case 234:
						if (sizeF.Height > this.ᜅ.ᜆ().Height)
						{
							num = 38;
							continue;
						}
						goto IL_D39;
					case 235:
						num = 179;
						continue;
					case 236:
						flag5 = false;
						goto IL_14D2;
					case 237:
						if (!flag3)
						{
							num = 224;
							continue;
						}
						goto IL_ABE;
					case 238:
						if (!rectangleF4.IsEmpty)
						{
							num = 80;
							continue;
						}
						goto IL_1BE1;
					case 239:
						num = 191;
						continue;
					case 240:
						flag6 = true;
						goto IL_13A9;
					case 241:
						if (sprℐ == null)
						{
							num = 175;
							continue;
						}
						num = 67;
						continue;
					case 242:
						num = 66;
						continue;
					case 243:
						if (sprᦰ2 != null)
						{
							num = 63;
							continue;
						}
						goto IL_1D3C;
					case 244:
						num = 154;
						continue;
					case 245:
						goto IL_D39;
					}
					break;
					IL_4C4:
					this.ᜀ = LayoutState.Fitted;
					num = 56;
					continue;
					IL_DDB:
					goto IL_4C4;
					IL_519:
					this.ᜃ.ᜂ(true);
					num = 83;
					continue;
					IL_600:
					(spr_u as TextRange).Text = ClipboardData.b("穲", a_);
					num = 126;
					continue;
					IL_8FF:
					rectangleF4 = sprᡌ2.ᜀ();
					num = 238;
					continue;
					IL_931:
					num3 = A_0.X + sizeF.Width;
					num2 = 0f;
					num = 119;
					continue;
					IL_A18:
					sprᦰ2 = this.ᜀ(this.ᜅ(), sizeF, a_4, a_2);
					num = 243;
					continue;
					IL_ABE:
					num = 184;
					continue;
					IL_C68:
					num = 204;
					continue;
					IL_C91:
					num = 240;
					continue;
					IL_CD8:
					num = 43;
					continue;
					IL_D39:
					this.ᜀ(spr_u17BA, this.ᜅ.ᜇ().Width);
					num = 213;
					continue;
					IL_F4A:
					sprḈ = (spr_u.ᜀ() as sprḈ);
					num = 164;
					continue;
					IL_F7B:
					num = 237;
					continue;
					IL_1044:
					num = 194;
					continue;
					IL_10B9:
					this.ᜀ(spr_u17BA2, width);
					num = 24;
					continue;
					IL_116C:
					flag3 = true;
					num = 59;
					continue;
					IL_1180:
					flag7 = flag4;
					num = 229;
					continue;
					IL_122A:
					flag2 = this.ᜅ(sizeF);
					num = 91;
					continue;
					IL_1257:
					num = 51;
					continue;
					IL_12D2:
					num = 160;
					continue;
					IL_1396:
					num2 = num4;
					num = 99;
					continue;
					IL_13A9:
					flag3 = flag6;
					rectangleF3 = this.ᜅ.ᜇ();
					num = 130;
					continue;
					IL_13E6:
					this.ᜀ = LayoutState.NotFitted;
					this.ᜇ = (sizeF.Height > this.ᜅ.ᜆ().Height);
					num = 207;
					continue;
					IL_141E:
					this.ᜈ();
					num = 201;
					continue;
					IL_14D2:
					flag = flag5;
					num = 143;
					continue;
					IL_16B2:
					num = 221;
					continue;
					IL_17D9:
					num = 107;
					continue;
					IL_1802:
					num = 102;
					continue;
					IL_184B:
					this.ᜁ(spr_u, sizeF, ref A_0);
					num = 165;
					continue;
					IL_1A5A:
					num = 37;
					continue;
					IL_1AD7:
					this.ᜀ = LayoutState.NotFitted;
					this.ᜇ = (sizeF.Height > this.ᜅ.ᜆ().Height);
					num = 125;
					continue;
					IL_1B19:
					num = 189;
					continue;
					IL_1BB9:
					num = 54;
					continue;
					IL_1BE1:
					sprℐ = (base.\u171A() as sprℐ);
					num = 241;
					continue;
					IL_1D3C:
					spr_u17BA2 = (this.ᜅ() as spr\u17BA);
					num = 57;
				}
			}
			IL_531:
			goto IL_1D76;
			IL_1976:
			return this.ᜃ;
			IL_1D76:
			return this.ᜃ;
		}
	}

	// Token: 0x060008DC RID: 2268 RVA: 0x0007253C File Offset: 0x0007153C
	private new Section ᜁ(DocumentObject A_0)
	{
		DocumentObject owner;
		for (;;)
		{
			owner = A_0.Owner;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6B;
				case 1:
					goto IL_6D;
				case 2:
					if (owner is Section)
					{
						num = 0;
						continue;
					}
					owner = owner.Owner;
					num = 3;
					continue;
				case 3:
					goto IL_6D;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_75;
					default:
						goto IL_A1;
					}
					break;
				case 5:
					goto IL_75;
				}
				break;
				IL_75:
				if (true)
				{
				}
				if (owner == null)
				{
					num = 4;
					continue;
				}
				num = 2;
				continue;
				IL_6D:
				num = 5;
			}
		}
		IL_6B:
		return owner as Section;
		IL_A1:
		if (false)
		{
		}
		return null;
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x000725F4 File Offset: 0x000715F4
	private new sprᦰ ᜀ(spr\u2297 A_0, SizeF A_1, RectangleF A_2, float A_3)
	{
		int a_ = 13;
		switch (0)
		{
		default:
			for (;;)
			{
				TextRange textRange = A_0 as TextRange;
				bool flag = false;
				int num = 24;
				for (;;)
				{
					TextRange textRange2;
					string text;
					TextRange textRange3;
					spr\u1AB8 spr_u1AB2;
					string text2;
					float num2;
					TextRange textRange4;
					TableCell tableCell;
					SizeF sizeF;
					spr\u17BA[] array2;
					RectangleF rectangleF;
					TextRange textRange5;
					int num4;
					bool flag2;
					TextRange textRange6;
					TextRange textRange7;
					TextRange textRange8;
					int num6;
					int num7;
					float num10;
					int num11;
					int num12;
					float num14;
					int num15;
					switch (num)
					{
					case 0:
						num = 43;
						continue;
					case 1:
						goto IL_1C15;
					case 2:
						if (!textRange2.Text.StartsWith(ClipboardData.b("卲", a_)))
						{
							num = 12;
							continue;
						}
						goto IL_5FF;
					case 3:
					{
						spr\u1AB8 spr_u1AB;
						if (text.Length != (spr_u1AB as spr\u208E).ᜃ().Length)
						{
							num = 143;
							continue;
						}
						goto IL_1623;
					}
					case 4:
						if (textRange.NextSibling is TextRange)
						{
							num = 66;
							continue;
						}
						goto IL_17CC;
					case 5:
						num = 157;
						continue;
					case 6:
						num = 149;
						continue;
					case 7:
					{
						string[] array;
						textRange3.Text = array[0] + ClipboardData.b("卲", a_);
						num = 128;
						continue;
					}
					case 8:
						goto IL_4F4;
					case 9:
						text2 = (spr_u1AB2 as TextRange).Text;
						num = 27;
						continue;
					case 10:
						goto IL_81F;
					case 11:
						if (base.\u171E().ᜄ(textRange.Text.ToCharArray()[0]))
						{
							num = 65;
							continue;
						}
						goto IL_1157;
					case 12:
						num = 38;
						continue;
					case 13:
						goto IL_1623;
					case 14:
						goto IL_98E;
					case 15:
						if (flag)
						{
							num = 155;
							continue;
						}
						goto IL_19DE;
					case 16:
						num2 = 0f;
						goto IL_B58;
					case 17:
						goto IL_4F4;
					case 18:
						num = 129;
						continue;
					case 19:
						num = 2;
						continue;
					case 20:
						if (textRange4 != null)
						{
							num = 205;
							continue;
						}
						goto IL_1971;
					case 21:
						if (textRange.Text.Length > 0)
						{
							num = 190;
							continue;
						}
						goto IL_5FF;
					case 22:
						if (!tableCell.CellFormat.Paddings.IsEmpty)
						{
							num = 25;
							continue;
						}
						num = 162;
						continue;
					case 23:
						goto IL_16AC;
					case 24:
						if (A_3 > 0f)
						{
							num = 60;
							continue;
						}
						goto IL_1C39;
					case 25:
						num = 53;
						continue;
					case 26:
						num = 106;
						continue;
					case 27:
						goto IL_B95;
					case 28:
						goto IL_1AB6;
					case 29:
						goto IL_42E;
					case 30:
						num = 183;
						continue;
					case 31:
						num2 = (tableCell.Owner.Owner as Table).TableFormat.Paddings.Left;
						goto IL_B58;
					case 32:
						if (A_3 - A_1.Width - sizeF.Width <= -1f)
						{
							num = 110;
							continue;
						}
						goto IL_1C39;
					case 33:
					{
						this.ᜃ = new sprᦰ(array2[0]);
						SizeF size = array2[0].ᜀ(base.\u171E());
						this.ᜃ.ᜀ(new RectangleF(rectangleF.Location, size));
						num = 82;
						continue;
					}
					case 34:
						if (!textRange.Text.EndsWith(ClipboardData.b("卲", a_)))
						{
							num = 142;
							continue;
						}
						goto IL_5FF;
					case 35:
						num = 166;
						continue;
					case 36:
					{
						string text3;
						if (char.IsPunctuation(text3.ToCharArray()[0]))
						{
							num = 199;
							continue;
						}
						goto IL_1B59;
					}
					case 37:
					{
						spr\u2297 spr_u = textRange5;
						float num3;
						num3 += spr_u.ᜀ(base.\u171E()).Width;
						num4++;
						textRange5 = (textRange5.PreviousSibling as TextRange);
						num = 8;
						continue;
					}
					case 38:
						if (textRange2.Text.Length > 0)
						{
							num = 123;
							continue;
						}
						goto IL_5FF;
					case 39:
						if (!textRange.Text.Contains(ClipboardData.b("卲", a_)))
						{
							num = 42;
							continue;
						}
						goto IL_19DE;
					case 40:
						num = 3;
						continue;
					case 41:
						goto IL_17CC;
					case 42:
						num = 52;
						continue;
					case 43:
						if (!base.\u171E().ᜁ(textRange3.Text))
						{
							num = 5;
							continue;
						}
						goto IL_5FF;
					case 44:
						goto IL_19DE;
					case 45:
						if (spr_u1AB2 is TextRange)
						{
							num = 139;
							continue;
						}
						goto IL_15FA;
					case 46:
						flag2 = true;
						tableCell = (textRange.Owner.Owner as TableCell);
						num = 22;
						continue;
					case 47:
						num = 80;
						continue;
					case 48:
					{
						string text3;
						if (base.\u171E().ᜃ(text3.ToCharArray()[0]))
						{
							num = 187;
							continue;
						}
						goto IL_1B59;
					}
					case 49:
						goto IL_1ADC;
					case 50:
						num = 112;
						continue;
					case 51:
						goto IL_15B3;
					case 52:
						if (!this.ᜀ(textRange2.Text))
						{
							num = 172;
							continue;
						}
						goto IL_19DE;
					case 53:
						num2 = tableCell.CellFormat.Paddings.Left;
						goto IL_B58;
					case 54:
						textRange6 = new TextRange(textRange.Document);
						textRange6.ᜀ(textRange4.Owner);
						textRange6.CharacterFormat.Font = textRange4.CharacterFormat.Font;
						textRange6.CharacterFormat.FontSize = textRange4.CharacterFormat.FontSize;
						num = 191;
						continue;
					case 55:
						num = 152;
						continue;
					case 56:
						num = 31;
						continue;
					case 57:
						goto IL_1132;
					case 58:
						goto IL_CE7;
					case 59:
						textRange7 = null;
						goto IL_191E;
					case 60:
						num = 71;
						continue;
					case 61:
						num = 189;
						continue;
					case 62:
						if (!(tableCell.Owner.Owner as Table).TableFormat.Paddings.IsEmpty)
						{
							num = 115;
							continue;
						}
						num = 197;
						continue;
					case 63:
						goto IL_ACE;
					case 64:
						goto IL_1971;
					case 65:
						array2[0] = new spr\u208E(textRange, string.Empty);
						array2[1] = new spr\u208E(textRange, textRange.Text.TrimStart(new char[0]));
						num = 44;
						continue;
					case 66:
						num = 202;
						continue;
					case 67:
					{
						spr\u1AB8 spr_u1AB;
						text = (spr_u1AB as TextRange).Text;
						num = 13;
						continue;
					}
					case 68:
						num = 102;
						continue;
					case 69:
						textRange8 = new TextRange(textRange.Document);
						textRange8.ᜀ(textRange5.Owner);
						textRange8.CharacterFormat.Font = textRange5.CharacterFormat.Font;
						textRange8.CharacterFormat.FontSize = textRange5.CharacterFormat.FontSize;
						num = 10;
						continue;
					case 70:
						num = 147;
						continue;
					case 71:
						if (textRange != null)
						{
							num = 170;
							continue;
						}
						goto IL_1C39;
					case 72:
						if (!this.ᜀ(textRange.Text))
						{
							num = 158;
							continue;
						}
						goto IL_19DE;
					case 73:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_CE7;
						default:
							if (false)
							{
							}
							if (num4 <= this.ᜄ().\u171B().ᜊ().Count)
							{
								num = 58;
								continue;
							}
							goto IL_19DE;
						}
						break;
					case 74:
					{
						spr\u2297 spr_u2 = textRange4;
						float num5;
						num5 += spr_u2.ᜀ(base.\u171E()).Width;
						num6++;
						textRange4 = (textRange4.PreviousSibling as TextRange);
						num = 97;
						continue;
					}
					case 75:
						num = 113;
						continue;
					case 76:
						num = 86;
						continue;
					case 77:
						if (!textRange.Text.StartsWith(ClipboardData.b("卲", a_)))
						{
							num = 50;
							continue;
						}
						goto IL_18B5;
					case 78:
						goto IL_D48;
					case 79:
						if (textRange4.Text.Contains(ClipboardData.b("卲", a_)))
						{
							num = 181;
							continue;
						}
						textRange6 = textRange4;
						num = 132;
						continue;
					case 80:
						if (!char.IsPunctuation(textRange.Text, 0))
						{
							num = 18;
							continue;
						}
						goto IL_18B5;
					case 81:
						if (num7 >= num6)
						{
							num = 194;
							continue;
						}
						this.ᜄ().\u171B().ᜊ().RemoveAt(this.ᜄ().\u171B().ᜊ().Count - 1);
						num7++;
						num = 28;
						continue;
					case 82:
						if (array2[1] != null)
						{
							num = 167;
							continue;
						}
						goto IL_D5C;
					case 83:
						num = 95;
						continue;
					case 84:
						if (textRange != null)
						{
							num = 83;
							continue;
						}
						goto IL_1C39;
					case 85:
					{
						spr\u1AB8 spr_u1AB;
						if (text.Length != (spr_u1AB as TextRange).Text.Length)
						{
							num = 67;
							continue;
						}
						goto IL_A81;
					}
					case 86:
						if (textRange.TextToSplit != ClipboardData.b("穲", a_))
						{
							num = 192;
							continue;
						}
						goto IL_1C39;
					case 87:
					{
						int num8 = text2.LastIndexOf(' ');
						array2[0] = new spr\u208E(textRange5, text2.Substring(0, num8 + 1));
						array2[1] = new spr\u208E(textRange5, text2.Substring(num8 + 1, text2.Length - num8 - 1).TrimStart(new char[0]));
						flag = true;
						num = 78;
						continue;
					}
					case 88:
						num = 188;
						continue;
					case 89:
						goto IL_D48;
					case 90:
					{
						string text4 = textRange.Text;
						int num9 = text4.LastIndexOf(' ');
						array2[0] = new spr\u208E(A_0 as spr\u1C7D, text4.Substring(0, num9 + 1));
						array2[1] = new spr\u208E(A_0 as spr\u1C7D, text4.Substring(num9 + 1, text4.Length - num9 - 1).TrimStart(new char[0]));
						flag = true;
						num = 135;
						continue;
					}
					case 91:
					{
						TextRange textRange9;
						if (textRange9 != null)
						{
							num = 26;
							continue;
						}
						goto IL_391;
					}
					case 92:
						if (textRange.Text.Length > 0)
						{
							num = 47;
							continue;
						}
						goto IL_18B5;
					case 93:
						if (textRange2.Text.Length > 1)
						{
							num = 35;
							continue;
						}
						goto IL_19DE;
					case 94:
					{
						bool flag3 = true;
						num = 154;
						continue;
					}
					case 95:
						if (textRange.DocumentObjectType == DocumentObjectType.TextRange)
						{
							num = 76;
							continue;
						}
						goto IL_1C39;
					case 96:
						text2 = (spr_u1AB2 as spr\u208E).ᜃ();
						num = 137;
						continue;
					case 97:
						goto IL_E21;
					case 98:
						num = 39;
						continue;
					case 99:
						goto IL_E21;
					case 100:
						num = 153;
						continue;
					case 101:
						if (!tableCell.CellFormat.Paddings.IsEmpty)
						{
							num = 30;
							continue;
						}
						num = 62;
						continue;
					case 102:
					{
						float num3;
						if (num3 > num10)
						{
							num = 1;
							continue;
						}
						goto IL_AAA;
					}
					case 103:
						goto IL_154D;
					case 104:
						goto IL_1623;
					case 105:
						if (num11 == 1)
						{
							num = 114;
							continue;
						}
						goto IL_1157;
					case 106:
					{
						TextRange textRange9;
						if (this.ᜀ(textRange9.Text))
						{
							num = 94;
							continue;
						}
						goto IL_391;
					}
					case 107:
						if (!textRange2.Text.Contains(ClipboardData.b("卲", a_)))
						{
							num = 51;
							continue;
						}
						goto IL_146D;
					case 108:
						num = 206;
						continue;
					case 109:
						if (!char.IsPunctuation(textRange.Text, 0))
						{
							num = 19;
							continue;
						}
						goto IL_5FF;
					case 110:
					{
						array2 = new spr\u17BA[2];
						rectangleF = new RectangleF(new PointF((float)((double)A_2.X - base.\u171A().ᜊ().ᜃ()), (float)((double)A_2.Y - base.\u171A().ᜊ().ᜁ())), new SizeF(0f, 0f));
						TextRange textRange9 = textRange.PreviousSibling as TextRange;
						bool flag3 = false;
						num = 91;
						continue;
					}
					case 111:
						num = 92;
						continue;
					case 112:
						if (!this.ᜀ(textRange.Text))
						{
							num = 111;
							continue;
						}
						goto IL_18B5;
					case 113:
					{
						if (textRange.Text.Contains(ClipboardData.b("卲", a_)))
						{
							num = 90;
							continue;
						}
						num6 = 0;
						textRange4 = textRange;
						float num5 = sizeF.Width;
						num = 99;
						continue;
					}
					case 114:
						num = 11;
						continue;
					case 115:
						num = 185;
						continue;
					case 116:
						if (textRange2 != null)
						{
							num = 174;
							continue;
						}
						goto IL_1C39;
					case 117:
						if (flag2)
						{
							num = 61;
							continue;
						}
						goto IL_15B3;
					case 118:
						if (spr_u1AB2 is spr\u208E)
						{
							num = 120;
							continue;
						}
						goto IL_B95;
					case 119:
						textRange7 = (textRange.NextSibling as TextRange);
						goto IL_191E;
					case 120:
						num = 168;
						continue;
					case 121:
						goto IL_19DE;
					case 122:
						if (textRange5.Text.Contains(ClipboardData.b("卲", a_)))
						{
							num = 87;
							continue;
						}
						textRange8 = textRange5;
						num = 146;
						continue;
					case 123:
						num = 182;
						continue;
					case 124:
						if (textRange5 != null)
						{
							num = 164;
							continue;
						}
						goto IL_19DE;
					case 125:
						num = 133;
						continue;
					case 126:
						goto IL_19DE;
					case 127:
						if (text2.Length != (spr_u1AB2 as TextRange).Text.Length)
						{
							num = 9;
							continue;
						}
						goto IL_15FA;
					case 128:
						goto IL_98E;
					case 129:
					{
						TextRange textRange9;
						if (textRange9 != null)
						{
							num = 108;
							continue;
						}
						goto IL_18B5;
					}
					case 130:
						if (textRange.Owner.Owner is TableCell)
						{
							num = 46;
							continue;
						}
						goto IL_1ADC;
					case 131:
						if (textRange4.DocumentObjectType != DocumentObjectType.TextRange)
						{
							num = 64;
							continue;
						}
						num = 201;
						continue;
					case 132:
						if (textRange.Document != null)
						{
							num = 54;
							continue;
						}
						goto IL_18E3;
					case 133:
						goto IL_19DE;
					case 134:
						goto IL_ACE;
					case 135:
						goto IL_19DE;
					case 136:
					{
						if (num12 < 0)
						{
							num = 23;
							continue;
						}
						string text3 = textRange.Text.Substring(num12, 1);
						num = 36;
						continue;
					}
					case 137:
						goto IL_B95;
					case 138:
					{
						TextRange textRange9;
						if (textRange9.Text.Length > 0)
						{
							num = 6;
							continue;
						}
						goto IL_18B5;
					}
					case 139:
						num = 127;
						continue;
					case 140:
					{
						text = textRange4.Text;
						spr\u1AB8 spr_u1AB = this.ᜄ().\u171B().ᜊ()[this.ᜄ().\u171B().ᜊ().Count - num6].ᜂ();
						num = 186;
						continue;
					}
					case 141:
						num = 85;
						continue;
					case 142:
						num = 21;
						continue;
					case 143:
					{
						spr\u1AB8 spr_u1AB;
						text = (spr_u1AB as spr\u208E).ᜃ();
						num = 104;
						continue;
					}
					case 144:
						if (flag)
						{
							num = 33;
							continue;
						}
						goto IL_1C39;
					case 145:
						if (textRange.Owner != null)
						{
							num = 70;
							continue;
						}
						goto IL_1ADC;
					case 146:
						if (textRange.Document != null)
						{
							num = 69;
							continue;
						}
						goto IL_81F;
					case 147:
						if (textRange.Owner.Owner != null)
						{
							num = 184;
							continue;
						}
						goto IL_1ADC;
					case 148:
						if (flag2)
						{
							num = 68;
							continue;
						}
						goto IL_1C15;
					case 149:
					{
						TextRange textRange9;
						if (!char.IsPunctuation(textRange9.Text, textRange9.Text.Length - 1))
						{
							num = 171;
							continue;
						}
						goto IL_18B5;
					}
					case 150:
						num = 138;
						continue;
					case 151:
						if (num6 <= this.ᜄ().\u171B().ᜊ().Count)
						{
							num = 140;
							continue;
						}
						goto IL_19DE;
					case 152:
					{
						TextRange textRange9;
						if (!textRange9.Text.Contains(ClipboardData.b("卲", a_)))
						{
							num = 150;
							continue;
						}
						goto IL_18B5;
					}
					case 153:
						if (!base.\u171E().ᜁ(textRange.Text))
						{
							num = 0;
							continue;
						}
						goto IL_5FF;
					case 154:
						goto IL_391;
					case 155:
						num11 += base.\u171E().ᜂ(textRange.Text, num11);
						num = 105;
						continue;
					case 156:
						goto IL_AAA;
					case 157:
					{
						bool flag3;
						if (!flag3)
						{
							num = 75;
							continue;
						}
						goto IL_5FF;
					}
					case 158:
						num11 = 0;
						num12 = textRange.Text.Length - 1;
						num = 193;
						continue;
					case 159:
					{
						bool flag3;
						if (!flag3)
						{
							num = 165;
							continue;
						}
						goto IL_18B5;
					}
					case 160:
						if (textRange5 != null)
						{
							num = 88;
							continue;
						}
						goto IL_154D;
					case 161:
						if (!flag2)
						{
							num = 156;
							continue;
						}
						goto IL_19DE;
					case 162:
						if (!(tableCell.Owner.Owner as Table).TableFormat.Paddings.IsEmpty)
						{
							num = 56;
							continue;
						}
						num = 16;
						continue;
					case 163:
						if (text2 == textRange5.Text)
						{
							num = 203;
							continue;
						}
						goto IL_19DE;
					case 164:
						num = 73;
						continue;
					case 165:
					{
						num4 = 1;
						TextRange textRange9;
						textRange5 = textRange9;
						float num3 = sizeF.Width;
						num = 17;
						continue;
					}
					case 166:
						if (!textRange2.Text.Contains(ClipboardData.b("卲", a_)))
						{
							num = 98;
							continue;
						}
						goto IL_19DE;
					case 167:
						this.ᜁ = array2[1];
						this.ᜀ = LayoutState.Splitted;
						num = 196;
						continue;
					case 168:
						if (text2.Length != (spr_u1AB2 as spr\u208E).ᜃ().Length)
						{
							num = 96;
							continue;
						}
						goto IL_B95;
					case 169:
						goto IL_16AC;
					case 170:
						num = 84;
						continue;
					case 171:
						num = 159;
						continue;
					case 172:
						num = 72;
						continue;
					case 173:
						if (textRange4 != null)
						{
							num = 200;
							continue;
						}
						goto IL_19DE;
					case 174:
					{
						string[] array = textRange2.Text.Split(new char[]
						{
							' '
						}, StringSplitOptions.RemoveEmptyEntries);
						textRange3 = (TextRange)textRange2.Clone();
						num = 204;
						continue;
					}
					case 175:
						goto IL_1AB6;
					case 176:
						goto IL_42E;
					case 177:
						num = 107;
						continue;
					case 178:
						if (!textRange5.Text.Contains(ClipboardData.b("卲", a_)))
						{
							num = 37;
							continue;
						}
						goto IL_154D;
					case 179:
						goto IL_146D;
					case 180:
						if (!flag2)
						{
							num = 179;
							continue;
						}
						goto IL_19DE;
					case 181:
					{
						int num13 = text.LastIndexOf(' ');
						array2[0] = new spr\u208E(textRange4, text.Substring(0, num13 + 1));
						array2[1] = new spr\u208E(textRange4, text.Substring(num13 + 1, text.Length - num13 - 1).TrimStart(new char[0]));
						flag = true;
						num = 63;
						continue;
					}
					case 182:
						if (!char.IsPunctuation(textRange2.Text, 0))
						{
							num = 100;
							continue;
						}
						goto IL_5FF;
					case 183:
						num14 = tableCell.CellFormat.Paddings.Right;
						goto IL_1BF3;
					case 184:
						num = 130;
						continue;
					case 185:
						num14 = (tableCell.Owner.Owner as Table).TableFormat.Paddings.Right;
						goto IL_1BF3;
					case 186:
					{
						spr\u1AB8 spr_u1AB;
						if (spr_u1AB is TextRange)
						{
							num = 141;
							continue;
						}
						goto IL_A81;
					}
					case 187:
						num11 = num12 + 1;
						flag = true;
						num = 169;
						continue;
					case 188:
						if (textRange5.DocumentObjectType != DocumentObjectType.TextRange)
						{
							num = 103;
							continue;
						}
						num = 178;
						continue;
					case 189:
					{
						float num5;
						if (num5 <= num10)
						{
							num = 177;
							continue;
						}
						goto IL_15B3;
					}
					case 190:
						num = 109;
						continue;
					case 191:
						goto IL_18E3;
					case 192:
						num = 4;
						continue;
					case 193:
						goto IL_1132;
					case 194:
						num = 121;
						continue;
					case 195:
					{
						spr\u1AB8 spr_u1AB;
						if (spr_u1AB is spr\u208E)
						{
							num = 40;
							continue;
						}
						goto IL_1623;
					}
					case 196:
						goto IL_F99;
					case 197:
						num14 = 0f;
						goto IL_1BF3;
					case 198:
						if (num15 >= num4)
						{
							num = 125;
							continue;
						}
						this.ᜄ().\u171B().ᜊ().RemoveAt(this.ᜄ().\u171B().ᜊ().Count - 1);
						num15++;
						num = 176;
						continue;
					case 199:
						num = 48;
						continue;
					case 200:
						num = 151;
						continue;
					case 201:
						if (!textRange4.Text.Contains(ClipboardData.b("卲", a_)))
						{
							num = 74;
							continue;
						}
						goto IL_1971;
					case 202:
						if ((textRange.NextSibling as TextRange).DocumentObjectType != DocumentObjectType.TextRange)
						{
							num = 41;
							continue;
						}
						num = 119;
						continue;
					case 203:
						this.ᜄ().ᜀ(this.ᜄ().\u170D() - num4);
						rectangleF = this.ᜄ().\u171B().ᜊ()[this.ᜄ().\u171B().ᜊ().Count - num4].ᜁ();
						num = 122;
						continue;
					case 204:
					{
						string[] array;
						if (array.Length >= 2)
						{
							num = 7;
							continue;
						}
						textRange3.Text = textRange2.Text;
						num = 14;
						continue;
					}
					case 205:
						num = 131;
						continue;
					case 206:
					{
						TextRange textRange9;
						if (!textRange9.Text.EndsWith(ClipboardData.b("卲", a_)))
						{
							num = 55;
							continue;
						}
						goto IL_18B5;
					}
					}
					break;
					IL_391:
					num = 34;
					continue;
					IL_42E:
					num = 198;
					continue;
					IL_4F4:
					num = 160;
					continue;
					IL_5FF:
					num = 77;
					continue;
					IL_81F:
					array2[0] = new spr\u208E(textRange8, string.Empty);
					array2[1] = new spr\u208E(textRange5, text2.TrimStart(new char[0]));
					flag = true;
					num = 89;
					continue;
					IL_98E:
					spr\u2297 spr_u3 = textRange3;
					sizeF = spr_u3.ᜀ(base.\u171E());
					flag2 = false;
					num10 = 0f;
					num = 145;
					continue;
					IL_A81:
					num = 195;
					continue;
					IL_AAA:
					num = 124;
					continue;
					IL_ACE:
					num7 = 0;
					num = 175;
					continue;
					IL_B58:
					float num16 = num2;
					if (true)
					{
					}
					num = 101;
					continue;
					IL_B95:
					num = 163;
					continue;
					IL_CE7:
					text2 = textRange5.Text;
					spr_u1AB2 = this.ᜄ().\u171B().ᜊ()[this.ᜄ().\u171B().ᜊ().Count - num4].ᜂ();
					num = 45;
					continue;
					IL_D48:
					num15 = 0;
					num = 29;
					continue;
					IL_E21:
					num = 20;
					continue;
					IL_1132:
					num = 136;
					continue;
					IL_1157:
					array2[0] = new spr\u208E(textRange, textRange.Text.Substring(0, num11));
					array2[1] = new spr\u208E(textRange, textRange.Text.Substring(num11).TrimStart(new char[0]));
					num = 126;
					continue;
					IL_146D:
					num = 173;
					continue;
					IL_154D:
					num = 148;
					continue;
					IL_15B3:
					num = 180;
					continue;
					IL_15FA:
					num = 118;
					continue;
					IL_1623:
					this.ᜄ().ᜀ(this.ᜄ().\u170D() - num6);
					rectangleF = this.ᜄ().\u171B().ᜊ()[this.ᜄ().\u171B().ᜊ().Count - num6].ᜁ();
					num = 79;
					continue;
					IL_16AC:
					num = 15;
					continue;
					IL_17CC:
					num = 59;
					continue;
					IL_18B5:
					num = 93;
					continue;
					IL_18E3:
					array2[0] = new spr\u208E(textRange6, string.Empty);
					array2[1] = new spr\u208E(textRange4, text.TrimStart(new char[0]));
					flag = true;
					num = 134;
					continue;
					IL_191E:
					textRange2 = textRange7;
					num = 116;
					continue;
					IL_1971:
					num = 117;
					continue;
					IL_19DE:
					num = 144;
					continue;
					IL_1AB6:
					num = 81;
					continue;
					IL_1ADC:
					num = 32;
					continue;
					IL_1B59:
					num12--;
					num = 57;
					continue;
					IL_1BF3:
					float num17 = num14;
					num10 = tableCell.Width - num16 - num17;
					num = 49;
					continue;
					IL_1C15:
					num = 161;
				}
			}
			IL_D5C:
			return this.ᜃ;
			IL_F99:
			goto IL_D5C;
			IL_1C39:
			return null;
		}
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x0007423C File Offset: 0x0007323C
	private new void ᜀ(ref RectangleF A_0, ref SizeF A_1)
	{
		switch (0)
		{
		default:
		{
			float num4;
			sprḈ sprḈ;
			for (;;)
			{
				float num = A_0.X;
				float num2 = A_0.X - ((this.ᜆ as spr\u1DA4).ᜊ().ᜂ() - (this.ᜆ as spr\u1DA4).ᜄ());
				int num3 = 41;
				for (;;)
				{
					float num5;
					bool flag;
					spr\u2297 spr_u;
					float num6;
					switch (num3)
					{
					case 0:
						num3 = 25;
						continue;
					case 1:
						if ((double)(num4 + num) > (double)num5 + sprḈ.ᜀ())
						{
							num3 = 27;
							continue;
						}
						goto IL_2DC;
					case 2:
						goto IL_333;
					case 3:
						goto IL_383;
					case 4:
						if (flag)
						{
							num3 = 16;
							continue;
						}
						goto IL_661;
					case 5:
						num3 = 11;
						continue;
					case 6:
						num3 = 15;
						continue;
					case 7:
						num4 = num5 - (num - (float)sprḈ.ᜀ());
						num3 = 47;
						continue;
					case 8:
						num3 = 43;
						continue;
					case 9:
						if (!this.ᜃ())
						{
							num3 = 33;
							continue;
						}
						goto IL_8B1;
					case 10:
						num = A_0.Right;
						num3 = 23;
						continue;
					case 11:
						if (sprḈ.ᜅ() != Spire.Layouting.TabJustification.Right)
						{
							num3 = 60;
							continue;
						}
						goto IL_825;
					case 12:
						if ((double)num > (double)num5 + sprḈ.ᜀ())
						{
							num3 = 54;
							continue;
						}
						goto IL_88D;
					case 13:
						goto IL_825;
					case 14:
						if ((double)num < (double)num5 + sprḈ.ᜀ())
						{
							num3 = 38;
							continue;
						}
						goto IL_2DC;
					case 15:
						if (num5 != 0f)
						{
							num3 = 39;
							continue;
						}
						goto IL_862;
					case 16:
						num3 = 44;
						continue;
					case 17:
						goto IL_5F1;
					case 18:
						goto IL_5F1;
					case 19:
						goto IL_88D;
					case 20:
						if ((spr_u as TextRange).OwnerParagraph.Format.FirstLineIndent >= 0f)
						{
							num3 = 8;
							continue;
						}
						num3 = 57;
						continue;
					case 21:
						if (sprḈ.ᜃ.ᜂ() != 0f)
						{
							num3 = 36;
							continue;
						}
						goto IL_333;
					case 22:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5A5;
						default:
							if (false)
							{
							}
							num = (this.ᜆ as spr\u1DA4).ᜊ().ᜂ();
							A_0.Width -= num - A_0.X;
							A_0.X = num;
							base.ᜃ(A_0);
							num3 = 18;
							continue;
						}
						break;
					case 23:
						goto IL_6B0;
					case 24:
						if (sprḈ.ᜃ.ᜁ() == Spire.Layouting.TabJustification.Left)
						{
							num3 = 51;
							continue;
						}
						goto IL_4B7;
					case 25:
						if (num2 / 2f < (this.ᜆ as spr\u1DA4).ᜄ())
						{
							num3 = 53;
							continue;
						}
						goto IL_255;
					case 26:
						if (flag)
						{
							num3 = 48;
							continue;
						}
						goto IL_333;
					case 27:
						num3 = 55;
						continue;
					case 28:
						if (num4 > this.ᜅ.ᜇ().Width)
						{
							num3 = 56;
							continue;
						}
						goto IL_333;
					case 29:
						if (sprḈ.ᜅ() == Spire.Layouting.TabJustification.Decimal)
						{
							num3 = 13;
							continue;
						}
						goto IL_8B1;
					case 30:
						if ((this.ᜆ as spr\u1DA4).ᜊ().ᜁ() == Spire.Layouting.TabJustification.Right)
						{
							num3 = 37;
							continue;
						}
						goto IL_1EB;
					case 31:
						if ((double)(num + num4) < (double)num5 + sprḈ.ᜀ())
						{
							num3 = 45;
							continue;
						}
						goto IL_862;
					case 32:
						if (!(spr_u as DocumentObject).Document.DOP.ᜁ().ᜉ())
						{
							num3 = 52;
							continue;
						}
						goto IL_862;
					case 33:
						A_1.Width = 0f;
						num3 = 3;
						continue;
					case 34:
						if (A_0.X < (this.ᜆ as spr\u1DA4).ᜊ().ᜂ())
						{
							num3 = 22;
							continue;
						}
						goto IL_1EB;
					case 35:
						if (sprḈ.ᜁ.Count == 0)
						{
							num3 = 6;
							continue;
						}
						goto IL_862;
					case 36:
						goto IL_4B7;
					case 37:
						num3 = 34;
						continue;
					case 38:
						num3 = 1;
						continue;
					case 39:
						num3 = 31;
						continue;
					case 40:
						goto IL_5A5;
					case 41:
						if ((this.ᜆ as spr\u1DA4).ᜊ().ᜁ() == Spire.Layouting.TabJustification.Centered)
						{
							num3 = 0;
							continue;
						}
						goto IL_255;
					case 42:
						if ((spr_u as TextRange).CharacterFormat.CharacterSpacing != 0f)
						{
							num3 = 46;
							continue;
						}
						goto IL_88D;
					case 43:
						num6 = 0f;
						goto IL_516;
					case 44:
						if (spr_u is TextRange)
						{
							num3 = 59;
							continue;
						}
						goto IL_661;
					case 45:
						num4 = num5 - (num - (float)sprḈ.ᜀ());
						num3 = 40;
						continue;
					case 46:
						num4 += (spr_u as TextRange).CharacterFormat.CharacterSpacing;
						num3 = 19;
						continue;
					case 47:
						goto IL_2DC;
					case 48:
						num3 = 28;
						continue;
					case 49:
						if ((this.ᜆ as spr\u1DA4).ᜊ().ᜁ() == Spire.Layouting.TabJustification.Decimal)
						{
							num3 = 63;
							continue;
						}
						goto IL_5F1;
					case 50:
						goto IL_661;
					case 51:
						num3 = 21;
						continue;
					case 52:
						num3 = 35;
						continue;
					case 53:
						num = (this.ᜆ as spr\u1DA4).ᜊ().ᜂ() + num2 / 2f;
						num3 = 58;
						continue;
					case 54:
						num3 = 42;
						continue;
					case 55:
						if ((spr_u as TextRange).OwnerParagraph.Format.FirstLineIndent != 0f)
						{
							num3 = 7;
							continue;
						}
						goto IL_2DC;
					case 56:
						num3 = 24;
						continue;
					case 57:
						num6 = (spr_u as TextRange).OwnerParagraph.Format.LeftIndent;
						goto IL_516;
					case 58:
						if (A_0.Right < (this.ᜆ as spr\u1DA4).ᜊ().ᜂ() + num2 / 2f)
						{
							num3 = 10;
							continue;
						}
						goto IL_6B0;
					case 59:
					{
						TableCell tableCell = (spr_u as TextRange).OwnerParagraph.Owner as TableCell;
						sprḈ.ᜀ((double)(tableCell.ᜀ as spr\u2032).\u170D());
						num3 = 50;
						continue;
					}
					case 60:
						num3 = 29;
						continue;
					case 61:
						if (sprḈ.ᜅ() != Spire.Layouting.TabJustification.Centered)
						{
							num3 = 5;
							continue;
						}
						goto IL_825;
					case 62:
						goto IL_5F1;
					case 63:
						this.ᜀ(ref A_0, ref num, num2);
						num3 = 62;
						continue;
					}
					break;
					IL_1EB:
					num3 = 49;
					continue;
					IL_255:
					num3 = 30;
					continue;
					IL_2DC:
					num3 = 32;
					continue;
					IL_333:
					A_1.Width = num4;
					if (true)
					{
					}
					num3 = 61;
					continue;
					IL_4B7:
					num4 = this.ᜅ.ᜇ().Width;
					num3 = 2;
					continue;
					IL_516:
					num5 = num6;
					num4 = (float)sprḈ.ᜀ((double)num, (spr_u as TextRange).OwnerParagraph);
					num3 = 14;
					continue;
					IL_5F1:
					spr_u = this.ᜅ();
					sprḈ = (spr_u.ᜀ() as sprḈ);
					num5 = 0f;
					flag = this.ᜆ();
					num3 = 4;
					continue;
					IL_661:
					num3 = 20;
					continue;
					IL_6B0:
					A_0.Width -= num - A_0.X;
					A_0.X = num;
					base.ᜃ(A_0);
					num3 = 17;
					continue;
					IL_825:
					num3 = 9;
					continue;
					IL_862:
					num3 = 12;
					continue;
					IL_5A5:
					goto IL_862;
					IL_88D:
					num3 = 26;
				}
			}
			IL_383:
			IL_8B1:
			(this.ᜆ as spr\u1DA4).ᜀ(num4);
			sprḈ.ᜀ(num4);
			(this.ᜆ as spr\u1DA4).ᜀ(sprḈ.ᜃ);
			return;
		}
		}
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x00074B2C File Offset: 0x00073B2C
	private new bool ᜀ(string A_0)
	{
		int a_ = 13;
		for (;;)
		{
			Regex regex = new Regex(ClipboardData.b("⡲⥴ɶ䵸Ṻ䵼佾검\udf82뺆뢌튎몐", a_));
			int num = 2;
			for (;;)
			{
				Regex regex3;
				switch (num)
				{
				case 0:
					goto IL_90;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7C;
					default:
						goto IL_6C;
					}
					break;
				case 2:
				{
					if (regex.IsMatch(A_0))
					{
						num = 1;
						continue;
					}
					Regex regex2 = new Regex(ClipboardData.b("⡲⥴ɶ䩸䭺䥼佾검\udf82뒆릈늊튎몐", a_));
					regex3 = new Regex(ClipboardData.b("⡲⥴ɶ䩸䭺ᱼ佾검\udf82뒆릈튎몐", a_));
					num = 3;
					continue;
				}
				case 3:
				{
					Regex regex2;
					if (!regex2.IsMatch(A_0))
					{
						num = 5;
						continue;
					}
					return true;
				}
				case 4:
					goto IL_7C;
				case 5:
					num = 4;
					continue;
				}
				break;
				IL_7C:
				if (!regex3.IsMatch(A_0))
				{
					return false;
				}
				num = 0;
			}
		}
		IL_6C:
		if (false)
		{
		}
		return true;
		IL_90:
		if (true)
		{
		}
		return true;
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x00074C2C File Offset: 0x00073C2C
	private new void ᜀ(ref RectangleF A_0, ref float A_1, float A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				Paragraph ownerParagraph = (this.ᜅ() as TextRange).OwnerParagraph;
				int num = ownerParagraph.ChildObjects.IndexOf(this.ᜅ() as TextRange);
				int a_ = 0;
				int num2 = num - 1;
				int num3 = 0;
				for (;;)
				{
					float num4;
					switch (num3)
					{
					case 0:
						goto IL_8A;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num3 = 11;
							continue;
						}
						break;
					case 2:
						if (num2 < 0)
						{
							if (true)
							{
							}
							num3 = 5;
							continue;
						}
						num3 = 7;
						continue;
					case 3:
						return;
					case 4:
						if (num4 < (this.ᜆ as spr\u1DA4).ᜄ())
						{
							num3 = 6;
							continue;
						}
						return;
					case 5:
						goto IL_1AB;
					case 6:
						A_2 -= num4;
						A_1 = (this.ᜆ as spr\u1DA4).ᜊ().ᜂ() + A_2;
						A_0.Width -= A_1 - A_0.X;
						A_0.X = A_1;
						base.ᜃ(A_0);
						num3 = 3;
						continue;
					case 7:
						if (ownerParagraph.ChildObjects[num2] is TextRange)
						{
							num3 = 1;
							continue;
						}
						goto IL_B6;
					case 8:
						goto IL_8A;
					case 9:
						goto IL_1AB;
					case 10:
						a_ = num2;
						num3 = 9;
						continue;
					case 11:
						if ((ownerParagraph.ChildObjects[num2] as spr\u2297).ᜀ() is sprḈ)
						{
							num3 = 10;
							continue;
						}
						goto IL_B6;
					}
					break;
					IL_8A:
					num3 = 2;
					continue;
					IL_B6:
					num2--;
					num3 = 8;
					continue;
					IL_1AB:
					num4 = base.\u171E().ᜀ(ownerParagraph, a_, num);
					num3 = 4;
				}
			}
			return;
		}
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x00074E38 File Offset: 0x00073E38
	private new bool ᜃ()
	{
		switch (0)
		{
		default:
		{
			TextRange textRange;
			for (;;)
			{
				bool flag = false;
				Paragraph ownerParagraph = (this.ᜅ() as TextRange).OwnerParagraph;
				int num = 0;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (textRange != null)
						{
							num2 = 11;
							continue;
						}
						goto IL_158;
					case 1:
					{
						ParagraphBase paragraphBase;
						if (paragraphBase == this.ᜅ())
						{
							num2 = 12;
							continue;
						}
						goto IL_158;
					}
					case 2:
						goto IL_DF;
					case 3:
						goto IL_188;
					case 4:
						if (flag)
						{
							num2 = 8;
							continue;
						}
						num2 = 1;
						continue;
					case 5:
						return false;
					case 6:
						goto IL_DF;
					case 7:
						goto IL_158;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_188;
						default:
							if (false)
							{
							}
							textRange = (ownerParagraph.Items[num] as TextRange);
							num2 = 10;
							continue;
						}
						break;
					case 9:
					{
						if (num >= ownerParagraph.Items.Count - 1)
						{
							num2 = 5;
							continue;
						}
						ParagraphBase paragraphBase = ownerParagraph.Items[num];
						num2 = 4;
						continue;
					}
					case 10:
						if (textRange != null)
						{
							num2 = 3;
							continue;
						}
						goto IL_158;
					case 11:
						goto IL_1A4;
					case 12:
						flag = true;
						num2 = 7;
						continue;
					}
					break;
					IL_DF:
					num2 = 9;
					continue;
					IL_158:
					num++;
					if (true)
					{
					}
					num2 = 6;
					continue;
					IL_188:
					num2 = 0;
				}
			}
			return false;
			IL_1A4:
			return ((spr\u1AB8)textRange).ᜀ() is sprḈ;
		}
		}
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x00074FEC File Offset: 0x00073FEC
	internal new bool ᜅ(SizeF A_0)
	{
		bool result;
		for (;;)
		{
			if (true)
			{
			}
			result = false;
			bool flag = this.ᜄ(A_0);
			int num = 7;
			for (;;)
			{
				bool flag2;
				bool flag3;
				switch (num)
				{
				case 0:
					num = 13;
					continue;
				case 1:
					goto IL_19C;
				case 2:
					flag2 = this.ᜅ.ᜀ(A_0);
					goto IL_152;
				case 3:
					flag2 = (this.ᜅ.ᜈ() != 0.0);
					goto IL_152;
				case 4:
					flag3 = this.ᜁ(A_0);
					goto IL_DE;
				case 5:
					num = 16;
					continue;
				case 6:
					num = 8;
					continue;
				case 7:
					if (this.ᜂ())
					{
						num = 9;
						continue;
					}
					num = 18;
					continue;
				case 8:
					if (!flag)
					{
						num = 15;
						continue;
					}
					goto IL_18F;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_17F;
					default:
						goto IL_1F7;
					}
					break;
				case 10:
					flag2 = false;
					goto IL_152;
				case 11:
					goto IL_18F;
				case 12:
					num = 3;
					continue;
				case 13:
					if (!(this.ᜅ() is spr\u208E))
					{
						num = 5;
						continue;
					}
					goto IL_1A1;
				case 14:
					if (!(this.ᜅ() is TextRange))
					{
						num = 0;
						continue;
					}
					goto IL_1A1;
				case 15:
					num = 14;
					continue;
				case 16:
					flag3 = false;
					goto IL_DE;
				case 17:
					if (this.ᜃ(A_0))
					{
						num = 12;
						continue;
					}
					num = 10;
					continue;
				case 18:
					if (!(this.ᜅ() is DocPicture))
					{
						num = 19;
						continue;
					}
					goto IL_1FF;
				case 19:
					num = 20;
					continue;
				case 20:
					if (!(this.ᜅ() is DocOleObject))
					{
						goto IL_17F;
					}
					goto IL_1FF;
				case 21:
					num = 2;
					continue;
				case 22:
					goto IL_14D;
				}
				break;
				IL_DE:
				if (flag3)
				{
					num = 11;
					continue;
				}
				result = true;
				num = 22;
				continue;
				IL_152:
				if (!flag2)
				{
					num = 6;
					continue;
				}
				goto IL_18F;
				IL_17F:
				num = 21;
				continue;
				IL_18F:
				result = false;
				num = 1;
				continue;
				IL_1A1:
				num = 4;
				continue;
				IL_1FF:
				num = 17;
			}
		}
		IL_14D:
		IL_19C:
		return result;
		IL_1F7:
		if (false)
		{
		}
		return true;
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x00075260 File Offset: 0x00074260
	private new bool ᜂ()
	{
		int a_ = 12;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!(this.ᜅ() as spr\u208E).ᜃ().Contains(ClipboardData.b("硱", a_)))
				{
					num = 3;
					continue;
				}
				return true;
			case 2:
				goto IL_18A;
			case 3:
				goto IL_D2;
			case 4:
				num = 6;
				continue;
			case 5:
				if (!(this.ᜅ() as TextRange).Text.Contains(ClipboardData.b("硱", a_)))
				{
					num = 9;
					continue;
				}
				goto IL_1B2;
			case 6:
				if ((this.ᜅ() as spr\u208E).ᜃ() != null)
				{
					num = 10;
					continue;
				}
				return false;
			case 7:
				if (!(this.ᜅ() as TextRange).Text.Contains(ClipboardData.b("罱", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_1B2;
			case 8:
				if (this.ᜅ() is spr\u208E)
				{
					num = 4;
					continue;
				}
				return false;
			case 9:
				num = 7;
				continue;
			case 10:
				num = 0;
				continue;
			case 11:
				num = 5;
				continue;
			}
			if (this.ᜅ() is TextRange)
			{
				num = 11;
				continue;
			}
			IL_18A:
			num = 8;
		}
		IL_D2:
		if (true)
		{
		}
		return (this.ᜅ() as spr\u208E).ᜃ().Contains(ClipboardData.b("罱", a_));
		IL_1B2:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_D2;
		default:
			if (false)
			{
			}
			return true;
		}
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x0007543C File Offset: 0x0007443C
	private new bool ᜄ(SizeF A_0)
	{
		switch (0)
		{
		default:
		{
			bool result;
			for (;;)
			{
				result = false;
				int num = 15;
				for (;;)
				{
					TextRange textRange;
					DocumentObject documentObject;
					DocumentObject documentObject2;
					float num2;
					TextRange textRange2;
					float num3;
					float num4;
					switch (num)
					{
					case 0:
						if (!(this.ᜅ() is TextRange))
						{
							num = 54;
							continue;
						}
						goto IL_B0D;
					case 1:
						return result;
					case 2:
						goto IL_546;
					case 3:
						if (base.\u171A().ᜈ())
						{
							num = 2;
							continue;
						}
						goto IL_730;
					case 4:
						textRange = ((this.ᜅ() as spr\u208E).ᜂ() as TextRange);
						goto IL_9C0;
					case 5:
						goto IL_76F;
					case 6:
						num = 10;
						continue;
					case 7:
						goto IL_4DC;
					case 8:
						num = 26;
						continue;
					case 9:
						if ((this.ᜅ() as spr\u208E).ᜃ() != null)
						{
							num = 56;
							continue;
						}
						goto IL_811;
					case 10:
						if (A_0.Width > this.ᜅ.ᜇ().Width)
						{
							num = 31;
							continue;
						}
						goto IL_546;
					case 11:
						if (documentObject is DocPicture)
						{
							num = 19;
							continue;
						}
						goto IL_A77;
					case 12:
						if (A_0.Width > this.ᜅ.ᜇ().Height)
						{
							num = 76;
							continue;
						}
						goto IL_76F;
					case 13:
						goto IL_B0D;
					case 14:
						if ((documentObject as DocPicture).TextWrappingStyle == TextWrappingStyle.Inline)
						{
							num = 78;
							continue;
						}
						goto IL_B0D;
					case 15:
						if (!(this.ᜅ() is DocPicture))
						{
							num = 87;
							continue;
						}
						num = 20;
						continue;
					case 16:
						num = 84;
						continue;
					case 17:
						result = true;
						num = 73;
						continue;
					case 18:
						num = 59;
						continue;
					case 19:
						num = 47;
						continue;
					case 20:
						documentObject2 = (this.ᜅ() as DocPicture);
						goto IL_8DA;
					case 21:
						if (A_0.Width <= num2)
						{
							num = 52;
							continue;
						}
						goto IL_B0D;
					case 22:
						if ((this.ᜅ() as Symbol).OwnerParagraph.IsInCell)
						{
							num = 75;
							continue;
						}
						return result;
					case 23:
						num = 28;
						continue;
					case 24:
						if (textRange2.Text.Length != 1)
						{
							num = 7;
							continue;
						}
						goto IL_265;
					case 25:
					{
						DocumentObject documentObject3;
						if (documentObject3 is Table)
						{
							num = 62;
							continue;
						}
						goto IL_B0D;
					}
					case 26:
						num3 = 0f;
						goto IL_B3A;
					case 27:
						if (A_0.Width > num4)
						{
							num = 41;
							continue;
						}
						goto IL_79C;
					case 28:
						if (this.ᜅ.ᜈ() != 0.0)
						{
							num = 71;
							continue;
						}
						goto IL_B0D;
					case 29:
						goto IL_631;
					case 30:
						num = 24;
						continue;
					case 31:
						num = 3;
						continue;
					case 32:
						if (this.ᜅ.ᜂ() != 0.0)
						{
							num = 23;
							continue;
						}
						goto IL_B0D;
					case 33:
						result = true;
						num = 89;
						continue;
					case 34:
						if ((this.ᜅ() as spr\u208E).ᜃ().Length == 1)
						{
							num = 88;
							continue;
						}
						goto IL_811;
					case 35:
						if (this.ᜅ() is Symbol)
						{
							num = 40;
							continue;
						}
						return result;
					case 36:
						num = 21;
						continue;
					case 37:
						num = 32;
						continue;
					case 38:
						if (this.ᜀ(textRange2))
						{
							num = 33;
							continue;
						}
						goto IL_811;
					case 39:
						num = 12;
						continue;
					case 40:
						num = 22;
						continue;
					case 41:
						result = true;
						num = 83;
						continue;
					case 42:
						documentObject2 = (this.ᜅ() as DocumentObject);
						goto IL_8DA;
					case 43:
						if (base.\u171A().ᜈ())
						{
							num = 74;
							continue;
						}
						goto IL_631;
					case 44:
						if (!(this.ᜅ() is spr\u208E))
						{
							num = 37;
							continue;
						}
						goto IL_B0D;
					case 45:
						if (A_0.Height > this.ᜅ.ᜇ().Width)
						{
							num = 85;
							continue;
						}
						goto IL_631;
					case 46:
						if (((spr\u1AB8)(documentObject as DocPicture).OwnerParagraph).ᜀ().ᜀ())
						{
							num = 49;
							continue;
						}
						goto IL_49C;
					case 47:
						if (!(documentObject as DocPicture).LayoutInCell)
						{
							num = 69;
							continue;
						}
						goto IL_5A0;
					case 48:
						num = 0;
						continue;
					case 49:
						num = 82;
						continue;
					case 50:
						if (!(this.ᜅ() is DocOleObject))
						{
							num = 94;
							continue;
						}
						num = 80;
						continue;
					case 51:
						if (textRange2 != null)
						{
							num = 30;
							continue;
						}
						goto IL_4DC;
					case 52:
						result = false;
						num = 13;
						continue;
					case 53:
						textRange = (this.ᜅ() as TextRange);
						goto IL_9C0;
					case 54:
						num = 44;
						continue;
					case 55:
						if (A_0.Height <= this.ᜅ.ᜇ().Height)
						{
							num = 6;
							continue;
						}
						goto IL_546;
					case 56:
						num = 34;
						continue;
					case 57:
						if (!(documentObject.Owner.Owner is TableCell))
						{
							num = 8;
							continue;
						}
						num = 90;
						continue;
					case 58:
						goto IL_49C;
					case 59:
						if (A_0.Width > this.ᜅ.ᜇ().Width)
						{
							num = 36;
							continue;
						}
						goto IL_B0D;
					case 60:
						if (textRange2.OwnerParagraph != null)
						{
							num = 16;
							continue;
						}
						goto IL_811;
					case 61:
						if (!(this.ᜅ() is spr\u208E))
						{
							num = 93;
							continue;
						}
						num = 4;
						continue;
					case 62:
						num = 57;
						continue;
					case 63:
						num4 = base.\u171E().ᜂ(textRange2);
						num = 27;
						continue;
					case 64:
						if (A_0.Width > num4)
						{
							num = 68;
							continue;
						}
						return result;
					case 65:
						if (base.\u171A().ᜀ())
						{
							num = 48;
							continue;
						}
						goto IL_B0D;
					case 66:
						textRange = null;
						goto IL_9C0;
					case 67:
						if (!(this.ᜅ() is TextRange))
						{
							num = 91;
							continue;
						}
						num = 53;
						continue;
					case 68:
						result = true;
						num = 1;
						continue;
					case 69:
						if (true)
						{
						}
						num = 96;
						continue;
					case 70:
						if (A_0.Height <= this.ᜅ.ᜇ().Height)
						{
							num = 18;
							continue;
						}
						goto IL_B0D;
					case 71:
						num = 11;
						continue;
					case 72:
						if (base.\u171A().ᜈ())
						{
							num = 77;
							continue;
						}
						goto IL_76F;
					case 73:
						goto IL_B0D;
					case 74:
						num = 45;
						continue;
					case 75:
						num4 = base.\u171E().ᜂ(this.ᜅ() as DocumentObject);
						num = 64;
						continue;
					case 76:
						goto IL_730;
					case 77:
						num = 97;
						continue;
					case 78:
					{
						DocumentObject documentObject3 = this.ᜀ(documentObject);
						num = 25;
						continue;
					}
					case 79:
						goto IL_5A0;
					case 80:
						documentObject2 = (this.ᜅ() as DocOleObject).OlePicture;
						goto IL_8DA;
					case 81:
						if (this.ᜅ() is spr\u208E)
						{
							num = 92;
							continue;
						}
						goto IL_811;
					case 82:
						if (base.\u171A().ᜈ())
						{
							num = 58;
							continue;
						}
						goto IL_58D;
					case 83:
						goto IL_79C;
					case 84:
						if (textRange2.OwnerParagraph.IsInCell)
						{
							num = 63;
							continue;
						}
						goto IL_79C;
					case 85:
						goto IL_58D;
					case 86:
						if (A_0.Height > this.ᜅ.ᜇ().Height)
						{
							num = 98;
							continue;
						}
						goto IL_49C;
					case 87:
						num = 50;
						continue;
					case 88:
						goto IL_265;
					case 89:
						goto IL_811;
					case 90:
						num3 = (documentObject.Owner.Owner as TableCell).Width;
						goto IL_B3A;
					case 91:
						num = 61;
						continue;
					case 92:
						IL_504:
						num = 9;
						continue;
					case 93:
						num = 66;
						continue;
					case 94:
						num = 42;
						continue;
					case 95:
						if (!(documentObject is DocPicture))
						{
							num = 17;
							continue;
						}
						goto IL_B0D;
					case 96:
						if ((documentObject as DocPicture).TextWrappingStyle == TextWrappingStyle.Inline)
						{
							num = 79;
							continue;
						}
						goto IL_A77;
					case 97:
						if (A_0.Height <= this.ᜅ.ᜇ().Width)
						{
							num = 39;
							continue;
						}
						goto IL_76F;
					case 98:
						num = 46;
						continue;
					}
					break;
					IL_265:
					num = 60;
					continue;
					IL_49C:
					num = 43;
					continue;
					IL_4DC:
					num = 81;
					continue;
					IL_5A0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_504;
					default:
						if (false)
						{
						}
						num = 86;
						continue;
					}
					IL_546:
					num = 72;
					continue;
					IL_58D:
					result = true;
					num = 29;
					continue;
					IL_631:
					num = 55;
					continue;
					IL_730:
					result = true;
					num = 5;
					continue;
					IL_76F:
					num = 14;
					continue;
					IL_79C:
					num = 38;
					continue;
					IL_811:
					num = 35;
					continue;
					IL_8DA:
					documentObject = documentObject2;
					num = 65;
					continue;
					IL_9C0:
					textRange2 = textRange;
					num4 = this.ᜅ.ᜇ().Width;
					num = 51;
					continue;
					IL_A77:
					num = 95;
					continue;
					IL_B0D:
					num = 67;
					continue;
					IL_B3A:
					num2 = num3;
					num = 70;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x00075FC0 File Offset: 0x00074FC0
	private new bool ᜀ(ParagraphBase A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				float num = 0f;
				float num2 = 0f;
				int num3 = 11;
				for (;;)
				{
					sprℐ sprℐ;
					float num4;
					switch (num3)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_283;
						default:
						{
							if (false)
							{
							}
							DocumentObject documentObject;
							if (documentObject is Section)
							{
								num3 = 4;
								continue;
							}
							num3 = 20;
							continue;
						}
						}
						break;
					case 1:
					{
						DocumentObject documentObject;
						if (documentObject is Table)
						{
							num3 = 17;
							continue;
						}
						goto IL_117;
					}
					case 2:
						num3 = 22;
						continue;
					case 3:
						goto IL_B6;
					case 4:
						goto IL_1D2;
					case 5:
						if (num > num2)
						{
							num3 = 15;
							continue;
						}
						return false;
					case 6:
						if (sprℐ.ᜣ())
						{
							if (true)
							{
							}
							num3 = 16;
							continue;
						}
						num = (float)sprℐ.ᜰ().ᜃ();
						num3 = 21;
						continue;
					case 7:
					{
						DocumentObject documentObject;
						if ((documentObject as Table).Rows[0].Cells[0].WidthType != FtsWidth.Percentage)
						{
							num3 = 23;
							continue;
						}
						num3 = 27;
						continue;
					}
					case 8:
						goto IL_2ED;
					case 9:
						goto IL_168;
					case 10:
						num4 = (A_0.OwnerParagraph.OwnerTextBody as TableCell).Width;
						goto IL_218;
					case 11:
						if (A_0 != null)
						{
							num3 = 2;
							continue;
						}
						goto IL_B6;
					case 12:
					{
						DocumentObject documentObject;
						if (documentObject.Owner != null)
						{
							num3 = 13;
							continue;
						}
						goto IL_1D2;
					}
					case 13:
					{
						DocumentObject documentObject = documentObject.Owner;
						num3 = 25;
						continue;
					}
					case 14:
						num3 = 12;
						continue;
					case 15:
						return true;
					case 16:
						num = (float)sprℐ.ᜰ().ᜃ() + sprℐ.\u171B();
						num3 = 3;
						continue;
					case 17:
						goto IL_283;
					case 18:
					{
						DocumentObject documentObject;
						if (documentObject is Section)
						{
							num3 = 26;
							continue;
						}
						goto IL_2ED;
					}
					case 19:
						goto IL_117;
					case 20:
					{
						DocumentObject documentObject;
						if (!(documentObject is Table))
						{
							num3 = 14;
							continue;
						}
						goto IL_1D2;
					}
					case 21:
						goto IL_B6;
					case 22:
						if (A_0.OwnerParagraph != null)
						{
							num3 = 24;
							continue;
						}
						goto IL_B6;
					case 23:
						num3 = 10;
						continue;
					case 24:
					{
						DocumentObject documentObject = A_0;
						num3 = 9;
						continue;
					}
					case 25:
						goto IL_168;
					case 26:
						num2 = (this.ᜆ as spr\u1DA4).ᜈ().Width;
						num3 = 8;
						continue;
					case 27:
						num4 = (A_0.OwnerParagraph.OwnerTextBody as TableCell).Width / 20f;
						goto IL_218;
					}
					break;
					IL_B6:
					num3 = 5;
					continue;
					IL_117:
					sprℐ = (((spr\u1AB8)A_0.OwnerParagraph).ᜀ() as sprℐ);
					num3 = 6;
					continue;
					IL_168:
					num3 = 0;
					continue;
					IL_1D2:
					num3 = 18;
					continue;
					IL_218:
					num2 = num4;
					num3 = 19;
					continue;
					IL_283:
					num3 = 7;
					continue;
					IL_2ED:
					num3 = 1;
				}
			}
			return true;
		}
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x00076334 File Offset: 0x00075334
	private new bool ᜃ(SizeF A_0)
	{
		switch (0)
		{
		default:
		{
			bool result;
			for (;;)
			{
				result = false;
				int num = 83;
				for (;;)
				{
					DocPicture docPicture;
					Paragraph paragraph;
					DocPicture docPicture2;
					float num3;
					float num4;
					float num5;
					float num6;
					DocumentObject documentObject;
					sprℐ sprℐ;
					float num7;
					switch (num)
					{
					case 0:
						paragraph = (docPicture.Owner.Owner.Owner as Paragraph);
						num = 18;
						continue;
					case 1:
					{
						float num2;
						if (num2 > this.ᜅ.ᜇ().Bottom)
						{
							num = 32;
							continue;
						}
						goto IL_929;
					}
					case 2:
						if (docPicture.Owner is spr\u1AD2)
						{
							num = 0;
							continue;
						}
						goto IL_93C;
					case 3:
						docPicture2 = (this.ᜅ() as DocOleObject).OlePicture;
						goto IL_8BB;
					case 4:
						goto IL_28D;
					case 5:
						goto IL_661;
					case 6:
					{
						RectangleF rectangleF;
						if (rectangleF.X + A_0.Width <= num3)
						{
							num = 63;
							continue;
						}
						goto IL_B05;
					}
					case 7:
						num = 50;
						continue;
					case 8:
						if (A_0.Height > this.ᜅ.ᜇ().Height)
						{
							num = 34;
							continue;
						}
						goto IL_38B;
					case 9:
					{
						RectangleF rectangleF2;
						if (rectangleF2.X + A_0.Width > num4)
						{
							num = 21;
							continue;
						}
						goto IL_B05;
					}
					case 10:
					{
						Paragraph paragraph2;
						if (paragraph2.Format != null)
						{
							num = 56;
							continue;
						}
						return result;
					}
					case 11:
						if (A_0.Width > this.ᜅ.ᜇ().Width)
						{
							num = 37;
							continue;
						}
						goto IL_9B6;
					case 12:
						return true;
					case 13:
						num = 11;
						continue;
					case 14:
						num5 = (docPicture.Owner.Owner as TableCell).Width;
						goto IL_A54;
					case 15:
						goto IL_38B;
					case 16:
						result = true;
						num = 66;
						continue;
					case 17:
						if (A_0.Height > num6)
						{
							num = 15;
							continue;
						}
						return result;
					case 18:
						goto IL_93C;
					case 19:
						return result;
					case 20:
						num = 45;
						continue;
					case 21:
						goto IL_9B6;
					case 22:
						num4 = (documentObject as Section).PageSetup.ClientWidth;
						num = 5;
						continue;
					case 23:
						docPicture2 = (this.ᜅ() as DocPicture);
						goto IL_8BB;
					case 24:
						num = 27;
						continue;
					case 25:
						num = 85;
						continue;
					case 26:
						if (documentObject is Table)
						{
							num = 46;
							continue;
						}
						num = 52;
						continue;
					case 27:
						if (docPicture.TextWrappingStyle != TextWrappingStyle.Inline)
						{
							num = 70;
							continue;
						}
						goto IL_86C;
					case 28:
						num4 = (this.ᜆ as spr\u1DA4).ᜈ().Width - (float)(sprℐ.ᜰ().ᜃ() + sprℐ.ᜰ().ᜂ() + (double)sprℐ.\u171B() + (double)sprℐ.ᜢ());
						num = 71;
						continue;
					case 29:
						num = 51;
						continue;
					case 30:
						goto IL_929;
					case 31:
						if (true)
						{
						}
						num = 79;
						continue;
					case 32:
						num = 54;
						continue;
					case 33:
					{
						Paragraph paragraph2 = documentObject as Paragraph;
						num = 10;
						continue;
					}
					case 34:
						num = 17;
						continue;
					case 35:
						if (docPicture.VerticalOrigin == VerticalOrigin.Paragraph)
						{
							num = 68;
							continue;
						}
						goto IL_86C;
					case 36:
						if (A_0.Width <= num3)
						{
							num = 7;
							continue;
						}
						goto IL_B05;
					case 37:
					{
						RectangleF rectangleF2 = this.ᜅ.ᜇ();
						num = 9;
						continue;
					}
					case 38:
						num = 57;
						continue;
					case 39:
					{
						RectangleF rectangleF3;
						if (rectangleF3.X + A_0.Width >= num4)
						{
							num = 55;
							continue;
						}
						goto IL_B05;
					}
					case 40:
					{
						RectangleF rectangleF3 = this.ᜅ.ᜇ();
						num = 39;
						continue;
					}
					case 41:
						if (docPicture.TextWrappingStyle == TextWrappingStyle.TopAndBottom)
						{
							num = 38;
							continue;
						}
						result = true;
						num = 47;
						continue;
					case 42:
						if (A_0.Width > this.ᜅ.ᜇ().Width)
						{
							num = 13;
							continue;
						}
						goto IL_B05;
					case 43:
						num = 23;
						continue;
					case 44:
						if (A_0.Width > this.ᜅ.ᜇ().Width)
						{
							num = 29;
							continue;
						}
						goto IL_250;
					case 45:
						if (!(docPicture.Owner.Owner is TableCell))
						{
							num = 25;
							continue;
						}
						num = 14;
						continue;
					case 46:
						num = 62;
						continue;
					case 47:
						return result;
					case 48:
						if (A_0.Width <= num4)
						{
							num = 40;
							continue;
						}
						goto IL_30A;
					case 49:
						if (sprℐ.ᜣ())
						{
							num = 28;
							continue;
						}
						num = 84;
						continue;
					case 50:
						if (A_0.Width > num4)
						{
							num = 72;
							continue;
						}
						goto IL_729;
					case 51:
						if (A_0.Width <= num7)
						{
							num = 67;
							continue;
						}
						goto IL_250;
					case 52:
						if (documentObject is Paragraph)
						{
							num = 33;
							continue;
						}
						return result;
					case 53:
						num6 = (this.ᜆ as spr\u1DA4).ᜈ().Height;
						num4 = (this.ᜆ as spr\u1DA4).ᜈ().Width;
						num3 = (documentObject as Section).PageSetup.PageSize.Width;
						num = 49;
						continue;
					case 54:
					{
						float num2;
						if (num2 <= (documentObject as Section).PageSetup.PageSize.Height)
						{
							num = 30;
							continue;
						}
						return result;
					}
					case 55:
						goto IL_30A;
					case 56:
						num = 76;
						continue;
					case 57:
						if (A_0.Height <= this.ᜅ.ᜇ().Height)
						{
							num = 80;
							continue;
						}
						return result;
					case 58:
						if (docPicture.TextWrappingStyle != TextWrappingStyle.Inline)
						{
							num = 24;
							continue;
						}
						goto IL_28D;
					case 59:
					{
						RectangleF rectangleF4;
						if (rectangleF4.X + A_0.Width < num3)
						{
							num = 64;
							continue;
						}
						return result;
					}
					case 60:
					{
						RectangleF rectangleF4 = this.ᜅ.ᜇ();
						num = 59;
						continue;
					}
					case 61:
						return result;
					case 62:
						if ((documentObject as Table).IsTextBox)
						{
							num = 20;
							continue;
						}
						goto IL_250;
					case 63:
						goto IL_729;
					case 64:
						goto IL_B05;
					case 65:
						if (documentObject is Section)
						{
							num = 53;
							continue;
						}
						num = 26;
						continue;
					case 66:
						goto IL_62C;
					case 67:
						return false;
					case 68:
						num = 77;
						continue;
					case 69:
						if (A_0.Height <= this.ᜅ.ᜇ().Height)
						{
							num = 74;
							continue;
						}
						goto IL_250;
					case 70:
						num = 35;
						continue;
					case 71:
						goto IL_661;
					case 72:
					{
						RectangleF rectangleF = this.ᜅ.ᜇ();
						num = 6;
						continue;
					}
					case 73:
						if (A_0.Height <= this.ᜅ.ᜇ().Height)
						{
							num = 16;
							continue;
						}
						return result;
					case 74:
						num = 44;
						continue;
					case 75:
					{
						float num2 = this.ᜅ.ᜇ().Y + docPicture.VerticalPosition + A_0.Height;
						num = 1;
						continue;
					}
					case 76:
					{
						Paragraph paragraph2;
						if (paragraph2.Format.IsFrame)
						{
							num = 12;
							continue;
						}
						return result;
					}
					case 77:
						if (!(documentObject is Table))
						{
							goto IL_AF4;
						}
						goto IL_86C;
					case 78:
						goto IL_661;
					case 79:
						if (docPicture.TextWrappingStyle != TextWrappingStyle.Inline)
						{
							num = 75;
							continue;
						}
						goto IL_52C;
					case 80:
						return true;
					case 81:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AF4;
						default:
							if (false)
							{
							}
							if (docPicture.VerticalOrigin == VerticalOrigin.Paragraph)
							{
								num = 31;
								continue;
							}
							goto IL_52C;
						}
						break;
					case 82:
						if (A_0.Width > num4)
						{
							num = 60;
							continue;
						}
						return result;
					case 83:
						if (!(this.ᜅ() is DocOleObject))
						{
							num = 43;
							continue;
						}
						num = 3;
						continue;
					case 84:
						if ((documentObject as Section).Columns.Count == 1)
						{
							num = 22;
							continue;
						}
						num4 = base.\u171E().ᜀ(documentObject as Section);
						num = 78;
						continue;
					case 85:
						num5 = 0f;
						goto IL_A54;
					}
					break;
					IL_250:
					num = 73;
					continue;
					IL_28D:
					num6 = 0f;
					num4 = 0f;
					num3 = 0f;
					paragraph = docPicture.OwnerParagraph;
					num = 2;
					continue;
					IL_30A:
					num = 82;
					continue;
					IL_38B:
					num = 42;
					continue;
					IL_52C:
					num = 8;
					continue;
					IL_661:
					num6 = (documentObject as Section).PageSetup.PageSize.Height - (documentObject as Section).PageSetup.Margins.Top - (documentObject as Section).PageSetup.Margins.Bottom;
					num = 81;
					continue;
					IL_729:
					num = 48;
					continue;
					IL_86C:
					num = 41;
					continue;
					IL_8BB:
					docPicture = docPicture2;
					documentObject = this.ᜀ(docPicture);
					num = 58;
					continue;
					IL_929:
					result = true;
					num = 61;
					continue;
					IL_93C:
					sprℐ = (((spr\u1AB8)paragraph).ᜀ() as sprℐ);
					sprℐ.ᜰ().ᜁ();
					sprℐ.ᜭ().ᜁ();
					num = 65;
					continue;
					IL_9B6:
					num = 36;
					continue;
					IL_A54:
					num7 = num5;
					num = 69;
					continue;
					IL_AF4:
					num = 4;
					continue;
					IL_B05:
					result = true;
					num = 19;
				}
			}
			return true;
			IL_62C:
			return result;
		}
		}
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x00076E98 File Offset: 0x00075E98
	private new DocumentObject ᜀ(DocumentObject A_0)
	{
		DocumentObject documentObject;
		for (;;)
		{
			if (true)
			{
			}
			documentObject = A_0;
			DocPicture docPicture = A_0 as DocPicture;
			int num = 18;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if ((documentObject as Paragraph).Format.IsFrame)
					{
						num = 2;
						continue;
					}
					goto IL_A4;
				case 1:
					if ((documentObject as Paragraph).Format != null)
					{
						num = 3;
						continue;
					}
					goto IL_A4;
				case 2:
					goto IL_20B;
				case 3:
					num = 0;
					continue;
				case 4:
					if (docPicture.TextWrappingStyle != TextWrappingStyle.Inline)
					{
						num = 19;
						continue;
					}
					return documentObject;
				case 5:
					goto IL_172;
				case 6:
					if (docPicture != null)
					{
						num = 8;
						continue;
					}
					goto IL_A4;
				case 7:
					documentObject = documentObject.Owner;
					num = 5;
					continue;
				case 8:
					num = 20;
					continue;
				case 9:
					return documentObject;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_20B;
					default:
						if (false)
						{
						}
						num = 16;
						continue;
					}
					break;
				case 11:
					num = 4;
					continue;
				case 12:
					if (documentObject.Owner != null)
					{
						num = 7;
						continue;
					}
					return documentObject;
				case 13:
					if (documentObject is Table)
					{
						num = 10;
						continue;
					}
					goto IL_14F;
				case 14:
					num = 1;
					continue;
				case 15:
					if (documentObject is Paragraph)
					{
						num = 14;
						continue;
					}
					goto IL_A4;
				case 16:
					if (!docPicture.LayoutInCell)
					{
						num = 11;
						continue;
					}
					return documentObject;
				case 17:
					if (documentObject is Section)
					{
						num = 9;
						continue;
					}
					num = 15;
					continue;
				case 18:
					goto IL_172;
				case 19:
					goto IL_14F;
				case 20:
					if (docPicture.TextWrappingStyle != TextWrappingStyle.Inline)
					{
						num = 21;
						continue;
					}
					return documentObject;
				case 21:
					goto IL_A4;
				}
				break;
				IL_A4:
				num = 13;
				continue;
				IL_14F:
				num = 12;
				continue;
				IL_172:
				num = 17;
				continue;
				IL_20B:
				num = 6;
			}
		}
		return documentObject;
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x000770D4 File Offset: 0x000760D4
	private new bool ᜂ(SizeF A_0)
	{
		for (;;)
		{
			TextRange textRange = null;
			int num = 19;
			for (;;)
			{
				Paragraph paragraph;
				Paragraph paragraph2;
				switch (num)
				{
				case 0:
					num = 17;
					continue;
				case 1:
					num = 7;
					continue;
				case 2:
					if ((paragraph.Owner as TableCell).OwnerRow != null)
					{
						num = 4;
						continue;
					}
					return false;
				case 3:
					if ((paragraph.Owner as TableCell).OwnerRow.OwnerTable.\u1712 != null)
					{
						num = 12;
						continue;
					}
					return false;
				case 4:
					num = 11;
					continue;
				case 5:
					goto IL_D8;
				case 6:
					goto IL_288;
				case 7:
					paragraph2 = textRange.OwnerEmptyParagraph;
					goto IL_248;
				case 8:
					if (true)
					{
					}
					if (paragraph != null)
					{
						num = 0;
						continue;
					}
					return false;
				case 9:
					textRange = ((this.ᜅ() as spr\u208E).ᜂ() as TextRange);
					num = 6;
					continue;
				case 10:
					num = 5;
					continue;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D8;
					default:
						if (false)
						{
						}
						if ((paragraph.Owner as TableCell).OwnerRow.OwnerTable != null)
						{
							num = 20;
							continue;
						}
						return false;
					}
					break;
				case 12:
					goto IL_137;
				case 13:
					if (textRange.OwnerParagraph == null)
					{
						num = 1;
						continue;
					}
					num = 15;
					continue;
				case 14:
					textRange = (this.ᜅ() as TextRange);
					num = 18;
					continue;
				case 15:
					paragraph2 = textRange.OwnerParagraph;
					goto IL_248;
				case 16:
					num = 2;
					continue;
				case 17:
					if (paragraph.Owner is TableCell)
					{
						num = 16;
						continue;
					}
					return false;
				case 18:
					goto IL_288;
				case 19:
					if (this.ᜅ() is TextRange)
					{
						num = 14;
						continue;
					}
					num = 21;
					continue;
				case 20:
					num = 3;
					continue;
				case 21:
					if (this.ᜅ() is spr\u208E)
					{
						num = 10;
						continue;
					}
					goto IL_288;
				}
				break;
				IL_D8:
				if ((this.ᜅ() as spr\u208E).ᜂ() is TextRange)
				{
					num = 9;
					continue;
				}
				goto IL_288;
				IL_248:
				paragraph = paragraph2;
				num = 8;
				continue;
				IL_288:
				num = 13;
			}
		}
		IL_137:
		return spr\u17C7.ᜀ((double)A_0.Width, (double)this.ᜅ.ᜇ().Width) <= 0;
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x00077390 File Offset: 0x00076390
	private new bool ᜁ(SizeF A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 20;
			SizeF a_;
			for (;;)
			{
				bool flag;
				switch (num)
				{
				case 0:
					if (flag)
					{
						return flag;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_293;
					default:
						if (false)
						{
						}
						num = 11;
						continue;
					}
					break;
				case 1:
				{
					TextRange textRange = (this.ᜅ() as spr\u208E).ᜂ() as TextRange;
					string text = (this.ᜅ() as spr\u208E).ᜃ();
					num = 15;
					continue;
				}
				case 2:
					if (this.ᜅ() is spr\u208E)
					{
						num = 23;
						continue;
					}
					goto IL_301;
				case 3:
					if (this.ᜅ() is TextRange)
					{
						num = 13;
						continue;
					}
					num = 2;
					continue;
				case 4:
				{
					TextRange textRange;
					string text2;
					a_ = base.\u171E().ᜀ(textRange, text2);
					num = 18;
					continue;
				}
				case 5:
					num = 14;
					continue;
				case 6:
					return true;
				case 7:
					goto IL_301;
				case 8:
				{
					string text;
					string text2;
					if (text != text2)
					{
						num = 4;
						continue;
					}
					goto IL_276;
				}
				case 9:
					if ((this.ᜅ() as spr\u208E).ᜂ() is TextRange)
					{
						num = 1;
						continue;
					}
					goto IL_301;
				case 10:
					goto IL_271;
				case 11:
				{
					TextRange textRange = null;
					string text = null;
					num = 3;
					continue;
				}
				case 12:
				{
					TextRange textRange;
					spr\u1AB8 ownerParagraph = textRange.OwnerParagraph;
					num = 17;
					continue;
				}
				case 13:
				{
					TextRange textRange = this.ᜅ() as TextRange;
					string text = textRange.Text;
					num = 7;
					continue;
				}
				case 14:
				{
					string text;
					if (text != null)
					{
						num = 21;
						continue;
					}
					return flag;
				}
				case 15:
					goto IL_301;
				case 16:
				{
					TextRange textRange;
					if (textRange != null)
					{
						num = 5;
						continue;
					}
					return flag;
				}
				case 17:
				{
					spr\u1AB8 ownerParagraph;
					if (ownerParagraph != null)
					{
						if (true)
						{
						}
						num = 19;
						continue;
					}
					goto IL_1C2;
				}
				case 18:
					goto IL_276;
				case 19:
				{
					spr\u1AB8 ownerParagraph;
					a_.Width = (float)((double)a_.Width - ownerParagraph.ᜀ().ᜊ().ᜂ() - ownerParagraph.ᜀ().ᜋ().ᜂ());
					num = 10;
					continue;
				}
				case 21:
				{
					string text;
					string text2 = text.TrimEnd(new char[0]);
					a_ = A_0;
					num = 8;
					continue;
				}
				case 22:
				{
					string text2;
					if (!this.ᜁ.IsMatch(text2))
					{
						goto IL_293;
					}
					goto IL_1C2;
				}
				case 23:
					num = 9;
					continue;
				}
				if (this.ᜂ(A_0))
				{
					num = 6;
					continue;
				}
				flag = this.ᜀ(A_0);
				num = 0;
				continue;
				IL_276:
				num = 22;
				continue;
				IL_293:
				num = 12;
				continue;
				IL_301:
				num = 16;
			}
			return true;
			IL_1C2:
			return this.ᜀ(a_);
			IL_271:
			goto IL_1C2;
		}
		}
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x000776C4 File Offset: 0x000766C4
	private new bool ᜁ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6A;
				default:
					if (false)
					{
					}
					if ((this.ᜆ as spr\u1DA4).ᜊ().ᜂ() < (((this.ᜅ() as TextRange).OwnerParagraph.OwnerTextBody as TableCell).ᜀ as spr\u2032).\u170D() + base.\u171E().ᜂ(this.ᜅ() as DocumentObject))
					{
						num = 3;
						continue;
					}
					goto IL_F5;
				}
				break;
			case 2:
				goto IL_6A;
			case 3:
				goto IL_F0;
			}
			if (this.ᜆ())
			{
				num = 2;
				continue;
			}
			break;
			IL_6A:
			num = 1;
		}
		IL_3C:
		return (this.ᜆ as spr\u1DA4).ᜊ().ᜂ() >= this.ᜅ.ᜇ().Right;
		IL_F0:
		goto IL_3C;
		IL_F5:
		if (true)
		{
		}
		return true;
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x000777D0 File Offset: 0x000767D0
	private new bool ᜀ(SizeF A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				bool flag = false;
				int num = 13;
				for (;;)
				{
					TextRange textRange;
					switch (num)
					{
					case 0:
						textRange = (this.ᜅ() as TextRange);
						num = 17;
						continue;
					case 1:
						num = 16;
						continue;
					case 2:
						goto IL_23E;
					case 3:
						goto IL_3BF;
					case 4:
						num = 32;
						continue;
					case 5:
						if (!flag)
						{
							num = 23;
							continue;
						}
						return flag;
					case 6:
						goto IL_100;
					case 7:
						num = 19;
						continue;
					case 8:
						goto IL_123;
					case 9:
						if (this.ᜅ() is TextRange)
						{
							num = 0;
							continue;
						}
						num = 29;
						continue;
					case 10:
						if (spr\u17C7.ᜀ((double)(this.ᜆ as spr\u1DA4).ᜊ().ᜂ(), (double)this.ᜅ.ᜇ().Right) > 0)
						{
							num = 8;
							continue;
						}
						goto IL_23E;
					case 11:
						if (spr\u17C7.ᜀ((double)A_0.Height, this.ᜅ.ᜂ()) > 0)
						{
							num = 26;
							continue;
						}
						goto IL_123;
					case 12:
						goto IL_2BA;
					case 13:
						if (base.\u171A().ᜀ())
						{
							num = 20;
							continue;
						}
						goto IL_164;
					case 14:
					{
						Paragraph ownerParagraph;
						if (ownerParagraph.Format.FrameHeight < 0)
						{
							num = 28;
							continue;
						}
						return flag;
					}
					case 15:
						num = 24;
						continue;
					case 16:
					{
						Paragraph ownerParagraph;
						if (ownerParagraph.Format.IsFrame)
						{
							num = 7;
							continue;
						}
						return flag;
					}
					case 17:
						goto IL_100;
					case 18:
						if (spr\u17C7.ᜀ((double)A_0.Width, (double)this.ᜅ.ᜇ().Width) <= 0)
						{
							num = 12;
							continue;
						}
						return false;
					case 19:
					{
						Paragraph ownerParagraph;
						if (ownerParagraph.Format.FrameHeight > 0)
						{
							num = 21;
							continue;
						}
						num = 14;
						continue;
					}
					case 20:
						num = 25;
						continue;
					case 21:
						goto IL_F3;
					case 22:
					{
						Paragraph ownerParagraph;
						if (ownerParagraph != null)
						{
							num = 4;
							continue;
						}
						return flag;
					}
					case 23:
						textRange = null;
						num = 9;
						continue;
					case 24:
						if ((this.ᜅ() as spr\u208E).ᜂ() is TextRange)
						{
							num = 3;
							continue;
						}
						goto IL_100;
					case 25:
						if (spr\u17C7.ᜀ((double)A_0.Width, this.ᜅ.ᜈ()) <= 0)
						{
							num = 31;
							continue;
						}
						goto IL_164;
					case 26:
						goto IL_164;
					case 27:
					{
						Paragraph ownerParagraph = textRange.OwnerParagraph;
						num = 22;
						continue;
					}
					case 28:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3BF;
						default:
							if (false)
							{
							}
							num = 18;
							continue;
						}
						break;
					case 29:
						if (this.ᜅ() is spr\u208E)
						{
							num = 15;
							continue;
						}
						goto IL_100;
					case 30:
						if (textRange != null)
						{
							num = 27;
							continue;
						}
						return flag;
					case 31:
						num = 11;
						continue;
					case 32:
					{
						Paragraph ownerParagraph;
						if (ownerParagraph.Format != null)
						{
							num = 1;
							continue;
						}
						return flag;
					}
					}
					break;
					IL_100:
					num = 30;
					continue;
					IL_123:
					flag = true;
					num = 2;
					continue;
					IL_164:
					num = 10;
					continue;
					IL_23E:
					num = 5;
					continue;
					IL_3BF:
					textRange = ((this.ᜅ() as spr\u208E).ᜂ() as TextRange);
					num = 6;
				}
			}
			IL_F3:
			if (true)
			{
			}
			return spr\u17C7.ᜀ((double)A_0.Width, (double)this.ᜅ.ᜇ().Width) <= 0;
			IL_2BA:
			return spr\u17C7.ᜀ((double)A_0.Height, (double)(-(double)this.ᜅ.ᜇ().Height)) <= 0;
		}
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x00077C38 File Offset: 0x00076C38
	internal new sprᦰ ᜀ(RectangleF A_0, SizeF A_1)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 13;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜁ())
					{
						num = 3;
						continue;
					}
					num = 25;
					continue;
				case 1:
					if (this.ᜆ())
					{
						num = 16;
						continue;
					}
					goto IL_142;
				case 2:
					if ((this.ᜅ() as TextRange).OwnerParagraph == null)
					{
						num = 23;
						continue;
					}
					goto IL_4BF;
				case 3:
					goto IL_2CB;
				case 4:
				{
					TextRange textRange;
					if (!(((spr\u1AB8)textRange).ᜀ() is sprḈ))
					{
						num = 6;
						continue;
					}
					goto IL_2D0;
				}
				case 5:
					num = 0;
					continue;
				case 6:
				{
					TextRange textRange;
					SizeF sizeF;
					sizeF += base.\u171E().ᜀ(textRange, textRange.Text);
					string text;
					text += textRange.Text;
					num = 12;
					continue;
				}
				case 7:
					if (true)
					{
					}
					if (this.ᜀ(A_1, A_0))
					{
						num = 27;
						continue;
					}
					goto IL_4BF;
				case 8:
					if (this.ᜅ() is TextRange)
					{
						num = 29;
						continue;
					}
					goto IL_4BF;
				case 9:
					num = 7;
					continue;
				case 10:
					goto IL_13D;
				case 11:
					num = 17;
					continue;
				case 12:
					goto IL_40C;
				case 14:
				{
					string text;
					if (!this.ᜁ(text))
					{
						num = 18;
						continue;
					}
					goto IL_4BF;
				}
				case 15:
				{
					SizeF sizeF;
					SizeF sizeF2 = A_1 + sizeF;
					num = 22;
					continue;
				}
				case 16:
					num = 2;
					continue;
				case 17:
				{
					TextRange textRange;
					if (((spr\u1AB8)textRange).ᜀ() is sprḈ)
					{
						num = 10;
						continue;
					}
					goto IL_40C;
				}
				case 18:
					num = 35;
					continue;
				case 19:
					num = 8;
					continue;
				case 20:
				{
					TextRange textRange;
					if (this.ᜀ(textRange))
					{
						num = 15;
						continue;
					}
					goto IL_2D0;
				}
				case 21:
					num = 32;
					continue;
				case 22:
				{
					SizeF sizeF2;
					if (sizeF2.Width >= A_0.Width)
					{
						num = 26;
						continue;
					}
					TextRange textRange = textRange.NextSibling as TextRange;
					num = 33;
					continue;
				}
				case 23:
					goto IL_142;
				case 24:
					if (this.ᜀ(this.ᜅ() as TextRange))
					{
						num = 5;
						continue;
					}
					goto IL_4BF;
				case 25:
					if (!this.ᜁ((this.ᜅ() as TextRange).Text))
					{
						num = 9;
						continue;
					}
					goto IL_4BF;
				case 26:
					goto IL_1A9;
				case 27:
				{
					TextRange textRange = (this.ᜅ() as TextRange).NextSibling as TextRange;
					string text = textRange.Text;
					SizeF sizeF = base.\u171E().ᜀ(textRange, textRange.Text);
					num = 30;
					continue;
				}
				case 28:
				{
					SizeF sizeF;
					float width = (A_1 + sizeF).Width;
					float width2 = A_0.Width;
					num = 31;
					continue;
				}
				case 29:
					num = 1;
					continue;
				case 30:
				{
					string text;
					if (!text.StartsWith(ClipboardData.b("塷", a_)))
					{
						num = 21;
						continue;
					}
					goto IL_4B4;
				}
				case 31:
					goto IL_31E;
				case 32:
					if (!(this.ᜅ() as TextRange).Text.EndsWith(ClipboardData.b("塷", a_)))
					{
						num = 11;
						continue;
					}
					goto IL_4B4;
				case 33:
				{
					TextRange textRange;
					if (!textRange.Text.Contains(ClipboardData.b("塷", a_)))
					{
						num = 34;
						continue;
					}
					goto IL_2D0;
				}
				case 34:
					num = 4;
					continue;
				case 35:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1A9;
					default:
						if (false)
						{
						}
						if (A_1.Width <= A_0.Width)
						{
							num = 28;
							continue;
						}
						goto IL_4BF;
					}
					break;
				}
				if (A_1.Width != 0f)
				{
					num = 19;
					continue;
				}
				goto IL_4BF;
				IL_142:
				num = 24;
				continue;
				IL_2D0:
				num = 14;
				continue;
				IL_1A9:
				goto IL_2D0;
				IL_40C:
				num = 20;
			}
			IL_13D:
			goto IL_4B4;
			IL_2CB:
			return null;
			IL_31E:
			goto IL_4BF;
			IL_4B4:
			return this.ᜃ = null;
			IL_4BF:
			return this.ᜃ = null;
		}
		}
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x00078110 File Offset: 0x00077110
	internal new bool ᜆ()
	{
		bool result;
		for (;;)
		{
			result = false;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					result = true;
					num = 6;
					continue;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_70;
					default:
						if (false)
						{
						}
						if (this.ᜅ() is TextRange)
						{
							num = 5;
							continue;
						}
						return result;
					}
					break;
				case 2:
					goto IL_70;
				case 3:
					if (true)
					{
					}
					if ((this.ᜅ() as TextRange).OwnerParagraph != null)
					{
						num = 2;
						continue;
					}
					return result;
				case 4:
					if ((this.ᜅ() as TextRange).OwnerParagraph.IsInCell)
					{
						num = 0;
						continue;
					}
					return result;
				case 5:
					num = 3;
					continue;
				case 6:
					return result;
				}
				break;
				IL_70:
				num = 4;
			}
		}
		return result;
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x000781F8 File Offset: 0x000771F8
	internal new bool ᜀ(TextRange A_0)
	{
		bool result;
		for (;;)
		{
			IL_24:
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_72:
				num = 4;
				break;
			case 1:
				goto IL_44;
			default:
				goto IL_44;
			}
			for (;;)
			{
				IL_02:
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					result = true;
					num = 1;
					continue;
				case 1:
					return result;
				case 2:
					if (A_0.NextSibling != null)
					{
						num = 3;
						continue;
					}
					return result;
				case 3:
					goto IL_64;
				case 4:
					if (A_0.NextSibling is TextRange)
					{
						num = 0;
						continue;
					}
					return result;
				}
				goto IL_24;
			}
			IL_64:
			goto IL_72;
			IL_44:
			if (false)
			{
			}
			result = false;
			num = 2;
			goto IL_02;
		}
		return result;
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x0007829C File Offset: 0x0007729C
	internal new void ᜀ(spr\u17BA A_0, SizeF A_1)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 19;
			spr\u17BA[] array2;
			for (;;)
			{
				int num3;
				spr\u17BA[] array3;
				switch (num)
				{
				case 0:
					goto IL_26E;
				case 1:
				{
					string[] array;
					if (array.Length == 1)
					{
						num = 15;
						continue;
					}
					goto IL_102;
				}
				case 2:
				{
					sprℐ sprℐ;
					if (sprℐ.ᜣ())
					{
						num = 12;
						continue;
					}
					float num2 = (this.ᜆ as spr\u1DA4).ᜈ().Width - (float)(sprℐ.ᜰ().ᜃ() + sprℐ.ᜰ().ᜂ() + (double)sprℐ.ᜢ());
					num = 0;
					continue;
				}
				case 3:
					goto IL_26E;
				case 4:
				{
					string[] array;
					if (num3 >= array.Length - 1)
					{
						num = 10;
						continue;
					}
					string text = text + array[num3] + ClipboardData.b("味", a_);
					num3++;
					num = 29;
					continue;
				}
				case 5:
					A_1.Width = this.ᜅ.ᜆ().Width;
					num = 13;
					continue;
				case 6:
				{
					array2 = null;
					float num2 = (this.ᜆ as spr\u1DA4).ᜈ().Width;
					num = 8;
					continue;
				}
				case 7:
					goto IL_444;
				case 8:
					if ((this.ᜅ() as TextRange).OwnerParagraph != null)
					{
						num = 17;
						continue;
					}
					goto IL_3E8;
				case 9:
					if (true)
					{
					}
					A_1 = array2[0].ᜀ(base.\u171E());
					num = 16;
					continue;
				case 10:
				{
					string text;
					spr\u1C7D a_2;
					array3[0] = new spr\u208E(a_2, text);
					string[] array;
					array3[1] = new spr\u208E(a_2, array[array.Length - 1].TrimStart(new char[0]));
					num = 24;
					continue;
				}
				case 11:
					return;
				case 12:
				{
					sprℐ sprℐ;
					float num2 = (this.ᜆ as spr\u1DA4).ᜈ().Width - (float)(sprℐ.ᜰ().ᜃ() + sprℐ.ᜰ().ᜂ() + (double)sprℐ.\u171B() + (double)sprℐ.ᜢ());
					num = 3;
					continue;
				}
				case 13:
					goto IL_2CC;
				case 14:
				{
					string[] array;
					spr\u1C7D a_2;
					array3[0] = new spr\u208E(a_2, array[0]);
					string text;
					array3[1] = new spr\u208E(a_2, text.TrimStart(new char[0]));
					num = 7;
					continue;
				}
				case 15:
					num = 23;
					continue;
				case 16:
					if (!this.ᜅ.ᜀ(A_1))
					{
						num = 5;
						continue;
					}
					goto IL_20D;
				case 17:
				{
					sprℐ sprℐ = ((spr\u1AB8)(this.ᜅ() as TextRange).OwnerParagraph).ᜀ() as sprℐ;
					num = 2;
					continue;
				}
				case 18:
					array2 = new spr\u17BA[]
					{
						A_0,
						A_0
					};
					num = 21;
					continue;
				case 20:
					num = 25;
					continue;
				case 21:
					goto IL_412;
				case 22:
					if (array2 != null)
					{
						num = 9;
						continue;
					}
					return;
				case 23:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_27A;
					default:
					{
						if (false)
						{
						}
						float num2;
						if (A_1.Width > num2)
						{
							num = 14;
							continue;
						}
						goto IL_102;
					}
					}
					break;
				case 24:
					goto IL_444;
				case 25:
					if (A_1.Height <= this.ᜅ.ᜆ().Height)
					{
						num = 6;
						continue;
					}
					goto IL_2D1;
				case 26:
					goto IL_27A;
				case 27:
					goto IL_1E3;
				case 28:
					goto IL_412;
				case 29:
					goto IL_1E3;
				case 30:
				{
					float num2 = base.\u171E().ᜂ(this.ᜅ() as TextRange);
					num = 31;
					continue;
				}
				case 31:
					goto IL_3E8;
				case 32:
				{
					if (base.\u171A() is sprḈ)
					{
						num = 18;
						continue;
					}
					spr\u1C7D a_2 = this.ᜅ() as spr\u1C7D;
					string text = "";
					string[] array = (this.ᜅ() as TextRange).Text.Split(new char[]
					{
						' '
					});
					array3 = new spr\u17BA[2];
					num = 1;
					continue;
				}
				}
				if (A_0 != null)
				{
					num = 20;
					continue;
				}
				goto IL_2D1;
				IL_102:
				num3 = 0;
				num = 27;
				continue;
				IL_1E3:
				num = 4;
				continue;
				IL_26E:
				num = 26;
				continue;
				IL_27A:
				if ((this.ᜅ() as TextRange).OwnerParagraph.IsInCell)
				{
					num = 30;
					continue;
				}
				goto IL_3E8;
				IL_2D1:
				this.ᜀ = LayoutState.NotFitted;
				this.ᜇ = (A_1.Height > this.ᜅ.ᜆ().Height);
				num = 11;
				continue;
				IL_3E8:
				num = 32;
				continue;
				IL_412:
				this.ᜀ = LayoutState.NotFitted;
				num = 22;
				continue;
				IL_444:
				array2 = array3;
				num = 28;
			}
			IL_20D:
			this.ᜀ(A_1, array2[0]);
			this.ᜁ = array2[1];
			this.ᜀ = LayoutState.Splitted;
			return;
			IL_2CC:
			goto IL_20D;
		}
		}
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x00078828 File Offset: 0x00077828
	internal new bool ᜁ(string A_0)
	{
		int num = 6;
		bool result;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return false;
			case 1:
			{
				char[] array;
				int num2;
				if (array[num2] > 'ÿ')
				{
					num = 3;
					continue;
				}
				num2++;
				num = 2;
				continue;
			}
			case 2:
				goto IL_76;
			case 3:
				result = true;
				num = 5;
				continue;
			case 4:
			{
				char[] array;
				int num2;
				if (num2 >= array.Length)
				{
					num = 8;
					continue;
				}
				char c = array[num2];
				num = 1;
				continue;
			}
			case 5:
				goto IL_DC;
			case 7:
				goto IL_76;
			case 8:
				goto IL_8F;
			}
			if (!string.IsNullOrEmpty(A_0))
			{
				char[] array = A_0.ToCharArray();
				result = false;
				int num2 = 0;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3C;
				}
				if (false)
				{
				}
				num = 7;
				continue;
			}
			IL_3C:
			num = 0;
			continue;
			IL_76:
			num = 4;
		}
		return false;
		IL_8F:
		IL_DC:
		if (true)
		{
		}
		return result;
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x0007891C File Offset: 0x0007791C
	internal new bool ᜀ(SizeF A_0, RectangleF A_1)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			bool result;
			for (;;)
			{
				result = this.ᜇ();
				int num = 12;
				for (;;)
				{
					TextRange textRange;
					SizeF sz;
					switch (num)
					{
					case 0:
						goto IL_2D9;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_379;
						default:
							if (false)
							{
							}
							goto IL_21F;
						}
						break;
					case 2:
						if ((this.ᜅ() as TextRange).PreviousSibling == null)
						{
							num = 0;
							continue;
						}
						return result;
					case 3:
						if (!((this.ᜅ() as TextRange).PreviousSibling as TextRange).Text.EndsWith(""))
						{
							num = 9;
							continue;
						}
						goto IL_2D9;
					case 4:
						goto IL_379;
					case 5:
						if (((this.ᜅ() as TextRange).NextSibling as TextRange).Text.EndsWith(""))
						{
							num = 19;
							continue;
						}
						return result;
					case 6:
					{
						int num2;
						if (num2 != -1)
						{
							num = 13;
							continue;
						}
						return result;
					}
					case 7:
						return result;
					case 8:
						if (textRange.CharacterFormat.FontSize == 0f)
						{
							num = 25;
							continue;
						}
						goto IL_21F;
					case 9:
						goto IL_16E;
					case 10:
					{
						SizeF sizeF;
						if (sizeF.Width > A_1.Width)
						{
							num = 16;
							continue;
						}
						goto IL_280;
					}
					case 11:
					{
						SizeF sizeF = A_0 + sz;
						num = 10;
						continue;
					}
					case 12:
						if ((this.ᜅ() as TextRange).PreviousSibling != null)
						{
							num = 23;
							continue;
						}
						goto IL_16E;
					case 13:
						textRange = ((this.ᜅ() as TextRange).NextSibling as TextRange);
						num = 8;
						continue;
					case 14:
						num = 5;
						continue;
					case 15:
						if (A_0.Width <= A_1.Width)
						{
							num = 11;
							continue;
						}
						goto IL_280;
					case 16:
						result = true;
						num = 20;
						continue;
					case 17:
						if ((this.ᜅ() as TextRange).Text.StartsWith(""))
						{
							num = 24;
							continue;
						}
						return result;
					case 18:
						if ((this.ᜅ() as TextRange).PreviousSibling is TextRange)
						{
							num = 26;
							continue;
						}
						goto IL_16E;
					case 19:
					{
						int num2 = ((this.ᜅ() as TextRange).NextSibling as TextRange).Text.IndexOf(ClipboardData.b("䭪", a_));
						num = 6;
						continue;
					}
					case 20:
						return result;
					case 21:
						if (((this.ᜅ() as TextRange).NextSibling as TextRange).Text.StartsWith(""))
						{
							num = 14;
							continue;
						}
						return result;
					case 22:
						num = 21;
						continue;
					case 23:
						if (true)
						{
						}
						num = 18;
						continue;
					case 24:
						num = 4;
						continue;
					case 25:
						textRange.CharacterFormat.FontSize = (this.ᜅ() as TextRange).CharacterFormat.FontSize;
						num = 1;
						continue;
					case 26:
						num = 3;
						continue;
					}
					break;
					IL_16E:
					num = 2;
					continue;
					IL_21F:
					sz = base.\u171E().ᜀ(textRange, textRange.Text.Substring(0, textRange.Text.IndexOf(ClipboardData.b("䭪", a_)) + 1));
					num = 15;
					continue;
					IL_280:
					result = false;
					num = 7;
					continue;
					IL_2D9:
					num = 17;
					continue;
					IL_379:
					if (!(this.ᜅ() as TextRange).Text.EndsWith(""))
					{
						return result;
					}
					num = 22;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x00078D58 File Offset: 0x00077D58
	internal new bool ᜇ()
	{
		int a_ = 6;
		bool result;
		for (;;)
		{
			result = true;
			int num = 23;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 10;
					continue;
				case 1:
					goto IL_241;
				case 2:
					if (((this.ᜅ() as TextRange).NextSibling as TextRange).Text.StartsWith(""))
					{
						num = 26;
						continue;
					}
					goto IL_241;
				case 3:
					num = 5;
					continue;
				case 4:
					if (((this.ᜅ() as TextRange).PreviousSibling as TextRange).Text.EndsWith(""))
					{
						num = 8;
						continue;
					}
					return result;
				case 5:
					if (!((this.ᜅ() as TextRange).NextSibling as TextRange).Text.StartsWith(ClipboardData.b("䁫", a_)))
					{
						num = 12;
						continue;
					}
					goto IL_241;
				case 6:
					num = 9;
					continue;
				case 7:
					num = 4;
					continue;
				case 8:
					num = 17;
					continue;
				case 9:
					if ((this.ᜅ() as TextRange).Text.EndsWith(""))
					{
						num = 19;
						continue;
					}
					return result;
				case 10:
					if (true)
					{
					}
					if (!((this.ᜅ() as TextRange).NextSibling as TextRange).Text.StartsWith(ClipboardData.b("䱫", a_)))
					{
						num = 18;
						continue;
					}
					goto IL_241;
				case 11:
					if (!((this.ᜅ() as TextRange).NextSibling as TextRange).Text.EndsWith(""))
					{
						num = 1;
						continue;
					}
					return result;
				case 12:
					num = 20;
					continue;
				case 13:
					if ((this.ᜅ() as TextRange).PreviousSibling is TextRange)
					{
						num = 7;
						continue;
					}
					return result;
				case 14:
					if (!(this.ᜅ() as TextRange).Text.EndsWith(ClipboardData.b("䉫", a_)))
					{
						num = 21;
						continue;
					}
					goto IL_241;
				case 15:
					num = 13;
					continue;
				case 16:
					num = 14;
					continue;
				case 17:
					if ((this.ᜅ() as TextRange).Text.StartsWith(""))
					{
						num = 6;
						continue;
					}
					return result;
				case 18:
					num = 25;
					continue;
				case 19:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_183;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 20:
					if ((this.ᜅ() as TextRange).PreviousSibling != null)
					{
						num = 15;
						continue;
					}
					return result;
				case 21:
					num = 24;
					continue;
				case 22:
					return result;
				case 23:
					if (!(this.ᜅ() as TextRange).Text.EndsWith(ClipboardData.b("䱫", a_)))
					{
						num = 16;
						continue;
					}
					goto IL_241;
				case 24:
					if (!(this.ᜅ() as TextRange).Text.EndsWith(ClipboardData.b("䁫", a_)))
					{
						num = 0;
						continue;
					}
					goto IL_241;
				case 25:
					if (!((this.ᜅ() as TextRange).NextSibling as TextRange).Text.StartsWith(ClipboardData.b("䉫", a_)))
					{
						num = 3;
						continue;
					}
					goto IL_241;
				case 26:
					goto IL_183;
				}
				break;
				IL_183:
				num = 11;
				continue;
				IL_241:
				result = false;
				num = 22;
			}
		}
		return result;
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x00079174 File Offset: 0x00078174
	internal new void ᜁ(spr\u2297 A_0, SizeF A_1, ref RectangleF A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				Paragraph paragraph = null;
				int num = 20;
				for (;;)
				{
					int num2;
					float num4;
					float num3;
					RectangleF rectangleF;
					RectangleF rectangleF2;
					switch (num)
					{
					case 0:
						if (A_0 is DocPicture)
						{
							num = 61;
							continue;
						}
						goto IL_F7E;
					case 1:
					{
						DocumentObject documentObject;
						if (documentObject == null)
						{
							num = 19;
							continue;
						}
						num = 115;
						continue;
					}
					case 2:
						if (A_2.Y + A_1.Height >= spr\u25E5.ᜄ[num2].Y)
						{
							num = 62;
							continue;
						}
						goto IL_48B;
					case 3:
						if (A_2.X < spr\u25E5.ᜄ[num2].Right)
						{
							num = 102;
							continue;
						}
						goto IL_B40;
					case 4:
					{
						DocumentObject documentObject;
						if (!(documentObject is Paragraph))
						{
							num = 83;
							continue;
						}
						goto IL_4A0;
					}
					case 5:
						A_2.Width -= spr\u25E5.ᜄ[num2].Right - A_2.X;
						A_2.X = spr\u25E5.ᜄ[num2].Right;
						base.ᜃ(A_2);
						num = 30;
						continue;
					case 6:
						if (num2 >= spr\u25E5.ᜄ.Count)
						{
							num = 23;
							continue;
						}
						num = 31;
						continue;
					case 7:
						if (A_2.X > spr\u25E5.ᜄ[num2].Right)
						{
							num = 52;
							continue;
						}
						goto IL_B04;
					case 8:
						num = 55;
						continue;
					case 9:
						num = 125;
						continue;
					case 10:
						if (A_2.X >= spr\u25E5.ᜄ[num2].X)
						{
							num = 21;
							continue;
						}
						goto IL_B40;
					case 11:
						goto IL_EAA;
					case 12:
						num3 = Math.Abs(num4);
						goto IL_F6B;
					case 13:
						if (A_2.Y + A_1.Height < spr\u25E5.ᜄ[num2].Bottom)
						{
							num = 46;
							continue;
						}
						goto IL_56B;
					case 14:
						goto IL_48B;
					case 15:
						if (A_2.X > spr\u25E5.ᜄ[num2].X)
						{
							num = 109;
							continue;
						}
						goto IL_B04;
					case 16:
						goto IL_91B;
					case 17:
						num = 49;
						continue;
					case 18:
						A_2.Y = spr\u25E5.ᜄ[num2].Bottom;
						A_2.Height -= spr\u25E5.ᜄ[num2].Height;
						base.ᜃ(A_2);
						num = 88;
						continue;
					case 19:
						goto IL_4A0;
					case 20:
						goto IL_23C;
					case 21:
						num = 3;
						continue;
					case 22:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_23C;
						default:
							if (false)
							{
							}
							num = 74;
							continue;
						}
						break;
					case 23:
						return;
					case 24:
						num = 80;
						continue;
					case 25:
						goto IL_11D5;
					case 26:
						num = 123;
						continue;
					case 27:
					{
						DocumentObject documentObject;
						if (documentObject != null)
						{
							num = 79;
							continue;
						}
						goto IL_62D;
					}
					case 28:
						if (!(this.ᜆ as spr\u1DA4).ᜁ())
						{
							num = 36;
							continue;
						}
						return;
					case 29:
						if (A_2.Y >= spr\u25E5.ᜄ[num2].Y)
						{
							num = 47;
							continue;
						}
						goto IL_91B;
					case 30:
						goto IL_48B;
					case 31:
						if (rectangleF.X <= spr\u25E5.ᜄ[num2].Right + 16f)
						{
							num = 22;
							continue;
						}
						goto IL_48B;
					case 32:
						if (A_2.Width < 16f)
						{
							num = 107;
							continue;
						}
						A_2.X = spr\u25E5.ᜄ[num2].Right;
						base.ᜃ(A_2);
						num = 105;
						continue;
					case 33:
						goto IL_28F;
					case 34:
						if (A_2.Y >= spr\u25E5.ᜄ[num2].Bottom)
						{
							num = 16;
							continue;
						}
						goto IL_E7C;
					case 35:
						if (A_0 is ParagraphBase)
						{
							num = 90;
							continue;
						}
						goto IL_28F;
					case 36:
						num = 122;
						continue;
					case 37:
						goto IL_E7C;
					case 38:
						if (A_2.Y + A_1.Height < spr\u25E5.ᜄ[num2].Bottom)
						{
							num = 37;
							continue;
						}
						goto IL_48B;
					case 39:
						return;
					case 40:
						if (A_2.Y + A_1.Height >= spr\u25E5.ᜄ[num2].Y)
						{
							num = 124;
							continue;
						}
						goto IL_56B;
					case 41:
						goto IL_48B;
					case 42:
						goto IL_F7E;
					case 43:
						if (paragraph.Format.FrameVerticalPos != 2)
						{
							num = 39;
							continue;
						}
						goto IL_8C6;
					case 44:
						num4 = (float)(((spr\u1AB8)(A_0 as ParagraphBase).OwnerParagraph).ᜀ() as sprℐ).ᜰ().ᜂ();
						num = 11;
						continue;
					case 45:
						if (paragraph != null)
						{
							num = 24;
							continue;
						}
						goto IL_8C6;
					case 46:
						goto IL_6F7;
					case 47:
						num = 34;
						continue;
					case 48:
						if (spr\u25E5.ᜄ.Count > 0)
						{
							num = 121;
							continue;
						}
						return;
					case 49:
						if (spr\u25E5.ᜅ[num2] != TextWrappingStyle.Behind)
						{
							num = 60;
							continue;
						}
						goto IL_56B;
					case 50:
						if (spr\u25E5.ᜅ[num2] == TextWrappingStyle.TopAndBottom)
						{
							num = 18;
							continue;
						}
						goto IL_48B;
					case 51:
						num = 43;
						continue;
					case 52:
						A_2.Width = this.ᜅ.ᜆ().Width;
						base.ᜃ(A_2);
						num = 68;
						continue;
					case 53:
						if (A_2.X + A_1.Width > spr\u25E5.ᜄ[num2].X)
						{
							num = 72;
							continue;
						}
						goto IL_62D;
					case 54:
						paragraph = ((A_0 as spr\u208E).ᜂ() as TextRange).OwnerParagraph;
						num = 112;
						continue;
					case 55:
						if (A_2.Y >= spr\u25E5.ᜄ[num2].Bottom)
						{
							num = 96;
							continue;
						}
						goto IL_6F7;
					case 56:
					{
						Paragraph paragraph2;
						num4 = (float)(((spr\u1AB8)paragraph2).ᜀ() as sprℐ).ᜰ().ᜂ();
						num = 86;
						continue;
					}
					case 57:
						if (paragraph.Format.IsFrame)
						{
							num = 116;
							continue;
						}
						goto IL_697;
					case 58:
						num = 53;
						continue;
					case 59:
						goto IL_48B;
					case 60:
					{
						DocumentObject documentObject = A_0 as DocumentObject;
						num = 93;
						continue;
					}
					case 61:
						paragraph = (A_0 as DocPicture).OwnerParagraph;
						num = 106;
						continue;
					case 62:
						num = 38;
						continue;
					case 63:
					{
						Paragraph paragraph2 = (A_0 as TextRange).CharacterFormat.BaseFormat.OwnerBase as Paragraph;
						num = 130;
						continue;
					}
					case 64:
						if (A_2.Y >= spr\u25E5.ᜄ[num2].Y)
						{
							num = 8;
							continue;
						}
						goto IL_724;
					case 65:
						if (A_2.X < spr\u25E5.ᜄ[num2].X)
						{
							num = 58;
							continue;
						}
						goto IL_62D;
					case 66:
						if (A_2.Width < 16f)
						{
							num = 71;
							continue;
						}
						A_2.X = spr\u25E5.ᜄ[num2].Right;
						base.ᜃ(A_2);
						num = 41;
						continue;
					case 67:
						goto IL_48B;
					case 68:
						goto IL_48B;
					case 69:
						num = 57;
						continue;
					case 70:
						if (spr\u25E5.ᜅ[num2] != TextWrappingStyle.Inline)
						{
							num = 114;
							continue;
						}
						goto IL_56B;
					case 71:
						A_2.Y = spr\u25E5.ᜄ[num2].Bottom;
						A_2.Width = this.ᜅ.ᜆ().Width;
						A_2.Height -= spr\u25E5.ᜄ[num2].Bottom - A_2.Y;
						base.ᜃ(A_2);
						num = 67;
						continue;
					case 72:
						A_2.Y = spr\u25E5.ᜄ[num2].Bottom;
						A_2.Height -= spr\u25E5.ᜄ[num2].Height;
						base.ᜃ(A_2);
						num = 76;
						continue;
					case 73:
						num = 132;
						continue;
					case 74:
						if (rectangleF.Right >= spr\u25E5.ᜄ[num2].X - 16f)
						{
							num = 73;
							continue;
						}
						goto IL_48B;
					case 75:
						paragraph = (A_0 as TextRange).OwnerParagraph;
						num = 42;
						continue;
					case 76:
						goto IL_48B;
					case 77:
						goto IL_11D5;
					case 78:
					{
						DocumentObject documentObject = documentObject.Owner;
						num = 99;
						continue;
					}
					case 79:
						num = 104;
						continue;
					case 80:
						if (paragraph.Format != null)
						{
							num = 51;
							continue;
						}
						goto IL_8C6;
					case 81:
						if (A_0 is spr\u208E)
						{
							num = 111;
							continue;
						}
						goto IL_FA1;
					case 82:
						if (spr\u25E5.ᜅ[num2] != TextWrappingStyle.InFrontOfText)
						{
							num = 17;
							continue;
						}
						goto IL_56B;
					case 83:
						num = 1;
						continue;
					case 84:
						if (rectangleF2.X > A_2.X)
						{
							num = 127;
							continue;
						}
						goto IL_6BB;
					case 85:
						num = 65;
						continue;
					case 86:
						goto IL_EAA;
					case 87:
						A_2.Width = this.ᜅ.ᜇ().Right - spr\u25E5.ᜄ[num2].Right - num4;
						num = 97;
						continue;
					case 88:
						goto IL_48B;
					case 89:
						if (num4 >= 0f)
						{
							num = 9;
							continue;
						}
						num = 12;
						continue;
					case 90:
						num = 101;
						continue;
					case 91:
						if ((A_0 as spr\u208E).ᜂ() is TextRange)
						{
							num = 54;
							continue;
						}
						goto IL_FA1;
					case 92:
						if (A_2.Width < 16f)
						{
							num = 87;
							continue;
						}
						base.ᜃ(A_2);
						num = 14;
						continue;
					case 93:
						goto IL_605;
					case 94:
						A_2.Width = spr\u25E5.ᜄ[num2].X - A_2.X - num4;
						num = 92;
						continue;
					case 95:
						num = 64;
						continue;
					case 96:
						goto IL_724;
					case 97:
						if (A_2.Width < 16f)
						{
							num = 100;
							continue;
						}
						goto IL_48B;
					case 98:
					{
						DocumentObject documentObject;
						if (documentObject != null)
						{
							num = 78;
							continue;
						}
						goto IL_4A0;
					}
					case 99:
						goto IL_605;
					case 100:
						A_2.Y = spr\u25E5.ᜄ[num2].Bottom;
						A_2.Height -= spr\u25E5.ᜄ[num2].Height;
						base.ᜃ(A_2);
						num = 59;
						continue;
					case 101:
						if ((A_0 as ParagraphBase).OwnerParagraph != null)
						{
							num = 44;
							continue;
						}
						num = 119;
						continue;
					case 102:
						A_2.Width = A_2.Width - (spr\u25E5.ᜄ[num2].Right - A_2.X) - num4;
						num = 32;
						continue;
					case 103:
						goto IL_697;
					case 104:
					{
						DocumentObject documentObject;
						if (documentObject is Paragraph)
						{
							num = 26;
							continue;
						}
						goto IL_62D;
					}
					case 105:
						goto IL_48B;
					case 106:
						goto IL_F7E;
					case 107:
						A_2.Width = this.ᜅ.ᜇ().Right - spr\u25E5.ᜄ[num2].Right - num4;
						num = 66;
						continue;
					case 108:
						num = 98;
						continue;
					case 109:
						num = 7;
						continue;
					case 110:
						if (A_2.X < spr\u25E5.ᜄ[num2].Right)
						{
							num = 5;
							continue;
						}
						goto IL_48B;
					case 111:
						num = 91;
						continue;
					case 112:
						goto IL_F7E;
					case 113:
						if (A_2.Right > spr\u25E5.ᜄ[num2].X)
						{
							num = 94;
							continue;
						}
						goto IL_6BB;
					case 114:
						num = 128;
						continue;
					case 115:
					{
						DocumentObject documentObject;
						if (!(documentObject is Paragraph))
						{
							num = 108;
							continue;
						}
						goto IL_4A0;
					}
					case 116:
						goto IL_4EC;
					case 117:
						if (spr\u25E5.ᜄ.Count > 0)
						{
							num = 129;
							continue;
						}
						goto IL_48B;
					case 118:
						num = 110;
						continue;
					case 119:
						if (A_0 is TextRange)
						{
							if (true)
							{
							}
							num = 63;
							continue;
						}
						goto IL_EAA;
					case 120:
						num = 82;
						continue;
					case 121:
						num = 28;
						continue;
					case 122:
						if (paragraph != null)
						{
							num = 69;
							continue;
						}
						goto IL_4EC;
					case 123:
					{
						DocumentObject documentObject;
						if ((documentObject as Paragraph).Format.HorizontalAlignment != Spire.Doc.Documents.HorizontalAlignment.Left)
						{
							num = 85;
							continue;
						}
						goto IL_62D;
					}
					case 124:
						num = 13;
						continue;
					case 125:
						num3 = 0f;
						goto IL_F6B;
					case 126:
						if (paragraph == null)
						{
							num = 103;
							continue;
						}
						return;
					case 127:
						num = 113;
						continue;
					case 128:
						if (spr\u25E5.ᜅ[num2] != TextWrappingStyle.TopAndBottom)
						{
							num = 120;
							continue;
						}
						goto IL_56B;
					case 129:
						num = 29;
						continue;
					case 130:
					{
						Paragraph paragraph2;
						if (paragraph2 != null)
						{
							num = 56;
							continue;
						}
						goto IL_EAA;
					}
					case 131:
						if (A_2.X > spr\u25E5.ᜄ[num2].X)
						{
							num = 118;
							continue;
						}
						goto IL_48B;
					case 132:
						if (spr\u25E5.ᜄ.Count > 0)
						{
							num = 95;
							continue;
						}
						goto IL_56B;
					}
					break;
					IL_23C:
					if (A_0 is TextRange)
					{
						num = 75;
						continue;
					}
					num = 81;
					continue;
					IL_28F:
					num = 27;
					continue;
					IL_48B:
					num2++;
					num = 25;
					continue;
					IL_4A0:
					num4 = 0f;
					num = 35;
					continue;
					IL_4EC:
					num = 126;
					continue;
					IL_56B:
					num = 117;
					continue;
					IL_605:
					num = 4;
					continue;
					IL_62D:
					num = 10;
					continue;
					IL_697:
					rectangleF = (this.ᜆ as spr\u1DA4).ᜈ();
					num2 = 0;
					num = 77;
					continue;
					IL_6BB:
					num = 15;
					continue;
					IL_6F7:
					num = 70;
					continue;
					IL_724:
					num = 40;
					continue;
					IL_8C6:
					num = 48;
					continue;
					IL_91B:
					num = 2;
					continue;
					IL_B04:
					num = 131;
					continue;
					IL_B40:
					rectangleF2 = spr\u25E5.ᜄ[num2];
					num = 84;
					continue;
					IL_E7C:
					num = 50;
					continue;
					IL_EAA:
					num = 89;
					continue;
					IL_F6B:
					num4 = num3;
					num = 33;
					continue;
					IL_F7E:
					num = 45;
					continue;
					IL_FA1:
					num = 0;
					continue;
					IL_11D5:
					num = 6;
				}
			}
			return;
		}
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x0007A380 File Offset: 0x00079380
	protected override void ᜈ()
	{
		for (;;)
		{
			spr\u1DBA spr_u1DBA = base.\u171A() as spr\u1DBA;
			int num = 8;
			for (;;)
			{
				bool flag;
				bool flag2;
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					flag = false;
					goto IL_D2;
				case 2:
					num = 6;
					continue;
				case 3:
					goto IL_B9;
				case 4:
					this.ᜆ.ᜀ(this.ᜃ);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B9;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				case 5:
					if (flag2)
					{
						num = 2;
						continue;
					}
					return;
				case 6:
					if (this.ᜃ != null)
					{
						num = 4;
						continue;
					}
					return;
				case 7:
					return;
				case 8:
					if (spr_u1DBA == null)
					{
						num = 0;
						continue;
					}
					num = 3;
					continue;
				}
				break;
				IL_D2:
				flag2 = flag;
				if (true)
				{
				}
				num = 5;
				continue;
				IL_B9:
				flag = (spr_u1DBA.ᜀ() > -1);
				goto IL_D2;
			}
		}
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x0007A488 File Offset: 0x00079488
	private new void ᜀ(spr\u2297 A_0, SizeF A_1, ref RectangleF A_2)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				TextRange textRange = A_0 as TextRange;
				num = 6;
				continue;
			}
			case 1:
			{
				Paragraph ownerParagraph;
				if (ownerParagraph.IsInCell)
				{
					num = 4;
					continue;
				}
				return;
			}
			case 3:
			{
				Paragraph ownerParagraph;
				if (ownerParagraph.Owner is TableCell)
				{
					num = 5;
					continue;
				}
				return;
			}
			case 4:
				num = 3;
				continue;
			case 5:
			{
				Paragraph ownerParagraph;
				float right = (ownerParagraph.Owner.Owner.Owner as Table).TableFormat.Paddings.Right;
				A_2 = new RectangleF(A_2.X, A_2.Y, A_2.Width + right, A_2.Height);
				base.ᜃ(A_2);
				num = 10;
				continue;
			}
			case 6:
				if (A_0 is TextRange)
				{
					num = 7;
					continue;
				}
				return;
			case 7:
				num = 9;
				continue;
			case 8:
			{
				TextRange textRange;
				Paragraph ownerParagraph = textRange.OwnerParagraph;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			}
			case 9:
			{
				TextRange textRange;
				if (textRange.OwnerParagraph != null)
				{
					num = 8;
					continue;
				}
				return;
			}
			case 10:
				return;
			}
			IL_3C:
			if (A_1.Width > A_2.Width)
			{
				if (true)
				{
				}
				num = 0;
				continue;
			}
			break;
			goto IL_3C;
		}
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x0007A618 File Offset: 0x00079618
	private new void ᜀ(SizeF A_0, spr\u1AB8 A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				double num = (double)A_0.Width + base.\u171A().ᜊ().ᜃ() + base.\u171A().ᜊ().ᜂ();
				double num2 = (double)A_0.Height + base.\u171A().ᜊ().ᜁ() + base.\u171A().ᜊ().ᜀ();
				double num3 = 0.0;
				double num4 = 0.0;
				bool flag = false;
				DocPicture docPicture = null;
				int num5 = 145;
				for (;;)
				{
					float num6;
					double num12;
					double num14;
					sprᡌ sprᡌ;
					float num18;
					int num20;
					ParagraphBase paragraphBase;
					bool flag4;
					Paragraph paragraph;
					ShapeVerticalAlignment verticalAlignment3;
					HorizontalOrigin horizontalOrigin;
					int a_;
					VerticalOrigin verticalOrigin;
					switch (num5)
					{
					case 0:
					{
						ShapeVerticalAlignment verticalAlignment;
						switch (verticalAlignment)
						{
						case ShapeVerticalAlignment.None:
							goto IL_1A37;
						case ShapeVerticalAlignment.Top:
							num4 = 0.0;
							num5 = 95;
							continue;
						case ShapeVerticalAlignment.Center:
						{
							float num7;
							num4 = (double)((num6 - num7) / 2f);
							num5 = 103;
							continue;
						}
						case ShapeVerticalAlignment.Bottom:
						{
							float num7;
							num4 = (double)(num6 - num7);
							num5 = 122;
							continue;
						}
						case ShapeVerticalAlignment.Inside:
						{
							float num8;
							num4 = (double)num8;
							num5 = 69;
							continue;
						}
						case ShapeVerticalAlignment.Outside:
						{
							float num7;
							float num9;
							num4 = (double)(num6 - num9 - num7);
							num5 = 131;
							continue;
						}
						default:
							num5 = 10;
							continue;
						}
						break;
					}
					case 1:
					{
						bool flag2;
						if (flag2)
						{
							num5 = 71;
							continue;
						}
						float num10;
						float num11;
						num3 = (double)((num10 - num11) / 2f);
						num5 = 114;
						continue;
					}
					case 2:
					{
						Section section;
						if (section != null)
						{
							num5 = 110;
							continue;
						}
						num3 = (double)(this.ᜅ.ᜆ().X + docPicture.HorizontalPosition);
						num5 = 36;
						continue;
					}
					case 3:
						goto IL_155E;
					case 4:
						num5 = 84;
						continue;
					case 5:
						return;
					case 6:
						if (docPicture.LayoutInCell)
						{
							num5 = 188;
							continue;
						}
						goto IL_6F6;
					case 7:
						num12 = (double)0f;
						goto IL_13DD;
					case 8:
					{
						float num11;
						float num13;
						num12 = (double)(num13 - num11);
						goto IL_13DD;
					}
					case 9:
					{
						float num11;
						float num13;
						if (num11 <= num13)
						{
							num5 = 105;
							continue;
						}
						num5 = 7;
						continue;
					}
					case 10:
						num5 = 27;
						continue;
					case 11:
						goto IL_17D2;
					case 12:
						num5 = 6;
						continue;
					case 13:
					{
						float num10;
						float num15;
						num14 = (double)(num10 - num15);
						goto IL_D29;
					}
					case 14:
					{
						DocumentObject owner = owner.Owner;
						num5 = 158;
						continue;
					}
					case 15:
						this.ᜃ.ᜀ(new RectangleF((float)((double)this.ᜅ.ᜆ().X - base.\u171A().ᜊ().ᜃ() + num3), (float)((double)this.ᜅ.ᜆ().Y - base.\u171A().ᜊ().ᜁ() + num4), (float)num, (float)num2));
						num5 = 99;
						continue;
					case 16:
						num5 = 177;
						continue;
					case 17:
					{
						ShapeVerticalAlignment verticalAlignment2;
						switch (verticalAlignment2)
						{
						case ShapeVerticalAlignment.None:
							goto IL_1A37;
						case ShapeVerticalAlignment.Top:
						{
							float num16;
							num4 = (double)(docPicture.VerticalPosition + num16) - base.\u171A().ᜊ().ᜁ();
							num5 = 139;
							continue;
						}
						case ShapeVerticalAlignment.Center:
						{
							float num7;
							float num16;
							float num17;
							num4 = (double)(num16 + (num17 - num7) / 2f) - base.\u171A().ᜊ().ᜁ();
							num5 = 153;
							continue;
						}
						case ShapeVerticalAlignment.Bottom:
						{
							float num7;
							float num17;
							num4 = (double)(num17 - num7) - base.\u171A().ᜊ().ᜁ();
							num5 = 48;
							continue;
						}
						case ShapeVerticalAlignment.Inside:
						{
							float num16;
							num4 = (double)(docPicture.VerticalPosition + num16) - base.\u171A().ᜊ().ᜁ();
							num5 = 21;
							continue;
						}
						case ShapeVerticalAlignment.Outside:
						{
							float num7;
							float num17;
							num4 = (double)(num17 - num7) - base.\u171A().ᜊ().ᜁ();
							num5 = 169;
							continue;
						}
						default:
							num5 = 20;
							continue;
						}
						break;
					}
					case 18:
						if (sprᡌ.ᜂ().ᜁ() == TextWrappingStyle.TopAndBottom)
						{
							num5 = 163;
							continue;
						}
						return;
					case 19:
					{
						float num11;
						num3 = (double)(base.\u171E().ᜂ(docPicture) - num11);
						num5 = 168;
						continue;
					}
					case 20:
						num5 = 186;
						continue;
					case 21:
						goto IL_1A37;
					case 22:
					{
						bool flag2;
						if (flag2)
						{
							num5 = 35;
							continue;
						}
						num3 = (double)docPicture.HorizontalPosition;
						num5 = 173;
						continue;
					}
					case 23:
						if (base.\u171A().ᜈ())
						{
							num5 = 85;
							continue;
						}
						goto IL_135C;
					case 24:
						num2 = (double)this.ᜅ.ᜆ().Height;
						num5 = 93;
						continue;
					case 25:
						goto IL_1A37;
					case 26:
						goto IL_1BE5;
					case 27:
						goto IL_1A37;
					case 28:
						goto IL_1BE5;
					case 29:
						goto IL_1BE5;
					case 30:
						if (sprᡌ.ᜂ().ᜁ() != TextWrappingStyle.Behind)
						{
							num5 = 76;
							continue;
						}
						goto IL_A15;
					case 31:
					{
						bool flag3 = false;
						num5 = 81;
						continue;
					}
					case 32:
						goto IL_17D2;
					case 33:
						goto IL_572;
					case 34:
						if (!(docPicture.OwnerParagraph.Owner.Owner.Owner as Table).IsSDTTable)
						{
							num5 = 156;
							continue;
						}
						goto IL_D5A;
					case 35:
					{
						sprḰ sprḰ = (docPicture.OwnerParagraph.OwnerTextBody as TableCell).ᜀ;
						num3 = (double)((docPicture.OwnerParagraph.OwnerTextBody as TableCell).ᜀ as spr\u2032).\u170D() - sprḰ.ᜋ().ᜃ() - sprḰ.ᜊ().ᜃ() + (double)docPicture.HorizontalPosition;
						num5 = 175;
						continue;
					}
					case 36:
						goto IL_155E;
					case 37:
						goto IL_1A37;
					case 38:
						goto IL_1BE5;
					case 39:
						if (sprᡌ.ᜂ().ᜁ() != TextWrappingStyle.Inline)
						{
							num5 = 137;
							continue;
						}
						goto IL_A15;
					case 40:
						if ((A_1 as DocPicture).ShapeInfo != null)
						{
							num5 = 80;
							continue;
						}
						goto IL_184E;
					case 41:
						num5 = 74;
						continue;
					case 42:
						if ((A_1 as DocPicture).ShapeInfo == null)
						{
							num5 = 87;
							continue;
						}
						num5 = 57;
						continue;
					case 43:
						goto IL_17D2;
					case 44:
					{
						DocumentObject owner;
						if ((owner as Table).IsSDTTable)
						{
							num5 = 63;
							continue;
						}
						goto IL_C02;
					}
					case 45:
						base.\u171E().\u171D().ᜀ(sprᡌ);
						num5 = 51;
						continue;
					case 46:
					{
						float num11;
						float num15;
						if (num11 >= num15)
						{
							num5 = 55;
							continue;
						}
						num5 = 13;
						continue;
					}
					case 47:
						goto IL_1A37;
					case 48:
						goto IL_1A37;
					case 49:
					{
						DocumentObject owner;
						if (owner is Section)
						{
							num5 = 136;
							continue;
						}
						goto IL_1694;
					}
					case 50:
					{
						float num19;
						num18 = num19;
						num5 = 129;
						continue;
					}
					case 51:
						goto IL_A15;
					case 52:
					{
						DocumentObject owner;
						if (owner is Table)
						{
							num5 = 12;
							continue;
						}
						goto IL_6F6;
					}
					case 53:
						if (!flag)
						{
							num5 = 15;
							continue;
						}
						this.ᜃ.ᜀ(new RectangleF((float)(num3 - base.\u171A().ᜊ().ᜃ()), (float)(num4 - base.\u171A().ᜊ().ᜁ()), (float)num, (float)num2));
						num5 = 101;
						continue;
					case 54:
						goto IL_14D5;
					case 55:
						num5 = 125;
						continue;
					case 56:
						goto IL_1A37;
					case 57:
						num20 = (A_1 as DocPicture).ShapeInfo.\u1755();
						goto IL_A95;
					case 58:
						goto IL_745;
					case 59:
						num5 = 155;
						continue;
					case 60:
					{
						Section section;
						if (section != null)
						{
							num5 = 117;
							continue;
						}
						num3 = (double)(this.ᜅ.ᜆ().X + docPicture.HorizontalPosition);
						if (true)
						{
						}
						num5 = 73;
						continue;
					}
					case 61:
						if (paragraphBase.Owner is spr\u1AD2)
						{
							num5 = 97;
							continue;
						}
						goto IL_FB0;
					case 62:
					{
						DocumentObject owner = docPicture.Owner;
						num5 = 67;
						continue;
					}
					case 63:
						goto IL_6F6;
					case 64:
					{
						ShapeHorizontalAlignment horizontalAlignment;
						switch (horizontalAlignment)
						{
						case ShapeHorizontalAlignment.None:
							num5 = 22;
							continue;
						case ShapeHorizontalAlignment.Left:
							num5 = 9;
							continue;
						case ShapeHorizontalAlignment.Center:
							num5 = 1;
							continue;
						case ShapeHorizontalAlignment.Right:
							num5 = 138;
							continue;
						default:
							num5 = 134;
							continue;
						}
						break;
					}
					case 65:
					{
						ShapeHorizontalAlignment horizontalAlignment2 = docPicture.HorizontalAlignment;
						num5 = 184;
						continue;
					}
					case 66:
					{
						ShapeHorizontalAlignment horizontalAlignment3;
						switch (horizontalAlignment3)
						{
						case ShapeHorizontalAlignment.None:
							goto IL_1BE5;
						case ShapeHorizontalAlignment.Left:
							num3 = 0.0;
							num5 = 130;
							continue;
						case ShapeHorizontalAlignment.Center:
						{
							float num11;
							float num13;
							num3 = (double)((num13 - num11) / 2f);
							num5 = 150;
							continue;
						}
						case ShapeHorizontalAlignment.Right:
						{
							float num11;
							float num13;
							num3 = (double)(num13 - num11);
							num5 = 28;
							continue;
						}
						default:
							num5 = 124;
							continue;
						}
						break;
					}
					case 67:
						goto IL_15EB;
					case 68:
						goto IL_FB0;
					case 69:
						goto IL_1A37;
					case 70:
						num5 = 176;
						continue;
					case 71:
					{
						float num11;
						num3 = (double)((base.\u171E().ᜂ(docPicture) - num11) / 2f);
						num5 = 32;
						continue;
					}
					case 72:
						goto IL_1A37;
					case 73:
						goto IL_3AA;
					case 74:
						flag4 = true;
						goto IL_1172;
					case 75:
					{
						DocumentObject owner;
						if (owner.Owner != null)
						{
							num5 = 14;
							continue;
						}
						goto IL_C02;
					}
					case 76:
						num5 = 123;
						continue;
					case 77:
						num5 = 40;
						continue;
					case 78:
					{
						ShapeHorizontalAlignment horizontalAlignment4 = docPicture.HorizontalAlignment;
						num5 = 182;
						continue;
					}
					case 79:
						if (paragraph.IsInCell)
						{
							num5 = 16;
							continue;
						}
						goto IL_6AE;
					case 80:
						(A_1 as DocPicture).ShapeInfo.ᜀ(this.ᜃ.ᜁ().Location);
						num5 = 119;
						continue;
					case 81:
						goto IL_1694;
					case 82:
						goto IL_1A37;
					case 83:
						flag4 = ((this.ᜅ() as DocPicture).TextWrappingStyle == TextWrappingStyle.Inline);
						goto IL_1172;
					case 84:
						if (num2 > (double)this.ᜅ.ᜆ().Width)
						{
							num5 = 181;
							continue;
						}
						goto IL_135C;
					case 85:
						num5 = 149;
						continue;
					case 86:
						num5 = 54;
						continue;
					case 87:
						num5 = 104;
						continue;
					case 88:
						if (num3 < 0.0)
						{
							num5 = 70;
							continue;
						}
						goto IL_1BE5;
					case 89:
						num5 = 178;
						continue;
					case 90:
						if (docPicture.OwnerParagraph.IsInCell)
						{
							num5 = 167;
							continue;
						}
						goto IL_D5A;
					case 91:
						if (!(A_1 is DocPicture))
						{
							num5 = 41;
							continue;
						}
						num5 = 79;
						continue;
					case 92:
						num5 = 164;
						continue;
					case 93:
						goto IL_C92;
					case 94:
						goto IL_1BE5;
					case 95:
						goto IL_1A37;
					case 96:
						goto IL_1BE5;
					case 97:
						paragraph = (paragraphBase.Owner.Owner.Owner as Paragraph);
						num5 = 68;
						continue;
					case 98:
					{
						DocumentObject owner;
						if (owner is Section)
						{
							num5 = 106;
							continue;
						}
						num5 = 52;
						continue;
					}
					case 99:
						goto IL_4A4;
					case 100:
						num5 = 82;
						continue;
					case 101:
						goto IL_4A4;
					case 102:
						goto IL_1BE5;
					case 103:
						goto IL_1A37;
					case 104:
						num20 = 0;
						goto IL_A95;
					case 105:
						num5 = 8;
						continue;
					case 106:
						goto IL_C02;
					case 107:
						goto IL_135C;
					case 108:
						num5 = 33;
						continue;
					case 109:
						goto IL_3AA;
					case 110:
					{
						float num13;
						num3 = (double)(num13 + docPicture.HorizontalPosition);
						num5 = 3;
						continue;
					}
					case 111:
						num5 = 18;
						continue;
					case 112:
						num5 = 170;
						continue;
					case 113:
						goto IL_1BE5;
					case 114:
						goto IL_17D2;
					case 115:
						if (sprᡌ.ᜂ().ᜁ() == TextWrappingStyle.Behind)
						{
							num5 = 160;
							continue;
						}
						goto IL_67F;
					case 116:
						num5 = 42;
						continue;
					case 117:
					{
						Section section;
						num3 = (double)(section.PageSetup.Margins.Left + docPicture.HorizontalPosition);
						num5 = 109;
						continue;
					}
					case 118:
						goto IL_1A37;
					case 119:
						goto IL_184E;
					case 120:
					{
						flag = true;
						float num13 = 0f;
						float num15 = 0f;
						float num16 = 0f;
						float num9 = 0f;
						float num8 = 0f;
						float num19 = 0f;
						Section section = null;
						docPicture = (A_1 as DocPicture);
						float num10 = 0f;
						num6 = 0f;
						float num21 = 0f;
						float num17 = 0f;
						bool flag3 = true;
						float num11 = docPicture.Width * docPicture.WidthScale / 100f;
						float num7 = docPicture.Height * docPicture.WidthScale / 100f;
						num5 = 128;
						continue;
					}
					case 121:
						goto IL_67F;
					case 122:
						goto IL_1A37;
					case 123:
						if (sprᡌ.ᜂ().ᜁ() != TextWrappingStyle.InFrontOfText)
						{
							num5 = 45;
							continue;
						}
						goto IL_A15;
					case 124:
						num5 = 38;
						continue;
					case 125:
					{
						float num10;
						float num11;
						num14 = (double)(num10 - num11);
						goto IL_D29;
					}
					case 126:
						if (docPicture.LayoutInCell)
						{
							num5 = 147;
							continue;
						}
						goto IL_D5A;
					case 127:
						goto IL_17D2;
					case 128:
						if (docPicture.Owner != null)
						{
							num5 = 62;
							continue;
						}
						goto IL_1694;
					case 129:
						goto IL_745;
					case 130:
						goto IL_1BE5;
					case 131:
						goto IL_1A37;
					case 132:
						if ((A_1 as DocPicture).TextWrappingStyle == TextWrappingStyle.InFrontOfText)
						{
							num5 = 171;
							continue;
						}
						num5 = 165;
						continue;
					case 133:
						num5 = 132;
						continue;
					case 134:
						num5 = 11;
						continue;
					case 135:
						num5 = 26;
						continue;
					case 136:
					{
						DocumentObject owner;
						Section section = owner as Section;
						float num13 = section.PageSetup.Margins.Left;
						float num15 = section.PageSetup.Margins.Right;
						float num16 = section.PageSetup.Margins.Top;
						float num9 = section.PageSetup.Margins.Bottom;
						num6 = section.PageSetup.PageSize.Height;
						float num10 = section.PageSetup.PageSize.Width;
						float num21 = section.PageSetup.ClientWidth;
						float num19 = section.PageSetup.FooterDistance;
						float num8 = section.PageSetup.HeaderDistance;
						float num17 = (float)this.ᜅ.ᜂ();
						num5 = 159;
						continue;
					}
					case 137:
						num5 = 30;
						continue;
					case 138:
					{
						bool flag2;
						if (flag2)
						{
							num5 = 19;
							continue;
						}
						num5 = 46;
						continue;
					}
					case 139:
						goto IL_1A37;
					case 140:
						switch (verticalAlignment3)
						{
						case ShapeVerticalAlignment.None:
							goto IL_1A37;
						case ShapeVerticalAlignment.Top:
							num4 = (double)(num6 - num18) - base.\u171A().ᜊ().ᜁ();
							num5 = 157;
							continue;
						case ShapeVerticalAlignment.Center:
						{
							float num7;
							num4 = (double)(num6 - num18 + (num18 - num7) / 2f) - base.\u171A().ᜊ().ᜁ();
							num5 = 56;
							continue;
						}
						case ShapeVerticalAlignment.Bottom:
						{
							float num7;
							num4 = (double)(num6 - num7) - base.\u171A().ᜊ().ᜁ();
							num5 = 25;
							continue;
						}
						case ShapeVerticalAlignment.Inside:
							num4 = (double)(num6 - num18) - base.\u171A().ᜊ().ᜁ();
							num5 = 47;
							continue;
						case ShapeVerticalAlignment.Outside:
						{
							float num7;
							num4 = (double)(num6 - num7) - base.\u171A().ᜊ().ᜁ();
							num5 = 72;
							continue;
						}
						default:
							num5 = 100;
							continue;
						}
						break;
					case 141:
					{
						bool flag3;
						if (flag3)
						{
							num5 = 65;
							continue;
						}
						goto IL_1BE5;
					}
					case 142:
					{
						sprḰ sprḰ2 = (docPicture.OwnerParagraph.OwnerTextBody as TableCell).ᜀ;
						num3 = (double)((docPicture.OwnerParagraph.OwnerTextBody as TableCell).ᜀ as spr\u2032).\u170D() - sprḰ2.ᜋ().ᜃ() - sprḰ2.ᜊ().ᜃ();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3AA;
						default:
							if (false)
							{
							}
							num5 = 189;
							continue;
						}
						break;
					}
					case 143:
						goto IL_1BE5;
					case 144:
					{
						bool flag2 = false;
						num5 = 90;
						continue;
					}
					case 145:
						if (A_1 is DocPicture)
						{
							num5 = 120;
							continue;
						}
						goto IL_1BE5;
					case 146:
						goto IL_1BE5;
					case 147:
						num5 = 34;
						continue;
					case 148:
						paragraphBase = ((A_1 as spr\u208E).ᜂ() as ParagraphBase);
						num5 = 185;
						continue;
					case 149:
						if (A_1 is DocPicture)
						{
							num5 = 59;
							continue;
						}
						goto IL_135C;
					case 150:
						goto IL_1BE5;
					case 151:
						switch (horizontalOrigin)
						{
						case HorizontalOrigin.Margin:
							num5 = 2;
							continue;
						case HorizontalOrigin.Page:
						{
							num3 = (double)docPicture.HorizontalPosition;
							ShapeHorizontalAlignment horizontalAlignment = docPicture.HorizontalAlignment;
							num5 = 64;
							continue;
						}
						case HorizontalOrigin.Column:
							num5 = 60;
							continue;
						case HorizontalOrigin.Character:
							goto IL_14D5;
						case HorizontalOrigin.LeftMarginArea:
						{
							float num13;
							num3 = (double)num13;
							ShapeHorizontalAlignment horizontalAlignment3 = docPicture.HorizontalAlignment;
							num5 = 66;
							continue;
						}
						default:
							num5 = 86;
							continue;
						}
						break;
					case 152:
						if ((A_1 as DocPicture).Image == null)
						{
							num5 = 116;
							continue;
						}
						num5 = 187;
						continue;
					case 153:
						goto IL_1A37;
					case 154:
						goto IL_1BE5;
					case 155:
						if ((this.ᜅ() as DocPicture).TextWrappingStyle == TextWrappingStyle.Inline)
						{
							num5 = 4;
							continue;
						}
						goto IL_135C;
					case 156:
					{
						bool flag2 = true;
						num4 = this.ᜁ(docPicture);
						num3 = this.ᜀ(docPicture);
						num5 = 146;
						continue;
					}
					case 157:
						goto IL_1A37;
					case 158:
						goto IL_15EB;
					case 159:
					{
						Section section;
						if (section.Columns.Count > 1)
						{
							num5 = 31;
							continue;
						}
						goto IL_1694;
					}
					case 160:
					{
						spr\u2591 spr_u = new spr\u2591();
						spr_u.ᜀ(A_1 as DocPicture);
						spr_u.ᜀ(true);
						spr_u.ᜀ(this.ᜃ);
						spr_u.ᜁ(base.\u171E().ᜈ());
						spr_u.ᜀ(a_);
						base.\u171E().\u171D().ᜁ(spr_u);
						num5 = 121;
						continue;
					}
					case 161:
						switch (verticalOrigin)
						{
						case VerticalOrigin.Margin:
						{
							float num16;
							num4 = (double)(num16 + docPicture.VerticalPosition);
							ShapeVerticalAlignment verticalAlignment2 = docPicture.VerticalAlignment;
							num5 = 17;
							continue;
						}
						case VerticalOrigin.Page:
						{
							num4 = (double)docPicture.VerticalPosition;
							ShapeVerticalAlignment verticalAlignment = docPicture.VerticalAlignment;
							num5 = 0;
							continue;
						}
						case VerticalOrigin.Paragraph:
							num4 = (double)(this.ᜅ.ᜆ().Y + docPicture.VerticalPosition);
							num5 = 37;
							continue;
						case VerticalOrigin.Line:
						case VerticalOrigin.TopMarginArea:
							goto IL_572;
						case VerticalOrigin.BottomMarginArea:
							num18 = 0f;
							num5 = 179;
							continue;
						default:
							num5 = 108;
							continue;
						}
						break;
					case 162:
						goto IL_1BE5;
					case 163:
						goto IL_F49;
					case 164:
						if ((A_1 as DocPicture).TextWrappingStyle != TextWrappingStyle.Behind)
						{
							num5 = 133;
							continue;
						}
						goto IL_6AE;
					case 165:
						flag4 = true;
						goto IL_1172;
					case 166:
						if (sprᡌ.ᜂ().ᜁ() != TextWrappingStyle.InFrontOfText)
						{
							num5 = 111;
							continue;
						}
						goto IL_F49;
					case 167:
						num5 = 126;
						continue;
					case 168:
						goto IL_17D2;
					case 169:
						goto IL_1A37;
					case 170:
						if (num2 > (double)this.ᜅ.ᜆ().Height)
						{
							num5 = 24;
							continue;
						}
						goto IL_C92;
					case 171:
						goto IL_6AE;
					case 172:
					{
						bool flag3;
						if (flag3)
						{
							num5 = 78;
							continue;
						}
						goto IL_1BE5;
					}
					case 173:
						goto IL_17D2;
					case 174:
						if (docPicture.TextWrappingStyle != TextWrappingStyle.Inline)
						{
							num5 = 144;
							continue;
						}
						flag = false;
						num5 = 94;
						continue;
					case 175:
						goto IL_17D2;
					case 176:
					{
						bool flag2;
						if (flag2)
						{
							num5 = 142;
							continue;
						}
						goto IL_1BE5;
					}
					case 177:
						if ((A_1 as DocPicture).LayoutInCell)
						{
							num5 = 92;
							continue;
						}
						goto IL_6AE;
					case 178:
						goto IL_1BE5;
					case 179:
						if (base.\u171E().ᜈ())
						{
							num5 = 50;
							continue;
						}
						num18 = base.\u171E().\u171D().ᜀ()[1].ᜁ().Height;
						num5 = 58;
						continue;
					case 180:
						if (A_1 is spr\u208E)
						{
							num5 = 148;
							continue;
						}
						goto IL_1C1B;
					case 181:
					{
						spr\u1D30 spr_u1D = (paragraph.OwnerTextBody as TableCell).ᜀ;
						num2 = (double)this.ᜅ.ᜆ().Width + paragraph.ᜀ.ᜋ().ᜂ() - 2.0 * (spr_u1D.ᜋ().ᜁ() + spr_u1D.ᜋ().ᜀ()) - (spr_u1D.ᜋ().ᜃ() + spr_u1D.ᜋ().ᜂ());
						num5 = 107;
						continue;
					}
					case 182:
					{
						ShapeHorizontalAlignment horizontalAlignment4;
						switch (horizontalAlignment4)
						{
						case ShapeHorizontalAlignment.None:
							goto IL_1BE5;
						case ShapeHorizontalAlignment.Left:
							num3 = (double)this.ᜅ.ᜆ().X;
							num5 = 162;
							continue;
						case ShapeHorizontalAlignment.Center:
						{
							float num11;
							float num21;
							num3 = (double)(this.ᜅ.ᜆ().X + (num21 - num11) / 2f);
							num5 = 102;
							continue;
						}
						case ShapeHorizontalAlignment.Right:
						{
							float num11;
							float num21;
							num3 = (double)(this.ᜅ.ᜆ().X + num21 - num11);
							num5 = 96;
							continue;
						}
						default:
							num5 = 135;
							continue;
						}
						break;
					}
					case 183:
						if (A_1 is DocPicture)
						{
							num5 = 77;
							continue;
						}
						return;
					case 184:
					{
						ShapeHorizontalAlignment horizontalAlignment2;
						switch (horizontalAlignment2)
						{
						case ShapeHorizontalAlignment.None:
							goto IL_1BE5;
						case ShapeHorizontalAlignment.Left:
							num3 = (double)this.ᜅ.ᜆ().X;
							num5 = 143;
							continue;
						case ShapeHorizontalAlignment.Center:
						{
							float num11;
							float num13;
							float num21;
							num3 = (double)(num13 + (num21 - num11) / 2f);
							num5 = 154;
							continue;
						}
						case ShapeHorizontalAlignment.Right:
						{
							float num11;
							num3 = (double)(this.ᜅ.ᜆ().Right - num11);
							num5 = 29;
							continue;
						}
						default:
							num5 = 89;
							continue;
						}
						break;
					}
					case 185:
						goto IL_1C1B;
					case 186:
						goto IL_1A37;
					case 187:
						num20 = (A_1 as DocPicture).OrderIndex;
						goto IL_A95;
					case 188:
						num5 = 44;
						continue;
					case 189:
						goto IL_1BE5;
					}
					break;
					IL_3AA:
					num5 = 172;
					continue;
					IL_4A4:
					this.ᜀ();
					this.ᜃ.ᜀ((this.ᜆ as spr\u1DA4).ᜊ().ᜁ());
					num5 = 183;
					continue;
					IL_572:
					num4 = (double)this.ᜅ.ᜆ().Y - base.\u171A().ᜊ().ᜁ() + (double)docPicture.VerticalPosition;
					num5 = 118;
					continue;
					IL_67F:
					num5 = 166;
					continue;
					IL_6AE:
					num5 = 83;
					continue;
					IL_6F6:
					num5 = 75;
					continue;
					IL_745:
					num4 = (double)(num6 - num18) - base.\u171A().ᜊ().ᜁ();
					verticalAlignment3 = docPicture.VerticalAlignment;
					num5 = 140;
					continue;
					IL_A15:
					num5 = 152;
					continue;
					IL_A95:
					a_ = num20;
					num5 = 115;
					continue;
					IL_C02:
					num5 = 49;
					continue;
					IL_C92:
					num5 = 23;
					continue;
					IL_1172:
					if (flag4)
					{
						num5 = 112;
						continue;
					}
					goto IL_C92;
					IL_D29:
					num3 = num14;
					num5 = 43;
					continue;
					IL_D5A:
					verticalOrigin = docPicture.VerticalOrigin;
					num5 = 161;
					continue;
					IL_F49:
					spr\u2591 spr_u2 = new spr\u2591();
					spr_u2.ᜀ(A_1 as DocPicture);
					spr_u2.ᜀ(true);
					spr_u2.ᜀ(this.ᜃ);
					spr_u2.ᜁ(base.\u171E().ᜈ());
					spr_u2.ᜀ(a_);
					base.\u171E().\u171D().ᜀ(spr_u2);
					num5 = 5;
					continue;
					IL_FB0:
					num5 = 91;
					continue;
					IL_135C:
					this.ᜃ = new sprᦰ(A_1);
					num5 = 53;
					continue;
					IL_13DD:
					num3 = num12;
					num5 = 127;
					continue;
					IL_14D5:
					num3 = (double)(this.ᜅ.ᜆ().X + docPicture.HorizontalPosition);
					num5 = 113;
					continue;
					IL_155E:
					num5 = 141;
					continue;
					IL_15EB:
					num5 = 98;
					continue;
					IL_1694:
					num5 = 174;
					continue;
					IL_17D2:
					num5 = 88;
					continue;
					IL_184E:
					float num22 = (A_1 as DocPicture).WrapDistanceTop;
					float num23 = (A_1 as DocPicture).WrapDistanceBottom;
					float num24 = (A_1 as DocPicture).WrapDistanceLeft;
					float num25 = (A_1 as DocPicture).WrapDistanceRight;
					sprᡌ = new sprᡌ();
					sprᡌ.ᜀ(new RectangleF(this.ᜃ.ᜁ().X - num24, this.ᜃ.ᜁ().Y - num22, this.ᜃ.ᜁ().Width + num24 + num25, this.ᜃ.ᜁ().Height + num22 + num23));
					sprᡌ.ᜂ().ᜀ((A_1 as DocPicture).TextWrappingStyle);
					sprᡌ.ᜂ().ᜀ((A_1 as DocPicture).TextWrappingType);
					sprᡌ.ᜁ((A_1 as DocPicture).LayoutInCell);
					num5 = 39;
					continue;
					IL_1A37:
					horizontalOrigin = docPicture.HorizontalOrigin;
					num5 = 151;
					continue;
					IL_1BE5:
					num = this.ᜀ(num, A_1);
					paragraphBase = (A_1 as ParagraphBase);
					num5 = 180;
					continue;
					IL_1C1B:
					paragraph = paragraphBase.OwnerParagraph;
					num5 = 61;
				}
			}
			return;
		}
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x0007C278 File Offset: 0x0007B278
	private new double ᜁ(DocPicture A_0)
	{
		switch (0)
		{
		default:
		{
			double num;
			double num2;
			for (;;)
			{
				num = 0.0;
				num2 = (double)((A_0.OwnerParagraph.OwnerTextBody as TableCell).ᜀ as spr\u2032).\u1713();
				VerticalOrigin verticalOrigin = A_0.VerticalOrigin;
				int num3 = 2;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_12E;
					case 1:
						goto IL_EF;
					case 2:
						switch (verticalOrigin)
						{
						case VerticalOrigin.Margin:
						case VerticalOrigin.Page:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_12E;
							default:
								if (false)
								{
								}
								num = num2 + (double)A_0.VerticalPosition;
								num3 = 0;
								continue;
							}
							break;
						default:
							num3 = 4;
							continue;
						}
						break;
					case 3:
						goto IL_9B;
					case 4:
						num3 = 6;
						continue;
					case 5:
						goto IL_12E;
					case 6:
						num = (double)(this.ᜅ.ᜆ().Y + A_0.VerticalPosition);
						num3 = 5;
						continue;
					case 7:
						num3 = 3;
						continue;
					case 8:
						if (num >= num2)
						{
							num3 = 7;
							continue;
						}
						num3 = 1;
						continue;
					}
					break;
					IL_12E:
					num3 = 8;
				}
			}
			IL_9B:
			return num;
			IL_EF:
			return num2;
		}
		}
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x0007C3D8 File Offset: 0x0007B3D8
	private new double ᜀ(DocPicture A_0)
	{
		switch (0)
		{
		default:
		{
			double num;
			for (;;)
			{
				num = 0.0;
				sprḰ sprḰ = (A_0.OwnerParagraph.OwnerTextBody as TableCell).ᜀ;
				double num2 = (double)base.\u171E().ᜂ(A_0) + sprḰ.ᜊ().ᜃ() + sprḰ.ᜊ().ᜂ();
				double num3 = (double)((A_0.OwnerParagraph.OwnerTextBody as TableCell).ᜀ as spr\u2032).\u170D() - sprḰ.ᜊ().ᜃ();
				float left = (A_0.OwnerParagraph.Owner.Owner.Owner as Table).TableBounds.Left;
				HorizontalOrigin horizontalOrigin = A_0.HorizontalOrigin;
				int num4 = 4;
				for (;;)
				{
					switch (num4)
					{
					case 0:
						goto IL_1F4;
					case 1:
						goto IL_1F4;
					case 2:
						goto IL_1F4;
					case 3:
						goto IL_1F4;
					case 4:
						switch (horizontalOrigin)
						{
						case HorizontalOrigin.Margin:
						case HorizontalOrigin.Column:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_142;
							default:
							{
								if (false)
								{
								}
								ShapeHorizontalAlignment horizontalAlignment = A_0.HorizontalAlignment;
								num4 = 17;
								continue;
							}
							}
							break;
						case HorizontalOrigin.Page:
						{
							num = (double)A_0.HorizontalPosition;
							ShapeHorizontalAlignment horizontalAlignment2 = A_0.HorizontalAlignment;
							num4 = 8;
							continue;
						}
						default:
							num4 = 6;
							continue;
						}
						break;
					case 5:
						goto IL_1F4;
					case 6:
						goto IL_142;
					case 7:
						if (num < num3)
						{
							num4 = 18;
							continue;
						}
						return num;
					case 8:
					{
						ShapeHorizontalAlignment horizontalAlignment2;
						switch (horizontalAlignment2)
						{
						case ShapeHorizontalAlignment.None:
							num = num3 + (double)A_0.HorizontalPosition;
							num4 = 5;
							continue;
						case ShapeHorizontalAlignment.Left:
							num = num3;
							num4 = 1;
							continue;
						case ShapeHorizontalAlignment.Center:
							num = num3 + (num2 - (double)A_0.Width) / 2.0;
							num4 = 10;
							continue;
						case ShapeHorizontalAlignment.Right:
							num = num3 + (num2 - (double)A_0.Width);
							num4 = 11;
							continue;
						default:
							num4 = 20;
							continue;
						}
						break;
					}
					case 9:
						num4 = 2;
						continue;
					case 10:
						goto IL_1F4;
					case 11:
						goto IL_1F4;
					case 12:
						return num;
					case 13:
						num4 = 7;
						continue;
					case 14:
						goto IL_1F4;
					case 15:
						goto IL_1F4;
					case 16:
						goto IL_1F4;
					case 17:
					{
						ShapeHorizontalAlignment horizontalAlignment;
						switch (horizontalAlignment)
						{
						case ShapeHorizontalAlignment.None:
							num = num3 + sprḰ.ᜊ().ᜃ() + (double)A_0.HorizontalPosition;
							num4 = 14;
							continue;
						case ShapeHorizontalAlignment.Left:
							num = num3 + sprḰ.ᜊ().ᜃ();
							num4 = 21;
							continue;
						case ShapeHorizontalAlignment.Center:
							num = num3 + sprḰ.ᜊ().ᜃ() + (num2 - sprḰ.ᜊ().ᜃ() - sprḰ.ᜊ().ᜂ() - (double)A_0.Width) / 2.0;
							num4 = 16;
							continue;
						case ShapeHorizontalAlignment.Right:
							num = num3 + sprḰ.ᜊ().ᜃ() + (num2 - sprḰ.ᜊ().ᜃ() - sprḰ.ᜊ().ᜂ() - (double)A_0.Width);
							num4 = 3;
							continue;
						default:
							num4 = 9;
							continue;
						}
						break;
					}
					case 18:
						num = num3;
						num4 = 12;
						continue;
					case 19:
						num = num3 + sprḰ.ᜊ().ᜃ() + (double)A_0.HorizontalPosition;
						num4 = 15;
						continue;
					case 20:
						num4 = 0;
						continue;
					case 21:
						goto IL_1F4;
					case 22:
						if (A_0.HorizontalOrigin == HorizontalOrigin.Column)
						{
							num4 = 13;
							continue;
						}
						return num;
					}
					break;
					IL_142:
					num4 = 19;
					continue;
					IL_1F4:
					if (true)
					{
					}
					num4 = 22;
				}
			}
			return num;
		}
		}
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x0007C7D0 File Offset: 0x0007B7D0
	private new void ᜀ()
	{
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				float num2;
				spr\u1D30 spr_u1D;
				switch (num)
				{
				case 0:
				{
					TableCell tableCell;
					if (tableCell.OwnerRow.OwnerTable.TableFormat.CellSpacing > 0f)
					{
						num = 1;
						continue;
					}
					goto IL_16E;
				}
				case 1:
				{
					TableCell tableCell;
					num2 = tableCell.OwnerRow.OwnerTable.TableFormat.CellSpacing * 2f;
					num = 5;
					continue;
				}
				case 2:
					num = 3;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_214;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if ((this.ᜃ.ᜂ() as TextRange).OwnerParagraph != null)
						{
							num = 10;
							continue;
						}
						return;
					}
					break;
				case 4:
					num2 = 0f;
					goto IL_214;
				case 5:
					goto IL_16E;
				case 6:
					if ((this.ᜃ.ᜂ() as TextRange).OwnerParagraph.IsInCell)
					{
						num = 11;
						continue;
					}
					return;
				case 8:
				{
					RectangleF rectangleF;
					if (rectangleF.X < (spr_u1D as spr\u2032).\u170D())
					{
						num = 4;
						continue;
					}
					return;
				}
				case 9:
					return;
				case 10:
					num = 6;
					continue;
				case 11:
				{
					ParagraphFormat format = (base.\u1718() as TextRange).OwnerParagraph.Format;
					TableCell tableCell = (base.\u1718() as TextRange).OwnerParagraph.Owner as TableCell;
					spr_u1D = ((spr\u1AB8)tableCell).ᜀ();
					RectangleF rectangleF = this.ᜃ.ᜁ();
					num = 8;
					continue;
				}
				}
				if (this.ᜃ.ᜂ() is TextRange)
				{
					num = 2;
					continue;
				}
				break;
				IL_16E:
				float num3 = (float)(spr_u1D.ᜊ().ᜃ() + spr_u1D.ᜋ().ᜃ() - (double)num2);
				this.ᜃ.ᜁ((spr_u1D as spr\u2032).\u170D() - this.ᜃ.ᜁ().X - num3);
				num = 9;
				continue;
				IL_214:
				num = 0;
			}
			return;
		}
		}
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x0007CA30 File Offset: 0x0007BA30
	private new double ᜀ(double A_0, spr\u1AB8 A_1)
	{
		switch (0)
		{
		default:
		{
			Paragraph paragraph;
			for (;;)
			{
				IL_AA:
				ParagraphBase paragraphBase = A_1 as ParagraphBase;
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_369:
					num = 22;
					break;
				default:
					if (false)
					{
					}
					num = 34;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (base.\u171A().ᜈ())
						{
							num = 2;
							continue;
						}
						return A_0;
					case 1:
						goto IL_4A0;
					case 2:
						goto IL_22E;
					case 3:
						if (this.ᜆ())
						{
							num = 27;
							continue;
						}
						goto IL_122;
					case 4:
						if (paragraphBase.Owner is spr\u1AD2)
						{
							num = 20;
							continue;
						}
						goto IL_369;
					case 5:
						if (A_0 > (double)this.ᜅ.ᜇ().Height + (paragraph.Owner as TableCell).ᜀ.ᜊ().ᜀ())
						{
							num = 17;
							continue;
						}
						return A_0;
					case 6:
						if (A_0 > (double)this.ᜅ.ᜇ().Width + (paragraph.Owner as TableCell).ᜀ.ᜊ().ᜂ())
						{
							num = 16;
							continue;
						}
						goto IL_3EB;
					case 7:
						if (base.\u171A().ᜀ())
						{
							num = 30;
							continue;
						}
						return A_0;
					case 8:
						goto IL_3C6;
					case 9:
					{
						RectangleF rectangleF = this.ᜅ.ᜆ();
						num = 15;
						continue;
					}
					case 10:
						if (A_1 is DocPicture)
						{
							num = 35;
							continue;
						}
						goto IL_3EB;
					case 11:
						if (A_0 > (double)this.ᜅ.ᜆ().Width)
						{
							num = 9;
							continue;
						}
						goto IL_122;
					case 12:
						if (true)
						{
						}
						if ((this.ᜆ as spr\u1DA4).ᜊ().ᜁ() == Spire.Layouting.TabJustification.Centered)
						{
							num = 8;
							continue;
						}
						goto IL_233;
					case 13:
						if ((this.ᜆ as spr\u1DA4).ᜊ().ᜁ() != Spire.Layouting.TabJustification.Right)
						{
							num = 26;
							continue;
						}
						goto IL_3C6;
					case 14:
						paragraphBase = ((A_1 as spr\u208E).ᜂ() as ParagraphBase);
						num = 29;
						continue;
					case 15:
					{
						RectangleF rectangleF;
						if (rectangleF.Width > 0f)
						{
							num = 21;
							continue;
						}
						goto IL_122;
					}
					case 16:
						num = 32;
						continue;
					case 17:
						num = 0;
						continue;
					case 18:
						goto IL_11D;
					case 19:
						num = 6;
						continue;
					case 20:
						paragraph = (paragraphBase.Owner.Owner.Owner as Paragraph);
						num = 24;
						continue;
					case 21:
						num = 13;
						continue;
					case 22:
						if (!(A_1 is DocPicture))
						{
							num = 23;
							continue;
						}
						goto IL_122;
					case 23:
						num = 11;
						continue;
					case 24:
						goto IL_1D0;
					case 25:
						if (A_1 is DocPicture)
						{
							num = 28;
							continue;
						}
						return A_0;
					case 26:
						num = 12;
						continue;
					case 27:
						num = 33;
						continue;
					case 28:
						num = 7;
						continue;
					case 29:
						goto IL_4A5;
					case 30:
						num = 5;
						continue;
					case 31:
						if (base.\u171A().ᜀ())
						{
							num = 19;
							continue;
						}
						goto IL_3EB;
					case 32:
						if (!base.\u171A().ᜈ())
						{
							num = 18;
							continue;
						}
						goto IL_3EB;
					case 33:
						if ((this.ᜆ as spr\u1DA4).ᜊ().ᜁ() == Spire.Layouting.TabJustification.Right)
						{
							num = 1;
							continue;
						}
						goto IL_122;
					case 34:
						if (A_1 is spr\u208E)
						{
							num = 14;
							continue;
						}
						goto IL_4A5;
					case 35:
						num = 31;
						continue;
					}
					goto IL_AA;
					IL_122:
					num = 10;
					continue;
					IL_3C6:
					num = 3;
					continue;
					IL_3EB:
					num = 25;
					continue;
					IL_4A5:
					paragraph = paragraphBase.OwnerParagraph;
					num = 4;
				}
				IL_1D0:
				goto IL_369;
			}
			IL_11D:
			return (double)this.ᜅ.ᜆ().Width + (paragraph.Owner as TableCell).ᜀ.ᜊ().ᜂ();
			IL_22E:
			return (double)this.ᜅ.ᜆ().Height + (paragraph.Owner as TableCell).ᜀ.ᜊ().ᜀ();
			IL_233:
			return (double)this.ᜅ.ᜆ().Width;
			IL_4A0:
			goto IL_233;
		}
		}
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x0007CF44 File Offset: 0x0007BF44
	private new void ᜀ(spr\u17BA A_0, float A_1)
	{
		switch (0)
		{
		default:
		{
			SizeF a_;
			for (;;)
			{
				IL_57:
				spr\u17BA[] array = null;
				for (;;)
				{
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 13;
							continue;
						case 1:
							if (!base.\u171A().ᜆ())
							{
								num = 2;
								continue;
							}
							goto IL_197;
						case 2:
							num = 11;
							continue;
						case 3:
							if (base.\u171A() is sprḈ)
							{
								num = 4;
								continue;
							}
							array = A_0.ᜀ(base.\u171E(), this.ᜅ.ᜆ().Size, (this.ᜆ as spr\u1DA4).ᜈ().Width, A_1);
							num = 5;
							continue;
						case 4:
							array = new spr\u17BA[]
							{
								A_0,
								A_0
							};
							num = 6;
							continue;
						case 5:
							goto IL_1DD;
						case 6:
							goto IL_1DD;
						case 7:
							goto IL_1D8;
						case 8:
							goto IL_179;
						case 9:
							if (!this.ᜅ.ᜀ(a_))
							{
								num = 0;
								continue;
							}
							goto IL_7D;
						case 10:
							goto IL_1B3;
						case 11:
							if (array[1] == null)
							{
								num = 8;
								continue;
							}
							goto IL_197;
						case 12:
							a_ = array[0].ᜀ(base.\u171E());
							num = 9;
							continue;
						case 13:
							if (this.ᜂ())
							{
								num = 7;
								continue;
							}
							a_.Width = this.ᜅ.ᜆ().Width;
							num = 15;
							continue;
						case 14:
							if (array != null)
							{
								num = 12;
								continue;
							}
							return;
						case 15:
							goto IL_7D;
						}
						goto IL_57;
						IL_7D:
						if (true)
						{
						}
						this.ᜀ(a_, array[0]);
						num = 1;
						continue;
						IL_197:
						this.ᜁ = array[1];
						this.ᜀ = LayoutState.Splitted;
						num = 10;
						continue;
						IL_1DD:
						this.ᜀ = LayoutState.NotFitted;
						num = 14;
					}
					IL_179:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_18F;
					}
				}
			}
			IL_18F:
			if (false)
			{
			}
			this.ᜀ = LayoutState.Fitted;
			return;
			IL_1B3:
			return;
			IL_1D8:
			this.ᜀ = LayoutState.NotFitted;
			this.ᜇ = (a_.Height > this.ᜅ.ᜆ().Height);
			return;
		}
		}
	}

	// Token: 0x0400135B RID: 4955
	private new const float ᜀ = 16f;

	// Token: 0x0400135C RID: 4956
	private new Regex ᜁ;

	// Token: 0x0400135D RID: 4957
	private new spr\u25E5 ᜂ;
}
