using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting
{
	// Token: 0x020006C3 RID: 1731
	internal class DelayLoadClientChannelEntry
	{
		// Token: 0x06003E6F RID: 15983 RVA: 0x000D63CA File Offset: 0x000D53CA
		internal DelayLoadClientChannelEntry(RemotingXmlConfigFileData.ChannelEntry entry, bool ensureSecurity)
		{
			this._entry = entry;
			this._channel = null;
			this._bRegistered = false;
			this._ensureSecurity = ensureSecurity;
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06003E70 RID: 15984 RVA: 0x000D63EE File Offset: 0x000D53EE
		internal IChannelSender Channel
		{
			get
			{
				if (this._channel == null && !this._bRegistered)
				{
					this._channel = (IChannelSender)RemotingConfigHandler.CreateChannelFromConfigEntry(this._entry);
					this._entry = null;
				}
				return this._channel;
			}
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x000D6423 File Offset: 0x000D5423
		internal void RegisterChannel()
		{
			ChannelServices.RegisterChannel(this._channel, this._ensureSecurity);
			this._bRegistered = true;
			this._channel = null;
		}

		// Token: 0x04001FCA RID: 8138
		private RemotingXmlConfigFileData.ChannelEntry _entry;

		// Token: 0x04001FCB RID: 8139
		private IChannelSender _channel;

		// Token: 0x04001FCC RID: 8140
		private bool _bRegistered;

		// Token: 0x04001FCD RID: 8141
		private bool _ensureSecurity;
	}
}
