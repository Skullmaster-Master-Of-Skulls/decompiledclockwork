using System;
using System.IO;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x020000F4 RID: 244
	public class QuietTextWriter : TextWriterAdapter
	{
		// Token: 0x060006D9 RID: 1753 RVA: 0x00015B8A File Offset: 0x00013D8A
		public QuietTextWriter(TextWriter writer, IErrorHandler errorHandler) : base(writer)
		{
			if (errorHandler == null)
			{
				throw new ArgumentNullException("errorHandler");
			}
			this.ErrorHandler = errorHandler;
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00015BA8 File Offset: 0x00013DA8
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x00015BB0 File Offset: 0x00013DB0
		public IErrorHandler ErrorHandler
		{
			get
			{
				return this.m_errorHandler;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_errorHandler = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00015BC7 File Offset: 0x00013DC7
		public bool Closed
		{
			get
			{
				return this.m_closed;
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00015BD0 File Offset: 0x00013DD0
		public override void Write(char value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception e)
			{
				this.m_errorHandler.Error("Failed to write [" + value + "].", e, ErrorCode.WriteFailure);
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00015C1C File Offset: 0x00013E1C
		public override void Write(char[] buffer, int index, int count)
		{
			try
			{
				base.Write(buffer, index, count);
			}
			catch (Exception e)
			{
				this.m_errorHandler.Error("Failed to write buffer.", e, ErrorCode.WriteFailure);
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00015C5C File Offset: 0x00013E5C
		public override void Write(string value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception e)
			{
				this.m_errorHandler.Error("Failed to write [" + value + "].", e, ErrorCode.WriteFailure);
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00015CA4 File Offset: 0x00013EA4
		public override void Close()
		{
			this.m_closed = true;
			base.Close();
		}

		// Token: 0x040002A3 RID: 675
		private IErrorHandler m_errorHandler;

		// Token: 0x040002A4 RID: 676
		private bool m_closed;
	}
}
