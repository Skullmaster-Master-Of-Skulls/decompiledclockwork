using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace System.Resources
{
	// Token: 0x020000F3 RID: 243
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class ResXResourceWriter : IResourceWriter, IDisposable
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000B52B File Offset: 0x0000972B
		// (set) Token: 0x0600039B RID: 923 RVA: 0x0000B533 File Offset: 0x00009733
		public string BasePath
		{
			get
			{
				return this.basePath;
			}
			set
			{
				this.basePath = value;
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000B53C File Offset: 0x0000973C
		public ResXResourceWriter(string fileName)
		{
			this.fileName = fileName;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000B556 File Offset: 0x00009756
		public ResXResourceWriter(string fileName, Func<Type, string> typeNameConverter)
		{
			this.fileName = fileName;
			this.typeNameConverter = typeNameConverter;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000B577 File Offset: 0x00009777
		public ResXResourceWriter(Stream stream)
		{
			this.stream = stream;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000B591 File Offset: 0x00009791
		public ResXResourceWriter(Stream stream, Func<Type, string> typeNameConverter)
		{
			this.stream = stream;
			this.typeNameConverter = typeNameConverter;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000B5B2 File Offset: 0x000097B2
		public ResXResourceWriter(TextWriter textWriter)
		{
			this.textWriter = textWriter;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000B5CC File Offset: 0x000097CC
		public ResXResourceWriter(TextWriter textWriter, Func<Type, string> typeNameConverter)
		{
			this.textWriter = textWriter;
			this.typeNameConverter = typeNameConverter;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000B5F0 File Offset: 0x000097F0
		~ResXResourceWriter()
		{
			this.Dispose(false);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000B620 File Offset: 0x00009820
		private void InitializeWriter()
		{
			if (this.xmlTextWriter == null)
			{
				bool flag = false;
				if (this.textWriter != null)
				{
					this.textWriter.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
					flag = true;
					this.xmlTextWriter = new XmlTextWriter(this.textWriter);
				}
				else if (this.stream != null)
				{
					this.xmlTextWriter = new XmlTextWriter(this.stream, Encoding.UTF8);
				}
				else
				{
					this.xmlTextWriter = new XmlTextWriter(this.fileName, Encoding.UTF8);
				}
				this.xmlTextWriter.Formatting = Formatting.Indented;
				this.xmlTextWriter.Indentation = 2;
				if (!flag)
				{
					this.xmlTextWriter.WriteStartDocument();
				}
			}
			else
			{
				this.xmlTextWriter.WriteStartDocument();
			}
			this.xmlTextWriter.WriteStartElement("root");
			XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(ResXResourceWriter.ResourceSchema));
			xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
			this.xmlTextWriter.WriteNode(xmlTextReader, true);
			this.xmlTextWriter.WriteStartElement("resheader");
			this.xmlTextWriter.WriteAttributeString("name", "resmimetype");
			this.xmlTextWriter.WriteStartElement("value");
			this.xmlTextWriter.WriteString(ResXResourceWriter.ResMimeType);
			this.xmlTextWriter.WriteEndElement();
			this.xmlTextWriter.WriteEndElement();
			this.xmlTextWriter.WriteStartElement("resheader");
			this.xmlTextWriter.WriteAttributeString("name", "version");
			this.xmlTextWriter.WriteStartElement("value");
			this.xmlTextWriter.WriteString(ResXResourceWriter.Version);
			this.xmlTextWriter.WriteEndElement();
			this.xmlTextWriter.WriteEndElement();
			this.xmlTextWriter.WriteStartElement("resheader");
			this.xmlTextWriter.WriteAttributeString("name", "reader");
			this.xmlTextWriter.WriteStartElement("value");
			this.xmlTextWriter.WriteString(MultitargetUtil.GetAssemblyQualifiedName(typeof(ResXResourceReader), this.typeNameConverter));
			this.xmlTextWriter.WriteEndElement();
			this.xmlTextWriter.WriteEndElement();
			this.xmlTextWriter.WriteStartElement("resheader");
			this.xmlTextWriter.WriteAttributeString("name", "writer");
			this.xmlTextWriter.WriteStartElement("value");
			this.xmlTextWriter.WriteString(MultitargetUtil.GetAssemblyQualifiedName(typeof(ResXResourceWriter), this.typeNameConverter));
			this.xmlTextWriter.WriteEndElement();
			this.xmlTextWriter.WriteEndElement();
			this.initialized = true;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000B897 File Offset: 0x00009A97
		private XmlWriter Writer
		{
			get
			{
				if (!this.initialized)
				{
					this.InitializeWriter();
				}
				return this.xmlTextWriter;
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000B8AD File Offset: 0x00009AAD
		public virtual void AddAlias(string aliasName, AssemblyName assemblyName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (this.cachedAliases == null)
			{
				this.cachedAliases = new Hashtable();
			}
			this.cachedAliases[assemblyName.FullName] = aliasName;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000B8E2 File Offset: 0x00009AE2
		public void AddMetadata(string name, byte[] value)
		{
			this.AddDataRow("metadata", name, value);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000B8F1 File Offset: 0x00009AF1
		public void AddMetadata(string name, string value)
		{
			this.AddDataRow("metadata", name, value);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000B900 File Offset: 0x00009B00
		public void AddMetadata(string name, object value)
		{
			this.AddDataRow("metadata", name, value);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000B90F File Offset: 0x00009B0F
		public void AddResource(string name, byte[] value)
		{
			this.AddDataRow("data", name, value);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000B91E File Offset: 0x00009B1E
		public void AddResource(string name, object value)
		{
			if (value is ResXDataNode)
			{
				this.AddResource((ResXDataNode)value);
				return;
			}
			this.AddDataRow("data", name, value);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000B942 File Offset: 0x00009B42
		public void AddResource(string name, string value)
		{
			this.AddDataRow("data", name, value);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000B954 File Offset: 0x00009B54
		public void AddResource(ResXDataNode node)
		{
			ResXDataNode resXDataNode = node.DeepClone();
			ResXFileRef fileRef = resXDataNode.FileRef;
			string text = this.BasePath;
			if (!string.IsNullOrEmpty(text))
			{
				if (!text.EndsWith("\\"))
				{
					text += "\\";
				}
				if (fileRef != null)
				{
					fileRef.MakeFilePathRelative(text);
				}
			}
			DataNodeInfo dataNodeInfo = resXDataNode.GetDataNodeInfo();
			this.AddDataRow("data", dataNodeInfo.Name, dataNodeInfo.ValueData, dataNodeInfo.TypeName, dataNodeInfo.MimeType, dataNodeInfo.Comment);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000B9D1 File Offset: 0x00009BD1
		private void AddDataRow(string elementName, string name, byte[] value)
		{
			this.AddDataRow(elementName, name, ResXResourceWriter.ToBase64WrappedString(value), this.TypeNameWithAssembly(typeof(byte[])), null, null);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000B9F4 File Offset: 0x00009BF4
		private void AddDataRow(string elementName, string name, object value)
		{
			if (value is string)
			{
				this.AddDataRow(elementName, name, (string)value);
				return;
			}
			if (value is byte[])
			{
				this.AddDataRow(elementName, name, (byte[])value);
				return;
			}
			if (value is ResXFileRef)
			{
				ResXFileRef resXFileRef = (ResXFileRef)value;
				ResXDataNode resXDataNode = new ResXDataNode(name, resXFileRef, this.typeNameConverter);
				if (resXFileRef != null)
				{
					resXFileRef.MakeFilePathRelative(this.BasePath);
				}
				DataNodeInfo dataNodeInfo = resXDataNode.GetDataNodeInfo();
				this.AddDataRow(elementName, dataNodeInfo.Name, dataNodeInfo.ValueData, dataNodeInfo.TypeName, dataNodeInfo.MimeType, dataNodeInfo.Comment);
				return;
			}
			ResXDataNode resXDataNode2 = new ResXDataNode(name, value, this.typeNameConverter);
			DataNodeInfo dataNodeInfo2 = resXDataNode2.GetDataNodeInfo();
			this.AddDataRow(elementName, dataNodeInfo2.Name, dataNodeInfo2.ValueData, dataNodeInfo2.TypeName, dataNodeInfo2.MimeType, dataNodeInfo2.Comment);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000BAC8 File Offset: 0x00009CC8
		private void AddDataRow(string elementName, string name, string value)
		{
			if (value == null)
			{
				this.AddDataRow(elementName, name, value, MultitargetUtil.GetAssemblyQualifiedName(typeof(ResXNullRef), this.typeNameConverter), null, null);
				return;
			}
			this.AddDataRow(elementName, name, value, null, null, null);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000BAFC File Offset: 0x00009CFC
		private void AddDataRow(string elementName, string name, string value, string type, string mimeType, string comment)
		{
			if (this.hasBeenSaved)
			{
				throw new InvalidOperationException(SR.GetString("ResXResourceWriterSaved"));
			}
			string text = null;
			if (!string.IsNullOrEmpty(type) && elementName == "data")
			{
				string fullName = this.GetFullName(type);
				if (string.IsNullOrEmpty(fullName))
				{
					try
					{
						Type type2 = Type.GetType(type);
						if (type2 == typeof(string))
						{
							type = null;
						}
						else if (type2 != null)
						{
							fullName = this.GetFullName(MultitargetUtil.GetAssemblyQualifiedName(type2, this.typeNameConverter));
							text = this.GetAliasFromName(new AssemblyName(fullName));
						}
						goto IL_A2;
					}
					catch
					{
						goto IL_A2;
					}
				}
				text = this.GetAliasFromName(new AssemblyName(this.GetFullName(type)));
			}
			IL_A2:
			this.Writer.WriteStartElement(elementName);
			this.Writer.WriteAttributeString("name", name);
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(type) && elementName == "data")
			{
				string typeName = this.GetTypeName(type);
				string value2 = typeName + ", " + text;
				this.Writer.WriteAttributeString("type", value2);
			}
			else if (type != null)
			{
				this.Writer.WriteAttributeString("type", type);
			}
			if (mimeType != null)
			{
				this.Writer.WriteAttributeString("mimetype", mimeType);
			}
			if ((type == null && mimeType == null) || (type != null && type.StartsWith("System.Char", StringComparison.Ordinal)))
			{
				this.Writer.WriteAttributeString("xml", "space", null, "preserve");
			}
			this.Writer.WriteStartElement("value");
			if (!string.IsNullOrEmpty(value))
			{
				this.Writer.WriteString(value);
			}
			this.Writer.WriteEndElement();
			if (!string.IsNullOrEmpty(comment))
			{
				this.Writer.WriteStartElement("comment");
				this.Writer.WriteString(comment);
				this.Writer.WriteEndElement();
			}
			this.Writer.WriteEndElement();
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000BCF0 File Offset: 0x00009EF0
		private void AddAssemblyRow(string elementName, string alias, string name)
		{
			this.Writer.WriteStartElement(elementName);
			if (!string.IsNullOrEmpty(alias))
			{
				this.Writer.WriteAttributeString("alias", alias);
			}
			if (!string.IsNullOrEmpty(name))
			{
				this.Writer.WriteAttributeString("name", name);
			}
			this.Writer.WriteEndElement();
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000BD48 File Offset: 0x00009F48
		private string GetAliasFromName(AssemblyName assemblyName)
		{
			if (this.cachedAliases == null)
			{
				this.cachedAliases = new Hashtable();
			}
			string text = (string)this.cachedAliases[assemblyName.FullName];
			if (string.IsNullOrEmpty(text))
			{
				text = assemblyName.Name;
				this.AddAlias(text, assemblyName);
				this.AddAssemblyRow("assembly", text, assemblyName.FullName);
			}
			return text;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000BDA9 File Offset: 0x00009FA9
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000BDB1 File Offset: 0x00009FB1
		public virtual void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000BDC0 File Offset: 0x00009FC0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (!this.hasBeenSaved)
				{
					this.Generate();
				}
				if (this.xmlTextWriter != null)
				{
					this.xmlTextWriter.Close();
					this.xmlTextWriter = null;
				}
				if (this.stream != null)
				{
					this.stream.Close();
					this.stream = null;
				}
				if (this.textWriter != null)
				{
					this.textWriter.Close();
					this.textWriter = null;
				}
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000BE2C File Offset: 0x0000A02C
		private string GetTypeName(string typeName)
		{
			int num = typeName.IndexOf(",");
			if (num != -1)
			{
				return typeName.Substring(0, num);
			}
			return typeName;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000BE54 File Offset: 0x0000A054
		private string GetFullName(string typeName)
		{
			int num = typeName.IndexOf(",");
			if (num == -1)
			{
				return null;
			}
			return typeName.Substring(num + 2);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000BE7C File Offset: 0x0000A07C
		private static string ToBase64WrappedString(byte[] data)
		{
			string text = Convert.ToBase64String(data);
			if (text.Length > 80)
			{
				StringBuilder stringBuilder = new StringBuilder(text.Length + text.Length / 80 * 3);
				int i;
				for (i = 0; i < text.Length - 80; i += 80)
				{
					stringBuilder.Append("\r\n");
					stringBuilder.Append("        ");
					stringBuilder.Append(text, i, 80);
				}
				stringBuilder.Append("\r\n");
				stringBuilder.Append("        ");
				stringBuilder.Append(text, i, text.Length - i);
				stringBuilder.Append("\r\n");
				return stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000BF2C File Offset: 0x0000A12C
		private string TypeNameWithAssembly(Type type)
		{
			return MultitargetUtil.GetAssemblyQualifiedName(type, this.typeNameConverter);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000BF47 File Offset: 0x0000A147
		public void Generate()
		{
			if (this.hasBeenSaved)
			{
				throw new InvalidOperationException(SR.GetString("ResXResourceWriterSaved"));
			}
			this.hasBeenSaved = true;
			this.Writer.WriteEndElement();
			this.Writer.Flush();
		}

		// Token: 0x040003E5 RID: 997
		internal const string TypeStr = "type";

		// Token: 0x040003E6 RID: 998
		internal const string NameStr = "name";

		// Token: 0x040003E7 RID: 999
		internal const string DataStr = "data";

		// Token: 0x040003E8 RID: 1000
		internal const string MetadataStr = "metadata";

		// Token: 0x040003E9 RID: 1001
		internal const string MimeTypeStr = "mimetype";

		// Token: 0x040003EA RID: 1002
		internal const string ValueStr = "value";

		// Token: 0x040003EB RID: 1003
		internal const string ResHeaderStr = "resheader";

		// Token: 0x040003EC RID: 1004
		internal const string VersionStr = "version";

		// Token: 0x040003ED RID: 1005
		internal const string ResMimeTypeStr = "resmimetype";

		// Token: 0x040003EE RID: 1006
		internal const string ReaderStr = "reader";

		// Token: 0x040003EF RID: 1007
		internal const string WriterStr = "writer";

		// Token: 0x040003F0 RID: 1008
		internal const string CommentStr = "comment";

		// Token: 0x040003F1 RID: 1009
		internal const string AssemblyStr = "assembly";

		// Token: 0x040003F2 RID: 1010
		internal const string AliasStr = "alias";

		// Token: 0x040003F3 RID: 1011
		private Hashtable cachedAliases;

		// Token: 0x040003F4 RID: 1012
		private static TraceSwitch ResValueProviderSwitch = new TraceSwitch("ResX", "Debug the resource value provider");

		// Token: 0x040003F5 RID: 1013
		internal static readonly string Beta2CompatSerializedObjectMimeType = "text/microsoft-urt/psuedoml-serialized/base64";

		// Token: 0x040003F6 RID: 1014
		internal static readonly string CompatBinSerializedObjectMimeType = "text/microsoft-urt/binary-serialized/base64";

		// Token: 0x040003F7 RID: 1015
		internal static readonly string CompatSoapSerializedObjectMimeType = "text/microsoft-urt/soap-serialized/base64";

		// Token: 0x040003F8 RID: 1016
		public static readonly string BinSerializedObjectMimeType = "application/x-microsoft.net.object.binary.base64";

		// Token: 0x040003F9 RID: 1017
		public static readonly string SoapSerializedObjectMimeType = "application/x-microsoft.net.object.soap.base64";

		// Token: 0x040003FA RID: 1018
		public static readonly string DefaultSerializedObjectMimeType = ResXResourceWriter.BinSerializedObjectMimeType;

		// Token: 0x040003FB RID: 1019
		public static readonly string ByteArraySerializedObjectMimeType = "application/x-microsoft.net.object.bytearray.base64";

		// Token: 0x040003FC RID: 1020
		public static readonly string ResMimeType = "text/microsoft-resx";

		// Token: 0x040003FD RID: 1021
		public static readonly string Version = "2.0";

		// Token: 0x040003FE RID: 1022
		public static readonly string ResourceSchema = string.Concat(new string[]
		{
			"\r\n    <!-- \r\n    Microsoft ResX Schema \r\n    \r\n    Version ",
			ResXResourceWriter.Version,
			"\r\n    \r\n    The primary goals of this format is to allow a simple XML format \r\n    that is mostly human readable. The generation and parsing of the \r\n    various data types are done through the TypeConverter classes \r\n    associated with the data types.\r\n    \r\n    Example:\r\n    \r\n    ... ado.net/XML headers & schema ...\r\n    <resheader name=\"resmimetype\">text/microsoft-resx</resheader>\r\n    <resheader name=\"version\">",
			ResXResourceWriter.Version,
			"</resheader>\r\n    <resheader name=\"reader\">System.Resources.ResXResourceReader, System.Windows.Forms, ...</resheader>\r\n    <resheader name=\"writer\">System.Resources.ResXResourceWriter, System.Windows.Forms, ...</resheader>\r\n    <data name=\"Name1\"><value>this is my long string</value><comment>this is a comment</comment></data>\r\n    <data name=\"Color1\" type=\"System.Drawing.Color, System.Drawing\">Blue</data>\r\n    <data name=\"Bitmap1\" mimetype=\"",
			ResXResourceWriter.BinSerializedObjectMimeType,
			"\">\r\n        <value>[base64 mime encoded serialized .NET Framework object]</value>\r\n    </data>\r\n    <data name=\"Icon1\" type=\"System.Drawing.Icon, System.Drawing\" mimetype=\"",
			ResXResourceWriter.ByteArraySerializedObjectMimeType,
			"\">\r\n        <value>[base64 mime encoded string representing a byte array form of the .NET Framework object]</value>\r\n        <comment>This is a comment</comment>\r\n    </data>\r\n                \r\n    There are any number of \"resheader\" rows that contain simple \r\n    name/value pairs.\r\n    \r\n    Each data row contains a name, and value. The row also contains a \r\n    type or mimetype. Type corresponds to a .NET class that support \r\n    text/value conversion through the TypeConverter architecture. \r\n    Classes that don't support this are serialized and stored with the \r\n    mimetype set.\r\n    \r\n    The mimetype is used for serialized objects, and tells the \r\n    ResXResourceReader how to depersist the object. This is currently not \r\n    extensible. For a given mimetype the value must be set accordingly:\r\n    \r\n    Note - ",
			ResXResourceWriter.BinSerializedObjectMimeType,
			" is the format \r\n    that the ResXResourceWriter will generate, however the reader can \r\n    read any of the formats listed below.\r\n    \r\n    mimetype: ",
			ResXResourceWriter.BinSerializedObjectMimeType,
			"\r\n    value   : The object must be serialized with \r\n            : System.Runtime.Serialization.Formatters.Binary.BinaryFormatter\r\n            : and then encoded with base64 encoding.\r\n    \r\n    mimetype: ",
			ResXResourceWriter.SoapSerializedObjectMimeType,
			"\r\n    value   : The object must be serialized with \r\n            : System.Runtime.Serialization.Formatters.Soap.SoapFormatter\r\n            : and then encoded with base64 encoding.\r\n\r\n    mimetype: ",
			ResXResourceWriter.ByteArraySerializedObjectMimeType,
			"\r\n    value   : The object must be serialized into a byte array \r\n            : using a System.ComponentModel.TypeConverter\r\n            : and then encoded with base64 encoding.\r\n    -->\r\n    <xsd:schema id=\"root\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\r\n        <xsd:import namespace=\"http://www.w3.org/XML/1998/namespace\"/>\r\n        <xsd:element name=\"root\" msdata:IsDataSet=\"true\">\r\n            <xsd:complexType>\r\n                <xsd:choice maxOccurs=\"unbounded\">\r\n                    <xsd:element name=\"metadata\">\r\n                        <xsd:complexType>\r\n                            <xsd:sequence>\r\n                            <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\"/>\r\n                            </xsd:sequence>\r\n                            <xsd:attribute name=\"name\" use=\"required\" type=\"xsd:string\"/>\r\n                            <xsd:attribute name=\"type\" type=\"xsd:string\"/>\r\n                            <xsd:attribute name=\"mimetype\" type=\"xsd:string\"/>\r\n                            <xsd:attribute ref=\"xml:space\"/>                            \r\n                        </xsd:complexType>\r\n                    </xsd:element>\r\n                    <xsd:element name=\"assembly\">\r\n                      <xsd:complexType>\r\n                        <xsd:attribute name=\"alias\" type=\"xsd:string\"/>\r\n                        <xsd:attribute name=\"name\" type=\"xsd:string\"/>\r\n                      </xsd:complexType>\r\n                    </xsd:element>\r\n                    <xsd:element name=\"data\">\r\n                        <xsd:complexType>\r\n                            <xsd:sequence>\r\n                                <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"1\" />\r\n                                <xsd:element name=\"comment\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"2\" />\r\n                            </xsd:sequence>\r\n                            <xsd:attribute name=\"name\" type=\"xsd:string\" use=\"required\" msdata:Ordinal=\"1\" />\r\n                            <xsd:attribute name=\"type\" type=\"xsd:string\" msdata:Ordinal=\"3\" />\r\n                            <xsd:attribute name=\"mimetype\" type=\"xsd:string\" msdata:Ordinal=\"4\" />\r\n                            <xsd:attribute ref=\"xml:space\"/>\r\n                        </xsd:complexType>\r\n                    </xsd:element>\r\n                    <xsd:element name=\"resheader\">\r\n                        <xsd:complexType>\r\n                            <xsd:sequence>\r\n                                <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"1\" />\r\n                            </xsd:sequence>\r\n                            <xsd:attribute name=\"name\" type=\"xsd:string\" use=\"required\" />\r\n                        </xsd:complexType>\r\n                    </xsd:element>\r\n                </xsd:choice>\r\n            </xsd:complexType>\r\n        </xsd:element>\r\n        </xsd:schema>\r\n        "
		});

		// Token: 0x040003FF RID: 1023
		private IFormatter binaryFormatter = new BinaryFormatter();

		// Token: 0x04000400 RID: 1024
		private string fileName;

		// Token: 0x04000401 RID: 1025
		private Stream stream;

		// Token: 0x04000402 RID: 1026
		private TextWriter textWriter;

		// Token: 0x04000403 RID: 1027
		private XmlTextWriter xmlTextWriter;

		// Token: 0x04000404 RID: 1028
		private string basePath;

		// Token: 0x04000405 RID: 1029
		private bool hasBeenSaved;

		// Token: 0x04000406 RID: 1030
		private bool initialized;

		// Token: 0x04000407 RID: 1031
		private Func<Type, string> typeNameConverter;
	}
}
