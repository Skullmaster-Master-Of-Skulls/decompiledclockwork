using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.NodeBuilders
{
	// Token: 0x02000D04 RID: 3332
	internal class MeasureNodesBuilder : INodeBuilder
	{
		// Token: 0x06007C41 RID: 31809 RVA: 0x001C927D File Offset: 0x001C747D
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Web.UI.PivotGrid.Core.Fields.ContainerNode.#ctor(System.String,System.String,Telerik.Web.UI.PivotGrid.Core.Fields.ContainerNodeRole)", Justification = "Will fix soon.")]
		public MeasureNodesBuilder(IEnumerable<MeasureSchemaElement> measures)
		{
			this.measures = measures;
			if (this.measures == null)
			{
				this.measures = new List<MeasureSchemaElement>();
			}
			this.measuresContainer = new ContainerNode("[Measures]", "Measures", ContainerNodeRole.Measure);
		}

		// Token: 0x06007C42 RID: 31810 RVA: 0x001C92B5 File Offset: 0x001C74B5
		public void BuildNodes(ContainerNode parent)
		{
			this.BuildMeasuresHierarchy(parent);
			this.AddMasuresNodeIfNeeded(parent);
		}

		// Token: 0x06007C43 RID: 31811 RVA: 0x001C92C8 File Offset: 0x001C74C8
		private void BuildMeasuresHierarchy(ContainerNode parent)
		{
			IEnumerable<MeasureSchemaElement> validMeasures = this.GetValidMeasures();
			foreach (MeasureSchemaElement measureItem in validMeasures)
			{
				this.ProcessValidMeasureItem(parent, measureItem);
			}
		}

		// Token: 0x06007C44 RID: 31812 RVA: 0x001C9318 File Offset: 0x001C7518
		private void AddMasuresNodeIfNeeded(ContainerNode parent)
		{
			if (this.measuresContainer.Children.Count > 0)
			{
				parent.Children.Add(this.measuresContainer);
			}
		}

		// Token: 0x06007C45 RID: 31813 RVA: 0x001C9340 File Offset: 0x001C7540
		private IEnumerable<MeasureSchemaElement> GetValidMeasures()
		{
			SchemaElementValidator validatorForType = SchemaValidatorFactory.GetValidatorForType(typeof(MeasureSchemaElement));
			List<MeasureSchemaElement> list = new List<MeasureSchemaElement>();
			foreach (MeasureSchemaElement measureSchemaElement in this.measures)
			{
				SchemaValidationResult schemaValidationResult = validatorForType.Validate(measureSchemaElement);
				if (schemaValidationResult.IsValid)
				{
					list.Add(measureSchemaElement);
				}
				else
				{
					NodeBuilderHelper.SubmitTraceInformation(schemaValidationResult, "Measure");
				}
			}
			return list;
		}

		// Token: 0x06007C46 RID: 31814 RVA: 0x001C93C8 File Offset: 0x001C75C8
		private void ProcessValidMeasureItem(ContainerNode root, MeasureSchemaElement measureItem)
		{
			ContainerNode parentNodeForMeasure = this.GetParentNodeForMeasure(root, measureItem);
			FieldInfoNode item = MeasureNodesBuilder.CreateAggregateFieldDescriptionNode(measureItem, FieldRoles.Value);
			parentNodeForMeasure.Children.Add(item);
		}

		// Token: 0x06007C47 RID: 31815 RVA: 0x001C93F4 File Offset: 0x001C75F4
		private static FieldInfoNode CreateAggregateFieldDescriptionNode(MeasureSchemaElement measure, FieldRoles role)
		{
			return new FieldInfoNode(new OlapAggregateFieldInfo
			{
				DisplayName = measure.Caption,
				Name = measure.UniqueName,
				PreferredRole = role,
				DataType = MeasureNodesBuilder.GetDataTypeFromDataTypeNumber(measure.DataTypeNumber)
			});
		}

		// Token: 0x06007C48 RID: 31816 RVA: 0x001C943F File Offset: 0x001C763F
		private static Type GetDataTypeFromDataTypeNumber(int number)
		{
			if (number == 3)
			{
				return typeof(int);
			}
			return typeof(double);
		}

		// Token: 0x06007C49 RID: 31817 RVA: 0x001C945C File Offset: 0x001C765C
		private ContainerNode GetParentNodeForMeasure(ContainerNode rootNode, MeasureSchemaElement measure)
		{
			if (!string.IsNullOrEmpty(measure.GroupCaption))
			{
				IList<string> folderLevels = new OlapDisplayFolderParser(measure.GroupCaption).FolderLevels;
				ContainerNode orCreateFolderNodes = ContainerNodeCollectionHelper.GetOrCreateFolderNodes(rootNode, folderLevels);
				orCreateFolderNodes.Role = ContainerNodeRole.Measure;
				return orCreateFolderNodes;
			}
			if (!string.IsNullOrEmpty(measure.GroupName))
			{
				IList<string> folderLevels2 = new OlapDisplayFolderParser(measure.GroupName).FolderLevels;
				ContainerNode orCreateFolderNodes2 = ContainerNodeCollectionHelper.GetOrCreateFolderNodes(rootNode, folderLevels2);
				orCreateFolderNodes2.Role = ContainerNodeRole.Measure;
				return orCreateFolderNodes2;
			}
			if (!string.IsNullOrEmpty(measure.DisplayFolder))
			{
				IList<string> folderLevels3 = new OlapDisplayFolderParser(measure.DisplayFolder).FolderLevels;
				ContainerNode orCreateFolderNodes3 = ContainerNodeCollectionHelper.GetOrCreateFolderNodes(rootNode, folderLevels3);
				orCreateFolderNodes3.Role = ContainerNodeRole.Measure;
				return orCreateFolderNodes3;
			}
			return this.measuresContainer;
		}

		// Token: 0x0400220D RID: 8717
		private IEnumerable<MeasureSchemaElement> measures;

		// Token: 0x0400220E RID: 8718
		private ContainerNode measuresContainer;
	}
}
