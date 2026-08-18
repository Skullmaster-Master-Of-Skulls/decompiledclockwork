using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000599 RID: 1433
[spr\u2593(TBIFFRecord.Selection)]
[CLSCompliant(false)]
internal class spr\u21A4 : BiffRecordRaw, ICloneable
{
	// Token: 0x06005701 RID: 22273 RVA: 0x003776A4 File Offset: 0x003766A4
	public byte ᜀ()
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

	// Token: 0x06005702 RID: 22274 RVA: 0x003776E8 File Offset: 0x003766E8
	public void ᜀ(byte A_0)
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

	// Token: 0x06005703 RID: 22275 RVA: 0x0037772C File Offset: 0x0037672C
	public ushort ᜂ()
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

	// Token: 0x06005704 RID: 22276 RVA: 0x00377770 File Offset: 0x00376770
	public void ᜂ(ushort A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x06005705 RID: 22277 RVA: 0x003777B4 File Offset: 0x003767B4
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
		return this.ᜄ;
	}

	// Token: 0x06005706 RID: 22278 RVA: 0x003777F8 File Offset: 0x003767F8
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
		this.ᜄ = A_0;
	}

	// Token: 0x06005707 RID: 22279 RVA: 0x0037783C File Offset: 0x0037683C
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
		return this.ᜅ;
	}

	// Token: 0x06005708 RID: 22280 RVA: 0x00377880 File Offset: 0x00376880
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
		this.ᜅ = A_0;
	}

	// Token: 0x06005709 RID: 22281 RVA: 0x003778C4 File Offset: 0x003768C4
	public new ushort ᜃ()
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

	// Token: 0x0600570A RID: 22282 RVA: 0x00377908 File Offset: 0x00376908
	public virtual int ᜅ()
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
		return 9;
	}

	// Token: 0x0600570B RID: 22283 RVA: 0x00377948 File Offset: 0x00376948
	public spr\u21A4.ᜀ[] ᜆ()
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
		return this.ᜇ.ToArray();
	}

	// Token: 0x0600570C RID: 22284 RVA: 0x00377990 File Offset: 0x00376990
	public void ᜀ(spr\u21A4.ᜀ[] A_0)
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
		this.ᜇ = new List<spr\u21A4.ᜀ>(A_0);
		this.ᜆ = (ushort)this.ᜇ.Count;
	}

	// Token: 0x0600570D RID: 22285 RVA: 0x003779EC File Offset: 0x003769EC
	public void ᜀ(int A_0, spr\u21A4.ᜀ A_1)
	{
		int a_ = 12;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6A;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					break;
				}
				break;
			case 1:
				goto IL_92;
			case 2:
				num = 3;
				continue;
			case 3:
				if (A_0 < 0)
				{
					num = 1;
					continue;
				}
				goto IL_94;
			}
			if (A_0 >= (int)this.ᜃ())
			{
				break;
			}
			num = 2;
		}
		IL_6A:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭁ൃ⡅ⱇ⽉㑋", a_));
		IL_92:
		goto IL_6A;
		IL_94:
		this.ᜇ[A_0] = A_1;
	}

	// Token: 0x0600570E RID: 22286 RVA: 0x00377A9C File Offset: 0x00376A9C
	public spr\u21A4()
	{
	}

	// Token: 0x0600570F RID: 22287 RVA: 0x00377AE8 File Offset: 0x00376AE8
	public spr\u21A4(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06005710 RID: 22288 RVA: 0x00377B34 File Offset: 0x00376B34
	public spr\u21A4(int A_0) : base(A_0)
	{
	}

	// Token: 0x06005711 RID: 22289 RVA: 0x00377B80 File Offset: 0x00376B80
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		int a_ = 7;
		for (;;)
		{
			this.ᜂ = A_0.ReadByte(A_1);
			this.ᜃ = A_0.ReadUInt16(A_1 + 1);
			this.ᜄ = A_0.ReadUInt16(A_1 + 3);
			this.ᜅ = A_0.ReadUInt16(A_1 + 5);
			this.ᜆ = A_0.ReadUInt16(A_1 + 7);
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_16E;
				case 1:
					return;
				case 2:
				{
					int num2;
					if (num2 >= (int)this.ᜆ)
					{
						num = 1;
						continue;
					}
					spr\u21A4.ᜀ item;
					int num3;
					item.ᜀ = A_0.ReadUInt16(A_1 + num3);
					item.ᜁ = A_0.ReadUInt16(A_1 + num3 + 2);
					item.ᜂ = A_0.ReadByte(A_1 + num3 + 4);
					item.ᜃ = A_0.ReadByte(A_1 + num3 + 5);
					this.ᜇ.Add(item);
					num2++;
					num3 += 6;
					num = 0;
					continue;
				}
				case 3:
					goto IL_9B;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						goto IL_16E;
					}
					break;
				case 5:
				{
					if (this.m_iLength < (int)(9 + this.ᜆ * 6))
					{
						num = 3;
						continue;
					}
					spr\u21A4.ᜀ item = default(spr\u21A4.ᜀ);
					int num3 = 9;
					this.ᜇ.Clear();
					int num2 = 0;
					num = 4;
					continue;
				}
				}
				break;
				IL_16E:
				num = 2;
			}
		}
		IL_9B:
		if (true)
		{
		}
		throw new sprῩ(RecordTableEnumerator.b("礼帾㕀≂敄⭆ⱈ╊⩌㭎㥐獒ㅔ㡖㱘⡚絜ㅞ๠ᝢ䕤ŦhὪ䵬᭮Ṱ卲᭴ɶᑸ᥺᡼ൾꆀꞆﮈﶒ떚", a_));
	}

	// Token: 0x06005712 RID: 22290 RVA: 0x00377D20 File Offset: 0x00376D20
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		for (;;)
		{
			this.m_iLength = 9;
			A_0.WriteByte(A_1, this.ᜂ);
			A_0.WriteUInt16(A_1 + 1, this.ᜃ);
			A_0.WriteUInt16(A_1 + 3, this.ᜄ);
			A_0.WriteUInt16(A_1 + 5, this.ᜅ);
			A_0.WriteUInt16(A_1 + 7, this.ᜆ);
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_75;
				case 1:
					return;
				case 2:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
					{
						if (false)
						{
						}
						if (num >= (int)this.ᜆ)
						{
							num2 = 1;
							continue;
						}
						spr\u21A4.ᜀ ᜀ = this.ᜇ[num];
						A_0.WriteUInt16(A_1 + this.m_iLength, ᜀ.ᜀ);
						A_0.WriteUInt16(A_1 + this.m_iLength + 2, ᜀ.ᜁ);
						A_0.WriteByte(A_1 + this.m_iLength + 4, ᜀ.ᜂ);
						A_0.WriteByte(A_1 + this.m_iLength + 5, ᜀ.ᜃ);
						num++;
						this.m_iLength += 6;
						num2 = 3;
						continue;
					}
					}
					break;
				case 3:
					goto IL_75;
				}
				break;
				IL_75:
				num2 = 2;
			}
		}
	}

	// Token: 0x06005713 RID: 22291 RVA: 0x00377E80 File Offset: 0x00376E80
	public virtual int ᜀ(ExcelVersion A_0)
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
		return 9 + this.ᜇ.Count * 6;
	}

	// Token: 0x06005714 RID: 22292 RVA: 0x00377ECC File Offset: 0x00376ECC
	public object ᜇ()
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
		spr\u21A4 spr_u21A = (spr\u21A4)base.MemberwiseClone();
		spr_u21A.ᜇ = new List<spr\u21A4.ᜀ>(this.ᜇ);
		return spr_u21A;
	}

	// Token: 0x04002956 RID: 10582
	private new const int ᜀ = 9;

	// Token: 0x04002957 RID: 10583
	private const int ᜁ = 6;

	// Token: 0x04002958 RID: 10584
	[spr\u2429(0, 1)]
	private byte ᜂ = 3;

	// Token: 0x04002959 RID: 10585
	[spr\u2429(1, 2)]
	private new ushort ᜃ;

	// Token: 0x0400295A RID: 10586
	[spr\u2429(3, 2)]
	private ushort ᜄ;

	// Token: 0x0400295B RID: 10587
	[spr\u2429(5, 2)]
	private ushort ᜅ;

	// Token: 0x0400295C RID: 10588
	[spr\u2429(7, 2)]
	private ushort ᜆ = 1;

	// Token: 0x0400295D RID: 10589
	private List<spr\u21A4.ᜀ> ᜇ = new List<spr\u21A4.ᜀ>(new spr\u21A4.ᜀ[]
	{
		default(spr\u21A4.ᜀ)
	});

	// Token: 0x0200059A RID: 1434
	internal new struct ᜀ
	{
		// Token: 0x06005715 RID: 22293 RVA: 0x00377F28 File Offset: 0x00376F28
		public ᜀ(ushort A_0, ushort A_1, byte A_2, byte A_3)
		{
			this.ᜀ = A_0;
			this.ᜁ = A_1;
			this.ᜂ = A_2;
			this.ᜃ = A_3;
		}

		// Token: 0x06005716 RID: 22294 RVA: 0x00377F54 File Offset: 0x00376F54
		public string ᜀ()
		{
			int a_ = 17;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return string.Format(RecordTableEnumerator.b("ⅆ⁈㥊㹌㭎͐㱒≔浖祘⁚浜≞䵠䍢।٦ᩨὪ㽬nٰ䥲啴౶䡸ٺ兼彾ﶈ좊ﺒﮔ궖릘꾜趠莢즤욦\udaa8\udfaa삮\uddb0욲\ud8b4\ud9b6莸鮺욼貾변", a_), new object[]
			{
				this.ᜀ,
				this.ᜁ,
				this.ᜂ,
				this.ᜃ
			});
		}

		// Token: 0x0400295E RID: 10590
		public ushort ᜀ;

		// Token: 0x0400295F RID: 10591
		public ushort ᜁ;

		// Token: 0x04002960 RID: 10592
		public byte ᜂ;

		// Token: 0x04002961 RID: 10593
		public byte ᜃ;
	}
}
