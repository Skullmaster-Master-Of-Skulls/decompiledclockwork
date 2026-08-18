using System;
using System.Collections;

namespace ClockWorkWebAPI
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	public class AccommodationCollection : CollectionBase
	{
		// Token: 0x1700001E RID: 30
		public Accommodation this[int index]
		{
			get
			{
				return (Accommodation)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000361C File Offset: 0x0000181C
		public void SortListByCaptionWithValue()
		{
			AccommodationCollection.AscendingCaptionWithValueSorter comparer = new AccommodationCollection.AscendingCaptionWithValueSorter();
			base.InnerList.Sort(comparer);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003640 File Offset: 0x00001840
		public int Add(Accommodation accommodation)
		{
			return base.List.Add(accommodation);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000365E File Offset: 0x0000185E
		public void Insert(int index, Accommodation accommodation)
		{
			base.List.Insert(index, accommodation);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000366F File Offset: 0x0000186F
		public void Remove(Accommodation accommodation)
		{
			base.List.Remove(accommodation);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003680 File Offset: 0x00001880
		public bool Contains(Accommodation accommodation)
		{
			return base.List.Contains(accommodation);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000036A0 File Offset: 0x000018A0
		public bool Contains(int controlId)
		{
			foreach (object obj in this)
			{
				Accommodation accommodation = (Accommodation)obj;
				bool flag = accommodation.ControlId == controlId;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000370C File Offset: 0x0000190C
		public void CopyAccommodations(AccommodationCollection fromCollection, int[] controlids)
		{
			base.Clear();
			bool flag = controlids != null && fromCollection != null;
			if (flag)
			{
				for (int i = 0; i < fromCollection.Count; i++)
				{
					Accommodation accommodation = fromCollection[i];
					int controlId = accommodation.ControlId;
					for (int j = 0; j < controlids.Length; j++)
					{
						bool flag2 = controlId == controlids[j];
						if (flag2)
						{
							this.Add(accommodation.Copy());
							break;
						}
					}
				}
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003790 File Offset: 0x00001990
		public string GetControlidsCommaSeparated()
		{
			string text = "";
			for (int i = 0; i < base.Count; i++)
			{
				Accommodation accommodation = this[i];
				bool flag = i > 0;
				if (flag)
				{
					text += ",";
				}
				text += accommodation.ControlId.ToString();
			}
			return text;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000037F8 File Offset: 0x000019F8
		public override string ToString()
		{
			string text = "";
			foreach (object obj in base.List)
			{
				Accommodation accommodation = (Accommodation)obj;
				bool flag = text.Length > 0;
				if (flag)
				{
					text += ", ";
				}
				text += accommodation.CaptionWithValue;
			}
			return text;
		}

		// Token: 0x02000085 RID: 133
		private class AscendingCaptionWithValueSorter : IComparer
		{
			// Token: 0x0600066B RID: 1643 RVA: 0x0002B3B8 File Offset: 0x000295B8
			public int Compare(object x, object y)
			{
				Accommodation accommodation = x as Accommodation;
				Accommodation accommodation2 = y as Accommodation;
				return (accommodation.CaptionWithValue ?? "").CompareTo(accommodation2.CaptionWithValue ?? "");
			}
		}
	}
}
