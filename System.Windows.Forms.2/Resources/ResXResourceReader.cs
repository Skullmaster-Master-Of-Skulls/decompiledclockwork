using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Xml;

namespace System.Resources
{
	// Token: 0x020000F1 RID: 241
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class ResXResourceReader : IResourceReader, IEnumerable, IDisposable
	{
		// Token: 0x0600036E RID: 878 RVA: 0x0000A77D File Offset: 0x0000897D
		private ResXResourceReader(ITypeResolutionService typeResolver)
		{
			this.typeResolver = typeResolver;
			this.aliasResolver = new ResXResourceReader.ReaderAliasResolver();
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000A797 File Offset: 0x00008997
		private ResXResourceReader(AssemblyName[] assemblyNames)
		{
			this.assemblyNames = assemblyNames;
			this.aliasResolver = new ResXResourceReader.ReaderAliasResolver();
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000A7B1 File Offset: 0x000089B1
		public ResXResourceReader(string fileName) : this(fileName, null, null)
		{
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000A7BC File Offset: 0x000089BC
		public ResXResourceReader(string fileName, ITypeResolutionService typeResolver) : this(fileName, typeResolver, null)
		{
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000A7C7 File Offset: 0x000089C7
		internal ResXResourceReader(string fileName, ITypeResolutionService typeResolver, IAliasResolver aliasResolver)
		{
			this.fileName = fileName;
			this.typeResolver = typeResolver;
			this.aliasResolver = aliasResolver;
			if (this.aliasResolver == null)
			{
				this.aliasResolver = new ResXResourceReader.ReaderAliasResolver();
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000A7F7 File Offset: 0x000089F7
		public ResXResourceReader(TextReader reader) : this(reader, null, null)
		{
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000A802 File Offset: 0x00008A02
		public ResXResourceReader(TextReader reader, ITypeResolutionService typeResolver) : this(reader, typeResolver, null)
		{
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000A80D File Offset: 0x00008A0D
		internal ResXResourceReader(TextReader reader, ITypeResolutionService typeResolver, IAliasResolver aliasResolver)
		{
			this.reader = reader;
			this.typeResolver = typeResolver;
			this.aliasResolver = aliasResolver;
			if (this.aliasResolver == null)
			{
				this.aliasResolver = new ResXResourceReader.ReaderAliasResolver();
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000A83D File Offset: 0x00008A3D
		public ResXResourceReader(Stream stream) : this(stream, null, null)
		{
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000A848 File Offset: 0x00008A48
		public ResXResourceReader(Stream stream, ITypeResolutionService typeResolver) : this(stream, typeResolver, null)
		{
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000A853 File Offset: 0x00008A53
		internal ResXResourceReader(Stream stream, ITypeResolutionService typeResolver, IAliasResolver aliasResolver)
		{
			this.stream = stream;
			this.typeResolver = typeResolver;
			this.aliasResolver = aliasResolver;
			if (this.aliasResolver == null)
			{
				this.aliasResolver = new ResXResourceReader.ReaderAliasResolver();
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000A883 File Offset: 0x00008A83
		public ResXResourceReader(Stream stream, AssemblyName[] assemblyNames) : this(stream, assemblyNames, null)
		{
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000A88E File Offset: 0x00008A8E
		internal ResXResourceReader(Stream stream, AssemblyName[] assemblyNames, IAliasResolver aliasResolver)
		{
			this.stream = stream;
			this.assemblyNames = assemblyNames;
			this.aliasResolver = aliasResolver;
			if (this.aliasResolver == null)
			{
				this.aliasResolver = new ResXResourceReader.ReaderAliasResolver();
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000A8BE File Offset: 0x00008ABE
		public ResXResourceReader(TextReader reader, AssemblyName[] assemblyNames) : this(reader, assemblyNames, null)
		{
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000A8C9 File Offset: 0x00008AC9
		internal ResXResourceReader(TextReader reader, AssemblyName[] assemblyNames, IAliasResolver aliasResolver)
		{
			this.reader = reader;
			this.assemblyNames = assemblyNames;
			this.aliasResolver = aliasResolver;
			if (this.aliasResolver == null)
			{
				this.aliasResolver = new ResXResourceReader.ReaderAliasResolver();
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000A8F9 File Offset: 0x00008AF9
		public ResXResourceReader(string fileName, AssemblyName[] assemblyNames) : this(fileName, assemblyNames, null)
		{
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000A904 File Offset: 0x00008B04
		internal ResXResourceReader(string fileName, AssemblyName[] assemblyNames, IAliasResolver aliasResolver)
		{
			this.fileName = fileName;
			this.assemblyNames = assemblyNames;
			this.aliasResolver = aliasResolver;
			if (this.aliasResolver == null)
			{
				this.aliasResolver = new ResXResourceReader.ReaderAliasResolver();
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000A934 File Offset: 0x00008B34
		~ResXResourceReader()
		{
			this.Dispose(false);
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000A964 File Offset: 0x00008B64
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0000A96C File Offset: 0x00008B6C
		public string BasePath
		{
			get
			{
				return this.basePath;
			}
			set
			{
				if (this.isReaderDirty)
				{
					throw new InvalidOperationException(SR.GetString("InvalidResXBasePathOperation"));
				}
				this.basePath = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000A98D File Offset: 0x00008B8D
		// (set) Token: 0x06000383 RID: 899 RVA: 0x0000A995 File Offset: 0x00008B95
		public bool UseResXDataNodes
		{
			get
			{
				return this.useResXDataNodes;
			}
			set
			{
				if (this.isReaderDirty)
				{
					throw new InvalidOperationException(SR.GetString("InvalidResXBasePathOperation"));
				}
				this.useResXDataNodes = value;
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public void Close()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000A9BE File Offset: 0x00008BBE
		void IDisposable.Dispose()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000A9D0 File Offset: 0x00008BD0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.fileName != null && this.stream != null)
				{
					this.stream.Close();
					this.stream = null;
				}
				if (this.reader != null)
				{
					this.reader.Close();
					this.reader = null;
				}
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000AA1C File Offset: 0x00008C1C
		private void SetupNameTable(XmlReader reader)
		{
			reader.NameTable.Add("type");
			reader.NameTable.Add("name");
			reader.NameTable.Add("data");
			reader.NameTable.Add("metadata");
			reader.NameTable.Add("mimetype");
			reader.NameTable.Add("value");
			reader.NameTable.Add("resheader");
			reader.NameTable.Add("version");
			reader.NameTable.Add("resmimetype");
			reader.NameTable.Add("reader");
			reader.NameTable.Add("writer");
			reader.NameTable.Add(ResXResourceWriter.BinSerializedObjectMimeType);
			reader.NameTable.Add(ResXResourceWriter.SoapSerializedObjectMimeType);
			reader.NameTable.Add("assembly");
			reader.NameTable.Add("alias");
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000AB28 File Offset: 0x00008D28
		private void EnsureResData()
		{
			if (this.resData == null)
			{
				this.resData = new ListDictionary();
				this.resMetadata = new ListDictionary();
				XmlTextReader xmlTextReader = null;
				try
				{
					if (this.fileContents != null)
					{
						xmlTextReader = new XmlTextReader(new StringReader(this.fileContents));
					}
					else if (this.reader != null)
					{
						xmlTextReader = new XmlTextReader(this.reader);
					}
					else if (this.fileName != null || this.stream != null)
					{
						if (this.stream == null)
						{
							this.stream = new FileStream(this.fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
						}
						xmlTextReader = new XmlTextReader(this.stream);
					}
					this.SetupNameTable(xmlTextReader);
					xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
					this.ParseXml(xmlTextReader);
				}
				finally
				{
					if (this.fileName != null && this.stream != null)
					{
						this.stream.Close();
						this.stream = null;
					}
				}
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000AC0C File Offset: 0x00008E0C
		public static ResXResourceReader FromFileContents(string fileContents)
		{
			return ResXResourceReader.FromFileContents(fileContents, null);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000AC18 File Offset: 0x00008E18
		public static ResXResourceReader FromFileContents(string fileContents, ITypeResolutionService typeResolver)
		{
			return new ResXResourceReader(typeResolver)
			{
				fileContents = fileContents
			};
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000AC34 File Offset: 0x00008E34
		public static ResXResourceReader FromFileContents(string fileContents, AssemblyName[] assemblyNames)
		{
			return new ResXResourceReader(assemblyNames)
			{
				fileContents = fileContents
			};
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000AC50 File Offset: 0x00008E50
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000AC58 File Offset: 0x00008E58
		public IDictionaryEnumerator GetEnumerator()
		{
			this.isReaderDirty = true;
			this.EnsureResData();
			return this.resData.GetEnumerator();
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000AC72 File Offset: 0x00008E72
		public IDictionaryEnumerator GetMetadataEnumerator()
		{
			this.EnsureResData();
			return this.resMetadata.GetEnumerator();
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000AC88 File Offset: 0x00008E88
		private Point GetPosition(XmlReader reader)
		{
			Point result = new Point(0, 0);
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			if (xmlLineInfo != null)
			{
				result.Y = xmlLineInfo.LineNumber;
				result.X = xmlLineInfo.LinePosition;
			}
			return result;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000ACC4 File Offset: 0x00008EC4
		private void ParseXml(XmlTextReader reader)
		{
			bool flag = false;
			try
			{
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						string localName = reader.LocalName;
						if (reader.LocalName.Equals("assembly"))
						{
							this.ParseAssemblyNode(reader, false);
						}
						else if (reader.LocalName.Equals("data"))
						{
							this.ParseDataNode(reader, false);
						}
						else if (reader.LocalName.Equals("resheader"))
						{
							this.ParseResHeaderNode(reader);
						}
						else if (reader.LocalName.Equals("metadata"))
						{
							this.ParseDataNode(reader, true);
						}
					}
				}
				flag = true;
			}
			catch (SerializationException ex)
			{
				Point position = this.GetPosition(reader);
				string @string = SR.GetString("SerializationException", new object[]
				{
					reader["type"],
					position.Y,
					position.X,
					ex.Message
				});
				XmlException innerException = new XmlException(@string, ex, position.Y, position.X);
				SerializationException ex2 = new SerializationException(@string, innerException);
				throw ex2;
			}
			catch (TargetInvocationException ex3)
			{
				Point position2 = this.GetPosition(reader);
				string string2 = SR.GetString("InvocationException", new object[]
				{
					reader["type"],
					position2.Y,
					position2.X,
					ex3.InnerException.Message
				});
				XmlException inner = new XmlException(string2, ex3.InnerException, position2.Y, position2.X);
				TargetInvocationException ex4 = new TargetInvocationException(string2, inner);
				throw ex4;
			}
			catch (XmlException ex5)
			{
				throw new ArgumentException(SR.GetString("InvalidResXFile", new object[]
				{
					ex5.Message
				}), ex5);
			}
			catch (Exception ex6)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex6))
				{
					throw;
				}
				Point position3 = this.GetPosition(reader);
				XmlException ex7 = new XmlException(ex6.Message, ex6, position3.Y, position3.X);
				throw new ArgumentException(SR.GetString("InvalidResXFile", new object[]
				{
					ex7.Message
				}), ex7);
			}
			finally
			{
				if (!flag)
				{
					this.resData = null;
					this.resMetadata = null;
				}
			}
			bool flag2 = false;
			if (object.Equals(this.resHeaderMimeType, ResXResourceWriter.ResMimeType))
			{
				Type typeFromHandle = typeof(ResXResourceReader);
				Type typeFromHandle2 = typeof(ResXResourceWriter);
				string text = this.resHeaderReaderType;
				string text2 = this.resHeaderWriterType;
				if (text != null && text.IndexOf(',') != -1)
				{
					text = text.Split(new char[]
					{
						','
					})[0].Trim();
				}
				if (text2 != null && text2.IndexOf(',') != -1)
				{
					text2 = text2.Split(new char[]
					{
						','
					})[0].Trim();
				}
				if (text != null && text2 != null && text.Equals(typeFromHandle.FullName) && text2.Equals(typeFromHandle2.FullName))
				{
					flag2 = true;
				}
			}
			if (!flag2)
			{
				this.resData = null;
				this.resMetadata = null;
				throw new ArgumentException(SR.GetString("InvalidResXFileReaderWriterTypes"));
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000B048 File Offset: 0x00009248
		private void ParseResHeaderNode(XmlReader reader)
		{
			string text = reader["name"];
			if (text != null)
			{
				reader.ReadStartElement();
				if (object.Equals(text, "version"))
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						this.resHeaderVersion = reader.ReadElementString();
						return;
					}
					this.resHeaderVersion = reader.Value.Trim();
					return;
				}
				else if (object.Equals(text, "resmimetype"))
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						this.resHeaderMimeType = reader.ReadElementString();
						return;
					}
					this.resHeaderMimeType = reader.Value.Trim();
					return;
				}
				else if (object.Equals(text, "reader"))
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						this.resHeaderReaderType = reader.ReadElementString();
						return;
					}
					this.resHeaderReaderType = reader.Value.Trim();
					return;
				}
				else if (object.Equals(text, "writer"))
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						this.resHeaderWriterType = reader.ReadElementString();
						return;
					}
					this.resHeaderWriterType = reader.Value.Trim();
					return;
				}
				else
				{
					string a = text.ToLower(CultureInfo.InvariantCulture);
					if (!(a == "version"))
					{
						if (!(a == "resmimetype"))
						{
							if (!(a == "reader"))
							{
								if (!(a == "writer"))
								{
									return;
								}
								if (reader.NodeType == XmlNodeType.Element)
								{
									this.resHeaderWriterType = reader.ReadElementString();
									return;
								}
								this.resHeaderWriterType = reader.Value.Trim();
							}
							else
							{
								if (reader.NodeType == XmlNodeType.Element)
								{
									this.resHeaderReaderType = reader.ReadElementString();
									return;
								}
								this.resHeaderReaderType = reader.Value.Trim();
								return;
							}
						}
						else
						{
							if (reader.NodeType == XmlNodeType.Element)
							{
								this.resHeaderMimeType = reader.ReadElementString();
								return;
							}
							this.resHeaderMimeType = reader.Value.Trim();
							return;
						}
					}
					else
					{
						if (reader.NodeType == XmlNodeType.Element)
						{
							this.resHeaderVersion = reader.ReadElementString();
							return;
						}
						this.resHeaderVersion = reader.Value.Trim();
						return;
					}
				}
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000B224 File Offset: 0x00009424
		private void ParseAssemblyNode(XmlReader reader, bool isMetaData)
		{
			string text = reader["alias"];
			string assemblyName = reader["name"];
			AssemblyName assemblyName2 = new AssemblyName(assemblyName);
			if (string.IsNullOrEmpty(text))
			{
				text = assemblyName2.Name;
			}
			this.aliasResolver.PushAlias(text, assemblyName2);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000B26C File Offset: 0x0000946C
		private void ParseDataNode(XmlTextReader reader, bool isMetaData)
		{
			DataNodeInfo dataNodeInfo = new DataNodeInfo();
			dataNodeInfo.Name = reader["name"];
			string text = reader["type"];
			string text2 = null;
			AssemblyName assemblyName = null;
			if (!string.IsNullOrEmpty(text))
			{
				text2 = this.GetAliasFromTypeName(text);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				assemblyName = this.aliasResolver.ResolveAlias(text2);
			}
			if (assemblyName != null)
			{
				dataNodeInfo.TypeName = this.GetTypeFromTypeName(text) + ", " + assemblyName.FullName;
			}
			else
			{
				dataNodeInfo.TypeName = reader["type"];
			}
			dataNodeInfo.MimeType = reader["mimetype"];
			bool flag = false;
			dataNodeInfo.ReaderPosition = this.GetPosition(reader);
			while (!flag && reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && (reader.LocalName.Equals("data") || reader.LocalName.Equals("metadata")))
				{
					flag = true;
				}
				else if (reader.NodeType == XmlNodeType.Element)
				{
					if (reader.Name.Equals("value"))
					{
						WhitespaceHandling whitespaceHandling = reader.WhitespaceHandling;
						try
						{
							reader.WhitespaceHandling = WhitespaceHandling.Significant;
							dataNodeInfo.ValueData = reader.ReadString();
							continue;
						}
						finally
						{
							reader.WhitespaceHandling = whitespaceHandling;
						}
					}
					if (reader.Name.Equals("comment"))
					{
						dataNodeInfo.Comment = reader.ReadString();
					}
				}
				else
				{
					dataNodeInfo.ValueData = reader.Value.Trim();
				}
			}
			if (dataNodeInfo.Name == null)
			{
				throw new ArgumentException(SR.GetString("InvalidResXResourceNoName", new object[]
				{
					dataNodeInfo.ValueData
				}));
			}
			ResXDataNode resXDataNode = new ResXDataNode(dataNodeInfo, this.BasePath);
			if (this.UseResXDataNodes)
			{
				this.resData[dataNodeInfo.Name] = resXDataNode;
				return;
			}
			IDictionary dictionary = isMetaData ? this.resMetadata : this.resData;
			if (this.assemblyNames == null)
			{
				dictionary[dataNodeInfo.Name] = resXDataNode.GetValue(this.typeResolver);
				return;
			}
			dictionary[dataNodeInfo.Name] = resXDataNode.GetValue(this.assemblyNames);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000B484 File Offset: 0x00009684
		private string GetAliasFromTypeName(string typeName)
		{
			int num = typeName.IndexOf(",");
			return typeName.Substring(num + 2);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000B4A8 File Offset: 0x000096A8
		private string GetTypeFromTypeName(string typeName)
		{
			int length = typeName.IndexOf(",");
			return typeName.Substring(0, length);
		}

		// Token: 0x040003D5 RID: 981
		private string fileName;

		// Token: 0x040003D6 RID: 982
		private TextReader reader;

		// Token: 0x040003D7 RID: 983
		private Stream stream;

		// Token: 0x040003D8 RID: 984
		private string fileContents;

		// Token: 0x040003D9 RID: 985
		private AssemblyName[] assemblyNames;

		// Token: 0x040003DA RID: 986
		private string basePath;

		// Token: 0x040003DB RID: 987
		private bool isReaderDirty;

		// Token: 0x040003DC RID: 988
		private ITypeResolutionService typeResolver;

		// Token: 0x040003DD RID: 989
		private IAliasResolver aliasResolver;

		// Token: 0x040003DE RID: 990
		private ListDictionary resData;

		// Token: 0x040003DF RID: 991
		private ListDictionary resMetadata;

		// Token: 0x040003E0 RID: 992
		private string resHeaderVersion;

		// Token: 0x040003E1 RID: 993
		private string resHeaderMimeType;

		// Token: 0x040003E2 RID: 994
		private string resHeaderReaderType;

		// Token: 0x040003E3 RID: 995
		private string resHeaderWriterType;

		// Token: 0x040003E4 RID: 996
		private bool useResXDataNodes;

		// Token: 0x02000544 RID: 1348
		private sealed class ReaderAliasResolver : IAliasResolver
		{
			// Token: 0x06005566 RID: 21862 RVA: 0x001663E8 File Offset: 0x001645E8
			internal ReaderAliasResolver()
			{
				this.cachedAliases = new Hashtable();
			}

			// Token: 0x06005567 RID: 21863 RVA: 0x001663FC File Offset: 0x001645FC
			public AssemblyName ResolveAlias(string alias)
			{
				AssemblyName result = null;
				if (this.cachedAliases != null)
				{
					result = (AssemblyName)this.cachedAliases[alias];
				}
				return result;
			}

			// Token: 0x06005568 RID: 21864 RVA: 0x00166426 File Offset: 0x00164626
			public void PushAlias(string alias, AssemblyName name)
			{
				if (this.cachedAliases != null && !string.IsNullOrEmpty(alias))
				{
					this.cachedAliases[alias] = name;
				}
			}

			// Token: 0x0400380C RID: 14348
			private Hashtable cachedAliases;
		}
	}
}
