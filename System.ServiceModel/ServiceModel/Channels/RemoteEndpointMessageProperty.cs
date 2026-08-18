using System;
using System.Net;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200075C RID: 1884
	public sealed class RemoteEndpointMessageProperty
	{
		// Token: 0x0600480C RID: 18444 RVA: 0x0010AE74 File Offset: 0x00109074
		public RemoteEndpointMessageProperty(string address, int port)
		{
			if (string.IsNullOrEmpty(address))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			if (port < 0 || port > 65535)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("port", SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					65535
				}));
			}
			this.port = port;
			this.address = address;
			this.state = RemoteEndpointMessageProperty.InitializationState.All;
		}

		// Token: 0x0600480D RID: 18445 RVA: 0x0010AF01 File Offset: 0x00109101
		internal RemoteEndpointMessageProperty(RemoteEndpointMessageProperty.IRemoteEndpointProvider remoteEndpointProvider)
		{
			this.remoteEndpointProvider = remoteEndpointProvider;
		}

		// Token: 0x0600480E RID: 18446 RVA: 0x0010AF1B File Offset: 0x0010911B
		internal RemoteEndpointMessageProperty(IPEndPoint remoteEndPoint)
		{
			this.remoteEndPoint = remoteEndPoint;
		}

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x0600480F RID: 18447 RVA: 0x0010AF35 File Offset: 0x00109135
		public static string Name
		{
			get
			{
				return "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
			}
		}

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x06004810 RID: 18448 RVA: 0x0010AF3C File Offset: 0x0010913C
		public string Address
		{
			get
			{
				if ((this.state & RemoteEndpointMessageProperty.InitializationState.Address) != RemoteEndpointMessageProperty.InitializationState.Address)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if ((this.state & RemoteEndpointMessageProperty.InitializationState.Address) != RemoteEndpointMessageProperty.InitializationState.Address)
						{
							this.Initialize(false);
						}
					}
				}
				return this.address;
			}
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x06004811 RID: 18449 RVA: 0x0010AF9C File Offset: 0x0010919C
		public int Port
		{
			get
			{
				if ((this.state & RemoteEndpointMessageProperty.InitializationState.Port) != RemoteEndpointMessageProperty.InitializationState.Port)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if ((this.state & RemoteEndpointMessageProperty.InitializationState.Port) != RemoteEndpointMessageProperty.InitializationState.Port)
						{
							this.Initialize(true);
						}
					}
				}
				return this.port;
			}
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x06004812 RID: 18450 RVA: 0x0010AFFC File Offset: 0x001091FC
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x06004813 RID: 18451 RVA: 0x0010B004 File Offset: 0x00109204
		private void Initialize(bool getHostedPort)
		{
			if (this.remoteEndPoint != null)
			{
				this.address = this.remoteEndPoint.Address.ToString();
				this.port = this.remoteEndPoint.Port;
				this.state = RemoteEndpointMessageProperty.InitializationState.All;
				this.remoteEndPoint = null;
				return;
			}
			if ((this.state & RemoteEndpointMessageProperty.InitializationState.Address) != RemoteEndpointMessageProperty.InitializationState.Address)
			{
				this.address = this.remoteEndpointProvider.GetAddress();
				this.state |= RemoteEndpointMessageProperty.InitializationState.Address;
			}
			if (getHostedPort)
			{
				this.port = this.remoteEndpointProvider.GetPort();
				this.state |= RemoteEndpointMessageProperty.InitializationState.Port;
				this.remoteEndpointProvider = null;
			}
		}

		// Token: 0x04002DDB RID: 11739
		private string address;

		// Token: 0x04002DDC RID: 11740
		private int port;

		// Token: 0x04002DDD RID: 11741
		private IPEndPoint remoteEndPoint;

		// Token: 0x04002DDE RID: 11742
		private RemoteEndpointMessageProperty.IRemoteEndpointProvider remoteEndpointProvider;

		// Token: 0x04002DDF RID: 11743
		private RemoteEndpointMessageProperty.InitializationState state;

		// Token: 0x04002DE0 RID: 11744
		private object thisLock = new object();

		// Token: 0x02000CDE RID: 3294
		internal interface IRemoteEndpointProvider
		{
			// Token: 0x06007A0C RID: 31244
			string GetAddress();

			// Token: 0x06007A0D RID: 31245
			int GetPort();
		}

		// Token: 0x02000CDF RID: 3295
		[Flags]
		private enum InitializationState
		{
			// Token: 0x040045CD RID: 17869
			None = 0,
			// Token: 0x040045CE RID: 17870
			Address = 1,
			// Token: 0x040045CF RID: 17871
			Port = 2,
			// Token: 0x040045D0 RID: 17872
			All = 3
		}
	}
}
