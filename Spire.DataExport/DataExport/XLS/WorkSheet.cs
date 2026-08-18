using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.Common;
using Spire.DataExport.Delegates;
using Spire.DataExport.EventArgs;
using Spire.DataExport.PropEditors;
using Spire.DataExport.TypeConverters;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001D0 RID: 464
	[TypeConverter(typeof(CollectionTypeConverter))]
	public class WorkSheet : CollectionItem, ICloneable
	{
		// Token: 0x06000D85 RID: 3461 RVA: 0x00095388 File Offset: 0x00094388
		public WorkSheet()
		{
			this.\u1719 = new FormatsExport(this);
			this.ᜋ = new ColumnFormats(this);
			this.ᜌ = new ItemStyles(this);
			this.\u171D = new CellHyperlinks(this);
			this.\u171E = new CellNotes(this);
			this.\u171F = new Charts(this);
			this.ᜠ = new CellImages(this);
			this.ᜡ = new Cells(this);
			this.ᜢ = new MergedCellList(this);
			this.ᜀ(this.DataSource, this.SQLCommand, this.DataTable, this.ListView);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x000954D4 File Offset: 0x000944D4
		private void ᜀ(ExportSource A_0, IDbCommand A_1, DataTable A_2, ListView A_3)
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
			this.ᜊ.AggregateFormat.ExportSource = A_0;
			this.ᜊ.TitlesFormat.ExportSource = A_0;
			this.ᜊ.CustomDataFormat.ExportSource = A_0;
			this.ᜊ.FooterFormat.ExportSource = A_0;
			this.ᜊ.HeaderFormat.ExportSource = A_0;
			this.ᜊ.HyperlinkFormat.ExportSource = A_0;
			this.ᜊ.AggregateFormat.Command = A_1;
			this.ᜊ.TitlesFormat.Command = A_1;
			this.ᜊ.CustomDataFormat.Command = A_1;
			this.ᜊ.FooterFormat.Command = A_1;
			this.ᜊ.HeaderFormat.Command = A_1;
			this.ᜊ.HyperlinkFormat.Command = A_1;
			this.ᜊ.AggregateFormat.DataTable = A_2;
			this.ᜊ.TitlesFormat.DataTable = A_2;
			this.ᜊ.CustomDataFormat.DataTable = A_2;
			this.ᜊ.FooterFormat.DataTable = A_2;
			this.ᜊ.HeaderFormat.DataTable = A_2;
			this.ᜊ.HyperlinkFormat.DataTable = A_2;
			this.ᜊ.AggregateFormat.ListView = A_3;
			this.ᜊ.TitlesFormat.ListView = A_3;
			this.ᜊ.CustomDataFormat.ListView = A_3;
			this.ᜊ.FooterFormat.ListView = A_3;
			this.ᜊ.HeaderFormat.ListView = A_3;
			this.ᜊ.HyperlinkFormat.ListView = A_3;
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x000956B0 File Offset: 0x000946B0
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
			return new WorkSheet
			{
				AutoFitColWidth = this.AutoFitColWidth,
				AutoFitTitleWidth = this.AutoFitTitleWidth,
				SheetName = this.SheetName,
				Options = this.Options,
				ColumnFormats = this.ColumnFormats,
				ItemStyles = this.ItemStyles,
				ItemType = this.ItemType,
				Hyperlinks = this.Hyperlinks,
				Notes = this.Notes,
				Charts = this.Charts,
				Images = this.Images,
				Cells = this.Cells,
				MergedCells = this.MergedCells,
				Background = this.Background,
				DataSource = this.DataSource,
				SQLCommand = this.SQLCommand,
				ListView = this.ListView,
				Columns = this.Columns,
				HeaderRows = this.HeaderRows,
				StartDataCol = this.StartDataCol,
				FooterRows = this.FooterRows,
				Header = this.Header,
				Titles = this.Titles,
				Footer = this.Footer,
				FormatsExport = this.FormatsExport,
				CustomFormats = this.CustomFormats,
				ColumnsWidth = this.ColumnsWidth,
				AllowTitles = this.AllowTitles,
				MaxRows = this.MaxRows,
				SkipRows = this.SkipRows
			};
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0009585C File Offset: 0x0009485C
		internal override void InitCollectionItem()
		{
			int a_ = 5;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜉ = string.Format(HyperlinksCollectionEditor.b("爠䬢䀤䈦崨ପ嘬Ἦ䰰", a_), this.ᜀ.Sheets.Count + 1);
					if (true)
					{
					}
					num = 8;
					continue;
				case 1:
					if (this.ᜉ.Length == 0)
					{
						num = 0;
						continue;
					}
					goto IL_A0;
				case 3:
					if (base.Collection is WorkSheets)
					{
						num = 12;
						continue;
					}
					goto IL_A0;
				case 4:
					num = 9;
					continue;
				case 5:
					if (base.Collection is WorkSheets)
					{
						num = 4;
						continue;
					}
					return;
				case 6:
					if (base.Collection != null)
					{
						num = 7;
						continue;
					}
					return;
				case 7:
					num = 5;
					continue;
				case 8:
					goto IL_A0;
				case 9:
					if (this.ᜀ != null)
					{
						num = 11;
						continue;
					}
					return;
				case 10:
					this.ᜀ = (base.Collection.Holder as CellExport);
					goto IL_207;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_207;
					default:
						if (false)
						{
						}
						this.ᜁ = new ColumnsExport(this, new NormalFunc(this.ᜀ.NormalString));
						this.ᜂ = new RowExport(this.ᜁ, this.\u1719, this.ᜮ, null);
						num = 13;
						continue;
					}
					break;
				case 12:
					num = 15;
					continue;
				case 13:
					return;
				case 14:
					num = 3;
					continue;
				case 15:
					if (base.Collection.Holder is CellExport)
					{
						num = 10;
						continue;
					}
					goto IL_A0;
				}
				if (base.Collection != null)
				{
					num = 14;
					continue;
				}
				IL_A0:
				num = 6;
				continue;
				IL_207:
				num = 1;
			}
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00095A9C File Offset: 0x00094A9C
		internal ushort ᜀ(string A_0, CellFormat A_1)
		{
			int a_ = 11;
			switch (0)
			{
			default:
			{
				ushort result;
				for (;;)
				{
					result = 15;
					int num = 3;
					for (;;)
					{
						sprḓ sprḓ;
						int num2;
						spr\u17ED spr_u17ED;
						int num3;
						CellFont cellFont;
						switch (num)
						{
						case 0:
							goto IL_241;
						case 1:
							goto IL_113;
						case 2:
							goto IL_1EC;
						case 3:
							if (string.Compare(A_0, HyperlinksCollectionEditor.b("怦䰨䔪䠬崮倰弲", a_)) == 0)
							{
								num = 25;
								continue;
							}
							goto IL_199;
						case 4:
							this.ᜀ.LastTextFormat++;
							sprḓ.ᜀ((ushort)this.ᜀ.LastTextFormat);
							this.ᜀ.TextFormatList.ᜀ(sprḓ);
							num = 16;
							continue;
						case 5:
							goto IL_D4;
						case 6:
							goto IL_23C;
						case 7:
							A_0 = HyperlinksCollectionEditor.b("怦䰨䔪䠬崮倰弲", a_);
							num = 19;
							continue;
						case 8:
							if (num2 == -1)
							{
								num = 1;
								continue;
							}
							result = (ushort)num2;
							num = 28;
							continue;
						case 9:
							goto IL_457;
						case 10:
							spr_u17ED = new spr\u17ED(A_1.Font, sprḓ);
							spr_u17ED.ᜀ(A_1.Borders.Clone() as Borders);
							spr_u17ED.ᜀ(A_1.FillStyle.Clone() as FillType);
							spr_u17ED.ᜀ(A_1.Alignment.Clone() as TextAlignment);
							spr_u17ED.ᜀ(A_1.WordWrap);
							num = 15;
							continue;
						case 11:
							if (num3 == -1)
							{
								num = 4;
								continue;
							}
							sprḓ = null;
							spr_u17ED.ᜀ(this.ᜀ.TextFormatList.ᜀ(num3));
							num = 2;
							continue;
						case 12:
							if (true)
							{
							}
							this.ᜀ.LastFont++;
							cellFont.FontIndex = (int)((ushort)this.ᜀ.LastFont);
							this.ᜀ.FontList.Add(cellFont);
							num = 5;
							continue;
						case 13:
							return result;
						case 14:
							if (sprḓ == null)
							{
								goto IL_1EC;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_113;
							default:
								if (false)
								{
								}
								num = 21;
								continue;
							}
							break;
						case 15:
							goto IL_241;
						case 16:
							goto IL_1EC;
						case 17:
							if (num3 == -1)
							{
								num = 12;
								continue;
							}
							cellFont = null;
							spr_u17ED.ᜀ(this.ᜀ.FontList[num3]);
							num = 22;
							continue;
						case 18:
							if (A_1 != null)
							{
								num = 10;
								continue;
							}
							spr_u17ED = new spr\u17ED(null, sprḓ);
							num = 0;
							continue;
						case 19:
							goto IL_283;
						case 20:
							sprḓ = new sprḓ();
							sprḓ.ᜀ(A_0);
							num = 9;
							continue;
						case 21:
							num3 = this.ᜀ.TextFormatList.ᜁ(sprḓ.ᜁ());
							num = 11;
							continue;
						case 22:
							goto IL_D4;
						case 23:
							if (string.Compare(A_0, HyperlinksCollectionEditor.b("怦䰨䔪䠬崮倰弲", a_)) != 0)
							{
								num = 20;
								continue;
							}
							goto IL_457;
						case 24:
							if (cellFont != null)
							{
								num = 29;
								continue;
							}
							goto IL_D4;
						case 25:
							num = 26;
							continue;
						case 26:
							if (A_1 == null)
							{
								num = 13;
								continue;
							}
							goto IL_199;
						case 27:
							if (A_0.Length == 0)
							{
								num = 7;
								continue;
							}
							goto IL_283;
						case 28:
							goto IL_2F6;
						case 29:
							num3 = this.ᜀ.FontList.ListIndexByFont(cellFont);
							num = 17;
							continue;
						}
						break;
						IL_D4:
						num = 14;
						continue;
						IL_113:
						num = 24;
						continue;
						IL_199:
						num = 27;
						continue;
						IL_1EC:
						this.ᜀ.LastFormat++;
						result = (ushort)this.ᜀ.LastFormat;
						spr_u17ED.ᜀ((ushort)this.ᜀ.LastFormat);
						this.ᜀ.FormatList.ᜁ(spr_u17ED);
						num = 6;
						continue;
						IL_241:
						cellFont = spr_u17ED.ᜅ();
						num2 = this.ᜀ.FormatList.ᜀ(spr_u17ED);
						num3 = 0;
						num = 8;
						continue;
						IL_283:
						sprḓ = null;
						num = 23;
						continue;
						IL_457:
						spr_u17ED = null;
						num = 18;
					}
				}
				IL_23C:
				IL_2F6:
				return result;
			}
			}
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00095F60 File Offset: 0x00094F60
		protected int GetXF(int ColIndex)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				int num;
				for (;;)
				{
					string name = this.ColumnsExport[ColIndex].Name;
					num = this.ᜀ.FormatFieldList.ᜁ(this.Index, name);
					int num2 = 7;
					for (;;)
					{
						string text;
						switch (num2)
						{
						case 0:
							if (this.ᜀ.ᜩ != null)
							{
								num2 = 9;
								continue;
							}
							return num;
						case 1:
							goto IL_2F8;
						case 2:
						{
							CellItemType u170D;
							switch (u170D)
							{
							case CellItemType.Col:
								num = (int)this.ᜀ.FormatColRowList.ᜀ(this.Index, ColIndex);
								num2 = 11;
								continue;
							case CellItemType.Row:
								num = (int)this.ᜀ.FormatColRowList.ᜀ(this.Index, this.RecordCounter % this.ᜌ.Count * 1000 + ColIndex);
								num2 = 3;
								continue;
							default:
								num2 = 10;
								continue;
							}
							break;
						}
						case 3:
							goto IL_2F8;
						case 4:
							goto IL_2C0;
						case 5:
							try
							{
								num2 = 4;
								for (;;)
								{
									spr\u17ED spr_u17ED;
									switch (num2)
									{
									case 0:
										text = this.ColumnsExport[ColIndex].Format;
										num2 = 3;
										continue;
									case 1:
										goto IL_2B4;
									case 2:
										goto IL_195;
									case 3:
										goto IL_29F;
									case 5:
										goto IL_195;
									case 6:
										text = spr_u17ED.ᜂ().ᜁ();
										num2 = 5;
										continue;
									case 7:
										goto IL_24B;
									case 8:
										if (spr_u17ED.ᜂ() == null)
										{
											goto IL_195;
										}
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_24B;
										default:
											if (false)
											{
											}
											num2 = 6;
											continue;
										}
										break;
									case 9:
										if (text.Length == 0)
										{
											num2 = 0;
											continue;
										}
										goto IL_29F;
									}
									if (num == 15)
									{
										num2 = 7;
										continue;
									}
									spr_u17ED = this.ᜀ.FormatList.ᜁ((ushort)num);
									CellFormat cellFormat;
									cellFormat.ᜀ(spr_u17ED);
									num2 = 8;
									continue;
									IL_195:
									DataParamsEventArgs dataParamsEventArgs = new DataParamsEventArgs(this.Index, ColIndex + (int)this.\u1714, this.ᜃ, cellFormat, text);
									this.ᜀ.ᜀ(this, dataParamsEventArgs);
									text = dataParamsEventArgs.FormatText;
									num2 = 9;
									continue;
									IL_24B:
									cellFormat = (this.Options.CustomDataFormat.Clone() as CellFormat);
									text = HyperlinksCollectionEditor.b("栮吰崲倴䔶堸场", a_);
									num2 = 2;
									continue;
									IL_29F:
									num = (int)this.ᜀ(text, cellFormat);
									num2 = 1;
								}
								IL_2B4:
								return num;
							}
							finally
							{
								CellFormat cellFormat;
								cellFormat.Dispose();
							}
							goto IL_2C0;
						case 6:
						{
							if (true)
							{
							}
							CellItemType u170D = this.\u170D;
							num2 = 2;
							continue;
						}
						case 7:
							if (num == 15)
							{
								num2 = 4;
								continue;
							}
							goto IL_2F8;
						case 8:
							if (this.ᜀ.FormatColRowList.ᜁ() > 0)
							{
								num2 = 6;
								continue;
							}
							goto IL_2F8;
						case 9:
						{
							CellFormat cellFormat = new CellFormat();
							num2 = 5;
							continue;
						}
						case 10:
							num2 = 1;
							continue;
						case 11:
							goto IL_2F8;
						}
						break;
						IL_2C0:
						num2 = 8;
						continue;
						IL_2F8:
						text = string.Empty;
						num2 = 0;
					}
				}
				return num;
			}
			}
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x000962F4 File Offset: 0x000952F4
		protected void AddColumnToFormatList(int ColIndex)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					string name = this.ColumnsExport[ColIndex].Name;
					string format = this.ColumnsExport[ColIndex].Format;
					int num = this.ColumnFormats.IndexByName(name);
					ushort num2 = 0;
					int num3 = 0;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							if (num > -1)
							{
								num3 = 7;
								continue;
							}
							num2 = this.ᜀ(format, this.Options.CustomDataFormat);
							num3 = 8;
							continue;
						case 1:
							goto IL_132;
						case 2:
						{
							sprḚ sprḚ = new sprḚ();
							sprḚ.ᜀ(name);
							sprḚ.ᜀ(this.Index);
							sprḚ.ᜀ(this.ᜀ.FormatList.ᜀ(this.ᜀ.FormatList.ᜀ(num2)));
							this.ᜀ.FormatFieldList.ᜀ(sprḚ);
							num3 = 5;
							continue;
						}
						case 3:
							if (num == -1)
							{
								num3 = 2;
								continue;
							}
							return;
						case 4:
							if (num2 == 15)
							{
								num3 = 6;
								continue;
							}
							num = this.ᜀ.FormatFieldList.ᜀ(this.Index, name);
							if (true)
							{
							}
							num3 = 3;
							continue;
						case 5:
							return;
						case 6:
							return;
						case 7:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_132;
							default:
								if (false)
								{
								}
								num2 = this.ᜀ(format, this.ColumnFormats[num]);
								num3 = 1;
								continue;
							}
							break;
						case 8:
							goto IL_CA;
						}
						break;
						IL_CA:
						num3 = 4;
						continue;
						IL_132:
						goto IL_CA;
					}
				}
				return;
			}
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x000964C0 File Offset: 0x000954C0
		protected void AddStylesToFormatList()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int count = this.ItemStyles.Count;
					int num = 7;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_306;
						case 1:
						{
							int num2;
							num2++;
							if (true)
							{
							}
							num = 15;
							continue;
						}
						case 2:
							return;
						case 3:
						{
							int num2;
							if (num2 >= this.ColumnsExport.Count)
							{
								num = 11;
								continue;
							}
							string a_ = this.ColumnsExport[num2].Format;
							ushort a_2 = this.ᜀ(a_, this.ItemStyles[num2 % count]);
							sprᤑ sprᤑ = new sprᤑ();
							sprᤑ.ᜁ(num2);
							int num3 = this.ᜀ.FormatList.ᜀ(a_2);
							sprᤑ.ᜀ(this.ᜀ.FormatList.ᜀ(num3));
							sprᤑ.ᜀ(this.Index);
							this.ᜀ.FormatColRowList.ᜀ(sprᤑ);
							num2++;
							num = 0;
							continue;
						}
						case 4:
						{
							int num2;
							if (num2 >= this.ItemStyles.Count)
							{
								num = 9;
								continue;
							}
							int num3 = 0;
							num = 8;
							continue;
						}
						case 5:
							goto IL_22F;
						case 6:
							goto IL_1BD;
						case 7:
						{
							if (count == 0)
							{
								num = 2;
								continue;
							}
							string a_ = string.Empty;
							int num2 = 0;
							int num3 = 0;
							CellItemType u170D = this.\u170D;
							num = 14;
							continue;
						}
						case 8:
							goto IL_22F;
						case 9:
							return;
						case 10:
						{
							int num3;
							if (num3 >= this.ColumnsExport.Count)
							{
								num = 1;
								continue;
							}
							string a_ = this.ColumnsExport[num3].Format;
							int num2;
							ushort a_2 = this.ᜀ(a_, this.ItemStyles[num2]);
							sprᤑ sprᤑ = new sprᤑ();
							sprᤑ.ᜁ(num2 * 1000 + num3);
							int a_3 = this.ᜀ.FormatList.ᜀ(a_2);
							sprᤑ.ᜀ(this.ᜀ.FormatList.ᜀ(a_3));
							sprᤑ.ᜀ(this.Index);
							this.ᜀ.FormatColRowList.ᜀ(sprᤑ);
							num3++;
							num = 5;
							continue;
						}
						case 11:
							return;
						case 12:
							return;
						case 13:
							goto IL_1EC;
						case 14:
						{
							CellItemType u170D;
							switch (u170D)
							{
							case CellItemType.Col:
							{
								int num2 = 0;
								num = 13;
								continue;
							}
							case CellItemType.Row:
							{
								int num2 = 0;
								num = 6;
								continue;
							}
							default:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_306;
								default:
									if (false)
									{
									}
									num = 12;
									continue;
								}
								break;
							}
							break;
						}
						case 15:
							goto IL_1BD;
						}
						break;
						IL_1BD:
						num = 4;
						continue;
						IL_1EC:
						num = 3;
						continue;
						IL_306:
						goto IL_1EC;
						IL_22F:
						num = 10;
					}
				}
				return;
			}
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x000967D8 File Offset: 0x000957D8
		protected void HeaderFooter(StringListCollection HeaderFooter, ushort Limit, HeaderFooterParamsEventHandler EventHandler, CellFormat Fmt)
		{
			int a_ = 13;
			switch (0)
			{
			default:
				for (;;)
				{
					ushort num = 0;
					int num2 = 18;
					for (;;)
					{
						int num4;
						string text;
						switch (num2)
						{
						case 0:
							goto IL_3A4;
						case 1:
							num = Limit;
							num2 = 30;
							continue;
						case 2:
						{
							this.ᜃ++;
							int num3;
							num3++;
							num2 = 28;
							continue;
						}
						case 3:
							goto IL_229;
						case 4:
							goto IL_3A4;
						case 5:
						{
							HeaderFooterParamsEventArgs headerFooterParamsEventArgs = new HeaderFooterParamsEventArgs(this.Index, num4, this.ᜃ, Fmt, text);
							EventHandler(this, headerFooterParamsEventArgs);
							text = headerFooterParamsEventArgs.Str;
							num2 = 21;
							continue;
						}
						case 6:
						{
							int num5;
							this.ᜀ.ᜀ(num5, this.ᜃ);
							num2 = 26;
							continue;
						}
						case 7:
							if (this.ᜆ)
							{
								num2 = 6;
								continue;
							}
							goto IL_2CC;
						case 8:
							num2 = 27;
							continue;
						case 9:
							goto IL_189;
						case 10:
							if (text.Length > 0)
							{
								num2 = 25;
								continue;
							}
							goto IL_172;
						case 11:
						{
							if (num == 0)
							{
								num2 = 19;
								continue;
							}
							string text2 = string.Empty;
							text = string.Empty;
							int num3 = 0;
							num2 = 17;
							continue;
						}
						case 12:
							return;
						case 13:
						{
							string[] array;
							if (array.Length <= num4)
							{
								goto IL_3A4;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2BB;
							default:
								if (false)
								{
								}
								num2 = 29;
								continue;
							}
							break;
						}
						case 14:
							goto IL_229;
						case 15:
							if (HeaderFooter.Count > 0)
							{
								num2 = 8;
								continue;
							}
							goto IL_13C;
						case 16:
						{
							text = string.Empty;
							string text2;
							string[] array = text2.Split(new char[]
							{
								'\t'
							});
							num2 = 13;
							continue;
						}
						case 17:
							goto IL_24F;
						case 18:
							if (Limit > 0)
							{
								num2 = 1;
								continue;
							}
							num = (ushort)HeaderFooter.Count;
							num2 = 9;
							continue;
						case 19:
							return;
						case 20:
							if (num4 > 255)
							{
								num2 = 2;
								continue;
							}
							num2 = 15;
							continue;
						case 21:
							goto IL_1B3;
						case 22:
							if (EventHandler != null)
							{
								num2 = 5;
								continue;
							}
							goto IL_1B3;
						case 23:
						{
							int num3;
							if (num3 >= (int)num)
							{
								num2 = 12;
								continue;
							}
							try
							{
								string text2 = HeaderFooter[num3];
								goto IL_2B8;
							}
							catch
							{
								string text2 = string.Empty;
								goto IL_2B8;
							}
							goto IL_229;
							IL_2B8:
							num4 = 0;
							goto IL_2BB;
						}
						case 24:
							goto IL_172;
						case 25:
						{
							int num5 = (int)this.ᜀ(HyperlinksCollectionEditor.b("渨个䌬䨮䌰刲头", a_), Fmt);
							this.ᜀ.BoundSheetList.ᜀ(this.Index, this.ᜃ, num4);
							this.ᜀ.ᜀ((ushort)this.ᜃ, (ushort)num4, (ushort)num5, text);
							num2 = 7;
							continue;
						}
						case 26:
							goto IL_2CC;
						case 27:
						{
							int num3;
							if (HeaderFooter.Count >= num3 + 1)
							{
								num2 = 16;
								continue;
							}
							goto IL_13C;
						}
						case 28:
							goto IL_24F;
						case 29:
						{
							string[] array;
							text = array[num4];
							num2 = 0;
							continue;
						}
						case 30:
							if (true)
							{
							}
							goto IL_189;
						}
						break;
						IL_13C:
						text = string.Empty;
						num2 = 4;
						continue;
						IL_172:
						num4++;
						num2 = 14;
						continue;
						IL_189:
						num2 = 11;
						continue;
						IL_1B3:
						num2 = 10;
						continue;
						IL_229:
						num2 = 20;
						continue;
						IL_24F:
						num2 = 23;
						continue;
						IL_2BB:
						num2 = 3;
						continue;
						IL_2CC:
						text = string.Empty;
						num2 = 24;
						continue;
						IL_3A4:
						num2 = 22;
					}
				}
				return;
			}
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00096BFC File Offset: 0x00095BFC
		internal void ᜀ(bool A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜃ = 0;
					this.ᜅ = 0;
					this.ᜄ = 0;
					this.ᜮ = new CultureInfo(this.\u1719.CultureName);
					this.ᜁ.Clear();
					this.ᜁ.Fill(A_0);
					this.ᜂ.Clear();
					this.ᜂ.Index.Clear();
					this.ᜂ.Culture = this.ᜮ;
					int num = 0;
					int num2 = 4;
					for (;;)
					{
						IEnumerator enumerator;
						int num4;
						int num5;
						switch (num2)
						{
						case 0:
						{
							int num3;
							if (num3 > -1)
							{
								num2 = 8;
								continue;
							}
							goto IL_1F1;
						}
						case 1:
							num2 = 5;
							continue;
						case 2:
							if (this.\u170D != CellItemType.None)
							{
								num2 = 1;
								continue;
							}
							goto IL_1C2;
						case 3:
						{
							int num3;
							this.ColumnsExport[num].Width = this.ᜋ[num3].Width;
							if (true)
							{
							}
							num2 = 20;
							continue;
						}
						case 4:
							goto IL_349;
						case 5:
							if (this.ᜌ.Count == 0)
							{
								num2 = 18;
								continue;
							}
							goto IL_16D;
						case 6:
							goto IL_402;
						case 7:
							try
							{
								num2 = 1;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										goto IL_2C1;
									case 2:
										goto IL_2C1;
									case 3:
									{
										if (!enumerator.MoveNext())
										{
											num2 = 2;
											continue;
										}
										CellImage cellImage = (CellImage)enumerator.Current;
										num2 = 6;
										continue;
									}
									case 4:
										goto IL_2CD;
									case 5:
										for (;;)
										{
											this.ᜆ = true;
											switch ((1 == 1) ? 1 : 0)
											{
											case 0:
											case 2:
												break;
											default:
												goto IL_2AD;
											}
										}
										IL_2AD:
										if (false)
										{
										}
										num2 = 0;
										continue;
									case 6:
									{
										CellImage cellImage;
										if (this.ᜀ.Pictures.Find(cellImage.PictureName, ref num4))
										{
											num2 = 5;
											continue;
										}
										break;
									}
									}
									IL_273:
									num2 = 3;
									continue;
									goto IL_273;
									IL_2C1:
									num2 = 4;
								}
								IL_2CD:
								return;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable = enumerator as IDisposable;
									num2 = 2;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											goto IL_318;
										case 1:
											disposable.Dispose();
											num2 = 0;
											continue;
										case 2:
											if (disposable != null)
											{
												num2 = 1;
												continue;
											}
											goto IL_31A;
										}
										break;
									}
								}
								IL_318:
								IL_31A:;
							}
							goto IL_31B;
						case 8:
							num2 = 15;
							continue;
						case 9:
							goto IL_31B;
						case 10:
							goto IL_16D;
						case 11:
							if (this.ᜇ)
							{
								num2 = 17;
								continue;
							}
							goto IL_402;
						case 12:
							num2 = 11;
							continue;
						case 13:
						{
							if (num >= this.ColumnsExport.Count)
							{
								num2 = 12;
								continue;
							}
							this.ᜂ.Add(this.ColumnsExport[num].Name, num);
							int num3 = this.ᜋ.IndexByName(this.ColumnsExport[num].Name);
							num2 = 0;
							continue;
						}
						case 14:
							if (num5 >= this.ColumnsExport.Count)
							{
								num2 = 10;
								continue;
							}
							this.AddColumnToFormatList(num5);
							num5++;
							num2 = 9;
							continue;
						case 15:
						{
							int num3;
							if (this.ᜋ[num3].Width > 0)
							{
								num2 = 3;
								continue;
							}
							goto IL_1F1;
						}
						case 16:
							goto IL_349;
						case 17:
							this.ᜁ.AutoCalcColWidth();
							num2 = 6;
							continue;
						case 18:
							goto IL_1C2;
						case 19:
							goto IL_31B;
						case 20:
							goto IL_1F1;
						}
						break;
						IL_16D:
						this.ᜆ = false;
						num4 = 0;
						enumerator = this.ᜠ.GetEnumerator();
						num2 = 7;
						continue;
						IL_1C2:
						num5 = 0;
						num2 = 19;
						continue;
						IL_1F1:
						num++;
						num2 = 16;
						continue;
						IL_31B:
						num2 = 14;
						continue;
						IL_349:
						num2 = 13;
						continue;
						IL_402:
						this.ᜀ(this.ᜀ.DataFormats.Currency, null);
						this.ᜀ(this.ᜀ.DataFormats.DateTime, null);
						this.ᜀ(this.ᜀ.DataFormats.Float, null);
						this.ᜀ(this.ᜀ.DataFormats.Integer, null);
						this.ᜀ(this.ᜀ.DataFormats.Time, null);
						this.AddStylesToFormatList();
						num2 = 2;
					}
				}
				return;
			}
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x000970C4 File Offset: 0x000960C4
		internal void ᜊ()
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
			this.ExportStage = XlsExportStage.Header;
			this.HeaderFooter(this.\u1716, (ushort)this.\u1713, this.ᜀ.ᜧ, this.ᜊ.HeaderFormat);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00097130 File Offset: 0x00096130
		protected void DoBeforeData()
		{
			int a_ = 11;
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
					{
						if (num2 >= (int)this.\u1714)
						{
							num = 1;
							continue;
						}
						CellFormat cellFormat = this.ᜊ.CustomDataFormat.Clone() as CellFormat;
						num = 5;
						continue;
					}
					case 1:
						return;
					case 3:
						return;
					case 4:
						goto IL_177;
					case 5:
						try
						{
							for (;;)
							{
								IL_AA:
								string text = string.Empty;
								CellFormat cellFormat;
								HeaderFooterParamsEventArgs headerFooterParamsEventArgs = new HeaderFooterParamsEventArgs(this.Index, num2, this.ᜃ, cellFormat, text);
								this.ᜀ.ᜂ(this, headerFooterParamsEventArgs);
								text = headerFooterParamsEventArgs.Str;
								num = 1;
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
										{
											ushort a_2 = this.ᜀ(HyperlinksCollectionEditor.b("怦䰨䔪䠬崮倰弲", a_), cellFormat);
											this.ᜀ.BoundSheetList.ᜀ(this.Index, this.ᜃ, num2);
											this.ᜀ.ᜀ((ushort)this.ᜃ, (ushort)num2, a_2, text);
											goto IL_141;
										}
										case 1:
											if (text.Length > 0)
											{
												num = 0;
												continue;
											}
											goto IL_14F;
										case 2:
											goto IL_15B;
										case 3:
											goto IL_14F;
										}
										goto IL_AA;
										IL_14F:
										num = 2;
										continue;
									}
									IL_141:
									num = 3;
								}
							}
							IL_15B:
							goto IL_19E;
						}
						finally
						{
							CellFormat cellFormat;
							cellFormat.Dispose();
						}
						goto IL_164;
						IL_19E:
						num2++;
						num = 6;
						continue;
					case 6:
						goto IL_177;
					}
					if (this.ᜀ.ᜨ == null)
					{
						num = 3;
						continue;
					}
					IL_164:
					num2 = 0;
					num = 4;
					continue;
					IL_177:
					num = 0;
				}
				return;
			}
			}
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00097324 File Offset: 0x00096324
		internal void ᜉ()
		{
			int a_ = 10;
			switch (0)
			{
			default:
				for (;;)
				{
					this.ExportStage = XlsExportStage.Caption;
					this.DoBeforeData();
					int num = 0;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_292;
						case 1:
							try
							{
								for (;;)
								{
									string caption = this.ColumnsExport[num].Caption;
									CellFormat cellFormat;
									TitleParamsEventArgs titleParamsEventArgs = new TitleParamsEventArgs(this.Index, num + (int)this.\u1714, cellFormat, caption);
									this.ᜀ.ᜀ(this, titleParamsEventArgs);
									caption = titleParamsEventArgs.Caption;
									ushort num3 = 0;
									num2 = 12;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											this.ᜀ.ᜁ(caption, (int)num3, num);
											switch ((1 == 1) ? 1 : 0)
											{
											case 0:
											case 2:
												goto IL_11F;
											default:
												if (false)
												{
												}
												num2 = 6;
												continue;
											}
											break;
										case 1:
											num2 = 7;
											continue;
										case 2:
											num3 = this.ᜀ(HyperlinksCollectionEditor.b("愥䴧䐩䤫尭儯帱", a_), cellFormat);
											num2 = 8;
											continue;
										case 3:
											this.ᜀ.ᜀ(caption, (int)num3, num);
											num2 = 11;
											continue;
										case 4:
											if (this.ᜈ)
											{
												num2 = 0;
												continue;
											}
											goto IL_26B;
										case 5:
											if (true)
											{
											}
											goto IL_139;
										case 6:
											goto IL_26B;
										case 7:
											if (!cellFormat.IsDefault())
											{
												num2 = 2;
												continue;
											}
											goto IL_11F;
										case 8:
											goto IL_139;
										case 9:
											goto IL_277;
										case 10:
											if (this.ᜇ)
											{
												num2 = 3;
												continue;
											}
											num2 = 4;
											continue;
										case 11:
											goto IL_26B;
										case 12:
											if (caption.Length > 0)
											{
												num2 = 1;
												continue;
											}
											goto IL_26B;
										}
										break;
										IL_11F:
										num3 = 15;
										num2 = 5;
										continue;
										IL_139:
										this.ᜀ.BoundSheetList.ᜀ(this.Index, this.ᜃ, num + (int)this.\u1714);
										this.ᜀ.ᜀ((ushort)this.ᜃ, (ushort)(num + (int)this.\u1714), num3, caption);
										num2 = 10;
										continue;
										IL_26B:
										num2 = 9;
									}
								}
								IL_277:;
							}
							finally
							{
								CellFormat cellFormat;
								cellFormat.Dispose();
							}
							num++;
							num2 = 2;
							continue;
						case 2:
							goto IL_292;
						case 3:
							this.ᜃ += (this.ᜦ ? 1 : 0);
							num2 = 4;
							continue;
						case 4:
							goto IL_2FC;
						case 5:
						{
							if (num >= this.ColumnsExport.Count)
							{
								num2 = 6;
								continue;
							}
							CellFormat cellFormat = this.ᜊ.TitlesFormat.Clone() as CellFormat;
							num2 = 1;
							continue;
						}
						case 6:
							num2 = 3;
							continue;
						}
						break;
						IL_292:
						num2 = 5;
					}
				}
				IL_2FC:
				this.ᜃ += (this.ᜧ ? 1 : 0);
				return;
			}
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00097668 File Offset: 0x00096668
		internal void ᜆ()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_7B:
					NormalFunc a_ = new NormalFunc(this.ᜀ.NormalString);
					int num = 0;
					int num2 = 16;
					for (;;)
					{
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
							ExportSource dataSource;
							switch (num2)
							{
							case 0:
								if (this.ᜁ[num].ColExportType == ColExportType.Time)
								{
									num2 = 6;
									continue;
								}
								goto IL_145;
							case 1:
								if (true)
								{
								}
								num2 = 0;
								continue;
							case 2:
							{
								ColumnExport columnExport;
								if (!spr\u2059.ᜂ.IsNull(columnExport.Number))
								{
									num2 = 9;
									continue;
								}
								goto IL_145;
							}
							case 3:
								num2 = 10;
								continue;
							case 4:
								return;
							case 5:
								if (this.ᜁ[num].ColExportType != ColExportType.DateTime)
								{
									num2 = 1;
									continue;
								}
								goto IL_28D;
							case 6:
								goto IL_28D;
							case 7:
								switch (dataSource)
								{
								case ExportSource.SqlCommand:
									num2 = 15;
									continue;
								case ExportSource.DataTable:
									num2 = 2;
									continue;
								case ExportSource.ListView:
								{
									object a_2 = this.ᜑ.Items[this.ᜄ + this.ᜩ];
									ColumnExport columnExport;
									this.ᜂ.ᜀ(columnExport.Name, a_2);
									num2 = 12;
									continue;
								}
								default:
									num2 = 3;
									continue;
								}
								break;
							case 8:
								goto IL_145;
							case 9:
							{
								ColumnExport columnExport;
								this.ᜂ.ᜀ(columnExport.Name, spr\u2059.ᜂ[columnExport.Number]);
								goto IL_C2;
							}
							case 10:
								goto IL_145;
							case 11:
								goto IL_145;
							case 12:
								goto IL_145;
							case 13:
								goto IL_22D;
							case 14:
							{
								ColumnExport columnExport;
								this.ᜂ.ᜀ(columnExport.Name, spr\u2059.ᜀ.GetValue(columnExport.Number));
								num2 = 8;
								continue;
							}
							case 15:
							{
								ColumnExport columnExport;
								if (!spr\u2059.ᜀ.IsDBNull(columnExport.Number))
								{
									num2 = 14;
									continue;
								}
								goto IL_145;
							}
							case 16:
								goto IL_22D;
							case 17:
							{
								if (num >= this.ᜁ.Count)
								{
									num2 = 4;
									continue;
								}
								ColumnExport columnExport = this.ᜁ[num];
								num2 = 5;
								continue;
							}
							}
							goto IL_7B;
							IL_145:
							string value = spr\u2059.ᜀ(this.ᜎ, spr\u2059.ᜀ, this.ᜑ, this.ᜁ, this.ᜮ, a_, num, this.ᜄ, this.ᜩ, false);
							this.ᜂ.SetValue(this.ᜁ[num].Name, value);
							num++;
							num2 = 13;
							continue;
							IL_22D:
							num2 = 17;
							continue;
							IL_28D:
							dataSource = this.DataSource;
							num2 = 7;
							continue;
						}
						}
						IL_C2:
						num2 = 11;
					}
				}
				return;
			}
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0009797C File Offset: 0x0009697C
		internal void ᜀ()
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

		// Token: 0x06000D94 RID: 3476 RVA: 0x000979B8 File Offset: 0x000969B8
		internal void ᜋ()
		{
			int a_ = 5;
			switch (0)
			{
			default:
				for (;;)
				{
					this.DoBeforeData();
					this.ExportStage = XlsExportStage.Data;
					int num = 0;
					int num2 = 35;
					for (;;)
					{
						DateTime d;
						TimeSpan timeSpan;
						ColExportType colExportType;
						switch (num2)
						{
						case 0:
						{
							string text;
							ushort num3;
							this.ᜀ.ᜀ(text, (int)num3, num);
							num2 = 11;
							continue;
						}
						case 1:
							goto IL_2B8;
						case 2:
							if (this.ᜀ.AutoFormula)
							{
								num2 = 15;
								continue;
							}
							goto IL_239;
						case 3:
							goto IL_1CB;
						case 4:
						{
							object obj;
							if (obj is DateTime)
							{
								num2 = 28;
								continue;
							}
							try
							{
								string text;
								d = DateTime.Parse(text, this.ᜮ);
								goto IL_572;
							}
							catch
							{
								d = DateTime.MinValue;
								goto IL_572;
							}
							goto IL_239;
						}
						case 5:
						{
							if (timeSpan.Days <= 60)
							{
								goto IL_115;
							}
							this.ᜀ.BoundSheetList.ᜀ(this.Index, this.ᜃ, num + (int)this.\u1714);
							ushort num3;
							this.ᜀ.ᜀ((ushort)this.ᜃ, (ushort)(num + (int)this.\u1714), num3, timeSpan.TotalDays);
							num2 = 19;
							continue;
						}
						case 6:
						{
							this.ᜀ.BoundSheetList.ᜀ(this.Index, this.ᜃ, num + (int)this.\u1714);
							ushort num3;
							this.ᜀ.ᜀ((ushort)this.ᜃ, (ushort)(num + (int)this.\u1714), num3, timeSpan.TotalDays - 1.0);
							num2 = 42;
							continue;
						}
						case 7:
						{
							string text;
							if (text != null)
							{
								num2 = 34;
								continue;
							}
							goto IL_239;
						}
						case 8:
							if (this.ᜇ)
							{
								num2 = 0;
								continue;
							}
							goto IL_47E;
						case 9:
							goto IL_176;
						case 10:
						{
							this.ᜀ.BoundSheetList.ᜀ(this.Index, this.ᜃ, num + (int)this.\u1714);
							string text;
							ushort num3;
							this.ᜀ.ᜀ((ushort)this.ᜃ, (ushort)(num + (int)this.\u1714), num3, text);
							num2 = 24;
							continue;
						}
						case 11:
							goto IL_47E;
						case 12:
						{
							if (timeSpan.Days > 1)
							{
								num2 = 29;
								continue;
							}
							this.ᜀ.BoundSheetList.ᜀ(this.Index, this.ᜃ, num + (int)this.\u1714);
							ushort num3;
							this.ᜀ.ᜀ((ushort)this.ᜃ, (ushort)(num + (int)this.\u1714), num3, timeSpan.TotalDays);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_115;
							default:
								if (false)
								{
								}
								num2 = 30;
								continue;
							}
							break;
						}
						case 13:
						{
							this.ᜀ.BoundSheetList.ᜀ(this.Index, this.ᜃ, num + (int)this.\u1714);
							string text;
							ushort num3;
							this.ᜀ.ᜀ(this, (ushort)this.ᜃ, (ushort)(num + (int)this.\u1714), num3, text);
							num2 = 25;
							continue;
						}
						case 14:
							switch (colExportType)
							{
							case ColExportType.Integer:
							case ColExportType.Bigint:
							case ColExportType.Float:
							case ColExportType.Currency:
							{
								double a_2 = 0.0;
								num2 = 16;
								continue;
							}
							case ColExportType.DateTime:
							case ColExportType.Time:
							{
								d = DateTime.MinValue;
								timeSpan = TimeSpan.Zero;
								object obj = this.ᜂ[num].OriginalValue;
								num2 = 31;
								continue;
							}
							case ColExportType.String:
							case ColExportType.Guid:
							case ColExportType.Binary:
							{
								this.ᜀ.BoundSheetList.ᜀ(this.Index, this.ᜃ, num + (int)this.\u1714);
								string text;
								ushort num3;
								this.ᜀ.ᜀ((ushort)this.ᜃ, (ushort)(num + (int)this.\u1714), num3, text);
								num2 = 9;
								continue;
							}
							case ColExportType.Boolean:
							{
								string text;
								bool a_3 = string.Compare(text, this.\u1719.BooleanTrue, true) == 0;
								this.ᜀ.BoundSheetList.ᜀ(this.Index, this.ᜃ, num + (int)this.\u1714);
								ushort num3;
								this.ᜀ.ᜀ((ushort)this.ᜃ, (ushort)(num + (int)this.\u1714), num3, a_3);
								num2 = 33;
								continue;
							}
							default:
								num2 = 26;
								continue;
							}
							break;
						case 15:
							num2 = 7;
							continue;
						case 16:
						{
							double a_2;
							try
							{
								string text;
								a_2 = double.Parse(text, this.ᜮ);
								goto IL_500;
							}
							catch
							{
								a_2 = 0.0;
								goto IL_500;
							}
							goto IL_572;
							IL_500:
							this.ᜀ.BoundSheetList.ᜀ(this.Index, this.ᜃ, num + (int)this.\u1714);
							ushort num3;
							this.ᜀ.ᜀ((ushort)this.ᜃ, (ushort)(num + (int)this.\u1714), num3, a_2);
							num2 = 41;
							continue;
						}
						case 17:
						{
							string text;
							if (text.Length == 0)
							{
								num2 = 21;
								continue;
							}
							num2 = 32;
							continue;
						}
						case 18:
						{
							object obj;
							timeSpan = (TimeSpan)obj;
							num2 = 3;
							continue;
						}
						case 19:
							goto IL_176;
						case 20:
							goto IL_176;
						case 21:
						{
							this.ᜀ.BoundSheetList.ᜀ(this.Index, this.ᜃ, num + (int)this.\u1714);
							ushort num3;
							this.ᜀ.ᜁ((ushort)this.ᜃ, (ushort)(num + (int)this.\u1714), num3);
							num2 = 20;
							continue;
						}
						case 22:
							goto IL_1CB;
						case 23:
							goto IL_2E1;
						case 24:
							goto IL_176;
						case 25:
							goto IL_176;
						case 26:
							num2 = 43;
							continue;
						case 27:
							goto IL_7C7;
						case 28:
						{
							object obj;
							d = (DateTime)obj;
							num2 = 38;
							continue;
						}
						case 29:
							if (true)
							{
							}
							num2 = 5;
							continue;
						case 30:
							goto IL_176;
						case 31:
						{
							object obj;
							if (obj is TimeSpan)
							{
								num2 = 18;
								continue;
							}
							num2 = 4;
							continue;
						}
						case 32:
						{
							string text;
							if (string.Compare(text, this.\u1719.NullString) == 0)
							{
								num2 = 10;
								continue;
							}
							num2 = 2;
							continue;
						}
						case 33:
							goto IL_176;
						case 34:
							num2 = 36;
							continue;
						case 35:
							goto IL_2B8;
						case 36:
						{
							string text;
							if (text.StartsWith(HyperlinksCollectionEditor.b("ᰠ", a_)))
							{
								num2 = 13;
								continue;
							}
							goto IL_239;
						}
						case 37:
						{
							if (num >= this.ᜂ.Count)
							{
								num2 = 23;
								continue;
							}
							string text = this.ᜂ[num].Value;
							this.ᜭ.ᜀ(this.ᜂ[num].Name, this.ᜃ, num + (int)this.\u1714);
							TextEventArgs textEventArgs = new TextEventArgs(this.ᜃ, num, text);
							this.ᜀ.ᜀ(this, textEventArgs);
							XLSTextEventArgs xlstextEventArgs = new XLSTextEventArgs(this.Index, this.ᜃ, num, textEventArgs.Text);
							this.ᜀ.ᜀ(this, xlstextEventArgs);
							text = xlstextEventArgs.Text;
							ushort num3 = (ushort)this.GetXF(num);
							num2 = 17;
							continue;
						}
						case 38:
							goto IL_572;
						case 39:
							if (this.ᜆ)
							{
								num2 = 40;
								continue;
							}
							goto IL_7C7;
						case 40:
						{
							ushort num3;
							this.ᜀ.ᜀ((int)num3, this.ᜃ);
							num2 = 27;
							continue;
						}
						case 41:
							goto IL_176;
						case 42:
							goto IL_176;
						case 43:
							goto IL_176;
						}
						break;
						IL_115:
						num2 = 6;
						continue;
						IL_176:
						num2 = 8;
						continue;
						IL_1CB:
						num2 = 12;
						continue;
						IL_239:
						colExportType = this.ᜁ[num].ColExportType;
						num2 = 14;
						continue;
						IL_2B8:
						num2 = 37;
						continue;
						IL_47E:
						num2 = 39;
						continue;
						IL_572:
						timeSpan = d - spr\u1C2B.ᡞ;
						num2 = 22;
						continue;
						IL_7C7:
						num++;
						num2 = 1;
					}
				}
				IL_2E1:
				this.ᜃ++;
				return;
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0009825C File Offset: 0x0009725C
		internal void ᜈ()
		{
			int a_ = 10;
			switch (0)
			{
			default:
				for (;;)
				{
					bool flag = false;
					this.ᜁ.EmptyTags();
					int num = 0;
					int num2 = 30;
					for (;;)
					{
						int num3;
						ushort num4;
						int num5;
						switch (num2)
						{
						case 0:
							try
							{
								for (;;)
								{
									string text = HyperlinksCollectionEditor.b("愥䴧䐩䤫尭儯帱", a_);
									string text2 = string.Empty;
									CellFormat cellFormat;
									AggregateParamsEventArgs aggregateParamsEventArgs = new AggregateParamsEventArgs(this.Index, num3, cellFormat, text, text2);
									this.ᜀ.ᜀ(this, aggregateParamsEventArgs);
									text = aggregateParamsEventArgs.FormatText;
									text2 = aggregateParamsEventArgs.Value;
									num2 = 9;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											this.ᜀ.ᜀ(text2, (int)num4, num3);
											num2 = 8;
											continue;
										case 1:
										{
											bool flag2 = true;
											num2 = 10;
											continue;
										}
										case 2:
											text = HyperlinksCollectionEditor.b("愥䴧䐩䤫尭儯帱", a_);
											num4 = this.ᜀ(text, cellFormat);
											this.ᜀ.BoundSheetList.ᜀ(this.Index, this.ᜃ, num3 + (int)this.\u1714);
											this.ᜀ.ᜀ((ushort)this.ᜃ, (ushort)(num3 + (int)this.\u1714), num4, text2);
											num2 = 6;
											continue;
										case 3:
											goto IL_7D7;
										case 4:
											goto IL_7CB;
										case 5:
											this.ᜀ.ᜀ((int)num4, this.ᜃ);
											num2 = 4;
											continue;
										case 6:
										{
											bool flag2;
											if (!flag2)
											{
												num2 = 1;
												continue;
											}
											goto IL_784;
										}
										case 7:
											if (this.ᜇ)
											{
												num2 = 0;
												continue;
											}
											goto IL_6A7;
										case 8:
											goto IL_6A7;
										case 9:
											if (text2.Length > 0)
											{
												num2 = 2;
												continue;
											}
											goto IL_7CB;
										case 10:
											goto IL_784;
										case 11:
											if (this.ᜆ)
											{
												num2 = 5;
												continue;
											}
											goto IL_7CB;
										}
										break;
										IL_6A7:
										num2 = 11;
										continue;
										IL_784:
										num2 = 7;
										continue;
										IL_7CB:
										num2 = 3;
									}
								}
								IL_7D7:
								goto IL_238;
							}
							finally
							{
								CellFormat cellFormat;
								cellFormat.Dispose();
							}
							goto IL_7E4;
						case 1:
							if (num3 >= this.ᜁ.Count)
							{
								num2 = 4;
								continue;
							}
							num2 = 8;
							continue;
						case 2:
							goto IL_2EA;
						case 3:
							goto IL_192;
						case 4:
							num2 = 7;
							continue;
						case 5:
							goto IL_343;
						case 6:
							num2 = 17;
							continue;
						case 7:
						{
							bool flag2;
							if (flag2)
							{
								num2 = 10;
								continue;
							}
							return;
						}
						case 8:
							if (this.ᜁ[num3].Tag != 0)
							{
								num2 = 9;
								continue;
							}
							num2 = 11;
							continue;
						case 9:
						{
							CellFormat cellFormat = this.ᜊ.AggregateFormat.Clone() as CellFormat;
							num2 = 29;
							continue;
						}
						case 10:
							this.ᜃ++;
							num2 = 13;
							continue;
						case 11:
							if (this.ᜀ.ᜪ != null)
							{
								num2 = 24;
								continue;
							}
							goto IL_238;
						case 12:
						{
							bool flag2;
							if (!flag2)
							{
								if (true)
								{
								}
								num2 = 15;
								continue;
							}
							goto IL_238;
						}
						case 13:
							return;
						case 14:
							if (this.ᜋ[num].Aggregate != Aggregate.None)
							{
								num2 = 6;
								continue;
							}
							goto IL_2EA;
						case 15:
						{
							bool flag2 = true;
							num2 = 16;
							continue;
						}
						case 16:
							goto IL_238;
						case 17:
							if (!flag)
							{
								num2 = 25;
								continue;
							}
							goto IL_192;
						case 18:
							goto IL_57A;
						case 19:
							num2 = 31;
							continue;
						case 20:
							if (this.ᜆ)
							{
								num2 = 26;
								continue;
							}
							goto IL_57A;
						case 21:
						{
							this.ExportStage = XlsExportStage.Aggregate;
							bool flag2 = false;
							CellFormat cellFormat = null;
							string text = string.Empty;
							string text2 = string.Empty;
							num4 = 0;
							num3 = 0;
							num2 = 5;
							continue;
						}
						case 22:
							if (num >= this.ᜋ.Count)
							{
								num2 = 19;
								continue;
							}
							num5 = this.ᜁ.IndexOfName(this.ᜋ[num].FieldName);
							num2 = 27;
							continue;
						case 23:
							goto IL_343;
						case 24:
						{
							CellFormat cellFormat = this.ᜊ.AggregateFormat.Clone() as CellFormat;
							num2 = 0;
							continue;
						}
						case 25:
							flag = true;
							num2 = 3;
							continue;
						case 26:
							goto IL_7E4;
						case 27:
							if (num5 > -1)
							{
								num2 = 28;
								continue;
							}
							goto IL_2EA;
						case 28:
							num2 = 14;
							continue;
						case 29:
							try
							{
								for (;;)
								{
									string text = this.ᜁ[num3].Format;
									num2 = 7;
									for (;;)
									{
										CellFormat cellFormat;
										switch (num2)
										{
										case 0:
											if (text.Length == 0)
											{
												num2 = 1;
												continue;
											}
											goto IL_50F;
										case 1:
											text = HyperlinksCollectionEditor.b("愥䴧䐩䤫尭儯帱", a_);
											num2 = 3;
											continue;
										case 2:
											goto IL_561;
										case 3:
											goto IL_50F;
										case 4:
											goto IL_56D;
										case 5:
											goto IL_42C;
										case 6:
											goto IL_547;
										case 7:
											if (text.Length == 0)
											{
												num2 = 11;
												continue;
											}
											goto IL_42C;
										case 8:
											if (string.Compare(text, HyperlinksCollectionEditor.b("愥䴧䐩䤫尭儯帱", a_)) == 0)
											{
												num2 = 9;
												continue;
											}
											goto IL_547;
										case 9:
											num2 = 10;
											continue;
										case 10:
											if (!cellFormat.IsDefault())
											{
												num2 = 6;
												continue;
											}
											goto IL_561;
										case 11:
											text = HyperlinksCollectionEditor.b("愥䴧䐩䤫尭儯帱", a_);
											switch ((1 == 1) ? 1 : 0)
											{
											case 0:
											case 2:
												goto IL_513;
											default:
												if (false)
												{
												}
												num2 = 5;
												continue;
											}
											break;
										}
										break;
										IL_42C:
										string text2 = string.Empty;
										AggregateParamsEventArgs aggregateParamsEventArgs2 = new AggregateParamsEventArgs(this.Index, num3, cellFormat, text, text2);
										this.ᜀ.ᜀ(this, aggregateParamsEventArgs2);
										text = aggregateParamsEventArgs2.FormatText;
										text2 = aggregateParamsEventArgs2.Value;
										num2 = 0;
										continue;
										IL_513:
										num2 = 8;
										continue;
										IL_50F:
										num4 = 15;
										goto IL_513;
										IL_547:
										num4 = this.ᜀ(text, cellFormat);
										num2 = 2;
										continue;
										IL_561:
										num2 = 4;
									}
								}
								IL_56D:
								goto IL_F4;
							}
							finally
							{
								CellFormat cellFormat;
								cellFormat.Dispose();
							}
							goto IL_57A;
							IL_F4:
							this.ᜀ.BoundSheetList.ᜀ(this.Index, this.ᜃ, num3 + (int)this.\u1714);
							this.ᜀ.ᜀ((ushort)this.ᜃ, (ushort)(num3 + (int)this.\u1714), this.StartDataRow, (ushort)(this.ᜃ - 1), this.ᜋ[this.ᜁ[num3].Tag - 1].Aggregate, num4);
							num2 = 20;
							continue;
						case 30:
							goto IL_20A;
						case 31:
							if (flag)
							{
								num2 = 21;
								continue;
							}
							return;
						case 32:
							goto IL_20A;
						}
						break;
						IL_192:
						this.ᜁ[num5].Tag = num + 1;
						num2 = 2;
						continue;
						IL_20A:
						num2 = 22;
						continue;
						IL_238:
						num3++;
						num2 = 23;
						continue;
						IL_2EA:
						num++;
						num2 = 32;
						continue;
						IL_343:
						num2 = 1;
						continue;
						IL_57A:
						num2 = 12;
						continue;
						IL_7E4:
						this.ᜀ.ᜀ((int)num4, this.ᜃ);
						num2 = 18;
					}
				}
				return;
			}
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00098AA8 File Offset: 0x00097AA8
		internal void ᜂ()
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
			this.ExportStage = XlsExportStage.Footer;
			this.HeaderFooter(this.\u1718, (ushort)this.\u1715, this.ᜀ.ᜫ, this.ᜊ.FooterFormat);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00098B14 File Offset: 0x00097B14
		public void LoadFromXLS()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜋ.Clear();
					IEnumerator enumerator = this.ᜀ.ColumnFormats.ToArray(typeof(ColumnFormat)).GetEnumerator();
					int num = 0;
					for (;;)
					{
						IEnumerator enumerator2;
						IEnumerator enumerator3;
						IEnumerator enumerator4;
						switch (num)
						{
						case 0:
							try
							{
								num = 3;
								for (;;)
								{
									switch (num)
									{
									case 1:
										num = 2;
										continue;
									case 2:
										goto IL_484;
									case 4:
									{
										if (!enumerator.MoveNext())
										{
											num = 1;
											continue;
										}
										ColumnFormat item = (ColumnFormat)enumerator.Current;
										this.ᜋ.Add(item);
										num = 0;
										continue;
									}
									}
									IL_45E:
									num = 4;
									continue;
									goto IL_45E;
								}
								IL_484:
								goto IL_134;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable = enumerator as IDisposable;
									num = 0;
									for (;;)
									{
										switch (num)
										{
										case 0:
											if (disposable != null)
											{
												num = 2;
												continue;
											}
											goto IL_4D1;
										case 1:
											goto IL_4CF;
										case 2:
											disposable.Dispose();
											num = 1;
											continue;
										}
										break;
									}
								}
								IL_4CF:
								IL_4D1:;
							}
							goto Block_5;
						case 1:
							try
							{
								num = 4;
								for (;;)
								{
									switch (num)
									{
									case 1:
									{
										if (!enumerator2.MoveNext())
										{
											num = 3;
											continue;
										}
										CellNote item2 = (CellNote)enumerator2.Current;
										this.\u171E.Add(item2);
										num = 0;
										continue;
									}
									case 2:
										goto IL_7C7;
									case 3:
										num = 2;
										continue;
									}
									IL_7A1:
									num = 1;
									continue;
									goto IL_7A1;
								}
								IL_7C7:
								goto IL_313;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable2 = enumerator2 as IDisposable;
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_812;
										case 1:
											disposable2.Dispose();
											num = 0;
											continue;
										case 2:
											if (disposable2 != null)
											{
												num = 1;
												continue;
											}
											goto IL_814;
										}
										break;
									}
								}
								IL_812:
								IL_814:;
							}
							goto IL_815;
						case 2:
							try
							{
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 1:
										num = 4;
										continue;
									case 2:
									{
										if (!enumerator3.MoveNext())
										{
											num = 1;
											continue;
										}
										CellImage item3 = (CellImage)enumerator3.Current;
										this.ᜠ.Add(item3);
										num = 3;
										continue;
									}
									case 4:
										goto IL_1E6;
									}
									IL_1C0:
									num = 2;
									continue;
									goto IL_1C0;
								}
								IL_1E6:
								goto IL_815;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable3 = enumerator3 as IDisposable;
									num = 0;
									for (;;)
									{
										switch (num)
										{
										case 0:
											if (disposable3 != null)
											{
												goto IL_216;
											}
											goto IL_24F;
										case 1:
											switch ((1 == 1) ? 1 : 0)
											{
											case 0:
											case 2:
												goto IL_216;
											default:
												goto IL_247;
											}
											break;
										case 2:
											disposable3.Dispose();
											num = 1;
											continue;
										}
										break;
										IL_216:
										num = 2;
									}
								}
								IL_247:
								if (false)
								{
								}
								IL_24F:;
							}
							goto Block_3;
						case 3:
							goto IL_4D2;
						case 4:
							goto IL_593;
						case 5:
							goto IL_654;
						case 6:
							goto IL_250;
						case 7:
							try
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										num = 3;
										continue;
									case 1:
									{
										if (!enumerator4.MoveNext())
										{
											num = 0;
											continue;
										}
										Chart item4 = (Chart)enumerator4.Current;
										this.\u171F.Add(item4);
										num = 4;
										continue;
									}
									case 3:
										goto IL_E6;
									}
									IL_99:
									num = 1;
									continue;
									goto IL_99;
								}
								IL_E6:
								goto IL_717;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable4 = enumerator4 as IDisposable;
									num = 0;
									for (;;)
									{
										switch (num)
										{
										case 0:
											if (disposable4 != null)
											{
												num = 2;
												continue;
											}
											goto IL_133;
										case 1:
											goto IL_131;
										case 2:
											disposable4.Dispose();
											num = 1;
											continue;
										}
										break;
									}
								}
								IL_131:
								IL_133:;
							}
							goto IL_134;
						}
						break;
						IL_134:
						this.ᜌ.Clear();
						IEnumerator enumerator5 = this.ᜀ.ItemStyles.ToArray(typeof(StripStyle)).GetEnumerator();
						num = 3;
						continue;
						IL_313:
						this.\u171F.Clear();
						enumerator4 = this.ᜀ.Charts.ToArray(typeof(Chart)).GetEnumerator();
						num = 7;
						continue;
						Block_3:
						IEnumerator enumerator6;
						try
						{
							IL_250:
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_2C5;
								case 2:
								{
									if (!enumerator6.MoveNext())
									{
										num = 3;
										continue;
									}
									MergedCells item5 = (MergedCells)enumerator6.Current;
									this.ᜢ.Add(item5);
									num = 4;
									continue;
								}
								case 3:
									num = 0;
									continue;
								}
								IL_278:
								num = 2;
								continue;
								goto IL_278;
							}
							IL_2C5:
							goto IL_852;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable5 = enumerator6 as IDisposable;
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_310;
									case 1:
										if (disposable5 != null)
										{
											num = 2;
											continue;
										}
										goto IL_312;
									case 2:
										disposable5.Dispose();
										num = 0;
										continue;
									}
									break;
								}
							}
							IL_310:
							IL_312:;
						}
						goto IL_313;
						Block_5:
						try
						{
							IL_4D2:
							num = 3;
							for (;;)
							{
								switch (num)
								{
								case 0:
									num = 1;
									continue;
								case 1:
									goto IL_545;
								case 4:
								{
									if (!enumerator5.MoveNext())
									{
										num = 0;
										continue;
									}
									StripStyle item6 = (StripStyle)enumerator5.Current;
									this.ᜌ.Add(item6);
									num = 2;
									continue;
								}
								}
								IL_4FA:
								num = 4;
								continue;
								goto IL_4FA;
							}
							IL_545:
							goto IL_350;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable6 = enumerator5 as IDisposable;
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 0:
										if (disposable6 != null)
										{
											num = 1;
											continue;
										}
										goto IL_592;
									case 1:
										disposable6.Dispose();
										num = 2;
										continue;
									case 2:
										goto IL_590;
									}
									break;
								}
							}
							IL_590:
							IL_592:;
						}
						goto Block_6;
						IL_350:
						this.\u171D.Clear();
						IEnumerator enumerator7 = this.ᜀ.Hyperlinks.ToArray(typeof(CellHyperlink)).GetEnumerator();
						num = 4;
						continue;
						Block_7:
						IEnumerator enumerator8;
						try
						{
							IL_654:
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									num = 4;
									continue;
								case 3:
								{
									if (!enumerator8.MoveNext())
									{
										num = 0;
										continue;
									}
									Cell item7 = (Cell)enumerator8.Current;
									this.ᜡ.Add(item7);
									num = 2;
									continue;
								}
								case 4:
									goto IL_6C9;
								}
								IL_6A3:
								num = 3;
								continue;
								goto IL_6A3;
							}
							IL_6C9:
							goto IL_38D;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable7 = enumerator8 as IDisposable;
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										disposable7.Dispose();
										num = 1;
										continue;
									case 1:
										goto IL_714;
									case 2:
										if (disposable7 != null)
										{
											num = 0;
											continue;
										}
										goto IL_716;
									}
									break;
								}
							}
							IL_714:
							IL_716:;
						}
						goto IL_717;
						IL_38D:
						this.ᜢ.Clear();
						enumerator6 = this.ᜀ.MergedCells.ToArray(typeof(MergedCells)).GetEnumerator();
						num = 6;
						continue;
						Block_6:
						try
						{
							IL_593:
							num = 4;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_606;
								case 1:
									num = 0;
									continue;
								case 3:
								{
									if (!enumerator7.MoveNext())
									{
										num = 1;
										continue;
									}
									CellHyperlink item8 = (CellHyperlink)enumerator7.Current;
									this.\u171D.Add(item8);
									num = 2;
									continue;
								}
								}
								IL_5BB:
								num = 3;
								continue;
								goto IL_5BB;
							}
							IL_606:
							goto IL_3D4;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable8 = enumerator7 as IDisposable;
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 0:
										if (disposable8 != null)
										{
											num = 2;
											continue;
										}
										goto IL_653;
									case 1:
										goto IL_651;
									case 2:
										disposable8.Dispose();
										num = 1;
										continue;
									}
									break;
								}
							}
							IL_651:
							IL_653:;
						}
						goto Block_7;
						IL_3D4:
						this.\u171E.Clear();
						enumerator2 = this.ᜀ.Notes.ToArray(typeof(CellNote)).GetEnumerator();
						num = 1;
						continue;
						IL_717:
						this.ᜠ.Clear();
						enumerator3 = this.ᜀ.Images.ToArray(typeof(CellImage)).GetEnumerator();
						num = 2;
						continue;
						IL_815:
						this.ᜡ.Clear();
						enumerator8 = this.ᜀ.Cells.ToArray(typeof(Cell)).GetEnumerator();
						num = 5;
					}
				}
				IL_852:
				if (true)
				{
				}
				this.ᜇ = this.ᜀ.AutoFitColWidth;
				this.ᜈ = this.ᜀ.AutoFitTitleWidth;
				this.ᜉ = this.ᜀ.SheetOptions.SheetTitle;
				this.ᜊ = this.ᜀ.SheetOptions;
				this.\u170D = this.ᜀ.ItemType;
				this.ᜣ = this.ᜀ.Background;
				this.ᜎ = this.ᜀ.DataSource;
				this.ᜏ = this.ᜀ.SQLCommand;
				this.ᜐ = this.ᜀ.DataTable;
				this.ᜑ = this.ᜀ.ListView;
				this.\u1712 = this.ᜀ.Columns;
				this.\u1713 = this.ᜀ.HeaderRows;
				this.\u1714 = this.ᜀ.StartDataCol;
				this.\u1715 = this.ᜀ.FooterRows;
				this.\u1716 = this.ᜀ.Header;
				this.\u1717 = this.ᜀ.Titles;
				this.\u1718 = this.ᜀ.Footer;
				this.\u1719 = this.ᜀ.DataFormats;
				this.\u171A = this.ᜀ.CustomFormats;
				this.\u171B = this.ᜀ.ColumnsWidth;
				this.ᜦ = this.ᜀ.AddTitles;
				this.ᜧ = this.ᜀ.Options.InsertRowAfterTitle;
				this.ᜨ = this.ᜀ.MaxRows;
				this.ᜩ = this.ᜀ.SkipRows;
				this.ᜮ = this.ᜀ.Culture;
				this.ᜫ = this.ᜀ.DataExported;
				this.ᜉ = this.ᜀ.SheetName;
				this.ᜯ = this.ᜀ.ᜑ;
				this.\u171C = this.ᜀ.NotTruncatableColumns;
				return;
			}
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x000995D8 File Offset: 0x000985D8
		public void SaveToXLS()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜀ.ColumnFormats.Clear();
					IEnumerator enumerator = this.ᜋ.ToArray(typeof(ColumnFormat)).GetEnumerator();
					int num = 0;
					for (;;)
					{
						IEnumerator enumerator2;
						IEnumerator enumerator3;
						IEnumerator enumerator4;
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							try
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										if (!enumerator.MoveNext())
										{
											num = 2;
											continue;
										}
										ColumnFormat item = (ColumnFormat)enumerator.Current;
										this.ᜀ.ColumnFormats.Add(item);
										num = 3;
										continue;
									}
									case 2:
										num = 4;
										continue;
									case 4:
										goto IL_484;
									}
									IL_45E:
									num = 0;
									continue;
									goto IL_45E;
								}
								IL_484:
								goto IL_141;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable = enumerator as IDisposable;
									num = 0;
									for (;;)
									{
										switch (num)
										{
										case 0:
											if (disposable != null)
											{
												num = 1;
												continue;
											}
											goto IL_4D1;
										case 1:
											disposable.Dispose();
											num = 2;
											continue;
										case 2:
											goto IL_4CF;
										}
										break;
									}
								}
								IL_4CF:
								IL_4D1:;
							}
							goto Block_5;
						case 1:
							goto IL_65E;
						case 2:
							goto IL_4D2;
						case 3:
							try
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										if (!enumerator2.MoveNext())
										{
											num = 4;
											continue;
										}
										CellNote item2 = (CellNote)enumerator2.Current;
										this.ᜀ.Notes.Add(item2);
										goto IL_7C6;
									}
									case 2:
										goto IL_7FA;
									case 3:
										goto IL_7D1;
									case 4:
										num = 2;
										continue;
									}
									goto IL_789;
									IL_7C6:
									num = 3;
									continue;
									IL_789:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_7C6;
									default:
										if (false)
										{
										}
										break;
									}
									IL_7D1:
									num = 0;
								}
								IL_7FA:
								goto IL_30E;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable2 = enumerator2 as IDisposable;
									num = 1;
									for (;;)
									{
										switch (num)
										{
										case 0:
											disposable2.Dispose();
											num = 2;
											continue;
										case 1:
											if (disposable2 != null)
											{
												num = 0;
												continue;
											}
											goto IL_847;
										case 2:
											goto IL_845;
										}
										break;
									}
								}
								IL_845:
								IL_847:;
							}
							goto IL_848;
						case 4:
							goto IL_246;
						case 5:
							goto IL_598;
						case 6:
							try
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										if (!enumerator3.MoveNext())
										{
											num = 2;
											continue;
										}
										Chart item3 = (Chart)enumerator3.Current;
										this.ᜀ.Charts.Add(item3);
										num = 4;
										continue;
									}
									case 2:
										num = 3;
										continue;
									case 3:
										goto IL_F3;
									}
									IL_A1:
									num = 0;
									continue;
									goto IL_A1;
								}
								IL_F3:
								goto IL_726;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable3 = enumerator3 as IDisposable;
									num = 1;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_13E;
										case 1:
											if (disposable3 != null)
											{
												num = 2;
												continue;
											}
											goto IL_140;
										case 2:
											disposable3.Dispose();
											num = 0;
											continue;
										}
										break;
									}
								}
								IL_13E:
								IL_140:;
							}
							goto IL_141;
						case 7:
							try
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 1:
										num = 4;
										continue;
									case 3:
									{
										if (!enumerator4.MoveNext())
										{
											num = 1;
											continue;
										}
										CellImage item4 = (CellImage)enumerator4.Current;
										this.ᜀ.Images.Add(item4);
										num = 0;
										continue;
									}
									case 4:
										goto IL_1F8;
									}
									IL_1D2:
									num = 3;
									continue;
									goto IL_1D2;
								}
								IL_1F8:
								goto IL_848;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable4 = enumerator4 as IDisposable;
									num = 1;
									for (;;)
									{
										switch (num)
										{
										case 0:
											disposable4.Dispose();
											num = 2;
											continue;
										case 1:
											if (disposable4 != null)
											{
												num = 0;
												continue;
											}
											goto IL_245;
										case 2:
											goto IL_243;
										}
										break;
									}
								}
								IL_243:
								IL_245:;
							}
							goto Block_3;
						}
						break;
						IL_141:
						this.ᜀ.ItemStyles.Clear();
						IEnumerator enumerator5 = this.ᜌ.ToArray(typeof(StripStyle)).GetEnumerator();
						num = 2;
						continue;
						IL_30E:
						this.ᜀ.Charts.Clear();
						enumerator3 = this.\u171F.ToArray(typeof(Chart)).GetEnumerator();
						num = 6;
						continue;
						Block_3:
						IEnumerator enumerator6;
						try
						{
							IL_246:
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 1:
									goto IL_2C0;
								case 3:
								{
									if (!enumerator6.MoveNext())
									{
										num = 4;
										continue;
									}
									MergedCells item5 = (MergedCells)enumerator6.Current;
									this.ᜀ.MergedCells.Add(item5);
									num = 0;
									continue;
								}
								case 4:
									num = 1;
									continue;
								}
								IL_26E:
								num = 3;
								continue;
								goto IL_26E;
							}
							IL_2C0:
							goto IL_885;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable5 = enumerator6 as IDisposable;
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_30B;
									case 1:
										disposable5.Dispose();
										num = 0;
										continue;
									case 2:
										if (disposable5 != null)
										{
											num = 1;
											continue;
										}
										goto IL_30D;
									}
									break;
								}
							}
							IL_30B:
							IL_30D:;
						}
						goto IL_30E;
						Block_5:
						try
						{
							IL_4D2:
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 1:
									num = 4;
									continue;
								case 3:
								{
									if (!enumerator5.MoveNext())
									{
										num = 1;
										continue;
									}
									StripStyle item6 = (StripStyle)enumerator5.Current;
									this.ᜀ.ItemStyles.Add(item6);
									num = 0;
									continue;
								}
								case 4:
									goto IL_54A;
								}
								IL_4FA:
								num = 3;
								continue;
								goto IL_4FA;
							}
							IL_54A:
							goto IL_355;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable6 = enumerator5 as IDisposable;
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										disposable6.Dispose();
										num = 1;
										continue;
									case 1:
										goto IL_595;
									case 2:
										if (disposable6 != null)
										{
											num = 0;
											continue;
										}
										goto IL_597;
									}
									break;
								}
							}
							IL_595:
							IL_597:;
						}
						goto Block_6;
						IL_355:
						this.ᜀ.Hyperlinks.Clear();
						IEnumerator enumerator7 = this.\u171D.ToArray(typeof(CellHyperlink)).GetEnumerator();
						num = 5;
						continue;
						Block_7:
						IEnumerator enumerator8;
						try
						{
							IL_65E:
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 2:
									goto IL_6D8;
								case 3:
								{
									if (!enumerator8.MoveNext())
									{
										num = 4;
										continue;
									}
									Cell item7 = (Cell)enumerator8.Current;
									this.ᜀ.Cells.Add(item7);
									num = 1;
									continue;
								}
								case 4:
									num = 2;
									continue;
								}
								IL_6B2:
								num = 3;
								continue;
								goto IL_6B2;
							}
							IL_6D8:
							goto IL_392;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable7 = enumerator8 as IDisposable;
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_723;
									case 1:
										disposable7.Dispose();
										num = 0;
										continue;
									case 2:
										if (disposable7 != null)
										{
											num = 1;
											continue;
										}
										goto IL_725;
									}
									break;
								}
							}
							IL_723:
							IL_725:;
						}
						goto IL_726;
						IL_392:
						this.ᜀ.MergedCells.Clear();
						enumerator6 = this.ᜢ.ToArray(typeof(MergedCells)).GetEnumerator();
						num = 4;
						continue;
						Block_6:
						try
						{
							IL_598:
							num = 4;
							for (;;)
							{
								switch (num)
								{
								case 0:
								{
									if (!enumerator7.MoveNext())
									{
										num = 2;
										continue;
									}
									CellHyperlink item8 = (CellHyperlink)enumerator7.Current;
									this.ᜀ.Hyperlinks.Add(item8);
									num = 1;
									continue;
								}
								case 2:
									num = 3;
									continue;
								case 3:
									goto IL_610;
								}
								IL_5C0:
								num = 0;
								continue;
								goto IL_5C0;
							}
							IL_610:
							goto IL_3CF;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable8 = enumerator7 as IDisposable;
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										disposable8.Dispose();
										num = 2;
										continue;
									case 1:
										if (disposable8 != null)
										{
											num = 0;
											continue;
										}
										goto IL_65D;
									case 2:
										goto IL_65B;
									}
									break;
								}
							}
							IL_65B:
							IL_65D:;
						}
						goto Block_7;
						IL_3CF:
						this.ᜀ.Notes.Clear();
						enumerator2 = this.\u171E.ToArray(typeof(CellNote)).GetEnumerator();
						num = 3;
						continue;
						IL_726:
						this.ᜀ.Images.Clear();
						enumerator4 = this.ᜠ.ToArray(typeof(CellImage)).GetEnumerator();
						num = 7;
						continue;
						IL_848:
						this.ᜀ.Cells.Clear();
						enumerator8 = this.ᜡ.ToArray(typeof(Cell)).GetEnumerator();
						num = 1;
					}
				}
				IL_885:
				this.ᜀ.AutoFitColWidth = this.ᜇ;
				this.ᜀ.AutoFitTitleWidth = this.ᜈ;
				this.ᜀ.SheetOptions.SheetTitle = this.ᜉ;
				this.ᜀ.SheetOptions = this.ᜊ;
				this.ᜀ.ItemType = this.\u170D;
				this.ᜀ.Background = this.ᜣ;
				this.ᜀ.DataSource = this.ᜎ;
				this.ᜀ.SQLCommand = this.ᜏ;
				this.ᜀ.DataTable = this.ᜐ;
				this.ᜀ.ListView = this.ᜑ;
				this.ᜀ.Columns = this.\u1712;
				this.ᜀ.HeaderRows = this.\u1713;
				this.ᜀ.StartDataCol = this.\u1714;
				this.ᜀ.FooterRows = this.\u1715;
				this.ᜀ.Header = this.\u1716;
				this.ᜀ.Titles = this.\u1717;
				this.ᜀ.Footer = this.\u1718;
				this.ᜀ.DataFormats = this.\u1719;
				this.ᜀ.CustomFormats = this.\u171A;
				this.ᜀ.ColumnsWidth = this.\u171B;
				this.ᜀ.AddTitles = this.ᜦ;
				this.ᜀ.MaxRows = this.ᜨ;
				this.ᜀ.SkipRows = this.ᜩ;
				this.ᜀ.DataExported = this.ᜫ;
				this.ᜀ.ᜑ = this.ᜯ;
				this.ᜀ.Culture = this.ᜮ;
				return;
			}
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0009A090 File Offset: 0x00099090
		public void SetColumnWidth(ushort Col, ushort Width)
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A2;
				case 1:
					goto IL_34;
				case 2:
					goto IL_4E;
				case 3:
					if (!this.ᜯ.ContainsKey(Col))
					{
						goto IL_BC;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				if (this.ᜯ == null)
				{
					num = 1;
					continue;
				}
				goto IL_4E;
				IL_34:
				this.ᜯ = new Hashtable();
				num = 2;
				continue;
				IL_4E:
				num = 3;
			}
			IL_A2:
			this.ᜯ[Col] = Width;
			return;
			IL_BC:
			this.ᜯ.Add(Col, Width);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0009A170 File Offset: 0x00099170
		public Cell AddBoolean(ushort Col, ushort Row, bool Value)
		{
			for (;;)
			{
				IL_30:
				int num = this.ᜡ.IndexOf(Col, Row);
				int num2 = 0;
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
						switch (num2)
						{
						case 0:
							if (num >= 0)
							{
								num2 = 1;
								continue;
							}
							goto IL_8A;
						case 1:
							goto IL_52;
						case 2:
							goto IL_88;
						}
						goto IL_30;
					}
					IL_52:
					this.ᜡ.Remove(this.ᜡ[num]);
					if (true)
					{
					}
					num2 = 2;
				}
			}
			IL_88:
			IL_8A:
			Cell cell = this.ᜡ.Add(new Cell());
			cell.CellType = CellType.Boolean;
			cell.Column = (int)Col;
			cell.Row = (int)Row;
			cell.Value = Value;
			return cell;
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0009A23C File Offset: 0x0009923C
		public Cell AddDateTime(ushort Col, ushort Row, string DateTimeFormat, DateTime Value)
		{
			for (;;)
			{
				IL_38:
				int num = this.ᜡ.IndexOf(Col, Row);
				int num2 = 0;
				for (;;)
				{
					if (true)
					{
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
						switch (num2)
						{
						case 0:
							if (num >= 0)
							{
								num2 = 1;
								continue;
							}
							goto IL_8A;
						case 1:
							goto IL_64;
						case 2:
							goto IL_88;
						}
						goto IL_38;
					}
					IL_64:
					this.ᜡ.Remove(this.ᜡ[num]);
					num2 = 2;
				}
			}
			IL_88:
			IL_8A:
			Cell cell = this.ᜡ.Add(new Cell());
			cell.CellType = CellType.DateTime;
			cell.Column = (int)Col;
			cell.DateTimeFormat = DateTimeFormat;
			cell.Row = (int)Row;
			cell.Value = Value;
			return cell;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0009A310 File Offset: 0x00099310
		public Cell AddNumeric(ushort Col, ushort Row, string NumericFormat, double Value)
		{
			for (;;)
			{
				IL_30:
				int num = this.ᜡ.IndexOf(Col, Row);
				int num2 = 1;
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
						switch (num2)
						{
						case 0:
							goto IL_52;
						case 1:
							if (num >= 0)
							{
								num2 = 0;
								continue;
							}
							goto IL_8A;
						case 2:
							goto IL_88;
						}
						goto IL_30;
					}
					IL_52:
					if (true)
					{
					}
					this.ᜡ.Remove(this.ᜡ[num]);
					num2 = 2;
				}
			}
			IL_88:
			IL_8A:
			Cell cell = this.ᜡ.Add(new Cell());
			cell.CellType = CellType.Numeric;
			cell.Column = (int)Col;
			cell.NumericFormat = NumericFormat;
			cell.Row = (int)Row;
			cell.Value = Value;
			return cell;
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0009A3E4 File Offset: 0x000993E4
		public Cell AddNumeric(ushort Col, ushort Row, double Value)
		{
			for (;;)
			{
				IL_30:
				int num = this.ᜡ.IndexOf(Col, Row);
				int num2 = 1;
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
						switch (num2)
						{
						case 0:
							goto IL_52;
						case 1:
							if (num >= 0)
							{
								num2 = 0;
								continue;
							}
							goto IL_7F;
						case 2:
							goto IL_7D;
						}
						goto IL_30;
					}
					IL_52:
					this.ᜡ.Remove(this.ᜡ[num]);
					num2 = 2;
				}
			}
			IL_7D:
			IL_7F:
			if (true)
			{
			}
			Cell cell = this.ᜡ.Add(new Cell());
			cell.CellType = CellType.Numeric;
			cell.NumericFormat = string.Empty;
			cell.Column = (int)Col;
			cell.Row = (int)Row;
			cell.Value = Value;
			return cell;
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0009A4B8 File Offset: 0x000994B8
		public Cell AddString(ushort Col, ushort Row, string Value)
		{
			for (;;)
			{
				IL_30:
				int num = this.ᜡ.IndexOf(Col, Row);
				int num2 = 0;
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
						switch (num2)
						{
						case 0:
							if (num >= 0)
							{
								num2 = 2;
								continue;
							}
							goto IL_87;
						case 1:
							goto IL_7D;
						case 2:
							goto IL_52;
						}
						goto IL_30;
					}
					IL_52:
					this.ᜡ.Remove(this.ᜡ[num]);
					num2 = 1;
				}
			}
			IL_7D:
			if (true)
			{
			}
			IL_87:
			Cell cell = this.ᜡ.Add(new Cell());
			cell.CellType = CellType.String;
			cell.Column = (int)Col;
			cell.Row = (int)Row;
			cell.Value = Value;
			return cell;
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0009A57C File Offset: 0x0009957C
		public Cell AddFormula(ushort Col, ushort Row, string Value)
		{
			for (;;)
			{
				IL_38:
				int num = this.ᜡ.IndexOf(Col, Row);
				int num2 = 0;
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
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
							if (num >= 0)
							{
								num2 = 2;
								continue;
							}
							goto IL_8A;
						case 1:
							goto IL_88;
						case 2:
							goto IL_64;
						}
						goto IL_38;
					}
					IL_64:
					this.ᜡ.Remove(this.ᜡ[num]);
					num2 = 1;
				}
			}
			IL_88:
			IL_8A:
			Cell cell = this.ᜡ.Add(new Cell());
			cell.CellType = CellType.Formula;
			cell.Column = (int)Col;
			cell.Row = (int)Row;
			cell.Value = Value;
			return cell;
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0009A644 File Offset: 0x00099644
		public CellHyperlink AddHyperLink(ushort Col, ushort Row, string Title, string Url)
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
			CellHyperlink cellHyperlink = this.\u171D.Add(new CellHyperlink());
			cellHyperlink.Col = (int)Col;
			cellHyperlink.Row = (int)Row;
			cellHyperlink.Title = Title;
			cellHyperlink.Target = Url;
			cellHyperlink.Tip = Url;
			return cellHyperlink;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0009A6B8 File Offset: 0x000996B8
		public MergedCells AddMerged(ushort FirstRow, ushort LastRow, ushort FirstCol, ushort LastCol)
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
			MergedCells mergedCells = this.ᜢ.Add(new MergedCells());
			mergedCells.StartRow = (int)FirstRow;
			mergedCells.EndRow = (int)LastRow;
			mergedCells.StartCol = (int)FirstCol;
			mergedCells.EndCol = (int)LastCol;
			return mergedCells;
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x0009A724 File Offset: 0x00099724
		internal ColumnsExport ColumnsExport
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
				return this.ᜁ;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x0009A768 File Offset: 0x00099768
		internal RowExport ExportRowExport
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
				return this.ᜂ;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x0009A7AC File Offset: 0x000997AC
		protected ushort StartDataRow
		{
			get
			{
				ushort num;
				for (;;)
				{
					num = 0;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							num2 = 2;
							continue;
						case 1:
							goto IL_36;
						case 2:
							if (this.\u1713 < this.\u1716.Count)
							{
								num2 = 8;
								continue;
							}
							goto IL_6D;
						case 3:
							goto IL_6D;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_36;
							default:
								if (false)
								{
								}
								num = (ushort)this.\u1716.Count;
								num2 = 7;
								continue;
							}
							break;
						case 5:
							num += (this.ᜦ ? 1 : 0);
							num2 = 6;
							continue;
						case 6:
							goto IL_95;
						case 7:
							if (this.\u1713 > 0)
							{
								num2 = 0;
								continue;
							}
							goto IL_6D;
						case 8:
							if (true)
							{
							}
							num = (ushort)this.\u1713;
							num2 = 3;
							continue;
						}
						break;
						IL_36:
						if (this.\u1716.Count > 0)
						{
							num2 = 4;
							continue;
						}
						IL_6D:
						num2 = 5;
					}
				}
				IL_95:
				return num + (this.ᜧ ? 1 : 0);
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x0009A8F8 File Offset: 0x000998F8
		protected int CurrentRow
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

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x0009A93C File Offset: 0x0009993C
		// (set) Token: 0x06000DA7 RID: 3495 RVA: 0x0009A980 File Offset: 0x00099980
		internal int TotalCols
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
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜅ = value;
						num = 2;
						continue;
					case 2:
						return;
					}
					if (value == this.ᜅ)
					{
						break;
					}
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
						num = 0;
						break;
					}
				}
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x0009A9FC File Offset: 0x000999FC
		// (set) Token: 0x06000DA9 RID: 3497 RVA: 0x0009AA44 File Offset: 0x00099A44
		protected XlsExportStage ExportStage
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
				return this.ᜀ.ExportStage;
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
						this.ᜀ.ExportStage = value;
						num = 0;
						continue;
					}
					if (value == this.ᜀ.ExportStage)
					{
						break;
					}
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
						num = 1;
						break;
					}
				}
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x0009AACC File Offset: 0x00099ACC
		// (set) Token: 0x06000DAB RID: 3499 RVA: 0x0009AB10 File Offset: 0x00099B10
		internal int RecordCounter
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						this.ᜄ = value;
						num = 0;
						continue;
					}
					if (true)
					{
					}
					if (value == this.ᜄ)
					{
						break;
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
						num = 1;
						break;
					}
				}
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x0009AB8C File Offset: 0x00099B8C
		// (set) Token: 0x06000DAD RID: 3501 RVA: 0x0009ABD0 File Offset: 0x00099BD0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CellExport ExportCell
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
				return this.ᜀ;
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
						this.ᜀ = value;
						num = 0;
						continue;
					}
					if (value == this.ᜀ)
					{
						break;
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
						num = 1;
						break;
					}
				}
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x0009AC4C File Offset: 0x00099C4C
		internal new int Index
		{
			get
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						num = 1;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return -1;
						default:
							if (false)
							{
							}
							if (base.Collection is WorkSheets)
							{
								num = 2;
								continue;
							}
							return -1;
						}
						break;
					case 2:
						goto IL_92;
					}
					if (base.Collection == null)
					{
						return -1;
					}
					num = 0;
				}
				IL_92:
				return (base.Collection as WorkSheets).IndexOf(this);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0009ACF0 File Offset: 0x00099CF0
		// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x0009AD34 File Offset: 0x00099D34
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool AutoFitColWidth
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
						return;
					case 2:
						this.ᜇ = value;
						num = 1;
						continue;
					}
					if (value == this.ᜇ)
					{
						break;
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
						if (true)
						{
						}
						num = 2;
						break;
					}
				}
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x0009ADB0 File Offset: 0x00099DB0
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x0009ADF4 File Offset: 0x00099DF4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(false)]
		public bool AutoFitTitleWidth
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
				return this.ᜈ;
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
						this.ᜈ = value;
						num = 0;
						continue;
					}
					if (value == this.ᜈ)
					{
						break;
					}
					if (true)
					{
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
						num = 1;
						break;
					}
				}
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x0009AE70 File Offset: 0x00099E70
		// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x0009AEB4 File Offset: 0x00099EB4
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string SheetName
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
						this.ᜉ = value;
						this.SetName(value);
						num = 0;
						continue;
					}
					if (!(value != this.ᜉ))
					{
						break;
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
						num = 1;
						break;
					}
				}
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x0009AF3C File Offset: 0x00099F3C
		// (set) Token: 0x06000DB6 RID: 3510 RVA: 0x0009AF80 File Offset: 0x00099F80
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public SheetOptions Options
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
				return this.ᜊ;
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
						if (value == this.ᜊ)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3B;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 3:
						num = 2;
						continue;
					case 4:
						goto IL_3B;
					}
					if (value != null)
					{
						num = 3;
						continue;
					}
					break;
					IL_3B:
					this.ᜊ = value;
					num = 0;
				}
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0009B018 File Offset: 0x0009A018
		// (set) Token: 0x06000DB8 RID: 3512 RVA: 0x0009B05C File Offset: 0x0009A05C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(ColumnFormatsCollectionEditor), typeof(UITypeEditor))]
		public ColumnFormats ColumnFormats
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
				return this.ᜋ;
			}
			set
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_3B;
					case 1:
						if (value == this.ᜋ)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3B;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 2:
						num = 1;
						continue;
					case 3:
						goto IL_4A;
					}
					if (value != null)
					{
						num = 2;
						continue;
					}
					return;
					IL_3B:
					this.ᜋ = value;
					num = 3;
				}
				IL_4A:
				if (true)
				{
				}
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x0009B0F4 File Offset: 0x0009A0F4
		// (set) Token: 0x06000DBA RID: 3514 RVA: 0x0009B138 File Offset: 0x0009A138
		[Editor(typeof(CellItemStylesCollectionEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ItemStyles ItemStyles
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
				return this.ᜌ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						num = 4;
						continue;
					case 2:
						goto IL_4A;
					case 3:
						goto IL_3B;
					case 4:
						if (value == this.ᜌ)
						{
							goto IL_83;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3B;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					}
					if (value != null)
					{
						num = 1;
						continue;
					}
					break;
					IL_3B:
					this.ᜌ = value;
					num = 2;
				}
				IL_4A:
				IL_83:
				if (true)
				{
				}
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x0009B1D0 File Offset: 0x0009A1D0
		// (set) Token: 0x06000DBC RID: 3516 RVA: 0x0009B214 File Offset: 0x0009A214
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(CellItemType.None)]
		public CellItemType ItemType
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
				return this.\u170D;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.\u170D = value;
						num = 2;
						continue;
					case 2:
						return;
					}
					if (value == this.\u170D)
					{
						break;
					}
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
						num = 1;
						break;
					}
				}
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x0009B290 File Offset: 0x0009A290
		// (set) Token: 0x06000DBE RID: 3518 RVA: 0x0009B2D4 File Offset: 0x0009A2D4
		[Editor(typeof(HyperlinksCollectionEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CellHyperlinks Hyperlinks
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
				return this.\u171D;
			}
			set
			{
				if (true)
				{
				}
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value == this.\u171D)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_43;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 1:
						return;
					case 2:
						num = 0;
						continue;
					case 3:
						goto IL_43;
					}
					if (value != null)
					{
						num = 2;
						continue;
					}
					break;
					IL_43:
					this.\u171D = value;
					num = 1;
				}
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x0009B36C File Offset: 0x0009A36C
		// (set) Token: 0x06000DC0 RID: 3520 RVA: 0x0009B3B0 File Offset: 0x0009A3B0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
		public CellNotes Notes
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
				return this.\u171E;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value == this.\u171E)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3B;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_3B;
					case 3:
						return;
					case 4:
						num = 0;
						continue;
					}
					if (value != null)
					{
						num = 4;
						continue;
					}
					break;
					IL_3B:
					this.\u171E = value;
					num = 3;
				}
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x0009B448 File Offset: 0x0009A448
		// (set) Token: 0x06000DC2 RID: 3522 RVA: 0x0009B48C File Offset: 0x0009A48C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[Description("Allows you to select string fields that will not be truncated by occurrences of carriage returns.")]
		public StringListCollection NotTruncatableColumns
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
				return this.\u171C;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						num = 4;
						continue;
					case 1:
						goto IL_3B;
					case 3:
						return;
					case 4:
						if (value == this.\u171C)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3B;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					if (value != null)
					{
						num = 0;
						continue;
					}
					break;
					IL_3B:
					this.\u171C = value;
					num = 3;
				}
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x0009B524 File Offset: 0x0009A524
		// (set) Token: 0x06000DC4 RID: 3524 RVA: 0x0009B568 File Offset: 0x0009A568
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(ChartsCollectionEditor), typeof(UITypeEditor))]
		public Charts Charts
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
				return this.\u171F;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value == this.\u171F)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_43;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 1:
						return;
					case 2:
						if (true)
						{
						}
						break;
					case 3:
						num = 0;
						continue;
					case 4:
						goto IL_43;
					}
					if (value != null)
					{
						num = 3;
						continue;
					}
					break;
					IL_43:
					this.\u171F = value;
					num = 1;
				}
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x0009B600 File Offset: 0x0009A600
		// (set) Token: 0x06000DC6 RID: 3526 RVA: 0x0009B644 File Offset: 0x0009A644
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(ImagesCollectionEditor), typeof(UITypeEditor))]
		public CellImages Images
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
				return this.ᜠ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_3B;
					case 2:
						return;
					case 3:
						if (value == this.ᜠ)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3B;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 4:
						num = 3;
						continue;
					}
					if (value != null)
					{
						num = 4;
						continue;
					}
					break;
					IL_3B:
					if (true)
					{
					}
					this.ᜠ = value;
					num = 2;
				}
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x0009B6DC File Offset: 0x0009A6DC
		// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x0009B720 File Offset: 0x0009A720
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(CellsCollectionEditor), typeof(UITypeEditor))]
		public Cells Cells
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
				return this.ᜡ;
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
						goto IL_3B;
					case 3:
						if (value == this.ᜡ)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3B;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 4:
						num = 3;
						continue;
					}
					if (value != null)
					{
						num = 4;
						continue;
					}
					break;
					IL_3B:
					this.ᜡ = value;
					num = 0;
				}
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x0009B7B8 File Offset: 0x0009A7B8
		// (set) Token: 0x06000DCA RID: 3530 RVA: 0x0009B7FC File Offset: 0x0009A7FC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
		public MergedCellList MergedCells
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
				return this.ᜢ;
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
						goto IL_43;
					case 3:
						num = 4;
						continue;
					case 4:
						if (value == this.ᜢ)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_43;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					}
					if (value != null)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					break;
					IL_43:
					this.ᜢ = value;
					num = 1;
				}
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x0009B894 File Offset: 0x0009A894
		// (set) Token: 0x06000DCC RID: 3532 RVA: 0x0009B8D8 File Offset: 0x0009A8D8
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CellGraphic Background
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
				return this.ᜣ;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_3B;
					case 1:
						num = 4;
						continue;
					case 3:
						return;
					case 4:
						if (value == this.ᜣ)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3B;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					if (value != null)
					{
						num = 1;
						continue;
					}
					break;
					IL_3B:
					this.ᜣ = value;
					num = 3;
				}
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x0009B970 File Offset: 0x0009A970
		// (set) Token: 0x06000DCE RID: 3534 RVA: 0x0009B9B4 File Offset: 0x0009A9B4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(0)]
		public int HeaderRows
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
				return this.\u1713;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u1713 = value;
						num = 2;
						continue;
					case 1:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						if (false)
						{
						}
						break;
					case 2:
						return;
					}
					if (value == this.\u1713)
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x0009BA30 File Offset: 0x0009AA30
		// (set) Token: 0x06000DD0 RID: 3536 RVA: 0x0009BA74 File Offset: 0x0009AA74
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public byte StartDataCol
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
				return this.\u1714;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u1714 = value;
						num = 1;
						continue;
					case 1:
						return;
					case 2:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						if (false)
						{
						}
						break;
					}
					if (value == this.\u1714)
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x0009BAF0 File Offset: 0x0009AAF0
		// (set) Token: 0x06000DD2 RID: 3538 RVA: 0x0009BB34 File Offset: 0x0009AB34
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int FooterRows
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
				return this.\u1715;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						return;
					case 2:
						this.\u1715 = value;
						num = 1;
						continue;
					}
					if (true)
					{
					}
					if (value == this.\u1715)
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x0009BBB0 File Offset: 0x0009ABB0
		// (set) Token: 0x06000DD4 RID: 3540 RVA: 0x0009BBF4 File Offset: 0x0009ABF4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(ExportColumnsEditor), typeof(UITypeEditor))]
		public StringListCollection Columns
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
				return this.\u1712;
			}
			set
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (true)
						{
						}
						this.\u1712 = value;
						num = 0;
						continue;
					case 2:
						if (value != this.\u1712)
						{
							num = 1;
							continue;
						}
						return;
					case 3:
						goto IL_55;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_55:
						num = 2;
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

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x0009BC8C File Offset: 0x0009AC8C
		// (set) Token: 0x06000DD6 RID: 3542 RVA: 0x0009BCD0 File Offset: 0x0009ACD0
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public StringListCollection Header
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
				return this.\u1716;
			}
			set
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value != this.\u1716)
						{
							num = 2;
							continue;
						}
						return;
					case 1:
						goto IL_5D;
					case 2:
						this.\u1716 = value;
						num = 3;
						continue;
					case 3:
						return;
					case 4:
						if (true)
						{
						}
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5D:
						num = 0;
						break;
					default:
						if (false)
						{
						}
						if (value == null)
						{
							return;
						}
						num = 1;
						break;
					}
				}
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x0009BD68 File Offset: 0x0009AD68
		// (set) Token: 0x06000DD8 RID: 3544 RVA: 0x0009BDAC File Offset: 0x0009ADAC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		public StringListCollection Titles
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
				return this.\u1717;
			}
			set
			{
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 2:
						if (value != this.\u1717)
						{
							num = 3;
							continue;
						}
						return;
					case 3:
						this.\u1717 = value;
						num = 0;
						continue;
					case 4:
						goto IL_5D;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5D:
						num = 2;
						break;
					default:
						if (false)
						{
						}
						if (value == null)
						{
							return;
						}
						num = 4;
						break;
					}
				}
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x0009BE44 File Offset: 0x0009AE44
		// (set) Token: 0x06000DDA RID: 3546 RVA: 0x0009BE88 File Offset: 0x0009AE88
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public StringListCollection Footer
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
				return this.\u1718;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u1718 = value;
						num = 2;
						continue;
					case 2:
						return;
					case 3:
						goto IL_55;
					case 4:
						if (value != this.\u1718)
						{
							num = 0;
							continue;
						}
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_55:
						if (true)
						{
						}
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

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x0009BF20 File Offset: 0x0009AF20
		// (set) Token: 0x06000DDC RID: 3548 RVA: 0x0009BF64 File Offset: 0x0009AF64
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public FormatsExport FormatsExport
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
				return this.\u1719;
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
						this.\u1719 = value;
						num = 0;
						continue;
					case 3:
						goto IL_5D;
					case 4:
						if (value != this.\u1719)
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
						IL_5D:
						num = 4;
						break;
					default:
						if (true)
						{
						}
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

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x0009BFFC File Offset: 0x0009AFFC
		// (set) Token: 0x06000DDE RID: 3550 RVA: 0x0009C040 File Offset: 0x0009B040
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public StringListCollection CustomFormats
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
				return this.\u171A;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value != this.\u171A)
						{
							num = 2;
							continue;
						}
						return;
					case 1:
						if (true)
						{
						}
						break;
					case 2:
						this.\u171A = value;
						num = 3;
						continue;
					case 3:
						return;
					case 4:
						goto IL_5D;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5D:
						num = 0;
						break;
					default:
						if (false)
						{
						}
						if (value == null)
						{
							return;
						}
						num = 4;
						break;
					}
				}
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x0009C0D8 File Offset: 0x0009B0D8
		// (set) Token: 0x06000DE0 RID: 3552 RVA: 0x0009C11C File Offset: 0x0009B11C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		public StringListCollection ColumnsWidth
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
				return this.\u171B;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_5D;
					case 2:
						return;
					case 3:
						this.\u171B = value;
						num = 2;
						continue;
					case 4:
						if (value != this.\u171B)
						{
							num = 3;
							continue;
						}
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5D:
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
						if (true)
						{
						}
						num = 0;
						break;
					}
				}
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x0009C1B4 File Offset: 0x0009B1B4
		// (set) Token: 0x06000DE2 RID: 3554 RVA: 0x0009C1F8 File Offset: 0x0009B1F8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(12.75)]
		public double DefRowHeight
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
				return this.ᜤ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜤ = value;
						num = 2;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 2:
						return;
					}
					if (true)
					{
					}
					if (value == this.ᜤ)
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x0009C274 File Offset: 0x0009B274
		// (set) Token: 0x06000DE4 RID: 3556 RVA: 0x0009C2B8 File Offset: 0x0009B2B8
		[DefaultValue(8)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int DefColWidth
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
				return this.ᜥ;
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
						this.ᜥ = value;
						num = 0;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					if (value == this.ᜥ)
					{
						break;
					}
					if (true)
					{
					}
					num = 1;
				}
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x0009C334 File Offset: 0x0009B334
		// (set) Token: 0x06000DE6 RID: 3558 RVA: 0x0009C378 File Offset: 0x0009B378
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(true)]
		public bool AllowTitles
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
				return this.ᜦ;
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
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 2:
						this.ᜦ = value;
						num = 0;
						continue;
					}
					if (true)
					{
					}
					if (value == this.ᜦ)
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x0009C3F4 File Offset: 0x0009B3F4
		// (set) Token: 0x06000DE8 RID: 3560 RVA: 0x0009C438 File Offset: 0x0009B438
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int MaxRows
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
				return this.ᜨ;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜨ = value;
						num = 1;
						continue;
					case 1:
						return;
					case 2:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						if (false)
						{
						}
						break;
					}
					if (value == this.ᜨ)
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x0009C4B4 File Offset: 0x0009B4B4
		// (set) Token: 0x06000DEA RID: 3562 RVA: 0x0009C4F8 File Offset: 0x0009B4F8
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int SkipRows
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
				return this.ᜩ;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜩ = value;
						num = 1;
						continue;
					case 1:
						return;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					if (value == this.ᜩ)
					{
						break;
					}
					if (true)
					{
					}
					num = 0;
				}
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x0009C574 File Offset: 0x0009B574
		// (set) Token: 0x06000DEC RID: 3564 RVA: 0x0009C5B8 File Offset: 0x0009B5B8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(true)]
		public bool Exported
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
				return this.ᜪ;
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
						this.ᜪ = value;
						num = 0;
						continue;
					case 2:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						if (false)
						{
						}
						break;
					}
					if (value == this.ᜪ)
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x0009C634 File Offset: 0x0009B634
		// (set) Token: 0x06000DEE RID: 3566 RVA: 0x0009C678 File Offset: 0x0009B678
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(true)]
		public bool DataExported
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
				return this.ᜫ;
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
					case 1:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						if (false)
						{
						}
						break;
					case 2:
						this.ᜫ = value;
						num = 0;
						continue;
					}
					if (value == this.ᜫ)
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x0009C6F4 File Offset: 0x0009B6F4
		// (set) Token: 0x06000DF0 RID: 3568 RVA: 0x0009C738 File Offset: 0x0009B738
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int Tag
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
				return this.ᜬ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						if (false)
						{
						}
						break;
					case 1:
						return;
					case 2:
						this.ᜬ = value;
						num = 1;
						continue;
					}
					if (value == this.ᜬ)
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x0009C7B4 File Offset: 0x0009B7B4
		internal sprᱨ ColumnList
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
				return this.ᜭ;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0009C7F8 File Offset: 0x0009B7F8
		internal bool NeedCheckRowHeight
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
				return this.ᜆ;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x0009C83C File Offset: 0x0009B83C
		// (set) Token: 0x06000DF4 RID: 3572 RVA: 0x0009C880 File Offset: 0x0009B880
		internal CultureInfo Culture
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
				return this.ᜮ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_55;
					case 2:
						if (true)
						{
						}
						if (value != this.ᜮ)
						{
							num = 3;
							continue;
						}
						return;
					case 3:
						this.ᜮ = value;
						num = 4;
						continue;
					case 4:
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_55:
						num = 2;
						break;
					default:
						if (false)
						{
						}
						if (value == null)
						{
							return;
						}
						num = 0;
						break;
					}
				}
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0009C918 File Offset: 0x0009B918
		// (set) Token: 0x06000DF6 RID: 3574 RVA: 0x0009C95C File Offset: 0x0009B95C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(ExportSource.SqlCommand)]
		public ExportSource DataSource
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
				return this.ᜎ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜎ = value;
						num = 2;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							break;
						}
						break;
					case 2:
						goto IL_6C;
					}
					if (value == this.ᜎ)
					{
						break;
					}
					num = 0;
				}
				IL_6C:
				this.ᜀ(value, this.SQLCommand, this.DataTable, this.ListView);
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x0009C9F0 File Offset: 0x0009B9F0
		// (set) Token: 0x06000DF8 RID: 3576 RVA: 0x0009CA34 File Offset: 0x0009BA34
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public IDbCommand SQLCommand
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
				return this.ᜏ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_64;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 2:
						this.ᜏ = value;
						num = 0;
						continue;
					}
					if (value == this.ᜏ)
					{
						break;
					}
					num = 2;
				}
				IL_64:
				if (true)
				{
				}
				this.ᜀ(this.DataSource, value, this.DataTable, this.ListView);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x0009CAC8 File Offset: 0x0009BAC8
		// (set) Token: 0x06000DFA RID: 3578 RVA: 0x0009CB0C File Offset: 0x0009BB0C
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public DataTable DataTable
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
				return this.ᜐ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6C;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 2:
						if (true)
						{
						}
						this.ᜐ = value;
						num = 0;
						continue;
					}
					if (value == this.ᜐ)
					{
						break;
					}
					num = 2;
				}
				IL_6C:
				this.ᜀ(this.DataSource, this.SQLCommand, value, this.ListView);
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x0009CBA0 File Offset: 0x0009BBA0
		// (set) Token: 0x06000DFC RID: 3580 RVA: 0x0009CBE4 File Offset: 0x0009BBE4
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public ListView ListView
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
				return this.ᜑ;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6C;
					case 1:
						this.ᜑ = value;
						num = 0;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							break;
						}
						break;
					}
					if (value == this.ᜑ)
					{
						break;
					}
					num = 1;
				}
				IL_6C:
				this.ᜀ(this.DataSource, this.SQLCommand, this.DataTable, value);
			}
		}

		// Token: 0x04000A77 RID: 2679
		internal CellExport ᜀ;

		// Token: 0x04000A78 RID: 2680
		private ColumnsExport ᜁ;

		// Token: 0x04000A79 RID: 2681
		private RowExport ᜂ;

		// Token: 0x04000A7A RID: 2682
		private int ᜃ;

		// Token: 0x04000A7B RID: 2683
		private int ᜄ;

		// Token: 0x04000A7C RID: 2684
		private int ᜅ;

		// Token: 0x04000A7D RID: 2685
		private bool ᜆ;

		// Token: 0x04000A7E RID: 2686
		private bool ᜇ;

		// Token: 0x04000A7F RID: 2687
		private bool ᜈ;

		// Token: 0x04000A80 RID: 2688
		private string ᜉ = string.Empty;

		// Token: 0x04000A81 RID: 2689
		private SheetOptions ᜊ = new SheetOptions();

		// Token: 0x04000A82 RID: 2690
		private ColumnFormats ᜋ;

		// Token: 0x04000A83 RID: 2691
		private ItemStyles ᜌ;

		// Token: 0x04000A84 RID: 2692
		private CellItemType \u170D;

		// Token: 0x04000A85 RID: 2693
		private ExportSource ᜎ;

		// Token: 0x04000A86 RID: 2694
		private IDbCommand ᜏ;

		// Token: 0x04000A87 RID: 2695
		private DataTable ᜐ;

		// Token: 0x04000A88 RID: 2696
		private ListView ᜑ;

		// Token: 0x04000A89 RID: 2697
		private StringListCollection \u1712 = new StringListCollection();

		// Token: 0x04000A8A RID: 2698
		private int \u1713;

		// Token: 0x04000A8B RID: 2699
		private byte \u1714;

		// Token: 0x04000A8C RID: 2700
		private int \u1715;

		// Token: 0x04000A8D RID: 2701
		private StringListCollection \u1716 = new StringListCollection();

		// Token: 0x04000A8E RID: 2702
		private StringListCollection \u1717 = new StringListCollection();

		// Token: 0x04000A8F RID: 2703
		private StringListCollection \u1718 = new StringListCollection();

		// Token: 0x04000A90 RID: 2704
		private FormatsExport \u1719;

		// Token: 0x04000A91 RID: 2705
		private StringListCollection \u171A = new StringListCollection();

		// Token: 0x04000A92 RID: 2706
		private StringListCollection \u171B = new StringListCollection();

		// Token: 0x04000A93 RID: 2707
		private StringListCollection \u171C = new StringListCollection();

		// Token: 0x04000A94 RID: 2708
		private CellHyperlinks \u171D;

		// Token: 0x04000A95 RID: 2709
		private CellNotes \u171E;

		// Token: 0x04000A96 RID: 2710
		private Charts \u171F;

		// Token: 0x04000A97 RID: 2711
		private CellImages ᜠ;

		// Token: 0x04000A98 RID: 2712
		private Cells ᜡ;

		// Token: 0x04000A99 RID: 2713
		private MergedCellList ᜢ;

		// Token: 0x04000A9A RID: 2714
		private CellGraphic ᜣ = new CellGraphic();

		// Token: 0x04000A9B RID: 2715
		private double ᜤ = 12.75;

		// Token: 0x04000A9C RID: 2716
		private int ᜥ = 8;

		// Token: 0x04000A9D RID: 2717
		private bool ᜦ = true;

		// Token: 0x04000A9E RID: 2718
		private string[] \u2593\u0097\u0081\u008C;

		// Token: 0x04000A9F RID: 2719
		private bool ᜧ;

		// Token: 0x04000AA0 RID: 2720
		private int ᜨ;

		// Token: 0x04000AA1 RID: 2721
		private int ᜩ;

		// Token: 0x04000AA2 RID: 2722
		private bool ᜪ = true;

		// Token: 0x04000AA3 RID: 2723
		private bool ᜫ = true;

		// Token: 0x04000AA4 RID: 2724
		private int ᜬ;

		// Token: 0x04000AA5 RID: 2725
		private sprᱨ ᜭ = new sprᱨ();

		// Token: 0x04000AA6 RID: 2726
		private CultureInfo ᜮ = CultureInfo.CurrentCulture;

		// Token: 0x04000AA7 RID: 2727
		internal Hashtable ᜯ;
	}
}
