using System;

namespace System.Net
{
	// Token: 0x020004EA RID: 1258
	internal class BaseLoggingObject
	{
		// Token: 0x06002730 RID: 10032 RVA: 0x000A24E4 File Offset: 0x000A14E4
		internal BaseLoggingObject()
		{
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x000A24EC File Offset: 0x000A14EC
		internal virtual void EnterFunc(string funcname)
		{
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x000A24EE File Offset: 0x000A14EE
		internal virtual void LeaveFunc(string funcname)
		{
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x000A24F0 File Offset: 0x000A14F0
		internal virtual void DumpArrayToConsole()
		{
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x000A24F2 File Offset: 0x000A14F2
		internal virtual void PrintLine(string msg)
		{
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x000A24F4 File Offset: 0x000A14F4
		internal virtual void DumpArray(bool shouldClose)
		{
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x000A24F6 File Offset: 0x000A14F6
		internal virtual void DumpArrayToFile(bool shouldClose)
		{
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x000A24F8 File Offset: 0x000A14F8
		internal virtual void Flush()
		{
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x000A24FA File Offset: 0x000A14FA
		internal virtual void Flush(bool close)
		{
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x000A24FC File Offset: 0x000A14FC
		internal virtual void LoggingMonitorTick()
		{
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x000A24FE File Offset: 0x000A14FE
		internal virtual void Dump(byte[] buffer)
		{
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x000A2500 File Offset: 0x000A1500
		internal virtual void Dump(byte[] buffer, int length)
		{
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000A2502 File Offset: 0x000A1502
		internal virtual void Dump(byte[] buffer, int offset, int length)
		{
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x000A2504 File Offset: 0x000A1504
		internal virtual void Dump(IntPtr pBuffer, int offset, int length)
		{
		}
	}
}
