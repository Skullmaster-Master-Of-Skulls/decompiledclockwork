using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000530 RID: 1328
	[AttributeUsage(AttributeTargets.Method)]
	public class ConnectionConsumerAttribute : Attribute
	{
		// Token: 0x06004365 RID: 17253 RVA: 0x000DE089 File Offset: 0x000DC289
		public ConnectionConsumerAttribute(string displayName)
		{
			if (string.IsNullOrEmpty(displayName))
			{
				throw new ArgumentNullException("displayName");
			}
			this._displayName = displayName;
			this._allowsMultipleConnections = false;
		}

		// Token: 0x06004366 RID: 17254 RVA: 0x000DE0B2 File Offset: 0x000DC2B2
		public ConnectionConsumerAttribute(string displayName, string id) : this(displayName)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException("id");
			}
			this._id = id;
		}

		// Token: 0x06004367 RID: 17255 RVA: 0x000DE0D5 File Offset: 0x000DC2D5
		public ConnectionConsumerAttribute(string displayName, Type connectionPointType) : this(displayName)
		{
			if (connectionPointType == null)
			{
				throw new ArgumentNullException("connectionPointType");
			}
			this._connectionPointType = connectionPointType;
		}

		// Token: 0x06004368 RID: 17256 RVA: 0x000DE0F9 File Offset: 0x000DC2F9
		public ConnectionConsumerAttribute(string displayName, string id, Type connectionPointType) : this(displayName, connectionPointType)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException("id");
			}
			this._id = id;
		}

		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x06004369 RID: 17257 RVA: 0x000DE11D File Offset: 0x000DC31D
		// (set) Token: 0x0600436A RID: 17258 RVA: 0x000DE125 File Offset: 0x000DC325
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

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x0600436B RID: 17259 RVA: 0x000DE12E File Offset: 0x000DC32E
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

		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x0600436C RID: 17260 RVA: 0x000DE144 File Offset: 0x000DC344
		public virtual string DisplayName
		{
			get
			{
				return this.DisplayNameValue;
			}
		}

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x0600436D RID: 17261 RVA: 0x000DE14C File Offset: 0x000DC34C
		// (set) Token: 0x0600436E RID: 17262 RVA: 0x000DE154 File Offset: 0x000DC354
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

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x0600436F RID: 17263 RVA: 0x000DE15D File Offset: 0x000DC35D
		public Type ConnectionPointType
		{
			get
			{
				if (WebPartUtil.IsConnectionPointTypeValid(this._connectionPointType, true))
				{
					return this._connectionPointType;
				}
				throw new InvalidOperationException(SR.GetString("ConnectionConsumerAttribute_InvalidConnectionPointType", new object[]
				{
					this._connectionPointType.Name
				}));
			}
		}

		// Token: 0x040025DB RID: 9691
		private string _displayName;

		// Token: 0x040025DC RID: 9692
		private string _id;

		// Token: 0x040025DD RID: 9693
		private Type _connectionPointType;

		// Token: 0x040025DE RID: 9694
		private bool _allowsMultipleConnections;
	}
}
