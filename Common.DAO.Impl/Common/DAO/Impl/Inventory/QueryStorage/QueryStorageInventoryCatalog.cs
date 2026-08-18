using System;

namespace TechnoPro.Common.DAO.Impl.Inventory.QueryStorage
{
	// Token: 0x020000BD RID: 189
	internal static class QueryStorageInventoryCatalog
	{
		// Token: 0x04000270 RID: 624
		internal const string SQ_CATALOG_BY_ID = "select cat.CatalogID, cat.CatalogName, cat.CatalogDescription, cat.DateCreated,\r\n                    cat.WhoCreatedPersonId as personid, p.firstname as firstname, p.lastname as lastname, p.middlename as middlename, p.student_no as student_no, pg.mingroupid AS groupid\r\n            from InventoryV2_Catalog cat \r\n            left join people p on p.PersonID=cat.WhoCreatedPersonId\r\n            left join peoplemingroup pg on pg.PersonID=cat.WhoCreatedPersonId\r\n            where cat.CatalogID=@catalogid";

		// Token: 0x04000271 RID: 625
		internal const string SQ_CATALOG_BY_NAME = "select cat.CatalogID, cat.CatalogName, cat.CatalogDescription, cat.DateCreated,\r\n                    cat.WhoCreatedPersonId as personid, p.firstname as firstname, p.lastname as lastname, p.middlename as middlename, p.student_no as student_no, pg.mingroupid AS groupid\r\n            from InventoryV2_Catalog cat \r\n            left join people p on p.PersonID=cat.WhoCreatedPersonId\r\n            left join peoplemingroup pg on pg.PersonID=cat.WhoCreatedPersonId\r\n            where cat.CatalogName=@catalogname";

		// Token: 0x04000272 RID: 626
		internal const string SQ_CATALOGS = "select OrderID as CatalogID into #temp from SplitOrderIDs(@allowedcatalogids, ',')\r\n\r\n            select cat.CatalogID, cat.CatalogName, cat.CatalogDescription, cat.DateCreated,\r\n            cat.WhoCreatedPersonId as personid, p.firstname as firstname, p.lastname as lastname, p.middlename as middlename, p.student_no as student_no, pg.mingroupid AS groupid\r\n            from InventoryV2_Catalog cat \r\n            left join people p on p.PersonID=cat.WhoCreatedPersonId\r\n            left join peoplemingroup pg on pg.PersonID=cat.WhoCreatedPersonId\r\n            where cat.IsActive=1 and cat.CatalogID in (select CatalogID from #temp);\r\n\r\n            drop table #temp";

		// Token: 0x04000273 RID: 627
		internal const string SQ_ALL_CATALOGS = "select cat.CatalogID, cat.CatalogName, cat.CatalogDescription, cat.DateCreated,\r\n                    cat.WhoCreatedPersonId as personid, p.firstname as firstname, p.lastname as lastname, p.middlename as middlename, p.student_no as student_no, pg.mingroupid AS groupid\r\n            from InventoryV2_Catalog cat \r\n            left join people p on p.PersonID=cat.WhoCreatedPersonId\r\n            left join peoplemingroup pg on pg.PersonID=cat.WhoCreatedPersonId\r\n            where cat.IsActive=1";

		// Token: 0x04000274 RID: 628
		internal const string DQ_CATALOG_BY_ID = "delete from InventoryV2_Catalog \r\n                where CatalogID=@catalogid and not exists(select 1 from InventoryV2_Category where CatalogID=@catalogid)";

		// Token: 0x04000275 RID: 629
		internal const string IQ_CATALOG = "if not exists (select 1 from InventoryV2_Catalog where CatalogName=@catalogname)\r\n\t            begin\r\n\t\t            insert into InventoryV2_Catalog (CatalogName, CatalogDescription, WhoCreatedPersonId)\r\n\t\t            values (@catalogname, @catalogdescription, @whocreated)\r\n\t\t            set @catalogid=SCOPE_IDENTITY()\r\n\t            end";

		// Token: 0x04000276 RID: 630
		internal const string UQ_CATALOG_BY_ID = "update InventoryV2_Catalog\r\n            set CatalogDescription=@catalogdescription\r\n            where CatalogID=@catalogid";
	}
}
