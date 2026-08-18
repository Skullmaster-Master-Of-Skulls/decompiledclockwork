using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Adapters
{
	// Token: 0x02000C81 RID: 3201
	public static class DynamicFormsAdapter
	{
		// Token: 0x060042BD RID: 17085 RVA: 0x00021564 File Offset: 0x0001F764
		public static List<string[]> DecodeDocumentsList(this string list)
		{
			List<string[]> list2 = new List<string[]>();
			bool flag = string.IsNullOrEmpty(list);
			List<string[]> result;
			if (flag)
			{
				result = list2;
			}
			else
			{
				string[] array = list.Split(new char[]
				{
					'\t'
				});
				string[] array2 = new string[0];
				foreach (string text in array)
				{
					string[] array4 = text.Split(new char[1]);
					array2 = new string[array4.Length];
					for (int j = 0; j < array4.Length; j++)
					{
						array2[j] = array4[j];
					}
					list2.Add(array2);
				}
				result = list2;
			}
			return result;
		}

		// Token: 0x060042BE RID: 17086 RVA: 0x00021610 File Offset: 0x0001F810
		public static string EncodeDocumentsList(this List<string[]> items)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string[] array in items)
			{
				bool flag = array != null && array.Length != 0;
				if (flag)
				{
					string value = string.Join('\0'.ToString(), array);
					bool flag2 = stringBuilder.Length > 0;
					if (flag2)
					{
						stringBuilder.Append('\t');
					}
					stringBuilder.Append(value);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060042BF RID: 17087 RVA: 0x000216B4 File Offset: 0x0001F8B4
		public static string GetDescription(this DynamicFieldDTO DynamicField)
		{
			int num = DynamicField.ControlCaption.IndexOf("~~");
			return (num > 0) ? DynamicField.ControlCaption.Substring(0, num) : DynamicField.ControlCaption;
		}

		// Token: 0x060042C0 RID: 17088 RVA: 0x000216F4 File Offset: 0x0001F8F4
		public static string ControlCaptionForDisplay(this DynamicFieldDTO Field)
		{
			bool flag = Field == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = Field.ControlCaption.GetCaptionForDisplay();
			}
			return result;
		}

		// Token: 0x060042C1 RID: 17089 RVA: 0x00021724 File Offset: 0x0001F924
		public static string AltLanguageControlCaptionForDisplay(this DynamicFieldDTO Field)
		{
			string setting4String = Field.Setting4String;
			int num = setting4String.IndexOf("__");
			bool flag = num >= 0;
			string result;
			if (flag)
			{
				result = ((num == 0) ? "" : setting4String.Substring(0, num));
			}
			else
			{
				num = setting4String.IndexOf("~~");
				bool flag2 = num >= 0;
				if (flag2)
				{
					result = ((num == 0) ? "" : setting4String.Substring(0, num));
				}
				else
				{
					result = setting4String;
				}
			}
			return result;
		}

		// Token: 0x060042C2 RID: 17090 RVA: 0x00021798 File Offset: 0x0001F998
		public static object GetValueForPresentation(this DynamicDataDTO DataItem)
		{
			bool flag = DataItem == null;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DynamicFieldDTO field = DataItem.Field;
				Type type;
				if (field == null)
				{
					type = null;
				}
				else
				{
					DynamicControlAttribute attribute = field.ControlCode.GetAttribute<DynamicControlAttribute>();
					type = ((attribute != null) ? attribute.PresentationDataType : null);
				}
				Type presentationDataType = type ?? typeof(string);
				object value = DataItem.Value;
				object secondaryValue = DataItem.SecondaryValue;
				DynamicFieldDTO field2 = DataItem.Field;
				eControlCode controlCode = (field2 != null) ? field2.ControlCode : eControlCode.Unknown;
				DynamicFieldDTO field3 = DataItem.Field;
				int setting = (field3 != null) ? field3.Setting3 : -1;
				string text;
				if (DataItem == null)
				{
					text = null;
				}
				else
				{
					DynamicFieldDTO field4 = DataItem.Field;
					text = ((field4 != null) ? field4.ControlCaption : null);
				}
				result = DynamicFormsAdapter.GetValueForPresentation(value, secondaryValue, controlCode, setting, (text ?? "").GetCaptionForDisplay(), presentationDataType);
			}
			return result;
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x0002184C File Offset: 0x0001FA4C
		public static string GetValueDisplay(this DynamicDataDTO DynamicData)
		{
			bool flag = DynamicData == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = DynamicData.Field == null;
				if (flag2)
				{
					result = ((DynamicData.Value == null) ? "" : DynamicData.Value.ToString());
				}
				else
				{
					string caption = DynamicData.Field.ControlCaptionForDisplay();
					object secondaryValue = DynamicData.SecondaryValue;
					object value = DynamicData.Value;
					int controlCode = (int)DynamicData.Field.ControlCode;
					bool flag3 = Enum.IsDefined(typeof(eControlCode), controlCode);
					eControlCode controlCode2;
					if (flag3)
					{
						controlCode2 = (eControlCode)controlCode;
					}
					else
					{
						controlCode2 = eControlCode.Unknown;
					}
					int setting = DynamicData.Field.Setting3;
					result = DynamicFormsAdapter.GetString(controlCode2, caption, secondaryValue, value, setting);
				}
			}
			return result;
		}

		// Token: 0x060042C4 RID: 17092 RVA: 0x00021908 File Offset: 0x0001FB08
		public static Forest<DynamicFormOrGroupOrFieldDTO> GetForestFromList(this IList<DynamicFormOrGroupOrFieldDTO> items)
		{
			Forest<DynamicFormOrGroupOrFieldDTO> forest = new Forest<DynamicFormOrGroupOrFieldDTO>();
			bool flag = items.Count < 1;
			Forest<DynamicFormOrGroupOrFieldDTO> result;
			if (flag)
			{
				result = forest;
			}
			else
			{
				List<DynamicFormDTO> list = new List<DynamicFormDTO>();
				Dictionary<int, IList<DynamicFormOrGroupOrFieldDTO>> dictionary = new Dictionary<int, IList<DynamicFormOrGroupOrFieldDTO>>();
				foreach (DynamicFormOrGroupOrFieldDTO dynamicFormOrGroupOrFieldDTO in items)
				{
					DynamicFormDTO dynamicForm = dynamicFormOrGroupOrFieldDTO.DynamicForm;
					int screenNum2 = dynamicForm.ScreenNum;
					bool flag2 = !dictionary.ContainsKey(screenNum2);
					if (flag2)
					{
						list.Add(dynamicForm);
						dictionary.Add(screenNum2, new List<DynamicFormOrGroupOrFieldDTO>());
					}
					dictionary[screenNum2].Add(dynamicFormOrGroupOrFieldDTO);
				}
				Dictionary<string, TreeNode<DynamicFormOrGroupOrFieldDTO>> dictionary2 = new Dictionary<string, TreeNode<DynamicFormOrGroupOrFieldDTO>>();
				foreach (KeyValuePair<int, IList<DynamicFormOrGroupOrFieldDTO>> keyValuePair in dictionary)
				{
					int screenNum = keyValuePair.Key;
					DynamicFormDTO dynamicFormDTO = list.FirstOrDefault((DynamicFormDTO g) => g.ScreenNum == screenNum);
					string text = dynamicFormDTO.GroupName ?? "";
					bool flag3 = text.Length < 1;
					if (flag3)
					{
						text = "General";
					}
					string key = text.ToLower().Trim();
					bool flag4 = !dictionary2.ContainsKey(key);
					if (flag4)
					{
						TreeNode<DynamicFormOrGroupOrFieldDTO> value = forest.AppendNode(null, new DynamicFormOrGroupOrFieldDTO
						{
							GroupName = text
						});
						dictionary2.Add(key, value);
					}
					TreeNode<DynamicFormOrGroupOrFieldDTO> parentNode = dictionary2[key];
					TreeNode<DynamicFormOrGroupOrFieldDTO> formNode = forest.AppendNode(parentNode, new DynamicFormOrGroupOrFieldDTO
					{
						DynamicForm = dynamicFormDTO
					});
					DynamicFormsAdapter.AddFieldsToForest(ref forest, formNode, keyValuePair.Value);
				}
				result = forest;
			}
			return result;
		}

		// Token: 0x060042C5 RID: 17093 RVA: 0x00021AE4 File Offset: 0x0001FCE4
		private static void AddFieldsToForest(ref Forest<DynamicFormOrGroupOrFieldDTO> forest, TreeNode<DynamicFormOrGroupOrFieldDTO> formNode, IList<DynamicFormOrGroupOrFieldDTO> fields)
		{
			Stack<TreeNode<DynamicFormOrGroupOrFieldDTO>> stack = new Stack<TreeNode<DynamicFormOrGroupOrFieldDTO>>();
			foreach (DynamicFormOrGroupOrFieldDTO dynamicFormOrGroupOrFieldDTO in fields)
			{
				TreeNode<DynamicFormOrGroupOrFieldDTO> parentNode = (stack.Count > 0) ? stack.Peek() : formNode;
				DynamicFieldDTO field = dynamicFormOrGroupOrFieldDTO.Field;
				bool flag = field != null;
				if (flag)
				{
					eControlCode controlCode = field.ControlCode;
					DynamicControlAttribute attribute = DynamicControlAttribute.GetAttribute(controlCode);
					bool flag2 = attribute != null && attribute.IsControlCollectionStart;
					if (flag2)
					{
						TreeNode<DynamicFormOrGroupOrFieldDTO> item = forest.AppendNode(parentNode, new DynamicFormOrGroupOrFieldDTO
						{
							GroupName = field.ControlCaptionForDisplay()
						});
						stack.Push(item);
					}
					else
					{
						bool flag3 = attribute != null && attribute.IsControlCollectionEnd;
						if (flag3)
						{
							stack.Pop();
						}
						else
						{
							forest.AppendNode(parentNode, new DynamicFormOrGroupOrFieldDTO
							{
								Field = field
							});
						}
					}
				}
			}
		}
	}
}
