using System;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000058 RID: 88
	public class FastZipEvents
	{
		// Token: 0x060003BD RID: 957 RVA: 0x00015858 File Offset: 0x00014858
		public bool OnDirectoryFailure(string directory, Exception e)
		{
			bool result = false;
			DirectoryFailureHandler directoryFailure = this.DirectoryFailure;
			if (directoryFailure != null)
			{
				ScanFailureEventArgs scanFailureEventArgs = new ScanFailureEventArgs(directory, e);
				directoryFailure(this, scanFailureEventArgs);
				result = scanFailureEventArgs.ContinueRunning;
			}
			return result;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001588C File Offset: 0x0001488C
		public bool OnFileFailure(string file, Exception e)
		{
			FileFailureHandler fileFailure = this.FileFailure;
			bool flag = fileFailure != null;
			if (flag)
			{
				ScanFailureEventArgs scanFailureEventArgs = new ScanFailureEventArgs(file, e);
				fileFailure(this, scanFailureEventArgs);
				flag = scanFailureEventArgs.ContinueRunning;
			}
			return flag;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000158C4 File Offset: 0x000148C4
		public bool OnProcessFile(string file)
		{
			bool result = true;
			ProcessFileHandler processFile = this.ProcessFile;
			if (processFile != null)
			{
				ScanEventArgs scanEventArgs = new ScanEventArgs(file);
				processFile(this, scanEventArgs);
				result = scanEventArgs.ContinueRunning;
			}
			return result;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000158F4 File Offset: 0x000148F4
		public bool OnCompletedFile(string file)
		{
			bool result = true;
			CompletedFileHandler completedFile = this.CompletedFile;
			if (completedFile != null)
			{
				ScanEventArgs scanEventArgs = new ScanEventArgs(file);
				completedFile(this, scanEventArgs);
				result = scanEventArgs.ContinueRunning;
			}
			return result;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00015924 File Offset: 0x00014924
		public bool OnProcessDirectory(string directory, bool hasMatchingFiles)
		{
			bool result = true;
			ProcessDirectoryHandler processDirectory = this.ProcessDirectory;
			if (processDirectory != null)
			{
				DirectoryEventArgs directoryEventArgs = new DirectoryEventArgs(directory, hasMatchingFiles);
				processDirectory(this, directoryEventArgs);
				result = directoryEventArgs.ContinueRunning;
			}
			return result;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00015955 File Offset: 0x00014955
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0001595D File Offset: 0x0001495D
		public TimeSpan ProgressInterval
		{
			get
			{
				return this.progressInterval_;
			}
			set
			{
				this.progressInterval_ = value;
			}
		}

		// Token: 0x040002AA RID: 682
		public ProcessDirectoryHandler ProcessDirectory;

		// Token: 0x040002AB RID: 683
		public ProcessFileHandler ProcessFile;

		// Token: 0x040002AC RID: 684
		public ProgressHandler Progress;

		// Token: 0x040002AD RID: 685
		public CompletedFileHandler CompletedFile;

		// Token: 0x040002AE RID: 686
		public DirectoryFailureHandler DirectoryFailure;

		// Token: 0x040002AF RID: 687
		public FileFailureHandler FileFailure;

		// Token: 0x040002B0 RID: 688
		private TimeSpan progressInterval_ = TimeSpan.FromSeconds(3.0);
	}
}
