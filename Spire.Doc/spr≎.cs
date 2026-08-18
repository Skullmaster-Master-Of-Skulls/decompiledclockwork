using System;
using System.Drawing;
using System.IO;

// Token: 0x020002BD RID: 701
[CLSCompliant(false)]
internal class spr\u224E : spr\u23F8
{
	// Token: 0x06002623 RID: 9763 RVA: 0x0025D694 File Offset: 0x0025C694
	internal spr\u224E()
	{
	}

	// Token: 0x06002624 RID: 9764 RVA: 0x0025D6BC File Offset: 0x0025C6BC
	internal spr\u224E(byte[] A_0, int A_1)
	{
		this.ᜁ(A_0, A_1);
	}

	// Token: 0x06002625 RID: 9765 RVA: 0x0025D6EC File Offset: 0x0025C6EC
	internal new void ᜁ(byte[] A_0, int A_1)
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
		this.ᜁ = A_0[A_1];
		this.ᜂ = A_0[A_1 + 1];
		this.ᜃ = A_0[A_1 + 2];
		this.ᜅ = A_0[A_1 + 3];
		this.ᜆ = false;
	}

	// Token: 0x06002626 RID: 9766 RVA: 0x0025D758 File Offset: 0x0025C758
	internal new void ᜃ(byte[] A_0, int A_1)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_5A:
			this.ᜄ = sprṡ.ᜀ(BitConverter.ToUInt32(A_0, A_1));
			this.ᜁ = A_0[A_1 + 4];
			this.ᜂ = A_0[A_1 + 5];
			this.ᜅ = A_0[A_1 + 6];
			byte b = A_0[A_1 + 7];
			this.ᜆ = false;
			num = 1;
			break;
		}
		default:
			if (false)
			{
			}
			num = 0;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				break;
			case 1:
				return;
			case 2:
				goto IL_5A;
			}
			if (A_0.Length - A_1 < 8)
			{
				break;
			}
			num = 2;
		}
	}

	// Token: 0x06002627 RID: 9767 RVA: 0x0025D80C File Offset: 0x0025C80C
	internal new void ᜀ(byte[] A_0, int A_1)
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
		A_0[A_1] = this.ᜁ;
		A_0[A_1 + 1] = this.ᜂ;
		A_0[A_1 + 2] = this.ᜃ;
		A_0[A_1 + 3] = this.ᜅ;
		this.ᜆ = false;
	}

	// Token: 0x06002628 RID: 9768 RVA: 0x0025D878 File Offset: 0x0025C878
	internal void ᜂ(byte[] A_0, int A_1)
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
		byte[] bytes = BitConverter.GetBytes(sprṡ.ᜂ(this.ᜄ));
		bytes.CopyTo(A_0, A_1);
		A_0[A_1 + 4] = this.ᜁ;
		A_0[A_1 + 5] = this.ᜂ;
		A_0[A_1 + 6] = this.ᜅ;
		A_0[A_1 + 7] = 0;
		this.ᜆ = false;
	}

	// Token: 0x06002629 RID: 9769 RVA: 0x0025D8FC File Offset: 0x0025C8FC
	internal void ᜀ(BinaryReader A_0)
	{
		for (;;)
		{
			this.ᜁ = A_0.ReadByte();
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_91;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B2;
					default:
						if (false)
						{
						}
						this.ᜆ = false;
						num = 0;
						continue;
					}
					break;
				case 2:
					goto IL_B2;
				case 3:
					this.ᜂ = A_0.ReadByte();
					num = 2;
					continue;
				case 4:
					if (this.ᜁ != 255)
					{
						num = 3;
						continue;
					}
					goto IL_D1;
				}
				break;
				IL_B2:
				if (this.ᜂ == 0)
				{
					goto IL_4A;
				}
				num = 1;
			}
		}
		IL_4A:
		this.ᜃ = A_0.ReadByte();
		this.ᜅ = A_0.ReadByte();
		return;
		IL_91:
		if (true)
		{
		}
		goto IL_4A;
		IL_D1:
		A_0.ReadByte();
		A_0.ReadByte();
		A_0.ReadByte();
	}

	// Token: 0x0600262A RID: 9770 RVA: 0x0025D9F0 File Offset: 0x0025C9F0
	internal void ᜀ(Stream A_0)
	{
		while (!this.ᜁ())
		{
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
				A_0.WriteByte(this.ᜁ);
				A_0.WriteByte(this.ᜂ);
				A_0.WriteByte(this.ᜃ);
				A_0.WriteByte(this.ᜅ);
				return;
			}
		}
		spr\u23F8.ᜀ(A_0, uint.MaxValue);
	}

	// Token: 0x0600262B RID: 9771 RVA: 0x0025DA70 File Offset: 0x0025CA70
	internal spr\u224E ᜌ()
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
		return base.MemberwiseClone() as spr\u224E;
	}

	// Token: 0x0600262C RID: 9772 RVA: 0x0025DAB8 File Offset: 0x0025CAB8
	internal byte ᜊ()
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

	// Token: 0x0600262D RID: 9773 RVA: 0x0025DAFC File Offset: 0x0025CAFC
	internal void ᜀ(byte A_0)
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
		this.ᜆ = false;
	}

	// Token: 0x0600262E RID: 9774 RVA: 0x0025DB48 File Offset: 0x0025CB48
	internal float ᜈ()
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
		return (float)this.ᜁ / 8f;
	}

	// Token: 0x0600262F RID: 9775 RVA: 0x0025DB90 File Offset: 0x0025CB90
	internal void ᜀ(float A_0)
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
		this.ᜁ = (byte)(A_0 * 8f);
	}

	// Token: 0x06002630 RID: 9776 RVA: 0x0025DBDC File Offset: 0x0025CBDC
	internal byte ᜄ()
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
		return this.ᜂ;
	}

	// Token: 0x06002631 RID: 9777 RVA: 0x0025DC20 File Offset: 0x0025CC20
	internal new void ᜃ(byte A_0)
	{
		for (;;)
		{
			for (;;)
			{
				this.ᜂ = A_0;
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜆ = false;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 1:
						return;
					case 2:
						if (A_0 != 0)
						{
							num = 0;
							continue;
						}
						return;
					}
					break;
				}
			}
		}
	}

	// Token: 0x06002632 RID: 9778 RVA: 0x0025DC9C File Offset: 0x0025CC9C
	internal byte ᜆ()
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
		return this.ᜅ & 31;
	}

	// Token: 0x06002633 RID: 9779 RVA: 0x0025DCE4 File Offset: 0x0025CCE4
	internal void ᜂ(byte A_0)
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
		this.ᜅ &= 224;
		this.ᜅ += A_0;
		this.ᜆ = false;
	}

	// Token: 0x06002634 RID: 9780 RVA: 0x0025DD4C File Offset: 0x0025CD4C
	internal float ᜂ()
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
		return (float)this.ᜆ();
	}

	// Token: 0x06002635 RID: 9781 RVA: 0x0025DD90 File Offset: 0x0025CD90
	internal bool ᜋ()
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
		byte b = this.ᜅ & 32;
		b = (byte)(b >> 5);
		return b == 1;
	}

	// Token: 0x06002636 RID: 9782 RVA: 0x0025DDE0 File Offset: 0x0025CDE0
	internal void ᜀ(bool A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_59:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			num = 3;
			break;
		}
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_6E;
			case 1:
				goto IL_59;
			case 2:
				goto IL_63;
			}
			if (!A_0)
			{
				num = 1;
			}
			else
			{
				num = 2;
			}
		}
		IL_63:
		byte b = 1;
		goto IL_71;
		IL_6E:
		b = 0;
		IL_71:
		byte b2 = b;
		this.ᜅ &= 223;
		b2 = (byte)(b2 << 5);
		this.ᜅ += b2;
		this.ᜆ = false;
	}

	// Token: 0x06002637 RID: 9783 RVA: 0x0025DE90 File Offset: 0x0025CE90
	internal byte ᜉ()
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
		return this.ᜃ;
	}

	// Token: 0x06002638 RID: 9784 RVA: 0x0025DED4 File Offset: 0x0025CED4
	internal void ᜁ(byte A_0)
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
		this.ᜃ = A_0;
		this.ᜆ = false;
	}

	// Token: 0x06002639 RID: 9785 RVA: 0x0025DF20 File Offset: 0x0025CF20
	internal Color ᜅ()
	{
		while (this.ᜄ == Color.Empty)
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
				return sprṡ.ᜀ((int)this.ᜃ);
			}
		}
		return this.ᜄ;
	}

	// Token: 0x0600263A RID: 9786 RVA: 0x0025DF84 File Offset: 0x0025CF84
	internal void ᜀ(Color A_0)
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
		this.ᜄ = A_0;
		this.ᜆ = false;
	}

	// Token: 0x0600263B RID: 9787 RVA: 0x0025DFD0 File Offset: 0x0025CFD0
	internal bool ᜀ()
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
		return this.ᜆ;
	}

	// Token: 0x0600263C RID: 9788 RVA: 0x0025E014 File Offset: 0x0025D014
	internal bool ᜁ()
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
		return this.ᜁ == byte.MaxValue;
	}

	// Token: 0x04002224 RID: 8740
	internal new const int ᜀ = 8;

	// Token: 0x04002225 RID: 8741
	private new byte ᜁ;

	// Token: 0x04002226 RID: 8742
	private new byte ᜂ;

	// Token: 0x04002227 RID: 8743
	private new byte ᜃ;

	// Token: 0x04002228 RID: 8744
	private new Color ᜄ = Color.Empty;

	// Token: 0x04002229 RID: 8745
	private new byte ᜅ;

	// Token: 0x0400222A RID: 8746
	private bool ᜆ = true;
}
