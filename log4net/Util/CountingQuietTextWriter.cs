using System;
using System.IO;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x020000F5 RID: 245
	public class CountingQuietTextWriter : QuietTextWriter
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x00015CB3 File Offset: 0x00013EB3
		public CountingQuietTextWriter(TextWriter writer, IErrorHandler errorHandler) : base(writer, errorHandler)
		{
			this.m_countBytes = 0L;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00015CC8 File Offset: 0x00013EC8
		public override void Write(char value)
		{
			try
			{
				base.Write(value);
				this.m_countBytes += (long)this.Encoding.GetByteCount(new char[]
				{
					value
				});
			}
			catch (Exception e)
			{
				base.ErrorHandler.Error("Failed to write [" + value + "].", e, ErrorCode.WriteFailure);
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00015D38 File Offset: 0x00013F38
		public override void Write(char[] buffer, int index, int count)
		{
			if (count > 0)
			{
				try
				{
					base.Write(buffer, index, count);
					this.m_countBytes += (long)this.Encoding.GetByteCount(buffer, index, count);
				}
				catch (Exception e)
				{
					base.ErrorHandler.Error("Failed to write buffer.", e, ErrorCode.WriteFailure);
				}
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00015D98 File Offset: 0x00013F98
		public override void Write(string str)
		{
			if (str != null && str.Length > 0)
			{
				try
				{
					base.Write(str);
					this.m_countBytes += (long)this.Encoding.GetByteCount(str);
				}
				catch (Exception e)
				{
					base.ErrorHandler.Error("Failed to write [" + str + "].", e, ErrorCode.WriteFailure);
				}
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00015E04 File Offset: 0x00014004
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x00015E0C File Offset: 0x0001400C
		public long Count
		{
			get
			{
				return this.m_countBytes;
			}
			set
			{
				this.m_countBytes = value;
			}
		}

		// Token: 0x040002A5 RID: 677
		private long m_countBytes;
	}
}
