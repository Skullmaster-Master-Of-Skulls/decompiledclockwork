using System;
using System.Runtime.InteropServices;

// Token: 0x02000076 RID: 118
internal class spr\u1D78 : sprᠺ
{
	// Token: 0x060003B0 RID: 944 RVA: 0x00022BD8 File Offset: 0x00021BD8
	public spr\u1D78(ushort A_0, ushort A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x00022BF0 File Offset: 0x00021BF0
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
		spr\u1D78.ᜀ a_;
		a_.ᜀ = 134217741U;
		a_.ᜁ = 134217740U;
		a_.ᜂ = 134217751U;
		a_.ᜃ = 268435703U;
		sprᮌ.ᜀ(61726, base.ᜆ(), base.ᜄ(), this.ᜀ() - sizeof(spr\u1CC5), A_0);
		byte[] array = spr\u1D78.ᜀ.ᜀ(a_);
		A_0.ᜁ(array, array.Length);
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x00022C94 File Offset: 0x00021C94
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
		spr\u1D78.ᜀ a_;
		a_.ᜀ = 134217741U;
		a_.ᜁ = 134217740U;
		a_.ᜂ = 134217751U;
		a_.ᜃ = 268435703U;
		sprᮌ.ᜀ(61726, base.ᜆ(), base.ᜄ(), this.ᜀ() - sizeof(spr\u1CC5), A_0, ref A_1);
		byte[] array = spr\u1D78.ᜀ.ᜀ(a_);
		Array.Copy(array, 0, A_0, A_1, array.Length);
		A_1 += array.Length;
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00022D40 File Offset: 0x00021D40
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
		return sizeof(spr\u1D78.ᜀ) + sizeof(spr\u1CC5);
	}

	// Token: 0x02000077 RID: 119
	private new struct ᜀ
	{
		// Token: 0x060003B4 RID: 948
		[DllImport("kernel32")]
		private static extern void CopyMemory(IntPtr A_0, IntPtr A_1, int A_2);

		// Token: 0x060003B5 RID: 949 RVA: 0x00022D88 File Offset: 0x00021D88
		public unsafe static byte[] ᜀ(spr\u1D78.ᜀ A_0)
		{
			switch (0)
			{
			default:
			{
				byte[] array;
				byte* ptr;
				for (;;)
				{
					array = new byte[spr\u1D78.ᜀ.ᜀ()];
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							byte[] array2;
							if (array2.Length == 0)
							{
								num = 3;
								continue;
							}
							fixed (byte* ptr = &array2[0])
							{
								num = 5;
								continue;
								break;
							}
						}
						case 1:
						{
							if (true)
							{
							}
							byte[] array2;
							if ((array2 = array) != null)
							{
								num = 4;
								continue;
							}
							goto IL_6E;
						}
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							}
							goto Block_2;
						case 3:
							goto IL_6E;
						case 4:
							num = 0;
							continue;
						case 5:
							goto IL_6C;
						}
						break;
						IL_6E:
						ptr = null;
						num = 2;
					}
				}
				IL_6C:
				goto IL_C1;
				Block_2:
				if (false)
				{
				}
				IL_C1:
				void* value = (void*)(&A_0);
				spr\u1D78.ᜀ.CopyMemory((IntPtr)((void*)ptr), (IntPtr)value, spr\u1D78.ᜀ.ᜀ());
				ptr = null;
				return array;
			}
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00022E78 File Offset: 0x00021E78
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
			return sizeof(spr\u1D78.ᜀ);
		}

		// Token: 0x04000279 RID: 633
		public uint ᜀ;

		// Token: 0x0400027A RID: 634
		public uint ᜁ;

		// Token: 0x0400027B RID: 635
		public uint ᜂ;

		// Token: 0x0400027C RID: 636
		public uint ᜃ;
	}
}
