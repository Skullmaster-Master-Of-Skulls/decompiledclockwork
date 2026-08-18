using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.Room;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Adapters
{
	// Token: 0x02000C8C RID: 3212
	public static class RoomAdapter
	{
		// Token: 0x060042F3 RID: 17139 RVA: 0x00023630 File Offset: 0x00021830
		public static Forest<ForestSeatBaseDTO> ConvertSeatCollectionToForest(this SeatCollectionDTO seatCollection)
		{
			Forest<ForestSeatBaseDTO> forest = new Forest<ForestSeatBaseDTO>();
			Dictionary<eTestExamSeatType, TreeNode<ForestSeatBaseDTO>> dictionary = new Dictionary<eTestExamSeatType, TreeNode<ForestSeatBaseDTO>>();
			dictionary.Add(eTestExamSeatType.Final, forest.AppendNode(null, new ForestSeatTypeDTO(eTestExamSeatType.Final)));
			dictionary.Add(eTestExamSeatType.Midterm, forest.AppendNode(null, new ForestSeatTypeDTO(eTestExamSeatType.Midterm)));
			dictionary.Add(eTestExamSeatType.AllRooms, forest.AppendNode(null, new ForestSeatTypeDTO(eTestExamSeatType.AllRooms)));
			List<string> list = (from h in seatCollection.Seats
			where !string.IsNullOrEmpty(h.Campus)
			select h into g
			select g.Campus ?? "").Distinct<string>().ToList<string>();
			Dictionary<eTestExamSeatType, TreeNode<ForestSeatBaseDTO>> dictionary2 = new Dictionary<eTestExamSeatType, TreeNode<ForestSeatBaseDTO>>();
			foreach (KeyValuePair<eTestExamSeatType, TreeNode<ForestSeatBaseDTO>> keyValuePair in dictionary)
			{
				foreach (string campus in list)
				{
					dictionary2.Add(keyValuePair.Key, forest.AppendNode(keyValuePair.Value, new ForestSeatCampusDTO(campus)));
				}
			}
			using (IEnumerator<SeatDTO> enumerator3 = seatCollection.Seats.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					SeatDTO seat = enumerator3.Current;
					TreeNode<ForestSeatBaseDTO> parentNode = (!string.IsNullOrEmpty(seat.Campus)) ? dictionary2.FirstOrDefault((KeyValuePair<eTestExamSeatType, TreeNode<ForestSeatBaseDTO>> g) => g.Key == seat.SeatType && ((ForestSeatCampusDTO)g.Value.Value).Campus == seat.Campus).Value : dictionary[seat.SeatType];
					forest.AppendNode(parentNode, new ForestSeatDTO(seat));
				}
			}
			return forest;
		}
	}
}
