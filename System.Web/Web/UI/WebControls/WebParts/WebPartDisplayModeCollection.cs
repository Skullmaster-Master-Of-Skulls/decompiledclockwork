using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000722 RID: 1826
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class WebPartDisplayModeCollection : CollectionBase
	{
		// Token: 0x06005884 RID: 22660 RVA: 0x00163CA4 File Offset: 0x00162CA4
		internal WebPartDisplayModeCollection()
		{
		}

		// Token: 0x170016F0 RID: 5872
		// (get) Token: 0x06005885 RID: 22661 RVA: 0x00163CAC File Offset: 0x00162CAC
		public bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		// Token: 0x170016F1 RID: 5873
		public WebPartDisplayMode this[int index]
		{
			get
			{
				return (WebPartDisplayMode)base.List[index];
			}
		}

		// Token: 0x170016F2 RID: 5874
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

		// Token: 0x06005888 RID: 22664 RVA: 0x00163D30 File Offset: 0x00162D30
		public int Add(WebPartDisplayMode value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06005889 RID: 22665 RVA: 0x00163D40 File Offset: 0x00162D40
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

		// Token: 0x0600588A RID: 22666 RVA: 0x00163D94 File Offset: 0x00162D94
		private void CheckReadOnly()
		{
			if (this._readOnly)
			{
				throw new InvalidOperationException(SR.GetString(this._readOnlyExceptionMessage));
			}
		}

		// Token: 0x0600588B RID: 22667 RVA: 0x00163DAF File Offset: 0x00162DAF
		public bool Contains(WebPartDisplayMode value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x0600588C RID: 22668 RVA: 0x00163DBD File Offset: 0x00162DBD
		public void CopyTo(WebPartDisplayMode[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x0600588D RID: 22669 RVA: 0x00163DCC File Offset: 0x00162DCC
		public int IndexOf(WebPartDisplayMode value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x0600588E RID: 22670 RVA: 0x00163DDA File Offset: 0x00162DDA
		public void Insert(int index, WebPartDisplayMode value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x0600588F RID: 22671 RVA: 0x00163DE9 File Offset: 0x00162DE9
		protected override void OnClear()
		{
			throw new InvalidOperationException(SR.GetString("WebPartDisplayModeCollection_CantRemove"));
		}

		// Token: 0x06005890 RID: 22672 RVA: 0x00163DFC File Offset: 0x00162DFC
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

		// Token: 0x06005891 RID: 22673 RVA: 0x00163E9C File Offset: 0x00162E9C
		protected override void OnRemove(int index, object value)
		{
			throw new InvalidOperationException(SR.GetString("WebPartDisplayModeCollection_CantRemove"));
		}

		// Token: 0x06005892 RID: 22674 RVA: 0x00163EAD File Offset: 0x00162EAD
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			throw new InvalidOperationException(SR.GetString("WebPartDisplayModeCollection_CantSet"));
		}

		// Token: 0x06005893 RID: 22675 RVA: 0x00163EC0 File Offset: 0x00162EC0
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

		// Token: 0x06005894 RID: 22676 RVA: 0x00163F19 File Offset: 0x00162F19
		internal void SetReadOnly(string exceptionMessage)
		{
			this._readOnlyExceptionMessage = exceptionMessage;
			this._readOnly = true;
		}

		// Token: 0x04002FE9 RID: 12265
		private bool _readOnly;

		// Token: 0x04002FEA RID: 12266
		private string _readOnlyExceptionMessage;
	}
}
