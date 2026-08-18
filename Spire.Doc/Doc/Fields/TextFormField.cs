using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Interface;
using Spire.Layouting;

namespace Spire.Doc.Fields
{
	// Token: 0x02000522 RID: 1314
	public class TextFormField : FormField, spr\u2297
	{
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060044F1 RID: 17649 RVA: 0x00404A64 File Offset: 0x00403A64
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
				return DocumentObjectType.TextFormField;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060044F2 RID: 17650 RVA: 0x00404AA4 File Offset: 0x00403AA4
		// (set) Token: 0x060044F3 RID: 17651 RVA: 0x00404AE8 File Offset: 0x00403AE8
		public TextFormFieldType TextFieldType
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜁ = value;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060044F4 RID: 17652 RVA: 0x00404B2C File Offset: 0x00403B2C
		// (set) Token: 0x060044F5 RID: 17653 RVA: 0x00404B70 File Offset: 0x00403B70
		public string StringFormat
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

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060044F6 RID: 17654 RVA: 0x00404BB4 File Offset: 0x00403BB4
		// (set) Token: 0x060044F7 RID: 17655 RVA: 0x00404BF8 File Offset: 0x00403BF8
		public string DefaultText
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

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060044F8 RID: 17656 RVA: 0x00404C3C File Offset: 0x00403C3C
		// (set) Token: 0x060044F9 RID: 17657 RVA: 0x00404C80 File Offset: 0x00403C80
		public int MaximumLength
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
				int a_ = 5;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						if (value != 0)
						{
							goto IL_8E;
						}
						goto IL_9B;
					case 2:
						goto IL_99;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8E;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					if (true)
					{
					}
					if (value < this.ᜂ.Length)
					{
						num = 3;
						continue;
					}
					goto IL_9B;
					IL_8E:
					num = 2;
				}
				IL_99:
				throw new ArgumentOutOfRangeException(ClipboardData.b("♪౬ᝮᡰṲt᩶㕸Ṻ፼᡾ꖄ愈ꮊ랖ﲜ膠삢키햦\udba8캪쎬\udbae醰잲킴쾶춸鮺톼\udabe꿀꓂뇄꿆", a_));
				IL_9B:
				this.ᜃ = value;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060044FA RID: 17658 RVA: 0x00404D30 File Offset: 0x00403D30
		// (set) Token: 0x060044FB RID: 17659 RVA: 0x00404D74 File Offset: 0x00403D74
		public TextRange TextRange
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
				return this.ᜅ;
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
				this.ᜅ = value;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060044FC RID: 17660 RVA: 0x00404DB8 File Offset: 0x00403DB8
		// (set) Token: 0x060044FD RID: 17661 RVA: 0x00404E00 File Offset: 0x00403E00
		public override string Text
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
				return this.ᜅ.Text;
			}
			set
			{
				int a_ = 1;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_AE;
					case 2:
						num = 3;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5D;
						default:
							if (false)
							{
							}
							if (string.IsNullOrEmpty(value))
							{
								num = 0;
								continue;
							}
							goto IL_D0;
						}
						break;
					case 4:
						if (base.FormFieldType == FormFieldType.TextInput)
						{
							num = 2;
							continue;
						}
						goto IL_D0;
					case 5:
						num = 4;
						continue;
					}
					if (base.Document.ᜇ)
					{
						goto IL_D0;
					}
					if (true)
					{
					}
					num = 5;
				}
				IL_5D:
				this.ᜅ.Text = ClipboardData.b("敆歈楊潌济", a_);
				return;
				IL_AE:
				goto IL_5D;
				IL_D0:
				this.ᜅ.Text = value;
			}
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x00404EEC File Offset: 0x00403EEC
		public TextFormField(IDocument doc) : base(doc)
		{
			this.m_curFormFieldType = FormFieldType.TextInput;
			this.m_paraItemType = ParagraphItemType.TextFormField;
			base.Type = FieldType.FieldFormTextInput;
			base.Params = 128;
			this.ᜂ = string.Empty;
			this.ᜅ = new TextRange(doc);
			this.ᜄ = string.Empty;
		}

		// Token: 0x060044FF RID: 17663 RVA: 0x00404F44 File Offset: 0x00403F44
		protected override object CloneImpl()
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
			return (TextFormField)base.CloneImpl();
		}

		// Token: 0x06004500 RID: 17664 RVA: 0x00404F8C File Offset: 0x00403F8C
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 5;
			for (;;)
			{
				IL_67:
				base.ReadXmlAttributes(reader);
				int num = 8;
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
							return;
						case 1:
							goto IL_C6;
						case 2:
							if (reader.HasAttribute(ClipboardData.b("㽪࡬ᝮհ❲౴ݶᱸ", a_)))
							{
								num = 9;
								continue;
							}
							return;
						case 3:
							this.ᜃ = reader.ReadInt(ClipboardData.b("♪౬ᝮ㵰ᙲ᭴ၶ൸፺", a_));
							num = 6;
							continue;
						case 4:
							this.ᜂ = reader.ReadString(ClipboardData.b("⽪࡬८ၰٲᥴͶ⵸Ṻռ୾", a_));
							num = 1;
							continue;
						case 5:
							if (reader.HasAttribute(ClipboardData.b("㡪ᥬᵮᡰᵲቴ⍶ᱸͺॼ㥾ﶈ", a_)))
							{
								if (true)
								{
								}
								num = 10;
								continue;
							}
							goto IL_193;
						case 6:
							goto IL_FF;
						case 7:
							goto IL_C1;
						case 8:
							if (reader.HasAttribute(ClipboardData.b("♪౬ᝮ㵰ᙲ᭴ၶ൸፺", a_)))
							{
								num = 3;
								continue;
							}
							goto IL_FF;
						case 9:
							this.ᜁ = (TextFormFieldType)reader.ReadEnum(ClipboardData.b("㽪࡬ᝮհ❲౴ݶᱸ", a_), typeof(TextFormFieldType));
							num = 0;
							continue;
						case 10:
							this.ᜄ = reader.ReadString(ClipboardData.b("㡪ᥬᵮᡰᵲቴ⍶ᱸͺॼ㥾ﶈ", a_));
							num = 7;
							continue;
						case 11:
							if (reader.HasAttribute(ClipboardData.b("⽪࡬८ၰٲᥴͶ⵸Ṻռ୾", a_)))
							{
								num = 4;
								continue;
							}
							goto IL_C6;
						}
						goto IL_67;
						IL_C6:
						num = 5;
						continue;
						IL_FF:
						num = 11;
						continue;
					}
					IL_193:
					num = 2;
					continue;
					IL_C1:
					goto IL_193;
				}
			}
		}

		// Token: 0x06004501 RID: 17665 RVA: 0x00405188 File Offset: 0x00404188
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 11;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			base.WriteXmlAttributes(writer);
			writer.WriteValue(ClipboardData.b("㱰ቲ൴㭶ᱸᕺ᩼୾", a_), this.ᜃ);
			writer.WriteValue(ClipboardData.b("㕰ᙲ፴ᙶ౸᝺ॼ⭾ﮂ", a_), this.ᜂ);
			writer.WriteValue(ClipboardData.b("≰ݲݴṶ᝸ᱺ⥼᩾呂쎄ﮈﮎ", a_), this.ᜄ);
			writer.WriteValue(ClipboardData.b("╰ᙲ൴Ͷ⵸ɺർ᩾", a_), (int)this.ᜁ);
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x0040523C File Offset: 0x0040423C
		protected override void InitXDLSHolder()
		{
			int a_ = 8;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.XDLSHolder.AddElement(ClipboardData.b("ᩭᕯੱs孵੷᭹ቻ᥽", a_), this.ᜅ);
		}

		// Token: 0x06004503 RID: 17667 RVA: 0x004052A0 File Offset: 0x004042A0
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
			return dc.ᜀ(this.TextRange, this.TextRange.Text);
		}

		// Token: 0x06004504 RID: 17668 RVA: 0x004052F4 File Offset: 0x004042F4
		protected override void CreateLayoutInfo()
		{
			int a_ = 16;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜀ = ((this.Text == ClipboardData.b("罵", a_)) ? new TextRange.ᜀ(this) : new spr\u22A8(ChildrenLayoutDirection.Horizontal));
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06004505 RID: 17669 RVA: 0x0040536C File Offset: 0x0040436C
		spr\u1D30 spr\u1AB8.LayoutInfo
		{
			get
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
						this.CreateLayoutInfo();
						num = 1;
						continue;
					case 1:
						goto IL_50;
					}
					goto IL_24;
					IL_2C:
					num = 0;
					continue;
					IL_24:
					if (this.ᜀ == null)
					{
						goto IL_2C;
					}
					IL_50:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2C;
					default:
						goto IL_66;
					}
				}
				IL_66:
				if (false)
				{
				}
				return this.ᜀ;
			}
		}

		// Token: 0x06004506 RID: 17670 RVA: 0x004053EC File Offset: 0x004043EC
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
			dc.ᜀ(this.TextRange, ltWidget, this.TextRange.Text);
		}

		// Token: 0x04003622 RID: 13858
		private bool \u2609\u009C\u0095\u00AB;

		// Token: 0x04003623 RID: 13859
		internal new const string ᜀ = "\u2002\u2002\u2002\u2002\u2002";

		// Token: 0x04003624 RID: 13860
		private bool \u2593\u0091\u0084\u00AC;

		// Token: 0x04003625 RID: 13861
		private new TextFormFieldType ᜁ;

		// Token: 0x04003626 RID: 13862
		private bool \u25D8\u0090\u008B\u00AD;

		// Token: 0x04003627 RID: 13863
		private string ᜂ;

		// Token: 0x04003628 RID: 13864
		private new int ᜃ;

		// Token: 0x04003629 RID: 13865
		private long[] \u2593\u008D\u0095\u008C;

		// Token: 0x0400362A RID: 13866
		private new string ᜄ;

		// Token: 0x0400362B RID: 13867
		private TextRange ᜅ;
	}
}
