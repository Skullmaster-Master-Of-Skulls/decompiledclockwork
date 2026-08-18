using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Infrastructure
{
	// Token: 0x02000018 RID: 24
	public class AuthenticationTokenProvider : IAuthenticationTokenProvider
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002AF7 File Offset: 0x00000CF7
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002AFF File Offset: 0x00000CFF
		public Action<AuthenticationTokenCreateContext> OnCreate { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002B08 File Offset: 0x00000D08
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002B10 File Offset: 0x00000D10
		public Func<AuthenticationTokenCreateContext, Task> OnCreateAsync { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002B19 File Offset: 0x00000D19
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002B21 File Offset: 0x00000D21
		public Action<AuthenticationTokenReceiveContext> OnReceive { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002B2A File Offset: 0x00000D2A
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002B32 File Offset: 0x00000D32
		public Func<AuthenticationTokenReceiveContext, Task> OnReceiveAsync { get; set; }

		// Token: 0x0600004C RID: 76 RVA: 0x00002B3B File Offset: 0x00000D3B
		public virtual void Create(AuthenticationTokenCreateContext context)
		{
			if (this.OnCreateAsync != null && this.OnCreate == null)
			{
				throw new InvalidOperationException(Resources.Exception_AuthenticationTokenDoesNotProvideSyncMethods);
			}
			if (this.OnCreate != null)
			{
				this.OnCreate(context);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C8C File Offset: 0x00000E8C
		public virtual async Task CreateAsync(AuthenticationTokenCreateContext context)
		{
			if (this.OnCreateAsync != null && this.OnCreate == null)
			{
				throw new InvalidOperationException(Resources.Exception_AuthenticationTokenDoesNotProvideSyncMethods);
			}
			if (this.OnCreateAsync != null)
			{
				await this.OnCreateAsync(context);
			}
			else
			{
				this.Create(context);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002CDA File Offset: 0x00000EDA
		public virtual void Receive(AuthenticationTokenReceiveContext context)
		{
			if (this.OnReceiveAsync != null && this.OnReceive == null)
			{
				throw new InvalidOperationException(Resources.Exception_AuthenticationTokenDoesNotProvideSyncMethods);
			}
			if (this.OnReceive != null)
			{
				this.OnReceive(context);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E2C File Offset: 0x0000102C
		public virtual async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
		{
			if (this.OnReceiveAsync != null && this.OnReceive == null)
			{
				throw new InvalidOperationException(Resources.Exception_AuthenticationTokenDoesNotProvideSyncMethods);
			}
			if (this.OnReceiveAsync != null)
			{
				await this.OnReceiveAsync(context);
			}
			else
			{
				this.Receive(context);
			}
		}
	}
}
