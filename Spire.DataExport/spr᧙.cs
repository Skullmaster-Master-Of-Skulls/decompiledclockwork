using System;
using System.Runtime.InteropServices;

// Token: 0x02000088 RID: 136
internal class spr᧙ : sprᠺ
{
	// Token: 0x0600041F RID: 1055 RVA: 0x000281C0 File Offset: 0x000271C0
	public spr᧙(ushort A_0, ushort A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x000281D8 File Offset: 0x000271D8
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
		spr᧙.ᜀ a_;
		a_.ᜀ = 2U;
		a_.ᜁ = 1025U;
		sprᮌ.ᜀ(61448, base.ᜆ(), base.ᜄ(), base.ᜅ(), A_0);
		byte[] a_2 = spr᧙.ᜀ.ᜀ(a_);
		A_0.ᜁ(a_2, base.ᜅ());
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x0002825C File Offset: 0x0002725C
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
		spr᧙.ᜀ a_;
		a_.ᜀ = 2U;
		a_.ᜁ = 1025U;
		sprᮌ.ᜀ(61448, base.ᜆ(), base.ᜄ(), base.ᜅ(), A_0, ref A_1);
		byte[] sourceArray = spr᧙.ᜀ.ᜀ(a_);
		Array.Copy(sourceArray, 0, A_0, A_1, base.ᜅ());
		A_1 += base.ᜅ();
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x000282EC File Offset: 0x000272EC
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
		return sizeof(spr᧙.ᜀ) + sizeof(spr\u1CC5);
	}

	// Token: 0x02000089 RID: 137
	private new struct ᜀ
	{
		// Token: 0x06000423 RID: 1059
		[DllImport("kernel32")]
		private static extern void CopyMemory(IntPtr A_0, IntPtr A_1, int A_2);

		// Token: 0x06000424 RID: 1060 RVA: 0x00028334 File Offset: 0x00027334
		public unsafe static byte[] ᜀ(spr᧙.ᜀ A_0)
		{
			byte[] array;
			byte* ptr;
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
					for (;;)
					{
						array = new byte[spr᧙.ᜀ.ᜀ()];
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								if (true)
								{
								}
								byte[] array2;
								if ((array2 = array) != null)
								{
									num = 2;
									continue;
								}
								goto IL_94;
							}
							case 1:
								goto IL_94;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_00;
								default:
									if (false)
									{
									}
									num = 3;
									continue;
								}
								break;
							case 3:
							{
								byte[] array2;
								if (array2.Length == 0)
								{
									num = 1;
									continue;
								}
								fixed (byte* ptr = &array2[0])
								{
									num = 5;
									continue;
									break;
								}
							}
							case 4:
								goto IL_A3;
							case 5:
								goto IL_92;
							}
							break;
							IL_94:
							ptr = null;
							num = 4;
						}
					}
					break;
				}
			}
			IL_92:
			IL_A3:
			void* value = (void*)(&A_0);
			spr᧙.ᜀ.CopyMemory((IntPtr)((void*)ptr), (IntPtr)value, spr᧙.ᜀ.ᜀ());
			ptr = null;
			return array;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00028424 File Offset: 0x00027424
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
			return sizeof(spr᧙.ᜀ);
		}

		// Token: 0x04000295 RID: 661
		public uint ᜀ;

		// Token: 0x04000296 RID: 662
		public uint ᜁ;
	}
}
