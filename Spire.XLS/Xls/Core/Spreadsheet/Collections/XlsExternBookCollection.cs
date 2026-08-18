using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Security;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x020001E7 RID: 487
	public class XlsExternBookCollection : CollectionExtended<XlsExternWorkbook>
	{
		// Token: 0x06001BBE RID: 7102 RVA: 0x000EEEAC File Offset: 0x000EDEAC
		internal XlsExternBookCollection(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ();
		}

		// Token: 0x17000A5E RID: 2654
		public new XlsExternWorkbook this[int index]
		{
			get
			{
				int a_ = 18;
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_8E:
					num = 3;
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
						goto IL_85;
					case 2:
						num = 1;
						continue;
					case 3:
						goto IL_A0;
					}
					if (index < 0)
					{
						break;
					}
					num = 2;
				}
				IL_5B:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⅇ⑉⡋⭍⡏", a_), RecordTableEnumerator.b("ṇ⭉⁋㭍㕏牑㝓㝕㙗㑙㍛⩝䁟aţ䙥ѧཀྵὫᵭ偯ٱᱳ᝵ᙷ婹䱻幽ꚅ뚕ﶛ肟쮣펥욧\udea9", a_));
				IL_85:
				if (index > base.Count)
				{
					goto IL_8E;
				}
				return base.List[index];
				IL_A0:
				goto IL_5B;
			}
		}

		// Token: 0x17000A5F RID: 2655
		public XlsExternWorkbook this[string strUrl]
		{
			get
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_63:
					num = 1;
					break;
				default:
					if (false)
					{
					}
					num = 3;
					break;
				}
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						goto IL_75;
					case 2:
						goto IL_5B;
					}
					if (strUrl == null)
					{
						break;
					}
					num = 0;
				}
				IL_51:
				return null;
				IL_5B:
				if (strUrl.Length == 0)
				{
					goto IL_63;
				}
				XlsExternWorkbook result;
				this.ᜄ.TryGetValue(strUrl, out result);
				return result;
				IL_75:
				goto IL_51;
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x000EF030 File Offset: 0x000EE030
		public XlsWorkbook ParentWorkbook
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
				return this.ᜃ;
			}
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x000EF074 File Offset: 0x000EE074
		internal new int ᜀ(BiffRecordRaw[] A_0, int A_1)
		{
			int a_ = 19;
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (A_1 > A_0.Length - 1)
					{
						num = 2;
						continue;
					}
					goto IL_E4;
				case 2:
					goto IL_CE;
				case 3:
					if (A_0[A_1].TypeCode != TBIFFRecord.SupBook)
					{
						num = 7;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D0;
					default:
					{
						if (false)
						{
						}
						XlsExternWorkbook xlsExternWorkbook = new XlsExternWorkbook(base.ReservedHandle, this);
						xlsExternWorkbook.Index = base.InnerList.Count;
						A_1 = xlsExternWorkbook.ᜀ(A_0, A_1);
						this.Add(xlsExternWorkbook);
						num = 5;
						continue;
					}
					}
					break;
				case 4:
					if (true)
					{
					}
					if (A_1 >= 0)
					{
						num = 0;
						continue;
					}
					goto IL_135;
				case 5:
					goto IL_E4;
				case 6:
					goto IL_55;
				case 7:
					return A_1;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				num = 4;
				continue;
				IL_E4:
				num = 3;
			}
			IL_55:
			goto IL_D0;
			IL_CE:
			goto IL_135;
			IL_D0:
			throw new ArgumentNullException(RecordTableEnumerator.b("⡈㥊㽌୎ぐ❒㑔", a_));
			IL_135:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⁈ъ⭌⥎≐㙒⅔", a_), RecordTableEnumerator.b("Ὀ⩊⅌㩎㑐獒㙔㙖㝘㕚㉜⭞䅠Ţd䝦ը๪Ṭᱮ兰ݲᵴᙶ᝸孺䵼彾Ꞇ力랖ﲜ膠슢힤햦쪪\ud9ac캮龰ﾲ킴\ud9b6\udeb8쾺햼龾", a_));
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x000EF1DC File Offset: 0x000EE1DC
		internal new void ᜀ(sprἛ A_0, IDecryptor A_1)
		{
			int a_ = 9;
			for (;;)
			{
				IL_09:
				int num = 1;
				for (;;)
				{
					TBIFFRecord tbiffrecord;
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						goto IL_C7;
					case 2:
					{
						if (tbiffrecord != TBIFFRecord.SupBook)
						{
							num = 3;
							continue;
						}
						XlsExternWorkbook xlsExternWorkbook = new XlsExternWorkbook((spr\u2158)base.ReservedHandle, this);
						xlsExternWorkbook.ᜀ(A_0, A_1);
						this.Add(xlsExternWorkbook);
						tbiffrecord = A_0.ᜉ();
						num = 5;
						continue;
					}
					case 3:
						return;
					case 4:
						goto IL_3C;
					case 5:
						goto IL_C7;
					}
					if (A_0 == null)
					{
						num = 4;
						continue;
					}
					tbiffrecord = A_0.ᜉ();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					IL_C7:
					num = 2;
				}
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀≂⅄≆㭈", a_));
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x000EF2D4 File Offset: 0x000EE2D4
		public void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 19;
			for (;;)
			{
				IL_09:
				int num = 0;
				for (;;)
				{
					int num2;
					int count;
					switch (num)
					{
					case 1:
						goto IL_B6;
					case 2:
						return;
					case 3:
						if (num2 >= count)
						{
							num = 2;
							continue;
						}
						if (true)
						{
						}
						base.List[num2].ᜀ(records);
						num2++;
						num = 4;
						continue;
					case 4:
						goto IL_B6;
					case 5:
						goto IL_3C;
					}
					if (records == null)
					{
						num = 5;
						continue;
					}
					num2 = 0;
					count = base.Count;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					IL_B6:
					num = 3;
				}
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊⹌⁎⍐㝒♔", a_));
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x000EF3B8 File Offset: 0x000EE3B8
		public new int Add(XlsExternWorkbook book)
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
			book.Index = base.List.Count;
			base.Add(book);
			return base.Count - 1;
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x000EF414 File Offset: 0x000EE414
		public int Add(string fileName)
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
			return this.Add(fileName, false);
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x000EF458 File Offset: 0x000EE458
		public int Add(string fileName, bool bAddInFunctions)
		{
			switch (0)
			{
			default:
			{
				int num2;
				int firstSheetIndex;
				for (;;)
				{
					XlsExternWorkbook xlsExternWorkbook = new XlsExternWorkbook(base.ReservedHandle, this);
					xlsExternWorkbook.IsInternalReference = false;
					xlsExternWorkbook.IsAddInFunctions = true;
					int num = 0;
					for (;;)
					{
						int sheetNumber;
						int num3;
						switch (num)
						{
						case 0:
							xlsExternWorkbook.URL = ((fileName != null) ? Path.GetFullPath(fileName) : fileName);
							num2 = this.Add(xlsExternWorkbook);
							sheetNumber = xlsExternWorkbook.SheetNumber;
							num = 2;
							continue;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_120;
							default:
								goto IL_B4;
							}
							break;
						case 2:
							if (sheetNumber != 0)
							{
								num = 4;
								continue;
							}
							num = 6;
							continue;
						case 3:
							goto IL_88;
						case 4:
							num = 8;
							continue;
						case 5:
							num = 1;
							continue;
						case 6:
							num3 = 65534;
							goto IL_113;
						case 7:
							goto IL_120;
						case 8:
							num3 = 0;
							goto IL_113;
						}
						break;
						IL_120:
						if (sheetNumber != 0)
						{
							num = 5;
							continue;
						}
						if (true)
						{
						}
						num = 3;
						continue;
						IL_113:
						firstSheetIndex = num3;
						num = 7;
					}
				}
				IL_88:
				int num4 = 65534;
				goto IL_137;
				IL_B4:
				if (false)
				{
				}
				num4 = 0;
				IL_137:
				int lastSheetIndex = num4;
				this.ᜃ.AddSheetReference(num2, firstSheetIndex, lastSheetIndex);
				return num2;
			}
			}
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x000EF5B0 File Offset: 0x000EE5B0
		public int AddDDEFile(string fileName)
		{
			int a_ = 8;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			XlsExternWorkbook xlsExternWorkbook = new XlsExternWorkbook(base.ReservedHandle, this);
			xlsExternWorkbook.IsInternalReference = false;
			xlsExternWorkbook.URL = fileName;
			int num = this.Add(xlsExternWorkbook);
			xlsExternWorkbook.SheetNumber = 0;
			int firstSheetIndex = 65534;
			int lastSheetIndex = 65534;
			this.ᜃ.AddSheetReference(num, firstSheetIndex, lastSheetIndex);
			sprᭆ sprᭆ = xlsExternWorkbook.ExternNames;
			int a_2 = sprᭆ.ᜃ(RecordTableEnumerator.b("洽㐿♁C⥅⭇㽉⅋⭍㹏♑", a_));
			sprἉ sprἉ = sprᭆ.ᜀ(a_2);
			sprἉ.ᜄ().ᜁ(32746);
			return num;
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x000EF678 File Offset: 0x000EE678
		public int Add(string filePath, string fileName, List<string> sheets, string[] names)
		{
			int a_ = 13;
			int num = 4;
			XlsExternWorkbook xlsExternWorkbook;
			int result;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_122;
				case 1:
					goto IL_FC;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_FC;
					default:
						if (false)
						{
						}
						if (sheets == null)
						{
							num = 8;
							continue;
						}
						num = 6;
						continue;
					}
					break;
				case 3:
					num = 1;
					continue;
				case 5:
					goto IL_88;
				case 6:
					goto IL_133;
				case 7:
					xlsExternWorkbook.URL = ((filePath == null) ? fileName : (filePath + fileName));
					result = this.Add(xlsExternWorkbook);
					num = 2;
					continue;
				case 8:
					num = 0;
					continue;
				case 9:
					goto IL_90;
				}
				if (fileName != null)
				{
					num = 3;
					continue;
				}
				goto IL_88;
				IL_FC:
				if (fileName.Length == 0)
				{
					num = 5;
					continue;
				}
				xlsExternWorkbook = new XlsExternWorkbook(base.AppImplementation, this);
				xlsExternWorkbook.IsInternalReference = false;
				num = 7;
				continue;
				IL_88:
				num = 9;
			}
			IL_90:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("╂ⱄ⭆ⱈՊⱌ≎㑐", a_));
			IL_122:
			int num2 = 0;
			goto IL_13B;
			IL_133:
			num2 = sheets.Count;
			IL_13B:
			int sheetNumber = num2;
			xlsExternWorkbook.SheetNumber = sheetNumber;
			xlsExternWorkbook.ᜀ(sheets);
			xlsExternWorkbook.AddNames(names);
			return result;
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x000EF7D8 File Offset: 0x000EE7D8
		public int InsertSelfSupbook()
		{
			switch (0)
			{
			default:
			{
				int num;
				XlsExternWorkbook xlsExternWorkbook;
				for (;;)
				{
					for (;;)
					{
						num = 0;
						int count = base.Count;
						int num2 = 4;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								if (num >= count)
								{
									num2 = 2;
									continue;
								}
								xlsExternWorkbook = this[num];
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									num2 = 3;
									continue;
								}
								break;
							case 1:
								goto IL_D8;
							case 2:
								goto IL_F6;
							case 3:
								if (xlsExternWorkbook.IsInternalReference)
								{
									num2 = 1;
									continue;
								}
								num++;
								if (true)
								{
								}
								num2 = 5;
								continue;
							case 4:
								goto IL_DA;
							case 5:
								goto IL_DA;
							}
							break;
							IL_DA:
							num2 = 0;
						}
					}
				}
				IL_D8:
				xlsExternWorkbook.SheetNumber = (int)((ushort)(this.ᜃ.Worksheets.Count + this.ᜃ.Charts.Count));
				return num;
				IL_F6:
				base.Add(new XlsExternWorkbook(base.ReservedHandle, this)
				{
					Index = base.List.Count,
					IsInternalReference = true,
					SheetNumber = (int)((ushort)(this.ᜃ.Worksheets.Count + this.ᜃ.Charts.Count))
				});
				return base.Count - 1;
			}
			}
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x000EF93C File Offset: 0x000EE93C
		public bool ContainsExternName(string strName)
		{
			for (;;)
			{
				int num = 0;
				int count = base.Count;
				int num2 = 5;
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
							num2 = 3;
							continue;
						}
						XlsExternWorkbook xlsExternWorkbook = this[num];
						num2 = 4;
						continue;
					}
					case 2:
						return true;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_33;
						default:
							goto IL_AD;
						}
						break;
					case 4:
					{
						XlsExternWorkbook xlsExternWorkbook;
						if (xlsExternWorkbook.ExternNames.ᜀ(strName))
						{
							num2 = 2;
							continue;
						}
						goto IL_33;
					}
					case 5:
						goto IL_7D;
					}
					break;
					IL_33:
					if (true)
					{
					}
					num++;
					num2 = 0;
					continue;
					IL_7D:
					num2 = 1;
				}
			}
			return true;
			IL_AD:
			if (false)
			{
			}
			return false;
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x000EFA00 File Offset: 0x000EEA00
		public bool ContainsExternName(string strName, ref int iBookIndex, ref int iNameIndex)
		{
			XlsExternWorkbook xlsExternWorkbook;
			for (;;)
			{
				int num = 0;
				int count = base.Count;
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_33;
						default:
							goto IL_D5;
						}
						break;
					case 1:
						if (iNameIndex >= 0)
						{
							num2 = 4;
							continue;
						}
						goto IL_33;
					case 2:
						goto IL_A5;
					case 3:
						if (num >= count)
						{
							num2 = 0;
							continue;
						}
						xlsExternWorkbook = this[num];
						iNameIndex = xlsExternWorkbook.ExternNames.ᜂ(strName);
						num2 = 1;
						continue;
					case 4:
						goto IL_A3;
					case 5:
						goto IL_A5;
					}
					break;
					IL_33:
					num++;
					if (true)
					{
					}
					num2 = 2;
					continue;
					IL_A5:
					num2 = 3;
				}
			}
			IL_A3:
			iBookIndex = this.ᜃ.AddSheetReference(xlsExternWorkbook.Index, 65534, 65534);
			return true;
			IL_D5:
			if (false)
			{
			}
			return false;
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x000EFAEC File Offset: 0x000EEAEC
		public int GetNameIndexes(string strName, out int iRefIndex)
		{
			switch (0)
			{
			default:
			{
				int num;
				int num3;
				for (;;)
				{
					for (;;)
					{
						iRefIndex = -1;
						num = 0;
						int count = base.Count;
						int num2 = 4;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								if (num >= count)
								{
									num2 = 3;
									continue;
								}
								if (true)
								{
								}
								XlsExternWorkbook xlsExternWorkbook = this[num];
								num3 = xlsExternWorkbook.ExternNames.ᜂ(strName);
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
									continue;
								}
								break;
							}
							case 1:
								if (num3 != -1)
								{
									num2 = 2;
									continue;
								}
								num++;
								num2 = 5;
								continue;
							case 2:
								goto IL_B9;
							case 3:
								return -1;
							case 4:
								goto IL_BB;
							case 5:
								goto IL_BB;
							}
							break;
							IL_BB:
							num2 = 0;
						}
					}
				}
				IL_B9:
				iRefIndex = num3;
				return num;
			}
			}
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x000EFBD4 File Offset: 0x000EEBD4
		public XlsExternWorkbook GetBookByShortName(string strShortName)
		{
			int a_ = 5;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_4D:
				if (strShortName == null)
				{
					num = 2;
				}
				else
				{
					num = 3;
				}
				break;
			default:
				if (true)
				{
				}
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
					goto IL_86;
				case 2:
					goto IL_58;
				case 3:
					if (strShortName.Length == 0)
					{
						num = 1;
						continue;
					}
					goto IL_A6;
				}
				break;
			}
			goto IL_4D;
			IL_58:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠺䤼䴾ቀ⭂⩄㕆㵈Պⱌ≎㑐", a_));
			IL_86:
			throw new ArgumentException(RecordTableEnumerator.b("䠺䤼䴾ቀ⭂⩄㕆㵈Պⱌ≎㑐獒硔睖⩘⽚⽜㙞འѢ䕤Ѧࡨժͬnհ卲᝴ቶ奸Ṻၼཾ廒", a_));
			IL_A6:
			XlsExternWorkbook result;
			this.ᜅ.TryGetValue(strShortName, out result);
			return result;
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x000EFC98 File Offset: 0x000EEC98
		private new void ᜀ()
		{
			int a_ = 8;
			for (;;)
			{
				this.ᜃ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
				if (this.ᜃ != null)
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_4E;
				}
			}
			IL_4E:
			if (false)
			{
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("匽Ἷ⁁⭃⥅⍇", a_));
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x000EFD18 File Offset: 0x000EED18
		public int GetFirstInternalIndex()
		{
			int num;
			for (;;)
			{
				IL_20:
				num = 0;
				int count = base.List.Count;
				for (;;)
				{
					IL_2E:
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return num;
						case 1:
							return -1;
						case 2:
						{
							XlsExternWorkbook xlsExternWorkbook;
							if (xlsExternWorkbook.IsInternalReference)
							{
								num2 = 0;
								continue;
							}
							num++;
							num2 = 4;
							continue;
						}
						case 3:
							goto IL_77;
						case 4:
							if (true)
							{
							}
							goto IL_77;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2E;
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
								XlsExternWorkbook xlsExternWorkbook = base.List[num];
								num2 = 2;
								continue;
							}
							}
							break;
						}
						goto IL_20;
						IL_77:
						num2 = 5;
					}
				}
			}
			return num;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x000EFDDC File Offset: 0x000EEDDC
		protected override void OnInsertComplete(int index, XlsExternWorkbook value)
		{
			int a_ = 13;
			for (;;)
			{
				base.OnInsertComplete(index, value);
				value.Index = base.List.Count - 1;
				int num = 11;
				for (;;)
				{
					string shortName;
					switch (num)
					{
					case 0:
						goto IL_86;
					case 1:
						goto IL_137;
					case 2:
						num = 3;
						continue;
					case 3:
					{
						string url;
						if (url != RecordTableEnumerator.b("捂", a_))
						{
							num = 6;
							continue;
						}
						goto IL_1B4;
					}
					case 4:
						if (!this.ᜅ.ContainsKey(shortName))
						{
							num = 9;
							continue;
						}
						goto IL_1B4;
					case 5:
						goto IL_15B;
					case 6:
						IL_11D:
						num = 13;
						continue;
					case 7:
						if (!this.ᜃ.Loading)
						{
							num = 0;
							continue;
						}
						goto IL_15B;
					case 8:
					{
						string url;
						if (url != null)
						{
							num = 2;
							continue;
						}
						goto IL_1B4;
					}
					case 9:
						this.ᜅ.Add(shortName, value);
						num = 1;
						continue;
					case 10:
					{
						string url = value.URL;
						num = 8;
						continue;
					}
					case 11:
						if (!value.IsInternalReference)
						{
							num = 10;
							continue;
						}
						goto IL_1B4;
					case 12:
						num = 7;
						continue;
					case 13:
					{
						string url;
						if (this.ᜄ.ContainsKey(url))
						{
							num = 12;
							continue;
						}
						goto IL_86;
					}
					}
					break;
					IL_86:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_11D;
					default:
					{
						if (false)
						{
						}
						string url;
						this.ᜄ.Add(url, value);
						num = 5;
						continue;
					}
					}
					IL_15B:
					shortName = value.ShortName;
					num = 4;
				}
			}
			IL_137:
			IL_1B4:
			if (true)
			{
			}
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x000EFFA8 File Offset: 0x000EEFA8
		internal new Dictionary<int, int> ᜀ(XlsExternBookCollection A_0)
		{
			int a_ = 9;
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					int firstInternalIndex;
					int num2;
					int count;
					int value;
					Dictionary<int, int> dictionary;
					switch (num)
					{
					case 0:
						if (firstInternalIndex >= 0)
						{
							num = 10;
							continue;
						}
						goto IL_B1;
					case 1:
					{
						if (num2 >= count)
						{
							num = 13;
							continue;
						}
						XlsExternWorkbook xlsExternWorkbook = A_0[num2];
						value = -1;
						XlsExternWorkbook xlsExternWorkbook2 = this[xlsExternWorkbook.URL];
						num = 2;
						continue;
					}
					case 2:
					{
						XlsExternWorkbook xlsExternWorkbook;
						if (xlsExternWorkbook.IsInternalReference)
						{
							num = 11;
							continue;
						}
						goto IL_B1;
					}
					case 3:
						goto IL_F3;
					case 4:
					{
						XlsExternWorkbook xlsExternWorkbook = (XlsExternWorkbook)xlsExternWorkbook.Clone(this);
						value = this.Add(xlsExternWorkbook);
						num = 8;
						continue;
					}
					case 6:
					{
						XlsExternWorkbook xlsExternWorkbook2;
						if (xlsExternWorkbook2 == null)
						{
							num = 4;
							continue;
						}
						value = xlsExternWorkbook2.Index;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_CF;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					}
					case 7:
						goto IL_149;
					case 8:
						goto IL_F3;
					case 9:
						goto IL_F3;
					case 10:
						value = firstInternalIndex;
						num = 9;
						continue;
					case 11:
						num = 0;
						continue;
					case 12:
						goto IL_149;
					case 13:
						return dictionary;
					case 14:
						goto IL_79;
					}
					if (A_0 == null)
					{
						if (true)
						{
						}
						num = 14;
						continue;
					}
					goto IL_CF;
					IL_B1:
					num = 6;
					continue;
					IL_CF:
					dictionary = new Dictionary<int, int>();
					firstInternalIndex = this.GetFirstInternalIndex();
					num2 = 0;
					count = A_0.Count;
					num = 7;
					continue;
					IL_F3:
					dictionary.Add(num2, value);
					num2++;
					num = 12;
					continue;
					IL_149:
					num = 1;
				}
				IL_79:
				throw new ArgumentNullException(RecordTableEnumerator.b("䰾㑀⅂݄⡆♈⁊㹌", a_));
			}
			}
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x000F01A0 File Offset: 0x000EF1A0
		internal new XlsExternWorkbook ᜀ(string A_0, string A_1)
		{
			int a_ = 9;
			int num = 1;
			XlsExternWorkbook result;
			for (;;)
			{
				string text;
				string key;
				switch (num)
				{
				case 0:
					text = A_0;
					goto IL_1B5;
				case 2:
					goto IL_B0;
				case 3:
					if (A_1 != null)
					{
						num = 16;
						continue;
					}
					num = 0;
					continue;
				case 4:
					if (true)
					{
					}
					if (A_0.Length == 0)
					{
						num = 5;
						continue;
					}
					num = 3;
					continue;
				case 5:
					goto IL_187;
				case 6:
					goto IL_18C;
				case 7:
					text = A_1 + A_0;
					goto IL_1B5;
				case 8:
					result = this.ᜅ[A_0];
					num = 9;
					continue;
				case 9:
					goto IL_F8;
				case 10:
					if (A_0 != null)
					{
						num = 17;
						continue;
					}
					goto IL_18C;
				case 11:
					num = 4;
					continue;
				case 12:
					if (!this.ᜄ.ContainsKey(key))
					{
						num = 10;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_80;
					default:
						if (false)
						{
						}
						num = 15;
						continue;
					}
					break;
				case 13:
					goto IL_14B;
				case 14:
					goto IL_80;
				case 15:
					result = this.ᜄ[key];
					num = 2;
					continue;
				case 16:
					num = 7;
					continue;
				case 17:
					num = 14;
					continue;
				case 18:
					if (this.ᜅ.ContainsKey(A_0))
					{
						num = 8;
						continue;
					}
					goto IL_12F;
				}
				if (A_0 != null)
				{
					num = 11;
					continue;
				}
				goto IL_FD;
				IL_80:
				if (A_0.Length == 0)
				{
					num = 6;
					continue;
				}
				IL_12F:
				result = this[this.Add(A_1, A_0, null, null)];
				num = 13;
				continue;
				IL_18C:
				num = 18;
				continue;
				IL_1B5:
				key = text;
				result = null;
				num = 12;
			}
			IL_B0:
			IL_F8:
			return result;
			IL_FD:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䰾㕀ㅂ݄⡆♈⁊", a_));
			IL_14B:
			return result;
			IL_187:
			goto IL_FD;
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x000F03C4 File Offset: 0x000EF3C4
		internal new void ᜁ()
		{
			for (;;)
			{
				IL_18:
				int num = base.Count - 1;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_47:
					num2 = 2;
					break;
				default:
					if (false)
					{
					}
					num2 = 1;
					break;
				}
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_8C;
					case 1:
						goto IL_45;
					case 2:
					{
						if (num < 0)
						{
							num2 = 3;
							continue;
						}
						XlsExternWorkbook xlsExternWorkbook = this[num];
						xlsExternWorkbook.Dispose();
						num--;
						if (true)
						{
						}
						num2 = 0;
						continue;
					}
					case 3:
						goto IL_65;
					}
					goto IL_18;
				}
				IL_45:
				IL_8C:
				goto IL_47;
			}
			IL_65:
			base.Clear();
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x000F0468 File Offset: 0x000EF468
		internal new int ᜀ(string A_0, XlsWorkbook A_1, IXLSRange A_2)
		{
			switch (0)
			{
			default:
			{
				XlsExternWorksheet xlsExternWorksheet;
				int num4;
				int firstSheetIndex;
				for (;;)
				{
					if (true)
					{
					}
					XlsExternWorkbook xlsExternWorkbook = new XlsExternWorkbook(base.AppImplementation, this);
					xlsExternWorkbook.IsInternalReference = false;
					xlsExternWorkbook.IsAddInFunctions = false;
					int num = 12;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							num = 4;
							continue;
						case 1:
							num = 8;
							continue;
						case 2:
						{
							XlsExternWorksheet xlsExternWorksheet2;
							xlsExternWorksheet = xlsExternWorksheet2;
							num = 14;
							continue;
						}
						case 3:
							num2 = 65534;
							goto IL_1D2;
						case 4:
							num2 = xlsExternWorksheet.Index;
							goto IL_1D2;
						case 5:
						{
							int sheetNumber = xlsExternWorkbook.SheetNumber;
							num = 15;
							continue;
						}
						case 6:
						{
							string name;
							string name2;
							if (name == name2)
							{
								num = 2;
								continue;
							}
							goto IL_BE;
						}
						case 7:
						{
							int sheetNumber;
							if (sheetNumber != 0)
							{
								num = 1;
								continue;
							}
							num = 11;
							continue;
						}
						case 8:
							goto IL_1B7;
						case 9:
						{
							int num3;
							int count;
							if (num3 >= count)
							{
								num = 5;
								continue;
							}
							IWorksheets worksheets;
							IWorksheet worksheet = worksheets[num3];
							string name = worksheet.Name;
							XlsExternWorksheet xlsExternWorksheet2 = xlsExternWorkbook.ᜄ(name);
							num = 6;
							continue;
						}
						case 10:
							IL_1A9:
							goto IL_210;
						case 11:
							goto IL_209;
						case 12:
						{
							xlsExternWorkbook.URL = ((A_0 != null) ? Path.GetFullPath(A_0) : A_0);
							num4 = this.Add(xlsExternWorkbook);
							IWorksheets worksheets = A_2.Worksheet.Workbook.Worksheets;
							xlsExternWorksheet = null;
							string name2 = A_2.Worksheet.Name;
							int num3 = 0;
							int count = worksheets.Count;
							num = 10;
							continue;
						}
						case 13:
							goto IL_210;
						case 14:
							goto IL_BE;
						case 15:
						{
							int sheetNumber;
							if (sheetNumber != 0)
							{
								num = 0;
								continue;
							}
							num = 3;
							continue;
						}
						}
						break;
						IL_BE:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1A9;
						default:
						{
							if (false)
							{
							}
							int num3;
							num3++;
							num = 13;
							continue;
						}
						}
						IL_1D2:
						firstSheetIndex = num2;
						num = 7;
						continue;
						IL_210:
						num = 9;
					}
				}
				IL_1B7:
				int num5 = xlsExternWorksheet.Index;
				goto IL_236;
				IL_209:
				num5 = 65534;
				IL_236:
				int lastSheetIndex = num5;
				xlsExternWorksheet.ᜀ(A_2);
				this.ᜃ.AddSheetReference(num4, firstSheetIndex, lastSheetIndex);
				return num4;
			}
			}
		}

		// Token: 0x04001047 RID: 4167
		private int \u2593\u0081\u00A1\u0090;

		// Token: 0x04001048 RID: 4168
		private new const int ᜀ = 32746;

		// Token: 0x04001049 RID: 4169
		private float \u2593\u0089\u009A\u00A6;

		// Token: 0x0400104A RID: 4170
		private new const int ᜁ = 65534;

		// Token: 0x0400104B RID: 4171
		internal new const string ᜂ = " ";

		// Token: 0x0400104C RID: 4172
		private XlsWorkbook ᜃ;

		// Token: 0x0400104D RID: 4173
		private int \u25D9\u0089\u00AE\u00A5;

		// Token: 0x0400104E RID: 4174
		private bool[] \u25D9\u0086\u0092\u00A6;

		// Token: 0x0400104F RID: 4175
		private Dictionary<string, XlsExternWorkbook> ᜄ = new Dictionary<string, XlsExternWorkbook>();

		// Token: 0x04001050 RID: 4176
		private Dictionary<string, XlsExternWorkbook> ᜅ = new Dictionary<string, XlsExternWorkbook>();
	}
}
