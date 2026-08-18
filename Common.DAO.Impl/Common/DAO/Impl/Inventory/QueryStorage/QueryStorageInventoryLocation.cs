using System;

namespace TechnoPro.Common.DAO.Impl.Inventory.QueryStorage
{
	// Token: 0x020000C1 RID: 193
	internal static class QueryStorageInventoryLocation
	{
		// Token: 0x04000299 RID: 665
		internal const string SQ_INVENTORY_LOCATION_IN_USE = "select 1 from InventoryV2_Product where LocationID = @locationid AND IsActive = 1\r\nUNION\r\nselect 1 from InventoryV2_LoanGroup where LocationID = @locationid";

		// Token: 0x0400029A RID: 666
		internal const string SQ_INVENTORY_LOCATIONS = "select * from InventoryV2_Location";

		// Token: 0x0400029B RID: 667
		internal const string SQ_INVENTORY_LOCATION_BY_ID = "select * from InventoryV2_Location where LocationID=@locationid";

		// Token: 0x0400029C RID: 668
		internal const string SQ_INVENTORY_LOCATION_SEARCH_BY_CONTAINING_TEXT = "SELECT * FROM InventoryV2_Location\r\n                where Campus LIKE '%'+@searchingtext+'%' OR\r\n\t                  Building LIKE '%'+@searchingtext+'%' OR\r\n\t                  RoomNumber LIKE '%'+@searchingtext+'%' OR\r\n\t                  LocationNotes LIKE '%'+@searchingtext+'%'";

		// Token: 0x0400029D RID: 669
		internal const string DQ_LOCATION_BY_ID = "if not exists (select 1 from InventoryV2_Product where LocationID = @locationid AND IsActive = 1\r\n\t\t\tUNION\r\n\t\t\tselect 1 from InventoryV2_LoanGroup where LocationID = @locationid\r\n\t\t\t)\r\n\tdelete from InventoryV2_Location where LocationID=@locationid";

		// Token: 0x0400029E RID: 670
		internal const string IQ_ADD_LOCATION = "insert into InventoryV2_Location (Campus, Building, RoomNumber, Seat, LocationNotes)\r\nvalues (@campus, @building, @roomnumber, @seat, @locationnotes)\r\nset @locationid=SCOPE_IDENTITY()";

		// Token: 0x0400029F RID: 671
		internal const string UQ_LOCATION_BY_ID = "update InventoryV2_Location\r\nset  Campus = @campus\r\n\t,Building = @building\r\n\t,RoomNumber = @roomnumber\r\n\t,Seat = @seat\r\n\t,LocationNotes = @locationnotes\r\nwhere LocationID = @locationid";
	}
}
