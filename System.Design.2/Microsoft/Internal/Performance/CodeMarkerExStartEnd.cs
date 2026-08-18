using System;
using System.Text;

namespace Microsoft.Internal.Performance
{
	// Token: 0x02000007 RID: 7
	internal struct CodeMarkerExStartEnd : IDisposable
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002199 File Offset: 0x00000399
		internal CodeMarkerExStartEnd(int begin, int end, byte[] aBuff)
		{
			CodeMarkers.Instance.CodeMarkerEx(begin, aBuff);
			this._end = end;
			this._aBuff = aBuff;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021B6 File Offset: 0x000003B6
		internal CodeMarkerExStartEnd(int begin, int end, Guid guidData)
		{
			this = new CodeMarkerExStartEnd(begin, end, guidData.ToByteArray());
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021C7 File Offset: 0x000003C7
		internal CodeMarkerExStartEnd(int begin, int end, string stringData)
		{
			this = new CodeMarkerExStartEnd(begin, end, Encoding.Unicode.GetBytes(stringData));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021DC File Offset: 0x000003DC
		internal CodeMarkerExStartEnd(int begin, int end, uint uintData)
		{
			this = new CodeMarkerExStartEnd(begin, end, BitConverter.GetBytes(uintData));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021EC File Offset: 0x000003EC
		internal CodeMarkerExStartEnd(int begin, int end, ulong ulongData)
		{
			this = new CodeMarkerExStartEnd(begin, end, BitConverter.GetBytes(ulongData));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021FC File Offset: 0x000003FC
		public void Dispose()
		{
			if (this._end != 0)
			{
				CodeMarkers.Instance.CodeMarkerEx(this._end, this._aBuff);
				this._end = 0;
				this._aBuff = null;
			}
		}

		// Token: 0x0400005A RID: 90
		private int _end;

		// Token: 0x0400005B RID: 91
		private byte[] _aBuff;
	}
}
