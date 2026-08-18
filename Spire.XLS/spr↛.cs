using System;
using System.Collections.Generic;
using System.Reflection;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.PivotTables;

// Token: 0x020004C8 RID: 1224
[DefaultMember("Item")]
internal class spr\u219B : CollectionBase<XlsPivotCacheField>
{
	// Token: 0x06004B50 RID: 19280 RVA: 0x002DD74C File Offset: 0x002DC74C
	public XlsPivotCacheField ᜀ(string A_0)
	{
		XlsPivotCacheField result;
		for (;;)
		{
			using (List<XlsPivotCacheField>.Enumerator enumerator = base.InnerList.GetEnumerator())
			{
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (true)
						{
						}
						if (!enumerator.MoveNext())
						{
							num = 3;
							continue;
						}
						XlsPivotCacheField xlsPivotCacheField = enumerator.Current;
						num = 6;
						continue;
					}
					case 1:
					{
						XlsPivotCacheField xlsPivotCacheField;
						result = xlsPivotCacheField;
						num = 4;
						continue;
					}
					case 2:
						goto IL_AA;
					case 3:
						num = 2;
						continue;
					case 4:
						goto IL_9D;
					case 6:
					{
						XlsPivotCacheField xlsPivotCacheField;
						if (xlsPivotCacheField.Name == A_0)
						{
							num = 1;
							continue;
						}
						break;
					}
					}
					IL_48:
					num = 0;
					continue;
					goto IL_48;
				}
				IL_9D:
				goto IL_BD;
				IL_AA:
				break;
			}
			IL_BD:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_D3;
			}
		}
		return null;
		IL_D3:
		if (false)
		{
		}
		return result;
	}

	// Token: 0x06004B52 RID: 19282 RVA: 0x002DD858 File Offset: 0x002DC858
	[CLSCompliant(false)]
	public void ᜀ(sprἛ A_0, int A_1)
	{
		int a_ = 8;
		int num = 2;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
			{
				TBIFFRecord tbiffrecord;
				if (tbiffrecord != TBIFFRecord.PivotField)
				{
					num = 6;
					continue;
				}
				XlsPivotCacheField a_2 = new XlsPivotCacheField(A_0);
				this.ᜀ(a_2);
				num2++;
				num = 5;
				continue;
			}
			case 1:
			{
				if (num2 >= A_1)
				{
					num = 7;
					continue;
				}
				TBIFFRecord tbiffrecord = A_0.ᜉ();
				num = 0;
				continue;
			}
			case 3:
				goto IL_44;
			case 4:
				goto IL_98;
			case 5:
				goto IL_98;
			case 6:
				goto IL_82;
			case 7:
				goto IL_B2;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num2 = 0;
			num = 4;
			continue;
			IL_98:
			num = 1;
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
		IL_82:
		if (true)
		{
		}
		throw new spr\u1AC0();
		IL_B2:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_44;
		default:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x06004B53 RID: 19283 RVA: 0x002DD95C File Offset: 0x002DC95C
	[CLSCompliant(false)]
	public void ᜀ(RecordArrayList A_0)
	{
		int a_ = 16;
		int num = 3;
		for (;;)
		{
			int num2;
			int count;
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				if (num2 < count)
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
						XlsPivotCacheField xlsPivotCacheField = base[num2];
						xlsPivotCacheField.SerializeDataToList(A_0);
						num2++;
						num = 4;
						continue;
					}
					}
				}
				num = 5;
				continue;
			case 1:
				goto IL_9E;
			case 2:
				goto IL_3C;
			case 4:
				goto IL_9E;
			case 5:
				return;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num2 = 0;
			count = base.Count;
			num = 1;
			continue;
			IL_9E:
			num = 0;
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⥉⍋㱍㑏⅑", a_));
	}

	// Token: 0x06004B54 RID: 19284 RVA: 0x002DDA38 File Offset: 0x002DCA38
	public int ᜀ(XlsPivotCacheField A_0)
	{
		int a_ = 19;
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
				base.Add(A_0);
				int num = base.Count - 1;
				A_0.Index = num;
				return num;
			}
			}
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("⽈≊⡌⍎㕐", a_));
	}

	// Token: 0x06004B55 RID: 19285 RVA: 0x002DDAB0 File Offset: 0x002DCAB0
	public XlsPivotCacheField ᜁ(string A_0)
	{
		int a_ = 7;
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
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_58;
				case 2:
					goto IL_86;
				case 3:
					goto IL_76;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				num = 3;
				continue;
			}
			IL_76:
			if (A_0.Length != 0)
			{
				goto IL_9C;
			}
			num = 2;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("丼䬾㍀ൂ⑄⩆ⱈ", a_));
		IL_86:
		throw new ArgumentException(RecordTableEnumerator.b("丼䬾㍀ൂ⑄⩆ⱈ歊恌潎≐❒❔㹖㝘㱚絜㱞`ൢ୤ࡦᵨ䭪ཬ੮兰ᙲᡴݶ൸ɺ", a_));
		IL_9C:
		XlsPivotCacheField xlsPivotCacheField = new XlsPivotCacheField();
		xlsPivotCacheField.Name = A_0;
		this.ᜀ(xlsPivotCacheField);
		return xlsPivotCacheField;
	}

	// Token: 0x06004B56 RID: 19286 RVA: 0x002DDB7C File Offset: 0x002DCB7C
	public XlsPivotCacheField ᜀ(string A_0, string A_1)
	{
		int a_ = 6;
		int num = 3;
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
				switch (num)
				{
				case 0:
					goto IL_58;
				case 1:
					goto IL_76;
				case 2:
					goto IL_86;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				num = 1;
				continue;
			}
			IL_76:
			if (A_0.Length != 0)
			{
				goto IL_9C;
			}
			num = 2;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("伻䨽㈿ు╃⭅ⵇ", a_));
		IL_86:
		throw new ArgumentException(RecordTableEnumerator.b("伻䨽㈿ు╃⭅ⵇ橉態湍⍏♑♓㽕㙗㵙籛㵝şౡ੣॥ᱧ䩩๫୭偯᝱ᥳٵ౷͹", a_));
		IL_9C:
		XlsPivotCacheField xlsPivotCacheField = new XlsPivotCacheField();
		xlsPivotCacheField.Name = A_0;
		xlsPivotCacheField.Formula = A_1;
		this.ᜀ(xlsPivotCacheField);
		return xlsPivotCacheField;
	}

	// Token: 0x06004B57 RID: 19287 RVA: 0x002DDC4C File Offset: 0x002DCC4C
	public int ᜀ()
	{
		int num;
		for (;;)
		{
			switch (0)
			{
			default:
				num = 0;
				using (List<XlsPivotCacheField>.Enumerator enumerator = base.InnerList.GetEnumerator())
				{
					int num2 = 14;
					for (;;)
					{
						bool flag;
						bool flag2;
						switch (num2)
						{
						case 0:
							num2 = 4;
							continue;
						case 2:
							flag = true;
							goto IL_F5;
						case 3:
							flag = false;
							goto IL_F5;
						case 4:
							goto IL_188;
						case 5:
							num++;
							num2 = 1;
							continue;
						case 6:
						{
							if (!enumerator.MoveNext())
							{
								num2 = 0;
								continue;
							}
							XlsPivotCacheField xlsPivotCacheField = enumerator.Current;
							flag2 = true;
							num2 = 8;
							continue;
						}
						case 7:
						{
							XlsPivotCacheField xlsPivotCacheField;
							if (!xlsPivotCacheField.FieldGroup.ᜊ())
							{
								num2 = 13;
								continue;
							}
							num2 = 3;
							continue;
						}
						case 8:
						{
							XlsPivotCacheField xlsPivotCacheField;
							if (xlsPivotCacheField.IsFieldGroup)
							{
								num2 = 12;
								continue;
							}
							goto IL_7F;
						}
						case 9:
							goto IL_7F;
						case 10:
						{
							XlsPivotCacheField xlsPivotCacheField;
							if (!xlsPivotCacheField.IsFormulaField)
							{
								num2 = 11;
								continue;
							}
							break;
						}
						case 11:
							num2 = 15;
							continue;
						case 12:
							num2 = 7;
							continue;
						case 13:
							num2 = 2;
							continue;
						case 15:
							if (flag2)
							{
								num2 = 5;
								continue;
							}
							break;
						}
						goto IL_7A;
						IL_7F:
						num2 = 10;
						continue;
						IL_F5:
						flag2 = flag;
						num2 = 9;
						continue;
						IL_139:
						num2 = 6;
						continue;
						IL_7A:
						goto IL_139;
					}
					IL_188:;
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
					goto IL_1B6;
				}
				break;
			}
		}
		IL_1B6:
		if (false)
		{
		}
		return num;
	}
}
