using System;
using System.IO;
using System.Security;

namespace NLog.Internal.FileAppenders
{
	// Token: 0x02000084 RID: 132
	[SecuritySafeCritical]
	internal class CountingSingleProcessFileAppender : BaseFileAppender
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x00009B8C File Offset: 0x00007D8C
		public CountingSingleProcessFileAppender(string fileName, ICreateFileParameters parameters) : base(fileName, parameters)
		{
			FileInfo fileInfo = new FileInfo(fileName);
			if (fileInfo.Exists)
			{
				base.FileTouched(fileInfo.LastWriteTimeUtc);
				this.currentFileLength = fileInfo.Length;
			}
			else
			{
				base.FileTouched();
				this.currentFileLength = 0L;
			}
			this.file = base.CreateFileStream(false);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00009BE5 File Offset: 0x00007DE5
		public override void Close()
		{
			if (this.file != null)
			{
				this.file.Close();
				this.file = null;
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00009C01 File Offset: 0x00007E01
		public override void Flush()
		{
			if (this.file == null)
			{
				return;
			}
			this.file.Flush();
			base.FileTouched();
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00009C1D File Offset: 0x00007E1D
		public override FileCharacteristics GetFileCharacteristics()
		{
			return new FileCharacteristics(base.CreationTime, base.LastWriteTime, this.currentFileLength);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00009C36 File Offset: 0x00007E36
		public override void Write(byte[] bytes)
		{
			if (this.file == null)
			{
				return;
			}
			this.currentFileLength += (long)bytes.Length;
			this.file.Write(bytes, 0, bytes.Length);
			base.FileTouched();
		}

		// Token: 0x040000E5 RID: 229
		public static readonly IFileAppenderFactory TheFactory = new CountingSingleProcessFileAppender.Factory();

		// Token: 0x040000E6 RID: 230
		private FileStream file;

		// Token: 0x040000E7 RID: 231
		private long currentFileLength;

		// Token: 0x02000086 RID: 134
		private class Factory : IFileAppenderFactory
		{
			// Token: 0x0600046B RID: 1131 RVA: 0x00009C74 File Offset: 0x00007E74
			BaseFileAppender IFileAppenderFactory.Open(string fileName, ICreateFileParameters parameters)
			{
				return new CountingSingleProcessFileAppender(fileName, parameters);
			}
		}
	}
}
