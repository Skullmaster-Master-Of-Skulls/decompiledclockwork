using System;
using System.ComponentModel;

namespace Infralution.Licensing
{
	// Token: 0x02000018 RID: 24
	public class EncryptedLicense : License
	{
		// Token: 0x06000086 RID: 134 RVA: 0x000038D6 File Offset: 0x00001AD6
		public EncryptedLicense(string key, ushort serialNo, string productInfo)
		{
			this._key = key;
			this._serialNo = serialNo;
			this._productInfo = productInfo;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000038F8 File Offset: 0x00001AF8
		public override string LicenseKey
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003910 File Offset: 0x00001B10
		public string ProductInfo
		{
			get
			{
				return this._productInfo;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003928 File Offset: 0x00001B28
		public ushort SerialNo
		{
			get
			{
				return this._serialNo;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003940 File Offset: 0x00001B40
		public override void Dispose()
		{
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003944 File Offset: 0x00001B44
		public static string Checksum(string input)
		{
			int hashCode = input.GetHashCode();
			return Math.Abs(hashCode % 1000).ToString();
		}

		// Token: 0x0400002E RID: 46
		private string _key;

		// Token: 0x0400002F RID: 47
		private ushort _serialNo;

		// Token: 0x04000030 RID: 48
		private string _productInfo;
	}
}
