using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000017 RID: 23
	public class KeysRequiredEventArgs : EventArgs
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00005DB6 File Offset: 0x00004DB6
		public KeysRequiredEventArgs(string name)
		{
			this.fileName = name;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005DC5 File Offset: 0x00004DC5
		public KeysRequiredEventArgs(string name, byte[] keyValue)
		{
			this.fileName = name;
			this.key = keyValue;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005DDB File Offset: 0x00004DDB
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00005DE3 File Offset: 0x00004DE3
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00005DEB File Offset: 0x00004DEB
		public byte[] Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x040000BF RID: 191
		private string fileName;

		// Token: 0x040000C0 RID: 192
		private byte[] key;
	}
}
