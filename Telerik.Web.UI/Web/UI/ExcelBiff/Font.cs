using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AA5 RID: 2725
	internal sealed class Font : BaseBiffRecord, IRecord
	{
		// Token: 0x060067E0 RID: 26592 RVA: 0x00184A74 File Offset: 0x00182C74
		public Font() : base(49)
		{
			this.dyHeight = 200;
			this.grbit = 0;
			this.icv = 32767;
			this.bls = 400;
			this.sss = 0;
			this.uls = 0;
			this.bFamily = 0;
			this.bCharSet = 0;
			this.reserved = 0;
			this.unicodeFlag = 1;
			this.rgch = "Arial";
		}

		// Token: 0x060067E1 RID: 26593 RVA: 0x00184AE8 File Offset: 0x00182CE8
		public byte[] GetData()
		{
			this.cch = (byte)this.rgch.Length;
			base.Length = (ushort)(16 + this.rgch.Length * 2);
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.dyHeight);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.grbit);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.icv);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.bls);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.sss);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			data[num] = this.uls;
			num++;
			data[num] = this.bFamily;
			num++;
			using (Font font = new Font(this.rgch, (float)this.dyHeight))
			{
				Font.LOGFONT logfont = new Font.LOGFONT();
				font.ToLogFont(logfont);
				this.bCharSet = logfont.lfCharSet;
			}
			data[num] = this.bCharSet;
			num++;
			data[num] = this.reserved;
			num++;
			data[num] = this.cch;
			num++;
			data[num] = this.unicodeFlag;
			num++;
			bytes = Encoding.Unicode.GetBytes(this.rgch);
			bytes.CopyTo(data, num);
			return data;
		}

		// Token: 0x060067E2 RID: 26594 RVA: 0x00184C5C File Offset: 0x00182E5C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[FONT]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("dyHeight={0};", this.dyHeight);
			stringBuilder.AppendFormat("grbit=0x{0:x4};", this.grbit);
			stringBuilder.AppendFormat("icv=0x{0:x4};", this.icv);
			stringBuilder.AppendFormat("bls={0};", this.bls);
			stringBuilder.AppendFormat("sss=0x{0:x4};", this.sss);
			stringBuilder.AppendFormat("uls=0x{0:x4};", this.uls);
			stringBuilder.AppendFormat("bFamily={0};", this.bFamily);
			stringBuilder.AppendFormat("bCharSet={0};", this.bCharSet);
			stringBuilder.AppendFormat("reserved={0};", this.reserved);
			stringBuilder.AppendFormat("cch={0};", this.cch);
			stringBuilder.AppendFormat("unicodeFlag=0x{0:x4};", this.unicodeFlag);
			stringBuilder.AppendFormat("rgch={0};", this.rgch);
			stringBuilder.Append("[/FONT]");
			return stringBuilder.ToString();
		}

		// Token: 0x17002228 RID: 8744
		// (set) Token: 0x060067E3 RID: 26595 RVA: 0x00184DA9 File Offset: 0x00182FA9
		public BiffCell.FontAttributes FontAttributes
		{
			set
			{
				this.grbit = (ushort)value;
			}
		}

		// Token: 0x17002229 RID: 8745
		// (set) Token: 0x060067E4 RID: 26596 RVA: 0x00184DB3 File Offset: 0x00182FB3
		public BiffCell.FontBoldness FontBold
		{
			set
			{
				this.bls = (ushort)value;
			}
		}

		// Token: 0x1700222A RID: 8746
		// (set) Token: 0x060067E5 RID: 26597 RVA: 0x00184DBD File Offset: 0x00182FBD
		public ushort FontColor
		{
			set
			{
				this.icv = value;
			}
		}

		// Token: 0x1700222B RID: 8747
		// (get) Token: 0x060067E6 RID: 26598 RVA: 0x00184DC6 File Offset: 0x00182FC6
		// (set) Token: 0x060067E7 RID: 26599 RVA: 0x00184DCE File Offset: 0x00182FCE
		public string FontName
		{
			get
			{
				return this.rgch;
			}
			set
			{
				this.rgch = value;
			}
		}

		// Token: 0x1700222C RID: 8748
		// (set) Token: 0x060067E8 RID: 26600 RVA: 0x00184DD7 File Offset: 0x00182FD7
		public BiffCell.FontScripts FontScript
		{
			set
			{
				this.sss = (ushort)value;
			}
		}

		// Token: 0x1700222D RID: 8749
		// (get) Token: 0x060067E9 RID: 26601 RVA: 0x00184DE1 File Offset: 0x00182FE1
		// (set) Token: 0x060067EA RID: 26602 RVA: 0x00184DE9 File Offset: 0x00182FE9
		public ushort FontSize
		{
			get
			{
				return this.dyHeight;
			}
			set
			{
				this.dyHeight = value;
			}
		}

		// Token: 0x1700222E RID: 8750
		// (set) Token: 0x060067EB RID: 26603 RVA: 0x00184DF2 File Offset: 0x00182FF2
		internal BiffCell.FontUnderlines FontUnderline
		{
			set
			{
				this.uls = (byte)value;
			}
		}

		// Token: 0x04001AD6 RID: 6870
		private const ushort type = 49;

		// Token: 0x04001AD7 RID: 6871
		private const ushort fixedPartLength = 16;

		// Token: 0x04001AD8 RID: 6872
		private ushort dyHeight;

		// Token: 0x04001AD9 RID: 6873
		private ushort grbit;

		// Token: 0x04001ADA RID: 6874
		private ushort icv;

		// Token: 0x04001ADB RID: 6875
		private ushort bls;

		// Token: 0x04001ADC RID: 6876
		private ushort sss;

		// Token: 0x04001ADD RID: 6877
		private byte uls;

		// Token: 0x04001ADE RID: 6878
		private byte bFamily;

		// Token: 0x04001ADF RID: 6879
		private byte bCharSet;

		// Token: 0x04001AE0 RID: 6880
		private byte reserved;

		// Token: 0x04001AE1 RID: 6881
		private byte cch;

		// Token: 0x04001AE2 RID: 6882
		private byte unicodeFlag;

		// Token: 0x04001AE3 RID: 6883
		private string rgch;

		// Token: 0x02000AA6 RID: 2726
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private class LOGFONT
		{
			// Token: 0x04001AE4 RID: 6884
			public int lfHeight;

			// Token: 0x04001AE5 RID: 6885
			public int lfWidth;

			// Token: 0x04001AE6 RID: 6886
			public int lfEscapement;

			// Token: 0x04001AE7 RID: 6887
			public int lfOrientation;

			// Token: 0x04001AE8 RID: 6888
			public int lfWeight;

			// Token: 0x04001AE9 RID: 6889
			public byte lfItalic;

			// Token: 0x04001AEA RID: 6890
			public byte lfUnderline;

			// Token: 0x04001AEB RID: 6891
			public byte lfStrikeOut;

			// Token: 0x04001AEC RID: 6892
			public byte lfCharSet;

			// Token: 0x04001AED RID: 6893
			public byte lfOutPrecision;

			// Token: 0x04001AEE RID: 6894
			public byte lfClipPrecision;

			// Token: 0x04001AEF RID: 6895
			public byte lfQuality;

			// Token: 0x04001AF0 RID: 6896
			public byte lfPitchAndFamily;

			// Token: 0x04001AF1 RID: 6897
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string lfFaceName;
		}
	}
}
