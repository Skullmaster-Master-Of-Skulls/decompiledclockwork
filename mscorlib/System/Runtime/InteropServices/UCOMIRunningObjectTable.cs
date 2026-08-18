using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000547 RID: 1351
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00000010-0000-0000-C000-000000000046")]
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IRunningObjectTable instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[ComImport]
	public interface UCOMIRunningObjectTable
	{
		// Token: 0x0600338A RID: 13194
		void Register(int grfFlags, [MarshalAs(UnmanagedType.Interface)] object punkObject, UCOMIMoniker pmkObjectName, out int pdwRegister);

		// Token: 0x0600338B RID: 13195
		void Revoke(int dwRegister);

		// Token: 0x0600338C RID: 13196
		void IsRunning(UCOMIMoniker pmkObjectName);

		// Token: 0x0600338D RID: 13197
		void GetObject(UCOMIMoniker pmkObjectName, [MarshalAs(UnmanagedType.Interface)] out object ppunkObject);

		// Token: 0x0600338E RID: 13198
		void NoteChangeTime(int dwRegister, ref FILETIME pfiletime);

		// Token: 0x0600338F RID: 13199
		void GetTimeOfLastChange(UCOMIMoniker pmkObjectName, out FILETIME pfiletime);

		// Token: 0x06003390 RID: 13200
		void EnumRunning(out UCOMIEnumMoniker ppenumMoniker);
	}
}
