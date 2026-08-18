using System;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000479 RID: 1145
[CLSCompliant(false)]
internal class spr\u197A : ICloneable
{
	// Token: 0x06004612 RID: 17938 RVA: 0x002AA1D0 File Offset: 0x002A91D0
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
		return this.ᜆ;
	}

	// Token: 0x06004613 RID: 17939 RVA: 0x002AA214 File Offset: 0x002A9214
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
		this.ᜆ = A_0;
	}

	// Token: 0x06004614 RID: 17940 RVA: 0x002AA258 File Offset: 0x002A9258
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
		return this.ᜇ;
	}

	// Token: 0x06004615 RID: 17941 RVA: 0x002AA29C File Offset: 0x002A929C
	public void ᜀ(ushort A_0)
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

	// Token: 0x06004616 RID: 17942 RVA: 0x002AA2E0 File Offset: 0x002A92E0
	public ushort ᜄ()
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
		return this.ᜈ;
	}

	// Token: 0x06004617 RID: 17943 RVA: 0x002AA324 File Offset: 0x002A9324
	public ushort ᜇ()
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
		return this.ᜉ;
	}

	// Token: 0x06004618 RID: 17944 RVA: 0x002AA368 File Offset: 0x002A9368
	public ushort[] ᜊ()
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

	// Token: 0x06004619 RID: 17945 RVA: 0x002AA3AC File Offset: 0x002A93AC
	public void ᜀ(ushort[] A_0)
	{
		int a_ = 17;
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
				this.ᜊ = A_0;
				this.ᜈ = (ushort)(this.ᜊ.Length - 1);
				return;
			}
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅆ⡈❊㡌⩎", a_));
	}

	// Token: 0x0600461A RID: 17946 RVA: 0x002AA424 File Offset: 0x002A9424
	public bool ᜂ()
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
		return BiffRecordRaw.GetBitFromVar(this.ᜉ, 0);
	}

	// Token: 0x0600461B RID: 17947 RVA: 0x002AA46C File Offset: 0x002A946C
	public void ᜂ(bool A_0)
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
		this.ᜉ = (ushort)BiffRecordRaw.SetBit((int)this.ᜉ, 0, A_0);
	}

	// Token: 0x0600461C RID: 17948 RVA: 0x002AA4BC File Offset: 0x002A94BC
	public bool ᜆ()
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
		return BiffRecordRaw.GetBitFromVar(this.ᜉ, 9);
	}

	// Token: 0x0600461D RID: 17949 RVA: 0x002AA504 File Offset: 0x002A9504
	public void ᜄ(bool A_0)
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
		this.ᜉ = (ushort)BiffRecordRaw.SetBit((int)this.ᜉ, 9, A_0);
	}

	// Token: 0x0600461E RID: 17950 RVA: 0x002AA554 File Offset: 0x002A9554
	public bool ᜉ()
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
		return BiffRecordRaw.GetBitFromVar(this.ᜉ, 10);
	}

	// Token: 0x0600461F RID: 17951 RVA: 0x002AA59C File Offset: 0x002A959C
	public void ᜁ(bool A_0)
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
		this.ᜉ = (ushort)BiffRecordRaw.SetBit((int)this.ᜉ, 10, A_0);
	}

	// Token: 0x06004620 RID: 17952 RVA: 0x002AA5EC File Offset: 0x002A95EC
	public bool ᜋ()
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
		return BiffRecordRaw.GetBitFromVar(this.ᜉ, 11);
	}

	// Token: 0x06004621 RID: 17953 RVA: 0x002AA634 File Offset: 0x002A9634
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
		this.ᜉ = (ushort)BiffRecordRaw.SetBit((int)this.ᜉ, 11, A_0);
	}

	// Token: 0x06004622 RID: 17954 RVA: 0x002AA684 File Offset: 0x002A9684
	public bool ᜈ()
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
		return BiffRecordRaw.GetBitFromVar(this.ᜉ, 12);
	}

	// Token: 0x06004623 RID: 17955 RVA: 0x002AA6CC File Offset: 0x002A96CC
	public void ᜃ(bool A_0)
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
		this.ᜉ = (ushort)BiffRecordRaw.SetBit((int)this.ᜉ, 12, A_0);
	}

	// Token: 0x06004624 RID: 17956 RVA: 0x002AA71C File Offset: 0x002A971C
	public int ᜅ()
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
		return 8 + this.ᜊ.Length * 2;
	}

	// Token: 0x06004625 RID: 17957 RVA: 0x002AA764 File Offset: 0x002A9764
	public int ᜀ(DataProvider A_0, int A_1, int A_2)
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
			for (;;)
			{
				this.ᜆ = A_0.ReadUInt16(A_1);
				A_1 += 2;
				this.ᜇ = A_0.ReadUInt16(A_1);
				A_1 += 2;
				this.ᜈ = A_0.ReadUInt16(A_1);
				A_1 += 2;
				this.ᜉ = A_0.ReadUInt16(A_1);
				A_1 += 2;
				this.ᜊ = new ushort[A_2];
				int num = 0;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_9E;
					case 1:
						if (num >= A_2)
						{
							num2 = 3;
							continue;
						}
						this.ᜊ[num] = A_0.ReadUInt16(A_1);
						num++;
						A_1 += 2;
						num2 = 0;
						continue;
					case 2:
						goto IL_9E;
					case 3:
						goto IL_B5;
					}
					break;
					IL_9E:
					num2 = 1;
				}
			}
			break;
		}
		IL_B5:
		return A_2 * 2 + 8;
	}

	// Token: 0x06004626 RID: 17958 RVA: 0x002AA85C File Offset: 0x002A985C
	public int ᜀ(byte[] A_0, int A_1)
	{
		int num;
		int num2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A9:
			Buffer.BlockCopy(this.ᜊ, 0, A_0, A_1, num * 2);
			num2 = 2;
			break;
		default:
			if (false)
			{
			}
			goto IL_3A;
		}
		for (;;)
		{
			IL_28:
			switch (num2)
			{
			case 0:
				goto IL_A7;
			case 1:
				if (num > 0)
				{
					if (true)
					{
					}
					num2 = 0;
					continue;
				}
				goto IL_C7;
			case 2:
				goto IL_C5;
			}
			goto IL_3A;
		}
		IL_A7:
		goto IL_A9;
		IL_C5:
		IL_C7:
		return num * 2 + 8;
		IL_3A:
		BiffRecordRaw.SetUInt16(A_0, A_1, this.ᜆ);
		A_1 += 2;
		BiffRecordRaw.SetUInt16(A_0, A_1, this.ᜇ);
		A_1 += 2;
		BiffRecordRaw.SetUInt16(A_0, A_1, this.ᜈ);
		A_1 += 2;
		BiffRecordRaw.SetUInt16(A_0, A_1, this.ᜉ);
		A_1 += 2;
		num = this.ᜊ.Length;
		num2 = 1;
		goto IL_28;
	}

	// Token: 0x06004627 RID: 17959 RVA: 0x002AA938 File Offset: 0x002A9938
	public object ᜀ()
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
		spr\u197A spr_u197A = (spr\u197A)base.MemberwiseClone();
		spr_u197A.ᜊ = spr\u1CD3.ᜀ(this.ᜊ);
		return spr_u197A;
	}

	// Token: 0x04001FEF RID: 8175
	private const int ᜀ = 8;

	// Token: 0x04001FF0 RID: 8176
	private const int ᜁ = 0;

	// Token: 0x04001FF1 RID: 8177
	private const int ᜂ = 9;

	// Token: 0x04001FF2 RID: 8178
	private const int ᜃ = 10;

	// Token: 0x04001FF3 RID: 8179
	private const int ᜄ = 11;

	// Token: 0x04001FF4 RID: 8180
	private const int ᜅ = 12;

	// Token: 0x04001FF5 RID: 8181
	private ushort ᜆ;

	// Token: 0x04001FF6 RID: 8182
	private ushort ᜇ;

	// Token: 0x04001FF7 RID: 8183
	private ushort ᜈ;

	// Token: 0x04001FF8 RID: 8184
	private ushort ᜉ;

	// Token: 0x04001FF9 RID: 8185
	private ushort[] ᜊ;

	// Token: 0x0200047A RID: 1146
	public enum LineItemType
	{
		// Token: 0x04001FFB RID: 8187
		Data,
		// Token: 0x04001FFC RID: 8188
		Default,
		// Token: 0x04001FFD RID: 8189
		Sum,
		// Token: 0x04001FFE RID: 8190
		CountA,
		// Token: 0x04001FFF RID: 8191
		Count,
		// Token: 0x04002000 RID: 8192
		Average,
		// Token: 0x04002001 RID: 8193
		Max,
		// Token: 0x04002002 RID: 8194
		Min,
		// Token: 0x04002003 RID: 8195
		Product,
		// Token: 0x04002004 RID: 8196
		Stdev,
		// Token: 0x04002005 RID: 8197
		StdevP,
		// Token: 0x04002006 RID: 8198
		Var,
		// Token: 0x04002007 RID: 8199
		VarP,
		// Token: 0x04002008 RID: 8200
		GrandTotal
	}
}
