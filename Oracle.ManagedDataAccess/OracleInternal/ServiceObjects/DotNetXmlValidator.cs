using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001A4 RID: 420
	internal class DotNetXmlValidator
	{
		// Token: 0x06000FCA RID: 4042 RVA: 0x000A3600 File Offset: 0x000A1800
		internal DotNetXmlValidator(string xslSchema)
		{
			this.m_readerSettings = this.CreateReaderValidationSettings(xslSchema);
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x000A3618 File Offset: 0x000A1818
		internal XmlReaderSettings CreateReaderValidationSettings(string xslSchema)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			xmlReaderSettings.ValidationFlags |= (XmlSchemaValidationFlags.ProcessSchemaLocation | XmlSchemaValidationFlags.ReportValidationWarnings);
			xmlReaderSettings.ValidationEventHandler += this.ValidationEventHandler;
			xmlReaderSettings.Schemas.Add(null, XmlReader.Create(new StringReader(xslSchema)));
			return xmlReaderSettings;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x000A366C File Offset: 0x000A186C
		internal bool Validate(XmlReader xmlReaderToValidate)
		{
			try
			{
				while (xmlReaderToValidate.Read() && !this.m_bHasError)
				{
				}
			}
			catch (Exception validatingException)
			{
				this.m_bHasError = true;
				this.m_validatingException = validatingException;
			}
			return !this.m_bHasError;
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x000A36B8 File Offset: 0x000A18B8
		private void ValidationEventHandler(object sender, ValidationEventArgs e)
		{
			if (e.Severity == XmlSeverityType.Error)
			{
				this.m_bHasError = true;
			}
			this.m_validatingException = e.Exception;
		}

		// Token: 0x04001272 RID: 4722
		internal Exception m_validatingException;

		// Token: 0x04001273 RID: 4723
		internal bool m_bHasError;

		// Token: 0x04001274 RID: 4724
		internal XmlReaderSettings m_readerSettings;
	}
}
