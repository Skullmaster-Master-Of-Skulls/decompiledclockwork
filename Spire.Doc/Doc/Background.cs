using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc.Core.DataStreamParser.Escher;
using Spire.Doc.Documents;
using Spire.Doc.Documents.XML;
using Spire.Doc.Fields;
using Spire.Doc.Interface;

namespace Spire.Doc
{
	// Token: 0x020000D3 RID: 211
	public class Background : DocumentSerializable
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00016DD8 File Offset: 0x00015DD8
		// (set) Token: 0x06000214 RID: 532 RVA: 0x00016E1C File Offset: 0x00015E1C
		public BackgroundType Type
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
				this.ᜀ = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00016E60 File Offset: 0x00015E60
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00016EB4 File Offset: 0x00015EB4
		public Image Picture
		{
			get
			{
				if (this.ᜃ != null)
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
						return this.ᜃ;
					}
				}
				return this.ᜀ();
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
				this.ᜆ = BackgroundFillType.msofillPicture;
				this.ᜁ(value);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00016F00 File Offset: 0x00015F00
		internal Image Image
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_70;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_24;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							this.ᜃ = this.ᜀ();
							num = 0;
							continue;
						}
						break;
					}
					goto IL_1C;
					IL_24:
					num = 2;
					continue;
					IL_1C:
					if (this.ᜃ == null)
					{
						goto IL_24;
					}
					break;
				}
				IL_70:
				return this.ᜃ;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00016F88 File Offset: 0x00015F88
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00016FCC File Offset: 0x00015FCC
		public Color Color
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

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00017010 File Offset: 0x00016010
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00017054 File Offset: 0x00016054
		public BackgroundGradient Gradient
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

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00017098 File Offset: 0x00016098
		// (set) Token: 0x0600021D RID: 541 RVA: 0x000170DC File Offset: 0x000160DC
		internal sprᠾ ImageRecord
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
				return this.ᜄ;
			}
			set
			{
				if (true)
				{
				}
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
							goto IL_2C;
						default:
						{
							if (false)
							{
							}
							sprᠾ sprᠾ = this.ᜄ;
							sprᠾ.ᜂ(sprᠾ.ᜅ() - 1);
							num = 2;
							continue;
						}
						}
						break;
					case 2:
						goto IL_77;
					}
					goto IL_24;
					IL_2C:
					num = 1;
					continue;
					IL_24:
					if (this.ᜄ != null)
					{
						goto IL_2C;
					}
					break;
				}
				IL_77:
				this.ᜄ = value;
				sprᠾ sprᠾ2 = this.ᜄ;
				sprᠾ2.ᜂ(sprᠾ2.ᜅ() + 1);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0001717C File Offset: 0x0001617C
		// (set) Token: 0x0600021F RID: 543 RVA: 0x000171C0 File Offset: 0x000161C0
		internal byte[] ImageBytes
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
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜇ = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00017204 File Offset: 0x00016204
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00017248 File Offset: 0x00016248
		internal BackgroundFillType FillType
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
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜆ = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0001728C File Offset: 0x0001628C
		internal Color PictureBackColor
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
				return this.ᜂ;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000172D0 File Offset: 0x000162D0
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00017314 File Offset: 0x00016314
		internal Stream PatternFill2010
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
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00017358 File Offset: 0x00016358
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0001739C File Offset: 0x0001639C
		internal Stream PatternFill
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

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000227 RID: 551 RVA: 0x000173E0 File Offset: 0x000163E0
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00017424 File Offset: 0x00016424
		internal byte[] PatternImageBytes
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜌ = value;
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00017468 File Offset: 0x00016468
		internal Background(BackgroundType A_0) : base(null, null)
		{
			this.ᜀ = A_0;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000174A8 File Offset: 0x000164A8
		internal Background(Document A_0) : base(A_0, null)
		{
			this.ᜉ = A_0.Escher;
			this.ᜀ(this.ᜉ.ᜇ(), true);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000174FC File Offset: 0x000164FC
		internal Background(Document A_0, spr\u2459 A_1) : base(A_0, null)
		{
			this.ᜉ = A_0.Escher;
			this.ᜀ(A_1, false);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00017548 File Offset: 0x00016548
		internal Background ᜇ()
		{
			Background background;
			for (;;)
			{
				background = new Background(this.Type);
				int num = 1;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_98;
					case 1:
						if (this.ImageBytes == null)
						{
							goto IL_9A;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_98;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						background.ImageBytes = new byte[this.ImageBytes.Length];
						this.ImageBytes.CopyTo(background.ImageBytes, 0);
						num = 0;
						continue;
					}
					break;
				}
			}
			IL_98:
			IL_9A:
			background.Gradient = this.Gradient.Clone();
			background.Color = this.Color;
			return background;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00017610 File Offset: 0x00016610
		internal void ᜀ(Document A_0)
		{
			int num = 0;
			for (;;)
			{
				sprᠾ sprᠾ;
				switch (num)
				{
				case 1:
					return;
				case 2:
					this.ᜄ = A_0.Images.ᜀ(sprᠾ.ᜂ, true);
					num = 3;
					continue;
				case 3:
					goto IL_DC;
				case 4:
					goto IL_DC;
				case 5:
					if (sprᠾ.ᜄ())
					{
						if (true)
						{
						}
						num = 2;
						continue;
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
						this.ᜄ = A_0.Images.ᜂ(sprᠾ.ᜃ());
						num = 4;
						continue;
					}
					break;
				case 6:
					sprᠾ = this.ᜄ;
					num = 5;
					continue;
				}
				if (this.ᜄ != null)
				{
					num = 6;
					continue;
				}
				break;
				IL_DC:
				this.ᜄ.ᜀ(sprᠾ.ᜁ());
				this.ᜄ.ᜀ(sprᠾ.ᜂ());
				this.ᜄ.ᜁ(sprᠾ.ᜆ());
				sprᠾ.ᜈ();
				sprᠾ = null;
				num = 1;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00017744 File Offset: 0x00016744
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 3;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_ED;
					case 2:
						if (reader.HasAttribute(ClipboardData.b("⽨ɪŬͮ㍰ቲᙴᱶṸॺቼ੾욄ﾌ", a_)))
						{
							num = 3;
							continue;
						}
						return;
					case 3:
						this.ᜂ = reader.ReadColor(ClipboardData.b("⽨ɪŬͮ㍰ቲᙴᱶṸॺቼ੾욄ﾌ", a_));
						num = 0;
						continue;
					case 4:
						goto IL_148;
					case 5:
						if (reader.HasAttribute(ClipboardData.b("⽨ɪŬͮ㉰ᱲᥴᡶ୸", a_)))
						{
							num = 7;
							continue;
						}
						goto IL_181;
					case 6:
						if (reader.HasAttribute(ClipboardData.b("㵨ቪᵬ੮", a_)))
						{
							num = 4;
							continue;
						}
						goto IL_ED;
					case 7:
						this.ᜁ = reader.ReadColor(ClipboardData.b("⽨ɪŬͮ㉰ᱲᥴᡶ୸", a_));
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_148;
						default:
							if (false)
							{
							}
							num = 10;
							continue;
						}
						break;
					case 8:
						if (reader.HasAttribute(ClipboardData.b("⁨ᡪ⁬੮հቲ፴ṶᕸṺ", a_)))
						{
							num = 9;
							continue;
						}
						goto IL_BC;
					case 9:
						this.ᜈ = reader.ReadBoolean(ClipboardData.b("⁨ᡪ⁬੮հቲ፴ṶᕸṺ", a_));
						num = 11;
						continue;
					case 10:
						goto IL_181;
					case 11:
						if (true)
						{
						}
						goto IL_BC;
					}
					break;
					IL_BC:
					num = 5;
					continue;
					IL_ED:
					num = 8;
					continue;
					IL_148:
					this.ᜀ = (BackgroundType)reader.ReadEnum(ClipboardData.b("㵨ቪᵬ੮", a_), typeof(BackgroundType));
					num = 1;
					continue;
					IL_181:
					num = 2;
				}
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00017940 File Offset: 0x00016940
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 4;
			for (;;)
			{
				base.WriteXmlAttributes(writer);
				writer.WriteValue(ClipboardData.b("㹩ᕫṭᕯ", a_), this.ᜀ);
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
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_97;
						case 1:
							writer.WriteValue(ClipboardData.b("⍩Ὣ⍭ᕯٱᕳၵᅷᙹ᥻", a_), this.ᜈ);
							num = 3;
							continue;
						case 2:
							writer.WriteValue(ClipboardData.b("Ⱪիɭᱯぱᕳᕵ፷ᵹ๻ᅽ얅ﲍ", a_), this.ᜂ);
							num = 6;
							continue;
						case 3:
							goto IL_143;
						case 4:
							if (this.ᜈ)
							{
								num = 1;
								continue;
							}
							goto IL_143;
						case 5:
							if (this.ᜂ != Color.White)
							{
								num = 2;
								continue;
							}
							return;
						case 6:
							return;
						case 7:
							if (true)
							{
							}
							if (this.ᜁ != Color.White)
							{
								num = 8;
								continue;
							}
							goto IL_97;
						case 8:
							writer.WriteValue(ClipboardData.b("Ⱪիɭᱯㅱ᭳᩵᝷ࡹ", a_), this.ᜁ);
							num = 0;
							continue;
						}
						break;
						IL_97:
						num = 5;
						continue;
						IL_143:
						num = 7;
					}
					break;
				}
				}
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00017AC8 File Offset: 0x00016AC8
		protected override void WriteXmlContent(IXDLSContentWriter writer)
		{
			int a_ = 13;
			base.WriteXmlContent(writer);
			if (this.ᜇ != null)
			{
				if (true)
				{
				}
			}
			else
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
					(writer as sprṑ).ᜁ(this.ᜃ);
					return;
				}
			}
			writer.WriteChildBinaryElement(ClipboardData.b("ᩲᡴᙶṸṺ", a_), this.ᜇ);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00017B4C File Offset: 0x00016B4C
		protected override bool ReadXmlContent(IXDLSContentReader reader)
		{
			int a_ = 7;
			bool result;
			for (;;)
			{
				result = base.ReadXmlContent(reader);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
						if (!(reader.TagName == ClipboardData.b("Ѭɮၰᑲၴ", a_)))
						{
							return result;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return result;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						if (true)
						{
						}
						this.ᜇ = reader.ReadChildBinaryElement();
						num = 0;
						continue;
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00017BF4 File Offset: 0x00016BF4
		protected override void InitXDLSHolder()
		{
			int a_ = 19;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.InitXDLSHolder();
			base.XDLSHolder.AddElement(ClipboardData.b("Ṹॺᱼ᭾", a_), this.ᜅ);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00017C60 File Offset: 0x00016C60
		private void ᜀ(spr\u2459 A_0, bool A_1)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜀ = (this.m_doc.DOP.\u171C().ᜑ() ? BackgroundType.Color : BackgroundType.NoBackground);
					num = 6;
					continue;
				case 1:
				{
					if (this.ᜀ == BackgroundType.NoBackground)
					{
						num = 9;
						continue;
					}
					BackgroundType backgroundType = this.ᜀ;
					num = 5;
					continue;
				}
				case 2:
					if (!A_0.ᜄ())
					{
						num = 10;
						continue;
					}
					this.ᜆ = A_0.ᜉ();
					this.ᜀ = A_0.\u1712();
					num = 7;
					continue;
				case 4:
					num = 2;
					continue;
				case 5:
				{
					BackgroundType backgroundType;
					switch (backgroundType)
					{
					case BackgroundType.Gradient:
						goto IL_1D6;
					case BackgroundType.Picture:
					case BackgroundType.Texture:
						goto IL_5B;
					case BackgroundType.Color:
						goto IL_D5;
					default:
						num = 8;
						continue;
					}
					break;
				}
				case 6:
					goto IL_194;
				case 7:
					if (this.ᜀ == BackgroundType.NoBackground)
					{
						num = 13;
						continue;
					}
					goto IL_194;
				case 8:
					return;
				case 9:
					return;
				case 10:
					return;
				case 11:
					if (A_1)
					{
						num = 12;
						continue;
					}
					goto IL_194;
				case 12:
					num = 0;
					continue;
				case 13:
					num = 11;
					continue;
				}
				if (A_0 != null)
				{
					num = 4;
					continue;
				}
				return;
				IL_194:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					num = 1;
					break;
				}
			}
			IL_5B:
			if (true)
			{
			}
			this.ᜄ = A_0.ᜀ(this.ᜉ);
			this.ᜂ = A_0.ᜁ(true);
			return;
			IL_D5:
			this.ᜁ = A_0.ᜁ(false);
			return;
			IL_1D6:
			this.ᜅ = new BackgroundGradient(base.Document, A_0);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00017E58 File Offset: 0x00016E58
		private Image ᜀ()
		{
			int a_ = 15;
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				MemoryStream stream;
				switch (num)
				{
				case 1:
					goto IL_64;
				case 2:
					IL_A1:
					try
					{
						this.ᜃ = Image.FromStream(stream, true, false);
						goto IL_A3;
					}
					catch
					{
						throw new ArgumentException(ClipboardData.b("㑴նṸ๺ၼ᩾ꖄ愈ꮊ뎒ﲔ殺ﲚ뾞쎠\udaa2톤슦覨쪪\udfac\uddae킰쪲", a_));
					}
					goto IL_64;
				}
				if (this.ᜇ != null)
				{
					num = 1;
					continue;
				}
				break;
				IL_64:
				stream = new MemoryStream(this.ᜇ);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A1;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
			}
			IL_A3:
			return this.ᜃ;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00017F20 File Offset: 0x00016F20
		private void ᜁ(Image A_0)
		{
			int a_ = 10;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_84;
				case 1:
					goto IL_E3;
				case 2:
					this.ᜄ = base.Document.Images.ᜀ(DocPicture.ᜀ(A_0 as Metafile), false);
					num = 0;
					continue;
				case 3:
					if (true)
					{
					}
					if (A_0 is Metafile)
					{
						num = 2;
						continue;
					}
					this.ᜄ = base.Document.Images.ᜂ(DocPicture.ᜂ(A_0));
					num = 1;
					continue;
				case 4:
					goto IL_58;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_86;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				IL_86:
				num = 3;
			}
			IL_58:
			throw new ArgumentNullException(ClipboardData.b("᥯άᕳᅵᵷ", a_));
			IL_84:
			IL_E3:
			this.ᜄ.ᜀ(A_0);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00018028 File Offset: 0x00017028
		private void ᜀ(Metafile A_0)
		{
			int a_ = 1;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_78;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			Rectangle bounds = A_0.GetMetafileHeader().Bounds;
			Bitmap image = null;
			try
			{
				image = new Bitmap(bounds.Width, bounds.Height, A_0.PixelFormat);
			}
			catch
			{
				throw new ArgumentException(ClipboardData.b("⹦Ὠ੪Ŭٮᕰ卲ᡴቶ൸᩺᭼ᙾꖄ力뎒", a_));
			}
			IL_78:
			Graphics graphics = Graphics.FromImage(image);
			IntPtr hdc = graphics.GetHdc();
			MemoryStream memoryStream = new MemoryStream();
			Metafile metafile = new Metafile(memoryStream, hdc, EmfType.EmfOnly);
			graphics.ReleaseHdc(hdc);
			Graphics graphics2 = Graphics.FromImage(metafile);
			graphics2.DrawImageUnscaled(A_0, bounds);
			graphics2.Dispose();
			metafile.Dispose();
			this.ᜇ = memoryStream.ToArray();
			memoryStream.Close();
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00018118 File Offset: 0x00017118
		private void ᜀ(Image A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				try
				{
					A_0.Save(memoryStream, A_0.RawFormat);
				}
				catch
				{
					A_0.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
				}
				this.ᜇ = memoryStream.ToArray();
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_99;
					case 1:
						((IDisposable)memoryStream).Dispose();
						num = 0;
						continue;
					}
					if (memoryStream == null)
					{
						break;
					}
					num = 1;
				}
				IL_99:;
			}
		}

		// Token: 0x04000C36 RID: 3126
		private int \u25D8\u008D\u0095\u0083;

		// Token: 0x04000C37 RID: 3127
		private new BackgroundType ᜀ;

		// Token: 0x04000C38 RID: 3128
		private Color ᜁ = Color.White;

		// Token: 0x04000C39 RID: 3129
		private Color ᜂ = Color.White;

		// Token: 0x04000C3A RID: 3130
		private long \u25D8\u0087\u0091\u00A5;

		// Token: 0x04000C3B RID: 3131
		private string[] \u2609\u00B0\u0097\u00AB;

		// Token: 0x04000C3C RID: 3132
		private Image ᜃ;

		// Token: 0x04000C3D RID: 3133
		private sprᠾ ᜄ;

		// Token: 0x04000C3E RID: 3134
		private long \u25D9\u009C\u0094\u008C;

		// Token: 0x04000C3F RID: 3135
		private BackgroundGradient ᜅ = new BackgroundGradient();

		// Token: 0x04000C40 RID: 3136
		private BackgroundFillType ᜆ;

		// Token: 0x04000C41 RID: 3137
		private byte[] ᜇ;

		// Token: 0x04000C42 RID: 3138
		private byte[] \u2593\u0098\u0083\u0084;

		// Token: 0x04000C43 RID: 3139
		private bool[] \u2593\u0096\u008B\u00A2;

		// Token: 0x04000C44 RID: 3140
		private bool ᜈ;

		// Token: 0x04000C45 RID: 3141
		private spr\u24E3 ᜉ;

		// Token: 0x04000C46 RID: 3142
		private Stream ᜊ;

		// Token: 0x04000C47 RID: 3143
		private Stream ᜋ;

		// Token: 0x04000C48 RID: 3144
		private byte[] ᜌ;
	}
}
