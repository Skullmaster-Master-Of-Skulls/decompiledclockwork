using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using Spire.Xls.Collections;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;
using Spire.Xls.Core.Spreadsheet.Shapes;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x020001B6 RID: 438
	public abstract class XlsWorksheetBase : XlsObject, INamedObject, spr\u1D46, ITabSheet, ICloneParent
	{
		// Token: 0x06001783 RID: 6019 RVA: 0x000E23A0 File Offset: 0x000E13A0
		internal XlsWorksheetBase(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.FindParents();
			this.InitializeCollections();
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x000E2434 File Offset: 0x000E1434
		internal XlsWorksheetBase(spr\u1DF5 A_0, object A_1, sprἛ A_2, ExcelParseOptions A_3, bool A_4, Dictionary<int, int> A_5, IDecryptor A_6) : this(A_0, A_1)
		{
			this.KeepRecord = true;
			this.ᜀ(A_2, A_3, A_4, A_5, A_6);
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x000E2460 File Offset: 0x000E1460
		// (set) Token: 0x06001786 RID: 6022 RVA: 0x000E24A4 File Offset: 0x000E14A4
		public string Name
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
				return this.ᜉ;
			}
			set
			{
				int a_ = 12;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_14B;
					case 1:
					{
						int length;
						if (value[length - 1] == '\'')
						{
							num = 0;
							continue;
						}
						num = 9;
						continue;
					}
					case 2:
						IL_11:
						break;
					case 3:
						goto IL_126;
					case 4:
						if (value[0] != '\'')
						{
							num = 8;
							continue;
						}
						goto IL_98;
					case 5:
					{
						int length = value.Length;
						num = 4;
						continue;
					}
					case 6:
						if (true)
						{
						}
						goto IL_D6;
					case 7:
						value = value.Substring(0, 31);
						value = this.ᜀ(new XlsWorksheetBase.ᜀ(this.ᜀ), value);
						num = 6;
						continue;
					case 8:
						num = 1;
						continue;
					case 9:
						if (value.Length > 31)
						{
							num = 7;
							continue;
						}
						goto IL_D6;
					}
					if (value != this.ᜉ)
					{
						num = 5;
						continue;
					}
					return;
					IL_D6:
					XlsEventArgs args = new XlsEventArgs(this.ᜉ, value, RecordTableEnumerator.b("ు╃⭅ⵇ", a_));
					this.ᜉ = value;
					this.OnNameChanged(args);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_11;
					default:
						if (false)
						{
						}
						num = 3;
						break;
					}
				}
				IL_98:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᙁⱃ⍅桇⭉㱋⅍⍏♑♓㥕⡗㉙㥛繝͟͡੣䙥٧թᡫ乭ቯ᝱味͵୷ό᡻幽ꒃﾋ揄낏﶑뚕ﮙ肟송첣장\udaa7쮩쾫\udaad햯삱钳\ud9b5\udeb7骹좻횽ꖿ돃꧅뫇ꇉ뿋ꛍ뗏럑ꃓꯗ龎닛뿝跟蟡쫣", a_));
				IL_126:
				return;
				IL_14B:
				goto IL_98;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x000E262C File Offset: 0x000E162C
		// (set) Token: 0x06001788 RID: 6024 RVA: 0x000E2670 File Offset: 0x000E1670
		public bool IsSaved
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
				return !this.ᜊ;
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
				this.ᜊ = !value;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x000E26B8 File Offset: 0x000E16B8
		protected internal XlsCommentsCollection InnerComments
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
				return this.ᜏ.ᜌ();
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x000E2700 File Offset: 0x000E1700
		protected internal XlsPicturesCollection InnerPictures
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
							goto IL_7B;
						case 1:
							this.ᜑ = new PicturesCollection((spr\u2158)base.ReservedHandle, this);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							}
							if (false)
							{
							}
							num = 0;
							continue;
						}
						if (true)
						{
						}
						if (this.ᜑ != null)
						{
							goto IL_7D;
						}
						num = 1;
					}
				}
				IL_7B:
				IL_7D:
				return this.ᜑ;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x000E2790 File Offset: 0x000E1790
		protected internal XlsWorksheetChartsCollection InnerCharts
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
							this.ᜐ = new WorksheetChartsCollection((spr\u2158)base.ReservedHandle, this);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							}
							if (false)
							{
							}
							num = 2;
							continue;
						case 2:
							goto IL_7B;
						}
						if (true)
						{
						}
						if (this.ᜐ != null)
						{
							goto IL_7D;
						}
						num = 0;
					}
				}
				IL_7B:
				IL_7D:
				return this.ᜐ;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x000E2820 File Offset: 0x000E1820
		internal spr\u1D9B InnerShapes
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
				return this.ᜏ;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x000E2864 File Offset: 0x000E1864
		protected internal IShapes Shapes
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
				return this.ᜏ;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x000E28A8 File Offset: 0x000E18A8
		// (set) Token: 0x0600178F RID: 6031 RVA: 0x000E28EC File Offset: 0x000E18EC
		protected internal ShapeCollectionBase InnerShapesBase
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
			internal set
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
				this.ᜏ = (spr\u22F9)value;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x000E2934 File Offset: 0x000E1934
		// (set) Token: 0x06001791 RID: 6033 RVA: 0x000E2978 File Offset: 0x000E1978
		internal XlsHeaderFooterShapeCollection InnerHeaderFooterShapes
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
				return this.\u171B;
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
				this.\u171B = value;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x000E29BC File Offset: 0x000E19BC
		public XlsHeaderFooterShapeCollection HeaderFooterShapes
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
							goto IL_7B;
						case 1:
							this.\u171B = new XlsHeaderFooterShapeCollection((spr\u2158)base.ReservedHandle, this);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							}
							if (false)
							{
							}
							num = 0;
							continue;
						}
						if (true)
						{
						}
						if (this.\u171B != null)
						{
							goto IL_7D;
						}
						num = 1;
					}
				}
				IL_7B:
				IL_7D:
				return this.\u171B;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x000E2A4C File Offset: 0x000E1A4C
		public IComments Comments
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
				return this.ᜏ.ᜅ();
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x000E2A94 File Offset: 0x000E1A94
		public IChartShapes Charts
		{
			get
			{
				for (;;)
				{
					IL_00:
					if (true)
					{
					}
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_7B;
						case 1:
							this.ᜐ = new WorksheetChartsCollection((spr\u2158)base.ReservedHandle, this);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							}
							if (false)
							{
							}
							num = 0;
							continue;
						}
						if (this.ᜐ != null)
						{
							goto IL_7D;
						}
						num = 1;
					}
				}
				IL_7B:
				IL_7D:
				return this.ᜐ;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x000E2B24 File Offset: 0x000E1B24
		public IPictures Pictures
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
							goto IL_7B;
						case 2:
							this.ᜑ = new PicturesCollection(base.ReservedHandle as spr\u2158, this);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								num = 1;
								continue;
							}
							break;
						}
						if (this.ᜑ != null)
						{
							goto IL_7D;
						}
						num = 2;
					}
				}
				IL_7B:
				IL_7D:
				return this.ᜑ;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x000E2BB4 File Offset: 0x000E1BB4
		// (set) Token: 0x06001797 RID: 6039 RVA: 0x000E2C08 File Offset: 0x000E1C08
		public string CodeName
		{
			get
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
					if (this.m_strCodeName != null)
					{
						return this.m_strCodeName;
					}
					break;
				}
				return this.ᜉ;
			}
			internal set
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
				this.m_strCodeName = value;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x000E2C4C File Offset: 0x000E1C4C
		internal sprṫ WindowTwo
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_57;
					case 2:
						this.\u1719.ᜁ(10);
						num = 1;
						continue;
					case 3:
						this.\u1719 = (sprṫ)spr\u175E.ᜀ(TBIFFRecord.WindowTwo);
						num = 4;
						continue;
					case 4:
						goto IL_59;
					case 5:
						if (this.BOF.ᜉ() != sprḯ.TType.TYPE_CHART)
						{
							goto IL_C5;
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
							num = 2;
							continue;
						}
						break;
					}
					IL_28:
					if (true)
					{
					}
					if (this.\u1719 == null)
					{
						num = 3;
						continue;
					}
					goto IL_59;
					goto IL_28;
					IL_59:
					num = 5;
				}
				IL_57:
				IL_C5:
				return this.\u1719;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06001799 RID: 6041 RVA: 0x000E2D24 File Offset: 0x000E1D24
		// (set) Token: 0x0600179A RID: 6042 RVA: 0x000E2D74 File Offset: 0x000E1D74
		public virtual bool ProtectContents
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
				return (this.InnerProtection & SheetProtectionType.Content) != SheetProtectionType.None;
			}
			internal set
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
					if (!value)
					{
						this.InnerProtection &= ~SheetProtectionType.Content;
						return;
					}
					break;
				}
				if (true)
				{
				}
				this.InnerProtection |= SheetProtectionType.Content;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x000E2DDC File Offset: 0x000E1DDC
		public virtual bool ProtectDrawingObjects
		{
			get
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
					if (!this.ProtectContents)
					{
						return false;
					}
					break;
				}
				return (this.InnerProtection & SheetProtectionType.Objects) == SheetProtectionType.None;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x000E2E30 File Offset: 0x000E1E30
		public virtual bool ProtectScenarios
		{
			get
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
					if (!this.ProtectContents)
					{
						return false;
					}
					break;
				}
				if (true)
				{
				}
				return (this.InnerProtection & SheetProtectionType.Scenarios) == SheetProtectionType.None;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x000E2E84 File Offset: 0x000E1E84
		public bool IsPasswordProtected
		{
			get
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
					if (this.\u1715 == null)
					{
						if (true)
						{
						}
						return false;
					}
					break;
				}
				return this.\u1715.ᜀ() != 0;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x000E2EDC File Offset: 0x000E1EDC
		// (set) Token: 0x0600179F RID: 6047 RVA: 0x000E2F20 File Offset: 0x000E1F20
		public bool IsParsed
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
				return this.\u1716;
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
				this.\u1716 = value;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x000E2F64 File Offset: 0x000E1F64
		// (set) Token: 0x060017A1 RID: 6049 RVA: 0x000E2FA8 File Offset: 0x000E1FA8
		public bool IsParsing
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
				return this.\u1717;
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
				this.\u1717 = value;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x000E2FEC File Offset: 0x000E1FEC
		public bool IsSkipParsing
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
				return this.\u1718;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x000E3030 File Offset: 0x000E2030
		// (set) Token: 0x060017A4 RID: 6052 RVA: 0x000E3074 File Offset: 0x000E2074
		public bool IsSupported
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
				return this.\u1712;
			}
			protected set
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
				this.\u1712 = value;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x000E30B8 File Offset: 0x000E20B8
		public XlsWorkbook ParentWorkbook
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
				return this.m_book;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x000E30FC File Offset: 0x000E20FC
		// (set) Token: 0x060017A7 RID: 6055 RVA: 0x000E3144 File Offset: 0x000E2144
		public virtual int FirstRow
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
				this.ParseData();
				return this.m_iFirstRow;
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
				this.m_iFirstRow = value;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x000E3188 File Offset: 0x000E2188
		// (set) Token: 0x060017A9 RID: 6057 RVA: 0x000E31D0 File Offset: 0x000E21D0
		[CLSCompliant(false)]
		public virtual int FirstColumn
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
				this.ParseData();
				return this.m_iFirstColumn;
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
				this.m_iFirstColumn = value;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x000E3214 File Offset: 0x000E2214
		// (set) Token: 0x060017AB RID: 6059 RVA: 0x000E325C File Offset: 0x000E225C
		public virtual int LastRow
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
				this.ParseData();
				return this.m_iLastRow;
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
				this.m_iLastRow = value;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x000E32A0 File Offset: 0x000E22A0
		// (set) Token: 0x060017AD RID: 6061 RVA: 0x000E32E8 File Offset: 0x000E22E8
		[CLSCompliant(false)]
		public int LastColumn
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
				this.ParseData();
				return this.m_iLastColumn;
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
				this.m_iLastColumn = value;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x000E332C File Offset: 0x000E232C
		// (set) Token: 0x060017AF RID: 6063 RVA: 0x000E337C File Offset: 0x000E237C
		public virtual int Zoom
		{
			get
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
					if (this.\u1713 == 0)
					{
						return 100;
					}
					break;
				}
				return this.\u1713;
			}
			set
			{
				int a_ = 5;
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
							goto IL_A3;
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
						break;
					case 1:
						goto IL_A3;
					case 2:
						if (value > 400)
						{
							num = 1;
							continue;
						}
						goto IL_A5;
					}
					if (value < 10)
					{
						break;
					}
					num = 0;
				}
				IL_66:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("愺刼倾ⱀ", a_), RecordTableEnumerator.b("愺刼倾ⱀ捂⡄㉆㩈㽊浌ⵎ㑐獒㝔㉖ⵘⱚ㡜㩞འ䍢呤坦䥨੪ͬ୮兰䝲䕴䝶坸", a_));
				IL_A3:
				goto IL_66;
				IL_A5:
				this.\u1713 = value;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x000E3438 File Offset: 0x000E2438
		public virtual OColor TabColorObject
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_50;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						this.\u171A = new OColor((ExcelColors)(-1));
						num = 2;
						continue;
					case 2:
						goto IL_76;
					}
					goto IL_38;
					IL_50:
					if (true)
					{
					}
					num = 1;
					continue;
					IL_38:
					if (this.\u171A == null)
					{
						goto IL_50;
					}
					break;
				}
				IL_76:
				return this.\u171A;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x000E34C4 File Offset: 0x000E24C4
		// (set) Token: 0x060017B2 RID: 6066 RVA: 0x000E3524 File Offset: 0x000E2524
		public virtual ExcelColors TabKnownColor
		{
			get
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
					if (this.\u171A != null)
					{
						return this.\u171A.ᜂ(this.m_book);
					}
					break;
				}
				return (ExcelColors)(-1);
			}
			set
			{
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_58;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						this.\u171A = new OColor(ExcelColors.Black);
						num = 2;
						continue;
					case 2:
						goto IL_76;
					}
					goto IL_4A;
					IL_58:
					num = 1;
					continue;
					IL_4A:
					if (this.\u171A == null)
					{
						goto IL_58;
					}
					break;
				}
				IL_76:
				this.\u171A.SetKnownColor(value);
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x000E35B8 File Offset: 0x000E25B8
		// (set) Token: 0x060017B4 RID: 6068 RVA: 0x000E361C File Offset: 0x000E261C
		public virtual Color TabColor
		{
			get
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
					if (this.\u171A != null)
					{
						return this.\u171A.ᜁ(this.m_book);
					}
					break;
				}
				return XlsWorksheetBase.ᜈ;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u171A = new OColor(ExcelColors.Black);
						num = 1;
						continue;
					case 1:
						goto IL_76;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_58;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							break;
						}
						break;
					}
					goto IL_4A;
					IL_58:
					num = 0;
					continue;
					IL_4A:
					if (this.\u171A == null)
					{
						goto IL_58;
					}
					break;
				}
				IL_76:
				this.\u171A.ᜀ(value, this.m_book);
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x000E36B4 File Offset: 0x000E26B4
		public IWorkbook Workbook
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
				return this.m_book;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x000E36F8 File Offset: 0x000E26F8
		// (set) Token: 0x060017B7 RID: 6071 RVA: 0x000E3740 File Offset: 0x000E2740
		public ExcelColors GridLineColor
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
				return (ExcelColors)this.\u1719.ᜆ();
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
				this.WindowTwo.ᜃ(false);
				this.WindowTwo.ᜀ((int)value);
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x000E3794 File Offset: 0x000E2794
		// (set) Token: 0x060017B9 RID: 6073 RVA: 0x000E37DC File Offset: 0x000E27DC
		public bool DefaultGridlineColor
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
				return this.WindowTwo.ᜅ();
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
				this.WindowTwo.ᜃ(value);
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x000E3824 File Offset: 0x000E2824
		// (set) Token: 0x060017BB RID: 6075 RVA: 0x000E386C File Offset: 0x000E286C
		public bool IsRightToLeft
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
				return this.WindowTwo.ᜊ();
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
				this.WindowTwo.ᜋ(value);
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x060017BC RID: 6076
		internal abstract XlsPageSetupBase PageSetupBase { get; }

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x000E38B4 File Offset: 0x000E28B4
		public bool IsSelected
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
				return this.WindowTwo.\u1712();
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x000E38FC File Offset: 0x000E28FC
		// (set) Token: 0x060017BF RID: 6079 RVA: 0x000E3940 File Offset: 0x000E2940
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
				return this.\u171C;
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
				this.\u171C = value;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x000E3984 File Offset: 0x000E2984
		public virtual SheetProtectionType Protection
		{
			get
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
					if (this.\u1714 != null)
					{
						if (true)
						{
						}
						return (SheetProtectionType)this.\u1714.ᜁ();
					}
					break;
				}
				return SheetProtectionType.None;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x000E39D8 File Offset: 0x000E29D8
		// (set) Token: 0x060017C2 RID: 6082 RVA: 0x000E3A30 File Offset: 0x000E2A30
		protected internal virtual SheetProtectionType InnerProtection
		{
			get
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
					if (this.\u1714 != null)
					{
						return (SheetProtectionType)this.\u1714.ᜁ();
					}
					break;
				}
				if (true)
				{
				}
				return this.UnprotectedOptions;
			}
			internal set
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
				this.\u1714.ᜀ((int)value);
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x000E3A78 File Offset: 0x000E2A78
		protected virtual SheetProtectionType UnprotectedOptions
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
				return SheetProtectionType.None;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x000E3AB4 File Offset: 0x000E2AB4
		internal sprḯ BOF
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
				return this.\u171E;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x000E3AF8 File Offset: 0x000E2AF8
		// (set) Token: 0x060017C6 RID: 6086 RVA: 0x000E3B3C File Offset: 0x000E2B3C
		public WorksheetVisibility Visibility
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
				return this.\u171F;
			}
			set
			{
				int a_ = 15;
				switch (0)
				{
				default:
				{
					int num = 10;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							int num2;
							int count;
							if (num2 >= count)
							{
								num = 13;
								continue;
							}
							num = 14;
							continue;
						}
						case 1:
							goto IL_F8;
						case 2:
						{
							XlsWorkbookObjectsCollection objects;
							int num3;
							if (this.ᜀ(objects, num3))
							{
								num = 12;
								continue;
							}
							if (true)
							{
							}
							num3--;
							num = 11;
							continue;
						}
						case 3:
							goto IL_F6;
						case 4:
							this.\u171F = value;
							num = 8;
							continue;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_F8;
							default:
							{
								if (false)
								{
								}
								int realIndex = this.RealIndex;
								XlsWorkbookObjectsCollection objects = this.m_book.Objects;
								int num2 = realIndex + 1;
								int count = objects.Count;
								num = 6;
								continue;
							}
							}
							break;
						case 6:
							goto IL_8C;
						case 7:
							if (value != WorksheetVisibility.Visible)
							{
								num = 5;
								continue;
							}
							return;
						case 8:
							if (!this.m_book.Loading)
							{
								num = 1;
								continue;
							}
							return;
						case 9:
							return;
						case 11:
							goto IL_D9;
						case 12:
							return;
						case 13:
						{
							int realIndex;
							int num3 = realIndex - 1;
							num = 17;
							continue;
						}
						case 14:
						{
							int num2;
							XlsWorkbookObjectsCollection objects;
							if (this.ᜀ(objects, num2))
							{
								num = 9;
								continue;
							}
							num2++;
							num = 15;
							continue;
						}
						case 15:
							goto IL_8C;
						case 16:
						{
							int num3;
							if (num3 < 0)
							{
								num = 3;
								continue;
							}
							num = 2;
							continue;
						}
						case 17:
							goto IL_D9;
						}
						if (this.Visibility != value)
						{
							num = 4;
							continue;
						}
						return;
						IL_8C:
						num = 0;
						continue;
						IL_D9:
						num = 16;
						continue;
						IL_F8:
						num = 7;
					}
					IL_F6:
					throw new NotSupportedException(RecordTableEnumerator.b("ф杆㹈⑊㽌⑎㍐㱒㩔㱖祘㙚⡜ⱞᕠ䍢٤ࡦݨὪ౬ٮὰ卲ᑴͶ奸᝺᡼Ṿꖄ권年ﲔﺚ붜캠톢캤풦솨캪좬\udbae龰", a_));
				}
				}
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x000E3D68 File Offset: 0x000E2D68
		// (set) Token: 0x060017C8 RID: 6088 RVA: 0x000E3DAC File Offset: 0x000E2DAC
		internal sprᡟ DataHolder
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
				return this.ᜠ;
			}
			set
			{
				if (true)
				{
				}
				for (;;)
				{
					this.ᜠ = value;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (value != null)
							{
								goto IL_2E;
							}
							goto IL_49;
						case 1:
							this.\u1716 = false;
							num = 2;
							continue;
						case 2:
							goto IL_49;
						}
						break;
						IL_2E:
						num = 1;
						continue;
						IL_49:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2E;
						default:
							goto IL_5F;
						}
					}
				}
				IL_5F:
				if (false)
				{
				}
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x000E3E28 File Offset: 0x000E2E28
		// (set) Token: 0x060017CA RID: 6090 RVA: 0x000E3E70 File Offset: 0x000E2E70
		public int TopVisibleRow
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
				return (int)(this.WindowTwo.ᜐ() + 1);
			}
			set
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
					if (value > 0)
					{
						this.WindowTwo.ᜃ((ushort)(value - 1));
						return;
					}
					break;
				}
				if (true)
				{
				}
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x000E3EC8 File Offset: 0x000E2EC8
		// (set) Token: 0x060017CC RID: 6092 RVA: 0x000E3F10 File Offset: 0x000E2F10
		public int LeftVisibleColumn
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
				return (int)(this.WindowTwo.ᜌ() + 1);
			}
			set
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
					if (value > 0)
					{
						this.WindowTwo.ᜀ((ushort)(value - 1));
						return;
					}
					break;
				}
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x060017CD RID: 6093 RVA: 0x000E3F68 File Offset: 0x000E2F68
		internal spr\u24C3 Password
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
				return this.\u1715;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x000E3FAC File Offset: 0x000E2FAC
		// (set) Token: 0x060017CF RID: 6095 RVA: 0x000E3FF0 File Offset: 0x000E2FF0
		public bool UnknownVmlShapes
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
				return this.ᜡ;
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
				this.ᜡ = value;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x000E4034 File Offset: 0x000E3034
		public TextBoxCollection TypedTextBoxes
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6E;
					case 1:
						this.ᜢ = new TextBoxCollection(base.AppImplementation, this);
						num = 0;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4A;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					goto IL_38;
					IL_4A:
					num = 1;
					continue;
					IL_38:
					if (this.ᜢ == null)
					{
						goto IL_4A;
					}
					break;
				}
				IL_6E:
				if (true)
				{
				}
				return this.ᜢ;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x060017D1 RID: 6097 RVA: 0x000E40C0 File Offset: 0x000E30C0
		internal TextBoxCollection InnerTextBoxes
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
				return this.ᜢ;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x000E4104 File Offset: 0x000E3104
		public ITextBoxes TextBoxes
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
				return this.TypedTextBoxes;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x000E4148 File Offset: 0x000E3148
		public CheckBoxCollection TypedCheckBoxes
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_52;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						this.ᜣ = new CheckBoxCollection(base.AppImplementation, this);
						num = 2;
						continue;
					case 2:
						goto IL_76;
					}
					goto IL_38;
					IL_52:
					num = 1;
					continue;
					IL_38:
					if (true)
					{
					}
					if (this.ᜣ == null)
					{
						goto IL_52;
					}
					break;
				}
				IL_76:
				return this.ᜣ;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x000E41D4 File Offset: 0x000E31D4
		internal RadioButtonCollection TypedOptionButtons
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_52;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						goto IL_76;
					case 2:
						this.ᜤ = new RadioButtonCollection(base.AppImplementation, this);
						num = 1;
						continue;
					}
					goto IL_4A;
					IL_52:
					num = 2;
					continue;
					IL_4A:
					if (this.ᜤ == null)
					{
						goto IL_52;
					}
					break;
				}
				IL_76:
				return this.ᜤ;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x000E4260 File Offset: 0x000E3260
		public ComboBoxCollection TypedComboBoxes
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜥ = new ComboBoxCollection(base.AppImplementation, this);
						num = 2;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_52;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 2:
						goto IL_76;
					}
					goto IL_38;
					IL_52:
					num = 0;
					continue;
					IL_38:
					if (true)
					{
					}
					if (this.ᜥ == null)
					{
						goto IL_52;
					}
					break;
				}
				IL_76:
				return this.ᜥ;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x000E42EC File Offset: 0x000E32EC
		protected internal CheckBoxCollection InnerCheckBoxes
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
				return this.ᜣ;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x000E4330 File Offset: 0x000E3330
		public ICheckBoxes CheckBoxes
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
				return this.TypedCheckBoxes;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x000E4374 File Offset: 0x000E3374
		public IRadioButtons RadioButtons
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
				return this.TypedOptionButtons;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x000E43B8 File Offset: 0x000E33B8
		public IComboBoxes ComboBoxes
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
				return this.TypedComboBoxes;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x000E43FC File Offset: 0x000E33FC
		public bool HasVmlShapes
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜣ != null)
						{
							num = 11;
							continue;
						}
						goto IL_14D;
					case 1:
						if (this.InnerComments != null)
						{
							num = 4;
							continue;
						}
						goto IL_60;
					case 3:
						goto IL_14B;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F6;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 5:
						if (this.InnerComments.Count <= 0)
						{
							num = 12;
							continue;
						}
						return true;
					case 6:
						if (this.ᜤ != null)
						{
							num = 8;
							continue;
						}
						goto IL_F6;
					case 7:
						if (this.ᜤ.Count <= 0)
						{
							num = 3;
							continue;
						}
						return true;
					case 8:
						if (true)
						{
						}
						num = 7;
						continue;
					case 9:
						num = 0;
						continue;
					case 10:
						goto IL_14D;
					case 11:
						num = 13;
						continue;
					case 12:
						goto IL_60;
					case 13:
						if (this.ᜣ.Count <= 0)
						{
							num = 10;
							continue;
						}
						return true;
					}
					if (!this.UnknownVmlShapes)
					{
						num = 9;
						continue;
					}
					return true;
					IL_60:
					num = 6;
					continue;
					IL_14D:
					num = 1;
				}
				IL_F6:
				return this.ᜃ();
				IL_14B:
				goto IL_F6;
			}
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x000E4580 File Offset: 0x000E3580
		private bool ᜃ()
		{
			for (;;)
			{
				int num = 0;
				int count = this.ᜏ.Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_7A;
					case 1:
						if (true)
						{
						}
						if ((this.ᜏ[num] as XlsShape).VmlShape)
						{
							num2 = 3;
							continue;
						}
						num++;
						num2 = 2;
						continue;
					case 2:
						goto IL_7A;
					case 3:
						return true;
					case 4:
						if (num >= count)
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
								num2 = 5;
								continue;
							}
						}
						num2 = 1;
						continue;
					case 5:
						return false;
					}
					break;
					IL_7A:
					num2 = 4;
				}
			}
			return true;
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x000E4648 File Offset: 0x000E3648
		public int VmlShapesCount
		{
			get
			{
				int num;
				for (;;)
				{
					num = 0;
					int num2 = 0;
					int count = this.ᜏ.Count;
					int num3 = 5;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							if (num2 >= count)
							{
								num3 = 4;
								continue;
							}
							num3 = 1;
							continue;
						case 1:
							IL_89:
							if ((this.ᜏ[num2] as XlsShape).VmlShape)
							{
								num3 = 3;
								continue;
							}
							goto IL_3E;
						case 2:
							if (true)
							{
							}
							goto IL_B8;
						case 3:
							num++;
							num3 = 6;
							continue;
						case 4:
							return num;
						case 5:
							goto IL_B8;
						case 6:
							goto IL_3E;
						}
						break;
						IL_3E:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_89;
						default:
							if (false)
							{
							}
							num2++;
							num3 = 2;
							continue;
						}
						IL_B8:
						num3 = 0;
					}
				}
				return num;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060017DD RID: 6109
		protected abstract SheetProtectionType DefaultProtectionOptions { get; }

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x000E472C File Offset: 0x000E372C
		private bool ProtectionMeaningDirect
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
				return (this.DefaultProtectionOptions & SheetProtectionType.Content) != SheetProtectionType.None;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x060017DF RID: 6111 RVA: 0x000E477C File Offset: 0x000E377C
		protected virtual bool ContainsProtection
		{
			get
			{
				if (this.\u1714 != null)
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
						return this.\u1714.ᜁ() != 17408;
					}
				}
				if (true)
				{
				}
				return false;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x000E47D8 File Offset: 0x000E37D8
		internal spr\u22A0 SheetProtection
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
				return this.\u1714;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x060017E1 RID: 6113 RVA: 0x000E481C File Offset: 0x000E381C
		// (set) Token: 0x060017E2 RID: 6114 RVA: 0x000E4860 File Offset: 0x000E3860
		public bool IsTransitionEvaluation
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
				return this.ᜦ;
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
				this.ᜦ = value;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x060017E3 RID: 6115 RVA: 0x000E48A4 File Offset: 0x000E38A4
		public bool HasPictures
		{
			get
			{
				if (true)
				{
				}
				if (this.ᜑ != null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return false;
					}
					if (false)
					{
					}
					return this.ᜑ.Count > 0;
				}
				return false;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x000E48FC File Offset: 0x000E38FC
		public int SheetId
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
				return this.Index;
			}
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x000E4940 File Offset: 0x000E3940
		internal void \u1739()
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
			this.ᜧ = null;
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x000E4984 File Offset: 0x000E3984
		private bool ᜀ(XlsWorkbookObjectsCollection A_0, int A_1)
		{
			XlsWorksheetBase xlsWorksheetBase = (XlsWorksheetBase)A_0[A_1];
			if (xlsWorksheetBase.Visibility == WorksheetVisibility.Visible)
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
					this.m_book.ActiveSheetIndex = A_1;
					this.m_book.DisplayedTab = A_1;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x000E49F4 File Offset: 0x000E39F4
		protected virtual void FindParents()
		{
			int a_ = 15;
			object obj = base.FindParent(typeof(XlsWorkbook));
			if (obj == null)
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
					throw new ApplicationException(RecordTableEnumerator.b("ᕄ♆㭈⹊⍌㭎煐㱒㝔㵖㱘㡚⥜罞ɠɢ୤०٨Ὢ䵬൮ᑰ卲፴ᡶ౸ᕺ᥼兾", a_));
				}
			}
			this.m_book = (XlsWorkbook)obj;
			this.ᜋ = this.m_book.ObjectCount;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x000E4A80 File Offset: 0x000E3A80
		protected virtual void OnNameChanged(XlsEventArgs args)
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
			this.RaiseNameChangedEvent(args);
			this.SetChanged();
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x000E4AC8 File Offset: 0x000E3AC8
		protected void RaiseNameChangedEvent(XlsEventArgs args)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					this.ᜧ(this, args);
					if (true)
					{
					}
					num = 2;
					continue;
				case 2:
					return;
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
					if (this.ᜧ == null)
					{
						return;
					}
					break;
				}
				num = 1;
			}
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x000E4B48 File Offset: 0x000E3B48
		public void SetChanged()
		{
			if (true)
			{
			}
			if (this.m_book.Loading)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3E;
				}
				if (false)
				{
				}
				return;
			}
			IL_3E:
			this.m_book.Saved = false;
			this.IsSaved = false;
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x000E4BA8 File Offset: 0x000E3BA8
		protected virtual void InitializeCollections()
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
			spr\u2158 a_ = (spr\u2158)base.ReservedHandle;
			this.ᜏ = new spr\u22F9(a_, this);
			this.ᜑ = new PicturesCollection(a_, this);
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x000E4C0C File Offset: 0x000E3C0C
		internal virtual void ClearAll(WorksheetCopyType flags)
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_64;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_64;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						this.\u170D.Clear();
						num = 3;
						continue;
					}
					break;
				case 2:
					if (this.ᜑ != null)
					{
						num = 0;
						continue;
					}
					return;
				case 3:
					goto IL_CF;
				case 4:
					goto IL_49;
				case 5:
					this.ᜐ.Clear();
					num = 4;
					continue;
				case 6:
					if (this.ᜐ != null)
					{
						num = 5;
						continue;
					}
					goto IL_49;
				case 8:
					return;
				}
				if (this.\u170D != null)
				{
					num = 1;
					continue;
				}
				goto IL_CF;
				IL_49:
				num = 2;
				continue;
				IL_64:
				this.ᜑ.Clear();
				num = 8;
				continue;
				IL_CF:
				this.ᜏ.Clear();
				num = 6;
			}
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x000E4D24 File Offset: 0x000E3D24
		public virtual void Activate()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 2:
					base.AppImplementation.ᜀ(this);
					this.m_book.SetActiveWorksheet(this);
					this.m_book.InnerWorksheetGroup.ᜁ(this);
					num = 0;
					continue;
				}
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
					if ((int)this.m_book.WindowOne.ᜊ() == this.RealIndex)
					{
						return;
					}
					break;
				}
				num = 2;
			}
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x000E4DD4 File Offset: 0x000E3DD4
		public virtual void Select()
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
			this.Activate();
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x000E4E18 File Offset: 0x000E3E18
		public void Unselect()
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
			this.Unselect(true);
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x000E4E5C File Offset: 0x000E3E5C
		public void Unselect(bool Check)
		{
			for (;;)
			{
				if (true)
				{
				}
				spr\u17B5 spr_u17B = this.m_book.WindowOne;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6A;
					case 1:
						return;
					case 2:
						if (this.WindowTwo.\u1712())
						{
							num = 10;
							continue;
						}
						return;
					case 3:
						if (Check)
						{
							num = 9;
							continue;
						}
						goto IL_AD;
					case 4:
						this.m_book.InnerWorksheetGroup.ᜀ(this);
						num = 1;
						continue;
					case 5:
						if (!Check)
						{
							num = 0;
							continue;
						}
						return;
					case 6:
						if (Check)
						{
							num = 4;
							continue;
						}
						return;
					case 7:
						if (spr_u17B.ᜆ() <= 1)
						{
							num = 8;
							continue;
						}
						goto IL_6A;
					case 8:
						goto IL_AD;
					case 9:
						num = 7;
						continue;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					}
					break;
					IL_6A:
					this.WindowTwo.ᜇ(false);
					spr\u17B5 spr_u17B2 = this.m_book.WindowOne;
					spr_u17B2.ᜁ(spr_u17B2.ᜆ() - 1);
					num = 6;
					continue;
					IL_AD:
					num = 5;
				}
			}
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x000E4FBC File Offset: 0x000E3FBC
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
			this.Protect(password, SheetProtectionType.LockedCells | SheetProtectionType.UnLockedCells);
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x000E5004 File Offset: 0x000E4004
		public void Protect(string password, SheetProtectionType options)
		{
			int a_ = 14;
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B8;
					default:
						goto IL_67;
					}
					break;
				case 1:
					goto IL_77;
				case 3:
					goto IL_DB;
				case 4:
					num = 1;
					continue;
				case 5:
					goto IL_B0;
				case 6:
					if (password.Length <= 0)
					{
						num = 4;
						continue;
					}
					num = 5;
					continue;
				case 7:
					if (password == null)
					{
						num = 3;
						continue;
					}
					num = 6;
					continue;
				}
				if (this.IsPasswordProtected)
				{
					num = 0;
					continue;
				}
				IL_B8:
				num = 7;
			}
			IL_67:
			if (false)
			{
			}
			throw new ApplicationException(RecordTableEnumerator.b("ፃ⥅㩇ⅉ㽋♍㕏㝑⁓癕し㭙⽛繝ɟݡţࡥ䡧ᩩṫŭѯ᝱ᝳɵᵷṹ剻", a_));
			IL_77:
			ushort num2 = 0;
			goto IL_101;
			IL_B0:
			num2 = XlsWorksheetBase.ᜀ(password);
			goto IL_101;
			IL_DB:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑃❅㭇㥉㭋⅍≏㙑", a_));
			IL_101:
			ushort a_2 = num2;
			this.ᜀ(a_2, options);
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x000E511C File Offset: 0x000E411C
		protected virtual SheetProtectionType PrepareProtectionOptions(SheetProtectionType options)
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
			return options;
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x000E5158 File Offset: 0x000E4158
		public void Unprotect()
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
			this.\u1715 = null;
			this.\u1714 = null;
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x000E51A4 File Offset: 0x000E41A4
		public void Unprotect(string password)
		{
			int a_ = 18;
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.\u1715.ᜀ() == XlsWorksheetBase.ᜀ(password))
					{
						num = 11;
						continue;
					}
					goto IL_178;
				case 1:
					if (password == null)
					{
						num = 3;
						continue;
					}
					num = 6;
					continue;
				case 2:
					goto IL_A3;
				case 3:
					goto IL_176;
				case 4:
					return;
				case 5:
					num = 7;
					continue;
				case 6:
					if (password.Length > 15)
					{
						num = 9;
						continue;
					}
					if (true)
					{
					}
					num = 8;
					continue;
				case 7:
					if (this.\u1714 == null)
					{
						num = 4;
						continue;
					}
					goto IL_13E;
				case 8:
					if (this.IsPasswordProtected)
					{
						num = 2;
						continue;
					}
					goto IL_11B;
				case 9:
					goto IL_A0;
				case 11:
					goto IL_CF;
				}
				if (this.\u1715 == null)
				{
					num = 5;
					continue;
				}
				goto IL_13E;
				IL_A3:
				num = 0;
				continue;
				IL_13E:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A3;
				default:
					if (false)
					{
					}
					num = 1;
					break;
				}
			}
			IL_A0:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ч⽉≋⥍⑏㩑瑓㥕㹗穙⡛㙝՟䉡ᑣݥ᭧ᥩ᭫ŭɯᙱ味յၷᕹॻችꊁꢇﶍ늑ﺕ聯벛", a_) + 15);
			IL_CF:
			IL_11B:
			this.\u1715 = null;
			this.\u1714 = null;
			return;
			IL_176:
			throw new ArgumentNullException(RecordTableEnumerator.b("㡇⭉㽋㵍❏㵑♓㉕", a_));
			IL_178:
			throw new ArgumentException(RecordTableEnumerator.b("Ň⑉㩋⽍㱏㭑こ癕⡗㭙⽛ⵝ᝟ൡᙣɥ", a_));
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x000E533C File Offset: 0x000E433C
		protected virtual void OnRealIndexChanged(int iOldIndex)
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

		// Token: 0x060017F7 RID: 6135 RVA: 0x000E5378 File Offset: 0x000E4378
		public void SelectTab()
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (true)
					{
					}
					this.WindowTwo.ᜇ(true);
					num = 0;
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
					if (this.WindowTwo.\u1712())
					{
						return;
					}
					break;
				}
				num = 1;
			}
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x000E53FC File Offset: 0x000E43FC
		public virtual void UpdateFormula(int iCurIndex, int iSourceIndex, Rectangle sourceRect, int iDestIndex, Rectangle destRect)
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
			this.ᜏ.ᜀ(iCurIndex, iSourceIndex, sourceRect, iDestIndex, destRect);
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x000E544C File Offset: 0x000E444C
		public virtual void UpdateExtendedFormatIndex(Dictionary<int, int> dictFormats)
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
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x000E5488 File Offset: 0x000E4488
		public virtual object Clone(object parent)
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
			return this.Clone(parent, true);
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x000E54CC File Offset: 0x000E44CC
		internal virtual object Clone(object parent, bool cloneShapes)
		{
			int a_ = 16;
			int num = 6;
			for (;;)
			{
				XlsWorksheetBase xlsWorksheetBase;
				switch (num)
				{
				case 0:
					xlsWorksheetBase.ᜑ = new PicturesCollection((spr\u2158)base.ReservedHandle, this);
					num = 4;
					continue;
				case 1:
					xlsWorksheetBase.ᜠ = this.ᜠ.ᜀ(xlsWorksheetBase.m_book.DataHolder);
					num = 7;
					continue;
				case 2:
					this.ᜁ(xlsWorksheetBase);
					num = 9;
					continue;
				case 3:
					if (this.ᜐ != null)
					{
						num = 13;
						continue;
					}
					goto IL_219;
				case 4:
					goto IL_1D2;
				case 5:
					if (this.ᜠ != null)
					{
						num = 1;
						continue;
					}
					return xlsWorksheetBase;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1D2;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 7:
					return xlsWorksheetBase;
				case 8:
					goto IL_78;
				case 9:
					goto IL_163;
				case 10:
					if (this.ᜑ != null)
					{
						num = 0;
						continue;
					}
					goto IL_1D2;
				case 11:
					goto IL_219;
				case 12:
					if (cloneShapes)
					{
						num = 2;
						continue;
					}
					goto IL_163;
				case 13:
					xlsWorksheetBase.ᜐ = new WorksheetChartsCollection((spr\u2158)base.ReservedHandle, this);
					num = 11;
					continue;
				}
				if (parent == null)
				{
					num = 8;
					continue;
				}
				xlsWorksheetBase = (XlsWorksheetBase)base.MemberwiseClone();
				xlsWorksheetBase.SetParent(parent);
				xlsWorksheetBase.FindParents();
				xlsWorksheetBase.\u1715 = (spr\u24C3)spr\u1CD3.ᜀ(this.\u1715);
				xlsWorksheetBase.\u1719 = (sprṫ)spr\u1CD3.ᜀ(this.\u1719);
				xlsWorksheetBase.\u171E = (sprḯ)spr\u1CD3.ᜀ(this.\u171E);
				xlsWorksheetBase.\u170D = spr\u1CD3.ᜀ(this.\u170D);
				xlsWorksheetBase.ᜎ = spr\u1CD3.ᜀ(this.ᜎ);
				num = 3;
				continue;
				IL_163:
				xlsWorksheetBase.ᜏ = (spr\u22F9)this.ᜏ.Clone(xlsWorksheetBase);
				xlsWorksheetBase.\u171B = (XlsHeaderFooterShapeCollection)spr\u1CD3.ᜀ(this.\u171B, xlsWorksheetBase);
				num = 5;
				continue;
				IL_1D2:
				num = 12;
				continue;
				IL_219:
				num = 10;
			}
			IL_78:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㙅⥇㡉⥋⁍⑏", a_));
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x000E573C File Offset: 0x000E473C
		internal void ᜁ(XlsWorksheetBase A_0)
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
			A_0.ᜏ = (spr\u22F9)this.ᜏ.Clone(A_0);
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x000E5790 File Offset: 0x000E4790
		protected internal virtual void UpdateStyleIndexes(int[] styleIndexes)
		{
			int a_ = 6;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (styleIndexes != null)
				{
					return;
				}
				break;
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("伻䨽㤿⹁⅃ཅ♇⹉⥋㙍㕏⅑", a_));
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x000E57F0 File Offset: 0x000E47F0
		internal void ᜀ(ushort A_0, SheetProtectionType A_1)
		{
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_5C;
				case 1:
					this.\u1715 = (spr\u24C3)spr\u175E.ᜀ(TBIFFRecord.Password);
					num = 0;
					continue;
				}
				IL_2E:
				if (this.\u1715 == null)
				{
					num = 1;
					continue;
				}
				IL_5C:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2E;
				default:
					goto IL_72;
				}
			}
			IL_72:
			if (false)
			{
			}
			A_1 = this.PrepareProtectionOptions(A_1);
			this.\u1715.ᜀ(A_0);
			this.\u1714 = (spr\u22A0)spr\u175E.ᜀ(TBIFFRecord.SheetProtection);
			this.\u1714.ᜀ((int)((ushort)A_1));
			this.\u1714.ᜀ(true);
		}

		// Token: 0x060017FF RID: 6143
		public abstract void MarkUsedReferences(bool[] usedItems);

		// Token: 0x06001800 RID: 6144
		public abstract void UpdateReferenceIndexes(int[] arrUpdatedIndexes);

		// Token: 0x06001801 RID: 6145 RVA: 0x000E58B8 File Offset: 0x000E48B8
		internal void ᜀ(sprἛ A_0, ExcelParseOptions A_1, bool A_2, Dictionary<int, int> A_3, IDecryptor A_4)
		{
			int a_ = 3;
			int num = 2;
			for (;;)
			{
				IL_13:
				int num2;
				switch (num)
				{
				case 0:
					goto IL_40;
				case 1:
					goto IL_42;
				case 3:
					while (num2 != 0)
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
							num = 6;
							goto IL_13;
						}
					}
					goto IL_DF;
				case 4:
					goto IL_DD;
				case 5:
					if (A_0.ᜂ())
					{
						num = 4;
						continue;
					}
					goto IL_42;
				case 6:
					num = 5;
					continue;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				this.PrepareVariables(A_1, A_2);
				num2 = 0;
				bool bSkipStyles = false;
				num = 1;
				continue;
				IL_42:
				num2 = this.ParseNextRecord(A_0, num2, A_1, bSkipStyles, A_3, A_4);
				num = 3;
			}
			IL_40:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
			IL_DD:
			IL_DF:
			if (true)
			{
			}
			this.PrepareProtection();
			this.\u1716 = false;
			this.\u1718 = A_2;
			this.IsSaved = true;
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x000E59C8 File Offset: 0x000E49C8
		protected void PrepareProtection()
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.\u171D != SheetProtectionType.None)
					{
						num = 2;
						continue;
					}
					return;
				case 1:
					num = 0;
					continue;
				case 2:
					num = 6;
					continue;
				case 3:
					if (true)
					{
					}
					this.\u1714 = (spr\u22A0)spr\u175E.ᜀ(TBIFFRecord.SheetProtection);
					this.\u1714.ᜀ((int)this.\u171D);
					this.\u1714.ᜀ(true);
					num = 5;
					continue;
				case 4:
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
					break;
				case 5:
					return;
				case 6:
					if (this.\u171D != (SheetProtectionType)(-1))
					{
						num = 3;
						continue;
					}
					return;
				}
				if (this.\u1714 != null)
				{
					break;
				}
				num = 1;
			}
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x000E5AC4 File Offset: 0x000E4AC4
		[CLSCompliant(false)]
		internal virtual int ParseNextRecord(sprἛ reader, int iBOFCounter, ExcelParseOptions options, bool bSkipStyles, Dictionary<int, int> hashNewXFormatIndexes, IDecryptor decryptor)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					BiffRecordRaw biffRecordRaw = reader.ᜀ(decryptor);
					int num = 79;
					for (;;)
					{
						spr\u22A0 spr_u22A;
						switch (num)
						{
						case 0:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.WindowZoom)
							{
								num = 67;
								continue;
							}
							this.ParseWindowZoom((spr\u1CF7)biffRecordRaw);
							num = 58;
							continue;
						}
						case 1:
							num = 84;
							continue;
						case 2:
							goto IL_1DF;
						case 3:
							if (spr_u22A.ᜀ())
							{
								num = 1;
								continue;
							}
							return iBOFCounter;
						case 4:
							if (!this.ProtectionMeaningDirect)
							{
								num = 30;
								continue;
							}
							return iBOFCounter;
						case 5:
							num = 2;
							continue;
						case 6:
							goto IL_1DF;
						case 7:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.MSODrawing)
							{
								num = 69;
								continue;
							}
							num = 22;
							continue;
						}
						case 8:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.ScenProtect)
							{
								num = 72;
								continue;
							}
							this.ᜀ((sprℷ)biffRecordRaw);
							num = 95;
							continue;
						}
						case 9:
							goto IL_1DF;
						case 10:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.SheetLayout)
							{
								num = 31;
								continue;
							}
							this.ᜀ((sprᲿ)biffRecordRaw);
							num = 41;
							continue;
						}
						case 11:
							return iBOFCounter;
						case 12:
							num = 55;
							continue;
						case 13:
							goto IL_4BA;
						case 14:
							return iBOFCounter;
						case 15:
						{
							sprḯ.TType ttype;
							this.\u1712 = (ttype == sprḯ.TType.TYPE_WORKSHEET || ttype == sprḯ.TType.TYPE_CHART);
							num = 59;
							continue;
						}
						case 16:
							iBOFCounter--;
							num = 13;
							continue;
						case 17:
						{
							TBIFFRecord typeCode;
							if (typeCode <= TBIFFRecord.ScenProtect)
							{
								num = 43;
								continue;
							}
							num = 24;
							continue;
						}
						case 18:
							if (this.KeepRecord)
							{
								num = 48;
								continue;
							}
							goto IL_8FC;
						case 19:
							goto IL_27F;
						case 20:
							if (!this.Workbook.IsCellProtection)
							{
								num = 33;
								continue;
							}
							return iBOFCounter;
						case 21:
							goto IL_774;
						case 22:
							if (!this.KeepRecord)
							{
								num = 65;
								continue;
							}
							goto IL_692;
						case 23:
							iBOFCounter++;
							num = 74;
							continue;
						case 24:
						{
							TBIFFRecord typeCode;
							if (typeCode <= TBIFFRecord.Dimensions)
							{
								num = 64;
								continue;
							}
							num = 85;
							continue;
						}
						case 25:
							if (!this.m_book.ᜀ(biffRecordRaw))
							{
								num = 14;
								continue;
							}
							goto IL_481;
						case 26:
							this.m_iMsoStartIndex = this.ᜎ.Count - 1;
							num = 21;
							continue;
						case 27:
							num = 78;
							continue;
						case 28:
							goto IL_6D3;
						case 29:
							num = 49;
							continue;
						case 30:
							num = 38;
							continue;
						case 31:
							num = 75;
							continue;
						case 32:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.CodeName)
							{
								num = 71;
								continue;
							}
							this.m_strCodeName = ((spr\u2384)biffRecordRaw).ᜀ();
							num = 42;
							continue;
						}
						case 33:
							this.Workbook.Protect(true, false);
							num = 40;
							continue;
						case 34:
							goto IL_607;
						case 35:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.WindowTwo)
							{
								num = 68;
								continue;
							}
							this.ParseWindowTwo((sprṫ)biffRecordRaw);
							num = 28;
							continue;
						}
						case 36:
						{
							TBIFFRecord typeCode = biffRecordRaw.TypeCode;
							num = 17;
							continue;
						}
						case 37:
							goto IL_7F3;
						case 38:
							if ((this.\u171D & SheetProtectionType.Content) == SheetProtectionType.None)
							{
								num = 46;
								continue;
							}
							return iBOFCounter;
						case 39:
							num = 88;
							continue;
						case 40:
							return iBOFCounter;
						case 41:
							goto IL_5BC;
						case 42:
							goto IL_41C;
						case 43:
							num = 57;
							continue;
						case 44:
						{
							this.\u171E = (sprḯ)biffRecordRaw;
							sprḯ.TType ttype = this.\u171E.ᜉ();
							num = 15;
							continue;
						}
						case 45:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_571;
							default:
								goto IL_657;
							}
							break;
						case 46:
							goto IL_1C7;
						case 47:
							if (this.m_iMsoStartIndex < 0)
							{
								num = 26;
								continue;
							}
							return iBOFCounter;
						case 48:
							this.ᜎ.Add(biffRecordRaw);
							num = 86;
							continue;
						case 49:
							goto IL_1DF;
						case 50:
						{
							bool flag;
							if (flag)
							{
								num = 73;
								continue;
							}
							return iBOFCounter;
						}
						case 51:
							num = 25;
							continue;
						case 52:
							goto IL_1DA;
						case 53:
							num = 20;
							continue;
						case 54:
							goto IL_571;
						case 55:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.WindowProtect)
							{
								num = 70;
								continue;
							}
							bool flag = ((spr\u2520)biffRecordRaw).ᜁ();
							num = 50;
							continue;
						}
						case 56:
							num = 3;
							continue;
						case 57:
						{
							TBIFFRecord typeCode;
							if (typeCode <= TBIFFRecord.WindowProtect)
							{
								num = 27;
								continue;
							}
							num = 81;
							continue;
						}
						case 58:
							return iBOFCounter;
						case 59:
							goto IL_81A;
						case 60:
							if (biffRecordRaw.TypeCode == TBIFFRecord.BOF)
							{
								num = 23;
								continue;
							}
							return iBOFCounter;
						case 61:
							goto IL_692;
						case 62:
							if ((this.\u171D & SheetProtectionType.Content) == SheetProtectionType.None)
							{
								num = 19;
								continue;
							}
							goto IL_1C7;
						case 63:
							if (!this.Workbook.IsWindowProtection)
							{
								num = 53;
								continue;
							}
							return iBOFCounter;
						case 64:
							num = 7;
							continue;
						case 65:
							if (true)
							{
							}
							this.KeepRecord = true;
							this.ᜎ.Add(biffRecordRaw);
							num = 61;
							continue;
						case 66:
							if (iBOFCounter <= 1)
							{
								num = 36;
								continue;
							}
							num = 91;
							continue;
						case 67:
							num = 8;
							continue;
						case 68:
							num = 80;
							continue;
						case 69:
							num = 32;
							continue;
						case 70:
							num = 9;
							continue;
						case 71:
							num = 92;
							continue;
						case 72:
							num = 6;
							continue;
						case 73:
							num = 63;
							continue;
						case 74:
							goto IL_78A;
						case 75:
						{
							TBIFFRecord typeCode;
							switch (typeCode)
							{
							case TBIFFRecord.HeaderFooterImage:
							{
								spr\u1976 spr_u = (spr\u1976)biffRecordRaw;
								this.HeaderFooterShapes.ᜀ(spr_u.ᜃ(), options);
								num = 11;
								continue;
							}
							case TBIFFRecord.SheetProtection:
								spr_u22A = (spr\u22A0)biffRecordRaw;
								num = 94;
								continue;
							default:
								num = 54;
								continue;
							}
							break;
						}
						case 76:
							num = 0;
							continue;
						case 77:
							goto IL_1DF;
						case 78:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.EOF)
							{
								num = 39;
								continue;
							}
							iBOFCounter--;
							num = 37;
							continue;
						}
						case 79:
							if (bSkipStyles)
							{
								num = 51;
								continue;
							}
							goto IL_481;
						case 80:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.BOF)
							{
								num = 29;
								continue;
							}
							iBOFCounter++;
							num = 89;
							continue;
						}
						case 81:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.ObjectProtect)
							{
								num = 76;
								continue;
							}
							this.ᜀ((spr\u17CF)biffRecordRaw);
							num = 93;
							continue;
						}
						case 82:
							goto IL_750;
						case 83:
							num = 62;
							continue;
						case 84:
							if (this.ProtectionMeaningDirect)
							{
								num = 83;
								continue;
							}
							goto IL_27F;
						case 85:
						{
							TBIFFRecord typeCode;
							if (typeCode <= TBIFFRecord.BOF)
							{
								num = 90;
								continue;
							}
							num = 10;
							continue;
						}
						case 86:
							goto IL_8FC;
						case 87:
							goto IL_1F6;
						case 88:
						{
							TBIFFRecord typeCode;
							switch (typeCode)
							{
							case TBIFFRecord.Protect:
								this.ᜀ((spr\u1AE8)biffRecordRaw);
								num = 82;
								continue;
							case TBIFFRecord.Password:
								this.ᜀ((spr\u24C3)biffRecordRaw);
								num = 34;
								continue;
							default:
								num = 12;
								continue;
							}
							break;
						}
						case 89:
							if (iBOFCounter == 1)
							{
								num = 44;
								continue;
							}
							return iBOFCounter;
						case 90:
							num = 35;
							continue;
						case 91:
							if (biffRecordRaw.TypeCode == TBIFFRecord.EOF)
							{
								num = 16;
								continue;
							}
							num = 60;
							continue;
						case 92:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.Dimensions)
							{
								num = 5;
								continue;
							}
							this.ParseDimensions((spr\u203C)biffRecordRaw);
							num = 45;
							continue;
						}
						case 93:
							goto IL_5EA;
						case 94:
							if (this.\u171D != (SheetProtectionType)(-1))
							{
								num = 56;
								continue;
							}
							return iBOFCounter;
						case 95:
							goto IL_270;
						}
						break;
						IL_1C7:
						this.\u1714 = spr_u22A;
						num = 52;
						continue;
						IL_1DF:
						this.ParseRecord(biffRecordRaw, bSkipStyles, hashNewXFormatIndexes);
						num = 87;
						continue;
						IL_27F:
						num = 4;
						continue;
						IL_481:
						num = 18;
						continue;
						IL_571:
						num = 77;
						continue;
						IL_692:
						num = 47;
						continue;
						IL_8FC:
						num = 66;
					}
				}
				IL_1DA:
				IL_1F6:
				IL_270:
				IL_41C:
				IL_4BA:
				IL_5BC:
				IL_5EA:
				IL_607:
				return iBOFCounter;
				IL_657:
				if (false)
				{
				}
				IL_6D3:
				IL_750:
				IL_774:
				IL_78A:
				IL_7F3:
				IL_81A:
				return iBOFCounter;
			}
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x000E652C File Offset: 0x000E552C
		internal void ᜀ(spr\u1AE8 A_0)
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
						goto IL_5F;
					default:
						if (false)
						{
						}
						this.\u171D = this.DefaultProtectionOptions;
						num = 1;
						continue;
					}
					break;
				case 1:
					if (this.ProtectionMeaningDirect)
					{
						num = 4;
						continue;
					}
					goto IL_5F;
				case 2:
					goto IL_79;
				case 4:
					goto IL_C0;
				}
				if (A_0.ᜁ())
				{
					num = 0;
					continue;
				}
				break;
				IL_5F:
				this.\u171D &= ~SheetProtectionType.Content;
				num = 2;
			}
			IL_79:
			return;
			IL_C0:
			if (true)
			{
			}
			this.\u171D |= SheetProtectionType.Content;
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x000E65FC File Offset: 0x000E55FC
		internal void ᜀ(spr\u24C3 A_0)
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
			this.\u1715 = A_0;
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x000E6640 File Offset: 0x000E5640
		internal void ᜀ(spr\u17CF A_0)
		{
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 1:
					goto IL_62;
				case 2:
					if (this.ProtectionMeaningDirect)
					{
						num = 3;
						continue;
					}
					this.\u171D &= ~SheetProtectionType.Objects;
					num = 4;
					continue;
				case 3:
					goto IL_AA;
				case 4:
					goto IL_7B;
				}
				if (!A_0.ᜁ())
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
					num = 1;
					continue;
				}
				IL_62:
				num = 2;
			}
			IL_7B:
			return;
			IL_AA:
			this.\u171D |= SheetProtectionType.Objects;
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x000E66FC File Offset: 0x000E56FC
		internal void ᜀ(sprℷ A_0)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_AA;
				case 1:
					goto IL_5A;
				case 3:
					if (this.ProtectionMeaningDirect)
					{
						num = 0;
						continue;
					}
					this.\u171D &= ~SheetProtectionType.Scenarios;
					num = 4;
					continue;
				case 4:
					goto IL_73;
				}
				if (!A_0.ᜁ())
				{
					return;
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
					num = 1;
					continue;
				}
				IL_5A:
				num = 3;
			}
			IL_73:
			if (true)
			{
			}
			return;
			IL_AA:
			this.\u171D |= SheetProtectionType.Scenarios;
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x000E67B8 File Offset: 0x000E57B8
		protected virtual void PrepareVariables(ExcelParseOptions options, bool bSkipParsing)
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
			this.ᜎ.Clear();
			this.m_iMsoStartIndex = -1;
			this.m_parseOptions = options;
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x000E680C File Offset: 0x000E580C
		internal virtual void ParseWindowTwo(sprṫ windowTwo)
		{
			int a_ = 12;
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.m_book.WorksheetGroup.Add(this);
					num = 4;
					continue;
				case 1:
					this.m_view = ViewMode.Preview;
					num = 6;
					continue;
				case 2:
					goto IL_47;
				case 3:
					if (this.\u1719.\u1712())
					{
						goto IL_102;
					}
					goto IL_51;
				case 4:
					goto IL_51;
				case 5:
					if (!this.\u1719.\u170D())
					{
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_102;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 6:
					return;
				}
				if (windowTwo == null)
				{
					num = 2;
					continue;
				}
				this.\u1719 = windowTwo;
				num = 3;
				continue;
				IL_51:
				num = 5;
				continue;
				IL_102:
				num = 0;
			}
			IL_47:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㕁ⵃ⡅ⱇ╉㭋ᩍ❏㵑", a_));
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x000E6928 File Offset: 0x000E5928
		internal virtual void ParseRecord(BiffRecordRaw raw, bool bIgnoreStyles, Dictionary<int, int> hashNewXFormatIndexes)
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
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x000E6964 File Offset: 0x000E5964
		internal virtual void ParseDimensions(spr\u203C dimensions)
		{
			int a_ = 19;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.m_iLastColumn = 1;
					num = 6;
					continue;
				case 2:
					goto IL_57;
				case 3:
					if (true)
					{
					}
					if (this.m_iLastColumn == 0)
					{
						num = 0;
						continue;
					}
					goto IL_128;
				case 4:
					goto IL_DB;
				case 5:
					goto IL_1C6;
				case 6:
					goto IL_128;
				case 7:
					num = 8;
					continue;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DB;
					default:
						if (false)
						{
						}
						if (dimensions.ᜃ() == 0)
						{
							num = 5;
							continue;
						}
						goto IL_70;
					}
					break;
				case 9:
					goto IL_10F;
				case 10:
					if (this.m_iLastRow == 0)
					{
						num = 11;
						continue;
					}
					return;
				case 11:
					this.m_iLastRow = 1;
					num = 9;
					continue;
				}
				if (dimensions == null)
				{
					num = 2;
					continue;
				}
				num = 4;
				continue;
				IL_70:
				this.m_iFirstColumn = (int)(dimensions.ᜆ() + 1);
				this.m_iFirstRow = dimensions.ᜀ() + 1;
				this.m_iLastColumn = Math.Min((int)dimensions.ᜁ(), this.m_book.MaxColumnCount);
				num = 3;
				continue;
				IL_DB:
				if (dimensions.ᜁ() == 0)
				{
					num = 7;
					continue;
				}
				goto IL_70;
				IL_128:
				this.m_iLastRow = Math.Min(dimensions.ᜃ(), this.m_book.MaxRowCount);
				num = 10;
			}
			IL_57:
			throw new ArgumentNullException(RecordTableEnumerator.b("ⵈ≊⁌⩎㽐⁒㱔㡖㝘⡚", a_));
			IL_10F:
			return;
			IL_1C6:
			this.m_iFirstColumn = int.MaxValue;
			this.m_iFirstRow = -1;
			this.m_iLastColumn = int.MaxValue;
			this.m_iLastRow = -1;
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x000E6B3C File Offset: 0x000E5B3C
		internal virtual void ParseWindowZoom(spr\u1CF7 windowZoom)
		{
			int a_ = 12;
			int num = 9;
			XlsWorksheet xlsWorksheet;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.\u1719.ᜇ() >= 10)
					{
						num = 12;
						continue;
					}
					return;
				case 1:
					if (this is XlsWorksheet)
					{
						num = 5;
						continue;
					}
					return;
				case 2:
					if (true)
					{
					}
					if (this.\u1719.ᜇ() <= 400)
					{
						num = 3;
						continue;
					}
					return;
				case 3:
					xlsWorksheet.ZoomScaleNormal = (int)this.\u1719.ᜇ();
					num = 6;
					continue;
				case 4:
					goto IL_188;
				case 5:
					xlsWorksheet = (this as XlsWorksheet);
					num = 4;
					continue;
				case 6:
					goto IL_11E;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_188;
					default:
						goto IL_6E;
					}
					break;
				case 8:
					if (xlsWorksheet.ViewMode == ViewMode.Normal)
					{
						num = 10;
						continue;
					}
					return;
				case 10:
					num = 0;
					continue;
				case 11:
					goto IL_19F;
				case 12:
					num = 2;
					continue;
				}
				if (windowZoom == null)
				{
					num = 7;
					continue;
				}
				this.\u1713 = windowZoom.ᜀ();
				num = 1;
				continue;
				IL_188:
				if (xlsWorksheet.ViewMode == ViewMode.Preview)
				{
					num = 11;
				}
				else
				{
					num = 8;
				}
			}
			IL_6E:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㕁ⵃ⡅ⱇ╉㭋ᑍ㽏㵑㥓", a_));
			IL_11E:
			return;
			IL_19F:
			xlsWorksheet.ZoomScalePageBreakView = this.\u1713;
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x000E6CEC File Offset: 0x000E5CEC
		internal void ᜀ(sprᲿ A_0)
		{
			int a_ = 10;
			if (A_0 != null)
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
					this.TabKnownColor = (ExcelColors)A_0.ᜂ();
					return;
				}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㌿⩁⅃⍅㱇ىⵋ㝍㽏❑⁓", a_));
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x000E6D58 File Offset: 0x000E5D58
		public virtual void SerializeDataToList(RecordArrayList records)
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
			throw new NotImplementedException();
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x000E6D98 File Offset: 0x000E5D98
		public virtual void SerializeMsoDrawings(RecordArrayList records)
		{
			int a_ = 0;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 2:
					this.ᜏ.SerializeDataToList(records);
					num = 0;
					continue;
				case 3:
					goto IL_53;
				case 4:
					num = 8;
					continue;
				case 5:
					if (this.ᜏ != null)
					{
						goto IL_109;
					}
					return;
				case 6:
					num = 7;
					continue;
				case 7:
					if ((base.ReservedHandle.\u1712() & SkipExtRecordsType.Drawings) == SkipExtRecordsType.Drawings)
					{
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_109;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 8:
					if (this.ᜏ.Count > 0)
					{
						num = 6;
						continue;
					}
					return;
				}
				if (records == null)
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				num = 5;
				continue;
				IL_109:
				num = 4;
			}
			IL_53:
			throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷夹医䰽␿ㅁ", a_));
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x000E6EBC File Offset: 0x000E5EBC
		internal virtual void SerializeProtection(RecordArrayList records, bool bContentNotNecessary)
		{
			int a_ = 9;
			int num = 13;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 8;
					continue;
				case 1:
					goto IL_12C;
				case 2:
					if (this.\u1715 != null)
					{
						num = 3;
						continue;
					}
					return;
				case 3:
					records.ᜀ(this.\u1715);
					num = 9;
					continue;
				case 4:
					if (this.ProtectDrawingObjects)
					{
						num = 12;
						continue;
					}
					goto IL_FE;
				case 5:
					goto IL_FE;
				case 6:
					if (this.ProtectScenarios)
					{
						num = 15;
						continue;
					}
					goto IL_98;
				case 7:
				{
					if (true)
					{
					}
					spr\u1AE8 spr_u1AE = (spr\u1AE8)spr\u175E.ᜀ(TBIFFRecord.Protect);
					spr_u1AE.ᜀ(true);
					records.ᜀ(spr_u1AE);
					num = 18;
					continue;
				}
				case 8:
					if (this.ProtectContents)
					{
						num = 19;
						continue;
					}
					goto IL_FE;
				case 9:
					return;
				case 10:
					if (this.\u1715 != null)
					{
						num = 14;
						continue;
					}
					goto IL_FE;
				case 11:
					if (this.ProtectContents)
					{
						num = 7;
						continue;
					}
					goto IL_BB;
				case 12:
				{
					spr\u17CF spr_u17CF = (spr\u17CF)spr\u175E.ᜀ(TBIFFRecord.ObjectProtect);
					spr_u17CF.ᜀ(true);
					records.ᜀ(spr_u17CF);
					num = 5;
					continue;
				}
				case 14:
					goto IL_192;
				case 15:
				{
					sprℷ sprℷ = (sprℷ)spr\u175E.ᜀ(TBIFFRecord.ScenProtect);
					sprℷ.ᜀ(true);
					records.ᜀ(sprℷ);
					num = 17;
					continue;
				}
				case 16:
					goto IL_93;
				case 17:
					goto IL_98;
				case 18:
					goto IL_BB;
				case 19:
					num = 10;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_12C:
					if (!bContentNotNecessary)
					{
						num = 0;
						continue;
					}
					goto IL_192;
				default:
					if (false)
					{
					}
					if (records == null)
					{
						num = 16;
						continue;
					}
					num = 1;
					continue;
				}
				IL_98:
				num = 4;
				continue;
				IL_BB:
				num = 6;
				continue;
				IL_FE:
				num = 2;
				continue;
				IL_192:
				num = 11;
			}
			IL_93:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀⁂⩄㕆ⵈ㡊", a_));
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x000E7118 File Offset: 0x000E6118
		[CLSCompliant(false)]
		internal void ᜏ(RecordArrayList A_0)
		{
			int a_ = 2;
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
					switch (num)
					{
					case 0:
						num = 6;
						continue;
					case 1:
						return;
					case 2:
						if (true)
						{
						}
						if (this.\u1714 != null)
						{
							num = 0;
							continue;
						}
						return;
					case 4:
						goto IL_5C;
					case 5:
					{
						spr\u22A0 spr_u22A = (spr\u22A0)this.\u1714.Clone();
						spr\u22A0 spr_u22A2 = spr_u22A;
						spr_u22A2.ᜀ(spr_u22A2.ᜁ() & -32769);
						A_0.ᜀ(spr_u22A);
						goto IL_E6;
					}
					case 6:
						if (this.ContainsProtection)
						{
							num = 5;
							continue;
						}
						return;
					}
					if (A_0 == null)
					{
						num = 4;
						continue;
					}
					num = 2;
					continue;
				}
				IL_E6:
				num = 1;
			}
			IL_5C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹弻儽㈿♁㝃", a_));
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x000E7218 File Offset: 0x000E6218
		internal virtual void SerializeHeaderFooterPictures(RecordArrayList records)
		{
			int a_ = 1;
			int num = 4;
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
						num = 2;
						continue;
					case 1:
						if (this.\u171B != null)
						{
							num = 0;
							continue;
						}
						return;
					case 2:
						if (this.\u171B.Count > 0)
						{
							num = 6;
							continue;
						}
						return;
					case 3:
						return;
					case 5:
						goto IL_6E;
					case 6:
						this.\u171B.SerializeDataToList(records);
						goto IL_D0;
					}
					if (records == null)
					{
						num = 5;
						continue;
					}
					num = 1;
					continue;
				}
				IL_D0:
				num = 3;
			}
			IL_6E:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸堺刼䴾╀あ", a_));
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x000E7304 File Offset: 0x000E6304
		internal virtual void SerializeWindowTwo(RecordArrayList records)
		{
			int a_ = 16;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (this == this.m_book.ActiveSheet)
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					goto IL_C1;
				case 2:
					goto IL_80;
				case 3:
					goto IL_5E;
				case 4:
					this.WindowTwo.ᜆ(true);
					this.WindowTwo.ᜇ(true);
					num = 2;
					continue;
				}
				if (records == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5E;
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
			IL_5E:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⥉⍋㱍㑏⅑", a_));
			IL_80:
			IL_C1:
			records.ᜀ(this.WindowTwo);
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x000E73E0 File Offset: 0x000E63E0
		internal virtual void SerializeMacrosSupport(RecordArrayList records)
		{
			int a_ = 10;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (this.m_strCodeName != null)
					{
						num = 3;
						continue;
					}
					return;
				case 3:
					num = 5;
					continue;
				case 4:
					if ((base.ReservedHandle.\u1712() & SkipExtRecordsType.Macros) != SkipExtRecordsType.Macros)
					{
						goto IL_120;
					}
					return;
				case 5:
					if (!this.m_book.HasMacros)
					{
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_120;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				case 6:
					goto IL_53;
				case 7:
					return;
				case 8:
				{
					spr\u2384 spr_u = (spr\u2384)spr\u175E.ᜀ(TBIFFRecord.CodeName);
					spr_u.ᜀ(this.m_strCodeName);
					records.ᜀ(spr_u);
					num = 7;
					continue;
				}
				}
				if (records == null)
				{
					if (true)
					{
					}
					num = 6;
					continue;
				}
				num = 4;
				continue;
				IL_120:
				num = 0;
			}
			IL_53:
			throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁❃⥅㩇⹉㽋", a_));
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x000E7520 File Offset: 0x000E6520
		internal void ᜐ(RecordArrayList A_0)
		{
			int a_ = 13;
			if (true)
			{
			}
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
					break;
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄⑆♈㥊⥌㱎", a_));
			}
			spr\u1CF7 spr_u1CF = (spr\u1CF7)spr\u175E.ᜀ(TBIFFRecord.WindowZoom);
			spr_u1CF.ᜀ(this.Zoom);
			A_0.ᜀ(spr_u1CF);
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x000E75A4 File Offset: 0x000E65A4
		internal void ᜑ(RecordArrayList A_0)
		{
			int a_ = 18;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					sprᲿ sprᲿ = (sprᲿ)spr\u175E.ᜀ(TBIFFRecord.SheetLayout);
					sprᲿ.ᜁ((int)this.\u171A.ᜂ(this.m_book));
					A_0.ᜀ(sprᲿ);
					num = 4;
					continue;
				}
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					if (false)
					{
					}
					if (this.\u171A != null)
					{
						num = 0;
						continue;
					}
					return;
				case 3:
					goto IL_38;
				case 4:
					return;
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
			IL_38:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉⽋⅍≏㙑❓", a_));
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x000E7690 File Offset: 0x000E6690
		internal static ushort ᜀ(string A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 4;
				ushort num4;
				for (;;)
				{
					int num2;
					int length;
					switch (num)
					{
					case 0:
						return 0;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E1;
						default:
							if (false)
							{
							}
							goto IL_C0;
						}
						break;
					case 2:
					{
						if (num2 >= length)
						{
							num = 5;
							continue;
						}
						bool[] a_ = XlsWorksheetBase.ᜀ(A_0[num2]);
						a_ = XlsWorksheetBase.ᜀ(a_, num2 + 1);
						ushort num3 = XlsWorksheetBase.ᜀ(a_);
						num4 ^= num3;
						num2++;
						num = 3;
						continue;
					}
					case 3:
						goto IL_C0;
					case 5:
						goto IL_DF;
					}
					if (A_0 == null)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					num4 = 0;
					num2 = 0;
					length = A_0.Length;
					num = 1;
					continue;
					IL_C0:
					num = 2;
				}
				return 0;
				IL_DF:
				IL_E1:
				return (ushort)((int)num4 ^ A_0.Length ^ 52811);
			}
			}
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x000E7790 File Offset: 0x000E6790
		private static bool[] ᜀ(char A_0)
		{
			switch (0)
			{
			default:
			{
				bool[] array;
				for (;;)
				{
					array = new bool[15];
					ushort num = Convert.ToUInt16(A_0);
					ushort num2 = 1;
					int num3 = 0;
					int num4 = 1;
					for (;;)
					{
						switch (num4)
						{
						case 0:
							if (num3 >= 15)
							{
								num4 = 3;
								continue;
							}
							array[num3] = ((num & num2) == num2);
							num2 = (ushort)(num2 << 1);
							num3++;
							num4 = 2;
							continue;
						case 1:
							if (true)
							{
							}
							goto IL_4D;
						case 2:
							goto IL_A0;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_A0;
							default:
								goto IL_7A;
							}
							break;
						}
						break;
						IL_4D:
						num4 = 0;
						continue;
						IL_A0:
						goto IL_4D;
					}
				}
				IL_7A:
				if (false)
				{
				}
				return array;
			}
			}
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x000E784C File Offset: 0x000E684C
		private static ushort ᜀ(bool[] A_0)
		{
			int a_ = 12;
			switch (0)
			{
			default:
			{
				int num = 9;
				for (;;)
				{
					int num2;
					ushort num5;
					switch (num)
					{
					case 0:
					{
						int num3;
						if (num2 >= num3)
						{
							num = 5;
							continue;
						}
						num = 1;
						continue;
					}
					case 1:
						if (A_0[num2])
						{
							num = 6;
							continue;
						}
						goto IL_10B;
					case 2:
						goto IL_D7;
					case 3:
					{
						if (A_0.Length > 16)
						{
							num = 2;
							continue;
						}
						if (true)
						{
						}
						ushort num4 = 0;
						num5 = 1;
						num2 = 0;
						int num3 = A_0.Length;
						goto IL_134;
					}
					case 4:
						goto IL_10B;
					case 5:
					{
						ushort num4;
						return num4;
					}
					case 6:
					{
						ushort num4;
						num4 += num5;
						num = 4;
						continue;
					}
					case 7:
						goto IL_61;
					case 8:
						goto IL_D9;
					case 10:
						goto IL_D9;
					}
					if (A_0 == null)
					{
						num = 7;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_134;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					IL_D9:
					num = 0;
					continue;
					IL_10B:
					num5 = (ushort)(num5 << 1);
					num2++;
					num = 10;
					continue;
					IL_134:
					num = 8;
				}
				IL_61:
				throw new ArgumentNullException(RecordTableEnumerator.b("⁁ⵃ㉅㭇", a_));
				IL_D7:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᙁⱃ⍅㩇⽉汋ⵍㅏ㱑獓≕硗㡙㥛繝ൟൡᙣͥ䡧ṩѫ཭ṯ剱䕳䁵塷᡹ᕻ੽", a_));
			}
			}
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x000E79B0 File Offset: 0x000E69B0
		private static bool[] ᜀ(bool[] A_0, int A_1)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_65;
					case 1:
						goto IL_D6;
					case 3:
						goto IL_14F;
					case 4:
						if (A_0.Length == 0)
						{
							num = 6;
							continue;
						}
						num = 9;
						continue;
					case 5:
					{
						bool[] array;
						return array;
					}
					case 6:
						return A_0;
					case 7:
						goto IL_D6;
					case 8:
					{
						int num2;
						int num3;
						if (num2 >= num3)
						{
							num = 5;
							continue;
						}
						int num4 = (num2 + A_1) % num3;
						bool[] array;
						array[num4] = A_0[num2];
						num2++;
						num = 7;
						continue;
					}
					case 9:
					{
						if (A_1 < 0)
						{
							num = 3;
							continue;
						}
						bool[] array = new bool[A_0.Length];
						int num2 = 0;
						int num3 = A_0.Length;
						num = 1;
						continue;
					}
					}
					if (true)
					{
					}
					if (A_0 == null)
					{
						num = 0;
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
					IL_D6:
					num = 8;
				}
				IL_65:
				throw new ArgumentNullException(RecordTableEnumerator.b("尽⤿㙁㝃", a_));
				IL_14F:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("崽⼿㝁⩃㉅桇⥉ⵋ⁍睏♑瑓㑕㵗穙せ㭝፟ᅡ䑣ብg୩ɫ乭੯᝱ٳ᥵", a_));
			}
			}
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x000E7B14 File Offset: 0x000E6B14
		internal static int ᜀ(int A_0, int A_1)
		{
			int a_ = 15;
			if (A_1 == 0)
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
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⅄≆⹈㥊⡌⩎煐げ㑔㥖繘⽚絜㵞Ѡ䍢啤", a_));
			}
			int num = A_0 % A_1;
			return A_0 - num + A_1;
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600181C RID: 6172 RVA: 0x000E7B7C File Offset: 0x000E6B7C
		// (remove) Token: 0x0600181D RID: 6173 RVA: 0x000E7C14 File Offset: 0x000E6C14
		[Category("Property Changed")]
		public event XlsEventHandler NameChanged
		{
			add
			{
				for (;;)
				{
					XlsEventHandler xlsEventHandler = this.ᜧ;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							XlsEventHandler xlsEventHandler2;
							if (xlsEventHandler == xlsEventHandler2)
							{
								if (true)
								{
								}
								num = 2;
								continue;
							}
							goto IL_25;
						}
						case 1:
							goto IL_25;
						case 2:
							return;
						}
						break;
						IL_25:
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
							XlsEventHandler xlsEventHandler2 = xlsEventHandler;
							XlsEventHandler value2 = (XlsEventHandler)Delegate.Combine(xlsEventHandler2, value);
							xlsEventHandler = Interlocked.CompareExchange<XlsEventHandler>(ref this.ᜧ, value2, xlsEventHandler2);
							num = 0;
							break;
						}
						}
					}
				}
			}
			remove
			{
				for (;;)
				{
					if (true)
					{
					}
					XlsEventHandler xlsEventHandler = this.ᜧ;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_2D;
						case 1:
						{
							XlsEventHandler xlsEventHandler2;
							if (xlsEventHandler == xlsEventHandler2)
							{
								num = 2;
								continue;
							}
							goto IL_2D;
						}
						case 2:
							return;
						}
						break;
						IL_2D:
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
							XlsEventHandler xlsEventHandler2 = xlsEventHandler;
							XlsEventHandler value2 = (XlsEventHandler)Delegate.Remove(xlsEventHandler2, value);
							xlsEventHandler = Interlocked.CompareExchange<XlsEventHandler>(ref this.ᜧ, value2, xlsEventHandler2);
							num = 1;
							break;
						}
						}
					}
				}
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x000E7CAC File Offset: 0x000E6CAC
		// (set) Token: 0x0600181F RID: 6175 RVA: 0x000E7CF0 File Offset: 0x000E6CF0
		public int RealIndex
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
				return this.ᜋ;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					IL_0A:
					switch (num)
					{
					case 0:
					{
						int iOldIndex = this.ᜋ;
						this.ᜋ = value;
						this.OnRealIndexChanged(iOldIndex);
						num = 1;
						continue;
					}
					case 1:
						return;
					}
					while (this.ᜋ != value)
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
							num = 0;
							goto IL_0A;
						}
					}
					break;
				}
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x000E7D7C File Offset: 0x000E6D7C
		int ITabSheet.TabIndex
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
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x000E7DC0 File Offset: 0x000E6DC0
		public virtual void Parse()
		{
			for (;;)
			{
				this.IsParsed = false;
				this.ParseData();
				bool isParsed = this.IsParsed;
				this.IsParsing = true;
				this.IsParsed = false;
				this.ExtractMSODrawing(this.m_iMsoStartIndex, this.m_parseOptions);
				this.IsParsing = false;
				this.IsParsed = isParsed;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.IsSupported)
						{
							num = 1;
							continue;
						}
						goto IL_DE;
					case 1:
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
							num = 4;
							continue;
						}
						break;
					case 2:
						this.ᜎ.Clear();
						num = 3;
						continue;
					case 3:
						goto IL_8E;
					case 4:
						if (this.IsParsed)
						{
							num = 2;
							continue;
						}
						goto IL_DE;
					}
					break;
				}
			}
			IL_8E:
			IL_DE:
			this.IsParsed = true;
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x000E7EB4 File Offset: 0x000E6EB4
		protected internal void ParseData()
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
			this.ParseData(null);
		}

		// Token: 0x06001823 RID: 6179
		protected internal abstract void ParseData(Dictionary<int, int> updatedSSTIndexes);

		// Token: 0x06001824 RID: 6180 RVA: 0x000E7EF8 File Offset: 0x000E6EF8
		protected void ExtractMSODrawing(int startIndex, ExcelParseOptions options)
		{
			switch (0)
			{
			default:
			{
				int num = 26;
				for (;;)
				{
					int num3;
					TBIFFRecord tbiffrecord;
					TBIFFRecord typeCode;
					switch (num)
					{
					case 0:
					{
						int num2;
						if (num2 <= 0)
						{
							num = 5;
							continue;
						}
						spr\u2553 spr_u = (spr\u2553)this.ᜎ[num3];
						byte[] data = spr_u.Data;
						bool flag = data[0] != 0;
						int num4 = data.Length - 1;
						num = 8;
						continue;
					}
					case 1:
						num = 20;
						continue;
					case 2:
						this.\u170D.Clear();
						num = 24;
						continue;
					case 3:
						goto IL_3D8;
					case 4:
						goto IL_45D;
					case 5:
					{
						spr\u1FF0 spr_u1FF;
						int num5 = (int)spr_u1FF.ᜉ();
						num = 31;
						continue;
					}
					case 6:
						goto IL_2EA;
					case 7:
						if (tbiffrecord <= TBIFFRecord.Continue)
						{
							num = 25;
							continue;
						}
						num = 37;
						continue;
					case 8:
					{
						int num2;
						bool flag;
						int num4;
						num2 -= (flag ? (num4 / 2) : num4);
						this.\u170D.Add(this.ᜎ[num3]);
						num3++;
						num = 34;
						continue;
					}
					case 9:
						goto IL_1E1;
					case 10:
						goto IL_45D;
					case 11:
					{
						int count;
						if (num3 >= count)
						{
							num = 40;
							continue;
						}
						typeCode = this.ᜎ[num3].TypeCode;
						num = 12;
						continue;
					}
					case 12:
					{
						int num6;
						if (num6 == 0)
						{
							num = 1;
							continue;
						}
						goto IL_52B;
					}
					case 13:
					{
						if (tbiffrecord != TBIFFRecord.Continue)
						{
							num = 28;
							continue;
						}
						spr\u2293 spr_u2 = spr\u175E.ᜀ(TBIFFRecord.MSODrawing) as spr\u2293;
						spr\u2553 spr_u3 = this.ᜎ[num3] as spr\u2553;
						spr_u2.ᜀ = new byte[spr_u3.ᜀ.Length];
						spr_u3.ᜀ.CopyTo(spr_u2.ᜀ, 0);
						spr_u2.ᜁ(spr_u3.Length);
						this.\u170D.Add(spr_u2);
						num = 9;
						continue;
					}
					case 14:
					{
						int num6;
						num6++;
						if (true)
						{
						}
						num = 21;
						continue;
					}
					case 15:
						num3--;
						num = 38;
						continue;
					case 16:
						goto IL_3D8;
					case 17:
						goto IL_1F6;
					case 18:
						return;
					case 19:
					{
						if (tbiffrecord != TBIFFRecord.EOF)
						{
							num = 36;
							continue;
						}
						int num6;
						num6--;
						num = 4;
						continue;
					}
					case 20:
						if (Array.IndexOf<TBIFFRecord>(XlsWorksheetBase.ᜇ, typeCode) == -1)
						{
							num = 22;
							continue;
						}
						goto IL_558;
					case 21:
						goto IL_45D;
					case 22:
						goto IL_52B;
					case 23:
						num = 32;
						continue;
					case 24:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2EA;
						default:
							if (false)
							{
							}
							goto IL_1F6;
						}
						break;
					case 25:
						num = 19;
						continue;
					case 27:
						goto IL_363;
					case 28:
						num = 10;
						continue;
					case 29:
					{
						if (startIndex < 0)
						{
							num = 18;
							continue;
						}
						num3 = startIndex;
						int count = this.ᜎ.Count;
						int num6 = 0;
						num = 16;
						continue;
					}
					case 30:
					{
						spr\u1FF0 spr_u1FF;
						if (spr_u1FF.ᜈ() > 0)
						{
							num = 35;
							continue;
						}
						goto IL_1E1;
					}
					case 31:
						goto IL_363;
					case 32:
						if (tbiffrecord == TBIFFRecord.BOF)
						{
							num = 14;
							continue;
						}
						goto IL_45D;
					case 33:
						goto IL_1E1;
					case 34:
						goto IL_2EA;
					case 35:
					{
						num3++;
						spr\u1FF0 spr_u1FF;
						int num2 = (int)spr_u1FF.ᜈ();
						num = 6;
						continue;
					}
					case 36:
						num = 13;
						continue;
					case 37:
					{
						if (tbiffrecord != TBIFFRecord.TextObject)
						{
							num = 23;
							continue;
						}
						spr\u1FF0 spr_u1FF = (spr\u1FF0)this.ᜎ[num3];
						this.\u170D.Add(spr_u1FF);
						num = 30;
						continue;
					}
					case 38:
						goto IL_1E1;
					case 39:
					{
						int num5;
						if (num5 <= 0)
						{
							num = 15;
							continue;
						}
						spr\u2553 spr_u4 = (spr\u2553)this.ᜎ[num3];
						num5 -= spr_u4.Length;
						this.\u170D.Add(spr_u4);
						num3++;
						num = 27;
						continue;
					}
					case 40:
						goto IL_3F7;
					}
					if (this.\u170D != null)
					{
						num = 2;
						continue;
					}
					this.\u170D = new List<BiffRecordRaw>();
					num = 17;
					continue;
					IL_1E1:
					num3++;
					num = 3;
					continue;
					IL_1F6:
					num = 29;
					continue;
					IL_2EA:
					num = 0;
					continue;
					IL_363:
					num = 39;
					continue;
					IL_3D8:
					num = 11;
					continue;
					IL_45D:
					this.\u170D.Add(this.ᜎ[num3]);
					num = 33;
					continue;
					IL_52B:
					tbiffrecord = typeCode;
					num = 7;
				}
				return;
				IL_3F7:
				IL_558:
				List<spr\u1D3B> a_ = this.ᜁ();
				this.ᜏ.ᜀ(a_, options);
				this.ᜏ.ᜆ();
				return;
			}
			}
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x000E8480 File Offset: 0x000E7480
		private List<spr\u1D3B> ᜁ()
		{
			switch (0)
			{
			default:
			{
				List<spr\u1D3B> list2;
				for (;;)
				{
					List<byte[]> list = new List<byte[]>();
					list2 = new List<spr\u1D3B>();
					int num = 0;
					int num2 = 0;
					int num3 = 12;
					for (;;)
					{
						int num4;
						switch (num3)
						{
						case 0:
						{
							num4 = 0;
							int count = this.\u170D.Count;
							num3 = 8;
							continue;
						}
						case 1:
							goto IL_A6;
						case 2:
							goto IL_19F;
						case 3:
							num3 = 17;
							continue;
						case 4:
							num++;
							num3 = 7;
							continue;
						case 5:
						{
							BiffRecordRaw biffRecordRaw;
							byte[] data = biffRecordRaw.Data;
							num2 += data.Length;
							list.Add(data);
							num3 = 10;
							continue;
						}
						case 6:
						{
							MemoryStream memoryStream;
							if (memoryStream.Position >= (long)num2)
							{
								if (true)
								{
								}
								num3 = 13;
								continue;
							}
							spr\u1D3B item = spr\u231F.ᜀ(null, memoryStream, new spr\u24C9(this.ᜀ));
							list2.Add(item);
							num3 = 2;
							continue;
						}
						case 7:
							goto IL_1E2;
						case 8:
							goto IL_A6;
						case 9:
						{
							BiffRecordRaw biffRecordRaw;
							if (biffRecordRaw.TypeCode == TBIFFRecord.EOF)
							{
								num3 = 15;
								continue;
							}
							goto IL_17E;
						}
						case 10:
							goto IL_17E;
						case 11:
						{
							BiffRecordRaw biffRecordRaw;
							if (biffRecordRaw.TypeCode == TBIFFRecord.BOF)
							{
								num3 = 4;
								continue;
							}
							num3 = 9;
							continue;
						}
						case 12:
							if (this.\u170D.Count > 0)
							{
								num3 = 0;
								continue;
							}
							return list2;
						case 13:
							return list2;
						case 14:
						{
							byte[] buffer = XlsWorksheetBase.ᜀ(num2, list);
							MemoryStream memoryStream = new MemoryStream(buffer);
							num3 = 18;
							continue;
						}
						case 15:
							num--;
							num3 = 20;
							continue;
						case 16:
							if (num == 0)
							{
								num3 = 3;
								continue;
							}
							goto IL_282;
						case 17:
						{
							BiffRecordRaw biffRecordRaw;
							if (biffRecordRaw is spr\u2293)
							{
								num3 = 5;
								continue;
							}
							goto IL_282;
						}
						case 18:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1E2;
							default:
								if (false)
								{
								}
								goto IL_19F;
							}
							break;
						case 19:
						{
							int count;
							if (num4 >= count)
							{
								num3 = 14;
								continue;
							}
							BiffRecordRaw biffRecordRaw = this.\u170D[num4];
							num3 = 16;
							continue;
						}
						case 20:
							goto IL_17E;
						}
						break;
						IL_A6:
						num3 = 19;
						continue;
						IL_17E:
						num4++;
						num3 = 1;
						continue;
						IL_1E2:
						goto IL_17E;
						IL_19F:
						num3 = 6;
						continue;
						IL_282:
						num3 = 11;
					}
				}
				return list2;
			}
			}
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x000E8740 File Offset: 0x000E7740
		private BiffRecordRaw[] ᜀ()
		{
			int a_ = 11;
			List<BiffRecordRaw> list;
			for (;;)
			{
				list = new List<BiffRecordRaw>();
				bool flag = false;
				int num = 0;
				int num2 = 13;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (this.\u170D[this.ᜌ] is spr\u22C5)
						{
							num2 = 20;
							continue;
						}
						goto IL_107;
					case 1:
						if (!flag)
						{
							num2 = 12;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F0;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num2 = 4;
							continue;
						}
						break;
					case 2:
						goto IL_211;
					case 3:
						if (this.ᜌ >= this.\u170D.Count)
						{
							num2 = 24;
							continue;
						}
						num2 = 11;
						continue;
					case 4:
						num2 = 22;
						continue;
					case 5:
						if (!(this.\u170D[this.ᜌ] is spr\u2293))
						{
							num2 = 14;
							continue;
						}
						goto IL_2FC;
					case 6:
						if (flag)
						{
							num2 = 15;
							continue;
						}
						goto IL_2FC;
					case 7:
						num2 = 5;
						continue;
					case 8:
						goto IL_107;
					case 9:
						flag = true;
						num2 = 2;
						continue;
					case 10:
						goto IL_27C;
					case 11:
						if (num == 0)
						{
							num2 = 7;
							continue;
						}
						goto IL_2AF;
					case 12:
						if (this.ᜌ >= this.\u170D.Count)
						{
							num2 = 10;
							continue;
						}
						num2 = 17;
						continue;
					case 13:
						goto IL_211;
					case 14:
						goto IL_F0;
					case 15:
						num2 = 3;
						continue;
					case 16:
						num++;
						num2 = 8;
						continue;
					case 17:
						if (!(this.\u170D[this.ᜌ] is spr\u2293))
						{
							num2 = 9;
							continue;
						}
						this.ᜌ++;
						num2 = 23;
						continue;
					case 18:
						if (this.\u170D[this.ᜌ] is sprḯ)
						{
							num2 = 16;
							continue;
						}
						num2 = 0;
						continue;
					case 19:
						goto IL_1DC;
					case 20:
						num--;
						num2 = 21;
						continue;
					case 21:
						goto IL_107;
					case 22:
						goto IL_1DC;
					case 23:
						goto IL_211;
					case 24:
						goto IL_180;
					}
					break;
					IL_107:
					this.ᜌ++;
					num2 = 19;
					continue;
					IL_1DC:
					num2 = 6;
					continue;
					IL_211:
					num2 = 1;
					continue;
					IL_2AF:
					list.Add(this.\u170D[this.ᜌ]);
					num2 = 18;
					continue;
					IL_F0:
					goto IL_2AF;
				}
			}
			IL_180:
			goto IL_2FC;
			IL_27C:
			throw new ApplicationException(RecordTableEnumerator.b("ీ၂੄͆㭈⩊㩌♎㽐㑒畔㍖㡘⽚㱜罞ɠɢ୤䝦ݨѪᥬ佮፰ᙲ啴ᅶᙸ๺፼᭾꾀", a_));
			IL_2FC:
			return list.ToArray();
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x000E8A50 File Offset: 0x000E7A50
		internal static byte[] ᜀ(int A_0, List<byte[]> A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							goto IL_103;
						}
						byte[] array = A_1[num2];
						int num3 = array.Length;
						byte[] array2;
						int num4;
						Buffer.BlockCopy(array, 0, array2, num4, num3);
						num4 += num3;
						num2++;
						num = 7;
						continue;
					}
					case 1:
						goto IL_F0;
					case 2:
						goto IL_EE;
					case 4:
					{
						byte[] array2;
						return array2;
					}
					case 5:
						num = 6;
						continue;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_103;
						default:
						{
							if (false)
							{
							}
							if (A_1.Count == 0)
							{
								if (true)
								{
								}
								num = 2;
								continue;
							}
							int count = A_1.Count;
							byte[] array2 = new byte[A_0];
							int num4 = 0;
							int num2 = 0;
							num = 1;
							continue;
						}
						}
						break;
					case 7:
						goto IL_F0;
					}
					if (A_1 != null)
					{
						num = 5;
						continue;
					}
					break;
					IL_F0:
					num = 0;
					continue;
					IL_103:
					num = 4;
				}
				IL_EE:
				return new byte[0];
			}
			}
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x000E8B78 File Offset: 0x000E7B78
		internal void ᜀ(XlsWorksheetBase A_0, Dictionary<string, string> A_1, Dictionary<string, string> A_2, Dictionary<int, int> A_3, WorksheetCopyType A_4, Dictionary<int, int> A_5)
		{
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8A;
				case 1:
					this.CopyOptions(A_0);
					num = 0;
					continue;
				case 2:
					goto IL_F6;
				case 3:
					this.CopyShapes(A_0, A_2, A_3);
					num = 2;
					continue;
				case 4:
					if ((A_4 & WorksheetCopyType.CopyPageSetup) != WorksheetCopyType.None)
					{
						num = 10;
						continue;
					}
					return;
				case 5:
					if ((A_4 & WorksheetCopyType.CopyShapes) != WorksheetCopyType.None)
					{
						num = 3;
						continue;
					}
					goto IL_F6;
				case 6:
					if (true)
					{
					}
					goto IL_A6;
				case 7:
					this.ClearAll(A_4);
					num = 6;
					continue;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8A;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 9:
					return;
				case 10:
					this.CopyHeaderFooterImages(A_0, A_2, A_3);
					num = 9;
					continue;
				case 11:
					if ((A_4 & WorksheetCopyType.CopyOptions) != WorksheetCopyType.None)
					{
						num = 1;
						continue;
					}
					goto IL_8A;
				}
				if ((A_4 & WorksheetCopyType.ClearBefore) != WorksheetCopyType.None)
				{
					num = 7;
					continue;
				}
				goto IL_A6;
				IL_8A:
				num = 5;
				continue;
				IL_A6:
				num = 11;
				continue;
				IL_F6:
				num = 4;
			}
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x000E8CB4 File Offset: 0x000E7CB4
		protected void CopyHeaderFooterImages(XlsWorksheetBase sourceSheet, Dictionary<string, string> hashNewNames, IDictionary dicFontIndexes)
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
			XlsPageSetupBase pageSetupBase = this.PageSetupBase;
			XlsPageSetupBase pageSetupBase2 = sourceSheet.PageSetupBase;
			spr\u1CD3.ᜀ(sourceSheet.\u171B, this);
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x000E8D0C File Offset: 0x000E7D0C
		protected void CopyShapes(XlsWorksheetBase sourceSheet, Dictionary<string, string> hashNewNames, Dictionary<int, int> dicFontIndexes)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					ShapeCollectionBase shapeCollectionBase = sourceSheet.Shapes as ShapeCollectionBase;
					int num = 0;
					int count = shapeCollectionBase.Count;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								goto IL_B2;
							}
							break;
						case 1:
							goto IL_47;
						case 2:
							goto IL_47;
						case 3:
						{
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							XlsShape sourceXlsShape = (XlsShape)shapeCollectionBase[num];
							this.ᜏ.AddCopy(sourceXlsShape, hashNewNames, dicFontIndexes);
							num++;
							num2 = 1;
							continue;
						}
						}
						break;
						IL_47:
						if (true)
						{
						}
						num2 = 3;
					}
				}
				IL_B2:
				if (false)
				{
				}
				return;
			}
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x000E8DD4 File Offset: 0x000E7DD4
		protected virtual void CopyOptions(XlsWorksheetBase sourceSheet)
		{
			for (;;)
			{
				this.\u1714 = (spr\u22A0)spr\u1CD3.ᜀ(sourceSheet.\u1714);
				int num = 12;
				for (;;)
				{
					if (true)
					{
					}
					string strCodeName;
					switch (num)
					{
					case 0:
						(this as XlsWorksheet).ZoomScaleNormal = this.\u1713;
						num = 8;
						continue;
					case 1:
						if (this is XlsWorksheet)
						{
							num = 0;
							continue;
						}
						goto IL_1B0;
					case 2:
						return;
					case 3:
						goto IL_1D1;
					case 4:
						this.m_strCodeName = this.ᜀ(new XlsWorksheetBase.ᜀ(this.ᜁ), strCodeName);
						num = 2;
						continue;
					case 5:
						this.\u1715 = (spr\u24C3)sourceSheet.\u1715.Clone();
						num = 10;
						continue;
					case 6:
						if (sourceSheet.\u1719 != null)
						{
							num = 3;
							continue;
						}
						goto IL_9A;
					case 7:
						if (strCodeName.Length > 0)
						{
							num = 4;
							continue;
						}
						return;
					case 8:
						goto IL_1B0;
					case 9:
						goto IL_9A;
					case 10:
						goto IL_104;
					case 11:
						num = 7;
						continue;
					case 12:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1D1;
						default:
							if (false)
							{
							}
							if (sourceSheet.\u1715 != null)
							{
								num = 5;
								continue;
							}
							goto IL_104;
						}
						break;
					case 13:
						if (strCodeName != null)
						{
							num = 11;
							continue;
						}
						return;
					}
					break;
					IL_9A:
					this.ᜀ(sourceSheet);
					strCodeName = sourceSheet.m_strCodeName;
					num = 13;
					continue;
					IL_104:
					this.\u1713 = sourceSheet.\u1713;
					num = 1;
					continue;
					IL_1B0:
					num = 6;
					continue;
					IL_1D1:
					this.\u1719 = (sprṫ)sourceSheet.\u1719.Clone();
					this.\u1719.ᜇ(false);
					this.\u1719.ᜆ(false);
					num = 9;
				}
			}
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x000E8FD8 File Offset: 0x000E7FD8
		private string ᜀ(XlsWorksheetBase.ᜀ A_0, string A_1)
		{
			int a_ = 19;
			switch (0)
			{
			default:
				for (;;)
				{
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					ITabSheets tabSheets = this.m_book.TabSheets;
					bool flag = true;
					int num = 0;
					int count = tabSheets.Count;
					int num2 = 2;
					for (;;)
					{
						string text2;
						switch (num2)
						{
						case 0:
							goto IL_1BD;
						case 1:
						{
							int num3 = 0;
							string text = A_1;
							goto IL_B6;
						}
						case 2:
							goto IL_1BD;
						case 3:
							num2 = 14;
							continue;
						case 4:
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							text2 = A_0(tabSheets[num]);
							num2 = 12;
							continue;
						case 5:
						{
							string text;
							if (text.Length > 31)
							{
								num2 = 10;
								continue;
							}
							goto IL_FD;
						}
						case 6:
							flag = false;
							num2 = 13;
							continue;
						case 7:
						{
							string text;
							A_1 = text;
							num2 = 9;
							continue;
						}
						case 8:
							goto IL_FD;
						case 9:
							return A_1;
						case 10:
						{
							int num3 = 0;
							string text;
							A_1 = (text = A_1.Remove(A_1.Length - 1));
							num2 = 8;
							continue;
						}
						case 11:
						{
							string text;
							if (!dictionary.ContainsKey(text))
							{
								num2 = 7;
								continue;
							}
							int num3;
							num3++;
							text = A_1 + RecordTableEnumerator.b("ᙈ", a_) + num3;
							num2 = 5;
							continue;
						}
						case 12:
							if (text2 == A_1)
							{
								num2 = 6;
								continue;
							}
							goto IL_1DF;
						case 13:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_B6;
							default:
								if (false)
								{
								}
								goto IL_1DF;
							}
							break;
						case 14:
							if (!flag)
							{
								num2 = 1;
								continue;
							}
							return A_1;
						case 15:
							goto IL_FD;
						}
						break;
						IL_B6:
						num2 = 15;
						continue;
						IL_FD:
						num2 = 11;
						continue;
						IL_1BD:
						num2 = 4;
						continue;
						IL_1DF:
						dictionary.Add(text2, null);
						num++;
						if (true)
						{
						}
						num2 = 0;
					}
				}
				return A_1;
			}
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x000E9208 File Offset: 0x000E8208
		private string ᜁ(ITabSheet A_0)
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
			return A_0.CodeName;
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x000E924C File Offset: 0x000E824C
		private string ᜀ(ITabSheet A_0)
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
			return A_0.Name;
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x000E9290 File Offset: 0x000E8290
		private void ᜀ(XlsWorksheetBase A_0)
		{
			int a_ = 7;
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
						continue;
					default:
						goto IL_EB;
					}
					break;
				case 1:
					if (A_0.\u171A == null)
					{
						num = 0;
						continue;
					}
					num = 4;
					continue;
				case 3:
					goto IL_A5;
				case 4:
					if (this.\u171A == null)
					{
						num = 5;
						continue;
					}
					goto IL_F6;
				case 5:
					if (true)
					{
					}
					this.\u171A = new OColor(ExcelColors.Black);
					num = 3;
					continue;
				case 6:
					goto IL_40;
				}
				if (A_0 == null)
				{
					num = 6;
				}
				else
				{
					num = 1;
				}
			}
			IL_40:
			throw new ArgumentNullException(RecordTableEnumerator.b("丼倾㑀ㅂ♄≆ᩈ⍊⡌⩎═", a_));
			IL_A5:
			goto IL_F6;
			IL_EB:
			if (false)
			{
			}
			this.\u171A = A_0.\u171A;
			return;
			IL_F6:
			this.\u171A.ᜀ(A_0.\u171A, true);
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x000E93A8 File Offset: 0x000E83A8
		// Note: this type is marked as 'beforefieldinit'.
		static XlsWorksheetBase()
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
			XlsWorksheetBase.ᜇ = new TBIFFRecord[]
			{
				TBIFFRecord.PivotViewDefinition,
				TBIFFRecord.Note,
				TBIFFRecord.WindowTwo,
				(TBIFFRecord)2128,
				TBIFFRecord.HeaderFooterImage,
				(TBIFFRecord)237,
				TBIFFRecord.ChartUnits,
				TBIFFRecord.ChartChart,
				TBIFFRecord.DCON
			};
			XlsWorksheetBase.ᜈ = spr\u1D39.ᜂ;
		}

		// Token: 0x04000FAD RID: 4013
		internal const int ᜀ = 15;

		// Token: 0x04000FAE RID: 4014
		private const ushort ᜁ = 52811;

		// Token: 0x04000FAF RID: 4015
		public const int DEF_MIN_COLUMN_INDEX = 2147483647;

		// Token: 0x04000FB0 RID: 4016
		internal const int ᜂ = -1;

		// Token: 0x04000FB1 RID: 4017
		internal const ExcelColors ᜃ = (ExcelColors)(-1);

		// Token: 0x04000FB2 RID: 4018
		internal const int ᜄ = 65536;

		// Token: 0x04000FB3 RID: 4019
		internal const int ᜅ = 256;

		// Token: 0x04000FB4 RID: 4020
		private const int ᜆ = 31;

		// Token: 0x04000FB5 RID: 4021
		private static readonly TBIFFRecord[] ᜇ;

		// Token: 0x04000FB6 RID: 4022
		private static readonly Color ᜈ;

		// Token: 0x04000FB7 RID: 4023
		protected XlsWorkbook m_book;

		// Token: 0x04000FB8 RID: 4024
		private string ᜉ = string.Empty;

		// Token: 0x04000FB9 RID: 4025
		private bool ᜊ = true;

		// Token: 0x04000FBA RID: 4026
		private int ᜋ;

		// Token: 0x04000FBB RID: 4027
		protected int m_iMsoStartIndex = -1;

		// Token: 0x04000FBC RID: 4028
		private int ᜌ;

		// Token: 0x04000FBD RID: 4029
		protected ExcelParseOptions m_parseOptions;

		// Token: 0x04000FBE RID: 4030
		private List<BiffRecordRaw> \u170D;

		// Token: 0x04000FBF RID: 4031
		internal List<BiffRecordRaw> ᜎ = new List<BiffRecordRaw>();

		// Token: 0x04000FC0 RID: 4032
		private spr\u1D9B ᜏ;

		// Token: 0x04000FC1 RID: 4033
		private XlsWorksheetChartsCollection ᜐ;

		// Token: 0x04000FC2 RID: 4034
		private XlsPicturesCollection ᜑ;

		// Token: 0x04000FC3 RID: 4035
		private bool \u1712 = true;

		// Token: 0x04000FC4 RID: 4036
		private int \u1713 = 100;

		// Token: 0x04000FC5 RID: 4037
		private spr\u22A0 \u1714;

		// Token: 0x04000FC6 RID: 4038
		private spr\u24C3 \u1715;

		// Token: 0x04000FC7 RID: 4039
		protected string m_strCodeName;

		// Token: 0x04000FC8 RID: 4040
		private bool \u1716 = true;

		// Token: 0x04000FC9 RID: 4041
		private bool \u1717;

		// Token: 0x04000FCA RID: 4042
		private bool \u1718;

		// Token: 0x04000FCB RID: 4043
		private sprṫ \u1719;

		// Token: 0x04000FCC RID: 4044
		protected ViewMode m_view;

		// Token: 0x04000FCD RID: 4045
		[CLSCompliant(false)]
		protected int m_iFirstColumn = int.MaxValue;

		// Token: 0x04000FCE RID: 4046
		[CLSCompliant(false)]
		protected int m_iLastColumn = int.MaxValue;

		// Token: 0x04000FCF RID: 4047
		protected int m_iFirstRow = -1;

		// Token: 0x04000FD0 RID: 4048
		protected int m_iLastRow = -1;

		// Token: 0x04000FD1 RID: 4049
		private OColor \u171A;

		// Token: 0x04000FD2 RID: 4050
		private XlsHeaderFooterShapeCollection \u171B;

		// Token: 0x04000FD3 RID: 4051
		private int \u171C;

		// Token: 0x04000FD4 RID: 4052
		private SheetProtectionType \u171D;

		// Token: 0x04000FD5 RID: 4053
		internal sprḯ \u171E = (sprḯ)spr\u175E.ᜀ(TBIFFRecord.BOF);

		// Token: 0x04000FD6 RID: 4054
		protected bool KeepRecord;

		// Token: 0x04000FD7 RID: 4055
		private WorksheetVisibility \u171F;

		// Token: 0x04000FD8 RID: 4056
		internal sprᡟ ᜠ;

		// Token: 0x04000FD9 RID: 4057
		private bool ᜡ;

		// Token: 0x04000FDA RID: 4058
		private TextBoxCollection ᜢ;

		// Token: 0x04000FDB RID: 4059
		private CheckBoxCollection ᜣ;

		// Token: 0x04000FDC RID: 4060
		private RadioButtonCollection ᜤ;

		// Token: 0x04000FDD RID: 4061
		private ComboBoxCollection ᜥ;

		// Token: 0x04000FDE RID: 4062
		private bool ᜦ;

		// Token: 0x04000FDF RID: 4063
		private XlsEventHandler ᜧ;

		// Token: 0x020005F2 RID: 1522
		// (Invoke) Token: 0x060059F0 RID: 23024
		private delegate string ᜀ(ITabSheet A_0);
	}
}
