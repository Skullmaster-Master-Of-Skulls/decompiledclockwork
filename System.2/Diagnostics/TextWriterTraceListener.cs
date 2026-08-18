using System;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace System.Diagnostics
{
	// Token: 0x020004AC RID: 1196
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class TextWriterTraceListener : TraceListener
	{
		// Token: 0x06002C4D RID: 11341 RVA: 0x000C7BF4 File Offset: 0x000C5DF4
		public TextWriterTraceListener()
		{
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x000C7BFC File Offset: 0x000C5DFC
		public TextWriterTraceListener(Stream stream) : this(stream, string.Empty)
		{
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x000C7C0A File Offset: 0x000C5E0A
		public TextWriterTraceListener(Stream stream, string name) : base(name)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.writer = new StreamWriter(stream);
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x000C7C2D File Offset: 0x000C5E2D
		public TextWriterTraceListener(TextWriter writer) : this(writer, string.Empty)
		{
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x000C7C3B File Offset: 0x000C5E3B
		public TextWriterTraceListener(TextWriter writer, string name) : base(name)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x000C7C59 File Offset: 0x000C5E59
		public TextWriterTraceListener(string fileName)
		{
			this.fileName = fileName;
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x000C7C68 File Offset: 0x000C5E68
		public TextWriterTraceListener(string fileName, string name) : base(name)
		{
			this.fileName = fileName;
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06002C54 RID: 11348 RVA: 0x000C7C78 File Offset: 0x000C5E78
		// (set) Token: 0x06002C55 RID: 11349 RVA: 0x000C7C87 File Offset: 0x000C5E87
		public TextWriter Writer
		{
			get
			{
				this.EnsureWriter();
				return this.writer;
			}
			set
			{
				this.writer = value;
			}
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x000C7C90 File Offset: 0x000C5E90
		public override void Close()
		{
			if (this.writer != null)
			{
				try
				{
					this.writer.Close();
				}
				catch (ObjectDisposedException)
				{
				}
			}
			this.writer = null;
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x000C7CCC File Offset: 0x000C5ECC
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.Close();
				}
				else
				{
					if (this.writer != null)
					{
						try
						{
							this.writer.Close();
						}
						catch (ObjectDisposedException)
						{
						}
					}
					this.writer = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x000C7D2C File Offset: 0x000C5F2C
		public override void Flush()
		{
			if (!this.EnsureWriter())
			{
				return;
			}
			try
			{
				this.writer.Flush();
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x000C7D64 File Offset: 0x000C5F64
		public override void Write(string message)
		{
			if (!this.EnsureWriter())
			{
				return;
			}
			if (base.NeedIndent)
			{
				this.WriteIndent();
			}
			try
			{
				this.writer.Write(message);
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x000C7DAC File Offset: 0x000C5FAC
		public override void WriteLine(string message)
		{
			if (!this.EnsureWriter())
			{
				return;
			}
			if (base.NeedIndent)
			{
				this.WriteIndent();
			}
			try
			{
				this.writer.WriteLine(message);
				base.NeedIndent = true;
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x000C7DFC File Offset: 0x000C5FFC
		private static Encoding GetEncodingWithFallback(Encoding encoding)
		{
			Encoding encoding2 = (Encoding)encoding.Clone();
			encoding2.EncoderFallback = EncoderFallback.ReplacementFallback;
			encoding2.DecoderFallback = DecoderFallback.ReplacementFallback;
			return encoding2;
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x000C7E2C File Offset: 0x000C602C
		internal bool EnsureWriter()
		{
			bool flag = true;
			if (this.writer == null)
			{
				flag = false;
				if (this.fileName == null)
				{
					return flag;
				}
				Encoding encodingWithFallback = TextWriterTraceListener.GetEncodingWithFallback(new UTF8Encoding(false));
				string path = Path.GetFullPath(this.fileName);
				string directoryName = Path.GetDirectoryName(path);
				string text = Path.GetFileName(path);
				for (int i = 0; i < 2; i++)
				{
					try
					{
						this.writer = new StreamWriter(path, true, encodingWithFallback, 4096);
						flag = true;
						break;
					}
					catch (IOException)
					{
						text = Guid.NewGuid().ToString() + text;
						path = Path.Combine(directoryName, text);
					}
					catch (UnauthorizedAccessException)
					{
						break;
					}
					catch (Exception)
					{
						break;
					}
				}
				if (!flag)
				{
					this.fileName = null;
				}
			}
			return flag;
		}

		// Token: 0x040026CE RID: 9934
		internal TextWriter writer;

		// Token: 0x040026CF RID: 9935
		private string fileName;
	}
}
