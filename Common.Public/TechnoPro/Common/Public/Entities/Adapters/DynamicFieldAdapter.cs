using System;
using System.Collections.Generic;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005C0 RID: 1472
	public static class DynamicFieldAdapter
	{
		// Token: 0x06002F76 RID: 12150 RVA: 0x00036058 File Offset: 0x00034258
		public static bool IsDataEncrypted(this DynamicField field)
		{
			bool flag = field == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				eControlCode controlCode = field.ControlCode;
				eControlCode eControlCode = controlCode;
				if (eControlCode <= eControlCode.MyTextBox)
				{
					if (eControlCode == eControlCode.TextBox)
					{
						goto IL_6A;
					}
					if (eControlCode != eControlCode.DropList)
					{
						if (eControlCode != eControlCode.MyTextBox)
						{
							goto IL_76;
						}
						goto IL_6A;
					}
				}
				else if (eControlCode <= eControlCode.RtfTextBox)
				{
					if (eControlCode != eControlCode.MaskedTextBox && eControlCode != eControlCode.RtfTextBox)
					{
						goto IL_76;
					}
					goto IL_6A;
				}
				else
				{
					if (eControlCode == eControlCode.AccommodationTextBox)
					{
						goto IL_6A;
					}
					if (eControlCode != eControlCode.AccommodationDropList)
					{
						goto IL_76;
					}
				}
				return field.Setting3 == -1;
				IL_6A:
				return field.Setting3 != 0;
				IL_76:
				result = false;
			}
			return result;
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x000360E0 File Offset: 0x000342E0
		public static Forest<DynamicField> FieldListToForest(this List<DynamicField> Fields)
		{
			Forest<DynamicField> forest = new Forest<DynamicField>();
			Stack<TreeNode<DynamicField>> stack = new Stack<TreeNode<DynamicField>>();
			stack.Push(null);
			foreach (DynamicField dynamicField in Fields)
			{
				TreeNode<DynamicField> parentNode = stack.Peek();
				DynamicControlAttribute dynamicControlAttribute = dynamicField.ControlCode.GetDynamicControlAttribute();
				bool isControlCollectionStart = dynamicControlAttribute.IsControlCollectionStart;
				if (isControlCollectionStart)
				{
					TreeNode<DynamicField> item = forest.AppendNode(parentNode, dynamicField);
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
						forest.AppendNode(parentNode, dynamicField);
					}
				}
			}
			return forest;
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x000361A8 File Offset: 0x000343A8
		public static DynamicControlAttribute GetDynamicControlAttribute(this eControlCode eControlCodeDTO)
		{
			DynamicControlAttribute dynamicControlAttribute = eControlCodeDTO.GetAttribute<DynamicControlAttribute>();
			bool flag = dynamicControlAttribute == null;
			if (flag)
			{
				dynamicControlAttribute = new DynamicControlAttribute();
			}
			return dynamicControlAttribute;
		}
	}
}
