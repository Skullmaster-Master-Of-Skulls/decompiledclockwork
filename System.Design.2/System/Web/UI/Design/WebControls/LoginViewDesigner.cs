using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000E2 RID: 226
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class LoginViewDesigner : ControlDesigner
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x0002986C File Offset: 0x00027A6C
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new LoginViewDesigner.LoginViewDesignerActionList(this));
				return designerActionListCollection;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0002989C File Offset: 0x00027A9C
		private object CurrentObject
		{
			get
			{
				if (this.CurrentView == 0)
				{
					return base.Component;
				}
				if (this.CurrentView == 1)
				{
					return base.Component;
				}
				return this._loginView.RoleGroups[this.CurrentView - 2];
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x000298E4 File Offset: 0x00027AE4
		private ITemplate CurrentTemplate
		{
			get
			{
				if (this.CurrentView == 0)
				{
					return this._loginView.AnonymousTemplate;
				}
				if (this.CurrentView == 1)
				{
					return this._loginView.LoggedInTemplate;
				}
				RoleGroup roleGroup = this._loginView.RoleGroups[this.CurrentView - 2];
				return roleGroup.ContentTemplate;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0002993C File Offset: 0x00027B3C
		private PropertyDescriptor CurrentTemplateDescriptor
		{
			get
			{
				if (this.CurrentView == 0)
				{
					return TypeDescriptor.GetProperties(base.Component)["AnonymousTemplate"];
				}
				if (this.CurrentView == 1)
				{
					return TypeDescriptor.GetProperties(base.Component)["LoggedInTemplate"];
				}
				RoleGroup component = this._loginView.RoleGroups[this.CurrentView - 2];
				return TypeDescriptor.GetProperties(component)["ContentTemplate"];
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x000299B0 File Offset: 0x00027BB0
		// (set) Token: 0x0600079A RID: 1946 RVA: 0x000299F5 File Offset: 0x00027BF5
		private int CurrentView
		{
			get
			{
				object obj = base.DesignerState["CurrentView"];
				int num = (obj == null) ? 0 : ((int)obj);
				if (num <= 2 + this._loginView.RoleGroups.Count - 1)
				{
					return num;
				}
				return 0;
			}
			set
			{
				base.DesignerState["CurrentView"] = value;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x00029A10 File Offset: 0x00027C10
		private ITemplate CurrentViewControlTemplate
		{
			get
			{
				if (this.CurrentView == 0)
				{
					return ((LoginView)base.ViewControl).AnonymousTemplate;
				}
				if (this.CurrentView == 1)
				{
					return ((LoginView)base.ViewControl).LoggedInTemplate;
				}
				RoleGroup roleGroup = ((LoginView)base.ViewControl).RoleGroups[this.CurrentView - 2];
				return roleGroup.ContentTemplate;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00029A74 File Offset: 0x00027C74
		private TemplateDefinition TemplateDefinition
		{
			get
			{
				int currentView = this.CurrentView;
				if (currentView == 0)
				{
					return new TemplateDefinition(this, "AnonymousTemplate", this._loginView, "AnonymousTemplate");
				}
				if (this.CurrentView == 1)
				{
					return new TemplateDefinition(this, "LoggedInTemplate", this._loginView, "LoggedInTemplate");
				}
				return new TemplateDefinition(this, "ContentTemplate", this._loginView.RoleGroups[currentView - 2], "ContentTemplate");
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x00029AE8 File Offset: 0x00027CE8
		public override TemplateGroupCollection TemplateGroups
		{
			get
			{
				TemplateGroupCollection templateGroups = base.TemplateGroups;
				if (this._templateGroups == null)
				{
					this._templateGroups = new TemplateGroupCollection();
					TemplateGroup templateGroup = new TemplateGroup("AnonymousTemplate");
					templateGroup.AddTemplateDefinition(new TemplateDefinition(this, "AnonymousTemplate", this._loginView, "AnonymousTemplate"));
					this._templateGroups.Add(templateGroup);
					templateGroup = new TemplateGroup("LoggedInTemplate");
					templateGroup.AddTemplateDefinition(new TemplateDefinition(this, "LoggedInTemplate", this._loginView, "LoggedInTemplate"));
					this._templateGroups.Add(templateGroup);
					RoleGroupCollection roleGroups = this._loginView.RoleGroups;
					for (int i = 0; i < roleGroups.Count; i++)
					{
						string text = LoginViewDesigner.CreateRoleGroupCaption(i, roleGroups);
						templateGroup = new TemplateGroup(text);
						templateGroup.AddTemplateDefinition(new TemplateDefinition(this, text, this._loginView.RoleGroups[i], "ContentTemplate"));
						this._templateGroups.Add(templateGroup);
					}
				}
				templateGroups.AddRange(this._templateGroups);
				return templateGroups;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00029BE8 File Offset: 0x00027DE8
		private EditableDesignerRegion BuildRegion()
		{
			return new LoginViewDesigner.LoginViewDesignerRegion(this, this.CurrentObject, this.CurrentTemplate, this.CurrentTemplateDescriptor, this.TemplateDefinition)
			{
				Description = SR.GetString("ContainerControlDesigner_RegionWatermark")
			};
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00029C28 File Offset: 0x00027E28
		private static string CreateRoleGroupCaption(int roleGroupIndex, RoleGroupCollection roleGroups)
		{
			string text = roleGroups[roleGroupIndex].ToString();
			string text2 = "RoleGroup[" + roleGroupIndex.ToString(CultureInfo.InvariantCulture) + "]";
			if (text != null && text.Length > 0)
			{
				text2 = text2 + " - " + text;
			}
			return text2;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00029C78 File Offset: 0x00027E78
		private void EditRoleGroups()
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["RoleGroups"];
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.EditRoleGroupsChangeCallback), propertyDescriptor, SR.GetString("LoginView_EditRoleGroupsTransactionDescription"), propertyDescriptor);
			int num = this._loginView.RoleGroups.Count + 2;
			if (this.CurrentView >= num)
			{
				this.CurrentView = num - 1;
			}
			if (this.CurrentView < 0)
			{
				this.CurrentView = 0;
			}
			this._templateGroups = null;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00029CFC File Offset: 0x00027EFC
		private bool EditRoleGroupsChangeCallback(object context)
		{
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)context;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			UITypeEditor uitypeEditor = (UITypeEditor)propertyDescriptor.GetEditor(typeof(UITypeEditor));
			object obj = uitypeEditor.EditValue(new TypeDescriptorContext(designerHost, propertyDescriptor, base.Component), new WindowsFormsEditorServiceHelper(this), propertyDescriptor.GetValue(base.Component));
			return obj != null;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00029D68 File Offset: 0x00027F68
		public override string GetDesignTimeHtml()
		{
			string result = string.Empty;
			if (this.CurrentViewControlTemplate != null)
			{
				LoginView loginView = (LoginView)base.ViewControl;
				IDictionary dictionary = new HybridDictionary(1);
				dictionary["TemplateIndex"] = this.CurrentView;
				((IControlDesignerAccessor)loginView).SetDesignModeState(dictionary);
				loginView.DataBind();
				result = base.GetDesignTimeHtml();
			}
			return result;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00029DC4 File Offset: 0x00027FC4
		public override string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			string text = string.Empty;
			bool flag = base.UseRegions(regions, this.CurrentTemplate, this.CurrentViewControlTemplate);
			if (flag)
			{
				regions.Add(this.BuildRegion());
			}
			else
			{
				text = this.GetDesignTimeHtml();
			}
			StringBuilder stringBuilder = new StringBuilder(1024);
			stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "<table cellspacing=0 cellpadding=0 border=0 style=\"display:inline-block\">\r\n                <tr>\r\n                    <td nowrap align=center valign=middle style=\"color:{0}; background-color:{1}; \">{2}</td>\r\n                </tr>\r\n                <tr>\r\n                    <td style=\"vertical-align:top;\" {3}='0'>{4}</td>\r\n                </tr>\r\n          </table>", new object[]
			{
				ColorTranslator.ToHtml(SystemColors.ControlText),
				ColorTranslator.ToHtml(SystemColors.Control),
				this._loginView.ID,
				DesignerRegion.DesignerRegionAttributeName,
				text
			}));
			return stringBuilder.ToString();
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00029E68 File Offset: 0x00028068
		public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
		{
			if (region is LoginViewDesigner.LoginViewDesignerRegion)
			{
				ITemplate template = ((LoginViewDesigner.LoginViewDesignerRegion)region).Template;
				if (template != null)
				{
					IDesignerHost host = (IDesignerHost)base.Component.Site.GetService(typeof(IDesignerHost));
					return ControlPersister.PersistTemplate(template, host);
				}
			}
			return base.GetEditableDesignerRegionContent(region);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00029EBC File Offset: 0x000280BC
		protected override string GetEmptyDesignTimeHtml()
		{
			string str = string.Empty;
			int currentView = this.CurrentView;
			if (currentView != 0)
			{
				if (currentView != 1)
				{
					int roleGroupIndex = this.CurrentView - 2;
					string text = LoginViewDesigner.CreateRoleGroupCaption(roleGroupIndex, this._loginView.RoleGroups);
					str = SR.GetString("LoginView_RoleGroupTemplateEmpty", new object[]
					{
						text
					});
				}
				else
				{
					str = SR.GetString("LoginView_LoggedInTemplateEmpty");
				}
			}
			else
			{
				str = SR.GetString("LoginView_AnonymousTemplateEmpty");
			}
			return base.CreatePlaceHolderDesignTimeHtml(str + "<br>" + SR.GetString("LoginView_NoTemplateInst"));
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00029F44 File Offset: 0x00028144
		protected override string GetErrorDesignTimeHtml(Exception e)
		{
			return base.CreatePlaceHolderDesignTimeHtml(SR.GetString("LoginView_ErrorRendering") + "<br />" + e.Message);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00029F66 File Offset: 0x00028166
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(LoginView));
			this._loginView = (LoginView)component;
			base.Initialize(component);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00029F8C File Offset: 0x0002818C
		private void LaunchWebAdmin()
		{
			if (base.Component.Site != null)
			{
				IDesignerHost designerHost = (IDesignerHost)base.Component.Site.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					IWebAdministrationService webAdministrationService = (IWebAdministrationService)designerHost.GetService(typeof(IWebAdministrationService));
					if (webAdministrationService != null)
					{
						webAdministrationService.Start(null);
					}
				}
			}
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00029FEC File Offset: 0x000281EC
		public override void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			if (e.Member == null || e.Member.Name.Equals("RoleGroups"))
			{
				int num = this._loginView.RoleGroups.Count + 2;
				if (this.CurrentView >= num)
				{
					this.CurrentView = num - 1;
				}
				this._templateGroups = null;
			}
			base.OnComponentChanged(sender, e);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0002A04C File Offset: 0x0002824C
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			if (base.InTemplateMode)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties["RoleGroups"];
				properties["RoleGroups"] = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[]
				{
					BrowsableAttribute.No
				});
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0002A0A0 File Offset: 0x000282A0
		public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
		{
			LoginViewDesigner.LoginViewDesignerRegion loginViewDesignerRegion = region as LoginViewDesigner.LoginViewDesignerRegion;
			if (loginViewDesignerRegion == null)
			{
				return;
			}
			IDesignerHost designerHost = (IDesignerHost)base.Component.Site.GetService(typeof(IDesignerHost));
			ITemplate template = ControlParser.ParseTemplate(designerHost, content);
			using (DesignerTransaction designerTransaction = designerHost.CreateTransaction("SetEditableDesignerRegionContent"))
			{
				loginViewDesignerRegion.PropertyDescriptor.SetValue(loginViewDesignerRegion.Object, template);
				designerTransaction.Commit();
			}
			loginViewDesignerRegion.Template = template;
		}

		// Token: 0x0400048A RID: 1162
		private const string _designtimeHTML = "<table cellspacing=0 cellpadding=0 border=0 style=\"display:inline-block\">\r\n                <tr>\r\n                    <td nowrap align=center valign=middle style=\"color:{0}; background-color:{1}; \">{2}</td>\r\n                </tr>\r\n                <tr>\r\n                    <td style=\"vertical-align:top;\" {3}='0'>{4}</td>\r\n                </tr>\r\n          </table>";

		// Token: 0x0400048B RID: 1163
		private LoginView _loginView;

		// Token: 0x0400048C RID: 1164
		private TemplateGroupCollection _templateGroups;

		// Token: 0x0400048D RID: 1165
		private const int _anonymousTemplateIndex = 0;

		// Token: 0x0400048E RID: 1166
		private const int _loggedInTemplateIndex = 1;

		// Token: 0x0400048F RID: 1167
		private const int _roleGroupStartingIndex = 2;

		// Token: 0x04000490 RID: 1168
		private const string _anonymousTemplateName = "AnonymousTemplate";

		// Token: 0x04000491 RID: 1169
		private const string _loggedInTemplateName = "LoggedInTemplate";

		// Token: 0x04000492 RID: 1170
		private const string _contentTemplateName = "ContentTemplate";

		// Token: 0x04000493 RID: 1171
		private const string _roleGroupsPropertyName = "RoleGroups";

		// Token: 0x04000494 RID: 1172
		private static readonly string[] _templateNames = new string[]
		{
			"AnonymousTemplate",
			"LoggedInTemplate"
		};

		// Token: 0x0200040A RID: 1034
		private class LoginViewDesignerRegion : TemplatedEditableDesignerRegion
		{
			// Token: 0x17000854 RID: 2132
			// (get) Token: 0x060027DA RID: 10202 RVA: 0x000F4589 File Offset: 0x000F2789
			// (set) Token: 0x060027DB RID: 10203 RVA: 0x000F4591 File Offset: 0x000F2791
			public ITemplate Template
			{
				get
				{
					return this._template;
				}
				set
				{
					this._template = value;
				}
			}

			// Token: 0x17000855 RID: 2133
			// (get) Token: 0x060027DC RID: 10204 RVA: 0x000F459A File Offset: 0x000F279A
			public object Object
			{
				get
				{
					return this._object;
				}
			}

			// Token: 0x17000856 RID: 2134
			// (get) Token: 0x060027DD RID: 10205 RVA: 0x000F45A2 File Offset: 0x000F27A2
			public PropertyDescriptor PropertyDescriptor
			{
				get
				{
					return this._prop;
				}
			}

			// Token: 0x060027DE RID: 10206 RVA: 0x000F45AA File Offset: 0x000F27AA
			public LoginViewDesignerRegion(ControlDesigner owner, object obj, ITemplate template, PropertyDescriptor descriptor, TemplateDefinition definition) : base(definition)
			{
				this._template = template;
				this._object = obj;
				this._prop = descriptor;
				base.EnsureSize = true;
			}

			// Token: 0x04001C75 RID: 7285
			private ITemplate _template;

			// Token: 0x04001C76 RID: 7286
			private object _object;

			// Token: 0x04001C77 RID: 7287
			private PropertyDescriptor _prop;
		}

		// Token: 0x0200040B RID: 1035
		private class LoginViewDesignerActionList : DesignerActionList
		{
			// Token: 0x060027DF RID: 10207 RVA: 0x000F45D1 File Offset: 0x000F27D1
			public LoginViewDesignerActionList(LoginViewDesigner designer) : base(designer.Component)
			{
				this._designer = designer;
			}

			// Token: 0x17000857 RID: 2135
			// (get) Token: 0x060027E0 RID: 10208 RVA: 0x00003B0F File Offset: 0x00001D0F
			// (set) Token: 0x060027E1 RID: 10209 RVA: 0x00003937 File Offset: 0x00001B37
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

			// Token: 0x17000858 RID: 2136
			// (get) Token: 0x060027E2 RID: 10210 RVA: 0x000F45E8 File Offset: 0x000F27E8
			// (set) Token: 0x060027E3 RID: 10211 RVA: 0x000F466C File Offset: 0x000F286C
			[TypeConverter(typeof(LoginViewDesigner.LoginViewDesignerActionList.LoginViewViewTypeConverter))]
			public string View
			{
				get
				{
					int num = this._designer.CurrentView;
					if (num - 2 >= this._designer._loginView.RoleGroups.Count)
					{
						num = this._designer._loginView.RoleGroups.Count + 1;
						this._designer.CurrentView = num;
					}
					if (num == 0)
					{
						return "AnonymousTemplate";
					}
					if (num == 1)
					{
						return "LoggedInTemplate";
					}
					return LoginViewDesigner.CreateRoleGroupCaption(num - 2, this._designer._loginView.RoleGroups);
				}
				set
				{
					if (string.Compare(value, "AnonymousTemplate", StringComparison.Ordinal) == 0)
					{
						this._designer.CurrentView = 0;
					}
					else if (string.Compare(value, "LoggedInTemplate", StringComparison.Ordinal) == 0)
					{
						this._designer.CurrentView = 1;
					}
					else
					{
						RoleGroupCollection roleGroups = this._designer._loginView.RoleGroups;
						for (int i = 0; i < roleGroups.Count; i++)
						{
							string strB = LoginViewDesigner.CreateRoleGroupCaption(i, roleGroups);
							if (string.Compare(value, strB, StringComparison.Ordinal) == 0)
							{
								this._designer.CurrentView = i + 2;
							}
						}
					}
					this._designer.UpdateDesignTimeHtml();
				}
			}

			// Token: 0x060027E4 RID: 10212 RVA: 0x000F46FE File Offset: 0x000F28FE
			public void EditRoleGroups()
			{
				this._designer.EditRoleGroups();
			}

			// Token: 0x060027E5 RID: 10213 RVA: 0x000F470B File Offset: 0x000F290B
			public void LaunchWebAdmin()
			{
				this._designer.LaunchWebAdmin();
			}

			// Token: 0x060027E6 RID: 10214 RVA: 0x000F4718 File Offset: 0x000F2918
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				return new DesignerActionItemCollection
				{
					new DesignerActionMethodItem(this, "EditRoleGroups", SR.GetString("LoginView_EditRoleGroups"), string.Empty, SR.GetString("LoginView_EditRoleGroupsDescription"), true),
					new DesignerActionPropertyItem("View", SR.GetString("WebControls_Views"), string.Empty, SR.GetString("WebControls_ViewsDescription"))
					{
						ShowInSourceView = false
					},
					new DesignerActionMethodItem(this, "LaunchWebAdmin", SR.GetString("Login_LaunchWebAdmin"), string.Empty, SR.GetString("Login_LaunchWebAdminDescription"), true)
				};
			}

			// Token: 0x04001C78 RID: 7288
			private LoginViewDesigner _designer;

			// Token: 0x020005C2 RID: 1474
			private class LoginViewViewTypeConverter : TypeConverter
			{
				// Token: 0x060033FC RID: 13308 RVA: 0x0011C114 File Offset: 0x0011A314
				public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
				{
					LoginViewDesigner.LoginViewDesignerActionList loginViewDesignerActionList = (LoginViewDesigner.LoginViewDesignerActionList)context.Instance;
					LoginView loginView = loginViewDesignerActionList._designer._loginView;
					RoleGroupCollection roleGroups = loginView.RoleGroups;
					string[] array = new string[roleGroups.Count + 2];
					array[0] = "AnonymousTemplate";
					array[1] = "LoggedInTemplate";
					for (int i = 0; i < roleGroups.Count; i++)
					{
						array[i + 2] = LoginViewDesigner.CreateRoleGroupCaption(i, roleGroups);
					}
					return new TypeConverter.StandardValuesCollection(array);
				}

				// Token: 0x060033FD RID: 13309 RVA: 0x00003B0F File Offset: 0x00001D0F
				public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
				{
					return true;
				}

				// Token: 0x060033FE RID: 13310 RVA: 0x00003B0F File Offset: 0x00001D0F
				public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
				{
					return true;
				}
			}
		}
	}
}
