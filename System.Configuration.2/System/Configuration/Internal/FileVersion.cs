using System;

namespace System.Configuration.Internal
{
	// Token: 0x020000AC RID: 172
	internal class FileVersion
	{
		// Token: 0x060006E7 RID: 1767 RVA: 0x0001F935 File Offset: 0x0001DB35
		internal FileVersion(bool exists, long fileSize, DateTime utcCreationTime, DateTime utcLastWriteTime)
		{
			this._exists = exists;
			this._fileSize = fileSize;
			this._utcCreationTime = utcCreationTime;
			this._utcLastWriteTime = utcLastWriteTime;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001F95C File Offset: 0x0001DB5C
		public override bool Equals(object obj)
		{
			FileVersion fileVersion = obj as FileVersion;
			return fileVersion != null && this._exists == fileVersion._exists && this._fileSize == fileVersion._fileSize && this._utcCreationTime == fileVersion._utcCreationTime && this._utcLastWriteTime == fileVersion._utcLastWriteTime;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001F9B5 File Offset: 0x0001DBB5
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000450 RID: 1104
		private bool _exists;

		// Token: 0x04000451 RID: 1105
		private long _fileSize;

		// Token: 0x04000452 RID: 1106
		private DateTime _utcCreationTime;

		// Token: 0x04000453 RID: 1107
		private DateTime _utcLastWriteTime;
	}
}
