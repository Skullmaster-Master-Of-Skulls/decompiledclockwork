using System;
using System.IO;
using System.Text;
using Spire.Xls;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200031F RID: 799
[spr\u2593(TBIFFRecord.Style)]
[CLSCompliant(false)]
internal class sprᬐ : BiffRecordRaw, INamedObject
{
	// Token: 0x06003150 RID: 12624 RVA: 0x001C8FFC File Offset: 0x001C7FFC
	public bool ᜄ()
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

	// Token: 0x06003151 RID: 12625 RVA: 0x001C9040 File Offset: 0x001C8040
	public void ᜀ(bool A_0)
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

	// Token: 0x06003152 RID: 12626 RVA: 0x001C9084 File Offset: 0x001C8084
	public ushort ᜅ()
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
		return this.ᜁ & 4095;
	}

	// Token: 0x06003153 RID: 12627 RVA: 0x001C90CC File Offset: 0x001C80CC
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
		this.ᜁ = A_0;
	}

	// Token: 0x06003154 RID: 12628 RVA: 0x001C9110 File Offset: 0x001C8110
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
		return this.ᜃ;
	}

	// Token: 0x06003155 RID: 12629 RVA: 0x001C9154 File Offset: 0x001C8154
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
		this.ᜃ = A_0;
	}

	// Token: 0x06003156 RID: 12630 RVA: 0x001C9198 File Offset: 0x001C8198
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
		return this.ᜄ;
	}

	// Token: 0x06003157 RID: 12631 RVA: 0x001C91DC File Offset: 0x001C81DC
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
		this.ᜄ = A_0;
	}

	// Token: 0x06003158 RID: 12632 RVA: 0x001C9220 File Offset: 0x001C8220
	public string ᜆ()
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

	// Token: 0x06003159 RID: 12633 RVA: 0x001C9264 File Offset: 0x001C8264
	public void ᜀ(string A_0)
	{
		int a_ = 10;
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
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_84;
				case 1:
					if (A_0.Length == 0)
					{
						num = 3;
						continue;
					}
					num = 4;
					continue;
				case 2:
					goto IL_62;
				case 3:
					goto IL_F9;
				case 4:
					if (A_0.Length > 255)
					{
						num = 0;
						continue;
					}
					goto IL_FB;
				}
				if (A_0 == null)
				{
					num = 2;
				}
				else
				{
					if (true)
					{
					}
					num = 1;
				}
			}
			IL_62:
			throw new ArgumentNullException(RecordTableEnumerator.b("ጿ㙁㵃⩅ⵇщⵋ⍍㕏", a_));
			IL_84:
			break;
			IL_F9:
			throw new ArgumentException(RecordTableEnumerator.b("ጿ㙁㵃⩅ⵇщⵋ⍍㕏牑祓癕⭗⹙⹛㝝๟ա䑣ե१ѩɫŭѯ剱ᙳ፵塷όᅻ๽ﮁꪃ", a_));
			IL_FB:
			this.ᜅ = A_0;
			this.ᜂ = false;
			this.ᜃ = checked((byte)this.ᜅ.Length);
			return;
		}
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ጿ㙁㵃⩅ⵇщⵋ⍍㕏", a_), RecordTableEnumerator.b("ጿ㙁㵃⩅ⵇ橉≋⽍㵏㝑瑓㕕㥗㑙㉛ㅝᑟ䉡٣ͥ䡧٩൫ᱭᝯ᝱ٳ噵䩷佹䥻幽ﮁﾋꂍ", a_));
	}

	// Token: 0x0600315A RID: 12634 RVA: 0x001C938C File Offset: 0x001C838C
	public virtual int ᜃ()
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
		return 4;
	}

	// Token: 0x0600315B RID: 12635 RVA: 0x001C93C8 File Offset: 0x001C83C8
	public string ᜂ()
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

	// Token: 0x0600315C RID: 12636 RVA: 0x001C940C File Offset: 0x001C840C
	public sprᬐ()
	{
	}

	// Token: 0x0600315D RID: 12637 RVA: 0x001C9434 File Offset: 0x001C8434
	public sprᬐ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600315E RID: 12638 RVA: 0x001C945C File Offset: 0x001C845C
	public sprᬐ(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600315F RID: 12639 RVA: 0x001C9484 File Offset: 0x001C8484
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		for (;;)
		{
			this.m_iLength = A_2;
			this.ᜁ = A_0.ReadUInt16(A_1);
			this.ᜂ = A_0.ReadBit(A_1 + 1, 7);
			this.ᜃ = A_0.ReadByte(A_1 + 2);
			this.ᜁ &= 4095;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_D6;
				case 1:
					if (A_2 <= 4)
					{
						goto IL_8A;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_70;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 2:
					goto IL_70;
				case 3:
					num = 1;
					continue;
				}
				break;
				IL_70:
				if (true)
				{
				}
				if (!this.ᜂ)
				{
					goto IL_D8;
				}
				num = 3;
			}
		}
		IL_8A:
		this.ᜄ = A_0.ReadByte(A_1 + 3);
		return;
		IL_D6:
		throw new spr\u2598();
		IL_D8:
		A_0.ReadByte(A_1 + 4);
		int iStrLen = (int)this.ᜃ;
		int num2;
		this.ᜅ = A_0.ReadString(A_1 + 4, iStrLen, out num2, false);
	}

	// Token: 0x06003160 RID: 12640 RVA: 0x001C9590 File Offset: 0x001C8590
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜂ)
				{
					num = 1;
					continue;
				}
				goto IL_DA;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_37;
				default:
					goto IL_C9;
				}
				break;
			case 3:
				goto IL_47;
			}
			goto IL_2A;
			IL_37:
			if (true)
			{
			}
			num = 3;
			continue;
			IL_2A:
			if (this.ᜁ > 4095)
			{
				goto IL_37;
			}
			this.m_iLength = this.GetStoreSize(A_2);
			A_0.WriteUInt16(A_1, this.ᜁ);
			A_0.WriteBit(A_1 + 1, this.ᜂ, 7);
			A_0.WriteByte(A_1 + 2, this.ᜃ);
			num = 0;
		}
		IL_47:
		throw new ArgumentOutOfRangeException();
		IL_C9:
		if (false)
		{
		}
		A_0.WriteByte(A_1 + 3, this.ᜄ);
		return;
		IL_DA:
		A_0.WriteByte(A_1 + 2, (byte)this.ᜅ.Length);
		A_0.WriteByte(A_1 + 3, 0);
		byte[] bytes = Encoding.Unicode.GetBytes(this.ᜅ);
		A_0.WriteByte(A_1 + 4, 1);
		A_0.WriteBytes(A_1 + 5, bytes, 0, bytes.Length);
	}

	// Token: 0x06003161 RID: 12641 RVA: 0x001C96C0 File Offset: 0x001C86C0
	public virtual int ᜀ(ExcelVersion A_0)
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
			if (true)
			{
			}
			if (this.ᜂ)
			{
				return 4;
			}
			break;
		}
		int byteCount = Encoding.Unicode.GetByteCount(this.ᜅ);
		return 5 + byteCount;
	}

	// Token: 0x06003162 RID: 12642 RVA: 0x001C971C File Offset: 0x001C871C
	public virtual void ᜀ(BiffRecordRaw A_0)
	{
		int a_ = 7;
		if (true)
		{
		}
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_57;
		}
		if (false)
		{
		}
		sprᬐ sprᬐ = A_0 as sprᬐ;
		if (sprᬐ == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("樼䴾⹀ⵂ≄杆ୈ≊⭌⥎͐㙒㙔㡖⭘㽚絜⭞ᡠ።d", a_));
		}
		IL_57:
		sprᬐ.ᜁ = this.ᜁ;
		sprᬐ.ᜂ = this.ᜂ;
		sprᬐ.ᜃ = this.ᜃ;
		sprᬐ.ᜄ = this.ᜄ;
		sprᬐ.ᜅ = this.ᜅ;
	}

	// Token: 0x040015BA RID: 5562
	private new const ushort ᜀ = 4095;

	// Token: 0x040015BB RID: 5563
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x040015BC RID: 5564
	[spr\u2429(1, 7, TFieldType.Bit)]
	private bool ᜂ = true;

	// Token: 0x040015BD RID: 5565
	[spr\u2429(2, 1)]
	private new byte ᜃ;

	// Token: 0x040015BE RID: 5566
	private byte ᜄ = byte.MaxValue;

	// Token: 0x040015BF RID: 5567
	private string ᜅ;
}
