using System;
using System.Collections;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000508 RID: 1288
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class XmlDesignerDataSourceView : DesignerDataSourceView
	{
		// Token: 0x06002E01 RID: 11777 RVA: 0x00104EDA File Offset: 0x00103EDA
		public XmlDesignerDataSourceView(XmlDataSourceDesigner owner, string viewName) : base(owner, viewName)
		{
			this._owner = owner;
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06002E02 RID: 11778 RVA: 0x00104EEC File Offset: 0x00103EEC
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
					if (views != null && views.Length > 0)
					{
						return views[0];
					}
				}
				return null;
			}
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x00104F4C File Offset: 0x00103F4C
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

		// Token: 0x04001F50 RID: 8016
		private XmlDataSourceDesigner _owner;
	}
}
