using System;
using System.Runtime.InteropServices;

// Token: 0x02000429 RID: 1065
[CLSCompliant(false)]
[StructLayout(LayoutKind.Sequential)]
internal class spr\u22D4 : spr\u2562
{
	// Token: 0x06003B44 RID: 15172 RVA: 0x00370748 File Offset: 0x0036F748
	public spr\u22D4(byte[] A_0, int A_1)
	{
		this.ᜁ(A_0, A_1);
	}

	// Token: 0x06003B45 RID: 15173 RVA: 0x00370764 File Offset: 0x0036F764
	public spr\u22D4()
	{
	}

	// Token: 0x06003B46 RID: 15174 RVA: 0x00370778 File Offset: 0x0036F778
	public byte ᜆ()
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

	// Token: 0x06003B47 RID: 15175 RVA: 0x003707BC File Offset: 0x0036F7BC
	public void ᜀ(byte A_0)
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

	// Token: 0x06003B48 RID: 15176 RVA: 0x00370800 File Offset: 0x0036F800
	public byte ᜄ()
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
		return this.ᜂ;
	}

	// Token: 0x06003B49 RID: 15177 RVA: 0x00370844 File Offset: 0x0036F844
	public void ᜄ(byte A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06003B4A RID: 15178 RVA: 0x00370888 File Offset: 0x0036F888
	public byte ᜅ()
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
		return this.ᜄ & 31;
	}

	// Token: 0x06003B4B RID: 15179 RVA: 0x003708D0 File Offset: 0x0036F8D0
	public void ᜃ(byte A_0)
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
		this.ᜄ &= 224;
		this.ᜄ += A_0;
	}

	// Token: 0x06003B4C RID: 15180 RVA: 0x00370930 File Offset: 0x0036F930
	public bool ᜁ()
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
		byte b = this.ᜄ & 32;
		b = (byte)(b >> 5);
		return b == 1;
	}

	// Token: 0x06003B4D RID: 15181 RVA: 0x00370980 File Offset: 0x0036F980
	public void ᜀ(bool A_0)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2B;
				default:
					goto IL_5E;
				}
				break;
			case 1:
				goto IL_2B;
			case 2:
				goto IL_35;
			}
			if (!A_0)
			{
				num = 1;
				continue;
			}
			num = 2;
			continue;
			IL_2B:
			if (true)
			{
			}
			num = 0;
		}
		IL_35:
		byte b = 1;
		goto IL_71;
		IL_5E:
		if (false)
		{
		}
		b = 0;
		IL_71:
		byte b2 = b;
		this.ᜄ &= 223;
		b2 = (byte)(b2 << 5);
		this.ᜄ += b2;
	}

	// Token: 0x06003B4E RID: 15182 RVA: 0x00370A28 File Offset: 0x0036FA28
	public byte ᜃ()
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

	// Token: 0x06003B4F RID: 15183 RVA: 0x00370A6C File Offset: 0x0036FA6C
	public void ᜁ(byte A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x06003B50 RID: 15184 RVA: 0x00370AB0 File Offset: 0x0036FAB0
	public bool ᜇ()
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
		return this.ᜁ == byte.MaxValue;
	}

	// Token: 0x06003B51 RID: 15185 RVA: 0x00370AF8 File Offset: 0x0036FAF8
	internal byte ᜂ()
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
		return this.ᜄ;
	}

	// Token: 0x06003B52 RID: 15186 RVA: 0x00370B3C File Offset: 0x0036FB3C
	internal void ᜂ(byte A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x06003B53 RID: 15187 RVA: 0x00370B80 File Offset: 0x0036FB80
	internal override int ᜀ()
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
		return 4;
	}

	// Token: 0x06003B54 RID: 15188 RVA: 0x00370BBC File Offset: 0x0036FBBC
	internal override void ᜁ(byte[] A_0, int A_1)
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
		this.ᜁ = A_0[A_1];
		this.ᜂ = A_0[A_1 + 1];
		this.ᜃ = A_0[A_1 + 2];
		this.ᜄ = A_0[A_1 + 3];
	}

	// Token: 0x06003B55 RID: 15189 RVA: 0x00370C24 File Offset: 0x0036FC24
	internal override int ᜀ(byte[] A_0, int A_1)
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
		A_0[A_1] = this.ᜁ;
		A_0[A_1 + 1] = this.ᜂ;
		A_0[A_1 + 2] = this.ᜃ;
		A_0[A_1 + 3] = this.ᜄ;
		return 4;
	}

	// Token: 0x04002B91 RID: 11153
	private new const int ᜀ = 4;

	// Token: 0x04002B92 RID: 11154
	private new byte ᜁ;

	// Token: 0x04002B93 RID: 11155
	private new byte ᜂ;

	// Token: 0x04002B94 RID: 11156
	private new byte ᜃ;

	// Token: 0x04002B95 RID: 11157
	private new byte ᜄ;
}
