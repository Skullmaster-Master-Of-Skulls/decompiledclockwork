using System;

namespace TechnoPro.Common.DAO.Impl.AlternativeFormat.QueryStorage
{
	// Token: 0x02000174 RID: 372
	public static class QueryStoragePublisher
	{
		// Token: 0x040006DE RID: 1758
		internal const string SQ_GET_PUBLISHER_BY_ID = "Select * from AlternativeFormat_Publisher where publisherid=@publisherid";

		// Token: 0x040006DF RID: 1759
		internal const string SQ_GET_PUBLISHER_BY_NAME = "Select * from AlternativeFormat_Publisher where publishername=@publishername";

		// Token: 0x040006E0 RID: 1760
		internal const string SQ_GET_PUBLISHERS = "Select * from AlternativeFormat_Publisher";

		// Token: 0x040006E1 RID: 1761
		internal const string DQ_DELETE_PUBLISHER_BY_ID = "if not exists (select 1 from AlternativeFormat_MediaContent where IsActive=1 AND PublisherID=@publisherid)\r\nbegin\r\n\tdelete from AlternativeFormat_Publisher where publisherid=@publisherid\r\nend";

		// Token: 0x040006E2 RID: 1762
		internal const string UQ_UPDATE_PUBLISHER = "IF NOT EXISTS(SELECT 1 FROM [AlternativeFormat_Publisher] WHERE PublisherName=@publishername and PublisherId <> @publisherid)\r\nBEGIN\r\n\tupdate [AlternativeFormat_Publisher]\r\n    set   [PublisherName]=@publishername\r\n        ,[PublisherDescription]=@publisherdescription\r\n        ,[PublisherNotes]=@publishernotes\r\n        ,[PublisherPhone]=@publisherphone\r\n        ,[PublisherAddress]=@publisheraddress\r\n        ,[PublisherFax]=@publisherfax\r\n        ,[PublisherEmail]=@publisheremail\r\n        ,[PublisherWebsite]=@publisherwebsite\r\n    where PublisherId=@publisherid\r\nEND";

		// Token: 0x040006E3 RID: 1763
		internal const string IQ_NEW_PUBLISHER = "SET @publisherid = 0\r\n\r\nIF NOT EXISTS(SELECT 1 FROM [AlternativeFormat_Publisher] WHERE PublisherName=@publishername)\r\nBEGIN\r\n\tINSERT INTO [AlternativeFormat_Publisher]\r\n        ([PublisherName]\r\n        ,[PublisherDescription]\r\n        ,[PublisherNotes]\r\n        ,[PublisherPhone]\r\n        ,[PublisherAddress]\r\n        ,[PublisherFax]\r\n        ,[PublisherEmail]\r\n        ,[PublisherWebsite])\r\n    VALUES\r\n        (@publishername\r\n        ,@publisherdescription\r\n        ,@publishernotes\r\n        ,@publisherphone\r\n        ,@publisheraddress\r\n        ,@publisherfax\r\n        ,@publisheremail\r\n        ,@publisherwebsite)\r\n\r\n\tSET @publisherid = SCOPE_IDENTITY()\r\nEND";
	}
}
