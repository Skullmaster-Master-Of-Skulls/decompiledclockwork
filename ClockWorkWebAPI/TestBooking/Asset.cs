using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x0200003A RID: 58
	[Serializable]
	public class Asset
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0001246C File Offset: 0x0001066C
		public string Title
		{
			get
			{
				return this.title;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00012484 File Offset: 0x00010684
		public string AssetId
		{
			get
			{
				return this.assetId;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0001249C File Offset: 0x0001069C
		// (set) Token: 0x060002FA RID: 762 RVA: 0x000124B4 File Offset: 0x000106B4
		public int Score
		{
			get
			{
				return this.score;
			}
			set
			{
				this.score = value;
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000124C0 File Offset: 0x000106C0
		public string ToStringDebug()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.title);
			stringBuilder.Append(" (");
			stringBuilder.Append(this.assetId);
			stringBuilder.Append(") [score=");
			stringBuilder.Append(this.score.ToString());
			stringBuilder.Append("; isactive=");
			stringBuilder.Append(this.isActive.ToString());
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0001254C File Offset: 0x0001074C
		public static string ToStringDebug(string title, List<Asset> assets)
		{
			StringBuilder stringBuilder = new StringBuilder(title);
			stringBuilder.Append("<br />");
			foreach (Asset asset in assets)
			{
				stringBuilder.Append(asset.ToStringDebug());
				stringBuilder.Append("<br />");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002FD RID: 765 RVA: 0x000125D0 File Offset: 0x000107D0
		public List<Accommodation> AccommodationsSupported
		{
			get
			{
				return this.accommodationsSupported;
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000125E8 File Offset: 0x000107E8
		public bool Matches(Asset asset)
		{
			return this.assetId == asset.assetId;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0001260B File Offset: 0x0001080B
		public Asset(string assetId, string title, int score)
		{
			this.assetId = assetId;
			this.title = title;
			this.score = score;
			this.accommodationsSupported = new List<Accommodation>();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00012635 File Offset: 0x00010835
		public void AddAccommodation(Accommodation accommodation)
		{
			this.accommodationsSupported.Add(accommodation);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00012648 File Offset: 0x00010848
		public bool Intersects(List<Accommodation> accommodations)
		{
			return this.Intersects(accommodations, 1);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00012664 File Offset: 0x00010864
		public bool Intersects(List<Accommodation> accommodations, int level)
		{
			List<Accommodation> list = this.accommodationsSupported.FindAll((Accommodation f) => f.Level == level);
			using (List<Accommodation>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Asset.<>c__DisplayClass21_1 CS$<>8__locals2 = new Asset.<>c__DisplayClass21_1();
					CS$<>8__locals2.acc = enumerator.Current;
					string subText = CS$<>8__locals2.acc.SubText.ToLower();
					Accommodation accommodation = accommodations.Find((Accommodation e) => e.Controlid == CS$<>8__locals2.acc.Controlid && (string.IsNullOrEmpty(subText) || e.Title.ToLower().Contains(subText)));
					bool flag = accommodation != null;
					if (flag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0001273C File Offset: 0x0001093C
		public static bool ContainsAsset(List<Asset> assets, Asset asset)
		{
			foreach (Asset asset2 in assets)
			{
				bool flag = asset2.AssetId.Equals(asset.AssetId);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000127A8 File Offset: 0x000109A8
		public static List<Asset> LoadAssets(string xml)
		{
			List<Asset> list = new List<Asset>();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			foreach (object obj in xmlDocument.LastChild.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string text = "";
				string text2 = "";
				int num = 100;
				List<Accommodation> list2 = new List<Accommodation>();
				foreach (object obj2 in xmlNode.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					string text3 = xmlNode2.Name.ToLower();
					bool flag = text3.Equals("id");
					if (flag)
					{
						text = xmlNode2.InnerText;
					}
					else
					{
						bool flag2 = text3.Equals("title");
						if (flag2)
						{
							text2 = xmlNode2.InnerText;
						}
						else
						{
							bool flag3 = text3.Equals("score");
							if (flag3)
							{
								try
								{
									num = int.Parse(xmlNode2.InnerText);
								}
								catch
								{
								}
							}
							else
							{
								bool flag4 = text3.Equals("accommodations");
								if (flag4)
								{
									string innerText = xmlNode2.InnerText;
									string[] array = innerText.Split(new char[]
									{
										','
									});
									foreach (string text4 in array)
									{
										bool flag5 = innerText.Trim().Length > 0;
										if (flag5)
										{
											int num2 = text4.IndexOf('.');
											int num3 = text4.IndexOf(':');
											bool flag6 = num3 > 0;
											int level;
											string text6;
											string subText;
											if (flag6)
											{
												string text5 = text4.Substring(num3 + 1);
												bool flag7 = !string.IsNullOrEmpty(text5);
												if (flag7)
												{
													bool flag8 = !int.TryParse(text5, out level);
													if (flag8)
													{
														level = 1;
													}
												}
												else
												{
													level = 1;
												}
												bool flag9 = num2 > 0;
												if (flag9)
												{
													text6 = text4.Substring(0, num3);
													subText = text6.Substring(num2 + 1);
													text6 = text6.Substring(0, num2);
												}
												else
												{
													text6 = text4.Substring(0, num3);
													subText = "";
												}
											}
											else
											{
												level = 1;
												bool flag10 = num2 > 0;
												if (flag10)
												{
													text6 = text4.Substring(0, num2);
													subText = text4.Substring(num2 + 1);
												}
												else
												{
													text6 = text4;
													subText = "";
												}
											}
											int num4;
											bool flag11 = !int.TryParse(text6, out num4);
											if (flag11)
											{
												num4 = 0;
											}
											bool flag12 = num4 > 0;
											if (flag12)
											{
												list2.Add(new Accommodation(num4, num4.ToString(), "", level)
												{
													SubText = subText
												});
											}
										}
									}
								}
							}
						}
					}
				}
				Asset asset = new Asset(text, text2, num);
				foreach (Accommodation accommodation in list2)
				{
					asset.AddAccommodation(accommodation);
				}
				list.Add(asset);
			}
			return list;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00012B54 File Offset: 0x00010D54
		public static int GetMaxAccommodationLevel(List<Asset> assets, List<Accommodation> studentsAccommodations)
		{
			int num = 1;
			List<Asset> list = new List<Asset>();
			foreach (Asset asset in assets)
			{
				using (List<Accommodation>.Enumerator enumerator2 = studentsAccommodations.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Accommodation acc = enumerator2.Current;
						bool flag = asset.accommodationsSupported.Find((Accommodation e) => e.Controlid == acc.Controlid) != null;
						if (flag)
						{
							list.Add(asset);
							break;
						}
					}
				}
			}
			foreach (Asset asset2 in list)
			{
				foreach (Accommodation accommodation in asset2.accommodationsSupported)
				{
					bool flag2 = accommodation.Level > num;
					if (flag2)
					{
						num = accommodation.Level;
					}
				}
			}
			return num;
		}

		// Token: 0x0400018B RID: 395
		private string assetId;

		// Token: 0x0400018C RID: 396
		private string title;

		// Token: 0x0400018D RID: 397
		private bool isActive;

		// Token: 0x0400018E RID: 398
		private int score;

		// Token: 0x0400018F RID: 399
		private List<Accommodation> accommodationsSupported;

		// Token: 0x04000190 RID: 400
		public const int DEFAULT_SCORE = 100;
	}
}
