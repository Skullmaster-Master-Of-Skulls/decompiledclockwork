using System;

namespace EncryptionClassLibrary
{
	// Token: 0x0200000A RID: 10
	public class EncryptionCredentials
	{
		// Token: 0x06000044 RID: 68 RVA: 0x000038C9 File Offset: 0x00001AC9
		public EncryptionCredentials(string hash, string vector, string pass, string salt)
		{
			this.hash = hash;
			this.vector = vector;
			this.pass = pass;
			this.salt = salt;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000038F0 File Offset: 0x00001AF0
		public string Hash
		{
			get
			{
				return this.hash;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003908 File Offset: 0x00001B08
		public string Vector
		{
			get
			{
				return this.vector;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003920 File Offset: 0x00001B20
		public string Pass
		{
			get
			{
				return this.pass;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003938 File Offset: 0x00001B38
		public string Salt
		{
			get
			{
				return this.salt;
			}
		}

		// Token: 0x0400001A RID: 26
		private string hash;

		// Token: 0x0400001B RID: 27
		private string vector;

		// Token: 0x0400001C RID: 28
		private string pass;

		// Token: 0x0400001D RID: 29
		private string salt;
	}
}
