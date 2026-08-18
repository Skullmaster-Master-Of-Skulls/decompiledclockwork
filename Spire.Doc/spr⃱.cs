using System;
using Spire.CompoundFile.Doc;
using Spire.Doc;

// Token: 0x02000346 RID: 838
internal class spr\u20F1
{
	// Token: 0x06002CC2 RID: 11458 RVA: 0x002B07AC File Offset: 0x002AF7AC
	internal short ᜁ()
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

	// Token: 0x06002CC3 RID: 11459 RVA: 0x002B07F0 File Offset: 0x002AF7F0
	internal void ᜀ(short A_0)
	{
		int a_ = 3;
		for (;;)
		{
			IL_39:
			if (true)
			{
			}
			LineSpacingRule lineSpacingRule = this.ᜂ();
			int num = 0;
			for (;;)
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
					switch (num)
					{
					case 0:
						switch (lineSpacingRule)
						{
						case LineSpacingRule.AtLeast:
							goto IL_7C;
						case LineSpacingRule.Exactly:
							goto IL_72;
						default:
							num = 1;
							continue;
						}
						break;
					case 1:
						goto IL_84;
					case 2:
						goto IL_8F;
					}
					goto IL_39;
				}
				IL_84:
				num = 2;
			}
		}
		IL_72:
		this.ᜀ = -A_0;
		return;
		IL_7C:
		this.ᜀ = A_0;
		return;
		IL_8F:
		throw new Exception(ClipboardData.b("㵨ᥪᑬٮὰᑲ啴Ͷᙸ孺๼᩾ꎂ愈ﺊﶌﾎﺐﶘ뮚쾠욢薤풦\ud9a8쪪캬욮\udfb0풲閴얶첸ힺ\ud8bc醾", a_));
	}

	// Token: 0x06002CC4 RID: 11460 RVA: 0x002B08A4 File Offset: 0x002AF8A4
	internal LineSpacingRule ᜂ()
	{
		int num = 1;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			case 1:
				goto IL_2A;
			default:
				goto IL_2A;
			}
			IL_46:
			if (this.ᜁ)
			{
				num = 3;
				continue;
			}
			num = 2;
			continue;
			goto IL_46;
			IL_2A:
			if (false)
			{
			}
			switch (num)
			{
			case 0:
				return LineSpacingRule.Exactly;
			case 2:
				if (this.ᜀ < 0)
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				return LineSpacingRule.AtLeast;
			case 3:
				return LineSpacingRule.Multiple;
			}
			goto IL_46;
		}
		return LineSpacingRule.Multiple;
	}

	// Token: 0x06002CC5 RID: 11461 RVA: 0x002B0934 File Offset: 0x002AF934
	internal void ᜀ(LineSpacingRule A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_3A;
		}
		if (false)
		{
		}
		switch (A_0)
		{
		case LineSpacingRule.AtLeast:
			this.ᜁ = false;
			this.ᜀ = Math.Abs(this.ᜀ);
			return;
		case LineSpacingRule.Exactly:
			this.ᜁ = false;
			this.ᜀ = -Math.Abs(this.ᜀ);
			return;
		case LineSpacingRule.Multiple:
			this.ᜁ = true;
			this.ᜀ = Math.Abs(this.ᜀ);
			return;
		default:
			IL_3A:
			if (true)
			{
			}
			return;
		}
	}

	// Token: 0x06002CC6 RID: 11462 RVA: 0x002B09D4 File Offset: 0x002AF9D4
	internal spr\u20F1()
	{
	}

	// Token: 0x06002CC7 RID: 11463 RVA: 0x002B09E8 File Offset: 0x002AF9E8
	internal spr\u20F1(byte[] A_0)
	{
		this.ᜀ(A_0);
	}

	// Token: 0x06002CC8 RID: 11464 RVA: 0x002B0A04 File Offset: 0x002AFA04
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
		this.ᜀ = BitConverter.ToInt16(A_0, 0);
		this.ᜁ = (BitConverter.ToInt16(A_0, 2) != 0);
	}

	// Token: 0x06002CC9 RID: 11465 RVA: 0x002B0A60 File Offset: 0x002AFA60
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
		byte[] array = new byte[4];
		BitConverter.GetBytes(this.ᜀ).CopyTo(array, 0);
		array[2] = (this.ᜁ ? 1 : 0);
		return array;
	}

	// Token: 0x04002653 RID: 9811
	private short ᜀ;

	// Token: 0x04002654 RID: 9812
	private bool ᜁ;
}
