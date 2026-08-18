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
	// Token: 0x02000181 RID: 385
	public class HtmlTableOptions : ICloneable
	{
		// Token: 0x06000A8A RID: 2698 RVA: 0x0006E518 File Offset: 0x0006D518
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
			return new HtmlTableOptions
			{
				BorderWidth = this.BorderWidth,
				CellPadding = this.CellPadding,
				CellSpacing = this.CellSpacing,
				AdvancedAttributes = this.AdvancedAttributes,
				HeadersBackColor = this.HeadersBackColor,
				HeadersFontColor = this.HeadersFontColor,
				BackColor = this.BackColor,
				FontColor = this.FontColor,
				OddBackColor = this.OddBackColor,
				BorderColor = this.BorderColor
			};
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0006E5D4 File Offset: 0x0006D5D4
		public void SaveToXmlFile(XMLFile File, string Section)
		{
			int a_ = 7;
			switch (0)
			{
			default:
			{
				StringBuilder stringBuilder;
				for (;;)
				{
					File.WriteValue(HyperlinksCollectionEditor.b("欢焤樦攨", a_), HyperlinksCollectionEditor.b("怢䀤䬦䔨笪䰬䬮唰娲嬴倶", a_), this.ᜀ.ToString());
					File.WriteValue(HyperlinksCollectionEditor.b("欢焤樦攨", a_), HyperlinksCollectionEditor.b("怢䀤䬦䔨砪崬丮到娲嬴倶", a_), this.ᜁ.ToString());
					File.WriteValue(HyperlinksCollectionEditor.b("欢焤樦攨", a_), HyperlinksCollectionEditor.b("愢䨤唦䴨个弬瀮昰娲儴䌶儸", a_), this.ᜂ.ToString());
					File.WriteValue(HyperlinksCollectionEditor.b("欢焤樦攨", a_), HyperlinksCollectionEditor.b("眢䐤䔦䔨个漬丮到堲刴䔶嘸为匼嬾", a_), this.ᜊ);
					stringBuilder = new StringBuilder(this.ᜃ.Count);
					IEnumerator enumerator = this.ᜃ.GetEnumerator();
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (stringBuilder.Length > 0)
							{
								num = 1;
								continue;
							}
							goto IL_24A;
						case 1:
							goto IL_22C;
						case 2:
							goto IL_248;
						case 3:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								if (false)
								{
								}
								try
								{
									num = 0;
									for (;;)
									{
										switch (num)
										{
										case 1:
											goto IL_1E2;
										case 2:
										{
											if (!enumerator.MoveNext())
											{
												num = 3;
												continue;
											}
											string arg = (string)enumerator.Current;
											stringBuilder.AppendFormat(HyperlinksCollectionEditor.b("堢ᔤ娦刨ᨪ倬", a_), arg, ';');
											num = 4;
											continue;
										}
										case 3:
											num = 1;
											continue;
										}
										IL_1BD:
										num = 2;
										continue;
										goto IL_1BD;
									}
									IL_1E2:
									goto IL_138;
								}
								finally
								{
									for (;;)
									{
										IDisposable disposable = enumerator as IDisposable;
										num = 2;
										for (;;)
										{
											switch (num)
											{
											case 0:
												goto IL_229;
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
												goto IL_22B;
											}
											break;
										}
									}
									IL_229:
									IL_22B:;
								}
								goto IL_22C;
								IL_138:
								num = 0;
								continue;
							}
							break;
						}
						break;
						IL_22C:
						stringBuilder.Remove(stringBuilder.Length - 1, 1);
						num = 2;
					}
				}
				IL_248:
				IL_24A:
				File.WriteValue(HyperlinksCollectionEditor.b("欢焤樦攨", a_), HyperlinksCollectionEditor.b("眢䐤䔦䔨个氬䬮䜰刲嬴吶尸强", a_), stringBuilder.ToString());
				File.WriteValue(HyperlinksCollectionEditor.b("欢焤樦攨", a_), HyperlinksCollectionEditor.b("氢䄤䌦笨䐪娬洮倰倲帴倶䬸吺䠼儾╀B⩄⭆♈㥊", a_), this.ᜄ.ToArgb().ToString(HyperlinksCollectionEditor.b("笢", a_)));
				File.WriteValue(HyperlinksCollectionEditor.b("欢焤樦攨", a_), HyperlinksCollectionEditor.b("欢䀤䘦䴨洪䈬䄮䔰瀲娴嬶嘸䤺", a_), this.ᜅ.ToArgb().ToString(HyperlinksCollectionEditor.b("笢", a_)));
				File.WriteValue(HyperlinksCollectionEditor.b("欢焤樦攨", a_), HyperlinksCollectionEditor.b("朢䐤匦䠨洪䈬䄮䔰瀲娴嬶嘸䤺", a_), this.ᜆ.ToArgb().ToString(HyperlinksCollectionEditor.b("笢", a_)));
				File.WriteValue(HyperlinksCollectionEditor.b("欢焤樦攨", a_), HyperlinksCollectionEditor.b("昢匤䈦䜨礪䈬堮猰刲嘴尶常䤺刼䨾⽀❂ل⡆╈⑊㽌", a_), this.ᜇ.ToArgb().ToString(HyperlinksCollectionEditor.b("笢", a_)));
				File.WriteValue(HyperlinksCollectionEditor.b("欢焤樦攨", a_), HyperlinksCollectionEditor.b("欢䀤䘦䴨椪䰬䰮娰吲䜴堶䰸唺夼簾⹀⽂⩄㕆", a_), this.ᜈ.ToArgb().ToString(HyperlinksCollectionEditor.b("笢", a_)));
				File.SaveToFile();
				return;
			}
			}
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0006E9C0 File Offset: 0x0006D9C0
		public void LoadFromXmlFile(XMLFile File, string Section)
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				for (;;)
				{
					this.ᜀ = Convert.ToInt32(File.ReadValue(HyperlinksCollectionEditor.b("圞甠渢椤", a_), HyperlinksCollectionEditor.b("尞䐠伢䤤眦䠨伪䤬䘮弰吲", a_), 4.ToString()));
					this.ᜁ = Convert.ToInt32(File.ReadValue(HyperlinksCollectionEditor.b("圞甠渢椤", a_), HyperlinksCollectionEditor.b("尞䐠伢䤤琦夨䨪丬䘮弰吲", a_), 1.ToString()));
					this.ᜂ = Convert.ToInt32(File.ReadValue(HyperlinksCollectionEditor.b("圞甠渢椤", a_), HyperlinksCollectionEditor.b("崞丠儢䄤䈦嬨琪稬䘮唰䜲崴", a_), 1.ToString()));
					this.ᜊ = File.ReadValue(HyperlinksCollectionEditor.b("圞甠渢椤", a_), HyperlinksCollectionEditor.b("䬞䀠䄢䤤䈦欨䨪丬䐮嘰䄲娴䈶圸强", a_), string.Empty);
					string text = File.ReadValue(HyperlinksCollectionEditor.b("圞甠渢椤", a_), HyperlinksCollectionEditor.b("䬞䀠䄢䤤䈦栨伪嬬丮弰倲倴匶", a_), string.Empty);
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							this.ᜃ = new StringListCollection();
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 5;
								continue;
							}
							break;
						case 1:
							if (this.ᜃ.Count > 0)
							{
								num = 0;
								continue;
							}
							goto IL_1F0;
						case 2:
							goto IL_1EE;
						case 3:
							this.ᜃ.SetStrings(text.Split(new char[]
							{
								';'
							}));
							num = 2;
							continue;
						case 4:
							if (text.Length > 0)
							{
								num = 3;
								continue;
							}
							num = 1;
							continue;
						case 5:
							goto IL_185;
						}
						break;
					}
				}
				IL_185:
				IL_1EE:
				IL_1F0:
				string sectionName = HyperlinksCollectionEditor.b("圞甠渢椤", a_);
				string key = HyperlinksCollectionEditor.b("倞䔠䜢眤䠦帨椪䰬䰮娰吲䜴堶䰸唺夼簾⹀⽂⩄㕆", a_);
				Color roddRowBgColor = HtmlExportStyles.DOS.ROddRowBgColor;
				this.ᜄ = Color.FromArgb(Convert.ToInt32(File.ReadValue(sectionName, key, roddRowBgColor.ToArgb().ToString(HyperlinksCollectionEditor.b("䜞", a_))), 16));
				string sectionName2 = HyperlinksCollectionEditor.b("圞甠渢椤", a_);
				string key2 = HyperlinksCollectionEditor.b("圞䐠䈢䄤愦䘨䔪夬氮帰弲娴䔶", a_);
				Color rheadersRowFontColor = HtmlExportStyles.DOS.RHeadersRowFontColor;
				this.ᜅ = Color.FromArgb(Convert.ToInt32(File.ReadValue(sectionName2, key2, rheadersRowFontColor.ToArgb().ToString(HyperlinksCollectionEditor.b("䜞", a_))), 16));
				string sectionName3 = HyperlinksCollectionEditor.b("圞甠渢椤", a_);
				string key3 = HyperlinksCollectionEditor.b("嬞䀠圢䐤愦䘨䔪夬氮帰弲娴䔶", a_);
				Color rtableFontColor = HtmlExportStyles.DOS.RTableFontColor;
				this.ᜆ = Color.FromArgb(Convert.ToInt32(File.ReadValue(sectionName3, key3, rtableFontColor.ToArgb().ToString(HyperlinksCollectionEditor.b("䜞", a_))), 16));
				string sectionName4 = HyperlinksCollectionEditor.b("圞甠渢椤", a_);
				string key4 = HyperlinksCollectionEditor.b("娞圠䘢䬤甦䘨尪漬丮到堲刴䔶嘸为匼嬾ɀⱂ⥄⡆㭈", a_);
				Color rtableBgColor = HtmlExportStyles.DOS.RTableBgColor;
				this.ᜇ = Color.FromArgb(Convert.ToInt32(File.ReadValue(sectionName4, key4, rtableBgColor.ToArgb().ToString(HyperlinksCollectionEditor.b("䜞", a_))), 16));
				string sectionName5 = HyperlinksCollectionEditor.b("圞甠渢椤", a_);
				string key5 = HyperlinksCollectionEditor.b("圞䐠䈢䄤攦䠨䠪䘬䠮䌰尲䀴夶崸砺刼匾⹀ㅂ", a_);
				Color rheadersRowBgColor = HtmlExportStyles.DOS.RHeadersRowBgColor;
				this.ᜈ = Color.FromArgb(Convert.ToInt32(File.ReadValue(sectionName5, key5, rheadersRowBgColor.ToArgb().ToString(HyperlinksCollectionEditor.b("䜞", a_))), 16));
				return;
			}
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0006ED94 File Offset: 0x0006DD94
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x0006EDD8 File Offset: 0x0006DDD8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(1)]
		public int BorderWidth
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
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 2:
							if (true)
							{
							}
							this.ᜂ = value;
							num = 0;
							continue;
						}
						if (this.ᜂ == value)
						{
							break;
						}
						num = 2;
					}
					break;
				}
				}
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0006EE54 File Offset: 0x0006DE54
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x0006EE98 File Offset: 0x0006DE98
		[DefaultValue(4)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int CellPadding
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
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							break;
						case 1:
							return;
						case 2:
							this.ᜀ = value;
							num = 1;
							continue;
						}
						if (this.ᜀ == value)
						{
							break;
						}
						num = 2;
					}
					break;
				}
				}
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0006EF14 File Offset: 0x0006DF14
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x0006EF58 File Offset: 0x0006DF58
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(1)]
		public int CellSpacing
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
					if (true)
					{
					}
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.ᜁ = value;
							num = 1;
							continue;
						case 1:
							return;
						}
						if (this.ᜁ == value)
						{
							break;
						}
						num = 0;
					}
					break;
				}
				}
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0006EFD4 File Offset: 0x0006DFD4
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x0006F018 File Offset: 0x0006E018
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		public StringListCollection AdvancedAttributes
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
				return this.ᜃ;
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
						return;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							return;
						case 1:
							if (true)
							{
							}
							break;
						case 2:
							num = 3;
							continue;
						case 3:
							if (value != this.ᜃ)
							{
								num = 4;
								continue;
							}
							return;
						case 4:
							this.ᜃ = value;
							num = 0;
							continue;
						}
						if (value == null)
						{
							return;
						}
						num = 2;
						break;
					}
				}
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0006F0B0 File Offset: 0x0006E0B0
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x0006F0F4 File Offset: 0x0006E0F4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(typeof(Color), "Empty")]
		public Color HeadersBackColor
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
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜈ = value;
						num = 2;
						continue;
					case 1:
						IL_08:
						break;
					case 2:
						goto IL_57;
					}
					if (true)
					{
					}
					if (this.ᜈ != value)
					{
						num = 0;
						continue;
					}
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_08;
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

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0006F174 File Offset: 0x0006E174
		// (set) Token: 0x06000A98 RID: 2712 RVA: 0x0006F1B8 File Offset: 0x0006E1B8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(typeof(Color), "Empty")]
		public Color HeadersFontColor
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
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						this.ᜅ = value;
						num = 1;
						continue;
					case 1:
						goto IL_57;
					case 2:
						IL_08:
						break;
					}
					if (this.ᜅ != value)
					{
						num = 0;
						continue;
					}
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_08;
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

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0006F238 File Offset: 0x0006E238
		// (set) Token: 0x06000A9A RID: 2714 RVA: 0x0006F27C File Offset: 0x0006E27C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(typeof(Color), "Empty")]
		public Color BackColor
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
				return this.ᜇ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						IL_08:
						break;
					case 1:
						goto IL_4F;
					case 2:
						this.ᜇ = value;
						num = 1;
						continue;
					}
					if (this.ᜇ != value)
					{
						num = 2;
						continue;
					}
					IL_4F:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_08;
					default:
						goto IL_65;
					}
				}
				IL_65:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0006F2FC File Offset: 0x0006E2FC
		// (set) Token: 0x06000A9C RID: 2716 RVA: 0x0006F340 File Offset: 0x0006E340
		[DefaultValue(typeof(Color), "Empty")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Color FontColor
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
					case 1:
						IL_08:
						break;
					case 2:
						goto IL_4F;
					}
					if (this.ᜆ != value)
					{
						num = 0;
						continue;
					}
					IL_4F:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_08;
					default:
						goto IL_65;
					}
				}
				IL_65:
				if (false)
				{
				}
				if (true)
				{
				}
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0006F3C0 File Offset: 0x0006E3C0
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x0006F404 File Offset: 0x0006E404
		[DefaultValue(typeof(Color), "Empty")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Color OddBackColor
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
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_57;
					case 1:
						IL_08:
						break;
					case 2:
						this.ᜄ = value;
						if (true)
						{
						}
						num = 0;
						continue;
					}
					if (this.ᜄ != value)
					{
						num = 2;
						continue;
					}
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_08;
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

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0006F484 File Offset: 0x0006E484
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x0006F4C8 File Offset: 0x0006E4C8
		[DefaultValue(typeof(Color), "White")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Color BorderColor
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
				return this.ᜉ;
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
				this.ᜉ = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0006F50C File Offset: 0x0006E50C
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x0006F550 File Offset: 0x0006E550
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string BackImageUrl
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
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜊ = value;
			}
		}

		// Token: 0x040007F3 RID: 2035
		private int ᜀ = 4;

		// Token: 0x040007F4 RID: 2036
		private int ᜁ = 1;

		// Token: 0x040007F5 RID: 2037
		private int ᜂ = 1;

		// Token: 0x040007F6 RID: 2038
		private int[] \u2593\u009D\u007Fª;

		// Token: 0x040007F7 RID: 2039
		private StringListCollection ᜃ = new StringListCollection();

		// Token: 0x040007F8 RID: 2040
		private long[] \u2593\u00A6\u0096\u0084;

		// Token: 0x040007F9 RID: 2041
		private Color ᜄ = Color.Empty;

		// Token: 0x040007FA RID: 2042
		private byte[] \u25D9\u0090\u0090\u00A6;

		// Token: 0x040007FB RID: 2043
		private int[] \u25D9ª\u0099\u007F;

		// Token: 0x040007FC RID: 2044
		private Color ᜅ = Color.Empty;

		// Token: 0x040007FD RID: 2045
		private string \u25D8\u00AF\u0099\u0084;

		// Token: 0x040007FE RID: 2046
		private bool[] \u2609\u009B\u0086\u008B;

		// Token: 0x040007FF RID: 2047
		private Color ᜆ = Color.Empty;

		// Token: 0x04000800 RID: 2048
		private Color ᜇ = Color.Empty;

		// Token: 0x04000801 RID: 2049
		private Color ᜈ = Color.Empty;

		// Token: 0x04000802 RID: 2050
		private Color ᜉ = Color.White;

		// Token: 0x04000803 RID: 2051
		private string ᜊ = string.Empty;
	}
}
