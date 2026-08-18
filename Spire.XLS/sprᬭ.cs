using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000423 RID: 1059
[DefaultMember("Item")]
internal class sprᬭ : CollectionExtended<object>, IWorkbooks
{
	// Token: 0x06003FE3 RID: 16355 RVA: 0x0023E828 File Offset: 0x0023D828
	public new IWorkbook ᜀ(int A_0)
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
		return (IWorkbook)base.InnerList[A_0];
	}

	// Token: 0x06003FE4 RID: 16356 RVA: 0x0023E874 File Offset: 0x0023D874
	public new IWorkbook ᜀ(string[] A_0)
	{
		int a_ = 2;
		int num = 6;
		XlsWorkbook xlsWorkbook;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6B;
			case 1:
				goto IL_C1;
			case 2:
			{
				if (A_0.Length == 0)
				{
					num = 5;
					continue;
				}
				xlsWorkbook = base.AppImplementation.ᜀ(this, A_0.Length, base.AppImplementation.ᜋ());
				int num2 = 0;
				num = 1;
				continue;
			}
			case 3:
				goto IL_C1;
			case 4:
			{
				int num2;
				if (num2 >= A_0.Length)
				{
					num = 7;
					continue;
				}
				xlsWorkbook.Worksheets[num2].Name = A_0[num2];
				num2++;
				num = 3;
				continue;
			}
			case 5:
				goto IL_FA;
			case 7:
				goto IL_DD;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_C1:
				num = 4;
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
					num = 0;
				}
				else
				{
					num = 2;
				}
				break;
			}
		}
		IL_6B:
		throw new ArgumentNullException(RecordTableEnumerator.b("嘷嬹儻嬽㌿", a_));
		IL_DD:
		base.Add(xlsWorkbook);
		xlsWorkbook.Activate();
		return xlsWorkbook;
		IL_FA:
		throw new ArgumentException(RecordTableEnumerator.b("琷弹崻䴽㐿扁⭃⡅ⵇ橉≋⽍㵏㝑瑓㭕ⵗ⥙⡛繝՟ᩡൣᕥᱧᥩ䉫", a_));
	}

	// Token: 0x06003FE5 RID: 16357 RVA: 0x0023E9B8 File Offset: 0x0023D9B8
	public new IWorkbook ᜁ(int A_0)
	{
		int a_ = 14;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			if (A_0 >= 0)
			{
				XlsWorkbook xlsWorkbook = base.AppImplementation.ᜀ(this, A_0, base.AppImplementation.ᜋ());
				base.Add(xlsWorkbook);
				xlsWorkbook.Activate();
				return xlsWorkbook;
			}
			break;
		}
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㝃⹅ⵇ⽉㡋്㽏❑㩓≕", a_), RecordTableEnumerator.b("㝃⹅ⵇ⽉㡋്㽏❑㩓≕硗㝙⥛ⵝᑟ䉡٣ͥ䡧൩ṫ୭ᅯٱᅳѵ塷๹ᑻώꊁﺃ慎ꊋ", a_));
	}

	// Token: 0x06003FE6 RID: 16358 RVA: 0x0023EA4C File Offset: 0x0023DA4C
	public new IWorkbook ᜀ()
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
		XlsWorkbook xlsWorkbook = base.AppImplementation.ᜀ(this, base.AppImplementation.ᜋ());
		base.Add(xlsWorkbook);
		xlsWorkbook.Activate();
		return xlsWorkbook;
	}

	// Token: 0x06003FE7 RID: 16359 RVA: 0x0023EAB0 File Offset: 0x0023DAB0
	public IWorkbook ᜃ()
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
		return this.ᜀ(null, base.AppImplementation.ᜋ());
	}

	// Token: 0x06003FE8 RID: 16360 RVA: 0x0023EB00 File Offset: 0x0023DB00
	public new IWorkbook ᜀ(ExcelVersion A_0)
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
		return this.ᜀ(null, A_0);
	}

	// Token: 0x06003FE9 RID: 16361 RVA: 0x0023EB44 File Offset: 0x0023DB44
	public new IWorkbook ᜂ(string A_0)
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
		return this.ᜀ(A_0, ExcelParseOptions.Default, base.AppImplementation.ᜋ());
	}

	// Token: 0x06003FEA RID: 16362 RVA: 0x0023EB94 File Offset: 0x0023DB94
	public new IWorkbook ᜀ(string A_0, ExcelVersion A_1)
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
		return this.ᜀ(A_0, ExcelParseOptions.Default, A_1);
	}

	// Token: 0x06003FEB RID: 16363 RVA: 0x0023EBD8 File Offset: 0x0023DBD8
	public new IWorkbook ᜂ(string A_0, ExcelParseOptions A_1)
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
		return this.ᜀ(A_0, A_1, base.AppImplementation.ᜋ());
	}

	// Token: 0x06003FEC RID: 16364 RVA: 0x0023EC28 File Offset: 0x0023DC28
	public new IWorkbook ᜀ(string A_0, ExcelParseOptions A_1, ExcelVersion A_2)
	{
		XlsWorkbook xlsWorkbook;
		for (;;)
		{
			xlsWorkbook = null;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0 == null)
					{
						num = 1;
						continue;
					}
					xlsWorkbook = base.AppImplementation.ᜀ(this, A_0, A_1, A_2);
					num = 2;
					continue;
				case 1:
					goto IL_37;
				case 2:
					goto IL_51;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_37;
					default:
						goto IL_7F;
					}
					break;
				}
				break;
				IL_37:
				xlsWorkbook = base.AppImplementation.ᜀ(this, A_2);
				num = 3;
			}
		}
		IL_51:
		goto IL_8F;
		IL_7F:
		if (true)
		{
		}
		if (false)
		{
		}
		IL_8F:
		base.Add(xlsWorkbook);
		xlsWorkbook.Activate();
		xlsWorkbook.Worksheets[0].Activate();
		return xlsWorkbook;
	}

	// Token: 0x06003FED RID: 16365 RVA: 0x0023ECE4 File Offset: 0x0023DCE4
	public new IWorkbook ᜀ(string A_0)
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
		return this.ᜁ(A_0, base.AppImplementation.ᜋ());
	}

	// Token: 0x06003FEE RID: 16366 RVA: 0x0023ED34 File Offset: 0x0023DD34
	public new IWorkbook ᜁ(string A_0, ExcelVersion A_1)
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
		XlsWorkbook xlsWorkbook = base.AppImplementation.ᜀ(this, A_0, A_1);
		xlsWorkbook.InternalSaved = true;
		base.Add(xlsWorkbook);
		return xlsWorkbook;
	}

	// Token: 0x06003FEF RID: 16367 RVA: 0x0023ED90 File Offset: 0x0023DD90
	public new IWorkbook ᜀ(Stream A_0, string A_1, int A_2, int A_3)
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
		return this.ᜀ(A_0, A_1, A_2, A_3, null, null, base.AppImplementation.ᜋ());
	}

	// Token: 0x06003FF0 RID: 16368 RVA: 0x0023EDE4 File Offset: 0x0023DDE4
	public new IWorkbook ᜀ(string A_0, string A_1, int A_2, int A_3)
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
		return this.ᜀ(A_0, A_1, A_2, A_3, null);
	}

	// Token: 0x06003FF1 RID: 16369 RVA: 0x0023EE2C File Offset: 0x0023DE2C
	public new IWorkbook ᜀ(string A_0, string A_1, int A_2, int A_3, Encoding A_4)
	{
		int a_ = 10;
		int num = 2;
		IWorkbook result;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_10B;
			case 1:
			{
				if (A_0.Length == 0)
				{
					num = 0;
					continue;
				}
				FileStream fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read, FileShare.Read);
				num = 4;
				continue;
			}
			case 3:
				goto IL_45;
			case 4:
				try
				{
					FileStream fileStream;
					XlsWorkbook xlsWorkbook = (XlsWorkbook)this.ᜀ(fileStream, A_1, A_2, A_3, A_0, A_4, base.AppImplementation.ᜋ());
					xlsWorkbook.FullFileName = Path.GetFullPath(A_0);
					xlsWorkbook.InternalSaved = true;
					result = xlsWorkbook;
					goto IL_127;
				}
				finally
				{
					num = 1;
					for (;;)
					{
						FileStream fileStream;
						switch (num)
						{
						case 0:
							((IDisposable)fileStream).Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_BA;
						}
						if (fileStream == null)
						{
							break;
						}
						num = 0;
					}
					IL_BA:;
				}
				goto IL_BD;
				IL_127:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_13D;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 3;
			}
			else
			{
				num = 1;
			}
		}
		IL_45:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("☿⭁⡃⍅ه⭉⅋⭍", a_));
		IL_BD:
		throw new ArgumentException(RecordTableEnumerator.b("☿⭁⡃⍅ه⭉⅋⭍", a_));
		IL_10B:
		goto IL_BD;
		IL_13D:
		if (false)
		{
		}
		return result;
	}

	// Token: 0x06003FF2 RID: 16370 RVA: 0x0023EF90 File Offset: 0x0023DF90
	private new IWorkbook ᜀ(Stream A_0, string A_1, int A_2, int A_3, string A_4, Encoding A_5, ExcelVersion A_6)
	{
		int a_ = 13;
		int num = 5;
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
					if (A_1.Length == 0)
					{
						num = 1;
						continue;
					}
					goto IL_DD;
				case 1:
					goto IL_84;
				case 2:
					goto IL_DB;
				case 3:
					goto IL_6A;
				case 4:
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					num = 0;
					continue;
				case 5:
					if (true)
					{
					}
					break;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 3;
			}
			else
			{
				num = 4;
			}
		}
		IL_6A:
		throw new ArgumentNullException(RecordTableEnumerator.b("あㅄ㕆ⱈ⩊⁌", a_));
		IL_84:
		throw new ArgumentException(RecordTableEnumerator.b("あ⁄㝆⡈㥊ⱌ㭎㹐⅒", a_));
		IL_DB:
		throw new ArgumentNullException(RecordTableEnumerator.b("あ⁄㝆⡈㥊ⱌ㭎㹐⅒", a_));
		IL_DD:
		XlsWorkbook xlsWorkbook = base.AppImplementation.ᜀ(this, A_0, A_1, A_2, A_3, A_6, A_4, A_5);
		base.Add(xlsWorkbook);
		return xlsWorkbook;
	}

	// Token: 0x06003FF3 RID: 16371 RVA: 0x0023F09C File Offset: 0x0023E09C
	public new IWorkbook ᜀ(Stream A_0, string A_1)
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
		return this.ᜀ(A_0, A_1, 1, 1);
	}

	// Token: 0x06003FF4 RID: 16372 RVA: 0x0023F0E4 File Offset: 0x0023E0E4
	public new IWorkbook ᜁ(string A_0, string A_1)
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
		return this.ᜀ(A_0, A_1, 1, 1);
	}

	// Token: 0x06003FF5 RID: 16373 RVA: 0x0023F12C File Offset: 0x0023E12C
	public new IWorkbook ᜀ(string A_0, ExcelParseOptions A_1)
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
		XlsWorkbook xlsWorkbook = base.AppImplementation.ᜀ(this, A_0, A_1, false, null, base.AppImplementation.ᜋ());
		base.Add(xlsWorkbook);
		return xlsWorkbook;
	}

	// Token: 0x06003FF6 RID: 16374 RVA: 0x0023F18C File Offset: 0x0023E18C
	public new IWorkbook ᜀ(string A_0, ExcelParseOptions A_1, bool A_2, string A_3)
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
		return this.ᜀ(A_0, A_1, A_2, A_3, base.AppImplementation.ᜋ());
	}

	// Token: 0x06003FF7 RID: 16375 RVA: 0x0023F1E0 File Offset: 0x0023E1E0
	public new IWorkbook ᜀ(string A_0, ExcelParseOptions A_1, bool A_2, string A_3, ExcelVersion A_4)
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
		XlsWorkbook xlsWorkbook = base.AppImplementation.ᜀ(this, A_0, A_1, A_2, A_3, A_4);
		base.Add(xlsWorkbook);
		return xlsWorkbook;
	}

	// Token: 0x06003FF8 RID: 16376 RVA: 0x0023F238 File Offset: 0x0023E238
	public new IWorkbook ᜀ(Stream A_0, ExcelParseOptions A_1, bool A_2, string A_3)
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
		return this.ᜀ(A_0, A_1, A_2, A_3, base.AppImplementation.ᜋ());
	}

	// Token: 0x06003FF9 RID: 16377 RVA: 0x0023F28C File Offset: 0x0023E28C
	public new IWorkbook ᜀ(Stream A_0, ExcelParseOptions A_1, bool A_2, string A_3, ExcelVersion A_4)
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
		XlsWorkbook xlsWorkbook = base.AppImplementation.ᜀ(this, A_0, A_1, A_2, A_3, A_4);
		base.Add(xlsWorkbook);
		return xlsWorkbook;
	}

	// Token: 0x06003FFA RID: 16378 RVA: 0x0023F2E4 File Offset: 0x0023E2E4
	public new IWorkbook ᜀ(Stream A_0, ExcelParseOptions A_1, bool A_2, string A_3, ExcelOpenType A_4)
	{
		int a_ = 10;
		for (;;)
		{
			A_4 = this.ᜀ(A_0, A_4);
			ExcelOpenType excelOpenType = A_4;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_77;
				case 1:
					switch (excelOpenType)
					{
					case ExcelOpenType.CSV:
						goto IL_BF;
					case ExcelOpenType.SpreadsheetML:
						goto IL_6E;
					case ExcelOpenType.BIFF:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_77;
						default:
							goto IL_A4;
						}
						break;
					case ExcelOpenType.SpreadsheetML2007:
						goto IL_61;
					case ExcelOpenType.SpreadsheetML2010:
						goto IL_81;
					default:
						num = 0;
						continue;
					}
					break;
				case 2:
					goto IL_7F;
				}
				break;
				IL_77:
				num = 2;
			}
		}
		IL_61:
		return this.ᜀ(A_0, A_1, A_2, A_3, ExcelVersion.Version2007);
		IL_6E:
		return this.ᜀ(A_0, XmlOpenType.MSExcel);
		IL_7F:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⼿㉁⅃⡅᱇㍉㱋⭍", a_));
		IL_81:
		return this.ᜀ(A_0, A_1, A_2, A_3, ExcelVersion.Version2010);
		IL_A4:
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜀ(A_0, A_1, A_2, A_3, ExcelVersion.Version97to2003);
		IL_BF:
		return this.ᜀ(A_0, base.AppImplementation.\u1714());
	}

	// Token: 0x06003FFB RID: 16379 RVA: 0x0023F3D8 File Offset: 0x0023E3D8
	public new IWorkbook ᜀ(Stream A_0)
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
		return this.ᜀ(A_0, base.AppImplementation.ᜋ());
	}

	// Token: 0x06003FFC RID: 16380 RVA: 0x0023F428 File Offset: 0x0023E428
	public new IWorkbook ᜀ(Stream A_0, ExcelVersion A_1)
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
		return this.ᜀ(A_0, A_1, ExcelParseOptions.Default);
	}

	// Token: 0x06003FFD RID: 16381 RVA: 0x0023F46C File Offset: 0x0023E46C
	public new IWorkbook ᜀ(Stream A_0, ExcelVersion A_1, ExcelParseOptions A_2)
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
		XlsWorkbook xlsWorkbook = base.AppImplementation.ᜀ(this, A_0, A_1, A_2);
		base.Add(xlsWorkbook);
		return xlsWorkbook;
	}

	// Token: 0x06003FFE RID: 16382 RVA: 0x0023F4C0 File Offset: 0x0023E4C0
	public new IWorkbook ᜀ(Stream A_0, ExcelParseOptions A_1)
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
		XlsWorkbook xlsWorkbook = base.AppImplementation.ᜀ(this, A_0, A_1, base.AppImplementation.ᜋ());
		base.Add(xlsWorkbook);
		return xlsWorkbook;
	}

	// Token: 0x06003FFF RID: 16383 RVA: 0x0023F520 File Offset: 0x0023E520
	public new IWorkbook ᜀ(string A_0, ExcelOpenType A_1)
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
		return this.ᜁ(A_0, A_1, ExcelParseOptions.Default);
	}

	// Token: 0x06004000 RID: 16384 RVA: 0x0023F564 File Offset: 0x0023E564
	public new IWorkbook ᜁ(string A_0, ExcelOpenType A_1, ExcelParseOptions A_2)
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
		return this.ᜀ(A_0, A_1, base.AppImplementation.ᜋ(), A_2);
	}

	// Token: 0x06004001 RID: 16385 RVA: 0x0023F5B4 File Offset: 0x0023E5B4
	public new IWorkbook ᜀ(string A_0, ExcelOpenType A_1, ExcelVersion A_2)
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
		return this.ᜀ(A_0, A_1, A_2, ExcelParseOptions.Default);
	}

	// Token: 0x06004002 RID: 16386 RVA: 0x0023F5FC File Offset: 0x0023E5FC
	public new IWorkbook ᜀ(string A_0, ExcelOpenType A_1, ExcelVersion A_2, ExcelParseOptions A_3)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				FileStream fileStream;
				switch (num)
				{
				case 0:
					goto IL_F1;
				case 2:
					try
					{
						if (true)
						{
						}
						string currentDirectory = Environment.CurrentDirectory;
						A_0 = Path.GetFullPath(A_0);
						Environment.CurrentDirectory = Path.GetDirectoryName(A_0);
						XlsWorkbook xlsWorkbook = (XlsWorkbook)this.ᜀ(fileStream, A_1, A_0, A_2, A_3);
						xlsWorkbook.FullFileName = Path.GetFullPath(A_0);
						Environment.CurrentDirectory = currentDirectory;
						return xlsWorkbook;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_EE;
							case 2:
								((IDisposable)fileStream).Dispose();
								num = 0;
								continue;
							}
							if (fileStream == null)
							{
								break;
							}
							num = 2;
						}
						IL_EE:;
					}
					goto IL_F1;
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
					if (!File.Exists(A_0))
					{
						goto IL_116;
					}
					break;
				}
				num = 0;
				continue;
				IL_F1:
				fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read, FileShare.Read);
				num = 2;
			}
			IL_116:
			throw new FileNotFoundException(RecordTableEnumerator.b("社⡀⽂⁄杆⩈⑊㡌⍎㕐獒㭔㡖ⵘ筚㽜㩞䅠բ੤ቦݨཪ䍬佮ⅰὲၴᙶ੸Ṻ嵼ॾꮊ歷뎒ﺖﺚ붜삠힢춤覦", a_));
		}
		}
	}

	// Token: 0x06004003 RID: 16387 RVA: 0x0023F744 File Offset: 0x0023E744
	public new IWorkbook ᜀ(string A_0, ExcelParseOptions A_1, bool A_2, string A_3, ExcelOpenType A_4)
	{
		int a_ = 7;
		for (;;)
		{
			FileStream fileStream;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read, FileShare.Read);
				break;
			}
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					ExcelOpenType excelOpenType;
					switch (excelOpenType)
					{
					case ExcelOpenType.CSV:
						goto IL_51;
					case ExcelOpenType.SpreadsheetML:
						goto IL_CD;
					case ExcelOpenType.BIFF:
						goto IL_64;
					case ExcelOpenType.SpreadsheetML2007:
						goto IL_71;
					case ExcelOpenType.SpreadsheetML2010:
						goto IL_D6;
					default:
						num = 2;
						continue;
					}
					break;
				}
				case 1:
					goto IL_EE;
				case 2:
					num = 1;
					continue;
				case 3:
				{
					try
					{
						if (true)
						{
						}
						A_4 = this.ᜀ(fileStream, A_4);
						goto IL_F0;
					}
					finally
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								((IDisposable)fileStream).Dispose();
								num = 1;
								continue;
							case 1:
								goto IL_CA;
							}
							if (fileStream == null)
							{
								break;
							}
							num = 0;
						}
						IL_CA:;
					}
					goto IL_CD;
					IL_F0:
					ExcelOpenType excelOpenType = A_4;
					num = 0;
					continue;
				}
				}
				break;
			}
		}
		IL_51:
		return this.ᜁ(A_0, base.AppImplementation.\u1714());
		IL_64:
		return this.ᜀ(A_0, A_1, A_2, A_3, ExcelVersion.Version97to2003);
		IL_71:
		return this.ᜀ(A_0, A_1, A_2, A_3, ExcelVersion.Version2007);
		IL_CD:
		return this.ᜀ(A_0, XmlOpenType.MSExcel);
		IL_D6:
		return this.ᜀ(A_0, A_1, A_2, A_3, ExcelVersion.Version2010);
		IL_EE:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("刼伾⑀ⵂᅄ㹆㥈⹊", a_));
	}

	// Token: 0x06004004 RID: 16388 RVA: 0x0023F8A4 File Offset: 0x0023E8A4
	public new IWorkbook ᜁ(Stream A_0, ExcelOpenType A_1)
	{
		int a_ = 4;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				ExcelOpenType excelOpenType;
				switch (excelOpenType)
				{
				case ExcelOpenType.CSV:
					goto IL_68;
				case ExcelOpenType.SpreadsheetML:
					goto IL_7B;
				case ExcelOpenType.BIFF:
					goto IL_E8;
				case ExcelOpenType.SpreadsheetML2007:
					goto IL_DF;
				case ExcelOpenType.SpreadsheetML2010:
					goto IL_C2;
				default:
					num = 3;
					continue;
				}
				break;
			}
			case 2:
				goto IL_66;
			case 3:
				num = 4;
				continue;
			case 4:
				goto IL_FC;
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
				if (A_0 == null)
				{
					num = 2;
				}
				else
				{
					A_1 = this.ᜀ(A_0, A_1);
					ExcelOpenType excelOpenType = A_1;
					num = 0;
				}
				break;
			}
		}
		IL_66:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤹䠻䰽┿⍁⥃", a_));
		IL_68:
		return this.ᜀ(A_0, base.ReservedHandle.\u1718());
		IL_7B:
		return this.ᜀ(A_0, XmlOpenType.MSExcel);
		IL_C2:
		return this.ᜀ(A_0, ExcelVersion.Version2010);
		IL_DF:
		return this.ᜀ(A_0, ExcelVersion.Version2007);
		IL_E8:
		return this.ᜀ(A_0, ExcelVersion.Version97to2003);
		IL_FC:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("唹䰻嬽⸿ᙁ㵃㙅ⵇ", a_));
	}

	// Token: 0x06004005 RID: 16389 RVA: 0x0023F9C4 File Offset: 0x0023E9C4
	public new IWorkbook ᜀ(Stream A_0, ExcelOpenType A_1, ExcelParseOptions A_2)
	{
		int a_ = 19;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				ExcelOpenType excelOpenType;
				switch (excelOpenType)
				{
				case ExcelOpenType.CSV:
					goto IL_60;
				case ExcelOpenType.SpreadsheetML:
					goto IL_73;
				case ExcelOpenType.BIFF:
					goto IL_E8;
				case ExcelOpenType.SpreadsheetML2007:
					goto IL_DF;
				case ExcelOpenType.SpreadsheetML2010:
					goto IL_C2;
				default:
					num = 3;
					continue;
				}
				break;
			}
			case 1:
				goto IL_5E;
			case 3:
				num = 4;
				continue;
			case 4:
				goto IL_FD;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			}
			if (false)
			{
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				A_1 = this.ᜀ(A_0, A_1);
				ExcelOpenType excelOpenType = A_1;
				num = 0;
			}
		}
		IL_5E:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩈㽊㽌⩎ぐ㹒", a_));
		IL_60:
		return this.ᜀ(A_0, base.ReservedHandle.\u1718());
		IL_73:
		if (true)
		{
		}
		return this.ᜀ(A_0, XmlOpenType.MSExcel);
		IL_C2:
		return this.ᜀ(A_0, ExcelVersion.Version2010);
		IL_DF:
		return this.ᜀ(A_0, ExcelVersion.Version2007);
		IL_E8:
		return this.ᜀ(A_0, ExcelVersion.Version97to2003, A_2);
		IL_FD:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("♈㭊⡌ⅎՐ⩒╔㉖", a_));
	}

	// Token: 0x06004006 RID: 16390 RVA: 0x0023FAE4 File Offset: 0x0023EAE4
	private new IWorkbook ᜀ(Stream A_0, ExcelOpenType A_1, string A_2, ExcelVersion A_3)
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
		return this.ᜀ(A_0, A_1, A_2, A_3, ExcelParseOptions.Default);
	}

	// Token: 0x06004007 RID: 16391 RVA: 0x0023FB2C File Offset: 0x0023EB2C
	private new IWorkbook ᜀ(Stream A_0, ExcelOpenType A_1, string A_2, ExcelVersion A_3, ExcelParseOptions A_4)
	{
		int a_ = 14;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 2;
				continue;
			case 1:
			{
				ExcelOpenType excelOpenType;
				switch (excelOpenType)
				{
				case ExcelOpenType.CSV:
					goto IL_3D;
				case ExcelOpenType.SpreadsheetML:
					goto IL_56;
				case ExcelOpenType.BIFF:
					goto IL_E8;
				case ExcelOpenType.SpreadsheetML2007:
					goto IL_DD;
				case ExcelOpenType.SpreadsheetML2010:
					goto IL_A2;
				default:
					num = 0;
					continue;
				}
				break;
			}
			case 2:
				goto IL_106;
			case 3:
				goto IL_38;
			}
			if (A_0 == null)
			{
				num = 3;
			}
			else
			{
				A_1 = this.ᜀ(A_0, A_1);
				ExcelOpenType excelOpenType = A_1;
				num = 1;
			}
		}
		IL_38:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝃㉅㩇⽉ⵋ⍍", a_));
		IL_3D:
		return this.ᜀ(A_0, base.AppImplementation.\u1714(), 1, 1, A_2, null, A_3);
		IL_56:
		if (true)
		{
		}
		return this.ᜀ(A_0, XmlOpenType.MSExcel);
		IL_A2:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_38;
		default:
			if (false)
			{
			}
			return this.ᜀ(A_0, ExcelVersion.Version2010, A_4);
		}
		IL_DD:
		return this.ᜀ(A_0, ExcelVersion.Version2007, A_4);
		IL_E8:
		return this.ᜀ(A_0, ExcelVersion.Version97to2003);
		IL_106:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭃㙅ⵇ⑉ᡋ㝍⁏㝑", a_));
	}

	// Token: 0x06004008 RID: 16392 RVA: 0x0023FC54 File Offset: 0x0023EC54
	public new IWorkbook ᜀ(string A_0, XmlOpenType A_1)
	{
		int a_ = 14;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				try
				{
					FileStream fileStream;
					return this.ᜀ(fileStream, A_1);
				}
				finally
				{
					num = 0;
					for (;;)
					{
						FileStream fileStream;
						switch (num)
						{
						case 1:
							goto IL_89;
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
					IL_89:;
				}
				goto IL_8C;
			case 2:
				IL_38:
				goto IL_8C;
			}
			if (File.Exists(A_0))
			{
				num = 2;
				continue;
			}
			break;
			IL_8C:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_38;
			default:
			{
				if (false)
				{
				}
				FileStream fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read, FileShare.Read);
				num = 1;
				break;
			}
			}
		}
		if (true)
		{
		}
		throw new FileNotFoundException(RecordTableEnumerator.b("Ƀ⽅⑇⽉汋❍⍏牑㩓㥕ⱗ穙㥛♝य़ᅡၣ䡥", a_));
	}

	// Token: 0x06004009 RID: 16393 RVA: 0x0023FD50 File Offset: 0x0023ED50
	public new IWorkbook ᜀ(Stream A_0, XmlOpenType A_1)
	{
		int a_ = 5;
		if (A_0 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("䠺䤼䴾⑀≂⡄", a_));
			}
		}
		XmlReader a_2 = UtilityMethods.ᜀ(A_0, false);
		return this.ᜀ(a_2, A_1);
	}

	// Token: 0x0600400A RID: 16394 RVA: 0x0023FDC0 File Offset: 0x0023EDC0
	public new IWorkbook ᜀ(XmlReader A_0, XmlOpenType A_1)
	{
		int a_ = 8;
		int num = 3;
		XlsWorkbook xlsWorkbook;
		for (;;)
		{
			XmlTextReader xmlTextReader;
			switch (num)
			{
			case 0:
				if (xmlTextReader != null)
				{
					num = 6;
					continue;
				}
				goto IL_42;
			case 1:
				goto IL_40;
			case 2:
				goto IL_64;
			case 4:
				if (true)
				{
				}
				goto IL_42;
			case 5:
				if (xlsWorkbook != null)
				{
					num = 2;
					continue;
				}
				goto IL_BE;
			case 6:
				xmlTextReader.WhitespaceHandling = WhitespaceHandling.Significant;
				num = 4;
				continue;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			xmlTextReader = (A_0 as XmlTextReader);
			num = 0;
			continue;
			IL_42:
			xlsWorkbook = new XlsWorkbook(base.AppImplementation, this, A_0, A_1);
			num = 5;
		}
		IL_40:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
		IL_64:
		base.Add(xlsWorkbook);
		return xlsWorkbook;
		IL_BE:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_40;
		default:
			if (false)
			{
			}
			throw new ApplicationException(RecordTableEnumerator.b("爽⼿⍁⁃晅ぇ❉⁋湍㙏㍑㵓㩕㵗㹙牛", a_));
		}
	}

	// Token: 0x0600400B RID: 16395 RVA: 0x0023FEC4 File Offset: 0x0023EEC4
	public new IWorkbook ᜁ(string A_0)
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
		return this.ᜁ(A_0, ExcelParseOptions.Default);
	}

	// Token: 0x0600400C RID: 16396 RVA: 0x0023FF08 File Offset: 0x0023EF08
	public new IWorkbook ᜀ(string A_0, string A_1)
	{
		IWorkbook result;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return result;
		}
		if (false)
		{
		}
		FileStream fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		try
		{
			if (true)
			{
			}
			IWorkbook workbook = this.ᜀ(fileStream, A_1);
			(workbook as XlsWorkbook).ReadOnly = true;
			result = workbook;
		}
		finally
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8A;
				case 1:
					((IDisposable)fileStream).Dispose();
					num = 0;
					continue;
				}
				if (fileStream == null)
				{
					break;
				}
				num = 1;
			}
			IL_8A:;
		}
		return result;
	}

	// Token: 0x0600400D RID: 16397 RVA: 0x0023FFB4 File Offset: 0x0023EFB4
	public new IWorkbook ᜁ(string A_0, ExcelParseOptions A_1)
	{
		IWorkbook result;
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
			FileStream fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			try
			{
				IWorkbook workbook = this.ᜀ(fileStream, A_1);
				(workbook as XlsWorkbook).ReadOnly = true;
				result = workbook;
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						((IDisposable)fileStream).Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_8A;
					}
					if (fileStream == null)
					{
						break;
					}
					num = 0;
				}
				IL_8A:;
			}
			break;
		}
		}
		return result;
	}

	// Token: 0x0600400E RID: 16398 RVA: 0x00240060 File Offset: 0x0023F060
	public new IWorkbook ᜀ(string A_0, ExcelOpenType A_1, ExcelParseOptions A_2)
	{
		IWorkbook result;
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
			FileStream fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			try
			{
				result = this.ᜀ(fileStream, A_1, A_2);
			}
			finally
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						((IDisposable)fileStream).Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_7D;
					}
					if (fileStream == null)
					{
						break;
					}
					num = 1;
				}
				IL_7D:;
			}
			break;
		}
		}
		return result;
	}

	// Token: 0x0600400F RID: 16399 RVA: 0x00240100 File Offset: 0x0023F100
	public new void ᜁ()
	{
		for (;;)
		{
			IWorkbook workbook = base.ReservedHandle.ᜂ();
			if (true)
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
					if (workbook != null)
					{
						goto IL_46;
					}
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_46;
					default:
						if (false)
						{
						}
						workbook.Close(true, null);
						num = 0;
						continue;
					}
					break;
				case 3:
				{
					int num2;
					if (num2 >= 0)
					{
						num = 6;
						continue;
					}
					return;
				}
				case 4:
					if (base.InnerList.Count > 0)
					{
						num = 2;
						continue;
					}
					return;
				case 5:
				{
					int num2 = base.InnerList.IndexOf(workbook);
					num = 3;
					continue;
				}
				case 6:
					num = 4;
					continue;
				}
				break;
				IL_46:
				num = 5;
			}
		}
	}

	// Token: 0x06004010 RID: 16400 RVA: 0x002401E0 File Offset: 0x0023F1E0
	internal sprᬭ(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06004011 RID: 16401 RVA: 0x002401F8 File Offset: 0x0023F1F8
	public new IWorkbook ᜂ()
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
		spr\u214D spr_u214D = base.AppImplementation.ᜪ();
		return spr_u214D.ᜀ(this);
	}

	// Token: 0x06004012 RID: 16402 RVA: 0x00240248 File Offset: 0x0023F248
	private new ExcelOpenType ᜀ(Stream A_0, ExcelOpenType A_1)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 20;
			long position;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					bool flag = false;
					num = 7;
					continue;
				}
				case 1:
					try
					{
						MemoryStream memoryStream;
						A_1 = this.ᜀ(A_0, memoryStream, position);
						goto IL_203;
					}
					finally
					{
						num = 2;
						for (;;)
						{
							MemoryStream memoryStream;
							switch (num)
							{
							case 0:
								goto IL_1EC;
							case 1:
								((IDisposable)memoryStream).Dispose();
								num = 0;
								continue;
							}
							if (memoryStream == null)
							{
								break;
							}
							num = 1;
						}
						IL_1EC:;
					}
					goto IL_1EF;
				case 2:
					A_0.Position = position;
					A_1 = ExcelOpenType.SpreadsheetML2007;
					num = 5;
					continue;
				case 3:
				{
					int num2;
					if (num2 >= 8)
					{
						num = 9;
						continue;
					}
					goto IL_3A6;
				}
				case 4:
				{
					spr\u2496 spr_u = base.AppImplementation.ᜁ(A_0);
					if (true)
					{
					}
					num = 10;
					continue;
				}
				case 5:
					goto IL_203;
				case 6:
					goto IL_161;
				case 7:
					goto IL_3A6;
				case 8:
					goto IL_3A6;
				case 9:
				{
					int num3 = 0;
					num = 21;
					continue;
				}
				case 10:
					try
					{
						num = 1;
						for (;;)
						{
							ExcelOpenType excelOpenType;
							switch (num)
							{
							case 0:
								num = 4;
								continue;
							case 2:
								goto IL_11C;
							case 3:
								excelOpenType = ExcelOpenType.SpreadsheetML2007;
								goto IL_111;
							case 4:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_11C;
								default:
									if (false)
									{
									}
									excelOpenType = ExcelOpenType.BIFF;
									goto IL_111;
								}
								break;
							}
							spr\u2496 spr_u;
							if (!spr\u2389.ᜃ(spr_u.ᜀ()))
							{
								num = 0;
								continue;
							}
							num = 3;
							continue;
							IL_111:
							A_1 = excelOpenType;
							num = 2;
						}
						IL_11C:
						goto IL_203;
					}
					finally
					{
						num = 2;
						for (;;)
						{
							spr\u2496 spr_u;
							switch (num)
							{
							case 0:
								goto IL_15E;
							case 1:
								spr_u.Dispose();
								num = 0;
								continue;
							}
							if (spr_u == null)
							{
								break;
							}
							num = 1;
						}
						IL_15E:;
					}
					goto IL_161;
				case 11:
					goto IL_222;
				case 12:
				{
					bool flag;
					if (flag)
					{
						num = 4;
						continue;
					}
					int num2;
					byte[] array;
					MemoryStream memoryStream = new MemoryStream(array, 0, num2);
					num = 1;
					continue;
				}
				case 13:
					return A_1;
				case 14:
				{
					int num3;
					if (num3 >= 8)
					{
						num = 8;
						continue;
					}
					num = 16;
					continue;
				}
				case 15:
				{
					if (spr\u249E.ᜅ(A_0) == 67324752)
					{
						num = 2;
						continue;
					}
					A_0.Position = position;
					byte[] array = new byte[512];
					int num2 = A_0.Read(array, 0, 512);
					bool flag = true;
					num = 18;
					continue;
				}
				case 16:
				{
					int num3;
					byte[] array;
					if (sprᬭ.ᜄ[num3] != array[num3])
					{
						num = 0;
						continue;
					}
					num3++;
					num = 6;
					continue;
				}
				case 17:
					goto IL_397;
				case 18:
				{
					int num2;
					if (num2 != 0)
					{
						num = 22;
						continue;
					}
					goto IL_203;
				}
				case 19:
					if (A_0 == null)
					{
						num = 17;
						continue;
					}
					position = A_0.Position;
					num = 15;
					continue;
				case 21:
					goto IL_161;
				case 22:
					num = 3;
					continue;
				case 23:
					if (A_1 == ExcelOpenType.Automatic)
					{
						num = 11;
						continue;
					}
					goto IL_3D0;
				}
				if (A_1 != ExcelOpenType.Automatic)
				{
					num = 13;
					continue;
				}
				num = 19;
				continue;
				IL_161:
				num = 14;
				continue;
				IL_203:
				num = 23;
				continue;
				IL_3A6:
				A_0.Position = position;
				num = 12;
			}
			return A_1;
			IL_1EF:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠺䤼䴾⑀≂⡄", a_));
			IL_222:
			throw new ArgumentException(RecordTableEnumerator.b("紺吼匾⑀捂ㅄ㹆㥈⹊浌ⱎぐ㵒牔⍖祘㥚㡜罞፠٢٤ࡦ๨ժѬᕮᑰᝲ孴", a_));
			IL_397:
			goto IL_1EF;
			IL_3D0:
			A_0.Position = position;
			return A_1;
		}
		}
	}

	// Token: 0x06004013 RID: 16403 RVA: 0x0024064C File Offset: 0x0023F64C
	private new ExcelOpenType ᜀ(Stream A_0, MemoryStream A_1, long A_2)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
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
					string text;
					StreamReader streamReader;
					Encoding currentEncoding;
					switch (num)
					{
					case 0:
					{
						int num2;
						if (num2 != -1)
						{
							num = 9;
							continue;
						}
						if (true)
						{
						}
						num = 3;
						continue;
					}
					case 1:
					{
						if (text == null)
						{
							num = 12;
							continue;
						}
						text = text.ToLower();
						int num2 = text.IndexOf(RecordTableEnumerator.b("ਵܷ䈹儻刽", a_));
						num = 0;
						continue;
					}
					case 2:
						goto IL_1CD;
					case 3:
						if (text.IndexOf(RecordTableEnumerator.b("ਵ倷丹儻刽", a_)) == -1)
						{
							num = 6;
							continue;
						}
						goto IL_BB;
					case 4:
						goto IL_1F7;
					case 6:
						num = 8;
						continue;
					case 7:
						goto IL_2C9;
					case 8:
					{
						bool flag;
						if (!flag)
						{
							num = 13;
							continue;
						}
						goto IL_2C9;
					}
					case 9:
					{
						int num2;
						A_0.Position = A_2 + (long)num2;
						ExcelOpenType excelOpenType = ExcelOpenType.SpreadsheetML;
						num = 19;
						continue;
					}
					case 10:
					{
						ExcelOpenType excelOpenType = ExcelOpenType.CSV;
						num = 2;
						continue;
					}
					case 11:
					{
						ExcelOpenType excelOpenType;
						return excelOpenType;
					}
					case 12:
						goto IL_BB;
					case 13:
					{
						string text2;
						bool a_2;
						bool flag = this.ᜀ(text, text2, a_2);
						num = 7;
						continue;
					}
					case 14:
						goto IL_B6;
					case 15:
					{
						bool flag;
						if (!flag)
						{
							num = 10;
							continue;
						}
						goto IL_1CD;
					}
					case 16:
						goto IL_16F;
					case 17:
					{
						bool flag;
						if (!flag)
						{
							num = 11;
							continue;
						}
						return ExcelOpenType.Automatic;
					}
					case 18:
						goto IL_16F;
					case 19:
						goto IL_BB;
					case 20:
					{
						ExcelOpenType excelOpenType;
						if (excelOpenType == ExcelOpenType.Automatic)
						{
							num = 4;
							continue;
						}
						goto IL_1CD;
					}
					case 21:
						goto IL_16D;
					case 22:
					{
						if (A_1 == null)
						{
							num = 21;
							continue;
						}
						string text2 = base.ReservedHandle.\u1718();
						bool a_2 = this.ᜀ(text2, "", false);
						bool flag = false;
						streamReader = new StreamReader(A_1, true);
						text = streamReader.ReadLine();
						currentEncoding = streamReader.CurrentEncoding;
						ExcelOpenType excelOpenType = ExcelOpenType.Automatic;
						num = 16;
						continue;
					}
					}
					if (A_0 == null)
					{
						num = 14;
						continue;
					}
					num = 22;
					continue;
					IL_BB:
					num = 20;
					continue;
					IL_16F:
					num = 1;
					continue;
					IL_1CD:
					num = 17;
					continue;
					IL_2C9:
					A_2 += (long)currentEncoding.GetByteCount(text);
					A_2 += (long)(currentEncoding.GetByteCount(RecordTableEnumerator.b("㰵", a_)) * 2);
					text = streamReader.ReadLine();
					num = 18;
					continue;
				}
				}
				IL_1F7:
				num = 15;
			}
			IL_B6:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔵䰷䠹夻弽ⴿ", a_));
			IL_16D:
			throw new ArgumentNullException(RecordTableEnumerator.b("嬵崷圹医䰽㤿ᅁぃ㑅ⵇ⭉⅋", a_));
		}
		}
	}

	// Token: 0x06004014 RID: 16404 RVA: 0x00240968 File Offset: 0x0023F968
	private new bool ᜀ(string A_0, string A_1, bool A_2)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			int num = 12;
			for (;;)
			{
				bool flag;
				bool flag2;
				int num2;
				switch (num)
				{
				case 0:
					goto IL_118;
				case 1:
					num = 21;
					continue;
				case 2:
					num = 14;
					continue;
				case 3:
					if (flag)
					{
						num = 9;
						continue;
					}
					goto IL_1A2;
				case 4:
				{
					if (true)
					{
					}
					char c;
					if (char.IsSeparator(c))
					{
						goto IL_AF;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_215;
					default:
						if (false)
						{
						}
						num = 15;
						continue;
					}
					break;
				}
				case 5:
				{
					char c;
					flag2 = !char.IsSymbol(c);
					goto IL_F4;
				}
				case 6:
					goto IL_F2;
				case 7:
					goto IL_118;
				case 8:
					goto IL_8D;
				case 9:
					num = 17;
					continue;
				case 10:
				{
					char c;
					if (!char.IsLetterOrDigit(c))
					{
						num = 2;
						continue;
					}
					goto IL_AF;
				}
				case 11:
					goto IL_1E3;
				case 13:
					flag2 = false;
					goto IL_F4;
				case 14:
				{
					char c;
					if (!char.IsPunctuation(c))
					{
						num = 18;
						continue;
					}
					goto IL_AF;
				}
				case 15:
					num = 5;
					continue;
				case 16:
					return false;
				case 17:
					if (A_2)
					{
						num = 1;
						continue;
					}
					return true;
				case 18:
					num = 4;
					continue;
				case 19:
				{
					if (A_1 == null)
					{
						num = 6;
						continue;
					}
					num2 = 0;
					int length = A_0.Length;
					num = 7;
					continue;
				}
				case 20:
				{
					int length;
					if (num2 >= length)
					{
						num = 16;
						continue;
					}
					char c = A_0[num2];
					num = 10;
					continue;
				}
				case 21:
				{
					char c;
					if (A_1.IndexOf(c) == -1)
					{
						num = 11;
						continue;
					}
					goto IL_1A2;
				}
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				num = 19;
				continue;
				IL_AF:
				num = 13;
				continue;
				IL_F4:
				flag = flag2;
				num = 3;
				continue;
				IL_118:
				num = 20;
				continue;
				IL_1A2:
				num2++;
				num = 0;
			}
			IL_8D:
			goto IL_215;
			IL_F2:
			throw new ArgumentNullException(RecordTableEnumerator.b("㩈㽊㽌ᱎ㑐⍒㑔╖㡘⽚㉜ⵞ", a_));
			IL_1E3:
			return true;
			IL_215:
			throw new ArgumentNullException(RecordTableEnumerator.b("㩈㽊㽌᥎ぐ㽒⁔㉖", a_));
		}
		}
	}

	// Token: 0x06004015 RID: 16405 RVA: 0x00240BE4 File Offset: 0x0023FBE4
	// Note: this type is marked as 'beforefieldinit'.
	static sprᬭ()
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
		sprᬭ.ᜄ = new byte[]
		{
			208,
			207,
			17,
			224,
			161,
			177,
			26,
			225
		};
	}

	// Token: 0x04001CC3 RID: 7363
	private new const byte ᜀ = 8;

	// Token: 0x04001CC4 RID: 7364
	private new const string ᜁ = "<?xml";

	// Token: 0x04001CC5 RID: 7365
	private new const string ᜂ = "<html";

	// Token: 0x04001CC6 RID: 7366
	private const int ᜃ = 512;

	// Token: 0x04001CC7 RID: 7367
	private static readonly byte[] ᜄ;
}
