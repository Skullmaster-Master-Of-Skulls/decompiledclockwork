using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using MailBee;
using MailBee.EwsMail;
using MailBee.Mime;
using Microsoft.Exchange.WebServices.Autodiscover;
using Microsoft.Exchange.WebServices.Data;

namespace a.l
{
	// Token: 0x02000218 RID: 536
	internal class d : bc
	{
		// Token: 0x0600115F RID: 4447 RVA: 0x0004D1FC File Offset: 0x0004C1FC
		static d()
		{
			ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(global::a.l.d.a);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0004D27C File Offset: 0x0004C27C
		internal d(bo A_0, bc A_1, Logger A_2, int A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0004D2E2 File Offset: 0x0004C2E2
		public override string er()
		{
			return " EWS";
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0004D2E9 File Offset: 0x0004C2E9
		protected override void fw(MailBeeException A_0)
		{
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0004D2EB File Offset: 0x0004C2EB
		protected override System.Threading.Tasks.Task f1(MailBeeException A_0)
		{
			return System.Threading.Tasks.Task.FromResult<int>(0);
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0004D2F3 File Offset: 0x0004C2F3
		public void q()
		{
			this.a(ExchangeVersion.Exchange2007_SP1);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0004D2FC File Offset: 0x0004C2FC
		public new void a(ExchangeVersion A_0)
		{
			this.a(A_0, TimeZoneInfo.Local);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0004D30A File Offset: 0x0004C30A
		public new void a(ExchangeVersion A_0, TimeZoneInfo A_1)
		{
			this.k = A_0;
			this.j = new ExchangeService(A_0, A_1);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0004D320 File Offset: 0x0004C320
		private new void e()
		{
			if (this.j == null)
			{
				throw new MailBeeInvalidStateException(11);
			}
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0004D334 File Offset: 0x0004C334
		public new void g(string A_0)
		{
			this.e();
			if (A_0 == null || A_0 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			try
			{
				this.j.AutodiscoverUrl(A_0, new AutodiscoverRedirectionUrlValidationCallback(this.d));
			}
			catch (FormatException ex)
			{
				throw new MailBeeInvalidArgumentException(ex.Message, 20);
			}
			catch (ServiceValidationException ex2)
			{
				throw new MailBeeInvalidArgumentException(ex2.Message, 20);
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x0004D3F0 File Offset: 0x0004C3F0
		public new void b(string A_0, string A_1)
		{
			this.e();
			if (A_0 == null)
			{
				this.j.UseDefaultCredentials = true;
				return;
			}
			this.j.UseDefaultCredentials = false;
			this.j.Credentials = new WebCredentials(A_0, A_1);
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x0004D428 File Offset: 0x0004C428
		public void h(string A_0)
		{
			this.e();
			if (A_0 == null || A_0 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			try
			{
				this.j.Url = new Uri(A_0);
			}
			catch (UriFormatException)
			{
				throw new MailBeeInvalidArgumentException(20);
			}
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x0004D480 File Offset: 0x0004C480
		private new bool d(string A_0)
		{
			bool result = false;
			if (new Uri(A_0).Scheme == "https")
			{
				result = true;
			}
			else if (this.i)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0004D4B8 File Offset: 0x0004C4B8
		private new static bool a(object A_0, X509Certificate A_1, X509Chain A_2, SslPolicyErrors A_3)
		{
			if (!global::a.l.d.g)
			{
				return true;
			}
			if (A_3 == SslPolicyErrors.None)
			{
				return true;
			}
			if ((A_3 & SslPolicyErrors.RemoteCertificateChainErrors) != SslPolicyErrors.None)
			{
				if (A_2 != null && A_2.ChainStatus != null)
				{
					foreach (X509ChainStatus x509ChainStatus in A_2.ChainStatus)
					{
						if ((!(A_1.Subject == A_1.Issuer) || x509ChainStatus.Status != X509ChainStatusFlags.UntrustedRoot) && x509ChainStatus.Status != X509ChainStatusFlags.NoError)
						{
							return false;
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0004D52C File Offset: 0x0004C52C
		public new static RemoteCertificateValidationCallback d()
		{
			return ServicePointManager.ServerCertificateValidationCallback;
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0004D533 File Offset: 0x0004C533
		public new static void a(RemoteCertificateValidationCallback A_0)
		{
			ServicePointManager.ServerCertificateValidationCallback = global::a.l.d.d();
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0004D53F File Offset: 0x0004C53F
		public new static bool c()
		{
			return global::a.l.d.g;
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0004D546 File Offset: 0x0004C546
		public new static void c(bool A_0)
		{
			global::a.l.d.g = A_0;
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0004D54E File Offset: 0x0004C54E
		public new static bool b()
		{
			return global::a.l.d.h;
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0004D555 File Offset: 0x0004C555
		public new static void b(bool A_0)
		{
			global::a.l.d.h = A_0;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0004D55D File Offset: 0x0004C55D
		public new bool g()
		{
			return this.i;
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0004D565 File Offset: 0x0004C565
		public new void e(bool A_0)
		{
			this.i = A_0;
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0004D56E File Offset: 0x0004C56E
		public char o()
		{
			return this.o.a();
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x0004D57B File Offset: 0x0004C57B
		public new void a(char A_0)
		{
			this.o.a(A_0);
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0004D589 File Offset: 0x0004C589
		public new bool l()
		{
			return this.p;
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0004D591 File Offset: 0x0004C591
		public new void f(bool A_0)
		{
			this.p = A_0;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0004D59A File Offset: 0x0004C59A
		public WellKnownFolderName h()
		{
			return this.r;
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x0004D5A2 File Offset: 0x0004C5A2
		public new void a(WellKnownFolderName A_0)
		{
			this.r = A_0;
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0004D5AB File Offset: 0x0004C5AB
		public DeleteMode n()
		{
			return this.q;
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x0004D5B3 File Offset: 0x0004C5B3
		public new void a(DeleteMode A_0)
		{
			this.q = A_0;
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0004D5BC File Offset: 0x0004C5BC
		public new string m()
		{
			return this.l;
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0004D5C4 File Offset: 0x0004C5C4
		public new void e(string A_0)
		{
			this.l = A_0;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0004D5D0 File Offset: 0x0004C5D0
		public new SearchFilter f()
		{
			if (this.n == null)
			{
				SearchFilter.IsEqualTo isEqualTo = new SearchFilter.IsEqualTo(FolderSchema.FolderClass, "IPF.Note");
				SearchFilter.Not not = new SearchFilter.Not(new SearchFilter.Exists(FolderSchema.FolderClass));
				this.n = new SearchFilter.SearchFilterCollection(LogicalOperator.Or, new SearchFilter[]
				{
					isEqualTo,
					not
				});
			}
			return this.n;
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0004D625 File Offset: 0x0004C625
		public new ExchangeService k()
		{
			return this.j;
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0004D62D File Offset: 0x0004C62D
		public new bool i()
		{
			return this.t;
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0004D635 File Offset: 0x0004C635
		public new void g(bool A_0)
		{
			this.t = A_0;
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0004D63E File Offset: 0x0004C63E
		private new SearchFilter a(bool A_0)
		{
			if (!A_0)
			{
				return null;
			}
			return new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false);
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0004D655 File Offset: 0x0004C655
		public new FolderView d(bool A_0)
		{
			return this.a(A_0, true, true, this.p);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0004D666 File Offset: 0x0004C666
		private new FolderView a(bool A_0, bool A_1, bool A_2, bool A_3)
		{
			return new FolderView(int.MaxValue)
			{
				Traversal = (A_0 ? FolderTraversal.Deep : FolderTraversal.Shallow),
				PropertySet = this.a(A_1, A_2, A_3)
			};
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0004D690 File Offset: 0x0004C690
		private new PropertySet a(bool A_0, bool A_1, bool A_2)
		{
			PropertySet propertySet = new PropertySet(A_0 ? BasePropertySet.FirstClassProperties : BasePropertySet.IdOnly);
			if (A_0 && this.k >= ExchangeVersion.Exchange2013 && !global::a.l.d.h)
			{
				propertySet.Add(FolderSchema.WellKnownFolderName);
			}
			if (A_1 && !A_0)
			{
				propertySet.Add(FolderSchema.DisplayName);
			}
			if (A_1)
			{
				propertySet.Add(global::a.l.d.d);
			}
			if (A_2)
			{
				propertySet.Add(global::a.l.d.a);
			}
			return propertySet;
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0004D6F6 File Offset: 0x0004C6F6
		private new ai a()
		{
			return new ai((this.j.Url == null) ? null : this.j.Url.ToString());
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0004D724 File Offset: 0x0004C724
		public void p()
		{
			this.e();
			try
			{
				WellKnownFolderName name = WellKnownFolderName.Inbox;
				this.d.b(string.Format(Resources.Instance.Log_EwsWillBindFolderId0, name.ToString()), null, LogMessageType.Info, this);
				Folder.Bind(this.j, name);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0004D7CC File Offset: 0x0004C7CC
		private new FolderId a(FolderId A_0)
		{
			if (A_0 != null)
			{
				return A_0;
			}
			return this.r;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0004D7E0 File Offset: 0x0004C7E0
		public new List<EwsFolder> a(FolderId A_0, FolderView A_1, SearchFilter A_2, bool A_3, bool? A_4, bool? A_5)
		{
			this.e();
			if (A_4 == null)
			{
				A_4 = new bool?(false);
			}
			if (A_5 == null)
			{
				A_5 = new bool?(this.p);
			}
			if (A_1 == null)
			{
				A_1 = this.a(A_4.Value, true, true, A_5.Value);
			}
			List<EwsFolder> result;
			try
			{
				FolderId folderId = this.a(A_0);
				this.d.b(string.Format(Resources.Instance.Log_EwsWillBindFolderId0, folderId), null, LogMessageType.Info, this);
				Folder folder = Folder.Bind(this.j, folderId, A_3 ? A_1.PropertySet : this.a(false, false, false));
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
				this.d.b(Resources.Instance.Log_EwsWillFindFolders, null, LogMessageType.Info, this);
				FindFoldersResults findFoldersResults = folder.FindFolders(A_2, A_1);
				this.d.b(string.Format(Resources.Instance.Log_EwsOperationDone0ItemsReturned, findFoldersResults.Folders.Count), null, LogMessageType.Info, this);
				List<EwsFolder> list = new List<EwsFolder>();
				if (A_3)
				{
					list.Add(new EwsFolder(folder, this.o));
				}
				foreach (Folder a_ in findFoldersResults)
				{
					list.Add(new EwsFolder(a_, this.o));
				}
				result = list;
			}
			catch (ServiceLocalException a_2)
			{
				throw new MailBeeEwsException(700, a_2, this.a());
			}
			catch (ServiceRemoteException a_3)
			{
				throw new MailBeeEwsException(701, a_3, this.a());
			}
			return result;
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0004D9BC File Offset: 0x0004C9BC
		public new EwsFolder a(FolderId A_0, PropertySet A_1)
		{
			this.e();
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (A_1 == null)
			{
				A_1 = this.a(true, true, this.p);
			}
			EwsFolder result;
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_EwsWillBindFolderId0, A_0), null, LogMessageType.Info, this);
				Folder a_ = Folder.Bind(this.j, A_0, A_1);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
				result = new EwsFolder(a_, this.o);
			}
			catch (ServiceLocalException a_2)
			{
				throw new MailBeeEwsException(700, a_2, this.a());
			}
			catch (ServiceRemoteException a_3)
			{
				throw new MailBeeEwsException(701, a_3, this.a());
			}
			return result;
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0004DA84 File Offset: 0x0004CA84
		private new bool a(FolderId A_0, string A_1)
		{
			global::a.l.d.a a = new global::a.l.d.a();
			a.a = A_1;
			this.d.b(string.Format(Resources.Instance.Log_EwsWillCheckFolderExistsByShortName0InParentFolderId1, a.a, this.a(A_0)), null, LogMessageType.Info, this);
			if (a.a == null || a.a == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			return this.a(A_0, this.a(false, false, true, false), new SearchFilter.IsEqualTo(FolderSchema.DisplayName, a.a), false, null, null).Exists(new Predicate<EwsFolder>(a.b));
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0004DB30 File Offset: 0x0004CB30
		private new bool c(string A_0)
		{
			global::a.l.d.e e = new global::a.l.d.e();
			e.a = A_0;
			this.d.b(string.Format(Resources.Instance.Log_EwsWillCheckFolderExistsByFullName0, e.a), null, LogMessageType.Info, this);
			if (e.a == null || e.a == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			return this.a(null, this.a(true, false, true, false), null, false, null, null).Exists(new Predicate<EwsFolder>(e.b));
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0004DBC5 File Offset: 0x0004CBC5
		public new bool b(FolderId A_0, string A_1, bool A_2)
		{
			if (A_2)
			{
				return this.c(A_1);
			}
			return this.a(A_0, A_1);
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0004DBDC File Offset: 0x0004CBDC
		public new EwsFolder d(FolderId A_0, string A_1)
		{
			global::a.l.d.d d = new global::a.l.d.d();
			d.a = A_1;
			this.d.b(string.Format(Resources.Instance.Log_EwsWillDownloadFolderByShortName0InParentFolderId1, d.a, this.a(A_0)), null, LogMessageType.Info, this);
			return this.a(A_0, null, new SearchFilter.IsEqualTo(FolderSchema.DisplayName, d.a), false, new bool?(false), null).Find(new Predicate<EwsFolder>(d.b));
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0004DC5C File Offset: 0x0004CC5C
		public new FolderId b(FolderId A_0, string A_1)
		{
			global::a.l.d.c c = new global::a.l.d.c();
			c.a = A_1;
			this.d.b(string.Format(Resources.Instance.Log_EwsWillFindFolderIdByShortName0InParentFolderId1, c.a, this.a(A_0)), null, LogMessageType.Info, this);
			FolderView a_ = this.a(false, false, true, false);
			EwsFolder ewsFolder = this.a(A_0, a_, new SearchFilter.IsEqualTo(FolderSchema.DisplayName, c.a), false, null, null).Find(new Predicate<EwsFolder>(c.b));
			if (ewsFolder != null)
			{
				return ewsFolder.Id;
			}
			return null;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0004DCF4 File Offset: 0x0004CCF4
		private new EwsFolder a(FolderId A_0, string A_1, bool A_2)
		{
			global::a.l.d.f f = new global::a.l.d.f();
			f.a = A_1;
			this.d.b(string.Format(Resources.Instance.Log_EwsWillDownloadFolderByFullName0InContainingFolderId1, f.a, this.a(A_0)), null, LogMessageType.Info, this);
			FolderView a_ = this.a(true, false, true, false);
			EwsFolder ewsFolder = this.a(A_0, a_, null, false, null, null).Find(new Predicate<EwsFolder>(f.b));
			if (ewsFolder == null)
			{
				return null;
			}
			if (!A_2)
			{
				return this.a(ewsFolder.Id, null);
			}
			return ewsFolder;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x0004DD88 File Offset: 0x0004CD88
		public new EwsFolder c(FolderId A_0, string A_1)
		{
			return this.a(A_0, A_1, false);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0004DD94 File Offset: 0x0004CD94
		public new FolderId e(FolderId A_0, string A_1)
		{
			EwsFolder ewsFolder = this.a(A_0, A_1, true);
			if (ewsFolder != null)
			{
				return ewsFolder.Id;
			}
			return null;
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0004DDB8 File Offset: 0x0004CDB8
		private new EwsFolder a(string A_0, bool A_1, int A_2)
		{
			this.d.b(string.Format(Resources.Instance.Log_EwsWillDownloadFolderByFullName0Recursively, A_0), null, LogMessageType.Info, this);
			if (A_2 < 0)
			{
				throw new MailBeeInvalidArgumentException(23);
			}
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			string[] array = A_0.Split(new char[]
			{
				this.o.a()
			});
			if (array.Length <= A_2)
			{
				A_2 = array.Length - 1;
			}
			FolderId a_ = this.r;
			FolderView a_2 = this.a(false, false, true, false);
			for (int i = 0; i < A_2; i++)
			{
				List<EwsFolder> list = this.a(a_, a_2, new SearchFilter.IsEqualTo(FolderSchema.DisplayName, array[i]), false, null, null);
				if (list.Count <= 0)
				{
					this.d.b(Resources.Instance.Log_EwsDownloadFolderByFullNameRecursivelyFoundNothing, null, LogMessageType.Info, this);
					return null;
				}
				a_ = list[0].Id;
			}
			return this.a(a_, A_0, A_1);
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0004DEB2 File Offset: 0x0004CEB2
		public new EwsFolder a(string A_0, int A_1)
		{
			return this.a(A_0, false, A_1);
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0004DEC0 File Offset: 0x0004CEC0
		public new FolderId b(string A_0, int A_1)
		{
			EwsFolder ewsFolder = this.a(A_0, true, A_1);
			if (ewsFolder != null)
			{
				return ewsFolder.Id;
			}
			return null;
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0004DEE4 File Offset: 0x0004CEE4
		public new EwsFolder a(string A_0, List<EwsFolder> A_1)
		{
			global::a.l.d.b b = new global::a.l.d.b();
			b.a = A_0;
			if (b.a == null || A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			return A_1.Find(new Predicate<EwsFolder>(b.b));
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0004DF24 File Offset: 0x0004CF24
		public new void b(string A_0, FolderId A_1)
		{
			this.e();
			Folder folder = new Folder(this.j);
			folder.DisplayName = A_0;
			folder.FolderClass = this.l;
			try
			{
				if (A_1 == null)
				{
					this.d.b(string.Format(Resources.Instance.Log_EwsWillCreateFolder0, A_0), null, LogMessageType.Info, this);
					folder.Save(this.r);
				}
				else
				{
					this.d.b(string.Format(Resources.Instance.Log_EwsWillCreateFolder0InId1, A_0, A_1), null, LogMessageType.Info, this);
					folder.Save(A_1);
				}
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0004E008 File Offset: 0x0004D008
		public new void a(string A_0, FolderId A_1)
		{
			this.e();
			if (A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (A_0 == null || A_0 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_EwsWillBindFolderId0, A_1), null, LogMessageType.Info, this);
				Folder folder = Folder.Bind(this.j, A_1);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
				folder.DisplayName = A_0;
				this.d.b(string.Format(Resources.Instance.Log_EwsWillRenameFolderTo0, A_0), null, LogMessageType.Info, this);
				folder.Update();
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0004E108 File Offset: 0x0004D108
		public new void a(FolderId A_0, FolderId A_1)
		{
			this.e();
			if (A_0 == null || A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_EwsWillBindFolderId0, A_0), null, LogMessageType.Info, this);
				Folder folder = Folder.Bind(this.j, A_0);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
				this.d.b(string.Format(Resources.Instance.Log_EwsWillMoveFolderToId0, A_1), null, LogMessageType.Info, this);
				folder.Move(A_1);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0004E1F0 File Offset: 0x0004D1F0
		private new string b(string A_0)
		{
			int num = A_0.LastIndexOf(this.o.a());
			if (num < 0)
			{
				return A_0;
			}
			return A_0.Substring(num + 1);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0004E220 File Offset: 0x0004D220
		private new string a(string A_0)
		{
			int num = A_0.LastIndexOf(this.o.a());
			if (num < 0)
			{
				return string.Empty;
			}
			return A_0.Substring(0, num);
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0004E254 File Offset: 0x0004D254
		private new bool a(string A_0, string A_1)
		{
			string text = this.a(A_0).ToLower();
			string text2 = this.a(A_1).ToLower();
			return text == text2;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0004E280 File Offset: 0x0004D280
		private new string a(string A_0, string A_1, char A_2)
		{
			global::a.l.d.h h = new global::a.l.d.h();
			if (!A_0.EndsWith(A_2.ToString()))
			{
				A_0 += A_2.ToString();
			}
			if (!A_1.EndsWith(A_2.ToString()))
			{
				A_1 += A_2.ToString();
			}
			h.a = new string[]
			{
				A_0,
				A_1
			};
			Enumerable.Range(0, h.a.Min(new Func<string, int>(global::a.l.d.<>c.<>9.a))).Reverse<int>();
			string text = Enumerable.Range(0, h.a.Min(new Func<string, int>(global::a.l.d.<>c.<>9.b)) + 1).Reverse<int>().Select(new Func<int, global::a<int, string>>(h.b)).Where(new Func<global::a<int, string>, bool>(h.b)).Select(new Func<global::a<int, string>, string>(global::a.l.d.<>c.<>9.a)).First<string>();
			int num = text.LastIndexOf(A_2);
			if (num > -1)
			{
				return text.Substring(0, num);
			}
			return string.Empty;
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0004E3B8 File Offset: 0x0004D3B8
		public new void a(string A_0, string A_1, List<EwsFolder> A_2)
		{
			this.d.b(string.Format(Resources.Instance.Log_EwsWillRenameOrMoveFolderWithOldFullName0ToNewFullName1, A_0, A_1), null, LogMessageType.Info, this);
			this.e();
			if (A_0 == null || A_0 == string.Empty || A_1 == null || A_1 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			FolderId folderId = null;
			FolderId folderId2 = null;
			if (A_2 != null)
			{
				if (this.a(A_1, A_2) != null)
				{
					this.d.b(string.Format(Resources.Instance.Log_EwsFolderFullName0AlreadyExists, A_1), null, LogMessageType.Info, this);
					throw new MailBeeInvalidArgumentException(711);
				}
				EwsFolder ewsFolder = this.a(A_0, A_2);
				if (ewsFolder == null)
				{
					this.d.b(string.Format(Resources.Instance.Log_EwsFolderFullName0NotFound, A_0), null, LogMessageType.Info, this);
					throw new MailBeeInvalidArgumentException(710);
				}
				folderId2 = ewsFolder.Id;
				this.d.b(string.Format(Resources.Instance.Log_EwsFolderFullName0MatchedToId1, A_0, folderId2), null, LogMessageType.Info, this);
			}
			string text = this.a(A_1);
			FolderId folderId3 = null;
			if (folderId2 == null)
			{
				string text2 = this.a(A_0, text, this.o.a());
				if (text2 != string.Empty)
				{
					folderId = this.b(text2, 3);
					if (folderId == null)
					{
						this.d.b(string.Format(Resources.Instance.Log_EwsFolderFullName0NotFound, text2), null, LogMessageType.Info, this);
						throw new MailBeeInvalidArgumentException(710);
					}
					if (text2.Equals(text, StringComparison.InvariantCultureIgnoreCase))
					{
						folderId3 = folderId;
					}
					if (text2.Equals(A_0, StringComparison.InvariantCultureIgnoreCase))
					{
						folderId2 = folderId;
					}
				}
				if (folderId2 == null)
				{
					if (folderId != null)
					{
						folderId2 = this.e(folderId, A_0);
					}
					else
					{
						folderId2 = this.b(A_0, 3);
					}
				}
			}
			if (folderId2 == null)
			{
				this.d.b(string.Format(Resources.Instance.Log_EwsFolderFullName0NotFound, A_0), null, LogMessageType.Info, this);
				throw new MailBeeInvalidArgumentException(710);
			}
			if (A_2 == null)
			{
				FolderId folderId4;
				if (folderId != null)
				{
					folderId4 = this.e(folderId, A_1);
				}
				else
				{
					folderId4 = this.b(A_1, 3);
				}
				if (folderId4 != null)
				{
					this.d.b(string.Format(Resources.Instance.Log_EwsFolderFullName0AlreadyExists, A_1), null, LogMessageType.Info, this);
					throw new MailBeeInvalidArgumentException(711);
				}
			}
			if (!this.a(A_1, A_0))
			{
				if (text == string.Empty)
				{
					this.a(folderId2, this.r);
				}
				else
				{
					if (A_2 != null)
					{
						EwsFolder ewsFolder2 = this.a(text, A_2);
						if (ewsFolder2 == null)
						{
							this.d.b(string.Format(Resources.Instance.Log_EwsFolderFullName0NotFound, text), null, LogMessageType.Info, this);
							throw new MailBeeInvalidArgumentException(710);
						}
						folderId3 = ewsFolder2.Id;
					}
					if (folderId3 == null)
					{
						if (folderId != null)
						{
							folderId3 = this.e(folderId, text);
						}
						else
						{
							folderId3 = this.b(text, 3);
						}
						if (folderId3 == null)
						{
							this.d.b(string.Format(Resources.Instance.Log_EwsFolderFullName0NotFound, text), null, LogMessageType.Info, this);
							throw new MailBeeInvalidArgumentException(710);
						}
					}
					this.a(folderId2, folderId3);
				}
			}
			string text3 = this.b(A_1);
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_EwsWillBindFolderId0, folderId2), null, LogMessageType.Info, this);
				Folder folder = Folder.Bind(this.j, folderId2);
				folder.DisplayName = text3;
				this.d.b(string.Format(Resources.Instance.Log_EwsWillRenameFolderTo0, text3), null, LogMessageType.Info, this);
				folder.Update();
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x0004E73C File Offset: 0x0004D73C
		public new void b(FolderId A_0)
		{
			this.e();
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_EwsWillBindFolderId0, A_0), null, LogMessageType.Info, this);
				Folder folder = Folder.Bind(this.j, A_0);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
				this.d.b(string.Format(Resources.Instance.Log_EwsWillDeleteFolderUsingMethod0, this.q.ToString()), null, LogMessageType.Info, this);
				folder.Delete(this.q);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0004E834 File Offset: 0x0004D834
		public new void a(FolderId A_0, bool A_1)
		{
			this.e();
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_EwsWillBindFolderId0, A_0), null, LogMessageType.Info, this);
				Folder folder = Folder.Bind(this.j, A_0);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
				this.d.b(string.Format(Resources.Instance.Log_EwsWillEmptyFolderUsingMethod0, this.q.ToString()), null, LogMessageType.Info, this);
				folder.Empty(this.q, A_1);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0004E92C File Offset: 0x0004D92C
		public new PropertySet a(EwsItemParts A_0)
		{
			return global::a.l.a.a(A_0, this.k);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0004E93C File Offset: 0x0004D93C
		public new EwsItemList a(FolderId A_0, ItemView A_1, bool A_2, EwsItemParts? A_3, PropertySet A_4)
		{
			this.e();
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			FindItemsResults<Item> findItemsResults = null;
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_EwsWillFindItemsInFolderId0, A_0), null, LogMessageType.Info, this);
				findItemsResults = this.j.FindItems(A_0, this.a(A_2), (A_1 == null) ? this.m : A_1);
				this.d.b(string.Format(Resources.Instance.Log_EwsOperationDone0ItemsReturned, findItemsResults.Items.Count), null, LogMessageType.Info, this);
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
			if (A_3 == null)
			{
				A_3 = new EwsItemParts?(EwsItemParts.GenericItem);
			}
			if (A_4 == null && A_3 != EwsItemParts.IdOnly)
			{
				A_4 = global::a.l.a.a(A_3.Value, this.k);
			}
			if (findItemsResults.Items.Count > 0 && A_4 != null)
			{
				try
				{
					this.d.b(Resources.Instance.Log_EwsWillLoadPropertiesForItems, null, LogMessageType.Info, this);
					this.j.LoadPropertiesForItems(findItemsResults, A_4);
					this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
				}
				catch (ServiceLocalException a_3)
				{
					throw new MailBeeEwsException(700, a_3, this.a());
				}
				catch (ServiceRemoteException a_4)
				{
					throw new MailBeeEwsException(701, a_4, this.a());
				}
			}
			EwsItemList ewsItemList = new EwsItemList(findItemsResults);
			foreach (Item a_5 in findItemsResults)
			{
				ewsItemList.Add(new EwsItem(a_5, this.k));
			}
			return ewsItemList;
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0004EB3C File Offset: 0x0004DB3C
		public new EwsItemList a(FolderId A_0, int A_1, int A_2, bool A_3, EwsItemParts? A_4, PropertySet A_5)
		{
			ItemView a_ = new ItemView(A_2, A_1);
			return this.a(A_0, a_, A_3, A_4, A_5);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0004EB60 File Offset: 0x0004DB60
		public new EwsItemList a(IEnumerable<EwsItem> A_0, EwsItemParts? A_1, PropertySet A_2)
		{
			this.e();
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			List<Item> list = list = new List<Item>();
			foreach (EwsItem ewsItem in A_0)
			{
				list.Add(ewsItem.NativeItem);
			}
			if (A_1 == null)
			{
				A_1 = new EwsItemParts?(EwsItemParts.GenericItem);
			}
			if (A_2 == null && A_1 != EwsItemParts.IdOnly)
			{
				A_2 = global::a.l.a.a(A_1.Value, this.k);
			}
			if (list.Count > 0 && A_2 != null)
			{
				try
				{
					this.d.b(Resources.Instance.Log_EwsWillLoadPropertiesForItems, null, LogMessageType.Info, this);
					this.j.LoadPropertiesForItems(list, A_2);
					this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
				}
				catch (ServiceLocalException a_)
				{
					throw new MailBeeEwsException(700, a_, this.a());
				}
				catch (ServiceRemoteException a_2)
				{
					throw new MailBeeEwsException(701, a_2, this.a());
				}
			}
			EwsItemList ewsItemList = new EwsItemList(null);
			foreach (Item a_3 in list)
			{
				ewsItemList.Add(new EwsItem(a_3, this.k));
			}
			return ewsItemList;
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0004ECF0 File Offset: 0x0004DCF0
		public new EwsItem a(ItemId A_0, EwsItemParts? A_1, PropertySet A_2)
		{
			this.e();
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (A_1 == null)
			{
				A_1 = new EwsItemParts?(EwsItemParts.GenericItem);
			}
			if (A_2 == null)
			{
				A_2 = global::a.l.a.a(A_1.Value, this.k);
			}
			Item a_ = null;
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_EwsWillBindItemId0, A_0), null, LogMessageType.Info, this);
				a_ = Item.Bind(this.j, A_0, A_2);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
			}
			catch (ServiceLocalException a_2)
			{
				throw new MailBeeEwsException(700, a_2, this.a());
			}
			catch (ServiceRemoteException a_3)
			{
				throw new MailBeeEwsException(701, a_3, this.a());
			}
			return new EwsItem(a_, this.k);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0004EDCC File Offset: 0x0004DDCC
		public new MailMessage c(ItemId A_0)
		{
			return this.a(A_0, new EwsItemParts?(EwsItemParts.MailMessageRawData), null).MailBeeMessage;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0004EDE4 File Offset: 0x0004DDE4
		public new List<Microsoft.Exchange.WebServices.Data.Attachment> a(string[] A_0, bool A_1)
		{
			this.e();
			if (A_0 == null || A_0.Length == 0)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			ServiceResponseCollection<GetAttachmentResponse> serviceResponseCollection = null;
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_EwsWillGet0Attachments, A_0.Length.ToString()), null, LogMessageType.Info, this);
				serviceResponseCollection = this.j.GetAttachments(A_0, null, new PropertySet(new PropertyDefinitionBase[]
				{
					ItemSchema.MimeContent
				}));
				this.d.b(string.Format(Resources.Instance.Log_EwsOperationDone0ItemsReturned, serviceResponseCollection.Count), null, LogMessageType.Info, this);
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
			List<Microsoft.Exchange.WebServices.Data.Attachment> list = new List<Microsoft.Exchange.WebServices.Data.Attachment>();
			foreach (GetAttachmentResponse getAttachmentResponse in serviceResponseCollection)
			{
				if (getAttachmentResponse.Result == ServiceResult.Success)
				{
					if (getAttachmentResponse.Attachment is FileAttachment)
					{
						list.Add(getAttachmentResponse.Attachment);
					}
					else if (getAttachmentResponse.Attachment is ItemAttachment && !A_1)
					{
						list.Add(getAttachmentResponse.Attachment);
					}
				}
			}
			this.d.b(string.Format(Resources.Instance.Log_Ews0AttachmentsReturned, list.Count), null, LogMessageType.Info, this);
			return list;
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0004EF74 File Offset: 0x0004DF74
		public new FileAttachment a(ItemId A_0, string A_1, string A_2)
		{
			this.e();
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (A_1 == null || A_1 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			FileAttachment result;
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_EwsWillBindItemId0, A_0), null, LogMessageType.Info, this);
				Item item = Item.Bind(this.j, A_0);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
				if (A_2 == null)
				{
					result = item.Attachments.AddFileAttachment(A_1);
				}
				else
				{
					result = item.Attachments.AddFileAttachment(A_2, A_1);
				}
				this.d.b(Resources.Instance.Log_EwsWillAddAttachmentAndUpdateItem, null, LogMessageType.Info, this);
				item.Update(ConflictResolutionMode.AlwaysOverwrite);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
			return result;
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0004F08C File Offset: 0x0004E08C
		public new void b(ItemId A_0)
		{
			this.e();
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_EwsWillBindItemId0, A_0), null, LogMessageType.Info, this);
				Item item = Item.Bind(this.j, A_0, new PropertySet(new PropertyDefinitionBase[]
				{
					ItemSchema.Attachments
				}));
				item.Attachments.Clear();
				this.d.b(Resources.Instance.Log_EwsWillDeleteAllAttachmentsAndUpdateItem, null, LogMessageType.Info, this);
				item.Update(ConflictResolutionMode.AlwaysOverwrite);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0004F170 File Offset: 0x0004E170
		public new int a(ItemId A_0, string A_1, bool A_2, bool A_3)
		{
			this.e();
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (A_1 == null || A_1 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			int result;
			try
			{
				int num = 0;
				this.d.b(string.Format(Resources.Instance.Log_EwsWillBindItemId0, A_0), null, LogMessageType.Info, this);
				Item item = Item.Bind(this.j, A_0, new PropertySet(new PropertyDefinitionBase[]
				{
					ItemSchema.Attachments
				}));
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
				string text = null;
				if (!A_2)
				{
					text = A_1.ToLower();
				}
				bool flag;
				do
				{
					flag = false;
					foreach (Microsoft.Exchange.WebServices.Data.Attachment attachment in item.Attachments)
					{
						if ((!A_2 && attachment.Name.ToLower() == text) || (A_2 && attachment.Id == A_1))
						{
							item.Attachments.Remove(attachment);
							num++;
							flag = !A_3;
							break;
						}
					}
				}
				while (flag);
				if (num > 0)
				{
					this.d.b(string.Format(Resources.Instance.Log_EwsWillDelete0AttachmentsAndUpdateItem, num), null, LogMessageType.Info, this);
					item.Update(ConflictResolutionMode.AlwaysOverwrite);
					this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
				}
				else
				{
					this.d.b(Resources.Instance.Log_EwsAttachmentNotFound, null, LogMessageType.Info, this);
				}
				result = num;
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
			return result;
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0004F360 File Offset: 0x0004E360
		public new void a(EwsItem A_0)
		{
			this.e();
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_EwsWillUpdateItemId0, A_0.Id), null, LogMessageType.Info, this);
				A_0.NativeMessage.Update(ConflictResolutionMode.AlwaysOverwrite);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0004F40C File Offset: 0x0004E40C
		public new void a(FolderId A_0, byte[] A_1, bool A_2)
		{
			this.e();
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (A_1 == null || A_1.Length == 0)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			EmailMessage emailMessage = new EmailMessage(this.j);
			emailMessage.MimeContent = new MimeContent(this.s, A_1);
			if (!A_2)
			{
				ExtendedPropertyDefinition extendedPropertyDefinition = new ExtendedPropertyDefinition(3591, MapiPropertyType.Integer);
				emailMessage.SetExtendedProperty(extendedPropertyDefinition, 1);
			}
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_EwsWillUpload0BytesMessageIntoFolderId1, A_1.Length, A_0), null, LogMessageType.Info, this);
				emailMessage.Save(A_0);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0004F500 File Offset: 0x0004E500
		public new ItemId b(ItemId A_0, FolderId A_1)
		{
			this.e();
			if (A_0 == null || A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			ItemId id;
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_EwsWillBindItemId0, A_0), null, LogMessageType.Info, this);
				Item item = Item.Bind(this.j, A_0, PropertySet.IdOnly);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
				this.d.b(string.Format(Resources.Instance.Log_EwsWillCopyItemToFolderId0, A_1), null, LogMessageType.Info, this);
				Item item2 = item.Copy(A_1);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
				id = item2.Id;
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
			return id;
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0004F5F0 File Offset: 0x0004E5F0
		public new ItemId a(ItemId A_0, FolderId A_1)
		{
			this.e();
			if (A_0 == null || A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			ItemId id;
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_EwsWillBindItemId0, A_0), null, LogMessageType.Info, this);
				Item item = Item.Bind(this.j, A_0, PropertySet.IdOnly);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
				this.d.b(string.Format(Resources.Instance.Log_EwsWillMoveItemToFolderId0, A_1), null, LogMessageType.Info, this);
				Item item2 = item.Move(A_1);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
				id = item2.Id;
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
			return id;
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0004F6E0 File Offset: 0x0004E6E0
		public new void a(ItemId A_0)
		{
			this.e();
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_EwsWillBindItemId0, A_0), null, LogMessageType.Info, this);
				Item item = Item.Bind(this.j, A_0, PropertySet.IdOnly);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
				this.d.b(string.Format(Resources.Instance.Log_EwsWillDeleteItemUsingMethod0, this.q.ToString()), null, LogMessageType.Info, this);
				item.Delete(this.q);
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0004F7DC File Offset: 0x0004E7DC
		public new List<ItemId> a(IEnumerable<ItemId> A_0)
		{
			this.e();
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			ServiceResponseCollection<ServiceResponse> serviceResponseCollection = null;
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_EwsWillDeleteItemsUsingMethod0, this.q.ToString()), null, LogMessageType.Info, this);
				serviceResponseCollection = this.j.DeleteItems(A_0, this.q, new SendCancellationsMode?(SendCancellationsMode.SendToNone), new AffectedTaskOccurrence?(AffectedTaskOccurrence.AllOccurrences));
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
			List<ItemId> list = new List<ItemId>();
			IEnumerator<ItemId> enumerator = A_0.GetEnumerator();
			IEnumerator<ServiceResponse> enumerator2 = serviceResponseCollection.GetEnumerator();
			while (enumerator.MoveNext() && enumerator2.MoveNext())
			{
				if (enumerator2.Current.Result != ServiceResult.Error)
				{
					list.Add(enumerator.Current);
				}
			}
			this.d.b(string.Format(Resources.Instance.Log_EwsOperationDone0MessagesDeleted, list.Count), null, LogMessageType.Info, this);
			return list;
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0004F8FC File Offset: 0x0004E8FC
		public new EwsItemList a(FolderId A_0, SearchFilter A_1, ItemView A_2)
		{
			this.e();
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			FindItemsResults<Item> findItemsResults = null;
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_EwsWillFindItemsInFolderId0, A_0), null, LogMessageType.Info, this);
				findItemsResults = this.j.FindItems(A_0, A_1, (A_2 == null) ? this.m : A_2);
				this.d.b(string.Format(Resources.Instance.Log_EwsOperationDone0ItemsReturned, findItemsResults.Items.Count), null, LogMessageType.Info, this);
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
			EwsItemList ewsItemList = new EwsItemList(findItemsResults);
			foreach (Item a_3 in findItemsResults.Items)
			{
				ewsItemList.Add(new EwsItem(a_3, this.k));
			}
			return ewsItemList;
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x0004FA1C File Offset: 0x0004EA1C
		public new List<EwsItem> a(FolderId A_0, SearchFilter A_1)
		{
			return this.a(A_0, A_1, null);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0004FA28 File Offset: 0x0004EA28
		public new static List<ItemId> a(IEnumerable<EwsItem> A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			List<ItemId> list = new List<ItemId>();
			foreach (EwsItem ewsItem in A_0)
			{
				list.Add(ewsItem.Id);
			}
			return list;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0004FA88 File Offset: 0x0004EA88
		public new void a(MailMessage A_0, bool A_1, FolderId A_2)
		{
			this.e();
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			EmailMessage emailMessage = new EmailMessage(this.j);
			emailMessage.MimeContent = new MimeContent(this.s, A_0.GetMessageRawData());
			try
			{
				if (!A_1)
				{
					this.d.b(Resources.Instance.Log_EwsWillSendEmail, null, LogMessageType.Info, this);
					emailMessage.Send();
				}
				else if (A_2 == null)
				{
					this.d.b(Resources.Instance.Log_EwsWillSendEmailAndSaveCopy, null, LogMessageType.Info, this);
					emailMessage.SendAndSaveCopy();
				}
				else
				{
					this.d.b(string.Format(Resources.Instance.Log_EwsWillSendEmailAndSaveCopyInFolderId0, A_2), null, LogMessageType.Info, this);
					emailMessage.SendAndSaveCopy(A_2);
				}
				this.d.b(Resources.Instance.Log_EwsOperationDone, null, LogMessageType.Info, this);
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0004FB90 File Offset: 0x0004EB90
		public new MailBee.Mime.EmailAddressCollection f(string A_0)
		{
			this.e();
			if (A_0 == null || A_0 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			NameResolutionCollection nameResolutionCollection = null;
			try
			{
				this.d.b(string.Format(Resources.Instance.Log_EwsWillResolveName0, A_0), null, LogMessageType.Info, this);
				nameResolutionCollection = this.j.ResolveName(A_0);
			}
			catch (ServiceLocalException a_)
			{
				throw new MailBeeEwsException(700, a_, this.a());
			}
			catch (ServiceRemoteException a_2)
			{
				throw new MailBeeEwsException(701, a_2, this.a());
			}
			MailBee.Mime.EmailAddressCollection emailAddressCollection = new MailBee.Mime.EmailAddressCollection();
			foreach (NameResolution nameResolution in nameResolutionCollection)
			{
				emailAddressCollection.Add(nameResolution.Mailbox.Address, nameResolution.Mailbox.Name);
			}
			this.d.b(string.Format(Resources.Instance.Log_EwsOperationDone0ItemsReturned, emailAddressCollection.Count), null, LogMessageType.Info, this);
			return emailAddressCollection;
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0004FCB0 File Offset: 0x0004ECB0
		public new string j()
		{
			if (this.j.ServerInfo != null)
			{
				return this.j.ServerInfo.VersionString;
			}
			return null;
		}

		// Token: 0x04000EFF RID: 3839
		public new static readonly ExtendedPropertyDefinition a = new ExtendedPropertyDefinition(3592, MapiPropertyType.Long);

		// Token: 0x04000F00 RID: 3840
		public new static readonly ExtendedPropertyDefinition b = new ExtendedPropertyDefinition(4115, MapiPropertyType.Binary);

		// Token: 0x04000F01 RID: 3841
		public new static readonly ExtendedPropertyDefinition c = new ExtendedPropertyDefinition(4096, MapiPropertyType.String);

		// Token: 0x04000F02 RID: 3842
		public new static readonly ExtendedPropertyDefinition d = new ExtendedPropertyDefinition(26293, MapiPropertyType.String);

		// Token: 0x04000F03 RID: 3843
		public new static readonly ExtendedPropertyDefinition e = new ExtendedPropertyDefinition(16350, MapiPropertyType.Integer);

		// Token: 0x04000F04 RID: 3844
		private new const int f = 3;

		// Token: 0x04000F05 RID: 3845
		private new static bool g = false;

		// Token: 0x04000F06 RID: 3846
		private static bool h = true;

		// Token: 0x04000F07 RID: 3847
		private new bool i;

		// Token: 0x04000F08 RID: 3848
		private new ExchangeService j;

		// Token: 0x04000F09 RID: 3849
		private new ExchangeVersion k;

		// Token: 0x04000F0A RID: 3850
		private new string l = "IPF.Note";

		// Token: 0x04000F0B RID: 3851
		private new ItemView m = new ItemView(int.MaxValue)
		{
			PropertySet = BasePropertySet.IdOnly
		};

		// Token: 0x04000F0C RID: 3852
		private SearchFilter n;

		// Token: 0x04000F0D RID: 3853
		private global::a.l.b<char> o = new global::a.l.b<char>('/');

		// Token: 0x04000F0E RID: 3854
		private bool p;

		// Token: 0x04000F0F RID: 3855
		private DeleteMode q;

		// Token: 0x04000F10 RID: 3856
		private WellKnownFolderName r = WellKnownFolderName.MsgFolderRoot;

		// Token: 0x04000F11 RID: 3857
		private string s = "UTF-8";

		// Token: 0x04000F12 RID: 3858
		private bool t = true;

		// Token: 0x0200021A RID: 538
		[CompilerGenerated]
		private new sealed class a
		{
			// Token: 0x060011E6 RID: 4582 RVA: 0x00050301 File Offset: 0x0004F301
			internal bool b(EwsFolder A_0)
			{
				return A_0.ShortName.ToLower() == this.a.ToLower();
			}

			// Token: 0x04000F22 RID: 3874
			public string a;
		}

		// Token: 0x0200021B RID: 539
		[CompilerGenerated]
		private new sealed class e
		{
			// Token: 0x060011E8 RID: 4584 RVA: 0x00050326 File Offset: 0x0004F326
			internal bool b(EwsFolder A_0)
			{
				return A_0.FullNameSafe.ToLower() == this.a.ToLower();
			}

			// Token: 0x04000F23 RID: 3875
			public string a;
		}

		// Token: 0x0200021C RID: 540
		[CompilerGenerated]
		private new sealed class d
		{
			// Token: 0x060011EA RID: 4586 RVA: 0x0005034B File Offset: 0x0004F34B
			internal bool b(EwsFolder A_0)
			{
				return A_0.ShortName.ToLower() == this.a.ToLower();
			}

			// Token: 0x04000F24 RID: 3876
			public string a;
		}

		// Token: 0x0200021D RID: 541
		[CompilerGenerated]
		private new sealed class c
		{
			// Token: 0x060011EC RID: 4588 RVA: 0x00050370 File Offset: 0x0004F370
			internal bool b(EwsFolder A_0)
			{
				return A_0.ShortName.ToLower() == this.a.ToLower();
			}

			// Token: 0x04000F25 RID: 3877
			public string a;
		}

		// Token: 0x0200021E RID: 542
		[CompilerGenerated]
		private new sealed class f
		{
			// Token: 0x060011EE RID: 4590 RVA: 0x00050395 File Offset: 0x0004F395
			internal bool b(EwsFolder A_0)
			{
				return A_0.FullNameSafe.ToLower() == this.a.ToLower();
			}

			// Token: 0x04000F26 RID: 3878
			public string a;
		}

		// Token: 0x0200021F RID: 543
		[CompilerGenerated]
		private new sealed class b
		{
			// Token: 0x060011F0 RID: 4592 RVA: 0x000503BA File Offset: 0x0004F3BA
			internal bool b(EwsFolder A_0)
			{
				return A_0.FullNameSafe.ToLower() == this.a.ToLower();
			}

			// Token: 0x04000F27 RID: 3879
			public string a;
		}

		// Token: 0x02000220 RID: 544
		[CompilerGenerated]
		private sealed class h
		{
			// Token: 0x060011F2 RID: 4594 RVA: 0x000503DF File Offset: 0x0004F3DF
			internal global::a<int, string> b(int A_0)
			{
				return new global::a<int, string>(A_0, this.a.First<string>().Substring(0, A_0));
			}

			// Token: 0x060011F3 RID: 4595 RVA: 0x000503FC File Offset: 0x0004F3FC
			internal bool b(global::a<int, string> A_0)
			{
				global::a.l.d.g g = new global::a.l.d.g();
				g.a = A_0;
				return this.a.All(new Func<string, bool>(g.b));
			}

			// Token: 0x04000F28 RID: 3880
			public string[] a;
		}

		// Token: 0x02000221 RID: 545
		[CompilerGenerated]
		private new sealed class g
		{
			// Token: 0x060011F5 RID: 4597 RVA: 0x00050435 File Offset: 0x0004F435
			internal bool b(string A_0)
			{
				return A_0.StartsWith(this.a.possibleMatch, StringComparison.InvariantCultureIgnoreCase);
			}

			// Token: 0x04000F29 RID: 3881
			public global::a<int, string> a;
		}
	}
}
