using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A5C RID: 2652
	internal sealed class Backup : BaseBiffRecord, IRecord
	{
		// Token: 0x060066E3 RID: 26339 RVA: 0x00180F4F File Offset: 0x0017F14F
		public Backup() : base(64)
		{
			base.Length = 2;
			this.fBackupFile = 0;
		}

		// Token: 0x060066E4 RID: 26340 RVA: 0x00180F68 File Offset: 0x0017F168
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.fBackupFile);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x060066E5 RID: 26341 RVA: 0x00180F94 File Offset: 0x0017F194
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[BACKUP]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("fBackupFile={0};", this.fBackupFile);
			stringBuilder.Append("[/BACKUP]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001908 RID: 6408
		private const ushort type = 64;

		// Token: 0x04001909 RID: 6409
		private const ushort length = 2;

		// Token: 0x0400190A RID: 6410
		private ushort fBackupFile;
	}
}
