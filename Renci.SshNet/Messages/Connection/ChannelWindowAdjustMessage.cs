using System;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000BA RID: 186
	[Message("SSH_MSG_CHANNEL_WINDOW_ADJUST", 93)]
	public class ChannelWindowAdjustMessage : ChannelMessage
	{
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0001EF00 File Offset: 0x0001D100
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x0001EF08 File Offset: 0x0001D108
		public uint BytesToAdd { get; private set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0001EF11 File Offset: 0x0001D111
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4;
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001DC2C File Offset: 0x0001BE2C
		public ChannelWindowAdjustMessage()
		{
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001EF1B File Offset: 0x0001D11B
		public ChannelWindowAdjustMessage(uint localChannelNumber, uint bytesToAdd) : base(localChannelNumber)
		{
			this.BytesToAdd = bytesToAdd;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001EF2B File Offset: 0x0001D12B
		protected override void LoadData()
		{
			base.LoadData();
			this.BytesToAdd = base.ReadUInt32();
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0001EF3F File Offset: 0x0001D13F
		protected override void SaveData()
		{
			base.SaveData();
			base.Write(this.BytesToAdd);
		}
	}
}
