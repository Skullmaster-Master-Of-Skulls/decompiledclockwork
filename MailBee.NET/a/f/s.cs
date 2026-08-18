using System;
using System.Collections;
using System.Text;

namespace a.f
{
	// Token: 0x020000ED RID: 237
	internal class s
	{
		// Token: 0x060007E0 RID: 2016 RVA: 0x00024A4A File Offset: 0x00023A4A
		private s()
		{
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00024A54 File Offset: 0x00023A54
		public static Hashtable a(ArrayList A_0, string A_1, string A_2, Encoding A_3)
		{
			Hashtable hashtable = new Hashtable();
			if (A_0 != null)
			{
				k k = k.a();
				s.a(A_0, hashtable, k, A_3);
				k.a(hashtable, A_2);
			}
			return hashtable;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00024A84 File Offset: 0x00023A84
		public static Hashtable c(ArrayList A_0, Encoding A_1)
		{
			Hashtable hashtable = new Hashtable();
			if (A_0 != null)
			{
				u a_ = u.a();
				s.a(A_0, hashtable, a_, A_1);
			}
			return hashtable;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00024AAC File Offset: 0x00023AAC
		public static Hashtable b(ArrayList A_0, Encoding A_1)
		{
			Hashtable hashtable = new Hashtable();
			if (A_0 != null)
			{
				l a_ = l.a();
				s.a(A_0, hashtable, a_, A_1);
			}
			return hashtable;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00024AD4 File Offset: 0x00023AD4
		public static Hashtable a(ArrayList A_0, Encoding A_1)
		{
			Hashtable hashtable = new Hashtable();
			if (A_0 != null)
			{
				q a_ = q.a();
				s.a(A_0, hashtable, a_, A_1);
			}
			return hashtable;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00024AFC File Offset: 0x00023AFC
		private static void a(object A_0, Hashtable A_1, n A_2, Encoding A_3)
		{
			ArrayList arrayList = A_0 as ArrayList;
			if (arrayList != null)
			{
				int i = 0;
				while (i < arrayList.Count)
				{
					object obj = arrayList[i];
					object a_ = (i + 1 < arrayList.Count) ? arrayList[i + 1] : null;
					string text = null;
					if (obj is ao)
					{
						text = ((ao)obj).a(A_3);
						text = text.ToUpper();
						int num = A_2.j9(text, a_);
						if (num != 0)
						{
							if (num != 1)
							{
								ArrayList value;
								if (num == -1)
								{
									if (i < arrayList.Count - 1)
									{
										value = arrayList.GetRange(i + 1, arrayList.Count - (i + 1));
									}
									else
									{
										value = new ArrayList();
									}
								}
								else
								{
									try
									{
										value = arrayList.GetRange(i + 1, num);
									}
									catch
									{
										value = null;
									}
								}
								try
								{
									A_1.Add(text, value);
								}
								catch (ArgumentException)
								{
								}
								if (num == -1)
								{
									i = arrayList.Count;
								}
								else
								{
									i += num + 1;
								}
							}
							else
							{
								object value2 = A_2.ka(text, a_, A_3);
								try
								{
									A_1.Add(text, value2);
								}
								catch (ArgumentException)
								{
								}
								i += 2;
							}
						}
						else
						{
							try
							{
								A_1.Add(text, string.Empty);
							}
							catch (ArgumentException)
							{
							}
							i++;
						}
					}
					else
					{
						s.a(obj, A_1, A_2, A_3);
						i++;
					}
				}
			}
		}
	}
}
