using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Text;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.Common;
using Spire.DataExport.Delegates;
using Spire.DataExport.EventArgs;
using Spire.DataExport.Forms;
using Spire.DataExport.License;
using Spire.DataExport.PropEditors;
using Spire.DataExport.ResourceMgr;
using Spire.DataExport.Utils;

namespace Spire.DataExport.HTML
{
	// Token: 0x0200017C RID: 380
	[LicenseProvider(typeof(DataExportLicenseProvider))]
	[ToolboxItem(true)]
	public class HTMLExport : FormatTextExport
	{
		// Token: 0x06000A21 RID: 2593 RVA: 0x0006747C File Offset: 0x0006647C
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
			this.ᜁ = !LicenseManager.IsValid(base.GetType(), this, out this.ᜁ);
			base.InitializeVariables();
			this.ᜀ(HtmlExportStyles.DOS);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x000674E4 File Offset: 0x000664E4
		protected override void Dispose(bool Disposing)
		{
			if (!this.ᜀ)
			{
				if (true)
				{
				}
				try
				{
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
								break;
							default:
								if (false)
								{
								}
								if (this.ᜁ != null)
								{
									num = 5;
									continue;
								}
								goto IL_BD;
							}
							break;
						case 1:
							goto IL_BD;
						case 2:
							this.ᜂ();
							num = 3;
							continue;
						case 3:
							goto IL_84;
						case 5:
							this.ᜁ.Dispose();
							this.ᜁ = null;
							num = 1;
							continue;
						case 6:
							goto IL_CF;
						}
						IL_4B:
						if (Disposing)
						{
							num = 2;
							continue;
						}
						goto IL_84;
						goto IL_4B;
						IL_84:
						num = 0;
						continue;
						IL_BD:
						this.ᜀ = true;
						num = 6;
					}
					IL_CF:;
				}
				finally
				{
					base.Dispose(Disposing);
				}
			}
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x000675DC File Offset: 0x000665DC
		public override void SaveToFile()
		{
			for (;;)
			{
				for (;;)
				{
					spr\u2561.ᜀ = this.ᜁ;
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
								break;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								AboutDataExport.ShowAbout(false);
								num = 4;
								continue;
							}
							break;
						case 1:
							if (Environment.UserInteractive)
							{
								num = 0;
								continue;
							}
							goto IL_99;
						case 2:
							if (this.ᜁ)
							{
								num = 3;
								continue;
							}
							goto IL_99;
						case 3:
							num = 1;
							continue;
						case 4:
							goto IL_50;
						}
						break;
					}
				}
			}
			IL_50:
			IL_99:
			base.SaveToFile();
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00067688 File Offset: 0x00066688
		public void SaveToFile(string fileName)
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
			this.FileName = fileName;
			this.SaveToFile();
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x000676D0 File Offset: 0x000666D0
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

		// Token: 0x06000A26 RID: 2598 RVA: 0x00067720 File Offset: 0x00066720
		private void ᜀ(HtmlExportStyles.RHTMLTemplate A_0)
		{
			for (;;)
			{
				IL_14:
				this.ᜇ.BackgroundColor = A_0.RBackgroundColor;
				this.ᜇ.LinkColor = A_0.RLinkColor;
				this.ᜇ.LinkVisitedColor = A_0.RVLinkColor;
				this.ᜇ.LinkActiveColor = A_0.RALinkColor;
				this.ᜇ.FontColor = A_0.RDefaultTextColor;
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜇ.Font != null)
						{
							num = 2;
							continue;
						}
						goto IL_DB;
					case 1:
						goto IL_B3;
					case 2:
						this.ᜇ.Font.Dispose();
						num = 1;
						continue;
					}
					goto IL_14;
				}
				IL_B3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_C9;
				}
			}
			IL_C9:
			if (false)
			{
			}
			IL_DB:
			this.ᜇ.Font = new Font(A_0.RTextFontName, 8f);
			this.ᜉ.HeadersBackColor = A_0.RHeadersRowBgColor;
			this.ᜉ.HeadersFontColor = A_0.RHeadersRowFontColor;
			this.ᜉ.BackColor = A_0.RTableBgColor;
			this.ᜉ.FontColor = A_0.RTableFontColor;
			this.ᜉ.OddBackColor = A_0.ROddRowBgColor;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00067880 File Offset: 0x00066880
		private int ᜀ(StringListCollection A_0)
		{
			int a_ = 6;
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int num = 0;
				IEnumerator enumerator = A_0.GetEnumerator();
				try
				{
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							if (!enumerator.MoveNext())
							{
								num2 = 3;
								continue;
							}
							string text = (string)enumerator.Current;
							num += text.Length + HyperlinksCollectionEditor.b("⼡⸣", a_).Length;
							num2 = 2;
							continue;
						}
						case 3:
							num2 = 4;
							continue;
						case 4:
							goto IL_B5;
						}
						IL_90:
						num2 = 0;
						continue;
						goto IL_90;
					}
					IL_B5:;
				}
				finally
				{
					for (;;)
					{
						IL_CC:
						IDisposable disposable = enumerator as IDisposable;
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								if (disposable != null)
								{
									num2 = 2;
									continue;
								}
								goto IL_117;
							case 1:
								goto IL_F9;
							case 2:
								disposable.Dispose();
								num2 = 1;
								continue;
							}
							goto IL_CC;
						}
						IL_F9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_10F;
						}
					}
					IL_10F:
					if (false)
					{
					}
					IL_117:;
				}
				return num;
			}
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x000679B8 File Offset: 0x000669B8
		private void ᜃ()
		{
			int a_ = 16;
			switch (0)
			{
			default:
			{
				string fileName = this.FileName;
				FileStream fileStream = new FileStream(fileName, FileMode.Create);
				StreamWriter streamWriter = new StreamWriter(fileStream, base.CurrentEncoding);
				spr\u217F spr_u217F = new spr\u217F(this, fileStream, streamWriter);
				try
				{
					for (;;)
					{
						spr_u217F.ᜂ();
						spr_u217F.ᜈ();
						int num = 3;
						for (;;)
						{
							string str;
							int num2;
							switch (num)
							{
							case 0:
								spr_u217F.ᜉ();
								spr_u217F.ᜄ();
								spr_u217F.ᜊ();
								num = 9;
								continue;
							case 1:
							{
								spr_u217F.ᜇ();
								IEnumerator enumerator = this.ᜋ.GetEnumerator();
								num = 11;
								continue;
							}
							case 2:
								goto IL_1FC;
							case 3:
								if (this.ᜊ == UsingCSS.Internal)
								{
									num = 1;
									continue;
								}
								spr_u217F.ᜅ(this.ᜆ);
								num = 15;
								continue;
							case 4:
								goto IL_237;
							case 5:
								if (this.ᜅ.LinkTemplate.Length == 0)
								{
									num = 16;
									continue;
								}
								spr_u217F.ᜇ(string.Format(HyperlinksCollectionEditor.b("ါ伭ု娱䘳匵帷ܹḻ䔽瀿㽁晃硅㍇等ㅋ牍罏㍑橓", a_), Path.GetFileNameWithoutExtension(fileName) + str + Path.GetExtension(fileName), this.ᜅ.LinkTemplate + str));
								num = 10;
								continue;
							case 6:
								if (num2 < 10)
								{
									num = 13;
									continue;
								}
								str = num2.ToString();
								num = 12;
								continue;
							case 7:
								if (num2 > this.ᜌ)
								{
									num = 0;
									continue;
								}
								spr_u217F.ᜃ();
								num = 6;
								continue;
							case 8:
								goto IL_2E8;
							case 9:
								goto IL_3B4;
							case 10:
								goto IL_287;
							case 11:
								goto IL_123;
								try
								{
									for (;;)
									{
										IL_123:
										num = 0;
										for (;;)
										{
											switch (num)
											{
											case 1:
												goto IL_1AE;
											case 2:
												switch ((1 == 1) ? 1 : 0)
												{
												case 0:
												case 2:
													goto IL_123;
												default:
													if (false)
													{
													}
													num = 1;
													continue;
												}
												break;
											case 4:
											{
												IEnumerator enumerator;
												if (!enumerator.MoveNext())
												{
													num = 2;
													continue;
												}
												string a_2 = (string)enumerator.Current;
												spr_u217F.ᜇ(a_2);
												num = 3;
												continue;
											}
											}
											IL_14B:
											num = 4;
											continue;
											goto IL_14B;
										}
									}
									IL_1AE:
									goto IL_BD;
								}
								finally
								{
									for (;;)
									{
										IEnumerator enumerator;
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
												goto IL_1FB;
											case 1:
												disposable.Dispose();
												num = 2;
												continue;
											case 2:
												goto IL_1F9;
											}
											break;
										}
									}
									IL_1F9:
									IL_1FB:;
								}
								goto IL_1FC;
								IL_BD:
								spr_u217F.ᜅ();
								num = 8;
								continue;
							case 12:
								goto IL_1FC;
							case 13:
								str = '0' + num2.ToString();
								num = 2;
								continue;
							case 14:
								goto IL_237;
							case 15:
								goto IL_2E8;
							case 16:
								spr_u217F.ᜇ(string.Format(HyperlinksCollectionEditor.b("ါ伭ု娱䘳匵帷ܹḻ䔽瀿㽁晃硅㍇等ㅋ牍罏㍑橓", a_), Path.GetFileNameWithoutExtension(fileName) + str + Path.GetExtension(fileName), Path.GetFileNameWithoutExtension(fileName) + str + Path.GetExtension(fileName)));
								num = 17;
								continue;
							case 17:
								goto IL_287;
							}
							break;
							IL_1FC:
							fileName = Path.GetFileName(this.FileName);
							num = 5;
							continue;
							IL_237:
							num = 7;
							continue;
							IL_287:
							num2++;
							num = 14;
							continue;
							IL_2E8:
							spr_u217F.ᜀ();
							spr_u217F.ᜁ();
							spr_u217F.ᜆ();
							str = string.Empty;
							num2 = 1;
							num = 4;
						}
					}
					IL_3B4:;
				}
				finally
				{
					if (true)
					{
					}
					streamWriter.Close();
					fileStream.Close();
				}
				return;
			}
			}
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00067DD0 File Offset: 0x00066DD0
		private void ᜂ()
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
			this.\u1712.Clear();
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00067E18 File Offset: 0x00066E18
		private new bool ᜁ()
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
			return File.Exists(this.ᜉ.BackImageUrl);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00067E64 File Offset: 0x00066E64
		private void ᜀ(string A_0)
		{
			int a_ = 16;
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜀ().ᜂ();
					this.ᜀ().ᜈ();
					this.ᜀ().ᜁ(this.Title);
					int num = 17;
					for (;;)
					{
						FileStream fileStream;
						IEnumerator enumerator3;
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
									case 0:
										goto IL_655;
									case 2:
										num = 0;
										continue;
									case 4:
									{
										IEnumerator enumerator;
										if (!enumerator.MoveNext())
										{
											num = 2;
											continue;
										}
										string a_2 = (string)enumerator.Current;
										this.ᜀ().ᜇ(a_2);
										num = 1;
										continue;
									}
									}
									IL_62F:
									num = 4;
									continue;
									goto IL_62F;
								}
								IL_655:
								goto IL_4B8;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator;
									IDisposable disposable = enumerator as IDisposable;
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_6A0;
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
											goto IL_6A2;
										}
										break;
									}
								}
								IL_6A0:
								IL_6A2:;
							}
							return;
							IL_4B8:
							this.ᜀ().ᜅ();
							num = 9;
							continue;
						case 1:
							num = 16;
							continue;
						case 2:
							if (File.Exists(this.ᜆ))
							{
								num = 18;
								continue;
							}
							goto IL_5C6;
						case 3:
							if (this.\u1712[this.ᜌ - 1] != null)
							{
								num = 11;
								continue;
							}
							goto IL_449;
						case 4:
							if (this.ᜀ().\u170D() is FileStream)
							{
								num = 1;
								continue;
							}
							goto IL_449;
						case 5:
							if (this.ᜄ > 0)
							{
								if (true)
								{
								}
								num = 23;
								continue;
							}
							goto IL_449;
						case 6:
							if (this.ᜎ)
							{
								num = 10;
								continue;
							}
							goto IL_2DB;
						case 7:
							if (this.ᜆ.Length == 0)
							{
								num = 21;
								continue;
							}
							goto IL_4FC;
						case 8:
							goto IL_5C6;
						case 9:
							goto IL_2DB;
						case 10:
							goto IL_592;
						case 11:
						{
							int a_3 = (int)this.ᜀ().\u170D().Position;
							(this.\u1712[this.ᜌ - 1] as sprᾐ).ᜁ(a_3);
							num = 25;
							continue;
						}
						case 12:
							try
							{
								StreamWriter streamWriter = new StreamWriter(fileStream, base.CurrentEncoding);
								try
								{
									IEnumerator enumerator2 = this.ᜋ.GetEnumerator();
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
												if (!enumerator2.MoveNext())
												{
													num = 0;
													continue;
												}
												string value = (string)enumerator2.Current;
												streamWriter.WriteLine(value);
												num = 1;
												continue;
											}
											case 3:
												goto IL_198;
											}
											IL_172:
											num = 2;
											continue;
											goto IL_172;
										}
										IL_198:;
									}
									finally
									{
										for (;;)
										{
											IDisposable disposable2 = enumerator2 as IDisposable;
											num = 0;
											for (;;)
											{
												switch (num)
												{
												case 0:
													if (disposable2 != null)
													{
														num = 1;
														continue;
													}
													goto IL_1E2;
												case 1:
													disposable2.Dispose();
													num = 2;
													continue;
												case 2:
													goto IL_1E0;
												}
												break;
											}
										}
										IL_1E0:
										IL_1E2:;
									}
								}
								finally
								{
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_220;
										case 1:
											((IDisposable)streamWriter).Dispose();
											num = 0;
											continue;
										}
										if (streamWriter == null)
										{
											break;
										}
										num = 1;
									}
									IL_220:;
								}
								goto IL_2DB;
							}
							finally
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_263;
									case 2:
										((IDisposable)fileStream).Dispose();
										num = 0;
										continue;
									}
									if (fileStream == null)
									{
										break;
									}
									num = 2;
								}
								IL_263:;
							}
							goto IL_266;
						case 13:
							num = 6;
							continue;
						case 14:
						{
							this.ᜀ().ᜇ();
							IEnumerator enumerator = this.ᜋ.GetEnumerator();
							num = 0;
							continue;
						}
						case 15:
							if (this.ᜀ().\u170D() is FileStream)
							{
								num = 22;
								continue;
							}
							goto IL_2DB;
						case 16:
							if (this.ᜌ > 0)
							{
								num = 20;
								continue;
							}
							goto IL_449;
						case 17:
							if (this.ᜊ == UsingCSS.Internal)
							{
								num = 14;
								continue;
							}
							goto IL_266;
						case 18:
							goto IL_400;
						case 19:
							goto IL_4FC;
						case 20:
							num = 3;
							continue;
						case 21:
							this.ᜆ = Path.ChangeExtension(A_0, HyperlinksCollectionEditor.b("ȫ䴭䌯䄱", a_));
							num = 19;
							continue;
						case 22:
							num = 7;
							continue;
						case 23:
							num = 4;
							continue;
						case 24:
							try
							{
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 2:
										goto IL_396;
									case 3:
									{
										if (!enumerator3.MoveNext())
										{
											num = 4;
											continue;
										}
										string a_4 = (string)enumerator3.Current;
										this.ᜀ().ᜇ(a_4);
										num = 1;
										continue;
									}
									case 4:
										num = 2;
										continue;
									}
									IL_370:
									num = 3;
									continue;
									goto IL_370;
								}
								IL_396:
								return;
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
												goto IL_3C6;
											}
											goto IL_3FF;
										case 1:
											goto IL_3FD;
										case 2:
											disposable3.Dispose();
											switch ((1 == 1) ? 1 : 0)
											{
											case 0:
											case 2:
												goto IL_3C6;
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
										IL_3C6:
										num = 2;
									}
								}
								IL_3FD:
								IL_3FF:;
							}
							goto IL_400;
						case 25:
							goto IL_449;
						case 26:
							if (File.Exists(this.ᜆ))
							{
								num = 13;
								continue;
							}
							goto IL_592;
						}
						break;
						IL_266:
						num = 15;
						continue;
						IL_2DB:
						this.ᜀ().ᜀ();
						this.ᜀ().ᜁ();
						num = 5;
						continue;
						IL_400:
						File.Delete(this.ᜆ);
						num = 8;
						continue;
						IL_449:
						enumerator3 = base.Header.GetEnumerator();
						num = 24;
						continue;
						IL_4FC:
						this.ᜀ().ᜅ(this.ᜆ);
						num = 26;
						continue;
						IL_592:
						num = 2;
						continue;
						IL_5C6:
						fileStream = File.Create(this.ᜆ);
						num = 12;
					}
				}
				return;
			}
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00068590 File Offset: 0x00067590
		protected override void WriteRow()
		{
			int a_ = 4;
			switch (0)
			{
			default:
			{
				int num = 37;
				IEnumerator enumerator2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (base.Options.InsertRowAfterTitle)
						{
							num = 42;
							continue;
						}
						goto IL_264;
					case 1:
						num = 9;
						continue;
					case 2:
						num = 32;
						continue;
					case 3:
						goto IL_49E;
					case 4:
						(this.\u1712[this.ᜌ - 1] as sprᾐ).ᜀ((int)this.ᜑ.Position);
						num = 12;
						continue;
					case 5:
						goto IL_264;
					case 6:
						goto IL_513;
					case 7:
						this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("\u001fȡᠣथ尧堩ራ", a_));
						num = 0;
						continue;
					case 8:
						if (this.ᜌ > 0)
						{
							num = 28;
							continue;
						}
						goto IL_3CD;
					case 9:
						if (this.ᜄ > 0)
						{
							num = 36;
							continue;
						}
						goto IL_264;
					case 10:
						goto IL_1FE;
					case 11:
						goto IL_74A;
					case 12:
						goto IL_3CD;
					case 13:
					{
						this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("\u001fȡᠣ別娧ᐩ", a_));
						int num2 = 0;
						num = 31;
						continue;
					}
					case 14:
						if (this.\u1712[this.ᜌ - 1] != null)
						{
							num = 4;
							continue;
						}
						goto IL_3CD;
					case 15:
						this.ᜀ().ᜋ().Close();
						num = 27;
						continue;
					case 16:
						if (base.AddTitles)
						{
							num = 13;
							continue;
						}
						goto IL_264;
					case 17:
						if (base.RowsCount % this.ᜄ == 0)
						{
							num = 34;
							continue;
						}
						goto IL_264;
					case 18:
						this.ᜀ().\u170D().Close();
						if (true)
						{
						}
						num = 30;
						continue;
					case 19:
						try
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
								{
									IEnumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num = 3;
										continue;
									}
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
										string a_2 = (string)enumerator.Current;
										this.ᜀ().ᜇ(a_2);
										num = 1;
										continue;
									}
									}
									break;
								}
								case 3:
									num = 4;
									continue;
								case 4:
									goto IL_355;
								}
								IL_32C:
								num = 0;
								continue;
								IL_2EA:
								goto IL_32C;
								goto IL_2EA;
							}
							IL_355:
							goto IL_475;
						}
						finally
						{
							for (;;)
							{
								IEnumerator enumerator;
								IDisposable disposable = enumerator as IDisposable;
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_3A0;
									case 1:
										if (disposable != null)
										{
											num = 2;
											continue;
										}
										goto IL_3A2;
									case 2:
										disposable.Dispose();
										num = 0;
										continue;
									}
									break;
								}
							}
							IL_3A0:
							IL_3A2:;
						}
						goto IL_3A3;
						IL_475:
						num = 8;
						continue;
					case 20:
					{
						int num2;
						if (num2 >= base.ColumnsExport.Count)
						{
							num = 7;
							continue;
						}
						this.ᜀ().ᜀ(this.GetColumnTitle(num2));
						num2++;
						num = 11;
						continue;
					}
					case 21:
						goto IL_49E;
					case 22:
						if (this.\u170D)
						{
							num = 26;
							continue;
						}
						goto IL_6B5;
					case 23:
						if (this.ᜀ().\u170D() is FileStream)
						{
							num = 2;
							continue;
						}
						goto IL_778;
					case 24:
						if (this.ᜄ > 0)
						{
							num = 29;
							continue;
						}
						goto IL_778;
					case 25:
						if (this.ᜀ().\u170D() is FileStream)
						{
							num = 10;
							continue;
						}
						goto IL_264;
					case 26:
						this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("ᰟ嘡䔣䐥䐧伩ఫ䴭唯帱堳䔵䠷嬹弻圽⸿╁祃癅桇⥉⥋≍㱏≑㕓㉕㱗㍙㉛㥝嵟剡䑣եѧ୩Ὣᵭ䵯偱⁳ѵ㩷㥹幻䁽", a_));
						this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("ᰟ嘡嘣ᠥᐧ帩䠫ိ", a_));
						num = 33;
						continue;
					case 27:
						goto IL_688;
					case 28:
						num = 14;
						continue;
					case 29:
						num = 23;
						continue;
					case 30:
						goto IL_123;
					case 31:
						goto IL_74A;
					case 32:
						if (this.ᜌ > 0)
						{
							num = 38;
							continue;
						}
						goto IL_513;
					case 33:
						goto IL_6B5;
					case 34:
						num = 25;
						continue;
					case 35:
						if (this.ᜀ().ᜋ() != null)
						{
							num = 15;
							continue;
						}
						goto IL_688;
					case 36:
						num = 17;
						continue;
					case 38:
					{
						this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("ᰟഡ倣䜥䨧䘩䤫ိ", a_));
						IEnumerator enumerator = base.Footer.GetEnumerator();
						num = 19;
						continue;
					}
					case 39:
						if (this.ᜀ().\u170D() != null)
						{
							num = 18;
							continue;
						}
						goto IL_123;
					case 40:
						goto IL_778;
					case 41:
						goto IL_3A3;
					case 42:
					{
						this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("\u001fȡᠣ別娧ᐩ", a_));
						int num3 = 0;
						num = 3;
						continue;
					}
					case 43:
					{
						int num3;
						if (num3 >= base.ColumnsExport.Count)
						{
							num = 41;
							continue;
						}
						this.ᜀ().ᜂ(HyperlinksCollectionEditor.b("؟䰡䘣唥堧ᄩ", a_));
						num3++;
						num = 21;
						continue;
					}
					case 44:
						goto IL_296;
					}
					if (base.RowsCount != 0)
					{
						num = 1;
						continue;
					}
					goto IL_1FE;
					IL_123:
					string text;
					this.ᜑ = new FileStream(text, FileMode.Create);
					this.ᜀ().ᜀ(this.ᜑ);
					this.ᜀ().ᜀ(new StreamWriter(this.ᜑ, base.CurrentEncoding));
					(this.ᜀ().ᜋ() as StreamWriter).AutoFlush = true;
					sprᾐ value = new sprᾐ();
					this.\u1712.Add(value);
					num = 40;
					continue;
					IL_1FE:
					text = this.FileName;
					num = 24;
					continue;
					IL_264:
					this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("\u001fȡᠣ別娧ᐩ", a_));
					enumerator2 = base.ExportRowExport.GetEnumerator();
					num = 44;
					continue;
					IL_3A3:
					this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("\u001fȡᠣथ尧堩ራ", a_));
					num = 5;
					continue;
					IL_3CD:
					this.ᜀ().ᜄ();
					this.ᜀ().ᜊ();
					num = 6;
					continue;
					IL_49E:
					num = 43;
					continue;
					IL_513:
					this.ᜌ++;
					text = spr\u2059.ᜀ(text, this.ᜌ, 2);
					num = 35;
					continue;
					IL_688:
					num = 39;
					continue;
					IL_6B5:
					this.ᜀ().ᜀ(this.ᜉ.BorderWidth, this.ᜉ.CellPadding, this.ᜉ.CellSpacing, this.ᜉ.BackImageUrl);
					this.ᜐ = true;
					num = 16;
					continue;
					IL_74A:
					num = 20;
					continue;
					IL_778:
					this.ᜀ(text);
					num = 22;
				}
				IL_296:
				try
				{
					num = 7;
					for (;;)
					{
						ColExport colExport;
						switch (num)
						{
						case 0:
							this.ᜀ().ᜃ(colExport.GetExportedValue(true));
							num = 14;
							continue;
						case 1:
							num = 5;
							continue;
						case 2:
							num = 8;
							continue;
						case 3:
							if ((base.RowsCount + 1) % this.ᜄ == 0)
							{
								num = 15;
								continue;
							}
							goto IL_929;
						case 5:
							if (((base.RowsCount + 1) % this.ᜄ & 1) != 1)
							{
								num = 2;
								continue;
							}
							goto IL_905;
						case 8:
							if ((this.ᜄ & 1) == 1)
							{
								num = 9;
								continue;
							}
							goto IL_929;
						case 9:
							num = 3;
							continue;
						case 10:
							goto IL_9A7;
						case 11:
							num = 10;
							continue;
						case 12:
							if (this.ᜄ > 0)
							{
								num = 1;
								continue;
							}
							num = 17;
							continue;
						case 15:
							goto IL_905;
						case 16:
							if (!enumerator2.MoveNext())
							{
								num = 11;
								continue;
							}
							colExport = (ColExport)enumerator2.Current;
							num = 12;
							continue;
						case 17:
							if ((base.RowsCount + 1 & 1) == 1)
							{
								num = 0;
								continue;
							}
							this.ᜀ().ᜄ(colExport.GetExportedValue(true));
							num = 6;
							continue;
						}
						IL_868:
						num = 16;
						continue;
						goto IL_868;
						IL_905:
						this.ᜀ().ᜃ(colExport.GetExportedValue(true));
						num = 13;
						continue;
						IL_929:
						this.ᜀ().ᜄ(colExport.GetExportedValue(true));
						num = 4;
					}
					IL_9A7:;
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
								goto IL_9EF;
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
								goto IL_9F1;
							}
							break;
						}
					}
					IL_9EF:
					IL_9F1:;
				}
				this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("\u001fȡᠣथ尧堩ራ", a_));
				return;
			}
			}
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00068FDC File Offset: 0x00067FDC
		private void ᜀ(ref string A_0, ref bool A_1)
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
			A_0 += ' ';
			A_1 = true;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0006902C File Offset: 0x0006802C
		protected override string GetColumnValue(ColExport ExportColExport)
		{
			int a_ = 9;
			switch (0)
			{
			default:
			{
				string text;
				for (;;)
				{
					text = base.GetColumnValue(ExportColExport);
					int columnIndex = ExportColExport.ColumnIndex;
					string text2 = string.Empty;
					ColumAlign colAlign = base.ColumnsExport[columnIndex].ColAlign;
					string text3 = string.Empty;
					bool flag = false;
					Color color = Color.Empty;
					int num = 1;
					for (;;)
					{
						Font font;
						Color fontColor;
						switch (num)
						{
						case 0:
							text = HyperlinksCollectionEditor.b("̤䤦䬨堪崬ᐮ", a_);
							num = 7;
							continue;
						case 1:
							if (text.Length == 0)
							{
								num = 0;
								continue;
							}
							goto IL_F6;
						case 2:
							goto IL_97;
						case 3:
							goto IL_97;
						case 4:
							goto IL_99F;
						case 5:
							try
							{
								for (;;)
								{
									base.ᜀ(this, new CellParamsEventArgs(base.RowsCount, columnIndex, text, colAlign, font, color));
									text2 = ' ' + string.Format(HyperlinksCollectionEditor.b("䐤䬦䀨䰪䌬ሮጰ䠲Դ䨶ᬸ", a_), this.ᜀ().ᜀ(colAlign));
									num = 50;
									for (;;)
									{
										switch (num)
										{
										case 0:
											if (string.Compare(font.Name, this.ᜇ.Font.Name, true) != 0)
											{
												num = 13;
												continue;
											}
											goto IL_3AA;
										case 1:
											if ((this.ᜇ.Font.Style & FontStyle.Underline) == FontStyle.Underline)
											{
												num = 16;
												continue;
											}
											goto IL_2FE;
										case 2:
											if ((font.Style & FontStyle.Italic) != FontStyle.Italic)
											{
												num = 18;
												continue;
											}
											goto IL_900;
										case 3:
											goto IL_322;
										case 4:
											num = 30;
											continue;
										case 5:
											num = 22;
											continue;
										case 6:
											goto IL_34A;
										case 7:
											num = 8;
											continue;
										case 8:
											if ((this.ᜇ.Font.Style & FontStyle.Italic) != FontStyle.Italic)
											{
												num = 20;
												continue;
											}
											goto IL_28F;
										case 9:
											if (fontColor != this.ᜇ.FontColor)
											{
												num = 36;
												continue;
											}
											goto IL_5C4;
										case 10:
											if ((font.Style & FontStyle.Italic) == FontStyle.Italic)
											{
												num = 7;
												continue;
											}
											goto IL_28F;
										case 11:
											text3 = string.Format(HyperlinksCollectionEditor.b("䜤䘦䨨䀪䨬崮帰䘲嬴匶ᐸ堺刼匾⹀ㅂ罄杆㉈筊が瑎", a_), spr\u2089.ᜀ(color));
											this.ᜀ(ref text3, ref flag);
											num = 15;
											continue;
										case 12:
											text3 += string.Format(HyperlinksCollectionEditor.b("儤䈦儨弪,䬮吰倲娴䔶堸伺吼倾⽀祂敄㱆祈㙊癌", a_), HyperlinksCollectionEditor.b("値䤦䴨个弬䌮堰崲倴", a_));
											this.ᜀ(ref text3, ref flag);
											num = 32;
											continue;
										case 13:
											text3 += string.Format(HyperlinksCollectionEditor.b("䌤䠦䜨弪,䤮倰帲尴嬶䀸ĺᴼ䐾煀㹂繄", a_), font.Name);
											this.ᜀ(ref text3, ref flag);
											num = 43;
											continue;
										case 14:
											goto IL_5ED;
										case 15:
											goto IL_5ED;
										case 16:
											text3 += string.Format(HyperlinksCollectionEditor.b("儤䈦儨弪,䬮吰倲娴䔶堸伺吼倾⽀祂敄㱆祈㙊癌", a_), HyperlinksCollectionEditor.b("䬤䠦䜨个", a_));
											this.ᜀ(ref text3, ref flag);
											num = 26;
											continue;
										case 17:
											text3 += string.Format(HyperlinksCollectionEditor.b("䌤䠦䜨弪,尮䔰䨲头制̸ᬺ䘼༾㱀硂", a_), HyperlinksCollectionEditor.b("䬤䠦嬨䘪䰬䌮", a_));
											this.ᜀ(ref text3, ref flag);
											num = 42;
											continue;
										case 18:
											num = 25;
											continue;
										case 19:
											text3 = string.Format(HyperlinksCollectionEditor.b("䜤䘦䨨䀪䨬崮帰䘲嬴匶ᐸ堺刼匾⹀ㅂ罄杆㉈筊が瑎", a_), spr\u2089.ᜀ(color));
											this.ᜀ(ref text3, ref flag);
											num = 14;
											continue;
										case 20:
											text3 += string.Format(HyperlinksCollectionEditor.b("䌤䠦䜨弪,尮䔰䨲头制̸ᬺ䘼༾㱀硂", a_), HyperlinksCollectionEditor.b("䰤匦䠨䜪䐬䰮", a_));
											this.ᜀ(ref text3, ref flag);
											num = 31;
											continue;
										case 21:
											goto IL_696;
										case 22:
											if (text3.Length >= 2)
											{
												num = 44;
												continue;
											}
											goto IL_322;
										case 23:
											num = 53;
											continue;
										case 24:
											if ((font.Style & FontStyle.Bold) != FontStyle.Bold)
											{
												num = 4;
												continue;
											}
											goto IL_7C0;
										case 25:
											if ((this.ᜇ.Font.Style & FontStyle.Italic) == FontStyle.Italic)
											{
												num = 17;
												continue;
											}
											goto IL_900;
										case 26:
											goto IL_2FE;
										case 27:
											switch ((1 == 1) ? 1 : 0)
											{
											case 0:
											case 2:
												goto IL_6CA;
											default:
												if (false)
												{
												}
												if (font.Size != this.ᜇ.Font.Size)
												{
													num = 34;
													continue;
												}
												goto IL_696;
											}
											break;
										case 28:
											goto IL_6CA;
										case 29:
											text3 += string.Format(HyperlinksCollectionEditor.b("䌤䠦䜨弪,堮吰娲刴弶䴸ĺᴼ䐾煀㹂繄", a_), HyperlinksCollectionEditor.b("䜤䠦䔨伪", a_));
											this.ᜀ(ref text3, ref flag);
											num = 6;
											continue;
										case 30:
											if ((this.ᜇ.Font.Style & FontStyle.Bold) == FontStyle.Bold)
											{
												num = 28;
												continue;
											}
											goto IL_7C0;
										case 31:
											goto IL_28F;
										case 32:
											goto IL_7E9;
										case 33:
											if ((font.Style & FontStyle.Underline) == FontStyle.Underline)
											{
												num = 45;
												continue;
											}
											goto IL_7E9;
										case 34:
											text3 += string.Format(HyperlinksCollectionEditor.b("䌤䠦䜨弪,尮堰䤲倴ശᤸ䀺഼䈾ㅀ㝂繄", a_), font.Size);
											this.ᜀ(ref text3, ref flag);
											num = 21;
											continue;
										case 35:
											if (flag)
											{
												num = 5;
												continue;
											}
											goto IL_95E;
										case 36:
											text3 += string.Format(HyperlinksCollectionEditor.b("䘤䠦䔨䐪弬ᔮᄰ䠲Դ䨶ȸ", a_), spr\u2089.ᜀ(fontColor));
											this.ᜀ(ref text3, ref flag);
											num = 47;
											continue;
										case 37:
											num = 39;
											continue;
										case 38:
											if ((this.ᜇ.Font.Style & FontStyle.Underline) != FontStyle.Underline)
											{
												num = 12;
												continue;
											}
											goto IL_7E9;
										case 39:
											if ((this.ᜇ.Font.Style & FontStyle.Bold) != FontStyle.Bold)
											{
												num = 29;
												continue;
											}
											goto IL_34A;
										case 40:
											if ((font.Style & FontStyle.Underline) != FontStyle.Underline)
											{
												num = 46;
												continue;
											}
											goto IL_2FE;
										case 41:
											goto IL_7C0;
										case 42:
											goto IL_900;
										case 43:
											goto IL_3AA;
										case 44:
											text3 = text3.Remove(text3.Length - 2, 2);
											num = 3;
											continue;
										case 45:
											num = 38;
											continue;
										case 46:
											num = 1;
											continue;
										case 47:
											goto IL_5C4;
										case 48:
											goto IL_995;
										case 49:
											if ((font.Style & FontStyle.Bold) == FontStyle.Bold)
											{
												num = 37;
												continue;
											}
											goto IL_34A;
										case 50:
											if ((base.RowsCount & 1) == 1)
											{
												num = 23;
												continue;
											}
											num = 51;
											continue;
										case 51:
											if (color != this.ᜉ.OddBackColor)
											{
												num = 19;
												continue;
											}
											goto IL_5ED;
										case 52:
											goto IL_95E;
										case 53:
											if (color != this.ᜉ.BackColor)
											{
												num = 11;
												continue;
											}
											goto IL_5ED;
										}
										break;
										IL_28F:
										num = 2;
										continue;
										IL_2FE:
										num = 35;
										continue;
										IL_322:
										text3 = string.Format(HyperlinksCollectionEditor.b("Ԥ否崨刪䄬䨮రᄲ临ܶ䐸᤺", a_), text3);
										num = 52;
										continue;
										IL_34A:
										num = 24;
										continue;
										IL_3AA:
										num = 27;
										continue;
										IL_5C4:
										num = 49;
										continue;
										IL_5ED:
										num = 0;
										continue;
										IL_696:
										num = 9;
										continue;
										IL_6CA:
										text3 += string.Format(HyperlinksCollectionEditor.b("䌤䠦䜨弪,堮吰娲刴弶䴸ĺᴼ䐾煀㹂繄", a_), HyperlinksCollectionEditor.b("䬤䠦嬨䘪䰬䌮", a_));
										this.ᜀ(ref text3, ref flag);
										num = 41;
										continue;
										IL_7C0:
										num = 10;
										continue;
										IL_7E9:
										num = 40;
										continue;
										IL_900:
										num = 33;
										continue;
										IL_95E:
										text = string.Concat(new object[]
										{
											text2,
											text3,
											'>',
											text
										});
										num = 48;
									}
								}
								IL_995:
								return text;
							}
							finally
							{
								font.Dispose();
							}
							goto IL_99F;
						case 6:
							if ((base.RowsCount & 1) == 1)
							{
								num = 4;
								continue;
							}
							color = this.ᜉ.OddBackColor;
							if (true)
							{
							}
							num = 2;
							continue;
						case 7:
							goto IL_F6;
						}
						break;
						IL_97:
						font = (this.ᜇ.Font.Clone() as Font);
						fontColor = this.ᜇ.FontColor;
						num = 5;
						continue;
						IL_F6:
						num = 6;
						continue;
						IL_99F:
						color = this.ᜉ.BackColor;
						num = 3;
					}
				}
				return text;
			}
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00069A14 File Offset: 0x00068A14
		protected override void WriteTitleRow()
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
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00069A50 File Offset: 0x00068A50
		protected override bool CharInSpecialCharacters(char Char)
		{
			switch (0)
			{
			default:
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_A5:
					goto IL_7B;
				default:
					if (false)
					{
					}
					goto IL_59;
				}
				int num;
				int num2;
				char[] array;
				for (;;)
				{
					IL_36:
					switch (num)
					{
					case 0:
						goto IL_A5;
					case 1:
						goto IL_94;
					case 2:
					{
						bool result = true;
						num = 6;
						continue;
					}
					case 3:
					{
						if (num2 >= array.Length)
						{
							num = 1;
							continue;
						}
						char c = array[num2];
						num = 5;
						continue;
					}
					case 4:
						goto IL_79;
					case 5:
					{
						char c;
						if (Char == c)
						{
							num = 2;
							continue;
						}
						num2++;
						num = 0;
						continue;
					}
					case 6:
					{
						bool result;
						return result;
					}
					}
					goto IL_59;
				}
				IL_79:
				goto IL_7B;
				IL_94:
				if (true)
				{
				}
				return false;
				IL_59:
				char[] array2 = new char[]
				{
					'<',
					'>',
					'&',
					'"'
				};
				array = array2;
				num2 = 0;
				num = 4;
				goto IL_36;
				IL_7B:
				num = 3;
				goto IL_36;
			}
			}
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00069B40 File Offset: 0x00068B40
		internal new spr\u217F ᜀ()
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
			return base.ᜀ() as spr\u217F;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00069B88 File Offset: 0x00068B88
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
			return typeof(spr\u217F);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00069BD0 File Offset: 0x00068BD0
		private void ᜀ(int A_0)
		{
			int a_ = 18;
			for (;;)
			{
				if (true)
				{
				}
				string text = new string(' ', A_0);
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
							goto IL_53;
						default:
							if (false)
							{
							}
							this.ᜋ.Add(string.Concat(new object[]
							{
								text,
								HyperlinksCollectionEditor.b("䠭弯就䀳ᬵ帷嬹儻圽ⰿ㭁繃晅", a_),
								this.ᜇ.Font.Name,
								';'
							}));
							num = 3;
							continue;
						}
						break;
					case 1:
						this.ᜋ.Add(string.Format(text + HyperlinksCollectionEditor.b("䠭弯就䀳ᬵ䬷匹䘻嬽稿扁㽃癅㕇㩉㡋畍", a_), this.ᜇ.Font.Size));
						num = 5;
						continue;
					case 2:
						if ((byte)(this.ᜇ.DefaultOptions & DefaultOptions.FontName) != 1)
						{
							goto IL_53;
						}
						goto IL_A7;
					case 3:
						goto IL_A7;
					case 4:
						if ((byte)(this.ᜇ.DefaultOptions & DefaultOptions.FontSize) != 2)
						{
							num = 1;
							continue;
						}
						return;
					case 5:
						return;
					}
					break;
					IL_53:
					num = 0;
					continue;
					IL_A7:
					num = 4;
				}
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00069D34 File Offset: 0x00068D34
		protected override void BeginDataExport()
		{
			int a_ = 12;
			for (;;)
			{
				base.BeginDataExport();
				this.ᜂ();
				this.ᜋ.Clear();
				this.ᜋ.Add(string.Concat(new object[]
				{
					HyperlinksCollectionEditor.b("ࠧ਩渫愭琯欱ᐳ䴵ᠷ堹崻崽⬿╁㙃⥅㵇⑉⡋瑍灏", a_),
					spr\u2089.ᜀ(this.ᜇ.BackgroundColor),
					';',
					HyperlinksCollectionEditor.b("ࠧ䤩䌫䈭弯䀱ำᘵ", a_),
					spr\u2089.ᜀ(this.ᜇ.FontColor),
					';'
				}));
				this.ᜀ(9);
				int num = 13;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.\u170D)
						{
							num = 7;
							continue;
						}
						goto IL_5A2;
					case 1:
						this.ᜋ.Add(string.Format(HyperlinksCollectionEditor.b("䨧䬩伫䔭圯䀱嬳䌵嘷帹ᄻ圽ⴿ⍁⍃⍅片橉㥋㱍㱏穑⽓晕╗獙", a_), this.ᜇ.BackImageUrl));
						num = 16;
						continue;
					case 2:
						goto IL_467;
					case 3:
						goto IL_31D;
					case 4:
						if (base.AddTitles)
						{
							num = 14;
							continue;
						}
						goto IL_1CB;
					case 5:
						goto IL_1CB;
					case 6:
						this.ᜋ.Add(string.Format(HyperlinksCollectionEditor.b("ࠧ਩ఫอုሱᐳᘵᠷᨹ᰻ḽ∿⍁❃ⵅ⽇㡉⍋㭍㹏㙑祓㕕㝗㙙㍛ⱝ婟䉡ὣ噥ᕧ兩", a_), spr\u2089.ᜀ(this.ᜉ.OddBackColor)));
						if (true)
						{
						}
						num = 3;
						continue;
					case 7:
						this.ᜋ.Add(HyperlinksCollectionEditor.b("ࠧ਩ȫ稭䈯瀱眳ᘵ䌷ᨹ帻弽⌿⥁⍃㑅❇㽉≋⩍絏ㅑ㭓㩕㝗⡙晛繝", a_) + spr\u2089.ᜀ(this.ᜉ.BorderColor) + HyperlinksCollectionEditor.b("ࠧ圩", a_));
						num = 2;
						continue;
					case 8:
						this.ᜋ.Add(string.Format(HyperlinksCollectionEditor.b("ࠧ਩ఫอုሱᐳᘵᠷᨹ᰻ḽ∿⍁❃ⵅ⽇㡉⍋㭍㹏㙑祓㕕㝗㙙㍛ⱝ婟䉡ὣ噥ᕧ兩", a_), spr\u2089.ᜀ(this.ᜉ.HeadersBackColor)));
						num = 15;
						continue;
					case 9:
						if (!this.ᜁ())
						{
							num = 10;
							continue;
						}
						goto IL_13A;
					case 10:
						this.ᜋ.Add(string.Format(HyperlinksCollectionEditor.b("ࠧ਩ఫอုሱᐳᘵᠷᨹ᰻ḽ∿⍁❃ⵅ⽇㡉⍋㭍㹏㙑祓㕕㝗㙙㍛ⱝ婟䉡ὣ噥ᕧ兩", a_), spr\u2089.ᜀ(this.ᜉ.BackColor)));
						num = 17;
						continue;
					case 11:
						if (!this.ᜁ())
						{
							num = 8;
							continue;
						}
						goto IL_46C;
					case 12:
						if (!this.ᜁ())
						{
							num = 6;
							continue;
						}
						goto IL_31D;
					case 13:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							if (this.ᜇ.BackImageUrl.Length <= 0)
							{
								goto IL_20B;
							}
							break;
						}
						num = 1;
						continue;
					case 14:
						this.ᜋ.Add(HyperlinksCollectionEditor.b("ࠧ਩ȫ稭堯怱嬳䄵䬷ᨹ䜻ḽ", a_));
						num = 11;
						continue;
					case 15:
						goto IL_46C;
					case 16:
						goto IL_20B;
					case 17:
						goto IL_13A;
					}
					break;
					IL_13A:
					this.ᜋ.Add(string.Format(HyperlinksCollectionEditor.b("ࠧ਩ఫอုሱᐳᘵᠷᨹ᰻ḽ⌿ⵁ⡃⥅㩇灉汋㕍恏⽑潓", a_), spr\u2089.ᜀ(this.ᜉ.FontColor)));
					this.ᜀ(12);
					this.ᜋ.Add(HyperlinksCollectionEditor.b("ࠧ਩儫", a_));
					this.ᜋ.Add(HyperlinksCollectionEditor.b("ࠧ਩ȫ稭䈯紱倳刵ᠷᨹ䜻", a_));
					num = 12;
					continue;
					IL_1CB:
					this.ᜋ.Add(HyperlinksCollectionEditor.b("ࠧ਩ȫ稭䈯怱嬳䄵䬷ᨹ䜻", a_));
					num = 9;
					continue;
					IL_20B:
					this.ᜋ.Add(HyperlinksCollectionEditor.b("ࠧ਩儫", a_));
					this.ᜋ.Add(HyperlinksCollectionEditor.b("ࠧ਩洫ᐭ尯嬱娳崵ᠷ䄹᰻崽⼿⹁⭃㑅片橉", a_) + spr\u2089.ᜀ(this.ᜇ.LinkColor) + HyperlinksCollectionEditor.b("ࠧ圩", a_));
					this.ᜋ.Add(HyperlinksCollectionEditor.b("ࠧ਩洫ᐭ䘯嬱䜳張䰷弹堻ḽ㬿扁❃⥅⑇╉㹋瑍灏", a_) + spr\u2089.ᜀ(this.ᜇ.LinkVisitedColor) + HyperlinksCollectionEditor.b("ࠧ圩", a_));
					this.ᜋ.Add(HyperlinksCollectionEditor.b("ࠧ਩洫ᐭ儯儱䀳張丷弹᰻䔽怿⅁⭃⩅❇㡉癋湍", a_) + spr\u2089.ᜀ(this.ᜇ.LinkActiveColor) + HyperlinksCollectionEditor.b("ࠧ圩", a_));
					this.\u170D = (this.ᜉ.BorderColor != Color.White);
					num = 4;
					continue;
					IL_31D:
					this.ᜋ.Add(string.Format(HyperlinksCollectionEditor.b("ࠧ਩ఫอုሱᐳᘵᠷᨹ᰻ḽ⌿ⵁ⡃⥅㩇灉汋㕍恏⽑潓", a_), spr\u2089.ᜀ(this.ᜉ.FontColor)));
					this.ᜀ(12);
					this.ᜋ.Add(HyperlinksCollectionEditor.b("ࠧ਩儫", a_));
					num = 0;
					continue;
					IL_46C:
					this.ᜋ.Add(string.Format(HyperlinksCollectionEditor.b("ࠧ਩ఫอုሱᐳᘵᠷᨹ᰻ḽ⌿ⵁ⡃⥅㩇灉汋㕍恏⽑潓", a_), spr\u2089.ᜀ(this.ᜉ.HeadersFontColor)));
					this.ᜋ.Add(HyperlinksCollectionEditor.b("ࠧ਩ఫอုሱᐳᘵᠷᨹ᰻ḽ☿ⵁ⩃㉅敇㵉⥋❍㝏㩑⁓汕硗㡙㍛㉝џ奡䑣ብ൧ቩᡫ䍭ᅯṱᵳᅵᙷ䁹屻ᵽ慎놉", a_));
					this.ᜀ(12);
					this.ᜋ.Add(HyperlinksCollectionEditor.b("ࠧ਩儫", a_));
					num = 5;
				}
			}
			IL_467:
			IL_5A2:
			this.ᜌ = 0;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0006A2EC File Offset: 0x000692EC
		protected override void AfterExport()
		{
			int a_ = 2;
			int num = 12;
			for (;;)
			{
				IEnumerator enumerator;
				switch (num)
				{
				case 0:
					goto IL_181;
				case 1:
					if (this.ᜄ > 0)
					{
						num = 6;
						continue;
					}
					goto IL_E1;
				case 2:
					goto IL_C5;
				case 3:
					if (this.ᜌ > 0)
					{
						num = 9;
						continue;
					}
					goto IL_E1;
				case 4:
					try
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_223;
							case 1:
							{
								if (!enumerator.MoveNext())
								{
									goto IL_233;
								}
								string a_2 = (string)enumerator.Current;
								this.ᜀ().ᜇ(a_2);
								num = 0;
								continue;
							}
							case 3:
								num = 4;
								continue;
							case 4:
								goto IL_245;
							}
							goto IL_1E3;
							IL_223:
							num = 1;
							continue;
							IL_1E3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								IL_233:
								num = 3;
								break;
							default:
								if (false)
								{
								}
								goto IL_223;
							}
						}
						IL_245:
						goto IL_186;
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
									goto IL_28A;
								case 1:
									goto IL_288;
								case 2:
									disposable.Dispose();
									num = 1;
									continue;
								}
								break;
							}
						}
						IL_288:
						IL_28A:;
					}
					goto IL_28B;
					IL_186:
					num = 1;
					continue;
				case 5:
					this.ᜀ().ᜄ();
					this.ᜀ().ᜊ();
					num = 0;
					continue;
				case 6:
					if (true)
					{
					}
					num = 15;
					continue;
				case 7:
					this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("∝༟嘡䔣䐥䐧伩ራ", a_));
					num = 2;
					continue;
				case 8:
					goto IL_28B;
				case 9:
					num = 13;
					continue;
				case 10:
					if (this.ᜐ)
					{
						num = 5;
						continue;
					}
					goto IL_2B2;
				case 11:
					goto IL_E1;
				case 13:
					if (this.\u1712[this.ᜌ - 1] != null)
					{
						num = 14;
						continue;
					}
					goto IL_E1;
				case 14:
					(this.\u1712[this.ᜌ - 1] as sprᾐ).ᜀ((int)this.ᜑ.Position);
					num = 11;
					continue;
				case 15:
					if (this.ᜀ().\u170D() is FileStream)
					{
						num = 8;
						continue;
					}
					goto IL_E1;
				}
				if (this.ᜐ)
				{
					num = 7;
					continue;
				}
				IL_C5:
				enumerator = base.Footer.GetEnumerator();
				num = 4;
				continue;
				IL_E1:
				num = 10;
				continue;
				IL_28B:
				num = 3;
			}
			IL_181:
			IL_2B2:
			base.AfterExport();
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0006A5C4 File Offset: 0x000695C4
		protected override void EndDataExport()
		{
			int a_ = 15;
			switch (0)
			{
			default:
			{
				int num = 3;
				for (;;)
				{
					if (true)
					{
					}
					byte[] array;
					MemoryStream memoryStream;
					switch (num)
					{
					case 0:
						num = 12;
						continue;
					case 1:
						num = 2;
						continue;
					case 2:
						if (this.ᜀ().ᜋ() != null)
						{
							num = 7;
							continue;
						}
						goto IL_8D3;
					case 4:
						this.ᜃ();
						num = 9;
						continue;
					case 5:
						goto IL_8D3;
					case 6:
						if (this.ᜀ().\u170D() is FileStream)
						{
							num = 4;
							continue;
						}
						goto IL_98E;
					case 7:
						this.ᜀ().ᜋ().Close();
						num = 5;
						continue;
					case 8:
						if (this.ᜃ)
						{
							num = 16;
							continue;
						}
						goto IL_98E;
					case 9:
						goto IL_98E;
					case 10:
						try
						{
							for (;;)
							{
								StringListCollection stringListCollection = new StringListCollection();
								int num2 = 1;
								num = 4;
								for (;;)
								{
									switch (num)
									{
									case 0:
										num = 5;
										continue;
									case 1:
									{
										if (num2 > this.ᜌ)
										{
											num = 0;
											continue;
										}
										string path = spr\u2059.ᜀ(this.FileName, num2, 2);
										FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
										this.ᜀ().ᜀ(fileStream);
										this.ᜀ().ᜀ(new StreamWriter(fileStream, base.CurrentEncoding));
										num = 2;
										continue;
									}
									case 2:
										try
										{
											num = 1;
											for (;;)
											{
												FileStream fileStream;
												IEnumerator enumerator;
												switch (num)
												{
												case 0:
													if (this.\u1712[num2 - 1] != null)
													{
														num = 22;
														continue;
													}
													goto IL_57E;
												case 2:
													if (fileStream.Read(array, 0, array.Length) != array.Length)
													{
														num = 6;
														continue;
													}
													goto IL_360;
												case 3:
													goto IL_822;
												case 4:
													if (memoryStream.Read(array, 0, array.Length) != array.Length)
													{
														num = 8;
														continue;
													}
													fileStream.Write(array, 0, array.Length);
													num = 3;
													continue;
												case 5:
													if (memoryStream.Read(array, 0, array.Length) != array.Length)
													{
														num = 12;
														continue;
													}
													fileStream.Write(array, 0, array.Length);
													num = 16;
													continue;
												case 6:
													goto IL_67C;
												case 7:
													fileStream.Seek((long)(this.\u1712[num2 - 1] as sprᾐ).ᜀ(), SeekOrigin.Begin);
													array = new byte[fileStream.Length - fileStream.Position];
													num = 21;
													continue;
												case 8:
													goto IL_43D;
												case 9:
												{
													sprᾐ sprᾐ = this.\u1712[num2 - 1] as sprᾐ;
													sprᾐ.ᜀ(sprᾐ.ᜀ() + this.ᜀ(stringListCollection));
													num = 10;
													continue;
												}
												case 10:
													goto IL_57E;
												case 11:
													memoryStream.SetLength(0L);
													num = 13;
													continue;
												case 12:
													goto IL_482;
												case 13:
													if (this.\u1712[num2 - 1] != null)
													{
														num = 7;
														continue;
													}
													goto IL_822;
												case 14:
													memoryStream.SetLength(0L);
													num = 0;
													continue;
												case 15:
													goto IL_509;
												case 16:
													if (this.ᜅ.NavigationAlign == NavigationAlign.Bottom)
													{
														num = 9;
														continue;
													}
													goto IL_57E;
												case 17:
													goto IL_82E;
												case 18:
													try
													{
														num = 2;
														for (;;)
														{
															switch (num)
															{
															case 0:
															{
																if (!enumerator.MoveNext())
																{
																	num = 4;
																	continue;
																}
																string str = (string)enumerator.Current;
																array = base.CurrentEncoding.GetBytes(str + HyperlinksCollectionEditor.b("☪✬", a_));
																fileStream.Write(array, 0, array.Length);
																num = 1;
																continue;
															}
															case 3:
																goto IL_312;
															case 4:
																num = 3;
																continue;
															}
															IL_2E9:
															num = 0;
															continue;
															goto IL_2E9;
														}
														IL_312:
														goto IL_442;
													}
													finally
													{
														for (;;)
														{
															IDisposable disposable = enumerator as IDisposable;
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
																	goto IL_35F;
																case 2:
																	goto IL_35D;
																}
																break;
															}
														}
														IL_35D:
														IL_35F:;
													}
													goto IL_360;
													IL_442:
													memoryStream.Position = 0L;
													array = new byte[memoryStream.Length];
													num = 5;
													continue;
												case 19:
													try
													{
														num = 4;
														for (;;)
														{
															IL_726:
															switch (num)
															{
															case 0:
																num = 2;
																continue;
															case 2:
																goto IL_7D4;
															case 3:
															{
																IEnumerator enumerator2;
																while (enumerator2.MoveNext())
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
																		string str2 = (string)enumerator2.Current;
																		array = base.CurrentEncoding.GetBytes(str2 + HyperlinksCollectionEditor.b("☪✬", a_));
																		fileStream.Write(array, 0, array.Length);
																		num = 1;
																		goto IL_726;
																	}
																	}
																}
																num = 0;
																continue;
															}
															}
															IL_743:
															num = 3;
															continue;
															goto IL_743;
														}
														IL_7D4:
														goto IL_3FD;
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
																	goto IL_821;
																case 2:
																	goto IL_81F;
																}
																break;
															}
														}
														IL_81F:
														IL_821:;
													}
													goto IL_822;
													IL_3FD:
													memoryStream.Position = 0L;
													array = new byte[memoryStream.Length];
													num = 4;
													continue;
												case 20:
													if (this.ᜅ.NavigationAlign == NavigationAlign.Bottom)
													{
														num = 11;
														continue;
													}
													goto IL_822;
												case 21:
												{
													if (fileStream.Read(array, 0, array.Length) != array.Length)
													{
														num = 15;
														continue;
													}
													memoryStream.Write(array, 0, array.Length);
													fileStream.Seek((long)(this.\u1712[num2 - 1] as sprᾐ).ᜀ(), SeekOrigin.Begin);
													this.ᜀ().ᜀ(this.ᜅ.PageTitle, this.ᜅ.FirstDisplayCaption, this.ᜅ.PriorDisplayCaption, this.ᜅ.NextDisplayCaption, this.ᜅ.LastDisplayCaption, this.FileName, this.ᜌ, num2, false, this.ᜃ, stringListCollection);
													IEnumerator enumerator2 = stringListCollection.GetEnumerator();
													num = 19;
													continue;
												}
												case 22:
													fileStream.Seek((long)(this.\u1712[num2 - 1] as sprᾐ).ᜁ(), SeekOrigin.Begin);
													array = new byte[fileStream.Length - fileStream.Position];
													num = 2;
													continue;
												}
												if (this.ᜅ.NavigationAlign == NavigationAlign.Top)
												{
													num = 14;
													continue;
												}
												goto IL_57E;
												IL_360:
												memoryStream.Write(array, 0, array.Length);
												fileStream.Seek((long)(this.\u1712[num2 - 1] as sprᾐ).ᜁ(), SeekOrigin.Begin);
												this.ᜀ().ᜀ(this.ᜅ.PageTitle, this.ᜅ.FirstDisplayCaption, this.ᜅ.PriorDisplayCaption, this.ᜅ.NextDisplayCaption, this.ᜅ.LastDisplayCaption, this.FileName, this.ᜌ, num2, true, this.ᜃ, stringListCollection);
												enumerator = stringListCollection.GetEnumerator();
												num = 18;
												continue;
												IL_57E:
												num = 20;
												continue;
												IL_822:
												num = 17;
											}
											IL_43D:
											throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("截䌬央倰弲尴匶瘸䬺堼䴾⁀㝂ⱄ⡆❈ᑊὌ⩎ぐ㝒㱔㥖㹘࡚⥜ⵞѠɢࡤ", a_)));
											IL_482:
											throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("截䌬央倰弲尴匶瘸䬺堼䴾⁀㝂ⱄ⡆❈ᑊὌ⩎ぐ㝒㱔㥖㹘࡚⥜ⵞѠɢࡤ", a_)));
											IL_509:
											throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("截䌬央倰弲尴匶瘸䬺堼䴾⁀㝂ⱄ⡆❈ᑊὌ⩎ぐ㝒㱔㥖㹘࡚⥜ⵞѠɢࡤ", a_)));
											IL_67C:
											throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("截䌬央倰弲尴匶瘸䬺堼䴾⁀㝂ⱄ⡆❈ᑊὌ⩎ぐ㝒㱔㥖㹘࡚⥜ⵞѠɢࡤ", a_)));
											IL_82E:;
										}
										finally
										{
											this.ᜀ().ᜋ().Close();
											this.ᜀ().\u170D().Close();
										}
										num2++;
										num = 3;
										continue;
									case 3:
										goto IL_863;
									case 4:
										goto IL_863;
									case 5:
										goto IL_895;
									}
									break;
									IL_863:
									num = 1;
								}
							}
							IL_895:
							goto IL_9BB;
						}
						finally
						{
							memoryStream.Close();
						}
						goto IL_8A1;
					case 11:
						if (this.ᜅ.NavigationAlign != NavigationAlign.None)
						{
							num = 0;
							continue;
						}
						goto IL_9BB;
					case 12:
						if (this.ᜌ > 1)
						{
							num = 1;
							continue;
						}
						goto IL_9BB;
					case 13:
						this.ᜀ().\u170D().Close();
						num = 14;
						continue;
					case 14:
						goto IL_948;
					case 15:
						num = 8;
						continue;
					case 16:
						goto IL_8A1;
					case 17:
						if (this.ᜀ().\u170D() != null)
						{
							num = 13;
							continue;
						}
						goto IL_948;
					}
					if (this.ᜄ > 0)
					{
						num = 15;
						continue;
					}
					break;
					IL_8A1:
					num = 6;
					continue;
					IL_8D3:
					num = 17;
					continue;
					IL_948:
					array = null;
					memoryStream = new MemoryStream();
					num = 10;
					continue;
					IL_98E:
					num = 11;
				}
				IL_9BB:
				base.EndDataExport();
				return;
			}
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0006AFF8 File Offset: 0x00069FF8
		protected override string GetShowedFileName()
		{
			string text;
			for (;;)
			{
				text = base.GetShowedFileName();
				string text2 = spr\u2059.ᜀ(text, 1, 2);
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_75;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							text = text2;
							num = 4;
							continue;
						}
						break;
					case 1:
						num = 2;
						continue;
					case 2:
						if (!this.ᜃ)
						{
							num = 6;
							continue;
						}
						return text;
					case 3:
						if (this.ᜄ > 0)
						{
							num = 1;
							continue;
						}
						return text;
					case 4:
						return text;
					case 5:
						if (File.Exists(text2))
						{
							goto IL_75;
						}
						return text;
					case 6:
						num = 5;
						continue;
					}
					break;
					IL_75:
					num = 0;
				}
			}
			return text;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0006B0CC File Offset: 0x0006A0CC
		protected override string GetPrintedFileName()
		{
			string text;
			for (;;)
			{
				text = base.GetShowedFileName();
				string text2 = spr\u2059.ᜀ(text, 1, 2);
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 4;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_75;
						default:
							if (false)
							{
							}
							text = text2;
							num = 3;
							continue;
						}
						break;
					case 2:
						num = 5;
						continue;
					case 3:
						goto IL_63;
					case 4:
						if (File.Exists(text2))
						{
							goto IL_75;
						}
						goto IL_BB;
					case 5:
						if (!this.ᜃ)
						{
							num = 0;
							continue;
						}
						goto IL_BB;
					case 6:
						if (this.ᜄ > 0)
						{
							num = 2;
							continue;
						}
						goto IL_BB;
					}
					break;
					IL_75:
					num = 1;
				}
			}
			IL_63:
			IL_BB:
			if (true)
			{
			}
			return text;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0006B1A0 File Offset: 0x0006A1A0
		public override void Stop()
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

		// Token: 0x06000A3A RID: 2618 RVA: 0x0006B1DC File Offset: 0x0006A1DC
		internal override string NormalString(string S)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				StringBuilder stringBuilder;
				for (;;)
				{
					stringBuilder = new StringBuilder((int)((double)S.Length * 1.25));
					int num = 0;
					int num2 = 18;
					for (;;)
					{
						char c;
						switch (num2)
						{
						case 0:
							if (this.InterpretTags)
							{
								num2 = 30;
								continue;
							}
							stringBuilder.Append(c);
							num2 = 3;
							continue;
						case 1:
							stringBuilder.Append(HyperlinksCollectionEditor.b("༨娪堬䀮䔰࠲", a_));
							num2 = 20;
							continue;
						case 2:
						{
							char c2;
							if (c2 != '\n')
							{
								num2 = 12;
								continue;
							}
							stringBuilder.Append(HyperlinksCollectionEditor.b("ᔨ䤪弬ᄮ", a_));
							num2 = 35;
							continue;
						}
						case 3:
							goto IL_1E5;
						case 4:
							if (true)
							{
							}
							num2 = 27;
							continue;
						case 5:
							num2 = 2;
							continue;
						case 6:
						{
							if (num >= S.Length)
							{
								num2 = 19;
								continue;
							}
							c = S[num];
							char c2 = c;
							num2 = 10;
							continue;
						}
						case 7:
							num2 = 15;
							continue;
						case 8:
							goto IL_1E5;
						case 9:
							goto IL_179;
						case 10:
						{
							char c2;
							if (c2 <= '\r')
							{
								num2 = 5;
								continue;
							}
							num2 = 11;
							continue;
						}
						case 11:
						{
							char c2;
							switch (c2)
							{
							case ' ':
								stringBuilder.Append(HyperlinksCollectionEditor.b("༨䔪伬尮䄰࠲", a_));
								num2 = 22;
								continue;
							case '!':
								goto IL_34F;
							case '"':
								num2 = 24;
								continue;
							default:
								num2 = 9;
								continue;
							}
							break;
						}
						case 12:
							num2 = 21;
							continue;
						case 13:
							goto IL_1E5;
						case 14:
							goto IL_1E5;
						case 15:
						{
							char c2;
							switch (c2)
							{
							case '<':
								num2 = 36;
								continue;
							case '=':
								goto IL_34F;
							case '>':
								num2 = 33;
								continue;
							default:
								num2 = 26;
								continue;
							}
							break;
						}
						case 16:
							stringBuilder.Append(HyperlinksCollectionEditor.b("༨䜪夬ᐮ", a_));
							num2 = 31;
							continue;
						case 17:
							goto IL_273;
						case 18:
							goto IL_273;
						case 19:
							goto IL_297;
						case 20:
							goto IL_1E5;
						case 21:
						{
							char c2;
							if (c2 != '\r')
							{
								num2 = 4;
								continue;
							}
							goto IL_1E5;
						}
						case 22:
							goto IL_1E5;
						case 23:
							goto IL_1E5;
						case 24:
							if (this.InterpretTags)
							{
								num2 = 1;
								continue;
							}
							stringBuilder.Append(c);
							num2 = 8;
							continue;
						case 25:
							goto IL_1E5;
						case 26:
							num2 = 34;
							continue;
						case 27:
							goto IL_34F;
						case 28:
							stringBuilder.Append(HyperlinksCollectionEditor.b("༨䰪夬ᐮ", a_));
							num2 = 25;
							continue;
						case 29:
						{
							char c2;
							if (c2 != '&')
							{
								num2 = 7;
								continue;
							}
							num2 = 0;
							continue;
						}
						case 30:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_179;
							default:
								if (false)
								{
								}
								stringBuilder.Append(HyperlinksCollectionEditor.b("༨䨪䀬弮ਰ", a_));
								num2 = 14;
								continue;
							}
							break;
						case 31:
							goto IL_1E5;
						case 32:
							goto IL_1E5;
						case 33:
							if (this.InterpretTags)
							{
								num2 = 28;
								continue;
							}
							stringBuilder.Append(c);
							num2 = 32;
							continue;
						case 34:
							goto IL_34F;
						case 35:
							goto IL_1E5;
						case 36:
							if (this.InterpretTags)
							{
								num2 = 16;
								continue;
							}
							stringBuilder.Append(c);
							num2 = 23;
							continue;
						}
						break;
						IL_179:
						num2 = 29;
						continue;
						IL_1E5:
						num++;
						num2 = 17;
						continue;
						IL_273:
						num2 = 6;
						continue;
						IL_34F:
						stringBuilder.Append(c);
						num2 = 13;
					}
				}
				IL_297:
				return stringBuilder.ToString();
			}
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0006B65C File Offset: 0x0006A65C
		protected override void ExportToFile()
		{
			FileStream fileStream = this.ᜑ = new FileStream(this.FileName, FileMode.Create);
			try
			{
				StreamWriter streamWriter = new StreamWriter(this.ᜑ, base.CurrentEncoding);
				try
				{
					this.SaveToMemoryStream(this.ᜑ, streamWriter);
				}
				finally
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							((IDisposable)streamWriter).Dispose();
							num = 2;
							continue;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								goto IL_92;
							}
							break;
						}
						if (streamWriter == null)
						{
							goto IL_9A;
						}
						num = 0;
					}
					IL_92:
					if (false)
					{
					}
					IL_9A:;
				}
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						((IDisposable)fileStream).Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_D4;
					}
					if (fileStream == null)
					{
						goto IL_DE;
					}
					num = 0;
				}
				IL_D4:
				if (true)
				{
				}
				IL_DE:;
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0006B764 File Offset: 0x0006A764
		public void SaveTemplateToFile(string FileName)
		{
			int a_ = 13;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			XMLFile xmlfile = new XMLFile(FileName);
			xmlfile.WriteValue(HyperlinksCollectionEditor.b("愨缪怬挮ᄰ尲䔴䌶倸吺匼䰾", a_), HyperlinksCollectionEditor.b("欨䨪丬䐮嘰䄲娴䈶圸强ြ尾⹀⽂⩄㕆", a_), this.ᜇ.BackgroundColor.ToArgb().ToString(HyperlinksCollectionEditor.b("焨", a_)));
			xmlfile.WriteValue(HyperlinksCollectionEditor.b("愨缪怬挮ᄰ尲䔴䌶倸吺匼䰾", a_), HyperlinksCollectionEditor.b("漨䐪䌬嬮ᰰ倲娴嬶嘸䤺", a_), this.ᜇ.FontColor.ToArgb().ToString(HyperlinksCollectionEditor.b("焨", a_)));
			xmlfile.WriteValue(HyperlinksCollectionEditor.b("愨缪怬挮ᄰ尲䔴䌶倸吺匼䰾", a_), HyperlinksCollectionEditor.b("漨䐪䌬嬮ᰰ唲吴娶倸场䐼", a_), this.ᜇ.Font.Name);
			xmlfile.WriteValue(HyperlinksCollectionEditor.b("愨缪怬挮ᄰ尲䔴䌶倸吺匼䰾", a_), HyperlinksCollectionEditor.b("攨䈪䌬䐮爰尲头堶䬸", a_), this.ᜇ.LinkColor.ToArgb().ToString(HyperlinksCollectionEditor.b("焨", a_)));
			xmlfile.WriteValue(HyperlinksCollectionEditor.b("愨缪怬挮ᄰ尲䔴䌶倸吺匼䰾", a_), HyperlinksCollectionEditor.b("缨未䐬䄮娰瀲娴嬶嘸䤺", a_), this.ᜇ.LinkVisitedColor.ToArgb().ToString(HyperlinksCollectionEditor.b("焨", a_)));
			xmlfile.WriteValue(HyperlinksCollectionEditor.b("愨缪怬挮ᄰ尲䔴䌶倸吺匼䰾", a_), HyperlinksCollectionEditor.b("栨未䐬䄮娰瀲娴嬶嘸䤺", a_), this.ᜇ.LinkActiveColor.ToArgb().ToString(HyperlinksCollectionEditor.b("焨", a_)));
			xmlfile.WriteValue(HyperlinksCollectionEditor.b("紨䨪伬䌮吰ጲ娴䜶䴸刺刼儾㉀", a_), HyperlinksCollectionEditor.b("愨个䰬䬮吰䄲ᠴ唶常堺刼匾⹀ㅂ", a_), this.ᜉ.HeadersBackColor.ToArgb().ToString(HyperlinksCollectionEditor.b("焨", a_)));
			xmlfile.WriteValue(HyperlinksCollectionEditor.b("紨䨪伬䌮吰ጲ娴䜶䴸刺刼儾㉀", a_), HyperlinksCollectionEditor.b("愨个䰬䬮吰䄲ᠴ吶嘸场刼䴾", a_), this.ᜉ.HeadersFontColor.ToArgb().ToString(HyperlinksCollectionEditor.b("焨", a_)));
			xmlfile.WriteValue(HyperlinksCollectionEditor.b("紨䨪伬䌮吰ጲ娴䜶䴸刺刼儾㉀", a_), HyperlinksCollectionEditor.b("紨䨪伬䌮吰Ḳ嘴堶唸吺似", a_), this.ᜉ.FontColor.ToArgb().ToString(HyperlinksCollectionEditor.b("焨", a_)));
			xmlfile.WriteValue(HyperlinksCollectionEditor.b("紨䨪伬䌮吰ጲ娴䜶䴸刺刼儾㉀", a_), HyperlinksCollectionEditor.b("氨崪䠬䄮渰䄲娴䀶ᐸ夺娼尾⹀⽂⩄㕆", a_), this.ᜉ.BackColor.ToArgb().ToString(HyperlinksCollectionEditor.b("焨", a_)));
			xmlfile.WriteValue(HyperlinksCollectionEditor.b("紨䨪伬䌮吰ጲ娴䜶䴸刺刼儾㉀", a_), HyperlinksCollectionEditor.b("昨伪䤬瀮䌰尲䈴ᨶ嬸尺帼倾ⵀⱂ㝄", a_), this.ᜉ.OddBackColor.ToArgb().ToString(HyperlinksCollectionEditor.b("焨", a_)));
			xmlfile.WriteValue(HyperlinksCollectionEditor.b("紨䨪伬䌮吰ጲ娴䜶䴸刺刼儾㉀", a_), HyperlinksCollectionEditor.b("欨䐪弬䬮吰䄲ᠴ吶嘸场刼䴾", a_), this.ᜉ.BorderColor.ToArgb().ToString(HyperlinksCollectionEditor.b("焨", a_)));
			xmlfile.SaveToFile(FileName);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0006BB38 File Offset: 0x0006AB38
		public void LoadTemplateFromFile(string FileName)
		{
			int a_ = 2;
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				XMLFile xmlfile;
				string familyName;
				for (;;)
				{
					IL_34:
					xmlfile = new XMLFile(FileName);
					this.ᜇ.BackgroundColor = Color.FromArgb(Convert.ToInt32(xmlfile.ReadValue(HyperlinksCollectionEditor.b("嘝琟漡栣إ䜧娩堫䜭弯就䜳", a_), HyperlinksCollectionEditor.b("尝䄟䄡伣䄥娧䔩夫䀭启ἱ圳夵吷唹主", a_), this.ᜇ.BackgroundColor.ToArgb().ToString(HyperlinksCollectionEditor.b("䘝", a_))), 16));
					this.ᜇ.FontColor = Color.FromArgb(Convert.ToInt32(xmlfile.ReadValue(HyperlinksCollectionEditor.b("嘝琟漡栣إ䜧娩堫䜭弯就䜳", a_), HyperlinksCollectionEditor.b("堝伟䰡倣ଥ䬧䔩䀫䄭䈯", a_), this.ᜇ.FontColor.ToArgb().ToString(HyperlinksCollectionEditor.b("䘝", a_))), 16));
					familyName = xmlfile.ReadValue(HyperlinksCollectionEditor.b("嘝琟漡栣إ䜧娩堫䜭弯就䜳", a_), HyperlinksCollectionEditor.b("堝伟䰡倣ଥ丧䬩䄫䜭尯䬱", a_), this.ᜇ.Font.Name);
					for (;;)
					{
						IL_134:
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
									goto IL_134;
								default:
									if (false)
									{
									}
									this.ᜇ.Font.Dispose();
									num = 2;
									continue;
								}
								break;
							case 1:
								if (this.ᜇ.Font != null)
								{
									num = 0;
									continue;
								}
								goto IL_19F;
							case 2:
								goto IL_19D;
							}
							goto IL_34;
						}
					}
				}
				IL_19D:
				IL_19F:
				this.ᜇ.Font = new Font(familyName, 8f);
				this.ᜇ.LinkColor = Color.FromArgb(Convert.ToInt32(xmlfile.ReadValue(HyperlinksCollectionEditor.b("嘝琟漡栣إ䜧娩堫䜭弯就䜳", a_), HyperlinksCollectionEditor.b("初䤟䰡伣攥䜧䘩䌫尭", a_), this.ᜇ.LinkColor.ToArgb().ToString(HyperlinksCollectionEditor.b("䘝", a_))), 16));
				this.ᜇ.LinkVisitedColor = Color.FromArgb(Convert.ToInt32(xmlfile.ReadValue(HyperlinksCollectionEditor.b("嘝琟漡栣إ䜧娩堫䜭弯就䜳", a_), HyperlinksCollectionEditor.b("䠝氟䬡䨣䴥欧䔩䀫䄭䈯", a_), this.ᜇ.LinkVisitedColor.ToArgb().ToString(HyperlinksCollectionEditor.b("䘝", a_))), 16));
				this.ᜇ.LinkActiveColor = Color.FromArgb(Convert.ToInt32(xmlfile.ReadValue(HyperlinksCollectionEditor.b("嘝琟漡栣إ䜧娩堫䜭弯就䜳", a_), HyperlinksCollectionEditor.b("弝氟䬡䨣䴥欧䔩䀫䄭䈯", a_), this.ᜇ.LinkActiveColor.ToArgb().ToString(HyperlinksCollectionEditor.b("䘝", a_))), 16));
				this.ᜉ.HeadersBackColor = Color.FromArgb(Convert.ToInt32(xmlfile.ReadValue(HyperlinksCollectionEditor.b("䨝䄟䀡䠣䌥ࠧ䔩尫娭夯崱娳䔵", a_), HyperlinksCollectionEditor.b("嘝䔟䌡䀣䌥娧ܩ丫䤭匯崱堳夵䨷", a_), this.ᜉ.HeadersBackColor.ToArgb().ToString(HyperlinksCollectionEditor.b("䘝", a_))), 16));
				this.ᜉ.HeadersFontColor = Color.FromArgb(Convert.ToInt32(xmlfile.ReadValue(HyperlinksCollectionEditor.b("䨝䄟䀡䠣䌥ࠧ䔩尫娭夯崱娳䔵", a_), HyperlinksCollectionEditor.b("嘝䔟䌡䀣䌥娧ܩ伫䄭尯崱䘳", a_), this.ᜉ.HeadersFontColor.ToArgb().ToString(HyperlinksCollectionEditor.b("䘝", a_))), 16));
				this.ᜉ.FontColor = Color.FromArgb(Convert.ToInt32(xmlfile.ReadValue(HyperlinksCollectionEditor.b("䨝䄟䀡䠣䌥ࠧ䔩尫娭夯崱娳䔵", a_), HyperlinksCollectionEditor.b("䨝䄟䀡䠣䌥ԧ䤩䌫䈭弯䀱", a_), this.ᜉ.FontColor.ToArgb().ToString(HyperlinksCollectionEditor.b("䘝", a_))), 16));
				this.ᜉ.BackColor = Color.FromArgb(Convert.ToInt32(xmlfile.ReadValue(HyperlinksCollectionEditor.b("䨝䄟䀡䠣䌥ࠧ䔩尫娭夯崱娳䔵", a_), HyperlinksCollectionEditor.b("嬝嘟䜡䨣礥娧䔩嬫̭刯唱圳夵吷唹主", a_), this.ᜉ.BackColor.ToArgb().ToString(HyperlinksCollectionEditor.b("䘝", a_))), 16));
				this.ᜉ.OddBackColor = Color.FromArgb(Convert.ToInt32(xmlfile.ReadValue(HyperlinksCollectionEditor.b("䨝䄟䀡䠣䌥ࠧ䔩尫娭夯崱娳䔵", a_), HyperlinksCollectionEditor.b("儝䐟䘡笣吥䜧崩ī䰭圯儱嬳娵圷䠹", a_), this.ᜉ.OddBackColor.ToArgb().ToString(HyperlinksCollectionEditor.b("䘝", a_))), 16));
				this.ᜉ.BorderColor = Color.FromArgb(Convert.ToInt32(xmlfile.ReadValue(HyperlinksCollectionEditor.b("䨝䄟䀡䠣䌥ࠧ䔩尫娭夯崱娳䔵", a_), HyperlinksCollectionEditor.b("尝伟倡䀣䌥娧ܩ伫䄭尯崱䘳", a_), this.ᜉ.BorderColor.ToArgb().ToString(HyperlinksCollectionEditor.b("䘝", a_))), 16));
				return;
			}
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0006C080 File Offset: 0x0006B080
		// (set) Token: 0x06000A3F RID: 2623 RVA: 0x0006C0C4 File Offset: 0x0006B0C4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(UsingCSS.Internal)]
		[Description("Gets or sets whether use CSS file.")]
		public UsingCSS UsingCSS
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
					if (true)
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
						return;
					case 2:
						this.ᜊ = value;
						num = 0;
						continue;
					}
					if (value == this.ᜊ)
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x0006C140 File Offset: 0x0006B140
		// (set) Token: 0x06000A41 RID: 2625 RVA: 0x0006C184 File Offset: 0x0006B184
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("")]
		[Description("Gets or sets css file name.")]
		public string CSSFileName
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
						this.ᜆ = value;
						num = 2;
						continue;
					case 2:
						return;
					}
					if (true)
					{
					}
					if (!(value != this.ᜆ))
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x0006C204 File Offset: 0x0006B204
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x0006C248 File Offset: 0x0006B248
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Indicates whether override css file if the file already exists.")]
		[DefaultValue(true)]
		public bool OverwriteCSSFile
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
				return this.ᜎ;
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
						this.ᜎ = value;
						num = 2;
						continue;
					case 2:
						return;
					}
					if (true)
					{
					}
					if (value == this.ᜎ)
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0006C2C4 File Offset: 0x0006B2C4
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x0006C308 File Offset: 0x0006B308
		[Description("Gets or sets table element options of html page in result html file.")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public HtmlTableOptions HtmlTableOptions
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
				return this.ᜉ;
			}
			set
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜉ = value;
						num = 2;
						continue;
					case 1:
						if (value == this.ᜉ)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 2:
						return;
					case 4:
						if (true)
						{
						}
						num = 1;
						continue;
					}
					if (value == null)
					{
						break;
					}
					num = 4;
				}
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0006C3A0 File Offset: 0x0006B3A0
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x0006C3E4 File Offset: 0x0006B3E4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets or sets text element options of html document in result html file.")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public HtmlTextOptions HtmlTextOptions
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
				int num = 3;
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
						if (value == this.ᜇ)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						this.ᜇ = value;
						num = 0;
						continue;
					case 4:
						num = 1;
						continue;
					}
					if (value == null)
					{
						break;
					}
					num = 4;
				}
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0006C47C File Offset: 0x0006B47C
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x0006C4C0 File Offset: 0x0006B4C0
		[Description("Gets or sets template style of html document.")]
		[DefaultValue(HtmlStyle.DOS)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public HtmlStyle HtmlStyle
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
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.ᜈ = value;
						num = 4;
						continue;
					case 2:
						goto IL_D5;
					case 3:
						return;
					case 4:
						switch (value)
						{
						case HtmlStyle.Desert:
							goto IL_1A0;
						case HtmlStyle.Silver:
							goto IL_6D;
						case HtmlStyle.DOS:
							goto IL_55;
						case HtmlStyle.Yellow:
							goto IL_A7;
						case HtmlStyle.Gray:
							goto IL_17B;
						case HtmlStyle.MSMoney:
							goto IL_DA;
						case HtmlStyle.Murky:
							goto IL_188;
						case HtmlStyle.Olive:
							goto IL_1AC;
						case HtmlStyle.Plain:
							goto IL_91;
						case HtmlStyle.Brick:
							goto IL_79;
						case HtmlStyle.Eggplant:
							return;
						case HtmlStyle.Lilac:
							goto IL_3D;
						case HtmlStyle.Maple:
							goto IL_194;
						case HtmlStyle.Marine:
							goto IL_61;
						case HtmlStyle.Rose:
							goto IL_B3;
						case HtmlStyle.Green:
							goto IL_85;
						case HtmlStyle.Wheat:
							this.ᜀ(HtmlExportStyles.Wheat);
							num = 2;
							continue;
						case HtmlStyle.Normal:
							goto IL_49;
						default:
							num = 3;
							continue;
						}
						break;
					}
					if (this.ᜈ == value)
					{
						return;
					}
					num = 1;
				}
				IL_3D:
				this.ᜀ(HtmlExportStyles.Lilac);
				return;
				IL_49:
				this.ᜀ(HtmlExportStyles.Normal);
				return;
				IL_55:
				this.ᜀ(HtmlExportStyles.DOS);
				return;
				IL_61:
				this.ᜀ(HtmlExportStyles.Marine);
				return;
				IL_6D:
				this.ᜀ(HtmlExportStyles.Silver);
				return;
				IL_79:
				this.ᜀ(HtmlExportStyles.Brick);
				return;
				IL_85:
				this.ᜀ(HtmlExportStyles.Green);
				return;
				IL_91:
				this.ᜀ(HtmlExportStyles.Plain);
				return;
				IL_A7:
				this.ᜀ(HtmlExportStyles.Yellow);
				return;
				IL_B3:
				this.ᜀ(HtmlExportStyles.Rose);
				return;
				IL_D5:
				return;
				IL_DA:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_194:
					this.ᜀ(HtmlExportStyles.Maple);
					return;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					this.ᜀ(HtmlExportStyles.MSMoney);
					return;
				}
				return;
				IL_17B:
				this.ᜀ(HtmlExportStyles.Gray);
				return;
				IL_188:
				this.ᜀ(HtmlExportStyles.Murky);
				return;
				IL_1A0:
				this.ᜀ(HtmlExportStyles.Desert);
				return;
				IL_1AC:
				this.ᜀ(HtmlExportStyles.Olive);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0006C688 File Offset: 0x0006B688
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x0006C6CC File Offset: 0x0006B6CC
		[Description("Gets or sets maximum number of result records in the result html file.")]
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int PageRecords
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
							if (value < 0)
							{
								goto IL_8A;
							}
							this.ᜄ = value;
							num = 4;
							continue;
						case 2:
							if (true)
							{
							}
							num = 0;
							continue;
						case 3:
							goto IL_95;
						case 4:
							goto IL_74;
						}
						if (this.ᜄ != value)
						{
							num = 2;
							continue;
						}
						return;
					}
					IL_8A:
					num = 3;
				}
				IL_74:
				return;
				IL_95:
				this.ᜄ = 0;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0006C770 File Offset: 0x0006B770
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x0006C7B4 File Offset: 0x0006B7B4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Indicates whether create the Html index file automatically.")]
		[DefaultValue(false)]
		public bool GenerateIndex
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
					if (true)
					{
					}
					switch (num)
					{
					case 0:
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

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x0006C830 File Offset: 0x0006B830
		// (set) Token: 0x06000A4F RID: 2639 RVA: 0x0006C874 File Offset: 0x0006B874
		[Description("Gets or sets the options of navigation links.")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public HtmlExportIndexOption HtmlIndexOption
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
				return this.ᜅ;
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
						if (true)
						{
						}
						this.ᜅ = value;
						num = 0;
						continue;
					case 3:
						num = 4;
						continue;
					case 4:
						if (value == this.ᜅ)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					if (value == null)
					{
						break;
					}
					num = 3;
				}
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0006C90C File Offset: 0x0006B90C
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x0006C950 File Offset: 0x0006B950
		[DefaultValue(false)]
		[Description("Indicates whether boolean columns are exported as checkboxs.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool BoolAsCheckBox
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
						this.ᜂ = value;
						num = 1;
						continue;
					case 1:
						goto IL_5A;
					}
					if (value == this.ᜂ)
					{
						return;
					}
					num = 0;
				}
				IL_5A:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0006C9CC File Offset: 0x0006B9CC
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x0006CA10 File Offset: 0x0006BA10
		[Description("Indicates whether all special symbols &lt; &gt; &quot; &amp; found in exported data (text) will be replaced with corresponding &lt; &gt; &quot; &amp; ones.")]
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool InterpretTags
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
				int num;
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
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜏ = value;
						num = 2;
						continue;
					case 2:
						return;
					}
					if (value == this.ᜏ)
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0006CA8C File Offset: 0x0006BA8C
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x0006CAD0 File Offset: 0x0006BAD0
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new string Title
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
				return base.Title;
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
				base.Title = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x0006CB14 File Offset: 0x0006BB14
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x0006CB58 File Offset: 0x0006BB58
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
				if (true)
				{
				}
				if (false)
				{
				}
				base.ColumnsAlign = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0006CB9C File Offset: 0x0006BB9C
		// (set) Token: 0x06000A59 RID: 2649 RVA: 0x0006CBE0 File Offset: 0x0006BBE0
		[Description("Allows you to select string fields that will not be truncated by occurrences of carriage returns.")]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0006CC24 File Offset: 0x0006BC24
		// (set) Token: 0x06000A5B RID: 2651 RVA: 0x0006CC68 File Offset: 0x0006BC68
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("")]
		[Browsable(true)]
		[Editor(typeof(HtmlFileNameEditor), typeof(UITypeEditor))]
		public new string FileName
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

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0006CCAC File Offset: 0x0006BCAC
		// (set) Token: 0x06000A5D RID: 2653 RVA: 0x0006CCF0 File Offset: 0x0006BCF0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(EncodingType.UTF8)]
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
				if (false)
				{
				}
				if (true)
				{
				}
				base.DataEncoding = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0006CD34 File Offset: 0x0006BD34
		// (set) Token: 0x06000A5F RID: 2655 RVA: 0x0006CD78 File Offset: 0x0006BD78
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(false)]
		[Browsable(true)]
		[Description("Indicate whether export long char/binary column.")]
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
				if (false)
				{
				}
				if (true)
				{
				}
				base.ExportLongColumn = value;
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000A60 RID: 2656 RVA: 0x0006CDBC File Offset: 0x0006BDBC
		// (remove) Token: 0x06000A61 RID: 2657 RVA: 0x0006CE00 File Offset: 0x0006BE00
		protected new event CellParamsEventHandler GetCellParams
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
				base.GetCellParams += value;
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
				base.GetCellParams -= value;
			}
		}

		// Token: 0x040007BD RID: 1981
		private string \u2460\u008B\u0081\u0084;

		// Token: 0x040007BE RID: 1982
		private new bool ᜀ;

		// Token: 0x040007BF RID: 1983
		private new License ᜁ;

		// Token: 0x040007C0 RID: 1984
		private new bool ᜂ;

		// Token: 0x040007C1 RID: 1985
		private new bool ᜃ;

		// Token: 0x040007C2 RID: 1986
		private new int ᜄ;

		// Token: 0x040007C3 RID: 1987
		private HtmlExportIndexOption ᜅ = new HtmlExportIndexOption();

		// Token: 0x040007C4 RID: 1988
		private string ᜆ = string.Empty;

		// Token: 0x040007C5 RID: 1989
		private HtmlTextOptions ᜇ = new HtmlTextOptions();

		// Token: 0x040007C6 RID: 1990
		private HtmlStyle ᜈ = HtmlStyle.DOS;

		// Token: 0x040007C7 RID: 1991
		private HtmlTableOptions ᜉ = new HtmlTableOptions();

		// Token: 0x040007C8 RID: 1992
		private UsingCSS ᜊ;

		// Token: 0x040007C9 RID: 1993
		private StringListCollection ᜋ = new StringListCollection();

		// Token: 0x040007CA RID: 1994
		private int ᜌ;

		// Token: 0x040007CB RID: 1995
		private bool \u170D;

		// Token: 0x040007CC RID: 1996
		private float[] \u2460\u00A2\u00A1\u00A8;

		// Token: 0x040007CD RID: 1997
		private bool ᜎ = true;

		// Token: 0x040007CE RID: 1998
		private bool ᜏ = true;

		// Token: 0x040007CF RID: 1999
		private bool ᜐ;

		// Token: 0x040007D0 RID: 2000
		private FileStream ᜑ;

		// Token: 0x040007D1 RID: 2001
		private ArrayList \u1712 = new ArrayList();
	}
}
