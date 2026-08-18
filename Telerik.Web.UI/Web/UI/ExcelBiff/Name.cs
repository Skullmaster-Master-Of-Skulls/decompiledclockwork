using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AC0 RID: 2752
	internal sealed class Name : BaseBiffRecord, IRecord
	{
		// Token: 0x0600683B RID: 26683 RVA: 0x001864D8 File Offset: 0x001846D8
		public Name(string nameValue, ushort externSheetIndex, ushort row, ushort col) : base(24)
		{
			this.cch = (byte)nameValue.Length;
			this.cce = 7;
			this.unicodeByte = 1;
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
			this.rgch = unicodeEncoding.GetBytes(nameValue);
			this.rgce = new byte[7];
			this.rgce[0] = 58;
			int num = 1;
			byte[] bytes = BitConverter.GetBytes(externSheetIndex);
			bytes.CopyTo(this.rgce, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(row);
			bytes.CopyTo(this.rgce, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(col);
			bytes.CopyTo(this.rgce, num);
			base.Length = (ushort)(22 + this.rgch.Length);
		}

		// Token: 0x0600683C RID: 26684 RVA: 0x00186590 File Offset: 0x00184790
		public Name(ushort externSheetIndex, ushort sheetIndex, ushort firstRow, ushort lastRow) : base(24)
		{
			this.grbit = 32;
			this.cch = 1;
			this.cce = 11;
			this.itab = sheetIndex;
			this.unicodeByte = 0;
			this.rgch = new byte[]
			{
				7
			};
			this.rgce = new byte[11];
			this.rgce[0] = 59;
			int num = 1;
			byte[] bytes = BitConverter.GetBytes(externSheetIndex);
			bytes.CopyTo(this.rgce, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(firstRow);
			bytes.CopyTo(this.rgce, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(lastRow);
			bytes.CopyTo(this.rgce, num);
			num += bytes.Length;
			ushort value = 0;
			bytes = BitConverter.GetBytes(value);
			bytes.CopyTo(this.rgce, num);
			num += bytes.Length;
			value = 255;
			bytes = BitConverter.GetBytes(value);
			bytes.CopyTo(this.rgce, num);
			base.Length = 27;
		}

		// Token: 0x0600683D RID: 26685 RVA: 0x00186684 File Offset: 0x00184884
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.grbit);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			data[num] = this.chKey;
			num++;
			data[num] = this.cch;
			num++;
			bytes = BitConverter.GetBytes(this.cce);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.ixals);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.itab);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			data[num] = this.cchCustMenu;
			num++;
			data[num] = this.cchDescription;
			num++;
			data[num] = this.lcchHelpTopic;
			num++;
			data[num] = this.cchStatusText;
			num++;
			data[num] = this.unicodeByte;
			num++;
			this.rgch.CopyTo(data, num);
			num += this.rgch.Length;
			this.rgce.CopyTo(data, num);
			return data;
		}

		// Token: 0x0600683E RID: 26686 RVA: 0x00186784 File Offset: 0x00184984
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[NAME]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("grbit=0x{0:x4};", this.grbit);
			stringBuilder.AppendFormat("chKey={0};", this.chKey);
			stringBuilder.AppendFormat("cch={0};", this.cch);
			stringBuilder.AppendFormat("cce={0};", this.cce);
			stringBuilder.AppendFormat("ixals={0};", this.ixals);
			stringBuilder.AppendFormat("itab={0};", this.itab);
			stringBuilder.AppendFormat("cchCustMenu=0x{0:x2};", this.cchCustMenu);
			stringBuilder.AppendFormat("cchDescription=0x{0:x2};", this.cchDescription);
			stringBuilder.AppendFormat("lcchHelpTopic=0x{0:x2};", this.lcchHelpTopic);
			stringBuilder.AppendFormat("cchStatusText=0x{0:x2};", this.cchStatusText);
			stringBuilder.AppendFormat("unicodeByte=0x{0:x2};", this.unicodeByte);
			stringBuilder.AppendFormat("rgch.Length={0};", this.rgch.Length);
			stringBuilder.AppendFormat("rgce.Length={0};", this.rgce.Length);
			stringBuilder.Append("[/NAME]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001B61 RID: 7009
		private const ushort type = 24;

		// Token: 0x04001B62 RID: 7010
		private const ushort descriptionLength = 7;

		// Token: 0x04001B63 RID: 7011
		private const ushort builtInDescriptionLength = 11;

		// Token: 0x04001B64 RID: 7012
		private const byte ptgRef3D = 58;

		// Token: 0x04001B65 RID: 7013
		private const byte ptgArea3D = 59;

		// Token: 0x04001B66 RID: 7014
		private ushort grbit;

		// Token: 0x04001B67 RID: 7015
		private byte chKey;

		// Token: 0x04001B68 RID: 7016
		private byte cch;

		// Token: 0x04001B69 RID: 7017
		private ushort cce;

		// Token: 0x04001B6A RID: 7018
		private ushort ixals;

		// Token: 0x04001B6B RID: 7019
		private ushort itab;

		// Token: 0x04001B6C RID: 7020
		private byte cchCustMenu;

		// Token: 0x04001B6D RID: 7021
		private byte cchDescription;

		// Token: 0x04001B6E RID: 7022
		private byte lcchHelpTopic;

		// Token: 0x04001B6F RID: 7023
		private byte cchStatusText;

		// Token: 0x04001B70 RID: 7024
		private byte unicodeByte;

		// Token: 0x04001B71 RID: 7025
		private byte[] rgch;

		// Token: 0x04001B72 RID: 7026
		private byte[] rgce;
	}
}
