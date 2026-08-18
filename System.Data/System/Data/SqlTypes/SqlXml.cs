using System;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000378 RID: 888
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public sealed class SqlXml : INullable, IXmlSerializable
	{
		// Token: 0x06002F42 RID: 12098 RVA: 0x002D3CF8 File Offset: 0x002D30F8
		public SqlXml()
		{
			this.SetNull();
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x002D3D18 File Offset: 0x002D3118
		private SqlXml(bool fNull)
		{
			this.SetNull();
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x002D3D38 File Offset: 0x002D3138
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

		// Token: 0x06002F45 RID: 12101 RVA: 0x002D3D78 File Offset: 0x002D3178
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

		// Token: 0x06002F46 RID: 12102 RVA: 0x002D3DB8 File Offset: 0x002D31B8
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
			XmlReader result = null;
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
			if (this.createSqlReaderMethodInfo == null)
			{
				this.createSqlReaderMethodInfo = typeof(XmlReader).GetMethod("CreateSqlReader", BindingFlags.Static | BindingFlags.NonPublic);
			}
			object[] array = new object[3];
			array[0] = sqlXmlStreamWrapper;
			array[1] = xmlReaderSettings;
			object[] parameters = array;
			new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
			try
			{
				result = (XmlReader)this.createSqlReaderMethodInfo.Invoke(null, parameters);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			this.firstCreateReader = false;
			return result;
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06002F47 RID: 12103 RVA: 0x002D3E98 File Offset: 0x002D3298
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002F48 RID: 12104 RVA: 0x002D3EB8 File Offset: 0x002D32B8
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

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06002F49 RID: 12105 RVA: 0x002D3F38 File Offset: 0x002D3338
		public static SqlXml Null
		{
			get
			{
				return new SqlXml(true);
			}
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x002D3F58 File Offset: 0x002D3358
		private void SetNull()
		{
			this.m_fNotNull = false;
			this.m_stream = null;
			this.firstCreateReader = true;
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x002D3F88 File Offset: 0x002D3388
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

		// Token: 0x06002F4C RID: 12108 RVA: 0x002D4018 File Offset: 0x002D3418
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x002D4028 File Offset: 0x002D3428
		void IXmlSerializable.ReadXml(XmlReader r)
		{
			string attribute = r.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
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

		// Token: 0x06002F4E RID: 12110 RVA: 0x002D40B8 File Offset: 0x002D34B8
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
			}
			else
			{
				SqlXmlStreamWrapper sqlXmlStreamWrapper = new SqlXmlStreamWrapper(this.m_stream);
				if (sqlXmlStreamWrapper.CanSeek && sqlXmlStreamWrapper.Position != 0L)
				{
					sqlXmlStreamWrapper.Seek(0L, SeekOrigin.Begin);
				}
				StreamReader streamReader = new StreamReader(sqlXmlStreamWrapper);
				char[] buffer = new char[4096];
				for (int i = streamReader.Read(buffer, 0, 4096); i > 0; i = streamReader.Read(buffer, 0, 4096))
				{
					writer.WriteRaw(buffer, 0, i);
				}
			}
			writer.Flush();
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x002D4158 File Offset: 0x002D3558
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04001D67 RID: 7527
		private bool m_fNotNull;

		// Token: 0x04001D68 RID: 7528
		private Stream m_stream;

		// Token: 0x04001D69 RID: 7529
		private bool firstCreateReader;

		// Token: 0x04001D6A RID: 7530
		private MethodInfo createSqlReaderMethodInfo;
	}
}
