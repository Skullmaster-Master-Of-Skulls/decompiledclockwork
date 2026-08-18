using System;
using System.Collections.Generic;
using Spire.CompoundFile.Doc;

// Token: 0x02000328 RID: 808
internal class spr\u217C : spr\u2547, spr\u19AD
{
	// Token: 0x06002BB1 RID: 11185 RVA: 0x002A9C18 File Offset: 0x002A8C18
	public spr\u217C(spr\u20BF A_0, string A_1, int A_2)
	{
		int a_ = 15;
		this.ᜁ = new SortedList<string, spr\u19AD>(new spr\u2008());
		this.ᜃ = new List<string>();
		this.ᜄ = new List<string>();
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("մᙶ୸Ṻ፼୾", a_));
		}
		this.ᜀ = A_0;
		this.ᜂ = new spr\u2486(A_1, spr\u2486.EntryType.Storage, A_2);
	}

	// Token: 0x06002BB2 RID: 11186 RVA: 0x002A9C88 File Offset: 0x002A8C88
	public spr\u217C(spr\u20BF A_0, spr\u2486 A_1)
	{
		int a_ = 4;
		this.ᜁ = new SortedList<string, spr\u19AD>(new spr\u2008());
		this.ᜃ = new List<string>();
		this.ᜄ = new List<string>();
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("ᩩ൫ᱭᕯᱱsふᅷᙹ᥻", a_));
		}
		if (A_1 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("ཀྵɫᩭɯୱ", a_));
		}
		if (A_1.ᜄ() != spr\u2486.EntryType.Storage && A_1.ᜄ() != spr\u2486.EntryType.Root)
		{
			throw new ArgumentOutOfRangeException(ClipboardData.b("ཀྵɫᩭɯୱ", a_));
		}
		this.ᜂ = A_1;
		this.ᜀ = A_0;
		this.ᜀ(A_1.ᜅ());
	}

	// Token: 0x06002BB3 RID: 11187 RVA: 0x002A9D40 File Offset: 0x002A8D40
	private void ᜀ(int A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 6;
			spr\u2486 spr_u;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					spr\u244A value = new spr\u244A(this.ᜀ, spr_u);
					string text;
					this.ᜁ.Add(text, value);
					this.ᜄ.Add(text);
					num = 1;
					continue;
				}
				case 1:
					goto IL_17D;
				case 2:
					goto IL_C0;
				case 3:
				{
					spr\u2486.EntryType entryType;
					switch (entryType)
					{
					case spr\u2486.EntryType.Storage:
					{
						string text;
						this.ᜁ.Add(text, new spr\u217C(this.ᜀ, spr_u));
						this.ᜃ.Add(text);
						num = 2;
						continue;
					}
					case spr\u2486.EntryType.Stream:
						num = 8;
						continue;
					default:
						num = 5;
						continue;
					}
					break;
				}
				case 4:
					goto IL_5B;
				case 5:
					num = 7;
					continue;
				case 7:
					goto IL_18B;
				case 8:
				{
					if (true)
					{
					}
					string text;
					if (!this.ᜄ.Contains(text))
					{
						num = 0;
						continue;
					}
					goto IL_18D;
				}
				}
				if (A_0 < 0)
				{
					num = 4;
				}
				else
				{
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
						List<spr\u2486> list = this.ᜀ.\u170D().ᜁ();
						spr_u = list[A_0];
						int a_ = spr_u.ᜈ();
						string text = spr_u.ᜀ();
						this.ᜀ(a_);
						spr\u2486.EntryType entryType = spr_u.ᜄ();
						num = 3;
						break;
					}
					}
				}
			}
			IL_5B:
			return;
			IL_C0:
			IL_17D:
			goto IL_18D;
			IL_18B:
			throw new NotImplementedException();
			IL_18D:
			int a_2 = spr_u.ᜉ();
			this.ᜀ(a_2);
			return;
		}
		}
	}

	// Token: 0x06002BB4 RID: 11188 RVA: 0x002A9EEC File Offset: 0x002A8EEC
	public spr\u2578 ᜀ(string A_0)
	{
		int a_ = 3;
		spr\u2486 a_2;
		for (;;)
		{
			IL_09:
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_57;
				case 2:
					if (this.ᜇ(A_0))
					{
						num = 5;
						continue;
					}
					a_2 = this.ᜀ.ᜀ(A_0, spr\u2486.EntryType.Stream);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 3:
					if (!this.ᜀ.ᜄ())
					{
						num = 4;
						continue;
					}
					num = 7;
					continue;
				case 4:
					num = 0;
					continue;
				case 5:
					goto IL_E8;
				case 6:
					num = 2;
					continue;
				case 7:
					goto IL_F5;
				}
				if (this.ᜆ(A_0))
				{
					goto IL_103;
				}
				num = 6;
			}
		}
		IL_57:
		if (true)
		{
		}
		spr\u244A spr_u244A = new spr\u244A(this.ᜀ, a_2);
		goto IL_125;
		IL_E8:
		goto IL_103;
		IL_F5:
		spr_u244A = new spr\u2517(this.ᜀ, a_2);
		goto IL_125;
		IL_103:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᩨὪὬ੮ၰṲ㭴ᙶᑸṺ", a_), ClipboardData.b("♨४ݬ੮ተݲ啴vၸེᕼ彾ꦈ뎒ﮖﺚﲜﮞ\ud8a0莢삤\udfa6삨\ud8aa\ud9ac\udcae", a_));
		IL_125:
		spr\u244A spr_u244A2 = spr_u244A;
		this.ᜄ.Add(A_0);
		this.ᜁ.Add(A_0, spr_u244A2);
		spr_u244A2.ᜃ();
		return new spr\u20DE(spr_u244A2);
	}

	// Token: 0x06002BB5 RID: 11189 RVA: 0x002AA044 File Offset: 0x002A9044
	public spr\u2578 ᜂ(string A_0)
	{
		spr\u244A spr_u244A;
		for (;;)
		{
			spr_u244A = (this.ᜁ[A_0] as spr\u244A);
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_4A:
				if (spr_u244A == null)
				{
					goto IL_79;
				}
				num = 1;
				break;
			default:
				if (false)
				{
				}
				num = 0;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_4A;
				case 1:
					spr_u244A.ᜃ();
					if (true)
					{
					}
					num = 2;
					continue;
				case 2:
					goto IL_77;
				}
				break;
			}
		}
		IL_77:
		IL_79:
		return new spr\u20DE(spr_u244A);
	}

	// Token: 0x06002BB6 RID: 11190 RVA: 0x002AA0D0 File Offset: 0x002A90D0
	public void ᜁ(string A_0)
	{
		for (;;)
		{
			spr\u244A spr_u244A = this.ᜁ[A_0] as spr\u244A;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_4A:
				if (spr_u244A == null)
				{
					return;
				}
				num = 0;
				break;
			default:
				if (false)
				{
				}
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜀ.ᜁ(spr_u244A.ᜈ());
					spr_u244A.Dispose();
					this.ᜁ.Remove(A_0);
					if (true)
					{
					}
					num = 1;
					continue;
				case 1:
					return;
				case 2:
					goto IL_4A;
				}
				break;
			}
		}
	}

	// Token: 0x06002BB7 RID: 11191 RVA: 0x002AA178 File Offset: 0x002A9178
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

	// Token: 0x06002BB8 RID: 11192 RVA: 0x002AA1C0 File Offset: 0x002A91C0
	public spr\u2547 ᜄ(string A_0)
	{
		int a_ = 17;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_32;
				}
				if (false)
				{
				}
				num = 1;
				continue;
			case 1:
				if (this.ᜇ(A_0))
				{
					num = 3;
					continue;
				}
				goto IL_AD;
			case 3:
				goto IL_AB;
			}
			goto IL_29;
			IL_32:
			if (true)
			{
			}
			num = 0;
			continue;
			IL_29:
			if (!this.ᜆ(A_0))
			{
				goto IL_32;
			}
			break;
		}
		IL_44:
		throw new ArgumentOutOfRangeException(ClipboardData.b("Ѷ൸ॺ᡼Ṿ춂", a_), ClipboardData.b("㡶᭸ᅺ᡼᱾ꎂﶈ권ﲎﶔ랖漢爵膠슢즤햦첨쪪즬횮醰횲춴\udeb6쪸쾺캼", a_));
		IL_AB:
		goto IL_44;
		IL_AD:
		spr\u2486 spr_u = this.ᜀ.ᜀ(A_0, spr\u2486.EntryType.Storage);
		spr\u2486 spr_u2 = spr_u;
		DateTime now;
		spr_u.ᜀ(now = DateTime.Now);
		spr_u2.ᜁ(now);
		spr\u217C spr_u217C = new spr\u217C(this.ᜀ, spr_u);
		this.ᜃ.Add(A_0);
		this.ᜁ.Add(A_0, spr_u217C);
		spr_u217C.ᜀ();
		return new spr\u24E0(spr_u217C);
	}

	// Token: 0x06002BB9 RID: 11193 RVA: 0x002AA2D0 File Offset: 0x002A92D0
	public spr\u2547 ᜃ(string A_0)
	{
		spr\u217C spr_u217C;
		for (;;)
		{
			spr_u217C = (this.ᜁ[A_0] as spr\u217C);
			if (true)
			{
			}
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_52:
				if (spr_u217C == null)
				{
					goto IL_79;
				}
				num = 1;
				break;
			default:
				if (false)
				{
				}
				num = 0;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_52;
				case 1:
					spr_u217C.ᜀ();
					num = 2;
					continue;
				case 2:
					goto IL_77;
				}
				break;
			}
		}
		IL_77:
		IL_79:
		return new spr\u24E0(spr_u217C);
	}

	// Token: 0x06002BBA RID: 11194 RVA: 0x002AA35C File Offset: 0x002A935C
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

	// Token: 0x06002BBB RID: 11195 RVA: 0x002AA398 File Offset: 0x002A9398
	public void ᜅ(string A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			spr\u217C spr_u217C = this.ᜁ[A_0] as spr\u217C;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_52:
				if (spr_u217C == null)
				{
					return;
				}
				num = 0;
				break;
			default:
				if (false)
				{
				}
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					spr_u217C.ᜃ();
					this.ᜁ.Remove(A_0);
					num = 1;
					continue;
				case 1:
					return;
				case 2:
					goto IL_52;
				}
				break;
			}
		}
	}

	// Token: 0x06002BBC RID: 11196 RVA: 0x002AA430 File Offset: 0x002A9430
	public void ᜃ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_4A;
			case 1:
				return;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_4A:
				this.ᜀ = null;
				this.ᜁ = null;
				this.ᜂ = null;
				GC.SuppressFinalize(this);
				if (true)
				{
				}
				num = 1;
				break;
			default:
				if (false)
				{
				}
				if (this.ᜀ == null)
				{
					return;
				}
				num = 0;
				break;
			}
		}
	}

	// Token: 0x06002BBD RID: 11197 RVA: 0x002AA4C0 File Offset: 0x002A94C0
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

	// Token: 0x06002BBE RID: 11198 RVA: 0x002AA508 File Offset: 0x002A9508
	public void ᜄ()
	{
		switch (0)
		{
		default:
		{
			this.ᜂ.ᜁ(-1);
			IEnumerator<spr\u19AD> enumerator = this.ᜁ.Values.GetEnumerator();
			try
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_1E5;
					case 3:
					{
						if (!enumerator.MoveNext())
						{
							num = 4;
							continue;
						}
						spr\u19AD spr_u19AD = enumerator.Current;
						spr_u19AD.ᜁ();
						num = 2;
						continue;
					}
					case 4:
						num = 1;
						continue;
					}
					IL_1A8:
					num = 3;
					continue;
					goto IL_1A8;
				}
				IL_1E5:
				goto IL_167;
			}
			finally
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_22F:
					enumerator.Dispose();
					num = 1;
					break;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_22D;
					case 1:
						goto IL_23E;
					}
					if (enumerator == null)
					{
						goto IL_240;
					}
					num = 0;
				}
				IL_22D:
				goto IL_22F;
				IL_23E:
				IL_240:;
			}
			return;
			for (;;)
			{
				IL_167:
				spr\u19AD spr_u19AD2 = null;
				IEnumerator<spr\u19AD> enumerator2 = this.ᜁ.Values.GetEnumerator();
				try
				{
					int num = 2;
					for (;;)
					{
						spr\u19AD spr_u19AD3;
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							break;
						case 1:
							goto IL_68;
						case 3:
							goto IL_122;
						case 4:
							goto IL_68;
						case 5:
							num = 3;
							continue;
						case 6:
							if (!enumerator2.MoveNext())
							{
								num = 5;
								continue;
							}
							spr_u19AD3 = enumerator2.Current;
							num = 7;
							continue;
						case 7:
							if (spr_u19AD2 != null)
							{
								num = 8;
								continue;
							}
							this.ᜂ.ᜅ(spr_u19AD3.ᜀ().ᜁ());
							num = 4;
							continue;
						case 8:
							spr_u19AD2.ᜀ().ᜀ(spr_u19AD3.ᜀ().ᜁ());
							spr_u19AD2.ᜀ().ᜁ(-1);
							num = 1;
							continue;
						}
						goto IL_66;
						IL_68:
						spr_u19AD2 = spr_u19AD3;
						num = 0;
						continue;
						IL_7D:
						num = 6;
						continue;
						IL_66:
						goto IL_7D;
					}
					IL_122:
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
							enumerator2.Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_164;
						}
						if (enumerator2 == null)
						{
							break;
						}
						num = 1;
					}
					IL_164:;
				}
			}
			return;
		}
		}
	}

	// Token: 0x06002BBF RID: 11199 RVA: 0x002AA77C File Offset: 0x002A977C
	private void ᜁ(spr\u25B8 A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				object obj = A_0.ᜆ();
				int num = 6;
				for (;;)
				{
					spr\u2486 spr_u;
					spr\u244A spr_u244A;
					switch (num)
					{
					case 0:
						goto IL_11A;
					case 1:
						this.ᜂ.ᜅ(spr_u.ᜁ());
						num = 5;
						continue;
					case 2:
						goto IL_135;
					case 3:
					{
						spr\u19AD spr_u19AD = obj as spr\u19AD;
						spr_u = spr_u19AD.ᜀ();
						spr_u.ᜀ((byte)A_0.ᜃ());
						spr_u.ᜁ(this.ᜀ(A_0.ᜅ()));
						spr_u.ᜀ(this.ᜀ(A_0.ᜇ()));
						num = 4;
						continue;
					}
					case 4:
						if (this.ᜂ.ᜅ() < 0)
						{
							num = 1;
							continue;
						}
						goto IL_62;
					case 5:
						goto IL_62;
					case 6:
						if (obj != null)
						{
							num = 3;
							continue;
						}
						goto IL_135;
					case 7:
						if (spr_u244A != null)
						{
							num = 0;
							continue;
						}
						goto IL_135;
					}
					break;
					IL_62:
					if (true)
					{
					}
					spr_u244A = (obj as spr\u244A);
					num = 7;
					continue;
					IL_11A:
					spr_u.ᜀ((uint)spr_u244A.Length);
					num = 2;
					continue;
					IL_135:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_11A;
					default:
						goto IL_14B;
					}
				}
			}
			IL_14B:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x06002BC0 RID: 11200 RVA: 0x002AA8DC File Offset: 0x002A98DC
	private int ᜀ(spr\u25B8 A_0)
	{
		if (!A_0.ᜁ())
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
				return (A_0.ᜆ() as spr\u19AD).ᜀ().ᜁ();
			}
		}
		return -1;
	}

	// Token: 0x06002BC1 RID: 11201 RVA: 0x002AA938 File Offset: 0x002A9938
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

	// Token: 0x06002BC2 RID: 11202 RVA: 0x002AA980 File Offset: 0x002A9980
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

	// Token: 0x06002BC3 RID: 11203 RVA: 0x002AA9C8 File Offset: 0x002A99C8
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

	// Token: 0x06002BC4 RID: 11204 RVA: 0x002AAA10 File Offset: 0x002A9A10
	public spr\u2486 ᜂ()
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

	// Token: 0x06002BC5 RID: 11205 RVA: 0x002AAA54 File Offset: 0x002A9A54
	public void ᜀ(spr\u2547 A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u2547 spr_u = this.ᜄ(A_0.ᜃ());
				string[] array = A_0.ᜁ();
				int num = 0;
				int num2 = array.Length;
				if (true)
				{
				}
				int num3 = 6;
				for (;;)
				{
					int num4;
					int num5;
					string[] array2;
					switch (num3)
					{
					case 0:
						goto IL_1A1;
					case 1:
						goto IL_181;
					case 2:
						goto IL_C6;
					case 3:
					{
						if (num4 >= num5)
						{
							num3 = 4;
							continue;
						}
						spr\u2547 spr_u2 = A_0.ᜅ(array2[num4]);
						num3 = 7;
						continue;
					}
					case 4:
						return;
					case 5:
					{
						if (num >= num2)
						{
							num3 = 2;
							continue;
						}
						spr\u2578 spr_u3 = A_0.ᜁ(array[num]);
						num3 = 8;
						continue;
					}
					case 6:
						goto IL_1A1;
					case 7:
						try
						{
							spr\u2547 spr_u2;
							spr_u.ᜀ(spr_u2);
							goto IL_151;
						}
						finally
						{
							num3 = 2;
							for (;;)
							{
								spr\u2547 spr_u2;
								switch (num3)
								{
								case 0:
									spr_u2.Dispose();
									num3 = 1;
									continue;
								case 1:
									goto IL_C3;
								}
								if (spr_u2 == null)
								{
									break;
								}
								num3 = 0;
							}
							IL_C3:;
						}
						goto IL_C6;
					case 8:
						try
						{
							spr\u2578 spr_u3;
							spr_u.ᜀ(spr_u3);
							goto IL_1E0;
						}
						finally
						{
							spr\u2578 spr_u3;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								IL_13E:
								((IDisposable)spr_u3).Dispose();
								num3 = 2;
								break;
							default:
								if (false)
								{
								}
								num3 = 0;
								break;
							}
							for (;;)
							{
								switch (num3)
								{
								case 1:
									goto IL_13C;
								case 2:
									goto IL_14E;
								}
								if (spr_u3 == null)
								{
									goto IL_150;
								}
								num3 = 1;
							}
							IL_13C:
							goto IL_13E;
							IL_14E:
							IL_150:;
						}
						goto IL_151;
						IL_1E0:
						num++;
						num3 = 0;
						continue;
					case 9:
						goto IL_181;
					}
					break;
					IL_C6:
					array2 = A_0.ᜂ();
					num4 = 0;
					num5 = array2.Length;
					num3 = 9;
					continue;
					IL_151:
					num4++;
					num3 = 1;
					continue;
					IL_181:
					num3 = 3;
					continue;
					IL_1A1:
					num3 = 5;
				}
			}
			return;
		}
	}

	// Token: 0x06002BC6 RID: 11206 RVA: 0x002AAC70 File Offset: 0x002A9C70
	public void ᜀ(spr\u2578 A_0)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 1;
			long position;
			for (;;)
			{
				byte[] buffer;
				spr\u2578 spr_u;
				switch (num)
				{
				case 0:
					goto IL_73;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 2:
					goto IL_D7;
				case 3:
				{
					int count;
					if ((count = A_0.Read(buffer, 0, 32768)) <= 0)
					{
						num = 5;
						continue;
					}
					spr_u.Write(buffer, 0, count);
					num = 4;
					continue;
				}
				case 4:
					goto IL_D7;
				case 5:
					goto IL_101;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				if (true)
				{
				}
				spr_u = this.ᜀ(A_0.ᜋ());
				buffer = new byte[32768];
				position = A_0.Position;
				A_0.Position = 0L;
				num = 2;
				continue;
				IL_D7:
				num = 3;
			}
			IL_73:
			throw new ArgumentNullException(ClipboardData.b("ٴͶ୸Ṻᱼቾ햀욄麗", a_));
			IL_101:
			A_0.Position = position;
			return;
		}
		}
	}

	// Token: 0x040025EA RID: 9706
	private spr\u20BF ᜀ;

	// Token: 0x040025EB RID: 9707
	private SortedList<string, spr\u19AD> ᜁ;

	// Token: 0x040025EC RID: 9708
	private spr\u2486 ᜂ;

	// Token: 0x040025ED RID: 9709
	private List<string> ᜃ;

	// Token: 0x040025EE RID: 9710
	private List<string> ᜄ;
}
