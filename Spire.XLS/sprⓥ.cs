using System;
using System.IO;
using System.Text;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002C1 RID: 705
internal class spr\u24E5 : DataProvider
{
	// Token: 0x06002A9E RID: 10910 RVA: 0x0017D004 File Offset: 0x0017C004
	public spr\u24E5() : this(new byte[128])
	{
	}

	// Token: 0x06002A9F RID: 10911 RVA: 0x0017D024 File Offset: 0x0017C024
	public spr\u24E5(byte[] A_0)
	{
		int a_ = 6;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("崻䰽㈿ف╃㉅⥇", a_));
		}
		this.ᜀ = A_0;
	}

	// Token: 0x06002AA0 RID: 10912 RVA: 0x0017D060 File Offset: 0x0017C060
	public byte[] ᜅ()
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

	// Token: 0x06002AA1 RID: 10913 RVA: 0x0017D0A4 File Offset: 0x0017C0A4
	public virtual int ᜂ()
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
		return this.ᜀ.Length;
	}

	// Token: 0x06002AA2 RID: 10914 RVA: 0x0017D0E8 File Offset: 0x0017C0E8
	public virtual bool ᜁ()
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
		return this.ᜀ == null;
	}

	// Token: 0x06002AA3 RID: 10915 RVA: 0x0017D12C File Offset: 0x0017C12C
	public virtual byte ᜁ(int A_0)
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
		return this.ᜀ[A_0];
	}

	// Token: 0x06002AA4 RID: 10916 RVA: 0x0017D170 File Offset: 0x0017C170
	public virtual short ᜃ(int A_0)
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
		return BitConverter.ToInt16(this.ᜀ, A_0);
	}

	// Token: 0x06002AA5 RID: 10917 RVA: 0x0017D1B8 File Offset: 0x0017C1B8
	public virtual int ᜂ(int A_0)
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
		return BitConverter.ToInt32(this.ᜀ, A_0);
	}

	// Token: 0x06002AA6 RID: 10918 RVA: 0x0017D200 File Offset: 0x0017C200
	public virtual long ᜀ(int A_0)
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
		return BitConverter.ToInt64(this.ᜀ, A_0);
	}

	// Token: 0x06002AA7 RID: 10919 RVA: 0x0017D248 File Offset: 0x0017C248
	public virtual void ᜁ(int A_0, byte[] A_1, int A_2, int A_3)
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
		Buffer.BlockCopy(this.ᜀ, A_0, A_1, A_2, A_3);
	}

	// Token: 0x06002AA8 RID: 10920 RVA: 0x0017D294 File Offset: 0x0017C294
	public virtual void ᜀ(int A_0, DataProvider A_1, int A_2, int A_3)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 2:
				A_1.WriteBytes(A_2, this.ᜀ, A_0, A_3);
				num = 0;
				continue;
			}
			IL_26:
			if (true)
			{
			}
			if (A_3 <= 0)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				num = 2;
				continue;
			}
			goto IL_26;
		}
	}

	// Token: 0x06002AA9 RID: 10921 RVA: 0x0017D314 File Offset: 0x0017C314
	public virtual void ᜀ(BinaryReader A_0, int A_1, int A_2, byte[] A_3)
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
			if (A_1 + A_2 <= this.ᜀ.Length)
			{
				A_0.Read(this.ᜀ, A_1, A_2);
				return;
			}
			break;
		}
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException();
	}

	// Token: 0x06002AAA RID: 10922 RVA: 0x0017D374 File Offset: 0x0017C374
	public virtual string ᜀ(int A_0, int A_1, Encoding A_2, bool A_3)
	{
		int num = 5;
		for (;;)
		{
			Encoding encoding;
			switch (num)
			{
			case 0:
				goto IL_A8;
			case 1:
				encoding = Encoding.Unicode;
				goto IL_91;
			case 2:
				encoding = Encoding.ASCII;
				goto IL_91;
			case 3:
				num = 2;
				continue;
			case 4:
				num = 6;
				continue;
			case 5:
				if (true)
				{
				}
				break;
			case 6:
				if (!A_3)
				{
					num = 3;
					continue;
				}
				num = 1;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				if (false)
				{
				}
				if (A_2 == null)
				{
					num = 4;
					continue;
				}
				goto IL_AA;
			}
			IL_91:
			A_2 = encoding;
			num = 0;
		}
		IL_A8:
		IL_AA:
		return A_2.GetString(this.ᜀ, A_0, A_1);
	}

	// Token: 0x06002AAB RID: 10923 RVA: 0x0017D43C File Offset: 0x0017C43C
	public virtual void ᜄ(int A_0)
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
		this.EnsureCapacity(A_0, 0);
	}

	// Token: 0x06002AAC RID: 10924 RVA: 0x0017D480 File Offset: 0x0017C480
	public virtual void ᜁ(int A_0, int A_1)
	{
		int num = 5;
		for (;;)
		{
			byte[] dst;
			int num2;
			int num3;
			switch (num)
			{
			case 0:
				Buffer.BlockCopy(this.ᜀ, 0, dst, 0, num2);
				num = 6;
				continue;
			case 1:
				if (num2 > 0)
				{
					num = 0;
					continue;
				}
				goto IL_D8;
			case 2:
				num = 9;
				continue;
			case 3:
				dst = new byte[A_0];
				num = 1;
				continue;
			case 4:
				return;
			case 6:
				goto IL_D8;
			case 7:
				num3 = this.ᜀ.Length;
				goto IL_98;
			case 8:
				if (num2 < A_0)
				{
					num = 3;
					continue;
				}
				return;
			case 9:
				if (true)
				{
				}
				num3 = 0;
				goto IL_98;
			}
			if (this.ᜀ == null)
			{
				num = 2;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_DF;
			default:
				if (false)
				{
				}
				num = 7;
				continue;
			}
			IL_98:
			num2 = num3;
			num = 8;
			continue;
			IL_DF:
			num = 4;
			continue;
			IL_D8:
			this.ᜀ = dst;
			goto IL_DF;
		}
	}

	// Token: 0x06002AAD RID: 10925 RVA: 0x0017D598 File Offset: 0x0017C598
	public virtual void ᜄ()
	{
		for (;;)
		{
			IL_3E:
			if (true)
			{
			}
			int num = 0;
			int num2 = this.ᜀ.Length;
			int num3 = 0;
			for (;;)
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
					switch (num3)
					{
					case 0:
						goto IL_5B;
					case 1:
						return;
					case 2:
						goto IL_5B;
					case 3:
						if (num >= num2)
						{
							goto IL_67;
						}
						this.ᜀ[num] = 0;
						num++;
						num3 = 2;
						continue;
					}
					goto IL_3E;
					IL_5B:
					num3 = 3;
					continue;
				}
				IL_67:
				num3 = 1;
			}
		}
	}

	// Token: 0x06002AAE RID: 10926 RVA: 0x0017D630 File Offset: 0x0017C630
	public virtual void ᜀ(int A_0, byte A_1)
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
		this.ᜀ[A_0] = A_1;
	}

	// Token: 0x06002AAF RID: 10927 RVA: 0x0017D674 File Offset: 0x0017C674
	public virtual void ᜀ(int A_0, short A_1)
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
		BitConverter.GetBytes(A_1).CopyTo(this.ᜀ, A_0);
	}

	// Token: 0x06002AB0 RID: 10928 RVA: 0x0017D6C4 File Offset: 0x0017C6C4
	[CLSCompliant(false)]
	public virtual void ᜀ(int A_0, ushort A_1)
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
		BitConverter.GetBytes(A_1).CopyTo(this.ᜀ, A_0);
	}

	// Token: 0x06002AB1 RID: 10929 RVA: 0x0017D714 File Offset: 0x0017C714
	public virtual void ᜀ(int A_0, int A_1)
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
		BitConverter.GetBytes(A_1).CopyTo(this.ᜀ, A_0);
	}

	// Token: 0x06002AB2 RID: 10930 RVA: 0x0017D764 File Offset: 0x0017C764
	public virtual void ᜀ(int A_0, long A_1)
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
		BitConverter.GetBytes(A_1).CopyTo(this.ᜀ, A_0);
	}

	// Token: 0x06002AB3 RID: 10931 RVA: 0x0017D7B4 File Offset: 0x0017C7B4
	public virtual void ᜀ(int A_0, bool A_1, int A_2)
	{
		int a_ = 17;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_2 > 7)
				{
					num = 1;
					continue;
				}
				num = 3;
				continue;
			case 1:
				goto IL_B4;
			case 3:
				if (A_1)
				{
					num = 4;
					continue;
				}
				goto IL_E1;
			case 4:
				goto IL_76;
			case 5:
				num = 0;
				continue;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (A_2 < 0)
				{
					goto IL_78;
				}
				num = 5;
				break;
			}
		}
		IL_76:
		byte[] array = this.ᜀ;
		array[A_0] |= (byte)(1 << A_2);
		return;
		IL_78:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("╆⁈㽊ᵌ⁎≐", a_), RecordTableEnumerator.b("Ն⁈㽊浌὎㹐⁒㱔⍖じ㑚㍜罞ɠɢ୤䝦୨๪䵬ᕮᑰŲᩴ坶ᙸॺ嵼᡾力권ﮎ戀ﮔ랖꺘떚", a_));
		IL_B4:
		goto IL_78;
		IL_E1:
		byte[] array2 = this.ᜀ;
		array2[A_0] &= (byte)(~(byte)(1 << A_2));
	}

	// Token: 0x06002AB4 RID: 10932 RVA: 0x0017D8C4 File Offset: 0x0017C8C4
	public virtual void ᜀ(int A_0, double A_1)
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
		BitConverter.GetBytes(A_1).CopyTo(this.ᜀ, A_0);
	}

	// Token: 0x06002AB5 RID: 10933 RVA: 0x0017D914 File Offset: 0x0017C914
	public virtual void ᜀ(ref int A_0, string A_1, bool A_2)
	{
		int num = 8;
		byte[] bytes;
		for (;;)
		{
			Encoding encoding;
			switch (num)
			{
			case 0:
				if (!A_2)
				{
					num = 4;
					continue;
				}
				num = 2;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_37;
				default:
					goto IL_C5;
				}
				break;
			case 2:
				encoding = Encoding.Unicode;
				goto IL_73;
			case 3:
				encoding = Encoding.ASCII;
				goto IL_73;
			case 4:
				num = 3;
				continue;
			case 5:
				num = 7;
				continue;
			case 6:
				goto IL_84;
			case 7:
				if (A_1.Length == 0)
				{
					num = 1;
					continue;
				}
				num = 0;
				continue;
			}
			goto IL_34;
			IL_37:
			num = 5;
			continue;
			IL_34:
			if (A_1 != null)
			{
				goto IL_37;
			}
			break;
			IL_73:
			Encoding encoding2 = encoding;
			bytes = encoding2.GetBytes(A_1);
			num = 6;
		}
		return;
		IL_84:
		this.ᜀ[A_0] = (A_2 ? 1 : 0);
		A_0++;
		int num2 = bytes.Length;
		Buffer.BlockCopy(bytes, 0, this.ᜀ, A_0, num2);
		A_0 += num2;
		return;
		IL_C5:
		if (false)
		{
		}
		if (true)
		{
		}
	}

	// Token: 0x06002AB6 RID: 10934 RVA: 0x0017DA30 File Offset: 0x0017CA30
	public virtual void ᜀ(int A_0, byte[] A_1, int A_2, int A_3)
	{
		int a_ = 7;
		int num = 2;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_BA;
			case 1:
				goto IL_125;
			case 3:
				return;
			case 4:
				if (A_2 < 0)
				{
					num = 7;
					continue;
				}
				num = 9;
				continue;
			case 5:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				num = 4;
				continue;
			case 6:
				goto IL_EB;
			case 7:
				goto IL_166;
			case 8:
				if (A_2 + A_3 > A_1.Length)
				{
					num = 6;
					continue;
				}
				goto IL_17F;
			case 9:
				if (A_3 < 0)
				{
					num = 1;
					continue;
				}
				num = 8;
				continue;
			}
			if (A_3 == 0)
			{
				num = 3;
			}
			else
			{
				num = 5;
			}
		}
		return;
		IL_BA:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬼帾ⵀ㙂⁄", a_));
		IL_EB:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return;
		default:
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䬼帾ⵀ㙂⁄", a_), RecordTableEnumerator.b("洼倾㉀⩂ㅄ⹆♈╊浌⁎⍐獒㥔㉖㝘㱚⥜㝞䅠ୢѤᑦ䥨ᱪὬnὰᑲ啴Ŷᡸ᝺ࡼ᩾꾀", a_));
		}
		IL_125:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("儼娾⽀⑂ㅄ⽆", a_), RecordTableEnumerator.b("焼娾⽀⑂ㅄ⽆楈⑊⭌潎㕐㉒⅔㙖祘⽚㉜罞ɠౢᕤṦ䥨٪ᡬᱮհ卲᝴ቶ奸ᱺོ᩾ꦈﾊﾐ뎒뎜", a_));
		IL_166:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䴼倾㉀", a_), RecordTableEnumerator.b("洼倾㉀⩂ㅄ⹆♈╊浌ⱎぐ㵒㭔㡖ⵘ筚㽜㩞䅠ᥢdᕦ٨ݪ࡬ᱮɰ嵲", a_));
		IL_17F:
		Buffer.BlockCopy(A_1, A_2, this.ᜀ, A_0, A_3);
	}

	// Token: 0x06002AB7 RID: 10935 RVA: 0x0017DBCC File Offset: 0x0017CBCC
	public virtual void ᜀ(BinaryWriter A_0, int A_1, int A_2, byte[] A_3)
	{
		int a_ = 1;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			if (A_0 != null)
			{
				A_0.Write(this.ᜀ, A_1, A_2);
				return;
			}
			break;
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
	}

	// Token: 0x06002AB8 RID: 10936 RVA: 0x0017DC38 File Offset: 0x0017CC38
	internal void ᜀ(byte[] A_0)
	{
		int a_ = 1;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			if (A_0 != null)
			{
				if (true)
				{
				}
				this.ᜀ = A_0;
				return;
			}
			break;
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("嘶䬸䤺猼娾㙀łいⅆ⽈⹊㽌", a_));
	}

	// Token: 0x06002AB9 RID: 10937 RVA: 0x0017DC9C File Offset: 0x0017CC9C
	protected virtual void ᜀ()
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
		this.ᜀ = null;
	}

	// Token: 0x06002ABA RID: 10938 RVA: 0x0017DCE0 File Offset: 0x0017CCE0
	public virtual void ᜃ()
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
		this.ᜀ = null;
	}

	// Token: 0x06002ABB RID: 10939 RVA: 0x0017DD24 File Offset: 0x0017CD24
	public virtual void ᜁ(int A_0, int A_1, int A_2)
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
		Buffer.BlockCopy(this.ᜀ, A_1, this.ᜀ, A_0, A_2);
	}

	// Token: 0x06002ABC RID: 10940 RVA: 0x0017DD74 File Offset: 0x0017CD74
	public virtual void ᜀ(int A_0, int A_1, int A_2)
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
		Buffer.BlockCopy(this.ᜀ, A_1, this.ᜀ, A_0, A_2);
	}

	// Token: 0x06002ABD RID: 10941 RVA: 0x0017DDC4 File Offset: 0x0017CDC4
	public virtual DataProvider ᜆ()
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
		return new spr\u24E5();
	}

	// Token: 0x0400142D RID: 5165
	private byte[] ᜀ;
}
