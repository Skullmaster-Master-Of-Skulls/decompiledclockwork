using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200042D RID: 1069
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.CondFMT)]
internal class spr\u21C4 : BiffRecordRaw, ICloneable
{
	// Token: 0x060040A8 RID: 16552 RVA: 0x0024430C File Offset: 0x0024330C
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
		return this.ᜃ;
	}

	// Token: 0x060040A9 RID: 16553 RVA: 0x00244350 File Offset: 0x00243350
	public void ᜁ(ushort A_0)
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

	// Token: 0x060040AA RID: 16554 RVA: 0x00244394 File Offset: 0x00243394
	public new bool ᜃ()
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
		return this.ᜄ == 1;
	}

	// Token: 0x060040AB RID: 16555 RVA: 0x002443D8 File Offset: 0x002433D8
	public void ᜀ(bool A_0)
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
		this.ᜄ = (A_0 ? 1 : 0);
	}

	// Token: 0x060040AC RID: 16556 RVA: 0x00244428 File Offset: 0x00243428
	public TAddr ᜀ()
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
		return this.ᜅ;
	}

	// Token: 0x060040AD RID: 16557 RVA: 0x0024446C File Offset: 0x0024346C
	public void ᜀ(TAddr A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x060040AE RID: 16558 RVA: 0x002444B0 File Offset: 0x002434B0
	public ushort ᜆ()
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
		return this.ᜆ;
	}

	// Token: 0x060040AF RID: 16559 RVA: 0x002444F4 File Offset: 0x002434F4
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
		this.ᜆ = A_0;
	}

	// Token: 0x060040B0 RID: 16560 RVA: 0x00244538 File Offset: 0x00243538
	public List<Rectangle> ᜄ()
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

	// Token: 0x060040B1 RID: 16561 RVA: 0x0024457C File Offset: 0x0024357C
	internal void ᜀ(List<Rectangle> A_0)
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
		this.ᜇ = A_0;
	}

	// Token: 0x060040B2 RID: 16562 RVA: 0x002445C0 File Offset: 0x002435C0
	public virtual int ᜂ()
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
		return 14;
	}

	// Token: 0x060040B3 RID: 16563 RVA: 0x00244600 File Offset: 0x00243600
	public spr\u21C4()
	{
	}

	// Token: 0x060040B4 RID: 16564 RVA: 0x00244634 File Offset: 0x00243634
	public spr\u21C4(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060040B5 RID: 16565 RVA: 0x00244668 File Offset: 0x00243668
	public spr\u21C4(int A_0) : base(A_0)
	{
	}

	// Token: 0x060040B6 RID: 16566 RVA: 0x0024469C File Offset: 0x0024369C
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
		this.ᜃ = A_0.ReadUInt16(A_1);
		A_1 += 2;
		this.ᜄ = A_0.ReadUInt16(A_1);
		A_1 += 2;
		this.ᜅ = A_0.ᜆ(A_1);
		A_1 += 8;
		this.ᜀ(A_0, ref A_1);
	}

	// Token: 0x060040B7 RID: 16567 RVA: 0x00244718 File Offset: 0x00243718
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		for (;;)
		{
			IL_18:
			this.ᜆ = (ushort)this.ᜇ.Count;
			this.m_iLength = this.GetStoreSize(A_2);
			A_0.WriteUInt16(A_1, this.ᜃ);
			A_0.WriteUInt16(A_1 + 2, this.ᜄ);
			A_1 += 4;
			A_0.WriteAddr(A_1, this.ᜅ);
			A_1 += 8;
			A_0.WriteUInt16(A_1, this.ᜆ);
			A_1 += 2;
			int num = 0;
			for (;;)
			{
				IL_7E:
				if (true)
				{
				}
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_93;
					case 1:
					{
						if (num >= (int)this.ᜆ)
						{
							num2 = 3;
							continue;
						}
						Rectangle addr = this.ᜇ[num];
						A_0.WriteAddr(A_1, addr);
						num++;
						A_1 += 8;
						num2 = 0;
						continue;
					}
					case 2:
						goto IL_93;
					case 3:
						return;
					}
					goto IL_18;
					IL_93:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7E;
					default:
						if (false)
						{
						}
						num2 = 1;
						break;
					}
				}
			}
		}
	}

	// Token: 0x060040B8 RID: 16568 RVA: 0x0024482C File Offset: 0x0024382C
	private void ᜀ(DataProvider A_0, ref int A_1)
	{
		for (;;)
		{
			IL_18:
			this.ᜆ = A_0.ReadUInt16(A_1);
			A_1 += 2;
			int num = 0;
			for (;;)
			{
				IL_2E:
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						if (num >= (int)this.ᜆ)
						{
							num2 = 3;
							continue;
						}
						Rectangle item = A_0.ReadAddrAsRectangle(A_1);
						this.ᜇ.Add(item);
						num++;
						A_1 += 8;
						num2 = 2;
						continue;
					}
					case 1:
						if (true)
						{
						}
						goto IL_40;
					case 2:
						goto IL_40;
					case 3:
						return;
					}
					goto IL_18;
					IL_40:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2E;
					default:
						if (false)
						{
						}
						num2 = 0;
						break;
					}
				}
			}
		}
	}

	// Token: 0x060040B9 RID: 16569 RVA: 0x002448E8 File Offset: 0x002438E8
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
		return 14 + this.ᜇ.Count * 8;
	}

	// Token: 0x060040BA RID: 16570 RVA: 0x00244934 File Offset: 0x00243934
	public void ᜀ(Rectangle A_0)
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
		this.ᜇ.Add(A_0);
		this.ᜆ += 1;
	}

	// Token: 0x060040BB RID: 16571 RVA: 0x0024498C File Offset: 0x0024398C
	public virtual object ᜅ()
	{
		spr\u21C4 spr_u21C;
		for (;;)
		{
			IL_18:
			spr_u21C = (spr\u21C4)base.Clone();
			spr_u21C.ᜇ = new List<Rectangle>(this.ᜇ.Count);
			int num = 0;
			int count = this.ᜇ.Count;
			for (;;)
			{
				IL_48:
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_52;
					case 1:
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						if (true)
						{
						}
						spr_u21C.ᜀ(this.ᜇ[num]);
						num++;
						num2 = 3;
						continue;
					case 2:
						return spr_u21C;
					case 3:
						goto IL_52;
					}
					goto IL_18;
					IL_52:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_48;
					default:
						if (false)
						{
						}
						num2 = 1;
						break;
					}
				}
			}
		}
		return spr_u21C;
	}

	// Token: 0x04001CE0 RID: 7392
	private new const ushort ᜀ = 14;

	// Token: 0x04001CE1 RID: 7393
	private const int ᜁ = 14;

	// Token: 0x04001CE2 RID: 7394
	private const int ᜂ = 8;

	// Token: 0x04001CE3 RID: 7395
	[spr\u2429(0, 2)]
	private new ushort ᜃ;

	// Token: 0x04001CE4 RID: 7396
	[spr\u2429(2, 2)]
	private ushort ᜄ = 1;

	// Token: 0x04001CE5 RID: 7397
	private TAddr ᜅ = default(TAddr);

	// Token: 0x04001CE6 RID: 7398
	private ushort ᜆ;

	// Token: 0x04001CE7 RID: 7399
	private List<Rectangle> ᜇ = new List<Rectangle>();
}
