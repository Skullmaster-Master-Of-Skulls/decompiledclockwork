using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

// Token: 0x02000310 RID: 784
[CLSCompliant(false)]
internal class sprḊ : spr\u2276
{
	// Token: 0x06002ACD RID: 10957 RVA: 0x002A3310 File Offset: 0x002A2310
	internal sprḊ(Stream A_0, sprᾱ A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06002ACE RID: 10958 RVA: 0x002A332C File Offset: 0x002A232C
	internal sprḊ()
	{
		this.ᜁ = new sprḊ.ᜁ();
	}

	// Token: 0x06002ACF RID: 10959 RVA: 0x002A3354 File Offset: 0x002A2354
	internal override void ᜀ(Stream A_0, sprᾱ A_1)
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
		this.ᜀ(A_1.ᜬ(), A_1.ᝨ());
		this.ᜁ = new sprḊ.ᜁ(this.ᜅ, A_1);
	}

	// Token: 0x06002AD0 RID: 10960 RVA: 0x002A33BC File Offset: 0x002A23BC
	internal override void ᜁ(Stream A_0, sprᾱ A_1)
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
		base.ᜁ(A_0, A_1);
		this.ᜄ();
		this.ᜁ.ᜀ(this.ᜆ, A_1);
	}

	// Token: 0x06002AD1 RID: 10961 RVA: 0x002A3418 File Offset: 0x002A2418
	internal new void ᜀ(sprᝦ A_0, int A_1, int A_2, int A_3)
	{
		for (;;)
		{
			int num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_47:
				if (num == -1)
				{
					return;
				}
				num2 = 1;
				break;
			default:
				if (false)
				{
				}
				this.ᜀ(A_0, A_1);
				num = A_0.ᜁ();
				num2 = 2;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					this.ᜁ.ᜀ(num, new sprḊ.ᜀ(A_2, A_3));
					if (true)
					{
					}
					num2 = 0;
					continue;
				case 2:
					goto IL_47;
				}
				break;
			}
		}
	}

	// Token: 0x06002AD2 RID: 10962 RVA: 0x002A34B0 File Offset: 0x002A24B0
	internal new void ᜀ(sprᝦ A_0, int A_1)
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
		this.ᜃ.Add(A_0);
		base.ᜋ(A_1);
	}

	// Token: 0x06002AD3 RID: 10963 RVA: 0x002A3500 File Offset: 0x002A2500
	internal new int ᜀ(string A_0)
	{
		int num;
		for (;;)
		{
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_45:
				if (num != -1)
				{
					return num;
				}
				num2 = 2;
				break;
			default:
				if (false)
				{
				}
				num = this.ᜀ.IndexOf(A_0);
				num2 = 0;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_45;
				case 1:
					return num;
				case 2:
					if (true)
					{
					}
					num = this.ᜀ.Count;
					this.ᜀ.Add(A_0);
					num2 = 1;
					continue;
				}
				break;
			}
		}
		return num;
	}

	// Token: 0x06002AD4 RID: 10964 RVA: 0x002A3598 File Offset: 0x002A2598
	internal new sprᝦ ᜃ(int A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 < this.ᜃ.Count)
				{
					num = 3;
					continue;
				}
				goto IL_AE;
			case 1:
				goto IL_86;
			case 3:
				this.ᜂ = this.ᜃ[A_0];
				this.ᜃ = A_0;
				num = 1;
				continue;
			case 4:
				num = 0;
				continue;
			}
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
				if (A_0 == this.ᜃ)
				{
					goto IL_AE;
				}
				break;
			}
			num = 4;
		}
		IL_86:
		IL_AE:
		return this.ᜂ;
	}

	// Token: 0x06002AD5 RID: 10965 RVA: 0x002A365C File Offset: 0x002A265C
	internal new string ᜄ(int A_0)
	{
		sprᝦ sprᝦ;
		for (;;)
		{
			sprᝦ = this.ᜃ(A_0);
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_56:
				if (sprᝦ == null)
				{
					num = 2;
				}
				else
				{
					num = 0;
				}
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num = 3;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					if ((int)sprᝦ.ᜄ() < this.ᜀ.Count)
					{
						num = 1;
						continue;
					}
					goto IL_A8;
				case 1:
					goto IL_A0;
				case 2:
					goto IL_61;
				case 3:
					goto IL_56;
				}
				break;
			}
		}
		IL_61:
		return "";
		IL_A0:
		return this.ᜀ[(int)sprᝦ.ᜄ()].ToString();
		IL_A8:
		return "";
	}

	// Token: 0x06002AD6 RID: 10966 RVA: 0x002A3718 File Offset: 0x002A2718
	internal new int ᜁ(int A_0)
	{
		sprḊ.ᜀ ᜀ;
		for (;;)
		{
			sprᝦ sprᝦ = this.ᜃ(A_0);
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (sprᝦ.ᜁ() != -1)
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
							ᜀ = this.ᜁ.ᜀ(sprᝦ.ᜁ());
							num = 1;
							continue;
						}
					}
					num = 3;
					continue;
				case 1:
					if (ᜀ == null)
					{
						num = 2;
						continue;
					}
					goto IL_9A;
				case 2:
					return 0;
				case 3:
					return 0;
				}
				break;
			}
		}
		return 0;
		IL_9A:
		return this.ᜂ[A_0] - ᜀ.ᜁ();
	}

	// Token: 0x06002AD7 RID: 10967 RVA: 0x002A37D4 File Offset: 0x002A27D4
	internal new int ᜀ(int A_0)
	{
		sprḊ.ᜀ ᜀ;
		for (;;)
		{
			sprᝦ sprᝦ = this.ᜃ(A_0);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return 0;
				case 1:
					if (ᜀ == null)
					{
						num = 3;
						continue;
					}
					goto IL_9A;
				case 2:
					if (true)
					{
					}
					if (sprᝦ.ᜁ() != -1)
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
							ᜀ = this.ᜁ.ᜀ(sprᝦ.ᜁ());
							num = 1;
							continue;
						}
					}
					num = 0;
					continue;
				case 3:
					return 0;
				}
				break;
			}
		}
		return 0;
		IL_9A:
		return ᜀ.ᜀ() - this.ᜂ[A_0];
	}

	// Token: 0x06002AD8 RID: 10968 RVA: 0x002A3890 File Offset: 0x002A2890
	internal new int ᜂ(int A_0)
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
		return this.ᜂ[A_0];
	}

	// Token: 0x06002AD9 RID: 10969 RVA: 0x002A38D8 File Offset: 0x002A28D8
	private new void ᜀ(int A_0, int A_1)
	{
		int num = 3;
		for (;;)
		{
			if (true)
			{
			}
			int num2;
			int num3;
			switch (num)
			{
			case 0:
				num2 = 0;
				goto IL_9F;
			case 1:
				goto IL_9D;
			case 2:
				if (this.ᜅ.BaseStream.Position == (long)(A_0 + A_1))
				{
					num = 6;
					continue;
				}
				num = 4;
				continue;
			case 4:
				num2 = (int)this.ᜅ.ReadInt16();
				goto IL_9F;
			case 5:
				this.ᜅ.BaseStream.Position = (long)A_0;
				num3 = (int)this.ᜅ.ReadInt16();
				num = 1;
				continue;
			case 6:
				num = 0;
				continue;
			case 7:
				return;
			case 8:
			{
				if (num3 == 0)
				{
					num = 7;
					continue;
				}
				string @string = Encoding.Unicode.GetString(this.ᜅ.ReadBytes(num3 * 2));
				this.ᜀ(@string);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9D;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			}
			case 9:
				goto IL_51;
			}
			if (A_1 > 0)
			{
				num = 5;
				continue;
			}
			break;
			IL_51:
			num = 8;
			continue;
			IL_9D:
			goto IL_51;
			IL_9F:
			num3 = num2;
			num = 9;
		}
	}

	// Token: 0x06002ADA RID: 10970 RVA: 0x002A3A24 File Offset: 0x002A2A24
	private new void ᜄ()
	{
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4D;
					default:
					{
						if (false)
						{
						}
						this.ᜀ.\u1719((int)this.ᜆ.BaseStream.Position);
						int num2 = 0;
						int count = this.ᜀ.Count;
						num = 3;
						continue;
					}
					}
					break;
				case 1:
					goto IL_58;
				case 2:
					this.ᜀ.ᝠ((int)(this.ᜆ.BaseStream.Position - (long)this.ᜀ.ᜬ()));
					num = 4;
					continue;
				case 3:
					goto IL_58;
				case 4:
					return;
				case 5:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					string text = this.ᜀ[num2];
					short value = (short)text.Length;
					this.ᜆ.Write(value);
					this.ᜆ.Write(Encoding.Unicode.GetBytes(text));
					num2++;
					num = 1;
					continue;
				}
				}
				goto IL_3C;
				IL_4D:
				num = 0;
				continue;
				IL_3C:
				if (this.ᜀ.Count > 0)
				{
					goto IL_4D;
				}
				break;
				IL_58:
				if (true)
				{
				}
				num = 5;
			}
			return;
		}
		}
	}

	// Token: 0x06002ADB RID: 10971 RVA: 0x002A3B90 File Offset: 0x002A2B90
	protected override void ᜅ()
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
		base.ᜅ();
		this.ᜀ = new List<string>();
	}

	// Token: 0x06002ADC RID: 10972 RVA: 0x002A3BDC File Offset: 0x002A2BDC
	protected override void ᜀ()
	{
		for (;;)
		{
			int num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_4C:
				if (num <= 0)
				{
					return;
				}
				num2 = 2;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num = this.ᜀ.ᝀ();
				num2 = 1;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					goto IL_4C;
				case 2:
				{
					this.ᜅ.BaseStream.Position = (long)this.ᜀ.\u1714();
					int a_ = num / 4;
					base.ᜅ(a_);
					num2 = 0;
					continue;
				}
				}
				break;
			}
		}
	}

	// Token: 0x06002ADD RID: 10973 RVA: 0x002A3C84 File Offset: 0x002A2C84
	protected override void ᜁ()
	{
		for (;;)
		{
			int num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_4C:
				if (num <= 0)
				{
					return;
				}
				num2 = 2;
				break;
			default:
				if (false)
				{
				}
				num = this.ᜀ.ឳ();
				if (true)
				{
				}
				num2 = 0;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_4C;
				case 1:
					return;
				case 2:
					this.ᜅ.BaseStream.Position = (long)this.ᜀ.ᜫ();
					base.ᜁ(num, 30);
					base.ᜁ();
					num2 = 1;
					continue;
				}
				break;
			}
		}
	}

	// Token: 0x06002ADE RID: 10974 RVA: 0x002A3D30 File Offset: 0x002A2D30
	protected override void ᜀ(BinaryReader A_0, int A_1, int A_2)
	{
		if (true)
		{
		}
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6C;
			case 1:
				return;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_6C:
				base.ᜀ(A_0, A_1, A_2);
				this.ᜃ.Add(new sprᝦ(A_0));
				num = 1;
				break;
			default:
				if (false)
				{
				}
				if (A_0.BaseStream.Position >= A_0.BaseStream.Length)
				{
					return;
				}
				num = 0;
				break;
			}
		}
	}

	// Token: 0x06002ADF RID: 10975 RVA: 0x002A3DD0 File Offset: 0x002A2DD0
	protected override void ᜃ()
	{
		for (;;)
		{
			IL_18:
			this.ᜀ.ᜌ((int)this.ᜆ.BaseStream.Position);
			this.ᜇ(this.ᜋ);
			int num = 0;
			int count = this.ᜃ.Count;
			for (;;)
			{
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_5A;
					case 1:
						goto IL_5A;
					case 2:
						goto IL_78;
					case 3:
					{
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						sprᝦ sprᝦ = this.ᜃ[num];
						sprᝦ.ᜀ(this.ᜆ);
						num++;
						num2 = 0;
						continue;
					}
					}
					goto IL_18;
					IL_5A:
					num2 = 3;
				}
				IL_78:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_C2;
				}
			}
		}
		IL_C2:
		if (false)
		{
		}
		this.ᜀ.\u171F((int)(this.ᜆ.BaseStream.Position - (long)this.ᜀ.ᜫ()));
	}

	// Token: 0x06002AE0 RID: 10976 RVA: 0x002A3ED0 File Offset: 0x002A2ED0
	protected override void ᜂ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_62;
			case 1:
				return;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_62:
				this.ᜀ.ᜄ((int)this.ᜆ.BaseStream.Position);
				base.ᜆ();
				this.ᜀ.ᝤ((int)(this.ᜆ.BaseStream.Position - (long)this.ᜀ.\u1714()));
				num = 1;
				break;
			default:
				if (false)
				{
				}
				if (this.ᜁ.Count <= 0)
				{
					return;
				}
				num = 0;
				break;
			}
		}
	}

	// Token: 0x04002513 RID: 9491
	private new List<string> ᜀ;

	// Token: 0x04002514 RID: 9492
	private new sprḊ.ᜁ ᜁ;

	// Token: 0x04002515 RID: 9493
	private new sprᝦ ᜂ;

	// Token: 0x04002516 RID: 9494
	private new int ᜃ = -1;

	// Token: 0x02000311 RID: 785
	internal new class ᜀ
	{
		// Token: 0x06002AE1 RID: 10977 RVA: 0x002A3F98 File Offset: 0x002A2F98
		internal int ᜁ()
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

		// Token: 0x06002AE2 RID: 10978 RVA: 0x002A3FDC File Offset: 0x002A2FDC
		internal void ᜀ(int A_0)
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

		// Token: 0x06002AE3 RID: 10979 RVA: 0x002A4020 File Offset: 0x002A3020
		internal int ᜀ()
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

		// Token: 0x06002AE4 RID: 10980 RVA: 0x002A4064 File Offset: 0x002A3064
		internal void ᜁ(int A_0)
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

		// Token: 0x06002AE5 RID: 10981 RVA: 0x002A40A8 File Offset: 0x002A30A8
		internal ᜀ(int A_0, int A_1)
		{
			this.ᜁ = A_1;
			this.ᜀ = A_0;
		}

		// Token: 0x04002517 RID: 9495
		private int ᜀ = -1;

		// Token: 0x04002518 RID: 9496
		private int ᜁ = -1;
	}

	// Token: 0x02000312 RID: 786
	[DefaultMember("Item")]
	internal new class ᜁ
	{
		// Token: 0x06002AE6 RID: 10982 RVA: 0x002A40D8 File Offset: 0x002A30D8
		internal sprḊ.ᜀ ᜀ(int A_0)
		{
			if (!this.ᜁ.Contains(A_0))
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
					return null;
				}
			}
			if (true)
			{
			}
			int a_ = this.ᜁ.IndexOf(A_0);
			return new sprḊ.ᜀ(this.ᜀ.ᜂ(a_), this.ᜀ.ᜀ(a_));
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x002A4154 File Offset: 0x002A3154
		internal ᜁ(BinaryReader A_0, sprᾱ A_1)
		{
			this.ᜀ(A_0, A_1);
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x002A417C File Offset: 0x002A317C
		internal ᜁ()
		{
			this.ᜀ = new sprᲈ();
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x002A41A8 File Offset: 0x002A31A8
		internal void ᜀ(BinaryReader A_0, sprᾱ A_1)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_6E:
				this.ᜀ = new sprᲈ(A_0.BaseStream, this.ᜂ, A_1.ឤ(), A_1.\u17B6(), A_1.\u17B7(), A_1.\u17CE());
				num = 1;
				break;
			default:
				if (false)
				{
				}
				goto IL_42;
			}
			for (;;)
			{
				IL_28:
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (this.ᜂ > 0)
					{
						num = 2;
						continue;
					}
					return;
				case 1:
					return;
				case 2:
					goto IL_6E;
				}
				goto IL_42;
			}
			return;
			IL_42:
			this.ᜀ(A_0, A_1.ᜉ(), A_1.ᜇ());
			num = 0;
			goto IL_28;
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x002A4260 File Offset: 0x002A3260
		internal void ᜀ(int A_0, sprḊ.ᜀ A_1)
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
			this.ᜁ.Add(A_0);
			this.ᜀ.ᜁ(A_1.ᜁ());
			this.ᜀ.ᜃ(this.ᜀ.ᜁ() - 1, A_1.ᜀ());
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x002A42D8 File Offset: 0x002A32D8
		internal void ᜀ(BinaryWriter A_0, sprᾱ A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 2;
				int[] array;
				int[] array2;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						goto IL_C4;
					case 1:
						goto IL_C4;
					case 3:
						goto IL_F5;
					case 4:
						goto IL_4E;
					case 5:
						if (true)
						{
						}
						if (num2 >= this.ᜀ.ᜁ())
						{
							num = 3;
							continue;
						}
						array[num2] = this.ᜀ.ᜂ(num2);
						array2[num2] = this.ᜁ[num2];
						num2++;
						num = 0;
						continue;
					}
					if (this.ᜀ.ᜁ() == 0)
					{
						num = 4;
						continue;
					}
					int num3 = this.ᜀ.ᜁ();
					array = new int[num3];
					array2 = new int[num3];
					num2 = 0;
					num = 1;
					continue;
					IL_C4:
					num = 5;
				}
				IL_4E:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					IL_F5:
					Array.Sort(array, array2, new sprḊ.ᜁ.ᜀ());
					A_1.ᜢ((int)A_0.BaseStream.Position);
					this.ᜀ(A_0, array2);
					A_1.ក((int)(A_0.BaseStream.Position - (long)A_1.ᜉ()));
					int num4 = A_1.\u1774() + 2;
					A_1.\u177B((int)A_0.BaseStream.Position);
					this.ᜀ(A_0, array, array2, num4);
					A_1.ᝮ((int)(A_0.BaseStream.Position - (long)A_1.ឤ()));
					A_1.ដ((int)A_0.BaseStream.Position);
					this.ᜀ(A_0, num4);
					A_1.\u175F((int)(A_0.BaseStream.Position - (long)A_1.\u17B7()));
					return;
				}
				default:
					if (false)
					{
					}
					return;
				}
				break;
			}
			}
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x002A449C File Offset: 0x002A349C
		private void ᜀ(BinaryReader A_0, int A_1, int A_2)
		{
			int num = 3;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					A_0.BaseStream.Position = (long)(A_1 + 2);
					this.ᜂ = (int)A_0.ReadInt16();
					A_0.ReadInt16();
					num2 = 0;
					num = 4;
					continue;
				case 1:
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_41;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						goto IL_39;
					}
					break;
				case 4:
					goto IL_39;
				case 5:
					goto IL_41;
				}
				if (A_2 > 0)
				{
					num = 0;
					continue;
				}
				break;
				IL_39:
				num = 5;
				continue;
				IL_41:
				if (num2 >= this.ᜂ)
				{
					num = 1;
				}
				else
				{
					A_0.ReadInt32();
					this.ᜁ.Add(A_0.ReadInt32());
					A_0.ReadInt32();
					num2++;
					num = 2;
				}
			}
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x002A4590 File Offset: 0x002A3590
		private void ᜀ(BinaryWriter A_0, int[] A_1)
		{
			for (;;)
			{
				A_0.Write(-1);
				A_0.Write((short)this.ᜀ.ᜁ());
				A_0.Write(10);
				int num = 0;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num >= A_1.Length)
						{
							num2 = 3;
							continue;
						}
						A_0.Write(16777216);
						A_0.Write(A_1[num]);
						A_0.Write(-1);
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B5;
						}
						if (false)
						{
						}
						num2 = 1;
						continue;
					case 1:
						goto IL_B5;
					case 2:
						if (true)
						{
						}
						goto IL_4D;
					case 3:
						return;
					}
					break;
					IL_4D:
					num2 = 0;
					continue;
					IL_B5:
					goto IL_4D;
				}
			}
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x002A4654 File Offset: 0x002A3654
		private void ᜀ(BinaryWriter A_0, int[] A_1, int[] A_2, int A_3)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int[] array = new int[A_1.Length];
					int num = 0;
					int num2 = 0;
					int num3 = 0;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_100;
						case 1:
							goto IL_DD;
						case 2:
							goto IL_DD;
						case 3:
						{
							A_0.Write(A_3);
							int num4 = 0;
							num3 = 1;
							continue;
						}
						case 4:
							goto IL_100;
						case 5:
							return;
						case 6:
						{
							int num4;
							if (num4 >= A_1.Length)
							{
								num3 = 5;
								continue;
							}
							A_0.Write(array[num4]);
							num4++;
							goto IL_5F;
						}
						case 7:
							if (num2 >= A_1.Length)
							{
								num3 = 3;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_5F;
							default:
								if (false)
								{
								}
								array[num] = this.ᜁ.IndexOf(A_2[num2]);
								A_0.Write(A_1[num2]);
								num++;
								num2++;
								num3 = 4;
								continue;
							}
							break;
						}
						break;
						IL_5F:
						if (true)
						{
						}
						num3 = 2;
						continue;
						IL_DD:
						num3 = 6;
						continue;
						IL_100:
						num3 = 7;
					}
				}
				return;
			}
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x002A4788 File Offset: 0x002A3788
		private void ᜀ(BinaryWriter A_0, int A_1)
		{
			for (;;)
			{
				int num = 0;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_24;
					case 1:
						goto IL_42;
					case 2:
						if (num >= this.ᜀ.ᜁ())
						{
							num2 = 1;
							continue;
						}
						if (true)
						{
						}
						A_0.Write(this.ᜀ.ᜀ(num));
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_93;
						}
						if (false)
						{
						}
						num2 = 3;
						continue;
					case 3:
						goto IL_93;
					}
					break;
					IL_24:
					num2 = 2;
					continue;
					IL_93:
					goto IL_24;
				}
			}
			IL_42:
			A_0.Write(A_1);
		}

		// Token: 0x04002519 RID: 9497
		private sprᲈ ᜀ;

		// Token: 0x0400251A RID: 9498
		private List<int> ᜁ = new List<int>();

		// Token: 0x0400251B RID: 9499
		private int ᜂ;

		// Token: 0x02000313 RID: 787
		internal class ᜀ : IComparer
		{
			// Token: 0x06002AF0 RID: 10992 RVA: 0x002A4834 File Offset: 0x002A3834
			public int ᜀ(object A_0, object A_1)
			{
				for (;;)
				{
					int num;
					int num2;
					int num3;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_6A:
						if (num == num2)
						{
							return 0;
						}
						num3 = 3;
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = (int)A_0;
						num2 = (int)A_1;
						num3 = 1;
						break;
					}
					for (;;)
					{
						switch (num3)
						{
						case 0:
							return 1;
						case 1:
							if (num > num2)
							{
								num3 = 0;
								continue;
							}
							num3 = 2;
							continue;
						case 2:
							goto IL_6A;
						case 3:
							return -1;
						}
						break;
					}
				}
				return 1;
			}
		}
	}
}
