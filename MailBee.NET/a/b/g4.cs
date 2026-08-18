using System;
using System.Collections;
using System.IO;
using System.Text;
using a.h;
using MailBee;
using MailBee.Mime;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000246 RID: 582
	internal class g4 : ab
	{
		// Token: 0x0600137C RID: 4988 RVA: 0x000585C8 File Offset: 0x000575C8
		public new void a(string A_0, string A_1)
		{
			if (A_0 == string.Empty)
			{
				A_0 = A_1;
			}
			string a_ = "SMTP";
			string text = "0042";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_0, this.a, this.o));
			byte[] array = new byte[]
			{
				0,
				0,
				0,
				0,
				129,
				43,
				31,
				164,
				190,
				163,
				16,
				25,
				157,
				110,
				0,
				221,
				1,
				15,
				84,
				2,
				0,
				0,
				1,
				144
			};
			byte[] bytes = Encoding.ASCII.GetBytes(A_0 + "\0SMTP\0" + A_1 + "\0");
			byte[] array2 = new byte[array.Length + bytes.Length * 2];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = array[i];
			}
			int num = 0;
			for (int j = array.Length; j < array2.Length; j += 2)
			{
				array2[j] = bytes[num++];
				array2[j + 1] = 0;
			}
			text = "0041";
			base.b(text, "0102");
			base.a("__substg1.0_" + text, "0102", ab.a(array2));
			text = "003B";
			base.b(text, "0102");
			base.a("__substg1.0_" + text, "0102", ab.a(Encoding.ASCII.GetBytes("SMTP:" + A_1.ToUpper() + "\0")));
			text = "0064";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(a_, this.o));
			text = "0065";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_1, this.o));
			text = "0C19";
			base.b(text, "0102");
			base.a("__substg1.0_" + text, "0102", ab.a(array2));
			text = "0C1A";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_0, this.a, this.o));
			text = "0C1D";
			base.b(text, "0102");
			base.a("__substg1.0_" + text, "0102", ab.a(Encoding.ASCII.GetBytes("SMTP:" + A_1.ToUpper() + "\0")));
			text = "0C1E";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(a_, this.o));
			text = "0C1F";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_1, this.o));
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x000588C4 File Offset: 0x000578C4
		public new void c(string A_0)
		{
			string text = "001A";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_0, this.o));
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00058908 File Offset: 0x00057908
		public new void d(string A_0)
		{
			this.a();
			string text = "1000";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_0, this.a, this.o));
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00058958 File Offset: 0x00057958
		public new void f(string A_0)
		{
			string text = "1009";
			base.b(text, "0102");
			base.a("__substg1.0_" + text, "0102", ab.a(global::a.h.f.a(Global.DefaultEncoding.GetBytes(A_0), false)));
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x000589A4 File Offset: 0x000579A4
		public new void b()
		{
			string text = "0E1F";
			base.b(text, "000B");
			this.m.a(Convert.ToInt64(text, 16), true);
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x000589D7 File Offset: 0x000579D7
		public new Encoding c()
		{
			return this.a;
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x000589DF File Offset: 0x000579DF
		public new void a(Encoding A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x000589E8 File Offset: 0x000579E8
		private new void a()
		{
			if (this.a != null && !this.b)
			{
				string str = "3FDE";
				base.a(this.k, "__substg1.0_" + str, "0003", (long)this.a.CodePage);
				this.b = true;
			}
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x00058A3C File Offset: 0x00057A3C
		public new void b(string A_0)
		{
			this.a();
			string text = "1013";
			if (this.o)
			{
				base.b(text, "0102");
				base.a("__substg1.0_" + text, "0102", ab.a(A_0, this.a));
				return;
			}
			base.b(text, "001E");
			base.a("__substg1.0_" + text, "001E", ab.a(A_0, this.a));
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00058ABC File Offset: 0x00057ABC
		public new void e(string A_0)
		{
			string text = "0037";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_0, this.a, this.o));
			text = "0E1D";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_0, this.a, this.o));
			text = "0070";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_0, this.a, this.o));
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00058B80 File Offset: 0x00057B80
		public new void a(string A_0)
		{
			string text = "007D";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_0, this.o));
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00058BC4 File Offset: 0x00057BC4
		public new void c(DateTime A_0)
		{
			string text = "3008";
			base.b(text, "0040");
			base.a(this.k, "__substg1.0_" + text, "0040", ab.a(A_0));
			text = "3007";
			base.b(text, "0040");
			base.a(this.k, "__substg1.0_" + text, "0040", ab.a(A_0));
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00058C3C File Offset: 0x00057C3C
		public new void b(DateTime A_0)
		{
			string text = "0E06";
			base.b(text, "0040");
			base.a(this.k, "__substg1.0_" + text, "0040", ab.a(A_0));
			this.c = A_0;
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00058C85 File Offset: 0x00057C85
		public new DateTime d()
		{
			return this.c;
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00058C90 File Offset: 0x00057C90
		public new void a(DateTime A_0)
		{
			string text = "0039";
			base.b(text, "0040");
			base.a(this.k, "__substg1.0_" + text, "0040", ab.a(A_0));
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00058CD4 File Offset: 0x00057CD4
		public new void a(MailPriority A_0)
		{
			base.b("0026", "0003");
			switch (A_0)
			{
			case MailPriority.Highest:
			case MailPriority.High:
				this.m.a(38L, 3L, true, 889058230273UL);
				return;
			case MailPriority.Normal:
				break;
			case MailPriority.Low:
			case MailPriority.Lowest:
				this.m.a(38L, 3L, true, 893353197567UL);
				break;
			default:
				return;
			}
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00058D44 File Offset: 0x00057D44
		public new void b(bool A_0)
		{
			string text = "0E1B";
			base.b(text, "000B");
			this.m.a(Convert.ToInt64(text, 16), A_0);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x00058D78 File Offset: 0x00057D78
		public new void e()
		{
			string text = "1009";
			base.b(text, "000B");
			this.m.a(Convert.ToInt64(text, 16), 258L, true, 0UL);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00058DB3 File Offset: 0x00057DB3
		public g4()
		{
			this.f();
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00058DF0 File Offset: 0x00057DF0
		public g4(bool A_0, bool A_1, bool A_2)
		{
			this.o = A_0;
			this.p = A_1;
			this.q = A_2;
			this.f();
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00058E4C File Offset: 0x00057E4C
		public g4(Stream A_0)
		{
			try
			{
				IEnumerator a_ = new POIFSFileSystem(A_0).Root.Entries;
				this.n = base.a(a_);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00058EC0 File Offset: 0x00057EC0
		public new void f()
		{
			this.e = 0;
			this.f = 0;
			this.g = string.Empty;
			this.i = string.Empty;
			this.h = string.Empty;
			this.d = new POIFSFileSystem();
			this.k = this.d.Root;
			this.k = e8.a(this.k);
			this.m = new e();
			this.c("IPM.Note");
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00058F40 File Offset: 0x00057F40
		public new void a(string A_0, byte[] A_1)
		{
			this.e++;
			this.b(true);
			string text = string.Concat(this.e - 1);
			this.m.b(this.e);
			this.m.a(this.f);
			while (text.Length < 8)
			{
				text = "0" + text;
			}
			io io = new io(this.k.eo("__attach_version1.0_#" + text), this.o);
			io.a(this.e - 1);
			io.b(A_0);
			io.a(A_1);
			io.c("application/octet-stream");
			io.b();
			io.b(this.e - 1);
			io.a();
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x00059010 File Offset: 0x00058010
		public new void a(string A_0, byte[] A_1, string A_2, string A_3)
		{
			string str = this.e.ToString("X8");
			this.e++;
			this.b(true);
			this.m.b(this.e);
			this.m.a(this.f);
			io io = new io(this.k.eo("__attach_version1.0_#" + str), this.o);
			io.a(this.e - 1);
			io.b(A_0);
			io.a(A_1);
			io.c((A_3 != string.Empty) ? A_3 : "application/octet-stream");
			io.b();
			io.b(this.e - 1);
			if (A_2 != string.Empty)
			{
				io.a(A_2);
			}
			io.a();
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x000590F0 File Offset: 0x000580F0
		public new void a(string A_0, int A_1)
		{
			this.f++;
			string text = string.Concat(this.f - 1);
			this.m.b(this.e);
			this.m.a(this.f);
			while (text.Length < 8)
			{
				text = "0" + text;
			}
			ig a_ = this.k.eo("__recip_version1.0_#" + text);
			if (A_1 == 1)
			{
				if (this.g != string.Empty)
				{
					this.g += "; ";
				}
				this.g += A_0;
				string text2 = "0E04";
				base.b(text2, base.g());
				base.a("__substg1.0_" + text2, base.g(), ab.a(this.g, this.o));
			}
			else if (A_1 == 2)
			{
				if (this.h != string.Empty)
				{
					this.h += "; ";
				}
				this.h += A_0;
				string text3 = "0E03";
				base.b(text3, base.g());
				base.a("__substg1.0_" + text3, base.g(), ab.a(this.h, this.o));
			}
			else if (A_1 == 3)
			{
				if (this.i != string.Empty)
				{
					this.i += "; ";
				}
				this.i += A_0;
				string text4 = "0E02";
				base.b(text4, base.g());
				base.a("__substg1.0_" + text4, base.g(), ab.a(this.i, this.o));
			}
			ih ih = new ih(a_, this.o);
			ih.a((long)(this.f - 1));
			ih.b(A_0);
			ih.a("SMTP");
			ih.a((long)(this.f - 1), A_1);
			ih.a();
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00059324 File Offset: 0x00058324
		public new void a(string A_0, string A_1, int A_2)
		{
			if (A_0 == string.Empty)
			{
				A_0 = A_1;
			}
			string str = this.f.ToString("X8");
			this.f++;
			this.m.b(this.e);
			this.m.a(this.f);
			ig a_ = this.k.eo("__recip_version1.0_#" + str);
			if (A_2 == 1)
			{
				if (this.g != string.Empty)
				{
					this.g += "; ";
				}
				this.g += A_0;
				string text = "0E04";
				base.b(text, base.g());
				base.a("__substg1.0_" + text, base.g(), ab.a(this.g, this.a, this.o));
			}
			else if (A_2 == 2)
			{
				if (this.h != string.Empty)
				{
					this.h += "; ";
				}
				this.h += A_0;
				string text2 = "0E03";
				base.b(text2, base.g());
				base.a("__substg1.0_" + text2, base.g(), ab.a(this.h, this.a, this.o));
			}
			else if (A_2 == 3)
			{
				if (this.i != string.Empty)
				{
					this.i += "; ";
				}
				this.i += A_0;
				string text3 = "0E02";
				base.b(text3, base.g());
				base.a("__substg1.0_" + text3, base.g(), ab.a(this.i, this.a, this.o));
			}
			ih ih = new ih(a_, this.o);
			ih.a((long)(this.f - 1));
			ih.a(A_0, this.a, A_1);
			ih.a("SMTP");
			ih.a((long)(this.f - 1), A_2);
			ih.a();
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00059570 File Offset: 0x00058570
		private new void a(bool A_0)
		{
			if (this.o)
			{
				string text = "800F";
				base.b(text, "001F");
				base.a("__substg1.0_" + text, "001F", ab.a(""));
				this.m.a(13325L, 3L, false, 32088572547042937UL);
			}
			ulong num = 0UL;
			if (this.p)
			{
				num |= 8UL;
			}
			if (this.q)
			{
				num |= 32UL;
			}
			if (this.e > 0)
			{
				num |= 16UL;
			}
			this.m.a(3591L, 3L, false, num);
			if (A_0)
			{
				this.m.a(3615L, 11L, true, 1UL);
			}
			else if (this.d() != DateTime.MinValue)
			{
				double num2 = this.d().ToUniversalTime().ToOADate();
				int num3 = Convert.ToInt32((num2 - Math.Floor(num2)) * 100000000.0 + 3.0);
				this.m.a(4246L, 3L, false, (ulong)((long)num3));
			}
			this.m.a(4084L, 3L, false, 27303497942695938UL);
			byte[] array = this.m.du();
			byte[] array2 = new byte[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = array[i];
			}
			Stream a_ = new MemoryStream(array2);
			this.k.em("__properties_version1.0", a_);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00059700 File Offset: 0x00058700
		public new void a(Stream A_0, bool A_1)
		{
			try
			{
				this.a(A_1);
				this.d.c(A_0);
			}
			catch (IOException a_)
			{
				throw new MailBeeIOException(30, a_);
			}
			catch (NotSupportedException a_2)
			{
				throw new MailBeeIOException(20, a_2);
			}
			catch (ArgumentException a_3)
			{
				throw new MailBeeIOException(20, a_3);
			}
		}

		// Token: 0x04000F99 RID: 3993
		private new Encoding a;

		// Token: 0x04000F9A RID: 3994
		private new bool b;

		// Token: 0x04000F9B RID: 3995
		private new DateTime c = DateTime.MinValue;

		// Token: 0x04000F9C RID: 3996
		private new POIFSFileSystem d;

		// Token: 0x04000F9D RID: 3997
		private new int e;

		// Token: 0x04000F9E RID: 3998
		private new int f;

		// Token: 0x04000F9F RID: 3999
		private new string g = string.Empty;

		// Token: 0x04000FA0 RID: 4000
		private new string h = string.Empty;

		// Token: 0x04000FA1 RID: 4001
		private new string i = string.Empty;
	}
}
