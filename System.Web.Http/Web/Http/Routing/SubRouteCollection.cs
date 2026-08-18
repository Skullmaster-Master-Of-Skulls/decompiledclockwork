using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http.Properties;

namespace System.Web.Http.Routing
{
	// Token: 0x0200009C RID: 156
	internal class SubRouteCollection : IReadOnlyCollection<IHttpRoute>, IEnumerable<IHttpRoute>, IEnumerable
	{
		// Token: 0x060003BF RID: 959 RVA: 0x0000BDEC File Offset: 0x00009FEC
		public void Add(RouteEntry entry)
		{
			IHttpRoute route = entry.Route;
			string name = entry.Name;
			if (name != null)
			{
				RouteEntry routeEntry = this._entries.SingleOrDefault((RouteEntry e) => e.Name == name);
				if (routeEntry != null)
				{
					SubRouteCollection.ThrowExceptionForDuplicateRouteNames(name, route, routeEntry.Route);
				}
			}
			this._routes.Add(route);
			this._entries.Add(entry);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000BE68 File Offset: 0x0000A068
		public void AddRange(IEnumerable<RouteEntry> entries)
		{
			foreach (RouteEntry entry in entries)
			{
				this.Add(entry);
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000BEB0 File Offset: 0x0000A0B0
		public int Count
		{
			get
			{
				return this._entries.Count;
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000BEBD File Offset: 0x0000A0BD
		public IEnumerator<IHttpRoute> GetEnumerator()
		{
			return this._routes.GetEnumerator();
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000BECF File Offset: 0x0000A0CF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this._routes).GetEnumerator();
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		public IReadOnlyCollection<RouteEntry> Entries
		{
			get
			{
				return this._entries;
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		private static void ThrowExceptionForDuplicateRouteNames(string name, IHttpRoute route1, IHttpRoute route2)
		{
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, SRResources.SubRouteCollection_DuplicateRouteName, new object[]
			{
				name,
				route1.RouteTemplate,
				route2.RouteTemplate
			}));
		}

		// Token: 0x04000116 RID: 278
		private readonly List<IHttpRoute> _routes = new List<IHttpRoute>();

		// Token: 0x04000117 RID: 279
		private readonly List<RouteEntry> _entries = new List<RouteEntry>();
	}
}
