using System;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200055A RID: 1370
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DetailsViewDeletedEventArgs : EventArgs
	{
		// Token: 0x060043FB RID: 17403 RVA: 0x00119759 File Offset: 0x00118759
		public DetailsViewDeletedEventArgs(int affectedRows, Exception e)
		{
			this._affectedRows = affectedRows;
			this._exceptionHandled = false;
			this._exception = e;
		}

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x060043FC RID: 17404 RVA: 0x00119776 File Offset: 0x00118776
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x060043FD RID: 17405 RVA: 0x0011977E File Offset: 0x0011877E
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x060043FE RID: 17406 RVA: 0x00119786 File Offset: 0x00118786
		// (set) Token: 0x060043FF RID: 17407 RVA: 0x0011978E File Offset: 0x0011878E
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

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x06004400 RID: 17408 RVA: 0x00119797 File Offset: 0x00118797
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

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x06004401 RID: 17409 RVA: 0x001197B2 File Offset: 0x001187B2
		public IOrderedDictionary Values
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

		// Token: 0x06004402 RID: 17410 RVA: 0x001197CD File Offset: 0x001187CD
		internal void SetKeys(IOrderedDictionary keys)
		{
			this._keys = keys;
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x001197D6 File Offset: 0x001187D6
		internal void SetValues(IOrderedDictionary values)
		{
			this._values = values;
		}

		// Token: 0x0400298D RID: 10637
		private int _affectedRows;

		// Token: 0x0400298E RID: 10638
		private Exception _exception;

		// Token: 0x0400298F RID: 10639
		private bool _exceptionHandled;

		// Token: 0x04002990 RID: 10640
		private IOrderedDictionary _keys;

		// Token: 0x04002991 RID: 10641
		private IOrderedDictionary _values;
	}
}
