using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000268 RID: 616
[CLSCompliant(false)]
internal abstract class spr\u1D3B : spr\u251F
{
	// Token: 0x06002521 RID: 9505 RVA: 0x00159830 File Offset: 0x00158830
	static spr\u1D3B()
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
		spr\u1D3B.ᜈ = new Dictionary<Type, int>();
		spr\u1D3B.ᜈ.Add(typeof(spr\u2016), 61453);
		spr\u1D3B.ᜈ.Add(typeof(sprἼ), 61450);
		spr\u1D3B.ᜈ.Add(typeof(spr\u21EB), 61443);
		spr\u1D3B.ᜈ.Add(typeof(spr\u232D), 61454);
		spr\u1D3B.ᜈ.Add(typeof(sprᮋ), 61456);
		spr\u1D3B.ᜈ.Add(typeof(spr\u20A0), 61442);
		spr\u1D3B.ᜈ.Add(typeof(spr\u262B), 61720);
		spr\u1D3B.ᜈ.Add(typeof(spr\u2608), 61448);
		spr\u1D3B.ᜈ.Add(typeof(sprᬈ), 61440);
		spr\u1D3B.ᜈ.Add(typeof(spr\u23E7), 61451);
		spr\u1D3B.ᜈ.Add(typeof(sprὙ), 61444);
		spr\u1D3B.ᜈ.Add(typeof(spr\u227E), 61726);
		spr\u1D3B.ᜈ.Add(typeof(spr\u2412), 61446);
		spr\u1D3B.ᜈ.Add(typeof(sprᜪ), 61447);
		spr\u1D3B.ᜈ.Add(typeof(spr\u1B5C), 61449);
		spr\u1D3B.ᜈ.Add(typeof(spr\u1C27), 61441);
		spr\u1D3B.ᜈ.Add(typeof(spr᪙), 61457);
		spr\u1D3B.ᜈ.Add(typeof(sprᢦ), 65535);
		spr\u1D3B.ᜈ.Add(typeof(spr\u23CF), 61455);
		spr\u1D3B.ᜈ.Add(typeof(spr\u17B7), 0);
		spr\u1D3B.ᜈ.Add(typeof(spr៣), 0);
	}

	// Token: 0x06002522 RID: 9506 RVA: 0x00159A7C File Offset: 0x00158A7C
	public spr\u1D3B()
	{
		Type type = base.GetType();
		spr\u1D3B.ᜈ.TryGetValue(type, out this.m_iCode);
		this.ᜅ = (ushort)this.m_iCode;
	}

	// Token: 0x06002523 RID: 9507 RVA: 0x00159AB8 File Offset: 0x00158AB8
	public spr\u1D3B(spr\u1D3B A_0) : this()
	{
		this.ᜇ = A_0;
	}

	// Token: 0x06002524 RID: 9508 RVA: 0x00159AD4 File Offset: 0x00158AD4
	public spr\u1D3B(spr\u1D3B A_0, byte[] A_1, int A_2) : this(A_0, A_1, A_2, null)
	{
	}

	// Token: 0x06002525 RID: 9509 RVA: 0x00159AEC File Offset: 0x00158AEC
	public spr\u1D3B(spr\u1D3B A_0, byte[] A_1, int A_2, spr\u24C9 A_3) : this(A_0)
	{
		this.ᜆ = A_3;
		this.ᜀ(A_1, A_2);
	}

	// Token: 0x06002526 RID: 9510 RVA: 0x00159B14 File Offset: 0x00158B14
	public spr\u1D3B(spr\u1D3B A_0, Stream A_1, spr\u24C9 A_2) : this(A_0)
	{
		this.ᜆ = A_2;
		this.ᜅ(A_1);
	}

	// Token: 0x06002527 RID: 9511 RVA: 0x00159B38 File Offset: 0x00158B38
	public int \u1713()
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
		return (int)BiffRecordRaw.ᜀ(this.ᜄ, 15);
	}

	// Token: 0x06002528 RID: 9512 RVA: 0x00159B80 File Offset: 0x00158B80
	public void ᜉ(int A_0)
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
		BiffRecordRaw.ᜀ(ref this.ᜄ, 15, (ushort)A_0);
	}

	// Token: 0x06002529 RID: 9513 RVA: 0x00159BCC File Offset: 0x00158BCC
	public int \u1714()
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
		return BiffRecordRaw.ᜀ(this.ᜄ, 65520) >> 4;
	}

	// Token: 0x0600252A RID: 9514 RVA: 0x00159C1C File Offset: 0x00158C1C
	public void ᜈ(int A_0)
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
		BiffRecordRaw.ᜀ(ref this.ᜄ, 65520, (ushort)(A_0 << 4));
	}

	// Token: 0x0600252B RID: 9515 RVA: 0x00159C6C File Offset: 0x00158C6C
	public MsoRecords \u1717()
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
		return (MsoRecords)this.ᜅ;
	}

	// Token: 0x0600252C RID: 9516 RVA: 0x00159CB0 File Offset: 0x00158CB0
	public new void ᜀ(MsoRecords A_0)
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
		this.ᜅ = (ushort)A_0;
	}

	// Token: 0x0600252D RID: 9517 RVA: 0x00159CF4 File Offset: 0x00158CF4
	public spr\u24C9 \u1716()
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

	// Token: 0x0600252E RID: 9518 RVA: 0x00159D38 File Offset: 0x00158D38
	public new void ᜀ(spr\u24C9 A_0)
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

	// Token: 0x0600252F RID: 9519 RVA: 0x00159D7C File Offset: 0x00158D7C
	public spr\u1D3B \u1718()
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

	// Token: 0x06002530 RID: 9520 RVA: 0x00159DC0 File Offset: 0x00158DC0
	public new virtual int ᜀ(byte[] A_0, int A_1)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 1:
					goto IL_4C;
				case 2:
					try
					{
						num = 8;
						int result;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (this.m_iLength < this.MinimumRecordSize)
								{
									num = 10;
									continue;
								}
								goto IL_26A;
							case 1:
								goto IL_326;
							case 2:
								if (A_0.Length - A_1 - this.m_iLength < 0)
								{
									num = 3;
									continue;
								}
								this.ᜀ = new byte[this.m_iLength];
								Array.Copy(A_0, A_1, this.ᜀ, 0, this.m_iLength);
								this.ᜂ();
								result = this.m_iLength + 8;
								num = 1;
								continue;
							case 3:
								goto IL_265;
							case 4:
								goto IL_BE;
							case 5:
								goto IL_10A;
							case 6:
								if (this.m_iLength > this.MaximumRecordSize)
								{
									num = 9;
									continue;
								}
								num = 2;
								continue;
							case 7:
								if (this.ᜅ == 0)
								{
									num = 5;
									continue;
								}
								this.m_iLength = BitConverter.ToInt32(A_0, A_1);
								A_1 += 4;
								num = 0;
								continue;
							case 9:
								goto IL_290;
							case 10:
								goto IL_2E1;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								IL_26A:
								num = 6;
								break;
							default:
								if (false)
								{
								}
								if (A_0.Length - A_1 - 8 < 0)
								{
									num = 4;
								}
								else
								{
									this.ᜄ = BitConverter.ToUInt16(A_0, A_1);
									A_1 += 2;
									this.ᜅ = BitConverter.ToUInt16(A_0, A_1);
									A_1 += 2;
									num = 7;
								}
								break;
							}
						}
						IL_BE:
						throw new ApplicationException(RecordTableEnumerator.b("ᙂ⭄≆ㅈ㭊⡌ⱎ═㙒ㅔ睖㱘㕚㥜罞๠բ䕤ᕦ౨ࡪɬᵮᕰ卲塴坶୸Ṻᱼ᱾Ꞇ꾎ﺐ떔ﺚ붜ﺞ펠톢쒤\udea6螨", a_));
						IL_10A:
						throw new ApplicationException(RecordTableEnumerator.b("โ㙄⡆楈᥊⡌ⱎ㹐⅒ㅔ睖じ㽚㡜ㅞᕠ੢ͤ๦੨੪ᥬٮṰᵲ啴ᑶᙸὺ᡼彾ꖄﮈ놐뮒뒜놞", a_));
						IL_265:
						throw new ApplicationException(RecordTableEnumerator.b("ᙂ⭄≆ㅈ㭊⡌ⱎ═㙒ㅔ睖㱘㕚㥜罞๠բ䕤ᕦ౨ࡪɬᵮᕰr啴Ѷ൸ॺ᡼Ṿ궂ꖄ햆ﶎ뎒漢붜ﲞ삠춢쮤좦\udda8讪쾬쪮醰솲킴횶\uddb8鮺邼龾돀ꛂ꓄꓆ꇈ껊꧌듐뷒뇔뛘뷚﷜곞闠釢胤蛦蓨엪", a_));
						IL_290:
						throw new spr\u2598(string.Concat(new object[]
						{
							RecordTableEnumerator.b("B⩄⍆ⱈ歊睌", a_),
							((MsoRecords)this.m_iCode).ToString(),
							this.m_iCode,
							RecordTableEnumerator.b("䥂敄ᕆⱈ⩊⅌潎≐㩒⽔㉖捘筚", a_),
							this.m_iLength,
							RecordTableEnumerator.b("浂敄Ɇㅈ㭊⡌ⱎ═㙒ㅔ睖⩘㉚❜㩞孠䍢", a_),
							this.MaximumRecordSize.ToString()
						}));
						IL_2E1:
						throw new spr\u1AEA(string.Concat(new object[]
						{
							RecordTableEnumerator.b("B⩄⍆ⱈ歊睌", a_),
							this.m_iCode.ToString(),
							RecordTableEnumerator.b("䥂敄ᕆⱈ⩊⅌潎≐㩒⽔㉖捘筚", a_),
							this.m_iLength,
							RecordTableEnumerator.b("浂敄Ɇㅈ㭊⡌ⱎ═㙒ㅔ睖⩘㉚❜㩞孠䍢", a_),
							this.MaximumRecordSize.ToString()
						}));
						IL_326:
						return result;
					}
					catch (ApplicationException ex)
					{
						Exception innerException = ex.InnerException;
						A_1 = num2;
						throw;
					}
					goto IL_335;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				IL_335:
				num2 = A_1;
				num = 2;
			}
			IL_4C:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
		}
		}
	}

	// Token: 0x06002531 RID: 9521 RVA: 0x0015A150 File Offset: 0x00159150
	public virtual void ᜆ(Stream A_0)
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
		this.ᜁ(A_0, 0, null, null);
	}

	// Token: 0x06002532 RID: 9522 RVA: 0x0015A198 File Offset: 0x00159198
	public virtual void ᜁ(Stream A_0, int A_1, List<int> A_2, List<List<BiffRecordRaw>> A_3)
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
		byte[] array = new byte[8];
		long position = A_0.Position;
		A_0.Position += 8L;
		this.ᜀ(A_0, A_1, A_2, A_3);
		long position2 = A_0.Position;
		A_0.Position = position;
		int num = 0;
		BitConverter.GetBytes(this.ᜄ).CopyTo(array, num);
		num += 2;
		BitConverter.GetBytes(this.ᜅ).CopyTo(array, num);
		num += 2;
		BitConverter.GetBytes(this.m_iLength).CopyTo(array, num);
		num += 4;
		A_0.Write(array, 0, num);
		A_0.Position = position2;
	}

	// Token: 0x06002533 RID: 9523 RVA: 0x0015A260 File Offset: 0x00159260
	public virtual int \u1715()
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
		return int.MaxValue;
	}

	// Token: 0x06002534 RID: 9524 RVA: 0x0015A2A0 File Offset: 0x001592A0
	public override void ᜀ(ExcelVersion A_0)
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
		MemoryStream a_ = new MemoryStream();
		this.ᜀ(a_, 0, null, null);
	}

	// Token: 0x06002535 RID: 9525
	public new abstract void ᜀ(Stream A_0, int A_1, List<int> A_2, List<List<BiffRecordRaw>> A_3);

	// Token: 0x06002536 RID: 9526 RVA: 0x0015A2EC File Offset: 0x001592EC
	public spr\u1D3B ᜁ(spr\u1D3B A_0)
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
		spr\u1D3B spr_u1D3B = (spr\u1D3B)this.ᜅ();
		spr_u1D3B.ᜇ = A_0;
		return spr_u1D3B;
	}

	// Token: 0x06002537 RID: 9527 RVA: 0x0015A33C File Offset: 0x0015933C
	protected virtual object ᜅ()
	{
		spr\u1D3B spr_u1D3B;
		for (;;)
		{
			spr_u1D3B = (spr\u1D3B)base.Clone();
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return spr_u1D3B;
				case 1:
					if (this.ᜀ != null)
					{
						num = 2;
						continue;
					}
					return spr_u1D3B;
				case 2:
					spr_u1D3B.ᜀ = spr\u1CD3.ᜀ(this.ᜀ);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return spr_u1D3B;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
		}
		return spr_u1D3B;
	}

	// Token: 0x06002538 RID: 9528 RVA: 0x0015A3D0 File Offset: 0x001593D0
	public virtual object ᜑ()
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
		return this.ᜅ();
	}

	// Token: 0x06002539 RID: 9529 RVA: 0x0015A414 File Offset: 0x00159414
	public virtual void ᜏ()
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
	}

	// Token: 0x0600253A RID: 9530
	public new abstract void ᜀ(Stream A_0);

	// Token: 0x0600253B RID: 9531 RVA: 0x0015A450 File Offset: 0x00159450
	public override void ᜂ()
	{
		int a_ = 19;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("ᵈ⍊⡌潎㱐㙒⅔㽖㙘㽚絜ぞ፠䍢੤ᝦ౨ᥪ౬᭮ᡰᱲ᭴坶ၸࡺ嵼ᅾꖄﲈﮊﶌ릘ﶚ膠횤좦ﮨ캪캬삮쎰ힲ운馶", a_));
	}

	// Token: 0x0600253C RID: 9532 RVA: 0x0015A4A8 File Offset: 0x001594A8
	public new static double ᜀ(int A_0)
	{
		switch (0)
		{
		default:
		{
			double num;
			for (;;)
			{
				byte[] bytes = BitConverter.GetBytes(A_0);
				num = (double)BitConverter.ToInt16(bytes, 0);
				ushort num2 = BitConverter.ToUInt16(bytes, 2);
				double num3 = 0.5;
				ushort num4 = 32768;
				int num5 = 0;
				int num6 = 4;
				for (;;)
				{
					switch (num6)
					{
					case 0:
						goto IL_6A;
					case 1:
						if (num5 >= 16)
						{
							num6 = 2;
							continue;
						}
						num6 = 6;
						continue;
					case 2:
						return num;
					case 3:
						goto IL_C8;
					case 4:
						goto IL_C8;
					case 5:
						if (true)
						{
						}
						num += num3;
						num6 = 0;
						continue;
					case 6:
						if ((num4 & num2) != 0)
						{
							num6 = 5;
							continue;
						}
						goto IL_6A;
					}
					break;
					IL_6A:
					num3 /= 2.0;
					num4 = (ushort)(num4 >> 1);
					num5++;
					num6 = 3;
					continue;
					IL_C8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return num;
					default:
						if (false)
						{
						}
						num6 = 1;
						break;
					}
				}
			}
			return num;
		}
		}
	}

	// Token: 0x0600253D RID: 9533 RVA: 0x0015A5C4 File Offset: 0x001595C4
	public new static void ᜀ(Stream A_0, int A_1)
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
		byte[] bytes = BitConverter.GetBytes(A_1);
		A_0.Write(bytes, 0, bytes.Length);
	}

	// Token: 0x0600253E RID: 9534 RVA: 0x0015A614 File Offset: 0x00159614
	public new static void ᜀ(Stream A_0, uint A_1)
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
		byte[] bytes = BitConverter.GetBytes(A_1);
		A_0.Write(bytes, 0, bytes.Length);
	}

	// Token: 0x0600253F RID: 9535 RVA: 0x0015A664 File Offset: 0x00159664
	public new static void ᜀ(Stream A_0, short A_1)
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
		byte[] bytes = BitConverter.GetBytes(A_1);
		A_0.Write(bytes, 0, bytes.Length);
	}

	// Token: 0x06002540 RID: 9536 RVA: 0x0015A6B4 File Offset: 0x001596B4
	public new static void ᜀ(Stream A_0, ushort A_1)
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
		byte[] bytes = BitConverter.GetBytes(A_1);
		A_0.Write(bytes, 0, bytes.Length);
	}

	// Token: 0x06002541 RID: 9537 RVA: 0x0015A704 File Offset: 0x00159704
	public static int ᜄ(Stream A_0)
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
		byte[] array = new byte[4];
		A_0.Read(array, 0, 4);
		return BitConverter.ToInt32(array, 0);
	}

	// Token: 0x06002542 RID: 9538 RVA: 0x0015A758 File Offset: 0x00159758
	public new static uint ᜃ(Stream A_0)
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
		byte[] array = new byte[4];
		A_0.Read(array, 0, 4);
		return BitConverter.ToUInt32(array, 0);
	}

	// Token: 0x06002543 RID: 9539 RVA: 0x0015A7AC File Offset: 0x001597AC
	public static short ᜂ(Stream A_0)
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
		byte[] array = new byte[2];
		A_0.Read(array, 0, 2);
		return BitConverter.ToInt16(array, 0);
	}

	// Token: 0x06002544 RID: 9540 RVA: 0x0015A800 File Offset: 0x00159800
	public static ushort ᜁ(Stream A_0)
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
		byte[] array = new byte[2];
		A_0.Read(array, 0, 2);
		return BitConverter.ToUInt16(array, 0);
	}

	// Token: 0x06002545 RID: 9541 RVA: 0x0015A854 File Offset: 0x00159854
	internal void ᜅ(Stream A_0)
	{
		int a_ = 16;
		switch (0)
		{
		default:
			if (true)
			{
			}
			if (A_0 != null)
			{
				try
				{
					int num = 8;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_146;
						case 1:
							if (this.m_iLength < this.MinimumRecordSize)
							{
								num = 5;
								continue;
							}
							num = 4;
							continue;
						case 2:
							if (A_0.Length - A_0.Position - (long)this.m_iLength < 0L)
							{
								num = 10;
								continue;
							}
							this.ᜀ(A_0);
							num = 9;
							continue;
						case 3:
							if (this.ᜅ == 0)
							{
								num = 6;
								continue;
							}
							this.m_iLength = spr\u1D3B.ᜄ(A_0);
							num = 1;
							continue;
						case 4:
							if (this.m_iLength > this.MaximumRecordSize)
							{
								num = 0;
								continue;
							}
							num = 2;
							continue;
						case 5:
							goto IL_18E;
						case 6:
							goto IL_118;
						case 7:
							goto IL_A8;
						case 8:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_DE;
							default:
								if (false)
								{
								}
								break;
							}
							break;
						case 9:
							goto IL_2C8;
						case 10:
							goto IL_DE;
						}
						if (A_0.Length - A_0.Position - 8L < 0L)
						{
							num = 7;
						}
						else
						{
							this.ᜄ = spr\u1D3B.ᜁ(A_0);
							this.ᜅ = spr\u1D3B.ᜁ(A_0);
							num = 3;
						}
					}
					IL_A8:
					throw new ApplicationException(RecordTableEnumerator.b("ፅ♇⽉㑋㹍㕏ㅑ⁓㍕㱗穙㥛そџ䉡ୣe䡧ᡩ५൭Ὧqၳ噵啷婹๻᭽ꪉ늑ﮓ뢗ﮝ肟쎡횣풥즧펩芫", a_));
					IL_DE:
					throw new ApplicationException(RecordTableEnumerator.b("ፅ♇⽉㑋㹍㕏ㅑ⁓㍕㱗穙㥛そџ䉡ୣe䡧ᡩ५൭Ὧqၳյ塷ॹࡻ౽ꢅꢇ\ud889ﾏ뚕ﲗﮙﾝ肟송얣좥욧얩\ud8ab躭튯ힱ钳쒵\uddb7\udbb9\ud8bb麽뛃ꏅ꧇꧉꓋ꯍ듏뇓룕볗龎돛룝샟釡郣铥跧诩臫샭", a_));
					IL_118:
					throw new ApplicationException(RecordTableEnumerator.b("୅㭇╉汋ᱍ㕏ㅑ㭓⑕㱗穙㕛㩝՟ౡၣཥ๧ͩཫ཭ѯ᭱᭳ᡵ塷᥹፻᩽ꊁꢇﶉﺋﺏ뒓뺕ﾙ覟財", a_));
					IL_146:
					throw new spr\u2598(string.Concat(new object[]
					{
						RecordTableEnumerator.b("Յ❇⹉⥋湍橏", a_),
						((MsoRecords)this.m_iCode).ToString(),
						this.m_iCode,
						RecordTableEnumerator.b("䱅桇ᡉ⥋⽍㱏牑❓㽕≗㽙晛繝", a_),
						this.m_iLength,
						RecordTableEnumerator.b("桅桇ཉ㑋㹍㕏ㅑ⁓㍕㱗穙⽛㝝᩟ݡ幣䙥", a_),
						this.MaximumRecordSize.ToString()
					}));
					IL_18E:
					throw new spr\u1AEA(string.Concat(new object[]
					{
						RecordTableEnumerator.b("Յ❇⹉⥋湍橏", a_),
						this.m_iCode.ToString(),
						RecordTableEnumerator.b("䱅桇ᡉ⥋⽍㱏牑❓㽕≗㽙晛繝", a_),
						this.m_iLength,
						RecordTableEnumerator.b("桅桇ཉ㑋㹍㕏ㅑ⁓㍕㱗穙⽛㝝᩟ݡ幣䙥", a_),
						this.MaximumRecordSize.ToString()
					}));
					IL_2C8:
					return;
				}
				catch (ApplicationException ex)
				{
					Exception innerException = ex.InnerException;
					throw;
				}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㕅㱇㡉⥋⽍㵏", a_));
		}
	}

	// Token: 0x04001298 RID: 4760
	private new const ushort ᜀ = 15;

	// Token: 0x04001299 RID: 4761
	private new const ushort ᜁ = 65520;

	// Token: 0x0400129A RID: 4762
	private new const ushort ᜂ = 4;

	// Token: 0x0400129B RID: 4763
	private new const int ᜃ = 2147483647;

	// Token: 0x0400129C RID: 4764
	protected new ushort ᜄ;

	// Token: 0x0400129D RID: 4765
	private new ushort ᜅ;

	// Token: 0x0400129E RID: 4766
	private new spr\u24C9 ᜆ;

	// Token: 0x0400129F RID: 4767
	private spr\u1D3B ᜇ;

	// Token: 0x040012A0 RID: 4768
	private static Dictionary<Type, int> ᜈ;
}
