using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000538 RID: 1336
	[Bindable(false)]
	[Designer("System.Web.UI.Design.WebControls.WebParts.EditorPartDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class EditorPart : Part
	{
		// Token: 0x1700140E RID: 5134
		// (get) Token: 0x06004437 RID: 17463 RVA: 0x000E1E84 File Offset: 0x000E0084
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool Display
		{
			get
			{
				return base.DesignMode || (this.WebPartToEdit != null && !(this.WebPartToEdit is ProxyWebPart) && (this.WebPartToEdit.AllowEdit || !this.WebPartToEdit.IsShared || this.WebPartManager == null || this.WebPartManager.Personalization.Scope != PersonalizationScope.User));
			}
		}

		// Token: 0x1700140F RID: 5135
		// (get) Token: 0x06004438 RID: 17464 RVA: 0x000E1EEC File Offset: 0x000E00EC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DisplayTitle
		{
			get
			{
				string text = this.Title;
				if (string.IsNullOrEmpty(text))
				{
					text = SR.GetString("Part_Untitled");
				}
				return text;
			}
		}

		// Token: 0x17001410 RID: 5136
		// (get) Token: 0x06004439 RID: 17465 RVA: 0x000E1F14 File Offset: 0x000E0114
		protected WebPartManager WebPartManager
		{
			get
			{
				return this._webPartManager;
			}
		}

		// Token: 0x17001411 RID: 5137
		// (get) Token: 0x0600443A RID: 17466 RVA: 0x000E1F1C File Offset: 0x000E011C
		protected WebPart WebPartToEdit
		{
			get
			{
				return this._webPartToEdit;
			}
		}

		// Token: 0x17001412 RID: 5138
		// (get) Token: 0x0600443B RID: 17467 RVA: 0x000E1F24 File Offset: 0x000E0124
		protected EditorZoneBase Zone
		{
			get
			{
				return this._zone;
			}
		}

		// Token: 0x0600443C RID: 17468
		public abstract bool ApplyChanges();

		// Token: 0x0600443D RID: 17469 RVA: 0x000E1F2C File Offset: 0x000E012C
		internal string CreateErrorMessage(string exceptionMessage)
		{
			if (this.Context != null && this.Context.IsCustomErrorEnabled)
			{
				return SR.GetString("EditorPart_ErrorSettingProperty");
			}
			return SR.GetString("EditorPart_ErrorSettingPropertyWithExceptionMessage", new object[]
			{
				exceptionMessage
			});
		}

		// Token: 0x0600443E RID: 17470 RVA: 0x000E1F64 File Offset: 0x000E0164
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override IDictionary GetDesignModeState()
		{
			IDictionary dictionary = new HybridDictionary(1);
			dictionary["Zone"] = this.Zone;
			return dictionary;
		}

		// Token: 0x0600443F RID: 17471 RVA: 0x000E1F8A File Offset: 0x000E018A
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Zone == null)
			{
				throw new InvalidOperationException(SR.GetString("EditorPart_MustBeInZone", new object[]
				{
					this.ID
				}));
			}
			if (!this.Display)
			{
				this.Visible = false;
			}
		}

		// Token: 0x06004440 RID: 17472 RVA: 0x000E1FC9 File Offset: 0x000E01C9
		private void RenderDisplayName(HtmlTextWriter writer, string displayName, string associatedClientID)
		{
			if (this.Zone != null)
			{
				this.Zone.LabelStyle.AddAttributesToRender(writer, this);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.For, associatedClientID);
			writer.RenderBeginTag(HtmlTextWriterTag.Label);
			writer.WriteEncodedText(displayName);
			writer.RenderEndTag();
		}

		// Token: 0x06004441 RID: 17473 RVA: 0x000E2004 File Offset: 0x000E0204
		internal void RenderPropertyEditors(HtmlTextWriter writer, string[] propertyDisplayNames, string[] propertyDescriptions, WebControl[] propertyEditors, string[] errorMessages)
		{
			if (propertyDisplayNames.Length == 0)
			{
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "4");
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			for (int i = 0; i < propertyDisplayNames.Length; i++)
			{
				WebControl webControl = propertyEditors[i];
				if (this.Zone != null && !this.Zone.EditUIStyle.IsEmpty)
				{
					webControl.ApplyStyle(this.Zone.EditUIStyle);
				}
				string value = (propertyDescriptions != null) ? propertyDescriptions[i] : null;
				if (!string.IsNullOrEmpty(value))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Title, value);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				if (webControl is CheckBox)
				{
					webControl.RenderControl(writer);
					writer.Write("&nbsp;");
					this.RenderDisplayName(writer, propertyDisplayNames[i], webControl.ClientID);
				}
				else
				{
					CompositeControl compositeControl = webControl as CompositeControl;
					string clientID;
					if (compositeControl != null)
					{
						clientID = compositeControl.Controls[0].ClientID;
					}
					else
					{
						clientID = webControl.ClientID;
					}
					this.RenderDisplayName(writer, propertyDisplayNames[i] + ":", clientID);
					writer.WriteBreak();
					writer.WriteLine();
					webControl.RenderControl(writer);
				}
				writer.WriteBreak();
				writer.WriteLine();
				string text = errorMessages[i];
				if (!string.IsNullOrEmpty(text))
				{
					if (this.Zone != null && !this.Zone.ErrorStyle.IsEmpty)
					{
						this.Zone.ErrorStyle.AddAttributesToRender(writer, this);
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Span);
					writer.WriteEncodedText(text);
					writer.RenderEndTag();
					writer.WriteBreak();
					writer.WriteLine();
				}
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
		}

		// Token: 0x06004442 RID: 17474 RVA: 0x000E219C File Offset: 0x000E039C
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override void SetDesignModeState(IDictionary data)
		{
			if (data != null)
			{
				object obj = data["Zone"];
				if (obj != null)
				{
					this.SetZone((EditorZoneBase)obj);
				}
			}
		}

		// Token: 0x06004443 RID: 17475 RVA: 0x000E21C7 File Offset: 0x000E03C7
		internal void SetWebPartToEdit(WebPart webPartToEdit)
		{
			this._webPartToEdit = webPartToEdit;
		}

		// Token: 0x06004444 RID: 17476 RVA: 0x000E21D0 File Offset: 0x000E03D0
		internal void SetWebPartManager(WebPartManager webPartManager)
		{
			this._webPartManager = webPartManager;
		}

		// Token: 0x06004445 RID: 17477 RVA: 0x000E21D9 File Offset: 0x000E03D9
		internal void SetZone(EditorZoneBase zone)
		{
			this._zone = zone;
		}

		// Token: 0x06004446 RID: 17478
		public abstract void SyncChanges();

		// Token: 0x04002621 RID: 9761
		private WebPart _webPartToEdit;

		// Token: 0x04002622 RID: 9762
		private WebPartManager _webPartManager;

		// Token: 0x04002623 RID: 9763
		private EditorZoneBase _zone;
	}
}
