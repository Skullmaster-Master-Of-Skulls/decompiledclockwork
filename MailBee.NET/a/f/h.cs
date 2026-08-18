using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MailBee;
using MailBee.ImapMail;

namespace a.f
{
	// Token: 0x020000E1 RID: 225
	internal class h : u
	{
		// Token: 0x06000753 RID: 1875 RVA: 0x00021DE8 File Offset: 0x00020DE8
		public h(ab A_0) : base(A_0)
		{
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00021DF1 File Offset: 0x00020DF1
		protected override IMailBeeLoginBadCredentialsException ea(int A_0, ai A_1, at A_2, string A_3, string A_4)
		{
			return new MailBeeImapLoginBadCredentialsException(A_0, A_1, A_2, A_3, A_4);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00021DFF File Offset: 0x00020DFF
		protected override IMailBeeLoginBadMethodException eb(int A_0, ai A_1, at A_2, AuthenticationMethods A_3)
		{
			return new MailBeeImapLoginBadMethodException(A_0, A_1, A_2, A_3);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00021E0B File Offset: 0x00020E0B
		protected override bool ec(AuthenticationMethods A_0, string A_1, string A_2, string A_3, bool A_4)
		{
			return false;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00021E0E File Offset: 0x00020E0E
		protected override bf hg(bool A_0)
		{
			if (A_0)
			{
				return ((t)this.a).p();
			}
			return base.hg(true);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00021E2C File Offset: 0x00020E2C
		protected override bool ed(string A_0, string A_1, string A_2)
		{
			if (this.a.t("LOGINDISABLED") != null)
			{
				return false;
			}
			this.a.a8().b(string.Format(Resources.Instance.Log_ImapWillTryRegularAuth, new object[0]), null, LogMessageType.Info, this.a);
			if (A_1 == null || A_1 == string.Empty || A_2 == null || A_2 == string.Empty)
			{
				throw new MailBeeLoginNoCredentialsException(112, this.a.a1(), A_1, A_2);
			}
			v v = new v(true, false, false, null);
			string text = this.a.o2(string.Concat(new string[]
			{
				"LOGIN \"",
				global::a.f.b.a(A_1),
				"\" \"",
				global::a.f.b.a(A_2),
				"\""
			}), v);
			v.j = (A_2.Length > 0);
			if (v.j)
			{
				v.k = this.a.a5().d().c.GetBytes(text.Replace(A_2, Global.PrivateDataCover));
			}
			if (!this.a.c(text, v, false))
			{
				throw new MailBeeImapLoginBadCredentialsException(113, this.a.a1(), this.a.ak(), A_1, A_2);
			}
			return true;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00021F6F File Offset: 0x00020F6F
		protected override Task<bool> ef(AuthenticationMethods A_0, string A_1, string A_2, string A_3, bool A_4)
		{
			return Task.FromResult<bool>(false);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00021F78 File Offset: 0x00020F78
		protected override Task<bool> eg(string A_0, string A_1, string A_2)
		{
			h.a a;
			a.c = this;
			a.d = A_1;
			a.e = A_2;
			a.b = AsyncTaskMethodBuilder<bool>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<bool> b = a.b;
			b.Start<h.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x040004F0 RID: 1264
		private new const string a = "LOGIN";
	}
}
