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
	// Token: 0x0200013C RID: 316
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class XmlDataSourceDesigner : HierarchicalDataSourceDesigner, IDataSourceDesigner
	{
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool CanConfigure
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool CanRefreshSchema
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0004A112 File Offset: 0x00048312
		// (set) Token: 0x06000B67 RID: 2919 RVA: 0x0004A11F File Offset: 0x0004831F
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

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0004A156 File Offset: 0x00048356
		// (set) Token: 0x06000B69 RID: 2921 RVA: 0x0004A163 File Offset: 0x00048363
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

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0004A1A1 File Offset: 0x000483A1
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x0004A1AE File Offset: 0x000483AE
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

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0004A1E5 File Offset: 0x000483E5
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x0004A1F2 File Offset: 0x000483F2
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

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0004A230 File Offset: 0x00048430
		private XmlDataSource XmlDataSource
		{
			get
			{
				return this._xmlDataSource;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x0004A238 File Offset: 0x00048438
		// (set) Token: 0x06000B70 RID: 2928 RVA: 0x0004A245 File Offset: 0x00048445
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

		// Token: 0x06000B71 RID: 2929 RVA: 0x0004A27C File Offset: 0x0004847C
		public override void Configure()
		{
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ConfigureDataSourceChangeCallback), null, SR.GetString("DataSource_ConfigureTransactionDescription"));
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0004A2A0 File Offset: 0x000484A0
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

		// Token: 0x06000B73 RID: 2931 RVA: 0x0004A2F4 File Offset: 0x000484F4
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

		// Token: 0x06000B74 RID: 2932 RVA: 0x0004A40C File Offset: 0x0004860C
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

		// Token: 0x06000B75 RID: 2933 RVA: 0x0004A440 File Offset: 0x00048640
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

		// Token: 0x06000B76 RID: 2934 RVA: 0x0004A491 File Offset: 0x00048691
		public override DesignerHierarchicalDataSourceView GetView(string viewPath)
		{
			return new XmlDesignerHierarchicalDataSourceView(this, viewPath);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0004A49A File Offset: 0x0004869A
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(XmlDataSource));
			base.Initialize(component);
			this._xmlDataSource = (XmlDataSource)component;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0004A4C0 File Offset: 0x000486C0
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			foreach (string key in XmlDataSourceDesigner._shadowProperties)
			{
				PropertyDescriptor oldPropertyDescriptor = (PropertyDescriptor)properties[key];
				properties[key] = TypeDescriptor.CreateProperty(base.GetType(), oldPropertyDescriptor, new Attribute[0]);
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0004A514 File Offset: 0x00048714
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

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00034C2C File Offset: 0x00032E2C
		bool IDataSourceDesigner.CanConfigure
		{
			get
			{
				return this.CanConfigure;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00034C34 File Offset: 0x00032E34
		bool IDataSourceDesigner.CanRefreshSchema
		{
			get
			{
				return this.CanRefreshSchema;
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000B7C RID: 2940 RVA: 0x00034C3C File Offset: 0x00032E3C
		// (remove) Token: 0x06000B7D RID: 2941 RVA: 0x00034C45 File Offset: 0x00032E45
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

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000B7E RID: 2942 RVA: 0x00034C4E File Offset: 0x00032E4E
		// (remove) Token: 0x06000B7F RID: 2943 RVA: 0x00034C57 File Offset: 0x00032E57
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

		// Token: 0x06000B80 RID: 2944 RVA: 0x00034C60 File Offset: 0x00032E60
		void IDataSourceDesigner.Configure()
		{
			this.Configure();
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0004A558 File Offset: 0x00048758
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

		// Token: 0x06000B82 RID: 2946 RVA: 0x0000C5BB File Offset: 0x0000A7BB
		string[] IDataSourceDesigner.GetViewNames()
		{
			return new string[0];
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00034C79 File Offset: 0x00032E79
		void IDataSourceDesigner.RefreshSchema(bool preferSilent)
		{
			this.RefreshSchema(preferSilent);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00034C82 File Offset: 0x00032E82
		void IDataSourceDesigner.ResumeDataSourceEvents()
		{
			this.ResumeDataSourceEvents();
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00034C8A File Offset: 0x00032E8A
		void IDataSourceDesigner.SuppressDataSourceEvents()
		{
			this.SuppressDataSourceEvents();
		}

		// Token: 0x040006F8 RID: 1784
		private string _mappedDataFile;

		// Token: 0x040006F9 RID: 1785
		private string _mappedTransformFile;

		// Token: 0x040006FA RID: 1786
		private XmlDataSource _xmlDataSource;

		// Token: 0x040006FB RID: 1787
		private XmlDesignerDataSourceView _view;

		// Token: 0x040006FC RID: 1788
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
