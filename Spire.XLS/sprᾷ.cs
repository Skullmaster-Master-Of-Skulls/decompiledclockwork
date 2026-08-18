using System;
using System.Collections.Generic;
using System.Reflection;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.PivotTables;

// Token: 0x02000312 RID: 786
[DefaultMember("Item")]
internal class sprᾷ : CollectionExtended<XlsPivotCacheField>
{
	// Token: 0x06003041 RID: 12353 RVA: 0x001B71E4 File Offset: 0x001B61E4
	internal sprᾷ()
	{
	}

	// Token: 0x06003042 RID: 12354 RVA: 0x001B71F8 File Offset: 0x001B61F8
	internal sprᾷ(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06003043 RID: 12355 RVA: 0x001B7210 File Offset: 0x001B6210
	public new XlsPivotCacheField ᜀ(int A_0)
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
		return base.InnerList[A_0];
	}

	// Token: 0x06003044 RID: 12356 RVA: 0x001B7258 File Offset: 0x001B6258
	public new XlsPivotCacheField ᜀ(string A_0)
	{
		XlsPivotCacheField result;
		using (List<XlsPivotCacheField>.Enumerator enumerator = base.InnerList.GetEnumerator())
		{
			int num = 1;
			for (;;)
			{
				XlsPivotCacheField xlsPivotCacheField;
				switch (num)
				{
				case 0:
					num = 6;
					continue;
				case 2:
					if (!enumerator.MoveNext())
					{
						num = 0;
						continue;
					}
					xlsPivotCacheField = enumerator.Current;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_97;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 3:
					goto IL_BF;
				case 4:
					result = xlsPivotCacheField;
					num = 3;
					continue;
				case 5:
					goto IL_97;
				case 6:
					goto IL_CC;
				}
				IL_50:
				num = 2;
				continue;
				goto IL_50;
				IL_97:
				if (!(xlsPivotCacheField.Name == A_0))
				{
					goto IL_50;
				}
				num = 4;
			}
			IL_BF:
			return result;
			IL_CC:
			goto IL_0E;
		}
		return result;
		IL_0E:
		if (true)
		{
		}
		return null;
	}

	// Token: 0x06003045 RID: 12357 RVA: 0x001B7358 File Offset: 0x001B6358
	internal new void ᜀ(sprἛ A_0, int A_1)
	{
		int a_ = 14;
		int num = 5;
		for (;;)
		{
			IL_13:
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_44;
			case 2:
			{
				TBIFFRecord tbiffrecord;
				if (tbiffrecord != TBIFFRecord.PivotField)
				{
					num = 4;
					continue;
				}
				XlsPivotCacheField a_2 = new XlsPivotCacheField(A_0);
				this.ᜀ(a_2);
				int num2;
				num2++;
				num = 6;
				continue;
			}
			case 3:
				goto IL_98;
			case 4:
				goto IL_82;
			case 6:
				goto IL_98;
			case 7:
			{
				int num2;
				if (num2 >= A_1)
				{
					num = 0;
					continue;
				}
				TBIFFRecord tbiffrecord = A_0.ᜉ();
				num = 2;
				continue;
			}
			}
			while (A_0 != null)
			{
				int num2 = 0;
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
					num = 3;
					goto IL_13;
				}
			}
			num = 1;
			continue;
			IL_98:
			num = 7;
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
		IL_82:
		throw new spr\u1AC0();
	}

	// Token: 0x06003046 RID: 12358 RVA: 0x001B745C File Offset: 0x001B645C
	public new void ᜀ(RecordArrayList A_0)
	{
		int a_ = 6;
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
				goto IL_46;
			case 4:
			{
				if (true)
				{
				}
				int num2;
				int count;
				if (num2 >= count)
				{
					num = 0;
					continue;
				}
				XlsPivotCacheField xlsPivotCacheField = this.ᜀ(num2);
				xlsPivotCacheField.SerializeDataToList(A_0);
				num2++;
				num = 5;
				continue;
			}
			case 5:
				goto IL_A8;
			}
			if (A_0 == null)
			{
				num = 2;
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
				int num2 = 0;
				int count = base.Count;
				break;
			}
			}
			num = 1;
			continue;
			IL_A8:
			num = 4;
		}
		IL_46:
		throw new ArgumentNullException(RecordTableEnumerator.b("主嬽⌿ⵁ㙃≅㭇", a_));
	}

	// Token: 0x06003047 RID: 12359 RVA: 0x001B7538 File Offset: 0x001B6538
	public new int ᜀ(XlsPivotCacheField A_0)
	{
		int a_ = 17;
		if (A_0 == null)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_24;
				}
			}
			IL_24:
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ⅆ⁈⹊⅌⭎", a_));
		}
		base.Add(A_0);
		int num = base.Count - 1;
		A_0.Index = num;
		return num;
	}

	// Token: 0x06003048 RID: 12360 RVA: 0x001B75B0 File Offset: 0x001B65B0
	public new XlsPivotCacheField ᜁ(string A_0)
	{
		int a_ = 14;
		for (;;)
		{
			IL_09:
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A1;
				case 1:
					if (A_0.Length == 0)
					{
						num = 0;
						continue;
					}
					goto IL_B7;
				case 2:
					goto IL_58;
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						num = 2;
						break;
					}
				}
				else
				{
					num = 1;
				}
			}
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("⩃❅╇⽉", a_));
		IL_A1:
		throw new ArgumentException(RecordTableEnumerator.b("ᝃ㉅㩇⍉≋⥍灏ㅑ㕓㡕㙗㕙⡛繝ɟݡ䑣ͥէᩩᡫ᝭幯", a_), RecordTableEnumerator.b("⩃❅╇⽉", a_));
		IL_B7:
		XlsPivotCacheField xlsPivotCacheField = new XlsPivotCacheField();
		xlsPivotCacheField.Name = A_0;
		this.ᜀ(xlsPivotCacheField);
		return xlsPivotCacheField;
	}

	// Token: 0x06003049 RID: 12361 RVA: 0x001B768C File Offset: 0x001B668C
	public new XlsPivotCacheField ᜀ(string A_0, string A_1)
	{
		int a_ = 9;
		for (;;)
		{
			IL_09:
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (A_0.Length == 0)
					{
						num = 2;
						continue;
					}
					goto IL_A6;
				case 2:
					goto IL_90;
				case 3:
					goto IL_58;
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
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
						break;
					}
				}
				else
				{
					num = 1;
				}
			}
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰾㕀ㅂୄ♆⑈⹊", a_));
		IL_90:
		throw new ArgumentException(RecordTableEnumerator.b("䰾㕀ㅂୄ♆⑈⹊浌扎煐⁒⅔╖じ㕚㩜罞ɠɢ୤०٨Ὢ䵬൮ᑰ卲ၴ᩶ॸེѼ", a_));
		IL_A6:
		XlsPivotCacheField xlsPivotCacheField = new XlsPivotCacheField();
		xlsPivotCacheField.Name = A_0;
		xlsPivotCacheField.Formula = A_1;
		this.ᜀ(xlsPivotCacheField);
		return xlsPivotCacheField;
	}

	// Token: 0x0600304A RID: 12362 RVA: 0x001B775C File Offset: 0x001B675C
	internal new int ᜀ()
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			List<XlsPivotCacheField>.Enumerator enumerator = base.InnerList.GetEnumerator();
			try
			{
				int num2 = 8;
				for (;;)
				{
					bool flag;
					bool flag2;
					switch (num2)
					{
					case 0:
						num++;
						num2 = 7;
						continue;
					case 1:
						num2 = 14;
						continue;
					case 2:
					{
						XlsPivotCacheField xlsPivotCacheField;
						if (!xlsPivotCacheField.FieldGroup.ᜊ())
						{
							num2 = 4;
							continue;
						}
						num2 = 6;
						continue;
					}
					case 3:
					{
						XlsPivotCacheField xlsPivotCacheField;
						if (!xlsPivotCacheField.IsFormulaField)
						{
							num2 = 15;
							continue;
						}
						break;
					}
					case 4:
						goto IL_A1;
					case 5:
					{
						if (!enumerator.MoveNext())
						{
							num2 = 1;
							continue;
						}
						XlsPivotCacheField xlsPivotCacheField = enumerator.Current;
						flag = true;
						num2 = 9;
						continue;
					}
					case 6:
						flag2 = false;
						goto IL_F5;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A1;
						default:
						{
							if (false)
							{
							}
							XlsPivotCacheField xlsPivotCacheField;
							if (xlsPivotCacheField.IsFieldGroup)
							{
								num2 = 10;
								continue;
							}
							goto IL_7F;
						}
						}
						break;
					case 10:
						num2 = 2;
						continue;
					case 11:
						if (flag)
						{
							num2 = 0;
							continue;
						}
						break;
					case 12:
						flag2 = true;
						goto IL_F5;
					case 13:
						goto IL_7F;
					case 14:
						goto IL_1A4;
					case 15:
						num2 = 11;
						continue;
					}
					goto IL_7A;
					IL_7F:
					num2 = 3;
					continue;
					IL_A1:
					num2 = 12;
					continue;
					IL_F5:
					flag = flag2;
					num2 = 13;
					continue;
					IL_155:
					num2 = 5;
					continue;
					IL_7A:
					goto IL_155;
				}
				IL_1A4:;
			}
			finally
			{
				if (true)
				{
				}
				((IDisposable)enumerator).Dispose();
			}
			return num;
		}
		}
	}
}
