using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000353 RID: 851
	public class GanttDataBindings : BaseDataBindings
	{
		// Token: 0x06001D70 RID: 7536 RVA: 0x0005CBE6 File Offset: 0x0005ADE6
		public GanttDataBindings()
		{
			this.TasksDataBindings = new TasksDataBindings();
			this.DependenciesDataBindings = new DependenciesDataBindings();
			this.ResourcesDataBindings = new ResourcesDataBindings();
			this.AssignmentsDataBindings = new AssignmentsDataBindings();
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06001D71 RID: 7537 RVA: 0x0005CC1A File Offset: 0x0005AE1A
		public static GanttDataBindings Empty
		{
			get
			{
				return new GanttDataBindings();
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x0005CC21 File Offset: 0x0005AE21
		// (set) Token: 0x06001D73 RID: 7539 RVA: 0x0005CC29 File Offset: 0x0005AE29
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TasksDataBindings TasksDataBindings { get; set; }

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x0005CC32 File Offset: 0x0005AE32
		// (set) Token: 0x06001D75 RID: 7541 RVA: 0x0005CC3A File Offset: 0x0005AE3A
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DependenciesDataBindings DependenciesDataBindings { get; set; }

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x0005CC43 File Offset: 0x0005AE43
		// (set) Token: 0x06001D77 RID: 7543 RVA: 0x0005CC4B File Offset: 0x0005AE4B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ResourcesDataBindings ResourcesDataBindings { get; set; }

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x0005CC54 File Offset: 0x0005AE54
		// (set) Token: 0x06001D79 RID: 7545 RVA: 0x0005CC5C File Offset: 0x0005AE5C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public AssignmentsDataBindings AssignmentsDataBindings { get; set; }

		// Token: 0x06001D7A RID: 7546 RVA: 0x0005CC65 File Offset: 0x0005AE65
		public override void EnsureDataFields()
		{
			this.TasksDataBindings.EnsureDataFields();
			this.DependenciesDataBindings.EnsureDataFields();
			this.ResourcesDataBindings.EnsureDataFields();
			this.AssignmentsDataBindings.EnsureDataFields();
		}
	}
}
