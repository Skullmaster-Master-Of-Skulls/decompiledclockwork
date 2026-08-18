using System;
using Spire.XLS.File;

// Token: 0x02000012 RID: 18
internal class spr\u2103 : spr\u1DEE
{
	// Token: 0x060000B7 RID: 183 RVA: 0x000087E4 File Offset: 0x000077E4
	public spr\u2103(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
		int a_ = this.ᜃ();
		this.ᜁ = base.ᜩ().ᜀ(a_);
		this.ᜁ.ᜇ();
		this.ᜁ.ᜀ(new EventHandler(this.ᜀ));
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00008838 File Offset: 0x00007838
	protected override void ᜀ(bool A_0)
	{
		for (;;)
		{
			if (!this.ᜀ)
			{
				try
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.ᜁ.ᜅ();
							num = 5;
							continue;
						case 2:
							if (this.ᜁ != null)
							{
								num = 0;
								continue;
							}
							goto IL_79;
						case 3:
							num = 2;
							continue;
						case 4:
							goto IL_88;
						case 5:
							goto IL_79;
						}
						if (A_0)
						{
							if (true)
							{
							}
							num = 3;
							continue;
						}
						IL_79:
						this.ᜀ = true;
						num = 4;
					}
					IL_88:;
				}
				finally
				{
					base.ᜀ(A_0);
				}
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_A8;
			}
		}
		IL_A8:
		if (false)
		{
		}
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00008910 File Offset: 0x00007910
	private void ᜀ(object A_0, EventArgs A_1)
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
		this.ᜁ = null;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00008954 File Offset: 0x00007954
	protected override BiffCellType ᜂ()
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
		return BiffCellType.String;
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00008990 File Offset: 0x00007990
	protected override string ᜁ()
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
		return this.ᜁ.ᜆ().ᜉ();
	}

	// Token: 0x060000BC RID: 188 RVA: 0x000089DC File Offset: 0x000079DC
	protected override void ᜀ(string A_0)
	{
		for (;;)
		{
			IL_14:
			sprặ sprặ = this.ᜁ;
			this.ᜁ = base.ᜩ().ᜀ(base.ᜩ().ᜀ(A_0));
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (sprặ != null)
					{
						num = 2;
						continue;
					}
					return;
				case 1:
					goto IL_6D;
				case 2:
					sprặ.ᜅ();
					num = 1;
					continue;
				}
				goto IL_14;
			}
			IL_6D:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_83;
			}
		}
		IL_83:
		if (false)
		{
		}
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00008A74 File Offset: 0x00007A74
	protected override object ᜀ()
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
		return this.ᜁ();
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00008AB8 File Offset: 0x00007AB8
	protected override void ᜀ(object A_0)
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
		this.ᜀ((string)A_0);
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00008B00 File Offset: 0x00007B00
	public unsafe int ᜃ()
	{
		int num = 0;
		int ᜃ;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 1:
				return ᜃ;
			case 2:
				goto IL_64;
			case 3:
				IL_3E:
				if (true)
				{
				}
				num = 5;
				continue;
			case 4:
				goto IL_88;
			case 5:
				if (array.Length == 0)
				{
					num = 2;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 6;
					continue;
					break;
				}
			case 6:
				goto IL_88;
			}
			if ((array = base.ᜢ()) != null)
			{
				num = 3;
				continue;
			}
			goto IL_64;
			IL_88:
			byte* ptr;
			ᜃ = ((sprᰕ*)ptr)->ᜃ;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3E;
			default:
				if (false)
				{
				}
				num = 1;
				continue;
			}
			IL_64:
			ptr = null;
			num = 4;
		}
		return ᜃ;
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00008BC8 File Offset: 0x00007BC8
	public unsafe void ᜀ(int A_0)
	{
		int num = 1;
		byte* ptr;
		for (;;)
		{
			byte[] array;
			switch (num)
			{
			case 0:
				goto IL_81;
			case 2:
				goto IL_74;
			case 3:
				if (true)
				{
				}
				if (array.Length == 0)
				{
					num = 2;
					continue;
				}
				fixed (byte* ptr = &array[0])
				{
					num = 4;
					continue;
					break;
				}
			case 4:
				goto IL_72;
			case 5:
				goto IL_7F;
			}
			if ((array = base.ᜢ()) != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_81;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
			}
			IL_74:
			ptr = null;
			num = 5;
			continue;
			IL_81:
			num = 3;
		}
		IL_72:
		IL_7F:
		((sprᰕ*)ptr)->ᜃ = A_0;
		ptr = null;
	}

	// Token: 0x04000022 RID: 34
	private new bool ᜀ;

	// Token: 0x04000023 RID: 35
	private new sprặ ᜁ;
}
