using System;
using System.Text.RegularExpressions;
using Spire.CompoundFile.Doc;

// Token: 0x020003C8 RID: 968
internal class spr\u23E3
{
	// Token: 0x06003688 RID: 13960 RVA: 0x00330C80 File Offset: 0x0032FC80
	internal string ᜂ()
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

	// Token: 0x06003689 RID: 13961 RVA: 0x00330CC4 File Offset: 0x0032FCC4
	internal string ᜃ()
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

	// Token: 0x0600368A RID: 13962 RVA: 0x00330D08 File Offset: 0x0032FD08
	internal void ᜀ(string A_0)
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

	// Token: 0x0600368B RID: 13963 RVA: 0x00330D4C File Offset: 0x0032FD4C
	internal bool ᜁ()
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

	// Token: 0x0600368C RID: 13964 RVA: 0x00330D90 File Offset: 0x0032FD90
	private Regex ᜀ()
	{
		int a_ = 18;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 1:
				goto IL_86;
			case 2:
				if (true)
				{
				}
				this.ᜃ = new Regex(ClipboardData.b("㕷㽹⹻㥽앿쒁춃쎅쒇캉킋ﶍ뮏낑ꮓ뺕쎗쒙뺛쎝讟计蚣", a_));
				num = 1;
				continue;
			}
			if (this.ᜃ != null)
			{
				break;
			}
			num = 2;
		}
		IL_86:
		return this.ᜃ;
	}

	// Token: 0x0600368D RID: 13965 RVA: 0x00330E2C File Offset: 0x0032FE2C
	internal spr\u23E3(string A_0)
	{
		int a_ = 7;
		base..ctor();
		if (A_0 == null)
		{
			return;
		}
		if (A_0.IndexOf(ClipboardData.b("⁬⩮⍰㑲ぴㅶへ㹺ㅼ㭾", a_)) != -1)
		{
			Match match = this.ᜀ().Match(A_0);
			if (match.Groups.Count > 1)
			{
				this.ᜁ = match.Groups[1].Value;
				this.ᜀ = true;
			}
			return;
		}
		char[] separator = new char[]
		{
			'"'
		};
		string[] array = A_0.Split(separator);
		if (array.Length == 1)
		{
			this.ᜂ = A_0.Trim();
			return;
		}
		if (array.Length == 3)
		{
			this.ᜂ = array[1];
			return;
		}
		this.ᜂ = string.Empty;
	}

	// Token: 0x040029B6 RID: 10678
	private bool ᜀ;

	// Token: 0x040029B7 RID: 10679
	private string ᜁ;

	// Token: 0x040029B8 RID: 10680
	private string ᜂ;

	// Token: 0x040029B9 RID: 10681
	private Regex ᜃ;
}
