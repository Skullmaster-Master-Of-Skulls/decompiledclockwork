using System;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;

// Token: 0x0200038D RID: 909
[CLSCompliant(false)]
internal class sprἛ : IDisposable
{
	// Token: 0x0600377A RID: 14202 RVA: 0x001F3058 File Offset: 0x001F2058
	public Stream ᜈ()
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

	// Token: 0x0600377B RID: 14203 RVA: 0x001F309C File Offset: 0x001F209C
	public BinaryReader ᜄ()
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

	// Token: 0x0600377C RID: 14204 RVA: 0x001F30E0 File Offset: 0x001F20E0
	public int ᜆ()
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
		return this.ᜆ;
	}

	// Token: 0x0600377D RID: 14205 RVA: 0x001F3124 File Offset: 0x001F2124
	public void ᜀ(int A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x0600377E RID: 14206 RVA: 0x001F3168 File Offset: 0x001F2168
	public byte[] ᜇ()
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

	// Token: 0x0600377F RID: 14207 RVA: 0x001F31AC File Offset: 0x001F21AC
	public DataProvider ᜋ()
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

	// Token: 0x06003780 RID: 14208 RVA: 0x001F31F0 File Offset: 0x001F21F0
	private sprἛ()
	{
		this.ᜆ = 1536;
		this.ᜇ = new byte[8228];
		base..ctor();
		this.ᜈ = new spr\u24E5(this.ᜇ);
	}

	// Token: 0x06003781 RID: 14209 RVA: 0x001F3230 File Offset: 0x001F2230
	public sprἛ(Stream A_0)
	{
		int a_ = 10;
		this..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("㌿㙁㙃⍅⥇❉", a_));
		}
		this.ᜂ = A_0;
		this.ᜃ = new BinaryReader(this.ᜂ);
	}

	// Token: 0x06003782 RID: 14210 RVA: 0x001F3280 File Offset: 0x001F2280
	public sprἛ(Stream A_0, bool A_1) : this(A_0)
	{
		this.ᜅ = A_1;
	}

	// Token: 0x06003783 RID: 14211 RVA: 0x001F329C File Offset: 0x001F229C
	public void ᜅ()
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_45;
			case 1:
				if (this.ᜈ != null)
				{
					num = 3;
					continue;
				}
				return;
			case 2:
				if (this.ᜅ)
				{
					num = 6;
					continue;
				}
				goto IL_45;
			case 3:
				this.ᜈ.Dispose();
				this.ᜈ = null;
				num = 4;
				continue;
			case 4:
				return;
			case 6:
				((IDisposable)this.ᜃ).Dispose();
				((IDisposable)this.ᜂ).Dispose();
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_DE;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			case 7:
				return;
			}
			if (this.ᜄ)
			{
				num = 7;
				continue;
			}
			goto IL_DE;
			IL_45:
			this.ᜂ = null;
			this.ᜃ = null;
			this.ᜇ = null;
			if (true)
			{
			}
			num = 1;
			continue;
			IL_DE:
			this.ᜄ = true;
			num = 2;
		}
	}

	// Token: 0x06003784 RID: 14212 RVA: 0x001F33C0 File Offset: 0x001F23C0
	public bool ᜂ()
	{
		int a_ = 10;
		if (this.ᜂ != null)
		{
			goto IL_20;
			try
			{
				bool result;
				for (;;)
				{
					IL_20:
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_6C;
						case 2:
							result = true;
							num = 0;
							continue;
						case 3:
							goto IL_BF;
						}
						if (this.ᜂ.Position == this.ᜂ.Length)
						{
							num = 2;
						}
						else
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_20;
							default:
							{
								if (false)
								{
								}
								int num2 = (int)this.ᜃ.ReadInt16();
								this.ᜃ.BaseStream.Position -= 2L;
								result = (num2 == 0);
								num = 3;
								break;
							}
							}
						}
					}
				}
				IL_6C:
				IL_BF:
				return result;
			}
			catch (Exception)
			{
				return true;
			}
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("⤿ⱁぃ⍅㩇⑉ⵋ≍灏⅑⁓⑕㵗㭙ㅛ", a_));
	}

	// Token: 0x06003785 RID: 14213 RVA: 0x001F34C0 File Offset: 0x001F24C0
	public BiffRecordRaw ᜃ()
	{
		int a_ = 11;
		if (this.ᜂ != null)
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
				return spr\u175E.ᜀ(this.ᜃ, this.ᜈ, this.ᜇ);
			}
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("⡀ⵂㅄ≆㭈╊ⱌ⍎煐⁒⅔╖㱘㩚ぜ", a_));
	}

	// Token: 0x06003786 RID: 14214 RVA: 0x001F353C File Offset: 0x001F253C
	public BiffRecordRaw ᜀ(IDecryptor A_0)
	{
		int a_ = 8;
		if (true)
		{
		}
		if (this.ᜂ != null)
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
				return spr\u175E.ᜀ(this.ᜃ, this.ᜈ, A_0, this.ᜇ);
			}
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("圽⸿㙁⅃㑅♇⭉⁋湍⍏♑♓㍕㥗㝙", a_));
	}

	// Token: 0x06003787 RID: 14215 RVA: 0x001F35B8 File Offset: 0x001F25B8
	public BiffRecordRaw ᜊ()
	{
		int a_ = 2;
		if (this.ᜂ != null)
		{
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
				long position = this.ᜂ.Position;
				BiffRecordRaw result = this.ᜃ();
				this.ᜂ.Position = position;
				return result;
			}
			}
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("儷吹䠻嬽㈿ⱁ╃⩅桇㥉㡋㱍㕏㍑㥓", a_));
	}

	// Token: 0x06003788 RID: 14216 RVA: 0x001F363C File Offset: 0x001F263C
	public TBIFFRecord ᜉ()
	{
		int a_ = 0;
		if (this.ᜂ == null)
		{
			if (true)
			{
			}
		}
		else
		{
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
				long position = this.ᜂ.Position;
				TBIFFRecord result = (TBIFFRecord)spr\u175E.ᜀ(this.ᜃ);
				this.ᜂ.Position = position;
				return result;
			}
			}
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("張嘷丹夻䰽⸿⍁⡃晅㭇㹉㹋⭍ㅏ㽑", a_));
	}

	// Token: 0x06003789 RID: 14217 RVA: 0x001F36C4 File Offset: 0x001F26C4
	public BiffRecordRaw ᜁ()
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int num = 13;
			int num2;
			for (;;)
			{
				BiffRecordRaw biffRecordRaw;
				TBIFFRecord tbiffrecord;
				switch (num)
				{
				case 0:
					return biffRecordRaw;
				case 1:
					goto IL_27E;
				case 2:
					if (this.ᜂ.Position < this.ᜂ.Length)
					{
						goto IL_1A6;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1B2;
					default:
						if (false)
						{
						}
						num = 11;
						continue;
					}
					break;
				case 3:
					goto IL_1A6;
				case 4:
					num = 10;
					continue;
				case 5:
					if (num2 < this.ᜆ())
					{
						num = 1;
						continue;
					}
					this.ᜂ.Position -= 6L;
					biffRecordRaw = spr\u175E.ᜀ(this.ᜃ, this.ᜈ, this.ᜇ);
					num = 9;
					continue;
				case 6:
					goto IL_1B2;
				case 7:
					goto IL_7F;
				case 8:
					goto IL_22B;
				case 9:
					goto IL_84;
				case 10:
					if (tbiffrecord == TBIFFRecord.BOF2)
					{
						num = 8;
						continue;
					}
					goto IL_84;
				case 11:
					goto IL_D0;
				case 12:
					if (tbiffrecord != TBIFFRecord.BOF)
					{
						num = 4;
						continue;
					}
					goto IL_22B;
				}
				if (this.ᜂ == null)
				{
					num = 7;
					continue;
				}
				biffRecordRaw = null;
				long position = this.ᜂ.Position;
				byte[] array = new byte[2];
				num = 3;
				continue;
				IL_84:
				num = 2;
				continue;
				IL_1A6:
				num = 6;
				continue;
				IL_1B2:
				if (biffRecordRaw != null)
				{
					num = 0;
					continue;
				}
				this.ᜂ.Read(array, 0, 2);
				int num3 = (int)array[0] + ((int)array[1] << 8);
				tbiffrecord = (TBIFFRecord)num3;
				num = 12;
				continue;
				IL_22B:
				this.ᜂ.Position += 2L;
				this.ᜂ.Read(array, 0, 2);
				num2 = (int)array[0] + ((int)array[1] << 8);
				num = 5;
			}
			IL_7F:
			throw new ArgumentNullException(RecordTableEnumerator.b("⹆❈㽊⡌㵎㽐㉒㥔睖⩘⽚⽜㩞`๢", a_));
			IL_D0:
			if (true)
			{
			}
			return null;
			IL_27E:
			throw new FormatException(string.Concat(new object[]
			{
				RecordTableEnumerator.b("Ն⡈⽊浌⥎㡐㽒ご睖⽘㹚⽜ⱞࡠౢ୤䥦䥨⹪ᕬὮᑰၲŴቶᵸ孺୼᩾ꮊﲎ", a_),
				this.ᜆ(),
				RecordTableEnumerator.b("杆㽈⹊㽌㱎㡐㱒㭔睖㽘㑚⡜ㅞՠ䍢", a_),
				num2
			}));
		}
		}
	}

	// Token: 0x0600378A RID: 14218 RVA: 0x001F3958 File Offset: 0x001F2958
	public BiffRecordRaw ᜀ(TBIFFRecord A_0)
	{
		int a_ = 2;
		int num = 7;
		for (;;)
		{
			BiffRecordRaw biffRecordRaw;
			switch (num)
			{
			case 0:
				goto IL_56;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7E;
				default:
				{
					if (false)
					{
					}
					if (biffRecordRaw != null)
					{
						num = 2;
						continue;
					}
					int num2 = (this.ᜂ.ReadByte() & 255) + ((this.ᜂ.ReadByte() & 255) << 8);
					num = 3;
					continue;
				}
				}
				break;
			case 2:
				return biffRecordRaw;
			case 3:
			{
				int num2;
				if (num2 == (int)A_0)
				{
					num = 4;
					continue;
				}
				goto IL_56;
			}
			case 4:
				biffRecordRaw = spr\u175E.ᜀ(this.ᜃ, this.ᜈ, this.ᜇ);
				num = 0;
				continue;
			case 5:
				if (this.ᜂ.Position >= this.ᜂ.Length)
				{
					num = 9;
					continue;
				}
				goto IL_D5;
			case 6:
				goto IL_D5;
			case 8:
				goto IL_51;
			case 9:
				goto IL_7E;
			}
			if (this.ᜂ == null)
			{
				num = 8;
				continue;
			}
			biffRecordRaw = null;
			long position = this.ᜂ.Position;
			if (true)
			{
			}
			num = 6;
			continue;
			IL_56:
			num = 5;
			continue;
			IL_D5:
			num = 1;
		}
		IL_51:
		throw new ArgumentNullException(RecordTableEnumerator.b("儷吹䠻嬽㈿ⱁ╃⩅桇㥉㡋㱍㕏㍑㥓", a_));
		IL_7E:
		return null;
	}

	// Token: 0x0600378B RID: 14219 RVA: 0x001F3AD0 File Offset: 0x001F2AD0
	protected BiffRecordRaw ᜀ()
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 2;
			BiffRecordRaw result;
			Stream baseStream;
			long position;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_BC;
				case 1:
					goto IL_60;
				case 3:
					result = sprᱬ.ᜀ();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BC;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 4:
					goto IL_8D;
				}
				if (this.ᜂ == null)
				{
					num = 1;
					continue;
				}
				baseStream = this.ᜃ.BaseStream;
				position = baseStream.Position;
				result = null;
				int num2 = (int)this.ᜃ.ReadInt16();
				num = 0;
				continue;
				IL_BC:
				if (num2 <= 0)
				{
					goto IL_E2;
				}
				num = 3;
			}
			IL_60:
			throw new ArgumentNullException(RecordTableEnumerator.b("吼儾㕀♂㝄⥆⡈❊浌㱎═⅒ご㙖㑘", a_));
			IL_8D:
			IL_E2:
			baseStream.Position = position;
			return result;
		}
		}
	}

	// Token: 0x0400186F RID: 6255
	private const int ᜀ = 262144;

	// Token: 0x04001870 RID: 6256
	private const int ᜁ = 1536;

	// Token: 0x04001871 RID: 6257
	private Stream ᜂ;

	// Token: 0x04001872 RID: 6258
	private BinaryReader ᜃ;

	// Token: 0x04001873 RID: 6259
	private bool ᜄ;

	// Token: 0x04001874 RID: 6260
	private bool ᜅ;

	// Token: 0x04001875 RID: 6261
	private int ᜆ;

	// Token: 0x04001876 RID: 6262
	private byte[] ᜇ;

	// Token: 0x04001877 RID: 6263
	private DataProvider ᜈ;
}
