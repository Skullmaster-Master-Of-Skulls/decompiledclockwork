using System;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000593 RID: 1427
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class FormViewUpdatedEventArgs : EventArgs
	{
		// Token: 0x060045F6 RID: 17910 RVA: 0x0011EB68 File Offset: 0x0011DB68
		public FormViewUpdatedEventArgs(int affectedRows, Exception e)
		{
			this._affectedRows = affectedRows;
			this._exceptionHandled = false;
			this._exception = e;
			this._keepInEditMode = false;
		}

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x060045F7 RID: 17911 RVA: 0x0011EB8C File Offset: 0x0011DB8C
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
		}

		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x060045F8 RID: 17912 RVA: 0x0011EB94 File Offset: 0x0011DB94
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x060045F9 RID: 17913 RVA: 0x0011EB9C File Offset: 0x0011DB9C
		// (set) Token: 0x060045FA RID: 17914 RVA: 0x0011EBA4 File Offset: 0x0011DBA4
		public bool ExceptionHandled
		{
			get
			{
				return this._exceptionHandled;
			}
			set
			{
				this._exceptionHandled = value;
			}
		}

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x060045FB RID: 17915 RVA: 0x0011EBAD File Offset: 0x0011DBAD
		// (set) Token: 0x060045FC RID: 17916 RVA: 0x0011EBB5 File Offset: 0x0011DBB5
		public bool KeepInEditMode
		{
			get
			{
				return this._keepInEditMode;
			}
			set
			{
				this._keepInEditMode = value;
			}
		}

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x060045FD RID: 17917 RVA: 0x0011EBBE File Offset: 0x0011DBBE
		public IOrderedDictionary Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new OrderedDictionary();
				}
				return this._keys;
			}
		}

		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x060045FE RID: 17918 RVA: 0x0011EBD9 File Offset: 0x0011DBD9
		public IOrderedDictionary NewValues
		{
			get
			{
				if (this._values == null)
				{
					this._values = new OrderedDictionary();
				}
				return this._values;
			}
		}

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x060045FF RID: 17919 RVA: 0x0011EBF4 File Offset: 0x0011DBF4
		public IOrderedDictionary OldValues
		{
			get
			{
				if (this._oldValues == null)
				{
					this._oldValues = new OrderedDictionary();
				}
				return this._oldValues;
			}
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x0011EC0F File Offset: 0x0011DC0F
		internal void SetKeys(IOrderedDictionary keys)
		{
			this._keys = keys;
		}

		// Token: 0x06004601 RID: 17921 RVA: 0x0011EC18 File Offset: 0x0011DC18
		internal void SetNewValues(IOrderedDictionary newValues)
		{
			this._values = newValues;
		}

		// Token: 0x06004602 RID: 17922 RVA: 0x0011EC21 File Offset: 0x0011DC21
		internal void SetOldValues(IOrderedDictionary oldValues)
		{
			this._oldValues = oldValues;
		}

		// Token: 0x04002A27 RID: 10791
		private int _affectedRows;

		// Token: 0x04002A28 RID: 10792
		private Exception _exception;

		// Token: 0x04002A29 RID: 10793
		private bool _exceptionHandled;

		// Token: 0x04002A2A RID: 10794
		private bool _keepInEditMode;

		// Token: 0x04002A2B RID: 10795
		private IOrderedDictionary _values;

		// Token: 0x04002A2C RID: 10796
		private IOrderedDictionary _keys;

		// Token: 0x04002A2D RID: 10797
		private IOrderedDictionary _oldValues;
	}
}
