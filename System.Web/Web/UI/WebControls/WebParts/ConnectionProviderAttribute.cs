using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020006B1 RID: 1713
	[AttributeUsage(AttributeTargets.Method)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ConnectionProviderAttribute : Attribute
	{
		// Token: 0x060053EA RID: 21482 RVA: 0x0015494F File Offset: 0x0015394F
		public ConnectionProviderAttribute(string displayName)
		{
			if (string.IsNullOrEmpty(displayName))
			{
				throw new ArgumentNullException("displayName");
			}
			this._displayName = displayName;
			this._allowsMultipleConnections = true;
		}

		// Token: 0x060053EB RID: 21483 RVA: 0x00154978 File Offset: 0x00153978
		public ConnectionProviderAttribute(string displayName, string id) : this(displayName)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException("id");
			}
			this._id = id;
		}

		// Token: 0x060053EC RID: 21484 RVA: 0x0015499B File Offset: 0x0015399B
		public ConnectionProviderAttribute(string displayName, Type connectionPointType) : this(displayName)
		{
			if (connectionPointType == null)
			{
				throw new ArgumentNullException("connectionPointType");
			}
			this._connectionPointType = connectionPointType;
		}

		// Token: 0x060053ED RID: 21485 RVA: 0x001549B9 File Offset: 0x001539B9
		public ConnectionProviderAttribute(string displayName, string id, Type connectionPointType) : this(displayName, connectionPointType)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException("id");
			}
			this._id = id;
		}

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x060053EE RID: 21486 RVA: 0x001549DD File Offset: 0x001539DD
		// (set) Token: 0x060053EF RID: 21487 RVA: 0x001549E5 File Offset: 0x001539E5
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

		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x060053F0 RID: 21488 RVA: 0x001549EE File Offset: 0x001539EE
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

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x060053F1 RID: 21489 RVA: 0x00154A04 File Offset: 0x00153A04
		public virtual string DisplayName
		{
			get
			{
				return this.DisplayNameValue;
			}
		}

		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x060053F2 RID: 21490 RVA: 0x00154A0C File Offset: 0x00153A0C
		// (set) Token: 0x060053F3 RID: 21491 RVA: 0x00154A14 File Offset: 0x00153A14
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

		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x060053F4 RID: 21492 RVA: 0x00154A20 File Offset: 0x00153A20
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

		// Token: 0x04002E9F RID: 11935
		private string _displayName;

		// Token: 0x04002EA0 RID: 11936
		private string _id;

		// Token: 0x04002EA1 RID: 11937
		private Type _connectionPointType;

		// Token: 0x04002EA2 RID: 11938
		private bool _allowsMultipleConnections;
	}
}
