using System;

namespace System.Web.UI
{
	// Token: 0x0200031D RID: 797
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class UrlPropertyAttribute : Attribute
	{
		// Token: 0x06002515 RID: 9493 RVA: 0x0007A74E File Offset: 0x0007894E
		public UrlPropertyAttribute() : this("*.*")
		{
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x0007A75B File Offset: 0x0007895B
		public UrlPropertyAttribute(string filter)
		{
			if (filter == null)
			{
				this._filter = "*.*";
				return;
			}
			this._filter = filter;
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06002517 RID: 9495 RVA: 0x0007A779 File Offset: 0x00078979
		public string Filter
		{
			get
			{
				return this._filter;
			}
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x0007A781 File Offset: 0x00078981
		public override int GetHashCode()
		{
			return this.Filter.GetHashCode();
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x0007A790 File Offset: 0x00078990
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			UrlPropertyAttribute urlPropertyAttribute = obj as UrlPropertyAttribute;
			return urlPropertyAttribute != null && this.Filter.Equals(urlPropertyAttribute.Filter);
		}

		// Token: 0x04001D6E RID: 7534
		private string _filter;
	}
}
