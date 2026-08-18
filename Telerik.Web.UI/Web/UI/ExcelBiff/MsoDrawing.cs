using System;
using System.IO;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000ABC RID: 2748
	internal sealed class MsoDrawing : BaseBiffRecord, IRecord
	{
		// Token: 0x0600682E RID: 26670 RVA: 0x00185E44 File Offset: 0x00184044
		public MsoDrawing() : base(236)
		{
		}

		// Token: 0x0600682F RID: 26671 RVA: 0x00185E51 File Offset: 0x00184051
		public byte[] GetData()
		{
			return null;
		}

		// Token: 0x06006830 RID: 26672 RVA: 0x00185E54 File Offset: 0x00184054
		public void WriteMsoDrawingHeader(Stream stream, ushort length)
		{
			base.Length = length;
			byte[] baseData = base.GetBaseData();
			stream.Write(baseData, 0, baseData.Length);
		}

		// Token: 0x04001B51 RID: 6993
		private const ushort type = 236;
	}
}
