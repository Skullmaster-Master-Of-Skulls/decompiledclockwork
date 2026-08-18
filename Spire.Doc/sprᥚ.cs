using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x020003F6 RID: 1014
[CLSCompliant(false)]
internal class sprᥚ
{
	// Token: 0x060038C7 RID: 14535 RVA: 0x00351434 File Offset: 0x00350434
	internal sprᥚ()
	{
	}

	// Token: 0x060038C8 RID: 14536 RVA: 0x00351448 File Offset: 0x00350448
	internal sprᥚ(sprᾱ A_0, Stream A_1, sprᡆ A_2)
	{
		int num = A_0.ឋ();
		int num2 = A_0.\u1733();
		this.ᜀ = new byte[num];
		this.ᜁ = new byte[num2];
		this.ᜀ(A_2, A_0, A_1);
		A_1.Position = (long)A_0.\u177F();
		A_1.Read(this.ᜀ, 0, num);
		A_1.Position = (long)A_0.ឝ();
		A_1.Read(this.ᜁ, 0, num2);
	}

	// Token: 0x060038C9 RID: 14537 RVA: 0x003514C4 File Offset: 0x003504C4
	internal byte[] ᜀ()
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

	// Token: 0x060038CA RID: 14538 RVA: 0x00351508 File Offset: 0x00350508
	internal void ᜁ(byte[] A_0)
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

	// Token: 0x060038CB RID: 14539 RVA: 0x0035154C File Offset: 0x0035054C
	internal byte[] ᜁ()
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

	// Token: 0x060038CC RID: 14540 RVA: 0x00351590 File Offset: 0x00350590
	internal void ᜀ(byte[] A_0)
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

	// Token: 0x060038CD RID: 14541 RVA: 0x003515D4 File Offset: 0x003505D4
	internal void ᜀ(sprᾱ A_0, Stream A_1)
	{
		for (;;)
		{
			IL_00:
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜀ != null)
					{
						num = 2;
						continue;
					}
					return;
				case 1:
					goto IL_CA;
				case 2:
					A_0.ឃ((int)A_1.Position);
					A_1.Write(this.ᜀ, 0, this.ᜀ.Length);
					A_0.ប(this.ᜀ.Length);
					A_0.ᜥ((int)A_1.Position);
					A_1.Write(this.ᜁ, 0, this.ᜁ.Length);
					A_0.\u1756(this.ᜁ.Length);
					num = 1;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				if (this.ᜁ == null)
				{
					return;
				}
				num = 3;
			}
		}
		IL_CA:
		if (true)
		{
		}
	}

	// Token: 0x060038CE RID: 14542 RVA: 0x003516D8 File Offset: 0x003506D8
	internal void ᜁ(sprᾱ A_0, Stream A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				BinaryReader binaryReader = new BinaryReader(A_1);
				int num = 8;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_1AE;
					case 1:
						goto IL_120;
					case 2:
					{
						int num2;
						int num3;
						if (num2 >= num3)
						{
							goto IL_1C1;
						}
						this.ᜃ.Add(binaryReader.ReadInt32());
						num2++;
						num = 0;
						continue;
					}
					case 3:
						if (this.ᜁ.Length <= 0)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1C1;
						default:
							if (false)
							{
							}
							num = 9;
							continue;
						}
						break;
					case 4:
					{
						this.ᜃ = new List<int>();
						int num3 = (this.ᜀ.Length + 2) / 6;
						binaryReader.BaseStream.Position = (long)A_0.\u177F();
						int num2 = 0;
						num = 5;
						continue;
					}
					case 5:
						goto IL_1AE;
					case 6:
						return;
					case 7:
						goto IL_120;
					case 8:
						if (this.ᜀ.Length > 0)
						{
							num = 4;
							continue;
						}
						goto IL_AB;
					case 9:
					{
						this.ᜂ = new List<int>();
						int num4 = (this.ᜁ.Length + 2) / 6;
						binaryReader.BaseStream.Position = (long)A_0.ឝ();
						int num5 = 0;
						num = 1;
						continue;
					}
					case 10:
					{
						int num4;
						int num5;
						if (num5 >= num4)
						{
							num = 6;
							continue;
						}
						this.ᜂ.Add(binaryReader.ReadInt32());
						num5++;
						num = 7;
						continue;
					}
					case 11:
						goto IL_AB;
					}
					break;
					IL_AB:
					if (true)
					{
					}
					num = 3;
					continue;
					IL_120:
					num = 10;
					continue;
					IL_1AE:
					num = 2;
					continue;
					IL_1C1:
					num = 11;
				}
			}
			return;
		}
	}

	// Token: 0x060038CF RID: 14543 RVA: 0x003518B8 File Offset: 0x003508B8
	private void ᜀ(sprᡆ A_0, sprᾱ A_1, Stream A_2)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜁ(A_1, A_2);
				num = 2;
				continue;
			case 1:
				this.ᜀ(A_2, A_1);
				num = 4;
				continue;
			case 2:
				if (this.ᜀ(A_0, A_1))
				{
					num = 1;
					continue;
				}
				return;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_7A;
				}
				break;
			case 5:
				if (A_0 != null)
				{
					num = 0;
					continue;
				}
				return;
			case 6:
				if (true)
				{
				}
				num = 5;
				continue;
			}
			if (A_1.ណ() <= 0)
			{
				return;
			}
			num = 6;
		}
		IL_7A:
		if (false)
		{
		}
	}

	// Token: 0x060038D0 RID: 14544 RVA: 0x0035198C File Offset: 0x0035098C
	private bool ᜀ(sprᡆ A_0, sprᾱ A_1)
	{
		switch (0)
		{
		default:
		{
			bool flag;
			for (;;)
			{
				flag = false;
				int num = 10;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜂ != null)
						{
							num = 1;
							continue;
						}
						goto IL_149;
					case 1:
					{
						int num2;
						int a_;
						int num3;
						flag = this.ᜀ(true, num2, a_, num3);
						num = 9;
						continue;
					}
					case 2:
						if (this.ᜃ != null)
						{
							goto IL_15D;
						}
						return flag;
					case 3:
						num = 11;
						continue;
					case 4:
					{
						int num2;
						int a_;
						int num3;
						flag = this.ᜀ(false, num2, a_, num3);
						num = 5;
						continue;
					}
					case 5:
						return flag;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_15D;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					case 7:
					{
						if (true)
						{
						}
						int num2 = A_1.\u1774() + A_1.ព();
						int num3 = A_0.ᜀ()[6];
						int a_ = num2 + num3;
						num = 0;
						continue;
					}
					case 8:
						if (flag)
						{
							num = 4;
							continue;
						}
						return flag;
					case 9:
						goto IL_149;
					case 10:
						if (A_1.ណ() > 0)
						{
							num = 3;
							continue;
						}
						return flag;
					case 11:
						if (A_0 != null)
						{
							num = 7;
							continue;
						}
						return flag;
					}
					break;
					IL_149:
					num = 2;
					continue;
					IL_15D:
					num = 6;
				}
			}
			return flag;
		}
		}
	}

	// Token: 0x060038D1 RID: 14545 RVA: 0x00351B08 File Offset: 0x00350B08
	private bool ᜀ(bool A_0, int A_1, int A_2, int A_3)
	{
		int num;
		int num2;
		for (;;)
		{
			num = this.ᜀ(A_0, A_1);
			num2 = this.ᜀ(A_0, A_2) + 1;
			if (true)
			{
			}
			int num3 = 3;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_91;
				case 1:
					num3 = 2;
					continue;
				case 2:
					if (num2 == 2147483647)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3C;
						}
						if (false)
						{
						}
						num3 = 0;
						continue;
					}
					goto IL_93;
				case 3:
					goto IL_3C;
				}
				break;
				IL_3C:
				if (num == 2147483647)
				{
					return false;
				}
				num3 = 1;
			}
		}
		return false;
		IL_91:
		return false;
		IL_93:
		this.ᜀ(A_1, num, num2, A_0);
		this.ᜀ(num2, A_3, A_0);
		return true;
	}

	// Token: 0x060038D2 RID: 14546 RVA: 0x00351BC0 File Offset: 0x00350BC0
	private void ᜀ(int A_0, int A_1, int A_2, bool A_3)
	{
		int num = 3;
		for (;;)
		{
			List<int> list;
			int num2;
			List<int> list2;
			switch (num)
			{
			case 0:
				list = this.ᜂ;
				goto IL_C1;
			case 1:
				return;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					goto IL_7A;
				}
				break;
			case 4:
				num = 6;
				continue;
			case 5:
				if (num2 >= A_2)
				{
					num = 1;
					continue;
				}
				list2[num2] = A_0;
				num2++;
				num = 2;
				continue;
			case 6:
				if (true)
				{
				}
				list = this.ᜃ;
				goto IL_C1;
			case 7:
				goto IL_7A;
			}
			if (!A_3)
			{
				num = 4;
				continue;
			}
			num = 0;
			continue;
			IL_7A:
			num = 5;
			continue;
			IL_C1:
			list2 = list;
			num2 = A_1;
			num = 7;
		}
	}

	// Token: 0x060038D3 RID: 14547 RVA: 0x00351CA0 File Offset: 0x00350CA0
	private int ᜀ(bool A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 10;
			int result;
			for (;;)
			{
				List<int> list;
				int num2;
				List<int> list2;
				int count;
				switch (num)
				{
				case 0:
					if (list[num2] >= A_1)
					{
						num = 8;
						continue;
					}
					num2++;
					num = 1;
					continue;
				case 1:
					goto IL_D3;
				case 2:
					list2 = this.ᜃ;
					goto IL_117;
				case 3:
					return result;
				case 4:
					goto IL_D3;
				case 5:
					return result;
				case 6:
					num = 2;
					continue;
				case 7:
					if (num2 >= count)
					{
						num = 5;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return result;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 8:
					result = num2;
					num = 3;
					continue;
				case 9:
					list2 = this.ᜂ;
					goto IL_117;
				case 10:
					if (true)
					{
					}
					break;
				}
				if (!A_0)
				{
					num = 6;
					continue;
				}
				num = 9;
				continue;
				IL_D3:
				num = 7;
				continue;
				IL_117:
				list = list2;
				result = int.MaxValue;
				num2 = 0;
				count = list.Count;
				num = 4;
			}
			return result;
		}
		}
	}

	// Token: 0x060038D4 RID: 14548 RVA: 0x00351DE4 File Offset: 0x00350DE4
	private void ᜀ(int A_0, int A_1, bool A_2)
	{
		int num = 4;
		for (;;)
		{
			int num2;
			int count;
			List<int> list;
			List<int> list2;
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					goto IL_89;
				}
				break;
			case 1:
				if (num2 >= count)
				{
					num = 2;
					continue;
				}
				list[num2] -= A_1;
				num2++;
				if (true)
				{
				}
				num = 0;
				continue;
			case 2:
				return;
			case 3:
				list2 = this.ᜂ;
				goto IL_CB;
			case 5:
				list2 = this.ᜃ;
				goto IL_CB;
			case 6:
				num = 5;
				continue;
			case 7:
				goto IL_89;
			}
			if (!A_2)
			{
				num = 6;
				continue;
			}
			num = 3;
			continue;
			IL_89:
			num = 1;
			continue;
			IL_CB:
			list = list2;
			num2 = A_0;
			count = list.Count;
			num = 7;
		}
	}

	// Token: 0x060038D5 RID: 14549 RVA: 0x00351ED4 File Offset: 0x00350ED4
	private void ᜀ(Stream A_0, sprᾱ A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				BinaryWriter binaryWriter = new BinaryWriter(A_0);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_18A;
					case 1:
						if (this.ᜃ != null)
						{
							num = 11;
							continue;
						}
						goto IL_A4;
					case 2:
					{
						binaryWriter.BaseStream.Position = (long)A_1.ឝ();
						int num2 = 0;
						int count = this.ᜂ.Count;
						if (true)
						{
						}
						num = 6;
						continue;
					}
					case 3:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 9;
							continue;
						}
						binaryWriter.Write(this.ᜂ[num2]);
						num2++;
						num = 4;
						continue;
					}
					case 4:
						goto IL_10C;
					case 5:
					{
						int num3;
						int count2;
						if (num3 >= count2)
						{
							goto IL_19D;
						}
						binaryWriter.Write(this.ᜃ[num3]);
						num3++;
						num = 0;
						continue;
					}
					case 6:
						goto IL_10C;
					case 7:
						goto IL_18A;
					case 8:
						goto IL_A4;
					case 9:
						return;
					case 10:
						if (this.ᜂ == null)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_19D;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 11:
					{
						binaryWriter.BaseStream.Position = (long)A_1.\u177F();
						int num3 = 0;
						int count2 = this.ᜃ.Count;
						num = 7;
						continue;
					}
					}
					break;
					IL_A4:
					num = 10;
					continue;
					IL_10C:
					num = 3;
					continue;
					IL_18A:
					num = 5;
					continue;
					IL_19D:
					num = 8;
				}
			}
			return;
		}
	}

	// Token: 0x04002A6C RID: 10860
	private byte[] ᜀ;

	// Token: 0x04002A6D RID: 10861
	private byte[] ᜁ;

	// Token: 0x04002A6E RID: 10862
	private List<int> ᜂ;

	// Token: 0x04002A6F RID: 10863
	private List<int> ᜃ;
}
