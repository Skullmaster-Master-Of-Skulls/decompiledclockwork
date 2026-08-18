using System;
using System.Text;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000AD RID: 173
	internal class ExecRequestInfo : RequestInfo
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0001E76E File Offset: 0x0001C96E
		public override string RequestName
		{
			get
			{
				return "exec";
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0001E775 File Offset: 0x0001C975
		public string Command
		{
			get
			{
				return this.Encoding.GetString(this._command, 0, this._command.Length);
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x0001E791 File Offset: 0x0001C991
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x0001E799 File Offset: 0x0001C999
		public Encoding Encoding { get; private set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x0001E7A2 File Offset: 0x0001C9A2
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._command.Length;
			}
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001E57E File Offset: 0x0001C77E
		public ExecRequestInfo()
		{
			base.WantReply = true;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001E7B5 File Offset: 0x0001C9B5
		public ExecRequestInfo(string command, Encoding encoding) : this()
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			this._command = encoding.GetBytes(command);
			this.Encoding = encoding;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001E7ED File Offset: 0x0001C9ED
		protected override void LoadData()
		{
			base.LoadData();
			this._command = base.ReadBinary();
			this.Encoding = SshData.Utf8;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001E80C File Offset: 0x0001CA0C
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._command);
		}

		// Token: 0x04000331 RID: 817
		private byte[] _command;

		// Token: 0x04000332 RID: 818
		public const string Name = "exec";
	}
}
