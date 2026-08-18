using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AE6 RID: 2790
	internal class StyleBIFF : BaseBiffRecord, IRecord
	{
		// Token: 0x060068DF RID: 26847 RVA: 0x001894C4 File Offset: 0x001876C4
		public StyleBIFF(ushort index, byte builtInStyle) : base(659)
		{
			base.Length = 4;
			this.ixfe = (index | 32768);
			this.istyBuiltIn = builtInStyle;
			this.iLevel = byte.MaxValue;
		}

		// Token: 0x060068E0 RID: 26848 RVA: 0x001894F8 File Offset: 0x001876F8
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.ixfe);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			data[num] = this.istyBuiltIn;
			num++;
			data[num] = this.iLevel;
			return data;
		}

		// Token: 0x060068E1 RID: 26849 RVA: 0x00189540 File Offset: 0x00187740
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[STYLE]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("ixfe=0x{0:x4};", this.ixfe);
			stringBuilder.AppendFormat("istyBuiltIn=0x{0:x4};", this.istyBuiltIn);
			stringBuilder.AppendFormat("iLevel=0x{0:x4};", this.iLevel);
			stringBuilder.Append("[/STYLE]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001C14 RID: 7188
		private const ushort type = 659;

		// Token: 0x04001C15 RID: 7189
		private const ushort length = 4;

		// Token: 0x04001C16 RID: 7190
		private ushort ixfe;

		// Token: 0x04001C17 RID: 7191
		private byte istyBuiltIn;

		// Token: 0x04001C18 RID: 7192
		private byte iLevel;
	}
}
