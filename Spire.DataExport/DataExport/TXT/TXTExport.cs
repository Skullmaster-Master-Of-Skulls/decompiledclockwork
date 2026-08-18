using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Threading;
using System.Web;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.Common;
using Spire.DataExport.Delegates;
using Spire.DataExport.Forms;
using Spire.DataExport.License;
using Spire.DataExport.PropEditors;
using Spire.DataExport.Utils;

namespace Spire.DataExport.TXT
{
	// Token: 0x0200020B RID: 523
	[LicenseProvider(typeof(DataExportLicenseProvider))]
	[ToolboxItem(true)]
	public class TXTExport : FormatTextExport
	{
		// Token: 0x06000FA9 RID: 4009 RVA: 0x000A7C24 File Offset: 0x000A6C24
		protected override void InitializeVariables()
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
			this.ᜁ = !LicenseManager.IsValid(base.GetType(), this, out this.ᜀ);
			base.InitializeVariables();
			this.AutoFitColWidth = true;
			this.ᜂ = new CSVOption(this);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x000A7C94 File Offset: 0x000A6C94
		protected override void Dispose(bool disposing)
		{
			try
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜂ != null)
						{
							num = 4;
							continue;
						}
						goto IL_B7;
					case 1:
						goto IL_B7;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B7;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 3:
						goto IL_76;
					case 4:
						this.ᜂ.Dispose();
						this.ᜂ = null;
						num = 1;
						continue;
					case 5:
						this.ᜀ.Dispose();
						this.ᜀ = null;
						if (true)
						{
						}
						num = 3;
						continue;
					case 6:
						goto IL_C2;
					}
					if (this.ᜀ != null)
					{
						num = 5;
						continue;
					}
					IL_76:
					num = 0;
					continue;
					IL_B7:
					num = 6;
				}
				IL_C2:;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x000A7D88 File Offset: 0x000A6D88
		public override void SaveToFile()
		{
			for (;;)
			{
				IL_42:
				spr\u2561.ᜀ = this.ᜁ;
				int num = 2;
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
							if (Environment.UserInteractive)
							{
								if (true)
								{
								}
								num = 3;
								continue;
							}
							goto IL_9C;
						case 1:
							num = 0;
							continue;
						case 2:
							if (this.ᜁ)
							{
								num = 1;
								continue;
							}
							goto IL_9C;
						case 3:
							AboutDataExport.ShowAbout(false);
							goto IL_6E;
						case 4:
							goto IL_76;
						}
						goto IL_42;
					}
					IL_6E:
					num = 4;
				}
			}
			IL_76:
			IL_9C:
			base.SaveToFile();
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x000A7E38 File Offset: 0x000A6E38
		public void SaveToFile(string fileName)
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
			this.FileName = fileName;
			this.SaveToFile();
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x000A7E80 File Offset: 0x000A6E80
		public override void SaveToStream(Stream Stream)
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
			spr\u2561.ᜀ = this.ᜁ;
			base.SaveToStream(Stream);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x000A7ED0 File Offset: 0x000A6ED0
		public void SaveToHttpResponse(string FileName, HttpResponse response, SaveType saveType)
		{
			int a_ = 5;
			spr\u2561.ᜀ = this.ᜁ;
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				for (;;)
				{
					base.SaveToStream(memoryStream);
					TextExportType exportType = this.ExportType;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_13F;
						case 1:
							switch (exportType)
							{
							case TextExportType.TXT:
								base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("䀠匢唤䬦䀨䠪䰬嬮堰尲嬴ᠶ䴸䌺䤼", a_), response, saveType);
								num = 0;
								continue;
							case TextExportType.CSV:
								base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("䀠匢唤䬦䀨䠪䰬嬮堰尲嬴ᠶ娸䠺䬼", a_), response, saveType);
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_13F;
								default:
									if (false)
									{
									}
									num = 5;
									continue;
								}
								break;
							case TextExportType.DIF:
								base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("䀠匢唤䬦䀨䠪䰬嬮堰尲嬴ᠶ崸刺嬼", a_), response, saveType);
								num = 3;
								continue;
							case TextExportType.SYLK:
								base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("䀠匢唤䬦䀨䠪䰬嬮堰尲嬴ᠶ䨸䈺儼吾", a_), response, saveType);
								num = 4;
								continue;
							default:
								num = 6;
								continue;
							}
							break;
						case 2:
							goto IL_13F;
						case 3:
							goto IL_13F;
						case 4:
							goto IL_13F;
						case 5:
							if (true)
							{
							}
							goto IL_13F;
						case 6:
							num = 2;
							continue;
						case 7:
							goto IL_14A;
						}
						break;
						IL_13F:
						num = 7;
					}
				}
				IL_14A:;
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_183;
					case 1:
						((IDisposable)memoryStream).Dispose();
						num = 0;
						continue;
					}
					if (memoryStream == null)
					{
						break;
					}
					num = 1;
				}
				IL_183:;
			}
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x000A808C File Offset: 0x000A708C
		public void SaveToHttpResponse(string FileName, HttpResponse response)
		{
			int a_ = 9;
			spr\u2561.ᜀ = this.ᜁ;
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				for (;;)
				{
					base.SaveToStream(memoryStream);
					TextExportType exportType = this.ExportType;
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_14A;
						case 1:
							goto IL_13F;
						case 2:
							goto IL_13F;
						case 3:
							num = 4;
							continue;
						case 4:
							goto IL_13F;
						case 5:
							switch (exportType)
							{
							case TextExportType.TXT:
								base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("䐤圦夨䜪䐬䰮倰䜲尴堶圸ᐺ䤼䜾㕀", a_), response, SaveType.Attachment);
								num = 2;
								continue;
							case TextExportType.CSV:
								base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("䐤圦夨䜪䐬䰮倰䜲尴堶圸ᐺ帼䰾㝀", a_), response, SaveType.Attachment);
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_13F;
								default:
									if (false)
									{
									}
									num = 7;
									continue;
								}
								break;
							case TextExportType.DIF:
								base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("䐤圦夨䜪䐬䰮倰䜲尴堶圸ᐺ夼嘾❀", a_), response, SaveType.Attachment);
								num = 1;
								continue;
							case TextExportType.SYLK:
								base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("䐤圦夨䜪䐬䰮倰䜲尴堶圸ᐺ丼䘾ⵀ⡂", a_), response, SaveType.Attachment);
								num = 6;
								continue;
							default:
								num = 3;
								continue;
							}
							break;
						case 6:
							if (true)
							{
							}
							goto IL_13F;
						case 7:
							goto IL_13F;
						}
						break;
						IL_13F:
						num = 0;
					}
				}
				IL_14A:;
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						((IDisposable)memoryStream).Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_183;
					}
					if (memoryStream == null)
					{
						break;
					}
					num = 0;
				}
				IL_183:;
			}
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x000A8248 File Offset: 0x000A7248
		public void SaveToHttpResponse(HttpResponse response)
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
			spr\u2561.ᜀ = this.ᜁ;
			this.SaveToHttpResponse(this.FileName, response);
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x000A829C File Offset: 0x000A729C
		protected override void BeginDataExport()
		{
			int a_ = 11;
			switch (0)
			{
			default:
				for (;;)
				{
					base.BeginDataExport();
					this.AutoFitColWidth &= (this.ExportType == TextExportType.TXT);
					TextExportType exportType = this.ExportType;
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (base.DataSource == ExportSource.SqlCommand)
							{
								num = 10;
								continue;
							}
							goto IL_135;
						case 1:
							goto IL_4C5;
						case 2:
							goto IL_380;
						case 3:
							goto IL_3A9;
						case 4:
							switch (exportType)
							{
							case TextExportType.TXT:
								num = 8;
								continue;
							case TextExportType.CSV:
							{
								if (true)
								{
								}
								int num2 = 0;
								num = 7;
								continue;
							}
							case TextExportType.DIF:
								this.ᜅ = 0;
								base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("猦栨椪愬樮", a_));
								base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("ᜦԨᨪ", a_));
								base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("Ԧ", a_) + base.Title + HyperlinksCollectionEditor.b("Ԧ", a_));
								base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("焦氨株礬怮挰怲", a_));
								base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("ᜦԨ", a_) + base.ColumnsExport.Count.ToString());
								base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("Ԧନ", a_));
								base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("猦簨笪愬樮戰", a_));
								this.ᜄ = ExportState.Fetching;
								num = 0;
								continue;
							case TextExportType.SYLK:
								goto IL_515;
							default:
								num = 5;
								continue;
							}
							break;
						case 5:
							goto IL_A2;
						case 6:
						{
							int num2;
							if (num2 >= base.Header.Count)
							{
								num = 3;
								continue;
							}
							base.ᜀ().ᜇ(base.Header[num2]);
							num2++;
							num = 2;
							continue;
						}
						case 7:
							goto IL_380;
						case 8:
							if (base.Title.Length > 0)
							{
								num = 9;
								continue;
							}
							goto IL_4C5;
						case 9:
							goto IL_301;
						case 10:
							Monitor.Enter(this);
							spr\u2059.ᜀ = base.SQLCommand.ExecuteReader();
							num = 11;
							continue;
						case 11:
							goto IL_135;
						case 12:
							goto IL_4DD;
						}
						break;
						IL_301:
						base.ᜀ().ᜇ(base.Title);
						base.ᜀ().ᜀ('-', 80);
						base.ᜀ().ᜌ();
						num = 1;
						continue;
						try
						{
							IL_135:
							for (;;)
							{
								base.First();
								base.Skip(base.SkipRows);
								base.RowsCount = 0;
								num = 8;
								for (;;)
								{
									switch (num)
									{
									case 0:
										num = 3;
										continue;
									case 1:
										num = 4;
										continue;
									case 2:
										goto IL_293;
									case 3:
										if (!base.CanContinue())
										{
											num = 6;
											continue;
										}
										goto IL_21A;
									case 4:
										if (base.MaxRows != 0)
										{
											num = 13;
											continue;
										}
										goto IL_262;
									case 5:
										if (base.Stoped)
										{
											num = 0;
											continue;
										}
										goto IL_21A;
									case 6:
										num = 9;
										continue;
									case 7:
										goto IL_240;
									case 8:
										goto IL_240;
									case 9:
										goto IL_215;
									case 10:
										goto IL_287;
									case 11:
										if (!base.EndOfFile())
										{
											num = 1;
											continue;
										}
										goto IL_287;
									case 12:
										if (base.RowsCount >= base.MaxRows)
										{
											num = 10;
											continue;
										}
										goto IL_262;
									case 13:
										num = 12;
										continue;
									}
									break;
									IL_21A:
									base.Next();
									this.RaiseRecordFetched(base.RowsCount);
									Thread.Sleep(0);
									num = 7;
									continue;
									IL_240:
									num = 11;
									continue;
									IL_262:
									num = 5;
									continue;
									IL_287:
									num = 2;
								}
							}
							IL_215:
							return;
							IL_293:
							goto IL_A7;
						}
						finally
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_2FE;
								case 1:
									spr\u2059.ᜀ.Close();
									Monitor.Exit(this);
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_300;
									default:
										if (false)
										{
										}
										num = 0;
										continue;
									}
									break;
								}
								if (base.DataSource != ExportSource.SqlCommand)
								{
									break;
								}
								num = 1;
							}
							IL_2FE:
							IL_300:;
						}
						goto IL_301;
						IL_380:
						num = 6;
						continue;
						IL_4C5:
						IEnumerator enumerator = base.Header.GetEnumerator();
						num = 12;
					}
				}
				IL_A2:
				return;
				IL_A7:
				base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("ᜦԨ", a_) + this.ᜅ.ToString());
				base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("Ԧନ", a_));
				base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("挦栨缪氬", a_));
				base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("ᜦԨᬪ", a_));
				base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("Ԧନ", a_));
				return;
				IL_3A9:
				return;
				IL_4DD:
				try
				{
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 1:
						{
							IEnumerator enumerator;
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							string a_2 = (string)enumerator.Current;
							base.ᜀ().ᜇ(a_2);
							num = 0;
							continue;
						}
						case 2:
							goto IL_59F;
						case 4:
							num = 2;
							continue;
						}
						IL_57A:
						num = 1;
						continue;
						goto IL_57A;
					}
					IL_59F:
					return;
				}
				finally
				{
					for (;;)
					{
						IEnumerator enumerator;
						IDisposable disposable = enumerator as IDisposable;
						int num = 0;
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
								goto IL_5EB;
							case 1:
								goto IL_5E9;
							case 2:
								disposable.Dispose();
								num = 1;
								continue;
							}
							break;
						}
					}
					IL_5E9:
					IL_5EB:;
				}
				return;
				IL_515:
				base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("渦洨ဪ紬礮瀰怲瘴縶瀸縺攼漾เᅂᅄ", a_));
				return;
			}
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x000A88F4 File Offset: 0x000A78F4
		protected override string GetColumnTitle(int Index)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				string text;
				for (;;)
				{
					text = base.GetColumnTitle(Index);
					string str = string.Empty;
					TextExportType exportType = this.ExportType;
					int num = 13;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							ColumAlign colAlign;
							switch (colAlign)
							{
							case ColumAlign.Left:
								text = base.ᜀ().ᜁ(text, ' ', base.ColumnsExport[Index].Width);
								num = 1;
								continue;
							case ColumAlign.Center:
								if (true)
								{
								}
								text = base.ᜀ().ᜂ(text, ' ', base.ColumnsExport[Index].Width);
								num = 21;
								continue;
							case ColumAlign.Right:
								text = base.ᜀ().ᜀ(text, ' ', base.ColumnsExport[Index].Width);
								num = 11;
								continue;
							default:
								num = 9;
								continue;
							}
							break;
						}
						case 1:
							goto IL_2A2;
						case 2:
							if (this.ᜂ.AllowQuote)
							{
								num = 17;
								continue;
							}
							goto IL_32A;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_9D;
							default:
								if (false)
								{
								}
								text = spr\u2059.ᜀ(text, this.ᜂ.Quote);
								num = 20;
								continue;
							}
							break;
						case 4:
							return text;
						case 5:
							goto IL_2A2;
						case 6:
							return text;
						case 7:
							return text;
						case 8:
							return text;
						case 9:
							num = 5;
							continue;
						case 10:
							if (Index == 0)
							{
								num = 18;
								continue;
							}
							goto IL_21A;
						case 11:
							goto IL_2A2;
						case 12:
							goto IL_21A;
						case 13:
							goto IL_9D;
						case 14:
							num = 7;
							continue;
						case 15:
							if (this.ᜃ > 0)
							{
								num = 19;
								continue;
							}
							return text;
						case 16:
							if (this.ᜂ.Quote != '\0')
							{
								num = 3;
								continue;
							}
							goto IL_32A;
						case 17:
							num = 16;
							continue;
						case 18:
							str = HyperlinksCollectionEditor.b("̭įḱг㬵㈷砹猻樽䴿䡁", a_);
							num = 12;
							continue;
						case 19:
							text = base.ᜀ().ᜁ(text, ' ', text.Length + this.ᜃ);
							num = 4;
							continue;
						case 20:
							goto IL_32A;
						case 21:
							goto IL_2A2;
						case 22:
							return text;
						}
						break;
						IL_9D:
						switch (exportType)
						{
						case TextExportType.TXT:
						{
							ColumAlign colAlign = base.ColumnsExport[Index].ColAlign;
							num = 0;
							continue;
						}
						case TextExportType.CSV:
							num = 2;
							continue;
						case TextExportType.DIF:
							num = 10;
							continue;
						case TextExportType.SYLK:
							text = string.Concat(new string[]
							{
								HyperlinksCollectionEditor.b("洭ଯ樱", a_),
								Convert.ToString(Index + 1),
								HyperlinksCollectionEditor.b("ᔭ椯̱༳紵ᨷ", a_),
								text,
								HyperlinksCollectionEditor.b("భ㴯㠱", a_)
							});
							num = 22;
							continue;
						default:
							num = 14;
							continue;
						}
						IL_21A:
						text = str + HyperlinksCollectionEditor.b("Ἥᰯȱ㤳㰵ᨷ", a_) + text + HyperlinksCollectionEditor.b("భ㴯㠱", a_);
						num = 6;
						continue;
						IL_2A2:
						num = 15;
						continue;
						IL_32A:
						text += this.ᜂ.Separator;
						num = 8;
					}
				}
				return text;
			}
			}
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x000A8CB4 File Offset: 0x000A7CB4
		protected override string GetColumnValue(ColExport ExportColExport)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				string text;
				for (;;)
				{
					text = string.Empty;
					int columnIndex = ExportColExport.ColumnIndex;
					TextExportType exportType = this.ExportType;
					int num = 22;
					for (;;)
					{
						string text2;
						ColExportType colExportType;
						switch (num)
						{
						case 0:
							num = 34;
							continue;
						case 1:
							goto IL_57D;
						case 2:
							if (text2 != null)
							{
								num = 7;
								continue;
							}
							goto IL_2FC;
						case 3:
						{
							ColumAlign colAlign;
							switch (colAlign)
							{
							case ColumAlign.Left:
								text = base.ᜀ().ᜁ(text, ' ', base.ColumnsExport[columnIndex].Width);
								num = 33;
								continue;
							case ColumAlign.Center:
								text = base.ᜀ().ᜂ(text, ' ', base.ColumnsExport[columnIndex].Width);
								num = 1;
								continue;
							case ColumAlign.Right:
								text = base.ᜀ().ᜀ(text, ' ', base.ColumnsExport[columnIndex].Width);
								num = 26;
								continue;
							default:
								num = 0;
								continue;
							}
							break;
						}
						case 4:
							goto IL_17E;
						case 5:
							text += HyperlinksCollectionEditor.b("ШᨪĬἮ㰰㤲", a_);
							text += HyperlinksCollectionEditor.b("欨搪礬∮㬰", a_);
							num = 25;
							continue;
						case 6:
							return text;
						case 7:
							num = 24;
							continue;
						case 8:
						{
							string[] array;
							array[3] = Convert.ToString(base.RowsCount + 1 + (base.AddTitles ? 1 : 0) + (base.Options.InsertRowAfterTitle ? 1 : 0));
							array[4] = HyperlinksCollectionEditor.b("ረ怪༬", a_);
							array[5] = base.GetColumnValue(ExportColExport);
							array[6] = HyperlinksCollectionEditor.b("ନ☪✬", a_);
							text = string.Concat(array);
							num = 17;
							continue;
						}
						case 9:
							return text;
						case 10:
							text = text + string.Format(HyperlinksCollectionEditor.b("ᤨܪ嘬Ἦ䰰", a_), base.GetColumnValue(ExportColExport)) + HyperlinksCollectionEditor.b("␨K", a_);
							text += HyperlinksCollectionEditor.b("缨☪✬", a_);
							num = 29;
							continue;
						case 11:
							if (this.ᜂ.AllowQuote)
							{
								num = 28;
								continue;
							}
							goto IL_17E;
						case 12:
							return text;
						case 13:
							if (this.ᜂ.Quote != '\0')
							{
								goto IL_150;
							}
							goto IL_17E;
						case 14:
							if (this.ᜃ > 0)
							{
								num = 32;
								continue;
							}
							return text;
						case 15:
							if (columnIndex == 0)
							{
								num = 5;
								continue;
							}
							goto IL_1F6;
						case 16:
							if (colExportType != ColExportType.Binary)
							{
								num = 31;
								continue;
							}
							text += HyperlinksCollectionEditor.b("ᠨܪᴬ∮㬰", a_);
							text = text + HyperlinksCollectionEditor.b("ନ", a_) + base.GetColumnValue(ExportColExport) + HyperlinksCollectionEditor.b("ନ☪✬", a_);
							num = 12;
							continue;
						case 17:
							return text;
						case 18:
							if (colExportType != ColExportType.String)
							{
								num = 30;
								continue;
							}
							text += HyperlinksCollectionEditor.b("ᠨܪᴬ∮㬰", a_);
							text2 = base.GetColumnValue(ExportColExport);
							num = 2;
							continue;
						case 19:
							num = 9;
							continue;
						case 20:
							text = spr\u2059.ᜀ(text, this.ᜂ.Quote);
							num = 4;
							continue;
						case 21:
							goto IL_2FC;
						case 22:
							switch (exportType)
							{
							case TextExportType.TXT:
							{
								text = base.GetColumnValue(ExportColExport);
								ColumAlign colAlign = base.ColumnsExport[columnIndex].ColAlign;
								num = 3;
								continue;
							}
							case TextExportType.CSV:
								text = base.GetColumnValue(ExportColExport);
								num = 11;
								continue;
							case TextExportType.DIF:
								num = 15;
								continue;
							case TextExportType.SYLK:
							{
								string[] array = new string[7];
								array[0] = HyperlinksCollectionEditor.b("樨ဪ甬", a_);
								array[1] = Convert.ToString(columnIndex + 1);
								array[2] = HyperlinksCollectionEditor.b("ረ爪", a_);
								num = 8;
								continue;
							}
							default:
								num = 19;
								continue;
							}
							break;
						case 23:
							return text;
						case 24:
							if (text2.Length > 0)
							{
								num = 27;
								continue;
							}
							goto IL_2FC;
						case 25:
							goto IL_1F6;
						case 26:
							goto IL_57D;
						case 27:
							text2 = text2.Replace(HyperlinksCollectionEditor.b("ନ", a_), HyperlinksCollectionEditor.b("ନप", a_));
							num = 21;
							continue;
						case 28:
							num = 13;
							continue;
						case 29:
							return text;
						case 30:
							num = 16;
							continue;
						case 31:
							num = 10;
							continue;
						case 32:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_150;
							default:
								if (false)
								{
								}
								text = base.ᜀ().ᜁ(text, ' ', text.Length + this.ᜃ);
								num = 35;
								continue;
							}
							break;
						case 33:
							goto IL_57D;
						case 34:
							goto IL_57D;
						case 35:
							return text;
						}
						break;
						IL_150:
						num = 20;
						continue;
						IL_17E:
						text += this.ᜂ.Separator;
						num = 6;
						continue;
						IL_1F6:
						colExportType = base.ColumnsExport[columnIndex].ColExportType;
						if (true)
						{
						}
						num = 18;
						continue;
						IL_2FC:
						text = text + HyperlinksCollectionEditor.b("ନ", a_) + text2 + HyperlinksCollectionEditor.b("ନ☪✬", a_);
						num = 23;
						continue;
						IL_57D:
						num = 14;
					}
				}
				return text;
			}
			}
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x000A92A8 File Offset: 0x000A82A8
		protected override string GetCaptionRow()
		{
			int a_ = 11;
			string text;
			for (;;)
			{
				text = base.GetCaptionRow();
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 3;
						continue;
					case 1:
						text = text.Remove(text.Length - 1, 1);
						num = 2;
						continue;
					case 2:
						goto IL_82;
					case 3:
						if (text.Length > 0)
						{
							num = 1;
							continue;
						}
						goto IL_82;
					case 4:
						if (this.ᜁ != TextExportType.CSV)
						{
							if (true)
							{
							}
							num = 8;
							continue;
						}
						goto IL_C9;
					case 5:
						goto IL_C9;
					case 6:
						goto IL_10B;
					case 7:
						if (this.ᜁ == TextExportType.CSV)
						{
							num = 0;
							continue;
						}
						goto IL_82;
					case 8:
						goto IL_65;
					case 9:
						if (this.ᜁ == TextExportType.TXT)
						{
							num = 5;
							continue;
						}
						goto IL_10B;
					}
					break;
					IL_65:
					num = 9;
					continue;
					IL_10B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_65;
					default:
						goto IL_121;
					}
					IL_82:
					num = 4;
					continue;
					IL_C9:
					text += HyperlinksCollectionEditor.b("⨦⌨", a_);
					num = 6;
				}
			}
			IL_121:
			if (false)
			{
			}
			return text;
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x000A93E0 File Offset: 0x000A83E0
		protected override void WriteTitleRow()
		{
			if (true)
			{
			}
			for (;;)
			{
				string captionRow = this.GetCaptionRow();
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (captionRow != string.Empty)
						{
							num = 1;
							continue;
						}
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							base.ᜀ().ᜆ(captionRow);
							num = 2;
							continue;
						}
						break;
					case 2:
						return;
					}
					break;
				}
			}
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x000A946C File Offset: 0x000A846C
		protected override void WriteBlankRow()
		{
			int a_ = 4;
			for (;;)
			{
				TextExportType exportType = this.ExportType;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_7A;
					case 1:
						goto IL_7A;
					case 2:
						goto IL_9B;
					case 3:
					{
						int num2;
						if (num2 >= base.ExportRowExport.Count)
						{
							num = 2;
							continue;
						}
						base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("ᄟมᐣ", a_));
						base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("ȟ!", a_));
						num2++;
						num = 1;
						continue;
					}
					case 4:
						switch (exportType)
						{
						case TextExportType.TXT:
						case TextExportType.CSV:
							goto IL_12E;
						case TextExportType.DIF:
						{
							base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("ടጡࠣᘥ", a_));
							base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("戟洡瀣", a_));
							int num2 = 0;
							num = 0;
							continue;
						}
						default:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_12E;
							default:
								if (false)
								{
								}
								num = 5;
								continue;
							}
							break;
						}
						break;
					case 5:
						return;
					}
					break;
					IL_7A:
					num = 3;
				}
			}
			return;
			IL_9B:
			if (true)
			{
			}
			return;
			IL_12E:
			base.ᜀ().ᜇ(string.Empty);
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x000A95B8 File Offset: 0x000A85B8
		protected override string GetDataRow(bool NeedFormat)
		{
			int a_ = 11;
			string text;
			for (;;)
			{
				text = base.GetDataRow(NeedFormat);
				int num = 8;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜁ == TextExportType.TXT)
						{
							if (true)
							{
							}
							num = 4;
							continue;
						}
						goto IL_10F;
					case 1:
						num = 7;
						continue;
					case 2:
						goto IL_66;
					case 3:
						text = text.Remove(text.Length - 1, 1);
						num = 9;
						continue;
					case 4:
						goto IL_CD;
					case 5:
						if (this.ᜁ != TextExportType.CSV)
						{
							num = 2;
							continue;
						}
						goto IL_CD;
					case 6:
						goto IL_10F;
					case 7:
						if (text.Length > 0)
						{
							num = 3;
							continue;
						}
						goto IL_8B;
					case 8:
						if (this.ᜁ == TextExportType.CSV)
						{
							num = 1;
							continue;
						}
						goto IL_8B;
					case 9:
						goto IL_8B;
					}
					break;
					IL_66:
					num = 0;
					continue;
					IL_10F:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_66;
					default:
						goto IL_125;
					}
					IL_8B:
					num = 5;
					continue;
					IL_CD:
					text += HyperlinksCollectionEditor.b("⨦⌨", a_);
					num = 6;
				}
			}
			IL_125:
			if (false)
			{
			}
			return text;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x000A96F4 File Offset: 0x000A86F4
		protected override void WriteRow()
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
			base.ᜀ().ᜆ(this.GetDataRow(true));
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x000A9744 File Offset: 0x000A8744
		protected override void EndDataExport()
		{
			int a_ = 10;
			switch (0)
			{
			default:
				for (;;)
				{
					if (true)
					{
					}
					TextExportType exportType = this.ExportType;
					int num = 5;
					for (;;)
					{
						IEnumerator enumerator;
						switch (num)
						{
						case 0:
							goto IL_1E3;
						case 1:
							goto IL_D8;
						case 2:
							try
							{
								num = 4;
								for (;;)
								{
									switch (num)
									{
									case 0:
										num = 3;
										continue;
									case 2:
									{
										if (!enumerator.MoveNext())
										{
											num = 0;
											continue;
										}
										string a_2 = (string)enumerator.Current;
										base.ᜀ().ᜇ(a_2);
										num = 1;
										continue;
									}
									case 3:
										goto IL_258;
									}
									IL_233:
									num = 2;
									continue;
									goto IL_233;
								}
								IL_258:
								goto IL_2DE;
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
											goto IL_2C0;
										case 1:
											switch ((1 == 1) ? 1 : 0)
											{
											case 0:
											case 2:
												goto IL_2C0;
											default:
												if (false)
												{
												}
												disposable.Dispose();
												num = 2;
												continue;
											}
											break;
										case 2:
											goto IL_2BE;
										}
										break;
									}
								}
								IL_2BE:
								IL_2C0:;
							}
							goto IL_2C1;
						case 3:
							goto IL_1AD;
						case 4:
							goto IL_1B9;
						case 5:
							switch (exportType)
							{
							case TextExportType.TXT:
								goto IL_2C1;
							case TextExportType.CSV:
							{
								IEnumerator enumerator2 = base.Footer.GetEnumerator();
								num = 6;
								continue;
							}
							case TextExportType.DIF:
								base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("ଥᤧةᰫ", a_));
								base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("挥朧温", a_));
								num = 1;
								continue;
							case TextExportType.SYLK:
								base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("挥", a_));
								num = 0;
								continue;
							default:
								num = 3;
								continue;
							}
							break;
						case 6:
							try
							{
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 2:
										num = 3;
										continue;
									case 3:
										goto IL_15F;
									case 4:
									{
										IEnumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num = 2;
											continue;
										}
										string str = (string)enumerator2.Current;
										base.ᜀ().ᜇ(str + this.ᜂ.Separator);
										num = 1;
										continue;
									}
									}
									IL_105:
									num = 4;
									continue;
									goto IL_105;
								}
								IL_15F:
								goto IL_2DE;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator2;
									IDisposable disposable2 = enumerator2 as IDisposable;
									num = 0;
									for (;;)
									{
										switch (num)
										{
										case 0:
											if (disposable2 != null)
											{
												num = 2;
												continue;
											}
											goto IL_1AC;
										case 1:
											goto IL_1AA;
										case 2:
											disposable2.Dispose();
											num = 1;
											continue;
										}
										break;
									}
								}
								IL_1AA:
								IL_1AC:;
							}
							goto IL_1AD;
						}
						break;
						IL_1AD:
						num = 4;
						continue;
						IL_2C1:
						enumerator = base.Footer.GetEnumerator();
						num = 2;
					}
				}
				IL_D8:
				IL_1B9:
				IL_1E3:
				IL_2DE:
				base.EndDataExport();
				return;
			}
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x000A9A54 File Offset: 0x000A8A54
		protected virtual void RaiseRecordFetched(int RecNo)
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
			base.ᜂ(this, RecNo);
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x000A9A98 File Offset: 0x000A8A98
		public override void Stop()
		{
			int a_ = 15;
			switch (0)
			{
			default:
				for (;;)
				{
					TextExportType textExportType = this.ᜁ;
					int num = 9;
					for (;;)
					{
						ExportState exportState;
						switch (num)
						{
						case 0:
							goto IL_232;
						case 1:
							try
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										IEnumerator enumerator;
										if (!enumerator.MoveNext())
										{
											num = 4;
											continue;
										}
										string a_2 = (string)enumerator.Current;
										base.ᜀ().ᜇ(a_2);
										num = 3;
										continue;
									}
									case 2:
										goto IL_2A9;
									case 4:
										num = 2;
										continue;
									}
									IL_283:
									num = 0;
									continue;
									goto IL_283;
								}
								IL_2A9:
								goto IL_37E;
							}
							finally
							{
								for (;;)
								{
									IL_2C3:
									IEnumerator enumerator;
									IDisposable disposable = enumerator as IDisposable;
									for (;;)
									{
										num = 2;
										for (;;)
										{
											switch (num)
											{
											case 0:
												goto IL_2F4;
											case 1:
												disposable.Dispose();
												num = 0;
												continue;
											case 2:
												if (disposable != null)
												{
													num = 1;
													continue;
												}
												goto IL_312;
											}
											goto IL_2C3;
										}
										IL_2F4:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											break;
										default:
											goto IL_30A;
										}
									}
								}
								IL_30A:
								if (false)
								{
								}
								IL_312:;
							}
							goto IL_313;
						case 2:
							base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("渪", a_));
							num = 0;
							continue;
						case 3:
							goto IL_106;
						case 4:
							goto IL_313;
						case 5:
							if (this.ᜄ == ExportState.Writting)
							{
								num = 2;
								continue;
							}
							goto IL_37E;
						case 6:
							goto IL_CC;
						case 7:
							num = 3;
							continue;
						case 8:
							goto IL_31F;
						case 9:
							switch (textExportType)
							{
							case TextExportType.TXT:
								goto IL_1DB;
							case TextExportType.CSV:
							{
								if (true)
								{
								}
								base.ᜀ().ᜌ();
								IEnumerator enumerator2 = base.Footer.GetEnumerator();
								num = 10;
								continue;
							}
							case TextExportType.DIF:
								base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("تᰬ̮İ", a_));
								base.ᜀ().ᜇ(HyperlinksCollectionEditor.b("渪戬欮", a_));
								num = 6;
								continue;
							case TextExportType.SYLK:
								num = 5;
								continue;
							default:
								num = 4;
								continue;
							}
							break;
						case 10:
							try
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 2:
										goto IL_18D;
									case 3:
										num = 2;
										continue;
									case 4:
									{
										IEnumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num = 3;
											continue;
										}
										string str = (string)enumerator2.Current;
										base.ᜀ().ᜇ(str + this.ᜂ.Separator);
										num = 0;
										continue;
									}
									}
									IL_167:
									num = 4;
									continue;
									goto IL_167;
								}
								IL_18D:
								goto IL_37E;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator2;
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
											goto IL_1DA;
										case 2:
											goto IL_1D8;
										}
										break;
									}
								}
								IL_1D8:
								IL_1DA:;
							}
							goto IL_1DB;
						case 11:
							switch (exportState)
							{
							case ExportState.Fetching:
								goto IL_37E;
							case ExportState.Writting:
							{
								base.ᜀ().ᜌ();
								IEnumerator enumerator = base.Footer.GetEnumerator();
								num = 1;
								continue;
							}
							default:
								num = 7;
								continue;
							}
							break;
						}
						break;
						IL_1DB:
						exportState = this.ᜄ;
						num = 11;
						continue;
						IL_313:
						num = 8;
					}
				}
				IL_CC:
				IL_106:
				IL_232:
				IL_31F:
				IL_37E:
				base.Stop();
				return;
			}
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x000A9E48 File Offset: 0x000A8E48
		protected override void SaveProperties(XMLFile File)
		{
			int a_ = 16;
			int num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_2F3:
				File.RemoveSection(HyperlinksCollectionEditor.b("笫札琯昱簳", a_));
				num = 0;
				num2 = 2;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				switch (0)
				{
				default:
					goto IL_64;
				}
				break;
			}
			int num3;
			for (;;)
			{
				IL_3D:
				switch (num2)
				{
				case 0:
					return;
				case 1:
					if (num >= this.ColumnsWidth.Count)
					{
						num2 = 0;
						continue;
					}
					File.WriteValue(HyperlinksCollectionEditor.b("笫札琯昱簳", a_), string.Format(HyperlinksCollectionEditor.b("圫ḭ䴯䤱Գ䬵", a_), HyperlinksCollectionEditor.b("䀫䜭帯圱", a_), num), this.ColumnsWidth[num]);
					num++;
					num2 = 5;
					continue;
				case 2:
					goto IL_29F;
				case 3:
					goto IL_2F3;
				case 4:
					goto IL_2CA;
				case 5:
					goto IL_29F;
				case 6:
					if (num3 >= this.ColumnsAlign.Count)
					{
						num2 = 3;
						continue;
					}
					File.WriteValue(HyperlinksCollectionEditor.b("洫戭礯由稳", a_), string.Format(HyperlinksCollectionEditor.b("圫ḭ䴯䤱Գ䬵", a_), HyperlinksCollectionEditor.b("䀫䜭帯圱", a_), num3), this.ColumnsAlign[num3]);
					num3++;
					num2 = 7;
					continue;
				case 7:
					goto IL_2CA;
				}
				goto IL_64;
				IL_29F:
				num2 = 1;
				continue;
				IL_2CA:
				num2 = 6;
			}
			return;
			IL_64:
			base.SaveProperties(File);
			File.WriteValue(HyperlinksCollectionEditor.b("砫瘭搯", a_), HyperlinksCollectionEditor.b("椫嘭䀯崱䘳䈵氷䌹䰻嬽", a_), ((int)this.ExportType).ToString());
			File.WriteValue(HyperlinksCollectionEditor.b("砫瘭搯", a_), HyperlinksCollectionEditor.b("漫䄭崯弱唳", a_), this.CSVOption.Separator);
			File.WriteValue(HyperlinksCollectionEditor.b("砫瘭搯", a_), HyperlinksCollectionEditor.b("紫嬭弯䘱儳", a_), this.CSVOption.Quote.ToString());
			File.WriteValue(HyperlinksCollectionEditor.b("砫瘭搯", a_), HyperlinksCollectionEditor.b("紫嬭弯䘱儳攵䰷䠹唻倽✿ㅁ", a_), this.CSVOption.AllowQuote.ToString());
			File.WriteValue(HyperlinksCollectionEditor.b("砫瘭搯", a_), HyperlinksCollectionEditor.b("缫席儯儱崳堵強", a_), this.TXTSpacing.ToString());
			File.WriteValue(HyperlinksCollectionEditor.b("砫瘭搯", a_), HyperlinksCollectionEditor.b("洫嬭䐯崱爳張䰷礹医刽᜿⭁⁃㉅⁇", a_), this.AutoFitColWidth.ToString());
			File.RemoveSection(HyperlinksCollectionEditor.b("洫戭礯由稳", a_));
			num3 = 0;
			num2 = 4;
			goto IL_3D;
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x000AA150 File Offset: 0x000A9150
		protected override void LoadProperties(XMLFile File)
		{
			int a_ = 18;
			switch (0)
			{
			default:
				for (;;)
				{
					base.LoadProperties(File);
					this.ExportType = (TextExportType)Convert.ToInt32(File.ReadValue(HyperlinksCollectionEditor.b("稭栯昱", a_), HyperlinksCollectionEditor.b("欭䠯䈱嬳䐵䰷渹䔻丽┿", a_), ((int)this.ExportType).ToString()));
					this.CSVOption.Separator = File.ReadValue(HyperlinksCollectionEditor.b("稭栯昱", a_), HyperlinksCollectionEditor.b("洭弯弱夳圵", a_), this.CSVOption.Separator);
					this.CSVOption.Quote = Convert.ToChar(File.ReadValue(HyperlinksCollectionEditor.b("稭栯昱", a_), HyperlinksCollectionEditor.b("缭䔯崱䀳匵", a_), this.CSVOption.Quote.ToString()));
					this.CSVOption.AllowQuote = Convert.ToBoolean(File.ReadValue(HyperlinksCollectionEditor.b("稭栯昱", a_), HyperlinksCollectionEditor.b("缭䔯崱䀳匵欷丹主圽⸿╁㝃", a_), this.CSVOption.AllowQuote.ToString()));
					this.TXTSpacing = Convert.ToInt32(File.ReadValue(HyperlinksCollectionEditor.b("稭栯昱", a_), HyperlinksCollectionEditor.b("紭䀯匱圳張嘷崹", a_), this.TXTSpacing.ToString()));
					this.AutoFitColWidth = Convert.ToBoolean(File.ReadValue(HyperlinksCollectionEditor.b("稭栯昱", a_), HyperlinksCollectionEditor.b("漭䔯䘱嬳瀵儷丹缻儽ⰿᕁⵃ≅㱇≉", a_), this.AutoFitColWidth.ToString()));
					Array array = null;
					this.ColumnsAlign.Clear();
					File.ReadValues(HyperlinksCollectionEditor.b("漭簯笱猳砵", a_), ref array);
					int num = 10;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							this.ColumnsAlign.SetStrings(array as string[]);
							int num2 = 0;
							num = 2;
							continue;
						}
						case 1:
						{
							int num3;
							if (num3 >= this.ColumnsWidth.Count)
							{
								num = 7;
								continue;
							}
							this.ColumnsWidth[num3] = File.ReadValue(HyperlinksCollectionEditor.b("礭礯瘱怳縵", a_), this.ColumnsWidth[num3], string.Empty);
							num3++;
							num = 8;
							continue;
						}
						case 2:
							if (true)
							{
							}
							goto IL_37C;
						case 3:
						{
							int num2;
							if (num2 >= this.ColumnsAlign.Count)
							{
								num = 9;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2E2;
							default:
								if (false)
								{
								}
								this.ColumnsAlign[num2] = File.ReadValue(HyperlinksCollectionEditor.b("漭簯笱猳砵", a_), this.ColumnsAlign[num2], string.Empty);
								num2++;
								num = 6;
								continue;
							}
							break;
						}
						case 4:
						{
							this.ColumnsWidth.SetStrings(array as string[]);
							int num3 = 0;
							num = 11;
							continue;
						}
						case 5:
							if (array != null)
							{
								num = 4;
								continue;
							}
							return;
						case 6:
							goto IL_37C;
						case 7:
							return;
						case 8:
							goto IL_2E2;
						case 9:
							goto IL_23F;
						case 10:
							if (array != null)
							{
								num = 0;
								continue;
							}
							goto IL_23F;
						case 11:
							goto IL_2E2;
						}
						break;
						IL_23F:
						this.ColumnsWidth.Clear();
						File.ReadValues(HyperlinksCollectionEditor.b("礭礯瘱怳縵", a_), ref array);
						num = 5;
						continue;
						IL_2E2:
						num = 1;
						continue;
						IL_37C:
						num = 3;
					}
				}
				return;
			}
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x000AA508 File Offset: 0x000A9508
		private bool ᜂ()
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
			return string.Compare(this.ᜂ.Separator, spr\u1C2B.ᡜ) != 0;
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x000AA560 File Offset: 0x000A9560
		private new void ᜁ()
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
			this.ᜂ.Separator = spr\u1C2B.ᡜ;
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x000AA5AC File Offset: 0x000A95AC
		// (set) Token: 0x06000FC1 RID: 4033 RVA: 0x000AA5F0 File Offset: 0x000A95F0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(TextExportType.TXT)]
		[Description("Gets or sets the format of the output export file.")]
		public TextExportType ExportType
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
							continue;
						}
						if (false)
						{
						}
						if (true)
						{
						}
						this.ᜁ = value;
						num = 2;
						continue;
					case 2:
						return;
					}
					if (value == this.ᜁ)
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x000AA66C File Offset: 0x000A966C
		// (set) Token: 0x06000FC3 RID: 4035 RVA: 0x000AA6B0 File Offset: 0x000A96B0
		[Description("Gets or sets options of format if the format type equals CSV.")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(true)]
		public CSVOption CSVOption
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
				return this.ᜂ;
			}
			set
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
				this.ᜂ = value;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x000AA6F4 File Offset: 0x000A96F4
		// (set) Token: 0x06000FC5 RID: 4037 RVA: 0x000AA738 File Offset: 0x000A9738
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(1)]
		[Description("Gets or sets internal columns spacing in the result file.")]
		public int TXTSpacing
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
				int num = 2;
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
						}
						if (false)
						{
						}
						if (true)
						{
						}
						this.ᜃ = value;
						num = 1;
						continue;
					case 1:
						return;
					}
					if (value == this.ᜃ)
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x000AA7B4 File Offset: 0x000A97B4
		// (set) Token: 0x06000FC7 RID: 4039 RVA: 0x000AA7F8 File Offset: 0x000A97F8
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new bool AutoFitColWidth
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
				return base.AutoFitColWidth;
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
				base.AutoFitColWidth = value;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x000AA83C File Offset: 0x000A983C
		// (set) Token: 0x06000FC9 RID: 4041 RVA: 0x000AA880 File Offset: 0x000A9880
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		public new StringListCollection ColumnsAlign
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
				return base.ColumnsAlign;
			}
			set
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
				base.ColumnsAlign = value;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x000AA8C4 File Offset: 0x000A98C4
		// (set) Token: 0x06000FCB RID: 4043 RVA: 0x000AA908 File Offset: 0x000A9908
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		public new StringListCollection ColumnsWidth
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
				return base.ColumnsWidth;
			}
			set
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
				base.ColumnsWidth = value;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x000AA94C File Offset: 0x000A994C
		// (set) Token: 0x06000FCD RID: 4045 RVA: 0x000AA990 File Offset: 0x000A9990
		[Description("Allows you to select string fields that will not be truncated by occurrences of carriage returns.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		public new StringListCollection NotTruncatableColumns
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
				return base.NotTruncatableColumns;
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
				base.NotTruncatableColumns = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x000AA9D4 File Offset: 0x000A99D4
		// (set) Token: 0x06000FCF RID: 4047 RVA: 0x000AAA18 File Offset: 0x000A9A18
		[Editor(typeof(TXTFileNameEditor), typeof(UITypeEditor))]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		public new string FileName
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
				return base.FileName;
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
				base.FileName = value;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x000AAA5C File Offset: 0x000A9A5C
		// (set) Token: 0x06000FD1 RID: 4049 RVA: 0x000AAAA0 File Offset: 0x000A9AA0
		[DefaultValue(EncodingType.UTF8)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new EncodingType DataEncoding
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
				return base.DataEncoding;
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
				base.DataEncoding = value;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x000AAAE4 File Offset: 0x000A9AE4
		// (set) Token: 0x06000FD3 RID: 4051 RVA: 0x000AAB28 File Offset: 0x000A9B28
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(false)]
		[Browsable(true)]
		[Description("Indicate whether export long char/binary column.")]
		public new bool ExportLongColumn
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
				return base.ExportLongColumn;
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
				base.ExportLongColumn = value;
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000FD4 RID: 4052 RVA: 0x000AAB6C File Offset: 0x000A9B6C
		// (remove) Token: 0x06000FD5 RID: 4053 RVA: 0x000AABB0 File Offset: 0x000A9BB0
		public new event DataRowEventHandler FetchedRecord
		{
			add
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
				base.FetchedRecord += value;
			}
			remove
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
				base.FetchedRecord -= value;
			}
		}

		// Token: 0x04000B8C RID: 2956
		private string \u2609\u00AF\u00A6\u008B;

		// Token: 0x04000B8D RID: 2957
		private new License ᜀ;

		// Token: 0x04000B8E RID: 2958
		private bool \u25D9\u00A9\u009F\u00A9;

		// Token: 0x04000B8F RID: 2959
		private new TextExportType ᜁ;

		// Token: 0x04000B90 RID: 2960
		private new CSVOption ᜂ;

		// Token: 0x04000B91 RID: 2961
		private int[] \u2609\u008A\u00AB\u0085;

		// Token: 0x04000B92 RID: 2962
		private long[] \u25D9\u00A3\u0096\u00A4;

		// Token: 0x04000B93 RID: 2963
		private new int ᜃ = 1;

		// Token: 0x04000B94 RID: 2964
		private new ExportState ᜄ;

		// Token: 0x04000B95 RID: 2965
		private int ᜅ;
	}
}
