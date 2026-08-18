using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000541 RID: 1345
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ChartAlruns)]
internal class sprᜰ : BiffRecordRaw
{
	// Token: 0x060051CF RID: 20943 RVA: 0x00330958 File Offset: 0x0032F958
	public ushort ᜂ()
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

	// Token: 0x060051D0 RID: 20944 RVA: 0x0033099C File Offset: 0x0032F99C
	public void ᜀ(ushort A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				this.ᜀ = A_0;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2D;
				}
				if (false)
				{
				}
				num = 2;
				continue;
			case 2:
				return;
			}
			goto IL_1C;
			IL_2D:
			num = 1;
			continue;
			IL_1C:
			if (true)
			{
			}
			if (A_0 != this.ᜀ)
			{
				goto IL_2D;
			}
			break;
		}
	}

	// Token: 0x060051D1 RID: 20945 RVA: 0x00330A18 File Offset: 0x0032FA18
	public sprᜰ.ᜀ[] ᜀ()
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

	// Token: 0x060051D2 RID: 20946 RVA: 0x00330A5C File Offset: 0x0032FA5C
	public void ᜀ(sprᜰ.ᜀ[] A_0)
	{
		int a_ = 17;
		if (A_0 == null)
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
				break;
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅆ⡈❊㡌⩎", a_));
		}
		this.ᜁ = A_0;
		this.ᜀ = (ushort)this.ᜁ.Length;
	}

	// Token: 0x060051D3 RID: 20947 RVA: 0x00330AD0 File Offset: 0x0032FAD0
	public sprᜰ()
	{
	}

	// Token: 0x060051D4 RID: 20948 RVA: 0x00330AF0 File Offset: 0x0032FAF0
	public sprᜰ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060051D5 RID: 20949 RVA: 0x00330B14 File Offset: 0x0032FB14
	public sprᜰ(int A_0) : base(A_0)
	{
	}

	// Token: 0x060051D6 RID: 20950 RVA: 0x00330B34 File Offset: 0x0032FB34
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		for (;;)
		{
			this.ᜀ = A_0.ReadUInt16(A_1);
			this.ᜁ = new sprᜰ.ᜀ[(int)this.ᜀ];
			int num = A_1 + 2;
			int num2 = 0;
			int num3 = 3;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_50;
				case 1:
					if (num2 >= (int)this.ᜀ)
					{
						num3 = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						this.ᜁ[num2] = new sprᜰ.ᜀ(A_0.ReadUInt16(num), A_0.ReadUInt16(num + 2));
						num2++;
						num += 4;
						num3 = 0;
						continue;
					}
					break;
				case 2:
					return;
				case 3:
					goto IL_50;
				}
				break;
				IL_50:
				num3 = 1;
			}
		}
	}

	// Token: 0x060051D7 RID: 20951 RVA: 0x00330C04 File Offset: 0x0032FC04
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		int num;
		for (;;)
		{
			A_0.WriteUInt16(A_1, this.ᜀ);
			num = A_1 + 2;
			int num2 = 0;
			int num3 = 0;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					if (true)
					{
					}
					goto IL_47;
				case 1:
					goto IL_47;
				case 2:
					goto IL_60;
				case 3:
					if (num2 >= (int)this.ᜀ)
					{
						num3 = 2;
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
						A_0.WriteUInt16(num, this.ᜁ[num2].ᜂ());
						A_0.WriteUInt16(num + 2, this.ᜁ[num2].ᜀ());
						num2++;
						num += 4;
						num3 = 1;
						continue;
					}
					break;
				}
				break;
				IL_47:
				num3 = 3;
			}
		}
		IL_60:
		this.m_iLength = num;
	}

	// Token: 0x060051D8 RID: 20952 RVA: 0x00330CD8 File Offset: 0x0032FCD8
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
		return 2 + this.ᜁ.Length * 4;
	}

	// Token: 0x060051D9 RID: 20953 RVA: 0x00330D20 File Offset: 0x0032FD20
	public virtual object ᜁ()
	{
		sprᜰ sprᜰ;
		for (;;)
		{
			sprᜰ = (sprᜰ)base.Clone();
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_44;
				case 1:
					goto IL_9C;
				case 2:
					goto IL_9C;
				case 3:
				{
					if (this.ᜁ == null)
					{
						num = 0;
						continue;
					}
					int num2 = this.ᜁ.Length;
					sprᜰ.ᜁ = new sprᜰ.ᜀ[num2];
					int num3 = 0;
					num = 1;
					continue;
				}
				case 4:
				{
					int num2;
					int num3;
					if (num3 < num2)
					{
						sprᜰ.ᜁ[num3] = (sprᜰ.ᜀ)spr\u1CD3.ᜀ(this.ᜁ[num3]);
						num3++;
						num = 2;
						continue;
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
						num = 5;
						continue;
					}
					break;
				}
				case 5:
					return sprᜰ;
				}
				break;
				IL_9C:
				num = 4;
			}
		}
		IL_44:
		if (true)
		{
		}
		return sprᜰ;
	}

	// Token: 0x04002473 RID: 9331
	[spr\u2429(0, 2)]
	private new ushort ᜀ;

	// Token: 0x04002474 RID: 9332
	private sprᜰ.ᜀ[] ᜁ = new sprᜰ.ᜀ[0];

	// Token: 0x02000542 RID: 1346
	internal new class ᜀ : ICloneable
	{
		// Token: 0x060051DA RID: 20954 RVA: 0x00330E0C File Offset: 0x0032FE0C
		public ushort ᜂ()
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

		// Token: 0x060051DB RID: 20955 RVA: 0x00330E50 File Offset: 0x0032FE50
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
			this.ᜁ = A_0;
		}

		// Token: 0x060051DC RID: 20956 RVA: 0x00330E94 File Offset: 0x0032FE94
		public ushort ᜀ()
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

		// Token: 0x060051DD RID: 20957 RVA: 0x00330ED8 File Offset: 0x0032FED8
		public void ᜁ(ushort A_0)
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
			this.ᜂ = A_0;
		}

		// Token: 0x060051DE RID: 20958 RVA: 0x00330F1C File Offset: 0x0032FF1C
		public ᜀ(ushort A_0, ushort A_1)
		{
			this.ᜁ = A_0;
			this.ᜂ = A_1;
		}

		// Token: 0x060051DF RID: 20959 RVA: 0x00330F40 File Offset: 0x0032FF40
		public object ᜁ()
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

		// Token: 0x04002475 RID: 9333
		internal const int ᜀ = 4;

		// Token: 0x04002476 RID: 9334
		private ushort ᜁ;

		// Token: 0x04002477 RID: 9335
		private ushort ᜂ;
	}
}
