using System;
using System.Collections.Generic;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000437 RID: 1079
internal class sprឤ : spr\u20C3, spr\u2228
{
	// Token: 0x06004114 RID: 16660 RVA: 0x002465BC File Offset: 0x002455BC
	public sprឤ(spr\u2604 A_0, string A_1, int A_2)
	{
		int a_ = 14;
		this.ᜁ = new SortedList<string, spr\u2228>(new sprἝ());
		this.ᜃ = new List<string>();
		this.ᜄ = new List<string>();
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("㑃❅㩇⽉≋㩍", a_));
		}
		this.ᜀ = A_0;
		this.ᜂ = new spr\u1DAB(A_1, spr\u1DAB.EntryType.Storage, A_2);
	}

	// Token: 0x06004115 RID: 16661 RVA: 0x0024662C File Offset: 0x0024562C
	public sprឤ(spr\u2604 A_0, spr\u1DAB A_1)
	{
		int a_ = 19;
		this.ᜁ = new SortedList<string, spr\u2228>(new sprἝ());
		this.ᜃ = new List<string>();
		this.ᜄ = new List<string>();
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("㥈⩊㽌⩎㽐❒ፔ㹖㕘㹚", a_));
		}
		if (A_1 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("ⱈ╊㥌㵎⡐", a_));
		}
		if (A_1.ᜄ() != spr\u1DAB.EntryType.Storage && A_1.ᜄ() != spr\u1DAB.EntryType.Root)
		{
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⱈ╊㥌㵎⡐", a_));
		}
		this.ᜂ = A_1;
		this.ᜀ = A_0;
		this.ᜀ(A_1.ᜅ());
	}

	// Token: 0x06004116 RID: 16662 RVA: 0x002466E4 File Offset: 0x002456E4
	private void ᜀ(int A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_D0:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				num = 8;
				break;
			}
			break;
		}
		spr\u1DAB spr_u1DAB;
		string text;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_DC;
			case 1:
			{
				spr\u1DAB.EntryType entryType;
				switch (entryType)
				{
				case spr\u1DAB.EntryType.Storage:
					goto IL_AC;
				case spr\u1DAB.EntryType.Stream:
					num = 3;
					continue;
				default:
					num = 7;
					continue;
				}
				break;
			}
			case 2:
			{
				spr\u1B4E value = new spr\u1B4E(this.ᜀ, spr_u1DAB);
				this.ᜁ.Add(text, value);
				this.ᜄ.Add(text);
				num = 4;
				continue;
			}
			case 3:
				if (!this.ᜄ.Contains(text))
				{
					num = 2;
					continue;
				}
				goto IL_18D;
			case 4:
				goto IL_17D;
			case 5:
				goto IL_18B;
			case 6:
				return;
			case 7:
				num = 5;
				continue;
			}
			if (A_0 < 0)
			{
				if (true)
				{
				}
				num = 6;
			}
			else
			{
				List<spr\u1DAB> list = this.ᜀ.\u170D().ᜁ();
				spr_u1DAB = list[A_0];
				int a_ = spr_u1DAB.ᜈ();
				text = spr_u1DAB.ᜀ();
				this.ᜀ(a_);
				spr\u1DAB.EntryType entryType = spr_u1DAB.ᜄ();
				num = 1;
			}
		}
		return;
		IL_AC:
		this.ᜁ.Add(text, new sprឤ(this.ᜀ, spr_u1DAB));
		this.ᜃ.Add(text);
		goto IL_D0;
		IL_DC:
		IL_17D:
		goto IL_18D;
		IL_18B:
		throw new NotImplementedException();
		IL_18D:
		int a_2 = spr_u1DAB.ᜉ();
		this.ᜀ(a_2);
	}

	// Token: 0x06004117 RID: 16663 RVA: 0x00246890 File Offset: 0x00245890
	public spr\u1FDC ᜀ(string A_0)
	{
		int a_ = 16;
		int num = 6;
		spr\u1DAB a_2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_F2;
			case 1:
				goto IL_E5;
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
					num = 4;
					continue;
				}
				break;
			case 3:
				if (!this.ᜀ.ᜄ())
				{
					num = 5;
					continue;
				}
				num = 0;
				continue;
			case 4:
				if (this.ᜇ(A_0))
				{
					num = 1;
					continue;
				}
				a_2 = this.ᜀ.ᜀ(A_0, spr\u1DAB.EntryType.Stream);
				num = 3;
				continue;
			case 5:
				num = 7;
				continue;
			case 7:
				goto IL_57;
			}
			if (this.ᜆ(A_0))
			{
				goto IL_100;
			}
			num = 2;
		}
		IL_57:
		if (true)
		{
		}
		spr\u1B4E spr_u1B4E = new spr\u1B4E(this.ᜀ, a_2);
		goto IL_122;
		IL_E5:
		goto IL_100;
		IL_F2:
		spr_u1B4E = new spr\u2255(this.ᜀ, a_2);
		goto IL_122;
		IL_100:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㕅㱇㡉⥋⽍㵏᱑㕓㭕㵗", a_), RecordTableEnumerator.b("ॅ⩇⁉⥋ⵍ⑏牑⍓㽕ⱗ㉙籛ⵝᕟšౣ䙥٧୩ū୭偯፱ᡳѵᵷ᭹᡻ݽꁿﲃﮇﺉﾋ", a_));
		IL_122:
		spr\u1B4E spr_u1B4E2 = spr_u1B4E;
		this.ᜄ.Add(A_0);
		this.ᜁ.Add(A_0, spr_u1B4E2);
		spr_u1B4E2.ᜃ();
		return new sprἃ(spr_u1B4E2);
	}

	// Token: 0x06004118 RID: 16664 RVA: 0x002469E8 File Offset: 0x002459E8
	public spr\u1FDC ᜂ(string A_0)
	{
		spr\u1B4E spr_u1B4E;
		for (;;)
		{
			spr_u1B4E = (this.ᜁ[A_0] as spr\u1B4E);
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_77;
			}
			if (false)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_77;
				case 1:
					if (spr_u1B4E != null)
					{
						num = 2;
						continue;
					}
					goto IL_79;
				case 2:
					spr_u1B4E.ᜃ();
					if (true)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
		}
		IL_77:
		IL_79:
		return new sprἃ(spr_u1B4E);
	}

	// Token: 0x06004119 RID: 16665 RVA: 0x00246A74 File Offset: 0x00245A74
	public void ᜁ(string A_0)
	{
		for (;;)
		{
			spr\u1B4E spr_u1B4E = this.ᜁ[A_0] as spr\u1B4E;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (false)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜀ.ᜁ(spr_u1B4E.ᜈ());
					spr_u1B4E.Dispose();
					this.ᜁ.Remove(A_0);
					if (true)
					{
					}
					num = 2;
					continue;
				case 1:
					if (spr_u1B4E != null)
					{
						num = 0;
						continue;
					}
					return;
				case 2:
					return;
				}
				break;
			}
		}
	}

	// Token: 0x0600411A RID: 16666 RVA: 0x00246B1C File Offset: 0x00245B1C
	public bool ᜆ(string A_0)
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

	// Token: 0x0600411B RID: 16667 RVA: 0x00246B64 File Offset: 0x00245B64
	public spr\u20C3 ᜄ(string A_0)
	{
		int a_ = 15;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_A8;
			case 2:
				num = 3;
				continue;
			case 3:
				if (this.ᜇ(A_0))
				{
					num = 1;
					continue;
				}
				goto IL_AA;
			}
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
				if (this.ᜆ(A_0))
				{
					goto IL_6A;
				}
				break;
			}
			num = 2;
		}
		IL_6A:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㙄㍆㭈⹊ⱌ≎ὐ㉒㡔㉖", a_), RecordTableEnumerator.b("੄╆⍈⹊⹌㭎煐⑒㱔⍖ㅘ筚⹜⩞ɠୢ䕤०ࡨ٪࡬佮ၰὲݴቶᡸὺѼ彾ﮂﶈ", a_));
		IL_A8:
		goto IL_6A;
		IL_AA:
		spr\u1DAB spr_u1DAB = this.ᜀ.ᜀ(A_0, spr\u1DAB.EntryType.Storage);
		spr\u1DAB spr_u1DAB2 = spr_u1DAB;
		DateTime now;
		spr_u1DAB.ᜀ(now = DateTime.Now);
		spr_u1DAB2.ᜁ(now);
		sprឤ sprឤ = new sprឤ(this.ᜀ, spr_u1DAB);
		this.ᜃ.Add(A_0);
		this.ᜁ.Add(A_0, sprឤ);
		sprឤ.ᜀ();
		return new spr\u2360(sprឤ);
	}

	// Token: 0x0600411C RID: 16668 RVA: 0x00246C70 File Offset: 0x00245C70
	public spr\u20C3 ᜃ(string A_0)
	{
		sprឤ sprឤ;
		for (;;)
		{
			sprឤ = (this.ᜁ[A_0] as sprឤ);
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_77;
			default:
			{
				if (false)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_77;
					case 1:
						sprឤ.ᜀ();
						num = 0;
						continue;
					case 2:
						if (sprឤ != null)
						{
							num = 1;
							continue;
						}
						goto IL_79;
					}
					break;
				}
				break;
			}
			}
		}
		IL_77:
		IL_79:
		return new spr\u2360(sprឤ);
	}

	// Token: 0x0600411D RID: 16669 RVA: 0x00246CFC File Offset: 0x00245CFC
	private void ᜀ()
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
	}

	// Token: 0x0600411E RID: 16670 RVA: 0x00246D38 File Offset: 0x00245D38
	public void ᜅ(string A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			sprឤ sprឤ = this.ᜁ[A_0] as sprឤ;
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
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (sprឤ != null)
						{
							num = 2;
							continue;
						}
						return;
					case 2:
						sprឤ.ᜃ();
						this.ᜁ.Remove(A_0);
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
			}
		}
	}

	// Token: 0x0600411F RID: 16671 RVA: 0x00246DD0 File Offset: 0x00245DD0
	public void ᜃ()
	{
		int num = 0;
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
				case 1:
					return;
				case 2:
					this.ᜀ = null;
					this.ᜁ = null;
					this.ᜂ = null;
					GC.SuppressFinalize(this);
					if (true)
					{
					}
					num = 1;
					continue;
				}
				if (this.ᜀ == null)
				{
					return;
				}
				break;
			}
			num = 2;
		}
	}

	// Token: 0x06004120 RID: 16672 RVA: 0x00246E60 File Offset: 0x00245E60
	public bool ᜇ(string A_0)
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
		return this.ᜁ.ContainsKey(A_0);
	}

	// Token: 0x06004121 RID: 16673 RVA: 0x00246EA8 File Offset: 0x00245EA8
	public void ᜄ()
	{
		switch (0)
		{
		default:
		{
			this.ᜂ.ᜁ(-1);
			IEnumerator<spr\u2228> enumerator = this.ᜁ.Values.GetEnumerator();
			try
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 1:
						num = 4;
						continue;
					case 2:
					{
						if (!enumerator.MoveNext())
						{
							num = 1;
							continue;
						}
						spr\u2228 spr_u = enumerator.Current;
						spr_u.ᜁ();
						num = 0;
						continue;
					}
					case 4:
						goto IL_20E;
					}
					IL_1D1:
					num = 2;
					continue;
					goto IL_1D1;
				}
				IL_20E:
				goto IL_190;
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						enumerator.Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_24B;
					}
					if (enumerator == null)
					{
						break;
					}
					num = 0;
				}
				IL_24B:;
			}
			return;
			for (;;)
			{
				IL_190:
				spr\u2228 spr_u2 = null;
				IEnumerator<spr\u2228> enumerator2 = this.ᜁ.Values.GetEnumerator();
				try
				{
					int num = 2;
					for (;;)
					{
						spr\u2228 spr_u3;
						switch (num)
						{
						case 0:
							goto IL_14B;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_D7;
							default:
								if (false)
								{
								}
								if (spr_u2 != null)
								{
									num = 4;
									continue;
								}
								this.ᜂ.ᜅ(spr_u3.ᜀ().ᜁ());
								num = 6;
								continue;
							}
							break;
						case 3:
							if (!enumerator2.MoveNext())
							{
								num = 8;
								continue;
							}
							goto IL_D7;
						case 4:
							spr_u2.ᜀ().ᜀ(spr_u3.ᜀ().ᜁ());
							spr_u2.ᜀ().ᜁ(-1);
							num = 7;
							continue;
						case 5:
							if (true)
							{
							}
							break;
						case 6:
							goto IL_72;
						case 7:
							goto IL_72;
						case 8:
							num = 0;
							continue;
						}
						goto IL_70;
						IL_72:
						spr_u2 = spr_u3;
						num = 5;
						continue;
						IL_87:
						num = 3;
						continue;
						IL_70:
						goto IL_87;
						IL_D7:
						spr_u3 = enumerator2.Current;
						num = 1;
					}
					IL_14B:
					break;
				}
				finally
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_18D;
						case 2:
							enumerator2.Dispose();
							num = 1;
							continue;
						}
						if (enumerator2 == null)
						{
							break;
						}
						num = 2;
					}
					IL_18D:;
				}
			}
			return;
		}
		}
	}

	// Token: 0x06004122 RID: 16674 RVA: 0x00247138 File Offset: 0x00246138
	private void ᜁ(spr\u25CC A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_BB:
			num = 4;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_53;
			}
			break;
		}
		object obj;
		spr\u1DAB spr_u1DAB;
		for (;;)
		{
			IL_2C:
			spr\u1B4E spr_u1B4E;
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				if (obj != null)
				{
					num = 6;
					continue;
				}
				return;
			case 1:
				if (spr_u1B4E != null)
				{
					num = 2;
					continue;
				}
				return;
			case 2:
				spr_u1DAB.ᜀ((uint)spr_u1B4E.Length);
				num = 5;
				continue;
			case 3:
				if (this.ᜂ.ᜅ() < 0)
				{
					num = 7;
					continue;
				}
				goto IL_86;
			case 4:
				goto IL_86;
			case 5:
				return;
			case 6:
			{
				spr\u2228 spr_u = obj as spr\u2228;
				spr_u1DAB = spr_u.ᜀ();
				spr_u1DAB.ᜀ((byte)A_0.ᜃ());
				spr_u1DAB.ᜁ(this.ᜀ(A_0.ᜅ()));
				spr_u1DAB.ᜀ(this.ᜀ(A_0.ᜇ()));
				num = 3;
				continue;
			}
			case 7:
				goto IL_131;
			}
			goto IL_53;
			IL_86:
			spr_u1B4E = (obj as spr\u1B4E);
			num = 1;
		}
		IL_131:
		this.ᜂ.ᜅ(spr_u1DAB.ᜁ());
		goto IL_BB;
		IL_53:
		obj = A_0.ᜆ();
		num = 0;
		goto IL_2C;
	}

	// Token: 0x06004123 RID: 16675 RVA: 0x00247298 File Offset: 0x00246298
	private int ᜀ(spr\u25CC A_0)
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
			if (A_0.ᜁ())
			{
				return -1;
			}
			break;
		}
		return (A_0.ᜆ() as spr\u2228).ᜀ().ᜁ();
	}

	// Token: 0x06004124 RID: 16676 RVA: 0x002472F4 File Offset: 0x002462F4
	public string[] ᜆ()
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
		return this.ᜄ.ToArray();
	}

	// Token: 0x06004125 RID: 16677 RVA: 0x0024733C File Offset: 0x0024633C
	public string[] ᜁ()
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
		return this.ᜃ.ToArray();
	}

	// Token: 0x06004126 RID: 16678 RVA: 0x00247384 File Offset: 0x00246384
	public string ᜅ()
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
		return this.ᜂ.ᜀ();
	}

	// Token: 0x06004127 RID: 16679 RVA: 0x002473CC File Offset: 0x002463CC
	public spr\u1DAB ᜂ()
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
		return this.ᜂ;
	}

	// Token: 0x06004128 RID: 16680 RVA: 0x00247410 File Offset: 0x00246410
	public void ᜀ(spr\u20C3 A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u20C3 spr_u20C = this.ᜄ(A_0.ᜃ());
				string[] array = A_0.ᜁ();
				int num = 0;
				int num2 = array.Length;
				if (true)
				{
				}
				int num3 = 0;
				for (;;)
				{
					int num4;
					int num5;
					string[] array2;
					switch (num3)
					{
					case 0:
						goto IL_19B;
					case 1:
						goto IL_19B;
					case 2:
						try
						{
							spr\u1FDC spr_u1FDC;
							spr_u20C.ᜀ(spr_u1FDC);
							goto IL_1DA;
						}
						finally
						{
							num3 = 1;
							for (;;)
							{
								spr\u1FDC spr_u1FDC;
								switch (num3)
								{
								case 0:
									((IDisposable)spr_u1FDC).Dispose();
									num3 = 2;
									continue;
								case 2:
									goto IL_14B;
								}
								if (spr_u1FDC == null)
								{
									break;
								}
								num3 = 0;
							}
							IL_14B:;
						}
						goto IL_14E;
						IL_1DA:
						num++;
						num3 = 1;
						continue;
					case 3:
						goto IL_E2;
					case 4:
						return;
					case 5:
					{
						if (num >= num2)
						{
							num3 = 3;
							continue;
						}
						spr\u1FDC spr_u1FDC = A_0.ᜁ(array[num]);
						num3 = 2;
						continue;
					}
					case 6:
						goto IL_17B;
					case 7:
						goto IL_17B;
					case 8:
						try
						{
							spr\u20C3 spr_u20C2;
							spr_u20C.ᜀ(spr_u20C2);
							goto IL_14E;
						}
						finally
						{
							num3 = 0;
							for (;;)
							{
								spr\u20C3 spr_u20C2;
								switch (num3)
								{
								case 1:
									spr_u20C2.Dispose();
									num3 = 2;
									continue;
								case 2:
									goto IL_DF;
								}
								if (spr_u20C2 == null)
								{
									break;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_E1;
								default:
									if (false)
									{
									}
									num3 = 1;
									break;
								}
							}
							IL_DF:
							IL_E1:;
						}
						goto IL_E2;
					case 9:
					{
						if (num4 >= num5)
						{
							num3 = 4;
							continue;
						}
						spr\u20C3 spr_u20C2 = A_0.ᜅ(array2[num4]);
						num3 = 8;
						continue;
					}
					}
					break;
					IL_E2:
					array2 = A_0.ᜂ();
					num4 = 0;
					num5 = array2.Length;
					num3 = 6;
					continue;
					IL_14E:
					num4++;
					num3 = 7;
					continue;
					IL_17B:
					num3 = 9;
					continue;
					IL_19B:
					num3 = 5;
				}
			}
			return;
		}
	}

	// Token: 0x06004129 RID: 16681 RVA: 0x00247628 File Offset: 0x00246628
	public void ᜀ(spr\u1FDC A_0)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 5;
			long position;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_107;
				case 1:
					goto IL_DA;
				case 2:
				{
					byte[] buffer;
					int count;
					if ((count = A_0.Read(buffer, 0, 32768)) <= 0)
					{
						num = 0;
						continue;
					}
					spr\u1FDC spr_u1FDC;
					spr_u1FDC.Write(buffer, 0, count);
					num = 1;
					continue;
				}
				case 3:
					goto IL_4D;
				case 4:
					goto IL_DA;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
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
					if (true)
					{
					}
					spr\u1FDC spr_u1FDC = this.ᜀ(A_0.ᜋ());
					byte[] buffer = new byte[32768];
					position = A_0.Position;
					A_0.Position = 0L;
					break;
				}
				}
				num = 4;
				continue;
				IL_DA:
				num = 2;
			}
			IL_4D:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰾㕀ㅂ⁄♆⑈Ὂ≌౎㹐⍒ⱔ", a_));
			IL_107:
			A_0.Position = position;
			return;
		}
		}
	}

	// Token: 0x04001CFF RID: 7423
	private spr\u2604 ᜀ;

	// Token: 0x04001D00 RID: 7424
	private SortedList<string, spr\u2228> ᜁ;

	// Token: 0x04001D01 RID: 7425
	private spr\u1DAB ᜂ;

	// Token: 0x04001D02 RID: 7426
	private List<string> ᜃ;

	// Token: 0x04001D03 RID: 7427
	private List<string> ᜄ;
}
