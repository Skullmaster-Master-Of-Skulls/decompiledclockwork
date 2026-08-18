using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.SessionState
{
	// Token: 0x0200012B RID: 299
	public interface IHttpSessionState
	{
		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060011CE RID: 4558
		string SessionID { get; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060011CF RID: 4559
		// (set) Token: 0x060011D0 RID: 4560
		int Timeout { get; set; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060011D1 RID: 4561
		bool IsNewSession { get; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060011D2 RID: 4562
		SessionStateMode Mode { get; }

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060011D3 RID: 4563
		bool IsCookieless { get; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060011D4 RID: 4564
		HttpCookieMode CookieMode { get; }

		// Token: 0x060011D5 RID: 4565
		void Abandon();

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060011D6 RID: 4566
		// (set) Token: 0x060011D7 RID: 4567
		int LCID { get; set; }

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060011D8 RID: 4568
		// (set) Token: 0x060011D9 RID: 4569
		int CodePage { get; set; }

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060011DA RID: 4570
		HttpStaticObjectsCollection StaticObjects { get; }

		// Token: 0x1700058E RID: 1422
		object this[string name]
		{
			get;
			set;
		}

		// Token: 0x1700058F RID: 1423
		object this[int index]
		{
			get;
			set;
		}

		// Token: 0x060011DF RID: 4575
		void Add(string name, object value);

		// Token: 0x060011E0 RID: 4576
		void Remove(string name);

		// Token: 0x060011E1 RID: 4577
		void RemoveAt(int index);

		// Token: 0x060011E2 RID: 4578
		void Clear();

		// Token: 0x060011E3 RID: 4579
		void RemoveAll();

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060011E4 RID: 4580
		int Count { get; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060011E5 RID: 4581
		NameObjectCollectionBase.KeysCollection Keys { get; }

		// Token: 0x060011E6 RID: 4582
		IEnumerator GetEnumerator();

		// Token: 0x060011E7 RID: 4583
		void CopyTo(Array array, int index);

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060011E8 RID: 4584
		object SyncRoot { get; }

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060011E9 RID: 4585
		bool IsReadOnly { get; }

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060011EA RID: 4586
		bool IsSynchronized { get; }
	}
}
