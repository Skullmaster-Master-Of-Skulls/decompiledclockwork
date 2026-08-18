using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000066 RID: 102
	public class FileSystemScanner
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x00016308 File Offset: 0x00015308
		public FileSystemScanner(string filter)
		{
			this.fileFilter_ = new PathFilter(filter);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001631C File Offset: 0x0001531C
		public FileSystemScanner(string fileFilter, string directoryFilter)
		{
			this.fileFilter_ = new PathFilter(fileFilter);
			this.directoryFilter_ = new PathFilter(directoryFilter);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001633C File Offset: 0x0001533C
		public FileSystemScanner(IScanFilter fileFilter)
		{
			this.fileFilter_ = fileFilter;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001634B File Offset: 0x0001534B
		public FileSystemScanner(IScanFilter fileFilter, IScanFilter directoryFilter)
		{
			this.fileFilter_ = fileFilter;
			this.directoryFilter_ = directoryFilter;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00016364 File Offset: 0x00015364
		private bool OnDirectoryFailure(string directory, Exception e)
		{
			DirectoryFailureHandler directoryFailure = this.DirectoryFailure;
			bool flag = directoryFailure != null;
			if (flag)
			{
				ScanFailureEventArgs scanFailureEventArgs = new ScanFailureEventArgs(directory, e);
				directoryFailure(this, scanFailureEventArgs);
				this.alive_ = scanFailureEventArgs.ContinueRunning;
			}
			return flag;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000163A0 File Offset: 0x000153A0
		private bool OnFileFailure(string file, Exception e)
		{
			FileFailureHandler fileFailure = this.FileFailure;
			bool flag = fileFailure != null;
			if (flag)
			{
				ScanFailureEventArgs scanFailureEventArgs = new ScanFailureEventArgs(file, e);
				this.FileFailure(this, scanFailureEventArgs);
				this.alive_ = scanFailureEventArgs.ContinueRunning;
			}
			return flag;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000163E4 File Offset: 0x000153E4
		private void OnProcessFile(string file)
		{
			ProcessFileHandler processFile = this.ProcessFile;
			if (processFile != null)
			{
				ScanEventArgs scanEventArgs = new ScanEventArgs(file);
				processFile(this, scanEventArgs);
				this.alive_ = scanEventArgs.ContinueRunning;
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00016418 File Offset: 0x00015418
		private void OnCompleteFile(string file)
		{
			CompletedFileHandler completedFile = this.CompletedFile;
			if (completedFile != null)
			{
				ScanEventArgs scanEventArgs = new ScanEventArgs(file);
				completedFile(this, scanEventArgs);
				this.alive_ = scanEventArgs.ContinueRunning;
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001644C File Offset: 0x0001544C
		private void OnProcessDirectory(string directory, bool hasMatchingFiles)
		{
			ProcessDirectoryHandler processDirectory = this.ProcessDirectory;
			if (processDirectory != null)
			{
				DirectoryEventArgs directoryEventArgs = new DirectoryEventArgs(directory, hasMatchingFiles);
				processDirectory(this, directoryEventArgs);
				this.alive_ = directoryEventArgs.ContinueRunning;
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001647F File Offset: 0x0001547F
		public void Scan(string directory, bool recurse)
		{
			this.alive_ = true;
			this.ScanDir(directory, recurse);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00016490 File Offset: 0x00015490
		private void ScanDir(string directory, bool recurse)
		{
			try
			{
				string[] files = Directory.GetFiles(directory);
				bool flag = false;
				for (int i = 0; i < files.Length; i++)
				{
					if (!this.fileFilter_.IsMatch(files[i]))
					{
						files[i] = null;
					}
					else
					{
						flag = true;
					}
				}
				this.OnProcessDirectory(directory, flag);
				if (this.alive_ && flag)
				{
					foreach (string text in files)
					{
						try
						{
							if (text != null)
							{
								this.OnProcessFile(text);
								if (!this.alive_)
								{
									break;
								}
							}
						}
						catch (Exception e)
						{
							if (!this.OnFileFailure(text, e))
							{
								throw;
							}
						}
					}
				}
			}
			catch (Exception e2)
			{
				if (!this.OnDirectoryFailure(directory, e2))
				{
					throw;
				}
			}
			if (this.alive_ && recurse)
			{
				try
				{
					string[] directories = Directory.GetDirectories(directory);
					foreach (string text2 in directories)
					{
						if (this.directoryFilter_ == null || this.directoryFilter_.IsMatch(text2))
						{
							this.ScanDir(text2, true);
							if (!this.alive_)
							{
								break;
							}
						}
					}
				}
				catch (Exception e3)
				{
					if (!this.OnDirectoryFailure(directory, e3))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x040002D0 RID: 720
		public ProcessDirectoryHandler ProcessDirectory;

		// Token: 0x040002D1 RID: 721
		public ProcessFileHandler ProcessFile;

		// Token: 0x040002D2 RID: 722
		public CompletedFileHandler CompletedFile;

		// Token: 0x040002D3 RID: 723
		public DirectoryFailureHandler DirectoryFailure;

		// Token: 0x040002D4 RID: 724
		public FileFailureHandler FileFailure;

		// Token: 0x040002D5 RID: 725
		private IScanFilter fileFilter_;

		// Token: 0x040002D6 RID: 726
		private IScanFilter directoryFilter_;

		// Token: 0x040002D7 RID: 727
		private bool alive_;
	}
}
