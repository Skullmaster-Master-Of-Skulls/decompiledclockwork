using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MailBee;
using MailBee.Pop3Mail;

namespace a.a
{
	// Token: 0x020003E4 RID: 996
	internal class f : u
	{
		// Token: 0x0600238B RID: 9099 RVA: 0x00094A69 File Offset: 0x00093A69
		public f(ab A_0) : base(A_0)
		{
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x00094A72 File Offset: 0x00093A72
		protected override IMailBeeLoginBadCredentialsException ea(int A_0, ai A_1, at A_2, string A_3, string A_4)
		{
			return new MailBeePop3LoginBadCredentialsException(A_0, A_1, A_2, A_3, A_4);
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x00094A80 File Offset: 0x00093A80
		protected override IMailBeeLoginBadMethodException eb(int A_0, ai A_1, at A_2, AuthenticationMethods A_3)
		{
			return new MailBeePop3LoginBadMethodException(A_0, A_1, A_2, A_3);
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x00094A8C File Offset: 0x00093A8C
		protected override bool ec(AuthenticationMethods A_0, string A_1, string A_2, string A_3, bool A_4)
		{
			if ((A_0 & AuthenticationMethods.Apop) == AuthenticationMethods.None)
			{
				return false;
			}
			this.a.a8().b(string.Format(Resources.Instance.Log_Pop3WillTryApopAuth, new object[0]), null, LogMessageType.Info, this.a);
			string text = ((c)this.a).k();
			if (text == null || text == string.Empty || Global.FipsMode)
			{
				if (A_4)
				{
					throw (MailBeeException)this.eb(114, this.a.a1(), this.a.ak(), AuthenticationMethods.Apop);
				}
				this.a.a8().b(string.Format(Resources.Instance.Log_Pop3ApopAuthNotSupported, new object[0]), null, LogMessageType.Info, this.a);
				return false;
			}
			else
			{
				if (A_2 == null || A_2 == string.Empty || A_3 == null || A_3 == string.Empty)
				{
					throw new MailBeeLoginNoCredentialsException(112, this.a.a1(), A_2, A_3);
				}
				if (this.a.o1(this.a(A_2, A_3, text), false))
				{
					return true;
				}
				if ((A_0 & AuthenticationMethods.Regular) == AuthenticationMethods.None || A_4)
				{
					throw new MailBeePop3LoginBadCredentialsException(113, this.a.a1(), this.a.ak(), A_2, A_3);
				}
				return false;
			}
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x00094BCC File Offset: 0x00093BCC
		private new string a(string A_0, string A_1, string A_2)
		{
			A_2 += A_1;
			return string.Format("APOP {0} {1}", A_0, this.a(A_2));
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x00094BEC File Offset: 0x00093BEC
		private new string a(string A_0)
		{
			byte[] array = new MD5CryptoServiceProvider().ComputeHash(this.a.a5().d().c.GetBytes(A_0));
			StringBuilder stringBuilder = new StringBuilder(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("X2"));
			}
			return stringBuilder.ToString().ToLower();
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x00094C5C File Offset: 0x00093C5C
		protected override bool ed(string A_0, string A_1, string A_2)
		{
			this.a.a8().b(string.Format(Resources.Instance.Log_Pop3WillTryRegularAuth, new object[0]), null, LogMessageType.Info, this.a);
			if (A_1 == null || A_1 == string.Empty || A_2 == null || A_2 == string.Empty)
			{
				throw new MailBeeLoginNoCredentialsException(112, this.a.a1(), A_1, A_2);
			}
			if (!this.a.o1("USER " + A_1, false))
			{
				throw new MailBeePop3LoginBadCredentialsException(113, this.a.a1(), this.a.ak(), A_1, A_2);
			}
			bf bf = this.a.fg(true);
			string text = this.a.o2("PASS " + A_2, bf);
			bf.j = (A_2.Length > 0);
			if (bf.j)
			{
				bf.k = this.a.a5().d().c.GetBytes(text.Replace(A_2, Global.PrivateDataCover));
			}
			if (!this.a.c(text, bf, false))
			{
				throw new MailBeePop3LoginBadCredentialsException(113, this.a.a1(), this.a.ak(), A_1, A_2);
			}
			return true;
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x00094DA0 File Offset: 0x00093DA0
		protected override Task<bool> ef(AuthenticationMethods A_0, string A_1, string A_2, string A_3, bool A_4)
		{
			global::a.a.f.a a;
			a.d = this;
			a.c = A_0;
			a.f = A_2;
			a.g = A_3;
			a.e = A_4;
			a.b = AsyncTaskMethodBuilder<bool>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<global::a.a.f.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x00094E08 File Offset: 0x00093E08
		protected override Task<bool> eg(string A_0, string A_1, string A_2)
		{
			global::a.a.f.b b;
			b.c = this;
			b.d = A_1;
			b.e = A_2;
			b.b = AsyncTaskMethodBuilder<bool>.Create();
			b.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<global::a.a.f.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x04001771 RID: 6001
		private new const string a = "USER";

		// Token: 0x04001772 RID: 6002
		private new const string b = "PASS";
	}
}
