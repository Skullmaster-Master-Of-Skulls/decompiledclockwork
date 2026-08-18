using System;
using System.Globalization;
using System.IO;
using log4net.Core;

namespace log4net.Layout
{
	// Token: 0x020000AC RID: 172
	public class Layout2RawLayoutAdapter : IRawLayout
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x0000FEDC File Offset: 0x0000E0DC
		public Layout2RawLayoutAdapter(ILayout layout)
		{
			this.m_layout = layout;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000FEEC File Offset: 0x0000E0EC
		public virtual object Format(LoggingEvent loggingEvent)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.m_layout.Format(stringWriter, loggingEvent);
			return stringWriter.ToString();
		}

		// Token: 0x04000212 RID: 530
		private ILayout m_layout;
	}
}
