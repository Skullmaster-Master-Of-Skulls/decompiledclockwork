using System;
using System.IO;
using System.Text;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000572 RID: 1394
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.CustomProperty)]
internal class sprế : BiffRecordRaw
{
	// Token: 0x060053BF RID: 21439 RVA: 0x0034186C File Offset: 0x0034086C
	public sprế()
	{
	}

	// Token: 0x060053C0 RID: 21440 RVA: 0x00341880 File Offset: 0x00340880
	public sprế(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060053C1 RID: 21441 RVA: 0x00341898 File Offset: 0x00340898
	public sprế(int A_0) : base(A_0)
	{
	}

	// Token: 0x060053C2 RID: 21442 RVA: 0x003418AC File Offset: 0x003408AC
	public string ᜀ()
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

	// Token: 0x060053C3 RID: 21443 RVA: 0x003418F0 File Offset: 0x003408F0
	public void ᜁ(string A_0)
	{
		int a_ = 17;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_DB;
			case 1:
				goto IL_7F;
			case 2:
				goto IL_60;
			case 3:
				if (A_0.Length > 255)
				{
					num = 1;
					continue;
				}
				goto IL_DD;
			case 5:
				if (A_0.Length == 0)
				{
					num = 0;
					continue;
				}
				num = 3;
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
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				break;
			}
			num = 5;
		}
		IL_60:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅆ⡈❊㡌⩎", a_));
		IL_7F:
		throw new ArgumentException(RecordTableEnumerator.b("ㅆ⡈❊㡌⩎煐繒畔⑖ⵘ⥚㑜ㅞ٠䍢౤ᑦ䥨Ὢɬn兰ὲᩴ᥶Ṹ啺", a_));
		IL_DB:
		throw new ArgumentException(RecordTableEnumerator.b("ㅆ⡈❊㡌⩎煐繒畔⑖ⵘ⥚㑜ㅞ٠䍢٤٦ݨժɬ᭮兰ᅲၴ坶ᱸᙺർ୾궂", a_));
		IL_DD:
		this.ᜃ = A_0;
	}

	// Token: 0x060053C4 RID: 21444 RVA: 0x003419EC File Offset: 0x003409EC
	public string ᜁ()
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
		return this.ᜄ;
	}

	// Token: 0x060053C5 RID: 21445 RVA: 0x00341A30 File Offset: 0x00340A30
	public void ᜀ(string A_0)
	{
		int a_ = 3;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_5A;
			case 1:
				if (A_0.Length == 0)
				{
					goto IL_80;
				}
				goto IL_A6;
			case 3:
				goto IL_88;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_80:
				num = 3;
				break;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 0;
				}
				else
				{
					num = 1;
				}
				break;
			}
		}
		IL_5A:
		throw new ArgumentNullException(RecordTableEnumerator.b("伸娺儼䨾⑀", a_));
		IL_88:
		if (true)
		{
		}
		throw new ArgumentException(RecordTableEnumerator.b("伸娺儼䨾⑀捂桄杆㩈㽊㽌♎㽐㑒畔㑖㡘㕚㍜ぞᕠ䍢ݤɦ䥨๪lὮհੲ孴", a_));
		IL_A6:
		this.ᜄ = A_0;
	}

	// Token: 0x060053C6 RID: 21446 RVA: 0x00341AEC File Offset: 0x00340AEC
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
		A_1 += 2;
		int stringLength = A_0.ReadInt32(A_1);
		A_1 += 4;
		int num = (int)A_0.ReadByte(A_1);
		A_1++;
		Encoding ascii = Encoding.ASCII;
		this.ᜃ = A_0.ReadString(A_1, num, ascii, false);
		A_1 += num;
		this.ᜄ = A_0.ReadString(A_1, stringLength, Encoding.Unicode, true);
	}

	// Token: 0x060053C7 RID: 21447 RVA: 0x00341B78 File Offset: 0x00340B78
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
		this.m_iLength = this.GetStoreSize(A_2);
		A_0.WriteBytes(A_1, sprế.ᜂ, 0, sprế.ᜂ.Length);
		A_1 += sprế.ᜂ.Length;
		int num = A_1;
		A_1 += 4;
		A_1++;
		Encoding ascii = Encoding.ASCII;
		byte[] bytes = ascii.GetBytes(this.ᜃ);
		int num2 = bytes.Length;
		A_0.WriteBytes(A_1, bytes, 0, num2);
		A_0.WriteByte(num + 4, (byte)num2);
		A_1 += num2;
		bytes = Encoding.Unicode.GetBytes(this.ᜄ);
		num2 = bytes.Length;
		A_0.WriteBytes(A_1, bytes, 0, num2);
		A_0.WriteInt32(num, num2);
	}

	// Token: 0x060053C8 RID: 21448 RVA: 0x00341C44 File Offset: 0x00340C44
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
		Encoding ascii = Encoding.ASCII;
		return 7 + ascii.GetByteCount(this.ᜃ) + Encoding.Unicode.GetByteCount(this.ᜄ);
	}

	// Token: 0x060053C9 RID: 21449 RVA: 0x00341CA4 File Offset: 0x00340CA4
	// Note: this type is marked as 'beforefieldinit'.
	static sprế()
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
		sprế.ᜂ = new byte[]
		{
			0,
			16
		};
	}

	// Token: 0x04002726 RID: 10022
	private new const int ᜀ = 7;

	// Token: 0x04002727 RID: 10023
	private const int ᜁ = 255;

	// Token: 0x04002728 RID: 10024
	private static readonly byte[] ᜂ;

	// Token: 0x04002729 RID: 10025
	private new string ᜃ;

	// Token: 0x0400272A RID: 10026
	private string ᜄ;
}
