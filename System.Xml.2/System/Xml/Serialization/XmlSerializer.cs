using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.Xml.Serialization
{
	// Token: 0x020001BA RID: 442
	[__DynamicallyInvokable]
	public class XmlSerializer
	{
		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001E88 RID: 7816 RVA: 0x000A794C File Offset: 0x000A5B4C
		private static XmlSerializerNamespaces DefaultNamespaces
		{
			get
			{
				if (XmlSerializer.defaultNamespaces == null)
				{
					XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
					xmlSerializerNamespaces.AddInternal("xsi", "http://www.w3.org/2001/XMLSchema-instance");
					xmlSerializerNamespaces.AddInternal("xsd", "http://www.w3.org/2001/XMLSchema");
					if (XmlSerializer.defaultNamespaces == null)
					{
						XmlSerializer.defaultNamespaces = xmlSerializerNamespaces;
					}
				}
				return XmlSerializer.defaultNamespaces;
			}
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x000A79A0 File Offset: 0x000A5BA0
		[__DynamicallyInvokable]
		protected XmlSerializer()
		{
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x000A79A8 File Offset: 0x000A5BA8
		[__DynamicallyInvokable]
		public XmlSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace) : this(type, overrides, extraTypes, root, defaultNamespace, null)
		{
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x000A79B8 File Offset: 0x000A5BB8
		[__DynamicallyInvokable]
		public XmlSerializer(Type type, XmlRootAttribute root) : this(type, null, new Type[0], root, null, null)
		{
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x000A79CB File Offset: 0x000A5BCB
		[__DynamicallyInvokable]
		public XmlSerializer(Type type, Type[] extraTypes) : this(type, null, extraTypes, null, null, null)
		{
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x000A79D9 File Offset: 0x000A5BD9
		[__DynamicallyInvokable]
		public XmlSerializer(Type type, XmlAttributeOverrides overrides) : this(type, overrides, new Type[0], null, null, null)
		{
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x000A79EC File Offset: 0x000A5BEC
		[__DynamicallyInvokable]
		public XmlSerializer(XmlTypeMapping xmlTypeMapping)
		{
			this.tempAssembly = XmlSerializer.GenerateTempAssembly(xmlTypeMapping);
			this.mapping = xmlTypeMapping;
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x000A7A07 File Offset: 0x000A5C07
		[__DynamicallyInvokable]
		public XmlSerializer(Type type) : this(type, null)
		{
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x000A7A14 File Offset: 0x000A5C14
		[__DynamicallyInvokable]
		public XmlSerializer(Type type, string defaultNamespace)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.mapping = XmlSerializer.GetKnownMapping(type, defaultNamespace);
			if (this.mapping != null)
			{
				this.primitiveType = type;
				return;
			}
			this.tempAssembly = XmlSerializer.cache[defaultNamespace, type];
			if (this.tempAssembly == null)
			{
				TempAssemblyCache obj = XmlSerializer.cache;
				lock (obj)
				{
					this.tempAssembly = XmlSerializer.cache[defaultNamespace, type];
					if (this.tempAssembly == null)
					{
						XmlSerializerImplementation contract;
						Assembly assembly = TempAssembly.LoadGeneratedAssembly(type, defaultNamespace, out contract);
						if (assembly == null)
						{
							XmlReflectionImporter xmlReflectionImporter = new XmlReflectionImporter(defaultNamespace);
							this.mapping = xmlReflectionImporter.ImportTypeMapping(type, null, defaultNamespace);
							this.tempAssembly = XmlSerializer.GenerateTempAssembly(this.mapping, type, defaultNamespace);
						}
						else
						{
							this.mapping = XmlReflectionImporter.GetTopLevelMapping(type, defaultNamespace);
							this.tempAssembly = new TempAssembly(new XmlMapping[]
							{
								this.mapping
							}, assembly, contract);
						}
					}
					XmlSerializer.cache.Add(defaultNamespace, type, this.tempAssembly);
				}
			}
			if (this.mapping == null)
			{
				this.mapping = XmlReflectionImporter.GetTopLevelMapping(type, defaultNamespace);
			}
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x000A7B4C File Offset: 0x000A5D4C
		public XmlSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace, string location) : this(type, overrides, extraTypes, root, defaultNamespace, location, null)
		{
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x000A7B60 File Offset: 0x000A5D60
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use a XmlSerializer constructor overload which does not take an Evidence parameter. See http://go2.microsoft.com/fwlink/?LinkId=131738 for more information.")]
		public XmlSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace, string location, Evidence evidence)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			XmlReflectionImporter xmlReflectionImporter = new XmlReflectionImporter(overrides, defaultNamespace);
			if (extraTypes != null)
			{
				for (int i = 0; i < extraTypes.Length; i++)
				{
					xmlReflectionImporter.IncludeType(extraTypes[i]);
				}
			}
			this.mapping = xmlReflectionImporter.ImportTypeMapping(type, root, defaultNamespace);
			if (location != null || evidence != null)
			{
				this.DemandForUserLocationOrEvidence();
			}
			this.tempAssembly = XmlSerializer.GenerateTempAssembly(this.mapping, type, defaultNamespace, location, evidence);
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x000A7BE1 File Offset: 0x000A5DE1
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		private void DemandForUserLocationOrEvidence()
		{
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x000A7BE3 File Offset: 0x000A5DE3
		internal static TempAssembly GenerateTempAssembly(XmlMapping xmlMapping)
		{
			return XmlSerializer.GenerateTempAssembly(xmlMapping, null, null);
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x000A7BED File Offset: 0x000A5DED
		internal static TempAssembly GenerateTempAssembly(XmlMapping xmlMapping, Type type, string defaultNamespace)
		{
			if (xmlMapping == null)
			{
				throw new ArgumentNullException("xmlMapping");
			}
			return new TempAssembly(new XmlMapping[]
			{
				xmlMapping
			}, new Type[]
			{
				type
			}, defaultNamespace, null, null);
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x000A7C19 File Offset: 0x000A5E19
		internal static TempAssembly GenerateTempAssembly(XmlMapping xmlMapping, Type type, string defaultNamespace, string location, Evidence evidence)
		{
			return new TempAssembly(new XmlMapping[]
			{
				xmlMapping
			}, new Type[]
			{
				type
			}, defaultNamespace, location, evidence);
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x000A7C38 File Offset: 0x000A5E38
		[__DynamicallyInvokable]
		public void Serialize(TextWriter textWriter, object o)
		{
			this.Serialize(textWriter, o, null);
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x000A7C44 File Offset: 0x000A5E44
		[__DynamicallyInvokable]
		public void Serialize(TextWriter textWriter, object o, XmlSerializerNamespaces namespaces)
		{
			this.Serialize(new XmlTextWriter(textWriter)
			{
				Formatting = Formatting.Indented,
				Indentation = 2
			}, o, namespaces);
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x000A7C6F File Offset: 0x000A5E6F
		[__DynamicallyInvokable]
		public void Serialize(Stream stream, object o)
		{
			this.Serialize(stream, o, null);
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x000A7C7C File Offset: 0x000A5E7C
		[__DynamicallyInvokable]
		public void Serialize(Stream stream, object o, XmlSerializerNamespaces namespaces)
		{
			this.Serialize(new XmlTextWriter(stream, null)
			{
				Formatting = Formatting.Indented,
				Indentation = 2
			}, o, namespaces);
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x000A7CA8 File Offset: 0x000A5EA8
		[__DynamicallyInvokable]
		public void Serialize(XmlWriter xmlWriter, object o)
		{
			this.Serialize(xmlWriter, o, null);
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x000A7CB3 File Offset: 0x000A5EB3
		[__DynamicallyInvokable]
		public void Serialize(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces)
		{
			this.Serialize(xmlWriter, o, namespaces, null);
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x000A7CBF File Offset: 0x000A5EBF
		public void Serialize(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces, string encodingStyle)
		{
			this.Serialize(xmlWriter, o, namespaces, encodingStyle, null);
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x000A7CD0 File Offset: 0x000A5ED0
		public void Serialize(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces, string encodingStyle, string id)
		{
			try
			{
				if (this.primitiveType != null)
				{
					if (encodingStyle != null && encodingStyle.Length > 0)
					{
						throw new InvalidOperationException(Res.GetString("XmlInvalidEncodingNotEncoded1", new object[]
						{
							encodingStyle
						}));
					}
					this.SerializePrimitive(xmlWriter, o, namespaces);
				}
				else
				{
					if (this.tempAssembly == null || this.typedSerializer)
					{
						XmlSerializationWriter xmlSerializationWriter = this.CreateWriter();
						xmlSerializationWriter.Init(xmlWriter, (namespaces == null || namespaces.Count == 0) ? XmlSerializer.DefaultNamespaces : namespaces, encodingStyle, id, this.tempAssembly);
						try
						{
							this.Serialize(o, xmlSerializationWriter);
							goto IL_B8;
						}
						finally
						{
							xmlSerializationWriter.Dispose();
						}
					}
					this.tempAssembly.InvokeWriter(this.mapping, xmlWriter, o, (namespaces == null || namespaces.Count == 0) ? XmlSerializer.DefaultNamespaces : namespaces, encodingStyle, id);
				}
				IL_B8:;
			}
			catch (Exception innerException)
			{
				if (innerException is ThreadAbortException || innerException is StackOverflowException || innerException is OutOfMemoryException)
				{
					throw;
				}
				if (innerException is TargetInvocationException)
				{
					innerException = innerException.InnerException;
				}
				throw new InvalidOperationException(Res.GetString("XmlGenError"), innerException);
			}
			xmlWriter.Flush();
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x000A7DF4 File Offset: 0x000A5FF4
		[__DynamicallyInvokable]
		public object Deserialize(Stream stream)
		{
			return this.Deserialize(new XmlTextReader(stream)
			{
				WhitespaceHandling = WhitespaceHandling.Significant,
				Normalization = true,
				XmlResolver = null
			}, null);
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x000A7E28 File Offset: 0x000A6028
		[__DynamicallyInvokable]
		public object Deserialize(TextReader textReader)
		{
			return this.Deserialize(new XmlTextReader(textReader)
			{
				WhitespaceHandling = WhitespaceHandling.Significant,
				Normalization = true,
				XmlResolver = null
			}, null);
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x000A7E59 File Offset: 0x000A6059
		[__DynamicallyInvokable]
		public object Deserialize(XmlReader xmlReader)
		{
			return this.Deserialize(xmlReader, null);
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x000A7E63 File Offset: 0x000A6063
		public object Deserialize(XmlReader xmlReader, XmlDeserializationEvents events)
		{
			return this.Deserialize(xmlReader, null, events);
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x000A7E6E File Offset: 0x000A606E
		public object Deserialize(XmlReader xmlReader, string encodingStyle)
		{
			return this.Deserialize(xmlReader, encodingStyle, this.events);
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x000A7E80 File Offset: 0x000A6080
		public object Deserialize(XmlReader xmlReader, string encodingStyle, XmlDeserializationEvents events)
		{
			events.sender = this;
			object result;
			try
			{
				if (this.primitiveType != null)
				{
					if (encodingStyle != null && encodingStyle.Length > 0)
					{
						throw new InvalidOperationException(Res.GetString("XmlInvalidEncodingNotEncoded1", new object[]
						{
							encodingStyle
						}));
					}
					result = this.DeserializePrimitive(xmlReader, events);
				}
				else
				{
					if (this.tempAssembly == null || this.typedSerializer)
					{
						XmlSerializationReader xmlSerializationReader = this.CreateReader();
						xmlSerializationReader.Init(xmlReader, events, encodingStyle, this.tempAssembly);
						try
						{
							return this.Deserialize(xmlSerializationReader);
						}
						finally
						{
							xmlSerializationReader.Dispose();
						}
					}
					result = this.tempAssembly.InvokeReader(this.mapping, xmlReader, events, encodingStyle);
				}
			}
			catch (Exception innerException)
			{
				if (innerException is ThreadAbortException || innerException is StackOverflowException || innerException is OutOfMemoryException)
				{
					throw;
				}
				if (innerException is TargetInvocationException)
				{
					innerException = innerException.InnerException;
				}
				if (xmlReader is IXmlLineInfo)
				{
					IXmlLineInfo xmlLineInfo = (IXmlLineInfo)xmlReader;
					throw new InvalidOperationException(Res.GetString("XmlSerializeErrorDetails", new object[]
					{
						xmlLineInfo.LineNumber.ToString(CultureInfo.InvariantCulture),
						xmlLineInfo.LinePosition.ToString(CultureInfo.InvariantCulture)
					}), innerException);
				}
				throw new InvalidOperationException(Res.GetString("XmlSerializeError"), innerException);
			}
			return result;
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x000A7FD8 File Offset: 0x000A61D8
		[__DynamicallyInvokable]
		public virtual bool CanDeserialize(XmlReader xmlReader)
		{
			if (this.primitiveType != null)
			{
				TypeDesc typeDesc = (TypeDesc)TypeScope.PrimtiveTypes[this.primitiveType];
				return xmlReader.IsStartElement(typeDesc.DataType.Name, string.Empty);
			}
			return this.tempAssembly != null && this.tempAssembly.CanRead(this.mapping, xmlReader);
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x000A803C File Offset: 0x000A623C
		[__DynamicallyInvokable]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static XmlSerializer[] FromMappings(XmlMapping[] mappings)
		{
			return XmlSerializer.FromMappings(mappings, null);
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x000A8048 File Offset: 0x000A6248
		[__DynamicallyInvokable]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static XmlSerializer[] FromMappings(XmlMapping[] mappings, Type type)
		{
			if (mappings == null || mappings.Length == 0)
			{
				return new XmlSerializer[0];
			}
			XmlSerializerImplementation xmlSerializerImplementation = null;
			Assembly left = (type == null) ? null : TempAssembly.LoadGeneratedAssembly(type, null, out xmlSerializerImplementation);
			if (!(left == null))
			{
				XmlSerializer[] array = new XmlSerializer[mappings.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (XmlSerializer)xmlSerializerImplementation.TypedSerializers[mappings[i].Key];
				}
				return array;
			}
			if (XmlMapping.IsShallow(mappings))
			{
				return new XmlSerializer[0];
			}
			if (type == null)
			{
				TempAssembly tempAssembly = new TempAssembly(mappings, new Type[]
				{
					type
				}, null, null, null);
				XmlSerializer[] array2 = new XmlSerializer[mappings.Length];
				xmlSerializerImplementation = tempAssembly.Contract;
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = (XmlSerializer)xmlSerializerImplementation.TypedSerializers[mappings[j].Key];
					array2[j].SetTempAssembly(tempAssembly, mappings[j]);
				}
				return array2;
			}
			return XmlSerializer.GetSerializersFromCache(mappings, type);
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x000A814C File Offset: 0x000A634C
		private static XmlSerializer[] GetSerializersFromCache(XmlMapping[] mappings, Type type)
		{
			XmlSerializer[] array = new XmlSerializer[mappings.Length];
			Hashtable hashtable = null;
			Hashtable obj = XmlSerializer.xmlSerializerTable;
			lock (obj)
			{
				hashtable = (XmlSerializer.xmlSerializerTable[type] as Hashtable);
				if (hashtable == null)
				{
					hashtable = new Hashtable();
					XmlSerializer.xmlSerializerTable[type] = hashtable;
				}
			}
			Hashtable obj2 = hashtable;
			lock (obj2)
			{
				Hashtable hashtable2 = new Hashtable();
				for (int i = 0; i < mappings.Length; i++)
				{
					XmlSerializer.XmlSerializerMappingKey key = new XmlSerializer.XmlSerializerMappingKey(mappings[i]);
					array[i] = (hashtable[key] as XmlSerializer);
					if (array[i] == null)
					{
						hashtable2.Add(key, i);
					}
				}
				if (hashtable2.Count > 0)
				{
					XmlMapping[] array2 = new XmlMapping[hashtable2.Count];
					int num = 0;
					foreach (object obj3 in hashtable2.Keys)
					{
						XmlSerializer.XmlSerializerMappingKey xmlSerializerMappingKey = (XmlSerializer.XmlSerializerMappingKey)obj3;
						array2[num++] = xmlSerializerMappingKey.Mapping;
					}
					TempAssembly tempAssembly = new TempAssembly(array2, new Type[]
					{
						type
					}, null, null, null);
					XmlSerializerImplementation contract = tempAssembly.Contract;
					foreach (object obj4 in hashtable2.Keys)
					{
						XmlSerializer.XmlSerializerMappingKey xmlSerializerMappingKey2 = (XmlSerializer.XmlSerializerMappingKey)obj4;
						num = (int)hashtable2[xmlSerializerMappingKey2];
						array[num] = (XmlSerializer)contract.TypedSerializers[xmlSerializerMappingKey2.Mapping.Key];
						array[num].SetTempAssembly(tempAssembly, xmlSerializerMappingKey2.Mapping);
						hashtable[xmlSerializerMappingKey2] = array[num];
					}
				}
			}
			return array;
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x000A838C File Offset: 0x000A658C
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of FromMappings which does not take an Evidence parameter. See http://go2.microsoft.com/fwlink/?LinkId=131738 for more information.")]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static XmlSerializer[] FromMappings(XmlMapping[] mappings, Evidence evidence)
		{
			if (mappings == null || mappings.Length == 0)
			{
				return new XmlSerializer[0];
			}
			if (XmlMapping.IsShallow(mappings))
			{
				return new XmlSerializer[0];
			}
			TempAssembly tempAssembly = new TempAssembly(mappings, new Type[0], null, null, evidence);
			XmlSerializerImplementation contract = tempAssembly.Contract;
			XmlSerializer[] array = new XmlSerializer[mappings.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (XmlSerializer)contract.TypedSerializers[mappings[i].Key];
			}
			return array;
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x000A8400 File Offset: 0x000A6600
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static Assembly GenerateSerializer(Type[] types, XmlMapping[] mappings)
		{
			return XmlSerializer.GenerateSerializer(types, mappings, new CompilerParameters
			{
				TempFiles = new TempFileCollection(),
				GenerateInMemory = false,
				IncludeDebugInformation = false
			});
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x000A8434 File Offset: 0x000A6634
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public static Assembly GenerateSerializer(Type[] types, XmlMapping[] mappings, CompilerParameters parameters)
		{
			if (types == null || types.Length == 0)
			{
				return null;
			}
			if (mappings == null)
			{
				throw new ArgumentNullException("mappings");
			}
			if (XmlMapping.IsShallow(mappings))
			{
				throw new InvalidOperationException(Res.GetString("XmlMelformMapping"));
			}
			Assembly assembly = null;
			foreach (Type type in types)
			{
				if (DynamicAssemblies.IsTypeDynamic(type))
				{
					throw new InvalidOperationException(Res.GetString("XmlPregenTypeDynamic", new object[]
					{
						type.FullName
					}));
				}
				if (assembly == null)
				{
					assembly = type.Assembly;
				}
				else if (type.Assembly != assembly)
				{
					throw new ArgumentException(Res.GetString("XmlPregenOrphanType", new object[]
					{
						type.FullName,
						assembly.Location
					}), "types");
				}
			}
			return TempAssembly.GenerateAssembly(mappings, types, null, null, XmlSerializerCompilerParameters.Create(parameters, true), assembly, new Hashtable());
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x000A8514 File Offset: 0x000A6714
		[__DynamicallyInvokable]
		public static XmlSerializer[] FromTypes(Type[] types)
		{
			if (types == null)
			{
				return new XmlSerializer[0];
			}
			XmlReflectionImporter xmlReflectionImporter = new XmlReflectionImporter();
			XmlTypeMapping[] array = new XmlTypeMapping[types.Length];
			for (int i = 0; i < types.Length; i++)
			{
				array[i] = xmlReflectionImporter.ImportTypeMapping(types[i]);
			}
			XmlMapping[] mappings = array;
			return XmlSerializer.FromMappings(mappings);
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x000A855C File Offset: 0x000A675C
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public static string GetXmlSerializerAssemblyName(Type type)
		{
			return XmlSerializer.GetXmlSerializerAssemblyName(type, null);
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x000A8565 File Offset: 0x000A6765
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public static string GetXmlSerializerAssemblyName(Type type, string defaultNamespace)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return Compiler.GetTempAssemblyName(type.Assembly.GetName(), defaultNamespace);
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06001EAF RID: 7855 RVA: 0x000A858C File Offset: 0x000A678C
		// (remove) Token: 0x06001EB0 RID: 7856 RVA: 0x000A85AA File Offset: 0x000A67AA
		public event XmlNodeEventHandler UnknownNode
		{
			add
			{
				this.events.OnUnknownNode = (XmlNodeEventHandler)Delegate.Combine(this.events.OnUnknownNode, value);
			}
			remove
			{
				this.events.OnUnknownNode = (XmlNodeEventHandler)Delegate.Remove(this.events.OnUnknownNode, value);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06001EB1 RID: 7857 RVA: 0x000A85C8 File Offset: 0x000A67C8
		// (remove) Token: 0x06001EB2 RID: 7858 RVA: 0x000A85E6 File Offset: 0x000A67E6
		public event XmlAttributeEventHandler UnknownAttribute
		{
			add
			{
				this.events.OnUnknownAttribute = (XmlAttributeEventHandler)Delegate.Combine(this.events.OnUnknownAttribute, value);
			}
			remove
			{
				this.events.OnUnknownAttribute = (XmlAttributeEventHandler)Delegate.Remove(this.events.OnUnknownAttribute, value);
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06001EB3 RID: 7859 RVA: 0x000A8604 File Offset: 0x000A6804
		// (remove) Token: 0x06001EB4 RID: 7860 RVA: 0x000A8622 File Offset: 0x000A6822
		public event XmlElementEventHandler UnknownElement
		{
			add
			{
				this.events.OnUnknownElement = (XmlElementEventHandler)Delegate.Combine(this.events.OnUnknownElement, value);
			}
			remove
			{
				this.events.OnUnknownElement = (XmlElementEventHandler)Delegate.Remove(this.events.OnUnknownElement, value);
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06001EB5 RID: 7861 RVA: 0x000A8640 File Offset: 0x000A6840
		// (remove) Token: 0x06001EB6 RID: 7862 RVA: 0x000A865E File Offset: 0x000A685E
		public event UnreferencedObjectEventHandler UnreferencedObject
		{
			add
			{
				this.events.OnUnreferencedObject = (UnreferencedObjectEventHandler)Delegate.Combine(this.events.OnUnreferencedObject, value);
			}
			remove
			{
				this.events.OnUnreferencedObject = (UnreferencedObjectEventHandler)Delegate.Remove(this.events.OnUnreferencedObject, value);
			}
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x000A867C File Offset: 0x000A687C
		protected virtual XmlSerializationReader CreateReader()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x000A8683 File Offset: 0x000A6883
		protected virtual object Deserialize(XmlSerializationReader reader)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x000A868A File Offset: 0x000A688A
		protected virtual XmlSerializationWriter CreateWriter()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x000A8691 File Offset: 0x000A6891
		protected virtual void Serialize(object o, XmlSerializationWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x000A8698 File Offset: 0x000A6898
		internal void SetTempAssembly(TempAssembly tempAssembly, XmlMapping mapping)
		{
			this.tempAssembly = tempAssembly;
			this.mapping = mapping;
			this.typedSerializer = true;
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x000A86B0 File Offset: 0x000A68B0
		private static XmlTypeMapping GetKnownMapping(Type type, string ns)
		{
			if (ns != null && ns != string.Empty)
			{
				return null;
			}
			TypeDesc typeDesc = (TypeDesc)TypeScope.PrimtiveTypes[type];
			if (typeDesc == null)
			{
				return null;
			}
			XmlTypeMapping xmlTypeMapping = new XmlTypeMapping(null, new ElementAccessor
			{
				Name = typeDesc.DataType.Name
			});
			xmlTypeMapping.SetKeyInternal(XmlMapping.GenerateKey(type, null, null));
			return xmlTypeMapping;
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x000A8714 File Offset: 0x000A6914
		private void SerializePrimitive(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces)
		{
			XmlSerializationPrimitiveWriter xmlSerializationPrimitiveWriter = new XmlSerializationPrimitiveWriter();
			xmlSerializationPrimitiveWriter.Init(xmlWriter, namespaces, null, null, null);
			switch (Type.GetTypeCode(this.primitiveType))
			{
			case TypeCode.Boolean:
				xmlSerializationPrimitiveWriter.Write_boolean(o);
				return;
			case TypeCode.Char:
				xmlSerializationPrimitiveWriter.Write_char(o);
				return;
			case TypeCode.SByte:
				xmlSerializationPrimitiveWriter.Write_byte(o);
				return;
			case TypeCode.Byte:
				xmlSerializationPrimitiveWriter.Write_unsignedByte(o);
				return;
			case TypeCode.Int16:
				xmlSerializationPrimitiveWriter.Write_short(o);
				return;
			case TypeCode.UInt16:
				xmlSerializationPrimitiveWriter.Write_unsignedShort(o);
				return;
			case TypeCode.Int32:
				xmlSerializationPrimitiveWriter.Write_int(o);
				return;
			case TypeCode.UInt32:
				xmlSerializationPrimitiveWriter.Write_unsignedInt(o);
				return;
			case TypeCode.Int64:
				xmlSerializationPrimitiveWriter.Write_long(o);
				return;
			case TypeCode.UInt64:
				xmlSerializationPrimitiveWriter.Write_unsignedLong(o);
				return;
			case TypeCode.Single:
				xmlSerializationPrimitiveWriter.Write_float(o);
				return;
			case TypeCode.Double:
				xmlSerializationPrimitiveWriter.Write_double(o);
				return;
			case TypeCode.Decimal:
				xmlSerializationPrimitiveWriter.Write_decimal(o);
				return;
			case TypeCode.DateTime:
				xmlSerializationPrimitiveWriter.Write_dateTime(o);
				return;
			case TypeCode.String:
				xmlSerializationPrimitiveWriter.Write_string(o);
				return;
			}
			if (this.primitiveType == typeof(XmlQualifiedName))
			{
				xmlSerializationPrimitiveWriter.Write_QName(o);
				return;
			}
			if (this.primitiveType == typeof(byte[]))
			{
				xmlSerializationPrimitiveWriter.Write_base64Binary(o);
				return;
			}
			if (this.primitiveType == typeof(Guid))
			{
				xmlSerializationPrimitiveWriter.Write_guid(o);
				return;
			}
			if (this.primitiveType == typeof(TimeSpan))
			{
				xmlSerializationPrimitiveWriter.Write_TimeSpan(o);
				return;
			}
			throw new InvalidOperationException(Res.GetString("XmlUnxpectedType", new object[]
			{
				this.primitiveType.FullName
			}));
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x000A88A0 File Offset: 0x000A6AA0
		private object DeserializePrimitive(XmlReader xmlReader, XmlDeserializationEvents events)
		{
			XmlSerializationPrimitiveReader xmlSerializationPrimitiveReader = new XmlSerializationPrimitiveReader();
			xmlSerializationPrimitiveReader.Init(xmlReader, events, null, null);
			switch (Type.GetTypeCode(this.primitiveType))
			{
			case TypeCode.Boolean:
				return xmlSerializationPrimitiveReader.Read_boolean();
			case TypeCode.Char:
				return xmlSerializationPrimitiveReader.Read_char();
			case TypeCode.SByte:
				return xmlSerializationPrimitiveReader.Read_byte();
			case TypeCode.Byte:
				return xmlSerializationPrimitiveReader.Read_unsignedByte();
			case TypeCode.Int16:
				return xmlSerializationPrimitiveReader.Read_short();
			case TypeCode.UInt16:
				return xmlSerializationPrimitiveReader.Read_unsignedShort();
			case TypeCode.Int32:
				return xmlSerializationPrimitiveReader.Read_int();
			case TypeCode.UInt32:
				return xmlSerializationPrimitiveReader.Read_unsignedInt();
			case TypeCode.Int64:
				return xmlSerializationPrimitiveReader.Read_long();
			case TypeCode.UInt64:
				return xmlSerializationPrimitiveReader.Read_unsignedLong();
			case TypeCode.Single:
				return xmlSerializationPrimitiveReader.Read_float();
			case TypeCode.Double:
				return xmlSerializationPrimitiveReader.Read_double();
			case TypeCode.Decimal:
				return xmlSerializationPrimitiveReader.Read_decimal();
			case TypeCode.DateTime:
				return xmlSerializationPrimitiveReader.Read_dateTime();
			case TypeCode.String:
				return xmlSerializationPrimitiveReader.Read_string();
			}
			object result;
			if (this.primitiveType == typeof(XmlQualifiedName))
			{
				result = xmlSerializationPrimitiveReader.Read_QName();
			}
			else if (this.primitiveType == typeof(byte[]))
			{
				result = xmlSerializationPrimitiveReader.Read_base64Binary();
			}
			else if (this.primitiveType == typeof(Guid))
			{
				result = xmlSerializationPrimitiveReader.Read_guid();
			}
			else
			{
				if (!(this.primitiveType == typeof(TimeSpan)) || !LocalAppContextSwitches.EnableTimeSpanSerialization)
				{
					throw new InvalidOperationException(Res.GetString("XmlUnxpectedType", new object[]
					{
						this.primitiveType.FullName
					}));
				}
				result = xmlSerializationPrimitiveReader.Read_TimeSpan();
			}
			return result;
		}

		// Token: 0x04000CE0 RID: 3296
		private TempAssembly tempAssembly;

		// Token: 0x04000CE1 RID: 3297
		private bool typedSerializer;

		// Token: 0x04000CE2 RID: 3298
		private Type primitiveType;

		// Token: 0x04000CE3 RID: 3299
		private XmlMapping mapping;

		// Token: 0x04000CE4 RID: 3300
		private XmlDeserializationEvents events;

		// Token: 0x04000CE5 RID: 3301
		private static TempAssemblyCache cache = new TempAssemblyCache();

		// Token: 0x04000CE6 RID: 3302
		private static volatile XmlSerializerNamespaces defaultNamespaces;

		// Token: 0x04000CE7 RID: 3303
		private static Hashtable xmlSerializerTable = new Hashtable();

		// Token: 0x02000487 RID: 1159
		private class XmlSerializerMappingKey
		{
			// Token: 0x06003111 RID: 12561 RVA: 0x0011DE4B File Offset: 0x0011C04B
			public XmlSerializerMappingKey(XmlMapping mapping)
			{
				this.Mapping = mapping;
			}

			// Token: 0x06003112 RID: 12562 RVA: 0x0011DE5C File Offset: 0x0011C05C
			public override bool Equals(object obj)
			{
				XmlSerializer.XmlSerializerMappingKey xmlSerializerMappingKey = obj as XmlSerializer.XmlSerializerMappingKey;
				return xmlSerializerMappingKey != null && !(this.Mapping.Key != xmlSerializerMappingKey.Mapping.Key) && !(this.Mapping.ElementName != xmlSerializerMappingKey.Mapping.ElementName) && !(this.Mapping.Namespace != xmlSerializerMappingKey.Mapping.Namespace) && this.Mapping.IsSoap == xmlSerializerMappingKey.Mapping.IsSoap;
			}

			// Token: 0x06003113 RID: 12563 RVA: 0x0011DEF0 File Offset: 0x0011C0F0
			public override int GetHashCode()
			{
				int num = this.Mapping.IsSoap ? 0 : 1;
				if (this.Mapping.Key != null)
				{
					num ^= this.Mapping.Key.GetHashCode();
				}
				if (this.Mapping.ElementName != null)
				{
					num ^= this.Mapping.ElementName.GetHashCode();
				}
				if (this.Mapping.Namespace != null)
				{
					num ^= this.Mapping.Namespace.GetHashCode();
				}
				return num;
			}

			// Token: 0x04001E06 RID: 7686
			public XmlMapping Mapping;
		}
	}
}
