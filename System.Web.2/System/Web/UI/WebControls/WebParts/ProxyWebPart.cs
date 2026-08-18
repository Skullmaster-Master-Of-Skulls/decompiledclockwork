using System;
using System.ComponentModel;
using System.Web.Util;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000565 RID: 1381
	[ToolboxItem(false)]
	public abstract class ProxyWebPart : WebPart
	{
		// Token: 0x06004615 RID: 17941 RVA: 0x000E7224 File Offset: 0x000E5424
		protected ProxyWebPart(WebPart webPart)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			GenericWebPart genericWebPart = webPart as GenericWebPart;
			if (genericWebPart != null)
			{
				Control childControl = genericWebPart.ChildControl;
				if (childControl == null)
				{
					throw new ArgumentException(SR.GetString("PropertyCannotBeNull", new object[]
					{
						"ChildControl"
					}), "webPart");
				}
				this._originalID = childControl.ID;
				if (string.IsNullOrEmpty(this._originalID))
				{
					throw new ArgumentException(SR.GetString("PropertyCannotBeNullOrEmptyString", new object[]
					{
						"ChildControl.ID"
					}), "webPart");
				}
				UserControl userControl = childControl as UserControl;
				Type type;
				if (userControl != null)
				{
					type = typeof(UserControl);
					this._originalPath = userControl.AppRelativeVirtualPath;
				}
				else
				{
					type = childControl.GetType();
				}
				this._originalTypeName = WebPartUtil.SerializeType(type);
				this._genericWebPartID = genericWebPart.ID;
				if (string.IsNullOrEmpty(this._genericWebPartID))
				{
					throw new ArgumentException(SR.GetString("PropertyCannotBeNullOrEmptyString", new object[]
					{
						"ID"
					}), "webPart");
				}
				this.ID = this._genericWebPartID;
				return;
			}
			else
			{
				this._originalID = webPart.ID;
				if (string.IsNullOrEmpty(this._originalID))
				{
					throw new ArgumentException(SR.GetString("PropertyCannotBeNullOrEmptyString", new object[]
					{
						"ID"
					}), "webPart");
				}
				this._originalTypeName = WebPartUtil.SerializeType(webPart.GetType());
				this.ID = this._originalID;
				return;
			}
		}

		// Token: 0x06004616 RID: 17942 RVA: 0x000E7394 File Offset: 0x000E5594
		protected ProxyWebPart(string originalID, string originalTypeName, string originalPath, string genericWebPartID)
		{
			if (string.IsNullOrEmpty(originalID))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("originalID");
			}
			if (string.IsNullOrEmpty(originalTypeName))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("originalTypeName");
			}
			if (!string.IsNullOrEmpty(originalPath) && string.IsNullOrEmpty(genericWebPartID))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("genericWebPartID");
			}
			this._originalID = originalID;
			this._originalTypeName = originalTypeName;
			this._originalPath = originalPath;
			this._genericWebPartID = genericWebPartID;
			if (!string.IsNullOrEmpty(genericWebPartID))
			{
				this.ID = this._genericWebPartID;
				return;
			}
			this.ID = this._originalID;
		}

		// Token: 0x170014A6 RID: 5286
		// (get) Token: 0x06004617 RID: 17943 RVA: 0x000E7428 File Offset: 0x000E5628
		public string GenericWebPartID
		{
			get
			{
				if (this._genericWebPartID == null)
				{
					return string.Empty;
				}
				return this._genericWebPartID;
			}
		}

		// Token: 0x170014A7 RID: 5287
		// (get) Token: 0x06004618 RID: 17944 RVA: 0x00069884 File Offset: 0x00067A84
		// (set) Token: 0x06004619 RID: 17945 RVA: 0x0006988C File Offset: 0x00067A8C
		public sealed override string ID
		{
			get
			{
				return base.ID;
			}
			set
			{
				base.ID = value;
			}
		}

		// Token: 0x170014A8 RID: 5288
		// (get) Token: 0x0600461A RID: 17946 RVA: 0x000E743E File Offset: 0x000E563E
		public string OriginalID
		{
			get
			{
				if (this._originalID == null)
				{
					return string.Empty;
				}
				return this._originalID;
			}
		}

		// Token: 0x170014A9 RID: 5289
		// (get) Token: 0x0600461B RID: 17947 RVA: 0x000E7454 File Offset: 0x000E5654
		public string OriginalTypeName
		{
			get
			{
				if (this._originalTypeName == null)
				{
					return string.Empty;
				}
				return this._originalTypeName;
			}
		}

		// Token: 0x170014AA RID: 5290
		// (get) Token: 0x0600461C RID: 17948 RVA: 0x000E746A File Offset: 0x000E566A
		public string OriginalPath
		{
			get
			{
				if (this._originalPath == null)
				{
					return string.Empty;
				}
				return this._originalPath;
			}
		}

		// Token: 0x0600461D RID: 17949 RVA: 0x00006164 File Offset: 0x00004364
		protected internal override void LoadControlState(object savedState)
		{
		}

		// Token: 0x0600461E RID: 17950 RVA: 0x00006164 File Offset: 0x00004364
		protected override void LoadViewState(object savedState)
		{
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x000E7480 File Offset: 0x000E5680
		protected internal override object SaveControlState()
		{
			base.SaveControlState();
			return null;
		}

		// Token: 0x06004620 RID: 17952 RVA: 0x000E748A File Offset: 0x000E568A
		protected override object SaveViewState()
		{
			base.SaveViewState();
			return null;
		}

		// Token: 0x04002690 RID: 9872
		private string _originalID;

		// Token: 0x04002691 RID: 9873
		private string _originalTypeName;

		// Token: 0x04002692 RID: 9874
		private string _originalPath;

		// Token: 0x04002693 RID: 9875
		private string _genericWebPartID;
	}
}
