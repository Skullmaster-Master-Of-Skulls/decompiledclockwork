using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000482 RID: 1154
	[Serializable]
	public struct ControlSetting : IXmlSerializable
	{
		// Token: 0x06002930 RID: 10544 RVA: 0x0008467C File Offset: 0x0008287C
		private static void ConvertListOfObjectToHashSetOfArray(ref List<object> outerList)
		{
			int count = outerList.Count;
			for (int i = 0; i < count; i++)
			{
				List<object> list = outerList[i] as List<object>;
				if (list != null)
				{
					if ((from x in list
					where x is List<object>
					select x).Count<object>() <= 0)
					{
						outerList.RemoveAt(i);
						outerList.Insert(i, list.ToArray<object>());
					}
					else
					{
						ControlSetting.ConvertListOfObjectToHashSetOfArray(ref list);
					}
				}
			}
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x000846FC File Offset: 0x000828FC
		private static void SerializeArray(ref XmlWriter writer, Array array)
		{
			foreach (object obj in array)
			{
				if (obj is Array)
				{
					writer.WriteStartElement("InnerArray");
					ControlSetting.SerializeArray(ref writer, (Array)obj);
					writer.WriteEndElement();
				}
				else
				{
					writer.WriteElementString("Value", ControlSetting.SerializeObject(obj));
				}
			}
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x00084780 File Offset: 0x00082980
		private static void DeserializeHashSetContent(XmlReader reader, ref List<object> list)
		{
			if (reader.Name == "Value")
			{
				list.Add(ControlSetting.DeserializeObject(reader.ReadString()));
				reader.ReadEndElement();
			}
			else if (reader.Name == "InnerArray")
			{
				List<object> item = new List<object>();
				reader.Read();
				list.Add(item);
				ControlSetting.DeserializeHashSetContent(reader, ref item);
			}
			if (reader.Name != "HashSetOfArray")
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					bool flag = reader.Name == "InnerArray";
					reader.ReadEndElement();
					if (flag)
					{
						return;
					}
				}
				ControlSetting.DeserializeHashSetContent(reader, ref list);
			}
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x00084828 File Offset: 0x00082A28
		private static string SerializeObject(object val)
		{
			string text = string.Empty;
			XmlSerializer xmlSerializer = new XmlSerializer(val.GetType());
			using (TextWriter textWriter = new StringWriter())
			{
				xmlSerializer.Serialize(textWriter, val);
				text = ControlSetting.StripDocumentNode(textWriter.ToString());
			}
			text = string.Format("{0}||{1}", val.GetType().FullName, text);
			return Convert.ToBase64String(ControlSetting.encoding.GetBytes(text));
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x000848A4 File Offset: 0x00082AA4
		private static object DeserializeObject(string encodedXml)
		{
			object result = null;
			byte[] bytes = Convert.FromBase64String(encodedXml);
			string[] array = ControlSetting.encoding.GetString(bytes).Split(new string[]
			{
				"||"
			}, StringSplitOptions.RemoveEmptyEntries);
			XmlSerializer xmlSerializer = new XmlSerializer(Type.GetType(array[0]));
			using (TextReader textReader = new StringReader(array[1]))
			{
				result = xmlSerializer.Deserialize(textReader);
			}
			return result;
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x00084920 File Offset: 0x00082B20
		private static string StripDocumentNode(string p)
		{
			int num = p.IndexOf('\n') + 1;
			return p.Substring(num, p.Length - num);
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06002936 RID: 10550 RVA: 0x00084947 File Offset: 0x00082B47
		// (set) Token: 0x06002937 RID: 10551 RVA: 0x0008494F File Offset: 0x00082B4F
		public string Name { get; set; }

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06002938 RID: 10552 RVA: 0x00084958 File Offset: 0x00082B58
		// (set) Token: 0x06002939 RID: 10553 RVA: 0x00084960 File Offset: 0x00082B60
		public object Value { get; set; }

		// Token: 0x0600293A RID: 10554 RVA: 0x00084969 File Offset: 0x00082B69
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x0008496C File Offset: 0x00082B6C
		public void ReadXml(XmlReader reader)
		{
			reader.Read();
			reader.ReadStartElement("Name");
			this.Name = reader.ReadString();
			reader.ReadEndElement();
			string text = reader.GetAttribute("Type");
			if (!string.IsNullOrEmpty(text))
			{
				if (text.Contains("Telerik.Web.UI"))
				{
					text = new Regex(ControlSetting.fqnRegex).Replace(text, string.Empty);
				}
				Type type = Type.GetType(text);
				reader.ReadStartElement("Value");
				if (reader.Name == "Unit")
				{
					this.Value = Unit.Parse(reader.ReadString());
				}
				else if (reader.Name == "ArrayOfUnit")
				{
					reader.ReadStartElement("ArrayOfUnit");
					List<Unit> list = new List<Unit>();
					new XmlSerializer(typeof(Unit));
					while (reader.Name == "Unit")
					{
						list.Add(Unit.Parse(reader.ReadString()));
						if (reader.IsEmptyElement)
						{
							reader.Read();
						}
						else
						{
							reader.ReadEndElement();
						}
					}
					if (text.Contains("System.Collections.Generic.List"))
					{
						this.Value = list;
					}
					else
					{
						this.Value = list.ToArray();
					}
					reader.ReadEndElement();
				}
				else if (reader.Name == "Color")
				{
					int alpha = Convert.ToInt32(reader.GetAttribute("alpha"));
					int red = Convert.ToInt32(reader.GetAttribute("red"));
					int green = Convert.ToInt32(reader.GetAttribute("green"));
					int blue = Convert.ToInt32(reader.GetAttribute("blue"));
					this.Value = Color.FromArgb(alpha, red, green, blue);
				}
				else if (reader.Name == "HashSetOfArray")
				{
					if (!reader.IsEmptyElement)
					{
						reader.ReadStartElement("HashSetOfArray");
						HashSet<Array> hashSet = new HashSet<Array>(new ControlSetting.ArrayComparer());
						List<object> list2 = new List<object>();
						ControlSetting.DeserializeHashSetContent(reader, ref list2);
						ControlSetting.ConvertListOfObjectToHashSetOfArray(ref list2);
						foreach (object obj in list2)
						{
							object[] item = (object[])obj;
							hashSet.Add(item);
						}
						this.Value = hashSet;
					}
				}
				else if (reader.Name == "ArrayOfGridSortExpression")
				{
					GridSortExpressionCollection gridSortExpressionCollection = new GridSortExpressionCollection();
					GridSortExpression gridSortExpression = new GridSortExpression();
					if (!reader.IsEmptyElement)
					{
						reader.Read();
					}
					while (reader.Name == "GridSortExpression")
					{
						gridSortExpression = new GridSortExpression();
						reader.Read();
						if (reader.Name == "FieldName")
						{
							gridSortExpression.FieldName = reader.ReadElementContentAsString();
						}
						if (reader.Name == "SortOrder")
						{
							gridSortExpression.SortOrder = (GridSortOrder)Enum.Parse(typeof(GridSortOrder), reader.ReadElementContentAsString());
						}
						gridSortExpressionCollection.AddSortExpression(gridSortExpression);
						reader.ReadEndElement();
						if (gridSortExpressionCollection.Count > 0 && !gridSortExpressionCollection.AllowMultiColumnSorting)
						{
							gridSortExpressionCollection.AllowMultiColumnSorting = true;
						}
					}
					this.Value = gridSortExpressionCollection;
				}
				else if (reader.Name == "ArrayOfGridGroupByExpression")
				{
					GridGroupByExpressionCollection gridGroupByExpressionCollection = new GridGroupByExpressionCollection();
					if (!reader.IsEmptyElement)
					{
						reader.Read();
						while (!(reader.Name == "ArrayOfGridGroupByExpression") || reader.NodeType != XmlNodeType.EndElement)
						{
							this.ReadGroupExpressions(gridGroupByExpressionCollection, reader);
						}
					}
					this.Value = gridGroupByExpressionCollection;
				}
				else
				{
					XmlSerializer xmlSerializer = new XmlSerializer(type);
					this.Value = xmlSerializer.Deserialize(reader.ReadSubtree());
				}
				if (reader.IsEmptyElement)
				{
					reader.Read();
				}
				else
				{
					reader.ReadEndElement();
				}
				reader.ReadEndElement();
				reader.ReadEndElement();
			}
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x00084D4C File Offset: 0x00082F4C
		private void ReadGroupExpressions(GridGroupByExpressionCollection groupExpressions, XmlReader reader)
		{
			if (reader.Name == "GridGroupByExpression")
			{
				GridGroupByExpression gridGroupByExpression = new GridGroupByExpression();
				reader.Read();
				if (!reader.IsEmptyElement)
				{
					this.ReadInnerFields(gridGroupByExpression, reader);
				}
				reader.Read();
				if (!reader.IsEmptyElement)
				{
					this.ReadInnerFields(gridGroupByExpression, reader);
				}
				reader.Read();
				if (reader.Name == "Expression")
				{
					reader.Skip();
				}
				groupExpressions.Add(gridGroupByExpression);
				reader.Read();
			}
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x00084DD0 File Offset: 0x00082FD0
		private void ReadInnerFields(GridGroupByExpression groupExpression, XmlReader reader)
		{
			bool flag = reader.Name == "SelectFields";
			reader.Read();
			while ((!(reader.Name == "GroupByFields") && !(reader.Name == "SelectFields")) || reader.NodeType != XmlNodeType.EndElement)
			{
				this.ReadGroupByFields(flag ? groupExpression.SelectFields : groupExpression.GroupByFields, reader);
			}
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x00084E40 File Offset: 0x00083040
		private void ReadGroupByFields(GridGroupByFieldList gridGroupByFieldList, XmlReader reader)
		{
			GridGroupByField gridGroupByField = new GridGroupByField();
			reader.Read();
			while (!(reader.Name == "GridGroupByField") || reader.NodeType != XmlNodeType.EndElement)
			{
				string name;
				switch (name = reader.Name)
				{
				case "Aggregate":
					gridGroupByField.Aggregate = (GridAggregateFunction)Enum.Parse(typeof(GridAggregateFunction), reader.ReadElementContentAsString());
					break;
				case "FieldAlias":
					gridGroupByField.FieldAlias = reader.ReadElementContentAsString();
					break;
				case "FieldName":
					gridGroupByField.FieldName = reader.ReadElementContentAsString();
					break;
				case "FormatString":
					gridGroupByField.FormatString = reader.ReadElementContentAsString();
					break;
				case "HeaderText":
					gridGroupByField.HeaderText = reader.ReadElementContentAsString();
					break;
				case "HeaderValueSeparator":
					gridGroupByField.HeaderValueSeparator = reader.ReadElementContentAsString();
					break;
				case "SortOrder":
					gridGroupByField.SortOrder = (GridSortOrder)Enum.Parse(typeof(GridSortOrder), reader.ReadElementContentAsString());
					break;
				}
			}
			gridGroupByFieldList.Add(gridGroupByField);
			reader.Read();
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x00084FCC File Offset: 0x000831CC
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteElementString("Name", this.Name);
			writer.WriteStartElement("Value");
			writer.WriteAttributeString("Type", this.Value.GetType().FullName);
			if (this.Value.GetType() == typeof(Unit))
			{
				writer.WriteStartElement("Unit");
				writer.WriteString(this.Value.ToString());
				writer.WriteEndElement();
			}
			else if (this.Value.GetType() == typeof(Unit[]) || this.Value.GetType() == typeof(List<Unit>))
			{
				Unit[] array;
				if (this.Value.GetType() == typeof(Unit[]))
				{
					array = (Unit[])this.Value;
				}
				else
				{
					array = (this.Value as List<Unit>).ToArray();
				}
				writer.WriteStartElement("ArrayOfUnit");
				foreach (Unit unit in array)
				{
					writer.WriteStartElement("Unit");
					writer.WriteString(unit.ToString());
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
			else if (this.Value.GetType() == typeof(Color))
			{
				Color color = (Color)this.Value;
				writer.WriteStartElement("Color");
				writer.WriteAttributeString("alpha", color.A.ToString());
				writer.WriteAttributeString("red", color.R.ToString());
				writer.WriteAttributeString("green", color.G.ToString());
				writer.WriteAttributeString("blue", color.B.ToString());
				writer.WriteEndElement();
			}
			else if (this.Value.GetType() == typeof(HashSet<Array>))
			{
				HashSet<Array> hashSet = this.Value as HashSet<Array>;
				writer.WriteStartElement("HashSetOfArray");
				foreach (Array array3 in hashSet)
				{
					writer.WriteStartElement("InnerArray");
					ControlSetting.SerializeArray(ref writer, array3);
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
			else
			{
				XmlSerializer xmlSerializer = new XmlSerializer(this.Value.GetType());
				xmlSerializer.Serialize(writer, this.Value);
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000A79 RID: 2681
		private static Encoding encoding = Encoding.Unicode;

		// Token: 0x04000A7A RID: 2682
		public static string fqnRegex = ",\\s{1}Telerik\\.Web\\.UI,\\s{1}Version=\\d+\\.\\d+\\.\\d+\\.\\d+,\\s{1}Culture=.+,\\s{1}PublicKeyToken=121fae78165ba3d4";

		// Token: 0x02000483 RID: 1155
		[Serializable]
		private class ArrayComparer : IEqualityComparer<Array>
		{
			// Token: 0x06002942 RID: 10562 RVA: 0x000852A4 File Offset: 0x000834A4
			public bool Equals(Array x, Array y)
			{
				if (x.Length != y.Length)
				{
					return false;
				}
				for (int i = 0; i < x.Length; i++)
				{
					if (!object.Equals(x.GetValue(i), y.GetValue(i)))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06002943 RID: 10563 RVA: 0x000852EC File Offset: 0x000834EC
			public int GetHashCode(Array obj)
			{
				int num = obj.Length;
				for (int i = 0; i < obj.Length; i++)
				{
					num = num * 2903 + obj.GetValue(i).GetHashCode();
				}
				return num;
			}
		}
	}
}
