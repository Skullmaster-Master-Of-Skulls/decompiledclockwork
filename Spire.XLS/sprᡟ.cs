using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Collections;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.PivotTables;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x020003AF RID: 943
internal class sprᡟ : IDisposable
{
	// Token: 0x06003919 RID: 14617 RVA: 0x001FCF20 File Offset: 0x001FBF20
	public sprᡟ(sprវ A_0, sprᦨ A_1, string A_2)
	{
		int a_ = 7;
		this.ᜆ = new MemoryStream();
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("唼倾ⵀ❂⁄㕆", a_));
		}
		if (A_1 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("似娾ⵀ≂ㅄ⹆♈╊", a_));
		}
		this.ᜄ = A_0.ᜀ(A_1, A_2);
		this.ᜅ = A_0;
	}

	// Token: 0x0600391A RID: 14618 RVA: 0x001FCF90 File Offset: 0x001FBF90
	public sprᡟ(sprវ A_0, spr\u2570 A_1)
	{
		int a_ = 2;
		this.ᜆ = new MemoryStream();
		base..ctor();
		if (A_1 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("儷丹夻匽", a_));
		}
		this.ᜄ = A_1;
		this.ᜅ = A_0;
	}

	// Token: 0x0600391B RID: 14619 RVA: 0x001FCFE0 File Offset: 0x001FBFE0
	public sprវ ᜋ()
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

	// Token: 0x0600391C RID: 14620 RVA: 0x001FD024 File Offset: 0x001FC024
	public spr\u2570 ᜉ()
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

	// Token: 0x0600391D RID: 14621 RVA: 0x001FD068 File Offset: 0x001FC068
	public void ᜀ(spr\u2570 A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x0600391E RID: 14622 RVA: 0x001FD0AC File Offset: 0x001FC0AC
	public string ᜌ()
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
		return this.ᜈ;
	}

	// Token: 0x0600391F RID: 14623 RVA: 0x001FD0F0 File Offset: 0x001FC0F0
	public void ᜂ(string A_0)
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
		this.ᜈ = A_0;
	}

	// Token: 0x06003920 RID: 14624 RVA: 0x001FD134 File Offset: 0x001FC134
	public string \u170D()
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
		return this.ᜉ;
	}

	// Token: 0x06003921 RID: 14625 RVA: 0x001FD178 File Offset: 0x001FC178
	public void ᜅ(string A_0)
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
		this.ᜉ = A_0;
	}

	// Token: 0x06003922 RID: 14626 RVA: 0x001FD1BC File Offset: 0x001FC1BC
	public RelationsCollection ᜇ()
	{
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
					goto IL_08;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜊ = new RelationsCollection();
				num = 1;
				continue;
			case 1:
				goto IL_6F;
			}
			IL_1C:
			if (this.ᜊ == null)
			{
				num = 0;
				continue;
			}
			break;
			IL_08:
			goto IL_1C;
		}
		IL_6F:
		return this.ᜊ;
	}

	// Token: 0x06003923 RID: 14627 RVA: 0x001FD240 File Offset: 0x001FC240
	public RelationsCollection ᜈ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6F;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					this.ᜋ = new RelationsCollection();
					num = 0;
					continue;
				}
				break;
			}
			IL_1C:
			if (true)
			{
			}
			if (this.ᜋ == null)
			{
				num = 1;
				continue;
			}
			break;
			goto IL_1C;
		}
		IL_6F:
		return this.ᜋ;
	}

	// Token: 0x06003924 RID: 14628 RVA: 0x001FD2C4 File Offset: 0x001FC2C4
	public RelationsCollection ᜏ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_08;
				}
				if (false)
				{
				}
				this.ᜌ = new RelationsCollection();
				if (true)
				{
				}
				num = 2;
				continue;
			case 2:
				goto IL_6F;
			}
			IL_1C:
			if (this.ᜌ == null)
			{
				num = 1;
				continue;
			}
			break;
			IL_08:
			goto IL_1C;
		}
		IL_6F:
		return this.ᜌ;
	}

	// Token: 0x06003925 RID: 14629 RVA: 0x001FD348 File Offset: 0x001FC348
	public string ᜎ()
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
		return this.\u170D;
	}

	// Token: 0x06003926 RID: 14630 RVA: 0x001FD38C File Offset: 0x001FC38C
	public void ᜄ(string A_0)
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
		this.\u170D = A_0;
	}

	// Token: 0x06003927 RID: 14631 RVA: 0x001FD3D0 File Offset: 0x001FC3D0
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
		return this.ᜎ;
	}

	// Token: 0x06003928 RID: 14632 RVA: 0x001FD414 File Offset: 0x001FC414
	public void ᜁ(string A_0)
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
		this.ᜎ = A_0;
	}

	// Token: 0x06003929 RID: 14633 RVA: 0x001FD458 File Offset: 0x001FC458
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
		return this.ᜏ;
	}

	// Token: 0x0600392A RID: 14634 RVA: 0x001FD49C File Offset: 0x001FC49C
	public void ᜆ(string A_0)
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
		this.ᜏ = A_0;
	}

	// Token: 0x0600392B RID: 14635 RVA: 0x001FD4E0 File Offset: 0x001FC4E0
	public string ᜊ()
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
		return this.ᜐ;
	}

	// Token: 0x0600392C RID: 14636 RVA: 0x001FD524 File Offset: 0x001FC524
	public void ᜃ(string A_0)
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
		this.ᜐ = A_0;
	}

	// Token: 0x0600392D RID: 14637 RVA: 0x001FD568 File Offset: 0x001FC568
	public Stream ᜐ()
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
		return this.ᜑ;
	}

	// Token: 0x0600392E RID: 14638 RVA: 0x001FD5AC File Offset: 0x001FC5AC
	public void ᜀ(Stream A_0)
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
		this.ᜑ = A_0;
	}

	// Token: 0x0600392F RID: 14639 RVA: 0x001FD5F0 File Offset: 0x001FC5F0
	public void ᜀ(List<spr\u21A7> A_0, XlsWorksheet A_1)
	{
		int a_ = 8;
		int num = 2;
		for (;;)
		{
			XmlReader xmlReader;
			spr\u2306 spr_u;
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 1:
				xmlReader.Read();
				num = 7;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_115;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 3:
				if (this.ᜇ.Length != 0L)
				{
					num = 4;
					continue;
				}
				return;
			case 4:
				this.ᜇ.Position = 0L;
				spr_u = this.ᜅ.\u1718();
				xmlReader = UtilityMethods.ᜀ(this.ᜇ);
				num = 6;
				continue;
			case 5:
				return;
			case 6:
				if (xmlReader.LocalName == RecordTableEnumerator.b("䰽⼿ⵁぃ", a_))
				{
					if (true)
					{
					}
					num = 1;
					continue;
				}
				goto IL_115;
			case 7:
				goto IL_115;
			}
			if (this.ᜇ != null)
			{
				num = 0;
				continue;
			}
			break;
			IL_115:
			spr_u.ᜀ(xmlReader, A_1.ConditionalFormats, A_0);
			xmlReader.Close();
			this.ᜇ.Close();
			this.ᜇ = null;
			num = 5;
		}
	}

	// Token: 0x06003930 RID: 14640 RVA: 0x001FD748 File Offset: 0x001FC748
	public void ᜁ(XlsWorksheet A_0, Dictionary<int, int> A_1)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			string text2;
			for (;;)
			{
				spr\u2306 spr_u = this.ᜅ.\u1718();
				string text = this.ᜄ.ᜇ();
				int num = text.LastIndexOf('/');
				string a_2 = text.Substring(0, num);
				text2 = text.Insert(num, '/' + RecordTableEnumerator.b("昸䤺堼匾㉀", a_)) + RecordTableEnumerator.b("᜸䤺堼匾㉀", a_);
				spr\u2570 spr_u2 = this.ᜅ.\u1714().ᜃ(text2);
				int num2 = 0;
				for (;;)
				{
					XmlReader a_3;
					switch (num2)
					{
					case 0:
						if (spr_u2 != null)
						{
							num2 = 1;
							continue;
						}
						goto IL_12A;
					case 1:
						a_3 = UtilityMethods.ᜀ(spr_u2.ᜐ());
						this.ᜊ = spr_u.ᜧ(a_3);
						this.ᜊ.ItemPath = text2;
						num2 = 5;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_194;
						default:
							if (false)
							{
							}
							this.ᜇ(text);
							num2 = 6;
							continue;
						}
						break;
					case 3:
						goto IL_194;
					case 4:
						if (this.ᜊ != null)
						{
							num2 = 3;
							continue;
						}
						goto IL_1EE;
					case 5:
						if (true)
						{
						}
						goto IL_12A;
					case 6:
						goto IL_1EC;
					case 7:
						if (this.ᜊ.Count > 0)
						{
							num2 = 2;
							continue;
						}
						goto IL_1EE;
					}
					break;
					IL_12A:
					this.ᜄ.ᜀ(true);
					a_3 = UtilityMethods.ᜀ(this.ᜄ.ᜐ());
					spr_u.ᜀ(a_3, A_0, a_2, ref this.ᜆ, ref this.ᜇ, this.ᜅ.\u1717(), this.ᜅ.ᜣ(), A_1);
					num2 = 4;
					continue;
					IL_194:
					num2 = 7;
				}
			}
			IL_1EC:
			IL_1EE:
			this.ᜅ.ᜣ().Add(this.ᜄ.ᜇ(), null);
			this.ᜅ.ᜣ().Add(text2, null);
			this.ᜄ = null;
			return;
		}
		}
	}

	// Token: 0x06003931 RID: 14641 RVA: 0x001FD97C File Offset: 0x001FC97C
	internal void ᜇ(string A_0)
	{
		int a_ = 1;
		if (true)
		{
		}
		switch (0)
		{
		default:
		{
			this.\u1712 = new Dictionary<string, RelationsCollection>();
			RelationsCollection relationsCollection = new RelationsCollection();
			new RelationsCollection();
			IEnumerator enumerator = this.ᜊ.GetEnumerator();
			try
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						KeyValuePair<string, sprᦨ> keyValuePair;
						if (keyValuePair.Value.ᜃ() == RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㩔❖㱘㕚╜㉞ൠբ੤ᕦѨ੪ᥬᱮ彰ᱲݴၶ噸ᑺ᭼᥾쎆ﶒ뢖ꮘꮚ궜ꦞ躠톢삤쮦좨\udfaa쒬삮\udfb0삲\uddb4\udeb6즸좺銼쾾ꣀ뗂꫄돆鷈꫊꿌ꏎ듐", a_))
						{
							num = 6;
							continue;
						}
						goto IL_11A;
					}
					case 3:
						num = 7;
						continue;
					case 4:
						goto IL_11A;
					case 5:
					{
						if (!enumerator.MoveNext())
						{
							num = 3;
							continue;
						}
						KeyValuePair<string, sprᦨ> keyValuePair = (KeyValuePair<string, sprᦨ>)enumerator.Current;
						num = 0;
						continue;
					}
					case 6:
					{
						KeyValuePair<string, sprᦨ> keyValuePair;
						relationsCollection.ᜀ(keyValuePair.Value);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					}
					case 7:
						goto IL_145;
					}
					IL_C3:
					num = 5;
					continue;
					goto IL_C3;
					IL_11A:
					relationsCollection.ItemPath = this.ᜊ.ItemPath;
					num = 1;
				}
				IL_145:;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (disposable != null)
							{
								num = 2;
								continue;
							}
							goto IL_18B;
						case 1:
							goto IL_189;
						case 2:
							disposable.Dispose();
							num = 1;
							continue;
						}
						break;
					}
				}
				IL_189:
				IL_18B:;
			}
			this.\u1712.Add(A_0, relationsCollection);
			return;
		}
		}
	}

	// Token: 0x06003932 RID: 14642 RVA: 0x001FDB34 File Offset: 0x001FCB34
	internal void ᜀ(IWorksheet A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				Dictionary<string, RelationsCollection>.Enumerator enumerator;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if (this.\u1712.Count > 0)
					{
						num = 1;
						continue;
					}
					return;
				case 1:
					goto IL_103;
				case 3:
					num = 0;
					continue;
				case 4:
					try
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_F3;
							case 1:
								goto IL_EA;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_EA;
								default:
									if (false)
									{
									}
									break;
								}
								break;
							case 4:
							{
								if (!enumerator.MoveNext())
								{
									num = 1;
									continue;
								}
								KeyValuePair<string, RelationsCollection> keyValuePair = enumerator.Current;
								string key = keyValuePair.Key;
								int length = key.LastIndexOf('/');
								string a_ = key.Substring(0, length);
								this.ᜀ(A_0, a_, keyValuePair.Value);
								num = 3;
								continue;
							}
							}
							IL_CD:
							num = 4;
							continue;
							goto IL_CD;
							IL_EA:
							num = 0;
						}
						IL_F3:
						return;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					goto IL_103;
				}
				if (this.\u1712 != null)
				{
					num = 3;
					continue;
				}
				break;
				IL_103:
				enumerator = this.\u1712.GetEnumerator();
				num = 4;
			}
			return;
		}
		}
	}

	// Token: 0x06003933 RID: 14643 RVA: 0x001FDCAC File Offset: 0x001FCCAC
	internal void ᜁ(XlsChart A_0)
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
		spr\u2306 spr_u = this.ᜅ.\u1718();
		string text = this.ᜄ.ᜇ();
		int length = text.LastIndexOf('/');
		text.Substring(0, length);
		string a_ = sprវ.ᜁ(text);
		this.ᜊ = this.ᜅ.ᜇ(a_);
		XmlReader a_2 = UtilityMethods.ᜀ(this.ᜄ.ᜐ());
		spr_u.ᜁ(a_2, A_0);
	}

	// Token: 0x06003934 RID: 14644 RVA: 0x001FDD48 File Offset: 0x001FCD48
	public void ᜀ(XlsWorksheet A_0, Dictionary<int, int> A_1, Dictionary<XlsPivotCache, string> A_2)
	{
		int a_ = 6;
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
				throw new ArgumentNullException(RecordTableEnumerator.b("伻嘽┿❁ぃ", a_));
			}
		}
		if (true)
		{
		}
		this.ᜀ(A_0, A_1);
		this.ᜂ(A_0);
		this.ᜀ(A_0);
		this.ᜀ(A_0, null);
		this.ᜁ(A_0);
		this.ᜀ(A_0, A_2);
		this.ᜃ();
	}

	// Token: 0x06003935 RID: 14645 RVA: 0x001FDDD8 File Offset: 0x001FCDD8
	private void ᜁ(XlsWorksheet A_0)
	{
		int a_ = 11;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
			{
				if (!A_0.HasOleObjects)
				{
					num = 2;
					continue;
				}
				sprᜭ sprᜭ = (sprᜭ)A_0.OleObjects;
				List<IOleObject>.Enumerator enumerator = sprᜭ.GetEnumerator();
				num = 3;
				continue;
			}
			case 2:
				goto IL_144;
			case 3:
				goto IL_164;
			case 4:
				goto IL_4D;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 4;
			}
			else
			{
				num = 1;
			}
		}
		IL_4D:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉀⭂⁄≆㵈", a_));
		IL_144:
		return;
		IL_164:
		try
		{
			num = 1;
			for (;;)
			{
				switch (num)
				{
				case 2:
				{
					List<IOleObject>.Enumerator enumerator;
					if (!enumerator.MoveNext())
					{
						num = 6;
						continue;
					}
					sprᰑ sprᰑ = (sprᰑ)enumerator.Current;
					num = 4;
					continue;
				}
				case 3:
					goto IL_101;
				case 4:
				{
					sprᰑ sprᰑ;
					if (sprᰑ.ᜉ() == OleLinkType.Embed)
					{
						num = 5;
						continue;
					}
					break;
				}
				case 5:
				{
					sprᰑ sprᰑ;
					this.ᜀ(A_0, sprᰑ);
					num = 0;
					continue;
				}
				case 6:
					num = 3;
					continue;
				}
				IL_80:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_101;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				goto IL_80;
			}
			IL_101:
			return;
		}
		finally
		{
			List<IOleObject>.Enumerator enumerator;
			((IDisposable)enumerator).Dispose();
		}
	}

	// Token: 0x06003936 RID: 14646 RVA: 0x001FDF60 File Offset: 0x001FCF60
	private void ᜀ(XlsWorksheet A_0, sprᰑ A_1)
	{
		int a_ = 15;
		MemoryStream a_3;
		string text;
		for (;;)
		{
			spr\u1B7A a_2 = this.ᜅ.\u170D();
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					a_3 = (MemoryStream)A_1.ᜃ();
					num = 3;
					continue;
				case 1:
					if (true)
					{
					}
					A_1.ᜆ(spr\u20E9.ᜁ());
					num = 5;
					continue;
				case 2:
					if (A_1.ᜁ() == null)
					{
						num = 1;
						continue;
					}
					goto IL_8F;
				case 3:
					goto IL_67;
				case 4:
					goto IL_102;
				case 5:
					goto IL_8F;
				case 6:
					if (A_1.ᜈ())
					{
						num = 0;
						continue;
					}
					a_3 = (MemoryStream)this.ᜀ(A_1, a_2);
					num = 4;
					continue;
				}
				break;
				IL_8F:
				text = RecordTableEnumerator.b("㵄⭆晈⹊⁌ⵎ㑐㝒ㅔ㹖㝘㱚⹜灞", a_) + A_1.ᜁ();
				num = 6;
			}
		}
		IL_67:
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
		IL_102:
		this.ᜅ.\u1714().ᜀ(text, a_3, true, FileAttributes.Archive);
		this.ᜇ()[A_1.ᜌ()] = new sprᦨ('/' + text, A_1.\u1714());
		this.ᜅ.ᜡ()['/' + text] = A_1.ᜅ();
	}

	// Token: 0x06003937 RID: 14647 RVA: 0x001FE0D4 File Offset: 0x001FD0D4
	private Stream ᜀ(sprᰑ A_0, spr\u1B7A A_1)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			spr\u2604 spr_u;
			for (;;)
			{
				string a_2 = A_0.ᜁ();
				A_0.ᜃ().Position = 0L;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						int num2;
						int num3;
						if (num2 >= num3)
						{
							num = 3;
							continue;
						}
						spr\u20C3 spr_u20C;
						spr\u1FDC spr_u1FDC = spr_u.ᜇ().ᜀ(spr_u20C.ᜁ()[num2]);
						spr\u1FDC spr_u1FDC2 = spr_u20C.ᜁ(spr_u20C.ᜁ()[num2]);
						byte[] array = new byte[spr_u1FDC2.Length];
						spr_u1FDC2.Read(array, 0, array.Length);
						spr_u1FDC.Write(array, 0, array.Length);
						spr_u1FDC2.Close();
						spr_u1FDC2.Dispose();
						spr_u1FDC.Close();
						spr_u1FDC.Dispose();
						num2++;
						num = 5;
						continue;
					}
					case 1:
					{
						if (A_0.ᜃ() == null)
						{
							num = 2;
							continue;
						}
						spr\u2604 spr_u2 = new spr\u2604(A_0.ᜃ());
						spr\u20C3 spr_u20C = spr_u2.ᜇ().ᜅ(a_2);
						spr_u = new spr\u2604();
						spr_u.\u170D().ᜁ()[0].ᜀ(spr\u20E9.ᜂ());
						int num2 = 0;
						int num3 = spr_u20C.ᜁ().Length;
						num = 4;
						continue;
					}
					case 2:
						goto IL_69;
					case 3:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_15A;
						}
						goto Block_3;
					case 4:
						goto IL_15A;
					case 5:
						goto IL_15A;
					}
					break;
					IL_15A:
					num = 0;
				}
			}
			IL_69:
			throw new Exception(RecordTableEnumerator.b("ࡅ㵇♉⁋湍♏㍑㡓⍕㵗", a_));
			Block_3:
			if (false)
			{
			}
			MemoryStream memoryStream = new MemoryStream();
			spr_u.ᜂ(memoryStream);
			spr_u.ᜊ();
			memoryStream.Position = 0L;
			return memoryStream;
		}
		}
	}

	// Token: 0x06003938 RID: 14648 RVA: 0x001FE2AC File Offset: 0x001FD2AC
	public void ᜂ(XlsChart A_0)
	{
		int a_ = 8;
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
			if (A_0 != null)
			{
				this.ᜀ(A_0);
				this.ᜀ(A_0);
				this.ᜀ(A_0, null);
				this.ᜃ();
				return;
			}
			break;
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("崽⠿⍁㙃㉅", a_));
	}

	// Token: 0x06003939 RID: 14649 RVA: 0x001FE328 File Offset: 0x001FD328
	public RelationsCollection ᜀ(ShapeCollectionBase A_0, string A_1, RelationsCollection A_2)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 21;
			RelationsCollection relationsCollection;
			for (;;)
			{
				if (true)
				{
				}
				int num2;
				int num3;
				XlsWorksheet worksheet;
				int num4;
				XmlReader a_2;
				string text2;
				switch (num)
				{
				case 0:
					goto IL_FE;
				case 1:
				{
					IComments comments;
					num2 = comments.Count;
					goto IL_248;
				}
				case 2:
				{
					IComments comments;
					if (comments == null)
					{
						num = 13;
						continue;
					}
					num = 1;
					continue;
				}
				case 3:
					if (num3 > 0)
					{
						num = 17;
						continue;
					}
					return relationsCollection;
				case 4:
					(worksheet.Comments as XlsCommentsCollection).Clear();
					num = 22;
					continue;
				case 5:
					goto IL_2BA;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_329;
					default:
						if (false)
						{
						}
						num2 = 0;
						goto IL_248;
					}
					break;
				case 7:
					if (num4 >= 0)
					{
						num = 18;
						continue;
					}
					goto IL_BD;
				case 8:
				{
					sprᦨ sprᦨ;
					if (sprᦨ == null)
					{
						num = 4;
						continue;
					}
					string text;
					a_2 = this.ᜅ.ᜁ(sprᦨ, text);
					this.ᜅ.\u1718().\u1714(a_2, worksheet);
					num = 16;
					continue;
				}
				case 9:
					goto IL_BD;
				case 10:
					if (worksheet != null)
					{
						num = 14;
						continue;
					}
					return relationsCollection;
				case 11:
					goto IL_A0;
				case 12:
					if (A_2 != null)
					{
						num = 19;
						continue;
					}
					return relationsCollection;
				case 13:
					num = 6;
					continue;
				case 14:
				{
					IComments comments = worksheet.Comments;
					num = 2;
					continue;
				}
				case 15:
				{
					sprᦨ sprᦨ2;
					if (sprᦨ2 == null)
					{
						num = 5;
						continue;
					}
					string text = Path.GetDirectoryName(this.ᜄ.ᜇ());
					text = text.Replace('\\', '/');
					a_2 = this.ᜅ.ᜁ(sprᦨ2, text, out text2);
					string a_3 = sprវ.ᜁ(text2);
					relationsCollection = this.ᜅ.ᜇ(a_3);
					num4 = text2.LastIndexOf('/');
					num = 7;
					continue;
				}
				case 16:
					goto IL_1E3;
				case 17:
				{
					sprᦨ sprᦨ = this.ᜊ.ᜀ(RecordTableEnumerator.b("ⱃ㉅㱇㩉癋慍罏⅑㝓㹕㵗㝙㵛ⵝ也ൡᑣͥ٧ቩūɭᙯᵱٳ᭵᥷๹ཻ偽ꦅ킓秊ﮗﮝ캟횡讣钥颧骩骫膭슯ힱ\ud8b3ힵ첷펹펻킽뎿꫁귃뛅믇꿋ꇍ뷏뿑뇓룕곗꧙", a_), out this.ᜏ);
					num = 8;
					continue;
				}
				case 18:
					goto IL_329;
				case 19:
				{
					sprᦨ sprᦨ2 = A_2[A_1];
					num = 15;
					continue;
				}
				case 20:
					if (A_2 == null)
					{
						num = 23;
						continue;
					}
					goto IL_FE;
				case 22:
					goto IL_243;
				case 23:
					A_2 = this.ᜊ;
					num = 0;
					continue;
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
				}
				relationsCollection = null;
				num = 20;
				continue;
				IL_BD:
				this.ᜅ.\u1718().ᜀ(a_2, A_0, relationsCollection, text2);
				worksheet = A_0.Worksheet;
				num = 10;
				continue;
				IL_FE:
				num = 12;
				continue;
				IL_248:
				num3 = num2;
				num = 3;
				continue;
				IL_329:
				text2 = text2.Substring(0, num4);
				num = 9;
			}
			IL_A0:
			throw new ArgumentNullException(RecordTableEnumerator.b("㝃⹅ⵇ⽉㡋", a_));
			IL_1E3:
			IL_243:
			return relationsCollection;
			IL_2BA:
			throw new ArgumentException(RecordTableEnumerator.b("㙃⍅⑇⭉㡋❍㽏㱑ᵓ㉕", a_));
		}
		}
	}

	// Token: 0x0600393A RID: 14650 RVA: 0x001FE694 File Offset: 0x001FD694
	internal void ᜀ(XlsWorksheetBase A_0, string A_1, sprᰑ A_2)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 3;
			byte[] buffer;
			string text2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					sprᦨ sprᦨ;
					if (sprᦨ == null)
					{
						num = 1;
						continue;
					}
					string text = Path.GetDirectoryName(this.ᜄ.ᜇ());
					text = text.Replace('\\', '/');
					buffer = A_0.DataHolder.ᜋ().ᜀ(sprᦨ, text, true);
					text2 = sprᦨ.ᜂ();
					text2 = sprវ.ᜀ(text, text2);
					text2 = text2.Replace('\\', '/');
					string a_2 = this.ᜅ.ᜉ('/' + text2);
					A_2.ᜄ(a_2);
					A_2.ᜅ(sprᦨ.ᜃ());
					num = 2;
					continue;
				}
				case 1:
					goto IL_222;
				case 2:
					if (A_2.ᜏ() == spr\u20E9.ᜁ(RecordTableEnumerator.b("紸吺帼䨾ⱀ♂⭄㍆", a_)))
					{
						num = 6;
						continue;
					}
					A_2.ᜀ(new MemoryStream(buffer));
					A_2.ᜃ().Position = 0L;
					A_2.ᜂ(true);
					A_2.ᜀ(new byte[0]);
					A_2.ᜆ(Path.GetFileName(text2));
					num = 7;
					continue;
				case 4:
				{
					sprᦨ sprᦨ = this.ᜊ[A_1];
					num = 0;
					continue;
				}
				case 5:
					if (this.ᜊ != null)
					{
						num = 4;
						continue;
					}
					goto IL_227;
				case 6:
					goto IL_10C;
				case 7:
					goto IL_19B;
				case 8:
					goto IL_5C;
				}
				if (A_0 == null)
				{
					num = 8;
				}
				else
				{
					num = 5;
				}
			}
			IL_5C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨸区堼娾㕀", a_));
			IL_10C:
			A_2.ᜀ(new MemoryStream(buffer));
			A_2.ᜃ().Position = 0L;
			A_2.ᜂ(true);
			A_2.ᜀ(new byte[0]);
			text2 = Path.GetFileName(text2);
			A_2.ᜆ(text2);
			return;
			IL_19B:
			if (true)
			{
			}
			goto IL_227;
			IL_222:
			throw new ArgumentException(RecordTableEnumerator.b("䬸帺儼帾㕀⩂⩄⥆H⽊", a_));
			IL_227:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_222;
			default:
				if (false)
				{
				}
				return;
			}
			break;
		}
		}
	}

	// Token: 0x0600393B RID: 14651 RVA: 0x001FE8E4 File Offset: 0x001FD8E4
	private bool ᜀ(string A_0)
	{
		int a_ = 13;
		bool result;
		for (;;)
		{
			result = false;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					if (spr\u22D2.ᜦ.TryGetValue(A_0, out num2))
					{
						num = 7;
						continue;
					}
					return result;
				}
				case 1:
					spr\u22D2.ᜦ = new Dictionary<string, int>(8)
					{
						{
							RecordTableEnumerator.b("ፂ⩄うⱈ㥊ᵌ⁎㡐㵒⅔祖੘㍚㉜⡞你孢", a_),
							0
						},
						{
							RecordTableEnumerator.b("ፂ⩄うⱈ㥊ᵌ⁎㡐㵒⅔祖੘㝚㑜㭞Ѡ䵢呤啦", a_),
							1
						},
						{
							RecordTableEnumerator.b("ፂ⩄うⱈ㥊ᵌ⁎㡐㵒⅔祖੘㍚㉜⡞你剢坤", a_),
							2
						},
						{
							RecordTableEnumerator.b("ᑂ⩄㕆ⵈ敊ौ⁎㉐♒㡔㉖㝘⽚ၜ㹞ɠᅢ੤≦ݨ੪ཬͮᑰᝲ孴䙶䭸", a_),
							3
						},
						{
							RecordTableEnumerator.b("ፂ⩄うⱈ㥊ᵌ⁎㡐㵒⅔祖੘㝚㑜㭞Ѡ⹢ѤѦ᭨Ѫ⡬Ůၰᅲᥴቶᵸ啺䱼䵾", a_),
							4
						},
						{
							RecordTableEnumerator.b("ᑂ⩄㕆ⵈ敊ौ⁎㉐♒㡔㉖㝘⽚獜湞占", a_),
							5
						},
						{
							RecordTableEnumerator.b("ፂ⩄うⱈ㥊ᵌ⁎㡐㵒⅔祖੘㍚㉜⡞Ⱡɢ٤ᕦ٨⹪ͬ๮፰ὲၴ፶坸䩺佼", a_),
							6
						},
						{
							RecordTableEnumerator.b("ᑂ⩄㕆ⵈ敊ौ⁎㉐♒㡔㉖㝘⽚獜杞", a_),
							7
						}
					};
					num = 9;
					continue;
				case 2:
					num = 8;
					continue;
				case 3:
					if (A_0 != null)
					{
						num = 2;
						continue;
					}
					return result;
				case 4:
				{
					int num2;
					switch (num2)
					{
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
					case 6:
					case 7:
						return true;
					default:
						num = 6;
						continue;
					}
					break;
				}
				case 5:
					goto IL_65;
				case 6:
					if (true)
					{
					}
					num = 5;
					continue;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C6;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 8:
					if (spr\u22D2.ᜦ == null)
					{
						goto IL_C6;
					}
					goto IL_6A;
				case 9:
					goto IL_6A;
				}
				break;
				IL_6A:
				num = 0;
				continue;
				IL_C6:
				num = 1;
			}
		}
		IL_65:
		return result;
	}

	// Token: 0x0600393C RID: 14652 RVA: 0x001FEADC File Offset: 0x001FDADC
	public void ᜀ(XlsWorksheetBase A_0, string A_1, Dictionary<string, object> A_2)
	{
		int a_ = 10;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				sprᦨ a_2 = this.ᜊ[A_1];
				this.ᜀ(A_0, a_2, A_2);
				num = 3;
				continue;
			}
			case 2:
				goto IL_38;
			case 3:
				goto IL_62;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					if (this.ᜊ != null)
					{
						num = 0;
						continue;
					}
					return;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				num = 4;
			}
		}
		IL_38:
		throw new ArgumentNullException(RecordTableEnumerator.b("㌿⩁⅃⍅㱇", a_));
		IL_62:
		if (true)
		{
		}
	}

	// Token: 0x0600393D RID: 14653 RVA: 0x001FEBA4 File Offset: 0x001FDBA4
	public void ᜀ(XlsWorksheetBase A_0, sprᦨ A_1, Dictionary<string, object> A_2)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			int num = 8;
			for (;;)
			{
				List<string> list;
				RelationsCollection relationsCollection;
				switch (num)
				{
				case 0:
					goto IL_64;
				case 1:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					string id = list[num2];
					this.ᜋ.Remove(id);
					num2++;
					num = 6;
					continue;
				}
				case 2:
					goto IL_1A3;
				case 3:
				{
					int num2 = 0;
					int count = list.Count;
					num = 7;
					continue;
				}
				case 4:
					this.ᜋ = relationsCollection;
					num = 5;
					continue;
				case 5:
					goto IL_83;
				case 6:
					goto IL_185;
				case 7:
					goto IL_185;
				case 9:
					if (relationsCollection != null)
					{
						num = 4;
						continue;
					}
					goto IL_83;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1A5;
					default:
						if (false)
						{
						}
						if (this.ᜋ != null)
						{
							num = 3;
							continue;
						}
						return;
					}
					break;
				}
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				string text = Path.GetDirectoryName(this.ᜄ.ᜇ());
				text = text.Replace('\\', '/');
				XmlReader a_2 = this.ᜅ.ᜁ(A_1, text);
				string a_3 = sprវ.ᜀ(text, A_1.ᜂ());
				spr\u2306 spr_u = this.ᜅ.\u1718();
				string a_4 = sprវ.ᜁ(a_3);
				relationsCollection = this.ᜅ.ᜇ(a_4);
				if (true)
				{
				}
				num = 9;
				continue;
				IL_83:
				string a_5;
				sprវ.ᜀ(a_3, out a_5);
				list = new List<string>();
				spr_u.ᜄ(a_2, A_0, a_5, list, A_2);
				spr\u249E spr_u249E = this.ᜅ.\u1714();
				spr_u249E.ᜀ(a_4);
				spr_u249E.ᜀ(a_3);
				num = 10;
				continue;
				IL_185:
				num = 1;
			}
			IL_64:
			goto IL_1A5;
			IL_1A3:
			return;
			IL_1A5:
			throw new ArgumentException(RecordTableEnumerator.b("㭈⹊⅌⹎═㩒㩔㥖ၘ㽚", a_));
		}
		}
	}

	// Token: 0x0600393E RID: 14654 RVA: 0x001FEDBC File Offset: 0x001FDDBC
	private void ᜀ(XlsWorksheet A_0, Dictionary<int, int> A_1)
	{
		int a_ = 15;
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
			if (A_0 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("㙄⽆ⱈ⹊㥌", a_));
			}
			break;
		}
		spr\u1B7A spr_u1B7A = this.ᜅ.\u170D();
		spr\u1F5E spr_u1F5E = new spr\u1F5E(new spr\u249E.ᜀ(A_0.AppImplementation.ᜂ));
		StreamWriter a_2 = new StreamWriter(spr_u1F5E);
		XmlWriter xmlWriter = UtilityMethods.ᜀ(a_2);
		spr_u1B7A.ᜀ(xmlWriter, A_0, this.ᜆ, this.ᜇ, A_1);
		xmlWriter.Flush();
		spr_u1F5E.Flush();
		this.ᜄ.ᜀ(spr_u1F5E);
	}

	// Token: 0x0600393F RID: 14655 RVA: 0x001FEE78 File Offset: 0x001FDE78
	private void ᜀ(XlsChart A_0)
	{
		int a_ = 19;
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
			if (A_0 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("⩈⍊ⱌ㵎═", a_));
			}
			break;
		}
		string text = this.ᜂ();
		string text2 = this.ᜇ().GenerateRelationId();
		this.ᜊ[text2] = new sprᦨ('/' + text, RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊ朗\udd98ﺜ철욢쮤펦蚨馪鶬龮螰鲲잴튶햸\udaba즼횾껀귂뛄꿆ꃈ믊뻌뗐ꇒ듔ꃖ냘뗚뫜", a_));
		this.ᜅ.ᜡ()['/' + text] = RecordTableEnumerator.b("⡈㭊㵌⍎㡐げ㑔⍖じ㑚㍜灞ᝠൢŤ䥦٨᭪࡬Ů॰ṲᥴᅶᙸॺၼṾꢄ杖햠趢솤햦좨\udcaa쒬솮횰颲춴\udab6햸", a_);
		spr\u2541 spr_u = new spr\u2541();
		spr\u1F5E spr_u1F5E = new spr\u1F5E(new spr\u249E.ᜀ(A_0.AppImplementation.ᜂ));
		StreamWriter a_2 = new StreamWriter(spr_u1F5E);
		XmlWriter xmlWriter = UtilityMethods.ᜀ(a_2);
		spr_u.ᜃ(xmlWriter, A_0, text2);
		xmlWriter.Flush();
		spr_u1F5E.Flush();
		this.ᜄ.ᜀ(spr_u1F5E);
		RelationsCollection relationsCollection = new RelationsCollection();
		string a_3 = this.ᜀ(A_0, text, relationsCollection);
		this.ᜀ(A_0, relationsCollection, a_3);
		this.ᜀ(relationsCollection, text);
	}

	// Token: 0x06003940 RID: 14656 RVA: 0x001FEFB8 File Offset: 0x001FDFB8
	private void ᜀ(XlsChart A_0, RelationsCollection A_1, string A_2)
	{
		int a_ = 2;
		if (true)
		{
		}
		string text;
		MemoryStream memoryStream;
		StreamWriter streamWriter;
		XmlWriter xmlWriter;
		spr\u2541 spr_u;
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
					text = spr\u21A8.ᜀ(this, A_0);
					A_1[A_2] = new sprᦨ(text, RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹፻᡽첇ﮍﶏ望랗ꢙ겛꺝隟趡횣쎥쒧쮩\ud8ab잭\udfaf\udcb1잳\udeb5톷쪹쾻醽ꎿ꫁ꗃ듅볇", a_));
					this.ᜅ.ᜡ()[text] = RecordTableEnumerator.b("夷䨹䰻刽⤿⅁╃㉅ⅇ╉≋慍♏㱑こ硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű女᥵ṷᱹᕻᵽﶇ벑聯잟쾡좣袥쮧슩춫\udcad쒯馱첳\udbb5풷", a_);
					memoryStream = new MemoryStream();
					streamWriter = new StreamWriter(memoryStream);
					xmlWriter = UtilityMethods.ᜀ(streamWriter);
					spr_u = new spr\u2541();
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.ᜅ.ᜁ(A_0, text);
							num = 1;
							continue;
						case 1:
							goto IL_EF;
						case 2:
							if (A_0.DataHolder == null)
							{
								num = 0;
								continue;
							}
							goto IL_F1;
						}
						break;
					}
				}
				IL_EF:
				break;
			}
			break;
		}
		IL_F1:
		spr_u.ᜁ(xmlWriter, A_0, text);
		xmlWriter.Flush();
		streamWriter.Flush();
		text = UtilityMethods.ᜀ(text);
		this.ᜅ.\u1714().ᜀ(text, memoryStream, true, FileAttributes.Archive);
		this.ᜀ(A_0.Relations, text);
	}

	// Token: 0x06003941 RID: 14657 RVA: 0x001FF0F8 File Offset: 0x001FE0F8
	private string ᜀ(XlsChart A_0, string A_1, RelationsCollection A_2)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			for (;;)
			{
				IL_17:
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_86;
					case 1:
						if (A_1.Length == 0)
						{
							num = 5;
							continue;
						}
						goto IL_10F;
					case 2:
						if (A_1 != null)
						{
							num = 4;
							continue;
						}
						goto IL_BE;
					case 3:
						goto IL_ED;
					case 4:
						num = 1;
						continue;
					case 5:
						goto IL_A2;
					case 7:
						if (A_2 == null)
						{
							num = 3;
							continue;
						}
						num = 2;
						continue;
					}
					if (A_0 == null)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 0;
							break;
						}
					}
					else
					{
						num = 7;
					}
				}
			}
			IL_86:
			throw new ArgumentNullException(RecordTableEnumerator.b("夹吻弽㈿㙁", a_));
			IL_A2:
			IL_BE:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("帹主弽㜿⭁⩃ⅅŇ㹉⥋⍍ṏ㍑㥓㍕", a_));
			IL_ED:
			throw new ArgumentNullException();
			IL_10F:
			ShapeCollectionBase innerShapesBase = A_0.InnerShapesBase;
			string text = A_2.GenerateRelationId();
			A_2[text] = null;
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream);
			XmlWriter xmlWriter = UtilityMethods.ᜀ(streamWriter);
			spr\u2541 spr_u = new spr\u2541();
			spr_u.ᜂ(xmlWriter, A_0, text);
			xmlWriter.Flush();
			streamWriter.Flush();
			this.ᜅ.\u1714().ᜀ(A_1, memoryStream, true, FileAttributes.Archive);
			return text;
		}
		}
	}

	// Token: 0x06003942 RID: 14658 RVA: 0x001FF274 File Offset: 0x001FE274
	private void ᜀ(XlsWorksheet A_0, Dictionary<XlsPivotCache, string> A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				for (;;)
				{
					PivotTablesCollection pivotTablesCollection = A_0.InnerPivotTables;
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_DF;
						case 1:
						{
							int num2 = 0;
							int count = pivotTablesCollection.Count;
							num = 6;
							continue;
						}
						case 2:
							if (pivotTablesCollection.Count > 0)
							{
								num = 1;
								continue;
							}
							return;
						case 3:
							num = 2;
							continue;
						case 4:
						{
							int num2;
							int count;
							if (num2 >= count)
							{
								if (true)
								{
								}
								num = 7;
								continue;
							}
							XlsPivotTable a_ = (XlsPivotTable)pivotTablesCollection[num2];
							this.ᜀ(a_, A_1);
							num2++;
							num = 0;
							continue;
						}
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								if (pivotTablesCollection != null)
								{
									num = 3;
									continue;
								}
								return;
							}
							break;
						case 6:
							goto IL_DF;
						case 7:
							return;
						}
						break;
						IL_DF:
						num = 4;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06003943 RID: 14659 RVA: 0x001FF388 File Offset: 0x001FE388
	private void ᜀ(XlsPivotTable A_0, Dictionary<XlsPivotCache, string> A_1)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 6;
			string text;
			MemoryStream memoryStream;
			XmlWriter xmlWriter;
			for (;;)
			{
				int num2;
				PivotReportFilter pivotReportFilter;
				int num4;
				switch (num)
				{
				case 0:
				{
					if (num2 >= pivotReportFilter.FilterItemStrings.Count)
					{
						num = 15;
						continue;
					}
					int num3 = A_0.Cache.CacheFields.ᜀ(pivotReportFilter.FieldIndex).Items.IndexOf(pivotReportFilter.FilterItemStrings[num2]);
					num = 20;
					continue;
				}
				case 1:
					if (pivotReportFilter.IsMultipleSelect)
					{
						num = 14;
						continue;
					}
					(A_0.PageFields[num4] as XlsPivotField).ItemIndex = pivotReportFilter.ItemIndex;
					num = 13;
					continue;
				case 2:
					goto IL_223;
				case 3:
					goto IL_91;
				case 4:
					goto IL_21E;
				case 5:
					goto IL_8C;
				case 7:
					goto IL_337;
				case 8:
					goto IL_25B;
				case 9:
				{
					int num3;
					A_0.PivotFields[pivotReportFilter.FieldIndex].ItemOptions[num3].ᜃ(false);
					num = 2;
					continue;
				}
				case 10:
					goto IL_337;
				case 11:
					num = 8;
					continue;
				case 12:
					goto IL_1F5;
				case 13:
					goto IL_25B;
				case 14:
				{
					int num3 = 0;
					num2 = 0;
					num = 7;
					continue;
				}
				case 15:
				{
					int num5 = 0;
					num = 3;
					continue;
				}
				case 16:
					goto IL_91;
				case 17:
					if (num4 >= A_0.ReportFilters.Count)
					{
						num = 4;
						continue;
					}
					pivotReportFilter = A_0.ReportFilters[num4];
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_288;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 18:
				{
					if (true)
					{
					}
					int num5;
					if (num5 >= pivotReportFilter.RemovedStrings.Count)
					{
						num = 11;
						continue;
					}
					int num3 = A_0.Cache.CacheFields.ᜀ(pivotReportFilter.FieldIndex).Items.IndexOf(pivotReportFilter.RemovedStrings[num5]);
					A_0.PivotFields[pivotReportFilter.FieldIndex].ItemOptions[num3].ᜃ(true);
					num5++;
					num = 16;
					continue;
				}
				case 19:
					goto IL_288;
				case 20:
				{
					int num3;
					if (num3 != -1)
					{
						num = 9;
						continue;
					}
					goto IL_223;
				}
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				text = this.ᜅ.\u1716();
				memoryStream = new MemoryStream();
				xmlWriter = UtilityMethods.ᜀ(memoryStream, Encoding.UTF8);
				num4 = 0;
				num = 12;
				continue;
				IL_91:
				num = 18;
				continue;
				IL_1F5:
				num = 17;
				continue;
				IL_288:
				goto IL_1F5;
				IL_223:
				num2++;
				num = 10;
				continue;
				IL_25B:
				(A_0.PageFields[num4] as XlsPivotField).FieldIndex = pivotReportFilter.FieldIndex;
				num4++;
				num = 19;
				continue;
				IL_337:
				num = 0;
			}
			IL_8C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㉅⥇⡉⁋⭍", a_));
			IL_21E:
			spr\u2514.\u170D(xmlWriter, A_0);
			xmlWriter.Flush();
			this.ᜅ.\u1714().ᜀ(text, memoryStream, true, FileAttributes.Archive);
			this.ᜅ.ᜃ(text, RecordTableEnumerator.b("❅㡇㩉⁋❍㍏㍑⁓㽕㝗㑙獛⡝๟١䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽꾁﶑ﾙ躟톡풣풥춧쮩좫\uddad\ud8afힱ톳습햷횹銻캽ꦿ듁ꯃ닅鳇ꯉ껋ꋍ뗏六곓믕듗", a_));
			string a_2 = this.ᜇ().GenerateRelationId();
			this.ᜇ()[a_2] = new sprᦨ('/' + text, RecordTableEnumerator.b("⹅㱇㹉㱋瑍罏絑❓㕕し㽙ㅛ㽝፟䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ깿ꞇ憐튕蓮얟첡킣覥骧骩鲫颭龯삱톳\udab5\ud9b7캹햻톽꺿뇁곃꿅룇막뻍맏ꓑ믓ꋕ賗믙뻛닝藟", a_));
			RelationsCollection relationsCollection = new RelationsCollection();
			string a_3 = relationsCollection.GenerateRelationId();
			string arg = A_1[A_0.Cache];
			relationsCollection[a_3] = new sprᦨ('/' + arg, RecordTableEnumerator.b("⹅㱇㹉㱋瑍罏絑❓㕕し㽙ㅛ㽝፟䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ깿ꞇ憐튕蓮얟첡킣覥骧骩鲫颭龯삱톳\udab5\ud9b7캹햻톽꺿뇁곃꿅룇막뻍맏ꓑ믓ꋕ鯗믙뿛뛝藟ꛡ臣胥臧蓩藫髭駯鷱髳", a_));
			this.ᜅ.ᜀ(text, relationsCollection);
			return;
		}
		}
	}

	// Token: 0x06003944 RID: 14660 RVA: 0x001FF7C8 File Offset: 0x001FE7C8
	private void ᜃ()
	{
		int a_ = 14;
		switch (0)
		{
		default:
			for (;;)
			{
				IL_21:
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						num = 4;
						continue;
					case 1:
						return;
					case 2:
					{
						string text = this.ᜄ.ᜇ();
						int startIndex = text.LastIndexOf('/');
						string a_2 = text.Insert(startIndex, '/' + RecordTableEnumerator.b("ᭃ㑅ⵇ♉㽋", a_)) + RecordTableEnumerator.b("橃㑅ⵇ♉㽋", a_);
						MemoryStream memoryStream = new MemoryStream();
						StreamWriter streamWriter = new StreamWriter(memoryStream);
						XmlWriter xmlWriter = UtilityMethods.ᜀ(streamWriter);
						spr\u1B7A spr_u1B7A = this.ᜅ.\u170D();
						spr_u1B7A.ᜁ(xmlWriter, this.ᜊ);
						xmlWriter.Flush();
						streamWriter.Flush();
						this.ᜅ.\u1714().ᜀ(a_2, memoryStream, true, FileAttributes.Archive);
						num = 1;
						continue;
					}
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_21;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 4:
						if (this.ᜊ.Count > 0)
						{
							num = 2;
							continue;
						}
						return;
					}
					if (this.ᜊ == null)
					{
						return;
					}
					num = 0;
				}
			}
			return;
		}
	}

	// Token: 0x06003945 RID: 14661 RVA: 0x001FF938 File Offset: 0x001FE938
	private void ᜂ(XlsWorksheetBase A_0)
	{
		int a_ = 5;
		int num = 11;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_1B4;
			case 1:
				this.ᜊ.Remove(this.ᜐ);
				this.ᜐ = null;
				num = 5;
				continue;
			case 2:
				if (A_0 is IWorksheet)
				{
					num = 9;
					continue;
				}
				goto IL_1D9;
			case 3:
				if (!A_0.UnknownVmlShapes)
				{
					num = 7;
					continue;
				}
				goto IL_1D9;
			case 4:
			{
				int count;
				if (count == 0)
				{
					num = 6;
					continue;
				}
				goto IL_1D9;
			}
			case 5:
				goto IL_191;
			case 6:
				num = 2;
				continue;
			case 7:
				num = 13;
				continue;
			case 8:
				goto IL_70;
			case 9:
				num = 3;
				continue;
			case 10:
				if (true)
				{
				}
				num = 15;
				continue;
			case 12:
				if (A_0.UnknownVmlShapes)
				{
					num = 17;
					continue;
				}
				return;
			case 13:
				if (this.ᜐ != null)
				{
					num = 0;
					continue;
				}
				return;
			case 14:
				if (!this.ᜁ(A_0))
				{
					num = 10;
					continue;
				}
				return;
			case 15:
				if (this.ᜐ != null)
				{
					num = 1;
					continue;
				}
				return;
			case 16:
				goto IL_75;
			case 17:
				goto IL_13B;
			case 18:
			{
				int count;
				if (count == 0)
				{
					num = 16;
					continue;
				}
				goto IL_13B;
			}
			}
			if (A_0 == null)
			{
				num = 8;
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
				int count = A_0.Shapes.Count;
				num = 4;
				continue;
			}
			}
			IL_75:
			num = 12;
			continue;
			IL_13B:
			this.ᜀ(A_0);
			num = 14;
			continue;
			IL_1D9:
			num = 18;
		}
		IL_70:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠺唼娾⑀㝂", a_));
		IL_191:
		return;
		IL_1B4:
		this.ᜊ.Remove(this.ᜐ);
		this.ᜐ = null;
	}

	// Token: 0x06003946 RID: 14662 RVA: 0x001FFB64 File Offset: 0x001FEB64
	private bool ᜁ(XlsWorksheetBase A_0)
	{
		int a_ = 16;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜀ(A_0, this.ᜇ(), this.ᜐ, RecordTableEnumerator.b("❅㡇㩉⁋❍㍏㍑⁓㽕㝗㑙獛⡝๟١䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽꾁﶑ﾙ躟욡횣장\udfa7쎩슫즭鮯쪱\ud9b3\udab5", a_), RecordTableEnumerator.b("⹅㱇㹉㱋瑍罏絑❓㕕し㽙ㅛ㽝፟䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ깿ꞇ憐튕蓮얟첡킣覥骧骩鲫颭龯삱톳\udab5\ud9b7캹햻톽꺿뇁곃꿅룇막꫍ꋏ돑ꏓ뿕뛗뷙", a_));
	}

	// Token: 0x06003947 RID: 14663 RVA: 0x001FFBD8 File Offset: 0x001FEBD8
	public bool ᜀ(XlsWorksheetBase A_0, RelationsCollection A_1, string A_2, string A_3, string A_4)
	{
		int a_ = 12;
		int num;
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
			switch (num)
			{
			default:
				num = 11;
				break;
			}
			break;
		}
		for (;;)
		{
			XlsWorksheet xlsWorksheet;
			int num2;
			spr\u1D9B spr_u1D9B;
			int num3;
			string text;
			switch (num)
			{
			case 0:
				return true;
			case 1:
				num2 = xlsWorksheet.AutoFilters.Count;
				goto IL_236;
			case 2:
				num = 4;
				continue;
			case 3:
				goto IL_90;
			case 4:
				num2 = 0;
				goto IL_236;
			case 5:
				num = 6;
				continue;
			case 6:
				if (!spr\u1B7A.ᜀ(spr_u1D9B))
				{
					num = 7;
					continue;
				}
				goto IL_95;
			case 7:
				return false;
			case 8:
				if (this.ᜋ.Count > 0)
				{
					num = 13;
					continue;
				}
				return true;
			case 9:
				if (this.ᜋ != null)
				{
					num = 14;
					continue;
				}
				return true;
			case 10:
				if (xlsWorksheet == null)
				{
					num = 2;
					continue;
				}
				num = 1;
				continue;
			case 12:
				if (spr_u1D9B.Count - A_0.VmlShapesCount - num3 <= 0)
				{
					num = 5;
					continue;
				}
				goto IL_95;
			case 13:
				this.ᜀ(this.ᜋ, text);
				num = 0;
				continue;
			case 14:
				num = 8;
				continue;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			spr_u1D9B = A_0.InnerShapes;
			xlsWorksheet = (A_0 as XlsWorksheet);
			if (true)
			{
			}
			num = 10;
			continue;
			IL_95:
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream);
			XmlWriter xmlWriter = UtilityMethods.ᜀ(streamWriter);
			spr\u1B7A spr_u1B7A = this.ᜅ.\u170D();
			spr_u1B7A.ᜀ(xmlWriter, spr_u1D9B, this);
			xmlWriter.Flush();
			streamWriter.Flush();
			memoryStream.Flush();
			text = this.ᜂ();
			this.ᜅ.\u1714().ᜀ(text, memoryStream, true, FileAttributes.Archive);
			string text2 = '/' + text;
			A_1[A_2] = new sprᦨ(text2, A_4);
			this.ᜅ.ᜡ()[text2] = A_3;
			num = 9;
			continue;
			IL_236:
			num3 = num2;
			num = 12;
		}
		IL_90:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅁⱃ⍅ⵇ㹉", a_));
	}

	// Token: 0x06003948 RID: 14664 RVA: 0x001FFE50 File Offset: 0x001FEE50
	private void ᜀ(XlsWorksheetBase A_0)
	{
		int a_ = 5;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_6E:
			num = 2;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				num = 5;
				break;
			}
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				this.ᜊ.Remove(this.\u170D);
				this.\u170D = null;
				num = 0;
				continue;
			case 2:
				goto IL_77;
			case 3:
				if (!A_0.HasVmlShapes)
				{
					num = 6;
					continue;
				}
				goto IL_F7;
			case 4:
				if (this.\u170D != null)
				{
					num = 1;
					continue;
				}
				return;
			case 6:
				num = 4;
				continue;
			}
			if (A_0 == null)
			{
				goto IL_6E;
			}
			num = 3;
		}
		IL_77:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䠺唼娾⑀㝂", a_));
		IL_F7:
		this.ᜅ.\u171A()[RecordTableEnumerator.b("䴺值匾", a_)] = RecordTableEnumerator.b("娺䴼伾ⵀ⩂♄♆㵈≊≌ⅎ繐╒㭔㍖睘㑚ⵜ㩞འ᭢ࡤ୦ཨѪὬɮၰݲٴ婶ᙸᵺ᭼ᙾﺊﾐ뮔\ud99c삠풢첤즦캨", a_);
		spr\u1B7A spr_u1B7A = this.ᜅ.\u170D();
		string text = this.ᜁ();
		RelationsCollection relationsCollection = new RelationsCollection();
		MemoryStream memoryStream = new MemoryStream();
		StreamWriter streamWriter = new StreamWriter(memoryStream);
		XmlWriter a_2 = UtilityMethods.ᜀ(streamWriter, true);
		spr_u1B7A.ᜀ(a_2, A_0.InnerShapes, this, spr_u1B7A.ᜄ(), relationsCollection);
		streamWriter.Flush();
		memoryStream.Flush();
		this.ᜅ.\u1714().ᜀ(text, memoryStream, true, FileAttributes.Archive);
		this.ᜇ()[this.\u170D] = new sprᦨ('/' + text, RecordTableEnumerator.b("区䤼䬾ㅀ祂橄框㩈⡊╌⩎㱐㉒♔祖㙘⭚㡜ㅞᥠ๢।Ŧ٨ᥪl๮հr孴ᡶ୸ᱺ剼ၾ쾊ﺒ練뒚꾜꾞醠関誤햦첨잪첬\udbae\ud8b0\udcb2\udbb4쒶톸튺춼첾뗂꣄ꯆ跈맊곌룎룐뷒닔", a_));
		this.ᜀ(relationsCollection, text);
	}

	// Token: 0x06003949 RID: 14665 RVA: 0x00200024 File Offset: 0x001FF024
	internal void ᜀ(XlsWorksheetBase A_0, RelationsCollection A_1)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			int num = 1;
			XlsHeaderFooterShapeCollection xlsHeaderFooterShapeCollection;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 10;
					continue;
				case 2:
					goto IL_6F;
				case 3:
					if (this.ᜎ != null)
					{
						num = 8;
						continue;
					}
					return;
				case 4:
					goto IL_74;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6F;
					default:
						if (false)
						{
						}
						if (A_1 == null)
						{
							num = 9;
							continue;
						}
						goto IL_12C;
					}
					break;
				case 6:
					goto IL_12A;
				case 7:
					goto IL_12C;
				case 8:
					A_1.Remove(this.ᜎ);
					num = 6;
					continue;
				case 9:
					A_1 = this.ᜇ();
					num = 7;
					continue;
				case 10:
					if (xlsHeaderFooterShapeCollection.Count == 0)
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					goto IL_165;
				case 11:
					if (xlsHeaderFooterShapeCollection != null)
					{
						num = 0;
						continue;
					}
					goto IL_74;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				xlsHeaderFooterShapeCollection = A_0.InnerHeaderFooterShapes;
				num = 5;
				continue;
				IL_74:
				num = 3;
				continue;
				IL_12C:
				num = 11;
			}
			IL_6F:
			throw new ArgumentNullException(RecordTableEnumerator.b("あⵄ≆ⱈ㽊", a_));
			IL_12A:
			return;
			IL_165:
			this.ᜅ.\u171A()[RecordTableEnumerator.b("㕂⡄⭆", a_)] = RecordTableEnumerator.b("≂㕄㝆╈≊⹌⹎═㩒㩔㥖癘ⵚ㍜㭞你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼剾뎜철쾢햦좨\udcaa쒬솮횰", a_);
			spr\u1B7A spr_u1B7A = this.ᜅ.\u170D();
			string text = this.ᜁ();
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream);
			XmlWriter a_2 = UtilityMethods.ᜀ(streamWriter, true);
			spr_u1B7A.ᜀ(a_2, xlsHeaderFooterShapeCollection, this, spr_u1B7A.ᜃ(), A_1);
			streamWriter.Flush();
			memoryStream.Flush();
			this.ᜅ.\u1714().ᜀ(text, memoryStream, true, FileAttributes.Archive);
			A_1[this.ᜎ] = new sprᦨ('/' + text, RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄ힒杖햠貢鞤鞦馨鶪芬\uddae풰\udfb2풴쎶킸풺펼첾꧀ꫂ뗄듆뷊ꃌꏎ闐ꇒ듔ꃖ냘뗚뫜", a_));
			this.ᜀ(this.ᜌ, text);
			return;
		}
		}
	}

	// Token: 0x0600394A RID: 14666 RVA: 0x0020025C File Offset: 0x001FF25C
	internal void ᜈ(string A_0)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
				{
					MemoryStream memoryStream = new MemoryStream();
					StreamWriter streamWriter = new StreamWriter(memoryStream);
					XmlWriter xmlWriter = UtilityMethods.ᜀ(streamWriter);
					this.ᜅ.\u170D().ᜁ(xmlWriter, this.ᜊ);
					xmlWriter.Flush();
					streamWriter.Flush();
					memoryStream.Flush();
					int startIndex = A_0.LastIndexOf('/');
					string a_2 = A_0.Insert(startIndex, '/' + RecordTableEnumerator.b("ᵁ㙃⍅⑇㥉", a_)) + RecordTableEnumerator.b("汁㙃⍅⑇㥉", a_);
					this.ᜅ.\u1714().ᜀ(a_2, memoryStream, true, FileAttributes.Archive);
					num = 2;
					continue;
				}
				case 1:
					goto IL_FE;
				case 2:
					goto IL_136;
				case 4:
					if (this.ᜊ.Count > 0)
					{
						num = 0;
						continue;
					}
					goto IL_136;
				}
				if (this.ᜊ != null)
				{
					num = 1;
					continue;
				}
				goto IL_136;
				IL_FE:
				num = 4;
				continue;
				IL_136:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_FE;
				default:
					goto IL_14C;
				}
			}
			IL_14C:
			if (false)
			{
			}
			return;
		}
		}
	}

	// Token: 0x0600394B RID: 14667 RVA: 0x002003BC File Offset: 0x001FF3BC
	internal void ᜀ(RelationsCollection A_0, string A_1)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				int num2;
				MemoryStream memoryStream;
				switch (num)
				{
				case 0:
					if (A_1[0] == '/')
					{
						num = 6;
						continue;
					}
					goto IL_134;
				case 1:
					goto IL_134;
				case 2:
					if (A_0.Count > 0)
					{
						num = 8;
						continue;
					}
					return;
				case 3:
					if (A_0 != null)
					{
						num = 11;
						continue;
					}
					return;
				case 4:
					goto IL_1AE;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1D0;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						A_1 = UtilityMethods.ᜀ(A_1);
						num2--;
						num = 1;
						continue;
					}
					break;
				case 7:
					if (A_1.Length == 0)
					{
						num = 4;
						continue;
					}
					goto IL_1D0;
				case 8:
				{
					memoryStream = new MemoryStream();
					StreamWriter streamWriter = new StreamWriter(memoryStream);
					XmlWriter xmlWriter = UtilityMethods.ᜀ(streamWriter);
					this.ᜅ.\u170D().ᜁ(xmlWriter, A_0);
					xmlWriter.Flush();
					streamWriter.Flush();
					memoryStream.Flush();
					num2 = A_1.LastIndexOf('/');
					num = 0;
					continue;
				}
				case 9:
					num = 7;
					continue;
				case 10:
					goto IL_18C;
				case 11:
					num = 2;
					continue;
				}
				if (A_1 != null)
				{
					num = 9;
					continue;
				}
				break;
				IL_134:
				string a_2 = A_1.Insert(num2, '/' + RecordTableEnumerator.b("改主嬽ⰿㅁ", a_)) + RecordTableEnumerator.b("ᐹ主嬽ⰿㅁ", a_);
				this.ᜅ.\u1714().ᜀ(a_2, memoryStream, true, FileAttributes.Archive);
				num = 10;
				continue;
				IL_1D0:
				num = 3;
			}
			IL_12D:
			throw new ArgumentOutOfRangeException(A_1);
			IL_18C:
			return;
			IL_1AE:
			goto IL_12D;
		}
		}
	}

	// Token: 0x0600394C RID: 14668 RVA: 0x002005BC File Offset: 0x001FF5BC
	private void ᜀ(XlsWorksheet A_0)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					XlsCommentsCollection innerComments;
					if (innerComments.Count == 0)
					{
						num = 4;
						continue;
					}
					goto IL_CF;
				}
				case 1:
				{
					XlsCommentsCollection innerComments;
					if (innerComments != null)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					return;
				}
				case 2:
					num = 0;
					continue;
				case 3:
					goto IL_4D;
				case 4:
					return;
				}
				IL_41:
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
					XlsCommentsCollection innerComments = A_0.InnerComments;
					num = 1;
					continue;
				}
				}
				goto IL_41;
			}
			IL_4D:
			throw new ArgumentNullException(RecordTableEnumerator.b("㉀⭂⁄≆㵈", a_));
			IL_CF:
			string text = this.ᜀ();
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream);
			XmlWriter xmlWriter = UtilityMethods.ᜀ(streamWriter);
			spr\u1B7A spr_u1B7A = this.ᜅ.\u170D();
			spr_u1B7A.ᜎ(xmlWriter, A_0);
			xmlWriter.Flush();
			streamWriter.Flush();
			memoryStream.Flush();
			this.ᜅ.\u1714().ᜀ(text, memoryStream, true, FileAttributes.Archive);
			this.ᜇ()[this.ᜏ] = new sprᦨ('/' + text, RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂햐ﲒﺚ躠醢閤鞦龨蒪\udfac쪮\uddb0튲솴\udeb6횸햺캼ힾꣀ돂뛄꫈꓊ꃌꋎ듐뷒ꇔꓖ", a_));
			this.ᜅ.ᜡ()['/' + text] = RecordTableEnumerator.b("⁀㍂㕄⭆⁈⡊ⱌ㭎㡐㱒㭔硖⽘㕚㥜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ偼ၾﺒ練떚펠욢쒤쎦\udaa8쎪좬쪮얰\udeb2\ud9b4馶\udab8풺킼튾꓀귂뇄듆돊ꃌꏎ", a_);
			return;
		}
		}
	}

	// Token: 0x0600394D RID: 14669 RVA: 0x00200750 File Offset: 0x001FF750
	private string ᜂ()
	{
		int a_ = 13;
		string text;
		for (;;)
		{
			sprវ sprវ = this.ᜅ;
			int num;
			sprវ.ᜂ(num = sprវ.ᜤ() + 1);
			int num2 = num;
			text = string.Format(RecordTableEnumerator.b("㭂⥄框ⵈ㥊ⱌ㡎㡐㵒㉔⑖癘㽚⽜㹞ᙠ੢୤fቨ孪ၬ䅮॰Ṳᥴ", a_), num2);
			if (this.ᜅ.\u1714().ᜆ(text) == -1)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_66;
				}
			}
		}
		IL_66:
		if (false)
		{
		}
		if (true)
		{
		}
		return text;
	}

	// Token: 0x0600394E RID: 14670 RVA: 0x002007DC File Offset: 0x001FF7DC
	private string ᜁ()
	{
		int a_ = 11;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		sprវ sprវ = this.ᜅ;
		int num;
		sprវ.ᜄ(num = sprវ.\u1715() + 1);
		int num2 = num;
		return string.Format(RecordTableEnumerator.b("㥀⽂橄⍆㭈⩊㩌♎㽐㑒♔硖⽘㙚ㅜ᭞፠ɢቤ๦ݨ౪ᙬ彮౰嵲ʹ᩶ᕸ", a_), num2);
	}

	// Token: 0x0600394F RID: 14671 RVA: 0x00200854 File Offset: 0x001FF854
	private string ᜀ()
	{
		int a_ = 13;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		sprវ sprវ = this.ᜅ;
		int num;
		sprវ.ᜁ(num = sprវ.ᜐ() + 1);
		int num2 = num;
		return string.Format(RecordTableEnumerator.b("㭂⥄框⩈⑊⁌≎㑐㵒⅔⑖≘歚⁜煞ᥠ๢।", a_), num2);
	}

	// Token: 0x06003950 RID: 14672 RVA: 0x002008CC File Offset: 0x001FF8CC
	internal void ᜀ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 9;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 0:
					return;
				case 1:
					goto IL_72;
				case 2:
				{
					IListObjects listObjects;
					if (listObjects == null)
					{
						num = 10;
						continue;
					}
					num = 4;
					continue;
				}
				case 3:
					goto IL_D8;
				case 4:
				{
					IListObjects listObjects;
					num2 = listObjects.Count;
					goto IL_170;
				}
				case 5:
					num2 = 0;
					goto IL_170;
				case 6:
					goto IL_D8;
				case 7:
					goto IL_F4;
				case 8:
				{
					if (num3 == 0)
					{
						num = 0;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("䠻弽∿⹁⅃ᙅ⥇㡉㡋㵍", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("弻儽㔿ⱁぃ", a_), num3.ToString());
					int num4 = 0;
					num = 3;
					continue;
				}
				case 10:
					num = 5;
					continue;
				case 11:
				{
					int num4;
					if (num4 >= num3)
					{
						num = 7;
						continue;
					}
					IListObjects listObjects;
					IListObject a_2 = listObjects[num4];
					string value = this.ᜀ(a_2);
					A_0.WriteStartElement(RecordTableEnumerator.b("䠻弽∿⹁⅃ᙅ⥇㡉㡋", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("唻娽", a_), RecordTableEnumerator.b("吻䨽㐿㉁繃楅杇㥉⽋♍㕏㽑㕓╕癗㕙ⱛ㭝๟ᩡॣ੥๧թṫͭᅯٱݳ塵᝷ࡹ᭻兽좋煉뎛겝邟銡銣覥\udaa7쾩삫쾭쒯\udbb1\udbb3\ud8b5쮷특햻캽뎿", a_), value);
					A_0.WriteEndElement();
					num4++;
					num = 6;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_72;
				default:
				{
					if (false)
					{
					}
					IListObjects listObjects = A_1.InnerListObjects;
					num = 2;
					continue;
				}
				}
				IL_D8:
				num = 11;
				continue;
				IL_170:
				num3 = num2;
				num = 8;
			}
			IL_72:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
			IL_F4:
			if (true)
			{
			}
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06003951 RID: 14673 RVA: 0x00200AB8 File Offset: 0x001FFAB8
	private string ᜀ(IListObject A_0)
	{
		int a_ = 14;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		string arg = this.ᜅ.ᜀ(A_0);
		sprᦨ a_2 = new sprᦨ('/' + arg, RecordTableEnumerator.b("ⱃ㉅㱇㩉癋慍罏⅑㝓㹕㵗㝙㵛ⵝ也ൡᑣͥ٧ቩūɭᙯᵱٳ᭵᥷๹ཻ偽ꦅ킓秊ﮗﮝ캟횡讣钥颧骩骫膭슯ힱ\ud8b3ힵ첷펹펻킽뎿꫁귃뛅믇룋꿍닏뻑뇓", a_));
		return this.ᜇ().ᜀ(a_2);
	}

	// Token: 0x06003952 RID: 14674 RVA: 0x00200B38 File Offset: 0x001FFB38
	internal void ᜀ(IWorksheet A_0, string A_1, string A_2)
	{
		switch (0)
		{
		default:
		{
			XmlReader xmlReader;
			string key;
			for (;;)
			{
				sprᦨ sprᦨ = this.ᜊ[A_1];
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_D8;
					case 1:
						goto IL_B7;
					case 2:
						if (xmlReader.NodeType == XmlNodeType.Element)
						{
							num = 0;
							continue;
						}
						xmlReader.Read();
						num = 4;
						continue;
					case 3:
						if (sprᦨ == null)
						{
							num = 5;
							continue;
						}
						xmlReader = this.ᜅ.ᜂ(sprᦨ, A_2, out key);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 4:
						if (true)
						{
						}
						goto IL_B7;
					case 5:
						goto IL_5B;
					}
					break;
					IL_B7:
					num = 2;
				}
			}
			IL_5B:
			throw new XmlException();
			IL_D8:
			sprᮜ sprᮜ = new sprᮜ();
			sprᮜ.ᜀ(xmlReader, A_0);
			this.ᜅ.ᜣ()[key] = null;
			return;
		}
		}
	}

	// Token: 0x06003953 RID: 14675 RVA: 0x00200C40 File Offset: 0x001FFC40
	internal void ᜀ(IWorksheet A_0, string A_1, RelationsCollection A_2)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			string key = null;
			PivotTableCollection pivotTables = A_0.PivotTables;
			IEnumerator enumerator = A_2.GetEnumerator();
			try
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						goto IL_15A;
					case 4:
					{
						KeyValuePair<string, sprᦨ> keyValuePair;
						if (keyValuePair.Value.ᜃ() == RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂햐ﲒﺚ躠醢閤鞦龨蒪\udfac쪮\uddb0튲솴\udeb6횸햺캼ힾꣀ돂뛄마ꋊ믌ꃎꗐ蟒듔뗖뗘뻚", a_))
						{
							num = 6;
							continue;
						}
						break;
					}
					case 5:
					{
						if (!enumerator.MoveNext())
						{
							num = 0;
							continue;
						}
						KeyValuePair<string, sprᦨ> keyValuePair = (KeyValuePair<string, sprᦨ>)enumerator.Current;
						num = 4;
						continue;
					}
					case 6:
					{
						KeyValuePair<string, sprᦨ> keyValuePair;
						XmlReader a_2 = this.ᜅ.ᜂ(keyValuePair.Value, A_1, out key);
						XlsPivotCachesCollection pivotCaches = A_0.Workbook.PivotCaches;
						XlsPivotTable xlsPivotTable = new XlsPivotTable(((XlsWorksheet)A_0).AppImplementation, A_0);
						spr\u2005.ᜑ(a_2, xlsPivotTable);
						pivotTables.ᜁ(xlsPivotTable);
						this.ᜅ.ᜣ().Add(key, null);
						this.ᜊ.Remove(keyValuePair.Key);
						num = 2;
						continue;
					}
					}
					IL_B1:
					num = 5;
					continue;
					goto IL_B1;
				}
				IL_15A:;
			}
			finally
			{
				for (;;)
				{
					IL_18D:
					IDisposable disposable = enumerator as IDisposable;
					int num = 0;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1C0;
						default:
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								if (disposable != null)
								{
									num = 2;
									continue;
								}
								goto IL_1C0;
							case 1:
								goto IL_1BE;
							case 2:
								disposable.Dispose();
								num = 1;
								continue;
							}
							goto IL_18D;
						}
					}
				}
				IL_1BE:
				IL_1C0:;
			}
			return;
		}
		}
	}

	// Token: 0x06003954 RID: 14676 RVA: 0x00200E2C File Offset: 0x001FFE2C
	internal sprᡟ ᜀ(sprវ A_0)
	{
		if (true)
		{
		}
		sprᡟ sprᡟ;
		for (;;)
		{
			IL_38:
			sprᡟ = (sprᡟ)base.MemberwiseClone();
			sprᡟ.ᜅ = A_0;
			int num = 0;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8B;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						if (this.ᜄ != null)
						{
							num = 1;
							continue;
						}
						goto IL_8B;
					case 1:
						sprᡟ.ᜄ = A_0.\u1714().ᜃ(this.ᜄ.ᜇ());
						num = 2;
						continue;
					case 2:
						goto IL_89;
					}
					goto IL_38;
				}
			}
		}
		IL_89:
		IL_8B:
		sprᡟ.ᜊ = (RelationsCollection)sprἽ.ᜀ(this.ᜊ);
		sprᡟ.ᜋ = (RelationsCollection)sprἽ.ᜀ(this.ᜋ);
		sprᡟ.ᜌ = (RelationsCollection)sprἽ.ᜀ(this.ᜌ);
		byte[] array = new byte[this.ᜇ.Length];
		this.ᜇ.Position = 0L;
		this.ᜇ.Read(array, 0, array.Length);
		this.ᜇ.Position = 0L;
		sprᡟ.ᜇ = new MemoryStream(array);
		sprᡟ.ᜆ = (MemoryStream)sprἽ.ᜀ(this.ᜆ);
		sprᡟ.ᜑ = (MemoryStream)sprἽ.ᜀ(this.ᜑ);
		return sprᡟ;
	}

	// Token: 0x06003955 RID: 14677 RVA: 0x00200F88 File Offset: 0x001FFF88
	public void ᜄ()
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
		this.ᜄ = null;
		this.ᜇ = null;
		this.ᜆ = null;
		this.ᜅ.\u171F();
		GC.SuppressFinalize(this);
	}

	// Token: 0x04001914 RID: 6420
	private const string ᜀ = "xl/drawings/vmlDrawing{0}.vml";

	// Token: 0x04001915 RID: 6421
	private const string ᜁ = "xl/comments{0}.xml";

	// Token: 0x04001916 RID: 6422
	private const string ᜂ = "xl/drawings/drawing{0}.xml";

	// Token: 0x04001917 RID: 6423
	private const string ᜃ = "vml";

	// Token: 0x04001918 RID: 6424
	private spr\u2570 ᜄ;

	// Token: 0x04001919 RID: 6425
	private sprវ ᜅ;

	// Token: 0x0400191A RID: 6426
	private MemoryStream ᜆ;

	// Token: 0x0400191B RID: 6427
	private MemoryStream ᜇ;

	// Token: 0x0400191C RID: 6428
	private string ᜈ;

	// Token: 0x0400191D RID: 6429
	private string ᜉ;

	// Token: 0x0400191E RID: 6430
	private RelationsCollection ᜊ;

	// Token: 0x0400191F RID: 6431
	private RelationsCollection ᜋ;

	// Token: 0x04001920 RID: 6432
	private RelationsCollection ᜌ;

	// Token: 0x04001921 RID: 6433
	private string \u170D;

	// Token: 0x04001922 RID: 6434
	private string ᜎ;

	// Token: 0x04001923 RID: 6435
	private string ᜏ;

	// Token: 0x04001924 RID: 6436
	private string ᜐ;

	// Token: 0x04001925 RID: 6437
	private Stream ᜑ;

	// Token: 0x04001926 RID: 6438
	private Dictionary<string, RelationsCollection> \u1712;
}
