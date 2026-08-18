using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000585 RID: 1413
	[Editor("System.ComponentModel.Design.CollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public sealed class WebPartConnectionCollection : CollectionBase
	{
		// Token: 0x060047A4 RID: 18340 RVA: 0x000EC434 File Offset: 0x000EA634
		internal WebPartConnectionCollection(WebPartManager webPartManager)
		{
			this._webPartManager = webPartManager;
		}

		// Token: 0x17001525 RID: 5413
		// (get) Token: 0x060047A5 RID: 18341 RVA: 0x000EC443 File Offset: 0x000EA643
		public bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		// Token: 0x17001526 RID: 5414
		public WebPartConnection this[int index]
		{
			get
			{
				return (WebPartConnection)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x17001527 RID: 5415
		public WebPartConnection this[string id]
		{
			get
			{
				foreach (object obj in base.List)
				{
					WebPartConnection webPartConnection = (WebPartConnection)obj;
					if (string.Equals(webPartConnection.ID, id, StringComparison.OrdinalIgnoreCase))
					{
						return webPartConnection;
					}
				}
				return null;
			}
		}

		// Token: 0x060047A9 RID: 18345 RVA: 0x000170DB File Offset: 0x000152DB
		public int Add(WebPartConnection value)
		{
			return base.List.Add(value);
		}

		// Token: 0x060047AA RID: 18346 RVA: 0x000EC4B4 File Offset: 0x000EA6B4
		private void CheckReadOnly()
		{
			if (this._readOnly)
			{
				throw new InvalidOperationException(SR.GetString(this._readOnlyExceptionMessage));
			}
		}

		// Token: 0x060047AB RID: 18347 RVA: 0x00017184 File Offset: 0x00015384
		public bool Contains(WebPartConnection value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x000EC4D0 File Offset: 0x000EA6D0
		internal bool ContainsProvider(WebPart provider)
		{
			foreach (object obj in base.List)
			{
				WebPartConnection webPartConnection = (WebPartConnection)obj;
				if (webPartConnection.Provider == provider)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060047AD RID: 18349 RVA: 0x00017192 File Offset: 0x00015392
		public void CopyTo(WebPartConnection[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x000171A1 File Offset: 0x000153A1
		public int IndexOf(WebPartConnection value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x000171AF File Offset: 0x000153AF
		public void Insert(int index, WebPartConnection value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x060047B0 RID: 18352 RVA: 0x000EC534 File Offset: 0x000EA734
		protected override void OnClear()
		{
			this.CheckReadOnly();
			base.OnClear();
		}

		// Token: 0x060047B1 RID: 18353 RVA: 0x000EC542 File Offset: 0x000EA742
		protected override void OnInsert(int index, object value)
		{
			this.CheckReadOnly();
			((WebPartConnection)value).SetWebPartManager(this._webPartManager);
			base.OnInsert(index, value);
		}

		// Token: 0x060047B2 RID: 18354 RVA: 0x000EC563 File Offset: 0x000EA763
		protected override void OnRemove(int index, object value)
		{
			this.CheckReadOnly();
			((WebPartConnection)value).SetWebPartManager(null);
			base.OnRemove(index, value);
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x000EC57F File Offset: 0x000EA77F
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.CheckReadOnly();
			((WebPartConnection)oldValue).SetWebPartManager(null);
			((WebPartConnection)newValue).SetWebPartManager(this._webPartManager);
			base.OnSet(index, oldValue, newValue);
		}

		// Token: 0x060047B4 RID: 18356 RVA: 0x000EC5B0 File Offset: 0x000EA7B0
		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.GetString("Collection_CantAddNull"));
			}
			if (!(value is WebPartConnection))
			{
				throw new ArgumentException(SR.GetString("Collection_InvalidType", new object[]
				{
					"WebPartConnection"
				}), "value");
			}
		}

		// Token: 0x060047B5 RID: 18357 RVA: 0x000171BE File Offset: 0x000153BE
		public void Remove(WebPartConnection value)
		{
			base.List.Remove(value);
		}

		// Token: 0x060047B6 RID: 18358 RVA: 0x000EC607 File Offset: 0x000EA807
		internal void SetReadOnly(string exceptionMessage)
		{
			this._readOnlyExceptionMessage = exceptionMessage;
			this._readOnly = true;
		}

		// Token: 0x04002707 RID: 9991
		private bool _readOnly;

		// Token: 0x04002708 RID: 9992
		private string _readOnlyExceptionMessage;

		// Token: 0x04002709 RID: 9993
		private WebPartManager _webPartManager;
	}
}
