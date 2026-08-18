using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200021B RID: 539
	public class ICC_Profile
	{
		// Token: 0x060014FE RID: 5374 RVA: 0x000760FA File Offset: 0x000750FA
		protected ICC_Profile()
		{
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x00076104 File Offset: 0x00075104
		public static ICC_Profile GetInstance(byte[] data)
		{
			if (data.Length < 128 || data[36] != 97 || data[37] != 99 || data[38] != 115 || data[39] != 112)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.icc.profile"));
			}
			ICC_Profile icc_Profile = new ICC_Profile();
			icc_Profile.data = data;
			ICC_Profile.cstags.TryGetValue(Encoding.ASCII.GetString(data, 16, 4), out icc_Profile.numComponents);
			return icc_Profile;
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00076178 File Offset: 0x00075178
		public static ICC_Profile GetInstance(Stream file)
		{
			byte[] array = new byte[128];
			int i = array.Length;
			int num = 0;
			while (i > 0)
			{
				int num2 = file.Read(array, num, i);
				if (num2 <= 0)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.icc.profile"));
				}
				i -= num2;
				num += num2;
			}
			if (array[36] != 97 || array[37] != 99 || array[38] != 115 || array[39] != 112)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.icc.profile"));
			}
			i = ((int)(array[0] & byte.MaxValue) << 24 | (int)(array[1] & byte.MaxValue) << 16 | (int)(array[2] & byte.MaxValue) << 8 | (int)(array[3] & byte.MaxValue));
			byte[] array2 = new byte[i];
			Array.Copy(array, 0, array2, 0, array.Length);
			i -= array.Length;
			num = array.Length;
			while (i > 0)
			{
				int num3 = file.Read(array2, num, i);
				if (num3 <= 0)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.icc.profile"));
				}
				i -= num3;
				num += num3;
			}
			return ICC_Profile.GetInstance(array2);
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0007627C File Offset: 0x0007527C
		public static ICC_Profile GetInstance(string fname)
		{
			FileStream fileStream = new FileStream(fname, FileMode.Open, FileAccess.Read, FileShare.Read);
			ICC_Profile instance = ICC_Profile.GetInstance(fileStream);
			fileStream.Close();
			return instance;
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x000762A1 File Offset: 0x000752A1
		public byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x000762A9 File Offset: 0x000752A9
		public int NumComponents
		{
			get
			{
				return this.numComponents;
			}
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x000762B4 File Offset: 0x000752B4
		static ICC_Profile()
		{
			ICC_Profile.cstags["XYZ "] = 3;
			ICC_Profile.cstags["Lab "] = 3;
			ICC_Profile.cstags["Luv "] = 3;
			ICC_Profile.cstags["YCbr"] = 3;
			ICC_Profile.cstags["Yxy "] = 3;
			ICC_Profile.cstags["RGB "] = 3;
			ICC_Profile.cstags["GRAY"] = 1;
			ICC_Profile.cstags["HSV "] = 3;
			ICC_Profile.cstags["HLS "] = 3;
			ICC_Profile.cstags["CMYK"] = 4;
			ICC_Profile.cstags["CMY "] = 3;
			ICC_Profile.cstags["2CLR"] = 2;
			ICC_Profile.cstags["3CLR"] = 3;
			ICC_Profile.cstags["4CLR"] = 4;
			ICC_Profile.cstags["5CLR"] = 5;
			ICC_Profile.cstags["6CLR"] = 6;
			ICC_Profile.cstags["7CLR"] = 7;
			ICC_Profile.cstags["8CLR"] = 8;
			ICC_Profile.cstags["9CLR"] = 9;
			ICC_Profile.cstags["ACLR"] = 10;
			ICC_Profile.cstags["BCLR"] = 11;
			ICC_Profile.cstags["CCLR"] = 12;
			ICC_Profile.cstags["DCLR"] = 13;
			ICC_Profile.cstags["ECLR"] = 14;
			ICC_Profile.cstags["FCLR"] = 15;
		}

		// Token: 0x04000E36 RID: 3638
		protected byte[] data;

		// Token: 0x04000E37 RID: 3639
		protected int numComponents;

		// Token: 0x04000E38 RID: 3640
		private static Dictionary<string, int> cstags = new Dictionary<string, int>();
	}
}
