using System;

namespace TechnoPro.Common.DAO.Impl.Settings
{
	// Token: 0x02000049 RID: 73
	internal static class QueryStorageSpecialControl
	{
		// Token: 0x040000D1 RID: 209
		internal const string QS_SPECIAL_CONTROL_VALUE_STRING = "SELECT TOP 1 ps.valtext,ps.valbytes,ps.valint\r\nFROM perstudentdata2 ps\r\nWHERE ps.PersonID=@pid AND ps.SpecialControlType=@specialControlType\r\nORDER BY ps.controlID DESC";

		// Token: 0x040000D2 RID: 210
		internal const string QS_SPECIAL_CONTROL_VALUE_DATE = "SELECT TOP 1 ps.valtext,ps.valbytes,ps.valdate\r\nFROM perstudentdata2 ps\r\nWHERE ps.PersonID=@pid AND ps.SpecialControlType=@specialControlType\r\nORDER BY ps.controlID DESC";

		// Token: 0x040000D3 RID: 211
		internal const string QS_SPECIAL_CONTROL_VALUE_INT = "SELECT TOP 1 ps.valtext,ps.valint,ps.valbytes\r\nFROM perstudentdata2 ps\r\nWHERE ps.PersonID=@pid AND ps.SpecialControlType=@specialControlType\r\nORDER BY ps.controlID DESC";

		// Token: 0x040000D4 RID: 212
		internal const string QS_DEFINED_SPECIAL_CONTROLIDS = "SELECT\tdc.SpecialControlType,MAX(dc.ControlID) AS controlid\r\nFROM\tDynamicControls dc \r\nWHERE   dc.SpecialControlType>0 AND\r\n\t\t(@restricttypes IS NULL OR @restricttypes='' OR dc.SpecialControlType IN (SELECT orderid AS specialcontroltype FROM splitorderids(@restricttypes,',')))\r\nGROUP BY dc.SpecialControlType ";
	}
}
