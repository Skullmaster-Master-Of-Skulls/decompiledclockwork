using System;
using System.Threading;

namespace a
{
	// Token: 0x02000493 RID: 1171
	internal class o : IAsyncResult
	{
		// Token: 0x06002835 RID: 10293 RVA: 0x000BBAEA File Offset: 0x000BAAEA
		public o(Delegate A_0, IAsyncResult A_1)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = null;
			this.d = null;
			this.e = null;
			this.f = false;
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x000BBB1C File Offset: 0x000BAB1C
		public o(WaitHandle A_0, AsyncCallback A_1, object A_2)
		{
			this.a = null;
			this.b = null;
			this.c = A_0;
			this.d = A_1;
			this.e = A_2;
			this.f = false;
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x000BBB4E File Offset: 0x000BAB4E
		public object get_AsyncState()
		{
			if (this.a == null)
			{
				return this.e;
			}
			if (this.b == null)
			{
				return null;
			}
			return this.b.AsyncState;
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x000BBB74 File Offset: 0x000BAB74
		public WaitHandle get_AsyncWaitHandle()
		{
			if (this.c != null)
			{
				return this.c;
			}
			if (this.b == null)
			{
				return null;
			}
			return this.b.AsyncWaitHandle;
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x000BBB9A File Offset: 0x000BAB9A
		public bool get_CompletedSynchronously()
		{
			return this.a != null && this.b != null && this.b.CompletedSynchronously;
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x000BBBBB File Offset: 0x000BABBB
		public bool get_IsCompleted()
		{
			if (this.a == null)
			{
				return this.f;
			}
			return this.b != null && this.b.IsCompleted;
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x000BBBE1 File Offset: 0x000BABE1
		internal void b()
		{
			this.f = true;
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x000BBBEA File Offset: 0x000BABEA
		internal Delegate c()
		{
			return this.a;
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x000BBBF2 File Offset: 0x000BABF2
		internal IAsyncResult d()
		{
			return this.b;
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x000BBBFA File Offset: 0x000BABFA
		internal void a(IAsyncResult A_0)
		{
			this.b = A_0;
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x000BBC03 File Offset: 0x000BAC03
		internal AsyncCallback a()
		{
			return this.d;
		}

		// Token: 0x04001B73 RID: 7027
		private Delegate a;

		// Token: 0x04001B74 RID: 7028
		private IAsyncResult b;

		// Token: 0x04001B75 RID: 7029
		private WaitHandle c;

		// Token: 0x04001B76 RID: 7030
		private AsyncCallback d;

		// Token: 0x04001B77 RID: 7031
		private object e;

		// Token: 0x04001B78 RID: 7032
		private bool f;
	}
}
