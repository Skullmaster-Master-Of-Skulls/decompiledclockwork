using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200001A RID: 26
	public class TestStatus
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00005DF4 File Offset: 0x00004DF4
		public TestStatus(ZipFile file)
		{
			this.file_ = file;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005E03 File Offset: 0x00004E03
		public TestOperation Operation
		{
			get
			{
				return this.operation_;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00005E0B File Offset: 0x00004E0B
		public ZipFile File
		{
			get
			{
				return this.file_;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00005E13 File Offset: 0x00004E13
		public ZipEntry Entry
		{
			get
			{
				return this.entry_;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00005E1B File Offset: 0x00004E1B
		public int ErrorCount
		{
			get
			{
				return this.errorCount_;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005E23 File Offset: 0x00004E23
		public long BytesTested
		{
			get
			{
				return this.bytesTested_;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00005E2B File Offset: 0x00004E2B
		public bool EntryValid
		{
			get
			{
				return this.entryValid_;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005E33 File Offset: 0x00004E33
		internal void AddError()
		{
			this.errorCount_++;
			this.entryValid_ = false;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005E4A File Offset: 0x00004E4A
		internal void SetOperation(TestOperation operation)
		{
			this.operation_ = operation;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005E53 File Offset: 0x00004E53
		internal void SetEntry(ZipEntry entry)
		{
			this.entry_ = entry;
			this.entryValid_ = true;
			this.bytesTested_ = 0L;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005E6B File Offset: 0x00004E6B
		internal void SetBytesTested(long value)
		{
			this.bytesTested_ = value;
		}

		// Token: 0x040000CB RID: 203
		private ZipFile file_;

		// Token: 0x040000CC RID: 204
		private ZipEntry entry_;

		// Token: 0x040000CD RID: 205
		private bool entryValid_;

		// Token: 0x040000CE RID: 206
		private int errorCount_;

		// Token: 0x040000CF RID: 207
		private long bytesTested_;

		// Token: 0x040000D0 RID: 208
		private TestOperation operation_;
	}
}
