using System;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x020006DA RID: 1754
	internal class FileDetails
	{
		// Token: 0x06005466 RID: 21606 RVA: 0x00127BAE File Offset: 0x00125DAE
		internal FileDetails(bool exists, long fileSize, DateTime utcCreationTime, DateTime utcLastWriteTime)
		{
			this._exists = exists;
			this._fileSize = fileSize;
			this._utcCreationTime = utcCreationTime;
			this._utcLastWriteTime = utcLastWriteTime;
		}

		// Token: 0x06005467 RID: 21607 RVA: 0x00127BD4 File Offset: 0x00125DD4
		public override bool Equals(object obj)
		{
			FileDetails fileDetails = obj as FileDetails;
			return fileDetails != null && this._exists == fileDetails._exists && this._fileSize == fileDetails._fileSize && this._utcCreationTime == fileDetails._utcCreationTime && this._utcLastWriteTime == fileDetails._utcLastWriteTime;
		}

		// Token: 0x06005468 RID: 21608 RVA: 0x00127C2D File Offset: 0x00125E2D
		public override int GetHashCode()
		{
			return HashCodeCombiner.CombineHashCodes(this._exists.GetHashCode(), this._fileSize.GetHashCode(), this._utcCreationTime.GetHashCode(), this._utcLastWriteTime.GetHashCode());
		}

		// Token: 0x04002C4D RID: 11341
		private bool _exists;

		// Token: 0x04002C4E RID: 11342
		private long _fileSize;

		// Token: 0x04002C4F RID: 11343
		private DateTime _utcCreationTime;

		// Token: 0x04002C50 RID: 11344
		private DateTime _utcLastWriteTime;
	}
}
