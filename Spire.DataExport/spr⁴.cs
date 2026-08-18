using System;
using System.Runtime.InteropServices;

// Token: 0x020000E7 RID: 231
internal abstract class spr\u2074
{
	// Token: 0x060004DB RID: 1243 RVA: 0x00030214 File Offset: 0x0002F214
	static spr\u2074()
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
		spr\u2074.ᜀ = new spr\u2074.ᜀ[16];
		spr\u2074.ᜀ[0] = new spr\u2074.ᜀ(0, 0, 65525, 32, 0, 0, 0, 0, 8384);
		spr\u2074.ᜀ[1] = new spr\u2074.ᜀ(1, 0, 65525, 32, 62464, 0, 0, 0, 8384);
		spr\u2074.ᜀ[2] = new spr\u2074.ᜀ(1, 0, 65525, 32, 62464, 0, 0, 0, 8384);
		spr\u2074.ᜀ[3] = new spr\u2074.ᜀ(2, 0, 65525, 32, 62464, 0, 0, 0, 8384);
		spr\u2074.ᜀ[4] = new spr\u2074.ᜀ(2, 0, 65525, 32, 62464, 0, 0, 0, 8384);
		spr\u2074.ᜀ[5] = new spr\u2074.ᜀ(0, 0, 65525, 32, 62464, 0, 0, 0, 8384);
		spr\u2074.ᜀ[6] = new spr\u2074.ᜀ(0, 0, 65525, 32, 62464, 0, 0, 0, 8384);
		spr\u2074.ᜀ[7] = new spr\u2074.ᜀ(0, 0, 65525, 32, 62464, 0, 0, 0, 8384);
		spr\u2074.ᜀ[8] = new spr\u2074.ᜀ(0, 0, 65525, 32, 62464, 0, 0, 0, 8384);
		spr\u2074.ᜀ[9] = new spr\u2074.ᜀ(0, 0, 65525, 32, 62464, 0, 0, 0, 8384);
		spr\u2074.ᜀ[10] = new spr\u2074.ᜀ(0, 0, 65525, 32, 62464, 0, 0, 0, 8384);
		spr\u2074.ᜀ[11] = new spr\u2074.ᜀ(0, 0, 65525, 32, 62464, 0, 0, 0, 8384);
		spr\u2074.ᜀ[12] = new spr\u2074.ᜀ(0, 0, 65525, 32, 62464, 0, 0, 0, 8384);
		spr\u2074.ᜀ[13] = new spr\u2074.ᜀ(0, 0, 65525, 32, 62464, 0, 0, 0, 8384);
		spr\u2074.ᜀ[14] = new spr\u2074.ᜀ(0, 0, 65525, 32, 62464, 0, 0, 0, 8384);
		spr\u2074.ᜀ[15] = new spr\u2074.ᜀ(1, 0, 1, 32, 0, 0, 0, 0, 8384);
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x00030478 File Offset: 0x0002F478
	public static spr\u2074.ᜀ[] ᜀ()
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
		return spr\u2074.ᜀ;
	}

	// Token: 0x0400053B RID: 1339
	private static readonly spr\u2074.ᜀ[] ᜀ;

	// Token: 0x020000E8 RID: 232
	public class ᜀ
	{
		// Token: 0x060004DE RID: 1246 RVA: 0x000304CC File Offset: 0x0002F4CC
		public ᜀ(ushort A_0, ushort A_1, ushort A_2, ushort A_3, ushort A_4, ushort A_5, ushort A_6, int A_7, ushort A_8)
		{
			this.ᜀ.ᜀ = A_0;
			this.ᜀ.ᜁ = A_1;
			this.ᜀ.ᜂ = A_2;
			this.ᜀ.ᜃ = A_3;
			this.ᜀ.ᜄ = A_4;
			this.ᜀ.ᜅ = A_5;
			this.ᜀ.ᜆ = A_6;
			this.ᜀ.ᜇ = A_7;
			this.ᜀ.ᜈ = A_8;
		}

		// Token: 0x060004DF RID: 1247
		[DllImport("kernel32")]
		private static extern void CopyMemory(IntPtr A_0, IntPtr A_1, int A_2);

		// Token: 0x060004E0 RID: 1248 RVA: 0x00030554 File Offset: 0x0002F554
		public unsafe byte[] ᜀ()
		{
			switch (0)
			{
			default:
			{
				byte[] array;
				byte* ptr;
				for (;;)
				{
					array = new byte[sizeof(spr\u2245)];
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 5;
							continue;
						case 1:
						{
							if (true)
							{
							}
							byte[] array2;
							if ((array2 = array) != null)
							{
								num = 0;
								continue;
							}
							goto IL_95;
						}
						case 2:
							goto IL_A4;
						case 3:
							goto IL_95;
						case 4:
							goto IL_89;
						case 5:
						{
							byte[] array2;
							if (array2.Length == 0)
							{
								num = 3;
								continue;
							}
							fixed (byte* ptr = &array2[0])
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_98;
								default:
									if (false)
									{
									}
									num = 4;
									continue;
								}
								break;
							}
						}
						}
						break;
						IL_98:
						num = 2;
						continue;
						IL_95:
						ptr = null;
						goto IL_98;
					}
				}
				IL_89:
				IL_A4:
				fixed (IntPtr* ptr2 = (IntPtr*)(&this.ᜀ))
				{
					spr\u2074.ᜀ.CopyMemory((IntPtr)((void*)ptr), (IntPtr)((void*)ptr2), sizeof(spr\u2245));
				}
				ptr = null;
				return array;
			}
			}
		}

		// Token: 0x0400053C RID: 1340
		private float \u2460\u0080\u00AD\u00A4;

		// Token: 0x0400053D RID: 1341
		private int[] \u25D9\u009D\u008B\u00AC;

		// Token: 0x0400053E RID: 1342
		private byte \u2609\u008C\u00A1\u00A8;

		// Token: 0x0400053F RID: 1343
		private byte \u2460\u00AF\u00AF\u0083;

		// Token: 0x04000540 RID: 1344
		private byte \u2593\u009D\u00A5\u0090;

		// Token: 0x04000541 RID: 1345
		private spr\u2245 ᜀ;
	}
}
