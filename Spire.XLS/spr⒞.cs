using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text.RegularExpressions;
using Spire.Compression;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200037B RID: 891
[DefaultMember("Item")]
internal class spr\u249E : IDisposable
{
	// Token: 0x0600364D RID: 13901 RVA: 0x001EB4CC File Offset: 0x001EA4CC
	public spr\u2570 ᜀ(int A_0)
	{
		int a_ = 12;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 1;
				continue;
			case 1:
				if (A_0 > this.ᜁ.Count)
				{
					num = 3;
					continue;
				}
				goto IL_99;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_91;
				}
				break;
			}
			if (true)
			{
			}
			if (A_0 < 0)
			{
				break;
			}
			num = 0;
		}
		IL_49:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭁⩃≅ⵇ㉉", a_));
		IL_91:
		if (false)
		{
		}
		goto IL_49;
		IL_99:
		return this.ᜁ[A_0];
	}

	// Token: 0x0600364E RID: 13902 RVA: 0x001EB580 File Offset: 0x001EA580
	public spr\u2570 ᜃ(string A_0)
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
		spr\u2570 result;
		this.ᜂ.TryGetValue(A_0, out result);
		return result;
	}

	// Token: 0x0600364F RID: 13903 RVA: 0x001EB5CC File Offset: 0x001EA5CC
	public int ᜇ()
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
			if (this.ᜁ == null)
			{
				if (true)
				{
				}
				return 0;
			}
			break;
		}
		return this.ᜁ.Count;
	}

	// Token: 0x06003650 RID: 13904 RVA: 0x001EB620 File Offset: 0x001EA620
	public spr\u24A5 ᜆ()
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

	// Token: 0x06003651 RID: 13905 RVA: 0x001EB664 File Offset: 0x001EA664
	public void ᜀ(spr\u24A5 A_0)
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

	// Token: 0x06003652 RID: 13906 RVA: 0x001EB6A8 File Offset: 0x001EA6A8
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

	// Token: 0x06003653 RID: 13907 RVA: 0x001EB6EC File Offset: 0x001EA6EC
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

	// Token: 0x06003654 RID: 13908 RVA: 0x001EB730 File Offset: 0x001EA730
	public bool ᜅ()
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

	// Token: 0x06003655 RID: 13909 RVA: 0x001EB774 File Offset: 0x001EA774
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

	// Token: 0x06003656 RID: 13910 RVA: 0x001EB7B8 File Offset: 0x001EA7B8
	[CLSCompliant(false)]
	public static long ᜀ(Stream A_0, uint A_1, int A_2)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 14;
			for (;;)
			{
				bool flag;
				switch (num)
				{
				case 0:
				{
					long length;
					if (length < 4L)
					{
						num = 13;
						continue;
					}
					byte[] array = new byte[4];
					long num2 = Math.Max(0L, length - (long)A_2);
					long num3 = length - 1L - 4L;
					A_0.Position = num3;
					A_0.Read(array, 0, 4);
					uint num4 = BitConverter.ToUInt32(array, 0);
					flag = (num4 == A_1);
					num = 16;
					continue;
				}
				case 1:
					num = 5;
					continue;
				case 2:
				{
					if (!flag)
					{
						num = 9;
						continue;
					}
					long num3;
					return num3;
				}
				case 3:
					goto IL_13F;
				case 4:
					if (A_0.CanSeek)
					{
						num = 11;
						continue;
					}
					goto IL_1CC;
				case 5:
					goto IL_1E0;
				case 6:
				{
					uint num4;
					if (num4 == A_1)
					{
						num = 8;
						continue;
					}
					goto IL_1E0;
				}
				case 7:
					if (true)
					{
					}
					goto IL_144;
				case 8:
					flag = true;
					num = 10;
					continue;
				case 9:
					goto IL_163;
				case 10:
					goto IL_144;
				case 11:
					num = 12;
					continue;
				case 12:
				{
					if (!A_0.CanRead)
					{
						num = 3;
						continue;
					}
					long length = A_0.Length;
					num = 0;
					continue;
				}
				case 13:
					goto IL_252;
				case 15:
					goto IL_9C;
				case 16:
					goto IL_107;
				case 17:
				{
					long num2;
					long num3;
					if (num3 <= num2)
					{
						num = 7;
						continue;
					}
					uint num4;
					num4 <<= 8;
					num3 -= 1L;
					A_0.Position = num3;
					num4 += (uint)A_0.ReadByte();
					num = 6;
					continue;
				}
				}
				if (A_0 != null)
				{
					num = 4;
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
					num = 15;
					continue;
				}
				IL_107:
				if (!flag)
				{
					num = 1;
					continue;
				}
				IL_144:
				num = 2;
				continue;
				IL_1E0:
				num = 17;
			}
			IL_9C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨸伺似娾⁀⹂", a_));
			IL_13F:
			goto IL_1CC;
			IL_163:
			return -1L;
			IL_1CC:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("游帺ᴼ儾⑀♂⅄杆㵈⑊浌❎ぐ╒ご睖⩘㹚㡜㑞`Ţ।ɦ䥨੪ͬ୮兰Ųၴᙶᵸ᩺ὼ፾ꎂﮈ뾐", a_));
			IL_252:
			return -1L;
		}
		}
	}

	// Token: 0x06003657 RID: 13911 RVA: 0x001EBA20 File Offset: 0x001EAA20
	public static int ᜅ(Stream A_0)
	{
		int a_ = 17;
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
			if (A_0.Read(spr\u249E.ᜀ, 0, 4) != 4)
			{
				throw new sprớ(RecordTableEnumerator.b("ቆ❈⩊⽌⍎㑐獒⅔㡖祘⥚㡜㹞ՠ䍢፤٦ըṪ࡬佮ၰݲ啴ͶᅸṺ嵼౾놐杖쾠莢袤螦첨얪즬辮\udeb0햲閴쒶춸즺\ud8bc\udebe곀닄ꛆ뫈뿌꫎냐냒뷔닖뷘", a_));
			}
			break;
		}
		return BitConverter.ToInt32(spr\u249E.ᜀ, 0);
	}

	// Token: 0x06003658 RID: 13912 RVA: 0x001EBA98 File Offset: 0x001EAA98
	public static short ᜄ(Stream A_0)
	{
		int a_ = 14;
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
			if (A_0.Read(spr\u249E.ᜀ, 0, 2) != 2)
			{
				throw new sprớ(RecordTableEnumerator.b("ᅃ⡅⥇⡉⁋⭍灏♑㭓癕⩗㽙㵛㩝䁟ᑡգ੥ᵧཀྵ䱫཭ѯ剱sṵᵷ婹ཻ๽꺍﶑ﾕ肟辡蒣쎥욧캩貫솭횯銱잳습쪷\udfb9\uddbb펽뗁ꗃ뗅룉꧋꿍돏뫑뇓닕", a_));
			}
			break;
		}
		return BitConverter.ToInt16(spr\u249E.ᜀ, 0);
	}

	// Token: 0x06003659 RID: 13913 RVA: 0x001EBB10 File Offset: 0x001EAB10
	public spr\u249E()
	{
		this.ᜆ = new spr\u249E.ᜀ(this.ᜃ);
	}

	// Token: 0x0600365A RID: 13914 RVA: 0x001EBB5C File Offset: 0x001EAB5C
	private Stream ᜃ(Stream A_0)
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
		return new DeflateStream(A_0, CompressionMode.Compress, true);
	}

	// Token: 0x0600365B RID: 13915 RVA: 0x001EBBA0 File Offset: 0x001EABA0
	public spr\u2570 ᜁ(string A_0)
	{
		int a_ = 7;
		int num = 4;
		FileAttributes attributes;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜃ != null)
				{
					num = 1;
					continue;
				}
				goto IL_E7;
			case 1:
				A_0 = this.ᜃ.ᜀ(A_0);
				num = 6;
				continue;
			case 2:
				num = 3;
				continue;
			case 3:
			{
				if (A_0.Length == 0)
				{
					num = 5;
					continue;
				}
				DirectoryInfo directoryInfo = new DirectoryInfo(A_0);
				attributes = directoryInfo.Attributes;
				num = 0;
				continue;
			}
			case 5:
				goto IL_CA;
			case 6:
				goto IL_E5;
			}
			IL_35:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_35;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					goto IL_8E;
				}
				num = 2;
				break;
			}
		}
		IL_8E:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("夼嘾㍀♂♄㍆♈㥊㑌Ŏぐ㹒ご", a_));
		IL_CA:
		goto IL_8E;
		IL_E5:
		IL_E7:
		return this.ᜁ(A_0, null, false, attributes);
	}

	// Token: 0x0600365C RID: 13916 RVA: 0x001EBCA0 File Offset: 0x001EACA0
	public spr\u2570 ᜅ(string A_0)
	{
		Stream a_;
		FileAttributes attributes;
		for (;;)
		{
			for (;;)
			{
				a_ = new FileStream(A_0, FileMode.Open, FileAccess.Read);
				FileInfo fileInfo = new FileInfo(A_0);
				attributes = fileInfo.Attributes;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						A_0 = this.ᜃ.ᜀ(A_0);
						num = 2;
						continue;
					case 1:
						if (this.ᜃ != null)
						{
							num = 0;
							continue;
						}
						goto IL_8B;
					case 2:
						goto IL_6D;
					}
					break;
				}
			}
			IL_6D:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_83;
			}
		}
		IL_83:
		if (false)
		{
		}
		IL_8B:
		return this.ᜁ(A_0, a_, true, attributes);
	}

	// Token: 0x0600365D RID: 13917 RVA: 0x001EBD44 File Offset: 0x001EAD44
	public spr\u2570 ᜁ(string A_0, Stream A_1, bool A_2, FileAttributes A_3)
	{
		int a_ = 0;
		for (;;)
		{
			A_0 = A_0.Replace('\\', '/');
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (A_0.IndexOf(':') == A_0.LastIndexOf(':'))
					{
						num = 1;
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
						num = 2;
						continue;
					}
					break;
				case 1:
					if (this.ᜂ.ContainsKey(A_0))
					{
						num = 3;
						continue;
					}
					goto IL_EF;
				case 2:
					goto IL_7D;
				case 3:
					goto IL_CB;
				}
				break;
			}
		}
		IL_7D:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("氵儷䨹画䨽┿⽁摃⡅⥇❉⥋湍㍏㵑㩓≕㥗㍙㉛ⵝ䁟ୡࡣ੥൧൩൫ɭ偯ᅱᱳ᝵੷᭹ύ੽ꢅ", a_), RecordTableEnumerator.b("張䰷弹儻瀽ℿ⽁⅃", a_));
		IL_CB:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("缵䰷弹儻ḽ", a_) + A_0 + RecordTableEnumerator.b("ᘵ夷嘹主嬽ℿ♁㵃晅ⵇ㉉╋㵍⑏⅑瑓㽕㙗穙⡛㙝՟䉡գᑥ୧ɩիᡭᕯ", a_));
		IL_EF:
		spr\u2570 spr_u = new spr\u2570(this, A_0, A_1, A_2, A_3);
		spr_u.ᜀ(this.ᜅ);
		return this.ᜀ(spr_u);
	}

	// Token: 0x0600365E RID: 13918 RVA: 0x001EBE60 File Offset: 0x001EAE60
	public spr\u2570 ᜀ(spr\u2570 A_0)
	{
		int a_ = 10;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("⤿㙁⅃⭅", a_));
			}
			break;
		}
		this.ᜁ.Add(A_0);
		this.ᜂ.Add(A_0.ᜇ(), A_0);
		return A_0;
	}

	// Token: 0x0600365F RID: 13919 RVA: 0x001EBEDC File Offset: 0x001EAEDC
	public void ᜀ(string A_0)
	{
		for (;;)
		{
			for (;;)
			{
				int num = this.ᜆ(A_0);
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						if (num >= 0)
						{
							num2 = 2;
							continue;
						}
						return;
					case 1:
						goto IL_53;
					case 2:
						this.ᜁ(num);
						num2 = 1;
						continue;
					}
					break;
				}
			}
			IL_53:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_69;
			}
		}
		IL_69:
		if (false)
		{
		}
	}

	// Token: 0x06003660 RID: 13920 RVA: 0x001EBF5C File Offset: 0x001EAF5C
	public void ᜁ(int A_0)
	{
		int a_ = 14;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 >= this.ᜁ.Count)
				{
					num = 2;
					continue;
				}
				goto IL_91;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_89;
				}
				break;
			case 3:
				num = 0;
				continue;
			}
			if (A_0 < 0)
			{
				break;
			}
			num = 3;
		}
		IL_41:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⵃ⡅ⱇ⽉㑋", a_));
		IL_89:
		if (false)
		{
		}
		goto IL_41;
		IL_91:
		if (true)
		{
		}
		spr\u2570 spr_u = this.ᜀ(A_0);
		this.ᜁ.RemoveAt(A_0);
		this.ᜂ.Remove(spr_u.ᜇ());
	}

	// Token: 0x06003661 RID: 13921 RVA: 0x001EC028 File Offset: 0x001EB028
	public void ᜀ(Regex A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				if (true)
				{
				}
				int num = 0;
				int num2 = this.ᜁ.Count;
				int num3 = 4;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
						{
							if (false)
							{
							}
							if (num >= num2)
							{
								num3 = 5;
								continue;
							}
							spr\u2570 spr_u = this.ᜁ[num];
							string text = spr_u.ᜇ();
							num3 = 6;
							continue;
						}
						}
						break;
					case 1:
						goto IL_5E;
					case 2:
						goto IL_D3;
					case 3:
					{
						this.ᜁ.RemoveAt(num);
						string text;
						this.ᜂ.Remove(text);
						num--;
						num2--;
						num3 = 1;
						continue;
					}
					case 4:
						goto IL_D3;
					case 5:
						return;
					case 6:
					{
						string text;
						if (A_0.IsMatch(text))
						{
							num3 = 3;
							continue;
						}
						goto IL_5E;
					}
					}
					break;
					IL_5E:
					num++;
					num3 = 2;
					continue;
					IL_D3:
					num3 = 0;
				}
			}
			return;
		}
	}

	// Token: 0x06003662 RID: 13922 RVA: 0x001EC144 File Offset: 0x001EB144
	public void ᜀ(string A_0, Stream A_1, bool A_2)
	{
		int a_ = 18;
		spr\u2570 spr_u;
		for (;;)
		{
			spr_u = this.ᜃ(A_0);
			if (spr_u != null)
			{
				goto IL_66;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_2A;
			}
		}
		IL_2A:
		if (false)
		{
		}
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⅇ㹉⥋⍍ṏ㍑㥓㍕", a_), RecordTableEnumerator.b("େ⭉≋⁍㽏♑瑓さㅗ㑙㡛繝፟ቡţեŧ౩ի୭ᑯ剱ᵳɵᵷ᝹剻", a_));
		IL_66:
		spr_u.ᜁ(A_1, A_2);
	}

	// Token: 0x06003663 RID: 13923 RVA: 0x001EC1C0 File Offset: 0x001EB1C0
	public void ᜀ(string A_0, Stream A_1, bool A_2, FileAttributes A_3)
	{
		spr\u2570 spr_u;
		for (;;)
		{
			spr_u = this.ᜃ(A_0);
			if (spr_u == null)
			{
				goto IL_44;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_21;
			}
		}
		IL_21:
		if (false)
		{
		}
		if (true)
		{
		}
		spr_u.ᜁ(A_1, A_2);
		return;
		IL_44:
		this.ᜁ(A_0, A_1, A_2, A_3);
	}

	// Token: 0x06003664 RID: 13924 RVA: 0x001EC220 File Offset: 0x001EB220
	public void ᜀ(string A_0, byte[] A_1)
	{
		int a_ = 13;
		spr\u2570 spr_u;
		for (;;)
		{
			if (true)
			{
			}
			spr_u = this.ᜃ(A_0);
			if (spr_u != null)
			{
				goto IL_66;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_32;
			}
		}
		IL_32:
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⩂ㅄ≆⑈Պⱌ≎㑐", a_), RecordTableEnumerator.b("B⑄⥆❈⑊㥌潎㝐㩒㭔㍖祘⡚ⵜ㩞ɠ੢ͤ๦౨ཪ䵬ٮհᙲᡴ奶", a_));
		IL_66:
		MemoryStream a_2 = new MemoryStream(A_1);
		spr_u.ᜁ(a_2, true);
	}

	// Token: 0x06003665 RID: 13925 RVA: 0x001EC2A4 File Offset: 0x001EB2A4
	public void ᜂ(string A_0)
	{
		int a_ = 5;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_4A;
			case 2:
				goto IL_86;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4A;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					if (A_0.Length == 0)
					{
						num = 2;
						continue;
					}
					goto IL_88;
				}
				break;
			}
			if (A_0 != null)
			{
				num = 1;
				continue;
			}
			break;
			IL_4A:
			num = 3;
		}
		IL_36:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("吺䠼䬾ㅀ㙂ㅄņ⁈❊⡌Ŏぐ㹒ご", a_));
		IL_86:
		goto IL_36;
		IL_88:
		this.ᜀ(A_0, false);
	}

	// Token: 0x06003666 RID: 13926 RVA: 0x001EC34C File Offset: 0x001EB34C
	public void ᜀ(string A_0, bool A_1)
	{
		int a_ = 18;
		int num = 0;
		for (;;)
		{
			string directoryName;
			FileStream fileStream;
			switch (num)
			{
			case 1:
				if (A_0.Length == 0)
				{
					num = 8;
					continue;
				}
				num = 3;
				continue;
			case 2:
				Directory.CreateDirectory(directoryName);
				num = 9;
				continue;
			case 3:
				if (A_1)
				{
					num = 4;
					continue;
				}
				goto IL_6A;
			case 4:
				if (true)
				{
				}
				goto IL_119;
			case 5:
				if (!Directory.Exists(directoryName))
				{
					goto IL_13D;
				}
				goto IL_6A;
			case 6:
				try
				{
					this.ᜀ(fileStream, false);
					return;
				}
				finally
				{
					num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							((IDisposable)fileStream).Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_116;
						}
						if (fileStream == null)
						{
							break;
						}
						num = 0;
					}
					IL_116:;
				}
				goto IL_119;
			case 7:
				num = 1;
				continue;
			case 8:
				goto IL_D0;
			case 9:
				goto IL_6A;
			}
			if (A_0 == null)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_13D;
			default:
				if (false)
				{
				}
				num = 7;
				continue;
			}
			IL_6A:
			fileStream = new FileStream(A_0, FileMode.Create, FileAccess.Write);
			num = 6;
			continue;
			IL_119:
			string fullPath = Path.GetFullPath(A_0);
			directoryName = Path.GetDirectoryName(fullPath);
			num = 5;
			continue;
			IL_13D:
			num = 2;
		}
		IL_9B:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("❇㽉㡋㹍╏♑ቓ㽕㑗㽙ቛ㽝ൟݡ", a_));
		IL_D0:
		goto IL_9B;
	}

	// Token: 0x06003667 RID: 13927 RVA: 0x001EC4DC File Offset: 0x001EB4DC
	public void ᜀ(Stream A_0, bool A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_0E:
				int num = 11;
				for (;;)
				{
					int num2;
					int count;
					Stream stream;
					switch (num)
					{
					case 0:
						goto IL_9F;
					case 1:
						return;
					case 2:
						goto IL_164;
					case 3:
					{
						if (num2 >= count)
						{
							num = 9;
							continue;
						}
						spr\u2570 spr_u = this.ᜁ[num2];
						spr_u.ᜈ(A_0);
						num2++;
						num = 0;
						continue;
					}
					case 4:
						if (stream != null)
						{
							num = 14;
							continue;
						}
						goto IL_103;
					case 5:
						goto IL_9F;
					case 6:
						if (!A_0.CanSeek)
						{
							num = 8;
							continue;
						}
						goto IL_164;
					case 7:
						goto IL_103;
					case 8:
						stream = A_0;
						A_0 = new MemoryStream();
						if (true)
						{
						}
						num = 2;
						continue;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_0E;
						default:
							if (false)
							{
							}
							this.ᜂ(A_0);
							num = 4;
							continue;
						}
						break;
					case 10:
						goto IL_72;
					case 12:
						if (A_1)
						{
							num = 13;
							continue;
						}
						return;
					case 13:
						A_0.Close();
						num = 1;
						continue;
					case 14:
						A_0.Position = 0L;
						((MemoryStream)A_0).WriteTo(stream);
						A_0.Close();
						A_0 = stream;
						num = 7;
						continue;
					}
					if (A_0 == null)
					{
						num = 10;
						continue;
					}
					stream = null;
					num = 6;
					continue;
					IL_9F:
					num = 3;
					continue;
					IL_103:
					num = 12;
					continue;
					IL_164:
					num2 = 0;
					count = this.ᜁ.Count;
					num = 5;
				}
			}
			IL_72:
			throw new ArgumentNullException();
		}
	}

	// Token: 0x06003668 RID: 13928 RVA: 0x001EC6B4 File Offset: 0x001EB6B4
	public void ᜄ(string A_0)
	{
		int a_ = 2;
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
					goto IL_D6;
				}
				finally
				{
					num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							((IDisposable)fileStream).Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_89;
						}
						if (fileStream == null)
						{
							break;
						}
						num = 0;
					}
					IL_89:;
				}
				goto IL_8C;
				IL_D6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A2;
				default:
					goto IL_F6;
				}
				break;
			case 1:
				goto IL_C0;
			case 2:
				if (A_0.Length == 0)
				{
					num = 1;
					continue;
				}
				goto IL_8C;
			case 3:
				goto IL_A2;
			}
			if (A_0 != null)
			{
				if (true)
				{
				}
				num = 3;
				continue;
			}
			break;
			IL_8C:
			fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read);
			num = 0;
			continue;
			IL_A2:
			num = 2;
		}
		IL_C0:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("儷吹䰻䬽㐿сⵃ⩅ⵇщⵋ⍍㕏", a_));
		IL_F6:
		if (false)
		{
		}
	}

	// Token: 0x06003669 RID: 13929 RVA: 0x001EC7D0 File Offset: 0x001EB7D0
	public void ᜁ(Stream A_0, bool A_1)
	{
		int a_ = 10;
		int num = 2;
		long num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_A1;
			case 1:
				goto IL_34;
			case 3:
				if (num2 >= 0L)
				{
					goto IL_B7;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2C;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
			goto IL_29;
			IL_2C:
			num = 1;
			continue;
			IL_29:
			if (A_0 == null)
			{
				goto IL_2C;
			}
			num2 = spr\u249E.ᜀ(A_0, 101010256U, 65557);
			num = 3;
		}
		IL_34:
		throw new ArgumentNullException(RecordTableEnumerator.b("㌿㙁㙃⍅⥇❉", a_));
		IL_A1:
		throw new sprớ(RecordTableEnumerator.b("̿⍁⩃慅㱇橉⁋⅍㍏㍑⁓㍕硗㽙㉛㩝䁟ൡɣ䙥୧ཀྵɫᩭɯ፱ᡳ噵ᱷ፹๻᭽ꪉﺋ﶑뚗몙첛펟톡춣쒥쒧쾩貫\ud9ad슯\uddb1\udab3통颷\udcb9햻튽ꖿꋃ꧅뫇Ꟊ귋뫍뷑ꛓ맗꣙뿛뛝觟铡臣웥臧駩쳫跭鿯胱蛳菵裷軹틻", a_));
		IL_B7:
		A_0.Position = num2 + 12L;
		int num3 = spr\u249E.ᜅ(A_0);
		long position = num2 - (long)num3;
		A_0.Position = position;
		this.ᜁ(A_0);
		this.ᜀ(A_0);
	}

	// Token: 0x0600366A RID: 13930 RVA: 0x001EC8C0 File Offset: 0x001EB8C0
	public void ᜁ()
	{
		for (;;)
		{
			IL_18:
			int num = 0;
			int count = this.ᜁ.Count;
			for (;;)
			{
				IL_30:
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_4E;
					case 1:
						goto IL_3A;
					case 2:
						if (num >= count)
						{
							num2 = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_30;
						default:
						{
							if (false)
							{
							}
							spr\u2570 spr_u = this.ᜁ[num];
							spr_u.\u1714();
							num++;
							if (true)
							{
							}
							num2 = 1;
							continue;
						}
						}
						break;
					case 3:
						goto IL_3A;
					}
					goto IL_18;
					IL_3A:
					num2 = 2;
				}
			}
		}
		IL_4E:
		this.ᜁ.Clear();
		this.ᜂ.Clear();
		this.ᜂ = null;
	}

	// Token: 0x0600366B RID: 13931 RVA: 0x001EC984 File Offset: 0x001EB984
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
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return result;
				default:
				{
					if (false)
					{
					}
					int num = 8;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							int num2;
							result = num2;
							num = 6;
							continue;
						}
						case 1:
						{
							spr\u2570 spr_u;
							spr\u2570 spr_u2;
							if (spr_u == spr_u2)
							{
								num = 0;
								continue;
							}
							int num2;
							num2++;
							num = 7;
							continue;
						}
						case 2:
							return result;
						case 3:
							goto IL_D0;
						case 4:
						{
							int num2 = 0;
							int count = this.ᜁ.Count;
							num = 3;
							continue;
						}
						case 5:
						{
							int num2;
							int count;
							if (num2 >= count)
							{
								num = 2;
								continue;
							}
							spr\u2570 spr_u = this.ᜁ[num2];
							num = 1;
							continue;
						}
						case 6:
							return result;
						case 7:
							goto IL_D0;
						case 8:
						{
							spr\u2570 spr_u2;
							if (this.ᜂ.TryGetValue(A_0, out spr_u2))
							{
								num = 4;
								continue;
							}
							return result;
						}
						}
						break;
						IL_D0:
						num = 5;
					}
					break;
				}
				}
			}
			return result;
		}
		}
	}

	// Token: 0x0600366C RID: 13932 RVA: 0x001ECAB0 File Offset: 0x001EBAB0
	public int ᜁ(Regex A_0)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int result;
			for (;;)
			{
				result = -1;
				int num = 0;
				int count = this.ᜁ.Count;
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_B5;
					case 1:
						return result;
					case 2:
					{
						string input;
						if (A_0.IsMatch(input))
						{
							num2 = 6;
							continue;
						}
						num++;
						num2 = 0;
						continue;
					}
					case 3:
						return result;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return result;
						default:
						{
							if (false)
							{
							}
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							spr\u2570 spr_u = this.ᜁ[num];
							string input = spr_u.ᜇ();
							num2 = 2;
							continue;
						}
						}
						break;
					case 5:
						goto IL_B5;
					case 6:
						result = num;
						num2 = 3;
						continue;
					}
					break;
					IL_B5:
					num2 = 4;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x0600366D RID: 13933 RVA: 0x001ECBB0 File Offset: 0x001EBBB0
	private void ᜂ(Stream A_0)
	{
		switch (0)
		{
		default:
		{
			long position;
			for (;;)
			{
				IL_27:
				position = A_0.Position;
				int num = 0;
				int count = this.ᜁ.Count;
				for (;;)
				{
					IL_3C:
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_5D;
						case 1:
							goto IL_47;
						case 2:
							goto IL_47;
						case 3:
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_3C;
							default:
							{
								if (false)
								{
								}
								spr\u2570 spr_u = this.ᜁ[num];
								spr_u.ᜇ(A_0);
								num++;
								num2 = 2;
								continue;
							}
							}
							break;
						}
						goto IL_27;
						IL_47:
						num2 = 3;
					}
				}
			}
			IL_5D:
			this.ᜀ(A_0, position);
			return;
		}
		}
	}

	// Token: 0x0600366E RID: 13934 RVA: 0x001ECC78 File Offset: 0x001EBC78
	private void ᜀ(Stream A_0, long A_1)
	{
		int a_ = 14;
		while (A_0 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("㝃㉅㩇⽉ⵋ⍍", a_));
			}
		}
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
	}

	// Token: 0x0600366F RID: 13935 RVA: 0x001ECD5C File Offset: 0x001EBD5C
	private void ᜁ(Stream A_0)
	{
		int a_ = 17;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_3A;
			case 2:
				goto IL_5F;
			case 3:
			{
				if (spr\u249E.ᜅ(A_0) != 33639248)
				{
					if (true)
					{
					}
					num = 2;
					continue;
				}
				spr\u2570 spr_u = new spr\u2570(this);
				spr_u.ᜆ(A_0);
				this.ᜁ.Add(spr_u);
				num = 0;
				continue;
			}
			case 4:
				goto IL_38;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			IL_3A:
			num = 3;
		}
		for (;;)
		{
			IL_5F:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_B9;
			}
		}
		IL_B9:
		if (false)
		{
		}
		return;
		IL_38:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑆㵈㥊⡌⹎㱐", a_));
	}

	// Token: 0x06003670 RID: 13936 RVA: 0x001ECE28 File Offset: 0x001EBE28
	private void ᜀ(Stream A_0)
	{
		int a_ = 5;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				int num2;
				int count;
				if (num2 >= count)
				{
					num = 9;
					continue;
				}
				spr\u2570 spr_u = this.ᜁ[num2];
				spr_u.ᜀ(A_0, this.ᜄ);
				this.ᜂ.Add(spr_u.ᜇ(), spr_u);
				num2++;
				goto IL_12B;
			}
			case 1:
				if (true)
				{
				}
				num = 6;
				continue;
			case 2:
				goto IL_68;
			case 3:
				goto IL_159;
			case 4:
				if (A_0.CanSeek)
				{
					num = 1;
					continue;
				}
				goto IL_85;
			case 6:
			{
				if (!A_0.CanRead)
				{
					num = 3;
					continue;
				}
				int num2 = 0;
				int count = this.ᜁ.Count;
				num = 8;
				continue;
			}
			case 7:
				goto IL_D9;
			case 8:
				goto IL_D9;
			case 9:
				return;
			}
			if (A_0 != null)
			{
				num = 4;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_12B;
			default:
				if (false)
				{
				}
				num = 2;
				continue;
			}
			IL_D9:
			num = 0;
			continue;
			IL_12B:
			num = 7;
		}
		IL_68:
		throw new ArgumentNullException();
		IL_85:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䠺䤼䴾⑀≂⡄", a_), RecordTableEnumerator.b("氺堼Ἶ⽀♂⁄⍆楈㡊⡌⩎㩐㉒㝔㭖㱘筚㱜ㅞՠ䍢ᝤɦࡨཪ౬൮ᵰᙲ啴Ѷ൸ॺ᡼Ṿꎂꦈﮊﶎ떔ﺖﺚ辠", a_));
		IL_159:
		goto IL_85;
	}

	// Token: 0x06003671 RID: 13937 RVA: 0x001ECF94 File Offset: 0x001EBF94
	public spr\u249E ᜀ()
	{
		switch (0)
		{
		default:
		{
			spr\u249E spr_u249E;
			for (;;)
			{
				IL_27:
				if (true)
				{
				}
				spr_u249E = (spr\u249E)base.MemberwiseClone();
				spr_u249E.ᜁ = new List<spr\u2570>();
				spr_u249E.ᜂ = new Dictionary<string, spr\u2570>();
				int num = 0;
				int count = this.ᜁ.Count;
				for (;;)
				{
					IL_5F:
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_6A;
						case 1:
							return spr_u249E;
						case 2:
							goto IL_6A;
						case 3:
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_5F;
							default:
							{
								if (false)
								{
								}
								spr\u2570 spr_u = this.ᜁ[num];
								spr_u = spr_u.\u170D();
								spr_u249E.ᜀ(spr_u);
								num++;
								num2 = 2;
								continue;
							}
							}
							break;
						}
						goto IL_27;
						IL_6A:
						num2 = 3;
					}
				}
			}
			return spr_u249E;
		}
		}
	}

	// Token: 0x06003672 RID: 13938 RVA: 0x001ED078 File Offset: 0x001EC078
	public void ᜃ()
	{
		int num = 3;
		for (;;)
		{
			int num2;
			int count;
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
					goto IL_64;
				case 1:
					goto IL_64;
				case 2:
					num2 = 0;
					count = this.ᜁ.Count;
					num = 1;
					continue;
				case 4:
					goto IL_6C;
				case 5:
					return;
				case 6:
					GC.SuppressFinalize(this);
					num = 5;
					continue;
				}
				if (this.ᜁ != null)
				{
					num = 2;
					continue;
				}
				return;
				IL_64:
				num = 4;
				continue;
			}
			IL_6C:
			if (true)
			{
			}
			if (num2 >= count)
			{
				num = 6;
			}
			else
			{
				spr\u2570 spr_u = this.ᜁ[num2];
				spr_u.ᜅ();
				num2++;
				num = 0;
			}
		}
	}

	// Token: 0x06003673 RID: 13939 RVA: 0x001ED15C File Offset: 0x001EC15C
	protected virtual void ᜄ()
	{
		try
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_56;
				case 1:
					goto IL_66;
				case 2:
					goto IL_6E;
				}
				if (this.ᜁ == null)
				{
					goto IL_66;
				}
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
					num = 0;
					continue;
				}
				IL_56:
				this.ᜃ();
				num = 1;
				continue;
				IL_66:
				num = 2;
			}
			IL_6E:;
		}
		finally
		{
			base.Finalize();
		}
	}

	// Token: 0x06003674 RID: 13940 RVA: 0x001ED1FC File Offset: 0x001EC1FC
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u249E()
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
		spr\u249E.ᜀ = new byte[4];
	}

	// Token: 0x0400178E RID: 6030
	private static byte[] ᜀ;

	// Token: 0x0400178F RID: 6031
	private List<spr\u2570> ᜁ = new List<spr\u2570>();

	// Token: 0x04001790 RID: 6032
	private Dictionary<string, spr\u2570> ᜂ = new Dictionary<string, spr\u2570>();

	// Token: 0x04001791 RID: 6033
	private spr\u24A5 ᜃ;

	// Token: 0x04001792 RID: 6034
	private bool ᜄ = true;

	// Token: 0x04001793 RID: 6035
	private CompressionLevel ᜅ = CompressionLevel.Best;

	// Token: 0x04001794 RID: 6036
	public spr\u249E.ᜀ ᜆ;

	// Token: 0x0200037C RID: 892
	// (Invoke) Token: 0x06003676 RID: 13942
	public delegate Stream ᜀ(Stream A_0);
}
