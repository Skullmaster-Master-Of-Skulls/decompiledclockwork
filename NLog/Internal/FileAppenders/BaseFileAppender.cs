using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using Microsoft.Win32.SafeHandles;
using NLog.Common;

namespace NLog.Internal.FileAppenders
{
	// Token: 0x02000083 RID: 131
	[SecuritySafeCritical]
	internal abstract class BaseFileAppender : IDisposable
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x000097FF File Offset: 0x000079FF
		public BaseFileAppender(string fileName, ICreateFileParameters createParameters)
		{
			this.CreateFileParameters = createParameters;
			this.FileName = fileName;
			this.OpenTime = DateTime.UtcNow;
			this.LastWriteTime = DateTime.MinValue;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00009836 File Offset: 0x00007A36
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x0000983E File Offset: 0x00007A3E
		public string FileName { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00009847 File Offset: 0x00007A47
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x0000984F File Offset: 0x00007A4F
		public DateTime CreationTime { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00009858 File Offset: 0x00007A58
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x00009860 File Offset: 0x00007A60
		public DateTime OpenTime { get; private set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00009869 File Offset: 0x00007A69
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x00009871 File Offset: 0x00007A71
		public DateTime LastWriteTime { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0000987A File Offset: 0x00007A7A
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x00009882 File Offset: 0x00007A82
		public ICreateFileParameters CreateFileParameters { get; private set; }

		// Token: 0x06000458 RID: 1112
		public abstract void Write(byte[] bytes);

		// Token: 0x06000459 RID: 1113
		public abstract void Flush();

		// Token: 0x0600045A RID: 1114
		public abstract void Close();

		// Token: 0x0600045B RID: 1115
		public abstract FileCharacteristics GetFileCharacteristics();

		// Token: 0x0600045C RID: 1116 RVA: 0x0000988B File Offset: 0x00007A8B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000989A File Offset: 0x00007A9A
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000098A5 File Offset: 0x00007AA5
		protected void FileTouched()
		{
			this.FileTouched(DateTime.UtcNow);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000098B2 File Offset: 0x00007AB2
		protected void FileTouched(DateTime dateTime)
		{
			this.LastWriteTime = dateTime;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000098BC File Offset: 0x00007ABC
		protected FileStream CreateFileStream(bool allowFileSharedWriting)
		{
			int num = this.CreateFileParameters.ConcurrentWriteAttemptDelay;
			InternalLogger.Trace("Opening {0} with allowFileSharedWriting={1}", new object[]
			{
				this.FileName,
				allowFileSharedWriting
			});
			for (int i = 0; i < this.CreateFileParameters.ConcurrentWriteAttempts; i++)
			{
				try
				{
					try
					{
						return this.TryCreateFileStream(allowFileSharedWriting);
					}
					catch (DirectoryNotFoundException)
					{
						if (!this.CreateFileParameters.CreateDirs)
						{
							throw;
						}
						Directory.CreateDirectory(Path.GetDirectoryName(this.FileName));
						return this.TryCreateFileStream(allowFileSharedWriting);
					}
				}
				catch (IOException)
				{
					if (!this.CreateFileParameters.ConcurrentWrites || i + 1 == this.CreateFileParameters.ConcurrentWriteAttempts)
					{
						throw;
					}
					int num2 = this.random.Next(num);
					InternalLogger.Warn("Attempt #{0} to open {1} failed. Sleeping for {2}ms", new object[]
					{
						i,
						this.FileName,
						num2
					});
					num *= 2;
					Thread.Sleep(num2);
				}
			}
			throw new InvalidOperationException("Should not be reached.");
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000099E8 File Offset: 0x00007BE8
		private FileStream WindowsCreateFile(string fileName, bool allowFileSharedWriting)
		{
			int num = 1;
			if (allowFileSharedWriting)
			{
				num |= 2;
			}
			if (this.CreateFileParameters.EnableFileDelete && PlatformDetector.CurrentOS != RuntimeOS.Windows)
			{
				num |= 4;
			}
			SafeFileHandle safeFileHandle = null;
			FileStream fileStream = null;
			FileStream result;
			try
			{
				safeFileHandle = Win32FileNativeMethods.CreateFile(fileName, Win32FileNativeMethods.FileAccess.GenericWrite, num, IntPtr.Zero, Win32FileNativeMethods.CreationDisposition.OpenAlways, this.CreateFileParameters.FileAttributes, IntPtr.Zero);
				if (safeFileHandle.IsInvalid)
				{
					Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
				}
				fileStream = new FileStream(safeFileHandle, FileAccess.Write, this.CreateFileParameters.BufferSize);
				fileStream.Seek(0L, SeekOrigin.End);
				result = fileStream;
			}
			catch
			{
				if (fileStream != null)
				{
					fileStream.Dispose();
				}
				if (safeFileHandle != null && !safeFileHandle.IsClosed)
				{
					safeFileHandle.Close();
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00009AA0 File Offset: 0x00007CA0
		private FileStream TryCreateFileStream(bool allowFileSharedWriting)
		{
			this.UpdateCreationTime();
			try
			{
				if (!this.CreateFileParameters.ForceManaged && PlatformDetector.IsDesktopWin32)
				{
					return this.WindowsCreateFile(this.FileName, allowFileSharedWriting);
				}
			}
			catch (SecurityException)
			{
				InternalLogger.Debug("Could not use native Windows create file, falling back to managed filestream");
			}
			FileShare fileShare = allowFileSharedWriting ? FileShare.ReadWrite : FileShare.Read;
			if (this.CreateFileParameters.EnableFileDelete && PlatformDetector.CurrentOS != RuntimeOS.Windows)
			{
				fileShare |= FileShare.Delete;
			}
			return new FileStream(this.FileName, FileMode.Append, FileAccess.Write, fileShare, this.CreateFileParameters.BufferSize);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00009B34 File Offset: 0x00007D34
		private void UpdateCreationTime()
		{
			if (File.Exists(this.FileName))
			{
				this.CreationTime = File.GetCreationTimeUtc(this.FileName);
				return;
			}
			File.Create(this.FileName).Dispose();
			this.CreationTime = DateTime.UtcNow;
			File.SetCreationTimeUtc(this.FileName, this.CreationTime);
		}

		// Token: 0x040000DF RID: 223
		private readonly Random random = new Random();
	}
}
