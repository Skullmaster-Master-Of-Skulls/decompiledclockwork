using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200074C RID: 1868
	internal class LateBoundChannelParameterCollection : ChannelParameterCollection
	{
		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x0600475E RID: 18270 RVA: 0x00109368 File Offset: 0x00107568
		protected override IChannel Channel
		{
			get
			{
				return this.channel;
			}
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x00109370 File Offset: 0x00107570
		internal void SetChannel(IChannel channel)
		{
			this.channel = channel;
		}

		// Token: 0x04002DAA RID: 11690
		private IChannel channel;
	}
}
