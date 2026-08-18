using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

namespace Spire.Doc.Fields
{
	// Token: 0x02000519 RID: 1305
	public class FieldMark : ParagraphBase
	{
		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060043CD RID: 17357 RVA: 0x003F8498 File Offset: 0x003F7498
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
				return DocumentObjectType.FieldMark;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060043CE RID: 17358 RVA: 0x003F84D8 File Offset: 0x003F74D8
		internal CharacterFormat CharacterFormat
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
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060043CF RID: 17359 RVA: 0x003F851C File Offset: 0x003F751C
		// (set) Token: 0x060043D0 RID: 17360 RVA: 0x003F8560 File Offset: 0x003F7560
		public FieldMarkType Type
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

		// Token: 0x060043D1 RID: 17361 RVA: 0x003F85A4 File Offset: 0x003F75A4
		internal FieldMark(IDocument A_0) : base((Document)A_0)
		{
			this.m_charFormat = new CharacterFormat(A_0);
		}

		// Token: 0x060043D2 RID: 17362 RVA: 0x003F85CC File Offset: 0x003F75CC
		protected internal FieldMark(FieldMark fieldMark, IDocument doc) : this(doc)
		{
			this.Type = fieldMark.Type;
			this.m_charFormat = (CharacterFormat)fieldMark.CharacterFormat.ឱ();
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x003F8604 File Offset: 0x003F7604
		internal FieldMark(IDocument A_0, FieldMarkType A_1) : base((Document)A_0)
		{
			this.ᜀ = A_1;
			this.m_charFormat = new CharacterFormat(A_0);
		}

		// Token: 0x060043D4 RID: 17364 RVA: 0x003F8630 File Offset: 0x003F7630
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 16;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					return;
				case 2:
					if (true)
					{
					}
					this.ᜀ = (FieldMarkType)reader.ReadEnum(ClipboardData.b("ふᅷόၻ᩽쵿\udc87ﲋ", a_), typeof(FieldMarkType));
					goto IL_9C;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_9C:
					num = 1;
					break;
				default:
					if (false)
					{
					}
					if (!reader.HasAttribute(ClipboardData.b("ふᅷόၻ᩽쵿\udc87ﲋ", a_)))
					{
						return;
					}
					num = 2;
					break;
				}
			}
		}

		// Token: 0x060043D5 RID: 17365 RVA: 0x003F86E8 File Offset: 0x003F76E8
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
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
			base.WriteXmlAttributes(writer);
			writer.WriteValue(ClipboardData.b("ብᅧᩩ५", a_), ParagraphItemType.FieldMark);
			writer.WriteValue(ClipboardData.b("⁥ŧཀྵk੭㵯፱ٳᵵⱷ͹౻᭽", a_), this.ᜀ);
		}

		// Token: 0x060043D6 RID: 17366 RVA: 0x003F876C File Offset: 0x003F776C
		protected override void InitXDLSHolder()
		{
			int a_ = 4;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.XDLSHolder.AddElement(ClipboardData.b("३ѫ཭ɯ፱ᝳɵᵷࡹ养᡽ﲇ", a_), this.m_charFormat);
		}

		// Token: 0x060043D7 RID: 17367 RVA: 0x003F87D0 File Offset: 0x003F77D0
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
			this.ᜀ = new spr\u22A8();
		}

		// Token: 0x040035A4 RID: 13732
		private byte \u25D8\u0091\u0090\u0099;

		// Token: 0x040035A5 RID: 13733
		private string \u25D9\u00A7\u0099\u00AC;

		// Token: 0x040035A6 RID: 13734
		private long \u25D9\u00A8\u008A\u0096;

		// Token: 0x040035A7 RID: 13735
		private byte \u2460\u00A6\u009B\u0080;

		// Token: 0x040035A8 RID: 13736
		private new FieldMarkType ᜀ;
	}
}
