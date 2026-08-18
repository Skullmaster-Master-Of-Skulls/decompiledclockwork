using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;

// Token: 0x02000362 RID: 866
[sprᵴ(MsoRecords.msofbtDgg)]
[CLSCompliant(false)]
internal class spr\u2412 : spr\u1D3B
{
	// Token: 0x0600350B RID: 13579 RVA: 0x001E5AFC File Offset: 0x001E4AFC
	public spr\u2412(spr\u1D3B A_0) : base(A_0)
	{
	}

	// Token: 0x0600350C RID: 13580 RVA: 0x001E5B1C File Offset: 0x001E4B1C
	public spr\u2412(spr\u1D3B A_0, byte[] A_1, int A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x0600350D RID: 13581 RVA: 0x001E5B40 File Offset: 0x001E4B40
	public uint ᜁ()
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

	// Token: 0x0600350E RID: 13582 RVA: 0x001E5B84 File Offset: 0x001E4B84
	public new void ᜀ(uint A_0)
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

	// Token: 0x0600350F RID: 13583 RVA: 0x001E5BC8 File Offset: 0x001E4BC8
	public new uint ᜀ()
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

	// Token: 0x06003510 RID: 13584 RVA: 0x001E5C0C File Offset: 0x001E4C0C
	public uint ᜆ()
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

	// Token: 0x06003511 RID: 13585 RVA: 0x001E5C50 File Offset: 0x001E4C50
	public void ᜂ(uint A_0)
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
	}

	// Token: 0x06003512 RID: 13586 RVA: 0x001E5C94 File Offset: 0x001E4C94
	public new uint ᜃ()
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
		return this.ᜄ;
	}

	// Token: 0x06003513 RID: 13587 RVA: 0x001E5CD8 File Offset: 0x001E4CD8
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
		this.ᜄ = A_0;
	}

	// Token: 0x06003514 RID: 13588 RVA: 0x001E5D1C File Offset: 0x001E4D1C
	public new spr\u2412.ᜀ[] ᜄ()
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
		return this.ᜅ.ToArray();
	}

	// Token: 0x06003515 RID: 13589 RVA: 0x001E5D64 File Offset: 0x001E4D64
	public override void ᜀ(Stream A_0)
	{
		for (;;)
		{
			for (;;)
			{
				this.ᜁ = spr\u1D3B.ᜃ(A_0);
				this.ᜂ = spr\u1D3B.ᜃ(A_0);
				this.ᜃ = spr\u1D3B.ᜃ(A_0);
				this.ᜄ = spr\u1D3B.ᜃ(A_0);
				int num = 16;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return;
					case 1:
						if (true)
						{
						}
						if (this.ᜂ > 0U)
						{
							num2 = 3;
							continue;
						}
						return;
					case 2:
						goto IL_83;
					case 3:
					{
						int num3 = 0;
						num2 = 5;
						continue;
					}
					case 4:
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
							int num3;
							if ((long)num3 >= (long)((ulong)(this.ᜂ - 1U)))
							{
								num2 = 0;
								continue;
							}
							spr\u2412.ᜀ item = new spr\u2412.ᜀ(A_0);
							this.ᜅ.Add(item);
							num3++;
							num += spr\u2412.ᜀ.ᜀ();
							num2 = 2;
							continue;
						}
						}
						break;
					case 5:
						goto IL_83;
					}
					break;
					IL_83:
					num2 = 4;
				}
			}
		}
	}

	// Token: 0x06003516 RID: 13590 RVA: 0x001E5E70 File Offset: 0x001E4E70
	public override void ᜀ(Stream A_0, int A_1, List<int> A_2, List<List<BiffRecordRaw>> A_3)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_8E:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			goto IL_3E;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				goto IL_E1;
			case 1:
			{
				if (num2 >= count)
				{
					num = 2;
					continue;
				}
				if (true)
				{
				}
				spr\u2412.ᜀ ᜀ = this.ᜅ[num2];
				ᜀ.ᜀ(A_0);
				num2++;
				this.m_iLength += spr\u2412.ᜀ.ᜀ();
				num = 0;
				continue;
			}
			case 2:
				return;
			case 3:
				goto IL_8C;
			}
			goto IL_3E;
		}
		IL_8C:
		IL_E1:
		goto IL_8E;
		IL_3E:
		spr\u1D3B.ᜀ(A_0, this.ᜁ);
		spr\u1D3B.ᜀ(A_0, this.ᜂ);
		spr\u1D3B.ᜀ(A_0, this.ᜃ);
		spr\u1D3B.ᜀ(A_0, this.ᜄ);
		this.m_iLength = 16;
		num2 = 0;
		count = this.ᜅ.Count;
		num = 3;
		goto IL_28;
	}

	// Token: 0x06003517 RID: 13591 RVA: 0x001E5F60 File Offset: 0x001E4F60
	protected override object ᜅ()
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			spr\u2412 spr_u;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_6C:
				if (this.ᜅ == null)
				{
					return spr_u;
				}
				num = 3;
				break;
			default:
				if (false)
				{
				}
				goto IL_57;
			}
			for (;;)
			{
				IL_34:
				switch (num)
				{
				case 0:
					goto IL_10C;
				case 1:
				{
					List<spr\u2412.ᜀ> list;
					spr_u.ᜅ = list;
					num = 0;
					continue;
				}
				case 2:
					goto IL_8C;
				case 3:
				{
					int count = this.ᜅ.Count;
					List<spr\u2412.ᜀ> list = new List<spr\u2412.ᜀ>(count);
					int num2 = 0;
					num = 2;
					continue;
				}
				case 4:
					goto IL_8C;
				case 5:
				{
					int count;
					int num2;
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					spr\u2412.ᜀ item = (spr\u2412.ᜀ)this.ᜅ[num2].ᜃ();
					List<spr\u2412.ᜀ> list;
					list.Add(item);
					num2++;
					num = 4;
					continue;
				}
				case 6:
					goto IL_6C;
				}
				goto IL_57;
				IL_8C:
				num = 5;
			}
			IL_10C:
			return spr_u;
			IL_57:
			spr_u = (spr\u2412)base.ᜅ();
			num = 6;
			goto IL_34;
		}
		}
	}

	// Token: 0x06003518 RID: 13592 RVA: 0x001E607C File Offset: 0x001E507C
	public new void ᜀ(uint A_0, uint A_1)
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
		spr\u2412.ᜀ item = new spr\u2412.ᜀ(A_0, A_1);
		this.ᜅ.Add(item);
		this.ᜂ = (uint)(this.ᜅ.Count + 1);
		this.ᜄ = (uint)this.ᜅ.Count;
	}

	// Token: 0x04001726 RID: 5926
	private new const int ᜀ = 16;

	// Token: 0x04001727 RID: 5927
	[spr\u2429(0, 4)]
	private new uint ᜁ;

	// Token: 0x04001728 RID: 5928
	[spr\u2429(4, 4)]
	private new uint ᜂ;

	// Token: 0x04001729 RID: 5929
	[spr\u2429(8, 4)]
	private new uint ᜃ;

	// Token: 0x0400172A RID: 5930
	[spr\u2429(12, 4)]
	private new uint ᜄ;

	// Token: 0x0400172B RID: 5931
	private new List<spr\u2412.ᜀ> ᜅ = new List<spr\u2412.ᜀ>();

	// Token: 0x02000363 RID: 867
	internal new class ᜀ : ICloneable
	{
		// Token: 0x06003519 RID: 13593 RVA: 0x001E60F0 File Offset: 0x001E50F0
		public ᜀ(uint A_0, uint A_1)
		{
			this.ᜁ = A_0;
			this.ᜂ = A_1;
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x001E6114 File Offset: 0x001E5114
		public ᜀ(byte[] A_0, int A_1)
		{
			this.ᜀ(BitConverter.ToUInt32(A_0, A_1));
			A_1 += 4;
			this.ᜁ(BitConverter.ToUInt32(A_0, A_1));
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x001E6148 File Offset: 0x001E5148
		public ᜀ(Stream A_0)
		{
			this.ᜀ(spr\u1D3B.ᜃ(A_0));
			this.ᜁ(spr\u1D3B.ᜃ(A_0));
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x001E6174 File Offset: 0x001E5174
		public uint ᜄ()
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

		// Token: 0x0600351D RID: 13597 RVA: 0x001E61B8 File Offset: 0x001E51B8
		public void ᜀ(uint A_0)
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

		// Token: 0x0600351E RID: 13598 RVA: 0x001E61FC File Offset: 0x001E51FC
		public uint ᜂ()
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

		// Token: 0x0600351F RID: 13599 RVA: 0x001E6240 File Offset: 0x001E5240
		public void ᜁ(uint A_0)
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

		// Token: 0x06003520 RID: 13600 RVA: 0x001E6284 File Offset: 0x001E5284
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
			return 8;
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x001E62C0 File Offset: 0x001E52C0
		public byte[] ᜁ()
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
			byte[] array = new byte[spr\u2412.ᜀ.ᜀ()];
			BitConverter.GetBytes(this.ᜁ).CopyTo(array, 0);
			BitConverter.GetBytes(this.ᜂ).CopyTo(array, 4);
			return array;
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x001E632C File Offset: 0x001E532C
		public void ᜀ(Stream A_0)
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
			spr\u1D3B.ᜀ(A_0, this.ᜁ);
			spr\u1D3B.ᜀ(A_0, this.ᜂ);
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x001E6380 File Offset: 0x001E5380
		public object ᜃ()
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
			return (spr\u2412.ᜀ)base.MemberwiseClone();
		}

		// Token: 0x0400172C RID: 5932
		private const int ᜀ = 8;

		// Token: 0x0400172D RID: 5933
		private uint ᜁ;

		// Token: 0x0400172E RID: 5934
		private uint ᜂ;
	}
}
