using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MailBee;

namespace a
{
	// Token: 0x020000E2 RID: 226
	internal abstract class u
	{
		// Token: 0x0600075B RID: 1883 RVA: 0x00021FCD File Offset: 0x00020FCD
		public u(ab A_0)
		{
			this.a = A_0;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00021FDC File Offset: 0x00020FDC
		private byte[] a(string A_0)
		{
			int num = A_0.IndexOfAny(new char[]
			{
				' ',
				'\t'
			});
			if (num > -1)
			{
				A_0 = A_0.Substring(0, num);
			}
			byte[] result;
			try
			{
				result = Convert.FromBase64String(A_0);
			}
			catch (FormatException)
			{
				throw new MailBeeInvalidTextResponseItemException(126, this.a.a1(), A_0, this.a.bg());
			}
			return result;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0002204C File Offset: 0x0002104C
		protected virtual bf hg(bool A_0)
		{
			return this.a.fg(true);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0002205C File Offset: 0x0002105C
		public bool d(SaslMethod A_0, bool A_1)
		{
			bf bf = this.hg(A_1);
			string text;
			if (A_0.ClientAnswer == null)
			{
				text = "*";
			}
			else if (A_0.ClientAnswer.Length == 0)
			{
				text = "==";
			}
			else
			{
				text = Convert.ToBase64String(A_0.ClientAnswer);
			}
			string text2;
			if (A_1)
			{
				text2 = string.Concat(new string[]
				{
					this.a.aj().jw(),
					" ",
					A_0.GetSaslID(),
					" ",
					text
				});
			}
			else
			{
				text2 = text;
			}
			text2 = this.a.o2(text2, bf);
			bf.j = (!A_0.IsSecure() && text.Length > 0);
			if (bf.j)
			{
				bf.k = this.a.bd().GetBytes(text2.Replace(text, Global.PrivateDataCover));
			}
			if (!this.a.c(text2, bf, false))
			{
				if (A_0.ClientAnswer == null)
				{
					return false;
				}
				throw (Exception)this.ea(116, this.a.a1(), this.a.ak(), A_0.AccountName, A_0.Password);
			}
			else
			{
				if (this.a.ak().t() == af.b)
				{
					if (A_0.ExpectBase64Challenge)
					{
						A_0.ServerChallenge = this.a(this.a.ak().r());
					}
					else
					{
						A_0.ServerChallenge = new byte[0];
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x000221C8 File Offset: 0x000211C8
		private void b(SaslMethod A_0, string A_1, string A_2, string A_3, string A_4, string A_5, bool A_6)
		{
			A_0.ClientAnswerEncoding = this.a.bd();
			A_0.ServerChallengeEncoding = this.a.bg();
			A_0.AccountName = A_4;
			A_0.Password = A_5;
			A_0.AccountDomain = A_3;
			A_0.ServiceNameInternal = this.a.er();
			A_0.ServerNameInternal = this.a.a1().b();
			A_0.TargetNameInternal = A_2;
			if (A_0.ExpectBase64Challenge)
			{
				A_0.ServerChallenge = this.a(A_1);
			}
			else
			{
				A_0.ServerChallenge = new byte[0];
			}
			if (A_0 is bi)
			{
				bi bi = (bi)A_0;
				bi bi2 = bi;
				bi2.a((u.d)Delegate.Combine(bi2.b(), new u.d(this.d)));
				try
				{
					bi.c(A_6);
					return;
				}
				finally
				{
					bi bi3 = bi;
					bi3.a((u.d)Delegate.Remove(bi3.b(), new u.d(this.d)));
					A_0.Dispose();
				}
			}
			bool flag = A_6;
			try
			{
				for (;;)
				{
					try
					{
						A_0.CreateNextClientAnswer();
					}
					catch (MailBeeLocalException)
					{
						A_0.ClientAnswer = null;
						if (!flag)
						{
							this.d(A_0, false);
						}
						throw;
					}
					if (!this.d(A_0, flag))
					{
						break;
					}
					flag = false;
				}
			}
			finally
			{
				A_0.Dispose();
			}
		}

		// Token: 0x06000760 RID: 1888
		protected abstract IMailBeeLoginBadCredentialsException ea(int A_0, ai A_1, at A_2, string A_3, string A_4);

		// Token: 0x06000761 RID: 1889
		protected abstract IMailBeeLoginBadMethodException eb(int A_0, ai A_1, at A_2, AuthenticationMethods A_3);

		// Token: 0x06000762 RID: 1890
		protected abstract bool ec(AuthenticationMethods A_0, string A_1, string A_2, string A_3, bool A_4);

		// Token: 0x06000763 RID: 1891 RVA: 0x00022328 File Offset: 0x00021328
		private string b(SaslMethod A_0, bool A_1)
		{
			if (this.a.o1(this.a.aj().jw() + " " + A_0.GetSaslID(), false))
			{
				return this.a.ak().r();
			}
			MailBeeException ex = (MailBeeException)this.eb(114, this.a.a1(), this.a.ak(), A_0.GetMethodEnumMember());
			if (A_1)
			{
				throw ex;
			}
			this.a.c(ex);
			return null;
		}

		// Token: 0x06000764 RID: 1892
		protected abstract bool ed(string A_0, string A_1, string A_2);

		// Token: 0x06000765 RID: 1893 RVA: 0x000223B0 File Offset: 0x000213B0
		private bool a(ref AuthenticationMethods A_0, SaslMethod A_1, string A_2)
		{
			if (A_1.AccountDataIsPassword() && A_2 != null && A_0 != A_1.GetMethodEnumMember())
			{
				A_0 &= AuthenticationMethods.Auto - (int)A_1.GetMethodEnumMember();
				return true;
			}
			return false;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x000223DB File Offset: 0x000213DB
		protected virtual bool ee()
		{
			return true;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000223E0 File Offset: 0x000213E0
		public virtual void b(AuthenticationMethods A_0, AuthenticationMethods A_1, SaslMethod A_2, AuthenticationOptions A_3, string A_4, string A_5, string A_6, string A_7)
		{
			AuthenticationMethods authenticationMethods;
			if ((A_3 & AuthenticationOptions.TryUnsupportedMethods) > AuthenticationOptions.None)
			{
				authenticationMethods = A_0;
			}
			else
			{
				authenticationMethods = (A_0 & (A_1 | AuthenticationMethods.Regular));
			}
			bool flag = false;
			bool flag2 = true;
			ag ag = ag.a;
			if ((A_3 & AuthenticationOptions.PreferSimpleMethods) > AuthenticationOptions.None)
			{
				flag2 = false;
				ag = ag.d;
			}
			SaslMethod saslMethod = null;
			MailBeeException ex = null;
			while (!flag)
			{
				switch (ag)
				{
				case ag.a:
					goto IL_48;
				case ag.b:
					goto IL_80;
				case ag.c:
					goto IL_D0;
				case ag.d:
					goto IL_FB;
				}
				IL_117:
				if (ag == ag.d && flag2)
				{
					break;
				}
				if (!flag && saslMethod != null)
				{
					this.a.a8().b(string.Format(Resources.Instance.Log_WillTrySasl0Auth, saslMethod.GetSaslID()), null, LogMessageType.Info, this.a);
					if (Global.FipsMode && !saslMethod.IsFipsCompliant())
					{
						authenticationMethods &= AuthenticationMethods.Auto - (int)saslMethod.GetMethodEnumMember();
					}
					else
					{
						if (saslMethod.RequiresCredentials() && (((A_6 == null || A_6 == string.Empty) && !saslMethod.AccountDataIsPassword()) || ((A_7 == null || A_7 == string.Empty) && saslMethod.AccountDataIsPassword())))
						{
							throw new MailBeeLoginNoCredentialsException(112, this.a.a1(), A_6, A_7);
						}
						bool flag3 = this.ee();
						string text;
						if (flag3)
						{
							text = this.b(saslMethod, (A_3 & AuthenticationOptions.UseSingleMethodOnly) > AuthenticationOptions.None);
						}
						else
						{
							text = string.Empty;
						}
						if (text == null)
						{
							authenticationMethods &= AuthenticationMethods.Auto - (int)saslMethod.GetMethodEnumMember();
							this.a.a8().b(string.Format(Resources.Instance.Log_Sasl0AuthUnsupported, saslMethod.GetSaslID()), null, LogMessageType.Info, this.a);
						}
						else
						{
							saslMethod.LocalDomainIsDefault = ((A_3 & AuthenticationOptions.UseLocalDomainAsDefault) > AuthenticationOptions.None);
							try
							{
								this.b(saslMethod, text, A_4, A_5, A_6, A_7, !flag3);
							}
							catch (MailBeeException ex2)
							{
								if ((ex2 is IMailBeeLoginBadCredentialsException || ex2 is MailBeeLoginWin32Exception) && (A_3 & AuthenticationOptions.DisableSimpleMethodAfterSecure) == AuthenticationOptions.None && (A_3 & AuthenticationOptions.PreferSimpleMethods) == AuthenticationOptions.None && (A_3 & AuthenticationOptions.UseSingleMethodOnly) == AuthenticationOptions.None)
								{
									ex = ex2;
									if (saslMethod.IsSecure())
									{
										ag = ag.c;
									}
									else
									{
										ag = ag.d;
									}
									continue;
								}
								throw;
							}
							flag = true;
						}
					}
				}
				if (ag != ag.a || flag2)
				{
					continue;
				}
				break;
				IL_48:
				saslMethod = SaslMethod.a(authenticationMethods, (A_3 & AuthenticationOptions.PreferSspiOverNegotiateStream) > AuthenticationOptions.None, A_2);
				if (saslMethod == null)
				{
					if (!flag2)
					{
						goto IL_117;
					}
					ag = ag.b;
				}
				else
				{
					if (this.a(ref authenticationMethods, saslMethod, A_6))
					{
						saslMethod = null;
						goto IL_117;
					}
					goto IL_117;
				}
				IL_80:
				try
				{
					flag = this.ec(authenticationMethods, A_5, A_6, A_7, (A_3 & AuthenticationOptions.UseSingleMethodOnly) > AuthenticationOptions.None);
				}
				catch (MailBeeEmailProtocolNegativeResponseException ex3)
				{
					if (!(ex3 is IMailBeeLoginBadCredentialsException) || (A_3 & AuthenticationOptions.DisableSimpleMethodAfterSecure) != AuthenticationOptions.None || (A_3 & AuthenticationOptions.PreferSimpleMethods) != AuthenticationOptions.None || (A_3 & AuthenticationOptions.UseSingleMethodOnly) != AuthenticationOptions.None)
					{
						throw;
					}
					ex = ex3;
				}
				if (flag)
				{
					goto IL_117;
				}
				if (!flag2)
				{
					ag = ag.a;
					goto IL_48;
				}
				ag = ag.c;
				IL_D0:
				saslMethod = SaslMethod.b(authenticationMethods, A_2);
				if (saslMethod == null)
				{
					if (!flag2)
					{
						ag = ag.b;
						goto IL_80;
					}
					ag = ag.d;
				}
				else
				{
					if (this.a(ref authenticationMethods, saslMethod, A_6))
					{
						saslMethod = null;
						goto IL_117;
					}
					goto IL_117;
				}
				IL_FB:
				if ((authenticationMethods & AuthenticationMethods.Regular) > AuthenticationMethods.None)
				{
					flag = this.ed(A_5, A_6, A_7);
				}
				if (!flag2 && !flag)
				{
					ag = ag.c;
					goto IL_D0;
				}
				goto IL_117;
			}
			if (flag)
			{
				return;
			}
			if (ex != null)
			{
				throw ex;
			}
			throw new MailBeeLoginNoSupportedMethodsException(115, this.a.a1(), A_0, A_1);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000226E0 File Offset: 0x000216E0
		public Task<bool> c(SaslMethod A_0, bool A_1)
		{
			u.a a;
			a.c = this;
			a.e = A_0;
			a.d = A_1;
			a.b = AsyncTaskMethodBuilder<bool>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<bool> b = a.b;
			b.Start<u.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00022738 File Offset: 0x00021738
		private Task a(SaslMethod A_0, string A_1, string A_2, string A_3, string A_4, string A_5, bool A_6)
		{
			u.c c;
			c.d = this;
			c.c = A_0;
			c.i = A_1;
			c.h = A_2;
			c.g = A_3;
			c.e = A_4;
			c.f = A_5;
			c.j = A_6;
			c.b = AsyncTaskMethodBuilder.Create();
			c.a = -1;
			AsyncTaskMethodBuilder b = c.b;
			b.Start<u.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x0600076A RID: 1898
		protected abstract Task<bool> ef(AuthenticationMethods A_0, string A_1, string A_2, string A_3, bool A_4);

		// Token: 0x0600076B RID: 1899 RVA: 0x000227BC File Offset: 0x000217BC
		private Task<string> a(SaslMethod A_0, bool A_1)
		{
			u.b b;
			b.c = this;
			b.d = A_0;
			b.e = A_1;
			b.b = AsyncTaskMethodBuilder<string>.Create();
			b.a = -1;
			AsyncTaskMethodBuilder<string> b2 = b.b;
			b2.Start<u.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x0600076C RID: 1900
		protected abstract Task<bool> eg(string A_0, string A_1, string A_2);

		// Token: 0x0600076D RID: 1901 RVA: 0x00022814 File Offset: 0x00021814
		public virtual Task a(AuthenticationMethods A_0, AuthenticationMethods A_1, SaslMethod A_2, AuthenticationOptions A_3, string A_4, string A_5, string A_6, string A_7)
		{
			u.e e;
			e.j = this;
			e.d = A_0;
			e.e = A_1;
			e.h = A_2;
			e.c = A_3;
			e.q = A_4;
			e.l = A_5;
			e.k = A_6;
			e.m = A_7;
			e.b = AsyncTaskMethodBuilder.Create();
			e.a = -1;
			AsyncTaskMethodBuilder b = e.b;
			b.Start<u.e>(ref e);
			return e.b.Task;
		}

		// Token: 0x040004F1 RID: 1265
		protected ab a;

		// Token: 0x020004EA RID: 1258
		// (Invoke) Token: 0x06002A17 RID: 10775
		public delegate bool d(SaslMethod A_0, bool A_1);

		// Token: 0x020004EB RID: 1259
		// (Invoke) Token: 0x06002A1B RID: 10779
		public delegate Task<bool> f(SaslMethod A_0, bool A_1);
	}
}
