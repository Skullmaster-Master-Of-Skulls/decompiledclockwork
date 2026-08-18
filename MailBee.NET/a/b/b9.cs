using System;
using MailBee;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002F4 RID: 756
	internal class b9 : dc
	{
		// Token: 0x06001AA8 RID: 6824 RVA: 0x00074FA1 File Offset: 0x00073FA1
		public b9()
		{
			this.a = new bn[0];
			this.b = null;
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x00074FBC File Offset: 0x00073FBC
		public virtual void a(bn[] A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x00074FC5 File Offset: 0x00073FC5
		public virtual void fa(int A_0)
		{
			if (A_0 >= 0 && A_0 < this.a.Length)
			{
				this.a[A_0] = null;
			}
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x00074FDF File Offset: 0x00073FDF
		protected bn a(int A_0)
		{
			return this.a[A_0];
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x00074FEC File Offset: 0x00073FEC
		public virtual bn fb(int A_0)
		{
			bn bn = null;
			try
			{
				bn = this.a[A_0];
				if (bn == null)
				{
					throw new MailBeeOutlookMsgBuildingException(string.Format(Resources.Instance.ErrorDesc_OleDocBlock0AlreadyRemoved, A_0), 1201);
				}
				this.a[A_0] = null;
			}
			catch (IndexOutOfRangeException)
			{
				throw new MailBeeOutlookMsgBuildingException(string.Format(Resources.Instance.ErrorDesc_OleDocCannotRemoveBlock0OutOfRange1, A_0, this.a.Length - 1), 1201);
			}
			return bn;
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x00075074 File Offset: 0x00074074
		public virtual bn[] fc(int A_0, int A_1)
		{
			if (this.b == null)
			{
				throw new MailBeeOutlookMsgBuildingException(Resources.Instance.ErrorDesc_OleDocImproperlyInitializedList, 1201);
			}
			return this.b.a(A_0, A_1, this);
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x000750A1 File Offset: 0x000740A1
		public virtual void fd(e7 A_0)
		{
			if (this.b != null)
			{
				throw new MailBeeOutlookMsgBuildingException(Resources.Instance.ErrorDesc_OleDocAttemptToReplaceExistingBlockAllocationTable, 1201);
			}
			this.b = A_0;
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x000750C7 File Offset: 0x000740C7
		public virtual int fe()
		{
			return this.a.Length;
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x000750D4 File Offset: 0x000740D4
		protected int a()
		{
			int num = 0;
			for (int i = 0; i < this.a.Length; i++)
			{
				if (this.a[i] != null)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x040012EF RID: 4847
		private bn[] a;

		// Token: 0x040012F0 RID: 4848
		private e7 b;
	}
}
