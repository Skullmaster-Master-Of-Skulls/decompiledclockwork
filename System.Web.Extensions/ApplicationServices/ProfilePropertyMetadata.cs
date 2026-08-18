using System;
using System.Runtime.Serialization;

namespace System.Web.ApplicationServices
{
	// Token: 0x02000120 RID: 288
	[DataContract]
	public class ProfilePropertyMetadata : IExtensibleDataObject
	{
		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0003621C File Offset: 0x0003441C
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x00036224 File Offset: 0x00034424
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this._extensionDataObject;
			}
			set
			{
				this._extensionDataObject = value;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x0003622D File Offset: 0x0003442D
		// (set) Token: 0x06000F04 RID: 3844 RVA: 0x00036235 File Offset: 0x00034435
		[DataMember]
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
			set
			{
				this._propertyName = value;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x0003623E File Offset: 0x0003443E
		// (set) Token: 0x06000F06 RID: 3846 RVA: 0x00036246 File Offset: 0x00034446
		[DataMember]
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
			set
			{
				this._typeName = value;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0003624F File Offset: 0x0003444F
		// (set) Token: 0x06000F08 RID: 3848 RVA: 0x00036257 File Offset: 0x00034457
		[DataMember]
		public bool AllowAnonymousAccess
		{
			get
			{
				return this._allowAnonymousAccess;
			}
			set
			{
				this._allowAnonymousAccess = value;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x00036260 File Offset: 0x00034460
		// (set) Token: 0x06000F0A RID: 3850 RVA: 0x00036268 File Offset: 0x00034468
		[DataMember]
		public bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
			set
			{
				this._isReadOnly = value;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x00036271 File Offset: 0x00034471
		// (set) Token: 0x06000F0C RID: 3852 RVA: 0x00036279 File Offset: 0x00034479
		[DataMember]
		public int SerializeAs
		{
			get
			{
				return this._serializeAs;
			}
			set
			{
				this._serializeAs = value;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x00036282 File Offset: 0x00034482
		// (set) Token: 0x06000F0E RID: 3854 RVA: 0x0003628A File Offset: 0x0003448A
		[DataMember]
		public string DefaultValue
		{
			get
			{
				return this._defaultValue;
			}
			set
			{
				this._defaultValue = value;
			}
		}

		// Token: 0x0400043E RID: 1086
		private ExtensionDataObject _extensionDataObject;

		// Token: 0x0400043F RID: 1087
		private string _propertyName;

		// Token: 0x04000440 RID: 1088
		private string _typeName;

		// Token: 0x04000441 RID: 1089
		private bool _allowAnonymousAccess;

		// Token: 0x04000442 RID: 1090
		private bool _isReadOnly;

		// Token: 0x04000443 RID: 1091
		private int _serializeAs;

		// Token: 0x04000444 RID: 1092
		private string _defaultValue;
	}
}
