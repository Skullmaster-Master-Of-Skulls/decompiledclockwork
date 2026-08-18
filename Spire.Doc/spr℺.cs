using System;
using Spire.Doc.Fields.Shape;

// Token: 0x02000253 RID: 595
internal class spr\u213A
{
	// Token: 0x06001DE5 RID: 7653 RVA: 0x001D9504 File Offset: 0x001D8504
	internal spr\u213A(int A_0, int A_1, int A_2, int A_3)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
		this.ᜂ = A_2;
		this.ᜃ = A_3;
		this.ᜄ = new byte[3][];
		this.ᜆ = new byte[3][];
	}

	// Token: 0x06001DE6 RID: 7654 RVA: 0x001D954C File Offset: 0x001D854C
	internal int ᜁ()
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

	// Token: 0x06001DE7 RID: 7655 RVA: 0x001D9590 File Offset: 0x001D8590
	internal float ᜀ()
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
		return (float)this.ᜂ / (float)this.ᜁ;
	}

	// Token: 0x06001DE8 RID: 7656 RVA: 0x001D95DC File Offset: 0x001D85DC
	internal float ᜂ()
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
		return (float)this.ᜃ / (float)this.ᜁ;
	}

	// Token: 0x06001DE9 RID: 7657 RVA: 0x001D9628 File Offset: 0x001D8628
	internal void ᜀ(BorderType A_0, PageBorderArtElementPosition A_1, byte[] A_2)
	{
		switch (A_0)
		{
		case BorderType.Bottom:
			break;
		case BorderType.Left:
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
				this.ᜇ = A_2;
				return;
			}
			break;
		case BorderType.Right:
			this.ᜅ = A_2;
			return;
		case BorderType.Top:
			this.ᜄ[(int)A_1] = A_2;
			return;
		default:
			return;
		}
		this.ᜆ[(int)A_1] = A_2;
	}

	// Token: 0x06001DEA RID: 7658 RVA: 0x001D96A4 File Offset: 0x001D86A4
	internal byte[] ᜀ(BorderType A_0, PageBorderArtElementPosition A_1)
	{
		for (;;)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (A_0)
					{
					case BorderType.Bottom:
						goto IL_4F;
					case BorderType.Left:
						goto IL_3E;
					case BorderType.Right:
						goto IL_62;
					case BorderType.Top:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_58;
						default:
							goto IL_87;
						}
						break;
					default:
						num = 1;
						continue;
					}
					break;
				case 1:
					goto IL_58;
				case 2:
					goto IL_60;
				}
				break;
				IL_58:
				num = 2;
			}
		}
		IL_3E:
		return this.ᜇ;
		IL_4F:
		return this.ᜆ[(int)A_1];
		IL_60:
		return new byte[0];
		IL_62:
		if (true)
		{
		}
		return this.ᜅ;
		IL_87:
		if (false)
		{
		}
		return this.ᜄ[(int)A_1];
	}

	// Token: 0x04001F80 RID: 8064
	private int ᜀ;

	// Token: 0x04001F81 RID: 8065
	private int ᜁ;

	// Token: 0x04001F82 RID: 8066
	private int ᜂ;

	// Token: 0x04001F83 RID: 8067
	private int ᜃ;

	// Token: 0x04001F84 RID: 8068
	private byte[][] ᜄ;

	// Token: 0x04001F85 RID: 8069
	private byte[] ᜅ;

	// Token: 0x04001F86 RID: 8070
	private byte[][] ᜆ;

	// Token: 0x04001F87 RID: 8071
	private byte[] ᜇ;
}
