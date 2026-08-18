using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000101 RID: 257
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class SiteMapDataSourceDesigner : HierarchicalDataSourceDesigner, IDataSourceDesigner
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool CanRefreshSchema
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00034B64 File Offset: 0x00032D64
		internal SiteMapProvider DesignTimeSiteMapProvider
		{
			get
			{
				if (this._siteMapProvider == null)
				{
					IDesignerHost host = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					this._siteMapProvider = new DesignTimeSiteMapProvider(host);
				}
				return this._siteMapProvider;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x00034BA1 File Offset: 0x00032DA1
		internal SiteMapDataSource SiteMapDataSource
		{
			get
			{
				return this._siteMapDataSource;
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00034BA9 File Offset: 0x00032DA9
		public override DesignerHierarchicalDataSourceView GetView(string viewPath)
		{
			return new SiteMapDesignerHierarchicalDataSourceView(this, viewPath);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0000C5BB File Offset: 0x0000A7BB
		public virtual string[] GetViewNames()
		{
			return new string[0];
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00034BB2 File Offset: 0x00032DB2
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(SiteMapDataSource));
			base.Initialize(component);
			this._siteMapDataSource = (SiteMapDataSource)component;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00034BD7 File Offset: 0x00032DD7
		public override void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			base.OnComponentChanged(sender, e);
			this.OnDataSourceChanged(EventArgs.Empty);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00034BEC File Offset: 0x00032DEC
		public override void RefreshSchema(bool preferSilent)
		{
			try
			{
				this.SuppressDataSourceEvents();
				this._siteMapProvider = null;
				this.OnDataSourceChanged(EventArgs.Empty);
			}
			finally
			{
				this.ResumeDataSourceEvents();
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00034C2C File Offset: 0x00032E2C
		bool IDataSourceDesigner.CanConfigure
		{
			get
			{
				return this.CanConfigure;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x00034C34 File Offset: 0x00032E34
		bool IDataSourceDesigner.CanRefreshSchema
		{
			get
			{
				return this.CanRefreshSchema;
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600091A RID: 2330 RVA: 0x00034C3C File Offset: 0x00032E3C
		// (remove) Token: 0x0600091B RID: 2331 RVA: 0x00034C45 File Offset: 0x00032E45
		event EventHandler IDataSourceDesigner.DataSourceChanged
		{
			add
			{
				base.DataSourceChanged += value;
			}
			remove
			{
				base.DataSourceChanged -= value;
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600091C RID: 2332 RVA: 0x00034C4E File Offset: 0x00032E4E
		// (remove) Token: 0x0600091D RID: 2333 RVA: 0x00034C57 File Offset: 0x00032E57
		event EventHandler IDataSourceDesigner.SchemaRefreshed
		{
			add
			{
				base.SchemaRefreshed += value;
			}
			remove
			{
				base.SchemaRefreshed -= value;
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00034C60 File Offset: 0x00032E60
		void IDataSourceDesigner.Configure()
		{
			this.Configure();
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00034C68 File Offset: 0x00032E68
		DesignerDataSourceView IDataSourceDesigner.GetView(string viewName)
		{
			return new SiteMapDesignerDataSourceView(this, viewName);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00034C71 File Offset: 0x00032E71
		string[] IDataSourceDesigner.GetViewNames()
		{
			return this.GetViewNames();
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00034C79 File Offset: 0x00032E79
		void IDataSourceDesigner.RefreshSchema(bool preferSilent)
		{
			this.RefreshSchema(preferSilent);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00034C82 File Offset: 0x00032E82
		void IDataSourceDesigner.ResumeDataSourceEvents()
		{
			this.ResumeDataSourceEvents();
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00034C8A File Offset: 0x00032E8A
		void IDataSourceDesigner.SuppressDataSourceEvents()
		{
			this.SuppressDataSourceEvents();
		}

		// Token: 0x04000555 RID: 1365
		internal static readonly SiteMapDataSourceDesigner.SiteMapSchema SiteMapHierarchicalSchema = new SiteMapDataSourceDesigner.SiteMapSchema();

		// Token: 0x04000556 RID: 1366
		private SiteMapDataSource _siteMapDataSource;

		// Token: 0x04000557 RID: 1367
		private SiteMapProvider _siteMapProvider;

		// Token: 0x04000558 RID: 1368
		private static readonly string _siteMapNodeType = typeof(SiteMapNode).Name;

		// Token: 0x0200042C RID: 1068
		internal class SiteMapSchema : IDataSourceSchema
		{
			// Token: 0x0600288A RID: 10378 RVA: 0x000F7A8C File Offset: 0x000F5C8C
			IDataSourceViewSchema[] IDataSourceSchema.GetViews()
			{
				return new SiteMapDataSourceDesigner.SiteMapDataSourceViewSchema[]
				{
					new SiteMapDataSourceDesigner.SiteMapDataSourceViewSchema()
				};
			}
		}

		// Token: 0x0200042D RID: 1069
		internal class SiteMapDataSourceViewSchema : IDataSourceViewSchema
		{
			// Token: 0x17000878 RID: 2168
			// (get) Token: 0x0600288C RID: 10380 RVA: 0x000F7AA9 File Offset: 0x000F5CA9
			string IDataSourceViewSchema.Name
			{
				get
				{
					return SiteMapDataSourceDesigner._siteMapNodeType;
				}
			}

			// Token: 0x0600288D RID: 10381 RVA: 0x00003598 File Offset: 0x00001798
			IDataSourceViewSchema[] IDataSourceViewSchema.GetChildren()
			{
				return null;
			}

			// Token: 0x0600288E RID: 10382 RVA: 0x000F7AB0 File Offset: 0x000F5CB0
			IDataSourceFieldSchema[] IDataSourceViewSchema.GetFields()
			{
				return new SiteMapDataSourceDesigner.SiteMapDataSourceTextField[]
				{
					SiteMapDataSourceDesigner.SiteMapDataSourceTextField.DescriptionField,
					SiteMapDataSourceDesigner.SiteMapDataSourceTextField.TitleField,
					SiteMapDataSourceDesigner.SiteMapDataSourceTextField.UrlField
				};
			}
		}

		// Token: 0x0200042E RID: 1070
		private class SiteMapDataSourceTextField : IDataSourceFieldSchema
		{
			// Token: 0x06002890 RID: 10384 RVA: 0x000F7ADD File Offset: 0x000F5CDD
			internal SiteMapDataSourceTextField(string fieldName)
			{
				this._fieldName = fieldName;
			}

			// Token: 0x17000879 RID: 2169
			// (get) Token: 0x06002891 RID: 10385 RVA: 0x00013B29 File Offset: 0x00011D29
			Type IDataSourceFieldSchema.DataType
			{
				get
				{
					return typeof(string);
				}
			}

			// Token: 0x1700087A RID: 2170
			// (get) Token: 0x06002892 RID: 10386 RVA: 0x0000445B File Offset: 0x0000265B
			bool IDataSourceFieldSchema.Identity
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700087B RID: 2171
			// (get) Token: 0x06002893 RID: 10387 RVA: 0x00003B0F File Offset: 0x00001D0F
			bool IDataSourceFieldSchema.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700087C RID: 2172
			// (get) Token: 0x06002894 RID: 10388 RVA: 0x0000445B File Offset: 0x0000265B
			bool IDataSourceFieldSchema.IsUnique
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700087D RID: 2173
			// (get) Token: 0x06002895 RID: 10389 RVA: 0x0000C1CD File Offset: 0x0000A3CD
			int IDataSourceFieldSchema.Length
			{
				get
				{
					return -1;
				}
			}

			// Token: 0x1700087E RID: 2174
			// (get) Token: 0x06002896 RID: 10390 RVA: 0x000F7AEC File Offset: 0x000F5CEC
			string IDataSourceFieldSchema.Name
			{
				get
				{
					return this._fieldName;
				}
			}

			// Token: 0x1700087F RID: 2175
			// (get) Token: 0x06002897 RID: 10391 RVA: 0x00003B0F File Offset: 0x00001D0F
			bool IDataSourceFieldSchema.Nullable
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000880 RID: 2176
			// (get) Token: 0x06002898 RID: 10392 RVA: 0x0000C1CD File Offset: 0x0000A3CD
			int IDataSourceFieldSchema.Precision
			{
				get
				{
					return -1;
				}
			}

			// Token: 0x17000881 RID: 2177
			// (get) Token: 0x06002899 RID: 10393 RVA: 0x0000445B File Offset: 0x0000265B
			bool IDataSourceFieldSchema.PrimaryKey
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000882 RID: 2178
			// (get) Token: 0x0600289A RID: 10394 RVA: 0x0000C1CD File Offset: 0x0000A3CD
			int IDataSourceFieldSchema.Scale
			{
				get
				{
					return -1;
				}
			}

			// Token: 0x04001CD8 RID: 7384
			internal static readonly SiteMapDataSourceDesigner.SiteMapDataSourceTextField DescriptionField = new SiteMapDataSourceDesigner.SiteMapDataSourceTextField("Description");

			// Token: 0x04001CD9 RID: 7385
			internal static readonly SiteMapDataSourceDesigner.SiteMapDataSourceTextField TitleField = new SiteMapDataSourceDesigner.SiteMapDataSourceTextField("Title");

			// Token: 0x04001CDA RID: 7386
			internal static readonly SiteMapDataSourceDesigner.SiteMapDataSourceTextField UrlField = new SiteMapDataSourceDesigner.SiteMapDataSourceTextField("Url");

			// Token: 0x04001CDB RID: 7387
			private string _fieldName;
		}
	}
}
