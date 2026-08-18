using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

namespace Spire.Doc.Fields
{
	// Token: 0x02000514 RID: 1300
	public class Symbol : ParagraphBase, spr\u2297
	{
		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06004337 RID: 17207 RVA: 0x003F0230 File Offset: 0x003EF230
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
				return DocumentObjectType.Symbol;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06004338 RID: 17208 RVA: 0x003F0270 File Offset: 0x003EF270
		public CharacterFormat CharacterFormat
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

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06004339 RID: 17209 RVA: 0x003F02B4 File Offset: 0x003EF2B4
		// (set) Token: 0x0600433A RID: 17210 RVA: 0x003F02F8 File Offset: 0x003EF2F8
		public string FontName
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

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600433B RID: 17211 RVA: 0x003F033C File Offset: 0x003EF33C
		// (set) Token: 0x0600433C RID: 17212 RVA: 0x003F0380 File Offset: 0x003EF380
		public byte CharacterCode
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

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600433D RID: 17213 RVA: 0x003F03C4 File Offset: 0x003EF3C4
		// (set) Token: 0x0600433E RID: 17214 RVA: 0x003F0408 File Offset: 0x003EF408
		internal byte CharCodeExt
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

		// Token: 0x0600433F RID: 17215 RVA: 0x003F044C File Offset: 0x003EF44C
		public Symbol(IDocument doc)
		{
			int a_ = 3;
			this.ᜀ = ClipboardData.b("㩨ቪl൮Ṱὲ", a_);
			base..ctor((Document)doc);
			this.m_charFormat = new CharacterFormat(doc);
			this.m_charFormat.ᜀ(this);
		}

		// Token: 0x06004340 RID: 17216 RVA: 0x003F049C File Offset: 0x003EF49C
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
			this.ᜀ.ᜁ(false);
		}

		// Token: 0x06004341 RID: 17217 RVA: 0x003F04F0 File Offset: 0x003EF4F0
		protected override object CloneImpl()
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
			return (Symbol)base.CloneImpl();
		}

		// Token: 0x06004342 RID: 17218 RVA: 0x003F0538 File Offset: 0x003EF538
		protected override void InitXDLSHolder()
		{
			int a_ = 0;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			base.InitXDLSHolder();
			base.XDLSHolder.AddElement(ClipboardData.b("եg୩ṫ཭፯ٱᅳѵ啷ᱹ፻౽", a_), this.m_charFormat);
		}

		// Token: 0x06004343 RID: 17219 RVA: 0x003F05A4 File Offset: 0x003EF5A4
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 17;
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
			writer.WriteValue(ClipboardData.b("Ͷx୺᡼", a_), ParagraphItemType.Symbol);
			writer.WriteValue(ClipboardData.b("ㅶᙸᕺॼㅾ", a_), this.FontName);
			writer.WriteValue(ClipboardData.b("㑶ᅸོ᩺㱾", a_), (int)this.CharacterCode);
			writer.WriteValue(ClipboardData.b("㑶ᅸོ᩺㱾슆ﾊ", a_), (int)this.CharCodeExt);
		}

		// Token: 0x06004344 RID: 17220 RVA: 0x003F0658 File Offset: 0x003EF658
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 5;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_CE;
						default:
							if (false)
							{
							}
							goto IL_138;
						}
						break;
					case 1:
						if (reader.HasAttribute(ClipboardData.b("⡪լ๮Ͱひᩴ፶ᱸ㹺ռ୾", a_)))
						{
							num = 2;
							continue;
						}
						return;
					case 2:
						this.CharCodeExt = reader.ReadByte(ClipboardData.b("⡪լ๮Ͱひᩴ፶ᱸ㹺ռ୾", a_));
						num = 8;
						continue;
					case 3:
						goto IL_71;
					case 4:
						goto IL_CE;
					case 5:
						if (reader.HasAttribute(ClipboardData.b("⡪լ๮Ͱひᩴ፶ᱸ", a_)))
						{
							num = 7;
							continue;
						}
						goto IL_71;
					case 6:
						if (reader.HasAttribute(ClipboardData.b("⵪ɬŮհ㵲ᑴ᩶ᱸ", a_)))
						{
							num = 4;
							continue;
						}
						goto IL_138;
					case 7:
						this.CharacterCode = reader.ReadByte(ClipboardData.b("⡪լ๮Ͱひᩴ፶ᱸ", a_));
						num = 3;
						continue;
					case 8:
						return;
					}
					break;
					IL_71:
					if (true)
					{
					}
					num = 1;
					continue;
					IL_CE:
					this.FontName = reader.ReadString(ClipboardData.b("⵪ɬŮհ㵲ᑴ᩶ᱸ", a_));
					num = 0;
					continue;
					IL_138:
					num = 5;
				}
			}
		}

		// Token: 0x06004345 RID: 17221 RVA: 0x003F07D4 File Offset: 0x003EF7D4
		SizeF spr\u2297.Measure(spr\u19E0 dc)
		{
			string a_;
			CharacterFormat characterFormat;
			for (;;)
			{
				a_ = char.ConvertFromUtf32((int)this.CharacterCode);
				characterFormat = new CharacterFormat(base.Document);
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						characterFormat.ImportContainer(this.CharacterFormat);
						characterFormat.ᜃ(this.CharacterFormat);
						characterFormat.ApplyBase(base.OwnerParagraph.BreakCharacterFormat.BaseFormat);
						characterFormat.FontSize = this.CharacterFormat.FontSize;
						characterFormat.FontName = this.FontName;
						num = 5;
						continue;
					case 1:
						if (this.FontName != this.CharacterFormat.FontName)
						{
							goto IL_B5;
						}
						goto IL_126;
					case 2:
						num = 8;
						continue;
					case 3:
						if (!this.CharacterFormat.HasValue(0))
						{
							num = 2;
							continue;
						}
						goto IL_126;
					case 4:
						if (true)
						{
						}
						num = 1;
						continue;
					case 5:
						if (characterFormat.IsSmallCaps)
						{
							num = 7;
							continue;
						}
						goto IL_6B;
					case 6:
						if (this.CharacterFormat.IsSmallCaps)
						{
							num = 9;
							continue;
						}
						goto IL_1CC;
					case 7:
						goto IL_1C7;
					case 8:
						if (!(this.FontName != string.Empty))
						{
							goto IL_126;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B5;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 9:
						goto IL_14C;
					}
					break;
					IL_B5:
					num = 0;
					continue;
					IL_126:
					num = 6;
				}
			}
			IL_6B:
			return dc.ᜁ(a_, characterFormat.Font, null);
			IL_14C:
			return dc.ᜀ(a_, this.CharacterFormat.Font, null, true);
			IL_1C7:
			return dc.ᜀ(a_, characterFormat.Font, null, true);
			IL_1CC:
			return dc.ᜁ(a_, this.CharacterFormat.Font, null);
		}

		// Token: 0x06004346 RID: 17222 RVA: 0x003F09C0 File Offset: 0x003EF9C0
		void spr\u1AB8.Draw(spr\u19E0 dc, sprᦰ ltWidget)
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
			dc.ᜀ(this, ltWidget);
		}

		// Token: 0x0400356B RID: 13675
		private bool \u25D9\u009B\u00A5\u0099;

		// Token: 0x0400356C RID: 13676
		private new string ᜀ;

		// Token: 0x0400356D RID: 13677
		private new byte ᜁ;

		// Token: 0x0400356E RID: 13678
		private byte ᜂ;
	}
}
