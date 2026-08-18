using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Interface;
using Spire.Layouting;

namespace Spire.Doc.Fields
{
	// Token: 0x0200051D RID: 1309
	public class CheckBoxFormField : FormField, spr\u2297
	{
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06004443 RID: 17475 RVA: 0x003FBAB4 File Offset: 0x003FAAB4
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
				return DocumentObjectType.CheckBox;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06004444 RID: 17476 RVA: 0x003FBAF4 File Offset: 0x003FAAF4
		// (set) Token: 0x06004445 RID: 17477 RVA: 0x003FBB38 File Offset: 0x003FAB38
		public int CheckBoxSize
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜀ = value;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06004446 RID: 17478 RVA: 0x003FBB7C File Offset: 0x003FAB7C
		// (set) Token: 0x06004447 RID: 17479 RVA: 0x003FBBC0 File Offset: 0x003FABC0
		public bool DefaultCheckBoxValue
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

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06004448 RID: 17480 RVA: 0x003FBC04 File Offset: 0x003FAC04
		// (set) Token: 0x06004449 RID: 17481 RVA: 0x003FBCD0 File Offset: 0x003FACD0
		public bool Checked
		{
			get
			{
				int a_ = 17;
				for (;;)
				{
					int num = base.InnerValue;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							num2 = 1;
							continue;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_AA;
							default:
								if (false)
								{
								}
								if (num != 25)
								{
									num2 = 4;
									continue;
								}
								goto IL_98;
							}
							break;
						case 2:
							goto IL_AA;
						case 3:
							switch (num)
							{
							case 0:
								return false;
							case 1:
								return true;
							default:
								if (true)
								{
								}
								num2 = 0;
								continue;
							}
							break;
						case 4:
							num2 = 2;
							continue;
						}
						break;
					}
				}
				return true;
				IL_98:
				return this.ᜁ;
				IL_AA:
				throw new ArgumentException(ClipboardData.b("≶᝸ࡺࡼཾ권戀ﲖﮘ뾞잠쪢삤쮦춨讪\udbac캮\uddb0욲킴鞶\udfb8풺좼톾ꗀ", a_));
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
				base.InnerValue = (value ? 1 : 0);
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x0600444A RID: 17482 RVA: 0x003FBD1C File Offset: 0x003FAD1C
		// (set) Token: 0x0600444B RID: 17483 RVA: 0x003FBD70 File Offset: 0x003FAD70
		public CheckBoxSizeType SizeType
		{
			get
			{
				if ((base.Params & 1024) != 1024)
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
						break;
					}
					return CheckBoxSizeType.Auto;
				}
				return CheckBoxSizeType.Exactly;
			}
			set
			{
				this.ᜂ = value;
				if (value == CheckBoxSizeType.Exactly)
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
						base.Params = (int)((short)spr\u23F8.ᜀ(base.Params, 1024, 10, 1));
						return;
					}
				}
				base.Params = (int)((short)spr\u23F8.ᜀ(base.Params, 1024, 10, 0));
			}
		}

		// Token: 0x0600444C RID: 17484 RVA: 0x003FBDF0 File Offset: 0x003FADF0
		public CheckBoxFormField(IDocument doc) : base(doc)
		{
			this.m_curFormFieldType = FormFieldType.CheckBox;
			this.m_paraItemType = ParagraphItemType.CheckBox;
			base.Type = FieldType.FieldFormCheckBox;
			base.Params = 229;
			this.ᜀ = 20;
		}

		// Token: 0x0600444D RID: 17485 RVA: 0x003FBE30 File Offset: 0x003FAE30
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
			return (CheckBoxFormField)base.CloneImpl();
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x003FBE78 File Offset: 0x003FAE78
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 13;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_AC:
				num = 5;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				goto IL_59;
			}
			for (;;)
			{
				IL_2F:
				switch (num)
				{
				case 0:
					return;
				case 1:
					this.ᜀ = (int)reader.ReadShort(ClipboardData.b("ひᵴቶ᩸ၺ㽼ၾ呂킂ﶆ", a_));
					num = 7;
					continue;
				case 2:
					if (reader.HasAttribute(ClipboardData.b("ひᵴቶ᩸ၺ㽼ၾ呂킂ﶆ", a_)))
					{
						num = 1;
						continue;
					}
					goto IL_13D;
				case 3:
					this.ᜁ = reader.ReadBoolean(ClipboardData.b("㝲ၴᅶᡸ๺ᅼ୾슀즊자璉ﲘ", a_));
					num = 4;
					continue;
				case 4:
					goto IL_8B;
				case 5:
					this.SizeType = (CheckBoxSizeType)reader.ReadEnum(ClipboardData.b("ひᵴቶ᩸ၺ㽼ၾ呂킂ﶆ\udf8aﾎ", a_), typeof(CheckBoxSizeType));
					num = 0;
					continue;
				case 6:
					goto IL_93;
				case 7:
					goto IL_13D;
				case 8:
					if (reader.HasAttribute(ClipboardData.b("㝲ၴᅶᡸ๺ᅼ୾슀즊자璉ﲘ", a_)))
					{
						num = 3;
						continue;
					}
					goto IL_8B;
				}
				goto IL_59;
				IL_8B:
				num = 6;
				continue;
				IL_13D:
				num = 8;
			}
			IL_93:
			if (reader.HasAttribute(ClipboardData.b("ひᵴቶ᩸ၺ㽼ၾ呂킂ﶆ\udf8aﾎ", a_)))
			{
				goto IL_AC;
			}
			return;
			IL_59:
			base.ReadXmlAttributes(reader);
			num = 2;
			goto IL_2F;
		}

		// Token: 0x0600444F RID: 17487 RVA: 0x003FC000 File Offset: 0x003FB000
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 14;
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
			writer.WriteValue(ClipboardData.b("㝳ṵᵷ᥹᝻㱽嬨힃", a_), this.ᜀ);
			writer.WriteValue(ClipboardData.b("㝳ṵᵷ᥹᝻㱽嬨힃\ud88b", a_), this.SizeType);
			writer.WriteValue(ClipboardData.b("び፵ṷ᭹ॻች솁캋쒑歹ﾙ", a_), this.ᜁ);
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x003FC0A0 File Offset: 0x003FB0A0
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

		// Token: 0x06004451 RID: 17489 RVA: 0x003FC0E8 File Offset: 0x003FB0E8
		SizeF spr\u2297.Measure(spr\u19E0 dc)
		{
			if (this.ᜂ != CheckBoxSizeType.Auto)
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
					return new SizeF((float)this.ᜀ, (float)this.ᜀ);
				}
			}
			float fontSize = this.CharacterFormat.FontSize;
			return new SizeF(fontSize, fontSize);
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06004452 RID: 17490 RVA: 0x003FC158 File Offset: 0x003FB158
		spr\u1D30 spr\u1AB8.LayoutInfo
		{
			get
			{
				for (;;)
				{
					IL_00:
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_60;
						case 2:
							this.CreateLayoutInfo();
							num = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							if (this.ᜀ != null)
							{
								goto IL_62;
							}
							num = 2;
							break;
						}
					}
				}
				IL_60:
				IL_62:
				return this.ᜀ;
			}
		}

		// Token: 0x06004453 RID: 17491 RVA: 0x003FC1D8 File Offset: 0x003FB1D8
		void spr\u1AB8.Draw(spr\u19E0 dc, sprᦰ ltWidget)
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
			dc.ᜀ(this, ltWidget);
		}

		// Token: 0x040035E1 RID: 13793
		private long \u2609\u0096\u0086\u0083;

		// Token: 0x040035E2 RID: 13794
		private new int ᜀ;

		// Token: 0x040035E3 RID: 13795
		private string[] \u2609\u0088\u008B\u009D;

		// Token: 0x040035E4 RID: 13796
		private bool \u2593\u00A1\u00AF\u0085;

		// Token: 0x040035E5 RID: 13797
		private new bool ᜁ;

		// Token: 0x040035E6 RID: 13798
		private CheckBoxSizeType ᜂ;
	}
}
