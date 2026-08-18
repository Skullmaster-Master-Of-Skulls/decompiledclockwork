using System;

namespace Microsoft.Internal.Performance
{
	// Token: 0x02000006 RID: 6
	internal struct CodeMarkerStartEnd : IDisposable
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002162 File Offset: 0x00000362
		internal CodeMarkerStartEnd(int begin, int end)
		{
			CodeMarkers.Instance.CodeMarker(begin);
			this._end = end;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002177 File Offset: 0x00000377
		public void Dispose()
		{
			if (this._end != 0)
			{
				CodeMarkers.Instance.CodeMarker(this._end);
				this._end = 0;
			}
		}

		// Token: 0x04000059 RID: 89
		private int _end;
	}
}
