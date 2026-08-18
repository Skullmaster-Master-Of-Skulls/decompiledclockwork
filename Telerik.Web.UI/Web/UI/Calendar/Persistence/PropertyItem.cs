using System;

namespace Telerik.Web.UI.Calendar.Persistence
{
	// Token: 0x02001005 RID: 4101
	public class PropertyItem
	{
		// Token: 0x0600A03F RID: 41023 RVA: 0x0023A76E File Offset: 0x0023896E
		internal PropertyItem(object initialValue)
		{
			this._RealValue = initialValue;
			this._IsDirty = false;
		}

		// Token: 0x0600A040 RID: 41024 RVA: 0x0023A784 File Offset: 0x00238984
		public object PersistStateValue()
		{
			if (this.IsDefault)
			{
				return null;
			}
			return this._RealValue;
		}

		// Token: 0x0600A041 RID: 41025 RVA: 0x0023A796 File Offset: 0x00238996
		public object PersistStateValue(object comparerValue)
		{
			if (this._RealValue.Equals(comparerValue))
			{
				return null;
			}
			return this._RealValue;
		}

		// Token: 0x170032A5 RID: 12965
		// (get) Token: 0x0600A042 RID: 41026 RVA: 0x0023A7AE File Offset: 0x002389AE
		// (set) Token: 0x0600A043 RID: 41027 RVA: 0x0023A7B6 File Offset: 0x002389B6
		public bool IsDirty
		{
			get
			{
				return this._IsDirty;
			}
			set
			{
				this._IsDirty = value;
			}
		}

		// Token: 0x170032A6 RID: 12966
		// (get) Token: 0x0600A044 RID: 41028 RVA: 0x0023A7BF File Offset: 0x002389BF
		public bool IsDefault
		{
			get
			{
				return this._DefaultValue.Equals(this._RealValue);
			}
		}

		// Token: 0x170032A7 RID: 12967
		// (get) Token: 0x0600A045 RID: 41029 RVA: 0x0023A7D2 File Offset: 0x002389D2
		// (set) Token: 0x0600A046 RID: 41030 RVA: 0x0023A7DA File Offset: 0x002389DA
		public object Value
		{
			get
			{
				return this._RealValue;
			}
			set
			{
				this._RealValue = value;
			}
		}

		// Token: 0x170032A8 RID: 12968
		// (get) Token: 0x0600A047 RID: 41031 RVA: 0x0023A7E3 File Offset: 0x002389E3
		// (set) Token: 0x0600A048 RID: 41032 RVA: 0x0023A7EB File Offset: 0x002389EB
		public object DefaultValue
		{
			get
			{
				return this._DefaultValue;
			}
			set
			{
				this._DefaultValue = value;
			}
		}

		// Token: 0x04002CDC RID: 11484
		private bool _IsDirty;

		// Token: 0x04002CDD RID: 11485
		private object _RealValue;

		// Token: 0x04002CDE RID: 11486
		private object _DefaultValue;
	}
}
