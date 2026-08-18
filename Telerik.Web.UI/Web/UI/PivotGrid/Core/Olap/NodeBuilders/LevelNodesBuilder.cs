using System;
using System.Collections.Generic;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.NodeBuilders
{
	// Token: 0x02000D03 RID: 3331
	internal class LevelNodesBuilder : INodeBuilder
	{
		// Token: 0x06007C3B RID: 31803 RVA: 0x001C9010 File Offset: 0x001C7210
		public LevelNodesBuilder(HierarchySchemaElement parentHierarchy)
		{
			this.parentHierarchy = parentHierarchy;
		}

		// Token: 0x06007C3C RID: 31804 RVA: 0x001C901F File Offset: 0x001C721F
		public void BuildNodes(ContainerNode parent)
		{
			if (this.parentHierarchy != null)
			{
				this.GenerateLevelNodes(parent);
			}
		}

		// Token: 0x06007C3D RID: 31805 RVA: 0x001C9030 File Offset: 0x001C7230
		private void GenerateLevelNodes(ContainerNode parent)
		{
			IList<LevelSchemaElement> validLevels = this.GetValidLevels();
			if (validLevels.Count == 2 && !string.IsNullOrEmpty(this.parentHierarchy.AllMemberName))
			{
				FieldInfoNode fieldInfoNode = LevelNodesBuilder.CreateGroupFieldDescriptionNode(this.parentHierarchy);
				OlapHierarchyFieldInfo olapHierarchyFieldInfo = fieldInfoNode.FieldInfo as OlapHierarchyFieldInfo;
				olapHierarchyFieldInfo.IsUserHierarchy = false;
				foreach (LevelSchemaElement levelItem in validLevels)
				{
					OlapHierarchyFieldInfo item = LevelNodesBuilder.CreateFielfInfoForLevel(levelItem);
					olapHierarchyFieldInfo.Levels.Add(item);
				}
				parent.Children.Add(fieldInfoNode);
				return;
			}
			FieldInfoNode fieldInfoNode2 = LevelNodesBuilder.CreateGroupFieldDescriptionNode(this.parentHierarchy);
			OlapHierarchyFieldInfo olapHierarchyFieldInfo2 = fieldInfoNode2.FieldInfo as OlapHierarchyFieldInfo;
			olapHierarchyFieldInfo2.IsUserHierarchy = true;
			foreach (LevelSchemaElement levelItem2 in validLevels)
			{
				OlapHierarchyFieldInfo olapHierarchyFieldInfo3 = LevelNodesBuilder.CreateFielfInfoForLevel(levelItem2);
				FieldInfoNode item2 = new FieldInfoNode(olapHierarchyFieldInfo3, ContainerNodeRole.None);
				olapHierarchyFieldInfo2.Levels.Add(olapHierarchyFieldInfo3);
				fieldInfoNode2.Children.Add(item2);
			}
			parent.Children.Add(fieldInfoNode2);
		}

		// Token: 0x06007C3E RID: 31806 RVA: 0x001C9170 File Offset: 0x001C7370
		private static OlapHierarchyFieldInfo CreateFielfInfoForLevel(LevelSchemaElement levelItem)
		{
			return new OlapHierarchyFieldInfo
			{
				AutoGenerateField = false,
				DisplayName = levelItem.Caption,
				Name = levelItem.UniqueName
			};
		}

		// Token: 0x06007C3F RID: 31807 RVA: 0x001C91A4 File Offset: 0x001C73A4
		private IList<LevelSchemaElement> GetValidLevels()
		{
			List<LevelSchemaElement> list = new List<LevelSchemaElement>();
			SchemaElementValidator validatorForType = SchemaValidatorFactory.GetValidatorForType(typeof(LevelSchemaElement));
			foreach (LevelSchemaElement levelSchemaElement in this.parentHierarchy.Levels)
			{
				SchemaValidationResult schemaValidationResult = validatorForType.Validate(levelSchemaElement);
				if (schemaValidationResult.IsValid)
				{
					list.Add(levelSchemaElement);
				}
				else
				{
					NodeBuilderHelper.SubmitTraceInformation(schemaValidationResult, "Level");
				}
			}
			return list;
		}

		// Token: 0x06007C40 RID: 31808 RVA: 0x001C9230 File Offset: 0x001C7430
		private static FieldInfoNode CreateGroupFieldDescriptionNode(HierarchySchemaElement hierarchyInfo)
		{
			return new FieldInfoNode(new OlapHierarchyFieldInfo
			{
				DisplayName = hierarchyInfo.Caption,
				Name = hierarchyInfo.UniqueName,
				PreferredRole = FieldRoles.Row,
				AllMemberName = hierarchyInfo.AllMemberName,
				SupportsMembersFunction = true
			});
		}

		// Token: 0x0400220C RID: 8716
		private HierarchySchemaElement parentHierarchy;
	}
}
