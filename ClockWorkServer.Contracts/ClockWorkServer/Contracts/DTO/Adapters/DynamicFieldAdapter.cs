using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Adapters
{
	// Token: 0x02000C80 RID: 3200
	public static class DynamicFieldAdapter
	{
		// Token: 0x060042BC RID: 17084 RVA: 0x0002149C File Offset: 0x0001F69C
		public static Forest<DynamicFieldDTO> FieldListToForest(this List<DynamicFieldDTO> Fields)
		{
			Forest<DynamicFieldDTO> forest = new Forest<DynamicFieldDTO>();
			Stack<TreeNode<DynamicFieldDTO>> stack = new Stack<TreeNode<DynamicFieldDTO>>();
			stack.Push(null);
			foreach (DynamicFieldDTO dynamicFieldDTO in Fields)
			{
				TreeNode<DynamicFieldDTO> parentNode = stack.Peek();
				DynamicControlAttribute dynamicControlAttribute = dynamicFieldDTO.ControlCode.GetDynamicControlAttribute();
				bool isControlCollectionStart = dynamicControlAttribute.IsControlCollectionStart;
				if (isControlCollectionStart)
				{
					TreeNode<DynamicFieldDTO> item = forest.AppendNode(parentNode, dynamicFieldDTO);
					stack.Push(item);
				}
				else
				{
					bool isControlCollectionEnd = dynamicControlAttribute.IsControlCollectionEnd;
					if (isControlCollectionEnd)
					{
						stack.Pop();
					}
					else
					{
						forest.AppendNode(parentNode, dynamicFieldDTO);
					}
				}
			}
			return forest;
		}
	}
}
