using System;
using System.IO;
using System.Runtime.InteropServices;

// Token: 0x02000142 RID: 322
internal class sprẐ : spr\u2155
{
	// Token: 0x060007DD RID: 2013 RVA: 0x0004EF78 File Offset: 0x0004DF78
	public sprẐ(ushort A_0, ushort A_1, string A_2, Stream A_3, int A_4, int A_5) : base(A_0, A_1, A_2, A_3, A_4)
	{
		this.ᜀ = A_5;
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x0004EF9C File Offset: 0x0004DF9C
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
		sprẐ.ᜁ a_;
		a_.ᜀ = (byte)base.ᜁ();
		a_.ᜁ = (byte)base.ᜁ();
		a_.ᜂ.ᜀ = 0L;
		a_.ᜂ.ᜁ = 0L;
		a_.ᜃ = 255;
		a_.ᜄ = (uint)(base.ᜂ() + 25);
		a_.ᜅ = (uint)this.ᜀ;
		a_.ᜆ = 0U;
		a_.ᜇ = 0;
		a_.ᜈ = 0;
		a_.ᜉ = 574;
		base.ᜂ((ushort)a_.ᜀ);
		sprᮌ.ᜀ(61447, base.ᜆ(), base.ᜄ(), base.ᜅ(), A_0);
		byte[] a_2 = sprẐ.ᜁ.ᜀ(a_);
		A_0.ᜁ(a_2, base.ᜅ());
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x0004F09C File Offset: 0x0004E09C
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
		sprẐ.ᜁ a_;
		a_.ᜀ = (byte)base.ᜁ();
		a_.ᜁ = (byte)base.ᜁ();
		a_.ᜂ.ᜀ = 0L;
		a_.ᜂ.ᜁ = 0L;
		a_.ᜃ = 255;
		a_.ᜄ = (uint)(base.ᜂ() + 25);
		a_.ᜅ = (uint)this.ᜀ;
		a_.ᜆ = 0U;
		a_.ᜇ = 0;
		a_.ᜈ = 0;
		a_.ᜉ = 574;
		base.ᜂ((ushort)a_.ᜀ);
		sprᮌ.ᜀ(61447, base.ᜆ(), base.ᜄ(), base.ᜅ(), A_0, ref A_1);
		byte[] array = sprẐ.ᜁ.ᜀ(a_);
		Array.Copy(array, 0, A_0, A_1, array.Length);
		A_1 += array.Length;
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x0004F1A4 File Offset: 0x0004E1A4
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
		return sizeof(sprẐ.ᜁ) + sizeof(spr\u1CC5);
	}

	// Token: 0x04000627 RID: 1575
	private new int ᜀ;

	// Token: 0x02000143 RID: 323
	private new struct ᜀ
	{
		// Token: 0x04000628 RID: 1576
		public long ᜀ;

		// Token: 0x04000629 RID: 1577
		public long ᜁ;
	}

	// Token: 0x02000144 RID: 324
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	private struct ᜁ
	{
		// Token: 0x060007E1 RID: 2017
		[DllImport("kernel32")]
		private static extern void CopyMemory(IntPtr A_0, IntPtr A_1, int A_2);

		// Token: 0x060007E2 RID: 2018 RVA: 0x0004F1EC File Offset: 0x0004E1EC
		public unsafe static byte[] ᜀ(sprẐ.ᜁ A_0)
		{
			switch (0)
			{
			default:
			{
				byte[] array;
				for (;;)
				{
					array = new byte[sprẐ.ᜁ.ᜀ()];
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
								goto IL_AE;
							default:
								if (false)
								{
								}
								num = 4;
								continue;
							}
							break;
						case 1:
							goto IL_76;
						case 2:
							goto IL_78;
						case 3:
							goto IL_84;
						case 4:
							goto IL_AE;
						case 5:
							if (true)
							{
							}
							if ((array2 = array) != null)
							{
								num = 0;
								continue;
							}
							goto IL_78;
						}
						break;
						IL_AE:
						if (array2.Length == 0)
						{
							num = 2;
							continue;
						}
						fixed (byte* ptr = &array2[0])
						{
							num = 1;
							continue;
							IL_78:;
						}
						num = 3;
					}
				}
				IL_76:
				IL_84:
				void* value = (void*)(&A_0);
				byte* ptr;
				sprẐ.ᜁ.CopyMemory((IntPtr)((void*)ptr), (IntPtr)value, sprẐ.ᜁ.ᜀ());
				ptr = null;
				return array;
			}
			}
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0004F2DC File Offset: 0x0004E2DC
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
			return sizeof(sprẐ.ᜁ);
		}

		// Token: 0x0400062A RID: 1578
		public byte ᜀ;

		// Token: 0x0400062B RID: 1579
		public byte ᜁ;

		// Token: 0x0400062C RID: 1580
		public sprẐ.ᜀ ᜂ;

		// Token: 0x0400062D RID: 1581
		public ushort ᜃ;

		// Token: 0x0400062E RID: 1582
		public uint ᜄ;

		// Token: 0x0400062F RID: 1583
		public uint ᜅ;

		// Token: 0x04000630 RID: 1584
		public uint ᜆ;

		// Token: 0x04000631 RID: 1585
		public byte ᜇ;

		// Token: 0x04000632 RID: 1586
		public byte ᜈ;

		// Token: 0x04000633 RID: 1587
		public ushort ᜉ;
	}
}
