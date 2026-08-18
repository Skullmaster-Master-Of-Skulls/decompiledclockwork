using System;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006B1 RID: 1713
	internal class RegisteredChannelList
	{
		// Token: 0x06003DE5 RID: 15845 RVA: 0x000D3CD7 File Offset: 0x000D2CD7
		internal RegisteredChannelList()
		{
			this._channels = new RegisteredChannel[0];
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x000D3CEB File Offset: 0x000D2CEB
		internal RegisteredChannelList(RegisteredChannel[] channels)
		{
			this._channels = channels;
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06003DE7 RID: 15847 RVA: 0x000D3CFA File Offset: 0x000D2CFA
		internal RegisteredChannel[] RegisteredChannels
		{
			get
			{
				return this._channels;
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06003DE8 RID: 15848 RVA: 0x000D3D02 File Offset: 0x000D2D02
		internal int Count
		{
			get
			{
				if (this._channels == null)
				{
					return 0;
				}
				return this._channels.Length;
			}
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x000D3D16 File Offset: 0x000D2D16
		internal IChannel GetChannel(int index)
		{
			return this._channels[index].Channel;
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x000D3D25 File Offset: 0x000D2D25
		internal bool IsSender(int index)
		{
			return this._channels[index].IsSender();
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x000D3D34 File Offset: 0x000D2D34
		internal bool IsReceiver(int index)
		{
			return this._channels[index].IsReceiver();
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06003DEC RID: 15852 RVA: 0x000D3D44 File Offset: 0x000D2D44
		internal int ReceiverCount
		{
			get
			{
				if (this._channels == null)
				{
					return 0;
				}
				int num = 0;
				for (int i = 0; i < this._channels.Length; i++)
				{
					if (this.IsReceiver(i))
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x000D3D80 File Offset: 0x000D2D80
		internal int FindChannelIndex(IChannel channel)
		{
			for (int i = 0; i < this._channels.Length; i++)
			{
				if (channel == this.GetChannel(i))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x000D3DB0 File Offset: 0x000D2DB0
		internal int FindChannelIndex(string name)
		{
			for (int i = 0; i < this._channels.Length; i++)
			{
				if (string.Compare(name, this.GetChannel(i).ChannelName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x04001F94 RID: 8084
		private RegisteredChannel[] _channels;
	}
}
