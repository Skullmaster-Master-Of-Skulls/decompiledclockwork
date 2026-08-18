using System;
using System.Collections;
using System.Text.RegularExpressions;
using MailBee.AntiSpam;
using MailBee.Mime;

namespace a.m
{
	// Token: 0x0200020E RID: 526
	internal class f
	{
		// Token: 0x06001128 RID: 4392 RVA: 0x0004A54E File Offset: 0x0004954E
		public void c(bool A_0)
		{
			this.c = A_0;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0004A557 File Offset: 0x00049557
		public void a(bool A_0)
		{
			this.d = A_0;
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x0004A560 File Offset: 0x00049560
		public void a(int A_0)
		{
			this.e = A_0;
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0004A569 File Offset: 0x00049569
		public void b(bool A_0)
		{
			this.f = A_0;
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0004A574 File Offset: 0x00049574
		public f(BayesFilter A_0, MailMessage A_1, bool A_2)
		{
			this.h = A_1;
			this.n = A_0;
			this.c = true;
			this.d = true;
			this.e = 3;
			this.f = true;
			this.g = new ArrayList();
			this.j = false;
			this.k = false;
			this.i = A_2;
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0004A600 File Offset: 0x00049600
		private void c()
		{
			if (this.j)
			{
				return;
			}
			this.a(this.h.From.AsString, "F*");
			this.a(this.h.Subject, "S*");
			this.a(this.h.XMailer, "H*");
			HeaderCollection headerCollection = this.h.Headers.Items("Received");
			if (headerCollection != null)
			{
				foreach (object obj in headerCollection)
				{
					Header header = (Header)obj;
					int startat = 0;
					this.l = new Regex("([a-z0-9_\\-дьц]+(\\.[a-z0-9_\\-дьц]+)+)", RegexOptions.IgnoreCase);
					MatchCollection matchCollection = this.l.Matches(header.Value, startat);
					for (int i = 0; i < matchCollection.Count; i++)
					{
						string a_ = header.Value.Substring(matchCollection[i].Index, matchCollection[i].Length);
						this.a(a_, "H*");
					}
				}
			}
			this.j = true;
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0004A73C File Offset: 0x0004973C
		private void b()
		{
			if (this.k)
			{
				return;
			}
			if (this.h.BodyPlainText != string.Empty)
			{
				string a_ = this.b(this.h.BodyPlainText);
				this.a(a_, string.Empty);
			}
			if (this.h.BodyHtmlText != string.Empty)
			{
				string a_ = this.b(this.h.BodyHtmlText);
				if (this.c)
				{
					a_ = this.d(a_);
					this.e(a_);
				}
				a_ = this.c(a_);
				this.a(a_, string.Empty);
			}
			this.a();
			this.k = true;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0004A7E8 File Offset: 0x000497E8
		private void e(string A_0)
		{
			char[] trimChars = new char[]
			{
				'"',
				'\''
			};
			char[] a_ = new char[]
			{
				'\r',
				'\n',
				'\t',
				' ',
				'.',
				'-',
				'_',
				'!'
			};
			char[] a_2 = new char[]
			{
				'.',
				'-',
				'_',
				'!',
				'/',
				'?',
				'&'
			};
			char[] a_3 = new char[]
			{
				'.',
				'-',
				'_',
				'!'
			};
			for (int i = 0; i <= 7; i++)
			{
				switch (i)
				{
				case 0:
					this.l = new Regex("<[[:space:]\r\n]*(A|FORM)[^>]+(HREF|ACTION)[[:space:]\r\n]*=[[:space:]\r\n]*([^[:space:]>]+)[^>]*>", RegexOptions.IgnoreCase);
					break;
				case 1:
					this.l = new Regex("<[[:space:]\r\n]*(IMG|INPUT)[^>]+SRC[[:space:]\r\n]*=[[:space:]\r\n]*([^[:space:]>]+)[^>]*>", RegexOptions.IgnoreCase);
					break;
				case 2:
					this.l = new Regex("<[[:space:]\r\n]*FONT[^>]+COLOR[[:space:]\r\n]*=[[:space:]\r\n]*([^[:space:]>]+)[^>]*>", RegexOptions.IgnoreCase);
					break;
				case 3:
					this.l = new Regex("<[[:space:]\r\n]*FONT[^>]+(SIZE[[:space:]\r\n]*=[[:space:]\r\n]*[^[:space:]>]+)[^>]*>", RegexOptions.IgnoreCase);
					break;
				case 4:
					this.l = new Regex("<[[:space:]\r\n]*(H[12345678])[[:space:]\r\n]*>", RegexOptions.IgnoreCase);
					break;
				case 5:
					this.l = new Regex("<[[:space:]\r\n]*(BODY|TABLE|TD|TR)[^>]+(BGCOLOR|TEXT|BORDERCOLOR|BORDERCOLORLIGHT|BORDERCOLORDARK)[[:space:]\r\n]*=[[:space:]\r\n]*([^[:space:]>]+)[^>]*>", RegexOptions.IgnoreCase);
					break;
				case 6:
					this.l = new Regex("<[[:space:]\r\n]*META[^>]+CHARSET[[:space:]\r\n]*=[[:space:]\r\n]*([^[:space:]>]+)[^>]*>", RegexOptions.IgnoreCase);
					break;
				case 7:
					this.l = new Regex("(COLOR|BACKGROUND\\-COLOR|FONT\\-WEIGHT|FONT\\-SIZE|TEXT\\-DECORATION)[[:space:]\r\n]*:[[:space:]\r\n]*([^;>]+)", RegexOptions.IgnoreCase);
					break;
				}
				MatchCollection matchCollection = this.l.Matches(A_0);
				for (int j = 0; j < matchCollection.Count; j++)
				{
					string text = A_0.Substring(matchCollection[j].Index, matchCollection[j].Length);
					text = text.Trim(trimChars);
					switch (i)
					{
					case 0:
						if (string.Compare(text, 0, "mailto:", 0, 7) == 0)
						{
							this.m = new Regex("([0-9a-z\\.\\-_]+@[0-9a-z\\.\\-_дьц]+\\.[a-z]{2,4})(\\?subject=.*)?", RegexOptions.IgnoreCase);
							MatchCollection matchCollection2 = this.l.Matches(text);
							if (matchCollection2.Count > 0)
							{
								string text2 = text.Substring(matchCollection2[0].Index, matchCollection2[0].Length);
								this.a(text2);
								if (matchCollection2.Count > 2 && matchCollection2[2].Length > 9)
								{
									text2 = text.Substring(matchCollection2[2].Index + 9, matchCollection2[2].Length - 9);
									this.a(text2);
								}
							}
						}
						break;
					case 1:
					{
						string text3 = au.i(text);
						this.m = new Regex("([^:/?#]+://)?([^/?#]*)?([^?#]*)?([^&#]*)?([^#]*)?", RegexOptions.IgnoreCase);
						MatchCollection matchCollection3 = this.l.Matches(text3);
						for (int k = 0; k < matchCollection3.Count; k++)
						{
							text = text3.Substring(matchCollection3[k].Index, matchCollection3[k].Length);
							if (-1 != text.IndexOf('/'))
							{
								int num = 0;
								string text2 = text;
								int num2 = 0;
								int num3;
								while (-1 != (num3 = text2.IndexOf('/', num)))
								{
									if (num2 > 1)
									{
										text = text2.Substring(num, num3 - num);
										this.a(text, a_2);
									}
									num = num3 + 1;
									num2++;
								}
							}
							else
							{
								this.a(text, a_3);
							}
						}
						break;
					}
					case 2:
					case 3:
					case 5:
					case 6:
						text = text.Replace("\"", string.Empty);
						text = text.Replace("'", string.Empty);
						text = text.ToUpper();
						break;
					case 4:
						text = text.Remove(0, 1);
						text = "HEADERSIZE" + text;
						break;
					case 7:
						text = "STYLESHEET" + text.ToUpper();
						break;
					}
					if (i >= 2)
					{
						this.a(text, a_);
					}
				}
			}
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0004AB80 File Offset: 0x00049B80
		private string d(string A_0)
		{
			int startat = 0;
			this.l = new Regex("<\\!\\-\\-.*\\-\\->{0,256}?>", RegexOptions.IgnoreCase);
			MatchCollection matchCollection = this.l.Matches(A_0, startat);
			for (int i = 0; i < matchCollection.Count; i++)
			{
				this.a(A_0.Substring(matchCollection[i].Index, matchCollection[i].Length), string.Empty);
			}
			A_0 = this.l.Replace(A_0, string.Empty);
			return A_0;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0004ABFC File Offset: 0x00049BFC
		private string c(string A_0)
		{
			this.l = new Regex("<[^<>]{0,256}?>", RegexOptions.IgnoreCase);
			A_0 = this.l.Replace(A_0, string.Empty);
			return A_0;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0004AC24 File Offset: 0x00049C24
		private string b(string A_0)
		{
			if (A_0.IndexOf("&nbsp;") != -1)
			{
				A_0 = A_0.Replace("&nbsp;", " ");
			}
			if (A_0.IndexOf("&amp;") != -1)
			{
				A_0 = A_0.Replace("&amp;", "&");
			}
			if (A_0.IndexOf("&uuml;") != -1)
			{
				A_0 = A_0.Replace("&uuml;", "ь");
			}
			if (A_0.IndexOf("&Uuml;") != -1)
			{
				A_0 = A_0.Replace("&Uuml;", "Ь");
			}
			if (A_0.IndexOf("&ouml;") != -1)
			{
				A_0 = A_0.Replace("&ouml;", "ц");
			}
			if (A_0.IndexOf("&Ouml;") != -1)
			{
				A_0 = A_0.Replace("&Ouml;", "Ц");
			}
			if (A_0.IndexOf("&auml;") != -1)
			{
				A_0 = A_0.Replace("&auml;", "д");
			}
			if (A_0.IndexOf("&Auml;") != -1)
			{
				A_0 = A_0.Replace("&Auml;", "Д");
			}
			if (A_0.IndexOf("&szlig;") != -1)
			{
				A_0 = A_0.Replace("&szlig;", "Я");
			}
			if (A_0.IndexOf("&quot;") != -1)
			{
				A_0 = A_0.Replace("&quot;", "\"");
			}
			this.l = new Regex("\\.{3,}", RegexOptions.None);
			A_0 = this.l.Replace(A_0, ".");
			this.l = new Regex("\\-{3,}", RegexOptions.None);
			A_0 = this.l.Replace(A_0, "-");
			this.l = new Regex("_{3,}", RegexOptions.None);
			A_0 = this.l.Replace(A_0, "_");
			this.l = new Regex("&#x([0-9A-F]{1,4});", RegexOptions.IgnoreCase);
			int num = 0;
			MatchCollection matchCollection = this.l.Matches(A_0);
			for (int i = 0; i < matchCollection.Count; i++)
			{
				string value = A_0.Substring(matchCollection[i].Index + num + 3, matchCollection[i].Length - 4);
				int num2;
				try
				{
					num2 = Convert.ToInt32(value, 16);
				}
				catch (Exception)
				{
					num2 = 0;
				}
				if (num2 >= 33 && num2 <= 255)
				{
					string text = ((char)num2).ToString();
					A_0 = A_0.Remove(matchCollection[i].Index + num, matchCollection[i].Length);
					A_0 = A_0.Insert(matchCollection[i].Index + num, text);
					num += text.Length - matchCollection[i].Length;
				}
			}
			this.l = new Regex("&#([0-9]{1,5});", RegexOptions.IgnoreCase);
			num = 0;
			matchCollection = this.l.Matches(A_0);
			for (int i = 0; i < matchCollection.Count; i++)
			{
				string value2 = A_0.Substring(matchCollection[i].Index + num + 2, matchCollection[i].Length - 3);
				int num3;
				try
				{
					num3 = Convert.ToInt32(value2, 10);
				}
				catch (Exception)
				{
					num3 = 0;
				}
				if (num3 >= 33 && num3 <= 255)
				{
					string text2 = ((char)num3).ToString();
					A_0 = A_0.Remove(matchCollection[i].Index + num, matchCollection[i].Length);
					A_0 = A_0.Insert(matchCollection[i].Index + num, text2);
					num += text2.Length - matchCollection[i].Length;
				}
			}
			this.l = new Regex("(&#x?[0-9A-F]{1,5});", RegexOptions.None);
			A_0 = this.l.Replace(A_0, "($1#)");
			return A_0;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0004AFD8 File Offset: 0x00049FD8
		private void a(string A_0, char[] A_1)
		{
			if (A_1.Length != 0)
			{
				A_0 = A_0.Trim(A_1);
			}
			this.a(A_0);
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0004AFF0 File Offset: 0x00049FF0
		private void a(string A_0)
		{
			if (A_0.Length >= this.e)
			{
				if (this.f)
				{
					A_0 = A_0.ToLower();
				}
				this.g.Add(new a(A_0.Replace("\r\n", string.Empty).Replace("\r", string.Empty)));
			}
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0004B04C File Offset: 0x0004A04C
		private void a(string A_0, string A_1)
		{
			int length = A_0.Length;
			int num = 0;
			int num2;
			while (-1 != (num2 = A_0.IndexOfAny(this.b, num)))
			{
				if (num2 != num)
				{
					string text;
					if (A_1.Length > 0)
					{
						text = A_1;
					}
					else
					{
						text = string.Empty;
					}
					text += A_0.Substring(num, num2 - num);
					this.a(text, this.a);
				}
				num = num2 + 1;
			}
			if (num != 0 && num < A_0.Length)
			{
				string text;
				if (A_1.Length > 0)
				{
					text = A_1;
				}
				else
				{
					text = string.Empty;
				}
				text += A_0.Substring(num);
				this.a(text, this.a);
				return;
			}
			if (num == 0 && num2 == -1)
			{
				if (A_1.Length > 0)
				{
					A_0 = A_1 + A_0;
				}
				this.a(A_0, this.a);
			}
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0004B110 File Offset: 0x0004A110
		private void a()
		{
			if (!this.d)
			{
				return;
			}
			for (int i = 0; i < this.g.Count; i++)
			{
				for (int j = i + 1; j < this.g.Count; j++)
				{
					if (((a)this.g[i]).a == ((a)this.g[j]).a)
					{
						((a)this.g[i]).b += 1U;
						this.g.RemoveAt(j);
						j--;
					}
				}
			}
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0004B1B8 File Offset: 0x0004A1B8
		public ArrayList d()
		{
			this.c();
			this.b();
			return this.g;
		}

		// Token: 0x04000EA9 RID: 3753
		private readonly char[] a = new char[]
		{
			'.',
			'-',
			'_',
			'!'
		};

		// Token: 0x04000EAA RID: 3754
		private readonly char[] b = new char[]
		{
			'\u0001',
			'\u0002',
			'\u0003',
			'\u0004',
			'\u0005',
			'\u0006',
			'\a',
			'\b',
			'\t',
			'\n',
			'\v',
			'\f',
			'\r',
			'\u000e',
			'\u000f',
			'\u0010',
			'\u0011',
			'\u0012',
			'\u0013',
			'\u0014',
			'\u0015',
			'\u0016',
			'\u0017',
			'\u0018',
			'\u0019',
			' ',
			'\u007f',
			'(',
			')',
			'{',
			'}',
			'<',
			'>',
			'@',
			',',
			';',
			':',
			'*',
			'\\',
			'"',
			'/',
			'[',
			']',
			'?',
			'_',
			'='
		};

		// Token: 0x04000EAB RID: 3755
		private bool c;

		// Token: 0x04000EAC RID: 3756
		private bool d;

		// Token: 0x04000EAD RID: 3757
		private int e;

		// Token: 0x04000EAE RID: 3758
		private bool f;

		// Token: 0x04000EAF RID: 3759
		private ArrayList g;

		// Token: 0x04000EB0 RID: 3760
		private MailMessage h;

		// Token: 0x04000EB1 RID: 3761
		private bool i;

		// Token: 0x04000EB2 RID: 3762
		private bool j;

		// Token: 0x04000EB3 RID: 3763
		private bool k;

		// Token: 0x04000EB4 RID: 3764
		private Regex l;

		// Token: 0x04000EB5 RID: 3765
		private Regex m;

		// Token: 0x04000EB6 RID: 3766
		private BayesFilter n;
	}
}
