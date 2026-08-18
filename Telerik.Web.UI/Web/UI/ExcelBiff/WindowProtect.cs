using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AEF RID: 2799
	internal sealed class WindowProtect : BaseBiffRecord, IRecord
	{
		// Token: 0x06006900 RID: 26880 RVA: 0x0018A11F File Offset: 0x0018831F
		public WindowProtect() : base(25)
		{
			base.Length = 2;
			this.fLockWn = 0;
		}

		// Token: 0x06006901 RID: 26881 RVA: 0x0018A138 File Offset: 0x00188338
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.fLockWn);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x06006902 RID: 26882 RVA: 0x0018A164 File Offset: 0x00188364
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[WINDOWPROTECT]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("fLockWn={0};", this.fLockWn);
			stringBuilder.Append("[/WINDOWPROTECT]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001C43 RID: 7235
		private const ushort type = 25;

		// Token: 0x04001C44 RID: 7236
		private const ushort length = 2;

		// Token: 0x04001C45 RID: 7237
		private ushort fLockWn;
	}
}
