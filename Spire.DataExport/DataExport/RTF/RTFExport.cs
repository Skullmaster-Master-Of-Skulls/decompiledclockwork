using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Text;
using System.Web;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.Common;
using Spire.DataExport.Delegates;
using Spire.DataExport.EventArgs;
using Spire.DataExport.Forms;
using Spire.DataExport.License;
using Spire.DataExport.PropEditors;

namespace Spire.DataExport.RTF
{
	// Token: 0x02000175 RID: 373
	[ToolboxItem(true)]
	[LicenseProvider(typeof(DataExportLicenseProvider))]
	public class RTFExport : FormatTextExport
	{
		// Token: 0x060009BC RID: 2492 RVA: 0x00062C3C File Offset: 0x00061C3C
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
			this.ᜁ = new RTFOptions(this);
			this.m_currEncoding = Encoding.GetEncoding(base.Culture.TextInfo.ANSICodePage);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00062CC0 File Offset: 0x00061CC0
		protected override void Dispose(bool disposing)
		{
			try
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_4E:
					this.ᜀ.Dispose();
					this.ᜀ = null;
					num = 3;
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
						goto IL_7A;
					case 2:
						goto IL_4C;
					case 3:
						if (true)
						{
						}
						goto IL_72;
					}
					if (this.ᜀ != null)
					{
						num = 2;
						continue;
					}
					IL_72:
					num = 1;
				}
				IL_4C:
				goto IL_4E;
				IL_7A:;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00062D6C File Offset: 0x00061D6C
		public override void SaveToFile()
		{
			for (;;)
			{
				IL_00:
				for (;;)
				{
					IL_38:
					spr\u2561.ᜀ = this.ᜁ;
					int num = 4;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								AboutDataExport.ShowAbout(false);
								num = 3;
								continue;
							case 1:
								if (Environment.UserInteractive)
								{
									if (true)
									{
									}
									num = 0;
									continue;
								}
								goto IL_9C;
							case 2:
								num = 1;
								continue;
							case 3:
								goto IL_6C;
							case 4:
								if (this.ᜁ)
								{
									num = 2;
									continue;
								}
								goto IL_9C;
							}
							goto IL_38;
						}
					}
				}
			}
			IL_6C:
			IL_9C:
			base.SaveToFile();
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00062E1C File Offset: 0x00061E1C
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

		// Token: 0x060009C0 RID: 2496 RVA: 0x00062E6C File Offset: 0x00061E6C
		public void SaveToHttpResponse(string FileName, HttpResponse response, SaveType saveType)
		{
			int a_ = 10;
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
					base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("䜥堧娩䀫䜭匯匱䀳張圷吹ጻ匽㌿㕁⭃㑅ⱇ", a_), response, saveType);
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

		// Token: 0x060009C1 RID: 2497 RVA: 0x00062F30 File Offset: 0x00061F30
		public void SaveToHttpResponse(string FileName, HttpResponse response)
		{
			int a_ = 13;
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
					base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("䠨嬪崬䌮堰倲吴䌶倸吺匼ှⱀあ㉄⡆㭈⽊", a_), response, SaveType.Attachment);
				}
				finally
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							((IDisposable)memoryStream).Dispose();
							num = 1;
							continue;
						case 1:
							goto IL_A2;
						}
						if (memoryStream == null)
						{
							break;
						}
						num = 0;
					}
					IL_A2:;
				}
				break;
			}
			}
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00062FF4 File Offset: 0x00061FF4
		public void SaveToHttpResponse(HttpResponse response)
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
			this.SaveToHttpResponse(this.FileName, response);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00063048 File Offset: 0x00062048
		private void ᜀ(RTFStyle A_0, ref string A_1, ref string A_2, ref string A_3, ref string A_4, ref string A_5, ref string A_6)
		{
			int a_ = 6;
			sprᢑ sprᢑ;
			for (;;)
			{
				RtfTextAlignment alignment = A_0.Alignment;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_DD;
					case 1:
						goto IL_193;
					case 2:
						if ((A_0.Font.Style & FontStyle.Underline) == FontStyle.Underline)
						{
							num = 13;
							continue;
						}
						goto IL_165;
					case 3:
						switch (alignment)
						{
						case RtfTextAlignment.Right:
							A_1 = HyperlinksCollectionEditor.b("縡唣吥", a_);
							num = 5;
							continue;
						case RtfTextAlignment.Center:
							A_1 = HyperlinksCollectionEditor.b("縡唣䔥", a_);
							num = 14;
							continue;
						case RtfTextAlignment.Fill:
							A_1 = HyperlinksCollectionEditor.b("縡唣䰥", a_);
							num = 15;
							continue;
						default:
							num = 22;
							continue;
						}
						break;
					case 4:
						goto IL_1B4;
					case 5:
						goto IL_2A3;
					case 6:
						A_1 = HyperlinksCollectionEditor.b("縡唣䨥", a_);
						num = 24;
						continue;
					case 7:
						if ((A_0.Font.Style & FontStyle.Strikeout) == FontStyle.Strikeout)
						{
							num = 11;
							continue;
						}
						goto IL_329;
					case 8:
						A_4 += HyperlinksCollectionEditor.b("縡䴣", a_);
						num = 0;
						continue;
					case 9:
						goto IL_329;
					case 10:
						A_4 += HyperlinksCollectionEditor.b("縡䘣", a_);
						num = 12;
						continue;
					case 11:
						A_4 += HyperlinksCollectionEditor.b("縡圣別娧䌩䜫䬭", a_);
						num = 9;
						continue;
					case 12:
						goto IL_2FB;
					case 13:
						A_4 += HyperlinksCollectionEditor.b("縡儣䨥", a_);
						num = 16;
						continue;
					case 14:
						goto IL_2A3;
					case 15:
						goto IL_2A3;
					case 16:
						goto IL_165;
					case 17:
						if ((A_0.Font.Style & FontStyle.Bold) == FontStyle.Bold)
						{
							goto IL_2EB;
						}
						goto IL_2FB;
					case 18:
						A_5 = sprᢑ.ᜀ(A_0.BackgroundColor, RtfColorType.Background);
						num = 1;
						continue;
					case 19:
						if (A_0.AllowBackground)
						{
							num = 18;
							continue;
						}
						if (true)
						{
						}
						A_5 = string.Empty;
						num = 23;
						continue;
					case 20:
						if (A_0.AllowHighlight)
						{
							num = 4;
							continue;
						}
						goto IL_377;
					case 21:
						if ((A_0.Font.Style & FontStyle.Italic) == FontStyle.Italic)
						{
							num = 8;
							continue;
						}
						goto IL_DD;
					case 22:
						num = 6;
						continue;
					case 23:
						goto IL_193;
					case 24:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2EB;
						default:
							if (false)
							{
							}
							goto IL_2A3;
						}
						break;
					}
					break;
					IL_DD:
					num = 2;
					continue;
					IL_165:
					num = 7;
					continue;
					IL_193:
					num = 20;
					continue;
					IL_2A3:
					sprᢑ = this.ᜀ();
					sprᢑ.ᜀ(A_0.Font, false, ref A_2);
					A_3 = sprᢑ.ᜀ(A_0.FontColor, RtfColorType.Text);
					A_4 = string.Empty;
					num = 17;
					continue;
					IL_2EB:
					num = 10;
					continue;
					IL_2FB:
					num = 21;
					continue;
					IL_329:
					num = 19;
				}
			}
			IL_1B4:
			A_6 = sprᢑ.ᜀ(A_0.HighlightColor, RtfColorType.Highlight);
			return;
			IL_377:
			A_6 = string.Empty;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x000633D4 File Offset: 0x000623D4
		protected override void BeginDataExport()
		{
			int a_ = 11;
			switch (0)
			{
			default:
				for (;;)
				{
					base.BeginDataExport();
					this.ᜀ().ᜂ();
					this.ᜀ().ᜀ();
					spr\u2266 a_2 = new spr\u2266(0, HyperlinksCollectionEditor.b("䤦䀨䜪", a_), this.ᜁ.TitleStyle.Font.Name);
					this.ᜀ().ᜀ(a_2);
					a_2 = new spr\u2266(1, HyperlinksCollectionEditor.b("䤦䀨䜪", a_), this.ᜁ.DataStyle.Font.Name);
					this.ᜀ().ᜀ(a_2);
					this.ᜀ().ᜆ();
					spr\u2495 a_3 = new spr\u2495(Color.Black);
					this.ᜀ().ᜀ(a_3);
					this.ᜀ().ᜅ();
					string empty = string.Empty;
					this.ᜀ().ᜀ(this.ᜁ.DataStyle.Font, true, ref empty);
					int num = 9;
					for (;;)
					{
						string empty2;
						string empty3;
						string empty4;
						string empty5;
						string text3;
						IEnumerator enumerator2;
						int num2;
						int num3;
						switch (num)
						{
						case 0:
							if (base.Header.Count > 0)
							{
								num = 2;
								continue;
							}
							goto IL_33C;
						case 1:
							try
							{
								IEnumerator enumerator = base.Header.GetEnumerator();
								try
								{
									num = 5;
									for (;;)
									{
										string text;
										string text2;
										switch (num)
										{
										case 0:
											if (!enumerator.MoveNext())
											{
												num = 6;
												continue;
											}
											text = (string)enumerator.Current;
											text2 = empty + empty2 + empty3;
											num = 1;
											continue;
										case 1:
											if (text2.Length > 0)
											{
												num = 2;
												continue;
											}
											goto IL_502;
										case 2:
											text2 += ' ';
											num = 7;
											continue;
										case 4:
											goto IL_587;
										case 6:
											num = 4;
											continue;
										case 7:
											goto IL_502;
										}
										IL_4DF:
										num = 0;
										continue;
										goto IL_4DF;
										IL_502:
										this.ᜀ().ᜇ(string.Concat(new object[]
										{
											'{',
											empty4,
											text2,
											text,
											'}'
										}));
										this.ᜀ().ᜃ();
										num = 3;
									}
									IL_587:;
								}
								finally
								{
									IDisposable disposable;
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										IL_5DB:
										disposable.Dispose();
										num = 2;
										break;
									default:
										if (false)
										{
										}
										goto IL_5BA;
									}
									for (;;)
									{
										IL_5A7:
										switch (num)
										{
										case 0:
											goto IL_5D9;
										case 1:
											if (disposable != null)
											{
												num = 0;
												continue;
											}
											goto IL_5ED;
										case 2:
											goto IL_5EB;
										}
										goto IL_5BA;
									}
									IL_5D9:
									goto IL_5DB;
									IL_5EB:
									IL_5ED:
									goto EndFinally_16;
									IL_5BA:
									disposable = (enumerator as IDisposable);
									num = 1;
									goto IL_5A7;
									EndFinally_16:;
								}
								goto IL_33C;
							}
							finally
							{
								this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("娦", a_));
							}
							goto IL_60D;
						case 2:
							this.ᜀ().ᜇ('{' + empty5 + text3);
							num = 1;
							continue;
						case 3:
							goto IL_3AA;
						case 4:
							goto IL_3D8;
						case 5:
							goto IL_3D8;
						case 6:
							goto IL_2E0;
						case 7:
							if (text3.Length > 0)
							{
								if (true)
								{
								}
								num = 10;
								continue;
							}
							goto IL_3AA;
						case 8:
							try
							{
								num = 3;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_292;
									case 1:
										num = 0;
										continue;
									case 2:
									{
										if (!enumerator2.MoveNext())
										{
											num = 1;
											continue;
										}
										ColumnExport columnExport = (ColumnExport)enumerator2.Current;
										num2 += columnExport.Width * num3 * 15 + 10;
										this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("符䨨䜪伬崮唰䄲头欶嬸䤺夼䴾㕀⭂敄ᭆ⩈❊⽌㵎㕐⅒❔ୖ㭘⥚㥜ⵞᕠୢ䕤㭦੨ݪཬᵮᕰŲŴ⭶᭸ॺ᥼ൾꖄ\udb86ﶎ쮖ﮘ列햠쮢", a_));
										this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("符䨨个䄬䌮䤰", a_) + num2.ToString());
										num = 4;
										continue;
									}
									}
									IL_260:
									num = 2;
									continue;
									goto IL_260;
								}
								IL_292:
								return;
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
											disposable2.Dispose();
											num = 1;
											continue;
										case 1:
											goto IL_2DD;
										case 2:
											if (disposable2 != null)
											{
												num = 0;
												continue;
											}
											goto IL_2DF;
										}
										break;
									}
								}
								IL_2DD:
								IL_2DF:;
							}
							goto IL_2E0;
						case 9:
							if (this.ᜁ.PageOrientation == PageOrientation.Landscape)
							{
								num = 6;
								continue;
							}
							this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("符夨䨪崬䨮䌰䐲дض8଺଼", a_));
							this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("符夨䨪崬䨮䌰嬲дĶĸ࠺Լ", a_));
							num = 5;
							continue;
						case 10:
							goto IL_60D;
						}
						break;
						IL_2E0:
						this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("符䔨䨪䌬䬮䈰倲吴䜶尸", a_));
						this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("符夨䨪崬䨮䌰䐲дĶĸ࠺Լ", a_));
						this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("符夨䨪崬䨮䌰嬲дض8଺଼", a_));
						num = 4;
						continue;
						IL_33C:
						this.ᜀ().ᜃ();
						this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("符崨太䈬堮唰漲䄴䔶䠸场愼䬾㍀⑂⑄㝆ⅈ筊ᅌ㭎⍐㽒ごㅖⵘ桚歜", a_));
						num2 = 36;
						num3 = 0;
						num3 = spr\u2059.ᜀ(HyperlinksCollectionEditor.b("缦", a_), this.ᜁ.TitleStyle.Font);
						enumerator2 = base.ColumnsExport.GetEnumerator();
						num = 8;
						continue;
						IL_3AA:
						num = 0;
						continue;
						IL_3D8:
						RTFStyle rtfstyle = this.ᜁ.HeaderStyle.Clone() as RTFStyle;
						this.ᜁ(this, rtfstyle);
						empty5 = string.Empty;
						empty2 = string.Empty;
						empty3 = string.Empty;
						text3 = string.Empty;
						empty4 = string.Empty;
						this.ᜀ(rtfstyle, ref empty5, ref empty, ref empty2, ref empty3, ref text3, ref empty4);
						num = 7;
						continue;
						IL_60D:
						text3 += ' ';
						num = 3;
					}
				}
				return;
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00063A5C File Offset: 0x00062A5C
		protected override void BeforeExport()
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
			string empty = string.Empty;
			this.ᜀ().ᜀ(this.ᜁ.DataStyle.Font, true, ref empty);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00063ABC File Offset: 0x00062ABC
		protected override string GetColumnTitle(int Index)
		{
			int a_ = 15;
			switch (0)
			{
			default:
			{
				string text;
				string text2;
				string text3;
				string empty4;
				string empty5;
				for (;;)
				{
					int num;
					ColumAlign a_2;
					RtfTextAlignment alignment;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_10A:
						num = 13;
						break;
					default:
						if (false)
						{
						}
						text = base.GetColumnTitle(Index);
						a_2 = ColumAlign.Center;
						alignment = this.ᜁ.TitleStyle.Alignment;
						num = 16;
						break;
					}
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							goto IL_2A4;
						case 1:
							goto IL_1EB;
						case 2:
							num = 12;
							continue;
						case 3:
						{
							int num2;
							if (num2 > -1)
							{
								num = 5;
								continue;
							}
							goto IL_1EB;
						}
						case 4:
							goto IL_1EB;
						case 5:
							num = 18;
							continue;
						case 6:
							a_2 = ColumAlign.Left;
							num = 1;
							continue;
						case 7:
							goto IL_1EB;
						case 8:
						{
							int num2 = this.ᜁ.TitleAligns.IndexOfName(base.ColumnsExport[Index].Name);
							num = 3;
							continue;
						}
						case 9:
						{
							char c = this.ᜁ.TitleAligns.GetValue(base.ColumnsExport[Index].Name).ToUpper()[0];
							num = 19;
							continue;
						}
						case 10:
							goto IL_183;
						case 11:
							if (this.ᜁ.TitleAligns.Count > 0)
							{
								num = 8;
								continue;
							}
							goto IL_1EB;
						case 12:
							a_2 = ColumAlign.Left;
							num = 17;
							continue;
						case 13:
							num = 6;
							continue;
						case 14:
							goto IL_2A4;
						case 15:
						{
							char c;
							if (c != 'R')
							{
								goto IL_10A;
							}
							a_2 = ColumAlign.Right;
							num = 4;
							continue;
						}
						case 16:
							switch (alignment)
							{
							case RtfTextAlignment.Right:
								a_2 = ColumAlign.Right;
								num = 0;
								continue;
							case RtfTextAlignment.Center:
								a_2 = ColumAlign.Center;
								num = 14;
								continue;
							default:
								num = 2;
								continue;
							}
							break;
						case 17:
							goto IL_2A4;
						case 18:
							if (this.ᜁ.TitleAligns.GetValue(base.ColumnsExport[Index].Name).Length > 0)
							{
								num = 9;
								continue;
							}
							goto IL_1EB;
						case 19:
						{
							char c;
							if (c != 'C')
							{
								num = 21;
								continue;
							}
							a_2 = ColumAlign.Center;
							num = 7;
							continue;
						}
						case 20:
							if (text2.Length > 0)
							{
								num = 22;
								continue;
							}
							goto IL_37F;
						case 21:
							num = 15;
							continue;
						case 22:
							text2 += ' ';
							num = 10;
							continue;
						}
						break;
						IL_1EB:
						RTFStyle rtfstyle = this.ᜁ.TitleStyle.Clone() as RTFStyle;
						this.ᜀ(this, new RTFTitleStyleEventArgs(Index, rtfstyle));
						text3 = string.Empty;
						string empty = string.Empty;
						string empty2 = string.Empty;
						string empty3 = string.Empty;
						empty4 = string.Empty;
						empty5 = string.Empty;
						this.ᜀ(rtfstyle, ref text3, ref empty, ref empty2, ref empty3, ref empty4, ref empty5);
						text3 = this.ᜀ().ᜀ(a_2);
						text2 = empty + empty2 + empty3;
						num = 20;
						continue;
						IL_2A4:
						num = 11;
					}
				}
				IL_183:
				IL_37F:
				text = string.Concat(new object[]
				{
					HyperlinksCollectionEditor.b("眪崬丮䌰圲椴帶圸伺弼匾ᵀ⽂ⱄ瑆祈ᝊ㽌♎扐捒", a_),
					empty4,
					text3,
					'{',
					empty5,
					text2,
					text,
					HyperlinksCollectionEditor.b("嘪焬䰮吰弲头", a_)
				});
				return text;
			}
			}
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00063EA8 File Offset: 0x00062EA8
		protected override void WriteTitleRow()
		{
			int a_ = 0;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			string empty = string.Empty;
			this.ᜀ().ᜀ(this.ᜁ.TitleStyle.Font, true, ref empty);
			this.ᜀ().ᜇ(this.GetCaptionRow() + HyperlinksCollectionEditor.b("䀛氝伟唡", a_));
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00063F34 File Offset: 0x00062F34
		protected override void WriteBlankRow()
		{
			int a_ = 1;
			StringBuilder stringBuilder;
			for (;;)
			{
				stringBuilder = new StringBuilder();
				int num = 0;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						goto IL_3B;
					case 1:
						goto IL_3B;
					case 2:
						if (num >= base.ExportRowExport.Count)
						{
							num2 = 3;
							continue;
						}
						stringBuilder.Append(HyperlinksCollectionEditor.b("䄜簞䐠伢䤤", a_));
						num++;
						num2 = 1;
						continue;
					case 3:
						goto IL_59;
					}
					break;
					IL_3B:
					num2 = 2;
				}
			}
			for (;;)
			{
				IL_59:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_9E;
				}
			}
			IL_9E:
			if (false)
			{
			}
			this.ᜀ().ᜇ(stringBuilder.ToString() + HyperlinksCollectionEditor.b("䄜洞丠吢", a_));
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0006400C File Offset: 0x0006300C
		protected override string GetColumnValue(ColExport ExportColExport)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				string text;
				string text2;
				string text3;
				string empty4;
				string empty5;
				for (;;)
				{
					text = base.GetColumnValue(ExportColExport);
					int columnIndex = ExportColExport.ColumnIndex;
					text = text.Replace(HyperlinksCollectionEditor.b("⤣Ⱕ", a_), HyperlinksCollectionEditor.b("У稥堧䬩師อ", a_));
					ColumAlign colAlign = base.ColumnsExport[columnIndex].ColAlign;
					RTFStyle rtfstyle = null;
					int num = 4;
					for (;;)
					{
						int index;
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_255;
							default:
								if (false)
								{
								}
								goto IL_1CC;
							}
							break;
						case 1:
							goto IL_107;
						case 2:
							index = columnIndex % this.ᜁ.ItemStyles.Count;
							num = 5;
							continue;
						case 3:
							if (text2.Length > 0)
							{
								goto IL_255;
							}
							goto IL_2B5;
						case 4:
							if (this.ᜁ.ItemType != RtfItemType.None)
							{
								num = 12;
								continue;
							}
							goto IL_C4;
						case 5:
							goto IL_107;
						case 6:
							if (this.ᜁ.ItemType == RtfItemType.Col)
							{
								num = 2;
								continue;
							}
							index = base.RowsCount % this.ᜁ.ItemStyles.Count;
							num = 1;
							continue;
						case 7:
							goto IL_194;
						case 8:
							goto IL_1CC;
						case 9:
							if (this.ᜁ.ItemStyles.Count > 0)
							{
								num = 11;
								continue;
							}
							goto IL_C4;
						case 10:
							text2 += ' ';
							num = 7;
							continue;
						case 11:
							index = 0;
							if (true)
							{
							}
							num = 6;
							continue;
						case 12:
							num = 9;
							continue;
						}
						break;
						IL_C4:
						rtfstyle = (this.ᜁ.DataStyle.Clone() as RTFStyle);
						num = 0;
						continue;
						IL_107:
						rtfstyle = (this.ᜁ.ItemStyles[index].Clone() as RTFStyle);
						num = 8;
						continue;
						IL_1CC:
						this.ᜀ(this, new RTFDataStyleEventArgs(base.SkipRows + base.RowsCount, columnIndex, rtfstyle));
						text3 = string.Empty;
						string empty = string.Empty;
						string empty2 = string.Empty;
						string empty3 = string.Empty;
						empty4 = string.Empty;
						empty5 = string.Empty;
						this.ᜀ(rtfstyle, ref text3, ref empty, ref empty2, ref empty3, ref empty4, ref empty5);
						text3 = this.ᜀ().ᜀ(colAlign);
						text2 = empty + empty2 + empty3;
						num = 3;
						continue;
						IL_255:
						num = 10;
					}
				}
				IL_194:
				IL_2B5:
				return string.Concat(new string[]
				{
					HyperlinksCollectionEditor.b("砣嘥䤧堩䠫爭夯就䀳吵吷昹倻圽猿牁ᡃ㑅ⅇ祉籋", a_),
					empty4,
					empty5,
					text3,
					HyperlinksCollectionEditor.b("弣", a_),
					text2,
					text,
					HyperlinksCollectionEditor.b("夣稥䬧伩䀫䈭", a_)
				});
			}
			}
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00064334 File Offset: 0x00063334
		protected override void WriteRow()
		{
			int a_ = 14;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜀ().ᜇ(this.GetDataRow(true) + HyperlinksCollectionEditor.b("瘩師䄭䜯", a_));
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x000643A0 File Offset: 0x000633A0
		protected override void EndDataExport()
		{
			int a_ = 1;
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("䄜漞䀠儢䄤", a_));
					RTFStyle rtfstyle = this.ᜁ.FooterStyle.Clone() as RTFStyle;
					this.ᜀ(this, rtfstyle);
					string empty = string.Empty;
					string empty2 = string.Empty;
					string empty3 = string.Empty;
					string empty4 = string.Empty;
					string text = string.Empty;
					string empty5 = string.Empty;
					this.ᜀ(rtfstyle, ref empty, ref empty2, ref empty3, ref empty4, ref text, ref empty5);
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							if (base.Footer.Count > 0)
							{
								num = 4;
								continue;
							}
							goto IL_321;
						case 1:
							goto IL_2EB;
						case 2:
							try
							{
								IEnumerator enumerator = base.Footer.GetEnumerator();
								try
								{
									num = 3;
									for (;;)
									{
										string text2;
										string text3;
										switch (num)
										{
										case 0:
											if (text2.Length > 0)
											{
												num = 2;
												continue;
											}
											goto IL_119;
										case 2:
											text2 += ' ';
											num = 6;
											continue;
										case 4:
											if (!enumerator.MoveNext())
											{
												num = 5;
												continue;
											}
											text3 = (string)enumerator.Current;
											text2 = empty2 + empty3 + empty4;
											num = 0;
											continue;
										case 5:
											num = 7;
											continue;
										case 6:
											goto IL_119;
										case 7:
											goto IL_207;
										}
										goto IL_114;
										IL_119:
										this.ᜀ().ᜇ(string.Concat(new object[]
										{
											'{',
											empty5,
											text2,
											text3,
											'}'
										}));
										this.ᜀ().ᜃ();
										num = 1;
										continue;
										IL_1B4:
										num = 4;
										continue;
										IL_114:
										goto IL_1B4;
									}
									IL_207:;
								}
								finally
								{
									IDisposable disposable;
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										IL_25B:
										disposable.Dispose();
										num = 0;
										break;
									default:
										if (false)
										{
										}
										goto IL_23A;
									}
									for (;;)
									{
										IL_227:
										switch (num)
										{
										case 0:
											goto IL_26B;
										case 1:
											if (disposable != null)
											{
												num = 2;
												continue;
											}
											goto IL_26D;
										case 2:
											goto IL_259;
										}
										goto IL_23A;
									}
									IL_259:
									goto IL_25B;
									IL_26B:
									IL_26D:
									goto EndFinally_10;
									IL_23A:
									disposable = (enumerator as IDisposable);
									num = 1;
									goto IL_227;
									EndFinally_10:;
								}
								goto IL_321;
							}
							finally
							{
								this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("怜", a_));
							}
							goto IL_28D;
						case 3:
							text += ' ';
							num = 1;
							continue;
						case 4:
							goto IL_28D;
						case 5:
							if (text.Length > 0)
							{
								num = 3;
								continue;
							}
							goto IL_2EB;
						}
						break;
						IL_28D:
						this.ᜀ().ᜃ();
						this.ᜀ().ᜇ('{' + empty + text);
						num = 2;
						continue;
						IL_2EB:
						num = 0;
					}
				}
				IL_321:
				this.ᜀ().ᜄ();
				base.EndDataExport();
				return;
			}
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00064714 File Offset: 0x00063714
		internal new sprᢑ ᜀ()
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
			return base.ᜀ() as sprᢑ;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0006475C File Offset: 0x0006375C
		protected override Type GetWriterType()
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
			return typeof(sprᢑ);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x000647A4 File Offset: 0x000637A4
		public override void Stop()
		{
			int a_ = 13;
			if (true)
			{
			}
			this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("用嬪䰬崮唰", a_));
			this.ᜀ().ᜃ();
			IEnumerator enumerator = base.Footer.GetEnumerator();
			try
			{
				int num = 1;
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
						this.ᜀ().ᜃ();
						this.ᜀ().ᜇ(a_2);
						num = 4;
						continue;
					}
					case 3:
						goto IL_B8;
					}
					IL_96:
					num = 2;
					continue;
					goto IL_96;
				}
				IL_B8:;
			}
			finally
			{
				int num;
				IDisposable disposable;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_106:
					disposable.Dispose();
					num = 2;
					break;
				default:
					if (false)
					{
					}
					goto IL_EA;
				}
				for (;;)
				{
					IL_D8:
					switch (num)
					{
					case 0:
						if (disposable != null)
						{
							num = 1;
							continue;
						}
						goto IL_116;
					case 1:
						goto IL_104;
					case 2:
						goto IL_114;
					}
					goto IL_EA;
				}
				IL_104:
				goto IL_106;
				IL_114:
				IL_116:
				goto EndFinally_5;
				IL_EA:
				disposable = (enumerator as IDisposable);
				num = 0;
				goto IL_D8;
				EndFinally_5:;
			}
			base.Stop();
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x000648E8 File Offset: 0x000638E8
		internal override string NormalString(string S)
		{
			int a_ = 16;
			switch (0)
			{
			default:
			{
				StringBuilder stringBuilder;
				for (;;)
				{
					stringBuilder = new StringBuilder(S.Length);
					int num = 0;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_5B;
						case 1:
						{
							if (num >= S.Length)
							{
								num2 = 5;
								continue;
							}
							char c = S[num];
							num2 = 7;
							continue;
						}
						case 2:
							goto IL_CF;
						case 3:
							goto IL_CF;
						case 4:
							goto IL_F2;
						case 5:
							goto IL_F0;
						case 6:
							goto IL_5B;
						case 7:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_F2;
							default:
							{
								if (false)
								{
								}
								if (true)
								{
								}
								char c;
								if (c == '\\')
								{
									num2 = 4;
									continue;
								}
								stringBuilder.Append(c);
								num2 = 6;
								continue;
							}
							}
							break;
						}
						break;
						IL_5B:
						num++;
						num2 = 2;
						continue;
						IL_CF:
						num2 = 1;
						continue;
						IL_F2:
						stringBuilder.Append(HyperlinksCollectionEditor.b("瀫爭", a_));
						num2 = 0;
					}
				}
				IL_F0:
				return stringBuilder.ToString();
			}
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00064A14 File Offset: 0x00063A14
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x00064A58 File Offset: 0x00063A58
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Description("Gets or sets options of the result RTF document.")]
		public RTFOptions RTFOptions
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
					switch (num)
					{
					case 0:
						this.ᜁ = value;
						num = 2;
						continue;
					case 1:
						if (true)
						{
						}
						break;
					case 2:
						goto IL_6F;
					case 3:
						num = 4;
						continue;
					case 4:
						if (value != this.ᜁ)
						{
							num = 0;
							continue;
						}
						goto IL_6F;
					}
					IL_2C:
					if (value != null)
					{
						num = 3;
						continue;
					}
					IL_6F:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2C;
					default:
						goto IL_85;
					}
				}
				IL_85:
				if (false)
				{
				}
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00064AF0 File Offset: 0x00063AF0
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x00064B34 File Offset: 0x00063B34
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new StringListCollection ColumnsWidth
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
				return base.ColumnsWidth;
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
				base.ColumnsWidth = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00064B78 File Offset: 0x00063B78
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x00064BBC File Offset: 0x00063BBC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Allows you to select string fields that will not be truncated by occurrences of carriage returns.")]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		public new StringListCollection NotTruncatableColumns
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
				return base.NotTruncatableColumns;
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
				base.NotTruncatableColumns = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00064C00 File Offset: 0x00063C00
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x00064C44 File Offset: 0x00063C44
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				base.ColumnsAlign = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x00064C88 File Offset: 0x00063C88
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x00064CCC File Offset: 0x00063CCC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("")]
		[Browsable(true)]
		[Editor(typeof(RTFFileNameEditor), typeof(UITypeEditor))]
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
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				base.FileName = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x00064D10 File Offset: 0x00063D10
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x00064D54 File Offset: 0x00063D54
		[Description("Determines the encoding type of the result file.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(RTFEncodingType.ASCII)]
		public new RTFEncodingType DataEncoding
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
				for (;;)
				{
					this.ᜆ = value;
					if (true)
					{
					}
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch (value)
							{
							case RTFEncodingType.ASCII:
								goto IL_65;
							case RTFEncodingType.OEM:
								goto IL_49;
							case RTFEncodingType.UTF8:
								goto IL_B4;
							default:
								num = 2;
								continue;
							}
							break;
						case 1:
							goto IL_B2;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_C0;
							default:
								if (false)
								{
								}
								num = 1;
								continue;
							}
							break;
						}
						break;
					}
				}
				IL_49:
				this.m_currEncoding = Encoding.GetEncoding(base.Culture.TextInfo.OEMCodePage);
				return;
				IL_65:
				this.m_currEncoding = Encoding.GetEncoding(base.Culture.TextInfo.ANSICodePage);
				return;
				IL_B2:
				goto IL_C0;
				IL_B4:
				this.m_currEncoding = Encoding.UTF8;
				return;
				IL_C0:
				this.m_currEncoding = Encoding.GetEncoding(base.Culture.TextInfo.ANSICodePage);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x00064E3C File Offset: 0x00063E3C
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x00064E80 File Offset: 0x00063E80
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(false)]
		[Description("Indicate whether export long char/binary column.")]
		[Browsable(true)]
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

		// Token: 0x060009DE RID: 2526 RVA: 0x00064EC4 File Offset: 0x00063EC4
		internal new void ᜁ(object A_0, RTFStyle A_1)
		{
			int num = 1;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					this.ᜂ(A_0, A_1);
					num = 2;
					continue;
				case 2:
					goto IL_4D;
				}
				goto IL_24;
				IL_2C:
				num = 0;
				continue;
				IL_24:
				if (this.ᜂ != null)
				{
					goto IL_2C;
				}
				IL_4D:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2C;
				default:
					goto IL_63;
				}
			}
			IL_63:
			if (false)
			{
			}
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00064F44 File Offset: 0x00063F44
		internal void ᜀ(object A_0, RTFTitleStyleEventArgs A_1)
		{
			int a_ = 4;
			int num = 3;
			for (;;)
			{
				IL_13:
				switch (num)
				{
				case 0:
					goto IL_38;
				case 1:
					this.ᜃ(A_0, A_1);
					num = 2;
					continue;
				case 2:
					goto IL_4F;
				case 4:
					if (this.ᜃ != null)
					{
						num = 1;
						continue;
					}
					return;
				}
				while (A_1 != null)
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
						num = 4;
						goto IL_13;
					}
				}
				num = 0;
			}
			IL_38:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("ⴟ⠡瘣爥渧漩含席弯䀱䀳వȷ根崻圽㌿❁̓⍅㱇ॉⵋ㹍⑏㭑㭓㡕ୗ⹙╛㉝՟乡ባݥᩧ偩५", a_));
			IL_4F:
			if (true)
			{
			}
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00065000 File Offset: 0x00064000
		internal void ᜀ(object A_0, RTFDataStyleEventArgs A_1)
		{
			int a_ = 5;
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				IL_1B:
				switch (num)
				{
				case 1:
					this.ᜄ(A_0, A_1);
					num = 3;
					continue;
				case 2:
					goto IL_40;
				case 3:
					return;
				case 4:
					if (this.ᜄ != null)
					{
						num = 1;
						continue;
					}
					return;
				}
				while (A_1 != null)
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
						num = 4;
						goto IL_1B;
					}
				}
				num = 2;
			}
			IL_40:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("Ⱐ⤢眤猦漨渪唬弮帰䄲䄴ശ̸椺尼嘾㉀♂Ʉ≆㵈ཊⱌ㭎ぐR⅔⹖㕘㹚煜⥞`ᅢ彤ɦ", a_));
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x000650BC File Offset: 0x000640BC
		internal void ᜀ(object A_0, RTFStyle A_1)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_4D;
				case 2:
					this.ᜅ(A_0, A_1);
					num = 1;
					continue;
				}
				goto IL_1C;
				IL_2C:
				num = 2;
				continue;
				IL_1C:
				if (true)
				{
				}
				if (this.ᜅ != null)
				{
					goto IL_2C;
				}
				IL_4D:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2C;
				default:
					goto IL_63;
				}
			}
			IL_63:
			if (false)
			{
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060009E2 RID: 2530 RVA: 0x0006513C File Offset: 0x0006413C
		// (remove) Token: 0x060009E3 RID: 2531 RVA: 0x000651A0 File Offset: 0x000641A0
		[Description("Occur when the style of header changed.")]
		public event RTFStyleEventHandler GetHeaderStyle
		{
			add
			{
				while (this.ᜂ == null)
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
						this.ᜂ = value;
						return;
					}
				}
				if (true)
				{
				}
				this.ᜂ = (RTFStyleEventHandler)Delegate.Combine(this.ᜂ, value);
			}
			remove
			{
				int num = 1;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_57;
					case 2:
						this.ᜂ = (RTFStyleEventHandler)Delegate.Remove(this.ᜂ, value);
						num = 0;
						continue;
					}
					goto IL_24;
					IL_2C:
					num = 2;
					continue;
					IL_24:
					if (this.ᜂ != null)
					{
						goto IL_2C;
					}
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2C;
					default:
						goto IL_6D;
					}
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060009E4 RID: 2532 RVA: 0x0006522C File Offset: 0x0006422C
		// (remove) Token: 0x060009E5 RID: 2533 RVA: 0x00065290 File Offset: 0x00064290
		[Description("Occur when the style of the titles changed.")]
		public event RTFTitleStyleEventHandler GetCaptionStyle
		{
			add
			{
				while (this.ᜃ == null)
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
						this.ᜃ = value;
						return;
					}
				}
				this.ᜃ = (RTFTitleStyleEventHandler)Delegate.Combine(this.ᜃ, value);
			}
			remove
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜃ = (RTFTitleStyleEventHandler)Delegate.Remove(this.ᜃ, value);
						if (true)
						{
						}
						num = 1;
						continue;
					case 1:
						goto IL_57;
					}
					goto IL_1C;
					IL_24:
					num = 0;
					continue;
					IL_1C:
					if (this.ᜃ != null)
					{
						goto IL_24;
					}
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_24;
					default:
						goto IL_6D;
					}
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060009E6 RID: 2534 RVA: 0x0006531C File Offset: 0x0006431C
		// (remove) Token: 0x060009E7 RID: 2535 RVA: 0x00065380 File Offset: 0x00064380
		[Description("Occur when the data style changed.")]
		public event RTFDataStyleEventHandler GetDataStyle
		{
			add
			{
				while (this.ᜄ == null)
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
						this.ᜄ = value;
						return;
					}
				}
				this.ᜄ = (RTFDataStyleEventHandler)Delegate.Combine(this.ᜄ, value);
			}
			remove
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
						this.ᜄ = (RTFDataStyleEventHandler)Delegate.Remove(this.ᜄ, value);
						num = 1;
						continue;
					case 1:
						goto IL_57;
					}
					goto IL_1C;
					IL_24:
					num = 0;
					continue;
					IL_1C:
					if (this.ᜄ != null)
					{
						goto IL_24;
					}
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_24;
					default:
						goto IL_6D;
					}
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060009E8 RID: 2536 RVA: 0x0006540C File Offset: 0x0006440C
		// (remove) Token: 0x060009E9 RID: 2537 RVA: 0x00065470 File Offset: 0x00064470
		[Description("Occur when the style of footer changed.")]
		public event RTFStyleEventHandler GetFooterStyle
		{
			add
			{
				while (this.ᜅ == null)
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
						this.ᜅ = value;
						return;
					}
				}
				this.ᜅ = (RTFStyleEventHandler)Delegate.Combine(this.ᜅ, value);
			}
			remove
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.ᜅ = (RTFStyleEventHandler)Delegate.Remove(this.ᜅ, value);
						num = 2;
						continue;
					case 2:
						goto IL_57;
					}
					goto IL_1C;
					IL_24:
					if (true)
					{
					}
					num = 1;
					continue;
					IL_1C:
					if (this.ᜅ != null)
					{
						goto IL_24;
					}
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_24;
					default:
						goto IL_6D;
					}
				}
				IL_6D:
				if (false)
				{
				}
			}
		}

		// Token: 0x0400076E RID: 1902
		private new License ᜀ;

		// Token: 0x0400076F RID: 1903
		private new RTFOptions ᜁ;

		// Token: 0x04000770 RID: 1904
		private new RTFStyleEventHandler ᜂ;

		// Token: 0x04000771 RID: 1905
		private new RTFTitleStyleEventHandler ᜃ;

		// Token: 0x04000772 RID: 1906
		private string \u25D8\u008D\u00AE\u00A6;

		// Token: 0x04000773 RID: 1907
		private new RTFDataStyleEventHandler ᜄ;

		// Token: 0x04000774 RID: 1908
		private RTFStyleEventHandler ᜅ;

		// Token: 0x04000775 RID: 1909
		private string \u2609\u0093\u0094\u00AF;

		// Token: 0x04000776 RID: 1910
		private string[] \u2609\u008B\u00A9\u0082;

		// Token: 0x04000777 RID: 1911
		private RTFEncodingType ᜆ;
	}
}
