using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Authentication
{
	// Token: 0x020000C5 RID: 197
	[Message("SSH_MSG_USERAUTH_REQUEST", 50)]
	public abstract class RequestMessage : Message
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x0001F428 File Offset: 0x0001D628
		public byte[] Username
		{
			get
			{
				return this._userName;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x0001F430 File Offset: 0x0001D630
		public byte[] ServiceName
		{
			get
			{
				return this._serviceName;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0001F438 File Offset: 0x0001D638
		public virtual string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x0001F440 File Offset: 0x0001D640
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.Username.Length + 4 + this.ServiceName.Length + 4 + this._methodNameBytes.Length;
			}
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001F469 File Offset: 0x0001D669
		protected RequestMessage(ServiceName serviceName, string username, string methodName)
		{
			this._serviceName = serviceName.ToArray();
			this._userName = SshData.Utf8.GetBytes(username);
			this._methodNameBytes = SshData.Ascii.GetBytes(methodName);
			this._methodName = methodName;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001F4A6 File Offset: 0x0001D6A6
		protected override void LoadData()
		{
			throw new InvalidOperationException("Load data is not supported.");
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001F4B2 File Offset: 0x0001D6B2
		protected override void SaveData()
		{
			base.WriteBinaryString(this._userName);
			base.WriteBinaryString(this._serviceName);
			base.WriteBinaryString(this._methodNameBytes);
		}

		// Token: 0x0400036C RID: 876
		internal const int AuthenticationMessageCode = 50;

		// Token: 0x0400036D RID: 877
		private readonly byte[] _serviceName;

		// Token: 0x0400036E RID: 878
		private readonly byte[] _userName;

		// Token: 0x0400036F RID: 879
		private readonly byte[] _methodNameBytes;

		// Token: 0x04000370 RID: 880
		private readonly string _methodName;
	}
}
