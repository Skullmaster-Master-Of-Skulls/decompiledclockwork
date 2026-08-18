using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Spire.CompoundFile.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;
using Spire.Layouting;

namespace Spire.Doc
{
	// Token: 0x0200009A RID: 154
	public class Body : DocumentContainer, IBody, spr\u17C8
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000F8D0 File Offset: 0x0000E8D0
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
				return DocumentObjectType.Body;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000F90C File Offset: 0x0000E90C
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
				return this.ᜀ;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000F950 File Offset: 0x0000E950
		public TableCollection Tables
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

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000F994 File Offset: 0x0000E994
		public FormFieldCollection FormFields
		{
			get
			{
				int num;
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
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_70;
					case 1:
						this.ᜂ = new FormFieldCollection(this);
						if (true)
						{
						}
						num = 0;
						continue;
					}
					if (this.ᜂ != null)
					{
						break;
					}
					num = 1;
				}
				IL_70:
				return this.ᜂ;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000FA1C File Offset: 0x0000EA1C
		public IParagraph LastParagraph
		{
			get
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
					if (this.Paragraphs.Count > 0)
					{
						return this.Paragraphs[this.Paragraphs.Count - 1];
					}
					break;
				}
				if (true)
				{
				}
				return null;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000FA84 File Offset: 0x0000EA84
		internal bool IsFormFieldsCreated
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
				return this.ᜂ != null;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000FACC File Offset: 0x0000EACC
		internal BodyRegionCollection Items
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
				return this.m_bodyItems;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000FB10 File Offset: 0x0000EB10
		public DocumentObjectCollection ChildObjects
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
				return this.m_bodyItems;
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000FB54 File Offset: 0x0000EB54
		internal Body(Document A_0, DocumentObject A_1) : base(A_0, A_1)
		{
			this.m_bodyItems = new BodyRegionCollection(this);
			this.ᜀ = new ParagraphCollection(this.m_bodyItems);
			this.ᜁ = new TableCollection(this.m_bodyItems);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000FB98 File Offset: 0x0000EB98
		internal Body(Section A_0) : this(A_0.Document, A_0)
		{
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000FBB4 File Offset: 0x0000EBB4
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
			Paragraph entity = new Paragraph(base.Document);
			int a_ = this.m_bodyItems.Add(entity);
			return this.m_bodyItems[a_] as Paragraph;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000FC1C File Offset: 0x0000EC1C
		public Table AddTable()
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
			Table table = new Table(base.Document);
			this.m_bodyItems.Add(table);
			return table;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000FC74 File Offset: 0x0000EC74
		public Table AddTable(bool showBorder)
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
			Table table = new Table(showBorder, base.Document);
			this.m_bodyItems.Add(table);
			return table;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000FCCC File Offset: 0x0000ECCC
		internal spr\u2215 ᜐ()
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
			spr\u2215 spr_u = new spr\u1AE7(this.m_doc);
			this.m_bodyItems.Add(spr_u);
			return spr_u;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000FD24 File Offset: 0x0000ED24
		public void InsertXHTML(string html)
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
			this.InsertXHTML(html, this.Paragraphs.Count);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000FD74 File Offset: 0x0000ED74
		public void InsertXHTML(string html, int paragraphIndex)
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
			this.Paragraphs.Insert(paragraphIndex, new Paragraph(base.Document));
			this.InsertXHTML(html, paragraphIndex, 0);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000FDD0 File Offset: 0x0000EDD0
		public void InsertXHTML(string html, int paragraphIndex, int paragraphItemIndex)
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
			spr\u2477 spr_u = sprᴈ.ᜀ();
			spr_u.ᜀ(this, html, paragraphIndex, paragraphItemIndex);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000FE1C File Offset: 0x0000EE1C
		public bool IsValidXHTML(string html, XHTMLValidationType type)
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
			spr\u2477 spr_u = sprᴈ.ᜀ();
			return spr_u.ᜀ(html, type);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000FE68 File Offset: 0x0000EE68
		public bool IsValidXHTML(string html, XHTMLValidationType type, out string exceptionMessage)
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
			spr\u2477 spr_u = sprᴈ.ᜀ();
			return spr_u.ᜀ(html, type, out exceptionMessage);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000FEB4 File Offset: 0x0000EEB4
		public void EnsureMinimum()
		{
			int num;
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
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					this.AddParagraph();
					num = 0;
					continue;
				}
				if (this.Paragraphs.Count != 0)
				{
					break;
				}
				num = 2;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000FF34 File Offset: 0x0000EF34
		internal new TextSelection ᜀ(Regex A_0)
		{
			switch (0)
			{
			default:
			{
				IEnumerator enumerator = this.Items.GetEnumerator();
				TextSelection result;
				try
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_10E;
						case 2:
						{
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							BodyRegion bodyRegion = (BodyRegion)enumerator.Current;
							TextSelection textSelection = bodyRegion.Find(A_0);
							num = 5;
							continue;
						}
						case 3:
						{
							TextSelection textSelection;
							if (textSelection.Count > 0)
							{
								num = 7;
								continue;
							}
							break;
						}
						case 4:
							goto IL_102;
						case 5:
						{
							TextSelection textSelection;
							if (textSelection != null)
							{
								num = 8;
								continue;
							}
							break;
						}
						case 6:
							goto IL_D7;
						case 7:
						{
							TextSelection textSelection;
							result = textSelection;
							num = 6;
							continue;
						}
						case 8:
							num = 3;
							continue;
						}
						IL_60:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_102:
							num = 0;
							continue;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						goto IL_60;
					}
					IL_D7:
					goto IL_15B;
					IL_10E:
					goto IL_26;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_158;
							case 1:
								if (disposable != null)
								{
									num = 2;
									continue;
								}
								goto IL_15A;
							case 2:
								disposable.Dispose();
								num = 0;
								continue;
							}
							break;
						}
					}
					IL_158:
					IL_15A:;
				}
				goto IL_15B;
				IL_26:
				return null;
				IL_15B:
				if (true)
				{
				}
				return result;
			}
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000100B8 File Offset: 0x0000F0B8
		internal spr\u226E ᜁ(Regex A_0)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				spr\u226E spr_u226E = null;
				IEnumerator enumerator = this.Items.GetEnumerator();
				try
				{
					int num = 0;
					for (;;)
					{
						spr\u226E spr_u226E2;
						switch (num)
						{
						case 1:
						{
							if (!enumerator.MoveNext())
							{
								num = 8;
								continue;
							}
							BodyRegion bodyRegion = (BodyRegion)enumerator.Current;
							spr_u226E2 = bodyRegion.FindAll(A_0);
							num = 2;
							continue;
						}
						case 2:
							if (spr_u226E2 != null)
							{
								num = 11;
								continue;
							}
							break;
						case 3:
							num = 7;
							continue;
						case 4:
							goto IL_158;
						case 5:
							goto IL_103;
						case 6:
							if (spr_u226E2.Count > 0)
							{
								goto IL_86;
							}
							break;
						case 7:
							if (spr_u226E == null)
							{
								num = 9;
								continue;
							}
							goto IL_103;
						case 8:
							num = 4;
							continue;
						case 9:
							spr_u226E = new spr\u226E();
							num = 5;
							continue;
						case 11:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_86;
							default:
								if (false)
								{
								}
								num = 6;
								continue;
							}
							break;
						}
						goto IL_72;
						IL_86:
						num = 3;
						continue;
						IL_94:
						num = 1;
						continue;
						IL_72:
						goto IL_94;
						IL_103:
						spr_u226E.AddRange(spr_u226E2);
						num = 10;
					}
					IL_158:;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_19F;
							case 1:
								if (disposable != null)
								{
									num = 2;
									continue;
								}
								goto IL_1A1;
							case 2:
								disposable.Dispose();
								num = 0;
								continue;
							}
							break;
						}
					}
					IL_19F:
					IL_1A1:;
				}
				return spr_u226E;
			}
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00010284 File Offset: 0x0000F284
		internal new int ᜀ(Regex A_0, string A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 0;
				IEnumerator enumerator = this.Items.GetEnumerator();
				int result;
				try
				{
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 1:
							goto IL_E6;
						case 2:
							goto IL_114;
						case 3:
							result = num;
							num2 = 1;
							continue;
						case 4:
							if (base.Document.ReplaceFirst)
							{
								num2 = 7;
								continue;
							}
							break;
						case 5:
							goto IL_120;
						case 6:
						{
							if (!enumerator.MoveNext())
							{
								num2 = 2;
								continue;
							}
							BodyRegion bodyRegion = (BodyRegion)enumerator.Current;
							num += bodyRegion.Replace(A_0, A_1);
							num2 = 4;
							continue;
						}
						case 7:
							num2 = 8;
							continue;
						case 8:
							if (true)
							{
							}
							if (num > 0)
							{
								num2 = 3;
								continue;
							}
							break;
						}
						IL_62:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_114:
							num2 = 5;
							continue;
						default:
							if (false)
							{
							}
							num2 = 6;
							continue;
						}
						goto IL_62;
					}
					IL_E6:
					return result;
					IL_120:
					return num;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num2 = 1;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								disposable.Dispose();
								num2 = 2;
								continue;
							case 1:
								if (disposable != null)
								{
									num2 = 0;
									continue;
								}
								goto IL_16C;
							case 2:
								goto IL_16A;
							}
							break;
						}
					}
					IL_16A:
					IL_16C:;
				}
				return result;
			}
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00010410 File Offset: 0x0000F410
		internal new int ᜀ(Regex A_0, TextSelection A_1, bool A_2)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				int num = 0;
				IEnumerator enumerator = this.Items.GetEnumerator();
				int result;
				try
				{
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (num > 0)
							{
								num2 = 6;
								continue;
							}
							break;
						case 1:
							goto IL_EF;
						case 3:
							num2 = 0;
							continue;
						case 4:
							goto IL_11E;
						case 5:
						{
							if (!enumerator.MoveNext())
							{
								num2 = 7;
								continue;
							}
							BodyRegion bodyRegion = (BodyRegion)enumerator.Current;
							num += bodyRegion.Replace(A_0, A_1, A_2);
							num2 = 8;
							continue;
						}
						case 6:
							result = num;
							num2 = 1;
							continue;
						case 7:
							goto IL_112;
						case 8:
							if (base.Document.ReplaceFirst)
							{
								num2 = 3;
								continue;
							}
							break;
						}
						IL_6A:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_112:
							num2 = 4;
							continue;
						default:
							if (false)
							{
							}
							num2 = 5;
							continue;
						}
						goto IL_6A;
					}
					IL_EF:
					return result;
					IL_11E:
					return num;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_168;
							case 1:
								disposable.Dispose();
								num2 = 0;
								continue;
							case 2:
								if (disposable != null)
								{
									num2 = 1;
									continue;
								}
								goto IL_16A;
							}
							break;
						}
					}
					IL_168:
					IL_16A:;
				}
				return result;
			}
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0001059C File Offset: 0x0000F59C
		internal new int ᜀ(Regex A_0, TextBodyPart A_1, bool A_2)
		{
			int a_ = 11;
			switch (0)
			{
			default:
			{
				int num = 3;
				spr\u226E spr_u226E;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (spr_u226E != null)
						{
							num = 2;
							continue;
						}
						return 0;
					case 1:
						goto IL_6B;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
						{
							if (true)
							{
							}
							if (false)
							{
							}
							List<TextSelection>.Enumerator enumerator = spr_u226E.GetEnumerator();
							break;
						}
						}
						num = 1;
						continue;
					case 4:
						goto IL_58;
					}
					if (spr\u1AB5.ᜀ(A_0))
					{
						num = 4;
					}
					else
					{
						spr_u226E = this.ᜁ(A_0);
						num = 0;
					}
				}
				IL_58:
				throw new ArgumentException(ClipboardData.b("≰ᙲᑴն᩸፺嵼౾ꮊﾐﶒ杖릘連뾞쒠캢햤펦킨", a_));
				IL_6B:
				try
				{
					num = 6;
					for (;;)
					{
						TextSelection textSelection;
						CharacterFormat a_2;
						switch (num)
						{
						case 0:
							goto IL_1C0;
						case 2:
							if (A_2)
							{
								num = 8;
								continue;
							}
							goto IL_164;
						case 3:
							goto IL_1B4;
						case 4:
							goto IL_164;
						case 5:
						{
							List<TextSelection>.Enumerator enumerator;
							if (!enumerator.MoveNext())
							{
								num = 3;
								continue;
							}
							textSelection = enumerator.Current;
							a_2 = null;
							num = 2;
							continue;
						}
						case 7:
							if (!base.Document.ReplaceFirst)
							{
								num = 1;
								continue;
							}
							goto IL_1B4;
						case 8:
							a_2 = textSelection.StartTextRange.CharacterFormat;
							num = 4;
							continue;
						}
						IL_109:
						num = 5;
						continue;
						goto IL_109;
						IL_164:
						int a_3 = textSelection.ᜄ();
						Paragraph paragraph = textSelection.OwnerParagraph;
						A_1.ᜀ(paragraph.OwnerTextBody, paragraph.ឯ(), a_3, a_2, A_2);
						num = 7;
						continue;
						IL_1B4:
						num = 0;
					}
					IL_1C0:
					goto IL_81;
				}
				finally
				{
					List<TextSelection>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				return 0;
				IL_81:
				return spr_u226E.Count;
			}
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00010790 File Offset: 0x0000F790
		internal new int ᜀ(Regex A_0, IDocument A_1, bool A_2)
		{
			int a_ = 9;
			switch (0)
			{
			default:
			{
				int num = 0;
				spr\u226E spr_u226E;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_58;
					case 2:
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
							List<TextSelection>.Enumerator enumerator = spr_u226E.GetEnumerator();
							break;
						}
						}
						num = 4;
						continue;
					case 3:
						if (spr_u226E != null)
						{
							num = 2;
							continue;
						}
						goto IL_2BB;
					case 4:
						goto IL_6B;
					}
					if (spr\u1AB5.ᜀ(A_0))
					{
						num = 1;
					}
					else
					{
						CharacterFormat a_2 = null;
						spr_u226E = this.ᜁ(A_0);
						num = 3;
					}
				}
				IL_58:
				throw new ArgumentException(ClipboardData.b("㱮ᑰቲݴᑶᅸ孺๼୾ꦈﾐﲒ랖ﮘﺚ붜爵철펢톤\udea6", a_));
				IL_6B:
				try
				{
					num = 14;
					for (;;)
					{
						CharacterFormat a_2;
						TextSelection textSelection;
						ISection section;
						int num2;
						switch (num)
						{
						case 0:
							goto IL_1AF;
						case 1:
							goto IL_1AF;
						case 3:
							goto IL_29C;
						case 4:
							num = 10;
							continue;
						case 5:
							goto IL_1CE;
						case 6:
							if (!A_2)
							{
								num = 9;
								continue;
							}
							goto IL_251;
						case 7:
							goto IL_251;
						case 8:
						{
							List<TextSelection>.Enumerator enumerator;
							if (!enumerator.MoveNext())
							{
								num = 3;
								continue;
							}
							textSelection = enumerator.Current;
							num = 12;
							continue;
						}
						case 9:
							base.Document.CurClonedSection = (section as Section);
							num = 7;
							continue;
						case 10:
							if (!base.Document.ReplaceFirst)
							{
								num = 2;
								continue;
							}
							goto IL_29C;
						case 11:
							if (num2 < 0)
							{
								num = 4;
								continue;
							}
							section = A_1.Sections[num2];
							num = 6;
							continue;
						case 12:
							if (A_2)
							{
								num = 15;
								continue;
							}
							goto IL_1CE;
						case 13:
							goto IL_2A8;
						case 15:
							a_2 = textSelection.StartTextRange.CharacterFormat;
							num = 5;
							continue;
						}
						IL_122:
						num = 8;
						continue;
						goto IL_122;
						IL_1AF:
						num = 11;
						continue;
						IL_1CE:
						int a_3 = textSelection.ᜄ();
						Paragraph paragraph = textSelection.OwnerParagraph;
						num2 = A_1.Sections.Count - 1;
						num = 0;
						continue;
						IL_251:
						TextBodyPart textBodyPart = new TextBodyPart(base.Document);
						textBodyPart.ᜀ(section.Body, false);
						textBodyPart.ᜀ(paragraph.OwnerTextBody, paragraph.ឯ(), a_3, a_2, A_2);
						num2--;
						num = 1;
						continue;
						IL_29C:
						num = 13;
					}
					IL_2A8:
					goto IL_81;
				}
				finally
				{
					List<TextSelection>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				goto IL_2BB;
				IL_81:
				return spr_u226E.Count;
				IL_2BB:
				if (true)
				{
				}
				return 0;
			}
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00010A80 File Offset: 0x0000FA80
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
			Body body = (Body)base.CloneImpl();
			body.m_bodyItems = new BodyRegionCollection(body);
			this.ChildObjects.ᜀ(body.m_bodyItems);
			body.ᜀ = new ParagraphCollection(body.m_bodyItems);
			body.ᜁ = new TableCollection(body.m_bodyItems);
			return body;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00010B08 File Offset: 0x0000FB08
		internal override void CloneRelationsTo(Document doc, OwnerHolder nextOwner)
		{
			for (;;)
			{
				IL_3C:
				int num = 0;
				int count = this.ChildObjects.Count;
				int num2 = 2;
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
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
						{
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							DocumentObject documentObject = this.ChildObjects[num];
							documentObject.CloneRelationsTo(doc, nextOwner);
							num++;
							goto IL_85;
						}
						case 1:
							goto IL_56;
						case 2:
							goto IL_56;
						case 3:
							return;
						}
						goto IL_3C;
						IL_56:
						num2 = 0;
						continue;
					}
					IL_85:
					num2 = 1;
				}
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00010BB4 File Offset: 0x0000FBB4
		internal void ᜅ()
		{
			int num = 0;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 1:
					goto IL_BE;
				case 2:
					goto IL_1E5;
				case 3:
					goto IL_87;
				case 4:
					this.ᜀ.Clear();
					this.ᜀ = null;
					num = 16;
					continue;
				case 5:
					this.m_bodyItems.Clear();
					this.m_bodyItems = null;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AA;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 6:
					if (this.ᜂ != null)
					{
						num = 7;
						continue;
					}
					return;
				case 7:
					this.ᜂ.InnerList.Clear();
					this.ᜂ = null;
					num = 12;
					continue;
				case 8:
				{
					if (num2 >= this.m_bodyItems.Count)
					{
						num = 5;
						continue;
					}
					BodyRegion bodyRegion = this.m_bodyItems[num2];
					bodyRegion.Close();
					num2++;
					num = 14;
					continue;
				}
				case 9:
					this.ᜁ.Clear();
					this.ᜁ = null;
					num = 1;
					continue;
				case 10:
					if (this.m_bodyItems.Count > 0)
					{
						num = 11;
						continue;
					}
					goto IL_87;
				case 11:
					goto IL_AA;
				case 12:
					return;
				case 13:
					num = 10;
					continue;
				case 14:
					goto IL_1E5;
				case 15:
					if (this.ᜀ != null)
					{
						num = 4;
						continue;
					}
					goto IL_6A;
				case 16:
					goto IL_6A;
				case 17:
					if (this.ᜁ != null)
					{
						num = 9;
						continue;
					}
					goto IL_BE;
				}
				if (this.m_bodyItems != null)
				{
					num = 13;
					continue;
				}
				goto IL_87;
				IL_6A:
				num = 17;
				continue;
				IL_87:
				num = 15;
				continue;
				IL_AA:
				num2 = 0;
				num = 2;
				continue;
				IL_BE:
				num = 6;
				continue;
				IL_1E5:
				num = 8;
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00010DD0 File Offset: 0x0000FDD0
		internal void ᜂ(bool A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_B2:
					int num;
					BodyRegion bodyRegion;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_4CD:
						num = 33;
						break;
					default:
						if (false)
						{
						}
						bodyRegion = null;
						num2 = 0;
						num = 6;
						break;
					}
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_1E5;
						case 1:
							num = 22;
							continue;
						case 2:
							return;
						case 3:
							goto IL_E3;
						case 4:
							goto IL_3E3;
						case 5:
						{
							Table table = bodyRegion as Table;
							num = 11;
							continue;
						}
						case 6:
							goto IL_22B;
						case 7:
							if (bodyRegion.IsChangedPFormat)
							{
								num = 20;
								continue;
							}
							goto IL_13A;
						case 8:
							if (!A_0)
							{
								num = 31;
								continue;
							}
							goto IL_4CD;
						case 9:
						{
							Table table;
							if (table.\u1716 != null)
							{
								num = 21;
								continue;
							}
							goto IL_1C2;
						}
						case 10:
							goto IL_1B0;
						case 11:
							if (A_0)
							{
								num = 30;
								continue;
							}
							num = 28;
							continue;
						case 12:
						{
							bool flag = this.ᜀ(bodyRegion, A_0);
							num = 23;
							continue;
						}
						case 13:
							goto IL_13A;
						case 14:
							if (!A_0)
							{
								num = 18;
								continue;
							}
							goto IL_1C2;
						case 15:
							if (!bodyRegion.IsDeleteRevision)
							{
								num = 35;
								continue;
							}
							goto IL_E3;
						case 16:
							goto IL_22B;
						case 17:
							if (bodyRegion.IsChangedCFormat)
							{
								num = 3;
								continue;
							}
							goto IL_3E3;
						case 18:
							num = 9;
							continue;
						case 19:
						{
							Table table;
							table.DocxTableFormat.Format.ClearFormatting();
							table.DocxTableFormat = table.TrackTblFormat.ᜀ(table);
							table.FirstRow.RowFormat.ClearFormatting();
							table.FirstRow.RowFormat.ImportContainer(table.TrackTblFormat.Format);
							table.ᜐ = null;
							num = 26;
							continue;
						}
						case 20:
							bodyRegion.AcceptPChanges();
							num = 13;
							continue;
						case 21:
						{
							Table table;
							table.TableGrid.Clear();
							List<float>.Enumerator enumerator = table.TrackTableGrid.GetEnumerator();
							num = 32;
							continue;
						}
						case 22:
							if (this.ᜀ(bodyRegion))
							{
								num = 25;
								continue;
							}
							goto IL_1B0;
						case 23:
							if (bodyRegion is Table)
							{
								num = 5;
								continue;
							}
							goto IL_1C2;
						case 24:
							num = 15;
							continue;
						case 25:
							this.ChildObjects.RemoveAt(num2);
							num2--;
							num = 10;
							continue;
						case 26:
							goto IL_1E5;
						case 27:
						{
							bool flag;
							if (flag)
							{
								num = 1;
								continue;
							}
							goto IL_1B0;
						}
						case 28:
						{
							Table table;
							if (table.ᜐ != null)
							{
								num = 19;
								continue;
							}
							goto IL_1E5;
						}
						case 29:
							if (num2 >= this.m_bodyItems.Count)
							{
								num = 2;
								continue;
							}
							goto IL_2DC;
						case 30:
						{
							Table table;
							table.ᜐ = null;
							table.\u1716 = null;
							num = 0;
							continue;
						}
						case 31:
							this.ᜁ(bodyRegion);
							num = 37;
							continue;
						case 32:
						{
							Table table;
							try
							{
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 1:
									{
										List<float>.Enumerator enumerator;
										if (!enumerator.MoveNext())
										{
											num = 4;
											continue;
										}
										float item = enumerator.Current;
										table.TableGrid.Add(item);
										num = 3;
										continue;
									}
									case 2:
										goto IL_2C9;
									case 4:
										num = 2;
										continue;
									}
									IL_2A3:
									num = 1;
									continue;
									goto IL_2A3;
								}
								IL_2C9:
								goto IL_40B;
							}
							finally
							{
								List<float>.Enumerator enumerator;
								((IDisposable)enumerator).Dispose();
							}
							goto IL_2DC;
							IL_40B:
							table.\u1716 = null;
							num = 36;
							continue;
						}
						case 33:
							if (!bodyRegion.IsInsertRevision)
							{
								num = 24;
								continue;
							}
							goto IL_E3;
						case 34:
							if (!this.ᜀ(bodyRegion, A_0, ref num2))
							{
								num = 12;
								continue;
							}
							goto IL_1B0;
						case 35:
							if (true)
							{
							}
							num = 17;
							continue;
						case 36:
							goto IL_1C2;
						case 37:
							goto IL_10D;
						}
						goto IL_B2;
						IL_E3:
						bodyRegion.AcceptCChanges();
						num = 4;
						continue;
						IL_13A:
						bodyRegion.MakeChanges(A_0);
						num = 27;
						continue;
						IL_1B0:
						num2++;
						num = 16;
						continue;
						IL_1C2:
						num = 8;
						continue;
						IL_1E5:
						num = 14;
						continue;
						IL_22B:
						num = 29;
						continue;
						IL_2DC:
						bodyRegion = this.m_bodyItems[num2];
						num = 34;
						continue;
						IL_3E3:
						num = 7;
					}
					IL_10D:
					goto IL_4CD;
				}
				return;
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000112E4 File Offset: 0x000102E4
		private new bool ᜀ(BodyRegion A_0, bool A_1, ref int A_2)
		{
			int num = 9;
			for (;;)
			{
				bool flag;
				switch (num)
				{
				case 0:
					goto IL_19C;
				case 1:
					if (A_0 is Paragraph)
					{
						num = 14;
						continue;
					}
					goto IL_D1;
				case 2:
					if (flag)
					{
						num = 8;
						continue;
					}
					return false;
				case 3:
				{
					Table table = A_0 as Table;
					flag = table.ᜆ();
					num = 5;
					continue;
				}
				case 4:
					goto IL_AB;
				case 5:
					goto IL_D1;
				case 6:
					if (!A_1)
					{
						goto IL_15E;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_19C;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 7:
					num = 10;
					continue;
				case 8:
					goto IL_ED;
				case 10:
					if (A_1)
					{
						num = 0;
						continue;
					}
					return false;
				case 11:
					if (A_0 is Table)
					{
						num = 3;
						continue;
					}
					num = 1;
					continue;
				case 12:
					if (A_0.IsDeleteRevision)
					{
						num = 7;
						continue;
					}
					return false;
				case 13:
					goto IL_D1;
				case 14:
					if (true)
					{
					}
					flag = (A_0 as Paragraph).\u1713();
					num = 13;
					continue;
				case 15:
					num = 6;
					continue;
				}
				if (A_0.IsInsertRevision)
				{
					num = 15;
					continue;
				}
				IL_AB:
				num = 12;
				continue;
				IL_D1:
				num = 2;
				continue;
				IL_15E:
				flag = true;
				num = 11;
				continue;
				IL_19C:
				goto IL_15E;
			}
			IL_ED:
			this.ChildObjects.RemoveAt(A_2);
			A_2--;
			return true;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00011490 File Offset: 0x00010490
		internal bool ᜑ()
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				IEnumerator enumerator = this.m_bodyItems.GetEnumerator();
				bool result;
				try
				{
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_B5:
						num = 4;
						break;
					default:
						if (false)
						{
						}
						num = 6;
						break;
					}
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_CB;
						case 1:
						{
							if (!enumerator.MoveNext())
							{
								num = 5;
								continue;
							}
							BodyRegion bodyRegion = (BodyRegion)enumerator.Current;
							num = 3;
							continue;
						}
						case 2:
							goto IL_D6;
						case 3:
						{
							BodyRegion bodyRegion;
							if (bodyRegion.HasTrackedChanges())
							{
								goto IL_B5;
							}
							break;
						}
						case 4:
							result = true;
							num = 0;
							continue;
						case 5:
							num = 2;
							continue;
						}
						IL_7C:
						num = 1;
						continue;
						goto IL_7C;
					}
					IL_CB:
					return result;
					IL_D6:
					return false;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_11D;
							case 1:
								if (disposable != null)
								{
									num = 2;
									continue;
								}
								goto IL_11F;
							case 2:
								disposable.Dispose();
								num = 0;
								continue;
							}
							break;
						}
					}
					IL_11D:
					IL_11F:;
				}
				return result;
			}
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000115D0 File Offset: 0x000105D0
		private void ᜁ(BodyRegion A_0)
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
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						A_0.RemovePFormatChanges();
						num = 3;
						continue;
					case 1:
						goto IL_66;
					case 2:
						A_0.RemoveCFormatChanges();
						num = 1;
						continue;
					case 3:
						return;
					case 4:
						if (A_0.IsChangedPFormat)
						{
							num = 0;
							continue;
						}
						return;
					}
					if (A_0.IsChangedCFormat)
					{
						num = 2;
						continue;
					}
					IL_66:
					if (true)
					{
					}
					num = 4;
				}
				break;
			}
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00011680 File Offset: 0x00010680
		private new bool ᜀ(BodyRegion A_0, bool A_1)
		{
			bool result;
			for (;;)
			{
				result = false;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_0.NextSibling is Paragraph)
						{
							num = 12;
							continue;
						}
						goto IL_117;
					case 1:
						if (A_0 is Paragraph)
						{
							num = 5;
							continue;
						}
						goto IL_117;
					case 2:
						num = 7;
						continue;
					case 3:
						goto IL_5B;
					case 4:
						if (A_0.IsInsertRevision)
						{
							num = 2;
							continue;
						}
						goto IL_F4;
					case 5:
						num = 0;
						continue;
					case 6:
						goto IL_F4;
					case 7:
						if (A_1)
						{
							num = 6;
							continue;
						}
						goto IL_BB;
					case 8:
						goto IL_BB;
					case 9:
						if (A_0.IsDeleteRevision)
						{
							num = 3;
							continue;
						}
						goto IL_117;
					case 10:
						if (true)
						{
						}
						if (A_1)
						{
							num = 8;
							continue;
						}
						goto IL_117;
					case 11:
						goto IL_117;
					case 12:
						num = 4;
						continue;
					}
					break;
					IL_5B:
					num = 10;
					continue;
					IL_117:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5B;
					default:
						goto IL_12D;
					}
					IL_BB:
					result = true;
					num = 11;
					continue;
					IL_F4:
					num = 9;
				}
			}
			IL_12D:
			if (false)
			{
			}
			return result;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000117C4 File Offset: 0x000107C4
		private new bool ᜀ(BodyRegion A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					for (;;)
					{
						Paragraph paragraph = A_0 as Paragraph;
						int num = paragraph.Items.Count - 1;
						Paragraph paragraph2 = A_0.NextSibling as Paragraph;
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								if (paragraph2 == null)
								{
									num2 = 5;
									continue;
								}
								if (true)
								{
								}
								int num3 = num;
								num2 = 3;
								continue;
							}
							case 1:
								goto IL_C4;
							case 2:
							{
								int num3;
								if (num3 < 0)
								{
									num2 = 1;
									continue;
								}
								paragraph2.Items.Insert(0, paragraph.Items[num3]);
								num3--;
								num2 = 4;
								continue;
							}
							case 3:
								goto IL_A8;
							case 4:
								goto IL_A8;
							case 5:
								return false;
							}
							break;
							IL_A8:
							num2 = 2;
						}
					}
					IL_C4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_DC;
					}
				}
				return false;
				IL_DC:
				if (false)
				{
				}
				return true;
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000118C0 File Offset: 0x000108C0
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
			base.XDLSHolder.AddElement(ClipboardData.b("ᡧ୩ṫ཭ᝯqᕳٵၷॹ", a_), this.m_bodyItems);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00011924 File Offset: 0x00010924
		protected override void CreateLayoutInfo()
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
			this.ᜀ = new spr\u22A8(ChildrenLayoutDirection.Vertical);
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0001196C File Offset: 0x0001096C
		protected override IDocumentObjectCollection WidgetCollection
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
				return this.m_bodyItems;
			}
		}

		// Token: 0x04000980 RID: 2432
		protected BodyRegionCollection m_bodyItems;

		// Token: 0x04000981 RID: 2433
		private new ParagraphCollection ᜀ;

		// Token: 0x04000982 RID: 2434
		private bool \u2593\u0093\u00A3\u0088;

		// Token: 0x04000983 RID: 2435
		private long[] \u2609\u0089\u009E\u00A5;

		// Token: 0x04000984 RID: 2436
		private TableCollection ᜁ;

		// Token: 0x04000985 RID: 2437
		private long[] \u2460\u0083\u00A8\u0082;

		// Token: 0x04000986 RID: 2438
		private byte \u25D8\u00AE\u0094\u008F;

		// Token: 0x04000987 RID: 2439
		private FormFieldCollection ᜂ;
	}
}
