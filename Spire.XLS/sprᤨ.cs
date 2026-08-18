using System;
using System.Xml;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004C7 RID: 1223
internal class spr\u1928
{
	// Token: 0x06004B49 RID: 19273 RVA: 0x002DD3FC File Offset: 0x002DC3FC
	internal byte[] ᜁ()
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
		return this.ᜀ;
	}

	// Token: 0x06004B4A RID: 19274 RVA: 0x002DD440 File Offset: 0x002DC440
	internal void ᜁ(byte[] A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x06004B4B RID: 19275 RVA: 0x002DD484 File Offset: 0x002DC484
	internal byte[] ᜀ()
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
		return this.ᜁ;
	}

	// Token: 0x06004B4C RID: 19276 RVA: 0x002DD4C8 File Offset: 0x002DC4C8
	internal void ᜀ(byte[] A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06004B4D RID: 19277 RVA: 0x002DD50C File Offset: 0x002DC50C
	internal void ᜀ(XmlReader A_0)
	{
		int a_ = 13;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.LocalName != RecordTableEnumerator.b("❂⑄㍆⡈Ɋ⍌㭎㑐㑒❔㹖ⵘ≚", a_))
				{
					num = 9;
					continue;
				}
				num = 2;
				continue;
			case 1:
				this.ᜀ = Convert.FromBase64String(A_0.Value);
				num = 5;
				continue;
			case 2:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("♂⭄⑆㭈㉊㵌㭎㑐㝒ᵔ㩖㡘㡚ᙜ㩞ᡠ", a_)))
				{
					num = 1;
					continue;
				}
				goto IL_114;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_15C;
				default:
					if (false)
					{
					}
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("♂⭄⑆㭈㉊㵌㭎㑐㝒ᵔ㩖㡘㡚ଡ଼㹞ൠᙢd", a_)))
					{
						num = 7;
						continue;
					}
					return;
				}
				break;
			case 5:
				goto IL_114;
			case 6:
				return;
			case 7:
				goto IL_15C;
			case 8:
				goto IL_4C;
			case 9:
				goto IL_D5;
			}
			if (A_0 == null)
			{
				num = 8;
				continue;
			}
			num = 0;
			continue;
			IL_114:
			num = 3;
			continue;
			IL_15C:
			this.ᜁ = Convert.FromBase64String(A_0.Value);
			num = 6;
		}
		IL_4C:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
		IL_D5:
		throw new XmlException();
	}

	// Token: 0x06004B4E RID: 19278 RVA: 0x002DD680 File Offset: 0x002DC680
	internal void ᜀ(XmlWriter A_0)
	{
		int a_ = 3;
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
			if (A_0 != null)
			{
				A_0.WriteStartElement(RecordTableEnumerator.b("崸娺䤼帾ࡀⵂㅄ≆⹈㥊⑌㭎⡐", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("尸唺帼䴾㡀㍂ㅄ≆ⵈ͊⁌⹎㉐ᡒご⹖", a_), Convert.ToBase64String(this.ᜀ));
				A_0.WriteAttributeString(RecordTableEnumerator.b("尸唺帼䴾㡀㍂ㅄ≆ⵈ͊⁌⹎㉐Ւ㑔㭖ⱘ㹚", a_), Convert.ToBase64String(this.ᜁ));
				A_0.WriteEndElement();
				return;
			}
			break;
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
	}

	// Token: 0x04002208 RID: 8712
	private byte[] ᜀ;

	// Token: 0x04002209 RID: 8713
	private byte[] ᜁ;
}
