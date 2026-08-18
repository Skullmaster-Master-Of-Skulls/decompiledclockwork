using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using AutoComboBox.MyControls;
using EncryptionClassLibrary;
using UnivOleDb;

namespace AutoComboBox
{
	// Token: 0x0200006B RID: 107
	public class AutoComboBox : ComboBox, MyDynamicControl
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00020CE0 File Offset: 0x0001FCE0
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x00020CF8 File Offset: 0x0001FCF8
		public bool TryToSelectOnFocusLeave
		{
			get
			{
				return this.tryToSelectOnFocusLeave;
			}
			set
			{
				this.tryToSelectOnFocusLeave = value;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060003D9 RID: 985 RVA: 0x00020D04 File Offset: 0x0001FD04
		// (remove) Token: 0x060003DA RID: 986 RVA: 0x00020D40 File Offset: 0x0001FD40
		public event KeyPressEventHandler EnterPressed;

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00020D7C File Offset: 0x0001FD7C
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00020D94 File Offset: 0x0001FD94
		public UnivDataAdapter Da
		{
			get
			{
				return this.da;
			}
			set
			{
				this.da = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00020DA0 File Offset: 0x0001FDA0
		// (set) Token: 0x060003DE RID: 990 RVA: 0x00020DB8 File Offset: 0x0001FDB8
		public TripleDESEncryptionClass TripleDES
		{
			get
			{
				return this.tripleDES;
			}
			set
			{
				this.tripleDES = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00020DC4 File Offset: 0x0001FDC4
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x00020DDC File Offset: 0x0001FDDC
		public int Pid
		{
			get
			{
				return this.pid;
			}
			set
			{
				this.pid = value;
				if (this.pid > 0 && !string.IsNullOrEmpty(this.sql))
				{
					this.RunSql();
				}
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00020E18 File Offset: 0x0001FE18
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x00020E2F File Offset: 0x0001FE2F
		public MyTextBox MaskedTextBox { get; set; }

		// Token: 0x060003E3 RID: 995 RVA: 0x00020E38 File Offset: 0x0001FE38
		public DataTable RunSql()
		{
			if (!string.IsNullOrEmpty(this.sql) && this.da != null)
			{
				DataTable dataTable = new DataTable();
				this.da.SelectCommand.CommandText = this.sql;
				this.da.SelectCommand.Parameters.Clear();
				if (this.sql.IndexOf("@pid") >= 0)
				{
					this.da.SelectCommand.Parameters.Add("@pid", this.pid);
				}
				string text;
				this.da.Fill(dataTable, out text);
				if (string.IsNullOrEmpty(text))
				{
					byte[] array = new byte[0];
					Type type = array.GetType();
					List<string> list = new List<string>();
					foreach (object obj in dataTable.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj;
						if (dataColumn.DataType == type)
						{
							list.Add(dataColumn.ColumnName);
						}
					}
					if (list.Count > 0)
					{
						string[] array2 = new string[list.Count];
						list.CopyTo(array2);
						dataTable = this.tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, array2);
					}
					if (dataTable.Columns.Count > 2)
					{
						foreach (object obj2 in dataTable.Rows)
						{
							DataRow dataRow = (DataRow)obj2;
							string text2 = dataRow[1].ToString();
							for (int i = 2; i < dataTable.Columns.Count; i++)
							{
								text2 = text2 + " " + dataRow[i].ToString();
							}
							dataRow[1] = text2;
						}
						while (dataTable.Columns.Count > 2)
						{
							dataTable.Columns.RemoveAt(dataTable.Columns.Count - 1);
						}
					}
					if (dataTable.Rows.Count > 0)
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2[0] = -1;
						dataRow2[1] = "";
						dataTable.Rows.InsertAt(dataRow2, 0);
					}
					base.DataSource = dataTable;
					base.DisplayMember = (dataTable.Columns.Contains("LookupText") ? "LookupText" : dataTable.Columns[1].ColumnName);
					base.ValueMember = (dataTable.Columns.Contains("LookupListID") ? "LookupListID" : dataTable.Columns[0].ColumnName);
					this.LookupGroupId = -1;
					return dataTable;
				}
				MessageBox.Show(text);
			}
			return null;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x000211A4 File Offset: 0x000201A4
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x000211BC File Offset: 0x000201BC
		public string Sql
		{
			get
			{
				return this.sql;
			}
			set
			{
				this.sql = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x000211C8 File Offset: 0x000201C8
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x000211E0 File Offset: 0x000201E0
		public int CalcButtonCid
		{
			get
			{
				return this.calcButtonCid;
			}
			set
			{
				this.calcButtonCid = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x000211EC File Offset: 0x000201EC
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x00021204 File Offset: 0x00020204
		public string AltValueMember
		{
			get
			{
				return this.altValueMember;
			}
			set
			{
				this.altValueMember = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00021210 File Offset: 0x00020210
		// (set) Token: 0x060003EB RID: 1003 RVA: 0x00021228 File Offset: 0x00020228
		public bool IgnoreScrollWheel
		{
			get
			{
				return this.ignoreScrollWheel;
			}
			set
			{
				this.ignoreScrollWheel = value;
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00021234 File Offset: 0x00020234
		public void InformCalcButtonOfChange()
		{
			if (this.calcButtonCid > 0)
			{
				Control parent = ListViewEx.GetParent(this);
				Control control = ListViewEx.FindControl(parent, this.calcButtonCid);
				if (control != null && control is MyDynamicControl)
				{
					MyDynamicControl myDynamicControl = (MyDynamicControl)control;
					myDynamicControl.Refresh();
				}
			}
		}

		// Token: 0x170000D8 RID: 216
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x0002128E File Offset: 0x0002028E
		public MyCheckBox SyncedCheckbox
		{
			set
			{
				this.syncedCheckbox = value;
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00021298 File Offset: 0x00020298
		public void FromString(string s)
		{
			if (base.DataSource != null)
			{
				this.SelectIndexByDisplayMember(s);
			}
			else
			{
				this.Text = s;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x000212C8 File Offset: 0x000202C8
		public bool FilledIn
		{
			get
			{
				DataRow dataRow = this.SelectedDataRow();
				return dataRow != null && dataRow[base.DisplayMember].ToString().Length > 0;
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00021300 File Offset: 0x00020300
		public override string ToString()
		{
			return (string)this.ReportObject;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00021320 File Offset: 0x00020320
		public object ReportObject
		{
			get
			{
				DataRow dataRow = this.SelectedDataRow();
				return (dataRow == null) ? "" : dataRow[base.DisplayMember].ToString();
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060003F2 RID: 1010 RVA: 0x00021354 File Offset: 0x00020354
		// (remove) Token: 0x060003F3 RID: 1011 RVA: 0x00021390 File Offset: 0x00020390
		public event AutoComboBox.ToolTipPopupHandler OnTooltipPopup;

		// Token: 0x060003F4 RID: 1012 RVA: 0x000213CC File Offset: 0x000203CC
		private void FireOnTooltipPopup(string s)
		{
			if (this.OnTooltipPopup != null)
			{
				this.OnTooltipPopup(this, new EventArgs(), this.Text);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00021404 File Offset: 0x00020404
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0002141C File Offset: 0x0002041C
		public bool AllowUserToEnterAnyText
		{
			get
			{
				return this.allowUserToEnterAnyText;
			}
			set
			{
				this.allowUserToEnterAnyText = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00021428 File Offset: 0x00020428
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x00021440 File Offset: 0x00020440
		public int ChildLookupGroupId
		{
			get
			{
				return this.childLookupGroupId;
			}
			set
			{
				this.childLookupGroupId = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0002144C File Offset: 0x0002044C
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x00021464 File Offset: 0x00020464
		public int LookupGroupId
		{
			get
			{
				return this.lookupGroupId;
			}
			set
			{
				this.lookupGroupId = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00021470 File Offset: 0x00020470
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x00021488 File Offset: 0x00020488
		public bool GotoNextItemOnDoubleClick
		{
			get
			{
				return this.gotoNextItemOnDoubleClick;
			}
			set
			{
				this.gotoNextItemOnDoubleClick = value;
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00021494 File Offset: 0x00020494
		public AutoComboBox()
		{
			this.InitializeComponent();
			base.QueryAccessibilityHelp += this.AutoComboBox_QueryAccessibilityHelp;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0002154C File Offset: 0x0002054C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.syncedCheckbox = null;
				this.MaskedTextBox = null;
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00021598 File Offset: 0x00020598
		private void InitializeComponent()
		{
			base.AccessibleRole = AccessibleRole.ComboBox;
			base.CausesValidation = false;
			base.KeyDown += this.comboBox1_KeyDown;
			base.KeyUp += this.comboBox1_KeyUp;
			base.TextChanged += this.AutoComboBox_TextChanged;
			base.DataSourceChanged += this.UserControl1_DataSourceChanged;
			base.MouseUp += this.AutoComboBox_MouseUp;
			base.SelectionChangeCommitted += this.UserControl1_SelectionChangeCommitted;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00021629 File Offset: 0x00020629
		public void ClearSelection()
		{
			base.SelectionLength = 0;
			base.SelectionStart = 0;
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000401 RID: 1025 RVA: 0x0002163C File Offset: 0x0002063C
		// (remove) Token: 0x06000402 RID: 1026 RVA: 0x00021678 File Offset: 0x00020678
		public event AutoComboBox.UserSelectedHandler UserSelectedSomething;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000403 RID: 1027 RVA: 0x000216B4 File Offset: 0x000206B4
		// (remove) Token: 0x06000404 RID: 1028 RVA: 0x000216F0 File Offset: 0x000206F0
		public event AutoComboBox.UserSelectedSameItemHandler UserSelectedSameItem;

		// Token: 0x06000405 RID: 1029 RVA: 0x0002172C File Offset: 0x0002072C
		public int GetItemCount()
		{
			int result;
			if (base.DataSource == null)
			{
				result = 0;
			}
			else if (base.DataSource is DataTable)
			{
				result = ((DataTable)base.DataSource).Rows.Count;
			}
			else if (base.DataSource is DataView)
			{
				result = ((DataView)base.DataSource).Count;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000217AC File Offset: 0x000207AC
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				e.Handled = true;
				this.FireEnterPressed();
			}
			else
			{
				base.OnKeyPress(e);
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000217E8 File Offset: 0x000207E8
		private void FireEnterPressed()
		{
			if (this.EnterPressed != null)
			{
				this.EnterPressed(this, new KeyPressEventArgs('\r'));
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0002181C File Offset: 0x0002081C
		private void comboBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if (this.autoCompleteEnabled)
			{
				char c = Convert.ToChar(e.KeyCode);
				if (e.KeyCode == Keys.Back)
				{
					if (base.DropDownStyle != ComboBoxStyle.DropDownList && base.SelectionLength > 0)
					{
						AutoComboBox.MyText textMinusSelected = this.GetTextMinusSelected(this);
						this.Text = textMinusSelected.MyString;
						base.SelectionStart = textMinusSelected.SelectionStart;
						base.SelectionLength = textMinusSelected.SelectionLength;
					}
				}
				else if (e.KeyCode != Keys.Return)
				{
					if (e.KeyCode != Keys.Delete && e.KeyCode != Keys.Home && e.KeyCode != Keys.End && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
					{
						if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
						{
							if (e.KeyCode != Keys.Next && e.KeyCode != Keys.Prior)
							{
								if ((char.IsLetterOrDigit(c) || char.IsPunctuation(c) || e.KeyCode == Keys.Space) && base.SelectionLength == this.Text.Length)
								{
									int num = base.FindStringExact(this.Text);
									if (num >= 0)
									{
										string s = this.Text + c;
										int num2 = base.FindString(s);
										if (num2 >= 0)
										{
											base.SelectionStart = this.Text.Length;
											base.SelectionLength = 0;
										}
										else
										{
											this.Text = "";
										}
										e.Handled = true;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00021A12 File Offset: 0x00020A12
		public void SetDataSource(object NewDataSource, string NewDisplayMember, string NewValueMember)
		{
			base.DataSource = NewDataSource;
			base.DisplayMember = NewDisplayMember;
			base.ValueMember = NewValueMember;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00021A30 File Offset: 0x00020A30
		public void CopyDataSource(AutoComboBox cmb)
		{
			if (cmb.DataSource is DataTable)
			{
				DataTable dataTable = (DataTable)cmb.DataSource;
				DataTable newDataSource = dataTable.Copy();
				this.SetDataSource(newDataSource, cmb.DisplayMember, cmb.ValueMember);
			}
			else
			{
				DataView dataView = (DataView)cmb.DataSource;
				DataTable dataTable = dataView.Table.Copy();
				this.SetDataSource(new DataView(dataTable)
				{
					Sort = dataView.Sort
				}, cmb.DisplayMember, cmb.ValueMember);
			}
		}

		// Token: 0x170000DF RID: 223
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x00021AC2 File Offset: 0x00020AC2
		public int LastSelectedIndex
		{
			set
			{
				this.lastSelectedIndex = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00021ACC File Offset: 0x00020ACC
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x00021AE4 File Offset: 0x00020AE4
		public int CidToNotifyWithValueMember
		{
			get
			{
				return this.cidToNotifyWithValueMember;
			}
			set
			{
				this.cidToNotifyWithValueMember = value;
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00021AF0 File Offset: 0x00020AF0
		private void RaiseEventUserSelectedSomething(int newSelectedIndex)
		{
			if (this.lastSelectedIndex != newSelectedIndex)
			{
				if (this.syncedCheckbox != null)
				{
					this.syncedCheckbox.Checked = (newSelectedIndex >= 0);
				}
				this.NotifyChildren();
				this.lastSelectedIndex = newSelectedIndex;
				if (this.UserSelectedSomething != null)
				{
					this.UserSelectedSomething(this);
				}
			}
			else if (this.UserSelectedSameItem != null)
			{
				this.UserSelectedSameItem(this);
			}
			if (this.cidToNotifyWithValueMember > 0)
			{
				Control topLevelControl = base.TopLevelControl;
				if (topLevelControl != null)
				{
					DataRow dataRow = this.SelectedDataRow();
					if (dataRow != null)
					{
						string val = dataRow["lookupvalue"].ToString();
						this.SetValue(topLevelControl, this.cidToNotifyWithValueMember, val);
					}
				}
			}
			this.InformCalcButtonOfChange();
			this.InformTextBoxOfChange();
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00021BDC File Offset: 0x00020BDC
		private void InformTextBoxOfChange()
		{
			if (this.MaskedTextBox != null)
			{
				DataRow dataRow = this.SelectedDataRow();
				if (dataRow == null)
				{
					this.MaskedTextBox.UpdateMask("");
				}
				else
				{
					this.MaskedTextBox.UpdateMask(dataRow[base.DisplayMember].ToString());
				}
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00021C3C File Offset: 0x00020C3C
		private bool SetValue(Control parent, int cid, string val)
		{
			if (parent.Tag is DataRow)
			{
				DataRow dataRow = (DataRow)parent.Tag;
				if (dataRow.Table.Columns.Contains("controlid"))
				{
					if ((int)dataRow["controlid"] == cid)
					{
						if (parent is TextBox)
						{
							TextBox textBox = (TextBox)parent;
							textBox.Text = val;
							return true;
						}
					}
				}
			}
			foreach (object obj in parent.Controls)
			{
				Control parent2 = (Control)obj;
				if (this.SetValue(parent2, cid, val))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00021D4C File Offset: 0x00020D4C
		private void comboBox1_KeyUp(object sender, KeyEventArgs e)
		{
			e.Handled = this.ComboKeyUp(e.KeyCode, e.Shift, e.Control, e.Alt);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00021D74 File Offset: 0x00020D74
		private bool ComboKeyUp(Keys keyCode, bool shiftPressed, bool ctrlPressed, bool altPressed)
		{
			try
			{
				if (base.DropDownStyle != ComboBoxStyle.Simple || base.DropDownStyle != ComboBoxStyle.DropDown)
				{
					char c = Convert.ToChar(keyCode);
					if (!altPressed && !ctrlPressed)
					{
						if (!base.DroppedDown && (keyCode == Keys.Return || ((keyCode == Keys.Up || keyCode == Keys.Down) && this.childLookupGroupId > 0)))
						{
							int num = base.FindStringExact(this.Text);
							if (num >= 0)
							{
								try
								{
									base.SelectionStart = 0;
									base.SelectionLength = this.Text.Length;
									this.RaiseEventUserSelectedSomething(num);
								}
								catch (Exception ex)
								{
								}
							}
							if (keyCode == Keys.Return && this.childLookupGroupId > 0)
							{
								AutoComboBox autoComboBox = AutoComboBox.FindComboBoxInSameParent(this, this.childLookupGroupId);
								if (autoComboBox != null)
								{
									autoComboBox.Focus();
								}
							}
						}
						else if (this.autoCompleteEnabled)
						{
							if (keyCode != Keys.Delete && keyCode != Keys.Home && keyCode != Keys.End && keyCode != Keys.Left && keyCode != Keys.Right)
							{
								if (keyCode == Keys.Up || keyCode == Keys.Down || keyCode == Keys.Next || keyCode == Keys.Prior)
								{
									return true;
								}
								if (keyCode == Keys.Space || char.IsLetterOrDigit(c) || char.IsPunctuation(c) || keyCode == Keys.None)
								{
									string text = this.Text;
									if (base.SelectionLength > 0)
									{
										return false;
									}
									int num = base.FindStringExact(text);
									int num2 = base.FindString(text);
									int length = text.Length;
									if (num >= 0 && length >= 0)
									{
										base.SelectionStart = 0;
										base.SelectionLength = length;
										bool flag = true;
										if (num < base.Items.Count - 1)
										{
											string text2 = this.GetItem(num + 1);
											if (text2.Length > text.Length)
											{
												text2 = text2.Substring(0, text.Length);
												if (text == text2)
												{
													flag = false;
												}
											}
										}
										if (flag)
										{
											this.RaiseEventUserSelectedSomething(num);
										}
									}
									else if (num2 >= 0)
									{
										int length2 = text.Length;
										string text2 = this.GetItem(num2);
										this.Text = text2;
										base.SelectionStart = length2;
										int num3 = this.Text.Length - base.SelectionStart;
										if (num3 < 0)
										{
											num3 = 0;
										}
										base.SelectionLength = num3;
										return true;
									}
								}
								else
								{
									if (keyCode == Keys.Delete)
									{
										this.Text = "";
										return false;
									}
									if (keyCode == Keys.Escape)
									{
										this.Text = "";
										return false;
									}
								}
							}
						}
					}
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000220E8 File Offset: 0x000210E8
		private int FindIndexOfText(DataView associatedDataView, string text)
		{
			DataView dataView = (associatedDataView == null) ? this.GetAssociatedDataView() : associatedDataView;
			string strB = text.ToLower().Trim();
			int result = -1;
			for (int i = 0; i < dataView.Count; i++)
			{
				DataRow row = dataView[i].Row;
				string text2 = row[base.DisplayMember].ToString().Trim().ToLower();
				if (text2.CompareTo(strB) == 0)
				{
					result = i;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00022178 File Offset: 0x00021178
		private DataView GetAssociatedDataView()
		{
			DataView result;
			if (base.DataSource == null)
			{
				result = null;
			}
			else if (base.DataSource is DataTable)
			{
				result = ((DataTable)base.DataSource).DefaultView;
			}
			else if (base.DataSource is DataView)
			{
				result = (DataView)base.DataSource;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000221EC File Offset: 0x000211EC
		private string GetItem(int index)
		{
			string result;
			if (base.DataSource == null)
			{
				result = base.Items[index].ToString();
			}
			else if (base.DataSource is DataTable)
			{
				result = this.GetDataTableItem((DataTable)base.DataSource, index);
			}
			else if (base.DataSource is DataView)
			{
				DataView dataView = (DataView)base.DataSource;
				DataRowView dataRowView = dataView[index];
				result = this.GetDataTableItem(dataRowView.Row);
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00022290 File Offset: 0x00021290
		private int GetDataRowIndex(DataRow dr0)
		{
			int result;
			if (base.DataSource == null)
			{
				result = -1;
			}
			else if (base.DataSource is DataTable)
			{
				DataTable dataTable = (DataTable)base.DataSource;
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					DataRow dataRow = dataTable.Rows[i];
					if (dataRow == dr0)
					{
						return i;
					}
				}
				result = -1;
			}
			else if (base.DataSource is DataView)
			{
				DataView dataView = (DataView)base.DataSource;
				for (int i = 0; i < dataView.Count; i++)
				{
					DataRow dataRow = dataView[i].Row;
					if (dataRow == dr0)
					{
						return i;
					}
				}
				result = -1;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0002238C File Offset: 0x0002138C
		private string GetDataTableItem(DataTable t, int index)
		{
			int num = t.Columns.IndexOf(base.DisplayMember);
			string result;
			if (num >= 0)
			{
				DataRow dataRow = t.Rows[index];
				if (dataRow.RowState == DataRowState.Deleted)
				{
					dataRow.RejectChanges();
					string text = dataRow[num].ToString();
					dataRow.Delete();
					result = text;
				}
				else
				{
					result = dataRow[num].ToString();
				}
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00022410 File Offset: 0x00021410
		private string GetDataTableItem(DataRow dr)
		{
			int num = dr.Table.Columns.IndexOf(base.DisplayMember);
			string result;
			if (num >= 0)
			{
				if (dr.RowState == DataRowState.Deleted)
				{
					dr.RejectChanges();
					string text = dr[num].ToString();
					dr.Delete();
					result = text;
				}
				else
				{
					result = dr[num].ToString();
				}
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00022488 File Offset: 0x00021488
		private AutoComboBox.MyText GetTextMinusSelected(ComboBox comboBox)
		{
			int num = base.SelectionStart;
			string text = "";
			if (comboBox.SelectionStart > 0)
			{
				text = comboBox.Text.Substring(0, comboBox.SelectionStart);
			}
			int num2 = comboBox.SelectionStart + comboBox.SelectionLength + 1;
			if (num2 < comboBox.Text.Length)
			{
				text += this.Text.Substring(num2);
			}
			if (num < 0)
			{
				num = 0;
			}
			AutoComboBox.MyText result = new AutoComboBox.MyText(text, num, 0);
			return result;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00022523 File Offset: 0x00021523
		private void UserControl1_SelectionChangeCommitted(object sender, EventArgs e)
		{
			this.RaiseEventUserSelectedSomething(this.SelectedIndex);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00022534 File Offset: 0x00021534
		private void SetSelectedIndex(int addToCurrentIndex)
		{
			try
			{
				int num = this.SelectedIndex + addToCurrentIndex;
				if (addToCurrentIndex < 0)
				{
					if (num >= 0)
					{
						this.SelectedIndex = num;
					}
					else
					{
						this.SelectedIndex = 0;
					}
				}
				else if (num < base.Items.Count)
				{
					this.SelectedIndex = num;
				}
				else
				{
					this.SelectedIndex = base.Items.Count - 1;
				}
				if (base.DropDownStyle != ComboBoxStyle.DropDownList)
				{
					base.SelectionLength = 0;
					base.SelectionStart = this.Text.Length;
					base.SelectionLength = 0;
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000225F4 File Offset: 0x000215F4
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Up)
			{
				this.SetSelectedIndex(-1);
				if (base.DroppedDown)
				{
					return true;
				}
				base.DroppedDown = true;
			}
			else if (keyData == Keys.Down)
			{
				this.SetSelectedIndex(1);
				if (base.DroppedDown)
				{
					return true;
				}
				base.DroppedDown = true;
			}
			else
			{
				if (keyData == Keys.Prior)
				{
					return true;
				}
				if (keyData == Keys.Next)
				{
					return true;
				}
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00022690 File Offset: 0x00021690
		private void UserControl1_DataSourceChanged(object sender, EventArgs e)
		{
			this.ResetLastSelectedIndex();
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0002269A File Offset: 0x0002169A
		public void ResetLastSelectedIndex()
		{
			this.lastSelectedIndex = -1;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000226A4 File Offset: 0x000216A4
		private void AutoComboBox_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Middle)
			{
				bool shiftPressed = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
				bool ctrlPressed = (Control.ModifierKeys & Keys.Control) == Keys.Control;
				bool altPressed = (Control.ModifierKeys & Keys.Alt) == Keys.Alt;
				this.ComboKeyUp(Keys.Return, shiftPressed, ctrlPressed, altPressed);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x00022710 File Offset: 0x00021710
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x00022728 File Offset: 0x00021728
		public bool AutoCompleteEnabled
		{
			get
			{
				return this.autoCompleteEnabled;
			}
			set
			{
				this.autoCompleteEnabled = value;
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00022734 File Offset: 0x00021734
		public int SelectedIndexByText()
		{
			int result;
			if (this.SelectedIndex >= 0)
			{
				result = this.SelectedIndex;
			}
			else
			{
				int num = base.FindStringExact(this.Text.Trim());
				result = num;
			}
			return result;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00022770 File Offset: 0x00021770
		public DataRow SelectedDataRow()
		{
			int num = this.SelectedIndexByText();
			DataRow result;
			if (num < 0)
			{
				result = null;
			}
			else if (base.DataSource is DataTable)
			{
				DataTable dataTable = (DataTable)base.DataSource;
				result = dataTable.Rows[num];
			}
			else if (base.DataSource is DataView)
			{
				DataView dataView = (DataView)base.DataSource;
				DataRowView dataRowView = dataView[num];
				result = dataRowView.Row;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0002280C File Offset: 0x0002180C
		public DataRow GetDataRow(int itemIndex)
		{
			DataRow result;
			if (base.DataSource is DataTable)
			{
				DataTable dataTable = (DataTable)base.DataSource;
				result = dataTable.Rows[itemIndex];
			}
			else if (base.DataSource is DataView)
			{
				DataView dataView = (DataView)base.DataSource;
				result = dataView[itemIndex].Row;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00022881 File Offset: 0x00021881
		private void AutoComboBox_TextChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00022884 File Offset: 0x00021884
		public void SelectIndexByTextContains(string subTextLCase)
		{
			if (!(base.ValueMember == ""))
			{
				if (base.DataSource is DataTable)
				{
					DataTable dataTable = (DataTable)base.DataSource;
					int num = dataTable.Columns.IndexOf(base.DisplayMember);
					if (num >= 0)
					{
						for (int i = 0; i < dataTable.Rows.Count; i++)
						{
							DataRow dataRow = dataTable.Rows[i];
							if (dataRow[num] != DBNull.Value)
							{
								string text = ((string)dataRow[num]).ToString().ToLower();
								if (text.IndexOf(subTextLCase) >= 0)
								{
									this.SelectedIndex = i;
									return;
								}
							}
						}
					}
				}
				else if (base.DataSource is DataView)
				{
					DataView dataView = (DataView)base.DataSource;
					DataTable dataTable = dataView.Table;
					int num = dataTable.Columns.IndexOf(base.DisplayMember);
					if (num >= 0)
					{
						int i = 0;
						foreach (object obj in dataView)
						{
							DataRowView dataRowView = (DataRowView)obj;
							DataRow dataRow = dataRowView.Row;
							if (dataRow[num] != DBNull.Value)
							{
								string text = ((string)dataRow[num]).ToString().ToLower();
								if (text.IndexOf(subTextLCase) >= 0)
								{
									this.SelectedIndex = i;
									return;
								}
							}
							i++;
						}
					}
				}
			}
			this.SelectedIndex = -1;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00022A84 File Offset: 0x00021A84
		public void SelectIndexByValueMember(int _value)
		{
			if (!(base.ValueMember == ""))
			{
				if (base.DataSource is DataTable)
				{
					DataTable dataTable = (DataTable)base.DataSource;
					int num = dataTable.Columns.IndexOf(base.ValueMember);
					if (num >= 0)
					{
						for (int i = 0; i < dataTable.Rows.Count; i++)
						{
							DataRow dataRow = dataTable.Rows[i];
							if (dataRow[num] != DBNull.Value)
							{
								int num2 = (int)dataRow[num];
								if (num2 == _value)
								{
									this.SelectedIndex = i;
									return;
								}
							}
						}
					}
				}
				else if (base.DataSource is DataView)
				{
					DataView dataView = (DataView)base.DataSource;
					DataTable dataTable = dataView.Table;
					int num = dataTable.Columns.IndexOf(base.ValueMember);
					if (num >= 0)
					{
						int i = 0;
						foreach (object obj in dataView)
						{
							DataRowView dataRowView = (DataRowView)obj;
							DataRow dataRow = dataRowView.Row;
							if (dataRow[num] != DBNull.Value)
							{
								int num2 = (int)dataRow[num];
								if (num2 == _value)
								{
									this.SelectedIndex = i;
									return;
								}
							}
							i++;
						}
					}
				}
			}
			this.SelectedIndex = -1;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00022C68 File Offset: 0x00021C68
		public void SelectIndexByDisplayMember(string _value)
		{
			if (base.DataSource != null && !(base.DisplayMember == ""))
			{
				if (base.DataSource is DataTable)
				{
					DataTable dataTable = (DataTable)base.DataSource;
					int num = dataTable.Columns.IndexOf(base.DisplayMember);
					if (num >= 0)
					{
						for (int i = 0; i < dataTable.Rows.Count; i++)
						{
							DataRow dataRow = dataTable.Rows[i];
							if (dataRow[num] != DBNull.Value)
							{
								string text = (string)dataRow[num];
								if (text.Equals(_value))
								{
									this.SelectedIndex = i;
									return;
								}
							}
						}
					}
				}
				else if (base.DataSource is DataView)
				{
					DataView dataView = (DataView)base.DataSource;
					DataTable dataTable = dataView.Table;
					int num = dataTable.Columns.IndexOf(base.DisplayMember);
					if (num >= 0)
					{
						int i = 0;
						foreach (object obj in dataView)
						{
							DataRowView dataRowView = (DataRowView)obj;
							DataRow dataRow = dataRowView.Row;
							if (dataRow[num] != DBNull.Value)
							{
								string text = (string)dataRow[num];
								if (text.Equals(_value))
								{
									this.SelectedIndex = i;
									return;
								}
							}
							i++;
						}
					}
				}
			}
			this.SelectedIndex = -1;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00022E5C File Offset: 0x00021E5C
		public int GetValue(int index)
		{
			if (base.DataSource is DataTable)
			{
				DataTable dataTable = (DataTable)base.DataSource;
				if (index >= 0 && index < dataTable.Rows.Count)
				{
					DataRow dataRow = dataTable.Rows[index];
					int num = dataTable.Columns.IndexOf(base.ValueMember);
					if (num >= 0)
					{
						return (int)dataRow[num];
					}
				}
			}
			return -1;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00022EEC File Offset: 0x00021EEC
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new AutoComboBoxAccessibleObject(this);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00022F04 File Offset: 0x00021F04
		private void AutoComboBox_QueryAccessibilityHelp(object sender, QueryAccessibilityHelpEventArgs e)
		{
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00022F08 File Offset: 0x00021F08
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (this.gotoNextItemOnDoubleClick)
			{
				TimeSpan timeSpan = DateTime.Now - this.lastMouseDown;
				this.lastMouseDown = DateTime.Now;
				base.OnMouseDown(e);
				if (timeSpan.TotalMilliseconds <= 600.0)
				{
					this.DoubleClicked(e);
				}
			}
			else
			{
				base.OnMouseDown(e);
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00022F74 File Offset: 0x00021F74
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (this.ignoreScrollWheel)
			{
				((HandledMouseEventArgs)e).Handled = true;
			}
			else
			{
				base.OnMouseWheel(e);
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00022FAC File Offset: 0x00021FAC
		private void DoubleClicked(MouseEventArgs e)
		{
			if (base.DataSource != null)
			{
				this.lastMouseDown = DateTime.Now.AddSeconds(-1.0);
				DataRow dr = this.SelectedDataRow();
				int num = this.GetDataRowIndex(dr) + 1;
				int itemCount = this.GetItemCount();
				if (num >= itemCount)
				{
					num = 0;
				}
				if (num < itemCount)
				{
					this.SelectedIndex = num;
				}
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00023020 File Offset: 0x00022020
		public void FilterList(string lookupListIds)
		{
			base.BeginUpdate();
			DataTable dataTable = (base.DataSource is DataTable) ? ((DataTable)base.DataSource) : ((DataView)base.DataSource).Table;
			if (this.originalTable == null)
			{
				this.originalTable = dataTable.Copy();
			}
			dataTable.Rows.Clear();
			this.SelectedIndex = -1;
			if (this.originalTable != dataTable)
			{
				foreach (object obj in this.originalTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					int num = (dataRow[0] == DBNull.Value) ? 0 : ((int)dataRow[0]);
					if (num <= 0 || this.ListContains(lookupListIds, num))
					{
						dataTable.Rows.Add(dataRow.ItemArray);
					}
				}
			}
			base.EndUpdate();
			if (this.childLookupGroupId > 0)
			{
				AutoComboBox autoComboBox = AutoComboBox.FindComboBoxInSameParent(this, this.childLookupGroupId);
				if (autoComboBox != null)
				{
					autoComboBox.FilterList("");
				}
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00023198 File Offset: 0x00022198
		private bool ListContains(string list, int item)
		{
			string strB = item.ToString().Trim();
			string[] array = list.Split(new char[]
			{
				','
			});
			foreach (string text in array)
			{
				if (text.Trim().CompareTo(strB) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00023214 File Offset: 0x00022214
		protected override void OnLeave(EventArgs e)
		{
			if (base.DropDownStyle == ComboBoxStyle.DropDownList)
			{
				base.OnLeave(e);
			}
			else
			{
				DataRow dataRow = this.SelectedDataRow();
				if (!this.AllowUserToEnterAnyText && dataRow == null)
				{
					base.SelectedText = "";
				}
				if (this.tryToSelectOnFocusLeave)
				{
					int num = base.FindStringExact(this.Text);
					if (num >= 0)
					{
						try
						{
							base.BeginUpdate();
							base.SelectionStart = 0;
							base.SelectionLength = this.Text.Length;
							base.EndUpdate();
							this.RaiseEventUserSelectedSomething(num);
							this.ClearSelection();
						}
						catch
						{
						}
					}
				}
				base.OnLeave(e);
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000232EC File Offset: 0x000222EC
		public static AutoComboBox FindComboBoxInSameParent(AutoComboBox combo, int childLookupGroupId)
		{
			Control parent = combo.Parent;
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				if (control is AutoComboBox)
				{
					AutoComboBox autoComboBox = (AutoComboBox)control;
					if (autoComboBox.LookupGroupId == combo.ChildLookupGroupId)
					{
						return autoComboBox;
					}
				}
			}
			return null;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0002339C File Offset: 0x0002239C
		private void NotifyChildren()
		{
			if (base.IsHandleCreated)
			{
				DataRow dataRow = this.SelectedDataRow();
				if (dataRow != null)
				{
					if (dataRow.RowState == DataRowState.Deleted)
					{
						dataRow.RejectChanges();
					}
					if (this.childLookupGroupId > 0)
					{
						if (dataRow != null && dataRow.Table.Columns.Contains("children"))
						{
							AutoComboBox autoComboBox = AutoComboBox.FindComboBoxInSameParent(this, this.childLookupGroupId);
							if (autoComboBox != null)
							{
								autoComboBox.FilterList(dataRow["children"].ToString());
							}
						}
					}
				}
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00023448 File Offset: 0x00022448
		public int GetSelectedValue()
		{
			DataRow dataRow = this.SelectedDataRow();
			if (dataRow != null && !string.IsNullOrEmpty(base.ValueMember) && dataRow.Table.Columns[base.ValueMember].DataType == typeof(int))
			{
				try
				{
					return (dataRow[base.ValueMember] == DBNull.Value) ? -1 : ((int)dataRow[base.ValueMember]);
				}
				catch
				{
					return -1;
				}
			}
			return -1;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000234E8 File Offset: 0x000224E8
		public string GetSelectedDescription()
		{
			DataRow dataRow = this.SelectedDataRow();
			if (dataRow != null && !string.IsNullOrEmpty(base.DisplayMember))
			{
				try
				{
					return dataRow[base.DisplayMember].ToString().Trim();
				}
				catch
				{
					return "";
				}
			}
			return "";
		}

		// Token: 0x040003BA RID: 954
		private Container components = null;

		// Token: 0x040003BB RID: 955
		private bool gotoNextItemOnDoubleClick = false;

		// Token: 0x040003BC RID: 956
		private bool tryToSelectOnFocusLeave = true;

		// Token: 0x040003BD RID: 957
		private bool ignoreScrollWheel = true;

		// Token: 0x040003BF RID: 959
		private string altValueMember = null;

		// Token: 0x040003C0 RID: 960
		private int calcButtonCid = 0;

		// Token: 0x040003C1 RID: 961
		private UnivDataAdapter da;

		// Token: 0x040003C2 RID: 962
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x040003C3 RID: 963
		private int pid = 0;

		// Token: 0x040003C4 RID: 964
		private string sql = "";

		// Token: 0x040003C5 RID: 965
		private MyCheckBox syncedCheckbox = null;

		// Token: 0x040003C7 RID: 967
		private bool allowUserToEnterAnyText = true;

		// Token: 0x040003C8 RID: 968
		private int childLookupGroupId = 0;

		// Token: 0x040003C9 RID: 969
		private int lookupGroupId = 0;

		// Token: 0x040003CA RID: 970
		public int defaultIndex = -1;

		// Token: 0x040003CD RID: 973
		private bool autoCompleteEnabled = true;

		// Token: 0x040003CE RID: 974
		private int lastSelectedIndex = -1;

		// Token: 0x040003CF RID: 975
		private int cidToNotifyWithValueMember = 0;

		// Token: 0x040003D0 RID: 976
		private DateTime lastMouseDown = DateTime.Now;

		// Token: 0x040003D1 RID: 977
		private DataTable originalTable = null;

		// Token: 0x0200006C RID: 108
		// (Invoke) Token: 0x06000437 RID: 1079
		public delegate void ToolTipPopupHandler(object sender, EventArgs e, string text);

		// Token: 0x0200006D RID: 109
		// (Invoke) Token: 0x0600043B RID: 1083
		public delegate void UserSelectedHandler(object sender);

		// Token: 0x0200006E RID: 110
		// (Invoke) Token: 0x0600043F RID: 1087
		public delegate void UserSelectedSameItemHandler(object sender);

		// Token: 0x0200006F RID: 111
		private struct MyText
		{
			// Token: 0x06000442 RID: 1090 RVA: 0x00023554 File Offset: 0x00022554
			public MyText(string MyString, int SelectionStart, int SelectionLength)
			{
				this.myString = MyString;
				this.selectionStart = SelectionStart;
				this.selectionLength = SelectionLength;
			}

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x06000443 RID: 1091 RVA: 0x0002356C File Offset: 0x0002256C
			// (set) Token: 0x06000444 RID: 1092 RVA: 0x00023584 File Offset: 0x00022584
			public string MyString
			{
				get
				{
					return this.myString;
				}
				set
				{
					this.myString = value;
				}
			}

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x06000445 RID: 1093 RVA: 0x00023590 File Offset: 0x00022590
			// (set) Token: 0x06000446 RID: 1094 RVA: 0x000235A8 File Offset: 0x000225A8
			public int SelectionStart
			{
				get
				{
					return this.selectionStart;
				}
				set
				{
					this.selectionStart = value;
				}
			}

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x06000447 RID: 1095 RVA: 0x000235B4 File Offset: 0x000225B4
			// (set) Token: 0x06000448 RID: 1096 RVA: 0x000235CC File Offset: 0x000225CC
			public int SelectionLength
			{
				get
				{
					return this.selectionLength;
				}
				set
				{
					this.selectionLength = value;
				}
			}

			// Token: 0x040003D3 RID: 979
			private string myString;

			// Token: 0x040003D4 RID: 980
			private int selectionStart;

			// Token: 0x040003D5 RID: 981
			private int selectionLength;
		}
	}
}
