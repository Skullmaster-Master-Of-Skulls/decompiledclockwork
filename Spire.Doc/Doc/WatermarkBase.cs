using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Interface;

namespace Spire.Doc
{
	// Token: 0x020000D9 RID: 217
	public class WatermarkBase : ParagraphBase
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0001A88C File Offset: 0x0001988C
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
				return DocumentObjectType.Undefined;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0001A8CC File Offset: 0x000198CC
		public WatermarkType Type
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

		// Token: 0x06000275 RID: 629 RVA: 0x0001A910 File Offset: 0x00019910
		internal WatermarkBase(WatermarkType A_0) : base(null)
		{
			this.ᜀ = A_0;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0001A940 File Offset: 0x00019940
		internal WatermarkBase(Document A_0, WatermarkType A_1) : base(A_0)
		{
			this.ᜀ = A_1;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0001A970 File Offset: 0x00019970
		internal override void RemoveSelf()
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

		// Token: 0x06000278 RID: 632 RVA: 0x0001A9AC File Offset: 0x000199AC
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 0;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.WriteXmlAttributes(writer);
			writer.WriteValue(ClipboardData.b("ㅥ१ṩ५ᱭᵯ፱ٳᵵⱷ͹౻᭽", a_), this.ᜀ);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0001AA18 File Offset: 0x00019A18
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 12;
			for (;;)
			{
				IL_1D:
				base.ReadXmlAttributes(reader);
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_70:
					this.ᜀ = (WatermarkType)reader.ReadEnum(ClipboardData.b("╱ᕳɵᵷࡹᅻώ킃ﾅ", a_), typeof(WatermarkType));
					num = 1;
					break;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_66;
					case 1:
						return;
					case 2:
						if (reader.HasAttribute(ClipboardData.b("╱ᕳɵᵷࡹᅻώ킃ﾅ", a_)))
						{
							num = 0;
							continue;
						}
						return;
					}
					goto IL_1D;
				}
				IL_66:
				if (true)
				{
				}
				goto IL_70;
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0001AAD8 File Offset: 0x00019AD8
		protected override void CreateLayoutInfo()
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

		// Token: 0x04000C71 RID: 3185
		private new WatermarkType ᜀ;

		// Token: 0x04000C72 RID: 3186
		private new bool ᜁ = true;

		// Token: 0x04000C73 RID: 3187
		private bool ᜂ = true;

		// Token: 0x04000C74 RID: 3188
		private int[] \u2593\u00B0\u0099\u0099;

		// Token: 0x04000C75 RID: 3189
		private float[] \u2460\u0082\u0084\u0087;

		// Token: 0x04000C76 RID: 3190
		private string[] \u25D9\u0096\u00A3\u008B;

		// Token: 0x04000C77 RID: 3191
		private bool ᜃ = true;
	}
}
