using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Web.Resources;

namespace System.Web.UI
{
	// Token: 0x0200008A RID: 138
	[DefaultProperty("AssociatedUpdatePanelID")]
	[Designer("System.Web.UI.Design.UpdateProgressDesigner, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[ToolboxBitmap(typeof(EmbeddedResourceFinder), "System.Web.Resources.UpdateProgress.bmp")]
	public class UpdateProgress : Control, IAttributeAccessor, IScriptControl
	{
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0001ABBB File Offset: 0x00018DBB
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x0001ABD1 File Offset: 0x00018DD1
		[Category("Behavior")]
		[DefaultValue("")]
		[IDReferenceProperty(typeof(UpdatePanel))]
		[ResourceDescription("UpdateProgress_AssociatedUpdatePanelID")]
		[TypeConverter("System.Web.UI.Design.UpdateProgressAssociatedUpdatePanelIDConverter")]
		public string AssociatedUpdatePanelID
		{
			get
			{
				if (this._associatedUpdatePanelID == null)
				{
					return string.Empty;
				}
				return this._associatedUpdatePanelID;
			}
			set
			{
				this._associatedUpdatePanelID = value;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0001ABDC File Offset: 0x00018DDC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("WebControl_Attributes")]
		public AttributeCollection Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					StateBag bag = new StateBag(true);
					this._attributes = new AttributeCollection(bag);
				}
				return this._attributes;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0001AC0A File Offset: 0x00018E0A
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0001AC18 File Offset: 0x00018E18
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x0001AC20 File Offset: 0x00018E20
		[DefaultValue(500)]
		[ResourceDescription("UpdateProgress_DisplayAfter")]
		[Category("Behavior")]
		public int DisplayAfter
		{
			get
			{
				return this._displayAfter;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(AtlasWeb.UpdateProgress_DisplayAfterInvalid);
				}
				this._displayAfter = value;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0001AC38 File Offset: 0x00018E38
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x0001AC40 File Offset: 0x00018E40
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[ResourceDescription("UpdateProgress_ProgressTemplate")]
		public ITemplate ProgressTemplate
		{
			get
			{
				return this._progressTemplate;
			}
			set
			{
				this._progressTemplate = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0001AC49 File Offset: 0x00018E49
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x0001AC51 File Offset: 0x00018E51
		[DefaultValue(true)]
		[ResourceDescription("UpdateProgress_DynamicLayout")]
		[Category("Behavior")]
		public bool DynamicLayout
		{
			get
			{
				return this._dynamicLayout;
			}
			set
			{
				this._dynamicLayout = value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0001AC5C File Offset: 0x00018E5C
		private ScriptManager ScriptManager
		{
			get
			{
				ScriptManager current = ScriptManager.GetCurrent(this.Page);
				if (current == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.Common_ScriptManagerRequired, new object[]
					{
						this.ID
					}));
				}
				return current;
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001AC9D File Offset: 0x00018E9D
		protected internal override void CreateChildControls()
		{
			if (this._progressTemplate != null)
			{
				this._progressTemplateContainer = new Control();
				this._progressTemplate.InstantiateIn(this._progressTemplateContainer);
				this.Controls.Add(this._progressTemplateContainer);
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001ACD4 File Offset: 0x00018ED4
		public override void DataBind()
		{
			this.EnsureChildControls();
			base.DataBind();
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001ACE2 File Offset: 0x00018EE2
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.ScriptManager.RegisterScriptControl<UpdateProgress>(this);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001ACF8 File Offset: 0x00018EF8
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			if (this._dynamicLayout)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			}
			else
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Visibility, "hidden");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "block");
			}
			if (this._attributes != null)
			{
				this._attributes.AddAttributes(writer);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			base.Render(writer);
			writer.RenderEndTag();
			if (!base.DesignMode)
			{
				this.ScriptManager.RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001AD87 File Offset: 0x00018F87
		protected virtual IEnumerable<ScriptReference> GetScriptReferences()
		{
			yield break;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001AD90 File Offset: 0x00018F90
		protected virtual IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			if (this.Page != null && this.ScriptManager.SupportsPartialRendering && this.Visible)
			{
				ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("Sys.UI._UpdateProgress", this.ClientID);
				string value = null;
				if (!string.IsNullOrEmpty(this.AssociatedUpdatePanelID))
				{
					UpdatePanel updatePanel = ControlUtil.FindTargetControl(this.AssociatedUpdatePanelID, this, true) as UpdatePanel;
					if (updatePanel == null)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.UpdateProgress_NoUpdatePanel, new object[]
						{
							this.AssociatedUpdatePanelID
						}));
					}
					value = updatePanel.ClientID;
				}
				scriptControlDescriptor.AddProperty("associatedUpdatePanelId", value);
				scriptControlDescriptor.AddProperty("dynamicLayout", this.DynamicLayout);
				scriptControlDescriptor.AddProperty("displayAfter", this.DisplayAfter);
				yield return scriptControlDescriptor;
			}
			yield break;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001ADA0 File Offset: 0x00018FA0
		string IAttributeAccessor.GetAttribute(string key)
		{
			if (this._attributes == null)
			{
				return null;
			}
			return this._attributes[key];
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001ADB8 File Offset: 0x00018FB8
		void IAttributeAccessor.SetAttribute(string key, string value)
		{
			this.Attributes[key] = value;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001ADC7 File Offset: 0x00018FC7
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return this.GetScriptReferences();
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001ADCF File Offset: 0x00018FCF
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			return this.GetScriptDescriptors();
		}

		// Token: 0x04000221 RID: 545
		private AttributeCollection _attributes;

		// Token: 0x04000222 RID: 546
		private ITemplate _progressTemplate;

		// Token: 0x04000223 RID: 547
		private Control _progressTemplateContainer;

		// Token: 0x04000224 RID: 548
		private int _displayAfter = 500;

		// Token: 0x04000225 RID: 549
		private bool _dynamicLayout = true;

		// Token: 0x04000226 RID: 550
		private string _associatedUpdatePanelID;
	}
}
