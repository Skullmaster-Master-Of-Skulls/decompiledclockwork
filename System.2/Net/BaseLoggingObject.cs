using System;

namespace System.Net
{
	// Token: 0x020001C1 RID: 449
	internal class BaseLoggingObject
	{
		// Token: 0x060011B5 RID: 4533 RVA: 0x00060318 File Offset: 0x0005E518
		internal BaseLoggingObject()
		{
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00060320 File Offset: 0x0005E520
		internal virtual void EnterFunc(string funcname)
		{
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00060322 File Offset: 0x0005E522
		internal virtual void LeaveFunc(string funcname)
		{
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00060324 File Offset: 0x0005E524
		internal virtual void DumpArrayToConsole()
		{
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00060326 File Offset: 0x0005E526
		internal virtual void PrintLine(string msg)
		{
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00060328 File Offset: 0x0005E528
		internal virtual void DumpArray(bool shouldClose)
		{
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0006032A File Offset: 0x0005E52A
		internal virtual void DumpArrayToFile(bool shouldClose)
		{
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0006032C File Offset: 0x0005E52C
		internal virtual void Flush()
		{
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0006032E File Offset: 0x0005E52E
		internal virtual void Flush(bool close)
		{
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00060330 File Offset: 0x0005E530
		internal virtual void LoggingMonitorTick()
		{
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00060332 File Offset: 0x0005E532
		internal virtual void Dump(byte[] buffer)
		{
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00060334 File Offset: 0x0005E534
		internal virtual void Dump(byte[] buffer, int length)
		{
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00060336 File Offset: 0x0005E536
		internal virtual void Dump(byte[] buffer, int offset, int length)
		{
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00060338 File Offset: 0x0005E538
		internal virtual void Dump(IntPtr pBuffer, int offset, int length)
		{
		}
	}
}
