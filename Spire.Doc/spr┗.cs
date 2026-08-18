using System;
using System.IO;

// Token: 0x0200035D RID: 861
internal class spr\u2517 : spr\u244A, spr\u19AD
{
	// Token: 0x06002E3C RID: 11836 RVA: 0x002C00D4 File Offset: 0x002BF0D4
	public spr\u2517(spr\u20BF A_0, spr\u2486 A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06002E3D RID: 11837 RVA: 0x002C00EC File Offset: 0x002BF0EC
	public override void ᜃ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_66:
			base.ᜃ();
			num = 0;
			break;
		default:
			if (false)
			{
			}
			num = 1;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_74;
			case 1:
				if (true)
				{
				}
				break;
			case 2:
				goto IL_64;
			}
			if (base.ᜈ().ᜌ() >= 32768U)
			{
				goto IL_76;
			}
			num = 2;
		}
		IL_64:
		goto IL_66;
		IL_74:
		IL_76:
		this.ᜁ = 0L;
	}

	// Token: 0x06002E3E RID: 11838 RVA: 0x002C0178 File Offset: 0x002BF178
	public override int ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			num = 0;
			break;
		}
		int num2;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 1:
				goto IL_82;
			case 2:
				goto IL_96;
			case 3:
				num2 = base.ᜀ(A_0, A_1, A_2);
				num = 2;
				continue;
			}
			if (base.ᜊ() != null)
			{
				num = 3;
			}
			else
			{
				num2 = base.ᜅ().ᜀ(base.ᜈ(), this.ᜁ, A_0, A_2);
				num = 1;
			}
		}
		IL_82:
		IL_96:
		this.ᜁ += (long)num2;
		return num2;
	}

	// Token: 0x06002E3F RID: 11839 RVA: 0x002C0230 File Offset: 0x002BF230
	public override void ᜁ(byte[] A_0, int A_1, int A_2)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_4E:
			if (base.ᜊ() == null)
			{
				num = 1;
			}
			else
			{
				base.ᜁ(A_0, A_1, A_2);
				num = 0;
			}
			break;
		default:
			if (false)
			{
			}
			num = 2;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (base.ᜊ().Length > 32768L)
				{
					num = 5;
					continue;
				}
				goto IL_E8;
			case 1:
				if (true)
				{
				}
				base.ᜅ().ᜀ(base.ᜈ(), this.ᜁ, A_0, A_1, A_2);
				num = 3;
				continue;
			case 3:
				goto IL_E6;
			case 4:
				goto IL_86;
			case 5:
				base.ᜅ().ᜂ(base.ᜈ(), base.ᜊ());
				base.ᜀ(null);
				num = 4;
				continue;
			}
			break;
		}
		goto IL_4E;
		IL_86:
		IL_E6:
		IL_E8:
		this.ᜁ += (long)A_2;
	}

	// Token: 0x06002E40 RID: 11840 RVA: 0x002C0334 File Offset: 0x002BF334
	public override long ᜀ(long A_0, SeekOrigin A_1)
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
					break;
				default:
					if (false)
					{
					}
					break;
				}
				base.ᜊ().Seek(A_0, A_1);
				num = 3;
				continue;
			case 2:
				goto IL_88;
			case 3:
				goto IL_C9;
			case 4:
				if (true)
				{
				}
				num = 2;
				continue;
			case 5:
				goto IL_7B;
			case 6:
				goto IL_C7;
			case 7:
				switch (A_1)
				{
				case SeekOrigin.Begin:
					this.ᜁ = A_0;
					num = 8;
					continue;
				case SeekOrigin.Current:
					this.ᜁ += A_0;
					num = 5;
					continue;
				case SeekOrigin.End:
					this.ᜁ = (long)((ulong)base.ᜈ().ᜌ() + (ulong)A_0);
					num = 6;
					continue;
				default:
					num = 4;
					continue;
				}
				break;
			case 8:
				goto IL_119;
			}
			if (base.ᜊ() != null)
			{
				num = 1;
				continue;
			}
			IL_C9:
			num = 7;
		}
		IL_7B:
		IL_88:
		IL_C7:
		IL_119:
		return this.ᜁ;
	}

	// Token: 0x06002E41 RID: 11841 RVA: 0x002C0464 File Offset: 0x002BF464
	public override void ᜁ(long A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_5C:
			base.ᜁ(A_0);
			num = 2;
			break;
		default:
			if (false)
			{
			}
			num = 0;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_52;
			case 2:
				goto IL_6B;
			}
			if (base.ᜊ() == null)
			{
				goto IL_6D;
			}
			num = 1;
		}
		IL_52:
		if (true)
		{
		}
		goto IL_5C;
		IL_6B:
		IL_6D:
		base.ᜈ().ᜀ((uint)A_0);
	}

	// Token: 0x06002E42 RID: 11842 RVA: 0x002C04EC File Offset: 0x002BF4EC
	public override long ᜂ()
	{
		if (base.ᜊ() == null)
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
				return (long)((ulong)base.ᜈ().ᜌ());
			}
		}
		return base.ᜊ().Length;
	}

	// Token: 0x06002E43 RID: 11843 RVA: 0x002C054C File Offset: 0x002BF54C
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

	// Token: 0x06002E44 RID: 11844 RVA: 0x002C0590 File Offset: 0x002BF590
	public override void ᜀ(long A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			for (;;)
			{
				IL_28:
				switch (num)
				{
				case 0:
					return;
				case 1:
					base.ᜊ().Position = A_0;
					num = 0;
					continue;
				case 2:
					if (base.ᜊ() != null)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					return;
				}
				goto IL_3A;
			}
			return;
		}
		if (false)
		{
		}
		IL_3A:
		this.ᜁ = A_0;
		num = 2;
		goto IL_28;
	}

	// Token: 0x06002E45 RID: 11845 RVA: 0x002C0618 File Offset: 0x002BF618
	public override void ᜁ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_5C:
			base.ᜁ();
			num = 0;
			break;
		default:
			if (false)
			{
			}
			if (true)
			{
			}
			num = 2;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_5A;
			}
			if (base.ᜊ() == null)
			{
				return;
			}
			num = 1;
		}
		IL_5A:
		goto IL_5C;
	}

	// Token: 0x040026B0 RID: 9904
	private new const int ᜀ = 32768;

	// Token: 0x040026B1 RID: 9905
	private new long ᜁ;
}
