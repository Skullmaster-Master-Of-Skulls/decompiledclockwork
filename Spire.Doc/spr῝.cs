using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text.RegularExpressions;
using Spire.CompoundFile.Doc;
using Spire.Compression;

// Token: 0x020002D7 RID: 727
[DefaultMember("Item")]
internal class spr\u1FDD : IDisposable
{
	// Token: 0x0600278B RID: 10123 RVA: 0x0027B34C File Offset: 0x0027A34C
	public sprℭ ᜀ(int A_0)
	{
		int a_ = 10;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_9A;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_35;
				default:
					if (false)
					{
					}
					if (A_0 > this.ᜁ.Count)
					{
						num = 0;
						continue;
					}
					goto IL_9C;
				}
				break;
			case 3:
				num = 1;
				continue;
			}
			goto IL_29;
			IL_35:
			num = 3;
			continue;
			IL_29:
			if (true)
			{
			}
			if (A_0 >= 0)
			{
				goto IL_35;
			}
			break;
		}
		IL_49:
		throw new ArgumentOutOfRangeException(ClipboardData.b("᥯ᱱၳ፵w", a_));
		IL_9A:
		goto IL_49;
		IL_9C:
		return this.ᜁ[A_0];
	}

	// Token: 0x0600278C RID: 10124 RVA: 0x0027B404 File Offset: 0x0027A404
	public sprℭ ᜃ(string A_0)
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
		sprℭ result;
		this.ᜂ.TryGetValue(A_0, out result);
		return result;
	}

	// Token: 0x0600278D RID: 10125 RVA: 0x0027B450 File Offset: 0x0027A450
	public int ᜈ()
	{
		if (this.ᜁ == null)
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
				break;
			}
			if (true)
			{
			}
			return 0;
		}
		return this.ᜁ.Count;
	}

	// Token: 0x0600278E RID: 10126 RVA: 0x0027B4A4 File Offset: 0x0027A4A4
	public sprᰣ ᜇ()
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

	// Token: 0x0600278F RID: 10127 RVA: 0x0027B4E8 File Offset: 0x0027A4E8
	public void ᜀ(sprᰣ A_0)
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

	// Token: 0x06002790 RID: 10128 RVA: 0x0027B52C File Offset: 0x0027A52C
	public CompressionLevel ᜂ()
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

	// Token: 0x06002791 RID: 10129 RVA: 0x0027B570 File Offset: 0x0027A570
	public void ᜀ(CompressionLevel A_0)
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

	// Token: 0x06002792 RID: 10130 RVA: 0x0027B5B4 File Offset: 0x0027A5B4
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
		return this.ᜄ;
	}

	// Token: 0x06002793 RID: 10131 RVA: 0x0027B5F8 File Offset: 0x0027A5F8
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
		this.ᜄ = A_0;
	}

	// Token: 0x06002794 RID: 10132 RVA: 0x0027B63C File Offset: 0x0027A63C
	public bool ᜆ()
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

	// Token: 0x06002795 RID: 10133 RVA: 0x0027B680 File Offset: 0x0027A680
	public void ᜁ(bool A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x06002796 RID: 10134 RVA: 0x0027B6C4 File Offset: 0x0027A6C4
	[CLSCompliant(false)]
	public static long ᜀ(Stream A_0, uint A_1, int A_2)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.CanSeek)
					{
						num = 15;
						continue;
					}
					goto IL_1D4;
				case 1:
				{
					long num2;
					long num3;
					if (num2 <= num3)
					{
						num = 10;
						continue;
					}
					uint num4;
					num4 <<= 8;
					num2 -= 1L;
					A_0.Position = num2;
					num4 += (uint)A_0.ReadByte();
					num = 2;
					continue;
				}
				case 2:
				{
					uint num4;
					if (num4 == A_1)
					{
						num = 13;
						continue;
					}
					goto IL_1E8;
				}
				case 4:
				{
					long length;
					if (length < 4L)
					{
						num = 8;
						continue;
					}
					byte[] array = new byte[4];
					long num3 = Math.Max(0L, length - (long)A_2);
					long num2 = length - 1L - 4L;
					A_0.Position = num2;
					A_0.Read(array, 0, 4);
					uint num4 = BitConverter.ToUInt32(array, 0);
					bool flag = num4 == A_1;
					num = 17;
					continue;
				}
				case 5:
					goto IL_1E8;
				case 6:
					goto IL_168;
				case 7:
				{
					bool flag;
					if (!flag)
					{
						num = 6;
						continue;
					}
					long num2;
					return num2;
				}
				case 8:
					goto IL_252;
				case 9:
					num = 5;
					continue;
				case 10:
					goto IL_12D;
				case 11:
					goto IL_12D;
				case 12:
					goto IL_80;
				case 13:
				{
					bool flag = true;
					num = 11;
					continue;
				}
				case 14:
				{
					if (true)
					{
					}
					if (!A_0.CanRead)
					{
						num = 16;
						continue;
					}
					long length = A_0.Length;
					num = 4;
					continue;
				}
				case 15:
					num = 14;
					continue;
				case 16:
					goto IL_128;
				case 17:
				{
					bool flag;
					if (!flag)
					{
						num = 9;
						continue;
					}
					goto IL_12D;
				}
				}
				if (A_0 == null)
				{
					num = 12;
					continue;
				}
				num = 0;
				continue;
				IL_12D:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_168;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				IL_1E8:
				num = 1;
			}
			IL_80:
			throw new ArgumentNullException(ClipboardData.b("űsѵᵷ᭹ᅻ", a_));
			IL_128:
			goto IL_1D4;
			IL_168:
			return -1L;
			IL_1D4:
			throw new ArgumentOutOfRangeException(ClipboardData.b("╱ᅳ噵ᙷό᥻᩽ꁿꚅ懲낏ﮙﺛ얟芡얣좥첧誩\udeab쮭톯횱햳풵풷\udfb9鲻춽뒿냁ꇃꟅꗇ", a_));
			IL_252:
			return -1L;
		}
		}
	}

	// Token: 0x06002797 RID: 10135 RVA: 0x0027B92C File Offset: 0x0027A92C
	public static int ᜅ(Stream A_0)
	{
		int a_ = 13;
		if (A_0.Read(spr\u1FDD.ᜀ, 0, 4) != 4)
		{
			if (true)
			{
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
				break;
			}
			throw new sprᥠ(ClipboardData.b("♲᭴ᙶ᭸᝺᡼彾ꖄ꾎璉ﲘ뮚ﲜ膠힢춤슦覨\ud8aa\uddac쪮튰\udab2펴\udeb6\udcb8\udfba鶼쾾껀냂계돆ꃈ꓊ꏌﳐ냔맖뷘ﯚ닜맞쇠郢釤闦賨諪胬쿮蛰鋲蛴ퟶ诸黺鳼鳾椀昂愄⤆", a_));
		}
		return BitConverter.ToInt32(spr\u1FDD.ᜀ, 0);
	}

	// Token: 0x06002798 RID: 10136 RVA: 0x0027B9A4 File Offset: 0x0027A9A4
	public static short ᜄ(Stream A_0)
	{
		int a_ = 3;
		if (A_0.Read(spr\u1FDD.ᜀ, 0, 2) != 2)
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
				break;
			}
			throw new sprᥠ(ClipboardData.b("㱨ժ౬൮ᵰᙲ啴Ͷᙸ孺ོ᩾ꖄ놐랖뾞튠펢삤쒦삨춪쒬쪮햰鎲어\ud8b6쪸튺즼횾껀귂껊ꏌꯎ볒돔꫘꿚꿜뫞胠転엤郦裨飪췬鷮铰鋲雴鿶鳸鿺폼", a_));
		}
		return BitConverter.ToInt16(spr\u1FDD.ᜀ, 0);
	}

	// Token: 0x06002799 RID: 10137 RVA: 0x0027BA1C File Offset: 0x0027AA1C
	public spr\u1FDD()
	{
		this.ᜇ = new spr\u1FDD.ᜀ(this.ᜃ);
	}

	// Token: 0x0600279A RID: 10138 RVA: 0x0027BA68 File Offset: 0x0027AA68
	private Stream ᜃ(Stream A_0)
	{
		if (this.ᜆ)
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
				break;
			}
			return new spr᭗(CompressionLevel.Best, A_0);
		}
		return new DeflateStream(A_0, CompressionMode.Compress, true);
	}

	// Token: 0x0600279B RID: 10139 RVA: 0x0027BAC0 File Offset: 0x0027AAC0
	public sprℭ ᜁ(string A_0)
	{
		int a_ = 16;
		int num = 0;
		FileAttributes attributes;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				break;
			case 1:
				goto IL_B0;
			case 2:
				goto IL_E8;
			case 3:
				num = 1;
				continue;
			case 4:
				if (this.ᜃ == null)
				{
					goto IL_EA;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B0;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 5:
				A_0 = this.ᜃ.ᜀ(A_0);
				num = 2;
				continue;
			case 6:
				goto IL_CD;
			}
			if (A_0 != null)
			{
				num = 3;
				continue;
			}
			break;
			IL_B0:
			if (A_0.Length == 0)
			{
				num = 6;
			}
			else
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(A_0);
				attributes = directoryInfo.Attributes;
				num = 4;
			}
		}
		IL_91:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ትᅷࡹ᥻ᵽﾅ욇", a_));
		IL_CD:
		goto IL_91;
		IL_E8:
		IL_EA:
		return this.ᜁ(A_0, null, false, attributes);
	}

	// Token: 0x0600279C RID: 10140 RVA: 0x0027BBC4 File Offset: 0x0027ABC4
	public sprℭ ᜅ(string A_0)
	{
		Stream a_;
		FileAttributes attributes;
		for (;;)
		{
			IL_30:
			a_ = new FileStream(A_0, FileMode.Open, FileAccess.Read);
			FileInfo fileInfo = new FileInfo(A_0);
			attributes = fileInfo.Attributes;
			int num = 1;
			for (;;)
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
					switch (num)
					{
					case 0:
						goto IL_6B;
					case 1:
						if (this.ᜃ != null)
						{
							num = 0;
							continue;
						}
						goto IL_8E;
					case 2:
						goto IL_8C;
					}
					goto IL_30;
				}
				IL_6B:
				A_0 = this.ᜃ.ᜀ(A_0);
				if (true)
				{
				}
				num = 2;
			}
		}
		IL_8C:
		IL_8E:
		return this.ᜁ(A_0, a_, true, attributes);
	}

	// Token: 0x0600279D RID: 10141 RVA: 0x0027BC6C File Offset: 0x0027AC6C
	public sprℭ ᜁ(string A_0, Stream A_1, bool A_2, FileAttributes A_3)
	{
		int a_ = 11;
		for (;;)
		{
			A_0 = A_0.Replace('\\', '/');
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!this.ᜂ.ContainsKey(A_0))
					{
						goto IL_EC;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_57;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 1:
					goto IL_57;
				case 2:
					if (A_0.IndexOf(':') != A_0.LastIndexOf(':'))
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					num = 0;
					continue;
				case 3:
					goto IL_C8;
				}
				break;
			}
		}
		IL_57:
		throw new ArgumentOutOfRangeException(ClipboardData.b("⭰ᩲմ㹶൸Ṻၼ彾ꦈﲔ練뮚춠욢스욦얨讪캬잮킰솲풴풶춸\udeba쾼첾", a_), ClipboardData.b("ᡰݲၴ᩶㝸᩺ၼ᩾", a_));
		IL_C8:
		throw new ArgumentOutOfRangeException(ClipboardData.b("㡰ݲၴ᩶奸", a_) + A_0 + ClipboardData.b("兰ቲᥴնᱸ᩺᥼پꆀﶄ愈ﾊﺌ꾎ﶒ떔ﺚ붜ﺞ펠삢춤캦\udfa8캪", a_));
		IL_EC:
		sprℭ sprℭ = new sprℭ(this, A_0, A_1, A_2, A_3);
		sprℭ.ᜀ(this.ᜅ);
		return this.ᜀ(sprℭ);
	}

	// Token: 0x0600279E RID: 10142 RVA: 0x0027BD84 File Offset: 0x0027AD84
	public sprℭ ᜀ(sprℭ A_0)
	{
		int a_ = 0;
		if (A_0 == null)
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
				break;
			}
			throw new ArgumentNullException(ClipboardData.b("ཥᱧཀྵū", a_));
		}
		if (true)
		{
		}
		this.ᜁ.Add(A_0);
		this.ᜂ.Add(A_0.ᜇ(), A_0);
		return A_0;
	}

	// Token: 0x0600279F RID: 10143 RVA: 0x0027BE00 File Offset: 0x0027AE00
	public void ᜀ(string A_0)
	{
		for (;;)
		{
			IL_14:
			if (true)
			{
			}
			int num = this.ᜆ(A_0);
			for (;;)
			{
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						this.ᜁ(num);
						num2 = 2;
						continue;
					case 1:
						if (num >= 0)
						{
							num2 = 0;
							continue;
						}
						goto IL_55;
					case 2:
						goto IL_53;
					}
					goto IL_14;
				}
				IL_55:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_6B;
				}
				IL_53:
				goto IL_55;
			}
		}
		IL_6B:
		if (false)
		{
		}
	}

	// Token: 0x060027A0 RID: 10144 RVA: 0x0027BE80 File Offset: 0x0027AE80
	public void ᜁ(int A_0)
	{
		int a_ = 11;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_9A;
			case 1:
				if (A_0 >= this.ᜁ.Count)
				{
					num = 0;
					continue;
				}
				goto IL_9C;
			case 3:
				num = 1;
				continue;
			}
			if (A_0 < 0)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_9C;
			default:
				if (false)
				{
				}
				num = 3;
				break;
			}
		}
		IL_5D:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᡰᵲᅴቶŸ", a_));
		IL_9A:
		goto IL_5D;
		IL_9C:
		sprℭ sprℭ = this.ᜀ(A_0);
		this.ᜁ.RemoveAt(A_0);
		this.ᜂ.Remove(sprℭ.ᜇ());
	}

	// Token: 0x060027A1 RID: 10145 RVA: 0x0027BF50 File Offset: 0x0027AF50
	public void ᜀ(Regex A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int num2 = this.ᜁ.Count;
				int num3 = 6;
				for (;;)
				{
					string text;
					switch (num3)
					{
					case 0:
						if (A_0.IsMatch(text))
						{
							num3 = 3;
							continue;
						}
						goto IL_56;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_65;
						default:
						{
							if (true)
							{
							}
							if (false)
							{
							}
							if (num >= num2)
							{
								num3 = 2;
								continue;
							}
							sprℭ sprℭ = this.ᜁ[num];
							text = sprℭ.ᜇ();
							num3 = 0;
							continue;
						}
						}
						break;
					case 2:
						return;
					case 3:
						goto IL_65;
					case 4:
						goto IL_C8;
					case 5:
						goto IL_56;
					case 6:
						goto IL_C8;
					}
					break;
					IL_56:
					num++;
					num3 = 4;
					continue;
					IL_65:
					this.ᜁ.RemoveAt(num);
					this.ᜂ.Remove(text);
					num--;
					num2--;
					num3 = 5;
					continue;
					IL_C8:
					num3 = 1;
				}
			}
			return;
		}
	}

	// Token: 0x060027A2 RID: 10146 RVA: 0x0027C068 File Offset: 0x0027B068
	public void ᜀ(string A_0, Stream A_1, bool A_2)
	{
		int a_ = 1;
		if (true)
		{
		}
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
			sprℭ sprℭ = this.ᜃ(A_0);
			if (sprℭ != null)
			{
				sprℭ.ᜁ(A_1, A_2);
				return;
			}
			break;
		}
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("๦ᵨ๪lⅮၰṲၴ", a_), ClipboardData.b("⑦ࡨժͬnհ卲፴Ṷ᝸ὺ嵼౾놐朗떚", a_));
	}

	// Token: 0x060027A3 RID: 10147 RVA: 0x0027C0E4 File Offset: 0x0027B0E4
	public void ᜀ(string A_0, Stream A_1, bool A_2, FileAttributes A_3)
	{
		sprℭ sprℭ;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			sprℭ = this.ᜃ(A_0);
			if (sprℭ == null)
			{
				this.ᜁ(A_0, A_1, A_2, A_3);
				return;
			}
			break;
		}
		if (true)
		{
		}
		sprℭ.ᜁ(A_1, A_2);
	}

	// Token: 0x060027A4 RID: 10148 RVA: 0x0027C144 File Offset: 0x0027B144
	public void ᜀ(string A_0, byte[] A_1)
	{
		int a_ = 12;
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
			sprℭ sprℭ = this.ᜃ(A_0);
			if (sprℭ != null)
			{
				if (true)
				{
				}
				MemoryStream a_2 = new MemoryStream(A_1);
				sprℭ.ᜁ(a_2, true);
				return;
			}
			break;
		}
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("᭱s፵ᕷ㑹ᵻ፽", a_), ClipboardData.b("ㅱᕳᡵᙷᕹࡻ幽ꢇ黎ﲋﮑﾕﶗﺙ벛풟잡즣袥", a_));
	}

	// Token: 0x060027A5 RID: 10149 RVA: 0x0027C1C8 File Offset: 0x0027B1C8
	public void ᜂ(string A_0)
	{
		int a_ = 10;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_90;
			case 1:
				if (A_0.Length == 0)
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				goto IL_92;
			case 3:
				num = 1;
				continue;
			}
			if (A_0 == null)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_92;
			default:
				if (false)
				{
				}
				num = 3;
				break;
			}
		}
		IL_5C:
		throw new ArgumentOutOfRangeException(ClipboardData.b("Ὧݱsٵ൷๹㩻᝽쪃", a_));
		IL_90:
		goto IL_5C;
		IL_92:
		this.ᜀ(A_0, false);
	}

	// Token: 0x060027A6 RID: 10150 RVA: 0x0027C270 File Offset: 0x0027B270
	public void ᜀ(string A_0, bool A_1)
	{
		int a_ = 5;
		int num = 4;
		for (;;)
		{
			FileStream fileStream;
			string directoryName;
			switch (num)
			{
			case 0:
				if (A_0.Length == 0)
				{
					num = 7;
					continue;
				}
				num = 5;
				continue;
			case 1:
				num = 0;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					goto IL_F8;
				}
				break;
			case 3:
				if (true)
				{
				}
				try
				{
					this.ᜀ(fileStream, false);
					return;
				}
				finally
				{
					num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							((IDisposable)fileStream).Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_F5;
						}
						if (fileStream == null)
						{
							break;
						}
						num = 1;
					}
					IL_F5:;
				}
				goto IL_F8;
			case 5:
				if (A_1)
				{
					num = 2;
					continue;
				}
				goto IL_4E;
			case 6:
				if (!Directory.Exists(directoryName))
				{
					num = 8;
					continue;
				}
				goto IL_4E;
			case 7:
				goto IL_AF;
			case 8:
				Directory.CreateDirectory(directoryName);
				num = 9;
				continue;
			case 9:
				goto IL_4E;
			}
			if (A_0 != null)
			{
				num = 1;
				continue;
			}
			break;
			IL_4E:
			fileStream = new FileStream(A_0, FileMode.Create, FileAccess.Write);
			num = 3;
			continue;
			IL_F8:
			string fullPath = Path.GetFullPath(A_0);
			directoryName = Path.GetDirectoryName(fullPath);
			num = 6;
		}
		IL_7A:
		throw new ArgumentOutOfRangeException(ClipboardData.b("Ѫᡬ᭮ŰٲŴㅶၸ᝺᡼ㅾ", a_));
		IL_AF:
		goto IL_7A;
	}

	// Token: 0x060027A7 RID: 10151 RVA: 0x0027C400 File Offset: 0x0027B400
	public void ᜀ(Stream A_0, bool A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				Stream stream;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					goto IL_14E;
				case 1:
					if (stream != null)
					{
						goto IL_126;
					}
					goto IL_EA;
				case 2:
					goto IL_A2;
				case 3:
					if (!A_0.CanSeek)
					{
						num = 7;
						continue;
					}
					goto IL_14E;
				case 5:
				{
					if (num2 >= count)
					{
						num = 9;
						continue;
					}
					sprℭ sprℭ = this.ᜁ[num2];
					sprℭ.ᜈ(A_0);
					num2++;
					num = 2;
					continue;
				}
				case 6:
					A_0.Position = 0L;
					((MemoryStream)A_0).WriteTo(stream);
					A_0.Close();
					A_0 = stream;
					num = 10;
					continue;
				case 7:
					stream = A_0;
					A_0 = new MemoryStream();
					num = 0;
					continue;
				case 8:
					if (A_1)
					{
						num = 13;
						continue;
					}
					return;
				case 9:
					this.ᜂ(A_0);
					num = 1;
					continue;
				case 10:
					goto IL_EA;
				case 11:
					goto IL_A2;
				case 12:
					goto IL_72;
				case 13:
					A_0.Close();
					num = 14;
					continue;
				case 14:
					return;
				}
				if (A_0 == null)
				{
					num = 12;
					continue;
				}
				stream = null;
				num = 3;
				continue;
				IL_A2:
				num = 5;
				continue;
				IL_EA:
				num = 8;
				continue;
				IL_126:
				num = 6;
				continue;
				IL_14E:
				if (true)
				{
				}
				num2 = 0;
				count = this.ᜁ.Count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_126;
				default:
					if (false)
					{
					}
					num = 11;
					break;
				}
			}
			IL_72:
			throw new ArgumentNullException();
		}
		}
	}

	// Token: 0x060027A8 RID: 10152 RVA: 0x0027C5E0 File Offset: 0x0027B5E0
	public void ᜄ(string A_0)
	{
		int a_ = 11;
		int num = 4;
		for (;;)
		{
			FileStream fileStream;
			switch (num)
			{
			case 0:
				try
				{
					this.ᜁ(fileStream, false);
					return;
				}
				finally
				{
					num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_A7;
						case 2:
							((IDisposable)fileStream).Dispose();
							num = 1;
							continue;
						}
						if (fileStream == null)
						{
							break;
						}
						num = 2;
					}
					IL_A7:;
				}
				goto IL_AA;
			case 1:
				if (A_0.Length == 0)
				{
					if (true)
					{
					}
					num = 2;
					continue;
				}
				goto IL_AA;
			case 2:
				goto IL_E6;
			case 3:
				num = 1;
				continue;
			}
			IL_2D:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_2D;
			default:
				if (false)
				{
				}
				if (A_0 != null)
				{
					num = 3;
					continue;
				}
				goto IL_E8;
			}
			IL_AA:
			fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read);
			num = 0;
		}
		IL_E6:
		IL_E8:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᡰᵲմɶ൸㵺ᑼ፾춂", a_));
	}

	// Token: 0x060027A9 RID: 10153 RVA: 0x0027C6FC File Offset: 0x0027B6FC
	public void ᜁ(Stream A_0, bool A_1)
	{
		int a_ = 6;
		int num = 1;
		long num2;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_62;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_62;
				case 2:
					goto IL_A1;
				case 3:
					if (num2 < 0L)
					{
						num = 2;
						continue;
					}
					goto IL_B7;
				}
				if (A_0 == null)
				{
					num = 0;
				}
				else
				{
					num2 = spr\u1FDD.ᜀ(A_0, 101010256U, 65557);
					num = 3;
				}
				break;
			}
		}
		IL_62:
		throw new ArgumentNullException(ClipboardData.b("Ὣᩭɯ᝱ᕳ᭵", a_));
		IL_A1:
		throw new sprᥠ(ClipboardData.b("⽫཭ṯ啱s噵ᑷᕹύώꒃ겋늑ﾝ첟芡삣쾥\udaa7쾩쾫\udaad\udfaf삱춳隵쪷\udfb9\udfbb톽늿ꛁ飇ꗉ뿋뷍맏냑룓돕귙껛뇝軟藡쓣胥臧蛩觫컭雯鷱蛳鯵駷軹\udcfb釽狿∁攃琅欇戉攋砍甏㈑紓攕㠗礙猛氝刟圡吣別ا", a_));
		IL_B7:
		A_0.Position = num2 + 12L;
		int num3 = spr\u1FDD.ᜅ(A_0);
		long position = num2 - (long)num3;
		A_0.Position = position;
		this.ᜁ(A_0);
		this.ᜀ(A_0);
	}

	// Token: 0x060027AA RID: 10154 RVA: 0x0027C7EC File Offset: 0x0027B7EC
	public void ᜁ()
	{
		if (true)
		{
		}
		for (;;)
		{
			int num = 0;
			int count = this.ᜁ.Count;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					if (num >= count)
					{
						num2 = 1;
						continue;
					}
					sprℭ sprℭ = this.ᜁ[num];
					sprℭ.\u1714();
					num++;
					num2 = 3;
					continue;
				}
				case 1:
					goto IL_68;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						goto IL_54;
					}
					break;
				case 3:
					goto IL_54;
				}
				break;
				IL_54:
				num2 = 0;
			}
		}
		IL_68:
		this.ᜁ.Clear();
		this.ᜂ.Clear();
		this.ᜂ = null;
	}

	// Token: 0x060027AB RID: 10155 RVA: 0x0027C8B0 File Offset: 0x0027B8B0
	public int ᜆ(string A_0)
	{
		switch (0)
		{
		default:
		{
			int result;
			for (;;)
			{
				result = -1;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_9F;
					case 1:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							if (true)
							{
							}
							num = 2;
							continue;
						}
						sprℭ sprℭ = this.ᜁ[num2];
						num = 8;
						continue;
					}
					case 2:
						return result;
					case 3:
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
							int num2;
							result = num2;
							num = 6;
							continue;
						}
						}
						break;
					case 4:
						goto IL_9F;
					case 5:
					{
						sprℭ sprℭ2;
						if (this.ᜂ.TryGetValue(A_0, out sprℭ2))
						{
							num = 7;
							continue;
						}
						return result;
					}
					case 6:
						return result;
					case 7:
					{
						int num2 = 0;
						int count = this.ᜁ.Count;
						num = 0;
						continue;
					}
					case 8:
					{
						sprℭ sprℭ;
						sprℭ sprℭ2;
						if (sprℭ == sprℭ2)
						{
							num = 3;
							continue;
						}
						int num2;
						num2++;
						num = 4;
						continue;
					}
					}
					break;
					IL_9F:
					num = 1;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x060027AC RID: 10156 RVA: 0x0027C9DC File Offset: 0x0027B9DC
	public int ᜁ(Regex A_0)
	{
		switch (0)
		{
		default:
		{
			int result;
			for (;;)
			{
				result = -1;
				int num = 0;
				int count = this.ᜁ.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						string input;
						if (A_0.IsMatch(input))
						{
							num2 = 4;
							continue;
						}
						if (true)
						{
						}
						num++;
						num2 = 6;
						continue;
					}
					case 1:
						goto IL_B2;
					case 2:
						return result;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
						{
							if (false)
							{
							}
							if (num >= count)
							{
								num2 = 5;
								continue;
							}
							sprℭ sprℭ = this.ᜁ[num];
							string input = sprℭ.ᜇ();
							num2 = 0;
							continue;
						}
						}
						break;
					case 4:
						goto IL_6F;
					case 5:
						return result;
					case 6:
						goto IL_B2;
					}
					break;
					IL_6F:
					result = num;
					num2 = 2;
					continue;
					IL_B2:
					num2 = 3;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x060027AD RID: 10157 RVA: 0x0027CAD8 File Offset: 0x0027BAD8
	private void ᜂ(Stream A_0)
	{
		switch (0)
		{
		default:
		{
			long position;
			for (;;)
			{
				IL_43:
				position = A_0.Position;
				int num = 0;
				int count = this.ᜁ.Count;
				int num2 = 0;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_61;
					default:
						if (false)
						{
						}
						switch (num2)
						{
						case 0:
							goto IL_61;
						case 1:
							goto IL_8B;
						case 2:
						{
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							sprℭ sprℭ = this.ᜁ[num];
							sprℭ.ᜇ(A_0);
							num++;
							num2 = 3;
							continue;
						}
						case 3:
							goto IL_75;
						}
						goto IL_43;
					}
					IL_75:
					num2 = 2;
					continue;
					IL_61:
					if (true)
					{
					}
					goto IL_75;
				}
			}
			IL_8B:
			this.ᜀ(A_0, position);
			return;
		}
		}
	}

	// Token: 0x060027AE RID: 10158 RVA: 0x0027CBA0 File Offset: 0x0027BBA0
	private void ᜀ(Stream A_0, long A_1)
	{
		int a_ = 6;
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
			if (A_0 != null)
			{
				int value = (int)(A_0.Position - A_1);
				A_0.Write(BitConverter.GetBytes(101010256), 0, 4);
				A_0.WriteByte(0);
				A_0.WriteByte(0);
				A_0.WriteByte(0);
				A_0.WriteByte(0);
				byte[] bytes = BitConverter.GetBytes((short)this.ᜁ.Count);
				A_0.Write(bytes, 0, 2);
				A_0.Write(bytes, 0, 2);
				A_0.Write(BitConverter.GetBytes(value), 0, 4);
				A_0.Write(BitConverter.GetBytes((int)A_1), 0, 4);
				A_0.WriteByte(0);
				A_0.WriteByte(0);
				return;
			}
			break;
		}
		throw new ArgumentNullException(ClipboardData.b("Ὣᩭɯ᝱ᕳ᭵", a_));
	}

	// Token: 0x060027AF RID: 10159 RVA: 0x0027CC84 File Offset: 0x0027BC84
	private void ᜁ(Stream A_0)
	{
		int a_ = 11;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				if (spr\u1FDD.ᜅ(A_0) != 33639248)
				{
					num = 3;
					continue;
				}
				sprℭ sprℭ = new sprℭ(this);
				sprℭ.ᜆ(A_0);
				this.ᜁ.Add(sprℭ);
				num = 1;
				continue;
			}
			case 1:
				goto IL_68;
			case 3:
				return;
			case 4:
				goto IL_66;
			}
			IL_2D:
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_2D;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				break;
			}
			IL_68:
			num = 0;
		}
		IL_66:
		throw new ArgumentNullException(ClipboardData.b("ɰݲݴቶᡸᙺ", a_));
	}

	// Token: 0x060027B0 RID: 10160 RVA: 0x0027CD54 File Offset: 0x0027BD54
	private void ᜀ(Stream A_0)
	{
		int a_ = 2;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_A8;
			case 2:
				goto IL_4C;
			case 4:
				if (A_0.CanSeek)
				{
					num = 9;
					continue;
				}
				goto IL_69;
			case 5:
			{
				int num2;
				int count;
				if (num2 >= count)
				{
					num = 0;
					continue;
				}
				sprℭ sprℭ = this.ᜁ[num2];
				sprℭ.ᜀ(A_0, this.ᜄ);
				this.ᜂ.Add(sprℭ.ᜇ(), sprℭ);
				num2++;
				num = 1;
				continue;
			}
			case 6:
				goto IL_A8;
			case 7:
			{
				if (!A_0.CanRead)
				{
					num = 8;
					continue;
				}
				int num2 = 0;
				int count = this.ᜁ.Count;
				num = 6;
				continue;
			}
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_153;
				}
				break;
			case 9:
				num = 7;
				continue;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 4;
			continue;
			IL_A8:
			if (true)
			{
			}
			num = 5;
		}
		IL_4C:
		throw new ArgumentNullException();
		IL_69:
		throw new ArgumentOutOfRangeException(ClipboardData.b("᭧ṩṫ୭ᅯά", a_), ClipboardData.b("㽧ཀྵ䱫mᕯ᝱ၳ噵୷ό᥻ᕽꢇ낏ﲗﮙﺛ얟芡힣튥\udaa7쾩춫쎭邯욱\udbb3隵좷\udbb9캻춽ꖿ귃닅귇Ꟊ뿋", a_));
		IL_153:
		if (false)
		{
		}
		goto IL_69;
	}

	// Token: 0x060027B1 RID: 10161 RVA: 0x0027CEC0 File Offset: 0x0027BEC0
	public spr\u1FDD ᜀ()
	{
		if (true)
		{
		}
		switch (0)
		{
		default:
		{
			spr\u1FDD spr_u1FDD;
			for (;;)
			{
				IL_4B:
				spr_u1FDD = (spr\u1FDD)base.MemberwiseClone();
				spr_u1FDD.ᜁ = new List<sprℭ>();
				spr_u1FDD.ᜂ = new Dictionary<string, sprℭ>();
				int num = 0;
				int count = this.ᜁ.Count;
				int num2 = 2;
				for (;;)
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
						switch (num2)
						{
						case 0:
							goto IL_90;
						case 1:
						{
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							sprℭ sprℭ = this.ᜁ[num];
							sprℭ = sprℭ.\u170D();
							spr_u1FDD.ᜀ(sprℭ);
							num++;
							num2 = 0;
							continue;
						}
						case 2:
							goto IL_8E;
						case 3:
							return spr_u1FDD;
						}
						goto IL_4B;
					}
					IL_90:
					num2 = 1;
					continue;
					IL_8E:
					goto IL_90;
				}
			}
			return spr_u1FDD;
		}
		}
	}

	// Token: 0x060027B2 RID: 10162 RVA: 0x0027CFAC File Offset: 0x0027BFAC
	public void ᜃ()
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_41;
			case 1:
				goto IL_41;
			case 2:
			{
				int num2 = 0;
				int count = this.ᜁ.Count;
				num = 1;
				continue;
			}
			case 3:
				return;
			case 4:
			{
				if (true)
				{
				}
				int num2;
				int count;
				if (num2 >= count)
				{
					num = 6;
					continue;
				}
				sprℭ sprℭ = this.ᜁ[num2];
				sprℭ.ᜅ();
				num2++;
				num = 0;
				continue;
			}
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					GC.SuppressFinalize(this);
					break;
				}
				num = 3;
				continue;
			}
			if (this.ᜁ != null)
			{
				num = 2;
				continue;
			}
			break;
			IL_41:
			num = 4;
		}
	}

	// Token: 0x060027B3 RID: 10163 RVA: 0x0027D090 File Offset: 0x0027C090
	protected virtual void ᜅ()
	{
		try
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					IL_30:
					this.ᜃ();
					num = 3;
					continue;
				case 2:
					goto IL_66;
				case 3:
					goto IL_42;
				}
				if (this.ᜁ != null)
				{
					num = 1;
					continue;
				}
				IL_42:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_30;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
			}
			IL_66:;
		}
		finally
		{
			base.Finalize();
		}
		if (true)
		{
		}
	}

	// Token: 0x060027B4 RID: 10164 RVA: 0x0027D130 File Offset: 0x0027C130
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u1FDD()
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
		spr\u1FDD.ᜀ = new byte[4];
	}

	// Token: 0x040022DA RID: 8922
	private static byte[] ᜀ;

	// Token: 0x040022DB RID: 8923
	private List<sprℭ> ᜁ = new List<sprℭ>();

	// Token: 0x040022DC RID: 8924
	private Dictionary<string, sprℭ> ᜂ = new Dictionary<string, sprℭ>();

	// Token: 0x040022DD RID: 8925
	private sprᰣ ᜃ;

	// Token: 0x040022DE RID: 8926
	private bool ᜄ = true;

	// Token: 0x040022DF RID: 8927
	private CompressionLevel ᜅ = CompressionLevel.Best;

	// Token: 0x040022E0 RID: 8928
	private bool ᜆ;

	// Token: 0x040022E1 RID: 8929
	public spr\u1FDD.ᜀ ᜇ;

	// Token: 0x020002D8 RID: 728
	// (Invoke) Token: 0x060027B6 RID: 10166
	public delegate Stream ᜀ(Stream A_0);
}
