using System;
using System.Collections;
using Spire.CompoundFile.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Interface;

namespace Spire.Doc.Fields
{
	// Token: 0x0200051B RID: 1307
	public abstract class FormField : Field
	{
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060043ED RID: 17389 RVA: 0x003F8D84 File Offset: 0x003F7D84
		public FormFieldType FormFieldType
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
				return this.m_curFormFieldType;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060043EE RID: 17390 RVA: 0x003F8DC8 File Offset: 0x003F7DC8
		// (set) Token: 0x060043EF RID: 17391 RVA: 0x003F8E0C File Offset: 0x003F7E0C
		public string Name
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜂ(this.ᜂ, value);
				this.ᜂ = value;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060043F0 RID: 17392 RVA: 0x003F8E5C File Offset: 0x003F7E5C
		// (set) Token: 0x060043F1 RID: 17393 RVA: 0x003F8EA0 File Offset: 0x003F7EA0
		public string Help
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
				this.ᜁ = (short)spr\u23F8.ᜀ((int)this.ᜁ, 128, 7, 1);
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060043F2 RID: 17394 RVA: 0x003F8EFC File Offset: 0x003F7EFC
		// (set) Token: 0x060043F3 RID: 17395 RVA: 0x003F8F40 File Offset: 0x003F7F40
		public string StatusBarHelp
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
				this.ᜁ = (short)spr\u23F8.ᜀ((int)this.ᜁ, 256, 8, 1);
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060043F4 RID: 17396 RVA: 0x003F8F9C File Offset: 0x003F7F9C
		// (set) Token: 0x060043F5 RID: 17397 RVA: 0x003F8FE0 File Offset: 0x003F7FE0
		public string MacroOnStart
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

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060043F6 RID: 17398 RVA: 0x003F9024 File Offset: 0x003F8024
		// (set) Token: 0x060043F7 RID: 17399 RVA: 0x003F9068 File Offset: 0x003F8068
		public string MacroOnEnd
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
				return this.ᜆ;
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
				this.ᜆ = value;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060043F8 RID: 17400 RVA: 0x003F90AC File Offset: 0x003F80AC
		// (set) Token: 0x060043F9 RID: 17401 RVA: 0x003F90F4 File Offset: 0x003F80F4
		internal int InnerValue
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
				return (this.ᜁ & 124) >> 2;
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
				this.ᜁ = (short)((int)(this.ᜁ & -125) | value << 2);
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060043FA RID: 17402 RVA: 0x003F9144 File Offset: 0x003F8144
		// (set) Token: 0x060043FB RID: 17403 RVA: 0x003F9188 File Offset: 0x003F8188
		internal int Params
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
				return (int)this.ᜁ;
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
				this.ᜁ = (short)value;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060043FC RID: 17404 RVA: 0x003F91CC File Offset: 0x003F81CC
		// (set) Token: 0x060043FD RID: 17405 RVA: 0x003F9218 File Offset: 0x003F8218
		public bool Enabled
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
				return (this.ᜁ & 512) == 0;
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_5B;
						case 1:
							if (true)
							{
							}
							num = 2;
							continue;
						case 2:
							goto IL_6E;
						}
						if (!value)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								num = 1;
								break;
							}
						}
						else
						{
							num = 0;
						}
					}
				}
				IL_5B:
				int num2 = 0;
				goto IL_71;
				IL_6E:
				num2 = 1;
				IL_71:
				int a_ = num2;
				this.ᜁ = (short)spr\u23F8.ᜀ((int)this.ᜁ, 512, 9, a_);
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060043FE RID: 17406 RVA: 0x003F92B4 File Offset: 0x003F82B4
		// (set) Token: 0x060043FF RID: 17407 RVA: 0x003F9304 File Offset: 0x003F8304
		public bool CalculateOnExit
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
				return (this.ᜁ & 16384) == 16384;
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
				this.ᜁ = (value ? ((short)spr\u23F8.ᜀ((int)this.ᜁ, 16384, 14, 1)) : ((short)spr\u23F8.ᜀ((int)this.ᜁ, 16384, 14, 0)));
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06004400 RID: 17408 RVA: 0x003F9378 File Offset: 0x003F8378
		// (set) Token: 0x06004401 RID: 17409 RVA: 0x003F93BC File Offset: 0x003F83BC
		internal bool HasFFData
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
				return this.ᜇ;
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
				this.ᜇ = value;
			}
		}

		// Token: 0x06004402 RID: 17410 RVA: 0x003F9400 File Offset: 0x003F8400
		public FormField(IDocument doc) : base(doc)
		{
			this.m_paraItemType = ParagraphItemType.FormField;
			this.ᜂ = string.Empty;
			this.ᜃ = string.Empty;
			this.ᜄ = string.Empty;
			this.ᜅ = string.Empty;
			this.ᜆ = string.Empty;
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x003F945C File Offset: 0x003F845C
		protected FormField(FormField formField, IDocument doc) : this(doc)
		{
			this.Help = formField.Help;
			this.MacroOnEnd = formField.MacroOnEnd;
			this.MacroOnStart = formField.MacroOnStart;
			this.Params = formField.Params;
			this.Name = formField.Name;
			this.StatusBarHelp = formField.StatusBarHelp;
			this.InnerValue = formField.InnerValue;
			base.Type = formField.Type;
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x003F94D0 File Offset: 0x003F84D0
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
			FormField formField = (FormField)base.CloneImpl();
			formField.CharacterFormat.ImportContainer(base.CharacterFormat);
			return formField;
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x003F952C File Offset: 0x003F852C
		internal override void Attach(Paragraph paragraph, int itemPos)
		{
			for (;;)
			{
				base.Attach(paragraph, itemPos);
				int num = 1;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_68;
					case 1:
						if (!base.Document.ᜇ)
						{
							num = 2;
							continue;
						}
						goto IL_68;
					case 2:
						IL_41:
						this.ᜁ(paragraph.Owner as Body);
						num = 0;
						continue;
					}
					break;
					IL_68:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_41;
					default:
						goto IL_7E;
					}
				}
			}
			IL_7E:
			if (false)
			{
			}
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x003F95C0 File Offset: 0x003F85C0
		internal override void Detach()
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
			base.Detach();
			this.ᜀ(base.OwnerParagraph.Owner as Body);
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x003F9618 File Offset: 0x003F8618
		private new void ᜁ(Body A_0)
		{
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0 is TableCell)
					{
						num = 3;
						continue;
					}
					goto IL_FD;
				case 1:
					if (A_0.IsFormFieldsCreated)
					{
						num = 2;
						continue;
					}
					goto IL_FD;
				case 2:
					A_0.FormFields.ᜁ(this);
					num = 0;
					continue;
				case 3:
				{
					Table table = A_0.Owner.Owner as Table;
					num = 4;
					continue;
				}
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_42;
					default:
					{
						if (false)
						{
						}
						Table table;
						if (table != null)
						{
							num = 5;
							continue;
						}
						goto IL_FD;
					}
					}
					break;
				case 5:
				{
					Table table;
					this.ᜁ(table.Owner as Body);
					num = 8;
					continue;
				}
				case 7:
					goto IL_42;
				case 8:
					goto IL_FB;
				}
				if (A_0 != null)
				{
					num = 7;
					continue;
				}
				break;
				IL_42:
				num = 1;
			}
			IL_FB:
			IL_FD:
			if (true)
			{
			}
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x003F972C File Offset: 0x003F872C
		private new void ᜀ(Body A_0)
		{
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					this.ᜀ(A_0.Owner.Owner.Owner as Body);
					num = 1;
					continue;
				case 1:
					return;
				case 2:
					goto IL_5C;
				case 3:
					A_0.FormFields.ᜀ(this);
					num = 2;
					continue;
				case 4:
					if (A_0 is TableCell)
					{
						num = 0;
						continue;
					}
					return;
				case 5:
					if (A_0.IsFormFieldsCreated)
					{
						num = 3;
						continue;
					}
					goto IL_5C;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						break;
					}
					num = 5;
					continue;
				}
				if (A_0 != null)
				{
					num = 7;
					continue;
				}
				break;
				IL_5C:
				num = 4;
			}
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x003F9824 File Offset: 0x003F8824
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 4;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜄ = reader.ReadString(ClipboardData.b("㹩ͫŭᱯٱᵳٵ", a_));
						num = 6;
						continue;
					case 1:
						if (reader.HasAttribute(ClipboardData.b("㹩իᩭᱯ᝱", a_)))
						{
							num = 11;
							continue;
						}
						goto IL_279;
					case 2:
						this.Params = reader.ReadInt(ClipboardData.b("㩩൫ᱭᅯάݳ", a_));
						num = 14;
						continue;
					case 3:
						this.ᜅ = reader.ReadString(ClipboardData.b("❩൫൭ɯᵱ㭳ᡵ⭷๹ᵻ౽", a_));
						num = 10;
						continue;
					case 4:
						if (reader.HasAttribute(ClipboardData.b("㩩൫ᱭᅯάݳ", a_)))
						{
							num = 2;
							continue;
						}
						goto IL_F5;
					case 5:
						if (reader.HasAttribute(ClipboardData.b("≩५ɭo", a_)))
						{
							num = 12;
							continue;
						}
						goto IL_BC;
					case 6:
						goto IL_8B;
					case 7:
						if (reader.HasAttribute(ClipboardData.b("❩൫൭ɯᵱ㭳ᡵ㵷ᑹ᡻", a_)))
						{
							num = 13;
							continue;
						}
						return;
					case 8:
						if (reader.HasAttribute(ClipboardData.b("❩൫൭ɯᵱ㭳ᡵ⭷๹ᵻ౽", a_)))
						{
							num = 3;
							continue;
						}
						goto IL_129;
					case 9:
						return;
					case 10:
						goto IL_129;
					case 11:
						this.ᜂ = reader.ReadString(ClipboardData.b("㹩իᩭᱯ᝱", a_));
						num = 16;
						continue;
					case 12:
						this.ᜃ = reader.ReadString(ClipboardData.b("≩५ɭo", a_));
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_158;
						default:
							if (false)
							{
							}
							num = 15;
							continue;
						}
						break;
					case 13:
						goto IL_158;
					case 14:
						goto IL_F5;
					case 15:
						goto IL_BC;
					case 16:
						goto IL_279;
					case 17:
						if (reader.HasAttribute(ClipboardData.b("㹩ͫŭᱯٱᵳٵ", a_)))
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						goto IL_8B;
					}
					break;
					IL_8B:
					num = 8;
					continue;
					IL_BC:
					num = 17;
					continue;
					IL_F5:
					num = 1;
					continue;
					IL_129:
					num = 7;
					continue;
					IL_158:
					this.ᜆ = reader.ReadString(ClipboardData.b("❩൫൭ɯᵱ㭳ᡵ㵷ᑹ᡻", a_));
					num = 9;
					continue;
					IL_279:
					num = 5;
				}
			}
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x003F9AE0 File Offset: 0x003F8AE0
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 5;
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
			writer.WriteValue(ClipboardData.b("㭪౬ᵮၰṲٴ", a_), (int)this.ᜁ);
			writer.WriteValue(ClipboardData.b("㽪Ѭ᭮ᵰᙲ", a_), this.ᜂ);
			writer.WriteValue(ClipboardData.b("⍪࡬ͮŰ", a_), this.ᜃ);
			writer.WriteValue(ClipboardData.b("㽪ɬnᵰݲᱴݶ", a_), this.ᜄ);
			writer.WriteValue(ClipboardData.b("♪౬౮Ͱᱲ㩴᥶⩸ེᱼൾ", a_), this.ᜅ);
			writer.WriteValue(ClipboardData.b("♪౬౮Ͱᱲ㩴᥶㱸ᕺ᥼", a_), this.ᜆ);
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x003F9BC8 File Offset: 0x003F8BC8
		private void ᜂ(string A_0, string A_1)
		{
			int num = 5;
			for (;;)
			{
				bool flag;
				switch (num)
				{
				case 0:
					flag = this.ᜁ(A_0, A_1);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_13B;
					default:
						if (false)
						{
						}
						num = 10;
						continue;
					}
					break;
				case 1:
					if (base.Document.ᜇ)
					{
						num = 16;
						continue;
					}
					goto IL_171;
				case 2:
					base.OwnerParagraph.OwnerTextBody.FormFields.ᜀ(A_0, A_1);
					num = 14;
					continue;
				case 3:
					this.ᜀ(A_0, A_1);
					num = 9;
					continue;
				case 4:
					if (base.OwnerParagraph != null)
					{
						num = 13;
						continue;
					}
					return;
				case 6:
					if (base.OwnerParagraph.OwnerTextBody.IsFormFieldsCreated)
					{
						num = 2;
						continue;
					}
					return;
				case 7:
					num = 1;
					continue;
				case 8:
					if (!flag)
					{
						num = 3;
						continue;
					}
					goto IL_69;
				case 9:
					goto IL_69;
				case 10:
					goto IL_8B;
				case 11:
					num = 6;
					continue;
				case 12:
					if (base.OwnerParagraph.OwnerTextBody != null)
					{
						num = 11;
						continue;
					}
					return;
				case 13:
					if (true)
					{
					}
					goto IL_13B;
				case 14:
					return;
				case 15:
					if (base.Document != null)
					{
						num = 0;
						continue;
					}
					goto IL_8B;
				case 16:
					return;
				}
				if (base.Document != null)
				{
					num = 7;
					continue;
				}
				goto IL_171;
				IL_69:
				num = 12;
				continue;
				IL_8B:
				num = 8;
				continue;
				IL_13B:
				this.ᜀ(A_1);
				flag = false;
				num = 15;
				continue;
				IL_171:
				num = 4;
			}
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x003F9DA4 File Offset: 0x003F8DA4
		private new bool ᜁ(string A_0, string A_1)
		{
			bool result;
			for (;;)
			{
				result = false;
				BookmarkCollection bookmarks = base.Document.Bookmarks;
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (bookmarks.Count > 0)
						{
							goto IL_56;
						}
						return result;
					case 1:
					{
						Bookmark bookmark;
						if (bookmark.BookmarkStart != null)
						{
							num = 3;
							continue;
						}
						return result;
					}
					case 2:
					{
						Bookmark bookmark;
						bookmark.BookmarkStart.ᜀ(A_1);
						bookmark.BookmarkEnd.ᜀ(A_1);
						result = true;
						num = 5;
						continue;
					}
					case 3:
						num = 7;
						continue;
					case 4:
					{
						Bookmark bookmark = bookmarks[A_0];
						num = 8;
						continue;
					}
					case 5:
						return result;
					case 6:
						num = 1;
						continue;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_56;
						default:
						{
							if (false)
							{
							}
							Bookmark bookmark;
							if (bookmark.BookmarkEnd != null)
							{
								num = 2;
								continue;
							}
							return result;
						}
						}
						break;
					case 8:
					{
						Bookmark bookmark;
						if (bookmark != null)
						{
							num = 6;
							continue;
						}
						return result;
					}
					}
					break;
					IL_56:
					num = 4;
				}
			}
			return result;
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x003F9EC0 File Offset: 0x003F8EC0
		private new void ᜀ(string A_0, string A_1)
		{
			switch (0)
			{
			default:
				if (true)
				{
				}
				for (;;)
				{
					BookmarkStart bookmarkStart = null;
					BookmarkEnd bookmarkEnd = null;
					IEnumerator enumerator = base.OwnerParagraph.Items.GetEnumerator();
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							try
							{
								num = 12;
								for (;;)
								{
									switch (num)
									{
									case 1:
										num = 14;
										continue;
									case 2:
										if (bookmarkStart == null)
										{
											num = 0;
											continue;
										}
										goto IL_212;
									case 3:
										goto IL_212;
									case 5:
										num = 8;
										continue;
									case 6:
										if (enumerator.MoveNext())
										{
											IParagraphBase paragraphBase = (IParagraphBase)enumerator.Current;
											num = 7;
											continue;
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
											num = 3;
											continue;
										}
										break;
									case 7:
									{
										IParagraphBase paragraphBase;
										if (paragraphBase is BookmarkStart)
										{
											num = 5;
											continue;
										}
										goto IL_1BA;
									}
									case 8:
									{
										IParagraphBase paragraphBase;
										if ((paragraphBase as BookmarkStart).Name == A_0)
										{
											num = 11;
											continue;
										}
										goto IL_1BA;
									}
									case 9:
									{
										IParagraphBase paragraphBase;
										bookmarkEnd = (paragraphBase as BookmarkEnd);
										num = 2;
										continue;
									}
									case 10:
										goto IL_21E;
									case 11:
									{
										IParagraphBase paragraphBase;
										bookmarkStart = (paragraphBase as BookmarkStart);
										num = 4;
										continue;
									}
									case 13:
									{
										IParagraphBase paragraphBase;
										if (paragraphBase is BookmarkEnd)
										{
											num = 1;
											continue;
										}
										break;
									}
									case 14:
									{
										IParagraphBase paragraphBase;
										if ((paragraphBase as BookmarkEnd).Name == A_0)
										{
											num = 9;
											continue;
										}
										break;
									}
									}
									IL_112:
									num = 6;
									continue;
									goto IL_112;
									IL_1BA:
									num = 13;
									continue;
									IL_212:
									num = 10;
								}
								IL_21E:
								goto IL_7E;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable = enumerator as IDisposable;
									num = 1;
									for (;;)
									{
										switch (num)
										{
										case 0:
											disposable.Dispose();
											num = 2;
											continue;
										case 1:
											if (disposable != null)
											{
												num = 0;
												continue;
											}
											goto IL_26A;
										case 2:
											goto IL_268;
										}
										break;
									}
								}
								IL_268:
								IL_26A:;
							}
							goto IL_26B;
							IL_7E:
							num = 4;
							continue;
						case 1:
							if (bookmarkEnd != null)
							{
								num = 3;
								continue;
							}
							return;
						case 2:
							num = 1;
							continue;
						case 3:
							goto IL_26B;
						case 4:
							if (bookmarkStart != null)
							{
								num = 2;
								continue;
							}
							return;
						case 5:
							return;
						}
						break;
						IL_26B:
						bookmarkStart.ᜀ(A_1);
						bookmarkEnd.ᜀ(A_1);
						num = 5;
					}
				}
				return;
			}
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x003FA170 File Offset: 0x003F9170
		private new void ᜀ(string A_0)
		{
			int a_ = 9;
			switch (0)
			{
			default:
				for (;;)
				{
					Bookmark bookmark = base.Document.Bookmarks[A_0];
					int num = 2;
					for (;;)
					{
						IEnumerator enumerator;
						switch (num)
						{
						case 0:
							goto IL_56;
						case 1:
							try
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										num = 7;
										continue;
									case 2:
									{
										Section section;
										if (section.Body.FormFields != null)
										{
											num = 6;
											continue;
										}
										break;
									}
									case 3:
									{
										Section section;
										if (section.Body.FormFields.ContainsName(A_0))
										{
											num = 5;
											continue;
										}
										break;
									}
									case 4:
										if (!enumerator.MoveNext())
										{
											num = 0;
											continue;
										}
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_F7;
										default:
										{
											if (false)
											{
											}
											Section section = (Section)enumerator.Current;
											num = 2;
											continue;
										}
										}
										break;
									case 5:
										goto IL_14A;
									case 6:
										num = 3;
										continue;
									case 7:
										goto IL_158;
									}
									IL_D8:
									num = 4;
									continue;
									goto IL_D8;
								}
								IL_F7:
								throw new ArgumentException(ClipboardData.b("ŮၰṲၴ坶學", a_) + A_0 + ClipboardData.b("䵮兰ᝲtݶᕸቺṼṾꮄ", a_));
								IL_14A:
								goto IL_F7;
								IL_158:
								return;
							}
							finally
							{
								for (;;)
								{
									if (true)
									{
									}
									IDisposable disposable = enumerator as IDisposable;
									num = 1;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_1A7;
										case 1:
											if (disposable != null)
											{
												num = 2;
												continue;
											}
											goto IL_1A9;
										case 2:
											disposable.Dispose();
											num = 0;
											continue;
										}
										break;
									}
								}
								IL_1A7:
								IL_1A9:;
							}
							goto IL_1AA;
						case 2:
							if (bookmark != null)
							{
								num = 0;
								continue;
							}
							goto IL_1AA;
						}
						break;
						IL_1AA:
						enumerator = base.Document.Sections.GetEnumerator();
						num = 1;
					}
				}
				IL_56:
				throw new ArgumentException(ClipboardData.b("ŮၰṲၴ坶學", a_) + A_0 + ClipboardData.b("䵮兰ᝲtݶᕸቺṼṾꮄ", a_));
			}
		}

		// Token: 0x040035B1 RID: 13745
		internal new const int ᜀ = 25;

		// Token: 0x040035B2 RID: 13746
		private long[] \u2609\u008F\u00A2\u009D;

		// Token: 0x040035B3 RID: 13747
		private int \u2593\u009F\u009E\u0083;

		// Token: 0x040035B4 RID: 13748
		private string[] \u2609\u008D\u008E\u009F;

		// Token: 0x040035B5 RID: 13749
		protected FormFieldType m_curFormFieldType;

		// Token: 0x040035B6 RID: 13750
		private new short ᜁ;

		// Token: 0x040035B7 RID: 13751
		private string ᜂ;

		// Token: 0x040035B8 RID: 13752
		private new string ᜃ;

		// Token: 0x040035B9 RID: 13753
		private bool[] \u2593\u00A3\u00A1\u009C;

		// Token: 0x040035BA RID: 13754
		private new string ᜄ;

		// Token: 0x040035BB RID: 13755
		private string \u2460\u0099\u00A5\u0096;

		// Token: 0x040035BC RID: 13756
		private string ᜅ;

		// Token: 0x040035BD RID: 13757
		private byte \u25D8\u008F\u009E\u008B;

		// Token: 0x040035BE RID: 13758
		private string ᜆ;

		// Token: 0x040035BF RID: 13759
		private bool ᜇ = true;
	}
}
