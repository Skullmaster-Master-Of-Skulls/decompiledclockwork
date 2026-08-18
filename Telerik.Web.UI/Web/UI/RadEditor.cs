using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using Telerik.Licensing;
using Telerik.Web.UI.Editor;
using Telerik.Web.UI.Editor.Animations;
using Telerik.Web.UI.Editor.Content;
using Telerik.Web.UI.Editor.DialogControls;
using Telerik.Web.UI.Editor.Docx;
using Telerik.Web.UI.Editor.Dpl;
using Telerik.Web.UI.Editor.Export;
using Telerik.Web.UI.Editor.JavascriptSerialization;
using Telerik.Web.UI.Editor.MarkdownSharp;
using Telerik.Web.UI.Editor.Rtf;
using Telerik.Web.UI.Editor.TrackChanges;

namespace Telerik.Web.UI
{
	// Token: 0x02000F6B RID: 3947
	[EmbeddedSkin("Editor")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Mobile, typeof(RadButton))]
	[RequiredScript(typeof(ResizeExtender))]
	[RequiredScript(typeof(RadEditorScripts))]
	[ClientScriptResource("Telerik.Web.UI.RadEditor", "Telerik.Web.UI.Common.Core.js")]
	[AdaptiveRendering]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[LightweightRendering]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadEditor))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(RadEditor))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[RequiredScript(typeof(SpellCheckService))]
	[RequiredScript(typeof(LayoutBuilderEngine))]
	[EmbeddedSkin("Editor", "Default")]
	[Designer("Telerik.Web.Design.RadEditorDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ValidationProperty("Content")]
	[TelerikToolboxCategory("Data Editing")]
	[ToolboxBitmap(typeof(RadEditor), "Telerik.Web.UI.Editor.png")]
	[ToolboxData("<{0}:RadEditor Runat=server></{0}:RadEditor>")]
	[ParseChildren(true)]
	public class RadEditor : RadWebControl, ILocalizableControl, IEditableTextControl, ITextControl, INamingContainer
	{
		// Token: 0x0600962C RID: 38444 RVA: 0x00218893 File Offset: 0x00216A93
		private void FillDefaultDialogDefinitions()
		{
			if (!this._filledDefaultDialogDefinitions)
			{
				this._filledDefaultDialogDefinitions = true;
				if (this.IsSkinTouch)
				{
					this.FillTouchDefinitions();
				}
				else
				{
					this.FillDefinitions();
				}
				this.AddMobileDialogs();
				return;
			}
		}

		// Token: 0x0600962D RID: 38445 RVA: 0x002188C4 File Offset: 0x00216AC4
		private void FillTouchDefinitions()
		{
			Unit width = Unit.Pixel(690);
			Unit height = Unit.Pixel(490);
			this.FillDialogDefinition("ImageProperties", typeof(SetImagePropertiesDialog), Unit.Pixel(430), Unit.Pixel(570), this.Localization.Tools.SetImageProperties, new DialogParameters(), true, false);
			this.FillDialogDefinition("FlashManager", typeof(FlashManagerDialog), width, Unit.Pixel(670), this.Localization.Tools.FlashManager, this.FlashManager.ToDialogParameters());
			this.FillDialogDefinition("MediaManager", typeof(MediaManagerDialog), width, height, this.Localization.Tools.MediaManager, this.MediaManager.ToDialogParameters());
			this.FillDialogDefinition("InsertSelectDialog", typeof(InsertSelectDialog), Unit.Pixel(540), Unit.Pixel(500), this.Localization.Tools.InsertSelectDialog, new DialogParameters(), true, false);
			Unit width2 = Unit.Pixel(517);
			Unit height2 = Unit.Pixel(405);
			this.FillDialogDefinition("CleanPasteHtmlContent", typeof(MozillaPasteHtmlDialog), width2, height2, this.Localization.Tools.PasteAsHtml, new DialogParameters(), true, false);
			this.FillDialogDefinition("PasteMarkdown", typeof(MozillaPasteTextDialog), width2, height2, this.Localization.Tools.PasteMarkdown, new DialogParameters(), true, false);
			this.FillDialogDefinition("PasteHtml", typeof(MozillaPasteTextDialog), width2, height2, this.Localization.Tools.PasteHtml, new DialogParameters(), true, false);
			this.FillDialogDefinition("CleanPasteTextContent", typeof(MozillaPasteTextDialog), width2, height2, this.Localization.Tools.PastePlainText, new DialogParameters(), true, false);
			this.FillDialogDefinition("XhtmlValidator", typeof(XhtmlValidator), Unit.Pixel(750), Unit.Pixel(550), this.Localization.Tools.XhtmlValidator);
			this.FillDialogDefinition("FormatCodeBlock", typeof(FormatCodeBlockDialog), Unit.Pixel(724), Unit.Pixel(705), this.Localization.Tools.FormatCodeBlock);
			this.FillDialogDefinition("AboutDialog", typeof(About), Unit.Pixel(507), Unit.Pixel(339), this.Localization.Tools.AboutDialog);
			this.FillDialogDefinition("FindAndReplace", typeof(FindAndReplaceDialog), Unit.Pixel(440), Unit.Pixel(475), this.Localization.Tools.FindAndReplace, false);
			this.FillDialogDefinition("CSDialog", typeof(CSDialog), Unit.Pixel(1000), Unit.Pixel(600), this.Localization.Tools.CSDialog);
			this.FillDialogDefinition("LinkManager", typeof(LinkManagerDialog), Unit.Pixel(408), Unit.Pixel(340), this.Localization.Tools.LinkManager, new DialogParameters(), true, false);
			this.FillDialogDefinition("PageProperties", typeof(PageProperties), Unit.Pixel(409), Unit.Pixel(685), this.Localization.Tools.PageProperties);
			this.FillDialogDefinition("InsertExternalVideo", typeof(InsertExternalVideo), Unit.Pixel(770), Unit.Pixel(610), this.Localization.Tools.InsertExternalVideo, new DialogParameters(), true, false);
			this.FillDialogDefinition("ImageEditor", typeof(ImageEditorDialog), Unit.Pixel(832), Unit.Pixel(520), this.Localization.Tools.ImageEditor, this.ImageManager.ToDialogParameters(), true, false);
			this.FillDialogDefinition("TableWizard", typeof(TableWizardDialog), Unit.Pixel(730), Unit.Pixel(595), this.Localization.Tools.TableWizard, new DialogParameters(), true, false);
			this.FillDialogDefinition("ImageMapDialog", typeof(ImageMapDialog), Unit.Pixel(840), Unit.Pixel(575), this.Localization.Tools.ImageMapDialog, new DialogParameters(), true, false);
			this.FillDialogDefinition("StyleBuilder", typeof(StyleBuilder), Unit.Pixel(720), Unit.Pixel(590), this.Localization.Tools.StyleBuilder, new DialogParameters(), true, false);
			this.FillDialogDefinition("Help", typeof(Help), Unit.Pixel(720), Unit.Pixel(525), this.Localization.Tools.Help);
			this.FillDialogDefinition("TemplateManager", typeof(TemplateManagerDialog), width, height, this.Localization.Tools.TemplateManager, this.TemplateManager.ToDialogParameters());
			this.FillDialogDefinition("SilverlightManager", typeof(SilverlightManagerDialog), width, height, this.Localization.Tools.SilverlightManager, this.SilverlightManager.ToDialogParameters());
			this.FillDialogDefinition("DocumentManager", typeof(DocumentManagerDialog), width, height, this.Localization.Tools.DocumentManager, this.DocumentManager.ToDialogParameters(), true, false);
			this.FillDialogDefinition("ImageManager", typeof(ImageManagerDialog), Unit.Pixel(770), Unit.Pixel(730), this.Localization.Tools.ImageManager, this.ImageManager.ToDialogParameters(), true, false);
		}

		// Token: 0x0600962E RID: 38446 RVA: 0x00218E98 File Offset: 0x00217098
		private void FillDefinitions()
		{
			Unit width = Unit.Pixel(690);
			Unit height = Unit.Pixel(490);
			this.FillDialogDefinition("ImageManager", typeof(ImageManagerDialog), Unit.Pixel(770), Unit.Pixel(588), this.Localization.Tools.ImageManager, this.ImageManager.ToDialogParameters(), true, false);
			this.FillDialogDefinition("DocumentManager", typeof(DocumentManagerDialog), width, height, this.Localization.Tools.DocumentManager, this.DocumentManager.ToDialogParameters(), true, false);
			this.FillDialogDefinition("FlashManager", typeof(FlashManagerDialog), width, height, this.Localization.Tools.FlashManager, this.FlashManager.ToDialogParameters());
			this.FillDialogDefinition("SilverlightManager", typeof(SilverlightManagerDialog), width, height, this.Localization.Tools.SilverlightManager, this.SilverlightManager.ToDialogParameters());
			this.FillDialogDefinition("MediaManager", typeof(MediaManagerDialog), width, height, this.Localization.Tools.MediaManager, this.MediaManager.ToDialogParameters());
			this.FillDialogDefinition("TemplateManager", typeof(TemplateManagerDialog), width, height, this.Localization.Tools.TemplateManager, this.TemplateManager.ToDialogParameters());
			this.FillDialogDefinition("AboutDialog", typeof(About), Unit.Pixel(507), Unit.Pixel(339), this.Localization.Tools.AboutDialog);
			this.FillDialogDefinition("PageProperties", typeof(PageProperties), Unit.Pixel(409), Unit.Pixel(450), this.Localization.Tools.PageProperties);
			this.FillDialogDefinition("Help", typeof(Help), Unit.Pixel(720), Unit.Pixel(490), this.Localization.Tools.Help);
			this.FillDialogDefinition("LinkManager", typeof(LinkManagerDialog), Unit.Pixel(408), Unit.Pixel(340), this.Localization.Tools.LinkManager, new DialogParameters(), true, false);
			this.FillDialogDefinition("TableWizard", typeof(TableWizardDialog), Unit.Pixel(720), Unit.Pixel(500), this.Localization.Tools.TableWizard, new DialogParameters(), true, false);
			this.FillDialogDefinition("FindAndReplace", typeof(FindAndReplaceDialog), Unit.Pixel(450), Unit.Pixel(300), this.Localization.Tools.FindAndReplace, false);
			this.FillDialogDefinition("CleanPasteHtmlContent", typeof(MozillaPasteHtmlDialog), Unit.Pixel(517), Unit.Pixel(300), this.Localization.Tools.PasteAsHtml, new DialogParameters(), true, false);
			this.FillDialogDefinition("PasteMarkdown", typeof(MozillaPasteTextDialog), Unit.Pixel(517), Unit.Pixel(300), this.Localization.Tools.PasteMarkdown, new DialogParameters(), true, false);
			this.FillDialogDefinition("PasteHtml", typeof(MozillaPasteTextDialog), Unit.Pixel(517), Unit.Pixel(300), this.Localization.Tools.PasteHtml, new DialogParameters(), true, false);
			this.FillDialogDefinition("CleanPasteTextContent", typeof(MozillaPasteTextDialog), Unit.Pixel(517), Unit.Pixel(300), this.Localization.Tools.PastePlainText, new DialogParameters(), true, false);
			this.FillDialogDefinition("ImageEditor", typeof(ImageEditorDialog), Unit.Pixel(832), Unit.Pixel(520), this.Localization.Tools.ImageEditor, this.ImageManager.ToDialogParameters(), true, false);
			this.FillDialogDefinition("ImageProperties", typeof(SetImagePropertiesDialog), Unit.Pixel(430), Unit.Pixel(432), this.Localization.Tools.SetImageProperties, new DialogParameters(), true, false);
			this.FillDialogDefinition("FormatCodeBlock", typeof(FormatCodeBlockDialog), Unit.Pixel(724), Unit.Pixel(590), this.Localization.Tools.FormatCodeBlock);
			this.FillDialogDefinition("ImageMapDialog", typeof(ImageMapDialog), Unit.Pixel(700), Unit.Pixel(430), this.Localization.Tools.ImageMapDialog, new DialogParameters(), true, false);
			this.FillDialogDefinition("XhtmlValidator", typeof(XhtmlValidator), Unit.Pixel(750), Unit.Pixel(550), this.Localization.Tools.XhtmlValidator);
			this.FillDialogDefinition("StyleBuilder", typeof(StyleBuilder), Unit.Pixel(620), Unit.Pixel(530), this.Localization.Tools.StyleBuilder, new DialogParameters(), true, false);
			this.FillDialogDefinition("InsertExternalVideo", typeof(InsertExternalVideo), Unit.Pixel(770), Unit.Pixel(610), this.Localization.Tools.InsertExternalVideo, new DialogParameters(), true, false);
			this.FillDialogDefinition("CSDialog", typeof(CSDialog), Unit.Pixel(1000), Unit.Pixel(600), this.Localization.Tools.CSDialog);
			this.FillDialogDefinition("InsertSelectDialog", typeof(InsertSelectDialog), Unit.Pixel(527), Unit.Pixel(410), this.Localization.Tools.InsertSelectDialog, new DialogParameters(), true, false);
		}

		// Token: 0x0600962F RID: 38447 RVA: 0x00219493 File Offset: 0x00217693
		private void FillDialogDefinition(string dialogName, Type dialogType, Unit width, Unit height, string title)
		{
			this.FillDialogDefinition(dialogName, dialogType, width, height, title, new DialogParameters(), true);
		}

		// Token: 0x06009630 RID: 38448 RVA: 0x002194A8 File Offset: 0x002176A8
		private void FillDialogDefinition(string dialogName, Type dialogType, Unit width, Unit height, string title, bool modal)
		{
			this.FillDialogDefinition(dialogName, dialogType, width, height, title, new DialogParameters(), modal);
		}

		// Token: 0x06009631 RID: 38449 RVA: 0x002194BE File Offset: 0x002176BE
		private void FillDialogDefinition(string dialogName, Type dialogType, Unit width, Unit height, string title, DialogParameters dialogParameters)
		{
			this.FillDialogDefinition(dialogName, dialogType, width, height, title, dialogParameters, true);
		}

		// Token: 0x06009632 RID: 38450 RVA: 0x002194D0 File Offset: 0x002176D0
		private void FillDialogDefinition(string dialogName, Type dialogType, Unit width, Unit height, string title, DialogParameters dialogParameters, bool modal)
		{
			this.FillDialogDefinition(dialogName, dialogType, width, height, title, dialogParameters, modal, true);
		}

		// Token: 0x06009633 RID: 38451 RVA: 0x002194F0 File Offset: 0x002176F0
		private void FillDialogDefinition(string dialogName, Type dialogType, Unit width, Unit height, string title, DialogParameters dialogParameters, bool modal, bool checkIfToolContained)
		{
			if (!checkIfToolContained || this.ContainsTool(dialogName))
			{
				DialogDefinition dialogDefinition = new DialogDefinition(dialogType, dialogParameters);
				dialogDefinition.Width = width;
				dialogDefinition.Height = height;
				dialogDefinition.Title = title;
				dialogDefinition.Modal = modal;
				dialogDefinition.Parameters["IsSkinTouch"] = this.IsSkinTouch;
				dialogDefinition.Parameters["EnableEmbeddedSkins"] = this.EnableEmbeddedSkins;
				dialogDefinition.Parameters["EnableEmbeddedBaseStylesheet"] = this.EnableEmbeddedBaseStylesheet;
				dialogDefinition.Parameters["Language"] = this.Language;
				dialogDefinition.Parameters["ExternalDialogsPath"] = this.ExternalDialogsPath;
				if (this.IsInAccessibleMode)
				{
					dialogDefinition.Parameters["IsInAccessibleMode"] = true;
				}
				dialogDefinition.Parameters["LocalizationPath"] = this.LocalizationPath;
				if (dialogName == "ImageMapDialog")
				{
					dialogDefinition.ReloadOnShow = true;
				}
				if (dialogParameters is FileManagerDialogParameters)
				{
					this.FillCallbackMethods(dialogParameters);
				}
				this.DialogDefinitions[dialogName] = dialogDefinition;
			}
		}

		// Token: 0x06009634 RID: 38452 RVA: 0x00219618 File Offset: 0x00217818
		private void FillEditorPropertiesInDialogDefinition(DialogDefinition definition)
		{
			definition.Parameters["IsSkinTouch"] = this.IsSkinTouch;
			definition.Parameters["EnableEmbeddedSkins"] = this.EnableEmbeddedSkins;
			definition.Parameters["EnableEmbeddedBaseStylesheet"] = this.EnableEmbeddedBaseStylesheet;
			definition.Parameters["Language"] = this.Language;
			definition.Parameters["ExternalDialogsPath"] = this.ExternalDialogsPath;
			if (this.IsInAccessibleMode)
			{
				definition.Parameters["IsInAccessibleMode"] = true;
			}
			definition.Parameters["LocalizationPath"] = this.LocalizationPath;
			this.FillCallbackMethods(definition.Parameters);
		}

		// Token: 0x06009635 RID: 38453 RVA: 0x002196E2 File Offset: 0x002178E2
		private void FillDialogDefinition(string dialogName, DialogDefinition definition, bool checkIfToolContained)
		{
			if (!checkIfToolContained || this.ContainsTool(dialogName))
			{
				this.AddDialogDefinition(dialogName, definition);
			}
		}

		// Token: 0x06009636 RID: 38454 RVA: 0x002196F8 File Offset: 0x002178F8
		private void FillCallbackMethods(DialogParameters dialogParameters)
		{
			EditorDialogEventHandler editorDialogEventHandler = base.Events[this.FileUploadEvent] as EditorDialogEventHandler;
			if (editorDialogEventHandler != null)
			{
				if (!editorDialogEventHandler.Method.IsPublic)
				{
					throw new ArgumentException("FileUpload event handler must be a public method.");
				}
				string name = editorDialogEventHandler.Method.Name;
				dialogParameters["OnFileUpload"] = name;
				string assemblyQualifiedName = editorDialogEventHandler.Method.DeclaringType.AssemblyQualifiedName;
				dialogParameters["OnFileUploadDeclaringClass"] = assemblyQualifiedName;
			}
			EditorDialogEventHandler editorDialogEventHandler2 = base.Events[this.FileDeleteEvent] as EditorDialogEventHandler;
			if (editorDialogEventHandler2 != null)
			{
				if (!editorDialogEventHandler2.Method.IsPublic)
				{
					throw new ArgumentException("FileDelete event handler must be a public method.");
				}
				string name2 = editorDialogEventHandler2.Method.Name;
				dialogParameters["OnFileDelete"] = name2;
				string assemblyQualifiedName2 = editorDialogEventHandler2.Method.DeclaringType.AssemblyQualifiedName;
				dialogParameters["OnFileDeleteDeclaringClass"] = assemblyQualifiedName2;
			}
		}

		// Token: 0x06009637 RID: 38455 RVA: 0x002197D8 File Offset: 0x002179D8
		private void AddMobileDialogs()
		{
			this.AddMobileDialog("FindReplaceSettings", typeof(FindReplaceSettingsDialog));
			this.AddMobileDialog("MobileLinkManager", typeof(MobileLinkManagerDialog));
			this.AddMobileDialog("MobileImageManager", typeof(MobileImageManagerDialog), this.ImageManager.ToDialogParameters());
			this.AddMobileDialog("InsertTable", typeof(InsertTableDialog));
			this.AddMobileDialog("MobileTableProperties", typeof(MobileTablePropertiesDialog));
			this.AddMobileDialog("MobileImageProperties", typeof(MobileImagePropertiesDialog));
			this.AddMobileDialog("SizeMargins", typeof(SizeMarginsDialog));
			this.AddMobileDialog("Border", typeof(BorderDialog));
		}

		// Token: 0x06009638 RID: 38456 RVA: 0x00219898 File Offset: 0x00217A98
		private void AddMobileDialog(string name, Type dialogType)
		{
			this.AddMobileDialog(name, dialogType, name);
		}

		// Token: 0x06009639 RID: 38457 RVA: 0x002198A4 File Offset: 0x00217AA4
		private void AddMobileDialog(string name, Type dialogType, string title)
		{
			DialogDefinition dialogDefinition = new DialogDefinition(dialogType, new DialogParameters())
			{
				Title = title,
				Modal = false,
				VisibleTitlebar = false
			};
			this.FillEditorPropertiesInDialogDefinition(dialogDefinition);
			this.AddDialogDefinition(name, dialogDefinition);
		}

		// Token: 0x0600963A RID: 38458 RVA: 0x002198E4 File Offset: 0x00217AE4
		private void AddMobileDialog(string name, Type dialogType, FileManagerDialogParameters parameters)
		{
			DialogDefinition dialogDefinition = new DialogDefinition(dialogType, parameters)
			{
				Title = name,
				Modal = false,
				VisibleTitlebar = false
			};
			this.FillEditorPropertiesInDialogDefinition(dialogDefinition);
			this.AddDialogDefinition(name, dialogDefinition);
		}

		// Token: 0x0600963B RID: 38459 RVA: 0x0021991F File Offset: 0x00217B1F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DialogDefinition GetDialogDefinition(string dialogName)
		{
			return this.DialogDefinitions[dialogName];
		}

		// Token: 0x0600963C RID: 38460 RVA: 0x0021992D File Offset: 0x00217B2D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void RemoveDialogDefinition(string dialogName)
		{
			if (this.DialogDefinitions.ContainsKey(dialogName))
			{
				this.DialogDefinitions.Remove(dialogName);
			}
		}

		// Token: 0x0600963D RID: 38461 RVA: 0x0021994A File Offset: 0x00217B4A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void AddDialogDefinition(string dialogName, DialogDefinition dialogDefinition)
		{
			this.DialogDefinitions[dialogName] = dialogDefinition;
		}

		// Token: 0x0600963E RID: 38462 RVA: 0x0021995C File Offset: 0x00217B5C
		public RadEditor()
		{
			this.FileDeleteEvent = new object();
			this.ExportContentEvent = new object();
			this.ImportContentEvent = new object();
			this.FileUploadEvent = new object();
			base..ctor();
			this._trackChangesAdapter = new TrackChangesXmlAdapter(this);
			this._pdfExportTemplate = new ApocPdfGenerator(this);
			this._rtfExportTemplate = new HtmlToRtfGenerator(this)
			{
				DplExportProxy = new DplExportProxy()
			};
			this._docxExportTemplate = new HtmlToDocxGenerator(this)
			{
				DplExportProxy = new DplExportProxy()
			};
			this._markdownExportTemplate = new HtmlToMarkdownGenerator(this);
			this._cssExpressionSanitizer = new CssExpressionSanitizer();
			this._domEventsSanitizer = new DomEventsSanitizer();
		}

		// Token: 0x0600963F RID: 38463 RVA: 0x00219A10 File Offset: 0x00217C10
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.CreateDialogOpener();
			this.InitiliazeDialogOpener();
			if (string.IsNullOrEmpty(this.ToolProviderID))
			{
				this.Controls.Add(this._dialogOpener);
			}
			this.CreateRibbonBarControl();
			this.Controls.Add(this.FindReplaceOverlay);
			if (this.EnableRadContextMenu())
			{
				this.CreateRadContextMenuControl();
			}
		}

		// Token: 0x06009640 RID: 38464 RVA: 0x00219A72 File Offset: 0x00217C72
		private void CreateDialogOpener()
		{
			this._dialogOpener = new RadDialogOpener();
			this._dialogOpener.ID = "dialogOpener";
		}

		// Token: 0x06009641 RID: 38465 RVA: 0x00219A8F File Offset: 0x00217C8F
		private void CreateRibbonBarControl()
		{
			this._ribbonbar = new RadRibbonBar();
			this._ribbonbar.ID = "RibbonBar";
			this._ribbonbar.Visible = this.isRibbonBarVisible;
			this.UpdateChildControlProperties(this._ribbonbar);
		}

		// Token: 0x06009642 RID: 38466 RVA: 0x00219ACC File Offset: 0x00217CCC
		private void CreateRadContextMenuControl()
		{
			this._radContextMenu = new RadContextMenu();
			this._radContextMenu.ID = "RadContextMenu";
			this._radContextMenu.CssClass = "reContextMenu";
			this._radContextMenu.EnableViewState = false;
			this._radContextMenu.EnableImageSprites = true;
			this._radContextMenu.EnableSelection = true;
			this.UpdateChildControlProperties(this._radContextMenu);
			this.Controls.Add(this._radContextMenu);
		}

		// Token: 0x06009643 RID: 38467 RVA: 0x00219B48 File Offset: 0x00217D48
		private void CreateRibbonbarResourcesHolder()
		{
			bool flag = this.ResolvedRenderMode == RenderMode.Lightweight;
			if (flag)
			{
				this._ribbonbarResourcesHolder = new EditorLiteRibbonBarResourcesHolder();
			}
			else
			{
				this._ribbonbarResourcesHolder = new EditorRibbonBarResourcesHolder();
			}
			this._ribbonbarResourcesHolder.ID = "EditorRibbonBarResourcesHolder";
			this._ribbonbarResourcesHolder.Visible = (this.ToolbarMode == EditorToolbarMode.RibbonBar);
		}

		// Token: 0x06009644 RID: 38468 RVA: 0x00219BA0 File Offset: 0x00217DA0
		private void AddRibbonBarToControlsTree()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes.Add("class", "reRibbonBarWrapper");
			htmlGenericControl.Controls.Add(this._ribbonbar);
			htmlGenericControl.Visible = this.isRibbonBarVisible;
			this.Controls.Add(htmlGenericControl);
		}

		// Token: 0x06009645 RID: 38469 RVA: 0x00219BF6 File Offset: 0x00217DF6
		private void SetRenderModeChildRadControls()
		{
			this.SetRenderModeToChildControl(this._dialogOpener);
			this.SetRenderModeToChildControl(this._ribbonbar);
			if (this._radContextMenu != null)
			{
				this.SetRenderModeToChildControl(this._radContextMenu);
			}
		}

		// Token: 0x06009646 RID: 38470 RVA: 0x00219C24 File Offset: 0x00217E24
		private void SetRenderModeToChildControl(ISkinnableControl control)
		{
			if (control != null)
			{
				control.RenderMode = this.RenderMode;
			}
		}

		// Token: 0x06009647 RID: 38471 RVA: 0x00219C35 File Offset: 0x00217E35
		private void UpdateChildControlProperties(ISkinnableControl control)
		{
			if (control == null)
			{
				return;
			}
			control.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
			control.EnableEmbeddedScripts = this.EnableEmbeddedScripts;
			control.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
			if (base.IsSkinSet)
			{
				control.Skin = base.RuntimeSkin;
			}
		}

		// Token: 0x06009648 RID: 38472 RVA: 0x00219C74 File Offset: 0x00217E74
		private bool _editorPageHasRadScriptManager()
		{
			if (this.Page != null)
			{
				ScriptManager current = ScriptManager.GetCurrent(this.Page);
				if (current != null && current is RadScriptManager)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009649 RID: 38473 RVA: 0x00219CA4 File Offset: 0x00217EA4
		protected virtual void ClearCollections()
		{
			this.ContextMenus.Clear();
			this.CssClasses.Clear();
			this.CssFiles.Clear();
			this.FontNames.Clear();
			this.FontSizes.Clear();
			this.Languages.Clear();
			this.Links.Clear();
			this.Modules.Clear();
			this.Paragraphs.Clear();
			this.Snippets.Clear();
			this.Symbols.Clear();
			this.Tools.Clear();
			this.HeaderTools.Clear();
		}

		// Token: 0x0600964A RID: 38474 RVA: 0x00219D40 File Offset: 0x00217F40
		public virtual void LoadToolsFile(XmlDocument doc)
		{
			this._toolsFileContent = doc;
			this.EnsureToolsFileLoaded();
		}

		// Token: 0x0600964B RID: 38475 RVA: 0x00219D4F File Offset: 0x00217F4F
		private void ResetToolsFileContent()
		{
			this._toolsFileContent = null;
			this._toolsFileLoaded = false;
		}

		// Token: 0x0600964C RID: 38476 RVA: 0x00219D5F File Offset: 0x00217F5F
		public virtual void EnsureToolsFileLoaded()
		{
			if (!this._toolsFileLoaded)
			{
				this.LoadToolsFile(false);
			}
		}

		// Token: 0x0600964D RID: 38477 RVA: 0x00219D70 File Offset: 0x00217F70
		protected virtual void LoadToolsFile(bool loadOnlyEmptyCollections)
		{
			if (!string.IsNullOrEmpty(this.ToolProviderID))
			{
				return;
			}
			if (loadOnlyEmptyCollections && this.Tools.Count > 0)
			{
				return;
			}
			if ((loadOnlyEmptyCollections && this.Colors.Count == 0) || !loadOnlyEmptyCollections)
			{
				this.ToolsFileLoader.LoadColors(this.Colors);
			}
			if (this.initialized)
			{
				this.ToolsFileLoader.LoadContextMenus(this.ContextMenus);
			}
			if ((loadOnlyEmptyCollections && this.CssClasses.Count == 0) || !loadOnlyEmptyCollections)
			{
				this.ToolsFileLoader.LoadCssClasses(this.CssClasses);
			}
			if ((loadOnlyEmptyCollections && this.CssFiles.Count == 0) || !loadOnlyEmptyCollections)
			{
				this.ToolsFileLoader.LoadCssFiles(this.CssFiles);
			}
			if ((loadOnlyEmptyCollections && this.FontNames.Count == 0) || !loadOnlyEmptyCollections)
			{
				this.ToolsFileLoader.LoadFontNames(this.FontNames);
			}
			if ((loadOnlyEmptyCollections && this.FontSizes.Count == 0) || !loadOnlyEmptyCollections)
			{
				this.ToolsFileLoader.LoadFontSizes(this.FontSizes);
			}
			if ((loadOnlyEmptyCollections && this.Languages.Count == 0) || !loadOnlyEmptyCollections)
			{
				this.ToolsFileLoader.LoadLanguages(this.Languages);
			}
			if ((loadOnlyEmptyCollections && this.Links.Count == 0) || !loadOnlyEmptyCollections)
			{
				this.ToolsFileLoader.LoadLinks(this.Links);
			}
			if ((loadOnlyEmptyCollections && this.Modules.Count == 0) || !loadOnlyEmptyCollections)
			{
				this.ToolsFileLoader.LoadModules(this.Modules);
			}
			if ((loadOnlyEmptyCollections && this.Paragraphs.Count == 0) || !loadOnlyEmptyCollections)
			{
				this.ToolsFileLoader.LoadParagraphs(this.Paragraphs);
			}
			if ((loadOnlyEmptyCollections && this.FormatSets.Count == 0) || !loadOnlyEmptyCollections)
			{
				this.ToolsFileLoader.LoadFormatSets(this.FormatSets);
			}
			if ((loadOnlyEmptyCollections && this.RealFontSizes.Count == 0) || !loadOnlyEmptyCollections)
			{
				this.ToolsFileLoader.LoadRealFontSizes(this.RealFontSizes);
			}
			if ((loadOnlyEmptyCollections && this.Snippets.Count == 0) || !loadOnlyEmptyCollections)
			{
				this.ToolsFileLoader.LoadSnippets(this.Snippets);
			}
			if ((loadOnlyEmptyCollections && this.Symbols.Count == 0) || !loadOnlyEmptyCollections)
			{
				this.ToolsFileLoader.LoadSymbols(this.Symbols);
			}
			if ((loadOnlyEmptyCollections && this.Tools.Count == 0) || !loadOnlyEmptyCollections)
			{
				this.ToolsFileLoader.LoadTools(this.Tools);
			}
			this._toolsFileLoaded = true;
		}

		// Token: 0x0600964E RID: 38478 RVA: 0x00219FB5 File Offset: 0x002181B5
		internal void LoadToolsFile()
		{
			this.LoadToolsFile(true);
		}

		// Token: 0x0600964F RID: 38479 RVA: 0x00219FBE File Offset: 0x002181BE
		private void ReloadTools()
		{
			this.ResetToolsFileContent();
			this.LoadToolsFile(false);
		}

		// Token: 0x06009650 RID: 38480 RVA: 0x00219FCD File Offset: 0x002181CD
		protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			if (base.IsEnabled)
			{
				return base.GetScriptDescriptors();
			}
			return new List<ScriptDescriptor>();
		}

		// Token: 0x06009651 RID: 38481 RVA: 0x00219FE4 File Offset: 0x002181E4
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			if (this.EnableTextareaMode)
			{
				List<ScriptReference> list = new List<ScriptReference>();
				string fullName = typeof(RadEditor).Assembly.FullName;
				list.Add(new ScriptReference("Telerik.Web.UI.Common.Core.js", fullName));
				list.Add(new ScriptReference("Telerik.Web.UI.Editor.TextAreaRadEditor.js", fullName));
				return list;
			}
			if (base.IsEnabled && this.EnableEmbeddedScripts)
			{
				List<ScriptReference> list2 = new List<ScriptReference>(base.GetScriptReferences());
				list2.AddRange(this.ToolAdapter.GetScriptReferences());
				string[] array = new string[]
				{
					"RadEditorStatistics",
					"RadEditorDomInspector",
					"RadEditorNodeInspector",
					"RadEditorHtmlInspector",
					"RadEditorTrackChangesInfo"
				};
				string fullName2 = typeof(RadEditor).Assembly.FullName;
				if (this.EnableComments || this.EnableTrackChanges)
				{
					list2.Add(new ScriptReference("Telerik.Web.UI.Editor.TrackChanges.js", fullName2));
				}
				bool flag = this.EnableComments || this.EnableTrackChanges;
				foreach (object obj in this.Modules)
				{
					EditorModule editorModule = (EditorModule)obj;
					if (Array.IndexOf<string>(array, editorModule.Name) > -1)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					list2.Add(new ScriptReference("Telerik.Web.UI.Editor.Modules.js", fullName2));
				}
				if (this.isInsertImageEnabled || this.isInsertLinkEnabled || this.isInsertTableEnabled)
				{
					list2.Add(new ScriptReference("Telerik.Web.UI.Editor.RadEditor.LightDialogsController.js", fullName2));
				}
				return list2;
			}
			return new List<ScriptReference>();
		}

		// Token: 0x06009652 RID: 38482 RVA: 0x0021A198 File Offset: 0x00218398
		protected void EnsureEditorToolType()
		{
			if (this._tools == null)
			{
				return;
			}
			this.isInsertLinkEnabled = false;
			this.isInsertImageEnabled = false;
			this.isInsertTableEnabled = false;
			foreach (object obj in this._tools)
			{
				EditorToolGroup editorToolGroup = (EditorToolGroup)obj;
				for (int i = 0; i < editorToolGroup.Tools.Count; i++)
				{
					EditorToolBase editorToolBase = editorToolGroup.Tools[i];
					EditorTool editorTool = editorToolBase as EditorTool;
					if (editorTool != null)
					{
						string text = editorTool.Name.ToLowerInvariant();
						string key;
						switch (key = text)
						{
						case "zoom":
						case "insertcustomlink":
						case "formatblock":
						case "formatsets":
						case "fontsize":
						case "fontname":
						case "realfontsize":
						case "applyclass":
							editorToolBase.Type = EditorToolType.DropDown;
							break;
						case "undo":
						case "redo":
						case "modulemanager":
						case "insertsnippet":
						case "forecolor":
						case "backcolor":
						case "insertsymbol":
							editorToolBase.Type = EditorToolType.SplitButton;
							break;
						case "ajaxspellcheck":
							if (this._languages != null && this._languages.Count > 0)
							{
								editorToolBase.Type = EditorToolType.SplitButton;
							}
							else
							{
								editorToolBase.Type = EditorToolType.Button;
							}
							break;
						case "inserttable":
						case "inserttableitems":
						case "deletetableitems":
						case "mergesplitcells":
						case "formatstripper":
						case "insertformelement":
						case "pastestrip":
						case "formatpainter":
						{
							EditorToolStrip editorToolStrip = new EditorToolStrip(editorTool);
							foreach (object obj2 in editorToolBase.Attributes.Keys)
							{
								string key2 = (string)obj2;
								if (editorToolStrip.Attributes[key2] == null)
								{
									editorToolStrip.Attributes.Add(key2, editorToolBase.Attributes[key2]);
								}
								else
								{
									editorToolStrip.Attributes[key2] = editorToolBase.Attributes[key2];
								}
							}
							editorToolGroup.Tools.RemoveAt(i);
							editorToolGroup.Tools.Insert(i, editorToolStrip);
							if (text == "inserttable")
							{
								editorToolStrip.Tools.AddRange(new string[]
								{
									"TableWizard",
									"InsertRowAbove",
									"InsertRowBelow",
									"DeleteRow",
									"InsertColumnLeft",
									"InsertColumnRight",
									"DeleteColumn",
									"MergeColumns",
									"MergeRows",
									"SplitCellHorizontal",
									"SplitCell",
									"DeleteCell",
									"SetTableProperties"
								});
							}
							else if (text == "inserttableitems")
							{
								editorToolStrip.Tools.AddRange(new string[]
								{
									"InsertRowAbove",
									"InsertRowBelow",
									"InsertColumnLeft",
									"InsertColumnRight"
								});
							}
							else if (text == "deletetableitems")
							{
								editorToolStrip.Tools.AddRange(new string[]
								{
									"DeleteRow",
									"DeleteColumn",
									"DeleteCell"
								});
							}
							else if (text == "mergesplitcells")
							{
								editorToolStrip.Tools.AddRange(new string[]
								{
									"MergeRows",
									"MergeColumns",
									"SplitCellHorizontal",
									"SplitCell"
								});
							}
							else if (text == "insertformelement")
							{
								editorToolStrip.Tools.AddRange(new string[]
								{
									"InsertFormForm",
									"InsertFormButton",
									"InsertFormCheckbox",
									"InsertFormHidden",
									"InsertFormPassword",
									"InsertFormRadio",
									"InsertFormReset",
									"InsertFormSelect",
									"InsertFormSubmit",
									"InsertFormTextarea",
									"InsertFormText"
								});
							}
							else if (text == "formatstripper")
							{
								editorToolStrip.Tools.AddRange(new string[]
								{
									"StripAll",
									"StripCss",
									"StripFont",
									"StripSpan",
									"StripWord"
								});
							}
							else if (text == "pastestrip")
							{
								editorToolStrip.Tools.AddRange(new string[]
								{
									"Paste",
									"PasteFromWord",
									"PasteFromWordNoFontsNoSizes",
									"PastePlainText",
									"PasteAsHtml",
									"PasteHtml"
								});
							}
							else if (text == "formatpainter")
							{
								editorToolStrip.Tools.AddRange(new string[]
								{
									"FormatPainterCopy",
									"FormatPainterApply",
									"FormatPainterClear"
								});
							}
							break;
						}
						case "insertlink":
							this.isInsertLinkEnabled = true;
							break;
						case "insertimage":
							this.isInsertImageEnabled = true;
							break;
						case "inserttablelight":
							this.isInsertTableEnabled = true;
							break;
						}
					}
				}
			}
		}

		// Token: 0x06009653 RID: 38483 RVA: 0x0021A898 File Offset: 0x00218A98
		internal void ForceEditorToolType()
		{
			this.EnsureEditorToolType();
		}

		// Token: 0x17002F6E RID: 12142
		// (get) Token: 0x06009654 RID: 38484 RVA: 0x0021A8A0 File Offset: 0x00218AA0
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.Renderer.TagKey;
			}
		}

		// Token: 0x17002F6F RID: 12143
		// (get) Token: 0x06009655 RID: 38485 RVA: 0x0021A8AD File Offset: 0x00218AAD
		private ToolsFileLoader ToolsFileLoader
		{
			get
			{
				if (this._toolsFileLoader == null)
				{
					this._toolsFileLoader = new ToolsFileLoader(this);
				}
				return this._toolsFileLoader;
			}
		}

		// Token: 0x06009656 RID: 38486 RVA: 0x0021A8CC File Offset: 0x00218ACC
		private string GetXmlFilePath(string path)
		{
			if (path.StartsWith("http://") || path.StartsWith("https://"))
			{
				return path;
			}
			if (this.Context == null)
			{
				return "";
			}
			string text = this.Context.Request.MapPath(path);
			if (File.Exists(text))
			{
				return text;
			}
			return path;
		}

		// Token: 0x17002F70 RID: 12144
		// (get) Token: 0x06009657 RID: 38487 RVA: 0x0021A920 File Offset: 0x00218B20
		internal XmlDocument ToolsFileContent
		{
			get
			{
				if (this._toolsFileContent == null)
				{
					this._toolsFileContent = new XmlDocument();
					if (!base.DesignMode)
					{
						if (string.IsNullOrEmpty(this.ToolsFile))
						{
							this._toolsFileContent.Load(new XmlTextReader(typeof(RadEditor).Assembly.GetManifestResourceStream(this.GetToolsFilePath())));
						}
						else
						{
							this._toolsFileContent.Load(this.GetXmlFilePath(this.ToolsFile));
						}
					}
				}
				return this._toolsFileContent;
			}
		}

		// Token: 0x06009658 RID: 38488 RVA: 0x0021A99E File Offset: 0x00218B9E
		private string GetToolsFilePath()
		{
			if (this.isRibbonBarVisible)
			{
				return "Telerik.Web.UI.Editor.Resources.RibbonBarTools.xml";
			}
			if (this.EditType == EditorEditType.Inline)
			{
				return "Telerik.Web.UI.Editor.Resources.InlineEditModeTools.xml";
			}
			if (this.ResolvedRenderMode == RenderMode.Mobile)
			{
				return "Telerik.Web.UI.Editor.Resources.PhoneLayoutTools.xml";
			}
			return "Telerik.Web.UI.Editor.Resources.ToolsFile.xml";
		}

		// Token: 0x17002F71 RID: 12145
		// (get) Token: 0x06009659 RID: 38489 RVA: 0x0021A9D4 File Offset: 0x00218BD4
		internal EditorContextMenuCollection DefaultContextMenus
		{
			get
			{
				if (this._defaultContextMenus == null)
				{
					this._defaultContextMenus = new EditorContextMenuCollection();
					if (!base.DesignMode)
					{
						EditorContextMenu editorContextMenu = new EditorContextMenu();
						editorContextMenu.TagName = "TABLE";
						editorContextMenu.Tools.AddRange(new string[]
						{
							"ToggleTableBorder",
							"SetTableProperties",
							"DeleteTable"
						});
						this._defaultContextMenus.Add(editorContextMenu);
						EditorContextMenu editorContextMenu2 = new EditorContextMenu();
						editorContextMenu2.TagName = "TD";
						if (this.EnableRadContextMenu())
						{
							editorContextMenu2.Tools.Add(new EditorContextMenuTool("Row")
							{
								IconCssClass = "reInsertRowAbove"
							});
							editorContextMenu2.Tools.Add(new EditorContextMenuTool("Column")
							{
								IconCssClass = "reInsertColumnLeft"
							});
							editorContextMenu2.Tools.Add(new EditorContextMenuTool("Cell")
							{
								IconCssClass = "reMergeColumns"
							});
							editorContextMenu2.Tools.Add(new EditorTool("SetCellProperties"));
							editorContextMenu2.Tools.Add(new EditorTool("SetTableProperties"));
							(editorContextMenu2.Tools[0] as EditorContextMenuTool).Tools.AddRange(new string[]
							{
								"InsertRowAbove",
								"InsertRowBelow",
								"DeleteRow"
							});
							(editorContextMenu2.Tools[1] as EditorContextMenuTool).Tools.AddRange(new string[]
							{
								"InsertColumnLeft",
								"InsertColumnRight",
								"DeleteColumn"
							});
							(editorContextMenu2.Tools[2] as EditorContextMenuTool).Tools.AddRange(new string[]
							{
								"MergeColumns",
								"MergeRows",
								"SplitCellHorizontal",
								"SplitCell",
								"DeleteCell"
							});
						}
						else
						{
							editorContextMenu2.Tools.AddRange(new string[]
							{
								"InsertRowAbove",
								"InsertRowBelow",
								"DeleteRow",
								"InsertColumnLeft",
								"InsertColumnRight",
								"DeleteColumn",
								"MergeColumns",
								"MergeRows",
								"SplitCellHorizontal",
								"SplitCell",
								"DeleteCell",
								"SetCellProperties",
								"SetTableProperties"
							});
						}
						this._defaultContextMenus.Add(editorContextMenu2);
						EditorContextMenu editorContextMenu3 = new EditorContextMenu();
						editorContextMenu3.TagName = "IMG";
						editorContextMenu3.Tools.AddRange(new string[]
						{
							"SetImageProperties",
							"ImageMapDialog"
						});
						this._defaultContextMenus.Add(editorContextMenu3);
						EditorContextMenu editorContextMenu4 = new EditorContextMenu();
						editorContextMenu4.TagName = "A";
						editorContextMenu4.Tools.AddRange(new string[]
						{
							"SetLinkProperties",
							"OpenLink",
							"Unlink"
						});
						this._defaultContextMenus.Add(editorContextMenu4);
						EditorContextMenu editorContextMenu5 = new EditorContextMenu();
						editorContextMenu5.TagName = "SELECT";
						editorContextMenu5.Tools.AddRange(new string[]
						{
							"InsertFormSelect"
						});
						this._defaultContextMenus.Add(editorContextMenu5);
						EditorContextMenu editorContextMenu6 = new EditorContextMenu();
						editorContextMenu6.TagName = "*";
						editorContextMenu6.Tools.AddRange(new string[]
						{
							"Cut",
							"Copy",
							"Paste",
							"PasteFromWord",
							"PastePlainText",
							"PasteAsHtml",
							"PasteHtml"
						});
						this._defaultContextMenus.Add(editorContextMenu6);
						if (this.EnableComments)
						{
							EditorContextMenu editorContextMenu7 = new EditorContextMenu();
							editorContextMenu7.TagName = "TrackChangeComment";
							editorContextMenu7.Tools.AddRange(new string[]
							{
								"RemoveComment"
							});
							this._defaultContextMenus.Add(editorContextMenu7);
							EditorContextMenu editorContextMenu8 = new EditorContextMenu();
							editorContextMenu8.TagName = "TrackChangeDefault";
							editorContextMenu8.Tools.AddRange(new string[]
							{
								"AddComment"
							});
							this._defaultContextMenus.Add(editorContextMenu8);
						}
						if (this.EnableTrackChanges)
						{
							EditorContextMenu editorContextMenu9 = new EditorContextMenu();
							editorContextMenu9.TagName = "TrackChangeFormat";
							editorContextMenu9.Tools.AddRange(new string[]
							{
								"AcceptTrackChange",
								"RejectTrackChange"
							});
							this._defaultContextMenus.Add(editorContextMenu9);
						}
					}
				}
				return this._defaultContextMenus;
			}
		}

		// Token: 0x17002F72 RID: 12146
		// (get) Token: 0x0600965A RID: 38490 RVA: 0x0021AE9C File Offset: 0x0021909C
		protected override string CssClassFormatString
		{
			get
			{
				if (this.originalEnabled)
				{
					string text = this.EnableTextareaMode ? "RadEditorTextArea" : this.Renderer.CssClassFormatString;
					if (this.IsInAccessibleMode)
					{
						text += " reAccessible";
					}
					return text;
				}
				return string.Empty;
			}
		}

		// Token: 0x0600965B RID: 38491 RVA: 0x0021AEE7 File Offset: 0x002190E7
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.originalEnabled = this.Enabled;
			this.Enabled = true;
			base.AddAttributesToRender(writer);
			this.Renderer.AddAttributesToRender(writer);
			this.Enabled = this.originalEnabled;
		}

		// Token: 0x17002F73 RID: 12147
		// (get) Token: 0x0600965C RID: 38492 RVA: 0x0021AF1B File Offset: 0x0021911B
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600965D RID: 38493 RVA: 0x0021AF1E File Offset: 0x0021911E
		protected override IRenderer CreateControlRenderer()
		{
			return RendererFactory.GetRenderer(this);
		}

		// Token: 0x0600965E RID: 38494 RVA: 0x0021AF26 File Offset: 0x00219126
		protected override void RegisterScriptControl()
		{
			if (base.IsEnabled)
			{
				base.RegisterScriptControl();
			}
		}

		// Token: 0x0600965F RID: 38495 RVA: 0x0021AF36 File Offset: 0x00219136
		protected override void RegisterCssReferences()
		{
			if (base.IsEnabled && !this.EnableTextareaMode)
			{
				base.RegisterCssReferences();
			}
		}

		// Token: 0x06009660 RID: 38496 RVA: 0x0021AF4E File Offset: 0x0021914E
		protected override void RegisterScriptDescriptors()
		{
			if (base.IsEnabled)
			{
				base.RegisterScriptDescriptors();
			}
		}

		// Token: 0x06009661 RID: 38497 RVA: 0x0021AF60 File Offset: 0x00219160
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (base.IsEnabled && !this.EnableTextareaMode)
			{
				if (this.Width.ToString().Equals(this.GetDefaultWidth(), StringComparison.OrdinalIgnoreCase))
				{
					writer.AddStyleAttribute("width", this.Width.ToString(CultureInfo.InvariantCulture));
				}
				if (this.Height.ToString().Equals(this.GetDefaultHeight(), StringComparison.OrdinalIgnoreCase))
				{
					writer.AddStyleAttribute("height", this.Height.ToString(CultureInfo.InvariantCulture));
				}
			}
			base.RenderBeginTag(writer);
		}

		// Token: 0x06009662 RID: 38498 RVA: 0x0021B00C File Offset: 0x0021920C
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (!base.IsEnabled)
			{
				writer.Write(this.Content);
				return;
			}
			if (this.EnableTextareaMode)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
				writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}{1}", this.ClientID, "TextArea"));
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.Width.ToString(CultureInfo.InvariantCulture));
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString(CultureInfo.InvariantCulture));
				writer.RenderBeginTag(HtmlTextWriterTag.Textarea);
				writer.Write(ContentEncoder.Encode(this.Content));
				writer.RenderEndTag();
				return;
			}
			base.RenderContents(writer);
			this.Renderer.RenderContents(writer);
			if (this.isInsertLinkEnabled)
			{
				this.RenderLightDialog(writer, "InsertLink");
			}
			if (this.isInsertImageEnabled)
			{
				this.RenderLightDialog(writer, "InsertImage");
			}
			if (this.isInsertTableEnabled)
			{
				string name = (this.ResolvedRenderMode == RenderMode.Lightweight) ? "InsertTableLight_Lite" : "InsertTableLight";
				this.RenderLightDialog(writer, name);
			}
		}

		// Token: 0x06009663 RID: 38499 RVA: 0x0021B11B File Offset: 0x0021931B
		public override void RenderClientStateField(HtmlTextWriter writer)
		{
			if (base.IsEnabled)
			{
				base.RenderClientStateField(writer);
			}
		}

		// Token: 0x06009664 RID: 38500 RVA: 0x0021B12C File Offset: 0x0021932C
		private void RenderCaption(HtmlTextWriter writer, string text)
		{
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Caption);
			writer.Write(text);
			writer.RenderEndTag();
		}

		// Token: 0x06009665 RID: 38501 RVA: 0x0021B150 File Offset: 0x00219350
		private void RenderTh(HtmlTextWriter writer, string text, string scope)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Scope, scope);
			writer.RenderBeginTag(HtmlTextWriterTag.Th);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(text);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06009666 RID: 38502 RVA: 0x0021B180 File Offset: 0x00219380
		protected virtual void RenderBottomZone(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reBottomTable");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			if (!base.DesignMode)
			{
				HttpBrowserCapabilities browser = this.Context.Request.Browser;
				if (browser.Browser == "IE")
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				}
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}_BottomTable", this.ClientID));
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			if (!base.DesignMode)
			{
				this.RenderCaption(writer, "It contains RadEditor's Modes/views (HTML, Design and Preview), Statistics and Resizer");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				writer.RenderBeginTag(HtmlTextWriterTag.Thead);
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				this.RenderTh(writer, "Editor Mode buttons", "col");
				this.RenderTh(writer, "Statistics module", "col");
				this.RenderTh(writer, "Editor resizer", "col");
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
			if (this.EditModes == EditModes.Design || this.EditModes == EditModes.Html || this.EditModes == EditModes.Preview)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reEditorModesCell");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			this.RenderEditModes(writer);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reBottomZone");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}{1}", this.ClientID, "Bottom"));
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.Write("&nbsp;");
			writer.RenderEndTag();
			if (this.EnableResize && !this.AutoResizeHeight)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "reResizeCell");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "15px");
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}{1}", this.ClientID, "BottomResizer"));
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.Write("&nbsp;");
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			else
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.Write("&nbsp;");
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderBeginTag(HtmlTextWriterTag.Noscript);
			writer.RenderBeginTag(HtmlTextWriterTag.P);
			writer.Write("RadEditor - please enable JavaScript to use the rich text editor.");
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06009667 RID: 38503 RVA: 0x0021B3D0 File Offset: 0x002195D0
		protected virtual void RenderEditModes(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reEditorModes");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}{1}", this.ClientID, "_ModesWrapper"));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (this.EditModes != EditModes.Design)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Ul);
				if ((this.EditModes & EditModes.Design) > (EditModes)0)
				{
					RadEditor.Render_LI_A_SPAN(writer, this.Localization.Main.RadEditorDesignMode, this.Localization.Main.RadEditorDesignMode, "reMode_design reMode_selected");
				}
				if ((this.EditModes & EditModes.Html) > (EditModes)0)
				{
					RadEditor.Render_LI_A_SPAN(writer, this.Localization.Main.RadEditorHtmlMode, this.Localization.Main.RadEditorHtmlMode, "reMode_html");
				}
				if ((this.EditModes & EditModes.Preview) > (EditModes)0)
				{
					RadEditor.Render_LI_A_SPAN(writer, this.Localization.Main.RadEditorPreviewMode, this.Localization.Main.RadEditorPreviewMode, "reMode_preview");
				}
				writer.RenderEndTag();
			}
			else
			{
				writer.Write(' ');
			}
			writer.RenderEndTag();
		}

		// Token: 0x06009668 RID: 38504 RVA: 0x0021B4DC File Offset: 0x002196DC
		private static void Render_LI_A_SPAN(HtmlTextWriter writer, string anchorTitle, string spanText, string className)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:void(0);");
			writer.AddAttribute(HtmlTextWriterAttribute.Title, anchorTitle);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, className);
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(spanText);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06009669 RID: 38505 RVA: 0x0021B5E8 File Offset: 0x002197E8
		private void RenderLightDialog(HtmlTextWriter writer, string name)
		{
			string text = string.Empty;
			if (this.ExternalDialogsPath.Length > 0)
			{
				try
				{
					string path = this.Page.Server.MapPath(this.ExternalDialogsPath + name + ".ascx");
					if (File.Exists(path))
					{
						using (StreamReader streamReader = new StreamReader(path))
						{
							text = streamReader.ReadToEnd();
						}
					}
				}
				catch (Exception)
				{
				}
			}
			if (text.Length == 0)
			{
				string format = "Telerik.Web.UI.Editor.DialogControls.{0}.ascx";
				Assembly assembly = typeof(RadEditor).Assembly;
				Encoding utf = Encoding.UTF8;
				using (Stream manifestResourceStream = assembly.GetManifestResourceStream(string.Format(format, name)))
				{
					byte[] array = new byte[manifestResourceStream.Length];
					manifestResourceStream.Read(array, 0, (int)manifestResourceStream.Length);
					text = utf.GetString(array);
				}
			}
			text = Regex.Replace(text, "\\<%@[^%\\<\\>]*%\\>", "");
			string pattern = "[\\s]id\\=(?<br>[\\\"\\'])(?<id>[^\\\"\\']+)\\k<br>";
			string text2 = Regex.Replace(Regex.Escape(text), pattern, delegate(Match match)
			{
				string value = match.Groups["id"].Value;
				return string.Concat(new string[]
				{
					" id=\"",
					this.ClientID,
					"_",
					value,
					"\""
				});
			}, RegexOptions.IgnoreCase);
			string pattern2 = "[\\s]for\\=(?<br>[\\\"\\'])(?<for>[^\\\"\\']+)\\k<br>";
			text2 = Regex.Replace(text2, pattern2, delegate(Match match)
			{
				string value = match.Groups["for"].Value;
				return string.Concat(new string[]
				{
					" for=\"",
					this.ClientID,
					"_",
					value,
					"\""
				});
			}, RegexOptions.IgnoreCase);
			text = Regex.Unescape(text2);
			Dictionary<string, string> lightDialogsStrings = EditorStrings.getLightDialogsStrings(this);
			foreach (KeyValuePair<string, string> keyValuePair in lightDialogsStrings)
			{
				text = text.Replace("[" + keyValuePair.Key + "]", keyValuePair.Value);
			}
			writer.Write(text);
		}

		// Token: 0x0600966A RID: 38506 RVA: 0x0021B7B4 File Offset: 0x002199B4
		private string GetSerializedSpellDialogParameters()
		{
			DialogParameters dialogParameters = this.SpellCheckSettings.DialogParameters;
			SpellDialogParameters spellDialogParameters = new SpellDialogParameters(dialogParameters);
			spellDialogParameters.AjaxUrl = this.SpellCheckSettings.AjaxUrl;
			HttpRequest request = HttpContext.Current.Request;
			string dictionaryPath = spellDialogParameters.DictionaryPath;
			if ((string.IsNullOrEmpty(dictionaryPath) || dictionaryPath.StartsWith("/") || dictionaryPath.StartsWith("~") || !Directory.Exists(dictionaryPath)) && request != null)
			{
				try
				{
					spellDialogParameters.DictionaryPath = request.MapPath(dictionaryPath);
				}
				catch (Exception)
				{
				}
			}
			return dialogParameters.Serialize();
		}

		// Token: 0x0600966B RID: 38507 RVA: 0x0021B854 File Offset: 0x00219A54
		private string GetSerializedSpellCheckParameters(JavaScriptSerializer serializer)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["configuration"] = this.GetSerializedSpellDialogParameters();
			dictionary["url"] = this.SpellCheckSettings.AjaxUrl;
			if (this._languages == null || (this._languages != null && this._languages.Count == 0))
			{
				dictionary["language"] = this.SpellCheckSettings.DictionaryLanguage;
			}
			return serializer.Serialize(dictionary);
		}

		// Token: 0x0600966C RID: 38508 RVA: 0x0021B8C8 File Offset: 0x00219AC8
		internal string GetDefaultWidth()
		{
			if (this.EditType == EditorEditType.Inline)
			{
				return this.GetDefaultWidthInInlineEditMode();
			}
			string result = (this.RenderMode == RenderMode.Lightweight) ? "810px" : "680px";
			if (base.RuntimeSkin == "Silk" || base.RuntimeSkin == "Glow")
			{
				return "850px";
			}
			if (!(base.RuntimeSkin == "MetroTouch") && !(base.RuntimeSkin == "BlackMetroTouch") && !(base.RuntimeSkin == "Material") && !(base.RuntimeSkin == "Bootstrap"))
			{
				return result;
			}
			return "1100px";
		}

		// Token: 0x0600966D RID: 38509 RVA: 0x0021B974 File Offset: 0x00219B74
		private string GetDefaultWidthInInlineEditMode()
		{
			string result = "36.85em";
			string runtimeSkin;
			if ((runtimeSkin = base.RuntimeSkin) != null)
			{
				if (!(runtimeSkin == "Silk") && !(runtimeSkin == "Glow"))
				{
					if (!(runtimeSkin == "MetroTouch") && !(runtimeSkin == "BlackMetroTouch"))
					{
						if (runtimeSkin == "Bootstrap")
						{
							if (this.RenderMode == RenderMode.Classic)
							{
								return "586px";
							}
							return "43em";
						}
					}
					else
					{
						if (this.RenderMode == RenderMode.Classic)
						{
							return "675px";
						}
						return result;
					}
				}
				else
				{
					if (this.RenderMode == RenderMode.Classic)
					{
						return "531px";
					}
					return result;
				}
			}
			if (this.RenderMode == RenderMode.Classic)
			{
				return "440px";
			}
			return result;
		}

		// Token: 0x0600966E RID: 38510 RVA: 0x0021BA1B File Offset: 0x00219C1B
		private string GetDefaultHeight()
		{
			if (!(base.RuntimeSkin == "MetroTouch") && !(base.RuntimeSkin == "BlackMetroTouch"))
			{
				return "400px";
			}
			return "600px";
		}

		// Token: 0x0600966F RID: 38511 RVA: 0x0021BA4C File Offset: 0x00219C4C
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			if (this.EnableTextareaMode)
			{
				return;
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new EditorHeaderToolConverter(this),
				new EditorToolConverter(this),
				new EditorToolGroupConverter(),
				new EditorLinkConverter(),
				new EditorContextMenuConverter(),
				new EditorModuleConverter(),
				new EditorAnimationSettingsConverter(),
				new EditorToolBarAnimationConverter()
			});
			if (!string.IsNullOrEmpty(this.ToolProviderID))
			{
				string text = this.ToolProviderID;
				Control control = ChildControlHelper.FindControlRecursive(this, this.ToolProviderID, null);
				if (control != null)
				{
					text = control.ClientID;
				}
				descriptor.AddProperty("toolProviderID", text);
				descriptor.AddComponentProperty("dialogOpener", text);
			}
			else
			{
				descriptor.AddComponentProperty("dialogOpener", this._dialogOpener.ClientID);
			}
			if (this.FindReplaceOverlay.Visible)
			{
				descriptor.AddComponentProperty("findReplaceOverlay", this.FindReplaceOverlay.ClientID);
			}
			if (this._ribbonbar.Visible)
			{
				descriptor.AddComponentProperty("ribbonBar", this._ribbonbar.ClientID);
			}
			descriptor.AddProperty("skin", base.RuntimeSkin);
			if (this.ContainsTool("AjaxSpellCheck"))
			{
				string webResourceUrl = SkinRegistrar.GetWebResourceUrl(this.Page, typeof(RadEditor), "Telerik.Web.UI.Editor.AjaxSpellCheck.js");
				descriptor.AddProperty("ajaxSpellCheckScriptReference", webResourceUrl);
				descriptor.AddScriptProperty("spellCheckJSON", this.GetSerializedSpellCheckParameters(javaScriptSerializer));
				if (!this.SpellCheckSettings.AllowAddCustom)
				{
					descriptor.AddProperty("spellAllowAddCustom", this.SpellCheckSettings.AllowAddCustom);
				}
			}
			if (this.EnableComments || this.EnableTrackChanges)
			{
				int num = 0;
				while (num < this.Modules.Count && this.Modules[num].Name != "RadEditorTrackChangesInfo")
				{
					num++;
				}
				if (num >= this.Modules.Count)
				{
					this.Modules.Add(new EditorModule("RadEditorTrackChangesInfo", string.Empty));
				}
			}
			if (this.EditType != EditorEditType.Inline && !this.ContainsTool("Undo") && !this.ContainsTool("Redo"))
			{
				descriptor.AddProperty("enableUndoRedo", false);
			}
			EditorTool editorTool = this.FindTool("ApplyClass");
			if (editorTool != null)
			{
				editorTool.Attributes["clearClassText"] = this.Localization.Main.ClearClass;
			}
			descriptor.AddScriptProperty("headerToolsJSON", javaScriptSerializer.Serialize(this.HeaderTools));
			descriptor.AddScriptProperty("toolJSON", javaScriptSerializer.Serialize(this.Tools));
			descriptor.AddScriptProperty("contextMenusJSON", javaScriptSerializer.Serialize(this.ContextMenus.EnabledContextMenus));
			descriptor.AddScriptProperty("modulesJSON", javaScriptSerializer.Serialize(this.Modules));
			descriptor.AddProperty("mozillaFlashOverlayImage", this.GetMozillaFlashOverlayImage());
			descriptor.AddProperty("contentAreaCssFile", this.GetContentAreaCssFileUrl());
			string tableLayoutCssFileUrl = this.GetTableLayoutCssFileUrl();
			if (!string.IsNullOrEmpty(tableLayoutCssFileUrl))
			{
				descriptor.AddProperty("tableLayoutCssFile", tableLayoutCssFileUrl);
			}
			this.SerializeCollections(descriptor, javaScriptSerializer);
			descriptor.AddProperty("headerToolsToolAdapterType", this.HeaderToolsToolAdapter.ClientType);
			descriptor.AddProperty("toolAdapterType", this.ToolAdapter.ClientType);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (!this.Page.ClientScript.IsClientScriptBlockRegistered(typeof(RadEditor), this.Language + "Main"))
			{
				this.Page.ClientScript.RegisterClientScriptBlock(typeof(RadEditor), this.Language + "Main", string.Empty);
			}
			if (!this.Page.ClientScript.IsClientScriptBlockRegistered(typeof(RadEditor), this.Language + "Tools"))
			{
				this.Page.ClientScript.RegisterClientScriptBlock(typeof(RadEditor), this.Language + "Tools", string.Empty);
			}
			if (this.Modules.Count > 0 && !this.Page.ClientScript.IsClientScriptBlockRegistered(typeof(RadEditor), this.Language + "Modules"))
			{
				this.Page.ClientScript.RegisterClientScriptBlock(typeof(RadEditor), this.Language + "Modules", string.Empty);
			}
			this.Localization.addStrings(dictionary, "Main");
			this.Localization.addStrings(dictionary, "Tools");
			if (this.Modules.Count > 0)
			{
				this.Localization.addStrings(dictionary, "Modules");
			}
			if (this.FindReplaceOverlay != null)
			{
				this.AddFindReplaceLocalization(dictionary);
			}
			if (dictionary.Count > 0)
			{
				descriptor.AddScriptProperty("_localization", javaScriptSerializer.Serialize(dictionary));
			}
			if (this.IsInAccessibleMode)
			{
				descriptor.AddProperty("isInAccessibleMode", this.IsInAccessibleMode);
			}
			if (this.EnableComments || this.EnableTrackChanges)
			{
				descriptor.AddProperty("author", this.TrackChangesSettings.Author);
				descriptor.AddProperty("userCssId", this.TrackChangesSettings.UserCssId);
				descriptor.AddProperty("canAcceptTrackChanges", this.TrackChangesSettings.CanAcceptTrackChanges);
			}
			descriptor.AddProperty("_renderMode", this.ResolvedRenderMode);
			this.SerializeAnimationSettings(javaScriptSerializer, descriptor);
			descriptor.AddProperty("useRadContextMenu", this.EnableRadContextMenu());
			if (this._radContextMenu != null)
			{
				descriptor.AddComponentProperty("radContextMenu", this._radContextMenu.ClientID);
			}
		}

		// Token: 0x06009670 RID: 38512 RVA: 0x0021BFF4 File Offset: 0x0021A1F4
		private void SerializeAnimationSettings(JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
			if (this.EditType == EditorEditType.Inline)
			{
				descriptor.AddScriptProperty("_animationSettings", serializer.Serialize(this.AnimationSettings));
			}
		}

		// Token: 0x06009671 RID: 38513 RVA: 0x0021C018 File Offset: 0x0021A218
		private void SerializeCollections(IScriptDescriptor descriptor, JavaScriptSerializer jss)
		{
			if (this._cssFiles != null && this._cssFiles.Count > 0)
			{
				descriptor.AddScriptProperty("cssFiles", this._cssFiles.Serialize(jss));
			}
			if (this._languages != null && this._languages.Count > 0 && (this.ContainsTool("SpellCheck") || this.ContainsTool("AjaxSpellCheck")))
			{
				descriptor.AddScriptProperty("languages", this._languages.Serialize(jss));
			}
			if (this._snippets != null && this._snippets.Count > 0 && this.ContainsTool("InsertSnippet"))
			{
				descriptor.AddScriptProperty("snippets", this._snippets.Serialize(jss));
			}
			if (this._cssClasses != null && this._cssClasses.Count > 0 && this.ContainsTool("ApplyClass"))
			{
				descriptor.AddScriptProperty("cssClasses", this._cssClasses.Serialize(jss));
			}
			if (this._realFontSizes != null && this._realFontSizes.Count > 0 && this.ContainsTool("RealFontSize"))
			{
				descriptor.AddScriptProperty("realFontSizes", this._realFontSizes.Serialize(jss));
			}
			if (this._paragraphs != null && this._paragraphs.Count > 0 && this.ContainsTool("FormatBlock"))
			{
				descriptor.AddScriptProperty("paragraphs", this._paragraphs.Serialize(jss));
			}
			if (this._formatSets != null && this._formatSets.Count > 0 && this.ContainsTool("FormatSets"))
			{
				descriptor.AddScriptProperty("formatSets", this._formatSets.Serialize(jss));
			}
			if (this._fontNames != null && this._fontNames.Count > 0 && this.ContainsTool("FontName"))
			{
				descriptor.AddScriptProperty("fontNames", this._fontNames.Serialize(jss));
			}
			if (this._fontSizes != null && this._fontSizes.Count > 0 && this.ContainsTool("FontSize"))
			{
				descriptor.AddScriptProperty("fontSizes", this._fontSizes.Serialize(jss));
			}
			if (this._symbols != null && this._symbols.Count > 0 && this.ContainsTool("InsertSymbol"))
			{
				descriptor.AddScriptProperty("symbols", this._symbols.Serialize(jss));
			}
			if (this._colors != null && this._colors.Count > 0)
			{
				descriptor.AddScriptProperty("colors", this._colors.Serialize(jss));
			}
			if (this._links != null && this._links.Count > 0 && this.ContainsTool("InsertCustomLink"))
			{
				descriptor.AddScriptProperty("links", jss.Serialize(this._links));
			}
		}

		// Token: 0x06009672 RID: 38514 RVA: 0x0021C2D0 File Offset: 0x0021A4D0
		public EditorTool FindTool(string name)
		{
			foreach (object obj in this.Tools)
			{
				EditorToolGroup editorToolGroup = (EditorToolGroup)obj;
				EditorTool editorTool = editorToolGroup.FindTool(name);
				if (editorTool != null)
				{
					return editorTool;
				}
			}
			return null;
		}

		// Token: 0x06009673 RID: 38515 RVA: 0x0021C338 File Offset: 0x0021A538
		protected bool ContainsTool(string name)
		{
			if (this.FindTool(name) != null)
			{
				return true;
			}
			foreach (object obj in this.ContextMenus)
			{
				EditorContextMenu editorContextMenu = (EditorContextMenu)obj;
				foreach (object obj2 in editorContextMenu.Tools)
				{
					EditorTool editorTool = (EditorTool)obj2;
					if (editorTool.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
			}
			return this.HeaderTools.Contains(name);
		}

		// Token: 0x06009674 RID: 38516 RVA: 0x0021C404 File Offset: 0x0021A604
		internal bool ContainsModule(string name)
		{
			foreach (object obj in this.Modules)
			{
				EditorModule editorModule = (EditorModule)obj;
				if (editorModule.Name == name)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009675 RID: 38517 RVA: 0x0021C46C File Offset: 0x0021A66C
		private static string FixJSEmptyCollections(string jsString)
		{
			if (jsString.Length == 0 || jsString == "[]")
			{
				return "null";
			}
			return jsString;
		}

		// Token: 0x06009676 RID: 38518 RVA: 0x0021C48C File Offset: 0x0021A68C
		private string GetContentAreaCssFileUrl()
		{
			string text = this.ContentAreaCssFile;
			if (string.IsNullOrEmpty(text))
			{
				return SkinRegistrar.GetWebResourceUrl(this.Page, typeof(RadEditor), "Telerik.Web.UI.Skins.Common.EditorContentArea.css");
			}
			if (text.IndexOf('~') > -1 && this.Page.Response != null)
			{
				text = this.Page.Response.ApplyAppPathModifier(text);
			}
			return text;
		}

		// Token: 0x06009677 RID: 38519 RVA: 0x0021C4F0 File Offset: 0x0021A6F0
		private string GetTableLayoutCssFileUrl()
		{
			string text = this.TableLayoutCssFile;
			if (text.IndexOf('~') > -1 && this.Page.Response != null)
			{
				text = this.Page.Response.ApplyAppPathModifier(text);
			}
			return text;
		}

		// Token: 0x06009678 RID: 38520 RVA: 0x0021C52F File Offset: 0x0021A72F
		private string GetMozillaFlashOverlayImage()
		{
			return SkinRegistrar.GetWebResourceUrl(this.Page, typeof(RadEditor), "Telerik.Web.UI.Skins.Common.FlashManager.gif");
		}

		// Token: 0x06009679 RID: 38521 RVA: 0x0021C54C File Offset: 0x0021A74C
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			bool result = base.LoadPostData(postDataKey, postCollection);
			string content = this.Content;
			string text = ContentEncoder.Decode(postCollection[this.UniqueID]);
			if (content == null || !content.Equals(text))
			{
				this.Content = text;
				return true;
			}
			return result;
		}

		// Token: 0x0600967A RID: 38522 RVA: 0x0021C594 File Offset: 0x0021A794
		private static string RemoveScriptBlocks(string initContent)
		{
			string text = initContent;
			if (!string.IsNullOrEmpty(text))
			{
				text = Regex.Replace(text, "<(SCRIPT)([^>]*)/>", "", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "<(SCRIPT)([^>]*)>[\\s\\S]*?</(SCRIPT)([^>]*)>", "", RegexOptions.IgnoreCase);
			}
			return text;
		}

		// Token: 0x0600967B RID: 38523 RVA: 0x0021C5D0 File Offset: 0x0021A7D0
		protected override void RaisePostDataChangedEvent()
		{
			base.RaisePostDataChangedEvent();
			this.OnTextChanged(EventArgs.Empty);
		}

		// Token: 0x0600967C RID: 38524 RVA: 0x0021C5E4 File Offset: 0x0021A7E4
		private void CheckMobileBrowserVersions()
		{
			if (base.DesignMode)
			{
				return;
			}
			bool flag = false;
			string text = string.Empty;
			if (this.Page != null && this.Page.Request != null && this.Page.Request.Browser != null && this.Page.Request.Browser.MobileDeviceModel == "IPhone")
			{
				if (!string.IsNullOrEmpty(this.Page.Request.UserAgent))
				{
					text = this.Page.Request.UserAgent.ToLowerInvariant();
				}
				flag = true;
			}
			else if (HttpContext.Current != null && HttpContext.Current.Request != null)
			{
				if (!string.IsNullOrEmpty(HttpContext.Current.Request.UserAgent))
				{
					text = HttpContext.Current.Request.UserAgent.ToLowerInvariant();
				}
				if ((text.Contains("android") || text.Contains("iphone") || text.Contains("ipad") || text.Contains("ipod")) && text.Contains("safari"))
				{
					flag = true;
				}
			}
			if (flag)
			{
				Match match = Regex.Match(text, "(iphone|ipad|cpu) os (\\d+)_");
				int num = 0;
				if (match != null && match.Success && match.Groups.Count > 2)
				{
					int.TryParse(match.Groups[2].Value, out num);
					if (num >= 5)
					{
						return;
					}
				}
				match = Regex.Match(text, "(android) (\\d+)\\.?");
				if (match != null && match.Success && match.Groups.Count > 2)
				{
					num = 0;
					int.TryParse(match.Groups[2].Value, out num);
					if (num >= 4)
					{
						return;
					}
				}
				this.EnableTextareaMode = true;
			}
		}

		// Token: 0x0600967D RID: 38525 RVA: 0x0021C796 File Offset: 0x0021A996
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.EnsureChildControls();
			this.CheckMobileBrowserVersions();
			this.initialized = true;
			this.ToolsFileLoader.LoadContextMenus(this.ContextMenus);
		}

		// Token: 0x0600967E RID: 38526 RVA: 0x0021C7C4 File Offset: 0x0021A9C4
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.SetDialogConfigurationRenderMode(this._imageManager);
			this.SetDialogConfigurationRenderMode(this._documentManager);
			this.SetDialogConfigurationRenderMode(this._flashManager);
			this.SetDialogConfigurationRenderMode(this._silverlightManager);
			this.SetDialogConfigurationRenderMode(this._mediaManager);
			this.SetDialogConfigurationRenderMode(this._templateManager);
		}

		// Token: 0x0600967F RID: 38527 RVA: 0x0021C820 File Offset: 0x0021AA20
		private void SetDialogConfigurationRenderMode(FileManagerDialogConfiguration dialogConfiguration)
		{
			if (dialogConfiguration != null)
			{
				dialogConfiguration.RenderMode = this.ResolvedRenderMode;
			}
		}

		// Token: 0x06009680 RID: 38528 RVA: 0x0021C834 File Offset: 0x0021AA34
		private void InitHeaderTools()
		{
			if (this.ResolvedRenderMode == RenderMode.Mobile)
			{
				EditorHeaderTool[] array = new EditorHeaderTool[this.HeaderTools.Count];
				this.HeaderTools.CopyTo(array, 0);
				this.HeaderTools.Clear();
				if (!string.IsNullOrEmpty(this.ToolsFile) || array.Length == 0)
				{
					this.ToolsFileLoader.LoadHeaderTools(this.HeaderTools);
				}
				this.HeaderTools.AddRange(array);
				ToolsStrings tools = this.Localization.Tools;
				using (IEnumerator enumerator = this.HeaderTools.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						EditorHeaderTool editorHeaderTool = (EditorHeaderTool)obj;
						if (string.IsNullOrEmpty(editorHeaderTool.Text) && !string.IsNullOrEmpty(editorHeaderTool.Name))
						{
							editorHeaderTool.Text = tools.GetString(editorHeaderTool.Name, false);
						}
					}
					return;
				}
			}
			this.HeaderTools.Clear();
		}

		// Token: 0x06009681 RID: 38529 RVA: 0x0021C934 File Offset: 0x0021AB34
		protected override void ControlPreRender()
		{
			base.ControlPreRender();
			this.ConfigureContentAreaMode();
			this.CreateRibbonbarResourcesHolder();
			this.Controls.Add(this._ribbonbarResourcesHolder);
			if (this._ribbonbar != null)
			{
				this._ribbonbar.Visible = (this._ribbonbarResourcesHolder.Visible = this.isRibbonBarVisible);
			}
			bool flag = base.IsEnabled && !this.EnableTextareaMode;
			if (flag != this._dialogOpener.Visible)
			{
				this._dialogOpener.Visible = flag;
			}
			this.LoadToolsFile(true);
			if (this._radContextMenu != null)
			{
				this.CreateRadContextMenu();
				this._radContextMenu.Visible = (this.Visible && base.IsEnabled);
			}
			this.EnsureEditorToolType();
			this.FillDefaultDialogDefinitions();
			this.InitHeaderTools();
			this.HeaderToolsToolAdapter.PreRender();
			this.ToolAdapter.PreRender();
			this.AddRibbonBarToControlsTree();
			this.SetRenderModeChildRadControls();
		}

		// Token: 0x06009682 RID: 38530 RVA: 0x0021CA20 File Offset: 0x0021AC20
		private void ConfigureContentAreaMode()
		{
			if (this.EditType == EditorEditType.Inline)
			{
				if (this._contentAreaMode == null)
				{
					this.ContentAreaMode = EditorContentAreaMode.Div;
					return;
				}
				if (this._contentAreaMode != EditorContentAreaMode.Div)
				{
					throw new ArgumentException("Inline editing is supported in DIV mode only.");
				}
			}
		}

		// Token: 0x06009683 RID: 38531 RVA: 0x0021CA78 File Offset: 0x0021AC78
		private void CreateRadContextMenu()
		{
			foreach (object obj in this.ContextMenus.EnabledContextMenus)
			{
				EditorContextMenu editorContextMenu = (EditorContextMenu)obj;
				this.AddToolsInMenuItemContainer(editorContextMenu.Tools, this._radContextMenu);
			}
			if (this.EnableTrackChanges || this.EnableComments)
			{
				string[] itemsToRearrange = new string[]
				{
					"RemoveComment",
					"AddComment",
					"RejectTrackChange",
					"AcceptTrackChange"
				};
				this.RearrangeRadContextMenuItems(itemsToRearrange);
			}
		}

		// Token: 0x06009684 RID: 38532 RVA: 0x0021CB28 File Offset: 0x0021AD28
		private void AddToolsInMenuItemContainer(EditorToolCollection tools, IRadMenuItemContainer itemContainer)
		{
			List<RadMenuItem> items = this.CreateRadContextMenuItems(tools);
			itemContainer.Items.AddRange(items);
		}

		// Token: 0x06009685 RID: 38533 RVA: 0x0021CB4C File Offset: 0x0021AD4C
		private List<RadMenuItem> CreateRadContextMenuItems(EditorToolCollection itemData)
		{
			List<RadMenuItem> list = new List<RadMenuItem>();
			foreach (object obj in itemData)
			{
				EditorTool editorTool = (EditorTool)obj;
				RadMenuItem radMenuItem = this.CreateRadContextMenuItem(editorTool);
				list.Add(radMenuItem);
				EditorContextMenuTool editorContextMenuTool = editorTool as EditorContextMenuTool;
				if (editorContextMenuTool != null && editorContextMenuTool.Tools.Count > 0)
				{
					this.AddToolsInMenuItemContainer(editorContextMenuTool.Tools, radMenuItem);
					radMenuItem.SpriteCssClass = "reToolIcon " + editorContextMenuTool.IconCssClass;
				}
			}
			return list;
		}

		// Token: 0x06009686 RID: 38534 RVA: 0x0021CBF4 File Offset: 0x0021ADF4
		private RadMenuItem CreateRadContextMenuItem(EditorTool tool)
		{
			return new RadMenuItem(this.Localization.Tools.GetString(tool.Name))
			{
				Value = tool.Name,
				Enabled = tool.Enabled,
				SpriteCssClass = "reToolIcon re" + tool.Name
			};
		}

		// Token: 0x06009687 RID: 38535 RVA: 0x0021CC50 File Offset: 0x0021AE50
		private void RearrangeRadContextMenuItems(string[] itemsToRearrange)
		{
			foreach (string value in itemsToRearrange)
			{
				RadMenuItem radMenuItem = this._radContextMenu.Items.FindItemByValue(value);
				if (radMenuItem != null)
				{
					int index = radMenuItem.Index;
					this._radContextMenu.Items.RemoveAt(index);
					this._radContextMenu.Items.Insert(0, radMenuItem);
				}
			}
		}

		// Token: 0x06009688 RID: 38536 RVA: 0x0021CCB8 File Offset: 0x0021AEB8
		private void InitiliazeDialogOpener()
		{
			if (this._dialogOpener != null && string.IsNullOrEmpty(this.ToolProviderID))
			{
				this._dialogOpener.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
				this._dialogOpener.EnableEmbeddedScripts = this.EnableEmbeddedScripts;
				this._dialogOpener.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
				this._dialogOpener.EnableAjaxSkinRendering = this.EnableAjaxSkinRendering;
				this._dialogOpener.Window.EnableAriaSupport = this.EnableAriaSupport;
				if (base.IsSkinSet)
				{
					this._dialogOpener.Skin = base.RuntimeSkin;
				}
				if (this._editorPageHasRadScriptManager())
				{
					this._dialogOpener.EnableTelerikManagers = true;
				}
			}
		}

		// Token: 0x06009689 RID: 38537 RVA: 0x0021CD68 File Offset: 0x0021AF68
		private void AddFindReplaceLocalization(IDictionary<string, string> locStrings)
		{
			DialogsStrings localization = this.FindReplaceOverlay.Localization;
			locStrings["find_notfound"] = localization.FindAndReplace_NotFound;
			locStrings["find_notsupported"] = localization.FindAndReplace_NotSupported;
			locStrings["find_allreplaced"] = localization.FindAndReplace_AllReplaced;
		}

		// Token: 0x0600968A RID: 38538 RVA: 0x0021CDB4 File Offset: 0x0021AFB4
		public void DisableFilter(EditorFilters filter)
		{
			if ((this.ContentFilters & filter) != EditorFilters.None)
			{
				this.ContentFilters ^= filter;
			}
		}

		// Token: 0x0600968B RID: 38539 RVA: 0x0021CDCE File Offset: 0x0021AFCE
		public void EnableFilter(EditorFilters filter)
		{
			this.ContentFilters |= filter;
		}

		// Token: 0x0600968C RID: 38540 RVA: 0x0021CDDE File Offset: 0x0021AFDE
		private bool IsFilterEnabled(EditorFilters filter)
		{
			return (this.ContentFilters & filter) != EditorFilters.None;
		}

		// Token: 0x0600968D RID: 38541 RVA: 0x0021CDF0 File Offset: 0x0021AFF0
		public void SetPaths(string[] paths, EditorFileTypes fileTypes, EditorFileOptions fileOptions)
		{
			string[] array = new string[]
			{
				"ViewPaths",
				"DeletePaths",
				"UploadPaths"
			};
			FileManagerDialogConfiguration[] array2 = new FileManagerDialogConfiguration[]
			{
				this.ImageManager,
				this.FlashManager,
				this.MediaManager,
				this.DocumentManager,
				this.TemplateManager,
				this.SilverlightManager
			};
			EditorFileTypes[] array3 = new EditorFileTypes[]
			{
				EditorFileTypes.Images,
				EditorFileTypes.Flash,
				EditorFileTypes.Media,
				EditorFileTypes.Documents,
				EditorFileTypes.Template,
				EditorFileTypes.Silverlight
			};
			EditorFileOptions[] array4 = new EditorFileOptions[]
			{
				EditorFileOptions.Browse,
				EditorFileOptions.Delete,
				EditorFileOptions.Upload
			};
			for (int i = 0; i < array3.Length; i++)
			{
				for (int j = 0; j < array4.Length; j++)
				{
					EditorFileTypes editorFileTypes = array3[i];
					EditorFileOptions editorFileOptions = array4[j];
					if ((editorFileOptions & fileOptions) > (EditorFileOptions)0 && (editorFileTypes & fileTypes) > (EditorFileTypes)0)
					{
						string name = array[j];
						array2[i].GetType().GetProperty(name).SetValue(array2[i], paths, null);
					}
				}
			}
		}

		// Token: 0x0600968E RID: 38542 RVA: 0x0021CF0E File Offset: 0x0021B10E
		private bool EnableRadContextMenu()
		{
			return this.UseRadContextMenu && this.ResolvedRenderMode == RenderMode.Lightweight;
		}

		// Token: 0x0600968F RID: 38543 RVA: 0x0021CF24 File Offset: 0x0021B124
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.Colors).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.ContentAreaSettings).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.ContextMenus).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.CssClasses).LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				((IStateManager)this.CssFiles).LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				((IStateManager)this.DocumentManager).LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				((IStateManager)this.FlashManager).LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				((IStateManager)this.FontNames).LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				((IStateManager)this.FontSizes).LoadViewState(array[9]);
			}
			if (array[10] != null)
			{
				((IStateManager)this.ImageManager).LoadViewState(array[10]);
			}
			if (array[11] != null)
			{
				((IStateManager)this.Links).LoadViewState(array[11]);
			}
			if (array[12] != null)
			{
				((IStateManager)this.Languages).LoadViewState(array[12]);
			}
			if (array[13] != null)
			{
				((IStateManager)this.MediaManager).LoadViewState(array[13]);
			}
			if (array[14] != null)
			{
				((IStateManager)this.Modules).LoadViewState(array[14]);
			}
			if (array[15] != null)
			{
				((IStateManager)this.Paragraphs).LoadViewState(array[15]);
			}
			if (array[16] != null)
			{
				((IStateManager)this.RealFontSizes).LoadViewState(array[16]);
			}
			if (array[17] != null)
			{
				((IStateManager)this.Snippets).LoadViewState(array[17]);
			}
			if (array[18] != null)
			{
				((IStateManager)this.Symbols).LoadViewState(array[18]);
			}
			if (array[19] != null)
			{
				((IStateManager)this.TemplateManager).LoadViewState(array[19]);
			}
			if (array[20] != null)
			{
				((IStateManager)this.Tools).LoadViewState(array[20]);
			}
			if (array[21] != null)
			{
				((IStateManager)this.SpellCheckSettings).LoadViewState(array[21]);
			}
			if (array[22] != null)
			{
				((IStateManager)this.TrackChangesSettings).LoadViewState(array[22]);
			}
		}

		// Token: 0x06009690 RID: 38544 RVA: 0x0021D100 File Offset: 0x0021B300
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				RadEditor.SaveState(this._colors),
				RadEditor.SaveState(this._contentAreaSettings),
				RadEditor.SaveState(this._contextMenus),
				RadEditor.SaveState(this._cssClasses),
				RadEditor.SaveState(this._cssFiles),
				RadEditor.SaveState(this._documentManager),
				RadEditor.SaveState(this._flashManager),
				RadEditor.SaveState(this._fontNames),
				RadEditor.SaveState(this._fontSizes),
				RadEditor.SaveState(this._imageManager),
				RadEditor.SaveState(this._links),
				RadEditor.SaveState(this._languages),
				RadEditor.SaveState(this._mediaManager),
				RadEditor.SaveState(this._modules),
				RadEditor.SaveState(this._paragraphs),
				RadEditor.SaveState(this._realFontSizes),
				RadEditor.SaveState(this._snippets),
				RadEditor.SaveState(this._symbols),
				RadEditor.SaveState(this._templateManager),
				RadEditor.SaveState(this._tools),
				RadEditor.SaveState(this._spellCheckSettings),
				RadEditor.SaveState(this._trackChangesSettings)
			};
		}

		// Token: 0x06009691 RID: 38545 RVA: 0x0021D264 File Offset: 0x0021B464
		protected override void TrackViewState()
		{
			base.TrackViewState();
			RadEditor.TrackState(this._colors);
			RadEditor.TrackState(this._contentAreaSettings);
			RadEditor.TrackState(this._contextMenus);
			RadEditor.TrackState(this._cssClasses);
			RadEditor.TrackState(this._cssFiles);
			RadEditor.TrackState(this._documentManager);
			RadEditor.TrackState(this._flashManager);
			RadEditor.TrackState(this._fontNames);
			RadEditor.TrackState(this._fontSizes);
			RadEditor.TrackState(this._imageManager);
			RadEditor.TrackState(this._languages);
			RadEditor.TrackState(this._links);
			RadEditor.TrackState(this._mediaManager);
			RadEditor.TrackState(this._modules);
			RadEditor.TrackState(this._paragraphs);
			RadEditor.TrackState(this._realFontSizes);
			RadEditor.TrackState(this._snippets);
			RadEditor.TrackState(this._symbols);
			RadEditor.TrackState(this._templateManager);
			RadEditor.TrackState(this._tools);
			RadEditor.TrackState(this._spellCheckSettings);
			RadEditor.TrackState(this._trackChangesSettings);
		}

		// Token: 0x06009692 RID: 38546 RVA: 0x0021D369 File Offset: 0x0021B569
		private static void TrackState(IStateManager obj)
		{
			if (obj != null)
			{
				obj.TrackViewState();
			}
		}

		// Token: 0x06009693 RID: 38547 RVA: 0x0021D374 File Offset: 0x0021B574
		private static object SaveState(IStateManager obj)
		{
			if (obj != null)
			{
				return obj.SaveViewState();
			}
			return null;
		}

		// Token: 0x17002F74 RID: 12148
		// (get) Token: 0x06009694 RID: 38548 RVA: 0x0021D381 File Offset: 0x0021B581
		CultureInfo ILocalizableControl.Culture
		{
			get
			{
				return this._culture;
			}
		}

		// Token: 0x17002F75 RID: 12149
		// (get) Token: 0x06009695 RID: 38549 RVA: 0x0021D389 File Offset: 0x0021B589
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Export settings")]
		[Category("Export")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public EditorExportSettings ExportSettings
		{
			get
			{
				if (this._exportSettings == null)
				{
					this._exportSettings = new EditorExportSettings(this.ViewState);
				}
				return this._exportSettings;
			}
		}

		// Token: 0x06009696 RID: 38550 RVA: 0x0021D3AA File Offset: 0x0021B5AA
		private void ImportContentHandler(object sender, EditorImportingArgs e)
		{
			this.OnImportContent(e);
		}

		// Token: 0x17002F76 RID: 12150
		// (get) Token: 0x06009697 RID: 38551 RVA: 0x0021D3B3 File Offset: 0x0021B5B3
		[NotifyParentProperty(true)]
		[Description("Import settings")]
		[Category("Import")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual EditorImportSettings ImportSettings
		{
			get
			{
				if (this._importSettings == null)
				{
					this._importSettings = new EditorImportSettings(this.ViewState);
				}
				return this._importSettings;
			}
		}

		// Token: 0x06009698 RID: 38552 RVA: 0x0021D3D4 File Offset: 0x0021B5D4
		public string GetHtml(EditorStripHtmlOptions stripOptions)
		{
			string text = this.Content;
			if ((stripOptions & EditorStripHtmlOptions.Comments) == EditorStripHtmlOptions.Comments)
			{
				text = this.StripComments(text);
			}
			else if (stripOptions == EditorStripHtmlOptions.AcceptTrackChanges)
			{
				text = this._trackChangesAdapter.AcceptChanges();
			}
			else if (stripOptions == EditorStripHtmlOptions.RejectTrackChanges)
			{
				text = this._trackChangesAdapter.RejectChanges();
			}
			return text;
		}

		// Token: 0x06009699 RID: 38553 RVA: 0x0021D41B File Offset: 0x0021B61B
		public void SetTrackChangesAdapter(ITrackChangesAdapter adapter)
		{
			this._trackChangesAdapter = adapter;
		}

		// Token: 0x0600969A RID: 38554 RVA: 0x0021D424 File Offset: 0x0021B624
		public virtual void AcceptTrackChanges()
		{
			this.Content = this._trackChangesAdapter.AcceptChanges();
		}

		// Token: 0x0600969B RID: 38555 RVA: 0x0021D437 File Offset: 0x0021B637
		public virtual void RejectTrackChanges()
		{
			this.Content = this._trackChangesAdapter.RejectChanges();
		}

		// Token: 0x0600969C RID: 38556 RVA: 0x0021D44C File Offset: 0x0021B64C
		private string StripComments(string html)
		{
			Regex regex = new Regex("<(span|font)[^>]*class=[\"']reComment[\\s\\w]*[\"'][^>]*>", RegexOptions.IgnoreCase);
			return regex.Replace(html, "<$1>");
		}

		// Token: 0x0600969D RID: 38557 RVA: 0x0021D471 File Offset: 0x0021B671
		public void Export(RadEditorExportTemplate template)
		{
			template.Export();
		}

		// Token: 0x0600969E RID: 38558 RVA: 0x0021D479 File Offset: 0x0021B679
		public void SetPdfExportTemplate(RadEditorExportTemplate template)
		{
			this._pdfExportTemplate = template;
		}

		// Token: 0x0600969F RID: 38559 RVA: 0x0021D482 File Offset: 0x0021B682
		public void ExportToPdf()
		{
			this.Export(this._pdfExportTemplate);
		}

		// Token: 0x060096A0 RID: 38560 RVA: 0x0021D490 File Offset: 0x0021B690
		public void SetRtfExportTemplate(RadEditorExportTemplate template)
		{
			this._rtfExportTemplate = template;
		}

		// Token: 0x060096A1 RID: 38561 RVA: 0x0021D499 File Offset: 0x0021B699
		public void ExportToRtf()
		{
			this.Export(this._rtfExportTemplate);
		}

		// Token: 0x060096A2 RID: 38562 RVA: 0x0021D4A7 File Offset: 0x0021B6A7
		public void LoadRtfContent(Stream rtfStream)
		{
			this.Content = this.CreateRtfImporter().Import(rtfStream);
		}

		// Token: 0x060096A3 RID: 38563 RVA: 0x0021D4BB File Offset: 0x0021B6BB
		public void LoadRtfContent(string rtfText)
		{
			this.Content = this.CreateRtfImporter().Import(rtfText);
		}

		// Token: 0x060096A4 RID: 38564 RVA: 0x0021D4D0 File Offset: 0x0021B6D0
		private RadEditorRtfImporter CreateRtfImporter()
		{
			RadEditorRtfImporter radEditorRtfImporter = new RadEditorRtfImporter();
			radEditorRtfImporter.ImportSettings = this.ImportSettings.Rtf;
			radEditorRtfImporter.DplImportProxy = this.DocumentImporter;
			radEditorRtfImporter.ImportContent += this.ImportContentHandler;
			return radEditorRtfImporter;
		}

		// Token: 0x060096A5 RID: 38565 RVA: 0x0021D513 File Offset: 0x0021B713
		public void SetDocxExportTemplate(RadEditorExportTemplate template)
		{
			this._docxExportTemplate = template;
		}

		// Token: 0x060096A6 RID: 38566 RVA: 0x0021D51C File Offset: 0x0021B71C
		public void ExportToDocx()
		{
			this.Export(this._docxExportTemplate);
		}

		// Token: 0x060096A7 RID: 38567 RVA: 0x0021D52A File Offset: 0x0021B72A
		public void LoadDocxContent(Stream docxStream)
		{
			this.Content = this.CreateDocxImporter().Import(docxStream);
		}

		// Token: 0x060096A8 RID: 38568 RVA: 0x0021D53E File Offset: 0x0021B73E
		public void LoadDocxContent(string docxText)
		{
			this.Content = this.CreateDocxImporter().Import(docxText);
		}

		// Token: 0x060096A9 RID: 38569 RVA: 0x0021D554 File Offset: 0x0021B754
		private RadEditorDocxImporter CreateDocxImporter()
		{
			RadEditorDocxImporter radEditorDocxImporter = new RadEditorDocxImporter();
			radEditorDocxImporter.ImportSettings = this.ImportSettings.Docx;
			radEditorDocxImporter.DplImportProxy = this.DocumentImporter;
			radEditorDocxImporter.ImportContent += this.ImportContentHandler;
			return radEditorDocxImporter;
		}

		// Token: 0x060096AA RID: 38570 RVA: 0x0021D597 File Offset: 0x0021B797
		public void SetMarkdownExportTemplate(RadEditorExportTemplate template)
		{
			this._markdownExportTemplate = template;
		}

		// Token: 0x060096AB RID: 38571 RVA: 0x0021D5A0 File Offset: 0x0021B7A0
		public void ExportToMarkdown()
		{
			this.Export(this._markdownExportTemplate);
		}

		// Token: 0x060096AC RID: 38572 RVA: 0x0021D5B0 File Offset: 0x0021B7B0
		public void LoadMarkdownContent(Stream markdownStream)
		{
			StreamReader streamReader = new StreamReader(markdownStream);
			this.LoadMarkdownContent(streamReader.ReadToEnd());
			streamReader.Close();
		}

		// Token: 0x060096AD RID: 38573 RVA: 0x0021D5D8 File Offset: 0x0021B7D8
		public void LoadMarkdownContent(string markdownText)
		{
			Markdown markdown = new Markdown();
			this.Content = markdown.Transform(markdownText);
		}

		// Token: 0x17002F77 RID: 12151
		// (get) Token: 0x060096AE RID: 38574 RVA: 0x0021D5F8 File Offset: 0x0021B7F8
		// (set) Token: 0x060096AF RID: 38575 RVA: 0x0021D600 File Offset: 0x0021B800
		[Description("Gets a value indicating whether the editor is being rendered in accessible mode")]
		[MergableProperty(true)]
		[DefaultValue(false)]
		[Category("Behavior")]
		public virtual bool IsInAccessibleMode
		{
			get
			{
				return this._isInAccessibleMode;
			}
			internal set
			{
				this._isInAccessibleMode = value;
			}
		}

		// Token: 0x17002F78 RID: 12152
		// (get) Token: 0x060096B0 RID: 38576 RVA: 0x0021D609 File Offset: 0x0021B809
		// (set) Token: 0x060096B1 RID: 38577 RVA: 0x0021D61B File Offset: 0x0021B81B
		[DefaultValue("")]
		[Category("Tools")]
		[Description("Gets or sets a string containing the ID (will search for both server or client ID) of a client object that should be used as a tool provider.")]
		[MergableProperty(true)]
		public string ToolProviderID
		{
			get
			{
				return base.GetViewStateValue<string>("ToolProviderID", string.Empty);
			}
			set
			{
				this.ViewState["ToolProviderID"] = value;
				if (!string.IsNullOrEmpty(value))
				{
					this.ResetToolsFileContent();
					this.ClearCollections();
				}
			}
		}

		// Token: 0x17002F79 RID: 12153
		// (get) Token: 0x060096B2 RID: 38578 RVA: 0x0021D642 File Offset: 0x0021B842
		[Description("Gets or sets the list of external CSS files that should be made available in the editor's content area.")]
		[Category("DropDown Configuration")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public EditorCssFileCollection CssFiles
		{
			get
			{
				if (this._cssFiles == null)
				{
					this._cssFiles = new EditorCssFileCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._cssFiles).TrackViewState();
					}
				}
				return this._cssFiles;
			}
		}

		// Token: 0x17002F7A RID: 12154
		// (get) Token: 0x060096B3 RID: 38579 RVA: 0x0021D670 File Offset: 0x0021B870
		[Category("Tools")]
		[Description("Gets the list of modules that should be made included in RadEditor.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public EditorModuleCollection Modules
		{
			get
			{
				if (this._modules == null)
				{
					this._modules = new EditorModuleCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._modules).TrackViewState();
					}
				}
				return this._modules;
			}
		}

		// Token: 0x17002F7B RID: 12155
		// (get) Token: 0x060096B4 RID: 38580 RVA: 0x0021D69E File Offset: 0x0021B89E
		[Category("DropDown Configuration")]
		[Description("Gets the collection containing the context menus which will be displayed in the RadEditor content area")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public EditorContextMenuCollection ContextMenus
		{
			get
			{
				if (this._contextMenus == null)
				{
					this._contextMenus = new EditorContextMenuCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._contextMenus).TrackViewState();
					}
				}
				return this._contextMenus;
			}
		}

		// Token: 0x17002F7C RID: 12156
		// (get) Token: 0x060096B5 RID: 38581 RVA: 0x0021D6CC File Offset: 0x0021B8CC
		[Description("Gets the collection containing the colors to put in the Foreground and Background color dropdowns")]
		[Category("DropDown Configuration")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public EditorColorCollection Colors
		{
			get
			{
				if (this._colors == null)
				{
					this._colors = new EditorColorCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._colors).TrackViewState();
					}
				}
				return this._colors;
			}
		}

		// Token: 0x17002F7D RID: 12157
		// (get) Token: 0x060096B6 RID: 38582 RVA: 0x0021D6FA File Offset: 0x0021B8FA
		[Description("Gets the collection containing the symbols to put in the Symbols dropdown")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("DropDown Configuration")]
		public EditorSymbolCollection Symbols
		{
			get
			{
				if (this._symbols == null)
				{
					this._symbols = new EditorSymbolCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._symbols).TrackViewState();
					}
				}
				return this._symbols;
			}
		}

		// Token: 0x17002F7E RID: 12158
		// (get) Token: 0x060096B7 RID: 38583 RVA: 0x0021D728 File Offset: 0x0021B928
		[Description("Gets the collection containing the links to put in the Custom Links dropdown.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("DropDown Configuration")]
		public EditorLinkCollection Links
		{
			get
			{
				if (this._links == null)
				{
					this._links = new EditorLinkCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._links).TrackViewState();
					}
				}
				return this._links;
			}
		}

		// Token: 0x17002F7F RID: 12159
		// (get) Token: 0x060096B8 RID: 38584 RVA: 0x0021D756 File Offset: 0x0021B956
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("DropDown Configuration")]
		[Description("Gets the collection containing the custom font sizes to put in the [Size] dropdown")]
		public EditorFontSizeCollection FontSizes
		{
			get
			{
				if (this._fontSizes == null)
				{
					this._fontSizes = new EditorFontSizeCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._fontSizes).TrackViewState();
					}
				}
				return this._fontSizes;
			}
		}

		// Token: 0x17002F80 RID: 12160
		// (get) Token: 0x060096B9 RID: 38585 RVA: 0x0021D784 File Offset: 0x0021B984
		[Description("Gets the collection containing the custom font names to put in the Font dropdown.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("DropDown Configuration")]
		public EditorFontCollection FontNames
		{
			get
			{
				if (this._fontNames == null)
				{
					this._fontNames = new EditorFontCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._fontNames).TrackViewState();
					}
				}
				return this._fontNames;
			}
		}

		// Token: 0x17002F81 RID: 12161
		// (get) Token: 0x060096BA RID: 38586 RVA: 0x0021D7B2 File Offset: 0x0021B9B2
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets the collection containing the paragraph styles to put in the Paragraph Style dropdown.")]
		[Category("DropDown Configuration")]
		public EditorParagraphCollection Paragraphs
		{
			get
			{
				if (this._paragraphs == null)
				{
					this._paragraphs = new EditorParagraphCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._paragraphs).TrackViewState();
					}
				}
				return this._paragraphs;
			}
		}

		// Token: 0x17002F82 RID: 12162
		// (get) Token: 0x060096BB RID: 38587 RVA: 0x0021D7E0 File Offset: 0x0021B9E0
		[Category("DropDown Configuration")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets the collection containing the paragraph styles to put in the Paragraph Style dropdown.")]
		public EditorFormatSetCollection FormatSets
		{
			get
			{
				if (this._formatSets == null)
				{
					this._formatSets = new EditorFormatSetCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._formatSets).TrackViewState();
					}
				}
				return this._formatSets;
			}
		}

		// Token: 0x17002F83 RID: 12163
		// (get) Token: 0x060096BC RID: 38588 RVA: 0x0021D80E File Offset: 0x0021BA0E
		[Description("Gets the collection containing the custom real font sizes to put in the RealFontSize dropdown.")]
		[Category("DropDown Configuration")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public EditorRealFontSizeCollection RealFontSizes
		{
			get
			{
				if (this._realFontSizes == null)
				{
					this._realFontSizes = new EditorRealFontSizeCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._realFontSizes).TrackViewState();
					}
				}
				return this._realFontSizes;
			}
		}

		// Token: 0x17002F84 RID: 12164
		// (get) Token: 0x060096BD RID: 38589 RVA: 0x0021D83C File Offset: 0x0021BA3C
		[Category("DropDown Configuration")]
		[Description("Gets the collection containing the CSS classes to put in the Apply CSS Class dropdown.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public EditorCssClassCollection CssClasses
		{
			get
			{
				if (this._cssClasses == null)
				{
					this._cssClasses = new EditorCssClassCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._cssClasses).TrackViewState();
					}
				}
				return this._cssClasses;
			}
		}

		// Token: 0x17002F85 RID: 12165
		// (get) Token: 0x060096BE RID: 38590 RVA: 0x0021D86A File Offset: 0x0021BA6A
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets the collection containing the snippets to put in the Code Snippet dropdown")]
		[Category("DropDown Configuration")]
		public EditorSnippetCollection Snippets
		{
			get
			{
				if (this._snippets == null)
				{
					this._snippets = new EditorSnippetCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._snippets).TrackViewState();
					}
				}
				return this._snippets;
			}
		}

		// Token: 0x17002F86 RID: 12166
		// (get) Token: 0x060096BF RID: 38591 RVA: 0x0021D898 File Offset: 0x0021BA98
		[Description("Gets the collection containing the available languages for spellchecking.")]
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public SpellCheckerLanguageCollection Languages
		{
			get
			{
				if (this._languages == null)
				{
					this._languages = new SpellCheckerLanguageCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._languages).TrackViewState();
					}
				}
				return this._languages;
			}
		}

		// Token: 0x17002F87 RID: 12167
		// (get) Token: 0x060096C0 RID: 38592 RVA: 0x0021D8C6 File Offset: 0x0021BAC6
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Tools")]
		[Description("Gets the collection containing RadEditor tools.")]
		public EditorToolGroupCollection Tools
		{
			get
			{
				if (this._tools == null)
				{
					this._tools = new EditorToolGroupCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._tools).TrackViewState();
					}
				}
				return this._tools;
			}
		}

		// Token: 0x17002F88 RID: 12168
		// (get) Token: 0x060096C1 RID: 38593 RVA: 0x0021D8F4 File Offset: 0x0021BAF4
		[Category("Tools")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets the collection containing RadEditor HeaderTools.")]
		public EditorHeaderToolCollection HeaderTools
		{
			get
			{
				if (this._headerTools == null)
				{
					this._headerTools = new EditorHeaderToolCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._headerTools).TrackViewState();
					}
				}
				return this._headerTools;
			}
		}

		// Token: 0x17002F89 RID: 12169
		// (get) Token: 0x060096C2 RID: 38594 RVA: 0x0021D922 File Offset: 0x0021BB22
		// (set) Token: 0x060096C3 RID: 38595 RVA: 0x0021D934 File Offset: 0x0021BB34
		[UrlProperty("*.xml")]
		[Category("Tools")]
		[Description("Gets or sets a string containing the path to a XML file, containing the editor toolbar configuration settings.")]
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		[Bindable(true)]
		[DefaultValue("")]
		[MergableProperty(true)]
		public string ToolsFile
		{
			get
			{
				return base.GetViewStateValue<string>("ToolsFile", string.Empty);
			}
			set
			{
				this.ViewState["ToolsFile"] = value;
				this.ResetToolsFileContent();
				this.LoadToolsFile(false);
			}
		}

		// Token: 0x17002F8A RID: 12170
		// (get) Token: 0x060096C4 RID: 38596 RVA: 0x0021D954 File Offset: 0x0021BB54
		// (set) Token: 0x060096C5 RID: 38597 RVA: 0x0021D96B File Offset: 0x0021BB6B
		[Category("Appearance")]
		[DefaultValue("en-US")]
		[ClientControlProperty]
		[MergableProperty(true)]
		[Description("Gets or sets a string containing the localization language for the RadEditor UI.")]
		public string Language
		{
			get
			{
				return base.GetViewStateValue<string>("Language", CultureInfo.CurrentUICulture.Name);
			}
			set
			{
				this.ViewState["Language"] = value;
				this._culture = ((value == null) ? null : CultureInfo.GetCultureInfo(value));
			}
		}

		// Token: 0x17002F8B RID: 12171
		// (get) Token: 0x060096C6 RID: 38598 RVA: 0x0021D990 File Offset: 0x0021BB90
		// (set) Token: 0x060096C7 RID: 38599 RVA: 0x0021D9A2 File Offset: 0x0021BBA2
		[MergableProperty(true)]
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		[Description("Gets or sets a string, containing the location of the content area CSS styles. You need to set this property only if you are using a custom skin.")]
		[Category("Appearance")]
		[DefaultValue("")]
		[UrlProperty("*.css")]
		public string ContentAreaCssFile
		{
			get
			{
				return base.GetViewStateValue<string>("ContentAreaCssFile", string.Empty);
			}
			set
			{
				this.ViewState["ContentAreaCssFile"] = value;
			}
		}

		// Token: 0x17002F8C RID: 12172
		// (get) Token: 0x060096C8 RID: 38600 RVA: 0x0021D9B5 File Offset: 0x0021BBB5
		// (set) Token: 0x060096C9 RID: 38601 RVA: 0x0021D9C7 File Offset: 0x0021BBC7
		[Category("Appearance")]
		[MergableProperty(true)]
		[Description("Gets or sets a string, containing the location of the CSS styles for table css style layout tool in the TableProperties dialogue.")]
		[DefaultValue("")]
		[UrlProperty("*.css")]
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		public string TableLayoutCssFile
		{
			get
			{
				return base.GetViewStateValue<string>("TableLayoutCssFile", string.Empty);
			}
			set
			{
				this.ViewState["TableLayoutCssFile"] = value;
			}
		}

		// Token: 0x17002F8D RID: 12173
		// (get) Token: 0x060096CA RID: 38602 RVA: 0x0021D9DA File Offset: 0x0021BBDA
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Misc")]
		public EditorStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new EditorStrings(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x17002F8E RID: 12174
		// (get) Token: 0x060096CB RID: 38603 RVA: 0x0021DA09 File Offset: 0x0021BC09
		// (set) Token: 0x060096CC RID: 38604 RVA: 0x0021DA2C File Offset: 0x0021BC2C
		[Category("Misc")]
		[DefaultValue("")]
		[Description("Gets or sets a value indicating where the editor will look for its .resx localization files.")]
		public string LocalizationPath
		{
			get
			{
				return ((string)this.ViewState["LocalizationPath"]) ?? string.Empty;
			}
			set
			{
				string text = value.Replace("\\", "/");
				if (text.Length > 0 && !text.EndsWith("/"))
				{
					text += "/";
				}
				this.ViewState["LocalizationPath"] = text;
			}
		}

		// Token: 0x17002F8F RID: 12175
		// (get) Token: 0x060096CD RID: 38605 RVA: 0x0021DA7F File Offset: 0x0021BC7F
		// (set) Token: 0x060096CE RID: 38606 RVA: 0x0021DA8F File Offset: 0x0021BC8F
		[DefaultValue(false)]
		[Obsolete("This property is now obsolete. Please use the ContentFilters property or the EnableFilter and DisableFilter methods", false)]
		[Bindable(true)]
		[MergableProperty(true)]
		[Category("Misc")]
		[Description("Gets or sets the value indicating whether script tags will be removed from the editor content.")]
		public bool AllowScripts
		{
			get
			{
				return (this.ContentFilters & EditorFilters.RemoveScripts) <= EditorFilters.None;
			}
			set
			{
				if (value)
				{
					this.DisableFilter(EditorFilters.RemoveScripts);
					return;
				}
				this.EnableFilter(EditorFilters.RemoveScripts);
			}
		}

		// Token: 0x17002F90 RID: 12176
		// (get) Token: 0x060096CF RID: 38607 RVA: 0x0021DAA3 File Offset: 0x0021BCA3
		// (set) Token: 0x060096D0 RID: 38608 RVA: 0x0021DAB1 File Offset: 0x0021BCB1
		[DefaultValue(false)]
		[ClientControlProperty]
		[Bindable(true)]
		[MergableProperty(true)]
		[Category("Behavior")]
		[Description("Gets or sets the value indicating whether the RadEditor will auto-resize its height to match content height .")]
		public bool AutoResizeHeight
		{
			get
			{
				return base.GetViewStateValue<bool>("AutoResizeHeight", false);
			}
			set
			{
				this.ViewState["AutoResizeHeight"] = value;
			}
		}

		// Token: 0x17002F91 RID: 12177
		// (get) Token: 0x060096D1 RID: 38609 RVA: 0x0021DAC9 File Offset: 0x0021BCC9
		// (set) Token: 0x060096D2 RID: 38610 RVA: 0x0021DAD7 File Offset: 0x0021BCD7
		[Description("Gets or sets the value indicating whether the users will be able to resize the RadEditor control on the client.")]
		[Category("Behavior")]
		[DefaultValue(true)]
		[Bindable(true)]
		[MergableProperty(true)]
		public bool EnableResize
		{
			get
			{
				return base.GetViewStateValue<bool>("EnableResize", true);
			}
			set
			{
				this.ViewState["EnableResize"] = value;
			}
		}

		// Token: 0x17002F92 RID: 12178
		// (get) Token: 0x060096D3 RID: 38611 RVA: 0x0021DAEF File Offset: 0x0021BCEF
		// (set) Token: 0x060096D4 RID: 38612 RVA: 0x0021DAFA File Offset: 0x0021BCFA
		[Category("Behavior")]
		[Obsolete("This property is obsolete. Please, use the NewLineMode property instead.", false)]
		[Bindable(true)]
		[Description("This property is obsolete. Please, use the NewLineMode property instead.")]
		[DefaultValue(true)]
		[MergableProperty(true)]
		public bool NewLineBr
		{
			get
			{
				return this.NewLineMode == EditorNewLineModes.P;
			}
			set
			{
				this.NewLineMode = (value ? EditorNewLineModes.Br : EditorNewLineModes.P);
			}
		}

		// Token: 0x17002F93 RID: 12179
		// (get) Token: 0x060096D5 RID: 38613 RVA: 0x0021DB09 File Offset: 0x0021BD09
		// (set) Token: 0x060096D6 RID: 38614 RVA: 0x0021DB17 File Offset: 0x0021BD17
		[ClientControlProperty]
		[DefaultValue(EditorNewLineModes.P)]
		[Description("Gets or sets the value indicating what element will be inserted when the [Enter] key is pressed.")]
		[Editor("Telerik.Web.Design.Common.FlagEnumUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[MergableProperty(true)]
		[Category("Behavior")]
		public EditorNewLineModes NewLineMode
		{
			get
			{
				return base.GetViewStateValue<EditorNewLineModes>("NewLineMode", EditorNewLineModes.P);
			}
			set
			{
				this.ViewState["NewLineMode"] = value;
			}
		}

		// Token: 0x17002F94 RID: 12180
		// (get) Token: 0x060096D7 RID: 38615 RVA: 0x0021DB2F File Offset: 0x0021BD2F
		// (set) Token: 0x060096D8 RID: 38616 RVA: 0x0021DB3D File Offset: 0x0021BD3D
		[ClientControlProperty]
		[MergableProperty(true)]
		[Category("Behavior")]
		[DefaultValue(EditorToolbarMode.Default)]
		[Description("Gets or sets the value indicating whether the RadEditor toolbar will be docked at the top of the page.")]
		[Bindable(true)]
		public EditorToolbarMode ToolbarMode
		{
			get
			{
				return base.GetViewStateValue<EditorToolbarMode>("ToolbarMode", EditorToolbarMode.Default);
			}
			set
			{
				this.ViewState["ToolbarMode"] = value;
				this._toolAdapter = null;
			}
		}

		// Token: 0x17002F95 RID: 12181
		// (get) Token: 0x060096D9 RID: 38617 RVA: 0x0021DB5C File Offset: 0x0021BD5C
		// (set) Token: 0x060096DA RID: 38618 RVA: 0x0021DBD2 File Offset: 0x0021BDD2
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ToolAdapter ToolAdapter
		{
			get
			{
				if (this._toolAdapter == null)
				{
					if (this.ResolvedRenderMode == RenderMode.Mobile)
					{
						this._toolAdapter = new MobileToolAdapter(this);
					}
					else
					{
						if (this.isRibbonBarVisible)
						{
							RibbonBarToolAdapter toolAdapter = (this.ResolvedRenderMode == RenderMode.Lightweight) ? new LiteRibbonBarToolAdapter(this) : new RibbonBarToolAdapter(this);
							this._toolAdapter = toolAdapter;
						}
						else
						{
							this._toolAdapter = new DefaultToolAdapter();
						}
						this._toolAdapter.Editor = this;
					}
				}
				return this._toolAdapter;
			}
			set
			{
				this._toolAdapter = value;
			}
		}

		// Token: 0x17002F96 RID: 12182
		// (get) Token: 0x060096DB RID: 38619 RVA: 0x0021DBDB File Offset: 0x0021BDDB
		// (set) Token: 0x060096DC RID: 38620 RVA: 0x0021DBF7 File Offset: 0x0021BDF7
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public HeaderToolsToolAdapter HeaderToolsToolAdapter
		{
			get
			{
				if (this._headerToolsToolAdapter == null)
				{
					this._headerToolsToolAdapter = new HeaderToolsToolAdapter(this);
				}
				return this._headerToolsToolAdapter;
			}
			set
			{
				this._headerToolsToolAdapter = value;
			}
		}

		// Token: 0x17002F97 RID: 12183
		// (get) Token: 0x060096DD RID: 38621 RVA: 0x0021DC00 File Offset: 0x0021BE00
		// (set) Token: 0x060096DE RID: 38622 RVA: 0x0021DC7D File Offset: 0x0021BE7D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public FindReplaceMobile FindReplaceOverlay
		{
			get
			{
				if (this._findReplaceOverlay == null)
				{
					LocalizationProvider localization = new LocalizationProvider("RadEditor.Dialogs", this, this.LocalizationPath);
					this._findReplaceOverlay = new FindReplaceMobile
					{
						ID = "FindReplaceOverlay",
						Localization = new DialogsStrings(localization, new string[]
						{
							"FindAndReplace"
						}, false),
						Skin = this.Skin,
						Visible = (this.RenderMode == RenderMode.Mobile)
					};
				}
				return this._findReplaceOverlay;
			}
			set
			{
				this._findReplaceOverlay = value;
			}
		}

		// Token: 0x17002F98 RID: 12184
		// (get) Token: 0x060096DF RID: 38623 RVA: 0x0021DC86 File Offset: 0x0021BE86
		// (set) Token: 0x060096E0 RID: 38624 RVA: 0x0021DCA6 File Offset: 0x0021BEA6
		[ClientPropertyName("load")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnClientLoad
		{
			get
			{
				return ((string)this.ViewState["OnClientLoad"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x17002F99 RID: 12185
		// (get) Token: 0x060096E1 RID: 38625 RVA: 0x0021DCB9 File Offset: 0x0021BEB9
		// (set) Token: 0x060096E2 RID: 38626 RVA: 0x0021DCD9 File Offset: 0x0021BED9
		[ClientPropertyName("init")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[DefaultValue("")]
		[Category("Client-side events")]
		public virtual string OnClientInit
		{
			get
			{
				return ((string)this.ViewState["OnClientInit"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientInit"] = value;
			}
		}

		// Token: 0x17002F9A RID: 12186
		// (get) Token: 0x060096E3 RID: 38627 RVA: 0x0021DCEC File Offset: 0x0021BEEC
		// (set) Token: 0x060096E4 RID: 38628 RVA: 0x0021DD1B File Offset: 0x0021BF1B
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("pasteHtml")]
		[Category("Client-side events")]
		public virtual string OnClientPasteHtml
		{
			get
			{
				if (this.ViewState["OnClientPasteHtml"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientPasteHtml"];
			}
			set
			{
				this.ViewState["OnClientPasteHtml"] = value;
			}
		}

		// Token: 0x17002F9B RID: 12187
		// (get) Token: 0x060096E5 RID: 38629 RVA: 0x0021DD2E File Offset: 0x0021BF2E
		// (set) Token: 0x060096E6 RID: 38630 RVA: 0x0021DD5D File Offset: 0x0021BF5D
		[DefaultValue("")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("domChange")]
		public virtual string OnClientDomChange
		{
			get
			{
				if (this.ViewState["OnClientDomChange"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientDomChange"];
			}
			set
			{
				this.ViewState["OnClientDomChange"] = value;
			}
		}

		// Token: 0x17002F9C RID: 12188
		// (get) Token: 0x060096E7 RID: 38631 RVA: 0x0021DD70 File Offset: 0x0021BF70
		// (set) Token: 0x060096E8 RID: 38632 RVA: 0x0021DD9F File Offset: 0x0021BF9F
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("submit")]
		[Category("Client-side events")]
		public virtual string OnClientSubmit
		{
			get
			{
				if (this.ViewState["OnClientSubmit"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientSubmit"];
			}
			set
			{
				this.ViewState["OnClientSubmit"] = value;
			}
		}

		// Token: 0x17002F9D RID: 12189
		// (get) Token: 0x060096E9 RID: 38633 RVA: 0x0021DDB2 File Offset: 0x0021BFB2
		// (set) Token: 0x060096EA RID: 38634 RVA: 0x0021DDE1 File Offset: 0x0021BFE1
		[ClientPropertyName("modeChange")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[DefaultValue("")]
		public virtual string OnClientModeChange
		{
			get
			{
				if (this.ViewState["OnClientModeChange"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientModeChange"];
			}
			set
			{
				this.ViewState["OnClientModeChange"] = value;
			}
		}

		// Token: 0x17002F9E RID: 12190
		// (get) Token: 0x060096EB RID: 38635 RVA: 0x0021DDF4 File Offset: 0x0021BFF4
		// (set) Token: 0x060096EC RID: 38636 RVA: 0x0021DE23 File Offset: 0x0021C023
		[ClientPropertyName("selectionChange")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public virtual string OnClientSelectionChange
		{
			get
			{
				if (this.ViewState["OnClientSelectionChange"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientSelectionChange"];
			}
			set
			{
				this.ViewState["OnClientSelectionChange"] = value;
			}
		}

		// Token: 0x17002F9F RID: 12191
		// (get) Token: 0x060096ED RID: 38637 RVA: 0x0021DE36 File Offset: 0x0021C036
		// (set) Token: 0x060096EE RID: 38638 RVA: 0x0021DE65 File Offset: 0x0021C065
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("commandExecuting")]
		public virtual string OnClientCommandExecuting
		{
			get
			{
				if (this.ViewState["OnClientCommandExecuting"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientCommandExecuting"];
			}
			set
			{
				this.ViewState["OnClientCommandExecuting"] = value;
			}
		}

		// Token: 0x17002FA0 RID: 12192
		// (get) Token: 0x060096EF RID: 38639 RVA: 0x0021DE78 File Offset: 0x0021C078
		// (set) Token: 0x060096F0 RID: 38640 RVA: 0x0021DEA7 File Offset: 0x0021C0A7
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("commandExecuted")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[DefaultValue("")]
		public virtual string OnClientCommandExecuted
		{
			get
			{
				if (this.ViewState["OnClientCommandExecuted"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientCommandExecuted"];
			}
			set
			{
				this.ViewState["OnClientCommandExecuted"] = value;
			}
		}

		// Token: 0x17002FA1 RID: 12193
		// (get) Token: 0x060096F1 RID: 38641 RVA: 0x0021DEBA File Offset: 0x0021C0BA
		// (set) Token: 0x060096F2 RID: 38642 RVA: 0x0021DEDA File Offset: 0x0021C0DA
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientPropertyName("inlineEditCompleted")]
		public string OnClientInlineEditCompleted
		{
			get
			{
				return (string)(this.ViewState["OnClientInlineEditCompleted"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientInlineEditCompleted"] = value;
			}
		}

		// Token: 0x17002FA2 RID: 12194
		// (get) Token: 0x060096F3 RID: 38643 RVA: 0x0021DEED File Offset: 0x0021C0ED
		// (set) Token: 0x060096F4 RID: 38644 RVA: 0x0021DF09 File Offset: 0x0021C109
		[DefaultValue(false)]
		[ClientPropertyName("isSkinTouch")]
		[Description("Gets or sets bool value indicating if the Runtime skin is touch.")]
		[Category("Appearance")]
		[ClientControlProperty]
		public bool IsSkinTouch
		{
			get
			{
				return base.RuntimeSkin.EndsWith("Touch") || this._isSkinTouch;
			}
			set
			{
				this._isSkinTouch = value;
			}
		}

		// Token: 0x17002FA3 RID: 12195
		// (get) Token: 0x060096F5 RID: 38645 RVA: 0x0021DF14 File Offset: 0x0021C114
		// (set) Token: 0x060096F6 RID: 38646 RVA: 0x0021DF48 File Offset: 0x0021C148
		[Category("Appearance")]
		[Description("Gets or sets the height of the Web server control. The default height is 400 pixels.")]
		[DefaultValue(typeof(Unit), "400px")]
		[NotifyParentProperty(true)]
		public override Unit Height
		{
			get
			{
				if (!base.Height.IsEmpty)
				{
					return base.Height;
				}
				return Unit.Parse(this.GetDefaultHeight(), CultureInfo.InvariantCulture);
			}
			set
			{
				base.Height = value;
			}
		}

		// Token: 0x17002FA4 RID: 12196
		// (get) Token: 0x060096F7 RID: 38647 RVA: 0x0021DF54 File Offset: 0x0021C154
		// (set) Token: 0x060096F8 RID: 38648 RVA: 0x0021DF88 File Offset: 0x0021C188
		[DefaultValue(typeof(Unit), "680px")]
		[Description("Gets or sets the width of the Web server control.")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		public override Unit Width
		{
			get
			{
				if (!base.Width.IsEmpty)
				{
					return base.Width;
				}
				return Unit.Parse(this.GetDefaultWidth(), CultureInfo.InvariantCulture);
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x17002FA5 RID: 12197
		// (get) Token: 0x060096F9 RID: 38649 RVA: 0x0021DF91 File Offset: 0x0021C191
		// (set) Token: 0x060096FA RID: 38650 RVA: 0x0021DFA3 File Offset: 0x0021C1A3
		[Description("Gets or sets the width of the editor's toolbar (should be used when ToolbarMode != Default).")]
		[DefaultValue(typeof(Unit), "")]
		[ClientControlProperty]
		[Category("Tools")]
		public Unit ToolsWidth
		{
			get
			{
				return base.GetViewStateValue<Unit>("ToolsWidth", Unit.Empty);
			}
			set
			{
				this.ViewState["ToolsWidth"] = value;
			}
		}

		// Token: 0x17002FA6 RID: 12198
		// (get) Token: 0x060096FB RID: 38651 RVA: 0x0021DFBC File Offset: 0x0021C1BC
		// (set) Token: 0x060096FC RID: 38652 RVA: 0x0021DFE5 File Offset: 0x0021C1E5
		[ClientControlProperty]
		[Description("Gets or sets the max length (in symbols) of the text inserted in the RadEditor. When the value is 0 the property is disabled.")]
		[Category("Behavior")]
		[DefaultValue(0)]
		public int MaxTextLength
		{
			get
			{
				object obj = this.ViewState["MaxTextLength"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["MaxTextLength"] = value;
			}
		}

		// Token: 0x17002FA7 RID: 12199
		// (get) Token: 0x060096FD RID: 38653 RVA: 0x0021E000 File Offset: 0x0021C200
		// (set) Token: 0x060096FE RID: 38654 RVA: 0x0021E029 File Offset: 0x0021C229
		[Description("Gets or sets the max length (in symbols) of the HTML inserted in the RadEditor. When the value is 0 the property is disabled.")]
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(0)]
		public int MaxHtmlLength
		{
			get
			{
				object obj = this.ViewState["MaxHtmlLength"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["MaxHtmlLength"] = value;
			}
		}

		// Token: 0x17002FA8 RID: 12200
		// (get) Token: 0x060096FF RID: 38655 RVA: 0x0021E041 File Offset: 0x0021C241
		// (set) Token: 0x06009700 RID: 38656 RVA: 0x0021E049 File Offset: 0x0021C249
		[Description("Specifies the skin that will be used by the control")]
		[TypeConverter("Telerik.Web.Design.SkinTypeConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		[NotifyParentProperty(true)]
		[DefaultValue("Default")]
		[Category("Appearance")]
		public override string Skin
		{
			get
			{
				return base.Skin;
			}
			set
			{
				if (base.ChildControlsCreated)
				{
					this._dialogOpener.Skin = value;
					if (this.FindReplaceOverlay != null)
					{
						this.FindReplaceOverlay.Skin = value;
					}
				}
				base.Skin = value;
			}
		}

		// Token: 0x17002FA9 RID: 12201
		// (get) Token: 0x06009701 RID: 38657 RVA: 0x0021E07A File Offset: 0x0021C27A
		// (set) Token: 0x06009702 RID: 38658 RVA: 0x0021E082 File Offset: 0x0021C282
		[Description("Whether to register the skin CSS during Ajax requests")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[DefaultValue(true)]
		public override bool EnableAjaxSkinRendering
		{
			get
			{
				return base.EnableAjaxSkinRendering;
			}
			set
			{
				if (base.ChildControlsCreated)
				{
					this._dialogOpener.EnableAjaxSkinRendering = value;
				}
				base.EnableAjaxSkinRendering = value;
			}
		}

		// Token: 0x17002FAA RID: 12202
		// (get) Token: 0x06009703 RID: 38659 RVA: 0x0021E09F File Offset: 0x0021C29F
		// (set) Token: 0x06009704 RID: 38660 RVA: 0x0021E0A7 File Offset: 0x0021C2A7
		[Category("Appearance")]
		[Description("Whether to output the control scripts automatically")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public override bool EnableEmbeddedScripts
		{
			get
			{
				return base.EnableEmbeddedScripts;
			}
			set
			{
				if (base.ChildControlsCreated)
				{
					this._dialogOpener.EnableEmbeddedScripts = value;
				}
				base.EnableEmbeddedScripts = value;
			}
		}

		// Token: 0x17002FAB RID: 12203
		// (get) Token: 0x06009705 RID: 38661 RVA: 0x0021E0C4 File Offset: 0x0021C2C4
		// (set) Token: 0x06009706 RID: 38662 RVA: 0x0021E0CC File Offset: 0x0021C2CC
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[Description("Whether to register the selected skin automatically")]
		[DefaultValue(true)]
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return base.EnableEmbeddedSkins;
			}
			set
			{
				if (base.ChildControlsCreated)
				{
					this._dialogOpener.EnableEmbeddedSkins = value;
				}
				base.EnableEmbeddedSkins = value;
			}
		}

		// Token: 0x17002FAC RID: 12204
		// (get) Token: 0x06009707 RID: 38663 RVA: 0x0021E0E9 File Offset: 0x0021C2E9
		// (set) Token: 0x06009708 RID: 38664 RVA: 0x0021E0F1 File Offset: 0x0021C2F1
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[Description("Whether to register the base control skin file automatically")]
		[DefaultValue(true)]
		public override bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return base.EnableEmbeddedBaseStylesheet;
			}
			set
			{
				if (base.ChildControlsCreated)
				{
					this._dialogOpener.EnableEmbeddedBaseStylesheet = value;
				}
				base.EnableEmbeddedBaseStylesheet = value;
			}
		}

		// Token: 0x17002FAD RID: 12205
		// (get) Token: 0x06009709 RID: 38665 RVA: 0x0021E10E File Offset: 0x0021C30E
		// (set) Token: 0x0600970A RID: 38666 RVA: 0x0021E12F File Offset: 0x0021C32F
		[Category("Behavior")]
		[Description("Whether to render the editor as a simple textarea (for compatibility with older browsers)")]
		[DefaultValue(false)]
		public bool EnableTextareaMode
		{
			get
			{
				return (bool)(this.ViewState["EnableTextareaMode"] ?? false);
			}
			set
			{
				this.ViewState["EnableTextareaMode"] = value;
			}
		}

		// Token: 0x17002FAE RID: 12206
		// (get) Token: 0x0600970B RID: 38667 RVA: 0x0021E160 File Offset: 0x0021C360
		// (set) Token: 0x0600970C RID: 38668 RVA: 0x0021E383 File Offset: 0x0021C583
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Bindable(false)]
		[Description("Gets the text content of the RadEditor control without the HTML markup.")]
		public string Text
		{
			get
			{
				if (string.IsNullOrEmpty(this.Content))
				{
					return string.Empty;
				}
				if (this._text == null)
				{
					this._text = this.Content;
					this._text = this._text.Replace("\r", " ");
					this._text = this._text.Replace("\n", " ");
					this._text = this._text.Replace("\t", string.Empty);
					this._text = Regex.Replace(this._text, " +", " ");
					this._text = Regex.Replace(this._text, "<head[^>]*>[\\s\\S]*?</head>", string.Empty, RegexOptions.IgnoreCase);
					this._text = Regex.Replace(this._text, "<script[^>]*>[\\s\\S]*?</script>", string.Empty, RegexOptions.IgnoreCase);
					this._text = Regex.Replace(this._text, "<style[^>]*>[\\s\\S]*?</style>", string.Empty, RegexOptions.IgnoreCase);
					this._text = Regex.Replace(this._text, "<!--[\\s\\S]*?-->", string.Empty, RegexOptions.IgnoreCase);
					this._text = Regex.Replace(this._text, "<(td|th)[^>]*>", "\t", RegexOptions.IgnoreCase);
					this._text = Regex.Replace(this._text, "<(br|li|h1|h2|h3|h4|h5|h6)[^>]*>", "\n", RegexOptions.IgnoreCase);
					this._text = Regex.Replace(this._text, "<(div|tr|p)[^>]*>", "\n\n", RegexOptions.IgnoreCase);
					this._text = Regex.Replace(this._text, "<[^>]*>", string.Empty, RegexOptions.IgnoreCase);
					this._text = Regex.Replace(this._text, "\\&[^;]+;", (Match m) => this.Context.Server.HtmlDecode(m.Value), RegexOptions.IgnoreCase);
					this._text = Regex.Replace(this._text, "^[\\n\\s]+", string.Empty, RegexOptions.IgnoreCase);
					this._text = Regex.Replace(this._text, "[\\n\\s]+$", string.Empty, RegexOptions.IgnoreCase);
					this._text = Regex.Replace(this._text, "\\n[\\s]+\\n", "\n\n", RegexOptions.IgnoreCase);
					this._text = Regex.Replace(this._text, "\\n\\n[\\n]+", "\n\n", RegexOptions.IgnoreCase);
				}
				return this._text;
			}
			set
			{
				throw new InvalidOperationException("Please, use the Content property of RadEditor to set its content. \r\nThe value of the Text property is generated using the value of the Content property.");
			}
		}

		// Token: 0x17002FAF RID: 12207
		// (get) Token: 0x0600970D RID: 38669 RVA: 0x0021E38F File Offset: 0x0021C58F
		// (set) Token: 0x0600970E RID: 38670 RVA: 0x0021E39D File Offset: 0x0021C59D
		[ClientControlProperty]
		[DefaultValue(EditModes.All)]
		[Editor("Telerik.Web.Design.Common.FlagEnumUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public EditModes EditModes
		{
			get
			{
				return base.GetViewStateValue<EditModes>("EditModes", EditModes.All);
			}
			set
			{
				this.ViewState["EditModes"] = value;
			}
		}

		// Token: 0x17002FB0 RID: 12208
		// (get) Token: 0x0600970F RID: 38671 RVA: 0x0021E3B5 File Offset: 0x0021C5B5
		// (set) Token: 0x06009710 RID: 38672 RVA: 0x0021E3C3 File Offset: 0x0021C5C3
		[Category("Behavior")]
		[Description("Specifies the edit type of the Editor")]
		[DefaultValue(EditorEditType.Normal)]
		[ClientControlProperty]
		public EditorEditType EditType
		{
			get
			{
				return base.GetViewStateValue<EditorEditType>("EditType", EditorEditType.Normal);
			}
			set
			{
				this.ViewState["EditType"] = value;
			}
		}

		// Token: 0x17002FB1 RID: 12209
		// (get) Token: 0x06009711 RID: 38673 RVA: 0x0021E3DB File Offset: 0x0021C5DB
		// (set) Token: 0x06009712 RID: 38674 RVA: 0x0021E3E3 File Offset: 0x0021C5E3
		[Description("Gets or sets the text content of the RadEditor control including the HTML markup. The Html property is deprecated in RadEditor for ASP.NET Ajax")]
		[Obsolete("The Html property is deprecated in RadEditor for ASP.NET Ajax. Use Content instead.", false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string Html
		{
			get
			{
				return this.Content;
			}
			set
			{
				this.Content = value;
			}
		}

		// Token: 0x17002FB2 RID: 12210
		// (get) Token: 0x06009713 RID: 38675 RVA: 0x0021E3EC File Offset: 0x0021C5EC
		// (set) Token: 0x06009714 RID: 38676 RVA: 0x0021E400 File Offset: 0x0021C600
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Themeable(true)]
		[Category("Behavior")]
		[Bindable(true)]
		[Description("Gets or sets the text content of the RadEditor control including the HTML markup.")]
		public string Content
		{
			get
			{
				return base.GetViewStateValue<string>("Content", string.Empty);
			}
			set
			{
				string text = value ?? string.Empty;
				if (this.IsFilterEnabled(EditorFilters.RemoveScripts))
				{
					text = RadEditor.RemoveScriptBlocks(text);
				}
				if (this.IsFilterEnabled(EditorFilters.StripCssExpressions))
				{
					text = this._cssExpressionSanitizer.Sanitize(text);
				}
				if (this.IsFilterEnabled(EditorFilters.StripDomEventAttributes))
				{
					text = this._domEventsSanitizer.Sanitize(text);
				}
				this.ViewState["Content"] = text;
				this._text = null;
			}
		}

		// Token: 0x17002FB3 RID: 12211
		// (get) Token: 0x06009715 RID: 38677 RVA: 0x0021E474 File Offset: 0x0021C674
		// (set) Token: 0x06009716 RID: 38678 RVA: 0x0021E4B3 File Offset: 0x0021C6B3
		[Category("Dialog Configuration")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ImageManagerDialogConfiguration ImageManager
		{
			get
			{
				if (this._imageManager == null)
				{
					this._imageManager = new ImageManagerDialogConfiguration();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._imageManager).TrackViewState();
					}
					this._imageManager.RenderMode = this.ResolvedRenderMode;
				}
				return this._imageManager;
			}
			set
			{
				this._imageManager = value;
			}
		}

		// Token: 0x17002FB4 RID: 12212
		// (get) Token: 0x06009717 RID: 38679 RVA: 0x0021E4BC File Offset: 0x0021C6BC
		// (set) Token: 0x06009718 RID: 38680 RVA: 0x0021E4FB File Offset: 0x0021C6FB
		[Category("Dialog Configuration")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public FileManagerDialogConfiguration DocumentManager
		{
			get
			{
				if (this._documentManager == null)
				{
					this._documentManager = new FileManagerDialogConfiguration();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._documentManager).TrackViewState();
					}
					this._documentManager.RenderMode = this.ResolvedRenderMode;
				}
				return this._documentManager;
			}
			set
			{
				this._documentManager = value;
			}
		}

		// Token: 0x17002FB5 RID: 12213
		// (get) Token: 0x06009719 RID: 38681 RVA: 0x0021E504 File Offset: 0x0021C704
		// (set) Token: 0x0600971A RID: 38682 RVA: 0x0021E543 File Offset: 0x0021C743
		[Category("Dialog Configuration")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public FileManagerDialogConfiguration FlashManager
		{
			get
			{
				if (this._flashManager == null)
				{
					this._flashManager = new FileManagerDialogConfiguration();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._flashManager).TrackViewState();
					}
					this._flashManager.RenderMode = this.ResolvedRenderMode;
				}
				return this._flashManager;
			}
			set
			{
				this._flashManager = value;
			}
		}

		// Token: 0x17002FB6 RID: 12214
		// (get) Token: 0x0600971B RID: 38683 RVA: 0x0021E54C File Offset: 0x0021C74C
		// (set) Token: 0x0600971C RID: 38684 RVA: 0x0021E58B File Offset: 0x0021C78B
		[Category("Dialog Configuration")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public FileManagerDialogConfiguration SilverlightManager
		{
			get
			{
				if (this._silverlightManager == null)
				{
					this._silverlightManager = new FileManagerDialogConfiguration();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._silverlightManager).TrackViewState();
					}
					this._silverlightManager.RenderMode = this.ResolvedRenderMode;
				}
				return this._silverlightManager;
			}
			set
			{
				this._silverlightManager = value;
			}
		}

		// Token: 0x17002FB7 RID: 12215
		// (get) Token: 0x0600971D RID: 38685 RVA: 0x0021E594 File Offset: 0x0021C794
		// (set) Token: 0x0600971E RID: 38686 RVA: 0x0021E5D3 File Offset: 0x0021C7D3
		[Category("Dialog Configuration")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public FileManagerDialogConfiguration MediaManager
		{
			get
			{
				if (this._mediaManager == null)
				{
					this._mediaManager = new FileManagerDialogConfiguration();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._mediaManager).TrackViewState();
					}
					this._mediaManager.RenderMode = this.ResolvedRenderMode;
				}
				return this._mediaManager;
			}
			set
			{
				this._mediaManager = value;
			}
		}

		// Token: 0x17002FB8 RID: 12216
		// (get) Token: 0x0600971F RID: 38687 RVA: 0x0021E5DC File Offset: 0x0021C7DC
		// (set) Token: 0x06009720 RID: 38688 RVA: 0x0021E61B File Offset: 0x0021C81B
		[Category("Dialog Configuration")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public FileManagerDialogConfiguration TemplateManager
		{
			get
			{
				if (this._templateManager == null)
				{
					this._templateManager = new FileManagerDialogConfiguration();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._templateManager).TrackViewState();
					}
					this._templateManager.RenderMode = this.ResolvedRenderMode;
				}
				return this._templateManager;
			}
			set
			{
				this._templateManager = value;
			}
		}

		// Token: 0x17002FB9 RID: 12217
		// (get) Token: 0x06009721 RID: 38689 RVA: 0x0021E624 File Offset: 0x0021C824
		[Category("Dialog Configuration")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SpellCheckSettings SpellCheckSettings
		{
			get
			{
				if (this._spellCheckSettings == null)
				{
					this._spellCheckSettings = new SpellCheckSettings();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._spellCheckSettings).TrackViewState();
					}
				}
				return this._spellCheckSettings;
			}
		}

		// Token: 0x17002FBA RID: 12218
		// (get) Token: 0x06009722 RID: 38690 RVA: 0x0021E652 File Offset: 0x0021C852
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Tools")]
		public TrackChangesSettings TrackChangesSettings
		{
			get
			{
				if (this._trackChangesSettings == null)
				{
					this._trackChangesSettings = new TrackChangesSettings();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._trackChangesSettings).TrackViewState();
					}
				}
				return this._trackChangesSettings;
			}
		}

		// Token: 0x17002FBB RID: 12219
		// (get) Token: 0x06009723 RID: 38691 RVA: 0x0021E680 File Offset: 0x0021C880
		[DefaultValue("")]
		[ClientPropertyName("DialogDefinitions")]
		private DialogDefinitionDictionary DialogDefinitions
		{
			get
			{
				this.EnsureChildControls();
				return this._dialogOpener.DialogDefinitions;
			}
		}

		// Token: 0x17002FBC RID: 12220
		// (get) Token: 0x06009724 RID: 38692 RVA: 0x0021E693 File Offset: 0x0021C893
		// (set) Token: 0x06009725 RID: 38693 RVA: 0x0021E69B File Offset: 0x0021C89B
		[Category("Behavior")]
		[Description("This property is obsolete. Please, use the StripFormattingOptions property instead.")]
		[Obsolete("This property is obsolete. Please, use the StripFormattingOptions property instead.", false)]
		[DefaultValue(EditorStripFormattingOptions.None)]
		public EditorStripFormattingOptions StripFormattingOnPaste
		{
			get
			{
				return this.StripFormattingOptions;
			}
			set
			{
				this.StripFormattingOptions = value;
			}
		}

		// Token: 0x17002FBD RID: 12221
		// (get) Token: 0x06009726 RID: 38694 RVA: 0x0021E6A4 File Offset: 0x0021C8A4
		// (set) Token: 0x06009727 RID: 38695 RVA: 0x0021E6B2 File Offset: 0x0021C8B2
		[DefaultValue(EditorStripFormattingOptions.None)]
		[Editor("Telerik.Web.Design.Common.FlagEnumUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Behavior")]
		[ClientControlProperty]
		[Description("Gets or sets the value indicating how the editor should clear the HTML formatting when the user pastes data into the content area.")]
		public EditorStripFormattingOptions StripFormattingOptions
		{
			get
			{
				return base.GetViewStateValue<EditorStripFormattingOptions>("StripFormattingOptions", EditorStripFormattingOptions.None);
			}
			set
			{
				this.ViewState["StripFormattingOptions"] = value;
			}
		}

		// Token: 0x17002FBE RID: 12222
		// (get) Token: 0x06009728 RID: 38696 RVA: 0x0021E6CA File Offset: 0x0021C8CA
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		private ContentAreaSettings ContentAreaSettings
		{
			get
			{
				if (this._contentAreaSettings == null)
				{
					this._contentAreaSettings = new ContentAreaSettings();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._contentAreaSettings).TrackViewState();
					}
				}
				return this._contentAreaSettings;
			}
		}

		// Token: 0x17002FBF RID: 12223
		// (get) Token: 0x06009729 RID: 38697 RVA: 0x0021E6F8 File Offset: 0x0021C8F8
		// (set) Token: 0x0600972A RID: 38698 RVA: 0x0021E70B File Offset: 0x0021C90B
		[Category("Dialog Configuration")]
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		[Description("Gets or sets the URL which the AJAX call will be made to. Check the help for more information.")]
		[DefaultValue("Telerik.Web.UI.DialogHandler.aspx")]
		[UrlProperty]
		public string DialogHandlerUrl
		{
			get
			{
				this.EnsureChildControls();
				return this._dialogOpener.HandlerUrl;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.EnsureChildControls();
					this._dialogOpener.HandlerUrl = value;
				}
			}
		}

		// Token: 0x17002FC0 RID: 12224
		// (get) Token: 0x0600972B RID: 38699 RVA: 0x0021E727 File Offset: 0x0021C927
		// (set) Token: 0x0600972C RID: 38700 RVA: 0x0021E73A File Offset: 0x0021C93A
		[Category("Dialog Configuration")]
		[DefaultValue("")]
		[UrlProperty("*.css")]
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		public string DialogsCssFile
		{
			get
			{
				this.EnsureChildControls();
				return this._dialogOpener.DialogsCssFile;
			}
			set
			{
				this.EnsureChildControls();
				this._dialogOpener.DialogsCssFile = value;
			}
		}

		// Token: 0x17002FC1 RID: 12225
		// (get) Token: 0x0600972D RID: 38701 RVA: 0x0021E74E File Offset: 0x0021C94E
		// (set) Token: 0x0600972E RID: 38702 RVA: 0x0021E761 File Offset: 0x0021C961
		[DefaultValue("")]
		[UrlProperty("*.js")]
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		[Category("Dialog Configuration")]
		public string DialogsScriptFile
		{
			get
			{
				this.EnsureChildControls();
				return this._dialogOpener.DialogsScriptFile;
			}
			set
			{
				this.EnsureChildControls();
				this._dialogOpener.DialogsScriptFile = value;
			}
		}

		// Token: 0x17002FC2 RID: 12226
		// (get) Token: 0x0600972F RID: 38703 RVA: 0x0021E775 File Offset: 0x0021C975
		public RadDialogOpener DialogOpener
		{
			get
			{
				this.EnsureChildControls();
				return this._dialogOpener;
			}
		}

		// Token: 0x17002FC3 RID: 12227
		// (get) Token: 0x06009730 RID: 38704 RVA: 0x0021E783 File Offset: 0x0021C983
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual RadRibbonBar RibbonBar
		{
			get
			{
				if (this._ribbonbar == null || !this._ribbonbar.Visible)
				{
					return null;
				}
				return this._ribbonbar;
			}
		}

		// Token: 0x17002FC4 RID: 12228
		// (get) Token: 0x06009731 RID: 38705 RVA: 0x0021E7A2 File Offset: 0x0021C9A2
		// (set) Token: 0x06009732 RID: 38706 RVA: 0x0021E7AC File Offset: 0x0021C9AC
		[NotifyParentProperty(true)]
		[DefaultValue(RenderMode.Classic)]
		[Description("Specifies the rendering mode of the control")]
		[Category("Appearance")]
		public override RenderMode RenderMode
		{
			get
			{
				return base.RenderMode;
			}
			set
			{
				bool flag = value == RenderMode.Mobile || base.RenderMode == RenderMode.Mobile;
				base.RenderMode = value;
				if (base.ChildControlsCreated)
				{
					this.SetRenderModeChildRadControls();
				}
				if (this.initialized && flag)
				{
					this.ReloadTools();
				}
			}
		}

		// Token: 0x17002FC5 RID: 12229
		// (get) Token: 0x06009733 RID: 38707 RVA: 0x0021E7F0 File Offset: 0x0021C9F0
		// (set) Token: 0x06009734 RID: 38708 RVA: 0x0021E802 File Offset: 0x0021CA02
		[Editor("Telerik.Web.Design.Common.FlagEnumUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Behavior")]
		[Description("Gets or sets a value indicating which content filters will be active when the editor is loaded in the browser.")]
		[ClientControlProperty]
		[DefaultValue(EditorFilters.DefaultFilters)]
		public EditorFilters ContentFilters
		{
			get
			{
				return base.GetViewStateValue<EditorFilters>("ContentFilters", EditorFilters.DefaultFilters);
			}
			set
			{
				this.ViewState["ContentFilters"] = value;
			}
		}

		// Token: 0x17002FC6 RID: 12230
		// (get) Token: 0x06009735 RID: 38709 RVA: 0x0021E81A File Offset: 0x0021CA1A
		// (set) Token: 0x06009736 RID: 38710 RVA: 0x0021E83C File Offset: 0x0021CA3C
		[Category("Dialog Configuration")]
		[Description("Gets or sets a value indicating where the editor will look for its dialogs.")]
		[DefaultValue("")]
		public string ExternalDialogsPath
		{
			get
			{
				return ((string)this.ViewState["ExternalDialogsPath"]) ?? string.Empty;
			}
			set
			{
				string text = value.Replace("\\", "/");
				if (text.Length > 0 && !text.EndsWith("/"))
				{
					text += "/";
				}
				this.ViewState["ExternalDialogsPath"] = text;
			}
		}

		// Token: 0x17002FC7 RID: 12231
		// (get) Token: 0x06009737 RID: 38711 RVA: 0x0021E88F File Offset: 0x0021CA8F
		// (set) Token: 0x06009738 RID: 38712 RVA: 0x0021E89D File Offset: 0x0021CA9D
		[MergableProperty(true)]
		[DefaultValue(EditorContentAreaMode.Iframe)]
		[Description("Gets or sets the rendering mode of the editor content area.")]
		[Category("Behavior")]
		[ClientControlProperty]
		public EditorContentAreaMode ContentAreaMode
		{
			get
			{
				return base.GetViewStateValue<EditorContentAreaMode>("ContentAreaMode", EditorContentAreaMode.Iframe);
			}
			set
			{
				this.ViewState["ContentAreaMode"] = value;
				this._contentAreaMode = new EditorContentAreaMode?(value);
			}
		}

		// Token: 0x17002FC8 RID: 12232
		// (get) Token: 0x06009739 RID: 38713 RVA: 0x0021E8C1 File Offset: 0x0021CAC1
		// (set) Token: 0x0600973A RID: 38714 RVA: 0x0021E8CF File Offset: 0x0021CACF
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("enableAriaSupport")]
		[DefaultValue(false)]
		[Description("When set to true enables support for WAI-ARIA")]
		public bool EnableAriaSupport
		{
			get
			{
				return base.GetViewStateValue<bool>("EnableAriaSupport", false);
			}
			set
			{
				this.ViewState["EnableAriaSupport"] = value;
			}
		}

		// Token: 0x17002FC9 RID: 12233
		// (get) Token: 0x0600973B RID: 38715 RVA: 0x0021E8E7 File Offset: 0x0021CAE7
		// (set) Token: 0x0600973C RID: 38716 RVA: 0x0021E8F5 File Offset: 0x0021CAF5
		[DefaultValue(false)]
		[ClientControlProperty]
		[Category("Behavior")]
		[MergableProperty(true)]
		[Description("When set to true enables support for entering comments in the editor content.")]
		public bool EnableComments
		{
			get
			{
				return base.GetViewStateValue<bool>("EnableComments", false);
			}
			set
			{
				this.ViewState["EnableComments"] = value;
				if (this._toolsFileLoaded)
				{
					this._defaultContextMenus = null;
					this.ResetToolsFileContent();
					this.LoadToolsFile(false);
				}
			}
		}

		// Token: 0x17002FCA RID: 12234
		// (get) Token: 0x0600973D RID: 38717 RVA: 0x0021E929 File Offset: 0x0021CB29
		// (set) Token: 0x0600973E RID: 38718 RVA: 0x0021E937 File Offset: 0x0021CB37
		[DefaultValue(false)]
		[Category("Behavior")]
		[MergableProperty(true)]
		[ClientControlProperty]
		[Description("When set to true enables support for tracking changes each time the editor content is modified.")]
		public bool EnableTrackChanges
		{
			get
			{
				return base.GetViewStateValue<bool>("EnableTrackChanges", false);
			}
			set
			{
				this.ViewState["EnableTrackChanges"] = value;
			}
		}

		// Token: 0x17002FCB RID: 12235
		// (get) Token: 0x0600973F RID: 38719 RVA: 0x0021E94F File Offset: 0x0021CB4F
		// (set) Token: 0x06009740 RID: 38720 RVA: 0x0021E961 File Offset: 0x0021CB61
		[Description("Message that will be shown over the content area when the editor is empty.")]
		[DefaultValue("")]
		[Category("Behavior")]
		[MergableProperty(true)]
		[ClientControlProperty]
		public string EmptyMessage
		{
			get
			{
				return base.GetViewStateValue<string>("EmptyMessage", string.Empty);
			}
			set
			{
				this.ViewState["EmptyMessage"] = value;
			}
		}

		// Token: 0x17002FCC RID: 12236
		// (get) Token: 0x06009741 RID: 38721 RVA: 0x0021E974 File Offset: 0x0021CB74
		// (set) Token: 0x06009742 RID: 38722 RVA: 0x0021E982 File Offset: 0x0021CB82
		[DefaultValue(false)]
		[ClientControlProperty]
		[Description("When set to true enables support for immutable HTML elements.")]
		[Category("Behavior")]
		[MergableProperty(true)]
		public bool EnableImmutableElements
		{
			get
			{
				return base.GetViewStateValue<bool>("EnableImmutableElements", false);
			}
			set
			{
				this.ViewState["EnableImmutableElements"] = value;
			}
		}

		// Token: 0x17002FCD RID: 12237
		// (get) Token: 0x06009743 RID: 38723 RVA: 0x0021E99A File Offset: 0x0021CB9A
		// (set) Token: 0x06009744 RID: 38724 RVA: 0x0021E9A8 File Offset: 0x0021CBA8
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Specifies whether the RadContextMenu should be used.")]
		public bool UseRadContextMenu
		{
			get
			{
				return base.GetViewStateValue<bool>("UseRadContextMenu", true);
			}
			set
			{
				this.ViewState["UseRadContextMenu"] = value;
			}
		}

		// Token: 0x17002FCE RID: 12238
		// (get) Token: 0x06009745 RID: 38725 RVA: 0x0021E9C0 File Offset: 0x0021CBC0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadContextMenu RadContextMenu
		{
			get
			{
				if (this._radContextMenu == null)
				{
					return null;
				}
				return this._radContextMenu;
			}
		}

		// Token: 0x17002FCF RID: 12239
		// (get) Token: 0x06009746 RID: 38726 RVA: 0x0021E9D2 File Offset: 0x0021CBD2
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Specifies the animation settings.")]
		[Category("Appearance")]
		public EditorAnimationSettings AnimationSettings
		{
			get
			{
				if (this._animationSettings == null)
				{
					this._animationSettings = new EditorAnimationSettings();
				}
				return this._animationSettings;
			}
		}

		// Token: 0x17002FD0 RID: 12240
		// (get) Token: 0x06009747 RID: 38727 RVA: 0x0021E9ED File Offset: 0x0021CBED
		// (set) Token: 0x06009748 RID: 38728 RVA: 0x0021EA10 File Offset: 0x0021CC10
		[Description("Contains import Rtf/Docx functionality.")]
		public IDplImportProxy DocumentImporter
		{
			get
			{
				if (this._documentImporter == null && !base.DesignMode)
				{
					this._documentImporter = new DplImportProxy();
				}
				return this._documentImporter;
			}
			set
			{
				this._documentImporter = value;
			}
		}

		// Token: 0x14000169 RID: 361
		// (add) Token: 0x06009749 RID: 38729 RVA: 0x0021EA19 File Offset: 0x0021CC19
		// (remove) Token: 0x0600974A RID: 38730 RVA: 0x0021EA2D File Offset: 0x0021CC2D
		[SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
		public event EditorDialogEventHandler FileDelete
		{
			add
			{
				base.Events.AddHandler(this.FileDeleteEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(this.FileDeleteEvent, value);
			}
		}

		// Token: 0x0600974B RID: 38731 RVA: 0x0021EA44 File Offset: 0x0021CC44
		protected virtual void OnFileDelete(string fileName)
		{
			EditorDialogEventHandler editorDialogEventHandler = (EditorDialogEventHandler)base.Events[this.FileDeleteEvent];
			if (editorDialogEventHandler != null)
			{
				editorDialogEventHandler(this, fileName);
			}
		}

		// Token: 0x1400016A RID: 362
		// (add) Token: 0x0600974C RID: 38732 RVA: 0x0021EA74 File Offset: 0x0021CC74
		// (remove) Token: 0x0600974D RID: 38733 RVA: 0x0021EA88 File Offset: 0x0021CC88
		public event EditorExportContentEventHandler ExportContent
		{
			add
			{
				base.Events.AddHandler(this.ExportContentEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(this.ExportContentEvent, value);
			}
		}

		// Token: 0x0600974E RID: 38734 RVA: 0x0021EA9C File Offset: 0x0021CC9C
		internal virtual void OnExportContent(EditorExportingArgs e)
		{
			EditorExportContentEventHandler editorExportContentEventHandler = (EditorExportContentEventHandler)base.Events[this.ExportContentEvent];
			if (editorExportContentEventHandler != null)
			{
				editorExportContentEventHandler(this, e);
			}
		}

		// Token: 0x1400016B RID: 363
		// (add) Token: 0x0600974F RID: 38735 RVA: 0x0021EACB File Offset: 0x0021CCCB
		// (remove) Token: 0x06009750 RID: 38736 RVA: 0x0021EADF File Offset: 0x0021CCDF
		public event EditorImportContentEventHandler ImportContent
		{
			add
			{
				base.Events.AddHandler(this.ImportContentEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(this.ImportContentEvent, value);
			}
		}

		// Token: 0x06009751 RID: 38737 RVA: 0x0021EAF4 File Offset: 0x0021CCF4
		internal virtual void OnImportContent(EditorImportingArgs e)
		{
			EditorImportContentEventHandler editorImportContentEventHandler = (EditorImportContentEventHandler)base.Events[this.ImportContentEvent];
			if (editorImportContentEventHandler != null)
			{
				editorImportContentEventHandler(this, e);
			}
		}

		// Token: 0x1400016C RID: 364
		// (add) Token: 0x06009752 RID: 38738 RVA: 0x0021EB23 File Offset: 0x0021CD23
		// (remove) Token: 0x06009753 RID: 38739 RVA: 0x0021EB37 File Offset: 0x0021CD37
		[SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
		public event EditorDialogEventHandler FileUpload
		{
			add
			{
				base.Events.AddHandler(this.FileUploadEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(this.FileUploadEvent, value);
			}
		}

		// Token: 0x06009754 RID: 38740 RVA: 0x0021EB4C File Offset: 0x0021CD4C
		protected virtual void OnFileUpload(string fileName)
		{
			EditorDialogEventHandler editorDialogEventHandler = (EditorDialogEventHandler)base.Events[this.FileUploadEvent];
			if (editorDialogEventHandler != null)
			{
				editorDialogEventHandler(this, fileName);
			}
		}

		// Token: 0x1400016D RID: 365
		// (add) Token: 0x06009755 RID: 38741 RVA: 0x0021EB7C File Offset: 0x0021CD7C
		// (remove) Token: 0x06009756 RID: 38742 RVA: 0x0021EB8F File Offset: 0x0021CD8F
		public event EventHandler TextChanged
		{
			add
			{
				base.Events.AddHandler(RadEditor.TextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadEditor.TextChangedEvent, value);
			}
		}

		// Token: 0x06009757 RID: 38743 RVA: 0x0021EBA4 File Offset: 0x0021CDA4
		protected virtual void OnTextChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RadEditor.TextChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x17002FD1 RID: 12241
		// (get) Token: 0x06009758 RID: 38744 RVA: 0x0021EBD2 File Offset: 0x0021CDD2
		// (set) Token: 0x06009759 RID: 38745 RVA: 0x0021EBDA File Offset: 0x0021CDDA
		string ITextControl.Text
		{
			get
			{
				return this.Content;
			}
			set
			{
				this.Content = value;
			}
		}

		// Token: 0x17002FD2 RID: 12242
		// (get) Token: 0x0600975A RID: 38746 RVA: 0x0021EBE4 File Offset: 0x0021CDE4
		private bool isRibbonBarVisible
		{
			get
			{
				EditorToolbarMode toolbarMode = this.ToolbarMode;
				if (toolbarMode <= EditorToolbarMode.RibbonBarFloating)
				{
					if (toolbarMode != EditorToolbarMode.RibbonBar && toolbarMode != EditorToolbarMode.RibbonBarFloating)
					{
						return false;
					}
				}
				else if (toolbarMode != EditorToolbarMode.RibbonBarPageTop && toolbarMode != EditorToolbarMode.RibbonBarShowOnFocus)
				{
					return false;
				}
				return base.IsEnabled;
			}
		}

		// Token: 0x17002FD3 RID: 12243
		// (get) Token: 0x0600975B RID: 38747 RVA: 0x0021EC1E File Offset: 0x0021CE1E
		internal HttpContext ControlContext
		{
			get
			{
				return this.Context;
			}
		}

		// Token: 0x17002FD4 RID: 12244
		// (get) Token: 0x0600975C RID: 38748 RVA: 0x0021EC26 File Offset: 0x0021CE26
		internal bool InDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x0600975D RID: 38749 RVA: 0x0021EC30 File Offset: 0x0021CE30
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "autoResizeHeight", this.AutoResizeHeight, false);
			base.DescribeProperty<EditorContentAreaMode>(descriptor, "contentAreaMode", this.ContentAreaMode, EditorContentAreaMode.Iframe);
			base.DescribeProperty<EditorFilters>(descriptor, "contentFilters", this.ContentFilters, EditorFilters.DefaultFilters);
			base.DescribeProperty<EditModes>(descriptor, "editModes", this.EditModes, EditModes.All);
			base.DescribeProperty<EditorEditType>(descriptor, "editType", this.EditType, EditorEditType.Normal);
			base.DescribeProperty<string>(descriptor, "emptyMessage", this.EmptyMessage, "");
			base.DescribeProperty<bool>(descriptor, "enableAriaSupport", this.EnableAriaSupport, false);
			base.DescribeProperty<bool>(descriptor, "enableComments", this.EnableComments, false);
			base.DescribeProperty<bool>(descriptor, "enableImmutableElements", this.EnableImmutableElements, false);
			base.DescribeProperty<bool>(descriptor, "enableTrackChanges", this.EnableTrackChanges, false);
			base.DescribeProperty<bool>(descriptor, "isSkinTouch", this.IsSkinTouch, false);
			base.DescribeProperty<string>(descriptor, "language", this.Language, "en-US");
			base.DescribeProperty<int>(descriptor, "maxHtmlLength", this.MaxHtmlLength, 0);
			base.DescribeProperty<int>(descriptor, "maxTextLength", this.MaxTextLength, 0);
			base.DescribeProperty<EditorNewLineModes>(descriptor, "newLineMode", this.NewLineMode, EditorNewLineModes.P);
			base.DescribeProperty<EditorStripFormattingOptions>(descriptor, "stripFormattingOptions", this.StripFormattingOptions, EditorStripFormattingOptions.None);
			base.DescribeProperty<EditorToolbarMode>(descriptor, "toolbarMode", this.ToolbarMode, EditorToolbarMode.Default);
			base.DescribeProperty<string>(descriptor, "toolsWidth", this.ToolsWidth.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600975E RID: 38750 RVA: 0x0021EDB8 File Offset: 0x0021CFB8
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "commandExecuted", this.OnClientCommandExecuted);
			RadWebControl.DescribeEvent(descriptor, "commandExecuting", this.OnClientCommandExecuting);
			RadWebControl.DescribeEvent(descriptor, "domChange", this.OnClientDomChange);
			RadWebControl.DescribeEvent(descriptor, "init", this.OnClientInit);
			RadWebControl.DescribeEvent(descriptor, "inlineEditCompleted", this.OnClientInlineEditCompleted);
			RadWebControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadWebControl.DescribeEvent(descriptor, "modeChange", this.OnClientModeChange);
			RadWebControl.DescribeEvent(descriptor, "pasteHtml", this.OnClientPasteHtml);
			RadWebControl.DescribeEvent(descriptor, "selectionChange", this.OnClientSelectionChange);
			RadWebControl.DescribeEvent(descriptor, "submit", this.OnClientSubmit);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x06009762 RID: 38754 RVA: 0x0021EE76 File Offset: 0x0021D076
		// Note: this type is marked as 'beforefieldinit'.
		static RadEditor()
		{
			RadEditor.TextChangedEvent = new object();
		}

		// Token: 0x04002AFF RID: 11007
		private bool _filledDefaultDialogDefinitions;

		// Token: 0x04002B00 RID: 11008
		private ToolsFileLoader _toolsFileLoader;

		// Token: 0x04002B01 RID: 11009
		private XmlDocument _toolsFileContent;

		// Token: 0x04002B02 RID: 11010
		private EditorContextMenuCollection _defaultContextMenus;

		// Token: 0x04002B03 RID: 11011
		private bool originalEnabled = true;

		// Token: 0x04002B04 RID: 11012
		private bool initialized;

		// Token: 0x04002B05 RID: 11013
		private CultureInfo _culture;

		// Token: 0x04002B06 RID: 11014
		private EditorExportSettings _exportSettings;

		// Token: 0x04002B07 RID: 11015
		private EditorImportSettings _importSettings;

		// Token: 0x04002B08 RID: 11016
		private bool _isInAccessibleMode;

		// Token: 0x04002B09 RID: 11017
		private bool _isSkinTouch;

		// Token: 0x04002B0A RID: 11018
		private RadRibbonBar _ribbonbar;

		// Token: 0x04002B0B RID: 11019
		private EditorContentAreaMode? _contentAreaMode;

		// Token: 0x04002B0C RID: 11020
		private RadContextMenu _radContextMenu;

		// Token: 0x04002B0D RID: 11021
		private EditorAnimationSettings _animationSettings;

		// Token: 0x04002B0E RID: 11022
		private IDplImportProxy _documentImporter;

		// Token: 0x04002B14 RID: 11028
		private string _text;

		// Token: 0x04002B15 RID: 11029
		private RadDialogOpener _dialogOpener;

		// Token: 0x04002B16 RID: 11030
		private RadWebControl _ribbonbarResourcesHolder;

		// Token: 0x04002B17 RID: 11031
		private ToolAdapter _toolAdapter;

		// Token: 0x04002B18 RID: 11032
		private HeaderToolsToolAdapter _headerToolsToolAdapter;

		// Token: 0x04002B19 RID: 11033
		private EditorToolGroupCollection _tools;

		// Token: 0x04002B1A RID: 11034
		private EditorHeaderToolCollection _headerTools;

		// Token: 0x04002B1B RID: 11035
		internal bool _toolsFileLoaded;

		// Token: 0x04002B1C RID: 11036
		private EditorStrings _localization;

		// Token: 0x04002B1D RID: 11037
		private EditorCssFileCollection _cssFiles;

		// Token: 0x04002B1E RID: 11038
		private EditorContextMenuCollection _contextMenus;

		// Token: 0x04002B1F RID: 11039
		private EditorColorCollection _colors;

		// Token: 0x04002B20 RID: 11040
		private EditorSymbolCollection _symbols;

		// Token: 0x04002B21 RID: 11041
		private EditorLinkCollection _links;

		// Token: 0x04002B22 RID: 11042
		private EditorFontSizeCollection _fontSizes;

		// Token: 0x04002B23 RID: 11043
		private EditorFontCollection _fontNames;

		// Token: 0x04002B24 RID: 11044
		private EditorParagraphCollection _paragraphs;

		// Token: 0x04002B25 RID: 11045
		private EditorRealFontSizeCollection _realFontSizes;

		// Token: 0x04002B26 RID: 11046
		private EditorCssClassCollection _cssClasses;

		// Token: 0x04002B27 RID: 11047
		private EditorSnippetCollection _snippets;

		// Token: 0x04002B28 RID: 11048
		private SpellCheckerLanguageCollection _languages;

		// Token: 0x04002B29 RID: 11049
		private EditorFormatSetCollection _formatSets;

		// Token: 0x04002B2A RID: 11050
		private ImageManagerDialogConfiguration _imageManager;

		// Token: 0x04002B2B RID: 11051
		private FileManagerDialogConfiguration _documentManager;

		// Token: 0x04002B2C RID: 11052
		private FileManagerDialogConfiguration _flashManager;

		// Token: 0x04002B2D RID: 11053
		private FileManagerDialogConfiguration _silverlightManager;

		// Token: 0x04002B2E RID: 11054
		private FileManagerDialogConfiguration _mediaManager;

		// Token: 0x04002B2F RID: 11055
		private FileManagerDialogConfiguration _templateManager;

		// Token: 0x04002B30 RID: 11056
		private ContentAreaSettings _contentAreaSettings;

		// Token: 0x04002B31 RID: 11057
		private SpellCheckSettings _spellCheckSettings;

		// Token: 0x04002B32 RID: 11058
		private TrackChangesSettings _trackChangesSettings;

		// Token: 0x04002B33 RID: 11059
		private ITrackChangesAdapter _trackChangesAdapter;

		// Token: 0x04002B34 RID: 11060
		private CssExpressionSanitizer _cssExpressionSanitizer;

		// Token: 0x04002B35 RID: 11061
		private DomEventsSanitizer _domEventsSanitizer;

		// Token: 0x04002B36 RID: 11062
		private RadEditorExportTemplate _pdfExportTemplate;

		// Token: 0x04002B37 RID: 11063
		private RadEditorExportTemplate _rtfExportTemplate;

		// Token: 0x04002B38 RID: 11064
		private RadEditorExportTemplate _docxExportTemplate;

		// Token: 0x04002B39 RID: 11065
		private RadEditorExportTemplate _markdownExportTemplate;

		// Token: 0x04002B3A RID: 11066
		private FindReplaceMobile _findReplaceOverlay;

		// Token: 0x04002B3B RID: 11067
		private EditorModuleCollection _modules;

		// Token: 0x04002B3C RID: 11068
		private bool isInsertLinkEnabled;

		// Token: 0x04002B3D RID: 11069
		private bool isInsertImageEnabled;

		// Token: 0x04002B3E RID: 11070
		private bool isInsertTableEnabled;

		// Token: 0x02000F6C RID: 3948
		private enum ToolbarZone
		{
			// Token: 0x04002B40 RID: 11072
			Left,
			// Token: 0x04002B41 RID: 11073
			Right,
			// Token: 0x04002B42 RID: 11074
			Top,
			// Token: 0x04002B43 RID: 11075
			Center,
			// Token: 0x04002B44 RID: 11076
			Bottom
		}
	}
}
