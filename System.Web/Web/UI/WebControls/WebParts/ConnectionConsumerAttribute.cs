using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020006AE RID: 1710
	[AttributeUsage(AttributeTargets.Method)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ConnectionConsumerAttribute : Attribute
	{
		// Token: 0x060053CD RID: 21453 RVA: 0x001545D3 File Offset: 0x001535D3
		public ConnectionConsumerAttribute(string displayName)
		{
			if (string.IsNullOrEmpty(displayName))
			{
				throw new ArgumentNullException("displayName");
			}
			this._displayName = displayName;
			this._allowsMultipleConnections = false;
		}

		// Token: 0x060053CE RID: 21454 RVA: 0x001545FC File Offset: 0x001535FC
		public ConnectionConsumerAttribute(string displayName, string id) : this(displayName)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException("id");
			}
			this._id = id;
		}

		// Token: 0x060053CF RID: 21455 RVA: 0x0015461F File Offset: 0x0015361F
		public ConnectionConsumerAttribute(string displayName, Type connectionPointType) : this(displayName)
		{
			if (connectionPointType == null)
			{
				throw new ArgumentNullException("connectionPointType");
			}
			this._connectionPointType = connectionPointType;
		}

		// Token: 0x060053D0 RID: 21456 RVA: 0x0015463D File Offset: 0x0015363D
		public ConnectionConsumerAttribute(string displayName, string id, Type connectionPointType) : this(displayName, connectionPointType)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException("id");
			}
			this._id = id;
		}

		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x060053D1 RID: 21457 RVA: 0x00154661 File Offset: 0x00153661
		// (set) Token: 0x060053D2 RID: 21458 RVA: 0x00154669 File Offset: 0x00153669
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

		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x060053D3 RID: 21459 RVA: 0x00154672 File Offset: 0x00153672
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

		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x060053D4 RID: 21460 RVA: 0x00154688 File Offset: 0x00153688
		public virtual string DisplayName
		{
			get
			{
				return this.DisplayNameValue;
			}
		}

		// Token: 0x1700155E RID: 5470
		// (get) Token: 0x060053D5 RID: 21461 RVA: 0x00154690 File Offset: 0x00153690
		// (set) Token: 0x060053D6 RID: 21462 RVA: 0x00154698 File Offset: 0x00153698
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

		// Token: 0x1700155F RID: 5471
		// (get) Token: 0x060053D7 RID: 21463 RVA: 0x001546A4 File Offset: 0x001536A4
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

		// Token: 0x04002E92 RID: 11922
		private string _displayName;

		// Token: 0x04002E93 RID: 11923
		private string _id;

		// Token: 0x04002E94 RID: 11924
		private Type _connectionPointType;

		// Token: 0x04002E95 RID: 11925
		private bool _allowsMultipleConnections;
	}
}
