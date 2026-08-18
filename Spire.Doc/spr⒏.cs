using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Fields.Shape;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;
using Spire.Layouting;

// Token: 0x0200018E RID: 398
internal class spr\u248F : spr\u1937, spr\u2297
{
	// Token: 0x06000EE6 RID: 3814 RVA: 0x000EBF50 File Offset: 0x000EAF50
	public override DocumentObjectType ᜁ()
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
		return DocumentObjectType.Shape;
	}

	// Token: 0x06000EE7 RID: 3815 RVA: 0x000EBF90 File Offset: 0x000EAF90
	internal sprᨼ ᜏ()
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

	// Token: 0x06000EE8 RID: 3816 RVA: 0x000EBFD4 File Offset: 0x000EAFD4
	internal new void ᜀ(sprᨼ A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x000EC018 File Offset: 0x000EB018
	internal TextBoxItemCollection ᜎ()
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

	// Token: 0x06000EEA RID: 3818 RVA: 0x000EC05C File Offset: 0x000EB05C
	internal bool \u1713()
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

	// Token: 0x06000EEB RID: 3819 RVA: 0x000EC0A0 File Offset: 0x000EB0A0
	internal new void ᜁ(bool A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06000EEC RID: 3820 RVA: 0x000EC0E4 File Offset: 0x000EB0E4
	internal CharacterFormat ᜌ()
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

	// Token: 0x06000EED RID: 3821 RVA: 0x000EC128 File Offset: 0x000EB128
	[CompilerGenerated]
	internal DocumentObject \u1712()
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

	// Token: 0x06000EEE RID: 3822 RVA: 0x000EC16C File Offset: 0x000EB16C
	[CompilerGenerated]
	internal new void ᜀ(DocumentObject A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x06000EEF RID: 3823 RVA: 0x000EC1B0 File Offset: 0x000EB1B0
	[CompilerGenerated]
	internal PointF \u1714()
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

	// Token: 0x06000EF0 RID: 3824 RVA: 0x000EC1F4 File Offset: 0x000EB1F4
	[CompilerGenerated]
	internal new void ᜀ(PointF A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x06000EF1 RID: 3825 RVA: 0x000EC238 File Offset: 0x000EB238
	internal spr\u248F(IDocument A_0) : base((Document)A_0)
	{
		this.ᜀ = new sprᨼ();
		this.ᜁ = new TextBoxItemCollection(A_0);
		this.m_charFormat = new CharacterFormat(A_0);
		base.ᜀ(Spire.Doc.Fields.Shape.ShapeType.Group);
	}

	// Token: 0x06000EF2 RID: 3826 RVA: 0x000EC27C File Offset: 0x000EB27C
	internal spr\u248F(IDocument A_0, Spire.Doc.Fields.Shape.ShapeType A_1) : base((Document)A_0)
	{
		this.ᜀ = new sprᨼ();
		this.ᜁ = new TextBoxItemCollection(A_0);
		this.m_charFormat = new CharacterFormat(A_0);
		base.ᜀ(A_1);
	}

	// Token: 0x06000EF3 RID: 3827 RVA: 0x000EC2C0 File Offset: 0x000EB2C0
	internal override void ᜀ(Paragraph A_0, int A_1)
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
		base.Attach(A_0, A_1);
	}

	// Token: 0x06000EF4 RID: 3828 RVA: 0x000EC304 File Offset: 0x000EB304
	internal override void ᜀ(Document A_0, OwnerHolder A_1)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			IEnumerator enumerator = this.ᜎ().GetEnumerator();
			try
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_180;
					case 1:
						try
						{
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									num = 3;
									continue;
								case 2:
								{
									IEnumerator enumerator2;
									if (!enumerator2.MoveNext())
									{
										num = 0;
										continue;
									}
									DocumentObject documentObject = (DocumentObject)enumerator2.Current;
									documentObject.CloneRelationsTo(A_0, A_1);
									documentObject.ᜀ(A_0);
									num = 4;
									continue;
								}
								case 3:
									goto IL_127;
								}
								IL_DC:
								num = 2;
								continue;
								goto IL_DC;
							}
							IL_127:
							break;
						}
						finally
						{
							for (;;)
							{
								IEnumerator enumerator2;
								IDisposable disposable = enumerator2 as IDisposable;
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 0:
										if (disposable != null)
										{
											num = 2;
											continue;
										}
										goto IL_173;
									case 1:
										goto IL_171;
									case 2:
										disposable.Dispose();
										num = 1;
										continue;
									}
									break;
								}
							}
							IL_171:
							IL_173:;
						}
						goto IL_174;
					case 2:
					{
						if (!enumerator.MoveNext())
						{
							num = 4;
							continue;
						}
						TextBox textBox = (TextBox)enumerator.Current;
						IEnumerator enumerator2 = textBox.ChildObjects.GetEnumerator();
						num = 1;
						continue;
					}
					case 4:
						goto IL_174;
					}
					IL_79:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_174:
						num = 0;
						continue;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					goto IL_79;
				}
				IL_180:;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable2 = enumerator as IDisposable;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (disposable2 != null)
							{
								num = 1;
								continue;
							}
							goto IL_1C9;
						case 1:
							disposable2.Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_1C7;
						}
						break;
					}
				}
				IL_1C7:
				IL_1C9:;
			}
			base.Document.ᜀ(A_0, this);
			this.ᜁ = false;
			return;
		}
		}
	}

	// Token: 0x06000EF5 RID: 3829 RVA: 0x000EC524 File Offset: 0x000EB524
	protected virtual object ᜃ()
	{
		spr\u248F spr_u248F;
		for (;;)
		{
			for (;;)
			{
				spr_u248F = (spr\u248F)base.CloneImpl();
				spr_u248F.ᜁ = new TextBoxItemCollection(base.Document);
				this.ᜁ.ᜀ(spr_u248F.ᜁ);
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A9;
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
							if (this.ᜏ() != null)
							{
								num = 2;
								continue;
							}
							goto IL_AB;
						}
						break;
					case 2:
						spr_u248F.ᜀ = this.ᜏ().\u1718();
						num = 0;
						continue;
					}
					break;
				}
			}
		}
		IL_A9:
		IL_AB:
		spr_u248F.ᜁ = true;
		return spr_u248F;
	}

	// Token: 0x06000EF6 RID: 3830 RVA: 0x000EC5E4 File Offset: 0x000EB5E4
	protected override void ᜂ()
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
		this.ᜀ.ᜁ(false);
		this.ᜀ.ᜀ(false);
	}

	// Token: 0x06000EF7 RID: 3831 RVA: 0x000EC644 File Offset: 0x000EB644
	void spr\u1AB8.ᜀ(spr\u19E0 A_0, sprᦰ A_1)
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
		A_0.ᜁ(this, A_1);
	}

	// Token: 0x06000EF8 RID: 3832 RVA: 0x000EC688 File Offset: 0x000EB688
	protected virtual void ᜀ(IXDLSAttributeWriter A_0)
	{
		int a_ = 15;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		base.WriteXmlAttributes(A_0);
		A_0.WriteValue(ClipboardData.b("Ŵ๶ॸṺ", a_), ParagraphItemType.ShapeObject);
		A_0.WriteValue(ClipboardData.b("♴ὶᡸ୺᡼㙾얀", a_), this.ᜀ.ᜡ());
		A_0.WriteValue(ClipboardData.b("㱴Ѷ㭸Ṻᅼၾ힂ﾆﶈ", a_), this.ᜀ.\u1715());
		A_0.WriteValue(ClipboardData.b("㵴ᡶ୸ቺݼၾ욈力ﶒ", a_), this.ᜀ.\u1719());
		A_0.WriteValue(ClipboardData.b("⍴ቶ୸ེᑼ᱾쪄", a_), this.ᜀ.\u1714());
		A_0.WriteValue(ClipboardData.b("≴նᡸ୺ർᙾ횄", a_), this.ᜀ.\u1716());
		A_0.WriteValue(ClipboardData.b("≴նᡸ୺ർᙾ톄ﺆ麗", a_), this.ᜀ.\u171E());
		A_0.WriteValue(ClipboardData.b("㵴ᡶ୸ቺݼၾ\ud988ﺌ朗杖練", a_), this.ᜀ.\u1713());
		A_0.WriteValue(ClipboardData.b("⍴ቶ୸ེᑼ᱾햄愈歷ﺐﶒ", a_), this.ᜀ.ᜠ());
		A_0.WriteValue(ClipboardData.b("ⅴྲྀ᭸ͺ㹼ၾ", a_), this.ᜀ.\u1717());
		A_0.WriteValue(ClipboardData.b("㵴ቶၸᱺᕼ୾", a_), this.ᜀ.\u171F());
		A_0.WriteValue(ClipboardData.b("≴Ṷᵸེᕼ", a_), this.ᜀ.\u1712());
		A_0.WriteValue(ClipboardData.b("㱴ѶㅸṺᱼ᭾", a_), this.ᜂ);
	}

	// Token: 0x06000EF9 RID: 3833 RVA: 0x000EC874 File Offset: 0x000EB874
	protected virtual void ᜀ(IXDLSAttributeReader A_0)
	{
		int a_ = 7;
		for (;;)
		{
			base.ReadXmlAttributes(A_0);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_383;
				case 1:
					if (A_0.HasAttribute(ClipboardData.b("⑬ᱮ㥰ᙲᑴ፶ᱸॺ", a_)))
					{
						num = 19;
						continue;
					}
					return;
				case 2:
					if (A_0.HasAttribute(ClipboardData.b("㹬ݮၰͲၴ㹶㵸", a_)))
					{
						num = 12;
						continue;
					}
					goto IL_540;
				case 3:
					this.ᜀ.ᜅ(A_0.ReadInt(ClipboardData.b("㥬ᝮ፰୲㙴ᡶ౸ᕺॼ", a_)));
					num = 13;
					continue;
				case 4:
					goto IL_260;
				case 5:
					return;
				case 6:
					if (A_0.HasAttribute(ClipboardData.b("㥬ᝮ፰୲㙴ᡶ౸ᕺॼ", a_)))
					{
						num = 3;
						continue;
					}
					goto IL_574;
				case 7:
					goto IL_219;
				case 8:
					goto IL_185;
				case 9:
					if (A_0.HasAttribute(ClipboardData.b("㭬੮Ͱݲᱴᑶᡸ᝺⵼ၾ", a_)))
					{
						num = 29;
						continue;
					}
					goto IL_1E5;
				case 10:
					this.ᜀ.ᜆ(A_0.ReadInt(ClipboardData.b("㩬ٮᕰݲᵴ", a_)));
					num = 7;
					continue;
				case 11:
					if (A_0.HasAttribute(ClipboardData.b("㭬੮Ͱݲᱴᑶᡸ᝺㉼ൾ", a_)))
					{
						num = 33;
						continue;
					}
					goto IL_40E;
				case 12:
					this.ᜀ.ᜄ(A_0.ReadInt(ClipboardData.b("㹬ݮၰͲၴ㹶㵸", a_)));
					num = 24;
					continue;
				case 13:
					goto IL_574;
				case 14:
					goto IL_49D;
				case 15:
					if (A_0.HasAttribute(ClipboardData.b("㩬ᵮၰͲմṶ᝸ᱺ⥼پ", a_)))
					{
						num = 27;
						continue;
					}
					goto IL_24D;
				case 16:
					if (A_0.HasAttribute(ClipboardData.b("㩬ٮᕰݲᵴ", a_)))
					{
						num = 10;
						continue;
					}
					goto IL_219;
				case 17:
					if (A_0.HasAttribute(ClipboardData.b("⑬ᱮ㍰ᙲᥴᡶ๸⽺᡼ݾ", a_)))
					{
						num = 20;
						continue;
					}
					goto IL_383;
				case 18:
					goto IL_40E;
				case 19:
					this.ᜂ = A_0.ReadBoolean(ClipboardData.b("⑬ᱮ㥰ᙲᑴ፶ᱸॺ", a_));
					num = 5;
					continue;
				case 20:
					this.ᜀ.ᜃ(A_0.ReadBoolean(ClipboardData.b("⑬ᱮ㍰ᙲᥴᡶ๸⽺᡼ݾ", a_)));
					num = 0;
					continue;
				case 21:
					goto IL_24D;
				case 22:
					this.ᜀ.ᜀ((TextWrappingStyle)A_0.ReadEnum(ClipboardData.b("㩬ᵮၰͲմṶ᝸ᱺ⹼୾", a_), typeof(TextWrappingStyle)));
					num = 14;
					continue;
				case 23:
					goto IL_289;
				case 24:
					goto IL_540;
				case 25:
					if (A_0.HasAttribute(ClipboardData.b("㩬ᵮၰͲմṶ᝸ᱺ⹼୾", a_)))
					{
						num = 22;
						continue;
					}
					goto IL_49D;
				case 26:
					if (A_0.HasAttribute(ClipboardData.b("╬nͰᩲུᡶ᝸ེᱼ፾캀", a_)))
					{
						num = 34;
						continue;
					}
					goto IL_289;
				case 27:
					this.ᜀ.ᜀ((TextWrappingType)A_0.ReadEnum(ClipboardData.b("㩬ᵮၰͲմṶ᝸ᱺ⥼پ", a_), typeof(TextWrappingType)));
					num = 21;
					continue;
				case 28:
					goto IL_50C;
				case 29:
					this.ᜀ.ᜇ(A_0.ReadInt(ClipboardData.b("㭬੮Ͱݲᱴᑶᡸ᝺⵼ၾ", a_)));
					num = 30;
					continue;
				case 30:
					goto IL_1E5;
				case 31:
					if (A_0.HasAttribute(ClipboardData.b("╬੮ᡰᑲᵴͶ", a_)))
					{
						num = 35;
						continue;
					}
					goto IL_185;
				case 32:
					this.ᜀ.ᜂ(A_0.ReadInt(ClipboardData.b("╬nͰᩲུᡶ᝸ེᱼ፾톀ﶈ", a_)));
					num = 28;
					continue;
				case 33:
					this.ᜀ.ᜀ((VerticalOrigin)A_0.ReadEnum(ClipboardData.b("㭬੮Ͱݲᱴᑶᡸ᝺㉼ൾ", a_), typeof(VerticalOrigin)));
					num = 18;
					continue;
				case 34:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_260;
					default:
						if (false)
						{
						}
						this.ᜀ.ᜀ((HorizontalOrigin)A_0.ReadEnum(ClipboardData.b("╬nͰᩲུᡶ᝸ེᱼ፾캀", a_), typeof(HorizontalOrigin)));
						num = 23;
						continue;
					}
					break;
				case 35:
					this.ᜀ.ᜃ(A_0.ReadInt(ClipboardData.b("╬੮ᡰᑲᵴͶ", a_)));
					num = 8;
					continue;
				}
				break;
				IL_185:
				num = 16;
				continue;
				IL_1E5:
				num = 6;
				continue;
				IL_219:
				num = 1;
				continue;
				IL_24D:
				if (true)
				{
				}
				num = 4;
				continue;
				IL_260:
				if (A_0.HasAttribute(ClipboardData.b("╬nͰᩲུᡶ᝸ེᱼ፾톀ﶈ", a_)))
				{
					num = 32;
					continue;
				}
				goto IL_50C;
				IL_289:
				num = 11;
				continue;
				IL_383:
				num = 26;
				continue;
				IL_40E:
				num = 25;
				continue;
				IL_49D:
				num = 15;
				continue;
				IL_50C:
				num = 9;
				continue;
				IL_540:
				num = 17;
				continue;
				IL_574:
				num = 31;
			}
		}
	}

	// Token: 0x06000EFA RID: 3834 RVA: 0x000ECE2C File Offset: 0x000EBE2C
	protected virtual void \u170D()
	{
		int a_ = 11;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		base.InitXDLSHolder();
		base.XDLSHolder.AddElement(ClipboardData.b("հᙲ൴Ͷ᭸ᑺռ᩾", a_), this.ᜁ);
		base.XDLSHolder.AddElement(ClipboardData.b("ተ᭲ᑴնᡸ᡺ॼ᩾꺂ﮈﮎ", a_), this.m_charFormat);
	}

	// Token: 0x06000EFB RID: 3835 RVA: 0x000ECEB4 File Offset: 0x000EBEB4
	SizeF spr\u2297.ᜀ(spr\u19E0 A_0)
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
		return A_0.ᜀ(this);
	}

	// Token: 0x06000EFC RID: 3836 RVA: 0x000ECEF8 File Offset: 0x000EBEF8
	internal DocPicture ᜑ()
	{
		switch (0)
		{
		default:
		{
			DocPicture docPicture;
			for (;;)
			{
				Paragraph paragraph = base.Owner as Paragraph;
				Document document = base.Document;
				int num = 0;
				for (;;)
				{
					int num4;
					int num5;
					int num6;
					switch (num)
					{
					case 0:
						if (this.\u1712() == null)
						{
							num = 12;
							continue;
						}
						num = 16;
						continue;
					case 1:
						if (base.\u176D() != null)
						{
							num = 3;
							continue;
						}
						goto IL_396;
					case 2:
						goto IL_394;
					case 3:
						num = 14;
						continue;
					case 4:
						if (true)
						{
						}
						goto IL_2A2;
					case 5:
						goto IL_2A2;
					case 6:
					{
						float num2;
						if ((double)num2 != base.\u177D())
						{
							num = 17;
							continue;
						}
						goto IL_3B2;
					}
					case 7:
					{
						float num3;
						if (base.ន() != (double)num3)
						{
							num = 13;
							continue;
						}
						goto IL_2A2;
					}
					case 8:
						num = 25;
						continue;
					case 9:
						if (base.\u175B() != null)
						{
							num = 8;
							continue;
						}
						goto IL_27E;
					case 10:
						document.DocObject.Add(paragraph);
						num = 21;
						continue;
					case 11:
					{
						float num2 = this.ᜀ(base.ង(), (float)((int)base.\u176D()));
						num = 26;
						continue;
					}
					case 12:
						num = 27;
						continue;
					case 13:
					{
						float num3;
						base.ᜄ((double)num3);
						num = 5;
						continue;
					}
					case 14:
						if ((int)base.\u176D() > 0)
						{
							num = 11;
							continue;
						}
						goto IL_396;
					case 15:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_337;
						default:
						{
							if (false)
							{
							}
							float num3;
							docPicture.Height = ((num3 > 0f) ? num3 : ((float)base.ន()));
							num = 20;
							continue;
						}
						}
						break;
					case 16:
						num4 = paragraph.Items.IndexOf(this.\u1712()) + 1;
						goto IL_FD;
					case 17:
					{
						float num2;
						base.ᜊ((double)num2);
						num = 2;
						continue;
					}
					case 18:
						if (num5 != num6)
						{
							num = 10;
							continue;
						}
						goto IL_142;
					case 19:
					{
						float num2;
						if (num2 > 0f)
						{
							num = 28;
							continue;
						}
						goto IL_3B2;
					}
					case 20:
					{
						float num3;
						if (num3 > 0f)
						{
							num = 22;
							continue;
						}
						goto IL_2A2;
					}
					case 21:
						goto IL_142;
					case 22:
						num = 7;
						continue;
					case 23:
						goto IL_3B0;
					case 24:
					{
						float num3 = this.ᜀ(base.\u1756(), (float)((int)base.\u175B()));
						num = 15;
						continue;
					}
					case 25:
						if ((int)base.\u175B() > 0)
						{
							num = 24;
							continue;
						}
						goto IL_27E;
					case 26:
					{
						float num2;
						docPicture.Width = ((num2 > 0f) ? num2 : ((float)base.\u177D()));
						goto IL_337;
					}
					case 27:
						num4 = paragraph.Items.IndexOf(this) + 1;
						goto IL_FD;
					case 28:
						num = 6;
						continue;
					}
					break;
					IL_FD:
					num6 = num4;
					num5 = num6;
					this.ᜀ(paragraph, this, ref num6);
					num = 18;
					continue;
					IL_142:
					docPicture = new DocPicture(base.Document);
					docPicture.ShapeInfo = this;
					docPicture.ᜀ(base.Owner);
					num = 9;
					continue;
					IL_27E:
					docPicture.Height = (float)base.ន();
					num = 4;
					continue;
					IL_2A2:
					num = 1;
					continue;
					IL_337:
					num = 19;
					continue;
					IL_396:
					docPicture.Width = (float)base.\u177D();
					num = 23;
				}
			}
			IL_394:
			IL_3B0:
			IL_3B2:
			docPicture.TextWrappingStyle = base.ᝋ();
			docPicture.TextWrappingType = base.ច();
			docPicture.HorizontalAlignment = (ShapeHorizontalAlignment)base.ᝊ();
			docPicture.VerticalAlignment = (ShapeVerticalAlignment)base.\u175C();
			docPicture.HorizontalOrigin = (HorizontalOrigin)base.ថ();
			docPicture.VerticalOrigin = (VerticalOrigin)base.ធ();
			docPicture.HorizontalPosition = (float)base.\u177A();
			docPicture.VerticalPosition = (float)base.ᝣ();
			docPicture.LayoutInCell = base.\u1775();
			return docPicture;
		}
		}
	}

	// Token: 0x06000EFD RID: 3837 RVA: 0x000ED330 File Offset: 0x000EC330
	private new float ᜀ(RelativeHeight A_0, float A_1)
	{
		switch (0)
		{
		default:
		{
			float num;
			for (;;)
			{
				num = 0f;
				DocumentObject owner = base.Owner;
				int num2 = 10;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_196;
					case 1:
						num2 = 6;
						continue;
					case 2:
						goto IL_EC;
					case 3:
						goto IL_150;
					case 4:
						goto IL_114;
					case 5:
						IL_F8:
						if (owner is Section)
						{
							num2 = 8;
							continue;
						}
						goto IL_150;
					case 6:
						if (owner is Section)
						{
							num2 = 2;
							continue;
						}
						owner = owner.Owner;
						num2 = 4;
						continue;
					case 7:
						if (A_0 == RelativeHeight.Page)
						{
							num2 = 0;
							continue;
						}
						goto IL_1C5;
					case 8:
					{
						Section section = owner as Section;
						float left = section.PageSetup.Margins.Left;
						float right = section.PageSetup.Margins.Right;
						float top = section.PageSetup.Margins.Top;
						float bottom = section.PageSetup.Margins.Bottom;
						num = section.PageSetup.PageSize.Height;
						float width = section.PageSetup.PageSize.Width;
						float clientWidth = section.PageSetup.ClientWidth;
						num2 = 3;
						continue;
					}
					case 9:
						if (owner != null)
						{
							num2 = 1;
							continue;
						}
						goto IL_EC;
					case 10:
						goto IL_114;
					}
					break;
					IL_EC:
					num2 = 5;
					continue;
					IL_150:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F8;
					default:
						if (false)
						{
						}
						num2 = 7;
						continue;
					}
					IL_114:
					num2 = 9;
				}
			}
			IL_196:
			if (true)
			{
			}
			return num * A_1 / 1000f;
			IL_1C5:
			return (float)base.ន();
		}
		}
	}

	// Token: 0x06000EFE RID: 3838 RVA: 0x000ED50C File Offset: 0x000EC50C
	private new float ᜀ(RelativeWidth A_0, float A_1)
	{
		switch (0)
		{
		default:
		{
			float num;
			for (;;)
			{
				num = 0f;
				DocumentObject owner = base.Owner;
				int num2 = 10;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						num2 = 4;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_133;
						default:
							if (false)
							{
							}
							goto IL_17E;
						}
						break;
					case 2:
						goto IL_19E;
					case 3:
						if (owner is Section)
						{
							num2 = 6;
							continue;
						}
						goto IL_17E;
					case 4:
						if (owner is Section)
						{
							num2 = 7;
							continue;
						}
						owner = owner.Owner;
						num2 = 5;
						continue;
					case 5:
						goto IL_138;
					case 6:
						goto IL_133;
					case 7:
						goto IL_110;
					case 8:
						if (owner != null)
						{
							num2 = 0;
							continue;
						}
						goto IL_110;
					case 9:
						if (A_0 == RelativeWidth.Page)
						{
							num2 = 2;
							continue;
						}
						goto IL_1C5;
					case 10:
						goto IL_138;
					}
					break;
					IL_110:
					num2 = 3;
					continue;
					IL_133:
					Section section = owner as Section;
					float left = section.PageSetup.Margins.Left;
					float right = section.PageSetup.Margins.Right;
					float top = section.PageSetup.Margins.Top;
					float bottom = section.PageSetup.Margins.Bottom;
					float height = section.PageSetup.PageSize.Height;
					num = section.PageSetup.PageSize.Width;
					float clientWidth = section.PageSetup.ClientWidth;
					num2 = 1;
					continue;
					IL_138:
					num2 = 8;
					continue;
					IL_17E:
					num2 = 9;
				}
			}
			IL_19E:
			return num * A_1 / 1000f;
			IL_1C5:
			return (float)base.\u177D();
		}
		}
	}

	// Token: 0x06000EFF RID: 3839 RVA: 0x000ED6E8 File Offset: 0x000EC6E8
	internal TextBox ᜐ()
	{
		int num = 2;
		TextBox textBox;
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
					textBox.Format.VerticalPosition = ((base.Owner.DocumentObjectType == DocumentObjectType.ShapeGroup) ? ((float)base.ᝣ() / 20f) : ((float)base.ᝣ()));
					if (true)
					{
					}
					num = 3;
					continue;
				case 1:
					textBox.Format.HorizontalPosition = ((base.Owner.DocumentObjectType == DocumentObjectType.ShapeGroup) ? ((float)base.\u177A() / 20f) : ((float)base.\u177A()));
					num = 0;
					continue;
				case 3:
					goto IL_204;
				case 4:
					goto IL_5E;
				case 5:
					textBox.Format.Height = ((base.Owner.DocumentObjectType == DocumentObjectType.ShapeGroup) ? ((float)base.ន() / 20f) : ((float)base.ន()));
					textBox.Format.TextWrappingStyle = base.ᝋ();
					textBox.Format.TextWrappingType = base.ច();
					textBox.Format.HorizontalAlignment = (ShapeHorizontalAlignment)base.ᝊ();
					textBox.Format.VerticalAlignment = (ShapeVerticalAlignment)base.\u175C();
					textBox.Format.HorizontalOrigin = (HorizontalOrigin)base.ថ();
					textBox.Format.VerticalOrigin = (VerticalOrigin)base.ធ();
					goto IL_173;
				case 6:
					textBox.Format.Width = ((base.Owner.DocumentObjectType == DocumentObjectType.ShapeGroup) ? ((float)base.\u177D() / 20f) : ((float)base.\u177D()));
					num = 5;
					continue;
				}
				if (this.ᜎ().Count <= 0)
				{
					num = 4;
					continue;
				}
				textBox = (this.ᜎ()[0].Clone() as TextBox);
				textBox.ᜀ(this.ᜀ());
				num = 6;
				continue;
			}
			IL_173:
			num = 1;
		}
		IL_5E:
		return null;
		IL_204:
		textBox.Format.OrderIndex = ((base.Owner.DocumentObjectType == DocumentObjectType.ShapeGroup) ? (base.Owner as sprᢋ).\u1755() : base.\u1755());
		textBox.Format.NoLine = true;
		textBox.Format.FillColor = Color.Empty;
		textBox.Format.LineWidth = 0f;
		textBox.Format.LineColor = Color.Empty;
		textBox.Format.FillEfects.Type = BackgroundType.NoBackground;
		textBox.Format.IsInShape = true;
		return textBox;
	}

	// Token: 0x06000F00 RID: 3840 RVA: 0x000ED98C File Offset: 0x000EC98C
	private new void ᜀ(Paragraph A_0, spr\u248F A_1, ref int A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				int num2;
				DocumentObject documentObject;
				TextBox textBox2;
				switch (num)
				{
				case 0:
				{
					TextBox textBox;
					A_0.Items.InnerList.Insert(A_2, textBox);
					A_2++;
					num = 5;
					continue;
				}
				case 1:
					goto IL_80;
				case 2:
					num = 10;
					continue;
				case 4:
					return;
				case 5:
					return;
				case 6:
				{
					int count;
					if (num2 >= count)
					{
						goto IL_AC;
					}
					documentObject = A_1.ᝰ()[num2];
					num = 9;
					continue;
				}
				case 7:
					goto IL_16F;
				case 8:
					if (textBox2 != null)
					{
						num = 15;
						continue;
					}
					goto IL_16F;
				case 9:
					if (documentObject is spr\u248F)
					{
						num = 2;
						continue;
					}
					goto IL_1F6;
				case 10:
					if (documentObject.DocumentObjectType == DocumentObjectType.ShapeGroup)
					{
						num = 11;
						continue;
					}
					goto IL_1F6;
				case 11:
					if (true)
					{
					}
					this.ᜀ(A_0, documentObject as spr\u248F, ref A_2);
					num = 17;
					continue;
				case 12:
				{
					num2 = 0;
					int count = A_1.ᝰ().Count;
					num = 16;
					continue;
				}
				case 13:
					num = 18;
					continue;
				case 14:
				{
					TextBox textBox;
					if (textBox != null)
					{
						num = 0;
						continue;
					}
					return;
				}
				case 15:
					A_0.Items.InnerList.Insert(A_2, textBox2);
					A_2++;
					num = 7;
					continue;
				case 16:
					goto IL_80;
				case 17:
					goto IL_16F;
				case 18:
				{
					if (A_1.DocumentObjectType == DocumentObjectType.ShapeGroup)
					{
						num = 12;
						continue;
					}
					TextBox textBox = this.ᜀ(A_1);
					num = 14;
					continue;
				}
				}
				if (A_0 != null)
				{
					num = 13;
					continue;
				}
				return;
				IL_80:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_AC:
					num = 4;
					continue;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				IL_16F:
				num2++;
				num = 1;
				continue;
				IL_1F6:
				textBox2 = this.ᜀ(documentObject as spr\u248F);
				num = 8;
			}
			return;
		}
		}
	}

	// Token: 0x06000F01 RID: 3841 RVA: 0x000EDBF0 File Offset: 0x000ECBF0
	private new TextBox ᜀ(spr\u248F A_0)
	{
		int num = 5;
		TextBox textBox;
		bool flag2;
		for (;;)
		{
			bool flag;
			DocumentObject owner;
			switch (num)
			{
			case 0:
				flag = true;
				goto IL_153;
			case 1:
				textBox = (A_0.ᜎ()[0].Clone() as TextBox);
				owner = A_0.Owner;
				num = 8;
				continue;
			case 2:
				num = 17;
				continue;
			case 3:
				if (owner != null)
				{
					num = 18;
					continue;
				}
				goto IL_110;
			case 4:
				goto IL_D5;
			case 6:
				textBox.Format.Width = (flag2 ? ((float)A_0.\u177D() / 20f) : ((float)A_0.\u177D()));
				num = 12;
				continue;
			case 7:
				num = 9;
				continue;
			case 8:
				goto IL_D5;
			case 9:
				if (A_0.ᜎ().Count > 0)
				{
					num = 1;
					continue;
				}
				goto IL_3A7;
			case 10:
				textBox.Format.HorizontalPosition = (flag2 ? ((float)A_0.\u177A() / 20f - (float)((A_0.Owner as sprᢋ).ᝍ() / 20)) : ((float)A_0.\u177A()));
				num = 11;
				continue;
			case 11:
				textBox.Format.VerticalPosition = (flag2 ? ((float)A_0.ᝣ() / 20f - (float)((A_0.Owner as sprᢋ).ឈ() / 20)) : ((float)A_0.ᝣ()));
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3A7;
				default:
					if (false)
					{
					}
					num = 15;
					continue;
				}
				break;
			case 12:
				textBox.Format.Height = (flag2 ? ((float)A_0.ន() / 20f) : ((float)A_0.ន()));
				textBox.Format.TextWrappingStyle = A_0.ᝋ();
				textBox.Format.TextWrappingType = A_0.ច();
				textBox.Format.HorizontalAlignment = (ShapeHorizontalAlignment)A_0.ᝊ();
				textBox.Format.VerticalAlignment = (ShapeVerticalAlignment)A_0.\u175C();
				textBox.Format.HorizontalOrigin = (HorizontalOrigin)A_0.ថ();
				textBox.Format.VerticalOrigin = (VerticalOrigin)A_0.ធ();
				num = 10;
				continue;
			case 13:
				goto IL_37E;
			case 14:
				if (A_0.Owner.DocumentObjectType != DocumentObjectType.ShapeGroup)
				{
					num = 2;
					continue;
				}
				num = 0;
				continue;
			case 15:
				textBox.Format.OrderIndex = (flag2 ? (A_0.Owner as sprᢋ).\u1755() : A_0.\u1755());
				textBox.Format.NoLine = true;
				textBox.Format.FillColor = Color.Empty;
				textBox.Format.LineWidth = 0f;
				textBox.Format.LineColor = Color.Empty;
				textBox.Format.FillEfects.Type = BackgroundType.NoBackground;
				textBox.Format.IsInShape = true;
				if (true)
				{
				}
				num = 13;
				continue;
			case 16:
				if (owner is Paragraph)
				{
					num = 19;
					continue;
				}
				owner = owner.Owner;
				num = 4;
				continue;
			case 17:
				flag = false;
				goto IL_153;
			case 18:
				num = 16;
				continue;
			case 19:
				goto IL_110;
			}
			if (A_0 != null)
			{
				num = 7;
				continue;
			}
			goto IL_3A7;
			IL_D5:
			num = 3;
			continue;
			IL_110:
			num = 14;
			continue;
			IL_153:
			flag2 = flag;
			textBox.ᜀ(owner);
			num = 6;
		}
		IL_37E:
		textBox.ShapeInfo = (flag2 ? (A_0.Owner as spr\u248F) : A_0);
		textBox.Format.IsInGroupShape = flag2;
		return textBox;
		IL_3A7:
		return null;
	}

	// Token: 0x06000F02 RID: 3842 RVA: 0x000EDFA8 File Offset: 0x000ECFA8
	private new DocumentObject ᜀ()
	{
		DocumentObject owner;
		for (;;)
		{
			owner = base.Owner;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (owner is Paragraph)
					{
						num = 2;
						continue;
					}
					owner = owner.Owner;
					num = 4;
					continue;
				case 1:
					if (owner != null)
					{
						num = 3;
						continue;
					}
					return owner;
				case 2:
					return owner;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return owner;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 4:
					goto IL_53;
				case 5:
					if (true)
					{
					}
					goto IL_53;
				}
				break;
				IL_53:
				num = 1;
			}
		}
		return owner;
	}

	// Token: 0x04001748 RID: 5960
	private new sprᨼ ᜀ;

	// Token: 0x04001749 RID: 5961
	private new TextBoxItemCollection ᜁ;

	// Token: 0x0400174A RID: 5962
	private new bool ᜂ;

	// Token: 0x0400174B RID: 5963
	[CompilerGenerated]
	private DocumentObject ᜃ;

	// Token: 0x0400174C RID: 5964
	[CompilerGenerated]
	private new PointF ᜄ;
}
