using System;
using System.Collections.Generic;
using Spire.Doc.Formatting;

// Token: 0x02000286 RID: 646
internal class spr\u2123
{
	// Token: 0x0600224C RID: 8780 RVA: 0x002368D4 File Offset: 0x002358D4
	internal spr\u2123(spr\u201A A_0, sprᨽ A_1)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
	}

	// Token: 0x0600224D RID: 8781 RVA: 0x0023690C File Offset: 0x0023590C
	public byte ᜂ()
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
		return this.ᜁ.ᝈ();
	}

	// Token: 0x0600224E RID: 8782 RVA: 0x00236954 File Offset: 0x00235954
	public void ᜀ(byte A_0)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_3A;
			case 1:
				num = 4;
				continue;
			case 2:
				return;
			case 4:
				IL_85:
				if (A_0 >= 0)
				{
					num = 0;
					continue;
				}
				return;
			}
			if (A_0 >= 8)
			{
				if (true)
				{
				}
				num = 1;
				continue;
			}
			IL_3A:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_85;
			default:
				if (false)
				{
				}
				this.ᜁ.ᜂ(A_0);
				this.ᜃ = this.ᜁ.ᝈ();
				num = 2;
				break;
			}
		}
	}

	// Token: 0x0600224F RID: 8783 RVA: 0x00236A04 File Offset: 0x00235A04
	internal Dictionary<string, short> ᜅ()
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
		return this.ᜅ;
	}

	// Token: 0x06002250 RID: 8784 RVA: 0x00236A48 File Offset: 0x00235A48
	public void ᜆ()
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
		this.ᜁ.ᜊ(this.ᜀ.ᜅ());
		this.ᜂ = this.ᜁ.\u1717();
		this.ᜁ.ᜂ(0);
	}

	// Token: 0x06002251 RID: 8785 RVA: 0x00236AB8 File Offset: 0x00235AB8
	public void ᜃ()
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
		this.ᜁ.ᜊ(this.ᜂ);
		this.ᜁ.ᜂ(this.ᜃ);
	}

	// Token: 0x06002252 RID: 8786 RVA: 0x00236B18 File Offset: 0x00235B18
	public void ᜁ()
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
		this.ᜁ.ᜊ(this.ᜀ.ᜁ());
		this.ᜂ = this.ᜁ.\u1717();
		this.ᜁ.ᜂ(0);
	}

	// Token: 0x06002253 RID: 8787 RVA: 0x00236B88 File Offset: 0x00235B88
	public void ᜇ()
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
		this.ᜀ(this.ᜂ() + 1);
	}

	// Token: 0x06002254 RID: 8788 RVA: 0x00236BD4 File Offset: 0x00235BD4
	public void ᜈ()
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
		this.ᜀ(this.ᜂ() - 1);
	}

	// Token: 0x06002255 RID: 8789 RVA: 0x00236C20 File Offset: 0x00235C20
	public void ᜀ()
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
		this.ᜁ.ᜊ(0);
		this.ᜁ.ᜂ(0);
	}

	// Token: 0x06002256 RID: 8790 RVA: 0x00236C74 File Offset: 0x00235C74
	internal void ᜀ(sprហ A_0, ListFormat A_1, spr\u2305 A_2)
	{
		for (;;)
		{
			string text = A_1.LFOStyleName;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_12E;
				case 1:
					num = 8;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F2;
					default:
						goto IL_D1;
					}
					break;
				case 3:
					if (text != null)
					{
						num = 1;
						continue;
					}
					num = 5;
					continue;
				case 4:
					goto IL_152;
				case 5:
					if (this.ᜅ.ContainsKey(A_1.CustomStyleName))
					{
						num = 6;
						continue;
					}
					goto IL_154;
				case 6:
					this.ᜁ.ᜊ(this.ᜅ[A_1.CustomStyleName]);
					if (true)
					{
					}
					num = 0;
					continue;
				case 7:
					this.ᜁ.ᜊ(this.ᜄ[text]);
					num = 4;
					continue;
				case 8:
					if (this.ᜄ.ContainsKey(text))
					{
						goto IL_F2;
					}
					this.ᜁ.ᜊ(this.ᜀ.ᜀ(A_0, A_1, A_2));
					this.ᜄ.Add(text, this.ᜁ.\u1717());
					num = 2;
					continue;
				}
				break;
				IL_F2:
				num = 7;
			}
		}
		IL_D1:
		if (false)
		{
		}
		IL_12E:
		IL_152:
		IL_154:
		this.ᜁ.ᜂ((byte)A_1.ListLevelNumber);
	}

	// Token: 0x06002257 RID: 8791 RVA: 0x00236DE8 File Offset: 0x00235DE8
	internal int ᜀ(sprហ A_0, ListFormat A_1, spr\u2305 A_2, bool A_3)
	{
		short num;
		for (;;)
		{
			if (true)
			{
			}
			num = this.ᜀ.ᜁ(A_0, A_1, A_2);
			int num2 = 9;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					num2 = 5;
					continue;
				case 1:
					return (int)num;
				case 2:
					this.ᜁ.ᜊ(num);
					this.ᜁ.ᜂ((byte)A_1.ListLevelNumber);
					num2 = 6;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return (int)num;
					default:
						if (false)
						{
						}
						this.ᜄ.Add(A_1.LFOStyleName, num);
						num2 = 11;
						continue;
					}
					break;
				case 4:
					if (!this.ᜅ.ContainsKey(A_1.CustomStyleName))
					{
						num2 = 10;
						continue;
					}
					this.ᜅ[A_1.CustomStyleName] = num;
					num2 = 1;
					continue;
				case 5:
					if (!this.ᜄ.ContainsKey(A_1.LFOStyleName))
					{
						num2 = 3;
						continue;
					}
					goto IL_72;
				case 6:
					goto IL_BC;
				case 7:
					if (A_1.LFOStyleName != null)
					{
						num2 = 0;
						continue;
					}
					goto IL_72;
				case 8:
					return (int)num;
				case 9:
					if (A_3)
					{
						num2 = 2;
						continue;
					}
					goto IL_BC;
				case 10:
					this.ᜅ.Add(A_1.CustomStyleName, num);
					num2 = 8;
					continue;
				case 11:
					goto IL_72;
				}
				break;
				IL_72:
				num2 = 4;
				continue;
				IL_BC:
				num2 = 7;
			}
		}
		return (int)num;
	}

	// Token: 0x06002258 RID: 8792 RVA: 0x00236F8C File Offset: 0x00235F8C
	internal int ᜁ(sprហ A_0, ListFormat A_1, spr\u2305 A_2)
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
		short num = this.ᜀ.ᜁ(A_0, A_1, A_2);
		this.ᜅ.Add(A_1.CustomStyleName, num);
		return (int)num;
	}

	// Token: 0x06002259 RID: 8793 RVA: 0x00236FEC File Offset: 0x00235FEC
	internal void ᜀ(byte A_0, bool A_1)
	{
		int num = 1;
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
					if (A_1)
					{
						num = 2;
						continue;
					}
					goto IL_B6;
				case 2:
					this.ᜁ.ᜂ(A_0);
					num = 3;
					continue;
				case 3:
					goto IL_B6;
				case 4:
					return;
				case 5:
					goto IL_78;
				case 6:
					num = 7;
					continue;
				case 7:
					if (A_0 >= 0)
					{
						num = 5;
						continue;
					}
					return;
				}
				if (true)
				{
				}
				if (A_0 >= 8)
				{
					num = 6;
					continue;
				}
				break;
				IL_B6:
				this.ᜃ = this.ᜁ.ᝈ();
				num = 4;
				continue;
			}
			IL_78:
			num = 0;
		}
	}

	// Token: 0x0600225A RID: 8794 RVA: 0x002370D0 File Offset: 0x002360D0
	internal sprᨽ ᜄ()
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

	// Token: 0x04002104 RID: 8452
	private spr\u201A ᜀ;

	// Token: 0x04002105 RID: 8453
	private sprᨽ ᜁ;

	// Token: 0x04002106 RID: 8454
	private short ᜂ;

	// Token: 0x04002107 RID: 8455
	private byte ᜃ;

	// Token: 0x04002108 RID: 8456
	private Dictionary<string, short> ᜄ = new Dictionary<string, short>();

	// Token: 0x04002109 RID: 8457
	private Dictionary<string, short> ᜅ = new Dictionary<string, short>();
}
