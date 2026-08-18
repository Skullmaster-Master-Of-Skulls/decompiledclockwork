using System;
using System.Diagnostics.CodeAnalysis;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.NodeBuilders
{
	// Token: 0x02000D02 RID: 3330
	internal class KpiNodesBuilder : INodeBuilder
	{
		// Token: 0x06007C2F RID: 31791 RVA: 0x001C8CD7 File Offset: 0x001C6ED7
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Web.UI.PivotGrid.Core.Fields.ContainerNode.#ctor(System.String,System.String,Telerik.Web.UI.PivotGrid.Core.Fields.ContainerNodeRole)", Justification = "Will fix soon.")]
		public KpiNodesBuilder(OlapCubeInfo cube)
		{
			this.cube = cube;
			if (cube == null)
			{
				this.cube = new OlapCubeInfo();
			}
			this.rootKpiNode = new ContainerNode("KPIs", "KPIs", ContainerNodeRole.Kpi);
		}

		// Token: 0x06007C30 RID: 31792 RVA: 0x001C8D0A File Offset: 0x001C6F0A
		public void BuildNodes(ContainerNode parent)
		{
			if (parent == null)
			{
				return;
			}
			this.BuildKpiHierarchy();
			this.AddToParentIfKpisExist(parent);
		}

		// Token: 0x06007C31 RID: 31793 RVA: 0x001C8D1D File Offset: 0x001C6F1D
		private void AddToParentIfKpisExist(ContainerNode parent)
		{
			if (this.rootKpiNode.Children.Count > 0)
			{
				parent.Children.Add(this.rootKpiNode);
			}
		}

		// Token: 0x06007C32 RID: 31794 RVA: 0x001C8D44 File Offset: 0x001C6F44
		private void BuildKpiHierarchy()
		{
			foreach (KpiSchemaElement kpiItem in this.cube.Kpis)
			{
				this.ProcessKpi(kpiItem);
			}
		}

		// Token: 0x06007C33 RID: 31795 RVA: 0x001C8D98 File Offset: 0x001C6F98
		private void ProcessKpi(KpiSchemaElement kpiItem)
		{
			SchemaElementValidator validatorForType = SchemaValidatorFactory.GetValidatorForType(typeof(KpiSchemaElement));
			SchemaValidationResult schemaValidationResult = validatorForType.Validate(kpiItem);
			if (schemaValidationResult.IsValid)
			{
				this.AddKpiNode(kpiItem);
				return;
			}
			NodeBuilderHelper.SubmitTraceInformation(schemaValidationResult, "KPI");
		}

		// Token: 0x06007C34 RID: 31796 RVA: 0x001C8DD8 File Offset: 0x001C6FD8
		private void AddKpiNode(KpiSchemaElement kpiItem)
		{
			ContainerNode item = KpiNodesBuilder.CreateKpiNode(kpiItem);
			ContainerNode containerNode = KpiNodesBuilder.CreateKpiRootNode(this.rootKpiNode, kpiItem);
			containerNode.Children.Add(item);
		}

		// Token: 0x06007C35 RID: 31797 RVA: 0x001C8E08 File Offset: 0x001C7008
		private static ContainerNode CreateKpiRootNode(ContainerNode initialRoot, KpiSchemaElement kpiItem)
		{
			ContainerNode result = initialRoot;
			OlapDisplayFolderParser olapDisplayFolderParser = new OlapDisplayFolderParser(kpiItem.DisplayFolder);
			if (olapDisplayFolderParser.HasFolder)
			{
				result = ContainerNodeCollectionHelper.GetOrCreateFolderNodes(initialRoot, olapDisplayFolderParser.FolderLevels);
			}
			return result;
		}

		// Token: 0x06007C36 RID: 31798 RVA: 0x001C8E3C File Offset: 0x001C703C
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Web.UI.PivotGrid.Core.Olap.NodeBuilders.KpisAsASingleNodeBuilder.CreateKpiMemberNode(System.String,System.String,Telerik.Web.UI.PivotGrid.Core.Olap.OlapKpiInfo)", Justification = "Will fix in the future.")]
		private static ContainerNode CreateKpiNode(KpiSchemaElement kpiInfo)
		{
			ContainerNode containerNode = new ContainerNode(kpiInfo.Name, kpiInfo.Caption, ContainerNodeRole.Kpi);
			FieldInfoNode fieldInfoNode = KpiNodesBuilder.CreateKpiValueNode(kpiInfo);
			FieldInfoNode fieldInfoNode2 = KpiNodesBuilder.CreateKpiGoalNode(kpiInfo);
			FieldInfoNode fieldInfoNode3 = KpiNodesBuilder.CreateKpiStatusNode(kpiInfo);
			FieldInfoNode fieldInfoNode4 = KpiNodesBuilder.CreateKpiTrendNode(kpiInfo);
			if (fieldInfoNode != null)
			{
				containerNode.Children.Add(fieldInfoNode);
			}
			if (fieldInfoNode2 != null)
			{
				containerNode.Children.Add(fieldInfoNode2);
			}
			if (fieldInfoNode3 != null)
			{
				containerNode.Children.Add(fieldInfoNode3);
			}
			if (fieldInfoNode4 != null)
			{
				containerNode.Children.Add(fieldInfoNode4);
			}
			return containerNode;
		}

		// Token: 0x06007C37 RID: 31799 RVA: 0x001C8EB8 File Offset: 0x001C70B8
		private static FieldInfoNode CreateKpiValueNode(KpiSchemaElement kpiInfo)
		{
			if (string.IsNullOrEmpty(kpiInfo.ValueMemberUniqueName))
			{
				return null;
			}
			return new FieldInfoNode(new OlapAggregateFieldInfo
			{
				DisplayName = "Value (" + kpiInfo.Name + ")",
				Name = kpiInfo.ValueMemberUniqueName
			});
		}

		// Token: 0x06007C38 RID: 31800 RVA: 0x001C8F0C File Offset: 0x001C710C
		private static FieldInfoNode CreateKpiGoalNode(KpiSchemaElement kpiInfo)
		{
			if (string.IsNullOrEmpty(kpiInfo.GoalMemberUniqueName))
			{
				return null;
			}
			return new FieldInfoNode(new OlapAggregateFieldInfo
			{
				DisplayName = "Goal (" + kpiInfo.Name + ")",
				Name = kpiInfo.GoalMemberUniqueName
			});
		}

		// Token: 0x06007C39 RID: 31801 RVA: 0x001C8F60 File Offset: 0x001C7160
		private static FieldInfoNode CreateKpiStatusNode(KpiSchemaElement kpiInfo)
		{
			if (string.IsNullOrEmpty(kpiInfo.StatusMemberUniqueName))
			{
				return null;
			}
			return new FieldInfoNode(new OlapAggregateFieldInfo
			{
				DisplayName = "Status (" + kpiInfo.Name + ")",
				Name = kpiInfo.StatusMemberUniqueName,
				DisplayValueAsKpi = true
			});
		}

		// Token: 0x06007C3A RID: 31802 RVA: 0x001C8FB8 File Offset: 0x001C71B8
		private static FieldInfoNode CreateKpiTrendNode(KpiSchemaElement kpiInfo)
		{
			if (string.IsNullOrEmpty(kpiInfo.TrendMemberUniqueName))
			{
				return null;
			}
			return new FieldInfoNode(new OlapAggregateFieldInfo
			{
				DisplayName = "Trend (" + kpiInfo.Name + ")",
				Name = kpiInfo.TrendMemberUniqueName,
				DisplayValueAsKpi = true
			});
		}

		// Token: 0x0400220A RID: 8714
		private OlapCubeInfo cube;

		// Token: 0x0400220B RID: 8715
		private ContainerNode rootKpiNode;
	}
}
