using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Web;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.PropEditors;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001E4 RID: 484
	public class CellGraphic : CustomItem, ICloneable
	{
		// Token: 0x06000EA5 RID: 3749 RVA: 0x000A1DB4 File Offset: 0x000A0DB4
		protected override void Dispose(bool Disposing)
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
				if (this.ᜀ)
				{
					return;
				}
				break;
			}
			try
			{
				this.ᜂ.Close();
				this.ᜀ = true;
			}
			finally
			{
				base.Dispose(Disposing);
			}
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x000A1E28 File Offset: 0x000A0E28
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
			return new CellGraphic
			{
				FileName = this.FileName,
				Height = this.Height,
				Stream = this.Stream,
				Width = this.Width
			};
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x000A1E9C File Offset: 0x000A0E9C
		internal override void InitCollectionItem()
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
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x000A1ED8 File Offset: 0x000A0ED8
		public bool IsFileSource()
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
			return this.ᜂ.Length == 0L;
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x000A1F24 File Offset: 0x000A0F24
		[Browsable(false)]
		public override ItemType ItemType
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
				return ItemType.Graphic;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x000A1F64 File Offset: 0x000A0F64
		[Browsable(false)]
		public XlsGraphicType GraphicType
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
				return this.ᜅ;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x000A1FA8 File Offset: 0x000A0FA8
		// (set) Token: 0x06000EAC RID: 3756 RVA: 0x000A1FEC File Offset: 0x000A0FEC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public MemoryStream Stream
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
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 1:
						return;
					case 2:
						this.ᜂ.SetLength(0L);
						value.WriteTo(this.ᜂ);
						num = 1;
						continue;
					}
					if (value == null)
					{
						break;
					}
					if (true)
					{
					}
					num = 2;
				}
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x000A2074 File Offset: 0x000A1074
		// (set) Token: 0x06000EAE RID: 3758 RVA: 0x000A20B8 File Offset: 0x000A10B8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Height
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
			set
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						this.ᜃ = value;
						if (true)
						{
						}
						num = 0;
						continue;
					}
					if (value == this.ᜃ)
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x000A2134 File Offset: 0x000A1134
		// (set) Token: 0x06000EB0 RID: 3760 RVA: 0x000A2178 File Offset: 0x000A1178
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int Width
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜄ;
			}
			set
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 1;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜄ = value;
						num = 2;
						continue;
					case 1:
						if (true)
						{
						}
						break;
					case 2:
						return;
					}
					if (value == this.ᜄ)
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x000A21F4 File Offset: 0x000A11F4
		// (set) Token: 0x06000EB2 RID: 3762 RVA: 0x000A237C File Offset: 0x000A137C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("")]
		[Description("Defines the name of the file that contains the picture.")]
		[Editor(typeof(ImageFileNameEditor), typeof(UITypeEditor))]
		public string FileName
		{
			get
			{
				string text;
				for (;;)
				{
					text = this.ᜁ;
					string text2 = null;
					int num = 6;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (sprᮌ.ᜀ())
							{
								num = 2;
								continue;
							}
							return text;
						case 1:
							num = 4;
							continue;
						case 2:
							goto IL_D1;
						case 3:
							if (text2 == null)
							{
								num = 5;
								continue;
							}
							return text;
						case 4:
							if (text.Trim().Length == 0)
							{
								num = 7;
								continue;
							}
							goto IL_13C;
						case 5:
							try
							{
								UriBuilder uriBuilder = new UriBuilder(text);
								return uriBuilder.Uri.LocalPath;
							}
							catch
							{
								return text;
							}
							goto Block_5;
						case 6:
							if (text != null)
							{
								num = 1;
								continue;
							}
							return text;
						case 7:
							goto IL_95;
						}
						break;
						Block_5:
						try
						{
							IL_D1:
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_129;
								case 1:
									goto IL_131;
								case 3:
									text2 = HttpContext.Current.Request.MapPath(this.ᜁ);
									text = text2;
									num = 0;
									continue;
								}
								if (HttpContext.Current.Request != null)
								{
									num = 3;
									continue;
								}
								IL_129:
								num = 1;
							}
							IL_131:
							goto IL_50;
						}
						catch (Exception)
						{
							goto IL_50;
						}
						goto IL_13C;
						IL_50:
						if (true)
						{
						}
						num = 3;
						continue;
						IL_13C:
						num = 0;
					}
				}
				return text;
				IL_95:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return text;
				default:
					if (false)
					{
					}
					return text;
				}
				return text;
			}
			set
			{
				int a_ = 19;
				int num = 23;
				for (;;)
				{
					string text;
					switch (num)
					{
					case 0:
						if (text == HyperlinksCollectionEditor.b("縮猰縲攴", a_))
						{
							num = 3;
							continue;
						}
						num = 2;
						continue;
					case 1:
						num = 13;
						continue;
					case 2:
						if (text == HyperlinksCollectionEditor.b("昮爰簲", a_))
						{
							num = 12;
							continue;
						}
						this.ᜅ = XlsGraphicType.Unknown;
						num = 11;
						continue;
					case 3:
						goto IL_2FF;
					case 4:
						num = 0;
						continue;
					case 5:
						if (!(text == HyperlinksCollectionEditor.b("攮愰琲", a_)))
						{
							num = 14;
							continue;
						}
						goto IL_16E;
					case 6:
						if (text == HyperlinksCollectionEditor.b("攮愰瘲爴", a_))
						{
							num = 25;
							continue;
						}
						num = 8;
						continue;
					case 7:
						text = text.Remove(0, 1);
						num = 22;
						continue;
					case 8:
						if (text == HyperlinksCollectionEditor.b("缮缰琲", a_))
						{
							num = 16;
							continue;
						}
						num = 15;
						continue;
					case 9:
						goto IL_34F;
					case 10:
						this.ᜁ = value;
						text = Path.GetExtension(this.ᜁ).Trim().ToUpper();
						num = 17;
						continue;
					case 11:
						goto IL_1C8;
					case 12:
						goto IL_1B4;
					case 13:
						if (text[0] == '.')
						{
							num = 7;
							continue;
						}
						goto IL_107;
					case 14:
						num = 6;
						continue;
					case 15:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_226;
						default:
							if (false)
							{
							}
							if (text == HyperlinksCollectionEditor.b("栮砰甲", a_))
							{
								num = 9;
								continue;
							}
							num = 19;
							continue;
						}
						break;
					case 16:
						goto IL_102;
					case 17:
						goto IL_226;
					case 18:
						if (text == HyperlinksCollectionEditor.b("樮簰甲", a_))
						{
							num = 20;
							continue;
						}
						num = 5;
						continue;
					case 19:
						if (!(text == HyperlinksCollectionEditor.b("洮簰挲", a_)))
						{
							num = 4;
							continue;
						}
						goto IL_138;
					case 20:
						goto IL_26E;
					case 21:
						if (text == HyperlinksCollectionEditor.b("砮簰甲", a_))
						{
							num = 24;
							continue;
						}
						num = 18;
						continue;
					case 22:
						if (true)
						{
						}
						goto IL_107;
					case 24:
						goto IL_136;
					case 25:
						goto IL_2CB;
					}
					if (this.ᜁ != value)
					{
						num = 10;
						continue;
					}
					return;
					IL_107:
					num = 21;
					continue;
					IL_226:
					if (text.Length <= 0)
					{
						return;
					}
					num = 1;
				}
				IL_102:
				this.ᜅ = XlsGraphicType.PNG;
				return;
				IL_136:
				this.ᜅ = XlsGraphicType.WMF;
				return;
				IL_138:
				this.ᜅ = XlsGraphicType.BMP;
				return;
				IL_16E:
				this.ᜅ = XlsGraphicType.JPG;
				return;
				IL_1B4:
				this.ᜅ = XlsGraphicType.ICO;
				return;
				IL_1C8:
				return;
				IL_26E:
				this.ᜅ = XlsGraphicType.EMF;
				return;
				IL_2CB:
				goto IL_16E;
				IL_2FF:
				goto IL_138;
				IL_34F:
				this.ᜅ = XlsGraphicType.GIF;
			}
		}

		// Token: 0x04000B22 RID: 2850
		private bool ᜀ;

		// Token: 0x04000B23 RID: 2851
		private string ᜁ = string.Empty;

		// Token: 0x04000B24 RID: 2852
		private byte \u2609\u0091\u0097\u0085;

		// Token: 0x04000B25 RID: 2853
		private MemoryStream ᜂ = new MemoryStream();

		// Token: 0x04000B26 RID: 2854
		private int ᜃ;

		// Token: 0x04000B27 RID: 2855
		private int ᜄ;

		// Token: 0x04000B28 RID: 2856
		private XlsGraphicType ᜅ;
	}
}
