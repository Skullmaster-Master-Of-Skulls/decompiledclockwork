using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	// Token: 0x020005D0 RID: 1488
	[ComVisible(true)]
	[Serializable]
	public class StringWriter : TextWriter
	{
		// Token: 0x06003792 RID: 14226 RVA: 0x000BB6EF File Offset: 0x000BA6EF
		public StringWriter() : this(new StringBuilder(), CultureInfo.CurrentCulture)
		{
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x000BB701 File Offset: 0x000BA701
		public StringWriter(IFormatProvider formatProvider) : this(new StringBuilder(), formatProvider)
		{
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x000BB70F File Offset: 0x000BA70F
		public StringWriter(StringBuilder sb) : this(sb, CultureInfo.CurrentCulture)
		{
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x000BB71D File Offset: 0x000BA71D
		public StringWriter(StringBuilder sb, IFormatProvider formatProvider) : base(formatProvider)
		{
			if (sb == null)
			{
				throw new ArgumentNullException("sb", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			this._sb = sb;
			this._isOpen = true;
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x000BB74C File Offset: 0x000BA74C
		public override void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x000BB755 File Offset: 0x000BA755
		protected override void Dispose(bool disposing)
		{
			this._isOpen = false;
			base.Dispose(disposing);
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06003798 RID: 14232 RVA: 0x000BB765 File Offset: 0x000BA765
		public override Encoding Encoding
		{
			get
			{
				if (StringWriter.m_encoding == null)
				{
					StringWriter.m_encoding = new UnicodeEncoding(false, false);
				}
				return StringWriter.m_encoding;
			}
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x000BB77F File Offset: 0x000BA77F
		public virtual StringBuilder GetStringBuilder()
		{
			return this._sb;
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x000BB787 File Offset: 0x000BA787
		public override void Write(char value)
		{
			if (!this._isOpen)
			{
				__Error.WriterClosed();
			}
			this._sb.Append(value);
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x000BB7A4 File Offset: 0x000BA7A4
		public override void Write(char[] buffer, int index, int count)
		{
			if (!this._isOpen)
			{
				__Error.WriterClosed();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			this._sb.Append(buffer, index, count);
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x000BB82F File Offset: 0x000BA82F
		public override void Write(string value)
		{
			if (!this._isOpen)
			{
				__Error.WriterClosed();
			}
			if (value != null)
			{
				this._sb.Append(value);
			}
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x000BB84E File Offset: 0x000BA84E
		public override string ToString()
		{
			return this._sb.ToString();
		}

		// Token: 0x04001CDC RID: 7388
		private static UnicodeEncoding m_encoding;

		// Token: 0x04001CDD RID: 7389
		private StringBuilder _sb;

		// Token: 0x04001CDE RID: 7390
		private bool _isOpen;
	}
}
