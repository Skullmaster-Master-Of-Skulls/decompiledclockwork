using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020018E2 RID: 6370
	[ToolboxItem(false)]
	public class RadFilterContextMenu : RadContextMenu
	{
		// Token: 0x17004A08 RID: 18952
		// (get) Token: 0x0600F5DA RID: 62938 RVA: 0x0037CAC9 File Offset: 0x0037ACC9
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new RadMenuItemCollection Items
		{
			get
			{
				return base.Items;
			}
		}

		// Token: 0x17004A09 RID: 18953
		// (get) Token: 0x0600F5DB RID: 62939 RVA: 0x0037CAD1 File Offset: 0x0037ACD1
		// (set) Token: 0x0600F5DC RID: 62940 RVA: 0x0037CAD9 File Offset: 0x0037ACD9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[NotifyParentProperty(true)]
		[Browsable(false)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x17004A0A RID: 18954
		// (get) Token: 0x0600F5DD RID: 62941 RVA: 0x0037CAE2 File Offset: 0x0037ACE2
		// (set) Token: 0x0600F5DE RID: 62942 RVA: 0x0037CAEA File Offset: 0x0037ACEA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string ID
		{
			get
			{
				return base.ID;
			}
			set
			{
				base.ID = value;
			}
		}

		// Token: 0x17004A0B RID: 18955
		// (get) Token: 0x0600F5DF RID: 62943 RVA: 0x0037CAF3 File Offset: 0x0037ACF3
		// (set) Token: 0x0600F5E0 RID: 62944 RVA: 0x0037CAFB File Offset: 0x0037ACFB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		// Token: 0x0600F5E1 RID: 62945 RVA: 0x0037CB1C File Offset: 0x0037AD1C
		public RadFilterContextMenu(RadFilter owner)
		{
			this.owner = owner;
			this.ID = "rfContextMenu";
			this.RegisterWithScriptManager = true;
			this.EnableEmbeddedScripts = owner.EnableEmbeddedScripts;
			this.EnableEmbeddedSkins = owner.EnableEmbeddedSkins;
			this.EnableEmbeddedBaseStylesheet = owner.EnableEmbeddedBaseStylesheet;
			this.RenderMode = owner.RenderMode;
			base.EnableAriaSupport = owner.EnableAriaSupport;
			base.PreRender += delegate(object sender, EventArgs e)
			{
				((RadContextMenu)sender).Skin = this.owner.RuntimeSkin;
			};
			if (!base.DesignMode)
			{
				this.EnableTheming = owner.EnableTheming;
			}
		}

		// Token: 0x0600F5E2 RID: 62946 RVA: 0x0037CBB1 File Offset: 0x0037ADB1
		protected override void RenderScriptsNoScriptManager(HtmlTextWriter writer)
		{
		}

		// Token: 0x0600F5E3 RID: 62947 RVA: 0x0037CBB3 File Offset: 0x0037ADB3
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			if (!ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack && !this.EnableAjaxSkinRendering)
			{
				((ISkinnableControl)this).AjaxCssRegistrations = string.Empty;
			}
			base.RenderEndTag(writer);
		}

		// Token: 0x0600F5E4 RID: 62948 RVA: 0x0037CBE4 File Offset: 0x0037ADE4
		internal void BuildContextMenuItems()
		{
			this.Items.Clear();
			foreach (string value in Enum.GetNames(typeof(RadFilterGroupOperation)))
			{
				RadFilterGroupOperation radFilterGroupOperation = (RadFilterGroupOperation)Enum.Parse(typeof(RadFilterGroupOperation), value);
				if (this.owner.isGroupSupported(radFilterGroupOperation))
				{
					RadMenuItem radMenuItem = new RadMenuItem();
					radMenuItem.Attributes.Add("isGroupItem", "true");
					radMenuItem.Text = this.owner.Localization.RetrieveGroupLocalizationString(radFilterGroupOperation);
					radMenuItem.Value = value;
					radMenuItem.PostBack = false;
					this.Items.Add(radMenuItem);
				}
			}
			foreach (string value2 in Enum.GetNames(typeof(RadFilterFunction)))
			{
				RadFilterFunction function = (RadFilterFunction)Enum.Parse(typeof(RadFilterFunction), value2);
				if (this.owner.isFilterFunctionSupported(function))
				{
					RadMenuItem radMenuItem2 = new RadMenuItem();
					radMenuItem2.Attributes.Add("isFunctionItem", "true");
					radMenuItem2.Text = this.owner.Localization.RetrieveFilterFunctionLocalizationString(function);
					radMenuItem2.Value = value2;
					radMenuItem2.PostBack = false;
					this.Items.Add(radMenuItem2);
				}
			}
			ArrayList arrayList = new ArrayList();
			foreach (RadFilterDataFieldEditor radFilterDataFieldEditor in this.owner.FieldEditors)
			{
				RadMenuItem radMenuItem3 = new RadMenuItem();
				radMenuItem3.Attributes.Add("isFieldItem", "true");
				radMenuItem3.Text = radFilterDataFieldEditor.RetrieveDisplayText();
				radMenuItem3.Value = radFilterDataFieldEditor.FieldName;
				radMenuItem3.PostBack = false;
				if (!string.IsNullOrEmpty(radFilterDataFieldEditor.FieldName) && arrayList.Contains(radFilterDataFieldEditor.FieldName))
				{
					throw new InvalidOperationException("Cannot use multiple field editors with the same FieldName.");
				}
				arrayList.Add(radFilterDataFieldEditor.FieldName);
				this.Items.Add(radMenuItem3);
			}
		}

		// Token: 0x04004679 RID: 18041
		private RadFilter owner;
	}
}
