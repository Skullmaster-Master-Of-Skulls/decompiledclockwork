using System;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting
{
	// Token: 0x02000735 RID: 1845
	[Serializable]
	internal sealed class ChannelInfo : IChannelInfo
	{
		// Token: 0x0600420C RID: 16908 RVA: 0x000E0994 File Offset: 0x000DF994
		internal ChannelInfo()
		{
			this.ChannelData = ChannelServices.CurrentChannelData;
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x0600420D RID: 16909 RVA: 0x000E09A7 File Offset: 0x000DF9A7
		// (set) Token: 0x0600420E RID: 16910 RVA: 0x000E09AF File Offset: 0x000DF9AF
		public object[] ChannelData
		{
			get
			{
				return this.channelData;
			}
			set
			{
				this.channelData = value;
			}
		}

		// Token: 0x0400211A RID: 8474
		private object[] channelData;
	}
}
