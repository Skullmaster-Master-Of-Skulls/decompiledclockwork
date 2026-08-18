using System;
using System.Data.Common;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000189 RID: 393
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public sealed class SqlXml : INullable, IXmlSerializable
	{
		// Token: 0x06001798 RID: 6040 RVA: 0x000A88C4 File Offset: 0x000A7CC4
		public SqlXml()
		{
			this.SetNull();
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x000A88E0 File Offset: 0x000A7CE0
		private SqlXml(bool fNull)
		{
			this.SetNull();
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x000A88FC File Offset: 0x000A7CFC
		public SqlXml(XmlReader value)
		{
			if (value == null)
			{
				this.SetNull();
				return;
			}
			this.m_fNotNull = true;
			this.firstCreateReader = true;
			this.m_stream = this.CreateMemoryStreamFromXmlReader(value);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x000A8934 File Offset: 0x000A7D34
		public SqlXml(Stream value)
		{
			if (value == null)
			{
				this.SetNull();
				return;
			}
			this.firstCreateReader = true;
			this.m_fNotNull = true;
			this.m_stream = value;
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x000A8968 File Offset: 0x000A7D68
		public XmlReader CreateReader()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			SqlXmlStreamWrapper sqlXmlStreamWrapper = new SqlXmlStreamWrapper(this.m_stream);
			if ((!this.firstCreateReader || sqlXmlStreamWrapper.CanSeek) && sqlXmlStreamWrapper.Position != 0L)
			{
				sqlXmlStreamWrapper.Seek(0L, SeekOrigin.Begin);
			}
			if (this.createSqlReaderMethodInfo == null)
			{
				this.createSqlReaderMethodInfo = SqlXml.CreateSqlReaderMethodInfo;
			}
			XmlReader result = SqlXml.CreateSqlXmlReader(sqlXmlStreamWrapper, false, false);
			this.firstCreateReader = false;
			return result;
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x000A89DC File Offset: 0x000A7DDC
		internal static XmlReader CreateSqlXmlReader(Stream stream, bool closeInput = false, bool throwTargetInvocationExceptions = false)
		{
			XmlReaderSettings arg = closeInput ? SqlXml.DefaultXmlReaderSettingsCloseInput : SqlXml.DefaultXmlReaderSettings;
			XmlReader result;
			try
			{
				result = SqlXml.sqlReaderDelegate(stream, arg, null);
			}
			catch (Exception ex)
			{
				if (!throwTargetInvocationExceptions || !ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw new TargetInvocationException(ex);
			}
			return result;
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x000A8A40 File Offset: 0x000A7E40
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private static Func<Stream, XmlReaderSettings, XmlParserContext, XmlReader> CreateSqlReaderDelegate()
		{
			return (Func<Stream, XmlReaderSettings, XmlParserContext, XmlReader>)Delegate.CreateDelegate(typeof(Func<Stream, XmlReaderSettings, XmlParserContext, XmlReader>), SqlXml.CreateSqlReaderMethodInfo);
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x000A8A68 File Offset: 0x000A7E68
		private static MethodInfo CreateSqlReaderMethodInfo
		{
			get
			{
				if (SqlXml.s_createSqlReaderMethodInfo == null)
				{
					SqlXml.s_createSqlReaderMethodInfo = typeof(XmlReader).GetMethod("CreateSqlReader", BindingFlags.Static | BindingFlags.NonPublic);
				}
				return SqlXml.s_createSqlReaderMethodInfo;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x000A8AA4 File Offset: 0x000A7EA4
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x000A8ABC File Offset: 0x000A7EBC
		public string Value
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				StringWriter stringWriter = new StringWriter(null);
				XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
				{
					CloseOutput = false,
					ConformanceLevel = ConformanceLevel.Fragment
				});
				XmlReader xmlReader = this.CreateReader();
				if (xmlReader.ReadState == ReadState.Initial)
				{
					xmlReader.Read();
				}
				while (!xmlReader.EOF)
				{
					xmlWriter.WriteNode(xmlReader, true);
				}
				xmlWriter.Flush();
				return stringWriter.ToString();
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x000A8B30 File Offset: 0x000A7F30
		public static SqlXml Null
		{
			get
			{
				return new SqlXml(true);
			}
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x000A8B44 File Offset: 0x000A7F44
		private void SetNull()
		{
			this.m_fNotNull = false;
			this.m_stream = null;
			this.firstCreateReader = true;
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x000A8B68 File Offset: 0x000A7F68
		private Stream CreateMemoryStreamFromXmlReader(XmlReader reader)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.CloseOutput = false;
			xmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;
			xmlWriterSettings.Encoding = Encoding.GetEncoding("utf-16");
			xmlWriterSettings.OmitXmlDeclaration = true;
			MemoryStream memoryStream = new MemoryStream();
			XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
			if (reader.ReadState == ReadState.Closed)
			{
				throw new InvalidOperationException(SQLResource.ClosedXmlReaderMessage);
			}
			if (reader.ReadState == ReadState.Initial)
			{
				reader.Read();
			}
			while (!reader.EOF)
			{
				xmlWriter.WriteNode(reader, true);
			}
			xmlWriter.Flush();
			memoryStream.Seek(0L, SeekOrigin.Begin);
			return memoryStream;
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x000A8BF4 File Offset: 0x000A7FF4
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x000A8C04 File Offset: 0x000A8004
		void IXmlSerializable.ReadXml(XmlReader r)
		{
			string attribute = r.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				r.ReadInnerXml();
				this.SetNull();
				return;
			}
			this.m_fNotNull = true;
			this.firstCreateReader = true;
			this.m_stream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(this.m_stream);
			streamWriter.Write(r.ReadInnerXml());
			streamWriter.Flush();
			if (this.m_stream.CanSeek)
			{
				this.m_stream.Seek(0L, SeekOrigin.Begin);
			}
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x000A8C90 File Offset: 0x000A8090
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
			}
			else
			{
				XmlReader xmlReader = this.CreateReader();
				if (xmlReader.ReadState == ReadState.Initial)
				{
					xmlReader.Read();
				}
				while (!xmlReader.EOF)
				{
					writer.WriteNode(xmlReader, true);
				}
			}
			writer.Flush();
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x000A8CF0 File Offset: 0x000A80F0
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04000E50 RID: 3664
		private bool m_fNotNull;

		// Token: 0x04000E51 RID: 3665
		private Stream m_stream;

		// Token: 0x04000E52 RID: 3666
		private bool firstCreateReader;

		// Token: 0x04000E53 RID: 3667
		private MethodInfo createSqlReaderMethodInfo;

		// Token: 0x04000E54 RID: 3668
		private static readonly Func<Stream, XmlReaderSettings, XmlParserContext, XmlReader> sqlReaderDelegate = SqlXml.CreateSqlReaderDelegate();

		// Token: 0x04000E55 RID: 3669
		private static readonly XmlReaderSettings DefaultXmlReaderSettings = new XmlReaderSettings
		{
			ConformanceLevel = ConformanceLevel.Fragment
		};

		// Token: 0x04000E56 RID: 3670
		private static readonly XmlReaderSettings DefaultXmlReaderSettingsCloseInput = new XmlReaderSettings
		{
			ConformanceLevel = ConformanceLevel.Fragment,
			CloseInput = true
		};

		// Token: 0x04000E57 RID: 3671
		private static MethodInfo s_createSqlReaderMethodInfo;
	}
}
