using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020002A4 RID: 676
[spr\u2593(TBIFFRecord.VerticalPageBreaks)]
[CLSCompliant(false)]
internal class spr\u2583 : BiffRecordRaw
{
	// Token: 0x060028D4 RID: 10452 RVA: 0x001720EC File Offset: 0x001710EC
	public spr\u2583.ᜀ[] ᜀ()
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
		return this.ᜃ;
	}

	// Token: 0x060028D5 RID: 10453 RVA: 0x00172130 File Offset: 0x00171130
	public void ᜀ(spr\u2583.ᜀ[] A_0)
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
		this.ᜃ = A_0;
		this.ᜂ = ((A_0 != null) ? ((ushort)A_0.Length) : 0);
	}

	// Token: 0x060028D6 RID: 10454 RVA: 0x00172188 File Offset: 0x00171188
	public virtual int ᜁ()
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
		return 2;
	}

	// Token: 0x060028D7 RID: 10455 RVA: 0x001721C4 File Offset: 0x001711C4
	public spr\u2583()
	{
	}

	// Token: 0x060028D8 RID: 10456 RVA: 0x001721D8 File Offset: 0x001711D8
	public spr\u2583(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060028D9 RID: 10457 RVA: 0x001721F0 File Offset: 0x001711F0
	public spr\u2583(int A_0) : base(A_0)
	{
	}

	// Token: 0x060028DA RID: 10458 RVA: 0x00172204 File Offset: 0x00171204
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				this.ᜂ = A_0.ReadUInt16(A_1);
				this.ᜃ = new spr\u2583.ᜀ[(int)this.ᜂ];
				A_1 += 2;
				int num = 0;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						if (num >= (int)this.ᜂ)
						{
							num2 = 1;
							continue;
						}
						ushort a_ = A_0.ReadUInt16(A_1);
						ushort a_2 = A_0.ReadUInt16(A_1 + 2);
						ushort a_3 = A_0.ReadUInt16(A_1 + 4);
						this.ᜃ[num] = new spr\u2583.ᜀ(a_, a_2, a_3);
						num++;
						A_1 += 6;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num2 = 3;
							continue;
						}
						break;
					}
					case 1:
						return;
					case 2:
						goto IL_57;
					case 3:
						goto IL_57;
					}
					break;
					IL_57:
					num2 = 0;
				}
			}
			return;
		}
	}

	// Token: 0x060028DB RID: 10459 RVA: 0x001722FC File Offset: 0x001712FC
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		for (;;)
		{
			A_0.WriteUInt16(A_1, this.ᜂ);
			this.m_iLength = 2;
			int num = 0;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					goto IL_42;
				case 2:
					if (num < (int)this.ᜂ)
					{
						A_0.WriteUInt16(A_1 + this.m_iLength, this.ᜃ[num].ᜁ());
						A_0.WriteUInt16(A_1 + this.m_iLength + 2, (ushort)this.ᜃ[num].ᜃ());
						A_0.WriteUInt16(A_1 + this.m_iLength + 4, (ushort)this.ᜃ[num].ᜀ());
						num++;
						this.m_iLength += 6;
						num2 = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_42;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num2 = 0;
						continue;
					}
					break;
				case 3:
					goto IL_42;
				}
				break;
				IL_42:
				num2 = 2;
			}
		}
	}

	// Token: 0x060028DC RID: 10460 RVA: 0x00172404 File Offset: 0x00171404
	public virtual int ᜀ(ExcelVersion A_0)
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
		return (int)(2 + 6 * this.ᜂ);
	}

	// Token: 0x04001383 RID: 4995
	internal new const int ᜀ = 2;

	// Token: 0x04001384 RID: 4996
	internal const int ᜁ = 6;

	// Token: 0x04001385 RID: 4997
	[spr\u2429(0, 2)]
	private ushort ᜂ;

	// Token: 0x04001386 RID: 4998
	private new spr\u2583.ᜀ[] ᜃ;

	// Token: 0x020002A5 RID: 677
	internal new class ᜀ : ICloneable
	{
		// Token: 0x060028DD RID: 10461 RVA: 0x0017244C File Offset: 0x0017144C
		public ᜀ()
		{
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x00172460 File Offset: 0x00171460
		public ᜀ(ushort A_0, ushort A_1, ushort A_2)
		{
			this.ᜀ = A_0;
			this.ᜁ = (uint)A_1;
			this.ᜂ = (uint)A_2;
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x00172488 File Offset: 0x00171488
		public ushort ᜁ()
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

		// Token: 0x060028E0 RID: 10464 RVA: 0x001724CC File Offset: 0x001714CC
		public void ᜀ(ushort A_0)
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

		// Token: 0x060028E1 RID: 10465 RVA: 0x00172510 File Offset: 0x00171510
		public uint ᜃ()
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

		// Token: 0x060028E2 RID: 10466 RVA: 0x00172554 File Offset: 0x00171554
		public void ᜀ(uint A_0)
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

		// Token: 0x060028E3 RID: 10467 RVA: 0x00172598 File Offset: 0x00171598
		public uint ᜀ()
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

		// Token: 0x060028E4 RID: 10468 RVA: 0x001725DC File Offset: 0x001715DC
		public void ᜁ(uint A_0)
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
			this.ᜂ = A_0;
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x00172620 File Offset: 0x00171620
		public object ᜂ()
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
			return base.MemberwiseClone();
		}

		// Token: 0x04001387 RID: 4999
		private ushort ᜀ;

		// Token: 0x04001388 RID: 5000
		private uint ᜁ;

		// Token: 0x04001389 RID: 5001
		private uint ᜂ;
	}
}
