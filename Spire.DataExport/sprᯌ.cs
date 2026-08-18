using System;
using System.Runtime.InteropServices;

// Token: 0x0200011D RID: 285
internal class sprᯌ : sprᠺ
{
	// Token: 0x060006A9 RID: 1705 RVA: 0x0003FAA4 File Offset: 0x0003EAA4
	public sprᯌ(ushort A_0, ushort A_1, int A_2) : base(A_0, A_1)
	{
		this.ᜀ = A_2;
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x0003FAC0 File Offset: 0x0003EAC0
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
		sprᯌ.ᜁ a_;
		a_.ᜀ = (uint)(1025 + this.ᜀ);
		a_.ᜁ = 2U;
		a_.ᜂ = (uint)(this.ᜀ + 1);
		a_.ᜃ = 1U;
		a_.ᜄ.ᜀ = 1U;
		a_.ᜄ.ᜁ = (uint)(this.ᜀ + 1);
		sprᮌ.ᜀ(61446, base.ᜆ(), base.ᜄ(), base.ᜅ(), A_0);
		byte[] a_2 = sprᯌ.ᜁ.ᜀ(a_);
		A_0.ᜁ(a_2, base.ᜅ());
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x0003FB80 File Offset: 0x0003EB80
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
		sprᯌ.ᜁ a_;
		a_.ᜀ = (uint)(1025 + this.ᜀ);
		a_.ᜁ = 2U;
		a_.ᜂ = (uint)(this.ᜀ + 1);
		a_.ᜃ = 1U;
		a_.ᜄ.ᜀ = 1U;
		a_.ᜄ.ᜁ = (uint)(this.ᜀ + 1);
		sprᮌ.ᜀ(61446, base.ᜆ(), base.ᜄ(), base.ᜅ(), A_0, ref A_1);
		byte[] sourceArray = sprᯌ.ᜁ.ᜀ(a_);
		Array.Copy(sourceArray, 0, A_0, A_1, base.ᜅ());
		A_1 += base.ᜅ();
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x0003FC50 File Offset: 0x0003EC50
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
		return sizeof(sprᯌ.ᜁ) + sizeof(spr\u1CC5);
	}

	// Token: 0x040005B3 RID: 1459
	private new int ᜀ;

	// Token: 0x0200011E RID: 286
	private new struct ᜀ
	{
		// Token: 0x040005B4 RID: 1460
		public uint ᜀ;

		// Token: 0x040005B5 RID: 1461
		public uint ᜁ;
	}

	// Token: 0x0200011F RID: 287
	private struct ᜁ
	{
		// Token: 0x060006AD RID: 1709
		[DllImport("kernel32")]
		private static extern void CopyMemory(IntPtr A_0, IntPtr A_1, int A_2);

		// Token: 0x060006AE RID: 1710 RVA: 0x0003FC98 File Offset: 0x0003EC98
		public unsafe static byte[] ᜀ(sprᯌ.ᜁ A_0)
		{
			switch (0)
			{
			default:
			{
				byte[] array;
				byte* ptr;
				for (;;)
				{
					array = new byte[sprᯌ.ᜁ.ᜀ()];
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_88;
						case 1:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_88;
							default:
							{
								if (false)
								{
								}
								byte[] array2;
								if ((array2 = array) != null)
								{
									num = 5;
									continue;
								}
								goto IL_8A;
							}
							}
							break;
						case 2:
							goto IL_8A;
						case 3:
						{
							byte[] array2;
							if (array2.Length == 0)
							{
								num = 2;
								continue;
							}
							fixed (byte* ptr = &array2[0])
							{
								num = 0;
								continue;
								break;
							}
						}
						case 4:
							goto IL_99;
						case 5:
							num = 3;
							continue;
						}
						break;
						IL_8A:
						ptr = null;
						num = 4;
					}
				}
				IL_88:
				IL_99:
				void* value = (void*)(&A_0);
				sprᯌ.ᜁ.CopyMemory((IntPtr)((void*)ptr), (IntPtr)value, sprᯌ.ᜁ.ᜀ());
				ptr = null;
				return array;
			}
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0003FD88 File Offset: 0x0003ED88
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
			return sizeof(sprᯌ.ᜁ);
		}

		// Token: 0x040005B6 RID: 1462
		public uint ᜀ;

		// Token: 0x040005B7 RID: 1463
		public uint ᜁ;

		// Token: 0x040005B8 RID: 1464
		public uint ᜂ;

		// Token: 0x040005B9 RID: 1465
		public uint ᜃ;

		// Token: 0x040005BA RID: 1466
		public sprᯌ.ᜀ ᜄ;
	}
}
