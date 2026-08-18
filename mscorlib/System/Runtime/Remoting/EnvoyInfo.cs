using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x02000736 RID: 1846
	[Serializable]
	internal sealed class EnvoyInfo : IEnvoyInfo
	{
		// Token: 0x0600420F RID: 16911 RVA: 0x000E09B8 File Offset: 0x000DF9B8
		internal static IEnvoyInfo CreateEnvoyInfo(ServerIdentity serverID)
		{
			IEnvoyInfo result = null;
			if (serverID != null)
			{
				if (serverID.EnvoyChain == null)
				{
					serverID.RaceSetEnvoyChain(serverID.ServerContext.CreateEnvoyChain(serverID.TPOrObject));
				}
				if (!(serverID.EnvoyChain is EnvoyTerminatorSink))
				{
					result = new EnvoyInfo(serverID.EnvoyChain);
				}
			}
			return result;
		}

		// Token: 0x06004210 RID: 16912 RVA: 0x000E0A06 File Offset: 0x000DFA06
		private EnvoyInfo(IMessageSink sinks)
		{
			this.EnvoySinks = sinks;
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06004211 RID: 16913 RVA: 0x000E0A15 File Offset: 0x000DFA15
		// (set) Token: 0x06004212 RID: 16914 RVA: 0x000E0A1D File Offset: 0x000DFA1D
		public IMessageSink EnvoySinks
		{
			get
			{
				return this.envoySinks;
			}
			set
			{
				this.envoySinks = value;
			}
		}

		// Token: 0x0400211B RID: 8475
		private IMessageSink envoySinks;
	}
}
