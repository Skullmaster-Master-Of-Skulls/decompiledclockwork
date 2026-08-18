using System;
using System.ComponentModel;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.XML
{
	// Token: 0x02000184 RID: 388
	public class XmlOptions : ICloneable
	{
		// Token: 0x06000AA6 RID: 2726 RVA: 0x0006FEA8 File Offset: 0x0006EEA8
		public XmlOptions()
		{
			int a_ = 13;
			this.ᜀ = HyperlinksCollectionEditor.b("ᠨԪᴬ", a_);
			this.ᜁ = string.Empty;
			this.ᜂ = true;
			base..ctor();
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0006FEEC File Offset: 0x0006EEEC
		public object Clone()
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
			return new XmlOptions
			{
				StandAlone = this.StandAlone,
				Encoding = this.Encoding,
				Version = this.Version
			};
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0006FF54 File Offset: 0x0006EF54
		// (set) Token: 0x06000AA9 RID: 2729 RVA: 0x0006FF98 File Offset: 0x0006EF98
		[DefaultValue("1.0")]
		[Description("Gets or sets the version of the result xml document.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string Version
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
				int num = 1;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							this.ᜀ = value;
							if (true)
							{
							}
							num = 2;
							continue;
						case 2:
							return;
						}
						if (!(value != this.ᜀ))
						{
							return;
						}
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x00070018 File Offset: 0x0006F018
		// (set) Token: 0x06000AAB RID: 2731 RVA: 0x0007005C File Offset: 0x0006F05C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the encoding of the result xml document.")]
		[DefaultValue("")]
		public string Encoding
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
				int num = 1;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							return;
						case 2:
							if (true)
							{
							}
							this.ᜁ = value;
							num = 0;
							continue;
						}
						if (!(value != this.ᜁ))
						{
							return;
						}
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x000700DC File Offset: 0x0006F0DC
		// (set) Token: 0x06000AAD RID: 2733 RVA: 0x00070120 File Offset: 0x0006F120
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(true)]
		[Description("Indicates whether the result document standalone.")]
		public bool StandAlone
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
				int num = 1;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							return;
						case 2:
							this.ᜂ = value;
							num = 0;
							continue;
						}
						if (value == this.ᜂ)
						{
							return;
						}
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x04000825 RID: 2085
		private string ᜀ;

		// Token: 0x04000826 RID: 2086
		private int[] \u2593\u00A6\u008C\u0090;

		// Token: 0x04000827 RID: 2087
		private float \u2609\u00AD\u0082\u0084;

		// Token: 0x04000828 RID: 2088
		private string ᜁ;

		// Token: 0x04000829 RID: 2089
		private bool ᜂ;
	}
}
