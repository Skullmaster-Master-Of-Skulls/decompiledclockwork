using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AutoComboBox;
using AutoComboBox.MyControls;
using MathCalc;

namespace DynamicScreens.Controls
{
	// Token: 0x02000046 RID: 70
	public class CalculationButton : UserControl, MyDynamicControl
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x00033FA9 File Offset: 0x00032FA9
		public CalculationButton()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00033FD8 File Offset: 0x00032FD8
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x00033FF0 File Offset: 0x00032FF0
		public int MyCid
		{
			get
			{
				return this.myCid;
			}
			set
			{
				this.myCid = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00033FFC File Offset: 0x00032FFC
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x00034014 File Offset: 0x00033014
		public string LookupTable
		{
			get
			{
				return this.lookupTable;
			}
			set
			{
				this.lookupTable = value;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00034020 File Offset: 0x00033020
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x00034038 File Offset: 0x00033038
		public string Calculation
		{
			get
			{
				return this.calculation;
			}
			set
			{
				this.calculation = value;
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00034044 File Offset: 0x00033044
		public void SetupAutoRecalc()
		{
			List<CalcControl> list = this.ParseUniqueControls(this.calculation);
			foreach (CalcControl calcControl in list)
			{
				if (calcControl.Ctrl is ListViewEx)
				{
					ListViewEx listViewEx = (ListViewEx)calcControl.Ctrl;
					listViewEx.CalcButtonCid = this.myCid;
				}
				else if (calcControl.Ctrl is MyTextBox)
				{
					MyTextBox myTextBox = (MyTextBox)calcControl.Ctrl;
					myTextBox.CalcButtonCid = this.myCid;
				}
				else if (calcControl.Ctrl is AutoComboBox)
				{
					AutoComboBox autoComboBox = (AutoComboBox)calcControl.Ctrl;
					autoComboBox.CalcButtonCid = this.myCid;
				}
				else if (calcControl.Ctrl is MyDateTimePicker)
				{
					MyDateTimePicker myDateTimePicker = (MyDateTimePicker)calcControl.Ctrl;
					myDateTimePicker.CalcButtonCid = this.myCid;
				}
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00034180 File Offset: 0x00033180
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x00034198 File Offset: 0x00033198
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				this.button1.Text = base.Text;
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000341B8 File Offset: 0x000331B8
		private Control GetParent()
		{
			Control parent;
			for (parent = base.Parent; parent != null; parent = parent.Parent)
			{
				if (parent.Parent == null)
				{
					break;
				}
				if (parent.Parent is Form)
				{
					break;
				}
			}
			return parent;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00034214 File Offset: 0x00033214
		private Control FindControl(Control parent, int cid)
		{
			foreach (object obj in parent.Controls)
			{
				Control parent2 = (Control)obj;
				Control control = this.FindControl(parent2, cid);
				if (control != null)
				{
					return control;
				}
			}
			if (parent.Tag != null && parent.Tag is DataRow)
			{
				DataRow dataRow = (DataRow)parent.Tag;
				if (dataRow.Table.Columns.Contains("controlid"))
				{
					int num = (int)dataRow["controlid"];
					if (num == cid)
					{
						return parent;
					}
				}
			}
			return null;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00034314 File Offset: 0x00033314
		private void button1_Click(object sender, EventArgs e)
		{
			this.ReCalculate();
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00034320 File Offset: 0x00033320
		public void ReCalculate()
		{
			string[] array = this.calculation.Split(new char[]
			{
				'`'
			});
			bool flag = false;
			DateTime? dateTime = null;
			foreach (string text in array)
			{
				if (!string.IsNullOrEmpty(text))
				{
					try
					{
						int num = text.IndexOf("=");
						string s = text.Substring(0, num);
						int cid;
						try
						{
							cid = int.Parse(s);
						}
						catch
						{
							cid = 0;
						}
						string text2 = text.Substring(num + 1);
						bool flag2;
						if (text2.ToLower().IndexOf("lookup") == 0)
						{
							flag2 = true;
							text2 = text2.Substring(7, text2.Length - 8);
						}
						else
						{
							flag2 = false;
						}
						string pattern = "\\[(.*?)\\]";
						MatchCollection matchCollection = Regex.Matches(text2, pattern);
						string text3 = text2;
						Control parent = this.GetParent();
						if (parent == null)
						{
							break;
						}
						foreach (object obj in matchCollection)
						{
							Match match = (Match)obj;
							string text4 = match.Value;
							string text5 = text4.Replace("[", "").Replace("]", "");
							int num2 = text5.IndexOf(".");
							string text6;
							int num3;
							if (num2 > 0)
							{
								string s2 = text5.Substring(0, num2);
								text6 = text5.Substring(num2 + 1);
								try
								{
									num3 = int.Parse(s2);
								}
								catch
								{
									num3 = 0;
								}
							}
							else
							{
								text6 = "";
								try
								{
									num3 = int.Parse(text5);
								}
								catch
								{
									num3 = 0;
								}
							}
							double num4 = 0.0;
							string text7 = "";
							if (num3 > 0)
							{
								Control control = this.FindControl(parent, num3);
								if (control != null)
								{
									if (control is ListView)
									{
										ListView listView = (ListView)control;
										string value = text6.Trim().ToLower();
										int num5 = -1;
										for (int j = 0; j < listView.Columns.Count; j++)
										{
											ColumnHeader columnHeader = listView.Columns[j];
											string text8 = columnHeader.Text.ToLower().Trim();
											int num6 = text8.IndexOf('`');
											if (num6 > 0)
											{
												text8 = text8.Substring(0, num6);
											}
											if (text8.Equals(value))
											{
												num5 = j;
											}
										}
										if (num5 >= 0)
										{
											foreach (object obj2 in listView.Items)
											{
												ListViewItem listViewItem = (ListViewItem)obj2;
												text7 = listViewItem.SubItems[num5].Text;
												if (!string.IsNullOrEmpty(text7.Trim()))
												{
													double num7;
													if (!double.TryParse(text7, out num7))
													{
														num7 = 0.0;
													}
													num4 += num7;
												}
											}
										}
									}
									else if (control is TextBox)
									{
										TextBox textBox = (TextBox)control;
										text7 = textBox.Text.Trim();
										if (string.IsNullOrEmpty(text7.Trim()))
										{
											num4 = 0.0;
										}
										else if (!double.TryParse(text7, out num4))
										{
											num4 = 0.0;
										}
									}
									else if (control is DateTimePicker)
									{
										flag = true;
										dateTime = new DateTime?(((DateTimePicker)control).Value);
										num4 = 0.0;
									}
									else
									{
										text7 = control.Text;
										if (string.IsNullOrEmpty(text7.Trim()))
										{
											num4 = 0.0;
										}
										else if (!double.TryParse(text7, out num4))
										{
											num4 = 0.0;
										}
									}
								}
							}
							if (!flag2)
							{
								text3 = text3.Replace(text4, num4.ToString());
							}
							else
							{
								text3 = text3.Replace(text4, text7.Trim());
							}
						}
						string text9;
						if (!flag2)
						{
							mcCalc mcCalc = new mcCalc();
							text9 = mcCalc.evaluate(text3).ToString();
						}
						else
						{
							bool flag3 = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
							string[] array3 = this.lookupTable.Split(new char[]
							{
								','
							});
							string value2 = text3.ToLower().Trim().Replace("+", ".");
							text9 = "";
							foreach (string text4 in array3)
							{
								int num2 = text4.IndexOf('=');
								if (num2 > 0)
								{
									string text10 = text4.Substring(0, num2).ToLower();
									int num8 = text10.IndexOf("*");
									if (num8 >= 0)
									{
										text9 = text4.Substring(num2 + 1);
										break;
									}
									if (text10.Equals(value2))
									{
										text9 = text4.Substring(num2 + 1);
										break;
									}
								}
							}
						}
						Control control2 = this.FindControl(parent, cid);
						if (control2 != null)
						{
							bool flag4;
							if (control2.TopLevelControl != null && control2.TopLevelControl is Form)
							{
								Form form = (Form)control2.TopLevelControl;
								Control activeControl = form.ActiveControl;
								flag4 = (activeControl == control2);
							}
							else
							{
								flag4 = false;
							}
							if (!flag4)
							{
								if (flag)
								{
									if (dateTime != null && dateTime.Value != DateTime.MinValue)
									{
										DateTime value3 = dateTime.Value;
										DateTime date = DateTime.Now.Date;
										int num9 = date.Year - value3.Year;
										if (date.Month < value3.Month || (date.Month == value3.Month && date.Day < value3.Day))
										{
											num9--;
										}
										text9 = ((num9 > 0) ? num9.ToString() : "-");
									}
									else
									{
										text9 = "-";
									}
									control2.Text = text9;
								}
								else
								{
									control2.Text = text9;
								}
								if (control2 is MyTextBox)
								{
									MyTextBox myTextBox = (MyTextBox)control2;
									if (myTextBox.IsCurrency)
									{
										myTextBox.FixCurrency();
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
					}
				}
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00034B2C File Offset: 0x00033B2C
		private List<CalcControl> ParseUniqueControls(string calculation)
		{
			List<CalcControl> list = new List<CalcControl>();
			string[] array = calculation.Split(new char[]
			{
				'`'
			});
			foreach (string text in array)
			{
				if (!string.IsNullOrEmpty(text))
				{
					try
					{
						int num = text.IndexOf("=");
						string s = text.Substring(0, num);
						try
						{
							int num2 = int.Parse(s);
						}
						catch
						{
						}
						string input = text.Substring(num + 1);
						string pattern = "\\[(.*?)\\]";
						MatchCollection matchCollection = Regex.Matches(input, pattern);
						Control parent = this.GetParent();
						if (parent == null)
						{
							return list;
						}
						foreach (object obj in matchCollection)
						{
							Match match = (Match)obj;
							string value = match.Value;
							string text2 = value.Replace("[", "").Replace("]", "");
							int num3 = text2.IndexOf(".");
							int num4;
							if (num3 > 0)
							{
								string s2 = text2.Substring(0, num3);
								string text3 = text2.Substring(num3 + 1);
								try
								{
									num4 = int.Parse(s2);
								}
								catch
								{
									num4 = 0;
								}
							}
							else
							{
								try
								{
									num4 = int.Parse(text2);
								}
								catch
								{
									num4 = 0;
								}
							}
							if (num4 > 0)
							{
								Control control = this.FindControl(parent, num4);
								if (control != null)
								{
									CalcControl calcControl = new CalcControl(num4, control);
									if (!CalcControl.Exists(list, calcControl))
									{
										list.Add(calcControl);
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
					}
				}
			}
			return list;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00034DC4 File Offset: 0x00033DC4
		public bool FilledIn
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00034DD7 File Offset: 0x00033DD7
		public void FromString(string s)
		{
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00034DDC File Offset: 0x00033DDC
		public object ReportObject
		{
			get
			{
				return "";
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00034DF3 File Offset: 0x00033DF3
		public new void Refresh()
		{
			this.ReCalculate();
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00034E00 File Offset: 0x00033E00
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00034E38 File Offset: 0x00033E38
		private void InitializeComponent()
		{
			this.button1 = new Button();
			base.SuspendLayout();
			this.button1.Dock = DockStyle.Fill;
			this.button1.Location = new Point(2, 2);
			this.button1.Name = "button1";
			this.button1.Size = new Size(120, 29);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += this.button1_Click;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.button1);
			base.Name = "CalculationButton";
			base.Padding = new Padding(2);
			base.Size = new Size(124, 33);
			base.ResumeLayout(false);
		}

		// Token: 0x040002C8 RID: 712
		private string calculation = "";

		// Token: 0x040002C9 RID: 713
		private int myCid;

		// Token: 0x040002CA RID: 714
		private string lookupTable = "";

		// Token: 0x040002CB RID: 715
		private IContainer components = null;

		// Token: 0x040002CC RID: 716
		private Button button1;
	}
}
