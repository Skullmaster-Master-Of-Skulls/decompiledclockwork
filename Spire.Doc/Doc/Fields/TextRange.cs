using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;
using Spire.Layouting;

namespace Spire.Doc.Fields
{
	// Token: 0x0200050E RID: 1294
	public class TextRange : ParagraphBase, ITextRange, spr\u1C7D
	{
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06004271 RID: 17009 RVA: 0x003E6798 File Offset: 0x003E5798
		// (set) Token: 0x06004272 RID: 17010 RVA: 0x003E67DC File Offset: 0x003E57DC
		internal Paragraph OwnerEmptyParagraph
		{
			[CompilerGenerated]
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
			[CompilerGenerated]
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

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06004273 RID: 17011 RVA: 0x003E6820 File Offset: 0x003E5820
		public override DocumentObjectType DocumentObjectType
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
				return DocumentObjectType.TextRange;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06004274 RID: 17012 RVA: 0x003E6860 File Offset: 0x003E5860
		// (set) Token: 0x06004275 RID: 17013 RVA: 0x003E69D0 File Offset: 0x003E59D0
		public virtual string Text
		{
			get
			{
				int num = 10;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_13E;
					case 1:
						if (!(this.TextToSplit == string.Empty))
						{
							num = 2;
							continue;
						}
						goto IL_C0;
					case 2:
						num = 5;
						continue;
					case 3:
					{
						string text = base.OwnerParagraph.Text;
						this.ᜁ = text.Substring(this.StartPos, this.ᜀ);
						num = 6;
						continue;
					}
					case 4:
						if (!this.IsTextToSplitAssignedInSecondLayouting)
						{
							num = 8;
							continue;
						}
						goto IL_13E;
					case 5:
						if (!spr\u1A69.ᜧ)
						{
							if (true)
							{
							}
							num = 7;
							continue;
						}
						goto IL_13E;
					case 6:
						goto IL_79;
					case 7:
						num = 4;
						continue;
					case 8:
						goto IL_C0;
					case 9:
						IL_AB:
						if (base.OwnerParagraph != null)
						{
							num = 3;
							continue;
						}
						goto IL_79;
					case 11:
						num = 9;
						continue;
					}
					if (!base.ItemDetached)
					{
						num = 11;
						continue;
					}
					IL_79:
					num = 1;
					continue;
					IL_13E:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AB;
					default:
						goto IL_154;
					}
					IL_C0:
					this.TextToSplit = this.ᜁ;
					this.IsTextToSplitAssignedInSecondLayouting = true;
					num = 0;
				}
				IL_154:
				if (false)
				{
				}
				return this.ᜁ;
			}
			set
			{
				for (;;)
				{
					for (;;)
					{
						this.ᜅ = null;
						int num = 6;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (value != null)
								{
									num = 1;
									continue;
								}
								goto IL_76;
							case 1:
								num = 9;
								continue;
							case 2:
								goto IL_8D;
							case 3:
								base.OwnerParagraph.ᜀ(this, value);
								this.ᜀ = value.Length;
								num = 8;
								continue;
							case 4:
								goto IL_76;
							case 5:
								if (base.OwnerParagraph == null)
								{
									if (true)
									{
									}
									num = 10;
									continue;
								}
								num = 0;
								continue;
							case 6:
								if (base.ItemDetached)
								{
									goto IL_C3;
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
									num = 7;
									continue;
								}
								break;
							case 7:
								num = 5;
								continue;
							case 8:
								goto IL_76;
							case 9:
								if (value != this.Text)
								{
									num = 3;
									continue;
								}
								goto IL_76;
							case 10:
								goto IL_C3;
							}
							break;
							IL_76:
							this.TextToSplit = this.ᜁ;
							num = 2;
							continue;
							IL_C3:
							this.ᜁ = value;
							num = 4;
						}
					}
				}
				IL_8D:
				this.ᜂ = base.Document.ᜇ;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06004276 RID: 17014 RVA: 0x003E6B40 File Offset: 0x003E5B40
		// (set) Token: 0x06004277 RID: 17015 RVA: 0x003E6B84 File Offset: 0x003E5B84
		internal bool IsTextToSplitAssignedInSecondLayouting
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
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							this.ᜄ = value;
							num = 0;
							continue;
						}
						break;
					}
					IL_1C:
					if (true)
					{
					}
					if (!spr\u1A69.ᜧ)
					{
						num = 1;
						continue;
					}
					break;
					goto IL_1C;
				}
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06004278 RID: 17016 RVA: 0x003E6C00 File Offset: 0x003E5C00
		// (set) Token: 0x06004279 RID: 17017 RVA: 0x003E6C44 File Offset: 0x003E5C44
		internal string TextToSplit
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜃ = value;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600427A RID: 17018 RVA: 0x003E6C88 File Offset: 0x003E5C88
		// (set) Token: 0x0600427B RID: 17019 RVA: 0x003E6CCC File Offset: 0x003E5CCC
		public CharacterFormat CharacterFormat
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
				return this.m_charFormat;
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
				this.m_charFormat = value;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600427C RID: 17020 RVA: 0x003E6D10 File Offset: 0x003E5D10
		// (set) Token: 0x0600427D RID: 17021 RVA: 0x003E6D68 File Offset: 0x003E5D68
		internal int TextLength
		{
			get
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
					if (!base.ItemDetached)
					{
						return this.ᜀ;
					}
					break;
				}
				return this.ᜁ.Length;
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

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600427E RID: 17022 RVA: 0x003E6DAC File Offset: 0x003E5DAC
		internal override int EndPos
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
				return base.EndPos + this.ᜀ;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600427F RID: 17023 RVA: 0x003E6DF4 File Offset: 0x003E5DF4
		// (set) Token: 0x06004280 RID: 17024 RVA: 0x003E6EAC File Offset: 0x003E5EAC
		internal new int StartPos
		{
			get
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A0;
					case 1:
						num = 2;
						continue;
					case 2:
						if (!(base.Owner.Owner is sprờ))
						{
							goto IL_A2;
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
							num = 0;
							continue;
						}
						break;
					}
					IL_20:
					if (base.Owner is spr\u1AD2)
					{
						num = 1;
						continue;
					}
					goto IL_A2;
					goto IL_20;
				}
				IL_A0:
				if (true)
				{
				}
				return (base.Owner.Owner as ParagraphBase).StartPos;
				IL_A2:
				return base.StartPos;
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
				base.StartPos = value;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06004281 RID: 17025 RVA: 0x003E6EF0 File Offset: 0x003E5EF0
		// (set) Token: 0x06004282 RID: 17026 RVA: 0x003E6F34 File Offset: 0x003E5F34
		internal bool SafeText
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

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06004283 RID: 17027 RVA: 0x003E6F78 File Offset: 0x003E5F78
		private bool IsSplitable
		{
			get
			{
				switch (0)
				{
				default:
				{
					int num = 11;
					for (;;)
					{
						string text;
						int num2;
						string text2;
						switch (num)
						{
						case 0:
							if (text.Length == 0)
							{
								num = 16;
								continue;
							}
							goto IL_184;
						case 1:
							this.ᜅ = new bool?(false);
							num = 5;
							continue;
						case 2:
							goto IL_184;
						case 3:
							goto IL_7A;
						case 4:
							goto IL_144;
						case 5:
							goto IL_182;
						case 6:
							if (this.ᜅ == null)
							{
								num = 1;
								continue;
							}
							goto IL_1EF;
						case 7:
							goto IL_10E;
						case 8:
							goto IL_144;
						case 9:
							if (text != null)
							{
								num = 13;
								continue;
							}
							goto IL_1D5;
						case 10:
							if (num2 >= text2.Length)
							{
								num = 12;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_83;
							default:
							{
								if (false)
								{
								}
								char a_ = text2[num2];
								num = 15;
								continue;
							}
							}
							break;
						case 12:
							goto IL_10E;
						case 13:
							num = 0;
							continue;
						case 14:
							this.ᜅ = new bool?(true);
							num = 7;
							continue;
						case 15:
						{
							char a_;
							if (sprᩁ.ᜀ(a_))
							{
								num = 14;
								continue;
							}
							num2++;
							goto IL_83;
						}
						case 16:
							goto IL_1D5;
						}
						if (this.ᜅ != null)
						{
							num = 3;
							continue;
						}
						text = this.Text;
						num = 9;
						continue;
						IL_83:
						num = 8;
						continue;
						IL_10E:
						num = 6;
						continue;
						IL_144:
						num = 10;
						continue;
						IL_184:
						text2 = text;
						num2 = 0;
						num = 4;
						continue;
						IL_1D5:
						this.ᜅ = new bool?(true);
						num = 2;
					}
					IL_7A:
					return this.ᜅ.Value;
					IL_182:
					IL_1EF:
					if (true)
					{
					}
					return this.ᜅ.Value;
				}
				}
			}
		}

		// Token: 0x06004284 RID: 17028 RVA: 0x003E7188 File Offset: 0x003E6188
		public TextRange(IDocument doc) : base((Document)doc)
		{
			this.m_charFormat = new CharacterFormat(base.Document);
			this.m_charFormat.ᜀ(this);
		}

		// Token: 0x06004285 RID: 17029 RVA: 0x003E71D4 File Offset: 0x003E61D4
		internal override void Attach(Paragraph paragraph, int itemPos)
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
			this.ᜀ = 0;
			base.Attach(paragraph, itemPos);
			this.Text = this.ᜁ;
		}

		// Token: 0x06004286 RID: 17030 RVA: 0x003E722C File Offset: 0x003E622C
		internal override void Detach()
		{
			for (;;)
			{
				base.Detach();
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜁ = this.Text;
						base.OwnerParagraph.ᜀ(this, string.Empty);
						if (true)
						{
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
							num = 1;
							continue;
						}
						break;
					case 1:
						return;
					case 2:
						if (base.OwnerParagraph != null)
						{
							num = 0;
							continue;
						}
						return;
					}
					break;
				}
			}
		}

		// Token: 0x06004287 RID: 17031 RVA: 0x003E72C8 File Offset: 0x003E62C8
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
			TextRange textRange = (TextRange)base.CloneImpl();
			textRange.ᜁ = this.Text;
			return textRange;
		}

		// Token: 0x06004288 RID: 17032 RVA: 0x003E731C File Offset: 0x003E631C
		internal override void CloneRelationsTo(Document doc, OwnerHolder nextOwner)
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
			base.CloneRelationsTo(doc, nextOwner);
		}

		// Token: 0x06004289 RID: 17033 RVA: 0x003E7360 File Offset: 0x003E6360
		public void ApplyCharacterFormat(CharacterFormat charFormat)
		{
			int num = 0;
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
					case 1:
						return;
					case 2:
						if (true)
						{
						}
						this.m_charFormat = (charFormat.ឱ() as CharacterFormat);
						num = 1;
						continue;
					}
					if (charFormat == null)
					{
						return;
					}
					break;
				}
				num = 2;
			}
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x003E73E0 File Offset: 0x003E63E0
		internal void \u171A()
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
			this.ᜁ();
			this.ᜀ();
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x003E7428 File Offset: 0x003E6428
		private new void ᜁ()
		{
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
					for (;;)
					{
						string text = string.Empty;
						int num = 5;
						for (;;)
						{
							TextRange textRange;
							int num3;
							switch (num)
							{
							case 0:
							{
								int num2;
								if (num2 > 0)
								{
									num = 1;
									continue;
								}
								if (true)
								{
								}
								num = 3;
								continue;
							}
							case 1:
							{
								int num2;
								textRange.Text = text.Substring(num2);
								this.Text = text.Substring(0, num2);
								num = 7;
								continue;
							}
							case 2:
							{
								int num2 = this.Text.IndexOf(spr\u20E8.\u1714);
								text = this.Text;
								num3 = base.OwnerParagraph.Items.IndexOf(this);
								string text2 = text.Substring(num2 + 1);
								textRange = (base.Clone() as TextRange);
								num = 0;
								continue;
							}
							case 3:
							{
								string text2;
								if (text2 != string.Empty)
								{
									num = 9;
									continue;
								}
								goto IL_112;
							}
							case 4:
								num = 6;
								continue;
							case 5:
								if (this.Text != spr\u20E8.\u1714)
								{
									num = 4;
									continue;
								}
								return;
							case 6:
								if (this.Text.Contains(spr\u20E8.\u1714))
								{
									num = 2;
									continue;
								}
								return;
							case 7:
								goto IL_112;
							case 8:
								return;
							case 9:
							{
								string text2;
								textRange.Text = text2;
								this.Text = spr\u20E8.\u1714;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_00;
								default:
									if (false)
									{
									}
									num = 10;
									continue;
								}
								break;
							}
							case 10:
								goto IL_112;
							}
							break;
							IL_112:
							base.OwnerParagraph.Items.Insert(num3 + 1, textRange);
							num = 8;
						}
					}
					break;
				}
			}
		}

		// Token: 0x0600428C RID: 17036 RVA: 0x003E7604 File Offset: 0x003E6604
		private new void ᜀ()
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_295:
				num = 0;
				break;
			case 1:
				goto IL_20;
			default:
				goto IL_20;
			}
			string text2;
			for (;;)
			{
				IL_36:
				TextRange textRange;
				int num3;
				Paragraph paragraph;
				switch (num)
				{
				case 0:
				{
					string text;
					if (text != string.Empty)
					{
						num = 14;
						continue;
					}
					goto IL_F9;
				}
				case 1:
				{
					int num2;
					textRange.Text = text2.Substring(num2 + 1);
					this.Text = text2.Substring(0, num2);
					num = 3;
					continue;
				}
				case 2:
					goto IL_CD;
				case 3:
					goto IL_F9;
				case 4:
					if (text2.Contains(spr\u20E8.\u171F))
					{
						num = 11;
						continue;
					}
					return;
				case 5:
					if (text2 == spr\u20E8.\u171F)
					{
						num = 9;
						continue;
					}
					goto IL_CD;
				case 6:
					if (num3 + 1 >= base.OwnerParagraph.Items.Count)
					{
						num = 13;
						continue;
					}
					paragraph.Items.Add(base.OwnerParagraph.Items[num3 + 1]);
					num = 7;
					continue;
				case 7:
					goto IL_23F;
				case 8:
					goto IL_23F;
				case 9:
					this.Text = string.Empty;
					textRange.Text = string.Empty;
					num = 2;
					continue;
				case 10:
					goto IL_F9;
				case 11:
				{
					int num2 = text2.IndexOf(spr\u20E8.\u171F);
					string text = text2.Substring(num2 + 1);
					textRange = (base.Clone() as TextRange);
					num = 12;
					continue;
				}
				case 12:
				{
					int num2;
					if (num2 > 0)
					{
						num = 1;
						continue;
					}
					goto IL_295;
				}
				case 13:
					return;
				case 14:
				{
					string text;
					textRange.Text = text;
					this.Text = string.Empty;
					num = 10;
					continue;
				}
				}
				goto IL_79;
				IL_CD:
				if (true)
				{
				}
				num3 = base.OwnerParagraph.Items.IndexOf(this);
				num = 8;
				continue;
				IL_F9:
				paragraph = (base.OwnerParagraph.Clone() as Paragraph);
				paragraph.\u170D();
				int num4 = base.OwnerParagraph.ឯ();
				base.OwnerParagraph.OwnerTextBody.Items.Insert(num4 + 1, paragraph);
				paragraph.Items.Add(textRange);
				num = 5;
				continue;
				IL_23F:
				num = 6;
			}
			return;
			IL_20:
			if (false)
			{
			}
			switch (0)
			{
			}
			IL_79:
			text2 = string.Empty;
			text2 = this.Text.Replace(spr\u20E8.ᜉ, spr\u20E8.\u171F);
			text2 = text2.Replace(spr\u20E8.ᜏ, '\r');
			num = 4;
			goto IL_36;
		}

		// Token: 0x0600428D RID: 17037 RVA: 0x003E78D0 File Offset: 0x003E68D0
		protected override void InitXDLSHolder()
		{
			int a_ = 18;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.XDLSHolder.AddElement(ClipboardData.b("᭷ቹᵻ౽慎꞉ﾑ", a_), this.m_charFormat);
		}

		// Token: 0x0600428E RID: 17038 RVA: 0x003E7934 File Offset: 0x003E6934
		protected override void WriteXmlContent(IXDLSContentWriter writer)
		{
			int a_ = 12;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.WriteXmlContent(writer);
			writer.WriteChildStringElement(ClipboardData.b("ٱᅳ๵౷", a_), this.Text);
		}

		// Token: 0x0600428F RID: 17039 RVA: 0x003E799C File Offset: 0x003E699C
		protected override bool ReadXmlContent(IXDLSContentReader reader)
		{
			int a_ = 4;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.Text == "")
					{
						num = 4;
						continue;
					}
					goto IL_F8;
				case 1:
					if (base.OwnerParagraph != null)
					{
						num = 7;
						continue;
					}
					goto IL_7F;
				case 3:
					goto IL_7F;
				case 4:
					reader.InnerReader.Read();
					num = 6;
					continue;
				case 5:
					num = 1;
					continue;
				case 6:
					goto IL_7D;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5F;
					default:
						if (false)
						{
						}
						this.StartPos = base.OwnerParagraph.Text.Length;
						num = 3;
						continue;
					}
					break;
				}
				goto IL_39;
				IL_5F:
				num = 5;
				continue;
				IL_39:
				if (true)
				{
				}
				if (reader.TagName == ClipboardData.b("ṩ५᙭ѯ", a_))
				{
					goto IL_5F;
				}
				return false;
				IL_7F:
				this.Text = reader.ReadChildStringContent();
				num = 0;
			}
			IL_7D:
			IL_F8:
			this.ᜂ = true;
			return true;
		}

		// Token: 0x06004290 RID: 17040 RVA: 0x003E7AD4 File Offset: 0x003E6AD4
		protected override void CreateLayoutInfo()
		{
			int a_ = 3;
			for (;;)
			{
				this.\u171A();
				int num = 65;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						Field field;
						if (field != null)
						{
							num = 59;
							continue;
						}
						goto IL_175;
					}
					case 1:
						if ((base.OwnerParagraph.OwnerTextBody as TableCell).CellFormat.TextDirection != TextDirection.LeftToRight)
						{
							num = 44;
							continue;
						}
						goto IL_793;
					case 2:
						num = 63;
						continue;
					case 3:
					{
						Paragraph paragraph;
						if (paragraph.IsInCell)
						{
							num = 18;
							continue;
						}
						goto IL_1CC;
					}
					case 4:
						goto IL_175;
					case 5:
					{
						Field field;
						if (!new Hyperlink(field).BookmarkName.StartsWith(ClipboardData.b("㙨㽪ɬ౮", a_)))
						{
							num = 8;
							continue;
						}
						goto IL_175;
					}
					case 6:
						if ((base.NextSibling as Break).BreakType == BreakType.LineBreak)
						{
							num = 38;
							continue;
						}
						goto IL_532;
					case 7:
						this.ᜀ.ᜋ().ᜀ((double)this.CharacterFormat.Position);
						num = 14;
						continue;
					case 8:
						goto IL_274;
					case 9:
					{
						Paragraph paragraph;
						if (paragraph != null)
						{
							num = 13;
							continue;
						}
						goto IL_1CC;
					}
					case 10:
					{
						Paragraph paragraph;
						if ((paragraph.OwnerTextBody as TableCell).CellFormat.TextDirection != TextDirection.LeftToRight)
						{
							num = 56;
							continue;
						}
						goto IL_1CC;
					}
					case 11:
						if (this.CharacterFormat.Position > 0f)
						{
							num = 7;
							continue;
						}
						goto IL_480;
					case 12:
					{
						Paragraph paragraph = this.CharacterFormat.BaseFormat.OwnerBase as Paragraph;
						num = 31;
						continue;
					}
					case 13:
						num = 3;
						continue;
					case 14:
						goto IL_480;
					case 15:
						num = 1;
						continue;
					case 16:
						num = 61;
						continue;
					case 17:
						num = 37;
						continue;
					case 18:
						num = 10;
						continue;
					case 19:
						num = 22;
						continue;
					case 20:
						if (base.OwnerParagraph != null)
						{
							num = 49;
							continue;
						}
						goto IL_793;
					case 21:
					{
						Paragraph paragraph = null;
						num = 52;
						continue;
					}
					case 22:
						if (base.NextSibling is Break)
						{
							num = 41;
							continue;
						}
						goto IL_532;
					case 23:
						if (base.NextSibling is FieldMark)
						{
							num = 60;
							continue;
						}
						goto IL_349;
					case 24:
					{
						Paragraph paragraph = base.Owner.Owner.Owner as Paragraph;
						num = 64;
						continue;
					}
					case 25:
					{
						Field field;
						if (field.Type != FieldType.FieldPage)
						{
							num = 16;
							continue;
						}
						goto IL_274;
					}
					case 26:
						goto IL_2A7;
					case 27:
						num = 5;
						continue;
					case 28:
						if (base.PreviousSibling.DocumentObjectType == DocumentObjectType.FieldMark)
						{
							num = 47;
							continue;
						}
						goto IL_175;
					case 29:
						if (base.PreviousSibling == null)
						{
							num = 39;
							continue;
						}
						goto IL_349;
					case 30:
						if (base.NextSibling != null)
						{
							num = 19;
							continue;
						}
						goto IL_532;
					case 31:
						goto IL_772;
					case 32:
						if (base.OwnerParagraph == null)
						{
							num = 21;
							continue;
						}
						goto IL_24E;
					case 33:
						goto IL_24E;
					case 34:
						goto IL_562;
					case 35:
						goto IL_2DC;
					case 36:
						if (this.CharacterFormat.Position < 0f)
						{
							num = 48;
							continue;
						}
						goto IL_562;
					case 37:
					{
						Field field;
						if (new Hyperlink(field).BookmarkName != null)
						{
							num = 27;
							continue;
						}
						goto IL_274;
					}
					case 38:
						this.ᜀ.ᜃ(true);
						num = 26;
						continue;
					case 39:
						num = 23;
						continue;
					case 40:
						if (base.PreviousSibling != null)
						{
							num = 54;
							continue;
						}
						goto IL_175;
					case 41:
						num = 6;
						continue;
					case 42:
						if (this.CharacterFormat.BaseFormat != null)
						{
							num = 51;
							continue;
						}
						goto IL_151;
					case 43:
						if (this.CharacterFormat.BaseFormat.OwnerBase != null)
						{
							num = 12;
							continue;
						}
						goto IL_151;
					case 44:
						this.ᜀ.ᜆ(true);
						num = 35;
						continue;
					case 45:
						this.ᜀ.ᜁ(true);
						num = 58;
						continue;
					case 46:
						this.ᜀ.ᜁ(true);
						num = 33;
						continue;
					case 47:
					{
						Field field = base.PreviousSibling.PreviousSibling as Field;
						num = 0;
						continue;
					}
					case 48:
						this.ᜀ.ᜋ().ᜁ((double)(-(double)this.CharacterFormat.Position));
						num = 34;
						continue;
					case 49:
						num = 53;
						continue;
					case 50:
						goto IL_1CC;
					case 51:
						num = 43;
						continue;
					case 52:
						if (base.Owner is spr\u1AD2)
						{
							num = 24;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2A7;
						default:
							if (false)
							{
							}
							num = 42;
							continue;
						}
						break;
					case 53:
						if (base.OwnerParagraph.IsInCell)
						{
							num = 15;
							continue;
						}
						goto IL_793;
					case 54:
						num = 28;
						continue;
					case 55:
						goto IL_772;
					case 56:
						this.ᜀ.ᜆ(true);
						num = 50;
						continue;
					case 57:
					{
						FieldMark fieldMark;
						if (fieldMark.Type == FieldMarkType.FieldEnd)
						{
							num = 45;
							continue;
						}
						goto IL_349;
					}
					case 58:
						goto IL_349;
					case 59:
						num = 25;
						continue;
					case 60:
					{
						FieldMark fieldMark = new FieldMark(base.NextSibling as FieldMark, base.Document);
						num = 57;
						continue;
					}
					case 61:
					{
						Field field;
						if (field.Type == FieldType.FieldHyperlink)
						{
							num = 17;
							continue;
						}
						goto IL_175;
					}
					case 62:
					{
						Paragraph paragraph;
						if (paragraph != null)
						{
							num = 2;
							continue;
						}
						goto IL_24E;
					}
					case 63:
					{
						Paragraph paragraph;
						if (paragraph.SectionEndMark)
						{
							num = 46;
							continue;
						}
						goto IL_24E;
					}
					case 64:
						goto IL_772;
					case 65:
						this.ᜀ = ((this.Text == ClipboardData.b("恨", a_)) ? new TextRange.ᜀ(this) : new spr\u22A8(ChildrenLayoutDirection.Horizontal));
						num = 29;
						continue;
					}
					break;
					IL_151:
					this.ᜀ.ᜁ(true);
					if (true)
					{
					}
					num = 55;
					continue;
					IL_175:
					num = 32;
					continue;
					IL_1CC:
					num = 62;
					continue;
					IL_24E:
					num = 20;
					continue;
					IL_274:
					this.ᜀ.ᜁ(true);
					num = 4;
					continue;
					IL_349:
					num = 30;
					continue;
					IL_480:
					num = 36;
					continue;
					IL_532:
					num = 11;
					continue;
					IL_2A7:
					goto IL_532;
					IL_562:
					num = 40;
					continue;
					IL_772:
					num = 9;
				}
			}
			IL_2DC:
			IL_793:
			bool hidden = this.CharacterFormat.Hidden;
		}

		// Token: 0x06004291 RID: 17041 RVA: 0x003E8280 File Offset: 0x003E7280
		void spr\u1AB8.Draw(spr\u19E0 dc, sprᦰ ltWidget)
		{
			int num = 2;
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
						if (true)
						{
						}
						num = 1;
						continue;
					case 1:
						goto IL_78;
					case 3:
						goto IL_68;
					}
					if (ltWidget.ᜅ() != null)
					{
						num = 3;
						continue;
					}
					break;
				}
				num = 0;
			}
			IL_68:
			string text = ltWidget.ᜅ();
			goto IL_80;
			IL_78:
			text = this.Text;
			IL_80:
			string a_ = text;
			((spr\u1C7D)this).ᜀ(dc, ltWidget, a_);
		}

		// Token: 0x06004292 RID: 17042 RVA: 0x003E8318 File Offset: 0x003E7318
		void spr\u1C7D.Draw(spr\u19E0 dc, sprᦰ ltWidget, string text)
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
			dc.ᜀ(this, ltWidget, text);
			this.DrawImpl(dc, ltWidget);
		}

		// Token: 0x06004293 RID: 17043 RVA: 0x003E8364 File Offset: 0x003E7364
		SizeF spr\u1C30.Measure(spr\u19E0 dc, string text)
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
			return dc.ᜀ(this, text);
		}

		// Token: 0x06004294 RID: 17044 RVA: 0x003E83A8 File Offset: 0x003E73A8
		double spr\u1C7D.GetTextAscent(spr\u19E0 dc)
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
			return (double)dc.ᜀ(this);
		}

		// Token: 0x06004295 RID: 17045 RVA: 0x003E83EC File Offset: 0x003E73EC
		int spr\u1C7D.OffsetToIndex(spr\u19E0 dc, double offset, string text, string textSplit, float clientWidth, float clientActiveAreaWidth)
		{
			switch (0)
			{
			default:
			{
				float a_;
				bool flag2;
				for (;;)
				{
					if (true)
					{
					}
					a_ = this.ᜀ(dc, clientWidth);
					sprℐ sprℐ = this.ᜀ as sprℐ;
					int num = 3;
					for (;;)
					{
						bool flag;
						switch (num)
						{
						case 0:
							num = 6;
							continue;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_A2;
							default:
								if (false)
								{
								}
								if (base.OwnerParagraph != null)
								{
									num = 0;
									continue;
								}
								num = 7;
								continue;
							}
							break;
						case 2:
							goto IL_A2;
						case 3:
							if (sprℐ == null)
							{
								num = 2;
								continue;
							}
							num = 4;
							continue;
						case 4:
							flag = sprℐ.ᜪ();
							goto IL_BB;
						case 5:
							flag = true;
							goto IL_BB;
						case 6:
							goto IL_75;
						case 7:
							goto IL_8B;
						}
						break;
						IL_A2:
						num = 5;
						continue;
						IL_BB:
						flag2 = flag;
						num = 1;
					}
				}
				IL_75:
				bool flag3 = base.OwnerParagraph.IsInCell;
				goto IL_FD;
				IL_8B:
				flag3 = false;
				IL_FD:
				bool a_2 = flag3;
				return dc.ᜀ(text, this, textSplit, offset, !flag2, a_2, a_, clientActiveAreaWidth);
			}
			}
		}

		// Token: 0x06004296 RID: 17046 RVA: 0x003E850C File Offset: 0x003E750C
		internal new float ᜀ(spr\u19E0 A_0, float A_1)
		{
			switch (0)
			{
			default:
			{
				float result;
				for (;;)
				{
					result = 0f;
					int num = 8;
					for (;;)
					{
						sprℐ sprℐ;
						DocumentObject owner;
						Paragraph paragraph;
						switch (num)
						{
						case 0:
							if (base.Owner is spr\u1AD2)
							{
								num = 21;
								continue;
							}
							goto IL_1AA;
						case 1:
							if (sprℐ.ᜣ())
							{
								num = 12;
								continue;
							}
							result = A_1 - (float)(sprℐ.ᜰ().ᜃ() + sprℐ.ᜰ().ᜂ() + (double)sprℐ.ᜢ());
							num = 11;
							continue;
						case 2:
							if (owner is Section)
							{
								num = 19;
								continue;
							}
							num = 7;
							continue;
						case 3:
							goto IL_29E;
						case 4:
							return result;
						case 5:
							owner = owner.Owner;
							num = 18;
							continue;
						case 6:
							goto IL_1F4;
						case 7:
							if (owner is Table)
							{
								num = 16;
								continue;
							}
							return result;
						case 8:
							if (base.Owner != null)
							{
								num = 13;
								continue;
							}
							return result;
						case 9:
							goto IL_2AA;
						case 10:
							num = 14;
							continue;
						case 11:
							return result;
						case 12:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2AA;
							default:
								if (false)
								{
								}
								result = A_1 - (float)(sprℐ.ᜰ().ᜃ() + sprℐ.ᜰ().ᜂ() + (double)sprℐ.\u171B() + (double)sprℐ.ᜢ());
								num = 4;
								continue;
							}
							break;
						case 13:
							owner = base.Owner;
							num = 3;
							continue;
						case 14:
							if (owner.Owner != null)
							{
								num = 5;
								continue;
							}
							goto IL_1F4;
						case 15:
							if (!(owner is Table))
							{
								if (true)
								{
								}
								num = 10;
								continue;
							}
							goto IL_1F4;
						case 16:
							result = A_0.ᜂ(this);
							num = 20;
							continue;
						case 17:
							goto IL_1AA;
						case 18:
							goto IL_29E;
						case 19:
							paragraph = base.OwnerParagraph;
							num = 0;
							continue;
						case 20:
							return result;
						case 21:
							paragraph = (base.Owner.Owner.Owner as Paragraph);
							num = 17;
							continue;
						}
						break;
						IL_2AA:
						if (owner is Section)
						{
							num = 6;
							continue;
						}
						num = 15;
						continue;
						IL_1AA:
						sprℐ = (((spr\u1AB8)paragraph).ᜀ() as sprℐ);
						num = 1;
						continue;
						IL_1F4:
						num = 2;
						continue;
						IL_29E:
						num = 9;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x003E87E0 File Offset: 0x003E77E0
		spr\u17BA[] spr\u17BA.SplitBySize(spr\u19E0 dc, SizeF offset, float clientWidth, float clientActiveAreaWidth)
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
			return spr\u208E.ᜀ(dc, (double)offset.Width, this, null, clientWidth, clientActiveAreaWidth);
		}

		// Token: 0x06004298 RID: 17048 RVA: 0x003E8830 File Offset: 0x003E7830
		SizeF spr\u2297.Measure(spr\u19E0 dc)
		{
			int a_ = 2;
			switch (0)
			{
			default:
			{
				string a_2;
				for (;;)
				{
					a_2 = this.Text;
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (base.Owner == null)
							{
								num = 8;
								continue;
							}
							goto IL_21F;
						case 1:
							num = 3;
							continue;
						case 2:
							if (true)
							{
							}
							if (!(this.Text.Trim() != string.Empty))
							{
								goto IL_8F;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_B4;
							default:
								if (false)
								{
								}
								num = 14;
								continue;
							}
							break;
						case 3:
							if (base.ឯ() == base.OwnerParagraph.Items.Count - 1)
							{
								num = 7;
								continue;
							}
							goto IL_8F;
						case 4:
							if (this.Text.Equals(string.Empty))
							{
								num = 5;
								continue;
							}
							num = 13;
							continue;
						case 5:
							goto IL_8A;
						case 6:
							goto IL_8F;
						case 7:
							num = 10;
							continue;
						case 8:
							goto IL_163;
						case 9:
							goto IL_B4;
						case 10:
							if (base.OwnerParagraph.Format.HorizontalAlignment != Spire.Doc.Documents.HorizontalAlignment.Left)
							{
								num = 11;
								continue;
							}
							goto IL_8F;
						case 11:
							num = 2;
							continue;
						case 12:
							if (this.CharacterFormat.Font == null)
							{
								num = 9;
								continue;
							}
							goto IL_21F;
						case 13:
							if (base.OwnerParagraph != null)
							{
								num = 1;
								continue;
							}
							goto IL_8F;
						case 14:
							a_2 = this.Text.TrimEnd(new char[0]);
							num = 6;
							continue;
						}
						break;
						IL_8F:
						num = 12;
						continue;
						IL_B4:
						num = 0;
					}
				}
				IL_8A:
				SizeF result = dc.ᜀ(this, ClipboardData.b("䙧", a_));
				result.Width = 0f;
				return result;
				IL_163:
				return default(SizeF);
				IL_21F:
				return dc.ᜀ(this, a_2);
			}
			}
		}

		// Token: 0x04003522 RID: 13602
		private new int ᜀ;

		// Token: 0x04003523 RID: 13603
		private new string ᜁ = string.Empty;

		// Token: 0x04003524 RID: 13604
		private bool ᜂ;

		// Token: 0x04003525 RID: 13605
		internal string ᜃ = string.Empty;

		// Token: 0x04003526 RID: 13606
		internal new bool ᜄ;

		// Token: 0x04003527 RID: 13607
		private bool? ᜅ;

		// Token: 0x04003528 RID: 13608
		[CompilerGenerated]
		private Paragraph ᜆ;

		// Token: 0x0200050F RID: 1295
		internal new class ᜀ : sprḈ
		{
			// Token: 0x06004299 RID: 17049 RVA: 0x003E8A68 File Offset: 0x003E7A68
			public ᜀ(TextRange A_0)
			{
				int a_ = 2;
				base..ctor(ChildrenLayoutDirection.Horizontal);
				A_0.Text = string.Empty;
				float num;
				if (A_0.Owner.Owner.Owner is Section)
				{
					num = (A_0.Owner.Owner.Owner as Section).PageSetup.Margins.Left;
				}
				else
				{
					num = A_0.Document.LastSection.PageSetup.Margins.Left;
				}
				num = ((num != -0.05f) ? num : 0f);
				this.ᜀ = (double)A_0.Owner.Document.LastSection.PageSetup.DefaultTabWidth;
				this.ᜂ = (double)num;
				Paragraph ownerParagraph = A_0.OwnerParagraph;
				if (ownerParagraph.IsInCell)
				{
					num = ((ownerParagraph.OwnerTextBody as TableCell).ᜀ as spr\u2032).\u170D();
				}
				ParagraphFormat paragraphFormat = ownerParagraph.Format;
				IParagraphStyle paragraphStyle = ownerParagraph.GetStyle();
				if (paragraphStyle == null)
				{
					paragraphStyle = (A_0.Document.Styles.FindByName(ClipboardData.b("♧թṫͭᅯṱ", a_), StyleType.ParagraphStyle) as IParagraphStyle);
					if (paragraphStyle == null)
					{
						paragraphStyle = (ParagraphStyle)Style.CreateBuiltinStyle(BuiltinStyle.Normal, A_0.Document);
					}
				}
				if (paragraphStyle.ParagraphFormat.Tabs.Count > 0)
				{
					paragraphFormat = ownerParagraph.GetStyle().ParagraphFormat;
				}
				float num2 = 20f;
				int i = 0;
				int count = paragraphFormat.Tabs.Count;
				while (i < count)
				{
					Tab tab = paragraphFormat.Tabs[i];
					if (tab.Position != 0f || tab.DeletePosition == 0f)
					{
						base.ᜀ(((tab.Position != 0f) ? tab.Position : (tab.DeletePosition / num2)) + num, (Spire.Layouting.TabJustification)tab.Justification, (Spire.Layouting.TabLeader)tab.TabLeader);
					}
					i++;
				}
				paragraphFormat = ownerParagraph.Format;
				int j = 0;
				int count2 = paragraphFormat.Tabs.Count;
				while (j < count2)
				{
					Tab tab = paragraphFormat.Tabs[j];
					bool flag = false;
					bool flag2 = false;
					int index = 0;
					if (this.ᜁ.Count != 0)
					{
						for (int k = 0; k < this.ᜁ.Count; k++)
						{
							if (Math.Truncate((double)this.ᜁ[k].ᜂ()) == Math.Truncate((double)(tab.Position + num)))
							{
								flag = true;
								index = k;
								break;
							}
							if (Math.Truncate((double)this.ᜁ[k].ᜂ()) == Math.Truncate((double)(tab.DeletePosition / num2 + num)))
							{
								flag2 = true;
								index = k;
								break;
							}
						}
					}
					if ((tab.Position != 0f || tab.DeletePosition == 0f) && !flag2 && !flag)
					{
						base.ᜀ(((tab.Position != 0f) ? tab.Position : (tab.DeletePosition / num2)) + num, (Spire.Layouting.TabJustification)tab.Justification, (Spire.Layouting.TabLeader)tab.TabLeader);
					}
					else if (flag2)
					{
						this.ᜁ.RemoveAt(index);
					}
					else if (flag)
					{
						this.ᜁ[index].ᜀ((Spire.Layouting.TabJustification)tab.Justification);
						this.ᜁ[index].ᜀ(((tab.Position != 0f) ? tab.Position : (tab.DeletePosition / num2)) + num);
						this.ᜁ[index].ᜀ((Spire.Layouting.TabLeader)tab.TabLeader);
					}
					j++;
				}
			}
		}
	}
}
