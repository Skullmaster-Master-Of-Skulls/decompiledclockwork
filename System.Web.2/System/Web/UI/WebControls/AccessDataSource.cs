using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Web.Caching;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200036F RID: 879
	[Designer("System.Web.UI.Design.WebControls.AccessDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxBitmap(typeof(AccessDataSource))]
	[WebSysDescription("AccessDataSource_Description")]
	[WebSysDisplayName("AccessDataSource_DisplayName")]
	public class AccessDataSource : SqlDataSource
	{
		// Token: 0x0600287A RID: 10362 RVA: 0x00082E27 File Offset: 0x00081027
		public AccessDataSource()
		{
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x00082E2F File Offset: 0x0008102F
		public AccessDataSource(string dataFile, string selectCommand)
		{
			if (string.IsNullOrEmpty(dataFile))
			{
				throw new ArgumentNullException("dataFile");
			}
			this.DataFile = dataFile;
			base.SelectCommand = selectCommand;
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x0600287C RID: 10364 RVA: 0x00082E58 File Offset: 0x00081058
		internal override DataSourceCache Cache
		{
			get
			{
				if (this._cache == null)
				{
					this._cache = new FileDataSourceCache();
				}
				return this._cache;
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x0600287D RID: 10365 RVA: 0x00082E73 File Offset: 0x00081073
		// (set) Token: 0x0600287E RID: 10366 RVA: 0x00082E8F File Offset: 0x0008108F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string ConnectionString
		{
			get
			{
				if (this._connectionString == null)
				{
					this._connectionString = this.CreateConnectionString();
				}
				return this._connectionString;
			}
			set
			{
				throw new InvalidOperationException(SR.GetString("AccessDataSource_CannotSetConnectionString"));
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x0600287F RID: 10367 RVA: 0x00082EA0 File Offset: 0x000810A0
		// (set) Token: 0x06002880 RID: 10368 RVA: 0x00082EB6 File Offset: 0x000810B6
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.MdbDataFileEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("Data")]
		[WebSysDescription("AccessDataSource_DataFile")]
		public string DataFile
		{
			get
			{
				if (this._dataFile != null)
				{
					return this._dataFile;
				}
				return string.Empty;
			}
			set
			{
				if (this.DataFile != value)
				{
					this._dataFile = value;
					this._connectionString = null;
					this._physicalDataFile = null;
					this.RaiseDataSourceChangedEvent(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06002881 RID: 10369 RVA: 0x00082EE8 File Offset: 0x000810E8
		private FileDataSourceCache FileDataSourceCache
		{
			get
			{
				return this.Cache as FileDataSourceCache;
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06002882 RID: 10370 RVA: 0x00082F02 File Offset: 0x00081102
		private string PhysicalDataFile
		{
			get
			{
				if (this._physicalDataFile == null)
				{
					this._physicalDataFile = this.GetPhysicalDataFilePath();
				}
				return this._physicalDataFile;
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06002883 RID: 10371 RVA: 0x00082F1E File Offset: 0x0008111E
		internal string NativeProvider
		{
			get
			{
				if (this.IsAccess2007)
				{
					return "Microsoft.ACE.OLEDB.12.0";
				}
				return "Microsoft.Jet.OLEDB.4.0";
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06002884 RID: 10372 RVA: 0x00082F33 File Offset: 0x00081133
		internal virtual bool IsAccess2007
		{
			get
			{
				return Path.GetExtension(this.PhysicalDataFile) == ".accdb";
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06002885 RID: 10373 RVA: 0x00082F4A File Offset: 0x0008114A
		// (set) Token: 0x06002886 RID: 10374 RVA: 0x00082F51 File Offset: 0x00081151
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string ProviderName
		{
			get
			{
				return "System.Data.OleDb";
			}
			set
			{
				throw new InvalidOperationException(SR.GetString("AccessDataSource_CannotSetProvider", new object[]
				{
					this.ID
				}));
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06002887 RID: 10375 RVA: 0x00082F71 File Offset: 0x00081171
		// (set) Token: 0x06002888 RID: 10376 RVA: 0x00082F71 File Offset: 0x00081171
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string SqlCacheDependency
		{
			get
			{
				throw new NotSupportedException(SR.GetString("AccessDataSource_SqlCacheDependencyNotSupported", new object[]
				{
					this.ID
				}));
			}
			set
			{
				throw new NotSupportedException(SR.GetString("AccessDataSource_SqlCacheDependencyNotSupported", new object[]
				{
					this.ID
				}));
			}
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x00082F94 File Offset: 0x00081194
		private void AddCacheFileDependency()
		{
			this.FileDataSourceCache.FileDependencies.Clear();
			string physicalDataFile = this.PhysicalDataFile;
			if (physicalDataFile.Length > 0)
			{
				this.FileDataSourceCache.FileDependencies.Add(physicalDataFile);
			}
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x00082FD3 File Offset: 0x000811D3
		private string CreateConnectionString()
		{
			return "Provider=" + this.NativeProvider + "; Data Source=" + this.PhysicalDataFile;
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x00082FF0 File Offset: 0x000811F0
		protected override SqlDataSourceView CreateDataSourceView(string viewName)
		{
			return new AccessDataSourceView(this, viewName, this.Context);
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x00082FFF File Offset: 0x000811FF
		protected override DbProviderFactory GetDbProviderFactory()
		{
			return OleDbFactory.Instance;
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x00083008 File Offset: 0x00081208
		private string GetPhysicalDataFilePath()
		{
			string text = this.DataFile;
			if (text.Length == 0)
			{
				return null;
			}
			if (!UrlPath.IsAbsolutePhysicalPath(text))
			{
				if (base.DesignMode)
				{
					throw new NotSupportedException(SR.GetString("AccessDataSource_DesignTimeRelativePathsNotSupported", new object[]
					{
						this.ID
					}));
				}
				text = this.Context.Request.MapPath(text, base.AppRelativeTemplateSourceDirectory, true);
			}
			HttpRuntime.CheckFilePermission(text, true);
			if (!HttpRuntime.HasPathDiscoveryPermission(text))
			{
				throw new HttpException(SR.GetString("AccessDataSource_NoPathDiscoveryPermission", new object[]
				{
					HttpRuntime.GetSafePath(text),
					this.ID
				}));
			}
			return text;
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x000830A6 File Offset: 0x000812A6
		internal override void SaveDataToCache(int startRowIndex, int maximumRows, object data, CacheDependency dependency)
		{
			this.AddCacheFileDependency();
			base.SaveDataToCache(startRowIndex, maximumRows, data, dependency);
		}

		// Token: 0x04001DF9 RID: 7673
		private const string OleDbProviderName = "System.Data.OleDb";

		// Token: 0x04001DFA RID: 7674
		private const string JetProvider = "Microsoft.Jet.OLEDB.4.0";

		// Token: 0x04001DFB RID: 7675
		private const string Access2007Provider = "Microsoft.ACE.OLEDB.12.0";

		// Token: 0x04001DFC RID: 7676
		private const string Access2007FileExtension = ".accdb";

		// Token: 0x04001DFD RID: 7677
		private FileDataSourceCache _cache;

		// Token: 0x04001DFE RID: 7678
		private string _connectionString;

		// Token: 0x04001DFF RID: 7679
		private string _dataFile;

		// Token: 0x04001E00 RID: 7680
		private string _physicalDataFile;
	}
}
