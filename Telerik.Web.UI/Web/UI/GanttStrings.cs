using System;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200034F RID: 847
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GanttStrings : LocalizationStrings
	{
		// Token: 0x06001D22 RID: 7458 RVA: 0x0005C618 File Offset: 0x0005A818
		internal GanttStrings(LocalizationProvider provider) : base(provider)
		{
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x0005C621 File Offset: 0x0005A821
		// (set) Token: 0x06001D24 RID: 7460 RVA: 0x0005C62E File Offset: 0x0005A82E
		[Localizable(true)]
		[Category("Common")]
		[NotifyParentProperty(true)]
		[DefaultValue("Save")]
		[ClientPropertyName("save")]
		[ScriptIgnore]
		public string Save
		{
			get
			{
				return this.GetString("Save");
			}
			set
			{
				this.SetString("Save", value);
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x0005C63C File Offset: 0x0005A83C
		// (set) Token: 0x06001D26 RID: 7462 RVA: 0x0005C649 File Offset: 0x0005A849
		[DefaultValue("Cancel")]
		[ClientPropertyName("cancel")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Category("Common")]
		public string Cancel
		{
			get
			{
				return this.GetString("Cancel");
			}
			set
			{
				this.SetString("Cancel", value);
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x0005C657 File Offset: 0x0005A857
		// (set) Token: 0x06001D28 RID: 7464 RVA: 0x0005C664 File Offset: 0x0005A864
		[ClientPropertyName("destroy")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Common")]
		[DefaultValue("Delete")]
		public string Delete
		{
			get
			{
				return this.GetString("Delete");
			}
			set
			{
				this.SetString("Delete", value);
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x0005C672 File Offset: 0x0005A872
		// (set) Token: 0x06001D2A RID: 7466 RVA: 0x0005C67F File Offset: 0x0005A87F
		[Category("Common")]
		[DefaultValue("Are you sure you want to delete this dependency?")]
		[ClientPropertyName("deleteDependencyConfirmation")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string ConfirmDeleteDependencyText
		{
			get
			{
				return this.GetString("ConfirmDeleteDependencyText");
			}
			set
			{
				this.SetString("ConfirmDeleteDependencyText", value);
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x0005C68D File Offset: 0x0005A88D
		// (set) Token: 0x06001D2C RID: 7468 RVA: 0x0005C69A File Offset: 0x0005A89A
		[Category("Common")]
		[DefaultValue("Delete dependency")]
		[ClientPropertyName("deleteDependencyWindowTitle")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
		public string ConfirmDeleteDependencyTitle
		{
			get
			{
				return this.GetString("ConfirmDeleteDependencyTitle");
			}
			set
			{
				this.SetString("ConfirmDeleteDependencyTitle", value);
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06001D2D RID: 7469 RVA: 0x0005C6A8 File Offset: 0x0005A8A8
		// (set) Token: 0x06001D2E RID: 7470 RVA: 0x0005C6B5 File Offset: 0x0005A8B5
		[Category("Common")]
		[ClientPropertyName("deleteTaskConfirmation")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Are you sure you want to delete this task?")]
		public string ConfirmDeleteTaskText
		{
			get
			{
				return this.GetString("ConfirmDeleteTaskText");
			}
			set
			{
				this.SetString("ConfirmDeleteTaskText", value);
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06001D2F RID: 7471 RVA: 0x0005C6C3 File Offset: 0x0005A8C3
		// (set) Token: 0x06001D30 RID: 7472 RVA: 0x0005C6D0 File Offset: 0x0005A8D0
		[ClientPropertyName("deleteTaskWindowTitle")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Common")]
		[DefaultValue("Delete task")]
		public string ConfirmDeleteTaskTitle
		{
			get
			{
				return this.GetString("ConfirmDeleteTaskTitle");
			}
			set
			{
				this.SetString("ConfirmDeleteTaskTitle", value);
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06001D31 RID: 7473 RVA: 0x0005C6DE File Offset: 0x0005A8DE
		// (set) Token: 0x06001D32 RID: 7474 RVA: 0x0005C6EB File Offset: 0x0005A8EB
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[ScriptIgnore]
		[ClientPropertyName("day")]
		[Category("Views")]
		[DefaultValue("Day")]
		public string HeaderDay
		{
			get
			{
				return this.GetString("HeaderDay");
			}
			set
			{
				this.SetString("HeaderDay", value);
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06001D33 RID: 7475 RVA: 0x0005C6F9 File Offset: 0x0005A8F9
		// (set) Token: 0x06001D34 RID: 7476 RVA: 0x0005C706 File Offset: 0x0005A906
		[ClientPropertyName("week")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Views")]
		[DefaultValue("Week")]
		public string HeaderWeek
		{
			get
			{
				return this.GetString("HeaderWeek");
			}
			set
			{
				this.SetString("HeaderWeek", value);
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x0005C714 File Offset: 0x0005A914
		// (set) Token: 0x06001D36 RID: 7478 RVA: 0x0005C721 File Offset: 0x0005A921
		[Category("Views")]
		[ScriptIgnore]
		[DefaultValue("Month")]
		[ClientPropertyName("month")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string HeaderMonth
		{
			get
			{
				return this.GetString("HeaderMonth");
			}
			set
			{
				this.SetString("HeaderMonth", value);
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x0005C72F File Offset: 0x0005A92F
		// (set) Token: 0x06001D38 RID: 7480 RVA: 0x0005C73C File Offset: 0x0005A93C
		[Category("Views")]
		[DefaultValue("Year")]
		[ClientPropertyName("year")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[ScriptIgnore]
		public string HeaderYear
		{
			get
			{
				return this.GetString("HeaderYear");
			}
			set
			{
				this.SetString("HeaderYear", value);
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x0005C74A File Offset: 0x0005A94A
		// (set) Token: 0x06001D3A RID: 7482 RVA: 0x0005C757 File Offset: 0x0005A957
		[Category("Views")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("Start")]
		[Localizable(true)]
		[ClientPropertyName("start")]
		public string TooltipStart
		{
			get
			{
				return this.GetString("TooltipStart");
			}
			set
			{
				this.SetString("TooltipStart", value);
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x0005C765 File Offset: 0x0005A965
		// (set) Token: 0x06001D3C RID: 7484 RVA: 0x0005C772 File Offset: 0x0005A972
		[Category("Views")]
		[ScriptIgnore]
		[DefaultValue("End")]
		[ClientPropertyName("end")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string TooltipEnd
		{
			get
			{
				return this.GetString("TooltipEnd");
			}
			set
			{
				this.SetString("TooltipEnd", value);
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06001D3D RID: 7485 RVA: 0x0005C780 File Offset: 0x0005A980
		// (set) Token: 0x06001D3E RID: 7486 RVA: 0x0005C78D File Offset: 0x0005A98D
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Actions")]
		[Localizable(true)]
		[DefaultValue("Add Task")]
		[ClientPropertyName("append")]
		public string Append
		{
			get
			{
				return this.GetString("Append");
			}
			set
			{
				this.SetString("Append", value);
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x0005C79B File Offset: 0x0005A99B
		// (set) Token: 0x06001D40 RID: 7488 RVA: 0x0005C7A8 File Offset: 0x0005A9A8
		[DefaultValue("Add Child")]
		[ScriptIgnore]
		[Category("Actions")]
		[ClientPropertyName("addChild")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string AddChild
		{
			get
			{
				return this.GetString("AddChild");
			}
			set
			{
				this.SetString("AddChild", value);
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06001D41 RID: 7489 RVA: 0x0005C7B6 File Offset: 0x0005A9B6
		// (set) Token: 0x06001D42 RID: 7490 RVA: 0x0005C7C3 File Offset: 0x0005A9C3
		[ClientPropertyName("insertBefore")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Add Above")]
		[ScriptIgnore]
		[Category("Actions")]
		public string InsertBefore
		{
			get
			{
				return this.GetString("InsertBefore");
			}
			set
			{
				this.SetString("InsertBefore", value);
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x0005C7D1 File Offset: 0x0005A9D1
		// (set) Token: 0x06001D44 RID: 7492 RVA: 0x0005C7DE File Offset: 0x0005A9DE
		[Localizable(true)]
		[ClientPropertyName("insertAfter")]
		[NotifyParentProperty(true)]
		[DefaultValue("Add Below")]
		[ScriptIgnore]
		[Category("Actions")]
		public string InsertAfter
		{
			get
			{
				return this.GetString("InsertAfter");
			}
			set
			{
				this.SetString("InsertAfter", value);
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x0005C7EC File Offset: 0x0005A9EC
		// (set) Token: 0x06001D46 RID: 7494 RVA: 0x0005C7F9 File Offset: 0x0005A9F9
		[Category("Actions")]
		[ClientPropertyName("pdf")]
		[ScriptIgnore]
		[DefaultValue("Export to PDF")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ExportPdf
		{
			get
			{
				return this.GetString("ExportPdf");
			}
			set
			{
				this.SetString("ExportPdf", value);
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06001D47 RID: 7495 RVA: 0x0005C807 File Offset: 0x0005AA07
		// (set) Token: 0x06001D48 RID: 7496 RVA: 0x0005C814 File Offset: 0x0005AA14
		[DefaultValue("Task")]
		[ClientPropertyName("editorTitle")]
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Editor")]
		public string AdvancedWindowTitle
		{
			get
			{
				return this.GetString("AdvancedWindowTitle");
			}
			set
			{
				this.SetString("AdvancedWindowTitle", value);
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x0005C822 File Offset: 0x0005AA22
		// (set) Token: 0x06001D4A RID: 7498 RVA: 0x0005C82F File Offset: 0x0005AA2F
		[ScriptIgnore]
		[DefaultValue("Resources")]
		[Localizable(true)]
		[ClientPropertyName("resourcesEditorTitle")]
		[NotifyParentProperty(true)]
		[Category("Editor")]
		public string AdvancedResourcesTitle
		{
			get
			{
				return this.GetString("AdvancedResourcesTitle");
			}
			set
			{
				this.SetString("AdvancedResourcesTitle", value);
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06001D4B RID: 7499 RVA: 0x0005C83D File Offset: 0x0005AA3D
		// (set) Token: 0x06001D4C RID: 7500 RVA: 0x0005C84A File Offset: 0x0005AA4A
		[Localizable(true)]
		[ClientPropertyName("title")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[Category("Editor")]
		[DefaultValue("Title")]
		public string AdvancedTitle
		{
			get
			{
				return this.GetString("AdvancedTitle");
			}
			set
			{
				this.SetString("AdvancedTitle", value);
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x0005C858 File Offset: 0x0005AA58
		// (set) Token: 0x06001D4E RID: 7502 RVA: 0x0005C865 File Offset: 0x0005AA65
		[ClientPropertyName("start")]
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Editor")]
		[DefaultValue("Start")]
		public string AdvancedStart
		{
			get
			{
				return this.GetString("AdvancedStart");
			}
			set
			{
				this.SetString("AdvancedStart", value);
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x0005C873 File Offset: 0x0005AA73
		// (set) Token: 0x06001D50 RID: 7504 RVA: 0x0005C880 File Offset: 0x0005AA80
		[ClientPropertyName("end")]
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Editor")]
		[DefaultValue("End")]
		public string AdvancedEnd
		{
			get
			{
				return this.GetString("AdvancedEnd");
			}
			set
			{
				this.SetString("AdvancedEnd", value);
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x0005C88E File Offset: 0x0005AA8E
		// (set) Token: 0x06001D52 RID: 7506 RVA: 0x0005C89B File Offset: 0x0005AA9B
		[Category("Editor")]
		[ScriptIgnore]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Complete")]
		[ClientPropertyName("percentComplete")]
		public string AdvancedPercentComplete
		{
			get
			{
				return this.GetString("AdvancedPercentComplete");
			}
			set
			{
				this.SetString("AdvancedPercentComplete", value);
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x0005C8A9 File Offset: 0x0005AAA9
		// (set) Token: 0x06001D54 RID: 7508 RVA: 0x0005C8B6 File Offset: 0x0005AAB6
		[ScriptIgnore]
		[DefaultValue("Resources")]
		[Localizable(true)]
		[ClientPropertyName("resources")]
		[NotifyParentProperty(true)]
		[Category("Editor")]
		public string AdvancedResources
		{
			get
			{
				return this.GetString("AdvancedResources");
			}
			set
			{
				this.SetString("AdvancedResources", value);
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x0005C8C4 File Offset: 0x0005AAC4
		// (set) Token: 0x06001D56 RID: 7510 RVA: 0x0005C8D1 File Offset: 0x0005AAD1
		[Localizable(true)]
		[ClientPropertyName("assingButton")]
		[NotifyParentProperty(true)]
		[DefaultValue("Assign")]
		[ScriptIgnore]
		[Category("Editor")]
		public string AdvancedAssignResources
		{
			get
			{
				return this.GetString("AdvancedAssignResources");
			}
			set
			{
				this.SetString("AdvancedAssignResources", value);
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x0005C8DF File Offset: 0x0005AADF
		// (set) Token: 0x06001D58 RID: 7512 RVA: 0x0005C8EC File Offset: 0x0005AAEC
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Resources")]
		[ScriptIgnore]
		[ClientPropertyName("resourcesHeader")]
		[Category("Editor")]
		public string AdvancedResourcesHeader
		{
			get
			{
				return this.GetString("AdvancedResourcesHeader");
			}
			set
			{
				this.SetString("AdvancedResourcesHeader", value);
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x0005C8FA File Offset: 0x0005AAFA
		// (set) Token: 0x06001D5A RID: 7514 RVA: 0x0005C907 File Offset: 0x0005AB07
		[ClientPropertyName("unitsHeader")]
		[DefaultValue("Units")]
		[Localizable(true)]
		[ScriptIgnore]
		[Category("Editor")]
		[NotifyParentProperty(true)]
		public string AdvancedUnitsHeader
		{
			get
			{
				return this.GetString("AdvancedUnitsHeader");
			}
			set
			{
				this.SetString("AdvancedUnitsHeader", value);
			}
		}
	}
}
