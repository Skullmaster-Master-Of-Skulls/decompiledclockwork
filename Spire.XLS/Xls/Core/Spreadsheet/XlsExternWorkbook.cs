using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x020001E8 RID: 488
	public class XlsExternWorkbook : XlsObject, ICloneParent
	{
		// Token: 0x06001BD6 RID: 7126 RVA: 0x000F06C8 File Offset: 0x000EF6C8
		internal XlsExternWorkbook(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜂ();
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x000F06FC File Offset: 0x000EF6FC
		private void ᜂ()
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
			this.ᜁ();
			this.ᜂ = new sprᭆ(base.ReservedHandle, this);
			this.ᜃ = (sprᶋ)spr\u175E.ᜀ(TBIFFRecord.SupBook);
			this.ᜃ.ᜀ(new List<string>());
			this.ᜀ();
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x000F077C File Offset: 0x000EF77C
		internal void ᜄ()
		{
			int a_ = 12;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜃ.ᜇ().Add(RecordTableEnumerator.b("ᅁⱃ⍅ⵇ㹉絋", a_));
			XlsExternWorksheet xlsExternWorksheet = new XlsExternWorksheet(base.AppImplementation, this);
			xlsExternWorksheet.Index = 0;
			this.ᜀ.Add(0, xlsExternWorksheet);
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x000F0800 File Offset: 0x000EF800
		private void ᜁ()
		{
			int a_ = 8;
			this.ᜅ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
			if (this.ᜅ == null)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_44;
					}
				}
				IL_44:
				if (false)
				{
				}
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("樽⠿❁摃㙅⥇㡉⥋⁍⑏牑㭓㑕㉗㽙㽛⩝䁟šգࡥ䡧ѩͫᩭ偯ၱᅳ噵ṷᕹॻၽꎁ", a_));
			}
			this.ᜃ = (sprᶋ)spr\u1CD3.ᜀ(this.ᜃ);
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x000F0894 File Offset: 0x000EF894
		internal int ᜀ(BiffRecordRaw[] A_0, int A_1)
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
			throw new NotImplementedException();
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x000F08D4 File Offset: 0x000EF8D4
		internal void ᜀ(sprἛ A_0, IDecryptor A_1)
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				int num = 24;
				for (;;)
				{
					string text;
					int num2;
					TBIFFRecord tbiffrecord;
					BiffRecordRaw biffRecordRaw;
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						goto IL_275;
					case 2:
					{
						List<string> list;
						if (list != null)
						{
							num = 20;
							continue;
						}
						goto IL_255;
					}
					case 3:
						if (this.ᜃ.ᜉ())
						{
							num = 29;
							continue;
						}
						goto IL_408;
					case 4:
						this.ᜃ.ᜁ(this.ᜅ.DecodeName(text));
						num = 26;
						continue;
					case 5:
						if (num2 > 0)
						{
							num = 0;
							continue;
						}
						goto IL_255;
					case 6:
						if (this.ᜀ.Count == 0)
						{
							num = 19;
							continue;
						}
						goto IL_255;
					case 7:
					{
						int num3;
						int count;
						if (num3 >= count)
						{
							num = 21;
							continue;
						}
						List<string> list;
						this.ᜀ(new XlsExternWorksheet(base.AppImplementation, this)
						{
							Name = list[num3],
							Index = num3
						});
						num3++;
						num = 28;
						continue;
					}
					case 8:
						goto IL_408;
					case 9:
						goto IL_160;
					case 10:
						if (text != null)
						{
							num = 4;
							continue;
						}
						goto IL_223;
					case 11:
						goto IL_255;
					case 12:
						goto IL_1FE;
					case 13:
						num = 10;
						continue;
					case 14:
					{
						if (tbiffrecord != TBIFFRecord.XCT)
						{
							num = 1;
							continue;
						}
						XlsExternWorksheet xlsExternWorksheet = new XlsExternWorksheet(base.AppImplementation, this);
						xlsExternWorksheet.ᜀ(A_0, A_1);
						this.ᜀ(xlsExternWorksheet);
						tbiffrecord = A_0.ᜉ();
						num = 17;
						continue;
					}
					case 15:
						if (!this.ᜃ.ᜋ())
						{
							num = 13;
							continue;
						}
						goto IL_223;
					case 16:
					{
						if (tbiffrecord != TBIFFRecord.ExternName)
						{
							num = 22;
							continue;
						}
						biffRecordRaw = A_0.ᜀ(A_1);
						spr\u2141 spr_u = (spr\u2141)biffRecordRaw;
						this.ᜂ.ᜀ(spr_u);
						num = 25;
						continue;
					}
					case 17:
						goto IL_255;
					case 18:
						num = 3;
						continue;
					case 19:
						goto IL_133;
					case 20:
					{
						int num3 = 0;
						List<string> list;
						int count = list.Count;
						num = 9;
						continue;
					}
					case 21:
						num = 11;
						continue;
					case 22:
					{
						List<string> list = this.ᜃ.ᜇ();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_133;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						break;
					}
					case 23:
						goto IL_B6;
					case 25:
					{
						spr\u2141 spr_u;
						if (spr_u.ᜆ() != 0)
						{
							num = 18;
							continue;
						}
						goto IL_378;
					}
					case 26:
						goto IL_223;
					case 27:
						goto IL_1FE;
					case 28:
						goto IL_160;
					case 29:
						goto IL_378;
					}
					if (A_0 == null)
					{
						num = 23;
						continue;
					}
					this.ᜀ.Clear();
					this.ᜂ.Clear();
					biffRecordRaw = A_0.ᜀ(A_1);
					biffRecordRaw.CheckTypeCode(TBIFFRecord.SupBook);
					this.ᜃ = (sprᶋ)biffRecordRaw;
					text = this.ᜃ.ᜃ();
					num = 15;
					continue;
					IL_133:
					num = 5;
					continue;
					IL_160:
					num = 7;
					continue;
					IL_1FE:
					num = 16;
					continue;
					IL_223:
					num2 = (int)this.ᜃ.ᜆ();
					tbiffrecord = A_0.ᜉ();
					int num4 = 0;
					num = 27;
					continue;
					IL_255:
					num = 14;
					continue;
					IL_378:
					this.ᜅ.InnerAddInFunctions.Add(this.ᜄ, num4);
					num = 8;
					continue;
					IL_408:
					num4++;
					tbiffrecord = A_0.ᜉ();
					num = 12;
				}
				IL_B6:
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
				IL_275:
				this.ᜀ();
				return;
			}
			}
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x000F0D10 File Offset: 0x000EFD10
		private void ᜀ(XlsExternWorksheet A_0)
		{
			int a_ = 19;
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
						goto IL_5B;
					case 2:
						return;
					case 3:
					{
						int index;
						string key = this.ᜃ.ᜇ()[index];
						this.ᜁ[key] = A_0;
						num = 2;
						continue;
					}
					case 4:
					{
						int index;
						int count;
						if (index < count)
						{
							num = 3;
							continue;
						}
						return;
					}
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
						int index = A_0.Index;
						this.ᜀ[index] = A_0;
						List<string> list = this.ᜃ.ᜇ();
						int count = list.Count;
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
							break;
						}
					}
				}
				IL_5B:
				throw new ArgumentNullException(RecordTableEnumerator.b("㩈⍊⡌⩎═", a_));
			}
			}
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x000F0E20 File Offset: 0x000EFE20
		internal void ᜀ(RecordArrayList A_0)
		{
			int a_ = 14;
			switch (0)
			{
			default:
				for (;;)
				{
					IL_17:
					int num = 5;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17;
						default:
						{
							if (false)
							{
							}
							sprᶋ sprᶋ;
							switch (num)
							{
							case 0:
								goto IL_A7;
							case 1:
							{
								IList<XlsExternWorksheet> values = this.ᜀ.Values;
								IEnumerator<XlsExternWorksheet> enumerator = values.GetEnumerator();
								num = 6;
								continue;
							}
							case 2:
								if (sprᶋ.ᜃ() != null)
								{
									num = 8;
									continue;
								}
								goto IL_A7;
							case 3:
								goto IL_79;
							case 4:
								if (!this.ᜃ.ᜋ())
								{
									num = 9;
									continue;
								}
								goto IL_A7;
							case 6:
								try
								{
									num = 4;
									for (;;)
									{
										switch (num)
										{
										case 0:
										{
											IEnumerator<XlsExternWorksheet> enumerator;
											if (!enumerator.MoveNext())
											{
												num = 2;
												continue;
											}
											XlsExternWorksheet xlsExternWorksheet = enumerator.Current;
											xlsExternWorksheet.ᜁ(A_0);
											num = 3;
											continue;
										}
										case 1:
											goto IL_18B;
										case 2:
											num = 1;
											continue;
										}
										IL_166:
										num = 0;
										continue;
										goto IL_166;
									}
									IL_18B:
									return;
								}
								finally
								{
									num = 2;
									for (;;)
									{
										IEnumerator<XlsExternWorksheet> enumerator;
										switch (num)
										{
										case 0:
											goto IL_1CB;
										case 1:
											enumerator.Dispose();
											num = 0;
											continue;
										}
										if (enumerator == null)
										{
											break;
										}
										num = 1;
									}
									IL_1CB:;
								}
								goto IL_1CE;
							case 7:
								if (!this.IsInternalReference)
								{
									num = 1;
									continue;
								}
								return;
							case 8:
								goto IL_1CE;
							case 9:
								num = 2;
								continue;
							}
							if (A_0 == null)
							{
								num = 3;
								break;
							}
							sprᶋ = this.ᜃ;
							num = 4;
							break;
							IL_A7:
							A_0.ᜀ(sprᶋ);
							this.ᜂ.ᜀ(A_0);
							num = 7;
							break;
							IL_1CE:
							sprᶋ = (sprᶋ)this.ᜃ.Clone();
							sprᶋ.ᜁ(this.ᜅ.EncodeName(this.ᜃ.ᜃ()));
							num = 0;
							break;
						}
						}
					}
				}
				IL_79:
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⭇╉㹋⩍⍏", a_));
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06001BDE RID: 7134 RVA: 0x000F1078 File Offset: 0x000F0078
		internal sprᭆ ExternNames
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
				return this.ᜂ;
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x000F10BC File Offset: 0x000F00BC
		// (set) Token: 0x06001BE0 RID: 7136 RVA: 0x000F1104 File Offset: 0x000F0104
		public bool IsInternalReference
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
				return this.ᜃ.ᜋ();
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
				this.ᜃ.ᜀ(value);
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x000F114C File Offset: 0x000F014C
		public bool IsOleLink
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 3;
						continue;
					case 1:
						if (true)
						{
						}
						break;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							goto IL_50;
						}
						break;
					case 3:
						if (this.ᜂ.Count == 1)
						{
							num = 2;
							continue;
						}
						return false;
					}
					if (this.ᜂ == null)
					{
						return false;
					}
					num = 0;
				}
				IL_50:
				if (false)
				{
				}
				return this.ᜂ.ᜀ(0).ᜄ().ᜃ();
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x000F11F4 File Offset: 0x000F01F4
		// (set) Token: 0x06001BE3 RID: 7139 RVA: 0x000F123C File Offset: 0x000F023C
		public int SheetNumber
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
				return (int)this.ᜃ.ᜆ();
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
				this.ᜃ.ᜀ((ushort)value);
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x000F1284 File Offset: 0x000F0284
		// (set) Token: 0x06001BE5 RID: 7141 RVA: 0x000F12CC File Offset: 0x000F02CC
		public string URL
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
				return this.ᜃ.ᜃ();
			}
			set
			{
				for (;;)
				{
					this.ᜃ.ᜁ(value);
					this.ᜀ();
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.ᜀ.Clear();
							this.ᜁ.Clear();
							this.ᜃ.ᜀ(null);
							num = 1;
							continue;
						case 1:
							return;
						case 2:
							if (value != null)
							{
								return;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
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

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x000F1374 File Offset: 0x000F0374
		// (set) Token: 0x06001BE7 RID: 7143 RVA: 0x000F13B8 File Offset: 0x000F03B8
		public int Index
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
				return this.ᜄ;
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
				this.ᜄ = value;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x000F13FC File Offset: 0x000F03FC
		public XlsWorkbook Workbook
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
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x000F1440 File Offset: 0x000F0440
		public string ShortName
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
				return this.ᜆ;
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x000F1484 File Offset: 0x000F0484
		// (set) Token: 0x06001BEB RID: 7147 RVA: 0x000F14CC File Offset: 0x000F04CC
		public bool IsAddInFunctions
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
				return this.ᜃ.ᜉ();
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
				this.ᜃ.ᜁ(value);
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x000F1514 File Offset: 0x000F0514
		public SortedList<int, XlsExternWorksheet> Worksheets
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
				return this.ᜀ;
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x000F1558 File Offset: 0x000F0558
		// (set) Token: 0x06001BEE RID: 7150 RVA: 0x000F159C File Offset: 0x000F059C
		internal string ProgramId
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
				return this.ᜇ;
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
				this.ᜇ = value;
			}
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x000F15E0 File Offset: 0x000F05E0
		public int IndexOf(string strSheetName)
		{
			int num = 4;
			XlsExternWorksheet xlsExternWorksheet;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!this.ᜁ.TryGetValue(strSheetName, out xlsExternWorksheet))
					{
						num = 2;
						continue;
					}
					goto IL_9E;
				case 1:
					goto IL_9A;
				case 2:
					return -1;
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
						num = 5;
						continue;
					}
					break;
				case 5:
					if (strSheetName.Length == 0)
					{
						num = 1;
						continue;
					}
					num = 0;
					continue;
				}
				if (strSheetName == null)
				{
					return -1;
				}
				num = 3;
			}
			return -1;
			IL_9A:
			return -1;
			IL_9E:
			if (true)
			{
			}
			return xlsExternWorksheet.Index;
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x000F169C File Offset: 0x000F069C
		public void saveAsHtml(string FileName)
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
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x000F16D8 File Offset: 0x000F06D8
		public int GetNewIndex(int iNameIndex)
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
			return this.ᜂ.ᜁ(iNameIndex);
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x000F1720 File Offset: 0x000F0720
		public object Clone(object parent)
		{
			switch (0)
			{
			default:
			{
				XlsExternWorkbook xlsExternWorkbook;
				for (;;)
				{
					IL_27:
					IList<int> keys;
					IList<XlsExternWorksheet> values;
					int num;
					int count;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_105:
						goto IL_9C;
					default:
						if (false)
						{
						}
						xlsExternWorkbook = (XlsExternWorkbook)base.MemberwiseClone();
						xlsExternWorkbook.SetParent(parent);
						xlsExternWorkbook.ᜁ();
						xlsExternWorkbook.ᜀ = new SortedList<int, XlsExternWorksheet>();
						keys = this.ᜀ.Keys;
						values = this.ᜀ.Values;
						num = 0;
						count = this.ᜀ.Count;
						num2 = 2;
						break;
					}
					for (;;)
					{
						IL_10:
						switch (num2)
						{
						case 0:
						{
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							int num3 = keys[num];
							XlsExternWorksheet xlsExternWorksheet = values[num];
							xlsExternWorksheet = (XlsExternWorksheet)xlsExternWorksheet.Clone(xlsExternWorkbook);
							xlsExternWorkbook.ᜀ(xlsExternWorksheet);
							num++;
							num2 = 3;
							continue;
						}
						case 1:
							goto IL_B9;
						case 2:
							goto IL_9A;
						case 3:
							goto IL_105;
						}
						goto IL_27;
					}
					IL_9A:
					IL_9C:
					num2 = 0;
					goto IL_10;
				}
				IL_B9:
				if (true)
				{
				}
				xlsExternWorkbook.ᜂ = (sprᭆ)this.ᜂ.Clone(this);
				return xlsExternWorkbook;
			}
			}
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x000F184C File Offset: 0x000F084C
		public string GetSheetName(int index)
		{
			int a_ = 10;
			while (index != 65535)
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
					return this.ᜃ.ᜇ()[index];
				}
			}
			return RecordTableEnumerator.b("挿၁ŃE", a_);
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x000F18BC File Offset: 0x000F08BC
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
			this.ᜆ = ((this.ᜃ.ᜃ() != null) ? Path.GetFileName(this.ᜃ.ᜃ()) : null);
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x000F1924 File Offset: 0x000F0924
		private static string ᜁ(string A_0)
		{
			int num = 4;
			int num2;
			int length;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
				{
					int num3;
					num2 = num3 + 1;
					goto IL_B2;
				}
				case 2:
					if (A_0.Length != 0)
					{
						int num3 = A_0.LastIndexOf('\\');
						length = A_0.Length;
						num2 = 0;
						num = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B2;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 3:
				{
					int num3;
					if (num3 > 0)
					{
						num = 1;
						continue;
					}
					goto IL_BF;
				}
				case 4:
					if (true)
					{
					}
					break;
				case 5:
					goto IL_BD;
				case 6:
					goto IL_AC;
				}
				if (A_0 != null)
				{
					num = 0;
					continue;
				}
				break;
				IL_B2:
				num = 5;
			}
			return A_0;
			IL_AC:
			return A_0;
			IL_BD:
			IL_BF:
			return A_0.Substring(num2, length - num2);
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x000F19FC File Offset: 0x000F09FC
		private static string ᜀ(string A_0)
		{
			switch (0)
			{
			default:
			{
				int num3;
				int num5;
				for (;;)
				{
					IL_0E:
					int num = 8;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_101;
						case 1:
							goto IL_59;
						case 2:
						{
							int num2;
							if (num2 > 0)
							{
								num = 9;
								continue;
							}
							goto IL_59;
						}
						case 3:
							if (true)
							{
							}
							num = 7;
							continue;
						case 4:
							goto IL_F1;
						case 5:
						{
							int num4;
							num3 = num4;
							num = 0;
							continue;
						}
						case 6:
						{
							int num4;
							if (num4 <= num5)
							{
								goto IL_124;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_0E;
							default:
								if (false)
								{
								}
								num = 5;
								continue;
							}
							break;
						}
						case 7:
						{
							if (A_0.Length == 0)
							{
								num = 4;
								continue;
							}
							int num2 = A_0.LastIndexOf('\\');
							int num4 = A_0.LastIndexOf('.');
							num5 = 0;
							num3 = A_0.Length;
							num = 2;
							continue;
						}
						case 9:
						{
							int num2;
							num5 = num2 + 1;
							num = 1;
							continue;
						}
						}
						if (A_0 != null)
						{
							num = 3;
							continue;
						}
						return A_0;
						IL_59:
						num = 6;
					}
				}
				IL_F1:
				return A_0;
				IL_101:
				IL_124:
				return A_0.Substring(num5, num3 - num5);
			}
			}
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x000F1B38 File Offset: 0x000F0B38
		internal void ᜀ(List<string> A_0)
		{
			int num = 1;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 0:
					goto IL_B5;
				case 2:
					return;
				case 3:
					num2 = A_0.Count;
					goto IL_B7;
				case 4:
					num2 = 0;
					goto IL_B7;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B5;
					default:
					{
						if (false)
						{
						}
						if (num3 == 0)
						{
							num = 9;
							continue;
						}
						int num4 = 0;
						if (true)
						{
						}
						num = 6;
						continue;
					}
					}
					break;
				case 6:
					goto IL_74;
				case 7:
				{
					int num4;
					if (num4 >= num3)
					{
						num = 2;
						continue;
					}
					this.ᜄ(A_0[num4]);
					num4++;
					num = 0;
					continue;
				}
				case 8:
					num = 4;
					continue;
				case 9:
					return;
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				num = 3;
				continue;
				IL_74:
				num = 7;
				continue;
				IL_B5:
				goto IL_74;
				IL_B7:
				num3 = num2;
				num = 5;
			}
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x000F1C3C File Offset: 0x000F0C3C
		internal void ᜀ(string[] A_0)
		{
			int num = 6;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 0:
					num2 = 0;
					goto IL_D2;
				case 1:
					goto IL_8D;
				case 2:
					goto IL_8D;
				case 3:
					return;
				case 4:
				{
					if (num3 == 0)
					{
						num = 7;
						continue;
					}
					int num4 = 0;
					num = 1;
					continue;
				}
				case 5:
				{
					int num4;
					if (num4 >= num3)
					{
						num = 3;
						continue;
					}
					this.ᜄ(A_0[num4]);
					num4++;
					num = 2;
					continue;
				}
				case 7:
					return;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 9:
					if (true)
					{
					}
					num2 = A_0.Length;
					goto IL_D2;
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				num = 9;
				continue;
				IL_8D:
				num = 5;
				continue;
				IL_D2:
				num3 = num2;
				num = 4;
			}
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x000F1D38 File Offset: 0x000F0D38
		internal XlsExternWorksheet ᜄ(string A_0)
		{
			int a_ = 5;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_90;
				case 1:
					if (A_0.Length == 0)
					{
						num = 0;
						continue;
					}
					goto IL_92;
				case 2:
					num = 1;
					continue;
				}
				IL_29:
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
					if (false)
					{
					}
					if (true)
					{
					}
					num = 2;
					continue;
				}
				goto IL_29;
			}
			IL_64:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䠺唼娾⑀㝂ୄ♆⑈⹊", a_));
			IL_90:
			goto IL_64;
			IL_92:
			XlsExternWorksheet xlsExternWorksheet = new XlsExternWorksheet(base.AppImplementation, this);
			int count = this.ᜀ.Count;
			xlsExternWorksheet.Index = count;
			xlsExternWorksheet.Name = A_0;
			this.ᜀ.Add(count, xlsExternWorksheet);
			this.ᜁ.Add(A_0, xlsExternWorksheet);
			this.ᜃ.ᜇ().Add(A_0);
			return xlsExternWorksheet;
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x000F1E2C File Offset: 0x000F0E2C
		public void AddNames(string[] names)
		{
			int num = 4;
			for (;;)
			{
				int num2;
				int num3;
				int num4;
				switch (num)
				{
				case 0:
					return;
				case 1:
					num2 = 0;
					goto IL_B9;
				case 2:
					num = 1;
					continue;
				case 3:
					if (num3 < num4)
					{
						this.AddName(names[num3]);
						num3++;
						num = 7;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BC;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 5:
					goto IL_66;
				case 6:
					num2 = names.Length;
					goto IL_B9;
				case 7:
					goto IL_66;
				}
				if (true)
				{
				}
				if (names == null)
				{
					num = 2;
					continue;
				}
				num = 6;
				continue;
				IL_66:
				num = 3;
				continue;
				IL_BC:
				num = 5;
				continue;
				IL_B9:
				num4 = num2;
				num3 = 0;
				goto IL_BC;
			}
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x000F1F04 File Offset: 0x000F0F04
		public void AddName(string name)
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
			this.ᜂ.ᜃ(name);
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x000F1F4C File Offset: 0x000F0F4C
		internal int ᜂ(string A_0)
		{
			int a_ = 3;
			if (true)
			{
			}
			int num = 1;
			XlsExternWorksheet xlsExternWorksheet;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_83;
				case 2:
					if (!this.ᜁ.TryGetValue(A_0, out xlsExternWorksheet))
					{
						num = 3;
						continue;
					}
					goto IL_E8;
				case 3:
					xlsExternWorksheet = this.ᜄ(A_0);
					goto IL_DB;
				case 4:
					num = 6;
					continue;
				case 5:
					goto IL_E6;
				case 6:
					if (A_0.Length == 0)
					{
						num = 0;
						continue;
					}
					xlsExternWorksheet = this.ᜁ[A_0];
					num = 2;
					continue;
				}
				if (A_0 != null)
				{
					num = 4;
					continue;
				}
				goto IL_83;
				IL_DB:
				num = 5;
				continue;
				IL_83:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_DB;
				default:
					goto IL_99;
				}
			}
			IL_99:
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䨸区堼娾㕀ൂ⑄⩆ⱈ", a_));
			IL_E6:
			IL_E8:
			return xlsExternWorksheet.Index;
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x000F2048 File Offset: 0x000F1048
		protected override void OnDispose()
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_14F;
				case 1:
					try
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 2;
								continue;
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_B5;
								default:
									if (false)
									{
									}
									break;
								}
								break;
							case 2:
								goto IL_E6;
							case 3:
								goto IL_B5;
							}
							IL_AD:
							num = 3;
							continue;
							goto IL_AD;
							IL_B5:
							IEnumerator<XlsExternWorksheet> enumerator;
							if (!enumerator.MoveNext())
							{
								num = 0;
							}
							else
							{
								XlsExternWorksheet xlsExternWorksheet = enumerator.Current;
								xlsExternWorksheet.Dispose();
								num = 4;
							}
						}
						IL_E6:
						goto IL_44;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							IEnumerator<XlsExternWorksheet> enumerator;
							switch (num)
							{
							case 0:
								goto IL_122;
							case 2:
								enumerator.Dispose();
								num = 0;
								continue;
							}
							if (enumerator == null)
							{
								break;
							}
							num = 2;
						}
						IL_122:;
					}
					goto IL_125;
					IL_44:
					this.ᜀ.Clear();
					this.ᜀ = null;
					if (true)
					{
					}
					num = 0;
					continue;
				case 2:
					if (this.ᜀ != null)
					{
						num = 4;
						continue;
					}
					goto IL_14F;
				case 3:
					goto IL_125;
				case 4:
				{
					IEnumerator<XlsExternWorksheet> enumerator = this.ᜀ.Values.GetEnumerator();
					num = 1;
					continue;
				}
				case 6:
					return;
				}
				if (!this.m_bIsDisposed)
				{
					num = 3;
					continue;
				}
				break;
				IL_125:
				num = 2;
				continue;
				IL_14F:
				base.OnDispose();
				num = 6;
			}
		}

		// Token: 0x04001051 RID: 4177
		private SortedList<int, XlsExternWorksheet> ᜀ = new SortedList<int, XlsExternWorksheet>();

		// Token: 0x04001052 RID: 4178
		private Dictionary<string, XlsExternWorksheet> ᜁ = new Dictionary<string, XlsExternWorksheet>();

		// Token: 0x04001053 RID: 4179
		private sprᭆ ᜂ;

		// Token: 0x04001054 RID: 4180
		private sprᶋ ᜃ;

		// Token: 0x04001055 RID: 4181
		private string \u25D9\u0090\u0091\u009C;

		// Token: 0x04001056 RID: 4182
		private int ᜄ;

		// Token: 0x04001057 RID: 4183
		private XlsWorkbook ᜅ;

		// Token: 0x04001058 RID: 4184
		private string ᜆ;

		// Token: 0x04001059 RID: 4185
		private string ᜇ;
	}
}
