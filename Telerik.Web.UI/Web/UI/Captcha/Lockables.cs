using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.Captcha
{
	// Token: 0x020000FA RID: 250
	internal static class Lockables
	{
		// Token: 0x06000A93 RID: 2707 RVA: 0x000259A8 File Offset: 0x00023BA8
		internal static void Create(string guid)
		{
			object value = new object();
			Lockables.locks.TryAdd(guid, value);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x000259C8 File Offset: 0x00023BC8
		internal static object GetOnce(string guid)
		{
			object obj = Lockables.Get(guid);
			if (obj == null)
			{
				return null;
			}
			object obj2 = obj;
			lock (obj2)
			{
				Lockables.RemoveInternal(guid, out obj);
			}
			return obj;
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00025A14 File Offset: 0x00023C14
		internal static object Get(string guid)
		{
			if (!Lockables.locks.ContainsKey(guid))
			{
				return null;
			}
			return Lockables.locks[guid];
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00025A30 File Offset: 0x00023C30
		internal static void Remove(string guid)
		{
			object obj;
			Lockables.RemoveInternal(guid, out obj);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00025A45 File Offset: 0x00023C45
		[SuppressMessage("Microsoft.Design", "CA1007:UseGenericsWhereAppropriate")]
		private static void RemoveInternal(string guid, out object lockable)
		{
			Lockables.locks.TryRemove(guid, out lockable);
		}

		// Token: 0x04000292 RID: 658
		private static readonly ConcurrentDictionary<string, object> locks = new ConcurrentDictionary<string, object>();
	}
}
