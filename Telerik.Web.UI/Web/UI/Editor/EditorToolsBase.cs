using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x02000291 RID: 657
	[RequiredScript(typeof(RadEditorScripts))]
	[ToolboxItem(false)]
	[EmbeddedSkin("Editor")]
	[AdaptiveRendering]
	[EmbeddedSkin("Editor", "Default")]
	[LightweightRendering]
	public abstract class EditorToolsBase : RadWebControl
	{
		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x0600177A RID: 6010
		[ClientControlProperty]
		[DefaultValue("")]
		public abstract string Name { get; }

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x0004EBA0 File Offset: 0x0004CDA0
		[ClientControlProperty]
		public bool AddClickHandler
		{
			get
			{
				return this._addClickHandler;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x0004EBA8 File Offset: 0x0004CDA8
		// (set) Token: 0x0600177D RID: 6013 RVA: 0x0004EBD7 File Offset: 0x0004CDD7
		[ClientControlEvent]
		[ClientPropertyName("valueSelected")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public virtual string OnClientValueSelected
		{
			get
			{
				if (this.ViewState["OnClientValueSelected"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientValueSelected"];
			}
			set
			{
				this.ViewState["OnClientValueSelected"] = value;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x0004EBEA File Offset: 0x0004CDEA
		// (set) Token: 0x0600177F RID: 6015 RVA: 0x0004EC19 File Offset: 0x0004CE19
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("show")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnClientShow
		{
			get
			{
				if (this.ViewState["OnClientShow"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientShow"];
			}
			set
			{
				this.ViewState["OnClientShow"] = value;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x0004EC2C File Offset: 0x0004CE2C
		protected override string CssClassFormatString
		{
			get
			{
				return "reToolWrapper {0}";
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x0004EC33 File Offset: 0x0004CE33
		protected virtual bool AddClientIDToRootTag
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x0004EC36 File Offset: 0x0004CE36
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			BaseClass.RenderVersionStamp(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format(this.CssClassFormatString, base.RuntimeSkin));
			if (this.AddClientIDToRootTag)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x0004EC75 File Offset: 0x0004CE75
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x0004EC7D File Offset: 0x0004CE7D
		protected override void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
			this.RenderContents(writer);
			this.RenderEndTag(writer);
			if (!base.DesignMode)
			{
				this.RegisterScriptDescriptors();
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x0004ECA2 File Offset: 0x0004CEA2
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x0004ECA5 File Offset: 0x0004CEA5
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("skin", base.RuntimeSkin);
			descriptor.AddProperty("rendermode", this.ResolvedRenderMode);
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x0004ECD5 File Offset: 0x0004CED5
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "addClickHandler", this.AddClickHandler, false);
			base.DescribeProperty<string>(descriptor, "name", this.Name, "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x0004ED08 File Offset: 0x0004CF08
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "show", this.OnClientShow);
			RadWebControl.DescribeEvent(descriptor, "valueSelected", this.OnClientValueSelected);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x0400061C RID: 1564
		private bool _addClickHandler = true;
	}
}
