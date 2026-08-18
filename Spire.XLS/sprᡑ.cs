using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;

// Token: 0x020004D5 RID: 1237
[CLSCompliant(false)]
internal class sprᡑ : IEnumerator
{
	// Token: 0x06004BF1 RID: 19441 RVA: 0x002E8558 File Offset: 0x002E7558
	private sprᡑ()
	{
		this.ᜅ = new List<int>(sprᡑ.ᜀ);
		this.ᜆ = new byte[4096];
		base..ctor();
	}

	// Token: 0x06004BF2 RID: 19442 RVA: 0x002E858C File Offset: 0x002E758C
	public sprᡑ(BinaryReader A_0, IDecryptor A_1, DataProvider A_2)
	{
		int a_ = 5;
		this.ᜅ = new List<int>(sprᡑ.ᜀ);
		this.ᜆ = new byte[4096];
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
		}
		this.ᜁ = A_0;
		this.ᜂ = A_0.BaseStream.Position;
		this.ᜇ = A_2;
		this.ᜈ = A_1;
	}

	// Token: 0x06004BF3 RID: 19443 RVA: 0x002E8608 File Offset: 0x002E7608
	protected bool ᜄ()
	{
		int a_ = 3;
		if (this.ᜁ == null)
		{
			if (true)
			{
			}
		}
		else
		{
			try
			{
				int num = 0;
				bool result;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_D7;
					case 2:
						if (this.ᜆ() == null)
						{
							num = 3;
							continue;
						}
						result = false;
						num = 1;
						continue;
					case 3:
						result = true;
						num = 6;
						continue;
					case 4:
						result = true;
						num = 5;
						continue;
					case 5:
						goto IL_A8;
					case 6:
						goto IL_80;
					}
					if (this.ᜁ.BaseStream.Position == this.ᜁ.BaseStream.Length)
					{
						num = 4;
					}
					else
					{
						num = 2;
					}
				}
				IL_80:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_A8:
					break;
				default:
					if (false)
					{
					}
					break;
				}
				IL_D7:
				return result;
			}
			catch (Exception)
			{
				return true;
			}
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("吸携䤼刾ㅀᅂ⁄♆ⵈ⹊㽌", a_));
	}

	// Token: 0x06004BF4 RID: 19444 RVA: 0x002E8724 File Offset: 0x002E7724
	protected BiffRecordRaw ᜆ()
	{
		int a_ = 19;
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
			if (this.ᜁ == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("⑈ᑊ㥌≎⅐Œご㙖㵘㹚⽜", a_));
			}
			break;
		}
		long position = this.ᜁ.BaseStream.Position;
		BiffRecordRaw result = spr\u175E.ᜁ(this.ᜁ);
		this.ᜁ.BaseStream.Position = position;
		return result;
	}

	// Token: 0x06004BF5 RID: 19445 RVA: 0x002E87B8 File Offset: 0x002E77B8
	public BiffRecordRaw ᜀ()
	{
		int a_ = 14;
		int num = 4;
		for (;;)
		{
			IL_13:
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				num = 1;
				continue;
			case 1:
				if (!this.ᜄ)
				{
					num = 5;
					continue;
				}
				goto IL_CD;
			case 2:
				if (this.ᜃ != null)
				{
					num = 0;
					continue;
				}
				goto IL_B9;
			case 3:
				goto IL_5D;
			case 5:
				goto IL_77;
			}
			while (this.ᜁ == null)
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
					num = 3;
					goto IL_13;
				}
			}
			num = 2;
		}
		IL_5D:
		throw new ArgumentNullException(RecordTableEnumerator.b("⥃᥅㱇❉㱋ᱍ㕏㍑こ㍕⩗", a_));
		IL_77:
		IL_B9:
		throw new ArgumentException(RecordTableEnumerator.b("Ƀ⽅㩇㥉㡋湍㍏㍑㡓㩕硗࡙㥛ⵝ՟ᙡ䑣୥൧ṩѫŭᑯ剱ᕳᡵᱷ婹ࡻᙽꒃ쮅ﲉ삍뢕뢗춙캟얡蒣쎥욧\udfa9솫쮭슯펱삳\ud9b5쪷骹햻킽ꦿ뛁귃Ʂ꓇ꏉ뛋꿍꓏믑믓룕", a_));
		IL_CD:
		return this.ᜃ;
	}

	// Token: 0x06004BF6 RID: 19446 RVA: 0x002E8898 File Offset: 0x002E7898
	public long ᜂ()
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
		this.ᜂ = this.ᜁ.BaseStream.Position;
		this.ᜃ = null;
		this.ᜄ = false;
		return this.ᜂ;
	}

	// Token: 0x06004BF7 RID: 19447 RVA: 0x002E8900 File Offset: 0x002E7900
	public void ᜀ(TBIFFRecord A_0)
	{
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				IL_02:
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					this.ᜅ.Add((int)A_0);
					num = 2;
					continue;
				case 1:
					while (this.ᜅ.IndexOf((int)A_0) == -1)
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
							num = 0;
							goto IL_02;
						}
					}
					return;
				case 2:
					return;
				}
				break;
			}
		}
	}

	// Token: 0x06004BF8 RID: 19448 RVA: 0x002E8988 File Offset: 0x002E7988
	void IEnumerator.ᜁ()
	{
		int a_ = 11;
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
			if (this.ᜁ == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("ⱀ᱂ㅄ⩆㥈᥊⡌⹎㕐㙒❔", a_));
			}
			break;
		}
		this.ᜁ.BaseStream.Position = this.ᜂ;
		this.ᜄ = true;
	}

	// Token: 0x06004BF9 RID: 19449 RVA: 0x002E8A08 File Offset: 0x002E7A08
	object IEnumerator.ᜅ()
	{
		int a_ = 15;
		int num = 1;
		for (;;)
		{
			IL_13:
			switch (num)
			{
			case 0:
				goto IL_7F;
			case 2:
				if (!this.ᜄ)
				{
					num = 0;
					continue;
				}
				goto IL_D0;
			case 3:
				if (this.ᜃ != null)
				{
					num = 4;
					continue;
				}
				goto IL_BC;
			case 4:
				num = 2;
				continue;
			case 5:
				goto IL_65;
			}
			while (this.ᜁ == null)
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
					num = 5;
					goto IL_13;
				}
			}
			num = 3;
		}
		IL_65:
		throw new ArgumentNullException(RecordTableEnumerator.b("⡄ᡆ㵈♊㵌ᵎ㑐㉒ㅔ㉖⭘", a_));
		IL_7F:
		IL_BC:
		throw new ArgumentException(RecordTableEnumerator.b("̈́⹆㭈㡊㥌潎㉐㉒㥔㭖祘ग़㡜ⱞѠᝢ䕤੦౨Ὢլnᕰ卲ᑴ᥶ᵸ孺ॼ᝾ꖄ쪆ﶊ솎릖릘첚쾠쒢薤슦잨\udeaa사쪮쎰튲솴\ud8b6쮸鮺풼톾ꣀ럂계ꛆꗈꋊ럌껎ꗐ뫒뫔맖", a_));
		IL_D0:
		return this.ᜃ;
	}

	// Token: 0x06004BFA RID: 19450 RVA: 0x002E8AEC File Offset: 0x002E7AEC
	bool IEnumerator.ᜃ()
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 1;
			byte[] array;
			for (;;)
			{
				int num3;
				switch (num)
				{
				case 0:
				{
					int num2;
					if (this.ᜅ.IndexOf(num2) != -1)
					{
						num = 6;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6A;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						this.ᜁ.BaseStream.Position -= 4L;
						num = 9;
						continue;
					}
					break;
				}
				case 2:
				{
					int num2 = (int)this.ᜁ.ReadInt16();
					num3 = (int)this.ᜁ.ReadInt16();
					num = 0;
					continue;
				}
				case 3:
					goto IL_65;
				case 4:
					if (this.ᜈ != null)
					{
						num = 5;
						continue;
					}
					goto IL_17B;
				case 5:
					goto IL_6A;
				case 6:
				{
					int num2;
					this.ᜃ = spr\u175E.ᜀ(num2);
					this.ᜃ.Length = num3;
					array = this.ᜁ.ReadBytes(num3);
					num = 4;
					continue;
				}
				case 7:
					if (!this.ᜄ())
					{
						num = 2;
						continue;
					}
					goto IL_1D7;
				case 8:
					goto IL_9B;
				case 9:
					goto IL_165;
				}
				if (this.ᜁ == null)
				{
					num = 3;
					continue;
				}
				num = 7;
				continue;
				IL_6A:
				spr\u24E5 provider = new spr\u24E5(array);
				this.ᜈ.Decrypt(provider, 0, num3, this.ᜁ.BaseStream.Position - (long)num3);
				num = 8;
			}
			IL_65:
			throw new ArgumentNullException(RecordTableEnumerator.b("嘺戼䬾ⱀ㍂ᝄ≆⡈⽊⡌㵎", a_));
			IL_9B:
			goto IL_17B;
			IL_165:
			goto IL_1D7;
			IL_17B:
			this.ᜃ.Data = array;
			this.ᜄ = true;
			return true;
			IL_1D7:
			this.ᜃ = null;
			this.ᜄ = false;
			return false;
		}
		}
	}

	// Token: 0x06004BFB RID: 19451 RVA: 0x002E8CE0 File Offset: 0x002E7CE0
	// Note: this type is marked as 'beforefieldinit'.
	static sprᡑ()
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
		sprᡑ.ᜀ = new int[]
		{
			60
		};
	}

	// Token: 0x0400228A RID: 8842
	private static readonly int[] ᜀ;

	// Token: 0x0400228B RID: 8843
	private BinaryReader ᜁ;

	// Token: 0x0400228C RID: 8844
	private long ᜂ;

	// Token: 0x0400228D RID: 8845
	private BiffRecordRaw ᜃ;

	// Token: 0x0400228E RID: 8846
	private bool ᜄ;

	// Token: 0x0400228F RID: 8847
	private List<int> ᜅ;

	// Token: 0x04002290 RID: 8848
	private byte[] ᜆ;

	// Token: 0x04002291 RID: 8849
	private DataProvider ᜇ;

	// Token: 0x04002292 RID: 8850
	private IDecryptor ᜈ;
}
