using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000625 RID: 1573
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeChecksumPragma : CodeDirective
	{
		// Token: 0x06003979 RID: 14713 RVA: 0x000F2F1C File Offset: 0x000F111C
		public CodeChecksumPragma()
		{
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x000F2F24 File Offset: 0x000F1124
		public CodeChecksumPragma(string fileName, Guid checksumAlgorithmId, byte[] checksumData)
		{
			this.fileName = fileName;
			this.checksumAlgorithmId = checksumAlgorithmId;
			this.checksumData = checksumData;
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x0600397B RID: 14715 RVA: 0x000F2F41 File Offset: 0x000F1141
		// (set) Token: 0x0600397C RID: 14716 RVA: 0x000F2F57 File Offset: 0x000F1157
		public string FileName
		{
			get
			{
				if (this.fileName != null)
				{
					return this.fileName;
				}
				return string.Empty;
			}
			set
			{
				this.fileName = value;
			}
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x0600397D RID: 14717 RVA: 0x000F2F60 File Offset: 0x000F1160
		// (set) Token: 0x0600397E RID: 14718 RVA: 0x000F2F68 File Offset: 0x000F1168
		public Guid ChecksumAlgorithmId
		{
			get
			{
				return this.checksumAlgorithmId;
			}
			set
			{
				this.checksumAlgorithmId = value;
			}
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x0600397F RID: 14719 RVA: 0x000F2F71 File Offset: 0x000F1171
		// (set) Token: 0x06003980 RID: 14720 RVA: 0x000F2F79 File Offset: 0x000F1179
		public byte[] ChecksumData
		{
			get
			{
				return this.checksumData;
			}
			set
			{
				this.checksumData = value;
			}
		}

		// Token: 0x04002BB3 RID: 11187
		private string fileName;

		// Token: 0x04002BB4 RID: 11188
		private byte[] checksumData;

		// Token: 0x04002BB5 RID: 11189
		private Guid checksumAlgorithmId;
	}
}
