using System;

// Token: 0x02000363 RID: 867
[CLSCompliant(false)]
internal class spr\u2375
{
	// Token: 0x06002E91 RID: 11921 RVA: 0x002C267C File Offset: 0x002C167C
	internal short ᜄ()
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

	// Token: 0x06002E92 RID: 11922 RVA: 0x002C26C0 File Offset: 0x002C16C0
	internal void ᜂ(short A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x06002E93 RID: 11923 RVA: 0x002C2704 File Offset: 0x002C1704
	internal short ᜆ()
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
		return this.ᜁ;
	}

	// Token: 0x06002E94 RID: 11924 RVA: 0x002C2748 File Offset: 0x002C1748
	internal void ᜃ(short A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06002E95 RID: 11925 RVA: 0x002C278C File Offset: 0x002C178C
	internal short ᜅ()
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
		return this.ᜂ;
	}

	// Token: 0x06002E96 RID: 11926 RVA: 0x002C27D0 File Offset: 0x002C17D0
	internal void ᜁ(short A_0)
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

	// Token: 0x06002E97 RID: 11927 RVA: 0x002C2814 File Offset: 0x002C1814
	internal short ᜂ()
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

	// Token: 0x06002E98 RID: 11928 RVA: 0x002C2858 File Offset: 0x002C1858
	internal void ᜀ(short A_0)
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

	// Token: 0x06002E99 RID: 11929 RVA: 0x002C289C File Offset: 0x002C189C
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
		return (int)this.ᜄ;
	}

	// Token: 0x06002E9A RID: 11930 RVA: 0x002C28E0 File Offset: 0x002C18E0
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
		this.ᜄ = (byte)A_0;
	}

	// Token: 0x06002E9B RID: 11931 RVA: 0x002C2924 File Offset: 0x002C1924
	internal bool ᜃ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜂ == -1)
				{
					num = 1;
					continue;
				}
				return false;
			case 1:
				num = 3;
				continue;
			case 2:
				if (true)
				{
				}
				break;
			case 3:
				if (this.ᜃ == -1)
				{
					num = 4;
					continue;
				}
				return false;
			case 4:
				goto IL_8C;
			case 5:
				num = 0;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return false;
			default:
				if (false)
				{
				}
				if (this.ᜀ != -1)
				{
					return false;
				}
				num = 5;
				break;
			}
		}
		IL_8C:
		return this.ᜁ == -1;
	}

	// Token: 0x06002E9C RID: 11932 RVA: 0x002C29E4 File Offset: 0x002C19E4
	internal spr\u2375()
	{
	}

	// Token: 0x06002E9D RID: 11933 RVA: 0x002C2A14 File Offset: 0x002C1A14
	internal spr\u2375(spr\u1CC1 A_0)
	{
		this.ᜀ(A_0);
	}

	// Token: 0x06002E9E RID: 11934 RVA: 0x002C2A4C File Offset: 0x002C1A4C
	internal void ᜀ(spr\u1CC1 A_0)
	{
		for (;;)
		{
			this.ᜄ = A_0.ᜅ()[0];
			byte b = A_0.ᜅ()[2];
			short a_ = BitConverter.ToInt16(A_0.ᜅ(), 4);
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if ((b & 1) != 0)
					{
						num = 3;
						continue;
					}
					goto IL_D8;
				case 1:
					if (true)
					{
					}
					if ((b & 4) != 0)
					{
						num = 6;
						continue;
					}
					goto IL_11D;
				case 2:
					goto IL_8D;
				case 3:
					this.ᜁ(a_);
					num = 10;
					continue;
				case 4:
					return;
				case 5:
					if ((b & 8) != 0)
					{
						num = 7;
						continue;
					}
					return;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E8;
					}
					if (false)
					{
					}
					this.ᜀ(a_);
					num = 11;
					continue;
				case 7:
					this.ᜃ(a_);
					num = 4;
					continue;
				case 8:
					if ((b & 2) != 0)
					{
						goto IL_E8;
					}
					goto IL_8D;
				case 9:
					this.ᜂ(a_);
					num = 2;
					continue;
				case 10:
					goto IL_D8;
				case 11:
					goto IL_11D;
				}
				break;
				IL_8D:
				num = 1;
				continue;
				IL_D8:
				num = 8;
				continue;
				IL_E8:
				num = 9;
				continue;
				IL_11D:
				num = 5;
			}
		}
	}

	// Token: 0x06002E9F RID: 11935 RVA: 0x002C2BAC File Offset: 0x002C1BAC
	internal void ᜀ(sprḍ A_0, int A_1, int A_2)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_89;
			case 1:
				if (this.ᜁ != -1)
				{
					num = 11;
					continue;
				}
				return;
			case 3:
				if (true)
				{
				}
				goto IL_113;
			case 4:
				if (this.ᜀ != -1)
				{
					goto IL_BE;
				}
				goto IL_89;
			case 5:
				A_0.ᜆ(this.ᜀ(2, this.ᜀ, A_1));
				num = 0;
				continue;
			case 6:
				goto IL_AA;
			case 7:
				A_0.ᜆ(this.ᜀ(1, this.ᜂ, A_1));
				num = 6;
				continue;
			case 8:
				if (this.ᜃ != -1)
				{
					num = 10;
					continue;
				}
				goto IL_113;
			case 9:
				return;
			case 10:
				A_0.ᜆ(this.ᜀ(4, this.ᜃ, A_1));
				num = 3;
				continue;
			case 11:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_BE;
				default:
					if (false)
					{
					}
					A_0.ᜆ(this.ᜀ(8, this.ᜁ, A_1));
					num = 9;
					continue;
				}
				break;
			}
			if (this.ᜂ != -1)
			{
				num = 7;
				continue;
			}
			goto IL_AA;
			IL_89:
			num = 8;
			continue;
			IL_AA:
			num = 4;
			continue;
			IL_BE:
			num = 5;
			continue;
			IL_113:
			num = 1;
		}
	}

	// Token: 0x06002EA0 RID: 11936 RVA: 0x002C2D30 File Offset: 0x002C1D30
	internal spr\u2375 ᜀ()
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
		return new spr\u2375
		{
			ᜃ = this.ᜂ(),
			ᜄ = (byte)this.ᜁ(),
			ᜀ = this.ᜄ(),
			ᜁ = this.ᜆ(),
			ᜂ = this.ᜅ()
		};
	}

	// Token: 0x06002EA1 RID: 11937 RVA: 0x002C2DB0 File Offset: 0x002C1DB0
	private spr\u1CC1 ᜀ(byte A_0, short A_1, int A_2)
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
		byte[] array = new byte[6];
		array[0] = this.ᜄ;
		array[1] = this.ᜄ + 1;
		array[2] = A_0;
		array[3] = 3;
		byte[] bytes = BitConverter.GetBytes(A_1);
		bytes.CopyTo(array, 4);
		spr\u1CC1 spr_u1CC = new spr\u1CC1(A_2);
		spr_u1CC.ᜁ(array);
		return spr_u1CC;
	}

	// Token: 0x040026CE RID: 9934
	private short ᜀ = -1;

	// Token: 0x040026CF RID: 9935
	private short ᜁ = -1;

	// Token: 0x040026D0 RID: 9936
	private short ᜂ = -1;

	// Token: 0x040026D1 RID: 9937
	private short ᜃ = -1;

	// Token: 0x040026D2 RID: 9938
	private byte ᜄ;
}
