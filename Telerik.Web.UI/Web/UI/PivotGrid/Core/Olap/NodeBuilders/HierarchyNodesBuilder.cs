using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.NodeBuilders
{
	// Token: 0x02000D01 RID: 3329
	internal class HierarchyNodesBuilder : INodeBuilder
	{
		// Token: 0x06007C29 RID: 31785 RVA: 0x001C8B1E File Offset: 0x001C6D1E
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Web.UI.PivotGrid.Core.Fields.ContainerNode.#ctor(System.String,System.String,Telerik.Web.UI.PivotGrid.Core.Fields.ContainerNodeRole)", Justification = "Will fix soon.")]
		public HierarchyNodesBuilder(DimensionSchemaElement dimension)
		{
			this.parentDimension = dimension;
			this.moreFieldsContainer = new ContainerNode("More fields", PivotLocalizationManager.MoreFields, ContainerNodeRole.Other);
		}

		// Token: 0x06007C2A RID: 31786 RVA: 0x001C8B43 File Offset: 0x001C6D43
		public void BuildNodes(ContainerNode parent)
		{
			if (this.parentDimension != null)
			{
				this.GenerateHierarchyNodes(parent);
			}
		}

		// Token: 0x06007C2B RID: 31787 RVA: 0x001C8B54 File Offset: 0x001C6D54
		private void GenerateHierarchyNodes(ContainerNode parent)
		{
			IEnumerable<HierarchySchemaElement> validHierarchies = this.GetValidHierarchies();
			foreach (HierarchySchemaElement hierarchySchemaElement in validHierarchies)
			{
				if (!string.IsNullOrEmpty(hierarchySchemaElement.DisplayFolder))
				{
					HierarchyNodesBuilder.AddHierarchiWithDisplayFolderToContainer(hierarchySchemaElement, parent);
				}
				else if (hierarchySchemaElement.Levels.Count == 2 && !string.IsNullOrEmpty(hierarchySchemaElement.AllMemberName))
				{
					HierarchyNodesBuilder.GenerateLevelNodes(hierarchySchemaElement, this.moreFieldsContainer);
				}
				else
				{
					HierarchyNodesBuilder.GenerateLevelNodes(hierarchySchemaElement, parent);
				}
			}
			if (this.moreFieldsContainer.HasChildren)
			{
				parent.Children.Add(this.moreFieldsContainer);
			}
		}

		// Token: 0x06007C2C RID: 31788 RVA: 0x001C8C00 File Offset: 0x001C6E00
		private IEnumerable<HierarchySchemaElement> GetValidHierarchies()
		{
			List<HierarchySchemaElement> list = new List<HierarchySchemaElement>();
			SchemaElementValidator validatorForType = SchemaValidatorFactory.GetValidatorForType(typeof(HierarchySchemaElement));
			foreach (HierarchySchemaElement hierarchySchemaElement in this.parentDimension.Hierarchies)
			{
				SchemaValidationResult schemaValidationResult = validatorForType.Validate(hierarchySchemaElement);
				if (schemaValidationResult.IsValid)
				{
					list.Add(hierarchySchemaElement);
				}
				else
				{
					NodeBuilderHelper.SubmitTraceInformation(schemaValidationResult, "Hierarchy");
				}
			}
			return list;
		}

		// Token: 0x06007C2D RID: 31789 RVA: 0x001C8C8C File Offset: 0x001C6E8C
		private static void AddHierarchiWithDisplayFolderToContainer(HierarchySchemaElement hierarchyItem, ContainerNode container)
		{
			OlapDisplayFolderParser olapDisplayFolderParser = new OlapDisplayFolderParser(hierarchyItem.DisplayFolder);
			ContainerNode orCreateFolderNodes = ContainerNodeCollectionHelper.GetOrCreateFolderNodes(container, olapDisplayFolderParser.FolderLevels);
			HierarchyNodesBuilder.GenerateLevelNodes(hierarchyItem, orCreateFolderNodes);
		}

		// Token: 0x06007C2E RID: 31790 RVA: 0x001C8CBC File Offset: 0x001C6EBC
		private static void GenerateLevelNodes(HierarchySchemaElement hierarchyInfo, ContainerNode hierarchyNode)
		{
			LevelNodesBuilder levelNodesBuilder = new LevelNodesBuilder(hierarchyInfo);
			levelNodesBuilder.BuildNodes(hierarchyNode);
		}

		// Token: 0x04002208 RID: 8712
		private DimensionSchemaElement parentDimension;

		// Token: 0x04002209 RID: 8713
		private ContainerNode moreFieldsContainer;
	}
}
