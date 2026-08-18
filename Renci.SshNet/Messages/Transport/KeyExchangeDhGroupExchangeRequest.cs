using System;

namespace Renci.SshNet.Messages.Transport
{
	// Token: 0x020000D6 RID: 214
	[Message("SSH_MSG_KEX_DH_GEX_REQUEST", 34)]
	internal class KeyExchangeDhGroupExchangeRequest : Message, IKeyExchangedAllowed
	{
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x0001FD5E File Offset: 0x0001DF5E
		// (set) Token: 0x0600094F RID: 2383 RVA: 0x0001FD66 File Offset: 0x0001DF66
		public uint Minimum { get; private set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0001FD6F File Offset: 0x0001DF6F
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x0001FD77 File Offset: 0x0001DF77
		public uint Preferred { get; private set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x0001FD80 File Offset: 0x0001DF80
		// (set) Token: 0x06000953 RID: 2387 RVA: 0x0001FD88 File Offset: 0x0001DF88
		public uint Maximum { get; private set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x0001FD91 File Offset: 0x0001DF91
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + 4 + 4;
			}
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0001FD9F File Offset: 0x0001DF9F
		public KeyExchangeDhGroupExchangeRequest(uint minimum, uint preferred, uint maximum)
		{
			this.Minimum = minimum;
			this.Preferred = preferred;
			this.Maximum = maximum;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0001FDBC File Offset: 0x0001DFBC
		protected override void LoadData()
		{
			this.Minimum = base.ReadUInt32();
			this.Preferred = base.ReadUInt32();
			this.Maximum = base.ReadUInt32();
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0001FDE2 File Offset: 0x0001DFE2
		protected override void SaveData()
		{
			base.Write(this.Minimum);
			base.Write(this.Preferred);
			base.Write(this.Maximum);
		}

		// Token: 0x040003A2 RID: 930
		internal const byte MessageNumber = 34;
	}
}
