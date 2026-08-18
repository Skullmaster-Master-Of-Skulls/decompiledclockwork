using System;
using System.Collections;
using System.ComponentModel;
using System.Design;
using System.IO;
using System.Security.Permissions;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000506 RID: 1286
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class XmlDataSourceDesigner : HierarchicalDataSourceDesigner, IDataSourceDesigner
	{
		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06002DD7 RID: 11735 RVA: 0x00104925 File Offset: 0x00103925
		public override bool CanConfigure
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06002DD8 RID: 11736 RVA: 0x00104928 File Offset: 0x00103928
		public override bool CanRefreshSchema
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06002DD9 RID: 11737 RVA: 0x0010492B File Offset: 0x0010392B
		// (set) Token: 0x06002DDA RID: 11738 RVA: 0x00104938 File Offset: 0x00103938
		public string Data
		{
			get
			{
				return this.XmlDataSource.Data;
			}
			set
			{
				if (value != this.XmlDataSource.Data)
				{
					this.XmlDataSource.Data = value;
					this.OnDataSourceChanged(EventArgs.Empty);
					this.OnSchemaRefreshed(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002DDB RID: 11739 RVA: 0x0010496F File Offset: 0x0010396F
		// (set) Token: 0x06002DDC RID: 11740 RVA: 0x0010497C File Offset: 0x0010397C
		public string DataFile
		{
			get
			{
				return this.XmlDataSource.DataFile;
			}
			set
			{
				if (value != this.XmlDataSource.DataFile)
				{
					this._mappedDataFile = null;
					this.XmlDataSource.DataFile = value;
					this.OnDataSourceChanged(EventArgs.Empty);
					this.OnSchemaRefreshed(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002DDD RID: 11741 RVA: 0x001049BA File Offset: 0x001039BA
		// (set) Token: 0x06002DDE RID: 11742 RVA: 0x001049C7 File Offset: 0x001039C7
		public string Transform
		{
			get
			{
				return this.XmlDataSource.Transform;
			}
			set
			{
				if (value != this.XmlDataSource.Transform)
				{
					this.XmlDataSource.Transform = value;
					this.OnDataSourceChanged(EventArgs.Empty);
					this.OnSchemaRefreshed(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002DDF RID: 11743 RVA: 0x001049FE File Offset: 0x001039FE
		// (set) Token: 0x06002DE0 RID: 11744 RVA: 0x00104A0B File Offset: 0x00103A0B
		public string TransformFile
		{
			get
			{
				return this.XmlDataSource.TransformFile;
			}
			set
			{
				if (value != this.XmlDataSource.TransformFile)
				{
					this._mappedTransformFile = null;
					this.XmlDataSource.TransformFile = value;
					this.OnDataSourceChanged(EventArgs.Empty);
					this.OnSchemaRefreshed(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002DE1 RID: 11745 RVA: 0x00104A49 File Offset: 0x00103A49
		private XmlDataSource XmlDataSource
		{
			get
			{
				return this._xmlDataSource;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002DE2 RID: 11746 RVA: 0x00104A51 File Offset: 0x00103A51
		// (set) Token: 0x06002DE3 RID: 11747 RVA: 0x00104A5E File Offset: 0x00103A5E
		public string XPath
		{
			get
			{
				return this.XmlDataSource.XPath;
			}
			set
			{
				if (value != this.XmlDataSource.XPath)
				{
					this.XmlDataSource.XPath = value;
					this.OnDataSourceChanged(EventArgs.Empty);
					this.OnSchemaRefreshed(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x00104A95 File Offset: 0x00103A95
		public override void Configure()
		{
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ConfigureDataSourceChangeCallback), null, SR.GetString("DataSource_ConfigureTransactionDescription"));
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x00104ABC File Offset: 0x00103ABC
		private bool ConfigureDataSourceChangeCallback(object context)
		{
			bool result;
			try
			{
				this.SuppressDataSourceEvents();
				IServiceProvider site = base.Component.Site;
				XmlDataSourceConfigureDataSourceForm form = new XmlDataSourceConfigureDataSourceForm(site, this.XmlDataSource);
				DialogResult dialogResult = UIServiceHelper.ShowDialog(site, form);
				result = (dialogResult == DialogResult.OK);
			}
			finally
			{
				this.ResumeDataSourceEvents();
			}
			return result;
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x00104B10 File Offset: 0x00103B10
		internal XmlDataSource GetDesignTimeXmlDataSource(string viewPath)
		{
			XmlDataSource xmlDataSource = new XmlDataSource();
			xmlDataSource.EnableCaching = false;
			xmlDataSource.Data = this.XmlDataSource.Data;
			xmlDataSource.Transform = this.XmlDataSource.Transform;
			xmlDataSource.XPath = (string.IsNullOrEmpty(viewPath) ? this.XmlDataSource.XPath : viewPath);
			if (this.XmlDataSource.DataFile.Length > 0)
			{
				if (this._mappedDataFile == null)
				{
					this._mappedDataFile = UrlPath.MapPath(base.Component.Site, this.XmlDataSource.DataFile);
				}
				xmlDataSource.DataFile = this._mappedDataFile;
				if (!File.Exists(xmlDataSource.DataFile))
				{
					return null;
				}
			}
			else if (xmlDataSource.Data.Length == 0)
			{
				return null;
			}
			if (this.XmlDataSource.TransformFile.Length > 0)
			{
				if (this._mappedTransformFile == null)
				{
					this._mappedTransformFile = UrlPath.MapPath(base.Component.Site, this.XmlDataSource.TransformFile);
				}
				xmlDataSource.TransformFile = this._mappedTransformFile;
				if (!File.Exists(xmlDataSource.TransformFile))
				{
					return null;
				}
			}
			return xmlDataSource;
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x00104C28 File Offset: 0x00103C28
		internal IHierarchicalEnumerable GetHierarchicalRuntimeEnumerable(string path)
		{
			XmlDataSource designTimeXmlDataSource = this.GetDesignTimeXmlDataSource(string.Empty);
			if (designTimeXmlDataSource == null)
			{
				return null;
			}
			HierarchicalDataSourceView hierarchicalView = ((IHierarchicalDataSource)designTimeXmlDataSource).GetHierarchicalView(path);
			if (hierarchicalView == null)
			{
				return null;
			}
			return hierarchicalView.Select();
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x00104C5C File Offset: 0x00103C5C
		internal IEnumerable GetRuntimeEnumerable(string listName)
		{
			XmlDataSource designTimeXmlDataSource = this.GetDesignTimeXmlDataSource(string.Empty);
			if (designTimeXmlDataSource == null)
			{
				return null;
			}
			XmlDataSourceView xmlDataSourceView = (XmlDataSourceView)((IDataSource)designTimeXmlDataSource).GetView(listName);
			if (xmlDataSourceView == null)
			{
				return null;
			}
			IEnumerable enumerable = xmlDataSourceView.Select(DataSourceSelectArguments.Empty);
			ICollection collection = enumerable as ICollection;
			if (collection != null && collection.Count == 0)
			{
				return null;
			}
			return enumerable;
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x00104CAD File Offset: 0x00103CAD
		public override DesignerHierarchicalDataSourceView GetView(string viewPath)
		{
			return new XmlDesignerHierarchicalDataSourceView(this, viewPath);
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x00104CB6 File Offset: 0x00103CB6
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(XmlDataSource));
			base.Initialize(component);
			this._xmlDataSource = (XmlDataSource)component;
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x00104CDC File Offset: 0x00103CDC
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			foreach (string key in XmlDataSourceDesigner._shadowProperties)
			{
				PropertyDescriptor oldPropertyDescriptor = (PropertyDescriptor)properties[key];
				properties[key] = TypeDescriptor.CreateProperty(base.GetType(), oldPropertyDescriptor, new Attribute[0]);
			}
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x00104D30 File Offset: 0x00103D30
		public override void RefreshSchema(bool preferSilent)
		{
			try
			{
				this.SuppressDataSourceEvents();
				this.OnDataSourceChanged(EventArgs.Empty);
				this.OnSchemaRefreshed(EventArgs.Empty);
			}
			finally
			{
				this.ResumeDataSourceEvents();
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06002DED RID: 11757 RVA: 0x00104D74 File Offset: 0x00103D74
		bool IDataSourceDesigner.CanConfigure
		{
			get
			{
				return this.CanConfigure;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06002DEE RID: 11758 RVA: 0x00104D7C File Offset: 0x00103D7C
		bool IDataSourceDesigner.CanRefreshSchema
		{
			get
			{
				return this.CanRefreshSchema;
			}
		}

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06002DEF RID: 11759 RVA: 0x00104D84 File Offset: 0x00103D84
		// (remove) Token: 0x06002DF0 RID: 11760 RVA: 0x00104D8D File Offset: 0x00103D8D
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

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06002DF1 RID: 11761 RVA: 0x00104D96 File Offset: 0x00103D96
		// (remove) Token: 0x06002DF2 RID: 11762 RVA: 0x00104D9F File Offset: 0x00103D9F
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

		// Token: 0x06002DF3 RID: 11763 RVA: 0x00104DA8 File Offset: 0x00103DA8
		void IDataSourceDesigner.Configure()
		{
			this.Configure();
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x00104DB0 File Offset: 0x00103DB0
		DesignerDataSourceView IDataSourceDesigner.GetView(string viewName)
		{
			if (!string.IsNullOrEmpty(viewName))
			{
				return null;
			}
			if (this._view == null)
			{
				this._view = new XmlDesignerDataSourceView(this, string.Empty);
			}
			return this._view;
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x00104DDB File Offset: 0x00103DDB
		string[] IDataSourceDesigner.GetViewNames()
		{
			return new string[0];
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x00104DE3 File Offset: 0x00103DE3
		void IDataSourceDesigner.RefreshSchema(bool preferSilent)
		{
			this.RefreshSchema(preferSilent);
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x00104DEC File Offset: 0x00103DEC
		void IDataSourceDesigner.ResumeDataSourceEvents()
		{
			this.ResumeDataSourceEvents();
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x00104DF4 File Offset: 0x00103DF4
		void IDataSourceDesigner.SuppressDataSourceEvents()
		{
			this.SuppressDataSourceEvents();
		}

		// Token: 0x04001F4A RID: 8010
		private string _mappedDataFile;

		// Token: 0x04001F4B RID: 8011
		private string _mappedTransformFile;

		// Token: 0x04001F4C RID: 8012
		private XmlDataSource _xmlDataSource;

		// Token: 0x04001F4D RID: 8013
		private XmlDesignerDataSourceView _view;

		// Token: 0x04001F4E RID: 8014
		private static readonly string[] _shadowProperties = new string[]
		{
			"Data",
			"DataFile",
			"Transform",
			"TransformFile",
			"XPath"
		};
	}
}
