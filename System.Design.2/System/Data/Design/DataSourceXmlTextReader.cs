using System;
using System.IO;
using System.Xml;

namespace System.Data.Design
{
	// Token: 0x0200022D RID: 557
	internal class DataSourceXmlTextReader : XmlTextReader
	{
		// Token: 0x060014A4 RID: 5284 RVA: 0x00076993 File Offset: 0x00074B93
		internal DataSourceXmlTextReader(DesignDataSource dataSource, TextReader textReader, string baseURI) : base(baseURI, textReader)
		{
			base.DtdProcessing = DtdProcessing.Ignore;
			this.dataSource = dataSource;
			this.readingDataSource = false;
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x000769B2 File Offset: 0x00074BB2
		internal DataSourceXmlTextReader(DesignDataSource dataSource, Stream stream, string baseURI) : base(baseURI, stream)
		{
			base.DtdProcessing = DtdProcessing.Ignore;
			this.dataSource = dataSource;
			this.readingDataSource = false;
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x000769D4 File Offset: 0x00074BD4
		public override bool Read()
		{
			bool flag = base.Read();
			if (flag && !this.readingDataSource && this.NodeType == XmlNodeType.Element && this.LocalName == "DataSource" && this.NamespaceURI == "urn:schemas-microsoft-com:xml-msdatasource")
			{
				this.readingDataSource = true;
				this.dataSource.ReadDataSourceExtraInformation(this);
				flag = !this.EOF;
			}
			return flag;
		}

		// Token: 0x04000AE6 RID: 2790
		private DesignDataSource dataSource;

		// Token: 0x04000AE7 RID: 2791
		private bool readingDataSource;
	}
}
