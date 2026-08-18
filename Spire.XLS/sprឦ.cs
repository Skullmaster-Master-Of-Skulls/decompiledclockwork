using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003E3 RID: 995
[DefaultMember("Item")]
internal class sprឦ : CollectionExtended<INamedRange>, INameRanges
{
	// Token: 0x06003BEC RID: 15340 RVA: 0x00217630 File Offset: 0x00216630
	internal sprឦ(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
		this.ᜁ();
	}

	// Token: 0x06003BED RID: 15341 RVA: 0x00217658 File Offset: 0x00216658
	public new INamedRange ᜁ(int A_0)
	{
		int a_ = 10;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				num = 2;
				continue;
			case 2:
				if (A_0 >= base.List.Count)
				{
					num = 3;
					continue;
				}
				goto IL_9B;
			case 3:
				goto IL_99;
			}
			if (true)
			{
			}
			if (A_0 < 0)
			{
				break;
			}
			num = 1;
		}
		IL_49:
		throw new ArgumentOutOfRangeException(string.Format(RecordTableEnumerator.b("⤿ⱁ⁃⍅ぇ橉╋㵍灏㵑⅓≕硗㕙㩛繝቟͡੣ť൧䑩", a_), A_0, base.List.Count));
		IL_99:
		goto IL_49;
		IL_9B:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_99;
		default:
			if (false)
			{
			}
			return base.List[A_0];
		}
	}

	// Token: 0x06003BEE RID: 15342 RVA: 0x00217728 File Offset: 0x00216728
	public INamedRange ᜅ(string A_0)
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
		INamedRange result;
		this.ᜀ.TryGetValue(A_0, out result);
		return result;
	}

	// Token: 0x06003BEF RID: 15343 RVA: 0x00217774 File Offset: 0x00216774
	public INamedRange ᜃ(string A_0)
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_C4;
				case 1:
					if (true)
					{
					}
					if (num >= base.List.Count)
					{
						num2 = 0;
						continue;
					}
					num2 = 5;
					continue;
				case 2:
					goto IL_7C;
				case 3:
					goto IL_7C;
				case 4:
					goto IL_7A;
				case 5:
					if (base.List[num].Name == A_0)
					{
						num2 = 4;
						continue;
					}
					num++;
					num2 = 3;
					continue;
				}
				break;
				IL_7C:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num2 = 1;
					break;
				}
			}
		}
		IL_7A:
		return base.List[num];
		IL_C4:
		return null;
	}

	// Token: 0x06003BF0 RID: 15344 RVA: 0x00217848 File Offset: 0x00216848
	public IWorksheet ᜇ()
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
		return null;
	}

	// Token: 0x06003BF1 RID: 15345 RVA: 0x00217884 File Offset: 0x00216884
	int INameRanges.ᜄ()
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
		return this.ᜀ();
	}

	// Token: 0x06003BF2 RID: 15346 RVA: 0x002178C8 File Offset: 0x002168C8
	public int ᜊ()
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
		return base.List.Count;
	}

	// Token: 0x06003BF3 RID: 15347 RVA: 0x00217910 File Offset: 0x00216910
	public new INamedRange ᜁ(string A_0)
	{
		int a_ = 10;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6A;
			case 1:
				goto IL_3C;
			case 3:
				if (A_0.Length != 0)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_A0;
					}
				}
				num = 0;
				continue;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 1;
			}
			else
			{
				num = 3;
			}
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("⸿⍁⥃⍅", a_));
		IL_6A:
		throw new ArgumentException(RecordTableEnumerator.b("⸿⍁⥃⍅", a_));
		IL_A0:
		if (false)
		{
		}
		XlsName xlsName = new XlsName(base.ReservedHandle, this.ᜁ, A_0, base.List.Count);
		this.ᜁ(xlsName);
		return xlsName;
	}

	// Token: 0x06003BF4 RID: 15348 RVA: 0x002179EC File Offset: 0x002169EC
	protected virtual void ᜅ()
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
		this.ᜀ.Clear();
	}

	// Token: 0x06003BF5 RID: 15349 RVA: 0x00217A34 File Offset: 0x00216A34
	public new INamedRange ᜀ(string A_0, IXLSRange A_1)
	{
		int a_ = 19;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.Length == 0)
				{
					num = 5;
					continue;
				}
				goto IL_DD;
			case 2:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				num = 0;
				continue;
			case 3:
				goto IL_DB;
			case 4:
				goto IL_44;
			case 5:
				goto IL_68;
			}
			IL_31:
			if (A_0 == null)
			{
				if (true)
				{
				}
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
				num = 2;
				continue;
			}
			goto IL_31;
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("❈⩊⁌⩎", a_));
		IL_68:
		throw new ArgumentException(RecordTableEnumerator.b("❈⩊⁌⩎", a_));
		IL_DB:
		throw new ArgumentNullException(RecordTableEnumerator.b("❈⩊⁌⩎㕐Œ㑔㥖㹘㹚", a_));
		IL_DD:
		XlsName xlsName = new XlsName(base.ReservedHandle, this, A_0, A_1, base.List.Count, true);
		this.ᜁ(xlsName);
		return xlsName;
	}

	// Token: 0x06003BF6 RID: 15350 RVA: 0x00217B44 File Offset: 0x00216B44
	public new INamedRange ᜁ(INamedRange A_0)
	{
		int a_ = 14;
		for (;;)
		{
			XlsName xlsName = A_0 as XlsName;
			bool isExternName = xlsName.IsExternName;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_D2;
				case 1:
					if (true)
					{
					}
					if (!isExternName)
					{
						num = 4;
						continue;
					}
					goto IL_109;
				case 2:
					if (!xlsName.IsLocal)
					{
						num = 3;
						continue;
					}
					goto IL_109;
				case 3:
					num = 7;
					continue;
				case 4:
					num = 2;
					continue;
				case 5:
					if (!this.ᜁ.Loading)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D2;
						}
						if (false)
						{
						}
						num = 0;
						continue;
					}
					goto IL_109;
				case 6:
					goto IL_B0;
				case 7:
					if (this.ᜀ.ContainsKey(A_0.Name))
					{
						num = 6;
						continue;
					}
					goto IL_109;
				}
				break;
				IL_D2:
				num = 1;
			}
		}
		IL_B0:
		throw new ArgumentException(RecordTableEnumerator.b("੃❅╇⽉汋⅍㙏牑⁓㹕㵗穙ቛ㽝ൟݡ䑣॥੧i५൭ѯ剱ᥳ͵୷๹屻ᱽꊁﮉ曆뺏", a_));
		IL_109:
		this.ᜀ(A_0);
		this.ᜀ(true);
		return A_0;
	}

	// Token: 0x06003BF7 RID: 15351 RVA: 0x00217C6C File Offset: 0x00216C6C
	public new void ᜂ(string A_0)
	{
		if (true)
		{
		}
		int num = 0;
		for (;;)
		{
			INamedRange namedRange;
			switch (num)
			{
			case 1:
				this.ᜀ(namedRange.Index);
				num = 2;
				continue;
			case 2:
				return;
			}
			if (!this.ᜀ.TryGetValue(A_0, out namedRange))
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (false)
				{
				}
				num = 1;
				break;
			}
		}
	}

	// Token: 0x06003BF8 RID: 15352 RVA: 0x00217CF4 File Offset: 0x00216CF4
	public new void ᜀ(int A_0)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 5;
			Dictionary<int, int> dictionary;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					goto IL_151;
				case 2:
				{
					if (A_0 > this.ᜊ() - 1)
					{
						num = 4;
						continue;
					}
					INamedRange namedRange = this.ᜁ(A_0);
					this.ᜀ.Remove(namedRange.Name);
					IList<INamedRange> list = base.List;
					list.RemoveAt(A_0);
					this.ᜀ(true);
					dictionary = new Dictionary<int, int>();
					int num2 = A_0;
					int count = list.Count;
					num = 3;
					continue;
				}
				case 3:
					goto IL_115;
				case 4:
					goto IL_113;
				case 6:
					goto IL_115;
				case 7:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					IList<INamedRange> list;
					XlsName xlsName = (XlsName)list[num2];
					dictionary.Add(xlsName.Index, num2);
					xlsName.SetIndex(num2);
					num2++;
					if (true)
					{
					}
					num = 6;
					continue;
				}
				}
				IL_49:
				if (A_0 >= 0)
				{
					num = 0;
					continue;
				}
				break;
				IL_115:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_49;
				default:
					if (false)
					{
					}
					num = 7;
					break;
				}
			}
			IL_113:
			goto IL_153;
			IL_151:
			this.ᜁ.UpdateNamedRangeIndexes(dictionary);
			return;
			IL_153:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⽅♇⹉⥋㙍", a_), RecordTableEnumerator.b("၅⥇♉㥋⭍灏ㅑ㕓㡕㙗㕙⡛繝ɟݡ䑣੥൧ᥩὫ乭ѯᩱᕳᡵ塷䩹屻ώꒃ慎揄뒓ﮙ뺝쎟춡톣좥\udca7蒩", a_));
		}
		}
	}

	// Token: 0x06003BF9 RID: 15353 RVA: 0x00217E84 File Offset: 0x00216E84
	public new void ᜁ(int[] A_0)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			Dictionary<int, int> dictionary;
			for (;;)
			{
				List<int> list = new List<int>(A_0);
				list.Sort();
				int count = base.List.Count;
				int num = list.Count - 1;
				int num2 = 0;
				for (;;)
				{
					int num4;
					int num5;
					switch (num2)
					{
					case 0:
						goto IL_AD;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D1;
						default:
							if (false)
							{
							}
							num2 = 7;
							continue;
						}
						break;
					case 2:
						goto IL_D1;
					case 3:
					{
						if (num < 0)
						{
							num2 = 2;
							continue;
						}
						int num3 = list[num];
						num2 = 4;
						continue;
					}
					case 4:
					{
						int num3;
						if (num3 >= 0)
						{
							num2 = 1;
							continue;
						}
						goto IL_1EA;
					}
					case 5:
						goto IL_113;
					case 6:
						goto IL_113;
					case 7:
					{
						int num3;
						if (num3 >= count)
						{
							num2 = 11;
							continue;
						}
						INamedRange namedRange = base.List[num3];
						this.ᜀ.Remove(namedRange.Name);
						base.RemoveAt(num3);
						this.ᜀ(true);
						num--;
						num2 = 10;
						continue;
					}
					case 8:
						goto IL_131;
					case 9:
					{
						if (num4 >= num5)
						{
							num2 = 8;
							continue;
						}
						XlsName xlsName = (XlsName)base.List[num4];
						dictionary.Add(xlsName.Index, num4);
						xlsName.SetIndex(num4);
						num4++;
						num2 = 5;
						continue;
					}
					case 10:
						goto IL_AD;
					case 11:
						goto IL_10E;
					}
					break;
					IL_AD:
					if (true)
					{
					}
					num2 = 3;
					continue;
					IL_D1:
					dictionary = new Dictionary<int, int>();
					num4 = list[0];
					num5 = this.ᜊ();
					num2 = 6;
					continue;
					IL_113:
					num2 = 9;
				}
			}
			IL_10E:
			goto IL_1EA;
			IL_131:
			this.ᜁ.UpdateNamedRangeIndexes(dictionary);
			return;
			IL_1EA:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⅇ⑉⡋⭍⡏", a_));
		}
		}
	}

	// Token: 0x06003BFA RID: 15354 RVA: 0x0021809C File Offset: 0x0021709C
	public bool ᜄ(string A_0)
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
		return this.ᜀ.ContainsKey(A_0);
	}

	// Token: 0x06003BFB RID: 15355 RVA: 0x002180E4 File Offset: 0x002170E4
	public new void ᜁ(int A_0, int A_1, string A_2)
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
		this.ᜀ(A_2, A_0, false, true, A_1);
	}

	// Token: 0x06003BFC RID: 15356 RVA: 0x0021812C File Offset: 0x0021712C
	public new void ᜀ(int A_0, string A_1)
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
		this.ᜀ(A_1, A_0, true, true, 1);
	}

	// Token: 0x06003BFD RID: 15357 RVA: 0x00218174 File Offset: 0x00217174
	public new void ᜀ(int A_0, string A_1, int A_2)
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
		this.ᜀ(A_1, A_0, true, true, A_2);
	}

	// Token: 0x06003BFE RID: 15358 RVA: 0x002181BC File Offset: 0x002171BC
	public new void ᜀ(int A_0, int A_1, string A_2)
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
		this.ᜀ(A_2, A_0, false, false, A_1);
	}

	// Token: 0x06003BFF RID: 15359 RVA: 0x00218204 File Offset: 0x00217204
	public new void ᜁ(int A_0, string A_1)
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
		this.ᜁ(A_0, A_1, 1);
	}

	// Token: 0x06003C00 RID: 15360 RVA: 0x00218248 File Offset: 0x00217248
	public new void ᜁ(int A_0, string A_1, int A_2)
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
		this.ᜀ(A_1, A_0, true, false, A_2);
	}

	// Token: 0x06003C01 RID: 15361 RVA: 0x00218290 File Offset: 0x00217290
	internal new INamedRange ᜀ(sprῚ A_0)
	{
		int a_ = 16;
		while (A_0 != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				XlsName xlsName = new XlsName(base.ReservedHandle, this, A_0.ᜊ(), base.List.Count);
				this.ᜁ(xlsName);
				xlsName.ᜀ(A_0);
				return xlsName;
			}
			}
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("⡅⥇❉⥋", a_));
	}

	// Token: 0x06003C02 RID: 15362 RVA: 0x0021831C File Offset: 0x0021731C
	internal new void ᜀ(sprῚ[] A_0)
	{
		int a_ = 0;
		int num = 4;
		for (;;)
		{
			int num2;
			int num3;
			switch (num)
			{
			case 0:
				goto IL_3C;
			case 1:
				if (true)
				{
				}
				if (num2 >= num3)
				{
					num = 2;
					continue;
				}
				this.ᜀ(A_0[num2]);
				num2++;
				num = 3;
				continue;
			case 2:
				return;
			case 3:
				goto IL_96;
			case 5:
				goto IL_96;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num2 = 0;
			num3 = A_0.Length;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				if (false)
				{
				}
				num = 5;
				continue;
			}
			IL_96:
			num = 1;
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("堵夷圹夻䴽", a_));
	}

	// Token: 0x06003C03 RID: 15363 RVA: 0x002183F0 File Offset: 0x002173F0
	public new void ᜀ(RecordArrayList A_0)
	{
		int a_ = 0;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_EA;
			case 2:
				goto IL_99;
			case 3:
				if (A_0 != null)
				{
					this.ᜆ();
					int num2 = 0;
					int num3 = this.ᜊ();
					num = 2;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_49;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 4:
				return;
			case 5:
				if (true)
				{
				}
				break;
			case 6:
				goto IL_99;
			case 7:
			{
				int num2;
				int num3;
				if (num2 >= num3)
				{
					num = 4;
					continue;
				}
				XlsName xlsName = (XlsName)base.InnerList[num2];
				xlsName.ᜀ(A_0);
				num2++;
				num = 6;
				continue;
			}
			}
			goto IL_41;
			IL_49:
			num = 0;
			continue;
			IL_41:
			if (this.ᜊ() == 0)
			{
				goto IL_49;
			}
			num = 3;
			continue;
			IL_99:
			num = 7;
		}
		return;
		IL_EA:
		throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷夹医䰽␿ㅁ", a_));
	}

	// Token: 0x06003C04 RID: 15364 RVA: 0x00218508 File Offset: 0x00217508
	public new void ᜀ(INamedRange A_0)
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
		this.ᜀ(A_0, true);
	}

	// Token: 0x06003C05 RID: 15365 RVA: 0x0021854C File Offset: 0x0021754C
	public new void ᜀ(INamedRange A_0, bool A_1)
	{
		for (;;)
		{
			((XlsName)A_0).SetIndex(this.ᜊ());
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1)
					{
						goto IL_34;
					}
					base.InnerList.Add(A_0);
					num = 1;
					continue;
				case 1:
					goto IL_78;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34;
					default:
						if (false)
						{
						}
						base.Add(A_0);
						num = 3;
						continue;
					}
					break;
				case 3:
					goto IL_8C;
				}
				break;
				IL_34:
				num = 2;
			}
		}
		IL_78:
		IL_8C:
		if (true)
		{
		}
		this.ᜀ(true);
	}

	// Token: 0x06003C06 RID: 15366 RVA: 0x002185F8 File Offset: 0x002175F8
	internal void ᜆ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
			{
				SortedList<string, object> a_ = this.ᜃ();
				int[] a_2 = this.ᜀ(a_);
				this.ᜀ(a_2);
				this.ᜂ();
				this.ᜀ(false);
				num = 2;
				continue;
			}
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_79;
				}
				break;
			}
			IL_1C:
			if (this.ᜂ)
			{
				num = 1;
				continue;
			}
			return;
			goto IL_1C;
		}
		IL_79:
		if (false)
		{
		}
		if (true)
		{
		}
	}

	// Token: 0x06003C07 RID: 15367 RVA: 0x00218690 File Offset: 0x00217690
	private SortedList<string, object> ᜃ()
	{
		switch (0)
		{
		default:
		{
			SortedList<string, object> sortedList;
			for (;;)
			{
				IWorksheets worksheets = this.ᜁ.Worksheets;
				int count = worksheets.Count;
				sortedList = new SortedList<string, object>(count);
				int num = 0;
				if (true)
				{
				}
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_56;
					case 1:
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						sortedList.Add(worksheets[num].Name, null);
						num++;
						num2 = 0;
						continue;
					case 2:
						return sortedList;
					case 3:
						IL_54:
						goto IL_56;
					}
					break;
					IL_56:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_54;
					default:
						if (false)
						{
						}
						num2 = 1;
						break;
					}
				}
			}
			return sortedList;
		}
		}
	}

	// Token: 0x06003C08 RID: 15368 RVA: 0x0021875C File Offset: 0x0021775C
	private new int[] ᜀ(SortedList<string, object> A_0)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 16;
			for (;;)
			{
				bool[] array;
				int num2;
				int index;
				int num3;
				int[] array2;
				List<INamedRange> list;
				string a_2;
				int num4;
				int num5;
				switch (num)
				{
				case 0:
					goto IL_162;
				case 1:
					goto IL_162;
				case 2:
					if (!array[num2])
					{
						num = 8;
						continue;
					}
					goto IL_C3;
				case 3:
					goto IL_20F;
				case 4:
				{
					XlsName xlsName;
					index = xlsName.Index;
					num = 6;
					continue;
				}
				case 5:
				{
					XlsName xlsName;
					base.InnerList[num3] = xlsName;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_142;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				}
				case 6:
					if (!array[index])
					{
						num = 13;
						continue;
					}
					goto IL_82;
				case 7:
					return array2;
				case 8:
				{
					XlsName xlsName2 = (XlsName)list[num2];
					XlsWorksheet worksheet = xlsName2.Worksheet;
					SortedList<string, XlsName> sortedList = this.ᜀ(xlsName2, a_2);
					int count = sortedList.Count;
					IList<XlsName> values = sortedList.Values;
					num4 = 0;
					num = 3;
					continue;
				}
				case 9:
					if (num2 >= num5)
					{
						num = 7;
						continue;
					}
					num = 2;
					continue;
				case 10:
					goto IL_82;
				case 11:
					if (num3 < base.InnerList.Count)
					{
						num = 5;
						continue;
					}
					goto IL_96;
				case 12:
					goto IL_7D;
				case 13:
					goto IL_142;
				case 14:
					goto IL_C3;
				case 15:
				{
					if (true)
					{
					}
					int count;
					if (num4 >= count)
					{
						num = 14;
						continue;
					}
					IList<XlsName> values;
					XlsName xlsName = values[num4];
					num = 11;
					continue;
				}
				case 17:
					goto IL_20F;
				}
				if (A_0 == null)
				{
					num = 12;
					continue;
				}
				num5 = this.ᜊ();
				list = new List<INamedRange>(base.InnerList);
				array2 = new int[num5];
				num3 = 0;
				array = new bool[num5];
				int count2 = A_0.Count;
				string str = A_0.Keys[count2 - 1];
				a_2 = str + RecordTableEnumerator.b("昸਺", a_);
				num2 = 0;
				num = 0;
				continue;
				IL_82:
				num4++;
				num = 17;
				continue;
				IL_C3:
				num2++;
				num = 1;
				continue;
				IL_142:
				array2[index] = num3;
				array[index] = true;
				num3++;
				num = 10;
				continue;
				IL_162:
				num = 9;
				continue;
				IL_20F:
				num = 15;
			}
			IL_7D:
			throw new ArgumentNullException(RecordTableEnumerator.b("唸刺丼䬾", a_));
			IL_96:
			throw new ApplicationException();
		}
		}
	}

	// Token: 0x06003C09 RID: 15369 RVA: 0x00218A24 File Offset: 0x00217A24
	private new SortedList<string, XlsName> ᜀ(XlsName A_0, string A_1)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 8;
			SortedList<string, XlsName> sortedList;
			for (;;)
			{
				IWorksheet worksheet;
				string name;
				int num2;
				int count;
				IWorksheets worksheets;
				int num3;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					goto IL_94;
				case 1:
				{
					IWorksheet worksheet2;
					if (worksheet != worksheet2)
					{
						num = 9;
						continue;
					}
					goto IL_94;
				}
				case 2:
					goto IL_6F;
				case 3:
					this.ᜀ(sortedList, this.ᜁ.Names, name, A_1);
					A_1 = worksheet.Name;
					num = 4;
					continue;
				case 4:
					goto IL_136;
				case 5:
					goto IL_74;
				case 6:
				{
					if (num2 >= count)
					{
						num = 7;
						continue;
					}
					IWorksheet worksheet2 = worksheets[num2];
					num = 1;
					continue;
				}
				case 7:
					num = 10;
					continue;
				case 9:
				{
					IWorksheet worksheet2;
					this.ᜀ(sortedList, (worksheet2 as XlsWorksheet).Names, name, worksheet2.Name);
					num = 0;
					continue;
				}
				case 10:
					if (num3 != 0)
					{
						num = 3;
						continue;
					}
					goto IL_1CD;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9A;
					default:
						if (false)
						{
						}
						goto IL_74;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				sortedList = new SortedList<string, XlsName>();
				name = A_0.Name;
				worksheets = this.ᜁ.Worksheets;
				num3 = (int)A_0.Record.ᜃ();
				worksheet = A_0.Worksheet;
				num2 = 0;
				count = worksheets.Count;
				num = 5;
				continue;
				IL_74:
				num = 6;
				continue;
				IL_9A:
				num = 11;
				continue;
				IL_94:
				num2++;
				goto IL_9A;
			}
			IL_6F:
			throw new ArgumentNullException(RecordTableEnumerator.b("ⱁ╃⭅ⵇ", a_));
			IL_136:
			IL_1CD:
			sortedList.Add(A_1, A_0);
			return sortedList;
		}
		}
	}

	// Token: 0x06003C0A RID: 15370 RVA: 0x00218C08 File Offset: 0x00217C08
	private new void ᜀ(SortedList<string, XlsName> A_0, INameRanges A_1, string A_2, string A_3)
	{
		int a_ = 13;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_FD;
			case 1:
				goto IL_116;
			case 2:
				goto IL_157;
			case 3:
				if (A_2 == null)
				{
					num = 2;
					continue;
				}
				num = 10;
				continue;
			case 4:
			{
				XlsName xlsName;
				A_0.Add(A_3, xlsName);
				num = 1;
				continue;
			}
			case 5:
			{
				XlsName xlsName;
				if (xlsName != null)
				{
					num = 4;
					continue;
				}
				return;
			}
			case 7:
				if (A_1 == null)
				{
					num = 8;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_CB;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 8:
				goto IL_9B;
			case 9:
				goto IL_58;
			case 10:
			{
				if (A_2.Length == 0)
				{
					num = 0;
					continue;
				}
				XlsName xlsName = (XlsName)A_1[A_2];
				num = 5;
				continue;
			}
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 9;
			}
			else
			{
				num = 7;
			}
		}
		IL_58:
		goto IL_CB;
		IL_9B:
		throw new ArgumentNullException(RecordTableEnumerator.b("ⵂ⑄⩆ⱈ㡊", a_));
		IL_CB:
		throw new ArgumentNullException(RecordTableEnumerator.b("⽂ⱄ㑆㵈", a_));
		IL_FD:
		throw new ArgumentException(RecordTableEnumerator.b("ⵂ⑄⩆ⱈ歊⹌⹎㽐㵒㩔⍖祘㥚㡜罞Ѡ๢ᕤ፦ၨ", a_));
		IL_116:
		return;
		IL_157:
		throw new ArgumentNullException(RecordTableEnumerator.b("ⵂ⑄⩆ⱈ", a_));
	}

	// Token: 0x06003C0B RID: 15371 RVA: 0x00218D88 File Offset: 0x00217D88
	private new void ᜀ(int[] A_0)
	{
		int a_ = 10;
		for (;;)
		{
			if (true)
			{
			}
			if (A_0 == null)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_40;
			}
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("ℿぁ㙃ࡅⵇ㵉Ջ⁍㑏㝑ⱓ", a_));
		IL_40:
		if (false)
		{
		}
		this.ᜁ.UpdateNamedRangeIndexes(A_0);
	}

	// Token: 0x06003C0C RID: 15372 RVA: 0x00218DF4 File Offset: 0x00217DF4
	private new void ᜂ()
	{
		for (;;)
		{
			IL_34:
			int num = 0;
			int num2 = this.ᜊ();
			int num3 = 0;
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
					switch (num3)
					{
					case 0:
						goto IL_47;
					case 1:
						return;
					case 2:
					{
						if (num >= num2)
						{
							if (true)
							{
							}
							num3 = 1;
							continue;
						}
						XlsName xlsName = (XlsName)base.InnerList[num];
						xlsName.SetIndex(num, false);
						num++;
						num3 = 3;
						continue;
					}
					case 3:
						goto IL_47;
					}
					goto IL_34;
				}
				IL_47:
				num3 = 2;
			}
		}
	}

	// Token: 0x06003C0D RID: 15373 RVA: 0x00218E9C File Offset: 0x00217E9C
	public void ᜈ()
	{
		for (;;)
		{
			IL_3C:
			int num = 0;
			int num2 = this.ᜊ();
			int num3 = 3;
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
					if (true)
					{
					}
					switch (num3)
					{
					case 0:
						return;
					case 1:
						goto IL_4F;
					case 2:
					{
						if (num >= num2)
						{
							num3 = 0;
							continue;
						}
						spr\u1D46 spr_u1D = (spr\u1D46)this.ᜁ(num);
						spr_u1D.ᜀ();
						num++;
						num3 = 1;
						continue;
					}
					case 3:
						goto IL_4F;
					}
					goto IL_3C;
				}
				IL_4F:
				num3 = 2;
			}
		}
	}

	// Token: 0x06003C0E RID: 15374 RVA: 0x00218F3C File Offset: 0x00217F3C
	public new int ᜀ(string A_0)
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
		sprῚ sprῚ = (sprῚ)spr\u175E.ᜀ(TBIFFRecord.Name);
		sprῚ.ᜅ(true);
		sprῚ.ᜆ(true);
		sprῚ.ᜄ(true);
		sprῚ.ᜆ(A_0);
		INamedRange namedRange = this.ᜀ(sprῚ);
		return namedRange.Index;
	}

	// Token: 0x06003C0F RID: 15375 RVA: 0x00218FB0 File Offset: 0x00217FB0
	internal new sprῚ ᜂ(int A_0)
	{
		int a_ = 4;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (A_0 >= this.ᜊ())
				{
					num = 3;
					continue;
				}
				goto IL_94;
			case 2:
				num = 1;
				continue;
			case 3:
				goto IL_92;
			}
			if (A_0 < 0)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_94;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				num = 2;
				break;
			}
		}
		IL_5B:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("匹刻娽┿㩁", a_));
		IL_92:
		goto IL_5B;
		IL_94:
		XlsName xlsName = (XlsName)this.ᜁ(A_0);
		return xlsName.Record;
	}

	// Token: 0x06003C10 RID: 15376 RVA: 0x00219064 File Offset: 0x00218064
	public new INamedRange ᜀ(INamedRange A_0, IWorksheet A_1, Dictionary<int, int> A_2, IDictionary A_3)
	{
		int a_ = 6;
		INamedRange result;
		for (;;)
		{
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (A_1 == null)
						{
							num = 6;
							continue;
						}
						string name = A_0.Name;
						result = null;
						this.ᜁ.AddSheetReference(A_1);
						XlsName xlsName = (XlsName)A_0;
						sprῚ sprῚ = xlsName.Record.Clone() as sprῚ;
						XlsWorkbook a_2 = xlsName.Workbook;
						sprᤗ.ᜀ(sprῚ, a_2, A_3, A_2, this.ᜁ);
						num = 2;
						continue;
					}
					case 2:
					{
						string name;
						if (this.ᜄ(name))
						{
							num = 5;
							continue;
						}
						sprῚ sprῚ;
						result = this.ᜀ(sprῚ);
						num = 4;
						continue;
					}
					case 3:
						goto IL_BF;
					case 4:
						goto IL_100;
					case 5:
					{
						sprῚ sprῚ;
						sprῚ.ᜀ((ushort)(A_1.Index + 1));
						sprᤗ sprᤗ = (sprᤗ)((XlsWorksheet)A_1).Names;
						result = sprᤗ.ᜀ(sprῚ, false);
						num = 3;
						continue;
					}
					case 6:
						goto IL_120;
					case 7:
						goto IL_60;
					}
					if (true)
					{
					}
					if (A_0 == null)
					{
						num = 7;
					}
					else
					{
						num = 0;
					}
				}
				IL_60:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_80;
				}
				break;
			}
			}
		}
		IL_80:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("刻弽ⴿ❁၃⥅େ╉㱋㝍", a_));
		IL_BF:
		IL_100:
		return result;
		IL_120:
		throw new ArgumentNullException(RecordTableEnumerator.b("堻嬽㌿㙁ᝃ⹅ⵇ⽉㡋", a_));
	}

	// Token: 0x06003C11 RID: 15377 RVA: 0x00219204 File Offset: 0x00218204
	private new void ᜀ(sprῚ A_0, int A_1)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				int num2;
				int num3;
				Ptg[] array;
				switch (num)
				{
				case 0:
				{
					if (num2 >= num3)
					{
						num = 1;
						continue;
					}
					Ptg ptg = array[num2];
					sprẄ sprẄ = ptg as sprẄ;
					num = 4;
					continue;
				}
				case 1:
					return;
				case 3:
					goto IL_AA;
				case 4:
				{
					sprẄ sprẄ;
					if (sprẄ != null)
					{
						num = 8;
						continue;
					}
					goto IL_5B;
				}
				case 5:
					goto IL_AA;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_59;
					default:
						if (false)
						{
						}
						goto IL_5B;
					}
					break;
				case 7:
					goto IL_59;
				case 8:
				{
					if (true)
					{
					}
					sprẄ sprẄ;
					sprẄ.ᜂ((ushort)A_1);
					num = 6;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				array = A_0.ᜈ();
				num2 = 0;
				num3 = array.Length;
				num = 5;
				continue;
				IL_5B:
				num2++;
				num = 3;
				continue;
				IL_AA:
				num = 0;
			}
			IL_59:
			throw new ArgumentNullException(RecordTableEnumerator.b("堵夷圹夻", a_));
		}
		}
	}

	// Token: 0x06003C12 RID: 15378 RVA: 0x00219334 File Offset: 0x00218334
	public virtual object ᜀ(object A_0)
	{
		int a_ = 18;
		for (;;)
		{
			if (true)
			{
			}
			if (A_0 != null)
			{
				goto IL_50;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_34;
			}
		}
		IL_34:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㡇⭉㹋⭍㹏♑", a_));
		IL_50:
		sprឦ sprឦ = (sprឦ)base.Clone(A_0);
		sprឦ.ᜂ = this.ᜂ;
		return sprឦ;
	}

	// Token: 0x06003C13 RID: 15379 RVA: 0x002193AC File Offset: 0x002183AC
	protected virtual void ᜀ(int A_0, INamedRange A_1)
	{
		for (;;)
		{
			XlsName xlsName = (XlsName)A_1;
			base.OnInsertComplete(A_0, A_1);
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!xlsName.IsExternName)
					{
						num = 2;
						continue;
					}
					return;
				case 1:
					if (!xlsName.IsBuiltIn)
					{
						num = 4;
						continue;
					}
					return;
				case 2:
					goto IL_D5;
				case 3:
					if (true)
					{
					}
					this.ᜀ[xlsName.Name] = xlsName;
					num = 5;
					continue;
				case 4:
					num = 0;
					continue;
				case 5:
					return;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D5;
					default:
						if (false)
						{
						}
						if (!xlsName.IsLocal)
						{
							num = 3;
							continue;
						}
						return;
					}
					break;
				}
				break;
				IL_D5:
				num = 6;
			}
		}
	}

	// Token: 0x06003C14 RID: 15380 RVA: 0x00219490 File Offset: 0x00218490
	public new void ᜀ(ExcelVersion A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜊ();
			int num3 = 3;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					return;
				case 1:
					if (true)
					{
					}
					goto IL_2B;
				case 2:
					if (num >= num2)
					{
						num3 = 0;
						continue;
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
						XlsName xlsName = (XlsName)this.ᜁ(num);
						xlsName.ConvertFullRowColumnName(A_0);
						num++;
						num3 = 1;
						continue;
					}
					}
					break;
				case 3:
					goto IL_2B;
				}
				break;
				IL_2B:
				num3 = 2;
			}
		}
	}

	// Token: 0x06003C15 RID: 15381 RVA: 0x00219530 File Offset: 0x00218530
	public bool ᜉ()
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
		return this.ᜂ;
	}

	// Token: 0x06003C16 RID: 15382 RVA: 0x00219574 File Offset: 0x00218574
	public new void ᜀ(bool A_0)
	{
		for (;;)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					this.ᜂ = A_0;
					num = 1;
					continue;
				case 1:
					goto IL_4A;
				}
				if (this.ᜁ.Loading)
				{
					break;
				}
				num = 0;
			}
			IL_4C:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				goto IL_62;
			}
			IL_4A:
			goto IL_4C;
		}
		IL_62:
		if (false)
		{
		}
	}

	// Token: 0x06003C17 RID: 15383 RVA: 0x002195F4 File Offset: 0x002185F4
	private new void ᜁ()
	{
		int a_ = 11;
		for (;;)
		{
			this.ᜁ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
			if (this.ᜁ != null)
			{
				return;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_4C;
			}
		}
		IL_4C:
		if (false)
		{
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("ᅀ≂㝄≆❈㽊浌⁎㍐㥒ご㑖ⵘ筚㹜㹞འൢ੤፦䥨४࡬佮ᝰᱲt᥶ᵸ啺", a_));
	}

	// Token: 0x06003C18 RID: 15384 RVA: 0x00219674 File Offset: 0x00218674
	private new void ᜀ(string A_0, int A_1, bool A_2, bool A_3, int A_4)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				int num2;
				Ptg[] array;
				int num4;
				XlsName xlsName;
				int num6;
				spr\u2590 spr_u;
				Ptg ptg;
				int num7;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_37B;
					default:
						if (false)
						{
						}
						goto IL_339;
					}
					break;
				case 1:
					goto IL_2D0;
				case 2:
				{
					sprẄ sprẄ;
					if (!this.ᜁ.IsExternalReference((int)sprẄ.ᜁ()))
					{
						num = 1;
						continue;
					}
					goto IL_1C6;
				}
				case 3:
					goto IL_3FC;
				case 4:
				{
					num2 = 0;
					int num3 = array.Length;
					num = 0;
					continue;
				}
				case 5:
					if (array != null)
					{
						num = 4;
						continue;
					}
					goto IL_22F;
				case 7:
				{
					int num5;
					if (num4 >= num5)
					{
						num = 23;
						continue;
					}
					List<INamedRange> innerList;
					xlsName = (XlsName)innerList[num4];
					sprῚ sprῚ = xlsName.Record;
					array = sprῚ.ᜈ();
					num = 5;
					continue;
				}
				case 8:
					num6 = this.ᜁ.MaxRowCount;
					goto IL_284;
				case 9:
					goto IL_380;
				case 10:
					ptg = spr_u.ᜀ();
					num = 27;
					continue;
				case 11:
				{
					int num3;
					if (num2 >= num3)
					{
						num = 28;
						continue;
					}
					spr_u = (array[num2] as spr\u2590);
					num = 24;
					continue;
				}
				case 12:
					num6 = this.ᜁ.MaxColumnCount;
					goto IL_284;
				case 13:
					num = 12;
					continue;
				case 14:
				{
					sprẄ sprẄ = spr_u as sprẄ;
					num = 26;
					continue;
				}
				case 15:
					if (ptg == null)
					{
						num = 10;
						continue;
					}
					goto IL_26C;
				case 16:
					goto IL_FF;
				case 17:
					num = 2;
					continue;
				case 18:
					if (A_1 > num7)
					{
						num = 32;
						continue;
					}
					num = 21;
					continue;
				case 19:
					goto IL_339;
				case 20:
					if (true)
					{
					}
					num = 18;
					continue;
				case 21:
					if (A_0 == null)
					{
						num = 16;
						continue;
					}
					num = 34;
					continue;
				case 22:
					goto IL_22F;
				case 23:
					return;
				case 24:
					if (spr_u != null)
					{
						num = 14;
						continue;
					}
					goto IL_1C6;
				case 25:
					if (A_1 >= 1)
					{
						num = 20;
						continue;
					}
					goto IL_258;
				case 26:
				{
					sprẄ sprẄ;
					if (sprẄ != null)
					{
						num = 17;
						continue;
					}
					goto IL_2D0;
				}
				case 27:
					goto IL_37B;
				case 28:
				{
					sprῚ sprῚ;
					sprῚ.ᜀ(array);
					num = 22;
					continue;
				}
				case 29:
					goto IL_380;
				case 30:
					goto IL_1C6;
				case 31:
				{
					if (A_4 == 0)
					{
						num = 33;
						continue;
					}
					this.ᜀ(true);
					A_1--;
					num4 = 0;
					int count;
					int num5 = count;
					num = 9;
					continue;
				}
				case 32:
					goto IL_214;
				case 33:
					return;
				case 34:
				{
					if (A_0.Length == 0)
					{
						num = 3;
						continue;
					}
					List<INamedRange> innerList = base.InnerList;
					int count = innerList.Count;
					num = 31;
					continue;
				}
				}
				if (!A_3)
				{
					num = 13;
					continue;
				}
				num = 8;
				continue;
				IL_1C6:
				num2++;
				num = 19;
				continue;
				IL_22F:
				num4++;
				num = 29;
				continue;
				IL_26C:
				array[num2] = ptg;
				num = 30;
				continue;
				IL_37B:
				goto IL_26C;
				IL_284:
				num7 = num6;
				num = 25;
				continue;
				IL_2D0:
				ptg = this.ᜀ(spr_u, A_0, A_1, A_2, A_3, A_4, xlsName.Worksheet);
				num = 15;
				continue;
				IL_339:
				num = 11;
				continue;
				IL_380:
				num = 7;
			}
			IL_FF:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬷丹主洽⠿❁⅃㉅ه⭉⅋⭍", a_));
			IL_214:
			IL_258:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("儷吹堻嬽㠿", a_));
			IL_3FC:
			throw new ArgumentException(RecordTableEnumerator.b("䬷丹主洽⠿❁⅃㉅ه⭉⅋⭍", a_));
		}
		}
	}

	// Token: 0x06003C19 RID: 15385 RVA: 0x00219AB0 File Offset: 0x00218AB0
	private new Ptg ᜀ(spr\u2590 A_0, string A_1, int A_2, bool A_3, bool A_4, int A_5, IWorksheet A_6)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 11;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					Ptg result;
					return result;
				}
				default:
				{
					if (false)
					{
					}
					Ptg result;
					IXLSRange ixlsrange;
					spr\u25A6.ᜀ ᜀ;
					spr\u25A6.ᜀ ᜀ2;
					Ptg ptg;
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						if (ixlsrange.Worksheet.Name == A_1)
						{
							num = 9;
							continue;
						}
						return result;
					case 2:
						if (ᜀ == null)
						{
							num = 8;
							continue;
						}
						num = 14;
						continue;
					case 3:
						if (!A_4)
						{
							num = 6;
							continue;
						}
						num = 4;
						continue;
					case 4:
					{
						spr\u25A6.ᜀ a_2;
						ᜀ2 = spr\u1FBC.ᜁ(a_2, A_2, A_3, A_5, this.ᜁ);
						goto IL_13E;
					}
					case 5:
						goto IL_8D;
					case 6:
						num = 12;
						continue;
					case 7:
						return result;
					case 8:
						num = 10;
						continue;
					case 9:
					{
						Rectangle rectangle = A_0.ᜀ();
						spr\u25A6.ᜀ a_2 = new spr\u25A6.ᜀ(rectangle.Top, rectangle.Bottom, rectangle.Left, rectangle.Right);
						result = null;
						num = 3;
						continue;
					}
					case 10:
						ptg = null;
						goto IL_15F;
					case 12:
					{
						spr\u25A6.ᜀ a_2;
						ᜀ2 = spr\u1FBC.ᜀ(a_2, A_2, A_3, A_5, this.ᜁ);
						goto IL_13E;
					}
					case 13:
						if (ixlsrange != null)
						{
							num = 0;
							continue;
						}
						return result;
					case 14:
						if (true)
						{
						}
						ptg = (result = A_0.ᜀ(ᜀ.ᜁ()));
						goto IL_15F;
					}
					if (A_0 == null)
					{
						num = 5;
						break;
					}
					ᜀ = null;
					result = (Ptg)A_0;
					ixlsrange = A_0.ᜀ(this.ᜁ, A_6);
					num = 13;
					break;
					IL_13E:
					ᜀ = ᜀ2;
					num = 2;
					break;
					IL_15F:
					result = ptg;
					num = 7;
					break;
				}
				}
			}
			IL_8D:
			throw new ArgumentNullException(RecordTableEnumerator.b("㉅❇ⅉ⥋⁍", a_));
		}
		}
	}

	// Token: 0x06003C1A RID: 15386 RVA: 0x00219CCC File Offset: 0x00218CCC
	internal new void ᜀ(bool[] A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				List<INamedRange> innerList = base.InnerList;
				int num = 0;
				int num2 = this.ᜊ();
				int num3 = 0;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_4A;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4A;
						default:
							if (false)
							{
							}
							goto IL_4C;
						}
						break;
					case 2:
						return;
					case 3:
					{
						if (num >= num2)
						{
							num3 = 2;
							continue;
						}
						XlsName xlsName = (XlsName)innerList[num];
						sprῚ sprῚ = xlsName.Record;
						Ptg[] a_ = sprῚ.ᜈ();
						FormulaUtil.ᜀ(a_, A_0);
						num++;
						num3 = 1;
						continue;
					}
					}
					break;
					IL_4C:
					if (true)
					{
					}
					num3 = 3;
					continue;
					IL_4A:
					goto IL_4C;
				}
			}
			return;
		}
	}

	// Token: 0x06003C1B RID: 15387 RVA: 0x00219D9C File Offset: 0x00218D9C
	internal new void ᜂ(int[] A_0)
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
			switch (0)
			{
			default:
				for (;;)
				{
					List<INamedRange> innerList = base.InnerList;
					int num = 0;
					int num2 = this.ᜊ();
					int num3 = 6;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_E1;
						case 1:
						{
							sprῚ sprῚ;
							Ptg[] a_;
							sprῚ.ᜀ(a_);
							num3 = 4;
							continue;
						}
						case 2:
						{
							if (num >= num2)
							{
								num3 = 3;
								continue;
							}
							XlsName xlsName = (XlsName)innerList[num];
							sprῚ sprῚ = xlsName.Record;
							Ptg[] a_ = sprῚ.ᜈ();
							num3 = 5;
							continue;
						}
						case 3:
							return;
						case 4:
							goto IL_74;
						case 5:
						{
							Ptg[] a_;
							if (FormulaUtil.ᜀ(a_, A_0))
							{
								num3 = 1;
								continue;
							}
							goto IL_74;
						}
						case 6:
							goto IL_E1;
						}
						break;
						IL_74:
						if (true)
						{
						}
						num++;
						num3 = 0;
						continue;
						IL_E1:
						num3 = 2;
					}
				}
				break;
			}
			break;
		}
	}

	// Token: 0x06003C1C RID: 15388 RVA: 0x00219EA8 File Offset: 0x00218EA8
	private new int ᜀ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return num;
		}
		if (true)
		{
		}
		if (false)
		{
		}
		num = 0;
		IEnumerator<INamedRange> enumerator = base.List.GetEnumerator();
		try
		{
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 2:
				{
					INamedRange namedRange;
					if (namedRange.RefersToRange != null)
					{
						num2 = 6;
						continue;
					}
					break;
				}
				case 3:
					num2 = 5;
					continue;
				case 4:
				{
					if (!enumerator.MoveNext())
					{
						num2 = 3;
						continue;
					}
					INamedRange namedRange = enumerator.Current;
					num2 = 2;
					continue;
				}
				case 5:
					goto IL_BD;
				case 6:
					num++;
					num2 = 1;
					continue;
				}
				IL_8D:
				num2 = 4;
				continue;
				goto IL_8D;
			}
			IL_BD:;
		}
		finally
		{
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_F6;
				case 2:
					enumerator.Dispose();
					num2 = 0;
					continue;
				}
				if (enumerator == null)
				{
					break;
				}
				num2 = 2;
			}
			IL_F6:;
		}
		return num;
	}

	// Token: 0x04001A0B RID: 6667
	private new Dictionary<string, INamedRange> ᜀ = new Dictionary<string, INamedRange>();

	// Token: 0x04001A0C RID: 6668
	private new XlsWorkbook ᜁ;

	// Token: 0x04001A0D RID: 6669
	private new bool ᜂ;
}
