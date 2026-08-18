using System;
using System.Collections;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200013E RID: 318
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class XmlDesignerDataSourceView : DesignerDataSourceView
	{
		// Token: 0x06000B8E RID: 2958 RVA: 0x0004A63E File Offset: 0x0004883E
		public XmlDesignerDataSourceView(XmlDataSourceDesigner owner, string viewName) : base(owner, viewName)
		{
			this._owner = owner;
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0004A650 File Offset: 0x00048850
		public override IDataSourceViewSchema Schema
		{
			get
			{
				XmlDataSource designTimeXmlDataSource = this._owner.GetDesignTimeXmlDataSource(string.Empty);
				if (designTimeXmlDataSource == null)
				{
					return null;
				}
				string text = designTimeXmlDataSource.XPath;
				if (text.Length == 0)
				{
					text = "/node()/node()";
				}
				IDataSourceSchema dataSourceSchema = new XmlDocumentSchema(designTimeXmlDataSource.GetXmlDocument(), text);
				if (dataSourceSchema != null)
				{
					IDataSourceViewSchema[] views = dataSourceSchema.GetViews();
					if (views != null && views.Length != 0)
					{
						return views[0];
					}
				}
				return null;
			}
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0004A6AC File Offset: 0x000488AC
		public override IEnumerable GetDesignTimeData(int minimumRows, out bool isSampleData)
		{
			IEnumerable runtimeEnumerable = this._owner.GetRuntimeEnumerable(base.Name);
			if (runtimeEnumerable != null)
			{
				isSampleData = false;
				return runtimeEnumerable;
			}
			return base.GetDesignTimeData(minimumRows, out isSampleData);
		}

		// Token: 0x040006FE RID: 1790
		private XmlDataSourceDesigner _owner;
	}
}
