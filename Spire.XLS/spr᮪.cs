using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200036E RID: 878
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.CRN)]
internal class spr\u1BAA : BiffRecordRaw, ICloneable
{
	// Token: 0x06003593 RID: 13715 RVA: 0x001E8D04 File Offset: 0x001E7D04
	public virtual bool ᜂ()
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
		return true;
	}

	// Token: 0x06003594 RID: 13716 RVA: 0x001E8D40 File Offset: 0x001E7D40
	public byte ᜁ()
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

	// Token: 0x06003595 RID: 13717 RVA: 0x001E8D84 File Offset: 0x001E7D84
	public void ᜀ(byte A_0)
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
	}

	// Token: 0x06003596 RID: 13718 RVA: 0x001E8DC8 File Offset: 0x001E7DC8
	public byte ᜅ()
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
		return this.ᜅ;
	}

	// Token: 0x06003597 RID: 13719 RVA: 0x001E8E0C File Offset: 0x001E7E0C
	public void ᜁ(byte A_0)
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

	// Token: 0x06003598 RID: 13720 RVA: 0x001E8E50 File Offset: 0x001E7E50
	public ushort ᜀ()
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

	// Token: 0x06003599 RID: 13721 RVA: 0x001E8E94 File Offset: 0x001E7E94
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

	// Token: 0x0600359A RID: 13722 RVA: 0x001E8ED8 File Offset: 0x001E7ED8
	public virtual int ᜃ()
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
		return 4;
	}

	// Token: 0x0600359B RID: 13723 RVA: 0x001E8F14 File Offset: 0x001E7F14
	public List<object> ᜄ()
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

	// Token: 0x0600359C RID: 13724 RVA: 0x001E8F58 File Offset: 0x001E7F58
	public spr\u1BAA()
	{
	}

	// Token: 0x0600359D RID: 13725 RVA: 0x001E8F78 File Offset: 0x001E7F78
	public spr\u1BAA(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600359E RID: 13726 RVA: 0x001E8F98 File Offset: 0x001E7F98
	public spr\u1BAA(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600359F RID: 13727 RVA: 0x001E8FB8 File Offset: 0x001E7FB8
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		for (;;)
		{
			int num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_99:
				if (true)
				{
				}
				object item = this.ᜀ(A_0, ref A_1);
				this.ᜇ.Add(item);
				num = 2;
				break;
			}
			default:
				if (false)
				{
				}
				this.ᜄ = A_0.ReadByte(A_1);
				this.ᜅ = A_0.ReadByte(A_1 + 1);
				this.ᜆ = A_0.ReadUInt16(A_1 + 2);
				num2 = A_1;
				A_1 += 4;
				this.ᜇ.Clear();
				num = 3;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1 - num2 >= A_2)
					{
						num = 1;
						continue;
					}
					goto IL_99;
				case 1:
					return;
				case 2:
					goto IL_7B;
				case 3:
					goto IL_7B;
				}
				break;
				IL_7B:
				num = 0;
			}
		}
	}

	// Token: 0x060035A0 RID: 13728 RVA: 0x001E9094 File Offset: 0x001E8094
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		for (;;)
		{
			this.m_iLength = this.GetStoreSize(A_2);
			A_0.WriteByte(A_1, this.ᜄ);
			A_0.WriteByte(A_1 + 1, this.ᜅ);
			A_0.WriteUInt16(A_1 + 2, this.ᜆ);
			A_1 += 4;
			int num = 0;
			int count = this.ᜇ.Count;
			int num2 = 3;
			for (;;)
			{
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7F;
					default:
						goto IL_AF;
					}
					break;
				case 1:
					goto IL_7F;
				case 2:
					if (num >= count)
					{
						num2 = 0;
						continue;
					}
					A_1 = this.ᜀ(A_0, A_1, this.ᜇ[num]);
					num++;
					num2 = 1;
					continue;
				case 3:
					goto IL_7F;
				}
				break;
				IL_7F:
				num2 = 2;
			}
		}
		IL_AF:
		if (false)
		{
		}
	}

	// Token: 0x060035A1 RID: 13729 RVA: 0x001E9180 File Offset: 0x001E8180
	private object ᜀ(DataProvider A_0, ref int A_1)
	{
		int a_ = 18;
		object result;
		for (;;)
		{
			result = null;
			byte b = A_0.ReadByte(A_1);
			A_1++;
			spr\u1BAA.CellValueType cellValueType = (spr\u1BAA.CellValueType)b;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (cellValueType)
					{
					case spr\u1BAA.CellValueType.Nil:
						A_1 += 11;
						num = 2;
						continue;
					case spr\u1BAA.CellValueType.Number:
						result = A_0.ReadDouble(A_1);
						A_1 += 8;
						num = 6;
						continue;
					case spr\u1BAA.CellValueType.String:
						result = A_0.ReadString16BitUpdateOffset(ref A_1);
						goto IL_14B;
					case (spr\u1BAA.CellValueType)3:
						goto IL_FB;
					case spr\u1BAA.CellValueType.Boolean:
						result = A_0.ReadBoolean(A_1);
						A_1 += 8;
						num = 9;
						continue;
					default:
						num = 3;
						continue;
					}
					break;
				case 1:
					num = 8;
					continue;
				case 2:
					return result;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_14B;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				case 4:
					goto IL_BB;
				case 5:
					return result;
				case 6:
					return result;
				case 7:
					if (cellValueType != spr\u1BAA.CellValueType.Error)
					{
						num = 1;
						continue;
					}
					if (true)
					{
					}
					result = A_0.ReadByte(A_1);
					A_1 += 8;
					num = 4;
					continue;
				case 8:
					goto IL_F9;
				case 9:
					goto IL_E9;
				}
				break;
				IL_14B:
				num = 5;
			}
		}
		IL_BB:
		IL_E9:
		return result;
		IL_F9:
		IL_FB:
		throw new ApplicationException(RecordTableEnumerator.b("ᵇ⑉❋⁍㽏║㩓癕㱗㭙⡛㽝䁟ᙡᵣᙥ൧", a_));
	}

	// Token: 0x060035A2 RID: 13730 RVA: 0x001E9308 File Offset: 0x001E8308
	private int ᜀ(DataProvider A_0, int A_1, object A_2)
	{
		int a_ = 7;
		int num = 14;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_0.WriteByte(A_1++, 0);
				num = 10;
				continue;
			case 1:
				A_0.WriteByte(A_1, 4);
				A_1++;
				num = 11;
				continue;
			case 2:
				if (A_2 is bool)
				{
					num = 1;
					continue;
				}
				num = 9;
				continue;
			case 3:
				A_0.WriteByte(A_1, 1);
				A_1++;
				A_0.WriteDouble(A_1, (double)A_2);
				A_1 += 8;
				num = 8;
				continue;
			case 4:
				goto IL_253;
			case 5:
				A_0.WriteByte(A_1++, 16);
				A_0.WriteByte(A_1++, (byte)A_2);
				A_0.WriteBytes(A_1, spr\u1BAA.ᜃ);
				A_1 += spr\u1BAA.ᜃ.Length;
				num = 4;
				continue;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D2;
				default:
					goto IL_1E9;
				}
				break;
			case 7:
				if (A_2 is double)
				{
					num = 3;
					continue;
				}
				goto IL_D2;
			case 8:
				goto IL_C5;
			case 9:
				if (A_2 is byte)
				{
					num = 5;
					continue;
				}
				goto IL_255;
			case 10:
				goto IL_14A;
			case 11:
				A_0.WriteByte(A_1++, ((bool)A_2) ? 1 : 0);
				A_0.WriteBytes(A_1, spr\u1BAA.ᜃ);
				A_1 += spr\u1BAA.ᜃ.Length;
				num = 6;
				continue;
			case 12:
			{
				A_0.WriteByte(A_1, 2);
				A_1++;
				string text = A_2 as string;
				A_0.WriteString16BitUpdateOffset(ref A_1, text);
				num = 16;
				continue;
			}
			case 13:
				if (A_2 is string)
				{
					num = 12;
					continue;
				}
				num = 2;
				continue;
			case 15:
				goto IL_68;
			case 16:
			{
				string text;
				if (text.Length == 0)
				{
					num = 0;
					continue;
				}
				return A_1;
			}
			}
			if (A_2 == null)
			{
				num = 15;
				continue;
			}
			num = 7;
			continue;
			IL_D2:
			num = 13;
		}
		IL_68:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬼帾ⵀ㙂⁄", a_));
		IL_C5:
		if (true)
		{
		}
		IL_14A:
		return A_1;
		IL_1E9:
		if (false)
		{
		}
		IL_253:
		return A_1;
		IL_255:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("樼䴾⹀ⵂ≄杆ⵈ⩊㥌⹎煐❒ⱔ❖㱘", a_));
	}

	// Token: 0x060035A3 RID: 13731 RVA: 0x001E9580 File Offset: 0x001E8580
	public virtual int ᜀ(ExcelVersion A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				num = 4;
				int num2 = 0;
				int count = this.ᜇ.Count;
				int num3 = 1;
				for (;;)
				{
					switch (num3)
					{
					case 0:
					{
						if (num2 >= count)
						{
							num3 = 4;
							continue;
						}
						object obj = this.ᜇ[num2];
						string text = obj as string;
						num3 = 2;
						continue;
					}
					case 1:
						goto IL_C8;
					case 2:
					{
						string text;
						if (text == null)
						{
							num += 9;
							num3 = 6;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return num;
						default:
							if (false)
							{
							}
							num3 = 3;
							continue;
						}
						break;
					}
					case 3:
					{
						string text;
						num += 4 + text.Length * 2;
						num3 = 7;
						continue;
					}
					case 4:
						return num;
					case 5:
						if (true)
						{
						}
						goto IL_C8;
					case 6:
						goto IL_52;
					case 7:
						goto IL_52;
					}
					break;
					IL_52:
					num2++;
					num3 = 5;
					continue;
					IL_C8:
					num3 = 0;
				}
			}
			return num;
		}
		}
	}

	// Token: 0x060035A4 RID: 13732 RVA: 0x001E96A0 File Offset: 0x001E86A0
	public virtual object ᜆ()
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
		spr\u1BAA spr_u1BAA = (spr\u1BAA)base.Clone();
		spr_u1BAA.ᜇ = new List<object>(this.ᜇ);
		return spr_u1BAA;
	}

	// Token: 0x060035A5 RID: 13733 RVA: 0x001E96FC File Offset: 0x001E86FC
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u1BAA()
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
		byte[] array = new byte[7];
		spr\u1BAA.ᜃ = array;
	}

	// Token: 0x04001758 RID: 5976
	private new const int ᜀ = 4;

	// Token: 0x04001759 RID: 5977
	private const string ᜁ = "Unknown data type";

	// Token: 0x0400175A RID: 5978
	private const int ᜂ = 8;

	// Token: 0x0400175B RID: 5979
	private new static readonly byte[] ᜃ;

	// Token: 0x0400175C RID: 5980
	[spr\u2429(0, 1)]
	private byte ᜄ;

	// Token: 0x0400175D RID: 5981
	[spr\u2429(1, 1)]
	private byte ᜅ;

	// Token: 0x0400175E RID: 5982
	[spr\u2429(2, 2)]
	private ushort ᜆ;

	// Token: 0x0400175F RID: 5983
	private List<object> ᜇ = new List<object>();

	// Token: 0x0200036F RID: 879
	private enum CellValueType
	{
		// Token: 0x04001761 RID: 5985
		Nil,
		// Token: 0x04001762 RID: 5986
		Number,
		// Token: 0x04001763 RID: 5987
		String,
		// Token: 0x04001764 RID: 5988
		Boolean = 4,
		// Token: 0x04001765 RID: 5989
		Error = 16
	}
}
