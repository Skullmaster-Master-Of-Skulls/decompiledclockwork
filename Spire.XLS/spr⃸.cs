using System;
using System.Xml;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003ED RID: 1005
internal class spr\u20F8
{
	// Token: 0x06003C79 RID: 15481 RVA: 0x0021CC48 File Offset: 0x0021BC48
	internal byte[] ᜀ()
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
		return this.ᜇ;
	}

	// Token: 0x06003C7A RID: 15482 RVA: 0x0021CC8C File Offset: 0x0021BC8C
	internal void ᜀ(byte[] A_0)
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
		this.ᜇ = A_0;
	}

	// Token: 0x06003C7B RID: 15483 RVA: 0x0021CCD0 File Offset: 0x0021BCD0
	internal spr\u20F8()
	{
		int a_ = 7;
		base..ctor();
		this.ᜀ = 16;
		this.ᜁ = 16;
		this.ᜂ = 128;
		this.ᜃ = 20;
		this.ᜄ = RecordTableEnumerator.b("簼稾ቀ", a_);
		this.ᜅ = RecordTableEnumerator.b("縼圾⁀⩂⭄⹆❈ⱊL⁎㕐㙒ᙔᕖᩘ", a_);
		this.ᜆ = RecordTableEnumerator.b("渼眾@牂", a_);
	}

	// Token: 0x06003C7C RID: 15484 RVA: 0x0021CD4C File Offset: 0x0021BD4C
	internal void ᜀ(XmlReader A_0)
	{
		int a_ = 10;
		int num = 26;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⬿❁㵃хⅇ㹉㽋", a_)))
				{
					if (true)
					{
					}
					num = 11;
					continue;
				}
				goto IL_1C5;
			case 1:
				this.ᜆ = A_0.Value;
				num = 27;
				continue;
			case 2:
				goto IL_125;
			case 3:
				goto IL_25B;
			case 4:
				goto IL_34E;
			case 5:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⌿⭁㑃⹅ⵇ㡉ോ≍㝏㵑♓㽕ⱗ㉙ㅛ", a_)))
				{
					num = 12;
					continue;
				}
				goto IL_9F;
			case 6:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⠿⍁㝃⹅ᭇ⍉㙋⭍", a_)))
				{
					num = 14;
					continue;
				}
				goto IL_34E;
			case 7:
				if (A_0.LocalName != RecordTableEnumerator.b("⬿❁㵃Ʌ⥇㹉ⵋ", a_))
				{
					num = 15;
					continue;
				}
				num = 20;
				continue;
			case 8:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⌿⭁㑃⹅ⵇ㡉ཋ♍ㅏ㭑㩓㽕㙗㵙", a_)))
				{
					num = 10;
					continue;
				}
				goto IL_125;
			case 9:
				this.ᜀ = Convert.ToInt32(A_0.Value);
				num = 18;
				continue;
			case 10:
				this.ᜅ = A_0.Value;
				num = 2;
				continue;
			case 11:
				this.ᜂ = Convert.ToInt32(A_0.Value);
				num = 17;
				continue;
			case 12:
				this.ᜄ = A_0.Value;
				num = 22;
				continue;
			case 13:
			{
				string value = A_0.Value;
				this.ᜇ = Convert.FromBase64String(value);
				num = 3;
				continue;
			}
			case 14:
				this.ᜃ = Convert.ToInt32(A_0.Value);
				num = 4;
				continue;
			case 15:
				goto IL_2F4;
			case 16:
				goto IL_2F9;
			case 17:
				goto IL_1C5;
			case 18:
				goto IL_292;
			case 19:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⠿⍁㝃⹅े♉⭋⅍≏㭑⁓㹕㕗", a_)))
				{
					num = 1;
					continue;
				}
				goto IL_178;
			case 20:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㌿⍁⡃㉅ᭇ⍉㙋⭍", a_)))
				{
					num = 9;
					continue;
				}
				goto IL_292;
			case 21:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㌿⍁⡃㉅ṇ⭉⁋㭍㕏", a_)))
				{
					goto IL_1B8;
				}
				return;
			case 22:
				goto IL_9F;
			case 23:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("∿⹁⭃╅⍇᥉╋㑍㕏", a_)))
				{
					num = 25;
					continue;
				}
				goto IL_2F9;
			case 24:
				goto IL_9A;
			case 25:
				this.ᜁ = Convert.ToInt32(A_0.Value);
				num = 16;
				continue;
			case 27:
				goto IL_178;
			}
			if (A_0 == null)
			{
				num = 24;
				continue;
			}
			num = 7;
			continue;
			IL_9F:
			num = 8;
			continue;
			IL_125:
			num = 19;
			continue;
			IL_178:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_1B8:
				num = 13;
				continue;
			default:
				if (false)
				{
				}
				num = 21;
				continue;
			}
			IL_1C5:
			num = 6;
			continue;
			IL_292:
			num = 23;
			continue;
			IL_2F9:
			num = 0;
			continue;
			IL_34E:
			num = 5;
		}
		IL_9A:
		throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
		IL_25B:
		return;
		IL_2F4:
		throw new XmlException();
	}

	// Token: 0x06003C7D RID: 15485 RVA: 0x0021D0FC File Offset: 0x0021C0FC
	internal void ᜀ(XmlWriter A_0)
	{
		int a_ = 18;
		if (A_0 == null)
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
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
			}
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("⍇⽉㕋੍ㅏ♑㕓", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("㭇⭉⁋㩍͏㭑⹓㍕", a_), this.ᜀ.ToString());
		A_0.WriteAttributeString(RecordTableEnumerator.b("⩇♉⍋ⵍ㭏ő㵓ⱕ㵗", a_), this.ᜁ.ToString());
		A_0.WriteAttributeString(RecordTableEnumerator.b("⍇⽉㕋్㥏♑❓", a_), this.ᜂ.ToString());
		A_0.WriteAttributeString(RecordTableEnumerator.b("⁇⭉㽋♍͏㭑⹓㍕", a_), this.ᜃ.ToString());
		A_0.WriteAttributeString(RecordTableEnumerator.b("⭇⍉㱋♍㕏⁑ᕓ㩕㽗㕙⹛㝝ᑟ੡ॣ", a_), this.ᜄ.ToString());
		A_0.WriteAttributeString(RecordTableEnumerator.b("⭇⍉㱋♍㕏⁑ᝓ㹕㥗㍙㉛㝝๟ա", a_), this.ᜅ.ToString());
		A_0.WriteAttributeString(RecordTableEnumerator.b("⁇⭉㽋♍ᅏ㹑㍓㥕⩗㍙⡛㙝ൟ", a_), this.ᜆ.ToString());
		A_0.WriteAttributeString(RecordTableEnumerator.b("㭇⭉⁋㩍ُ㍑㡓⍕㵗", a_), Convert.ToBase64String(this.ᜇ));
		A_0.WriteEndElement();
	}

	// Token: 0x04001A28 RID: 6696
	private int ᜀ;

	// Token: 0x04001A29 RID: 6697
	private int ᜁ;

	// Token: 0x04001A2A RID: 6698
	private int ᜂ;

	// Token: 0x04001A2B RID: 6699
	private int ᜃ;

	// Token: 0x04001A2C RID: 6700
	private string ᜄ;

	// Token: 0x04001A2D RID: 6701
	private string ᜅ;

	// Token: 0x04001A2E RID: 6702
	private string ᜆ;

	// Token: 0x04001A2F RID: 6703
	private byte[] ᜇ;
}
