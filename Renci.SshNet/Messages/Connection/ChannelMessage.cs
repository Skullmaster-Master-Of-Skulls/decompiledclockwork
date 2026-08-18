using System;
using System.Globalization;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x0200009F RID: 159
	public abstract class ChannelMessage : Message
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0001DDB3 File Offset: 0x0001BFB3
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x0001DDBB File Offset: 0x0001BFBB
		public uint LocalChannelNumber { get; protected set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0001DDC4 File Offset: 0x0001BFC4
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4;
			}
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001DDCE File Offset: 0x0001BFCE
		protected ChannelMessage()
		{
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001DDD6 File Offset: 0x0001BFD6
		protected ChannelMessage(uint localChannelNumber)
		{
			this.LocalChannelNumber = localChannelNumber;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001DDE5 File Offset: 0x0001BFE5
		protected override void LoadData()
		{
			this.LocalChannelNumber = base.ReadUInt32();
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001DDF3 File Offset: 0x0001BFF3
		protected override void SaveData()
		{
			base.Write(this.LocalChannelNumber);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001DE01 File Offset: 0x0001C001
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} : #{1}", new object[]
			{
				base.ToString(),
				this.LocalChannelNumber
			});
		}
	}
}
