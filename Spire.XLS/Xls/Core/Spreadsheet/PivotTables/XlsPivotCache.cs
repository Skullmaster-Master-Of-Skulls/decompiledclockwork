using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;

namespace Spire.Xls.Core.Spreadsheet.PivotTables
{
	// Token: 0x0200004F RID: 79
	public class XlsPivotCache : XlsObject, ICloneParent, IRecordStorage, IPivotCache
	{
		// Token: 0x06000789 RID: 1929 RVA: 0x00050D88 File Offset: 0x0004FD88
		internal XlsPivotCache(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00050DFC File Offset: 0x0004FDFC
		internal XlsPivotCache(spr\u1DF5 A_0, object A_1, sprἛ A_2, IDecryptor A_3, string A_4) : this(A_0, A_1)
		{
			this.ᜀ(A_2, A_3, A_4);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00050E1C File Offset: 0x0004FE1C
		internal XlsPivotCache(spr\u1DF5 A_0, object A_1, IXLSRange A_2) : base(A_0, A_1)
		{
			this.ᜀ.ᜀ(DataSourceType.Worksheet);
			int row = A_2.Row;
			int lastRow = A_2.LastRow;
			int column = A_2.Column;
			int lastColumn = A_2.LastColumn;
			int i = column;
			int num = 0;
			while (i <= lastColumn)
			{
				this.ᜀ(A_2.Worksheet, row, lastRow, i);
				i++;
				num++;
			}
			this.RefreshDate = DateTime.Now;
			byte refreshedVersion = 0;
			this.CreatedVersion = (this.MinRefreshableVersion = (this.RefreshedVersion = (int)refreshedVersion));
			this.ᜆ = A_2;
			this.SourceType = DataSourceType.Worksheet;
			this.HasCacheRecords = true;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00050F24 File Offset: 0x0004FF24
		private void ᜀ(IWorksheet A_0, int A_1, int A_2, int A_3)
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
			XlsPivotCacheField xlsPivotCacheField = this.ᜄ.ᜁ(A_0[A_1, A_3].Value);
			xlsPivotCacheField.ItemRange = A_0[A_1 + 1, A_3, A_2, A_3];
			xlsPivotCacheField.ᜀ(A_0, A_1, A_2, A_3);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00050F98 File Offset: 0x0004FF98
		public int AddIndexes(byte[] indexes)
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
			sprᜡ sprᜡ = new sprᜡ();
			sprᜡ.ᜀ(indexes);
			this.ᜅ.Add(sprᜡ);
			return this.ᜅ.Count - 1;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00050FFC File Offset: 0x0004FFFC
		internal object ᜀ(int A_0, int A_1)
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
			int index = (int)this.ᜅ[A_1].ᜀ()[A_0];
			XlsPivotCacheField xlsPivotCacheField = this.ᜄ.ᜀ(A_0);
			return xlsPivotCacheField.GetValue(index);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00051060 File Offset: 0x00050060
		internal byte ᜀ(int A_0, object A_1)
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
			XlsPivotCacheField xlsPivotCacheField = this.ᜄ.ᜀ(A_0);
			return (byte)xlsPivotCacheField.ᜁ(A_1);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000510B0 File Offset: 0x000500B0
		private int ᜀ(BiffRecordRaw[] A_0, int A_1)
		{
			int a_ = 2;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_C0;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_F6;
					}
					break;
				case 3:
				{
					if (A_1 > A_0.Length - 1)
					{
						num = 4;
						continue;
					}
					BiffRecordRaw biffRecordRaw = A_0[A_1];
					A_1++;
					biffRecordRaw.CheckTypeCode(TBIFFRecord.CacheData);
					biffRecordRaw = A_0[A_1];
					A_1++;
					num = 0;
					continue;
				}
				case 4:
					goto IL_15D;
				case 5:
					if (A_1 >= 0)
					{
						num = 8;
						continue;
					}
					goto IL_78;
				case 6:
					goto IL_4C;
				case 7:
					goto IL_C0;
				case 8:
					num = 3;
					continue;
				case 9:
				{
					BiffRecordRaw biffRecordRaw;
					if (biffRecordRaw.TypeCode == TBIFFRecord.EOF)
					{
						num = 1;
						continue;
					}
					this.ᜂ.Add(biffRecordRaw);
					biffRecordRaw = A_0[A_1];
					A_1++;
					num = 7;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				num = 5;
				continue;
				IL_C0:
				num = 9;
			}
			IL_4C:
			throw new ArgumentNullException(RecordTableEnumerator.b("尷嬹䠻弽", a_));
			IL_78:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䠷唹伻", a_), RecordTableEnumerator.b("渷嬹倻䬽┿扁❃❅♇⑉⍋㩍灏けㅓ癕㑗㽙⽛ⵝ䁟ᙡౣݥ٧䩩屫乭ᅯᱱၳ噵ίࡹ᥻ώꚅﲇ낏聯몙ﮝ캟얡킣캥蚧", a_));
			IL_F6:
			if (false)
			{
			}
			if (true)
			{
			}
			return A_1;
			IL_15D:
			goto IL_78;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00051220 File Offset: 0x00050220
		private void ᜀ(sprἛ A_0, IDecryptor A_1, string A_2)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num = 6;
				ushort num3;
				TBIFFRecord tbiffrecord;
				for (;;)
				{
					BiffRecordRaw biffRecordRaw;
					switch (num)
					{
					case 0:
						goto IL_12B;
					case 1:
						goto IL_12B;
					case 2:
						goto IL_AC;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AC;
						default:
							goto IL_16D;
						}
						break;
					case 4:
						if (A_0.ᜈ().Length == 0L)
						{
							num = 9;
							continue;
						}
						goto IL_91;
					case 5:
						goto IL_65;
					case 7:
						goto IL_1B8;
					case 8:
						if (biffRecordRaw.TypeCode == TBIFFRecord.EOF)
						{
							num = 3;
							continue;
						}
						biffRecordRaw = A_0.ᜀ(A_1);
						this.ᜂ.Add(biffRecordRaw);
						num = 0;
						continue;
					case 9:
					{
						int num2 = 0;
						int.TryParse(A_2, NumberStyles.AllowHexSpecifier, null, out num2);
						num3 = 213;
						num = 10;
						continue;
					}
					case 10:
					{
						int num2;
						if (num2 == (int)num3)
						{
							num = 7;
							continue;
						}
						goto IL_91;
					}
					case 11:
						goto IL_C0;
					}
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					num = 4;
					continue;
					IL_AC:
					if (tbiffrecord != TBIFFRecord.CacheData)
					{
						num = 11;
						continue;
					}
					this.ᜀ = (sprᾦ)A_0.ᜀ(A_1);
					biffRecordRaw = this.ᜀ;
					num = 1;
					continue;
					IL_91:
					tbiffrecord = A_0.ᜉ();
					if (true)
					{
					}
					num = 2;
					continue;
					IL_12B:
					num = 8;
				}
				IL_65:
				throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
				IL_C0:
				throw new spr\u1AC0(tbiffrecord);
				IL_16D:
				if (false)
				{
				}
				return;
				IL_1B8:
				this.ᜀ.ᜁ(num3);
				return;
			}
			}
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x000513E8 File Offset: 0x000503E8
		public void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 2;
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
				if (records != null)
				{
					records.ᜀ(this.ᜀ);
					records.AddList(this.ᜂ);
					return;
				}
				break;
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹弻儽㈿♁㝃", a_));
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00051460 File Offset: 0x00050460
		internal void ᜀ(Stream A_0, IEncryptor A_1)
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜃ.Length > 0L)
					{
						num = 7;
						continue;
					}
					goto IL_13A;
				case 1:
					if (this.ᜂ.Count > 0)
					{
						num = 6;
						continue;
					}
					return;
				case 2:
					num = 0;
					continue;
				case 3:
					goto IL_69;
				case 4:
					if (this.ᜂ != null)
					{
						num = 8;
						continue;
					}
					return;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_69;
					default:
					{
						if (false)
						{
						}
						RecordArrayList recordArrayList = new RecordArrayList();
						this.SerializeDataToList(recordArrayList);
						sprᡄ sprᡄ = new sprᡄ(A_0, false);
						num = 3;
						continue;
					}
					}
					break;
				case 7:
					goto IL_F4;
				case 8:
					num = 1;
					continue;
				}
				if (this.ᜃ != null)
				{
					num = 2;
					continue;
				}
				IL_13A:
				num = 4;
				continue;
				IL_69:
				try
				{
					RecordArrayList recordArrayList;
					sprᡄ sprᡄ;
					sprᡄ.ᜀ(recordArrayList, A_1);
					return;
				}
				finally
				{
					num = 2;
					for (;;)
					{
						sprᡄ sprᡄ;
						switch (num)
						{
						case 0:
							goto IL_137;
						case 1:
							((IDisposable)sprᡄ).Dispose();
							num = 0;
							continue;
						}
						if (sprᡄ == null)
						{
							break;
						}
						num = 1;
					}
					IL_137:;
				}
				goto IL_13A;
			}
			IL_F4:
			if (true)
			{
			}
			this.ᜃ.WriteTo(A_0);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x000515DC File Offset: 0x000505DC
		internal void ᜀ(XlsWorksheet A_0, int A_1, int A_2, bool A_3, bool A_4)
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (A_4)
					{
						num = 2;
						continue;
					}
					this.ᜀ(A_0, A_1, A_2, A_3);
					num = 4;
					continue;
				case 2:
					goto IL_66;
				case 3:
					if (true)
					{
					}
					num = 6;
					continue;
				case 4:
					goto IL_AB;
				case 6:
					if (this.ᜆ.Worksheet == A_0)
					{
						num = 0;
						continue;
					}
					goto IL_AD;
				}
				if (this.ᜆ == null)
				{
					goto IL_AD;
				}
				num = 3;
			}
			IL_66:
			this.ᜁ(A_0, A_1, A_2, A_3);
			return;
			IL_AB:
			IL_AD:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_AB;
			default:
				if (false)
				{
				}
				return;
			}
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x000516B4 File Offset: 0x000506B4
		private void ᜁ(XlsWorksheet A_0, int A_1, int A_2, bool A_3)
		{
			switch (0)
			{
			default:
			{
				int num;
				int num2;
				int num3;
				int num4;
				for (;;)
				{
					num = this.ᜆ.Row;
					num2 = this.ᜆ.Column;
					num3 = this.ᜆ.LastRow;
					num4 = this.ᜆ.LastColumn;
					int num5 = 11;
					for (;;)
					{
						switch (num5)
						{
						case 0:
							num = Math.Max(A_1, num - A_2);
							num3 -= A_2;
							num5 = 10;
							continue;
						case 1:
							num5 = 13;
							continue;
						case 2:
							num5 = 9;
							continue;
						case 3:
							goto IL_1AD;
						case 4:
							goto IL_F3;
						case 5:
							num2 = Math.Max(A_1, num2 - A_2);
							num4 -= A_2;
							num5 = 3;
							continue;
						case 6:
							if (A_1 <= num)
							{
								num5 = 0;
								continue;
							}
							goto IL_B6;
						case 7:
							goto IL_1F3;
						case 8:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_F3;
							default:
								if (false)
								{
								}
								if (!A_3)
								{
									num5 = 1;
									continue;
								}
								goto IL_1F5;
							}
							break;
						case 9:
							if (A_3)
							{
								num5 = 14;
								continue;
							}
							num4 = Math.Max(num4 - A_2, A_1 - 1);
							num5 = 7;
							continue;
						case 10:
							goto IL_191;
						case 11:
							if (XlsPivotCache.ᜀ(this.ᜆ, A_0, A_1, A_2, A_3))
							{
								num5 = 2;
								continue;
							}
							num5 = 12;
							continue;
						case 12:
							if (A_3)
							{
								num5 = 4;
								continue;
							}
							goto IL_B6;
						case 13:
							if (A_1 <= num2)
							{
								num5 = 5;
								continue;
							}
							goto IL_1F5;
						case 14:
							num3 = Math.Max(num3 - A_2, A_1 - 1);
							num5 = 15;
							continue;
						case 15:
							goto IL_133;
						}
						break;
						IL_B6:
						num5 = 8;
						continue;
						IL_F3:
						num5 = 6;
					}
				}
				IL_133:
				IL_191:
				goto IL_1F5;
				IL_1AD:
				if (true)
				{
				}
				IL_1F3:
				IL_1F5:
				this.ᜆ = A_0[num, num2, num3, num4];
				return;
			}
			}
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x000518C8 File Offset: 0x000508C8
		private void ᜀ(XlsWorksheet A_0, int A_1, int A_2, bool A_3)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						int row;
						int column;
						int lastRow;
						int lastColumn;
						this.ᜆ = (A_3 ? A_0[row, column, lastRow + A_2, lastColumn] : A_0[row, column, lastRow, lastColumn + A_2]);
						num = 2;
						continue;
					}
					case 2:
						return;
					case 3:
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
							int row = this.ᜆ.Row;
							int column = this.ᜆ.Column;
							int lastRow = this.ᜆ.LastRow;
							int lastColumn = this.ᜆ.LastColumn;
							num = 0;
							continue;
						}
						}
						break;
					}
					if (!XlsPivotCache.ᜀ(this.ᜆ, A_0, A_1, A_2, A_3))
					{
						break;
					}
					num = 3;
				}
				return;
			}
			}
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x000519C8 File Offset: 0x000509C8
		private static bool ᜀ(IXLSRange A_0, XlsWorksheet A_1, int A_2, int A_3, bool A_4)
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
			bool flag = A_0.Worksheet == A_1;
			return flag & (A_4 ? (A_0.Row < A_2 && A_0.LastRow >= A_2) : (A_0.Column < A_2 && A_0.LastColumn >= A_2));
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x00051A50 File Offset: 0x00050A50
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x00051A98 File Offset: 0x00050A98
		[CLSCompliant(false)]
		public ushort StreamId
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
				return this.ᜀ.ᜌ();
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
				this.ᜀ.ᜁ(value);
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x00051AE0 File Offset: 0x00050AE0
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x00051B28 File Offset: 0x00050B28
		public DataSourceType SourceType
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
				return this.ᜀ.ᜏ();
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
				this.ᜀ.ᜀ(value);
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00051B70 File Offset: 0x00050B70
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x00051BB4 File Offset: 0x00050BB4
		public bool IsUpgradeOnRefresh
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
				return this.ᜏ;
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
				this.ᜏ = value;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00051BF8 File Offset: 0x00050BF8
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x00051C40 File Offset: 0x00050C40
		public string RefreshedBy
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
				return this.ᜀ.ᜊ();
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
				this.ᜀ.ᜀ(value);
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x00051C88 File Offset: 0x00050C88
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x00051CCC File Offset: 0x00050CCC
		public bool IsSupportSubQuery
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
				return this.ᜎ;
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
				this.ᜎ = value;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00051D10 File Offset: 0x00050D10
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x00051D58 File Offset: 0x00050D58
		public bool IsSaveData
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
				return this.ᜀ.ᜃ();
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
				this.ᜀ.ᜀ(value);
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00051DA0 File Offset: 0x00050DA0
		// (set) Token: 0x060007A5 RID: 1957 RVA: 0x00051DE8 File Offset: 0x00050DE8
		public bool IsOptimizedCache
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
				return this.ᜀ.ᜀ();
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
				this.ᜀ.ᜂ(value);
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00051E30 File Offset: 0x00050E30
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x00051E78 File Offset: 0x00050E78
		public bool EnableRefresh
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
				return this.ᜀ.ᜁ();
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
				this.ᜀ.ᜄ(false);
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00051EC0 File Offset: 0x00050EC0
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x00051F08 File Offset: 0x00050F08
		public bool IsBackgroundQuery
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
				return this.ᜀ.\u170D();
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
				this.ᜀ.ᜃ(value);
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x00051F50 File Offset: 0x00050F50
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x00051F94 File Offset: 0x00050F94
		public int CreatedVersion
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
				return this.ᜋ;
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
				this.ᜋ = value;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00051FD8 File Offset: 0x00050FD8
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x0005201C File Offset: 0x0005101C
		public int MinRefreshableVersion
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
				return this.ᜌ;
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
				this.ᜌ = value;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00052060 File Offset: 0x00051060
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x000520A4 File Offset: 0x000510A4
		public int RefreshedVersion
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
				return this.\u170D;
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
				this.\u170D = value;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x000520E8 File Offset: 0x000510E8
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x00052130 File Offset: 0x00051130
		public bool IsInvalidData
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
				return this.ᜀ.ᜅ();
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
				this.ᜀ.ᜁ(value);
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00052178 File Offset: 0x00051178
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x000521BC File Offset: 0x000511BC
		public bool SupportAdvancedDrill
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
				return this.ᜊ;
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
				this.ᜊ = value;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00052200 File Offset: 0x00051200
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x00052248 File Offset: 0x00051248
		public bool IsRefreshOnLoad
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
				return this.ᜀ.ᜋ();
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
				this.ᜀ.ᜅ(value);
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00052290 File Offset: 0x00051290
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x000522DC File Offset: 0x000512DC
		public DateTime RefreshDate
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
				return DateTime.FromOADate(this.ᜁ.ᜀ());
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
				this.ᜁ.ᜀ(value.ToOADate());
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0005232C File Offset: 0x0005132C
		public int RecordCount
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
				return this.ᜅ.Count;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00052374 File Offset: 0x00051374
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x000523B8 File Offset: 0x000513B8
		public IXLSRange SourceRange
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
			internal set
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
				this.ᜆ = value;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x000523FC File Offset: 0x000513FC
		internal sprᾷ CacheFields
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
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00052440 File Offset: 0x00051440
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x00052484 File Offset: 0x00051484
		public int Index
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
				return this.ᜈ;
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
				this.ᜈ = value;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x000524C8 File Offset: 0x000514C8
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x0005250C File Offset: 0x0005150C
		internal spr\u257E Info
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
				return this.ᜉ;
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
				this.ᜉ = value;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00052550 File Offset: 0x00051550
		internal Dictionary<string, Stream> PreservedElements
		{
			get
			{
				int num = 1;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 2:
						this.ᜐ = new Dictionary<string, Stream>();
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
							continue;
						}
						break;
					}
					if (this.ᜐ != null)
					{
						break;
					}
					num = 2;
				}
				IL_6F:
				return this.ᜐ;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x000525D4 File Offset: 0x000515D4
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x00052618 File Offset: 0x00051618
		public string RangeName
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
				return this.ᜇ;
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
				this.ᜇ = value;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0005265C File Offset: 0x0005165C
		public bool HasNamedRange
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
				return this.ᜇ != null;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x000526A4 File Offset: 0x000516A4
		public int CalculatedItemIndex
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
				return 1;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x000526E0 File Offset: 0x000516E0
		// (set) Token: 0x060007C6 RID: 1990 RVA: 0x00052724 File Offset: 0x00051724
		internal sprᦨ PreservedExtenalRelation
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
				return this.ᜑ;
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
				this.ᜑ = value;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00052768 File Offset: 0x00051768
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x000527AC File Offset: 0x000517AC
		internal string RelationId
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
				return this.\u1712;
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
				this.\u1712 = value;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x000527F0 File Offset: 0x000517F0
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x00052834 File Offset: 0x00051834
		internal bool HasCacheRecords
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
				return this.\u1713;
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
				this.\u1713 = value;
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00052878 File Offset: 0x00051878
		public object Clone(object parent)
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
			return this.Clone(parent, null);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000528BC File Offset: 0x000518BC
		public object Clone(object parent, Dictionary<string, string> hashNewNames)
		{
			switch (0)
			{
			default:
			{
				XlsPivotCache xlsPivotCache;
				for (;;)
				{
					xlsPivotCache = (XlsPivotCache)base.MemberwiseClone();
					xlsPivotCache.SetParent(parent);
					xlsPivotCache.ᜀ = (sprᾦ)spr\u1CD3.ᜀ(this.ᜀ);
					xlsPivotCache.ᜁ = (sprᰔ)spr\u1CD3.ᜀ(this.ᜁ);
					xlsPivotCache.ᜂ = spr\u1CD3.ᜀ(this.ᜂ);
					xlsPivotCache.ᜉ = (spr\u257E)spr\u1CD3.ᜀ(this.ᜉ);
					xlsPivotCache.ᜃ = new MemoryStream();
					this.ᜃ.WriteTo(xlsPivotCache.ᜃ);
					int num = 1;
					for (;;)
					{
						XlsWorkbook xlsWorkbook;
						switch (num)
						{
						case 0:
						{
							if (xlsWorkbook == this.ᜆ.Worksheet.Workbook)
							{
								num = 2;
								continue;
							}
							XlsWorksheet xlsWorksheet = this.ᜆ.Worksheet as XlsWorksheet;
							IWorksheets worksheets = xlsWorkbook.Worksheets;
							IWorksheet worksheet = (worksheets as XlsWorksheetsCollection).AddCopy(xlsWorksheet, WorksheetCopyType.CopyCells);
							worksheet.Visibility = WorksheetVisibility.StrongHidden;
							Dictionary<string, string> dictionary = new Dictionary<string, string>();
							dictionary.Add(xlsWorksheet.Name, worksheet.Name);
							xlsPivotCache.ᜆ = ((ICombinedRange)this.ᜆ).Clone(parent, dictionary, xlsWorkbook);
							num = 5;
							continue;
						}
						case 1:
							if (this.ᜆ != null)
							{
								num = 3;
								continue;
							}
							return xlsPivotCache;
						case 2:
							goto IL_1F8;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1F8;
							default:
							{
								if (false)
								{
								}
								if (true)
								{
								}
								xlsWorkbook = (XlsWorkbook)xlsPivotCache.FindParent(typeof(XlsWorkbook));
								string name = this.ᜆ.Worksheet.Name;
								num = 0;
								continue;
							}
							}
							break;
						case 4:
							return xlsPivotCache;
						case 5:
							return xlsPivotCache;
						}
						break;
						IL_1F8:
						xlsPivotCache.ᜆ = ((ICombinedRange)this.ᜆ).Clone(parent, hashNewNames, xlsWorkbook);
						num = 4;
					}
				}
				return xlsPivotCache;
			}
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00052AC8 File Offset: 0x00051AC8
		private IXLSRange ᜁ(object A_0, XlsWorkbook A_1, IXLSRange A_2)
		{
			int a_ = 15;
			switch (0)
			{
			default:
			{
				string text;
				for (;;)
				{
					IL_2C:
					XlsWorkbook xlsWorkbook = (XlsWorkbook)A_2.Worksheet.Workbook;
					text = xlsWorkbook.FullFileName;
					for (;;)
					{
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (true)
								{
								}
								if (text == null)
								{
									num = 1;
									continue;
								}
								goto IL_A3;
							case 1:
								text = RecordTableEnumerator.b("݄⡆♈⁊籌慎⥐㽒♔⽖", a_);
								num = 2;
								continue;
							case 2:
								goto IL_85;
							}
							goto IL_2C;
						}
						IL_85:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_9B;
						}
					}
				}
				IL_9B:
				if (false)
				{
				}
				IL_A3:
				int index = A_1.ExternWorkbooks.ᜀ(text, A_1, A_2);
				XlsExternWorkbook xlsExternWorkbook = A_1.ExternWorkbooks[index];
				XlsExternWorksheet a_2 = xlsExternWorkbook.Worksheets[A_2.Worksheet.Index];
				return new spr\u20A6(a_2, A_2.Row, A_2.Column, A_2.LastRow, A_2.LastColumn);
			}
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00052BCC File Offset: 0x00051BCC
		private IXLSRange ᜀ(object A_0, XlsWorkbook A_1, IXLSRange A_2)
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
			return new spr\u171E(A_0, A_2);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00052C10 File Offset: 0x00051C10
		public bool ComparePreservedData(XlsPivotCache cache)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_6A;
					}
					break;
				case 3:
					goto IL_83;
				}
				if (this.ᜃ.Length == cache.ᜃ.Length)
				{
					num = 0;
				}
				else
				{
					num = 2;
				}
			}
			IL_6A:
			if (false)
			{
			}
			if (true)
			{
			}
			return false;
			IL_83:
			return BiffRecordRaw.CompareArrays(this.ᜃ.GetBuffer(), cache.ᜃ.GetBuffer());
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00052CC0 File Offset: 0x00051CC0
		public TBIFFRecord TypeCode
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
				return TBIFFRecord.Unknown;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00052CFC File Offset: 0x00051CFC
		public int RecordCode
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
				return 0;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00052D38 File Offset: 0x00051D38
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
				return true;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00052D74 File Offset: 0x00051D74
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x00052DB4 File Offset: 0x00051DB4
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
				return 0L;
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

		// Token: 0x060007D5 RID: 2005 RVA: 0x00052DF0 File Offset: 0x00051DF0
		public int GetStoreSize(ExcelVersion version)
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
			return (int)this.ᜃ.Length;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00052E38 File Offset: 0x00051E38
		public int FillStream(BinaryWriter writer, DataProvider provider, IEncryptor encryptor, int streamPosition)
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
			Stream baseStream = writer.BaseStream;
			this.ᜃ.WriteTo(baseStream);
			return (int)this.ᜃ.Length;
		}

		// Token: 0x04000132 RID: 306
		private sprᾦ ᜀ = (sprᾦ)spr\u175E.ᜀ(TBIFFRecord.CacheData);

		// Token: 0x04000133 RID: 307
		private sprᰔ ᜁ = (sprᰔ)spr\u175E.ᜀ(TBIFFRecord.CacheDataEx);

		// Token: 0x04000134 RID: 308
		private List<BiffRecordRaw> ᜂ = new List<BiffRecordRaw>();

		// Token: 0x04000135 RID: 309
		private MemoryStream ᜃ = new MemoryStream();

		// Token: 0x04000136 RID: 310
		private sprᾷ ᜄ = new sprᾷ();

		// Token: 0x04000137 RID: 311
		private List<sprᜡ> ᜅ = new List<sprᜡ>();

		// Token: 0x04000138 RID: 312
		private long \u2593\u00AFª\u0085;

		// Token: 0x04000139 RID: 313
		private long[] \u2609\u00B0\u00A4\u009B;

		// Token: 0x0400013A RID: 314
		private IXLSRange ᜆ;

		// Token: 0x0400013B RID: 315
		private string ᜇ;

		// Token: 0x0400013C RID: 316
		private int ᜈ = -1;

		// Token: 0x0400013D RID: 317
		private spr\u257E ᜉ;

		// Token: 0x0400013E RID: 318
		private bool ᜊ;

		// Token: 0x0400013F RID: 319
		private int ᜋ;

		// Token: 0x04000140 RID: 320
		private int ᜌ;

		// Token: 0x04000141 RID: 321
		private int \u170D;

		// Token: 0x04000142 RID: 322
		private bool ᜎ;

		// Token: 0x04000143 RID: 323
		private bool \u25D9\u00A9\u0099\u0090;

		// Token: 0x04000144 RID: 324
		private bool ᜏ;

		// Token: 0x04000145 RID: 325
		private string \u2460\u008A\u0091\u0083;

		// Token: 0x04000146 RID: 326
		private Dictionary<string, Stream> ᜐ;

		// Token: 0x04000147 RID: 327
		private sprᦨ ᜑ;

		// Token: 0x04000148 RID: 328
		private string \u1712;

		// Token: 0x04000149 RID: 329
		private bool \u1713;
	}
}
