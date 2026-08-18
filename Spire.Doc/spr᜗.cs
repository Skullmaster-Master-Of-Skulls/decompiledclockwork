using System;
using System.IO;

// Token: 0x02000140 RID: 320
[CLSCompliant(false)]
internal class spr\u1717 : spr\u243B
{
	// Token: 0x06000851 RID: 2129 RVA: 0x0005C9FC File Offset: 0x0005B9FC
	internal spr\u1717()
	{
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x0005CA10 File Offset: 0x0005BA10
	internal spr\u1717(Stream A_0, sprᾱ A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x0005CA28 File Offset: 0x0005BA28
	protected override void ᜂ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_60:
			this.ᜀ.ᜭ((int)this.ᜆ.BaseStream.Position);
			base.ᜆ();
			this.ᜀ.អ((int)(this.ᜆ.BaseStream.Position - (long)this.ᜀ.ន()));
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
			case 0:
				if (true)
				{
				}
				break;
			case 1:
				goto IL_60;
			case 2:
				return;
			}
			if (this.ᜁ.Count <= 0)
			{
				break;
			}
			num = 1;
		}
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x0005CAEC File Offset: 0x0005BAEC
	protected override void ᜃ()
	{
		for (;;)
		{
			IL_00:
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					}
					if (true)
					{
					}
					if (false)
					{
					}
					this.ᜀ.\u173B((int)this.ᜆ.BaseStream.Position);
					this.ᜇ(this.ᜋ);
					int num2 = 0;
					int count = this.ᜄ.Count;
					num = 4;
					continue;
				}
				case 1:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					short value = this.ᜄ[num2];
					this.ᜆ.Write(value);
					num2++;
					num = 6;
					continue;
				}
				case 2:
					this.ᜀ.ᝬ((int)(this.ᜆ.BaseStream.Position - (long)this.ᜀ.\u175A()));
					num = 5;
					continue;
				case 4:
					goto IL_75;
				case 5:
					return;
				case 6:
					goto IL_75;
				}
				if (this.ᜄ.Count > 0)
				{
					num = 0;
					continue;
				}
				return;
				IL_75:
				num = 1;
			}
		}
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x0005CC2C File Offset: 0x0005BC2C
	protected override void ᜀ()
	{
		int num;
		int num2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_5C:
			this.ᜅ.BaseStream.Position = (long)this.ᜀ.ន();
			int a_ = num / 4;
			base.ᜅ(a_);
			if (true)
			{
			}
			num2 = 2;
			break;
		}
		default:
			if (false)
			{
			}
			goto IL_3A;
		}
		for (;;)
		{
			IL_28:
			switch (num2)
			{
			case 0:
				goto IL_5A;
			case 1:
				if (num > 0)
				{
					num2 = 0;
					continue;
				}
				return;
			case 2:
				return;
			}
			goto IL_3A;
		}
		IL_5A:
		goto IL_5C;
		IL_3A:
		num = this.ᜀ.ឆ();
		num2 = 1;
		goto IL_28;
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x0005CCD0 File Offset: 0x0005BCD0
	protected override void ᜁ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_5B:
			this.ᜅ.BaseStream.Position = (long)this.ᜀ.\u175A();
			this.ᜀ.\u1777();
			this.ᜅ.ReadBytes(this.ᜀ.\u1777());
			this.ᜅ.BaseStream.Position = (long)this.ᜀ.\u175A();
			base.ᜁ(this.ᜀ.\u1777(), 2);
			base.ᜁ();
			if (true)
			{
			}
			num = 0;
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
				return;
			case 1:
				goto IL_5B;
			}
			if (this.ᜀ.\u1777() <= 0)
			{
				break;
			}
			num = 1;
		}
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x0005CDC4 File Offset: 0x0005BDC4
	protected override void ᜀ(BinaryReader A_0, int A_1, int A_2)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_62:
			if (true)
			{
			}
			this.ᜄ.Add(A_0.ReadInt16());
			base.ᜀ(A_0, A_1, A_2);
			num = 1;
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
				goto IL_62;
			case 1:
				return;
			}
			if (A_0.BaseStream.Position >= A_0.BaseStream.Length)
			{
				break;
			}
			num = 0;
		}
	}
}
