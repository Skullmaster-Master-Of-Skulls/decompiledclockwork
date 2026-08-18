using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents.XML;
using Spire.Doc.Interface;

namespace Spire.Doc.Formatting
{
	// Token: 0x02000474 RID: 1140
	public class ViewSetup : DocumentSerializable
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06003EDF RID: 16095 RVA: 0x003A0254 File Offset: 0x0039F254
		// (set) Token: 0x06003EE0 RID: 16096 RVA: 0x003A0298 File Offset: 0x0039F298
		public int ZoomPercent
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
				int a_ = 4;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						if (value > 500)
						{
							num = 2;
							continue;
						}
						goto IL_94;
					case 2:
						goto IL_40;
					case 3:
						IL_3E:
						num = 1;
						continue;
					}
					if (value >= 10)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					IL_40:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3E;
					default:
						goto IL_56;
					}
				}
				IL_56:
				if (false)
				{
				}
				throw new ArgumentOutOfRangeException(ClipboardData.b("どͫŭᵯ剱ѳ፵੷᥹᥻ၽꢇ曆ﶍ늑뢗鍊힟잡솣좥袧鮩鲫躭톯\udcb1킳隵趷誹費麽낿ꟁ뛃ꗅ귇꓉룋", a_));
				IL_94:
				this.ᜁ = value;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06003EE1 RID: 16097 RVA: 0x003A0340 File Offset: 0x0039F340
		// (set) Token: 0x06003EE2 RID: 16098 RVA: 0x003A0384 File Offset: 0x0039F384
		public ZoomType ZoomType
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
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06003EE3 RID: 16099 RVA: 0x003A03C8 File Offset: 0x0039F3C8
		// (set) Token: 0x06003EE4 RID: 16100 RVA: 0x003A040C File Offset: 0x0039F40C
		public DocumentViewType DocumentViewType
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
				this.ᜂ = value;
			}
		}

		// Token: 0x06003EE5 RID: 16101 RVA: 0x003A0450 File Offset: 0x0039F450
		public ViewSetup(IDocument doc) : base((Document)doc, null)
		{
			this.ᜀ = ZoomType.None;
			this.ᜂ = DocumentViewType.PrintLayout;
			this.ᜁ = 100;
		}

		// Token: 0x06003EE6 RID: 16102 RVA: 0x003A0480 File Offset: 0x0039F480
		internal ViewSetup ᜀ(Document A_0)
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
			ViewSetup viewSetup = (ViewSetup)this.CloneImpl();
			viewSetup.ᜀ(A_0);
			return viewSetup;
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x003A04D0 File Offset: 0x0039F4D0
		internal void ᜀ(int A_0)
		{
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					A_0 = 100;
					num = 3;
					continue;
				case 1:
					goto IL_5F;
				case 2:
					if (true)
					{
					}
					num = 6;
					continue;
				case 3:
					goto IL_5F;
				case 4:
					goto IL_5F;
				case 5:
					if (A_0 <= 500)
					{
						goto IL_5F;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A7;
					default:
						if (false)
						{
						}
						num = 11;
						continue;
					}
					break;
				case 6:
					if (A_0 <= 500)
					{
						num = 9;
						continue;
					}
					return;
				case 7:
					if (A_0 >= 10)
					{
						num = 2;
						continue;
					}
					return;
				case 9:
					goto IL_A7;
				case 10:
					return;
				case 11:
					A_0 = 500;
					num = 1;
					continue;
				case 12:
					if (A_0 < 10)
					{
						num = 13;
						continue;
					}
					num = 5;
					continue;
				case 13:
					A_0 = 10;
					num = 4;
					continue;
				}
				if (A_0 == 0)
				{
					num = 0;
					continue;
				}
				num = 12;
				continue;
				IL_5F:
				num = 7;
				continue;
				IL_A7:
				this.ᜁ = A_0;
				num = 10;
			}
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x003A062C File Offset: 0x0039F62C
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 4;
			for (;;)
			{
				for (;;)
				{
					base.WriteXmlAttributes(writer);
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.DocumentViewType != DocumentViewType.PrintLayout)
							{
								num = 7;
								continue;
							}
							return;
						case 1:
							writer.WriteValue(ClipboardData.b("どͫŭᵯ♱൳ٵᵷ", a_), this.ZoomType);
							num = 8;
							continue;
						case 2:
							goto IL_108;
						case 3:
							goto IL_126;
						case 4:
							if (this.ZoomType != ZoomType.None)
							{
								num = 1;
								continue;
							}
							goto IL_6D;
						case 5:
							if (this.ZoomPercent != 100)
							{
								num = 6;
								continue;
							}
							goto IL_126;
						case 6:
							if (true)
							{
							}
							writer.WriteValue(ClipboardData.b("どͫŭᵯ≱ᅳѵ᭷όቻ੽", a_), this.ZoomPercent);
							num = 3;
							continue;
						case 7:
							writer.WriteValue(ClipboardData.b("㱩ի୭ݯ♱൳ٵᵷ", a_), this.DocumentViewType);
							num = 2;
							continue;
						case 8:
							goto IL_6D;
						}
						break;
						IL_6D:
						num = 0;
						continue;
						IL_126:
						num = 4;
					}
				}
				IL_108:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_11E;
				}
			}
			IL_11E:
			if (false)
			{
			}
		}

		// Token: 0x06003EE9 RID: 16105 RVA: 0x003A0788 File Offset: 0x0039F788
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 8;
			for (;;)
			{
				for (;;)
				{
					base.ReadXmlAttributes(reader);
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (reader.HasAttribute(ClipboardData.b("㑭Ὧᵱᥳ♵ᵷࡹύ᭽", a_)))
							{
								num = 2;
								continue;
							}
							goto IL_156;
						case 1:
							if (reader.HasAttribute(ClipboardData.b("㑭Ὧᵱᥳ≵ŷ੹᥻", a_)))
							{
								num = 7;
								continue;
							}
							goto IL_71;
						case 2:
							this.ZoomPercent = reader.ReadInt(ClipboardData.b("㑭Ὧᵱᥳ♵ᵷࡹύ᭽", a_));
							num = 5;
							continue;
						case 3:
							goto IL_71;
						case 4:
							goto IL_130;
						case 5:
							goto IL_156;
						case 6:
							this.DocumentViewType = (DocumentViewType)reader.ReadEnum(ClipboardData.b("㡭᥯᝱ͳ≵ŷ੹᥻", a_), typeof(DocumentViewType));
							num = 4;
							continue;
						case 7:
							this.ZoomType = (ZoomType)reader.ReadEnum(ClipboardData.b("㑭Ὧᵱᥳ≵ŷ੹᥻", a_), typeof(ZoomType));
							num = 3;
							continue;
						case 8:
							if (reader.HasAttribute(ClipboardData.b("㡭᥯᝱ͳ≵ŷ੹᥻", a_)))
							{
								num = 6;
								continue;
							}
							return;
						}
						break;
						IL_71:
						num = 8;
						continue;
						IL_156:
						num = 1;
					}
				}
				IL_130:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_146;
				}
			}
			IL_146:
			if (false)
			{
			}
			if (true)
			{
			}
		}

		// Token: 0x04002DCF RID: 11727
		private long \u2593\u0082\u00A4\u007F;

		// Token: 0x04002DD0 RID: 11728
		public const int DEF_ZOOMING = 100;

		// Token: 0x04002DD1 RID: 11729
		private byte[] \u25D8\u00AB\u0088\u008E;

		// Token: 0x04002DD2 RID: 11730
		private byte[] \u25D8\u008B\u0094\u0088;

		// Token: 0x04002DD3 RID: 11731
		private new ZoomType ᜀ;

		// Token: 0x04002DD4 RID: 11732
		private int ᜁ;

		// Token: 0x04002DD5 RID: 11733
		private bool[] \u2460\u00A1\u00A4\u0091;

		// Token: 0x04002DD6 RID: 11734
		private DocumentViewType ᜂ;
	}
}
