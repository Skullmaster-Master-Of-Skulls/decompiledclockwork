using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200002D RID: 45
	public class DiskArchiveStorage : BaseArchiveStorage
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x00009508 File Offset: 0x00008508
		public DiskArchiveStorage(ZipFile file, FileUpdateMode updateMode) : base(updateMode)
		{
			if (file.Name == null)
			{
				throw new ZipException("Cant handle non file archives");
			}
			this.fileName_ = file.Name;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00009530 File Offset: 0x00008530
		public DiskArchiveStorage(ZipFile file) : this(file, FileUpdateMode.Safe)
		{
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000953C File Offset: 0x0000853C
		public override Stream GetTemporaryOutput()
		{
			if (this.temporaryName_ != null)
			{
				this.temporaryName_ = DiskArchiveStorage.GetTempFileName(this.temporaryName_, true);
				this.temporaryStream_ = File.Open(this.temporaryName_, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
			}
			else
			{
				this.temporaryName_ = Path.GetTempFileName();
				this.temporaryStream_ = File.Open(this.temporaryName_, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
			}
			return this.temporaryStream_;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000095A0 File Offset: 0x000085A0
		public override Stream ConvertTemporaryToFinal()
		{
			if (this.temporaryStream_ == null)
			{
				throw new ZipException("No temporary stream has been created");
			}
			Stream result = null;
			string tempFileName = DiskArchiveStorage.GetTempFileName(this.fileName_, false);
			bool flag = false;
			try
			{
				this.temporaryStream_.Close();
				File.Move(this.fileName_, tempFileName);
				File.Move(this.temporaryName_, this.fileName_);
				flag = true;
				File.Delete(tempFileName);
				result = File.Open(this.fileName_, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			catch (Exception)
			{
				result = null;
				if (!flag)
				{
					File.Move(tempFileName, this.fileName_);
					File.Delete(this.temporaryName_);
				}
				throw;
			}
			return result;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00009644 File Offset: 0x00008644
		public override Stream MakeTemporaryCopy(Stream stream)
		{
			stream.Close();
			this.temporaryName_ = DiskArchiveStorage.GetTempFileName(this.fileName_, true);
			File.Copy(this.fileName_, this.temporaryName_, true);
			this.temporaryStream_ = new FileStream(this.temporaryName_, FileMode.Open, FileAccess.ReadWrite);
			return this.temporaryStream_;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00009694 File Offset: 0x00008694
		public override Stream OpenForDirectUpdate(Stream stream)
		{
			Stream result;
			if (stream == null || !stream.CanWrite)
			{
				if (stream != null)
				{
					stream.Close();
				}
				result = new FileStream(this.fileName_, FileMode.Open, FileAccess.ReadWrite);
			}
			else
			{
				result = stream;
			}
			return result;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000096C8 File Offset: 0x000086C8
		public override void Dispose()
		{
			if (this.temporaryStream_ != null)
			{
				this.temporaryStream_.Close();
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000096E0 File Offset: 0x000086E0
		private static string GetTempFileName(string original, bool makeTempFile)
		{
			string text = null;
			if (original == null)
			{
				text = Path.GetTempFileName();
			}
			else
			{
				int num = 0;
				int second = DateTime.Now.Second;
				while (text == null)
				{
					num++;
					string text2 = string.Format("{0}.{1}{2}.tmp", original, second, num);
					if (!File.Exists(text2))
					{
						if (makeTempFile)
						{
							try
							{
								using (File.Create(text2))
								{
								}
								text = text2;
								continue;
							}
							catch
							{
								second = DateTime.Now.Second;
								continue;
							}
						}
						text = text2;
					}
				}
			}
			return text;
		}

		// Token: 0x04000109 RID: 265
		private Stream temporaryStream_;

		// Token: 0x0400010A RID: 266
		private string fileName_;

		// Token: 0x0400010B RID: 267
		private string temporaryName_;
	}
}
