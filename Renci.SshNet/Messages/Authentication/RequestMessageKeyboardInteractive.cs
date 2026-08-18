using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Authentication
{
	// Token: 0x020000C7 RID: 199
	internal class RequestMessageKeyboardInteractive : RequestMessage
	{
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0001F61B File Offset: 0x0001D81B
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x0001F623 File Offset: 0x0001D823
		public byte[] Language { get; private set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0001F62C File Offset: 0x0001D82C
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x0001F634 File Offset: 0x0001D834
		public byte[] SubMethods { get; private set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x0001F63D File Offset: 0x0001D83D
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.Language.Length + 4 + this.SubMethods.Length;
			}
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001F65B File Offset: 0x0001D85B
		public RequestMessageKeyboardInteractive(ServiceName serviceName, string username) : base(serviceName, username, "keyboard-interactive")
		{
			this.Language = Array<byte>.Empty;
			this.SubMethods = Array<byte>.Empty;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001F680 File Offset: 0x0001D880
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this.Language);
			base.WriteBinaryString(this.SubMethods);
		}
	}
}
