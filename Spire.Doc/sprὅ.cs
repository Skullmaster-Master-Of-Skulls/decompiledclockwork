using System;
using System.Xml;
using Spire.CompoundFile.Doc;

// Token: 0x02000307 RID: 775
[CLSCompliant(false)]
internal class sprὅ
{
	// Token: 0x06002A3E RID: 10814 RVA: 0x0029F53C File Offset: 0x0029E53C
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

	// Token: 0x06002A3F RID: 10815 RVA: 0x0029F580 File Offset: 0x0029E580
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
		this.ᜀ = A_0;
	}

	// Token: 0x06002A40 RID: 10816 RVA: 0x0029F5C4 File Offset: 0x0029E5C4
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

	// Token: 0x06002A41 RID: 10817 RVA: 0x0029F608 File Offset: 0x0029E608
	internal void ᜁ(byte[] A_0)
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

	// Token: 0x06002A42 RID: 10818 RVA: 0x0029F64C File Offset: 0x0029E64C
	internal sprὅ()
	{
	}

	// Token: 0x06002A43 RID: 10819 RVA: 0x0029F660 File Offset: 0x0029E660
	internal void ᜀ(XmlReader A_0)
	{
		int a_ = 12;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜀ = Convert.FromBase64String(A_0.GetAttribute(ClipboardData.b("᝱ᩳᕵ੷͹౻੽첃잋", a_)));
		this.ᜁ = Convert.FromBase64String(A_0.GetAttribute(ClipboardData.b("᝱ᩳᕵ੷͹౻੽첃\uda8bﲏ", a_)));
	}

	// Token: 0x06002A44 RID: 10820 RVA: 0x0029F6E4 File Offset: 0x0029E6E4
	internal void ᜀ(XmlWriter A_0)
	{
		int a_ = 15;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_0.WriteStartElement(ClipboardData.b("ᅴᙶ൸᩺㑼ᅾﾊ", a_));
		A_0.WriteAttributeString(ClipboardData.b("ၴ᥶᩸ॺѼཾ쾆쒎", a_), Convert.ToBase64String(this.ᜀ));
		A_0.WriteAttributeString(ClipboardData.b("ၴ᥶᩸ॺѼཾ쾆\ud98eﾒ", a_), Convert.ToBase64String(this.ᜁ));
		A_0.WriteEndElement();
	}

	// Token: 0x040024F0 RID: 9456
	private byte[] ᜀ;

	// Token: 0x040024F1 RID: 9457
	private byte[] ᜁ;
}
