using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design
{
	// Token: 0x0200006D RID: 109
	public class TemplateDefinition : DesignerObject
	{
		// Token: 0x0600034C RID: 844 RVA: 0x00011048 File Offset: 0x0000F248
		public TemplateDefinition(ControlDesigner designer, string name, object templatedObject, string templatePropertyName) : this(designer, name, templatedObject, templatePropertyName, false)
		{
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00011056 File Offset: 0x0000F256
		public TemplateDefinition(ControlDesigner designer, string name, object templatedObject, string templatePropertyName, Style style) : this(designer, name, templatedObject, templatePropertyName, style, false)
		{
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00011066 File Offset: 0x0000F266
		public TemplateDefinition(ControlDesigner designer, string name, object templatedObject, string templatePropertyName, bool serverControlsOnly) : this(designer, name, templatedObject, templatePropertyName, null, serverControlsOnly)
		{
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00011078 File Offset: 0x0000F278
		public TemplateDefinition(ControlDesigner designer, string name, object templatedObject, string templatePropertyName, Style style, bool serverControlsOnly) : base(designer, name)
		{
			if (templatePropertyName == null || templatePropertyName.Length == 0)
			{
				throw new ArgumentNullException("templatePropertyName");
			}
			if (templatedObject == null)
			{
				throw new ArgumentNullException("templatedObject");
			}
			this._serverControlsOnly = serverControlsOnly;
			this._style = style;
			this._templatedObject = templatedObject;
			this._templatePropertyName = templatePropertyName;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00003B0F File Offset: 0x00001D0F
		public virtual bool AllowEditing
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000351 RID: 849 RVA: 0x000110D4 File Offset: 0x0000F2D4
		// (set) Token: 0x06000352 RID: 850 RVA: 0x00011118 File Offset: 0x0000F318
		public virtual string Content
		{
			get
			{
				ITemplate template = (ITemplate)this.TemplateProperty.GetValue(this.TemplatedObject);
				IDesignerHost host = (IDesignerHost)base.GetService(typeof(IDesignerHost));
				return ControlPersister.PersistTemplate(template, host);
			}
			set
			{
				IDesignerHost designerHost = (IDesignerHost)base.GetService(typeof(IDesignerHost));
				ITemplate value2 = ControlParser.ParseTemplate(designerHost, value);
				this.TemplateProperty.SetValue(this.TemplatedObject, value2);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00011155 File Offset: 0x0000F355
		public bool ServerControlsOnly
		{
			get
			{
				return this._serverControlsOnly;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0001115D File Offset: 0x0000F35D
		// (set) Token: 0x06000355 RID: 853 RVA: 0x00011165 File Offset: 0x0000F365
		public bool SupportsDataBinding
		{
			get
			{
				return this._supportsDataBinding;
			}
			set
			{
				this._supportsDataBinding = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0001116E File Offset: 0x0000F36E
		public Style Style
		{
			get
			{
				return this._style;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00011176 File Offset: 0x0000F376
		public object TemplatedObject
		{
			get
			{
				return this._templatedObject;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00011180 File Offset: 0x0000F380
		private PropertyDescriptor TemplateProperty
		{
			get
			{
				if (this._templateProperty == null)
				{
					this._templateProperty = TypeDescriptor.GetProperties(this.TemplatedObject)[this.TemplatePropertyName];
					if (this._templateProperty == null || !typeof(ITemplate).IsAssignableFrom(this._templateProperty.PropertyType))
					{
						throw new InvalidOperationException(SR.GetString("TemplateDefinition_InvalidTemplateProperty", new object[]
						{
							this.TemplatePropertyName
						}));
					}
				}
				return this._templateProperty;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000359 RID: 857 RVA: 0x000111FA File Offset: 0x0000F3FA
		public string TemplatePropertyName
		{
			get
			{
				return this._templatePropertyName;
			}
		}

		// Token: 0x04000170 RID: 368
		private Style _style;

		// Token: 0x04000171 RID: 369
		private string _templatePropertyName;

		// Token: 0x04000172 RID: 370
		private object _templatedObject;

		// Token: 0x04000173 RID: 371
		private PropertyDescriptor _templateProperty;

		// Token: 0x04000174 RID: 372
		private bool _serverControlsOnly;

		// Token: 0x04000175 RID: 373
		private bool _supportsDataBinding;
	}
}
