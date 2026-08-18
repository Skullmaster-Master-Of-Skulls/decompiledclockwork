using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020002C5 RID: 709
	[Designer("Microsoft.VisualStudio.Web.WebForms.MasterPageWebFormDesigner, Microsoft.VisualStudio.Web, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[ControlBuilder(typeof(MasterPageControlBuilder))]
	[ParseChildren(false)]
	public class MasterPage : UserControl
	{
		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06001FFE RID: 8190 RVA: 0x00065AE8 File Offset: 0x00063CE8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal IDictionary ContentTemplates
		{
			get
			{
				return this._contentTemplates;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06001FFF RID: 8191 RVA: 0x00065AF0 File Offset: 0x00063CF0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal IList ContentPlaceHolders
		{
			get
			{
				if (this._contentPlaceHolders == null)
				{
					this._contentPlaceHolders = new ArrayList();
				}
				return this._contentPlaceHolders;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002000 RID: 8192 RVA: 0x00065B0B File Offset: 0x00063D0B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("MasterPage_MasterPage")]
		public MasterPage Master
		{
			get
			{
				if (this._master == null && !this._masterPageApplied)
				{
					this._master = MasterPage.CreateMaster(this, this.Context, this._masterPageFile, this._contentTemplateCollection);
				}
				return this._master;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x00065B41 File Offset: 0x00063D41
		// (set) Token: 0x06002002 RID: 8194 RVA: 0x00065B50 File Offset: 0x00063D50
		[DefaultValue("")]
		[WebCategory("Behavior")]
		[WebSysDescription("MasterPage_MasterPageFile")]
		public string MasterPageFile
		{
			get
			{
				return VirtualPath.GetVirtualPathString(this._masterPageFile);
			}
			set
			{
				if (this._masterPageApplied)
				{
					throw new InvalidOperationException(SR.GetString("PropertySetBeforePageEvent", new object[]
					{
						"MasterPageFile",
						"Page_PreInit"
					}));
				}
				if (value != VirtualPath.GetVirtualPathString(this._masterPageFile))
				{
					this._masterPageFile = VirtualPath.CreateAllowNull(value);
					if (this._master != null && this.Controls.Contains(this._master))
					{
						this.Controls.Remove(this._master);
					}
					this._master = null;
				}
			}
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x00065BE0 File Offset: 0x00063DE0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal void AddContentTemplate(string templateName, ITemplate template)
		{
			if (this._contentTemplateCollection == null)
			{
				this._contentTemplateCollection = new Hashtable(10, StringComparer.OrdinalIgnoreCase);
			}
			try
			{
				this._contentTemplateCollection.Add(templateName, template);
			}
			catch (ArgumentException)
			{
				throw new HttpException(SR.GetString("MasterPage_Multiple_content", new object[]
				{
					templateName
				}));
			}
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x00065C44 File Offset: 0x00063E44
		internal static MasterPage CreateMaster(TemplateControl owner, HttpContext context, VirtualPath masterPageFile, IDictionary contentTemplateCollection)
		{
			MasterPage masterPage = null;
			if (masterPageFile == null)
			{
				if (contentTemplateCollection != null && contentTemplateCollection.Count > 0)
				{
					throw new HttpException(SR.GetString("Content_only_allowed_in_content_page"));
				}
				return null;
			}
			else
			{
				VirtualPath virtualPath = VirtualPathProvider.CombineVirtualPathsInternal(owner.TemplateControlVirtualPath, masterPageFile);
				ITypedWebObjectFactory typedWebObjectFactory = (ITypedWebObjectFactory)BuildManager.GetVPathBuildResult(context, virtualPath);
				if (!typeof(MasterPage).IsAssignableFrom(typedWebObjectFactory.InstantiatedType))
				{
					throw new HttpException(SR.GetString("Invalid_master_base", new object[]
					{
						masterPageFile
					}));
				}
				masterPage = (MasterPage)typedWebObjectFactory.CreateInstance();
				masterPage.TemplateControlVirtualPath = virtualPath;
				if (owner.HasControls())
				{
					foreach (object obj in owner.Controls)
					{
						Control control = (Control)obj;
						LiteralControl literalControl = control as LiteralControl;
						if (literalControl == null || Util.FirstNonWhiteSpaceIndex(literalControl.Text) >= 0)
						{
							throw new HttpException(SR.GetString("Content_allowed_in_top_level_only"));
						}
					}
					owner.Controls.Clear();
				}
				if (owner.Controls.IsReadOnly)
				{
					throw new HttpException(SR.GetString("MasterPage_Cannot_ApplyTo_ReadOnly_Collection"));
				}
				if (contentTemplateCollection != null)
				{
					foreach (object obj2 in contentTemplateCollection.Keys)
					{
						string text = (string)obj2;
						if (!masterPage.ContentPlaceHolders.Contains(text.ToLower(CultureInfo.InvariantCulture)))
						{
							throw new HttpException(SR.GetString("MasterPage_doesnt_have_contentplaceholder", new object[]
							{
								text,
								masterPageFile
							}));
						}
					}
					masterPage._contentTemplates = contentTemplateCollection;
				}
				masterPage._ownerControl = owner;
				masterPage.InitializeAsUserControl(owner.Page);
				owner.Controls.Add(masterPage);
				return masterPage;
			}
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x00065E28 File Offset: 0x00064028
		internal static void ApplyMasterRecursive(MasterPage master, IList appliedMasterFilePaths)
		{
			if (master.Master != null)
			{
				string value = master._masterPageFile.VirtualPathString.ToLower(CultureInfo.InvariantCulture);
				if (appliedMasterFilePaths.Contains(value))
				{
					throw new InvalidOperationException(SR.GetString("MasterPage_Circular_Master_Not_Allowed", new object[]
					{
						master._masterPageFile
					}));
				}
				appliedMasterFilePaths.Add(value);
				MasterPage.ApplyMasterRecursive(master.Master, appliedMasterFilePaths);
			}
			master._masterPageApplied = true;
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x00065E98 File Offset: 0x00064098
		public void InstantiateInContentPlaceHolder(Control contentPlaceHolder, ITemplate template)
		{
			HttpContext httpContext = HttpContext.Current;
			TemplateControl templateControl = httpContext.TemplateControl;
			httpContext.TemplateControl = this._ownerControl;
			try
			{
				template.InstantiateIn(contentPlaceHolder);
			}
			finally
			{
				httpContext.TemplateControl = templateControl;
			}
		}

		// Token: 0x04001AC3 RID: 6851
		private VirtualPath _masterPageFile;

		// Token: 0x04001AC4 RID: 6852
		private MasterPage _master;

		// Token: 0x04001AC5 RID: 6853
		private IDictionary _contentTemplates;

		// Token: 0x04001AC6 RID: 6854
		private IDictionary _contentTemplateCollection;

		// Token: 0x04001AC7 RID: 6855
		private IList _contentPlaceHolders;

		// Token: 0x04001AC8 RID: 6856
		private bool _masterPageApplied;

		// Token: 0x04001AC9 RID: 6857
		internal TemplateControl _ownerControl;
	}
}
