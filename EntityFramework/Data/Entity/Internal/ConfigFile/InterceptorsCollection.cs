using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x0200019F RID: 415
	internal class InterceptorsCollection : ConfigurationElementCollection
	{
		// Token: 0x06000E1D RID: 3613 RVA: 0x0003E7C0 File Offset: 0x0003C9C0
		protected override ConfigurationElement CreateNewElement()
		{
			return new InterceptorElement(this._nextKey++);
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0003E7E3 File Offset: 0x0003C9E3
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((InterceptorElement)element).Key;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0003E7F5 File Offset: 0x0003C9F5
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x0003E7F8 File Offset: 0x0003C9F8
		protected override string ElementName
		{
			get
			{
				return "interceptor";
			}
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0003E7FF File Offset: 0x0003C9FF
		public void AddElement(InterceptorElement element)
		{
			base.BaseAdd(element);
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x0003E810 File Offset: 0x0003CA10
		public virtual IEnumerable<IDbInterceptor> Interceptors
		{
			get
			{
				return (from e in this.OfType<InterceptorElement>()
				select e.CreateInterceptor()).ToList<IDbInterceptor>();
			}
		}

		// Token: 0x040003C4 RID: 964
		private const string ElementKey = "interceptor";

		// Token: 0x040003C5 RID: 965
		private int _nextKey;
	}
}
