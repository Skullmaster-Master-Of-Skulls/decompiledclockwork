using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.VisualStyles;

namespace System.ComponentModel.Design
{
	// Token: 0x020001A3 RID: 419
	internal sealed class DesignerActionPanel : ContainerControl
	{
		// Token: 0x06000F61 RID: 3937 RVA: 0x00057BB8 File Offset: 0x00055DB8
		public DesignerActionPanel(IServiceProvider serviceProvider)
		{
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.Opaque, true);
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			base.SetStyle(ControlStyles.UserPaint, true);
			this._serviceProvider = serviceProvider;
			this._lines = new List<DesignerActionPanel.Line>();
			this._lineHeights = new List<int>();
			this._lineYPositions = new List<int>();
			this._toolTip = new ToolTip();
			IUIService iuiservice = (IUIService)this.ServiceProvider.GetService(typeof(IUIService));
			if (iuiservice != null)
			{
				this.Font = (Font)iuiservice.Styles["DialogFont"];
				if (iuiservice.Styles["VsColorPanelGradientDark"] is Color)
				{
					this._gradientDarkColor = (Color)iuiservice.Styles["VsColorPanelGradientDark"];
				}
				if (iuiservice.Styles["VsColorPanelGradientLight"] is Color)
				{
					this._gradientLightColor = (Color)iuiservice.Styles["VsColorPanelGradientLight"];
				}
				if (iuiservice.Styles["VsColorPanelHyperLink"] is Color)
				{
					this._linkColor = (Color)iuiservice.Styles["VsColorPanelHyperLink"];
				}
				if (iuiservice.Styles["VsColorPanelHyperLinkPressed"] is Color)
				{
					this._activeLinkColor = (Color)iuiservice.Styles["VsColorPanelHyperLinkPressed"];
				}
				if (iuiservice.Styles["VsColorPanelTitleBar"] is Color)
				{
					this._titleBarColor = (Color)iuiservice.Styles["VsColorPanelTitleBar"];
				}
				if (iuiservice.Styles["VsColorPanelTitleBarUnselected"] is Color)
				{
					this._titleBarUnselectedColor = (Color)iuiservice.Styles["VsColorPanelTitleBarUnselected"];
				}
				if (iuiservice.Styles["VsColorPanelTitleBarText"] is Color)
				{
					this._titleBarTextColor = (Color)iuiservice.Styles["VsColorPanelTitleBarText"];
				}
				if (iuiservice.Styles["VsColorPanelBorder"] is Color)
				{
					this._borderColor = (Color)iuiservice.Styles["VsColorPanelBorder"];
				}
				if (iuiservice.Styles["VsColorPanelSeparator"] is Color)
				{
					this._separatorColor = (Color)iuiservice.Styles["VsColorPanelSeparator"];
				}
				if (iuiservice.Styles["VsColorPanelText"] is Color)
				{
					this._labelForeColor = (Color)iuiservice.Styles["VsColorPanelText"];
				}
			}
			this.MinimumSize = new Size(150, 0);
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x00057EDE File Offset: 0x000560DE
		public Color ActiveLinkColor
		{
			get
			{
				return this._activeLinkColor;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x00057EE6 File Offset: 0x000560E6
		public Color BorderColor
		{
			get
			{
				return this._borderColor;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x00057EEE File Offset: 0x000560EE
		private bool DropDownActive
		{
			get
			{
				return this._dropDownActive;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x00057EF8 File Offset: 0x000560F8
		public CommandID[] FilteredCommandIDs
		{
			get
			{
				if (this._filteredCommandIDs == null)
				{
					this._filteredCommandIDs = new CommandID[]
					{
						StandardCommands.Copy,
						StandardCommands.Cut,
						StandardCommands.Delete,
						StandardCommands.F1Help,
						StandardCommands.Paste,
						StandardCommands.Redo,
						StandardCommands.SelectAll,
						StandardCommands.Undo,
						MenuCommands.KeyCancel,
						MenuCommands.KeyReverseCancel,
						MenuCommands.KeyDefaultAction,
						MenuCommands.KeyEnd,
						MenuCommands.KeyHome,
						MenuCommands.KeyMoveDown,
						MenuCommands.KeyMoveLeft,
						MenuCommands.KeyMoveRight,
						MenuCommands.KeyMoveUp,
						MenuCommands.KeyNudgeDown,
						MenuCommands.KeyNudgeHeightDecrease,
						MenuCommands.KeyNudgeHeightIncrease,
						MenuCommands.KeyNudgeLeft,
						MenuCommands.KeyNudgeRight,
						MenuCommands.KeyNudgeUp,
						MenuCommands.KeyNudgeWidthDecrease,
						MenuCommands.KeyNudgeWidthIncrease,
						MenuCommands.KeySizeHeightDecrease,
						MenuCommands.KeySizeHeightIncrease,
						MenuCommands.KeySizeWidthDecrease,
						MenuCommands.KeySizeWidthIncrease,
						MenuCommands.KeySelectNext,
						MenuCommands.KeySelectPrevious,
						MenuCommands.KeyShiftEnd,
						MenuCommands.KeyShiftHome
					};
				}
				return this._filteredCommandIDs;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x00058044 File Offset: 0x00056244
		private DesignerActionPanel.Line FocusedLine
		{
			get
			{
				Control activeControl = base.ActiveControl;
				if (activeControl != null)
				{
					return activeControl.Tag as DesignerActionPanel.Line;
				}
				return null;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x00058068 File Offset: 0x00056268
		public Color GradientDarkColor
		{
			get
			{
				return this._gradientDarkColor;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x00058070 File Offset: 0x00056270
		public Color GradientLightColor
		{
			get
			{
				return this._gradientLightColor;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x00058078 File Offset: 0x00056278
		// (set) Token: 0x06000F6A RID: 3946 RVA: 0x00058080 File Offset: 0x00056280
		public bool InMethodInvoke
		{
			get
			{
				return this._inMethodInvoke;
			}
			internal set
			{
				this._inMethodInvoke = value;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x00058089 File Offset: 0x00056289
		public Color LinkColor
		{
			get
			{
				return this._linkColor;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x00058091 File Offset: 0x00056291
		public Color SeparatorColor
		{
			get
			{
				return this._separatorColor;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x00058099 File Offset: 0x00056299
		private IServiceProvider ServiceProvider
		{
			get
			{
				return this._serviceProvider;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x000580A1 File Offset: 0x000562A1
		public Color TitleBarColor
		{
			get
			{
				return this._titleBarColor;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x000580A9 File Offset: 0x000562A9
		public Color TitleBarTextColor
		{
			get
			{
				return this._titleBarTextColor;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x000580B1 File Offset: 0x000562B1
		public Color TitleBarUnselectedColor
		{
			get
			{
				return this._titleBarUnselectedColor;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x000580B9 File Offset: 0x000562B9
		public Color LabelForeColor
		{
			get
			{
				return this._labelForeColor;
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000F72 RID: 3954 RVA: 0x000580C1 File Offset: 0x000562C1
		// (remove) Token: 0x06000F73 RID: 3955 RVA: 0x000580D4 File Offset: 0x000562D4
		private event EventHandler FormActivated
		{
			add
			{
				base.Events.AddHandler(DesignerActionPanel.EventFormActivated, value);
			}
			remove
			{
				base.Events.RemoveHandler(DesignerActionPanel.EventFormActivated, value);
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000F74 RID: 3956 RVA: 0x000580E7 File Offset: 0x000562E7
		// (remove) Token: 0x06000F75 RID: 3957 RVA: 0x000580FA File Offset: 0x000562FA
		private event EventHandler FormDeactivate
		{
			add
			{
				base.Events.AddHandler(DesignerActionPanel.EventFormDeactivate, value);
			}
			remove
			{
				base.Events.RemoveHandler(DesignerActionPanel.EventFormDeactivate, value);
			}
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00058110 File Offset: 0x00056310
		private void AddToCategories(DesignerActionPanel.LineInfo lineInfo, ListDictionary categories)
		{
			string text = lineInfo.Item.Category;
			if (text == null)
			{
				text = string.Empty;
			}
			ListDictionary listDictionary = (ListDictionary)categories[text];
			if (listDictionary == null)
			{
				listDictionary = new ListDictionary();
				categories.Add(text, listDictionary);
			}
			List<DesignerActionPanel.LineInfo> list = (List<DesignerActionPanel.LineInfo>)listDictionary[lineInfo.List];
			if (list == null)
			{
				list = new List<DesignerActionPanel.LineInfo>();
				listDictionary.Add(lineInfo.List, list);
			}
			list.Add(lineInfo);
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x00058180 File Offset: 0x00056380
		public static Point ComputePreferredDesktopLocation(Rectangle rectangleAnchor, Size sizePanel, out DockStyle edgeToDock)
		{
			Rectangle workingArea = Screen.FromPoint(rectangleAnchor.Location).WorkingArea;
			bool flag = true;
			bool flag2 = false;
			if (rectangleAnchor.Right + sizePanel.Width > workingArea.Right)
			{
				flag = false;
				if (rectangleAnchor.Left - sizePanel.Width < workingArea.Left)
				{
					flag2 = true;
				}
			}
			bool flag3 = flag;
			bool flag4 = false;
			if (flag3)
			{
				if (rectangleAnchor.Bottom + sizePanel.Height > workingArea.Bottom)
				{
					flag3 = false;
					if (rectangleAnchor.Top - sizePanel.Height < workingArea.Top)
					{
						flag4 = true;
					}
				}
			}
			else if (rectangleAnchor.Top - sizePanel.Height < workingArea.Top)
			{
				flag3 = true;
				if (rectangleAnchor.Bottom + sizePanel.Height > workingArea.Bottom)
				{
					flag4 = true;
				}
			}
			if (flag4)
			{
				flag2 = false;
			}
			int x = 0;
			int y = 0;
			edgeToDock = DockStyle.None;
			if (flag2 && flag3)
			{
				x = workingArea.Left;
				y = rectangleAnchor.Bottom;
				edgeToDock = DockStyle.Bottom;
			}
			else if (flag2 && !flag3)
			{
				x = workingArea.Left;
				y = rectangleAnchor.Top - sizePanel.Height;
				edgeToDock = DockStyle.Top;
			}
			else if (flag && flag4)
			{
				x = rectangleAnchor.Right;
				y = workingArea.Top;
				edgeToDock = DockStyle.Right;
			}
			else if (flag && flag3)
			{
				x = rectangleAnchor.Right;
				y = rectangleAnchor.Top;
				edgeToDock = DockStyle.Right;
			}
			else if (flag && !flag3)
			{
				x = rectangleAnchor.Right;
				y = rectangleAnchor.Bottom - sizePanel.Height;
				edgeToDock = DockStyle.Right;
			}
			else if (!flag && flag4)
			{
				x = rectangleAnchor.Left - sizePanel.Width;
				y = workingArea.Top;
				edgeToDock = DockStyle.Left;
			}
			else if (!flag && flag3)
			{
				x = rectangleAnchor.Left - sizePanel.Width;
				y = rectangleAnchor.Top;
				edgeToDock = DockStyle.Left;
			}
			else if (!flag && !flag3)
			{
				x = rectangleAnchor.Right - sizePanel.Width;
				y = rectangleAnchor.Top - sizePanel.Height;
				edgeToDock = DockStyle.Top;
			}
			return new Point(x, y);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0005838E File Offset: 0x0005658E
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._toolTip.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x000583A8 File Offset: 0x000565A8
		private Size DoLayout(Size proposedSize, bool measureOnly)
		{
			if (base.Disposing || base.IsDisposed)
			{
				return Size.Empty;
			}
			int num = 150;
			int num2 = 0;
			base.SuspendLayout();
			try
			{
				this._lineYPositions.Clear();
				this._lineHeights.Clear();
				for (int i = 0; i < this._lines.Count; i++)
				{
					DesignerActionPanel.Line line = this._lines[i];
					this._lineYPositions.Add(num2);
					Size size = line.LayoutControls(num2, proposedSize.Width, measureOnly);
					num = Math.Max(num, size.Width);
					this._lineHeights.Add(size.Height);
					num2 += size.Height;
				}
			}
			finally
			{
				base.ResumeLayout(!measureOnly);
			}
			return new Size(num, num2 + 2);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x00058480 File Offset: 0x00056680
		public override Size GetPreferredSize(Size proposedSize)
		{
			if (proposedSize.IsEmpty)
			{
				return proposedSize;
			}
			return this.DoLayout(proposedSize, true);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x00058495 File Offset: 0x00056695
		private static bool IsReadOnlyProperty(PropertyDescriptor pd)
		{
			return pd.IsReadOnly || pd.ComponentType.GetProperty(pd.Name).GetSetMethod() == null;
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x000584BD File Offset: 0x000566BD
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.UpdateEditXPos();
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x000584CC File Offset: 0x000566CC
		private void OnFormActivated(object sender, EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DesignerActionPanel.EventFormActivated];
			if (eventHandler != null)
			{
				eventHandler(sender, e);
			}
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x000584FC File Offset: 0x000566FC
		private void OnFormClosing(object sender, CancelEventArgs e)
		{
			if (!e.Cancel && base.TopLevelControl != null)
			{
				Form form = (Form)base.TopLevelControl;
				if (form != null)
				{
					form.Activated -= this.OnFormActivated;
					form.Deactivate -= this.OnFormDeactivate;
					form.Closing -= this.OnFormClosing;
				}
			}
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00058560 File Offset: 0x00056760
		private void OnFormDeactivate(object sender, EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DesignerActionPanel.EventFormDeactivate];
			if (eventHandler != null)
			{
				eventHandler(sender, e);
			}
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x00058590 File Offset: 0x00056790
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			Form form = base.TopLevelControl as Form;
			if (form != null)
			{
				form.Activated += this.OnFormActivated;
				form.Deactivate += this.OnFormDeactivate;
				form.Closing += this.OnFormClosing;
			}
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x000585E9 File Offset: 0x000567E9
		protected override void OnLayout(LayoutEventArgs levent)
		{
			if (this._updatingTasks)
			{
				return;
			}
			this.DoLayout(base.Size, false);
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x00058604 File Offset: 0x00056804
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (this._updatingTasks)
			{
				return;
			}
			Rectangle bounds = base.Bounds;
			if (this.RightToLeft == RightToLeft.Yes)
			{
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, this.GradientDarkColor, this.GradientLightColor, LinearGradientMode.Horizontal))
				{
					e.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
					goto IL_8C;
				}
			}
			using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(bounds, this.GradientLightColor, this.GradientDarkColor, LinearGradientMode.Horizontal))
			{
				e.Graphics.FillRectangle(linearGradientBrush2, base.ClientRectangle);
			}
			IL_8C:
			using (Pen pen = new Pen(this.BorderColor))
			{
				e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, base.Width - 1, base.Height - 1));
			}
			Rectangle clipRectangle = e.ClipRectangle;
			int num = 0;
			while (num < this._lineYPositions.Count - 1 && this._lineYPositions[num + 1] <= clipRectangle.Top)
			{
				num++;
			}
			Graphics graphics = e.Graphics;
			for (int i = num; i < this._lineYPositions.Count; i++)
			{
				DesignerActionPanel.Line line = this._lines[i];
				int num2 = this._lineYPositions[i];
				int num3 = this._lineHeights[i];
				int width = base.Width;
				graphics.SetClip(new Rectangle(0, num2, width, num3));
				graphics.TranslateTransform(0f, (float)num2);
				line.PaintLine(graphics, width, num3);
				graphics.ResetTransform();
				if (num2 + num3 > clipRectangle.Bottom)
				{
					break;
				}
			}
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x000587D4 File Offset: 0x000569D4
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			base.PerformLayout();
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x000587E4 File Offset: 0x000569E4
		protected override bool ProcessDialogKey(Keys keyData)
		{
			DesignerActionPanel.Line focusedLine = this.FocusedLine;
			return (focusedLine != null && focusedLine.ProcessDialogKey(keyData)) || base.ProcessDialogKey(keyData);
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0005880D File Offset: 0x00056A0D
		protected override bool ProcessTabKey(bool forward)
		{
			return base.SelectNextControl(base.ActiveControl, forward, true, true, true);
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00058820 File Offset: 0x00056A20
		private void ProcessLists(DesignerActionListCollection lists, ListDictionary categories)
		{
			if (lists == null)
			{
				return;
			}
			foreach (object obj in lists)
			{
				DesignerActionList designerActionList = (DesignerActionList)obj;
				if (designerActionList != null)
				{
					IEnumerable sortedActionItems = designerActionList.GetSortedActionItems();
					if (sortedActionItems != null)
					{
						foreach (object obj2 in sortedActionItems)
						{
							DesignerActionItem designerActionItem = (DesignerActionItem)obj2;
							if (designerActionItem != null)
							{
								DesignerActionPanel.LineInfo lineInfo = this.ProcessTaskItem(designerActionList, designerActionItem);
								if (lineInfo != null)
								{
									this.AddToCategories(lineInfo, categories);
									IComponent component = null;
									DesignerActionPropertyItem designerActionPropertyItem = designerActionItem as DesignerActionPropertyItem;
									if (designerActionPropertyItem != null)
									{
										component = designerActionPropertyItem.RelatedComponent;
									}
									else
									{
										DesignerActionMethodItem designerActionMethodItem = designerActionItem as DesignerActionMethodItem;
										if (designerActionMethodItem != null)
										{
											component = designerActionMethodItem.RelatedComponent;
										}
									}
									if (component != null)
									{
										IEnumerable<DesignerActionPanel.LineInfo> enumerable = this.ProcessRelatedTaskItems(component);
										if (enumerable != null)
										{
											foreach (DesignerActionPanel.LineInfo lineInfo2 in enumerable)
											{
												this.AddToCategories(lineInfo2, categories);
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0005899C File Offset: 0x00056B9C
		private IEnumerable<DesignerActionPanel.LineInfo> ProcessRelatedTaskItems(IComponent relatedComponent)
		{
			DesignerActionListCollection designerActionListCollection = null;
			DesignerActionService designerActionService = (DesignerActionService)this.ServiceProvider.GetService(typeof(DesignerActionService));
			if (designerActionService != null)
			{
				designerActionListCollection = designerActionService.GetComponentActions(relatedComponent);
			}
			else
			{
				IServiceProvider serviceProvider = relatedComponent.Site;
				if (serviceProvider == null)
				{
					serviceProvider = this.ServiceProvider;
				}
				IDesignerHost designerHost = (IDesignerHost)serviceProvider.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					ComponentDesigner componentDesigner = designerHost.GetDesigner(relatedComponent) as ComponentDesigner;
					if (componentDesigner != null)
					{
						designerActionListCollection = componentDesigner.ActionLists;
					}
				}
			}
			List<DesignerActionPanel.LineInfo> list = new List<DesignerActionPanel.LineInfo>();
			if (designerActionListCollection != null)
			{
				foreach (object obj in designerActionListCollection)
				{
					DesignerActionList designerActionList = (DesignerActionList)obj;
					if (designerActionList != null)
					{
						IEnumerable sortedActionItems = designerActionList.GetSortedActionItems();
						if (sortedActionItems != null)
						{
							foreach (object obj2 in sortedActionItems)
							{
								DesignerActionItem designerActionItem = (DesignerActionItem)obj2;
								if (designerActionItem != null && designerActionItem.AllowAssociate)
								{
									DesignerActionPanel.LineInfo lineInfo = this.ProcessTaskItem(designerActionList, designerActionItem);
									if (lineInfo != null)
									{
										list.Add(lineInfo);
									}
								}
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x00058AF0 File Offset: 0x00056CF0
		private DesignerActionPanel.LineInfo ProcessTaskItem(DesignerActionList list, DesignerActionItem item)
		{
			DesignerActionPanel.Line line;
			if (item is DesignerActionMethodItem)
			{
				line = new DesignerActionPanel.MethodLine(this._serviceProvider, this);
			}
			else if (item is DesignerActionPropertyItem)
			{
				DesignerActionPropertyItem designerActionPropertyItem = (DesignerActionPropertyItem)item;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(list)[designerActionPropertyItem.MemberName];
				if (propertyDescriptor == null)
				{
					throw new InvalidOperationException(SR.GetString("DesignerActionPanel_CouldNotFindProperty", new object[]
					{
						designerActionPropertyItem.MemberName,
						list.GetType().FullName
					}));
				}
				DesignerActionPanel.TypeDescriptorContext context = new DesignerActionPanel.TypeDescriptorContext(this._serviceProvider, propertyDescriptor, list);
				UITypeEditor uitypeEditor = (UITypeEditor)propertyDescriptor.GetEditor(typeof(UITypeEditor));
				bool standardValuesSupported = propertyDescriptor.Converter.GetStandardValuesSupported(context);
				if (uitypeEditor == null)
				{
					if (propertyDescriptor.PropertyType == typeof(bool))
					{
						if (DesignerActionPanel.IsReadOnlyProperty(propertyDescriptor))
						{
							line = new DesignerActionPanel.TextBoxPropertyLine(this._serviceProvider, this);
						}
						else
						{
							line = new DesignerActionPanel.CheckBoxPropertyLine(this._serviceProvider, this);
						}
					}
					else if (standardValuesSupported)
					{
						line = new DesignerActionPanel.EditorPropertyLine(this._serviceProvider, this);
					}
					else
					{
						line = new DesignerActionPanel.TextBoxPropertyLine(this._serviceProvider, this);
					}
				}
				else
				{
					line = new DesignerActionPanel.EditorPropertyLine(this._serviceProvider, this);
				}
			}
			else
			{
				if (!(item is DesignerActionTextItem))
				{
					return null;
				}
				if (item is DesignerActionHeaderItem)
				{
					line = new DesignerActionPanel.HeaderLine(this._serviceProvider, this);
				}
				else
				{
					line = new DesignerActionPanel.TextLine(this._serviceProvider, this);
				}
			}
			return new DesignerActionPanel.LineInfo(list, item, line);
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x00058C4A File Offset: 0x00056E4A
		private void SetDropDownActive(bool active)
		{
			this._dropDownActive = active;
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x00058C54 File Offset: 0x00056E54
		private void ShowError(string errorMessage)
		{
			IUIService iuiservice = (IUIService)this.ServiceProvider.GetService(typeof(IUIService));
			if (iuiservice != null)
			{
				iuiservice.ShowError(errorMessage);
				return;
			}
			MessageBoxOptions options = (MessageBoxOptions)0;
			if (SR.GetString("RTL") != "RTL_False")
			{
				options = (MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
			}
			MessageBox.Show(this, errorMessage, SR.GetString("UIServiceHelper_ErrorCaption"), MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, options);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x00058CBC File Offset: 0x00056EBC
		private static string StripAmpersands(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(s.Length);
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == '&')
				{
					i++;
					if (i == s.Length)
					{
						stringBuilder.Append('&');
						break;
					}
				}
				stringBuilder.Append(s[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00058D2C File Offset: 0x00056F2C
		private void UpdateEditXPos()
		{
			int num = 0;
			for (int i = 0; i < this._lines.Count; i++)
			{
				DesignerActionPanel.TextBoxPropertyLine textBoxPropertyLine = this._lines[i] as DesignerActionPanel.TextBoxPropertyLine;
				if (textBoxPropertyLine != null)
				{
					num = Math.Max(num, textBoxPropertyLine.GetEditRegionXPos());
				}
			}
			for (int j = 0; j < this._lines.Count; j++)
			{
				DesignerActionPanel.TextBoxPropertyLine textBoxPropertyLine2 = this._lines[j] as DesignerActionPanel.TextBoxPropertyLine;
				if (textBoxPropertyLine2 != null)
				{
					textBoxPropertyLine2.SetEditRegionXPos(num);
				}
			}
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x00058DA8 File Offset: 0x00056FA8
		public void UpdateTasks(DesignerActionListCollection actionLists, DesignerActionListCollection serviceActionLists, string title, string subtitle)
		{
			this._updatingTasks = true;
			base.SuspendLayout();
			try
			{
				base.AccessibleName = title;
				base.AccessibleDescription = subtitle;
				string text = string.Empty;
				DesignerActionPanel.Line focusedLine = this.FocusedLine;
				if (focusedLine != null)
				{
					text = focusedLine.FocusId;
				}
				ListDictionary listDictionary = new ListDictionary();
				this.ProcessLists(actionLists, listDictionary);
				this.ProcessLists(serviceActionLists, listDictionary);
				List<DesignerActionPanel.LineInfo> list = new List<DesignerActionPanel.LineInfo>();
				list.Add(new DesignerActionPanel.LineInfo(null, new DesignerActionPanel.DesignerActionPanelHeaderItem(title, subtitle), new DesignerActionPanel.PanelHeaderLine(this._serviceProvider, this)));
				int num = 0;
				foreach (object obj in listDictionary.Values)
				{
					ListDictionary listDictionary2 = (ListDictionary)obj;
					int num2 = 0;
					foreach (object obj2 in listDictionary2.Values)
					{
						List<DesignerActionPanel.LineInfo> list2 = (List<DesignerActionPanel.LineInfo>)obj2;
						for (int i = 0; i < list2.Count; i++)
						{
							list.Add(list2[i]);
						}
						num2++;
						if (num2 < listDictionary2.Count)
						{
							list.Add(new DesignerActionPanel.LineInfo(null, null, new DesignerActionPanel.SeparatorLine(this._serviceProvider, this, true)));
						}
					}
					num++;
					if (num < listDictionary.Count)
					{
						list.Add(new DesignerActionPanel.LineInfo(null, null, new DesignerActionPanel.SeparatorLine(this._serviceProvider, this)));
					}
				}
				int num3 = 0;
				for (int j = 0; j < list.Count; j++)
				{
					DesignerActionPanel.LineInfo lineInfo = list[j];
					DesignerActionPanel.Line line = lineInfo.Line;
					bool flag = false;
					if (j < this._lines.Count)
					{
						DesignerActionPanel.Line line2 = this._lines[j];
						if (line2.GetType() == line.GetType())
						{
							line2.UpdateActionItem(lineInfo.List, lineInfo.Item, this._toolTip, ref num3);
							flag = true;
						}
						else
						{
							line2.RemoveControls(base.Controls);
							this._lines.RemoveAt(j);
						}
					}
					if (!flag)
					{
						List<Control> controls = line.GetControls();
						Control[] array = new Control[controls.Count];
						controls.CopyTo(array);
						base.Controls.AddRange(array);
						line.UpdateActionItem(lineInfo.List, lineInfo.Item, this._toolTip, ref num3);
						this._lines.Insert(j, line);
					}
				}
				for (int k = this._lines.Count - 1; k >= list.Count; k--)
				{
					DesignerActionPanel.Line line3 = this._lines[k];
					line3.RemoveControls(base.Controls);
					this._lines.RemoveAt(k);
				}
				if (!string.IsNullOrEmpty(text))
				{
					foreach (DesignerActionPanel.Line line4 in this._lines)
					{
						if (string.Equals(line4.FocusId, text, StringComparison.Ordinal))
						{
							line4.Focus();
						}
					}
				}
			}
			finally
			{
				this.UpdateEditXPos();
				this._updatingTasks = false;
				base.ResumeLayout(true);
			}
			base.Invalidate();
		}

		// Token: 0x040008F6 RID: 2294
		public const string ExternDllGdi32 = "gdi32.dll";

		// Token: 0x040008F7 RID: 2295
		public const string ExternDllUser32 = "user32.dll";

		// Token: 0x040008F8 RID: 2296
		private static readonly object EventFormActivated = new object();

		// Token: 0x040008F9 RID: 2297
		private static readonly object EventFormDeactivate = new object();

		// Token: 0x040008FA RID: 2298
		private const int EditInputWidth = 150;

		// Token: 0x040008FB RID: 2299
		private const int ListBoxMaximumHeight = 200;

		// Token: 0x040008FC RID: 2300
		private const int MinimumWidth = 150;

		// Token: 0x040008FD RID: 2301
		private const int BottomPadding = 2;

		// Token: 0x040008FE RID: 2302
		private const int TopPadding = 2;

		// Token: 0x040008FF RID: 2303
		private const int LineLeftMargin = 5;

		// Token: 0x04000900 RID: 2304
		private const int LineRightMargin = 4;

		// Token: 0x04000901 RID: 2305
		private const int LineVerticalPadding = 7;

		// Token: 0x04000902 RID: 2306
		private const int TextBoxTopPadding = 4;

		// Token: 0x04000903 RID: 2307
		private const int SeparatorHorizontalPadding = 3;

		// Token: 0x04000904 RID: 2308
		private const int TextBoxLineCenterMargin = 5;

		// Token: 0x04000905 RID: 2309
		private const int TextBoxLineInnerPadding = 1;

		// Token: 0x04000906 RID: 2310
		private const int EditorLineSwatchPadding = 1;

		// Token: 0x04000907 RID: 2311
		private const int EditorLineButtonPadding = 1;

		// Token: 0x04000908 RID: 2312
		private const int PanelHeaderVerticalPadding = 3;

		// Token: 0x04000909 RID: 2313
		private const int PanelHeaderHorizontalPadding = 5;

		// Token: 0x0400090A RID: 2314
		private const int TextBoxHeightFixup = 2;

		// Token: 0x0400090B RID: 2315
		private CommandID[] _filteredCommandIDs;

		// Token: 0x0400090C RID: 2316
		private ToolTip _toolTip;

		// Token: 0x0400090D RID: 2317
		private List<DesignerActionPanel.Line> _lines;

		// Token: 0x0400090E RID: 2318
		private List<int> _lineYPositions;

		// Token: 0x0400090F RID: 2319
		private List<int> _lineHeights;

		// Token: 0x04000910 RID: 2320
		private Color _gradientLightColor = SystemColors.Control;

		// Token: 0x04000911 RID: 2321
		private Color _gradientDarkColor = SystemColors.Control;

		// Token: 0x04000912 RID: 2322
		private Color _titleBarColor = SystemColors.ActiveCaption;

		// Token: 0x04000913 RID: 2323
		private Color _titleBarUnselectedColor = SystemColors.InactiveCaption;

		// Token: 0x04000914 RID: 2324
		private Color _titleBarTextColor = SystemColors.ActiveCaptionText;

		// Token: 0x04000915 RID: 2325
		private Color _separatorColor = SystemColors.ControlDark;

		// Token: 0x04000916 RID: 2326
		private Color _borderColor = SystemColors.ActiveBorder;

		// Token: 0x04000917 RID: 2327
		private Color _linkColor = SystemColors.HotTrack;

		// Token: 0x04000918 RID: 2328
		private Color _activeLinkColor = SystemColors.HotTrack;

		// Token: 0x04000919 RID: 2329
		private Color _labelForeColor = SystemColors.ControlText;

		// Token: 0x0400091A RID: 2330
		private IServiceProvider _serviceProvider;

		// Token: 0x0400091B RID: 2331
		private bool _inMethodInvoke;

		// Token: 0x0400091C RID: 2332
		private bool _updatingTasks;

		// Token: 0x0400091D RID: 2333
		private bool _dropDownActive;

		// Token: 0x02000484 RID: 1156
		private class LineInfo
		{
			// Token: 0x06002AA4 RID: 10916 RVA: 0x001004CE File Offset: 0x000FE6CE
			public LineInfo(DesignerActionList list, DesignerActionItem item, DesignerActionPanel.Line line)
			{
				this.Line = line;
				this.Item = item;
				this.List = list;
			}

			// Token: 0x04001DDA RID: 7642
			public DesignerActionPanel.Line Line;

			// Token: 0x04001DDB RID: 7643
			public DesignerActionItem Item;

			// Token: 0x04001DDC RID: 7644
			public DesignerActionList List;
		}

		// Token: 0x02000485 RID: 1157
		internal sealed class TypeDescriptorContext : ITypeDescriptorContext, IServiceProvider
		{
			// Token: 0x06002AA5 RID: 10917 RVA: 0x001004EB File Offset: 0x000FE6EB
			public TypeDescriptorContext(IServiceProvider serviceProvider, PropertyDescriptor propDesc, object instance)
			{
				this._serviceProvider = serviceProvider;
				this._propDesc = propDesc;
				this._instance = instance;
			}

			// Token: 0x17000905 RID: 2309
			// (get) Token: 0x06002AA6 RID: 10918 RVA: 0x00100508 File Offset: 0x000FE708
			private IComponentChangeService ComponentChangeService
			{
				get
				{
					return (IComponentChangeService)this._serviceProvider.GetService(typeof(IComponentChangeService));
				}
			}

			// Token: 0x17000906 RID: 2310
			// (get) Token: 0x06002AA7 RID: 10919 RVA: 0x00100524 File Offset: 0x000FE724
			public IContainer Container
			{
				get
				{
					return (IContainer)this._serviceProvider.GetService(typeof(IContainer));
				}
			}

			// Token: 0x17000907 RID: 2311
			// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x00100540 File Offset: 0x000FE740
			public object Instance
			{
				get
				{
					return this._instance;
				}
			}

			// Token: 0x17000908 RID: 2312
			// (get) Token: 0x06002AA9 RID: 10921 RVA: 0x00100548 File Offset: 0x000FE748
			public PropertyDescriptor PropertyDescriptor
			{
				get
				{
					return this._propDesc;
				}
			}

			// Token: 0x06002AAA RID: 10922 RVA: 0x00100550 File Offset: 0x000FE750
			public object GetService(Type serviceType)
			{
				return this._serviceProvider.GetService(serviceType);
			}

			// Token: 0x06002AAB RID: 10923 RVA: 0x00100560 File Offset: 0x000FE760
			public bool OnComponentChanging()
			{
				if (this.ComponentChangeService != null)
				{
					try
					{
						this.ComponentChangeService.OnComponentChanging(this._instance, this._propDesc);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return false;
						}
						throw ex;
					}
					return true;
				}
				return true;
			}

			// Token: 0x06002AAC RID: 10924 RVA: 0x001005B0 File Offset: 0x000FE7B0
			public void OnComponentChanged()
			{
				if (this.ComponentChangeService != null)
				{
					this.ComponentChangeService.OnComponentChanged(this._instance, this._propDesc, null, null);
				}
			}

			// Token: 0x04001DDD RID: 7645
			private IServiceProvider _serviceProvider;

			// Token: 0x04001DDE RID: 7646
			private PropertyDescriptor _propDesc;

			// Token: 0x04001DDF RID: 7647
			private object _instance;
		}

		// Token: 0x02000486 RID: 1158
		private abstract class Line
		{
			// Token: 0x06002AAD RID: 10925 RVA: 0x001005D3 File Offset: 0x000FE7D3
			public Line(IServiceProvider serviceProvider, DesignerActionPanel actionPanel)
			{
				if (actionPanel == null)
				{
					throw new ArgumentNullException("actionPanel");
				}
				this._serviceProvider = serviceProvider;
				this._actionPanel = actionPanel;
			}

			// Token: 0x17000909 RID: 2313
			// (get) Token: 0x06002AAE RID: 10926 RVA: 0x001005F7 File Offset: 0x000FE7F7
			protected DesignerActionPanel ActionPanel
			{
				get
				{
					return this._actionPanel;
				}
			}

			// Token: 0x1700090A RID: 2314
			// (get) Token: 0x06002AAF RID: 10927
			public abstract string FocusId { get; }

			// Token: 0x1700090B RID: 2315
			// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x001005FF File Offset: 0x000FE7FF
			protected IServiceProvider ServiceProvider
			{
				get
				{
					return this._serviceProvider;
				}
			}

			// Token: 0x06002AB1 RID: 10929
			protected abstract void AddControls(List<Control> controls);

			// Token: 0x06002AB2 RID: 10930 RVA: 0x00100608 File Offset: 0x000FE808
			internal List<Control> GetControls()
			{
				this._addedControls = new List<Control>();
				this.AddControls(this._addedControls);
				foreach (Control control in this._addedControls)
				{
					control.Tag = this;
				}
				return this._addedControls;
			}

			// Token: 0x06002AB3 RID: 10931
			public abstract void Focus();

			// Token: 0x06002AB4 RID: 10932
			public abstract Size LayoutControls(int top, int width, bool measureOnly);

			// Token: 0x06002AB5 RID: 10933 RVA: 0x00003937 File Offset: 0x00001B37
			public virtual void PaintLine(Graphics g, int lineWidth, int lineHeight)
			{
			}

			// Token: 0x06002AB6 RID: 10934 RVA: 0x0000445B File Offset: 0x0000265B
			protected internal virtual bool ProcessDialogKey(Keys keyData)
			{
				return false;
			}

			// Token: 0x06002AB7 RID: 10935 RVA: 0x00100678 File Offset: 0x000FE878
			internal void RemoveControls(Control.ControlCollection controls)
			{
				for (int i = 0; i < this._addedControls.Count; i++)
				{
					Control control = this._addedControls[i];
					control.Tag = null;
					controls.Remove(control);
				}
			}

			// Token: 0x06002AB8 RID: 10936
			internal abstract void UpdateActionItem(DesignerActionList actionList, DesignerActionItem actionItem, ToolTip toolTip, ref int currentTabIndex);

			// Token: 0x04001DE0 RID: 7648
			private DesignerActionPanel _actionPanel;

			// Token: 0x04001DE1 RID: 7649
			private List<Control> _addedControls;

			// Token: 0x04001DE2 RID: 7650
			private IServiceProvider _serviceProvider;
		}

		// Token: 0x02000487 RID: 1159
		private sealed class DesignerActionPanelHeaderItem : DesignerActionItem
		{
			// Token: 0x06002AB9 RID: 10937 RVA: 0x001006B6 File Offset: 0x000FE8B6
			public DesignerActionPanelHeaderItem(string title, string subtitle) : base(title, null, null)
			{
				this._subtitle = subtitle;
			}

			// Token: 0x1700090C RID: 2316
			// (get) Token: 0x06002ABA RID: 10938 RVA: 0x001006C8 File Offset: 0x000FE8C8
			public string Subtitle
			{
				get
				{
					return this._subtitle;
				}
			}

			// Token: 0x04001DE3 RID: 7651
			private string _subtitle;
		}

		// Token: 0x02000488 RID: 1160
		private sealed class PanelHeaderLine : DesignerActionPanel.Line
		{
			// Token: 0x06002ABB RID: 10939 RVA: 0x001006D0 File Offset: 0x000FE8D0
			public PanelHeaderLine(IServiceProvider serviceProvider, DesignerActionPanel actionPanel) : base(serviceProvider, actionPanel)
			{
				actionPanel.FontChanged += this.OnParentControlFontChanged;
			}

			// Token: 0x1700090D RID: 2317
			// (get) Token: 0x06002ABC RID: 10940 RVA: 0x00003930 File Offset: 0x00001B30
			public sealed override string FocusId
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x06002ABD RID: 10941 RVA: 0x001006EC File Offset: 0x000FE8EC
			protected override void AddControls(List<Control> controls)
			{
				this._titleLabel = new Label();
				this._titleLabel.BackColor = Color.Transparent;
				this._titleLabel.ForeColor = base.ActionPanel.TitleBarTextColor;
				this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
				this._titleLabel.UseMnemonic = false;
				this._subtitleLabel = new Label();
				this._subtitleLabel.BackColor = Color.Transparent;
				this._subtitleLabel.ForeColor = base.ActionPanel.TitleBarTextColor;
				this._subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
				this._subtitleLabel.UseMnemonic = false;
				controls.Add(this._titleLabel);
				controls.Add(this._subtitleLabel);
				base.ActionPanel.FormActivated += this.OnFormActivated;
				base.ActionPanel.FormDeactivate += this.OnFormDeactivate;
			}

			// Token: 0x06002ABE RID: 10942 RVA: 0x00003937 File Offset: 0x00001B37
			public sealed override void Focus()
			{
			}

			// Token: 0x06002ABF RID: 10943 RVA: 0x001007D4 File Offset: 0x000FE9D4
			public override Size LayoutControls(int top, int width, bool measureOnly)
			{
				Size preferredSize = this._titleLabel.GetPreferredSize(new Size(int.MaxValue, int.MaxValue));
				Size size = Size.Empty;
				if (!string.IsNullOrEmpty(this._panelHeaderItem.Subtitle))
				{
					size = this._subtitleLabel.GetPreferredSize(new Size(int.MaxValue, int.MaxValue));
				}
				if (!measureOnly)
				{
					this._titleLabel.Location = new Point(5, top + 3);
					this._titleLabel.Size = preferredSize;
					this._subtitleLabel.Location = new Point(5, top + 6 + preferredSize.Height);
					this._subtitleLabel.Size = size;
				}
				int num = Math.Max(preferredSize.Width, size.Width) + 10;
				int num2 = size.IsEmpty ? (preferredSize.Height + 6) : (preferredSize.Height + size.Height + 9);
				return new Size(num + 2, num2 + 1);
			}

			// Token: 0x06002AC0 RID: 10944 RVA: 0x001008C3 File Offset: 0x000FEAC3
			private void OnFormActivated(object sender, EventArgs e)
			{
				this._formActive = true;
				base.ActionPanel.Invalidate();
			}

			// Token: 0x06002AC1 RID: 10945 RVA: 0x001008D7 File Offset: 0x000FEAD7
			private void OnFormDeactivate(object sender, EventArgs e)
			{
				this._formActive = false;
				base.ActionPanel.Invalidate();
			}

			// Token: 0x06002AC2 RID: 10946 RVA: 0x001008EC File Offset: 0x000FEAEC
			private void OnParentControlFontChanged(object sender, EventArgs e)
			{
				if (this._titleLabel != null && this._subtitleLabel != null)
				{
					this._titleLabel.Font = new Font(base.ActionPanel.Font, FontStyle.Bold);
					this._subtitleLabel.Font = base.ActionPanel.Font;
				}
			}

			// Token: 0x06002AC3 RID: 10947 RVA: 0x0010093C File Offset: 0x000FEB3C
			public override void PaintLine(Graphics g, int lineWidth, int lineHeight)
			{
				Color color = (this._formActive || base.ActionPanel.DropDownActive) ? base.ActionPanel.TitleBarColor : base.ActionPanel.TitleBarUnselectedColor;
				using (SolidBrush solidBrush = new SolidBrush(color))
				{
					g.FillRectangle(solidBrush, 1, 1, lineWidth - 2, lineHeight - 1);
				}
				using (Pen pen = new Pen(base.ActionPanel.BorderColor))
				{
					g.DrawLine(pen, 0, lineHeight - 1, lineWidth, lineHeight - 1);
				}
			}

			// Token: 0x06002AC4 RID: 10948 RVA: 0x001009E4 File Offset: 0x000FEBE4
			internal override void UpdateActionItem(DesignerActionList actionList, DesignerActionItem actionItem, ToolTip toolTip, ref int currentTabIndex)
			{
				this._actionList = actionList;
				this._panelHeaderItem = (DesignerActionPanel.DesignerActionPanelHeaderItem)actionItem;
				this._titleLabel.Text = this._panelHeaderItem.DisplayName;
				Control titleLabel = this._titleLabel;
				int num = currentTabIndex;
				currentTabIndex = num + 1;
				titleLabel.TabIndex = num;
				this._subtitleLabel.Text = this._panelHeaderItem.Subtitle;
				Control subtitleLabel = this._subtitleLabel;
				num = currentTabIndex;
				currentTabIndex = num + 1;
				subtitleLabel.TabIndex = num;
				this._subtitleLabel.Visible = (this._subtitleLabel.Text.Length != 0);
				this.OnParentControlFontChanged(null, EventArgs.Empty);
			}

			// Token: 0x04001DE4 RID: 7652
			private DesignerActionList _actionList;

			// Token: 0x04001DE5 RID: 7653
			private DesignerActionPanel.DesignerActionPanelHeaderItem _panelHeaderItem;

			// Token: 0x04001DE6 RID: 7654
			private Label _titleLabel;

			// Token: 0x04001DE7 RID: 7655
			private Label _subtitleLabel;

			// Token: 0x04001DE8 RID: 7656
			private bool _formActive;
		}

		// Token: 0x02000489 RID: 1161
		private sealed class MethodLine : DesignerActionPanel.Line
		{
			// Token: 0x06002AC5 RID: 10949 RVA: 0x00100A86 File Offset: 0x000FEC86
			public MethodLine(IServiceProvider serviceProvider, DesignerActionPanel actionPanel) : base(serviceProvider, actionPanel)
			{
			}

			// Token: 0x1700090E RID: 2318
			// (get) Token: 0x06002AC6 RID: 10950 RVA: 0x00100A90 File Offset: 0x000FEC90
			public sealed override string FocusId
			{
				get
				{
					return "METHOD:" + this._actionList.GetType().FullName + "." + this._methodItem.MemberName;
				}
			}

			// Token: 0x06002AC7 RID: 10951 RVA: 0x00100ABC File Offset: 0x000FECBC
			protected override void AddControls(List<Control> controls)
			{
				this._linkLabel = new DesignerActionPanel.MethodLine.MethodItemLinkLabel();
				this._linkLabel.ActiveLinkColor = base.ActionPanel.ActiveLinkColor;
				this._linkLabel.AutoSize = false;
				this._linkLabel.BackColor = Color.Transparent;
				this._linkLabel.LinkBehavior = LinkBehavior.HoverUnderline;
				this._linkLabel.LinkColor = base.ActionPanel.LinkColor;
				this._linkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
				this._linkLabel.UseMnemonic = false;
				this._linkLabel.VisitedLinkColor = base.ActionPanel.LinkColor;
				this._linkLabel.LinkClicked += this.OnLinkLabelLinkClicked;
				controls.Add(this._linkLabel);
			}

			// Token: 0x06002AC8 RID: 10952 RVA: 0x00100B7A File Offset: 0x000FED7A
			public sealed override void Focus()
			{
				this._linkLabel.Focus();
			}

			// Token: 0x06002AC9 RID: 10953 RVA: 0x00100B88 File Offset: 0x000FED88
			public override Size LayoutControls(int top, int width, bool measureOnly)
			{
				Size preferredSize = this._linkLabel.GetPreferredSize(new Size(int.MaxValue, int.MaxValue));
				if (!measureOnly)
				{
					this._linkLabel.Location = new Point(5, top + 3);
					this._linkLabel.Size = preferredSize;
				}
				return preferredSize + new Size(9, 7);
			}

			// Token: 0x06002ACA RID: 10954 RVA: 0x00100BE4 File Offset: 0x000FEDE4
			private void OnLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
			{
				base.ActionPanel.InMethodInvoke = true;
				try
				{
					this._methodItem.Invoke();
				}
				catch (Exception innerException)
				{
					if (innerException is TargetInvocationException)
					{
						innerException = innerException.InnerException;
					}
					base.ActionPanel.ShowError(SR.GetString("DesignerActionPanel_ErrorInvokingAction", new object[]
					{
						this._methodItem.DisplayName,
						Environment.NewLine + innerException.Message
					}));
				}
				finally
				{
					base.ActionPanel.InMethodInvoke = false;
				}
			}

			// Token: 0x06002ACB RID: 10955 RVA: 0x00100C84 File Offset: 0x000FEE84
			internal override void UpdateActionItem(DesignerActionList actionList, DesignerActionItem actionItem, ToolTip toolTip, ref int currentTabIndex)
			{
				this._actionList = actionList;
				this._methodItem = (DesignerActionMethodItem)actionItem;
				toolTip.SetToolTip(this._linkLabel, this._methodItem.Description);
				this._linkLabel.Text = DesignerActionPanel.StripAmpersands(this._methodItem.DisplayName);
				this._linkLabel.AccessibleDescription = actionItem.Description;
				Control linkLabel = this._linkLabel;
				int num = currentTabIndex;
				currentTabIndex = num + 1;
				linkLabel.TabIndex = num;
			}

			// Token: 0x04001DE9 RID: 7657
			private DesignerActionList _actionList;

			// Token: 0x04001DEA RID: 7658
			private DesignerActionMethodItem _methodItem;

			// Token: 0x04001DEB RID: 7659
			private DesignerActionPanel.MethodLine.MethodItemLinkLabel _linkLabel;

			// Token: 0x020005CF RID: 1487
			private sealed class MethodItemLinkLabel : LinkLabel
			{
				// Token: 0x06003432 RID: 13362 RVA: 0x0011C6F4 File Offset: 0x0011A8F4
				protected override bool ProcessDialogKey(Keys keyData)
				{
					if ((keyData & Keys.Control) == Keys.Control)
					{
						Keys keys = keyData & Keys.KeyCode;
						if (keys == Keys.Tab)
						{
							return false;
						}
					}
					return base.ProcessDialogKey(keyData);
				}
			}
		}

		// Token: 0x0200048A RID: 1162
		private abstract class PropertyLine : DesignerActionPanel.Line
		{
			// Token: 0x06002ACC RID: 10956 RVA: 0x00100A86 File Offset: 0x000FEC86
			public PropertyLine(IServiceProvider serviceProvider, DesignerActionPanel actionPanel) : base(serviceProvider, actionPanel)
			{
			}

			// Token: 0x1700090F RID: 2319
			// (get) Token: 0x06002ACD RID: 10957 RVA: 0x00100CFD File Offset: 0x000FEEFD
			public sealed override string FocusId
			{
				get
				{
					return "PROPERTY:" + this._actionList.GetType().FullName + "." + this._propertyItem.MemberName;
				}
			}

			// Token: 0x17000910 RID: 2320
			// (get) Token: 0x06002ACE RID: 10958 RVA: 0x00100D29 File Offset: 0x000FEF29
			protected PropertyDescriptor PropertyDescriptor
			{
				get
				{
					if (this._propDesc == null)
					{
						this._propDesc = TypeDescriptor.GetProperties(this._actionList)[this._propertyItem.MemberName];
					}
					return this._propDesc;
				}
			}

			// Token: 0x17000911 RID: 2321
			// (get) Token: 0x06002ACF RID: 10959 RVA: 0x00100D5A File Offset: 0x000FEF5A
			protected DesignerActionPropertyItem PropertyItem
			{
				get
				{
					return this._propertyItem;
				}
			}

			// Token: 0x17000912 RID: 2322
			// (get) Token: 0x06002AD0 RID: 10960 RVA: 0x00100D62 File Offset: 0x000FEF62
			protected ITypeDescriptorContext TypeDescriptorContext
			{
				get
				{
					if (this._typeDescriptorContext == null)
					{
						this._typeDescriptorContext = new DesignerActionPanel.TypeDescriptorContext(base.ServiceProvider, this.PropertyDescriptor, this._actionList);
					}
					return this._typeDescriptorContext;
				}
			}

			// Token: 0x17000913 RID: 2323
			// (get) Token: 0x06002AD1 RID: 10961 RVA: 0x00100D8F File Offset: 0x000FEF8F
			protected object Value
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x06002AD2 RID: 10962
			protected abstract void OnPropertyTaskItemUpdated(ToolTip toolTip, ref int currentTabIndex);

			// Token: 0x06002AD3 RID: 10963
			protected abstract void OnValueChanged();

			// Token: 0x06002AD4 RID: 10964 RVA: 0x00100D98 File Offset: 0x000FEF98
			protected void SetValue(object newValue)
			{
				if (this._pushingValue || base.ActionPanel.DropDownActive)
				{
					return;
				}
				this._pushingValue = true;
				try
				{
					if (newValue != null)
					{
						Type type = newValue.GetType();
						if (!this.PropertyDescriptor.PropertyType.IsAssignableFrom(type) && this.PropertyDescriptor.Converter != null)
						{
							if (!this.PropertyDescriptor.Converter.CanConvertFrom(this._typeDescriptorContext, type))
							{
								base.ActionPanel.ShowError(SR.GetString("DesignerActionPanel_CouldNotConvertValue", new object[]
								{
									newValue,
									this._propDesc.PropertyType
								}));
								return;
							}
							newValue = this.PropertyDescriptor.Converter.ConvertFrom(this._typeDescriptorContext, CultureInfo.CurrentCulture, newValue);
						}
					}
					if (!object.Equals(this._value, newValue))
					{
						this.PropertyDescriptor.SetValue(this._actionList, newValue);
						this._value = this.PropertyDescriptor.GetValue(this._actionList);
						this.OnValueChanged();
					}
				}
				catch (Exception innerException)
				{
					if (innerException is TargetInvocationException)
					{
						innerException = innerException.InnerException;
					}
					base.ActionPanel.ShowError(SR.GetString("DesignerActionPanel_ErrorSettingValue", new object[]
					{
						newValue,
						this.PropertyDescriptor.Name,
						innerException.Message
					}));
				}
				finally
				{
					this._pushingValue = false;
				}
			}

			// Token: 0x06002AD5 RID: 10965 RVA: 0x00100F1C File Offset: 0x000FF11C
			internal sealed override void UpdateActionItem(DesignerActionList actionList, DesignerActionItem actionItem, ToolTip toolTip, ref int currentTabIndex)
			{
				this._actionList = actionList;
				this._propertyItem = (DesignerActionPropertyItem)actionItem;
				this._propDesc = null;
				this._typeDescriptorContext = null;
				this._value = this.PropertyDescriptor.GetValue(actionList);
				this.OnPropertyTaskItemUpdated(toolTip, ref currentTabIndex);
				this._pushingValue = true;
				try
				{
					this.OnValueChanged();
				}
				finally
				{
					this._pushingValue = false;
				}
			}

			// Token: 0x04001DEC RID: 7660
			private DesignerActionList _actionList;

			// Token: 0x04001DED RID: 7661
			private DesignerActionPropertyItem _propertyItem;

			// Token: 0x04001DEE RID: 7662
			private object _value;

			// Token: 0x04001DEF RID: 7663
			private bool _pushingValue;

			// Token: 0x04001DF0 RID: 7664
			private PropertyDescriptor _propDesc;

			// Token: 0x04001DF1 RID: 7665
			private ITypeDescriptorContext _typeDescriptorContext;
		}

		// Token: 0x0200048B RID: 1163
		private sealed class CheckBoxPropertyLine : DesignerActionPanel.PropertyLine
		{
			// Token: 0x06002AD6 RID: 10966 RVA: 0x00100F8C File Offset: 0x000FF18C
			public CheckBoxPropertyLine(IServiceProvider serviceProvider, DesignerActionPanel actionPanel) : base(serviceProvider, actionPanel)
			{
			}

			// Token: 0x06002AD7 RID: 10967 RVA: 0x00100F98 File Offset: 0x000FF198
			protected override void AddControls(List<Control> controls)
			{
				this._checkBox = new CheckBox();
				this._checkBox.BackColor = Color.Transparent;
				this._checkBox.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;
				this._checkBox.CheckedChanged += this.OnCheckBoxCheckedChanged;
				this._checkBox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
				this._checkBox.UseMnemonic = false;
				this._checkBox.ForeColor = base.ActionPanel.LabelForeColor;
				controls.Add(this._checkBox);
			}

			// Token: 0x06002AD8 RID: 10968 RVA: 0x0010101F File Offset: 0x000FF21F
			public sealed override void Focus()
			{
				this._checkBox.Focus();
			}

			// Token: 0x06002AD9 RID: 10969 RVA: 0x00101030 File Offset: 0x000FF230
			public override Size LayoutControls(int top, int width, bool measureOnly)
			{
				Size preferredSize = this._checkBox.GetPreferredSize(new Size(int.MaxValue, int.MaxValue));
				if (!measureOnly)
				{
					this._checkBox.Location = new Point(5, top + 3);
					this._checkBox.Size = preferredSize;
				}
				return preferredSize + new Size(9, 7);
			}

			// Token: 0x06002ADA RID: 10970 RVA: 0x00101089 File Offset: 0x000FF289
			private void OnCheckBoxCheckedChanged(object sender, EventArgs e)
			{
				base.SetValue(this._checkBox.Checked);
			}

			// Token: 0x06002ADB RID: 10971 RVA: 0x001010A4 File Offset: 0x000FF2A4
			protected override void OnPropertyTaskItemUpdated(ToolTip toolTip, ref int currentTabIndex)
			{
				this._checkBox.Text = DesignerActionPanel.StripAmpersands(base.PropertyItem.DisplayName);
				this._checkBox.AccessibleDescription = base.PropertyItem.Description;
				Control checkBox = this._checkBox;
				int num = currentTabIndex;
				currentTabIndex = num + 1;
				checkBox.TabIndex = num;
				toolTip.SetToolTip(this._checkBox, base.PropertyItem.Description);
			}

			// Token: 0x06002ADC RID: 10972 RVA: 0x0010110D File Offset: 0x000FF30D
			protected override void OnValueChanged()
			{
				this._checkBox.Checked = (bool)base.Value;
			}

			// Token: 0x04001DF2 RID: 7666
			private CheckBox _checkBox;
		}

		// Token: 0x0200048C RID: 1164
		private class TextBoxPropertyLine : DesignerActionPanel.PropertyLine
		{
			// Token: 0x06002ADD RID: 10973 RVA: 0x00100F8C File Offset: 0x000FF18C
			public TextBoxPropertyLine(IServiceProvider serviceProvider, DesignerActionPanel actionPanel) : base(serviceProvider, actionPanel)
			{
			}

			// Token: 0x17000914 RID: 2324
			// (get) Token: 0x06002ADE RID: 10974 RVA: 0x00101125 File Offset: 0x000FF325
			protected Control EditControl
			{
				get
				{
					return this._editControl;
				}
			}

			// Token: 0x17000915 RID: 2325
			// (get) Token: 0x06002ADF RID: 10975 RVA: 0x0010112D File Offset: 0x000FF32D
			protected Point EditRegionLocation
			{
				get
				{
					return this._editRegionLocation;
				}
			}

			// Token: 0x17000916 RID: 2326
			// (get) Token: 0x06002AE0 RID: 10976 RVA: 0x00101135 File Offset: 0x000FF335
			protected Point EditRegionRelativeLocation
			{
				get
				{
					return this._editRegionRelativeLocation;
				}
			}

			// Token: 0x17000917 RID: 2327
			// (get) Token: 0x06002AE1 RID: 10977 RVA: 0x0010113D File Offset: 0x000FF33D
			protected Size EditRegionSize
			{
				get
				{
					return this._editRegionSize;
				}
			}

			// Token: 0x06002AE2 RID: 10978 RVA: 0x00101148 File Offset: 0x000FF348
			protected override void AddControls(List<Control> controls)
			{
				this._label = new Label();
				this._label.BackColor = Color.Transparent;
				this._label.ForeColor = base.ActionPanel.LabelForeColor;
				this._label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
				this._label.UseMnemonic = false;
				this._readOnlyTextBoxLabel = new DesignerActionPanel.TextBoxPropertyLine.EditorLabel();
				this._readOnlyTextBoxLabel.BackColor = Color.Transparent;
				this._readOnlyTextBoxLabel.ForeColor = SystemColors.WindowText;
				this._readOnlyTextBoxLabel.TabStop = true;
				this._readOnlyTextBoxLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft;
				this._readOnlyTextBoxLabel.UseMnemonic = false;
				this._readOnlyTextBoxLabel.Visible = false;
				this._readOnlyTextBoxLabel.MouseClick += this.OnReadOnlyTextBoxLabelClick;
				this._readOnlyTextBoxLabel.Enter += this.OnReadOnlyTextBoxLabelEnter;
				this._readOnlyTextBoxLabel.Leave += this.OnReadOnlyTextBoxLabelLeave;
				this._readOnlyTextBoxLabel.KeyDown += this.OnReadOnlyTextBoxLabelKeyDown;
				this._textBox = new TextBox();
				this._textBox.BorderStyle = BorderStyle.None;
				this._textBox.TextAlign = HorizontalAlignment.Left;
				this._textBox.Visible = false;
				this._textBox.TextChanged += this.OnTextBoxTextChanged;
				this._textBox.KeyDown += this.OnTextBoxKeyDown;
				this._textBox.LostFocus += this.OnTextBoxLostFocus;
				controls.Add(this._readOnlyTextBoxLabel);
				controls.Add(this._textBox);
				controls.Add(this._label);
			}

			// Token: 0x06002AE3 RID: 10979 RVA: 0x001012EF File Offset: 0x000FF4EF
			public sealed override void Focus()
			{
				this._editControl.Focus();
			}

			// Token: 0x06002AE4 RID: 10980 RVA: 0x00101300 File Offset: 0x000FF500
			internal int GetEditRegionXPos()
			{
				if (string.IsNullOrEmpty(this._label.Text))
				{
					return 5;
				}
				return 5 + this._label.GetPreferredSize(new Size(int.MaxValue, int.MaxValue)).Width + 5;
			}

			// Token: 0x06002AE5 RID: 10981 RVA: 0x00003B0F File Offset: 0x00001D0F
			protected virtual int GetTextBoxLeftPadding(int textBoxHeight)
			{
				return 1;
			}

			// Token: 0x06002AE6 RID: 10982 RVA: 0x00003B0F File Offset: 0x00001D0F
			protected virtual int GetTextBoxRightPadding(int textBoxHeight)
			{
				return 1;
			}

			// Token: 0x06002AE7 RID: 10983 RVA: 0x00101348 File Offset: 0x000FF548
			public override Size LayoutControls(int top, int width, bool measureOnly)
			{
				int num = this._textBox.GetPreferredSize(new Size(int.MaxValue, int.MaxValue)).Height;
				num += 2;
				int height = num + 7 + 2 + 2;
				int num2 = Math.Max(this._editXPos, this.GetEditRegionXPos());
				int num3 = num2 + 150 + 4;
				width = Math.Max(width, num3);
				int num4 = width - num3;
				if (!measureOnly)
				{
					this._editRegionLocation = new Point(num2, top + 4);
					this._editRegionRelativeLocation = new Point(num2, 4);
					this._editRegionSize = new Size(150 + num4, num + 2);
					this._label.Location = new Point(5, top);
					int width2 = this._label.GetPreferredSize(new Size(int.MaxValue, int.MaxValue)).Width;
					this._label.Size = new Size(width2, height);
					int num5 = 0;
					if (this._editControl is TextBox)
					{
						num5 = 2;
					}
					this._editControl.Location = new Point(this._editRegionLocation.X + this.GetTextBoxLeftPadding(num) + 1 + num5, this._editRegionLocation.Y + 1 + 1);
					this._editControl.Width = this._editRegionSize.Width - this.GetTextBoxRightPadding(num) - this.GetTextBoxLeftPadding(num) - num5;
					this._editControl.Height = this._editRegionSize.Height - 2 - 1;
				}
				return new Size(width, height);
			}

			// Token: 0x06002AE8 RID: 10984 RVA: 0x001014C5 File Offset: 0x000FF6C5
			protected virtual bool IsReadOnly()
			{
				return DesignerActionPanel.IsReadOnlyProperty(base.PropertyDescriptor);
			}

			// Token: 0x06002AE9 RID: 10985 RVA: 0x001014D4 File Offset: 0x000FF6D4
			protected override void OnPropertyTaskItemUpdated(ToolTip toolTip, ref int currentTabIndex)
			{
				this._label.Text = DesignerActionPanel.StripAmpersands(base.PropertyItem.DisplayName);
				Control label = this._label;
				int num = currentTabIndex;
				currentTabIndex = num + 1;
				label.TabIndex = num;
				toolTip.SetToolTip(this._label, base.PropertyItem.Description);
				this._textBoxDirty = false;
				if (this.IsReadOnly())
				{
					this._readOnlyTextBoxLabel.Visible = true;
					this._textBox.Visible = false;
					this._textBox.Location = new Point(int.MaxValue, int.MaxValue);
					this._editControl = this._readOnlyTextBoxLabel;
				}
				else
				{
					this._readOnlyTextBoxLabel.Visible = false;
					this._readOnlyTextBoxLabel.Location = new Point(int.MaxValue, int.MaxValue);
					this._textBox.Visible = true;
					this._editControl = this._textBox;
				}
				this._editControl.AccessibleDescription = base.PropertyItem.Description;
				this._editControl.AccessibleName = DesignerActionPanel.StripAmpersands(base.PropertyItem.DisplayName);
				Control editControl = this._editControl;
				num = currentTabIndex;
				currentTabIndex = num + 1;
				editControl.TabIndex = num;
				this._editControl.BringToFront();
			}

			// Token: 0x06002AEA RID: 10986 RVA: 0x00101604 File Offset: 0x000FF804
			protected virtual void OnReadOnlyTextBoxLabelClick(object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left)
				{
					this.Focus();
				}
			}

			// Token: 0x06002AEB RID: 10987 RVA: 0x00101619 File Offset: 0x000FF819
			private void OnReadOnlyTextBoxLabelEnter(object sender, EventArgs e)
			{
				this._readOnlyTextBoxLabel.ForeColor = SystemColors.HighlightText;
				this._readOnlyTextBoxLabel.BackColor = SystemColors.Highlight;
			}

			// Token: 0x06002AEC RID: 10988 RVA: 0x0010163B File Offset: 0x000FF83B
			private void OnReadOnlyTextBoxLabelLeave(object sender, EventArgs e)
			{
				this._readOnlyTextBoxLabel.ForeColor = SystemColors.WindowText;
				this._readOnlyTextBoxLabel.BackColor = SystemColors.Window;
			}

			// Token: 0x06002AED RID: 10989 RVA: 0x00101660 File Offset: 0x000FF860
			protected TypeConverter.StandardValuesCollection GetStandardValues()
			{
				TypeConverter converter = base.PropertyDescriptor.Converter;
				if (converter != null && converter.GetStandardValuesSupported(base.TypeDescriptorContext))
				{
					return converter.GetStandardValues(base.TypeDescriptorContext);
				}
				return null;
			}

			// Token: 0x06002AEE RID: 10990 RVA: 0x00101698 File Offset: 0x000FF898
			private void OnEditControlKeyDown(KeyEventArgs e)
			{
				if (e.KeyCode == Keys.Down)
				{
					e.Handled = true;
					TypeConverter.StandardValuesCollection standardValues = this.GetStandardValues();
					if (standardValues != null)
					{
						for (int i = 0; i < standardValues.Count; i++)
						{
							if (object.Equals(base.Value, standardValues[i]))
							{
								if (i < standardValues.Count - 1)
								{
									base.SetValue(standardValues[i + 1]);
								}
								return;
							}
						}
						if (standardValues.Count > 0)
						{
							base.SetValue(standardValues[0]);
						}
					}
					return;
				}
				if (e.KeyCode == Keys.Up)
				{
					e.Handled = true;
					TypeConverter.StandardValuesCollection standardValues2 = this.GetStandardValues();
					if (standardValues2 != null)
					{
						for (int j = 0; j < standardValues2.Count; j++)
						{
							if (object.Equals(base.Value, standardValues2[j]))
							{
								if (j > 0)
								{
									base.SetValue(standardValues2[j - 1]);
								}
								return;
							}
						}
						if (standardValues2.Count > 0)
						{
							base.SetValue(standardValues2[standardValues2.Count - 1]);
						}
					}
					return;
				}
			}

			// Token: 0x06002AEF RID: 10991 RVA: 0x00101789 File Offset: 0x000FF989
			private void OnReadOnlyTextBoxLabelKeyDown(object sender, KeyEventArgs e)
			{
				this.OnEditControlKeyDown(e);
			}

			// Token: 0x06002AF0 RID: 10992 RVA: 0x00101792 File Offset: 0x000FF992
			private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
			{
				if (base.ActionPanel.DropDownActive)
				{
					return;
				}
				if (e.KeyCode == Keys.Return)
				{
					this.UpdateValue();
					e.Handled = true;
					return;
				}
				this.OnEditControlKeyDown(e);
			}

			// Token: 0x06002AF1 RID: 10993 RVA: 0x001017C1 File Offset: 0x000FF9C1
			private void OnTextBoxLostFocus(object sender, EventArgs e)
			{
				if (base.ActionPanel.DropDownActive)
				{
					return;
				}
				this.UpdateValue();
			}

			// Token: 0x06002AF2 RID: 10994 RVA: 0x001017D7 File Offset: 0x000FF9D7
			private void OnTextBoxTextChanged(object sender, EventArgs e)
			{
				this._textBoxDirty = true;
			}

			// Token: 0x06002AF3 RID: 10995 RVA: 0x001017E0 File Offset: 0x000FF9E0
			protected override void OnValueChanged()
			{
				this._editControl.Text = base.PropertyDescriptor.Converter.ConvertToString(base.TypeDescriptorContext, base.Value);
			}

			// Token: 0x06002AF4 RID: 10996 RVA: 0x0010180C File Offset: 0x000FFA0C
			public override void PaintLine(Graphics g, int lineWidth, int lineHeight)
			{
				Rectangle rect = new Rectangle(this.EditRegionRelativeLocation, this.EditRegionSize);
				g.FillRectangle(SystemBrushes.Window, rect);
				g.DrawRectangle(SystemPens.ControlDark, rect);
			}

			// Token: 0x06002AF5 RID: 10997 RVA: 0x00101844 File Offset: 0x000FFA44
			internal void SetEditRegionXPos(int xPos)
			{
				if (!string.IsNullOrEmpty(this._label.Text))
				{
					this._editXPos = xPos;
					return;
				}
				this._editXPos = 5;
			}

			// Token: 0x06002AF6 RID: 10998 RVA: 0x00101867 File Offset: 0x000FFA67
			private void UpdateValue()
			{
				if (this._textBoxDirty)
				{
					base.SetValue(this._editControl.Text);
					this._textBoxDirty = false;
				}
			}

			// Token: 0x04001DF3 RID: 7667
			private TextBox _textBox;

			// Token: 0x04001DF4 RID: 7668
			private DesignerActionPanel.TextBoxPropertyLine.EditorLabel _readOnlyTextBoxLabel;

			// Token: 0x04001DF5 RID: 7669
			private Control _editControl;

			// Token: 0x04001DF6 RID: 7670
			private Label _label;

			// Token: 0x04001DF7 RID: 7671
			private int _editXPos;

			// Token: 0x04001DF8 RID: 7672
			private bool _textBoxDirty;

			// Token: 0x04001DF9 RID: 7673
			private Point _editRegionLocation;

			// Token: 0x04001DFA RID: 7674
			private Point _editRegionRelativeLocation;

			// Token: 0x04001DFB RID: 7675
			private Size _editRegionSize;

			// Token: 0x020005D0 RID: 1488
			private sealed class EditorLabel : Label
			{
				// Token: 0x06003434 RID: 13364 RVA: 0x0011C725 File Offset: 0x0011A925
				public EditorLabel()
				{
					base.SetStyle(ControlStyles.Selectable, true);
				}

				// Token: 0x06003435 RID: 13365 RVA: 0x0011C739 File Offset: 0x0011A939
				protected override AccessibleObject CreateAccessibilityInstance()
				{
					return new DesignerActionPanel.TextBoxPropertyLine.EditorLabel.EditorLabelAccessibleObject(this);
				}

				// Token: 0x06003436 RID: 13366 RVA: 0x0011C741 File Offset: 0x0011A941
				protected override void OnGotFocus(EventArgs e)
				{
					base.OnGotFocus(e);
					base.AccessibilityNotifyClients(AccessibleEvents.Focus, 0, -1);
				}

				// Token: 0x06003437 RID: 13367 RVA: 0x0011C757 File Offset: 0x0011A957
				protected override bool IsInputKey(Keys keyData)
				{
					return keyData == Keys.Down || keyData == Keys.Up || base.IsInputKey(keyData);
				}

				// Token: 0x020005F6 RID: 1526
				private sealed class EditorLabelAccessibleObject : Control.ControlAccessibleObject
				{
					// Token: 0x060034FE RID: 13566 RVA: 0x0011F032 File Offset: 0x0011D232
					public EditorLabelAccessibleObject(DesignerActionPanel.TextBoxPropertyLine.EditorLabel owner) : base(owner)
					{
					}

					// Token: 0x17000A38 RID: 2616
					// (get) Token: 0x060034FF RID: 13567 RVA: 0x0011F8C0 File Offset: 0x0011DAC0
					public override string Value
					{
						get
						{
							return base.Owner.Text;
						}
					}
				}
			}
		}

		// Token: 0x0200048D RID: 1165
		private sealed class EditorPropertyLine : DesignerActionPanel.TextBoxPropertyLine, IWindowsFormsEditorService, IServiceProvider
		{
			// Token: 0x06002AF7 RID: 10999 RVA: 0x00101889 File Offset: 0x000FFA89
			public EditorPropertyLine(IServiceProvider serviceProvider, DesignerActionPanel actionPanel) : base(serviceProvider, actionPanel)
			{
			}

			// Token: 0x06002AF8 RID: 11000 RVA: 0x00101894 File Offset: 0x000FFA94
			private void ActivateDropDown()
			{
				if (this._editor != null)
				{
					try
					{
						object value = this._editor.EditValue(base.TypeDescriptorContext, this, base.Value);
						base.SetValue(value);
						return;
					}
					catch (Exception ex)
					{
						base.ActionPanel.ShowError(SR.GetString("DesignerActionPanel_ErrorActivatingDropDown", new object[]
						{
							ex.Message
						}));
						return;
					}
				}
				ListBox listBox = new ListBox();
				listBox.BorderStyle = BorderStyle.None;
				listBox.IntegralHeight = false;
				listBox.Font = base.ActionPanel.Font;
				listBox.SelectedIndexChanged += this.OnListBoxSelectedIndexChanged;
				listBox.KeyDown += this.OnListBoxKeyDown;
				TypeConverter.StandardValuesCollection standardValues = base.GetStandardValues();
				if (standardValues != null)
				{
					foreach (object obj in standardValues)
					{
						string text = base.PropertyDescriptor.Converter.ConvertToString(base.TypeDescriptorContext, CultureInfo.CurrentCulture, obj);
						listBox.Items.Add(text);
						if (obj != null && obj.Equals(base.Value))
						{
							listBox.SelectedItem = text;
						}
					}
				}
				int num = 0;
				IntPtr dc = DesignerActionPanel.EditorPropertyLine.UnsafeNativeMethods.GetDC(new HandleRef(listBox, listBox.Handle));
				IntPtr handle = listBox.Font.ToHfont();
				DesignerActionPanel.EditorPropertyLine.NativeMethods.CommonHandles.GdiHandleCollector.Add();
				DesignerActionPanel.EditorPropertyLine.NativeMethods.TEXTMETRIC textmetric = default(DesignerActionPanel.EditorPropertyLine.NativeMethods.TEXTMETRIC);
				try
				{
					handle = DesignerActionPanel.EditorPropertyLine.SafeNativeMethods.SelectObject(new HandleRef(listBox, dc), new HandleRef(listBox.Font, handle));
					if (listBox.Items.Count > 0)
					{
						DesignerActionPanel.EditorPropertyLine.NativeMethods.SIZE size = new DesignerActionPanel.EditorPropertyLine.NativeMethods.SIZE();
						foreach (object obj2 in listBox.Items)
						{
							string text2 = (string)obj2;
							DesignerActionPanel.EditorPropertyLine.SafeNativeMethods.GetTextExtentPoint32(new HandleRef(listBox, dc), text2, text2.Length, size);
							num = Math.Max(size.cx, num);
						}
					}
					DesignerActionPanel.EditorPropertyLine.SafeNativeMethods.GetTextMetrics(new HandleRef(listBox, dc), ref textmetric);
					num += 2 + textmetric.tmMaxCharWidth + SystemInformation.VerticalScrollBarWidth;
					handle = DesignerActionPanel.EditorPropertyLine.SafeNativeMethods.SelectObject(new HandleRef(listBox, dc), new HandleRef(listBox.Font, handle));
				}
				finally
				{
					DesignerActionPanel.EditorPropertyLine.SafeNativeMethods.DeleteObject(new HandleRef(listBox.Font, handle));
					DesignerActionPanel.EditorPropertyLine.UnsafeNativeMethods.ReleaseDC(new HandleRef(listBox, listBox.Handle), new HandleRef(listBox, dc));
				}
				listBox.Height = Math.Max(textmetric.tmHeight + 2, Math.Min(200, listBox.PreferredHeight));
				listBox.Width = Math.Max(num, base.EditRegionSize.Width);
				this._ignoreDropDownValue = false;
				try
				{
					this.ShowDropDown(listBox, SystemColors.ControlDark);
				}
				finally
				{
					listBox.SelectedIndexChanged -= this.OnListBoxSelectedIndexChanged;
					listBox.KeyDown -= this.OnListBoxKeyDown;
				}
				if (!this._ignoreDropDownValue && listBox.SelectedItem != null)
				{
					base.SetValue(listBox.SelectedItem);
				}
			}

			// Token: 0x06002AF9 RID: 11001 RVA: 0x00101BD8 File Offset: 0x000FFDD8
			protected override void AddControls(List<Control> controls)
			{
				base.AddControls(controls);
				this._button = new DesignerActionPanel.EditorPropertyLine.EditorButton();
				this._button.Click += this.OnButtonClick;
				this._button.GotFocus += this.OnButtonGotFocus;
				controls.Add(this._button);
			}

			// Token: 0x06002AFA RID: 11002 RVA: 0x00101C31 File Offset: 0x000FFE31
			private void CloseDropDown()
			{
				if (this._dropDownHolder != null)
				{
					this._dropDownHolder.Visible = false;
				}
			}

			// Token: 0x06002AFB RID: 11003 RVA: 0x00101C47 File Offset: 0x000FFE47
			protected override int GetTextBoxLeftPadding(int textBoxHeight)
			{
				if (this._hasSwatch)
				{
					return base.GetTextBoxLeftPadding(textBoxHeight) + textBoxHeight + 2;
				}
				return base.GetTextBoxLeftPadding(textBoxHeight);
			}

			// Token: 0x06002AFC RID: 11004 RVA: 0x00101C64 File Offset: 0x000FFE64
			protected override int GetTextBoxRightPadding(int textBoxHeight)
			{
				return base.GetTextBoxRightPadding(textBoxHeight) + textBoxHeight + 2;
			}

			// Token: 0x06002AFD RID: 11005 RVA: 0x00101C74 File Offset: 0x000FFE74
			protected override bool IsReadOnly()
			{
				if (base.IsReadOnly())
				{
					return true;
				}
				bool flag = !base.PropertyDescriptor.Converter.CanConvertFrom(base.TypeDescriptorContext, typeof(string));
				bool flag2 = base.PropertyDescriptor.Converter.GetStandardValuesSupported(base.TypeDescriptorContext) && base.PropertyDescriptor.Converter.GetStandardValuesExclusive(base.TypeDescriptorContext);
				return flag || flag2;
			}

			// Token: 0x06002AFE RID: 11006 RVA: 0x00101CE4 File Offset: 0x000FFEE4
			public override Size LayoutControls(int top, int width, bool measureOnly)
			{
				Size result = base.LayoutControls(top, width, measureOnly);
				if (!measureOnly)
				{
					int num = base.EditRegionSize.Height - 2 - 1;
					this._button.Location = new Point(base.EditRegionLocation.X + base.EditRegionSize.Width - num - 1, base.EditRegionLocation.Y + 1 + 1);
					this._button.Size = new Size(num, num);
				}
				return result;
			}

			// Token: 0x06002AFF RID: 11007 RVA: 0x00101D67 File Offset: 0x000FFF67
			private void OnButtonClick(object sender, EventArgs e)
			{
				this.ActivateDropDown();
			}

			// Token: 0x06002B00 RID: 11008 RVA: 0x00101D6F File Offset: 0x000FFF6F
			private void OnButtonGotFocus(object sender, EventArgs e)
			{
				if (!this._button.Ellipsis)
				{
					this.Focus();
				}
			}

			// Token: 0x06002B01 RID: 11009 RVA: 0x00101D84 File Offset: 0x000FFF84
			private void OnListBoxKeyDown(object sender, KeyEventArgs e)
			{
				if (e.KeyData == Keys.Return)
				{
					this._ignoreNextSelectChange = false;
					this.CloseDropDown();
					e.Handled = true;
					return;
				}
				this._ignoreNextSelectChange = true;
			}

			// Token: 0x06002B02 RID: 11010 RVA: 0x00101DAC File Offset: 0x000FFFAC
			private void OnListBoxSelectedIndexChanged(object sender, EventArgs e)
			{
				if (this._ignoreNextSelectChange)
				{
					this._ignoreNextSelectChange = false;
					return;
				}
				this.CloseDropDown();
			}

			// Token: 0x06002B03 RID: 11011 RVA: 0x00101DC4 File Offset: 0x000FFFC4
			protected override void OnPropertyTaskItemUpdated(ToolTip toolTip, ref int currentTabIndex)
			{
				this._editor = (UITypeEditor)base.PropertyDescriptor.GetEditor(typeof(UITypeEditor));
				base.OnPropertyTaskItemUpdated(toolTip, ref currentTabIndex);
				if (this._editor != null)
				{
					this._button.Ellipsis = (this._editor.GetEditStyle(base.TypeDescriptorContext) == UITypeEditorEditStyle.Modal);
					this._hasSwatch = this._editor.GetPaintValueSupported(base.TypeDescriptorContext);
				}
				else
				{
					this._button.Ellipsis = false;
				}
				if (this._button.Ellipsis)
				{
					base.EditControl.AccessibleRole = (this.IsReadOnly() ? AccessibleRole.StaticText : AccessibleRole.Text);
				}
				else
				{
					base.EditControl.AccessibleRole = (this.IsReadOnly() ? AccessibleRole.DropList : AccessibleRole.ComboBox);
				}
				this._button.TabStop = this._button.Ellipsis;
				Control button = this._button;
				int num = currentTabIndex;
				currentTabIndex = num + 1;
				button.TabIndex = num;
				this._button.AccessibleRole = (this._button.Ellipsis ? AccessibleRole.PushButton : AccessibleRole.ButtonDropDown);
				this._button.AccessibleDescription = base.EditControl.AccessibleDescription;
				this._button.AccessibleName = base.EditControl.AccessibleName;
			}

			// Token: 0x06002B04 RID: 11012 RVA: 0x00101EFA File Offset: 0x001000FA
			protected override void OnReadOnlyTextBoxLabelClick(object sender, MouseEventArgs e)
			{
				base.OnReadOnlyTextBoxLabelClick(sender, e);
				if (e.Button == MouseButtons.Left)
				{
					if (base.ActionPanel.DropDownActive)
					{
						this._ignoreDropDownValue = true;
						this.CloseDropDown();
						return;
					}
					this.ActivateDropDown();
				}
			}

			// Token: 0x06002B05 RID: 11013 RVA: 0x00101F32 File Offset: 0x00100132
			protected override void OnValueChanged()
			{
				base.OnValueChanged();
				this._swatch = null;
				if (this._hasSwatch)
				{
					base.ActionPanel.Invalidate(new Rectangle(base.EditRegionLocation, base.EditRegionSize), false);
				}
			}

			// Token: 0x06002B06 RID: 11014 RVA: 0x00101F68 File Offset: 0x00100168
			public override void PaintLine(Graphics g, int lineWidth, int lineHeight)
			{
				base.PaintLine(g, lineWidth, lineHeight);
				if (this._hasSwatch)
				{
					if (this._swatch == null)
					{
						int num = base.EditRegionSize.Height - 2;
						int num2 = num - 1;
						this._swatch = new Bitmap(num, num2);
						Rectangle rectangle = new Rectangle(1, 1, num - 2, num2 - 2);
						using (Graphics graphics = Graphics.FromImage(this._swatch))
						{
							this._editor.PaintValue(base.Value, graphics, rectangle);
							graphics.DrawRectangle(SystemPens.ControlDark, new Rectangle(0, 0, num - 1, num2 - 1));
						}
					}
					g.DrawImage(this._swatch, new Point(base.EditRegionRelativeLocation.X + 2, 6));
				}
			}

			// Token: 0x06002B07 RID: 11015 RVA: 0x00102040 File Offset: 0x00100240
			protected internal override bool ProcessDialogKey(Keys keyData)
			{
				if (!this._button.Focused && !this._button.Ellipsis && !base.ActionPanel.DropDownActive && (keyData == (Keys.Back | Keys.Space | Keys.Alt) || keyData == (Keys.RButton | Keys.MButton | Keys.Space | Keys.Alt) || keyData == Keys.F4))
				{
					this.ActivateDropDown();
					return true;
				}
				return base.ProcessDialogKey(keyData);
			}

			// Token: 0x06002B08 RID: 11016 RVA: 0x00102098 File Offset: 0x00100298
			private void ShowDropDown(Control hostedControl, Color borderColor)
			{
				hostedControl.Width = Math.Max(hostedControl.Width, base.EditRegionSize.Width - 2);
				this._dropDownHolder = new DesignerActionPanel.EditorPropertyLine.DropDownHolder(hostedControl, base.ActionPanel, borderColor, base.ActionPanel.Font, this);
				if (base.ActionPanel.RightToLeft != RightToLeft.Yes)
				{
					Rectangle r = new Rectangle(Point.Empty, base.EditRegionSize);
					Size size = this._dropDownHolder.Size;
					Point location = base.ActionPanel.PointToScreen(base.EditRegionLocation);
					Rectangle workingArea = Screen.FromRectangle(base.ActionPanel.RectangleToScreen(r)).WorkingArea;
					size.Width = Math.Max(r.Width + 1, size.Width);
					location.X = Math.Min(workingArea.Right - size.Width, Math.Max(workingArea.X, location.X + r.Right - size.Width));
					location.Y += r.Y;
					if (workingArea.Bottom < size.Height + location.Y + r.Height)
					{
						location.Y -= size.Height + 1;
					}
					else
					{
						location.Y += r.Height;
					}
					this._dropDownHolder.Location = location;
				}
				else
				{
					this._dropDownHolder.RightToLeft = base.ActionPanel.RightToLeft;
					Rectangle r2 = new Rectangle(Point.Empty, base.EditRegionSize);
					Size size2 = this._dropDownHolder.Size;
					Point location2 = base.ActionPanel.PointToScreen(base.EditRegionLocation);
					Rectangle workingArea2 = Screen.FromRectangle(base.ActionPanel.RectangleToScreen(r2)).WorkingArea;
					size2.Width = Math.Max(r2.Width + 1, size2.Width);
					location2.X = Math.Min(workingArea2.Right - size2.Width, Math.Max(workingArea2.X, location2.X - r2.Width));
					location2.Y += r2.Y;
					if (workingArea2.Bottom < size2.Height + location2.Y + r2.Height)
					{
						location2.Y -= size2.Height + 1;
					}
					else
					{
						location2.Y += r2.Height;
					}
					this._dropDownHolder.Location = location2;
				}
				base.ActionPanel.InMethodInvoke = true;
				base.ActionPanel.SetDropDownActive(true);
				try
				{
					this._dropDownHolder.ShowDropDown(this._button);
				}
				finally
				{
					this._button.ResetMouseStates();
					base.ActionPanel.SetDropDownActive(false);
					base.ActionPanel.InMethodInvoke = false;
				}
			}

			// Token: 0x06002B09 RID: 11017 RVA: 0x00102390 File Offset: 0x00100590
			void IWindowsFormsEditorService.CloseDropDown()
			{
				this.CloseDropDown();
			}

			// Token: 0x06002B0A RID: 11018 RVA: 0x00102398 File Offset: 0x00100598
			void IWindowsFormsEditorService.DropDownControl(Control control)
			{
				this.ShowDropDown(control, base.ActionPanel.BorderColor);
			}

			// Token: 0x06002B0B RID: 11019 RVA: 0x001023AC File Offset: 0x001005AC
			DialogResult IWindowsFormsEditorService.ShowDialog(Form dialog)
			{
				IUIService iuiservice = (IUIService)base.ServiceProvider.GetService(typeof(IUIService));
				if (iuiservice != null)
				{
					return iuiservice.ShowDialog(dialog);
				}
				return dialog.ShowDialog();
			}

			// Token: 0x06002B0C RID: 11020 RVA: 0x001023E5 File Offset: 0x001005E5
			object IServiceProvider.GetService(Type serviceType)
			{
				if (serviceType == typeof(IWindowsFormsEditorService))
				{
					return this;
				}
				return base.ServiceProvider.GetService(serviceType);
			}

			// Token: 0x04001DFC RID: 7676
			private DesignerActionPanel.EditorPropertyLine.EditorButton _button;

			// Token: 0x04001DFD RID: 7677
			private UITypeEditor _editor;

			// Token: 0x04001DFE RID: 7678
			private bool _hasSwatch;

			// Token: 0x04001DFF RID: 7679
			private Image _swatch;

			// Token: 0x04001E00 RID: 7680
			private DesignerActionPanel.EditorPropertyLine.FlyoutDialog _dropDownHolder;

			// Token: 0x04001E01 RID: 7681
			private bool _ignoreNextSelectChange;

			// Token: 0x04001E02 RID: 7682
			private bool _ignoreDropDownValue;

			// Token: 0x020005D1 RID: 1489
			private class DropDownHolder : DesignerActionPanel.EditorPropertyLine.FlyoutDialog
			{
				// Token: 0x06003438 RID: 13368 RVA: 0x0011C76C File Offset: 0x0011A96C
				public DropDownHolder(Control hostedControl, Control parentControl, Color borderColor, Font font, DesignerActionPanel.EditorPropertyLine parent) : base(hostedControl, parentControl, borderColor, font)
				{
					this._parent = parent;
					this._parent.ActionPanel.SetDropDownActive(true);
				}

				// Token: 0x06003439 RID: 13369 RVA: 0x0011C792 File Offset: 0x0011A992
				protected override void OnClosed(EventArgs e)
				{
					base.OnClosed(e);
					this._parent.ActionPanel.SetDropDownActive(false);
				}

				// Token: 0x0600343A RID: 13370 RVA: 0x0011C7AC File Offset: 0x0011A9AC
				protected override bool ProcessDialogKey(Keys keyData)
				{
					if (keyData == Keys.Escape)
					{
						this._parent._ignoreDropDownValue = true;
						base.Visible = false;
						return true;
					}
					return base.ProcessDialogKey(keyData);
				}

				// Token: 0x040022E4 RID: 8932
				private DesignerActionPanel.EditorPropertyLine _parent;
			}

			// Token: 0x020005D2 RID: 1490
			internal class FlyoutDialog : Form
			{
				// Token: 0x0600343B RID: 13371 RVA: 0x0011C7D0 File Offset: 0x0011A9D0
				public FlyoutDialog(Control hostedControl, Control parentControl, Color borderColor, Font font)
				{
					this._hostedControl = hostedControl;
					this._parentControl = parentControl;
					this.BackColor = SystemColors.Window;
					base.ControlBox = false;
					this.Font = font;
					base.FormBorderStyle = FormBorderStyle.None;
					base.MinimizeBox = false;
					base.MaximizeBox = false;
					base.ShowInTaskbar = false;
					base.StartPosition = FormStartPosition.Manual;
					this.Text = string.Empty;
					base.SuspendLayout();
					try
					{
						base.Controls.Add(hostedControl);
						int num = Math.Max(this._hostedControl.Width, SystemInformation.MinimumWindowSize.Width);
						int num2 = Math.Max(this._hostedControl.Height, SystemInformation.MinimizedWindowSize.Height);
						if (!borderColor.IsEmpty)
						{
							base.DockPadding.All = 1;
							this.BackColor = borderColor;
							num += 2;
							num2 += 4;
						}
						this._hostedControl.Dock = DockStyle.Fill;
						base.Width = num;
						base.Height = num2;
					}
					finally
					{
						base.ResumeLayout();
					}
				}

				// Token: 0x17000A20 RID: 2592
				// (get) Token: 0x0600343C RID: 13372 RVA: 0x0011C8E0 File Offset: 0x0011AAE0
				protected override CreateParams CreateParams
				{
					get
					{
						CreateParams createParams = base.CreateParams;
						createParams.ExStyle |= 128;
						createParams.Style |= -2139095040;
						createParams.ClassStyle |= 2048;
						if (this._parentControl != null && !this._parentControl.IsDisposed)
						{
							createParams.Parent = this._parentControl.Handle;
						}
						return createParams;
					}
				}

				// Token: 0x0600343D RID: 13373 RVA: 0x0011C951 File Offset: 0x0011AB51
				public virtual void FocusComponent()
				{
					if (this._hostedControl != null && base.Visible)
					{
						this._hostedControl.Focus();
					}
				}

				// Token: 0x0600343E RID: 13374 RVA: 0x0011C96F File Offset: 0x0011AB6F
				public void DoModalLoop()
				{
					while (base.Visible)
					{
						Application.DoEvents();
						DesignerActionPanel.EditorPropertyLine.UnsafeNativeMethods.MsgWaitForMultipleObjectsEx(0, IntPtr.Zero, 250, 255, 4);
					}
				}

				// Token: 0x0600343F RID: 13375 RVA: 0x0011C998 File Offset: 0x0011AB98
				private bool OwnsWindow(IntPtr hWnd)
				{
					while (hWnd != IntPtr.Zero)
					{
						hWnd = DesignerActionPanel.EditorPropertyLine.UnsafeNativeMethods.GetWindowLong(new HandleRef(null, hWnd), -8);
						if (hWnd == IntPtr.Zero)
						{
							return false;
						}
						if (hWnd == base.Handle)
						{
							return true;
						}
					}
					return false;
				}

				// Token: 0x06003440 RID: 13376 RVA: 0x0011C9E4 File Offset: 0x0011ABE4
				protected override bool ProcessDialogKey(Keys keyData)
				{
					if (keyData == (Keys.Back | Keys.Space | Keys.Alt) || keyData == (Keys.RButton | Keys.MButton | Keys.Space | Keys.Alt) || keyData == Keys.F4)
					{
						base.Visible = false;
						return true;
					}
					return base.ProcessDialogKey(keyData);
				}

				// Token: 0x06003441 RID: 13377 RVA: 0x0011CA0C File Offset: 0x0011AC0C
				public void ShowDropDown(Control parent)
				{
					try
					{
						DesignerActionPanel.EditorPropertyLine.UnsafeNativeMethods.SetWindowLong(new HandleRef(this, base.Handle), -8, new HandleRef(parent, parent.Handle));
						IntPtr capture = DesignerActionPanel.EditorPropertyLine.UnsafeNativeMethods.GetCapture();
						if (capture != IntPtr.Zero)
						{
							DesignerActionPanel.EditorPropertyLine.UnsafeNativeMethods.SendMessage(new HandleRef(null, capture), 31, 0, 0);
							DesignerActionPanel.EditorPropertyLine.SafeNativeMethods.ReleaseCapture();
						}
						base.Visible = true;
						this.FocusComponent();
						this.DoModalLoop();
					}
					finally
					{
						DesignerActionPanel.EditorPropertyLine.UnsafeNativeMethods.SetWindowLong(new HandleRef(this, base.Handle), -8, new HandleRef(null, IntPtr.Zero));
						if (parent != null && parent.Visible)
						{
							parent.Focus();
						}
					}
				}

				// Token: 0x06003442 RID: 13378 RVA: 0x0011CABC File Offset: 0x0011ACBC
				protected override void WndProc(ref Message m)
				{
					if (m.Msg == 6 && base.Visible && DesignerActionPanel.EditorPropertyLine.NativeMethods.Util.LOWORD((int)((long)m.WParam)) == 0 && !this.OwnsWindow(m.LParam))
					{
						base.Visible = false;
						if (m.LParam == IntPtr.Zero)
						{
							Control topLevelControl = this._parentControl.TopLevelControl;
							ToolStripDropDown toolStripDropDown = topLevelControl as ToolStripDropDown;
							if (toolStripDropDown != null)
							{
								toolStripDropDown.Close();
								return;
							}
							if (topLevelControl != null)
							{
								topLevelControl.Visible = false;
							}
						}
						return;
					}
					base.WndProc(ref m);
				}

				// Token: 0x040022E5 RID: 8933
				private Control _hostedControl;

				// Token: 0x040022E6 RID: 8934
				private Control _parentControl;
			}

			// Token: 0x020005D3 RID: 1491
			private static class NativeMethods
			{
				// Token: 0x040022E7 RID: 8935
				public const int WM_ACTIVATE = 6;

				// Token: 0x040022E8 RID: 8936
				public const int WM_CANCELMODE = 31;

				// Token: 0x040022E9 RID: 8937
				public const int WM_MOUSEACTIVATE = 33;

				// Token: 0x040022EA RID: 8938
				public const int WM_NCLBUTTONDOWN = 161;

				// Token: 0x040022EB RID: 8939
				public const int WM_NCRBUTTONDOWN = 164;

				// Token: 0x040022EC RID: 8940
				public const int WM_NCMBUTTONDOWN = 167;

				// Token: 0x040022ED RID: 8941
				public const int WM_LBUTTONDOWN = 513;

				// Token: 0x040022EE RID: 8942
				public const int WM_RBUTTONDOWN = 516;

				// Token: 0x040022EF RID: 8943
				public const int WM_MBUTTONDOWN = 519;

				// Token: 0x040022F0 RID: 8944
				public const int WA_INACTIVE = 0;

				// Token: 0x040022F1 RID: 8945
				public const int WA_ACTIVE = 1;

				// Token: 0x040022F2 RID: 8946
				public const int WS_EX_TOOLWINDOW = 128;

				// Token: 0x040022F3 RID: 8947
				public const int WS_POPUP = -2147483648;

				// Token: 0x040022F4 RID: 8948
				public const int WS_BORDER = 8388608;

				// Token: 0x040022F5 RID: 8949
				public const int GWL_HWNDPARENT = -8;

				// Token: 0x040022F6 RID: 8950
				public const int QS_KEY = 1;

				// Token: 0x040022F7 RID: 8951
				public const int QS_MOUSEMOVE = 2;

				// Token: 0x040022F8 RID: 8952
				public const int QS_MOUSEBUTTON = 4;

				// Token: 0x040022F9 RID: 8953
				public const int QS_POSTMESSAGE = 8;

				// Token: 0x040022FA RID: 8954
				public const int QS_TIMER = 16;

				// Token: 0x040022FB RID: 8955
				public const int QS_PAINT = 32;

				// Token: 0x040022FC RID: 8956
				public const int QS_SENDMESSAGE = 64;

				// Token: 0x040022FD RID: 8957
				public const int QS_HOTKEY = 128;

				// Token: 0x040022FE RID: 8958
				public const int QS_ALLPOSTMESSAGE = 256;

				// Token: 0x040022FF RID: 8959
				public const int QS_MOUSE = 6;

				// Token: 0x04002300 RID: 8960
				public const int QS_INPUT = 7;

				// Token: 0x04002301 RID: 8961
				public const int QS_ALLEVENTS = 191;

				// Token: 0x04002302 RID: 8962
				public const int QS_ALLINPUT = 255;

				// Token: 0x04002303 RID: 8963
				public const int CS_SAVEBITS = 2048;

				// Token: 0x04002304 RID: 8964
				public const int MWMO_INPUTAVAILABLE = 4;

				// Token: 0x020005F7 RID: 1527
				internal static class Util
				{
					// Token: 0x06003500 RID: 13568 RVA: 0x00107568 File Offset: 0x00105768
					public static int LOWORD(int n)
					{
						return n & 65535;
					}
				}

				// Token: 0x020005F8 RID: 1528
				public static class CommonHandles
				{
					// Token: 0x04002353 RID: 9043
					public static HandleCollector GdiHandleCollector = new HandleCollector("GDI", 500);

					// Token: 0x04002354 RID: 9044
					public static HandleCollector HdcHandleCollector = new HandleCollector("HDC", 2);
				}

				// Token: 0x020005F9 RID: 1529
				[StructLayout(LayoutKind.Sequential)]
				public class SIZE
				{
					// Token: 0x04002355 RID: 9045
					public int cx;

					// Token: 0x04002356 RID: 9046
					public int cy;
				}

				// Token: 0x020005FA RID: 1530
				[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
				public struct TEXTMETRIC
				{
					// Token: 0x04002357 RID: 9047
					public int tmHeight;

					// Token: 0x04002358 RID: 9048
					public int tmAscent;

					// Token: 0x04002359 RID: 9049
					public int tmDescent;

					// Token: 0x0400235A RID: 9050
					public int tmInternalLeading;

					// Token: 0x0400235B RID: 9051
					public int tmExternalLeading;

					// Token: 0x0400235C RID: 9052
					public int tmAveCharWidth;

					// Token: 0x0400235D RID: 9053
					public int tmMaxCharWidth;

					// Token: 0x0400235E RID: 9054
					public int tmWeight;

					// Token: 0x0400235F RID: 9055
					public int tmOverhang;

					// Token: 0x04002360 RID: 9056
					public int tmDigitizedAspectX;

					// Token: 0x04002361 RID: 9057
					public int tmDigitizedAspectY;

					// Token: 0x04002362 RID: 9058
					public char tmFirstChar;

					// Token: 0x04002363 RID: 9059
					public char tmLastChar;

					// Token: 0x04002364 RID: 9060
					public char tmDefaultChar;

					// Token: 0x04002365 RID: 9061
					public char tmBreakChar;

					// Token: 0x04002366 RID: 9062
					public byte tmItalic;

					// Token: 0x04002367 RID: 9063
					public byte tmUnderlined;

					// Token: 0x04002368 RID: 9064
					public byte tmStruckOut;

					// Token: 0x04002369 RID: 9065
					public byte tmPitchAndFamily;

					// Token: 0x0400236A RID: 9066
					public byte tmCharSet;
				}

				// Token: 0x020005FB RID: 1531
				public struct TEXTMETRICA
				{
					// Token: 0x0400236B RID: 9067
					public int tmHeight;

					// Token: 0x0400236C RID: 9068
					public int tmAscent;

					// Token: 0x0400236D RID: 9069
					public int tmDescent;

					// Token: 0x0400236E RID: 9070
					public int tmInternalLeading;

					// Token: 0x0400236F RID: 9071
					public int tmExternalLeading;

					// Token: 0x04002370 RID: 9072
					public int tmAveCharWidth;

					// Token: 0x04002371 RID: 9073
					public int tmMaxCharWidth;

					// Token: 0x04002372 RID: 9074
					public int tmWeight;

					// Token: 0x04002373 RID: 9075
					public int tmOverhang;

					// Token: 0x04002374 RID: 9076
					public int tmDigitizedAspectX;

					// Token: 0x04002375 RID: 9077
					public int tmDigitizedAspectY;

					// Token: 0x04002376 RID: 9078
					public byte tmFirstChar;

					// Token: 0x04002377 RID: 9079
					public byte tmLastChar;

					// Token: 0x04002378 RID: 9080
					public byte tmDefaultChar;

					// Token: 0x04002379 RID: 9081
					public byte tmBreakChar;

					// Token: 0x0400237A RID: 9082
					public byte tmItalic;

					// Token: 0x0400237B RID: 9083
					public byte tmUnderlined;

					// Token: 0x0400237C RID: 9084
					public byte tmStruckOut;

					// Token: 0x0400237D RID: 9085
					public byte tmPitchAndFamily;

					// Token: 0x0400237E RID: 9086
					public byte tmCharSet;
				}
			}

			// Token: 0x020005D4 RID: 1492
			private static class SafeNativeMethods
			{
				// Token: 0x06003443 RID: 13379
				[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "DeleteObject", ExactSpelling = true, SetLastError = true)]
				private static extern bool IntDeleteObject(HandleRef hObject);

				// Token: 0x06003444 RID: 13380 RVA: 0x0011CB43 File Offset: 0x0011AD43
				public static bool DeleteObject(HandleRef hObject)
				{
					DesignerActionPanel.EditorPropertyLine.NativeMethods.CommonHandles.GdiHandleCollector.Remove();
					return DesignerActionPanel.EditorPropertyLine.SafeNativeMethods.IntDeleteObject(hObject);
				}

				// Token: 0x06003445 RID: 13381
				[DllImport("user32.dll", CharSet = CharSet.Auto)]
				public static extern bool ReleaseCapture();

				// Token: 0x06003446 RID: 13382
				[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
				public static extern IntPtr SelectObject(HandleRef hDC, HandleRef hObject);

				// Token: 0x06003447 RID: 13383
				[DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
				public static extern int GetTextExtentPoint32(HandleRef hDC, string str, int len, [In] [Out] DesignerActionPanel.EditorPropertyLine.NativeMethods.SIZE size);

				// Token: 0x06003448 RID: 13384
				[DllImport("gdi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
				public static extern int GetTextMetricsW(HandleRef hDC, [In] [Out] ref DesignerActionPanel.EditorPropertyLine.NativeMethods.TEXTMETRIC lptm);

				// Token: 0x06003449 RID: 13385
				[DllImport("gdi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
				public static extern int GetTextMetricsA(HandleRef hDC, [In] [Out] ref DesignerActionPanel.EditorPropertyLine.NativeMethods.TEXTMETRICA lptm);

				// Token: 0x0600344A RID: 13386 RVA: 0x0011CB58 File Offset: 0x0011AD58
				public static int GetTextMetrics(HandleRef hDC, ref DesignerActionPanel.EditorPropertyLine.NativeMethods.TEXTMETRIC lptm)
				{
					if (Marshal.SystemDefaultCharSize == 1)
					{
						DesignerActionPanel.EditorPropertyLine.NativeMethods.TEXTMETRICA textmetrica = default(DesignerActionPanel.EditorPropertyLine.NativeMethods.TEXTMETRICA);
						int textMetricsA = DesignerActionPanel.EditorPropertyLine.SafeNativeMethods.GetTextMetricsA(hDC, ref textmetrica);
						lptm.tmHeight = textmetrica.tmHeight;
						lptm.tmAscent = textmetrica.tmAscent;
						lptm.tmDescent = textmetrica.tmDescent;
						lptm.tmInternalLeading = textmetrica.tmInternalLeading;
						lptm.tmExternalLeading = textmetrica.tmExternalLeading;
						lptm.tmAveCharWidth = textmetrica.tmAveCharWidth;
						lptm.tmMaxCharWidth = textmetrica.tmMaxCharWidth;
						lptm.tmWeight = textmetrica.tmWeight;
						lptm.tmOverhang = textmetrica.tmOverhang;
						lptm.tmDigitizedAspectX = textmetrica.tmDigitizedAspectX;
						lptm.tmDigitizedAspectY = textmetrica.tmDigitizedAspectY;
						lptm.tmFirstChar = (char)textmetrica.tmFirstChar;
						lptm.tmLastChar = (char)textmetrica.tmLastChar;
						lptm.tmDefaultChar = (char)textmetrica.tmDefaultChar;
						lptm.tmBreakChar = (char)textmetrica.tmBreakChar;
						lptm.tmItalic = textmetrica.tmItalic;
						lptm.tmUnderlined = textmetrica.tmUnderlined;
						lptm.tmStruckOut = textmetrica.tmStruckOut;
						lptm.tmPitchAndFamily = textmetrica.tmPitchAndFamily;
						lptm.tmCharSet = textmetrica.tmCharSet;
						return textMetricsA;
					}
					return DesignerActionPanel.EditorPropertyLine.SafeNativeMethods.GetTextMetricsW(hDC, ref lptm);
				}
			}

			// Token: 0x020005D5 RID: 1493
			private static class UnsafeNativeMethods
			{
				// Token: 0x0600344B RID: 13387
				[DllImport("user32.dll", CharSet = CharSet.Auto)]
				public static extern IntPtr GetWindowLong(HandleRef hWnd, int nIndex);

				// Token: 0x0600344C RID: 13388
				[DllImport("user32.dll", CharSet = CharSet.Auto)]
				public static extern IntPtr SetWindowLong(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

				// Token: 0x0600344D RID: 13389
				[DllImport("user32.dll", CharSet = CharSet.Auto)]
				public static extern int MsgWaitForMultipleObjectsEx(int nCount, IntPtr pHandles, int dwMilliseconds, int dwWakeMask, int dwFlags);

				// Token: 0x0600344E RID: 13390
				[DllImport("user32.dll", CharSet = CharSet.Auto)]
				public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

				// Token: 0x0600344F RID: 13391
				[DllImport("user32.dll", CharSet = CharSet.Auto)]
				public static extern IntPtr GetCapture();

				// Token: 0x06003450 RID: 13392
				[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetDC", ExactSpelling = true)]
				private static extern IntPtr IntGetDC(HandleRef hWnd);

				// Token: 0x06003451 RID: 13393 RVA: 0x0011CC7A File Offset: 0x0011AE7A
				public static IntPtr GetDC(HandleRef hWnd)
				{
					DesignerActionPanel.EditorPropertyLine.NativeMethods.CommonHandles.HdcHandleCollector.Add();
					return DesignerActionPanel.EditorPropertyLine.UnsafeNativeMethods.IntGetDC(hWnd);
				}

				// Token: 0x06003452 RID: 13394
				[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "ReleaseDC", ExactSpelling = true)]
				private static extern int IntReleaseDC(HandleRef hWnd, HandleRef hDC);

				// Token: 0x06003453 RID: 13395 RVA: 0x0011CC8C File Offset: 0x0011AE8C
				public static int ReleaseDC(HandleRef hWnd, HandleRef hDC)
				{
					DesignerActionPanel.EditorPropertyLine.NativeMethods.CommonHandles.HdcHandleCollector.Remove();
					return DesignerActionPanel.EditorPropertyLine.UnsafeNativeMethods.IntReleaseDC(hWnd, hDC);
				}
			}

			// Token: 0x020005D6 RID: 1494
			internal sealed class EditorButton : Button
			{
				// Token: 0x06003454 RID: 13396 RVA: 0x0011CC9F File Offset: 0x0011AE9F
				protected override void OnMouseDown(MouseEventArgs e)
				{
					base.OnMouseDown(e);
					if (e.Button == MouseButtons.Left)
					{
						this._mouseDown = true;
					}
				}

				// Token: 0x06003455 RID: 13397 RVA: 0x0011CCBC File Offset: 0x0011AEBC
				protected override void OnMouseEnter(EventArgs e)
				{
					base.OnMouseEnter(e);
					this._mouseOver = true;
				}

				// Token: 0x06003456 RID: 13398 RVA: 0x0011CCCC File Offset: 0x0011AECC
				protected override void OnMouseLeave(EventArgs e)
				{
					base.OnMouseLeave(e);
					this._mouseOver = false;
				}

				// Token: 0x06003457 RID: 13399 RVA: 0x0011CCDC File Offset: 0x0011AEDC
				protected override void OnMouseUp(MouseEventArgs e)
				{
					base.OnMouseUp(e);
					if (e.Button == MouseButtons.Left)
					{
						this._mouseDown = false;
					}
				}

				// Token: 0x17000A21 RID: 2593
				// (get) Token: 0x06003458 RID: 13400 RVA: 0x0011CCF9 File Offset: 0x0011AEF9
				// (set) Token: 0x06003459 RID: 13401 RVA: 0x0011CD01 File Offset: 0x0011AF01
				public bool Ellipsis
				{
					get
					{
						return this._ellipsis;
					}
					set
					{
						this._ellipsis = value;
					}
				}

				// Token: 0x0600345A RID: 13402 RVA: 0x0011CD0C File Offset: 0x0011AF0C
				protected override void OnPaint(PaintEventArgs e)
				{
					Graphics graphics = e.Graphics;
					if (this._ellipsis)
					{
						PushButtonState state = PushButtonState.Normal;
						if (this._mouseDown)
						{
							state = PushButtonState.Pressed;
						}
						else if (this._mouseOver)
						{
							state = PushButtonState.Hot;
						}
						ButtonRenderer.DrawButton(graphics, new Rectangle(-1, -1, base.Width + 2, base.Height + 2), "…", this.Font, this.Focused, state);
						return;
					}
					if (ComboBoxRenderer.IsSupported)
					{
						ComboBoxState state2 = ComboBoxState.Normal;
						if (base.Enabled)
						{
							if (this._mouseDown)
							{
								state2 = ComboBoxState.Pressed;
							}
							else if (this._mouseOver)
							{
								state2 = ComboBoxState.Hot;
							}
						}
						else
						{
							state2 = ComboBoxState.Disabled;
						}
						ComboBoxRenderer.DrawDropDownButton(graphics, new Rectangle(0, 0, base.Width, base.Height), state2);
					}
					else
					{
						PushButtonState state3 = PushButtonState.Normal;
						if (base.Enabled)
						{
							if (this._mouseDown)
							{
								state3 = PushButtonState.Pressed;
							}
							else if (this._mouseOver)
							{
								state3 = PushButtonState.Hot;
							}
						}
						else
						{
							state3 = PushButtonState.Disabled;
						}
						ButtonRenderer.DrawButton(graphics, new Rectangle(-1, -1, base.Width + 2, base.Height + 2), string.Empty, this.Font, this.Focused, state3);
						try
						{
							using (Icon icon = new Icon(typeof(DesignerActionPanel), "Arrow.ico"))
							{
								Bitmap bitmap = icon.ToBitmap();
								using (ImageAttributes imageAttributes = new ImageAttributes())
								{
									imageAttributes.SetRemapTable(new ColorMap[]
									{
										new ColorMap
										{
											OldColor = Color.Black,
											NewColor = SystemColors.WindowText
										}
									}, ColorAdjustType.Bitmap);
									int width = bitmap.Width;
									int height = bitmap.Height;
									graphics.DrawImage(bitmap, new Rectangle((base.Width - width + 1) / 2, (base.Height - height + 1) / 2, width, height), 0, 0, width, width, GraphicsUnit.Pixel, imageAttributes, null, IntPtr.Zero);
								}
							}
						}
						catch
						{
						}
					}
					if (this.Focused)
					{
						ControlPaint.DrawFocusRectangle(graphics, new Rectangle(2, 2, base.Width - 5, base.Height - 5));
					}
				}

				// Token: 0x0600345B RID: 13403 RVA: 0x0011CF20 File Offset: 0x0011B120
				public void ResetMouseStates()
				{
					this._mouseDown = false;
					this._mouseOver = false;
					base.Invalidate();
				}

				// Token: 0x04002305 RID: 8965
				private bool _mouseOver;

				// Token: 0x04002306 RID: 8966
				private bool _mouseDown;

				// Token: 0x04002307 RID: 8967
				private bool _ellipsis;
			}
		}

		// Token: 0x0200048E RID: 1166
		private class TextLine : DesignerActionPanel.Line
		{
			// Token: 0x06002B0D RID: 11021 RVA: 0x00102407 File Offset: 0x00100607
			public TextLine(IServiceProvider serviceProvider, DesignerActionPanel actionPanel) : base(serviceProvider, actionPanel)
			{
				actionPanel.FontChanged += this.OnParentControlFontChanged;
			}

			// Token: 0x17000918 RID: 2328
			// (get) Token: 0x06002B0E RID: 11022 RVA: 0x00003930 File Offset: 0x00001B30
			public sealed override string FocusId
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x06002B0F RID: 11023 RVA: 0x00102424 File Offset: 0x00100624
			protected override void AddControls(List<Control> controls)
			{
				this._label = new Label();
				this._label.BackColor = Color.Transparent;
				this._label.ForeColor = base.ActionPanel.LabelForeColor;
				this._label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
				this._label.UseMnemonic = false;
				controls.Add(this._label);
			}

			// Token: 0x06002B10 RID: 11024 RVA: 0x00003937 File Offset: 0x00001B37
			public sealed override void Focus()
			{
			}

			// Token: 0x06002B11 RID: 11025 RVA: 0x00102488 File Offset: 0x00100688
			public override Size LayoutControls(int top, int width, bool measureOnly)
			{
				Size preferredSize = this._label.GetPreferredSize(new Size(int.MaxValue, int.MaxValue));
				if (!measureOnly)
				{
					this._label.Location = new Point(5, top + 3);
					this._label.Size = preferredSize;
				}
				return preferredSize + new Size(9, 7);
			}

			// Token: 0x06002B12 RID: 11026 RVA: 0x001024E1 File Offset: 0x001006E1
			private void OnParentControlFontChanged(object sender, EventArgs e)
			{
				if (this._label != null && this._label.Font != null)
				{
					this._label.Font = this.GetFont();
				}
			}

			// Token: 0x06002B13 RID: 11027 RVA: 0x00102509 File Offset: 0x00100709
			protected virtual Font GetFont()
			{
				return base.ActionPanel.Font;
			}

			// Token: 0x06002B14 RID: 11028 RVA: 0x00102518 File Offset: 0x00100718
			internal override void UpdateActionItem(DesignerActionList actionList, DesignerActionItem actionItem, ToolTip toolTip, ref int currentTabIndex)
			{
				this._textItem = (DesignerActionTextItem)actionItem;
				this._label.Text = DesignerActionPanel.StripAmpersands(this._textItem.DisplayName);
				this._label.Font = this.GetFont();
				Control label = this._label;
				int num = currentTabIndex;
				currentTabIndex = num + 1;
				label.TabIndex = num;
				toolTip.SetToolTip(this._label, this._textItem.Description);
			}

			// Token: 0x04001E03 RID: 7683
			private Label _label;

			// Token: 0x04001E04 RID: 7684
			private DesignerActionTextItem _textItem;
		}

		// Token: 0x0200048F RID: 1167
		private sealed class HeaderLine : DesignerActionPanel.TextLine
		{
			// Token: 0x06002B15 RID: 11029 RVA: 0x0010258A File Offset: 0x0010078A
			public HeaderLine(IServiceProvider serviceProvider, DesignerActionPanel actionPanel) : base(serviceProvider, actionPanel)
			{
			}

			// Token: 0x06002B16 RID: 11030 RVA: 0x00102594 File Offset: 0x00100794
			protected override Font GetFont()
			{
				return new Font(base.ActionPanel.Font, FontStyle.Bold);
			}
		}

		// Token: 0x02000490 RID: 1168
		private sealed class SeparatorLine : DesignerActionPanel.Line
		{
			// Token: 0x06002B17 RID: 11031 RVA: 0x001025A7 File Offset: 0x001007A7
			public SeparatorLine(IServiceProvider serviceProvider, DesignerActionPanel actionPanel) : this(serviceProvider, actionPanel, false)
			{
			}

			// Token: 0x06002B18 RID: 11032 RVA: 0x001025B2 File Offset: 0x001007B2
			public SeparatorLine(IServiceProvider serviceProvider, DesignerActionPanel actionPanel, bool isSubSeparator) : base(serviceProvider, actionPanel)
			{
				this._isSubSeparator = isSubSeparator;
			}

			// Token: 0x17000919 RID: 2329
			// (get) Token: 0x06002B19 RID: 11033 RVA: 0x00003930 File Offset: 0x00001B30
			public sealed override string FocusId
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x06002B1A RID: 11034 RVA: 0x00003937 File Offset: 0x00001B37
			protected override void AddControls(List<Control> controls)
			{
			}

			// Token: 0x06002B1B RID: 11035 RVA: 0x00003937 File Offset: 0x00001B37
			public sealed override void Focus()
			{
			}

			// Token: 0x06002B1C RID: 11036 RVA: 0x001025C3 File Offset: 0x001007C3
			public override Size LayoutControls(int top, int width, bool measureOnly)
			{
				return new Size(150, 1);
			}

			// Token: 0x06002B1D RID: 11037 RVA: 0x001025D0 File Offset: 0x001007D0
			public override void PaintLine(Graphics g, int lineWidth, int lineHeight)
			{
				using (Pen pen = new Pen(base.ActionPanel.SeparatorColor))
				{
					g.DrawLine(pen, 3, 0, lineWidth - 4, 0);
				}
			}

			// Token: 0x06002B1E RID: 11038 RVA: 0x00003937 File Offset: 0x00001B37
			internal override void UpdateActionItem(DesignerActionList actionList, DesignerActionItem actionItem, ToolTip toolTip, ref int currentTabIndex)
			{
			}

			// Token: 0x04001E05 RID: 7685
			private bool _isSubSeparator;
		}
	}
}
