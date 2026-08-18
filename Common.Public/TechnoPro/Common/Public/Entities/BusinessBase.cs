using System;

namespace TechnoPro.Common.Public.Entities
{
	// Token: 0x020000E9 RID: 233
	[Serializable]
	public abstract class BusinessBase<T> : IEquatable<BusinessBase<T>>
	{
		// Token: 0x06000564 RID: 1380 RVA: 0x0000E6A8 File Offset: 0x0000C8A8
		public override int GetHashCode()
		{
			return string.Format("{0}, {1}", this.Id, this.ToString()).GetHashCode();
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000E6DC File Offset: 0x0000C8DC
		public bool Equals(BusinessBase<T> other)
		{
			return other != null && (this.MatchingIds(other) || this.MatchingHashCodes(other));
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0000E708 File Offset: 0x0000C908
		public override bool Equals(object obj)
		{
			return obj != null && obj.GetType() == base.GetType() && (this.MatchingIds((BusinessBase<T>)obj) || this.MatchingHashCodes(obj));
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0000E74C File Offset: 0x0000C94C
		protected virtual bool MatchingIds(BusinessBase<T> obj)
		{
			bool result;
			if (!object.Equals(this.Id, default(T)) && !object.Equals(obj.Id, default(T)))
			{
				T id = this.Id;
				result = id.Equals(obj.Id);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0000E7C0 File Offset: 0x0000C9C0
		protected virtual bool MatchingHashCodes(object obj)
		{
			return this.GetHashCode().Equals(obj.GetHashCode());
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0000E7E6 File Offset: 0x0000C9E6
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x0000E7EE File Offset: 0x0000C9EE
		public virtual T Id { get; set; }
	}
}
