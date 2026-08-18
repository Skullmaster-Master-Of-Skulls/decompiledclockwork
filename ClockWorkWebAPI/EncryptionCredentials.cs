using System;

namespace ClockWorkWebAPI
{
	// Token: 0x02000018 RID: 24
	public class EncryptionCredentials
	{
		// Token: 0x0600016A RID: 362 RVA: 0x0000AA38 File Offset: 0x00008C38
		public EncryptionCredentials(string hash, string vector, string pass, string salt)
		{
			this.hash = hash;
			this.vector = vector;
			this.pass = pass;
			this.salt = salt;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000AA60 File Offset: 0x00008C60
		public string Hash
		{
			get
			{
				return this.hash;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000AA78 File Offset: 0x00008C78
		public string Vector
		{
			get
			{
				return this.vector;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000AA90 File Offset: 0x00008C90
		public string Pass
		{
			get
			{
				return this.pass;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000AAA8 File Offset: 0x00008CA8
		public string Salt
		{
			get
			{
				return this.salt;
			}
		}

		// Token: 0x04000075 RID: 117
		private string hash;

		// Token: 0x04000076 RID: 118
		private string vector;

		// Token: 0x04000077 RID: 119
		private string pass;

		// Token: 0x04000078 RID: 120
		private string salt;
	}
}
