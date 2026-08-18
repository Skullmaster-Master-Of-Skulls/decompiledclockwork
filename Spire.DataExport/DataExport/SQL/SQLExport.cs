using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Text;
using System.Web;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.Common;
using Spire.DataExport.Forms;
using Spire.DataExport.License;
using Spire.DataExport.PropEditors;

namespace Spire.DataExport.SQL
{
	// Token: 0x02000178 RID: 376
	[ToolboxItem(true)]
	[LicenseProvider(typeof(DataExportLicenseProvider))]
	public class SQLExport : FormatTextSqlExport
	{
		// Token: 0x060009FD RID: 2557 RVA: 0x00065D04 File Offset: 0x00064D04
		protected override void InitializeVariables()
		{
			int a_ = 18;
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
			base.DataFormats.NullString = HyperlinksCollectionEditor.b("怭支縱砳", a_);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00065D84 File Offset: 0x00064D84
		protected override void Dispose(bool disposing)
		{
			try
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
							goto IL_4C;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						goto IL_7A;
					case 2:
						if (true)
						{
						}
						goto IL_72;
					case 3:
						goto IL_4C;
					}
					if (this.ᜀ != null)
					{
						num = 3;
						continue;
					}
					goto IL_72;
					IL_4C:
					this.ᜀ.Dispose();
					this.ᜀ = null;
					num = 2;
					continue;
					IL_72:
					num = 1;
				}
				IL_7A:;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00065E30 File Offset: 0x00064E30
		public override void SaveToFile()
		{
			for (;;)
			{
				spr\u2561.ᜀ = this.ᜁ;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						goto IL_76;
					case 2:
						goto IL_80;
					case 3:
						AboutDataExport.ShowAbout(false);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_80;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 4:
						if (this.ᜁ)
						{
							num = 0;
							continue;
						}
						goto IL_9C;
					}
					break;
					IL_80:
					if (!Environment.UserInteractive)
					{
						goto IL_9C;
					}
					if (true)
					{
					}
					num = 3;
				}
			}
			IL_76:
			IL_9C:
			base.SaveToFile();
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00065EE0 File Offset: 0x00064EE0
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

		// Token: 0x06000A01 RID: 2561 RVA: 0x00065F28 File Offset: 0x00064F28
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

		// Token: 0x06000A02 RID: 2562 RVA: 0x00065F78 File Offset: 0x00064F78
		public void SaveToHttpResponse(string FileName, HttpResponse response, SaveType saveType)
		{
			int a_ = 5;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				spr\u2561.ᜀ = this.ᜁ;
				MemoryStream memoryStream = new MemoryStream();
				try
				{
					base.SaveToStream(memoryStream);
					base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("䀠匢唤䬦䀨䠪䰬嬮堰尲嬴ᠶ䴸䌺䤼", a_), response, saveType);
				}
				finally
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_A2;
						case 2:
							((IDisposable)memoryStream).Dispose();
							num = 0;
							continue;
						}
						if (memoryStream == null)
						{
							break;
						}
						num = 2;
					}
					IL_A2:;
				}
				break;
			}
			}
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0006603C File Offset: 0x0006503C
		public void SaveToHttpResponse(string FileName, HttpResponse response)
		{
			int a_ = 19;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (false)
			{
			}
			spr\u2561.ᜀ = this.ᜁ;
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				if (true)
				{
				}
				base.SaveToStream(memoryStream);
				base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("丮䄰䌲头帶娸娺䤼嘾⹀ⵂ橄㍆ㅈ㽊", a_), response, SaveType.Attachment);
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A2;
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
				IL_A2:;
			}
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00066100 File Offset: 0x00065100
		public void SaveToHttpResponse(HttpResponse response)
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
			spr\u2561.ᜀ = this.ᜁ;
			this.SaveToHttpResponse(this.FileName, response);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00066154 File Offset: 0x00065154
		protected override void BeginDataExport()
		{
			int a_ = 4;
			switch (0)
			{
			default:
			{
				StringBuilder stringBuilder;
				for (;;)
				{
					base.BeginDataExport();
					IEnumerator enumerator = base.Header.GetEnumerator();
					int num = 4;
					for (;;)
					{
						if (true)
						{
						}
						IEnumerator enumerator2;
						IEnumerator enumerator3;
						switch (num)
						{
						case 0:
							try
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										if (!enumerator2.MoveNext())
										{
											num = 1;
											continue;
										}
										ColumnExport columnExport = (ColumnExport)enumerator2.Current;
										stringBuilder.AppendFormat(HyperlinksCollectionEditor.b("\u001fȡ弣ᘥ唧਩ఫ唭į伱", a_), columnExport.Name, columnExport.SQLType);
										stringBuilder.Append(HyperlinksCollectionEditor.b("ట⼡⸣", a_));
										num = 4;
										continue;
									}
									case 1:
										num = 3;
										continue;
									case 3:
										goto IL_2B3;
									}
									IL_287:
									num = 0;
									continue;
									goto IL_287;
								}
								IL_2B3:
								goto IL_32C;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable = enumerator2 as IDisposable;
									num = 1;
									for (;;)
									{
										switch (num)
										{
										case 0:
											disposable.Dispose();
											num = 2;
											continue;
										case 1:
											if (disposable != null)
											{
												num = 0;
												continue;
											}
											goto IL_2FD;
										case 2:
											goto IL_2FB;
										}
										break;
									}
								}
								IL_2FB:
								IL_2FD:;
							}
							goto IL_2FE;
							IL_32C:
							stringBuilder.Remove(stringBuilder.Length - 3, 3);
							stringBuilder.Append(HyperlinksCollectionEditor.b("टᤡ⤣Ⱕ", a_));
							base.ᜀ().ᜇ(stringBuilder.ToString());
							num = 3;
							continue;
						case 1:
							if (stringBuilder.Length > 0)
							{
								num = 8;
								continue;
							}
							goto IL_492;
						case 2:
							goto IL_433;
						case 3:
							goto IL_1B7;
						case 4:
							try
							{
								num = 4;
								for (;;)
								{
									switch (num)
									{
									case 1:
									{
										if (!enumerator.MoveNext())
										{
											num = 2;
											continue;
										}
										string a_2 = (string)enumerator.Current;
										base.ᜀ().ᜇ(a_2);
										num = 0;
										continue;
									}
									case 2:
										num = 3;
										continue;
									case 3:
										goto IL_3E5;
									}
									IL_39B:
									num = 1;
									continue;
									goto IL_39B;
								}
								IL_3E5:
								goto IL_167;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable2 = enumerator as IDisposable;
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
											goto IL_432;
										case 2:
											goto IL_430;
										}
										break;
									}
								}
								IL_430:
								IL_432:;
							}
							goto IL_433;
						case 5:
							goto IL_2FE;
						case 6:
							try
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										if (!enumerator3.MoveNext())
										{
											num = 3;
											continue;
										}
										ColumnExport columnExport2 = (ColumnExport)enumerator3.Current;
										stringBuilder.AppendFormat(HyperlinksCollectionEditor.b("嬟ሡ夣崥ᤧ圩", a_), columnExport2.Name, ',');
										num = 1;
										continue;
									}
									case 3:
										num = 4;
										continue;
									case 4:
										goto IL_FD;
									}
									IL_D7:
									num = 0;
									continue;
									goto IL_D7;
								}
								IL_FD:
								goto IL_191;
							}
							finally
							{
								for (;;)
								{
									IL_133:
									IDisposable disposable3 = enumerator3 as IDisposable;
									num = 2;
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
												goto IL_164;
											case 1:
												disposable3.Dispose();
												num = 0;
												continue;
											case 2:
												if (disposable3 != null)
												{
													goto IL_149;
												}
												goto IL_166;
											}
											goto IL_133;
										}
										IL_149:
										num = 1;
									}
								}
								IL_164:
								IL_166:;
							}
							goto IL_167;
							IL_191:
							num = 1;
							continue;
						case 7:
							if (this.ᜆ.Length > 0)
							{
								num = 2;
								continue;
							}
							goto IL_1B7;
						case 8:
							stringBuilder.Remove(stringBuilder.Length - 1, 1);
							num = 9;
							continue;
						case 9:
							goto IL_20C;
						case 10:
							if (this.ᜂ)
							{
								num = 5;
								continue;
							}
							goto IL_1B7;
						}
						break;
						IL_167:
						stringBuilder = null;
						num = 10;
						continue;
						IL_1B7:
						stringBuilder = new StringBuilder(base.ColumnsExport.Count);
						enumerator3 = base.ColumnsExport.GetEnumerator();
						num = 6;
						continue;
						IL_2FE:
						num = 7;
						continue;
						IL_433:
						base.ᜀ().ᜇ(string.Format(HyperlinksCollectionEditor.b("挟瀡愣朥簧漩ఫ稭焯瀱砳猵ᠷ䄹఻䌽", a_) + '(', this.ᜆ));
						stringBuilder = new StringBuilder(base.ColumnsExport.Count);
						enumerator2 = base.ColumnsExport.GetEnumerator();
						num = 0;
					}
				}
				IL_20C:
				IL_492:
				this.ᜇ = stringBuilder.ToString();
				return;
			}
			}
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00066628 File Offset: 0x00065628
		protected override string GetColumnValue(ColExport ExportColExport)
		{
			int a_ = 12;
			string text;
			for (;;)
			{
				text = string.Empty;
				int columnIndex = ExportColExport.ColumnIndex;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (base.Culture.NumberFormat.NumberDecimalSeparator != HyperlinksCollectionEditor.b("ا", a_))
						{
							num = 1;
							continue;
						}
						goto IL_2F5;
					case 1:
						text = text.Replace(base.Culture.NumberFormat.NumberDecimalSeparator, HyperlinksCollectionEditor.b("ا", a_));
						num = 13;
						continue;
					case 2:
					{
						if (base.ColumnsExport.GetColumnIsNull(columnIndex, spr\u2059.ᜀ))
						{
							num = 10;
							continue;
						}
						text = base.GetColumnValue(ExportColExport);
						ColExportType colExportType = base.ColumnsExport[columnIndex].ColExportType;
						num = 17;
						continue;
					}
					case 3:
						goto IL_98;
					case 4:
						goto IL_1B1;
					case 5:
						if (string.Compare(HyperlinksCollectionEditor.b("簧堩夫䬭", a_), base.GetColumnValue(ExportColExport).Trim(), true) != 0)
						{
							num = 18;
							continue;
						}
						goto IL_98;
					case 6:
						text = base.DataFormats.NullString;
						num = 4;
						continue;
					case 7:
						goto IL_2F0;
					case 8:
						goto IL_281;
					case 9:
						goto IL_AF;
					case 10:
						num = 11;
						continue;
					case 11:
						if (base.ColumnsExport[columnIndex].ColExportType == ColExportType.String)
						{
							num = 6;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2F0;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							text = HyperlinksCollectionEditor.b("昧缩怫戭", a_);
							num = 12;
							continue;
						}
						break;
					case 12:
						goto IL_139;
					case 13:
						goto IL_223;
					case 14:
						goto IL_233;
					case 15:
						if (string.Compare(base.DataFormats.BooleanTrue, base.GetColumnValue(ExportColExport).Trim(), true) == 0)
						{
							num = 3;
							continue;
						}
						text = base.DataFormats.BooleanFalse;
						num = 8;
						continue;
					case 16:
						goto IL_297;
					case 17:
					{
						ColExportType colExportType;
						switch (colExportType)
						{
						case ColExportType.Float:
						case ColExportType.Currency:
							text = text.Replace(base.Culture.NumberFormat.NumberGroupSeparator, string.Empty);
							num = 0;
							continue;
						case ColExportType.DateTime:
						case ColExportType.Time:
						case ColExportType.String:
						case ColExportType.Guid:
							text = spr\u2059.ᜀ(text, '\'');
							num = 16;
							continue;
						case ColExportType.Boolean:
							num = 5;
							continue;
						default:
							num = 7;
							continue;
						}
						break;
					}
					case 18:
						num = 15;
						continue;
					}
					break;
					IL_98:
					text = base.DataFormats.BooleanTrue;
					num = 9;
					continue;
					IL_2F0:
					num = 14;
				}
			}
			IL_AF:
			IL_139:
			IL_1B1:
			IL_223:
			IL_233:
			IL_281:
			IL_297:
			IL_2F5:
			return text + ',';
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0006693C File Offset: 0x0006593C
		protected override string GetDataRow(bool NeedFormat)
		{
			int a_ = 13;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(HyperlinksCollectionEditor.b("怨攪縬樮挰朲ᔴ縶眸漺爼Ἶ㩀獂㡄䩆䍈歊浌李⩐扒⡔繖员党ଡ଼Ṟⵠ㙢⁤㑦", a_), this.ᜆ, this.ᜇ);
			stringBuilder.AppendFormat(HyperlinksCollectionEditor.b("刨ᬪ倬吮0串", a_), '(', base.GetDataRow(NeedFormat));
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			stringBuilder.AppendFormat(HyperlinksCollectionEditor.b("刨ᬪ倬吮0串", a_), ')', this.ᜃ);
			return stringBuilder.ToString();
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00066A08 File Offset: 0x00065A08
		protected override void WriteRow()
		{
			for (;;)
			{
				base.ᜀ().ᜇ(this.GetDataRow(this.ᜈ));
				base.ᜀ().ᜌ();
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜅ.Length > 0)
						{
							num = 1;
							continue;
						}
						return;
					case 1:
						base.ᜀ().ᜇ(this.ᜅ);
						base.ᜀ().ᜌ();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4E;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						break;
					case 2:
						goto IL_4E;
					case 3:
						num = 0;
						continue;
					case 4:
						if (base.RowsCount % this.ᜄ == 0)
						{
							num = 3;
							continue;
						}
						return;
					case 5:
						if (true)
						{
						}
						num = 4;
						continue;
					case 6:
						return;
					}
					break;
					IL_4E:
					if (this.ᜄ <= 0)
					{
						return;
					}
					num = 5;
				}
			}
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00066B20 File Offset: 0x00065B20
		protected override void EndDataExport()
		{
			int num = 5;
			for (;;)
			{
				IEnumerator enumerator;
				switch (num)
				{
				case 0:
					if (this.ᜅ.Length > 0)
					{
						num = 4;
						continue;
					}
					goto IL_10C;
				case 1:
					try
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_C6;
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									break;
								}
								break;
							case 2:
							{
								if (!enumerator.MoveNext())
								{
									num = 3;
									continue;
								}
								string a_ = (string)enumerator.Current;
								base.ᜀ().ᜇ(a_);
								num = 4;
								continue;
							}
							case 3:
								num = 0;
								continue;
							}
							IL_82:
							num = 2;
							continue;
							goto IL_82;
						}
						IL_C6:
						goto IL_17E;
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
									goto IL_10B;
								case 1:
									disposable.Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_109;
								}
								break;
							}
						}
						IL_109:
						IL_10B:;
					}
					goto IL_10C;
				case 2:
					num = 0;
					continue;
				case 3:
					goto IL_10C;
				case 4:
					base.ᜀ().ᜇ(this.ᜅ);
					num = 3;
					continue;
				}
				if (this.ᜁ)
				{
					num = 2;
					continue;
				}
				IL_10C:
				if (true)
				{
				}
				enumerator = base.Footer.GetEnumerator();
				num = 1;
			}
			IL_17E:
			base.EndDataExport();
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x00066CC4 File Offset: 0x00065CC4
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x00066D08 File Offset: 0x00065D08
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Allows you to select string fields that will not be truncated by occurrences of carriage returns.")]
		public new StringListCollection NotTruncatableColumns
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

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x00066D4C File Offset: 0x00065D4C
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x00066D90 File Offset: 0x00065D90
		[Description("Gets or sets the table name for insert and create table sql statement.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("")]
		public string TableName
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
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜆ = value;
						num = 2;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2A;
						default:
							goto IL_63;
						}
						break;
					}
					goto IL_1C;
					IL_2A:
					num = 0;
					continue;
					IL_1C:
					if (value != this.ᜆ)
					{
						goto IL_2A;
					}
					goto IL_6B;
				}
				IL_63:
				if (false)
				{
				}
				IL_6B:
				if (true)
				{
				}
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x00066E10 File Offset: 0x00065E10
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x00066E54 File Offset: 0x00065E54
		[DefaultValue(0)]
		[Description("Indicates to insert commit statment after define a number of rows.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int CommitRowCount
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
						this.ᜄ = value;
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
							goto IL_25;
						default:
							goto IL_66;
						}
						break;
					}
					goto IL_1C;
					IL_25:
					num = 0;
					continue;
					IL_1C:
					if (value != this.ᜄ)
					{
						goto IL_25;
					}
					return;
				}
				IL_66:
				if (false)
				{
				}
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x00066ED0 File Offset: 0x00065ED0
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x00066F14 File Offset: 0x00065F14
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Indicates whether insert commit statement after data exported complete.")]
		[DefaultValue(false)]
		public bool CommitAfterScript
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
				if (true)
				{
				}
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
							goto IL_2D;
						default:
							goto IL_66;
						}
						break;
					case 1:
						this.ᜁ = value;
						num = 0;
						continue;
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
					return;
				}
				IL_66:
				if (false)
				{
				}
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x00066F90 File Offset: 0x00065F90
		// (set) Token: 0x06000A13 RID: 2579 RVA: 0x00066FD4 File Offset: 0x00065FD4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("COMMIT WORK;")]
		[Description("Gets or sets the sql commit statement.")]
		public string CommitStatement
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
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2A;
						default:
							goto IL_63;
						}
						break;
					case 2:
						this.ᜅ = value;
						num = 0;
						continue;
					}
					goto IL_1C;
					IL_2A:
					num = 2;
					continue;
					IL_1C:
					if (value != this.ᜅ)
					{
						goto IL_2A;
					}
					goto IL_6B;
				}
				IL_63:
				if (false)
				{
				}
				IL_6B:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x00067054 File Offset: 0x00066054
		// (set) Token: 0x06000A15 RID: 2581 RVA: 0x00067098 File Offset: 0x00066098
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Indicate whether to generate the create table sql statement.")]
		public bool CreateTable
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
							goto IL_66;
						}
						break;
					case 2:
						this.ᜂ = value;
						num = 1;
						continue;
					}
					goto IL_1C;
					IL_2D:
					num = 2;
					continue;
					IL_1C:
					if (true)
					{
					}
					if (value != this.ᜂ)
					{
						goto IL_2D;
					}
					return;
				}
				IL_66:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x00067114 File Offset: 0x00066114
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x00067158 File Offset: 0x00066158
		[DefaultValue(';')]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the character for end of each sql statement.")]
		public char EndOfStatement
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
							goto IL_25;
						default:
							goto IL_5E;
						}
						break;
					case 1:
						this.ᜃ = value;
						num = 0;
						continue;
					}
					goto IL_1C;
					IL_25:
					num = 1;
					continue;
					IL_1C:
					if (value != this.ᜃ)
					{
						goto IL_25;
					}
					return;
				}
				IL_5E:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x000671D4 File Offset: 0x000661D4
		// (set) Token: 0x06000A19 RID: 2585 RVA: 0x00067218 File Offset: 0x00066218
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Indicate whether format exported value according to the dataformats property.")]
		[DefaultValue(false)]
		public bool FormatValues
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
				return this.ᜈ;
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
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2D;
						default:
							goto IL_66;
						}
						break;
					case 1:
						this.ᜈ = value;
						num = 0;
						continue;
					}
					goto IL_24;
					IL_2D:
					num = 1;
					continue;
					IL_24:
					if (value != this.ᜈ)
					{
						goto IL_2D;
					}
					return;
				}
				IL_66:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x00067294 File Offset: 0x00066294
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x000672D8 File Offset: 0x000662D8
		[DefaultValue("")]
		[Editor(typeof(SQLFileNameEditor), typeof(UITypeEditor))]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new string FileName
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

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0006731C File Offset: 0x0006631C
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x00067360 File Offset: 0x00066360
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

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x000673A4 File Offset: 0x000663A4
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x000673E8 File Offset: 0x000663E8
		[Description("Indicate whether export long char/binary column.")]
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		public new bool ExportLongColumn
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

		// Token: 0x06000A20 RID: 2592 RVA: 0x0006742C File Offset: 0x0006642C
		public SQLExport()
		{
			int a_ = 14;
			this.ᜃ = ';';
			this.ᜅ = HyperlinksCollectionEditor.b("椩挫挭累笱怳ᘵ漷甹渻甽笿", a_);
			this.ᜆ = string.Empty;
			this.ᜇ = string.Empty;
			base..ctor();
		}

		// Token: 0x04000789 RID: 1929
		private new License ᜀ;

		// Token: 0x0400078A RID: 1930
		private new bool ᜁ;

		// Token: 0x0400078B RID: 1931
		private new bool ᜂ;

		// Token: 0x0400078C RID: 1932
		private new char ᜃ;

		// Token: 0x0400078D RID: 1933
		private new int ᜄ;

		// Token: 0x0400078E RID: 1934
		private string ᜅ;

		// Token: 0x0400078F RID: 1935
		private string ᜆ;

		// Token: 0x04000790 RID: 1936
		private string ᜇ;

		// Token: 0x04000791 RID: 1937
		private long \u25D8\u00A5\u00A9\u0099;

		// Token: 0x04000792 RID: 1938
		private bool ᜈ;
	}
}
