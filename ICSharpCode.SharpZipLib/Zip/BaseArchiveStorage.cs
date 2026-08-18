using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200002C RID: 44
	public abstract class BaseArchiveStorage : IArchiveStorage
	{
		// Token: 0x0600019F RID: 415 RVA: 0x000094F1 File Offset: 0x000084F1
		protected BaseArchiveStorage(FileUpdateMode updateMode)
		{
			this.updateMode_ = updateMode;
		}

		// Token: 0x060001A0 RID: 416
		public abstract Stream GetTemporaryOutput();

		// Token: 0x060001A1 RID: 417
		public abstract Stream ConvertTemporaryToFinal();

		// Token: 0x060001A2 RID: 418
		public abstract Stream MakeTemporaryCopy(Stream stream);

		// Token: 0x060001A3 RID: 419
		public abstract Stream OpenForDirectUpdate(Stream stream);

		// Token: 0x060001A4 RID: 420
		public abstract void Dispose();

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00009500 File Offset: 0x00008500
		public FileUpdateMode UpdateMode
		{
			get
			{
				return this.updateMode_;
			}
		}

		// Token: 0x04000108 RID: 264
		private FileUpdateMode updateMode_;
	}
}
