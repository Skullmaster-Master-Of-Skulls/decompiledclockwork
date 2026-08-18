using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Interface;

namespace Spire.Doc
{
	// Token: 0x020000F0 RID: 240
	public class PictureWatermark : WatermarkBase
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0002DB2C File Offset: 0x0002CB2C
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x0002DB74 File Offset: 0x0002CB74
		public float Scaling
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
				return this.ᜀ.HeightScale;
			}
			set
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
				DocPicture docPicture = this.ᜀ;
				this.ᜀ.WidthScale = value;
				docPicture.HeightScale = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0002DBCC File Offset: 0x0002CBCC
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x0002DC10 File Offset: 0x0002CC10
		public bool IsWashout
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
				return this.ᜁ;
			}
			set
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
				this.ᜁ = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0002DC54 File Offset: 0x0002CC54
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0002DC9C File Offset: 0x0002CC9C
		public Image Picture
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
				return this.ᜀ.Image;
			}
			set
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
				this.ᜂ = -1;
				this.ᜀ.LoadImage(value);
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0002DCEC File Offset: 0x0002CCEC
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0002DD30 File Offset: 0x0002CD30
		internal DocPicture WordPicture
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
				return this.ᜀ;
			}
			set
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
				this.ᜀ = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0002DD74 File Offset: 0x0002CD74
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x0002DDB8 File Offset: 0x0002CDB8
		internal int OriginalPib
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
				return this.ᜂ;
			}
			set
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
				this.ᜂ = value;
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0002DDFC File Offset: 0x0002CDFC
		public PictureWatermark() : base(WatermarkType.PictureWatermark)
		{
			this.ᜀ = new DocPicture(null);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0002DE2C File Offset: 0x0002CE2C
		public PictureWatermark(Image image, bool washout) : this()
		{
			this.Picture = image;
			this.ᜁ = washout;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0002DE50 File Offset: 0x0002CE50
		internal PictureWatermark(Document A_0) : base(A_0, WatermarkType.PictureWatermark)
		{
			this.ᜀ = new DocPicture(A_0);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0002DE80 File Offset: 0x0002CE80
		protected override void InitXDLSHolder()
		{
			int a_ = 1;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			base.XDLSHolder.AddElement(ClipboardData.b("๦Ѩ੪੬੮", a_), this.ᜀ);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0002DEE4 File Offset: 0x0002CEE4
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 11;
			for (;;)
			{
				base.WriteXmlAttributes(writer);
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_38;
						default:
							if (false)
							{
							}
							goto IL_76;
						}
						break;
					case 1:
						if (true)
						{
						}
						writer.WriteValue(ClipboardData.b("ⅰᩲᙴͶ౸ॺ᡼⡾ﲈﾊ", a_), this.ᜁ);
						num = 0;
						continue;
					case 2:
						return;
					case 3:
						if (this.ᜂ != -1)
						{
							num = 5;
							continue;
						}
						return;
					case 4:
						goto IL_38;
					case 5:
						writer.WriteValue(ClipboardData.b("ⅰᩲᙴͶ౸ॺ᡼⽾", a_), this.ᜂ);
						num = 2;
						continue;
					}
					break;
					IL_38:
					if (!this.ᜁ)
					{
						num = 1;
						continue;
					}
					IL_76:
					num = 3;
				}
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0002DFD4 File Offset: 0x0002CFD4
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 14;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_38;
					case 1:
						return;
					case 2:
						this.ᜁ = reader.ReadBoolean(ClipboardData.b("⑳ή᭷๹ॻ౽햁曆揄", a_));
						num = 5;
						continue;
					case 3:
						this.ᜂ = reader.ReadInt(ClipboardData.b("⑳ή᭷๹ॻ౽튁", a_));
						num = 1;
						continue;
					case 4:
						if (reader.HasAttribute(ClipboardData.b("⑳ή᭷๹ॻ౽튁", a_)))
						{
							num = 3;
							continue;
						}
						return;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_38;
						default:
							if (false)
							{
							}
							goto IL_84;
						}
						break;
					}
					break;
					IL_38:
					if (reader.HasAttribute(ClipboardData.b("⑳ή᭷๹ॻ౽햁曆揄", a_)))
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					IL_84:
					num = 4;
				}
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0002E0E0 File Offset: 0x0002D0E0
		protected override object CloneImpl()
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
			PictureWatermark pictureWatermark = (PictureWatermark)base.CloneImpl();
			pictureWatermark.WordPicture = (DocPicture)this.WordPicture.Clone();
			return pictureWatermark;
		}

		// Token: 0x04000D0E RID: 3342
		private int \u25D9\u00A0\u009F\u00AB;

		// Token: 0x04000D0F RID: 3343
		private new DocPicture ᜀ;

		// Token: 0x04000D10 RID: 3344
		private float[] \u2609\u00A7\u0087\u00AF;

		// Token: 0x04000D11 RID: 3345
		private float[] \u2460\u009B\u0084\u008F;

		// Token: 0x04000D12 RID: 3346
		private new bool ᜁ = true;

		// Token: 0x04000D13 RID: 3347
		private byte[] \u2609ª\u0087\u00A2;

		// Token: 0x04000D14 RID: 3348
		private int ᜂ = -1;
	}
}
