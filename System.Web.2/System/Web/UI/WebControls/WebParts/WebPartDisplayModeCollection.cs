using System;
using System.Collections;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000596 RID: 1430
	public sealed class WebPartDisplayModeCollection : CollectionBase
	{
		// Token: 0x06004811 RID: 18449 RVA: 0x000170A2 File Offset: 0x000152A2
		internal WebPartDisplayModeCollection()
		{
		}

		// Token: 0x17001551 RID: 5457
		// (get) Token: 0x06004812 RID: 18450 RVA: 0x000ECCED File Offset: 0x000EAEED
		public bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		// Token: 0x17001552 RID: 5458
		public WebPartDisplayMode this[int index]
		{
			get
			{
				return (WebPartDisplayMode)base.List[index];
			}
		}

		// Token: 0x17001553 RID: 5459
		public WebPartDisplayMode this[string modeName]
		{
			get
			{
				foreach (object obj in base.List)
				{
					WebPartDisplayMode webPartDisplayMode = (WebPartDisplayMode)obj;
					if (string.Equals(webPartDisplayMode.Name, modeName, StringComparison.OrdinalIgnoreCase))
					{
						return webPartDisplayMode;
					}
				}
				return null;
			}
		}

		// Token: 0x06004815 RID: 18453 RVA: 0x000170DB File Offset: 0x000152DB
		public int Add(WebPartDisplayMode value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06004816 RID: 18454 RVA: 0x000ECD70 File Offset: 0x000EAF70
		internal int AddInternal(WebPartDisplayMode value)
		{
			bool readOnly = this._readOnly;
			this._readOnly = false;
			int result;
			try
			{
				try
				{
					result = base.List.Add(value);
				}
				finally
				{
					this._readOnly = readOnly;
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004817 RID: 18455 RVA: 0x000ECDC4 File Offset: 0x000EAFC4
		private void CheckReadOnly()
		{
			if (this._readOnly)
			{
				throw new InvalidOperationException(SR.GetString(this._readOnlyExceptionMessage));
			}
		}

		// Token: 0x06004818 RID: 18456 RVA: 0x00017184 File Offset: 0x00015384
		public bool Contains(WebPartDisplayMode value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06004819 RID: 18457 RVA: 0x00017192 File Offset: 0x00015392
		public void CopyTo(WebPartDisplayMode[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x0600481A RID: 18458 RVA: 0x000171A1 File Offset: 0x000153A1
		public int IndexOf(WebPartDisplayMode value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x0600481B RID: 18459 RVA: 0x000171AF File Offset: 0x000153AF
		public void Insert(int index, WebPartDisplayMode value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x0600481C RID: 18460 RVA: 0x000ECDDF File Offset: 0x000EAFDF
		protected override void OnClear()
		{
			throw new InvalidOperationException(SR.GetString("WebPartDisplayModeCollection_CantRemove"));
		}

		// Token: 0x0600481D RID: 18461 RVA: 0x000ECDF0 File Offset: 0x000EAFF0
		protected override void OnInsert(int index, object value)
		{
			this.CheckReadOnly();
			WebPartDisplayMode webPartDisplayMode = (WebPartDisplayMode)value;
			foreach (object obj in base.List)
			{
				WebPartDisplayMode webPartDisplayMode2 = (WebPartDisplayMode)obj;
				if (webPartDisplayMode.Name == webPartDisplayMode2.Name)
				{
					throw new ArgumentException(SR.GetString("WebPartDisplayModeCollection_DuplicateName", new object[]
					{
						webPartDisplayMode.Name
					}));
				}
			}
			base.OnInsert(index, value);
		}

		// Token: 0x0600481E RID: 18462 RVA: 0x000ECDDF File Offset: 0x000EAFDF
		protected override void OnRemove(int index, object value)
		{
			throw new InvalidOperationException(SR.GetString("WebPartDisplayModeCollection_CantRemove"));
		}

		// Token: 0x0600481F RID: 18463 RVA: 0x000ECE8C File Offset: 0x000EB08C
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			throw new InvalidOperationException(SR.GetString("WebPartDisplayModeCollection_CantSet"));
		}

		// Token: 0x06004820 RID: 18464 RVA: 0x000ECEA0 File Offset: 0x000EB0A0
		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.GetString("Collection_CantAddNull"));
			}
			if (!(value is WebPartDisplayMode))
			{
				throw new ArgumentException(SR.GetString("Collection_InvalidType", new object[]
				{
					"WebPartDisplayMode"
				}), "value");
			}
		}

		// Token: 0x06004821 RID: 18465 RVA: 0x000ECEF7 File Offset: 0x000EB0F7
		internal void SetReadOnly(string exceptionMessage)
		{
			this._readOnlyExceptionMessage = exceptionMessage;
			this._readOnly = true;
		}

		// Token: 0x04002720 RID: 10016
		private bool _readOnly;

		// Token: 0x04002721 RID: 10017
		private string _readOnlyExceptionMessage;
	}
}
