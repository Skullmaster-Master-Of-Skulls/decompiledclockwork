using System;
using System.Collections.Generic;
using System.Reflection;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000257 RID: 599
[DefaultMember("Item")]
internal class sprᭆ : CollectionExtended<sprἉ>
{
	// Token: 0x060023D9 RID: 9177 RVA: 0x0014E3E0 File Offset: 0x0014D3E0
	internal sprᭆ(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
		this.ᜀ();
		base.Removed += this.ᜁ;
		base.Inserted += this.ᜀ;
	}

	// Token: 0x060023DA RID: 9178 RVA: 0x0014E438 File Offset: 0x0014D438
	private new void ᜀ()
	{
		int a_ = 5;
		for (;;)
		{
			this.ᜀ = (base.FindParent(typeof(XlsExternWorkbook)) as XlsExternWorkbook);
			if (this.ᜀ == null)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_6A;
			}
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("砺尼儾晀㝂敄ⅆ⁈╊⥌潎⅐㉒❔㉖㝘⽚絜⡞๠ᅢ๤զ٨Ѫ٬䅮", a_));
		IL_6A:
		if (false)
		{
		}
	}

	// Token: 0x060023DB RID: 9179 RVA: 0x0014E4B8 File Offset: 0x0014D4B8
	public new sprἉ ᜀ(int A_0)
	{
		int a_ = 6;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6B;
				default:
					if (false)
					{
					}
					if (A_0 > base.Count)
					{
						num = 1;
						continue;
					}
					goto IL_A5;
				}
				break;
			case 1:
				goto IL_A3;
			case 2:
				goto IL_6B;
			}
			if (A_0 >= 0)
			{
				if (true)
				{
				}
				num = 2;
				continue;
			}
			break;
			IL_6B:
			num = 0;
		}
		IL_49:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("唻倽␿❁㱃", a_), RecordTableEnumerator.b("樻弽ⰿ㝁⅃晅⭇⭉≋⁍㽏♑瑓㑕㵗穙せ㭝፟ᅡ䑣ብg୩ɫ乭䁯剱ᕳᡵᱷ婹᭻౽慎ꪉﲑ뒓햕", a_));
		IL_A3:
		goto IL_49;
		IL_A5:
		return base.List[A_0];
	}

	// Token: 0x060023DC RID: 9180 RVA: 0x0014E578 File Offset: 0x0014D578
	public new sprἉ ᜁ(string A_0)
	{
		int num;
		for (;;)
		{
			IL_18:
			num = this.ᜂ(A_0);
			for (;;)
			{
				IL_20:
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						if (num <= base.Count)
						{
							num2 = 3;
							continue;
						}
						goto IL_8F;
					case 1:
						if (num >= 0)
						{
							num2 = 2;
							continue;
						}
						goto IL_8F;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_20;
						default:
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
						break;
					case 3:
						goto IL_8D;
					}
					goto IL_18;
				}
			}
		}
		IL_8D:
		return base.List[num];
		IL_8F:
		return null;
	}

	// Token: 0x060023DD RID: 9181 RVA: 0x0014E618 File Offset: 0x0014D618
	public new XlsExternWorkbook ᜁ()
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
		return this.ᜀ;
	}

	// Token: 0x060023DE RID: 9182 RVA: 0x0014E65C File Offset: 0x0014D65C
	internal new int ᜀ(spr\u2141 A_0)
	{
		int a_ = 0;
		int num = 0;
		XlsWorkbook workbook;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_89;
			case 2:
				num = 1;
				continue;
			case 3:
				if (workbook.Loading)
				{
					num = 7;
					continue;
				}
				goto IL_BB;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_89;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					if (!A_0.NeedDataArray)
					{
						num = 2;
						continue;
					}
					goto IL_145;
				}
				break;
			case 5:
				num = 3;
				continue;
			case 6:
				goto IL_47;
			case 7:
				goto IL_140;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			workbook = this.ᜀ.Workbook;
			num = 4;
			continue;
			IL_89:
			if (!this.ᜀ(A_0.ᜌ()))
			{
				goto IL_145;
			}
			num = 5;
		}
		IL_47:
		throw new ArgumentNullException(RecordTableEnumerator.b("堵夷圹夻", a_));
		IL_BB:
		throw new ApplicationException(RecordTableEnumerator.b("爵䴷䨹倻圽⌿⍁ぃ⍅桇⑉ⵋ⍍㕏⅑", a_));
		IL_140:
		int key = base.List.Count + this.ᜂ.Count;
		this.ᜂ.Add(key, null);
		workbook.HasDuplicatedNames = true;
		return -1;
		IL_145:
		sprἉ item = new sprἉ(base.ReservedHandle, this, A_0, base.List.Count);
		base.Add(item);
		return base.Count - 1;
	}

	// Token: 0x060023DF RID: 9183 RVA: 0x0014E7D8 File Offset: 0x0014D7D8
	public int ᜃ(string A_0)
	{
		int a_ = 2;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_90;
			case 1:
				goto IL_46;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (A_0.Length == 0)
					{
						num = 0;
						continue;
					}
					goto IL_A6;
				}
				break;
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
		IL_46:
		throw new ArgumentNullException(RecordTableEnumerator.b("嘷嬹儻嬽", a_));
		IL_90:
		throw new ArgumentException(RecordTableEnumerator.b("嘷嬹儻嬽怿潁摃㕅㱇㡉╋⁍㝏牑㝓㝕㙗㑙㍛⩝䁟aţ䙥൧ݩᱫᩭ९", a_));
		IL_A6:
		spr\u2141 spr_u = (spr\u2141)spr\u175E.ᜀ(TBIFFRecord.ExternName);
		spr_u.ᜀ(A_0);
		return this.ᜀ(spr_u);
	}

	// Token: 0x060023E0 RID: 9184 RVA: 0x0014E8A8 File Offset: 0x0014D8A8
	public new bool ᜀ(string A_0)
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
		return this.ᜁ.ContainsKey(A_0);
	}

	// Token: 0x060023E1 RID: 9185 RVA: 0x0014E8F0 File Offset: 0x0014D8F0
	public new void ᜀ(RecordArrayList A_0)
	{
		int a_ = 11;
		int num = 0;
		for (;;)
		{
			int num2;
			int count;
			switch (num)
			{
			case 1:
				goto IL_3C;
			case 2:
				return;
			case 3:
				goto IL_92;
			case 4:
				goto IL_92;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_92;
				default:
					if (false)
					{
					}
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					this.ᜀ(num2).ᜀ(A_0);
					num2++;
					num = 3;
					continue;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num2 = 0;
			count = base.Count;
			num = 4;
			continue;
			IL_92:
			num = 5;
		}
		IL_3C:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂♄⡆㭈⽊㹌", a_));
	}

	// Token: 0x060023E2 RID: 9186 RVA: 0x0014E9C8 File Offset: 0x0014D9C8
	public new int ᜂ(string A_0)
	{
		while (this.ᜁ.ContainsKey(A_0))
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
				sprἉ sprἉ = this.ᜁ[A_0];
				return sprἉ.ᜁ();
			}
			}
		}
		if (true)
		{
		}
		return -1;
	}

	// Token: 0x060023E3 RID: 9187 RVA: 0x0014EA28 File Offset: 0x0014DA28
	public new int ᜁ(int A_0)
	{
		int num;
		for (;;)
		{
			num = this.ᜂ.IndexOfKey(A_0);
			if (num != -1)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_39;
			}
		}
		return A_0 - num - 1;
		IL_39:
		if (true)
		{
		}
		if (false)
		{
		}
		return A_0;
	}

	// Token: 0x060023E4 RID: 9188 RVA: 0x0014EA80 File Offset: 0x0014DA80
	public virtual object ᜀ(object A_0)
	{
		switch (0)
		{
		default:
		{
			sprᭆ result;
			for (;;)
			{
				IL_0E:
				if (true)
				{
				}
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_0E;
					default:
					{
						if (false)
						{
						}
						result = (sprᭆ)base.Clone(A_0);
						IList<int> keys = this.ᜂ.Keys;
						int num = 0;
						int count = this.ᜂ.Count;
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_7D;
							case 1:
							{
								if (num >= count)
								{
									num2 = 2;
									continue;
								}
								int key = keys[num];
								this.ᜂ.Add(key, null);
								num++;
								num2 = 3;
								continue;
							}
							case 2:
								return result;
							case 3:
								goto IL_7D;
							}
							break;
							IL_7D:
							num2 = 1;
						}
						break;
					}
					}
				}
			}
			return result;
		}
		}
	}

	// Token: 0x060023E5 RID: 9189 RVA: 0x0014EB58 File Offset: 0x0014DB58
	private new int ᜀ(sprἉ A_0)
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
		base.Add(A_0);
		return base.Count - 1;
	}

	// Token: 0x060023E6 RID: 9190 RVA: 0x0014EBA4 File Offset: 0x0014DBA4
	private new void ᜁ(object A_0, CollectionChangeEventArgs<sprἉ> A_1)
	{
		for (;;)
		{
			int num = A_1.Index;
			int count = base.Count;
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= count)
					{
						goto IL_79;
					}
					this.ᜀ(num).ᜀ(num);
					num++;
					num2 = 2;
					continue;
				case 1:
				{
					sprἉ value = A_1.Value;
					if (true)
					{
					}
					num2 = 6;
					continue;
				}
				case 2:
					goto IL_6D;
				case 3:
				{
					sprἉ value;
					this.ᜁ.Remove(value.ᜃ());
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_79;
					}
					if (false)
					{
					}
					num2 = 5;
					continue;
				}
				case 4:
					goto IL_6D;
				case 5:
					return;
				case 6:
				{
					sprἉ value;
					if (!value.ᜄ().NeedDataArray)
					{
						num2 = 3;
						continue;
					}
					return;
				}
				}
				break;
				IL_6D:
				num2 = 0;
				continue;
				IL_79:
				num2 = 1;
			}
		}
	}

	// Token: 0x060023E7 RID: 9191 RVA: 0x0014EC98 File Offset: 0x0014DC98
	private new void ᜀ(object A_0, CollectionChangeEventArgs<sprἉ> A_1)
	{
		for (;;)
		{
			for (;;)
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
					sprἉ value = A_1.Value;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (!value.ᜄ().NeedDataArray)
							{
								num = 2;
								continue;
							}
							return;
						case 1:
							return;
						case 2:
							this.ᜁ.Add(value.ᜃ(), value);
							num = 1;
							continue;
						}
						break;
					}
					break;
				}
				}
			}
		}
	}

	// Token: 0x0400125E RID: 4702
	private new XlsExternWorkbook ᜀ;

	// Token: 0x0400125F RID: 4703
	private new Dictionary<string, sprἉ> ᜁ = new Dictionary<string, sprἉ>();

	// Token: 0x04001260 RID: 4704
	private new SortedList<int, object> ᜂ = new SortedList<int, object>();
}
