using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DynamicScreens.Properties;
using EncryptionClassLibrary;
using TechnoPro.Common.UI.WinForms.CoreComponents.Controls;
using UnivOleDb;

namespace DynamicScreens.AdminTools
{
	// Token: 0x0200004F RID: 79
	public partial class ScreensEditor : Form
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x0003B6C0 File Offset: 0x0003A6C0
		public ScreensEditor()
		{
			this.InitializeComponent();
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000436 RID: 1078 RVA: 0x0003B6DC File Offset: 0x0003A6DC
		// (remove) Token: 0x06000437 RID: 1079 RVA: 0x0003B718 File Offset: 0x0003A718
		public event CompileFormCodeBehindHandler OnFormCodeBehindCompileRequest;

		// Token: 0x06000438 RID: 1080 RVA: 0x0003B754 File Offset: 0x0003A754
		public ScreensEditor(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ShowListEditDialog showListEditDialog)
		{
			this.da = da;
			this.tripleDES = tripleDES;
			this.showListEditDialog = showListEditDialog;
			this.InitializeComponent();
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0003B782 File Offset: 0x0003A782
		private void toolStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0003B788 File Offset: 0x0003A788
		private void ScreensEditor_Load(object sender, EventArgs e)
		{
			this.LoadScreens();
			this.ScreensToScreen();
			this.tv.ExpandAll();
			if (this.tv.Nodes.Count > 0)
			{
				this.tv.SelectedNode = this.tv.Nodes[0];
				this.tv.Nodes[0].EnsureVisible();
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0003B800 File Offset: 0x0003A800
		private string GetGroupName(DataRow dr, bool hasLongDescription)
		{
			string text;
			if (!hasLongDescription)
			{
				text = "";
			}
			else if (dr["longdescription"] == DBNull.Value)
			{
				text = "";
			}
			else
			{
				text = (string)dr["longdescription"];
			}
			if (text.Length < 1)
			{
				text = "Un-grouped";
			}
			return text;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0003B868 File Offset: 0x0003A868
		private void ScreensToScreen()
		{
			bool hasLongDescription = this.screensTable.Columns.Contains("longdescription");
			this.tv.BeginUpdate();
			this.tv.SuspendLayout();
			this.tv.Nodes.Clear();
			TreeNodeCollection nodes = this.tv.Nodes;
			int j;
			for (int i = 0; i < this.screensTable.Rows.Count; i = j)
			{
				DataRow dataRow = this.screensTable.Rows[i];
				string groupName = this.GetGroupName(dataRow, hasLongDescription);
				int num = (int)dataRow["typecode"];
				for (j = i + 1; j < this.screensTable.Rows.Count; j++)
				{
					DataRow dataRow2 = this.screensTable.Rows[j];
					string groupName2 = this.GetGroupName(dataRow2, hasLongDescription);
					if (groupName2.ToLower().Trim().CompareTo(groupName.ToLower().Trim()) != 0)
					{
						break;
					}
				}
				TreeNode treeNode = new TreeNode(groupName);
				treeNode.ImageIndex = 0;
				treeNode.SelectedImageIndex = 0;
				nodes.Add(treeNode);
				TreeNode treeNode2 = new TreeNode(Enum.GetName(typeof(ScreenType), num));
				treeNode2.ImageIndex = 126;
				treeNode2.SelectedImageIndex = 126;
				treeNode.Nodes.Add(treeNode2);
				for (int k = i; k < j; k++)
				{
					DataRow dataRow2 = this.screensTable.Rows[k];
					string text = dataRow2["description"].ToString();
					bool flag = Convert.ToBoolean(dataRow2["isactive"]);
					ScreenType screenType = (ScreenType)((int)dataRow2["typecode"]);
					string name = Enum.GetName(typeof(ScreenType), screenType);
					if (name.CompareTo(treeNode2.Text) != 0)
					{
						treeNode2 = new TreeNode(name);
						treeNode2.ImageIndex = 126;
						treeNode2.SelectedImageIndex = 126;
						treeNode.Nodes.Add(treeNode2);
					}
					if (!flag)
					{
						text = "[INACTIVE] " + text;
					}
					int num2 = (int)dataRow2["largeiconindex"] + 2;
					TreeNode treeNode3 = new TreeNode(text);
					treeNode3.ImageIndex = (flag ? num2 : 1);
					treeNode3.SelectedImageIndex = num2;
					treeNode3.Tag = dataRow2;
					if (!flag)
					{
						treeNode3.ForeColor = SystemColors.InactiveCaptionText;
					}
					treeNode2.Nodes.Add(treeNode3);
				}
			}
			this.tv.EndUpdate();
			this.tv.ResumeLayout();
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0003BB49 File Offset: 0x0003AB49
		private void LoadScreens()
		{
			this.da.SelectCommand.CommandText = "SELECT * FROM screens ORDER BY longdescription,typecode,isactive DESC,description";
			this.screensTable = new DataTable();
			this.da.Fill(this.screensTable);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0003BB7F File Offset: 0x0003AB7F
		private void btn_close_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0003BB89 File Offset: 0x0003AB89
		private void tv_DoubleClick(object sender, EventArgs e)
		{
			this.EditSelectedNode();
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0003BB93 File Offset: 0x0003AB93
		private void btn_editSelectedForm_Click(object sender, EventArgs e)
		{
			this.EditSelectedNode();
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0003BBA0 File Offset: 0x0003ABA0
		private void EditSelectedNode()
		{
			TreeNode selectedNode = this.tv.SelectedNode;
			if (selectedNode != null && selectedNode.Tag is DataRow)
			{
				DataRow dataRow = (DataRow)selectedNode.Tag;
				int screenNum = (int)dataRow["screennum"];
				ScreenEditor screenEditor = new ScreenEditor(this.da, screenNum, this.tripleDES, this.showListEditDialog);
				screenEditor.OnFormCodeBehindCompileRequest += this.se_OnFormCodeBehindCompileRequest;
				screenEditor.ShowDialog(this);
				screenEditor.OnFormCodeBehindCompileRequest -= this.se_OnFormCodeBehindCompileRequest;
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0003BC40 File Offset: 0x0003AC40
		private void se_OnFormCodeBehindCompileRequest(object sender, string code_load, string code_misc, string code_preSave, int screenNum)
		{
			if (this.OnFormCodeBehindCompileRequest != null)
			{
				this.OnFormCodeBehindCompileRequest(sender, code_load, code_misc, code_preSave, screenNum);
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0003BC70 File Offset: 0x0003AC70
		private void AddNewScreen(int typeCode)
		{
			bool flag = DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(this.da, DatabaseVersionManager.ClockWorkFeature.ScreensExtended);
			string text = "";
			int num = 0;
			bool flag2 = false;
			DateTime now = DateTime.Now;
			Type type = text.GetType();
			Type type2 = num.GetType();
			Type type3 = flag2.GetType();
			Type type4 = now.GetType();
			DataRow dataRow = this.screensTable.NewRow();
			DataRow dataRow2 = dataRow;
			dataRow2["description"] = "New Screen";
			dataRow2["typecode"] = typeCode;
			dataRow2["bottomless"] = false;
			dataRow2["verticalcontrolpad"] = 0;
			dataRow2["columnwidth"] = 35;
			dataRow2["columnpad"] = 6;
			dataRow2["dateadded"] = DateTime.Now;
			dataRow2["datemodified"] = DateTime.Now;
			dataRow2["isactive"] = true;
			dataRow2["iconindex"] = -1;
			dataRow2["largeiconindex"] = -1;
			dataRow2["shorttext"] = "";
			dataRow2["studentnamenumeditable"] = false;
			dataRow2["screenid"] = 0;
			dataRow2["showasbutton"] = true;
			dataRow2["fontname"] = "";
			dataRow2["fontsize"] = 0;
			dataRow2["groupids"] = "";
			dataRow2["iswebscreen"] = false;
			dataRow2["longdescription"] = "";
			dataRow2["controlidtoactivate"] = 0;
			dataRow2["studentnumbercaption"] = "";
			dataRow2["studentnumberautogeneraterule"] = "";
			dataRow2["studentnamehidden"] = false;
			ScreenDetails screenDetails = new ScreenDetails(dataRow, this.da, this.imageList4, this.imageList2);
			DialogResult dialogResult = screenDetails.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				this.da.SelectCommand.CommandText = "SELECT MAX(screennum) FROM screens";
				DataTable dataTable = new DataTable();
				this.da.Fill(dataTable);
				int num3;
				if (dataTable.Rows.Count > 0)
				{
					int num2 = (int)dataTable.Rows[0][0];
					num3 = num2 + 1;
				}
				else
				{
					num3 = 25;
				}
				this.da.SelectCommand.CommandText = "INSERT INTO screens (screennum,description,typecode,bottomless,verticalcontrolpad,columnwidth,columnpad,dateadded,datemodified,isactive,iconindex,largeiconindex,shorttext,studentnamenumeditable,showasbutton,fontname,fontsize,groupids,iswebscreen,longdescription,controlidtoactivate, studentnumbercaption,studentnumberautogeneraterule,studentnamehidden) SELECT @screennum,@description,@typecode,@bottomless,@verticalcontrolpad,@columnwidth,@columnpad,@dateadded,@datemodified,@isactive,@iconindex,@largeiconindex,@shorttext,@studentnamenumeditable,@showasbutton,@fontname,@fontsize,@groupids,@iswebscreen,@longdescription,@controlidtoactivate,@studentnumbercaption,@studentnumberautogeneraterule,@studentnamehidden";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@screennum", num3);
				this.da.SelectCommand.Parameters.Add("@description", dataRow["description"]);
				this.da.SelectCommand.Parameters.Add("@typecode", dataRow["typecode"]);
				this.da.SelectCommand.Parameters.Add("@bottomless", dataRow["bottomless"]);
				this.da.SelectCommand.Parameters.Add("@verticalcontrolpad", dataRow["verticalcontrolpad"]);
				this.da.SelectCommand.Parameters.Add("@columnwidth", dataRow["columnwidth"]);
				this.da.SelectCommand.Parameters.Add("@columnpad", dataRow["columnpad"]);
				this.da.SelectCommand.Parameters.Add("@dateadded", dataRow["dateadded"]);
				this.da.SelectCommand.Parameters.Add("@datemodified", dataRow["datemodified"]);
				this.da.SelectCommand.Parameters.Add("@isactive", dataRow["isactive"]);
				this.da.SelectCommand.Parameters.Add("@iconindex", dataRow["iconindex"]);
				this.da.SelectCommand.Parameters.Add("@largeiconindex", dataRow["largeiconindex"]);
				this.da.SelectCommand.Parameters.Add("@shorttext", dataRow["shorttext"]);
				this.da.SelectCommand.Parameters.Add("@studentnamenumeditable", dataRow["studentnamenumeditable"]);
				this.da.SelectCommand.Parameters.Add("@showasbutton", dataRow["showasbutton"]);
				this.da.SelectCommand.Parameters.Add("@fontname", dataRow["fontname"]);
				this.da.SelectCommand.Parameters.Add("@fontsize", dataRow["fontsize"]);
				this.da.SelectCommand.Parameters.Add("@groupids", dataRow["groupids"]);
				this.da.SelectCommand.Parameters.Add("@iswebscreen", dataRow["iswebscreen"]);
				this.da.SelectCommand.Parameters.Add("@longdescription", dataRow["longdescription"]);
				this.da.SelectCommand.Parameters.Add("@controlidtoactivate", dataRow["controlidtoactivate"]);
				this.da.SelectCommand.Parameters.Add("@studentnumbercaption", dataRow["studentnumbercaption"]);
				this.da.SelectCommand.Parameters.Add("@studentnumberautogeneraterule", dataRow["studentnumberautogeneraterule"]);
				this.da.SelectCommand.Parameters.Add("@studentnamehidden", dataRow["studentnamehidden"]);
				DataTable t = new DataTable();
				string text2;
				this.da.Fill(t, out text2);
				if (text2 != null && text2.Length > 0)
				{
					MessageBox.Show(text2);
				}
				this.RefreshScreen(num3);
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0003C35E File Offset: 0x0003B35E
		private void btn_addNewForm_Click(object sender, EventArgs e)
		{
			this.AddNewForm();
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0003C368 File Offset: 0x0003B368
		private void AddNewForm()
		{
			ScreenTypeChooser screenTypeChooser = new ScreenTypeChooser();
			DialogResult dialogResult = screenTypeChooser.ShowDialog(this);
			if (dialogResult == DialogResult.OK && screenTypeChooser.SelectedScreenType > -1)
			{
				this.AddNewScreen(screenTypeChooser.SelectedScreenType);
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0003C3AA File Offset: 0x0003B3AA
		private void btn_editScreen_Click(object sender, EventArgs e)
		{
			this.EditSelectedScreenDetails();
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0003C3B4 File Offset: 0x0003B3B4
		private void EditSelectedScreenDetails()
		{
			TreeNode selectedNode = this.tv.SelectedNode;
			if (selectedNode != null && selectedNode.Tag is DataRow)
			{
				DataRow dataRow = (DataRow)selectedNode.Tag;
				int num = (int)dataRow["screennum"];
				this.da.SelectCommand.CommandText = "SELECT screennum,description,typecode,bottomless,verticalcontrolpad,columnwidth,columnpad,dateadded,datemodified,isactive,iconindex,largeiconindex,shorttext,studentnamenumeditable,screenid,showasbutton,fontname,fontsize,groupids,iswebscreen,longdescription,controlidtoactivate, studentnumbercaption,studentnumberautogeneraterule,studentnamehidden FROM screens wHERE screennum=@sn";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@sn", num);
				DataTable dataTable = new DataTable();
				this.da.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					ScreenDetails screenDetails = new ScreenDetails(dataTable.Rows[0], this.da, this.imageList4, this.imageList2);
					DialogResult dialogResult = screenDetails.ShowDialog(this);
					if (dialogResult == DialogResult.OK)
					{
						ScreenDetails.WriteScreenChangesToDatabase(this.da, screenDetails.ScreenDr);
						this.RefreshScreen(num);
					}
				}
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0003C4E0 File Offset: 0x0003B4E0
		private void RefreshScreen(int screenNumToSelect)
		{
			this.LoadScreens();
			this.ScreensToScreen();
			this.tv.ExpandAll();
			this.SelectScreen(screenNumToSelect);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0003C508 File Offset: 0x0003B508
		private void SelectScreen(int screenNum)
		{
			TreeNode treeNode = this.FindScreen(this.tv.Nodes, screenNum);
			if (treeNode != null)
			{
				this.tv.SelectedNode = treeNode;
				treeNode.EnsureVisible();
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0003C548 File Offset: 0x0003B548
		private TreeNode FindScreen(TreeNodeCollection parent, int screenNum)
		{
			foreach (object obj in parent)
			{
				TreeNode treeNode = (TreeNode)obj;
				if (treeNode.Tag is DataRow)
				{
					DataRow dataRow = (DataRow)treeNode.Tag;
					int num = (int)dataRow["screennum"];
					if (num == screenNum)
					{
						return treeNode;
					}
				}
				if (treeNode.Nodes.Count > 0)
				{
					TreeNode treeNode2 = this.FindScreen(treeNode.Nodes, screenNum);
					if (treeNode2 != null)
					{
						return treeNode2;
					}
				}
			}
			return null;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0003C63C File Offset: 0x0003B63C
		private void btn_save_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0003C63F File Offset: 0x0003B63F
		private void btn_editFormDetails_Click(object sender, EventArgs e)
		{
			this.EditSelectedScreenDetails();
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0003C649 File Offset: 0x0003B649
		private void editfieldsOnFormToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.EditSelectedNode();
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0003C654 File Offset: 0x0003B654
		private void toggleEnabledToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = this.tv.SelectedNode;
			if (selectedNode != null && selectedNode.Tag is DataRow)
			{
				DataRow dataRow = (DataRow)selectedNode.Tag;
				int num = (int)dataRow["screennum"];
				bool flag = (bool)dataRow["isactive"];
				flag = !flag;
				this.da.SelectCommand.CommandText = "UPDATE screens SET isactive=@isactive WHERE screennum=@sn";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@isactive", flag);
				this.da.SelectCommand.Parameters.Add("@sn", num);
				DataTable t = new DataTable();
				this.da.Fill(t);
				this.RefreshScreen(num);
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0003C74E File Offset: 0x0003B74E
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0003C758 File Offset: 0x0003B758
		private void importScreenFromXmlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = this.tv.SelectedNode;
			if (selectedNode != null && selectedNode.Tag is DataRow)
			{
				DataRow dataRow = (DataRow)selectedNode.Tag;
				int num = (int)dataRow["screennum"];
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "XML files|*.xml|All files|*.*";
				openFileDialog.Title = "Select the form XML file you would like to import:";
				DialogResult dialogResult = openFileDialog.ShowDialog(this);
				if (dialogResult == DialogResult.OK)
				{
					if (File.Exists(openFileDialog.FileName))
					{
						try
						{
							DataSet dataSet = new DataSet();
							dataSet.ReadXml(openFileDialog.FileName, XmlReadMode.ReadSchema);
							if (dataSet.Tables.Count > 0)
							{
								int num2 = num;
								DataTable dataTable = dataSet.Tables["lookupgroups"];
								DataTable dataTable2 = dataSet.Tables["lookuplists"];
								DataTable dataTable3 = dataSet.Tables["dynamiccontrols"];
								foreach (object obj in dataTable.Rows)
								{
									DataRow dataRow2 = (DataRow)obj;
									int num3 = (int)dataRow2["lookupgroupid"];
									if (num3 >= 0)
									{
										string parameterValue = ((string)dataRow2["description"]).ToLower().Trim();
										this.da.SelectCommand.CommandText = "SELECT * FROM lookupgroups WHERE description=@d";
										this.da.SelectCommand.Parameters.Clear();
										this.da.SelectCommand.Parameters.Add("@d", parameterValue);
										DataTable dataTable4 = new DataTable();
										this.da.Fill(dataTable4);
										int num4;
										if (dataTable4.Rows.Count < 1)
										{
											if (dataTable4.Columns.Contains("sortby"))
											{
												this.da.SelectCommand.CommandText = "INSERT INTO lookupgroups (description,sortby) VALUES (@d," + dataRow2["sortby"].ToString() + ")";
											}
											else
											{
												this.da.SelectCommand.CommandText = "INSERT INTO lookupgroups (description) VALUES (@d)";
											}
											dataTable4 = new DataTable();
											num4 = this.da.FillReturnIdentity(dataTable4, "lookupgroupid", "lookupgroups");
										}
										else
										{
											num4 = (int)dataTable4.Rows[0]["lookupgroupid"];
										}
										foreach (object obj2 in dataTable3.Rows)
										{
											DataRow dataRow3 = (DataRow)obj2;
											int num5 = (int)dataRow3["lookupgroupid"];
											if (num5 > -1 && num5 == num3)
											{
												dataRow3["setting1"] = num4;
											}
										}
										foreach (object obj3 in dataTable2.Rows)
										{
											DataRow dataRow4 = (DataRow)obj3;
											int num5 = (int)dataRow4["lookupgroupid"];
											if (num5 == num3)
											{
												if (dataTable2.Columns.Contains("lookupvalue"))
												{
													this.da.SelectCommand.CommandText = "INSERT INTO lookuplists (lookupgroupid,lookuptext,ordernum,lookupvalue,visible) SELECT @lgi,@LT AS lookuptext,@ordernum AS ordernum,@lookupvalue AS lookupvalue,@visible AS visible WHERE NOT EXISTS(SELECT lookuplistid FROM lookuplists WHERE lookupgroupid=@lgi AND lookuptext=@lt)";
												}
												else
												{
													this.da.SelectCommand.CommandText = "INSERT INTO lookuplists (lookupgroupid,lookuptext,ordernum) SELECT @lgi,@LT AS lookuptext,@ordernum AS ordernum WHERE NOT EXISTS(SELECT lookuplistid FROM lookuplists WHERE lookupgroupid=@lgi AND lookuptext=@lt)";
												}
												this.da.SelectCommand.Parameters.Clear();
												this.da.SelectCommand.Parameters.Add("@lgi", num4);
												this.da.SelectCommand.Parameters.Add("@lt", dataRow4["lookuptext"]);
												this.da.SelectCommand.Parameters.Add("@ordernum", dataRow4["ordernum"]);
												if (dataTable2.Columns.Contains("lookupvalue"))
												{
													this.da.SelectCommand.Parameters.Add("@lookupvalue", dataRow4["lookupvalue"]);
													this.da.SelectCommand.Parameters.Add("@visible", dataRow4["visible"]);
												}
												this.da.Fill(new DataTable());
											}
										}
									}
								}
								int num6 = 5000;
								foreach (object obj4 in dataTable3.Rows)
								{
									DataRow dataRow2 = (DataRow)obj4;
									int num7 = this.InsertControl(dataRow2);
									if (num7 > 0)
									{
										this.da.SelectCommand.CommandText = "INSERT INTO dynamicscreencontrols (screennum,controlid,ordernum,isactive) VALUES (@screennum,@controlid,@ordernum,@isactive)";
										this.da.SelectCommand.Parameters.Clear();
										this.da.SelectCommand.Parameters.Add("@screennum", num2);
										this.da.SelectCommand.Parameters.Add("@controlid", num7);
										this.da.SelectCommand.Parameters.Add("@ordernum", num6);
										this.da.SelectCommand.Parameters.Add("@isactive", true);
										this.da.Fill(new DataTable());
									}
									num6++;
								}
								MessageBox.Show("Done.");
							}
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.ToString());
						}
					}
				}
			}
			else
			{
				MessageBox.Show("Please select the form you would like to import into first.");
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0003CE64 File Offset: 0x0003BE64
		private int InsertControl(DataRow dr)
		{
			this.da.SelectCommand.CommandText = "INSERT INTO dynamiccontrols (controlcode,controlcaption,setting1,setting2,setting3,defaultvalue,ControlName,ControlGroup,HelpText,HelpTextDisplayMethod,Mask,Enforce,ActionHandlers,DefaultValueString,Setting4String,enabled,readonly,hidecaption,setting4,fontsize,dontwraptonextline,specialcontroltype) \r\nVALUES (@controlcode,@controlcaption,@setting1,@setting2,@setting3,@defaultvalue,@ControlName,@ControlGroup,@HelpText,@HelpTextDisplayMethod,@Mask,@Enforce,@ActionHandlers,@DefaultValueString,@Setting4String,@enabled,@readonly,@hidecaption,@setting4,@fontsize,@dontwraptonextline,@specialcontroltype)";
			this.da.SelectCommand.Parameters.Clear();
			this.da.SelectCommand.Parameters.Add("@controlcode", dr[2]);
			this.da.SelectCommand.Parameters.Add("@controlcaption", dr[3]);
			this.da.SelectCommand.Parameters.Add("@setting1", dr[4]);
			this.da.SelectCommand.Parameters.Add("@setting2", dr[5]);
			this.da.SelectCommand.Parameters.Add("@setting3", dr[6]);
			this.da.SelectCommand.Parameters.Add("@defaultvalue", dr[7]);
			this.da.SelectCommand.Parameters.Add("@ControlName", dr[8]);
			this.da.SelectCommand.Parameters.Add("@ControlGroup", dr[9]);
			this.da.SelectCommand.Parameters.Add("@HelpText", dr[10]);
			this.da.SelectCommand.Parameters.Add("@HelpTextDisplayMethod", dr[11]);
			this.da.SelectCommand.Parameters.Add("@Mask", dr[12]);
			this.da.SelectCommand.Parameters.Add("@Enforce", dr[13]);
			this.da.SelectCommand.Parameters.Add("@ActionHandlers", dr[14]);
			this.da.SelectCommand.Parameters.Add("@DefaultValueString", dr[15]);
			this.da.SelectCommand.Parameters.Add("@Setting4String", dr[16]);
			this.da.SelectCommand.Parameters.Add("@enabled", dr[17]);
			this.da.SelectCommand.Parameters.Add("@readonly", dr[18]);
			this.da.SelectCommand.Parameters.Add("@hidecaption", dr[19]);
			this.da.SelectCommand.Parameters.Add("@setting4", dr[20]);
			this.da.SelectCommand.Parameters.Add("@fontsize", dr[21]);
			this.da.SelectCommand.Parameters.Add("@dontwraptonextline", dr[22]);
			this.da.SelectCommand.Parameters.Add("@specialcontroltype", (dr.Table != null && dr.Table.Columns.Contains("specialcontroltype") && dr["specialcontroltype"] != DBNull.Value) ? ((int)dr["specialcontroltype"]) : 0);
			DataTable dataTable = new DataTable();
			return this.da.FillReturnIdentity(dataTable, "controlid", "dynamiccontrols");
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0003D1FC File Offset: 0x0003C1FC
		private void exportToXmlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = this.tv.SelectedNode;
			if (selectedNode != null && selectedNode.Tag is DataRow)
			{
				DataRow dataRow = (DataRow)selectedNode.Tag;
				int screenNum = (int)dataRow["screennum"];
				bool isActive = dataRow["isactive"] == DBNull.Value || (bool)dataRow["isactive"];
				this.ExportToXml(screenNum, dataRow["description"].ToString(), isActive, null);
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0003D294 File Offset: 0x0003C294
		private void exportallFormsToXmlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			DialogResult dialogResult = folderBrowserDialog.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				ArrayList arrayList = new ArrayList();
				this.GetAllFormNodes(ref arrayList, this.tv.Nodes);
				foreach (object obj in arrayList)
				{
					TreeNode treeNode = (TreeNode)obj;
					if (treeNode.Tag is DataRow)
					{
						DataRow dataRow = (DataRow)treeNode.Tag;
						int screenNum = (int)dataRow["screennum"];
						bool isActive = dataRow["isactive"] == DBNull.Value || (bool)dataRow["isactive"];
						this.ExportToXml(screenNum, dataRow["description"].ToString(), isActive, folderBrowserDialog.SelectedPath);
					}
				}
				MessageBox.Show("Done");
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0003D3C8 File Offset: 0x0003C3C8
		private void GetAllFormNodes(ref ArrayList nodes, TreeNodeCollection parentNodeCollection)
		{
			foreach (object obj in parentNodeCollection)
			{
				TreeNode treeNode = (TreeNode)obj;
				if (treeNode.Tag is DataRow)
				{
					nodes.Add(treeNode);
				}
				if (treeNode.Nodes.Count > 0)
				{
					this.GetAllFormNodes(ref nodes, treeNode.Nodes);
				}
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0003D468 File Offset: 0x0003C468
		private DataTable LoadControls()
		{
			bool flag = DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(this.da, DatabaseVersionManager.ClockWorkFeature.DynamicScreenControlExtendedDescriptionFields_Mar_07);
			this.da.SelectCommand.CommandText = "SELECT    dc.controlid,-1 AS screennum,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue,\r\n            dc.ControlName,dc.ControlGroup,dc.HelpText,dc.HelpTextDisplayMethod,dc.Mask,dc.Enforce,dc.ActionHandlers,dc.DefaultValueString,\r\n            dc.Setting4String,dc.enabled,dc.readonly,dc.hidecaption,dc.setting4,dc.fontsize,dc.dontwraptonextline,\r\n            dc.uniqueid,dc.specialcontroltype\r\nFROM dynamiccontrols dc ORDER BY dc.controlcaption";
			DataTable dataTable = new DataTable();
			string text;
			this.da.Fill(dataTable, out text);
			if (text != null && text.Length > 0)
			{
				MessageBox.Show(text);
			}
			return dataTable;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0003D4D4 File Offset: 0x0003C4D4
		private void ExportToXml(int screenNum, string screenTitle, bool isActive, string folder)
		{
			DataTable dataTable = this.LoadControls();
			this.da.SelectCommand.CommandText = "SELECT dynamicscreencontrolid,screennum,controlid,ordernum,isactive FROM dynamicscreencontrols ORDER BY screennum,ordernum";
			DataTable dataTable2 = new DataTable();
			this.da.Fill(dataTable2);
			DataTable dataTable3 = dataTable.Copy();
			DataTable dataTable4 = dataTable2.Copy();
			dataTable3.AcceptChanges();
			dataTable4.AcceptChanges();
			DataView dataView = new DataView(dataTable4);
			dataView.Sort = "ordernum";
			DataTable dataTable5 = dataView.Table.Clone();
			dataTable5.TableName = "dynamicscreencontrols";
			DataTable dataTable6 = dataTable.Clone();
			dataTable6.TableName = "dynamiccontrols";
			dataTable6.Columns.Add("lookupgroupid", typeof(int));
			DataTable dataTable7 = new DataTable("lookupgroups");
			DataTable dataTable8 = new DataTable("lookuplists");
			this.da.SelectCommand.CommandText = "SELECT * FROM lookupgroups WHERE 1=0";
			this.da.Fill(dataTable7);
			this.da.SelectCommand.CommandText = "SELECT * FROM lookuplists WHERE 1=0";
			this.da.Fill(dataTable8);
			DataTable dataTable9 = new DataTable("screens");
			foreach (object obj in dataView)
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow row = dataRowView.Row;
				int num = (int)row[1];
				if (num == screenNum)
				{
					if (dataTable9.Rows.Count < 1)
					{
						this.da.SelectCommand.CommandText = "SELECT * FROM screens WHERE screennum=" + num.ToString();
						this.da.Fill(dataTable9);
					}
					int controlID = (int)row[2];
					DataRow controlDataRow = this.GetControlDataRow(dataTable3, controlID);
					if (controlDataRow != null)
					{
						DataRow dataRow = dataTable6.NewRow();
						for (int i = 0; i < dataTable6.Columns.Count; i++)
						{
							string columnName = dataTable6.Columns[i].ColumnName;
							int num2 = dataTable.Columns.IndexOf(columnName);
							if (num2 >= 0)
							{
								dataRow[i] = controlDataRow[num2];
							}
						}
						DataRow dataRow2 = dataTable5.NewRow();
						for (int i = 0; i < dataTable5.Columns.Count; i++)
						{
							string columnName = dataTable5.Columns[i].ColumnName;
							int num2 = dataView.Table.Columns.IndexOf(columnName);
							if (num2 >= 0)
							{
								dataRow2[i] = row[num2];
							}
						}
						int num3 = (int)controlDataRow["controlcode"];
						int num4 = -1;
						if (num3 == 3 || num3 == 14 || num3 == 10)
						{
							int num5 = (int)controlDataRow["setting1"];
							if (num5 > 0)
							{
								num4 = num5;
							}
						}
						else if (num3 == 100)
						{
							dataRow[dataTable6.Columns.IndexOf("setting1")] = 0;
						}
						if (num4 > -1)
						{
							dataRow[dataTable6.Columns.Count - 1] = num4;
							bool flag = false;
							for (int i = 0; i < dataTable7.Rows.Count; i++)
							{
								int num6 = (int)dataTable7.Rows[i]["lookupgroupid"];
								if (num6 == num4)
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								this.da.SelectCommand.CommandText = "SELECT * FROM lookupgroups WHERE lookupgroupid=" + num4.ToString();
								DataTable dataTable10 = new DataTable();
								this.da.Fill(dataTable10);
								if (dataTable10.Rows.Count > 0)
								{
									DataRow dataRow3 = dataTable7.NewRow();
									DataRow dataRow4 = dataTable10.Rows[0];
									for (int i = 0; i < dataRow4.Table.Columns.Count; i++)
									{
										int num2 = dataTable7.Columns.IndexOf(dataRow4.Table.Columns[i].ColumnName);
										if (num2 >= 0)
										{
											dataRow3[num2] = dataRow4[i];
										}
									}
									dataTable7.Rows.Add(dataRow3);
									this.da.SelectCommand.CommandText = "SELECT * FROM lookuplists WHERE lookupgroupid=" + num4.ToString();
									DataTable dataTable11 = new DataTable();
									this.da.Fill(dataTable11);
									foreach (object obj2 in dataTable11.Rows)
									{
										DataRow dataRow5 = (DataRow)obj2;
										object[] array = new object[dataTable11.Columns.Count];
										DataRow dataRow6 = dataTable8.NewRow();
										for (int i = 0; i < dataTable11.Columns.Count; i++)
										{
											int num2 = dataRow6.Table.Columns.IndexOf(dataTable11.Columns[i].ColumnName);
											if (num2 >= 0)
											{
												dataRow6[num2] = dataRow5[i];
											}
										}
										dataTable8.Rows.Add(dataRow6);
									}
								}
							}
						}
						else
						{
							dataRow[dataTable6.Columns.Count - 1] = -1;
						}
						dataTable6.Rows.Add(dataRow);
						dataTable5.Rows.Add(dataRow2);
					}
				}
			}
			DataSet dataSet = new DataSet();
			dataSet.Tables.Add(dataTable6);
			dataSet.Tables.Add(dataTable5);
			dataSet.Tables.Add(dataTable7);
			dataSet.Tables.Add(dataTable8);
			dataSet.Tables.Add(dataTable9);
			string text = screenTitle;
			if (!isActive)
			{
				text += "__INACTIVE__";
			}
			text = text.Replace(" ", "_").Replace("/", ".").Replace("\\", ".").Replace(",", ".");
			text = text + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xml";
			string text2;
			if (folder == null)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Filter = "XML files|*.xml|All files|*.*";
				saveFileDialog.Title = "Save controls xml file as";
				saveFileDialog.FileName = text;
				DialogResult dialogResult = saveFileDialog.ShowDialog(this);
				if (dialogResult != DialogResult.OK)
				{
					return;
				}
				text2 = saveFileDialog.FileName;
			}
			else
			{
				text2 = Path.Combine(folder, text);
			}
			dataSet.WriteXml(text2, XmlWriteMode.WriteSchema);
			StreamReader streamReader = new StreamReader(text2);
			string text3 = streamReader.ReadToEnd();
			streamReader.Close();
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0003DCC8 File Offset: 0x0003CCC8
		private DataRow GetControlDataRow(DataTable t, int controlID)
		{
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow[0];
				if (num == controlID)
				{
					return dataRow;
				}
			}
			return null;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0003DD54 File Offset: 0x0003CD54
		private void btn_addNewScreen_Click(object sender, EventArgs e)
		{
			this.AddNewForm();
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0003DD5E File Offset: 0x0003CD5E
		private void editFormdetailsToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0003DD61 File Offset: 0x0003CD61
		private void editFormFieldsToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0003DD64 File Offset: 0x0003CD64
		private void tv_MouseUp(object sender, MouseEventArgs e)
		{
			TreeView treeView = this.tv;
			if (e.Button == MouseButtons.Right)
			{
				Point point = new Point(e.X, e.Y);
				TreeNode nodeAt = treeView.GetNodeAt(point);
				if (nodeAt != null)
				{
					treeView.SelectedNode = nodeAt;
					this.cm_form.Show(treeView, point);
				}
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0003DDCC File Offset: 0x0003CDCC
		private void MENU_deleteForm_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = this.tv.SelectedNode;
			if (selectedNode != null && selectedNode.Tag is DataRow)
			{
				DataRow dataRow = (DataRow)selectedNode.Tag;
				int num = (int)dataRow["screennum"];
				if (num == 4)
				{
					MessageBox.Show("The accommodations form is special and you cannot delete it.");
				}
				else
				{
					this.da.SelectCommand.CommandText = "SELECT controlid FROM dynamicscreencontrols WHERE screennum=" + num.ToString();
					this.da.SelectCommand.Parameters.Clear();
					DataTable dataTable = new DataTable();
					this.da.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						MessageBox.Show("You cannot delete a form that contains controls.  Please remove all controls and then try deleting again.");
					}
					else
					{
						DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this form?", "Delete Form", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
						if (dialogResult == DialogResult.Yes)
						{
							this.da.SelectCommand.CommandText = "DELETE FROM screens WHERE screennum=" + num.ToString();
							this.da.Fill(new DataTable());
							this.RefreshScreen(-1);
						}
					}
				}
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0003DF18 File Offset: 0x0003CF18
		private void tv_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.EditSelectedNode();
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0003DF40 File Offset: 0x0003CF40
		private void copyAllFieldsOnThisFormToAnotherFormToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = this.tv.SelectedNode;
			if (selectedNode != null && selectedNode.Tag is DataRow)
			{
				DataRow dataRow = (DataRow)selectedNode.Tag;
				int num = (int)dataRow["screennum"];
				DialogResult dialogResult = MessageBox.Show("This function will copy all fields from the form you right-clicked on, to another form that you will select in the next step.  Control ids will remain the same, which means that data entered by users into a field on this form will show up on both forms.  Would you like to continue?", "Copy fields to another form", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					string userInput = InputBox.GetUserInput("Screen to copy to", "Please enter the screen number of the form that the fields will be copied to:", this, "0", 0, true, false, 0);
					int num2;
					if (!string.IsNullOrEmpty(userInput) && int.TryParse(userInput, out num2) && num2 > 0)
					{
						this.da.SelectCommand.CommandText = "INSERT INTO dynamicscreencontrols (screennum,controlid,ordernum) SELECT @screennumdest,controlid,ordernum FROM dynamicscreencontrols WHERE screennum=@screennumsource";
						this.da.SelectCommand.Parameters.Clear();
						this.da.SelectCommand.Parameters.Add("@screennumsource", num);
						this.da.SelectCommand.Parameters.Add("@screennumdest", num2);
						string text;
						this.da.Fill(new DataTable(), out text);
						if (!string.IsNullOrEmpty(text))
						{
							MessageBox.Show("There was a problem: " + text);
						}
						else
						{
							MessageBox.Show("Done.");
						}
					}
				}
			}
		}

		// Token: 0x04000319 RID: 793
		private UnivDataAdapter da;

		// Token: 0x0400031A RID: 794
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x0400031B RID: 795
		private DataTable screensTable;

		// Token: 0x0400031C RID: 796
		private ShowListEditDialog showListEditDialog;
	}
}
