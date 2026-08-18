using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc.Properties;
using System.Web.Routing;

namespace System.Web.Mvc.Routing
{
	// Token: 0x02000029 RID: 41
	internal class SubRouteCollection : IReadOnlyCollection<Route>, IEnumerable<Route>, IEnumerable
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00004B3C File Offset: 0x00002D3C
		public void Add(RouteEntry entry)
		{
			Route route = entry.Route;
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

		// Token: 0x060000C9 RID: 201 RVA: 0x00004BB8 File Offset: 0x00002DB8
		public void AddRange(IEnumerable<RouteEntry> entries)
		{
			foreach (RouteEntry entry in entries)
			{
				this.Add(entry);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00004C00 File Offset: 0x00002E00
		public int Count
		{
			get
			{
				return this._entries.Count;
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004C0D File Offset: 0x00002E0D
		public IEnumerator<Route> GetEnumerator()
		{
			return this._routes.GetEnumerator();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004C1F File Offset: 0x00002E1F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this._routes).GetEnumerator();
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004C2C File Offset: 0x00002E2C
		public IReadOnlyCollection<RouteEntry> Entries
		{
			get
			{
				return this._entries;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004C34 File Offset: 0x00002E34
		private static void ThrowExceptionForDuplicateRouteNames(string name, Route route1, Route route2)
		{
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.SubRouteCollection_DuplicateRouteName, new object[]
			{
				name,
				route1.Url,
				route2.Url
			}));
		}

		// Token: 0x04000034 RID: 52
		private readonly List<Route> _routes = new List<Route>();

		// Token: 0x04000035 RID: 53
		private readonly List<RouteEntry> _entries = new List<RouteEntry>();
	}
}
