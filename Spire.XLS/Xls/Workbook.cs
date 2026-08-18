using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using Spire.License;
using Spire.Xls.Collections;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Sorting;

namespace Spire.Xls
{
	// Token: 0x02000061 RID: 97
	[LicenseProvider(typeof(Spire.License.LicenseProvider))]
	public sealed class Workbook : IDisposable
	{
		// Token: 0x0600096A RID: 2410 RVA: 0x0005E000 File Offset: 0x0005D000
		public Workbook()
		{
			this.ᜁ = new spr\u2602();
			this.ᜂ = (this.ᜁ.ᜂ() as spr\u2158);
			this.ᜁ.ᜀ(false);
			this.excelWorkbook = (this.ᜂ.ᜥ().Create() as XlsWorkbook);
			this.excelWorkbook.InnerWorkBook = this;
			this.ᜂ.ᜁ(new PasswordRequiredEventHandler(this.ᜀ));
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0005E08C File Offset: 0x0005D08C
		internal void ᜀ(object A_0, PasswordRequiredEventArgs A_1)
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
			A_1.NewPassword = this.OpenPassword;
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0005E0D4 File Offset: 0x0005D0D4
		public void InitCalcEngine()
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_5A:
				this.ᜄ = spr\u1DA0.ᜀ(this.excelWorkbook);
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
					if (true)
					{
					}
					break;
				case 1:
					return;
				case 2:
					goto IL_5A;
				}
				if (this.ᜄ != null)
				{
					break;
				}
				num = 2;
			}
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0005E158 File Offset: 0x0005D158
		public void CalculateAllValue()
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
			this.InitCalcEngine();
			this.ᜄ.ᜁ().ᜀ.ᜂ(false);
			this.ᜄ.ᜂ();
			this.ᜄ.ᜁ().ᜀ.ᜂ(true);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0005E1D0 File Offset: 0x0005D1D0
		public object CaculateFormulaValue(string text)
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
				}
				break;
			}
			string text2;
			DateTime dateTime;
			bool flag;
			for (;;)
			{
				this.InitCalcEngine();
				text2 = this.ᜄ.ᜁ().ᜀ.ឥ(text);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						double num2;
						if (double.TryParse(text2, out num2))
						{
							num = 3;
							continue;
						}
						num = 5;
						continue;
					}
					case 1:
						goto IL_AC;
					case 2:
						if (DateTime.TryParse(text2, out dateTime))
						{
							num = 1;
							continue;
						}
						return text2;
					case 3:
						goto IL_8E;
					case 4:
						goto IL_F2;
					case 5:
						if (true)
						{
						}
						if (bool.TryParse(text2, out flag))
						{
							num = 4;
							continue;
						}
						num = 2;
						continue;
					}
					break;
				}
			}
			IL_8E:
			return double.Parse(text2);
			IL_AC:
			return dateTime;
			IL_F2:
			return flag;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0005E2D4 File Offset: 0x0005D2D4
		public ExcelFont CreateFont()
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
			return new ExcelFont(this.excelWorkbook.CreateFont());
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0005E320 File Offset: 0x0005D320
		public ExcelFont CreateFont(Font font)
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
			return new ExcelFont(this.excelWorkbook.CreateFont(font));
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0005E36C File Offset: 0x0005D36C
		public Worksheet CreateEmptySheet()
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
			return this.excelWorkbook.Worksheets.Create() as Worksheet;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0005E3BC File Offset: 0x0005D3BC
		public Worksheet CreateEmptySheet(string name)
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
			return this.excelWorkbook.Worksheets.Create(name) as Worksheet;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0005E410 File Offset: 0x0005D410
		public void CreateEmptySheets(int sheetCount)
		{
			ExcelVersion version;
			for (;;)
			{
				for (;;)
				{
					if (true)
					{
					}
					version = this.excelWorkbook.Version;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_82;
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
								this.excelWorkbook.Dispose();
								this.excelWorkbook = null;
								num = 0;
								continue;
							}
							break;
						case 2:
							if (this.excelWorkbook != null)
							{
								num = 1;
								continue;
							}
							goto IL_84;
						}
						break;
					}
				}
			}
			IL_82:
			IL_84:
			this.excelWorkbook = (this.ᜂ.ᜥ().Create(sheetCount) as XlsWorkbook);
			this.excelWorkbook.Version = version;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0005E4CC File Offset: 0x0005D4CC
		public void CreateEmptySheets(string[] sheetNames)
		{
			ExcelVersion version;
			for (;;)
			{
				IL_00:
				for (;;)
				{
					version = this.excelWorkbook.Version;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_82;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							}
							if (false)
							{
							}
							this.excelWorkbook.Dispose();
							this.excelWorkbook = null;
							if (true)
							{
							}
							num = 0;
							continue;
						case 2:
							if (this.excelWorkbook != null)
							{
								num = 1;
								continue;
							}
							goto IL_84;
						}
						break;
					}
				}
			}
			IL_82:
			IL_84:
			this.excelWorkbook = (this.ᜂ.ᜥ().Create(sheetNames) as XlsWorkbook);
			this.excelWorkbook.Version = version;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0005E588 File Offset: 0x0005D588
		public void ChangePaletteColor(Color color, int index)
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
			this.excelWorkbook.SetPaletteColor(index, color);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0005E5D0 File Offset: 0x0005D5D0
		public void CopyToClipboard()
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
			this.excelWorkbook.CopyToClipboard();
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0005E618 File Offset: 0x0005D618
		public void CopyToClipboard(Worksheet worksheet)
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
			this.excelWorkbook.CopyToClipboard(worksheet);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0005E660 File Offset: 0x0005D660
		public void PasteFromClipboard()
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
			this.excelWorkbook.Paste();
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0005E6A8 File Offset: 0x0005D6A8
		public void LoadFromFile(string fileName)
		{
			int a_ = 13;
			while (fileName.EndsWith(RecordTableEnumerator.b("浂㵄⭆㩈㍊", a_), StringComparison.CurrentCultureIgnoreCase))
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
					this.LoadFromFile(fileName, ExcelVersion.Version2010);
					return;
				}
			}
			this.LoadFromFile(fileName, ExcelVersion.Version97to2003);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0005E718 File Offset: 0x0005D718
		public void LoadFromFile(string fileName, ExcelVersion version)
		{
			if (true)
			{
			}
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_5A:
				this.excelWorkbook.Close();
				this.excelWorkbook.Dispose();
				this.excelWorkbook = null;
				num = 2;
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
				case 1:
					goto IL_5A;
				case 2:
					goto IL_81;
				}
				if (this.excelWorkbook == null)
				{
					break;
				}
				num = 1;
			}
			IL_81:
			this.excelWorkbook = (this.ᜂ.ᜥ().Open(fileName, ExcelParseOptions.Default, false, this.OpenPassword, version) as XlsWorkbook);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0005E7D0 File Offset: 0x0005D7D0
		public void LoadFromFile(string fileName, bool preserveMode)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_66;
				case 1:
					IL_12:
					break;
				case 2:
					goto IL_98;
				case 3:
					if (preserveMode)
					{
						num = 2;
						continue;
					}
					goto IL_CC;
				case 4:
					if (true)
					{
					}
					this.excelWorkbook.Close();
					this.excelWorkbook.Dispose();
					this.excelWorkbook = null;
					num = 0;
					continue;
				}
				if (this.excelWorkbook != null)
				{
					num = 4;
					continue;
				}
				IL_66:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_12;
				default:
					if (false)
					{
					}
					num = 3;
					break;
				}
			}
			IL_98:
			this.excelWorkbook = (this.ᜂ.ᜥ().Open(fileName, ExcelParseOptions.DoNotParseCharts, false, this.OpenPassword, ExcelVersion.Version97to2003) as XlsWorkbook);
			return;
			IL_CC:
			this.LoadFromFile(fileName);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0005E8B0 File Offset: 0x0005D8B0
		public void LoadFromFile(string fileName, string separator)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_5A:
				this.excelWorkbook.Close();
				this.excelWorkbook.Dispose();
				this.excelWorkbook = null;
				num = 0;
				break;
			default:
				if (false)
				{
				}
				num = 1;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_81;
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					goto IL_5A;
				}
				if (this.excelWorkbook == null)
				{
					break;
				}
				num = 2;
			}
			IL_81:
			this.excelWorkbook = (this.ᜂ.ᜥ().Open(fileName, separator) as XlsWorkbook);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0005E960 File Offset: 0x0005D960
		public void LoadFromFile(string fileName, string separator, int row, int column)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_52:
				this.excelWorkbook.Close();
				this.excelWorkbook.Dispose();
				this.excelWorkbook = null;
				num = 0;
				break;
			default:
				if (false)
				{
				}
				num = 1;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_79;
				case 2:
					goto IL_52;
				}
				if (this.excelWorkbook == null)
				{
					break;
				}
				num = 2;
			}
			IL_79:
			if (true)
			{
			}
			this.excelWorkbook = (this.ᜂ.ᜥ().Open(fileName, separator, row, column) as XlsWorkbook);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0005EA10 File Offset: 0x0005DA10
		public void LoadFromXml(string fileName)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_5A:
				this.excelWorkbook.Close();
				this.excelWorkbook.Dispose();
				this.excelWorkbook = null;
				num = 0;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num = 1;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_81;
				case 2:
					goto IL_5A;
				}
				if (this.excelWorkbook == null)
				{
					break;
				}
				num = 2;
			}
			IL_81:
			this.excelWorkbook = (this.ᜂ.ᜥ().OpenFromXml(fileName, XmlOpenType.MSExcel) as XlsWorkbook);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0005EAC0 File Offset: 0x0005DAC0
		public void LoadFromXml(Stream stream)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_38:
				if (this.excelWorkbook == null)
				{
					goto IL_83;
				}
				if (true)
				{
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
					this.excelWorkbook.Close();
					this.excelWorkbook.Dispose();
					this.excelWorkbook = null;
					num = 1;
					continue;
				case 1:
					goto IL_81;
				}
				break;
			}
			goto IL_38;
			IL_81:
			IL_83:
			this.excelWorkbook = (this.ᜂ.ᜥ().OpenFromXml(stream, XmlOpenType.MSExcel) as XlsWorkbook);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0005EB70 File Offset: 0x0005DB70
		public void LoadFromStream(Stream stream)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_38:
				if (this.excelWorkbook == null)
				{
					goto IL_83;
				}
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
					goto IL_81;
				case 1:
					this.excelWorkbook.Close();
					this.excelWorkbook.Dispose();
					this.excelWorkbook = null;
					if (true)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
			goto IL_38;
			IL_81:
			IL_83:
			this.excelWorkbook = (this.ᜂ.ᜥ().Open(stream) as XlsWorkbook);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0005EC1C File Offset: 0x0005DC1C
		public void LoadFromStream(Stream stream, ExcelVersion version)
		{
			if (true)
			{
			}
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_40:
				if (this.excelWorkbook == null)
				{
					goto IL_83;
				}
				num = 2;
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
				case 1:
					goto IL_81;
				case 2:
					this.excelWorkbook.Close();
					this.excelWorkbook.Dispose();
					this.excelWorkbook = null;
					num = 1;
					continue;
				}
				break;
			}
			goto IL_40;
			IL_81:
			IL_83:
			this.excelWorkbook = (this.ᜂ.ᜥ().Open(stream, version) as XlsWorkbook);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0005ECCC File Offset: 0x0005DCCC
		public void LoadFromStream(Stream stream, bool loadStyles)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_7D:
				goto IL_7F;
			default:
				if (false)
				{
				}
				num = 0;
				break;
			}
			for (;;)
			{
				IL_30:
				switch (num)
				{
				case 1:
					goto IL_87;
				case 2:
					this.excelWorkbook.Close();
					this.excelWorkbook.Dispose();
					this.excelWorkbook = null;
					num = 3;
					continue;
				case 3:
					goto IL_7D;
				}
				if (this.excelWorkbook == null)
				{
					goto IL_7F;
				}
				num = 2;
			}
			IL_87:
			if (true)
			{
			}
			this.excelWorkbook = (this.ᜂ.ᜥ().Open(stream, loadStyles ? ExcelParseOptions.Default : ExcelParseOptions.SkipStyles) as XlsWorkbook);
			return;
			IL_7F:
			num = 1;
			goto IL_30;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0005ED90 File Offset: 0x0005DD90
		public void LoadTemplateFromFile(string fileName)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_38:
				if (this.excelWorkbook == null)
				{
					goto IL_7B;
				}
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
					goto IL_79;
				case 1:
					this.excelWorkbook.Close();
					this.excelWorkbook.Dispose();
					this.excelWorkbook = null;
					num = 0;
					continue;
				}
				break;
			}
			goto IL_38;
			IL_79:
			IL_7B:
			if (true)
			{
			}
			this.excelWorkbook = (XlsWorkbook)this.ᜂ.ᜥ().Open(fileName);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0005EE3C File Offset: 0x0005DE3C
		public void LoadTemplateFromFile(string fileName, bool loadStyles)
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
			this.excelWorkbook = (XlsWorkbook)this.ᜂ.ᜥ().Open(fileName, loadStyles ? ExcelParseOptions.Default : ExcelParseOptions.SkipStyles);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0005EEA0 File Offset: 0x0005DEA0
		public void SaveToFile(string fileName)
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
			this.ᜀ();
			this.excelWorkbook.SaveAs(fileName);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0005EEF0 File Offset: 0x0005DEF0
		public void SaveToFile(string fileName, ExcelVersion version)
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
			this.ᜀ();
			this.excelWorkbook.SaveAs(fileName, ExcelSaveType.SaveAsXLS, version);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0005EF40 File Offset: 0x0005DF40
		public void SaveToFile(string fileName, string separator)
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
			this.ᜀ();
			this.excelWorkbook.SaveAs(fileName, separator);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0005EF90 File Offset: 0x0005DF90
		public void Save()
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
			this.ᜀ();
			this.excelWorkbook.Save();
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0005EFDC File Offset: 0x0005DFDC
		public void SaveAsTemplate(string fileName)
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
			this.ᜀ();
			this.excelWorkbook.SaveAs(fileName, ExcelSaveType.SaveAsTemplate);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0005F02C File Offset: 0x0005E02C
		public void SaveAsTemplate(string fileName, HttpResponse response)
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
			this.ᜀ();
			this.excelWorkbook.SaveAs(fileName, ExcelSaveType.SaveAsTemplate, response);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0005F07C File Offset: 0x0005E07C
		public void SaveAsXml(string fileName)
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
			this.ᜀ();
			this.excelWorkbook.ᜀ(fileName, XmlSaveType.MSExcel);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0005F0CC File Offset: 0x0005E0CC
		public void SaveAsXml(Stream stream)
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
			this.ᜀ();
			this.excelWorkbook.ᜀ(stream, XmlSaveType.MSExcel);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0005F11C File Offset: 0x0005E11C
		public void SaveToStream(Stream Stream)
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
			this.ᜀ();
			this.excelWorkbook.SaveAs(Stream);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0005F16C File Offset: 0x0005E16C
		public void SaveToStream(Stream stream, string separator)
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
			this.ᜀ();
			this.excelWorkbook.SaveAs(stream, separator);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0005F1BC File Offset: 0x0005E1BC
		public void SaveToHttpResponse(string FileName, HttpResponse response)
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
			this.ᜀ();
			this.excelWorkbook.SaveAs(FileName, ExcelSaveType.SaveAsXLS, response);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0005F20C File Offset: 0x0005E20C
		public void SaveToHttpResponse(string FileName, HttpResponse response, bool isInlineMode)
		{
			for (;;)
			{
				if (true)
				{
				}
				this.ᜀ();
				if (isInlineMode)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_42;
				}
			}
			this.excelWorkbook.ᜀ(FileName, response, HttpDownloadType.Open);
			return;
			IL_42:
			if (false)
			{
			}
			this.excelWorkbook.ᜀ(FileName, response, HttpDownloadType.PromptDialog);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0005F270 File Offset: 0x0005E270
		public void SaveToHttpResponse(string FileName, HttpResponse response, HttpContentType httpContextType)
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
			this.ᜀ();
			this.excelWorkbook.ᜀ(FileName, response, httpContextType);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0005F2C0 File Offset: 0x0005E2C0
		public void SetWriteProtectionPassword(string password)
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
			this.excelWorkbook.SetWriteProtectionPassword(password);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0005F308 File Offset: 0x0005E308
		private void ᜀ(Worksheet A_0)
		{
			int a_ = 17;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			A_0.Name = RecordTableEnumerator.b("Ɇ㽈⩊⅌㩎ぐ❒㱔㡖㝘筚ੜ㹞፠ൢ౤०๨", a_);
			CellRange cellRange = A_0.Range[1, 2];
			cellRange.Text = RecordTableEnumerator.b("ᑆ㥈≊㽌⩎罐୒ᥔі祘㵚㉜ⵞ䅠䵢⭤≦㵨", a_);
			cellRange.Style.Font.IsBold = true;
			cellRange = A_0.Range[2, 2];
			cellRange.Text = RecordTableEnumerator.b("≆摈≊⹌⩎㍐㽒⁔㉖祘㡚㉜㉞ᅠɢ୤Ṧ䥨❪ᥬ୮彰卲䝴䝶䥸䥺偼", a_) + DateTime.Now.Year.ToString() + RecordTableEnumerator.b("杆ࡈ❊⅌潎⍐㩒㉔㽖ⵘ⡚絜ⵞѠၢdᕦὨ๪Ὤ୮", a_);
			cellRange.Style.Font.IsBold = true;
			A_0.Range[4, 2].Text = RecordTableEnumerator.b("ཆ♈♊⡌潎⅐㉒㉔㉖", a_);
			HyperLink hyperLink = A_0.HyperLinks.Add(A_0.Range[5, 2]);
			hyperLink.Type = HyperLinkType.Url;
			hyperLink.Address = RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒≔⁖⹘畚㡜牞ࡠbdզըṪ࡬䅮ተᱲᡴ", a_);
			A_0.Range[7, 2].Text = RecordTableEnumerator.b("ц♈╊㥌⹎㉐❒畔ɖ੘", a_);
			HyperLink hyperLink2 = A_0.HyperLinks.Add(A_0.Range[8, 2]);
			hyperLink2.Type = HyperLinkType.Url;
			hyperLink2.Address = RecordTableEnumerator.b("⩆⡈≊⅌㭎㹐楒♔≖⥘⭚㉜ⵞᕠ⍢d䩦hࡪ࡬൮ᵰٲၴ奶᩸ᑺၼ", a_);
			A_0.Range[10, 2].Text = RecordTableEnumerator.b("Ն㱈㉊浌Ŏ㹐⑒瑔", a_);
			HyperLink hyperLink3 = A_0.HyperLinks.Add(A_0.Range[11, 2]);
			hyperLink3.Type = HyperLinkType.Url;
			hyperLink3.Address = RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒≔⁖⹘畚㡜牞ࡠbdզըṪ࡬䅮ተᱲᡴ塶㭸๺Ѽ偾ﺌ벐ﲘ난咽캠톢袤즦첨\udfaa肬솮\udeb0쒲鮴\udfb6춸횺톼", a_);
			A_0.Activate();
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0005F4F4 File Offset: 0x0005E4F4
		private void ᜀ()
		{
			for (;;)
			{
				LicenseType licenseType = LicenseType.None;
				int num = 5;
				for (;;)
				{
					LicenseType licenseType2;
					switch (num)
					{
					case 0:
						goto IL_44;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E9;
						default:
							goto IL_73;
						}
						break;
					case 2:
						licenseType = LicenseType.Runtime;
						num = 4;
						continue;
					case 3:
						num = 7;
						continue;
					case 4:
						goto IL_E9;
					case 5:
						if (this.ᜅ != null)
						{
							num = 3;
							continue;
						}
						goto IL_83;
					case 6:
						if (licenseType2 == LicenseType.Runtime)
						{
							num = 1;
							continue;
						}
						goto IL_EE;
					case 7:
						if (spr\u2067.ᜀ(this.ᜅ))
						{
							num = 2;
							continue;
						}
						goto IL_83;
					}
					break;
					IL_44:
					licenseType2 = licenseType;
					num = 6;
					continue;
					IL_E9:
					goto IL_44;
					IL_83:
					License a_ = null;
					LicenseManager.IsValid(typeof(Workbook), this, out a_);
					licenseType = spr\u2067.ᜀ(a_);
					num = 0;
				}
			}
			IL_73:
			if (false)
			{
			}
			if (true)
			{
			}
			return;
			IL_EE:
			this.ᜀ((Worksheet)this.excelWorkbook.Worksheets.Create());
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0005F60C File Offset: 0x0005E60C
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x0005F650 File Offset: 0x0005E650
		internal XlsWorkbook excelWorkbook
		{
			get
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
				return this.ᜃ;
			}
			set
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_6F:
					num = 0;
					break;
				default:
					if (false)
					{
					}
					goto IL_3A;
				}
				for (;;)
				{
					IL_28:
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (true)
						{
						}
						if (this.ᜃ != null)
						{
							num = 2;
							continue;
						}
						return;
					case 2:
						goto IL_61;
					}
					goto IL_3A;
				}
				IL_61:
				this.ᜃ.InnerWorkBook = this;
				goto IL_6F;
				IL_3A:
				this.ᜃ = value;
				num = 1;
				goto IL_28;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x0005F6D8 File Offset: 0x0005E6D8
		public WorksheetsCollection Worksheets
		{
			get
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
				return (WorksheetsCollection)this.excelWorkbook.Worksheets;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0005F724 File Offset: 0x0005E724
		public Color[] Colors
		{
			get
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
				return this.excelWorkbook.Palette;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0005F76C File Offset: 0x0005E76C
		public StylesCollection Styles
		{
			get
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
				return (StylesCollection)this.excelWorkbook.Styles;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x0005F7B8 File Offset: 0x0005E7B8
		// (set) Token: 0x0600099B RID: 2459 RVA: 0x0005F800 File Offset: 0x0005E800
		public int ActiveSheetIndex
		{
			get
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
				return this.excelWorkbook.ActiveSheetIndex;
			}
			set
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
				this.excelWorkbook.ActiveSheetIndex = value;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0005F848 File Offset: 0x0005E848
		public ChartsCollection Charts
		{
			get
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
				return (ChartsCollection)this.excelWorkbook.Charts;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x0005F894 File Offset: 0x0005E894
		public PivotCachesCollection PivotCaches
		{
			get
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
				return (PivotCachesCollection)this.excelWorkbook.PivotCaches;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0005F8E0 File Offset: 0x0005E8E0
		public BuiltInDocumentProperties DocumentProperties
		{
			get
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
				return (BuiltInDocumentProperties)this.excelWorkbook.BuiltInDocumentProperties;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0005F92C File Offset: 0x0005E92C
		public Worksheet ActiveSheet
		{
			get
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
				return (Worksheet)this.excelWorkbook.ActiveSheet;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0005F978 File Offset: 0x0005E978
		public MarkerDesigner MarkerDesigner
		{
			get
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_38:
					if (this.ᜀ != null)
					{
						goto IL_74;
					}
					num = 0;
					break;
				default:
					if (false)
					{
					}
					num = 1;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜀ = new MarkerDesigner(this.excelWorkbook.CreateTemplateMarkersProcessor());
						num = 2;
						continue;
					case 2:
						goto IL_72;
					}
					break;
				}
				goto IL_38;
				IL_72:
				IL_74:
				if (true)
				{
				}
				return this.ᜀ;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0005FA08 File Offset: 0x0005EA08
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x0005FA50 File Offset: 0x0005EA50
		public ExcelVersion Version
		{
			get
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
				return this.excelWorkbook.Version;
			}
			set
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
				this.excelWorkbook.Version = value;
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0005FA98 File Offset: 0x0005EA98
		public void Dispose()
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
			this.ᜂ.ᜀ(new PasswordRequiredEventHandler(this.ᜀ));
			this.ᜁ.ᜁ();
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x0005FAF8 File Offset: 0x0005EAF8
		public INameRanges NameRanges
		{
			get
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
				return this.excelWorkbook.Names;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0005FB40 File Offset: 0x0005EB40
		public AddInFunctionsCollection AddInFunctions
		{
			get
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
				return (AddInFunctionsCollection)this.excelWorkbook.AddInFunctions;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x0005FB8C File Offset: 0x0005EB8C
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x0005FBD4 File Offset: 0x0005EBD4
		internal bool Allow3DRangesInDataValidation
		{
			get
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
				return this.excelWorkbook.Allow3DRangesInDataValidation;
			}
			set
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
				this.excelWorkbook.Allow3DRangesInDataValidation = value;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0005FC1C File Offset: 0x0005EC1C
		public DataSorter DataSorter
		{
			get
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
				return this.excelWorkbook.ᜰ();
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0005FC64 File Offset: 0x0005EC64
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x0005FCAC File Offset: 0x0005ECAC
		public string CodeName
		{
			get
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
				return this.excelWorkbook.CodeName;
			}
			set
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
				this.excelWorkbook.CodeName = value;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x0005FCF4 File Offset: 0x0005ECF4
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x0005FD3C File Offset: 0x0005ED3C
		public bool Date1904
		{
			get
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
				return this.excelWorkbook.Date1904;
			}
			set
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
				this.excelWorkbook.Date1904 = value;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x0005FD84 File Offset: 0x0005ED84
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x0005FDCC File Offset: 0x0005EDCC
		public bool DisableMacrosStart
		{
			get
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
				return this.excelWorkbook.DisableMacrosStart;
			}
			set
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
				this.excelWorkbook.DisableMacrosStart = value;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0005FE14 File Offset: 0x0005EE14
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x0005FE5C File Offset: 0x0005EE5C
		public int SelectedTab
		{
			get
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
				return this.excelWorkbook.DisplayedTab;
			}
			set
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
				this.excelWorkbook.DisplayedTab = value;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0005FEA4 File Offset: 0x0005EEA4
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x0005FEEC File Offset: 0x0005EEEC
		public bool ShowTabs
		{
			get
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
				return this.excelWorkbook.DisplayWorkbookTabs;
			}
			set
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
				this.excelWorkbook.DisplayWorkbookTabs = value;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0005FF34 File Offset: 0x0005EF34
		public string FileName
		{
			get
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
				return this.excelWorkbook.FullFileName;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x0005FF7C File Offset: 0x0005EF7C
		// (set) Token: 0x060009B5 RID: 2485 RVA: 0x0005FFC4 File Offset: 0x0005EFC4
		internal bool HasDuplicatedNames
		{
			get
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
				return this.excelWorkbook.HasDuplicatedNames;
			}
			set
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
				this.excelWorkbook.HasDuplicatedNames = value;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0006000C File Offset: 0x0005F00C
		public bool HasMacros
		{
			get
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
				return this.excelWorkbook.HasMacros;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00060054 File Offset: 0x0005F054
		public bool IsWindowProtection
		{
			get
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
				return this.excelWorkbook.IsWindowProtection;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0006009C File Offset: 0x0005F09C
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x000600E4 File Offset: 0x0005F0E4
		public bool IsRightToLeft
		{
			get
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
				return this.excelWorkbook.IsRightToLeft;
			}
			set
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
				this.excelWorkbook.IsRightToLeft = value;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0006012C File Offset: 0x0005F12C
		public bool IsCellProtection
		{
			get
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
				return this.excelWorkbook.IsCellProtection;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x00060174 File Offset: 0x0005F174
		internal bool InLoading
		{
			get
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
				return this.excelWorkbook.Loading;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x000601BC File Offset: 0x0005F1BC
		public bool ReadOnly
		{
			get
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
				return this.excelWorkbook.ReadOnly;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x00060204 File Offset: 0x0005F204
		// (set) Token: 0x060009BE RID: 2494 RVA: 0x0006024C File Offset: 0x0005F24C
		public bool IsSaved
		{
			get
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
				return this.excelWorkbook.Saved;
			}
			set
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
				this.excelWorkbook.Saved = value;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x00060294 File Offset: 0x0005F294
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x000602DC File Offset: 0x0005F2DC
		public string DefaultFontName
		{
			get
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
				return this.excelWorkbook.StandardFont;
			}
			set
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
				this.excelWorkbook.StandardFont = value;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00060324 File Offset: 0x0005F324
		internal XlsFont DefaultFont
		{
			get
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
				return this.excelWorkbook.DefaultFont;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0006036C File Offset: 0x0005F36C
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x000603B0 File Offset: 0x0005F3B0
		public string OpenPassword
		{
			get
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
				return this.ᜆ;
			}
			set
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
				this.ᜆ = value;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x000603F4 File Offset: 0x0005F3F4
		// (set) Token: 0x060009C5 RID: 2501 RVA: 0x0006043C File Offset: 0x0005F43C
		public double DefaultFontSize
		{
			get
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
				return this.excelWorkbook.StandardFontSize;
			}
			set
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
				this.excelWorkbook.StandardFontSize = value;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00060484 File Offset: 0x0005F484
		// (set) Token: 0x060009C7 RID: 2503 RVA: 0x000604C8 File Offset: 0x0005F4C8
		internal InternalLicense InternalLicense
		{
			get
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
			set
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
				this.ᜅ = value;
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0006050C File Offset: 0x0005F50C
		public double ColumnWidthToPixels(double columnWidth)
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
			return this.excelWorkbook.FileWidthToPixels(columnWidth);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00060554 File Offset: 0x0005F554
		public double PixelsToColumnWidth(double pixels)
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
			return this.excelWorkbook.PixelsToWidth(pixels);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0006059C File Offset: 0x0005F59C
		public void Protect(string password)
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
			this.excelWorkbook.PasswordToOpen = password;
			this.excelWorkbook.Protect(true, true);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x000605F0 File Offset: 0x0005F5F0
		public void UnProtect()
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
			this.excelWorkbook.PasswordToOpen = string.Empty;
			this.excelWorkbook.Unprotect();
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00060648 File Offset: 0x0005F648
		public Color GetPaletteColor(ExcelColors color)
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
			return this.excelWorkbook.GetPaletteColor(color);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00060690 File Offset: 0x0005F690
		public void Replace(string oldValue, string newValue)
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
			this.excelWorkbook.Replace(oldValue, newValue);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x000606D8 File Offset: 0x0005F6D8
		public void Replace(string oldValue, DateTime newValue)
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
			this.excelWorkbook.Replace(oldValue, newValue);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00060720 File Offset: 0x0005F720
		public void Replace(string oldValue, double newValue)
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
			this.excelWorkbook.Replace(oldValue, newValue);
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00060768 File Offset: 0x0005F768
		public void Replace(string oldValue, string[] newValues, bool isVertical)
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
			this.excelWorkbook.Replace(oldValue, newValues, isVertical);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x000607B4 File Offset: 0x0005F7B4
		public void Replace(string oldValue, int[] newValues, bool isVertical)
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
			this.excelWorkbook.Replace(oldValue, newValues, isVertical);
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00060800 File Offset: 0x0005F800
		public void Replace(string oldValue, double[] newValues, bool isVertical)
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
			this.excelWorkbook.Replace(oldValue, newValues, isVertical);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0006084C File Offset: 0x0005F84C
		public void Replace(string oldValue, DataTable newValues, bool includeColumnName)
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
			this.excelWorkbook.Replace(oldValue, newValues, includeColumnName);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00060898 File Offset: 0x0005F898
		public void Replace(string oldValue, DataColumn newValues, bool includeColumnName)
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
			this.excelWorkbook.Replace(oldValue, newValues, includeColumnName);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x000608E4 File Offset: 0x0005F8E4
		public void UpdateFormula(CellRange sourceRange, CellRange destRange)
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
			this.excelWorkbook.UpdateFormula(sourceRange, destRange);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0006092C File Offset: 0x0005F92C
		public bool ContainsFont(ExcelFont font)
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
			return this.excelWorkbook.ContainsFont(font.Font);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00060978 File Offset: 0x0005F978
		public void ResetPalette()
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
			this.excelWorkbook.ResetPalette();
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x000609C0 File Offset: 0x0005F9C0
		public ExcelColors GetMatchingColor(Color color)
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
			return this.excelWorkbook.GetNearestColor(color);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00060A08 File Offset: 0x0005FA08
		public ExcelColors GetMatchingColor(int r, int g, int b)
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
			return this.excelWorkbook.GetNearestColor(r, g, b);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00060A54 File Offset: 0x0005FA54
		public CellRange[] FindAllBool(bool boolValue)
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
			return this.excelWorkbook.FindAll(boolValue);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00060A9C File Offset: 0x0005FA9C
		public CellRange[] FindAllDateTime(DateTime dateTimeValue)
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
			return this.excelWorkbook.FindAll(dateTimeValue);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00060AE4 File Offset: 0x0005FAE4
		public CellRange[] FindAllNumber(double doubleValue, bool formulaValue)
		{
			if (formulaValue)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_42;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.excelWorkbook.FindAll(doubleValue, FindType.Number | FindType.FormulaValue);
			}
			IL_42:
			return this.excelWorkbook.FindAll(doubleValue, FindType.Number);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00060B44 File Offset: 0x0005FB44
		public CellRange[] FindAllString(string stringValue, bool formula, bool formulaValue)
		{
			FindType findType;
			for (;;)
			{
				findType = FindType.Text;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4E;
					case 1:
						if (formula)
						{
							num = 2;
							continue;
						}
						goto IL_4E;
					case 2:
						if (true)
						{
						}
						findType |= (FindType.Formula | FindType.FormulaStringValue);
						goto IL_71;
					case 3:
						findType |= FindType.FormulaValue;
						num = 5;
						continue;
					case 4:
						if (formulaValue)
						{
							num = 3;
							continue;
						}
						goto IL_7B;
					case 5:
						goto IL_7B;
					}
					break;
					IL_4E:
					num = 4;
					continue;
					IL_71:
					num = 0;
					continue;
					IL_7B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_71;
					default:
						goto IL_91;
					}
				}
			}
			IL_91:
			if (false)
			{
			}
			return this.excelWorkbook.FindAll(stringValue, findType);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00060BF8 File Offset: 0x0005FBF8
		public CellRange[] FindAllTimeSpan(TimeSpan timeSpanValue)
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
			return this.excelWorkbook.FindAll(timeSpanValue);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00060C40 File Offset: 0x0005FC40
		public CellRange FindBool(bool boolValue)
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
			return (CellRange)this.excelWorkbook.FindOne(boolValue);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00060C8C File Offset: 0x0005FC8C
		public CellRange FindDateTime(DateTime dateTimeValue)
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
			return (CellRange)this.excelWorkbook.FindOne(dateTimeValue);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00060CD8 File Offset: 0x0005FCD8
		public CellRange FindNumber(double doubleValue, bool formulaValue)
		{
			if (formulaValue)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_47;
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.excelWorkbook.FindOne(doubleValue, FindType.Number | FindType.FormulaValue) as CellRange;
			}
			IL_47:
			return this.excelWorkbook.FindOne(doubleValue, FindType.Number) as CellRange;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00060D40 File Offset: 0x0005FD40
		public CellRange FindString(string stringValue, bool formula, bool formulaValue)
		{
			FindType findType;
			for (;;)
			{
				findType = FindType.Text;
				if (true)
				{
				}
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (formulaValue)
						{
							num = 2;
							continue;
						}
						goto IL_7B;
					case 1:
						goto IL_4E;
					case 2:
						findType |= FindType.FormulaValue;
						num = 3;
						continue;
					case 3:
						goto IL_7B;
					case 4:
						if (formula)
						{
							num = 5;
							continue;
						}
						goto IL_4E;
					case 5:
						findType |= (FindType.Formula | FindType.FormulaStringValue);
						goto IL_71;
					}
					break;
					IL_4E:
					num = 0;
					continue;
					IL_71:
					num = 1;
					continue;
					IL_7B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_71;
					default:
						goto IL_91;
					}
				}
			}
			IL_91:
			if (false)
			{
			}
			return this.excelWorkbook.FindOne(stringValue, findType) as CellRange;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00060DF8 File Offset: 0x0005FDF8
		public CellRange FindTimeSpan(TimeSpan timeSpanValue)
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
			return this.excelWorkbook.FindOne(timeSpanValue) as CellRange;
		}

		// Token: 0x040001CD RID: 461
		private MarkerDesigner ᜀ;

		// Token: 0x040001CE RID: 462
		private spr\u2602 ᜁ;

		// Token: 0x040001CF RID: 463
		private float \u25D9\u0081\u009A\u007F;

		// Token: 0x040001D0 RID: 464
		private spr\u2158 ᜂ;

		// Token: 0x040001D1 RID: 465
		private bool[] \u25D8\u0081\u0081ª;

		// Token: 0x040001D2 RID: 466
		private float[] \u2609\u009E\u00B0\u009F;

		// Token: 0x040001D3 RID: 467
		internal XlsWorkbook ᜃ;

		// Token: 0x040001D4 RID: 468
		private spr\u1DA0 ᜄ;

		// Token: 0x040001D5 RID: 469
		private InternalLicense ᜅ;

		// Token: 0x040001D6 RID: 470
		private string ᜆ = string.Empty;
	}
}
