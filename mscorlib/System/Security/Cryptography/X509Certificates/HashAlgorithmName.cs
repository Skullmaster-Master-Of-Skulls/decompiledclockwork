using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008CE RID: 2254
	internal struct HashAlgorithmName : IEquatable<HashAlgorithmName>
	{
		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06005247 RID: 21063 RVA: 0x0012748C File Offset: 0x0012648C
		public static HashAlgorithmName MD5
		{
			get
			{
				return new HashAlgorithmName("MD5");
			}
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06005248 RID: 21064 RVA: 0x00127498 File Offset: 0x00126498
		public static HashAlgorithmName SHA1
		{
			get
			{
				return new HashAlgorithmName("SHA1");
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06005249 RID: 21065 RVA: 0x001274A4 File Offset: 0x001264A4
		public static HashAlgorithmName SHA256
		{
			get
			{
				return new HashAlgorithmName("SHA256");
			}
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x0600524A RID: 21066 RVA: 0x001274B0 File Offset: 0x001264B0
		public static HashAlgorithmName SHA384
		{
			get
			{
				return new HashAlgorithmName("SHA384");
			}
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x0600524B RID: 21067 RVA: 0x001274BC File Offset: 0x001264BC
		public static HashAlgorithmName SHA512
		{
			get
			{
				return new HashAlgorithmName("SHA512");
			}
		}

		// Token: 0x0600524C RID: 21068 RVA: 0x001274C8 File Offset: 0x001264C8
		public HashAlgorithmName(string name)
		{
			this._name = name;
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x0600524D RID: 21069 RVA: 0x001274D1 File Offset: 0x001264D1
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x0600524E RID: 21070 RVA: 0x001274D9 File Offset: 0x001264D9
		public override string ToString()
		{
			return this._name ?? string.Empty;
		}

		// Token: 0x0600524F RID: 21071 RVA: 0x001274EA File Offset: 0x001264EA
		public override bool Equals(object obj)
		{
			return obj is HashAlgorithmName && this.Equals((HashAlgorithmName)obj);
		}

		// Token: 0x06005250 RID: 21072 RVA: 0x00127502 File Offset: 0x00126502
		public bool Equals(HashAlgorithmName other)
		{
			return this._name == other._name;
		}

		// Token: 0x06005251 RID: 21073 RVA: 0x00127516 File Offset: 0x00126516
		public override int GetHashCode()
		{
			if (this._name != null)
			{
				return this._name.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06005252 RID: 21074 RVA: 0x0012752D File Offset: 0x0012652D
		public static bool operator ==(HashAlgorithmName left, HashAlgorithmName right)
		{
			return left.Equals(right);
		}

		// Token: 0x06005253 RID: 21075 RVA: 0x00127537 File Offset: 0x00126537
		public static bool operator !=(HashAlgorithmName left, HashAlgorithmName right)
		{
			return !(left == right);
		}

		// Token: 0x04002A58 RID: 10840
		private readonly string _name;
	}
}
