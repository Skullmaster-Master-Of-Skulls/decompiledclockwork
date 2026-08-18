using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Internal.Performance
{
	// Token: 0x02000005 RID: 5
	internal sealed class CodeMarkers
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public bool IsEnabled
		{
			get
			{
				return this.state == CodeMarkers.State.Enabled;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205B File Offset: 0x0000025B
		private CodeMarkers()
		{
			this.state = ((CodeMarkers.NativeMethods.FindAtom("VSCodeMarkersEnabled") != 0) ? CodeMarkers.State.Enabled : CodeMarkers.State.Disabled);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000207C File Offset: 0x0000027C
		public bool CodeMarker(int nTimerID)
		{
			if (!this.IsEnabled)
			{
				return false;
			}
			try
			{
				CodeMarkers.NativeMethods.DllPerfCodeMarker(nTimerID, null, 0);
			}
			catch (DllNotFoundException)
			{
				this.state = CodeMarkers.State.DisabledDueToDllImportException;
				return false;
			}
			return true;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020C0 File Offset: 0x000002C0
		public bool CodeMarkerEx(int nTimerID, byte[] aBuff)
		{
			if (!this.IsEnabled)
			{
				return false;
			}
			if (aBuff == null)
			{
				throw new ArgumentNullException("aBuff");
			}
			try
			{
				CodeMarkers.NativeMethods.DllPerfCodeMarker(nTimerID, aBuff, aBuff.Length);
			}
			catch (DllNotFoundException)
			{
				this.state = CodeMarkers.State.DisabledDueToDllImportException;
				return false;
			}
			return true;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002114 File Offset: 0x00000314
		public bool CodeMarkerEx(int nTimerID, Guid guidData)
		{
			return this.CodeMarkerEx(nTimerID, guidData.ToByteArray());
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002124 File Offset: 0x00000324
		public bool CodeMarkerEx(int nTimerID, string stringData)
		{
			return this.CodeMarkerEx(nTimerID, Encoding.Unicode.GetBytes(stringData));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002138 File Offset: 0x00000338
		public bool CodeMarkerEx(int nTimerID, uint uintData)
		{
			return this.CodeMarkerEx(nTimerID, BitConverter.GetBytes(uintData));
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002147 File Offset: 0x00000347
		public bool CodeMarkerEx(int nTimerID, ulong ulongData)
		{
			return this.CodeMarkerEx(nTimerID, BitConverter.GetBytes(ulongData));
		}

		// Token: 0x04000055 RID: 85
		public static readonly CodeMarkers Instance = new CodeMarkers();

		// Token: 0x04000056 RID: 86
		private const string AtomName = "VSCodeMarkersEnabled";

		// Token: 0x04000057 RID: 87
		private const string DllName = "Microsoft.Internal.Performance.CodeMarkers.dll";

		// Token: 0x04000058 RID: 88
		private CodeMarkers.State state;

		// Token: 0x0200039B RID: 923
		private static class NativeMethods
		{
			// Token: 0x0600258D RID: 9613
			[DllImport("Microsoft.Internal.Performance.CodeMarkers.dll", EntryPoint = "PerfCodeMarker")]
			public static extern void DllPerfCodeMarker(int nTimerID, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] aUserParams, int cbParams);

			// Token: 0x0600258E RID: 9614
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
			public static extern ushort FindAtom([MarshalAs(UnmanagedType.LPWStr)] string lpString);
		}

		// Token: 0x0200039C RID: 924
		private enum State
		{
			// Token: 0x04001B6E RID: 7022
			Enabled,
			// Token: 0x04001B6F RID: 7023
			Disabled,
			// Token: 0x04001B70 RID: 7024
			DisabledDueToDllImportException,
			// Token: 0x04001B71 RID: 7025
			DisabledViaRegistryCheck
		}
	}
}
