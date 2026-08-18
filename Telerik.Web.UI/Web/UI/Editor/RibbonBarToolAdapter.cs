using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002DC RID: 732
	internal class RibbonBarToolAdapter : DefaultToolAdapter
	{
		// Token: 0x0600196B RID: 6507 RVA: 0x000538FB File Offset: 0x00051AFB
		public RibbonBarToolAdapter()
		{
			if (base.Editor != null)
			{
				this._ribbonbar = base.Editor.RibbonBar;
				this.InitializeCommandsSprites(base.Editor);
			}
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0005392F File Offset: 0x00051B2F
		public RibbonBarToolAdapter(RadEditor editor) : base(editor)
		{
			this._ribbonbar = editor.RibbonBar;
			this.InitializeCommandsSprites(editor);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00053954 File Offset: 0x00051B54
		private void InitializeCommandsSprites(RadEditor editor)
		{
			try
			{
				HttpBrowserCapabilities browser = editor.Page.Request.Browser;
				string runtimeSkin;
				switch (runtimeSkin = editor.RuntimeSkin)
				{
				case "Black":
				case "Office2010Black":
					if (browser.IsBrowser("IE") && browser.MajorVersion < 7)
					{
						this._commandsSpriteUrl = base.Editor.Page.ClientScript.GetWebResourceUrl(typeof(RadEditor), "Telerik.Web.UI.Skins.Common.CommandSpritesDarkIE6.gif");
						this._commandsSpriteLargeUrl = base.Editor.Page.ClientScript.GetWebResourceUrl(typeof(RadEditor), "Telerik.Web.UI.Skins.Common.EditorCommandRibbonDarkIE6.gif");
						goto IL_3DB;
					}
					this._commandsSpriteUrl = base.Editor.Page.ClientScript.GetWebResourceUrl(typeof(RadEditor), "Telerik.Web.UI.Skins.Common.CommandSpritesDark.png");
					this._commandsSpriteLargeUrl = base.Editor.Page.ClientScript.GetWebResourceUrl(typeof(RadEditor), "Telerik.Web.UI.Skins.Common.EditorCommandRibbonDark.png");
					goto IL_3DB;
				case "BlackMetroTouch":
				case "Glow":
					if (browser.IsBrowser("IE") && browser.MajorVersion < 7)
					{
						this._commandsSpriteUrl = base.Editor.Page.ClientScript.GetWebResourceUrl(typeof(RadEditor), "Telerik.Web.UI.Skins.Common.CommandSpritesMonoDarkIE6.gif");
						this._commandsSpriteLargeUrl = base.Editor.Page.ClientScript.GetWebResourceUrl(typeof(RadEditor), "Telerik.Web.UI.Skins.Common.EditorCommandRibbonMonoDarkIE6.gif");
						goto IL_3DB;
					}
					this._commandsSpriteUrl = base.Editor.Page.ClientScript.GetWebResourceUrl(typeof(RadEditor), "Telerik.Web.UI.Skins.Common.CommandSpritesMonoDark.png");
					this._commandsSpriteLargeUrl = base.Editor.Page.ClientScript.GetWebResourceUrl(typeof(RadEditor), "Telerik.Web.UI.Skins.Common.EditorCommandRibbonMonoDark.png");
					goto IL_3DB;
				case "Metro":
				case "MetroTouch":
				case "Silk":
					if (browser.IsBrowser("IE") && browser.MajorVersion < 7)
					{
						this._commandsSpriteUrl = base.Editor.Page.ClientScript.GetWebResourceUrl(typeof(RadEditor), "Telerik.Web.UI.Skins.Common.CommandSpritesMonoLightIE6.gif");
						this._commandsSpriteLargeUrl = base.Editor.Page.ClientScript.GetWebResourceUrl(typeof(RadEditor), "Telerik.Web.UI.Skins.Common.EditorCommandRibbonMonoKightIE6.gif");
						goto IL_3DB;
					}
					this._commandsSpriteUrl = base.Editor.Page.ClientScript.GetWebResourceUrl(typeof(RadEditor), "Telerik.Web.UI.Skins.Common.CommandSpritesMonoLight.png");
					this._commandsSpriteLargeUrl = base.Editor.Page.ClientScript.GetWebResourceUrl(typeof(RadEditor), "Telerik.Web.UI.Skins.Common.EditorCommandRibbonMonoLight.png");
					goto IL_3DB;
				}
				if (browser.IsBrowser("IE") && browser.MajorVersion < 7)
				{
					this._commandsSpriteUrl = base.Editor.Page.ClientScript.GetWebResourceUrl(typeof(RadEditor), "Telerik.Web.UI.Skins.Common.CommandSpritesLightIE6.gif");
					this._commandsSpriteLargeUrl = base.Editor.Page.ClientScript.GetWebResourceUrl(typeof(RadEditor), "Telerik.Web.UI.Skins.Common.EditorCommandRibbonLightIE6.gif");
				}
				else
				{
					this._commandsSpriteUrl = base.Editor.Page.ClientScript.GetWebResourceUrl(typeof(RadEditor), "Telerik.Web.UI.Skins.Common.CommandSpritesLight.png");
					this._commandsSpriteLargeUrl = base.Editor.Page.ClientScript.GetWebResourceUrl(typeof(RadEditor), "Telerik.Web.UI.Skins.Common.EditorCommandRibbonLight.png");
				}
				IL_3DB:;
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x00053D60 File Offset: 0x00051F60
		public override string ClientType
		{
			get
			{
				return "Telerik.Web.UI.Editor.RibbonBarToolAdapter";
			}
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x00053D68 File Offset: 0x00051F68
		public override void PreRender()
		{
			if (this._ribbonbar != null && this._ribbonbar.Visible)
			{
				this._ribbonbar.Skin = base.Editor.RuntimeSkin;
				foreach (object obj in base.Editor.Tools)
				{
					EditorToolGroup group = (EditorToolGroup)obj;
					this.LoadToolGroup(group);
				}
			}
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x00053DF4 File Offset: 0x00051FF4
		private void LoadToolGroup(EditorToolGroup group)
		{
			string text = this.TranslateToolString(group.Tab);
			string text2 = this.TranslateToolString(group.Tag);
			RibbonBarTab ribbonBarTabByText = this.GetRibbonBarTabByText(text);
			RibbonBarGroup ribbonBarGroup = new RibbonBarGroup();
			ribbonBarGroup.Text = text2;
			foreach (object obj in group.Tools)
			{
				EditorToolBase editorToolBase = (EditorToolBase)obj;
				editorToolBase.RenderMode = base.Editor.ResolvedRenderMode;
				this.LoadEditorTool(editorToolBase, ribbonBarGroup);
			}
			ribbonBarTabByText.Groups.Add(ribbonBarGroup);
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x00053EA4 File Offset: 0x000520A4
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private void LoadEditorTool(EditorToolBase tool, RibbonBarGroup rbGroup)
		{
			EditorToolType type = tool.Type;
			switch (type)
			{
			case EditorToolType.Button:
			case (EditorToolType)3:
				break;
			case EditorToolType.DropDown:
			case EditorToolType.SplitButton:
			{
				RibbonBarTemplateItem item = this.CreateRibbonTemplate(tool);
				this.AppendRibbonBarItemToGroup(item, rbGroup);
				return;
			}
			default:
				if (type == EditorToolType.Separator)
				{
					return;
				}
				if (type == EditorToolType.ToolStrip)
				{
					EditorToolStrip editorToolStrip = tool as EditorToolStrip;
					if (editorToolStrip.Name == "InsertTable")
					{
						RibbonBarTemplateItem item2 = this.CreateRibbonTemplate(tool);
						this.AppendRibbonBarItemToGroup(item2, rbGroup);
						return;
					}
					RibbonBarSplitButton ribbonBarSplitButton = this.CreateRibbonSplitButton(editorToolStrip);
					if (ribbonBarSplitButton.Size == RibbonBarItemSize.Large)
					{
						RibbonBarControlGroup ribbonBarControlGroup = new RibbonBarControlGroup();
						ribbonBarControlGroup.Orientation = RibbonBarControlGroupOrientation.Vertical;
						ribbonBarControlGroup.Items.Add(ribbonBarSplitButton);
						rbGroup.Items.Add(ribbonBarControlGroup);
						return;
					}
					this.AppendRibbonBarItemToGroup(ribbonBarSplitButton, rbGroup);
					return;
				}
				break;
			}
			RibbonBarToggleButton rbButton = this.CreateRibbonButton(tool as EditorTool);
			this.AppendTool(tool, rbButton, rbGroup);
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x00053F78 File Offset: 0x00052178
		protected virtual RibbonBarSplitButton CreateRibbonSplitButton(EditorToolStrip tool)
		{
			RibbonBarSplitButton ribbonBarSplitButton = new RibbonBarSplitButton();
			ribbonBarSplitButton.Attributes.Add("rel", tool.Name);
			this.LoadSplitButtonTools(tool, ribbonBarSplitButton);
			this.InitializeRibbonBarSplitButton(ribbonBarSplitButton, tool);
			return ribbonBarSplitButton;
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x00053FB4 File Offset: 0x000521B4
		protected virtual RibbonBarTemplateItem CreateRibbonTemplate(EditorToolBase tool)
		{
			tool = base.GetToolUIObject(tool);
			EditorToolTemplateContainer child = new EditorToolTemplateContainer(tool as EditorTool);
			return new RibbonBarTemplateItem
			{
				Controls = 
				{
					child
				}
			};
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x00053FEC File Offset: 0x000521EC
		protected virtual RibbonBarToggleButton CreateRibbonButton(EditorTool tool)
		{
			RibbonBarToggleButton ribbonBarToggleButton = new RibbonBarToggleButton();
			this.InitializeRibbonBarButton(ribbonBarToggleButton, tool);
			return ribbonBarToggleButton;
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x00054008 File Offset: 0x00052208
		private void AppendRibbonBarItemToGroup(RibbonBarItem item, RibbonBarGroup group)
		{
			RibbonBarControlGroup controlGroupForAppending = this.GetControlGroupForAppending(group);
			controlGroupForAppending.Items.Add(item);
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0005402C File Offset: 0x0005222C
		private void InitializeRibbonBarSplitButton(RibbonBarSplitButton splitButton, EditorToolStrip toolStrip)
		{
			if (toolStrip.Tools.Count > 0)
			{
				EditorTool tool = toolStrip.Tools[0];
				this.SetRibbonBarButtonSize(splitButton, toolStrip);
				this.InitializeRibbonBarClickableItem(splitButton, tool);
			}
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00054064 File Offset: 0x00052264
		private void InitializeRibbonBarButton(RibbonBarButton button, EditorTool tool)
		{
			this.SetRibbonBarButtonSize(button, tool);
			button.Value = tool.Name;
			this.InitializeRibbonBarClickableItem(button, tool);
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x00054084 File Offset: 0x00052284
		protected virtual void InitializeRibbonBarClickableItem(RibbonBarClickableItem button, EditorTool tool)
		{
			button.ImageUrl = (string.IsNullOrEmpty(tool.ImageUrl) ? this._commandsSpriteUrl : tool.ImageUrl);
			button.DisabledImageUrl = button.ImageUrl;
			button.ImageUrlLarge = (string.IsNullOrEmpty(tool.ImageUrlLarge) ? this._commandsSpriteLargeUrl : tool.ImageUrlLarge);
			button.DisabledImageUrlLarge = button.ImageUrlLarge;
			button.CssClass = tool.Name;
			this.SetRibbonBarButtonText(button, tool);
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x00054100 File Offset: 0x00052300
		protected void SetRibbonBarButtonText(RibbonBarClickableItem button, EditorTool tool)
		{
			string text = tool.Text;
			if (string.IsNullOrEmpty(text))
			{
				text = base.Editor.Localization.Tools.GetString(tool.Name);
				if (string.IsNullOrEmpty(text))
				{
					text = tool.Name;
				}
			}
			button.Text = text;
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x00054150 File Offset: 0x00052350
		private void SetRibbonBarButtonSize(RibbonBarClickableItem button, EditorToolBase tool)
		{
			string a;
			if (tool.Attributes["size"] != null && (a = tool.Attributes["size"].ToLower()) != null)
			{
				if (a == "small")
				{
					button.Size = RibbonBarItemSize.Small;
					return;
				}
				if (a == "medium")
				{
					button.Size = RibbonBarItemSize.Medium;
					return;
				}
				if (!(a == "large"))
				{
					return;
				}
				button.Size = RibbonBarItemSize.Large;
			}
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x000541C8 File Offset: 0x000523C8
		private void LoadSplitButtonTools(EditorToolStrip toolStrip, RibbonBarSplitButton splitButton)
		{
			foreach (object obj in toolStrip.Tools)
			{
				EditorToolBase editorToolBase = (EditorToolBase)obj;
				if (editorToolBase.Type != EditorToolType.Separator)
				{
					EditorTool tool = editorToolBase as EditorTool;
					RibbonBarToggleButton button = new RibbonBarToggleButton();
					this.InitializeRibbonBarButton(button, tool);
					splitButton.Buttons.Add(button);
				}
			}
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00054248 File Offset: 0x00052448
		private void AppendTool(EditorToolBase tool, RibbonBarButton rbButton, RibbonBarGroup rbGroup)
		{
			if (tool.Attributes["strip"] != null)
			{
				RibbonBarButtonStrip ribbonBarButtonStripByID = this.GetRibbonBarButtonStripByID(tool.Attributes["strip"], rbGroup);
				ribbonBarButtonStripByID.Buttons.Add(rbButton);
				return;
			}
			if (rbButton.Size == RibbonBarItemSize.Large)
			{
				RibbonBarControlGroup ribbonBarControlGroup = new RibbonBarControlGroup();
				ribbonBarControlGroup.Orientation = RibbonBarControlGroupOrientation.Vertical;
				ribbonBarControlGroup.Items.Add(rbButton);
				rbGroup.Items.Add(ribbonBarControlGroup);
				return;
			}
			this.AppendRibbonBarItemToGroup(rbButton, rbGroup);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x000542C4 File Offset: 0x000524C4
		private RibbonBarControlGroup GetControlGroupForAppending(RibbonBarGroup rbGroup)
		{
			RibbonBarControlGroup ribbonBarControlGroup = this.GetGroupLastControlGroup(rbGroup);
			if (ribbonBarControlGroup == null || this.ControlGroupIsFull(ribbonBarControlGroup))
			{
				ribbonBarControlGroup = new RibbonBarControlGroup();
				ribbonBarControlGroup.Orientation = RibbonBarControlGroupOrientation.Vertical;
				rbGroup.Items.Add(ribbonBarControlGroup);
			}
			return ribbonBarControlGroup;
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00054300 File Offset: 0x00052500
		private bool ControlGroupIsFull(RibbonBarControlGroup contolGroup)
		{
			bool result = false;
			int count = contolGroup.Items.Count;
			foreach (RibbonBarItem ribbonBarItem in contolGroup.Items)
			{
				string name = ribbonBarItem.GetType().Name;
				if (name != "RibbonBarTemplateItem")
				{
					RibbonBarClickableItem ribbonBarClickableItem = ribbonBarItem as RibbonBarClickableItem;
					if (ribbonBarClickableItem != null && ribbonBarClickableItem.Size == RibbonBarItemSize.Large)
					{
						result = true;
					}
				}
			}
			if (count >= this._itemsInControlGroup)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x00054398 File Offset: 0x00052598
		private RibbonBarControlGroup GetGroupLastControlGroup(RibbonBarGroup rbGroup)
		{
			RibbonBarControlGroup result = null;
			foreach (RibbonBarItem ribbonBarItem in rbGroup.Items)
			{
				if (ribbonBarItem.GetType().Name == "RibbonBarControlGroup")
				{
					result = (RibbonBarControlGroup)ribbonBarItem;
				}
			}
			return result;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x00054408 File Offset: 0x00052608
		private RibbonBarButtonStrip GetRibbonBarButtonStripByID(string id, RibbonBarGroup rbGroup)
		{
			foreach (RibbonBarItem ribbonBarItem in rbGroup.Items)
			{
				string name = ribbonBarItem.GetType().Name;
				string a;
				if ((a = name) != null)
				{
					if (!(a == "RibbonBarButtonStrip"))
					{
						if (a == "RibbonBarControlGroup")
						{
							RibbonBarControlGroup ribbonBarControlGroup = (RibbonBarControlGroup)ribbonBarItem;
							foreach (RibbonBarItem ribbonBarItem2 in ribbonBarControlGroup.Items)
							{
								if (ribbonBarItem2.GetType().Name == "RibbonBarButtonStrip" && ribbonBarItem2.ID == id)
								{
									return ribbonBarItem2 as RibbonBarButtonStrip;
								}
							}
						}
					}
					else if (ribbonBarItem.ID == id)
					{
						return ribbonBarItem as RibbonBarButtonStrip;
					}
				}
			}
			RibbonBarButtonStrip ribbonBarButtonStrip = new RibbonBarButtonStrip();
			ribbonBarButtonStrip.ID = id;
			this.AppendRibbonBarItemToGroup(ribbonBarButtonStrip, rbGroup);
			return ribbonBarButtonStrip;
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0005453C File Offset: 0x0005273C
		private RibbonBarTab GetRibbonBarTabByText(string text)
		{
			foreach (RibbonBarTab ribbonBarTab in this._ribbonbar.Tabs)
			{
				if (ribbonBarTab.Text == text)
				{
					return ribbonBarTab;
				}
			}
			RibbonBarTab ribbonBarTab2 = new RibbonBarTab();
			ribbonBarTab2.Text = text;
			this._ribbonbar.Tabs.Add(ribbonBarTab2);
			return ribbonBarTab2;
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x000545C0 File Offset: 0x000527C0
		private string TranslateToolString(string key)
		{
			string result;
			if (!string.IsNullOrEmpty(key))
			{
				if ((result = base.Editor.Localization.Tools.GetString(key)) == null)
				{
					return key;
				}
			}
			else
			{
				result = key;
			}
			return result;
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x000545E7 File Offset: 0x000527E7
		public override void Render(HtmlTextWriter writer)
		{
		}

		// Token: 0x04000696 RID: 1686
		protected RadRibbonBar _ribbonbar;

		// Token: 0x04000697 RID: 1687
		private string _commandsSpriteUrl;

		// Token: 0x04000698 RID: 1688
		private string _commandsSpriteLargeUrl;

		// Token: 0x04000699 RID: 1689
		private readonly int _itemsInControlGroup = 3;
	}
}
