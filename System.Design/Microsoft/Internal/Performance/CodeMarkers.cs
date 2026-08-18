using System;
using System.Runtime.InteropServices;

namespace Microsoft.Internal.Performance
{
	// Token: 0x02000590 RID: 1424
	internal sealed class CodeMarkers
	{
		// Token: 0x06003271 RID: 12913 RVA: 0x0011D6E7 File Offset: 0x0011C6E7
		private CodeMarkers()
		{
			this.fUseCodeMarkers = (CodeMarkers.NativeMethods.FindAtom("VSCodeMarkersEnabled") != 0);
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x0011D708 File Offset: 0x0011C708
		public void CodeMarker(CodeMarkerEvent nTimerID)
		{
			if (!this.fUseCodeMarkers)
			{
				return;
			}
			try
			{
				CodeMarkers.NativeMethods.DllPerfCodeMarker((int)nTimerID, null, 0);
			}
			catch (DllNotFoundException)
			{
				this.fUseCodeMarkers = false;
			}
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x0011D744 File Offset: 0x0011C744
		public void CodeMarkerEx(CodeMarkerEvent nTimerID, byte[] aBuff)
		{
			if (aBuff == null)
			{
				throw new ArgumentNullException("aBuff");
			}
			if (!this.fUseCodeMarkers)
			{
				return;
			}
			try
			{
				CodeMarkers.NativeMethods.DllPerfCodeMarker((int)nTimerID, aBuff, aBuff.Length);
			}
			catch (DllNotFoundException)
			{
				this.fUseCodeMarkers = false;
			}
		}

		// Token: 0x04002180 RID: 8576
		private const string AtomName = "VSCodeMarkersEnabled";

		// Token: 0x04002181 RID: 8577
		private const string DllName = "Microsoft.Internal.Performance.CodeMarkers.dll";

		// Token: 0x04002182 RID: 8578
		public static readonly CodeMarkers Instance = new CodeMarkers();

		// Token: 0x04002183 RID: 8579
		private bool fUseCodeMarkers;

		// Token: 0x02000591 RID: 1425
		internal class NativeMethods
		{
			// Token: 0x06003275 RID: 12917
			[DllImport("Microsoft.Internal.Performance.CodeMarkers.dll", EntryPoint = "PerfCodeMarker")]
			public static extern void DllPerfCodeMarker(int nTimerID, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] aUserParams, int cbParams);

			// Token: 0x06003276 RID: 12918
			[DllImport("kernel32.dll")]
			public static extern ushort FindAtom(string lpString);

			// Token: 0x06003277 RID: 12919
			[DllImport("kernel32.dll")]
			public static extern ushort AddAtom(string lpString);

			// Token: 0x06003278 RID: 12920
			[DllImport("kernel32.dll")]
			public static extern ushort DeleteAtom(ushort atom);
		}
	}
}
