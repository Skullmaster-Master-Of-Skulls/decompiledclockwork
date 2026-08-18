using System;
using Spire.CompoundFile.Doc;

namespace Spire.Doc
{
	// Token: 0x020000E3 RID: 227
	public class PreferredWidth
	{
		// Token: 0x0600035A RID: 858 RVA: 0x000269E0 File Offset: 0x000259E0
		static PreferredWidth()
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
			PreferredWidth.ᜁ = new PreferredWidth(WidthType.Auto, 0);
			PreferredWidth.ᜂ = new PreferredWidth(WidthType.None, 0);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00026A34 File Offset: 0x00025A34
		public PreferredWidth(WidthType type, short value)
		{
			int a_ = 16;
			this.ᜄ = WidthType.Auto;
			base..ctor();
			switch (type)
			{
			case WidthType.None:
			case WidthType.Auto:
				if (value != 0)
				{
					string message = string.Format(ClipboardData.b("ⅵၷόቻ幽ꚅﲇﲋ낏ﮑ뚕ꪙ늝肟횡첣쎥袧\udca9춫슭얯ힱ钳\udbb5춷즹좻麽ꊿꟁ", a_), type);
					throw new ArgumentException(message, ClipboardData.b("u᥷ᙹॻ᭽", a_));
				}
				break;
			case WidthType.Percentage:
			{
				if (value >= 0)
				{
					if (value <= 600)
					{
						break;
					}
				}
				string message2 = string.Format(ClipboardData.b("ⅵၷόቻ幽ꚅﲇﲋ낏ﮑ뚕ꪙ늝肟횡첣쎥袧\udca9춫슭얯ힱ钳\udbb5춷즹좻麽ꊿꟁꣅꟇ꓉ꃍ뗏뗑뗓ꋕ뇗곙맛ﻝ臟賡胣웥蓧迩鿫鷭탯蛱鳳韵雷\udaf9鏻賽⃿朁甃猅椇昉Ⰻ稍缏㈑∓☕⠗", a_), type);
				throw new ArgumentException(message2, ClipboardData.b("u᥷ᙹॻ᭽", a_));
			}
			case WidthType.Twip:
			{
				if (value >= 0)
				{
					if (value <= 31680)
					{
						break;
					}
				}
				string message3 = string.Format(ClipboardData.b("ⅵၷόቻ幽ꚅﲇﲋ낏ﮑ뚕ꪙ늝肟횡첣쎥袧\udca9춫슭얯ힱ钳\udbb5춷즹좻麽ꊿꟁꣅꟇ꓉ꃍ뗏뗑뗓ꋕ뇗곙맛ﻝ臟賡胣웥觧蓩裫컭鳯韱蟳藵\ud8f7軹铻鿽滿∁欃琅⠇漉紋笍焏縑㐓戕眗㨙⼛⼝టᐡᰣᘥ", a_), type);
				throw new ArgumentException(message3, ClipboardData.b("u᥷ᙹॻ᭽", a_));
			}
			}
			this.ᜄ = type;
			this.ᜃ = value;
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00026B44 File Offset: 0x00025B44
		public short Value
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
				return this.ᜃ;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00026B88 File Offset: 0x00025B88
		public WidthType Type
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
				return this.ᜄ;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00026BCC File Offset: 0x00025BCC
		public static PreferredWidth Auto
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
				return PreferredWidth.ᜁ;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00026C0C File Offset: 0x00025C0C
		public static PreferredWidth None
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
				return PreferredWidth.None;
			}
		}

		// Token: 0x04000CB9 RID: 3257
		internal const int ᜀ = 50;

		// Token: 0x04000CBA RID: 3258
		private long[] \u2460\u0091\u0095\u00AD;

		// Token: 0x04000CBB RID: 3259
		private float \u2460\u00A2\u00A1\u008C;

		// Token: 0x04000CBC RID: 3260
		private string[] \u2460\u00AC\u009E\u00A0;

		// Token: 0x04000CBD RID: 3261
		private byte[] \u2609\u008F\u008F\u00A4;

		// Token: 0x04000CBE RID: 3262
		private bool[] \u25D8\u007F\u00A2\u0086;

		// Token: 0x04000CBF RID: 3263
		private static PreferredWidth ᜁ;

		// Token: 0x04000CC0 RID: 3264
		private static PreferredWidth ᜂ;

		// Token: 0x04000CC1 RID: 3265
		private short ᜃ;

		// Token: 0x04000CC2 RID: 3266
		private WidthType ᜄ;
	}
}
