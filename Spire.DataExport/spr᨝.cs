using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

// Token: 0x02000008 RID: 8
[DefaultMember("Item")]
internal class spr\u1A1D : spr\u2574
{
	// Token: 0x06000038 RID: 56 RVA: 0x000042B4 File Offset: 0x000032B4
	public spr\u1A1D(spr\u219E A_0) : base(A_0)
	{
	}

	// Token: 0x06000039 RID: 57 RVA: 0x000042C8 File Offset: 0x000032C8
	public int ᜀ(sprᥔ A_0)
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
		this.ᜀ = false;
		return base.ᜁ(A_0);
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00004314 File Offset: 0x00003314
	public void ᜁ(int A_0, sprᥔ A_1)
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
		this.ᜀ = false;
		base.ᜁ(A_0, A_1);
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00004360 File Offset: 0x00003360
	public bool ᜀ(int A_0, ref int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 10;
			bool result;
			int num2;
			for (;;)
			{
				int num4;
				int num5;
				switch (num)
				{
				case 0:
					goto IL_11C;
				case 1:
					goto IL_140;
				case 2:
					goto IL_11C;
				case 3:
					this.ᜀ();
					num = 14;
					continue;
				case 4:
					goto IL_11C;
				case 5:
				{
					result = true;
					int num3;
					num2 = num3;
					num = 12;
					continue;
				}
				case 6:
					goto IL_140;
				case 7:
					if (true)
					{
					}
					if (num4 == 0)
					{
						num = 5;
						continue;
					}
					goto IL_140;
				case 8:
				{
					if (num4 < 0)
					{
						num = 17;
						continue;
					}
					int num3;
					num5 = num3 - 1;
					num = 7;
					continue;
				}
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_209;
					default:
					{
						if (false)
						{
						}
						int num3;
						if ((int)this.ᜀ(num3).ᜁ() > A_0)
						{
							num = 15;
							continue;
						}
						num4 = 0;
						num = 0;
						continue;
					}
					}
					break;
				case 11:
					goto IL_15F;
				case 12:
					goto IL_140;
				case 13:
				{
					int num3;
					if ((int)this.ᜀ(num3).ᜁ() < A_0)
					{
						num = 18;
						continue;
					}
					num = 9;
					continue;
				}
				case 14:
					goto IL_FF;
				case 15:
					num4 = 1;
					num = 4;
					continue;
				case 16:
				{
					if (num2 > num5)
					{
						num = 11;
						continue;
					}
					int num3 = num2 + num5 >> 1;
					num = 13;
					continue;
				}
				case 17:
				{
					int num3;
					num2 = num3 + 1;
					num = 6;
					continue;
				}
				case 18:
					num4 = -1;
					goto IL_209;
				}
				if (!this.ᜀ)
				{
					num = 3;
					continue;
				}
				IL_FF:
				result = false;
				num2 = 0;
				num5 = base.ᜌ() - 1;
				num4 = 0;
				num = 1;
				continue;
				IL_11C:
				num = 8;
				continue;
				IL_140:
				num = 16;
				continue;
				IL_209:
				num = 2;
			}
			IL_15F:
			A_1 = num2;
			return result;
		}
		}
	}

	// Token: 0x0600003C RID: 60 RVA: 0x0000458C File Offset: 0x0000358C
	public new void ᜀ()
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
		base.ᜀ(new spr\u1A1D.ᜁ());
		this.ᜀ = true;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x000045DC File Offset: 0x000035DC
	public unsafe void ᜀ(sprᬱ A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 8;
			byte* ptr;
			for (;;)
			{
				ushort num2;
				ushort ᜁ;
				ushort ᜂ;
				ushort ᜃ;
				ushort ᜄ;
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (num2 > ᜁ)
						{
							num = 5;
							continue;
						}
						break;
					}
					this.ᜀ(new sprᥔ(num2, ᜂ, ᜃ, ᜄ));
					A_0.Close();
					num2 += 1;
					num = 11;
					continue;
				case 2:
					goto IL_E5;
				case 3:
				{
					byte[] array;
					if (array.Length == 0)
					{
						num = 7;
						continue;
					}
					fixed (byte* ptr = &array[0])
					{
						num = 6;
						continue;
						break;
					}
				}
				case 4:
					goto IL_5C;
				case 5:
					goto IL_11E;
				case 6:
					goto IL_61;
				case 7:
					goto IL_157;
				case 9:
				{
					byte[] array;
					if ((array = A_0.ᜢ()) != null)
					{
						num = 0;
						continue;
					}
					goto IL_157;
				}
				case 10:
					goto IL_61;
				case 11:
					goto IL_E5;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 9;
				continue;
				IL_61:
				ushort num3 = ((spr\u1A1D.ᜀ*)ptr)->ᜀ;
				ᜁ = ((spr\u1A1D.ᜀ*)ptr)->ᜁ;
				ᜂ = ((spr\u1A1D.ᜀ*)ptr)->ᜂ;
				ᜃ = ((spr\u1A1D.ᜀ*)ptr)->ᜃ;
				ᜄ = ((spr\u1A1D.ᜀ*)ptr)->ᜄ;
				num2 = num3;
				num = 2;
				continue;
				IL_E5:
				num = 1;
				continue;
				IL_157:
				ptr = null;
				num = 10;
			}
			IL_5C:
			if (true)
			{
			}
			return;
			IL_11E:
			ptr = null;
			return;
		}
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x0000477C File Offset: 0x0000377C
	public void ᜀ(sprḗ A_0, int A_1, ref int A_2)
	{
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
			{
				int num = 7;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						goto IL_182;
					case 1:
						if (num2 == A_1)
						{
							num = 8;
							continue;
						}
						goto IL_61;
					case 2:
						goto IL_61;
					case 3:
						goto IL_182;
					case 4:
						this.ᜀ();
						num = 5;
						continue;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							goto IL_1A8;
						}
						break;
					case 6:
						return;
					case 8:
						A_2 = (int)A_0.Position;
						num = 2;
						continue;
					case 9:
						if (num2 >= base.ᜌ())
						{
							num = 6;
							continue;
						}
						num = 1;
						continue;
					}
					if (!this.ᜀ)
					{
						num = 4;
						continue;
					}
					goto IL_1A8;
					IL_61:
					int num3 = num2 + 1;
					spr\u1DCF a_;
					a_.ᜀ = 125;
					a_.ᜁ = (ushort)sizeof(spr\u1A1D.ᜀ);
					byte[] array = spr\u1DCF.ᜀ(a_);
					A_0.ᜁ(array, array.Length);
					spr\u1A1D.ᜀ a_2;
					a_2.ᜀ = this.ᜀ(num2).ᜁ();
					a_2.ᜁ = this.ᜀ(num3 - 1).ᜁ();
					a_2.ᜂ = this.ᜀ(num2).ᜃ();
					a_2.ᜃ = this.ᜀ(num2).ᜂ();
					a_2.ᜄ = this.ᜀ(num2).ᜀ();
					a_2.ᜅ = 0;
					array = spr\u1A1D.ᜀ.ᜀ(a_2);
					A_0.ᜁ(array, array.Length);
					num2 = num3;
					num = 0;
					continue;
					IL_182:
					num = 9;
					continue;
					IL_1A8:
					num2 = 0;
					num = 3;
				}
				break;
			}
			}
		}
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00004960 File Offset: 0x00003960
	public void ᜀ(sprḗ A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_142;
				case 1:
					goto IL_142;
				case 2:
					goto IL_11B;
				case 3:
					if (num2 < base.ᜌ())
					{
						int num3 = num2 + 1;
						spr\u1DCF a_;
						a_.ᜀ = 125;
						a_.ᜁ = (ushort)sizeof(spr\u1A1D.ᜀ);
						byte[] array = spr\u1DCF.ᜀ(a_);
						A_0.ᜁ(array, array.Length);
						spr\u1A1D.ᜀ a_2;
						a_2.ᜀ = this.ᜀ(num2).ᜁ();
						a_2.ᜁ = this.ᜀ(num3 - 1).ᜁ();
						a_2.ᜂ = this.ᜀ(num2).ᜃ();
						a_2.ᜃ = this.ᜀ(num2).ᜂ();
						a_2.ᜄ = this.ᜀ(num2).ᜀ();
						a_2.ᜅ = 0;
						array = spr\u1A1D.ᜀ.ᜀ(a_2);
						A_0.ᜁ(array, array.Length);
						num2 = num3;
						num = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_11D;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 4:
					if (true)
					{
					}
					this.ᜀ();
					num = 2;
					continue;
				case 5:
					return;
				}
				if (!this.ᜀ)
				{
					num = 4;
					continue;
				}
				goto IL_11B;
				IL_11D:
				num = 0;
				continue;
				IL_11B:
				num2 = 0;
				goto IL_11D;
				IL_142:
				num = 3;
			}
			return;
		}
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00004AFC File Offset: 0x00003AFC
	public new sprᥔ ᜀ(int A_0)
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
		return base.ᜀ(A_0) as sprᥔ;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00004B44 File Offset: 0x00003B44
	public void ᜀ(int A_0, sprᥔ A_1)
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
		base.ᜀ(A_0, A_1);
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00004B88 File Offset: 0x00003B88
	public int ᜁ()
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 9;
			for (;;)
			{
				int num3;
				int num4;
				switch (num2)
				{
				case 0:
					goto IL_A4;
				case 1:
					if (true)
					{
					}
					goto IL_D4;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return num;
					default:
						if (false)
						{
						}
						this.ᜀ();
						num2 = 11;
						continue;
					}
					break;
				case 3:
					if (num3 < base.ᜌ())
					{
						num2 = 5;
						continue;
					}
					goto IL_108;
				case 4:
					return num;
				case 5:
					num2 = 7;
					continue;
				case 6:
					goto IL_A4;
				case 7:
					if (!this.ᜀ(num4).ᜀ(this.ᜀ(num3)))
					{
						num2 = 10;
						continue;
					}
					num3++;
					num2 = 0;
					continue;
				case 8:
					goto IL_D4;
				case 9:
					if (!this.ᜀ)
					{
						num2 = 2;
						continue;
					}
					goto IL_C5;
				case 10:
					goto IL_108;
				case 11:
					goto IL_C5;
				case 12:
					if (num4 >= base.ᜌ())
					{
						num2 = 4;
						continue;
					}
					num3 = num4 + 1;
					num2 = 6;
					continue;
				}
				break;
				IL_A4:
				num2 = 3;
				continue;
				IL_C5:
				num4 = 0;
				num2 = 8;
				continue;
				IL_D4:
				num2 = 12;
				continue;
				IL_108:
				num += sizeof(spr\u1DCF) + sizeof(spr\u1A1D.ᜀ);
				num4 = num3;
				num2 = 1;
			}
		}
		return num;
	}

	// Token: 0x0400000C RID: 12
	private new bool ᜀ;

	// Token: 0x02000009 RID: 9
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	private new struct ᜀ
	{
		// Token: 0x06000043 RID: 67
		[DllImport("kernel32")]
		private static extern void CopyMemory(IntPtr A_0, IntPtr A_1, int A_2);

		// Token: 0x06000044 RID: 68 RVA: 0x00004CF8 File Offset: 0x00003CF8
		public unsafe static byte[] ᜀ(spr\u1A1D.ᜀ A_0)
		{
			switch (0)
			{
			default:
			{
				byte[] array;
				byte* ptr;
				for (;;)
				{
					array = new byte[spr\u1A1D.ᜀ.ᜀ()];
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_A6;
						case 1:
							goto IL_97;
						case 2:
							goto IL_75;
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
								num = 4;
								continue;
								break;
							}
						}
						case 4:
							goto IL_95;
						case 5:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_75;
							default:
							{
								if (false)
								{
								}
								byte[] array2;
								if ((array2 = array) != null)
								{
									num = 2;
									continue;
								}
								goto IL_97;
							}
							}
							break;
						}
						break;
						IL_75:
						num = 3;
						continue;
						IL_97:
						ptr = null;
						num = 0;
					}
				}
				IL_95:
				IL_A6:
				void* value = (void*)(&A_0);
				spr\u1A1D.ᜀ.CopyMemory((IntPtr)((void*)ptr), (IntPtr)value, spr\u1A1D.ᜀ.ᜀ());
				ptr = null;
				return array;
			}
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00004DEC File Offset: 0x00003DEC
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
			return sizeof(spr\u1A1D.ᜀ);
		}

		// Token: 0x0400000D RID: 13
		public ushort ᜀ;

		// Token: 0x0400000E RID: 14
		public ushort ᜁ;

		// Token: 0x0400000F RID: 15
		public ushort ᜂ;

		// Token: 0x04000010 RID: 16
		public ushort ᜃ;

		// Token: 0x04000011 RID: 17
		public ushort ᜄ;

		// Token: 0x04000012 RID: 18
		public byte ᜅ;
	}

	// Token: 0x0200000A RID: 10
	private new class ᜁ : IComparer
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00004E30 File Offset: 0x00003E30
		int IComparer.ᜀ(object A_0, object A_1)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return -1;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_42;
					default:
						goto IL_6A;
					}
					break;
				case 2:
					if ((A_0 as sprᥔ).ᜁ() > (A_1 as sprᥔ).ᜁ())
					{
						num = 1;
						continue;
					}
					return 0;
				}
				goto IL_2A;
				IL_42:
				if (true)
				{
				}
				num = 0;
				continue;
				IL_2A:
				if ((A_0 as sprᥔ).ᜁ() < (A_1 as sprᥔ).ᜁ())
				{
					goto IL_42;
				}
				num = 2;
			}
			return -1;
			IL_6A:
			if (false)
			{
			}
			return 1;
		}
	}
}
