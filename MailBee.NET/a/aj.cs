using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using a.i;
using MailBee;

namespace a
{
	// Token: 0x02000481 RID: 1153
	internal class aj : SaslMethod
	{
		// Token: 0x060027CC RID: 10188 RVA: 0x000B85F8 File Offset: 0x000B75F8
		public override string GetSaslID()
		{
			return "DIGEST-MD5";
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x000B85FF File Offset: 0x000B75FF
		internal override AuthenticationMethods GetMethodEnumMember()
		{
			return AuthenticationMethods.SaslDigestMD5;
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x000B8604 File Offset: 0x000B7604
		public override void CreateNextClientAnswer()
		{
			int stage = base.Stage;
			if (stage == 0)
			{
				base.ClientAnswer = base.ClientAnswerEncoding.GetBytes(this.d());
				stage = base.Stage;
				base.Stage = stage + 1;
				return;
			}
			if (stage != 1)
			{
				return;
			}
			base.ClientAnswer = new byte[0];
			stage = base.Stage;
			base.Stage = stage + 1;
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x000B8664 File Offset: 0x000B7664
		public override bool IsSecure()
		{
			return true;
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x000B8667 File Offset: 0x000B7667
		public override bool IsFipsCompliant()
		{
			return false;
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x000B866C File Offset: 0x000B766C
		private new string d()
		{
			this.a = aj.a.i(base.ServerChallengeEncoding.GetString(base.ServerChallenge, 0, base.ServerChallenge.Length));
			this.b.a = base.AccountName;
			this.b.b = this.a.a;
			this.b.c = this.a.b;
			this.b.d = global::a.i.k.a();
			this.b.e = "00000001";
			this.b.f = "auth";
			this.b.g = base.ServiceName.ToLower() + "/" + base.ServerName.ToLower();
			this.b.j = this.a.f;
			this.b.h = this.c();
			this.b.i = null;
			this.b.k = null;
			this.b.l = null;
			return this.b.ToString();
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x000B8794 File Offset: 0x000B7794
		private new string c()
		{
			byte[] a_ = this.b();
			byte[] a_2 = this.a();
			string text = this.a(a_);
			string text2 = this.a(a_2);
			string s = string.Format("{0}:{1}:{2}:{3}:{4}:{5}", new object[]
			{
				text,
				this.b.c,
				this.b.e,
				this.b.d,
				this.b.f,
				text2
			});
			return this.a(Encoding.Default.GetBytes(s));
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x000B8824 File Offset: 0x000B7824
		private new byte[] b()
		{
			Encoding encoding = (this.a.f != null && this.a.f.Length > 0) ? Encoding.UTF8 : Encoding.Default;
			byte[] array = null;
			byte[] a_ = null;
			byte[] array2 = (this.b.a != null) ? encoding.GetBytes(this.b.a) : new byte[0];
			byte[] array3 = (this.b.b != null) ? encoding.GetBytes(this.b.b) : new byte[0];
			byte[] array4 = (base.Password != null) ? encoding.GetBytes(base.Password) : new byte[0];
			array2 = Encoding.Convert(encoding, Encoding.GetEncoding(28591), array2);
			array3 = Encoding.Convert(encoding, Encoding.GetEncoding(28591), array3);
			array4 = Encoding.Convert(encoding, Encoding.GetEncoding(28591), array4);
			array = w.b(array, array2);
			array = w.b(array, new byte[]
			{
				Convert.ToByte(':')
			});
			array = w.b(array, array3);
			array = w.b(array, new byte[]
			{
				Convert.ToByte(':')
			});
			array = w.b(array, array4);
			array = new MD5CryptoServiceProvider().ComputeHash(array);
			byte[] a_2 = (this.b.c != null) ? Encoding.Default.GetBytes(this.b.c) : new byte[0];
			byte[] a_3 = (this.b.d != null) ? Encoding.Default.GetBytes(this.b.d) : new byte[0];
			a_ = w.b(a_, array);
			a_ = w.b(a_, new byte[]
			{
				Convert.ToByte(':')
			});
			a_ = w.b(a_, a_2);
			a_ = w.b(a_, new byte[]
			{
				Convert.ToByte(':')
			});
			return w.b(a_, a_3);
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x000B8A1C File Offset: 0x000B7A1C
		private new byte[] a()
		{
			string s = string.Format("{0}:{1}", "AUTHENTICATE", this.b.g);
			return Encoding.Default.GetBytes(s);
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x000B8A50 File Offset: 0x000B7A50
		private new string a(byte[] A_0)
		{
			byte[] array = new MD5CryptoServiceProvider().ComputeHash(A_0);
			StringBuilder stringBuilder = new StringBuilder(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001B2D RID: 6957
		private new aj.a a;

		// Token: 0x04001B2E RID: 6958
		private new aj.b b;

		// Token: 0x02000482 RID: 1154
		internal new struct b
		{
			// Token: 0x060027D7 RID: 10199 RVA: 0x000B8AA8 File Offset: 0x000B7AA8
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (this.a != null && this.a.Length > 0)
				{
					stringBuilder.AppendFormat(null, "username=\"{0}\",", new object[]
					{
						this.a
					});
				}
				if (this.b != null && this.b.Length > 0)
				{
					stringBuilder.AppendFormat(null, "realm=\"{0}\",", new object[]
					{
						this.b
					});
				}
				if (this.c != null && this.c.Length > 0)
				{
					stringBuilder.AppendFormat(null, "nonce=\"{0}\",", new object[]
					{
						this.c
					});
				}
				if (this.d != null && this.d.Length > 0)
				{
					stringBuilder.AppendFormat(null, "cnonce=\"{0}\",", new object[]
					{
						this.d
					});
				}
				if (this.e != null && this.e.Length > 0)
				{
					stringBuilder.AppendFormat(null, "nc={0},", new object[]
					{
						this.e
					});
				}
				if (this.f != null && this.f.Length > 0)
				{
					stringBuilder.AppendFormat(null, "qop={0},", new object[]
					{
						this.f
					});
				}
				if (this.g != null && this.g.Length > 0)
				{
					stringBuilder.AppendFormat(null, "digest-uri=\"{0}\",", new object[]
					{
						this.g
					});
				}
				if (this.h != null && this.h.Length > 0)
				{
					stringBuilder.AppendFormat(null, "response={0},", new object[]
					{
						this.h
					});
				}
				if (this.i != null && this.i.Length > 0)
				{
					stringBuilder.AppendFormat(null, "maxbuf=\"{0}\",", new object[]
					{
						this.i
					});
				}
				if (this.j != null && this.j.Length > 0 && this.j.ToLower() == "utf-8")
				{
					stringBuilder.AppendFormat(null, "charset={0},", new object[]
					{
						this.j
					});
				}
				if (this.k != null && this.k.Length > 0)
				{
					stringBuilder.AppendFormat(null, "cipher={0},", new object[]
					{
						this.k
					});
				}
				if (this.l != null && this.l.Length > 0)
				{
					stringBuilder.AppendFormat(null, "authzid=\"{0}\",", new object[]
					{
						this.l
					});
				}
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
				return stringBuilder.ToString();
			}

			// Token: 0x04001B2F RID: 6959
			public string a;

			// Token: 0x04001B30 RID: 6960
			public string b;

			// Token: 0x04001B31 RID: 6961
			public string c;

			// Token: 0x04001B32 RID: 6962
			public string d;

			// Token: 0x04001B33 RID: 6963
			public string e;

			// Token: 0x04001B34 RID: 6964
			public string f;

			// Token: 0x04001B35 RID: 6965
			public string g;

			// Token: 0x04001B36 RID: 6966
			public string h;

			// Token: 0x04001B37 RID: 6967
			public string i;

			// Token: 0x04001B38 RID: 6968
			public string j;

			// Token: 0x04001B39 RID: 6969
			public string k;

			// Token: 0x04001B3A RID: 6970
			public string l;
		}

		// Token: 0x02000483 RID: 1155
		public new struct a
		{
			// Token: 0x060027D8 RID: 10200 RVA: 0x000B8D40 File Offset: 0x000B7D40
			public static aj.a i(string A_0)
			{
				aj.a result = default(aj.a);
				foreach (string input in A_0.Split(new char[]
				{
					','
				}))
				{
					Match match = new Regex("(?<attribute>\\w+)=(\")?(?<value>[^\"]+)(\")?", RegexOptions.Singleline).Match(input);
					if (match.Success)
					{
						string value = match.Groups["value"].Value;
						string a_ = match.Groups["attribute"].Value.ToLower();
						uint num = global::b.a(a_);
						if (num <= 2010794635U)
						{
							if (num <= 1749328254U)
							{
								if (num != 474311018U)
								{
									if (num == 1749328254U)
									{
										if (a_ == "stale")
										{
											result.d = value;
										}
									}
								}
								else if (a_ == "algorithm")
								{
									result.g = value;
								}
							}
							else if (num != 1914854288U)
							{
								if (num == 2010794635U)
								{
									if (a_ == "charset")
									{
										result.f = value;
									}
								}
							}
							else if (a_ == "realm")
							{
								result.a = value;
							}
						}
						else if (num <= 3972544558U)
						{
							if (num != 3907609162U)
							{
								if (num == 3972544558U)
								{
									if (a_ == "maxbuf")
									{
										result.e = value;
									}
								}
							}
							else if (a_ == "cipher")
							{
								result.h = value;
							}
						}
						else if (num != 4143537083U)
						{
							if (num == 4178082296U)
							{
								if (a_ == "nonce")
								{
									result.b = value;
								}
							}
						}
						else if (a_ == "qop")
						{
							result.c = value;
						}
					}
				}
				return result;
			}

			// Token: 0x04001B3B RID: 6971
			public string a;

			// Token: 0x04001B3C RID: 6972
			public string b;

			// Token: 0x04001B3D RID: 6973
			public string c;

			// Token: 0x04001B3E RID: 6974
			public string d;

			// Token: 0x04001B3F RID: 6975
			public string e;

			// Token: 0x04001B40 RID: 6976
			public string f;

			// Token: 0x04001B41 RID: 6977
			public string g;

			// Token: 0x04001B42 RID: 6978
			public string h;
		}
	}
}
