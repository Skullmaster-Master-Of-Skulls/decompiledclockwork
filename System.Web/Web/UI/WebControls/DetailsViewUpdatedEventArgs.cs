using System;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200056A RID: 1386
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DetailsViewUpdatedEventArgs : EventArgs
	{
		// Token: 0x06004442 RID: 17474 RVA: 0x00119A19 File Offset: 0x00118A19
		public DetailsViewUpdatedEventArgs(int affectedRows, Exception e)
		{
			this._affectedRows = affectedRows;
			this._exceptionHandled = false;
			this._exception = e;
			this._keepInEditMode = false;
		}

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x06004443 RID: 17475 RVA: 0x00119A3D File Offset: 0x00118A3D
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
		}

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x06004444 RID: 17476 RVA: 0x00119A45 File Offset: 0x00118A45
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x06004445 RID: 17477 RVA: 0x00119A4D File Offset: 0x00118A4D
		// (set) Token: 0x06004446 RID: 17478 RVA: 0x00119A55 File Offset: 0x00118A55
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

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x06004447 RID: 17479 RVA: 0x00119A5E File Offset: 0x00118A5E
		// (set) Token: 0x06004448 RID: 17480 RVA: 0x00119A66 File Offset: 0x00118A66
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

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x06004449 RID: 17481 RVA: 0x00119A6F File Offset: 0x00118A6F
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

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x0600444A RID: 17482 RVA: 0x00119A8A File Offset: 0x00118A8A
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

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x0600444B RID: 17483 RVA: 0x00119AA5 File Offset: 0x00118AA5
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

		// Token: 0x0600444C RID: 17484 RVA: 0x00119AC0 File Offset: 0x00118AC0
		internal void SetKeys(IOrderedDictionary keys)
		{
			this._keys = keys;
		}

		// Token: 0x0600444D RID: 17485 RVA: 0x00119AC9 File Offset: 0x00118AC9
		internal void SetNewValues(IOrderedDictionary newValues)
		{
			this._values = newValues;
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x00119AD2 File Offset: 0x00118AD2
		internal void SetOldValues(IOrderedDictionary oldValues)
		{
			this._oldValues = oldValues;
		}

		// Token: 0x040029A7 RID: 10663
		private int _affectedRows;

		// Token: 0x040029A8 RID: 10664
		private Exception _exception;

		// Token: 0x040029A9 RID: 10665
		private bool _exceptionHandled;

		// Token: 0x040029AA RID: 10666
		private bool _keepInEditMode;

		// Token: 0x040029AB RID: 10667
		private IOrderedDictionary _values;

		// Token: 0x040029AC RID: 10668
		private IOrderedDictionary _keys;

		// Token: 0x040029AD RID: 10669
		private IOrderedDictionary _oldValues;
	}
}
