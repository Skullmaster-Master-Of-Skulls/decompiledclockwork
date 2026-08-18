using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	// Token: 0x020003E2 RID: 994
	public sealed class ToolStripManager
	{
		// Token: 0x06004395 RID: 17301 RVA: 0x0011D8DA File Offset: 0x0011BADA
		private static void InitalizeThread()
		{
			if (!ToolStripManager.initialized)
			{
				ToolStripManager.initialized = true;
				ToolStripManager.currentRendererType = ToolStripManager.ProfessionalRendererType;
			}
		}

		// Token: 0x06004396 RID: 17302 RVA: 0x00002843 File Offset: 0x00000A43
		private ToolStripManager()
		{
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x0011D8F4 File Offset: 0x0011BAF4
		static ToolStripManager()
		{
			SystemEvents.UserPreferenceChanging += ToolStripManager.OnUserPreferenceChanging;
		}

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x06004398 RID: 17304 RVA: 0x0011D954 File Offset: 0x0011BB54
		internal static Font DefaultFont
		{
			get
			{
				if (DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
				{
					int num = ToolStripManager.CurrentDpi;
					Font font = null;
					if (!ToolStripManager.defaultFontCache.TryGetValue(num, out font) || font == null)
					{
						Font font2 = SystemInformation.GetMenuFontForDpi(num);
						if (font2 != null)
						{
							if (font2.Unit != GraphicsUnit.Point)
							{
								font = ControlPaint.FontInPoints(font2);
								font2.Dispose();
							}
							else
							{
								font = font2;
							}
							ToolStripManager.defaultFontCache[num] = font;
						}
					}
					return font;
				}
				Font font3 = ToolStripManager.defaultFont;
				if (font3 == null)
				{
					object obj = ToolStripManager.internalSyncObject;
					lock (obj)
					{
						font3 = ToolStripManager.defaultFont;
						if (font3 == null)
						{
							Font font2 = SystemFonts.MenuFont;
							if (font2 == null)
							{
								font2 = Control.DefaultFont;
							}
							if (font2 != null)
							{
								if (font2.Unit != GraphicsUnit.Point)
								{
									ToolStripManager.defaultFont = ControlPaint.FontInPoints(font2);
									font3 = ToolStripManager.defaultFont;
									font2.Dispose();
								}
								else
								{
									ToolStripManager.defaultFont = font2;
									font3 = ToolStripManager.defaultFont;
								}
							}
							return font3;
						}
					}
					return font3;
				}
				return font3;
			}
		}

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06004399 RID: 17305 RVA: 0x0011DA44 File Offset: 0x0011BC44
		// (set) Token: 0x0600439A RID: 17306 RVA: 0x0011DA4B File Offset: 0x0011BC4B
		internal static int CurrentDpi
		{
			get
			{
				return ToolStripManager.currentDpi;
			}
			set
			{
				ToolStripManager.currentDpi = value;
			}
		}

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x0600439B RID: 17307 RVA: 0x0011DA53 File Offset: 0x0011BC53
		internal static ClientUtils.WeakRefCollection ToolStrips
		{
			get
			{
				if (ToolStripManager.toolStripWeakArrayList == null)
				{
					ToolStripManager.toolStripWeakArrayList = new ClientUtils.WeakRefCollection();
				}
				return ToolStripManager.toolStripWeakArrayList;
			}
		}

		// Token: 0x0600439C RID: 17308 RVA: 0x0011DA6C File Offset: 0x0011BC6C
		private static void AddEventHandler(int key, Delegate value)
		{
			object obj = ToolStripManager.internalSyncObject;
			lock (obj)
			{
				if (ToolStripManager.staticEventHandlers == null)
				{
					ToolStripManager.staticEventHandlers = new Delegate[1];
				}
				ToolStripManager.staticEventHandlers[key] = Delegate.Combine(ToolStripManager.staticEventHandlers[key], value);
			}
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x0011DACC File Offset: 0x0011BCCC
		[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
		public static ToolStrip FindToolStrip(string toolStripName)
		{
			ToolStrip result = null;
			for (int i = 0; i < ToolStripManager.ToolStrips.Count; i++)
			{
				if (ToolStripManager.ToolStrips[i] != null && string.Equals(((ToolStrip)ToolStripManager.ToolStrips[i]).Name, toolStripName, StringComparison.Ordinal))
				{
					result = (ToolStrip)ToolStripManager.ToolStrips[i];
					break;
				}
			}
			return result;
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x0011DB30 File Offset: 0x0011BD30
		[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
		internal static ToolStrip FindToolStrip(Form owningForm, string toolStripName)
		{
			ToolStrip toolStrip = null;
			for (int i = 0; i < ToolStripManager.ToolStrips.Count; i++)
			{
				if (ToolStripManager.ToolStrips[i] != null && string.Equals(((ToolStrip)ToolStripManager.ToolStrips[i]).Name, toolStripName, StringComparison.Ordinal))
				{
					toolStrip = (ToolStrip)ToolStripManager.ToolStrips[i];
					if (toolStrip.FindForm() == owningForm)
					{
						break;
					}
				}
			}
			return toolStrip;
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x0011DB9C File Offset: 0x0011BD9C
		private static bool CanChangeSelection(ToolStrip start, ToolStrip toolStrip)
		{
			if (toolStrip == null)
			{
				return false;
			}
			bool flag = !toolStrip.TabStop && toolStrip.Enabled && toolStrip.Visible && !toolStrip.IsDisposed && !toolStrip.Disposing && !toolStrip.IsDropDown && ToolStripManager.IsOnSameWindow(start, toolStrip);
			if (flag)
			{
				foreach (object obj in toolStrip.Items)
				{
					ToolStripItem toolStripItem = (ToolStripItem)obj;
					if (toolStripItem.CanSelect)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060043A0 RID: 17312 RVA: 0x0011DC44 File Offset: 0x0011BE44
		private static bool ChangeSelection(ToolStrip start, ToolStrip toolStrip)
		{
			if (toolStrip == null || start == null)
			{
				return false;
			}
			if (start == toolStrip)
			{
				return false;
			}
			if (ToolStripManager.ModalMenuFilter.InMenuMode)
			{
				if (ToolStripManager.ModalMenuFilter.GetActiveToolStrip() == start)
				{
					ToolStripManager.ModalMenuFilter.RemoveActiveToolStrip(start);
					start.NotifySelectionChange(null);
				}
				ToolStripManager.ModalMenuFilter.SetActiveToolStrip(toolStrip);
			}
			else
			{
				toolStrip.FocusInternal();
			}
			start.SnapFocusChange(toolStrip);
			toolStrip.SelectNextToolStripItem(null, toolStrip.RightToLeft != RightToLeft.Yes);
			return true;
		}

		// Token: 0x060043A1 RID: 17313 RVA: 0x0011DCA8 File Offset: 0x0011BEA8
		private static Delegate GetEventHandler(int key)
		{
			object obj = ToolStripManager.internalSyncObject;
			Delegate result;
			lock (obj)
			{
				if (ToolStripManager.staticEventHandlers == null)
				{
					result = null;
				}
				else
				{
					result = ToolStripManager.staticEventHandlers[key];
				}
			}
			return result;
		}

		// Token: 0x060043A2 RID: 17314 RVA: 0x0011DCF8 File Offset: 0x0011BEF8
		private static bool IsOnSameWindow(Control control1, Control control2)
		{
			return WindowsFormsUtils.GetRootHWnd(control1).Handle == WindowsFormsUtils.GetRootHWnd(control2).Handle;
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x0011DD26 File Offset: 0x0011BF26
		internal static bool IsThreadUsingToolStrips()
		{
			return ToolStripManager.toolStripWeakArrayList != null && ToolStripManager.toolStripWeakArrayList.Count > 0;
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x0011DD40 File Offset: 0x0011BF40
		private static void OnUserPreferenceChanging(object sender, UserPreferenceChangingEventArgs e)
		{
			if (e.Category == UserPreferenceCategory.Window)
			{
				if (DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
				{
					ToolStripManager.defaultFontCache.Clear();
					return;
				}
				object obj = ToolStripManager.internalSyncObject;
				lock (obj)
				{
					ToolStripManager.defaultFont = null;
				}
			}
		}

		// Token: 0x060043A5 RID: 17317 RVA: 0x0011DD9C File Offset: 0x0011BF9C
		internal static void NotifyMenuModeChange(bool invalidateText, bool activationChange)
		{
			bool flag = false;
			for (int i = 0; i < ToolStripManager.ToolStrips.Count; i++)
			{
				ToolStrip toolStrip = ToolStripManager.ToolStrips[i] as ToolStrip;
				if (toolStrip == null)
				{
					flag = true;
				}
				else
				{
					if (invalidateText)
					{
						toolStrip.InvalidateTextItems();
					}
					if (activationChange)
					{
						toolStrip.KeyboardActive = false;
					}
				}
			}
			if (flag)
			{
				ToolStripManager.PruneToolStripList();
			}
		}

		// Token: 0x060043A6 RID: 17318 RVA: 0x0011DDF4 File Offset: 0x0011BFF4
		internal static void PruneToolStripList()
		{
			if (ToolStripManager.toolStripWeakArrayList != null && ToolStripManager.toolStripWeakArrayList.Count > 0)
			{
				for (int i = ToolStripManager.toolStripWeakArrayList.Count - 1; i >= 0; i--)
				{
					if (ToolStripManager.toolStripWeakArrayList[i] == null)
					{
						ToolStripManager.toolStripWeakArrayList.RemoveAt(i);
					}
				}
			}
		}

		// Token: 0x060043A7 RID: 17319 RVA: 0x0011DE44 File Offset: 0x0011C044
		private static void RemoveEventHandler(int key, Delegate value)
		{
			object obj = ToolStripManager.internalSyncObject;
			lock (obj)
			{
				if (ToolStripManager.staticEventHandlers != null)
				{
					ToolStripManager.staticEventHandlers[key] = Delegate.Remove(ToolStripManager.staticEventHandlers[key], value);
				}
			}
		}

		// Token: 0x060043A8 RID: 17320 RVA: 0x0011DE98 File Offset: 0x0011C098
		internal static bool SelectNextToolStrip(ToolStrip start, bool forward)
		{
			if (start == null || start.ParentInternal == null)
			{
				return false;
			}
			ToolStrip toolStrip = null;
			ToolStrip toolStrip2 = null;
			int tabIndex = start.TabIndex;
			int num = ToolStripManager.ToolStrips.IndexOf(start);
			int count = ToolStripManager.ToolStrips.Count;
			for (int i = 0; i < count; i++)
			{
				num = (forward ? ((num + 1) % count) : ((num + count - 1) % count));
				ToolStrip toolStrip3 = ToolStripManager.ToolStrips[num] as ToolStrip;
				if (toolStrip3 != null && toolStrip3 != start)
				{
					int tabIndex2 = toolStrip3.TabIndex;
					if (forward)
					{
						if (tabIndex2 >= tabIndex && ToolStripManager.CanChangeSelection(start, toolStrip3))
						{
							if (toolStrip2 == null)
							{
								toolStrip2 = toolStrip3;
							}
							else if (toolStrip3.TabIndex < toolStrip2.TabIndex)
							{
								toolStrip2 = toolStrip3;
							}
						}
						else if ((toolStrip == null || toolStrip3.TabIndex < toolStrip.TabIndex) && ToolStripManager.CanChangeSelection(start, toolStrip3))
						{
							toolStrip = toolStrip3;
						}
					}
					else if (tabIndex2 <= tabIndex && ToolStripManager.CanChangeSelection(start, toolStrip3))
					{
						if (toolStrip2 == null)
						{
							toolStrip2 = toolStrip3;
						}
						else if (toolStrip3.TabIndex > toolStrip2.TabIndex)
						{
							toolStrip2 = toolStrip3;
						}
					}
					else if ((toolStrip == null || toolStrip3.TabIndex > toolStrip.TabIndex) && ToolStripManager.CanChangeSelection(start, toolStrip3))
					{
						toolStrip = toolStrip3;
					}
					if (toolStrip2 != null && Math.Abs(toolStrip2.TabIndex - tabIndex) <= 1)
					{
						break;
					}
				}
			}
			if (toolStrip2 != null)
			{
				return ToolStripManager.ChangeSelection(start, toolStrip2);
			}
			return toolStrip != null && ToolStripManager.ChangeSelection(start, toolStrip);
		}

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x060043A9 RID: 17321 RVA: 0x0011DFEE File Offset: 0x0011C1EE
		// (set) Token: 0x060043AA RID: 17322 RVA: 0x0011DFFA File Offset: 0x0011C1FA
		private static Type CurrentRendererType
		{
			get
			{
				ToolStripManager.InitalizeThread();
				return ToolStripManager.currentRendererType;
			}
			set
			{
				ToolStripManager.currentRendererType = value;
			}
		}

		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x060043AB RID: 17323 RVA: 0x0011E002 File Offset: 0x0011C202
		private static Type DefaultRendererType
		{
			get
			{
				return ToolStripManager.ProfessionalRendererType;
			}
		}

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x060043AC RID: 17324 RVA: 0x0011E009 File Offset: 0x0011C209
		// (set) Token: 0x060043AD RID: 17325 RVA: 0x0011E028 File Offset: 0x0011C228
		public static ToolStripRenderer Renderer
		{
			get
			{
				if (ToolStripManager.defaultRenderer == null)
				{
					ToolStripManager.defaultRenderer = ToolStripManager.CreateRenderer(ToolStripManager.RenderMode);
				}
				return ToolStripManager.defaultRenderer;
			}
			[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
			set
			{
				if (ToolStripManager.defaultRenderer != value)
				{
					ToolStripManager.CurrentRendererType = ((value == null) ? ToolStripManager.DefaultRendererType : value.GetType());
					ToolStripManager.defaultRenderer = value;
					EventHandler eventHandler = (EventHandler)ToolStripManager.GetEventHandler(0);
					if (eventHandler != null)
					{
						eventHandler(null, EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x14000353 RID: 851
		// (add) Token: 0x060043AE RID: 17326 RVA: 0x0011E073 File Offset: 0x0011C273
		// (remove) Token: 0x060043AF RID: 17327 RVA: 0x0011E07C File Offset: 0x0011C27C
		public static event EventHandler RendererChanged
		{
			add
			{
				ToolStripManager.AddEventHandler(0, value);
			}
			remove
			{
				ToolStripManager.RemoveEventHandler(0, value);
			}
		}

		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x060043B0 RID: 17328 RVA: 0x0011E088 File Offset: 0x0011C288
		// (set) Token: 0x060043B1 RID: 17329 RVA: 0x0011E0D0 File Offset: 0x0011C2D0
		public static ToolStripManagerRenderMode RenderMode
		{
			get
			{
				Type left = ToolStripManager.CurrentRendererType;
				if (ToolStripManager.defaultRenderer != null && !ToolStripManager.defaultRenderer.IsAutoGenerated)
				{
					return ToolStripManagerRenderMode.Custom;
				}
				if (left == ToolStripManager.ProfessionalRendererType)
				{
					return ToolStripManagerRenderMode.Professional;
				}
				if (left == ToolStripManager.SystemRendererType)
				{
					return ToolStripManagerRenderMode.System;
				}
				return ToolStripManagerRenderMode.Custom;
			}
			[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolStripManagerRenderMode));
				}
				if (value == ToolStripManagerRenderMode.Custom)
				{
					throw new NotSupportedException(SR.GetString("ToolStripRenderModeUseRendererPropertyInstead"));
				}
				if (value - ToolStripManagerRenderMode.System <= 1)
				{
					ToolStripManager.Renderer = ToolStripManager.CreateRenderer(value);
					return;
				}
			}
		}

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x060043B2 RID: 17330 RVA: 0x0011E128 File Offset: 0x0011C328
		// (set) Token: 0x060043B3 RID: 17331 RVA: 0x0011E138 File Offset: 0x0011C338
		public static bool VisualStylesEnabled
		{
			get
			{
				return ToolStripManager.visualStylesEnabledIfPossible && Application.RenderWithVisualStyles;
			}
			[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
			set
			{
				bool visualStylesEnabled = ToolStripManager.VisualStylesEnabled;
				ToolStripManager.visualStylesEnabledIfPossible = value;
				if (visualStylesEnabled != ToolStripManager.VisualStylesEnabled)
				{
					EventHandler eventHandler = (EventHandler)ToolStripManager.GetEventHandler(0);
					if (eventHandler != null)
					{
						eventHandler(null, EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x0011E174 File Offset: 0x0011C374
		internal static ToolStripRenderer CreateRenderer(ToolStripManagerRenderMode renderMode)
		{
			switch (renderMode)
			{
			case ToolStripManagerRenderMode.System:
				return new ToolStripSystemRenderer(true);
			case ToolStripManagerRenderMode.Professional:
				return new ToolStripProfessionalRenderer(true);
			}
			return new ToolStripSystemRenderer(true);
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x0011E174 File Offset: 0x0011C374
		internal static ToolStripRenderer CreateRenderer(ToolStripRenderMode renderMode)
		{
			switch (renderMode)
			{
			case ToolStripRenderMode.System:
				return new ToolStripSystemRenderer(true);
			case ToolStripRenderMode.Professional:
				return new ToolStripProfessionalRenderer(true);
			}
			return new ToolStripSystemRenderer(true);
		}

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x060043B6 RID: 17334 RVA: 0x0011E19E File Offset: 0x0011C39E
		internal static ClientUtils.WeakRefCollection ToolStripPanels
		{
			get
			{
				if (ToolStripManager.toolStripPanelWeakArrayList == null)
				{
					ToolStripManager.toolStripPanelWeakArrayList = new ClientUtils.WeakRefCollection();
				}
				return ToolStripManager.toolStripPanelWeakArrayList;
			}
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x0011E1B8 File Offset: 0x0011C3B8
		internal static ToolStripPanel ToolStripPanelFromPoint(Control draggedControl, Point screenLocation)
		{
			if (ToolStripManager.toolStripPanelWeakArrayList != null)
			{
				ISupportToolStripPanel supportToolStripPanel = draggedControl as ISupportToolStripPanel;
				bool isCurrentlyDragging = supportToolStripPanel.IsCurrentlyDragging;
				for (int i = 0; i < ToolStripManager.toolStripPanelWeakArrayList.Count; i++)
				{
					ToolStripPanel toolStripPanel = ToolStripManager.toolStripPanelWeakArrayList[i] as ToolStripPanel;
					if (toolStripPanel != null && toolStripPanel.IsHandleCreated && toolStripPanel.Visible && toolStripPanel.DragBounds.Contains(toolStripPanel.PointToClient(screenLocation)))
					{
						if (!isCurrentlyDragging)
						{
							return toolStripPanel;
						}
						if (ToolStripManager.IsOnSameWindow(draggedControl, toolStripPanel))
						{
							return toolStripPanel;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x0011E23C File Offset: 0x0011C43C
		public static void LoadSettings(Form targetForm)
		{
			if (targetForm == null)
			{
				throw new ArgumentNullException("targetForm");
			}
			ToolStripManager.LoadSettings(targetForm, targetForm.GetType().FullName);
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x0011E260 File Offset: 0x0011C460
		public static void LoadSettings(Form targetForm, string key)
		{
			if (targetForm == null)
			{
				throw new ArgumentNullException("targetForm");
			}
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key");
			}
			ToolStripSettingsManager toolStripSettingsManager = new ToolStripSettingsManager(targetForm, key);
			toolStripSettingsManager.Load();
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x0011E29C File Offset: 0x0011C49C
		public static void SaveSettings(Form sourceForm)
		{
			if (sourceForm == null)
			{
				throw new ArgumentNullException("sourceForm");
			}
			ToolStripManager.SaveSettings(sourceForm, sourceForm.GetType().FullName);
		}

		// Token: 0x060043BB RID: 17339 RVA: 0x0011E2C0 File Offset: 0x0011C4C0
		public static void SaveSettings(Form sourceForm, string key)
		{
			if (sourceForm == null)
			{
				throw new ArgumentNullException("sourceForm");
			}
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key");
			}
			ToolStripSettingsManager toolStripSettingsManager = new ToolStripSettingsManager(sourceForm, key);
			toolStripSettingsManager.Save();
		}

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x060043BC RID: 17340 RVA: 0x0011E2FC File Offset: 0x0011C4FC
		internal static bool ShowMenuFocusCues
		{
			get
			{
				return DisplayInformation.MenuAccessKeysUnderlined || ToolStripManager.ModalMenuFilter.Instance.ShowUnderlines;
			}
		}

		// Token: 0x060043BD RID: 17341 RVA: 0x0011E314 File Offset: 0x0011C514
		public static bool IsValidShortcut(Keys shortcut)
		{
			Keys keys = shortcut & Keys.KeyCode;
			Keys keys2 = shortcut & Keys.Modifiers;
			return shortcut != Keys.None && (keys == Keys.Delete || keys == Keys.Insert || (keys >= Keys.F1 && keys <= Keys.F24) || (keys != Keys.None && keys2 != Keys.None && keys - Keys.ShiftKey > 2 && keys2 != Keys.Shift));
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x0011E370 File Offset: 0x0011C570
		internal static bool IsMenuKey(Keys keyData)
		{
			Keys keys = keyData & Keys.KeyCode;
			return Keys.Menu == keys || Keys.F10 == keys;
		}

		// Token: 0x060043BF RID: 17343 RVA: 0x0011E394 File Offset: 0x0011C594
		public static bool IsShortcutDefined(Keys shortcut)
		{
			for (int i = 0; i < ToolStripManager.ToolStrips.Count; i++)
			{
				ToolStrip toolStrip = ToolStripManager.ToolStrips[i] as ToolStrip;
				if (toolStrip != null && toolStrip.Shortcuts.Contains(shortcut))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060043C0 RID: 17344 RVA: 0x0011E3E0 File Offset: 0x0011C5E0
		internal static bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			if (ToolStripManager.IsValidShortcut(keyData))
			{
				return ToolStripManager.ProcessShortcut(ref m, keyData);
			}
			if (m.Msg == 260)
			{
				ToolStripManager.ModalMenuFilter.ProcessMenuKeyDown(ref m);
			}
			return false;
		}

		// Token: 0x060043C1 RID: 17345 RVA: 0x0011E408 File Offset: 0x0011C608
		internal static bool ProcessShortcut(ref Message m, Keys shortcut)
		{
			if (!ToolStripManager.IsThreadUsingToolStrips())
			{
				return false;
			}
			Control control = Control.FromChildHandleInternal(m.HWnd);
			Control control2 = control;
			if (control2 != null && ToolStripManager.IsValidShortcut(shortcut))
			{
				for (;;)
				{
					if (control2.ContextMenuStrip != null && control2.ContextMenuStrip.Shortcuts.ContainsKey(shortcut))
					{
						ToolStripMenuItem toolStripMenuItem = control2.ContextMenuStrip.Shortcuts[shortcut] as ToolStripMenuItem;
						if (toolStripMenuItem.ProcessCmdKey(ref m, shortcut))
						{
							break;
						}
					}
					control2 = control2.ParentInternal;
					if (control2 == null)
					{
						goto Block_6;
					}
				}
				return true;
				Block_6:
				if (control2 != null)
				{
					control = control2;
				}
				bool result = false;
				bool flag = false;
				for (int i = 0; i < ToolStripManager.ToolStrips.Count; i++)
				{
					ToolStrip toolStrip = ToolStripManager.ToolStrips[i] as ToolStrip;
					bool flag2 = false;
					bool flag3 = false;
					if (toolStrip == null)
					{
						flag = true;
					}
					else if ((control == null || toolStrip != control.ContextMenuStrip) && toolStrip.Shortcuts.ContainsKey(shortcut))
					{
						if (toolStrip.IsDropDown)
						{
							ToolStripDropDown toolStripDropDown = toolStrip as ToolStripDropDown;
							ContextMenuStrip contextMenuStrip = toolStripDropDown.GetFirstDropDown() as ContextMenuStrip;
							if (contextMenuStrip != null)
							{
								flag3 = contextMenuStrip.IsAssignedToDropDownItem;
								if (!flag3)
								{
									if (contextMenuStrip != control.ContextMenuStrip)
									{
										goto IL_1D0;
									}
									flag2 = true;
								}
							}
						}
						bool flag4 = false;
						if (!flag2)
						{
							ToolStrip toplevelOwnerToolStrip = toolStrip.GetToplevelOwnerToolStrip();
							if (toplevelOwnerToolStrip != null && control != null)
							{
								HandleRef rootHWnd = WindowsFormsUtils.GetRootHWnd(toplevelOwnerToolStrip);
								HandleRef rootHWnd2 = WindowsFormsUtils.GetRootHWnd(control);
								flag4 = (rootHWnd.Handle == rootHWnd2.Handle);
								if (flag4)
								{
									Form form = Control.FromHandleInternal(rootHWnd2.Handle) as Form;
									if (form != null && form.IsMdiContainer)
									{
										Form form2 = toplevelOwnerToolStrip.FindFormInternal();
										if (form2 != form && form2 != null)
										{
											flag4 = (form2 == form.ActiveMdiChildInternal);
										}
									}
								}
							}
						}
						if (flag2 || flag4 || flag3)
						{
							ToolStripMenuItem toolStripMenuItem2 = toolStrip.Shortcuts[shortcut] as ToolStripMenuItem;
							if (toolStripMenuItem2 != null && toolStripMenuItem2.ProcessCmdKey(ref m, shortcut))
							{
								result = true;
								break;
							}
						}
					}
					IL_1D0:;
				}
				if (flag)
				{
					ToolStripManager.PruneToolStripList();
				}
				return result;
			}
			return false;
		}

		// Token: 0x060043C2 RID: 17346 RVA: 0x0011E608 File Offset: 0x0011C808
		internal static bool ProcessMenuKey(ref Message m)
		{
			if (!ToolStripManager.IsThreadUsingToolStrips())
			{
				return false;
			}
			Keys keys = (Keys)((int)m.LParam);
			Control control = Control.FromHandleInternal(m.HWnd);
			Control control2 = null;
			MenuStrip menuStrip = null;
			if (control != null)
			{
				control2 = control.TopLevelControlInternal;
				if (control2 != null)
				{
					IntPtr menu = UnsafeNativeMethods.GetMenu(new HandleRef(control2, control2.Handle));
					if (menu == IntPtr.Zero)
					{
						menuStrip = ToolStripManager.GetMainMenuStrip(control2);
					}
				}
			}
			if ((ushort)keys == 32)
			{
				ToolStripManager.ModalMenuFilter.MenuKeyToggle = false;
			}
			else if ((ushort)keys == 45)
			{
				Form form = control2 as Form;
				if (form != null && form.IsMdiChild && form.WindowState == FormWindowState.Maximized)
				{
					ToolStripManager.ModalMenuFilter.MenuKeyToggle = false;
				}
			}
			else
			{
				if (UnsafeNativeMethods.GetKeyState(16) < 0 && keys == Keys.None)
				{
					return ToolStripManager.ModalMenuFilter.InMenuMode;
				}
				if (menuStrip != null && !ToolStripManager.ModalMenuFilter.MenuKeyToggle)
				{
					HandleRef rootHWnd = WindowsFormsUtils.GetRootHWnd(menuStrip);
					IntPtr foregroundWindow = UnsafeNativeMethods.GetForegroundWindow();
					if (rootHWnd.Handle == foregroundWindow)
					{
						return menuStrip.OnMenuKey();
					}
				}
				else if (menuStrip != null)
				{
					ToolStripManager.ModalMenuFilter.MenuKeyToggle = false;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060043C3 RID: 17347 RVA: 0x0011E6FC File Offset: 0x0011C8FC
		internal static MenuStrip GetMainMenuStrip(Control control)
		{
			if (control == null)
			{
				return null;
			}
			Form form = control.FindFormInternal();
			if (form != null && form.MainMenuStrip != null)
			{
				return form.MainMenuStrip;
			}
			return ToolStripManager.GetFirstMenuStripRecursive(control.Controls);
		}

		// Token: 0x060043C4 RID: 17348 RVA: 0x0011E734 File Offset: 0x0011C934
		private static MenuStrip GetFirstMenuStripRecursive(Control.ControlCollection controlsToLookIn)
		{
			try
			{
				for (int i = 0; i < controlsToLookIn.Count; i++)
				{
					if (controlsToLookIn[i] != null && controlsToLookIn[i] is MenuStrip)
					{
						return controlsToLookIn[i] as MenuStrip;
					}
				}
				for (int j = 0; j < controlsToLookIn.Count; j++)
				{
					if (controlsToLookIn[j] != null && controlsToLookIn[j].Controls != null && controlsToLookIn[j].Controls.Count > 0)
					{
						MenuStrip firstMenuStripRecursive = ToolStripManager.GetFirstMenuStripRecursive(controlsToLookIn[j].Controls);
						if (firstMenuStripRecursive != null)
						{
							return firstMenuStripRecursive;
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsCriticalException(ex))
				{
					throw;
				}
			}
			return null;
		}

		// Token: 0x060043C5 RID: 17349 RVA: 0x0011E7F4 File Offset: 0x0011C9F4
		private static ToolStripItem FindMatch(ToolStripItem source, ToolStripItemCollection destinationItems)
		{
			ToolStripItem toolStripItem = null;
			if (source != null)
			{
				for (int i = 0; i < destinationItems.Count; i++)
				{
					ToolStripItem toolStripItem2 = destinationItems[i];
					if (WindowsFormsUtils.SafeCompareStrings(source.Text, toolStripItem2.Text, true))
					{
						toolStripItem = toolStripItem2;
						break;
					}
				}
				if (toolStripItem == null && source.MergeIndex > -1 && source.MergeIndex < destinationItems.Count)
				{
					toolStripItem = destinationItems[source.MergeIndex];
				}
			}
			return toolStripItem;
		}

		// Token: 0x060043C6 RID: 17350 RVA: 0x0011E860 File Offset: 0x0011CA60
		internal static ArrayList FindMergeableToolStrips(ContainerControl container)
		{
			ArrayList arrayList = new ArrayList();
			if (container != null)
			{
				for (int i = 0; i < ToolStripManager.ToolStrips.Count; i++)
				{
					ToolStrip toolStrip = (ToolStrip)ToolStripManager.ToolStrips[i];
					if (toolStrip != null && toolStrip.AllowMerge && container == toolStrip.FindFormInternal())
					{
						arrayList.Add(toolStrip);
					}
				}
			}
			arrayList.Sort(new ToolStripCustomIComparer());
			return arrayList;
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x0011E8C4 File Offset: 0x0011CAC4
		private static bool IsSpecialMDIStrip(ToolStrip toolStrip)
		{
			return toolStrip is MdiControlStrip || toolStrip is MdiWindowListStrip;
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x0011E8DC File Offset: 0x0011CADC
		public static bool Merge(ToolStrip sourceToolStrip, ToolStrip targetToolStrip)
		{
			if (sourceToolStrip == null)
			{
				throw new ArgumentNullException("sourceToolStrip");
			}
			if (targetToolStrip == null)
			{
				throw new ArgumentNullException("targetToolStrip");
			}
			if (targetToolStrip == sourceToolStrip)
			{
				throw new ArgumentException(SR.GetString("ToolStripMergeImpossibleIdentical"));
			}
			bool flag = ToolStripManager.IsSpecialMDIStrip(sourceToolStrip) || (sourceToolStrip.AllowMerge && targetToolStrip.AllowMerge && (sourceToolStrip.GetType().IsAssignableFrom(targetToolStrip.GetType()) || targetToolStrip.GetType().IsAssignableFrom(sourceToolStrip.GetType())));
			MergeHistory mergeHistory = null;
			if (flag)
			{
				mergeHistory = new MergeHistory(sourceToolStrip);
				int count = sourceToolStrip.Items.Count;
				if (count > 0)
				{
					sourceToolStrip.SuspendLayout();
					targetToolStrip.SuspendLayout();
					try
					{
						int num = count;
						int i = 0;
						int num2 = 0;
						while (i < count)
						{
							ToolStripItem source = sourceToolStrip.Items[num2];
							ToolStripManager.MergeRecursive(source, targetToolStrip.Items, mergeHistory.MergeHistoryItemsStack);
							int num3 = num - sourceToolStrip.Items.Count;
							num2 = ((num3 > 0) ? num2 : (num2 + 1));
							num = sourceToolStrip.Items.Count;
							i++;
						}
					}
					finally
					{
						sourceToolStrip.ResumeLayout();
						targetToolStrip.ResumeLayout();
					}
					if (mergeHistory.MergeHistoryItemsStack.Count > 0)
					{
						targetToolStrip.MergeHistoryStack.Push(mergeHistory);
					}
				}
			}
			bool result = false;
			if (mergeHistory != null && mergeHistory.MergeHistoryItemsStack.Count > 0)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060043C9 RID: 17353 RVA: 0x0011EA44 File Offset: 0x0011CC44
		private static void MergeRecursive(ToolStripItem source, ToolStripItemCollection destinationItems, Stack<MergeHistoryItem> history)
		{
			switch (source.MergeAction)
			{
			case MergeAction.Append:
			{
				MergeHistoryItem mergeHistoryItem = new MergeHistoryItem(MergeAction.Remove);
				mergeHistoryItem.PreviousIndexCollection = source.Owner.Items;
				mergeHistoryItem.PreviousIndex = mergeHistoryItem.PreviousIndexCollection.IndexOf(source);
				mergeHistoryItem.TargetItem = source;
				int index = destinationItems.Add(source);
				mergeHistoryItem.Index = index;
				mergeHistoryItem.IndexCollection = destinationItems;
				history.Push(mergeHistoryItem);
				break;
			}
			case MergeAction.Insert:
				if (source.MergeIndex > -1)
				{
					MergeHistoryItem mergeHistoryItem = new MergeHistoryItem(MergeAction.Remove);
					mergeHistoryItem.PreviousIndexCollection = source.Owner.Items;
					mergeHistoryItem.PreviousIndex = mergeHistoryItem.PreviousIndexCollection.IndexOf(source);
					mergeHistoryItem.TargetItem = source;
					int index2 = Math.Min(destinationItems.Count, source.MergeIndex);
					destinationItems.Insert(index2, source);
					mergeHistoryItem.IndexCollection = destinationItems;
					mergeHistoryItem.Index = index2;
					history.Push(mergeHistoryItem);
					return;
				}
				break;
			case MergeAction.Replace:
			case MergeAction.Remove:
			case MergeAction.MatchOnly:
			{
				ToolStripItem toolStripItem = ToolStripManager.FindMatch(source, destinationItems);
				if (toolStripItem != null)
				{
					MergeAction mergeAction = source.MergeAction;
					if (mergeAction - MergeAction.Replace > 1)
					{
						if (mergeAction != MergeAction.MatchOnly)
						{
							break;
						}
						ToolStripDropDownItem toolStripDropDownItem = toolStripItem as ToolStripDropDownItem;
						ToolStripDropDownItem toolStripDropDownItem2 = source as ToolStripDropDownItem;
						if (toolStripDropDownItem == null || toolStripDropDownItem2 == null || toolStripDropDownItem2.DropDownItems.Count == 0)
						{
							break;
						}
						int count = toolStripDropDownItem2.DropDownItems.Count;
						if (count <= 0)
						{
							break;
						}
						int num = count;
						toolStripDropDownItem2.DropDown.SuspendLayout();
						try
						{
							int i = 0;
							int num2 = 0;
							while (i < count)
							{
								ToolStripManager.MergeRecursive(toolStripDropDownItem2.DropDownItems[num2], toolStripDropDownItem.DropDownItems, history);
								int num3 = num - toolStripDropDownItem2.DropDownItems.Count;
								num2 = ((num3 > 0) ? num2 : (num2 + 1));
								num = toolStripDropDownItem2.DropDownItems.Count;
								i++;
							}
							break;
						}
						finally
						{
							toolStripDropDownItem2.DropDown.ResumeLayout();
						}
					}
					MergeHistoryItem mergeHistoryItem = new MergeHistoryItem(MergeAction.Insert);
					mergeHistoryItem.TargetItem = toolStripItem;
					int index3 = destinationItems.IndexOf(toolStripItem);
					destinationItems.RemoveAt(index3);
					mergeHistoryItem.Index = index3;
					mergeHistoryItem.IndexCollection = destinationItems;
					mergeHistoryItem.TargetItem = toolStripItem;
					history.Push(mergeHistoryItem);
					if (source.MergeAction == MergeAction.Replace)
					{
						mergeHistoryItem = new MergeHistoryItem(MergeAction.Remove);
						mergeHistoryItem.PreviousIndexCollection = source.Owner.Items;
						mergeHistoryItem.PreviousIndex = mergeHistoryItem.PreviousIndexCollection.IndexOf(source);
						mergeHistoryItem.TargetItem = source;
						destinationItems.Insert(index3, source);
						mergeHistoryItem.Index = index3;
						mergeHistoryItem.IndexCollection = destinationItems;
						history.Push(mergeHistoryItem);
						return;
					}
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x060043CA RID: 17354 RVA: 0x0011ECC8 File Offset: 0x0011CEC8
		public static bool Merge(ToolStrip sourceToolStrip, string targetName)
		{
			if (sourceToolStrip == null)
			{
				throw new ArgumentNullException("sourceToolStrip");
			}
			if (targetName == null)
			{
				throw new ArgumentNullException("targetName");
			}
			ToolStrip toolStrip = ToolStripManager.FindToolStrip(targetName);
			return toolStrip != null && ToolStripManager.Merge(sourceToolStrip, toolStrip);
		}

		// Token: 0x060043CB RID: 17355 RVA: 0x0011ED04 File Offset: 0x0011CF04
		internal static bool RevertMergeInternal(ToolStrip targetToolStrip, ToolStrip sourceToolStrip, bool revertMDIControls)
		{
			bool result = false;
			if (targetToolStrip == null)
			{
				throw new ArgumentNullException("targetToolStrip");
			}
			if (targetToolStrip == sourceToolStrip)
			{
				throw new ArgumentException(SR.GetString("ToolStripMergeImpossibleIdentical"));
			}
			bool flag = false;
			if (sourceToolStrip != null)
			{
				foreach (MergeHistory mergeHistory in targetToolStrip.MergeHistoryStack)
				{
					flag = (mergeHistory.MergedToolStrip == sourceToolStrip);
					if (flag)
					{
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			if (sourceToolStrip != null)
			{
				sourceToolStrip.SuspendLayout();
			}
			targetToolStrip.SuspendLayout();
			try
			{
				Stack<ToolStrip> stack = new Stack<ToolStrip>();
				flag = false;
				while (targetToolStrip.MergeHistoryStack.Count > 0)
				{
					if (flag)
					{
						break;
					}
					result = true;
					MergeHistory mergeHistory2 = targetToolStrip.MergeHistoryStack.Pop();
					if (mergeHistory2.MergedToolStrip == sourceToolStrip)
					{
						flag = true;
					}
					else if (!revertMDIControls && sourceToolStrip == null)
					{
						if (ToolStripManager.IsSpecialMDIStrip(mergeHistory2.MergedToolStrip))
						{
							stack.Push(mergeHistory2.MergedToolStrip);
						}
					}
					else
					{
						stack.Push(mergeHistory2.MergedToolStrip);
					}
					while (mergeHistory2.MergeHistoryItemsStack.Count > 0)
					{
						MergeHistoryItem mergeHistoryItem = mergeHistory2.MergeHistoryItemsStack.Pop();
						MergeAction mergeAction = mergeHistoryItem.MergeAction;
						if (mergeAction != MergeAction.Insert)
						{
							if (mergeAction == MergeAction.Remove)
							{
								mergeHistoryItem.IndexCollection.Remove(mergeHistoryItem.TargetItem);
								mergeHistoryItem.PreviousIndexCollection.Insert(Math.Min(mergeHistoryItem.PreviousIndex, mergeHistoryItem.PreviousIndexCollection.Count), mergeHistoryItem.TargetItem);
							}
						}
						else
						{
							mergeHistoryItem.IndexCollection.Insert(Math.Min(mergeHistoryItem.Index, mergeHistoryItem.IndexCollection.Count), mergeHistoryItem.TargetItem);
						}
					}
				}
				while (stack.Count > 0)
				{
					ToolStrip sourceToolStrip2 = stack.Pop();
					ToolStripManager.Merge(sourceToolStrip2, targetToolStrip);
				}
			}
			finally
			{
				if (sourceToolStrip != null)
				{
					sourceToolStrip.ResumeLayout();
				}
				targetToolStrip.ResumeLayout();
			}
			return result;
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x0011EF10 File Offset: 0x0011D110
		public static bool RevertMerge(ToolStrip targetToolStrip)
		{
			return ToolStripManager.RevertMergeInternal(targetToolStrip, null, false);
		}

		// Token: 0x060043CD RID: 17357 RVA: 0x0011EF1A File Offset: 0x0011D11A
		public static bool RevertMerge(ToolStrip targetToolStrip, ToolStrip sourceToolStrip)
		{
			if (sourceToolStrip == null)
			{
				throw new ArgumentNullException("sourceToolStrip");
			}
			return ToolStripManager.RevertMergeInternal(targetToolStrip, sourceToolStrip, false);
		}

		// Token: 0x060043CE RID: 17358 RVA: 0x0011EF34 File Offset: 0x0011D134
		public static bool RevertMerge(string targetName)
		{
			ToolStrip toolStrip = ToolStripManager.FindToolStrip(targetName);
			return toolStrip != null && ToolStripManager.RevertMerge(toolStrip);
		}

		// Token: 0x040025E7 RID: 9703
		[ThreadStatic]
		private static ClientUtils.WeakRefCollection toolStripWeakArrayList;

		// Token: 0x040025E8 RID: 9704
		[ThreadStatic]
		private static ClientUtils.WeakRefCollection toolStripPanelWeakArrayList;

		// Token: 0x040025E9 RID: 9705
		[ThreadStatic]
		private static bool initialized;

		// Token: 0x040025EA RID: 9706
		private static Font defaultFont;

		// Token: 0x040025EB RID: 9707
		private static ConcurrentDictionary<int, Font> defaultFontCache = new ConcurrentDictionary<int, Font>();

		// Token: 0x040025EC RID: 9708
		[ThreadStatic]
		private static Delegate[] staticEventHandlers;

		// Token: 0x040025ED RID: 9709
		private const int staticEventDefaultRendererChanged = 0;

		// Token: 0x040025EE RID: 9710
		private const int staticEventCount = 1;

		// Token: 0x040025EF RID: 9711
		private static object internalSyncObject = new object();

		// Token: 0x040025F0 RID: 9712
		private static int currentDpi = DpiHelper.DeviceDpi;

		// Token: 0x040025F1 RID: 9713
		[ThreadStatic]
		private static ToolStripRenderer defaultRenderer;

		// Token: 0x040025F2 RID: 9714
		internal static Type SystemRendererType = typeof(ToolStripSystemRenderer);

		// Token: 0x040025F3 RID: 9715
		internal static Type ProfessionalRendererType = typeof(ToolStripProfessionalRenderer);

		// Token: 0x040025F4 RID: 9716
		private static bool visualStylesEnabledIfPossible = true;

		// Token: 0x040025F5 RID: 9717
		[ThreadStatic]
		private static Type currentRendererType;

		// Token: 0x0200080A RID: 2058
		internal class ModalMenuFilter : IMessageModifyAndFilter, IMessageFilter
		{
			// Token: 0x1700184B RID: 6219
			// (get) Token: 0x06006F20 RID: 28448 RVA: 0x001979D4 File Offset: 0x00195BD4
			internal static ToolStripManager.ModalMenuFilter Instance
			{
				get
				{
					if (ToolStripManager.ModalMenuFilter._instance == null)
					{
						ToolStripManager.ModalMenuFilter._instance = new ToolStripManager.ModalMenuFilter();
					}
					return ToolStripManager.ModalMenuFilter._instance;
				}
			}

			// Token: 0x06006F21 RID: 28449 RVA: 0x001979EC File Offset: 0x00195BEC
			private ModalMenuFilter()
			{
			}

			// Token: 0x1700184C RID: 6220
			// (get) Token: 0x06006F22 RID: 28450 RVA: 0x00197A16 File Offset: 0x00195C16
			internal static HandleRef ActiveHwnd
			{
				get
				{
					return ToolStripManager.ModalMenuFilter.Instance.ActiveHwndInternal;
				}
			}

			// Token: 0x1700184D RID: 6221
			// (get) Token: 0x06006F23 RID: 28451 RVA: 0x00197A22 File Offset: 0x00195C22
			// (set) Token: 0x06006F24 RID: 28452 RVA: 0x00197A2A File Offset: 0x00195C2A
			public bool ShowUnderlines
			{
				get
				{
					return this._showUnderlines;
				}
				set
				{
					if (this._showUnderlines != value)
					{
						this._showUnderlines = value;
						ToolStripManager.NotifyMenuModeChange(true, false);
					}
				}
			}

			// Token: 0x1700184E RID: 6222
			// (get) Token: 0x06006F25 RID: 28453 RVA: 0x00197A43 File Offset: 0x00195C43
			// (set) Token: 0x06006F26 RID: 28454 RVA: 0x00197A4C File Offset: 0x00195C4C
			private HandleRef ActiveHwndInternal
			{
				get
				{
					return this._activeHwnd;
				}
				set
				{
					if (this._activeHwnd.Handle != value.Handle)
					{
						Control control;
						if (this._activeHwnd.Handle != IntPtr.Zero)
						{
							control = Control.FromHandleInternal(this._activeHwnd.Handle);
							if (control != null)
							{
								control.HandleCreated -= this.OnActiveHwndHandleCreated;
							}
						}
						this._activeHwnd = value;
						control = Control.FromHandleInternal(this._activeHwnd.Handle);
						if (control != null)
						{
							control.HandleCreated += this.OnActiveHwndHandleCreated;
						}
					}
				}
			}

			// Token: 0x1700184F RID: 6223
			// (get) Token: 0x06006F27 RID: 28455 RVA: 0x00197ADE File Offset: 0x00195CDE
			internal static bool InMenuMode
			{
				get
				{
					return ToolStripManager.ModalMenuFilter.Instance._inMenuMode;
				}
			}

			// Token: 0x17001850 RID: 6224
			// (get) Token: 0x06006F28 RID: 28456 RVA: 0x00197AEA File Offset: 0x00195CEA
			// (set) Token: 0x06006F29 RID: 28457 RVA: 0x00197AF6 File Offset: 0x00195CF6
			internal static bool MenuKeyToggle
			{
				get
				{
					return ToolStripManager.ModalMenuFilter.Instance.menuKeyToggle;
				}
				set
				{
					if (ToolStripManager.ModalMenuFilter.Instance.menuKeyToggle != value)
					{
						ToolStripManager.ModalMenuFilter.Instance.menuKeyToggle = value;
					}
				}
			}

			// Token: 0x17001851 RID: 6225
			// (get) Token: 0x06006F2A RID: 28458 RVA: 0x00197B10 File Offset: 0x00195D10
			private ToolStripManager.ModalMenuFilter.HostedWindowsFormsMessageHook MessageHook
			{
				get
				{
					if (this.messageHook == null)
					{
						this.messageHook = new ToolStripManager.ModalMenuFilter.HostedWindowsFormsMessageHook();
					}
					return this.messageHook;
				}
			}

			// Token: 0x06006F2B RID: 28459 RVA: 0x00197B2C File Offset: 0x00195D2C
			private void EnterMenuModeCore()
			{
				if (!ToolStripManager.ModalMenuFilter.InMenuMode)
				{
					IntPtr activeWindow = UnsafeNativeMethods.GetActiveWindow();
					if (activeWindow != IntPtr.Zero)
					{
						this.ActiveHwndInternal = new HandleRef(this, activeWindow);
					}
					Application.ThreadContext.FromCurrent().AddMessageFilter(this);
					Application.ThreadContext.FromCurrent().TrackInput(true);
					if (!Application.ThreadContext.FromCurrent().GetMessageLoop(true))
					{
						this.MessageHook.HookMessages = true;
					}
					this._inMenuMode = true;
					if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
					{
						this.NotifyLastLastFocusedToolAboutFocusLoss();
					}
					this.ProcessMessages(true);
				}
			}

			// Token: 0x06006F2C RID: 28460 RVA: 0x00197BAC File Offset: 0x00195DAC
			internal void NotifyLastLastFocusedToolAboutFocusLoss()
			{
				IKeyboardToolTip keyboardToolTip = KeyboardToolTipStateMachine.Instance.LastFocusedTool;
				if (keyboardToolTip != null)
				{
					this.lastFocusedTool.SetTarget(keyboardToolTip);
					KeyboardToolTipStateMachine.Instance.NotifyAboutLostFocus(keyboardToolTip);
				}
			}

			// Token: 0x06006F2D RID: 28461 RVA: 0x00197BDE File Offset: 0x00195DDE
			internal static void ExitMenuMode()
			{
				ToolStripManager.ModalMenuFilter.Instance.ExitMenuModeCore();
			}

			// Token: 0x06006F2E RID: 28462 RVA: 0x00197BEC File Offset: 0x00195DEC
			private void ExitMenuModeCore()
			{
				this.ProcessMessages(false);
				if (ToolStripManager.ModalMenuFilter.InMenuMode)
				{
					try
					{
						if (this.messageHook != null)
						{
							this.messageHook.HookMessages = false;
						}
						Application.ThreadContext.FromCurrent().RemoveMessageFilter(this);
						Application.ThreadContext.FromCurrent().TrackInput(false);
						if (ToolStripManager.ModalMenuFilter.ActiveHwnd.Handle != IntPtr.Zero)
						{
							Control control = Control.FromHandleInternal(ToolStripManager.ModalMenuFilter.ActiveHwnd.Handle);
							if (control != null)
							{
								control.HandleCreated -= this.OnActiveHwndHandleCreated;
							}
							this.ActiveHwndInternal = NativeMethods.NullHandleRef;
						}
						if (this._inputFilterQueue != null)
						{
							this._inputFilterQueue.Clear();
						}
						if (this._caretHidden)
						{
							this._caretHidden = false;
							SafeNativeMethods.ShowCaret(NativeMethods.NullHandleRef);
						}
						IKeyboardToolTip keyboardToolTip;
						if (!AccessibilityImprovements.UseLegacyToolTipDisplay && this.lastFocusedTool.TryGetTarget(out keyboardToolTip) && keyboardToolTip != null)
						{
							KeyboardToolTipStateMachine.Instance.NotifyAboutGotFocus(keyboardToolTip);
						}
					}
					finally
					{
						this._inMenuMode = false;
						bool showUnderlines = this._showUnderlines;
						this._showUnderlines = false;
						ToolStripManager.NotifyMenuModeChange(showUnderlines, true);
					}
				}
			}

			// Token: 0x06006F2F RID: 28463 RVA: 0x00197D00 File Offset: 0x00195F00
			internal static ToolStrip GetActiveToolStrip()
			{
				return ToolStripManager.ModalMenuFilter.Instance.GetActiveToolStripInternal();
			}

			// Token: 0x06006F30 RID: 28464 RVA: 0x00197D0C File Offset: 0x00195F0C
			internal ToolStrip GetActiveToolStripInternal()
			{
				if (this._inputFilterQueue != null && this._inputFilterQueue.Count > 0)
				{
					return this._inputFilterQueue[this._inputFilterQueue.Count - 1];
				}
				return null;
			}

			// Token: 0x06006F31 RID: 28465 RVA: 0x00197D40 File Offset: 0x00195F40
			private ToolStrip GetCurrentToplevelToolStrip()
			{
				if (this._toplevelToolStrip == null)
				{
					ToolStrip activeToolStripInternal = this.GetActiveToolStripInternal();
					if (activeToolStripInternal != null)
					{
						this._toplevelToolStrip = activeToolStripInternal.GetToplevelOwnerToolStrip();
					}
				}
				return this._toplevelToolStrip;
			}

			// Token: 0x06006F32 RID: 28466 RVA: 0x00197D74 File Offset: 0x00195F74
			private void OnActiveHwndHandleCreated(object sender, EventArgs e)
			{
				Control control = sender as Control;
				this.ActiveHwndInternal = new HandleRef(this, control.Handle);
			}

			// Token: 0x06006F33 RID: 28467 RVA: 0x00197D9C File Offset: 0x00195F9C
			internal static void ProcessMenuKeyDown(ref Message m)
			{
				Keys keyData = (Keys)((int)m.WParam);
				ToolStrip toolStrip = Control.FromHandleInternal(m.HWnd) as ToolStrip;
				if (toolStrip != null && !toolStrip.IsDropDown)
				{
					return;
				}
				if (ToolStripManager.IsMenuKey(keyData))
				{
					if (!ToolStripManager.ModalMenuFilter.InMenuMode && ToolStripManager.ModalMenuFilter.MenuKeyToggle)
					{
						ToolStripManager.ModalMenuFilter.MenuKeyToggle = false;
						return;
					}
					if (!ToolStripManager.ModalMenuFilter.MenuKeyToggle)
					{
						ToolStripManager.ModalMenuFilter.Instance.ShowUnderlines = true;
					}
				}
			}

			// Token: 0x06006F34 RID: 28468 RVA: 0x00197E01 File Offset: 0x00196001
			internal static void CloseActiveDropDown(ToolStripDropDown activeToolStripDropDown, ToolStripDropDownCloseReason reason)
			{
				activeToolStripDropDown.SetCloseReason(reason);
				activeToolStripDropDown.Visible = false;
				if (ToolStripManager.ModalMenuFilter.GetActiveToolStrip() == null)
				{
					ToolStripManager.ModalMenuFilter.ExitMenuMode();
					if (activeToolStripDropDown.OwnerItem != null)
					{
						activeToolStripDropDown.OwnerItem.Unselect();
					}
				}
			}

			// Token: 0x06006F35 RID: 28469 RVA: 0x00197E30 File Offset: 0x00196030
			private void ProcessMessages(bool process)
			{
				if (process)
				{
					if (this._ensureMessageProcessingTimer == null)
					{
						this._ensureMessageProcessingTimer = new Timer();
					}
					this._ensureMessageProcessingTimer.Interval = 500;
					this._ensureMessageProcessingTimer.Enabled = true;
					return;
				}
				if (this._ensureMessageProcessingTimer != null)
				{
					this._ensureMessageProcessingTimer.Enabled = false;
					this._ensureMessageProcessingTimer.Dispose();
					this._ensureMessageProcessingTimer = null;
				}
			}

			// Token: 0x06006F36 RID: 28470 RVA: 0x00197E98 File Offset: 0x00196098
			private void ProcessMouseButtonPressed(IntPtr hwndMouseMessageIsFrom, int x, int y)
			{
				int count = this._inputFilterQueue.Count;
				for (int i = 0; i < count; i++)
				{
					ToolStrip activeToolStripInternal = this.GetActiveToolStripInternal();
					if (activeToolStripInternal == null)
					{
						break;
					}
					NativeMethods.POINT point = new NativeMethods.POINT();
					point.x = x;
					point.y = y;
					UnsafeNativeMethods.MapWindowPoints(new HandleRef(activeToolStripInternal, hwndMouseMessageIsFrom), new HandleRef(activeToolStripInternal, activeToolStripInternal.Handle), point, 1);
					if (activeToolStripInternal.ClientRectangle.Contains(point.x, point.y))
					{
						break;
					}
					ToolStripDropDown toolStripDropDown = activeToolStripInternal as ToolStripDropDown;
					if (toolStripDropDown != null)
					{
						if (toolStripDropDown.OwnerToolStrip == null || !(toolStripDropDown.OwnerToolStrip.Handle == hwndMouseMessageIsFrom) || toolStripDropDown.OwnerDropDownItem == null || !toolStripDropDown.OwnerDropDownItem.DropDownButtonArea.Contains(x, y))
						{
							ToolStripManager.ModalMenuFilter.CloseActiveDropDown(toolStripDropDown, ToolStripDropDownCloseReason.AppClicked);
						}
					}
					else
					{
						activeToolStripInternal.NotifySelectionChange(null);
						this.ExitMenuModeCore();
					}
				}
			}

			// Token: 0x06006F37 RID: 28471 RVA: 0x00197F80 File Offset: 0x00196180
			private bool ProcessActivationChange()
			{
				int count = this._inputFilterQueue.Count;
				for (int i = 0; i < count; i++)
				{
					ToolStripDropDown toolStripDropDown = this.GetActiveToolStripInternal() as ToolStripDropDown;
					if (toolStripDropDown != null && toolStripDropDown.AutoClose)
					{
						toolStripDropDown.Visible = false;
					}
				}
				this.ExitMenuModeCore();
				return true;
			}

			// Token: 0x06006F38 RID: 28472 RVA: 0x00197FCA File Offset: 0x001961CA
			internal static void SetActiveToolStrip(ToolStrip toolStrip, bool menuKeyPressed)
			{
				if (!ToolStripManager.ModalMenuFilter.InMenuMode && menuKeyPressed)
				{
					ToolStripManager.ModalMenuFilter.Instance.ShowUnderlines = true;
				}
				ToolStripManager.ModalMenuFilter.Instance.SetActiveToolStripCore(toolStrip);
			}

			// Token: 0x06006F39 RID: 28473 RVA: 0x00197FEE File Offset: 0x001961EE
			internal static void SetActiveToolStrip(ToolStrip toolStrip)
			{
				ToolStripManager.ModalMenuFilter.Instance.SetActiveToolStripCore(toolStrip);
			}

			// Token: 0x06006F3A RID: 28474 RVA: 0x00197FFC File Offset: 0x001961FC
			private void SetActiveToolStripCore(ToolStrip toolStrip)
			{
				if (toolStrip == null)
				{
					return;
				}
				if (toolStrip.IsDropDown)
				{
					ToolStripDropDown toolStripDropDown = toolStrip as ToolStripDropDown;
					if (!toolStripDropDown.AutoClose)
					{
						IntPtr activeWindow = UnsafeNativeMethods.GetActiveWindow();
						if (activeWindow != IntPtr.Zero)
						{
							this.ActiveHwndInternal = new HandleRef(this, activeWindow);
						}
						return;
					}
				}
				toolStrip.KeyboardActive = true;
				if (this._inputFilterQueue == null)
				{
					this._inputFilterQueue = new List<ToolStrip>();
				}
				else
				{
					ToolStrip activeToolStripInternal = this.GetActiveToolStripInternal();
					if (activeToolStripInternal != null)
					{
						if (!activeToolStripInternal.IsDropDown)
						{
							this._inputFilterQueue.Remove(activeToolStripInternal);
						}
						else if (toolStrip.IsDropDown && ToolStripDropDown.GetFirstDropDown(toolStrip) != ToolStripDropDown.GetFirstDropDown(activeToolStripInternal))
						{
							this._inputFilterQueue.Remove(activeToolStripInternal);
							ToolStripDropDown toolStripDropDown2 = activeToolStripInternal as ToolStripDropDown;
							toolStripDropDown2.DismissAll();
						}
					}
				}
				this._toplevelToolStrip = null;
				if (!this._inputFilterQueue.Contains(toolStrip))
				{
					this._inputFilterQueue.Add(toolStrip);
				}
				if (!ToolStripManager.ModalMenuFilter.InMenuMode && this._inputFilterQueue.Count > 0)
				{
					this.EnterMenuModeCore();
				}
				if (!this._caretHidden && toolStrip.IsDropDown && ToolStripManager.ModalMenuFilter.InMenuMode)
				{
					this._caretHidden = true;
					SafeNativeMethods.HideCaret(NativeMethods.NullHandleRef);
				}
			}

			// Token: 0x06006F3B RID: 28475 RVA: 0x00198117 File Offset: 0x00196317
			internal static void SuspendMenuMode()
			{
				ToolStripManager.ModalMenuFilter.Instance._suspendMenuMode = true;
			}

			// Token: 0x06006F3C RID: 28476 RVA: 0x00198124 File Offset: 0x00196324
			internal static void ResumeMenuMode()
			{
				ToolStripManager.ModalMenuFilter.Instance._suspendMenuMode = false;
			}

			// Token: 0x06006F3D RID: 28477 RVA: 0x00198131 File Offset: 0x00196331
			internal static void RemoveActiveToolStrip(ToolStrip toolStrip)
			{
				ToolStripManager.ModalMenuFilter.Instance.RemoveActiveToolStripCore(toolStrip);
			}

			// Token: 0x06006F3E RID: 28478 RVA: 0x0019813E File Offset: 0x0019633E
			private void RemoveActiveToolStripCore(ToolStrip toolStrip)
			{
				this._toplevelToolStrip = null;
				if (this._inputFilterQueue != null)
				{
					this._inputFilterQueue.Remove(toolStrip);
				}
			}

			// Token: 0x06006F3F RID: 28479 RVA: 0x0019815C File Offset: 0x0019635C
			private static bool IsChildOrSameWindow(HandleRef hwndParent, HandleRef hwndChild)
			{
				return hwndParent.Handle == hwndChild.Handle || UnsafeNativeMethods.IsChild(hwndParent, hwndChild);
			}

			// Token: 0x06006F40 RID: 28480 RVA: 0x00198184 File Offset: 0x00196384
			private static bool IsKeyOrMouseMessage(Message m)
			{
				bool result = false;
				if (m.Msg >= 512 && m.Msg <= 522)
				{
					result = true;
				}
				else if (m.Msg >= 161 && m.Msg <= 169)
				{
					result = true;
				}
				else if (m.Msg >= 256 && m.Msg <= 264)
				{
					result = true;
				}
				return result;
			}

			// Token: 0x06006F41 RID: 28481 RVA: 0x001981F4 File Offset: 0x001963F4
			public bool PreFilterMessage(ref Message m)
			{
				if (this._suspendMenuMode)
				{
					return false;
				}
				ToolStrip activeToolStrip = ToolStripManager.ModalMenuFilter.GetActiveToolStrip();
				if (activeToolStrip == null)
				{
					return false;
				}
				if (activeToolStrip.IsDisposed)
				{
					this.RemoveActiveToolStripCore(activeToolStrip);
					return false;
				}
				HandleRef handleRef = new HandleRef(activeToolStrip, activeToolStrip.Handle);
				HandleRef handleRef2 = new HandleRef(null, UnsafeNativeMethods.GetActiveWindow());
				if (handleRef2.Handle != this._lastActiveWindow.Handle)
				{
					if (handleRef2.Handle == IntPtr.Zero)
					{
						this.ProcessActivationChange();
					}
					else if (!(Control.FromChildHandleInternal(handleRef2.Handle) is ToolStripDropDown) && !ToolStripManager.ModalMenuFilter.IsChildOrSameWindow(handleRef2, handleRef) && !ToolStripManager.ModalMenuFilter.IsChildOrSameWindow(handleRef2, ToolStripManager.ModalMenuFilter.ActiveHwnd))
					{
						this.ProcessActivationChange();
					}
				}
				this._lastActiveWindow = handleRef2;
				if (!ToolStripManager.ModalMenuFilter.IsKeyOrMouseMessage(m))
				{
					return false;
				}
				DpiAwarenessContext awareness = CommonUnsafeNativeMethods.TryGetDpiAwarenessContextForWindow(m.HWnd);
				using (DpiHelper.EnterDpiAwarenessScope(awareness))
				{
					int msg = m.Msg;
					if (msg <= 167)
					{
						switch (msg)
						{
						case 160:
							goto IL_153;
						case 161:
						case 164:
							break;
						case 162:
						case 163:
							goto IL_23E;
						default:
							if (msg != 167)
							{
								goto IL_23E;
							}
							break;
						}
						this.ProcessMouseButtonPressed(IntPtr.Zero, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam));
						goto IL_23E;
					}
					if (msg - 256 > 7)
					{
						switch (msg)
						{
						case 512:
							goto IL_153;
						case 513:
						case 516:
							break;
						case 514:
						case 515:
							goto IL_23E;
						default:
							if (msg != 519)
							{
								goto IL_23E;
							}
							break;
						}
						this.ProcessMouseButtonPressed(m.HWnd, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam));
						goto IL_23E;
					}
					if (!activeToolStrip.ContainsFocus)
					{
						m.HWnd = activeToolStrip.Handle;
						goto IL_23E;
					}
					goto IL_23E;
					IL_153:
					Control control = Control.FromChildHandleInternal(m.HWnd);
					if ((control == null || !(control.TopLevelControlInternal is ToolStripDropDown)) && !ToolStripManager.ModalMenuFilter.IsChildOrSameWindow(handleRef, new HandleRef(null, m.HWnd)))
					{
						ToolStrip currentToplevelToolStrip = this.GetCurrentToplevelToolStrip();
						if (currentToplevelToolStrip != null && ToolStripManager.ModalMenuFilter.IsChildOrSameWindow(new HandleRef(currentToplevelToolStrip, currentToplevelToolStrip.Handle), new HandleRef(null, m.HWnd)))
						{
							return false;
						}
						if (!ToolStripManager.ModalMenuFilter.IsChildOrSameWindow(ToolStripManager.ModalMenuFilter.ActiveHwnd, new HandleRef(null, m.HWnd)))
						{
							return false;
						}
						return true;
					}
					IL_23E:;
				}
				return false;
			}

			// Token: 0x0400430D RID: 17165
			private HandleRef _activeHwnd = NativeMethods.NullHandleRef;

			// Token: 0x0400430E RID: 17166
			private HandleRef _lastActiveWindow = NativeMethods.NullHandleRef;

			// Token: 0x0400430F RID: 17167
			private List<ToolStrip> _inputFilterQueue;

			// Token: 0x04004310 RID: 17168
			private bool _inMenuMode;

			// Token: 0x04004311 RID: 17169
			private bool _caretHidden;

			// Token: 0x04004312 RID: 17170
			private bool _showUnderlines;

			// Token: 0x04004313 RID: 17171
			private bool menuKeyToggle;

			// Token: 0x04004314 RID: 17172
			private bool _suspendMenuMode;

			// Token: 0x04004315 RID: 17173
			private ToolStripManager.ModalMenuFilter.HostedWindowsFormsMessageHook messageHook;

			// Token: 0x04004316 RID: 17174
			private Timer _ensureMessageProcessingTimer;

			// Token: 0x04004317 RID: 17175
			private const int MESSAGE_PROCESSING_INTERVAL = 500;

			// Token: 0x04004318 RID: 17176
			private ToolStrip _toplevelToolStrip;

			// Token: 0x04004319 RID: 17177
			private readonly WeakReference<IKeyboardToolTip> lastFocusedTool = new WeakReference<IKeyboardToolTip>(null);

			// Token: 0x0400431A RID: 17178
			[ThreadStatic]
			private static ToolStripManager.ModalMenuFilter _instance;

			// Token: 0x020008CB RID: 2251
			private class HostedWindowsFormsMessageHook
			{
				// Token: 0x1700193D RID: 6461
				// (get) Token: 0x06007315 RID: 29461 RVA: 0x001A5202 File Offset: 0x001A3402
				// (set) Token: 0x06007316 RID: 29462 RVA: 0x001A5214 File Offset: 0x001A3414
				public bool HookMessages
				{
					get
					{
						return this.messageHookHandle != IntPtr.Zero;
					}
					set
					{
						if (value)
						{
							this.InstallMessageHook();
							return;
						}
						this.UninstallMessageHook();
					}
				}

				// Token: 0x06007317 RID: 29463 RVA: 0x001A5228 File Offset: 0x001A3428
				private void InstallMessageHook()
				{
					lock (this)
					{
						if (!(this.messageHookHandle != IntPtr.Zero))
						{
							this.hookProc = new NativeMethods.HookProc(this.MessageHookProc);
							this.messageHookHandle = UnsafeNativeMethods.SetWindowsHookEx(3, this.hookProc, new HandleRef(null, IntPtr.Zero), SafeNativeMethods.GetCurrentThreadId());
							if (this.messageHookHandle != IntPtr.Zero)
							{
								this.isHooked = true;
							}
						}
					}
				}

				// Token: 0x06007318 RID: 29464 RVA: 0x001A52C0 File Offset: 0x001A34C0
				private unsafe IntPtr MessageHookProc(int nCode, IntPtr wparam, IntPtr lparam)
				{
					if (nCode == 0 && this.isHooked && (int)wparam == 1)
					{
						NativeMethods.MSG* ptr = (NativeMethods.MSG*)((void*)lparam);
						if (ptr != null && Application.ThreadContext.FromCurrent().PreTranslateMessage(ref *ptr))
						{
							ptr->message = 0;
						}
					}
					return UnsafeNativeMethods.CallNextHookEx(new HandleRef(this, this.messageHookHandle), nCode, wparam, lparam);
				}

				// Token: 0x06007319 RID: 29465 RVA: 0x001A5318 File Offset: 0x001A3518
				private void UninstallMessageHook()
				{
					lock (this)
					{
						if (this.messageHookHandle != IntPtr.Zero)
						{
							UnsafeNativeMethods.UnhookWindowsHookEx(new HandleRef(this, this.messageHookHandle));
							this.hookProc = null;
							this.messageHookHandle = IntPtr.Zero;
							this.isHooked = false;
						}
					}
				}

				// Token: 0x04004558 RID: 17752
				private IntPtr messageHookHandle = IntPtr.Zero;

				// Token: 0x04004559 RID: 17753
				private bool isHooked;

				// Token: 0x0400455A RID: 17754
				private NativeMethods.HookProc hookProc;
			}
		}
	}
}
