using System;
using System.IO;

// Token: 0x02000285 RID: 645
internal class sprᤉ
{
	// Token: 0x0600223D RID: 8765 RVA: 0x00235F60 File Offset: 0x00234F60
	internal int ᜄ()
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
		return (int)((double)this.ᜐ * this.ᜅ());
	}

	// Token: 0x0600223E RID: 8766 RVA: 0x00235FAC File Offset: 0x00234FAC
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
		return (int)((double)this.ᜏ * this.ᜆ());
	}

	// Token: 0x0600223F RID: 8767 RVA: 0x00235FF8 File Offset: 0x00234FF8
	internal double ᜆ()
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
		return (double)this.ᜑ / 1000.0;
	}

	// Token: 0x06002240 RID: 8768 RVA: 0x00236044 File Offset: 0x00235044
	internal double ᜅ()
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
		return (double)this.\u1712 / 1000.0;
	}

	// Token: 0x06002241 RID: 8769 RVA: 0x00236090 File Offset: 0x00235090
	internal spr\u224E ᜂ()
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
		return this.\u171D;
	}

	// Token: 0x06002242 RID: 8770 RVA: 0x002360D4 File Offset: 0x002350D4
	internal spr\u224E ᜃ()
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
		return this.\u171E;
	}

	// Token: 0x06002243 RID: 8771 RVA: 0x00236118 File Offset: 0x00235118
	internal spr\u224E ᜈ()
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
		return this.ᜠ;
	}

	// Token: 0x06002244 RID: 8772 RVA: 0x0023615C File Offset: 0x0023515C
	internal spr\u224E ᜇ()
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
		return this.\u171F;
	}

	// Token: 0x06002245 RID: 8773 RVA: 0x002361A0 File Offset: 0x002351A0
	internal sprᤉ()
	{
		this.ᜃ = 68;
		this.ᜑ = 1000;
		this.\u1712 = 1000;
	}

	// Token: 0x06002246 RID: 8774 RVA: 0x00236200 File Offset: 0x00235200
	internal sprᤉ(BinaryReader A_0)
	{
		this.ᜀ(A_0);
	}

	// Token: 0x06002247 RID: 8775 RVA: 0x00236248 File Offset: 0x00235248
	internal void ᜀ(BinaryReader A_0)
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
		int num = (int)A_0.BaseStream.Position;
		this.ᜂ = A_0.ReadInt32();
		this.ᜃ = A_0.ReadInt16();
		this.ᜄ = A_0.ReadInt16();
		this.ᜅ = A_0.ReadInt16();
		this.ᜆ = A_0.ReadInt16();
		this.ᜇ = A_0.ReadInt16();
		this.ᜈ = A_0.ReadInt16();
		this.ᜉ = A_0.ReadInt16();
		this.ᜊ = A_0.ReadInt16();
		this.ᜋ = A_0.ReadInt16();
		this.ᜌ = A_0.ReadInt16();
		this.\u170D = A_0.ReadInt16();
		this.ᜎ = A_0.ReadInt16();
		this.ᜏ = A_0.ReadInt16();
		this.ᜐ = A_0.ReadInt16();
		this.ᜑ = (((this.ᜑ = A_0.ReadUInt16()) == 0) ? 1000 : this.ᜑ);
		this.\u1712 = (((this.\u1712 = A_0.ReadUInt16()) == 0) ? 1000 : this.\u1712);
		this.\u1713 = A_0.ReadInt16();
		this.\u1714 = A_0.ReadInt16();
		this.\u1715 = A_0.ReadInt16();
		this.\u1716 = A_0.ReadInt16();
		int num2 = (int)A_0.ReadInt16();
		this.\u1717 = (short)(num2 & 15);
		this.\u1718 = ((num2 & 16) != 0);
		this.\u1719 = ((num2 & 32) != 0);
		this.\u171A = ((num2 & 64) != 0);
		this.\u171B = ((num2 & 128) != 0);
		this.\u171C = (short)((num2 & 65280) >> 8);
		this.\u171D.ᜀ(A_0);
		this.\u171E.ᜀ(A_0);
		this.\u171F.ᜀ(A_0);
		this.ᜠ.ᜀ(A_0);
		this.ᜡ = A_0.ReadInt16();
		this.ᜢ = A_0.ReadInt16();
		this.ᜣ = A_0.ReadInt16();
		A_0.BaseStream.Position = (long)(num + (int)this.ᜃ);
	}

	// Token: 0x06002248 RID: 8776 RVA: 0x00236494 File Offset: 0x00235494
	internal void ᜀ(Stream A_0)
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
		BinaryReader a_ = new BinaryReader(A_0);
		this.ᜀ(a_);
	}

	// Token: 0x06002249 RID: 8777 RVA: 0x002364E0 File Offset: 0x002354E0
	internal void ᜁ(Stream A_0)
	{
		BinaryWriter binaryWriter;
		int num;
		for (;;)
		{
			if (true)
			{
			}
			binaryWriter = new BinaryWriter(A_0);
			binaryWriter.Write(this.ᜂ);
			binaryWriter.Write(this.ᜃ);
			binaryWriter.Write(this.ᜄ);
			binaryWriter.Write(this.ᜅ);
			binaryWriter.Write(this.ᜆ);
			binaryWriter.Write(this.ᜇ);
			binaryWriter.Write(this.ᜈ);
			binaryWriter.Write(this.ᜉ);
			binaryWriter.Write(this.ᜊ);
			binaryWriter.Write(this.ᜋ);
			binaryWriter.Write(this.ᜌ);
			binaryWriter.Write(this.\u170D);
			binaryWriter.Write(this.ᜎ);
			binaryWriter.Write(this.ᜏ);
			binaryWriter.Write(this.ᜐ);
			binaryWriter.Write((short)this.ᜑ);
			binaryWriter.Write((short)this.\u1712);
			binaryWriter.Write(this.\u1713);
			binaryWriter.Write(this.\u1714);
			binaryWriter.Write(this.\u1715);
			binaryWriter.Write(this.\u1716);
			num = (int)this.\u1717;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_1BA;
				case 1:
					num |= (this.\u171A ? 64 : 0);
					num2 = 0;
					continue;
				case 2:
					num |= (this.\u1719 ? 32 : 0);
					goto IL_190;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_190;
					default:
						if (false)
						{
						}
						num |= (this.\u1718 ? 16 : 0);
						num2 = 2;
						continue;
					}
					break;
				}
				break;
				IL_190:
				num2 = 1;
			}
		}
		IL_1BA:
		num |= (this.\u171B ? 128 : 0);
		num |= (int)this.\u171C << 8;
		binaryWriter.Write((short)num);
		this.\u171D.ᜀ(A_0);
		this.\u171E.ᜀ(A_0);
		this.\u171F.ᜀ(A_0);
		this.ᜠ.ᜀ(A_0);
		binaryWriter.Write(this.ᜡ);
		binaryWriter.Write(this.ᜢ);
		binaryWriter.Write(this.ᜣ);
	}

	// Token: 0x0600224A RID: 8778 RVA: 0x00236728 File Offset: 0x00235728
	internal sprᤉ ᜀ()
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
		sprᤉ sprᤉ = base.MemberwiseClone() as sprᤉ;
		sprᤉ.\u171F = this.\u171F.ᜌ();
		sprᤉ.\u171E = this.\u171E.ᜌ();
		sprᤉ.ᜠ = this.ᜠ.ᜌ();
		sprᤉ.\u171D = this.\u171D.ᜌ();
		return sprᤉ;
	}

	// Token: 0x0600224B RID: 8779 RVA: 0x002367B4 File Offset: 0x002357B4
	internal void ᜀ(int A_0, int A_1, float A_2, float A_3)
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜏ = short.MaxValue;
				num = 3;
				continue;
			case 1:
				goto IL_5D;
			case 2:
				if (true)
				{
				}
				this.ᜐ = short.MaxValue;
				num = 1;
				continue;
			case 3:
				goto IL_62;
			case 4:
				goto IL_62;
			case 6:
				goto IL_A7;
			case 7:
				if (A_0 > 32767)
				{
					num = 2;
					continue;
				}
				this.ᜐ = (short)A_0;
				num = 6;
				continue;
			}
			if (A_1 <= 32767)
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
					this.ᜏ = (short)A_1;
					num = 4;
					continue;
				}
			}
			num = 0;
			continue;
			IL_62:
			num = 7;
		}
		IL_5D:
		IL_A7:
		this.ᜑ = (ushort)Math.Round((double)(A_3 * 10f));
		this.\u1712 = (ushort)Math.Round((double)(A_2 * 10f));
	}

	// Token: 0x040020E0 RID: 8416
	internal const int ᜀ = 68;

	// Token: 0x040020E1 RID: 8417
	internal const int ᜁ = 1000;

	// Token: 0x040020E2 RID: 8418
	internal int ᜂ;

	// Token: 0x040020E3 RID: 8419
	internal short ᜃ;

	// Token: 0x040020E4 RID: 8420
	internal short ᜄ;

	// Token: 0x040020E5 RID: 8421
	internal short ᜅ;

	// Token: 0x040020E6 RID: 8422
	internal short ᜆ;

	// Token: 0x040020E7 RID: 8423
	internal short ᜇ;

	// Token: 0x040020E8 RID: 8424
	internal short ᜈ;

	// Token: 0x040020E9 RID: 8425
	internal short ᜉ;

	// Token: 0x040020EA RID: 8426
	internal short ᜊ;

	// Token: 0x040020EB RID: 8427
	internal short ᜋ;

	// Token: 0x040020EC RID: 8428
	internal short ᜌ;

	// Token: 0x040020ED RID: 8429
	internal short \u170D;

	// Token: 0x040020EE RID: 8430
	internal short ᜎ;

	// Token: 0x040020EF RID: 8431
	internal short ᜏ;

	// Token: 0x040020F0 RID: 8432
	internal short ᜐ;

	// Token: 0x040020F1 RID: 8433
	internal ushort ᜑ;

	// Token: 0x040020F2 RID: 8434
	internal ushort \u1712;

	// Token: 0x040020F3 RID: 8435
	internal short \u1713;

	// Token: 0x040020F4 RID: 8436
	internal short \u1714;

	// Token: 0x040020F5 RID: 8437
	internal short \u1715;

	// Token: 0x040020F6 RID: 8438
	internal short \u1716;

	// Token: 0x040020F7 RID: 8439
	internal short \u1717;

	// Token: 0x040020F8 RID: 8440
	internal bool \u1718;

	// Token: 0x040020F9 RID: 8441
	internal bool \u1719;

	// Token: 0x040020FA RID: 8442
	internal bool \u171A;

	// Token: 0x040020FB RID: 8443
	internal bool \u171B;

	// Token: 0x040020FC RID: 8444
	internal short \u171C;

	// Token: 0x040020FD RID: 8445
	internal spr\u224E \u171D = new spr\u224E();

	// Token: 0x040020FE RID: 8446
	internal spr\u224E \u171E = new spr\u224E();

	// Token: 0x040020FF RID: 8447
	internal spr\u224E \u171F = new spr\u224E();

	// Token: 0x04002100 RID: 8448
	internal spr\u224E ᜠ = new spr\u224E();

	// Token: 0x04002101 RID: 8449
	internal short ᜡ;

	// Token: 0x04002102 RID: 8450
	internal short ᜢ;

	// Token: 0x04002103 RID: 8451
	internal short ᜣ;
}
