using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x02000142 RID: 322
internal class sprℼ : spr\u23F8
{
	// Token: 0x0600085B RID: 2139 RVA: 0x0005CF00 File Offset: 0x0005BF00
	internal sprℼ()
	{
		this.ᜅ = new spr\u19DC();
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x0005CF20 File Offset: 0x0005BF20
	internal sprℼ(Stream A_0)
	{
		this.ᜅ = new spr\u19DC();
		this.ᜃ(A_0);
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x0005CF48 File Offset: 0x0005BF48
	internal List<object> ᜀ()
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

	// Token: 0x0600085E RID: 2142 RVA: 0x0005CF8C File Offset: 0x0005BF8C
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

	// Token: 0x0600085F RID: 2143 RVA: 0x0005CFD0 File Offset: 0x0005BFD0
	internal void ᜀ(int A_0)
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

	// Token: 0x06000860 RID: 2144 RVA: 0x0005D014 File Offset: 0x0005C014
	internal void ᜂ(Stream A_0)
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
		spr\u23F8.ᜁ(A_0, this.ᜀ);
		spr\u23F8.ᜁ(A_0, this.ᜁ);
		spr\u23F8.ᜁ(A_0, this.ᜂ);
		A_0.WriteByte((byte)this.ᜅ.Count);
		A_0.WriteByte((byte)this.ᜃ);
		spr\u23F8.ᜀ(A_0, (short)this.ᜄ);
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x0005D0A0 File Offset: 0x0005C0A0
	internal new void ᜃ(Stream A_0)
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
		long position = A_0.Position;
		this.ᜀ = spr\u23F8.ᜁ(A_0);
		this.ᜁ = spr\u23F8.ᜁ(A_0);
		this.ᜂ = spr\u23F8.ᜁ(A_0);
		this.ᜆ = A_0.ReadByte();
		this.ᜃ = A_0.ReadByte();
		this.ᜄ = (int)spr\u23F8.ᜂ(A_0);
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x0005D12C File Offset: 0x0005C12C
	internal void ᜁ(Stream A_0)
	{
		for (;;)
		{
			spr\u23F8.ᜀ(A_0, uint.MaxValue);
			int num = 0;
			int count = this.ᜅ.Count;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (true)
					{
					}
					goto IL_41;
				case 1:
					return;
				case 2:
				{
					if (num >= count)
					{
						num2 = 1;
						continue;
					}
					sprḁ sprḁ = (sprḁ)this.ᜅ[num];
					sprḁ.ᜀ(A_0);
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num2 = 3;
						continue;
					}
					break;
				}
				case 3:
					goto IL_41;
				}
				break;
				IL_41:
				num2 = 2;
			}
		}
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x0005D1E0 File Offset: 0x0005C1E0
	internal void ᜀ(Stream A_0)
	{
		for (;;)
		{
			spr\u23F8.ᜃ(A_0);
			int num = 0;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= this.ᜆ)
					{
						num2 = 2;
						continue;
					}
					this.ᜅ.Add(new sprḁ(A_0));
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num2 = 1;
						continue;
					}
					break;
				case 1:
					goto IL_33;
				case 2:
					return;
				case 3:
					if (true)
					{
					}
					goto IL_33;
				}
				break;
				IL_33:
				num2 = 0;
			}
		}
	}

	// Token: 0x0400132D RID: 4909
	private new int ᜀ;

	// Token: 0x0400132E RID: 4910
	private new int ᜁ;

	// Token: 0x0400132F RID: 4911
	private new int ᜂ;

	// Token: 0x04001330 RID: 4912
	internal new int ᜃ;

	// Token: 0x04001331 RID: 4913
	internal new int ᜄ;

	// Token: 0x04001332 RID: 4914
	private new List<object> ᜅ;

	// Token: 0x04001333 RID: 4915
	private int ᜆ;
}
