using System;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x0200034E RID: 846
	public class WebServiceSettings : WebServiceSettings
	{
		// Token: 0x06001D05 RID: 7429 RVA: 0x0005C371 File Offset: 0x0005A571
		public WebServiceSettings(string prefix, StateBag viewState) : base(prefix, viewState)
		{
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x0005C37B File Offset: 0x0005A57B
		public WebServiceSettings(StateBag viewState) : this("WebServiceSettings", viewState)
		{
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x0005C389 File Offset: 0x0005A589
		// (set) Token: 0x06001D08 RID: 7432 RVA: 0x0005C391 File Offset: 0x0005A591
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string Method
		{
			get
			{
				return this.GetTasksMethod;
			}
			set
			{
				base.ViewState["GetTasksMethod"] = value;
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x0005C3A4 File Offset: 0x0005A5A4
		// (set) Token: 0x06001D0A RID: 7434 RVA: 0x0005C3C4 File Offset: 0x0005A5C4
		[Category("Behavior")]
		[Description("Specifies the web service method name to be used to populate the gantt tasks.")]
		[DefaultValue("GetTasks")]
		[ClientControlProperty]
		public string GetTasksMethod
		{
			get
			{
				return (string)(base.ViewState["GetTasksMethod"] ?? "GetTasks");
			}
			set
			{
				base.ViewState["GetTasksMethod"] = value;
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06001D0B RID: 7435 RVA: 0x0005C3D7 File Offset: 0x0005A5D7
		// (set) Token: 0x06001D0C RID: 7436 RVA: 0x0005C3F7 File Offset: 0x0005A5F7
		[Description("Specifies the web service method name to be used to delete gantt tasks.")]
		[DefaultValue("DeleteTasks")]
		[ClientControlProperty]
		[Category("Behavior")]
		public string DeleteTasksMethod
		{
			get
			{
				return (string)(base.ViewState["DeleteTasksMethod"] ?? "DeleteTasks");
			}
			set
			{
				base.ViewState["DeleteTasksMethod"] = value;
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06001D0D RID: 7437 RVA: 0x0005C40A File Offset: 0x0005A60A
		// (set) Token: 0x06001D0E RID: 7438 RVA: 0x0005C42A File Offset: 0x0005A62A
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Specifies the web service method name to be used to insert gantt task.")]
		[DefaultValue("InsertTasks")]
		public string InsertTasksMethod
		{
			get
			{
				return (string)(base.ViewState["InsertTasksMethod"] ?? "InsertTasks");
			}
			set
			{
				base.ViewState["InsertTasksMethod"] = value;
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06001D0F RID: 7439 RVA: 0x0005C43D File Offset: 0x0005A63D
		// (set) Token: 0x06001D10 RID: 7440 RVA: 0x0005C45D File Offset: 0x0005A65D
		[ClientControlProperty]
		[Description("Specifies the web service method name to be used to update gantt tasks.")]
		[DefaultValue("UpdateTasks")]
		[Category("Behavior")]
		public string UpdateTasksMethod
		{
			get
			{
				return (string)(base.ViewState["UpdateTasksMethod"] ?? "UpdateTasks");
			}
			set
			{
				base.ViewState["UpdateTasksMethod"] = value;
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06001D11 RID: 7441 RVA: 0x0005C470 File Offset: 0x0005A670
		// (set) Token: 0x06001D12 RID: 7442 RVA: 0x0005C490 File Offset: 0x0005A690
		[Description("Specifies the web service method name to be used to populate the gantt dependencies.")]
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue("GetDependencies")]
		public string GetDependenciesMethod
		{
			get
			{
				return (string)(base.ViewState["GetDependenciesMethod"] ?? "GetDependencies");
			}
			set
			{
				base.ViewState["GetDependenciesMethod"] = value;
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06001D13 RID: 7443 RVA: 0x0005C4A3 File Offset: 0x0005A6A3
		// (set) Token: 0x06001D14 RID: 7444 RVA: 0x0005C4C3 File Offset: 0x0005A6C3
		[DefaultValue("DeleteDependencies")]
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Specifies the web service method name to be used to delete gantt dependencies.")]
		public string DeleteDependenciesMethod
		{
			get
			{
				return (string)(base.ViewState["DeleteDependenciesMethod"] ?? "DeleteDependencies");
			}
			set
			{
				base.ViewState["DeleteDependenciesMethod"] = value;
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06001D15 RID: 7445 RVA: 0x0005C4D6 File Offset: 0x0005A6D6
		// (set) Token: 0x06001D16 RID: 7446 RVA: 0x0005C4F6 File Offset: 0x0005A6F6
		[ClientControlProperty]
		[Description("Specifies the web service method name to be used to insert gantt dependency.")]
		[DefaultValue("InsertDependencies")]
		[Category("Behavior")]
		public string InsertDependenciesMethod
		{
			get
			{
				return (string)(base.ViewState["InsertDependenciesMethod"] ?? "InsertDependencies");
			}
			set
			{
				base.ViewState["InsertDependenciesMethod"] = value;
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x0005C509 File Offset: 0x0005A709
		// (set) Token: 0x06001D18 RID: 7448 RVA: 0x0005C529 File Offset: 0x0005A729
		[Category("Behavior")]
		[DefaultValue("GetResources")]
		[ClientControlProperty]
		[Description("Specifies the web service method name to be used to populate the gantt resources.")]
		public string GetResourcesMethod
		{
			get
			{
				return (string)(base.ViewState["GetResourcesMethod"] ?? "GetResources");
			}
			set
			{
				base.ViewState["GetResourcesMethod"] = value;
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x0005C53C File Offset: 0x0005A73C
		// (set) Token: 0x06001D1A RID: 7450 RVA: 0x0005C55C File Offset: 0x0005A75C
		[Description("Specifies the web service method name to be used to populate the gantt resource assignments.")]
		[DefaultValue("GetAssignments")]
		[Category("Behavior")]
		[ClientControlProperty]
		public string GetAssignmentsMethod
		{
			get
			{
				return (string)(base.ViewState["GetAssignmentsMethod"] ?? "GetAssignments");
			}
			set
			{
				base.ViewState["GetAssignmentsMethod"] = value;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06001D1B RID: 7451 RVA: 0x0005C56F File Offset: 0x0005A76F
		// (set) Token: 0x06001D1C RID: 7452 RVA: 0x0005C58F File Offset: 0x0005A78F
		[DefaultValue("DeleteAssignments")]
		[Category("Behavior")]
		[Description("Specifies the web service method name to be used to delete gantt resource assignments.")]
		[ClientControlProperty]
		public string DeleteAssignmentsMethod
		{
			get
			{
				return (string)(base.ViewState["DeleteAssignmentsMethod"] ?? "DeleteAssignments");
			}
			set
			{
				base.ViewState["DeleteAssignmentsMethod"] = value;
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06001D1D RID: 7453 RVA: 0x0005C5A2 File Offset: 0x0005A7A2
		// (set) Token: 0x06001D1E RID: 7454 RVA: 0x0005C5C2 File Offset: 0x0005A7C2
		[DefaultValue("InsertAssignments")]
		[Description("Specifies the web service method name to be used to insert gantt resource assignments.")]
		[ClientControlProperty]
		[Category("Behavior")]
		public string InsertAssignmentsMethod
		{
			get
			{
				return (string)(base.ViewState["InsertAssignmentsMethod"] ?? "InsertAssignments");
			}
			set
			{
				base.ViewState["InsertAssignmentsMethod"] = value;
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06001D1F RID: 7455 RVA: 0x0005C5D5 File Offset: 0x0005A7D5
		// (set) Token: 0x06001D20 RID: 7456 RVA: 0x0005C5F5 File Offset: 0x0005A7F5
		[ClientControlProperty]
		[Description("Specifies the web service method name to be used to update gantt resource assignments.")]
		[Category("Behavior")]
		[DefaultValue("UpdateAssignments")]
		public string UpdateAssignmentsMethod
		{
			get
			{
				return (string)(base.ViewState["UpdateAssignmentsMethod"] ?? "UpdateAssignments");
			}
			set
			{
				base.ViewState["UpdateAssignmentsMethod"] = value;
			}
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x0005C608 File Offset: 0x0005A808
		internal override void DescribeWebServiceSettings(string propertyName, JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
			descriptor.AddProperty(propertyName, serializer.Serialize(this));
		}
	}
}
