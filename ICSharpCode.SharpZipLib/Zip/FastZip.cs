using System;
using System.Collections;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000059 RID: 89
	public class FastZip
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x00015982 File Offset: 0x00014982
		public FastZip()
		{
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001599C File Offset: 0x0001499C
		public FastZip(FastZipEvents events)
		{
			this.events_ = events;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x000159BD File Offset: 0x000149BD
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x000159C5 File Offset: 0x000149C5
		public bool CreateEmptyDirectories
		{
			get
			{
				return this.createEmptyDirectories_;
			}
			set
			{
				this.createEmptyDirectories_ = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x000159CE File Offset: 0x000149CE
		// (set) Token: 0x060003CA RID: 970 RVA: 0x000159D6 File Offset: 0x000149D6
		public string Password
		{
			get
			{
				return this.password_;
			}
			set
			{
				this.password_ = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003CB RID: 971 RVA: 0x000159DF File Offset: 0x000149DF
		// (set) Token: 0x060003CC RID: 972 RVA: 0x000159EC File Offset: 0x000149EC
		public INameTransform NameTransform
		{
			get
			{
				return this.entryFactory_.NameTransform;
			}
			set
			{
				this.entryFactory_.NameTransform = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003CD RID: 973 RVA: 0x000159FA File Offset: 0x000149FA
		// (set) Token: 0x060003CE RID: 974 RVA: 0x00015A02 File Offset: 0x00014A02
		public IEntryFactory EntryFactory
		{
			get
			{
				return this.entryFactory_;
			}
			set
			{
				if (value == null)
				{
					this.entryFactory_ = new ZipEntryFactory();
					return;
				}
				this.entryFactory_ = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00015A1A File Offset: 0x00014A1A
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x00015A22 File Offset: 0x00014A22
		public UseZip64 UseZip64
		{
			get
			{
				return this.useZip64_;
			}
			set
			{
				this.useZip64_ = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00015A2B File Offset: 0x00014A2B
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x00015A33 File Offset: 0x00014A33
		public bool RestoreDateTimeOnExtract
		{
			get
			{
				return this.restoreDateTimeOnExtract_;
			}
			set
			{
				this.restoreDateTimeOnExtract_ = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00015A3C File Offset: 0x00014A3C
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x00015A44 File Offset: 0x00014A44
		public bool RestoreAttributesOnExtract
		{
			get
			{
				return this.restoreAttributesOnExtract_;
			}
			set
			{
				this.restoreAttributesOnExtract_ = value;
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00015A4D File Offset: 0x00014A4D
		public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
		{
			this.CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, directoryFilter);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00015A61 File Offset: 0x00014A61
		public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter)
		{
			this.CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, null);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00015A74 File Offset: 0x00014A74
		public void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
		{
			this.NameTransform = new ZipNameTransform(sourceDirectory);
			this.sourceDirectory_ = sourceDirectory;
			using (this.outputStream_ = new ZipOutputStream(outputStream))
			{
				if (this.password_ != null)
				{
					this.outputStream_.Password = this.password_;
				}
				this.outputStream_.UseZip64 = this.UseZip64;
				FileSystemScanner fileSystemScanner = new FileSystemScanner(fileFilter, directoryFilter);
				FileSystemScanner fileSystemScanner2 = fileSystemScanner;
				fileSystemScanner2.ProcessFile = (ProcessFileHandler)Delegate.Combine(fileSystemScanner2.ProcessFile, new ProcessFileHandler(this.ProcessFile));
				if (this.CreateEmptyDirectories)
				{
					FileSystemScanner fileSystemScanner3 = fileSystemScanner;
					fileSystemScanner3.ProcessDirectory = (ProcessDirectoryHandler)Delegate.Combine(fileSystemScanner3.ProcessDirectory, new ProcessDirectoryHandler(this.ProcessDirectory));
				}
				if (this.events_ != null)
				{
					if (this.events_.FileFailure != null)
					{
						FileSystemScanner fileSystemScanner4 = fileSystemScanner;
						fileSystemScanner4.FileFailure = (FileFailureHandler)Delegate.Combine(fileSystemScanner4.FileFailure, this.events_.FileFailure);
					}
					if (this.events_.DirectoryFailure != null)
					{
						FileSystemScanner fileSystemScanner5 = fileSystemScanner;
						fileSystemScanner5.DirectoryFailure = (DirectoryFailureHandler)Delegate.Combine(fileSystemScanner5.DirectoryFailure, this.events_.DirectoryFailure);
					}
				}
				fileSystemScanner.Scan(sourceDirectory, recurse);
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00015BAC File Offset: 0x00014BAC
		public void ExtractZip(string zipFileName, string targetDirectory, string fileFilter)
		{
			this.ExtractZip(zipFileName, targetDirectory, FastZip.Overwrite.Always, null, fileFilter, null, this.restoreDateTimeOnExtract_);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00015BC0 File Offset: 0x00014BC0
		public void ExtractZip(string zipFileName, string targetDirectory, FastZip.Overwrite overwrite, FastZip.ConfirmOverwriteDelegate confirmDelegate, string fileFilter, string directoryFilter, bool restoreDateTime)
		{
			Stream inputStream = File.Open(zipFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			this.ExtractZip(inputStream, targetDirectory, overwrite, confirmDelegate, fileFilter, directoryFilter, restoreDateTime, true);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00015BEC File Offset: 0x00014BEC
		public void ExtractZip(Stream inputStream, string targetDirectory, FastZip.Overwrite overwrite, FastZip.ConfirmOverwriteDelegate confirmDelegate, string fileFilter, string directoryFilter, bool restoreDateTime, bool isStreamOwner)
		{
			if (overwrite == FastZip.Overwrite.Prompt && confirmDelegate == null)
			{
				throw new ArgumentNullException("confirmDelegate");
			}
			this.continueRunning_ = true;
			this.overwrite_ = overwrite;
			this.confirmDelegate_ = confirmDelegate;
			this.extractNameTransform_ = new WindowsNameTransform(targetDirectory);
			this.fileFilter_ = new NameFilter(fileFilter);
			this.directoryFilter_ = new NameFilter(directoryFilter);
			this.restoreDateTimeOnExtract_ = restoreDateTime;
			using (this.zipFile_ = new ZipFile(inputStream))
			{
				if (this.password_ != null)
				{
					this.zipFile_.Password = this.password_;
				}
				this.zipFile_.IsStreamOwner = isStreamOwner;
				IEnumerator enumerator = this.zipFile_.GetEnumerator();
				while (this.continueRunning_ && enumerator.MoveNext())
				{
					ZipEntry zipEntry = (ZipEntry)enumerator.Current;
					if (zipEntry.IsFile)
					{
						if (this.directoryFilter_.IsMatch(Path.GetDirectoryName(zipEntry.Name)) && this.fileFilter_.IsMatch(zipEntry.Name))
						{
							this.ExtractEntry(zipEntry);
						}
					}
					else if (zipEntry.IsDirectory && this.directoryFilter_.IsMatch(zipEntry.Name) && this.CreateEmptyDirectories)
					{
						this.ExtractEntry(zipEntry);
					}
				}
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00015D34 File Offset: 0x00014D34
		private void ProcessDirectory(object sender, DirectoryEventArgs e)
		{
			if (!e.HasMatchingFiles && this.CreateEmptyDirectories)
			{
				if (this.events_ != null)
				{
					this.events_.OnProcessDirectory(e.Name, e.HasMatchingFiles);
				}
				if (e.ContinueRunning && e.Name != this.sourceDirectory_)
				{
					ZipEntry entry = this.entryFactory_.MakeDirectoryEntry(e.Name);
					this.outputStream_.PutNextEntry(entry);
				}
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00015DAC File Offset: 0x00014DAC
		private void ProcessFile(object sender, ScanEventArgs e)
		{
			if (this.events_ != null && this.events_.ProcessFile != null)
			{
				this.events_.ProcessFile(sender, e);
			}
			if (e.ContinueRunning)
			{
				try
				{
					using (FileStream fileStream = File.Open(e.Name, FileMode.Open, FileAccess.Read, FileShare.Read))
					{
						ZipEntry entry = this.entryFactory_.MakeFileEntry(e.Name);
						this.outputStream_.PutNextEntry(entry);
						this.AddFileContents(e.Name, fileStream);
					}
				}
				catch (Exception e2)
				{
					if (this.events_ == null)
					{
						this.continueRunning_ = false;
						throw;
					}
					this.continueRunning_ = this.events_.OnFileFailure(e.Name, e2);
				}
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00015E7C File Offset: 0x00014E7C
		private void AddFileContents(string name, Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (this.buffer_ == null)
			{
				this.buffer_ = new byte[4096];
			}
			if (this.events_ != null && this.events_.Progress != null)
			{
				StreamUtils.Copy(stream, this.outputStream_, this.buffer_, this.events_.Progress, this.events_.ProgressInterval, this, name);
			}
			else
			{
				StreamUtils.Copy(stream, this.outputStream_, this.buffer_);
			}
			if (this.events_ != null)
			{
				this.continueRunning_ = this.events_.OnCompletedFile(name);
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00015F1C File Offset: 0x00014F1C
		private void ExtractFileEntry(ZipEntry entry, string targetName)
		{
			bool flag = true;
			if (this.overwrite_ != FastZip.Overwrite.Always && File.Exists(targetName))
			{
				flag = (this.overwrite_ == FastZip.Overwrite.Prompt && this.confirmDelegate_ != null && this.confirmDelegate_(targetName));
			}
			if (flag)
			{
				if (this.events_ != null)
				{
					this.continueRunning_ = this.events_.OnProcessFile(entry.Name);
				}
				if (this.continueRunning_)
				{
					try
					{
						using (FileStream fileStream = File.Create(targetName))
						{
							if (this.buffer_ == null)
							{
								this.buffer_ = new byte[4096];
							}
							if (this.events_ != null && this.events_.Progress != null)
							{
								StreamUtils.Copy(this.zipFile_.GetInputStream(entry), fileStream, this.buffer_, this.events_.Progress, this.events_.ProgressInterval, this, entry.Name, entry.Size);
							}
							else
							{
								StreamUtils.Copy(this.zipFile_.GetInputStream(entry), fileStream, this.buffer_);
							}
							if (this.events_ != null)
							{
								this.continueRunning_ = this.events_.OnCompletedFile(entry.Name);
							}
						}
						if (this.restoreDateTimeOnExtract_)
						{
							File.SetLastWriteTime(targetName, entry.DateTime);
						}
						if (this.RestoreAttributesOnExtract && entry.IsDOSEntry && entry.ExternalFileAttributes != -1)
						{
							FileAttributes fileAttributes = (FileAttributes)entry.ExternalFileAttributes;
							fileAttributes &= (FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.Archive | FileAttributes.Normal);
							File.SetAttributes(targetName, fileAttributes);
						}
					}
					catch (Exception e)
					{
						if (this.events_ == null)
						{
							this.continueRunning_ = false;
							throw;
						}
						this.continueRunning_ = this.events_.OnFileFailure(targetName, e);
					}
				}
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000160CC File Offset: 0x000150CC
		private void ExtractEntry(ZipEntry entry)
		{
			bool flag = entry.IsCompressionMethodSupported();
			string text = entry.Name;
			if (flag)
			{
				if (entry.IsFile)
				{
					text = this.extractNameTransform_.TransformFile(text);
				}
				else if (entry.IsDirectory)
				{
					text = this.extractNameTransform_.TransformDirectory(text);
				}
				flag = (text != null && text.Length != 0);
			}
			string path = null;
			if (flag)
			{
				if (entry.IsDirectory)
				{
					path = text;
				}
				else
				{
					path = Path.GetDirectoryName(Path.GetFullPath(text));
				}
			}
			if (flag && !Directory.Exists(path))
			{
				if (entry.IsDirectory)
				{
					if (!this.CreateEmptyDirectories)
					{
						goto IL_D9;
					}
				}
				try
				{
					Directory.CreateDirectory(path);
				}
				catch (Exception e)
				{
					flag = false;
					if (this.events_ == null)
					{
						this.continueRunning_ = false;
						throw;
					}
					if (entry.IsDirectory)
					{
						this.continueRunning_ = this.events_.OnDirectoryFailure(text, e);
					}
					else
					{
						this.continueRunning_ = this.events_.OnFileFailure(text, e);
					}
				}
			}
			IL_D9:
			if (flag && entry.IsFile)
			{
				this.ExtractFileEntry(entry, text);
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000161D8 File Offset: 0x000151D8
		private static int MakeExternalAttributes(FileInfo info)
		{
			return (int)info.Attributes;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000161E0 File Offset: 0x000151E0
		private static bool NameIsValid(string name)
		{
			return name != null && name.Length > 0 && name.IndexOfAny(Path.GetInvalidPathChars()) < 0;
		}

		// Token: 0x040002B1 RID: 689
		private bool continueRunning_;

		// Token: 0x040002B2 RID: 690
		private byte[] buffer_;

		// Token: 0x040002B3 RID: 691
		private ZipOutputStream outputStream_;

		// Token: 0x040002B4 RID: 692
		private ZipFile zipFile_;

		// Token: 0x040002B5 RID: 693
		private string sourceDirectory_;

		// Token: 0x040002B6 RID: 694
		private NameFilter fileFilter_;

		// Token: 0x040002B7 RID: 695
		private NameFilter directoryFilter_;

		// Token: 0x040002B8 RID: 696
		private FastZip.Overwrite overwrite_;

		// Token: 0x040002B9 RID: 697
		private FastZip.ConfirmOverwriteDelegate confirmDelegate_;

		// Token: 0x040002BA RID: 698
		private bool restoreDateTimeOnExtract_;

		// Token: 0x040002BB RID: 699
		private bool restoreAttributesOnExtract_;

		// Token: 0x040002BC RID: 700
		private bool createEmptyDirectories_;

		// Token: 0x040002BD RID: 701
		private FastZipEvents events_;

		// Token: 0x040002BE RID: 702
		private IEntryFactory entryFactory_ = new ZipEntryFactory();

		// Token: 0x040002BF RID: 703
		private INameTransform extractNameTransform_;

		// Token: 0x040002C0 RID: 704
		private UseZip64 useZip64_ = UseZip64.Dynamic;

		// Token: 0x040002C1 RID: 705
		private string password_;

		// Token: 0x0200005A RID: 90
		public enum Overwrite
		{
			// Token: 0x040002C3 RID: 707
			Prompt,
			// Token: 0x040002C4 RID: 708
			Never,
			// Token: 0x040002C5 RID: 709
			Always
		}

		// Token: 0x0200005B RID: 91
		// (Invoke) Token: 0x060003E3 RID: 995
		public delegate bool ConfirmOverwriteDelegate(string fileName);
	}
}
