using System;
using System.IO;

// Token: 0x02000497 RID: 1175
internal class spr\u2255 : spr\u1B4E, spr\u2228
{
	// Token: 0x06004855 RID: 18517 RVA: 0x002BA530 File Offset: 0x002B9530
	public spr\u2255(spr\u2604 A_0, spr\u1DAB A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06004856 RID: 18518 RVA: 0x002BA548 File Offset: 0x002B9548
	public override void ᜃ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_74;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_76;
				default:
					if (false)
					{
					}
					base.ᜃ();
					num = 0;
					continue;
				}
				break;
			}
			if (true)
			{
			}
			if (base.ᜈ().ᜌ() >= 32768U)
			{
				break;
			}
			num = 2;
		}
		IL_74:
		IL_76:
		this.ᜁ = 0L;
	}

	// Token: 0x06004857 RID: 18519 RVA: 0x002BA5D4 File Offset: 0x002B95D4
	public override int ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int num = 0;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_5C;
			case 2:
				goto IL_99;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9B;
				default:
					if (false)
					{
					}
					num2 = base.ᜀ(A_0, A_1, A_2);
					num = 2;
					continue;
				}
				break;
			}
			if (base.ᜊ() != null)
			{
				if (true)
				{
				}
				num = 3;
			}
			else
			{
				num2 = base.ᜅ().ᜀ(base.ᜈ(), this.ᜁ, A_0, A_2);
				num = 1;
			}
		}
		IL_5C:
		IL_99:
		IL_9B:
		this.ᜁ += (long)num2;
		return num2;
	}

	// Token: 0x06004858 RID: 18520 RVA: 0x002BA68C File Offset: 0x002B968C
	public override void ᜁ(byte[] A_0, int A_1, int A_2)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_6A;
			case 2:
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
					base.ᜅ().ᜀ(base.ᜈ(), this.ᜁ, A_0, A_1, A_2);
					num = 5;
					continue;
				}
				break;
			case 3:
				if (base.ᜊ().Length > 32768L)
				{
					num = 4;
					continue;
				}
				goto IL_E8;
			case 4:
				base.ᜅ().ᜂ(base.ᜈ(), base.ᜊ());
				base.ᜀ(null);
				num = 1;
				continue;
			case 5:
				goto IL_E6;
			}
			if (base.ᜊ() == null)
			{
				num = 2;
			}
			else
			{
				base.ᜁ(A_0, A_1, A_2);
				num = 3;
			}
		}
		IL_6A:
		IL_E6:
		IL_E8:
		this.ᜁ += (long)A_2;
	}

	// Token: 0x06004859 RID: 18521 RVA: 0x002BA790 File Offset: 0x002B9790
	public override long ᜀ(long A_0, SeekOrigin A_1)
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 1;
				continue;
			case 1:
				goto IL_69;
			case 2:
				switch (A_1)
				{
				case SeekOrigin.Begin:
					this.ᜁ = A_0;
					num = 6;
					continue;
				case SeekOrigin.Current:
					this.ᜁ += A_0;
					num = 8;
					continue;
				case SeekOrigin.End:
					this.ᜁ = (long)((ulong)base.ᜈ().ᜌ() + (ulong)A_0);
					num = 4;
					continue;
				default:
					num = 0;
					continue;
				}
				break;
			case 3:
				goto IL_B1;
			case 4:
				goto IL_AF;
			case 6:
				goto IL_EF;
			case 7:
				base.ᜊ().Seek(A_0, A_1);
				num = 3;
				continue;
			case 8:
				goto IL_5C;
			}
			if (base.ᜊ() != null)
			{
				num = 7;
				continue;
			}
			IL_B1:
			num = 2;
		}
		IL_5C:
		IL_69:
		IL_AF:
		goto IL_F9;
		IL_EF:
		if (true)
		{
		}
		IL_F9:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_EF;
		default:
			if (false)
			{
			}
			return this.ᜁ;
		}
	}

	// Token: 0x0600485A RID: 18522 RVA: 0x002BA8B8 File Offset: 0x002B98B8
	public override void ᜁ(long A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6D;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					base.ᜁ(A_0);
					num = 2;
					continue;
				}
				break;
			case 2:
				goto IL_6B;
			}
			if (base.ᜊ() == null)
			{
				break;
			}
			num = 0;
		}
		IL_6B:
		IL_6D:
		base.ᜈ().ᜀ((uint)A_0);
	}

	// Token: 0x0600485B RID: 18523 RVA: 0x002BA940 File Offset: 0x002B9940
	public override long ᜂ()
	{
		if (base.ᜊ() == null)
		{
			for (;;)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				break;
			}
			if (false)
			{
			}
			return (long)((ulong)base.ᜈ().ᜌ());
		}
		return base.ᜊ().Length;
	}

	// Token: 0x0600485C RID: 18524 RVA: 0x002BA9A0 File Offset: 0x002B99A0
	public override long ᜀ()
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

	// Token: 0x0600485D RID: 18525 RVA: 0x002BA9E4 File Offset: 0x002B99E4
	public override void ᜀ(long A_0)
	{
		for (;;)
		{
			this.ᜁ = A_0;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (base.ᜊ() != null)
					{
						num = 1;
						continue;
					}
					return;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						base.ᜊ().Position = A_0;
						num = 2;
						continue;
					}
					break;
				case 2:
					return;
				}
				break;
			}
		}
	}

	// Token: 0x0600485E RID: 18526 RVA: 0x002BAA6C File Offset: 0x002B9A6C
	public override void ᜁ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					base.ᜁ();
					num = 0;
					continue;
				}
				break;
			}
			if (true)
			{
			}
			if (base.ᜊ() == null)
			{
				break;
			}
			num = 2;
		}
	}

	// Token: 0x040020E0 RID: 8416
	private new const int ᜀ = 32768;

	// Token: 0x040020E1 RID: 8417
	private new long ᜁ;
}
