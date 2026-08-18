using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Security;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x02000023 RID: 35
	public class XlsVPageBreaksCollection : CollectionExtended<IVPageBreak>, IVPageBreaks, IRecordStorage
	{
		// Token: 0x17000101 RID: 257
		protected internal IVPageBreak this[IXLSRange location]
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
				return this.GetPageBreak(location.Column);
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000163FC File Offset: 0x000153FC
		protected internal IVPageBreak Add(IXLSRange range)
		{
			int a_ = 3;
			while (range == null)
			{
				if (true)
				{
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
				throw new ArgumentNullException(RecordTableEnumerator.b("䬸娺匼堾⑀", a_));
			}
			IVPageBreak ivpageBreak = new VPageBreak((spr\u2158)base.ReservedHandle, this, range);
			base.Add(ivpageBreak);
			return ivpageBreak;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00016474 File Offset: 0x00015474
		protected internal IVPageBreak Remove(IXLSRange location)
		{
			IVPageBreak result;
			for (;;)
			{
				IL_14:
				int num = this.ᜀ(location);
				result = null;
				for (;;)
				{
					IL_1E:
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return result;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1E;
							default:
								if (false)
								{
								}
								result = base.List[num];
								base.RemoveAt(num);
								if (true)
								{
								}
								num2 = 0;
								continue;
							}
							break;
						case 2:
							if (num >= 0)
							{
								num2 = 1;
								continue;
							}
							return result;
						}
						goto IL_14;
					}
				}
			}
			return result;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00016504 File Offset: 0x00015504
		protected internal IVPageBreak GetPageBreak(int iColumn)
		{
			switch (0)
			{
			default:
			{
				IVPageBreak result;
				for (;;)
				{
					int num;
					int num2;
					int count;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_65:
						num++;
						num2 = 4;
						break;
					default:
						if (false)
						{
						}
						result = null;
						num = 0;
						count = base.Count;
						num2 = 1;
						break;
					}
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							XlsVPageBreak xlsVPageBreak = base[num] as XlsVPageBreak;
							num2 = 3;
							continue;
						}
						case 1:
							goto IL_C0;
						case 2:
							return result;
						case 3:
						{
							XlsVPageBreak xlsVPageBreak;
							if (xlsVPageBreak.Location.Column == iColumn)
							{
								num2 = 6;
								continue;
							}
							goto IL_65;
						}
						case 4:
							goto IL_C0;
						case 5:
							return result;
						case 6:
						{
							XlsVPageBreak xlsVPageBreak;
							result = xlsVPageBreak;
							num2 = 5;
							continue;
						}
						}
						break;
						IL_C0:
						if (true)
						{
						}
						num2 = 0;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000165F8 File Offset: 0x000155F8
		private new int ᜀ(IXLSRange A_0)
		{
			switch (0)
			{
			default:
			{
				int result;
				for (;;)
				{
					for (;;)
					{
						result = -1;
						int row = A_0.Row;
						int column = A_0.Column;
						int num = 0;
						int count = base.Count;
						int num2 = 7;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								IXLSRange location;
								if (location.Row != row)
								{
									goto IL_6D;
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
									num2 = 3;
									continue;
								}
								break;
							}
							case 1:
								goto IL_EB;
							case 2:
								return result;
							case 3:
								if (true)
								{
								}
								num2 = 8;
								continue;
							case 4:
								result = num;
								num2 = 2;
								continue;
							case 5:
								return result;
							case 6:
							{
								if (num >= count)
								{
									num2 = 5;
									continue;
								}
								XlsVPageBreak xlsVPageBreak = base[num] as XlsVPageBreak;
								IXLSRange location = xlsVPageBreak.Location;
								num2 = 0;
								continue;
							}
							case 7:
								goto IL_EB;
							case 8:
							{
								IXLSRange location;
								if (location.Column == column)
								{
									num2 = 4;
									continue;
								}
								goto IL_6D;
							}
							}
							break;
							IL_6D:
							num++;
							num2 = 1;
							continue;
							IL_EB:
							num2 = 6;
						}
					}
				}
				return result;
			}
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0001673C File Offset: 0x0001573C
		public int ManualBreakCount
		{
			get
			{
				int num = 0;
				IEnumerator<IVPageBreak> enumerator = base.List.GetEnumerator();
				try
				{
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 1:
						{
							if (!enumerator.MoveNext())
							{
								num2 = 5;
								continue;
							}
							XlsVPageBreak xlsVPageBreak = (XlsVPageBreak)enumerator.Current;
							if (true)
							{
							}
							num2 = 6;
							continue;
						}
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_91;
							default:
								if (false)
								{
								}
								break;
							}
							break;
						case 3:
							goto IL_C6;
						case 4:
							goto IL_91;
						case 5:
							num2 = 3;
							continue;
						case 6:
						{
							XlsVPageBreak xlsVPageBreak;
							if (xlsVPageBreak.Type == PageBreakType.Manual)
							{
								num2 = 4;
								continue;
							}
							break;
						}
						}
						IL_77:
						num2 = 1;
						continue;
						goto IL_77;
						IL_91:
						num++;
						num2 = 2;
					}
					IL_C6:;
				}
				finally
				{
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 1:
							enumerator.Dispose();
							num2 = 2;
							continue;
						case 2:
							goto IL_FF;
						}
						if (enumerator == null)
						{
							break;
						}
						num2 = 1;
					}
					IL_FF:;
				}
				return num;
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0001685C File Offset: 0x0001585C
		internal XlsVPageBreaksCollection(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜁ();
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00016878 File Offset: 0x00015878
		private new void ᜁ()
		{
			int a_ = 7;
			for (;;)
			{
				this.ᜀ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
				if (this.ᜀ != null)
				{
					return;
				}
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
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䴼帾㍀♂⭄㍆", a_));
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000168F8 File Offset: 0x000158F8
		internal new void ᜀ(spr\u2583 A_0)
		{
			int a_ = 16;
			switch (0)
			{
			default:
			{
				int num = 0;
				for (;;)
				{
					int num2;
					int num3;
					spr\u2583.ᜀ[] array;
					switch (num)
					{
					case 1:
						goto IL_BF;
					case 2:
						goto IL_BF;
					case 3:
						goto IL_55;
					case 4:
						return;
					case 5:
						if (num2 < num3)
						{
							spr\u2583.ᜀ a_2 = array[num2];
							XlsVPageBreak item = new VPageBreak((spr\u2158)base.ReservedHandle, this, a_2);
							base.Add(item);
							num2++;
							num = 2;
							continue;
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
							num = 4;
							continue;
						}
						break;
					}
					if (A_0 == null)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					array = A_0.ᜀ();
					num2 = 0;
					num3 = array.Length;
					num = 1;
					continue;
					IL_BF:
					num = 5;
				}
				IL_55:
				throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⥉⍋㱍㑏", a_));
			}
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00016A00 File Offset: 0x00015A00
		internal new void ᜀ(RecordArrayList A_0)
		{
			int a_ = 8;
			int num = 2;
			for (;;)
			{
				spr\u2583 spr_u;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_79;
					default:
						goto IL_4E;
					}
					break;
				case 1:
					if (spr_u != null)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					return;
				case 3:
					A_0.ᜀ(spr_u);
					num = 4;
					continue;
				case 4:
					return;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				spr_u = this.ᜀ();
				IL_79:
				num = 1;
			}
			IL_4E:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⅁⭃㑅ⱇ㥉", a_));
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00016ABC File Offset: 0x00015ABC
		private new spr\u2583 ᜀ()
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				spr\u2583.ᜀ[] array;
				for (;;)
				{
					int count = base.Count;
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_E1;
						case 1:
							goto IL_C5;
						case 2:
						{
							int num2;
							if (num2 >= count)
							{
								num = 0;
								continue;
							}
							List<IVPageBreak> innerList;
							XlsVPageBreak xlsVPageBreak = innerList[num2] as XlsVPageBreak;
							array[num2] = xlsVPageBreak.VPageBreak;
							num2++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_C3;
							default:
								if (false)
								{
								}
								num = 4;
								continue;
							}
							break;
						}
						case 3:
							goto IL_53;
						case 4:
							goto IL_C5;
						case 5:
						{
							if (count == 0)
							{
								num = 3;
								continue;
							}
							array = new spr\u2583.ᜀ[count];
							List<IVPageBreak> innerList = base.InnerList;
							int num2 = 0;
							num = 1;
							continue;
						}
						}
						break;
						IL_C5:
						num = 2;
					}
				}
				IL_53:
				IL_C3:
				return null;
				IL_E1:
				spr\u2583 spr_u = (spr\u2583)spr\u175E.ᜀ(TBIFFRecord.VerticalPageBreaks);
				spr_u.ᜀ(array);
				return spr_u;
			}
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00016BC4 File Offset: 0x00015BC4
		public override object Clone(object parent)
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
			XlsVPageBreaksCollection xlsVPageBreaksCollection = (XlsVPageBreaksCollection)base.Clone(parent);
			xlsVPageBreaksCollection.ᜁ();
			return xlsVPageBreaksCollection;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00016C14 File Offset: 0x00015C14
		internal new void ᜀ(XlsVPageBreak A_0)
		{
			int a_ = 6;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_53;
					default:
						if (false)
						{
						}
						if (this.ᜀ(A_0.Location) < 0)
						{
							num = 3;
							continue;
						}
						return;
					}
					break;
				case 3:
					base.Add(A_0);
					num = 1;
					continue;
				case 4:
					goto IL_40;
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					num = 4;
					continue;
				}
				IL_53:
				num = 2;
			}
			IL_40:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰻弽✿❁ك㑅ⵇ⭉❋", a_));
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00016CD0 File Offset: 0x00015CD0
		internal new void ᜂ()
		{
			switch (0)
			{
			default:
			{
				List<XlsVPageBreak> list = new List<XlsVPageBreak>();
				IEnumerator<IVPageBreak> enumerator = base.List.GetEnumerator();
				try
				{
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_254:
						goto IL_12A;
					default:
						if (false)
						{
						}
						num = 1;
						break;
					}
					for (;;)
					{
						IL_EA:
						switch (num)
						{
						case 0:
						{
							spr\u2583.ᜀ ᜀ;
							XlsWorkbook xlsWorkbook;
							ᜀ.ᜁ((uint)((ushort)(xlsWorkbook.MaxRowCount - 1)));
							num = 9;
							continue;
						}
						case 2:
							goto IL_265;
						case 3:
						{
							spr\u2583.ᜀ ᜀ;
							XlsWorkbook xlsWorkbook;
							if ((ulong)ᜀ.ᜃ() > (ulong)((long)(xlsWorkbook.MaxRowCount - 1)))
							{
								num = 5;
								continue;
							}
							goto IL_12A;
						}
						case 4:
							num = 2;
							continue;
						case 5:
						{
							spr\u2583.ᜀ ᜀ;
							XlsWorkbook xlsWorkbook;
							ᜀ.ᜀ((uint)((ushort)(xlsWorkbook.MaxRowCount - 1)));
							num = 12;
							continue;
						}
						case 6:
						{
							spr\u2583.ᜀ ᜀ;
							XlsWorkbook xlsWorkbook;
							if ((ulong)ᜀ.ᜀ() > (ulong)((long)(xlsWorkbook.MaxRowCount - 1)))
							{
								num = 0;
								continue;
							}
							break;
						}
						case 7:
						{
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							XlsVPageBreak xlsVPageBreak = (XlsVPageBreak)enumerator.Current;
							XlsWorksheet xlsWorksheet = ((XlsPageSetup)base.Parent).Worksheet;
							XlsWorkbook xlsWorkbook = (XlsWorkbook)xlsWorksheet.Workbook;
							spr\u2583.ᜀ ᜀ = xlsVPageBreak.VPageBreak;
							num = 8;
							continue;
						}
						case 8:
						{
							spr\u2583.ᜀ ᜀ;
							XlsWorkbook xlsWorkbook;
							if ((int)ᜀ.ᜁ() > xlsWorkbook.MaxColumnCount)
							{
								num = 11;
								continue;
							}
							num = 3;
							continue;
						}
						case 11:
						{
							XlsVPageBreak xlsVPageBreak;
							list.Add(xlsVPageBreak);
							num = 10;
							continue;
						}
						case 12:
							goto IL_254;
						}
						IL_163:
						num = 7;
						continue;
						goto IL_163;
					}
					IL_265:
					goto IL_AC;
					IL_12A:
					num = 6;
					goto IL_EA;
				}
				finally
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_2A7;
						case 2:
							enumerator.Dispose();
							num = 1;
							continue;
						}
						if (enumerator == null)
						{
							break;
						}
						num = 2;
					}
					IL_2A7:;
				}
				return;
				for (;;)
				{
					IL_AC:
					using (List<XlsVPageBreak>.Enumerator enumerator2 = list.GetEnumerator())
					{
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								num = 4;
								continue;
							case 3:
							{
								if (true)
								{
								}
								if (!enumerator2.MoveNext())
								{
									num = 1;
									continue;
								}
								XlsVPageBreak item = enumerator2.Current;
								base.Remove(item);
								num = 2;
								continue;
							}
							case 4:
								goto IL_99;
							}
							IL_4E:
							num = 3;
							continue;
							goto IL_4E;
						}
						IL_99:
						break;
					}
				}
				return;
			}
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00016FBC File Offset: 0x00015FBC
		public TBIFFRecord TypeCode
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
				return TBIFFRecord.Unknown;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00016FF8 File Offset: 0x00015FF8
		public int RecordCode
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
				return 0;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00017034 File Offset: 0x00016034
		public bool NeedDataArray
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
				return false;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00017070 File Offset: 0x00016070
		// (set) Token: 0x0600028C RID: 652 RVA: 0x000170B0 File Offset: 0x000160B0
		public long StreamPos
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
				return -1L;
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
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000170EC File Offset: 0x000160EC
		public int GetStoreSize(ExcelVersion version)
		{
			int count;
			for (;;)
			{
				count = base.Count;
				if (count > 0)
				{
					goto IL_3E;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_23;
				}
			}
			IL_23:
			if (true)
			{
			}
			if (false)
			{
			}
			return -4;
			IL_3E:
			return 6 * count + 2;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0001713C File Offset: 0x0001613C
		public int FillStream(BinaryWriter writer, DataProvider provider, IEncryptor encryptor, int streamPosition)
		{
			spr\u2583 spr_u;
			for (;;)
			{
				spr_u = this.ᜀ();
				if (spr_u != null)
				{
					goto IL_3C;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_22;
				}
			}
			IL_22:
			if (false)
			{
			}
			if (true)
			{
			}
			return 0;
			IL_3C:
			return spr_u.FillStream(writer, provider, encryptor, streamPosition);
		}

		// Token: 0x04000079 RID: 121
		private int \u2460\u008C\u0088\u0091;

		// Token: 0x0400007A RID: 122
		private string \u25D8\u008C\u00A3\u0086;

		// Token: 0x0400007B RID: 123
		private new XlsWorkbook ᜀ;
	}
}
