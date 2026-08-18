using System;
using System.Linq;
using System.Text;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.IDisplay;

namespace TechnoPro.Common.Display.Inventory
{
	// Token: 0x02000003 RID: 3
	public class InventoryLocationDTODisplayString : ClockWorkBaseDisplayString<InventoryLocationDTO>
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000207C File Offset: 0x0000027C
		protected override string GetHtmlDisplayString(InventoryLocationDTO location, DisplayParameters parameters = null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = parameters != null && parameters.Contains("InnerTag");
			stringBuilder.Append(flag ? "<div class='lightblueinnersummarybox'>" : "<div class='lightbluesummarybox'>");
			if (location != null)
			{
				if (parameters == null || parameters.DisplayPropertyList.Contains("LocationId"))
				{
					stringBuilder.AppendFormat("<b>Location Id: </b><i>{0}</i><br />", location.LocationId.ToString());
				}
				if (parameters == null || parameters.DisplayPropertyList.Contains("Campus"))
				{
					stringBuilder.AppendFormat("<b>Campus: </b><i>{0}</i><br />", location.Campus ?? string.Empty);
				}
				if (parameters == null || parameters.DisplayPropertyList.Contains("Building"))
				{
					stringBuilder.AppendFormat("<b>Building: </b><i>{0}</i><br />", location.Building ?? string.Empty);
				}
				if (parameters == null || parameters.DisplayPropertyList.Contains("RoomNumber"))
				{
					stringBuilder.AppendFormat("<b>Room number: </b><i>{0}</i><br />", location.RoomNumber ?? string.Empty);
				}
				if (parameters == null || parameters.DisplayPropertyList.Contains("Seat"))
				{
					stringBuilder.AppendFormat("<b>Seat: </b><i>{0}</i><br />", location.Seat ?? string.Empty);
				}
				if (parameters == null || parameters.DisplayPropertyList.Contains("Notes"))
				{
					stringBuilder.AppendFormat("<b>Notes: </b><i>{0}</i><br />", location.Notes ?? string.Empty);
				}
			}
			else
			{
				stringBuilder.Append("<i>No location</i>");
			}
			stringBuilder.Append("</div>");
			return stringBuilder.ToString();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021FC File Offset: 0x000003FC
		protected override string GetPlainTextDisplayString(InventoryLocationDTO t, DisplayParameters parameters = null)
		{
			return string.Empty;
		}
	}
}
