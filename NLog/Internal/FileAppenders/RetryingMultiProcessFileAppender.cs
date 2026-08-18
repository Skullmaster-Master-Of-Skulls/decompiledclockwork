using System;
using System.IO;
using System.Security;

namespace NLog.Internal.FileAppenders
{
	// Token: 0x0200008B RID: 139
	[SecuritySafeCritical]
	internal class RetryingMultiProcessFileAppender : BaseFileAppender
	{
		// Token: 0x06000494 RID: 1172 RVA: 0x0000A36D File Offset: 0x0000856D
		public RetryingMultiProcessFileAppender(string fileName, ICreateFileParameters parameters) : base(fileName, parameters)
		{
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000A378 File Offset: 0x00008578
		public override void Write(byte[] bytes)
		{
			using (FileStream fileStream = base.CreateFileStream(false))
			{
				fileStream.Write(bytes, 0, bytes.Length);
			}
			base.FileTouched();
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000A3BC File Offset: 0x000085BC
		public override void Flush()
		{
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000A3BE File Offset: 0x000085BE
		public override void Close()
		{
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000A3C0 File Offset: 0x000085C0
		public override FileCharacteristics GetFileCharacteristics()
		{
			FileInfo fileInfo = new FileInfo(base.FileName);
			if (fileInfo.Exists)
			{
				return new FileCharacteristics(fileInfo.CreationTimeUtc, fileInfo.LastWriteTimeUtc, fileInfo.Length);
			}
			return null;
		}

		// Token: 0x040000F3 RID: 243
		public static readonly IFileAppenderFactory TheFactory = new RetryingMultiProcessFileAppender.Factory();

		// Token: 0x0200008C RID: 140
		private class Factory : IFileAppenderFactory
		{
			// Token: 0x0600049A RID: 1178 RVA: 0x0000A406 File Offset: 0x00008606
			BaseFileAppender IFileAppenderFactory.Open(string fileName, ICreateFileParameters parameters)
			{
				return new RetryingMultiProcessFileAppender(fileName, parameters);
			}
		}
	}
}
