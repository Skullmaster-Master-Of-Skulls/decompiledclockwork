using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000080 RID: 128
	[SuppressUnmanagedCodeSecurity]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class UTF8CommandText
	{
		// Token: 0x060005B6 RID: 1462 RVA: 0x0003E82F File Offset: 0x0003D82F
		public UTF8CommandText(IntPtr pUTF8CmdText)
		{
			this.m_utf8CmdText = pUTF8CmdText;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0003E83E File Offset: 0x0003D83E
		public UTF8CommandText()
		{
			this.m_addParam = true;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0003E850 File Offset: 0x0003D850
		protected override void Finalize()
		{
			try
			{
				if (this.m_utf8CmdText != IntPtr.Zero)
				{
					try
					{
						Marshal.FreeCoTaskMem(this.m_utf8CmdText);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
					}
					this.m_utf8CmdText = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x040003A8 RID: 936
		internal IntPtr m_utf8CmdText;

		// Token: 0x040003A9 RID: 937
		internal bool m_parsed;

		// Token: 0x040003AA RID: 938
		internal bool m_addParam;

		// Token: 0x040003AB RID: 939
		internal static Pooler m_pooler = new Pooler(10, 200);
	}
}
