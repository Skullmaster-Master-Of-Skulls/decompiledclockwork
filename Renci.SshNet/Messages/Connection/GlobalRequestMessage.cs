using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000BB RID: 187
	[Message("SSH_MSG_GLOBAL_REQUEST", 80)]
	public class GlobalRequestMessage : Message
	{
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0001EF53 File Offset: 0x0001D153
		public GlobalRequestName RequestName
		{
			get
			{
				return this._requestName.ToGlobalRequestName();
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0001EF60 File Offset: 0x0001D160
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x0001EF68 File Offset: 0x0001D168
		public bool WantReply { get; private set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0001EF71 File Offset: 0x0001D171
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x0001EF8C File Offset: 0x0001D18C
		public string AddressToBind
		{
			get
			{
				return SshData.Utf8.GetString(this._addressToBind, 0, this._addressToBind.Length);
			}
			private set
			{
				this._addressToBind = SshData.Utf8.GetBytes(value);
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0001EF9F File Offset: 0x0001D19F
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x0001EFA7 File Offset: 0x0001D1A7
		public uint PortToBind { get; private set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x0001EFB0 File Offset: 0x0001D1B0
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._requestName.Length + 1 + 4 + this._addressToBind.Length + 4;
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001DDCE File Offset: 0x0001BFCE
		public GlobalRequestMessage()
		{
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001EFD2 File Offset: 0x0001D1D2
		public GlobalRequestMessage(GlobalRequestName requestName, bool wantReply, string addressToBind, uint portToBind)
		{
			this._requestName = requestName.ToArray();
			this.WantReply = wantReply;
			this.AddressToBind = addressToBind;
			this.PortToBind = portToBind;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001EFFC File Offset: 0x0001D1FC
		protected override void LoadData()
		{
			this._requestName = base.ReadBinary();
			this.WantReply = base.ReadBoolean();
			this._addressToBind = base.ReadBinary();
			this.PortToBind = base.ReadUInt32();
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0001F02E File Offset: 0x0001D22E
		protected override void SaveData()
		{
			base.WriteBinaryString(this._requestName);
			base.Write(this.WantReply);
			base.WriteBinaryString(this._addressToBind);
			base.Write(this.PortToBind);
		}

		// Token: 0x04000356 RID: 854
		private byte[] _requestName;

		// Token: 0x04000357 RID: 855
		private byte[] _addressToBind;
	}
}
