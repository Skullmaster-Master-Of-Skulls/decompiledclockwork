using System;
using System.Collections.Generic;
using System.Linq;
using MailBee;
using MailBee.EwsMail;
using MailBee.Mime;
using Microsoft.Exchange.WebServices.Data;

namespace a.l
{
	// Token: 0x02000223 RID: 547
	internal class c : bo, a0
	{
		// Token: 0x060011FB RID: 4603 RVA: 0x00050475 File Offset: 0x0004F475
		public c(Ews A_0)
		{
			this.a = A_0;
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00050484 File Offset: 0x0004F484
		protected override void f9()
		{
			this.p = new d(this, null, this.m, 0);
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0005049A File Offset: 0x0004F49A
		public new d c()
		{
			return this.p as d;
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x000504A7 File Offset: 0x0004F4A7
		public override bool j()
		{
			return this.a != null && this.a.a();
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x000504BE File Offset: 0x0004F4BE
		public override void k(ErrorEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnErrorOccurred(A_0);
			}
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x000504D4 File Offset: 0x0004F4D4
		public override bool l()
		{
			return this.a != null && this.a.b();
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x000504EB File Offset: 0x0004F4EB
		public override void m(LogNewEntryEventArgs A_0)
		{
			if (this.a != null)
			{
				this.a.OnLogNewEntry(A_0);
			}
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00050501 File Offset: 0x0004F501
		public new void e()
		{
			((d)this.p).q();
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00050513 File Offset: 0x0004F513
		public new void a(ExchangeVersion A_0)
		{
			((d)this.p).a(A_0);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00050526 File Offset: 0x0004F526
		public new void a(ExchangeVersion A_0, TimeZoneInfo A_1)
		{
			((d)this.p).a(A_0, A_1);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0005053A File Offset: 0x0004F53A
		private void b(string A_0)
		{
			this.p.pa();
			((d)this.p).g(A_0);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00050558 File Offset: 0x0004F558
		public bool b(bool A_0, string A_1)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.b(A_1);
				}
				else
				{
					try
					{
						this.b(A_1);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return false;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return true;
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x000505E0 File Offset: 0x0004F5E0
		private new void a()
		{
			this.p.pa();
			((d)this.p).p();
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00050600 File Offset: 0x0004F600
		public new bool a(bool A_0)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.a();
				}
				else
				{
					try
					{
						this.a();
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return false;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return true;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00050684 File Offset: 0x0004F684
		private new List<EwsFolder> a(FolderId A_0, FolderView A_1, SearchFilter A_2, bool A_3, bool? A_4)
		{
			this.p.pa();
			return ((d)this.p).a(A_0, A_1, A_2, A_3, A_4, null);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x000506BC File Offset: 0x0004F6BC
		private new List<EwsFolder> a(bool A_0, FolderId A_1, FolderView A_2, SearchFilter A_3, bool A_4, bool? A_5)
		{
			List<EwsFolder> result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.a(A_1, A_2, A_3, A_4, A_5);
				}
				else
				{
					try
					{
						result = this.a(A_1, A_2, A_3, A_4, A_5);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return null;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return result;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00050754 File Offset: 0x0004F754
		public new List<EwsFolder> a(bool A_0, FolderId A_1, FolderView A_2, SearchFilter A_3, bool A_4)
		{
			return this.a(A_0, A_1, A_2, A_3, A_4, null);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00050777 File Offset: 0x0004F777
		public new List<EwsFolder> a(bool A_0, FolderId A_1, bool A_2, bool A_3)
		{
			return this.a(A_0, A_1, null, null, A_2, new bool?(A_3));
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0005078B File Offset: 0x0004F78B
		public new List<EwsFolder> a(bool A_0, bool A_1, bool A_2)
		{
			return this.a(A_0, null, A_1, A_2);
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00050797 File Offset: 0x0004F797
		private new bool a(FolderId A_0, string A_1, bool A_2)
		{
			this.p.pa();
			return ((d)this.p).b(A_0, A_1, A_2);
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x000507B8 File Offset: 0x0004F7B8
		private new bool a(bool A_0, FolderId A_1, string A_2, bool A_3)
		{
			bool result = false;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.a(A_1, A_2, A_3);
				}
				else
				{
					try
					{
						result = this.a(A_1, A_2, A_3);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return false;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return result;
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00050848 File Offset: 0x0004F848
		public bool d(bool A_0, FolderId A_1, string A_2)
		{
			return this.a(A_0, A_1, A_2, false);
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00050854 File Offset: 0x0004F854
		public new bool c(bool A_0, string A_1)
		{
			return this.a(A_0, null, A_1, true);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00050860 File Offset: 0x0004F860
		private EwsFolder b(FolderId A_0, string A_1, bool A_2, int A_3)
		{
			this.p.pa();
			d d = (d)this.p;
			if (!A_2)
			{
				return d.d(A_0, A_1);
			}
			if (A_3 <= 0)
			{
				return d.c(A_0, A_1);
			}
			return d.a(A_1, A_3);
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x000508A8 File Offset: 0x0004F8A8
		private EwsFolder b(bool A_0, FolderId A_1, string A_2, bool A_3, int A_4)
		{
			EwsFolder result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.b(A_1, A_2, A_3, A_4);
				}
				else
				{
					try
					{
						result = this.b(A_1, A_2, A_3, A_4);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return null;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return result;
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0005093C File Offset: 0x0004F93C
		public new EwsFolder c(bool A_0, FolderId A_1, string A_2)
		{
			return this.b(A_0, A_1, A_2, false, 0);
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00050949 File Offset: 0x0004F949
		public EwsFolder b(bool A_0, FolderId A_1, string A_2)
		{
			return this.b(A_0, A_1, A_2, true, 0);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00050956 File Offset: 0x0004F956
		public new EwsFolder a(bool A_0, string A_1, int A_2)
		{
			return this.b(A_0, null, A_1, true, A_2);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00050963 File Offset: 0x0004F963
		private new EwsFolder a(FolderId A_0, PropertySet A_1)
		{
			this.p.pa();
			return ((d)this.p).a(A_0, A_1);
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00050984 File Offset: 0x0004F984
		public new EwsFolder a(bool A_0, FolderId A_1, PropertySet A_2)
		{
			EwsFolder result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.a(A_1, A_2);
				}
				else
				{
					try
					{
						result = this.a(A_1, A_2);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return null;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return result;
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00050A10 File Offset: 0x0004FA10
		public new EwsFolder a(bool A_0, FolderId A_1)
		{
			return this.a(A_0, A_1, null);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00050A1C File Offset: 0x0004FA1C
		private new FolderId a(FolderId A_0, string A_1, bool A_2, int A_3)
		{
			this.p.pa();
			d d = (d)this.p;
			if (!A_2)
			{
				return d.b(A_0, A_1);
			}
			if (A_3 <= 0)
			{
				return d.e(A_0, A_1);
			}
			return d.b(A_1, A_3);
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00050A64 File Offset: 0x0004FA64
		private new FolderId a(bool A_0, FolderId A_1, string A_2, bool A_3, int A_4)
		{
			FolderId result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.a(A_1, A_2, A_3, A_4);
				}
				else
				{
					try
					{
						result = this.a(A_1, A_2, A_3, A_4);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return null;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return result;
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00050AF8 File Offset: 0x0004FAF8
		public new FolderId a(bool A_0, FolderId A_1, string A_2)
		{
			return this.a(A_0, A_1, A_2, false, 0);
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00050B05 File Offset: 0x0004FB05
		public new FolderId e(bool A_0, FolderId A_1, string A_2)
		{
			return this.a(A_0, A_1, A_2, true, 0);
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00050B12 File Offset: 0x0004FB12
		public FolderId b(bool A_0, string A_1, int A_2)
		{
			return this.a(A_0, null, A_1, true, A_2);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00050B1F File Offset: 0x0004FB1F
		private void b(string A_0, FolderId A_1)
		{
			this.p.pa();
			((d)this.p).b(A_0, A_1);
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00050B40 File Offset: 0x0004FB40
		public bool b(bool A_0, string A_1, FolderId A_2)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.b(A_1, A_2);
				}
				else
				{
					try
					{
						this.b(A_1, A_2);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return false;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return true;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00050BC8 File Offset: 0x0004FBC8
		private new void a(string A_0, FolderId A_1)
		{
			this.p.pa();
			((d)this.p).a(A_0, A_1);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00050BE8 File Offset: 0x0004FBE8
		public new bool a(bool A_0, string A_1, FolderId A_2)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.a(A_1, A_2);
				}
				else
				{
					try
					{
						this.a(A_1, A_2);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return false;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return true;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00050C70 File Offset: 0x0004FC70
		private new void a(FolderId A_0, FolderId A_1)
		{
			this.p.pa();
			((d)this.p).a(A_0, A_1);
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00050C90 File Offset: 0x0004FC90
		public new bool a(bool A_0, FolderId A_1, FolderId A_2)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.a(A_1, A_2);
				}
				else
				{
					try
					{
						this.a(A_1, A_2);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return false;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return true;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00050D18 File Offset: 0x0004FD18
		private new void a(string A_0, string A_1, List<EwsFolder> A_2)
		{
			this.p.pa();
			((d)this.p).a(A_0, A_1, A_2);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00050D38 File Offset: 0x0004FD38
		public new bool a(bool A_0, string A_1, string A_2, List<EwsFolder> A_3)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.a(A_1, A_2, A_3);
				}
				else
				{
					try
					{
						this.a(A_1, A_2, A_3);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return false;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return true;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00050DC4 File Offset: 0x0004FDC4
		private new void a(FolderId A_0)
		{
			this.p.pa();
			((d)this.p).b(A_0);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00050DE4 File Offset: 0x0004FDE4
		public bool b(bool A_0, FolderId A_1)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.a(A_1);
				}
				else
				{
					try
					{
						this.a(A_1);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return false;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return true;
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00050E6C File Offset: 0x0004FE6C
		private new void a(FolderId A_0, bool A_1)
		{
			this.p.pa();
			((d)this.p).a(A_0, A_1);
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00050E8C File Offset: 0x0004FE8C
		public bool b(bool A_0, FolderId A_1, bool A_2)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.a(A_1, A_2);
				}
				else
				{
					try
					{
						this.a(A_1, A_2);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return false;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return true;
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00050F14 File Offset: 0x0004FF14
		private new EwsItemList a(FolderId A_0, ItemView A_1, bool A_2, EwsItemParts? A_3, PropertySet A_4)
		{
			this.p.pa();
			return ((d)this.p).a(A_0, A_1, A_2, A_3, A_4);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00050F38 File Offset: 0x0004FF38
		private new EwsItemList a(bool A_0, FolderId A_1, ItemView A_2, bool A_3, EwsItemParts? A_4, PropertySet A_5)
		{
			EwsItemList result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.a(A_1, A_2, A_3, A_4, A_5);
				}
				else
				{
					try
					{
						result = this.a(A_1, A_2, A_3, A_4, A_5);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return null;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return result;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00050FD0 File Offset: 0x0004FFD0
		public new EwsItemList a(bool A_0, FolderId A_1, ItemView A_2, bool A_3, PropertySet A_4)
		{
			return this.a(A_0, A_1, A_2, A_3, null, A_4);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00050FF3 File Offset: 0x0004FFF3
		public new EwsItemList a(bool A_0, FolderId A_1, ItemView A_2, bool A_3, EwsItemParts A_4)
		{
			return this.a(A_0, A_1, A_2, A_3, new EwsItemParts?(A_4), null);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00051008 File Offset: 0x00050008
		public EwsItemList b(bool A_0, FolderId A_1, ItemView A_2, bool A_3)
		{
			return this.a(A_0, A_1, A_2, A_3, null, null);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0005102A File Offset: 0x0005002A
		public new EwsItemList a(bool A_0, FolderId A_1, bool A_2)
		{
			return this.b(A_0, A_1, null, A_2);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00051036 File Offset: 0x00050036
		private new EwsItemList a(FolderId A_0, int A_1, int A_2, bool A_3, EwsItemParts? A_4, PropertySet A_5)
		{
			this.p.pa();
			return ((d)this.p).a(A_0, A_1, A_2, A_3, A_4, A_5);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0005105C File Offset: 0x0005005C
		private new EwsItemList a(bool A_0, FolderId A_1, int A_2, int A_3, bool A_4, EwsItemParts? A_5, PropertySet A_6)
		{
			EwsItemList result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.a(A_1, A_2, A_3, A_4, A_5, A_6);
				}
				else
				{
					try
					{
						result = this.a(A_1, A_2, A_3, A_4, A_5, A_6);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return null;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return result;
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x000510F8 File Offset: 0x000500F8
		public new EwsItemList a(bool A_0, FolderId A_1, int A_2, int A_3, bool A_4, PropertySet A_5)
		{
			return this.a(A_0, A_1, A_2, A_3, A_4, null, A_5);
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0005111D File Offset: 0x0005011D
		public new EwsItemList a(bool A_0, FolderId A_1, int A_2, int A_3, bool A_4, EwsItemParts A_5)
		{
			return this.a(A_0, A_1, A_2, A_3, A_4, new EwsItemParts?(A_5), null);
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00051134 File Offset: 0x00050134
		public new EwsItemList a(bool A_0, FolderId A_1, int A_2, int A_3, bool A_4)
		{
			return this.a(A_0, A_1, A_2, A_3, A_4, null, null);
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00051158 File Offset: 0x00050158
		public new EwsItem a(bool A_0, FolderId A_1, int A_2, PropertySet A_3)
		{
			return this.a(A_0, A_1, A_2, 1, false, null, A_3).FirstOrDefault<EwsItem>();
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00051180 File Offset: 0x00050180
		public new EwsItem a(bool A_0, FolderId A_1, int A_2, EwsItemParts A_3)
		{
			return this.a(A_0, A_1, A_2, 1, false, new EwsItemParts?(A_3), null).FirstOrDefault<EwsItem>();
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0005119C File Offset: 0x0005019C
		public new EwsItem a(bool A_0, FolderId A_1, int A_2)
		{
			return this.a(A_0, A_1, A_2, 1, false, null, null).FirstOrDefault<EwsItem>();
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x000511C3 File Offset: 0x000501C3
		public new EwsItemList c(bool A_0, FolderId A_1, bool A_2)
		{
			return this.a(A_0, A_1, null, A_2);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x000511CF File Offset: 0x000501CF
		public new EwsItemList a(bool A_0, FolderId A_1, ItemView A_2, bool A_3)
		{
			return this.a(A_0, A_1, A_2, A_3, new PropertySet(BasePropertySet.IdOnly));
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x000511E2 File Offset: 0x000501E2
		private new EwsItemList a(IEnumerable<EwsItem> A_0, EwsItemParts? A_1, PropertySet A_2)
		{
			this.p.pa();
			return ((d)this.p).a(A_0, A_1, A_2);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00051204 File Offset: 0x00050204
		private new EwsItemList a(bool A_0, IEnumerable<EwsItem> A_1, EwsItemParts? A_2, PropertySet A_3)
		{
			EwsItemList result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.a(A_1, A_2, A_3);
				}
				else
				{
					try
					{
						result = this.a(A_1, A_2, A_3);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return null;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return result;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00051294 File Offset: 0x00050294
		public new EwsItemList a(bool A_0, IEnumerable<EwsItem> A_1, PropertySet A_2)
		{
			return this.a(A_0, A_1, null, A_2);
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x000512B3 File Offset: 0x000502B3
		public new EwsItemList a(bool A_0, IEnumerable<EwsItem> A_1, EwsItemParts A_2)
		{
			return this.a(A_0, A_1, new EwsItemParts?(A_2), null);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x000512C4 File Offset: 0x000502C4
		private new EwsItem a(ItemId A_0, EwsItemParts? A_1, PropertySet A_2)
		{
			this.p.pa();
			return ((d)this.p).a(A_0, A_1, A_2);
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x000512E4 File Offset: 0x000502E4
		private new EwsItem a(bool A_0, ItemId A_1, EwsItemParts? A_2, PropertySet A_3)
		{
			EwsItem result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.a(A_1, A_2, A_3);
				}
				else
				{
					try
					{
						result = this.a(A_1, A_2, A_3);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return null;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return result;
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00051374 File Offset: 0x00050374
		public new EwsItem a(bool A_0, ItemId A_1, EwsItemParts A_2)
		{
			return this.a(A_0, A_1, new EwsItemParts?(A_2), null);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00051388 File Offset: 0x00050388
		public new EwsItem a(bool A_0, ItemId A_1, PropertySet A_2)
		{
			return this.a(A_0, A_1, null, A_2);
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x000513A7 File Offset: 0x000503A7
		private new MailMessage c(ItemId A_0)
		{
			this.p.pa();
			return ((d)this.p).c(A_0);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x000513C8 File Offset: 0x000503C8
		public new MailMessage c(bool A_0, ItemId A_1)
		{
			MailMessage result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.c(A_1);
				}
				else
				{
					try
					{
						result = this.c(A_1);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return null;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return result;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00051454 File Offset: 0x00050454
		private new List<Microsoft.Exchange.WebServices.Data.Attachment> a(string[] A_0, bool A_1)
		{
			this.p.pa();
			return ((d)this.p).a(A_0, A_1);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00051474 File Offset: 0x00050474
		public new List<Microsoft.Exchange.WebServices.Data.Attachment> a(bool A_0, string[] A_1, bool A_2)
		{
			List<Microsoft.Exchange.WebServices.Data.Attachment> result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.a(A_1, A_2);
				}
				else
				{
					try
					{
						result = this.a(A_1, A_2);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return null;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return result;
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00051500 File Offset: 0x00050500
		private new FileAttachment a(ItemId A_0, string A_1, string A_2)
		{
			this.p.pa();
			return ((d)this.p).a(A_0, A_1, A_2);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00051520 File Offset: 0x00050520
		public new FileAttachment a(bool A_0, ItemId A_1, string A_2, string A_3)
		{
			FileAttachment result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.a(A_1, A_2, A_3);
				}
				else
				{
					try
					{
						result = this.a(A_1, A_2, A_3);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return null;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return result;
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x000515B0 File Offset: 0x000505B0
		private void b(ItemId A_0)
		{
			this.p.pa();
			((d)this.p).b(A_0);
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x000515D0 File Offset: 0x000505D0
		public bool b(bool A_0, ItemId A_1)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.b(A_1);
				}
				else
				{
					try
					{
						this.b(A_1);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return false;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return true;
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00051658 File Offset: 0x00050658
		private new int a(ItemId A_0, string A_1, bool A_2, bool A_3)
		{
			this.p.pa();
			return ((d)this.p).a(A_0, A_1, A_2, A_3);
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0005167C File Offset: 0x0005067C
		public new int a(bool A_0, ItemId A_1, string A_2, bool A_3, bool A_4)
		{
			int result = -1;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.a(A_1, A_2, A_3, A_4);
				}
				else
				{
					try
					{
						result = this.a(A_1, A_2, A_3, A_4);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return result;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return result;
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00051710 File Offset: 0x00050710
		private new void a(EwsItem A_0)
		{
			this.p.pa();
			((d)this.p).a(A_0);
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00051730 File Offset: 0x00050730
		public new bool a(bool A_0, EwsItem A_1)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.a(A_1);
				}
				else
				{
					try
					{
						this.a(A_1);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return false;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return true;
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x000517B8 File Offset: 0x000507B8
		private new void a(FolderId A_0, byte[] A_1, bool A_2)
		{
			this.p.pa();
			((d)this.p).a(A_0, A_1, A_2);
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x000517D8 File Offset: 0x000507D8
		public new bool a(bool A_0, FolderId A_1, byte[] A_2, bool A_3)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.a(A_1, A_2, A_3);
				}
				else
				{
					try
					{
						this.a(A_1, A_2, A_3);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return false;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return true;
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00051864 File Offset: 0x00050864
		public new bool a(bool A_0, FolderId A_1, MailMessage A_2, bool A_3)
		{
			return this.a(A_0, A_1, A_2.GetMessageRawData(), A_3);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00051876 File Offset: 0x00050876
		private ItemId b(ItemId A_0, FolderId A_1)
		{
			this.p.pa();
			return ((d)this.p).b(A_0, A_1);
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00051898 File Offset: 0x00050898
		public ItemId b(bool A_0, ItemId A_1, FolderId A_2)
		{
			ItemId result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.b(A_1, A_2);
				}
				else
				{
					try
					{
						result = this.b(A_1, A_2);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return null;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return result;
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00051924 File Offset: 0x00050924
		private new ItemId a(ItemId A_0, FolderId A_1)
		{
			this.p.pa();
			return ((d)this.p).a(A_0, A_1);
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00051944 File Offset: 0x00050944
		public new ItemId a(bool A_0, ItemId A_1, FolderId A_2)
		{
			ItemId result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.a(A_1, A_2);
				}
				else
				{
					try
					{
						result = this.a(A_1, A_2);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return null;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return result;
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x000519D0 File Offset: 0x000509D0
		private new void a(ItemId A_0)
		{
			this.p.pa();
			((d)this.p).a(A_0);
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x000519F0 File Offset: 0x000509F0
		public new bool a(bool A_0, ItemId A_1)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.a(A_1);
				}
				else
				{
					try
					{
						this.a(A_1);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return false;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return true;
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00051A78 File Offset: 0x00050A78
		private new List<ItemId> a(IEnumerable<ItemId> A_0)
		{
			this.p.pa();
			return ((d)this.p).a(A_0);
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00051A98 File Offset: 0x00050A98
		public new List<ItemId> a(bool A_0, IEnumerable<ItemId> A_1)
		{
			List<ItemId> result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.a(A_1);
				}
				else
				{
					try
					{
						result = this.a(A_1);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return null;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return result;
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00051B24 File Offset: 0x00050B24
		private new EwsItemList a(FolderId A_0, SearchFilter A_1, ItemView A_2)
		{
			this.p.pa();
			return ((d)this.p).a(A_0, A_1, A_2);
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x00051B44 File Offset: 0x00050B44
		public new EwsItemList a(bool A_0, FolderId A_1, SearchFilter A_2, ItemView A_3)
		{
			EwsItemList result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.a(A_1, A_2, A_3);
				}
				else
				{
					try
					{
						result = this.a(A_1, A_2, A_3);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return null;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return result;
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00051BD4 File Offset: 0x00050BD4
		public new EwsItemList a(bool A_0, FolderId A_1, SearchFilter A_2)
		{
			return this.a(A_0, A_1, A_2, null);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00051BE0 File Offset: 0x00050BE0
		private new void a(MailMessage A_0, bool A_1, FolderId A_2)
		{
			this.p.pa();
			((d)this.p).a(A_0, A_1, A_2);
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00051C00 File Offset: 0x00050C00
		private new bool a(bool A_0, MailMessage A_1, bool A_2, FolderId A_3)
		{
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					this.a(A_1, A_2, A_3);
				}
				else
				{
					try
					{
						this.a(A_1, A_2, A_3);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return false;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return true;
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00051C8C File Offset: 0x00050C8C
		public new bool a(bool A_0, MailMessage A_1)
		{
			return this.a(A_0, A_1, false, null);
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00051C98 File Offset: 0x00050C98
		public new bool a(bool A_0, MailMessage A_1, FolderId A_2)
		{
			return this.a(A_0, A_1, true, A_2);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00051CA4 File Offset: 0x00050CA4
		private new MailBee.Mime.EmailAddressCollection a(string A_0)
		{
			this.p.pa();
			return ((d)this.p).f(A_0);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00051CC4 File Offset: 0x00050CC4
		public new MailBee.Mime.EmailAddressCollection a(bool A_0, string A_1)
		{
			MailBee.Mime.EmailAddressCollection result = null;
			if (A_0)
			{
				this.p.k(true);
			}
			try
			{
				if (this.i && this.k)
				{
					result = this.a(A_1);
				}
				else
				{
					try
					{
						result = this.a(A_1);
					}
					catch (MailBeeException a_)
					{
						base.b(a_);
						if (this.i)
						{
							throw;
						}
						return null;
					}
				}
			}
			finally
			{
				if (A_0)
				{
					this.p.k(false);
				}
			}
			return result;
		}

		// Token: 0x04000F2E RID: 3886
		private new Ews a;
	}
}
