using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.NodeBuilders
{
	// Token: 0x02000D00 RID: 3328
	internal class DimensionNodesBuilder : INodeBuilder
	{
		// Token: 0x06007C1F RID: 31775 RVA: 0x001C880F File Offset: 0x001C6A0F
		public DimensionNodesBuilder(OlapCubeInfo cubeInfo)
		{
			this.cubeInfo = cubeInfo;
			if (this.cubeInfo == null)
			{
				this.cubeInfo = new OlapCubeInfo();
			}
		}

		// Token: 0x06007C20 RID: 31776 RVA: 0x001C8831 File Offset: 0x001C6A31
		public void BuildNodes(ContainerNode parent)
		{
			this.GenerateDimensionNodes(parent);
		}

		// Token: 0x06007C21 RID: 31777 RVA: 0x001C883C File Offset: 0x001C6A3C
		private void GenerateDimensionNodes(ContainerNode root)
		{
			IEnumerable<DimensionSchemaElement> validDimensions = this.GetValidDimensions();
			foreach (DimensionSchemaElement dimensionItem in validDimensions)
			{
				ContainerNode item = this.CreateDimensionNode(dimensionItem);
				root.Children.Add(item);
			}
		}

		// Token: 0x06007C22 RID: 31778 RVA: 0x001C8898 File Offset: 0x001C6A98
		private ContainerNode CreateDimensionNode(DimensionSchemaElement dimensionItem)
		{
			ContainerNode containerNode = new ContainerNode(dimensionItem.UniqueName, dimensionItem.Caption, ContainerNodeRole.Dimension);
			DimensionNodesBuilder.GenerateHierarchiesForDimension(dimensionItem, containerNode);
			this.GenerateNamedSetsForDimension(containerNode);
			return containerNode;
		}

		// Token: 0x06007C23 RID: 31779 RVA: 0x001C88C8 File Offset: 0x001C6AC8
		private static void GenerateHierarchiesForDimension(DimensionSchemaElement dimensionItem, ContainerNode dimensionNode)
		{
			HierarchyNodesBuilder hierarchyNodesBuilder = new HierarchyNodesBuilder(dimensionItem);
			hierarchyNodesBuilder.BuildNodes(dimensionNode);
		}

		// Token: 0x06007C24 RID: 31780 RVA: 0x001C88E4 File Offset: 0x001C6AE4
		private IEnumerable<DimensionSchemaElement> GetValidDimensions()
		{
			List<DimensionSchemaElement> list = new List<DimensionSchemaElement>();
			SchemaElementValidator validatorForType = SchemaValidatorFactory.GetValidatorForType(typeof(DimensionSchemaElement));
			foreach (DimensionSchemaElement dimensionSchemaElement in this.cubeInfo.Dimensions)
			{
				if (!(dimensionSchemaElement.UniqueName == "[Measures]"))
				{
					SchemaValidationResult schemaValidationResult = validatorForType.Validate(dimensionSchemaElement);
					if (schemaValidationResult.IsValid)
					{
						list.Add(dimensionSchemaElement);
					}
					else
					{
						NodeBuilderHelper.SubmitTraceInformation(schemaValidationResult, "Dimension");
					}
				}
			}
			return list;
		}

		// Token: 0x06007C25 RID: 31781 RVA: 0x001C8984 File Offset: 0x001C6B84
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Web.UI.PivotGrid.Core.Fields.ContainerNode.#ctor(System.String,System.String,Telerik.Web.UI.PivotGrid.Core.Fields.ContainerNodeRole)", Justification = "Will fix soon.")]
		private void GenerateNamedSetsForDimension(ContainerNode dimensionNode)
		{
			IEnumerable<NamedSetSchemaElement> validNamedSets = DimensionNodesBuilder.GetValidNamedSets(this.cubeInfo.NamedSets);
			ContainerNode containerNode = new ContainerNode("Sets", "Sets", ContainerNodeRole.Folder);
			foreach (NamedSetSchemaElement namedSetItem in validNamedSets)
			{
				DimensionNodesBuilder.AddNamedSetNode(namedSetItem, containerNode);
			}
			if (containerNode.Children.Count > 0)
			{
				dimensionNode.Children.Add(containerNode);
			}
		}

		// Token: 0x06007C26 RID: 31782 RVA: 0x001C8A08 File Offset: 0x001C6C08
		private static IEnumerable<NamedSetSchemaElement> GetValidNamedSets(IList<NamedSetSchemaElement> namedSets)
		{
			List<NamedSetSchemaElement> list = new List<NamedSetSchemaElement>();
			SchemaElementValidator validatorForType = SchemaValidatorFactory.GetValidatorForType(typeof(NamedSetSchemaElement));
			foreach (NamedSetSchemaElement namedSetSchemaElement in namedSets)
			{
				SchemaValidationResult schemaValidationResult = validatorForType.Validate(namedSetSchemaElement);
				if (schemaValidationResult.IsValid)
				{
					list.Add(namedSetSchemaElement);
				}
				else
				{
					NodeBuilderHelper.SubmitTraceInformation(schemaValidationResult, "NamedSet");
				}
			}
			return list;
		}

		// Token: 0x06007C27 RID: 31783 RVA: 0x001C8A8C File Offset: 0x001C6C8C
		private static void AddNamedSetNode(NamedSetSchemaElement namedSetItem, ContainerNode namedSetsNode)
		{
			FieldInfoNode item = DimensionNodesBuilder.CreateGroupFieldDescriptionNode(namedSetItem);
			namedSetsNode.Children.Add(item);
		}

		// Token: 0x06007C28 RID: 31784 RVA: 0x001C8AAC File Offset: 0x001C6CAC
		private static FieldInfoNode CreateGroupFieldDescriptionNode(NamedSetSchemaElement namedSet)
		{
			OlapHierarchyFieldInfo olapHierarchyFieldInfo = new OlapHierarchyFieldInfo();
			olapHierarchyFieldInfo.DisplayName = namedSet.Caption;
			olapHierarchyFieldInfo.Name = MemberNameHelper.GetMemberWithBrackets(namedSet.Name);
			olapHierarchyFieldInfo.PreferredRole = FieldRoles.Row;
			olapHierarchyFieldInfo.AllowedRoles = (FieldRoles.Row | FieldRoles.Column);
			olapHierarchyFieldInfo.SupportsMembersFunction = false;
			olapHierarchyFieldInfo.ShouldIgnoreHierarchicalStructure = true;
			OlapHierarchyFieldInfo olapHierarchyFieldInfo2 = new OlapHierarchyFieldInfo();
			olapHierarchyFieldInfo2.Name = olapHierarchyFieldInfo.Name;
			olapHierarchyFieldInfo.Levels.Add(olapHierarchyFieldInfo2);
			return new FieldInfoNode(olapHierarchyFieldInfo);
		}

		// Token: 0x04002207 RID: 8711
		private OlapCubeInfo cubeInfo;
	}
}
