using System;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000B8 RID: 184
	internal class XonXoffRequestInfo : RequestInfo
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x0001EEA7 File Offset: 0x0001D0A7
		public override string RequestName
		{
			get
			{
				return "xon-xoff";
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x0001EEAE File Offset: 0x0001D0AE
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x0001EEB6 File Offset: 0x0001D0B6
		public bool ClientCanDo { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x0001EEBF File Offset: 0x0001D0BF
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 1;
			}
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001E69A File Offset: 0x0001C89A
		public XonXoffRequestInfo()
		{
			base.WantReply = false;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001EEC9 File Offset: 0x0001D0C9
		public XonXoffRequestInfo(bool clientCanDo) : this()
		{
			this.ClientCanDo = clientCanDo;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001EED8 File Offset: 0x0001D0D8
		protected override void LoadData()
		{
			base.LoadData();
			this.ClientCanDo = base.ReadBoolean();
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001EEEC File Offset: 0x0001D0EC
		protected override void SaveData()
		{
			base.SaveData();
			base.Write(this.ClientCanDo);
		}

		// Token: 0x04000353 RID: 851
		public const string Name = "xon-xoff";
	}
}
