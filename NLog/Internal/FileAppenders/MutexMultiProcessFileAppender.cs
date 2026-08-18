using System;
using System.IO;
using System.Security;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using NLog.Common;

namespace NLog.Internal.FileAppenders
{
	// Token: 0x02000089 RID: 137
	[SecuritySafeCritical]
	internal class MutexMultiProcessFileAppender : BaseFileAppender
	{
		// Token: 0x0600048A RID: 1162 RVA: 0x0000A0D8 File Offset: 0x000082D8
		public MutexMultiProcessFileAppender(string fileName, ICreateFileParameters parameters) : base(fileName, parameters)
		{
			try
			{
				this.mutex = MutexMultiProcessFileAppender.CreateSharableMutex(MutexMultiProcessFileAppender.GetMutexName(fileName));
				this.fileStream = base.CreateFileStream(true);
			}
			catch
			{
				if (this.mutex != null)
				{
					this.mutex.Close();
					this.mutex = null;
				}
				if (this.fileStream != null)
				{
					this.fileStream.Close();
					this.fileStream = null;
				}
				throw;
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000A154 File Offset: 0x00008354
		public override void Write(byte[] bytes)
		{
			if (this.mutex == null)
			{
				return;
			}
			try
			{
				this.mutex.WaitOne();
			}
			catch (AbandonedMutexException)
			{
			}
			try
			{
				this.fileStream.Seek(0L, SeekOrigin.End);
				this.fileStream.Write(bytes, 0, bytes.Length);
				this.fileStream.Flush();
				base.FileTouched();
			}
			finally
			{
				this.mutex.ReleaseMutex();
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000A1D8 File Offset: 0x000083D8
		public override void Close()
		{
			InternalLogger.Trace("Closing '{0}'", new object[]
			{
				base.FileName
			});
			if (this.mutex != null)
			{
				this.mutex.Close();
			}
			if (this.fileStream != null)
			{
				this.fileStream.Close();
			}
			this.mutex = null;
			this.fileStream = null;
			base.FileTouched();
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000A23A File Offset: 0x0000843A
		public override void Flush()
		{
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000A23C File Offset: 0x0000843C
		public override FileCharacteristics GetFileCharacteristics()
		{
			return FileCharacteristicsHelper.Helper.GetFileCharacteristics(base.FileName, this.fileStream.SafeFileHandle.DangerousGetHandle());
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000A260 File Offset: 0x00008460
		private static Mutex CreateSharableMutex(string name)
		{
			MutexSecurity mutexSecurity = new MutexSecurity();
			SecurityIdentifier identity = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
			mutexSecurity.AddAccessRule(new MutexAccessRule(identity, MutexRights.FullControl, AccessControlType.Allow));
			bool flag;
			return new Mutex(false, name, ref flag, mutexSecurity);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000A298 File Offset: 0x00008498
		private static string GetMutexName(string fileName)
		{
			string text = Path.GetFullPath(fileName).ToLowerInvariant();
			text = text.Replace('\\', '/');
			if ("Global\\NLog-FileLock-".Length + text.Length <= 260)
			{
				return "Global\\NLog-FileLock-" + text;
			}
			string text2;
			using (MD5 md = MD5.Create())
			{
				byte[] inArray = md.ComputeHash(Encoding.UTF8.GetBytes(text));
				text2 = Convert.ToBase64String(inArray);
			}
			int startIndex = text.Length - (260 - "Global\\NLog-FileLock-".Length - text2.Length);
			return "Global\\NLog-FileLock-" + text2 + text.Substring(startIndex);
		}

		// Token: 0x040000F0 RID: 240
		public static readonly IFileAppenderFactory TheFactory = new MutexMultiProcessFileAppender.Factory();

		// Token: 0x040000F1 RID: 241
		private FileStream fileStream;

		// Token: 0x040000F2 RID: 242
		private Mutex mutex;

		// Token: 0x0200008A RID: 138
		private class Factory : IFileAppenderFactory
		{
			// Token: 0x06000492 RID: 1170 RVA: 0x0000A35C File Offset: 0x0000855C
			BaseFileAppender IFileAppenderFactory.Open(string fileName, ICreateFileParameters parameters)
			{
				return new MutexMultiProcessFileAppender(fileName, parameters);
			}
		}
	}
}
