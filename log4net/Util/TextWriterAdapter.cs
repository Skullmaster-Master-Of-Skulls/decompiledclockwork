using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace log4net.Util
{
	// Token: 0x020000F3 RID: 243
	public abstract class TextWriterAdapter : TextWriter
	{
		// Token: 0x060006CC RID: 1740 RVA: 0x00015ADA File Offset: 0x00013CDA
		protected TextWriterAdapter(TextWriter writer) : base(CultureInfo.InvariantCulture)
		{
			this.m_writer = writer;
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x00015AEE File Offset: 0x00013CEE
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x00015AF6 File Offset: 0x00013CF6
		protected TextWriter Writer
		{
			get
			{
				return this.m_writer;
			}
			set
			{
				this.m_writer = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00015AFF File Offset: 0x00013CFF
		public override Encoding Encoding
		{
			get
			{
				return this.m_writer.Encoding;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00015B0C File Offset: 0x00013D0C
		public override IFormatProvider FormatProvider
		{
			get
			{
				return this.m_writer.FormatProvider;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x00015B19 File Offset: 0x00013D19
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x00015B26 File Offset: 0x00013D26
		public override string NewLine
		{
			get
			{
				return this.m_writer.NewLine;
			}
			set
			{
				this.m_writer.NewLine = value;
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00015B34 File Offset: 0x00013D34
		public override void Close()
		{
			this.m_writer.Close();
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00015B41 File Offset: 0x00013D41
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				((IDisposable)this.m_writer).Dispose();
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00015B51 File Offset: 0x00013D51
		public override void Flush()
		{
			this.m_writer.Flush();
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00015B5E File Offset: 0x00013D5E
		public override void Write(char value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00015B6C File Offset: 0x00013D6C
		public override void Write(char[] buffer, int index, int count)
		{
			this.m_writer.Write(buffer, index, count);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00015B7C File Offset: 0x00013D7C
		public override void Write(string value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x040002A2 RID: 674
		private TextWriter m_writer;
	}
}
