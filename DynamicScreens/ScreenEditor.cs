using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using AutoComboBox;
using AutoComboBox.MyControls;
using AutoComboBox.MyControls.CustomTableControls;
using DevComponents.DotNetBar;
using DynamicScreens.AdminTools;
using DynamicScreens.DynamicControlWrappers;
using DynamicScreens.DynamicControlWrappers.TypeConverters;
using DynamicScreens.Properties;
using EncryptionClassLibrary;
using UnivOleDb;

namespace DynamicScreens
{
	// Token: 0x02000013 RID: 19
	public partial class ScreenEditor : Form
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000FF RID: 255 RVA: 0x00008698 File Offset: 0x00007698
		// (remove) Token: 0x06000100 RID: 256 RVA: 0x000086D4 File Offset: 0x000076D4
		public event CompileFormCodeBehindHandler OnFormCodeBehindCompileRequest;

		// Token: 0x06000101 RID: 257 RVA: 0x00008710 File Offset: 0x00007710
		public static void ShowListEdit(int listGroupId, string listGroupDescription, UnivDataAdapter da)
		{
			LookupListEdit lookupListEdit = new LookupListEdit(da, listGroupId);
			LookupListEdit lookupListEdit2 = lookupListEdit;
			lookupListEdit2.Text = lookupListEdit2.Text + " [" + listGroupDescription + "]";
			lookupListEdit.ShowDialog();
			lookupListEdit.Dispose();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00008754 File Offset: 0x00007754
		private static int GetTypeCode(int screenNum, UnivDataAdapter da)
		{
			da.SelectCommand.CommandText = "SELECT typecode FROM screens WHERE screennum=" + screenNum.ToString();
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			int result;
			if (dataTable.Rows.Count > 0)
			{
				result = (int)dataTable.Rows[0][0];
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000087C4 File Offset: 0x000077C4
		public ScreenEditor(UnivDataAdapter da, int screenNum, TripleDESEncryptionClass tripleDES, ShowListEditDialog showListEditDialog)
		{
			DynamicControlWrapper_Base.ShowExtendedAccommodationInfo = (screenNum == 4);
			int typeCode = ScreenEditor.GetTypeCode(screenNum, da);
			DynamicControlWrapper_Base.ShowPerAppointmentInfo = (typeCode == 1);
			this.da = da;
			this.tripleDES = tripleDES;
			if (showListEditDialog == null)
			{
				this.showListEditDialog = new ShowListEditDialog(ScreenEditor.ShowListEdit);
			}
			else
			{
				this.showListEditDialog = showListEditDialog;
			}
			this.InitializeComponent();
			this.LoadScreenInfo(screenNum);
			this.Text = "ClockWork Screen Editor - " + this.screenInfo.description;
			da.SelectCommand.CommandText = "SELECT groupid,description FROM groups ORDER BY ordernum";
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			HE_GlobalVars_ClockWorkGroupList.groupsTable = dataTable;
			this.dynamicControlControls = new List<DynamicControlControl>();
			this.dynamicControlControls.Add(new DynamicControlControl(1));
			this.dynamicControlControls.Add(new DynamicControlControl(2));
			this.dynamicControlControls.Add(new DynamicControlControl(5));
			this.dynamicControlControls.Add(new DynamicControlControl(3));
			this.dynamicControlControls.Add(new DynamicControlControl(9));
			this.dynamicControlControls.Add(new DynamicControlControl(50));
			this.dynamicControlControls.Add(new DynamicControlControl(3));
			this.dynamicControlControls.Add(new DynamicControlControl(20));
			this.dynamicControlControls.Add(new DynamicControlControl(30));
			this.dynamicControlControls.Add(new DynamicControlControl(21));
			this.dynamicControlControls.Add(new DynamicControlControl(14));
			this.dynamicControlControls.Add(new DynamicControlControl(32));
			this.dynamicControlControls.Add(new DynamicControlControl(33));
			this.dynamicControlControls.Add(new DynamicControlControl(10));
			this.dynamicControlControls.Add(new DynamicControlControl(8));
			this.dynamicControlControls.Add(new DynamicControlControl(6));
			this.dynamicControlControls.Add(new DynamicControlControl(100));
			this.dynamicControlControls.Add(new DynamicControlControl(300));
			this.dynamicControlControls.Add(new DynamicControlControl(500));
			this.dynamicControlControls.Add(new DynamicControlControl(600));
			this.dynamicControlControls.Add(new DynamicControlControl(520));
			this.dynamicControlControls.Add(new DynamicControlControl(510));
			this.dynamicControlControls.Add(new DynamicControlControl(530));
			this.dynamicControlControls.Add(new DynamicControlControl(620));
			this.dynamicControlControls.Add(new DynamicControlControl(25));
			this.dynamicControlControls.Add(new DynamicControlControl(700));
			this.dynamicControlControls.Add(new DynamicControlControl(702));
			this.dynamicControlControls.Add(new DynamicControlControl(703));
			this.dynamicControlControls.Add(new DynamicControlControl(701));
			this.dynamicControlControls.Add(new DynamicControlControl(301));
			this.dynamicControlControls.Add(new DynamicControlControl(800));
			this.dynamicControlControls.Add(new DynamicControlControl(801));
			this.dynamicControlControls.Add(new DynamicControlControl(802));
			this.dynamicControlControls.Add(new DynamicControlControl(803));
			this.dynamicControlControls.Add(new DynamicControlControl(804));
			this.dynamicControlControls.Add(new DynamicControlControl(805));
			this.dynamicControlControls.Add(new DynamicControlControl(806));
			this.dynamicControlControls.Add(new DynamicControlControl(807));
			this.dynamicControlControls.Add(new DynamicControlControl(808));
			this.btn_textbox.Tag = this.FindDynamicControlControl(1);
			this.btn_checkbox.Tag = this.FindDynamicControlControl(2);
			this.btn_listSelectItem.Tag = this.FindDynamicControlControl(301);
			this.btn_label.Tag = this.FindDynamicControlControl(5);
			this.btn_dropList.Tag = this.FindDynamicControlControl(3);
			this.btn_blankSpace.Tag = this.FindDynamicControlControl(9);
			this.btn_columnBreak.Tag = this.FindDynamicControlControl(50);
			this.btn_dropList.Tag = this.FindDynamicControlControl(3);
			this.btn_fileList.Tag = this.FindDynamicControlControl(20);
			this.btn_groupBox.Tag = this.FindDynamicControlControl(30);
			this.btn_picture.Tag = this.FindDynamicControlControl(21);
			this.btn_radioButtonGroup.Tag = this.FindDynamicControlControl(14);
			this.btn_tabControl.Tag = this.FindDynamicControlControl(32);
			this.btn_table.Tag = this.FindDynamicControlControl(10);
			this.btn_date.Tag = this.FindDynamicControlControl(6);
			this.btn_hrule.Tag = this.FindDynamicControlControl(8);
			this.btn_tabPage.Tag = this.FindDynamicControlControl(33);
			this.btn_staffDropList.Tag = this.FindDynamicControlControl(100);
			this.btn_multiCheckbox.Tag = this.FindDynamicControlControl(500);
			this.multiCheckboxWithDroplistToolStripMenuItem.Tag = this.FindDynamicControlControl(520);
			this.multiCheckboxWithTextboxToolStripMenuItem.Tag = this.FindDynamicControlControl(510);
			this.btn_richTextBox.Tag = this.FindDynamicControlControl(600);
			this.btn_multiCheckHeader.Tag = this.FindDynamicControlControl(530);
			this.btn_multiLineTextbox.Tag = this.FindDynamicControlControl(620);
			this.btn_infoBox.Tag = this.FindDynamicControlControl(803);
			this.btn_dynamicTable.Tag = this.FindDynamicControlControl(25);
			this.btn_multiItemDbChooser.Tag = this.FindDynamicControlControl(802);
			this.btn_accommodationCheckbox.Tag = this.FindDynamicControlControl(700);
			this.btn_accommodationDatePicker.Tag = this.FindDynamicControlControl(702);
			this.btn_accommodationDropList.Tag = this.FindDynamicControlControl(703);
			this.btn_accommodationTextbox.Tag = this.FindDynamicControlControl(701);
			this.btn_perStudentForm.Tag = this.FindDynamicControlControl(800);
			this.btn_dynamicControlsChooser.Tag = this.FindDynamicControlControl(801);
			this.btn_calcButton.Tag = this.FindDynamicControlControl(804);
			this.btn_caseList.Tag = this.FindDynamicControlControl(805);
			this.btn_caseComboBox.Tag = this.FindDynamicControlControl(806);
			this.btn_emailHistory.Tag = this.FindDynamicControlControl(807);
			this.btn_appHistory.Tag = this.FindDynamicControlControl(808);
			this.deletedDynamicControls = new List<DynamicControl>();
			this.helperClass = new DynamicControlWrapper_HelperClass(da);
			this.icon = new NodeStateIcon();
			this._model = new TreeModel();
			this.tv_design.Model = this._model;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00008F48 File Offset: 0x00007F48
		private void LoadScreenInfo(int screenNum)
		{
			this.da.SelectCommand.CommandText = "SELECT screennum,description,typecode,bottomless,verticalcontrolpad,columnwidth,columnpad,dateadded,datemodified,isactive,iconindex,largeiconindex,shorttext,studentnamenumeditable,screenid,showasbutton,fontname,fontsize,groupids,iswebscreen,longdescription,controlidtoactivate FROM screens WHERE screennum=@screennum";
			this.da.SelectCommand.Parameters.Clear();
			this.da.SelectCommand.Parameters.Add("@screennum", screenNum);
			DataTable dataTable = new DataTable();
			this.da.Fill(dataTable);
			DataRow dataRow = (dataTable.Rows.Count > 0) ? dataTable.Rows[0] : null;
			if (dataRow != null)
			{
				this.screenInfo = ScreenInfo.GetScreenInfo(dataRow, this.p_data, true, false, Color.Empty, Color.Empty);
			}
			else
			{
				this.screenInfo = new ScreenInfo(screenNum, this.p_data, true, 0, this.p_data.Width, 0, this.Font, -1, "", false, false, Color.Transparent, Color.Transparent);
				this.screenInfo.WidthPercent = 0.95;
			}
			this.screenTypeCode = ((dataRow != null) ? ((int)dataRow["typecode"]) : 0);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00009069 File Offset: 0x00008069
		public void SetupForNoSaving()
		{
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00009070 File Offset: 0x00008070
		// (set) Token: 0x06000107 RID: 263 RVA: 0x0000908C File Offset: 0x0000808C
		public string XmlDefinition
		{
			get
			{
				return this.GetXmlDefinition();
			}
			set
			{
				try
				{
					string text = value.Trim();
					DataTable t;
					if (text.Length > 0)
					{
						DataSet dataSet = new DataSet();
						StringReader stringReader = new StringReader(text);
						dataSet.ReadXml(stringReader, XmlReadMode.ReadSchema);
						stringReader.Close();
						if (dataSet.Tables.Count > 0)
						{
							t = dataSet.Tables[0];
						}
						else
						{
							t = new DataTable();
						}
					}
					else
					{
						t = new DataTable();
					}
					this.LoadControls(t);
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00009130 File Offset: 0x00008130
		private void toolStripButton1_MouseDown(object sender, MouseEventArgs e)
		{
			if (sender is ToolStripButton)
			{
				ToolStripButton toolStripButton = (ToolStripButton)sender;
				if (toolStripButton.Tag != null)
				{
					DynamicControlControl dynamicControlControl = (DynamicControlControl)toolStripButton.Tag;
					bool flag = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
					bool flag2 = (Control.ModifierKeys & Keys.Control) == Keys.Control;
					if (flag)
					{
						this.QuickAdd(dynamicControlControl);
					}
					else if (flag2)
					{
						Form form = new Form();
						form.WindowState = FormWindowState.Maximized;
						Control control = null;
						int controlCode = dynamicControlControl.ControlCode;
						if (controlCode == 600)
						{
							control = new MyRichText();
						}
						if (control != null)
						{
							form.Controls.Add(control);
							control.Location = new Point(0, 0);
							control.Width = 200;
							control.Height = 200;
						}
						DialogResult dialogResult = form.ShowDialog(this);
					}
					else
					{
						base.DoDragDrop(dynamicControlControl, DragDropEffects.Copy);
					}
				}
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00009251 File Offset: 0x00008251
		private void ScreenEditor_Load(object sender, EventArgs e)
		{
			this._model.NodesChanged += this._model_NodesChanged;
			this.LoadControls();
			this.RefreshPanelSize("");
			this.ListsToScreen();
			this.LoadExistingControls();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00009290 File Offset: 0x00008290
		private void LoadExistingControls()
		{
			DataTable dataTable = DynamicScreen.LoadDynamicControlsTable2(this.da, 0, true);
			this.treeView_existingControls.BeginUpdate();
			int num = -1;
			TreeNodeCollection nodes = this.treeView_existingControls.Nodes;
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DynamicControl dynamicControl = new DynamicControl(dataRow);
				int num2 = (int)dataRow["screennum"];
				if (num2 != num)
				{
					TreeNode treeNode = this.treeView_existingControls.Nodes.Add(dataRow["description"].ToString());
					treeNode.ImageIndex = -1;
					nodes = treeNode.Nodes;
					num = num2;
				}
				TreeNode treeNode2 = nodes.Add(dynamicControl.ControlCaption);
				treeNode2.Tag = dynamicControl;
				int imageIndex = this.GetImageIndex(dynamicControl.ControlCode);
				treeNode2.ImageIndex = imageIndex;
				treeNode2.SelectedImageIndex = imageIndex;
			}
			this.treeView_existingControls.CollapseAll();
			this.treeView_existingControls.EndUpdate();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000093DC File Offset: 0x000083DC
		private void _model_NodesChanged(object sender, TreeModelEventArgs e)
		{
			foreach (MyIconNode myIconNode in e.Children)
			{
				myIconNode.DynamicControl.ControlCaption = myIconNode.Text;
			}
			this.propertyGrid1.Refresh();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00009430 File Offset: 0x00008430
		private DynamicControlControl FindDynamicControlControl(int controlCode)
		{
			foreach (DynamicControlControl dynamicControlControl in this.dynamicControlControls)
			{
				if (dynamicControlControl.ControlCode == controlCode)
				{
					return dynamicControlControl;
				}
			}
			return new DynamicControlControl(0);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000094A4 File Offset: 0x000084A4
		private MyIconNode AddNewNode2(DynamicControl dc, DynamicControlControl dcc, MyIconNode parentNode)
		{
			MyIconNode myIconNode = new MyIconNode(dc.ControlCaption, dc);
			myIconNode.IsChecked = true;
			myIconNode.Icon = this.GetImage(dcc);
			if (parentNode == null)
			{
				this._model.Nodes.Add(myIconNode);
			}
			else
			{
				parentNode.Nodes.Add(myIconNode);
			}
			return myIconNode;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00009508 File Offset: 0x00008508
		private MyIconNode AddNewNode(DynamicControl dc, DynamicControlControl dcc, Stack parentMyIconNodes)
		{
			MyIconNode parentNode = (parentMyIconNodes.Count < 1) ? null : ((MyIconNode)parentMyIconNodes.Peek());
			return this.AddNewNode2(dc, dcc, parentNode);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000953C File Offset: 0x0000853C
		private void LoadControls()
		{
			DataTable dataTable = DynamicScreen.LoadDynamicControlsTableWithExtendedAccommInfo(this.da, this.screenInfo.screenNum);
			if (this.screenInfo.screenNum <= 0)
			{
				dataTable.Rows.Clear();
			}
			this.LoadControls(dataTable);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00009588 File Offset: 0x00008588
		private void LoadControls(DataTable t)
		{
			Stack stack = new Stack();
			foreach (object obj in t.Rows)
			{
				DataRow dr = (DataRow)obj;
				DynamicControl dynamicControl = new DynamicControl(dr);
				dynamicControl.Tag = this.helperClass;
				DynamicControlControl dcc = this.FindDynamicControlControl(dynamicControl.ControlCode);
				if (dynamicControl.ControlCode == 30)
				{
					stack.Push(this.AddNewNode(dynamicControl, dcc, stack));
				}
				else if (dynamicControl.ControlCode == 32)
				{
					stack.Push(this.AddNewNode(dynamicControl, dcc, stack));
				}
				else if (dynamicControl.ControlCode == 33)
				{
					if (stack.Count > 0)
					{
						MyIconNode myIconNode = (MyIconNode)stack.Peek();
						if (myIconNode.DynamicControl.ControlCode == 33)
						{
							myIconNode = (MyIconNode)stack.Pop();
						}
					}
					stack.Push(this.AddNewNode(dynamicControl, dcc, stack));
				}
				else if (dynamicControl.ControlCode == 31)
				{
					MyIconNode myIconNode = (MyIconNode)stack.Pop();
					myIconNode.DynamicControl.AssociatedDynamicControl = dynamicControl;
				}
				else if (dynamicControl.ControlCode == 35)
				{
					MyIconNode myIconNode = (MyIconNode)stack.Pop();
					if (myIconNode.DynamicControl.ControlCode == 33)
					{
						stack.Pop();
					}
					myIconNode.DynamicControl.AssociatedDynamicControl = dynamicControl;
				}
				else
				{
					this.AddNewNode(dynamicControl, dcc, stack);
				}
			}
			this.tv_design.ExpandAll();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000978C File Offset: 0x0000878C
		private Image GetImage(int controlCode)
		{
			int imageIndex = this.GetImageIndex(controlCode);
			Image result;
			if (imageIndex >= 0)
			{
				result = this.imageList2.Images[imageIndex];
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000097C4 File Offset: 0x000087C4
		private Image GetImage(DynamicControlControl dcc)
		{
			int controlCode = dcc.ControlCode;
			return this.GetImage(controlCode);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000097E4 File Offset: 0x000087E4
		private int GetImageIndex(int controlCode)
		{
			if (controlCode <= 301)
			{
				if (controlCode <= 33)
				{
					switch (controlCode)
					{
					case 1:
						goto IL_135;
					case 2:
						goto IL_13D;
					case 3:
						goto IL_145;
					case 4:
					case 7:
					case 11:
					case 12:
					case 13:
					case 15:
					case 16:
					case 17:
					case 18:
					case 19:
						goto IL_1A1;
					case 5:
						return 2;
					case 6:
						goto IL_160;
					case 8:
						return 14;
					case 9:
						return 11;
					case 10:
						break;
					case 14:
						return 4;
					case 20:
						return 20;
					case 21:
						return 6;
					default:
						if (controlCode != 25)
						{
							switch (controlCode)
							{
							case 30:
								return 15;
							case 31:
								goto IL_1A1;
							case 32:
								return 12;
							case 33:
								return 16;
							default:
								goto IL_1A1;
							}
						}
						break;
					}
					return 7;
				}
				if (controlCode == 50)
				{
					return 10;
				}
				if (controlCode == 100)
				{
					return 19;
				}
				switch (controlCode)
				{
				case 300:
					return 17;
				case 301:
					goto IL_13D;
				default:
					goto IL_1A1;
				}
			}
			else if (controlCode <= 520)
			{
				if (controlCode == 500)
				{
					return 21;
				}
				if (controlCode == 510)
				{
					return 22;
				}
				if (controlCode != 520)
				{
					goto IL_1A1;
				}
				return 23;
			}
			else if (controlCode <= 620)
			{
				if (controlCode == 600)
				{
					return 18;
				}
				if (controlCode != 620)
				{
					goto IL_1A1;
				}
				return 24;
			}
			else
			{
				switch (controlCode)
				{
				case 700:
					goto IL_13D;
				case 701:
					break;
				case 702:
					goto IL_160;
				case 703:
					goto IL_145;
				default:
					if (controlCode != 800)
					{
						goto IL_1A1;
					}
					return 25;
				}
			}
			IL_135:
			return 1;
			IL_13D:
			return 3;
			IL_145:
			return 5;
			IL_160:
			return 13;
			IL_1A1:
			return -1;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00009998 File Offset: 0x00008998
		private TreeNodeAdv[] CreateNewNodeAndAddToModel(DynamicControlControl dcc)
		{
			return this.CreateNewNodeAndAddToModel(dcc, null);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000099B4 File Offset: 0x000089B4
		private TreeNodeAdv[] CreateNewNodeAndAddToModel(DynamicControlControl dcc, string defaultCaption)
		{
			int control_id = this.newCid--;
			DynamicControl dynamicControl = new DynamicControl(control_id, (defaultCaption == null) ? (dcc.Title + control_id.ToString()) : defaultCaption, dcc.ControlCode, 0, 0, 0, 0);
			dynamicControl.ControlName = "";
			DynamicControlWrapper_Base dynamicControlWrapper_Base = DynamicControlWrapper_Base.CreateWrapper(dynamicControl);
			dynamicControlWrapper_Base.SetDefaultValues(dynamicControl);
			dynamicControl.Tag = this.helperClass;
			MyIconNode myIconNode;
			if (dcc.ControlCode == 30)
			{
				dynamicControl.AssociatedDynamicControl = new DynamicControl(this.newCid--, dcc.Title + control_id.ToString(), 31, 0, 0, 0, 0);
				dynamicControlWrapper_Base.SetDefaultValues(dynamicControl.AssociatedDynamicControl);
				string controlCaption = dynamicControl.ControlCaption;
				myIconNode = new MyIconNode(controlCaption, dynamicControl);
			}
			else if (dcc.ControlCode == 32)
			{
				dynamicControl.AssociatedDynamicControl = new DynamicControl(this.newCid--, dcc.Title + control_id.ToString(), 35, 0, 0, 0, 0);
				dynamicControlWrapper_Base.SetDefaultValues(dynamicControl.AssociatedDynamicControl);
				myIconNode = new MyIconNode(dynamicControl.ControlCaption, dynamicControl);
			}
			else
			{
				myIconNode = new MyIconNode(dynamicControl.ControlCaption, dynamicControl);
			}
			myIconNode.IsChecked = true;
			myIconNode.Icon = this.GetImage(dcc);
			this._model.Nodes.Add(myIconNode);
			TreeNodeAdv treeNodeAdv = this.tv_design.FindNode(this._model.GetPath(myIconNode));
			return new TreeNodeAdv[]
			{
				treeNodeAdv
			};
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00009B68 File Offset: 0x00008B68
		private void treeViewAdv1_DragDrop(object sender, DragEventArgs e)
		{
			TreeNodeAdv[] array;
			if (e.Data.GetDataPresent("DynamicScreens.DynamicControlControl", false))
			{
				DynamicControlControl dcc = (DynamicControlControl)e.Data.GetData("DynamicScreens.DynamicControlControl", false);
				array = this.CreateNewNodeAndAddToModel(dcc);
			}
			else if (e.Data.GetDataPresent("Aga.Controls.Tree.TreeNodeAdv[]", false))
			{
				array = (TreeNodeAdv[])e.Data.GetData("Aga.Controls.Tree.TreeNodeAdv[]", false);
			}
			else if (e.Data.GetDataPresent("System.Collections.ArrayList", false))
			{
				ArrayList arrayList = (ArrayList)e.Data.GetData("System.Collections.ArrayList");
				foreach (object obj in arrayList)
				{
					DynamicControl dynamicControl = (DynamicControl)obj;
					DynamicControlControl dcc = this.FindDynamicControlControl(dynamicControl.ControlCode);
					if (this.tv_design.DropPosition.Node != null)
					{
						MyIconNode myIconNode = (MyIconNode)this._model.FindNode(this.tv_design.GetPath(this.tv_design.DropPosition.Node));
						DynamicControl dynamicControl2 = myIconNode.DynamicControl;
						if (dynamicControl2.ControlCode == 30)
						{
							this.AddNewNode2(dynamicControl, dcc, myIconNode);
						}
						else if (this.tv_design.DropPosition.Node.Parent != null)
						{
							myIconNode = (MyIconNode)this._model.FindNode(this.tv_design.GetPath(this.tv_design.DropPosition.Node.Parent));
							this.AddNewNode2(dynamicControl, dcc, myIconNode);
						}
						else
						{
							this.AddNewNode2(dynamicControl, dcc, null);
						}
					}
					else
					{
						this.AddNewNode2(dynamicControl, dcc, null);
					}
				}
				array = null;
			}
			else
			{
				array = null;
			}
			if (array != null)
			{
				MyIconNode myIconNode2 = (this.tv_design.DropPosition.Node == null) ? null : (this.tv_design.DropPosition.Node.Tag as MyIconNode);
				if (myIconNode2 != null && array != null)
				{
					foreach (TreeNodeAdv treeNodeAdv in array)
					{
						MyIconNode myIconNode3 = (MyIconNode)this._model.FindNode(this.tv_design.GetPath(treeNodeAdv));
						if (myIconNode3.DynamicControl.ControlId == myIconNode2.DynamicControl.ControlId)
						{
							return;
						}
					}
				}
				NodePosition nodePos = (myIconNode2 == null) ? 1 : this.tv_design.DropPosition.Position;
				this.PositionNode(array, myIconNode2, nodePos);
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00009E8C File Offset: 0x00008E8C
		private void PositionNode(TreeNodeAdv[] nodes, MyIconNode dropNode, NodePosition nodePos)
		{
			bool flag = false;
			if (nodePos == 0)
			{
				if (dropNode.DynamicControl.ControlCode != 30 && dropNode.DynamicControl.ControlCode != 32 && dropNode.DynamicControl.ControlCode != 33)
				{
					nodePos = 2;
				}
				else
				{
					foreach (TreeNodeAdv treeNodeAdv in nodes)
					{
						(treeNodeAdv.Tag as Node).Parent = dropNode;
					}
					TreeNodeAdv treeNodeAdv2 = this.tv_design.FindNode(this._model.GetPath(dropNode));
					treeNodeAdv2.IsExpanded = true;
					flag = true;
				}
			}
			if (!flag)
			{
				Collection<Node> collection = (dropNode == null || dropNode.Parent == null) ? this._model.Nodes : dropNode.Parent.Nodes;
				foreach (TreeNodeAdv treeNodeAdv3 in nodes)
				{
					(treeNodeAdv3.Tag as Node).Parent = null;
				}
				int num;
				if (nodePos == 2)
				{
					num = collection.IndexOf(dropNode) + 1;
				}
				else
				{
					num = collection.IndexOf(dropNode);
				}
				foreach (TreeNodeAdv treeNodeAdv3 in nodes)
				{
					Node item = treeNodeAdv3.Tag as Node;
					if (num == -1)
					{
						collection.Add(item);
					}
					else
					{
						collection.Insert(num, item);
						num++;
					}
				}
			}
			if (nodes.Length > 0)
			{
				this.tv_design.ClearSelection();
				foreach (TreeNodeAdv treeNodeAdv4 in nodes)
				{
					Node node = treeNodeAdv4.Tag as Node;
					treeNodeAdv4 = this.tv_design.FindNode(this._model.GetPath(node));
					treeNodeAdv4.IsSelected = true;
				}
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000A0A8 File Offset: 0x000090A8
		private void tv_design_ItemDrag(object sender, ItemDragEventArgs e)
		{
			TreeNodeAdv[] array = new TreeNodeAdv[this.tv_design.SelectedNodes.Count];
			this.tv_design.SelectedNodes.CopyTo(array, 0);
			base.DoDragDrop(array, DragDropEffects.Copy);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000A0E8 File Offset: 0x000090E8
		private void tv_design_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent("DynamicScreens.DynamicControlControl", false))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else if (e.Data.GetDataPresent("Aga.Controls.Tree.TreeNodeAdv[]", false))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else if (e.Data.GetDataPresent("System.Collections.ArrayList", false))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000A160 File Offset: 0x00009160
		private void button1_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000A164 File Offset: 0x00009164
		private void tv_design_SelectionChanged(object sender, EventArgs e)
		{
			TreeNodeAdv[] array = new TreeNodeAdv[this.tv_design.SelectedNodes.Count];
			this.tv_design.SelectedNodes.CopyTo(array, 0);
			if (array.Length == 1)
			{
				DynamicControl dynamicControl = this.GetDynamicControl(array[0]);
				this.propertyGrid1.SelectedObject = DynamicControlWrapper_Base.CreateWrapper(dynamicControl);
			}
			else if (array.Length > 1)
			{
				object[] array2 = new object[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					DynamicControl dynamicControl = this.GetDynamicControl(array[i]);
					array2[i] = DynamicControlWrapper_Base.CreateWrapper(dynamicControl);
				}
				this.propertyGrid1.SelectedObjects = array2;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000A21C File Offset: 0x0000921C
		private DynamicControl GetDynamicControl(TreeNodeAdv nodeAdv)
		{
			MyIconNode myIconNode = (MyIconNode)this._model.FindNode(this.tv_design.GetPath(nodeAdv));
			return (myIconNode == null) ? null : myIconNode.DynamicControl;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000A258 File Offset: 0x00009258
		private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			bool flag = e.ChangedItem != null && e.ChangedItem.Label.CompareTo("ControlCaption") == 0;
			foreach (object obj in this.propertyGrid1.SelectedObjects)
			{
				DynamicControlWrapper_Base dynamicControlWrapper_Base = (DynamicControlWrapper_Base)obj;
				DynamicControl dynamicControl = dynamicControlWrapper_Base.dynamicControl;
				if (flag && dynamicControl.ControlName.Length < 1)
				{
					dynamicControl.ControlName = this.GetControlNameFromCaption(dynamicControl.ControlCaption);
				}
				this.UpdateTreeNodes(this._model.Nodes, dynamicControl);
			}
			this.propertyGrid1.Refresh();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000A328 File Offset: 0x00009328
		private string ExtractCaption(string name)
		{
			string text = "";
			foreach (char c in name)
			{
				if (text.Length > 0)
				{
					if (char.IsUpper(c))
					{
						char c2 = char.ToLower(c);
						text = text + " " + c2;
					}
					else
					{
						text += c;
					}
				}
				else
				{
					text += c;
				}
			}
			return text;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000A3D0 File Offset: 0x000093D0
		private void UpdateTreeNodes(Collection<Node> nodes, DynamicControl dc)
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				MyIconNode myIconNode = (MyIconNode)nodes[i];
				if (myIconNode.DynamicControl.ControlId == dc.ControlId)
				{
					myIconNode.Text = dc.ControlCaption;
					myIconNode.CheckState = (dc.Enabled ? CheckState.Checked : CheckState.Unchecked);
					if (dc.IsLabel)
					{
						if (i == 0 && myIconNode.Parent != null && myIconNode.Parent is MyIconNode)
						{
							MyIconNode myIconNode2 = (MyIconNode)myIconNode.Parent;
							if (myIconNode2.DynamicControl.ControlCode == 30)
							{
								myIconNode2.DynamicControl.ControlCaption = dc.ControlCaption + "_GROUP";
								myIconNode2.DynamicControl.ControlName = "lbl_" + myIconNode2.DynamicControl.ControlCaption;
								myIconNode2.Text = myIconNode2.DynamicControl.ControlCaption;
							}
						}
					}
				}
				if (myIconNode.Nodes.Count > 0)
				{
					this.UpdateTreeNodes(myIconNode.Nodes, dc);
				}
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000A514 File Offset: 0x00009514
		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.tabControl1.SelectedTab == this.tp_preview)
			{
				this.screenInfo.UpdateWidthHasChanged();
				this.RefreshPreview();
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000A554 File Offset: 0x00009554
		private string GetXmlDefinition()
		{
			string result;
			if (this._model.Nodes.Count > 0)
			{
				DynamicControlCollection dynamicControlCollection = new DynamicControlCollection();
				DataTable table = DynamicControl.CreateControlsTable();
				this.AddNodesToControlsTable(ref table, this._model.Nodes);
				DataSet dataSet = new DataSet();
				dataSet.Tables.Add(table);
				StringWriter stringWriter = new StringWriter();
				dataSet.WriteXml(stringWriter, XmlWriteMode.WriteSchema);
				string text = stringWriter.ToString();
				stringWriter.Close();
				dataSet.Clear();
				result = text;
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000A5E8 File Offset: 0x000095E8
		private void RefreshPreview()
		{
			if (this.p_data.Controls.Count > 0)
			{
				this.p_data.Controls.Clear();
			}
			if (this._model.Nodes.Count > 0)
			{
				DynamicControlCollection dynamicControlCollection = new DynamicControlCollection();
				DataTable controlListTable = DynamicControl.CreateControlsTable();
				this.AddNodesToControlsTable(ref controlListTable, this._model.Nodes);
				DataSet dataSet = new DataSet();
				DynamicScreen.TranslateControls(this.da, this.tripleDES, ref this.p_data, this.screenInfo, controlListTable, ref dataSet, null, new DataSet(), new ArrayList(), 1);
				this.p_data.AutoScroll = true;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000A6A0 File Offset: 0x000096A0
		private void AddNodesToControlsTable(ref DataTable t, Collection<Node> nodes)
		{
			foreach (Node node in nodes)
			{
				MyIconNode myIconNode = (MyIconNode)node;
				DynamicControl dynamicControl = myIconNode.DynamicControl;
				dynamicControl.CreateRowAndAddToTable(ref t, this.screenInfo.screenNum);
				if (dynamicControl.ControlCode == 30)
				{
					this.AddNodesToControlsTable(ref t, myIconNode.Nodes);
				}
				else if (dynamicControl.ControlCode == 32)
				{
					this.AddNodesToControlsTable(ref t, myIconNode.Nodes);
				}
				else if (dynamicControl.ControlCode == 33)
				{
					this.AddNodesToControlsTable(ref t, myIconNode.Nodes);
				}
				if (dynamicControl.AssociatedDynamicControl != null)
				{
					dynamicControl.AssociatedDynamicControl.CreateRowAndAddToTable(ref t, this.screenInfo.screenNum);
				}
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000A7B0 File Offset: 0x000097B0
		private void bar1_ItemClick(object sender, EventArgs e)
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000A7B4 File Offset: 0x000097B4
		private void toolStripComboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			string text = this.toolStripComboBox1.Text;
			this.RefreshPanelSize(text);
			this.RefreshPreview();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000A7E0 File Offset: 0x000097E0
		private void RefreshPanelSize(string s)
		{
			if (s.Length < 1)
			{
				s = "1024x768";
			}
			string[] array = s.Split(new char[]
			{
				'x'
			});
			int width = int.Parse(array[0].Trim());
			int num = int.Parse(array[1].Trim());
			int num2 = 27;
			int num3 = 70;
			int num4 = 23;
			num -= num2 + num3 + num4;
			this.p_data.Width = width;
			this.p_data.Height = num;
			this.screenInfo.UpdateWidthHasChanged();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000A878 File Offset: 0x00009878
		private void ScreenEditor_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult != DialogResult.OK && this.AnyChanges(this._model.Nodes) && this.btn_save.Visible)
			{
				DialogResult dialogResult = MessageBox.Show("Would you like to save your changes?", "Changes will be lost", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
				else if (dialogResult == DialogResult.Yes)
				{
					this.SaveChanges(false);
				}
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000A8F4 File Offset: 0x000098F4
		private bool AnyChanges(Collection<Node> nodes)
		{
			foreach (Node node in nodes)
			{
				MyIconNode myIconNode = (MyIconNode)node;
				if (myIconNode.DynamicControl.HowModified != ModificationType.Unchanged)
				{
					return true;
				}
				if (myIconNode.Nodes.Count > 0)
				{
					bool flag = this.AnyChanges(myIconNode.Nodes);
					if (flag)
					{
						return true;
					}
				}
			}
			foreach (object obj in this.helperClass.ListGroups)
			{
				DynamicListGroup dynamicListGroup = (DynamicListGroup)obj;
				if (dynamicListGroup.HowModified != ModificationType.Unchanged)
				{
					return true;
				}
				foreach (object obj2 in dynamicListGroup)
				{
					DynamicListItem dynamicListItem = (DynamicListItem)obj2;
					if (dynamicListItem.HowModified != ModificationType.Unchanged)
					{
						return true;
					}
				}
			}
			return this.deletedDynamicControls.Count > 0;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000AA9C File Offset: 0x00009A9C
		private void SaveChanges(bool closeFormAfterwards)
		{
			List<DynamicControl> list = new List<DynamicControl>();
			this.NodesToDynamicControlsList(ref list, this._model.Nodes);
			foreach (DynamicControl dynamicControl in list)
			{
				if (dynamicControl.HowModified == ModificationType.Added)
				{
					this.da.SelectCommand.CommandText = "INSERT INTO dynamiccontrols (controlcode,controlcaption,setting1,setting2,setting3,defaultvalue,p,statsholding,controlname,controlgroup,helptext,helptextdisplaymethod,mask,enforce,actionhandlers,defaultvaluestring,setting4string,enabled,readonly,hidecaption,setting4,fontsize,dontwraptonextline,specialcontroltype) \r\nVALUES (@controlcode,@controlcaption,@setting1,@setting2,@setting3,@defaultvalue,@p,@statsholding,@controlname,@controlgroup,@helptext,@helptextdisplaymethod,@mask,@enforce,@actionhandlers,@defaultvaluestring,@setting4string,@enabled,@readonly,@hidecaption,@setting4,@fontsize,@dontwraptonextline,@specialcontroltype)";
					this.da.SelectCommand.Parameters.Clear();
					this.da.SelectCommand.Parameters.Add("@controlcode", dynamicControl.ControlCode);
					this.da.SelectCommand.Parameters.Add("@controlcaption", dynamicControl.ControlCaption);
					this.da.SelectCommand.Parameters.Add("@setting1", dynamicControl.Setting1);
					this.da.SelectCommand.Parameters.Add("@setting2", dynamicControl.Setting2);
					this.da.SelectCommand.Parameters.Add("@setting3", dynamicControl.Setting3);
					this.da.SelectCommand.Parameters.Add("@defaultvalue", dynamicControl.DefaultValue);
					this.da.SelectCommand.Parameters.Add("@p", "");
					this.da.SelectCommand.Parameters.Add("@statsholding", true);
					this.da.SelectCommand.Parameters.Add("@controlname", dynamicControl.ControlName);
					this.da.SelectCommand.Parameters.Add("@controlgroup", dynamicControl.ControlGroup);
					this.da.SelectCommand.Parameters.Add("@helptext", dynamicControl.HelpText);
					this.da.SelectCommand.Parameters.Add("@helptextdisplaymethod", dynamicControl.HelpTextDisplayMethod);
					this.da.SelectCommand.Parameters.Add("@mask", dynamicControl.Mask);
					this.da.SelectCommand.Parameters.Add("@enforce", dynamicControl.Enforce);
					this.da.SelectCommand.Parameters.Add("@actionhandlers", dynamicControl.ActionHandlers);
					this.da.SelectCommand.Parameters.Add("@defaultvaluestring", dynamicControl.DefaultValueString);
					this.da.SelectCommand.Parameters.Add("@setting4string", dynamicControl.Setting4String);
					this.da.SelectCommand.Parameters.Add("@enabled", dynamicControl.Enabled);
					this.da.SelectCommand.Parameters.Add("@readonly", dynamicControl.ReadOnly);
					this.da.SelectCommand.Parameters.Add("@hidecaption", dynamicControl.HideCaption);
					this.da.SelectCommand.Parameters.Add("@setting4", dynamicControl.Setting4);
					this.da.SelectCommand.Parameters.Add("@fontsize", dynamicControl.FontSize);
					this.da.SelectCommand.Parameters.Add("@dontwraptonextline", dynamicControl.DontWrapToNextLine);
					this.da.SelectCommand.Parameters.Add("@specialcontroltype", dynamicControl.SpecialControlType);
					DataTable dataTable = new DataTable();
					dynamicControl.ControlId = this.da.FillReturnIdentity(dataTable, "controlid", "dynamiccontrols");
					this.da.SelectCommand.CommandText = "INSERT INTO dynamicscreencontrols (screennum,controlid,ordernum,isactive,p,statsholding,controlgroup) VALUES (@screennum,@cid,0,'1','','1',@controlgroupoverride)";
					this.da.SelectCommand.Parameters.Clear();
					this.da.SelectCommand.Parameters.Add("@screennum", this.screenInfo.screenNum);
					this.da.SelectCommand.Parameters.Add("@cid", dynamicControl.ControlId);
					if (!string.IsNullOrEmpty(dynamicControl.ControlGroupOverride))
					{
						this.da.SelectCommand.Parameters.Add("@controlgroupoverride", dynamicControl.ControlGroupOverride);
					}
					else
					{
						this.da.SelectCommand.Parameters.AddNull("@controlgroupoverride", DbType.String);
					}
					this.da.Fill(new DataTable());
				}
				else if (dynamicControl.HowModified == ModificationType.CopiedFromAnotherScreen)
				{
					this.da.SelectCommand.CommandText = "INSERT INTO dynamicscreencontrols (screennum,controlid,ordernum,isactive,p,statsholding,controlgroup) VALUES (@screennum,@cid,0,'1','','1',@controlgroupoverride)";
					this.da.SelectCommand.Parameters.Clear();
					this.da.SelectCommand.Parameters.Add("@screennum", this.screenInfo.screenNum);
					this.da.SelectCommand.Parameters.Add("@cid", dynamicControl.ControlId);
					if (!string.IsNullOrEmpty(dynamicControl.ControlGroupOverride))
					{
						this.da.SelectCommand.Parameters.Add("@controlgroupoverride", dynamicControl.ControlGroupOverride);
					}
					else
					{
						this.da.SelectCommand.Parameters.AddNull("@controlgroupoverride", DbType.String);
					}
					this.da.Fill(new DataTable());
				}
				else if (dynamicControl.HowModified == ModificationType.Modified)
				{
					this.da.SelectCommand.CommandText = "UPDATE dynamiccontrols SET controlcode=@controlcode,controlcaption=@controlcaption,setting1=@setting1,setting2=@setting2,setting3=@setting3,defaultvalue=@defaultvalue,p=@p,statsholding=@statsholding,controlname=@controlname,controlgroup=@controlgroup,helptext=@helptext,helptextdisplaymethod=@helptextdisplaymethod,mask=@mask,enforce=@enforce,actionhandlers=@actionhandlers,defaultvaluestring=@defaultvaluestring,setting4string=@setting4string,enabled=@enabled,readonly=@readonly,hidecaption=@hidecaption,setting4=@setting4,fontsize=@fontsize,dontwraptonextline=@dontwraptonextline,specialcontroltype=@specialcontroltype WHERE controlid=@cid";
					this.da.SelectCommand.Parameters.Clear();
					this.da.SelectCommand.Parameters.Add("@cid", dynamicControl.ControlId);
					this.da.SelectCommand.Parameters.Add("@controlcode", dynamicControl.ControlCode);
					this.da.SelectCommand.Parameters.Add("@controlcaption", dynamicControl.ControlCaption);
					this.da.SelectCommand.Parameters.Add("@setting1", dynamicControl.Setting1);
					this.da.SelectCommand.Parameters.Add("@setting2", dynamicControl.Setting2);
					this.da.SelectCommand.Parameters.Add("@setting3", dynamicControl.Setting3);
					this.da.SelectCommand.Parameters.Add("@defaultvalue", dynamicControl.DefaultValue);
					this.da.SelectCommand.Parameters.Add("@p", "");
					this.da.SelectCommand.Parameters.Add("@statsholding", true);
					this.da.SelectCommand.Parameters.Add("@controlname", dynamicControl.ControlName);
					this.da.SelectCommand.Parameters.Add("@controlgroup", dynamicControl.ControlGroup);
					this.da.SelectCommand.Parameters.Add("@helptext", dynamicControl.HelpText);
					this.da.SelectCommand.Parameters.Add("@helptextdisplaymethod", dynamicControl.HelpTextDisplayMethod);
					this.da.SelectCommand.Parameters.Add("@mask", dynamicControl.Mask);
					this.da.SelectCommand.Parameters.Add("@enforce", dynamicControl.Enforce);
					this.da.SelectCommand.Parameters.Add("@actionhandlers", dynamicControl.ActionHandlers);
					this.da.SelectCommand.Parameters.Add("@defaultvaluestring", dynamicControl.DefaultValueString);
					this.da.SelectCommand.Parameters.Add("@setting4string", dynamicControl.Setting4String);
					this.da.SelectCommand.Parameters.Add("@enabled", dynamicControl.Enabled);
					this.da.SelectCommand.Parameters.Add("@readonly", dynamicControl.ReadOnly);
					this.da.SelectCommand.Parameters.Add("@hidecaption", dynamicControl.HideCaption);
					this.da.SelectCommand.Parameters.Add("@setting4", dynamicControl.Setting4);
					this.da.SelectCommand.Parameters.Add("@fontsize", dynamicControl.FontSize);
					this.da.SelectCommand.Parameters.Add("@dontwraptonextline", dynamicControl.DontWrapToNextLine);
					this.da.SelectCommand.Parameters.Add("@specialcontroltype", dynamicControl.SpecialControlType);
					string text;
					this.da.Fill(new DataTable(), out text);
					if (text != null && text.Length > 0)
					{
						MessageBox.Show(text);
					}
					this.da.SelectCommand.CommandText = "UPDATE dynamicscreencontrols SET controlgroup=@controlgroupoverride WHERE controlid=@cid AND screennum=@screennum";
					this.da.SelectCommand.Parameters.Clear();
					this.da.SelectCommand.Parameters.Add("@screennum", this.screenInfo.screenNum);
					this.da.SelectCommand.Parameters.Add("@cid", dynamicControl.ControlId);
					if (!string.IsNullOrEmpty(dynamicControl.ControlGroupOverride))
					{
						this.da.SelectCommand.Parameters.Add("@controlgroupoverride", dynamicControl.ControlGroupOverride);
					}
					else
					{
						this.da.SelectCommand.Parameters.AddNull("@controlgroupoverride", DbType.String);
					}
					this.da.Fill(new DataTable());
				}
				if (DynamicControlWrapper_Base.ShowExtendedAccommodationInfo && dynamicControl.ExtendedAccommodation_SomethingChangedByUser)
				{
					this.da.SelectCommand.CommandText = "UPDATE accommodations SET showonreport=@showonreport,showonletter=@showonletter,isgroup=@isgroup,other=@other,enlarged=@enlarged,extratime=@extratime,isalone=@isalone,needscomputer=@needscomputer,needsreaderscribe=@needsreaderscribe,longdescription=@longdescription,shortcode=@shortcode WHERE controlid=@cid";
					this.da.SelectCommand.Parameters.Clear();
					this.da.SelectCommand.Parameters.Add("@longdescription", dynamicControl.ExtendedAccommodation_LongDescription);
					this.da.SelectCommand.Parameters.Add("@shortcode", dynamicControl.ExtendedAccommodation_shortCode);
					this.da.SelectCommand.Parameters.Add("@extratime", dynamicControl.ExtendedAccommodation_IsExtraTimeAccommodation);
					this.da.SelectCommand.Parameters.Add("@isalone", dynamicControl.ExtendedAccommodation_IsAloneAccommodation);
					this.da.SelectCommand.Parameters.Add("@needscomputer", dynamicControl.ExtendedAccommodation_IsComputerAccommodation);
					this.da.SelectCommand.Parameters.Add("@needsreaderscribe", dynamicControl.ExtendedAccommodation_IsReaderScribeAccommodation);
					this.da.SelectCommand.Parameters.Add("@isgroup", dynamicControl.ExtendedAccommodation_IsGroupAccommodation);
					this.da.SelectCommand.Parameters.Add("@other", dynamicControl.ExtendedAccommodation_IsOtherAccommodation);
					this.da.SelectCommand.Parameters.Add("@enlarged", dynamicControl.ExtendedAccommodation_IsEnlargedTextAccommodation);
					this.da.SelectCommand.Parameters.Add("@showonletter", dynamicControl.ExtendedAccommodation_ShowOnLetter);
					this.da.SelectCommand.Parameters.Add("@showonreport", dynamicControl.ExtendedAccommodation_group_report);
					this.da.SelectCommand.Parameters.Add("@cid", dynamicControl.ControlId);
					int num = this.da.SelectCommand.ExecuteNonQuery();
					if (num < 1)
					{
						this.da.SelectCommand.CommandText = "INSERT INTO accommodations (controlid,longdescription,shortcode,extratime,isalone,needscomputer,needsreaderscribe,isgroup,other,enlarged,showonletter,showonreport) SELECT @cid,@longdescription,@shortcode,@extratime,@isalone,@needscomputer,@needsreaderscribe,@isgroup,@other,@enlarged,@showonletter,@showonreport WHERE NOT EXISTS(SELECT controlid FROM accommodations WHERE controlid=@cid)";
						this.da.Fill(new DataTable());
					}
				}
				dynamicControl.HowModified = ModificationType.Unchanged;
			}
			foreach (DynamicControl dynamicControl2 in this.deletedDynamicControls)
			{
				this.da.SelectCommand.CommandText = "DELETE FROM dynamicscreencontrols WHERE screennum=@screennum AND controlid=@cid";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@screennum", this.screenInfo.screenNum);
				this.da.SelectCommand.Parameters.Add("@cid", dynamicControl2.ControlId);
				this.da.Fill(new DataTable());
			}
			this.deletedDynamicControls.Clear();
			int num2 = 10;
			foreach (DynamicControl dynamicControl in list)
			{
				this.da.SelectCommand.CommandText = "UPDATE dynamicscreencontrols SET ordernum=@ordernum WHERE controlid=@cid AND screennum=@screennum";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@ordernum", num2);
				this.da.SelectCommand.Parameters.Add("@cid", dynamicControl.ControlId);
				this.da.SelectCommand.Parameters.Add("@screennum", this.screenInfo.screenNum);
				this.da.Fill(new DataTable());
				num2 += 10;
			}
			if (closeFormAfterwards)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000BA08 File Offset: 0x0000AA08
		private void NodesToDynamicControlsList(ref List<DynamicControl> dynamicControls, Collection<Node> nodes)
		{
			foreach (Node node in nodes)
			{
				MyIconNode myIconNode = (MyIconNode)node;
				DynamicControl dynamicControl = myIconNode.DynamicControl;
				dynamicControls.Add(dynamicControl);
				if (dynamicControl.ControlCode == 30)
				{
					this.NodesToDynamicControlsList(ref dynamicControls, myIconNode.Nodes);
				}
				else if (dynamicControl.ControlCode == 32)
				{
					this.NodesToDynamicControlsList(ref dynamicControls, myIconNode.Nodes);
				}
				else if (dynamicControl.ControlCode == 33)
				{
					this.NodesToDynamicControlsList(ref dynamicControls, myIconNode.Nodes);
				}
				if (dynamicControl.AssociatedDynamicControl != null)
				{
					dynamicControls.Add(dynamicControl.AssociatedDynamicControl);
				}
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000BAFC File Offset: 0x0000AAFC
		private void p_apps_Paint(object sender, PaintEventArgs e)
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000BAFF File Offset: 0x0000AAFF
		private void btn_close_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000BB09 File Offset: 0x0000AB09
		private void tv_design_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000BB0C File Offset: 0x0000AB0C
		private void btn_textbox_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000BB0F File Offset: 0x0000AB0F
		private void label1_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000BB14 File Offset: 0x0000AB14
		private void QuickAdd(DynamicControlControl dcc)
		{
			TreeNodeAdv treeNodeAdv = (this.tv_design.SelectedNodes.Count == 1) ? this.tv_design.SelectedNodes[0] : null;
			TreeNodeAdv[] nodes = this.CreateNewNodeAndAddToModel(dcc);
			MyIconNode myIconNode = (treeNodeAdv == null) ? null : ((MyIconNode)treeNodeAdv.Tag);
			int num = (myIconNode == null) ? -1 : myIconNode.DynamicControl.ControlCode;
			NodePosition nodePos;
			if (treeNodeAdv != null && (num == 30 || num == 32 || num == 33))
			{
				nodePos = 0;
			}
			else
			{
				nodePos = 2;
			}
			this.PositionNode(nodes, myIconNode, nodePos);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000BBB0 File Offset: 0x0000ABB0
		private void tv_design_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				if (this.tv_design.SelectedNodes.Count > 0)
				{
					DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the selected item(s)?", "Delete selected item(s)", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (dialogResult == DialogResult.Yes)
					{
						ArrayList arrayList = new ArrayList();
						foreach (TreeNodeAdv node in this.tv_design.SelectedNodes)
						{
							this.DeleteNodes(ref arrayList, node);
						}
						foreach (object obj in arrayList)
						{
							MyIconNode myIconNode = (MyIconNode)obj;
							DynamicControl dynamicControl = myIconNode.DynamicControl;
							if (dynamicControl.HowModified != ModificationType.Added)
							{
								this.deletedDynamicControls.Add(myIconNode.DynamicControl);
							}
							if (dynamicControl.AssociatedDynamicControl != null)
							{
								this.deletedDynamicControls.Add(dynamicControl.AssociatedDynamicControl);
							}
							myIconNode.Parent = null;
							this._model.Nodes.Remove(myIconNode);
						}
						arrayList.Clear();
						this.tv_design.Refresh();
					}
				}
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000BD50 File Offset: 0x0000AD50
		private void DeleteNodes(ref ArrayList nodesToRemove, TreeNodeAdv node)
		{
			MyIconNode value = (MyIconNode)node.Tag;
			nodesToRemove.Add(value);
			foreach (TreeNodeAdv node2 in node.Children)
			{
				this.DeleteNodes(ref nodesToRemove, node2);
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000BDC4 File Offset: 0x0000ADC4
		private void btn_newList_Click(object sender, EventArgs e)
		{
			string userInput = InputBox.GetUserInput(this, "Add New Lookup List", "Please enter the name for the group of lookup list items:", "New lookup list");
			if (userInput != null && userInput.Length > 0)
			{
				this.da.SelectCommand.CommandText = "INSERT INTO lookupgroups (description) VALUES (@description)";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@description", userInput);
				DataTable dataTable = new DataTable();
				int num = this.da.FillReturnIdentity(dataTable, "lookupgroupid", "lookupgroups");
				this.helperClass.ReloadLookupListGroups();
				this.ListsToScreen();
				if (num > 0)
				{
					this.SelectList(num);
					this.btn_editList_Click(this.btn_editList, null);
				}
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000BEA0 File Offset: 0x0000AEA0
		private void SelectList(int lookupGroupId)
		{
			foreach (object obj in this.lv_lists.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				DynamicListGroup dynamicListGroup = (DynamicListGroup)listViewItem.Tag;
				if (dynamicListGroup.LookupGroupId == lookupGroupId)
				{
					listViewItem.Selected = true;
				}
				else
				{
					listViewItem.Selected = false;
				}
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000BF38 File Offset: 0x0000AF38
		private void ListsToScreen()
		{
			DynamicListGroupCollection listGroups = this.helperClass.ListGroups;
			this.lv_lists.BeginUpdate();
			this.lv_lists.Items.Clear();
			foreach (object obj in listGroups)
			{
				DynamicListGroup dynamicListGroup = (DynamicListGroup)obj;
				ListViewItem listViewItem = new ListViewItem(dynamicListGroup.Description);
				listViewItem.Tag = dynamicListGroup;
				this.lv_lists.Items.Add(listViewItem);
			}
			this.lv_lists.EndUpdate();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000BFF4 File Offset: 0x0000AFF4
		private void lv_lists_DoubleClick(object sender, EventArgs e)
		{
			this.btn_editList_Click(this.btn_editList, new EventArgs());
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000C009 File Offset: 0x0000B009
		private void btn_save_Click(object sender, EventArgs e)
		{
			this.SaveChanges(true);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000C014 File Offset: 0x0000B014
		private void tv_design_KeyPress_1(object sender, KeyPressEventArgs e)
		{
			if (char.IsLetterOrDigit(e.KeyChar))
			{
				base.ActiveControl = this.propertyGrid1;
				e.Handled = false;
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000C04C File Offset: 0x0000B04C
		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			string text = "";
			this.GetOnscreenControlNames(ref text, this.p_data);
			MessageBox.Show(text);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000C078 File Offset: 0x0000B078
		private void GetOnscreenControlInfo(ref string names, Control parentControl)
		{
			string text = names;
			names = string.Concat(new string[]
			{
				text,
				parentControl.Name,
				" (",
				parentControl.Location.ToString(),
				" ",
				parentControl.Size.ToString(),
				" / visible=",
				parentControl.Visible.ToString()
			});
			names += Environment.NewLine;
			foreach (object obj in parentControl.Controls)
			{
				Control parentControl2 = (Control)obj;
				this.GetOnscreenControlInfo(ref names, parentControl2);
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000C174 File Offset: 0x0000B174
		private void GetOnscreenControlNames(ref string names, Control parentControl)
		{
			if (parentControl.Tag is DataRow)
			{
				DataRow dataRow = (DataRow)parentControl.Tag;
				if (dataRow.Table.Columns.Contains("controlcaption"))
				{
					names += dataRow["controlcaption"].ToString();
					names += ", ";
				}
			}
			foreach (object obj in parentControl.Controls)
			{
				Control parentControl2 = (Control)obj;
				this.GetOnscreenControlNames(ref names, parentControl2);
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000C248 File Offset: 0x0000B248
		public static string[] SplitStringIntoNEWLINE_delimitered_parts(string s, bool excludeEmptyStrings)
		{
			string[] array = s.Split(Environment.NewLine.ToCharArray());
			if (excludeEmptyStrings)
			{
				ArrayList arrayList = new ArrayList();
				foreach (string text in array)
				{
					if (text.Trim().Length > 0)
					{
						arrayList.Add(text);
					}
				}
				array = new string[arrayList.Count];
				for (int j = 0; j < arrayList.Count; j++)
				{
					array[j] = (string)arrayList[j];
				}
			}
			return array;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000C2F8 File Offset: 0x0000B2F8
		private void entergroupCaptionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string text = "";
			MyIconNode[] array = new MyIconNode[this.tv_design.SelectedNodes.Count];
			for (int i = 0; i < this.tv_design.SelectedNodes.Count; i++)
			{
				TreeNodeAdv treeNodeAdv = this.tv_design.SelectedNodes[i];
				if (text.Length > 0)
				{
					text += Environment.NewLine;
				}
				MyIconNode myIconNode = (MyIconNode)this._model.FindNode(this.tv_design.GetPath(treeNodeAdv));
				myIconNode.Index = treeNodeAdv.Index;
				array[i] = myIconNode;
			}
			Array.Sort<MyIconNode>(array);
			for (int i = 0; i < array.Length; i++)
			{
				MyIconNode myIconNode = array[i];
				if (i > 0)
				{
					text += Environment.NewLine;
				}
				text += myIconNode.DynamicControl.ControlCaption;
			}
			string userInput = ControlCaptionsEditor.GetUserInput(this, text);
			if (userInput != null && userInput.Trim().Length > 0)
			{
				string[] array2 = ScreenEditor.SplitStringIntoNEWLINE_delimitered_parts(userInput, true);
				int num = 0;
				foreach (MyIconNode myIconNode in array)
				{
					if (num < array2.Length)
					{
						myIconNode.DynamicControl.ControlCaption = array2[num];
						if (myIconNode.DynamicControl.ControlName.Length < 1)
						{
							myIconNode.DynamicControl.ControlName = this.GetControlNameFromCaption(array2[num]);
						}
						this.UpdateTreeNodes(this._model.Nodes, myIconNode.DynamicControl);
					}
					num++;
				}
				this.propertyGrid1.Refresh();
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000C4E0 File Offset: 0x0000B4E0
		private string GetControlNameFromCaption(string caption)
		{
			string text = "";
			foreach (char c in caption)
			{
				if (char.IsLetterOrDigit(c))
				{
					text += c;
				}
			}
			return text;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000C540 File Offset: 0x0000B540
		private DynamicControl GetSelectedDynamicControl()
		{
			TreeNodeAdv node = this.tv_design.SelectedNodes[0];
			return this.GetSelectedDynamicControl(node);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000C56C File Offset: 0x0000B56C
		private DynamicControl GetSelectedDynamicControl(TreeNodeAdv node)
		{
			MyIconNode myIconNode = (node != null) ? ((MyIconNode)this._model.FindNode(this.tv_design.GetPath(node))) : null;
			return (myIconNode != null) ? myIconNode.DynamicControl : null;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000C5B0 File Offset: 0x0000B5B0
		private void cm_nodes_Opening(object sender, CancelEventArgs e)
		{
			DynamicControl selectedDynamicControl = this.GetSelectedDynamicControl();
			bool visible = selectedDynamicControl != null && selectedDynamicControl.ControlCode == 600;
			bool visible2 = selectedDynamicControl != null && selectedDynamicControl.ControlCode == 1;
			bool flag = selectedDynamicControl != null && selectedDynamicControl.ControlCode == 800;
			this.entergroupCaptionsToolStripMenuItem.Enabled = (this.tv_design.SelectedNodes.Count > 1);
			this.convertTextBoxToRichTextBoxupgradeToolStripMenuItem.Visible = visible2;
			this.convertRichTextBoxToTextBoxdowngradeToolStripMenuItem.Visible = visible;
			bool enabled = selectedDynamicControl.ControlCode == 14 || selectedDynamicControl.ControlCode == 3 || selectedDynamicControl.ControlCode == 703;
			this.editThelistToolStripMenuItem.Enabled = enabled;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000C66C File Offset: 0x0000B66C
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TableProperty tableProperty = new TableProperty();
			TablePropertyEditor tablePropertyEditor = new TablePropertyEditor(tableProperty);
			Form form = new Form();
			form.Controls.Add(tablePropertyEditor);
			tablePropertyEditor.Dock = DockStyle.Fill;
			form.ShowDialog(this);
			base.Close();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000C6B4 File Offset: 0x0000B6B4
		private void convertSelectedControlExistingDataToENCRYPTEDToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.tv_design.SelectedNodes.Count == 1)
			{
				TreeNodeAdv treeNodeAdv = this.tv_design.SelectedNodes[0];
				MyIconNode myIconNode = (MyIconNode)this._model.FindNode(this.tv_design.GetPath(treeNodeAdv));
				DynamicControl dynamicControl = myIconNode.DynamicControl;
				this.ConvertExistingDataToEncryptedNonEncrypted(dynamicControl, true);
			}
			else
			{
				MessageBox.Show("Please select one control first.");
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000C72C File Offset: 0x0000B72C
		private void convertSelectedControlExistingDataToNOTENCRYPTEDToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.tv_design.SelectedNodes.Count == 1)
			{
				TreeNodeAdv treeNodeAdv = this.tv_design.SelectedNodes[0];
				MyIconNode myIconNode = (MyIconNode)this._model.FindNode(this.tv_design.GetPath(treeNodeAdv));
				DynamicControl dynamicControl = myIconNode.DynamicControl;
				this.ConvertExistingDataToEncryptedNonEncrypted(dynamicControl, false);
			}
			else
			{
				MessageBox.Show("Please select one control first.");
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000C7A4 File Offset: 0x0000B7A4
		private int ConvertExistingDataToEncryptedNonEncrypted(DynamicControl dc, bool encrypt)
		{
			try
			{
				if (!dc.IsComboBox || dc.ControlId <= 0)
				{
					if (!dc.IsTextBox || dc.ControlId <= 0)
					{
						MessageBox.Show("This only works for textboxes and droplists - and also for controls that have already been created.  Nothing was done.");
						return 0;
					}
				}
				string str = "";
				if (this.screenInfo.screenNum == 4)
				{
					str = "otherinfoaccommodationps";
				}
				else
				{
					this.da.SelectCommand.CommandText = "SELECT description,typecode FROM screens WHERE screennum=@screennum";
					this.da.SelectCommand.Parameters.Clear();
					this.da.SelectCommand.Parameters.Add("@screennum", this.screenInfo.screenNum);
					DataTable dataTable = new DataTable();
					this.da.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						int num = (int)dataTable.Rows[0][1];
						if (num == 0)
						{
							str = "otherinfops";
						}
						else
						{
							str = "otherinfopa";
						}
					}
					else
					{
						MessageBox.Show("Error; can't load screen");
					}
				}
				DataTable dataTable2 = new DataTable();
				this.da.SelectCommand.CommandText = "SELECT dataid,controlvalue FROM " + str + " WHERE controlid=@cid";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@cid", dc.ControlId);
				this.da.Fill(dataTable2);
				bool flag = !encrypt;
				dataTable2.Columns.Add("controlvalueplaintext");
				foreach (object obj in dataTable2.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					dataRow["controlvalueplaintext"] = DynamicScreen.BytesToString((byte[])dataRow["controlvalue"], flag, this.tripleDES);
				}
				DataTableView.ShowDataTableView(dataTable2);
				DialogResult dialogResult = MessageBox.Show("Verify data", "Would you like to continue? (ie. did the data show up properly in the previous screen?)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					dataTable2.Columns.Add("controlvaluenew");
					dataTable2.Columns.Add("controlvalueplaintextnew");
					foreach (object obj2 in dataTable2.Rows)
					{
						DataRow dataRow = (DataRow)obj2;
						string text = (string)dataRow["controlvalueplaintext"];
						byte[] array = DynamicScreen.StringToBytes(text, encrypt, this.tripleDES);
						dataRow["controlvaluenew"] = array;
						text = DynamicScreen.BytesToString(array, !flag, this.tripleDES);
						dataRow["controlvalueplaintextnew"] = text;
					}
					DataTableView.ShowDataTableView(this, dataTable2, "Nothing has been done yet; this is for review.  Next step will be to ok this and commit.");
					dialogResult = MessageBox.Show("Commit", "Would you like to commit these changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dialogResult == DialogResult.Yes)
					{
						foreach (object obj3 in dataTable2.Rows)
						{
							DataRow dataRow = (DataRow)obj3;
							this.da.SelectCommand.CommandText = "UPDATE " + str + " SET controlvalue=@cv WHERE dataid=@did";
							this.da.SelectCommand.Parameters.Clear();
							string text = (string)dataRow["controlvalueplaintextnew"];
							this.da.SelectCommand.Parameters.Add("@cv", DynamicScreen.StringToBytes(text, encrypt, this.tripleDES));
							this.da.SelectCommand.Parameters.Add("@did", dataRow["dataid"]);
							string text2;
							this.da.Fill(new DataTable(), out text2);
						}
						MessageBox.Show("Done.  Updated " + dataTable2.Rows.Count.ToString() + " row(s).");
						return dataTable2.Rows.Count;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
			return 0;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000CCDC File Offset: 0x0000BCDC
		private void btn_editList_Click(object sender, EventArgs e)
		{
			if (this.lv_lists.SelectedItems.Count == 1)
			{
				ListViewItem listViewItem = this.lv_lists.SelectedItems[0];
				DynamicListGroup dynamicListGroup = (DynamicListGroup)listViewItem.Tag;
				int lookupGroupId = dynamicListGroup.LookupGroupId;
				if (lookupGroupId > 0)
				{
					this.showListEditDialog(lookupGroupId, dynamicListGroup.Description, this.da);
				}
				else
				{
					MessageBox.Show(this, "No list selected!");
				}
			}
			else
			{
				MessageBox.Show("Please select one list to edit first...");
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000CD6C File Offset: 0x0000BD6C
		private void btn_refreshGroups_Click(object sender, EventArgs e)
		{
			this.helperClass.ReloadGroups();
			this.ListsToScreen();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000CD84 File Offset: 0x0000BD84
		private void createNewFieldsByEnteringCaptionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.tv_design.SelectedNodes.Count == 1)
			{
				TreeNodeAdv treeNodeAdv = this.tv_design.SelectedNodes[0];
				MyIconNode myIconNode = (MyIconNode)this._model.FindNode(this.tv_design.GetPath(treeNodeAdv));
				if (myIconNode.DynamicControl.ControlCode == 30 || myIconNode.DynamicControl.ControlCode == 32 || myIconNode.DynamicControl.ControlCode == 33)
				{
					string userInput = InputBox.GetUserInput(this, "Enter group captions to create new controls", "Please enter captions (followed by ,controltype[T=textbox,C=checkbox,R=radiobutton,L=label,D=droplist,S=stafflist,A=table,H=horizontalrule,B=columnbreak], separated by <newline>:", this.lastNewCaptions, 450, false);
					if (userInput != null && userInput.Trim().Length > 0)
					{
						this.lastNewCaptions = userInput;
						string[] array = ScreenEditor.SplitStringIntoNEWLINE_delimitered_parts(userInput, true);
						string[] array2 = array;
						int i = 0;
						while (i < array2.Length)
						{
							string text = array2[i];
							int num = text.IndexOf(',');
							string defaultCaption;
							string text2;
							if (num >= 0)
							{
								defaultCaption = ((num == 0) ? "" : text.Substring(0, num));
								text2 = text.Substring(num + 1).ToLower();
							}
							else
							{
								defaultCaption = text;
								text2 = "l";
							}
							string text3 = text2;
							if (text3 == null)
							{
								goto IL_24E;
							}
							if (<PrivateImplementationDetails>{8448B9FB-264B-4242-862F-729C10DB91B6}.$$method0x6000147-1 == null)
							{
								<PrivateImplementationDetails>{8448B9FB-264B-4242-862F-729C10DB91B6}.$$method0x6000147-1 = new Dictionary<string, int>(8)
								{
									{
										"t",
										0
									},
									{
										"c",
										1
									},
									{
										"r",
										2
									},
									{
										"d",
										3
									},
									{
										"s",
										4
									},
									{
										"a",
										5
									},
									{
										"h",
										6
									},
									{
										"b",
										7
									}
								};
							}
							int num2;
							if (!<PrivateImplementationDetails>{8448B9FB-264B-4242-862F-729C10DB91B6}.$$method0x6000147-1.TryGetValue(text3, out num2))
							{
								goto IL_24E;
							}
							DynamicControlControl dcc;
							switch (num2)
							{
							case 0:
								dcc = new DynamicControlControl(1);
								break;
							case 1:
								dcc = new DynamicControlControl(2);
								break;
							case 2:
								dcc = new DynamicControlControl(14);
								break;
							case 3:
								dcc = new DynamicControlControl(3);
								break;
							case 4:
								dcc = new DynamicControlControl(100);
								break;
							case 5:
								dcc = new DynamicControlControl(10);
								break;
							case 6:
								dcc = new DynamicControlControl(8);
								break;
							case 7:
								dcc = new DynamicControlControl(50);
								break;
							default:
								goto IL_24E;
							}
							IL_258:
							TreeNodeAdv[] array3 = this.CreateNewNodeAndAddToModel(dcc, defaultCaption);
							if (array3 != null)
							{
								NodePosition nodePos = (myIconNode == null) ? 1 : this.tv_design.DropPosition.Position;
								this.PositionNode(array3, myIconNode, nodePos);
							}
							i++;
							continue;
							IL_24E:
							dcc = new DynamicControlControl(5);
							goto IL_258;
						}
					}
				}
				else
				{
					MessageBox.Show("Please select a container control (i.e. groupbox) for these new controls to go into first");
				}
			}
			else
			{
				MessageBox.Show("Please select the group to add these new controls into first");
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000D05F File Offset: 0x0000C05F
		private void treeView_existingControls_DragDrop(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.None;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000D06C File Offset: 0x0000C06C
		private void treeView_existingControls_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this.treeView_existingControls.SelectedNodes)
				{
					TreeNode treeNode = (TreeNode)obj;
					if (treeNode.Tag is DynamicControl)
					{
						DynamicControl dynamicControl = (DynamicControl)treeNode.Tag;
						dynamicControl.HowModified = ModificationType.CopiedFromAnotherScreen;
						arrayList.Add(dynamicControl);
					}
				}
				if (arrayList.Count > 0)
				{
					base.DoDragDrop(arrayList, DragDropEffects.Copy);
				}
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000D144 File Offset: 0x0000C144
		private void convertADroplistFromRegularTextbasedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.tv_design.SelectedNodes.Count == 1)
			{
				TreeNodeAdv treeNodeAdv = this.tv_design.SelectedNodes[0];
				MyIconNode myIconNode = (MyIconNode)this._model.FindNode(this.tv_design.GetPath(treeNodeAdv));
				DynamicControl dynamicControl = myIconNode.DynamicControl;
				this.ConvertDropListFromRegularToTextbased(dynamicControl, false);
			}
			else
			{
				MessageBox.Show("Please select one drop-list first.");
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000D1BC File Offset: 0x0000C1BC
		private void convertADroplistFromTextbasedToRegularToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.tv_design.SelectedNodes.Count == 1)
			{
				TreeNodeAdv treeNodeAdv = this.tv_design.SelectedNodes[0];
				MyIconNode myIconNode = (MyIconNode)this._model.FindNode(this.tv_design.GetPath(treeNodeAdv));
				DynamicControl dynamicControl = myIconNode.DynamicControl;
				MessageBox.Show("Not implemented yet.");
			}
			else
			{
				MessageBox.Show("Please select one drop-list first.");
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000D238 File Offset: 0x0000C238
		private string GetDynamicDataTableNameSuffix()
		{
			return InputBox.GetUserInput(this, "Dynamic data table name suffix", "Please enter the dynamic data table name suffix:", "ps");
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000D264 File Offset: 0x0000C264
		private void ConvertDropListFromRegularToTextbased(DynamicControl dc, bool encrypt)
		{
			string dynamicDataTableNameSuffix = this.GetDynamicDataTableNameSuffix();
			if (dynamicDataTableNameSuffix != null)
			{
				string str = "maininfo" + dynamicDataTableNameSuffix;
				this.da.SelectCommand.CommandText = "SELECT t.*,ll.lookuptext FROM " + str + " t LEFT JOIN lookuplists ll ON ll.lookupgroupid=@lgid AND ll.lookuplistid=t.controlvalue WHERE t.controlid=@cid";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@cid", dc.ControlId);
				this.da.SelectCommand.Parameters.Add("@lgid", dc.Setting1);
				DataTable dataTable = new DataTable();
				string text;
				this.da.Fill(dataTable, out text);
				if (text != null && text.Length > 0)
				{
					MessageBox.Show(text);
				}
				else if (dataTable.Rows.Count < 1)
				{
					MessageBox.Show("No data to convert. Nothing was done.");
				}
				else
				{
					DialogResult dialogResult = DataTableView.ShowDataTableView(this, dataTable, "Ok to continue?");
					if (dialogResult == DialogResult.OK)
					{
						DataTable dataTable2 = dataTable.Clone();
						dataTable2.Columns.Remove("controlvalue");
						byte[] array = new byte[0];
						Type type = array.GetType();
						dataTable2.Columns.Add("controlvalue", type);
						foreach (object obj in dataTable.Rows)
						{
							DataRow dataRow = (DataRow)obj;
							dataTable2.ImportRow(dataRow);
							DataRow dataRow2 = dataTable2.Rows[dataTable2.Rows.Count - 1];
							dataRow2["controlvalue"] = DynamicScreen.StringToBytes(dataRow["lookuptext"].ToString(), encrypt, this.tripleDES);
							dataRow2["lookuptext"] = DynamicScreen.BytesToString((byte[])dataRow2["controlvalue"], encrypt, this.tripleDES);
						}
						dialogResult = DataTableView.ShowDataTableView(this, dataTable2, "ok to save this?");
						if (dialogResult == DialogResult.OK)
						{
							dataTable2.Columns.Remove("lookuptext");
							string text2 = "";
							string text3 = "";
							for (int i = 1; i < dataTable2.Columns.Count; i++)
							{
								if (i > 1)
								{
									text2 += ",";
									text3 += ",";
								}
								text2 += dataTable2.Columns[i].ColumnName;
								text3 = text3 + "@" + dataTable2.Columns[i].ColumnName;
							}
							string text4 = "otherinfo" + dynamicDataTableNameSuffix;
							try
							{
								foreach (object obj2 in dataTable2.Rows)
								{
									DataRow dataRow = (DataRow)obj2;
									this.da.SelectCommand.CommandText = string.Concat(new string[]
									{
										"INSERT INTO ",
										text4,
										" (",
										text2,
										") VALUES (",
										text3,
										")"
									});
									this.da.SelectCommand.Parameters.Clear();
									for (int i = 1; i < dataTable2.Columns.Count; i++)
									{
										string parameterName = "@" + dataTable2.Columns[i].ColumnName;
										this.da.SelectCommand.Parameters.Add(parameterName, dataRow[i]);
										this.da.Fill(new DataTable());
									}
								}
								MessageBox.Show("Done adding.  Deleting old info...");
								this.da.SelectCommand.CommandText = "DELETE FROM " + str + "WHERE controlid=@cid";
								this.da.SelectCommand.Parameters.Clear();
								this.da.SelectCommand.Parameters.Add("@cid", dc.ControlId);
								this.da.Fill(new DataTable());
								DataTableView.ShowDataTableView(this, dataTable, "Save the original data just in case.");
							}
							catch (Exception ex)
							{
								MessageBox.Show(ex.ToString());
							}
							dc.Setting3 = (encrypt ? -1 : 1);
						}
					}
				}
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000D7B8 File Offset: 0x0000C7B8
		private void btn_viewScreenControlInfo_Click(object sender, EventArgs e)
		{
			string text = "";
			this.GetOnscreenControlInfo(ref text, this.p_data);
			MessageBox.Show(text);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000D7E2 File Offset: 0x0000C7E2
		private void treeView_existingControls_MouseUp(object sender, MouseEventArgs e)
		{
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000D7E8 File Offset: 0x0000C7E8
		private void setCommonBackgroundColoursForSelectedGroupboxesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Color[] array = new Color[]
			{
				Color.LightBlue,
				Color.PapayaWhip,
				Color.PaleGreen,
				Color.LightPink,
				Color.LightYellow,
				Color.LightSteelBlue,
				Color.LightCoral,
				Color.Lavender
			};
			if (this.tv_design.SelectedNodes.Count > 0)
			{
				int num = 0;
				foreach (TreeNodeAdv treeNodeAdv in this.tv_design.SelectedNodes)
				{
					MyIconNode myIconNode = (MyIconNode)this._model.FindNode(this.tv_design.GetPath(treeNodeAdv));
					if (myIconNode.DynamicControl.ControlCode == 30)
					{
						if (num < array.Length)
						{
							myIconNode.DynamicControl.Setting2 = array[num].ToArgb();
							myIconNode.DynamicControl.Setting1 = 1;
							num++;
						}
					}
				}
				MessageBox.Show("Set " + num.ToString() + " colour(s).");
			}
			else
			{
				MessageBox.Show("Please select at least one group box first");
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000D9A4 File Offset: 0x0000C9A4
		private void setAsGroupBoxTitleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.tv_design.SelectedNodes.Count > 0)
			{
				foreach (TreeNodeAdv treeNodeAdv in this.tv_design.SelectedNodes)
				{
					MyIconNode myIconNode = (MyIconNode)this._model.FindNode(this.tv_design.GetPath(treeNodeAdv));
					if (myIconNode.DynamicControl.ControlCode == 5)
					{
						myIconNode.DynamicControl.Setting1 = 1;
						myIconNode.DynamicControl.DefaultValue = 120;
						myIconNode.DynamicControl.ControlCaption = myIconNode.DynamicControl.ControlCaption.ToUpper();
					}
				}
			}
			else
			{
				MessageBox.Show("Please select at least one field first");
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000DA94 File Offset: 0x0000CA94
		private void setAsPhoneNumberToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.tv_design.SelectedNodes.Count > 0)
			{
				foreach (TreeNodeAdv treeNodeAdv in this.tv_design.SelectedNodes)
				{
					MyIconNode myIconNode = (MyIconNode)this._model.FindNode(this.tv_design.GetPath(treeNodeAdv));
					if (myIconNode.DynamicControl.ControlCode == 1)
					{
						myIconNode.DynamicControl.Mask = "(999) 999-9999";
						myIconNode.DynamicControl.Setting2 = 15;
					}
				}
			}
			else
			{
				MessageBox.Show("Please select at least one field first");
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000DB6C File Offset: 0x0000CB6C
		private void pane_mainControls_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000DB70 File Offset: 0x0000CB70
		private void convertTextBoxToRichTextBoxupgradeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNodeAdv treeNodeAdv = this.tv_design.SelectedNodes[0];
			DynamicControl selectedDynamicControl = this.GetSelectedDynamicControl(treeNodeAdv);
			if (selectedDynamicControl != null && selectedDynamicControl.ControlCode == 1)
			{
				if (selectedDynamicControl.ControlId > 0)
				{
					List<DynamicControl> list = new List<DynamicControl>();
					TreeNodeAdv parent = treeNodeAdv.Parent;
					string controlCaption = selectedDynamicControl.ControlCaption;
					if (parent != null)
					{
						for (int i = treeNodeAdv.Index + 1; i < parent.Children.Count; i++)
						{
							TreeNodeAdv node = parent.Children[i];
							DynamicControl dynamicControl = this.GetSelectedDynamicControl(node);
							if (dynamicControl == null)
							{
								break;
							}
							if (dynamicControl.ControlCaption.IndexOf(controlCaption) != 0 || dynamicControl.ControlCaption.IndexOf("__") <= 0)
							{
								break;
							}
							list.Add(dynamicControl);
						}
					}
					string text;
					if (list.Count > 0)
					{
						text = "This textbox is extended: ";
						foreach (DynamicControl dynamicControl in list)
						{
							text = text + Environment.NewLine + dynamicControl.ControlCaption;
						}
					}
					else
					{
						text = "This textbox is not extended.";
					}
					DialogResult dialogResult = MessageBox.Show(string.Concat(new string[]
					{
						"Are you sure you want to convert this textbox ('",
						selectedDynamicControl.ControlCaption,
						"') to a RICHTEXTBOX?",
						Environment.NewLine,
						Environment.NewLine,
						text
					}), "Convert to RichTextBox", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dialogResult == DialogResult.Yes)
					{
						int num = this.screenTypeCode;
						string text2;
						string text3;
						if (num <= 21)
						{
							switch (num)
							{
							case 0:
								text2 = "otherinfops";
								text3 = "";
								goto IL_2B8;
							case 1:
								text2 = "otherinfopa";
								text3 = ",appointmentid";
								goto IL_2B8;
							case 2:
								text2 = "otherinfoan";
								text3 = "";
								goto IL_2B8;
							case 3:
								text2 = "otherinfoaccommodationps";
								text3 = ",courseid";
								goto IL_2B8;
							default:
								switch (num)
								{
								case 20:
									text2 = "otherinfopa";
									text3 = ",appointmentid";
									goto IL_2B8;
								case 21:
									text2 = "otherinfops";
									text3 = "";
									goto IL_2B8;
								}
								break;
							}
						}
						else
						{
							if (num == 25)
							{
								text2 = "otherinfopm";
								text3 = ",appointmentid";
								goto IL_2B8;
							}
							if (num == 30)
							{
								text2 = "otherinfoinstructorpm";
								text3 = ",appointmentid";
								goto IL_2B8;
							}
						}
						text2 = "otherinfops";
						text3 = "";
						IL_2B8:
						string text4 = selectedDynamicControl.ControlId.ToString();
						foreach (DynamicControl dynamicControl2 in list)
						{
							text4 = text4 + "," + dynamicControl2.ControlId.ToString();
						}
						this.da.SelectCommand.CommandText = string.Concat(new string[]
						{
							"SELECT d.*,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4,dc.defaultvalue FROM ",
							text2,
							" d LEFT JOIN dynamiccontrols dc ON dc.controlid=d.controlid LEFT JOIN dynamicscreencontrols dsc ON dsc.controlid=d.controlid WHERE d.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')) ORDER BY d.personid",
							text3,
							",dsc.ordernum"
						});
						this.da.SelectCommand.Parameters.Clear();
						this.da.SelectCommand.Parameters.Add("@cids", text4);
						DataTable dataTable = new DataTable();
						this.da.Fill(dataTable);
						DataTableView.ShowDataTableView(this, dataTable, "Existing info for this textbox is listed below; click 'Ok' to continue...");
						string text5 = text2.Replace("otherinfo", "imageinfo");
						this.da.SelectCommand.CommandText = "SELECT * FROM " + text5 + " WHERE controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))";
						DataTable dataTable2 = new DataTable();
						this.da.Fill(dataTable2);
						dataTable2.Columns.Add("newstringvalue");
						DataTableView.ShowDataTableView(this, dataTable2, "This is any existing info for the rich text box:");
						RichTextBox richTextBox = new RichTextBox();
						string text6 = (text3.Length > 0) ? text3.Substring(1) : "";
						bool flag = text6.Length > 0;
						int j;
						for (int i = 0; i < dataTable.Rows.Count; i = j)
						{
							DataRow dataRow = dataTable.Rows[i];
							int num2 = (int)dataRow["personid"];
							int num3;
							if (flag)
							{
								num3 = ((dataRow[text6] != DBNull.Value) ? ((int)dataRow[text6]) : 0);
							}
							else
							{
								num3 = 0;
							}
							for (j = i + 1; j < dataTable.Rows.Count; j++)
							{
								DataRow dataRow2 = dataTable.Rows[j];
								int num4 = (int)dataRow2["personid"];
								int num5;
								if (flag)
								{
									num5 = ((dataRow2[text6] != DBNull.Value) ? ((int)dataRow2[text6]) : 0);
								}
								else
								{
									num5 = 0;
								}
								if (num2 != num4 || num3 != num5)
								{
									break;
								}
							}
							DataRow dataRow3 = dataTable2.NewRow();
							for (int k = 0; k < dataTable.Columns.Count; k++)
							{
								string columnName = dataTable.Columns[k].ColumnName;
								if (dataTable2.Columns.Contains(columnName))
								{
									dataRow3[columnName] = dataRow[columnName];
								}
							}
							int num6 = (int)dataRow["setting3"];
							bool flag2 = num6 != 0;
							string text7 = "";
							for (int k = i; k < j; k++)
							{
								text7 += DynamicScreen.BytesToString((byte[])dataTable.Rows[k]["controlvalue"], flag2, this.tripleDES);
							}
							richTextBox.Text = text7;
							dataRow3["controlvalue"] = DynamicScreen.StringToBytes(richTextBox.Rtf, flag2, this.tripleDES);
							dataRow3["newstringvalue"] = richTextBox.Rtf;
							dataTable2.Rows.Add(dataRow3);
						}
						DataTableView.ShowDataTableView(this, dataTable2, "Preview of changes to be made (no changes have been made yet):");
						richTextBox.Dispose();
						SaveFileDialog saveFileDialog = new SaveFileDialog();
						saveFileDialog.Title = "Save backup of existing data";
						saveFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
						dialogResult = saveFileDialog.ShowDialog(this);
						if (dialogResult == DialogResult.OK)
						{
							DataSet dataSet = new DataSet();
							dataTable.TableName = "Backup_" + text2 + "_" + DateTime.Now.ToString("yyyy.MM.dd.h.mm tt");
							dataSet.Tables.Add(dataTable);
							dataSet.WriteXml(saveFileDialog.FileName, XmlWriteMode.WriteSchema);
							dialogResult = MessageBox.Show("Backup of old data is complete - do you want to proceed to add the new data to the system and remove the old data?", "Write changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
							if (dialogResult == DialogResult.Yes)
							{
								try
								{
									this.da.Connection.Open();
									int num7 = dataTable2.Columns.Count - 1;
									string text8 = "";
									string text9 = "";
									for (int k = 1; k < num7; k++)
									{
										if (k > 1)
										{
											text8 += ",";
											text9 += ",";
										}
										string text10 = "@cn" + k.ToString();
										text8 += dataTable2.Columns[k].ColumnName;
										text9 += text10;
									}
									string commandText = string.Format("INSERT INTO {0} ({1}) SELECT {2} WHERE NOT EXISTS(SELECT personid FROM {0} WHERE personid=@pid AND controlid=@cid{3})", new object[]
									{
										text5,
										text8,
										text9,
										(text6.Length > 0) ? (" AND " + text6 + "=@agb") : ""
									});
									foreach (object obj in dataTable2.Rows)
									{
										DataRow dataRow2 = (DataRow)obj;
										if (dataRow2.RowState == DataRowState.Added)
										{
											this.da.SelectCommand.CommandText = commandText;
											this.da.SelectCommand.Parameters.Clear();
											for (int k = 1; k < num7; k++)
											{
												string text10 = "@cn" + k.ToString();
												this.da.SelectCommand.Parameters.Add(text10, dataRow2[k]);
											}
											int num8 = (dataRow2["personid"] == DBNull.Value) ? 0 : ((int)dataRow2["personid"]);
											int num9 = (dataRow2["controlid"] == DBNull.Value) ? 0 : ((int)dataRow2["controlid"]);
											if (num8 > 0)
											{
												this.da.SelectCommand.Parameters.Add("@pid", num8);
												this.da.SelectCommand.Parameters.Add("@cid", num9);
												if (text6.Length > 0)
												{
													this.da.SelectCommand.Parameters.Add("@agb", dataRow2[text6]);
												}
												try
												{
													this.da.SelectCommand.ExecuteNonQuery();
													this.da.SelectCommand.CommandText = "UPDATE " + text2 + " SET controlid=-controlid,personid=-personid WHERE personid=@pid AND controlid=@cid";
													this.da.SelectCommand.Parameters.Clear();
													this.da.SelectCommand.Parameters.Add("@cid", num9);
													this.da.SelectCommand.Parameters.Add("@pid", num8);
													this.da.SelectCommand.ExecuteNonQuery();
												}
												catch (Exception ex)
												{
												}
											}
										}
									}
									selectedDynamicControl.SetControlCode(600);
									MyIconNode myIconNode = (MyIconNode)this._model.FindNode(this.tv_design.GetPath(treeNodeAdv));
									myIconNode.Icon = this.GetImage(selectedDynamicControl.ControlCode);
									this.da.SelectCommand.CommandText = "UPDATE dynamiccontrols SET controlcode=@cc WHERE controlid=@cid";
									this.da.SelectCommand.Parameters.Clear();
									this.da.SelectCommand.Parameters.Add("@cc", 600);
									this.da.SelectCommand.Parameters.Add("@cid", selectedDynamicControl.ControlId);
									this.da.SelectCommand.ExecuteNonQuery();
									this.btn_apply_Click(this.btn_apply, new EventArgs());
									MessageBox.Show("Done - the current form has been saved.");
								}
								catch (Exception ex2)
								{
									MessageBox.Show(ex2.ToString());
									MessageBox.Show("Nothing was done");
								}
								finally
								{
									this.da.Connection.Close();
								}
							}
						}
					}
				}
				else
				{
					MessageBox.Show("This control hasn't been saved yet, so there is no data in the system.  You can just remove the control and add in the desired control in it's place.");
				}
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000E810 File Offset: 0x0000D810
		private void convertRichTextBoxToTextBoxdowngradeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DynamicControl selectedDynamicControl = this.GetSelectedDynamicControl();
			if (selectedDynamicControl != null && selectedDynamicControl.ControlCode == 600)
			{
				if (selectedDynamicControl.ControlId > 0)
				{
					DialogResult dialogResult = MessageBox.Show("Are you sure you want to convert this RichTextBox ('" + selectedDynamicControl.ControlCaption + "') to a TEXTBOX?", "Convert to TextBox", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dialogResult == DialogResult.Yes)
					{
					}
				}
				else
				{
					MessageBox.Show("This control hasn't been saved yet, so there is no data in the system.  You can just remove the control and add in the desired control in it's place.");
				}
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000E894 File Offset: 0x0000D894
		private void getACommaSeparatedListOfControlidsForSelectedFieldsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string text = "";
			foreach (TreeNodeAdv treeNodeAdv in this.tv_design.SelectedNodes)
			{
				MyIconNode myIconNode = (MyIconNode)this._model.FindNode(this.tv_design.GetPath(treeNodeAdv));
				if (text.Length > 0)
				{
					text += ",";
				}
				text += myIconNode.DynamicControl.ControlId.ToString();
			}
			InputBox inputBox = new InputBox("Control Ids", "Here are the controlids for the selected fields:", text, false);
			inputBox.ShowDialog(this);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000E96C File Offset: 0x0000D96C
		private void ExtractDescriptor(string controlCaption, out string controlCaptionWithoutDescriptor, out string descriptor)
		{
			int num = controlCaption.IndexOf("~~");
			if (num >= 0 && num + 2 < controlCaption.Length)
			{
				descriptor = controlCaption.Substring(num + 2);
				controlCaptionWithoutDescriptor = ((num == 0) ? "" : controlCaption.Substring(0, num));
			}
			else
			{
				descriptor = "";
				controlCaptionWithoutDescriptor = controlCaption;
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000E9D0 File Offset: 0x0000D9D0
		private void MENU_markFieldsWithAGroupDescriptor_Click(object sender, EventArgs e)
		{
			string defaultText;
			if (this.tv_design.SelectedNodes.Count > 0)
			{
				MyIconNode myIconNode = (MyIconNode)this._model.FindNode(this.tv_design.GetPath(this.tv_design.SelectedNodes[0]));
				string controlCaption = myIconNode.DynamicControl.ControlCaption;
				string controlCaption2;
				this.ExtractDescriptor(controlCaption, out controlCaption2, out defaultText);
			}
			else
			{
				defaultText = "";
			}
			string userInput = InputBox.GetUserInput(this, "Mark fields with a group descriptor", "Please enter the group descriptor:", defaultText);
			if (userInput != null)
			{
				foreach (TreeNodeAdv treeNodeAdv in this.tv_design.SelectedNodes)
				{
					MyIconNode myIconNode = (MyIconNode)this._model.FindNode(this.tv_design.GetPath(treeNodeAdv));
					string controlCaption2;
					this.ExtractDescriptor(myIconNode.DynamicControl.ControlCaption, out controlCaption2, out defaultText);
					myIconNode.DynamicControl.ControlCaption = controlCaption2;
					if (userInput.Length > 0)
					{
						DynamicControl dynamicControl = myIconNode.DynamicControl;
						dynamicControl.ControlCaption = dynamicControl.ControlCaption + "~~" + userInput;
					}
					this.UpdateTreeNodes(this._model.Nodes, myIconNode.DynamicControl);
				}
				this.propertyGrid1.Refresh();
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000EB60 File Offset: 0x0000DB60
		private void _model_NodesChanged()
		{
			TreeNodeAdv treeNodeAdv = this.tv_design.SelectedNodes[0];
			MyIconNode myIconNode = (treeNodeAdv != null) ? ((MyIconNode)this._model.FindNode(this.tv_design.GetPath(treeNodeAdv))) : null;
			DynamicControl dynamicControl = (myIconNode != null) ? myIconNode.DynamicControl : null;
			if (myIconNode != null)
			{
				dynamicControl.ControlCaption = myIconNode.Text;
				this.propertyGrid1.Refresh();
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000EBD3 File Offset: 0x0000DBD3
		private void tv_design_TextChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000EBD6 File Offset: 0x0000DBD6
		private void btn_apply_Click(object sender, EventArgs e)
		{
			this.SaveChanges(false);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000EBE1 File Offset: 0x0000DBE1
		private void tp_preview_Click(object sender, EventArgs e)
		{
			this.p_data.Visible = true;
			MessageBox.Show(this.p_data.Parent.Name);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000EC07 File Offset: 0x0000DC07
		private void p_data_VisibleChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000EC0C File Offset: 0x0000DC0C
		private void btn_generateDefaultValuesXml_Click(object sender, EventArgs e)
		{
			if (this.tabControl1.SelectedTab == this.tp_preview)
			{
				InputBox.GetUserInput(this, "", "", "xml {sfkljjk eoijroiew jrfjdsl jfj dsf", 200);
			}
			else
			{
				MessageBox.Show("This only works in the preview tab.");
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000EC60 File Offset: 0x0000DC60
		private void btn_pullInPreviouslyDeletedField_Click(object sender, EventArgs e)
		{
			this.da.SelectCommand.CommandText = "SELECT dc.controlid,dc.controlcaption FROM dynamiccontrols dc WHERE NOT dc.controlid IN (SELECT controlid FROM dynamicscreencontrols) ORDER BY dc.controlcaption";
			this.da.SelectCommand.Parameters.Clear();
			DataTable dataTable = new DataTable();
			this.da.Fill(dataTable);
			InputListView inputListView = new InputListView("Recover a deleted field", "Please select the field you would like to recover:", dataTable.DefaultView, -1, false, false);
			DialogResult dialogResult = inputListView.ShowDialog(this);
			if (dialogResult == DialogResult.OK && inputListView.LV.SelectedItems.Count > 0)
			{
				ListViewItem listViewItem = inputListView.LV.SelectedItems[0];
				DataRow dataRow = (DataRow)listViewItem.Tag;
				int num = (int)dataRow[0];
				int screenNum = this.screenInfo.screenNum;
				this.da.SelectCommand.CommandText = "INSERT INTO dynamicscreencontrols (screennum,controlid,ordernum,isactive,p,statsholding) VALUES (@sn,@cid,99999,1,'',0)";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@sn", screenNum);
				this.da.SelectCommand.Parameters.Add("@cid", num);
				this.da.Fill(new DataTable());
				MessageBox.Show("Done.  Please save your changes and re-open this form to see the new field appeared.");
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000EDBD File Offset: 0x0000DDBD
		private void ScreenEditor_KeyUp(object sender, KeyEventArgs e)
		{
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000EDC0 File Offset: 0x0000DDC0
		private void Find()
		{
			string userInput = InputBox.GetUserInput(this, "Search", "Enter search string", this.lastSearchString);
			if (userInput != null && userInput.Trim().Length > 0)
			{
				this.Find(userInput);
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000EE0C File Offset: 0x0000DE0C
		private void Find(string searchString)
		{
			this.lastSearchString = searchString;
			string value = searchString.ToLower();
			bool flag = this.lastFoundNode == null;
			TreeNodeAdv treeNodeAdv = this.lastFoundNode;
			this.lastFoundNode = null;
			TreeNodeAdv treeNodeAdv2 = null;
			using (IEnumerator<TreeNodeAdv> enumerator = this.tv_design.AllNodes.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					TreeNodeAdv treeNodeAdv3 = enumerator.Current;
					treeNodeAdv2 = treeNodeAdv3;
				}
			}
			while (treeNodeAdv2 != null)
			{
				if (flag)
				{
					Node node = treeNodeAdv2.Tag as Node;
					if (node.Text.ToLower().IndexOf(value) >= 0)
					{
						this.tv_design.ClearSelection();
						treeNodeAdv2.IsSelected = true;
						this.tv_design.EnsureVisible(treeNodeAdv2);
						this.lastFoundNode = treeNodeAdv2;
						break;
					}
				}
				else if (treeNodeAdv2 == treeNodeAdv)
				{
					flag = true;
				}
				if (treeNodeAdv2.Children.Count > 0)
				{
					treeNodeAdv2 = treeNodeAdv2.Children[0];
				}
				else
				{
					while (treeNodeAdv2.NextNode == null)
					{
						treeNodeAdv2 = treeNodeAdv2.Parent;
						if (treeNodeAdv2 == null)
						{
							break;
						}
					}
					if (treeNodeAdv2 != null && treeNodeAdv2.NextNode != null)
					{
						treeNodeAdv2 = treeNodeAdv2.NextNode;
					}
				}
				if (treeNodeAdv2 == null)
				{
					break;
				}
			}
			if (this.lastFoundNode == null)
			{
				MessageBox.Show("Search string not found.");
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000EFC8 File Offset: 0x0000DFC8
		private void findToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Find();
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000EFD4 File Offset: 0x0000DFD4
		private void tv_design_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F3 || (e.Control && e.KeyCode == Keys.F))
			{
				this.Find();
			}
			else if (e.KeyCode == Keys.Return)
			{
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000F02C File Offset: 0x0000E02C
		private void ScreenEditor_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F3 || (e.Control && e.KeyCode == Keys.F))
			{
				this.Find();
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000F06C File Offset: 0x0000E06C
		private void btn_multiLineTextbox_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000F070 File Offset: 0x0000E070
		private void whatOtherFormsDoesThisControlBelongToToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.tv_design.SelectedNodes.Count > 0)
			{
				List<int> list = new List<int>();
				bool flag = false;
				foreach (TreeNodeAdv treeNodeAdv in this.tv_design.SelectedNodes)
				{
					MyIconNode myIconNode = (MyIconNode)this._model.FindNode(this.tv_design.GetPath(treeNodeAdv));
					int controlId = myIconNode.DynamicControl.ControlId;
					if (controlId < 1)
					{
						flag = true;
					}
					else
					{
						list.Add(controlId);
					}
				}
				if (flag)
				{
					MessageBox.Show("At least one of the fields you chose has not been saved to the database yet.  This means it doesn't have a control id assigned yet, and can't belong to other forms; there will be no results for this/these fields.");
				}
				string commandText = "SELECT DISTINCT s.description,dsc.controlid,dc.controlcaption,s.screennum,s.typecode\r\nFROM dynamiccontrols dc LEFT JOIN dynamicscreencontrols dsc ON dsc.controlid=dc.controlid\r\n\tLEFT JOIN screens s ON s.screennum=dsc.screennum\r\nWHERE dc.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))\r\nORDER BY s.description,dc.controlcaption";
				this.da.SelectCommand.CommandText = commandText;
				this.da.SelectCommand.Parameters.Clear();
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < list.Count; i++)
				{
					stringBuilder.AppendFormat("{0}{1}", (i == 0) ? "" : ",", list[i].ToString());
				}
				this.da.SelectCommand.Parameters.Add("@cids", stringBuilder.ToString());
				DataTable t = new DataTable();
				this.da.Fill(t);
				DataTableView.ShowDataTableView(t);
			}
			else
			{
				MessageBox.Show("Please select at least one field first");
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000F224 File Offset: 0x0000E224
		private void editThelistToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.tv_design.SelectedNodes.Count == 1)
			{
				MyIconNode myIconNode = (MyIconNode)this._model.FindNode(this.tv_design.GetPath(this.tv_design.SelectedNodes[0]));
				if (myIconNode.DynamicControl.ControlCode == 3 || myIconNode.DynamicControl.ControlCode == 703 || myIconNode.DynamicControl.ControlCode == 14)
				{
					int setting = myIconNode.DynamicControl.Setting1;
					if (setting > 0)
					{
						ListViewItem listViewItem = null;
						foreach (object obj in this.lv_lists.Items)
						{
							ListViewItem listViewItem2 = (ListViewItem)obj;
							DynamicListGroup dynamicListGroup = (DynamicListGroup)listViewItem2.Tag;
							int lookupGroupId = dynamicListGroup.LookupGroupId;
							if (lookupGroupId == setting)
							{
								listViewItem = listViewItem2;
								break;
							}
						}
						if (listViewItem != null)
						{
							DynamicListGroup dynamicListGroup2 = (DynamicListGroup)listViewItem.Tag;
							int lookupGroupId2 = dynamicListGroup2.LookupGroupId;
							if (lookupGroupId2 > 0)
							{
								this.showListEditDialog(lookupGroupId2, dynamicListGroup2.Description, this.da);
							}
							else
							{
								MessageBox.Show("Missing the list!");
							}
						}
						else
						{
							MessageBox.Show(this, "Missing list!");
						}
					}
					else
					{
						MessageBox.Show("There is no list currently set for this field.  Please set the list first in the property box.");
					}
				}
				else
				{
					MessageBox.Show("This is not a type of control that has a list attached to it.");
				}
			}
			else
			{
				MessageBox.Show("Please select exactly one field first");
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000F400 File Offset: 0x0000E400
		private void btn_list_rename_Click(object sender, EventArgs e)
		{
			DynamicListGroup selectedLookupGroupId = this.GetSelectedLookupGroupId();
			if (selectedLookupGroupId != null && selectedLookupGroupId.LookupGroupId > 0)
			{
				string userInput = InputBox.GetUserInput(this, "Rename list", "Please enter the new name for the list", selectedLookupGroupId.Description, true);
				if (!string.IsNullOrEmpty(userInput))
				{
					this.da.SelectCommand.CommandText = "UPDATE lookupgroups SET description=@s WHERE lookupgroupid=@id";
					this.da.SelectCommand.Parameters.Clear();
					this.da.SelectCommand.Parameters.Add("@s", userInput);
					this.da.SelectCommand.Parameters.Add("@id", selectedLookupGroupId.LookupGroupId);
					this.da.Fill(new DataTable());
					this.ReloadLookupLists(selectedLookupGroupId.LookupGroupId);
				}
			}
			else
			{
				MessageBox.Show("Please select a list first.");
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000F4F0 File Offset: 0x0000E4F0
		private void btn_list_delete_Click(object sender, EventArgs e)
		{
			DynamicListGroup selectedLookupGroupId = this.GetSelectedLookupGroupId();
			if (selectedLookupGroupId != null && selectedLookupGroupId.LookupGroupId > 0)
			{
				if (DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(this.da, DatabaseVersionManager.ClockWorkFeature.LookupGroupVisible))
				{
					DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this list?", string.Format("Delete list [{0}]", selectedLookupGroupId.Description), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dialogResult == DialogResult.Yes)
					{
						this.da.SelectCommand.CommandText = "UPDATE lookupgroups SET isvisible=0 WHERE lookupgroupid=@id";
						this.da.SelectCommand.Parameters.Clear();
						this.da.SelectCommand.Parameters.Add("@id", selectedLookupGroupId.LookupGroupId);
						this.da.Fill(new DataTable());
						this.ReloadLookupLists(0);
					}
				}
				else
				{
					MessageBox.Show("Your database requires an update.");
				}
			}
			else
			{
				MessageBox.Show("Please select a list first.");
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000F5EC File Offset: 0x0000E5EC
		private void btn_list_undelete_Click(object sender, EventArgs e)
		{
			if (DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(this.da, DatabaseVersionManager.ClockWorkFeature.LookupGroupVisible))
			{
				this.da.SelectCommand.CommandText = "SELECT lookupgroupid,description FROM lookupgroups WHERE isvisible=0 ORDER BY description";
				this.da.SelectCommand.Parameters.Clear();
				DataTable dataTable = new DataTable();
				this.da.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					InputListView inputListView = new InputListView("Un-delete a list", "Please select the list that you would like to un-delete:", dataTable.DefaultView, -1, false, false);
					DialogResult dialogResult = inputListView.ShowDialog(this);
					if (dialogResult == DialogResult.OK && inputListView.LV.SelectedItems.Count > 0)
					{
						DataRow dataRow = (DataRow)inputListView.LV.SelectedItems[0].Tag;
						int num = (int)dataRow["lookupgroupid"];
						this.da.SelectCommand.CommandText = "UPDATE lookupgroups SET isvisible=1 WHERE lookupgroupid=@id";
						this.da.SelectCommand.Parameters.Clear();
						this.da.SelectCommand.Parameters.Add("@id", num);
						this.da.Fill(new DataTable());
						this.ReloadLookupLists(num);
					}
				}
				else
				{
					MessageBox.Show("There are no deleted lists in the system.");
				}
			}
			else
			{
				MessageBox.Show("Your database requires an update.");
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000F76C File Offset: 0x0000E76C
		private void ReloadLookupLists(int lookupGroupIdToSelect)
		{
			this.helperClass.ReloadLookupListGroups();
			this.ListsToScreen();
			if (lookupGroupIdToSelect > 0)
			{
				this.SelectList(lookupGroupIdToSelect);
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000F7A4 File Offset: 0x0000E7A4
		private DynamicListGroup GetSelectedLookupGroupId()
		{
			DynamicListGroup result;
			if (this.lv_lists.SelectedItems.Count == 1)
			{
				ListViewItem listViewItem = this.lv_lists.SelectedItems[0];
				DynamicListGroup dynamicListGroup = (DynamicListGroup)listViewItem.Tag;
				result = dynamicListGroup;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0400007D RID: 125
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x0400007E RID: 126
		private int screenTypeCode = 0;

		// Token: 0x0400007F RID: 127
		private ShowListEditDialog showListEditDialog = null;

		// Token: 0x04000080 RID: 128
		private UnivDataAdapter da;

		// Token: 0x04000081 RID: 129
		private NodeStateIcon icon;

		// Token: 0x04000082 RID: 130
		private List<DynamicControlControl> dynamicControlControls;

		// Token: 0x04000083 RID: 131
		private ScreenInfo screenInfo;

		// Token: 0x04000084 RID: 132
		private List<DynamicControl> deletedDynamicControls;

		// Token: 0x04000085 RID: 133
		private int newCid = -1;

		// Token: 0x04000087 RID: 135
		private DynamicControlWrapper_HelperClass helperClass;

		// Token: 0x04000088 RID: 136
		private string lastNewCaptions = "";

		// Token: 0x04000089 RID: 137
		private TreeNodeAdv lastFoundNode = null;

		// Token: 0x0400008A RID: 138
		private string lastSearchString = "";
	}
}
