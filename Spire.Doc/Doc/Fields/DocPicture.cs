using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;
using Spire.Layouting;

namespace Spire.Doc.Fields
{
	// Token: 0x02000510 RID: 1296
	public class DocPicture : ParagraphBase, spr\u2297, IPicture
	{
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x0600429A RID: 17050 RVA: 0x003E8E44 File Offset: 0x003E7E44
		internal bool HasBorder
		{
			get
			{
				int num = 8;
				bool flag;
				for (;;)
				{
					bool flag2;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_250;
						default:
							if (false)
							{
							}
							if (this.PictureShape.ᜀ().ᜌ().ᜂ().ᜌ())
							{
								num = 18;
								continue;
							}
							return false;
						}
						break;
					case 1:
						goto IL_2D2;
					case 2:
						flag |= (this.PictureShape.ᜊ().ᜇ().ᜄ() == 0 && this.PictureShape.ᜊ().ᜃ().ᜄ() == 0 && this.PictureShape.ᜊ().ᜈ().ᜄ() == 0 && this.PictureShape.ᜊ().ᜂ().ᜄ() == 0);
						num = 9;
						continue;
					case 3:
						num = 2;
						continue;
					case 4:
						if (this.PictureShape.ᜀ().ᜌ() != null)
						{
							num = 6;
							continue;
						}
						return false;
					case 5:
						num = 15;
						continue;
					case 6:
						goto IL_250;
					case 7:
						num = 21;
						continue;
					case 9:
						goto IL_1F4;
					case 10:
						if (this.PictureShape.ᜊ().ᜈ().ᜀ())
						{
							num = 19;
							continue;
						}
						goto IL_141;
					case 11:
						if (this.PictureShape.ᜀ() != null)
						{
							num = 12;
							continue;
						}
						return false;
					case 12:
						num = 4;
						continue;
					case 13:
						num = 10;
						continue;
					case 14:
						flag2 = this.PictureShape.ᜊ().ᜂ().ᜀ();
						goto IL_14F;
					case 15:
						if (this.PictureShape.ᜊ().ᜃ().ᜀ())
						{
							num = 13;
							continue;
						}
						goto IL_141;
					case 16:
						flag2 = false;
						goto IL_14F;
					case 17:
						if (this.IsShape)
						{
							num = 3;
							continue;
						}
						goto IL_1F6;
					case 18:
						num = 20;
						continue;
					case 19:
						if (true)
						{
						}
						num = 14;
						continue;
					case 20:
						if (this.PictureShape.ᜀ().ᜌ().ᜂ().ᜂ())
						{
							num = 1;
							continue;
						}
						return false;
					case 21:
						if (this.PictureShape.ᜊ().ᜇ().ᜀ())
						{
							num = 5;
							continue;
						}
						goto IL_141;
					}
					if (this.TextWrappingStyle == TextWrappingStyle.Inline)
					{
						num = 7;
						continue;
					}
					num = 11;
					continue;
					IL_141:
					num = 16;
					continue;
					IL_14F:
					flag = flag2;
					num = 17;
					continue;
					IL_250:
					num = 0;
				}
				IL_1F4:
				IL_1F6:
				return !flag;
				IL_2D2:
				return this.PictureShape.ᜀ().ᜌ().ᜂ().ᜐ();
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600429B RID: 17051 RVA: 0x003E9144 File Offset: 0x003E8144
		// (set) Token: 0x0600429C RID: 17052 RVA: 0x003E91CC File Offset: 0x003E81CC
		internal Borders Borders
		{
			get
			{
				int num = 1;
				for (;;)
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
						switch (num)
						{
						case 0:
							goto IL_70;
						case 2:
							this.ᜢ = this.ᜁ();
							goto IL_68;
						}
						if (this.ᜢ == null)
						{
							num = 2;
							continue;
						}
						goto IL_72;
					}
					IL_68:
					num = 0;
				}
				IL_70:
				IL_72:
				return this.ᜢ;
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
				this.ᜀ(value);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600429D RID: 17053 RVA: 0x003E9210 File Offset: 0x003E8210
		// (set) Token: 0x0600429E RID: 17054 RVA: 0x003E9254 File Offset: 0x003E8254
		internal float WrapDistanceTop
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
				return this.\u1717;
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
				this.\u1717 = value;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600429F RID: 17055 RVA: 0x003E9298 File Offset: 0x003E8298
		// (set) Token: 0x060042A0 RID: 17056 RVA: 0x003E92DC File Offset: 0x003E82DC
		internal float WrapDistanceBottom
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
				return this.\u1718;
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
				this.\u1718 = value;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060042A1 RID: 17057 RVA: 0x003E9320 File Offset: 0x003E8320
		// (set) Token: 0x060042A2 RID: 17058 RVA: 0x003E9364 File Offset: 0x003E8364
		internal float WrapDistanceLeft
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
				return this.\u1719;
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
				this.\u1719 = value;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060042A3 RID: 17059 RVA: 0x003E93A8 File Offset: 0x003E83A8
		// (set) Token: 0x060042A4 RID: 17060 RVA: 0x003E93EC File Offset: 0x003E83EC
		internal float WrapDistanceRight
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
				return this.\u171A;
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
				this.\u171A = value;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060042A5 RID: 17061 RVA: 0x003E9430 File Offset: 0x003E8430
		public override DocumentObjectType DocumentObjectType
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
				return DocumentObjectType.Picture;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060042A6 RID: 17062 RVA: 0x003E9470 File Offset: 0x003E8470
		// (set) Token: 0x060042A7 RID: 17063 RVA: 0x003E94BC File Offset: 0x003E84BC
		public float Height
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
				return this.Size.Height;
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
				this.ᜀ.Height = value;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060042A8 RID: 17064 RVA: 0x003E9504 File Offset: 0x003E8504
		// (set) Token: 0x060042A9 RID: 17065 RVA: 0x003E9550 File Offset: 0x003E8550
		public float Width
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
				return this.Size.Width;
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
				this.ᜀ.Width = value;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060042AA RID: 17066 RVA: 0x003E9598 File Offset: 0x003E8598
		// (set) Token: 0x060042AB RID: 17067 RVA: 0x003E95DC File Offset: 0x003E85DC
		public float HeightScale
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
				return this.ᜂ;
			}
			set
			{
				int a_ = 1;
				if (value <= 0f)
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
						throw new ArgumentOutOfRangeException(ClipboardData.b("㑦੨੪Ŭ੮兰ᕲᑴᑶ൸ᑺོ彾ꦈ꾎ﺚ뾞햠쮢쒤즦覨鮪", a_));
					}
				}
				this.ᜂ = value;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060042AC RID: 17068 RVA: 0x003E9648 File Offset: 0x003E8648
		// (set) Token: 0x060042AD RID: 17069 RVA: 0x003E968C File Offset: 0x003E868C
		public float WidthScale
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
				return this.ᜁ;
			}
			set
			{
				int a_ = 8;
				if (value <= 0f)
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
						throw new ArgumentOutOfRangeException(ClipboardData.b("㵭፯፱ᡳ፵塷ᱹᵻᵽꚅﾉﾋ揄낏뚕ﾗ鍊ﾝ풟잡횣蚥\udca7슩춫삭邯花", a_));
					}
				}
				this.ᜁ = value;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060042AE RID: 17070 RVA: 0x003E96F8 File Offset: 0x003E86F8
		public Image Image
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
				return this.ᜁ(this.ImageBytes);
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060042AF RID: 17071 RVA: 0x003E9740 File Offset: 0x003E8740
		public byte[] ImageBytes
		{
			get
			{
				if (this.\u1716 == null)
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
						return null;
					}
				}
				return this.\u1716.ᜃ();
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060042B0 RID: 17072 RVA: 0x003E9794 File Offset: 0x003E8794
		internal sprᠾ ImageRecord
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
				return this.\u1716;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060042B1 RID: 17073 RVA: 0x003E97D8 File Offset: 0x003E87D8
		// (set) Token: 0x060042B2 RID: 17074 RVA: 0x003E981C File Offset: 0x003E881C
		public float Brightness
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
				return this.ᜠ;
			}
			set
			{
				int a_ = 4;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_3B;
					case 2:
						if (value > 100f)
						{
							num = 1;
							continue;
						}
						goto IL_8F;
					case 3:
						goto IL_6B;
					}
					if (value >= -100f)
					{
						num = 3;
						continue;
					}
					goto IL_3B;
					IL_6B:
					num = 2;
					continue;
					IL_3B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6B;
					default:
						goto IL_51;
					}
				}
				IL_51:
				if (false)
				{
				}
				throw new ArgumentOutOfRangeException(ClipboardData.b("㩩ի൭ѯݱٳ፵塷᡹๻᝽ﮇ黎겋뚕流ﾙ벛劣튟잡얣튥춧\ud8a9貫\udaad\ud8af펱\udab3隵袷骹\uddbb킽꒿ꣃ꧅뿇꿉뻋꓏뫑뗓룕", a_));
				IL_8F:
				if (true)
				{
				}
				this.ᜠ = value;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060042B3 RID: 17075 RVA: 0x003E98C8 File Offset: 0x003E88C8
		// (set) Token: 0x060042B4 RID: 17076 RVA: 0x003E990C File Offset: 0x003E890C
		public float Contrast
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
				return this.\u171F;
			}
			set
			{
				int a_ = 12;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						if (value > 100f)
						{
							num = 2;
							continue;
						}
						goto IL_9A;
					case 2:
						goto IL_43;
					case 3:
						goto IL_73;
					}
					if (true)
					{
					}
					if (value >= -100f)
					{
						num = 3;
						continue;
					}
					goto IL_43;
					IL_73:
					num = 1;
					continue;
					IL_43:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_73;
					default:
						goto IL_59;
					}
				}
				IL_59:
				if (false)
				{
				}
				throw new ArgumentOutOfRangeException(ClipboardData.b("≱ᵳᕵ౷ཹ๻᭽ꁿﲇﶍ늑煉벛ﲝ얟芡쎣풥춧쮩\ud8ab쮭슯銱삳\udeb5\ud9b7풹鲻鎽꧇꓉꣋볏뷑ꏓ돕꫗龎꣛뛝臟賡쓣ퟥ\ud8e7\udae9", a_));
				IL_9A:
				this.\u171F = value;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060042B5 RID: 17077 RVA: 0x003E99BC File Offset: 0x003E89BC
		// (set) Token: 0x060042B6 RID: 17078 RVA: 0x003E9A00 File Offset: 0x003E8A00
		public PictureColor Color
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
			set
			{
				for (;;)
				{
					IL_14:
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_52:
						num = 0;
						break;
					case 1:
						goto IL_34;
					default:
						goto IL_34;
					}
					for (;;)
					{
						IL_02:
						switch (num)
						{
						case 0:
							this.\u171F = 15f;
							this.ᜠ = 85f;
							if (true)
							{
							}
							num = 1;
							continue;
						case 1:
							return;
						case 2:
							goto IL_49;
						}
						goto IL_14;
					}
					IL_49:
					if (this.ᜣ == PictureColor.Washout)
					{
						goto IL_52;
					}
					break;
					IL_34:
					if (false)
					{
					}
					this.ᜣ = value;
					num = 2;
					goto IL_02;
				}
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060042B7 RID: 17079 RVA: 0x003E9A94 File Offset: 0x003E8A94
		// (set) Token: 0x060042B8 RID: 17080 RVA: 0x003E9AD8 File Offset: 0x003E8AD8
		internal float CropFromLeft
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
				return this.\u171D;
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
				this.\u171D = value;
				this.IsCrop = true;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060042B9 RID: 17081 RVA: 0x003E9B24 File Offset: 0x003E8B24
		// (set) Token: 0x060042BA RID: 17082 RVA: 0x003E9B68 File Offset: 0x003E8B68
		internal float CropFromRight
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.\u171C = value;
				this.IsCrop = true;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060042BB RID: 17083 RVA: 0x003E9BB4 File Offset: 0x003E8BB4
		// (set) Token: 0x060042BC RID: 17084 RVA: 0x003E9BF8 File Offset: 0x003E8BF8
		internal float CropFromTop
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
				this.IsCrop = true;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060042BD RID: 17085 RVA: 0x003E9C44 File Offset: 0x003E8C44
		// (set) Token: 0x060042BE RID: 17086 RVA: 0x003E9C88 File Offset: 0x003E8C88
		internal float CropFromBottom
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
				return this.\u171E;
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
				this.\u171E = value;
				this.IsCrop = true;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060042BF RID: 17087 RVA: 0x003E9CD4 File Offset: 0x003E8CD4
		// (set) Token: 0x060042C0 RID: 17088 RVA: 0x003E9D18 File Offset: 0x003E8D18
		internal Color Chromakey
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜡ = value;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060042C1 RID: 17089 RVA: 0x003E9D5C File Offset: 0x003E8D5C
		internal Rectangle CropRectangle
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
				return this.ᜀ(new RectangleF(new PointF(0f, 0f), new SizeF((float)this.Image.Size.Width, (float)this.Image.Size.Height)));
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060042C2 RID: 17090 RVA: 0x003E9DE0 File Offset: 0x003E8DE0
		internal bool IsPositivelyCrop
		{
			get
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.CropFromRight > 0f)
						{
							return true;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_40;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 1:
						if (this.CropFromTop <= 0f)
						{
							num = 5;
							continue;
						}
						return true;
					case 2:
						num = 0;
						continue;
					case 3:
						num = 1;
						continue;
					case 5:
						goto IL_7F;
					}
					goto IL_28;
					IL_40:
					num = 2;
					continue;
					IL_28:
					if (true)
					{
					}
					if (this.CropFromLeft <= 0f)
					{
						goto IL_40;
					}
					return true;
				}
				IL_7F:
				return this.CropFromBottom > 0f;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060042C3 RID: 17091 RVA: 0x003E9EB0 File Offset: 0x003E8EB0
		// (set) Token: 0x060042C4 RID: 17092 RVA: 0x003E9EF4 File Offset: 0x003E8EF4
		internal bool IsCrop
		{
			[CompilerGenerated]
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
				return this.ᜥ;
			}
			[CompilerGenerated]
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
				this.ᜥ = value;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060042C5 RID: 17093 RVA: 0x003E9F38 File Offset: 0x003E8F38
		// (set) Token: 0x060042C6 RID: 17094 RVA: 0x003E9F7C File Offset: 0x003E8F7C
		public HorizontalOrigin HorizontalOrigin
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜃ = value;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060042C7 RID: 17095 RVA: 0x003E9FC0 File Offset: 0x003E8FC0
		// (set) Token: 0x060042C8 RID: 17096 RVA: 0x003EA004 File Offset: 0x003E9004
		public VerticalOrigin VerticalOrigin
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

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060042C9 RID: 17097 RVA: 0x003EA048 File Offset: 0x003E9048
		// (set) Token: 0x060042CA RID: 17098 RVA: 0x003EA08C File Offset: 0x003E908C
		public float HorizontalPosition
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

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060042CB RID: 17099 RVA: 0x003EA0D0 File Offset: 0x003E90D0
		// (set) Token: 0x060042CC RID: 17100 RVA: 0x003EA114 File Offset: 0x003E9114
		public float VerticalPosition
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

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060042CD RID: 17101 RVA: 0x003EA158 File Offset: 0x003E9158
		// (set) Token: 0x060042CE RID: 17102 RVA: 0x003EA19C File Offset: 0x003E919C
		public TextWrappingStyle TextWrappingStyle
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
				int num = 10;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_13D;
					case 1:
						if (value != TextWrappingStyle.Inline)
						{
							num = 8;
							continue;
						}
						goto IL_5E;
					case 2:
						num = 1;
						continue;
					case 3:
						num = 9;
						continue;
					case 4:
						num = 6;
						continue;
					case 5:
						this.ᜋ = true;
						num = 7;
						continue;
					case 6:
						if (value == TextWrappingStyle.Inline)
						{
							num = 12;
							continue;
						}
						goto IL_179;
					case 7:
						IL_E5:
						goto IL_13D;
					case 8:
						this.PictureShape.ᜅ();
						num = 11;
						continue;
					case 9:
						if (this.ᜇ == TextWrappingStyle.Inline)
						{
							num = 2;
							continue;
						}
						goto IL_5E;
					case 11:
						goto IL_FD;
					case 12:
						this.PictureShape.ᜇ();
						num = 13;
						continue;
					case 13:
						goto IL_115;
					case 14:
						if (this.HasBorder)
						{
							num = 3;
							continue;
						}
						goto IL_179;
					case 15:
						if (this.ᜇ != TextWrappingStyle.Inline)
						{
							num = 4;
							continue;
						}
						goto IL_179;
					}
					if (value == TextWrappingStyle.Behind)
					{
						num = 5;
						continue;
					}
					this.ᜋ = false;
					num = 0;
					continue;
					IL_5E:
					if (true)
					{
					}
					num = 15;
					continue;
					IL_13D:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E5;
					default:
						if (false)
						{
						}
						num = 14;
						break;
					}
				}
				IL_FD:
				IL_115:
				IL_179:
				this.ᜇ = value;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060042CF RID: 17103 RVA: 0x003EA334 File Offset: 0x003E9334
		// (set) Token: 0x060042D0 RID: 17104 RVA: 0x003EA378 File Offset: 0x003E9378
		public TextWrappingType TextWrappingType
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

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060042D1 RID: 17105 RVA: 0x003EA3BC File Offset: 0x003E93BC
		// (set) Token: 0x060042D2 RID: 17106 RVA: 0x003EA400 File Offset: 0x003E9400
		public ShapeHorizontalAlignment HorizontalAlignment
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
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜉ = value;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060042D3 RID: 17107 RVA: 0x003EA444 File Offset: 0x003E9444
		// (set) Token: 0x060042D4 RID: 17108 RVA: 0x003EA488 File Offset: 0x003E9488
		public ShapeVerticalAlignment VerticalAlignment
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜊ = value;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060042D5 RID: 17109 RVA: 0x003EA4CC File Offset: 0x003E94CC
		// (set) Token: 0x060042D6 RID: 17110 RVA: 0x003EA510 File Offset: 0x003E9510
		public bool IsUnderText
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
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060042D7 RID: 17111 RVA: 0x003EA554 File Offset: 0x003E9554
		// (set) Token: 0x060042D8 RID: 17112 RVA: 0x003EA598 File Offset: 0x003E9598
		internal CharacterFormat PictureCharacterFormat
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
				return this.m_charFormat;
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
				this.m_charFormat = value;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060042D9 RID: 17113 RVA: 0x003EA5DC File Offset: 0x003E95DC
		// (set) Token: 0x060042DA RID: 17114 RVA: 0x003EA620 File Offset: 0x003E9620
		internal int ShapeId
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
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060042DB RID: 17115 RVA: 0x003EA664 File Offset: 0x003E9664
		// (set) Token: 0x060042DC RID: 17116 RVA: 0x003EA6A8 File Offset: 0x003E96A8
		internal bool IsHeaderPicture
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

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060042DD RID: 17117 RVA: 0x003EA6EC File Offset: 0x003E96EC
		// (set) Token: 0x060042DE RID: 17118 RVA: 0x003EA730 File Offset: 0x003E9730
		internal spr\u248F ShapeInfo
		{
			[CompilerGenerated]
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
			[CompilerGenerated]
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
				this.ᜦ = value;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060042DF RID: 17119 RVA: 0x003EA774 File Offset: 0x003E9774
		// (set) Token: 0x060042E0 RID: 17120 RVA: 0x003EA7B8 File Offset: 0x003E97B8
		internal sprẛ PictureShape
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
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜎ = value;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060042E1 RID: 17121 RVA: 0x003EA7FC File Offset: 0x003E97FC
		// (set) Token: 0x060042E2 RID: 17122 RVA: 0x003EA840 File Offset: 0x003E9840
		internal spr\u1B7E ShapeBase
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
				return this.ᜤ;
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
				this.ᜤ = value;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060042E3 RID: 17123 RVA: 0x003EA884 File Offset: 0x003E9884
		// (set) Token: 0x060042E4 RID: 17124 RVA: 0x003EA944 File Offset: 0x003E9944
		internal SizeF Size
		{
			get
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_48;
					case 1:
						if (this.ᜀ.Height < -3.4028235E+38f)
						{
							num = 0;
							continue;
						}
						goto IL_AB;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_48;
						default:
							goto IL_72;
						}
						break;
					case 3:
						if (true)
						{
						}
						break;
					case 4:
						num = 1;
						continue;
					}
					if (this.ᜀ.Width >= -3.4028235E+38f)
					{
						num = 4;
						continue;
					}
					IL_48:
					this.ᜀ(this.Image);
					num = 2;
				}
				IL_72:
				if (false)
				{
				}
				IL_AB:
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
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060042E5 RID: 17125 RVA: 0x003EA988 File Offset: 0x003E9988
		internal bool IsMetaFile
		{
			get
			{
				if (true)
				{
				}
				if (this.\u1716 != null)
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
						return this.\u1716.ᜄ();
					}
				}
				return false;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060042E6 RID: 17126 RVA: 0x003EA9DC File Offset: 0x003E99DC
		internal List<Stream> DocxProps
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 1:
						goto IL_5A;
					}
					if (true)
					{
					}
					if (this.ᜏ != null)
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
					IL_5A:
					this.ᜏ = new List<Stream>();
					num = 0;
				}
				IL_6F:
				return this.ᜏ;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060042E7 RID: 17127 RVA: 0x003EAA60 File Offset: 0x003E9A60
		// (set) Token: 0x060042E8 RID: 17128 RVA: 0x003EAAA4 File Offset: 0x003E9AA4
		public string AlternativeText
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜐ = value;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060042E9 RID: 17129 RVA: 0x003EAAE8 File Offset: 0x003E9AE8
		// (set) Token: 0x060042EA RID: 17130 RVA: 0x003EAB2C File Offset: 0x003E9B2C
		public string Title
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

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060042EB RID: 17131 RVA: 0x003EAB70 File Offset: 0x003E9B70
		// (set) Token: 0x060042EC RID: 17132 RVA: 0x003EABB4 File Offset: 0x003E9BB4
		internal Body EmbedBody
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
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060042ED RID: 17133 RVA: 0x003EABF8 File Offset: 0x003E9BF8
		// (set) Token: 0x060042EE RID: 17134 RVA: 0x003EAC3C File Offset: 0x003E9C3C
		internal bool IsShape
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.\u1713 = value;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060042EF RID: 17135 RVA: 0x003EAC80 File Offset: 0x003E9C80
		// (set) Token: 0x060042F0 RID: 17136 RVA: 0x003EACC4 File Offset: 0x003E9CC4
		internal int OrderIndex
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

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060042F1 RID: 17137 RVA: 0x003EAD08 File Offset: 0x003E9D08
		// (set) Token: 0x060042F2 RID: 17138 RVA: 0x003EAD4C File Offset: 0x003E9D4C
		internal bool LayoutInCell
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
				return this.\u1715;
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
				this.\u1715 = value;
			}
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x003EAD90 File Offset: 0x003E9D90
		public DocPicture(IDocument doc) : base((Document)doc)
		{
			this.m_charFormat = new CharacterFormat(doc);
			this.m_charFormat.ᜀ(this);
			this.ᜎ = new sprẛ(doc);
			this.ᜀ.Height = float.MinValue;
			this.ᜀ.Width = float.MinValue;
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x003EAE24 File Offset: 0x003E9E24
		private new Borders ᜁ()
		{
			switch (0)
			{
			default:
			{
				Borders borders;
				for (;;)
				{
					for (;;)
					{
						if (true)
						{
						}
						borders = new Borders();
						int num = 0;
						for (;;)
						{
							uint num2;
							uint num3;
							switch (num)
							{
							case 0:
								if (this.HasBorder)
								{
									num = 11;
									continue;
								}
								return borders;
							case 1:
								if (num2 != 4294967295U)
								{
									num = 13;
									continue;
								}
								goto IL_128;
							case 2:
								goto IL_128;
							case 3:
								return borders;
							case 4:
								num = 5;
								continue;
							case 5:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									if (this.IsShape)
									{
										num = 9;
										continue;
									}
									goto IL_1B5;
								}
								break;
							case 6:
								goto IL_7F;
							case 7:
								if (num3 != 4294967295U)
								{
									num = 12;
									continue;
								}
								goto IL_7F;
							case 8:
								if (this.TextWrappingStyle == TextWrappingStyle.Inline)
								{
									num = 4;
									continue;
								}
								goto IL_1B5;
							case 9:
								borders.Left.BorderType = (BorderStyle)this.PictureShape.ᜊ().ᜃ().ᜄ();
								borders.Left.LineWidth = this.PictureShape.ᜊ().ᜃ().ᜈ();
								borders.Left.Color = this.PictureShape.ᜊ().ᜃ().ᜅ();
								borders.Top.BorderType = (BorderStyle)this.PictureShape.ᜊ().ᜂ().ᜄ();
								borders.Top.LineWidth = this.PictureShape.ᜊ().ᜂ().ᜈ();
								borders.Top.Color = this.PictureShape.ᜊ().ᜂ().ᜅ();
								borders.Right.BorderType = (BorderStyle)this.PictureShape.ᜊ().ᜈ().ᜄ();
								borders.Right.LineWidth = this.PictureShape.ᜊ().ᜈ().ᜈ();
								borders.Right.Color = this.PictureShape.ᜊ().ᜈ().ᜅ();
								borders.Bottom.BorderType = (BorderStyle)this.PictureShape.ᜊ().ᜇ().ᜄ();
								borders.Bottom.LineWidth = this.PictureShape.ᜊ().ᜇ().ᜈ();
								borders.Bottom.Color = this.PictureShape.ᜊ().ᜇ().ᜅ();
								num = 10;
								continue;
							case 10:
								return borders;
							case 11:
								num = 8;
								continue;
							case 12:
								borders.LineWidth = (float)Math.Round((double)(num3 / 12700f * 8f), 2);
								num = 6;
								continue;
							case 13:
								borders.Color = sprṡ.ᜀ(num2);
								num = 2;
								continue;
							}
							break;
							IL_7F:
							spr\u22B7 spr_u22B;
							num2 = spr_u22B.ᜀ(448);
							num = 1;
							continue;
							IL_128:
							uint num4 = spr_u22B.ᜀ(461);
							uint num5 = spr_u22B.ᜀ(462);
							TextBoxLineStyle a_ = (TextBoxLineStyle)num4;
							LineDashing a_2 = (LineDashing)num5;
							borders.BorderType = this.PictureShape.ᜀ(a_2, a_);
							num = 3;
							continue;
							IL_1B5:
							borders.Color = System.Drawing.Color.Black;
							borders.BorderType = BorderStyle.Single;
							borders.LineWidth = 0.5f;
							spr_u22B = this.PictureShape.ᜀ().ᜌ();
							num3 = spr_u22B.ᜀ(459);
							num = 7;
						}
					}
				}
				return borders;
			}
			}
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x003EB1D8 File Offset: 0x003EA1D8
		private new void ᜀ(Borders A_0)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!this.IsShape)
					{
						goto IL_211;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 1:
					goto IL_204;
				case 2:
					num = 0;
					continue;
				}
				if (this.TextWrappingStyle != TextWrappingStyle.Inline)
				{
					goto IL_211;
				}
				num = 2;
			}
			IL_204:
			if (true)
			{
			}
			this.PictureShape.ᜊ().ᜃ().ᜃ((byte)A_0.Left.BorderType);
			this.PictureShape.ᜊ().ᜃ().ᜀ(A_0.Left.LineWidth);
			this.PictureShape.ᜊ().ᜃ().ᜀ(A_0.Left.Color);
			this.PictureShape.ᜊ().ᜂ().ᜃ((byte)A_0.Top.BorderType);
			this.PictureShape.ᜊ().ᜂ().ᜀ(A_0.Top.LineWidth);
			this.PictureShape.ᜊ().ᜂ().ᜀ(A_0.Top.Color);
			this.PictureShape.ᜊ().ᜈ().ᜃ((byte)A_0.Right.BorderType);
			this.PictureShape.ᜊ().ᜈ().ᜀ(A_0.Right.LineWidth);
			this.PictureShape.ᜊ().ᜈ().ᜀ(A_0.Right.Color);
			this.PictureShape.ᜊ().ᜇ().ᜃ((byte)A_0.Bottom.BorderType);
			this.PictureShape.ᜊ().ᜇ().ᜀ(A_0.Bottom.LineWidth);
			this.PictureShape.ᜊ().ᜇ().ᜀ(A_0.Bottom.Color);
			return;
			IL_211:
			this.PictureShape.ᜀ(A_0);
		}

		// Token: 0x060042F6 RID: 17142 RVA: 0x003EB404 File Offset: 0x003EA404
		public void LoadImage(Image image)
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
			this.ᜀ(image, true);
		}

		// Token: 0x060042F7 RID: 17143 RVA: 0x003EB448 File Offset: 0x003EA448
		internal new void ᜀ(Image A_0, bool A_1)
		{
			int a_ = 2;
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_169;
				case 1:
					if (!A_0.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff))
					{
						num = 2;
						continue;
					}
					goto IL_12B;
				case 2:
					num = 8;
					continue;
				case 3:
					goto IL_12B;
				case 4:
					goto IL_13D;
				case 5:
					this.\u1716 = new sprᠾ(DocPicture.ᜂ(A_0));
					num = 14;
					continue;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C5;
					default:
						if (false)
						{
						}
						this.\u1716 = base.Document.Images.ᜀ(DocPicture.ᜀ(A_0 as Metafile), false);
						num = 15;
						continue;
					}
					break;
				case 7:
					if (A_0 is Metafile)
					{
						num = 6;
						continue;
					}
					num = 1;
					continue;
				case 8:
					if (!A_0.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
					{
						num = 11;
						continue;
					}
					goto IL_12B;
				case 9:
					goto IL_64;
				case 11:
					num = 13;
					continue;
				case 12:
					if (base.Document == null)
					{
						num = 5;
						continue;
					}
					this.\u1716 = base.Document.Images.ᜂ(DocPicture.ᜂ(A_0));
					num = 0;
					continue;
				case 13:
					if (A_0 is Metafile)
					{
						num = 3;
						continue;
					}
					num = 12;
					continue;
				case 14:
					goto IL_112;
				case 15:
					goto IL_1C5;
				}
				if (A_0 == null)
				{
					num = 9;
					continue;
				}
				this.ᜀ();
				num = 7;
				continue;
				IL_12B:
				this.ᜁ(A_0);
				num = 4;
			}
			IL_64:
			if (true)
			{
			}
			throw new ArgumentNullException(ClipboardData.b("ŧݩ൫७ᕯ", a_));
			IL_112:
			IL_13D:
			IL_169:
			IL_1C5:
			this.ᜀ(A_0);
		}

		// Token: 0x060042F8 RID: 17144 RVA: 0x003EB660 File Offset: 0x003EA660
		public void LoadImage(byte[] imageBytes)
		{
			int a_ = 2;
			int num = 2;
			Image image;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_58;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7C;
					default:
						goto IL_124;
					}
					break;
				case 3:
					goto IL_140;
				case 4:
					goto IL_18F;
				case 5:
					if (!this.Image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff))
					{
						num = 8;
						continue;
					}
					goto IL_140;
				case 6:
					if (this.Image != null)
					{
						num = 7;
						continue;
					}
					goto IL_F3;
				case 7:
					num = 5;
					continue;
				case 8:
					num = 10;
					continue;
				case 9:
					if (image is Metafile)
					{
						num = 12;
						continue;
					}
					num = 6;
					continue;
				case 10:
					if (this.Image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
					{
						goto IL_7C;
					}
					goto IL_F3;
				case 11:
					goto IL_157;
				case 12:
					this.ᜀ(imageBytes, true);
					num = 4;
					continue;
				}
				if (imageBytes == null)
				{
					num = 0;
					continue;
				}
				this.ᜀ();
				image = this.ᜁ(imageBytes);
				num = 9;
				continue;
				IL_7C:
				num = 3;
				continue;
				IL_F3:
				if (true)
				{
				}
				this.ᜀ(imageBytes, false);
				num = 1;
				continue;
				IL_140:
				this.ᜁ(this.Image);
				num = 11;
			}
			IL_58:
			throw new ArgumentNullException(ClipboardData.b("Ⅷݩ൫७ᕯ剱ᙳཱུ౷όཻ幽ﺉ겋늑望벛튟芡솣쮥\ud8a7\udea9햫", a_));
			IL_124:
			if (false)
			{
			}
			IL_157:
			IL_18F:
			imageBytes = null;
			this.ᜀ(image);
		}

		// Token: 0x060042F9 RID: 17145 RVA: 0x003EB808 File Offset: 0x003EA808
		internal new void ᜀ(byte[] A_0, bool A_1)
		{
			int a_ = 7;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_AF:
				if (true)
				{
				}
				this.\u1716 = base.Document.Images.ᜂ(A_0);
				num = 5;
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
				switch (num)
				{
				case 0:
					if (A_1)
					{
						num = 1;
						continue;
					}
					goto IL_AF;
				case 1:
					this.\u1716 = base.Document.Images.ᜀ(A_0, false);
					num = 2;
					continue;
				case 2:
					goto IL_84;
				case 4:
					goto IL_62;
				case 5:
					goto IL_D9;
				}
				if (A_0 == null)
				{
					num = 4;
				}
				else
				{
					num = 0;
				}
			}
			IL_62:
			throw new ArgumentNullException(ClipboardData.b("Ѭɮၰᑲၴ", a_));
			IL_84:
			IL_D9:
			A_0 = null;
			this.ᜀ = new SizeF(float.MinValue, float.MinValue);
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x003EB908 File Offset: 0x003EA908
		internal new void ᜀ(sprᠾ A_0)
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
			this.\u1716 = A_0;
			this.ᜀ = new SizeF((float)this.\u1716.ᜁ().Width, (float)this.\u1716.ᜁ().Height);
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x003EB980 File Offset: 0x003EA980
		private new Image ᜁ(byte[] A_0)
		{
			switch (0)
			{
			default:
			{
				Image result = null;
				if (A_0 != null)
				{
					try
					{
						for (;;)
						{
							ImageTypeCheck.ImageType imageType = ImageTypeCheck.ᜀ(A_0);
							int num = 16;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (A_0[3] == 0)
									{
										num = 7;
										continue;
									}
									goto IL_2EF;
								case 1:
									if (A_0[1] == 0)
									{
										num = 5;
										continue;
									}
									goto IL_2EF;
								case 2:
									goto IL_A5;
								case 3:
									num = 14;
									continue;
								case 4:
									A_0[A_0.Length - 2] = 0;
									num = 2;
									continue;
								case 5:
									num = 6;
									continue;
								case 6:
									if (A_0[2] == 9)
									{
										num = 21;
										continue;
									}
									goto IL_2EF;
								case 7:
								{
									byte[] array = new byte[22 + A_0.Length];
									byte[] array2 = new byte[22];
									array2[0] = 215;
									array2[1] = 205;
									array2[2] = 198;
									array2[3] = 154;
									byte[] bytes = BitConverter.GetBytes((ushort)this.Width);
									Array.Copy(bytes, 0, array2, 10, bytes.Length);
									byte[] bytes2 = BitConverter.GetBytes((ushort)this.Height);
									Array.Copy(bytes2, 0, array2, 12, bytes2.Length);
									byte[] bytes3 = BitConverter.GetBytes(35);
									Array.Copy(bytes3, 0, array2, 14, bytes3.Length);
									byte[] bytes4 = BitConverter.GetBytes(this.ᜀ(array2));
									Array.Copy(bytes4, 0, array2, 20, bytes3.Length);
									Array.Copy(array2, 0, array, 0, array2.Length);
									Array.Copy(A_0, 0, array, 22, A_0.Length);
									A_0 = array;
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_251;
									default:
										if (false)
										{
										}
										num = 12;
										continue;
									}
									break;
								}
								case 8:
									goto IL_251;
								case 9:
									if (A_0[A_0.Length - 2] != 0)
									{
										num = 4;
										continue;
									}
									goto IL_A5;
								case 10:
									if (A_0[A_0.Length - 1] != 59)
									{
										num = 17;
										continue;
									}
									goto IL_2EF;
								case 11:
									if (A_0[0] == 1)
									{
										num = 20;
										continue;
									}
									goto IL_2EF;
								case 12:
									goto IL_2EF;
								case 13:
									num = 9;
									continue;
								case 14:
									if (A_0.Length > 4)
									{
										num = 18;
										continue;
									}
									goto IL_2EF;
								case 15:
									if (imageType == ImageTypeCheck.ImageType.None)
									{
										num = 3;
										continue;
									}
									goto IL_2EF;
								case 16:
									if (imageType == ImageTypeCheck.ImageType.GIF)
									{
										num = 13;
										continue;
									}
									num = 15;
									continue;
								case 17:
									A_0[A_0.Length - 1] = 59;
									num = 8;
									continue;
								case 18:
									num = 11;
									continue;
								case 19:
									goto IL_30C;
								case 20:
									num = 1;
									continue;
								case 21:
									num = 0;
									continue;
								}
								break;
								IL_A5:
								num = 10;
								continue;
								IL_2EF:
								result = Image.FromStream(new MemoryStream(A_0), true, false);
								A_0 = null;
								num = 19;
								continue;
								IL_251:
								goto IL_2EF;
							}
						}
						IL_30C:;
					}
					catch
					{
					}
				}
				if (true)
				{
				}
				return result;
			}
			}
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x003EBCD0 File Offset: 0x003EACD0
		private new ushort ᜀ(byte[] A_0)
		{
			int num2;
			ushort num3;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_92:
				if (true)
				{
				}
				int num;
				if (num >= 16)
				{
					num2 = 5;
				}
				else
				{
					num3 ^= BitConverter.ToUInt16(A_0, num);
					num += 2;
					num2 = 0;
				}
				break;
			}
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
					goto IL_8A;
				case 2:
					return 0;
				case 3:
					goto IL_8A;
				case 4:
					goto IL_92;
				case 5:
					return num3;
				}
				if (A_0 == null)
				{
					num2 = 2;
					continue;
				}
				num3 = BitConverter.ToUInt16(A_0, 0);
				int num = 2;
				num2 = 3;
				continue;
				IL_8A:
				num2 = 4;
			}
			return 0;
		}

		// Token: 0x060042FD RID: 17149 RVA: 0x003EBD8C File Offset: 0x003EAD8C
		public IParagraph AddCaption(string name, CaptionNumberingFormat format, CaptionPosition captionPosition)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				Paragraph paragraph;
				for (;;)
				{
					if (true)
					{
					}
					Body body = base.OwnerParagraph.Owner as Body;
					paragraph = null;
					int num = 4;
					for (;;)
					{
						int num2;
						int num4;
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
						{
							int num3;
							num2 = num3 + 1;
							goto IL_257;
						}
						case 2:
							return paragraph;
						case 3:
						{
							int num3;
							num2 = num3;
							goto IL_257;
						}
						case 4:
							if (body != null)
							{
								num = 11;
								continue;
							}
							return paragraph;
						case 5:
							goto IL_116;
						case 6:
							return paragraph;
						case 7:
							if (num4 > 0)
							{
								num = 5;
								continue;
							}
							return paragraph;
						case 8:
						{
							if (captionPosition == CaptionPosition.AboveImage)
							{
								num = 9;
								continue;
							}
							base.OwnerParagraph.Format.KeepFollow = true;
							int num3;
							body.Paragraphs.Insert(num3 + 1, paragraph);
							num = 6;
							continue;
						}
						case 9:
							paragraph.Format.KeepFollow = true;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_116;
							default:
								if (false)
								{
								}
								num = 10;
								continue;
							}
							break;
						case 10:
							if (num4 != 0)
							{
								num = 0;
								continue;
							}
							num = 3;
							continue;
						case 11:
						{
							int num3 = body.Paragraphs.IndexOf(base.OwnerParagraph);
							paragraph = new Paragraph(base.Document);
							paragraph.AppendText(name + ClipboardData.b("乭", a_));
							name = name.Replace(ClipboardData.b("乭", a_), ClipboardData.b("ㅭ", a_));
							SequenceField sequenceField = (SequenceField)paragraph.AppendField(ClipboardData.b("࡭᥯ᕱųѵᵷ婹", a_) + name, FieldType.FieldSequence);
							sequenceField.CaptionName = ClipboardData.b("࡭᥯ᕱųѵᵷ婹", a_) + name;
							sequenceField.NumberFormat = format;
							num4 = base.OwnerParagraph.Items.IndexOf(this);
							num = 8;
							continue;
						}
						}
						break;
						IL_116:
						base.OwnerParagraph.Items.RemoveAt(num4);
						Paragraph paragraph2 = new Paragraph(base.Document);
						paragraph2.Items.Insert(0, this);
						int num5;
						body.Paragraphs.Insert(num5 + 1, paragraph2);
						num = 2;
						continue;
						IL_257:
						num5 = num2;
						body.Paragraphs.Insert(num5, paragraph);
						num = 7;
					}
				}
				return paragraph;
			}
			}
		}

		// Token: 0x060042FE RID: 17150 RVA: 0x003EC024 File Offset: 0x003EB024
		protected override object CloneImpl()
		{
			DocPicture docPicture;
			for (;;)
			{
				for (;;)
				{
					docPicture = (DocPicture)base.CloneImpl();
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_57;
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
								docPicture.Size = this.ᜀ;
								num = 6;
								continue;
							}
							break;
						case 2:
							num = 5;
							continue;
						case 3:
							goto IL_D8;
						case 4:
							if (docPicture.ImageRecord == null)
							{
								num = 0;
								continue;
							}
							docPicture.m_charFormat = new CharacterFormat(base.Document);
							docPicture.m_charFormat.ImportContainer(this.m_charFormat);
							docPicture.ᜎ = (sprẛ)this.PictureShape.Clone();
							docPicture.\u1716 = this.\u1716;
							num = 7;
							continue;
						case 5:
							if (this.ᜀ.Height != -3.4028235E+38f)
							{
								num = 1;
								continue;
							}
							goto IL_59;
						case 6:
							goto IL_59;
						case 7:
							if (this.ᜀ.Width != -3.4028235E+38f)
							{
								num = 2;
								continue;
							}
							goto IL_59;
						case 8:
							if (true)
							{
							}
							docPicture.EmbedBody = (Body)this.EmbedBody.Clone();
							num = 3;
							continue;
						case 9:
							if (this.EmbedBody != null)
							{
								num = 8;
								continue;
							}
							goto IL_189;
						}
						break;
						IL_59:
						num = 9;
					}
				}
			}
			IL_57:
			return null;
			IL_D8:
			IL_189:
			docPicture.ᜁ = true;
			return docPicture;
		}

		// Token: 0x060042FF RID: 17151 RVA: 0x003EC1C4 File Offset: 0x003EB1C4
		internal override void CloneRelationsTo(Document doc, OwnerHolder nextOwner)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_B0;
				case 2:
					this.\u1716 = doc.Images.ᜀ(this.\u1716.ᜂ, true);
					num = 5;
					continue;
				case 3:
					if (this.EmbedBody != null)
					{
						num = 12;
						continue;
					}
					return;
				case 4:
					if (!(nextOwner.OwnerBase is HeaderFooter))
					{
						num = 0;
						continue;
					}
					goto IL_203;
				case 5:
					goto IL_11B;
				case 6:
					goto IL_11B;
				case 7:
					if (nextOwner is HeaderFooter)
					{
						num = 11;
						continue;
					}
					goto IL_198;
				case 8:
					goto IL_198;
				case 9:
					return;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F3;
					default:
						if (false)
						{
						}
						if (this.\u1716.ᜄ())
						{
							num = 2;
							continue;
						}
						this.\u1716 = doc.Images.ᜂ(this.\u1716.ᜃ());
						num = 6;
						continue;
					}
					break;
				case 11:
					goto IL_203;
				case 12:
					this.EmbedBody.CloneRelationsTo(doc, nextOwner);
					num = 9;
					continue;
				case 13:
					goto IL_F3;
				}
				if (nextOwner.OwnerBase != null)
				{
					num = 13;
					continue;
				}
				IL_B0:
				num = 7;
				continue;
				IL_F3:
				num = 4;
				continue;
				IL_11B:
				Size a_;
				this.\u1716.ᜀ(a_);
				System.Drawing.Imaging.ImageFormat a_2;
				this.\u1716.ᜀ(a_2);
				int a_3;
				this.\u1716.ᜁ(a_3);
				base.Document.ᜀ(doc, this);
				this.PictureShape.CloneRelationsTo(doc, nextOwner);
				this.ᜁ = false;
				if (true)
				{
				}
				num = 3;
				continue;
				IL_198:
				a_ = this.\u1716.ᜁ();
				a_2 = this.\u1716.ᜂ();
				a_3 = this.\u1716.ᜆ();
				num = 10;
				continue;
				IL_203:
				this.IsHeaderPicture = true;
				num = 8;
			}
		}

		// Token: 0x06004300 RID: 17152 RVA: 0x003EC3E8 File Offset: 0x003EB3E8
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 16;
			for (;;)
			{
				IL_25:
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_298:
					writer.WriteValue(ClipboardData.b("㽵୷㉹᥻ώ", a_), this.\u170D);
					num = 1;
					break;
				case 1:
					goto IL_45;
				default:
					goto IL_45;
				}
				for (;;)
				{
					IL_0B:
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						if (this.ᜇ != TextWrappingStyle.Inline)
						{
							num = 2;
							continue;
						}
						return;
					case 1:
						return;
					case 2:
						writer.WriteValue(ClipboardData.b("㹵᝷ࡹᕻѽ얉ﺋﮑ望", a_), this.ᜃ);
						writer.WriteValue(ClipboardData.b("⁵ᵷࡹࡻ᝽즅慎ﺏ", a_), this.ᜄ);
						writer.WriteValue(ClipboardData.b("⁵ᵷࡹࡻ᝽횅黎揄憐﶑望", a_), this.ᜆ);
						writer.WriteValue(ClipboardData.b("㹵᝷ࡹᕻѽ\uda89ﶍ憐ﶓ秊", a_), this.ᜅ);
						writer.WriteValue(ClipboardData.b("ⅵ੷᭹౻๽햅ﲇ", a_), this.ᜇ);
						writer.WriteValue(ClipboardData.b("ⅵ੷᭹౻๽튅憎", a_), this.ᜈ);
						writer.WriteValue(ClipboardData.b("㽵୷㡹᥻ች킃ﺉ", a_), this.ᜋ);
						writer.WriteValue(ClipboardData.b("㹵᝷ࡹᕻѽ쮉ﲑ煉", a_), this.ᜉ);
						writer.WriteValue(ClipboardData.b("⁵ᵷࡹࡻ᝽입ﶏ望", a_), this.ᜊ);
						writer.WriteValue(ClipboardData.b("╵ၷ᭹౻᭽쥿욁", a_), this.ᜌ);
						num = 4;
						continue;
					case 3:
						goto IL_298;
					case 4:
						if (this.\u170D)
						{
							num = 3;
							continue;
						}
						return;
					}
					goto IL_25;
				}
				IL_45:
				if (false)
				{
				}
				base.WriteXmlAttributes(writer);
				writer.WriteValue(ClipboardData.b("ɵŷ੹᥻", a_), ParagraphItemType.Picture);
				writer.WriteValue(ClipboardData.b("ŵᅷṹࡻᙽ", a_), this.Size.Width);
				writer.WriteValue(ClipboardData.b("ṵᵷ፹᭻ᙽ", a_), this.Size.Height);
				writer.WriteValue(ClipboardData.b("ⅵᅷṹࡻᙽ퍿", a_), this.ᜁ);
				writer.WriteValue(ClipboardData.b("㹵ᵷ፹᭻ᙽ톁", a_), this.ᜂ);
				writer.WriteValue(ClipboardData.b("㽵୷㝹᥻੽", a_), this.ImageRecord.ᜄ());
				num = 0;
				goto IL_0B;
			}
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x003EC694 File Offset: 0x003EB694
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 8;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				this.ᜀ.Width = reader.ReadFloat(ClipboardData.b("ᥭ᥯ᙱsṵ", a_));
				this.ᜀ.Height = reader.ReadFloat(ClipboardData.b("٭ᕯ᭱፳ṵ౷", a_));
				this.ᜁ = reader.ReadFloat(ClipboardData.b("㥭᥯ᙱsṵ⭷᥹ᵻች", a_));
				this.ᜂ = reader.ReadFloat(ClipboardData.b("♭ᕯ᭱፳ṵ౷⥹ύώ", a_));
				int num = 8;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (reader.HasAttribute(ClipboardData.b("㵭ᡯ፱ѳ፵ㅷ㹹", a_)))
						{
							num = 6;
							continue;
						}
						goto IL_2A8;
					case 1:
						if (reader.HasAttribute(ClipboardData.b("♭Ὧqᵳ౵᝷ᑹࡻώ쎁ﺏ", a_)))
						{
							num = 13;
							continue;
						}
						goto IL_274;
					case 2:
						goto IL_1A2;
					case 3:
						if (reader.HasAttribute(ClipboardData.b("❭ͯ㩱ᅳ᝵ᱷό๻", a_)))
						{
							num = 25;
							continue;
						}
						return;
					case 4:
						if (reader.HasAttribute(ClipboardData.b("㡭ᕯqsή᭷᭹ၻㅽ", a_)))
						{
							num = 28;
							continue;
						}
						goto IL_407;
					case 5:
						return;
					case 6:
						goto IL_346;
					case 7:
						this.ᜇ = (TextWrappingStyle)reader.ReadEnum(ClipboardData.b("㥭ɯ፱ѳٵᅷᑹ᭻⵽ﮁ", a_), typeof(TextWrappingStyle));
						num = 14;
						continue;
					case 8:
						if (reader.HasAttribute(ClipboardData.b("♭Ὧqᵳ౵᝷ᑹࡻώ춁", a_)))
						{
							num = 18;
							continue;
						}
						goto IL_53C;
					case 9:
						if (reader.HasAttribute(ClipboardData.b("㡭ᕯqsή᭷᭹ၻ⹽", a_)))
						{
							num = 19;
							continue;
						}
						goto IL_315;
					case 10:
						if (reader.HasAttribute(ClipboardData.b("㡭ᕯqsή᭷᭹ၻ㽽揄", a_)))
						{
							num = 26;
							continue;
						}
						goto IL_200;
					case 11:
						goto IL_2A8;
					case 12:
						goto IL_39A;
					case 13:
						this.ᜉ = (ShapeHorizontalAlignment)reader.ReadEnum(ClipboardData.b("♭Ὧqᵳ౵᝷ᑹࡻώ쎁ﺏ", a_), typeof(ShapeHorizontalAlignment));
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_346;
						default:
							if (false)
							{
							}
							num = 32;
							continue;
						}
						break;
					case 14:
						goto IL_508;
					case 15:
						goto IL_315;
					case 16:
						goto IL_200;
					case 17:
						this.ᜈ = (TextWrappingType)reader.ReadEnum(ClipboardData.b("㥭ɯ፱ѳٵᅷᑹ᭻⩽勵", a_), typeof(TextWrappingType));
						num = 2;
						continue;
					case 18:
						this.ᜃ = (HorizontalOrigin)reader.ReadEnum(ClipboardData.b("♭Ὧqᵳ౵᝷ᑹࡻώ춁", a_), typeof(HorizontalOrigin));
						num = 30;
						continue;
					case 19:
						this.ᜆ = reader.ReadFloat(ClipboardData.b("㡭ᕯqsή᭷᭹ၻ⹽", a_));
						num = 15;
						continue;
					case 20:
						this.ᜋ = reader.ReadBoolean(ClipboardData.b("❭ͯぱᅳ᩵᝷൹⡻᭽", a_));
						num = 27;
						continue;
					case 21:
						if (reader.HasAttribute(ClipboardData.b("♭Ὧqᵳ౵᝷ᑹࡻώ튁ﺉﺏ", a_)))
						{
							num = 31;
							continue;
						}
						goto IL_39A;
					case 22:
						goto IL_407;
					case 23:
						if (reader.HasAttribute(ClipboardData.b("㥭ɯ፱ѳٵᅷᑹ᭻⩽勵", a_)))
						{
							num = 17;
							continue;
						}
						goto IL_1A2;
					case 24:
						if (reader.HasAttribute(ClipboardData.b("❭ͯぱᅳ᩵᝷൹⡻᭽", a_)))
						{
							num = 20;
							continue;
						}
						goto IL_4D4;
					case 25:
						this.\u170D = reader.ReadBoolean(ClipboardData.b("❭ͯ㩱ᅳ᝵ᱷό๻", a_));
						num = 5;
						continue;
					case 26:
						this.ᜊ = (ShapeVerticalAlignment)reader.ReadEnum(ClipboardData.b("㡭ᕯqsή᭷᭹ၻ㽽揄", a_), typeof(ShapeVerticalAlignment));
						num = 16;
						continue;
					case 27:
						goto IL_4D4;
					case 28:
						this.ᜄ = (VerticalOrigin)reader.ReadEnum(ClipboardData.b("㡭ᕯqsή᭷᭹ၻㅽ", a_), typeof(VerticalOrigin));
						num = 22;
						continue;
					case 29:
						if (reader.HasAttribute(ClipboardData.b("㥭ɯ፱ѳٵᅷᑹ᭻⵽ﮁ", a_)))
						{
							num = 7;
							continue;
						}
						goto IL_508;
					case 30:
						goto IL_53C;
					case 31:
						this.ᜅ = reader.ReadFloat(ClipboardData.b("♭Ὧqᵳ౵᝷ᑹࡻώ튁ﺉﺏ", a_));
						num = 12;
						continue;
					case 32:
						goto IL_274;
					}
					break;
					IL_1A2:
					num = 24;
					continue;
					IL_200:
					num = 0;
					continue;
					IL_274:
					num = 10;
					continue;
					IL_2A8:
					num = 3;
					continue;
					IL_315:
					num = 21;
					continue;
					IL_346:
					this.ᜌ = reader.ReadInt(ClipboardData.b("㵭ᡯ፱ѳ፵ㅷ㹹", a_));
					num = 11;
					continue;
					IL_39A:
					num = 29;
					continue;
					IL_407:
					num = 9;
					continue;
					IL_4D4:
					num = 1;
					continue;
					IL_508:
					num = 23;
					continue;
					IL_53C:
					num = 4;
				}
			}
		}

		// Token: 0x06004302 RID: 17154 RVA: 0x003ECC3C File Offset: 0x003EBC3C
		protected override void WriteXmlContent(IXDLSContentWriter writer)
		{
			int a_ = 8;
			for (;;)
			{
				if (true)
				{
				}
				base.WriteXmlContent(writer);
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
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 1:
							if (this.\u1716 != null)
							{
								num = 2;
								continue;
							}
							return;
						case 2:
							writer.WriteChildBinaryElement(ClipboardData.b("ݭᵯ፱፳፵", a_), this.\u1716.ᜃ());
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

		// Token: 0x06004303 RID: 17155 RVA: 0x003ECCE0 File Offset: 0x003EBCE0
		protected override bool ReadXmlContent(IXDLSContentReader reader)
		{
			int a_ = 13;
			if (true)
			{
			}
			base.ReadXmlContent(reader);
			if (!(reader.TagName == ClipboardData.b("ᩲᡴᙶṸṺ", a_)))
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
					return false;
				}
			}
			Image image = this.ᜁ(reader.ReadChildBinaryElement());
			this.LoadImage(image);
			return true;
		}

		// Token: 0x06004304 RID: 17156 RVA: 0x003ECD60 File Offset: 0x003EBD60
		protected override void InitXDLSHolder()
		{
			int a_ = 0;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.XDLSHolder.AddElement(ClipboardData.b("եg୩ṫ཭፯ٱᅳѵ啷ᱹ፻౽", a_), this.m_charFormat);
			base.XDLSHolder.AddElement(ClipboardData.b("ᕥg୩ᱫ୭嵯ᑱ᭳ѵᕷ᭹ࡻ", a_), this.ᜎ);
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x003ECDE4 File Offset: 0x003EBDE4
		internal override void Close()
		{
			for (;;)
			{
				if (true)
				{
				}
				base.Close();
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.\u1716 != null)
						{
							num = 4;
							continue;
						}
						goto IL_50;
					case 1:
						if (!base.DeepDetached)
						{
							num = 2;
							continue;
						}
						goto IL_50;
					case 2:
					{
						sprᠾ u = this.\u1716;
						u.ᜂ(u.ᜅ() - 1);
						num = 7;
						continue;
					}
					case 3:
						return;
					case 4:
						goto IL_4E;
					case 5:
						if (this.\u1712 != null)
						{
							num = 6;
							continue;
						}
						return;
					case 6:
						this.\u1712.ᜅ();
						this.\u1712 = null;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4E;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 7:
						goto IL_50;
					}
					break;
					IL_4E:
					num = 1;
					continue;
					IL_50:
					num = 5;
				}
			}
		}

		// Token: 0x06004306 RID: 17158 RVA: 0x003ECEE4 File Offset: 0x003EBEE4
		internal new void ᜀ(byte[] A_0, byte[] A_1, bool A_2, bool A_3)
		{
			int a_ = 19;
			int num = 2;
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
						if (A_3)
						{
							num = 1;
							continue;
						}
						this.\u1716 = base.Document.Images.ᜂ(A_0);
						num = 3;
						continue;
					case 1:
						this.\u1716 = base.Document.Images.ᜀ(A_0, false);
						goto IL_72;
					case 3:
						goto IL_C8;
					case 4:
						goto IL_7A;
					case 5:
						goto IL_58;
					}
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					num = 0;
					continue;
				}
				IL_72:
				num = 4;
			}
			IL_58:
			throw new ArgumentNullException(ClipboardData.b("ၸᙺᱼ᡾", a_));
			IL_7A:
			IL_C8:
			if (true)
			{
			}
			A_0 = null;
			this.ᜀ = new SizeF(float.MinValue, float.MinValue);
		}

		// Token: 0x06004307 RID: 17159 RVA: 0x003ECFE8 File Offset: 0x003EBFE8
		internal new static byte[] ᜀ(Metafile A_0)
		{
			int a_ = 12;
			Rectangle bounds;
			Bitmap image;
			for (;;)
			{
				bounds = A_0.GetMetafileHeader().Bounds;
				image = null;
				try
				{
					image = new Bitmap(bounds.Width, bounds.Height, A_0.PixelFormat);
				}
				catch
				{
					throw new ArgumentException(ClipboardData.b("㭱ɳ᝵ᑷ፹᡻幽낏ﮓﮙ뺝", a_));
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
					goto IL_68;
				}
			}
			IL_68:
			if (false)
			{
			}
			Graphics graphics = Graphics.FromImage(image);
			IntPtr hdc = graphics.GetHdc();
			MemoryStream memoryStream = new MemoryStream();
			Metafile metafile = new Metafile(memoryStream, hdc, EmfType.EmfPlusOnly);
			graphics.ReleaseHdc(hdc);
			Graphics graphics2 = Graphics.FromImage(metafile);
			graphics2.DrawImageUnscaled(A_0, bounds);
			graphics2.Dispose();
			metafile.Dispose();
			byte[] result = memoryStream.ToArray();
			memoryStream.Close();
			return result;
		}

		// Token: 0x06004308 RID: 17160 RVA: 0x003ED0D8 File Offset: 0x003EC0D8
		internal static byte[] ᜂ(Image A_0)
		{
			if (true)
			{
			}
			MemoryStream memoryStream = new MemoryStream();
			byte[] result;
			try
			{
				try
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_AC;
						case 2:
							if (A_0.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
							{
								num = 4;
								continue;
							}
							A_0.Save(memoryStream, A_0.RawFormat);
							num = 1;
							continue;
						case 3:
							goto IL_B7;
						case 4:
							goto IL_93;
						case 5:
							num = 2;
							continue;
						case 6:
							goto IL_AC;
						}
						if (!A_0.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff))
						{
							num = 5;
							continue;
						}
						IL_93:
						A_0.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
						num = 6;
						continue;
						IL_AC:
						num = 3;
					}
					IL_B7:;
				}
				catch
				{
					A_0.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
				}
				result = memoryStream.ToArray();
			}
			finally
			{
				int num = 2;
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
							goto IL_124;
						case 1:
							((IDisposable)memoryStream).Dispose();
							goto IL_11C;
						}
						if (memoryStream != null)
						{
							num = 1;
							continue;
						}
						goto IL_126;
					}
					IL_11C:
					num = 0;
				}
				IL_124:
				IL_126:;
			}
			return result;
		}

		// Token: 0x06004309 RID: 17161 RVA: 0x003ED234 File Offset: 0x003EC234
		private new void ᜁ(Image A_0)
		{
			int num = 1;
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
					{
						sprᠾ u = this.\u1716;
						u.ᜂ(u.ᜅ() - 1);
						this.\u1716 = null;
						goto IL_64;
					}
					case 2:
						goto IL_7E;
					}
					if (this.\u1716 != null)
					{
						num = 0;
						continue;
					}
					goto IL_80;
				}
				IL_64:
				if (true)
				{
				}
				num = 2;
			}
			IL_7E:
			IL_80:
			this.\u1716 = base.Document.Images.ᜂ(DocPicture.ᜂ(A_0));
		}

		// Token: 0x0600430A RID: 17162 RVA: 0x003ED2E0 File Offset: 0x003EC2E0
		private new void ᜀ()
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜎ.ᜀ().ᜅ() != null)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					goto IL_6D;
				case 1:
				{
					sprᠾ u = this.\u1716;
					u.ᜂ(u.ᜅ() - 1);
					this.\u1716 = null;
					num = 4;
					continue;
				}
				case 2:
					this.ᜎ.ᜀ(null);
					num = 7;
					continue;
				case 4:
					goto IL_F7;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F7;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 6:
					if (this.\u1716 != null)
					{
						num = 1;
						continue;
					}
					goto IL_F9;
				case 7:
					goto IL_6D;
				}
				if (this.ᜎ.ᜀ() != null)
				{
					num = 5;
					continue;
				}
				IL_6D:
				num = 6;
			}
			IL_F7:
			IL_F9:
			this.ᜀ = new SizeF(float.MinValue, float.MinValue);
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x003ED3FC File Offset: 0x003EC3FC
		void spr\u1AB8.Draw(spr\u19E0 dc, sprᦰ ltWidget)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_34;
				case 1:
					if (this.ShapeInfo != null)
					{
						num = 3;
						continue;
					}
					return;
				case 3:
					dc.ᜁ(this.ShapeInfo, ltWidget);
					num = 4;
					continue;
				case 4:
					goto IL_5D;
				}
				if (this.Image != null)
				{
					num = 0;
				}
				else
				{
					num = 1;
				}
			}
			IL_34:
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (false)
				{
				}
				dc.ᜁ(this, ltWidget, true);
				return;
			}
			IL_5D:;
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x003ED4A8 File Offset: 0x003EC4A8
		SizeF spr\u2297.Measure(spr\u19E0 dc)
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
			return dc.ᜁ(this);
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x003ED4EC File Offset: 0x003EC4EC
		protected override void CreateLayoutInfo()
		{
			for (;;)
			{
				this.ᜀ = new spr\u22A8(ChildrenLayoutDirection.Horizontal);
				int num = 12;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 19;
						continue;
					case 1:
						goto IL_174;
					case 2:
						goto IL_209;
					case 3:
						if (this.\u1715().IsInCell)
						{
							num = 11;
							continue;
						}
						goto IL_151;
					case 4:
						if ((this.\u1715().OwnerBase as TableCell).IsFixedWidth)
						{
							num = 16;
							continue;
						}
						goto IL_198;
					case 5:
						if (this.\u1715().IsInCell)
						{
							if (true)
							{
							}
							num = 15;
							continue;
						}
						goto IL_198;
					case 6:
						num = 4;
						continue;
					case 7:
						goto IL_198;
					case 8:
						if (this.TextWrappingStyle != TextWrappingStyle.InFrontOfText)
						{
							num = 0;
							continue;
						}
						goto IL_209;
					case 9:
						this.ᜀ.ᜁ(true);
						num = 1;
						continue;
					case 10:
						if (!((spr\u1AB8)this.\u1715()).ᜀ().ᜀ())
						{
							num = 6;
							continue;
						}
						goto IL_241;
					case 11:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1DC;
						default:
							if (false)
							{
							}
							num = 21;
							continue;
						}
						break;
					case 12:
						if (base.PreviousSibling is DocOleObject)
						{
							num = 9;
							continue;
						}
						goto IL_174;
					case 13:
						this.ᜀ.ᜆ(true);
						num = 20;
						continue;
					case 14:
						this.ᜀ.ᜂ(true);
						num = 18;
						continue;
					case 15:
						goto IL_1DC;
					case 16:
						goto IL_241;
					case 17:
						if (this.TextWrappingStyle != TextWrappingStyle.Inline)
						{
							num = 14;
							continue;
						}
						return;
					case 18:
						return;
					case 19:
						if (this.TextWrappingStyle == TextWrappingStyle.Behind)
						{
							num = 2;
							continue;
						}
						num = 5;
						continue;
					case 20:
						goto IL_151;
					case 21:
						if ((this.\u1715().OwnerTextBody as TableCell).CellFormat.TextDirection != TextDirection.LeftToRight)
						{
							num = 13;
							continue;
						}
						goto IL_151;
					case 22:
						goto IL_198;
					}
					break;
					IL_151:
					num = 17;
					continue;
					IL_174:
					num = 8;
					continue;
					IL_198:
					num = 3;
					continue;
					IL_1DC:
					num = 10;
					continue;
					IL_209:
					this.ᜀ.ᜀ(false);
					num = 22;
					continue;
					IL_241:
					this.ᜀ.ᜀ(true);
					num = 7;
				}
			}
		}

		// Token: 0x0600430E RID: 17166 RVA: 0x003ED7A8 File Offset: 0x003EC7A8
		internal Paragraph \u1715()
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 4;
					continue;
				case 1:
					goto IL_10E;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10E;
					default:
						goto IL_66;
					}
					break;
				case 4:
					if (base.Owner.Owner.Owner is Paragraph)
					{
						num = 7;
						continue;
					}
					goto IL_110;
				case 5:
					if (base.Owner is spr\u1AD2)
					{
						num = 1;
						continue;
					}
					goto IL_110;
				case 6:
					if (base.Owner.Owner is sprờ)
					{
						num = 0;
						continue;
					}
					goto IL_110;
				case 7:
					goto IL_A2;
				}
				if (base.Owner is Paragraph)
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				num = 5;
				continue;
				IL_10E:
				num = 6;
			}
			IL_66:
			if (false)
			{
			}
			return base.OwnerParagraph;
			IL_A2:
			return base.Owner.Owner.Owner as Paragraph;
			IL_110:
			return null;
		}

		// Token: 0x0600430F RID: 17167 RVA: 0x003ED8C8 File Offset: 0x003EC8C8
		internal SizeF ᜃ(Image A_0)
		{
			int a_ = 2;
			switch (0)
			{
			default:
			{
				int num = 14;
				SizeF result;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_7A;
					case 1:
						if (A_0 is Metafile)
						{
							num = 11;
							continue;
						}
						goto IL_22D;
					case 2:
						goto IL_1D1;
					case 3:
						num = 1;
						continue;
					case 4:
						goto IL_22D;
					case 5:
						goto IL_75;
					case 6:
						if (A_0.PixelFormat != PixelFormat.Format8bppIndexed)
						{
							num = 12;
							continue;
						}
						goto IL_7A;
					case 7:
						if (A_0.PixelFormat != PixelFormat.Format32bppRgb)
						{
							num = 4;
							continue;
						}
						goto IL_7A;
					case 8:
						if (A_0.PixelFormat != PixelFormat.Format4bppIndexed)
						{
							if (true)
							{
							}
							num = 9;
							continue;
						}
						goto IL_7A;
					case 9:
						num = 15;
						continue;
					case 10:
					{
						if (!Enum.IsDefined(typeof(PixelFormat), A_0.PixelFormat))
						{
							num = 0;
							continue;
						}
						Graphics graphics = Graphics.FromImage(A_0);
						num = 2;
						continue;
					}
					case 11:
						num = 7;
						continue;
					case 12:
						num = 8;
						continue;
					case 13:
						goto IL_A2;
					case 15:
						if (A_0.PixelFormat != PixelFormat.Format1bppIndexed)
						{
							num = 3;
							continue;
						}
						goto IL_7A;
					}
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					result = A_0.Size;
					this.\u1716.ᜀ(A_0.Size);
					this.\u1716.ᜀ(A_0.RawFormat);
					num = 6;
					continue;
					IL_7A:
					spr\u1C39 spr_u1C = new spr\u1C39();
					result = spr_u1C.ᜀ(A_0.Size, PrintUnits.Point, A_0.HorizontalResolution);
					num = 13;
					continue;
					IL_22D:
					num = 10;
				}
				IL_75:
				goto IL_1AA;
				IL_A2:
				return result;
				IL_1AA:
				throw new ArgumentNullException(ClipboardData.b("ŧݩ൫७ᕯ", a_));
				IL_1D1:
				try
				{
					Graphics graphics;
					spr\u1C39 spr_u1C2 = new spr\u1C39(graphics);
					return spr_u1C2.ᜀ(A_0.Size, PrintUnits.Point);
				}
				finally
				{
					num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_198;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_198;
							default:
								if (false)
								{
								}
								break;
							}
							break;
						case 2:
							goto IL_1A7;
						}
						Graphics graphics;
						if (graphics != null)
						{
							num = 0;
							continue;
						}
						break;
						IL_198:
						((IDisposable)graphics).Dispose();
						num = 2;
					}
					IL_1A7:;
				}
				goto IL_1AA;
			}
			}
		}

		// Token: 0x06004310 RID: 17168 RVA: 0x003EDB7C File Offset: 0x003ECB7C
		private new void ᜀ(Image A_0)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					goto IL_4F;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4F;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				}
				if (A_0 != null)
				{
					num = 1;
					continue;
				}
				break;
				IL_4F:
				this.ᜀ = this.ᜃ(A_0);
				if (true)
				{
				}
				num = 0;
			}
		}

		// Token: 0x06004311 RID: 17169 RVA: 0x003EDBF8 File Offset: 0x003ECBF8
		private new Rectangle ᜀ(RectangleF A_0)
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
			double num = (double)(A_0.Left + A_0.Width * Math.Max(0f, this.CropFromLeft));
			double num2 = (double)(A_0.Left + A_0.Width * (1f - Math.Max(0f, this.CropFromRight)));
			double num3 = (double)(A_0.Top + A_0.Height * Math.Max(0f, this.CropFromTop));
			double num4 = (double)(A_0.Top + A_0.Height * (1f - Math.Max(0f, this.CropFromBottom)));
			return Rectangle.FromLTRB((int)num, (int)num3, (int)num2, (int)num4);
		}

		// Token: 0x04003529 RID: 13609
		private new SizeF ᜀ;

		// Token: 0x0400352A RID: 13610
		private new float ᜁ = 100f;

		// Token: 0x0400352B RID: 13611
		private float ᜂ = 100f;

		// Token: 0x0400352C RID: 13612
		private HorizontalOrigin ᜃ;

		// Token: 0x0400352D RID: 13613
		private new VerticalOrigin ᜄ;

		// Token: 0x0400352E RID: 13614
		private float ᜅ;

		// Token: 0x0400352F RID: 13615
		private float ᜆ;

		// Token: 0x04003530 RID: 13616
		private TextWrappingStyle ᜇ;

		// Token: 0x04003531 RID: 13617
		private TextWrappingType ᜈ;

		// Token: 0x04003532 RID: 13618
		private ShapeHorizontalAlignment ᜉ;

		// Token: 0x04003533 RID: 13619
		private ShapeVerticalAlignment ᜊ;

		// Token: 0x04003534 RID: 13620
		private bool ᜋ;

		// Token: 0x04003535 RID: 13621
		private int ᜌ;

		// Token: 0x04003536 RID: 13622
		private long[] \u25D9\u009A\u008F\u00A0;

		// Token: 0x04003537 RID: 13623
		private bool \u170D;

		// Token: 0x04003538 RID: 13624
		private sprẛ ᜎ;

		// Token: 0x04003539 RID: 13625
		private List<Stream> ᜏ;

		// Token: 0x0400353A RID: 13626
		private string ᜐ;

		// Token: 0x0400353B RID: 13627
		private string ᜑ;

		// Token: 0x0400353C RID: 13628
		private new Body \u1712;

		// Token: 0x0400353D RID: 13629
		private new bool \u1713;

		// Token: 0x0400353E RID: 13630
		private int \u1714 = int.MaxValue;

		// Token: 0x0400353F RID: 13631
		private bool \u1715;

		// Token: 0x04003540 RID: 13632
		private sprᠾ \u1716;

		// Token: 0x04003541 RID: 13633
		private float \u1717;

		// Token: 0x04003542 RID: 13634
		private bool[] \u2609\u00A0\u00A8\u0099;

		// Token: 0x04003543 RID: 13635
		private byte \u25D8\u008D\u00B0\u007F;

		// Token: 0x04003544 RID: 13636
		private float \u1718;

		// Token: 0x04003545 RID: 13637
		private float \u1719 = 9.5f;

		// Token: 0x04003546 RID: 13638
		private float \u171A = 9.5f;

		// Token: 0x04003547 RID: 13639
		private float \u171B;

		// Token: 0x04003548 RID: 13640
		private float \u171C;

		// Token: 0x04003549 RID: 13641
		private float \u171D;

		// Token: 0x0400354A RID: 13642
		private float \u171E;

		// Token: 0x0400354B RID: 13643
		private float \u171F;

		// Token: 0x0400354C RID: 13644
		private float ᜠ;

		// Token: 0x0400354D RID: 13645
		private Color ᜡ;

		// Token: 0x0400354E RID: 13646
		private Borders ᜢ;

		// Token: 0x0400354F RID: 13647
		private PictureColor ᜣ;

		// Token: 0x04003550 RID: 13648
		private spr\u1B7E ᜤ;

		// Token: 0x04003551 RID: 13649
		[CompilerGenerated]
		private bool ᜥ;

		// Token: 0x04003552 RID: 13650
		[CompilerGenerated]
		private spr\u248F ᜦ;
	}
}
