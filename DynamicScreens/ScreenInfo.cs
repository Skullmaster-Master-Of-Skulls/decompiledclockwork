using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.MyControls;

namespace DynamicScreens
{
	// Token: 0x02000050 RID: 80
	public class ScreenInfo : IDisposable
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0003E0B4 File Offset: 0x0003D0B4
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x0003E0CC File Offset: 0x0003D0CC
		public ListSelect CurrentListSelect
		{
			get
			{
				return this.currentListSelect;
			}
			set
			{
				this.currentListSelect = value;
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0003E0D8 File Offset: 0x0003D0D8
		public void AddArg(string name, string val)
		{
			if (this.args.ContainsKey(name))
			{
				this.args[name] = val;
			}
			else
			{
				this.args.Add(name, val);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0003E11C File Offset: 0x0003D11C
		public StringDictionary Args
		{
			get
			{
				return this.args;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0003E134 File Offset: 0x0003D134
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x0003E14C File Offset: 0x0003D14C
		public bool UseFrench
		{
			get
			{
				return this.useFrench;
			}
			set
			{
				this.useFrench = value;
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0003E158 File Offset: 0x0003D158
		public Control GetWindowsFormContainer()
		{
			Control result;
			if (this.parentControl != null)
			{
				result = this.parentControl.TopLevelControl;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0003E188 File Offset: 0x0003D188
		public ToolTip GetWindowFormContainerTooltip()
		{
			Control windowsFormContainer = this.GetWindowsFormContainer();
			if (windowsFormContainer != null)
			{
				if (windowsFormContainer is Form)
				{
					Form form = (Form)windowsFormContainer;
					foreach (object obj in form.Container.Components)
					{
						IComponent component = (IComponent)obj;
						if (component is ToolTip)
						{
							return (ToolTip)component;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0003E24C File Offset: 0x0003D24C
		public void SetToolTip(Control c, string toolTipText)
		{
			ToolTip windowFormContainerTooltip = this.GetWindowFormContainerTooltip();
			if (windowFormContainerTooltip != null)
			{
				windowFormContainerTooltip.SetToolTip(c, toolTipText);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0003E274 File Offset: 0x0003D274
		public Color OverridePanelBackgroundColour
		{
			get
			{
				return this.overridePanelBackgroundColour;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0003E28C File Offset: 0x0003D28C
		public Color OverridePanelForegroundColour
		{
			get
			{
				return this.overridePanelForegroundColour;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0003E2A4 File Offset: 0x0003D2A4
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x0003E2BC File Offset: 0x0003D2BC
		public int PerStudentScreenNum_Height
		{
			get
			{
				return this.perStudentScreenNum_Height;
			}
			set
			{
				this.perStudentScreenNum_Height = value;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0003E2C8 File Offset: 0x0003D2C8
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x0003E2E0 File Offset: 0x0003D2E0
		public int PerStudentScreenNum
		{
			get
			{
				return this.perStudentScreenNum;
			}
			set
			{
				this.perStudentScreenNum = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0003E2EC File Offset: 0x0003D2EC
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x0003E304 File Offset: 0x0003D304
		public bool OverridePanelBackgroundColourEnabled
		{
			get
			{
				return this.overridePanelColourEnabled;
			}
			set
			{
				this.overridePanelColourEnabled = value;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0003E310 File Offset: 0x0003D310
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x0003E328 File Offset: 0x0003D328
		public double WidthPercent
		{
			get
			{
				return this.widthPercent;
			}
			set
			{
				this.widthPercent = value;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0003E334 File Offset: 0x0003D334
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x0003E34C File Offset: 0x0003D34C
		public int BiggestCurrentRowHeight
		{
			get
			{
				return this.biggestCurrentRowHeight;
			}
			set
			{
				if (value == 0)
				{
					this.biggestCurrentRowHeight = 0;
				}
				else if (value > this.biggestCurrentRowHeight)
				{
					this.biggestCurrentRowHeight = value;
				}
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0003E388 File Offset: 0x0003D388
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x0003E3A7 File Offset: 0x0003D3A7
		public int columnWidth
		{
			get
			{
				return this.ColumnWidth - this.currentIndent;
			}
			set
			{
				this.ColumnWidth = value;
				this._labelWidth = (int)(0.3 * (double)this.ColumnWidth);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0003E3CC File Offset: 0x0003D3CC
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x0003E3E4 File Offset: 0x0003D3E4
		public int labelWidth
		{
			get
			{
				return this._labelWidth;
			}
			set
			{
				this._labelWidth = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0003E3F0 File Offset: 0x0003D3F0
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x0003E408 File Offset: 0x0003D408
		public string StudentNumberCaption
		{
			get
			{
				return this.studentNumberCaption;
			}
			set
			{
				this.studentNumberCaption = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0003E414 File Offset: 0x0003D414
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x0003E42C File Offset: 0x0003D42C
		public string StudentNumberAutoGenerateRule
		{
			get
			{
				return this.studentNumberAutoGenerateRule;
			}
			set
			{
				this.studentNumberAutoGenerateRule = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0003E438 File Offset: 0x0003D438
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x0003E450 File Offset: 0x0003D450
		public bool StudentNameHidden
		{
			get
			{
				return this.studentNameHidden;
			}
			set
			{
				this.studentNameHidden = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0003E45C File Offset: 0x0003D45C
		public int ControlIdToActivate
		{
			get
			{
				return this.controlIdToActivate;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0003E474 File Offset: 0x0003D474
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0003E48C File Offset: 0x0003D48C
		public bool NewNumLinesVerticalLimit
		{
			get
			{
				return this.newNumLinesVerticalLimit;
			}
			set
			{
				this.newNumLinesVerticalLimit = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0003E498 File Offset: 0x0003D498
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0003E4B0 File Offset: 0x0003D4B0
		public int NumLinesVerticalLimit
		{
			get
			{
				return this.numLinesVerticalLimit;
			}
			set
			{
				this.numLinesVerticalLimit = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0003E4BC File Offset: 0x0003D4BC
		public int CurrColInd
		{
			get
			{
				return this.currColInd;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0003E4D4 File Offset: 0x0003D4D4
		public int[] Groupids
		{
			get
			{
				return this.groupids;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0003E4EC File Offset: 0x0003D4EC
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0003E50B File Offset: 0x0003D50B
		public int currentX
		{
			get
			{
				return this.CurrentX + this.currentIndent;
			}
			set
			{
				this.CurrentX = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0003E518 File Offset: 0x0003D518
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x0003E530 File Offset: 0x0003D530
		public int currentY
		{
			get
			{
				return this.CurrentY;
			}
			set
			{
				this.CurrentY = value;
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0003E53C File Offset: 0x0003D53C
		public ScreenInfo(int _screenNum, Control _parentControl, bool _bottomLess, int _verticalControlPad, int _columnWidth, int _columnPad, Font _font, int _IconID, string Description, bool StudentNameNumEditable, bool overridePanelColourEnabled, Color overridePanelBackColour, Color overridePanelForeColour)
		{
			this.overridePanelColourEnabled = overridePanelColourEnabled;
			this.overridePanelForegroundColour = overridePanelForeColour;
			this.overridePanelBackgroundColour = overridePanelBackColour;
			this.description = Description;
			this.screenNum = _screenNum;
			this.bottomLess = _bottomLess;
			this.verticalControlPad = _verticalControlPad;
			this.font = _font;
			this.labelFont = new Font(this.font.FontFamily, this.font.Size - 2f, FontStyle.Bold);
			this.columnWidth = _columnWidth;
			this.columnPad = _columnPad;
			this.CurrentX = this.BORDERPADX;
			this.CurrentY = this.BORDERPADY;
			this.lastY = this.BORDERPADY;
			this.parentControl = _parentControl;
			this.parentControl.Font = this.font;
			this.parentControlHeight = this.parentControl.Height - 50;
			this.graphics = this.parentControl.CreateGraphics();
			this.iconID = _IconID;
			this.studentNameNumEditable = StudentNameNumEditable;
			this.groupids = new int[]
			{
				1
			};
			this.controlIdToActivate = 0;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0003E6F4 File Offset: 0x0003D6F4
		public void UpdateWidthHasChanged()
		{
			this.columnWidth = Convert.ToInt32(Convert.ToDouble(this.parentControl.Width) * this.widthPercent);
			this.currColInd = 0;
			this.lastY = this.BORDERPADY;
			this.CurrentX = this.BORDERPADX;
			this.CurrentY = this.BORDERPADY;
			this.biggestCurrentRowHeight = 0;
			this.tempOffsetX = 0;
			this.numColumns = 0;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0003E768 File Offset: 0x0003D768
		public static ScreenInfo GetScreenInfo(DataRow dr, Panel p_data, bool applyColWidthToCurrentPanel, bool overridePanelColourEnabled, Color overridePanelBackColour, Color overridePanelForeColour)
		{
			return ScreenInfo.GetScreenInfo(dr, p_data, applyColWidthToCurrentPanel, 0, overridePanelColourEnabled, overridePanelBackColour, overridePanelForeColour);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0003E788 File Offset: 0x0003D788
		public static ScreenInfo GetScreenInfo(DataRow dr, Panel p_data, bool applyColWidthToCurrentPanel, int panelWidthAvailable, bool overridePanelColourEnabled, Color overridePanelBackColour, Color overridePanelForeColour)
		{
			return ScreenInfo.GetScreenInfo(dr, p_data, applyColWidthToCurrentPanel, panelWidthAvailable, p_data.Height, overridePanelColourEnabled, overridePanelBackColour, overridePanelForeColour);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0003E7B0 File Offset: 0x0003D7B0
		public static ScreenInfo GetScreenInfo(DataRow dr, Panel p_data, bool applyColWidthToCurrentPanel, int panelWidthAvailable, int panelHeightAvailable, bool overridePanelColourEnabled, Color overridePanelBackColour, Color overridePanelForeColour)
		{
			p_data.Height = panelHeightAvailable;
			p_data.Width = panelWidthAvailable;
			bool flag = Convert.ToBoolean(dr[3]);
			int num = (int)dr[4];
			double num2 = Convert.ToDouble((int)dr[5]) / 100.0;
			int columnWidth = Convert.ToInt32(Convert.ToDouble((panelWidthAvailable > 0) ? panelWidthAvailable : p_data.Width) * num2);
			int num3 = (int)dr[6];
			int num4 = (int)dr[10];
			string text = dr.Table.Columns.Contains("groupids") ? ((string)dr["groupids"]) : "1";
			int num5 = dr.Table.Columns.Contains("controlidtoactivate") ? ((int)dr["controlidtoactivate"]) : 0;
			int num6 = 10;
			ScreenInfo screenInfo = new ScreenInfo((int)dr[0], p_data, flag, num, columnWidth, num3, new Font("Arial", (float)num6), num4, dr[1].ToString().Trim(), Convert.ToBoolean(dr[13]), text, num5, overridePanelColourEnabled, overridePanelBackColour, overridePanelForeColour);
			screenInfo.parentControlHeight = panelHeightAvailable;
			screenInfo.WidthPercent = num2;
			if (dr.Table.Columns.Contains("studentnumbercaption"))
			{
				screenInfo.StudentNumberCaption = dr["studentnumbercaption"].ToString();
				screenInfo.StudentNumberAutoGenerateRule = dr["studentnumberautogeneraterule"].ToString();
				screenInfo.StudentNameHidden = Convert.ToBoolean(dr["studentnamehidden"]);
			}
			else
			{
				screenInfo.StudentNumberCaption = "";
				screenInfo.StudentNumberAutoGenerateRule = "";
				screenInfo.StudentNameHidden = false;
			}
			return screenInfo;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0003E998 File Offset: 0x0003D998
		public ScreenInfo(int _screenNum, Control _parentControl, bool _bottomLess, int _verticalControlPad, int _columnWidth, int _columnPad, Font _font, int _IconID, string Description, bool StudentNameNumEditable, string groupids, int controlIdToActivate, bool overridePanelColourEnabled, Color overridePanelBackColour, Color overridePanelForeColour)
		{
			this.overridePanelColourEnabled = overridePanelColourEnabled;
			this.overridePanelForegroundColour = overridePanelForeColour;
			this.overridePanelBackgroundColour = overridePanelBackColour;
			this.description = Description;
			this.screenNum = _screenNum;
			this.bottomLess = _bottomLess;
			this.verticalControlPad = _verticalControlPad;
			this.font = _font;
			this.labelFont = new Font(this.font.FontFamily, this.font.Size - 2f, FontStyle.Bold);
			this.columnWidth = _columnWidth;
			this.columnPad = _columnPad;
			this.CurrentX = this.BORDERPADX;
			this.CurrentY = this.BORDERPADY;
			this.lastY = this.BORDERPADY;
			this.parentControl = _parentControl;
			this.parentControl.Font = this.font;
			this.parentControlHeight = this.parentControl.Height - 50;
			this.graphics = this.parentControl.CreateGraphics();
			this.iconID = _IconID;
			this.studentNameNumEditable = StudentNameNumEditable;
			ArrayList arrayList = new ArrayList();
			string text;
			if (groupids.Length > 1 && groupids[0] == '.')
			{
				text = groupids.Substring(1);
			}
			else
			{
				text = groupids;
				arrayList.Add(1);
			}
			string[] array = text.Split(new char[]
			{
				','
			});
			foreach (string text2 in array)
			{
				if (text2.Trim().Length > 0)
				{
					int num;
					try
					{
						num = int.Parse(text2);
					}
					catch
					{
						num = -1;
					}
					if (num > -1 && !arrayList.Contains(num))
					{
						arrayList.Add(num);
					}
				}
			}
			this.groupids = new int[arrayList.Count];
			for (int j = 0; j < arrayList.Count; j++)
			{
				this.groupids[j] = (int)arrayList[j];
			}
			this.controlIdToActivate = controlIdToActivate;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0003EC6C File Offset: 0x0003DC6C
		public int GetCurrentMaxY()
		{
			int num = 0;
			foreach (object obj in this.parentControl.Controls)
			{
				Control control = (Control)obj;
				int num2 = control.Location.Y + control.Height;
				if (num2 > num)
				{
					num = num2;
				}
			}
			return num + this.verticalControlPad;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0003ED10 File Offset: 0x0003DD10
		public void NotifyAddedControl()
		{
			this.numControlsInCurrentColumn++;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0003ED24 File Offset: 0x0003DD24
		public bool WillYFitInCurrentColumn(int y)
		{
			bool result;
			if (this.bottomLess)
			{
				result = true;
			}
			else if (!this.newNumLinesVerticalLimit)
			{
				result = (y < this.parentControlHeight);
			}
			else
			{
				result = (this.numControlsInCurrentColumn < this.numLinesVerticalLimit);
			}
			return result;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0003ED7C File Offset: 0x0003DD7C
		public void GotoNextColumn()
		{
			int num;
			int num2;
			int num3;
			this.GotoNextColumn(true, out num, out num2, out num3);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0003ED98 File Offset: 0x0003DD98
		public void GotoNextColumn(bool apply, out int currX, out int currY, out int curr_col_ind)
		{
			this.numControlsInCurrentColumn = 0;
			int num = this.CurrentY;
			int num2 = this.CurrentX;
			int num3 = this.currColInd;
			if (this.numColumns == 0 || this.bottomLess)
			{
				num = this.BORDERPADY;
				num2 += this.columnPad + this.ColumnWidth;
				num3++;
			}
			else if (!this.bottomLess)
			{
				if (num3 + 1 < this.numColumns)
				{
					num2 += this.columnPad + this.ColumnWidth;
					num = this.lastY;
					num3++;
				}
				else
				{
					num = this.BORDERPADY + this.GetCurrentMaxY();
					this.lastY = num;
					num2 = this.BORDERPADX;
					num3 = 0;
				}
			}
			currY = num;
			currX = num2;
			curr_col_ind = num3;
			if (apply)
			{
				this.CurrentY = num;
				this.CurrentX = num2;
				this.currColInd = num3;
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0003EE80 File Offset: 0x0003DE80
		public int GetEffectiveWidthForControl()
		{
			int num2;
			if (this.currColInd > 0)
			{
				int num = this.currColInd * (this.columnPad + this.columnWidth);
				num2 = this.currentX - num;
			}
			else
			{
				num2 = this.currentX;
			}
			int num3 = this.columnWidth - num2;
			return num3 - this.tempOffsetX;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0003EEE0 File Offset: 0x0003DEE0
		public void Dispose()
		{
			this.font = null;
			this.labelFont = null;
			this.graphics = null;
			this.parentControl = null;
		}

		// Token: 0x0400031D RID: 797
		public bool bottomLess;

		// Token: 0x0400031E RID: 798
		public Font font;

		// Token: 0x0400031F RID: 799
		public Font labelFont;

		// Token: 0x04000320 RID: 800
		public int verticalControlPad;

		// Token: 0x04000321 RID: 801
		public int ColumnWidth;

		// Token: 0x04000322 RID: 802
		public int columnPad;

		// Token: 0x04000323 RID: 803
		private double widthPercent;

		// Token: 0x04000324 RID: 804
		private StringDictionary args = new StringDictionary();

		// Token: 0x04000325 RID: 805
		private ListSelect currentListSelect = null;

		// Token: 0x04000326 RID: 806
		private bool useFrench = false;

		// Token: 0x04000327 RID: 807
		public int border = 0;

		// Token: 0x04000328 RID: 808
		private int biggestCurrentRowHeight = 0;

		// Token: 0x04000329 RID: 809
		private int perStudentScreenNum = 0;

		// Token: 0x0400032A RID: 810
		private int perStudentScreenNum_Height = 0;

		// Token: 0x0400032B RID: 811
		private Color overridePanelBackgroundColour = Color.Empty;

		// Token: 0x0400032C RID: 812
		private Color overridePanelForegroundColour = Color.Empty;

		// Token: 0x0400032D RID: 813
		private bool overridePanelColourEnabled = false;

		// Token: 0x0400032E RID: 814
		public DynamicControl radioGroupBehindMultipleCheckboxes = null;

		// Token: 0x0400032F RID: 815
		public Control parentControl;

		// Token: 0x04000330 RID: 816
		public Graphics graphics;

		// Token: 0x04000331 RID: 817
		private int _labelWidth;

		// Token: 0x04000332 RID: 818
		public int oneCharWidth = -1;

		// Token: 0x04000333 RID: 819
		public int BORDERPADY = 4;

		// Token: 0x04000334 RID: 820
		public int BORDERPADX = 2;

		// Token: 0x04000335 RID: 821
		public int parentControlHeight;

		// Token: 0x04000336 RID: 822
		public int screenNum;

		// Token: 0x04000337 RID: 823
		public int iconID;

		// Token: 0x04000338 RID: 824
		public string description;

		// Token: 0x04000339 RID: 825
		public bool studentNameNumEditable;

		// Token: 0x0400033A RID: 826
		public int currentIndent = 0;

		// Token: 0x0400033B RID: 827
		private int currColInd = 0;

		// Token: 0x0400033C RID: 828
		private int lastY;

		// Token: 0x0400033D RID: 829
		private int CurrentX;

		// Token: 0x0400033E RID: 830
		private int CurrentY;

		// Token: 0x0400033F RID: 831
		private int[] groupids;

		// Token: 0x04000340 RID: 832
		private int controlIdToActivate;

		// Token: 0x04000341 RID: 833
		private int numLinesVerticalLimit = 0;

		// Token: 0x04000342 RID: 834
		private bool newNumLinesVerticalLimit = false;

		// Token: 0x04000343 RID: 835
		private string studentNumberCaption;

		// Token: 0x04000344 RID: 836
		private string studentNumberAutoGenerateRule;

		// Token: 0x04000345 RID: 837
		private bool studentNameHidden;

		// Token: 0x04000346 RID: 838
		public int tempOffsetX = 0;

		// Token: 0x04000347 RID: 839
		public int numColumns = 0;

		// Token: 0x04000348 RID: 840
		private int numControlsInCurrentColumn = 0;
	}
}
