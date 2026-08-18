using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Shapes
{
	// Token: 0x02000160 RID: 352
	public class XlsBitmapShape : XlsShape, IDisposable, IPictureShape
	{
		// Token: 0x06000FD5 RID: 4053 RVA: 0x0009F940 File Offset: 0x0009E940
		internal XlsBitmapShape(spr\u1DF5 A_0, object A_1) : this(A_0, A_1, true)
		{
			this.m_bSupportOptions = true;
			base.ShapeType = ExcelShapeType.Picture;
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0009F968 File Offset: 0x0009E968
		internal XlsBitmapShape(spr\u1DF5 A_0, object A_1, bool A_2)
		{
			this.cropOptions = new MsoOptions[]
			{
				MsoOptions.CropFromBottom,
				MsoOptions.CropFromLeft,
				MsoOptions.CropFromRight,
				MsoOptions.CropFromTop
			};
			base..ctor(A_0, A_1);
			this.m_bSupportOptions = true;
			if (A_2)
			{
				this.m_bUpdateLineFill = true;
				base.Fill.Visible = false;
				base.Line.Visible = false;
			}
			base.ShapeType = ExcelShapeType.Picture;
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0009F9E0 File Offset: 0x0009E9E0
		internal XlsBitmapShape(spr\u1DF5 A_0, object A_1, spr\u1D3B[] A_2, int A_3)
		{
			this.cropOptions = new MsoOptions[]
			{
				MsoOptions.CropFromBottom,
				MsoOptions.CropFromLeft,
				MsoOptions.CropFromRight,
				MsoOptions.CropFromTop
			};
			base..ctor(A_0, A_1, A_2, A_3);
			this.m_bSupportOptions = true;
			base.ShapeType = ExcelShapeType.Picture;
			this.ᜃ = this.ᜂ.ᜄ().ᜀ();
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0009FA4C File Offset: 0x0009EA4C
		internal XlsBitmapShape(spr\u1DF5 A_0, object A_1, sprὙ A_2)
		{
			this.cropOptions = new MsoOptions[]
			{
				MsoOptions.CropFromBottom,
				MsoOptions.CropFromLeft,
				MsoOptions.CropFromRight,
				MsoOptions.CropFromTop
			};
			base..ctor(A_0, A_1, A_2, ExcelParseOptions.Default);
			this.m_bSupportOptions = true;
			base.ShapeType = ExcelShapeType.Picture;
			if (this.ᜂ != null)
			{
				this.ᜃ = this.ᜂ.ᜄ().ᜀ();
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0009FAC4 File Offset: 0x0009EAC4
		// (set) Token: 0x06000FDA RID: 4058 RVA: 0x0009FB08 File Offset: 0x0009EB08
		public string FileName
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
				return this.ᜁ;
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
				this.ᜁ = value;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0009FB4C File Offset: 0x0009EB4C
		// (set) Token: 0x06000FDC RID: 4060 RVA: 0x0009FB90 File Offset: 0x0009EB90
		[CLSCompliant(false)]
		public uint BlipId
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
				this.ᜀ = value;
				this.ᜂ = this.m_shapes.ShapeData.Pictures[(int)(value - 1U)];
				this.ᜃ = this.ᜂ.ᜄ().ᜀ();
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0009FC08 File Offset: 0x0009EC08
		internal int CropLeftOffset
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
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0009FC4C File Offset: 0x0009EC4C
		internal int CropRightOffset
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
				return this.ᜋ;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x0009FC90 File Offset: 0x0009EC90
		internal int CropBottomOffset
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
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0009FCD4 File Offset: 0x0009ECD4
		internal int CropTopOffset
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
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x0009FD18 File Offset: 0x0009ED18
		// (set) Token: 0x06000FE2 RID: 4066 RVA: 0x0009FD5C File Offset: 0x0009ED5C
		public Image Picture
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
				int a_ = 0;
				if (value == null)
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
						throw new ArgumentNullException(RecordTableEnumerator.b("琵儷丹儻弽〿", a_));
					}
				}
				XlsWorkbookShapeData shapeData = this.m_shapes.ShapeData;
				shapeData.RemovePicture(this.BlipId, true);
				int blipId = shapeData.AddPicture(value, ImageFormatType.Png, this.Name);
				this.BlipId = (uint)blipId;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x0009FDE8 File Offset: 0x0009EDE8
		// (set) Token: 0x06000FE4 RID: 4068 RVA: 0x0009FE2C File Offset: 0x0009EE2C
		internal Stream BlipSubNodesStream
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
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜄ = value;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x0009FE70 File Offset: 0x0009EE70
		// (set) Token: 0x06000FE6 RID: 4070 RVA: 0x0009FEB4 File Offset: 0x0009EEB4
		internal Stream ShapePropertiesStream
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
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x0009FEF8 File Offset: 0x0009EEF8
		// (set) Token: 0x06000FE8 RID: 4072 RVA: 0x0009FF3C File Offset: 0x0009EF3C
		internal Stream SourceRectStream
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
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x0009FF80 File Offset: 0x0009EF80
		public override int Instance
		{
			get
			{
				if (this.ᜏ == null)
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
						return 75;
					}
				}
				return this.ᜏ.\u1714();
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x0009FFD4 File Offset: 0x0009EFD4
		// (set) Token: 0x06000FEB RID: 4075 RVA: 0x000A0018 File Offset: 0x0009F018
		public string Macro
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

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x000A005C File Offset: 0x0009F05C
		// (set) Token: 0x06000FED RID: 4077 RVA: 0x000A00A0 File Offset: 0x0009F0A0
		public bool IsDDE
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
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜈ = value;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x000A00E4 File Offset: 0x0009F0E4
		// (set) Token: 0x06000FEF RID: 4079 RVA: 0x000A0128 File Offset: 0x0009F128
		public bool IsCamera
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
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜉ = value;
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x000A016C File Offset: 0x0009F16C
		internal override bool ParseOption(spr\u23E7.ᜀ option)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					num = 4;
					continue;
				case 2:
					return true;
				case 3:
				{
					if (true)
					{
					}
					MsoOptions msoOptions;
					switch (msoOptions)
					{
					case MsoOptions.CropFromTop:
					case MsoOptions.CropFromBottom:
					case MsoOptions.CropFromLeft:
					case MsoOptions.CropFromRight:
						goto IL_4F;
					case MsoOptions.BlipId:
						goto IL_CC;
					case MsoOptions.BlipName:
						goto IL_37;
					default:
						num = 1;
						continue;
					}
					break;
				}
				case 4:
					return false;
				}
				if (base.ParseOption(option))
				{
					num = 2;
				}
				else
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
						MsoOptions msoOptions = option.ᜈ();
						break;
					}
					}
					num = 3;
				}
			}
			return true;
			IL_37:
			this.ParseBlipName(option);
			return true;
			IL_4F:
			this.ᜀ(option);
			return true;
			IL_CC:
			this.ParseBlipId(option);
			return true;
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x000A0250 File Offset: 0x0009F250
		internal void ᜀ(spr\u23E7.ᜀ A_0)
		{
			int a_ = 1;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_79;
				case 1:
					if (Array.IndexOf<MsoOptions>(this.cropOptions, A_0.ᜈ()) < 0)
					{
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A8;
					default:
					{
						if (false)
						{
						}
						MsoOptions msoOptions = A_0.ᜈ();
						num = 3;
						continue;
					}
					}
					break;
				case 2:
					return;
				case 3:
				{
					MsoOptions msoOptions;
					switch (msoOptions)
					{
					case MsoOptions.CropFromTop:
						goto IL_136;
					case MsoOptions.CropFromBottom:
						goto IL_3E;
					case MsoOptions.CropFromLeft:
						goto IL_A8;
					case MsoOptions.CropFromRight:
						goto IL_7E;
					default:
						num = 2;
						continue;
					}
					break;
				}
				case 4:
					goto IL_3C;
				}
				if (A_0 == null)
				{
					num = 4;
				}
				else
				{
					num = 1;
				}
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("堶䤸伺吼倾⽀", a_));
			IL_3E:
			this.ᜌ = A_0.ᜅ() + A_0.ᜅ() / 2;
			return;
			IL_79:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("琶䬸吺䴼Ἶ⹀㍂ㅄ⹆♈╊浌⩎⥐⍒ご㑖ⵘ㹚㥜", a_));
			IL_7E:
			this.ᜋ = A_0.ᜅ() + A_0.ᜅ() / 2;
			return;
			IL_A8:
			this.ᜊ = A_0.ᜅ() + A_0.ᜅ() / 2;
			return;
			IL_136:
			if (true)
			{
			}
			this.\u170D = A_0.ᜅ() + A_0.ᜅ() / 2;
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x000A03B0 File Offset: 0x0009F3B0
		internal virtual void ParseBlipId(spr\u23E7.ᜀ option)
		{
			int a_ = 15;
			if (true)
			{
			}
			int num = 0;
			IList list;
			for (;;)
			{
				switch (num)
				{
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_42;
					default:
						if (false)
						{
						}
						if (option.ᜈ() != MsoOptions.BlipId)
						{
							num = 2;
							continue;
						}
						this.ᜀ = option.ᜆ();
						list = this.m_shapes.ShapeData.Pictures;
						num = 4;
						continue;
					}
					break;
				case 2:
					goto IL_99;
				case 3:
					goto IL_4A;
				case 4:
					goto IL_D7;
				}
				goto IL_3F;
				IL_42:
				num = 3;
				continue;
				IL_3F:
				if (option == null)
				{
					goto IL_42;
				}
				num = 1;
			}
			IL_4A:
			throw new ArgumentNullException(RecordTableEnumerator.b("⩄㝆㵈≊≌ⅎ", a_));
			IL_99:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("၄⥆ⱈ㍊㵌⩎㉐❒ご㍖祘ᥚㅜ㙞ᅠ⩢Ť䝦٨᭪ᥬٮṰᵲ孴", a_));
			IL_D7:
			this.ᜂ = ((this.ᜀ > 0U) ? ((sprᜪ)list[(int)(this.ᜀ - 1U)]) : null);
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x000A04C0 File Offset: 0x0009F4C0
		internal virtual void ParseBlipName(spr\u23E7.ᜀ option)
		{
			int a_ = 19;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_107;
				case 1:
					goto IL_D5;
				case 2:
					if (option.ᜈ() != MsoOptions.BlipName)
					{
						num = 0;
						continue;
					}
					num = 6;
					continue;
				case 3:
					goto IL_43;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
					{
						if (false)
						{
						}
						byte[] array = option.ᜄ();
						this.ᜁ = Encoding.Unicode.GetString(array, 0, array.Length);
						num = 1;
						continue;
					}
					}
					break;
				case 6:
					if (option.ᜄ() != null)
					{
						num = 4;
						continue;
					}
					return;
				}
				if (option == null)
				{
					num = 3;
				}
				else
				{
					num = 2;
				}
			}
			IL_43:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("♈㭊㥌♎㹐㵒", a_));
			IL_D5:
			return;
			IL_107:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("᱈╊⡌㝎⅐㙒㙔⍖㱘㽚絜ᵞൠ੢ᕤ⥦ࡨ٪࡬佮ṰͲŴṶᙸᕺ卼", a_));
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x000A05DC File Offset: 0x0009F5DC
		[CLSCompliant(false)]
		internal override bool ExtractNecessaryOption(spr\u23E7.ᜀ option)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return true;
				case 1:
					num = 4;
					continue;
				case 2:
				{
					MsoOptions msoOptions;
					switch (msoOptions)
					{
					case MsoOptions.BlipId:
						goto IL_37;
					case MsoOptions.BlipName:
						goto IL_4C;
					default:
						num = 1;
						continue;
					}
					break;
				}
				case 4:
					return false;
				}
				if (base.ExtractNecessaryOption(option))
				{
					num = 0;
				}
				else
				{
					if (true)
					{
					}
					MsoOptions msoOptions = option.ᜈ();
					num = 2;
				}
			}
			return true;
			IL_37:
			this.ParseBlipId(option);
			return true;
			IL_4C:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return true;
			default:
				if (false)
				{
				}
				this.ParseBlipName(option);
				return true;
			}
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x000A06A0 File Offset: 0x0009F6A0
		public new void Dispose()
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
			base.Dispose();
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x000A06E4 File Offset: 0x0009F6E4
		internal override void SerializeShape(spr\u21EB spgrContainer)
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
			sprὙ sprὙ = (sprὙ)spr\u231F.ᜀ(MsoRecords.msofbtSpContainer);
			sprὙ.ᜀ(this.ᜏ);
			this.ᜂ(sprὙ);
			this.ᜁ(sprὙ);
			this.ᜀ(sprὙ);
			spgrContainer.ᜀ(sprὙ);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x000A0758 File Offset: 0x0009F758
		protected override void OnPrepareForSerialization()
		{
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_40;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_40;
					default:
						goto IL_80;
					}
					break;
				}
				if (this.ᜏ == null)
				{
					num = 0;
					continue;
				}
				goto IL_88;
				IL_40:
				this.ᜏ = (sprἼ)spr\u231F.ᜀ(MsoRecords.msofbtSp);
				this.ᜏ.ᜈ(75);
				num = 1;
			}
			IL_80:
			if (false)
			{
			}
			IL_88:
			this.ᜏ.ᜆ(true);
			this.ᜏ.ᜇ(true);
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x000A0808 File Offset: 0x0009F808
		private new void ᜂ(sprὙ A_0)
		{
			spr\u23E7 spr_u23E;
			for (;;)
			{
				spr_u23E = this.\u1712;
				bool flag = spr_u23E == null;
				int num = 11;
				for (;;)
				{
					switch (num)
					{
					case 0:
						spr_u23E = base.ᜆ(spr_u23E);
						num = 4;
						continue;
					case 1:
						this.ᜁ += '\0';
						num = 9;
						continue;
					case 2:
						goto IL_FD;
					case 3:
						if (this.m_bUpdateLineFill)
						{
							num = 0;
							continue;
						}
						goto IL_94;
					case 4:
						goto IL_94;
					case 5:
						num = 10;
						continue;
					case 6:
						spr_u23E = this.CreateDefaultOptions();
						base.ᜀ(spr_u23E, MsoOptions.NoLineDrawDash, 524296U);
						base.ᜀ(spr_u23E, MsoOptions.NoFillHitTest, 1048576U);
						num = 2;
						continue;
					case 7:
						if (this.ᜁ != null)
						{
							num = 5;
							continue;
						}
						goto IL_211;
					case 8:
						goto IL_197;
					case 9:
						goto IL_12A;
					case 10:
						if (this.ᜁ[this.ᜁ.Length - 1] != '\0')
						{
							num = 1;
							continue;
						}
						goto IL_12A;
					case 11:
						if (flag)
						{
							goto IL_52;
						}
						goto IL_FD;
					}
					break;
					IL_52:
					num = 6;
					continue;
					IL_94:
					spr\u23E7.ᜀ ᜀ = new spr\u23E7.ᜀ();
					ᜀ.ᜀ(MsoOptions.BlipId);
					ᜀ.ᜀ(this.ᜀ);
					ᜀ.ᜀ(true);
					ᜀ.ᜁ(false);
					spr_u23E.ᜂ(ᜀ);
					ᜀ = new spr\u23E7.ᜀ();
					ᜀ.ᜀ(MsoOptions.BlipName);
					num = 7;
					continue;
					IL_FD:
					num = 3;
					continue;
					IL_12A:
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
						ᜀ.ᜀ((uint)(this.ᜁ.Length * 2));
						ᜀ.ᜀ(true);
						ᜀ.ᜁ(true);
						ᜀ.ᜀ(Encoding.Unicode.GetBytes(this.ᜁ));
						spr_u23E.ᜂ(ᜀ);
						num = 8;
						break;
					}
				}
			}
			IL_197:
			IL_211:
			base.ᜇ(spr_u23E);
			base.ᜀ(spr_u23E, MsoOptions.AlternativeText, base.AlternativeText);
			spr_u23E.ᜉ(3);
			spr_u23E.ᜈ(2);
			A_0.ᜀ(spr_u23E);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x000A0A54 File Offset: 0x0009FA54
		private new void ᜁ(sprὙ A_0)
		{
			int a_ = 10;
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
					throw new ArgumentNullException(RecordTableEnumerator.b("㌿㉁݃⥅♇㹉ⵋ❍㹏㝑♓", a_));
				}
			}
			A_0.ᜀ(base.ClientAnchor);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x000A0AC0 File Offset: 0x0009FAC0
		private void ᜀ(sprὙ A_0)
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				int num = 6;
				spr\u2003 spr_u;
				spr\u2223 spr_u2;
				spr᪙ spr᪙;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						spr_u = (spr\u2003)spr\u175E.ᜀ(TBIFFRecord.OBJ);
						spr_u2 = new spr\u2223();
						spr_u2.ᜀ(TObjType.otPicture);
						spr_u2.ᜀ(true);
						spr_u2.ᜂ(true);
						spr_u2.ᜄ(true);
						spr_u2.ᜃ(true);
						sprទ a_2 = new sprទ();
						spr_u.ᜀ(spr_u2);
						spr_u.ᜀ(a_2);
						num = 5;
						continue;
					}
					case 1:
						if (spr_u == null)
						{
							num = 0;
							continue;
						}
						goto IL_11D;
					case 2:
						if (A_0 == null)
						{
							num = 4;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_11D;
						default:
							if (false)
							{
							}
							spr᪙ = (spr᪙)spr\u231F.ᜀ(MsoRecords.msofbtClientData);
							spr_u = base.Obj;
							num = 1;
							continue;
						}
						break;
					case 3:
						goto IL_66;
					case 4:
						goto IL_FE;
					case 5:
						goto IL_66;
					case 7:
						goto IL_79;
					case 8:
						return;
					}
					if (base.IsShortVersion)
					{
						num = 8;
						continue;
					}
					num = 2;
					continue;
					IL_66:
					num = 7;
					continue;
					IL_11D:
					spr_u2 = (spr\u2223)spr_u.ᜀ()[0];
					num = 3;
				}
				return;
				IL_79:
				spr_u2.ᜁ((base.OldObjId > 0U) ? ((ushort)base.OldObjId) : ((ushort)base.ParentWorkbook.CurrentObjectId));
				spr᪙.ᜀ(spr_u);
				A_0.ᜀ(spr᪙);
				return;
				IL_FE:
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("㑆㥈ࡊ≌ⅎ═㉒㱔㥖㱘⥚", a_));
			}
			}
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x000A0C8C File Offset: 0x0009FC8C
		internal override void RegisterInSubCollection()
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
			this.m_shapes.WorksheetBase.InnerPictures.ᜀ(this);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x000A0CE0 File Offset: 0x0009FCE0
		protected override void OnDelete()
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
			this.OnDelete(true);
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x000A0D24 File Offset: 0x0009FD24
		protected void OnDelete(bool removeImage)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_41;
					default:
						goto IL_84;
					}
					break;
				case 2:
					if (true)
					{
					}
					goto IL_41;
				}
				if (this.BlipId > 0U)
				{
					num = 2;
					continue;
				}
				goto IL_8C;
				IL_41:
				XlsWorkbook workbook = base.ParentShapes.Workbook;
				workbook.ShapesData.RemovePicture(this.BlipId, removeImage);
				this.ᜀ = 0U;
				num = 1;
			}
			IL_84:
			if (false)
			{
			}
			IL_8C:
			XlsPicturesCollection xlsPicturesCollection = (XlsPicturesCollection)this.m_shapes.Worksheet.Pictures;
			xlsPicturesCollection.ᜁ(this);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x000A0DDC File Offset: 0x0009FDDC
		public void Remove(bool removeImage)
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
			this.OnDelete(removeImage);
			this.m_shapes.Remove(this);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x000A0E2C File Offset: 0x0009FE2C
		internal void ᜀ(XlsShape A_0)
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
			base.SetParent(A_0.Parent);
			this.SetParents();
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x000A0E7C File Offset: 0x0009FE7C
		public override IShape Clone(object parent, Dictionary<string, string> hashNewNames, Dictionary<int, int> dicFontIndexes, bool addToCollection)
		{
			switch (0)
			{
			default:
			{
				XlsBitmapShape xlsBitmapShape;
				for (;;)
				{
					bool flag = true;
					spr\u1D9B spr_u1D9B = XlsObject.FindParent(parent, typeof(spr\u1D9B), true) as spr\u1D9B;
					spr\u1D9B spr_u1D9B2 = spr_u1D9B;
					int num = 25;
					for (;;)
					{
						XlsWorksheetBase xlsWorksheetBase;
						int num2;
						XlsWorkbook parentWorkbook;
						XlsWorkbookShapeData xlsWorkbookShapeData;
						XlsWorkbook parentWorkbook2;
						XlsWorkbookShapeData xlsWorkbookShapeData2;
						XlsWorkbookShapeData xlsWorkbookShapeData3;
						switch (num)
						{
						case 0:
							goto IL_3AE;
						case 1:
							goto IL_2EB;
						case 2:
							if (!flag)
							{
								num = 3;
								continue;
							}
							goto IL_27E;
						case 3:
							num = 6;
							continue;
						case 4:
							flag = (0 == 0);
							num = 20;
							continue;
						case 5:
							xlsBitmapShape = (xlsWorksheetBase.HeaderFooterShapes.SetPicture(this.Name, this.Picture, num2, false) as XlsBitmapShape);
							xlsBitmapShape.\u1712 = (spr\u23E7)spr\u1CD3.ᜀ(this.\u1712);
							xlsBitmapShape.ᜆ = spr\u1CD3.ᜀ(this.ᜆ);
							xlsBitmapShape.ᜄ = spr\u1CD3.ᜀ(this.ᜄ);
							xlsBitmapShape.ᜅ = spr\u1CD3.ᜀ(this.ᜅ);
							xlsBitmapShape.AttachEvents();
							num = 24;
							continue;
						case 6:
							if (!addToCollection)
							{
								num = 21;
								continue;
							}
							num = 12;
							continue;
						case 7:
							xlsWorksheetBase = spr_u1D9B.WorksheetBase;
							num = 0;
							continue;
						case 8:
							if (!flag)
							{
								num = 9;
								continue;
							}
							num = 27;
							continue;
						case 9:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_CE;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								num = 29;
								continue;
							}
							break;
						case 10:
							num = 17;
							continue;
						case 11:
							if (!flag)
							{
								num = 10;
								continue;
							}
							num = 13;
							continue;
						case 12:
							if (!flag)
							{
								num = 5;
								continue;
							}
							goto IL_41C;
						case 13:
							xlsWorkbookShapeData = parentWorkbook.ShapesData;
							goto IL_386;
						case 14:
							if (parentWorkbook != parentWorkbook2)
							{
								num = 30;
								continue;
							}
							goto IL_192;
						case 15:
							goto IL_192;
						case 16:
							if (base.ImageRelation != null)
							{
								num = 19;
								continue;
							}
							return xlsBitmapShape;
						case 17:
							xlsWorkbookShapeData = parentWorkbook.HeaderFooterData;
							goto IL_386;
						case 18:
							if (flag)
							{
								num = 4;
								continue;
							}
							goto IL_33F;
						case 19:
							xlsBitmapShape.ImageRelation = (sprᦨ)base.ImageRelation.ᜁ();
							num = 28;
							continue;
						case 20:
							goto IL_33F;
						case 21:
							goto IL_27E;
						case 22:
							goto IL_232;
						case 23:
							if (addToCollection)
							{
								num = 26;
								continue;
							}
							goto IL_2EB;
						case 24:
							goto IL_232;
						case 25:
							if (spr_u1D9B2 != null)
							{
								goto IL_CE;
							}
							xlsWorksheetBase = (XlsObject.FindParent(parent, typeof(XlsWorksheetBase), true) as XlsWorksheetBase);
							flag = false;
							num = 31;
							continue;
						case 26:
							xlsWorksheetBase.InnerShapes.ᜀ(xlsBitmapShape);
							num = 1;
							continue;
						case 27:
							xlsWorkbookShapeData2 = parentWorkbook2.ShapesData;
							goto IL_162;
						case 28:
							goto IL_279;
						case 29:
							xlsWorkbookShapeData2 = parentWorkbook2.HeaderFooterData;
							goto IL_162;
						case 30:
							num = 8;
							continue;
						case 31:
							goto IL_3AE;
						case 32:
						{
							xlsBitmapShape.BlipId = (uint)num2;
							sprᜪ sprᜪ = xlsWorkbookShapeData3.Pictures[num2 - 1];
							sprᜪ sprᜪ2 = sprᜪ;
							sprᜪ2.ᜂ(sprᜪ2.\u170D() + 1U);
							num = 22;
							continue;
						}
						case 33:
							if (num2 > 0)
							{
								num = 32;
								continue;
							}
							goto IL_232;
						}
						break;
						IL_CE:
						num = 7;
						continue;
						IL_162:
						XlsWorkbookShapeData xlsWorkbookShapeData4 = xlsWorkbookShapeData2;
						sprᜪ sprᜪ3 = xlsWorkbookShapeData4.ᜀ(num2);
						num2 = xlsWorkbookShapeData3.ᜁ((sprᜪ)sprᜪ3.Clone());
						num = 15;
						continue;
						IL_192:
						num = 2;
						continue;
						IL_232:
						num = 16;
						continue;
						IL_27E:
						xlsBitmapShape = (XlsBitmapShape)base.MemberwiseClone();
						xlsBitmapShape.SetParent(xlsWorksheetBase.InnerShapes);
						xlsBitmapShape.SetParents();
						xlsBitmapShape.CopyFrom(this, hashNewNames, dicFontIndexes);
						xlsBitmapShape.CloneLineFill(this);
						num = 23;
						continue;
						IL_2EB:
						num = 33;
						continue;
						IL_33F:
						num = 11;
						continue;
						IL_386:
						xlsWorkbookShapeData3 = xlsWorkbookShapeData;
						num = 14;
						continue;
						IL_3AE:
						parentWorkbook2 = base.ParentWorkbook;
						parentWorkbook = xlsWorksheetBase.ParentWorkbook;
						num2 = (int)this.BlipId;
						num = 18;
					}
				}
				IL_279:
				return xlsBitmapShape;
				IL_41C:
				throw new NotImplementedException();
			}
			}
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x000A1308 File Offset: 0x000A0308
		[CLSCompliant(false)]
		internal override bool UpdateMso(spr\u1D3B mso)
		{
			for (;;)
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
						if (mso is sprᜪ)
						{
							num = 2;
							continue;
						}
						return false;
					case 2:
						goto IL_81;
					case 3:
						return true;
					}
					if (base.UpdateMso(mso))
					{
						num = 3;
					}
					else
					{
						num = 1;
					}
				}
				IL_81:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_97;
				}
			}
			return true;
			IL_97:
			if (false)
			{
			}
			this.ᜂ = (mso as sprᜪ);
			this.ᜃ = this.ᜂ.ᜄ().ᜀ();
			return true;
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x000A13B8 File Offset: 0x000A03B8
		internal override void GenerateDefaultName()
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
			this.Name = CollectionExtended<IShape>.GenerateDefaultName(this.m_shapes, RecordTableEnumerator.b("ቁⵃ╅㱇㽉㹋⭍灏", a_));
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x000A141C File Offset: 0x000A041C
		[CLSCompliant(false)]
		public void SetBlipId(uint newId)
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
			this.ᜀ = newId;
		}

		// Token: 0x04000DC5 RID: 3525
		public const int ShapeInstance = 75;

		// Token: 0x04000DC6 RID: 3526
		private new uint ᜀ;

		// Token: 0x04000DC7 RID: 3527
		private bool \u2593\u009E\u009B\u008E;

		// Token: 0x04000DC8 RID: 3528
		private new string ᜁ;

		// Token: 0x04000DC9 RID: 3529
		private new sprᜪ ᜂ;

		// Token: 0x04000DCA RID: 3530
		private new Image ᜃ;

		// Token: 0x04000DCB RID: 3531
		private byte \u2593\u0083\u00A1\u009D;

		// Token: 0x04000DCC RID: 3532
		private new Stream ᜄ;

		// Token: 0x04000DCD RID: 3533
		private string[] \u2460\u00A3\u0088\u00A2;

		// Token: 0x04000DCE RID: 3534
		private new Stream ᜅ;

		// Token: 0x04000DCF RID: 3535
		private new Stream ᜆ;

		// Token: 0x04000DD0 RID: 3536
		private new string ᜇ;

		// Token: 0x04000DD1 RID: 3537
		private bool ᜈ;

		// Token: 0x04000DD2 RID: 3538
		private byte \u2609\u0092\u00A6\u00AC;

		// Token: 0x04000DD3 RID: 3539
		private bool ᜉ;

		// Token: 0x04000DD4 RID: 3540
		protected MsoOptions[] cropOptions;

		// Token: 0x04000DD5 RID: 3541
		private int ᜊ;

		// Token: 0x04000DD6 RID: 3542
		private int ᜋ;

		// Token: 0x04000DD7 RID: 3543
		private int ᜌ;

		// Token: 0x04000DD8 RID: 3544
		private int \u170D;
	}
}
