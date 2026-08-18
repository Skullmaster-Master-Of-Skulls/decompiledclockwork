using System;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200013F RID: 319
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class XmlDesignerHierarchicalDataSourceView : DesignerHierarchicalDataSourceView
	{
		// Token: 0x06000B91 RID: 2961 RVA: 0x0004A6DB File Offset: 0x000488DB
		public XmlDesignerHierarchicalDataSourceView(XmlDataSourceDesigner owner, string viewPath) : base(owner, viewPath)
		{
			this._owner = owner;
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0004A6EC File Offset: 0x000488EC
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

		// Token: 0x06000B93 RID: 2963 RVA: 0x0004A724 File Offset: 0x00048924
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

		// Token: 0x040006FF RID: 1791
		private XmlDataSourceDesigner _owner;
	}
}
