using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200073E RID: 1854
	internal class ChannelPoolKey : IEquatable<ChannelPoolKey>
	{
		// Token: 0x0600467F RID: 18047 RVA: 0x00106D0C File Offset: 0x00104F0C
		public ChannelPoolKey(EndpointAddress address, Uri via)
		{
			this.address = address;
			this.via = via;
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x00106D22 File Offset: 0x00104F22
		public override int GetHashCode()
		{
			return this.address.GetHashCode() + this.via.GetHashCode();
		}

		// Token: 0x06004681 RID: 18049 RVA: 0x00106D3B File Offset: 0x00104F3B
		public bool Equals(ChannelPoolKey other)
		{
			return this.address.EndpointEquals(other.address) && this.via.Equals(other.via);
		}

		// Token: 0x04002D8B RID: 11659
		private EndpointAddress address;

		// Token: 0x04002D8C RID: 11660
		private Uri via;
	}
}
