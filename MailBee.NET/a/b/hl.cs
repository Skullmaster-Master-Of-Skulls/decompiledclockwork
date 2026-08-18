using System;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000353 RID: 851
	internal sealed class hl : gf, cy
	{
		// Token: 0x06001EF0 RID: 7920 RVA: 0x00083BE8 File Offset: 0x00082BE8
		public hl(params g5[] A_0) : base(A_0)
		{
			this.a = new a5(base.c().f());
			this.b = new dy(base.c().b());
			this.c = new cd(base.c().c());
			this.d = new ef(base.c().g());
			this.e = new ca();
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x00083C60 File Offset: 0x00082C60
		public static bool d(f A_0)
		{
			try
			{
				hl.c(A_0);
			}
			catch (RtfException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x00083C90 File Offset: 0x00082C90
		public static f c(f A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("rtfDocument");
			}
			if (A_0.nt().get_Count() == 0)
			{
				throw new RtfEmptyDocumentException(fa.l());
			}
			f8 f = A_0.nt().kb(0);
			if (f.p() != gl.a)
			{
				throw new RtfStructureException(fa.k());
			}
			c9 c = (c9)f;
			if (!"rtf".Equals(c.jz()))
			{
				throw new RtfStructureException(fa.f("rtf"));
			}
			if (!c.j0())
			{
				throw new RtfUnsupportedStructureException(fa.j());
			}
			if (c.j2() != 1)
			{
				throw new RtfUnsupportedStructureException(fa.k(c.j2()));
			}
			return A_0;
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x00083D38 File Offset: 0x00082D38
		protected override void op(f A_0)
		{
			this.b(hl.c(A_0));
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x00083D46 File Offset: 0x00082D46
		private void b(f A_0)
		{
			base.c().h();
			this.f = false;
			base.b();
			this.a(A_0);
			base.c().a(gw.d);
			base.a();
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x00083D7C File Offset: 0x00082D7C
		private void a(f A_0)
		{
			bool flag = false;
			if (base.c().kr() == gw.c)
			{
				base.c().d();
				flag = true;
			}
			try
			{
				foreach (object obj in A_0.nt())
				{
					((f8)obj).q(this);
				}
			}
			finally
			{
				if (flag)
				{
					base.c().e();
				}
			}
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x00083E0C File Offset: 0x00082E0C
		void cy.a(c9 A_0)
		{
			if (base.c().kr() != gw.c && base.c().kv().get_Count() > 0 && (base.c().kw().get_Count() > 0 || "viewkind".Equals(A_0.jz())))
			{
				base.c().a(gw.c);
			}
			switch (base.c().kr())
			{
			case gw.a:
				if ("rtf".Equals(A_0.jz()))
				{
					base.c().a(gw.b);
					base.c().a(A_0.j2());
					return;
				}
				throw new RtfStructureException(fa.e(A_0.ToString()));
			case gw.b:
			{
				string a_ = A_0.jz();
				if (a_ == "deff")
				{
					base.c().a("f" + A_0.j2());
					return;
				}
				break;
			}
			case gw.c:
			{
				string a_ = A_0.jz();
				uint num = global::b.a(a_);
				if (num <= 1850493229U)
				{
					if (num <= 1457505901U)
					{
						if (num <= 671913016U)
						{
							if (num <= 400234023U)
							{
								if (num != 327653265U)
								{
									if (num != 400234023U)
									{
										return;
									}
									if (!(a_ == "line"))
									{
										return;
									}
									base.a(RtfVisualBreakKind.Line);
									return;
								}
								else
								{
									if (!(a_ == "objattph"))
									{
										return;
									}
									base.a(de.e, 1, 1, 1, 1, 1, 1, string.Empty);
									break;
								}
							}
							else if (num != 480244007U)
							{
								if (num != 632598351U)
								{
									if (num != 671913016U)
									{
										return;
									}
									if (!(a_ == "-"))
									{
										return;
									}
									base.a(RtfVisualSpecialCharKind.OptionalHyphen);
									return;
								}
								else
								{
									if (!(a_ == "strike"))
									{
										return;
									}
									bool a_2 = !A_0.j0() || A_0.j2() != 0;
									base.c().a(base.c().a().f(a_2));
									return;
								}
							}
							else
							{
								if (!(a_ == "highlight"))
								{
									return;
								}
								goto IL_947;
							}
						}
						else if (num <= 1131883462U)
						{
							if (num != 1128467232U)
							{
								if (num != 1131883462U)
								{
									return;
								}
								if (!(a_ == "ulnone"))
								{
									return;
								}
								base.c().a(base.c().a().b(false));
								return;
							}
							else
							{
								if (!(a_ == "up"))
								{
									return;
								}
								int num2 = A_0.j2();
								if (num2 == 0)
								{
									num2 = 6;
								}
								base.c().a(base.c().a().a(num2));
								return;
							}
						}
						else if (num != 1428787088U)
						{
							if (num != 1445123422U)
							{
								if (num != 1457505901U)
								{
									return;
								}
								if (!(a_ == "lquote"))
								{
									return;
								}
								base.a(RtfVisualSpecialCharKind.LeftSingleQuote);
								return;
							}
							else
							{
								if (!(a_ == "fs"))
								{
									return;
								}
								int num3 = A_0.j2();
								if (num3 > 0)
								{
									base.c().a(base.c().a().b(num3));
									return;
								}
								throw new RtfInvalidDataException(fa.m(num3));
							}
						}
						else
						{
							if (!(a_ == "cb"))
							{
								return;
							}
							goto IL_947;
						}
					}
					else if (num <= 1662835111U)
					{
						if (num <= 1565273706U)
						{
							if (num != 1495897564U)
							{
								if (num != 1565273706U)
								{
									return;
								}
								if (!(a_ == "qr"))
								{
									return;
								}
								base.c().a(base.c().a().a(ay.c));
								return;
							}
							else
							{
								if (!(a_ == "cf"))
								{
									return;
								}
								goto IL_947;
							}
						}
						else if (num != 1598240564U)
						{
							if (num != 1598828944U)
							{
								if (num != 1662835111U)
								{
									return;
								}
								if (!(a_ == "dn"))
								{
									return;
								}
								int num4 = A_0.j2();
								if (num4 == 0)
								{
									num4 = 6;
								}
								base.c().a(base.c().a().a(-num4));
								return;
							}
							else
							{
								if (!(a_ == "ql"))
								{
									return;
								}
								base.c().a(base.c().a().a(ay.a));
								return;
							}
						}
						else
						{
							if (!(a_ == "ul"))
							{
								return;
							}
							bool a_3 = !A_0.j0() || A_0.j2() != 0;
							base.c().a(base.c().a().b(a_3));
							return;
						}
					}
					else if (num <= 1699494658U)
					{
						if (num != 1666584168U)
						{
							if (num != 1683285682U)
							{
								if (num != 1699494658U)
								{
									return;
								}
								if (!(a_ == "qj"))
								{
									return;
								}
								base.c().a(base.c().a().a(ay.d));
								return;
							}
							else
							{
								if (!(a_ == "sect"))
								{
									return;
								}
								base.a(RtfVisualBreakKind.Section);
								return;
							}
						}
						else
						{
							if (!(a_ == "par"))
							{
								return;
							}
							base.a(RtfVisualBreakKind.Paragraph);
							return;
						}
					}
					else if (num != 1806694916U)
					{
						if (num != 1819811044U)
						{
							if (num != 1850493229U)
							{
								return;
							}
							if (!(a_ == "qc"))
							{
								return;
							}
							base.c().a(base.c().a().a(ay.b));
							return;
						}
						else if (!(a_ == "pard"))
						{
							return;
						}
					}
					else
					{
						if (!(a_ == "enspace"))
						{
							return;
						}
						base.a(RtfVisualSpecialCharKind.EnSpace);
						return;
					}
				}
				else if (num <= 3658226030U)
				{
					if (num <= 3008542559U)
					{
						if (num <= 2328620175U)
						{
							if (num != 2170419830U)
							{
								if (num != 2328620175U)
								{
									return;
								}
								if (!(a_ == "qmspace"))
								{
									return;
								}
								base.a(RtfVisualSpecialCharKind.QmSpace);
								return;
							}
							else
							{
								if (!(a_ == "page"))
								{
									return;
								}
								base.a(RtfVisualBreakKind.Page);
								return;
							}
						}
						else if (num != 2566336076U)
						{
							if (num != 2861155257U)
							{
								if (num != 3008542559U)
								{
									return;
								}
								if (!(a_ == "rdblquote"))
								{
									return;
								}
								base.a(RtfVisualSpecialCharKind.RightDoubleQuote);
								return;
							}
							else
							{
								if (!(a_ == "nosupersub"))
								{
									return;
								}
								base.c().a(base.c().a().a(0));
								return;
							}
						}
						else
						{
							if (!(a_ == "tab"))
							{
								return;
							}
							base.a(RtfVisualSpecialCharKind.Tabulator);
							return;
						}
					}
					else if (num <= 3434512309U)
					{
						if (num != 3242461418U)
						{
							if (num != 3349635810U)
							{
								if (num != 3434512309U)
								{
									return;
								}
								if (!(a_ == "ldblquote"))
								{
									return;
								}
								base.a(RtfVisualSpecialCharKind.LeftDoubleQuote);
								return;
							}
							else if (!(a_ == "sectd"))
							{
								return;
							}
						}
						else
						{
							if (!(a_ == "endash"))
							{
								return;
							}
							base.a(RtfVisualSpecialCharKind.EnDash);
							return;
						}
					}
					else if (num != 3552460647U)
					{
						if (num != 3647204775U)
						{
							if (num != 3658226030U)
							{
								return;
							}
							if (!(a_ == "_"))
							{
								return;
							}
							base.a(RtfVisualSpecialCharKind.NonBreakingHyphen);
							return;
						}
						else
						{
							if (!(a_ == "rquote"))
							{
								return;
							}
							base.a(RtfVisualSpecialCharKind.RightSingleQuote);
							return;
						}
					}
					else
					{
						if (!(a_ == "plain"))
						{
							return;
						}
						base.c().a(base.c().a().c());
						return;
					}
				}
				else if (num <= 3902055289U)
				{
					if (num <= 3809224601U)
					{
						if (num != 3696113941U)
						{
							if (num != 3809224601U)
							{
								return;
							}
							if (!(a_ == "f"))
							{
								return;
							}
							string a_4 = A_0.jy();
							if (base.c().kv().ga(a_4))
							{
								base.c().a(base.c().a().a(base.c().kv().gc(a_4)));
								return;
							}
							throw new RtfUndefinedFontException(fa.d(a_4));
						}
						else
						{
							if (!(a_ == "sub"))
							{
								return;
							}
							base.c().a(base.c().a().c(false));
							return;
						}
					}
					else if (num != 3823790992U)
					{
						if (num != 3876335077U)
						{
							if (num != 3902055289U)
							{
								return;
							}
							if (!(a_ == "bullet"))
							{
								return;
							}
							base.a(RtfVisualSpecialCharKind.Bullet);
							return;
						}
						else
						{
							if (!(a_ == "b"))
							{
								return;
							}
							bool a_5 = !A_0.j0() || A_0.j2() != 0;
							base.c().a(base.c().a().e(a_5));
							return;
						}
					}
					else
					{
						if (!(a_ == "chcbpat"))
						{
							return;
						}
						goto IL_947;
					}
				}
				else if (num <= 4077666505U)
				{
					if (num != 3960223172U)
					{
						if (num != 4075356123U)
						{
							if (num != 4077666505U)
							{
								return;
							}
							if (!(a_ == "v"))
							{
								return;
							}
							bool a_6 = !A_0.j0() || A_0.j2() != 0;
							base.c().a(base.c().a().a(a_6));
							return;
						}
						else
						{
							if (!(a_ == "emdash"))
							{
								return;
							}
							base.a(RtfVisualSpecialCharKind.EmDash);
							return;
						}
					}
					else
					{
						if (!(a_ == "i"))
						{
							return;
						}
						bool a_7 = !A_0.j0() || A_0.j2() != 0;
						base.c().a(base.c().a().d(a_7));
						return;
					}
				}
				else if (num != 4152230356U)
				{
					if (num != 4199938611U)
					{
						if (num != 4211887457U)
						{
							return;
						}
						if (!(a_ == "~"))
						{
							return;
						}
						base.a(RtfVisualSpecialCharKind.NonBreakingSpace);
						return;
					}
					else
					{
						if (!(a_ == "emspace"))
						{
							return;
						}
						base.a(RtfVisualSpecialCharKind.EmSpace);
						return;
					}
				}
				else
				{
					if (!(a_ == "super"))
					{
						return;
					}
					base.c().a(base.c().a().c(true));
					return;
				}
				base.c().a(base.c().a().a(ay.a));
				return;
				IL_947:
				int num5 = A_0.j2();
				if (num5 >= 0 && num5 < base.c().kw().get_Count())
				{
					gb a_8 = base.c().kw().o7(num5);
					bool flag = "cf".Equals(A_0.jz());
					base.c().a(flag ? base.c().a().b(a_8) : base.c().a().a(a_8));
					return;
				}
				throw new RtfUndefinedColorException(fa.l(num5));
			}
			default:
				return;
			}
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x00084894 File Offset: 0x00083894
		void cy.e(f A_0)
		{
			string text = A_0.nu();
			switch (base.c().kr())
			{
			case gw.a:
				if ("rtf".Equals(text))
				{
					this.a(A_0);
					return;
				}
				throw new RtfStructureException(fa.c(text));
			case gw.b:
			{
				uint num = global::b.a(text);
				if (num <= 1860974018U)
				{
					if (num <= 1131883462U)
					{
						if (num != 0U)
						{
							if (num != 1131883462U)
							{
								return;
							}
							if (!(text == "ulnone"))
							{
								return;
							}
						}
						else if (text != null)
						{
							return;
						}
					}
					else if (num != 1819811044U)
					{
						if (num != 1860974018U)
						{
							return;
						}
						if (!(text == "generator"))
						{
							return;
						}
						base.c().a(gw.c);
						bp bp = (A_0.nt().get_Count() == 3) ? (A_0.nt().kb(2) as bp) : null;
						if (bp != null)
						{
							string text2 = bp.eu();
							base.c().b(text2.EndsWith(";") ? text2.Substring(0, text2.Length - 1) : text2);
							return;
						}
						throw new RtfInvalidDataException(fa.b(A_0.ToString()));
					}
					else if (!(text == "pard"))
					{
						return;
					}
				}
				else if (num <= 3051949850U)
				{
					if (num != 3003421458U)
					{
						if (num != 3051949850U)
						{
							return;
						}
						if (!(text == "colortbl"))
						{
							return;
						}
						this.b.ps(A_0);
						return;
					}
					else
					{
						if (!(text == "fonttbl"))
						{
							return;
						}
						this.a.ps(A_0);
						return;
					}
				}
				else if (num != 3349635810U)
				{
					if (num != 3552460647U)
					{
						return;
					}
					if (!(text == "plain"))
					{
						return;
					}
				}
				else if (!(text == "sectd"))
				{
					return;
				}
				base.c().a(gw.c);
				if (!A_0.nv())
				{
					this.a(A_0);
					return;
				}
				break;
			}
			case gw.c:
			{
				uint num = global::b.a(text);
				if (num <= 1475767949U)
				{
					if (num <= 290031026U)
					{
						if (num <= 180202684U)
						{
							if (num != 155321749U)
							{
								if (num != 180202684U)
								{
									goto IL_5B9;
								}
								if (!(text == "footerf"))
								{
									goto IL_5B9;
								}
								break;
							}
							else
							{
								if (!(text == "pict"))
								{
									goto IL_5B9;
								}
								this.e.ps(A_0);
								base.a(this.e.e(), this.e.f(), this.e.h(), this.e.c(), this.e.g(), this.e.b(), this.e.d(), this.e.a());
								return;
							}
						}
						else if (num != 263456517U)
						{
							if (num != 290031026U)
							{
								goto IL_5B9;
							}
							if (!(text == "footer"))
							{
								goto IL_5B9;
							}
							break;
						}
						else
						{
							if (!(text == "info"))
							{
								goto IL_5B9;
							}
							this.c.ps(A_0);
							return;
						}
					}
					else if (num <= 347978874U)
					{
						if (num != 327653265U)
						{
							if (num != 347978874U)
							{
								goto IL_5B9;
							}
							if (!(text == "footerl"))
							{
								goto IL_5B9;
							}
							break;
						}
						else
						{
							if (!(text == "objattph"))
							{
								goto IL_5B9;
							}
							base.a(de.e, 0, 0, 0, 0, 0, 0, string.Empty);
							return;
						}
					}
					else if (num != 881512982U)
					{
						if (num != 1392331604U)
						{
							if (num != 1475767949U)
							{
								goto IL_5B9;
							}
							if (!(text == "nonshppict"))
							{
								goto IL_5B9;
							}
							if (!this.f)
							{
								this.a(A_0);
							}
							this.f = false;
							return;
						}
						else if (!(text == "pntext"))
						{
							goto IL_5B9;
						}
					}
					else
					{
						if (!(text == "upr"))
						{
							goto IL_5B9;
						}
						f f = A_0.nw("ud");
						if (f != null)
						{
							this.a(f);
							return;
						}
						f f2 = (A_0.nt().get_Count() > 2) ? (A_0.nt().kb(2) as f) : null;
						if (f2 != null)
						{
							this.a(f2);
							return;
						}
						break;
					}
				}
				else if (num <= 3580221526U)
				{
					if (num <= 2363025592U)
					{
						if (num != 1966078305U)
						{
							if (num != 2363025592U)
							{
								goto IL_5B9;
							}
							if (!(text == "userprops"))
							{
								goto IL_5B9;
							}
							this.d.ps(A_0);
							return;
						}
						else
						{
							if (!(text == "stylesheet"))
							{
								goto IL_5B9;
							}
							break;
						}
					}
					else if (num != 3378890098U)
					{
						if (num != 3479555812U)
						{
							if (num != 3580221526U)
							{
								goto IL_5B9;
							}
							if (!(text == "headerr"))
							{
								goto IL_5B9;
							}
							break;
						}
						else
						{
							if (!(text == "headerl"))
							{
								goto IL_5B9;
							}
							break;
						}
					}
					else
					{
						if (!(text == "headerf"))
						{
							goto IL_5B9;
						}
						break;
					}
				}
				else if (num <= 3718652721U)
				{
					if (num != 3607388050U)
					{
						if (num != 3718652721U)
						{
							goto IL_5B9;
						}
						if (!(text == "footnote"))
						{
							goto IL_5B9;
						}
						break;
					}
					else
					{
						if (!(text == "shppict"))
						{
							goto IL_5B9;
						}
						this.a(A_0);
						this.f = true;
						return;
					}
				}
				else if (num != 3834172512U)
				{
					if (num != 4139617600U)
					{
						if (num != 4141617394U)
						{
							goto IL_5B9;
						}
						if (!(text == "listtext"))
						{
							goto IL_5B9;
						}
					}
					else
					{
						if (!(text == "footerr"))
						{
							goto IL_5B9;
						}
						break;
					}
				}
				else
				{
					if (!(text == "header"))
					{
						goto IL_5B9;
					}
					break;
				}
				base.a(RtfVisualSpecialCharKind.ParagraphNumberBegin);
				this.a(A_0);
				base.a(RtfVisualSpecialCharKind.ParagraphNumberEnd);
				return;
				IL_5B9:
				if (!A_0.nv())
				{
					this.a(A_0);
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x00084E6C File Offset: 0x00083E6C
		void cy.a(bp A_0)
		{
			switch (base.c().kr())
			{
			case gw.a:
				throw new RtfStructureException(fa.a(A_0.eu()));
			case gw.b:
				base.c().a(gw.c);
				break;
			}
			base.a(A_0.eu());
		}

		// Token: 0x04001419 RID: 5145
		private new readonly a5 a;

		// Token: 0x0400141A RID: 5146
		private new readonly dy b;

		// Token: 0x0400141B RID: 5147
		private readonly cd c;

		// Token: 0x0400141C RID: 5148
		private readonly ef d;

		// Token: 0x0400141D RID: 5149
		private readonly ca e;

		// Token: 0x0400141E RID: 5150
		private bool f;
	}
}
