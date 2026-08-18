using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005CD RID: 1485
	public static class StudentFilesAdapter
	{
		// Token: 0x06002FCD RID: 12237 RVA: 0x0003A2E8 File Offset: 0x000384E8
		public static string ConvertStudentFileCategoriesToXml(this StudentFileCategory[] studentFileCategories)
		{
			bool flag = studentFileCategories == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				XDeclaration declaration = new XDeclaration("1.0", "utf-8", "yes");
				object[] array = new object[1];
				array[0] = new XElement("filecats", studentFileCategories.Select(delegate(StudentFileCategory fileCat)
				{
					XName name = "filecat";
					object[] array2 = new object[3];
					array2[0] = new XAttribute("title", fileCat.Title ?? "");
					array2[1] = new XAttribute("isdisabled", fileCat.IsDisabled.ToString());
					array2[2] = new XElement("fields", (fileCat.Fields ?? new StudentFileCategoryField[0]).Select(delegate(StudentFileCategoryField g)
					{
						XName name2 = "field";
						object[] array3 = new object[5];
						array3[0] = new XAttribute("cid", g.ControlId.ToString());
						array3[1] = new XAttribute("formtype", ((int)g.FormType).ToString());
						array3[2] = new XAttribute("fieldtype", ((int)g.FieldType).ToString());
						array3[3] = new XAttribute("filenamefilter", (g.FilenameFilter ?? "").Trim());
						array3[4] = new XAttribute("notecols", string.Join(",", (from gg in g.NoteColumns ?? new int[0]
						select gg.ToString()).ToArray<string>()));
						return new XElement(name2, array3);
					}).ToArray<object>());
					return new XElement(name, array2);
				}).ToArray<object>());
				XDocument xdocument = new XDocument(declaration, array);
				result = xdocument.Declaration.ToString() + xdocument.ToString();
			}
			return result;
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x0003A37C File Offset: 0x0003857C
		public static StudentFileCategory[] ConvertXmlToStudentFileCategories(this string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			StudentFileCategory[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					IEnumerable<XElement> enumerable = XDocument.Parse(xml).Descendants("filecat");
					StudentFileCategory[] result2;
					if (enumerable == null)
					{
						result2 = null;
					}
					else
					{
						result2 = enumerable.Select(delegate(XElement el)
						{
							StudentFileCategory studentFileCategory = new StudentFileCategory();
							XAttribute xattribute = el.Attribute("title");
							studentFileCategory.Title = ((xattribute != null) ? xattribute.GetStringFromAttribute() : null);
							XAttribute xattribute2 = el.Attribute("isdisabled");
							studentFileCategory.IsDisabled = (xattribute2 != null && xattribute2.GetBoolFromAttribute(false));
							XElement xelement = el.Element("fields");
							StudentFileCategoryField[] fields;
							if (xelement == null)
							{
								fields = null;
							}
							else
							{
								IEnumerable<XElement> enumerable2 = xelement.Elements("field");
								if (enumerable2 == null)
								{
									fields = null;
								}
								else
								{
									fields = enumerable2.Select(delegate(XElement fel)
									{
										StudentFileCategoryField studentFileCategoryField = new StudentFileCategoryField();
										StudentFileCategoryField studentFileCategoryField2 = studentFileCategoryField;
										XAttribute xattribute3 = fel.Attribute("cid");
										studentFileCategoryField2.ControlId = ((xattribute3 != null) ? xattribute3.GetIntFromAttribute(0) : 0);
										StudentFileCategoryField studentFileCategoryField3 = studentFileCategoryField;
										XAttribute xattribute4 = fel.Attribute("fieldtype");
										studentFileCategoryField3.FieldType = ((xattribute4 != null) ? xattribute4.GetEnumFromAttributeInt(eStudentFileCategoryFieldType.Unknown) : eStudentFileCategoryFieldType.Unknown);
										StudentFileCategoryField studentFileCategoryField4 = studentFileCategoryField;
										XAttribute xattribute5 = fel.Attribute("formtype");
										studentFileCategoryField4.FormType = ((xattribute5 != null) ? xattribute5.GetEnumFromAttributeInt(eStudentFileCategoryFormType.Unknown) : eStudentFileCategoryFormType.Unknown);
										StudentFileCategoryField studentFileCategoryField5 = studentFileCategoryField;
										XAttribute xattribute6 = fel.Attribute("filenamefilter");
										string text;
										if (xattribute6 == null)
										{
											text = null;
										}
										else
										{
											string stringFromAttribute = xattribute6.GetStringFromAttribute();
											text = ((stringFromAttribute != null) ? stringFromAttribute.Trim() : null);
										}
										studentFileCategoryField5.FilenameFilter = (text ?? "");
										StudentFileCategoryField studentFileCategoryField6 = studentFileCategoryField;
										XAttribute xattribute7 = fel.Attribute("notecols");
										int[] noteColumns;
										if (xattribute7 == null)
										{
											noteColumns = null;
										}
										else
										{
											string stringFromAttribute2 = xattribute7.GetStringFromAttribute();
											if (stringFromAttribute2 == null)
											{
												noteColumns = null;
											}
											else
											{
												noteColumns = (from n in (from g in stringFromAttribute2.Split(new char[]
												{
													','
												})
												select g.Trim() into h
												where h.Length > 0
												select h).Select(delegate(string m)
												{
													int num;
													return int.TryParse(m, out num) ? num : -1;
												})
												where n >= 0
												select n).ToArray<int>();
											}
										}
										studentFileCategoryField6.NoteColumns = noteColumns;
										return studentFileCategoryField;
									}).ToArray<StudentFileCategoryField>();
								}
							}
							studentFileCategory.Fields = fields;
							return studentFileCategory;
						}).ToArray<StudentFileCategory>();
					}
					return result2;
				}
				catch
				{
				}
				result = null;
			}
			return result;
		}
	}
}
