using System;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200057C RID: 1404
internal class spr\u19E8
{
	// Token: 0x06005455 RID: 21589 RVA: 0x00348D3C File Offset: 0x00347D3C
	public spr\u19E8()
	{
		this.ᜅ = new byte[8];
		this.ᜆ = default(Guid);
		this.ᜇ = 62;
		this.ᜈ = 3;
		this.ᜉ = 65534;
		this.ᜊ = 9;
		this.ᜋ = 6;
		this.ᜐ = -1;
		this.\u1712 = 4096U;
		this.\u1713 = -2;
		this.\u1715 = -2;
		this.\u1717 = new int[109];
		base..ctor();
		Buffer.BlockCopy(spr\u19E8.ᜄ, 0, this.ᜅ, 0, 8);
	}

	// Token: 0x06005456 RID: 21590 RVA: 0x00348DD4 File Offset: 0x00347DD4
	public spr\u19E8(Stream A_0)
	{
		int a_ = 5;
		this.ᜅ = new byte[8];
		this.ᜆ = default(Guid);
		this.ᜇ = 62;
		this.ᜈ = 3;
		this.ᜉ = 65534;
		this.ᜊ = 9;
		this.ᜋ = 6;
		this.ᜐ = -1;
		this.\u1712 = 4096U;
		this.\u1713 = -2;
		this.\u1715 = -2;
		this.\u1717 = new int[109];
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("䠺䤼䴾⑀≂⡄", a_));
		}
		if (A_0.Length < 512L)
		{
			throw new sprẩ();
		}
		byte[] array = new byte[512];
		A_0.Read(array, 0, 512);
		Buffer.BlockCopy(array, 0, this.ᜅ, 0, 8);
		this.ᜀ();
		int num = 8;
		byte[] array2 = new byte[16];
		Buffer.BlockCopy(array, num, array2, 0, 16);
		num += 16;
		this.ᜆ = new Guid(array2);
		this.ᜇ = BitConverter.ToUInt16(array, num);
		num += 2;
		this.ᜈ = BitConverter.ToUInt16(array, num);
		num += 2;
		this.ᜉ = BitConverter.ToUInt16(array, num);
		num += 2;
		this.ᜊ = BitConverter.ToUInt16(array, num);
		num += 2;
		this.ᜋ = BitConverter.ToUInt16(array, num);
		num += 2;
		this.ᜌ = BitConverter.ToUInt16(array, num);
		num += 2;
		this.\u170D = BitConverter.ToUInt32(array, num);
		num += 4;
		this.ᜎ = BitConverter.ToUInt32(array, num);
		num += 4;
		this.ᜏ = BitConverter.ToInt32(array, num);
		num += 4;
		this.ᜐ = BitConverter.ToInt32(array, num);
		num += 4;
		this.ᜑ = BitConverter.ToInt32(array, num);
		num += 4;
		this.\u1712 = BitConverter.ToUInt32(array, num);
		num += 4;
		this.\u1713 = BitConverter.ToInt32(array, num);
		num += 4;
		this.\u1714 = BitConverter.ToInt32(array, num);
		num += 4;
		this.\u1715 = BitConverter.ToInt32(array, num);
		num += 4;
		this.\u1716 = BitConverter.ToInt32(array, num);
		num += 4;
		Buffer.BlockCopy(array, num, this.\u1717, 0, this.\u1717.Length * 4);
	}

	// Token: 0x06005457 RID: 21591 RVA: 0x0034900C File Offset: 0x0034800C
	public void ᜁ(Stream A_0)
	{
		int a_ = 15;
		if (A_0 != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				byte[] array = new byte[512];
				Buffer.BlockCopy(this.ᜅ, 0, array, 0, 8);
				int num = 8;
				byte[] src = this.ᜆ.ToByteArray();
				Buffer.BlockCopy(src, 0, array, num, 16);
				num += 16;
				this.ᜀ(array, num, this.ᜇ);
				num += 2;
				this.ᜀ(array, num, this.ᜈ);
				num += 2;
				this.ᜀ(array, num, this.ᜉ);
				num += 2;
				this.ᜀ(array, num, this.ᜊ);
				num += 2;
				this.ᜀ(array, num, this.ᜋ);
				num += 2;
				this.ᜀ(array, num, this.ᜌ);
				num += 2;
				this.ᜀ(array, num, this.\u170D);
				num += 4;
				this.ᜀ(array, num, this.ᜎ);
				num += 4;
				this.ᜀ(array, num, this.ᜏ);
				num += 4;
				this.ᜀ(array, num, this.ᜐ);
				num += 4;
				this.ᜀ(array, num, this.ᜑ);
				num += 4;
				this.ᜀ(array, num, this.\u1712);
				num += 4;
				this.ᜀ(array, num, this.\u1713);
				num += 4;
				this.ᜀ(array, num, this.\u1714);
				num += 4;
				this.ᜀ(array, num, this.\u1715);
				num += 4;
				this.ᜀ(array, num, this.\u1716);
				num += 4;
				Buffer.BlockCopy(this.\u1717, 0, array, num, this.\u1717.Length * 4);
				A_0.Write(array, 0, 512);
				return;
			}
			}
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㙄㍆㭈⹊ⱌ≎", a_));
	}

	// Token: 0x06005458 RID: 21592 RVA: 0x003491E8 File Offset: 0x003481E8
	public static bool ᜀ(Stream A_0)
	{
		bool result;
		for (;;)
		{
			IL_2A:
			result = false;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_5B:
				num = 0;
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				num = 3;
				break;
			}
			for (;;)
			{
				long position;
				switch (num)
				{
				case 0:
				{
					byte[] array = new byte[8];
					position = A_0.Position;
					num = 5;
					continue;
				}
				case 1:
					goto IL_65;
				case 2:
				{
					byte[] array;
					result = spr\u19E8.ᜀ(array);
					num = 1;
					continue;
				}
				case 3:
					goto IL_58;
				case 4:
					return result;
				case 5:
				{
					byte[] array;
					if (A_0.Read(array, 0, 8) == 8)
					{
						num = 2;
						continue;
					}
					goto IL_65;
				}
				}
				goto IL_2A;
				IL_65:
				A_0.Position = position;
				num = 4;
			}
			IL_58:
			if (A_0 != null)
			{
				goto IL_5B;
			}
			break;
		}
		return result;
	}

	// Token: 0x06005459 RID: 21593 RVA: 0x003492B0 File Offset: 0x003482B0
	private void ᜀ()
	{
		int a_ = 14;
		if (spr\u19E8.ᜀ(this.ᜅ))
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				return;
			}
		}
		throw new sprẩ(RecordTableEnumerator.b("ፃ㑅❇⑉⭋湍⍏㭑㍓㡕㥗⹙⥛ⱝ՟", a_));
	}

	// Token: 0x0600545A RID: 21594 RVA: 0x00349318 File Offset: 0x00348318
	private static bool ᜀ(byte[] A_0)
	{
		bool result;
		for (;;)
		{
			result = false;
			int num = 6;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_9F;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_64;
					default:
						if (false)
						{
						}
						result = true;
						num2 = 0;
						if (true)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 2:
					result = false;
					num = 9;
					continue;
				case 3:
					num = 10;
					continue;
				case 4:
					return result;
				case 5:
					goto IL_64;
				case 6:
					if (A_0 != null)
					{
						num = 3;
						continue;
					}
					return result;
				case 7:
					if (num2 >= 8)
					{
						num = 4;
						continue;
					}
					num = 5;
					continue;
				case 8:
					goto IL_9F;
				case 9:
					return result;
				case 10:
					if (A_0.Length == 8)
					{
						num = 1;
						continue;
					}
					return result;
				}
				break;
				IL_64:
				if (A_0[num2] != spr\u19E8.ᜄ[num2])
				{
					num = 2;
					continue;
				}
				num2++;
				num = 8;
				continue;
				IL_9F:
				num = 7;
			}
		}
		return result;
	}

	// Token: 0x0600545B RID: 21595 RVA: 0x00349428 File Offset: 0x00348428
	private void ᜀ(byte[] A_0, int A_1, ushort A_2)
	{
		int a_ = 4;
		if (A_0 != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				A_0[A_1] = (byte)(A_2 & 255);
				A_0[A_1 + 1] = (byte)((A_2 & 65280) >> 8);
				return;
			}
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("堹䤻堽☿❁㙃", a_));
	}

	// Token: 0x0600545C RID: 21596 RVA: 0x003494A0 File Offset: 0x003484A0
	private void ᜀ(byte[] A_0, int A_1, uint A_2)
	{
		int a_ = 4;
		if (A_0 != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				A_0[A_1] = (byte)(A_2 & 255U);
				A_2 >>= 8;
				A_0[A_1 + 1] = (byte)(A_2 & 255U);
				A_2 >>= 8;
				A_0[A_1 + 2] = (byte)(A_2 & 255U);
				A_2 >>= 8;
				A_0[A_1 + 3] = (byte)(A_2 & 255U);
				A_2 >>= 8;
				return;
			}
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("堹䤻堽☿❁㙃", a_));
	}

	// Token: 0x0600545D RID: 21597 RVA: 0x00349544 File Offset: 0x00348544
	private void ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 7;
		if (A_0 != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				A_0[A_1] = (byte)(A_2 & 255);
				A_2 >>= 8;
				A_0[A_1 + 1] = (byte)(A_2 & 255);
				A_2 >>= 8;
				A_0[A_1 + 2] = (byte)(A_2 & 255);
				A_2 >>= 8;
				A_0[A_1 + 3] = (byte)(A_2 & 255);
				A_2 >>= 8;
				return;
			}
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("弼䨾❀╂⁄㕆", a_));
	}

	// Token: 0x0600545E RID: 21598 RVA: 0x003495E8 File Offset: 0x003485E8
	public int ᜁ()
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
		return 1 << (int)this.ᜊ;
	}

	// Token: 0x0600545F RID: 21599 RVA: 0x00349630 File Offset: 0x00348630
	public ushort ᜈ()
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
		return this.ᜇ;
	}

	// Token: 0x06005460 RID: 21600 RVA: 0x00349674 File Offset: 0x00348674
	public ushort ᜃ()
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
		return this.ᜈ;
	}

	// Token: 0x06005461 RID: 21601 RVA: 0x003496B8 File Offset: 0x003486B8
	public ushort ᜄ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return this.ᜉ;
	}

	// Token: 0x06005462 RID: 21602 RVA: 0x003496FC File Offset: 0x003486FC
	public ushort \u170D()
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
		return this.ᜊ;
	}

	// Token: 0x06005463 RID: 21603 RVA: 0x00349740 File Offset: 0x00348740
	public ushort ᜑ()
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
		return this.ᜋ;
	}

	// Token: 0x06005464 RID: 21604 RVA: 0x00349784 File Offset: 0x00348784
	public ushort ᜌ()
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
		return this.ᜌ;
	}

	// Token: 0x06005465 RID: 21605 RVA: 0x003497C8 File Offset: 0x003487C8
	public uint ᜅ()
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
		return this.\u170D;
	}

	// Token: 0x06005466 RID: 21606 RVA: 0x0034980C File Offset: 0x0034880C
	public uint ᜇ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return this.ᜎ;
	}

	// Token: 0x06005467 RID: 21607 RVA: 0x00349850 File Offset: 0x00348850
	public int ᜏ()
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
		return this.ᜏ;
	}

	// Token: 0x06005468 RID: 21608 RVA: 0x00349894 File Offset: 0x00348894
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
		this.ᜏ = A_0;
	}

	// Token: 0x06005469 RID: 21609 RVA: 0x003498D8 File Offset: 0x003488D8
	public int ᜐ()
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
		return this.ᜐ;
	}

	// Token: 0x0600546A RID: 21610 RVA: 0x0034991C File Offset: 0x0034891C
	public void ᜂ(int A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜐ = A_0;
	}

	// Token: 0x0600546B RID: 21611 RVA: 0x00349960 File Offset: 0x00348960
	public int ᜊ()
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
		return this.ᜑ;
	}

	// Token: 0x0600546C RID: 21612 RVA: 0x003499A4 File Offset: 0x003489A4
	public uint ᜉ()
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
		return this.\u1712;
	}

	// Token: 0x0600546D RID: 21613 RVA: 0x003499E8 File Offset: 0x003489E8
	public int \u1712()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return this.\u1713;
	}

	// Token: 0x0600546E RID: 21614 RVA: 0x00349A2C File Offset: 0x00348A2C
	public void ᜄ(int A_0)
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
		this.\u1713 = A_0;
	}

	// Token: 0x0600546F RID: 21615 RVA: 0x00349A70 File Offset: 0x00348A70
	public int ᜆ()
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
		return this.\u1714;
	}

	// Token: 0x06005470 RID: 21616 RVA: 0x00349AB4 File Offset: 0x00348AB4
	public void ᜆ(int A_0)
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
		this.\u1714 = A_0;
	}

	// Token: 0x06005471 RID: 21617 RVA: 0x00349AF8 File Offset: 0x00348AF8
	public int ᜋ()
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
		return this.\u1715;
	}

	// Token: 0x06005472 RID: 21618 RVA: 0x00349B3C File Offset: 0x00348B3C
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
		this.\u1715 = A_0;
	}

	// Token: 0x06005473 RID: 21619 RVA: 0x00349B80 File Offset: 0x00348B80
	public int ᜎ()
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
		return this.\u1716;
	}

	// Token: 0x06005474 RID: 21620 RVA: 0x00349BC4 File Offset: 0x00348BC4
	public void ᜃ(int A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.\u1716 = A_0;
	}

	// Token: 0x06005475 RID: 21621 RVA: 0x00349C08 File Offset: 0x00348C08
	public int[] ᜂ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return this.\u1717;
	}

	// Token: 0x06005476 RID: 21622 RVA: 0x00349C4C File Offset: 0x00348C4C
	internal void ᜂ(Stream A_0)
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
		byte[] array = new byte[512];
		Buffer.BlockCopy(this.ᜅ, 0, array, 0, 8);
		int num = 8;
		byte[] src = this.ᜆ.ToByteArray();
		Buffer.BlockCopy(src, 0, array, num, 16);
		num += 16;
		src = BitConverter.GetBytes(this.ᜇ);
		Buffer.BlockCopy(src, 0, array, num, 2);
		num += 2;
		src = BitConverter.GetBytes(this.ᜈ);
		Buffer.BlockCopy(src, 0, array, num, 2);
		num += 2;
		src = BitConverter.GetBytes(this.ᜉ);
		Buffer.BlockCopy(src, 0, array, num, 2);
		num += 2;
		src = BitConverter.GetBytes(this.ᜊ);
		Buffer.BlockCopy(src, 0, array, num, 2);
		num += 2;
		src = BitConverter.GetBytes(this.ᜋ);
		Buffer.BlockCopy(src, 0, array, num, 2);
		num += 2;
		src = BitConverter.GetBytes(this.ᜌ);
		Buffer.BlockCopy(src, 0, array, num, 2);
		num += 2;
		src = BitConverter.GetBytes(this.\u170D);
		Buffer.BlockCopy(src, 0, array, num, 4);
		num += 4;
		src = BitConverter.GetBytes(this.ᜎ);
		Buffer.BlockCopy(src, 0, array, num, 4);
		num += 4;
		src = BitConverter.GetBytes(this.ᜏ);
		Buffer.BlockCopy(src, 0, array, num, 4);
		num += 4;
		src = BitConverter.GetBytes(this.ᜐ);
		Buffer.BlockCopy(src, 0, array, num, 4);
		num += 4;
		src = BitConverter.GetBytes(this.ᜑ);
		Buffer.BlockCopy(src, 0, array, num, 4);
		num += 4;
		src = BitConverter.GetBytes(this.\u1712);
		Buffer.BlockCopy(src, 0, array, num, 4);
		num += 4;
		src = BitConverter.GetBytes(this.\u1713);
		Buffer.BlockCopy(src, 0, array, num, 4);
		num += 4;
		src = BitConverter.GetBytes(this.\u1714);
		Buffer.BlockCopy(src, 0, array, num, 4);
		num += 4;
		src = BitConverter.GetBytes(this.\u1715);
		Buffer.BlockCopy(src, 0, array, num, 4);
		num += 4;
		src = BitConverter.GetBytes(this.\u1716);
		Buffer.BlockCopy(src, 0, array, num, 4);
		num += 4;
		Buffer.BlockCopy(this.\u1717, 0, array, num, this.\u1717.Length * 4);
		A_0.Position = 0L;
		A_0.Write(array, 0, 512);
	}

	// Token: 0x06005477 RID: 21623 RVA: 0x00349E8C File Offset: 0x00348E8C
	internal long ᜅ(int A_0)
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
		return (long)((A_0 << (int)this.ᜊ) + 512);
	}

	// Token: 0x06005478 RID: 21624 RVA: 0x00349EDC File Offset: 0x00348EDC
	internal long ᜀ(int A_0, int A_1)
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
		return (long)((A_0 << (int)this.ᜊ) + A_1);
	}

	// Token: 0x06005479 RID: 21625 RVA: 0x00349F28 File Offset: 0x00348F28
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u19E8()
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
		spr\u19E8.ᜄ = new byte[]
		{
			208,
			207,
			17,
			224,
			161,
			177,
			26,
			225
		};
	}

	// Token: 0x040027B6 RID: 10166
	public const int ᜀ = 512;

	// Token: 0x040027B7 RID: 10167
	private const int ᜁ = 8;

	// Token: 0x040027B8 RID: 10168
	internal const int ᜂ = 2;

	// Token: 0x040027B9 RID: 10169
	internal const int ᜃ = 4;

	// Token: 0x040027BA RID: 10170
	private static readonly byte[] ᜄ;

	// Token: 0x040027BB RID: 10171
	private byte[] ᜅ;

	// Token: 0x040027BC RID: 10172
	private Guid ᜆ;

	// Token: 0x040027BD RID: 10173
	private ushort ᜇ;

	// Token: 0x040027BE RID: 10174
	private ushort ᜈ;

	// Token: 0x040027BF RID: 10175
	private ushort ᜉ;

	// Token: 0x040027C0 RID: 10176
	private ushort ᜊ;

	// Token: 0x040027C1 RID: 10177
	private ushort ᜋ;

	// Token: 0x040027C2 RID: 10178
	private ushort ᜌ;

	// Token: 0x040027C3 RID: 10179
	private uint \u170D;

	// Token: 0x040027C4 RID: 10180
	private uint ᜎ;

	// Token: 0x040027C5 RID: 10181
	private int ᜏ;

	// Token: 0x040027C6 RID: 10182
	private int ᜐ;

	// Token: 0x040027C7 RID: 10183
	private int ᜑ;

	// Token: 0x040027C8 RID: 10184
	private uint \u1712;

	// Token: 0x040027C9 RID: 10185
	private int \u1713;

	// Token: 0x040027CA RID: 10186
	private int \u1714;

	// Token: 0x040027CB RID: 10187
	private int \u1715;

	// Token: 0x040027CC RID: 10188
	private int \u1716;

	// Token: 0x040027CD RID: 10189
	private int[] \u1717;
}
