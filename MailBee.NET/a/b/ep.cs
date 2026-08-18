using System;
using System.Globalization;
using System.Text;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200034B RID: 843
	internal class ep : en
	{
		// Token: 0x06001E85 RID: 7813 RVA: 0x0008246E File Offset: 0x0008146E
		public ep() : this(new eu())
		{
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x0008247B File Offset: 0x0008147B
		public ep(eu A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("settings");
			}
			this.c = A_0;
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x000824A3 File Offset: 0x000814A3
		public string a()
		{
			return this.b.ToString();
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x000824B0 File Offset: 0x000814B0
		public eu c()
		{
			return this.c;
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x000824B8 File Offset: 0x000814B8
		public void b()
		{
			this.b.Remove(0, this.b.Length);
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x000824D2 File Offset: 0x000814D2
		protected override void db(eq A_0)
		{
			this.b();
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x000824DA File Offset: 0x000814DA
		protected override void jt(eq A_0, string A_1)
		{
			if (!A_0.kz().g5() || this.c.o())
			{
				this.b.Append(A_1);
			}
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x00082504 File Offset: 0x00081504
		protected override void ju(eq A_0, RtfVisualSpecialCharKind A_1)
		{
			switch (A_1)
			{
			case RtfVisualSpecialCharKind.Tabulator:
				this.b.Append(this.c.p());
				return;
			case RtfVisualSpecialCharKind.NonBreakingSpace:
				this.b.Append(this.c.g());
				return;
			case RtfVisualSpecialCharKind.EmDash:
				this.b.Append(this.c.t());
				return;
			case RtfVisualSpecialCharKind.EnDash:
				this.b.Append(this.c.v());
				return;
			case RtfVisualSpecialCharKind.EmSpace:
				this.b.Append(this.c.b());
				return;
			case RtfVisualSpecialCharKind.EnSpace:
				this.b.Append(this.c.l());
				return;
			case RtfVisualSpecialCharKind.QmSpace:
				this.b.Append(this.c.h());
				return;
			case RtfVisualSpecialCharKind.Bullet:
				this.b.Append(this.c.q());
				return;
			case RtfVisualSpecialCharKind.LeftSingleQuote:
				this.b.Append(this.c.r());
				return;
			case RtfVisualSpecialCharKind.RightSingleQuote:
				this.b.Append(this.c.a());
				return;
			case RtfVisualSpecialCharKind.LeftDoubleQuote:
				this.b.Append(this.c.f());
				return;
			case RtfVisualSpecialCharKind.RightDoubleQuote:
				this.b.Append(this.c.i());
				return;
			case RtfVisualSpecialCharKind.OptionalHyphen:
				this.b.Append(this.c.m());
				return;
			case RtfVisualSpecialCharKind.NonBreakingHyphen:
				this.b.Append(this.c.s());
				return;
			}
			this.b.Append(this.c.n());
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x000826C4 File Offset: 0x000816C4
		protected override void jv(eq A_0, RtfVisualBreakKind A_1)
		{
			switch (A_1)
			{
			case RtfVisualBreakKind.Line:
				this.b.Append(this.c.e());
				return;
			case RtfVisualBreakKind.Page:
				this.b.Append(this.c.d());
				return;
			case RtfVisualBreakKind.Paragraph:
				this.b.Append(this.c.k());
				return;
			case RtfVisualBreakKind.Section:
				this.b.Append(this.c.j());
				return;
			default:
				this.b.Append(this.c.u());
				return;
			}
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x00082760 File Offset: 0x00081760
		protected override void dc(eq A_0, de A_1, int A_2, int A_3, int A_4, int A_5, int A_6, int A_7, string A_8)
		{
			string text = this.c.c();
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			string value = string.Format(CultureInfo.InvariantCulture, text, new object[]
			{
				A_1,
				A_2,
				A_3,
				A_4,
				A_5,
				A_6,
				A_7,
				A_8
			});
			this.b.Append(value);
		}

		// Token: 0x040013E3 RID: 5091
		public const string a = ".txt";

		// Token: 0x040013E4 RID: 5092
		private readonly StringBuilder b = new StringBuilder();

		// Token: 0x040013E5 RID: 5093
		private readonly eu c;
	}
}
