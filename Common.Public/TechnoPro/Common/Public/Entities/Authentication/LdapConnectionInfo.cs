using System;

namespace TechnoPro.Common.Public.Entities.Authentication
{
	// Token: 0x02000492 RID: 1170
	public class LdapConnectionInfo : BusinessBase<string>
	{
		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06002336 RID: 9014 RVA: 0x00026CC8 File Offset: 0x00024EC8
		// (set) Token: 0x06002337 RID: 9015 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public virtual string ServerName
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06002338 RID: 9016 RVA: 0x00026CE0 File Offset: 0x00024EE0
		// (set) Token: 0x06002339 RID: 9017 RVA: 0x00026CE8 File Offset: 0x00024EE8
		public virtual int Port { get; set; }

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x0600233A RID: 9018 RVA: 0x00026CF1 File Offset: 0x00024EF1
		// (set) Token: 0x0600233B RID: 9019 RVA: 0x00026CF9 File Offset: 0x00024EF9
		public virtual string LookupAttribute { get; set; }

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x0600233C RID: 9020 RVA: 0x00026D02 File Offset: 0x00024F02
		// (set) Token: 0x0600233D RID: 9021 RVA: 0x00026D0A File Offset: 0x00024F0A
		public virtual string AuthType { get; set; }

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x0600233E RID: 9022 RVA: 0x00026D13 File Offset: 0x00024F13
		// (set) Token: 0x0600233F RID: 9023 RVA: 0x00026D1B File Offset: 0x00024F1B
		public virtual bool SSL { get; set; }

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06002340 RID: 9024 RVA: 0x00026D24 File Offset: 0x00024F24
		// (set) Token: 0x06002341 RID: 9025 RVA: 0x00026D2C File Offset: 0x00024F2C
		public virtual bool TLS { get; set; }

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06002342 RID: 9026 RVA: 0x00026D35 File Offset: 0x00024F35
		// (set) Token: 0x06002343 RID: 9027 RVA: 0x00026D3D File Offset: 0x00024F3D
		public virtual bool DontVerifyServerCertificate { get; set; }

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06002344 RID: 9028 RVA: 0x00026D46 File Offset: 0x00024F46
		// (set) Token: 0x06002345 RID: 9029 RVA: 0x00026D4E File Offset: 0x00024F4E
		public virtual string Domain { get; set; }

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06002346 RID: 9030 RVA: 0x00026D57 File Offset: 0x00024F57
		// (set) Token: 0x06002347 RID: 9031 RVA: 0x00026D5F File Offset: 0x00024F5F
		public virtual int ProtocolVersion { get; set; }

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x00026D68 File Offset: 0x00024F68
		// (set) Token: 0x06002349 RID: 9033 RVA: 0x00026D70 File Offset: 0x00024F70
		public virtual string[] ReturnAttributes { get; set; }

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x0600234A RID: 9034 RVA: 0x00026D79 File Offset: 0x00024F79
		// (set) Token: 0x0600234B RID: 9035 RVA: 0x00026D81 File Offset: 0x00024F81
		public virtual bool IsDoubleBinding { get; set; }

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x00026D8A File Offset: 0x00024F8A
		// (set) Token: 0x0600234D RID: 9037 RVA: 0x00026D92 File Offset: 0x00024F92
		public virtual bool IsActiveDirectory { get; set; }

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x0600234E RID: 9038 RVA: 0x00026D9B File Offset: 0x00024F9B
		// (set) Token: 0x0600234F RID: 9039 RVA: 0x00026DA3 File Offset: 0x00024FA3
		public virtual bool UseLookupAttributeForActiveDirectory { get; set; }

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06002350 RID: 9040 RVA: 0x00026DAC File Offset: 0x00024FAC
		// (set) Token: 0x06002351 RID: 9041 RVA: 0x00026DB4 File Offset: 0x00024FB4
		public virtual string PreUsername { get; set; }

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06002352 RID: 9042 RVA: 0x00026DBD File Offset: 0x00024FBD
		// (set) Token: 0x06002353 RID: 9043 RVA: 0x00026DC5 File Offset: 0x00024FC5
		public virtual string PrePassword { get; set; }

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06002354 RID: 9044 RVA: 0x00026DCE File Offset: 0x00024FCE
		// (set) Token: 0x06002355 RID: 9045 RVA: 0x00026DD6 File Offset: 0x00024FD6
		public virtual string PreDomain { get; set; }

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06002356 RID: 9046 RVA: 0x00026DDF File Offset: 0x00024FDF
		// (set) Token: 0x06002357 RID: 9047 RVA: 0x00026DE7 File Offset: 0x00024FE7
		public virtual string PreLookupAttribute { get; set; }
	}
}
