using System;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000509 RID: 1289
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class XmlDesignerHierarchicalDataSourceView : DesignerHierarchicalDataSourceView
	{
		// Token: 0x06002E04 RID: 11780 RVA: 0x00104F7B File Offset: 0x00103F7B
		public XmlDesignerHierarchicalDataSourceView(XmlDataSourceDesigner owner, string viewPath) : base(owner, viewPath)
		{
			this._owner = owner;
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06002E05 RID: 11781 RVA: 0x00104F8C File Offset: 0x00103F8C
		public override IDataSourceSchema Schema
		{
			get
			{
				XmlDataSource designTimeXmlDataSource = this._owner.GetDesignTimeXmlDataSource(base.Path);
				if (designTimeXmlDataSource == null)
				{
					return null;
				}
				return new XmlDocumentSchema(designTimeXmlDataSource.GetXmlDocument(), designTimeXmlDataSource.XPath, true);
			}
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x00104FC4 File Offset: 0x00103FC4
		public override IHierarchicalEnumerable GetDesignTimeData(out bool isSampleData)
		{
			IHierarchicalEnumerable hierarchicalRuntimeEnumerable = this._owner.GetHierarchicalRuntimeEnumerable(base.Path);
			if (hierarchicalRuntimeEnumerable != null)
			{
				isSampleData = false;
				return hierarchicalRuntimeEnumerable;
			}
			return base.GetDesignTimeData(out isSampleData);
		}

		// Token: 0x04001F51 RID: 8017
		private XmlDataSourceDesigner _owner;
	}
}
