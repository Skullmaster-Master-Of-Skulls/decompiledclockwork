using System;
using System.IO;
using System.Security;
using NLog.Common;

namespace NLog.Internal.FileAppenders
{
	// Token: 0x0200008D RID: 141
	[SecuritySafeCritical]
	internal class SingleProcessFileAppender : BaseFileAppender
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x0000A418 File Offset: 0x00008618
		public SingleProcessFileAppender(string fileName, ICreateFileParameters parameters) : base(fileName, parameters)
		{
			FileInfo fileInfo = new FileInfo(fileName);
			if (fileInfo.Exists)
			{
				base.FileTouched(fileInfo.LastWriteTimeUtc);
			}
			else
			{
				base.FileTouched();
			}
			this.file = base.CreateFileStream(false);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000A45D File Offset: 0x0000865D
		public override void Write(byte[] bytes)
		{
			if (this.file == null)
			{
				return;
			}
			this.file.Write(bytes, 0, bytes.Length);
			base.FileTouched();
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000A47E File Offset: 0x0000867E
		public override void Flush()
		{
			if (this.file == null)
			{
				return;
			}
			this.file.Flush();
			base.FileTouched();
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000A49C File Offset: 0x0000869C
		public override void Close()
		{
			if (this.file == null)
			{
				return;
			}
			InternalLogger.Trace("Closing '{0}'", new object[]
			{
				base.FileName
			});
			this.file.Close();
			this.file = null;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000A4DF File Offset: 0x000086DF
		public override FileCharacteristics GetFileCharacteristics()
		{
			if (this.file != null)
			{
				return new FileCharacteristics(base.CreationTime, base.LastWriteTime, this.file.Length);
			}
			return null;
		}

		// Token: 0x040000F4 RID: 244
		public static readonly IFileAppenderFactory TheFactory = new SingleProcessFileAppender.Factory();

		// Token: 0x040000F5 RID: 245
		private FileStream file;

		// Token: 0x0200008E RID: 142
		private class Factory : IFileAppenderFactory
		{
			// Token: 0x060004A2 RID: 1186 RVA: 0x0000A513 File Offset: 0x00008713
			BaseFileAppender IFileAppenderFactory.Open(string fileName, ICreateFileParameters parameters)
			{
				return new SingleProcessFileAppender(fileName, parameters);
			}
		}
	}
}
