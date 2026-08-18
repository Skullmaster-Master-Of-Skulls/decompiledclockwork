using System;
using Spire.CompoundFile.Doc;

// Token: 0x020003FB RID: 1019
[CLSCompliant(false)]
internal class spr\u2472 : spr\u23F8
{
	// Token: 0x060038EC RID: 14572 RVA: 0x003530C8 File Offset: 0x003520C8
	internal spr\u2472()
	{
	}

	// Token: 0x060038ED RID: 14573 RVA: 0x003530DC File Offset: 0x003520DC
	internal spr\u2472(byte[] A_0, int A_1, int A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x060038EE RID: 14574 RVA: 0x003530F4 File Offset: 0x003520F4
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
		return this.ᜀ;
	}

	// Token: 0x060038EF RID: 14575 RVA: 0x00353138 File Offset: 0x00352138
	internal override int ᜇ()
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
		return this.ᜀ.Length;
	}

	// Token: 0x060038F0 RID: 14576 RVA: 0x0035317C File Offset: 0x0035217C
	internal override void ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 8;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_9D;
			case 1:
				if (true)
				{
				}
				if (A_1 > A_0.Length)
				{
					num = 11;
					continue;
				}
				num = 13;
				continue;
			case 2:
				num = 1;
				continue;
			case 3:
				goto IL_60;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1AB;
				default:
					if (false)
					{
					}
					if (this.ᜀ.Length != A_2)
					{
						num = 10;
						continue;
					}
					goto IL_1BB;
				}
				break;
			case 5:
				num = 4;
				continue;
			case 6:
				if (this.ᜀ != null)
				{
					num = 5;
					continue;
				}
				goto IL_138;
			case 7:
				num = 14;
				continue;
			case 8:
				if (A_1 >= 0)
				{
					num = 2;
					continue;
				}
				goto IL_151;
			case 10:
				goto IL_138;
			case 11:
				goto IL_197;
			case 12:
				goto IL_14F;
			case 13:
				if (A_2 >= 0)
				{
					goto IL_1AB;
				}
				goto IL_124;
			case 14:
				if (A_2 + A_1 > A_0.Length)
				{
					num = 0;
					continue;
				}
				num = 6;
				continue;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 8;
			continue;
			IL_138:
			this.ᜀ = new byte[A_2];
			num = 12;
			continue;
			IL_1AB:
			num = 7;
		}
		IL_60:
		throw new ArgumentNullException(ClipboardData.b("཭ɯqび᝵౷᭹", a_));
		IL_9D:
		IL_124:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ݭ㍯ᵱųᡵ౷", a_));
		IL_14F:
		goto IL_1BB;
		IL_151:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ݭ㽯ᑱታյᵷ๹", a_), ClipboardData.b("㡭ᅯṱų፵塷᥹ᵻၽꁿꢇ꺍ﲏ뢗ꪙ벛ﾝ캟욡蒣솥\udaa7쾩춫\udaad햯삱钳ힵ쪷좹\udfbd뒿ꏁ諅귇꓉ꯋ뫍룏", a_));
		IL_197:
		goto IL_151;
		IL_1BB:
		Array.Copy(A_0, A_1, this.ᜀ, 0, A_2);
	}

	// Token: 0x04002A98 RID: 10904
	private new byte[] ᜀ;
}
