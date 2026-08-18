using System;

namespace Renci.SshNet.Common
{
	// Token: 0x020000EE RID: 238
	internal class ChannelExtendedDataEventArgs : ChannelDataEventArgs
	{
		// Token: 0x06000A79 RID: 2681 RVA: 0x000240B0 File Offset: 0x000222B0
		public ChannelExtendedDataEventArgs(uint channelNumber, byte[] data, uint dataTypeCode) : base(channelNumber, data)
		{
			this.DataTypeCode = dataTypeCode;
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x000240C1 File Offset: 0x000222C1
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x000240C9 File Offset: 0x000222C9
		public uint DataTypeCode { get; private set; }
	}
}
