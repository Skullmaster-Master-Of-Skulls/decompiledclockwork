using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000533 RID: 1331
	[AttributeUsage(AttributeTargets.Method)]
	public class ConnectionProviderAttribute : Attribute
	{
		// Token: 0x06004382 RID: 17282 RVA: 0x000DE3CA File Offset: 0x000DC5CA
		public ConnectionProviderAttribute(string displayName)
		{
			if (string.IsNullOrEmpty(displayName))
			{
				throw new ArgumentNullException("displayName");
			}
			this._displayName = displayName;
			this._allowsMultipleConnections = true;
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x000DE3F3 File Offset: 0x000DC5F3
		public ConnectionProviderAttribute(string displayName, string id) : this(displayName)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException("id");
			}
			this._id = id;
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x000DE416 File Offset: 0x000DC616
		public ConnectionProviderAttribute(string displayName, Type connectionPointType) : this(displayName)
		{
			if (connectionPointType == null)
			{
				throw new ArgumentNullException("connectionPointType");
			}
			this._connectionPointType = connectionPointType;
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x000DE43A File Offset: 0x000DC63A
		public ConnectionProviderAttribute(string displayName, string id, Type connectionPointType) : this(displayName, connectionPointType)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException("id");
			}
			this._id = id;
		}

		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x06004386 RID: 17286 RVA: 0x000DE45E File Offset: 0x000DC65E
		// (set) Token: 0x06004387 RID: 17287 RVA: 0x000DE466 File Offset: 0x000DC666
		public bool AllowsMultipleConnections
		{
			get
			{
				return this._allowsMultipleConnections;
			}
			set
			{
				this._allowsMultipleConnections = value;
			}
		}

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x06004388 RID: 17288 RVA: 0x000DE46F File Offset: 0x000DC66F
		public string ID
		{
			get
			{
				if (this._id == null)
				{
					return string.Empty;
				}
				return this._id;
			}
		}

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x06004389 RID: 17289 RVA: 0x000DE485 File Offset: 0x000DC685
		public virtual string DisplayName
		{
			get
			{
				return this.DisplayNameValue;
			}
		}

		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x0600438A RID: 17290 RVA: 0x000DE48D File Offset: 0x000DC68D
		// (set) Token: 0x0600438B RID: 17291 RVA: 0x000DE495 File Offset: 0x000DC695
		protected string DisplayNameValue
		{
			get
			{
				return this._displayName;
			}
			set
			{
				this._displayName = value;
			}
		}

		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x0600438C RID: 17292 RVA: 0x000DE49E File Offset: 0x000DC69E
		public Type ConnectionPointType
		{
			get
			{
				if (WebPartUtil.IsConnectionPointTypeValid(this._connectionPointType, false))
				{
					return this._connectionPointType;
				}
				throw new InvalidOperationException(SR.GetString("ConnectionProviderAttribute_InvalidConnectionPointType", new object[]
				{
					this._connectionPointType.Name
				}));
			}
		}

		// Token: 0x040025E8 RID: 9704
		private string _displayName;

		// Token: 0x040025E9 RID: 9705
		private string _id;

		// Token: 0x040025EA RID: 9706
		private Type _connectionPointType;

		// Token: 0x040025EB RID: 9707
		private bool _allowsMultipleConnections;
	}
}
