using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000611 RID: 1553
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public class ENUMLOGFONTEX
	{
		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06005D3C RID: 23868 RVA: 0x003AA0A0 File Offset: 0x003A90A0
		public string FullName
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ(this.ᜀ);
			}
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06005D3D RID: 23869 RVA: 0x003AA0E8 File Offset: 0x003A90E8
		public string Style
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ(this.ᜁ);
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06005D3E RID: 23870 RVA: 0x003AA130 File Offset: 0x003A9130
		public LOGFONT LogFont
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.m_logFont;
			}
		}

		// Token: 0x06005D3F RID: 23871 RVA: 0x003AA174 File Offset: 0x003A9174
		private string ᜀ(byte[] A_0)
		{
			int num = 3;
			int num2;
			for (;;)
			{
				int num3;
				switch (num)
				{
				case 0:
					num2++;
					num = 4;
					continue;
				case 1:
					num2 = 0;
					num3 = A_0.Length;
					num = 2;
					continue;
				case 2:
					goto IL_63;
				case 4:
					goto IL_63;
				case 5:
					goto IL_6B;
				case 6:
					goto IL_77;
				case 7:
					if (A_0[num2] != 0)
					{
						num = 0;
						continue;
					}
					goto IL_9A;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_6B:
					if (num2 >= num3)
					{
						num = 6;
						continue;
					}
					if (true)
					{
					}
					num = 7;
					continue;
				default:
					if (false)
					{
					}
					if (A_0 != null)
					{
						num = 1;
						continue;
					}
					goto IL_CF;
				}
				IL_63:
				num = 5;
			}
			IL_77:
			IL_9A:
			Encoding @default = Encoding.Default;
			return @default.GetString(A_0, 0, num2);
			IL_CF:
			return null;
		}

		// Token: 0x04002D5D RID: 11613
		public LOGFONT m_logFont;

		// Token: 0x04002D5E RID: 11614
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		private byte[] ᜀ;

		// Token: 0x04002D5F RID: 11615
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		private byte[] ᜁ;

		// Token: 0x04002D60 RID: 11616
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		public byte[] m_arrScript;
	}
}
