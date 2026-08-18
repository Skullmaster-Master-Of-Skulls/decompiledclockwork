using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200002E RID: 46
	public class MemoryArchiveStorage : BaseArchiveStorage
	{
		// Token: 0x060001AE RID: 430 RVA: 0x00009784 File Offset: 0x00008784
		public MemoryArchiveStorage() : base(FileUpdateMode.Direct)
		{
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000978D File Offset: 0x0000878D
		public MemoryArchiveStorage(FileUpdateMode updateMode) : base(updateMode)
		{
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00009796 File Offset: 0x00008796
		public MemoryStream FinalStream
		{
			get
			{
				return this.finalStream_;
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000979E File Offset: 0x0000879E
		public override Stream GetTemporaryOutput()
		{
			this.temporaryStream_ = new MemoryStream();
			return this.temporaryStream_;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000097B1 File Offset: 0x000087B1
		public override Stream ConvertTemporaryToFinal()
		{
			if (this.temporaryStream_ == null)
			{
				throw new ZipException("No temporary stream has been created");
			}
			this.finalStream_ = new MemoryStream(this.temporaryStream_.ToArray());
			return this.finalStream_;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000097E2 File Offset: 0x000087E2
		public override Stream MakeTemporaryCopy(Stream stream)
		{
			this.temporaryStream_ = new MemoryStream();
			stream.Position = 0L;
			StreamUtils.Copy(stream, this.temporaryStream_, new byte[4096]);
			return this.temporaryStream_;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00009814 File Offset: 0x00008814
		public override Stream OpenForDirectUpdate(Stream stream)
		{
			Stream stream2;
			if (stream == null || !stream.CanWrite)
			{
				stream2 = new MemoryStream();
				if (stream != null)
				{
					stream.Position = 0L;
					StreamUtils.Copy(stream, stream2, new byte[4096]);
					stream.Close();
				}
			}
			else
			{
				stream2 = stream;
			}
			return stream2;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00009859 File Offset: 0x00008859
		public override void Dispose()
		{
			if (this.temporaryStream_ != null)
			{
				this.temporaryStream_.Close();
			}
		}

		// Token: 0x0400010C RID: 268
		private MemoryStream temporaryStream_;

		// Token: 0x0400010D RID: 269
		private MemoryStream finalStream_;
	}
}
