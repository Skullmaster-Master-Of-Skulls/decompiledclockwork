using System;
using System.Drawing;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Shapes
{
	// Token: 0x02000222 RID: 546
	public class XlsShapeLineFormat : XlsObject, IShapeLineFormat
	{
		// Token: 0x060020BC RID: 8380 RVA: 0x00127220 File Offset: 0x00126220
		static XlsShapeLineFormat()
		{
			int a_ = 17;
			for (;;)
			{
				XlsShapeLineFormat.ᜃ = new byte[5088];
				int num = 0;
				int num2 = 1;
				int num3 = 3;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_46;
					case 1:
						if (num2 >= 49)
						{
							num3 = 2;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
						{
							if (false)
							{
							}
							byte[] resData = XlsShapeFill.GetResData(RecordTableEnumerator.b("ᝆ⡈㽊㥌", a_) + num2.ToString());
							XlsShapeLineFormat.ᜃ[num] = (byte)resData.Length;
							num++;
							resData.CopyTo(XlsShapeLineFormat.ᜃ, num);
							num += resData.Length;
							num2++;
							num3 = 0;
							continue;
						}
						}
						break;
					case 2:
						return;
					case 3:
						if (true)
						{
						}
						goto IL_46;
					}
					break;
					IL_46:
					num3 = 1;
				}
			}
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x00127308 File Offset: 0x00126308
		internal XlsShapeLineFormat(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ();
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x0012737C File Offset: 0x0012637C
		private void ᜀ()
		{
			int a_ = 12;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_5C;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜇ = (XlsWorkbook)base.FindParent(typeof(XlsWorkbook));
			if (this.ᜇ != null)
			{
				return;
			}
			IL_5C:
			throw new ApplicationException(RecordTableEnumerator.b("Ł╃⡅♇浉㡋湍㙏㭑㩓㉕硗⩙㵛ⱝ՟ౡၣ䙥ݧࡩ٫୭፯ٱ婳", a_));
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x001273FC File Offset: 0x001263FC
		internal static double ᜀ(uint A_0)
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
			return (65500.0 - A_0) / 65500.0;
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x00127450 File Offset: 0x00126450
		internal static void ᜀ(sprᡍ A_0, MsoOptions A_1, double A_2)
		{
			int a_ = 8;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_34;
			}
			if (false)
			{
			}
			if (A_0 != null)
			{
				int a_2 = (int)((100.0 - A_2 * 100.0) * 655.0);
				XlsShape.ᜀ(A_0, A_1, a_2);
				return;
			}
			IL_34:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("儽〿㙁", a_));
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x001274D8 File Offset: 0x001264D8
		internal static void ᜀ(sprᡍ A_0, OColor A_1, XlsWorkbook A_2, MsoOptions A_3)
		{
			int a_ = 14;
			switch (0)
			{
			default:
			{
				int num = 12;
				for (;;)
				{
					bool flag;
					switch (num)
					{
					case 0:
					{
						byte[] a_2;
						XlsShape.ᜀ(A_0, A_3, a_2);
						num = 8;
						continue;
					}
					case 1:
					{
						Color color;
						if (color.B == 0)
						{
							num = 14;
							continue;
						}
						goto IL_DF;
					}
					case 2:
						num = 13;
						continue;
					case 3:
						goto IL_DF;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A9;
						default:
						{
							if (false)
							{
							}
							Color color;
							if (color.A == 0)
							{
								num = 2;
								continue;
							}
							goto IL_DF;
						}
						}
						break;
					case 5:
						goto IL_79;
					case 6:
						if (flag)
						{
							num = 0;
							continue;
						}
						return;
					case 7:
					{
						Color color;
						if (color.G == 0)
						{
							num = 9;
							continue;
						}
						goto IL_DF;
					}
					case 8:
						return;
					case 9:
						goto IL_A9;
					case 10:
					{
						if (A_1.ColorType == ColorType.Known)
						{
							num = 15;
							continue;
						}
						Color color = A_1.ᜁ(A_2);
						byte[] array = new byte[4];
						array[0] = color.R;
						array[1] = color.G;
						array[2] = color.B;
						byte[] a_2 = array;
						if (true)
						{
						}
						num = 4;
						continue;
					}
					case 11:
						goto IL_DF;
					case 13:
					{
						Color color;
						if (color.R == 0)
						{
							num = 16;
							continue;
						}
						goto IL_DF;
					}
					case 14:
						flag = false;
						num = 11;
						continue;
					case 15:
					{
						byte[] a_2 = new byte[]
						{
							(byte)A_1.Value,
							0,
							0,
							8
						};
						num = 3;
						continue;
					}
					case 16:
						num = 7;
						continue;
					}
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					flag = true;
					num = 10;
					continue;
					IL_A9:
					num = 1;
					continue;
					IL_DF:
					num = 6;
				}
				IL_79:
				throw new ArgumentNullException(RecordTableEnumerator.b("⭃㙅㱇", a_));
			}
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x060020C2 RID: 8386 RVA: 0x00127708 File Offset: 0x00126708
		// (set) Token: 0x060020C3 RID: 8387 RVA: 0x0012774C File Offset: 0x0012674C
		public double Weight
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
				return this.ᜄ;
			}
			set
			{
				int a_ = 19;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value <= 1584.0)
						{
							goto IL_9F;
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
						break;
					case 1:
						goto IL_93;
					case 2:
						num = 0;
						continue;
					}
					IL_29:
					if (true)
					{
					}
					if (value >= 0.0)
					{
						num = 2;
						continue;
					}
					break;
					goto IL_29;
				}
				IL_47:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("Ṉ⹊⑌⡎㥐❒", a_));
				IL_93:
				goto IL_47;
				IL_9F:
				this.ᜄ = value;
				this.Visible = true;
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x060020C4 RID: 8388 RVA: 0x00127808 File Offset: 0x00126808
		// (set) Token: 0x060020C5 RID: 8389 RVA: 0x0012784C File Offset: 0x0012684C
		public Color ForeColor
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
				this.Visible = true;
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x060020C6 RID: 8390 RVA: 0x00127898 File Offset: 0x00126898
		// (set) Token: 0x060020C7 RID: 8391 RVA: 0x001278DC File Offset: 0x001268DC
		public Color BackColor
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
				this.ᜆ = value;
				this.Visible = true;
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x060020C8 RID: 8392 RVA: 0x00127928 File Offset: 0x00126928
		// (set) Token: 0x060020C9 RID: 8393 RVA: 0x00127974 File Offset: 0x00126974
		public ExcelColors ForeKnownColor
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
				return this.ᜇ.GetNearestColor(this.ᜅ);
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
				this.ForeColor = this.ᜇ.GetPaletteColor(value);
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x060020CA RID: 8394 RVA: 0x001279C4 File Offset: 0x001269C4
		// (set) Token: 0x060020CB RID: 8395 RVA: 0x00127A10 File Offset: 0x00126A10
		public ExcelColors BackKnownColor
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
				return this.ᜇ.GetNearestColor(this.ᜆ);
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
				this.BackColor = this.ᜇ.GetPaletteColor(value);
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x060020CC RID: 8396 RVA: 0x00127A60 File Offset: 0x00126A60
		// (set) Token: 0x060020CD RID: 8397 RVA: 0x00127AA4 File Offset: 0x00126AA4
		public ShapeArrowStyleType BeginArrowHeadStyle
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
				this.Visible = true;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x060020CE RID: 8398 RVA: 0x00127AF0 File Offset: 0x00126AF0
		// (set) Token: 0x060020CF RID: 8399 RVA: 0x00127B34 File Offset: 0x00126B34
		public ShapeArrowStyleType EndArrowHeadStyle
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
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜉ = value;
				this.Visible = true;
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x060020D0 RID: 8400 RVA: 0x00127B80 File Offset: 0x00126B80
		// (set) Token: 0x060020D1 RID: 8401 RVA: 0x00127BC4 File Offset: 0x00126BC4
		public ShapeArrowLengthType BeginArrowheadLength
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
				return this.ᜊ;
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
				this.ᜊ = value;
				this.Visible = true;
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x00127C10 File Offset: 0x00126C10
		// (set) Token: 0x060020D3 RID: 8403 RVA: 0x00127C54 File Offset: 0x00126C54
		public ShapeArrowLengthType EndArrowheadLength
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
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜋ = value;
				this.Visible = true;
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x060020D4 RID: 8404 RVA: 0x00127CA0 File Offset: 0x00126CA0
		// (set) Token: 0x060020D5 RID: 8405 RVA: 0x00127CE4 File Offset: 0x00126CE4
		public ShapeArrowWidthType BeginArrowheadWidth
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
				return this.ᜌ;
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
				this.ᜌ = value;
				this.Visible = true;
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x00127D30 File Offset: 0x00126D30
		// (set) Token: 0x060020D7 RID: 8407 RVA: 0x00127D74 File Offset: 0x00126D74
		public ShapeArrowWidthType EndArrowheadWidth
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
				return this.\u170D;
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
				this.\u170D = value;
				this.Visible = true;
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x060020D8 RID: 8408 RVA: 0x00127DC0 File Offset: 0x00126DC0
		// (set) Token: 0x060020D9 RID: 8409 RVA: 0x00127E04 File Offset: 0x00126E04
		public ShapeDashLineStyleType DashStyle
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
				this.Visible = true;
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x00127E50 File Offset: 0x00126E50
		// (set) Token: 0x060020DB RID: 8411 RVA: 0x00127E94 File Offset: 0x00126E94
		public ShapeLineStyleType Style
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
				return this.ᜏ;
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
				this.ᜏ = value;
				this.Visible = true;
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x00127EE0 File Offset: 0x00126EE0
		// (set) Token: 0x060020DD RID: 8413 RVA: 0x00127F24 File Offset: 0x00126F24
		public double Transparency
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
				return this.ᜐ;
			}
			set
			{
				int a_ = 10;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value <= 1.0)
						{
							goto IL_9F;
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
						break;
					case 1:
						goto IL_93;
					case 3:
						num = 0;
						continue;
					}
					IL_29:
					if (true)
					{
					}
					if (value >= 0.0)
					{
						num = 3;
						continue;
					}
					break;
					goto IL_29;
				}
				IL_47:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㘿⍁⡃㍅ⵇ", a_));
				IL_93:
				goto IL_47;
				IL_9F:
				this.ᜐ = value;
				this.Visible = true;
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x060020DE RID: 8414 RVA: 0x00127FE0 File Offset: 0x00126FE0
		// (set) Token: 0x060020DF RID: 8415 RVA: 0x00128024 File Offset: 0x00127024
		public bool Visible
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
				return this.ᜑ;
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
				this.ᜑ = value;
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x00128068 File Offset: 0x00127068
		// (set) Token: 0x060020E1 RID: 8417 RVA: 0x001280D0 File Offset: 0x001270D0
		public GradientPatternType Pattern
		{
			get
			{
				int a_ = 18;
				if (!this.\u1713)
				{
					for (;;)
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
						break;
					}
					if (false)
					{
					}
					throw new NotSupportedException(RecordTableEnumerator.b("ే╉⥋㵍㹏畑⁓癕㭗㉙㥛㵝ୟݡc䙥ᡧ୩ᡫᩭᕯqᩳ፵ᱷ婹ཻ੽勵ꢅ", a_));
				}
				return this.\u1712;
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
				this.\u1712 = value;
				this.HasPattern = true;
				this.Visible = true;
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x060020E2 RID: 8418 RVA: 0x00128120 File Offset: 0x00127120
		// (set) Token: 0x060020E3 RID: 8419 RVA: 0x00128164 File Offset: 0x00127164
		public bool HasPattern
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
				return this.\u1713;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 2:
						this.\u1713 = value;
						this.Visible = true;
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
					}
					if (true)
					{
					}
					if (this.HasPattern == value)
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x060020E4 RID: 8420 RVA: 0x001281E8 File Offset: 0x001271E8
		internal XlsWorkbook Workbook
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
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x060020E5 RID: 8421 RVA: 0x0012822C File Offset: 0x0012722C
		// (set) Token: 0x060020E6 RID: 8422 RVA: 0x00128270 File Offset: 0x00127270
		public bool IsRound
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
				this.\u1714 = value;
			}
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x001282B4 File Offset: 0x001272B4
		[CLSCompliant(false)]
		internal bool ᜂ(spr\u23E7.ᜀ A_0)
		{
			int a_ = 8;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (this.ᜁ(A_0))
					{
						num = 9;
						continue;
					}
					MsoOptions msoOptions = A_0.ᜈ();
					num = 3;
					continue;
				}
				case 1:
					num = 8;
					continue;
				case 2:
				{
					MsoOptions msoOptions;
					if (msoOptions == MsoOptions.NoLineDrawDash)
					{
						num = 5;
						continue;
					}
					return false;
				}
				case 3:
				{
					MsoOptions msoOptions;
					switch (msoOptions)
					{
					case MsoOptions.LineColor:
						goto IL_161;
					case MsoOptions.LineTransparency:
						goto IL_54;
					case MsoOptions.LineBackColor:
						goto IL_E9;
					case (MsoOptions)451:
					case (MsoOptions)454:
					case (MsoOptions)455:
					case (MsoOptions)456:
					case (MsoOptions)457:
					case (MsoOptions)458:
					case (MsoOptions)460:
						return false;
					case MsoOptions.ContainLinePattern:
						goto IL_150;
					case MsoOptions.LinePattern:
						goto IL_12F;
					case MsoOptions.LineWeight:
						goto IL_67;
					case MsoOptions.LineStyle:
						goto IL_13E;
					case MsoOptions.LineDashStyle:
						goto IL_208;
					default:
						num = 1;
						continue;
					}
					break;
				}
				case 5:
					goto IL_19B;
				case 6:
					goto IL_4F;
				case 7:
					num = 2;
					continue;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_47;
					default:
					{
						if (false)
						{
						}
						MsoOptions msoOptions;
						if (msoOptions != MsoOptions.ContainRoundDot)
						{
							num = 7;
							continue;
						}
						goto IL_126;
					}
					}
					break;
				case 9:
					return true;
				}
				goto IL_41;
				IL_47:
				num = 6;
				continue;
				IL_41:
				if (A_0 == null)
				{
					goto IL_47;
				}
				num = 0;
			}
			IL_4F:
			throw new ArgumentNullException(RecordTableEnumerator.b("儽〿㙁ⵃ⥅♇", a_));
			IL_54:
			this.ᜐ = XlsShapeLineFormat.ᜀ(A_0.ᜆ());
			return true;
			IL_67:
			this.ᜄ = A_0.ᜆ() / 12700.0;
			return true;
			IL_E9:
			this.ᜆ = XlsShapeFill.ᜀ(this.ᜇ, A_0.ᜃ());
			return true;
			IL_126:
			this.ᜎ = ShapeDashLineStyleType.DottedRound;
			return true;
			IL_12F:
			this.\u1712 = this.ᜀ(A_0);
			return true;
			IL_13E:
			this.ᜏ = (int)A_0.ᜆ() + ShapeLineStyleType.LineSingle;
			return true;
			IL_150:
			if (true)
			{
			}
			this.\u1713 = true;
			return true;
			IL_161:
			this.ᜅ = XlsShapeFill.ᜀ(this.ᜇ, A_0.ᜃ());
			return true;
			IL_19B:
			this.ᜁ(A_0.ᜃ());
			return true;
			IL_208:
			this.ᜎ = (ShapeDashLineStyleType)A_0.ᜆ();
			return true;
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x001284D8 File Offset: 0x001274D8
		private bool ᜁ(spr\u23E7.ᜀ A_0)
		{
			int a_ = 0;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return false;
				case 1:
					goto IL_38;
				case 3:
				{
					MsoOptions msoOptions;
					switch (msoOptions)
					{
					case MsoOptions.LineStartArrow:
						goto IL_FF;
					case MsoOptions.LineEndArrow:
						goto IL_48;
					case MsoOptions.StartArrowWidth:
						goto IL_93;
					case MsoOptions.StartArrowLength:
						goto IL_BD;
					case MsoOptions.EndArrowWidth:
						goto IL_E7;
					case MsoOptions.EndArrowLength:
						goto IL_3A;
					default:
						num = 4;
						continue;
					}
					break;
				}
				case 4:
					num = 0;
					continue;
				}
				if (A_0 == null)
				{
					num = 1;
				}
				else
				{
					MsoOptions msoOptions = A_0.ᜈ();
					num = 3;
				}
			}
			IL_38:
			throw new ArgumentNullException(RecordTableEnumerator.b("夵䠷丹唻儽⸿", a_));
			IL_3A:
			this.ᜋ = (ShapeArrowLengthType)A_0.ᜆ();
			return true;
			IL_48:
			this.ᜉ = (ShapeArrowStyleType)A_0.ᜆ();
			return true;
			IL_93:
			if (true)
			{
			}
			this.ᜌ = (ShapeArrowWidthType)A_0.ᜆ();
			return true;
			IL_BD:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return false;
			default:
				if (false)
				{
				}
				this.ᜊ = (ShapeArrowLengthType)A_0.ᜆ();
				return true;
			}
			IL_E7:
			this.\u170D = (ShapeArrowWidthType)A_0.ᜆ();
			return true;
			IL_FF:
			this.ᜈ = (ShapeArrowStyleType)A_0.ᜆ();
			return true;
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x00128600 File Offset: 0x00127600
		private GradientPatternType ᜀ(spr\u23E7.ᜀ A_0)
		{
			int a_ = 18;
			int num = 8;
			byte[] array;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.ᜄ() != null)
					{
						num = 7;
						continue;
					}
					goto IL_B5;
				case 1:
					goto IL_B5;
				case 2:
				{
					sprᜪ sprᜪ = this.ᜇ.ShapesData.ᜀ((int)A_0.ᜆ());
					MemoryStream memoryStream = new MemoryStream(sprᜪ.Length);
					sprᜪ.ᜀ(memoryStream, 0, null, null);
					array = new byte[sprᜪ.Length - 36];
					memoryStream.Position = 36L;
					memoryStream.Read(array, 0, array.Length);
					num = 9;
					continue;
				}
				case 3:
					goto IL_4F;
				case 4:
					goto IL_12F;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_47;
					default:
						if (false)
						{
						}
						if (A_0.ᜆ() > 0U)
						{
							if (true)
							{
							}
							num = 2;
							continue;
						}
						return GradientPatternType.Pat5Percent;
					}
					break;
				case 6:
					if (A_0.ᜄ().Length == 0)
					{
						num = 1;
						continue;
					}
					array = A_0.ᜄ();
					num = 4;
					continue;
				case 7:
					num = 6;
					continue;
				case 9:
					goto IL_B0;
				}
				goto IL_41;
				IL_47:
				num = 3;
				continue;
				IL_41:
				if (A_0 == null)
				{
					goto IL_47;
				}
				num = 0;
				continue;
				IL_B5:
				num = 5;
			}
			IL_4F:
			throw new ArgumentNullException(RecordTableEnumerator.b("❇㩉㡋❍㽏㱑", a_));
			IL_B0:
			IL_12F:
			return this.ᜀ(array);
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x0012878C File Offset: 0x0012778C
		private void ᜁ(byte[] A_0)
		{
			int a_ = 6;
			byte b;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_1AE:
				if (b <= 16)
				{
					goto IL_15E;
				}
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
				byte b2;
				switch (num)
				{
				case 1:
					if (A_0[5] == 0)
					{
						num = 19;
						continue;
					}
					goto IL_13E;
				case 2:
					if (b != 24)
					{
						num = 12;
						continue;
					}
					goto IL_15E;
				case 3:
					goto IL_95;
				case 4:
					num = 14;
					continue;
				case 5:
					goto IL_1C3;
				case 6:
					goto IL_128;
				case 7:
					goto IL_1AE;
				case 8:
					if (b >= 8)
					{
						num = 18;
						continue;
					}
					goto IL_95;
				case 9:
					if (true)
					{
					}
					if (b2 >= 8)
					{
						num = 4;
						continue;
					}
					goto IL_1C3;
				case 10:
					num = 1;
					continue;
				case 11:
					if (b == 24)
					{
						num = 15;
						continue;
					}
					goto IL_1E3;
				case 12:
					goto IL_13E;
				case 13:
					if (b2 == 16)
					{
						num = 6;
						continue;
					}
					goto IL_1E3;
				case 14:
					if (b2 > 16)
					{
						num = 5;
						continue;
					}
					goto IL_13E;
				case 15:
					num = 13;
					continue;
				case 16:
					if (A_0[3] == 0)
					{
						num = 10;
						continue;
					}
					goto IL_13E;
				case 17:
					goto IL_90;
				case 18:
					num = 7;
					continue;
				case 19:
					num = 8;
					continue;
				}
				if (A_0 == null)
				{
					num = 17;
					continue;
				}
				b = A_0[4];
				b2 = A_0[2];
				num = 9;
				continue;
				IL_95:
				num = 2;
				continue;
				IL_13E:
				num = 11;
				continue;
				IL_1C3:
				num = 16;
			}
			IL_90:
			throw new ArgumentNullException(RecordTableEnumerator.b("堻弽㐿⍁", a_));
			IL_128:
			goto IL_15E;
			IL_1E3:
			this.ᜑ = true;
			return;
			IL_15E:
			this.ᜑ = false;
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x00128984 File Offset: 0x00127984
		[CLSCompliant(false)]
		internal void ᜅ(spr\u23E7 A_0)
		{
			int a_ = 13;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜑ)
					{
						num = 1;
						continue;
					}
					return;
				case 1:
					XlsShapeLineFormat.ᜀ(A_0, this.ᜅ, this.ᜇ, MsoOptions.LineColor);
					XlsShapeLineFormat.ᜀ(A_0, this.ᜆ, this.ᜇ, MsoOptions.LineBackColor);
					this.ᜄ(A_0);
					this.ᜃ(A_0);
					this.ᜂ(A_0);
					XlsShapeLineFormat.ᜀ(A_0, MsoOptions.LineTransparency, this.ᜐ);
					this.ᜀ(A_0);
					num = 7;
					continue;
				case 2:
					goto IL_158;
				case 3:
					goto IL_63;
				case 4:
					XlsShape.ᜀ(A_0, MsoOptions.LineWeight, (int)(this.ᜄ * 12700.0));
					num = 6;
					continue;
				case 6:
					goto IL_65;
				case 7:
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_158:
					if (this.ᜑ)
					{
						num = 4;
						continue;
					}
					break;
				default:
					if (false)
					{
					}
					if (A_0 == null)
					{
						num = 3;
						continue;
					}
					num = 2;
					continue;
				}
				IL_65:
				if (true)
				{
				}
				this.ᜁ(A_0);
				num = 0;
			}
			IL_63:
			throw new ArgumentNullException(RecordTableEnumerator.b("ⱂ㕄㍆", a_));
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x00128B04 File Offset: 0x00127B04
		private void ᜄ(spr\u23E7 A_0)
		{
			int a_ = 5;
			if (A_0 != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_0C;
				}
				if (false)
				{
				}
				XlsShape.ᜀ(A_0, MsoOptions.LineStartArrow, (int)this.ᜈ);
				XlsShape.ᜀ(A_0, MsoOptions.LineEndArrow, (int)this.ᜉ);
				XlsShape.ᜀ(A_0, MsoOptions.StartArrowLength, (int)this.ᜊ);
				XlsShape.ᜀ(A_0, MsoOptions.EndArrowLength, (int)this.ᜋ);
				XlsShape.ᜀ(A_0, MsoOptions.StartArrowWidth, (int)this.ᜌ);
				XlsShape.ᜀ(A_0, MsoOptions.EndArrowWidth, (int)this.\u170D);
				return;
			}
			IL_0C:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("吺䴼䬾", a_));
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x00128BC8 File Offset: 0x00127BC8
		private void ᜃ(spr\u23E7 A_0)
		{
			int a_ = 14;
			ShapeDashLineStyleType shapeDashLineStyleType;
			for (;;)
			{
				shapeDashLineStyleType = this.ᜎ;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						shapeDashLineStyleType = ShapeDashLineStyleType.Dotted;
						XlsShape.ᜀ(A_0, MsoOptions.ContainRoundDot, 0);
						if (true)
						{
						}
						num = 2;
						continue;
					case 1:
						goto IL_4D;
					case 2:
						goto IL_6D;
					case 3:
						goto IL_CB;
					case 4:
						if (shapeDashLineStyleType == ShapeDashLineStyleType.DottedRound)
						{
							num = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_45;
						default:
							if (false)
							{
							}
							A_0.ᜀ(471);
							num = 3;
							continue;
						}
						break;
					case 5:
						if (A_0 == null)
						{
							goto IL_45;
						}
						num = 4;
						continue;
					}
					break;
					IL_45:
					num = 1;
				}
			}
			IL_4D:
			throw new ArgumentNullException(RecordTableEnumerator.b("⭃㙅㱇", a_));
			IL_6D:
			IL_CB:
			XlsShape.ᜀ(A_0, MsoOptions.LineDashStyle, (int)shapeDashLineStyleType);
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x00128CB0 File Offset: 0x00127CB0
		private void ᜂ(spr\u23E7 A_0)
		{
			int a_ = 3;
			if (A_0 != null)
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
					int num = (int)this.ᜏ;
					XlsShape.ᜀ(A_0, MsoOptions.LineStyle, num - 1);
					return;
				}
				}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("嘸䬺䤼", a_));
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x00128D24 File Offset: 0x00127D24
		private void ᜁ(spr\u23E7 A_0)
		{
			int a_ = 6;
			int num = 0;
			byte[] array;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (true)
					{
					}
					array[0] = 0;
					array[2] = 24;
					num = 4;
					continue;
				case 2:
					goto IL_8C;
				case 3:
					goto IL_5E;
				case 4:
					goto IL_71;
				}
				if (A_0 == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8C;
					}
					if (false)
					{
					}
					num = 3;
					continue;
				}
				byte[] array2 = new byte[4];
				array2[0] = 8;
				array2[2] = 8;
				array = array2;
				num = 2;
				continue;
				IL_8C:
				if (this.ᜑ)
				{
					goto IL_BD;
				}
				num = 1;
			}
			IL_5E:
			throw new ArgumentNullException(RecordTableEnumerator.b("医丽㐿", a_));
			IL_71:
			IL_BD:
			XlsShape.ᜀ(A_0, MsoOptions.NoLineDrawDash, array);
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x00128DFC File Offset: 0x00127DFC
		private void ᜀ(spr\u23E7 A_0)
		{
			int a_ = 4;
			int num = 0;
			byte[] resData;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (this.\u1713)
					{
						num = 3;
						continue;
					}
					goto IL_E2;
				case 2:
					goto IL_34;
				case 3:
					goto IL_C9;
				}
				IL_29:
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
					if (true)
					{
					}
					if (false)
					{
					}
					int u = (int)this.\u1712;
					string str = u.ToString();
					resData = XlsShapeFill.GetResData(RecordTableEnumerator.b("樹崻䨽㐿", a_) + str);
					num = 1;
					continue;
				}
				}
				goto IL_29;
			}
			IL_34:
			throw new ArgumentNullException(RecordTableEnumerator.b("唹䰻䨽", a_));
			IL_C9:
			XlsShape.ᜀ(A_0, MsoOptions.LinePattern, 0, resData, true);
			XlsShape.ᜀ(A_0, MsoOptions.ContainLinePattern, 1);
			return;
			IL_E2:
			A_0.ᜀ(453);
			A_0.ᜀ(452);
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x00128F04 File Offset: 0x00127F04
		private GradientPatternType ᜀ(byte[] A_0)
		{
			int a_ = 1;
			switch (0)
			{
			default:
			{
				int num = 0;
				for (;;)
				{
					int num2;
					int num4;
					int num5;
					switch (num)
					{
					case 1:
						return (GradientPatternType)num2;
					case 2:
						goto IL_7A;
					case 3:
						goto IL_1A3;
					case 4:
					{
						int num3;
						if (num3 >= num4)
						{
							num = 2;
							continue;
						}
						goto IL_146;
					}
					case 5:
					{
						int num3;
						num3++;
						num = 15;
						continue;
					}
					case 6:
						return GradientPatternType.Pat5Percent;
					case 7:
					{
						if (num5 >= XlsShapeLineFormat.ᜃ.Length)
						{
							num = 6;
							continue;
						}
						int num3 = 0;
						num5++;
						num = 13;
						continue;
					}
					case 8:
						goto IL_75;
					case 9:
						num = 3;
						continue;
					case 10:
						goto IL_11F;
					case 11:
					{
						int num3;
						if (A_0[num3] == XlsShapeLineFormat.ᜃ[num5 + num3])
						{
							num = 5;
							continue;
						}
						goto IL_7A;
					}
					case 12:
						goto IL_11F;
					case 13:
						if ((int)XlsShapeLineFormat.ᜃ[num5 - 1] == num4)
						{
							num = 9;
							continue;
						}
						goto IL_EB;
					case 14:
					{
						int num3;
						if (num3 == num4)
						{
							num = 1;
							continue;
						}
						goto IL_EB;
					}
					case 15:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_146;
						default:
							if (false)
							{
							}
							goto IL_1A3;
						}
						break;
					}
					if (A_0 == null)
					{
						num = 8;
						continue;
					}
					num2 = 1;
					num5 = 0;
					num4 = A_0.Length;
					num = 10;
					continue;
					IL_7A:
					if (true)
					{
					}
					num = 14;
					continue;
					IL_EB:
					num5 += (int)XlsShapeLineFormat.ᜃ[num5 - 1];
					num2++;
					num = 12;
					continue;
					IL_11F:
					num = 7;
					continue;
					IL_146:
					num = 11;
					continue;
					IL_1A3:
					num = 4;
				}
				IL_75:
				throw new ArgumentNullException(RecordTableEnumerator.b("嘶䬸䤺", a_));
			}
			}
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x001290E4 File Offset: 0x001280E4
		public XlsShapeLineFormat Clone(object parent)
		{
			int a_ = 13;
			if (parent != null)
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
					if (true)
					{
					}
					XlsShapeLineFormat xlsShapeLineFormat = (XlsShapeLineFormat)base.MemberwiseClone();
					xlsShapeLineFormat.SetParent(parent);
					xlsShapeLineFormat.ᜀ();
					return xlsShapeLineFormat;
				}
				}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㍂⑄㕆ⱈ╊㥌", a_));
		}

		// Token: 0x0400115A RID: 4442
		private const double ᜀ = 12700.0;

		// Token: 0x0400115B RID: 4443
		private const int ᜁ = 1584;

		// Token: 0x0400115C RID: 4444
		private const int ᜂ = 5088;

		// Token: 0x0400115D RID: 4445
		private static byte[] ᜃ;

		// Token: 0x0400115E RID: 4446
		private double ᜄ = 0.75;

		// Token: 0x0400115F RID: 4447
		private int[] \u2593\u0081\u008B\u0081;

		// Token: 0x04001160 RID: 4448
		private Color ᜅ = spr\u1D39.ᜀ;

		// Token: 0x04001161 RID: 4449
		private Color ᜆ = spr\u1D39.ᜁ;

		// Token: 0x04001162 RID: 4450
		private XlsWorkbook ᜇ;

		// Token: 0x04001163 RID: 4451
		private int \u25D8\u00A1\u009C\u00AE;

		// Token: 0x04001164 RID: 4452
		private ShapeArrowStyleType ᜈ;

		// Token: 0x04001165 RID: 4453
		private ShapeArrowStyleType ᜉ;

		// Token: 0x04001166 RID: 4454
		private ShapeArrowLengthType ᜊ = ShapeArrowLengthType.ArrowHeadMedium;

		// Token: 0x04001167 RID: 4455
		private ShapeArrowLengthType ᜋ = ShapeArrowLengthType.ArrowHeadMedium;

		// Token: 0x04001168 RID: 4456
		private byte[] \u2609\u009B\u009C\u009F;

		// Token: 0x04001169 RID: 4457
		private ShapeArrowWidthType ᜌ = ShapeArrowWidthType.ArrowHeadMedium;

		// Token: 0x0400116A RID: 4458
		private ShapeArrowWidthType \u170D = ShapeArrowWidthType.ArrowHeadMedium;

		// Token: 0x0400116B RID: 4459
		private ShapeDashLineStyleType ᜎ;

		// Token: 0x0400116C RID: 4460
		private ShapeLineStyleType ᜏ = ShapeLineStyleType.LineSingle;

		// Token: 0x0400116D RID: 4461
		private double ᜐ;

		// Token: 0x0400116E RID: 4462
		private bool ᜑ = true;

		// Token: 0x0400116F RID: 4463
		private int[] \u25D9\u0081\u0093\u00AF;

		// Token: 0x04001170 RID: 4464
		private GradientPatternType \u1712 = GradientPatternType.Pat5Percent;

		// Token: 0x04001171 RID: 4465
		private bool \u1713;

		// Token: 0x04001172 RID: 4466
		private bool \u1714;
	}
}
