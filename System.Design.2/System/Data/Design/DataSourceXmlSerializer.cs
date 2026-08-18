using System;
using System.Collections;
using System.ComponentModel;
using System.Design;
using System.Xml;

namespace System.Data.Design
{
	// Token: 0x0200022B RID: 555
	internal class DataSourceXmlSerializer
	{
		// Token: 0x06001499 RID: 5273 RVA: 0x000760F8 File Offset: 0x000742F8
		internal DataSourceXmlSerializer()
		{
			this.objectNeedBeInitialized = new Queue();
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x00076118 File Offset: 0x00074318
		private Hashtable NameToType
		{
			get
			{
				if (DataSourceXmlSerializer.nameToType == null)
				{
					DataSourceXmlSerializer.nameToType = new Hashtable();
					DataSourceXmlSerializer.nameToType.Add("DbSource", typeof(DbSource));
					DataSourceXmlSerializer.nameToType.Add("Connection", typeof(DesignConnection));
					DataSourceXmlSerializer.nameToType.Add("TableAdapter", typeof(DesignTable));
					DataSourceXmlSerializer.nameToType.Add("DbCommand", typeof(DbSourceCommand));
					DataSourceXmlSerializer.nameToType.Add("Parameter", typeof(DesignParameter));
				}
				return DataSourceXmlSerializer.nameToType;
			}
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x000761BC File Offset: 0x000743BC
		private object CreateObject(string tagName)
		{
			if (tagName == "DbTable")
			{
				tagName = "TableAdapter";
			}
			if (!this.NameToType.Contains(tagName))
			{
				throw new DataSourceSerializationException(SR.GetString("DTDS_CouldNotDeserializeXmlElement", new object[]
				{
					tagName
				}));
			}
			Type type = (Type)this.NameToType[tagName];
			return Activator.CreateInstance(type);
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x00076220 File Offset: 0x00074420
		internal object Deserialize(XmlElement xmlElement)
		{
			object obj = this.CreateObject(xmlElement.LocalName);
			if (obj is IDataSourceXmlSerializable)
			{
				((IDataSourceXmlSerializable)obj).ReadXml(xmlElement, this);
			}
			else
			{
				this.DeserializeBody(xmlElement, obj);
			}
			if (obj is IDataSourceInitAfterLoading)
			{
				this.objectNeedBeInitialized.Enqueue(obj);
			}
			return obj;
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x00076270 File Offset: 0x00074470
		internal void DeserializeBody(XmlElement xmlElement, object obj)
		{
			DataSourceXmlSerializer.PropertySerializationInfo serializationInfo = this.GetSerializationInfo(obj.GetType());
			IDataSourceXmlSpecialOwner dataSourceXmlSpecialOwner = obj as IDataSourceXmlSpecialOwner;
			foreach (DataSourceXmlSerializer.XmlSerializableProperty xmlSerializableProperty in serializationInfo.AttributeProperties)
			{
				DataSourceXmlAttributeAttribute dataSourceXmlAttributeAttribute = xmlSerializableProperty.SerializationAttribute as DataSourceXmlAttributeAttribute;
				if (dataSourceXmlAttributeAttribute != null)
				{
					XmlAttribute xmlAttribute = xmlElement.Attributes[xmlSerializableProperty.Name];
					if (xmlAttribute != null)
					{
						PropertyDescriptor propertyDescriptor = xmlSerializableProperty.PropertyDescriptor;
						if (dataSourceXmlAttributeAttribute.SpecialWay)
						{
							dataSourceXmlSpecialOwner.ReadSpecialItem(propertyDescriptor.Name, xmlAttribute, this);
						}
						else
						{
							Type propertyType = xmlSerializableProperty.PropertyType;
							object obj2;
							if (propertyType == typeof(string))
							{
								obj2 = xmlAttribute.InnerText;
							}
							else
							{
								obj2 = TypeDescriptor.GetConverter(propertyType).ConvertFromString(xmlAttribute.InnerText);
							}
							if (obj2 != null)
							{
								propertyDescriptor.SetValue(obj, obj2);
							}
						}
					}
				}
			}
			foreach (object obj3 in xmlElement.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj3;
				XmlElement xmlElement2 = xmlNode as XmlElement;
				if (xmlElement2 != null)
				{
					DataSourceXmlSerializer.XmlSerializableProperty serializablePropertyWithElementName = serializationInfo.GetSerializablePropertyWithElementName(xmlElement2.LocalName);
					if (serializablePropertyWithElementName != null)
					{
						PropertyDescriptor propertyDescriptor2 = serializablePropertyWithElementName.PropertyDescriptor;
						DataSourceXmlSerializationAttribute serializationAttribute = serializablePropertyWithElementName.SerializationAttribute;
						if (serializationAttribute is DataSourceXmlElementAttribute)
						{
							DataSourceXmlElementAttribute dataSourceXmlElementAttribute = (DataSourceXmlElementAttribute)serializationAttribute;
							bool specialWay = serializationAttribute.SpecialWay;
							if (specialWay)
							{
								dataSourceXmlSpecialOwner.ReadSpecialItem(propertyDescriptor2.Name, xmlElement2, this);
								continue;
							}
							if (this.NameToType.Contains(xmlElement2.LocalName))
							{
								object value = this.Deserialize(xmlElement2);
								propertyDescriptor2.SetValue(obj, value);
								continue;
							}
							Type propertyType2 = serializablePropertyWithElementName.PropertyType;
							try
							{
								object obj2;
								if (propertyType2 == typeof(string))
								{
									obj2 = xmlElement2.InnerText;
								}
								else
								{
									obj2 = TypeDescriptor.GetConverter(propertyType2).ConvertFromString(xmlElement2.InnerText);
								}
								propertyDescriptor2.SetValue(obj, obj2);
								continue;
							}
							catch (Exception ex)
							{
								continue;
							}
						}
						DataSourceXmlSubItemAttribute dataSourceXmlSubItemAttribute = (DataSourceXmlSubItemAttribute)serializationAttribute;
						if (typeof(IList).IsAssignableFrom(propertyDescriptor2.PropertyType))
						{
							IList list = propertyDescriptor2.GetValue(obj) as IList;
							using (IEnumerator enumerator2 = xmlElement2.ChildNodes.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									object obj4 = enumerator2.Current;
									XmlNode xmlNode2 = (XmlNode)obj4;
									XmlElement xmlElement3 = xmlNode2 as XmlElement;
									if (xmlElement3 != null)
									{
										object value2 = this.Deserialize(xmlElement3);
										list.Add(value2);
									}
								}
								continue;
							}
						}
						for (XmlNode xmlNode3 = xmlElement2.FirstChild; xmlNode3 != null; xmlNode3 = xmlNode3.NextSibling)
						{
							if (xmlNode3 is XmlElement)
							{
								object value3 = this.Deserialize((XmlElement)xmlNode3);
								propertyDescriptor2.SetValue(obj, value3);
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x00076594 File Offset: 0x00074794
		private DataSourceXmlSerializer.PropertySerializationInfo GetSerializationInfo(Type type)
		{
			if (DataSourceXmlSerializer.propertySerializationInfoHash == null)
			{
				DataSourceXmlSerializer.propertySerializationInfoHash = new Hashtable();
			}
			if (DataSourceXmlSerializer.propertySerializationInfoHash.Contains(type))
			{
				return (DataSourceXmlSerializer.PropertySerializationInfo)DataSourceXmlSerializer.propertySerializationInfoHash[type];
			}
			DataSourceXmlSerializer.PropertySerializationInfo propertySerializationInfo = new DataSourceXmlSerializer.PropertySerializationInfo(type);
			DataSourceXmlSerializer.propertySerializationInfoHash.Add(type, propertySerializationInfo);
			return propertySerializationInfo;
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x000765E4 File Offset: 0x000747E4
		internal void InitializeObjects()
		{
			int count = this.objectNeedBeInitialized.Count;
			while (count-- > 0)
			{
				IDataSourceInitAfterLoading dataSourceInitAfterLoading = (IDataSourceInitAfterLoading)this.objectNeedBeInitialized.Dequeue();
				dataSourceInitAfterLoading.InitializeAfterLoading();
			}
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00076620 File Offset: 0x00074820
		internal void Serialize(XmlWriter xmlWriter, object obj)
		{
			if (obj is IDataSourceXmlSerializable)
			{
				((IDataSourceXmlSerializable)obj).WriteXml(xmlWriter, this);
				return;
			}
			Type type = obj.GetType();
			string text = null;
			AttributeCollection attributes = TypeDescriptor.GetAttributes(type);
			DataSourceXmlClassAttribute dataSourceXmlClassAttribute = attributes[typeof(DataSourceXmlClassAttribute)] as DataSourceXmlClassAttribute;
			if (dataSourceXmlClassAttribute != null)
			{
				text = dataSourceXmlClassAttribute.Name;
			}
			if (text == null)
			{
				text = type.Name;
			}
			xmlWriter.WriteStartElement(string.Empty, text, this.nameSpace);
			this.SerializeBody(xmlWriter, obj);
			xmlWriter.WriteFullEndElement();
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x000766A0 File Offset: 0x000748A0
		internal void SerializeBody(XmlWriter xmlWriter, object obj)
		{
			PropertyDescriptorCollection propertyDescriptorCollection;
			if (obj is ICustomTypeDescriptor)
			{
				propertyDescriptorCollection = ((ICustomTypeDescriptor)obj).GetProperties();
			}
			else
			{
				propertyDescriptorCollection = TypeDescriptor.GetProperties(obj);
			}
			propertyDescriptorCollection = propertyDescriptorCollection.Sort();
			ArrayList arrayList = new ArrayList();
			IDataSourceXmlSpecialOwner dataSourceXmlSpecialOwner = obj as IDataSourceXmlSpecialOwner;
			foreach (object obj2 in propertyDescriptorCollection)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
				DataSourceXmlSerializationAttribute dataSourceXmlSerializationAttribute = (DataSourceXmlSerializationAttribute)propertyDescriptor.Attributes[typeof(DataSourceXmlSerializationAttribute)];
				if (dataSourceXmlSerializationAttribute != null)
				{
					if (dataSourceXmlSerializationAttribute is DataSourceXmlAttributeAttribute)
					{
						DataSourceXmlAttributeAttribute dataSourceXmlAttributeAttribute = (DataSourceXmlAttributeAttribute)dataSourceXmlSerializationAttribute;
						object value = propertyDescriptor.GetValue(obj);
						if (value != null)
						{
							string name = dataSourceXmlAttributeAttribute.Name;
							if (name == null)
							{
								name = propertyDescriptor.Name;
							}
							if (dataSourceXmlAttributeAttribute.SpecialWay)
							{
								xmlWriter.WriteStartAttribute(string.Empty, name, string.Empty);
								dataSourceXmlSpecialOwner.WriteSpecialItem(propertyDescriptor.Name, xmlWriter, this);
								xmlWriter.WriteEndAttribute();
							}
							else
							{
								xmlWriter.WriteAttributeString(name, value.ToString());
							}
						}
					}
					else
					{
						arrayList.Add(propertyDescriptor);
					}
				}
			}
			foreach (object obj3 in arrayList)
			{
				PropertyDescriptor propertyDescriptor2 = (PropertyDescriptor)obj3;
				object value2 = propertyDescriptor2.GetValue(obj);
				if (value2 != null)
				{
					DataSourceXmlSerializationAttribute dataSourceXmlSerializationAttribute2 = (DataSourceXmlSerializationAttribute)propertyDescriptor2.Attributes[typeof(DataSourceXmlSerializationAttribute)];
					string name2 = dataSourceXmlSerializationAttribute2.Name;
					if (name2 == null)
					{
						name2 = propertyDescriptor2.Name;
					}
					if (!(dataSourceXmlSerializationAttribute2 is DataSourceXmlElementAttribute))
					{
						DataSourceXmlSubItemAttribute dataSourceXmlSubItemAttribute = (DataSourceXmlSubItemAttribute)dataSourceXmlSerializationAttribute2;
						xmlWriter.WriteStartElement(string.Empty, name2, this.nameSpace);
						if (value2 is ICollection)
						{
							using (IEnumerator enumerator3 = ((ICollection)value2).GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									object obj4 = enumerator3.Current;
									this.Serialize(xmlWriter, obj4);
								}
								goto IL_25A;
							}
							goto IL_251;
						}
						goto IL_251;
						IL_25A:
						xmlWriter.WriteFullEndElement();
						continue;
						IL_251:
						this.Serialize(xmlWriter, value2);
						goto IL_25A;
					}
					DataSourceXmlElementAttribute dataSourceXmlElementAttribute = (DataSourceXmlElementAttribute)dataSourceXmlSerializationAttribute2;
					bool specialWay = dataSourceXmlSerializationAttribute2.SpecialWay;
					if (specialWay)
					{
						xmlWriter.WriteStartElement(string.Empty, name2, this.nameSpace);
						dataSourceXmlSpecialOwner.WriteSpecialItem(propertyDescriptor2.Name, xmlWriter, this);
						xmlWriter.WriteFullEndElement();
					}
					else if (this.NameToType.Contains(name2))
					{
						this.Serialize(xmlWriter, value2);
					}
					else
					{
						xmlWriter.WriteElementString(name2, value2.ToString());
					}
				}
			}
		}

		// Token: 0x04000AE2 RID: 2786
		private static Hashtable nameToType;

		// Token: 0x04000AE3 RID: 2787
		private static Hashtable propertySerializationInfoHash;

		// Token: 0x04000AE4 RID: 2788
		private string nameSpace = "urn:schemas-microsoft-com:xml-msdatasource";

		// Token: 0x04000AE5 RID: 2789
		private Queue objectNeedBeInitialized;

		// Token: 0x020004BB RID: 1211
		private class PropertySerializationInfo
		{
			// Token: 0x06002C2D RID: 11309 RVA: 0x00106EB4 File Offset: 0x001050B4
			internal PropertySerializationInfo(Type type)
			{
				ArrayList arrayList = new ArrayList();
				this.elementProperties = new Hashtable();
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
				foreach (object obj in properties)
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
					DataSourceXmlSerializationAttribute dataSourceXmlSerializationAttribute = (DataSourceXmlSerializationAttribute)propertyDescriptor.Attributes[typeof(DataSourceXmlSerializationAttribute)];
					if (dataSourceXmlSerializationAttribute != null)
					{
						DataSourceXmlSerializer.XmlSerializableProperty xmlSerializableProperty = new DataSourceXmlSerializer.XmlSerializableProperty(dataSourceXmlSerializationAttribute, propertyDescriptor);
						if (dataSourceXmlSerializationAttribute is DataSourceXmlAttributeAttribute)
						{
							arrayList.Add(xmlSerializableProperty);
						}
						else
						{
							this.elementProperties.Add(xmlSerializableProperty.Name, xmlSerializableProperty);
						}
					}
				}
				this.AttributeProperties = (DataSourceXmlSerializer.XmlSerializableProperty[])arrayList.ToArray(typeof(DataSourceXmlSerializer.XmlSerializableProperty));
			}

			// Token: 0x06002C2E RID: 11310 RVA: 0x00106F90 File Offset: 0x00105190
			internal DataSourceXmlSerializer.XmlSerializableProperty GetSerializablePropertyWithElementName(string name)
			{
				if (this.elementProperties.Contains(name))
				{
					return (DataSourceXmlSerializer.XmlSerializableProperty)this.elementProperties[name];
				}
				return null;
			}

			// Token: 0x04001E97 RID: 7831
			internal DataSourceXmlSerializer.XmlSerializableProperty[] AttributeProperties;

			// Token: 0x04001E98 RID: 7832
			private Hashtable elementProperties;
		}

		// Token: 0x020004BC RID: 1212
		private class XmlSerializableProperty
		{
			// Token: 0x06002C2F RID: 11311 RVA: 0x00106FB4 File Offset: 0x001051B4
			internal XmlSerializableProperty(DataSourceXmlSerializationAttribute serializationAttribute, PropertyDescriptor propertyDescriptor)
			{
				this.Name = serializationAttribute.Name;
				if (this.Name == null)
				{
					this.Name = propertyDescriptor.Name;
				}
				this.SerializationAttribute = serializationAttribute;
				this.PropertyDescriptor = propertyDescriptor;
				this.PropertyType = serializationAttribute.ItemType;
				if (this.PropertyType == null)
				{
					this.PropertyType = propertyDescriptor.PropertyType;
				}
			}

			// Token: 0x04001E99 RID: 7833
			internal string Name;

			// Token: 0x04001E9A RID: 7834
			internal DataSourceXmlSerializationAttribute SerializationAttribute;

			// Token: 0x04001E9B RID: 7835
			internal Type PropertyType;

			// Token: 0x04001E9C RID: 7836
			internal PropertyDescriptor PropertyDescriptor;
		}
	}
}
