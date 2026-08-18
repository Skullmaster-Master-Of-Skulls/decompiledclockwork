using System;

namespace Renci.SshNet.Messages.Authentication
{
	// Token: 0x020000C9 RID: 201
	internal class RequestMessagePassword : RequestMessage
	{
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0001F6AF File Offset: 0x0001D8AF
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x0001F6B7 File Offset: 0x0001D8B7
		public byte[] Password { get; private set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0001F6C0 File Offset: 0x0001D8C0
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x0001F6C8 File Offset: 0x0001D8C8
		public byte[] NewPassword { get; private set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0001F6D4 File Offset: 0x0001D8D4
		protected override int BufferCapacity
		{
			get
			{
				int num = base.BufferCapacity;
				num++;
				num += 4;
				num += this.Password.Length;
				if (this.NewPassword != null)
				{
					num += 4;
					num += this.NewPassword.Length;
				}
				return num;
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0001F713 File Offset: 0x0001D913
		public RequestMessagePassword(ServiceName serviceName, string username, byte[] password) : base(serviceName, username, "password")
		{
			this.Password = password;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001F729 File Offset: 0x0001D929
		public RequestMessagePassword(ServiceName serviceName, string username, byte[] password, byte[] newPassword) : this(serviceName, username, password)
		{
			this.NewPassword = newPassword;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001F73C File Offset: 0x0001D93C
		protected override void SaveData()
		{
			base.SaveData();
			base.Write(this.NewPassword != null);
			base.WriteBinaryString(this.Password);
			if (this.NewPassword != null)
			{
				base.WriteBinaryString(this.NewPassword);
			}
		}
	}
}
