using System;
using System.IO;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net.Core;
using log4net.Layout;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02000022 RID: 34
	public class FileAppender : TextWriterAppender
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00004DF9 File Offset: 0x00002FF9
		public FileAppender()
		{
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004E20 File Offset: 0x00003020
		[Obsolete("Instead use the default constructor and set the Layout, File & AppendToFile properties")]
		public FileAppender(ILayout layout, string filename, bool append)
		{
			this.Layout = layout;
			this.File = filename;
			this.AppendToFile = append;
			this.ActivateOptions();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004E6C File Offset: 0x0000306C
		[Obsolete("Instead use the default constructor and set the Layout & File properties")]
		public FileAppender(ILayout layout, string filename) : this(layout, filename, true)
		{
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004E77 File Offset: 0x00003077
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00004E7F File Offset: 0x0000307F
		public virtual string File
		{
			get
			{
				return this.m_fileName;
			}
			set
			{
				this.m_fileName = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00004E88 File Offset: 0x00003088
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00004E90 File Offset: 0x00003090
		public bool AppendToFile
		{
			get
			{
				return this.m_appendToFile;
			}
			set
			{
				this.m_appendToFile = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00004E99 File Offset: 0x00003099
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00004EA1 File Offset: 0x000030A1
		public Encoding Encoding
		{
			get
			{
				return this.m_encoding;
			}
			set
			{
				this.m_encoding = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00004EAA File Offset: 0x000030AA
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00004EB2 File Offset: 0x000030B2
		public log4net.Core.SecurityContext SecurityContext
		{
			get
			{
				return this.m_securityContext;
			}
			set
			{
				this.m_securityContext = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00004EBB File Offset: 0x000030BB
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00004EC3 File Offset: 0x000030C3
		public FileAppender.LockingModelBase LockingModel
		{
			get
			{
				return this.m_lockingModel;
			}
			set
			{
				this.m_lockingModel = value;
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004ECC File Offset: 0x000030CC
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			if (this.m_securityContext == null)
			{
				this.m_securityContext = SecurityContextProvider.DefaultProvider.CreateSecurityContext(this);
			}
			if (this.m_lockingModel == null)
			{
				this.m_lockingModel = new FileAppender.ExclusiveLock();
			}
			this.m_lockingModel.CurrentAppender = this;
			this.m_lockingModel.ActivateOptions();
			if (this.m_fileName != null)
			{
				using (this.SecurityContext.Impersonate(this))
				{
					this.m_fileName = FileAppender.ConvertToFullPath(this.m_fileName.Trim());
				}
				this.SafeOpenFile(this.m_fileName, this.m_appendToFile);
				return;
			}
			LogLog.Warn(FileAppender.declaringType, "FileAppender: File option not set for appender [" + base.Name + "].");
			LogLog.Warn(FileAppender.declaringType, "FileAppender: Are you using FileAppender instead of ConsoleAppender?");
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004FAC File Offset: 0x000031AC
		protected override void Reset()
		{
			base.Reset();
			this.m_fileName = null;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004FBB File Offset: 0x000031BB
		protected override void OnClose()
		{
			base.OnClose();
			this.m_lockingModel.OnClose();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004FCE File Offset: 0x000031CE
		protected override void PrepareWriter()
		{
			this.SafeOpenFile(this.m_fileName, this.m_appendToFile);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004FE4 File Offset: 0x000031E4
		protected override void Append(LoggingEvent loggingEvent)
		{
			if (this.m_stream.AcquireLock())
			{
				try
				{
					base.Append(loggingEvent);
				}
				finally
				{
					this.m_stream.ReleaseLock();
				}
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00005024 File Offset: 0x00003224
		protected override void Append(LoggingEvent[] loggingEvents)
		{
			if (this.m_stream.AcquireLock())
			{
				try
				{
					base.Append(loggingEvents);
				}
				finally
				{
					this.m_stream.ReleaseLock();
				}
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005064 File Offset: 0x00003264
		protected override void WriteFooter()
		{
			if (this.m_stream != null)
			{
				this.m_stream.AcquireLock();
				try
				{
					base.WriteFooter();
				}
				finally
				{
					this.m_stream.ReleaseLock();
				}
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000050AC File Offset: 0x000032AC
		protected override void WriteHeader()
		{
			if (this.m_stream != null && this.m_stream.AcquireLock())
			{
				try
				{
					base.WriteHeader();
				}
				finally
				{
					this.m_stream.ReleaseLock();
				}
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000050F4 File Offset: 0x000032F4
		protected override void CloseWriter()
		{
			if (this.m_stream != null)
			{
				this.m_stream.AcquireLock();
				try
				{
					base.CloseWriter();
				}
				finally
				{
					this.m_stream.ReleaseLock();
				}
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000513C File Offset: 0x0000333C
		protected void CloseFile()
		{
			this.WriteFooterAndCloseWriter();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005144 File Offset: 0x00003344
		protected virtual void SafeOpenFile(string fileName, bool append)
		{
			try
			{
				this.OpenFile(fileName, append);
			}
			catch (Exception e)
			{
				this.ErrorHandler.Error(string.Concat(new object[]
				{
					"OpenFile(",
					fileName,
					",",
					append,
					") call failed."
				}), e, ErrorCode.FileOpenFailure);
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000051B0 File Offset: 0x000033B0
		protected virtual void OpenFile(string fileName, bool append)
		{
			if (LogLog.IsErrorEnabled)
			{
				bool flag = false;
				using (this.SecurityContext.Impersonate(this))
				{
					flag = Path.IsPathRooted(fileName);
				}
				if (!flag)
				{
					LogLog.Error(FileAppender.declaringType, "INTERNAL ERROR. OpenFile(" + fileName + "): File name is not fully qualified.");
				}
			}
			lock (this)
			{
				this.Reset();
				LogLog.Debug(FileAppender.declaringType, string.Concat(new object[]
				{
					"Opening file for writing [",
					fileName,
					"] append [",
					append,
					"]"
				}));
				this.m_fileName = fileName;
				this.m_appendToFile = append;
				this.LockingModel.CurrentAppender = this;
				this.LockingModel.OpenFile(fileName, append, this.m_encoding);
				this.m_stream = new FileAppender.LockingStream(this.LockingModel);
				if (this.m_stream != null)
				{
					this.m_stream.AcquireLock();
					try
					{
						this.SetQWForFiles(this.m_stream);
					}
					finally
					{
						this.m_stream.ReleaseLock();
					}
				}
				this.WriteHeader();
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005300 File Offset: 0x00003500
		protected virtual void SetQWForFiles(Stream fileStream)
		{
			this.SetQWForFiles(new StreamWriter(fileStream, this.m_encoding));
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005314 File Offset: 0x00003514
		protected virtual void SetQWForFiles(TextWriter writer)
		{
			base.QuietWriter = new QuietTextWriter(writer, this.ErrorHandler);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005328 File Offset: 0x00003528
		protected static string ConvertToFullPath(string path)
		{
			return SystemInfo.ConvertToFullPath(path);
		}

		// Token: 0x04000084 RID: 132
		private bool m_appendToFile = true;

		// Token: 0x04000085 RID: 133
		private string m_fileName;

		// Token: 0x04000086 RID: 134
		private Encoding m_encoding = Encoding.GetEncoding(0);

		// Token: 0x04000087 RID: 135
		private log4net.Core.SecurityContext m_securityContext;

		// Token: 0x04000088 RID: 136
		private FileAppender.LockingStream m_stream;

		// Token: 0x04000089 RID: 137
		private FileAppender.LockingModelBase m_lockingModel = new FileAppender.ExclusiveLock();

		// Token: 0x0400008A RID: 138
		private static readonly Type declaringType = typeof(FileAppender);

		// Token: 0x02000023 RID: 35
		private sealed class LockingStream : Stream, IDisposable
		{
			// Token: 0x06000154 RID: 340 RVA: 0x00005341 File Offset: 0x00003541
			public LockingStream(FileAppender.LockingModelBase locking)
			{
				if (locking == null)
				{
					throw new ArgumentException("Locking model may not be null", "locking");
				}
				this.m_lockingModel = locking;
			}

			// Token: 0x06000155 RID: 341 RVA: 0x0000536C File Offset: 0x0000356C
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				this.AssertLocked();
				IAsyncResult asyncResult = this.m_realStream.BeginRead(buffer, offset, count, callback, state);
				this.m_readTotal = this.EndRead(asyncResult);
				return asyncResult;
			}

			// Token: 0x06000156 RID: 342 RVA: 0x000053A0 File Offset: 0x000035A0
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				this.AssertLocked();
				IAsyncResult asyncResult = this.m_realStream.BeginWrite(buffer, offset, count, callback, state);
				this.EndWrite(asyncResult);
				return asyncResult;
			}

			// Token: 0x06000157 RID: 343 RVA: 0x000053CE File Offset: 0x000035CE
			public override void Close()
			{
				this.m_lockingModel.CloseFile();
			}

			// Token: 0x06000158 RID: 344 RVA: 0x000053DB File Offset: 0x000035DB
			public override int EndRead(IAsyncResult asyncResult)
			{
				this.AssertLocked();
				return this.m_readTotal;
			}

			// Token: 0x06000159 RID: 345 RVA: 0x000053E9 File Offset: 0x000035E9
			public override void EndWrite(IAsyncResult asyncResult)
			{
			}

			// Token: 0x0600015A RID: 346 RVA: 0x000053EB File Offset: 0x000035EB
			public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				this.AssertLocked();
				return this.m_realStream.ReadAsync(buffer, offset, count, cancellationToken);
			}

			// Token: 0x0600015B RID: 347 RVA: 0x00005403 File Offset: 0x00003603
			public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				this.AssertLocked();
				return base.WriteAsync(buffer, offset, count, cancellationToken);
			}

			// Token: 0x0600015C RID: 348 RVA: 0x00005416 File Offset: 0x00003616
			public override void Flush()
			{
				this.AssertLocked();
				this.m_realStream.Flush();
			}

			// Token: 0x0600015D RID: 349 RVA: 0x00005429 File Offset: 0x00003629
			public override int Read(byte[] buffer, int offset, int count)
			{
				return this.m_realStream.Read(buffer, offset, count);
			}

			// Token: 0x0600015E RID: 350 RVA: 0x00005439 File Offset: 0x00003639
			public override int ReadByte()
			{
				return this.m_realStream.ReadByte();
			}

			// Token: 0x0600015F RID: 351 RVA: 0x00005446 File Offset: 0x00003646
			public override long Seek(long offset, SeekOrigin origin)
			{
				this.AssertLocked();
				return this.m_realStream.Seek(offset, origin);
			}

			// Token: 0x06000160 RID: 352 RVA: 0x0000545B File Offset: 0x0000365B
			public override void SetLength(long value)
			{
				this.AssertLocked();
				this.m_realStream.SetLength(value);
			}

			// Token: 0x06000161 RID: 353 RVA: 0x0000546F File Offset: 0x0000366F
			void IDisposable.Dispose()
			{
				this.Close();
			}

			// Token: 0x06000162 RID: 354 RVA: 0x00005477 File Offset: 0x00003677
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.AssertLocked();
				this.m_realStream.Write(buffer, offset, count);
			}

			// Token: 0x06000163 RID: 355 RVA: 0x0000548D File Offset: 0x0000368D
			public override void WriteByte(byte value)
			{
				this.AssertLocked();
				this.m_realStream.WriteByte(value);
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x06000164 RID: 356 RVA: 0x000054A1 File Offset: 0x000036A1
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x06000165 RID: 357 RVA: 0x000054A4 File Offset: 0x000036A4
			public override bool CanSeek
			{
				get
				{
					this.AssertLocked();
					return this.m_realStream.CanSeek;
				}
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000166 RID: 358 RVA: 0x000054B7 File Offset: 0x000036B7
			public override bool CanWrite
			{
				get
				{
					this.AssertLocked();
					return this.m_realStream.CanWrite;
				}
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x06000167 RID: 359 RVA: 0x000054CA File Offset: 0x000036CA
			public override long Length
			{
				get
				{
					this.AssertLocked();
					return this.m_realStream.Length;
				}
			}

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x06000168 RID: 360 RVA: 0x000054DD File Offset: 0x000036DD
			// (set) Token: 0x06000169 RID: 361 RVA: 0x000054F0 File Offset: 0x000036F0
			public override long Position
			{
				get
				{
					this.AssertLocked();
					return this.m_realStream.Position;
				}
				set
				{
					this.AssertLocked();
					this.m_realStream.Position = value;
				}
			}

			// Token: 0x0600016A RID: 362 RVA: 0x00005504 File Offset: 0x00003704
			private void AssertLocked()
			{
				if (this.m_realStream == null)
				{
					throw new FileAppender.LockingStream.LockStateException("The file is not currently locked");
				}
			}

			// Token: 0x0600016B RID: 363 RVA: 0x0000551C File Offset: 0x0000371C
			public bool AcquireLock()
			{
				bool result = false;
				lock (this)
				{
					if (this.m_lockLevel == 0)
					{
						this.m_realStream = this.m_lockingModel.AcquireLock();
					}
					if (this.m_realStream != null)
					{
						this.m_lockLevel++;
						result = true;
					}
				}
				return result;
			}

			// Token: 0x0600016C RID: 364 RVA: 0x00005588 File Offset: 0x00003788
			public void ReleaseLock()
			{
				lock (this)
				{
					this.m_lockLevel--;
					if (this.m_lockLevel == 0)
					{
						this.m_lockingModel.ReleaseLock();
						this.m_realStream = null;
					}
				}
			}

			// Token: 0x0400008B RID: 139
			private Stream m_realStream;

			// Token: 0x0400008C RID: 140
			private FileAppender.LockingModelBase m_lockingModel;

			// Token: 0x0400008D RID: 141
			private int m_lockLevel;

			// Token: 0x0400008E RID: 142
			private int m_readTotal = -1;

			// Token: 0x02000025 RID: 37
			public sealed class LockStateException : LogException
			{
				// Token: 0x06000171 RID: 369 RVA: 0x0000560D File Offset: 0x0000380D
				public LockStateException(string message) : base(message)
				{
				}
			}
		}

		// Token: 0x02000026 RID: 38
		public abstract class LockingModelBase
		{
			// Token: 0x06000172 RID: 370
			public abstract void OpenFile(string filename, bool append, Encoding encoding);

			// Token: 0x06000173 RID: 371
			public abstract void CloseFile();

			// Token: 0x06000174 RID: 372
			public abstract void ActivateOptions();

			// Token: 0x06000175 RID: 373
			public abstract void OnClose();

			// Token: 0x06000176 RID: 374
			public abstract Stream AcquireLock();

			// Token: 0x06000177 RID: 375
			public abstract void ReleaseLock();

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x06000178 RID: 376 RVA: 0x00005616 File Offset: 0x00003816
			// (set) Token: 0x06000179 RID: 377 RVA: 0x0000561E File Offset: 0x0000381E
			public FileAppender CurrentAppender
			{
				get
				{
					return this.m_appender;
				}
				set
				{
					this.m_appender = value;
				}
			}

			// Token: 0x0600017A RID: 378 RVA: 0x00005628 File Offset: 0x00003828
			protected Stream CreateStream(string filename, bool append, FileShare fileShare)
			{
				Stream result;
				using (this.CurrentAppender.SecurityContext.Impersonate(this))
				{
					string directoryName = Path.GetDirectoryName(filename);
					if (!Directory.Exists(directoryName))
					{
						Directory.CreateDirectory(directoryName);
					}
					FileMode mode = append ? FileMode.Append : FileMode.Create;
					result = new FileStream(filename, mode, FileAccess.Write, fileShare);
				}
				return result;
			}

			// Token: 0x0600017B RID: 379 RVA: 0x0000568C File Offset: 0x0000388C
			protected void CloseStream(Stream stream)
			{
				using (this.CurrentAppender.SecurityContext.Impersonate(this))
				{
					stream.Close();
				}
			}

			// Token: 0x0400008F RID: 143
			private FileAppender m_appender;
		}

		// Token: 0x02000027 RID: 39
		public class ExclusiveLock : FileAppender.LockingModelBase
		{
			// Token: 0x0600017D RID: 381 RVA: 0x000056D8 File Offset: 0x000038D8
			public override void OpenFile(string filename, bool append, Encoding encoding)
			{
				try
				{
					this.m_stream = base.CreateStream(filename, append, FileShare.Read);
				}
				catch (Exception ex)
				{
					base.CurrentAppender.ErrorHandler.Error("Unable to acquire lock on file " + filename + ". " + ex.Message);
				}
			}

			// Token: 0x0600017E RID: 382 RVA: 0x00005730 File Offset: 0x00003930
			public override void CloseFile()
			{
				base.CloseStream(this.m_stream);
				this.m_stream = null;
			}

			// Token: 0x0600017F RID: 383 RVA: 0x00005745 File Offset: 0x00003945
			public override Stream AcquireLock()
			{
				return this.m_stream;
			}

			// Token: 0x06000180 RID: 384 RVA: 0x0000574D File Offset: 0x0000394D
			public override void ReleaseLock()
			{
			}

			// Token: 0x06000181 RID: 385 RVA: 0x0000574F File Offset: 0x0000394F
			public override void ActivateOptions()
			{
			}

			// Token: 0x06000182 RID: 386 RVA: 0x00005751 File Offset: 0x00003951
			public override void OnClose()
			{
			}

			// Token: 0x04000090 RID: 144
			private Stream m_stream;
		}

		// Token: 0x02000028 RID: 40
		public class MinimalLock : FileAppender.LockingModelBase
		{
			// Token: 0x06000184 RID: 388 RVA: 0x0000575B File Offset: 0x0000395B
			public override void OpenFile(string filename, bool append, Encoding encoding)
			{
				this.m_filename = filename;
				this.m_append = append;
			}

			// Token: 0x06000185 RID: 389 RVA: 0x0000576B File Offset: 0x0000396B
			public override void CloseFile()
			{
			}

			// Token: 0x06000186 RID: 390 RVA: 0x00005770 File Offset: 0x00003970
			public override Stream AcquireLock()
			{
				if (this.m_stream == null)
				{
					try
					{
						this.m_stream = base.CreateStream(this.m_filename, this.m_append, FileShare.Read);
						this.m_append = true;
					}
					catch (Exception ex)
					{
						base.CurrentAppender.ErrorHandler.Error("Unable to acquire lock on file " + this.m_filename + ". " + ex.Message);
					}
				}
				return this.m_stream;
			}

			// Token: 0x06000187 RID: 391 RVA: 0x000057EC File Offset: 0x000039EC
			public override void ReleaseLock()
			{
				base.CloseStream(this.m_stream);
				this.m_stream = null;
			}

			// Token: 0x06000188 RID: 392 RVA: 0x00005801 File Offset: 0x00003A01
			public override void ActivateOptions()
			{
			}

			// Token: 0x06000189 RID: 393 RVA: 0x00005803 File Offset: 0x00003A03
			public override void OnClose()
			{
			}

			// Token: 0x04000091 RID: 145
			private string m_filename;

			// Token: 0x04000092 RID: 146
			private bool m_append;

			// Token: 0x04000093 RID: 147
			private Stream m_stream;
		}

		// Token: 0x02000029 RID: 41
		public class InterProcessLock : FileAppender.LockingModelBase
		{
			// Token: 0x0600018B RID: 395 RVA: 0x00005810 File Offset: 0x00003A10
			[SecuritySafeCritical]
			public override void OpenFile(string filename, bool append, Encoding encoding)
			{
				try
				{
					this.m_stream = base.CreateStream(filename, append, FileShare.ReadWrite);
				}
				catch (Exception ex)
				{
					base.CurrentAppender.ErrorHandler.Error("Unable to acquire lock on file " + filename + ". " + ex.Message);
				}
			}

			// Token: 0x0600018C RID: 396 RVA: 0x00005868 File Offset: 0x00003A68
			public override void CloseFile()
			{
				try
				{
					base.CloseStream(this.m_stream);
					this.m_stream = null;
				}
				finally
				{
					this.ReleaseLock();
				}
			}

			// Token: 0x0600018D RID: 397 RVA: 0x000058A4 File Offset: 0x00003AA4
			public override Stream AcquireLock()
			{
				if (this.m_mutex != null)
				{
					this.m_mutex.WaitOne();
					this.m_recursiveWatch++;
					if (this.m_stream != null && this.m_stream.CanSeek)
					{
						this.m_stream.Seek(0L, SeekOrigin.End);
					}
				}
				else
				{
					base.CurrentAppender.ErrorHandler.Error("Programming error, no mutex available to acquire lock! From here on things will be dangerous!");
				}
				return this.m_stream;
			}

			// Token: 0x0600018E RID: 398 RVA: 0x00005914 File Offset: 0x00003B14
			public override void ReleaseLock()
			{
				if (this.m_mutex != null)
				{
					if (this.m_recursiveWatch > 0)
					{
						this.m_recursiveWatch--;
						this.m_mutex.ReleaseMutex();
						return;
					}
				}
				else
				{
					base.CurrentAppender.ErrorHandler.Error("Programming error, no mutex available to release the lock!");
				}
			}

			// Token: 0x0600018F RID: 399 RVA: 0x00005964 File Offset: 0x00003B64
			public override void ActivateOptions()
			{
				if (this.m_mutex == null)
				{
					string name = base.CurrentAppender.File.Replace("\\", "_").Replace(":", "_").Replace("/", "_");
					this.m_mutex = new Mutex(false, name);
					return;
				}
				base.CurrentAppender.ErrorHandler.Error("Programming error, mutex already initialized!");
			}

			// Token: 0x06000190 RID: 400 RVA: 0x000059D5 File Offset: 0x00003BD5
			public override void OnClose()
			{
				if (this.m_mutex != null)
				{
					this.m_mutex.Dispose();
					this.m_mutex = null;
					return;
				}
				base.CurrentAppender.ErrorHandler.Error("Programming error, mutex not initialized!");
			}

			// Token: 0x04000094 RID: 148
			private Mutex m_mutex;

			// Token: 0x04000095 RID: 149
			private Stream m_stream;

			// Token: 0x04000096 RID: 150
			private int m_recursiveWatch;
		}
	}
}
