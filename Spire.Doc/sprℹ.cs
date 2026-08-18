using System;
using Spire.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x02000255 RID: 597
internal class sprℹ
{
	// Token: 0x06001DF3 RID: 7667 RVA: 0x001D99BC File Offset: 0x001D89BC
	internal sprℹ(spr\u1937 A_0)
	{
		this.ᜀ = A_0;
	}

	// Token: 0x06001DF4 RID: 7668 RVA: 0x001D99D8 File Offset: 0x001D89D8
	internal DigitalSignature ᜀ()
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
		Document document = this.ᜀ.Document;
		string a_ = (string)this.ᜀ.ᜊ(1921);
		return document.DigitalSignatures.ᜀ(a_);
	}

	// Token: 0x06001DF5 RID: 7669 RVA: 0x001D9A44 File Offset: 0x001D8A44
	internal bool ᜂ()
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
		return this.ᜀ() != null;
	}

	// Token: 0x06001DF6 RID: 7670 RVA: 0x001D9A8C File Offset: 0x001D8A8C
	internal bool ᜃ()
	{
		if (this.ᜂ())
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
				return this.ᜀ().IsValid;
			}
		}
		return false;
	}

	// Token: 0x06001DF7 RID: 7671 RVA: 0x001D9AE0 File Offset: 0x001D8AE0
	internal byte[] ᜁ()
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
					continue;
				default:
					goto IL_76;
				}
				break;
			case 2:
				goto IL_30;
			case 3:
				if (this.ᜂ())
				{
					num = 1;
					continue;
				}
				goto IL_92;
			}
			if (this.ᜃ())
			{
				num = 2;
			}
			else
			{
				num = 3;
			}
		}
		IL_30:
		return this.ᜀ().ImageBytesValid;
		IL_76:
		if (false)
		{
		}
		if (true)
		{
		}
		return this.ᜀ().ImageBytesInvalid;
		IL_92:
		return this.ᜀ.ᜮ().\u170D();
	}

	// Token: 0x04001F8B RID: 8075
	private readonly spr\u1937 ᜀ;
}
