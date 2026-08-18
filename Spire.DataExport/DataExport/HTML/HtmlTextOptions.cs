using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.Utils;

namespace Spire.DataExport.HTML
{
	// Token: 0x0200017E RID: 382
	public class HtmlTextOptions : ICloneable
	{
		// Token: 0x06000A63 RID: 2659 RVA: 0x0006CEB0 File Offset: 0x0006BEB0
		public HtmlTextOptions()
		{
			int a_ = 14;
			this.ᜀ = Color.Empty;
			this.ᜁ = string.Empty;
			this.ᜂ = Color.Empty;
			this.ᜃ = Color.Empty;
			this.ᜄ = Color.Empty;
			this.ᜅ = new Font(HyperlinksCollectionEditor.b("欩師䜭儯帱", a_), 8f);
			this.ᜆ = Color.Black;
			this.ᜇ = new StringListCollection();
			this.ᜈ = DefaultOptions.FontSize;
			base..ctor();
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0006CF40 File Offset: 0x0006BF40
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
			return new HtmlTextOptions
			{
				BackgroundColor = this.BackgroundColor,
				BackImageUrl = this.BackImageUrl,
				LinkColor = this.LinkColor,
				LinkVisitedColor = this.LinkVisitedColor,
				LinkActiveColor = this.LinkActiveColor,
				Font = this.Font,
				AdvancedAttributes = this.AdvancedAttributes
			};
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0006CFD8 File Offset: 0x0006BFD8
		public void SaveToXmlFile(XMLFile File, string Section)
		{
			int a_ = 16;
			switch (0)
			{
			default:
			{
				StringBuilder stringBuilder;
				for (;;)
				{
					File.WriteValue(HyperlinksCollectionEditor.b("搫稭累縱", a_), HyperlinksCollectionEditor.b("渫伭匯失匳䐵圷伹刻娽̿ⵁ⡃⥅㩇", a_), this.ᜀ.ToArgb().ToString(HyperlinksCollectionEditor.b("琫", a_)));
					File.WriteValue(HyperlinksCollectionEditor.b("搫稭累縱", a_), HyperlinksCollectionEditor.b("渫伭匯失匳䐵圷伹刻娽ؿ⭁⡃⍅", a_), this.ᜁ);
					File.WriteValue(HyperlinksCollectionEditor.b("搫稭累縱", a_), HyperlinksCollectionEditor.b("稫焭猯崱堳夵䨷", a_), this.ᜂ.ToArgb().ToString(HyperlinksCollectionEditor.b("琫", a_)));
					File.WriteValue(HyperlinksCollectionEditor.b("搫稭累縱", a_), HyperlinksCollectionEditor.b("怫䜭帯失眳夵吷唹主", a_), this.ᜃ.ToArgb().ToString(HyperlinksCollectionEditor.b("琫", a_)));
					File.WriteValue(HyperlinksCollectionEditor.b("搫稭累縱", a_), HyperlinksCollectionEditor.b("洫焭猯崱堳夵䨷", a_), this.ᜄ.ToArgb().ToString(HyperlinksCollectionEditor.b("琫", a_)));
					File.WriteValue(HyperlinksCollectionEditor.b("搫稭累縱", a_), HyperlinksCollectionEditor.b("樫䄭帯䘱欳砵夷圹夻", a_), this.ᜅ.Name);
					File.WriteValue(HyperlinksCollectionEditor.b("搫稭累縱", a_), HyperlinksCollectionEditor.b("樫䄭帯䘱欳电圷嘹医䰽", a_), this.ᜆ.ToArgb().ToString(HyperlinksCollectionEditor.b("琫", a_)));
					stringBuilder = new StringBuilder(this.ᜇ.Count);
					IEnumerator enumerator = this.ᜇ.GetEnumerator();
					if (true)
					{
					}
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (stringBuilder.Length > 0)
							{
								num = 3;
								continue;
							}
							goto IL_34F;
						case 1:
							goto IL_34D;
						case 2:
							try
							{
								num = 4;
								for (;;)
								{
									switch (num)
									{
									case 0:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_2C9;
										default:
											if (false)
											{
											}
											break;
										}
										break;
									case 1:
										num = 2;
										continue;
									case 2:
										goto IL_2E3;
									case 3:
									{
										if (!enumerator.MoveNext())
										{
											goto IL_2C9;
										}
										string arg = (string)enumerator.Current;
										stringBuilder.AppendFormat(HyperlinksCollectionEditor.b("圫ḭ䴯䤱Գ䬵", a_), arg, ';');
										num = 0;
										continue;
									}
									}
									IL_2B7:
									num = 3;
									continue;
									goto IL_2B7;
									IL_2C9:
									num = 1;
								}
								IL_2E3:
								goto IL_215;
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
											goto IL_330;
										case 1:
											goto IL_32E;
										case 2:
											disposable.Dispose();
											num = 1;
											continue;
										}
										break;
									}
								}
								IL_32E:
								IL_330:;
							}
							goto IL_331;
							IL_215:
							num = 0;
							continue;
						case 3:
							goto IL_331;
						}
						break;
						IL_331:
						stringBuilder.Remove(stringBuilder.Length - 1, 1);
						num = 1;
					}
				}
				IL_34D:
				IL_34F:
				File.WriteValue(HyperlinksCollectionEditor.b("搫稭累縱", a_), HyperlinksCollectionEditor.b("渫䄭启䬱申刵丷嬹刻崽┿♁", a_), stringBuilder.ToString());
				File.SaveToFile();
				return;
			}
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0006D374 File Offset: 0x0006C374
		public void LoadFromXmlFile(XMLFile File, string Section)
		{
			int a_ = 11;
			switch (0)
			{
			default:
			{
				string text;
				for (;;)
				{
					string sectionName = HyperlinksCollectionEditor.b("漦紨昪愬", a_);
					string key = HyperlinksCollectionEditor.b("攦䠨䠪䘬䠮䌰尲䀴夶崸砺刼匾⹀ㅂ", a_);
					Color rbackgroundColor = HtmlExportStyles.DOS.RBackgroundColor;
					this.ᜀ = Color.FromArgb(Convert.ToInt32(File.ReadValue(sectionName, key, rbackgroundColor.ToArgb().ToString(HyperlinksCollectionEditor.b("缦", a_))), 16));
					this.ᜁ = File.ReadValue(HyperlinksCollectionEditor.b("漦紨昪愬", a_), HyperlinksCollectionEditor.b("攦䠨䠪䘬䠮䌰尲䀴夶崸紺吼匾⑀", a_), string.Empty);
					string sectionName2 = HyperlinksCollectionEditor.b("漦紨昪愬", a_);
					string key2 = HyperlinksCollectionEditor.b("焦瘨株䈬䌮帰䄲", a_);
					Color rvlinkColor = HtmlExportStyles.DOS.RVLinkColor;
					this.ᜂ = Color.FromArgb(Convert.ToInt32(File.ReadValue(sectionName2, key2, rvlinkColor.ToArgb().ToString(HyperlinksCollectionEditor.b("缦", a_))), 16));
					string sectionName3 = HyperlinksCollectionEditor.b("漦紨昪愬", a_);
					string key3 = HyperlinksCollectionEditor.b("欦䀨䔪䘬氮帰弲娴䔶", a_);
					Color rlinkColor = HtmlExportStyles.DOS.RLinkColor;
					this.ᜃ = Color.FromArgb(Convert.ToInt32(File.ReadValue(sectionName3, key3, rlinkColor.ToArgb().ToString(HyperlinksCollectionEditor.b("缦", a_))), 16));
					string sectionName4 = HyperlinksCollectionEditor.b("漦紨昪愬", a_);
					string key4 = HyperlinksCollectionEditor.b("昦瘨株䈬䌮帰䄲", a_);
					Color ralinkColor = HtmlExportStyles.DOS.RALinkColor;
					this.ᜄ = Color.FromArgb(Convert.ToInt32(File.ReadValue(sectionName4, key4, ralinkColor.ToArgb().ToString(HyperlinksCollectionEditor.b("缦", a_))), 16));
					string familyName = File.ReadValue(HyperlinksCollectionEditor.b("漦紨昪愬", a_), HyperlinksCollectionEditor.b("愦䘨䔪夬瀮缰刲場制", a_), HyperlinksCollectionEditor.b("昦嬨䈪䰬䌮", a_));
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (text.Length > 0)
							{
								num = 5;
								continue;
							}
							num = 2;
							continue;
						case 1:
							goto IL_2E1;
						case 2:
							if (this.ᜇ.Count <= 0)
							{
								return;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2DC;
							default:
								if (false)
								{
								}
								num = 7;
								continue;
							}
							break;
						case 3:
							if (this.ᜅ != null)
							{
								num = 6;
								continue;
							}
							goto IL_2E1;
						case 4:
							goto IL_2DC;
						case 5:
							goto IL_3A4;
						case 6:
							this.ᜅ.Dispose();
							num = 1;
							continue;
						case 7:
							this.ᜇ = new StringListCollection();
							num = 4;
							continue;
						}
						break;
						IL_2E1:
						if (true)
						{
						}
						this.ᜅ = new Font(familyName, 8f);
						string sectionName5 = HyperlinksCollectionEditor.b("漦紨昪愬", a_);
						string key5 = HyperlinksCollectionEditor.b("愦䘨䔪夬瀮爰尲头堶䬸", a_);
						Color rdefaultTextColor = HtmlExportStyles.DOS.RDefaultTextColor;
						this.ᜆ = Color.FromArgb(Convert.ToInt32(File.ReadValue(sectionName5, key5, rdefaultTextColor.ToArgb().ToString(HyperlinksCollectionEditor.b("缦", a_))), 16));
						text = File.ReadValue(HyperlinksCollectionEditor.b("漦紨昪愬", a_), HyperlinksCollectionEditor.b("攦䘨伪听渮唰䔲吴夶娸帺夼", a_), string.Empty);
						num = 0;
					}
				}
				IL_2DC:
				return;
				IL_3A4:
				this.ᜇ.SetStrings(text.Split(new char[]
				{
					';'
				}));
				return;
			}
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0006D72C File Offset: 0x0006C72C
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x0006D770 File Offset: 0x0006C770
		[DefaultValue(typeof(Color), "Empty")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Color BackgroundColor
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
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (true)
						{
						}
						goto IL_62;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_62;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					if (value != this.ᜀ)
					{
						num = 1;
						continue;
					}
					break;
					IL_62:
					this.ᜀ = value;
					num = 0;
				}
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0006D7F0 File Offset: 0x0006C7F0
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x0006D834 File Offset: 0x0006C834
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string BackImageUrl
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_62;
					case 1:
						return;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_62;
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
					}
					if (value != this.ᜁ)
					{
						num = 0;
						continue;
					}
					break;
					IL_62:
					this.ᜁ = value;
					num = 1;
				}
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x0006D8B4 File Offset: 0x0006C8B4
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x0006D8F8 File Offset: 0x0006C8F8
		[DefaultValue(typeof(Color), "Empty")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Color LinkColor
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
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_62;
						}
						if (true)
						{
						}
						if (false)
						{
						}
						break;
					case 1:
						return;
					case 2:
						goto IL_62;
					}
					if (value != this.ᜃ)
					{
						num = 2;
						continue;
					}
					break;
					IL_62:
					this.ᜃ = value;
					num = 1;
				}
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0006D978 File Offset: 0x0006C978
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x0006D9BC File Offset: 0x0006C9BC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(typeof(Color), "Empty")]
		public Color LinkVisitedColor
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_60;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_60;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					if (value != this.ᜂ)
					{
						num = 1;
						continue;
					}
					break;
					IL_60:
					this.ᜂ = value;
					num = 0;
				}
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x0006DA3C File Offset: 0x0006CA3C
		// (set) Token: 0x06000A70 RID: 2672 RVA: 0x0006DA80 File Offset: 0x0006CA80
		[DefaultValue(typeof(Color), "Empty")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Color LinkActiveColor
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_60;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_60;
						}
						if (true)
						{
						}
						if (false)
						{
						}
						break;
					}
					if (value != this.ᜄ)
					{
						num = 1;
						continue;
					}
					break;
					IL_60:
					this.ᜄ = value;
					num = 0;
				}
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x0006DB00 File Offset: 0x0006CB00
		// (set) Token: 0x06000A72 RID: 2674 RVA: 0x0006DB44 File Offset: 0x0006CB44
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Font Font
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
				int num = 2;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							this.ᜅ.Dispose();
							this.ᜅ = value;
							num = 1;
							continue;
						case 1:
							return;
						case 3:
							num = 4;
							continue;
						case 4:
							if (value != this.ᜅ)
							{
								num = 0;
								continue;
							}
							return;
						}
						if (value == null)
						{
							return;
						}
						if (true)
						{
						}
						num = 3;
						break;
					}
				}
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0006DBEC File Offset: 0x0006CBEC
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x0006DC30 File Offset: 0x0006CC30
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(typeof(Color), "Black")]
		public Color FontColor
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
							goto IL_58;
						}
						if (false)
						{
						}
						break;
					case 1:
						return;
					case 2:
						goto IL_58;
					}
					if (value != this.ᜆ)
					{
						num = 2;
						continue;
					}
					break;
					IL_58:
					this.ᜆ = value;
					if (true)
					{
					}
					num = 1;
				}
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x0006DCB0 File Offset: 0x0006CCB0
		// (set) Token: 0x06000A76 RID: 2678 RVA: 0x0006DCF4 File Offset: 0x0006CCF4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		public StringListCollection AdvancedAttributes
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
				int num = 4;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_66;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							this.ᜇ = value;
							num = 1;
							continue;
						case 1:
							goto IL_66;
						case 2:
							if (value != this.ᜇ)
							{
								num = 0;
								continue;
							}
							goto IL_83;
						case 3:
							num = 2;
							continue;
						}
						if (value == null)
						{
							goto IL_83;
						}
						num = 3;
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

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x0006DD8C File Offset: 0x0006CD8C
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x0006DDD0 File Offset: 0x0006CDD0
		[DefaultValue(DefaultOptions.FontSize)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public DefaultOptions DefaultOptions
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_5B;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5B;
						}
						if (false)
						{
						}
						break;
					}
					if (value != this.ᜈ)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					break;
					IL_5B:
					this.ᜈ = value;
					num = 0;
				}
			}
		}

		// Token: 0x040007D5 RID: 2005
		private int \u2460\u00AB\u008D\u00A6;

		// Token: 0x040007D6 RID: 2006
		private Color ᜀ;

		// Token: 0x040007D7 RID: 2007
		private string \u25D9\u00A2\u009E\u00B0;

		// Token: 0x040007D8 RID: 2008
		private string ᜁ;

		// Token: 0x040007D9 RID: 2009
		private Color ᜂ;

		// Token: 0x040007DA RID: 2010
		private Color ᜃ;

		// Token: 0x040007DB RID: 2011
		private int[] \u2609\u008D\u0085\u0081;

		// Token: 0x040007DC RID: 2012
		private Color ᜄ;

		// Token: 0x040007DD RID: 2013
		private int[] \u2593\u0093\u00A2\u009D;

		// Token: 0x040007DE RID: 2014
		private Font ᜅ;

		// Token: 0x040007DF RID: 2015
		private Color ᜆ;

		// Token: 0x040007E0 RID: 2016
		private StringListCollection ᜇ;

		// Token: 0x040007E1 RID: 2017
		private long \u2460\u00A2\u0094\u0097;

		// Token: 0x040007E2 RID: 2018
		private DefaultOptions ᜈ;
	}
}
