using System;
using System.ComponentModel;
using System.Drawing.Design;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.PropEditors;
using Spire.DataExport.TypeConverters;
using Spire.DataExport.Utils;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001A9 RID: 425
	[TypeConverter(typeof(CollectionTypeConverter))]
	public class Chart : CustomItem, ICloneable
	{
		// Token: 0x06000BA9 RID: 2985 RVA: 0x0007A9D4 File Offset: 0x000799D4
		public Chart()
		{
			this.ᜆ = new ChartSeriesList(this);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0007AA40 File Offset: 0x00079A40
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
			return new Chart
			{
				AutoColor = this.AutoColor,
				CategoryLabels = this.CategoryLabels,
				CategoryLabelsType = this.CategoryLabelsType,
				CategoryLabelsColumn = this.CategoryLabelsColumn,
				LegendPlacement = this.LegendPlacement,
				Position = this.Position,
				Series = this.Series,
				ShowLegend = this.ShowLegend,
				Style = this.Style,
				Title = this.Title,
				DataRangeSheet = this.DataRangeSheet
			};
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0007AB08 File Offset: 0x00079B08
		internal override void InitCollectionItem()
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

		// Token: 0x06000BAC RID: 2988 RVA: 0x0007AB44 File Offset: 0x00079B44
		internal spr\u1DCA ᜀ()
		{
			ChartPlacement placement;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_189:
				placement = this.ᜅ.AutoPosition.Placement;
				num = 3;
				break;
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
					goto IL_6F;
				}
				break;
			}
			spr\u1DCA result;
			int num2;
			for (;;)
			{
				IL_2C:
				switch (num)
				{
				case 0:
					if (this.WorkSheet != null)
					{
						num = 2;
						continue;
					}
					goto IL_3CC;
				case 1:
					goto IL_2DA;
				case 2:
				{
					ChartPositionType positionType = this.ᜅ.PositionType;
					num = 4;
					continue;
				}
				case 3:
					switch (placement)
					{
					case ChartPlacement.Bottom:
						result.ᜃ = (ushort)(Math.Max(this.WorkSheet.ExportCell.BoundSheetList.ᜁ(num2).ᜂ() + this.ᜅ.AutoPosition.Top, 0) + 1);
						result.ᜁ = (ushort)Math.Max(this.WorkSheet.ExportCell.BoundSheetList.ᜁ(num2).ᜄ() + this.ᜅ.AutoPosition.Left, 0);
						result.ᜇ = (ushort)Math.Max((int)result.ᜃ + this.ᜅ.AutoPosition.Height, 0);
						result.ᜅ = (ushort)Math.Max((int)result.ᜁ + this.ᜅ.AutoPosition.Width, 0);
						num = 13;
						continue;
					case ChartPlacement.Right:
						result.ᜃ = (ushort)Math.Max(this.ᜅ.AutoPosition.Top, 0);
						result.ᜁ = (ushort)(Math.Max(this.WorkSheet.ExportCell.BoundSheetList.ᜁ(num2).ᜆ() + this.ᜅ.AutoPosition.Left, 0) + 1);
						result.ᜇ = (ushort)Math.Max((int)result.ᜃ + this.ᜅ.AutoPosition.Height, 0);
						result.ᜅ = (ushort)Math.Max((int)result.ᜁ + this.ᜅ.AutoPosition.Width, 0);
						num = 5;
						continue;
					default:
						num = 8;
						continue;
					}
					break;
				case 4:
				{
					ChartPositionType positionType;
					switch (positionType)
					{
					case ChartPositionType.Auto:
						num = 9;
						continue;
					case ChartPositionType.Custom:
						result.ᜃ = (ushort)(this.ᜅ.CustomPosition.Y1 - 1);
						result.ᜁ = (ushort)(this.ᜅ.CustomPosition.X1 - 1);
						result.ᜇ = (ushort)(this.ᜅ.CustomPosition.Y2 - 1);
						result.ᜅ = (ushort)(this.ᜅ.CustomPosition.X2 - 1);
						num = 10;
						continue;
					default:
						num = 7;
						continue;
					}
					break;
				}
				case 5:
					goto IL_292;
				case 6:
					goto IL_39D;
				case 7:
					num = 11;
					continue;
				case 8:
					num = 1;
					continue;
				case 9:
					if (this.WorkSheet.ExportCell != null)
					{
						num = 12;
						continue;
					}
					goto IL_3CC;
				case 10:
					goto IL_353;
				case 11:
					goto IL_1D1;
				case 12:
					num2 = this.WorkSheet.ExportCell.BoundSheetList.ᜀ(this.WorkSheet.Index);
					if (true)
					{
					}
					num = 14;
					continue;
				case 13:
					goto IL_184;
				case 14:
					if (num2 > -1)
					{
						num = 6;
						continue;
					}
					goto IL_3CC;
				}
				goto IL_6F;
			}
			IL_184:
			IL_1D1:
			IL_292:
			IL_2DA:
			IL_353:
			goto IL_3CC;
			IL_39D:
			goto IL_189;
			IL_3CC:
			result.ᜀ = 0;
			result.ᜂ = 0;
			result.ᜄ = 0;
			result.ᜆ = 0;
			result.ᜈ = 0;
			return result;
			IL_6F:
			result.ᜃ = 0;
			result.ᜁ = 0;
			result.ᜇ = 0;
			result.ᜅ = 0;
			num2 = 0;
			num = 0;
			goto IL_2C;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0007AF48 File Offset: 0x00079F48
		public void SaveToXmlFile(XMLFile File, string Section)
		{
			int a_ = 17;
			switch (0)
			{
			default:
				for (;;)
				{
					for (;;)
					{
						File.WriteValue(Section, HyperlinksCollectionEditor.b("氬娮䔰尲瘴堶唸吺似", a_), this.ᜀ.ToString());
						string key = HyperlinksCollectionEditor.b("愬䨮嘰嘲嬴匶椸场尼尾⑀⹂⁄⥆㵈", a_);
						int num = (int)this.ᜄ;
						File.WriteValue(Section, key, num.ToString());
						File.WriteValue(Section, HyperlinksCollectionEditor.b("縬䜮帰䐲礴制常帺匼嬾", a_), this.ᜇ.ToString());
						string key2 = HyperlinksCollectionEditor.b("縬嬮䠰弲倴", a_);
						int num2 = (int)this.ᜈ;
						File.WriteValue(Section, key2, num2.ToString());
						File.WriteValue(Section, HyperlinksCollectionEditor.b("礬䘮䔰弲倴", a_), this.ᜉ);
						File.WriteValue(Section, HyperlinksCollectionEditor.b("椬丮䔰刲朴嘶圸尺堼氾⥀♂⁄㍆", a_), this.ᜊ);
						string key3 = HyperlinksCollectionEditor.b("測丮䔰嘲刴堶䬸䈺焼帾⍀♂⥄㑆ᵈ㉊㵌⩎", a_);
						int num3 = (int)this.ᜂ;
						File.WriteValue(Section, key3, num3.ToString());
						File.WriteValue(Section, HyperlinksCollectionEditor.b("測丮䔰嘲刴堶䬸䈺焼帾⍀♂⥄㑆ੈ⑊⅌㩎㱐㵒", a_), this.ᜃ);
						this.ᜁ.SaveToXmlFile(File, Section);
						this.ᜅ.SaveToXmlFile(File, Section);
						int num4 = 0;
						int num5 = 0;
						for (;;)
						{
							switch (num5)
							{
							case 0:
								goto IL_15E;
							case 1:
								goto IL_15E;
							case 2:
								goto IL_184;
							case 3:
								if (num4 >= this.ᜆ.Count)
								{
									num5 = 2;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									this.ᜆ[num4].SaveToXmlFile(File, Section + HyperlinksCollectionEditor.b("爬簮琰愲簴父樸携", a_) + num4.ToString());
									num4++;
									if (true)
									{
									}
									num5 = 1;
									continue;
								}
								break;
							}
							break;
							IL_15E:
							num5 = 3;
						}
					}
				}
				IL_184:
				File.SaveToFile();
				return;
			}
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0007B148 File Offset: 0x0007A148
		public void LoadFromXmlFile(XMLFile File, string Section)
		{
			int a_ = 11;
			switch (0)
			{
			default:
				for (;;)
				{
					IL_4C:
					int num;
					Array array;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_1C3:
						num = 0;
						break;
					default:
						if (false)
						{
						}
						this.ᜆ.Clear();
						this.ᜀ = Convert.ToBoolean(File.ReadValue(Section, HyperlinksCollectionEditor.b("昦尨弪䈬氮帰弲娴䔶", a_), true.ToString()));
						this.ᜄ = (ChartLegendPlacement)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("欦䰨䰪䠬䄮唰挲头嘶娸帺值娾⽀㝂", a_), 3.ToString()));
						this.ᜇ = Convert.ToBoolean(File.ReadValue(Section, HyperlinksCollectionEditor.b("琦䄨䐪娬挮吰吲倴夶崸", a_), true.ToString()));
						this.ᜈ = (ChartStyle)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("琦崨刪䄬䨮", a_), 0.ToString()));
						this.ᜉ = File.ReadValue(Section, HyperlinksCollectionEditor.b("猦䀨弪䄬䨮", a_), string.Empty);
						this.ᜊ = File.ReadValue(Section, HyperlinksCollectionEditor.b("挦䠨弪䰬紮倰崲刴制樸区堼娾㕀", a_), string.Empty);
						this.ᜂ = (RangeType)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("搦䠨弪䠬䠮帰䄲䰴笶堸夺堼匾㉀ᝂ㱄㝆ⱈ", a_), 0.ToString()));
						this.ᜃ = File.ReadValue(Section, HyperlinksCollectionEditor.b("搦䠨弪䠬䠮帰䄲䰴笶堸夺堼匾㉀B⩄⭆㱈♊⍌", a_), string.Empty);
						array = null;
						File.ReadSections(ref array);
						num = 5;
						break;
					}
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
						{
							string[] array2 = array as string[];
							string text = Section + HyperlinksCollectionEditor.b("砦稨渪缬昮琰怲樴", a_);
							string[] array3 = array2;
							num2 = 0;
							num = 7;
							continue;
						}
						case 1:
							goto IL_27E;
						case 2:
						{
							string text;
							string text2;
							if (text2.Length >= text.Length)
							{
								num = 4;
								continue;
							}
							goto IL_1D1;
						}
						case 3:
						{
							string text2;
							this.ᜆ.Add(new ChartSeries()).LoadFromXmlFile(File, text2);
							num = 9;
							continue;
						}
						case 4:
						{
							string text;
							string text2;
							string strB = text2.Substring(0, text.Length);
							num = 10;
							continue;
						}
						case 5:
							goto IL_1BD;
						case 6:
						{
							if (true)
							{
							}
							string[] array3;
							if (num2 >= array3.Length)
							{
								num = 1;
								continue;
							}
							string text2 = array3[num2];
							num = 2;
							continue;
						}
						case 7:
							goto IL_256;
						case 8:
							goto IL_256;
						case 9:
							goto IL_1D1;
						case 10:
						{
							string text;
							string strB;
							if (string.Compare(text, strB, true) == 0)
							{
								num = 3;
								continue;
							}
							goto IL_1D1;
						}
						}
						goto IL_4C;
						IL_1D1:
						num2++;
						num = 8;
						continue;
						IL_256:
						num = 6;
					}
					IL_1BD:
					if (array != null)
					{
						goto IL_1C3;
					}
					break;
				}
				IL_27E:
				this.ᜁ.LoadFromXmlFile(File, Section);
				this.ᜅ.LoadFromXmlFile(File, Section);
				return;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0007B44C File Offset: 0x0007A44C
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
				return ItemType.Chart;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0007B488 File Offset: 0x0007A488
		[Browsable(false)]
		public WorkSheet WorkSheet
		{
			get
			{
				for (;;)
				{
					int num = 13;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if ((base.Collection.Holder as CellExport).Sheets.Count == 1)
							{
								num = 11;
								continue;
							}
							goto IL_1CC;
						case 1:
							goto IL_1AE;
						case 2:
							if ((base.Collection.Holder as CellExport).Sheets != null)
							{
								num = 7;
								continue;
							}
							goto IL_1CC;
						case 3:
							if (base.Collection is Charts)
							{
								num = 9;
								continue;
							}
							goto IL_1CC;
						case 4:
							if (base.Collection.Holder != null)
							{
								num = 10;
								continue;
							}
							goto IL_1CC;
						case 5:
							num = 3;
							continue;
						case 6:
							if (base.Collection.Holder is CellExport)
							{
								num = 8;
								continue;
							}
							goto IL_1CC;
						case 7:
							if (true)
							{
							}
							num = 0;
							continue;
						case 8:
							num = 2;
							continue;
						case 9:
							num = 4;
							continue;
						case 10:
							num = 12;
							continue;
						case 11:
							goto IL_170;
						case 12:
							if (base.Collection.Holder is WorkSheet)
							{
								num = 1;
								continue;
							}
							num = 6;
							continue;
						}
						if (base.Collection == null)
						{
							goto IL_1CC;
						}
						num = 5;
					}
					IL_1AE:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_1C4;
					}
				}
				IL_170:
				return (base.Collection.Holder as CellExport).Sheets[0];
				IL_1C4:
				if (false)
				{
				}
				return base.Collection.Holder as WorkSheet;
				IL_1CC:
				return null;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x0007B664 File Offset: 0x0007A664
		// (set) Token: 0x06000BB2 RID: 2994 RVA: 0x0007B6A8 File Offset: 0x0007A6A8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Enables or disables the automatic defining colors of the chart series in the result Excel document.")]
		[DefaultValue(true)]
		public bool AutoColor
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
					switch (num)
					{
					case 0:
						return;
					case 2:
						this.ᜀ = value;
						goto IL_64;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_64:
						num = 0;
						break;
					default:
						if (false)
						{
						}
						if (value == this.ᜀ)
						{
							return;
						}
						num = 2;
						break;
					}
				}
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x0007B724 File Offset: 0x0007A724
		// (set) Token: 0x06000BB4 RID: 2996 RVA: 0x0007B768 File Offset: 0x0007A768
		[Description("Allows you to define the data range for the horizontal axis lables of the chart.")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataRange CategoryLabels
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
					switch (num)
					{
					case 0:
						return;
					case 1:
						this.ᜁ = value;
						num = 0;
						continue;
					case 2:
						if (true)
						{
						}
						break;
					case 3:
						goto IL_70;
					case 4:
						if (value != this.ᜁ)
						{
							num = 1;
							continue;
						}
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_70:
						num = 4;
						break;
					default:
						if (false)
						{
						}
						if (value == null)
						{
							return;
						}
						num = 3;
						break;
					}
				}
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x0007B800 File Offset: 0x0007A800
		// (set) Token: 0x06000BB6 RID: 2998 RVA: 0x0007B844 File Offset: 0x0007A844
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(RangeType.Column)]
		[Description("Defines the type of data range for marking the horizontal axis of the chart.")]
		public RangeType CategoryLabelsType
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
					switch (num)
					{
					case 0:
						this.ᜂ = value;
						goto IL_64;
					case 2:
						return;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_64:
						num = 2;
						break;
					default:
						if (false)
						{
						}
						if (value == this.ᜂ)
						{
							return;
						}
						num = 0;
						break;
					}
				}
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0007B8C0 File Offset: 0x0007A8C0
		// (set) Token: 0x06000BB8 RID: 3000 RVA: 0x0007B904 File Offset: 0x0007A904
		[Description("Defines the data column name for the horizontal axis lables of the chart.")]
		[DefaultValue("")]
		[Editor(typeof(ColumnNameEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string CategoryLabelsColumn
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
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.ᜃ = value;
						goto IL_69;
					case 2:
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_69:
						num = 2;
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (!(value != this.ᜃ))
						{
							return;
						}
						num = 1;
						break;
					}
				}
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0007B984 File Offset: 0x0007A984
		// (set) Token: 0x06000BBA RID: 3002 RVA: 0x0007B9C8 File Offset: 0x0007A9C8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(ChartLegendPlacement.Right)]
		[Description("Defines the position of the chart legend.")]
		public ChartLegendPlacement LegendPlacement
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
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.ᜄ = value;
						goto IL_5C;
					case 2:
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5C:
						if (true)
						{
						}
						num = 2;
						break;
					default:
						if (false)
						{
						}
						if (value == this.ᜄ)
						{
							return;
						}
						num = 1;
						break;
					}
				}
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0007BA44 File Offset: 0x0007AA44
		// (set) Token: 0x06000BBC RID: 3004 RVA: 0x0007BA88 File Offset: 0x0007AA88
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Description("Defines the chart position in the result Excel document.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ChartPosition Position
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
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_68;
					case 1:
						if (value != this.ᜅ)
						{
							num = 4;
							continue;
						}
						goto IL_83;
					case 2:
						goto IL_66;
					case 4:
						this.ᜅ = value;
						num = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_68:
						num = 1;
						break;
					default:
						if (false)
						{
						}
						if (value == null)
						{
							goto IL_83;
						}
						num = 0;
						break;
					}
				}
				IL_66:
				IL_83:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0007BB20 File Offset: 0x0007AB20
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x0007BB64 File Offset: 0x0007AB64
		[Description("Contains the collection of the chart series, which belongs to this chart.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(ChartSeriesListCollectionEditor), typeof(UITypeEditor))]
		public ChartSeriesList Series
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
				return this.ᜆ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜆ = value;
						goto IL_64;
					case 2:
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_64:
						num = 2;
						break;
					default:
						if (false)
						{
						}
						if (value == this.ᜆ)
						{
							return;
						}
						if (true)
						{
						}
						num = 0;
						break;
					}
				}
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x0007BBE0 File Offset: 0x0007ABE0
		// (set) Token: 0x06000BC0 RID: 3008 RVA: 0x0007BC24 File Offset: 0x0007AC24
		[Editor(typeof(WorkSheetNameEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the worksheet of the data range.")]
		public string DataRangeSheet
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
				return this.ᜊ;
			}
			set
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
						return;
					case 1:
						this.ᜊ = value;
						goto IL_69;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_69:
						num = 0;
						break;
					default:
						if (false)
						{
						}
						if (!(value != this.ᜊ))
						{
							return;
						}
						num = 1;
						break;
					}
				}
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0007BCA4 File Offset: 0x0007ACA4
		// (set) Token: 0x06000BC2 RID: 3010 RVA: 0x0007BCE8 File Offset: 0x0007ACE8
		[Description("Enables or disables displaying of the chart legend.")]
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool ShowLegend
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
				return this.ᜇ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_64;
					case 2:
						this.ᜇ = value;
						goto IL_5C;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5C:
						num = 1;
						break;
					default:
						if (false)
						{
						}
						if (value == this.ᜇ)
						{
							goto IL_66;
						}
						num = 2;
						break;
					}
				}
				IL_64:
				IL_66:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x0007BD64 File Offset: 0x0007AD64
		// (set) Token: 0x06000BC4 RID: 3012 RVA: 0x0007BDA8 File Offset: 0x0007ADA8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Defines the chart style.")]
		[DefaultValue(ChartStyle.Column)]
		public ChartStyle Style
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
				return this.ᜈ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						return;
					case 2:
						this.ᜈ = value;
						goto IL_64;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_64:
						num = 1;
						break;
					default:
						if (false)
						{
						}
						if (value == this.ᜈ)
						{
							return;
						}
						num = 2;
						break;
					}
				}
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0007BE24 File Offset: 0x0007AE24
		// (set) Token: 0x06000BC6 RID: 3014 RVA: 0x0007BE68 File Offset: 0x0007AE68
		[DefaultValue("")]
		[Description("Defines the chart title in the result Excel document.")]
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
				return this.ᜉ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						return;
					case 2:
						this.ᜉ = value;
						this.SetName(value);
						goto IL_68;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_68:
						if (true)
						{
						}
						num = 1;
						break;
					default:
						if (false)
						{
						}
						if (!(value != this.ᜉ))
						{
							return;
						}
						num = 2;
						break;
					}
				}
			}
		}

		// Token: 0x040008F4 RID: 2292
		private bool ᜀ = true;

		// Token: 0x040008F5 RID: 2293
		private string[] \u2609\u009F\u0091\u00AD;

		// Token: 0x040008F6 RID: 2294
		private DataRange ᜁ = new DataRange();

		// Token: 0x040008F7 RID: 2295
		private RangeType ᜂ;

		// Token: 0x040008F8 RID: 2296
		private string ᜃ = string.Empty;

		// Token: 0x040008F9 RID: 2297
		private ChartLegendPlacement ᜄ = ChartLegendPlacement.Right;

		// Token: 0x040008FA RID: 2298
		private ChartPosition ᜅ = new ChartPosition();

		// Token: 0x040008FB RID: 2299
		private ChartSeriesList ᜆ;

		// Token: 0x040008FC RID: 2300
		private long[] \u2593\u0093\u0084\u00A3;

		// Token: 0x040008FD RID: 2301
		private bool ᜇ = true;

		// Token: 0x040008FE RID: 2302
		private float[] \u25D9\u00A6\u00A6\u0087;

		// Token: 0x040008FF RID: 2303
		private ChartStyle ᜈ;

		// Token: 0x04000900 RID: 2304
		private string ᜉ = string.Empty;

		// Token: 0x04000901 RID: 2305
		private string ᜊ = string.Empty;
	}
}
