using System;
using System.IO;

// Token: 0x020003C4 RID: 964
internal class sprḁ : spr\u23F8
{
	// Token: 0x06003670 RID: 13936 RVA: 0x0032F384 File Offset: 0x0032E384
	internal sprḁ(bool A_0)
	{
		if (A_0)
		{
			this.ᜇ = new spr\u225B();
		}
	}

	// Token: 0x06003671 RID: 13937 RVA: 0x0032F3AC File Offset: 0x0032E3AC
	internal sprḁ(Stream A_0)
	{
		this.ᜀ = (int)spr\u23F8.ᜃ(A_0);
		int num = A_0.ReadByte();
		this.ᜁ = (num & 15);
		this.ᜂ = ((num & 16) != 0);
		this.ᜃ = ((num & 32) != 0);
		this.ᜄ = A_0.ReadByte();
		this.ᜅ = A_0.ReadByte();
		this.ᜆ = A_0.ReadByte();
		if (this.ᜃ)
		{
			this.ᜇ = new spr\u225B(A_0);
		}
	}

	// Token: 0x06003672 RID: 13938 RVA: 0x0032F438 File Offset: 0x0032E438
	internal void ᜀ(Stream A_0)
	{
		for (;;)
		{
			spr\u23F8.ᜀ(A_0, (uint)this.ᜀ);
			int num = 0;
			num |= this.ᜁ;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (this.ᜃ)
					{
						num2 = 4;
						continue;
					}
					return;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C2;
					default:
						goto IL_F9;
					}
					break;
				case 2:
					num |= (this.ᜂ ? 16 : 0);
					if (true)
					{
					}
					num2 = 3;
					continue;
				case 3:
					num |= (this.ᜃ ? 32 : 0);
					A_0.WriteByte((byte)num);
					A_0.WriteByte((byte)this.ᜄ);
					A_0.WriteByte((byte)this.ᜅ);
					A_0.WriteByte((byte)this.ᜆ);
					num2 = 0;
					continue;
				case 4:
					goto IL_C2;
				}
				break;
				IL_C2:
				this.ᜇ.ᜁ(A_0);
				num2 = 1;
			}
		}
		IL_F9:
		if (false)
		{
		}
	}

	// Token: 0x040029A8 RID: 10664
	internal new int ᜀ;

	// Token: 0x040029A9 RID: 10665
	internal new int ᜁ;

	// Token: 0x040029AA RID: 10666
	internal new bool ᜂ;

	// Token: 0x040029AB RID: 10667
	internal new bool ᜃ;

	// Token: 0x040029AC RID: 10668
	internal new int ᜄ;

	// Token: 0x040029AD RID: 10669
	internal new int ᜅ;

	// Token: 0x040029AE RID: 10670
	internal int ᜆ;

	// Token: 0x040029AF RID: 10671
	internal spr\u225B ᜇ;
}
