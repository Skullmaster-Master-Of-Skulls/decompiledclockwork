using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Telerik.Web.UI
{
	// Token: 0x02000BC0 RID: 3008
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ClientEvents
	{
		// Token: 0x06007346 RID: 29510 RVA: 0x001AFADD File Offset: 0x001ADCDD
		public ClientEvents()
		{
			this._onSuccess = "";
			this._onFail = "";
			this._onRequesting = "";
		}

		// Token: 0x17002588 RID: 9608
		// (get) Token: 0x06007347 RID: 29511 RVA: 0x001AFB06 File Offset: 0x001ADD06
		// (set) Token: 0x06007348 RID: 29512 RVA: 0x001AFB0E File Offset: 0x001ADD0E
		[Description("This event is fired when a request to the server is about to be sent.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		public virtual string Requesting
		{
			get
			{
				return this._onRequesting;
			}
			set
			{
				this._onRequesting = value;
			}
		}

		// Token: 0x17002589 RID: 9609
		// (get) Token: 0x06007349 RID: 29513 RVA: 0x001AFB17 File Offset: 0x001ADD17
		// (set) Token: 0x0600734A RID: 29514 RVA: 0x001AFB1F File Offset: 0x001ADD1F
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("This event is fired when a request to the server is started.")]
		[NotifyParentProperty(true)]
		public virtual string RequestSucceeded
		{
			get
			{
				return this._onSuccess;
			}
			set
			{
				this._onSuccess = value;
			}
		}

		// Token: 0x1700258A RID: 9610
		// (get) Token: 0x0600734B RID: 29515 RVA: 0x001AFB28 File Offset: 0x001ADD28
		// (set) Token: 0x0600734C RID: 29516 RVA: 0x001AFB30 File Offset: 0x001ADD30
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Client-side events")]
		[Description("This event is fired when a request to the server is started.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string RequestFailed
		{
			get
			{
				return this._onFail;
			}
			set
			{
				this._onFail = value;
			}
		}

		// Token: 0x04001F3D RID: 7997
		private string _onSuccess;

		// Token: 0x04001F3E RID: 7998
		private string _onFail;

		// Token: 0x04001F3F RID: 7999
		private string _onRequesting;
	}
}
