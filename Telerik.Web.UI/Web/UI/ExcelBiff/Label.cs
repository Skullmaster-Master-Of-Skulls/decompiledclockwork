using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AB6 RID: 2742
	internal sealed class Label : BaseBiffRecord, IRecord
	{
		// Token: 0x06006818 RID: 26648 RVA: 0x00185938 File Offset: 0x00183B38
		public Label(ushort row, ushort column, ushort xFIndex, string label) : base(516)
		{
			this.rw = row;
			this.col = column;
			this.ixfe = xFIndex;
			if (string.IsNullOrEmpty(label))
			{
				this.cch = 0;
				base.Length = 8;
				return;
			}
			this.rgch = this.ReplaceLineFeed(label);
			this.cch = (ushort)this.rgch.Length;
			base.Length = (ushort)(8 + this.rgch.Length * 2 + 3);
		}

		// Token: 0x06006819 RID: 26649 RVA: 0x001859B8 File Offset: 0x00183BB8
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.rw);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.col);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.ixfe);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.cch);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			if (this.rgch != null)
			{
				data[num] = 1;
				num++;
				UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
				bytes = unicodeEncoding.GetBytes(this.rgch);
				bytes.CopyTo(data, num);
			}
			return data;
		}

		// Token: 0x0600681A RID: 26650 RVA: 0x00185A64 File Offset: 0x00183C64
		private string ReplaceLineFeed(string textValue)
		{
			if (textValue != null)
			{
				string text = textValue.Replace("\r\n", "\n");
				return text.Replace("\r", "\n");
			}
			return textValue;
		}

		// Token: 0x04001B30 RID: 6960
		public const ushort Type = 516;

		// Token: 0x04001B31 RID: 6961
		private const ushort fixedPartLength = 8;

		// Token: 0x04001B32 RID: 6962
		private const int maxUnicodeHeader = 3;

		// Token: 0x04001B33 RID: 6963
		private ushort rw;

		// Token: 0x04001B34 RID: 6964
		private ushort col;

		// Token: 0x04001B35 RID: 6965
		private ushort ixfe;

		// Token: 0x04001B36 RID: 6966
		private ushort cch;

		// Token: 0x04001B37 RID: 6967
		private string rgch;
	}
}
