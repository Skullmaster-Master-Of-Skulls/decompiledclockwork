using System;
using System.Drawing;
using Spire.Doc.Fields.Shape.Ps;

// Token: 0x0200043C RID: 1084
internal class spr᠐
{
	// Token: 0x06003C6B RID: 15467 RVA: 0x003871AC File Offset: 0x003861AC
	internal void \u1717()
	{
		bool flag;
		for (;;)
		{
			this.ᜈ = ConnectedBorders.None;
			flag = false;
			int num = 0;
			for (;;)
			{
				bool flag2;
				switch (num)
				{
				case 0:
					if (this.ᜉ().ᜀ(this.ᜎ(), out flag2))
					{
						num = 3;
						continue;
					}
					goto IL_DB;
				case 1:
					if (this.\u1716().ᜀ(this.ᜏ(), out flag2))
					{
						num = 12;
						continue;
					}
					goto IL_188;
				case 2:
					if (this.ᜈ != ConnectedBorders.None)
					{
						num = 16;
						continue;
					}
					num = 9;
					continue;
				case 3:
					this.ᜈ |= ConnectedBorders.Left;
					this.ᜈ |= ConnectedBorders.Top;
					flag = flag2;
					num = 14;
					continue;
				case 4:
					goto IL_9C;
				case 5:
					goto IL_1E4;
				case 6:
					goto IL_A8;
				case 7:
					if (this.ᜎ().ᜂ(this.ᜏ()))
					{
						num = 15;
						continue;
					}
					goto IL_2A9;
				case 8:
					goto IL_1F4;
				case 9:
					if (this.ᜉ().ᜂ(this.\u1716()))
					{
						num = 5;
						continue;
					}
					num = 7;
					continue;
				case 10:
					goto IL_188;
				case 11:
					if (true)
					{
					}
					if (this.ᜎ().ᜀ(this.\u1716(), out flag2))
					{
						num = 18;
						continue;
					}
					goto IL_A8;
				case 12:
					this.ᜈ |= ConnectedBorders.Right;
					this.ᜈ |= ConnectedBorders.Bottom;
					flag = flag2;
					num = 10;
					continue;
				case 13:
					if (this.ᜏ().ᜀ(this.ᜉ(), out flag2))
					{
						num = 8;
						continue;
					}
					goto IL_27B;
				case 14:
					goto IL_DB;
				case 15:
					goto IL_24B;
				case 16:
					num = 4;
					continue;
				case 17:
					goto IL_27B;
				case 18:
					this.ᜈ |= ConnectedBorders.Top;
					this.ᜈ |= ConnectedBorders.Right;
					flag = flag2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1F4;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				}
				break;
				IL_A8:
				num = 1;
				continue;
				IL_DB:
				num = 11;
				continue;
				IL_188:
				num = 13;
				continue;
				IL_1F4:
				this.ᜈ |= ConnectedBorders.Bottom;
				this.ᜈ |= ConnectedBorders.Left;
				flag = flag2;
				num = 17;
				continue;
				IL_27B:
				num = 2;
			}
		}
		IL_9C:
		this.ᜇ = (flag ? BorderConnectionType.LineStyleMirror : BorderConnectionType.Regular);
		return;
		IL_1E4:
		this.ᜇ = BorderConnectionType.HorizontalContinue;
		return;
		IL_24B:
		this.ᜇ = BorderConnectionType.VerticalContinue;
		return;
		IL_2A9:
		this.ᜇ = BorderConnectionType.None;
	}

	// Token: 0x06003C6C RID: 15468 RVA: 0x0038746C File Offset: 0x0038646C
	internal PointF \u1712()
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

	// Token: 0x06003C6D RID: 15469 RVA: 0x003874B0 File Offset: 0x003864B0
	internal void ᜀ(PointF A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x06003C6E RID: 15470 RVA: 0x003874F4 File Offset: 0x003864F4
	internal bool ᜊ()
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
		return this.ᜃ == null;
	}

	// Token: 0x06003C6F RID: 15471 RVA: 0x00387538 File Offset: 0x00386538
	internal bool ᜇ()
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
		return this.ᜄ == null;
	}

	// Token: 0x06003C70 RID: 15472 RVA: 0x0038757C File Offset: 0x0038657C
	internal bool ᜌ()
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
		return this.ᜅ == null;
	}

	// Token: 0x06003C71 RID: 15473 RVA: 0x003875C0 File Offset: 0x003865C0
	internal bool \u1715()
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
		return this.ᜆ == null;
	}

	// Token: 0x06003C72 RID: 15474 RVA: 0x00387604 File Offset: 0x00386604
	internal spr\u2587 ᜉ()
	{
		if (this.ᜊ())
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
				return spr\u2587.\u1712;
			}
		}
		return this.ᜁ;
	}

	// Token: 0x06003C73 RID: 15475 RVA: 0x00387658 File Offset: 0x00386658
	internal void ᜁ(spr\u2587 A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06003C74 RID: 15476 RVA: 0x0038769C File Offset: 0x0038669C
	internal spr\u2587 \u1716()
	{
		if (this.ᜇ())
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
				return spr\u2587.\u1712;
			}
		}
		return this.ᜄ.ᜉ();
	}

	// Token: 0x06003C75 RID: 15477 RVA: 0x003876F4 File Offset: 0x003866F4
	internal spr\u2587 ᜎ()
	{
		if (true)
		{
		}
		if (this.ᜌ())
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
				return spr\u2587.\u1712;
			}
		}
		return this.ᜂ;
	}

	// Token: 0x06003C76 RID: 15478 RVA: 0x00387748 File Offset: 0x00386748
	internal void ᜀ(spr\u2587 A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06003C77 RID: 15479 RVA: 0x0038778C File Offset: 0x0038678C
	internal spr\u2587 ᜏ()
	{
		if (this.\u1715())
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return spr\u2587.\u1712;
			}
		}
		return this.ᜆ.ᜎ();
	}

	// Token: 0x06003C78 RID: 15480 RVA: 0x003877E4 File Offset: 0x003867E4
	internal spr᠐ ᜃ()
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
		return this.ᜃ;
	}

	// Token: 0x06003C79 RID: 15481 RVA: 0x00387828 File Offset: 0x00386828
	internal void ᜀ(spr᠐ A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x06003C7A RID: 15482 RVA: 0x0038786C File Offset: 0x0038686C
	internal spr᠐ \u1713()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return this.ᜄ;
	}

	// Token: 0x06003C7B RID: 15483 RVA: 0x003878B0 File Offset: 0x003868B0
	internal void ᜁ(spr᠐ A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x06003C7C RID: 15484 RVA: 0x003878F4 File Offset: 0x003868F4
	internal spr᠐ ᜑ()
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
		return this.ᜅ;
	}

	// Token: 0x06003C7D RID: 15485 RVA: 0x00387938 File Offset: 0x00386938
	internal void ᜃ(spr᠐ A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x06003C7E RID: 15486 RVA: 0x0038797C File Offset: 0x0038697C
	internal spr᠐ ᜐ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return this.ᜆ;
	}

	// Token: 0x06003C7F RID: 15487 RVA: 0x003879C0 File Offset: 0x003869C0
	internal void ᜂ(spr᠐ A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x06003C80 RID: 15488 RVA: 0x00387A04 File Offset: 0x00386A04
	internal bool \u1714()
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
		return this.ᜇ == BorderConnectionType.LineStyleMirror;
	}

	// Token: 0x06003C81 RID: 15489 RVA: 0x00387A48 File Offset: 0x00386A48
	internal bool ᜁ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return this.ᜇ == BorderConnectionType.HorizontalContinue;
	}

	// Token: 0x06003C82 RID: 15490 RVA: 0x00387A8C File Offset: 0x00386A8C
	internal bool ᜅ()
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
		return this.ᜇ == BorderConnectionType.VerticalContinue;
	}

	// Token: 0x06003C83 RID: 15491 RVA: 0x00387AD0 File Offset: 0x00386AD0
	internal bool ᜂ()
	{
		if (this.ᜇ != BorderConnectionType.Regular)
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
				return this.ᜇ == BorderConnectionType.LineStyleMirror;
			}
		}
		if (true)
		{
		}
		return true;
	}

	// Token: 0x06003C84 RID: 15492 RVA: 0x00387B24 File Offset: 0x00386B24
	internal int ᜆ()
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				int num2;
				num2++;
				num = 2;
				continue;
			}
			case 1:
			{
				int num2;
				return num2;
			}
			case 2:
				goto IL_BA;
			case 3:
			{
				int num2;
				num2++;
				num = 7;
				continue;
			}
			case 4:
				if (this.ᜋ())
				{
					num = 13;
					continue;
				}
				goto IL_113;
			case 6:
				return 0;
			case 7:
				goto IL_DF;
			case 8:
			{
				int num2;
				num2++;
				num = 1;
				continue;
			}
			case 9:
				if (this.\u170D())
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				goto IL_DF;
			case 10:
				goto IL_113;
			case 11:
			{
				if (this.ᜄ())
				{
					num = 8;
					continue;
				}
				int num2;
				return num2;
			}
			case 12:
				if (this.ᜀ())
				{
					num = 0;
					continue;
				}
				goto IL_BA;
			case 13:
			{
				int num2;
				num2++;
				goto IL_142;
			}
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_142:
				num = 10;
				continue;
			default:
			{
				if (false)
				{
				}
				if (!this.ᜂ())
				{
					num = 6;
					continue;
				}
				int num2 = 0;
				num = 4;
				continue;
			}
			}
			IL_BA:
			num = 11;
			continue;
			IL_DF:
			num = 12;
			continue;
			IL_113:
			num = 9;
		}
		return 0;
	}

	// Token: 0x06003C85 RID: 15493 RVA: 0x00387C8C File Offset: 0x00386C8C
	internal bool ᜋ()
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
		return (this.ᜈ & ConnectedBorders.Left) != ConnectedBorders.None;
	}

	// Token: 0x06003C86 RID: 15494 RVA: 0x00387CD8 File Offset: 0x00386CD8
	internal bool \u170D()
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
		return (this.ᜈ & ConnectedBorders.Right) != ConnectedBorders.None;
	}

	// Token: 0x06003C87 RID: 15495 RVA: 0x00387D24 File Offset: 0x00386D24
	internal bool ᜀ()
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
		return (this.ᜈ & ConnectedBorders.Top) != ConnectedBorders.None;
	}

	// Token: 0x06003C88 RID: 15496 RVA: 0x00387D70 File Offset: 0x00386D70
	internal bool ᜄ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return (this.ᜈ & ConnectedBorders.Bottom) != ConnectedBorders.None;
	}

	// Token: 0x06003C89 RID: 15497 RVA: 0x00387DBC File Offset: 0x00386DBC
	internal void ᜀ(float A_0, float A_1)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜀ = new PointF(this.ᜀ.X + A_0, this.ᜀ.Y + A_1);
	}

	// Token: 0x06003C8A RID: 15498 RVA: 0x00387E1C File Offset: 0x00386E1C
	internal spr᠐ ᜁ(float A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_158:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_63;
			}
			break;
		}
		spr᠐ spr᠐;
		spr᠐ spr᠐2;
		PointF pointF;
		for (;;)
		{
			IL_2C:
			bool flag;
			bool flag2;
			switch (num)
			{
			case 0:
				spr᠐.ᜁ(spr᠐2);
				spr᠐2.ᜀ(spr᠐);
				num = 10;
				continue;
			case 1:
				num = 2;
				continue;
			case 2:
				flag = (A_0 < spr᠐2.\u1712().X);
				goto IL_178;
			case 3:
				goto IL_197;
			case 4:
				flag = true;
				goto IL_178;
			case 5:
				if (!flag2)
				{
					num = 3;
					continue;
				}
				spr᠐ = new spr᠐();
				spr᠐.ᜀ(new PointF(A_0, this.\u1712().Y));
				this.ᜁ(spr᠐);
				spr᠐.ᜀ(this);
				num = 9;
				continue;
			case 6:
				num = 8;
				continue;
			case 7:
				if (true)
				{
				}
				flag = false;
				goto IL_178;
			case 8:
				if (!this.ᜇ())
				{
					goto IL_158;
				}
				num = 4;
				continue;
			case 9:
				if (spr᠐2 != null)
				{
					num = 0;
					continue;
				}
				return spr᠐;
			case 10:
				goto IL_CF;
			case 11:
				if (pointF.X < A_0)
				{
					num = 6;
					continue;
				}
				num = 7;
				continue;
			}
			goto IL_63;
			IL_178:
			flag2 = flag;
			num = 5;
		}
		IL_CF:
		return spr᠐;
		IL_197:
		return null;
		IL_63:
		spr᠐2 = this.\u1713();
		pointF = this.\u1712();
		num = 11;
		goto IL_2C;
	}

	// Token: 0x06003C8B RID: 15499 RVA: 0x00387FC8 File Offset: 0x00386FC8
	internal spr᠐ ᜀ(float A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_74;
				case 1:
					goto IL_49;
				case 2:
				{
					bool flag;
					if (!flag)
					{
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_99;
					}
					break;
				}
				}
				if (true)
				{
				}
				if (!this.ᜊ())
				{
					num = 1;
				}
				else
				{
					bool flag = A_0 < this.\u1712().X;
					num = 2;
				}
			}
			IL_49:
			return this.ᜃ().ᜁ(A_0);
			IL_74:
			return null;
			IL_99:
			if (false)
			{
			}
			spr᠐ spr᠐ = new spr᠐();
			spr᠐.ᜀ(new PointF(A_0, this.\u1712().Y));
			this.ᜀ(spr᠐);
			spr᠐.ᜁ(this);
			return spr᠐;
		}
		}
	}

	// Token: 0x06003C8C RID: 15500 RVA: 0x003880B0 File Offset: 0x003870B0
	internal spr᠐ ᜈ()
	{
		spr᠐ result;
		for (;;)
		{
			for (;;)
			{
				spr᠐ spr᠐ = this;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (spr᠐ == null)
						{
							num = 2;
							continue;
						}
						goto IL_20;
					case 1:
						goto IL_20;
					case 2:
						goto IL_44;
					}
					break;
					IL_20:
					result = spr᠐;
					spr᠐ = spr᠐.ᜃ();
					if (true)
					{
					}
					num = 0;
				}
			}
			IL_44:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_5C;
			}
		}
		IL_5C:
		if (false)
		{
		}
		return result;
	}

	// Token: 0x04002BE6 RID: 11238
	private PointF ᜀ = PointF.Empty;

	// Token: 0x04002BE7 RID: 11239
	private spr\u2587 ᜁ;

	// Token: 0x04002BE8 RID: 11240
	private spr\u2587 ᜂ;

	// Token: 0x04002BE9 RID: 11241
	private spr᠐ ᜃ;

	// Token: 0x04002BEA RID: 11242
	private spr᠐ ᜄ;

	// Token: 0x04002BEB RID: 11243
	private spr᠐ ᜅ;

	// Token: 0x04002BEC RID: 11244
	private spr᠐ ᜆ;

	// Token: 0x04002BED RID: 11245
	private BorderConnectionType ᜇ;

	// Token: 0x04002BEE RID: 11246
	private ConnectedBorders ᜈ;
}
