using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Interface;
using Spire.Doc.Rendering;
using Spire.Layouting;

namespace Spire.Doc.Fields
{
	// Token: 0x02000521 RID: 1313
	public class DropDownFormField : FormField, spr\u2297
	{
		// Token: 0x060044E0 RID: 17632 RVA: 0x00404340 File Offset: 0x00403340
		public DropDownFormField(IDocument doc) : base(doc)
		{
			this.m_curFormFieldType = FormFieldType.DropDown;
			this.m_paraItemType = ParagraphItemType.DropDownFormField;
			base.Type = FieldType.FieldFormDropDown;
			base.Params = 32998;
			this.ᜁ = new DropDownCollection(base.Document);
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060044E1 RID: 17633 RVA: 0x00404388 File Offset: 0x00403388
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
				return DocumentObjectType.DropDownFormField;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060044E2 RID: 17634 RVA: 0x004043C8 File Offset: 0x004033C8
		// (set) Token: 0x060044E3 RID: 17635 RVA: 0x0040441C File Offset: 0x0040341C
		public int DropDownSelectedIndex
		{
			get
			{
				if (base.InnerValue == 25)
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
						return (int)this.ᜀ;
					}
				}
				if (true)
				{
				}
				return base.InnerValue;
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
				base.InnerValue = value;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060044E4 RID: 17636 RVA: 0x00404460 File Offset: 0x00403460
		public DropDownCollection DropDownItems
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
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060044E5 RID: 17637 RVA: 0x004044A4 File Offset: 0x004034A4
		// (set) Token: 0x060044E6 RID: 17638 RVA: 0x004044E8 File Offset: 0x004034E8
		internal int DefaultDropDownValue
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
				return (int)this.ᜀ;
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
				this.ᜀ = (short)value;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060044E7 RID: 17639 RVA: 0x0040452C File Offset: 0x0040352C
		// (set) Token: 0x060044E8 RID: 17640 RVA: 0x00404580 File Offset: 0x00403580
		internal string DropDownValue
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
				return this.ᜁ[this.DropDownSelectedIndex].Text;
			}
			set
			{
				int num;
				for (;;)
				{
					num = 0;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (string.Compare(this.ᜁ[num].Text, value, true) == 0)
							{
								num2 = 4;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_93;
							default:
								if (false)
								{
								}
								num++;
								num2 = 1;
								continue;
							}
							break;
						case 1:
							goto IL_9F;
						case 2:
							return;
						case 3:
							goto IL_9F;
						case 4:
							goto IL_93;
						case 5:
							if (num >= this.ᜁ.Count)
							{
								num2 = 2;
								continue;
							}
							if (true)
							{
							}
							num2 = 0;
							continue;
						}
						break;
						IL_9F:
						num2 = 5;
					}
				}
				IL_93:
				this.DropDownSelectedIndex = num;
			}
		}

		// Token: 0x060044E9 RID: 17641 RVA: 0x00404654 File Offset: 0x00403654
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
			DropDownFormField dropDownFormField = (DropDownFormField)base.CloneImpl();
			dropDownFormField.ᜁ = new DropDownCollection(base.Document);
			this.ᜁ.ᜀ(dropDownFormField.ᜁ);
			return dropDownFormField;
		}

		// Token: 0x060044EA RID: 17642 RVA: 0x004046C0 File Offset: 0x004036C0
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 7;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (!reader.HasAttribute(ClipboardData.b("⥬੮ᝰቲt᭶൸㽺ོၾ잂\udd8a", a_)))
						{
							return;
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
							num = 2;
							continue;
						}
						break;
					case 2:
						this.ᜀ = reader.ReadShort(ClipboardData.b("⥬੮ᝰቲt᭶൸㽺ོၾ잂\udd8a", a_));
						if (true)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
			}
		}

		// Token: 0x060044EB RID: 17643 RVA: 0x00404770 File Offset: 0x00403770
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
			writer.WriteValue(ClipboardData.b("㕰ᙲ፴ᙶ౸᝺ॼ㭾쎆ﲊ\ud98eﾒ", a_), (int)this.ᜀ);
		}

		// Token: 0x060044EC RID: 17644 RVA: 0x004047D8 File Offset: 0x004037D8
		protected override void InitXDLSHolder()
		{
			int a_ = 2;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.InitXDLSHolder();
			base.XDLSHolder.AddElement(ClipboardData.b("౧ᡩͫṭᑯᵱͳᡵ啷፹ࡻ᭽", a_), this.ᜁ);
		}

		// Token: 0x060044ED RID: 17645 RVA: 0x00404844 File Offset: 0x00403844
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
			this.ᜀ = new spr\u22A8(ChildrenLayoutDirection.Horizontal);
		}

		// Token: 0x060044EE RID: 17646 RVA: 0x0040488C File Offset: 0x0040388C
		SizeF spr\u2297.Measure(spr\u19E0 dc)
		{
			string a_;
			for (;;)
			{
				a_ = string.Empty;
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜁ.Count <= 0)
						{
							goto IL_79;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_77;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 1:
						a_ = this.DropDownValue;
						num = 2;
						continue;
					case 2:
						goto IL_77;
					}
					break;
				}
			}
			IL_77:
			IL_79:
			return dc.ᜁ(a_, base.CharacterFormat.Font, null);
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060044EF RID: 17647 RVA: 0x00404928 File Offset: 0x00403928
		spr\u1D30 spr\u1AB8.LayoutInfo
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
						goto IL_6A;
					}
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_6A;
					case 2:
						this.CreateLayoutInfo();
						num = 0;
						continue;
					}
					if (true)
					{
					}
					if (this.ᜀ != null)
					{
						break;
					}
					num = 2;
				}
				IL_6A:
				return this.ᜀ;
			}
		}

		// Token: 0x060044F0 RID: 17648 RVA: 0x004049A8 File Offset: 0x004039A8
		void spr\u1AB8.Draw(spr\u19E0 dc, sprᦰ ltWidget)
		{
			if (true)
			{
			}
			string a_;
			for (;;)
			{
				a_ = string.Empty;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_77;
					case 1:
						a_ = this.DropDownValue;
						num = 0;
						continue;
					case 2:
						if (this.ᜁ.Count <= 0)
						{
							goto IL_79;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_77;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					break;
				}
			}
			IL_77:
			IL_79:
			dc.ᜀ(a_, base.CharacterFormat, null, ltWidget.ᜁ(), ltWidget.ᜁ().Width, new spr\u1AD7
			{
				ᜀ = DrawingTextDirection.Horizontal
			});
		}

		// Token: 0x0400361B RID: 13851
		private bool \u25D9\u007F\u0098\u009A;

		// Token: 0x0400361C RID: 13852
		private int[] \u2593\u00A3\u008B\u0083;

		// Token: 0x0400361D RID: 13853
		private new short ᜀ;

		// Token: 0x0400361E RID: 13854
		private float \u25D9\u009C\u009Cª;

		// Token: 0x0400361F RID: 13855
		private byte \u25D9\u008C\u0083\u008C;

		// Token: 0x04003620 RID: 13856
		private string[] \u25D8\u0086\u00AD\u00A0;

		// Token: 0x04003621 RID: 13857
		private new DropDownCollection ᜁ;
	}
}
