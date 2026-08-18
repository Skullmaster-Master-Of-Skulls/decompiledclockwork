using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Core.DataStreamParser.Escher;
using Spire.Doc.Documents;
using Spire.Doc.Documents.XML;
using Spire.Doc.Interface;

namespace Spire.Doc
{
	// Token: 0x020000D5 RID: 213
	public class BackgroundGradient : DocumentSerializable
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00018F98 File Offset: 0x00017F98
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00018FDC File Offset: 0x00017FDC
		public Color Color1
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

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00019020 File Offset: 0x00018020
		// (set) Token: 0x06000247 RID: 583 RVA: 0x00019064 File Offset: 0x00018064
		public Color Color2
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜈ = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000190A8 File Offset: 0x000180A8
		// (set) Token: 0x06000249 RID: 585 RVA: 0x000190EC File Offset: 0x000180EC
		public GradientShadingStyle ShadingStyle
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

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00019130 File Offset: 0x00018130
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00019174 File Offset: 0x00018174
		public GradientShadingVariant ShadingVariant
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
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜊ = value;
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000191B8 File Offset: 0x000181B8
		public BackgroundGradient() : base(null, null)
		{
			this.ᜇ = Color.White;
			this.ᜈ = Color.Black;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000191FC File Offset: 0x000181FC
		internal BackgroundGradient(Document A_0, spr\u2459 A_1) : base(A_0, null)
		{
			this.ᜋ = A_0.Escher;
			this.ᜀ(A_1);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0001923C File Offset: 0x0001823C
		public BackgroundGradient Clone()
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
			return (BackgroundGradient)base.CloneImpl();
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00019284 File Offset: 0x00018284
		private void ᜀ(spr\u2459 A_0)
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
			this.ᜆ = A_0.ᜉ();
			this.ᜇ = A_0.ᜁ(false);
			this.ᜈ = A_0.ᜁ(true);
			this.ᜉ = A_0.ᜀ(this.ᜆ);
			this.ᜊ = A_0.ᜂ(this.ᜉ);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0001930C File Offset: 0x0001830C
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 14;
			for (;;)
			{
				if (true)
				{
				}
				base.WriteXmlAttributes(writer);
				int num = 10;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (this.ᜊ == GradientShadingVariant.ShadingUp)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A6;
						default:
							if (false)
							{
							}
							num = 9;
							continue;
						}
						break;
					case 2:
						writer.WriteValue(ClipboardData.b("㉳ήᑷᙹ㽻ᅽ", a_), this.ᜇ);
						num = 8;
						continue;
					case 3:
						goto IL_14F;
					case 4:
						writer.WriteValue(ClipboardData.b("㍳ѵ᥷ṹᕻ᭽힃솑ﾙ", a_), this.ᜉ);
						num = 3;
						continue;
					case 5:
						if (this.ᜉ != GradientShadingStyle.Horizontal)
						{
							num = 4;
							continue;
						}
						goto IL_14F;
					case 6:
						if (this.ᜈ != Color.White)
						{
							num = 7;
							continue;
						}
						goto IL_A6;
					case 7:
						writer.WriteValue(ClipboardData.b("㉳ήᑷᙹ㹻ώﾉ펏﶑秊", a_), this.ᜈ);
						num = 11;
						continue;
					case 8:
						goto IL_C9;
					case 9:
						writer.WriteValue(ClipboardData.b("㍳ѵ᥷ṹᕻ᭽힃쒑ﮙ", a_), this.ᜊ);
						num = 0;
						continue;
					case 10:
						if (this.ᜇ != Color.White)
						{
							num = 2;
							continue;
						}
						goto IL_C9;
					case 11:
						goto IL_A6;
					}
					break;
					IL_A6:
					num = 5;
					continue;
					IL_C9:
					num = 6;
					continue;
					IL_14F:
					num = 1;
				}
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000194DC File Offset: 0x000184DC
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 10;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜉ = (GradientShadingStyle)reader.ReadEnum(ClipboardData.b("㝯qᕳትᅷόቻ੽퍿\udd8d", a_), typeof(GradientShadingStyle));
						num = 7;
						continue;
					case 1:
						this.ᜈ = reader.ReadColor(ClipboardData.b("㙯᭱ᡳ᩵㩷᭹ύᕽ쾋ﲏ﶑", a_));
						num = 3;
						continue;
					case 2:
						this.ᜇ = reader.ReadColor(ClipboardData.b("㙯᭱ᡳ᩵㭷ᕹၻᅽ", a_));
						num = 8;
						continue;
					case 3:
						goto IL_AF;
					case 4:
						return;
					case 5:
						if (reader.HasAttribute(ClipboardData.b("㙯᭱ᡳ᩵㭷ᕹၻᅽ", a_)))
						{
							num = 2;
							continue;
						}
						goto IL_E8;
					case 6:
						if (true)
						{
						}
						if (reader.HasAttribute(ClipboardData.b("㝯qᕳትᅷόቻ੽퍿\udd8d", a_)))
						{
							num = 0;
							continue;
						}
						goto IL_186;
					case 7:
						goto IL_186;
					case 8:
						goto IL_E8;
					case 9:
						if (reader.HasAttribute(ClipboardData.b("㙯᭱ᡳ᩵㩷᭹ύᕽ쾋ﲏ﶑", a_)))
						{
							num = 1;
							continue;
						}
						goto IL_AF;
					case 10:
						if (!reader.HasAttribute(ClipboardData.b("㝯qᕳትᅷόቻ੽퍿\ud88dﶓ", a_)))
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AF;
						default:
							if (false)
							{
							}
							num = 11;
							continue;
						}
						break;
					case 11:
						this.ᜊ = (GradientShadingVariant)reader.ReadEnum(ClipboardData.b("㝯qᕳትᅷόቻ੽퍿\ud88dﶓ", a_), typeof(GradientShadingVariant));
						num = 4;
						continue;
					}
					break;
					IL_AF:
					num = 6;
					continue;
					IL_E8:
					num = 9;
					continue;
					IL_186:
					num = 10;
				}
			}
		}

		// Token: 0x04000C51 RID: 3153
		private bool \u25D8\u00AE\u00A1\u00A5;

		// Token: 0x04000C52 RID: 3154
		private bool \u25D8\u00B0\u00AD\u00AE;

		// Token: 0x04000C53 RID: 3155
		internal new const uint ᜀ = 4289069056U;

		// Token: 0x04000C54 RID: 3156
		internal const uint ᜁ = 4286119936U;

		// Token: 0x04000C55 RID: 3157
		internal const uint ᜂ = 4292018176U;

		// Token: 0x04000C56 RID: 3158
		internal const uint ᜃ = 100U;

		// Token: 0x04000C57 RID: 3159
		internal const uint ᜄ = 4294967246U;

		// Token: 0x04000C58 RID: 3160
		private float[] \u25D8\u00A6ª\u00AE;

		// Token: 0x04000C59 RID: 3161
		private bool \u2609\u00A6\u008B\u0085;

		// Token: 0x04000C5A RID: 3162
		internal const uint ᜅ = 50U;

		// Token: 0x04000C5B RID: 3163
		private BackgroundFillType ᜆ;

		// Token: 0x04000C5C RID: 3164
		private Color ᜇ = Color.White;

		// Token: 0x04000C5D RID: 3165
		private float \u2609\u009A\u008A\u00A9;

		// Token: 0x04000C5E RID: 3166
		private Color ᜈ = Color.White;

		// Token: 0x04000C5F RID: 3167
		private GradientShadingStyle ᜉ;

		// Token: 0x04000C60 RID: 3168
		private GradientShadingVariant ᜊ;

		// Token: 0x04000C61 RID: 3169
		private spr\u24E3 ᜋ;
	}
}
