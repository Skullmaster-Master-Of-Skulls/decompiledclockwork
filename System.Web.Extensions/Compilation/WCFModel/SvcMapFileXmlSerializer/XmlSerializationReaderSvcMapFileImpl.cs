using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel.SvcMapFileXmlSerializer
{
	// Token: 0x0200002A RID: 42
	internal class XmlSerializationReaderSvcMapFileImpl : XmlSerializationReader
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x00008B94 File Offset: 0x00006D94
		public object Read16_ReferenceGroup()
		{
			object result = null;
			base.Reader.MoveToContent();
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (base.Reader.LocalName != this.id1_ReferenceGroup || base.Reader.NamespaceURI != this.id2_Item)
				{
					throw base.CreateUnknownNodeException();
				}
				result = this.Read15_SvcMapFileImpl(true, true);
			}
			else
			{
				base.UnknownNode(null, "urn:schemas-microsoft-com:xml-wcfservicemap:ReferenceGroup");
			}
			return result;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00008C04 File Offset: 0x00006E04
		private SvcMapFileImpl Read15_SvcMapFileImpl(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id3_SvcMapFileImpl || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			SvcMapFileImpl svcMapFileImpl = new SvcMapFileImpl();
			List<MetadataSource> metadataSourceList = svcMapFileImpl.MetadataSourceList;
			List<MetadataFile> metadataList = svcMapFileImpl.MetadataList;
			List<ExtensionFile> extensions = svcMapFileImpl.Extensions;
			bool[] array = new bool[5];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[4] && base.Reader.LocalName == this.id4_ID && base.Reader.NamespaceURI == this.id5_Item)
				{
					svcMapFileImpl.ID = base.Reader.Value;
					array[4] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(svcMapFileImpl, ":ID");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return svcMapFileImpl;
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
						if (base.Reader.LocalName == this.id6_ClientOptions && base.Reader.NamespaceURI == this.id2_Item)
						{
							svcMapFileImpl.ClientOptions = this.Read9_ClientOptions(false, true);
						}
						num = 1;
						break;
					case 1:
						if (base.Reader.LocalName == this.id7_MetadataSources && base.Reader.NamespaceURI == this.id2_Item)
						{
							if (!base.ReadNull())
							{
								List<MetadataSource> metadataSourceList2 = svcMapFileImpl.MetadataSourceList;
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
											if (base.Reader.LocalName == this.id8_MetadataSource && base.Reader.NamespaceURI == this.id2_Item)
											{
												if (metadataSourceList2 == null)
												{
													base.Reader.Skip();
												}
												else
												{
													metadataSourceList2.Add(this.Read10_MetadataSource(true, true));
												}
											}
											else
											{
												base.UnknownNode(null, "urn:schemas-microsoft-com:xml-wcfservicemap:MetadataSource");
											}
										}
										else
										{
											base.UnknownNode(null, "urn:schemas-microsoft-com:xml-wcfservicemap:MetadataSource");
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
							num = 2;
						}
						break;
					case 2:
						if (base.Reader.LocalName == this.id9_Metadata && base.Reader.NamespaceURI == this.id2_Item)
						{
							if (!base.ReadNull())
							{
								List<MetadataFile> metadataList2 = svcMapFileImpl.MetadataList;
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
											if (base.Reader.LocalName == this.id10_MetadataFile && base.Reader.NamespaceURI == this.id2_Item)
											{
												if (metadataList2 == null)
												{
													base.Reader.Skip();
												}
												else
												{
													metadataList2.Add(this.Read13_MetadataFile(true, true));
												}
											}
											else
											{
												base.UnknownNode(null, "urn:schemas-microsoft-com:xml-wcfservicemap:MetadataFile");
											}
										}
										else
										{
											base.UnknownNode(null, "urn:schemas-microsoft-com:xml-wcfservicemap:MetadataFile");
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
							num = 3;
						}
						break;
					case 3:
						if (base.Reader.LocalName == this.id11_Extensions && base.Reader.NamespaceURI == this.id2_Item)
						{
							if (!base.ReadNull())
							{
								List<ExtensionFile> extensions2 = svcMapFileImpl.Extensions;
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
											if (base.Reader.LocalName == this.id12_ExtensionFile && base.Reader.NamespaceURI == this.id2_Item)
											{
												if (extensions2 == null)
												{
													base.Reader.Skip();
												}
												else
												{
													extensions2.Add(this.Read14_ExtensionFile(true, true));
												}
											}
											else
											{
												base.UnknownNode(null, "urn:schemas-microsoft-com:xml-wcfservicemap:ExtensionFile");
											}
										}
										else
										{
											base.UnknownNode(null, "urn:schemas-microsoft-com:xml-wcfservicemap:ExtensionFile");
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
							num = 4;
						}
						break;
					default:
						base.UnknownNode(svcMapFileImpl, null);
						break;
					}
				}
				else
				{
					base.UnknownNode(svcMapFileImpl, null);
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num2, ref readerCount);
			}
			base.ReadEndElement();
			return svcMapFileImpl;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000091B4 File Offset: 0x000073B4
		private ExtensionFile Read14_ExtensionFile(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id12_ExtensionFile || xmlQualifiedName.Namespace != this.id2_Item))
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
				if (!array[0] && base.Reader.LocalName == this.id13_FileName && base.Reader.NamespaceURI == this.id5_Item)
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

		// Token: 0x060001C3 RID: 451 RVA: 0x00009380 File Offset: 0x00007580
		private MetadataFile Read13_MetadataFile(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id10_MetadataFile || xmlQualifiedName.Namespace != this.id2_Item))
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
				if (!array[0] && base.Reader.LocalName == this.id13_FileName && base.Reader.NamespaceURI == this.id5_Item)
				{
					metadataFile.FileName = base.Reader.Value;
					array[0] = true;
				}
				else if (!array[1] && base.Reader.LocalName == this.id15_MetadataType && base.Reader.NamespaceURI == this.id5_Item)
				{
					metadataFile.FileType = this.Read12_MetadataType(base.Reader.Value);
					array[1] = true;
				}
				else if (!array[2] && base.Reader.LocalName == this.id4_ID && base.Reader.NamespaceURI == this.id5_Item)
				{
					metadataFile.ID = base.Reader.Value;
					array[2] = true;
				}
				else if (!array[3] && base.Reader.LocalName == this.id16_Ignore && base.Reader.NamespaceURI == this.id5_Item)
				{
					metadataFile.Ignore = XmlConvert.ToBoolean(base.Reader.Value);
					metadataFile.IgnoreSpecified = true;
					array[3] = true;
				}
				else if (!array[4] && base.Reader.LocalName == this.id17_IsMergeResult && base.Reader.NamespaceURI == this.id5_Item)
				{
					metadataFile.IsMergeResult = XmlConvert.ToBoolean(base.Reader.Value);
					metadataFile.IsMergeResultSpecified = true;
					array[4] = true;
				}
				else if (!array[5] && base.Reader.LocalName == this.id18_SourceId && base.Reader.NamespaceURI == this.id5_Item)
				{
					metadataFile.SourceId = XmlConvert.ToInt32(base.Reader.Value);
					metadataFile.SourceIdSpecified = true;
					array[5] = true;
				}
				else if (!array[6] && base.Reader.LocalName == this.id19_SourceUrl && base.Reader.NamespaceURI == this.id5_Item)
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

		// Token: 0x060001C4 RID: 452 RVA: 0x000096D0 File Offset: 0x000078D0
		private MetadataFile.MetadataType Read12_MetadataType(string s)
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

		// Token: 0x060001C5 RID: 453 RVA: 0x000097C4 File Offset: 0x000079C4
		private MetadataSource Read10_MetadataSource(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id8_MetadataSource || xmlQualifiedName.Namespace != this.id2_Item))
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
				if (!array[0] && base.Reader.LocalName == this.id20_Address && base.Reader.NamespaceURI == this.id5_Item)
				{
					metadataSource.Address = base.Reader.Value;
					array[0] = true;
				}
				else if (!array[1] && base.Reader.LocalName == this.id21_Protocol && base.Reader.NamespaceURI == this.id5_Item)
				{
					metadataSource.Protocol = base.Reader.Value;
					array[1] = true;
				}
				else if (!array[2] && base.Reader.LocalName == this.id18_SourceId && base.Reader.NamespaceURI == this.id5_Item)
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

		// Token: 0x060001C6 RID: 454 RVA: 0x000099DC File Offset: 0x00007BDC
		private ClientOptions Read9_ClientOptions(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id6_ClientOptions || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			ClientOptions clientOptions = new ClientOptions();
			List<ReferencedType> excludedTypeList = clientOptions.ExcludedTypeList;
			List<NamespaceMapping> namespaceMappingList = clientOptions.NamespaceMappingList;
			List<ReferencedCollectionType> collectionMappingList = clientOptions.CollectionMappingList;
			List<ReferencedAssembly> referencedAssemblyList = clientOptions.ReferencedAssemblyList;
			List<ReferencedType> referencedDataContractTypeList = clientOptions.ReferencedDataContractTypeList;
			List<ContractMapping> serviceContractMappingList = clientOptions.ServiceContractMappingList;
			bool[] array = new bool[17];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(clientOptions);
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return clientOptions;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					if (!array[0] && base.Reader.LocalName == this.id22_GenerateAsynchronousMethods && base.Reader.NamespaceURI == this.id2_Item)
					{
						clientOptions.GenerateAsynchronousMethods = XmlConvert.ToBoolean(base.Reader.ReadElementString());
						array[0] = true;
					}
					else if (!array[1] && base.Reader.LocalName == this.id23_Item && base.Reader.NamespaceURI == this.id2_Item)
					{
						clientOptions.GenerateTaskBasedAsynchronousMethod = XmlConvert.ToBoolean(base.Reader.ReadElementString());
						array[1] = true;
					}
					else if (!array[2] && base.Reader.LocalName == this.id24_EnableDataBinding && base.Reader.NamespaceURI == this.id2_Item)
					{
						clientOptions.EnableDataBinding = XmlConvert.ToBoolean(base.Reader.ReadElementString());
						array[2] = true;
					}
					else if (base.Reader.LocalName == this.id25_ExcludedTypes && base.Reader.NamespaceURI == this.id2_Item)
					{
						if (!base.ReadNull())
						{
							List<ReferencedType> excludedTypeList2 = clientOptions.ExcludedTypeList;
							if (excludedTypeList2 == null || base.Reader.IsEmptyElement)
							{
								base.Reader.Skip();
							}
							else
							{
								base.Reader.ReadStartElement();
								base.Reader.MoveToContent();
								int num2 = 0;
								int readerCount2 = base.ReaderCount;
								while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
								{
									if (base.Reader.NodeType == XmlNodeType.Element)
									{
										if (base.Reader.LocalName == this.id26_ExcludedType && base.Reader.NamespaceURI == this.id2_Item)
										{
											if (excludedTypeList2 == null)
											{
												base.Reader.Skip();
											}
											else
											{
												excludedTypeList2.Add(this.Read2_ReferencedType(true, true));
											}
										}
										else
										{
											base.UnknownNode(null, "urn:schemas-microsoft-com:xml-wcfservicemap:ExcludedType");
										}
									}
									else
									{
										base.UnknownNode(null, "urn:schemas-microsoft-com:xml-wcfservicemap:ExcludedType");
									}
									base.Reader.MoveToContent();
									base.CheckReaderCount(ref num2, ref readerCount2);
								}
								base.ReadEndElement();
							}
						}
					}
					else if (!array[4] && base.Reader.LocalName == this.id27_ImportXmlTypes && base.Reader.NamespaceURI == this.id2_Item)
					{
						clientOptions.ImportXmlTypes = XmlConvert.ToBoolean(base.Reader.ReadElementString());
						array[4] = true;
					}
					else if (!array[5] && base.Reader.LocalName == this.id28_GenerateInternalTypes && base.Reader.NamespaceURI == this.id2_Item)
					{
						clientOptions.GenerateInternalTypes = XmlConvert.ToBoolean(base.Reader.ReadElementString());
						array[5] = true;
					}
					else if (!array[6] && base.Reader.LocalName == this.id29_GenerateMessageContracts && base.Reader.NamespaceURI == this.id2_Item)
					{
						clientOptions.GenerateMessageContracts = XmlConvert.ToBoolean(base.Reader.ReadElementString());
						array[6] = true;
					}
					else if (base.Reader.LocalName == this.id30_NamespaceMappings && base.Reader.NamespaceURI == this.id2_Item)
					{
						if (!base.ReadNull())
						{
							List<NamespaceMapping> namespaceMappingList2 = clientOptions.NamespaceMappingList;
							if (namespaceMappingList2 == null || base.Reader.IsEmptyElement)
							{
								base.Reader.Skip();
							}
							else
							{
								base.Reader.ReadStartElement();
								base.Reader.MoveToContent();
								int num3 = 0;
								int readerCount3 = base.ReaderCount;
								while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
								{
									if (base.Reader.NodeType == XmlNodeType.Element)
									{
										if (base.Reader.LocalName == this.id31_NamespaceMapping && base.Reader.NamespaceURI == this.id2_Item)
										{
											if (namespaceMappingList2 == null)
											{
												base.Reader.Skip();
											}
											else
											{
												namespaceMappingList2.Add(this.Read3_NamespaceMapping(true, true));
											}
										}
										else
										{
											base.UnknownNode(null, "urn:schemas-microsoft-com:xml-wcfservicemap:NamespaceMapping");
										}
									}
									else
									{
										base.UnknownNode(null, "urn:schemas-microsoft-com:xml-wcfservicemap:NamespaceMapping");
									}
									base.Reader.MoveToContent();
									base.CheckReaderCount(ref num3, ref readerCount3);
								}
								base.ReadEndElement();
							}
						}
					}
					else if (base.Reader.LocalName == this.id32_CollectionMappings && base.Reader.NamespaceURI == this.id2_Item)
					{
						if (!base.ReadNull())
						{
							List<ReferencedCollectionType> collectionMappingList2 = clientOptions.CollectionMappingList;
							if (collectionMappingList2 == null || base.Reader.IsEmptyElement)
							{
								base.Reader.Skip();
							}
							else
							{
								base.Reader.ReadStartElement();
								base.Reader.MoveToContent();
								int num4 = 0;
								int readerCount4 = base.ReaderCount;
								while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
								{
									if (base.Reader.NodeType == XmlNodeType.Element)
									{
										if (base.Reader.LocalName == this.id33_CollectionMapping && base.Reader.NamespaceURI == this.id2_Item)
										{
											if (collectionMappingList2 == null)
											{
												base.Reader.Skip();
											}
											else
											{
												collectionMappingList2.Add(this.Read5_ReferencedCollectionType(true, true));
											}
										}
										else
										{
											base.UnknownNode(null, "urn:schemas-microsoft-com:xml-wcfservicemap:CollectionMapping");
										}
									}
									else
									{
										base.UnknownNode(null, "urn:schemas-microsoft-com:xml-wcfservicemap:CollectionMapping");
									}
									base.Reader.MoveToContent();
									base.CheckReaderCount(ref num4, ref readerCount4);
								}
								base.ReadEndElement();
							}
						}
					}
					else if (!array[9] && base.Reader.LocalName == this.id34_GenerateSerializableTypes && base.Reader.NamespaceURI == this.id2_Item)
					{
						clientOptions.GenerateSerializableTypes = XmlConvert.ToBoolean(base.Reader.ReadElementString());
						array[9] = true;
					}
					else if (!array[10] && base.Reader.LocalName == this.id35_Serializer && base.Reader.NamespaceURI == this.id2_Item)
					{
						clientOptions.Serializer = this.Read6_ProxySerializerType(base.Reader.ReadElementString());
						array[10] = true;
					}
					else if (!array[11] && base.Reader.LocalName == this.id36_UseSerializerForFaults && base.Reader.NamespaceURI == this.id2_Item)
					{
						clientOptions.UseSerializerForFaults = XmlConvert.ToBoolean(base.Reader.ReadElementString());
						array[11] = true;
					}
					else if (!array[12] && base.Reader.LocalName == this.id37_Wrapped && base.Reader.NamespaceURI == this.id2_Item)
					{
						clientOptions.Wrapped = XmlConvert.ToBoolean(base.Reader.ReadElementString());
						array[12] = true;
					}
					else if (!array[13] && base.Reader.LocalName == this.id38_ReferenceAllAssemblies && base.Reader.NamespaceURI == this.id2_Item)
					{
						clientOptions.ReferenceAllAssemblies = XmlConvert.ToBoolean(base.Reader.ReadElementString());
						array[13] = true;
					}
					else if (base.Reader.LocalName == this.id39_ReferencedAssemblies && base.Reader.NamespaceURI == this.id2_Item)
					{
						if (!base.ReadNull())
						{
							List<ReferencedAssembly> referencedAssemblyList2 = clientOptions.ReferencedAssemblyList;
							if (referencedAssemblyList2 == null || base.Reader.IsEmptyElement)
							{
								base.Reader.Skip();
							}
							else
							{
								base.Reader.ReadStartElement();
								base.Reader.MoveToContent();
								int num5 = 0;
								int readerCount5 = base.ReaderCount;
								while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
								{
									if (base.Reader.NodeType == XmlNodeType.Element)
									{
										if (base.Reader.LocalName == this.id40_ReferencedAssembly && base.Reader.NamespaceURI == this.id2_Item)
										{
											if (referencedAssemblyList2 == null)
											{
												base.Reader.Skip();
											}
											else
											{
												referencedAssemblyList2.Add(this.Read7_ReferencedAssembly(true, true));
											}
										}
										else
										{
											base.UnknownNode(null, "urn:schemas-microsoft-com:xml-wcfservicemap:ReferencedAssembly");
										}
									}
									else
									{
										base.UnknownNode(null, "urn:schemas-microsoft-com:xml-wcfservicemap:ReferencedAssembly");
									}
									base.Reader.MoveToContent();
									base.CheckReaderCount(ref num5, ref readerCount5);
								}
								base.ReadEndElement();
							}
						}
					}
					else if (base.Reader.LocalName == this.id41_ReferencedDataContractTypes && base.Reader.NamespaceURI == this.id2_Item)
					{
						if (!base.ReadNull())
						{
							List<ReferencedType> referencedDataContractTypeList2 = clientOptions.ReferencedDataContractTypeList;
							if (referencedDataContractTypeList2 == null || base.Reader.IsEmptyElement)
							{
								base.Reader.Skip();
							}
							else
							{
								base.Reader.ReadStartElement();
								base.Reader.MoveToContent();
								int num6 = 0;
								int readerCount6 = base.ReaderCount;
								while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
								{
									if (base.Reader.NodeType == XmlNodeType.Element)
									{
										if (base.Reader.LocalName == this.id42_ReferencedDataContractType && base.Reader.NamespaceURI == this.id2_Item)
										{
											if (referencedDataContractTypeList2 == null)
											{
												base.Reader.Skip();
											}
											else
											{
												referencedDataContractTypeList2.Add(this.Read2_ReferencedType(true, true));
											}
										}
										else
										{
											base.UnknownNode(null, "urn:schemas-microsoft-com:xml-wcfservicemap:ReferencedDataContractType");
										}
									}
									else
									{
										base.UnknownNode(null, "urn:schemas-microsoft-com:xml-wcfservicemap:ReferencedDataContractType");
									}
									base.Reader.MoveToContent();
									base.CheckReaderCount(ref num6, ref readerCount6);
								}
								base.ReadEndElement();
							}
						}
					}
					else if (base.Reader.LocalName == this.id43_ServiceContractMappings && base.Reader.NamespaceURI == this.id2_Item)
					{
						if (!base.ReadNull())
						{
							List<ContractMapping> serviceContractMappingList2 = clientOptions.ServiceContractMappingList;
							if (serviceContractMappingList2 == null || base.Reader.IsEmptyElement)
							{
								base.Reader.Skip();
							}
							else
							{
								base.Reader.ReadStartElement();
								base.Reader.MoveToContent();
								int num7 = 0;
								int readerCount7 = base.ReaderCount;
								while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
								{
									if (base.Reader.NodeType == XmlNodeType.Element)
									{
										if (base.Reader.LocalName == this.id44_ServiceContractMapping && base.Reader.NamespaceURI == this.id2_Item)
										{
											if (serviceContractMappingList2 == null)
											{
												base.Reader.Skip();
											}
											else
											{
												serviceContractMappingList2.Add(this.Read8_ContractMapping(true, true));
											}
										}
										else
										{
											base.UnknownNode(null, "urn:schemas-microsoft-com:xml-wcfservicemap:ServiceContractMapping");
										}
									}
									else
									{
										base.UnknownNode(null, "urn:schemas-microsoft-com:xml-wcfservicemap:ServiceContractMapping");
									}
									base.Reader.MoveToContent();
									base.CheckReaderCount(ref num7, ref readerCount7);
								}
								base.ReadEndElement();
							}
						}
					}
					else
					{
						base.UnknownNode(clientOptions, "urn:schemas-microsoft-com:xml-wcfservicemap:GenerateAsynchronousMethods, urn:schemas-microsoft-com:xml-wcfservicemap:GenerateTaskBasedAsynchronousMethod, urn:schemas-microsoft-com:xml-wcfservicemap:EnableDataBinding, urn:schemas-microsoft-com:xml-wcfservicemap:ExcludedTypes, urn:schemas-microsoft-com:xml-wcfservicemap:ImportXmlTypes, urn:schemas-microsoft-com:xml-wcfservicemap:GenerateInternalTypes, urn:schemas-microsoft-com:xml-wcfservicemap:GenerateMessageContracts, urn:schemas-microsoft-com:xml-wcfservicemap:NamespaceMappings, urn:schemas-microsoft-com:xml-wcfservicemap:CollectionMappings, urn:schemas-microsoft-com:xml-wcfservicemap:GenerateSerializableTypes, urn:schemas-microsoft-com:xml-wcfservicemap:Serializer, urn:schemas-microsoft-com:xml-wcfservicemap:UseSerializerForFaults, urn:schemas-microsoft-com:xml-wcfservicemap:Wrapped, urn:schemas-microsoft-com:xml-wcfservicemap:ReferenceAllAssemblies, urn:schemas-microsoft-com:xml-wcfservicemap:ReferencedAssemblies, urn:schemas-microsoft-com:xml-wcfservicemap:ReferencedDataContractTypes, urn:schemas-microsoft-com:xml-wcfservicemap:ServiceContractMappings");
					}
				}
				else
				{
					base.UnknownNode(clientOptions, "urn:schemas-microsoft-com:xml-wcfservicemap:GenerateAsynchronousMethods, urn:schemas-microsoft-com:xml-wcfservicemap:GenerateTaskBasedAsynchronousMethod, urn:schemas-microsoft-com:xml-wcfservicemap:EnableDataBinding, urn:schemas-microsoft-com:xml-wcfservicemap:ExcludedTypes, urn:schemas-microsoft-com:xml-wcfservicemap:ImportXmlTypes, urn:schemas-microsoft-com:xml-wcfservicemap:GenerateInternalTypes, urn:schemas-microsoft-com:xml-wcfservicemap:GenerateMessageContracts, urn:schemas-microsoft-com:xml-wcfservicemap:NamespaceMappings, urn:schemas-microsoft-com:xml-wcfservicemap:CollectionMappings, urn:schemas-microsoft-com:xml-wcfservicemap:GenerateSerializableTypes, urn:schemas-microsoft-com:xml-wcfservicemap:Serializer, urn:schemas-microsoft-com:xml-wcfservicemap:UseSerializerForFaults, urn:schemas-microsoft-com:xml-wcfservicemap:Wrapped, urn:schemas-microsoft-com:xml-wcfservicemap:ReferenceAllAssemblies, urn:schemas-microsoft-com:xml-wcfservicemap:ReferencedAssemblies, urn:schemas-microsoft-com:xml-wcfservicemap:ReferencedDataContractTypes, urn:schemas-microsoft-com:xml-wcfservicemap:ServiceContractMappings");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return clientOptions;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000A5E8 File Offset: 0x000087E8
		private ContractMapping Read8_ContractMapping(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id45_ContractMapping || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			ContractMapping contractMapping = new ContractMapping();
			bool[] array = new bool[3];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[0] && base.Reader.LocalName == this.id14_Name && base.Reader.NamespaceURI == this.id5_Item)
				{
					contractMapping.Name = base.Reader.Value;
					array[0] = true;
				}
				else if (!array[1] && base.Reader.LocalName == this.id46_TargetNamespace && base.Reader.NamespaceURI == this.id5_Item)
				{
					contractMapping.TargetNamespace = base.Reader.Value;
					array[1] = true;
				}
				else if (!array[2] && base.Reader.LocalName == this.id47_TypeName && base.Reader.NamespaceURI == this.id5_Item)
				{
					contractMapping.TypeName = base.Reader.Value;
					array[2] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(contractMapping, ":Name, :TargetNamespace, :TypeName");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return contractMapping;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					base.UnknownNode(contractMapping, "");
				}
				else
				{
					base.UnknownNode(contractMapping, "");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return contractMapping;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000A7F8 File Offset: 0x000089F8
		private ReferencedType Read2_ReferencedType(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id48_ReferencedType || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			ReferencedType referencedType = new ReferencedType();
			bool[] array = new bool[1];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[0] && base.Reader.LocalName == this.id47_TypeName && base.Reader.NamespaceURI == this.id5_Item)
				{
					referencedType.TypeName = base.Reader.Value;
					array[0] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(referencedType, ":TypeName");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return referencedType;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					base.UnknownNode(referencedType, "");
				}
				else
				{
					base.UnknownNode(referencedType, "");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return referencedType;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000A97C File Offset: 0x00008B7C
		private ReferencedAssembly Read7_ReferencedAssembly(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id40_ReferencedAssembly || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			ReferencedAssembly referencedAssembly = new ReferencedAssembly();
			bool[] array = new bool[1];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[0] && base.Reader.LocalName == this.id49_AssemblyName && base.Reader.NamespaceURI == this.id5_Item)
				{
					referencedAssembly.AssemblyName = base.Reader.Value;
					array[0] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(referencedAssembly, ":AssemblyName");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return referencedAssembly;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					base.UnknownNode(referencedAssembly, "");
				}
				else
				{
					base.UnknownNode(referencedAssembly, "");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return referencedAssembly;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000AB00 File Offset: 0x00008D00
		private ClientOptions.ProxySerializerType Read6_ProxySerializerType(string s)
		{
			if (s == "Auto")
			{
				return ClientOptions.ProxySerializerType.Auto;
			}
			if (s == "DataContractSerializer")
			{
				return ClientOptions.ProxySerializerType.DataContractSerializer;
			}
			if (!(s == "XmlSerializer"))
			{
				throw base.CreateUnknownConstantException(s, typeof(ClientOptions.ProxySerializerType));
			}
			return ClientOptions.ProxySerializerType.XmlSerializer;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000AB50 File Offset: 0x00008D50
		private ReferencedCollectionType Read5_ReferencedCollectionType(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id50_ReferencedCollectionType || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			ReferencedCollectionType referencedCollectionType = new ReferencedCollectionType();
			bool[] array = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[0] && base.Reader.LocalName == this.id47_TypeName && base.Reader.NamespaceURI == this.id5_Item)
				{
					referencedCollectionType.TypeName = base.Reader.Value;
					array[0] = true;
				}
				else if (!array[1] && base.Reader.LocalName == this.id51_Category && base.Reader.NamespaceURI == this.id5_Item)
				{
					referencedCollectionType.Category = this.Read4_CollectionCategory(base.Reader.Value);
					array[1] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(referencedCollectionType, ":TypeName, :Category");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return referencedCollectionType;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					base.UnknownNode(referencedCollectionType, "");
				}
				else
				{
					base.UnknownNode(referencedCollectionType, "");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return referencedCollectionType;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000AD24 File Offset: 0x00008F24
		private ReferencedCollectionType.CollectionCategory Read4_CollectionCategory(string s)
		{
			if (s == "Unknown")
			{
				return ReferencedCollectionType.CollectionCategory.Unknown;
			}
			if (s == "List")
			{
				return ReferencedCollectionType.CollectionCategory.List;
			}
			if (!(s == "Dictionary"))
			{
				throw base.CreateUnknownConstantException(s, typeof(ReferencedCollectionType.CollectionCategory));
			}
			return ReferencedCollectionType.CollectionCategory.Dictionary;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000AD74 File Offset: 0x00008F74
		private NamespaceMapping Read3_NamespaceMapping(bool isNullable, bool checkType)
		{
			XmlQualifiedName xmlQualifiedName = checkType ? base.GetXsiType() : null;
			bool flag = false;
			if (isNullable)
			{
				flag = base.ReadNull();
			}
			if (checkType && !(xmlQualifiedName == null) && (xmlQualifiedName.Name != this.id31_NamespaceMapping || xmlQualifiedName.Namespace != this.id2_Item))
			{
				throw base.CreateUnknownTypeException(xmlQualifiedName);
			}
			if (flag)
			{
				return null;
			}
			NamespaceMapping namespaceMapping = new NamespaceMapping();
			bool[] array = new bool[2];
			while (base.Reader.MoveToNextAttribute())
			{
				if (!array[0] && base.Reader.LocalName == this.id46_TargetNamespace && base.Reader.NamespaceURI == this.id5_Item)
				{
					namespaceMapping.TargetNamespace = base.Reader.Value;
					array[0] = true;
				}
				else if (!array[1] && base.Reader.LocalName == this.id52_ClrNamespace && base.Reader.NamespaceURI == this.id5_Item)
				{
					namespaceMapping.ClrNamespace = base.Reader.Value;
					array[1] = true;
				}
				else if (!base.IsXmlnsAttribute(base.Reader.Name))
				{
					base.UnknownNode(namespaceMapping, ":TargetNamespace, :ClrNamespace");
				}
			}
			base.Reader.MoveToElement();
			if (base.Reader.IsEmptyElement)
			{
				base.Reader.Skip();
				return namespaceMapping;
			}
			base.Reader.ReadStartElement();
			base.Reader.MoveToContent();
			int num = 0;
			int readerCount = base.ReaderCount;
			while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != XmlNodeType.None)
			{
				if (base.Reader.NodeType == XmlNodeType.Element)
				{
					base.UnknownNode(namespaceMapping, "");
				}
				else
				{
					base.UnknownNode(namespaceMapping, "");
				}
				base.Reader.MoveToContent();
				base.CheckReaderCount(ref num, ref readerCount);
			}
			base.ReadEndElement();
			return namespaceMapping;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000032F4 File Offset: 0x000014F4
		protected override void InitCallbacks()
		{
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000AF40 File Offset: 0x00009140
		protected override void InitIDs()
		{
			this.id47_TypeName = base.Reader.NameTable.Add("TypeName");
			this.id1_ReferenceGroup = base.Reader.NameTable.Add("ReferenceGroup");
			this.id49_AssemblyName = base.Reader.NameTable.Add("AssemblyName");
			this.id38_ReferenceAllAssemblies = base.Reader.NameTable.Add("ReferenceAllAssemblies");
			this.id46_TargetNamespace = base.Reader.NameTable.Add("TargetNamespace");
			this.id29_GenerateMessageContracts = base.Reader.NameTable.Add("GenerateMessageContracts");
			this.id28_GenerateInternalTypes = base.Reader.NameTable.Add("GenerateInternalTypes");
			this.id13_FileName = base.Reader.NameTable.Add("FileName");
			this.id3_SvcMapFileImpl = base.Reader.NameTable.Add("SvcMapFileImpl");
			this.id7_MetadataSources = base.Reader.NameTable.Add("MetadataSources");
			this.id50_ReferencedCollectionType = base.Reader.NameTable.Add("ReferencedCollectionType");
			this.id8_MetadataSource = base.Reader.NameTable.Add("MetadataSource");
			this.id25_ExcludedTypes = base.Reader.NameTable.Add("ExcludedTypes");
			this.id10_MetadataFile = base.Reader.NameTable.Add("MetadataFile");
			this.id45_ContractMapping = base.Reader.NameTable.Add("ContractMapping");
			this.id15_MetadataType = base.Reader.NameTable.Add("MetadataType");
			this.id34_GenerateSerializableTypes = base.Reader.NameTable.Add("GenerateSerializableTypes");
			this.id31_NamespaceMapping = base.Reader.NameTable.Add("NamespaceMapping");
			this.id42_ReferencedDataContractType = base.Reader.NameTable.Add("ReferencedDataContractType");
			this.id16_Ignore = base.Reader.NameTable.Add("Ignore");
			this.id36_UseSerializerForFaults = base.Reader.NameTable.Add("UseSerializerForFaults");
			this.id52_ClrNamespace = base.Reader.NameTable.Add("ClrNamespace");
			this.id4_ID = base.Reader.NameTable.Add("ID");
			this.id17_IsMergeResult = base.Reader.NameTable.Add("IsMergeResult");
			this.id40_ReferencedAssembly = base.Reader.NameTable.Add("ReferencedAssembly");
			this.id48_ReferencedType = base.Reader.NameTable.Add("ReferencedType");
			this.id22_GenerateAsynchronousMethods = base.Reader.NameTable.Add("GenerateAsynchronousMethods");
			this.id2_Item = base.Reader.NameTable.Add("urn:schemas-microsoft-com:xml-wcfservicemap");
			this.id12_ExtensionFile = base.Reader.NameTable.Add("ExtensionFile");
			this.id32_CollectionMappings = base.Reader.NameTable.Add("CollectionMappings");
			this.id23_Item = base.Reader.NameTable.Add("GenerateTaskBasedAsynchronousMethod");
			this.id39_ReferencedAssemblies = base.Reader.NameTable.Add("ReferencedAssemblies");
			this.id35_Serializer = base.Reader.NameTable.Add("Serializer");
			this.id21_Protocol = base.Reader.NameTable.Add("Protocol");
			this.id44_ServiceContractMapping = base.Reader.NameTable.Add("ServiceContractMapping");
			this.id14_Name = base.Reader.NameTable.Add("Name");
			this.id19_SourceUrl = base.Reader.NameTable.Add("SourceUrl");
			this.id51_Category = base.Reader.NameTable.Add("Category");
			this.id5_Item = base.Reader.NameTable.Add("");
			this.id30_NamespaceMappings = base.Reader.NameTable.Add("NamespaceMappings");
			this.id9_Metadata = base.Reader.NameTable.Add("Metadata");
			this.id24_EnableDataBinding = base.Reader.NameTable.Add("EnableDataBinding");
			this.id27_ImportXmlTypes = base.Reader.NameTable.Add("ImportXmlTypes");
			this.id18_SourceId = base.Reader.NameTable.Add("SourceId");
			this.id20_Address = base.Reader.NameTable.Add("Address");
			this.id11_Extensions = base.Reader.NameTable.Add("Extensions");
			this.id33_CollectionMapping = base.Reader.NameTable.Add("CollectionMapping");
			this.id26_ExcludedType = base.Reader.NameTable.Add("ExcludedType");
			this.id43_ServiceContractMappings = base.Reader.NameTable.Add("ServiceContractMappings");
			this.id37_Wrapped = base.Reader.NameTable.Add("Wrapped");
			this.id41_ReferencedDataContractTypes = base.Reader.NameTable.Add("ReferencedDataContractTypes");
			this.id6_ClientOptions = base.Reader.NameTable.Add("ClientOptions");
		}

		// Token: 0x04000085 RID: 133
		private string id47_TypeName;

		// Token: 0x04000086 RID: 134
		private string id1_ReferenceGroup;

		// Token: 0x04000087 RID: 135
		private string id49_AssemblyName;

		// Token: 0x04000088 RID: 136
		private string id38_ReferenceAllAssemblies;

		// Token: 0x04000089 RID: 137
		private string id46_TargetNamespace;

		// Token: 0x0400008A RID: 138
		private string id29_GenerateMessageContracts;

		// Token: 0x0400008B RID: 139
		private string id28_GenerateInternalTypes;

		// Token: 0x0400008C RID: 140
		private string id13_FileName;

		// Token: 0x0400008D RID: 141
		private string id3_SvcMapFileImpl;

		// Token: 0x0400008E RID: 142
		private string id7_MetadataSources;

		// Token: 0x0400008F RID: 143
		private string id50_ReferencedCollectionType;

		// Token: 0x04000090 RID: 144
		private string id8_MetadataSource;

		// Token: 0x04000091 RID: 145
		private string id25_ExcludedTypes;

		// Token: 0x04000092 RID: 146
		private string id10_MetadataFile;

		// Token: 0x04000093 RID: 147
		private string id45_ContractMapping;

		// Token: 0x04000094 RID: 148
		private string id15_MetadataType;

		// Token: 0x04000095 RID: 149
		private string id34_GenerateSerializableTypes;

		// Token: 0x04000096 RID: 150
		private string id31_NamespaceMapping;

		// Token: 0x04000097 RID: 151
		private string id42_ReferencedDataContractType;

		// Token: 0x04000098 RID: 152
		private string id16_Ignore;

		// Token: 0x04000099 RID: 153
		private string id36_UseSerializerForFaults;

		// Token: 0x0400009A RID: 154
		private string id52_ClrNamespace;

		// Token: 0x0400009B RID: 155
		private string id4_ID;

		// Token: 0x0400009C RID: 156
		private string id17_IsMergeResult;

		// Token: 0x0400009D RID: 157
		private string id40_ReferencedAssembly;

		// Token: 0x0400009E RID: 158
		private string id48_ReferencedType;

		// Token: 0x0400009F RID: 159
		private string id22_GenerateAsynchronousMethods;

		// Token: 0x040000A0 RID: 160
		private string id2_Item;

		// Token: 0x040000A1 RID: 161
		private string id12_ExtensionFile;

		// Token: 0x040000A2 RID: 162
		private string id32_CollectionMappings;

		// Token: 0x040000A3 RID: 163
		private string id23_Item;

		// Token: 0x040000A4 RID: 164
		private string id39_ReferencedAssemblies;

		// Token: 0x040000A5 RID: 165
		private string id35_Serializer;

		// Token: 0x040000A6 RID: 166
		private string id21_Protocol;

		// Token: 0x040000A7 RID: 167
		private string id44_ServiceContractMapping;

		// Token: 0x040000A8 RID: 168
		private string id14_Name;

		// Token: 0x040000A9 RID: 169
		private string id19_SourceUrl;

		// Token: 0x040000AA RID: 170
		private string id51_Category;

		// Token: 0x040000AB RID: 171
		private string id5_Item;

		// Token: 0x040000AC RID: 172
		private string id30_NamespaceMappings;

		// Token: 0x040000AD RID: 173
		private string id9_Metadata;

		// Token: 0x040000AE RID: 174
		private string id24_EnableDataBinding;

		// Token: 0x040000AF RID: 175
		private string id27_ImportXmlTypes;

		// Token: 0x040000B0 RID: 176
		private string id18_SourceId;

		// Token: 0x040000B1 RID: 177
		private string id20_Address;

		// Token: 0x040000B2 RID: 178
		private string id11_Extensions;

		// Token: 0x040000B3 RID: 179
		private string id33_CollectionMapping;

		// Token: 0x040000B4 RID: 180
		private string id26_ExcludedType;

		// Token: 0x040000B5 RID: 181
		private string id43_ServiceContractMappings;

		// Token: 0x040000B6 RID: 182
		private string id37_Wrapped;

		// Token: 0x040000B7 RID: 183
		private string id41_ReferencedDataContractTypes;

		// Token: 0x040000B8 RID: 184
		private string id6_ClientOptions;
	}
}
