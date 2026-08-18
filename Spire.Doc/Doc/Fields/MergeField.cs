using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Spire.CompoundFile.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Interface;

namespace Spire.Doc.Fields
{
	// Token: 0x0200051F RID: 1311
	public class MergeField : Field, IMergeField
	{
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060044B7 RID: 17591 RVA: 0x00401B30 File Offset: 0x00400B30
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
				return DocumentObjectType.MergeField;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060044B8 RID: 17592 RVA: 0x00401B70 File Offset: 0x00400B70
		// (set) Token: 0x060044B9 RID: 17593 RVA: 0x00401BB4 File Offset: 0x00400BB4
		public string FieldName
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
				return this.m_fieldName;
			}
			set
			{
				int a_ = 14;
				switch (0)
				{
				default:
				{
					int num = 2;
					for (;;)
					{
						string[] array;
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_D8;
							default:
								if (false)
								{
								}
								array = value.Split(new char[]
								{
									':'
								}, StringSplitOptions.RemoveEmptyEntries);
								num = 6;
								continue;
							}
							break;
						case 1:
							if (true)
							{
							}
							if (this.Text == string.Empty)
							{
								num = 11;
								continue;
							}
							return;
						case 3:
							num = 1;
							continue;
						case 4:
							goto IL_AE;
						case 5:
							goto IL_D8;
						case 6:
							if (array.Length > 1)
							{
								num = 5;
								continue;
							}
							goto IL_AE;
						case 7:
							num = 8;
							continue;
						case 8:
							if (!base.Document.ᜈ)
							{
								num = 3;
								continue;
							}
							return;
						case 9:
							if (!base.Document.ᜇ)
							{
								num = 7;
								continue;
							}
							return;
						case 10:
							return;
						case 11:
						{
							char c = '«';
							char c2 = '»';
							this.Text = c + value + c2;
							num = 10;
							continue;
						}
						case 12:
							goto IL_AE;
						}
						if (value.Contains(ClipboardData.b("乳", a_)))
						{
							num = 0;
							continue;
						}
						this.m_fieldName = value;
						num = 4;
						continue;
						IL_AE:
						num = 9;
						continue;
						IL_D8:
						this.ᜃ = array[0];
						this.m_fieldName = array[1];
						num = 12;
					}
					return;
				}
				}
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060044BA RID: 17594 RVA: 0x00401DA0 File Offset: 0x00400DA0
		// (set) Token: 0x060044BB RID: 17595 RVA: 0x00401DE4 File Offset: 0x00400DE4
		public override string Text
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
				return base.Text;
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
				base.Text = value;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060044BC RID: 17596 RVA: 0x00401E28 File Offset: 0x00400E28
		// (set) Token: 0x060044BD RID: 17597 RVA: 0x00401E6C File Offset: 0x00400E6C
		public string TextBefore
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

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060044BE RID: 17598 RVA: 0x00401EB0 File Offset: 0x00400EB0
		// (set) Token: 0x060044BF RID: 17599 RVA: 0x00401EF4 File Offset: 0x00400EF4
		public string TextAfter
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

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060044C0 RID: 17600 RVA: 0x00401F38 File Offset: 0x00400F38
		// (set) Token: 0x060044C1 RID: 17601 RVA: 0x00401F7C File Offset: 0x00400F7C
		public string Prefix
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
				return this.ᜃ;
			}
			internal set
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
				this.ᜃ = value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060044C2 RID: 17602 RVA: 0x00401FC0 File Offset: 0x00400FC0
		public string NumberFormat
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
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060044C3 RID: 17603 RVA: 0x00402004 File Offset: 0x00401004
		public string DateFormat
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
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060044C4 RID: 17604 RVA: 0x00402048 File Offset: 0x00401048
		public ParagraphItemCollection TextItems
		{
			get
			{
				for (;;)
				{
					IL_00:
					int num = 2;
					for (;;)
					{
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
							switch (num)
							{
							case 0:
								this.ᜆ = new ParagraphItemCollection(base.Document);
								this.ᜆ.ᜀ(this);
								num = 1;
								continue;
							case 1:
								goto IL_81;
							}
							if (this.ᜆ != null)
							{
								goto IL_83;
							}
							num = 0;
							break;
						}
					}
				}
				IL_81:
				IL_83:
				return this.ᜆ;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060044C5 RID: 17605 RVA: 0x004020E0 File Offset: 0x004010E0
		// (set) Token: 0x060044C6 RID: 17606 RVA: 0x00402124 File Offset: 0x00401124
		internal string Domain
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
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜇ = value;
			}
		}

		// Token: 0x060044C7 RID: 17607 RVA: 0x00402168 File Offset: 0x00401168
		public MergeField(IDocument doc) : base(doc)
		{
			this.m_paraItemType = ParagraphItemType.MergeField;
			this.ᜆ = new ParagraphItemCollection(doc as Document);
			this.ᜆ.ᜀ(this);
		}

		// Token: 0x060044C8 RID: 17608 RVA: 0x004021E4 File Offset: 0x004011E4
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 15;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				this.m_fieldName = reader.ReadString(ClipboardData.b("㍴Ṷᱸ᝺᥼ㅾ", a_));
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜄ = reader.ReadString(ClipboardData.b("㭴ɶᑸ᥺᡼ൾ잀ﾊ", a_));
						num = 9;
						continue;
					case 1:
						if (reader.HasAttribute(ClipboardData.b("㑴ᅶ൸Ṻོ⭾ﮂ", a_)))
						{
							num = 3;
							continue;
						}
						goto IL_1FA;
					case 2:
						goto IL_12B;
					case 3:
						this.ᜁ = reader.ReadString(ClipboardData.b("㑴ᅶ൸Ṻོ⭾ﮂ", a_));
						num = 13;
						continue;
					case 4:
						goto IL_97;
					case 5:
						if (reader.HasAttribute(ClipboardData.b("㭴ɶᑸ᥺᡼ൾ잀ﾊ", a_)))
						{
							num = 0;
							continue;
						}
						goto IL_186;
					case 6:
						if (reader.HasAttribute(ClipboardData.b("㝴ቶὸᑺོ᩾햀ﶄ", a_)))
						{
							num = 4;
							continue;
						}
						goto IL_F7;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_97;
						default:
							if (false)
							{
							}
							this.ᜅ = reader.ReadString(ClipboardData.b("ㅴᙶ൸Ṻ㭼ၾ", a_));
							num = 2;
							continue;
						}
						break;
					case 8:
						if (reader.HasAttribute(ClipboardData.b("╴նᱸᵺᑼݾ", a_)))
						{
							num = 14;
							continue;
						}
						return;
					case 9:
						goto IL_186;
					case 10:
						return;
					case 11:
						if (reader.HasAttribute(ClipboardData.b("ㅴᙶ൸Ṻ㭼ၾ", a_)))
						{
							num = 7;
							continue;
						}
						goto IL_12B;
					case 12:
						goto IL_F7;
					case 13:
						goto IL_1FA;
					case 14:
						this.ᜃ = reader.ReadString(ClipboardData.b("╴նᱸᵺᑼݾ", a_));
						num = 10;
						continue;
					}
					break;
					IL_97:
					this.ᜀ = reader.ReadString(ClipboardData.b("㝴ቶὸᑺོ᩾햀ﶄ", a_));
					num = 12;
					continue;
					IL_F7:
					num = 1;
					continue;
					IL_12B:
					num = 8;
					continue;
					IL_186:
					num = 11;
					continue;
					IL_1FA:
					if (true)
					{
					}
					num = 5;
				}
			}
		}

		// Token: 0x060044C9 RID: 17609 RVA: 0x00402450 File Offset: 0x00401450
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 5;
			for (;;)
			{
				base.WriteXmlAttributes(writer);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (this.FieldName != string.Empty)
						{
							num = 7;
							continue;
						}
						goto IL_E1;
					case 2:
						goto IL_B4;
					case 3:
						if (true)
						{
						}
						if (this.ᜀ != string.Empty)
						{
							num = 8;
							continue;
						}
						goto IL_23C;
					case 4:
						goto IL_119;
					case 5:
						goto IL_23C;
					case 6:
						writer.WriteValue(ClipboardData.b("⽪౬᭮ᑰ㕲ᩴնᑸ᩺ॼ", a_), this.DateFormat);
						num = 4;
						continue;
					case 7:
						writer.WriteValue(ClipboardData.b("⵪Ѭ੮ᵰᝲ㭴ᙶᑸṺ", a_), this.FieldName);
						num = 10;
						continue;
					case 8:
						writer.WriteValue(ClipboardData.b("⥪࡬८ṰŲၴ⍶ᱸͺॼ", a_), this.TextBefore);
						num = 5;
						continue;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1B1;
						default:
							if (false)
							{
							}
							writer.WriteValue(ClipboardData.b("⩪୬᭮ᑰŲⅴቶŸེ", a_), this.TextAfter);
							num = 2;
							continue;
						}
						break;
					case 10:
						goto IL_E1;
					case 11:
						if (this.ᜅ != string.Empty)
						{
							num = 6;
							continue;
						}
						goto IL_119;
					case 12:
						goto IL_87;
					case 13:
						if (this.ᜄ != string.Empty)
						{
							num = 15;
							continue;
						}
						goto IL_87;
					case 14:
						writer.WriteValue(ClipboardData.b("㭪Ὤ੮ᝰᩲ൴", a_), this.ᜃ);
						goto IL_1B1;
					case 15:
						writer.WriteValue(ClipboardData.b("╪ᡬɮ፰ᙲݴㅶᙸॺၼṾ", a_), this.NumberFormat);
						num = 12;
						continue;
					case 16:
						if (this.ᜃ != string.Empty)
						{
							num = 14;
							continue;
						}
						return;
					case 17:
						if (this.ᜁ != string.Empty)
						{
							num = 9;
							continue;
						}
						goto IL_B4;
					}
					break;
					IL_87:
					num = 11;
					continue;
					IL_B4:
					num = 13;
					continue;
					IL_E1:
					num = 3;
					continue;
					IL_119:
					num = 16;
					continue;
					IL_1B1:
					num = 0;
					continue;
					IL_23C:
					num = 17;
				}
			}
		}

		// Token: 0x060044CA RID: 17610 RVA: 0x004026F0 File Offset: 0x004016F0
		protected internal override void ParseFieldCode(string fieldCode)
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
			base.Code = fieldCode;
			this.UpdateFieldCode(fieldCode);
		}

		// Token: 0x060044CB RID: 17611 RVA: 0x0040273C File Offset: 0x0040173C
		protected internal override void UpdateFieldCode(string fieldCode)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				string[] array;
				for (;;)
				{
					bool flag = true;
					string text = this.ᜁ(fieldCode);
					char[] separator = new char[]
					{
						'\\'
					};
					array = text.Split(separator);
					int num = 1;
					int num2 = 12;
					for (;;)
					{
						string text2;
						switch (num2)
						{
						case 0:
							goto IL_19D;
						case 1:
							num2 = 4;
							continue;
						case 2:
						{
							char c;
							if (c != 'm')
							{
								num2 = 11;
								continue;
							}
							goto IL_311;
						}
						case 3:
						{
							char c;
							if (c <= 'V')
							{
								num2 = 35;
								continue;
							}
							num2 = 22;
							continue;
						}
						case 4:
							goto IL_149;
						case 5:
						{
							string text3;
							text2 = MergeField.ᜂ(text3);
							char c2 = text3[0];
							char c = c2;
							num2 = 3;
							continue;
						}
						case 6:
							goto IL_19D;
						case 7:
							num2 = 28;
							continue;
						case 8:
						{
							char c;
							if (c <= 'F')
							{
								num2 = 7;
								continue;
							}
							num2 = 10;
							continue;
						}
						case 9:
							num2 = 29;
							continue;
						case 10:
						{
							char c;
							if (c != 'M')
							{
								num2 = 21;
								continue;
							}
							goto IL_311;
						}
						case 11:
							num2 = 34;
							continue;
						case 12:
							goto IL_224;
						case 13:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_19B;
							default:
							{
								if (false)
								{
								}
								char c;
								if (c != 'V')
								{
									num2 = 30;
									continue;
								}
								goto IL_311;
							}
							}
							break;
						case 14:
						{
							char c;
							if (c != 'f')
							{
								num2 = 24;
								continue;
							}
							goto IL_3F0;
						}
						case 15:
							goto IL_149;
						case 16:
							num2 = 36;
							continue;
						case 17:
							num2 = 25;
							continue;
						case 18:
							goto IL_224;
						case 19:
							goto IL_149;
						case 20:
						{
							string[] array2;
							(array2 = array)[0] = array2[0] + ClipboardData.b("乭Ɐ", a_) + array[num];
							num2 = 6;
							continue;
						}
						case 21:
							num2 = 13;
							continue;
						case 22:
						{
							char c;
							if (c <= 'f')
							{
								num2 = 16;
								continue;
							}
							num2 = 2;
							continue;
						}
						case 23:
							goto IL_19D;
						case 24:
							num2 = 19;
							continue;
						case 25:
							goto IL_19B;
						case 26:
							goto IL_19D;
						case 27:
							goto IL_246;
						case 28:
						{
							char c;
							if (c != 'B')
							{
								num2 = 9;
								continue;
							}
							goto IL_F2;
						}
						case 29:
						{
							if (true)
							{
							}
							char c;
							if (c != 'F')
							{
								num2 = 1;
								continue;
							}
							goto IL_3F0;
						}
						case 30:
							num2 = 15;
							continue;
						case 31:
						{
							if (num >= array.Length)
							{
								num2 = 27;
								continue;
							}
							string text3 = array[num];
							num2 = 37;
							continue;
						}
						case 32:
							num2 = 14;
							continue;
						case 33:
							if (flag)
							{
								num2 = 20;
								continue;
							}
							goto IL_19D;
						case 34:
						{
							char c;
							if (c != 'v')
							{
								num2 = 17;
								continue;
							}
							goto IL_311;
						}
						case 35:
							num2 = 8;
							continue;
						case 36:
						{
							char c;
							if (c != 'b')
							{
								num2 = 32;
								continue;
							}
							goto IL_F2;
						}
						case 37:
						{
							string text3;
							if (text3.Length > 0)
							{
								num2 = 5;
								continue;
							}
							goto IL_19D;
						}
						}
						break;
						IL_F2:
						this.ᜀ = text2;
						flag = false;
						num2 = 0;
						continue;
						IL_149:
						num2 = 33;
						continue;
						IL_19B:
						goto IL_149;
						IL_19D:
						num++;
						num2 = 18;
						continue;
						IL_224:
						num2 = 31;
						continue;
						IL_311:
						flag = false;
						num2 = 26;
						continue;
						IL_3F0:
						this.ᜁ = text2;
						flag = false;
						num2 = 23;
					}
				}
				IL_246:
				this.ᜅ(array[0]);
				return;
			}
			}
		}

		// Token: 0x060044CC RID: 17612 RVA: 0x00402B60 File Offset: 0x00401B60
		protected internal override string ConvertSwitchesToString()
		{
			int a_ = 8;
			string str;
			for (;;)
			{
				str = "";
				int num = 0;
				for (;;)
				{
					IL_0B:
					switch (num)
					{
					case 0:
						if (this.TextBefore != "")
						{
							num = 2;
							continue;
						}
						goto IL_8C;
					case 1:
						goto IL_87;
					case 2:
						str = ClipboardData.b("㉭ቯ剱噳", a_) + this.TextBefore + ClipboardData.b("䱭", a_);
						num = 3;
						continue;
					case 3:
						goto IL_8C;
					case 4:
						while (this.TextAfter != "")
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
								num = 5;
								goto IL_0B;
							}
						}
						goto IL_11F;
					case 5:
						str = str + ClipboardData.b("乭Ɐᑱ味呵", a_) + this.TextAfter + ClipboardData.b("䱭", a_);
						num = 1;
						continue;
					}
					break;
					IL_8C:
					num = 4;
				}
			}
			IL_87:
			IL_11F:
			return str + base.ConvertSwitchesToString();
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x00402C9C File Offset: 0x00401C9C
		internal new void ᜁ()
		{
			for (;;)
			{
				int num = 0;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_24;
					case 1:
						goto IL_24;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
						{
							if (false)
							{
							}
							if (num >= this.TextItems.Count)
							{
								num2 = 3;
								continue;
							}
							ParagraphBase paragraphBase = this.TextItems[num];
							paragraphBase.ParaItemCharFormat.ApplyBase(base.CharacterFormat.BaseFormat);
							num++;
							if (true)
							{
							}
							num2 = 0;
							continue;
						}
						}
						break;
					case 3:
						return;
					}
					break;
					IL_24:
					num2 = 2;
				}
			}
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x00402D54 File Offset: 0x00401D54
		private void ᜅ(string A_0)
		{
			int a_ = 5;
			switch (0)
			{
			default:
				for (;;)
				{
					string[] array = A_0.Trim().Split(new char[]
					{
						' '
					});
					array[0] = array[0].ToUpper();
					string text = "";
					int num = 0;
					int num2 = 4;
					for (;;)
					{
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
							goto IL_19C;
						case 1:
							goto IL_C5;
						case 2:
							A_0 = text;
							num2 = 5;
							continue;
						case 3:
							if (text.Contains(ClipboardData.b("兪", a_)))
							{
								num2 = 1;
								continue;
							}
							goto IL_1A1;
						case 4:
							goto IL_14B;
						case 5:
							if (text.StartsWith(ClipboardData.b("♪⡬㵮㙰㙲㍴㹶㱸㝺㥼", a_)))
							{
								num2 = 8;
								continue;
							}
							goto IL_1A1;
						case 6:
							if (!text.Contains(ClipboardData.b("㝪", a_)))
							{
								num2 = 0;
								continue;
							}
							goto IL_143;
						case 7:
							goto IL_14B;
						case 8:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_19C;
							default:
								if (false)
								{
								}
								num2 = 6;
								continue;
							}
							break;
						case 9:
							if (num >= array.Length)
							{
								num2 = 2;
								continue;
							}
							text = text + array[num] + ClipboardData.b("䭪", a_);
							num++;
							num2 = 7;
							continue;
						}
						break;
						IL_14B:
						num2 = 9;
						continue;
						IL_19C:
						num2 = 3;
					}
				}
				IL_C5:
				IL_143:
				this.ᜃ(A_0);
				return;
				IL_1A1:
				this.ᜄ(A_0);
				return;
			}
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x00402F0C File Offset: 0x00401F0C
		private new void ᜄ(string A_0)
		{
			Match match;
			for (;;)
			{
				match = MergeField.ᜂ.Match(A_0.Trim());
				if (match.Groups[2].Length == 0)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				goto Block_1;
			}
			this.ᜃ = "";
			this.m_fieldName = match.Groups[1].Value;
			return;
			Block_1:
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜃ = match.Groups[1].Value;
			this.m_fieldName = match.Groups[2].Value;
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x00402FC0 File Offset: 0x00401FC0
		private new void ᜃ(string A_0)
		{
			int a_ = 5;
			string text;
			for (;;)
			{
				bool flag = false;
				text = A_0.Replace(ClipboardData.b("♪⡬㵮㙰㙲㍴㹶㱸㝺㥼彾", a_), string.Empty).Trim();
				int num = 13;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_136;
					case 1:
						num = 9;
						continue;
					case 2:
						if (!A_0.Contains(ClipboardData.b("㝪", a_)))
						{
							num = 10;
							continue;
						}
						goto IL_136;
					case 3:
						num = 20;
						continue;
					case 4:
						if (!(this.ᜃ == ClipboardData.b("ⱪὬnѰͲ♴Ͷᡸॺॼ", a_)))
						{
							num = 1;
							continue;
						}
						goto IL_187;
					case 5:
						if (this.ᜃ == ClipboardData.b("≪l๮ᙰᙲ", a_))
						{
							num = 12;
							continue;
						}
						this.ᜃ = string.Empty;
						num = 0;
						continue;
					case 6:
						goto IL_141;
					case 7:
						num = 5;
						continue;
					case 8:
						text = text.Remove(0, 1);
						text = text.Remove(text.Length - 1, 1);
						num = 18;
						continue;
					case 9:
						if (!(this.ᜃ == ClipboardData.b("ⱪὬnѰͲぴ᥶ᵸ", a_)))
						{
							num = 14;
							continue;
						}
						goto IL_187;
					case 10:
						goto IL_1B5;
					case 11:
						if (!(this.ᜃ == ClipboardData.b("㽪౬൮ᵰᙲぴ᥶ᵸ", a_)))
						{
							num = 7;
							continue;
						}
						goto IL_187;
					case 12:
						goto IL_187;
					case 13:
						if (text.IndexOf(ClipboardData.b("䥪", a_)) == 0)
						{
							num = 3;
							continue;
						}
						goto IL_102;
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_141;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 19;
							continue;
						}
						break;
					case 15:
						this.ᜃ = text.Substring(0, text.IndexOf(ClipboardData.b("兪", a_)));
						num = 4;
						continue;
					case 16:
						goto IL_1E6;
					case 17:
						if (text.Contains(ClipboardData.b("兪", a_)))
						{
							num = 15;
							continue;
						}
						goto IL_136;
					case 18:
						goto IL_102;
					case 19:
						if (!(this.ᜃ == ClipboardData.b("㽪౬൮ᵰᙲ♴Ͷᡸॺॼ", a_)))
						{
							num = 22;
							continue;
						}
						goto IL_187;
					case 20:
						if (text.LastIndexOf(ClipboardData.b("䥪", a_)) == text.Length - 1)
						{
							num = 8;
							continue;
						}
						goto IL_102;
					case 21:
						text = text.Substring(text.IndexOf(ClipboardData.b("兪", a_)), text.Length);
						num = 16;
						continue;
					case 22:
						num = 11;
						continue;
					}
					break;
					IL_102:
					num = 17;
					continue;
					IL_136:
					num = 6;
					continue;
					IL_141:
					if (flag)
					{
						num = 21;
						continue;
					}
					goto IL_368;
					IL_187:
					flag = true;
					num = 2;
				}
			}
			IL_1B5:
			this.ᜄ(A_0);
			return;
			IL_1E6:
			IL_368:
			this.m_fieldName = text;
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x0040333C File Offset: 0x0040233C
		private static string ᜂ(string A_0)
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
			string text = A_0.Remove(0, 1);
			text = text.Trim();
			char[] trimChars = new char[]
			{
				'"'
			};
			return text.Trim(trimChars);
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x0040339C File Offset: 0x0040239C
		private new string ᜁ(string A_0)
		{
			int a_ = 1;
			switch (0)
			{
			default:
			{
				string text;
				for (;;)
				{
					IL_CF:
					text = A_0;
					string text2 = string.Empty;
					this.m_formattingString = string.Empty;
					List<int> list = new List<int>();
					int num = 34;
					for (;;)
					{
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
							int num2;
							int num3;
							switch (num)
							{
							case 0:
								goto IL_4B3;
							case 1:
								goto IL_32E;
							case 2:
								num = 22;
								continue;
							case 3:
								goto IL_32E;
							case 4:
								if (text2.Contains(ClipboardData.b("㭦", a_)))
								{
									num = 33;
									continue;
								}
								goto IL_312;
							case 5:
								goto IL_4FB;
							case 6:
								goto IL_32E;
							case 7:
								if (!text.Contains(ClipboardData.b("㭦䍨", a_)))
								{
									num = 28;
									continue;
								}
								text = this.ᜀ(text, ref list, ClipboardData.b("㭦䍨", a_));
								num = 35;
								continue;
							case 8:
								goto IL_4FB;
							case 9:
								text = this.ᜀ(text, ref list, ClipboardData.b("㭦", a_));
								num = 8;
								continue;
							case 10:
								if (text.Contains(ClipboardData.b("㭦൨", a_)))
								{
									num = 16;
									continue;
								}
								if (true)
								{
								}
								num = 23;
								continue;
							case 11:
								if (text.Contains(ClipboardData.b("㭦䩨", a_)))
								{
									num = 13;
									continue;
								}
								num = 31;
								continue;
							case 12:
								if (num2 != list.Count - 1)
								{
									num = 2;
									continue;
								}
								goto IL_237;
							case 13:
								text = this.ᜀ(text, ref list, ClipboardData.b("㭦䩨", a_));
								num = 6;
								continue;
							case 14:
								goto IL_4B3;
							case 15:
								goto IL_4FB;
							case 16:
								text = this.ᜀ(text, ref list, ClipboardData.b("㭦൨", a_));
								num = 15;
								continue;
							case 17:
								return text;
							case 18:
								text = this.ᜀ(text, ref list, ClipboardData.b("㭦ݨ", a_));
								num = 1;
								continue;
							case 19:
								if (text.Contains(ClipboardData.b("㭦❨", a_)))
								{
									num = 30;
									continue;
								}
								goto IL_32E;
							case 20:
								if (text.Contains(ClipboardData.b("㭦", a_)))
								{
									num = 9;
									continue;
								}
								goto IL_4FB;
							case 21:
								if (text.Contains(ClipboardData.b("㭦⥨", a_)))
								{
									num = 26;
									continue;
								}
								num = 10;
								continue;
							case 22:
								num3 = list[num2 + 1] - list[num2];
								goto IL_39A;
							case 23:
								if (text.Contains(ClipboardData.b("㭦⵨", a_)))
								{
									num = 25;
									continue;
								}
								num = 20;
								continue;
							case 24:
								if (num2 >= list.Count)
								{
									num = 17;
									continue;
								}
								num = 12;
								continue;
							case 25:
								text = this.ᜀ(text, ref list, ClipboardData.b("㭦⵨", a_));
								num = 5;
								continue;
							case 26:
								text = this.ᜀ(text, ref list, ClipboardData.b("㭦⥨", a_));
								num = 32;
								continue;
							case 27:
								num3 = A_0.Length - list[num2];
								goto IL_39A;
							case 28:
								num = 11;
								continue;
							case 29:
								goto IL_312;
							case 30:
								text = this.ᜀ(text, ref list, ClipboardData.b("㭦❨", a_));
								num = 3;
								continue;
							case 31:
								if (text.Contains(ClipboardData.b("㭦ݨ", a_)))
								{
									num = 18;
									continue;
								}
								num = 19;
								continue;
							case 32:
								goto IL_4FB;
							case 33:
								text2 = text2.Substring(0, text2.IndexOf(ClipboardData.b("㭦", a_)));
								num = 29;
								continue;
							case 34:
								goto IL_2DC;
							case 35:
								goto IL_2DC;
							}
							goto IL_CF;
							IL_2DC:
							num = 7;
							continue;
							IL_312:
							this.ᜀ(text2);
							num2++;
							num = 14;
							continue;
							IL_32E:
							num = 21;
							continue;
							IL_39A:
							int length = num3;
							text2 = A_0.Substring(list[num2], length);
							text2 = text2.Substring(1, text2.Length - 1);
							num = 4;
							continue;
							IL_4B3:
							num = 24;
							continue;
							IL_4FB:
							list.Sort();
							num2 = 0;
							num = 0;
							continue;
						}
						}
						IL_237:
						num = 27;
					}
				}
				return text;
			}
			}
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x004038E4 File Offset: 0x004028E4
		private new void ᜀ(string A_0)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				string text;
				for (;;)
				{
					for (;;)
					{
						text = string.Empty;
						int num = 21;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 27;
								continue;
							case 1:
							{
								char c;
								if (c <= '@')
								{
									num = 2;
									continue;
								}
								num = 6;
								continue;
							}
							case 2:
								num = 10;
								continue;
							case 3:
								num = 7;
								continue;
							case 4:
								num = 13;
								continue;
							case 5:
								num = 23;
								continue;
							case 6:
							{
								char c;
								if (c <= 'N')
								{
									num = 3;
									continue;
								}
								num = 25;
								continue;
							}
							case 7:
							{
								char c;
								if (c != 'D')
								{
									num = 26;
									continue;
								}
								goto IL_124;
							}
							case 8:
								return;
							case 9:
								num = 16;
								continue;
							case 10:
							{
								char c;
								if (c != '#')
								{
									num = 18;
									continue;
								}
								goto IL_195;
							}
							case 11:
							{
								char c;
								if (c != '*')
								{
									num = 4;
									continue;
								}
								num = 12;
								continue;
							}
							case 12:
							{
								string a;
								if ((a = text) != null)
								{
									num = 31;
									continue;
								}
								goto IL_1CA;
							}
							case 13:
							{
								char c;
								if (c != '@')
								{
									num = 15;
									continue;
								}
								goto IL_124;
							}
							case 14:
							{
								char c;
								if (c != 'N')
								{
									num = 20;
									continue;
								}
								goto IL_195;
							}
							case 15:
								return;
							case 16:
							{
								if (true)
								{
								}
								char c;
								if (c != 'n')
								{
									num = 8;
									continue;
								}
								goto IL_195;
							}
							case 17:
								goto IL_1CA;
							case 18:
								num = 11;
								continue;
							case 19:
							{
								text = MergeField.ᜂ(A_0);
								char c2 = A_0[0];
								char c = c2;
								num = 1;
								continue;
							}
							case 20:
								return;
							case 21:
								if (A_0.Length > 0)
								{
									num = 19;
									continue;
								}
								return;
							case 22:
							{
								string a;
								if (!(a == ClipboardData.b("⵭ᅯɱݳ", a_)))
								{
									num = 5;
									continue;
								}
								goto IL_2A5;
							}
							case 23:
							{
								string a;
								if (!(a == ClipboardData.b("⡭᥯qݳɵ㭷᭹౻", a_)))
								{
									num = 30;
									continue;
								}
								goto IL_18D;
							}
							case 24:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									num = 22;
									continue;
								}
								break;
							case 25:
							{
								char c;
								if (c != 'd')
								{
									num = 9;
									continue;
								}
								goto IL_124;
							}
							case 26:
								num = 14;
								continue;
							case 27:
							{
								string a;
								if (!(a == ClipboardData.b("≭Ὧձᅳѵ", a_)))
								{
									num = 24;
									continue;
								}
								goto IL_1C2;
							}
							case 28:
								goto IL_1F6;
							case 29:
							{
								string a;
								if (!(a == ClipboardData.b("㭭oɱᅳѵ", a_)))
								{
									num = 0;
									continue;
								}
								goto IL_12D;
							}
							case 30:
								num = 17;
								continue;
							case 31:
								num = 29;
								continue;
							}
							break;
							IL_1CA:
							this.m_formattingString = this.m_formattingString + ClipboardData.b("乭Ɐ", a_) + A_0;
							num = 28;
						}
					}
				}
				IL_124:
				this.ᜅ = text;
				return;
				IL_12D:
				this.m_textFormat = TextFormat.Uppercase;
				return;
				IL_18D:
				this.m_textFormat = TextFormat.FirstCapital;
				return;
				IL_195:
				this.ᜄ = text;
				return;
				IL_1C2:
				this.m_textFormat = TextFormat.Lowercase;
				return;
				IL_1F6:
				return;
				IL_2A5:
				this.m_textFormat = TextFormat.Titlecase;
				return;
			}
			}
		}

		// Token: 0x060044D4 RID: 17620 RVA: 0x00403CB8 File Offset: 0x00402CB8
		private new string ᜀ(string A_0, List<int> A_1)
		{
			int a_ = 7;
			for (;;)
			{
				int num = A_0.Substring(A_1[A_1.Count - 1] + 1).IndexOf(ClipboardData.b("ㅬ", a_));
				int num2 = 3;
				for (;;)
				{
					IL_0B:
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						A_0 = A_0.Substring(0, A_1[A_1.Count - 1]);
						num2 = 1;
						continue;
					case 1:
						return A_0;
					case 2:
						return A_0;
					case 3:
						while (num != -1)
						{
							A_0 = A_0.Remove(A_1[A_1.Count - 1], num);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num2 = 2;
								goto IL_0B;
							}
						}
						num2 = 0;
						continue;
					}
					break;
				}
			}
			return A_0;
		}

		// Token: 0x060044D5 RID: 17621 RVA: 0x00403DA0 File Offset: 0x00402DA0
		private new string ᜀ(string A_0, ref List<int> A_1, string A_2)
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				string result;
				for (;;)
				{
					int num = A_0.LastIndexOf(A_2);
					int num2 = 0;
					char[] array = new char[]
					{
						'b',
						'B',
						'f',
						'F',
						'm',
						'M',
						'v',
						'V'
					};
					int num3 = 11;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							num3 = 14;
							continue;
						case 1:
							goto IL_9C;
						case 2:
						{
							if (true)
							{
							}
							int num4;
							char[] array2;
							if (num4 >= array2.Length)
							{
								num3 = 7;
								continue;
							}
							char c = array2[num4];
							num3 = 12;
							continue;
						}
						case 3:
							if (A_0[num] == '\\')
							{
								num3 = 0;
								continue;
							}
							goto IL_222;
						case 4:
							if (num2 % 2 != 0)
							{
								num3 = 10;
								continue;
							}
							return A_0;
						case 5:
						{
							char[] array2 = array;
							int num4 = 0;
							num3 = 1;
							continue;
						}
						case 6:
							goto IL_9C;
						case 7:
							goto IL_1BB;
						case 8:
							if (A_2 == ClipboardData.b("㕨", a_))
							{
								num3 = 5;
								continue;
							}
							goto IL_1BB;
						case 9:
							goto IL_12B;
						case 10:
							A_1.Add(A_0.LastIndexOf(A_2));
							A_0 = this.ᜀ(A_0, A_1);
							num3 = 13;
							continue;
						case 11:
							goto IL_12B;
						case 12:
						{
							char c;
							if (A_0[A_0.LastIndexOf(ClipboardData.b("㕨", a_)) + 1] == c)
							{
								num3 = 16;
								continue;
							}
							int num4;
							num4++;
							num3 = 6;
							continue;
						}
						case 13:
							return A_0;
						case 14:
							if (num < 0)
							{
								num3 = 17;
								continue;
							}
							num--;
							num2++;
							num3 = 9;
							continue;
						case 15:
							return result;
						case 16:
							result = A_0;
							num3 = 15;
							continue;
						case 17:
							goto IL_222;
						}
						break;
						IL_9C:
						num3 = 2;
						continue;
						IL_12B:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return A_0;
						default:
							if (false)
							{
							}
							num3 = 3;
							continue;
						}
						IL_1BB:
						num3 = 4;
						continue;
						IL_222:
						num3 = 8;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x060044D6 RID: 17622 RVA: 0x00404008 File Offset: 0x00403008
		// Note: this type is marked as 'beforefieldinit'.
		static MergeField()
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
			MergeField.ᜂ = new Regex(ClipboardData.b("㭵㵷⡹㭻㭽왿쮁솃쪅첇횉ﾋꖍ늏궑벓축욗ꂙ뺛쎝讟计麣馥肧貭颱鶳钵螷", a_));
		}

		// Token: 0x04003609 RID: 13833
		private float \u2609\u0080\u008E\u0096;

		// Token: 0x0400360A RID: 13834
		protected string m_fieldName = "";

		// Token: 0x0400360B RID: 13835
		private int[] \u25D9\u008B\u00A0\u00A7;

		// Token: 0x0400360C RID: 13836
		private new string ᜀ = "";

		// Token: 0x0400360D RID: 13837
		private bool \u2609\u008F\u00A6\u008F;

		// Token: 0x0400360E RID: 13838
		private new string ᜁ = "";

		// Token: 0x0400360F RID: 13839
		private static Regex ᜂ;

		// Token: 0x04003610 RID: 13840
		private new string ᜃ = "";

		// Token: 0x04003611 RID: 13841
		private new string ᜄ = "";

		// Token: 0x04003612 RID: 13842
		private string ᜅ = "";

		// Token: 0x04003613 RID: 13843
		private ParagraphItemCollection ᜆ;

		// Token: 0x04003614 RID: 13844
		private long[] \u2593\u008F\u0080\u007F;

		// Token: 0x04003615 RID: 13845
		private string ᜇ;
	}
}
