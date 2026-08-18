using System;
using System.Text;

namespace MailBee.Mime
{
	// Token: 0x02000568 RID: 1384
	public class StringConversionConfig
	{
		// Token: 0x06002DDF RID: 11743 RVA: 0x000DD739 File Offset: 0x000DC739
		internal StringConversionConfig()
		{
			this.a = StringConversionMode.NoConversion;
			this.b = Global.DefaultEncoding;
			this.c = Global.DefaultEncoding;
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x000DD75E File Offset: 0x000DC75E
		internal StringConversionConfig(StringConversionMode A_0, Encoding A_1, Encoding A_2)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06002DE1 RID: 11745 RVA: 0x000DD77B File Offset: 0x000DC77B
		// (set) Token: 0x06002DE2 RID: 11746 RVA: 0x000DD783 File Offset: 0x000DC783
		public StringConversionMode ConversionMode
		{
			get
			{
				return this.a;
			}
			set
			{
				this.a = value;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x000DD78C File Offset: 0x000DC78C
		// (set) Token: 0x06002DE4 RID: 11748 RVA: 0x000DD794 File Offset: 0x000DC794
		public Encoding DestinationEncoding
		{
			get
			{
				return this.b;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.b = value;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06002DE5 RID: 11749 RVA: 0x000DD7A8 File Offset: 0x000DC7A8
		// (set) Token: 0x06002DE6 RID: 11750 RVA: 0x000DD7B0 File Offset: 0x000DC7B0
		public Encoding CustomByteEncoding
		{
			get
			{
				return this.c;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.c = value;
			}
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x000DD7C4 File Offset: 0x000DC7C4
		internal StringConversionConfig a()
		{
			return new StringConversionConfig(this.a, this.b, this.c);
		}

		// Token: 0x04001FA7 RID: 8103
		private StringConversionMode a;

		// Token: 0x04001FA8 RID: 8104
		private Encoding b;

		// Token: 0x04001FA9 RID: 8105
		private Encoding c;
	}
}
