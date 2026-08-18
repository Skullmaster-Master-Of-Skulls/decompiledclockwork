using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000533 RID: 1331
	[Serializable]
	public class Asset : BusinessBase<string>
	{
		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x06002A32 RID: 10802 RVA: 0x0002B4D4 File Offset: 0x000296D4
		// (set) Token: 0x06002A33 RID: 10803 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public virtual string AssetId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x06002A34 RID: 10804 RVA: 0x0002B4EC File Offset: 0x000296EC
		// (set) Token: 0x06002A35 RID: 10805 RVA: 0x0002B4F4 File Offset: 0x000296F4
		public int Score { get; set; }

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x06002A36 RID: 10806 RVA: 0x0002B4FD File Offset: 0x000296FD
		// (set) Token: 0x06002A37 RID: 10807 RVA: 0x0002B505 File Offset: 0x00029705
		public string Title { get; set; }

		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x06002A38 RID: 10808 RVA: 0x0002B50E File Offset: 0x0002970E
		// (set) Token: 0x06002A39 RID: 10809 RVA: 0x0002B516 File Offset: 0x00029716
		public bool IsActive { get; set; }

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x06002A3A RID: 10810 RVA: 0x0002B51F File Offset: 0x0002971F
		// (set) Token: 0x06002A3B RID: 10811 RVA: 0x0002B527 File Offset: 0x00029727
		public IList<Accommodation> AccommodationsSupported { get; set; }

		// Token: 0x06002A3C RID: 10812 RVA: 0x0002B530 File Offset: 0x00029730
		public Asset(string assetId, string title, int score)
		{
			this.AssetId = assetId;
			this.Title = title;
			this.Score = score;
			this.IsActive = true;
			this.AccommodationsSupported = new List<Accommodation>();
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x0002B566 File Offset: 0x00029766
		public Asset()
		{
			this.AssetId = "";
			this.Title = "";
			this.Score = 100;
			this.IsActive = true;
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x0002B59C File Offset: 0x0002979C
		public string ToStringDebug()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.Title ?? "");
			stringBuilder.Append(" (");
			stringBuilder.Append(this.AssetId ?? "");
			stringBuilder.Append(") [score=");
			stringBuilder.Append(this.Score.ToString());
			stringBuilder.Append("; isactive=");
			stringBuilder.Append(this.IsActive.ToString());
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x0002B640 File Offset: 0x00029840
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

		// Token: 0x06002A40 RID: 10816 RVA: 0x0002B6C4 File Offset: 0x000298C4
		public bool Matches(Asset asset)
		{
			return this.AssetId == asset.AssetId;
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x0002B6E7 File Offset: 0x000298E7
		public void AddAccommodation(Accommodation accommodation)
		{
			this.AccommodationsSupported.Add(accommodation);
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x0002B6F8 File Offset: 0x000298F8
		public bool Intersects(List<Accommodation> accommodations)
		{
			return this.Intersects(accommodations, 1);
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x0002B714 File Offset: 0x00029914
		public bool Intersects(List<Accommodation> accommodations, int level)
		{
			List<Accommodation> list = (from f in this.AccommodationsSupported
			where f.Level == level
			select f).ToList<Accommodation>();
			using (List<Accommodation>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Asset.<>c__DisplayClass27_1 CS$<>8__locals2 = new Asset.<>c__DisplayClass27_1();
					CS$<>8__locals2.acc = enumerator.Current;
					string subText = CS$<>8__locals2.acc.SubText.ToLower();
					Accommodation accommodation = accommodations.Find((Accommodation e) => e.ControlId == CS$<>8__locals2.acc.ControlId && (string.IsNullOrEmpty(subText) || e.Title.ToLower().Contains(subText)));
					bool flag = accommodation != null;
					if (flag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x0002B7F0 File Offset: 0x000299F0
		public static bool ContainsAsset(List<Asset> assets, Asset asset)
		{
			return assets.FirstOrDefault((Asset g) => g.AssetId == asset.AssetId) != null;
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x0002B824 File Offset: 0x00029A24
		public static List<Asset> LoadAssets(string xml)
		{
			List<Asset> list = new List<Asset>();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			foreach (object obj in xmlDocument.LastChild.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string assetId = "";
				string title = "";
				int score = 100;
				List<Accommodation> list2 = new List<Accommodation>();
				foreach (object obj2 in xmlNode.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					string text = xmlNode2.Name.ToLower();
					bool flag = text.Equals("id");
					if (flag)
					{
						assetId = xmlNode2.InnerText;
					}
					else
					{
						bool flag2 = text.Equals("title");
						if (flag2)
						{
							title = xmlNode2.InnerText;
						}
						else
						{
							bool flag3 = text.Equals("score");
							if (flag3)
							{
								try
								{
									score = int.Parse(xmlNode2.InnerText);
								}
								catch
								{
								}
							}
							else
							{
								bool flag4 = text.Equals("accommodations");
								if (flag4)
								{
									string innerText = xmlNode2.InnerText;
									string[] array = innerText.Split(new char[]
									{
										','
									});
									foreach (string text2 in array)
									{
										bool flag5 = innerText.Trim().Length > 0;
										if (flag5)
										{
											int num = text2.IndexOf('.');
											int num2 = text2.IndexOf(':');
											bool flag6 = num2 > 0;
											int level;
											string text4;
											string subText;
											if (flag6)
											{
												string text3 = text2.Substring(num2 + 1);
												bool flag7 = !string.IsNullOrEmpty(text3);
												if (flag7)
												{
													bool flag8 = !int.TryParse(text3, out level);
													if (flag8)
													{
														level = 1;
													}
												}
												else
												{
													level = 1;
												}
												bool flag9 = num > 0;
												if (flag9)
												{
													text4 = text2.Substring(0, num2);
													subText = text4.Substring(num + 1);
													text4 = text4.Substring(0, num);
												}
												else
												{
													text4 = text2.Substring(0, num2);
													subText = "";
												}
											}
											else
											{
												level = 1;
												bool flag10 = num > 0;
												if (flag10)
												{
													text4 = text2.Substring(0, num);
													subText = text2.Substring(num + 1);
												}
												else
												{
													text4 = text2;
													subText = "";
												}
											}
											int num3;
											bool flag11 = !int.TryParse(text4, out num3);
											if (flag11)
											{
												num3 = 0;
											}
											bool flag12 = num3 > 0;
											if (flag12)
											{
												list2.Add(new Accommodation(num3, num3.ToString(), "", level)
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
				Asset asset = new Asset(assetId, title, score);
				foreach (Accommodation accommodation in list2)
				{
					asset.AddAccommodation(accommodation);
				}
				list.Add(asset);
			}
			return list;
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x0002BBD0 File Offset: 0x00029DD0
		public static int GetMaxAccommodationLevel(IList<Asset> assets, IList<Accommodation> studentsAccommodations)
		{
			int num = 1;
			List<Asset> list = new List<Asset>();
			foreach (Asset asset in assets)
			{
				using (IEnumerator<Accommodation> enumerator2 = studentsAccommodations.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Accommodation acc = enumerator2.Current;
						bool flag = asset.AccommodationsSupported.FirstOrDefault((Accommodation e) => e.ControlId == acc.ControlId) != null;
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
				foreach (Accommodation accommodation in asset2.AccommodationsSupported)
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

		// Token: 0x04001E3F RID: 7743
		public const int DEFAULT_SCORE = 100;
	}
}
