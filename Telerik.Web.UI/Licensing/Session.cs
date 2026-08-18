using System;

namespace Telerik.Licensing
{
	// Token: 0x0200042A RID: 1066
	internal class Session
	{
		// Token: 0x06002641 RID: 9793 RVA: 0x0007D820 File Offset: 0x0007BA20
		public Session()
		{
			this.Id = Guid.NewGuid().ToString();
			this.Start = DateTime.Now;
			this.Timeout = TimeSpan.FromHours(24.0);
			this.Components = new TypesCollection();
		}

		// Token: 0x14000080 RID: 128
		// (add) Token: 0x06002642 RID: 9794 RVA: 0x0007D878 File Offset: 0x0007BA78
		// (remove) Token: 0x06002643 RID: 9795 RVA: 0x0007D8B0 File Offset: 0x0007BAB0
		public event SessionChangedEventHandler SessionChanged;

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06002644 RID: 9796 RVA: 0x0007D8E5 File Offset: 0x0007BAE5
		// (set) Token: 0x06002645 RID: 9797 RVA: 0x0007D8ED File Offset: 0x0007BAED
		public string Id { get; set; }

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06002646 RID: 9798 RVA: 0x0007D8F6 File Offset: 0x0007BAF6
		// (set) Token: 0x06002647 RID: 9799 RVA: 0x0007D8FE File Offset: 0x0007BAFE
		public DateTime Start { get; set; }

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06002648 RID: 9800 RVA: 0x0007D907 File Offset: 0x0007BB07
		// (set) Token: 0x06002649 RID: 9801 RVA: 0x0007D90F File Offset: 0x0007BB0F
		public TimeSpan Timeout { get; set; }

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x0600264A RID: 9802 RVA: 0x0007D918 File Offset: 0x0007BB18
		// (set) Token: 0x0600264B RID: 9803 RVA: 0x0007D920 File Offset: 0x0007BB20
		public TypesCollection Components
		{
			get
			{
				return this._types;
			}
			set
			{
				if (this._types != null)
				{
					this._types.CollectionChanged -= this.CollectionChanged;
				}
				this._types = value;
				this._types.CollectionChanged += this.CollectionChanged;
			}
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x0007D95F File Offset: 0x0007BB5F
		public SessionName GetName()
		{
			return this._name;
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x0007D967 File Offset: 0x0007BB67
		public Session SetName(SessionName name)
		{
			this._name = name;
			return this;
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x0007D974 File Offset: 0x0007BB74
		public bool IsExpired()
		{
			return this.Start.Add(this.Timeout) < DateTime.Now;
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x0007D99F File Offset: 0x0007BB9F
		public bool GetHasPendingChange()
		{
			return this._hasNewItem;
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x0007D9A7 File Offset: 0x0007BBA7
		public bool GetProductUsageLogged()
		{
			return this._productUsageLogged;
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x0007D9AF File Offset: 0x0007BBAF
		public void SetProductUsageLogged()
		{
			this._productUsageLogged = true;
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x0007D9B8 File Offset: 0x0007BBB8
		public void SetHasPendingChange()
		{
			this._hasNewItem = true;
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x0007D9C1 File Offset: 0x0007BBC1
		public void SetPendingChangeResolved()
		{
			this._hasNewItem = false;
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x0007D9CA File Offset: 0x0007BBCA
		public void Reset()
		{
			this.Start = DateTime.Now;
			this._productUsageLogged = false;
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x0007D9E0 File Offset: 0x0007BBE0
		private void CollectionChanged(object sender, CollectionChangedEventArgs e)
		{
			if (!this.GetHasPendingChange())
			{
				this.SetHasPendingChange();
			}
			SessionChangedEventHandler sessionChanged = this.SessionChanged;
			if (sessionChanged != null)
			{
				sessionChanged(sender, new SessionChangedEventArgs(this));
			}
		}

		// Token: 0x040009C3 RID: 2499
		private SessionName _name;

		// Token: 0x040009C4 RID: 2500
		private TypesCollection _types;

		// Token: 0x040009C5 RID: 2501
		private bool _productUsageLogged;

		// Token: 0x040009C6 RID: 2502
		private bool _hasNewItem;
	}
}
