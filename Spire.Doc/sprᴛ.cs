using System;
using System.Drawing;
using System.Reflection;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Layouting;

// Token: 0x02000397 RID: 919
[DefaultMember("Item")]
internal class sprᴛ : spr\u17C8
{
	// Token: 0x060033DE RID: 13278 RVA: 0x002FA814 File Offset: 0x002F9814
	public sprᴛ(spr\u17C8 A_0)
	{
		this.ᜀ = A_0;
		this.ᜁ = null;
		this.ᜂ = this.ᜀ.ᜁ();
		this.ᜃ = 0;
	}

	// Token: 0x060033DF RID: 13279 RVA: 0x002FA850 File Offset: 0x002F9850
	public sprᴛ(spr\u17C8 A_0, spr\u1AB8 A_1, int A_2)
	{
		int a_ = 3;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("੨Ѫͬ᭮ၰᩲ᭴ቶ୸", a_));
		}
		if (A_1 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("੨ṪὬᵮᑰᵲŴ㑶ᅸቺᅼ᭾", a_));
		}
		if (A_2 < 0)
		{
			throw new ArgumentOutOfRangeException(ClipboardData.b("ཨɪὬᱮհ㩲᭴፶ᱸͺ", a_), A_2, ClipboardData.b("㽨੪Ŭᩮᑰ卲ᙴᙶ᝸孺፼ၾꎂꦈﲎ뎒ꖔ", a_));
		}
		this.ᜀ = A_0;
		this.ᜁ = A_1;
		this.ᜂ = A_2;
	}

	// Token: 0x060033E0 RID: 13280 RVA: 0x002FA8E4 File Offset: 0x002F98E4
	internal spr\u1AB8 ᜀ()
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

	// Token: 0x060033E1 RID: 13281 RVA: 0x002FA928 File Offset: 0x002F9928
	public spr\u17C8 ᜁ()
	{
		sprᴛ sprᴛ = this.ᜀ as sprᴛ;
		if (sprᴛ == null)
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
				return this.ᜀ;
			}
		}
		return sprᴛ.ᜁ();
	}

	// Token: 0x060033E2 RID: 13282 RVA: 0x002FA984 File Offset: 0x002F9984
	public spr\u1D30 ᜃ()
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
		return this.ᜀ.ᜀ();
	}

	// Token: 0x060033E3 RID: 13283 RVA: 0x002FA9CC File Offset: 0x002F99CC
	public void ᜁ(spr\u19E0 A_0, sprᦰ A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 27;
			for (;;)
			{
				int num2;
				int num3;
				sprᦰ sprᦰ4;
				RectangleF rectangleF;
				switch (num)
				{
				case 0:
					num = 30;
					continue;
				case 1:
				{
					sprᦰ sprᦰ;
					if (num2 >= sprᦰ.ᜊ().Count)
					{
						num = 24;
						continue;
					}
					sprᦰ.ᜊ()[num2].ᜃ(true);
					num2++;
					num = 41;
					continue;
				}
				case 2:
				{
					sprᦰ sprᦰ2;
					if (sprᦰ2.ᜊ().Count > 0)
					{
						num = 23;
						continue;
					}
					goto IL_65A;
				}
				case 3:
				{
					sprᦰ sprᦰ;
					if (num3 >= sprᦰ.ᜊ().Count)
					{
						num = 13;
						continue;
					}
					sprᦰ sprᦰ3 = sprᦰ.ᜊ()[num3 - 1];
					sprᦰ4 = sprᦰ.ᜊ()[num3];
					num = 18;
					continue;
				}
				case 4:
				{
					float width = rectangleF.Width - ((sprᦰ4.ᜋ() != 0f) ? Convert.ToSingle((float)sprᦰ4.ᜈ() * sprᦰ4.ᜌ()) : 0f);
					sprᦰ sprᦰ3;
					sprᦰ4.ᜀ(new RectangleF(sprᦰ3.ᜁ().X + sprᦰ3.ᜁ().Width, sprᦰ4.ᜁ().Y, width, sprᦰ4.ᜁ().Height));
					num = 33;
					continue;
				}
				case 5:
				{
					sprᦰ sprᦰ;
					if (sprᦰ.ᜊ()[0].ᜎ() != Spire.Layouting.HorizontalAlignment.Distributed)
					{
						num = 12;
						continue;
					}
					goto IL_65A;
				}
				case 6:
					goto IL_1F9;
				case 7:
				{
					sprᦰ sprᦰ3;
					if ((sprᦰ3.ᜂ() as Table).\u1712.TextWrappingStyle == TextWrappingStyle.Inline)
					{
						num = 29;
						continue;
					}
					goto IL_2C4;
				}
				case 8:
					num = 2;
					continue;
				case 9:
					goto IL_5EC;
				case 10:
				{
					sprᦰ sprᦰ2;
					if (sprᦰ2.ᜂ() is sprᴛ)
					{
						num = 8;
						continue;
					}
					goto IL_65A;
				}
				case 11:
				{
					int count;
					if (count > 0)
					{
						num = 38;
						continue;
					}
					goto IL_65A;
				}
				case 12:
					num = 35;
					continue;
				case 13:
					goto IL_620;
				case 14:
					goto IL_173;
				case 15:
					goto IL_EC;
				case 16:
					if ((sprᦰ4.ᜂ() as DocPicture).TextWrappingStyle == TextWrappingStyle.Inline)
					{
						num = 14;
						continue;
					}
					goto IL_2C4;
				case 17:
					if (sprᦰ4.ᜂ() is Table)
					{
						num = 34;
						continue;
					}
					goto IL_296;
				case 18:
				{
					sprᦰ sprᦰ3;
					if (sprᦰ3.ᜂ() is Table)
					{
						num = 37;
						continue;
					}
					goto IL_FF;
				}
				case 19:
				{
					sprᦰ sprᦰ3;
					if (sprᦰ3.ᜂ() is DocPicture)
					{
						num = 22;
						continue;
					}
					goto IL_3D1;
				}
				case 20:
					if (sprᦰ4.ᜂ() is DocPicture)
					{
						num = 21;
						continue;
					}
					goto IL_173;
				case 21:
					if (true)
					{
					}
					num = 16;
					continue;
				case 22:
					goto IL_2BF;
				case 23:
				{
					sprᦰ sprᦰ2;
					sprᦰ sprᦰ = sprᦰ2.ᜊ()[sprᦰ2.ᜊ().Count - 1];
					int count = sprᦰ.ᜊ().Count;
					num = 11;
					continue;
				}
				case 24:
				{
					sprᦰ sprᦰ;
					sprᦰ.ᜊ()[0].ᜀ(new RectangleF(sprᦰ.ᜊ()[0].ᜁ().X, sprᦰ.ᜊ()[0].ᜁ().Y, sprᦰ.ᜊ()[0].ᜁ().Width - Convert.ToSingle((float)sprᦰ.ᜊ()[0].ᜈ() * sprᦰ.ᜊ()[0].ᜌ()), sprᦰ.ᜊ()[0].ᜁ().Height));
					num3 = 1;
					num = 9;
					continue;
				}
				case 25:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2BF;
					default:
						if (false)
						{
						}
						goto IL_296;
					}
					break;
				case 26:
					goto IL_3D1;
				case 28:
					goto IL_5EC;
				case 29:
					goto IL_FF;
				case 30:
				{
					sprᦰ sprᦰ;
					int count;
					if (sprᦰ.ᜊ()[count - 1].ᜂ() is TextRange)
					{
						num = 15;
						continue;
					}
					goto IL_65A;
				}
				case 31:
				{
					sprᦰ sprᦰ3;
					if ((sprᦰ3.ᜂ() as DocPicture).TextWrappingStyle == TextWrappingStyle.Inline)
					{
						num = 26;
						continue;
					}
					goto IL_2C4;
				}
				case 32:
				{
					sprᦰ sprᦰ2 = A_1.ᜊ()[0];
					num = 10;
					continue;
				}
				case 33:
					goto IL_2C4;
				case 34:
					num = 40;
					continue;
				case 35:
				{
					sprᦰ sprᦰ;
					int count;
					if (!(sprᦰ.ᜊ()[count - 1].ᜂ() is spr\u208E))
					{
						num = 0;
						continue;
					}
					goto IL_EC;
				}
				case 36:
					num = 39;
					continue;
				case 37:
					num = 7;
					continue;
				case 38:
					num = 5;
					continue;
				case 39:
					if (A_1.ᜊ().Count > 0)
					{
						num = 32;
						continue;
					}
					goto IL_65A;
				case 40:
					if ((sprᦰ4.ᜂ() as Table).\u1712.TextWrappingStyle == TextWrappingStyle.Inline)
					{
						num = 25;
						continue;
					}
					goto IL_2C4;
				case 41:
					goto IL_1F9;
				}
				if (A_1.ᜂ() is sprᴛ)
				{
					num = 36;
					continue;
				}
				break;
				IL_EC:
				num2 = 0;
				num = 6;
				continue;
				IL_FF:
				num = 17;
				continue;
				IL_173:
				rectangleF = sprᦰ4.ᜁ();
				num = 4;
				continue;
				IL_1F9:
				num = 1;
				continue;
				IL_296:
				num = 19;
				continue;
				IL_2BF:
				num = 31;
				continue;
				IL_2C4:
				num3++;
				num = 28;
				continue;
				IL_3D1:
				num = 20;
				continue;
				IL_5EC:
				num = 3;
			}
			IL_620:
			IL_65A:
			this.ᜁ().ᜀ(A_0, A_1);
			return;
		}
		}
	}

	// Token: 0x060033E4 RID: 13284 RVA: 0x002FB040 File Offset: 0x002FA040
	private static void ᜀ(spr\u19E0 A_0, sprᦰ A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 21;
			for (;;)
			{
				int num3;
				switch (num)
				{
				case 0:
					goto IL_458;
				case 1:
					goto IL_293;
				case 2:
				{
					int num2;
					sprᦰ sprᦰ;
					if (num2 >= sprᦰ.ᜊ().Count)
					{
						num = 5;
						continue;
					}
					sprᦰ sprᦰ2 = sprᦰ.ᜊ()[num2 - 1];
					sprᦰ sprᦰ3 = sprᦰ.ᜊ()[num2];
					RectangleF rectangleF = sprᦰ3.ᜁ();
					num = 23;
					continue;
				}
				case 3:
				{
					sprᦰ sprᦰ4;
					if (sprᦰ4.ᜊ().Count > 0)
					{
						num = 15;
						continue;
					}
					return;
				}
				case 4:
					num = 8;
					continue;
				case 5:
					return;
				case 6:
					if (A_1.ᜊ().Count > 0)
					{
						num = 19;
						continue;
					}
					return;
				case 7:
					num = 6;
					continue;
				case 8:
				{
					sprᦰ sprᦰ;
					int count;
					if (sprᦰ.ᜊ()[count - 1].ᜂ() is TextRange)
					{
						num = 10;
						continue;
					}
					return;
				}
				case 9:
				{
					sprᦰ sprᦰ;
					int count;
					if (!(sprᦰ.ᜊ()[count - 1].ᜂ() is spr\u208E))
					{
						num = 4;
						continue;
					}
					goto IL_2C2;
				}
				case 10:
					goto IL_2C2;
				case 11:
					num = 3;
					continue;
				case 12:
				{
					int count;
					if (count > 0)
					{
						num = 24;
						continue;
					}
					return;
				}
				case 13:
					goto IL_293;
				case 14:
				{
					sprᦰ sprᦰ;
					if (sprᦰ.ᜊ()[0].ᜎ() != Spire.Layouting.HorizontalAlignment.Distributed)
					{
						num = 22;
						continue;
					}
					return;
				}
				case 15:
				{
					sprᦰ sprᦰ4;
					sprᦰ sprᦰ = sprᦰ4.ᜊ()[sprᦰ4.ᜊ().Count - 1];
					int count = sprᦰ.ᜊ().Count;
					num = 12;
					continue;
				}
				case 16:
				{
					sprᦰ sprᦰ;
					sprᦰ.ᜊ()[0].ᜀ(new RectangleF(sprᦰ.ᜊ()[0].ᜁ().X, sprᦰ.ᜊ()[0].ᜁ().Y, sprᦰ.ᜊ()[0].ᜁ().Width - Convert.ToSingle((float)sprᦰ.ᜊ()[0].ᜈ() * sprᦰ.ᜊ()[0].ᜌ()), sprᦰ.ᜊ()[0].ᜁ().Height));
					int num2 = 1;
					num = 1;
					continue;
				}
				case 17:
				{
					sprᦰ sprᦰ;
					if (num3 >= sprᦰ.ᜊ().Count)
					{
						num = 16;
						continue;
					}
					sprᦰ.ᜊ()[num3].ᜃ(true);
					num3++;
					num = 0;
					continue;
				}
				case 18:
					goto IL_458;
				case 19:
				{
					sprᦰ sprᦰ4 = A_1.ᜊ()[0];
					num = 20;
					continue;
				}
				case 20:
				{
					sprᦰ sprᦰ4;
					if (sprᦰ4.ᜂ() is sprᴛ)
					{
						num = 11;
						continue;
					}
					return;
				}
				case 22:
					if (true)
					{
					}
					num = 9;
					continue;
				case 23:
				{
					sprᦰ sprᦰ3;
					RectangleF rectangleF;
					float width = rectangleF.Width - ((sprᦰ3.ᜋ() != 0f) ? Convert.ToSingle((float)sprᦰ3.ᜈ() * sprᦰ3.ᜌ()) : 0f);
					sprᦰ sprᦰ2;
					sprᦰ3.ᜀ(new RectangleF(sprᦰ2.ᜁ().X + sprᦰ2.ᜁ().Width, sprᦰ3.ᜁ().Y, width, sprᦰ3.ᜁ().Height));
					int num2;
					num2++;
					num = 13;
					continue;
				}
				case 24:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2C2;
					default:
						if (false)
						{
						}
						num = 14;
						continue;
					}
					break;
				}
				if (A_1.ᜂ() is sprᴛ)
				{
					num = 7;
					continue;
				}
				break;
				IL_293:
				num = 2;
				continue;
				IL_2C2:
				num3 = 0;
				num = 18;
				continue;
				IL_458:
				num = 17;
			}
			return;
		}
		}
	}

	// Token: 0x060033E5 RID: 13285 RVA: 0x002FB4D4 File Offset: 0x002FA4D4
	public int ᜂ()
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
		return this.ᜀ.ᜁ() - this.ᜂ;
	}

	// Token: 0x060033E6 RID: 13286 RVA: 0x002FB524 File Offset: 0x002FA524
	public spr\u1AB8 ᜀ(int A_0)
	{
		if (A_0 == 0)
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
				return this.ᜁ;
			}
		}
		return this.ᜀ.ᜀ(A_0 + this.ᜂ);
	}

	// Token: 0x0400282B RID: 10283
	private spr\u17C8 ᜀ;

	// Token: 0x0400282C RID: 10284
	private spr\u1AB8 ᜁ;

	// Token: 0x0400282D RID: 10285
	private int ᜂ;

	// Token: 0x0400282E RID: 10286
	private int ᜃ;
}
