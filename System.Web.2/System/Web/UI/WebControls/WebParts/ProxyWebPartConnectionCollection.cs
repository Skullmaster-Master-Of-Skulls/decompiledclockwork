using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000566 RID: 1382
	[Editor("System.ComponentModel.Design.CollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public sealed class ProxyWebPartConnectionCollection : CollectionBase
	{
		// Token: 0x170014AB RID: 5291
		// (get) Token: 0x06004621 RID: 17953 RVA: 0x000E7494 File Offset: 0x000E5694
		public bool IsReadOnly
		{
			get
			{
				return this._webPartManager != null && this._webPartManager.StaticConnections.IsReadOnly;
			}
		}

		// Token: 0x170014AC RID: 5292
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

		// Token: 0x170014AD RID: 5293
		public WebPartConnection this[string id]
		{
			get
			{
				foreach (object obj in base.List)
				{
					WebPartConnection webPartConnection = (WebPartConnection)obj;
					if (webPartConnection != null && string.Equals(webPartConnection.ID, id, StringComparison.OrdinalIgnoreCase))
					{
						return webPartConnection;
					}
				}
				return null;
			}
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x000170DB File Offset: 0x000152DB
		public int Add(WebPartConnection value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x000E7530 File Offset: 0x000E5730
		private void CheckReadOnly()
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(SR.GetString("ProxyWebPartConnectionCollection_ReadOnly"));
			}
		}

		// Token: 0x06004627 RID: 17959 RVA: 0x00017184 File Offset: 0x00015384
		public bool Contains(WebPartConnection value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06004628 RID: 17960 RVA: 0x00017192 File Offset: 0x00015392
		public void CopyTo(WebPartConnection[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x000171A1 File Offset: 0x000153A1
		public int IndexOf(WebPartConnection value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x000171AF File Offset: 0x000153AF
		public void Insert(int index, WebPartConnection value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x000E754C File Offset: 0x000E574C
		protected override void OnClear()
		{
			this.CheckReadOnly();
			if (this._webPartManager != null)
			{
				foreach (object obj in this)
				{
					WebPartConnection value = (WebPartConnection)obj;
					this._webPartManager.StaticConnections.Remove(value);
				}
			}
			base.OnClear();
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x000E75C0 File Offset: 0x000E57C0
		protected override void OnInsert(int index, object value)
		{
			this.CheckReadOnly();
			if (this._webPartManager != null)
			{
				this._webPartManager.StaticConnections.Insert(index, (WebPartConnection)value);
			}
			base.OnInsert(index, value);
		}

		// Token: 0x0600462D RID: 17965 RVA: 0x000E75EF File Offset: 0x000E57EF
		protected override void OnRemove(int index, object value)
		{
			this.CheckReadOnly();
			if (this._webPartManager != null)
			{
				this._webPartManager.StaticConnections.Remove((WebPartConnection)value);
			}
			base.OnRemove(index, value);
		}

		// Token: 0x0600462E RID: 17966 RVA: 0x000E7620 File Offset: 0x000E5820
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.CheckReadOnly();
			if (this._webPartManager != null)
			{
				int index2 = this._webPartManager.StaticConnections.IndexOf((WebPartConnection)oldValue);
				this._webPartManager.StaticConnections[index2] = (WebPartConnection)newValue;
			}
			base.OnSet(index, oldValue, newValue);
		}

		// Token: 0x0600462F RID: 17967 RVA: 0x000E7674 File Offset: 0x000E5874
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
				}));
			}
		}

		// Token: 0x06004630 RID: 17968 RVA: 0x000171BE File Offset: 0x000153BE
		public void Remove(WebPartConnection value)
		{
			base.List.Remove(value);
		}

		// Token: 0x06004631 RID: 17969 RVA: 0x000E76C8 File Offset: 0x000E58C8
		internal void SetWebPartManager(WebPartManager webPartManager)
		{
			this._webPartManager = webPartManager;
			foreach (object obj in this)
			{
				WebPartConnection value = (WebPartConnection)obj;
				this._webPartManager.StaticConnections.Add(value);
			}
		}

		// Token: 0x04002694 RID: 9876
		private WebPartManager _webPartManager;
	}
}
