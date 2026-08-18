using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.DropDownTree
{
	// Token: 0x02000B29 RID: 2857
	internal interface IEmbeddedTreeAdapter<T> where T : WebControl
	{
		// Token: 0x06006B1A RID: 27418
		IList<DropDownTreeNode> GetAllNodes();

		// Token: 0x17002313 RID: 8979
		// (get) Token: 0x06006B1B RID: 27419
		string ClientID { get; }

		// Token: 0x17002314 RID: 8980
		// (get) Token: 0x06006B1C RID: 27420
		// (set) Token: 0x06006B1D RID: 27421
		string DataFieldID { get; set; }

		// Token: 0x17002315 RID: 8981
		// (get) Token: 0x06006B1E RID: 27422
		// (set) Token: 0x06006B1F RID: 27423
		string DataFieldParentID { get; set; }

		// Token: 0x17002316 RID: 8982
		// (get) Token: 0x06006B20 RID: 27424
		// (set) Token: 0x06006B21 RID: 27425
		string DataTextField { get; set; }

		// Token: 0x17002317 RID: 8983
		// (get) Token: 0x06006B22 RID: 27426
		// (set) Token: 0x06006B23 RID: 27427
		string DataValueField { get; set; }

		// Token: 0x17002318 RID: 8984
		// (get) Token: 0x06006B24 RID: 27428
		// (set) Token: 0x06006B25 RID: 27429
		object DataSource { get; set; }

		// Token: 0x17002319 RID: 8985
		// (get) Token: 0x06006B26 RID: 27430
		// (set) Token: 0x06006B27 RID: 27431
		string DataSourceID { get; set; }

		// Token: 0x1700231A RID: 8986
		// (get) Token: 0x06006B28 RID: 27432
		// (set) Token: 0x06006B29 RID: 27433
		string ODataDataSourceID { get; set; }

		// Token: 0x1700231B RID: 8987
		// (get) Token: 0x06006B2A RID: 27434
		// (set) Token: 0x06006B2B RID: 27435
		ITemplate NodeTemplate { get; set; }

		// Token: 0x1700231C RID: 8988
		// (set) Token: 0x06006B2C RID: 27436
		DropDownTreeCheckBoxes CheckBoxes { set; }

		// Token: 0x140000F6 RID: 246
		// (add) Token: 0x06006B2D RID: 27437
		// (remove) Token: 0x06006B2E RID: 27438
		event DropDownTreeNodeDataBoundEventHandler DropDownTreeNodeDataBound;

		// Token: 0x06006B2F RID: 27439
		void DataBind();

		// Token: 0x06006B30 RID: 27440
		DropDownTreeNode FindNodeByHierarchicalIndex(string hierarchicalIndex);

		// Token: 0x06006B31 RID: 27441
		void RenderEmbeddedTree(HtmlTextWriter writer);

		// Token: 0x06006B32 RID: 27442
		T GetEmbeddedTree();

		// Token: 0x06006B33 RID: 27443
		void ExpandEmbeddedTree();

		// Token: 0x06006B34 RID: 27444
		void ClearNodesState();

		// Token: 0x06006B35 RID: 27445
		void CreateEntry(bool byValue, string value);

		// Token: 0x06006B36 RID: 27446
		void SyncWebServiceSettings(WebServiceSettings webServiceSettings);

		// Token: 0x06006B37 RID: 27447
		void SyncDataBindings(List<DropDownNodeBinding> dataBindings);
	}
}
