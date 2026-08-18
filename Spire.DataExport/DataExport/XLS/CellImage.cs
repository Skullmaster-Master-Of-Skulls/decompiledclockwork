using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.PropEditors;
using Spire.DataExport.TypeConverters;
using Spire.DataExport.Utils;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001AD RID: 429
	[TypeConverter(typeof(CollectionTypeConverter))]
	public class CellImage : CustomItem, ICloneable
	{
		// Token: 0x06000BE2 RID: 3042 RVA: 0x0007CC9C File Offset: 0x0007BC9C
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
			return new CellImage
			{
				Column = this.Column,
				PictureName = this.PictureName,
				Row = this.Row,
				Title = this.Title,
				Zoom = this.Zoom
			};
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0007CD1C File Offset: 0x0007BD1C
		internal override void InitCollectionItem()
		{
			int a_ = 9;
			int num2;
			int num3;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_67:
				int num = 10;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						goto IL_91;
					case 2:
						num = 7;
						continue;
					case 3:
						goto IL_91;
					case 4:
						if (true)
						{
						}
						num = 5;
						continue;
					case 5:
						if (base.Collection != null)
						{
							num = 2;
							continue;
						}
						goto IL_103;
					case 6:
						return;
					case 7:
						if (base.Collection is CellImages)
						{
							num = 0;
							continue;
						}
						goto IL_103;
					case 8:
						if (!(base.Collection as CellImages).Find(string.Format(HyperlinksCollectionEditor.b("氤䨦䠨䰪䠬瀮䨰̲䠴", a_), num2), ref num3))
						{
							num = 9;
							continue;
						}
						num2++;
						num = 3;
						continue;
					case 9:
						goto IL_103;
					case 10:
						if (this.ᜅ.Length == 0)
						{
							num = 4;
							continue;
						}
						return;
					}
					goto IL_63;
					IL_91:
					num = 8;
					continue;
					IL_103:
					this.ᜅ = string.Format(HyperlinksCollectionEditor.b("氤䨦䠨䰪䠬瀮䨰̲䠴", a_), num2);
					num = 6;
				}
				return;
			}
			default:
				if (false)
				{
				}
				break;
			}
			IL_63:
			num2 = 0;
			num3 = 0;
			goto IL_67;
		}

		// Token: 0x06000BE4 RID: 3044
		[DllImport("gdi32")]
		private static extern IntPtr GetStockObject(int A_0);

		// Token: 0x06000BE5 RID: 3045
		[DllImport("gdi32")]
		public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

		// Token: 0x06000BE6 RID: 3046
		[DllImport("gdi32")]
		public static extern bool DeleteDC(IntPtr hdc);

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0007CE94 File Offset: 0x0007BE94
		internal spr\u1DCA ᜀ()
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				spr\u1DCA result;
				for (;;)
				{
					result.ᜀ = 0;
					result.ᜁ = 0;
					result.ᜂ = 0;
					result.ᜃ = 0;
					result.ᜄ = 0;
					result.ᜅ = 0;
					result.ᜆ = 0;
					result.ᜇ = 0;
					result.ᜈ = 0;
					int num = 4;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
						{
							if (!this.Sheet.ExportCell.Pictures.Find(this.ᜄ, ref num2))
							{
								num = 2;
								continue;
							}
							CellPicture cellPicture = this.Sheet.ExportCell.Pictures[num2];
							Graphics graphics = Graphics.FromHwnd((IntPtr)0);
							num = 6;
							continue;
						}
						case 1:
							goto IL_100;
						case 2:
							return result;
						case 3:
							if (this.Sheet.ExportCell == null)
							{
								num = 1;
								continue;
							}
							goto IL_60D;
						case 4:
							if (this.Sheet != null)
							{
								num = 5;
								continue;
							}
							return result;
						case 5:
							num = 3;
							continue;
						case 6:
							try
							{
								for (;;)
								{
									Font font = Font.FromHfont(CellImage.GetStockObject(17));
									Color black = Color.Black;
									num = 14;
									for (;;)
									{
										Graphics graphics;
										int num4;
										double num3;
										int num5;
										double num6;
										double num7;
										double num8;
										switch (num)
										{
										case 0:
											if (num2 <= this.Sheet.ExportCell.ColWidthList.Count - 1)
											{
												num = 24;
												continue;
											}
											goto IL_43B;
										case 1:
											goto IL_2B8;
										case 2:
											num = 0;
											continue;
										case 3:
											num3 = (double)this.Sheet.ExportCell.RowHeightList[num4.ToString()];
											num = 16;
											continue;
										case 4:
											goto IL_5CC;
										case 5:
											goto IL_401;
										case 6:
											num5--;
											result.ᜆ = (ushort)Math.Round((num6 + num7) * 1024.0 / num7);
											num = 8;
											continue;
										case 7:
											if (num6 < 0.0)
											{
												num = 19;
												continue;
											}
											goto IL_2B8;
										case 8:
											goto IL_480;
										case 9:
											num4--;
											result.ᜈ = (ushort)Math.Round((num8 + num3) * 256.0 / num3);
											num = 4;
											continue;
										case 10:
											switch ((1 == 1) ? 1 : 0)
											{
											case 0:
											case 2:
												goto IL_57D;
											default:
												if (false)
												{
												}
												num = 15;
												continue;
											}
											break;
										case 11:
											if (num2 >= 0)
											{
												num = 2;
												continue;
											}
											goto IL_43B;
										case 12:
											goto IL_58E;
										case 13:
											goto IL_2EB;
										case 14:
											if (this.Sheet.ExportCell.FontList.Count == 0)
											{
												num = 17;
												continue;
											}
											this.Sheet.ExportCell.FontList[0].AssignTo(ref font, out black);
											num = 13;
											continue;
										case 15:
											if (num8 < 0.0)
											{
												num = 9;
												continue;
											}
											goto IL_5CC;
										case 16:
											goto IL_233;
										case 17:
											font = spr\u2059.ᜀ();
											num = 22;
											continue;
										case 18:
											if (num6 < 0.0)
											{
												goto IL_57D;
											}
											goto IL_480;
										case 19:
											num = 18;
											continue;
										case 20:
											if (this.Sheet.ExportCell.RowHeightList.Contains(num4.ToString()))
											{
												num = 3;
												continue;
											}
											goto IL_3A5;
										case 21:
											goto IL_233;
										case 22:
											goto IL_2EB;
										case 23:
											try
											{
												Font font2;
												num7 = (double)((float)((int)this.Sheet.ExportCell.ColWidthList[num2]) * graphics.MeasureString(HyperlinksCollectionEditor.b("ᘥ", a_), font2).Width);
												goto IL_401;
											}
											finally
											{
												Font font2;
												font2.Dispose();
											}
											goto IL_3A5;
										case 24:
										{
											Font font2 = spr\u2059.ᜀ();
											num = 23;
											continue;
										}
										case 25:
											goto IL_604;
										case 26:
											if (num8 < 0.0)
											{
												num = 10;
												continue;
											}
											goto IL_58E;
										}
										break;
										IL_233:
										num4++;
										num8 -= num3;
										num = 26;
										continue;
										IL_2B8:
										num2 = num5 - (int)this.Sheet.StartDataCol;
										num = 11;
										continue;
										IL_2EB:
										int num9 = 0;
										int num10 = 0;
										CellPicture cellPicture;
										cellPicture.GetMeasurements(ref num10, ref num9);
										num8 = (double)num10;
										num6 = (double)num9;
										num8 = Math.Round(num8 * (double)(this.ᜆ / 100));
										num6 = Math.Round(num6 * (double)(this.ᜆ / 100));
										num5 = this.ᜂ;
										num7 = 0.0;
										num3 = 0.0;
										num = 1;
										continue;
										IL_3A5:
										num3 = this.Sheet.DefRowHeight * 20.0;
										num = 21;
										continue;
										IL_401:
										num5++;
										num6 -= num7;
										num = 7;
										continue;
										IL_43B:
										num7 = (double)((float)(this.Sheet.DefColWidth - 1) * graphics.MeasureString(HyperlinksCollectionEditor.b("ᘥ", a_), font).Width + 1f);
										num = 5;
										continue;
										IL_480:
										result.ᜁ = Math.Min((ushort)this.ᜂ, (ushort)num5);
										result.ᜅ = Math.Max((ushort)this.ᜂ, (ushort)num5);
										num4 = this.ᜃ;
										num8 *= 15.0;
										num = 12;
										continue;
										IL_57D:
										num = 6;
										continue;
										IL_58E:
										num = 20;
										continue;
										IL_5CC:
										result.ᜃ = Math.Min((ushort)this.ᜃ, (ushort)num4);
										result.ᜇ = Math.Max((ushort)this.ᜃ, (ushort)num4);
										num = 25;
									}
								}
								IL_604:
								return result;
							}
							finally
							{
								Graphics graphics;
								graphics.Dispose();
							}
							goto IL_60D;
						}
						break;
						IL_60D:
						num2 = 0;
						if (true)
						{
						}
						num = 0;
					}
				}
				return result;
				IL_100:
				return result;
			}
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0007D538 File Offset: 0x0007C538
		public void SaveToXmlFile(XMLFile File, string Section)
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

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0007D574 File Offset: 0x0007C574
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

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0007D5B0 File Offset: 0x0007C5B0
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

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0007D5EC File Offset: 0x0007C5EC
		[Browsable(false)]
		public CellImages Images
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
				return base.Collection as CellImages;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0007D634 File Offset: 0x0007C634
		[Browsable(false)]
		public int PictureIndex
		{
			get
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_1AD:
					if (!(base.Collection.Holder is WorkSheet))
					{
						goto IL_A4;
					}
					num = 15;
					break;
				default:
					if (false)
					{
					}
					goto IL_7E;
				}
				int num2;
				CellPictures cellPictures;
				WorkSheet workSheet;
				for (;;)
				{
					IL_28:
					switch (num)
					{
					case 0:
						if (base.Collection is CellImages)
						{
							num = 9;
							continue;
						}
						return num2;
					case 1:
						if (base.Collection.Holder != null)
						{
							num = 19;
							continue;
						}
						return num2;
					case 2:
						cellPictures = (base.Collection.Holder as CellExport).Pictures;
						num = 4;
						continue;
					case 3:
						goto IL_19D;
					case 4:
						goto IL_239;
					case 5:
						num2 = -1;
						num = 7;
						continue;
					case 6:
						num = 0;
						continue;
					case 7:
						goto IL_131;
					case 8:
						num = 17;
						continue;
					case 9:
						num = 1;
						continue;
					case 10:
						if (workSheet.ExportCell != null)
						{
							num = 18;
							continue;
						}
						goto IL_A4;
					case 11:
						if (base.Collection != null)
						{
							num = 6;
							continue;
						}
						return num2;
					case 12:
						goto IL_213;
					case 13:
						goto IL_1AD;
					case 14:
						if (cellPictures != null)
						{
							num = 8;
							continue;
						}
						return num2;
					case 15:
						workSheet = (base.Collection.Holder as WorkSheet);
						num = 10;
						continue;
					case 16:
						if (base.Collection.Holder is CellExport)
						{
							num = 2;
							continue;
						}
						num = 13;
						continue;
					case 17:
						if (true)
						{
						}
						if (!cellPictures.Find(this.ᜄ, ref num2))
						{
							num = 5;
							continue;
						}
						num2++;
						num = 3;
						continue;
					case 18:
						cellPictures = workSheet.ExportCell.Pictures;
						num = 12;
						continue;
					case 19:
						num = 16;
						continue;
					}
					goto IL_7E;
				}
				IL_131:
				IL_19D:
				return num2;
				IL_213:
				IL_239:
				goto IL_A4;
				IL_7E:
				num2 = -1;
				cellPictures = null;
				workSheet = null;
				num = 11;
				goto IL_28;
				IL_A4:
				num = 14;
				goto IL_28;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x0007D880 File Offset: 0x0007C880
		// (set) Token: 0x06000BEE RID: 3054 RVA: 0x0007D8C4 File Offset: 0x0007C8C4
		[Description("Defines the horizontal position of the image in the result Excel file.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(0)]
		public int Column
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
				for (;;)
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_3E;
						case 1:
							this.ᜂ = value;
							num = 0;
							continue;
						}
						if (value == this.ᜂ)
						{
							break;
						}
						num = 1;
					}
					IL_40:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					break;
					IL_3E:
					goto IL_40;
				}
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0007D940 File Offset: 0x0007C940
		// (set) Token: 0x06000BF0 RID: 3056 RVA: 0x0007D984 File Offset: 0x0007C984
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Defines the vertical position of the image in the result Excel file.")]
		public int Row
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
				return this.ᜃ;
			}
			set
			{
				for (;;)
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_46;
						case 2:
							this.ᜃ = value;
							num = 1;
							continue;
						}
						if (true)
						{
						}
						if (value == this.ᜃ)
						{
							break;
						}
						num = 2;
					}
					IL_48:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_68;
					}
					IL_46:
					goto IL_48;
				}
				IL_68:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x0007DA00 File Offset: 0x0007CA00
		// (set) Token: 0x06000BF2 RID: 3058 RVA: 0x0007DA44 File Offset: 0x0007CA44
		[Description("Defines the name of the picture that the image uses.")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Editor(typeof(PictureNameEditor), typeof(UITypeEditor))]
		public string PictureName
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
				return this.ᜄ;
			}
			set
			{
				for (;;)
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							this.ᜄ = value;
							num = 2;
							continue;
						case 2:
							goto IL_4B;
						}
						if (true)
						{
						}
						if (!(value != this.ᜄ))
						{
							break;
						}
						num = 1;
					}
					IL_4D:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_6D;
					}
					IL_4B:
					goto IL_4D;
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0007DAC4 File Offset: 0x0007CAC4
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x0007DB08 File Offset: 0x0007CB08
		[Description("Defines the title of the image that would be displayed in the result Excel file.")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string Title
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
			set
			{
				for (;;)
				{
					if (true)
					{
					}
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.ᜅ = value;
							this.SetName(value);
							num = 1;
							continue;
						case 1:
							goto IL_52;
						}
						if (!(value != this.ᜅ))
						{
							break;
						}
						num = 0;
					}
					IL_54:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_74;
					}
					IL_52:
					goto IL_54;
				}
				IL_74:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0007DB90 File Offset: 0x0007CB90
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x0007DBD4 File Offset: 0x0007CBD4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(100)]
		[Description("Defines zooming ratio for the image in the result Excel file in percentage wise.")]
		public int Zoom
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
				return this.ᜆ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2D;
						default:
							if (false)
							{
							}
							this.ᜆ = value;
							num = 2;
							continue;
						}
						break;
					case 2:
						return;
					}
					goto IL_1C;
					IL_2D:
					num = 1;
					continue;
					IL_1C:
					if (true)
					{
					}
					if (value != this.ᜆ)
					{
						goto IL_2D;
					}
					break;
				}
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0007DC50 File Offset: 0x0007CC50
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x0007DC94 File Offset: 0x0007CC94
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public CellExport ExportCELLExport
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
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2D;
						default:
							if (false)
							{
							}
							this.ᜁ = value;
							num = 0;
							continue;
						}
						break;
					}
					goto IL_24;
					IL_2D:
					num = 1;
					continue;
					IL_24:
					if (value != this.ᜁ)
					{
						goto IL_2D;
					}
					break;
				}
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x0007DD10 File Offset: 0x0007CD10
		[Browsable(false)]
		public WorkSheet Sheet
		{
			get
			{
				int num = 9;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 6;
						continue;
					case 1:
						num = 13;
						continue;
					case 2:
						goto IL_182;
					case 3:
						num = 7;
						continue;
					case 4:
						if (true)
						{
						}
						num = 11;
						continue;
					case 5:
						if (base.Collection.Holder is CellExport)
						{
							num = 1;
							continue;
						}
						goto IL_1CF;
					case 6:
						goto IL_10E;
					case 7:
						if (base.Collection.Holder is WorkSheet)
						{
							num = 10;
							continue;
						}
						num = 5;
						continue;
					case 8:
						num = 12;
						continue;
					case 10:
						goto IL_1CD;
					case 11:
						if (base.Collection.Holder != null)
						{
							num = 3;
							continue;
						}
						goto IL_1CF;
					case 12:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_10E;
						default:
							if (false)
							{
							}
							if ((base.Collection.Holder as CellExport).Sheets.Count == 1)
							{
								num = 2;
								continue;
							}
							goto IL_1CF;
						}
						break;
					case 13:
						if ((base.Collection.Holder as CellExport).Sheets != null)
						{
							num = 8;
							continue;
						}
						goto IL_1CF;
					}
					if (base.Collection != null)
					{
						num = 0;
						continue;
					}
					goto IL_1CF;
					IL_10E:
					if (!(base.Collection is CellImages))
					{
						goto IL_1CF;
					}
					num = 4;
				}
				IL_182:
				return (base.Collection.Holder as CellExport).Sheets[0];
				IL_1CD:
				return base.Collection.Holder as WorkSheet;
				IL_1CF:
				return null;
			}
		}

		// Token: 0x0400091E RID: 2334
		private const int ᜀ = 17;

		// Token: 0x0400091F RID: 2335
		private CellExport ᜁ;

		// Token: 0x04000920 RID: 2336
		private float \u2460\u008C\u0096ª;

		// Token: 0x04000921 RID: 2337
		private int ᜂ;

		// Token: 0x04000922 RID: 2338
		private int ᜃ;

		// Token: 0x04000923 RID: 2339
		private string ᜄ = string.Empty;

		// Token: 0x04000924 RID: 2340
		private string ᜅ = string.Empty;

		// Token: 0x04000925 RID: 2341
		private int ᜆ = 100;
	}
}
