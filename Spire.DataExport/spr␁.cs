using System;
using System.Runtime.InteropServices;

// Token: 0x02000132 RID: 306
internal class spr\u2401 : sprᠺ
{
	// Token: 0x0600077B RID: 1915 RVA: 0x0004BCDC File Offset: 0x0004ACDC
	public spr\u2401(ushort A_0, ushort A_1, ushort A_2, ushort A_3) : base(A_0, A_1)
	{
		this.ᜀ = A_2;
		this.ᜁ = A_3;
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x0004BD00 File Offset: 0x0004AD00
	public override void ᜀ(sprḗ A_0)
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
		spr\u2401.ᜀ a_;
		a_.ᜀ = (uint)this.ᜀ;
		a_.ᜁ = (uint)this.ᜁ;
		sprᮌ.ᜀ(61450, base.ᜆ(), base.ᜄ(), base.ᜅ(), A_0);
		byte[] a_2 = spr\u2401.ᜀ.ᜀ(a_);
		A_0.ᜁ(a_2, base.ᜅ());
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x0004BD88 File Offset: 0x0004AD88
	public override void ᜀ(byte[] A_0, ref int A_1)
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
		spr\u2401.ᜀ a_;
		a_.ᜀ = (uint)this.ᜀ;
		a_.ᜁ = (uint)this.ᜁ;
		sprᮌ.ᜀ(61450, base.ᜆ(), base.ᜄ(), base.ᜅ(), A_0, ref A_1);
		byte[] sourceArray = spr\u2401.ᜀ.ᜀ(a_);
		Array.Copy(sourceArray, 0, A_0, A_1, base.ᜅ());
		A_1 += base.ᜅ();
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x0004BE20 File Offset: 0x0004AE20
	public override int ᜀ()
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
		return sizeof(spr\u2401.ᜀ) + sizeof(spr\u1CC5);
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x0004BE68 File Offset: 0x0004AE68
	public ushort ᜁ()
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
		return this.ᜀ;
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x0004BEAC File Offset: 0x0004AEAC
	public void ᜁ(ushort A_0)
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

	// Token: 0x06000781 RID: 1921 RVA: 0x0004BEF0 File Offset: 0x0004AEF0
	public ushort ᜂ()
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

	// Token: 0x06000782 RID: 1922 RVA: 0x0004BF34 File Offset: 0x0004AF34
	public void ᜀ(ushort A_0)
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

	// Token: 0x040005F2 RID: 1522
	private new ushort ᜀ;

	// Token: 0x040005F3 RID: 1523
	private ushort ᜁ;

	// Token: 0x02000133 RID: 307
	private new struct ᜀ
	{
		// Token: 0x06000783 RID: 1923
		[DllImport("kernel32")]
		private static extern void CopyMemory(IntPtr A_0, IntPtr A_1, int A_2);

		// Token: 0x06000784 RID: 1924 RVA: 0x0004BF78 File Offset: 0x0004AF78
		public unsafe static byte[] ᜀ(spr\u2401.ᜀ A_0)
		{
			switch (0)
			{
			default:
			{
				byte[] array;
				for (;;)
				{
					array = new byte[spr\u2401.ᜀ.ᜀ()];
					int num = 5;
					for (;;)
					{
						byte[] array2;
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_65;
							default:
								if (false)
								{
								}
								goto IL_78;
							}
							break;
						case 1:
							num = 3;
							continue;
						case 2:
							goto IL_84;
						case 3:
							if (array2.Length == 0)
							{
								num = 0;
								continue;
							}
							goto IL_65;
						case 4:
							goto IL_76;
						case 5:
							if (true)
							{
							}
							if ((array2 = array) != null)
							{
								num = 1;
								continue;
							}
							goto IL_78;
						}
						break;
						IL_65:
						fixed (byte* ptr = &array2[0])
						{
							num = 4;
							continue;
							IL_78:;
						}
						num = 2;
					}
				}
				IL_76:
				IL_84:
				void* value = (void*)(&A_0);
				byte* ptr;
				spr\u2401.ᜀ.CopyMemory((IntPtr)((void*)ptr), (IntPtr)value, spr\u2401.ᜀ.ᜀ());
				ptr = null;
				return array;
			}
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0004C064 File Offset: 0x0004B064
		public static int ᜀ()
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
			return sizeof(spr\u2401.ᜀ);
		}

		// Token: 0x040005F4 RID: 1524
		public uint ᜀ;

		// Token: 0x040005F5 RID: 1525
		public uint ᜁ;
	}
}
