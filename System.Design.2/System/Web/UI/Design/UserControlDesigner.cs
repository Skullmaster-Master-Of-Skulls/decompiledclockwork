using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Web.Configuration;
using System.Web.UI.HtmlControls;

namespace System.Web.UI.Design
{
	// Token: 0x0200007F RID: 127
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class UserControlDesigner : ControlDesigner
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0001298C File Offset: 0x00010B8C
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new UserControlDesigner.UserControlDesignerActionList(this));
				return designerActionListCollection;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool AllowResize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003DF RID: 991 RVA: 0x000129B9 File Offset: 0x00010BB9
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x000129DF File Offset: 0x00010BDF
		internal override bool ShouldCodeSerializeInternal
		{
			get
			{
				return !(base.Component.GetType() == typeof(UserControl)) && base.ShouldCodeSerializeInternal;
			}
			set
			{
				base.ShouldCodeSerializeInternal = value;
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000129E8 File Offset: 0x00010BE8
		private string GenerateUserControlCacheKey(string userControlPath, IThemeResolutionService themeService)
		{
			string text = userControlPath;
			if (themeService != null)
			{
				ThemeProvider stylesheetThemeProvider = themeService.GetStylesheetThemeProvider();
				if (stylesheetThemeProvider != null && !string.IsNullOrEmpty(stylesheetThemeProvider.ThemeName))
				{
					text = text + "|" + stylesheetThemeProvider.ThemeName;
				}
			}
			return text;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00012A24 File Offset: 0x00010C24
		private string GenerateUserControlHashCode(string contents, IThemeResolutionService themeService)
		{
			string text = contents.GetHashCode().ToString(CultureInfo.InvariantCulture);
			if (themeService != null)
			{
				ThemeProvider stylesheetThemeProvider = themeService.GetStylesheetThemeProvider();
				if (stylesheetThemeProvider != null)
				{
					text = text + "|" + stylesheetThemeProvider.ContentHashCode.ToString(CultureInfo.InvariantCulture);
				}
			}
			return text;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00012A74 File Offset: 0x00010C74
		private string MakeAppRelativePath(string path)
		{
			if (string.IsNullOrEmpty(path) || path.StartsWith("~", StringComparison.Ordinal))
			{
				return path;
			}
			string text = Path.GetDirectoryName(base.RootDesigner.DocumentUrl);
			if (string.IsNullOrEmpty(text))
			{
				text = "~";
			}
			text = text.Replace('\\', '/');
			text = text.Replace("~", "file://foo");
			path = path.Replace('\\', '/');
			Uri uri = new Uri(text + "/" + path);
			return uri.ToString().Replace("file://foo", "~");
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00012B08 File Offset: 0x00010D08
		public override string GetDesignTimeHtml()
		{
			if (base.Component.Site != null)
			{
				IWebApplication webApplication = (IWebApplication)base.Component.Site.GetService(typeof(IWebApplication));
				IDesignerHost designerHost = (IDesignerHost)base.Component.Site.GetService(typeof(IDesignerHost));
				if (webApplication != null && designerHost != null && base.RootDesigner.ReferenceManager != null)
				{
					IUserControlDesignerAccessor userControlDesignerAccessor = (IUserControlDesignerAccessor)base.Component;
					string[] array = userControlDesignerAccessor.TagName.Split(new char[]
					{
						':'
					});
					string text = base.RootDesigner.ReferenceManager.GetUserControlPath(array[0], array[1]);
					text = this.MakeAppRelativePath(text);
					IThemeResolutionService themeService = (IThemeResolutionService)base.Component.Site.GetService(typeof(IThemeResolutionService));
					string key = this.GenerateUserControlCacheKey(text, themeService);
					if (!string.IsNullOrEmpty(text))
					{
						string b = null;
						string text2 = string.Empty;
						bool flag = false;
						IDictionary dictionary = UserControlDesigner._antiRecursionDictionary;
						IDictionaryService dictionaryService = (IDictionaryService)webApplication.GetService(typeof(IDictionaryService));
						if (dictionaryService != null)
						{
							dictionary = (IDictionary)dictionaryService.GetValue("__aspnetUserControlCache");
							if (dictionary == null)
							{
								dictionary = new HybridDictionary();
								dictionaryService.SetValue("__aspnetUserControlCache", dictionary);
							}
							Pair pair = (Pair)dictionary[key];
							if (pair != null)
							{
								b = (string)pair.First;
								text2 = (string)pair.Second;
								flag = text2.Contains("mvwres:");
							}
							else
							{
								flag = true;
							}
						}
						IDocumentProjectItem documentProjectItem = webApplication.GetProjectItemFromUrl(text) as IDocumentProjectItem;
						if (documentProjectItem != null)
						{
							this._userControlFound = true;
							StreamReader streamReader = new StreamReader(documentProjectItem.GetContents());
							string text3 = streamReader.ReadToEnd();
							string text4 = null;
							if (!flag)
							{
								text4 = this.GenerateUserControlHashCode(text3, themeService);
								flag = (!string.Equals(text4, b, StringComparison.OrdinalIgnoreCase) || text3.Contains(".ascx"));
							}
							if (!flag)
							{
								goto IL_570;
							}
							if (UserControlDesigner._antiRecursionDictionary.Contains(key))
							{
								return base.CreateErrorDesignTimeHtml(SR.GetString("UserControlDesigner_CyclicError"));
							}
							UserControlDesigner._antiRecursionDictionary[key] = base.CreateErrorDesignTimeHtml(SR.GetString("UserControlDesigner_CyclicError"));
							text2 = string.Empty;
							Pair pair2 = new Pair();
							if (text4 == null)
							{
								text4 = this.GenerateUserControlHashCode(text3, themeService);
							}
							pair2.First = text4;
							pair2.Second = text2;
							dictionary[key] = pair2;
							UserControl userControl = (UserControl)base.Component;
							Page page = new Page();
							try
							{
								page.Controls.Add(userControl);
								IDesignerHost designerHost2 = new UserControlDesigner.UserControlDesignerHost(designerHost, page, text);
								if (!string.IsNullOrEmpty(text3))
								{
									List<Triplet> list = new List<Triplet>();
									Control[] array2 = ControlSerializer.DeserializeControlsInternal(text3, designerHost2, list);
									foreach (Control control in array2)
									{
										if (!(control is LiteralControl) && !(control is DesignerDataBoundLiteralControl) && !(control is DataBoundLiteralControl))
										{
											if (string.IsNullOrEmpty(control.ID))
											{
												throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, SR.GetString("UserControlDesigner_MissingID"), new object[]
												{
													control.GetType().Name
												}));
											}
											designerHost2.Container.Add(control);
										}
										userControl.Controls.Add(control);
									}
									foreach (Triplet triplet in list)
									{
										string tagPrefix = (string)triplet.First;
										Pair pair3 = (Pair)triplet.Second;
										Pair pair4 = (Pair)triplet.Third;
										if (pair3 != null)
										{
											string tagName = (string)pair3.First;
											string src = (string)pair3.Second;
											((UserControlDesigner.UserControlDesignerHost)designerHost2).RegisterUserControl(tagPrefix, tagName, src);
										}
										else if (pair4 != null)
										{
											string tagNamespace = (string)pair4.First;
											string assemblyName = (string)pair4.Second;
											((UserControlDesigner.UserControlDesignerHost)designerHost2).RegisterTagNamespace(tagPrefix, tagNamespace, assemblyName);
										}
									}
									StringBuilder stringBuilder = new StringBuilder();
									foreach (Control control2 in array2)
									{
										string empty = string.Empty;
										if (control2 is LiteralControl)
										{
											stringBuilder.Append(((LiteralControl)control2).Text);
										}
										else if (control2 is DesignerDataBoundLiteralControl)
										{
											stringBuilder.Append(((DesignerDataBoundLiteralControl)control2).Text);
										}
										else if (control2 is DataBoundLiteralControl)
										{
											stringBuilder.Append(((DataBoundLiteralControl)control2).Text);
										}
										else if (control2 is HtmlControl)
										{
											StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture);
											DesignTimeHtmlTextWriter writer = new DesignTimeHtmlTextWriter(stringWriter);
											control2.RenderControl(writer);
											stringBuilder.Append(stringWriter.GetStringBuilder().ToString());
										}
										else
										{
											ControlDesigner controlDesigner = (ControlDesigner)designerHost2.GetDesigner(control2);
											ViewRendering viewRendering = controlDesigner.GetViewRendering();
											stringBuilder.Append(viewRendering.Content);
										}
									}
									text2 = stringBuilder.ToString();
								}
								pair2.Second = text2;
								goto IL_570;
							}
							catch
							{
								dictionary.Remove(key);
								throw;
							}
							finally
							{
								UserControlDesigner._antiRecursionDictionary.Remove(key);
								userControl.Controls.Clear();
								page.Controls.Remove(userControl);
							}
						}
						text2 = base.CreateErrorDesignTimeHtml(SR.GetString("UserControlDesigner_NotFound", new object[]
						{
							text
						}));
						IL_570:
						if (text2.Trim().Length > 0)
						{
							return text2;
						}
					}
				}
			}
			return base.CreatePlaceHolderDesignTimeHtml();
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000130EC File Offset: 0x000112EC
		private void EditUserControl()
		{
			IWebApplication webApplication = (IWebApplication)base.Component.Site.GetService(typeof(IWebApplication));
			if (webApplication != null)
			{
				IUserControlDesignerAccessor userControlDesignerAccessor = (IUserControlDesignerAccessor)base.Component;
				string[] array = userControlDesignerAccessor.TagName.Split(new char[]
				{
					':'
				});
				string text = base.RootDesigner.ReferenceManager.GetUserControlPath(array[0], array[1]);
				if (!string.IsNullOrEmpty(text))
				{
					text = this.MakeAppRelativePath(text);
					IDocumentProjectItem documentProjectItem = webApplication.GetProjectItemFromUrl(text) as IDocumentProjectItem;
					if (documentProjectItem != null)
					{
						documentProjectItem.Open();
					}
				}
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001317F File Offset: 0x0001137F
		private void Refresh()
		{
			this.UpdateDesignTimeHtml();
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00013187 File Offset: 0x00011387
		internal override string GetPersistInnerHtmlInternal()
		{
			if (base.Component.GetType() == typeof(UserControl))
			{
				return null;
			}
			return base.GetPersistInnerHtmlInternal();
		}

		// Token: 0x040001A0 RID: 416
		private const string UserControlCacheKey = "__aspnetUserControlCache";

		// Token: 0x040001A1 RID: 417
		private static IDictionary _antiRecursionDictionary = new HybridDictionary();

		// Token: 0x040001A2 RID: 418
		private bool _userControlFound;

		// Token: 0x040001A3 RID: 419
		private const string _dummyProtocolAndServer = "file://foo";

		// Token: 0x020003B9 RID: 953
		private class UserControlDesignerActionList : DesignerActionList
		{
			// Token: 0x0600263A RID: 9786 RVA: 0x000ECB07 File Offset: 0x000EAD07
			public UserControlDesignerActionList(UserControlDesigner parent) : base(parent.Component)
			{
				this._parent = parent;
			}

			// Token: 0x17000812 RID: 2066
			// (get) Token: 0x0600263B RID: 9787 RVA: 0x00003B0F File Offset: 0x00001D0F
			// (set) Token: 0x0600263C RID: 9788 RVA: 0x00003937 File Offset: 0x00001B37
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

			// Token: 0x0600263D RID: 9789 RVA: 0x000ECB1C File Offset: 0x000EAD1C
			public void EditUserControl()
			{
				this._parent.EditUserControl();
			}

			// Token: 0x0600263E RID: 9790 RVA: 0x000ECB29 File Offset: 0x000EAD29
			public void Refresh()
			{
				this._parent.Refresh();
			}

			// Token: 0x0600263F RID: 9791 RVA: 0x000ECB38 File Offset: 0x000EAD38
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
				if (this._parent._userControlFound)
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "EditUserControl", SR.GetString("UserControlDesigner_EditUserControl"), string.Empty, string.Empty, true));
				}
				designerActionItemCollection.Add(new DesignerActionMethodItem(this, "Refresh", SR.GetString("UserControlDesigner_Refresh"), string.Empty, string.Empty, true)
				{
					ShowInSourceView = false
				});
				return designerActionItemCollection;
			}

			// Token: 0x04001BB8 RID: 7096
			private UserControlDesigner _parent;
		}

		// Token: 0x020003BA RID: 954
		private sealed class TagNamespaceRegisterEntry
		{
			// Token: 0x06002640 RID: 9792 RVA: 0x000ECBAE File Offset: 0x000EADAE
			public TagNamespaceRegisterEntry(string tagPrefix, string tagNamespace, string assemblyName)
			{
				this.TagPrefix = tagPrefix;
				this.TagNamespace = tagNamespace;
				this.AssemblyName = assemblyName;
			}

			// Token: 0x04001BB9 RID: 7097
			public string TagPrefix;

			// Token: 0x04001BBA RID: 7098
			public string TagNamespace;

			// Token: 0x04001BBB RID: 7099
			public string AssemblyName;
		}

		// Token: 0x020003BB RID: 955
		private sealed class UserControlDesignerHost : IContainer, IDisposable, IDesignerHost, IServiceContainer, IServiceProvider, IUrlResolutionService
		{
			// Token: 0x06002641 RID: 9793 RVA: 0x000ECBCC File Offset: 0x000EADCC
			public UserControlDesignerHost(IDesignerHost host, IComponent rootComponent, string userControlPath)
			{
				this._host = host;
				this._componentTable = new Hashtable();
				this._designerTable = new Hashtable();
				this._rootComponent = rootComponent;
				this._userControlPath = userControlPath;
				this._rootComponent.Site = new UserControlDesigner.DummySite(this._rootComponent, this);
			}

			// Token: 0x06002642 RID: 9794 RVA: 0x000ECC24 File Offset: 0x000EAE24
			~UserControlDesignerHost()
			{
				this.Dispose(false);
			}

			// Token: 0x17000813 RID: 2067
			// (get) Token: 0x06002643 RID: 9795 RVA: 0x000ECC54 File Offset: 0x000EAE54
			private Hashtable ComponentTable
			{
				get
				{
					return this._componentTable;
				}
			}

			// Token: 0x17000814 RID: 2068
			// (get) Token: 0x06002644 RID: 9796 RVA: 0x000ECC5C File Offset: 0x000EAE5C
			private Hashtable DesignerTable
			{
				get
				{
					return this._designerTable;
				}
			}

			// Token: 0x06002645 RID: 9797 RVA: 0x000ECC64 File Offset: 0x000EAE64
			public void ClearComponents()
			{
				for (int i = 0; i < this.DesignerTable.Count; i++)
				{
					if (this.DesignerTable[i] != null)
					{
						IDesigner designer = (IDesigner)this.DesignerTable[i];
						try
						{
							designer.Dispose();
						}
						catch
						{
						}
					}
				}
				this.DesignerTable.Clear();
				for (int j = 0; j < this.ComponentTable.Count; j++)
				{
					if (this.ComponentTable[j] != null)
					{
						IComponent component = (IComponent)this.ComponentTable[j];
						ISite site = component.Site;
						try
						{
							component.Dispose();
						}
						catch
						{
						}
						if (component.Site != null)
						{
							((IContainer)this).Remove(component);
						}
					}
				}
				this.ComponentTable.Clear();
			}

			// Token: 0x06002646 RID: 9798 RVA: 0x000ECD50 File Offset: 0x000EAF50
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06002647 RID: 9799 RVA: 0x000ECD5F File Offset: 0x000EAF5F
			public void Dispose(bool disposing)
			{
				if (!this._disposed && disposing)
				{
					this.ClearComponents();
					this._host = null;
					this._componentTable = null;
					this._designerTable = null;
				}
				this._disposed = true;
			}

			// Token: 0x06002648 RID: 9800 RVA: 0x000ECD90 File Offset: 0x000EAF90
			private IComponent[] GetComponents()
			{
				int count = this.ComponentTable.Count;
				IComponent[] array = new IComponent[count];
				if (count != 0)
				{
					int num = 0;
					foreach (object obj in this.ComponentTable.Values)
					{
						IComponent component = (IComponent)obj;
						array[num++] = component;
					}
				}
				return array;
			}

			// Token: 0x06002649 RID: 9801 RVA: 0x000ECE10 File Offset: 0x000EB010
			public void RegisterTagNamespace(string tagPrefix, string tagNamespace, string assemblyName)
			{
				if (this._tagNamespaceRegisterEntries == null)
				{
					this._tagNamespaceRegisterEntries = new List<UserControlDesigner.TagNamespaceRegisterEntry>();
				}
				this._tagNamespaceRegisterEntries.Add(new UserControlDesigner.TagNamespaceRegisterEntry(tagPrefix, tagNamespace, assemblyName));
			}

			// Token: 0x0600264A RID: 9802 RVA: 0x000ECE38 File Offset: 0x000EB038
			public void RegisterUserControl(string tagPrefix, string tagName, string src)
			{
				if (this._userControlRegisterEntries == null)
				{
					this._userControlRegisterEntries = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
				}
				this._userControlRegisterEntries[tagPrefix + ":" + tagName] = src;
			}

			// Token: 0x17000815 RID: 2069
			// (get) Token: 0x0600264B RID: 9803 RVA: 0x000ECE6A File Offset: 0x000EB06A
			ComponentCollection IContainer.Components
			{
				get
				{
					return new ComponentCollection(this.GetComponents());
				}
			}

			// Token: 0x0600264C RID: 9804 RVA: 0x000ECE77 File Offset: 0x000EB077
			void IContainer.Add(IComponent component)
			{
				((IContainer)this).Add(component, null);
			}

			// Token: 0x0600264D RID: 9805 RVA: 0x000ECE84 File Offset: 0x000EB084
			void IContainer.Add(IComponent component, string name)
			{
				if (component == null)
				{
					throw new ArgumentNullException("component");
				}
				if (component.Site == null)
				{
					component.Site = new UserControlDesigner.DummySite(component, this);
					if (component is Control)
					{
						component.Site.Name = ((Control)component).ID;
					}
					else
					{
						ISite site = component.Site;
						string str = "Temp";
						int nameCounter = this._nameCounter;
						this._nameCounter = nameCounter + 1;
						site.Name = str + nameCounter.ToString();
					}
				}
				if (name == null)
				{
					name = component.Site.Name;
				}
				if (this.ComponentTable[name] != null)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, SR.GetString("UserControlDesignerHost_ComponentAlreadyExists"), new object[]
					{
						name
					}));
				}
				this.ComponentTable[name] = component;
				IDesigner designer = TypeDescriptor.CreateDesigner(component, typeof(IDesigner));
				designer.Initialize(component);
				this.DesignerTable[component] = designer;
				if (component is Control)
				{
					((Control)component).Page = (Page)this._rootComponent;
				}
			}

			// Token: 0x0600264E RID: 9806 RVA: 0x000ECF94 File Offset: 0x000EB194
			void IContainer.Remove(IComponent component)
			{
				if (component == null)
				{
					throw new ArgumentNullException("component");
				}
				if (component.Site == null)
				{
					return;
				}
				string name = component.Site.Name;
				if (name != null && this.ComponentTable[name] == component)
				{
					if (this.DesignerTable != null)
					{
						IDesigner designer = (IDesigner)this.DesignerTable[component];
						if (designer != null)
						{
							this.DesignerTable.Remove(component);
							designer.Dispose();
						}
					}
					this.ComponentTable.Remove(name);
					component.Dispose();
					component.Site = null;
				}
			}

			// Token: 0x0600264F RID: 9807 RVA: 0x000ED01E File Offset: 0x000EB21E
			void IDisposable.Dispose()
			{
				this.Dispose();
			}

			// Token: 0x06002650 RID: 9808 RVA: 0x000ED028 File Offset: 0x000EB228
			object IServiceProvider.GetService(Type serviceType)
			{
				if (serviceType == typeof(IDesignerHost) || serviceType == typeof(IContainer) || serviceType == typeof(IUrlResolutionService))
				{
					return this;
				}
				return this._host.GetService(serviceType);
			}

			// Token: 0x06002651 RID: 9809 RVA: 0x00003937 File Offset: 0x00001B37
			void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
			{
			}

			// Token: 0x06002652 RID: 9810 RVA: 0x00003937 File Offset: 0x00001B37
			void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback)
			{
			}

			// Token: 0x06002653 RID: 9811 RVA: 0x00003937 File Offset: 0x00001B37
			void IServiceContainer.AddService(Type serviceType, object serviceInstance, bool promote)
			{
			}

			// Token: 0x06002654 RID: 9812 RVA: 0x00003937 File Offset: 0x00001B37
			void IServiceContainer.AddService(Type serviceType, object serviceInstance)
			{
			}

			// Token: 0x06002655 RID: 9813 RVA: 0x00003937 File Offset: 0x00001B37
			void IServiceContainer.RemoveService(Type serviceType, bool promote)
			{
			}

			// Token: 0x06002656 RID: 9814 RVA: 0x00003937 File Offset: 0x00001B37
			void IServiceContainer.RemoveService(Type serviceType)
			{
			}

			// Token: 0x17000816 RID: 2070
			// (get) Token: 0x06002657 RID: 9815 RVA: 0x0000CA50 File Offset: 0x0000AC50
			IContainer IDesignerHost.Container
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17000817 RID: 2071
			// (get) Token: 0x06002658 RID: 9816 RVA: 0x000ED079 File Offset: 0x000EB279
			bool IDesignerHost.InTransaction
			{
				get
				{
					return this._host.InTransaction;
				}
			}

			// Token: 0x17000818 RID: 2072
			// (get) Token: 0x06002659 RID: 9817 RVA: 0x000ED086 File Offset: 0x000EB286
			bool IDesignerHost.Loading
			{
				get
				{
					return this._host.Loading;
				}
			}

			// Token: 0x17000819 RID: 2073
			// (get) Token: 0x0600265A RID: 9818 RVA: 0x000ED093 File Offset: 0x000EB293
			string IDesignerHost.TransactionDescription
			{
				get
				{
					return this._host.TransactionDescription;
				}
			}

			// Token: 0x1700081A RID: 2074
			// (get) Token: 0x0600265B RID: 9819 RVA: 0x000ED0A0 File Offset: 0x000EB2A0
			IComponent IDesignerHost.RootComponent
			{
				get
				{
					return this._rootComponent;
				}
			}

			// Token: 0x1700081B RID: 2075
			// (get) Token: 0x0600265C RID: 9820 RVA: 0x000ED0A8 File Offset: 0x000EB2A8
			string IDesignerHost.RootComponentClassName
			{
				get
				{
					return this._rootComponent.GetType().Name;
				}
			}

			// Token: 0x14000060 RID: 96
			// (add) Token: 0x0600265D RID: 9821 RVA: 0x00003937 File Offset: 0x00001B37
			// (remove) Token: 0x0600265E RID: 9822 RVA: 0x00003937 File Offset: 0x00001B37
			event EventHandler IDesignerHost.Activated
			{
				add
				{
				}
				remove
				{
				}
			}

			// Token: 0x14000061 RID: 97
			// (add) Token: 0x0600265F RID: 9823 RVA: 0x00003937 File Offset: 0x00001B37
			// (remove) Token: 0x06002660 RID: 9824 RVA: 0x00003937 File Offset: 0x00001B37
			event EventHandler IDesignerHost.Deactivated
			{
				add
				{
				}
				remove
				{
				}
			}

			// Token: 0x14000062 RID: 98
			// (add) Token: 0x06002661 RID: 9825 RVA: 0x000ED0BA File Offset: 0x000EB2BA
			// (remove) Token: 0x06002662 RID: 9826 RVA: 0x000ED0C8 File Offset: 0x000EB2C8
			event EventHandler IDesignerHost.LoadComplete
			{
				add
				{
					this._host.LoadComplete += value;
				}
				remove
				{
					this._host.LoadComplete -= value;
				}
			}

			// Token: 0x14000063 RID: 99
			// (add) Token: 0x06002663 RID: 9827 RVA: 0x00003937 File Offset: 0x00001B37
			// (remove) Token: 0x06002664 RID: 9828 RVA: 0x00003937 File Offset: 0x00001B37
			event DesignerTransactionCloseEventHandler IDesignerHost.TransactionClosed
			{
				add
				{
				}
				remove
				{
				}
			}

			// Token: 0x14000064 RID: 100
			// (add) Token: 0x06002665 RID: 9829 RVA: 0x00003937 File Offset: 0x00001B37
			// (remove) Token: 0x06002666 RID: 9830 RVA: 0x00003937 File Offset: 0x00001B37
			event DesignerTransactionCloseEventHandler IDesignerHost.TransactionClosing
			{
				add
				{
				}
				remove
				{
				}
			}

			// Token: 0x14000065 RID: 101
			// (add) Token: 0x06002667 RID: 9831 RVA: 0x00003937 File Offset: 0x00001B37
			// (remove) Token: 0x06002668 RID: 9832 RVA: 0x00003937 File Offset: 0x00001B37
			event EventHandler IDesignerHost.TransactionOpened
			{
				add
				{
				}
				remove
				{
				}
			}

			// Token: 0x14000066 RID: 102
			// (add) Token: 0x06002669 RID: 9833 RVA: 0x00003937 File Offset: 0x00001B37
			// (remove) Token: 0x0600266A RID: 9834 RVA: 0x00003937 File Offset: 0x00001B37
			event EventHandler IDesignerHost.TransactionOpening
			{
				add
				{
				}
				remove
				{
				}
			}

			// Token: 0x0600266B RID: 9835 RVA: 0x00003937 File Offset: 0x00001B37
			void IDesignerHost.Activate()
			{
			}

			// Token: 0x0600266C RID: 9836 RVA: 0x00003598 File Offset: 0x00001798
			IComponent IDesignerHost.CreateComponent(Type componentType)
			{
				return null;
			}

			// Token: 0x0600266D RID: 9837 RVA: 0x00003598 File Offset: 0x00001798
			IComponent IDesignerHost.CreateComponent(Type componentType, string name)
			{
				return null;
			}

			// Token: 0x0600266E RID: 9838 RVA: 0x000ED0D6 File Offset: 0x000EB2D6
			DesignerTransaction IDesignerHost.CreateTransaction()
			{
				return this._host.CreateTransaction();
			}

			// Token: 0x0600266F RID: 9839 RVA: 0x000ED0E3 File Offset: 0x000EB2E3
			DesignerTransaction IDesignerHost.CreateTransaction(string description)
			{
				return this._host.CreateTransaction(description);
			}

			// Token: 0x06002670 RID: 9840 RVA: 0x000ED0F1 File Offset: 0x000EB2F1
			void IDesignerHost.DestroyComponent(IComponent component)
			{
				((IContainer)this).Remove(component);
			}

			// Token: 0x06002671 RID: 9841 RVA: 0x000ED0FA File Offset: 0x000EB2FA
			Type IDesignerHost.GetType(string typeName)
			{
				return this._host.GetType(typeName);
			}

			// Token: 0x06002672 RID: 9842 RVA: 0x000ED108 File Offset: 0x000EB308
			IDesigner IDesignerHost.GetDesigner(IComponent component)
			{
				IDesigner result;
				if (component == this._host.RootComponent)
				{
					result = this._host.GetDesigner(component);
				}
				else if (component == this._rootComponent)
				{
					result = new UserControlDesigner.DummyRootDesigner((WebFormsRootDesigner)this._host.GetDesigner(this._host.RootComponent), this._userControlRegisterEntries, this._tagNamespaceRegisterEntries, this._userControlPath);
				}
				else
				{
					result = (IDesigner)this.DesignerTable[component];
				}
				return result;
			}

			// Token: 0x06002673 RID: 9843 RVA: 0x000ED188 File Offset: 0x000EB388
			string IUrlResolutionService.ResolveClientUrl(string relativeUrl)
			{
				if (relativeUrl == null)
				{
					throw new ArgumentNullException("relativeUrl");
				}
				if (UserControlDesigner.UserControlDesignerHost.IsRooted(relativeUrl) || relativeUrl.Contains("mvwres:"))
				{
					return relativeUrl;
				}
				IUrlResolutionService urlResolutionService = (IUrlResolutionService)this._host.GetService(typeof(IUrlResolutionService));
				if (urlResolutionService != null)
				{
					if (UserControlDesigner.UserControlDesignerHost.IsAppRelativePath(relativeUrl))
					{
						relativeUrl = urlResolutionService.ResolveClientUrl(relativeUrl);
					}
					else
					{
						string text = this._userControlPath;
						if (text != null && text.Length != 0)
						{
							if (UserControlDesigner.UserControlDesignerHost.IsAppRelativePath(text))
							{
								text = text.Replace("~", "file://foo");
								Uri uri = new Uri(text);
								string[] segments = uri.Segments;
								StringBuilder stringBuilder = new StringBuilder("~");
								for (int i = 0; i < segments.Length - 1; i++)
								{
									stringBuilder.Append(segments[i]);
								}
								relativeUrl = urlResolutionService.ResolveClientUrl(stringBuilder.ToString() + relativeUrl);
							}
							else
							{
								string fileName = Path.GetFileName(text);
								int length = text.LastIndexOf(fileName, StringComparison.Ordinal);
								relativeUrl = Path.Combine(text.Substring(0, length), relativeUrl);
							}
						}
					}
				}
				return relativeUrl;
			}

			// Token: 0x06002674 RID: 9844 RVA: 0x00013A4F File Offset: 0x00011C4F
			private static bool IsRooted(string basepath)
			{
				return basepath == null || basepath.Length == 0 || basepath[0] == '/' || basepath[0] == '\\';
			}

			// Token: 0x06002675 RID: 9845 RVA: 0x00013A74 File Offset: 0x00011C74
			private static bool IsAppRelativePath(string path)
			{
				return path.Length >= 2 && path[0] == '~' && (path[1] == '/' || path[1] == '\\');
			}

			// Token: 0x04001BBC RID: 7100
			private Hashtable _componentTable;

			// Token: 0x04001BBD RID: 7101
			private Hashtable _designerTable;

			// Token: 0x04001BBE RID: 7102
			private IDesignerHost _host;

			// Token: 0x04001BBF RID: 7103
			private bool _disposed;

			// Token: 0x04001BC0 RID: 7104
			private IComponent _rootComponent;

			// Token: 0x04001BC1 RID: 7105
			private int _nameCounter;

			// Token: 0x04001BC2 RID: 7106
			private string _userControlPath;

			// Token: 0x04001BC3 RID: 7107
			private IDictionary<string, string> _userControlRegisterEntries;

			// Token: 0x04001BC4 RID: 7108
			private IList<UserControlDesigner.TagNamespaceRegisterEntry> _tagNamespaceRegisterEntries;

			// Token: 0x04001BC5 RID: 7109
			private const string dummyProtocolAndServer = "file://foo";

			// Token: 0x04001BC6 RID: 7110
			private const char appRelativeCharacter = '~';
		}

		// Token: 0x020003BC RID: 956
		private sealed class DummyRootDesigner : WebFormsRootDesigner
		{
			// Token: 0x06002676 RID: 9846 RVA: 0x000ED298 File Offset: 0x000EB498
			public DummyRootDesigner(WebFormsRootDesigner rootDesigner, IDictionary<string, string> userControlRegisterEntries, IList<UserControlDesigner.TagNamespaceRegisterEntry> tagNamespaceRegisterEntries, string documentUrl)
			{
				this._rootDesigner = rootDesigner;
				this._userControlRegisterEntries = userControlRegisterEntries;
				this._tagNamespaceRegisterEntries = tagNamespaceRegisterEntries;
				this._documentUrl = documentUrl;
			}

			// Token: 0x1700081C RID: 2076
			// (get) Token: 0x06002677 RID: 9847 RVA: 0x000ED2BD File Offset: 0x000EB4BD
			public override string DocumentUrl
			{
				get
				{
					return this._documentUrl;
				}
			}

			// Token: 0x1700081D RID: 2077
			// (get) Token: 0x06002678 RID: 9848 RVA: 0x000ED2C5 File Offset: 0x000EB4C5
			public override bool IsLoading
			{
				get
				{
					return this._rootDesigner.IsLoading;
				}
			}

			// Token: 0x1700081E RID: 2078
			// (get) Token: 0x06002679 RID: 9849 RVA: 0x00003B0F File Offset: 0x00001D0F
			public override bool IsDesignerViewLocked
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700081F RID: 2079
			// (get) Token: 0x0600267A RID: 9850 RVA: 0x000ED2D2 File Offset: 0x000EB4D2
			public override WebFormsReferenceManager ReferenceManager
			{
				get
				{
					return new UserControlDesigner.DummyRootDesigner.DummyWebFormsReferenceManager(this, this._rootDesigner.ReferenceManager, this._userControlRegisterEntries, this._tagNamespaceRegisterEntries);
				}
			}

			// Token: 0x17000820 RID: 2080
			// (get) Token: 0x0600267B RID: 9851 RVA: 0x000ED2F1 File Offset: 0x000EB4F1
			internal IWebApplication WebApplication
			{
				get
				{
					if (this._rootDesigner != null)
					{
						return (IWebApplication)this._rootDesigner.GetService(typeof(IWebApplication));
					}
					return null;
				}
			}

			// Token: 0x0600267C RID: 9852 RVA: 0x0000C5AC File Offset: 0x0000A7AC
			public override void AddClientScriptToDocument(ClientScriptItem scriptItem)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600267D RID: 9853 RVA: 0x0000C5AC File Offset: 0x0000A7AC
			public override string AddControlToDocument(Control newControl, Control referenceControl, ControlLocation location)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600267E RID: 9854 RVA: 0x0000C5AC File Offset: 0x0000A7AC
			public override ClientScriptItemCollection GetClientScriptsInDocument()
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600267F RID: 9855 RVA: 0x000ED317 File Offset: 0x000EB517
			protected internal override void GetControlViewAndTag(Control control, out IControlDesignerView view, out IControlDesignerTag tag)
			{
				view = null;
				tag = null;
			}

			// Token: 0x06002680 RID: 9856 RVA: 0x0000C5AC File Offset: 0x0000A7AC
			public override void RemoveClientScriptFromDocument(string clientScriptId)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06002681 RID: 9857 RVA: 0x0000C5AC File Offset: 0x0000A7AC
			public override void RemoveControlFromDocument(Control control)
			{
				throw new NotSupportedException();
			}

			// Token: 0x04001BC7 RID: 7111
			internal WebFormsRootDesigner _rootDesigner;

			// Token: 0x04001BC8 RID: 7112
			private IDictionary<string, string> _userControlRegisterEntries;

			// Token: 0x04001BC9 RID: 7113
			private IList<UserControlDesigner.TagNamespaceRegisterEntry> _tagNamespaceRegisterEntries;

			// Token: 0x04001BCA RID: 7114
			private string _documentUrl;

			// Token: 0x020005BE RID: 1470
			private sealed class DummyWebFormsReferenceManager : WebFormsReferenceManager
			{
				// Token: 0x060033E1 RID: 13281 RVA: 0x0011B7B4 File Offset: 0x001199B4
				public DummyWebFormsReferenceManager(UserControlDesigner.DummyRootDesigner owner, WebFormsReferenceManager baseReferenceManager, IDictionary<string, string> baseUserControlRegisterEntries, IList<UserControlDesigner.TagNamespaceRegisterEntry> tagNamespaceRegisterEntries)
				{
					this._owner = owner;
					this._baseReferenceManager = baseReferenceManager;
					this._baseUserControlRegisterEntries = baseUserControlRegisterEntries;
					this._tagNamespaceRegisterEntries = tagNamespaceRegisterEntries;
				}

				// Token: 0x060033E2 RID: 13282 RVA: 0x0011B7DC File Offset: 0x001199DC
				private bool GetNamespaceAndAssemblyFromType(Type objectType, out string ns, out string asmName)
				{
					if (objectType != null)
					{
						Assembly assembly = objectType.Module.Assembly;
						if (assembly.GlobalAssemblyCache)
						{
							asmName = assembly.FullName;
						}
						else
						{
							asmName = assembly.GetName().Name;
						}
						ns = objectType.Namespace;
						if (ns == null)
						{
							ns = string.Empty;
						}
						ns = ns.TrimEnd(new char[]
						{
							'.'
						});
						if (ns != null && asmName != null && asmName.Length > 0)
						{
							return true;
						}
					}
					ns = null;
					asmName = null;
					return false;
				}

				// Token: 0x060033E3 RID: 13283 RVA: 0x0011B85F File Offset: 0x00119A5F
				public override Type GetType(string tagPrefix, string tagName)
				{
					return this._baseReferenceManager.GetType(tagPrefix, tagName);
				}

				// Token: 0x060033E4 RID: 13284 RVA: 0x0011B870 File Offset: 0x00119A70
				public override string GetTagPrefix(Type objectType)
				{
					string text;
					string text2;
					if (this.GetNamespaceAndAssemblyFromType(objectType, out text, out text2))
					{
						string text3 = null;
						string text4 = null;
						if (text != null && text2 != null)
						{
							string assemblySpec = UserControlDesigner.DummyRootDesigner.DummyWebFormsReferenceManager.GetAssemblySpec(objectType.Module.Assembly.GetName());
							foreach (UserControlDesigner.TagNamespaceRegisterEntry tagNamespaceRegisterEntry in this._tagNamespaceRegisterEntries)
							{
								if (string.Equals(text, tagNamespaceRegisterEntry.TagNamespace, StringComparison.OrdinalIgnoreCase))
								{
									string assemblyName = tagNamespaceRegisterEntry.AssemblyName;
									if (!string.IsNullOrEmpty(assemblyName))
									{
										if (string.Equals(text2, assemblyName, StringComparison.OrdinalIgnoreCase))
										{
											text3 = tagNamespaceRegisterEntry.TagPrefix;
											break;
										}
										string text5 = null;
										try
										{
											text5 = UserControlDesigner.DummyRootDesigner.DummyWebFormsReferenceManager.GetAssemblySpec(new AssemblyName(assemblyName));
										}
										catch
										{
										}
										if (text5 != null && UserControlDesigner.DummyRootDesigner.DummyWebFormsReferenceManager.IsFrameworkTagPrefixAssembly(text5) && string.Equals(text5, assemblySpec, StringComparison.OrdinalIgnoreCase))
										{
											text3 = tagNamespaceRegisterEntry.TagPrefix;
											break;
										}
									}
									else if (text4 == null)
									{
										text4 = tagNamespaceRegisterEntry.TagPrefix;
									}
								}
							}
							if (text3 == null)
							{
								if (text4 != null)
								{
									text3 = text4;
								}
								else
								{
									text3 = string.Empty;
								}
							}
							return text3;
						}
					}
					return this._baseReferenceManager.GetTagPrefix(objectType);
				}

				// Token: 0x060033E5 RID: 13285 RVA: 0x0011B9A0 File Offset: 0x00119BA0
				private static bool IsFrameworkTagPrefixAssembly(string spec)
				{
					return UserControlDesigner.DummyRootDesigner.DummyWebFormsReferenceManager.FrameworkTagPrefixAssemblySpecs.Contains(spec, StringComparer.OrdinalIgnoreCase);
				}

				// Token: 0x060033E6 RID: 13286 RVA: 0x0011B9B4 File Offset: 0x00119BB4
				private static string GetAssemblySpec(AssemblyName assemblyName)
				{
					string str = (assemblyName.CultureInfo == null || string.IsNullOrEmpty(assemblyName.CultureInfo.Name)) ? "neutral" : assemblyName.CultureInfo.Name;
					string str2 = assemblyName.Name ?? string.Empty;
					string text = str2 + ", Culture=" + str;
					string text2 = UserControlDesigner.DummyRootDesigner.DummyWebFormsReferenceManager.EncodeHexString(assemblyName.GetPublicKeyToken());
					if (text2 != null)
					{
						text = text + ", PublicKeyToken=" + ((text2.Length == 0) ? "null" : text2);
					}
					return text;
				}

				// Token: 0x060033E7 RID: 13287 RVA: 0x0011BA38 File Offset: 0x00119C38
				private static string EncodeHexString(byte[] sArray)
				{
					string result = null;
					if (sArray != null)
					{
						char[] array = new char[sArray.Length * 2];
						int i = 0;
						int num = 0;
						while (i < sArray.Length)
						{
							int num2 = (sArray[i] & 240) >> 4;
							array[num++] = UserControlDesigner.DummyRootDesigner.DummyWebFormsReferenceManager.HexDigit(num2);
							num2 = (int)(sArray[i] & 15);
							array[num++] = UserControlDesigner.DummyRootDesigner.DummyWebFormsReferenceManager.HexDigit(num2);
							i++;
						}
						result = new string(array);
					}
					return result;
				}

				// Token: 0x060033E8 RID: 13288 RVA: 0x0011BA9F File Offset: 0x00119C9F
				private static char HexDigit(int num)
				{
					return (char)((num < 10) ? (num + 48) : (num + 87));
				}

				// Token: 0x060033E9 RID: 13289 RVA: 0x0000C5AC File Offset: 0x0000A7AC
				public override string RegisterTagPrefix(Type objectType)
				{
					throw new NotSupportedException();
				}

				// Token: 0x060033EA RID: 13290 RVA: 0x0011BAB1 File Offset: 0x00119CB1
				private static bool IsRooted(string basepath)
				{
					return basepath == null || basepath.Length == 0 || basepath[0] == '/' || basepath[0] == '\\' || Path.IsPathRooted(basepath) || basepath.IndexOf(Path.VolumeSeparatorChar) >= 0;
				}

				// Token: 0x060033EB RID: 13291 RVA: 0x00013A74 File Offset: 0x00011C74
				private static bool IsAppRelativePath(string path)
				{
					return path.Length >= 2 && path[0] == '~' && (path[1] == '/' || path[1] == '\\');
				}

				// Token: 0x060033EC RID: 13292 RVA: 0x0011BAF0 File Offset: 0x00119CF0
				private static string ResolveFileUrl(string baseURL, string relativeFileUrl)
				{
					if (!UserControlDesigner.DummyRootDesigner.DummyWebFormsReferenceManager.IsRooted(relativeFileUrl) && !UserControlDesigner.DummyRootDesigner.DummyWebFormsReferenceManager.IsAppRelativePath(relativeFileUrl))
					{
						string fileName = Path.GetFileName(baseURL);
						int length = baseURL.LastIndexOf(fileName, StringComparison.Ordinal);
						string path = baseURL.Substring(0, length);
						relativeFileUrl = Path.Combine(path, relativeFileUrl);
					}
					return relativeFileUrl;
				}

				// Token: 0x060033ED RID: 13293 RVA: 0x0011BB30 File Offset: 0x00119D30
				public override ICollection GetRegisterDirectives()
				{
					if (this._registerDirectives == null)
					{
						try
						{
							this._registerDirectives = new Collection<string>();
							IWebApplication webApplication = this._owner.WebApplication;
							if (webApplication != null)
							{
								Configuration configuration = webApplication.OpenWebConfiguration(true);
								if (configuration != null)
								{
									PagesSection pagesSection = (PagesSection)configuration.GetSection("system.web/pages");
									if (pagesSection != null)
									{
										string filePath = configuration.FilePath;
										IProjectItem rootProjectItem = webApplication.RootProjectItem;
										string physicalPath = rootProjectItem.PhysicalPath;
										string baseURL = "~/" + filePath.Substring(physicalPath.Length, filePath.Length - physicalPath.Length);
										foreach (object obj in pagesSection.Controls)
										{
											TagPrefixInfo tagPrefixInfo = (TagPrefixInfo)obj;
											Dictionary<string, string> dictionary = new Dictionary<string, string>();
											tagPrefixInfo.Source = UserControlDesigner.DummyRootDesigner.DummyWebFormsReferenceManager.ResolveFileUrl(baseURL, tagPrefixInfo.Source);
											ElementInformation elementInformation = tagPrefixInfo.ElementInformation;
											foreach (object obj2 in elementInformation.Properties)
											{
												PropertyInformation propertyInformation = (PropertyInformation)obj2;
												if (propertyInformation.Type == typeof(string))
												{
													dictionary[propertyInformation.Name] = ((propertyInformation.ValueOrigin != PropertyValueOrigin.Default) ? ((string)propertyInformation.Value) : null);
												}
											}
											this._registerDirectives.Add(this.GenerateRegisterDirective(dictionary["tagPrefix"], dictionary["tagName"], dictionary["namespace"], dictionary["assembly"], dictionary["src"]));
										}
									}
								}
							}
						}
						catch (Exception ex)
						{
						}
						if (this._baseUserControlRegisterEntries != null)
						{
							foreach (KeyValuePair<string, string> keyValuePair in this._baseUserControlRegisterEntries)
							{
								string item = this.GenerateRegisterDirective(keyValuePair.Key, keyValuePair.Value);
								if (!this._registerDirectives.Contains(item))
								{
									this._registerDirectives.Add(item);
								}
							}
						}
						if (this._tagNamespaceRegisterEntries != null)
						{
							foreach (UserControlDesigner.TagNamespaceRegisterEntry tagNamespaceRegisterEntry in this._tagNamespaceRegisterEntries)
							{
								string item2 = this.GenerateRegisterDirective(tagNamespaceRegisterEntry.TagPrefix, null, tagNamespaceRegisterEntry.TagNamespace, tagNamespaceRegisterEntry.AssemblyName, null);
								if (!this._registerDirectives.Contains(item2))
								{
									this._registerDirectives.Add(item2);
								}
							}
						}
					}
					return this._registerDirectives;
				}

				// Token: 0x060033EE RID: 13294 RVA: 0x0011BE60 File Offset: 0x0011A060
				public override string GetUserControlPath(string tagPrefix, string tagName)
				{
					return this._owner._userControlRegisterEntries[tagPrefix + ":" + tagName];
				}

				// Token: 0x060033EF RID: 13295 RVA: 0x0011BE80 File Offset: 0x0011A080
				private string GenerateRegisterDirective(string tagPrefix, string tagName, string ns, string assembly, string src)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("<%@ Register");
					if (tagPrefix != null && tagPrefix.Length > 0)
					{
						stringBuilder.Append(" TagPrefix=\"");
						stringBuilder.Append(tagPrefix);
						stringBuilder.Append("\"");
					}
					if (!string.IsNullOrEmpty(tagName))
					{
						stringBuilder.Append(" TagName=\"");
						stringBuilder.Append(tagName);
						stringBuilder.Append("\"");
					}
					if (ns != null)
					{
						stringBuilder.Append(" Namespace=\"");
						stringBuilder.Append(ns);
						stringBuilder.Append("\"");
					}
					if (!string.IsNullOrEmpty(assembly))
					{
						stringBuilder.Append(" Assembly=\"");
						stringBuilder.Append(assembly);
						stringBuilder.Append("\"");
					}
					if (!string.IsNullOrEmpty(src))
					{
						stringBuilder.Append(" Src=\"");
						stringBuilder.Append(src);
						stringBuilder.Append("\"");
					}
					stringBuilder.Append("%>");
					return stringBuilder.ToString();
				}

				// Token: 0x060033F0 RID: 13296 RVA: 0x0011BF7C File Offset: 0x0011A17C
				private string GenerateRegisterDirective(string tagPrefixAndName, string src)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("<%@ Register");
					if (!string.IsNullOrEmpty(tagPrefixAndName))
					{
						string[] array = tagPrefixAndName.Split(new char[]
						{
							':'
						});
						if (array.Length == 2)
						{
							stringBuilder.Append(" TagPrefix=\"");
							stringBuilder.Append(array[0]);
							stringBuilder.Append("\"");
							stringBuilder.Append(" TagName=\"");
							stringBuilder.Append(array[1]);
							stringBuilder.Append("\"");
						}
					}
					if (!string.IsNullOrEmpty(src))
					{
						stringBuilder.Append(" Src=\"");
						stringBuilder.Append(src);
						stringBuilder.Append("\"");
					}
					stringBuilder.Append("%>");
					return stringBuilder.ToString();
				}

				// Token: 0x040022C2 RID: 8898
				private UserControlDesigner.DummyRootDesigner _owner;

				// Token: 0x040022C3 RID: 8899
				private WebFormsReferenceManager _baseReferenceManager;

				// Token: 0x040022C4 RID: 8900
				private Collection<string> _registerDirectives;

				// Token: 0x040022C5 RID: 8901
				private IDictionary<string, string> _baseUserControlRegisterEntries;

				// Token: 0x040022C6 RID: 8902
				private IList<UserControlDesigner.TagNamespaceRegisterEntry> _tagNamespaceRegisterEntries;

				// Token: 0x040022C7 RID: 8903
				private static readonly string[] FrameworkTagPrefixAssemblySpecs = new string[]
				{
					UserControlDesigner.DummyRootDesigner.DummyWebFormsReferenceManager.GetAssemblySpec(new AssemblyName("System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")),
					UserControlDesigner.DummyRootDesigner.DummyWebFormsReferenceManager.GetAssemblySpec(new AssemblyName("System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")),
					UserControlDesigner.DummyRootDesigner.DummyWebFormsReferenceManager.GetAssemblySpec(new AssemblyName("System.Web.DynamicData, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")),
					UserControlDesigner.DummyRootDesigner.DummyWebFormsReferenceManager.GetAssemblySpec(new AssemblyName("System.Web.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"))
				};
			}
		}

		// Token: 0x020003BD RID: 957
		private sealed class DummySite : ISite, IServiceProvider
		{
			// Token: 0x06002682 RID: 9858 RVA: 0x000ED31F File Offset: 0x000EB51F
			public DummySite(IComponent component, UserControlDesigner.UserControlDesignerHost designerHost)
			{
				this._component = component;
				this._container = designerHost;
				this._designerHost = designerHost;
			}

			// Token: 0x17000821 RID: 2081
			// (get) Token: 0x06002683 RID: 9859 RVA: 0x000ED33C File Offset: 0x000EB53C
			IComponent ISite.Component
			{
				get
				{
					return this._component;
				}
			}

			// Token: 0x17000822 RID: 2082
			// (get) Token: 0x06002684 RID: 9860 RVA: 0x000ED344 File Offset: 0x000EB544
			IContainer ISite.Container
			{
				get
				{
					return this._container;
				}
			}

			// Token: 0x17000823 RID: 2083
			// (get) Token: 0x06002685 RID: 9861 RVA: 0x00003B0F File Offset: 0x00001D0F
			bool ISite.DesignMode
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000824 RID: 2084
			// (get) Token: 0x06002686 RID: 9862 RVA: 0x000ED34C File Offset: 0x000EB54C
			// (set) Token: 0x06002687 RID: 9863 RVA: 0x000ED354 File Offset: 0x000EB554
			string ISite.Name
			{
				get
				{
					return this._name;
				}
				set
				{
					this._name = value;
				}
			}

			// Token: 0x06002688 RID: 9864 RVA: 0x000ED35D File Offset: 0x000EB55D
			object IServiceProvider.GetService(Type type)
			{
				return this._designerHost.GetService(type);
			}

			// Token: 0x04001BCB RID: 7115
			private IComponent _component;

			// Token: 0x04001BCC RID: 7116
			private IDesignerHost _designerHost;

			// Token: 0x04001BCD RID: 7117
			private IContainer _container;

			// Token: 0x04001BCE RID: 7118
			private string _name;
		}
	}
}
