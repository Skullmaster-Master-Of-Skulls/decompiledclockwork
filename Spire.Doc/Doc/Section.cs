using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using Spire.CompoundFile.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Interface;
using Spire.Layouting;

namespace Spire.Doc
{
	// Token: 0x020000EA RID: 234
	public class Section : DocumentContainer, ISection, spr\u17C8
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0002957C File Offset: 0x0002857C
		public Body Body
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
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060003AF RID: 943 RVA: 0x000295C0 File Offset: 0x000285C0
		public HeadersFooters HeadersFooters
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

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00029604 File Offset: 0x00028604
		public PageSetup PageSetup
		{
			get
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_7A;
					case 1:
						num = 4;
						continue;
					case 2:
						if (this.ᜋ == null)
						{
							goto IL_AC;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7A;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 4:
						if (true)
						{
						}
						if ((base.Owner as Document).OperationType == DocumentOperationType.Layout)
						{
							num = 5;
							continue;
						}
						goto IL_AC;
					case 5:
						num = 2;
						continue;
					}
					if (!(base.Owner is Document))
					{
						goto IL_AC;
					}
					num = 1;
				}
				IL_7A:
				return this.ᜋ;
				IL_AC:
				return this.ᜂ;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x000296D0 File Offset: 0x000286D0
		internal PageSetup RealPageSetup
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
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00029714 File Offset: 0x00028714
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x00029758 File Offset: 0x00028758
		internal PageSetup LayoutPageSetup
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
				return this.ᜋ;
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
				this.ᜋ = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0002979C File Offset: 0x0002879C
		public ColumnCollection Columns
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
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x000297E0 File Offset: 0x000287E0
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x00029824 File Offset: 0x00028824
		public SectionBreakType BreakCode
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
				this.ᜅ = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00029868 File Offset: 0x00028868
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x000298AC File Offset: 0x000288AC
		internal byte[] DataArray
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
				return this.m_internalData;
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
				this.m_internalData = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x000298F0 File Offset: 0x000288F0
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
				return DocumentObjectType.Section;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0002992C File Offset: 0x0002892C
		public DocumentObjectCollection ChildObjects
		{
			get
			{
				int num = 4;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						goto IL_61;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_84;
						default:
							if (false)
							{
							}
							this.ᜆ = new Section.ᜀ();
							this.ᜆ.InnerList.Add(this.ᜁ);
							this.ᜁ.ᜀ(this);
							num2 = 0;
							num = 5;
							continue;
						}
						break;
					case 2:
						if (num2 >= 6)
						{
							num = 3;
							continue;
						}
						goto IL_84;
					case 3:
						goto IL_7F;
					case 5:
						goto IL_61;
					}
					if (this.ᜆ == null)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					break;
					IL_61:
					num = 2;
					continue;
					IL_84:
					this.ᜆ.InnerList.Add(this.ᜄ[num2]);
					this.ᜄ[num2].ᜀ(this);
					num2++;
					num = 0;
				}
				IL_7F:
				return this.ᜆ;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060003BB RID: 955 RVA: 0x00029A44 File Offset: 0x00028A44
		public ParagraphCollection Paragraphs
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
				return this.Body.Paragraphs;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00029A8C File Offset: 0x00028A8C
		public TableCollection Tables
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
				return this.Body.Tables;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00029AD4 File Offset: 0x00028AD4
		internal Dictionary<string, string> OldParaStylesHolder
		{
			get
			{
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜇ = new Dictionary<string, string>();
						goto IL_67;
					case 2:
						goto IL_6F;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_67:
						num = 2;
						break;
					default:
						if (false)
						{
						}
						if (this.ᜇ != null)
						{
							goto IL_71;
						}
						num = 0;
						break;
					}
				}
				IL_6F:
				IL_71:
				return this.ᜇ;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00029B58 File Offset: 0x00028B58
		internal Dictionary<string, string> OldCharStylesHolder
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 2:
						this.ᜈ = new Dictionary<string, string>();
						goto IL_5F;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5F:
						if (true)
						{
						}
						num = 0;
						break;
					default:
						if (false)
						{
						}
						if (this.ᜈ != null)
						{
							goto IL_71;
						}
						num = 2;
						break;
					}
				}
				IL_6F:
				IL_71:
				return this.ᜈ;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00029BDC File Offset: 0x00028BDC
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x00029C20 File Offset: 0x00028C20
		internal TextDirection TextDirection
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜉ = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00029C64 File Offset: 0x00028C64
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x00029CA8 File Offset: 0x00028CA8
		internal int SectionCountPages
		{
			get
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
				return this.ᜌ;
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
				this.ᜌ = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x00029CEC File Offset: 0x00028CEC
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x00029D30 File Offset: 0x00028D30
		public bool ProtectForm
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
				return this.ᜊ;
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
				this.ᜊ = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x00029D74 File Offset: 0x00028D74
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x00029DB8 File Offset: 0x00028DB8
		internal bool ReLayout
		{
			[CompilerGenerated]
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
				return this.\u170D;
			}
			[CompilerGenerated]
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
				this.\u170D = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00029DFC File Offset: 0x00028DFC
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x00029E40 File Offset: 0x00028E40
		internal bool IsColumnsBreak
		{
			[CompilerGenerated]
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
				return this.ᜎ;
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
				this.ᜎ = value;
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00029E84 File Offset: 0x00028E84
		public Section(IDocument doc) : base((Document)doc, null)
		{
			this.ᜁ = new Body(this);
			this.ᜃ = new ColumnCollection(this);
			this.ᜄ = new HeadersFooters(this);
			this.ᜄ.ᜀ(this);
			this.ᜂ = new PageSetup(this);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00029EE8 File Offset: 0x00028EE8
		public Column AddColumn(float width, float spacing)
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
			Column column = new Column(base.Document);
			column.Width = width;
			column.Space = spacing;
			this.Columns.Add(column);
			return column;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00029F4C File Offset: 0x00028F4C
		public void MakeColumnsSameWidth()
		{
			switch (0)
			{
			default:
			{
				int num = 2;
				float width;
				IEnumerator enumerator;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						SizeF pageSize = this.PageSetup.PageSize;
						num = 3;
						continue;
					}
					case 1:
						goto IL_11E;
					case 3:
					{
						SizeF pageSize;
						float num2 = pageSize.Width - ((this.PageSetup.Margins.Left != -0.05f) ? this.PageSetup.Margins.Left : 0f) - ((this.PageSetup.Margins.Right != -0.05f) ? this.PageSetup.Margins.Right : 0f);
						width = (num2 - (float)(this.Columns.Count - 1) * 36f) / (float)this.Columns.Count;
						enumerator = this.Columns.GetEnumerator();
						if (true)
						{
						}
						num = 1;
						continue;
					}
					}
					if (this.Columns.Count <= 0)
					{
						return;
					}
					num = 0;
				}
				IL_11E:
				try
				{
					num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_163;
							default:
								if (false)
								{
								}
								break;
							}
							break;
						case 1:
							goto IL_163;
						case 2:
							goto IL_1B7;
						case 4:
						{
							if (!enumerator.MoveNext())
							{
								num = 1;
								continue;
							}
							Column column = (Column)enumerator.Current;
							column.Width = width;
							column.Space = 36f;
							num = 0;
							continue;
						}
						}
						IL_148:
						num = 4;
						continue;
						goto IL_148;
						IL_163:
						num = 2;
					}
					IL_1B7:;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_1FF;
							case 1:
								disposable.Dispose();
								num = 0;
								continue;
							case 2:
								if (disposable != null)
								{
									num = 1;
									continue;
								}
								goto IL_201;
							}
							break;
						}
					}
					IL_1FF:
					IL_201:;
				}
				return;
			}
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0002A16C File Offset: 0x0002916C
		public new Section Clone()
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
			return (Section)base.Clone();
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0002A1B4 File Offset: 0x000291B4
		internal Section ᜏ()
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
			return (Section)this.ᜃ();
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0002A1FC File Offset: 0x000291FC
		public Paragraph AddParagraph()
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
			return this.Body.AddParagraph();
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0002A244 File Offset: 0x00029244
		public Table AddTable()
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
			return this.Body.AddTable();
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0002A28C File Offset: 0x0002928C
		public Table AddTable(bool showBorder)
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
			return this.Body.AddTable(showBorder);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0002A2D4 File Offset: 0x000292D4
		internal new spr\u2215 ᜀ()
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
			return this.Body.ᜐ();
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0002A31C File Offset: 0x0002931C
		internal string ᜋ()
		{
			string text;
			for (;;)
			{
				IL_3C:
				text = string.Empty;
				int num = 0;
				for (;;)
				{
					IL_44:
					int num2 = 5;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							num = base.Document.ᜅ.ឯ();
							base.Document.ᜅ = null;
							if (true)
							{
							}
							num2 = 7;
							continue;
						case 1:
						{
							if (num >= this.Body.ChildObjects.Count)
							{
								num2 = 4;
								continue;
							}
							DocumentObject documentObject = this.Body.ChildObjects[num];
							num2 = 12;
							continue;
						}
						case 2:
							goto IL_8D;
						case 3:
						{
							DocumentObject documentObject;
							if (documentObject is Table)
							{
								num2 = 6;
								continue;
							}
							goto IL_8D;
						}
						case 4:
							return text;
						case 5:
							goto IL_D4;
						case 6:
						{
							DocumentObject documentObject;
							text += (documentObject as Table).ᜐ();
							num2 = 2;
							continue;
						}
						case 7:
							goto IL_156;
						case 8:
							goto IL_D4;
						case 9:
							goto IL_8D;
						case 10:
						{
							DocumentObject documentObject;
							text += (documentObject as Paragraph).ᜈ();
							num2 = 9;
							continue;
						}
						case 11:
							if (base.Document.ᜅ != null)
							{
								num2 = 0;
								continue;
							}
							goto IL_156;
						case 12:
						{
							DocumentObject documentObject;
							if (documentObject is Paragraph)
							{
								num2 = 10;
								continue;
							}
							num2 = 3;
							continue;
						}
						}
						goto IL_3C;
						IL_8D:
						num2 = 11;
						continue;
						IL_D4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_44;
						default:
							if (false)
							{
							}
							num2 = 1;
							continue;
						}
						IL_156:
						num++;
						num2 = 8;
					}
				}
			}
			return text;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0002A4DC File Offset: 0x000294DC
		internal Section ᜂ()
		{
			bool flag;
			Section section;
			for (;;)
			{
				flag = base.Document.ᜉ;
				base.Document.ᜉ = true;
				section = new Section(base.Document);
				section.ᜂ = this.PageSetup.ᜄ();
				section.ᜂ.ᜀ(section);
				section.ᜃ = new ColumnCollection(section);
				this.ᜃ.ᜀ(section.ᜃ);
				section.ᜄ = this.ᜄ.ᜃ();
				section.ᜄ.ᜀ(section);
				int i = 0;
				int num = 3;
				for (;;)
				{
					IL_02:
					switch (num)
					{
					case 0:
						while (i < 6)
						{
							section.ᜄ[i].ᜀ(section);
							i++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 1;
								goto IL_02;
							}
						}
						if (true)
						{
						}
						num = 2;
						continue;
					case 1:
						goto IL_A2;
					case 2:
						goto IL_C4;
					case 3:
						goto IL_A2;
					}
					break;
					IL_A2:
					num = 0;
				}
			}
			IL_C4:
			base.Document.ᜉ = flag;
			return section;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0002A608 File Offset: 0x00029608
		internal override void CloneRelationsTo(Document doc, OwnerHolder nextOwner)
		{
			if (true)
			{
			}
			for (;;)
			{
				doc.CurClonedSection = this;
				base.CloneRelationsTo(doc, nextOwner);
				this.Body.CloneRelationsTo(doc, nextOwner);
				ImportOptions importOptions = doc.ImportOption;
				bool a_ = doc.ImportStyles;
				int num = 3;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						doc.ImportOption = ImportOptions.UseDestinationStyles;
						doc.ImportStyles = false;
						num = 5;
						continue;
					case 1:
						goto IL_130;
					case 2:
						if (doc.ImportOption == importOptions)
						{
							goto IL_152;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6A;
						default:
							if (false)
							{
							}
							num = 9;
							continue;
						}
						break;
					case 3:
						goto IL_6A;
					case 4:
						goto IL_130;
					case 5:
						goto IL_121;
					case 6:
						goto IL_11F;
					case 7:
						num = 2;
						continue;
					case 8:
						if (num2 > 5)
						{
							num = 7;
							continue;
						}
						this.ᜄ[num2].CloneRelationsTo(doc, nextOwner);
						num2++;
						num = 1;
						continue;
					case 9:
						doc.ImportOption = importOptions;
						doc.ImportStyles = a_;
						num = 6;
						continue;
					}
					break;
					IL_6A:
					if (doc.ImportOption != ImportOptions.UseDestinationStyles)
					{
						num = 0;
						continue;
					}
					IL_121:
					num2 = 0;
					num = 4;
					continue;
					IL_130:
					num = 8;
				}
			}
			IL_11F:
			IL_152:
			doc.CurClonedSection.OldCharStylesHolder.Clear();
			doc.CurClonedSection.OldParaStylesHolder.Clear();
			doc.CurClonedSection = null;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0002A790 File Offset: 0x00029790
		protected override object CloneImpl()
		{
			bool flag;
			Section section;
			for (;;)
			{
				flag = base.Document.ᜉ;
				base.Document.ᜉ = true;
				section = (Section)base.CloneImpl();
				section.ᜆ = null;
				section.ᜁ = (Body)this.ᜁ.Clone();
				section.ᜁ.ᜀ(section);
				section.ᜂ = this.PageSetup.ᜄ();
				section.ᜂ.ᜀ(section);
				section.ᜃ = new ColumnCollection(section);
				this.ᜃ.ᜀ(section.ᜃ);
				section.ᜄ = this.ᜄ.ᜃ();
				section.ᜄ.ᜀ(section);
				int i = 0;
				int num = 3;
				for (;;)
				{
					IL_02:
					switch (num)
					{
					case 0:
						goto IL_ED;
					case 1:
						while (i < 6)
						{
							section.ᜄ[i].ᜀ(section);
							i++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 2;
								goto IL_02;
							}
						}
						num = 0;
						continue;
					case 2:
						goto IL_D3;
					case 3:
						if (true)
						{
						}
						goto IL_D3;
					}
					break;
					IL_D3:
					num = 1;
				}
			}
			IL_ED:
			base.Document.ᜉ = flag;
			return section;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0002A8E4 File Offset: 0x000298E4
		internal object ᜃ()
		{
			bool flag;
			Section section;
			for (;;)
			{
				flag = base.Document.ᜉ;
				base.Document.ᜉ = true;
				section = (Section)base.CloneImpl();
				section.ᜆ = null;
				section.ᜁ = (Body)this.ᜁ.Clone();
				section.ᜁ.ᜀ(section);
				section.ᜂ = this.PageSetup.ᜄ();
				section.ᜂ.ᜀ(section);
				section.ᜃ = new ColumnCollection(section);
				this.ᜃ.ᜀ(section.ᜃ);
				section.ᜄ = this.ᜄ.ᜃ();
				int i = 0;
				int num = 0;
				for (;;)
				{
					IL_02:
					switch (num)
					{
					case 0:
						goto IL_BF;
					case 1:
						if (true)
						{
						}
						goto IL_BF;
					case 2:
						while (i < 6)
						{
							section.ᜄ[i].ᜀ(section);
							i++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 1;
								goto IL_02;
							}
						}
						num = 3;
						continue;
					case 3:
						goto IL_D9;
					}
					break;
					IL_BF:
					num = 2;
				}
			}
			IL_D9:
			base.Document.ᜉ = flag;
			return section;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0002AA2C File Offset: 0x00029A2C
		internal void ᜄ()
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
					return;
				case 1:
					for (;;)
					{
						this.ᜄ.ᜀ(this.ᜄ.LinkToPrevious);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_67;
						}
					}
					IL_67:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				if (!this.ᜄ.LinkToPrevious)
				{
					break;
				}
				num = 1;
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0002AABC File Offset: 0x00029ABC
		internal void ᜁ(bool A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜁ.ᜂ(A_0);
					int num = 9;
					for (;;)
					{
						int num2;
						sprḍ sprḍ;
						spr\u1CC1 spr_u1CC;
						switch (num)
						{
						case 0:
							goto IL_161;
						case 1:
							goto IL_294;
						case 2:
							if (this.m_internalData.Length < 300)
							{
								num = 19;
								continue;
							}
							goto IL_19D;
						case 3:
							if (num2 >= 6)
							{
								num = 11;
								continue;
							}
							this.HeadersFooters[num2].ᜂ(A_0);
							num2++;
							num = 14;
							continue;
						case 4:
							num = 2;
							continue;
						case 5:
						{
							int num3 = sprḍ.ᜂ().IndexOf(spr_u1CC) + 1;
							List<spr\u1CC1> list = null;
							num = 6;
							continue;
						}
						case 6:
						{
							int num3;
							if (num3 < sprḍ.ᜈ())
							{
								num = 20;
								continue;
							}
							goto IL_9C;
						}
						case 7:
							if (this.m_internalData.Length > 0)
							{
								num = 0;
								continue;
							}
							goto IL_19D;
						case 8:
						{
							int num4;
							int num5;
							if (num4 >= num5)
							{
								if (true)
								{
								}
								num = 10;
								continue;
							}
							List<spr\u1CC1> list;
							list.Add(sprḍ.ᜁ(num4));
							num4++;
							num = 18;
							continue;
						}
						case 9:
							if (this.m_internalData != null)
							{
								num = 4;
								continue;
							}
							goto IL_19D;
						case 10:
						{
							List<spr\u1CC1> list;
							List<spr\u1CC1>.Enumerator enumerator = list.GetEnumerator();
							num = 17;
							continue;
						}
						case 11:
							return;
						case 12:
							if (spr_u1CC != null)
							{
								num = 5;
								continue;
							}
							goto IL_294;
						case 13:
							goto IL_19D;
						case 14:
							goto IL_1D6;
						case 15:
							goto IL_205;
						case 16:
							goto IL_1D6;
						case 17:
							try
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 2:
										goto IL_14E;
									case 3:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											break;
										default:
											if (false)
											{
											}
											num = 2;
											continue;
										}
										break;
									case 4:
									{
										List<spr\u1CC1>.Enumerator enumerator;
										if (!enumerator.MoveNext())
										{
											num = 3;
											continue;
										}
										spr\u1CC1 spr_u1CC2 = enumerator.Current;
										sprḍ.ᜆ((int)spr_u1CC2.ᜂ());
										sprḍ.ᜆ(spr_u1CC2);
										num = 0;
										continue;
									}
									}
									IL_109:
									num = 4;
									continue;
									goto IL_109;
								}
								IL_14E:
								goto IL_9C;
							}
							finally
							{
								List<spr\u1CC1>.Enumerator enumerator;
								((IDisposable)enumerator).Dispose();
							}
							goto IL_161;
						case 18:
							goto IL_205;
						case 19:
							num = 7;
							continue;
						case 20:
						{
							List<spr\u1CC1> list = new List<spr\u1CC1>();
							int num3;
							int num4 = num3;
							int num5 = sprḍ.ᜈ();
							num = 15;
							continue;
						}
						}
						break;
						IL_9C:
						sprḍ.ᜆ(12857);
						num = 1;
						continue;
						IL_161:
						sprḍ = new sprḍ(this.m_internalData, 0);
						spr_u1CC = sprḍ.ᜃ(12857);
						num = 12;
						continue;
						IL_19D:
						num2 = 0;
						num = 16;
						continue;
						IL_1D6:
						num = 3;
						continue;
						IL_205:
						num = 8;
						continue;
						IL_294:
						this.m_internalData = new byte[this.m_internalData.Length];
						sprḍ.ᜀ(this.m_internalData, 0);
						num = 13;
					}
				}
				return;
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0002AE30 File Offset: 0x00029E30
		internal bool \u170D()
		{
			int num = 0;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 1:
					goto IL_AD;
				case 2:
					if (num2 >= 6)
					{
						num = 5;
						continue;
					}
					goto IL_62;
				case 3:
					goto IL_AD;
				case 4:
					if (this.HeadersFooters[num2].ᜑ())
					{
						num = 6;
						continue;
					}
					num2++;
					num = 3;
					continue;
				case 5:
					return false;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_62;
					default:
						goto IL_9B;
					}
					break;
				case 7:
					return true;
				}
				if (this.ᜁ.ᜑ())
				{
					num = 7;
					continue;
				}
				num2 = 0;
				num = 1;
				continue;
				IL_62:
				num = 4;
				continue;
				IL_AD:
				num = 2;
			}
			return true;
			IL_9B:
			if (true)
			{
			}
			if (false)
			{
			}
			return true;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0002AF18 File Offset: 0x00029F18
		internal void ᜁ()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_94;
				case 2:
					goto IL_74;
				case 3:
					goto IL_123;
				case 4:
					if (this.ᜄ != null)
					{
						num = 6;
						continue;
					}
					goto IL_74;
				case 5:
					this.ᜁ.ᜅ();
					this.ᜁ = null;
					num = 0;
					continue;
				case 6:
					if (true)
					{
					}
					this.ᜄ.ᜂ();
					this.ᜄ = null;
					num = 2;
					continue;
				case 7:
					if (this.ᜇ != null)
					{
						num = 11;
						continue;
					}
					return;
				case 8:
					return;
				case 9:
					this.ᜈ.Clear();
					this.ᜈ = null;
					num = 3;
					continue;
				case 10:
					if (this.ᜈ != null)
					{
						num = 9;
						continue;
					}
					goto IL_123;
				case 11:
					this.ᜇ.Clear();
					this.ᜇ = null;
					num = 8;
					continue;
				}
				if (this.ᜁ != null)
				{
					num = 5;
					continue;
				}
				goto IL_94;
				IL_74:
				num = 10;
				continue;
				IL_94:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				IL_123:
				num = 7;
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0002B08C File Offset: 0x0002A08C
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
			base.InitXDLSHolder();
			base.XDLSHolder.AddElement(ClipboardData.b("ࡩͫ੭९", a_), this.ᜁ);
			base.XDLSHolder.AddElement(ClipboardData.b("ᩩ൫७ᕯ影ݳ፵౷ཹ౻", a_), this.ᜂ);
			base.XDLSHolder.AddElement(ClipboardData.b("३ͫɭկάᩳյ", a_), this.ᜃ);
			base.XDLSHolder.AddElement(ClipboardData.b("ɩ५཭ᑯ᝱ٳյ啷ᱹ፻ᅽ", a_), this.ᜄ);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0002B154 File Offset: 0x0002A154
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 8;
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
			writer.WriteValue(ClipboardData.b("Ɑɯ᝱ᕳᵵ㭷ᕹ᡻᭽", a_), this.BreakCode);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0002B1C0 File Offset: 0x0002A1C0
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 14;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						if (reader.HasAttribute(ClipboardData.b("㙳ѵᵷ᭹᝻㵽", a_)))
						{
							num = 2;
							continue;
						}
						return;
					case 1:
						return;
					case 2:
						this.BreakCode = (SectionBreakType)reader.ReadEnum(ClipboardData.b("㙳ѵᵷ᭹᝻㵽", a_), typeof(SectionBreakType));
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
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
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0002B280 File Offset: 0x0002A280
		protected override void WriteXmlContent(IXDLSContentWriter writer)
		{
			int a_ = 16;
			for (;;)
			{
				if (true)
				{
				}
				base.WriteXmlContent(writer);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.DataArray != null)
						{
							num = 1;
							continue;
						}
						return;
					case 1:
						writer.WriteChildBinaryElement(ClipboardData.b("ήᙷ๹᥻౽ꮅ", a_), this.DataArray);
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
						return;
					}
					break;
				}
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0002B320 File Offset: 0x0002A320
		protected override bool ReadXmlContent(IXDLSContentReader reader)
		{
			int a_ = 2;
			bool result;
			for (;;)
			{
				result = base.ReadXmlContent(reader);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						if (reader.TagName == ClipboardData.b("ŧѩᡫ୭ɯᱱᕳ᩵啷ṹᵻ੽", a_))
						{
							num = 1;
							continue;
						}
						return result;
					case 1:
						this.DataArray = reader.ReadChildBinaryElement();
						result = true;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return result;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						return result;
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0002B3CC File Offset: 0x0002A3CC
		protected override void CreateLayoutInfo()
		{
			for (;;)
			{
				this.ᜀ = new spr\u22A8(ChildrenLayoutDirection.Vertical);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜀ.ᜁ(true);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (true)
							{
							}
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
						if (this.Body.Items.Count == 0)
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

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0002B464 File Offset: 0x0002A464
		protected override IDocumentObjectCollection WidgetCollection
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
				return this.ᜁ.Items;
			}
		}

		// Token: 0x04000CF0 RID: 3312
		private new const float ᜀ = 36f;

		// Token: 0x04000CF1 RID: 3313
		private Body ᜁ;

		// Token: 0x04000CF2 RID: 3314
		private PageSetup ᜂ;

		// Token: 0x04000CF3 RID: 3315
		private ColumnCollection ᜃ;

		// Token: 0x04000CF4 RID: 3316
		internal new HeadersFooters ᜄ;

		// Token: 0x04000CF5 RID: 3317
		private SectionBreakType ᜅ = SectionBreakType.NewPage;

		// Token: 0x04000CF6 RID: 3318
		private DocumentObjectCollection ᜆ;

		// Token: 0x04000CF7 RID: 3319
		protected internal byte[] m_internalData;

		// Token: 0x04000CF8 RID: 3320
		private Dictionary<string, string> ᜇ;

		// Token: 0x04000CF9 RID: 3321
		private Dictionary<string, string> ᜈ;

		// Token: 0x04000CFA RID: 3322
		internal TextDirection ᜉ;

		// Token: 0x04000CFB RID: 3323
		private bool ᜊ = true;

		// Token: 0x04000CFC RID: 3324
		private PageSetup ᜋ;

		// Token: 0x04000CFD RID: 3325
		private int ᜌ;

		// Token: 0x04000CFE RID: 3326
		[CompilerGenerated]
		private bool \u170D;

		// Token: 0x04000CFF RID: 3327
		[CompilerGenerated]
		private bool ᜎ;

		// Token: 0x020000EC RID: 236
		internal new class ᜀ : DocumentObjectCollection
		{
			// Token: 0x060003F1 RID: 1009 RVA: 0x0002B4AC File Offset: 0x0002A4AC
			internal ᜀ() : base(null)
			{
			}

			// Token: 0x060003F2 RID: 1010 RVA: 0x0002B4C0 File Offset: 0x0002A4C0
			protected virtual string ᜁ()
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
				throw new Exception();
			}

			// Token: 0x060003F3 RID: 1011 RVA: 0x0002B500 File Offset: 0x0002A500
			protected virtual OwnerHolder ᜀ(IXDLSContentReader A_0)
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
				throw new Exception();
			}

			// Token: 0x060003F4 RID: 1012 RVA: 0x0002B540 File Offset: 0x0002A540
			protected virtual Type[] ᜀ()
			{
				int a_ = 9;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(ClipboardData.b("Ɱၰᵲ᭴ᡶ൸孺ᑼᅾꦈ꾎ﺐﾔ滛붜캠莢슦쪨\udfaa쒬삮\udfb0\uddb4\udeb6햸\udfba톾뗀ꫂ뇄껆곈룊곎뻐뿒맔닖뫘꿚드냞迠췢", a_));
			}
		}
	}
}
