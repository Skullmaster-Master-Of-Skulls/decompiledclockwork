using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.Common;
using Spire.DataExport.Utils;

namespace Spire.DataExport.Forms
{
	// Token: 0x0200019E RID: 414
	public partial class DataExportColumnsEditor : Form
	{
		// Token: 0x06000B46 RID: 2886 RVA: 0x00075624 File Offset: 0x00074624
		public DataExportColumnsEditor()
		{
			this.ᜄ();
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x000756F8 File Offset: 0x000746F8
		private void ᜄ()
		{
			int a_ = 6;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.\u170D = new Container();
			ResourceManager resourceManager = new ResourceManager(typeof(DataExportColumnsEditor));
			this.ᜀ = new sprᯅ();
			this.ᜁ = new sprᯅ();
			this.ᜂ = new ListView();
			this.ᜅ = new ColumnHeader();
			this.ᜌ = new ImageList(this.\u170D);
			this.ᜃ = new ListView();
			this.ᜆ = new ColumnHeader();
			this.ᜄ = new sprᯅ();
			this.ᜇ = new sprᯅ();
			this.ᜈ = new sprᯅ();
			this.ᜉ = new sprᯅ();
			this.ᜊ = new sprᯅ();
			this.ᜋ = new sprᯅ();
			base.SuspendLayout();
			this.ᜀ.ᜀ(new Point(0, 0));
			this.ᜀ.ᜀ(emunType.BtnShape.Rectangle);
			this.ᜀ.ᜀ(emunType.XPStyle.Default);
			this.ᜀ.DialogResult = DialogResult.OK;
			this.ᜀ.Location = new Point(262, 296);
			this.ᜀ.Name = HyperlinksCollectionEditor.b("䀡倣䠥朧愩", a_);
			this.ᜀ.Size = new Size(90, 25);
			this.ᜀ.TabIndex = 8;
			this.ᜀ.Text = HyperlinksCollectionEditor.b("洡漣", a_);
			this.ᜁ.ᜀ(new Point(0, 0));
			this.ᜁ.ᜀ(emunType.BtnShape.Rectangle);
			this.ᜁ.ᜀ(emunType.XPStyle.Default);
			this.ᜁ.DialogResult = DialogResult.Cancel;
			this.ᜁ.Location = new Point(360, 296);
			this.ᜁ.Name = HyperlinksCollectionEditor.b("䀡倣䠥欧䬩䈫䴭唯帱", a_);
			this.ᜁ.Size = new Size(90, 25);
			this.ᜁ.TabIndex = 9;
			this.ᜁ.Text = HyperlinksCollectionEditor.b("愡䔣䠥䬧伩䀫", a_);
			this.ᜂ.AllowDrop = true;
			this.ᜂ.Columns.AddRange(new ColumnHeader[]
			{
				this.ᜅ
			});
			this.ᜂ.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.ᜂ.HideSelection = false;
			this.ᜂ.Location = new Point(19, 17);
			this.ᜂ.Name = HyperlinksCollectionEditor.b("両刣朥帧䬩䔫䈭儯倱堳匵", a_);
			this.ᜂ.Size = new Size(165, 259);
			this.ᜂ.SmallImageList = this.ᜌ;
			this.ᜂ.TabIndex = 0;
			this.ᜂ.View = View.Details;
			this.ᜂ.MouseDown += this.ᜂ;
			this.ᜂ.DoubleClick += this.ᜂ;
			this.ᜂ.MouseUp += this.ᜁ;
			this.ᜂ.DragOver += this.ᜁ;
			this.ᜂ.DragDrop += this.ᜀ;
			this.ᜂ.QueryContinueDrag += this.ᜀ;
			this.ᜂ.MouseMove += this.ᜀ;
			this.ᜂ.Enter += this.ᜀ;
			this.ᜂ.SelectedIndexChanged += this.ᜉ;
			this.ᜅ.Text = HyperlinksCollectionEditor.b("挡刣䜥䄧䘩䴫䰭尯圱ᐳ电圷嘹䤻匽⸿ㅁ", a_);
			this.ᜅ.Width = 160;
			this.ᜌ.ColorDepth = ColorDepth.Depth16Bit;
			this.ᜌ.ImageSize = new Size(16, 16);
			this.ᜌ.ImageStream = (ImageListStreamer)resourceManager.GetObject(HyperlinksCollectionEditor.b("䬡䤣䜥伧伩怫䜭䌯䘱ᨳ缵唷嬹嬻嬽ጿ㙁㙃⍅⥇❉", a_));
			this.ᜌ.TransparentColor = Color.Transparent;
			this.ᜃ.AllowDrop = true;
			this.ᜃ.Columns.AddRange(new ColumnHeader[]
			{
				this.ᜆ
			});
			this.ᜃ.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.ᜃ.HideSelection = false;
			this.ᜃ.Location = new Point(288, 16);
			this.ᜃ.Name = HyperlinksCollectionEditor.b("両刣挥倧娩䌫尭䐯圱倳", a_);
			this.ᜃ.Size = new Size(168, 239);
			this.ᜃ.SmallImageList = this.ᜌ;
			this.ᜃ.TabIndex = 1;
			this.ᜃ.View = View.Details;
			this.ᜃ.MouseDown += this.ᜂ;
			this.ᜃ.DoubleClick += this.ᜁ;
			this.ᜃ.MouseUp += this.ᜁ;
			this.ᜃ.DragOver += this.ᜁ;
			this.ᜃ.DragDrop += this.ᜀ;
			this.ᜃ.QueryContinueDrag += this.ᜀ;
			this.ᜃ.MouseMove += this.ᜀ;
			this.ᜃ.Enter += this.ᜀ;
			this.ᜃ.SelectedIndexChanged += this.ᜉ;
			this.ᜆ.Text = HyperlinksCollectionEditor.b("無䄣䨥䴧䤩堫䬭启ሱ眳夵吷伹儻倽㌿", a_);
			this.ᜆ.Width = 158;
			this.ᜄ.ᜀ(new Point(0, 0));
			this.ᜄ.ᜀ(emunType.BtnShape.Rectangle);
			this.ᜄ.ᜀ(emunType.XPStyle.Default);
			this.ᜄ.ImageList = this.ᜌ;
			this.ᜄ.Location = new Point(200, 78);
			this.ᜄ.Name = HyperlinksCollectionEditor.b("䀡倣䠥椧丩䠫紭唯帱儳唵䰷弹堻", a_);
			this.ᜄ.Size = new Size(64, 25);
			this.ᜄ.TabIndex = 2;
			this.ᜄ.TabStop = false;
			this.ᜄ.Text = HyperlinksCollectionEditor.b("ᰡ", a_);
			this.ᜄ.Click += this.ᜈ;
			this.ᜇ.ᜀ(new Point(0, 0));
			this.ᜇ.ᜀ(emunType.BtnShape.Rectangle);
			this.ᜇ.ᜀ(emunType.XPStyle.Default);
			this.ᜇ.ImageList = this.ᜌ;
			this.ᜇ.Location = new Point(200, 112);
			this.ᜇ.Name = HyperlinksCollectionEditor.b("䀡倣䠥椧丩䠫漭尯帱", a_);
			this.ᜇ.Size = new Size(64, 25);
			this.ᜇ.TabIndex = 3;
			this.ᜇ.TabStop = false;
			this.ᜇ.Text = HyperlinksCollectionEditor.b("ᰡᨣ", a_);
			this.ᜇ.Click += this.ᜇ;
			this.ᜈ.ᜀ(new Point(0, 0));
			this.ᜈ.ᜀ(emunType.BtnShape.Rectangle);
			this.ᜈ.ᜀ(emunType.XPStyle.Default);
			this.ᜈ.ImageList = this.ᜌ;
			this.ᜈ.Location = new Point(200, 146);
			this.ᜈ.Name = HyperlinksCollectionEditor.b("䀡倣䠥氧伩䀫紭唯帱儳唵䰷弹堻", a_);
			this.ᜈ.Size = new Size(64, 25);
			this.ᜈ.TabIndex = 4;
			this.ᜈ.TabStop = false;
			this.ᜈ.Text = HyperlinksCollectionEditor.b("ḡ", a_);
			this.ᜈ.Click += this.ᜆ;
			this.ᜉ.ᜀ(new Point(0, 0));
			this.ᜉ.ᜀ(emunType.BtnShape.Rectangle);
			this.ᜉ.ᜀ(emunType.XPStyle.Default);
			this.ᜉ.ImageList = this.ᜌ;
			this.ᜉ.Location = new Point(200, 181);
			this.ᜉ.Name = HyperlinksCollectionEditor.b("䀡倣䠥氧伩䀫漭尯帱", a_);
			this.ᜉ.Size = new Size(64, 25);
			this.ᜉ.TabIndex = 5;
			this.ᜉ.TabStop = false;
			this.ᜉ.Text = HyperlinksCollectionEditor.b("ḡᠣ", a_);
			this.ᜉ.Click += this.ᜅ;
			this.ᜊ.ᜀ(new Point(0, 0));
			this.ᜊ.ᜀ(emunType.BtnShape.Rectangle);
			this.ᜊ.ᜀ(emunType.XPStyle.Default);
			this.ᜊ.ImageList = this.ᜌ;
			this.ᜊ.Location = new Point(368, 264);
			this.ᜊ.Name = HyperlinksCollectionEditor.b("䀡倣䠥攧䔩娫䬭猯崱堳䌵唷吹椻丽", a_);
			this.ᜊ.Size = new Size(40, 25);
			this.ᜊ.TabIndex = 6;
			this.ᜊ.TabStop = false;
			this.ᜊ.Text = HyperlinksCollectionEditor.b("眡吣", a_);
			this.ᜊ.Click += this.ᜄ;
			this.ᜋ.ᜀ(new Point(0, 0));
			this.ᜋ.ᜀ(emunType.BtnShape.Rectangle);
			this.ᜋ.ᜀ(emunType.XPStyle.Default);
			this.ᜋ.ImageList = this.ᜌ;
			this.ᜋ.Location = new Point(416, 264);
			this.ᜋ.Name = HyperlinksCollectionEditor.b("䀡倣䠥攧䔩娫䬭猯崱堳䌵唷吹砻儽㜿ⱁ", a_);
			this.ᜋ.Size = new Size(40, 25);
			this.ᜋ.TabIndex = 7;
			this.ᜋ.TabStop = false;
			this.ᜋ.Text = HyperlinksCollectionEditor.b("昡䬣儥䘧", a_);
			this.ᜋ.Click += this.ᜃ;
			base.AcceptButton = this.ᜀ;
			this.AutoScaleBaseSize = new Size(6, 14);
			base.CancelButton = this.ᜁ;
			base.ClientSize = new Size(482, 336);
			base.Controls.AddRange(new Control[]
			{
				this.ᜋ,
				this.ᜊ,
				this.ᜉ,
				this.ᜈ,
				this.ᜇ,
				this.ᜄ,
				this.ᜃ,
				this.ᜂ,
				this.ᜁ,
				this.ᜀ
			});
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = HyperlinksCollectionEditor.b("昡䔣別䤧漩含席弯䀱䀳电圷嘹䤻匽⸿ㅁŃ≅ⅇ㹉⍋㱍", a_);
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = HyperlinksCollectionEditor.b("愡䬣䨥崧䜩䈫崭", a_);
			base.ResumeLayout(false);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x000762B4 File Offset: 0x000752B4
		public static bool RunDataExportColumnsEditor(ExportSource ExportSource, IDbCommand Command, DataTable DataTable, ListView ListView, StringListCollection ExportedFields)
		{
			int num;
			bool flag;
			DataExportColumnsEditor dataExportColumnsEditor;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				for (;;)
				{
					IL_28:
					switch (num)
					{
					case 0:
						return flag;
					case 1:
						if (flag)
						{
							num = 2;
							continue;
						}
						return flag;
					case 2:
						dataExportColumnsEditor.ᜁ();
						if (true)
						{
						}
						num = 0;
						continue;
					}
					goto IL_3A;
				}
				return flag;
			default:
				if (false)
				{
				}
				break;
			}
			IL_3A:
			dataExportColumnsEditor = new DataExportColumnsEditor();
			dataExportColumnsEditor.ᜎ = ExportSource;
			dataExportColumnsEditor.ᜏ = Command;
			dataExportColumnsEditor.ᜐ = DataTable;
			dataExportColumnsEditor.ᜑ = ListView;
			dataExportColumnsEditor.\u1712 = ExportedFields;
			dataExportColumnsEditor.ᜃ();
			flag = (dataExportColumnsEditor.ShowDialog() == DialogResult.OK);
			num = 1;
			goto IL_28;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00076364 File Offset: 0x00075364
		private void ᜃ()
		{
			switch (0)
			{
			default:
			{
				ListDictionary listDictionary = new ListDictionary();
				ListDictionary listDictionary2 = new ListDictionary();
				spr\u2059.ᜀ(this.ᜎ, this.ᜏ, this.ᜐ, this.ᜑ, this.\u1712, listDictionary, listDictionary2);
				this.ᜃ.BeginUpdate();
				try
				{
					for (;;)
					{
						IL_1D9:
						this.ᜃ.Items.Clear();
						IEnumerator enumerator = listDictionary2.Keys.GetEnumerator();
						for (;;)
						{
							int num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_2B8;
								case 1:
									num = 4;
									continue;
								case 2:
									goto IL_2B8;
								case 3:
									this.ᜃ.Items[0].Focused = true;
									this.ᜃ.Items[0].Selected = true;
									num = 5;
									continue;
								case 4:
									if (this.ᜃ.Items.Count > 0)
									{
										num = 3;
										continue;
									}
									goto IL_2DD;
								case 5:
									goto IL_2DD;
								case 6:
								{
									if (!enumerator.MoveNext())
									{
										num = 1;
										continue;
									}
									ListViewItem listViewItem = new ListViewItem();
									listViewItem.Text = (string)enumerator.Current;
									listViewItem.Tag = listDictionary2[enumerator.Current];
									this.ᜃ.Items.Add(listViewItem);
									num = 0;
									continue;
								}
								case 7:
									goto IL_2E9;
								}
								goto IL_1D9;
								IL_2B8:
								num = 6;
								continue;
								IL_2DD:
								num = 7;
							}
							IL_2E9:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_2FF;
							}
						}
					}
					IL_2FF:
					if (false)
					{
					}
					goto IL_1A0;
				}
				finally
				{
					if (true)
					{
					}
					this.ᜃ.EndUpdate();
				}
				goto IL_31E;
				for (;;)
				{
					IL_1A0:
					this.ᜂ.BeginUpdate();
					try
					{
						for (;;)
						{
							this.ᜂ.Items.Clear();
							IEnumerator enumerator2 = listDictionary.Keys.GetEnumerator();
							int num = 7;
							for (;;)
							{
								switch (num)
								{
								case 0:
								{
									if (!enumerator2.MoveNext())
									{
										num = 6;
										continue;
									}
									ListViewItem listViewItem = new ListViewItem();
									listViewItem.Text = (string)enumerator2.Current;
									listViewItem.Tag = listDictionary[enumerator2.Current];
									this.ᜂ.Items.Add(listViewItem);
									num = 4;
									continue;
								}
								case 1:
									if (this.ᜂ.Items.Count > 0)
									{
										num = 2;
										continue;
									}
									goto IL_183;
								case 2:
									this.ᜂ.Items[0].Focused = true;
									this.ᜂ.Items[0].Selected = true;
									num = 3;
									continue;
								case 3:
									goto IL_183;
								case 4:
									goto IL_112;
								case 5:
									goto IL_18F;
								case 6:
									num = 1;
									continue;
								case 7:
									goto IL_112;
								}
								break;
								IL_112:
								num = 0;
								continue;
								IL_183:
								num = 5;
							}
						}
						IL_18F:
						break;
					}
					finally
					{
						this.ᜂ.EndUpdate();
					}
				}
				IL_31E:
				this.ᜂ();
				return;
			}
			}
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x000766CC File Offset: 0x000756CC
		private void ᜂ()
		{
			this.ᜃ.BeginUpdate();
			goto IL_167;
			try
			{
				for (;;)
				{
					IL_167:
					for (;;)
					{
						IL_19F:
						int num = 0;
						int num2 = 2;
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_167;
							default:
								if (false)
								{
								}
								switch (num2)
								{
								case 0:
									num2 = 3;
									continue;
								case 1:
									if (num >= this.ᜃ.Items.Count)
									{
										num2 = 0;
										continue;
									}
									this.ᜃ.Items[num].ImageIndex = 0;
									num++;
									num2 = 4;
									continue;
								case 2:
									goto IL_1D0;
								case 3:
									goto IL_203;
								case 4:
									goto IL_1D0;
								}
								goto IL_19F;
								IL_1D0:
								num2 = 1;
								break;
							}
						}
					}
				}
				IL_203:
				goto IL_157;
			}
			finally
			{
				this.ᜃ.EndUpdate();
			}
			return;
			for (;;)
			{
				IL_157:
				this.ᜂ.BeginUpdate();
				try
				{
					for (;;)
					{
						int num3 = 0;
						int num2 = 6;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_50;
							case 1:
								if (this.ᜃ.Items.Count == 0)
								{
									num2 = 7;
									continue;
								}
								goto IL_D4;
							case 2:
								goto IL_13E;
							case 3:
								goto IL_50;
							case 4:
								if (num3 >= this.ᜂ.Items.Count)
								{
									num2 = 5;
									continue;
								}
								num2 = 1;
								continue;
							case 5:
								num2 = 2;
								continue;
							case 6:
								goto IL_5E;
							case 7:
								num2 = 9;
								continue;
							case 8:
								goto IL_5E;
							case 9:
								if ((int)this.ᜂ.Items[num3].Tag == 0)
								{
									num2 = 10;
									continue;
								}
								goto IL_D4;
							case 10:
								this.ᜂ.Items[num3].ImageIndex = 0;
								num2 = 3;
								continue;
							}
							break;
							IL_50:
							num3++;
							num2 = 8;
							continue;
							IL_5E:
							num2 = 4;
							continue;
							IL_D4:
							this.ᜂ.Items[num3].ImageIndex = 1;
							num2 = 0;
						}
					}
					IL_13E:
					break;
				}
				finally
				{
					if (true)
					{
					}
					this.ᜂ.EndUpdate();
				}
			}
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0007692C File Offset: 0x0007592C
		private void ᜁ()
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_4B:
				num = 1;
				break;
			default:
				if (false)
				{
				}
				goto IL_34;
			}
			int num2;
			for (;;)
			{
				IL_1E:
				switch (num)
				{
				case 0:
					goto IL_B3;
				case 1:
					if (true)
					{
					}
					if (num2 >= this.ᜃ.Items.Count)
					{
						num = 2;
						continue;
					}
					this.\u1712.Add(this.ᜃ.Items[num2].Text);
					num2++;
					num = 0;
					continue;
				case 2:
					return;
				case 3:
					goto IL_49;
				}
				goto IL_34;
			}
			IL_49:
			IL_B3:
			goto IL_4B;
			IL_34:
			this.\u1712.Clear();
			num2 = 0;
			num = 3;
			goto IL_1E;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x000769F0 File Offset: 0x000759F0
		private void ᜀ()
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_154:
				num = 3;
				break;
			case 1:
				goto IL_20;
			default:
				goto IL_20;
			}
			for (;;)
			{
				IL_30:
				switch (num)
				{
				case 0:
					this.ᜈ.Enabled = (this.ᜃ.Items.Count > 0 && this.ᜃ.SelectedItems.Count > 0);
					this.ᜉ.Enabled = (this.ᜃ.Items.Count > 0);
					num = 1;
					continue;
				case 1:
					goto IL_10D;
				case 3:
					goto IL_15F;
				}
				this.ᜄ.Enabled = (this.ᜂ.Items.Count > 0 && this.ᜂ.SelectedItems.Count > 0);
				this.ᜇ.Enabled = (this.ᜂ.Items.Count > 0);
				if (true)
				{
				}
				num = 0;
			}
			IL_10D:
			this.ᜊ.Enabled = (this.ᜃ.Items.Count > 0 && this.ᜃ.FocusedItem != null && this.ᜃ.FocusedItem.Index > 0);
			goto IL_154;
			IL_15F:
			this.ᜋ.Enabled = (this.ᜃ.Items.Count > 0 && this.ᜃ.FocusedItem != null && this.ᜃ.FocusedItem.Index < this.ᜃ.Items.Count - 1);
			return;
			IL_20:
			if (false)
			{
			}
			num = 2;
			goto IL_30;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00076BB4 File Offset: 0x00075BB4
		private void ᜀ(ListView A_0, ListView A_1, bool A_2, int A_3)
		{
			switch (0)
			{
			default:
			{
				int num = 35;
				for (;;)
				{
					ArrayList arrayList;
					int num2;
					int num4;
					switch (num)
					{
					case 0:
						goto IL_5BD;
					case 1:
						A_2 = (arrayList.Count > 1);
						num2 = arrayList.Count - 1;
						num = 12;
						continue;
					case 2:
						goto IL_44B;
					case 3:
						goto IL_1AE;
					case 4:
						goto IL_184;
					case 5:
					{
						if (A_0.FocusedItem == null)
						{
							num = 16;
							continue;
						}
						int num3 = A_0.FocusedItem.Index;
						arrayList = new ArrayList();
						num4 = 0;
						num = 9;
						continue;
					}
					case 6:
						goto IL_5E5;
					case 7:
						A_1.Items[0].Focused = true;
						A_1.Items[0].Selected = true;
						num = 4;
						continue;
					case 8:
						if (A_0.FocusedItem == null)
						{
							num = 14;
							continue;
						}
						goto IL_189;
					case 9:
						goto IL_26D;
					case 10:
						goto IL_3F7;
					case 11:
						if (A_0.Items.Count > 0)
						{
							num = 0;
							continue;
						}
						goto IL_189;
					case 12:
						goto IL_3F7;
					case 13:
						goto IL_5E5;
					case 14:
						if (true)
						{
						}
						A_0.Focus();
						num = 15;
						continue;
					case 15:
						goto IL_189;
					case 16:
						return;
					case 17:
						num = 11;
						continue;
					case 18:
						try
						{
							for (;;)
							{
								string text = A_0.Items[A_3].Text;
								object tag = A_0.Items[A_3].Tag;
								int imageIndex = A_0.Items[A_3].ImageIndex;
								A_0.Items[A_3].Text = A_0.FocusedItem.Text;
								A_0.Items[A_3].Tag = A_0.FocusedItem.Tag;
								A_0.Items[A_3].ImageIndex = A_0.FocusedItem.ImageIndex;
								A_0.FocusedItem.Text = text;
								A_0.FocusedItem.Tag = tag;
								A_0.FocusedItem.ImageIndex = imageIndex;
								int num5 = 0;
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										num = 3;
										continue;
									case 1:
										goto IL_538;
									case 2:
										if (num5 >= A_0.Items.Count)
										{
											num = 0;
											continue;
										}
										A_0.Items[num5].Focused = (num5 == A_3);
										A_0.Items[num5].Selected = (num5 == A_3);
										num5++;
										num = 4;
										continue;
									case 3:
										goto IL_5B1;
									case 4:
										goto IL_538;
									}
									break;
									IL_538:
									num = 2;
								}
							}
							IL_5B1:
							goto IL_640;
						}
						finally
						{
							A_0.EndUpdate();
						}
						goto IL_5BD;
					case 19:
						if (!A_0.Items[num4].Selected)
						{
							num = 28;
							continue;
						}
						goto IL_2E7;
					case 20:
					{
						int num3 = Math.Min(num3, A_0.Items.Count - 1);
						int num6 = 0;
						num = 3;
						continue;
					}
					case 21:
						goto IL_2E7;
					case 22:
						goto IL_1AE;
					case 23:
						num = 34;
						continue;
					case 24:
					{
						if (A_2)
						{
							num = 26;
							continue;
						}
						ListViewItem listViewItem;
						A_1.Items.Add(listViewItem);
						num = 13;
						continue;
					}
					case 25:
					{
						int num6;
						if (num6 >= A_0.Items.Count)
						{
							num = 23;
							continue;
						}
						int num3;
						A_0.Items[num6].Focused = (num6 == num3);
						A_0.Items[num6].Selected = (num6 == num3);
						num6++;
						num = 22;
						continue;
					}
					case 26:
					{
						ListViewItem listViewItem;
						A_1.Items.Insert(0, listViewItem);
						num = 6;
						continue;
					}
					case 27:
						if (num4 >= A_0.Items.Count)
						{
							num = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_44B;
						default:
							if (false)
							{
							}
							num = 19;
							continue;
						}
						break;
					case 28:
						num = 30;
						continue;
					case 29:
						num = 32;
						continue;
					case 30:
						if (A_2)
						{
							num = 21;
							continue;
						}
						goto IL_43B;
					case 31:
					{
						if (num2 < 0)
						{
							num = 20;
							continue;
						}
						ListViewItem listViewItem = new ListViewItem();
						listViewItem.Text = A_0.Items[(int)arrayList[num2]].Text;
						listViewItem.Tag = A_0.Items[(int)arrayList[num2]].Tag;
						num = 24;
						continue;
					}
					case 32:
						if (A_1.FocusedItem == null)
						{
							num = 7;
							continue;
						}
						goto IL_260;
					case 33:
						goto IL_43B;
					case 34:
						if (A_1.Items.Count > 0)
						{
							num = 29;
							continue;
						}
						goto IL_260;
					}
					if (A_0 != A_1)
					{
						num = 17;
						continue;
					}
					A_0.BeginUpdate();
					num = 18;
					continue;
					IL_189:
					num = 5;
					continue;
					IL_1AE:
					num = 25;
					continue;
					IL_26D:
					num = 27;
					continue;
					IL_44B:
					goto IL_26D;
					IL_2E7:
					arrayList.Add(num4);
					num = 33;
					continue;
					IL_3F7:
					num = 31;
					continue;
					IL_43B:
					num4++;
					num = 2;
					continue;
					IL_5BD:
					num = 8;
					continue;
					IL_5E5:
					A_0.Items.RemoveAt((int)arrayList[num2]);
					arrayList.RemoveAt(num2);
					num2--;
					num = 10;
				}
				IL_184:
				IL_260:
				this.ᜂ();
				this.ᜀ();
				return;
				IL_640:
				this.ᜀ();
				return;
			}
			}
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00077224 File Offset: 0x00076224
		private void ᜉ(object A_0, EventArgs A_1)
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
			this.ᜀ();
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00077268 File Offset: 0x00076268
		private void ᜈ(object A_0, EventArgs A_1)
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
			this.ᜀ(this.ᜂ, this.ᜃ, false, -1);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x000772B8 File Offset: 0x000762B8
		private void ᜇ(object A_0, EventArgs A_1)
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
			this.ᜀ(this.ᜂ, this.ᜃ, true, -1);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00077308 File Offset: 0x00076308
		private void ᜆ(object A_0, EventArgs A_1)
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
			this.ᜀ(this.ᜃ, this.ᜂ, false, -1);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00077358 File Offset: 0x00076358
		private void ᜅ(object A_0, EventArgs A_1)
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
			this.ᜀ(this.ᜃ, this.ᜂ, true, -1);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x000773A8 File Offset: 0x000763A8
		private void ᜄ(object A_0, EventArgs A_1)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					for (;;)
					{
						this.ᜀ(this.ᜃ, this.ᜃ, false, this.ᜃ.FocusedItem.Index - 1);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_8B;
						}
					}
					IL_8B:
					if (false)
					{
					}
					num = 0;
					continue;
				case 2:
					if (this.ᜃ.FocusedItem.Index - 1 >= 0)
					{
						num = 1;
						continue;
					}
					return;
				case 3:
					if (true)
					{
					}
					break;
				case 4:
					num = 2;
					continue;
				}
				if (this.ᜃ.FocusedItem == null)
				{
					break;
				}
				num = 4;
			}
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00077480 File Offset: 0x00076480
		private void ᜃ(object A_0, EventArgs A_1)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (true)
					{
					}
					if (this.ᜃ.FocusedItem.Index + 1 <= this.ᜃ.Items.Count - 1)
					{
						num = 2;
						continue;
					}
					return;
				case 2:
					for (;;)
					{
						this.ᜀ(this.ᜃ, this.ᜃ, false, this.ᜃ.FocusedItem.Index + 1);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_83;
						}
					}
					IL_83:
					if (false)
					{
					}
					num = 4;
					continue;
				case 3:
					num = 1;
					continue;
				case 4:
					return;
				}
				if (this.ᜃ.FocusedItem == null)
				{
					break;
				}
				num = 3;
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0007756C File Offset: 0x0007656C
		private void ᜂ(object A_0, EventArgs A_1)
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
			this.ᜀ(this.ᜂ, this.ᜃ, false, -1);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x000775BC File Offset: 0x000765BC
		private void ᜁ(object A_0, EventArgs A_1)
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
			this.ᜀ(this.ᜃ, this.ᜂ, false, -1);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0007760C File Offset: 0x0007660C
		private void ᜂ(object A_0, MouseEventArgs A_1)
		{
			for (;;)
			{
				IL_20:
				if (true)
				{
				}
				ListViewItem itemAt = (A_0 as ListView).GetItemAt(A_1.X, A_1.Y);
				for (;;)
				{
					IL_4A:
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.\u1714 < (A_0 as ListView).Items.Count)
							{
								num = 4;
								continue;
							}
							goto IL_111;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_4A;
							default:
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						case 2:
							if (this.\u1714 >= 0)
							{
								num = 1;
								continue;
							}
							goto IL_111;
						case 3:
							if (itemAt == null)
							{
								num = 5;
								continue;
							}
							this.\u1714 = itemAt.Index;
							num = 2;
							continue;
						case 4:
							goto IL_A9;
						case 5:
							return;
						}
						goto IL_20;
					}
				}
			}
			return;
			IL_A9:
			Size dragSize = SystemInformation.DragSize;
			this.\u1715 = new Rectangle(new Point(A_1.X - dragSize.Width / 2, A_1.Y - dragSize.Height / 2), dragSize);
			return;
			IL_111:
			this.\u1715 = Rectangle.Empty;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00077738 File Offset: 0x00076738
		private void ᜁ(object A_0, MouseEventArgs A_1)
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
			this.\u1715 = Rectangle.Empty;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00077780 File Offset: 0x00076780
		private void ᜀ(object A_0, MouseEventArgs A_1)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!this.\u1715.Contains(A_1.X, A_1.Y))
					{
						num = 6;
						continue;
					}
					return;
				case 1:
					num = 0;
					continue;
				case 3:
					num = 5;
					continue;
				case 4:
					return;
				case 5:
					if (true)
					{
					}
					if (this.\u1715 != Rectangle.Empty)
					{
						num = 1;
						continue;
					}
					return;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						this.\u1713 = (A_0 as ListView);
						this.\u1716 = SystemInformation.WorkingArea.Location;
						(A_0 as ListView).DoDragDrop((A_0 as ListView).Items[this.\u1714], DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link | DragDropEffects.Scroll);
						num = 4;
						continue;
					}
					break;
				}
				IL_2C:
				if ((A_1.Button & MouseButtons.Left) == MouseButtons.Left)
				{
					num = 3;
					continue;
				}
				break;
				goto IL_2C;
			}
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x000778BC File Offset: 0x000768BC
		private void ᜁ(object A_0, DragEventArgs A_1)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 7;
					continue;
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					if (!A_1.Data.GetDataPresent(typeof(ListViewItem)))
					{
						num = 5;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_81;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 3:
					goto IL_D8;
				case 4:
					if ((A_1.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
					{
						num = 6;
						continue;
					}
					goto IL_F2;
				case 5:
					goto IL_AD;
				case 6:
					goto IL_81;
				case 7:
					if (this.\u1713 == A_0)
					{
						num = 3;
						continue;
					}
					goto IL_83;
				}
				if (this.\u1713 != null)
				{
					num = 0;
					continue;
				}
				IL_83:
				num = 2;
			}
			IL_81:
			A_1.Effect = DragDropEffects.Move;
			return;
			IL_AD:
			A_1.Effect = DragDropEffects.None;
			return;
			IL_D8:
			A_1.Effect = DragDropEffects.None;
			return;
			IL_F2:
			A_1.Effect = DragDropEffects.None;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x000779C4 File Offset: 0x000769C4
		private void ᜀ(object A_0, DragEventArgs A_1)
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_4C;
				case 1:
					if (A_0 == this.ᜃ)
					{
						num = 4;
						continue;
					}
					num = 5;
					continue;
				case 2:
					if (A_1.Data.GetDataPresent(typeof(ListViewItem)))
					{
						num = 10;
						continue;
					}
					return;
				case 3:
					if (A_1.Effect == DragDropEffects.Move)
					{
						num = 8;
						continue;
					}
					return;
				case 4:
					goto IL_82;
				case 5:
					goto IL_110;
				case 6:
					return;
				case 8:
					num = 1;
					continue;
				case 9:
					this.ᜀ(this.ᜃ, this.ᜂ, false, -1);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_110;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 10:
					num = 3;
					continue;
				}
				if (!(A_0 is ListView))
				{
					num = 0;
					continue;
				}
				num = 2;
				continue;
				IL_110:
				if (true)
				{
				}
				if (A_0 != this.ᜂ)
				{
					return;
				}
				num = 9;
			}
			IL_4C:
			A_1.Effect = DragDropEffects.None;
			return;
			IL_82:
			this.ᜀ(this.ᜂ, this.ᜃ, false, -1);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00077B24 File Offset: 0x00076B24
		private void ᜀ(object A_0, QueryContinueDragEventArgs A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					ListView listView = A_0 as ListView;
					int num = 0;
					for (;;)
					{
						Point mousePosition4;
						switch (num)
						{
						case 0:
							if (listView != null)
							{
								num = 7;
								continue;
							}
							return;
						case 1:
							return;
						case 2:
						{
							Point mousePosition;
							Form form;
							if (mousePosition.X - this.\u1716.X <= form.DesktopBounds.Right)
							{
								num = 6;
								continue;
							}
							goto IL_160;
						}
						case 3:
						{
							Form form;
							Point mousePosition2;
							if (mousePosition2.X - this.\u1716.X < form.DesktopBounds.Left)
							{
								goto IL_160;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_AF;
							default:
								if (false)
								{
								}
								num = 4;
								continue;
							}
							break;
						}
						case 4:
						{
							Point mousePosition = Control.MousePosition;
							num = 2;
							continue;
						}
						case 5:
						{
							Form form;
							Point mousePosition3;
							if (mousePosition3.Y - this.\u1716.Y > form.DesktopBounds.Bottom)
							{
								num = 9;
								continue;
							}
							return;
						}
						case 6:
							if (true)
							{
							}
							goto IL_AF;
						case 7:
						{
							Form form = listView.FindForm();
							Point mousePosition2 = Control.MousePosition;
							num = 3;
							continue;
						}
						case 8:
						{
							Point mousePosition3 = Control.MousePosition;
							num = 5;
							continue;
						}
						case 9:
							goto IL_160;
						case 10:
						{
							Form form;
							if (mousePosition4.Y - this.\u1716.Y >= form.DesktopBounds.Top)
							{
								num = 8;
								continue;
							}
							goto IL_160;
						}
						}
						break;
						IL_AF:
						mousePosition4 = Control.MousePosition;
						num = 10;
						continue;
						IL_160:
						A_1.Action = DragAction.Cancel;
						num = 1;
					}
				}
				return;
			}
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00077D00 File Offset: 0x00076D00
		private void ᜀ(object A_0, EventArgs A_1)
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
			this.ᜀ();
		}

		// Token: 0x0400088E RID: 2190
		private sprᯅ ᜀ;

		// Token: 0x0400088F RID: 2191
		private sprᯅ ᜁ;

		// Token: 0x04000890 RID: 2192
		private ListView ᜂ;

		// Token: 0x04000891 RID: 2193
		private ListView ᜃ;

		// Token: 0x04000892 RID: 2194
		private sprᯅ ᜄ;

		// Token: 0x04000893 RID: 2195
		private ColumnHeader ᜅ;

		// Token: 0x04000894 RID: 2196
		private ColumnHeader ᜆ;

		// Token: 0x04000895 RID: 2197
		private string[] \u2460\u008C\u00A6\u008C;

		// Token: 0x04000896 RID: 2198
		private sprᯅ ᜇ;

		// Token: 0x04000897 RID: 2199
		private float[] \u2460\u009A\u0098\u0091;

		// Token: 0x04000898 RID: 2200
		private sprᯅ ᜈ;

		// Token: 0x04000899 RID: 2201
		private sprᯅ ᜉ;

		// Token: 0x0400089A RID: 2202
		private sprᯅ ᜊ;

		// Token: 0x0400089B RID: 2203
		private sprᯅ ᜋ;

		// Token: 0x0400089C RID: 2204
		private ImageList ᜌ;

		// Token: 0x0400089E RID: 2206
		private long[] \u25D9\u00A0\u00ACª;

		// Token: 0x0400089F RID: 2207
		private ExportSource ᜎ;

		// Token: 0x040008A0 RID: 2208
		private IDbCommand ᜏ;

		// Token: 0x040008A1 RID: 2209
		private DataTable ᜐ;

		// Token: 0x040008A2 RID: 2210
		private ListView ᜑ;

		// Token: 0x040008A3 RID: 2211
		private StringListCollection \u1712;

		// Token: 0x040008A4 RID: 2212
		private ListView \u1713;

		// Token: 0x040008A5 RID: 2213
		private int \u1714;

		// Token: 0x040008A6 RID: 2214
		private Rectangle \u1715 = Rectangle.Empty;

		// Token: 0x040008A7 RID: 2215
		private int \u25D8\u0086\u00A7\u0097;

		// Token: 0x040008A8 RID: 2216
		private Point \u1716 = Point.Empty;
	}
}
