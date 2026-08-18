using System;
using System.ComponentModel;
using System.Drawing;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;
using Spire.DataExport.TypeConverters;
using Spire.DataExport.Utils;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001E5 RID: 485
	[TypeConverter(typeof(CollectionTypeConverter))]
	public class CellPicture : CellGraphic
	{
		// Token: 0x06000EB4 RID: 3764 RVA: 0x000A2710 File Offset: 0x000A1710
		public new object Clone()
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
			return new CellPicture
			{
				FileName = base.FileName,
				PictureType = this.PictureType
			};
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x000A276C File Offset: 0x000A176C
		internal override void InitCollectionItem()
		{
			int a_ = 12;
			for (;;)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 7;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						if (base.Collection != null)
						{
							num3 = 4;
							continue;
						}
						goto IL_E7;
					case 1:
						goto IL_6D;
					case 2:
						goto IL_6D;
					case 3:
						num3 = 2;
						continue;
					case 4:
						num3 = 6;
						continue;
					case 5:
						goto IL_14C;
					case 6:
						if (base.Collection is CellPictures)
						{
							num3 = 3;
							continue;
						}
						goto IL_E7;
					case 7:
						if (this.ᜀ.Length == 0)
						{
							num3 = 10;
							continue;
						}
						goto IL_14C;
					case 8:
						goto IL_E7;
					case 9:
						if (!(base.Collection as CellPictures).Find(string.Format(HyperlinksCollectionEditor.b("砧䌩伫娭䔯䀱儳椵䌷ਹ䄻", a_), num), ref num2))
						{
							if (true)
							{
							}
							num3 = 8;
							continue;
						}
						num++;
						num3 = 1;
						continue;
					case 10:
						num3 = 0;
						continue;
					}
					break;
					IL_6D:
					num3 = 9;
					continue;
					IL_106:
					num3 = 5;
					continue;
					IL_14C:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_106;
					default:
						goto IL_162;
					}
					IL_E7:
					this.ᜀ = string.Format(HyperlinksCollectionEditor.b("砧䌩伫娭䔯䀱儳椵䌷ਹ䄻", a_), num);
					goto IL_106;
				}
			}
			IL_162:
			if (false)
			{
			}
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x000A28E4 File Offset: 0x000A18E4
		public int CalcRefCount()
		{
			switch (0)
			{
			default:
			{
				int num;
				for (;;)
				{
					IL_7F:
					num = 0;
					int num2 = 4;
					for (;;)
					{
						int num3;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
						{
							if (false)
							{
							}
							int num4;
							switch (num2)
							{
							case 0:
								if (base.Collection is CellPictures)
								{
									num2 = 2;
									continue;
								}
								return num;
							case 1:
								if (true)
								{
								}
								if (base.Collection.Holder is CellExport)
								{
									num2 = 14;
									continue;
								}
								return num;
							case 2:
								num2 = 13;
								continue;
							case 3:
								num2 = 0;
								continue;
							case 4:
								if (base.Collection != null)
								{
									num2 = 3;
									continue;
								}
								return num;
							case 5:
							{
								CellExport cellExport;
								if (string.Compare(this.ᜀ, cellExport.Sheets[num3].Images[num4].PictureName, true) == 0)
								{
									num2 = 12;
									continue;
								}
								goto IL_A6;
							}
							case 6:
								goto IL_A6;
							case 7:
								goto IL_175;
							case 8:
								goto IL_1AB;
							case 9:
								goto IL_1A9;
							case 10:
								return num;
							case 11:
								num2 = 1;
								continue;
							case 12:
								num++;
								num2 = 6;
								continue;
							case 13:
								if (base.Collection.Holder != null)
								{
									num2 = 11;
									continue;
								}
								return num;
							case 14:
							{
								CellExport cellExport = base.Collection.Holder as CellExport;
								num3 = 0;
								num2 = 8;
								continue;
							}
							case 15:
							{
								CellExport cellExport;
								if (num4 >= cellExport.Sheets[num3].Images.Count)
								{
									num2 = 9;
									continue;
								}
								num2 = 5;
								continue;
							}
							case 16:
								goto IL_175;
							case 17:
								goto IL_1AB;
							case 18:
							{
								CellExport cellExport;
								if (num3 >= cellExport.Sheets.Count)
								{
									num2 = 10;
									continue;
								}
								num4 = 0;
								num2 = 16;
								continue;
							}
							}
							goto IL_7F;
							IL_A6:
							num4++;
							num2 = 7;
							continue;
							IL_175:
							num2 = 15;
							continue;
							IL_1AB:
							num2 = 18;
							continue;
						}
						}
						IL_1A9:
						num3++;
						num2 = 17;
					}
				}
				return num;
			}
			}
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x000A2B44 File Offset: 0x000A1B44
		public void GetMeasurements(ref int H, ref int W)
		{
			int num = 8;
			for (;;)
			{
				Image image;
				switch (num)
				{
				case 0:
					image = sprᮌ.ᜃ(base.FileName);
					num = 2;
					continue;
				case 1:
					goto IL_101;
				case 2:
					goto IL_E8;
				case 3:
					goto IL_4F;
				case 4:
				{
					if (true)
					{
					}
					bool flag = true;
					num = 7;
					continue;
				}
				case 5:
					if (sprᮌ.ᜁ(base.FileName))
					{
						num = 4;
						continue;
					}
					return;
				case 6:
					if (sprᮌ.ᜀ())
					{
						num = 0;
						continue;
					}
					image = Image.FromFile(base.FileName);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_101;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					break;
				case 7:
				{
					bool flag;
					if (flag)
					{
						num = 1;
						continue;
					}
					return;
				}
				case 9:
					goto IL_E8;
				}
				if (base.Stream.Length > 0L)
				{
					num = 3;
					continue;
				}
				H = 0;
				W = 0;
				num = 5;
				continue;
				IL_101:
				image = null;
				num = 6;
				continue;
				try
				{
					IL_E8:
					H = image.Height;
					W = image.Width;
					return;
				}
				finally
				{
					image.Dispose();
				}
				goto IL_101;
			}
			IL_4F:
			H = base.Height;
			W = base.Width;
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x000A2CB0 File Offset: 0x000A1CB0
		public void SaveToXmlFile(XMLFile File, string Section)
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
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x000A2CEC File Offset: 0x000A1CEC
		public void LoadFromXmlFile(XMLFile File, string Section)
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
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x000A2D28 File Offset: 0x000A1D28
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
				return ItemType.Picture;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x000A2D64 File Offset: 0x000A1D64
		[Browsable(false)]
		public CellPictures Pictures
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
				return base.Collection as CellPictures;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x000A2DAC File Offset: 0x000A1DAC
		// (set) Token: 0x06000EBD RID: 3773 RVA: 0x000A2DF0 File Offset: 0x000A1DF0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CellPictureType PictureType
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

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x000A2E34 File Offset: 0x000A1E34
		// (set) Token: 0x06000EBF RID: 3775 RVA: 0x000A2E78 File Offset: 0x000A1E78
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the picture name in the result Excel document.")]
		[Browsable(true)]
		[DefaultValue("")]
		public new string Name
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
				return this.ᜀ;
			}
			set
			{
				int a_ = 3;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (base.Collection is CellPictures)
						{
							num = 10;
							continue;
						}
						goto IL_E2;
					case 1:
						if (value.Length != 0)
						{
							int num2 = 0;
							num = 8;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_133;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 2:
						num = 1;
						continue;
					case 4:
						goto IL_133;
					case 5:
						if (true)
						{
						}
						num = 0;
						continue;
					case 6:
						goto IL_F4;
					case 7:
						goto IL_92;
					case 8:
						if (base.Collection != null)
						{
							num = 5;
							continue;
						}
						goto IL_E2;
					case 9:
					{
						int num2;
						if ((base.Collection as CellPictures).Find(value, ref num2))
						{
							num = 7;
							continue;
						}
						goto IL_E2;
					}
					case 10:
						num = 9;
						continue;
					}
					if (this.ᜀ != value)
					{
						num = 2;
						continue;
					}
					return;
					IL_E2:
					this.ᜀ = value;
					num = 6;
				}
				IL_92:
				throw new Exception(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("嘞传唢䐤䬦䀨伪戬弮吰䄲吴䌶倸吺匼怾ᅀ⩂♄㍆㱈㥊⡌੎⥐㩒♔⍖", a_)), value));
				IL_F4:
				return;
				IL_133:
				throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("帞匠䐢嘤砦礨䈪丬嬮䐰䄲倴礶堸嘺堼", a_)));
			}
		}

		// Token: 0x04000B29 RID: 2857
		private int \u2460\u009E\u00A4\u009E;

		// Token: 0x04000B2A RID: 2858
		private long \u25D8\u00A7\u007F\u0093;

		// Token: 0x04000B2B RID: 2859
		private long \u2609\u0093\u009C\u00A7;

		// Token: 0x04000B2C RID: 2860
		private string ᜀ = string.Empty;

		// Token: 0x04000B2D RID: 2861
		private int[] \u2593\u008B\u0098\u00AB;

		// Token: 0x04000B2E RID: 2862
		private string[] \u2460\u00A8\u009C\u0087;

		// Token: 0x04000B2F RID: 2863
		private CellPictureType ᜁ;
	}
}
