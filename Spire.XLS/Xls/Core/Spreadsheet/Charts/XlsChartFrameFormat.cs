using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Spire.Xls.Charts;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x02000189 RID: 393
	public class XlsChartFrameFormat : XlsObject, IChartFrameFormat, spr\u218E
	{
		// Token: 0x06001337 RID: 4919 RVA: 0x000BB6D4 File Offset: 0x000BA6D4
		internal XlsChartFrameFormat(spr\u1DF5 A_0, object A_1) : this(A_0, A_1, false, false, true)
		{
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x000BB6EC File Offset: 0x000BA6EC
		internal XlsChartFrameFormat(spr\u1DF5 A_0, object A_1, bool A_2) : this(A_0, A_1, false, false, A_2)
		{
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x000BB704 File Offset: 0x000BA704
		internal XlsChartFrameFormat(spr\u1DF5 A_0, object A_1, bool A_2, bool A_3, bool A_4)
		{
			this.ᜀ = (sprᳫ)spr\u175E.ᜀ(TBIFFRecord.ChartFrame);
			base..ctor(A_0, A_1);
			this.ᜀ();
			if (!this.Workbook.Loading && A_4)
			{
				this.SetDefaultValues(A_2, A_3);
			}
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x000BB754 File Offset: 0x000BA754
		internal XlsChartFrameFormat(spr\u1DF5 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3)
		{
			this.ᜀ = (sprᳫ)spr\u175E.ᜀ(TBIFFRecord.ChartFrame);
			base..ctor(A_0, A_1);
			this.ᜀ();
			this.ᜀ(A_2, ref A_3);
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x000BB790 File Offset: 0x000BA790
		private void ᜀ()
		{
			int a_ = 15;
			this.m_chart = (base.FindParent(typeof(XlsChart)) as XlsChart);
			if (this.m_chart == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("ᕄ♆㭈⹊⍌㭎煐㱒㝔㵖㱘㡚⥜罞ɠɢ୤०٨Ὢ䵬൮ᑰ卲፴ᡶ౸ᕺ᥼", a_));
				}
			}
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x000BB810 File Offset: 0x000BA810
		internal void ᜀ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 17;
			int num = 1;
			for (;;)
			{
				int num2;
				BiffRecordRaw biffRecordRaw;
				switch (num)
				{
				case 0:
					goto IL_57;
				case 2:
					num = 5;
					continue;
				case 3:
					goto IL_122;
				case 4:
					if (num2 == 0)
					{
						num = 11;
						continue;
					}
					goto IL_13B;
				case 5:
					if (A_1 > A_0.Count)
					{
						num = 6;
						continue;
					}
					biffRecordRaw = A_0[A_1];
					biffRecordRaw = this.UnwrapRecord(biffRecordRaw);
					biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartFrame);
					this.ᜀ = (sprᳫ)biffRecordRaw;
					A_1++;
					biffRecordRaw = A_0[A_1];
					num2 = 0;
					num = 7;
					continue;
				case 6:
					goto IL_1BD;
				case 7:
					if (this.CheckBegin(biffRecordRaw))
					{
						num = 9;
						continue;
					}
					return;
				case 8:
					goto IL_13B;
				case 9:
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_16E;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num = 8;
						continue;
					}
					break;
				case 10:
					if (A_1 >= 0)
					{
						num = 2;
						continue;
					}
					goto IL_16E;
				case 11:
					A_1++;
					num = 3;
					continue;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				num = 10;
				continue;
				IL_13B:
				A_1++;
				biffRecordRaw = A_0[A_1];
				this.ParseRecord(biffRecordRaw, ref num2);
				num = 4;
			}
			IL_57:
			throw new ArgumentNullException(RecordTableEnumerator.b("⍆⡈㽊ⱌ", a_));
			IL_122:
			return;
			IL_16E:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⹆᥈⑊㹌", a_), RecordTableEnumerator.b("ᅆ⡈❊㡌⩎煐げ㑔㥖㝘㑚⥜罞͠٢䕤୦౨ᡪṬ佮հ᭲ᑴ᥶奸䭺嵼Ṿꖄﮈﮎ떔漢뾞슠첢키즦\udda8", a_));
			IL_1BD:
			goto IL_16E;
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x000BB9DC File Offset: 0x000BA9DC
		internal virtual bool CheckBegin(BiffRecordRaw record)
		{
			int a_ = 5;
			if (record == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼尾⹀ㅂ⅄", a_));
				}
			}
			return record.TypeCode == TBIFFRecord.Begin;
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x000BBA48 File Offset: 0x000BAA48
		internal virtual void ParseRecord(BiffRecordRaw record, ref int iBeginCounter)
		{
			int a_ = 10;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 4;
					continue;
				case 2:
				{
					TBIFFRecord typeCode;
					if (typeCode <= TBIFFRecord.ChartAreaFormat)
					{
						num = 8;
						continue;
					}
					num = 10;
					continue;
				}
				case 3:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartLineFormat)
					{
						num = 0;
						continue;
					}
					goto IL_108;
				}
				case 4:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartAreaFormat)
					{
						num = 6;
						continue;
					}
					goto IL_C2;
				}
				case 5:
					num = 9;
					continue;
				case 6:
					return;
				case 7:
					return;
				case 8:
					num = 3;
					continue;
				case 9:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartGelFrame)
					{
						num = 7;
						continue;
					}
					goto IL_1A6;
				}
				case 10:
				{
					TBIFFRecord typeCode;
					switch (typeCode)
					{
					case TBIFFRecord.Begin:
						goto IL_78;
					case TBIFFRecord.End:
						goto IL_13B;
					default:
						if (true)
						{
						}
						num = 5;
						continue;
					}
					break;
				}
				case 11:
					goto IL_73;
				}
				if (record == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 11;
						break;
					}
				}
				else
				{
					TBIFFRecord typeCode = record.TypeCode;
					num = 2;
				}
			}
			IL_73:
			throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁❃⥅㩇⹉", a_));
			IL_78:
			iBeginCounter++;
			return;
			IL_C2:
			this.ᜂ = new ChartInterior((spr\u2158)base.ReservedHandle, this, (sprᨓ)record);
			return;
			IL_108:
			this.m_border = new ChartBorder((spr\u2158)base.ReservedHandle, this, (spr\u22F3)record);
			return;
			IL_13B:
			iBeginCounter--;
			return;
			IL_1A6:
			this.ᜄ = new spr\u2436((spr\u2158)base.ReservedHandle, this, (spr\u216D)record);
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000BBC18 File Offset: 0x000BAC18
		internal void ᜀ(IList<IRecordStorage> A_0)
		{
			int a_ = 17;
			for (;;)
			{
				IL_09:
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜂ.ᜀ(A_0);
						num = 2;
						continue;
					case 2:
						goto IL_124;
					case 3:
						this.m_border.ᜀ(A_0);
						num = 10;
						continue;
					case 4:
						goto IL_10E;
					case 5:
						if (this.ᜄ != null)
						{
							num = 8;
							continue;
						}
						goto IL_160;
					case 6:
						if (this.m_border != null)
						{
							num = 3;
							continue;
						}
						goto IL_6E;
					case 7:
						goto IL_50;
					case 8:
						this.ᜄ.ᜀ(A_0);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_09;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 9:
						if (this.ᜂ != null)
						{
							num = 0;
							continue;
						}
						goto IL_124;
					case 10:
						goto IL_6E;
					}
					if (A_0 == null)
					{
						num = 7;
						continue;
					}
					this.SerializeRecord(A_0, this.ᜀ);
					this.SerializeRecord(A_0, spr\u175E.ᜀ(TBIFFRecord.Begin));
					num = 6;
					continue;
					IL_6E:
					num = 9;
					continue;
					IL_124:
					num = 5;
				}
			}
			IL_50:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⡊≌㵎㕐⁒", a_));
			IL_10E:
			IL_160:
			this.SerializeRecord(A_0, spr\u175E.ᜀ(TBIFFRecord.End));
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x000BBD98 File Offset: 0x000BAD98
		internal virtual void SerializeRecord(IList<IRecordStorage> list, BiffRecordRaw record)
		{
			int a_ = 0;
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
						goto IL_A1;
					default:
						if (false)
						{
						}
						if (record == null)
						{
							num = 3;
							continue;
						}
						goto IL_A1;
					}
					break;
				case 1:
					goto IL_3C;
				case 3:
					goto IL_8B;
				}
				if (list == null)
				{
					if (true)
					{
					}
					num = 1;
				}
				else
				{
					num = 0;
				}
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("娵儷䤹䠻", a_));
			IL_8B:
			throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷夹医䰽␿", a_));
			IL_A1:
			list.Add((BiffRecordRaw)record.Clone());
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x000BBE58 File Offset: 0x000BAE58
		internal virtual BiffRecordRaw UnwrapRecord(BiffRecordRaw record)
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
			return record;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x000BBE94 File Offset: 0x000BAE94
		public void SetDefaultValues(bool bAutoSize, bool bIsInteriorGray)
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
			this.ᜀ = (sprᳫ)spr\u175E.ᜀ(TBIFFRecord.ChartFrame);
			this.ᜀ.ᜁ(this.IsAutoSize);
			this.m_border = new ChartBorder((spr\u2158)base.ReservedHandle, this);
			this.m_border.KnownColor = ExcelColors.Gray50Percent;
			this.m_border.UseDefaultFormat = !this.m_chart.IsChart3D;
			this.ᜂ = new ChartInterior((spr\u2158)base.ReservedHandle, this);
			this.ᜂ.InitForFrameFormat(bAutoSize, this.m_chart.IsChart3D, bIsInteriorGray);
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x000BBF64 File Offset: 0x000BAF64
		internal sprᳫ FrameRecord
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
				return this.ᜀ;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x000BBFA8 File Offset: 0x000BAFA8
		protected internal XlsWorkbook Workbook
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
				return this.m_chart.InnerWorkbook;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x000BBFF0 File Offset: 0x000BAFF0
		// (set) Token: 0x06001346 RID: 4934 RVA: 0x000BC034 File Offset: 0x000BB034
		internal Stream LayoutStream
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
				return this.ᜅ;
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
				this.ᜅ = value;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x000BC078 File Offset: 0x000BB078
		public bool HasInterior
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
				return this.ᜂ != null;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x000BC0C0 File Offset: 0x000BB0C0
		// (set) Token: 0x06001349 RID: 4937 RVA: 0x000BC108 File Offset: 0x000BB108
		public bool HasLineProperties
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
				return this.m_border != null;
			}
			internal set
			{
				if (value)
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
						IChartBorder border = this.Border;
						return;
					}
					}
				}
				this.m_border = null;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x000BC158 File Offset: 0x000BB158
		public IChartBorder Border
		{
			get
			{
				for (;;)
				{
					IL_00:
					if (true)
					{
					}
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_71;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								this.m_border = new ChartBorder((spr\u2158)base.ReservedHandle, this);
								num = 0;
								continue;
							}
							break;
						}
						if (this.m_border != null)
						{
							goto IL_73;
						}
						num = 2;
					}
				}
				IL_71:
				IL_73:
				return this.m_border;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x000BC1E8 File Offset: 0x000BB1E8
		public IChartInterior Interior
		{
			get
			{
				for (;;)
				{
					IL_00:
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_71;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								this.ᜂ = new ChartInterior((spr\u2158)base.ReservedHandle, this);
								if (true)
								{
								}
								num = 0;
								continue;
							}
							break;
						}
						if (this.ᜂ != null)
						{
							goto IL_73;
						}
						num = 1;
					}
				}
				IL_71:
				IL_73:
				return this.ᜂ;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x000BC278 File Offset: 0x000BB278
		public Format3D Format3D
		{
			get
			{
				for (;;)
				{
					IL_00:
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_6C;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								this.ᜁ = new Format3D(base.AppImplementation, this);
								num = 0;
								continue;
							}
							break;
						}
						if (true)
						{
						}
						if (this.ᜁ != null)
						{
							goto IL_6E;
						}
						num = 2;
					}
				}
				IL_6C:
				IL_6E:
				return this.ᜁ;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x000BC304 File Offset: 0x000BB304
		public IShapeFill Fill
		{
			get
			{
				for (;;)
				{
					IL_00:
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_69;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								this.ᜄ = new spr\u2436((spr\u2158)base.ReservedHandle, this);
								num = 1;
								continue;
							}
							break;
						}
						if (this.ᜄ != null)
						{
							goto IL_6B;
						}
						num = 2;
					}
				}
				IL_69:
				IL_6B:
				if (true)
				{
				}
				this.IsAutomaticFormat = false;
				return this.ᜄ;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x000BC39C File Offset: 0x000BB39C
		// (set) Token: 0x0600134F RID: 4943 RVA: 0x000BC3E4 File Offset: 0x000BB3E4
		public bool HasShadow
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
				return this.ᜃ != null;
			}
			internal set
			{
				if (value)
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
						break;
					}
					ChartShadow shadow = this.Shadow;
					return;
				}
				this.ᜃ = null;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x000BC434 File Offset: 0x000BB434
		// (set) Token: 0x06001351 RID: 4945 RVA: 0x000BC47C File Offset: 0x000BB47C
		public bool HasFormat3D
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
				return this.ᜁ != null;
			}
			internal set
			{
				if (true)
				{
				}
				if (value)
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
						break;
					}
					Format3D format3D = this.Format3D;
					return;
				}
				this.ᜁ = null;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x000BC4CC File Offset: 0x000BB4CC
		public ChartShadow Shadow
		{
			get
			{
				for (;;)
				{
					IL_00:
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							this.ᜃ = new ChartShadow(base.AppImplementation, this);
							num = 2;
							continue;
						case 2:
							goto IL_76;
						}
						if (this.ᜃ != null)
						{
							goto IL_78;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							num = 0;
							break;
						}
					}
				}
				IL_76:
				IL_78:
				return this.ᜃ;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x000BC558 File Offset: 0x000BB558
		// (set) Token: 0x06001354 RID: 4948 RVA: 0x000BC5A0 File Offset: 0x000BB5A0
		public RectangleStyleType RectangleStyle
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

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x000BC5E8 File Offset: 0x000BB5E8
		// (set) Token: 0x06001356 RID: 4950 RVA: 0x000BC630 File Offset: 0x000BB630
		public bool IsAutoSize
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
				return this.ᜀ.ᜀ();
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

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x000BC678 File Offset: 0x000BB678
		// (set) Token: 0x06001358 RID: 4952 RVA: 0x000BC6C0 File Offset: 0x000BB6C0
		public bool IsAutoPosition
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
				this.ᜀ.ᜀ(value);
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x000BC708 File Offset: 0x000BB708
		// (set) Token: 0x0600135A RID: 4954 RVA: 0x000BC750 File Offset: 0x000BB750
		public bool IsBorderCornersRound
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
				return this.Interior.SwapColorsOnNegative;
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
				this.Interior.SwapColorsOnNegative = value;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x000BC798 File Offset: 0x000BB798
		public ChartBorder LineProperties
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
				return this.Border as ChartBorder;
			}
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x000BC7E0 File Offset: 0x000BB7E0
		internal static ExcelColors ᜀ(ExcelColors A_0)
		{
			if (A_0 < ExcelColors.Color0)
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
					break;
				}
				return A_0 + 8;
			}
			return A_0;
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x000BC828 File Offset: 0x000BB828
		public void Clear()
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
			this.SetDefaultValues(false, false);
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x000BC86C File Offset: 0x000BB86C
		// (set) Token: 0x0600135F RID: 4959 RVA: 0x000BC8C4 File Offset: 0x000BB8C4
		public ExcelColors ForeGroundKnownColor
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
				return (this.Interior as XlsChartInterior).ForegroundColorObject.ᜂ(this.Workbook);
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
				(this.Interior as XlsChartInterior).ForegroundColorObject.SetKnownColor(value);
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x000BC918 File Offset: 0x000BB918
		// (set) Token: 0x06001361 RID: 4961 RVA: 0x000BC970 File Offset: 0x000BB970
		public Color ForeGroundColor
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
				return (this.Interior as XlsChartInterior).ForegroundColorObject.ᜁ(this.Workbook);
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
				(this.Interior as XlsChartInterior).ForegroundColorObject.ᜀ(value);
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x000BC9C4 File Offset: 0x000BB9C4
		public OColor ForeGroundColorObject
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
				return (this.Interior as XlsChartInterior).ForegroundColorObject;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x000BCA10 File Offset: 0x000BBA10
		// (set) Token: 0x06001364 RID: 4964 RVA: 0x000BCA68 File Offset: 0x000BBA68
		public ExcelColors BackGroundKnownColor
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
				return (this.Interior as XlsChartInterior).BackgroundColorObject.ᜂ(this.Workbook);
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
				(this.Interior as XlsChartInterior).BackgroundColorObject.SetKnownColor(value);
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x000BCABC File Offset: 0x000BBABC
		// (set) Token: 0x06001366 RID: 4966 RVA: 0x000BCB14 File Offset: 0x000BBB14
		public Color BackGroundColor
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
				return (this.Interior as XlsChartInterior).BackgroundColorObject.ᜁ(this.Workbook);
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
				(this.Interior as XlsChartInterior).BackgroundColorObject.ᜀ(value);
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x000BCB68 File Offset: 0x000BBB68
		public OColor BackGroundColorObject
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
				return (this.Interior as XlsChartInterior).BackgroundColorObject;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x000BCBB4 File Offset: 0x000BBBB4
		// (set) Token: 0x06001369 RID: 4969 RVA: 0x000BCBFC File Offset: 0x000BBBFC
		public ExcelPatternType Pattern
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
				return this.Interior.Pattern;
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
				this.Interior.Pattern = value;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x000BCC44 File Offset: 0x000BBC44
		// (set) Token: 0x0600136B RID: 4971 RVA: 0x000BCC8C File Offset: 0x000BBC8C
		public bool IsAutomaticFormat
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
				return this.Interior.UseDefaultFormat;
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
				this.Interior.UseDefaultFormat = value;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x000BCCD4 File Offset: 0x000BBCD4
		// (set) Token: 0x0600136D RID: 4973 RVA: 0x000BCD20 File Offset: 0x000BBD20
		public bool Visible
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
				return this.Interior.Pattern != ExcelPatternType.None;
			}
			set
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_27;
						default:
							goto IL_65;
						}
						break;
					case 1:
						if (this.Interior.Pattern == ExcelPatternType.None)
						{
							if (true)
							{
							}
							num = 2;
							continue;
						}
						return;
					case 2:
						goto IL_A2;
					case 4:
						num = 1;
						continue;
					}
					goto IL_24;
					IL_27:
					num = 4;
					continue;
					IL_24:
					if (value)
					{
						goto IL_27;
					}
					this.Interior.Pattern = ExcelPatternType.None;
					num = 0;
				}
				IL_65:
				if (false)
				{
				}
				return;
				IL_A2:
				this.Interior.Pattern = ExcelPatternType.Solid;
			}
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x000BCDD4 File Offset: 0x000BBDD4
		public XlsChartFrameFormat Clone(object parent)
		{
			XlsChartFrameFormat xlsChartFrameFormat;
			for (;;)
			{
				xlsChartFrameFormat = (XlsChartFrameFormat)base.MemberwiseClone();
				xlsChartFrameFormat.SetParent(parent);
				xlsChartFrameFormat.ᜀ();
				xlsChartFrameFormat.m_bIsDisposed = this.m_bIsDisposed;
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.m_border != null)
						{
							num = 2;
							continue;
						}
						goto IL_B8;
					case 1:
						goto IL_D8;
					case 2:
						xlsChartFrameFormat.m_border = this.m_border.Clone(xlsChartFrameFormat);
						num = 9;
						continue;
					case 3:
						goto IL_147;
					case 4:
						xlsChartFrameFormat.ᜂ = this.ᜂ.Clone(xlsChartFrameFormat);
						num = 3;
						continue;
					case 5:
						xlsChartFrameFormat.ᜄ = (spr\u2436)this.ᜄ.Clone(xlsChartFrameFormat);
						num = 7;
						continue;
					case 6:
						if (this.ᜀ != null)
						{
							num = 11;
							continue;
						}
						goto IL_D8;
					case 7:
						return xlsChartFrameFormat;
					case 8:
						if (this.ᜂ != null)
						{
							num = 4;
							continue;
						}
						goto IL_147;
					case 9:
						goto IL_184;
					case 10:
						if (this.ᜄ != null)
						{
							num = 5;
							continue;
						}
						return xlsChartFrameFormat;
					case 11:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_184;
						default:
							if (false)
							{
							}
							xlsChartFrameFormat.ᜀ = (sprᳫ)this.ᜀ.Clone();
							num = 1;
							continue;
						}
						break;
					}
					break;
					IL_B8:
					num = 8;
					continue;
					IL_184:
					goto IL_B8;
					IL_D8:
					if (true)
					{
					}
					num = 0;
					continue;
					IL_147:
					num = 10;
				}
			}
			return xlsChartFrameFormat;
		}

		// Token: 0x04000E95 RID: 3733
		private sprᳫ ᜀ;

		// Token: 0x04000E96 RID: 3734
		private string[] \u2593\u00AC\u00A1\u00A4;

		// Token: 0x04000E97 RID: 3735
		protected XlsChartBorder m_border;

		// Token: 0x04000E98 RID: 3736
		private Format3D ᜁ;

		// Token: 0x04000E99 RID: 3737
		internal XlsChartInterior ᜂ;

		// Token: 0x04000E9A RID: 3738
		private ChartShadow ᜃ;

		// Token: 0x04000E9B RID: 3739
		private bool[] \u2593\u0098\u009D\u0093;

		// Token: 0x04000E9C RID: 3740
		private spr\u2436 ᜄ;

		// Token: 0x04000E9D RID: 3741
		private bool \u2460\u0086\u008C\u0084;

		// Token: 0x04000E9E RID: 3742
		protected XlsChart m_chart;

		// Token: 0x04000E9F RID: 3743
		private bool[] \u2609\u008F\u00A5\u00AC;

		// Token: 0x04000EA0 RID: 3744
		private string[] \u25D8\u008B\u00A0\u00A9;

		// Token: 0x04000EA1 RID: 3745
		private Stream ᜅ;
	}
}
