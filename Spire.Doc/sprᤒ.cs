using System;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc.Core.Biff_Records;

// Token: 0x020001AB RID: 427
[CLSCompliant(false)]
internal class sprᤒ : spr\u23F8
{
	// Token: 0x060010BA RID: 4282 RVA: 0x000FC6BC File Offset: 0x000FB6BC
	internal sprᤒ()
	{
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x000FC6DC File Offset: 0x000FB6DC
	internal sprᤒ(byte[] A_0)
	{
		this.ᜂ(A_0);
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x000FC704 File Offset: 0x000FB704
	internal sprᤒ(byte[] A_0, int A_1)
	{
		this.ᜁ(A_0, A_1);
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x000FC72C File Offset: 0x000FB72C
	internal sprᤒ(byte[] A_0, int A_1, int A_2)
	{
		this.ᜀ(A_0, A_1, A_2);
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x000FC754 File Offset: 0x000FB754
	internal sprᤒ(Stream A_0, int A_1, bool A_2)
	{
		this.ᜂ = A_2;
		this.ᜀ(A_0, A_1);
	}

	// Token: 0x060010BF RID: 4287 RVA: 0x000FC784 File Offset: 0x000FB784
	internal sprᤒ(spr\u2472 A_0)
	{
		byte[] array = A_0.ᜀ();
		this.ᜀ(array, 0, array.Length);
	}

	// Token: 0x060010C0 RID: 4288 RVA: 0x000FC7B4 File Offset: 0x000FB7B4
	internal override void ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 5;
		int num = 12;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_14E;
			case 1:
				if (A_1 >= 0)
				{
					if (true)
					{
					}
					num = 4;
					continue;
				}
				goto IL_150;
			case 2:
				goto IL_180;
			case 3:
				if (A_2 < 2)
				{
					num = 7;
					continue;
				}
				num = 11;
				continue;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_DE;
				default:
					if (false)
					{
					}
					num = 9;
					continue;
				}
				break;
			case 5:
				this.ᜀ = BitConverter.ToUInt16(A_0, 0);
				A_1 += 2;
				A_2 -= 2;
				num = 0;
				continue;
			case 6:
				goto IL_11D;
			case 7:
				goto IL_19F;
			case 8:
				goto IL_58;
			case 9:
				if (A_1 > A_0.Length)
				{
					num = 2;
					continue;
				}
				num = 3;
				continue;
			case 10:
				if (!this.ᜂ)
				{
					goto IL_DE;
				}
				goto IL_1A4;
			case 11:
				if (A_2 + A_1 > A_0.Length)
				{
					num = 6;
					continue;
				}
				num = 10;
				continue;
			}
			if (A_0 == null)
			{
				num = 8;
				continue;
			}
			num = 1;
			continue;
			IL_DE:
			num = 5;
		}
		IL_58:
		throw new ArgumentNullException(ClipboardData.b("੪Ὤᵮ㕰ቲŴᙶ", a_));
		IL_11D:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ɪ⹬nѰᵲŴ坶剸孺ᑼまﶈ", a_));
		IL_14E:
		goto IL_1A4;
		IL_150:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ɪ≬८ᝰrၴͶ", a_));
		IL_180:
		goto IL_150;
		IL_19F:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ɪ⹬nѰᵲŴ", a_));
		IL_1A4:
		this.ᜁ.ᜀ(A_0, A_1, A_2);
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x000FC974 File Offset: 0x000FB974
	internal override int ᜄ(Stream A_0)
	{
		int num;
		for (;;)
		{
			num = 2;
			A_0.Write(BitConverter.GetBytes(this.ᜀ), 0, 2);
			if (true)
			{
			}
			int num2 = 2;
			for (;;)
			{
				IL_02:
				switch (num2)
				{
				case 0:
					return num;
				case 1:
					num += this.ᜁ.ᜄ(A_0);
					num2 = 0;
					continue;
				case 2:
					while (this.ᜁ != null)
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
							num2 = 1;
							goto IL_02;
						}
					}
					return num;
				}
				break;
			}
		}
		return num;
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x000FCA10 File Offset: 0x000FBA10
	internal int ᜀ(Stream A_0)
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 1;
			for (;;)
			{
				IL_02:
				switch (num2)
				{
				case 0:
					return num;
				case 1:
					while (this.ᜁ != null)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						if (false)
						{
						}
						num2 = 2;
						goto IL_02;
					}
					return num;
				case 2:
					num += this.ᜁ.ᜄ(A_0);
					if (true)
					{
					}
					num2 = 0;
					continue;
				}
				break;
			}
		}
		return num;
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x000FCA98 File Offset: 0x000FBA98
	internal ushort ᜄ()
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
		return this.ᜀ;
	}

	// Token: 0x060010C4 RID: 4292 RVA: 0x000FCADC File Offset: 0x000FBADC
	internal void ᜀ(ushort A_0)
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

	// Token: 0x060010C5 RID: 4293 RVA: 0x000FCB20 File Offset: 0x000FBB20
	internal sprḍ ᜁ()
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
		return this.ᜁ;
	}

	// Token: 0x060010C6 RID: 4294 RVA: 0x000FCB64 File Offset: 0x000FBB64
	internal void ᜀ(sprḍ A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x060010C7 RID: 4295 RVA: 0x000FCBA8 File Offset: 0x000FBBA8
	internal int ᜉ()
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
		return this.ᜁ.ᜈ();
	}

	// Token: 0x060010C8 RID: 4296 RVA: 0x000FCBF0 File Offset: 0x000FBBF0
	internal override int ᜇ()
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
		return 2 + this.ᜁ.ᜇ();
	}

	// Token: 0x060010C9 RID: 4297 RVA: 0x000FCC38 File Offset: 0x000FBC38
	internal ushort ᜆ()
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
		return this.ᜁ.ᜁ(17920, 0);
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x000FCC84 File Offset: 0x000FBC84
	internal void ᜁ(ushort A_0)
	{
		if (true)
		{
		}
		int num = 1;
		for (;;)
		{
			IL_12:
			switch (num)
			{
			case 0:
				this.ᜁ.ᜀ(17920, A_0);
				num = 2;
				continue;
			case 2:
				return;
			}
			while (this.ᜆ() != A_0)
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
					num = 0;
					goto IL_12;
				}
			}
			break;
		}
	}

	// Token: 0x060010CB RID: 4299 RVA: 0x000FCD0C File Offset: 0x000FBD0C
	internal ParagraphJustify ᜂ()
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
		return (ParagraphJustify)this.ᜁ.ᜀ(9219, 0);
	}

	// Token: 0x060010CC RID: 4300 RVA: 0x000FCD58 File Offset: 0x000FBD58
	internal void ᜀ(ParagraphJustify A_0)
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
		this.ᜁ.ᜁ(9219, (byte)A_0);
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x000FCDA8 File Offset: 0x000FBDA8
	internal bool ᜅ()
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
		return this.ᜁ.ᜀ(9221, false);
	}

	// Token: 0x060010CE RID: 4302 RVA: 0x000FCDF4 File Offset: 0x000FBDF4
	internal void ᜁ(bool A_0)
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
		this.ᜁ.ᜁ(9221, A_0);
	}

	// Token: 0x060010CF RID: 4303 RVA: 0x000FCE40 File Offset: 0x000FBE40
	internal bool ᜊ()
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
		return this.ᜁ.ᜀ(9222, false);
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x000FCE8C File Offset: 0x000FBE8C
	internal void ᜀ(bool A_0)
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
		this.ᜁ.ᜁ(9222, A_0);
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x000FCED8 File Offset: 0x000FBED8
	internal bool ᜈ()
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
		return this.ᜁ.ᜀ(9222, false);
	}

	// Token: 0x060010D2 RID: 4306 RVA: 0x000FCF24 File Offset: 0x000FBF24
	internal void ᜂ(bool A_0)
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
		this.ᜁ.ᜁ(9222, A_0);
	}

	// Token: 0x040017D2 RID: 6098
	protected new ushort ᜀ;

	// Token: 0x040017D3 RID: 6099
	protected new sprḍ ᜁ = new sprḍ();

	// Token: 0x040017D4 RID: 6100
	private new bool ᜂ;
}
