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
	// Token: 0x02000338 RID: 824
	public class XmlSerializer
	{
		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x0600283D RID: 10301 RVA: 0x000D05FC File Offset: 0x000CF5FC
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

		// Token: 0x0600283E RID: 10302 RVA: 0x000D0648 File Offset: 0x000CF648
		protected XmlSerializer()
		{
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x000D065C File Offset: 0x000CF65C
		public XmlSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace) : this(type, overrides, extraTypes, root, defaultNamespace, null, null)
		{
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x000D066D File Offset: 0x000CF66D
		public XmlSerializer(Type type, XmlRootAttribute root) : this(type, null, new Type[0], root, null, null, null)
		{
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x000D0681 File Offset: 0x000CF681
		public XmlSerializer(Type type, Type[] extraTypes) : this(type, null, extraTypes, null, null, null, null)
		{
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x000D0690 File Offset: 0x000CF690
		public XmlSerializer(Type type, XmlAttributeOverrides overrides) : this(type, overrides, new Type[0], null, null, null, null)
		{
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x000D06A4 File Offset: 0x000CF6A4
		public XmlSerializer(XmlTypeMapping xmlTypeMapping)
		{
			this.tempAssembly = XmlSerializer.GenerateTempAssembly(xmlTypeMapping);
			this.mapping = xmlTypeMapping;
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x000D06CB File Offset: 0x000CF6CB
		public XmlSerializer(Type type) : this(type, null)
		{
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x000D06D8 File Offset: 0x000CF6D8
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
				lock (XmlSerializer.cache)
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

		// Token: 0x06002846 RID: 10310 RVA: 0x000D080C File Offset: 0x000CF80C
		public XmlSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace, string location, Evidence evidence)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			XmlReflectionImporter xmlReflectionImporter = new XmlReflectionImporter(overrides, defaultNamespace);
			for (int i = 0; i < extraTypes.Length; i++)
			{
				xmlReflectionImporter.IncludeType(extraTypes[i]);
			}
			this.mapping = xmlReflectionImporter.ImportTypeMapping(type, root, defaultNamespace);
			if (location != null)
			{
				this.DemandForUserLocation();
			}
			this.tempAssembly = XmlSerializer.GenerateTempAssembly(this.mapping, type, defaultNamespace, location, evidence);
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x000D088C File Offset: 0x000CF88C
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		private void DemandForUserLocation()
		{
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x000D088E File Offset: 0x000CF88E
		internal static TempAssembly GenerateTempAssembly(XmlMapping xmlMapping)
		{
			return XmlSerializer.GenerateTempAssembly(xmlMapping, null, null);
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x000D0898 File Offset: 0x000CF898
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

		// Token: 0x0600284A RID: 10314 RVA: 0x000D08D4 File Offset: 0x000CF8D4
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

		// Token: 0x0600284B RID: 10315 RVA: 0x000D0902 File Offset: 0x000CF902
		public void Serialize(TextWriter textWriter, object o)
		{
			this.Serialize(textWriter, o, null);
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x000D0910 File Offset: 0x000CF910
		public void Serialize(TextWriter textWriter, object o, XmlSerializerNamespaces namespaces)
		{
			this.Serialize(new XmlTextWriter(textWriter)
			{
				Formatting = Formatting.Indented,
				Indentation = 2
			}, o, namespaces);
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x000D093B File Offset: 0x000CF93B
		public void Serialize(Stream stream, object o)
		{
			this.Serialize(stream, o, null);
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x000D0948 File Offset: 0x000CF948
		public void Serialize(Stream stream, object o, XmlSerializerNamespaces namespaces)
		{
			this.Serialize(new XmlTextWriter(stream, null)
			{
				Formatting = Formatting.Indented,
				Indentation = 2
			}, o, namespaces);
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x000D0974 File Offset: 0x000CF974
		public void Serialize(XmlWriter xmlWriter, object o)
		{
			this.Serialize(xmlWriter, o, null);
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x000D097F File Offset: 0x000CF97F
		public void Serialize(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces)
		{
			this.Serialize(xmlWriter, o, namespaces, null);
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x000D098B File Offset: 0x000CF98B
		public void Serialize(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces, string encodingStyle)
		{
			this.Serialize(xmlWriter, o, namespaces, encodingStyle, null);
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x000D099C File Offset: 0x000CF99C
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
							goto IL_B4;
						}
						finally
						{
							xmlSerializationWriter.Dispose();
						}
					}
					this.tempAssembly.InvokeWriter(this.mapping, xmlWriter, o, (namespaces == null || namespaces.Count == 0) ? XmlSerializer.DefaultNamespaces : namespaces, encodingStyle, id);
				}
				IL_B4:;
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
			catch
			{
				throw new InvalidOperationException(Res.GetString("XmlGenError"), null);
			}
			xmlWriter.Flush();
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x000D0ADC File Offset: 0x000CFADC
		public object Deserialize(Stream stream)
		{
			return this.Deserialize(new XmlTextReader(stream)
			{
				WhitespaceHandling = WhitespaceHandling.Significant,
				Normalization = true,
				XmlResolver = null
			}, null);
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x000D0B10 File Offset: 0x000CFB10
		public object Deserialize(TextReader textReader)
		{
			return this.Deserialize(new XmlTextReader(textReader)
			{
				WhitespaceHandling = WhitespaceHandling.Significant,
				Normalization = true,
				XmlResolver = null
			}, null);
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x000D0B41 File Offset: 0x000CFB41
		public object Deserialize(XmlReader xmlReader)
		{
			return this.Deserialize(xmlReader, null);
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x000D0B4B File Offset: 0x000CFB4B
		public object Deserialize(XmlReader xmlReader, XmlDeserializationEvents events)
		{
			return this.Deserialize(xmlReader, null, events);
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x000D0B56 File Offset: 0x000CFB56
		public object Deserialize(XmlReader xmlReader, string encodingStyle)
		{
			return this.Deserialize(xmlReader, encodingStyle, this.events);
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x000D0B68 File Offset: 0x000CFB68
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
			catch
			{
				if (xmlReader is IXmlLineInfo)
				{
					IXmlLineInfo xmlLineInfo2 = (IXmlLineInfo)xmlReader;
					throw new InvalidOperationException(Res.GetString("XmlSerializeErrorDetails", new object[]
					{
						xmlLineInfo2.LineNumber.ToString(CultureInfo.InvariantCulture),
						xmlLineInfo2.LinePosition.ToString(CultureInfo.InvariantCulture)
					}), null);
				}
				throw new InvalidOperationException(Res.GetString("XmlSerializeError"), null);
			}
			return result;
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x000D0D40 File Offset: 0x000CFD40
		public virtual bool CanDeserialize(XmlReader xmlReader)
		{
			if (this.primitiveType != null)
			{
				TypeDesc typeDesc = (TypeDesc)TypeScope.PrimtiveTypes[this.primitiveType];
				return xmlReader.IsStartElement(typeDesc.DataType.Name, string.Empty);
			}
			return this.tempAssembly != null && this.tempAssembly.CanRead(this.mapping, xmlReader);
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x000D0D9E File Offset: 0x000CFD9E
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static XmlSerializer[] FromMappings(XmlMapping[] mappings)
		{
			return XmlSerializer.FromMappings(mappings, null);
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x000D0DA8 File Offset: 0x000CFDA8
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static XmlSerializer[] FromMappings(XmlMapping[] mappings, Type type)
		{
			if (mappings == null || mappings.Length == 0)
			{
				return new XmlSerializer[0];
			}
			XmlSerializerImplementation xmlSerializerImplementation = null;
			if (((type == null) ? null : TempAssembly.LoadGeneratedAssembly(type, null, out xmlSerializerImplementation)) != null)
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

		// Token: 0x0600285C RID: 10332 RVA: 0x000D0EA0 File Offset: 0x000CFEA0
		private static XmlSerializer[] GetSerializersFromCache(XmlMapping[] mappings, Type type)
		{
			XmlSerializer[] array = new XmlSerializer[mappings.Length];
			Hashtable hashtable = null;
			lock (XmlSerializer.xmlSerializerTable)
			{
				hashtable = (XmlSerializer.xmlSerializerTable[type] as Hashtable);
				if (hashtable == null)
				{
					hashtable = new Hashtable();
					XmlSerializer.xmlSerializerTable[type] = hashtable;
				}
			}
			lock (hashtable)
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

		// Token: 0x0600285D RID: 10333 RVA: 0x000D10C8 File Offset: 0x000D00C8
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

		// Token: 0x0600285E RID: 10334 RVA: 0x000D1140 File Offset: 0x000D0140
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

		// Token: 0x0600285F RID: 10335 RVA: 0x000D1174 File Offset: 0x000D0174
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

		// Token: 0x06002860 RID: 10336 RVA: 0x000D124C File Offset: 0x000D024C
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
			return XmlSerializer.FromMappings(array);
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x000D1292 File Offset: 0x000D0292
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public static string GetXmlSerializerAssemblyName(Type type)
		{
			return XmlSerializer.GetXmlSerializerAssemblyName(type, null);
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x000D129B File Offset: 0x000D029B
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public static string GetXmlSerializerAssemblyName(Type type, string defaultNamespace)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return Compiler.GetTempAssemblyName(type.Assembly.GetName(), defaultNamespace);
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06002863 RID: 10339 RVA: 0x000D12BC File Offset: 0x000D02BC
		// (remove) Token: 0x06002864 RID: 10340 RVA: 0x000D12DA File Offset: 0x000D02DA
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

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06002865 RID: 10341 RVA: 0x000D12F8 File Offset: 0x000D02F8
		// (remove) Token: 0x06002866 RID: 10342 RVA: 0x000D1316 File Offset: 0x000D0316
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

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06002867 RID: 10343 RVA: 0x000D1334 File Offset: 0x000D0334
		// (remove) Token: 0x06002868 RID: 10344 RVA: 0x000D1352 File Offset: 0x000D0352
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

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06002869 RID: 10345 RVA: 0x000D1370 File Offset: 0x000D0370
		// (remove) Token: 0x0600286A RID: 10346 RVA: 0x000D138E File Offset: 0x000D038E
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

		// Token: 0x0600286B RID: 10347 RVA: 0x000D13AC File Offset: 0x000D03AC
		protected virtual XmlSerializationReader CreateReader()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x000D13B3 File Offset: 0x000D03B3
		protected virtual object Deserialize(XmlSerializationReader reader)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x000D13BA File Offset: 0x000D03BA
		protected virtual XmlSerializationWriter CreateWriter()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x000D13C1 File Offset: 0x000D03C1
		protected virtual void Serialize(object o, XmlSerializationWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x000D13C8 File Offset: 0x000D03C8
		internal void SetTempAssembly(TempAssembly tempAssembly, XmlMapping mapping)
		{
			this.tempAssembly = tempAssembly;
			this.mapping = mapping;
			this.typedSerializer = true;
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x000D13E0 File Offset: 0x000D03E0
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

		// Token: 0x06002871 RID: 10353 RVA: 0x000D1444 File Offset: 0x000D0444
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
			throw new InvalidOperationException(Res.GetString("XmlUnxpectedType", new object[]
			{
				this.primitiveType.FullName
			}));
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x000D15A4 File Offset: 0x000D05A4
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
			else
			{
				if (this.primitiveType != typeof(Guid))
				{
					throw new InvalidOperationException(Res.GetString("XmlUnxpectedType", new object[]
					{
						this.primitiveType.FullName
					}));
				}
				result = xmlSerializationPrimitiveReader.Read_guid();
			}
			return result;
		}

		// Token: 0x04001678 RID: 5752
		private TempAssembly tempAssembly;

		// Token: 0x04001679 RID: 5753
		private bool typedSerializer;

		// Token: 0x0400167A RID: 5754
		private Type primitiveType;

		// Token: 0x0400167B RID: 5755
		private XmlMapping mapping;

		// Token: 0x0400167C RID: 5756
		private XmlDeserializationEvents events = default(XmlDeserializationEvents);

		// Token: 0x0400167D RID: 5757
		private static TempAssemblyCache cache = new TempAssemblyCache();

		// Token: 0x0400167E RID: 5758
		private static XmlSerializerNamespaces defaultNamespaces;

		// Token: 0x0400167F RID: 5759
		private static Hashtable xmlSerializerTable = new Hashtable();

		// Token: 0x02000339 RID: 825
		private class XmlSerializerMappingKey
		{
			// Token: 0x06002874 RID: 10356 RVA: 0x000D1759 File Offset: 0x000D0759
			public XmlSerializerMappingKey(XmlMapping mapping)
			{
				this.Mapping = mapping;
			}

			// Token: 0x06002875 RID: 10357 RVA: 0x000D1768 File Offset: 0x000D0768
			public override bool Equals(object obj)
			{
				XmlSerializer.XmlSerializerMappingKey xmlSerializerMappingKey = obj as XmlSerializer.XmlSerializerMappingKey;
				return xmlSerializerMappingKey != null && !(this.Mapping.Key != xmlSerializerMappingKey.Mapping.Key) && !(this.Mapping.ElementName != xmlSerializerMappingKey.Mapping.ElementName) && !(this.Mapping.Namespace != xmlSerializerMappingKey.Mapping.Namespace) && this.Mapping.IsSoap == xmlSerializerMappingKey.Mapping.IsSoap;
			}

			// Token: 0x06002876 RID: 10358 RVA: 0x000D17FC File Offset: 0x000D07FC
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

			// Token: 0x04001680 RID: 5760
			public XmlMapping Mapping;
		}
	}
}
