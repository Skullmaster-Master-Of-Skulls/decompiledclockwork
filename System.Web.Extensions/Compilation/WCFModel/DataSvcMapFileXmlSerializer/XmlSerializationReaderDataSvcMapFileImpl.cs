using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel.DataSvcMapFileXmlSerializer
{
	// Token: 0x0200002F RID: 47
	internal class XmlSerializationReaderDataSvcMapFileImpl : XmlSerializationReader
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x0000BBBC File Offset: 0x00009DBC
		public object Read9_ReferenceGroup()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_ReferenceGroup || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = this.Read8_DataSvcMapFileImpl(true, true);
			}
			else
			{
				base.UnknownNode(null, "urn:schemas-microsoft-com:xml-dataservicemap:ReferenceGroup");
			}
			return result;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000BC2C File Offset: 0x00009E2C
		private DataSvcMapFileImpl Read8_DataSvcMapFileImpl(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_DataSvcMapFileImpl || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			DataSvcMapFileImpl dataSvcMapFileImpl = new DataSvcMapFileImpl();
			List<MetadataSource> metadataSourceList = dataSvcMapFileImpl.MetadataSourceList;
			List<MetadataFile> metadataList = dataSvcMapFileImpl.MetadataList;
			List<ExtensionFile> extensions = dataSvcMapFileImpl.Extensions;
			List<Parameter> parameters = dataSvcMapFileImpl.Parameters;
			bool[] array = new bool[5];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[4] && base.Reader.LocalName == this.id4_ID && base.Reader.NamespaceURI == this.id5_Item)
				{
					dataSvcMapFileImpl.ID = base.Reader.Value;
					array[4] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(dataSvcMapFileImpl, ":ID");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return dataSvcMapFileImpl;
			}
			base.Reader.ReadStartElement();
			int num = 0;
			base.Reader.MoveToContent();
			int num2 = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					switch (num)
					{
					case 0:
						if (base.Reader.LocalName == this.id6_MetadataSources && base.Reader.NamespaceURI == this.id2_Item)
						{
							if (!base.ReadNull())
							{
								List<MetadataSource> metadataSourceList2 = dataSvcMapFileImpl.MetadataSourceList;
								if (metadataSourceList2 == null || base.Reader.IsEmptyElement)
								{
									base.Reader.Skip();
								}
								else
								{
									base.Reader.ReadStartElement();
									base.Reader.MoveToContent();
									int num3 = 0;
									int readerCount2 = base.ReaderCount;
									while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
									{
										if (base.Reader.NodeType == XmlNodeType.Element)
										{
											if (base.Reader.LocalName == this.id7_MetadataSource && base.Reader.NamespaceURI == this.id2_Item)
											{
												if (metadataSourceList2 == null)
												{
													base.Reader.Skip();
												}
												else
												{
													metadataSourceList2.Add(this.Read2_MetadataSource(true, true));
												}
											}
											else
											{
												base.UnknownNode(null, "urn:schemas-microsoft-com:xml-dataservicemap:MetadataSource");
											}
										}
										else
										{
											base.UnknownNode(null, "urn:schemas-microsoft-com:xml-dataservicemap:MetadataSource");
										}
										base.Reader.MoveToContent();
										base.CheckReaderCount(ref num3, ref readerCount2);
									}
									base.ReadEndElement();
								}
							}
						}
						else
						{
							num = 1;
						}
						break;
					case 1:
						if (base.Reader.LocalName == this.id8_Metadata && base.Reader.NamespaceURI == this.id2_Item)
						{
							if (!base.ReadNull())
							{
								List<MetadataFile> metadataList2 = dataSvcMapFileImpl.MetadataList;
								if (metadataList2 == null || base.Reader.IsEmptyElement)
								{
									base.Reader.Skip();
								}
								else
								{
									base.Reader.ReadStartElement();
									base.Reader.MoveToContent();
									int num4 = 0;
									int readerCount3 = base.ReaderCount;
									while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
									{
										if (base.Reader.NodeType == XmlNodeType.Element)
										{
											if (base.Reader.LocalName == this.id9_MetadataFile && base.Reader.NamespaceURI == this.id2_Item)
											{
												if (metadataList2 == null)
												{
													base.Reader.Skip();
												}
												else
												{
													metadataList2.Add(this.Read5_MetadataFile(true, true));
												}
											}
											else
											{
												base.UnknownNode(null, "urn:schemas-microsoft-com:xml-dataservicemap:MetadataFile");
											}
										}
										else
										{
											base.UnknownNode(null, "urn:schemas-microsoft-com:xml-dataservicemap:MetadataFile");
										}
										base.Reader.MoveToContent();
										base.CheckReaderCount(ref num4, ref readerCount3);
									}
									base.ReadEndElement();
								}
							}
						}
						else
						{
							num = 2;
						}
						break;
					case 2:
						if (base.Reader.LocalName == this.id10_Extensions && base.Reader.NamespaceURI == this.id2_Item)
						{
							if (!base.ReadNull())
							{
								List<ExtensionFile> extensions2 = dataSvcMapFileImpl.Extensions;
								if (extensions2 == null || base.Reader.IsEmptyElement)
								{
									base.Reader.Skip();
								}
								else
								{
									base.Reader.ReadStartElement();
									base.Reader.MoveToContent();
									int num5 = 0;
									int readerCount4 = base.ReaderCount;
									while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
									{
										if (base.Reader.NodeType == XmlNodeType.Element)
										{
											if (base.Reader.LocalName == this.id11_ExtensionFile && base.Reader.NamespaceURI == this.id2_Item)
											{
												if (extensions2 == null)
												{
													base.Reader.Skip();
												}
												else
												{
													extensions2.Add(this.Read6_ExtensionFile(true, true));
												}
											}
											else
											{
												base.UnknownNode(null, "urn:schemas-microsoft-com:xml-dataservicemap:ExtensionFile");
											}
										}
										else
										{
											base.UnknownNode(null, "urn:schemas-microsoft-com:xml-dataservicemap:ExtensionFile");
										}
										base.Reader.MoveToContent();
										base.CheckReaderCount(ref num5, ref readerCount4);
									}
									base.ReadEndElement();
								}
							}
						}
						else
						{
							num = 3;
						}
						break;
					case 3:
						if (base.Reader.LocalName == this.id12_Parameters && base.Reader.NamespaceURI == this.id2_Item)
						{
							if (!base.ReadNull())
							{
								List<Parameter> parameters2 = dataSvcMapFileImpl.Parameters;
								if (parameters2 == null || base.Reader.IsEmptyElement)
								{
									base.Reader.Skip();
								}
								else
								{
									base.Reader.ReadStartElement();
									base.Reader.MoveToContent();
									int num6 = 0;
									int readerCount5 = base.ReaderCount;
									while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
									{
										if (base.Reader.NodeType == XmlNodeType.Element)
										{
											if (base.Reader.LocalName == this.id13_Parameter && base.Reader.NamespaceURI == this.id2_Item)
											{
												if (parameters2 == null)
												{
													base.Reader.Skip();
												}
												else
												{
													parameters2.Add(this.Read7_Parameter(true, true));
												}
											}
											else
											{
												base.UnknownNode(null, "urn:schemas-microsoft-com:xml-dataservicemap:Parameter");
											}
										}
										else
										{
											base.UnknownNode(null, "urn:schemas-microsoft-com:xml-dataservicemap:Parameter");
										}
										base.Reader.MoveToContent();
										base.CheckReaderCount(ref num6, ref readerCount5);
									}
									base.ReadEndElement();
								}
							}
						}
						else
						{
							num = 4;
						}
						break;
					default:
						base.UnknownNode(dataSvcMapFileImpl, null);
						break;
					}
				}
				else
				{
					base.UnknownNode(dataSvcMapFileImpl, null);
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num2, ref readerCount);
			}
			base.ReadEndElement();
			return dataSvcMapFileImpl;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000C2E8 File Offset: 0x0000A4E8
		private Parameter Read7_Parameter(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id13_Parameter || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			Parameter parameter = new Parameter();
			bool[] array = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[0] && base.Reader.LocalName == this.id14_Name && base.Reader.NamespaceURI == this.id5_Item)
				{
					parameter.Name = base.Reader.Value;
					array[0] = true;
				}
				else if (!array[1] && base.Reader.LocalName == this.id15_Value && base.Reader.NamespaceURI == this.id5_Item)
				{
					parameter.Value = base.Reader.Value;
					array[1] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(parameter, ":Name, :Value");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return parameter;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					base.UnknownNode(parameter, "");
				}
				else
				{
					base.UnknownNode(parameter, "");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return parameter;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000C4B4 File Offset: 0x0000A6B4
		private ExtensionFile Read6_ExtensionFile(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id11_ExtensionFile || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			ExtensionFile extensionFile = new ExtensionFile();
			bool[] array = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[0] && base.Reader.LocalName == this.id16_FileName && base.Reader.NamespaceURI == this.id5_Item)
				{
					extensionFile.FileName = base.Reader.Value;
					array[0] = true;
				}
				else if (!array[1] && base.Reader.LocalName == this.id14_Name && base.Reader.NamespaceURI == this.id5_Item)
				{
					extensionFile.Name = base.Reader.Value;
					array[1] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(extensionFile, ":FileName, :Name");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return extensionFile;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					base.UnknownNode(extensionFile, "");
				}
				else
				{
					base.UnknownNode(extensionFile, "");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return extensionFile;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000C680 File Offset: 0x0000A880
		private MetadataFile Read5_MetadataFile(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id9_MetadataFile || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			MetadataFile metadataFile = new MetadataFile();
			bool[] array = new bool[7];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[0] && base.Reader.LocalName == this.id16_FileName && base.Reader.NamespaceURI == this.id5_Item)
				{
					metadataFile.FileName = base.Reader.Value;
					array[0] = true;
				}
				else if (!array[1] && base.Reader.LocalName == this.id17_MetadataType && base.Reader.NamespaceURI == this.id5_Item)
				{
					metadataFile.FileType = this.Read4_MetadataType(base.Reader.Value);
					array[1] = true;
				}
				else if (!array[2] && base.Reader.LocalName == this.id4_ID && base.Reader.NamespaceURI == this.id5_Item)
				{
					metadataFile.ID = base.Reader.Value;
					array[2] = true;
				}
				else if (!array[3] && base.Reader.LocalName == this.id18_Ignore && base.Reader.NamespaceURI == this.id5_Item)
				{
					metadataFile.Ignore = XmlConvert.ToBoolean(base.Reader.Value);
					metadataFile.IgnoreSpecified = true;
					array[3] = true;
				}
				else if (!array[4] && base.Reader.LocalName == this.id19_IsMergeResult && base.Reader.NamespaceURI == this.id5_Item)
				{
					metadataFile.IsMergeResult = XmlConvert.ToBoolean(base.Reader.Value);
					metadataFile.IsMergeResultSpecified = true;
					array[4] = true;
				}
				else if (!array[5] && base.Reader.LocalName == this.id20_SourceId && base.Reader.NamespaceURI == this.id5_Item)
				{
					metadataFile.SourceId = XmlConvert.ToInt32(base.Reader.Value);
					metadataFile.SourceIdSpecified = true;
					array[5] = true;
				}
				else if (!array[6] && base.Reader.LocalName == this.id21_SourceUrl && base.Reader.NamespaceURI == this.id5_Item)
				{
					metadataFile.SourceUrl = base.Reader.Value;
					array[6] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(metadataFile, ":FileName, :MetadataType, :ID, :Ignore, :IsMergeResult, :SourceId, :SourceUrl");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return metadataFile;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					base.UnknownNode(metadataFile, "");
				}
				else
				{
					base.UnknownNode(metadataFile, "");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return metadataFile;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000C9D0 File Offset: 0x0000ABD0
		private MetadataFile.MetadataType Read4_MetadataType(string s)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(s);
			if (num <= 2559606102U)
			{
				if (num != 1023084118U)
				{
					if (num != 1558337367U)
					{
						if (num == 2559606102U)
						{
							if (s == "Schema")
							{
								return MetadataFile.MetadataType.Schema;
							}
						}
					}
					else if (s == "Wsdl")
					{
						return MetadataFile.MetadataType.Wsdl;
					}
				}
				else if (s == "Xml")
				{
					return MetadataFile.MetadataType.Xml;
				}
			}
			else if (num <= 3122677113U)
			{
				if (num != 3065929585U)
				{
					if (num == 3122677113U)
					{
						if (s == "Disco")
						{
							return MetadataFile.MetadataType.Disco;
						}
					}
				}
				else if (s == "Edmx")
				{
					return MetadataFile.MetadataType.Edmx;
				}
			}
			else if (num != 3155965855U)
			{
				if (num == 3424652889U)
				{
					if (s == "Unknown")
					{
						return MetadataFile.MetadataType.Unknown;
					}
				}
			}
			else if (s == "Policy")
			{
				return MetadataFile.MetadataType.Policy;
			}
			throw base.CreateUnknownConstantException(s, typeof(MetadataFile.MetadataType));
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000CAC4 File Offset: 0x0000ACC4
		private MetadataSource Read2_MetadataSource(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id7_MetadataSource || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			MetadataSource metadataSource = new MetadataSource();
			bool[] array = new bool[3];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[0] && base.Reader.LocalName == this.id22_Address && base.Reader.NamespaceURI == this.id5_Item)
				{
					metadataSource.Address = base.Reader.Value;
					array[0] = true;
				}
				else if (!array[1] && base.Reader.LocalName == this.id23_Protocol && base.Reader.NamespaceURI == this.id5_Item)
				{
					metadataSource.Protocol = base.Reader.Value;
					array[1] = true;
				}
				else if (!array[2] && base.Reader.LocalName == this.id20_SourceId && base.Reader.NamespaceURI == this.id5_Item)
				{
					metadataSource.SourceId = XmlConvert.ToInt32(base.Reader.Value);
					array[2] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(metadataSource, ":Address, :Protocol, :SourceId");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return metadataSource;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					base.UnknownNode(metadataSource, "");
				}
				else
				{
					base.UnknownNode(metadataSource, "");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return metadataSource;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000032F4 File Offset: 0x000014F4
		protected override void InitCallbacks()
		{
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000CCDC File Offset: 0x0000AEDC
		protected override void InitIDs()
		{
			this.id5_Item = base.Reader.NameTable.Add("");
			this.id4_ID = base.Reader.NameTable.Add("ID");
			this.id18_Ignore = base.Reader.NameTable.Add("Ignore");
			this.id21_SourceUrl = base.Reader.NameTable.Add("SourceUrl");
			this.id20_SourceId = base.Reader.NameTable.Add("SourceId");
			this.id14_Name = base.Reader.NameTable.Add("Name");
			this.id2_Item = base.Reader.NameTable.Add("urn:schemas-microsoft-com:xml-dataservicemap");
			this.id11_ExtensionFile = base.Reader.NameTable.Add("ExtensionFile");
			this.id12_Parameters = base.Reader.NameTable.Add("Parameters");
			this.id1_ReferenceGroup = base.Reader.NameTable.Add("ReferenceGroup");
			this.id16_FileName = base.Reader.NameTable.Add("FileName");
			this.id6_MetadataSources = base.Reader.NameTable.Add("MetadataSources");
			this.id17_MetadataType = base.Reader.NameTable.Add("MetadataType");
			this.id13_Parameter = base.Reader.NameTable.Add("Parameter");
			this.id15_Value = base.Reader.NameTable.Add("Value");
			this.id23_Protocol = base.Reader.NameTable.Add("Protocol");
			this.id3_DataSvcMapFileImpl = base.Reader.NameTable.Add("DataSvcMapFileImpl");
			this.id8_Metadata = base.Reader.NameTable.Add("Metadata");
			this.id9_MetadataFile = base.Reader.NameTable.Add("MetadataFile");
			this.id19_IsMergeResult = base.Reader.NameTable.Add("IsMergeResult");
			this.id7_MetadataSource = base.Reader.NameTable.Add("MetadataSource");
			this.id10_Extensions = base.Reader.NameTable.Add("Extensions");
			this.id22_Address = base.Reader.NameTable.Add("Address");
		}

		// Token: 0x040000BC RID: 188
		private string id5_Item;

		// Token: 0x040000BD RID: 189
		private string id4_ID;

		// Token: 0x040000BE RID: 190
		private string id18_Ignore;

		// Token: 0x040000BF RID: 191
		private string id21_SourceUrl;

		// Token: 0x040000C0 RID: 192
		private string id20_SourceId;

		// Token: 0x040000C1 RID: 193
		private string id14_Name;

		// Token: 0x040000C2 RID: 194
		private string id2_Item;

		// Token: 0x040000C3 RID: 195
		private string id11_ExtensionFile;

		// Token: 0x040000C4 RID: 196
		private string id12_Parameters;

		// Token: 0x040000C5 RID: 197
		private string id1_ReferenceGroup;

		// Token: 0x040000C6 RID: 198
		private string id16_FileName;

		// Token: 0x040000C7 RID: 199
		private string id6_MetadataSources;

		// Token: 0x040000C8 RID: 200
		private string id17_MetadataType;

		// Token: 0x040000C9 RID: 201
		private string id13_Parameter;

		// Token: 0x040000CA RID: 202
		private string id15_Value;

		// Token: 0x040000CB RID: 203
		private string id23_Protocol;

		// Token: 0x040000CC RID: 204
		private string id3_DataSvcMapFileImpl;

		// Token: 0x040000CD RID: 205
		private string id8_Metadata;

		// Token: 0x040000CE RID: 206
		private string id9_MetadataFile;

		// Token: 0x040000CF RID: 207
		private string id19_IsMergeResult;

		// Token: 0x040000D0 RID: 208
		private string id7_MetadataSource;

		// Token: 0x040000D1 RID: 209
		private string id10_Extensions;

		// Token: 0x040000D2 RID: 210
		private string id22_Address;
	}
}
