using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;

// Token: 0x02000069 RID: 105
internal class sprᦛ
{
	// Token: 0x06000362 RID: 866 RVA: 0x0001FFAC File Offset: 0x0001EFAC
	public sprᦛ()
	{
		this.ᜀ = new sprᠹ(this);
		this.ᜁ = new sprᠹ(this);
	}

	// Token: 0x06000363 RID: 867 RVA: 0x0001FFF0 File Offset: 0x0001EFF0
	public int ᜀ(string A_0)
	{
		int num;
		int num3;
		for (;;)
		{
			num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_98;
				case 1:
				{
					if (this.ᜁ.ᜀ(A_0, ref num))
					{
						num2 = 4;
						continue;
					}
					sprᦠ sprᦠ = new sprᦠ();
					sprᦠ.ᜀ(A_0);
					sprᦠ.ᜀ(1);
					sprᦠ.ᜁ(this.ᜀ.ᜃ());
					this.ᜁ.ᜁ(num, sprᦠ);
					num = this.ᜀ.ᜀ(sprᦠ);
					num3 = A_0.Length * 2 + 3;
					num2 = 6;
					continue;
				}
				case 2:
					goto IL_ED;
				case 3:
					goto IL_ED;
				case 4:
					goto IL_4D;
				case 5:
					goto IL_134;
				case 6:
					if (this.ᜃ + num3 > 8224)
					{
						num2 = 0;
						continue;
					}
					goto IL_ED;
				case 7:
					if (num3 > 8224)
					{
						if (true)
						{
						}
						this.ᜂ.Add(8224);
						num3++;
						num3 -= 8224;
						this.ᜄ += 8224;
						num2 = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_98;
					default:
						if (false)
						{
						}
						num2 = 5;
						continue;
					}
					break;
				}
				break;
				IL_98:
				this.ᜂ.Add((ushort)this.ᜃ);
				this.ᜃ = 0;
				num2 = 2;
				continue;
				IL_ED:
				num2 = 7;
			}
		}
		IL_4D:
		sprᦠ sprᦠ2 = this.ᜀ.ᜁ(num);
		sprᦠ2.ᜀ(sprᦠ2.ᜀ() + 1);
		this.ᜇ++;
		return num;
		IL_134:
		this.ᜄ += num3;
		this.ᜃ += num3;
		this.ᜇ++;
		this.ᜈ++;
		return num;
	}

	// Token: 0x06000364 RID: 868 RVA: 0x000201E4 File Offset: 0x0001F1E4
	public void ᜀ(Stream A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				this.ᜂ.Add((ushort)this.ᜃ);
				spr\u1DCF a_;
				a_.ᜀ = 252;
				a_.ᜁ = (ushort)this.ᜂ[0];
				byte[] array = spr\u1DCF.ᜀ(a_);
				A_0.Write(array, 0, array.Length);
				sprᦛ.ᜀ a_2;
				a_2.ᜀ = this.ᜇ;
				a_2.ᜁ = this.ᜈ;
				array = sprᦛ.ᜀ.ᜀ(a_2);
				A_0.Write(array, 0, array.Length);
				this.ᜅ = 8;
				this.ᜆ = 1;
				int num = 0;
				if (true)
				{
				}
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_C9;
					case 1:
						goto IL_C9;
					case 2:
						return;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C9;
						default:
							if (false)
							{
							}
							if (num >= this.ᜀ.ᜃ())
							{
								num2 = 2;
								continue;
							}
							this.ᜀ.ᜀ(num, A_0);
							num++;
							num2 = 0;
							continue;
						}
						break;
					}
					break;
					IL_C9:
					num2 = 3;
				}
			}
			return;
		}
	}

	// Token: 0x06000365 RID: 869 RVA: 0x00020328 File Offset: 0x0001F328
	public ArrayList ᜂ()
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

	// Token: 0x06000366 RID: 870 RVA: 0x0002036C File Offset: 0x0001F36C
	public int ᜁ()
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
		return this.ᜆ;
	}

	// Token: 0x06000367 RID: 871 RVA: 0x000203B0 File Offset: 0x0001F3B0
	public void ᜀ(int A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x06000368 RID: 872 RVA: 0x000203F4 File Offset: 0x0001F3F4
	public int ᜀ()
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

	// Token: 0x06000369 RID: 873 RVA: 0x00020438 File Offset: 0x0001F438
	public void ᜁ(int A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x04000265 RID: 613
	private sprᠹ ᜀ;

	// Token: 0x04000266 RID: 614
	private sprᠹ ᜁ;

	// Token: 0x04000267 RID: 615
	private ArrayList ᜂ = new ArrayList();

	// Token: 0x04000268 RID: 616
	private int ᜃ = 8;

	// Token: 0x04000269 RID: 617
	private int ᜄ = 8;

	// Token: 0x0400026A RID: 618
	private int ᜅ;

	// Token: 0x0400026B RID: 619
	private int ᜆ;

	// Token: 0x0400026C RID: 620
	private int ᜇ;

	// Token: 0x0400026D RID: 621
	private int ᜈ;

	// Token: 0x0200006A RID: 106
	private struct ᜀ
	{
		// Token: 0x0600036A RID: 874
		[DllImport("kernel32")]
		private static extern void CopyMemory(IntPtr A_0, IntPtr A_1, int A_2);

		// Token: 0x0600036B RID: 875 RVA: 0x0002047C File Offset: 0x0001F47C
		public unsafe static byte[] ᜀ(sprᦛ.ᜀ A_0)
		{
			switch (0)
			{
			default:
			{
				byte[] array;
				byte* ptr;
				for (;;)
				{
					IL_2F:
					array = new byte[sprᦛ.ᜀ.ᜀ()];
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_A3;
						case 1:
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
							goto IL_8A;
						}
						case 2:
							num = 5;
							continue;
						case 3:
							goto IL_6C;
						case 4:
							goto IL_8A;
						case 5:
						{
							byte[] array2;
							if (array2.Length == 0)
							{
								num = 4;
								continue;
							}
							fixed (byte* ptr = &array2[0])
							{
								num = 3;
								continue;
								break;
							}
						}
						}
						goto IL_2F;
						IL_8A:
						ptr = null;
						num = 0;
					}
					IL_6C:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_82;
					}
				}
				IL_82:
				if (false)
				{
				}
				IL_A3:
				void* value = (void*)(&A_0);
				sprᦛ.ᜀ.CopyMemory((IntPtr)((void*)ptr), (IntPtr)value, sprᦛ.ᜀ.ᜀ());
				ptr = null;
				return array;
			}
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0002056C File Offset: 0x0001F56C
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
			return sizeof(sprᦛ.ᜀ);
		}

		// Token: 0x0400026E RID: 622
		public int ᜀ;

		// Token: 0x0400026F RID: 623
		public int ᜁ;
	}
}
