using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls;
using TechnoPro.Common.Converter.CustomFormControls.Serializers;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.Common.Converter.CustomFormControls
{
	// Token: 0x02000014 RID: 20
	public static class CustomControlBuilderFactory
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00003B68 File Offset: 0x00001D68
		public static string GetCustomControlTagForXml<T>(this T ctrl) where T : CustomControlBaseDTO
		{
			T t = ctrl;
			string result;
			if (t == null)
			{
				result = null;
			}
			else
			{
				CustomControlTypeAttribute attribute = t.CustomControlType.GetAttribute<CustomControlTypeAttribute>();
				result = ((attribute != null) ? attribute.ControlCode : null);
			}
			return result;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003BA4 File Offset: 0x00001DA4
		public static string SerializeControlForest<TN>(this IEnumerable<TN> forestFirstLevelNodes, Guid formId, Func<TN, CustomControlBaseDTO> GetCustomControlFromNode, Func<TN, IEnumerable<TN>> GetChildNodesFromNode) where TN : class
		{
			XElement xelement = new XElement("form", new XAttribute("id", formId.ToString()));
			XDocument xdocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new object[]
			{
				xelement
			});
			bool flag = forestFirstLevelNodes != null;
			if (flag)
			{
				CustomControlBuilderFactory.SerializeControlForest<TN>(formId, forestFirstLevelNodes, xelement, GetCustomControlFromNode, GetChildNodesFromNode);
			}
			return xdocument.Declaration.ToString() + xdocument.ToString();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003C30 File Offset: 0x00001E30
		private static void SerializeControlForest<TN>(Guid formId, IEnumerable<TN> parentCollection, XElement parentXElement, Func<TN, CustomControlBaseDTO> GetCustomControlFromNode, Func<TN, IEnumerable<TN>> GetChildNodesFromNode) where TN : class
		{
			foreach (TN arg in parentCollection)
			{
				CustomControlBaseDTO customControlBaseDTO = GetCustomControlFromNode(arg);
				XElement xelement = customControlBaseDTO.CustomControlType.GetAttribute<CustomControlTypeAttribute>().ControlCode.GetSerializer().SerializeItem(formId, customControlBaseDTO);
				parentXElement.Add(xelement);
				IEnumerable<TN> parentCollection2 = GetChildNodesFromNode(arg);
				CustomControlBuilderFactory.SerializeControlForest<TN>(formId, parentCollection2, xelement, GetCustomControlFromNode, GetChildNodesFromNode);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003CC0 File Offset: 0x00001EC0
		public static string SerializeControlForest(this Forest<CustomControlBaseDTO> forest, Guid formId)
		{
			XElement xelement = new XElement("form", new XAttribute("id", formId.ToString()));
			XDocument xdocument = new XDocument(new object[]
			{
				xelement
			});
			bool flag = forest != null;
			if (flag)
			{
				CustomControlBuilderFactory.SerializeControlForest(formId, forest.Nodes, xelement);
			}
			return xdocument.Declaration.ToString() + xdocument.ToString();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003D3C File Offset: 0x00001F3C
		private static void SerializeControlForest(Guid formId, TreeNodeCollection<CustomControlBaseDTO> parentCollection, XElement parentXElement)
		{
			foreach (TreeNode<CustomControlBaseDTO> treeNode in parentCollection)
			{
				XElement xelement = treeNode.Value.CustomControlType.GetAttribute<CustomControlTypeAttribute>().ControlCode.GetSerializer().SerializeItem(formId, treeNode.Value);
				parentXElement.Add(xelement);
				bool flag = treeNode.Nodes.Count > 0;
				if (flag)
				{
					CustomControlBuilderFactory.SerializeControlForest(formId, treeNode.Nodes, xelement);
				}
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003DD8 File Offset: 0x00001FD8
		public static Forest<CustomControlBaseDTO> ExtractControlForest(this string formXml, out Guid formId)
		{
			return formXml.ExtractControlForest(out formId, (CustomControlBaseDTO g) => g);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003E10 File Offset: 0x00002010
		public static Forest<T> ExtractControlForest<T>(this string formXml, out Guid formId, Func<CustomControlBaseDTO, T> createNodeItem) where T : class
		{
			Forest<T> forest = new Forest<T>();
			bool flag = string.IsNullOrEmpty(formXml);
			Forest<T> result;
			if (flag)
			{
				formId = Guid.Empty;
				result = forest;
			}
			else
			{
				TextReader textReader = new StringReader(formXml);
				XDocument xdocument = XDocument.Load(textReader);
				XElement xelement = xdocument.Element("form");
				string text;
				if (xelement == null)
				{
					text = null;
				}
				else
				{
					XAttribute xattribute = xelement.Attribute("id");
					text = ((xattribute != null) ? xattribute.Value : null);
				}
				string text2 = text ?? string.Empty;
				formId = (string.IsNullOrEmpty(text2) ? Guid.Empty : new Guid(text2));
				CustomControlBuilderFactory.ExtractControlForest<T>(formId, ref forest, xelement, null, createNodeItem);
				result = forest;
			}
			return result;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003EC4 File Offset: 0x000020C4
		private static void ExtractControlForest<T>(Guid formId, ref Forest<T> forest, XElement parentElement, TreeNode<T> parentNode, Func<CustomControlBaseDTO, T> createNodeItem) where T : class
		{
			foreach (XElement xelement in parentElement.Elements())
			{
				string controlTag = xelement.Name.ToString();
				CustomControlBaseDTO customControlBaseDTO = controlTag.GetSerializer().DeSerializeItem(formId, xelement);
				bool flag = customControlBaseDTO == null;
				if (!flag)
				{
					bool flag2 = customControlBaseDTO is CustomControlContainerDTO;
					if (flag2)
					{
						TreeNode<T> parentNode2 = forest.AppendNode(parentNode, createNodeItem(customControlBaseDTO));
						CustomControlBuilderFactory.ExtractControlForest<T>(formId, ref forest, xelement, parentNode2, createNodeItem);
					}
					else
					{
						forest.AppendNode(parentNode, createNodeItem(customControlBaseDTO));
					}
				}
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003F78 File Offset: 0x00002178
		public static void ExtractControlForest<F, FN, C>(this string formXml, ref F forest, out Guid formId, Func<FN, CustomControlBaseDTO, FN> createNode) where F : class where FN : class where C : class
		{
			bool flag = string.IsNullOrEmpty(formXml);
			if (flag)
			{
				formId = Guid.Empty;
			}
			else
			{
				TextReader textReader = new StringReader(formXml);
				XDocument xdocument = XDocument.Load(textReader);
				XElement xelement = xdocument.Element("form");
				string text;
				if (xelement == null)
				{
					text = null;
				}
				else
				{
					XAttribute xattribute = xelement.Attribute("id");
					text = ((xattribute != null) ? xattribute.Value : null);
				}
				string text2 = text ?? string.Empty;
				formId = (string.IsNullOrEmpty(text2) ? Guid.Empty : new Guid(text2));
				CustomControlBuilderFactory.ExtractControlForest<F, FN, C>(formId, ref forest, xelement, default(FN), createNode);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004020 File Offset: 0x00002220
		private static void ExtractControlForest<F, FN, C>(Guid formId, ref F forest, XElement parentElement, FN parentNode, Func<FN, CustomControlBaseDTO, FN> createNode) where F : class where FN : class where C : class
		{
			foreach (XElement xelement in parentElement.Elements())
			{
				string controlTag = xelement.Name.ToString();
				CustomControlBaseDTO customControlBaseDTO = controlTag.GetSerializer().DeSerializeItem(formId, xelement);
				bool flag = customControlBaseDTO == null;
				if (!flag)
				{
					bool flag2 = customControlBaseDTO is CustomControlContainerDTO;
					if (flag2)
					{
						FN parentNode2 = createNode(parentNode, customControlBaseDTO);
						CustomControlBuilderFactory.ExtractControlForest<F, FN, C>(formId, ref forest, xelement, parentNode2, createNode);
					}
					else
					{
						createNode(parentNode, customControlBaseDTO);
					}
				}
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000040C8 File Offset: 0x000022C8
		public static bool IsCustomContainer(this string controlType)
		{
			Type left = CustomControlBuilderFactory.CustomControlDefinitionList.FirstOrDefault(delegate(Type t)
			{
				CustomControlBaseAttribute customAttribute = t.GetCustomAttribute<CustomControlBaseAttribute>();
				bool? flag;
				if (customAttribute == null)
				{
					flag = null;
				}
				else
				{
					CustomControlTypeAttribute attribute = customAttribute.ControlType.GetAttribute<CustomControlTypeAttribute>();
					if (attribute == null)
					{
						flag = null;
					}
					else
					{
						string controlCode = attribute.ControlCode;
						flag = ((controlCode != null) ? new bool?(controlCode.Equals(controlType, StringComparison.OrdinalIgnoreCase)) : null);
					}
				}
				bool? flag2 = flag;
				return flag2.GetValueOrDefault();
			});
			return left == typeof(CustomGroupBoxDTO);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004110 File Offset: 0x00002310
		public static ICustomControlSerializer GetSerializer(this string controlTag)
		{
			Type type = CustomControlBuilderFactory.CustomControlDefinitionList.FirstOrDefault(delegate(Type t)
			{
				CustomControlBaseAttribute customAttribute = t.GetCustomAttribute<CustomControlBaseAttribute>();
				bool? flag14;
				if (customAttribute == null)
				{
					flag14 = null;
				}
				else
				{
					CustomControlTypeAttribute attribute = customAttribute.ControlType.GetAttribute<CustomControlTypeAttribute>();
					flag14 = ((attribute != null) ? new bool?(attribute.ControlCode.Equals(controlTag, StringComparison.OrdinalIgnoreCase)) : null);
				}
				bool? flag15 = flag14;
				return flag15.GetValueOrDefault();
			});
			bool flag = type == typeof(CustomGroupBoxDTO);
			ICustomControlSerializer result;
			if (flag)
			{
				result = new CustomGroupBoxSerializer();
			}
			else
			{
				bool flag2 = type == typeof(CustomCheckBoxDTO);
				if (flag2)
				{
					result = new CustomCheckBoxSerializer();
				}
				else
				{
					bool flag3 = type == typeof(CustomDropListDTO);
					if (flag3)
					{
						result = new CustomDropListSerializer();
					}
					else
					{
						bool flag4 = type == typeof(CustomLabelDTO);
						if (flag4)
						{
							result = new CustomLabelSerializer();
						}
						else
						{
							bool flag5 = type == typeof(CustomTextBoxDTO);
							if (flag5)
							{
								result = new CustomTextBoxSerializer();
							}
							else
							{
								bool flag6 = type == typeof(CustomTextBoxNumberDTO);
								if (flag6)
								{
									result = new CustomTextBoxNumberSerializer();
								}
								else
								{
									bool flag7 = type == typeof(CustomRadioGroupDTO);
									if (flag7)
									{
										result = new CustomRadioGroupSerializer();
									}
									else
									{
										bool flag8 = type == typeof(CustomSingleFileDTO);
										if (flag8)
										{
											result = new CustomSingleFileSerializer();
										}
										else
										{
											bool flag9 = type == typeof(CustomGroupBoxPopupDTO);
											if (flag9)
											{
												result = new CustomGroupBoxPopupSerializer();
											}
											else
											{
												bool flag10 = type == typeof(CustomRecaptchaDTO);
												if (flag10)
												{
													result = new CustomRecaptchaSerializer();
												}
												else
												{
													bool flag11 = type == typeof(CustomYesNoChooserDTO);
													if (flag11)
													{
														result = new CustomYesNoChooserSerializer();
													}
													else
													{
														bool flag12 = type == typeof(CustomTextBoxListDTO);
														if (flag12)
														{
															result = new CustomTextBoxListSerializer();
														}
														else
														{
															try
															{
																string text = type.ToString();
																int num = text.LastIndexOf('.');
																string text2 = (num > 0) ? text.Substring(num + 1) : text;
																bool flag13 = text2.Length > 3;
																if (flag13)
																{
																	text2 = text2.Substring(0, text2.Length - 3);
																}
																string typeName = "TechnoPro.Common.Converter.CustomFormControls.Serializers." + text2 + "Serializer";
																Type type2 = Type.GetType(typeName);
																return (ICustomControlSerializer)Activator.CreateInstance(type2);
															}
															catch
															{
															}
															result = null;
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x04000001 RID: 1
		public static Type[] CustomControlDefinitionList = "TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls".FindTypesByNamespace(typeof(CustomControlBaseDTO)).ToArray<Type>();
	}
}
