using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml;

namespace System.Web.UI.Design
{
	// Token: 0x02000017 RID: 23
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ControlDesigner : HtmlControlDesigner
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000043B4 File Offset: 0x000025B4
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new ControlDesigner.ControlDesignerActionList(this));
				return designerActionListCollection;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000043E1 File Offset: 0x000025E1
		public virtual bool AllowResize
		{
			get
			{
				return this.IsWebControl;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000043E9 File Offset: 0x000025E9
		public virtual DesignerAutoFormatCollection AutoFormats
		{
			get
			{
				return new DesignerAutoFormatCollection();
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000043F0 File Offset: 0x000025F0
		protected virtual bool DataBindingsEnabled
		{
			get
			{
				ControlDesigner designer;
				for (IControlDesignerView view = this.View; view != null; view = designer.View)
				{
					EditableDesignerRegion editableDesignerRegion = (EditableDesignerRegion)view.ContainingRegion;
					if (editableDesignerRegion == null)
					{
						return false;
					}
					if (editableDesignerRegion.SupportsDataBinding)
					{
						return true;
					}
					designer = editableDesignerRegion.Designer;
					if (designer == null)
					{
						return false;
					}
				}
				return false;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000443A File Offset: 0x0000263A
		protected ControlDesignerState DesignerState
		{
			get
			{
				if (this._designerState == null)
				{
					this._designerState = new ControlDesignerState(base.Component);
				}
				return this._designerState;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000445B File Offset: 0x0000265B
		[Obsolete("The recommended alternative is SetViewFlags(ViewFlags.DesignTimeHtmlRequiresLoadComplete, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		public virtual bool DesignTimeHtmlRequiresLoadComplete
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected internal virtual bool HidePropertiesInTemplateMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000445E File Offset: 0x0000265E
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00004470 File Offset: 0x00002670
		public virtual string ID
		{
			get
			{
				return ((Control)base.Component).ID;
			}
			set
			{
				if (this.RootDesigner != null)
				{
					this.RootDesigner.SetControlID((Control)base.Component, value);
				}
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00004491 File Offset: 0x00002691
		protected bool InTemplateMode
		{
			get
			{
				return this._inTemplateMode;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00004499 File Offset: 0x00002699
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000044A1 File Offset: 0x000026A1
		[Obsolete("The recommended alternative is to use Tag.SetDirty() and Tag.IsDirty. http://go.microsoft.com/fwlink/?linkid=14202")]
		public bool IsDirty
		{
			get
			{
				return this.IsDirtyInternal;
			}
			set
			{
				this.IsDirtyInternal = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000044AA File Offset: 0x000026AA
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000044C6 File Offset: 0x000026C6
		internal bool IsDirtyInternal
		{
			get
			{
				if (this.Tag != null)
				{
					return this.Tag.IsDirty;
				}
				return this.fDirty;
			}
			set
			{
				if (this.Tag != null)
				{
					this.Tag.SetDirty(value);
					return;
				}
				this.fDirty = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000044E4 File Offset: 0x000026E4
		internal bool IsIgnoringComponentChanges
		{
			get
			{
				return this._ignoreComponentChangesCount > 0;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000044EF File Offset: 0x000026EF
		internal bool IsWebControl
		{
			get
			{
				return this.isWebControl;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000044F7 File Offset: 0x000026F7
		internal string LocalizedInnerContent
		{
			get
			{
				return this._localizedInnerContent;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000044FF File Offset: 0x000026FF
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00004507 File Offset: 0x00002707
		public virtual bool ViewControlCreated
		{
			get
			{
				return this._viewControlCreated;
			}
			set
			{
				this._viewControlCreated = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00004510 File Offset: 0x00002710
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00004518 File Offset: 0x00002718
		[Obsolete("The recommended alternative is to inherit from ContainerControlDesigner instead and to use an EditableDesignerRegion. Regions allow for better control of the content in the designer. http://go.microsoft.com/fwlink/?linkid=14202")]
		public bool ReadOnly
		{
			get
			{
				return this.ReadOnlyInternal;
			}
			set
			{
				this.ReadOnlyInternal = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00004521 File Offset: 0x00002721
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00004529 File Offset: 0x00002729
		internal bool ReadOnlyInternal
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				this.readOnly = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00004534 File Offset: 0x00002734
		protected WebFormsRootDesigner RootDesigner
		{
			get
			{
				WebFormsRootDesigner result = null;
				ISite site = base.Component.Site;
				if (site != null)
				{
					IDesignerHost designerHost = (IDesignerHost)site.GetService(typeof(IDesignerHost));
					if (designerHost != null && designerHost.RootComponent != null)
					{
						result = (designerHost.GetDesigner(designerHost.RootComponent) as WebFormsRootDesigner);
					}
				}
				return result;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00004588 File Offset: 0x00002788
		private bool SupportsDataBindings
		{
			get
			{
				BindableAttribute bindableAttribute = (BindableAttribute)TypeDescriptor.GetAttributes(base.Component)[typeof(BindableAttribute)];
				return bindableAttribute != null && bindableAttribute.Bindable;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000045C0 File Offset: 0x000027C0
		protected IControlDesignerTag Tag
		{
			get
			{
				return this._tag;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000045C8 File Offset: 0x000027C8
		public virtual TemplateGroupCollection TemplateGroups
		{
			get
			{
				return new TemplateGroupCollection();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000045D0 File Offset: 0x000027D0
		protected virtual bool UsePreviewControl
		{
			get
			{
				object[] customAttributes = base.GetType().GetCustomAttributes(typeof(SupportsPreviewControlAttribute), false);
				if (customAttributes.Length != 0)
				{
					SupportsPreviewControlAttribute supportsPreviewControlAttribute = (SupportsPreviewControlAttribute)customAttributes[0];
					return supportsPreviewControlAttribute.SupportsPreviewControl;
				}
				return false;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00004609 File Offset: 0x00002809
		internal IControlDesignerView View
		{
			get
			{
				return this._view;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00004611 File Offset: 0x00002811
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00004649 File Offset: 0x00002849
		public Control ViewControl
		{
			get
			{
				if (!this.ViewControlCreated)
				{
					this._viewControl = (this.UsePreviewControl ? this.CreateViewControlInternal() : ((Control)base.Component));
					this.ViewControlCreated = true;
				}
				return this._viewControl;
			}
			set
			{
				this._viewControl = value;
				this.ViewControlCreated = true;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected virtual bool Visible
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000465C File Offset: 0x0000285C
		[Obsolete("Error: This property can no longer be referenced, and is included to support existing compiled applications. The design-time element view architecture is no longer used. http://go.microsoft.com/fwlink/?linkid=14202", true)]
		protected object DesignTimeElementView
		{
			get
			{
				IHtmlControlDesignerBehavior behaviorInternal = this.BehaviorInternal;
				if (behaviorInternal != null)
				{
					return ((IControlDesignerBehavior)behaviorInternal).DesignTimeElementView;
				}
				return null;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004680 File Offset: 0x00002880
		internal static DesignerAutoFormatCollection CreateAutoFormats(string[] schemeNames, Func<string, DesignerAutoFormat> creationDelegate)
		{
			DesignerAutoFormatCollection designerAutoFormatCollection = new DesignerAutoFormatCollection();
			foreach (string arg in schemeNames)
			{
				designerAutoFormatCollection.Add(creationDelegate(arg));
			}
			return designerAutoFormatCollection;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000046B8 File Offset: 0x000028B8
		private static DataTable GetSchemesTable(string schemes)
		{
			DataSet dataSet = new DataSet();
			dataSet.Locale = CultureInfo.InvariantCulture;
			dataSet.ReadXml(new XmlTextReader(new StringReader(schemes))
			{
				DtdProcessing = DtdProcessing.Ignore
			});
			DataTable dataTable = dataSet.Tables[0];
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["SchemeName"]
			};
			return dataTable;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000471C File Offset: 0x0000291C
		internal static DataRow GetSchemeDataRow(string schemeName, string schemes)
		{
			DataTable schemesTable = ControlDesigner.GetSchemesTable(schemes);
			return schemesTable.Rows.Find(schemeName);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000473C File Offset: 0x0000293C
		internal Control CreateClonedControl(IDesignerHost parseTimeDesignerHost, bool applyTheme)
		{
			string text = null;
			if (this.Tag != null)
			{
				text = this.Tag.GetOuterContent();
			}
			if (string.IsNullOrEmpty(text))
			{
				text = ControlPersister.PersistControl((Control)base.Component);
			}
			return ControlParser.ParseControl(parseTimeDesignerHost, text, applyTheme);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004782 File Offset: 0x00002982
		protected string CreatePlaceHolderDesignTimeHtml()
		{
			return this.CreatePlaceHolderDesignTimeHtml(null);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000478C File Offset: 0x0000298C
		protected string CreatePlaceHolderDesignTimeHtml(string instruction)
		{
			string name = base.Component.GetType().Name;
			string name2 = base.Component.Site.Name;
			if (instruction == null)
			{
				instruction = string.Empty;
			}
			return string.Format(CultureInfo.InvariantCulture, ControlDesigner.PlaceHolderDesignTimeHtmlTemplate, new object[]
			{
				name,
				name2,
				instruction
			});
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000047E6 File Offset: 0x000029E6
		protected string CreateErrorDesignTimeHtml(string errorMessage)
		{
			return this.CreateErrorDesignTimeHtml(errorMessage, null);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000047F0 File Offset: 0x000029F0
		protected string CreateErrorDesignTimeHtml(string errorMessage, Exception e)
		{
			return ControlDesigner.CreateErrorDesignTimeHtml(errorMessage, e, base.Component);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004800 File Offset: 0x00002A00
		internal static string CreateErrorDesignTimeHtml(string errorMessage, Exception e, IComponent component)
		{
			string name = component.Site.Name;
			if (errorMessage == null)
			{
				errorMessage = string.Empty;
			}
			else
			{
				errorMessage = HttpUtility.HtmlEncode(errorMessage);
			}
			if (e != null)
			{
				errorMessage = errorMessage + "<br />" + HttpUtility.HtmlEncode(e.Message);
			}
			return string.Format(CultureInfo.InvariantCulture, ControlDesigner.ErrorDesignTimeHtmlTemplate, new object[]
			{
				SR.GetString("ControlDesigner_DesignTimeHtmlError"),
				HttpUtility.HtmlEncode(name),
				errorMessage
			});
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004877 File Offset: 0x00002A77
		internal string CreateInvalidParentDesignTimeHtml(Type controlType, Type requiredParentType)
		{
			return this.CreateErrorDesignTimeHtml(SR.GetString("Control_CanOnlyBePlacedInside", new object[]
			{
				controlType.Name,
				requiredParentType.Name
			}));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000048A4 File Offset: 0x00002AA4
		private Control CreateViewControlInternal()
		{
			Control control = (Control)base.Component;
			Control control2 = this.CreateViewControl();
			control2.RenderingCompatibility = control.RenderingCompatibility;
			((IControlDesignerAccessor)control2).SetOwnerControl(control);
			this.UpdateExpressionValues(control2);
			return control2;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000048DF File Offset: 0x00002ADF
		protected virtual Control CreateViewControl()
		{
			return this.CreateClonedControl((IDesignerHost)this.GetService(typeof(IDesignerHost)), true);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004900 File Offset: 0x00002B00
		private object EnsureParsedExpression(TemplateControl templateControl, ExpressionBinding eb, object parsedData)
		{
			if (parsedData == null && templateControl != null)
			{
				string text;
				Type expressionBuilderType = ExpressionEditor.GetExpressionBuilderType(eb.ExpressionPrefix, base.Component.Site, out text);
				if (expressionBuilderType != null)
				{
					try
					{
						System.Web.Compilation.ExpressionBuilder expressionBuilder = (System.Web.Compilation.ExpressionBuilder)Activator.CreateInstance(expressionBuilderType);
						ExpressionBuilderContext context = new ExpressionBuilderContext(templateControl);
						parsedData = expressionBuilder.ParseExpression(eb.Expression, eb.PropertyType, context);
					}
					catch (Exception ex)
					{
						IComponentDesignerDebugService componentDesignerDebugService = (IComponentDesignerDebugService)this.GetService(typeof(IComponentDesignerDebugService));
						if (componentDesignerDebugService != null)
						{
							componentDesignerDebugService.Fail(SR.GetString("ControlDesigner_CouldNotGetExpressionBuilder", new object[]
							{
								eb.ExpressionPrefix,
								ex.Message
							}));
						}
					}
				}
			}
			return parsedData;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000049C0 File Offset: 0x00002BC0
		public Rectangle GetBounds()
		{
			if (this.View != null)
			{
				return this.View.GetBounds(null);
			}
			return Rectangle.Empty;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000049DC File Offset: 0x00002BDC
		internal static PropertyDescriptor GetComplexProperty(object target, string propName, out object realTarget)
		{
			realTarget = null;
			string[] array = propName.Split(new char[]
			{
				'.'
			});
			PropertyDescriptor propertyDescriptor = null;
			foreach (string text in array)
			{
				if (string.IsNullOrEmpty(text))
				{
					return null;
				}
				propertyDescriptor = TypeDescriptor.GetProperties(target)[text];
				if (propertyDescriptor == null)
				{
					return null;
				}
				realTarget = target;
				target = propertyDescriptor.GetValue(target);
			}
			return propertyDescriptor;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004A40 File Offset: 0x00002C40
		public virtual string GetDesignTimeHtml()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			DesignTimeHtmlTextWriter writer = new DesignTimeHtmlTextWriter(stringWriter);
			string text = null;
			bool flag = false;
			bool flag2 = true;
			Control control = null;
			try
			{
				control = this.ViewControl;
				flag2 = control.Visible;
				if (!flag2)
				{
					control.Visible = true;
					flag = !this.UsePreviewControl;
				}
				control.RenderControl(writer);
				text = stringWriter.ToString();
			}
			catch (Exception e)
			{
				text = this.GetErrorDesignTimeHtml(e);
			}
			finally
			{
				if (flag)
				{
					control.Visible = flag2;
				}
			}
			if (text == null || text.Length == 0)
			{
				text = this.GetEmptyDesignTimeHtml();
			}
			return text;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004AEC File Offset: 0x00002CEC
		public virtual string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			return this.GetDesignTimeHtml();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004AF4 File Offset: 0x00002CF4
		public static DesignTimeResourceProviderFactory GetDesignTimeResourceProviderFactory(IServiceProvider serviceProvider)
		{
			DesignTimeResourceProviderFactory designTimeResourceProviderFactory = null;
			IWebApplication webApplication = (IWebApplication)serviceProvider.GetService(typeof(IWebApplication));
			if (webApplication != null)
			{
				Configuration configuration = webApplication.OpenWebConfiguration(true);
				if (configuration != null)
				{
					GlobalizationSection globalizationSection = configuration.GetSection("system.web/globalization") as GlobalizationSection;
					if (globalizationSection != null)
					{
						string resourceProviderFactoryType = globalizationSection.ResourceProviderFactoryType;
						if (!string.IsNullOrEmpty(resourceProviderFactoryType))
						{
							ITypeResolutionService typeResolutionService = (ITypeResolutionService)serviceProvider.GetService(typeof(ITypeResolutionService));
							if (typeResolutionService != null)
							{
								Type type = typeResolutionService.GetType(resourceProviderFactoryType, true, true);
								if (type != null)
								{
									object[] customAttributes = type.GetCustomAttributes(typeof(DesignTimeResourceProviderFactoryAttribute), true);
									if (customAttributes != null && customAttributes.Length != 0)
									{
										DesignTimeResourceProviderFactoryAttribute designTimeResourceProviderFactoryAttribute = customAttributes[0] as DesignTimeResourceProviderFactoryAttribute;
										string factoryTypeName = designTimeResourceProviderFactoryAttribute.FactoryTypeName;
										if (!string.IsNullOrEmpty(factoryTypeName))
										{
											Type type2 = typeResolutionService.GetType(factoryTypeName, true, true);
											if (type2 != null && typeof(DesignTimeResourceProviderFactory).IsAssignableFrom(type2))
											{
												try
												{
													designTimeResourceProviderFactory = (DesignTimeResourceProviderFactory)Activator.CreateInstance(type2);
												}
												catch (Exception ex)
												{
													if (serviceProvider != null)
													{
														IComponentDesignerDebugService componentDesignerDebugService = (IComponentDesignerDebugService)serviceProvider.GetService(typeof(IComponentDesignerDebugService));
														if (componentDesignerDebugService != null)
														{
															componentDesignerDebugService.Fail(SR.GetString("ControlDesigner_CouldNotGetDesignTimeResourceProviderFactory", new object[]
															{
																factoryTypeName,
																ex.Message
															}));
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
				}
			}
			if (designTimeResourceProviderFactory == null)
			{
				IDesignTimeResourceProviderFactoryService designTimeResourceProviderFactoryService = (IDesignTimeResourceProviderFactoryService)serviceProvider.GetService(typeof(IDesignTimeResourceProviderFactoryService));
				if (designTimeResourceProviderFactoryService != null)
				{
					designTimeResourceProviderFactory = designTimeResourceProviderFactoryService.GetFactory();
				}
			}
			return designTimeResourceProviderFactory;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003930 File Offset: 0x00001B30
		public virtual string GetEditableDesignerRegionContent(EditableDesignerRegion region)
		{
			return string.Empty;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004C90 File Offset: 0x00002E90
		protected virtual string GetEmptyDesignTimeHtml()
		{
			string name = base.Component.GetType().Name;
			string name2 = base.Component.Site.Name;
			if (name2 != null && name2.Length > 0)
			{
				return string.Concat(new string[]
				{
					"[ ",
					name,
					" \"",
					name2,
					"\" ]"
				});
			}
			return "[ " + name + " ]";
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004D07 File Offset: 0x00002F07
		protected virtual string GetErrorDesignTimeHtml(Exception e)
		{
			return this.CreateErrorDesignTimeHtml(SR.GetString("ControlDesigner_UnhandledException"), e);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004D1A File Offset: 0x00002F1A
		[Obsolete("The recommended alternative is GetPersistenceContent(). http://go.microsoft.com/fwlink/?linkid=14202")]
		public virtual string GetPersistInnerHtml()
		{
			return this.GetPersistInnerHtmlInternal();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004D24 File Offset: 0x00002F24
		internal virtual string GetPersistInnerHtmlInternal()
		{
			if (this._localizedInnerContent != null)
			{
				return this._localizedInnerContent;
			}
			if (!this.IsDirtyInternal)
			{
				return null;
			}
			IDesignerHost host = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			this.IsDirtyInternal = false;
			return ControlSerializer.SerializeInnerContents((Control)base.Component, host);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004D78 File Offset: 0x00002F78
		public virtual string GetPersistenceContent()
		{
			return this.GetPersistInnerHtml();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004D80 File Offset: 0x00002F80
		internal void HideAllPropertiesUnlessExcluded(IDictionary properties, string[] propertiesToExclude)
		{
			ICollection values = properties.Values;
			if (values != null)
			{
				object[] array = new object[values.Count];
				values.CopyTo(array, 0);
				for (int i = 0; i < array.Length; i++)
				{
					PropertyDescriptor prop = (PropertyDescriptor)array[i];
					if (prop != null && !Array.Exists<string>(propertiesToExclude, (string s) => prop.Name.Equals(s, StringComparison.OrdinalIgnoreCase)))
					{
						properties[prop.Name] = TypeDescriptor.CreateProperty(prop.ComponentType, prop, new Attribute[]
						{
							BrowsableAttribute.No
						});
					}
				}
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004E28 File Offset: 0x00003028
		public void Localize(IDesignTimeResourceWriter resourceWriter)
		{
			this.OnComponentChanging(base.Component, new ComponentChangingEventArgs(base.Component, null));
			string text;
			string value = ControlLocalizer.LocalizeControl((Control)base.Component, resourceWriter, out text);
			if (!string.IsNullOrEmpty(value))
			{
				this.SetTagAttribute("meta:resourcekey", value, true);
			}
			if (!string.IsNullOrEmpty(text))
			{
				this._localizedInnerContent = text;
			}
			this.OnComponentChanged(base.Component, new ComponentChangedEventArgs(base.Component, null, null, null));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004EA0 File Offset: 0x000030A0
		public static ViewRendering GetViewRendering(Control control)
		{
			ControlDesigner designer = null;
			ISite site = control.Site;
			if (site != null)
			{
				IDesignerHost designerHost = (IDesignerHost)site.GetService(typeof(IDesignerHost));
				designer = (designerHost.GetDesigner(control) as ControlDesigner);
			}
			return ControlDesigner.GetViewRendering(designer);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004EE4 File Offset: 0x000030E4
		public static ViewRendering GetViewRendering(ControlDesigner designer)
		{
			string content = string.Empty;
			DesignerRegionCollection designerRegionCollection = new DesignerRegionCollection();
			bool visible = true;
			if (designer != null)
			{
				bool flag = false;
				if (designer.View != null)
				{
					flag = designer.View.SupportsRegions;
				}
				try
				{
					designer.ViewControlCreated = false;
					if (flag)
					{
						content = designer.GetDesignTimeHtml(designerRegionCollection);
					}
					else
					{
						content = designer.GetDesignTimeHtml();
					}
					visible = designer.Visible;
				}
				catch (Exception e)
				{
					designerRegionCollection.Clear();
					try
					{
						content = designer.GetErrorDesignTimeHtml(e);
					}
					catch (Exception ex)
					{
						content = designer.CreateErrorDesignTimeHtml(ex.Message);
					}
					visible = true;
				}
			}
			return new ViewRendering(content, designerRegionCollection, visible);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004F8C File Offset: 0x0000318C
		public ViewRendering GetViewRendering()
		{
			EditableDesignerRegion editableDesignerRegion = null;
			if (this.View != null)
			{
				editableDesignerRegion = (this.View.ContainingRegion as EditableDesignerRegion);
			}
			ViewRendering result;
			if (editableDesignerRegion != null)
			{
				result = editableDesignerRegion.GetChildViewRendering((Control)base.Component);
			}
			else
			{
				result = ControlDesigner.GetViewRendering(this);
			}
			return result;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004FD5 File Offset: 0x000031D5
		private void IgnoreComponentChanges(bool ignore)
		{
			this._ignoreComponentChangesCount += (ignore ? 1 : -1);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004FEC File Offset: 0x000031EC
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(Control));
			base.Initialize(component);
			if (this.RootDesigner != null)
			{
				this.RootDesigner.GetControlViewAndTag((Control)base.Component, out this._view, out this._tag);
				if (this._view != null)
				{
					this._view.ViewEvent += this.OnViewEvent;
				}
			}
			base.Expressions.Changed += this.OnExpressionsChanged;
			this.isWebControl = (component is WebControl);
			this.UpdateExpressionValues(component);
			this.SetDesignTimeRenderingCompatibility((Control)base.Component);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005097 File Offset: 0x00003297
		public void Invalidate()
		{
			if (this.View != null)
			{
				this.Invalidate(this.View.GetBounds(null));
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000050B3 File Offset: 0x000032B3
		public void Invalidate(Rectangle rectangle)
		{
			if (this.View != null)
			{
				this.View.Invalidate(rectangle);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000050C9 File Offset: 0x000032C9
		public static void InvokeTransactedChange(IComponent component, TransactedChangeCallback callback, object context, string description)
		{
			ControlDesigner.InvokeTransactedChange(component, callback, context, description, null);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000050D5 File Offset: 0x000032D5
		public static void InvokeTransactedChange(IComponent component, TransactedChangeCallback callback, object context, string description, MemberDescriptor member)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			ControlDesigner.InvokeTransactedChange(component.Site, component, callback, context, description, member);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000050F8 File Offset: 0x000032F8
		public static void InvokeTransactedChange(IServiceProvider serviceProvider, IComponent component, TransactedChangeCallback callback, object context, string description, MemberDescriptor member)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			if (serviceProvider == null)
			{
				throw new ArgumentException(SR.GetString("ControlDesigner_TransactedChangeRequiresServiceProvider"), "serviceProvider");
			}
			IDesignerHost designerHost = (IDesignerHost)serviceProvider.GetService(typeof(IDesignerHost));
			using (DesignerTransaction designerTransaction = designerHost.CreateTransaction(description))
			{
				IComponentChangeService componentChangeService = (IComponentChangeService)serviceProvider.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					try
					{
						componentChangeService.OnComponentChanging(component, member);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
				}
				ControlDesigner controlDesigner = designerHost.GetDesigner(component) as ControlDesigner;
				bool flag = false;
				try
				{
					if (controlDesigner != null)
					{
						controlDesigner.IgnoreComponentChanges(true);
					}
					if (callback(context))
					{
						if (controlDesigner != null)
						{
							flag = true;
							controlDesigner.IgnoreComponentChanges(false);
						}
						if (componentChangeService != null)
						{
							componentChangeService.OnComponentChanged(component, member, null, null);
						}
						TypeDescriptor.Refresh(component);
						designerTransaction.Commit();
					}
				}
				finally
				{
					if (controlDesigner != null && !flag)
					{
						controlDesigner.IgnoreComponentChanges(false);
					}
				}
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000521C File Offset: 0x0000341C
		[Obsolete("The recommended alternative is DataBindings.Contains(string). The DataBindings collection allows more control of the databindings associated with the control. http://go.microsoft.com/fwlink/?linkid=14202")]
		public bool IsPropertyBound(string propName)
		{
			return base.DataBindings[propName] != null;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void OnAutoFormatApplied(DesignerAutoFormat appliedAutoFormat)
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005230 File Offset: 0x00003430
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties["ID"];
			if (propertyDescriptor != null)
			{
				properties["ID"] = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor, ControlDesigner.emptyAttrs);
			}
			propertyDescriptor = (PropertyDescriptor)properties["SkinID"];
			if (propertyDescriptor != null)
			{
				properties["SkinID"] = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[]
				{
					new TypeConverterAttribute(typeof(SkinIDTypeConverter))
				});
			}
			if (this.InTemplateMode)
			{
				if (this.HidePropertiesInTemplateMode)
				{
					this.HideAllPropertiesUnlessExcluded(properties, this.DefaultEnabledPropertyInGrid);
				}
				propertyDescriptor = (PropertyDescriptor)properties["ID"];
				if (propertyDescriptor != null)
				{
					properties[propertyDescriptor.Name] = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[]
					{
						ReadOnlyAttribute.Yes
					});
				}
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005310 File Offset: 0x00003510
		[Obsolete("The recommended alternative is to handle the Changed event on the DataBindings collection. The DataBindings collection allows more control of the databindings associated with the control. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected override void OnBindingsCollectionChanged(string propName)
		{
			if (this.Tag == null)
			{
				return;
			}
			DataBindingCollection dataBindings = base.DataBindings;
			if (propName != null)
			{
				DataBinding dataBinding = dataBindings[propName];
				string text = propName.Replace('.', '-');
				if (dataBinding == null)
				{
					this.Tag.RemoveAttribute(text);
					return;
				}
				string value = "<%# " + dataBinding.Expression + " %>";
				this.Tag.SetAttribute(text, value);
				if (text.IndexOf('-') < 0)
				{
					this.ResetPropertyValue(text, false);
					return;
				}
			}
			else
			{
				string[] removedBindings = dataBindings.RemovedBindings;
				foreach (string text2 in removedBindings)
				{
					string name = text2.Replace('.', '-');
					this.Tag.RemoveAttribute(name);
				}
				foreach (object obj in dataBindings)
				{
					DataBinding dataBinding2 = (DataBinding)obj;
					string value2 = "<%# " + dataBinding2.Expression + " %>";
					string text3 = dataBinding2.PropertyName.Replace('.', '-');
					this.Tag.SetAttribute(text3, value2);
					if (text3.IndexOf('-') < 0)
					{
						this.ResetPropertyValue(text3, false);
					}
				}
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void OnClick(DesignerRegionMouseEventArgs e)
		{
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00005464 File Offset: 0x00003664
		public virtual void OnComponentChanged(object sender, ComponentChangedEventArgs ce)
		{
			if (this.IsIgnoringComponentChanges)
			{
				return;
			}
			IComponent component = base.Component;
			if (base.DesignTimeElementInternal == null)
			{
				return;
			}
			MemberDescriptor member = ce.Member;
			if (member != null)
			{
				PropertyDescriptor propertyDescriptor = member as PropertyDescriptor;
				BindingFlags bindingAttr = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public;
				if (propertyDescriptor == null || component.GetType().GetProperty(propertyDescriptor.Name, bindingAttr) == null || (ce.NewValue != null && ce.NewValue == ce.OldValue))
				{
					return;
				}
				if (propertyDescriptor.SerializationVisibility != DesignerSerializationVisibility.Hidden)
				{
					this.IsDirtyInternal = true;
					PersistenceModeAttribute persistenceModeAttribute = (PersistenceModeAttribute)member.Attributes[typeof(PersistenceModeAttribute)];
					PersistenceMode mode = persistenceModeAttribute.Mode;
					if (mode == PersistenceMode.Attribute || mode == PersistenceMode.InnerDefaultProperty || mode == PersistenceMode.EncodedInnerDefaultProperty)
					{
						string name = member.Name;
						if (ce.Component == base.Component)
						{
							if (base.DataBindings.Contains(name))
							{
								base.DataBindings.Remove(name, false);
								this.RemoveTagAttribute(name, true);
							}
							if (base.Expressions.Contains(name))
							{
								ExpressionBinding expressionBinding = base.Expressions[name];
								if (!expressionBinding.Generated)
								{
									base.Expressions.Remove(name, false);
									this.RemoveTagAttribute(name, true);
								}
								this._expressionsChanged = true;
							}
						}
						Control control = (Control)ce.Component;
						IDesignerHost designerHost = null;
						if (control.Site != null)
						{
							designerHost = (IDesignerHost)control.Site.GetService(typeof(IDesignerHost));
						}
						if (designerHost != null)
						{
							ArrayList controlPersistedAttribute = ControlSerializer.GetControlPersistedAttribute(control, propertyDescriptor, designerHost);
							this.PersistAttributes(controlPersistedAttribute);
						}
					}
				}
			}
			else
			{
				this.IsDirtyInternal = true;
				Control control2 = (Control)ce.Component;
				IDesignerHost designerHost2 = null;
				if (control2.Site != null)
				{
					designerHost2 = (IDesignerHost)control2.Site.GetService(typeof(IDesignerHost));
				}
				foreach (object obj in base.Expressions.RemovedBindings)
				{
					string propName = (string)obj;
					object component2;
					PropertyDescriptor complexProperty = ControlDesigner.GetComplexProperty(base.Component, propName, out component2);
					if (complexProperty != null)
					{
						this.IgnoreComponentChanges(true);
						try
						{
							complexProperty.ResetValue(component2);
						}
						finally
						{
							this.IgnoreComponentChanges(false);
						}
					}
				}
				if (designerHost2 != null)
				{
					ArrayList controlPersistedAttributes = ControlSerializer.GetControlPersistedAttributes(control2, designerHost2);
					this.PersistAttributes(controlPersistedAttributes);
				}
				foreach (object obj2 in base.DataBindings)
				{
					DataBinding dataBinding = (DataBinding)obj2;
					if (dataBinding.PropertyName.IndexOf('.') < 0)
					{
						this.ResetPropertyValue(dataBinding.PropertyName, false);
					}
				}
				base.OnBindingsCollectionChangedInternal(null);
				this._expressionsChanged = true;
			}
			if (this._expressionsChanged)
			{
				this.UpdateExpressionValues(base.Component);
			}
			this.UpdateDesignTimeHtml();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void OnComponentChanging(object sender, ComponentChangingEventArgs ce)
		{
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003937 File Offset: 0x00001B37
		[Obsolete("The recommended alternative is OnComponentChanged(). OnComponentChanged is called when any property of the control is changed. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual void OnControlResize()
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000576C File Offset: 0x0000396C
		private void OnExpressionsChanged(object sender, EventArgs e)
		{
			this._expressionsChanged = true;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void OnPaint(PaintEventArgs e)
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00005778 File Offset: 0x00003978
		private void OnViewEvent(object sender, ViewEventArgs e)
		{
			if (e.EventType == ViewEvent.Click)
			{
				this.OnClick((DesignerRegionMouseEventArgs)e.EventArgs);
				return;
			}
			if (e.EventType == ViewEvent.Paint)
			{
				this.OnPaint((PaintEventArgs)e.EventArgs);
				return;
			}
			if (e.EventType == ViewEvent.TemplateModeChanged)
			{
				TemplateModeChangedEventArgs templateModeChangedEventArgs = (TemplateModeChangedEventArgs)e.EventArgs;
				this._inTemplateMode = (templateModeChangedEventArgs.NewTemplateGroup != null);
				TypeDescriptor.Refresh(base.Component);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000057F8 File Offset: 0x000039F8
		private void PersistAttributes(ArrayList attributes)
		{
			foreach (object obj in attributes)
			{
				Triplet triplet = (Triplet)obj;
				string text = Convert.ToString(triplet.Second, CultureInfo.InvariantCulture);
				string text2 = triplet.First.ToString();
				if (text2 == null || text2.Length > 0)
				{
					text = text2 + ":" + text;
				}
				if (triplet.Third == null)
				{
					this.RemoveTagAttribute(text, true);
				}
				else
				{
					string value = Convert.ToString(triplet.Third, CultureInfo.InvariantCulture);
					this.SetTagAttribute(text, value, true);
				}
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000058AC File Offset: 0x00003AAC
		[Obsolete("Use of this method is not recommended because resizing is handled by the OnComponentChanged() method. http://go.microsoft.com/fwlink/?linkid=14202")]
		public void RaiseResizeEvent()
		{
			this.OnControlResize();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000058B4 File Offset: 0x00003AB4
		public void RegisterClone(object original, object clone)
		{
			if (original == null)
			{
				throw new ArgumentNullException("original");
			}
			if (clone == null)
			{
				throw new ArgumentNullException("clone");
			}
			ControlBuilder controlBuilder = ((IControlBuilderAccessor)base.Component).ControlBuilder;
			if (controlBuilder != null)
			{
				ObjectPersistData objectPersistData = controlBuilder.GetObjectPersistData();
				objectPersistData.BuiltObjects[clone] = objectPersistData.BuiltObjects[original];
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005910 File Offset: 0x00003B10
		private void ResetPropertyValue(string property, bool useInstance)
		{
			PropertyDescriptor propertyDescriptor;
			if (useInstance)
			{
				propertyDescriptor = TypeDescriptor.GetProperties(base.Component)[property];
			}
			else
			{
				propertyDescriptor = TypeDescriptor.GetProperties(base.Component.GetType())[property];
			}
			if (propertyDescriptor != null)
			{
				this.IgnoreComponentChanges(true);
				try
				{
					propertyDescriptor.ResetValue(base.Component);
				}
				finally
				{
					this.IgnoreComponentChanges(false);
				}
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005980 File Offset: 0x00003B80
		private void RemoveTagAttribute(string name, bool ignoreCase)
		{
			if (this.Tag != null)
			{
				this.Tag.RemoveAttribute(name);
				return;
			}
			this.BehaviorInternal.RemoveAttribute(name, ignoreCase);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000059A4 File Offset: 0x00003BA4
		private void SetDesignTimeRenderingCompatibility(Control control)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProvider(control).GetTypeDescriptor(control).GetProperties().Find("RenderingCompatibility", false);
			bool flag = propertyDescriptor == null;
			if (flag)
			{
				control.RenderingCompatibility = new Version(3, 5);
				return;
			}
			if (control.Site == null)
			{
				return;
			}
			IWebApplication webApplication = (IWebApplication)control.Site.GetService(typeof(IWebApplication));
			if (webApplication != null)
			{
				Configuration configuration = webApplication.OpenWebConfiguration(true);
				PagesSection pagesSection = (PagesSection)configuration.GetSection("system.web/pages");
				if (pagesSection != null)
				{
					control.RenderingCompatibility = pagesSection.ControlRenderingCompatibilityVersion;
				}
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005A36 File Offset: 0x00003C36
		protected void SetRegionContent(EditableDesignerRegion region, string content)
		{
			if (this.View != null)
			{
				this.View.SetRegionContent(region, content);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005A4D File Offset: 0x00003C4D
		private void SetTagAttribute(string name, object value, bool ignoreCase)
		{
			if (this.Tag != null)
			{
				this.Tag.SetAttribute(name, value.ToString());
				return;
			}
			this.BehaviorInternal.SetAttribute(name, value, ignoreCase);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005A78 File Offset: 0x00003C78
		protected void SetViewFlags(ViewFlags viewFlags, bool setFlag)
		{
			if (this.View != null)
			{
				this.View.SetFlags(viewFlags, setFlag);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005A90 File Offset: 0x00003C90
		public virtual void UpdateDesignTimeHtml()
		{
			if (this.View != null)
			{
				this.View.Update();
				return;
			}
			if (this.ReadOnlyInternal)
			{
				IHtmlControlDesignerBehavior behaviorInternal = this.BehaviorInternal;
				if (behaviorInternal != null)
				{
					((IControlDesignerBehavior)behaviorInternal).DesignTimeHtml = this.GetDesignTimeHtml();
				}
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005AD4 File Offset: 0x00003CD4
		private void UpdateExpressionValues(IComponent target)
		{
			IExpressionsAccessor expressionsAccessor = target as IExpressionsAccessor;
			TemplateControl templateControl = null;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				templateControl = (designerHost.RootComponent as TemplateControl);
			}
			foreach (object obj in expressionsAccessor.Expressions)
			{
				ExpressionBinding expressionBinding = (ExpressionBinding)obj;
				if (!expressionBinding.Generated)
				{
					string propertyName = expressionBinding.PropertyName;
					object component;
					PropertyDescriptor complexProperty = ControlDesigner.GetComplexProperty(target, propertyName, out component);
					if (complexProperty != null)
					{
						this.IgnoreComponentChanges(true);
						try
						{
							ExpressionEditor expressionEditor = ExpressionEditor.GetExpressionEditor(expressionBinding.ExpressionPrefix, target.Site);
							if (expressionEditor != null)
							{
								object parseTimeData = this.EnsureParsedExpression(templateControl, expressionBinding, expressionBinding.ParsedExpressionData);
								object obj2 = expressionEditor.EvaluateExpression(expressionBinding.Expression, parseTimeData, complexProperty.PropertyType, target.Site);
								if (obj2 != null)
								{
									if (obj2 is string)
									{
										TypeConverter converter = complexProperty.Converter;
										if (converter != null && converter.CanConvertFrom(typeof(string)))
										{
											obj2 = converter.ConvertFromInvariantString((string)obj2);
										}
									}
									complexProperty.SetValue(component, obj2);
								}
								else
								{
									complexProperty.SetValue(component, SR.GetString("ExpressionEditor_ExpressionBound", new object[]
									{
										expressionBinding.Expression
									}));
								}
							}
							else
							{
								complexProperty.SetValue(component, SR.GetString("ExpressionEditor_ExpressionBound", new object[]
								{
									expressionBinding.Expression
								}));
							}
						}
						catch
						{
						}
						finally
						{
							this.IgnoreComponentChanges(false);
						}
					}
				}
			}
			this._expressionsChanged = false;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005CBC File Offset: 0x00003EBC
		internal bool UseRegions(DesignerRegionCollection regions, ITemplate componentTemplate)
		{
			return this.UseRegionsCore(regions) && componentTemplate != null;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005CDC File Offset: 0x00003EDC
		internal bool UseRegions(DesignerRegionCollection regions, ITemplate componentTemplate, ITemplate viewControlTemplate)
		{
			bool flag = componentTemplate == null && viewControlTemplate != null;
			return this.UseRegionsCore(regions) && !flag;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005D08 File Offset: 0x00003F08
		private bool UseRegionsCore(DesignerRegionCollection regions)
		{
			return regions != null && this.View != null && this.View.SupportsRegions;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005D30 File Offset: 0x00003F30
		internal static void VerifyInitializeArgument(IComponent component, Type expectedType)
		{
			if (!expectedType.IsInstanceOfType(component))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("ControlDesigner_ArgumentMustBeOfType"), new object[]
				{
					expectedType.FullName
				}), "component");
			}
		}

		// Token: 0x040000C5 RID: 197
		internal static readonly string ErrorDesignTimeHtmlTemplate = "<table cellpadding=\"4\" cellspacing=\"0\" style=\"font: messagebox; color: buttontext; background-color: buttonface; border: solid 1px; border-top-color: buttonhighlight; border-left-color: buttonhighlight; border-bottom-color: buttonshadow; border-right-color: buttonshadow\">\r\n                <tr><td nowrap><span style=\"font-weight: bold; color: red\">{0}</span> - {1}</td></tr>\r\n                <tr><td>{2}</td></tr>\r\n              </table>";

		// Token: 0x040000C6 RID: 198
		private static readonly string PlaceHolderDesignTimeHtmlTemplate = "<table cellpadding=4 cellspacing=0 style=\"font:messagebox;color:buttontext;background-color:buttonface;border: solid 1px;border-top-color:buttonhighlight;border-left-color:buttonhighlight;border-bottom-color:buttonshadow;border-right-color:buttonshadow\">\r\n              <tr><td nowrap><span style=\"font-weight:bold\">{0}</span> - {1}</td></tr>\r\n              <tr><td>{2}</td></tr>\r\n            </table>";

		// Token: 0x040000C7 RID: 199
		private readonly string[] DefaultEnabledPropertyInGrid = new string[]
		{
			"ID"
		};

		// Token: 0x040000C8 RID: 200
		private bool isWebControl;

		// Token: 0x040000C9 RID: 201
		private bool readOnly = true;

		// Token: 0x040000CA RID: 202
		private bool fDirty;

		// Token: 0x040000CB RID: 203
		private int _ignoreComponentChangesCount;

		// Token: 0x040000CC RID: 204
		private bool _inTemplateMode;

		// Token: 0x040000CD RID: 205
		private Control _viewControl;

		// Token: 0x040000CE RID: 206
		private bool _viewControlCreated;

		// Token: 0x040000CF RID: 207
		private IControlDesignerTag _tag;

		// Token: 0x040000D0 RID: 208
		private IControlDesignerView _view;

		// Token: 0x040000D1 RID: 209
		private ControlDesignerState _designerState;

		// Token: 0x040000D2 RID: 210
		private bool _expressionsChanged;

		// Token: 0x040000D3 RID: 211
		private string _localizedInnerContent;

		// Token: 0x040000D4 RID: 212
		private static readonly Attribute[] emptyAttrs = new Attribute[0];

		// Token: 0x040000D5 RID: 213
		private static readonly Attribute[] nonBrowsableAttrs = new Attribute[]
		{
			BrowsableAttribute.No
		};

		// Token: 0x020003A2 RID: 930
		internal class ControlDesignerActionList : DesignerActionList
		{
			// Token: 0x060025AF RID: 9647 RVA: 0x000EBC3D File Offset: 0x000E9E3D
			public ControlDesignerActionList(ControlDesigner parent) : base(parent.Component)
			{
				this._parent = parent;
			}

			// Token: 0x170007F0 RID: 2032
			// (get) Token: 0x060025B0 RID: 9648 RVA: 0x00003B0F File Offset: 0x00001D0F
			// (set) Token: 0x060025B1 RID: 9649 RVA: 0x00003937 File Offset: 0x00001B37
			public override bool AutoShow
			{
				get
				{
					return true;
				}
				set
				{
				}
			}

			// Token: 0x060025B2 RID: 9650 RVA: 0x000EBC54 File Offset: 0x000E9E54
			private bool DataBindingsCallback(object context)
			{
				Control control = (Control)this._parent.Component;
				ISite site = control.Site;
				DataBindingsDialog form = new DataBindingsDialog(site, control);
				DialogResult dialogResult = UIServiceHelper.ShowDialog(site, form);
				return dialogResult == DialogResult.OK;
			}

			// Token: 0x060025B3 RID: 9651 RVA: 0x000EBC90 File Offset: 0x000E9E90
			public void EditDataBindings()
			{
				Control control = (Control)this._parent.Component;
				if (string.IsNullOrEmpty(control.ID))
				{
					UIServiceHelper.ShowMessage(control.Site, SR.GetString("ControlDesigner_EditDataBindingsRequiresID"));
					return;
				}
				ControlDesigner.InvokeTransactedChange(control, new TransactedChangeCallback(this.DataBindingsCallback), null, SR.GetString("Designer_DataBindingsVerb"));
				this._parent.UpdateDesignTimeHtml();
			}

			// Token: 0x060025B4 RID: 9652 RVA: 0x000EBCFC File Offset: 0x000E9EFC
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
				if (this._parent.SupportsDataBindings && this._parent.DataBindingsEnabled)
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "EditDataBindings", SR.GetString("Designer_DataBindingsVerb"), string.Empty, SR.GetString("Designer_DataBindingsVerbDesc"), true));
				}
				return designerActionItemCollection;
			}

			// Token: 0x04001B82 RID: 7042
			private ControlDesigner _parent;
		}
	}
}
