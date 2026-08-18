using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A72 RID: 2674
	internal sealed class BoundSheet : BaseBiffRecord, IRecord
	{
		// Token: 0x06006702 RID: 26370 RVA: 0x00181A34 File Offset: 0x0017FC34
		public BoundSheet(string workSheetName) : base(133)
		{
			workSheetName = BoundSheet.TrimName(workSheetName);
			base.Length = (ushort)(8 + workSheetName.Length * 2);
			this.lbPlyPos = 0U;
			this.grbit = 0;
			this.cch = (byte)workSheetName.Length;
			this.rgch = workSheetName;
		}

		// Token: 0x06006703 RID: 26371 RVA: 0x00181A88 File Offset: 0x0017FC88
		public static string TrimName(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				foreach (char value in ":\\/?*[]")
				{
					int num = name.IndexOf(value);
					if (num >= 0)
					{
						name = name.Remove(num, 1);
					}
				}
				if (name.Length > 31)
				{
					name = name.Remove(31);
				}
			}
			return name;
		}

		// Token: 0x06006704 RID: 26372 RVA: 0x00181AE8 File Offset: 0x0017FCE8
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.lbPlyPos);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.grbit);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			data[num] = this.cch;
			num++;
			data[num] = 1;
			num++;
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
			bytes = unicodeEncoding.GetBytes(this.rgch);
			bytes.CopyTo(data, num);
			return data;
		}

		// Token: 0x06006705 RID: 26373 RVA: 0x00181B64 File Offset: 0x0017FD64
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[BOUNDSHEET]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("lbPlyPos=0x{0:x8};", this.lbPlyPos);
			stringBuilder.AppendFormat("grbit=0x{0:x4};", this.grbit);
			stringBuilder.AppendFormat("cch={0};", this.cch);
			stringBuilder.AppendFormat("rgch={0};", this.rgch);
			stringBuilder.Append("[/BOUNDSHEET]");
			return stringBuilder.ToString();
		}

		// Token: 0x040019D8 RID: 6616
		private const ushort type = 133;

		// Token: 0x040019D9 RID: 6617
		private const ushort fixedPartLength = 8;

		// Token: 0x040019DA RID: 6618
		private uint lbPlyPos;

		// Token: 0x040019DB RID: 6619
		private byte cch;

		// Token: 0x040019DC RID: 6620
		private ushort grbit;

		// Token: 0x040019DD RID: 6621
		private string rgch;
	}
}
