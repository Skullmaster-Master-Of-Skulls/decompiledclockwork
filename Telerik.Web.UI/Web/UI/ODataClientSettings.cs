using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000B07 RID: 2823
	internal class ODataClientSettings
	{
		// Token: 0x060069B1 RID: 27057 RVA: 0x0018D358 File Offset: 0x0018B558
		public static ODataClientSettings FromHierarhicalControl<T>(T control) where T : RadDataBoundControl
		{
			ODataClientSettings odataClientSettings = new ODataClientSettings();
			IFlatBoundContainer flatBoundContainer = control as IFlatBoundContainer;
			if (flatBoundContainer != null)
			{
				odataClientSettings.DataTextField = flatBoundContainer.DataTextField;
				odataClientSettings.DataValueField = flatBoundContainer.DataValueField;
			}
			HierarchicalControlItemContainer hierarchicalControlItemContainer = control as HierarchicalControlItemContainer;
			if (hierarchicalControlItemContainer != null)
			{
				odataClientSettings.DataTextField = hierarchicalControlItemContainer.DataTextField;
				odataClientSettings.DataValueField = hierarchicalControlItemContainer.DataValueField;
				odataClientSettings.DataFieldID = hierarchicalControlItemContainer.DataFieldID;
				odataClientSettings.DataFieldParentID = hierarchicalControlItemContainer.DataFieldParentID;
				odataClientSettings.DataNavigateUrlField = hierarchicalControlItemContainer.DataNavigateUrlField;
			}
			odataClientSettings.DataModelID = control.DataModelID;
			odataClientSettings.ODataSourceID = ODataClientSettings.FindODataDataSourceClientID(control, control.ODataDataSourceID);
			return odataClientSettings;
		}

		// Token: 0x060069B2 RID: 27058 RVA: 0x0018D410 File Offset: 0x0018B610
		public static ODataClientSettings FromRadGridControl(RadGrid control)
		{
			return new ODataClientSettings
			{
				DataModelID = control.DataModelID,
				ODataSourceID = ODataClientSettings.FindODataDataSourceClientID(control, control.ODataDataSourceID)
			};
		}

		// Token: 0x060069B3 RID: 27059 RVA: 0x0018D444 File Offset: 0x0018B644
		public static ODataClientSettings FromRadListViewControl(RadListView control)
		{
			return new ODataClientSettings
			{
				DataModelID = control.DataModelID,
				ODataSourceID = ODataClientSettings.FindODataDataSourceClientID(control, control.ODataDataSourceID)
			};
		}

		// Token: 0x060069B4 RID: 27060 RVA: 0x0018D478 File Offset: 0x0018B678
		public static ODataClientSettings FromRadLiveTileControl(RadLiveTile control)
		{
			return new ODataClientSettings
			{
				DataModelID = control.DataModelID,
				ODataSourceID = ODataClientSettings.FindODataDataSourceClientID(control, control.ODataDataSourceID)
			};
		}

		// Token: 0x060069B5 RID: 27061 RVA: 0x0018D4AC File Offset: 0x0018B6AC
		private static string FindODataDataSourceClientID(Control control, string controlID)
		{
			Control control2 = control;
			Control control3 = null;
			if (control == control.Page)
			{
				Control control4 = control.FindControl(controlID);
				if (control4 == null)
				{
					return controlID;
				}
				return control4.ClientID;
			}
			else
			{
				while (control3 == null && control2 != control.Page)
				{
					control2 = control2.NamingContainer;
					if (control2 == null)
					{
						return controlID;
					}
					control3 = control2.FindControl(controlID);
				}
				if (control3 == null)
				{
					return controlID;
				}
				return control3.ClientID;
			}
		}

		// Token: 0x170022A0 RID: 8864
		// (get) Token: 0x060069B7 RID: 27063 RVA: 0x0018D50D File Offset: 0x0018B70D
		// (set) Token: 0x060069B8 RID: 27064 RVA: 0x0018D515 File Offset: 0x0018B715
		public string DataModelID { get; set; }

		// Token: 0x170022A1 RID: 8865
		// (get) Token: 0x060069B9 RID: 27065 RVA: 0x0018D51E File Offset: 0x0018B71E
		// (set) Token: 0x060069BA RID: 27066 RVA: 0x0018D526 File Offset: 0x0018B726
		public string DataFieldID { get; set; }

		// Token: 0x170022A2 RID: 8866
		// (get) Token: 0x060069BB RID: 27067 RVA: 0x0018D52F File Offset: 0x0018B72F
		// (set) Token: 0x060069BC RID: 27068 RVA: 0x0018D537 File Offset: 0x0018B737
		public string DataFieldParentID { get; set; }

		// Token: 0x170022A3 RID: 8867
		// (get) Token: 0x060069BD RID: 27069 RVA: 0x0018D540 File Offset: 0x0018B740
		// (set) Token: 0x060069BE RID: 27070 RVA: 0x0018D548 File Offset: 0x0018B748
		public string DataNavigateUrlField { get; set; }

		// Token: 0x170022A4 RID: 8868
		// (get) Token: 0x060069BF RID: 27071 RVA: 0x0018D551 File Offset: 0x0018B751
		// (set) Token: 0x060069C0 RID: 27072 RVA: 0x0018D559 File Offset: 0x0018B759
		public string DataTextField { get; set; }

		// Token: 0x170022A5 RID: 8869
		// (get) Token: 0x060069C1 RID: 27073 RVA: 0x0018D562 File Offset: 0x0018B762
		// (set) Token: 0x060069C2 RID: 27074 RVA: 0x0018D56A File Offset: 0x0018B76A
		public string DataValueField { get; set; }

		// Token: 0x170022A6 RID: 8870
		// (get) Token: 0x060069C3 RID: 27075 RVA: 0x0018D573 File Offset: 0x0018B773
		// (set) Token: 0x060069C4 RID: 27076 RVA: 0x0018D57B File Offset: 0x0018B77B
		public string ODataSourceID { get; set; }
	}
}
